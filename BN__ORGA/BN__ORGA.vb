Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ORGA

  Public oCleOrga As CLE__ORGA
  Public oCallParams As CLE__CLDP
  Public dsOrga As DataSet
  Public dcOrga As BindingSource = New BindingSource()
  Public strDescrContatti As String = ""
  Public bOrganizzazioneUnica As Boolean = False

#Region "Moduli"
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
#End Region

  Private components As System.ComponentModel.IContainer

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ORGA))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalvaNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbSkype = New NTSInformatica.NTSBarButtonItem
    Me.tlbEmail = New NTSInformatica.NTSBarButtonItem
    Me.tlbGeneraGuest = New NTSInformatica.NTSBarButtonItem
    Me.tlbRuoli = New NTSInformatica.NTSBarButtonItem
    Me.tlbInterna = New NTSInformatica.NTSBarButtonItem
    Me.tlbClienti = New NTSInformatica.NTSBarButtonItem
    Me.tlbFornitori = New NTSInformatica.NTSBarButtonItem
    Me.tlbLeads = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grOrga = New NTSInformatica.NTSGrid
    Me.grvOrga = New NTSInformatica.NTSGridView
    Me.og_progr = New NTSInformatica.NTSGridColumn
    Me.og_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.og_sede = New NTSInformatica.NTSGridColumn
    Me.og_divis = New NTSInformatica.NTSGridColumn
    Me.og_rep = New NTSInformatica.NTSGridColumn
    Me.og_codruaz = New NTSInformatica.NTSGridColumn
    Me.xx_codruaz = New NTSInformatica.NTSGridColumn
    Me.og_codcont = New NTSInformatica.NTSGridColumn
    Me.xx_codcont = New NTSInformatica.NTSGridColumn
    Me.og_telef = New NTSInformatica.NTSGridColumn
    Me.og_fax = New NTSInformatica.NTSGridColumn
    Me.og_email = New NTSInformatica.NTSGridColumn
    Me.og_dtiniz = New NTSInformatica.NTSGridColumn
    Me.og_dtfine = New NTSInformatica.NTSGridColumn
    Me.og_cell = New NTSInformatica.NTSGridColumn
    Me.og_descont = New NTSInformatica.NTSGridColumn
    Me.og_descont2 = New NTSInformatica.NTSGridColumn
    Me.og_titolo = New NTSInformatica.NTSGridColumn
    Me.og_indir = New NTSInformatica.NTSGridColumn
    Me.og_cap = New NTSInformatica.NTSGridColumn
    Me.og_citta = New NTSInformatica.NTSGridColumn
    Me.og_prov = New NTSInformatica.NTSGridColumn
    Me.og_stato = New NTSInformatica.NTSGridColumn
    Me.xx_stato = New NTSInformatica.NTSGridColumn
    Me.og_datnasc = New NTSInformatica.NTSGridColumn
    Me.og_sesso = New NTSInformatica.NTSGridColumn
    Me.og_codlead = New NTSInformatica.NTSGridColumn
    Me.og_coperat = New NTSInformatica.NTSGridColumn
    Me.og_mansioni = New NTSInformatica.NTSGridColumn
    Me.og_codcope = New NTSInformatica.NTSGridColumn
    Me.xx_codcope = New NTSInformatica.NTSGridColumn
    Me.og_codcage = New NTSInformatica.NTSGridColumn
    Me.xx_codcage = New NTSInformatica.NTSGridColumn
    Me.og_usaem = New NTSInformatica.NTSGridColumn
    Me.og_old = New NTSInformatica.NTSGridColumn
    Me.og_telefint = New NTSInformatica.NTSGridColumn
    Me.xx_descrizione = New NTSInformatica.NTSGridColumn
    Me.pnAll = New NTSInformatica.NTSPanel
    Me.pnPersone = New NTSInformatica.NTSPanel
    Me.pnRicerca = New NTSInformatica.NTSPanel
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.edRicerca = New NTSInformatica.NTSTextBoxStr
    Me.pnDati = New NTSInformatica.NTSPanel
    Me.pnDatiPersona = New NTSInformatica.NTSPanel
    Me.fmStato = New NTSInformatica.NTSGroupBox
    Me.ckOg_old = New NTSInformatica.NTSCheckBox
    Me.edOg_dtiniz = New NTSInformatica.NTSTextBoxData
    Me.lbOg_dtiniz = New NTSInformatica.NTSLabel
    Me.edOg_codstco = New NTSInformatica.NTSTextBoxNum
    Me.lbOg_referente = New NTSInformatica.NTSLabel
    Me.edOg_referente = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_referente = New NTSInformatica.NTSLabel
    Me.edOg_dtfine = New NTSInformatica.NTSTextBoxData
    Me.lbOg_codstco = New NTSInformatica.NTSLabel
    Me.lbOg_dtfine = New NTSInformatica.NTSLabel
    Me.lbXx_codstco = New NTSInformatica.NTSLabel
    Me.fmResidenza = New NTSInformatica.NTSGroupBox
    Me.lbOg_indir = New NTSInformatica.NTSLabel
    Me.edOg_indir = New NTSInformatica.NTSTextBoxStr
    Me.lbOg_cap = New NTSInformatica.NTSLabel
    Me.edOg_cap = New NTSInformatica.NTSTextBoxStr
    Me.lbOg_citta = New NTSInformatica.NTSLabel
    Me.edOg_citta = New NTSInformatica.NTSTextBoxStr
    Me.lbOg_prov = New NTSInformatica.NTSLabel
    Me.edOg_prov = New NTSInformatica.NTSTextBoxStr
    Me.lbOg_stato = New NTSInformatica.NTSLabel
    Me.lbXx_stato = New NTSInformatica.NTSLabel
    Me.edOg_stato = New NTSInformatica.NTSTextBoxStr
    Me.edOg_rep = New NTSInformatica.NTSTextBoxStr
    Me.lbOg_rep = New NTSInformatica.NTSLabel
    Me.edOg_divis = New NTSInformatica.NTSTextBoxStr
    Me.lbOg_divis = New NTSInformatica.NTSLabel
    Me.edOg_sede = New NTSInformatica.NTSTextBoxStr
    Me.lbOg_sede = New NTSInformatica.NTSLabel
    Me.edOg_datnasc = New NTSInformatica.NTSTextBoxData
    Me.cbOg_sesso = New NTSInformatica.NTSComboBox
    Me.fmRecapiti = New NTSInformatica.NTSGroupBox
    Me.cmdOg_skypeuser = New NTSInformatica.NTSButton
    Me.edOg_skypeuser = New NTSInformatica.NTSTextBoxStr
    Me.lbOg_skypeuser = New NTSInformatica.NTSLabel
    Me.cbOg_usaem = New NTSInformatica.NTSComboBox
    Me.lbOg_usaem = New NTSInformatica.NTSLabel
    Me.cmdOg_emailpers = New NTSInformatica.NTSButton
    Me.cmdOg_email = New NTSInformatica.NTSButton
    Me.cmdOg_cellpers = New NTSInformatica.NTSButton
    Me.cmdOg_cell = New NTSInformatica.NTSButton
    Me.cmdOg_telefpers = New NTSInformatica.NTSButton
    Me.cmdOg_telef = New NTSInformatica.NTSButton
    Me.edOg_faxpers = New NTSInformatica.NTSTextBoxStr
    Me.edOg_fax = New NTSInformatica.NTSTextBoxStr
    Me.edOg_emailpers = New NTSInformatica.NTSTextBoxStr
    Me.edOg_email = New NTSInformatica.NTSTextBoxStr
    Me.edOg_cellpers = New NTSInformatica.NTSTextBoxStr
    Me.edOg_cell = New NTSInformatica.NTSTextBoxStr
    Me.edOg_telefpers = New NTSInformatica.NTSTextBoxStr
    Me.edOg_telefint = New NTSInformatica.NTSTextBoxStr
    Me.edOg_telef = New NTSInformatica.NTSTextBoxStr
    Me.lbFax = New NTSInformatica.NTSLabel
    Me.lbEmail = New NTSInformatica.NTSLabel
    Me.lbCellulare = New NTSInformatica.NTSLabel
    Me.lbAziendale = New NTSInformatica.NTSLabel
    Me.lbPersonale = New NTSInformatica.NTSLabel
    Me.lbOg_telefint = New NTSInformatica.NTSLabel
    Me.lbTelefono = New NTSInformatica.NTSLabel
    Me.edOg_mansioni = New NTSInformatica.NTSMemoBox
    Me.edOg_descont2 = New NTSInformatica.NTSTextBoxStr
    Me.edOg_descont = New NTSInformatica.NTSTextBoxStr
    Me.edOg_codruaz = New NTSInformatica.NTSTextBoxStr
    Me.edOg_titolo = New NTSInformatica.NTSTextBoxStr
    Me.lbOg_datnasc = New NTSInformatica.NTSLabel
    Me.lbOg_sesso = New NTSInformatica.NTSLabel
    Me.lbOg_descont2 = New NTSInformatica.NTSLabel
    Me.lbOg_descont = New NTSInformatica.NTSLabel
    Me.lbXx_codruaz = New NTSInformatica.NTSLabel
    Me.lbOg_mansioni = New NTSInformatica.NTSLabel
    Me.lbOg_codruaz = New NTSInformatica.NTSLabel
    Me.lbOg_titolo = New NTSInformatica.NTSLabel
    Me.fmSocialNetwork = New NTSInformatica.NTSGroupBox
    Me.cmdOg_twitteruser = New NTSInformatica.NTSButton
    Me.edOg_twitteruser = New NTSInformatica.NTSTextBoxStr
    Me.cmdOg_fbuser = New NTSInformatica.NTSButton
    Me.edOg_fbuser = New NTSInformatica.NTSTextBoxStr
    Me.lbOg_twitteruser = New NTSInformatica.NTSLabel
    Me.lbOg_fbuser = New NTSInformatica.NTSLabel
    Me.fmContatti = New NTSInformatica.NTSGroupBox
    Me.lbOg_progr = New NTSInformatica.NTSLabel
    Me.lbID = New NTSInformatica.NTSLabel
    Me.cmdCollegaAContatto = New NTSInformatica.NTSButton
    Me.cmdCreaNuovoContatto = New NTSInformatica.NTSButton
    Me.cmdProssimo = New NTSInformatica.NTSButton
    Me.cmdCollega = New NTSInformatica.NTSButton
    Me.lbProponiContatto = New NTSInformatica.NTSLabel
    Me.lbOg_codcont = New NTSInformatica.NTSLabel
    Me.lbOg_codcope = New NTSInformatica.NTSLabel
    Me.edOg_codcont = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codcont = New NTSInformatica.NTSLabel
    Me.edOg_codcope = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codcope = New NTSInformatica.NTSLabel
    Me.lbOg_contatto = New NTSInformatica.NTSLabel
    Me.lbOg_codcage = New NTSInformatica.NTSLabel
    Me.edOg_contatto = New NTSInformatica.NTSTextBoxNum
    Me.edOg_codcage = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codcage = New NTSInformatica.NTSLabel
    Me.edOg_coperat = New NTSInformatica.NTSTextBoxStr
    Me.edOg_coddest = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_coddest = New NTSInformatica.NTSLabel
    Me.lbOg_coperat = New NTSInformatica.NTSLabel
    Me.lbOg_coddest = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grOrga, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvOrga, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAll, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAll.SuspendLayout()
    CType(Me.pnPersone, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPersone.SuspendLayout()
    CType(Me.pnRicerca, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRicerca.SuspendLayout()
    CType(Me.edRicerca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDati, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDati.SuspendLayout()
    CType(Me.pnDatiPersona, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDatiPersona.SuspendLayout()
    CType(Me.fmStato, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmStato.SuspendLayout()
    CType(Me.ckOg_old.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_dtiniz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_codstco.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_referente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_dtfine.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmResidenza, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmResidenza.SuspendLayout()
    CType(Me.edOg_indir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_cap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_citta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_prov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_stato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_rep.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_divis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_sede.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_datnasc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbOg_sesso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmRecapiti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmRecapiti.SuspendLayout()
    CType(Me.edOg_skypeuser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbOg_usaem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_faxpers.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_fax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_emailpers.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_email.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_cellpers.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_cell.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_telefpers.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_telefint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_telef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_mansioni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_descont2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_descont.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_codruaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_titolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmSocialNetwork, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSocialNetwork.SuspendLayout()
    CType(Me.edOg_twitteruser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_fbuser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmContatti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmContatti.SuspendLayout()
    CType(Me.edOg_codcont.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_codcope.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_contatto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_codcage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_coperat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edOg_coddest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbRuoli, Me.tlbEmail, Me.tlbSkype, Me.tlbGeneraGuest, Me.tlbInterna, Me.tlbClienti, Me.tlbFornitori, Me.tlbLeads, Me.tlbSalvaNuovo})
    Me.NtsBarManager1.MaxItemId = 26
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbSalvaNuovo, False), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbSkype, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEmail, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGeneraGuest), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRuoli), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbInterna, True), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbClienti, False), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbFornitori, False), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.tlbLeads, False), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbSalvaNuovo
    '
    Me.tlbSalvaNuovo.Caption = "Salva + Nuovo"
    Me.tlbSalvaNuovo.Glyph = CType(resources.GetObject("tlbSalvaNuovo.Glyph"), System.Drawing.Image)
    Me.tlbSalvaNuovo.GlyphPath = ""
    Me.tlbSalvaNuovo.Id = 25
    Me.tlbSalvaNuovo.Name = "tlbSalvaNuovo"
    Me.tlbSalvaNuovo.Visible = False
    '
    'tlbNuovo
    '
    Me.tlbNuovo.Caption = "Nuovo"
    Me.tlbNuovo.Glyph = CType(resources.GetObject("tlbNuovo.Glyph"), System.Drawing.Image)
    Me.tlbNuovo.GlyphPath = ""
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 1
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 3
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
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
    'tlbSkype
    '
    Me.tlbSkype.Caption = "Chiama con Skype"
    Me.tlbSkype.Glyph = CType(resources.GetObject("tlbSkype.Glyph"), System.Drawing.Image)
    Me.tlbSkype.GlyphPath = ""
    Me.tlbSkype.Id = 19
    Me.tlbSkype.Name = "tlbSkype"
    Me.tlbSkype.Visible = False
    '
    'tlbEmail
    '
    Me.tlbEmail.Caption = "Configura Account di Posta"
    Me.tlbEmail.Glyph = CType(resources.GetObject("tlbEmail.Glyph"), System.Drawing.Image)
    Me.tlbEmail.GlyphPath = ""
    Me.tlbEmail.Id = 18
    Me.tlbEmail.Name = "tlbEmail"
    Me.tlbEmail.Visible = True
    '
    'tlbGeneraGuest
    '
    Me.tlbGeneraGuest.Caption = "Genera utente guest"
    Me.tlbGeneraGuest.Glyph = CType(resources.GetObject("tlbGeneraGuest.Glyph"), System.Drawing.Image)
    Me.tlbGeneraGuest.GlyphPath = ""
    Me.tlbGeneraGuest.Id = 20
    Me.tlbGeneraGuest.Name = "tlbGeneraGuest"
    Me.tlbGeneraGuest.Visible = True
    '
    'tlbRuoli
    '
    Me.tlbRuoli.Caption = "Ruoli collegati"
    Me.tlbRuoli.Glyph = CType(resources.GetObject("tlbRuoli.Glyph"), System.Drawing.Image)
    Me.tlbRuoli.GlyphPath = ""
    Me.tlbRuoli.Id = 17
    Me.tlbRuoli.Name = "tlbRuoli"
    Me.tlbRuoli.Visible = True
    '
    'tlbInterna
    '
    Me.tlbInterna.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbInterna.Caption = "Organizzazione Interna"
    Me.tlbInterna.Glyph = CType(resources.GetObject("tlbInterna.Glyph"), System.Drawing.Image)
    Me.tlbInterna.GlyphPath = ""
    Me.tlbInterna.Id = 21
    Me.tlbInterna.Name = "tlbInterna"
    Me.tlbInterna.Visible = False
    '
    'tlbClienti
    '
    Me.tlbClienti.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbClienti.Caption = "Clienti"
    Me.tlbClienti.Down = True
    Me.tlbClienti.Glyph = CType(resources.GetObject("tlbClienti.Glyph"), System.Drawing.Image)
    Me.tlbClienti.GlyphPath = ""
    Me.tlbClienti.Id = 22
    Me.tlbClienti.Name = "tlbClienti"
    Me.tlbClienti.Visible = False
    '
    'tlbFornitori
    '
    Me.tlbFornitori.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbFornitori.Caption = "Fornitori"
    Me.tlbFornitori.Down = True
    Me.tlbFornitori.Glyph = CType(resources.GetObject("tlbFornitori.Glyph"), System.Drawing.Image)
    Me.tlbFornitori.GlyphPath = ""
    Me.tlbFornitori.Id = 23
    Me.tlbFornitori.Name = "tlbFornitori"
    Me.tlbFornitori.Visible = False
    '
    'tlbLeads
    '
    Me.tlbLeads.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check
    Me.tlbLeads.Caption = "Leads"
    Me.tlbLeads.Down = True
    Me.tlbLeads.Glyph = CType(resources.GetObject("tlbLeads.Glyph"), System.Drawing.Image)
    Me.tlbLeads.GlyphPath = ""
    Me.tlbLeads.Id = 24
    Me.tlbLeads.Name = "tlbLeads"
    Me.tlbLeads.Visible = False
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
    'grOrga
    '
    Me.grOrga.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grOrga.EmbeddedNavigator.Name = ""
    Me.grOrga.Location = New System.Drawing.Point(0, 32)
    Me.grOrga.MainView = Me.grvOrga
    Me.grOrga.Name = "grOrga"
    Me.grOrga.Size = New System.Drawing.Size(162, 484)
    Me.grOrga.TabIndex = 5
    Me.grOrga.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvOrga})
    '
    'grvOrga
    '
    Me.grvOrga.ActiveFilterEnabled = False
    Me.grvOrga.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.og_progr, Me.og_coddest, Me.xx_coddest, Me.og_sede, Me.og_divis, Me.og_rep, Me.og_codruaz, Me.xx_codruaz, Me.og_codcont, Me.xx_codcont, Me.og_telef, Me.og_fax, Me.og_email, Me.og_dtiniz, Me.og_dtfine, Me.og_cell, Me.og_descont, Me.og_descont2, Me.og_titolo, Me.og_indir, Me.og_cap, Me.og_citta, Me.og_prov, Me.og_stato, Me.xx_stato, Me.og_datnasc, Me.og_sesso, Me.og_codlead, Me.og_coperat, Me.og_mansioni, Me.og_codcope, Me.xx_codcope, Me.og_codcage, Me.xx_codcage, Me.og_usaem, Me.og_old, Me.og_telefint, Me.xx_descrizione})
    Me.grvOrga.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvOrga.Enabled = False
    Me.grvOrga.GridControl = Me.grOrga
    Me.grvOrga.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvOrga.MinRowHeight = 14
    Me.grvOrga.Name = "grvOrga"
    Me.grvOrga.NTSAllowDelete = True
    Me.grvOrga.NTSAllowInsert = True
    Me.grvOrga.NTSAllowUpdate = True
    Me.grvOrga.NTSMenuContext = Nothing
    Me.grvOrga.OptionsBehavior.Editable = False
    Me.grvOrga.OptionsCustomization.AllowRowSizing = True
    Me.grvOrga.OptionsFilter.AllowFilterEditor = False
    Me.grvOrga.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvOrga.OptionsNavigation.UseTabKey = False
    Me.grvOrga.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvOrga.OptionsView.ColumnAutoWidth = False
    Me.grvOrga.OptionsView.EnableAppearanceEvenRow = True
    Me.grvOrga.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvOrga.OptionsView.ShowColumnHeaders = False
    Me.grvOrga.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvOrga.OptionsView.ShowGroupPanel = False
    Me.grvOrga.OptionsView.ShowIndicator = False
    Me.grvOrga.RowHeight = 16
    '
    'og_progr
    '
    Me.og_progr.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.og_progr.AppearanceCell.Options.UseBackColor = True
    Me.og_progr.AppearanceCell.Options.UseTextOptions = True
    Me.og_progr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_progr.Caption = "Codice"
    Me.og_progr.Enabled = False
    Me.og_progr.FieldName = "og_progr"
    Me.og_progr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_progr.Name = "og_progr"
    Me.og_progr.NTSRepositoryComboBox = Nothing
    Me.og_progr.NTSRepositoryItemCheck = Nothing
    Me.og_progr.NTSRepositoryItemMemo = Nothing
    Me.og_progr.NTSRepositoryItemText = Nothing
    Me.og_progr.OptionsColumn.AllowEdit = False
    Me.og_progr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_progr.OptionsColumn.ReadOnly = True
    Me.og_progr.OptionsFilter.AllowFilter = False
    Me.og_progr.Width = 70
    '
    'og_coddest
    '
    Me.og_coddest.AppearanceCell.Options.UseBackColor = True
    Me.og_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.og_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_coddest.Caption = "Cod. destinaz."
    Me.og_coddest.Enabled = True
    Me.og_coddest.FieldName = "og_coddest"
    Me.og_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_coddest.Name = "og_coddest"
    Me.og_coddest.NTSRepositoryComboBox = Nothing
    Me.og_coddest.NTSRepositoryItemCheck = Nothing
    Me.og_coddest.NTSRepositoryItemMemo = Nothing
    Me.og_coddest.NTSRepositoryItemText = Nothing
    Me.og_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_coddest.OptionsFilter.AllowFilter = False
    Me.og_coddest.Width = 70
    '
    'xx_coddest
    '
    Me.xx_coddest.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.xx_coddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddest.Caption = "Descr. destin."
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
    Me.xx_coddest.Width = 70
    '
    'og_sede
    '
    Me.og_sede.AppearanceCell.Options.UseBackColor = True
    Me.og_sede.AppearanceCell.Options.UseTextOptions = True
    Me.og_sede.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_sede.Caption = "Sede"
    Me.og_sede.Enabled = True
    Me.og_sede.FieldName = "og_sede"
    Me.og_sede.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_sede.Name = "og_sede"
    Me.og_sede.NTSRepositoryComboBox = Nothing
    Me.og_sede.NTSRepositoryItemCheck = Nothing
    Me.og_sede.NTSRepositoryItemMemo = Nothing
    Me.og_sede.NTSRepositoryItemText = Nothing
    Me.og_sede.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_sede.OptionsFilter.AllowFilter = False
    Me.og_sede.Width = 70
    '
    'og_divis
    '
    Me.og_divis.AppearanceCell.Options.UseBackColor = True
    Me.og_divis.AppearanceCell.Options.UseTextOptions = True
    Me.og_divis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_divis.Caption = "Divisione"
    Me.og_divis.Enabled = True
    Me.og_divis.FieldName = "og_divis"
    Me.og_divis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_divis.Name = "og_divis"
    Me.og_divis.NTSRepositoryComboBox = Nothing
    Me.og_divis.NTSRepositoryItemCheck = Nothing
    Me.og_divis.NTSRepositoryItemMemo = Nothing
    Me.og_divis.NTSRepositoryItemText = Nothing
    Me.og_divis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_divis.OptionsFilter.AllowFilter = False
    Me.og_divis.Width = 70
    '
    'og_rep
    '
    Me.og_rep.AppearanceCell.Options.UseBackColor = True
    Me.og_rep.AppearanceCell.Options.UseTextOptions = True
    Me.og_rep.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_rep.Caption = "Reparto"
    Me.og_rep.Enabled = True
    Me.og_rep.FieldName = "og_rep"
    Me.og_rep.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_rep.Name = "og_rep"
    Me.og_rep.NTSRepositoryComboBox = Nothing
    Me.og_rep.NTSRepositoryItemCheck = Nothing
    Me.og_rep.NTSRepositoryItemMemo = Nothing
    Me.og_rep.NTSRepositoryItemText = Nothing
    Me.og_rep.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_rep.OptionsFilter.AllowFilter = False
    Me.og_rep.Width = 70
    '
    'og_codruaz
    '
    Me.og_codruaz.AppearanceCell.Options.UseBackColor = True
    Me.og_codruaz.AppearanceCell.Options.UseTextOptions = True
    Me.og_codruaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_codruaz.Caption = "Cod. ruolo az. principale"
    Me.og_codruaz.Enabled = True
    Me.og_codruaz.FieldName = "og_codruaz"
    Me.og_codruaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_codruaz.Name = "og_codruaz"
    Me.og_codruaz.NTSRepositoryComboBox = Nothing
    Me.og_codruaz.NTSRepositoryItemCheck = Nothing
    Me.og_codruaz.NTSRepositoryItemMemo = Nothing
    Me.og_codruaz.NTSRepositoryItemText = Nothing
    Me.og_codruaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_codruaz.OptionsFilter.AllowFilter = False
    Me.og_codruaz.Width = 70
    '
    'xx_codruaz
    '
    Me.xx_codruaz.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.xx_codruaz.AppearanceCell.Options.UseBackColor = True
    Me.xx_codruaz.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codruaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codruaz.Caption = "Descr. ruolo"
    Me.xx_codruaz.Enabled = False
    Me.xx_codruaz.FieldName = "xx_codruaz"
    Me.xx_codruaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codruaz.Name = "xx_codruaz"
    Me.xx_codruaz.NTSRepositoryComboBox = Nothing
    Me.xx_codruaz.NTSRepositoryItemCheck = Nothing
    Me.xx_codruaz.NTSRepositoryItemMemo = Nothing
    Me.xx_codruaz.NTSRepositoryItemText = Nothing
    Me.xx_codruaz.OptionsColumn.AllowEdit = False
    Me.xx_codruaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codruaz.OptionsColumn.ReadOnly = True
    Me.xx_codruaz.OptionsFilter.AllowFilter = False
    Me.xx_codruaz.Width = 70
    '
    'og_codcont
    '
    Me.og_codcont.AppearanceCell.Options.UseBackColor = True
    Me.og_codcont.AppearanceCell.Options.UseTextOptions = True
    Me.og_codcont.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_codcont.Caption = "Cod. risorsa / contatto"
    Me.og_codcont.Enabled = True
    Me.og_codcont.FieldName = "og_codcont"
    Me.og_codcont.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_codcont.Name = "og_codcont"
    Me.og_codcont.NTSRepositoryComboBox = Nothing
    Me.og_codcont.NTSRepositoryItemCheck = Nothing
    Me.og_codcont.NTSRepositoryItemMemo = Nothing
    Me.og_codcont.NTSRepositoryItemText = Nothing
    Me.og_codcont.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_codcont.OptionsFilter.AllowFilter = False
    Me.og_codcont.Width = 70
    '
    'xx_codcont
    '
    Me.xx_codcont.AppearanceCell.Options.UseBackColor = True
    Me.xx_codcont.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codcont.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codcont.Caption = "Descr. risorsa / contatto"
    Me.xx_codcont.Enabled = False
    Me.xx_codcont.FieldName = "xx_codcont"
    Me.xx_codcont.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codcont.Name = "xx_codcont"
    Me.xx_codcont.NTSRepositoryComboBox = Nothing
    Me.xx_codcont.NTSRepositoryItemCheck = Nothing
    Me.xx_codcont.NTSRepositoryItemMemo = Nothing
    Me.xx_codcont.NTSRepositoryItemText = Nothing
    Me.xx_codcont.OptionsColumn.AllowEdit = False
    Me.xx_codcont.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codcont.OptionsColumn.ReadOnly = True
    Me.xx_codcont.OptionsFilter.AllowFilter = False
    Me.xx_codcont.Width = 70
    '
    'og_telef
    '
    Me.og_telef.AppearanceCell.Options.UseBackColor = True
    Me.og_telef.AppearanceCell.Options.UseTextOptions = True
    Me.og_telef.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_telef.Caption = "Tel. az."
    Me.og_telef.Enabled = True
    Me.og_telef.FieldName = "og_telef"
    Me.og_telef.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_telef.Name = "og_telef"
    Me.og_telef.NTSRepositoryComboBox = Nothing
    Me.og_telef.NTSRepositoryItemCheck = Nothing
    Me.og_telef.NTSRepositoryItemMemo = Nothing
    Me.og_telef.NTSRepositoryItemText = Nothing
    Me.og_telef.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_telef.OptionsFilter.AllowFilter = False
    Me.og_telef.Width = 70
    '
    'og_fax
    '
    Me.og_fax.AppearanceCell.Options.UseBackColor = True
    Me.og_fax.AppearanceCell.Options.UseTextOptions = True
    Me.og_fax.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_fax.Caption = "Fax az."
    Me.og_fax.Enabled = True
    Me.og_fax.FieldName = "og_fax"
    Me.og_fax.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_fax.Name = "og_fax"
    Me.og_fax.NTSRepositoryComboBox = Nothing
    Me.og_fax.NTSRepositoryItemCheck = Nothing
    Me.og_fax.NTSRepositoryItemMemo = Nothing
    Me.og_fax.NTSRepositoryItemText = Nothing
    Me.og_fax.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_fax.OptionsFilter.AllowFilter = False
    Me.og_fax.Width = 70
    '
    'og_email
    '
    Me.og_email.AppearanceCell.Options.UseBackColor = True
    Me.og_email.AppearanceCell.Options.UseTextOptions = True
    Me.og_email.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_email.Caption = "e-mail az."
    Me.og_email.Enabled = True
    Me.og_email.FieldName = "og_email"
    Me.og_email.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_email.Name = "og_email"
    Me.og_email.NTSRepositoryComboBox = Nothing
    Me.og_email.NTSRepositoryItemCheck = Nothing
    Me.og_email.NTSRepositoryItemMemo = Nothing
    Me.og_email.NTSRepositoryItemText = Nothing
    Me.og_email.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_email.OptionsFilter.AllowFilter = False
    Me.og_email.Width = 70
    '
    'og_dtiniz
    '
    Me.og_dtiniz.AppearanceCell.Options.UseBackColor = True
    Me.og_dtiniz.AppearanceCell.Options.UseTextOptions = True
    Me.og_dtiniz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_dtiniz.Caption = "Data inizio val."
    Me.og_dtiniz.Enabled = True
    Me.og_dtiniz.FieldName = "og_dtiniz"
    Me.og_dtiniz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_dtiniz.Name = "og_dtiniz"
    Me.og_dtiniz.NTSRepositoryComboBox = Nothing
    Me.og_dtiniz.NTSRepositoryItemCheck = Nothing
    Me.og_dtiniz.NTSRepositoryItemMemo = Nothing
    Me.og_dtiniz.NTSRepositoryItemText = Nothing
    Me.og_dtiniz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_dtiniz.OptionsFilter.AllowFilter = False
    Me.og_dtiniz.Width = 70
    '
    'og_dtfine
    '
    Me.og_dtfine.AppearanceCell.Options.UseBackColor = True
    Me.og_dtfine.AppearanceCell.Options.UseTextOptions = True
    Me.og_dtfine.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_dtfine.Caption = "Data fine val."
    Me.og_dtfine.Enabled = True
    Me.og_dtfine.FieldName = "og_dtfine"
    Me.og_dtfine.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_dtfine.Name = "og_dtfine"
    Me.og_dtfine.NTSRepositoryComboBox = Nothing
    Me.og_dtfine.NTSRepositoryItemCheck = Nothing
    Me.og_dtfine.NTSRepositoryItemMemo = Nothing
    Me.og_dtfine.NTSRepositoryItemText = Nothing
    Me.og_dtfine.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_dtfine.OptionsFilter.AllowFilter = False
    Me.og_dtfine.Width = 70
    '
    'og_cell
    '
    Me.og_cell.AppearanceCell.Options.UseBackColor = True
    Me.og_cell.AppearanceCell.Options.UseTextOptions = True
    Me.og_cell.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_cell.Caption = "Tel. cellulare"
    Me.og_cell.Enabled = True
    Me.og_cell.FieldName = "og_cell"
    Me.og_cell.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_cell.Name = "og_cell"
    Me.og_cell.NTSRepositoryComboBox = Nothing
    Me.og_cell.NTSRepositoryItemCheck = Nothing
    Me.og_cell.NTSRepositoryItemMemo = Nothing
    Me.og_cell.NTSRepositoryItemText = Nothing
    Me.og_cell.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_cell.OptionsFilter.AllowFilter = False
    Me.og_cell.Width = 70
    '
    'og_descont
    '
    Me.og_descont.AppearanceCell.Options.UseBackColor = True
    Me.og_descont.AppearanceCell.Options.UseTextOptions = True
    Me.og_descont.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_descont.Caption = "Cognome / Denominaz,"
    Me.og_descont.Enabled = True
    Me.og_descont.FieldName = "og_descont"
    Me.og_descont.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_descont.Name = "og_descont"
    Me.og_descont.NTSRepositoryComboBox = Nothing
    Me.og_descont.NTSRepositoryItemCheck = Nothing
    Me.og_descont.NTSRepositoryItemMemo = Nothing
    Me.og_descont.NTSRepositoryItemText = Nothing
    Me.og_descont.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_descont.OptionsFilter.AllowFilter = False
    Me.og_descont.Width = 70
    '
    'og_descont2
    '
    Me.og_descont2.AppearanceCell.Options.UseBackColor = True
    Me.og_descont2.AppearanceCell.Options.UseTextOptions = True
    Me.og_descont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_descont2.Caption = "Nome"
    Me.og_descont2.Enabled = True
    Me.og_descont2.FieldName = "og_descont2"
    Me.og_descont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_descont2.Name = "og_descont2"
    Me.og_descont2.NTSRepositoryComboBox = Nothing
    Me.og_descont2.NTSRepositoryItemCheck = Nothing
    Me.og_descont2.NTSRepositoryItemMemo = Nothing
    Me.og_descont2.NTSRepositoryItemText = Nothing
    Me.og_descont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_descont2.OptionsFilter.AllowFilter = False
    Me.og_descont2.Width = 70
    '
    'og_titolo
    '
    Me.og_titolo.AppearanceCell.Options.UseBackColor = True
    Me.og_titolo.AppearanceCell.Options.UseTextOptions = True
    Me.og_titolo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_titolo.Caption = "Titolo"
    Me.og_titolo.Enabled = True
    Me.og_titolo.FieldName = "og_titolo"
    Me.og_titolo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_titolo.Name = "og_titolo"
    Me.og_titolo.NTSRepositoryComboBox = Nothing
    Me.og_titolo.NTSRepositoryItemCheck = Nothing
    Me.og_titolo.NTSRepositoryItemMemo = Nothing
    Me.og_titolo.NTSRepositoryItemText = Nothing
    Me.og_titolo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_titolo.OptionsFilter.AllowFilter = False
    Me.og_titolo.Width = 70
    '
    'og_indir
    '
    Me.og_indir.AppearanceCell.Options.UseBackColor = True
    Me.og_indir.AppearanceCell.Options.UseTextOptions = True
    Me.og_indir.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_indir.Caption = "Indir. abitaz."
    Me.og_indir.Enabled = True
    Me.og_indir.FieldName = "og_indir"
    Me.og_indir.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_indir.Name = "og_indir"
    Me.og_indir.NTSRepositoryComboBox = Nothing
    Me.og_indir.NTSRepositoryItemCheck = Nothing
    Me.og_indir.NTSRepositoryItemMemo = Nothing
    Me.og_indir.NTSRepositoryItemText = Nothing
    Me.og_indir.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_indir.OptionsFilter.AllowFilter = False
    Me.og_indir.Width = 70
    '
    'og_cap
    '
    Me.og_cap.AppearanceCell.Options.UseBackColor = True
    Me.og_cap.AppearanceCell.Options.UseTextOptions = True
    Me.og_cap.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_cap.Caption = "Cap abitaz."
    Me.og_cap.Enabled = True
    Me.og_cap.FieldName = "og_cap"
    Me.og_cap.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_cap.Name = "og_cap"
    Me.og_cap.NTSRepositoryComboBox = Nothing
    Me.og_cap.NTSRepositoryItemCheck = Nothing
    Me.og_cap.NTSRepositoryItemMemo = Nothing
    Me.og_cap.NTSRepositoryItemText = Nothing
    Me.og_cap.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_cap.OptionsFilter.AllowFilter = False
    Me.og_cap.Width = 70
    '
    'og_citta
    '
    Me.og_citta.AppearanceCell.Options.UseBackColor = True
    Me.og_citta.AppearanceCell.Options.UseTextOptions = True
    Me.og_citta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_citta.Caption = "Città abitaz."
    Me.og_citta.Enabled = True
    Me.og_citta.FieldName = "og_citta"
    Me.og_citta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_citta.Name = "og_citta"
    Me.og_citta.NTSRepositoryComboBox = Nothing
    Me.og_citta.NTSRepositoryItemCheck = Nothing
    Me.og_citta.NTSRepositoryItemMemo = Nothing
    Me.og_citta.NTSRepositoryItemText = Nothing
    Me.og_citta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_citta.OptionsFilter.AllowFilter = False
    Me.og_citta.Width = 70
    '
    'og_prov
    '
    Me.og_prov.AppearanceCell.Options.UseBackColor = True
    Me.og_prov.AppearanceCell.Options.UseTextOptions = True
    Me.og_prov.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_prov.Caption = "Prov. abitaz."
    Me.og_prov.Enabled = True
    Me.og_prov.FieldName = "og_prov"
    Me.og_prov.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_prov.Name = "og_prov"
    Me.og_prov.NTSRepositoryComboBox = Nothing
    Me.og_prov.NTSRepositoryItemCheck = Nothing
    Me.og_prov.NTSRepositoryItemMemo = Nothing
    Me.og_prov.NTSRepositoryItemText = Nothing
    Me.og_prov.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_prov.OptionsFilter.AllowFilter = False
    Me.og_prov.Width = 70
    '
    'og_stato
    '
    Me.og_stato.AppearanceCell.Options.UseBackColor = True
    Me.og_stato.AppearanceCell.Options.UseTextOptions = True
    Me.og_stato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_stato.Caption = "Stato abitaz."
    Me.og_stato.Enabled = True
    Me.og_stato.FieldName = "og_stato"
    Me.og_stato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_stato.Name = "og_stato"
    Me.og_stato.NTSRepositoryComboBox = Nothing
    Me.og_stato.NTSRepositoryItemCheck = Nothing
    Me.og_stato.NTSRepositoryItemMemo = Nothing
    Me.og_stato.NTSRepositoryItemText = Nothing
    Me.og_stato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_stato.OptionsFilter.AllowFilter = False
    Me.og_stato.Width = 70
    '
    'xx_stato
    '
    Me.xx_stato.AppearanceCell.Options.UseBackColor = True
    Me.xx_stato.AppearanceCell.Options.UseTextOptions = True
    Me.xx_stato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_stato.Caption = "Descr. stato"
    Me.xx_stato.Enabled = False
    Me.xx_stato.FieldName = "xx_stato"
    Me.xx_stato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_stato.Name = "xx_stato"
    Me.xx_stato.NTSRepositoryComboBox = Nothing
    Me.xx_stato.NTSRepositoryItemCheck = Nothing
    Me.xx_stato.NTSRepositoryItemMemo = Nothing
    Me.xx_stato.NTSRepositoryItemText = Nothing
    Me.xx_stato.OptionsColumn.AllowEdit = False
    Me.xx_stato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_stato.OptionsColumn.ReadOnly = True
    Me.xx_stato.OptionsFilter.AllowFilter = False
    Me.xx_stato.Width = 70
    '
    'og_datnasc
    '
    Me.og_datnasc.AppearanceCell.Options.UseBackColor = True
    Me.og_datnasc.AppearanceCell.Options.UseTextOptions = True
    Me.og_datnasc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_datnasc.Caption = "Data nascita"
    Me.og_datnasc.Enabled = True
    Me.og_datnasc.FieldName = "og_datnasc"
    Me.og_datnasc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_datnasc.Name = "og_datnasc"
    Me.og_datnasc.NTSRepositoryComboBox = Nothing
    Me.og_datnasc.NTSRepositoryItemCheck = Nothing
    Me.og_datnasc.NTSRepositoryItemMemo = Nothing
    Me.og_datnasc.NTSRepositoryItemText = Nothing
    Me.og_datnasc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_datnasc.OptionsFilter.AllowFilter = False
    Me.og_datnasc.Width = 70
    '
    'og_sesso
    '
    Me.og_sesso.AppearanceCell.Options.UseBackColor = True
    Me.og_sesso.AppearanceCell.Options.UseTextOptions = True
    Me.og_sesso.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_sesso.Caption = "Sesso"
    Me.og_sesso.Enabled = True
    Me.og_sesso.FieldName = "og_sesso"
    Me.og_sesso.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_sesso.Name = "og_sesso"
    Me.og_sesso.NTSRepositoryComboBox = Nothing
    Me.og_sesso.NTSRepositoryItemCheck = Nothing
    Me.og_sesso.NTSRepositoryItemMemo = Nothing
    Me.og_sesso.NTSRepositoryItemText = Nothing
    Me.og_sesso.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_sesso.OptionsFilter.AllowFilter = False
    Me.og_sesso.Width = 70
    '
    'og_codlead
    '
    Me.og_codlead.AppearanceCell.Options.UseBackColor = True
    Me.og_codlead.AppearanceCell.Options.UseTextOptions = True
    Me.og_codlead.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_codlead.Caption = "Cod. lead"
    Me.og_codlead.Enabled = True
    Me.og_codlead.FieldName = "og_codlead"
    Me.og_codlead.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_codlead.Name = "og_codlead"
    Me.og_codlead.NTSRepositoryComboBox = Nothing
    Me.og_codlead.NTSRepositoryItemCheck = Nothing
    Me.og_codlead.NTSRepositoryItemMemo = Nothing
    Me.og_codlead.NTSRepositoryItemText = Nothing
    Me.og_codlead.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_codlead.OptionsFilter.AllowFilter = False
    Me.og_codlead.Width = 70
    '
    'og_coperat
    '
    Me.og_coperat.AppearanceCell.Options.UseBackColor = True
    Me.og_coperat.AppearanceCell.Options.UseTextOptions = True
    Me.og_coperat.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_coperat.Caption = "Operatore Bus"
    Me.og_coperat.Enabled = True
    Me.og_coperat.FieldName = "og_coperat"
    Me.og_coperat.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_coperat.Name = "og_coperat"
    Me.og_coperat.NTSRepositoryComboBox = Nothing
    Me.og_coperat.NTSRepositoryItemCheck = Nothing
    Me.og_coperat.NTSRepositoryItemMemo = Nothing
    Me.og_coperat.NTSRepositoryItemText = Nothing
    Me.og_coperat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_coperat.OptionsFilter.AllowFilter = False
    Me.og_coperat.Width = 70
    '
    'og_mansioni
    '
    Me.og_mansioni.AppearanceCell.Options.UseBackColor = True
    Me.og_mansioni.AppearanceCell.Options.UseTextOptions = True
    Me.og_mansioni.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_mansioni.Caption = "Mansioni"
    Me.og_mansioni.Enabled = True
    Me.og_mansioni.FieldName = "og_mansioni"
    Me.og_mansioni.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_mansioni.Name = "og_mansioni"
    Me.og_mansioni.NTSRepositoryComboBox = Nothing
    Me.og_mansioni.NTSRepositoryItemCheck = Nothing
    Me.og_mansioni.NTSRepositoryItemMemo = Nothing
    Me.og_mansioni.NTSRepositoryItemText = Nothing
    Me.og_mansioni.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_mansioni.OptionsFilter.AllowFilter = False
    Me.og_mansioni.Width = 70
    '
    'og_codcope
    '
    Me.og_codcope.AppearanceCell.Options.UseBackColor = True
    Me.og_codcope.AppearanceCell.Options.UseTextOptions = True
    Me.og_codcope.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_codcope.Caption = "Cod. operaio"
    Me.og_codcope.Enabled = True
    Me.og_codcope.FieldName = "og_codcope"
    Me.og_codcope.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_codcope.Name = "og_codcope"
    Me.og_codcope.NTSRepositoryComboBox = Nothing
    Me.og_codcope.NTSRepositoryItemCheck = Nothing
    Me.og_codcope.NTSRepositoryItemMemo = Nothing
    Me.og_codcope.NTSRepositoryItemText = Nothing
    Me.og_codcope.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_codcope.OptionsFilter.AllowFilter = False
    Me.og_codcope.Width = 70
    '
    'xx_codcope
    '
    Me.xx_codcope.AppearanceCell.Options.UseBackColor = True
    Me.xx_codcope.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codcope.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codcope.Caption = "Descr. operaio"
    Me.xx_codcope.Enabled = False
    Me.xx_codcope.FieldName = "xx_codcope"
    Me.xx_codcope.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codcope.Name = "xx_codcope"
    Me.xx_codcope.NTSRepositoryComboBox = Nothing
    Me.xx_codcope.NTSRepositoryItemCheck = Nothing
    Me.xx_codcope.NTSRepositoryItemMemo = Nothing
    Me.xx_codcope.NTSRepositoryItemText = Nothing
    Me.xx_codcope.OptionsColumn.AllowEdit = False
    Me.xx_codcope.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codcope.OptionsColumn.ReadOnly = True
    Me.xx_codcope.OptionsFilter.AllowFilter = False
    Me.xx_codcope.Width = 70
    '
    'og_codcage
    '
    Me.og_codcage.AppearanceCell.Options.UseBackColor = True
    Me.og_codcage.AppearanceCell.Options.UseTextOptions = True
    Me.og_codcage.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_codcage.Caption = "Cod agente"
    Me.og_codcage.Enabled = True
    Me.og_codcage.FieldName = "og_codcage"
    Me.og_codcage.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_codcage.Name = "og_codcage"
    Me.og_codcage.NTSRepositoryComboBox = Nothing
    Me.og_codcage.NTSRepositoryItemCheck = Nothing
    Me.og_codcage.NTSRepositoryItemMemo = Nothing
    Me.og_codcage.NTSRepositoryItemText = Nothing
    Me.og_codcage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_codcage.OptionsFilter.AllowFilter = False
    Me.og_codcage.Width = 70
    '
    'xx_codcage
    '
    Me.xx_codcage.AppearanceCell.Options.UseBackColor = True
    Me.xx_codcage.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codcage.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codcage.Caption = "Descr. agente"
    Me.xx_codcage.Enabled = False
    Me.xx_codcage.FieldName = "xx_codcage"
    Me.xx_codcage.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codcage.Name = "xx_codcage"
    Me.xx_codcage.NTSRepositoryComboBox = Nothing
    Me.xx_codcage.NTSRepositoryItemCheck = Nothing
    Me.xx_codcage.NTSRepositoryItemMemo = Nothing
    Me.xx_codcage.NTSRepositoryItemText = Nothing
    Me.xx_codcage.OptionsColumn.AllowEdit = False
    Me.xx_codcage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codcage.OptionsColumn.ReadOnly = True
    Me.xx_codcage.OptionsFilter.AllowFilter = False
    Me.xx_codcage.Width = 70
    '
    'og_usaem
    '
    Me.og_usaem.AppearanceCell.Options.UseBackColor = True
    Me.og_usaem.AppearanceCell.Options.UseTextOptions = True
    Me.og_usaem.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_usaem.Caption = "Mod. corr."
    Me.og_usaem.Enabled = True
    Me.og_usaem.FieldName = "og_usaem"
    Me.og_usaem.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_usaem.Name = "og_usaem"
    Me.og_usaem.NTSRepositoryComboBox = Nothing
    Me.og_usaem.NTSRepositoryItemCheck = Nothing
    Me.og_usaem.NTSRepositoryItemMemo = Nothing
    Me.og_usaem.NTSRepositoryItemText = Nothing
    Me.og_usaem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_usaem.OptionsFilter.AllowFilter = False
    '
    'og_old
    '
    Me.og_old.AppearanceCell.Options.UseBackColor = True
    Me.og_old.AppearanceCell.Options.UseTextOptions = True
    Me.og_old.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_old.Caption = "Non operativo/obsoleto"
    Me.og_old.Enabled = True
    Me.og_old.FieldName = "og_old"
    Me.og_old.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_old.Name = "og_old"
    Me.og_old.NTSRepositoryComboBox = Nothing
    Me.og_old.NTSRepositoryItemCheck = Nothing
    Me.og_old.NTSRepositoryItemMemo = Nothing
    Me.og_old.NTSRepositoryItemText = Nothing
    Me.og_old.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_old.OptionsFilter.AllowFilter = False
    '
    'og_telefint
    '
    Me.og_telefint.AppearanceCell.Options.UseBackColor = True
    Me.og_telefint.AppearanceCell.Options.UseTextOptions = True
    Me.og_telefint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.og_telefint.Caption = "Telef. diretto"
    Me.og_telefint.Enabled = True
    Me.og_telefint.FieldName = "og_telefint"
    Me.og_telefint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.og_telefint.Name = "og_telefint"
    Me.og_telefint.NTSRepositoryComboBox = Nothing
    Me.og_telefint.NTSRepositoryItemCheck = Nothing
    Me.og_telefint.NTSRepositoryItemMemo = Nothing
    Me.og_telefint.NTSRepositoryItemText = Nothing
    Me.og_telefint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.og_telefint.OptionsFilter.AllowFilter = False
    '
    'xx_descrizione
    '
    Me.xx_descrizione.AppearanceCell.Options.UseBackColor = True
    Me.xx_descrizione.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descrizione.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descrizione.Caption = "Descrizione"
    Me.xx_descrizione.Enabled = True
    Me.xx_descrizione.FieldName = "xx_descrizione"
    Me.xx_descrizione.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descrizione.Name = "xx_descrizione"
    Me.xx_descrizione.NTSRepositoryComboBox = Nothing
    Me.xx_descrizione.NTSRepositoryItemCheck = Nothing
    Me.xx_descrizione.NTSRepositoryItemMemo = Nothing
    Me.xx_descrizione.NTSRepositoryItemText = Nothing
    Me.xx_descrizione.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descrizione.OptionsFilter.AllowFilter = False
    Me.xx_descrizione.Visible = True
    Me.xx_descrizione.VisibleIndex = 0
    '
    'pnAll
    '
    Me.pnAll.AllowDrop = True
    Me.pnAll.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAll.Appearance.Options.UseBackColor = True
    Me.pnAll.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAll.Controls.Add(Me.pnPersone)
    Me.pnAll.Controls.Add(Me.pnDati)
    Me.pnAll.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAll.Location = New System.Drawing.Point(0, 30)
    Me.pnAll.Name = "pnAll"
    Me.pnAll.NTSActiveTrasparency = True
    Me.pnAll.Size = New System.Drawing.Size(719, 516)
    Me.pnAll.TabIndex = 6
    Me.pnAll.Text = "NtsPanel1"
    '
    'pnPersone
    '
    Me.pnPersone.AllowDrop = True
    Me.pnPersone.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPersone.Appearance.Options.UseBackColor = True
    Me.pnPersone.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPersone.Controls.Add(Me.grOrga)
    Me.pnPersone.Controls.Add(Me.pnRicerca)
    Me.pnPersone.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnPersone.Location = New System.Drawing.Point(0, 0)
    Me.pnPersone.Name = "pnPersone"
    Me.pnPersone.NTSActiveTrasparency = True
    Me.pnPersone.Size = New System.Drawing.Size(162, 516)
    Me.pnPersone.TabIndex = 6
    Me.pnPersone.Text = "NtsPanel1"
    '
    'pnRicerca
    '
    Me.pnRicerca.AllowDrop = True
    Me.pnRicerca.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRicerca.Appearance.Options.UseBackColor = True
    Me.pnRicerca.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRicerca.Controls.Add(Me.cmdRicerca)
    Me.pnRicerca.Controls.Add(Me.edRicerca)
    Me.pnRicerca.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnRicerca.Location = New System.Drawing.Point(0, 0)
    Me.pnRicerca.Name = "pnRicerca"
    Me.pnRicerca.NTSActiveTrasparency = True
    Me.pnRicerca.Size = New System.Drawing.Size(162, 32)
    Me.pnRicerca.TabIndex = 6
    Me.pnRicerca.Text = "NtsPanel1"
    '
    'cmdRicerca
    '
    Me.cmdRicerca.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdRicerca.Image = CType(resources.GetObject("cmdRicerca.Image"), System.Drawing.Image)
    Me.cmdRicerca.ImagePath = ""
    Me.cmdRicerca.ImageText = ""
    Me.cmdRicerca.Location = New System.Drawing.Point(122, 3)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.NTSContextMenu = Nothing
    Me.cmdRicerca.Size = New System.Drawing.Size(36, 26)
    Me.cmdRicerca.TabIndex = 1
    '
    'edRicerca
    '
    Me.edRicerca.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edRicerca.EditValue = ""
    Me.edRicerca.Location = New System.Drawing.Point(3, 6)
    Me.edRicerca.Name = "edRicerca"
    Me.edRicerca.NTSDbField = ""
    Me.edRicerca.NTSForzaVisZoom = False
    Me.edRicerca.NTSOldValue = ""
    Me.edRicerca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRicerca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRicerca.Properties.AutoHeight = False
    Me.edRicerca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRicerca.Properties.MaxLength = 65536
    Me.edRicerca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRicerca.Size = New System.Drawing.Size(113, 20)
    Me.edRicerca.TabIndex = 0
    '
    'pnDati
    '
    Me.pnDati.AllowDrop = True
    Me.pnDati.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDati.Appearance.Options.UseBackColor = True
    Me.pnDati.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDati.Controls.Add(Me.pnDatiPersona)
    Me.pnDati.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnDati.Location = New System.Drawing.Point(162, 0)
    Me.pnDati.Name = "pnDati"
    Me.pnDati.NTSActiveTrasparency = True
    Me.pnDati.Size = New System.Drawing.Size(557, 516)
    Me.pnDati.TabIndex = 7
    Me.pnDati.Text = "NtsPanel1"
    '
    'pnDatiPersona
    '
    Me.pnDatiPersona.AllowDrop = True
    Me.pnDatiPersona.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDatiPersona.Appearance.Options.UseBackColor = True
    Me.pnDatiPersona.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDatiPersona.Controls.Add(Me.fmStato)
    Me.pnDatiPersona.Controls.Add(Me.fmResidenza)
    Me.pnDatiPersona.Controls.Add(Me.edOg_rep)
    Me.pnDatiPersona.Controls.Add(Me.lbOg_rep)
    Me.pnDatiPersona.Controls.Add(Me.edOg_divis)
    Me.pnDatiPersona.Controls.Add(Me.lbOg_divis)
    Me.pnDatiPersona.Controls.Add(Me.edOg_sede)
    Me.pnDatiPersona.Controls.Add(Me.lbOg_sede)
    Me.pnDatiPersona.Controls.Add(Me.edOg_datnasc)
    Me.pnDatiPersona.Controls.Add(Me.cbOg_sesso)
    Me.pnDatiPersona.Controls.Add(Me.fmRecapiti)
    Me.pnDatiPersona.Controls.Add(Me.edOg_mansioni)
    Me.pnDatiPersona.Controls.Add(Me.edOg_descont2)
    Me.pnDatiPersona.Controls.Add(Me.edOg_descont)
    Me.pnDatiPersona.Controls.Add(Me.edOg_codruaz)
    Me.pnDatiPersona.Controls.Add(Me.edOg_titolo)
    Me.pnDatiPersona.Controls.Add(Me.lbOg_datnasc)
    Me.pnDatiPersona.Controls.Add(Me.lbOg_sesso)
    Me.pnDatiPersona.Controls.Add(Me.lbOg_descont2)
    Me.pnDatiPersona.Controls.Add(Me.lbOg_descont)
    Me.pnDatiPersona.Controls.Add(Me.lbXx_codruaz)
    Me.pnDatiPersona.Controls.Add(Me.lbOg_mansioni)
    Me.pnDatiPersona.Controls.Add(Me.lbOg_codruaz)
    Me.pnDatiPersona.Controls.Add(Me.lbOg_titolo)
    Me.pnDatiPersona.Controls.Add(Me.fmSocialNetwork)
    Me.pnDatiPersona.Controls.Add(Me.fmContatti)
    Me.pnDatiPersona.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnDatiPersona.Location = New System.Drawing.Point(0, 0)
    Me.pnDatiPersona.Name = "pnDatiPersona"
    Me.pnDatiPersona.NTSActiveTrasparency = True
    Me.pnDatiPersona.Size = New System.Drawing.Size(557, 516)
    Me.pnDatiPersona.TabIndex = 0
    Me.pnDatiPersona.Text = "NtsPanel1"
    '
    'fmStato
    '
    Me.fmStato.AllowDrop = True
    Me.fmStato.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmStato.Appearance.Options.UseBackColor = True
    Me.fmStato.Controls.Add(Me.ckOg_old)
    Me.fmStato.Controls.Add(Me.edOg_dtiniz)
    Me.fmStato.Controls.Add(Me.lbOg_dtiniz)
    Me.fmStato.Controls.Add(Me.edOg_codstco)
    Me.fmStato.Controls.Add(Me.lbOg_referente)
    Me.fmStato.Controls.Add(Me.edOg_referente)
    Me.fmStato.Controls.Add(Me.lbXx_referente)
    Me.fmStato.Controls.Add(Me.edOg_dtfine)
    Me.fmStato.Controls.Add(Me.lbOg_codstco)
    Me.fmStato.Controls.Add(Me.lbOg_dtfine)
    Me.fmStato.Controls.Add(Me.lbXx_codstco)
    Me.fmStato.Location = New System.Drawing.Point(0, 93)
    Me.fmStato.Name = "fmStato"
    Me.fmStato.Size = New System.Drawing.Size(557, 71)
    Me.fmStato.TabIndex = 19
    Me.fmStato.Text = "Situazione"
    '
    'ckOg_old
    '
    Me.ckOg_old.Location = New System.Drawing.Point(414, 45)
    Me.ckOg_old.Name = "ckOg_old"
    Me.ckOg_old.NTSCheckValue = "S"
    Me.ckOg_old.NTSUnCheckValue = "N"
    Me.ckOg_old.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOg_old.Properties.Appearance.Options.UseBackColor = True
    Me.ckOg_old.Properties.AutoHeight = False
    Me.ckOg_old.Properties.Caption = "Non operativo\Obsoleto"
    Me.ckOg_old.Size = New System.Drawing.Size(139, 21)
    Me.ckOg_old.TabIndex = 17
    '
    'edOg_dtiniz
    '
    Me.edOg_dtiniz.EditValue = "01/01/1900"
    Me.edOg_dtiniz.Location = New System.Drawing.Point(110, 22)
    Me.edOg_dtiniz.Name = "edOg_dtiniz"
    Me.edOg_dtiniz.NTSDbField = ""
    Me.edOg_dtiniz.NTSForzaVisZoom = False
    Me.edOg_dtiniz.NTSOldValue = ""
    Me.edOg_dtiniz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_dtiniz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_dtiniz.Properties.AutoHeight = False
    Me.edOg_dtiniz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_dtiniz.Properties.MaxLength = 65536
    Me.edOg_dtiniz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_dtiniz.Size = New System.Drawing.Size(80, 20)
    Me.edOg_dtiniz.TabIndex = 13
    '
    'lbOg_dtiniz
    '
    Me.lbOg_dtiniz.AutoSize = True
    Me.lbOg_dtiniz.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_dtiniz.Location = New System.Drawing.Point(3, 25)
    Me.lbOg_dtiniz.Name = "lbOg_dtiniz"
    Me.lbOg_dtiniz.NTSDbField = ""
    Me.lbOg_dtiniz.Size = New System.Drawing.Size(67, 13)
    Me.lbOg_dtiniz.TabIndex = 7
    Me.lbOg_dtiniz.Text = "Presente dal"
    Me.lbOg_dtiniz.Tooltip = ""
    Me.lbOg_dtiniz.UseMnemonic = False
    '
    'edOg_codstco
    '
    Me.edOg_codstco.EditValue = "0"
    Me.edOg_codstco.Location = New System.Drawing.Point(110, 46)
    Me.edOg_codstco.Name = "edOg_codstco"
    Me.edOg_codstco.NTSDbField = ""
    Me.edOg_codstco.NTSFormat = "0"
    Me.edOg_codstco.NTSForzaVisZoom = False
    Me.edOg_codstco.NTSOldValue = ""
    Me.edOg_codstco.Properties.Appearance.Options.UseTextOptions = True
    Me.edOg_codstco.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edOg_codstco.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_codstco.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_codstco.Properties.AutoHeight = False
    Me.edOg_codstco.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_codstco.Properties.MaxLength = 65536
    Me.edOg_codstco.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_codstco.Size = New System.Drawing.Size(80, 20)
    Me.edOg_codstco.TabIndex = 16
    '
    'lbOg_referente
    '
    Me.lbOg_referente.AutoSize = True
    Me.lbOg_referente.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_referente.Location = New System.Drawing.Point(297, 25)
    Me.lbOg_referente.Name = "lbOg_referente"
    Me.lbOg_referente.NTSDbField = ""
    Me.lbOg_referente.Size = New System.Drawing.Size(43, 13)
    Me.lbOg_referente.TabIndex = 7
    Me.lbOg_referente.Text = "Tramite"
    Me.lbOg_referente.Tooltip = ""
    Me.lbOg_referente.UseMnemonic = False
    '
    'edOg_referente
    '
    Me.edOg_referente.EditValue = "0"
    Me.edOg_referente.Location = New System.Drawing.Point(344, 22)
    Me.edOg_referente.Name = "edOg_referente"
    Me.edOg_referente.NTSDbField = ""
    Me.edOg_referente.NTSFormat = "0"
    Me.edOg_referente.NTSForzaVisZoom = False
    Me.edOg_referente.NTSOldValue = ""
    Me.edOg_referente.Properties.Appearance.Options.UseTextOptions = True
    Me.edOg_referente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edOg_referente.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_referente.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_referente.Properties.AutoHeight = False
    Me.edOg_referente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_referente.Properties.MaxLength = 65536
    Me.edOg_referente.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_referente.Size = New System.Drawing.Size(68, 20)
    Me.edOg_referente.TabIndex = 16
    '
    'lbXx_referente
    '
    Me.lbXx_referente.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_referente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_referente.Location = New System.Drawing.Point(416, 22)
    Me.lbXx_referente.Name = "lbXx_referente"
    Me.lbXx_referente.NTSDbField = ""
    Me.lbXx_referente.Size = New System.Drawing.Size(132, 20)
    Me.lbXx_referente.TabIndex = 7
    Me.lbXx_referente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_referente.Tooltip = ""
    Me.lbXx_referente.UseMnemonic = False
    '
    'edOg_dtfine
    '
    Me.edOg_dtfine.EditValue = "01/01/1900"
    Me.edOg_dtfine.Location = New System.Drawing.Point(212, 22)
    Me.edOg_dtfine.Name = "edOg_dtfine"
    Me.edOg_dtfine.NTSDbField = ""
    Me.edOg_dtfine.NTSForzaVisZoom = False
    Me.edOg_dtfine.NTSOldValue = ""
    Me.edOg_dtfine.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_dtfine.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_dtfine.Properties.AutoHeight = False
    Me.edOg_dtfine.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_dtfine.Properties.MaxLength = 65536
    Me.edOg_dtfine.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_dtfine.Size = New System.Drawing.Size(80, 20)
    Me.edOg_dtfine.TabIndex = 15
    '
    'lbOg_codstco
    '
    Me.lbOg_codstco.AutoSize = True
    Me.lbOg_codstco.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_codstco.Location = New System.Drawing.Point(3, 49)
    Me.lbOg_codstco.Name = "lbOg_codstco"
    Me.lbOg_codstco.NTSDbField = ""
    Me.lbOg_codstco.Size = New System.Drawing.Size(101, 13)
    Me.lbOg_codstco.TabIndex = 7
    Me.lbOg_codstco.Text = "Status Commerciale"
    Me.lbOg_codstco.Tooltip = ""
    Me.lbOg_codstco.UseMnemonic = False
    '
    'lbOg_dtfine
    '
    Me.lbOg_dtfine.AutoSize = True
    Me.lbOg_dtfine.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_dtfine.Location = New System.Drawing.Point(194, 25)
    Me.lbOg_dtfine.Name = "lbOg_dtfine"
    Me.lbOg_dtfine.NTSDbField = ""
    Me.lbOg_dtfine.Size = New System.Drawing.Size(15, 13)
    Me.lbOg_dtfine.TabIndex = 14
    Me.lbOg_dtfine.Text = "al"
    Me.lbOg_dtfine.Tooltip = ""
    Me.lbOg_dtfine.UseMnemonic = False
    '
    'lbXx_codstco
    '
    Me.lbXx_codstco.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codstco.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codstco.Location = New System.Drawing.Point(196, 45)
    Me.lbXx_codstco.Name = "lbXx_codstco"
    Me.lbXx_codstco.NTSDbField = ""
    Me.lbXx_codstco.Size = New System.Drawing.Size(216, 21)
    Me.lbXx_codstco.TabIndex = 7
    Me.lbXx_codstco.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_codstco.Tooltip = ""
    Me.lbXx_codstco.UseMnemonic = False
    '
    'fmResidenza
    '
    Me.fmResidenza.AllowDrop = True
    Me.fmResidenza.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmResidenza.Appearance.Options.UseBackColor = True
    Me.fmResidenza.Controls.Add(Me.lbOg_indir)
    Me.fmResidenza.Controls.Add(Me.edOg_indir)
    Me.fmResidenza.Controls.Add(Me.lbOg_cap)
    Me.fmResidenza.Controls.Add(Me.edOg_cap)
    Me.fmResidenza.Controls.Add(Me.lbOg_citta)
    Me.fmResidenza.Controls.Add(Me.edOg_citta)
    Me.fmResidenza.Controls.Add(Me.lbOg_prov)
    Me.fmResidenza.Controls.Add(Me.edOg_prov)
    Me.fmResidenza.Controls.Add(Me.lbOg_stato)
    Me.fmResidenza.Controls.Add(Me.lbXx_stato)
    Me.fmResidenza.Controls.Add(Me.edOg_stato)
    Me.fmResidenza.Location = New System.Drawing.Point(0, 164)
    Me.fmResidenza.Name = "fmResidenza"
    Me.fmResidenza.Size = New System.Drawing.Size(557, 70)
    Me.fmResidenza.TabIndex = 17
    Me.fmResidenza.Text = "Residenza"
    '
    'lbOg_indir
    '
    Me.lbOg_indir.AutoSize = True
    Me.lbOg_indir.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_indir.Location = New System.Drawing.Point(2, 25)
    Me.lbOg_indir.Name = "lbOg_indir"
    Me.lbOg_indir.NTSDbField = ""
    Me.lbOg_indir.Size = New System.Drawing.Size(47, 13)
    Me.lbOg_indir.TabIndex = 3
    Me.lbOg_indir.Text = "Indirizzo"
    Me.lbOg_indir.Tooltip = ""
    Me.lbOg_indir.UseMnemonic = False
    '
    'edOg_indir
    '
    Me.edOg_indir.EditValue = ""
    Me.edOg_indir.Location = New System.Drawing.Point(65, 22)
    Me.edOg_indir.Name = "edOg_indir"
    Me.edOg_indir.NTSDbField = ""
    Me.edOg_indir.NTSForzaVisZoom = False
    Me.edOg_indir.NTSOldValue = ""
    Me.edOg_indir.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_indir.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_indir.Properties.AutoHeight = False
    Me.edOg_indir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_indir.Properties.MaxLength = 65536
    Me.edOg_indir.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_indir.Size = New System.Drawing.Size(240, 20)
    Me.edOg_indir.TabIndex = 4
    '
    'lbOg_cap
    '
    Me.lbOg_cap.AutoSize = True
    Me.lbOg_cap.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_cap.Location = New System.Drawing.Point(2, 49)
    Me.lbOg_cap.Name = "lbOg_cap"
    Me.lbOg_cap.NTSDbField = ""
    Me.lbOg_cap.Size = New System.Drawing.Size(27, 13)
    Me.lbOg_cap.TabIndex = 3
    Me.lbOg_cap.Text = "CAP"
    Me.lbOg_cap.Tooltip = ""
    Me.lbOg_cap.UseMnemonic = False
    '
    'edOg_cap
    '
    Me.edOg_cap.EditValue = ""
    Me.edOg_cap.Location = New System.Drawing.Point(65, 46)
    Me.edOg_cap.Name = "edOg_cap"
    Me.edOg_cap.NTSDbField = ""
    Me.edOg_cap.NTSForzaVisZoom = False
    Me.edOg_cap.NTSOldValue = ""
    Me.edOg_cap.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_cap.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_cap.Properties.AutoHeight = False
    Me.edOg_cap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_cap.Properties.MaxLength = 65536
    Me.edOg_cap.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_cap.Size = New System.Drawing.Size(70, 20)
    Me.edOg_cap.TabIndex = 4
    '
    'lbOg_citta
    '
    Me.lbOg_citta.AutoSize = True
    Me.lbOg_citta.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_citta.Location = New System.Drawing.Point(140, 49)
    Me.lbOg_citta.Name = "lbOg_citta"
    Me.lbOg_citta.NTSDbField = ""
    Me.lbOg_citta.Size = New System.Drawing.Size(30, 13)
    Me.lbOg_citta.TabIndex = 3
    Me.lbOg_citta.Text = "Città"
    Me.lbOg_citta.Tooltip = ""
    Me.lbOg_citta.UseMnemonic = False
    '
    'edOg_citta
    '
    Me.edOg_citta.EditValue = ""
    Me.edOg_citta.Location = New System.Drawing.Point(176, 46)
    Me.edOg_citta.Name = "edOg_citta"
    Me.edOg_citta.NTSDbField = ""
    Me.edOg_citta.NTSForzaVisZoom = False
    Me.edOg_citta.NTSOldValue = ""
    Me.edOg_citta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_citta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_citta.Properties.AutoHeight = False
    Me.edOg_citta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_citta.Properties.MaxLength = 65536
    Me.edOg_citta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_citta.Size = New System.Drawing.Size(282, 20)
    Me.edOg_citta.TabIndex = 4
    '
    'lbOg_prov
    '
    Me.lbOg_prov.AutoSize = True
    Me.lbOg_prov.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_prov.Location = New System.Drawing.Point(465, 49)
    Me.lbOg_prov.Name = "lbOg_prov"
    Me.lbOg_prov.NTSDbField = ""
    Me.lbOg_prov.Size = New System.Drawing.Size(50, 13)
    Me.lbOg_prov.TabIndex = 3
    Me.lbOg_prov.Text = "Provincia"
    Me.lbOg_prov.Tooltip = ""
    Me.lbOg_prov.UseMnemonic = False
    '
    'edOg_prov
    '
    Me.edOg_prov.EditValue = ""
    Me.edOg_prov.Location = New System.Drawing.Point(517, 46)
    Me.edOg_prov.Name = "edOg_prov"
    Me.edOg_prov.NTSDbField = ""
    Me.edOg_prov.NTSForzaVisZoom = False
    Me.edOg_prov.NTSOldValue = ""
    Me.edOg_prov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_prov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_prov.Properties.AutoHeight = False
    Me.edOg_prov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_prov.Properties.MaxLength = 65536
    Me.edOg_prov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_prov.Size = New System.Drawing.Size(31, 20)
    Me.edOg_prov.TabIndex = 4
    '
    'lbOg_stato
    '
    Me.lbOg_stato.AutoSize = True
    Me.lbOg_stato.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_stato.Location = New System.Drawing.Point(309, 25)
    Me.lbOg_stato.Name = "lbOg_stato"
    Me.lbOg_stato.NTSDbField = ""
    Me.lbOg_stato.Size = New System.Drawing.Size(33, 13)
    Me.lbOg_stato.TabIndex = 3
    Me.lbOg_stato.Text = "Stato"
    Me.lbOg_stato.Tooltip = ""
    Me.lbOg_stato.UseMnemonic = False
    '
    'lbXx_stato
    '
    Me.lbXx_stato.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_stato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_stato.Location = New System.Drawing.Point(415, 22)
    Me.lbXx_stato.Name = "lbXx_stato"
    Me.lbXx_stato.NTSDbField = ""
    Me.lbXx_stato.Size = New System.Drawing.Size(133, 20)
    Me.lbXx_stato.TabIndex = 3
    Me.lbXx_stato.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_stato.Tooltip = ""
    Me.lbXx_stato.UseMnemonic = False
    '
    'edOg_stato
    '
    Me.edOg_stato.EditValue = ""
    Me.edOg_stato.Location = New System.Drawing.Point(348, 22)
    Me.edOg_stato.Name = "edOg_stato"
    Me.edOg_stato.NTSDbField = ""
    Me.edOg_stato.NTSForzaVisZoom = False
    Me.edOg_stato.NTSOldValue = ""
    Me.edOg_stato.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_stato.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_stato.Properties.AutoHeight = False
    Me.edOg_stato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_stato.Properties.MaxLength = 65536
    Me.edOg_stato.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_stato.Size = New System.Drawing.Size(61, 20)
    Me.edOg_stato.TabIndex = 4
    '
    'edOg_rep
    '
    Me.edOg_rep.EditValue = ""
    Me.edOg_rep.Location = New System.Drawing.Point(424, 70)
    Me.edOg_rep.Name = "edOg_rep"
    Me.edOg_rep.NTSDbField = ""
    Me.edOg_rep.NTSForzaVisZoom = False
    Me.edOg_rep.NTSOldValue = ""
    Me.edOg_rep.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_rep.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_rep.Properties.AutoHeight = False
    Me.edOg_rep.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_rep.Properties.MaxLength = 65536
    Me.edOg_rep.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_rep.Size = New System.Drawing.Size(125, 20)
    Me.edOg_rep.TabIndex = 12
    '
    'lbOg_rep
    '
    Me.lbOg_rep.AutoSize = True
    Me.lbOg_rep.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_rep.Location = New System.Drawing.Point(372, 73)
    Me.lbOg_rep.Name = "lbOg_rep"
    Me.lbOg_rep.NTSDbField = ""
    Me.lbOg_rep.Size = New System.Drawing.Size(46, 13)
    Me.lbOg_rep.TabIndex = 11
    Me.lbOg_rep.Text = "Reparto"
    Me.lbOg_rep.Tooltip = ""
    Me.lbOg_rep.UseMnemonic = False
    '
    'edOg_divis
    '
    Me.edOg_divis.EditValue = ""
    Me.edOg_divis.Location = New System.Drawing.Point(246, 70)
    Me.edOg_divis.Name = "edOg_divis"
    Me.edOg_divis.NTSDbField = ""
    Me.edOg_divis.NTSForzaVisZoom = False
    Me.edOg_divis.NTSOldValue = ""
    Me.edOg_divis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_divis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_divis.Properties.AutoHeight = False
    Me.edOg_divis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_divis.Properties.MaxLength = 65536
    Me.edOg_divis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_divis.Size = New System.Drawing.Size(120, 20)
    Me.edOg_divis.TabIndex = 10
    '
    'lbOg_divis
    '
    Me.lbOg_divis.AutoSize = True
    Me.lbOg_divis.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_divis.Location = New System.Drawing.Point(191, 73)
    Me.lbOg_divis.Name = "lbOg_divis"
    Me.lbOg_divis.NTSDbField = ""
    Me.lbOg_divis.Size = New System.Drawing.Size(49, 13)
    Me.lbOg_divis.TabIndex = 9
    Me.lbOg_divis.Text = "Divisione"
    Me.lbOg_divis.Tooltip = ""
    Me.lbOg_divis.UseMnemonic = False
    '
    'edOg_sede
    '
    Me.edOg_sede.EditValue = ""
    Me.edOg_sede.Location = New System.Drawing.Point(65, 70)
    Me.edOg_sede.Name = "edOg_sede"
    Me.edOg_sede.NTSDbField = ""
    Me.edOg_sede.NTSForzaVisZoom = False
    Me.edOg_sede.NTSOldValue = ""
    Me.edOg_sede.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_sede.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_sede.Properties.AutoHeight = False
    Me.edOg_sede.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_sede.Properties.MaxLength = 65536
    Me.edOg_sede.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_sede.Size = New System.Drawing.Size(120, 20)
    Me.edOg_sede.TabIndex = 8
    '
    'lbOg_sede
    '
    Me.lbOg_sede.AutoSize = True
    Me.lbOg_sede.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_sede.Location = New System.Drawing.Point(3, 73)
    Me.lbOg_sede.Name = "lbOg_sede"
    Me.lbOg_sede.NTSDbField = ""
    Me.lbOg_sede.Size = New System.Drawing.Size(31, 13)
    Me.lbOg_sede.TabIndex = 7
    Me.lbOg_sede.Text = "Sede"
    Me.lbOg_sede.Tooltip = ""
    Me.lbOg_sede.UseMnemonic = False
    '
    'edOg_datnasc
    '
    Me.edOg_datnasc.EditValue = "01/01/1900"
    Me.edOg_datnasc.Location = New System.Drawing.Point(469, 22)
    Me.edOg_datnasc.Name = "edOg_datnasc"
    Me.edOg_datnasc.NTSDbField = ""
    Me.edOg_datnasc.NTSForzaVisZoom = False
    Me.edOg_datnasc.NTSOldValue = ""
    Me.edOg_datnasc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_datnasc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_datnasc.Properties.AutoHeight = False
    Me.edOg_datnasc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_datnasc.Properties.MaxLength = 65536
    Me.edOg_datnasc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_datnasc.Size = New System.Drawing.Size(80, 20)
    Me.edOg_datnasc.TabIndex = 6
    '
    'cbOg_sesso
    '
    Me.cbOg_sesso.DataSource = Nothing
    Me.cbOg_sesso.DisplayMember = ""
    Me.cbOg_sesso.Location = New System.Drawing.Point(372, 22)
    Me.cbOg_sesso.Name = "cbOg_sesso"
    Me.cbOg_sesso.NTSDbField = ""
    Me.cbOg_sesso.Properties.AutoHeight = False
    Me.cbOg_sesso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbOg_sesso.Properties.DropDownRows = 30
    Me.cbOg_sesso.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbOg_sesso.SelectedValue = ""
    Me.cbOg_sesso.Size = New System.Drawing.Size(91, 20)
    Me.cbOg_sesso.TabIndex = 5
    Me.cbOg_sesso.ValueMember = ""
    '
    'fmRecapiti
    '
    Me.fmRecapiti.AllowDrop = True
    Me.fmRecapiti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmRecapiti.Appearance.Options.UseBackColor = True
    Me.fmRecapiti.Controls.Add(Me.cmdOg_skypeuser)
    Me.fmRecapiti.Controls.Add(Me.edOg_skypeuser)
    Me.fmRecapiti.Controls.Add(Me.lbOg_skypeuser)
    Me.fmRecapiti.Controls.Add(Me.cbOg_usaem)
    Me.fmRecapiti.Controls.Add(Me.lbOg_usaem)
    Me.fmRecapiti.Controls.Add(Me.cmdOg_emailpers)
    Me.fmRecapiti.Controls.Add(Me.cmdOg_email)
    Me.fmRecapiti.Controls.Add(Me.cmdOg_cellpers)
    Me.fmRecapiti.Controls.Add(Me.cmdOg_cell)
    Me.fmRecapiti.Controls.Add(Me.cmdOg_telefpers)
    Me.fmRecapiti.Controls.Add(Me.cmdOg_telef)
    Me.fmRecapiti.Controls.Add(Me.edOg_faxpers)
    Me.fmRecapiti.Controls.Add(Me.edOg_fax)
    Me.fmRecapiti.Controls.Add(Me.edOg_emailpers)
    Me.fmRecapiti.Controls.Add(Me.edOg_email)
    Me.fmRecapiti.Controls.Add(Me.edOg_cellpers)
    Me.fmRecapiti.Controls.Add(Me.edOg_cell)
    Me.fmRecapiti.Controls.Add(Me.edOg_telefpers)
    Me.fmRecapiti.Controls.Add(Me.edOg_telefint)
    Me.fmRecapiti.Controls.Add(Me.edOg_telef)
    Me.fmRecapiti.Controls.Add(Me.lbFax)
    Me.fmRecapiti.Controls.Add(Me.lbEmail)
    Me.fmRecapiti.Controls.Add(Me.lbCellulare)
    Me.fmRecapiti.Controls.Add(Me.lbAziendale)
    Me.fmRecapiti.Controls.Add(Me.lbPersonale)
    Me.fmRecapiti.Controls.Add(Me.lbOg_telefint)
    Me.fmRecapiti.Controls.Add(Me.lbTelefono)
    Me.fmRecapiti.Location = New System.Drawing.Point(0, 234)
    Me.fmRecapiti.Name = "fmRecapiti"
    Me.fmRecapiti.Size = New System.Drawing.Size(557, 141)
    Me.fmRecapiti.TabIndex = 2
    Me.fmRecapiti.Text = "Recapiti"
    '
    'cmdOg_skypeuser
    '
    Me.cmdOg_skypeuser.Image = CType(resources.GetObject("cmdOg_skypeuser.Image"), System.Drawing.Image)
    Me.cmdOg_skypeuser.ImagePath = ""
    Me.cmdOg_skypeuser.ImageText = ""
    Me.cmdOg_skypeuser.Location = New System.Drawing.Point(507, 116)
    Me.cmdOg_skypeuser.Name = "cmdOg_skypeuser"
    Me.cmdOg_skypeuser.NTSContextMenu = Nothing
    Me.cmdOg_skypeuser.Size = New System.Drawing.Size(30, 24)
    Me.cmdOg_skypeuser.TabIndex = 9
    '
    'edOg_skypeuser
    '
    Me.edOg_skypeuser.EditValue = ""
    Me.edOg_skypeuser.Location = New System.Drawing.Point(351, 118)
    Me.edOg_skypeuser.Name = "edOg_skypeuser"
    Me.edOg_skypeuser.NTSDbField = ""
    Me.edOg_skypeuser.NTSForzaVisZoom = False
    Me.edOg_skypeuser.NTSOldValue = ""
    Me.edOg_skypeuser.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_skypeuser.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_skypeuser.Properties.AutoHeight = False
    Me.edOg_skypeuser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_skypeuser.Properties.MaxLength = 65536
    Me.edOg_skypeuser.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_skypeuser.Size = New System.Drawing.Size(153, 20)
    Me.edOg_skypeuser.TabIndex = 8
    '
    'lbOg_skypeuser
    '
    Me.lbOg_skypeuser.AutoSize = True
    Me.lbOg_skypeuser.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_skypeuser.Location = New System.Drawing.Point(304, 121)
    Me.lbOg_skypeuser.Name = "lbOg_skypeuser"
    Me.lbOg_skypeuser.NTSDbField = ""
    Me.lbOg_skypeuser.Size = New System.Drawing.Size(36, 13)
    Me.lbOg_skypeuser.TabIndex = 7
    Me.lbOg_skypeuser.Text = "Skype"
    Me.lbOg_skypeuser.Tooltip = ""
    Me.lbOg_skypeuser.UseMnemonic = False
    '
    'cbOg_usaem
    '
    Me.cbOg_usaem.DataSource = Nothing
    Me.cbOg_usaem.DisplayMember = ""
    Me.cbOg_usaem.Location = New System.Drawing.Point(65, 118)
    Me.cbOg_usaem.Name = "cbOg_usaem"
    Me.cbOg_usaem.NTSDbField = ""
    Me.cbOg_usaem.Properties.AutoHeight = False
    Me.cbOg_usaem.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbOg_usaem.Properties.DropDownRows = 30
    Me.cbOg_usaem.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbOg_usaem.SelectedValue = ""
    Me.cbOg_usaem.Size = New System.Drawing.Size(153, 20)
    Me.cbOg_usaem.TabIndex = 6
    Me.cbOg_usaem.ValueMember = ""
    '
    'lbOg_usaem
    '
    Me.lbOg_usaem.AutoSize = True
    Me.lbOg_usaem.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_usaem.Location = New System.Drawing.Point(3, 121)
    Me.lbOg_usaem.Name = "lbOg_usaem"
    Me.lbOg_usaem.NTSDbField = ""
    Me.lbOg_usaem.Size = New System.Drawing.Size(50, 13)
    Me.lbOg_usaem.TabIndex = 3
    Me.lbOg_usaem.Text = "Invio per"
    Me.lbOg_usaem.Tooltip = ""
    Me.lbOg_usaem.UseMnemonic = False
    '
    'cmdOg_emailpers
    '
    Me.cmdOg_emailpers.Image = CType(resources.GetObject("cmdOg_emailpers.Image"), System.Drawing.Image)
    Me.cmdOg_emailpers.ImagePath = ""
    Me.cmdOg_emailpers.ImageText = ""
    Me.cmdOg_emailpers.Location = New System.Drawing.Point(507, 68)
    Me.cmdOg_emailpers.Name = "cmdOg_emailpers"
    Me.cmdOg_emailpers.NTSContextMenu = Nothing
    Me.cmdOg_emailpers.Size = New System.Drawing.Size(30, 24)
    Me.cmdOg_emailpers.TabIndex = 2
    '
    'cmdOg_email
    '
    Me.cmdOg_email.Image = CType(resources.GetObject("cmdOg_email.Image"), System.Drawing.Image)
    Me.cmdOg_email.ImagePath = ""
    Me.cmdOg_email.ImageText = ""
    Me.cmdOg_email.Location = New System.Drawing.Point(221, 68)
    Me.cmdOg_email.Name = "cmdOg_email"
    Me.cmdOg_email.NTSContextMenu = Nothing
    Me.cmdOg_email.Size = New System.Drawing.Size(30, 24)
    Me.cmdOg_email.TabIndex = 2
    '
    'cmdOg_cellpers
    '
    Me.cmdOg_cellpers.Image = CType(resources.GetObject("cmdOg_cellpers.Image"), System.Drawing.Image)
    Me.cmdOg_cellpers.ImagePath = ""
    Me.cmdOg_cellpers.ImageText = ""
    Me.cmdOg_cellpers.Location = New System.Drawing.Point(507, 44)
    Me.cmdOg_cellpers.Name = "cmdOg_cellpers"
    Me.cmdOg_cellpers.NTSContextMenu = Nothing
    Me.cmdOg_cellpers.Size = New System.Drawing.Size(30, 24)
    Me.cmdOg_cellpers.TabIndex = 2
    '
    'cmdOg_cell
    '
    Me.cmdOg_cell.Image = CType(resources.GetObject("cmdOg_cell.Image"), System.Drawing.Image)
    Me.cmdOg_cell.ImagePath = ""
    Me.cmdOg_cell.ImageText = ""
    Me.cmdOg_cell.Location = New System.Drawing.Point(221, 44)
    Me.cmdOg_cell.Name = "cmdOg_cell"
    Me.cmdOg_cell.NTSContextMenu = Nothing
    Me.cmdOg_cell.Size = New System.Drawing.Size(30, 24)
    Me.cmdOg_cell.TabIndex = 2
    '
    'cmdOg_telefpers
    '
    Me.cmdOg_telefpers.Image = CType(resources.GetObject("cmdOg_telefpers.Image"), System.Drawing.Image)
    Me.cmdOg_telefpers.ImagePath = ""
    Me.cmdOg_telefpers.ImageText = ""
    Me.cmdOg_telefpers.Location = New System.Drawing.Point(507, 20)
    Me.cmdOg_telefpers.Name = "cmdOg_telefpers"
    Me.cmdOg_telefpers.NTSContextMenu = Nothing
    Me.cmdOg_telefpers.Size = New System.Drawing.Size(30, 24)
    Me.cmdOg_telefpers.TabIndex = 2
    '
    'cmdOg_telef
    '
    Me.cmdOg_telef.Image = CType(resources.GetObject("cmdOg_telef.Image"), System.Drawing.Image)
    Me.cmdOg_telef.ImagePath = ""
    Me.cmdOg_telef.ImageText = ""
    Me.cmdOg_telef.Location = New System.Drawing.Point(221, 20)
    Me.cmdOg_telef.Name = "cmdOg_telef"
    Me.cmdOg_telef.NTSContextMenu = Nothing
    Me.cmdOg_telef.Size = New System.Drawing.Size(30, 24)
    Me.cmdOg_telef.TabIndex = 2
    '
    'edOg_faxpers
    '
    Me.edOg_faxpers.EditValue = ""
    Me.edOg_faxpers.Location = New System.Drawing.Point(351, 94)
    Me.edOg_faxpers.Name = "edOg_faxpers"
    Me.edOg_faxpers.NTSDbField = ""
    Me.edOg_faxpers.NTSForzaVisZoom = False
    Me.edOg_faxpers.NTSOldValue = ""
    Me.edOg_faxpers.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_faxpers.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_faxpers.Properties.AutoHeight = False
    Me.edOg_faxpers.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_faxpers.Properties.MaxLength = 65536
    Me.edOg_faxpers.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_faxpers.Size = New System.Drawing.Size(153, 20)
    Me.edOg_faxpers.TabIndex = 1
    '
    'edOg_fax
    '
    Me.edOg_fax.EditValue = ""
    Me.edOg_fax.Location = New System.Drawing.Point(65, 94)
    Me.edOg_fax.Name = "edOg_fax"
    Me.edOg_fax.NTSDbField = ""
    Me.edOg_fax.NTSForzaVisZoom = False
    Me.edOg_fax.NTSOldValue = ""
    Me.edOg_fax.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_fax.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_fax.Properties.AutoHeight = False
    Me.edOg_fax.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_fax.Properties.MaxLength = 65536
    Me.edOg_fax.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_fax.Size = New System.Drawing.Size(153, 20)
    Me.edOg_fax.TabIndex = 1
    '
    'edOg_emailpers
    '
    Me.edOg_emailpers.EditValue = ""
    Me.edOg_emailpers.Location = New System.Drawing.Point(351, 70)
    Me.edOg_emailpers.Name = "edOg_emailpers"
    Me.edOg_emailpers.NTSDbField = ""
    Me.edOg_emailpers.NTSForzaVisZoom = False
    Me.edOg_emailpers.NTSOldValue = ""
    Me.edOg_emailpers.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_emailpers.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_emailpers.Properties.AutoHeight = False
    Me.edOg_emailpers.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_emailpers.Properties.MaxLength = 65536
    Me.edOg_emailpers.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_emailpers.Size = New System.Drawing.Size(153, 20)
    Me.edOg_emailpers.TabIndex = 1
    '
    'edOg_email
    '
    Me.edOg_email.EditValue = ""
    Me.edOg_email.Location = New System.Drawing.Point(65, 70)
    Me.edOg_email.Name = "edOg_email"
    Me.edOg_email.NTSDbField = ""
    Me.edOg_email.NTSForzaVisZoom = False
    Me.edOg_email.NTSOldValue = ""
    Me.edOg_email.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_email.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_email.Properties.AutoHeight = False
    Me.edOg_email.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_email.Properties.MaxLength = 65536
    Me.edOg_email.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_email.Size = New System.Drawing.Size(153, 20)
    Me.edOg_email.TabIndex = 1
    '
    'edOg_cellpers
    '
    Me.edOg_cellpers.EditValue = ""
    Me.edOg_cellpers.Location = New System.Drawing.Point(351, 46)
    Me.edOg_cellpers.Name = "edOg_cellpers"
    Me.edOg_cellpers.NTSDbField = ""
    Me.edOg_cellpers.NTSForzaVisZoom = False
    Me.edOg_cellpers.NTSOldValue = ""
    Me.edOg_cellpers.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_cellpers.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_cellpers.Properties.AutoHeight = False
    Me.edOg_cellpers.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_cellpers.Properties.MaxLength = 65536
    Me.edOg_cellpers.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_cellpers.Size = New System.Drawing.Size(153, 20)
    Me.edOg_cellpers.TabIndex = 1
    '
    'edOg_cell
    '
    Me.edOg_cell.EditValue = ""
    Me.edOg_cell.Location = New System.Drawing.Point(65, 46)
    Me.edOg_cell.Name = "edOg_cell"
    Me.edOg_cell.NTSDbField = ""
    Me.edOg_cell.NTSForzaVisZoom = False
    Me.edOg_cell.NTSOldValue = ""
    Me.edOg_cell.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_cell.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_cell.Properties.AutoHeight = False
    Me.edOg_cell.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_cell.Properties.MaxLength = 65536
    Me.edOg_cell.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_cell.Size = New System.Drawing.Size(153, 20)
    Me.edOg_cell.TabIndex = 1
    '
    'edOg_telefpers
    '
    Me.edOg_telefpers.EditValue = ""
    Me.edOg_telefpers.Location = New System.Drawing.Point(351, 22)
    Me.edOg_telefpers.Name = "edOg_telefpers"
    Me.edOg_telefpers.NTSDbField = ""
    Me.edOg_telefpers.NTSForzaVisZoom = False
    Me.edOg_telefpers.NTSOldValue = ""
    Me.edOg_telefpers.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_telefpers.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_telefpers.Properties.AutoHeight = False
    Me.edOg_telefpers.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_telefpers.Properties.MaxLength = 65536
    Me.edOg_telefpers.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_telefpers.Size = New System.Drawing.Size(153, 20)
    Me.edOg_telefpers.TabIndex = 1
    '
    'edOg_telefint
    '
    Me.edOg_telefint.EditValue = ""
    Me.edOg_telefint.Location = New System.Drawing.Point(278, 22)
    Me.edOg_telefint.Name = "edOg_telefint"
    Me.edOg_telefint.NTSDbField = ""
    Me.edOg_telefint.NTSForzaVisZoom = False
    Me.edOg_telefint.NTSOldValue = ""
    Me.edOg_telefint.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_telefint.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_telefint.Properties.AutoHeight = False
    Me.edOg_telefint.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_telefint.Properties.MaxLength = 65536
    Me.edOg_telefint.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_telefint.Size = New System.Drawing.Size(36, 20)
    Me.edOg_telefint.TabIndex = 1
    '
    'edOg_telef
    '
    Me.edOg_telef.EditValue = ""
    Me.edOg_telef.Location = New System.Drawing.Point(65, 22)
    Me.edOg_telef.Name = "edOg_telef"
    Me.edOg_telef.NTSDbField = ""
    Me.edOg_telef.NTSForzaVisZoom = False
    Me.edOg_telef.NTSOldValue = ""
    Me.edOg_telef.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_telef.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_telef.Properties.AutoHeight = False
    Me.edOg_telef.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_telef.Properties.MaxLength = 65536
    Me.edOg_telef.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_telef.Size = New System.Drawing.Size(153, 20)
    Me.edOg_telef.TabIndex = 1
    '
    'lbFax
    '
    Me.lbFax.AutoSize = True
    Me.lbFax.BackColor = System.Drawing.Color.Transparent
    Me.lbFax.Location = New System.Drawing.Point(3, 97)
    Me.lbFax.Name = "lbFax"
    Me.lbFax.NTSDbField = ""
    Me.lbFax.Size = New System.Drawing.Size(25, 13)
    Me.lbFax.TabIndex = 0
    Me.lbFax.Text = "Fax"
    Me.lbFax.Tooltip = ""
    Me.lbFax.UseMnemonic = False
    '
    'lbEmail
    '
    Me.lbEmail.AutoSize = True
    Me.lbEmail.BackColor = System.Drawing.Color.Transparent
    Me.lbEmail.Location = New System.Drawing.Point(3, 73)
    Me.lbEmail.Name = "lbEmail"
    Me.lbEmail.NTSDbField = ""
    Me.lbEmail.Size = New System.Drawing.Size(31, 13)
    Me.lbEmail.TabIndex = 0
    Me.lbEmail.Text = "Email"
    Me.lbEmail.Tooltip = ""
    Me.lbEmail.UseMnemonic = False
    '
    'lbCellulare
    '
    Me.lbCellulare.AutoSize = True
    Me.lbCellulare.BackColor = System.Drawing.Color.Transparent
    Me.lbCellulare.Location = New System.Drawing.Point(3, 49)
    Me.lbCellulare.Name = "lbCellulare"
    Me.lbCellulare.NTSDbField = ""
    Me.lbCellulare.Size = New System.Drawing.Size(48, 13)
    Me.lbCellulare.TabIndex = 0
    Me.lbCellulare.Text = "Cellulare"
    Me.lbCellulare.Tooltip = ""
    Me.lbCellulare.UseMnemonic = False
    '
    'lbAziendale
    '
    Me.lbAziendale.AutoSize = True
    Me.lbAziendale.BackColor = System.Drawing.Color.Transparent
    Me.lbAziendale.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAziendale.Location = New System.Drawing.Point(116, 3)
    Me.lbAziendale.Name = "lbAziendale"
    Me.lbAziendale.NTSDbField = ""
    Me.lbAziendale.Size = New System.Drawing.Size(53, 13)
    Me.lbAziendale.TabIndex = 0
    Me.lbAziendale.Text = "Aziendale"
    Me.lbAziendale.Tooltip = ""
    Me.lbAziendale.UseMnemonic = False
    '
    'lbPersonale
    '
    Me.lbPersonale.AutoSize = True
    Me.lbPersonale.BackColor = System.Drawing.Color.Transparent
    Me.lbPersonale.Location = New System.Drawing.Point(398, 3)
    Me.lbPersonale.Name = "lbPersonale"
    Me.lbPersonale.NTSDbField = ""
    Me.lbPersonale.Size = New System.Drawing.Size(54, 13)
    Me.lbPersonale.TabIndex = 0
    Me.lbPersonale.Text = "Personale"
    Me.lbPersonale.Tooltip = ""
    Me.lbPersonale.UseMnemonic = False
    '
    'lbOg_telefint
    '
    Me.lbOg_telefint.AutoSize = True
    Me.lbOg_telefint.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_telefint.Location = New System.Drawing.Point(253, 25)
    Me.lbOg_telefint.Name = "lbOg_telefint"
    Me.lbOg_telefint.NTSDbField = ""
    Me.lbOg_telefint.Size = New System.Drawing.Size(25, 13)
    Me.lbOg_telefint.TabIndex = 0
    Me.lbOg_telefint.Text = "Int."
    Me.lbOg_telefint.Tooltip = ""
    Me.lbOg_telefint.UseMnemonic = False
    '
    'lbTelefono
    '
    Me.lbTelefono.AutoSize = True
    Me.lbTelefono.BackColor = System.Drawing.Color.Transparent
    Me.lbTelefono.Location = New System.Drawing.Point(3, 25)
    Me.lbTelefono.Name = "lbTelefono"
    Me.lbTelefono.NTSDbField = ""
    Me.lbTelefono.Size = New System.Drawing.Size(49, 13)
    Me.lbTelefono.TabIndex = 0
    Me.lbTelefono.Text = "Telefono"
    Me.lbTelefono.Tooltip = ""
    Me.lbTelefono.UseMnemonic = False
    '
    'edOg_mansioni
    '
    Me.edOg_mansioni.Location = New System.Drawing.Point(424, 46)
    Me.edOg_mansioni.Name = "edOg_mansioni"
    Me.edOg_mansioni.NTSDbField = ""
    Me.edOg_mansioni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_mansioni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_mansioni.Properties.MaxLength = 65536
    Me.edOg_mansioni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_mansioni.Size = New System.Drawing.Size(125, 20)
    Me.edOg_mansioni.TabIndex = 1
    '
    'edOg_descont2
    '
    Me.edOg_descont2.EditValue = ""
    Me.edOg_descont2.Location = New System.Drawing.Point(209, 22)
    Me.edOg_descont2.Name = "edOg_descont2"
    Me.edOg_descont2.NTSDbField = ""
    Me.edOg_descont2.NTSForzaVisZoom = False
    Me.edOg_descont2.NTSOldValue = ""
    Me.edOg_descont2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_descont2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_descont2.Properties.AutoHeight = False
    Me.edOg_descont2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_descont2.Properties.MaxLength = 65536
    Me.edOg_descont2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_descont2.Size = New System.Drawing.Size(157, 20)
    Me.edOg_descont2.TabIndex = 1
    '
    'edOg_descont
    '
    Me.edOg_descont.EditValue = ""
    Me.edOg_descont.Location = New System.Drawing.Point(65, 22)
    Me.edOg_descont.Name = "edOg_descont"
    Me.edOg_descont.NTSDbField = ""
    Me.edOg_descont.NTSForzaVisZoom = False
    Me.edOg_descont.NTSOldValue = ""
    Me.edOg_descont.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_descont.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_descont.Properties.AutoHeight = False
    Me.edOg_descont.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_descont.Properties.MaxLength = 65536
    Me.edOg_descont.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_descont.Size = New System.Drawing.Size(138, 20)
    Me.edOg_descont.TabIndex = 1
    '
    'edOg_codruaz
    '
    Me.edOg_codruaz.EditValue = ""
    Me.edOg_codruaz.Location = New System.Drawing.Point(65, 46)
    Me.edOg_codruaz.Name = "edOg_codruaz"
    Me.edOg_codruaz.NTSDbField = ""
    Me.edOg_codruaz.NTSForzaVisZoom = False
    Me.edOg_codruaz.NTSOldValue = ""
    Me.edOg_codruaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_codruaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_codruaz.Properties.AutoHeight = False
    Me.edOg_codruaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_codruaz.Properties.MaxLength = 65536
    Me.edOg_codruaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_codruaz.Size = New System.Drawing.Size(69, 20)
    Me.edOg_codruaz.TabIndex = 1
    '
    'edOg_titolo
    '
    Me.edOg_titolo.EditValue = ""
    Me.edOg_titolo.Location = New System.Drawing.Point(6, 22)
    Me.edOg_titolo.Name = "edOg_titolo"
    Me.edOg_titolo.NTSDbField = ""
    Me.edOg_titolo.NTSForzaVisZoom = False
    Me.edOg_titolo.NTSOldValue = ""
    Me.edOg_titolo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_titolo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_titolo.Properties.AutoHeight = False
    Me.edOg_titolo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_titolo.Properties.MaxLength = 65536
    Me.edOg_titolo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_titolo.Size = New System.Drawing.Size(53, 20)
    Me.edOg_titolo.TabIndex = 1
    '
    'lbOg_datnasc
    '
    Me.lbOg_datnasc.AutoSize = True
    Me.lbOg_datnasc.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_datnasc.Location = New System.Drawing.Point(466, 6)
    Me.lbOg_datnasc.Name = "lbOg_datnasc"
    Me.lbOg_datnasc.NTSDbField = ""
    Me.lbOg_datnasc.Size = New System.Drawing.Size(68, 13)
    Me.lbOg_datnasc.TabIndex = 0
    Me.lbOg_datnasc.Text = "Data Nascita"
    Me.lbOg_datnasc.Tooltip = ""
    Me.lbOg_datnasc.UseMnemonic = False
    '
    'lbOg_sesso
    '
    Me.lbOg_sesso.AutoSize = True
    Me.lbOg_sesso.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_sesso.Location = New System.Drawing.Point(370, 6)
    Me.lbOg_sesso.Name = "lbOg_sesso"
    Me.lbOg_sesso.NTSDbField = ""
    Me.lbOg_sesso.Size = New System.Drawing.Size(35, 13)
    Me.lbOg_sesso.TabIndex = 0
    Me.lbOg_sesso.Text = "Sesso"
    Me.lbOg_sesso.Tooltip = ""
    Me.lbOg_sesso.UseMnemonic = False
    '
    'lbOg_descont2
    '
    Me.lbOg_descont2.AutoSize = True
    Me.lbOg_descont2.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_descont2.Location = New System.Drawing.Point(206, 6)
    Me.lbOg_descont2.Name = "lbOg_descont2"
    Me.lbOg_descont2.NTSDbField = ""
    Me.lbOg_descont2.Size = New System.Drawing.Size(34, 13)
    Me.lbOg_descont2.TabIndex = 0
    Me.lbOg_descont2.Text = "Nome"
    Me.lbOg_descont2.Tooltip = ""
    Me.lbOg_descont2.UseMnemonic = False
    '
    'lbOg_descont
    '
    Me.lbOg_descont.AutoSize = True
    Me.lbOg_descont.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_descont.Location = New System.Drawing.Point(62, 6)
    Me.lbOg_descont.Name = "lbOg_descont"
    Me.lbOg_descont.NTSDbField = ""
    Me.lbOg_descont.Size = New System.Drawing.Size(52, 13)
    Me.lbOg_descont.TabIndex = 0
    Me.lbOg_descont.Text = "Cognome"
    Me.lbOg_descont.Tooltip = ""
    Me.lbOg_descont.UseMnemonic = False
    '
    'lbXx_codruaz
    '
    Me.lbXx_codruaz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codruaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codruaz.Location = New System.Drawing.Point(140, 46)
    Me.lbXx_codruaz.Name = "lbXx_codruaz"
    Me.lbXx_codruaz.NTSDbField = ""
    Me.lbXx_codruaz.Size = New System.Drawing.Size(226, 20)
    Me.lbXx_codruaz.TabIndex = 0
    Me.lbXx_codruaz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_codruaz.Tooltip = ""
    Me.lbXx_codruaz.UseMnemonic = False
    '
    'lbOg_mansioni
    '
    Me.lbOg_mansioni.AutoSize = True
    Me.lbOg_mansioni.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_mansioni.Location = New System.Drawing.Point(372, 49)
    Me.lbOg_mansioni.Name = "lbOg_mansioni"
    Me.lbOg_mansioni.NTSDbField = ""
    Me.lbOg_mansioni.Size = New System.Drawing.Size(48, 13)
    Me.lbOg_mansioni.TabIndex = 0
    Me.lbOg_mansioni.Text = "Mansioni"
    Me.lbOg_mansioni.Tooltip = ""
    Me.lbOg_mansioni.UseMnemonic = False
    '
    'lbOg_codruaz
    '
    Me.lbOg_codruaz.AutoSize = True
    Me.lbOg_codruaz.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_codruaz.Location = New System.Drawing.Point(3, 49)
    Me.lbOg_codruaz.Name = "lbOg_codruaz"
    Me.lbOg_codruaz.NTSDbField = ""
    Me.lbOg_codruaz.Size = New System.Drawing.Size(34, 13)
    Me.lbOg_codruaz.TabIndex = 0
    Me.lbOg_codruaz.Text = "Ruolo"
    Me.lbOg_codruaz.Tooltip = ""
    Me.lbOg_codruaz.UseMnemonic = False
    '
    'lbOg_titolo
    '
    Me.lbOg_titolo.AutoSize = True
    Me.lbOg_titolo.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_titolo.Location = New System.Drawing.Point(7, 6)
    Me.lbOg_titolo.Name = "lbOg_titolo"
    Me.lbOg_titolo.NTSDbField = ""
    Me.lbOg_titolo.Size = New System.Drawing.Size(33, 13)
    Me.lbOg_titolo.TabIndex = 0
    Me.lbOg_titolo.Text = "Titolo"
    Me.lbOg_titolo.Tooltip = ""
    Me.lbOg_titolo.UseMnemonic = False
    '
    'fmSocialNetwork
    '
    Me.fmSocialNetwork.AllowDrop = True
    Me.fmSocialNetwork.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSocialNetwork.Appearance.Options.UseBackColor = True
    Me.fmSocialNetwork.Controls.Add(Me.cmdOg_twitteruser)
    Me.fmSocialNetwork.Controls.Add(Me.edOg_twitteruser)
    Me.fmSocialNetwork.Controls.Add(Me.cmdOg_fbuser)
    Me.fmSocialNetwork.Controls.Add(Me.edOg_fbuser)
    Me.fmSocialNetwork.Controls.Add(Me.lbOg_twitteruser)
    Me.fmSocialNetwork.Controls.Add(Me.lbOg_fbuser)
    Me.fmSocialNetwork.Location = New System.Drawing.Point(0, 375)
    Me.fmSocialNetwork.Name = "fmSocialNetwork"
    Me.fmSocialNetwork.Size = New System.Drawing.Size(557, 46)
    Me.fmSocialNetwork.TabIndex = 18
    Me.fmSocialNetwork.Text = "Social Network"
    '
    'cmdOg_twitteruser
    '
    Me.cmdOg_twitteruser.Image = CType(resources.GetObject("cmdOg_twitteruser.Image"), System.Drawing.Image)
    Me.cmdOg_twitteruser.ImagePath = ""
    Me.cmdOg_twitteruser.ImageText = ""
    Me.cmdOg_twitteruser.Location = New System.Drawing.Point(507, 20)
    Me.cmdOg_twitteruser.Name = "cmdOg_twitteruser"
    Me.cmdOg_twitteruser.NTSContextMenu = Nothing
    Me.cmdOg_twitteruser.Size = New System.Drawing.Size(30, 24)
    Me.cmdOg_twitteruser.TabIndex = 6
    '
    'edOg_twitteruser
    '
    Me.edOg_twitteruser.EditValue = ""
    Me.edOg_twitteruser.Location = New System.Drawing.Point(351, 22)
    Me.edOg_twitteruser.Name = "edOg_twitteruser"
    Me.edOg_twitteruser.NTSDbField = ""
    Me.edOg_twitteruser.NTSForzaVisZoom = False
    Me.edOg_twitteruser.NTSOldValue = ""
    Me.edOg_twitteruser.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_twitteruser.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_twitteruser.Properties.AutoHeight = False
    Me.edOg_twitteruser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_twitteruser.Properties.MaxLength = 65536
    Me.edOg_twitteruser.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_twitteruser.Size = New System.Drawing.Size(153, 20)
    Me.edOg_twitteruser.TabIndex = 5
    '
    'cmdOg_fbuser
    '
    Me.cmdOg_fbuser.Image = CType(resources.GetObject("cmdOg_fbuser.Image"), System.Drawing.Image)
    Me.cmdOg_fbuser.ImagePath = ""
    Me.cmdOg_fbuser.ImageText = ""
    Me.cmdOg_fbuser.Location = New System.Drawing.Point(221, 20)
    Me.cmdOg_fbuser.Name = "cmdOg_fbuser"
    Me.cmdOg_fbuser.NTSContextMenu = Nothing
    Me.cmdOg_fbuser.Size = New System.Drawing.Size(30, 24)
    Me.cmdOg_fbuser.TabIndex = 4
    '
    'edOg_fbuser
    '
    Me.edOg_fbuser.EditValue = ""
    Me.edOg_fbuser.Location = New System.Drawing.Point(65, 22)
    Me.edOg_fbuser.Name = "edOg_fbuser"
    Me.edOg_fbuser.NTSDbField = ""
    Me.edOg_fbuser.NTSForzaVisZoom = False
    Me.edOg_fbuser.NTSOldValue = ""
    Me.edOg_fbuser.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_fbuser.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_fbuser.Properties.AutoHeight = False
    Me.edOg_fbuser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_fbuser.Properties.MaxLength = 65536
    Me.edOg_fbuser.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_fbuser.Size = New System.Drawing.Size(153, 20)
    Me.edOg_fbuser.TabIndex = 3
    '
    'lbOg_twitteruser
    '
    Me.lbOg_twitteruser.AutoSize = True
    Me.lbOg_twitteruser.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_twitteruser.Location = New System.Drawing.Point(304, 25)
    Me.lbOg_twitteruser.Name = "lbOg_twitteruser"
    Me.lbOg_twitteruser.NTSDbField = ""
    Me.lbOg_twitteruser.Size = New System.Drawing.Size(41, 13)
    Me.lbOg_twitteruser.TabIndex = 0
    Me.lbOg_twitteruser.Text = "Twitter"
    Me.lbOg_twitteruser.Tooltip = ""
    Me.lbOg_twitteruser.UseMnemonic = False
    '
    'lbOg_fbuser
    '
    Me.lbOg_fbuser.AutoSize = True
    Me.lbOg_fbuser.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_fbuser.Location = New System.Drawing.Point(3, 25)
    Me.lbOg_fbuser.Name = "lbOg_fbuser"
    Me.lbOg_fbuser.NTSDbField = ""
    Me.lbOg_fbuser.Size = New System.Drawing.Size(53, 13)
    Me.lbOg_fbuser.TabIndex = 0
    Me.lbOg_fbuser.Text = "Facebook"
    Me.lbOg_fbuser.Tooltip = ""
    Me.lbOg_fbuser.UseMnemonic = False
    '
    'fmContatti
    '
    Me.fmContatti.AllowDrop = True
    Me.fmContatti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmContatti.Appearance.Options.UseBackColor = True
    Me.fmContatti.Controls.Add(Me.lbOg_progr)
    Me.fmContatti.Controls.Add(Me.lbID)
    Me.fmContatti.Controls.Add(Me.cmdCollegaAContatto)
    Me.fmContatti.Controls.Add(Me.cmdCreaNuovoContatto)
    Me.fmContatti.Controls.Add(Me.cmdProssimo)
    Me.fmContatti.Controls.Add(Me.cmdCollega)
    Me.fmContatti.Controls.Add(Me.lbProponiContatto)
    Me.fmContatti.Controls.Add(Me.lbOg_codcont)
    Me.fmContatti.Controls.Add(Me.lbOg_codcope)
    Me.fmContatti.Controls.Add(Me.edOg_codcont)
    Me.fmContatti.Controls.Add(Me.lbXx_codcont)
    Me.fmContatti.Controls.Add(Me.edOg_codcope)
    Me.fmContatti.Controls.Add(Me.lbXx_codcope)
    Me.fmContatti.Controls.Add(Me.lbOg_contatto)
    Me.fmContatti.Controls.Add(Me.lbOg_codcage)
    Me.fmContatti.Controls.Add(Me.edOg_contatto)
    Me.fmContatti.Controls.Add(Me.edOg_codcage)
    Me.fmContatti.Controls.Add(Me.lbXx_codcage)
    Me.fmContatti.Controls.Add(Me.edOg_coperat)
    Me.fmContatti.Controls.Add(Me.edOg_coddest)
    Me.fmContatti.Controls.Add(Me.lbXx_coddest)
    Me.fmContatti.Controls.Add(Me.lbOg_coperat)
    Me.fmContatti.Controls.Add(Me.lbOg_coddest)
    Me.fmContatti.Location = New System.Drawing.Point(0, 421)
    Me.fmContatti.Name = "fmContatti"
    Me.fmContatti.Size = New System.Drawing.Size(557, 95)
    Me.fmContatti.TabIndex = 0
    Me.fmContatti.Text = "Collegamenti con Business"
    '
    'lbOg_progr
    '
    Me.lbOg_progr.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_progr.Location = New System.Drawing.Point(513, 71)
    Me.lbOg_progr.Name = "lbOg_progr"
    Me.lbOg_progr.NTSDbField = ""
    Me.lbOg_progr.Size = New System.Drawing.Size(35, 19)
    Me.lbOg_progr.TabIndex = 29
    Me.lbOg_progr.Text = "0"
    Me.lbOg_progr.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    Me.lbOg_progr.Tooltip = ""
    Me.lbOg_progr.UseMnemonic = False
    '
    'lbID
    '
    Me.lbID.AutoSize = True
    Me.lbID.BackColor = System.Drawing.Color.Transparent
    Me.lbID.Location = New System.Drawing.Point(496, 74)
    Me.lbID.Name = "lbID"
    Me.lbID.NTSDbField = ""
    Me.lbID.Size = New System.Drawing.Size(18, 13)
    Me.lbID.TabIndex = 29
    Me.lbID.Text = "ID"
    Me.lbID.Tooltip = ""
    Me.lbID.UseMnemonic = False
    '
    'cmdCollegaAContatto
    '
    Me.cmdCollegaAContatto.ImagePath = ""
    Me.cmdCollegaAContatto.ImageText = ""
    Me.cmdCollegaAContatto.Location = New System.Drawing.Point(199, 69)
    Me.cmdCollegaAContatto.Name = "cmdCollegaAContatto"
    Me.cmdCollegaAContatto.NTSContextMenu = Nothing
    Me.cmdCollegaAContatto.Size = New System.Drawing.Size(67, 24)
    Me.cmdCollegaAContatto.TabIndex = 28
    Me.cmdCollegaAContatto.Text = "Collega"
    Me.cmdCollegaAContatto.ToolTipTitle = "Permette di scegliere di collegare la persona ad un contatto esistente"
    '
    'cmdCreaNuovoContatto
    '
    Me.cmdCreaNuovoContatto.ImagePath = ""
    Me.cmdCreaNuovoContatto.ImageText = ""
    Me.cmdCreaNuovoContatto.Location = New System.Drawing.Point(131, 69)
    Me.cmdCreaNuovoContatto.Name = "cmdCreaNuovoContatto"
    Me.cmdCreaNuovoContatto.NTSContextMenu = Nothing
    Me.cmdCreaNuovoContatto.Size = New System.Drawing.Size(67, 24)
    Me.cmdCreaNuovoContatto.TabIndex = 27
    Me.cmdCreaNuovoContatto.Text = "Crea nuovo"
    Me.cmdCreaNuovoContatto.ToolTipTitle = "Scollega la persona dal contatto esistente e ne crea un nuovo"
    '
    'cmdProssimo
    '
    Me.cmdProssimo.Image = CType(resources.GetObject("cmdProssimo.Image"), System.Drawing.Image)
    Me.cmdProssimo.ImagePath = ""
    Me.cmdProssimo.ImageText = ""
    Me.cmdProssimo.Location = New System.Drawing.Point(534, 1)
    Me.cmdProssimo.Name = "cmdProssimo"
    Me.cmdProssimo.NTSContextMenu = Nothing
    Me.cmdProssimo.Size = New System.Drawing.Size(18, 18)
    Me.cmdProssimo.TabIndex = 26
    Me.cmdProssimo.Visible = False
    '
    'cmdCollega
    '
    Me.cmdCollega.Image = CType(resources.GetObject("cmdCollega.Image"), System.Drawing.Image)
    Me.cmdCollega.ImagePath = ""
    Me.cmdCollega.ImageText = ""
    Me.cmdCollega.Location = New System.Drawing.Point(513, 1)
    Me.cmdCollega.Name = "cmdCollega"
    Me.cmdCollega.NTSContextMenu = Nothing
    Me.cmdCollega.Size = New System.Drawing.Size(18, 18)
    Me.cmdCollega.TabIndex = 26
    Me.cmdCollega.Visible = False
    '
    'lbProponiContatto
    '
    Me.lbProponiContatto.BackColor = System.Drawing.Color.Transparent
    Me.lbProponiContatto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbProponiContatto.Location = New System.Drawing.Point(1, 1)
    Me.lbProponiContatto.Name = "lbProponiContatto"
    Me.lbProponiContatto.NTSDbField = ""
    Me.lbProponiContatto.Size = New System.Drawing.Size(510, 17)
    Me.lbProponiContatto.TabIndex = 25
    Me.lbProponiContatto.Text = "Potrebbe essere ...?"
    Me.lbProponiContatto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbProponiContatto.Tooltip = ""
    Me.lbProponiContatto.UseMnemonic = False
    Me.lbProponiContatto.Visible = False
    '
    'lbOg_codcont
    '
    Me.lbOg_codcont.AutoSize = True
    Me.lbOg_codcont.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_codcont.Location = New System.Drawing.Point(274, 50)
    Me.lbOg_codcont.Name = "lbOg_codcont"
    Me.lbOg_codcont.NTSDbField = ""
    Me.lbOg_codcont.Size = New System.Drawing.Size(42, 13)
    Me.lbOg_codcont.TabIndex = 23
    Me.lbOg_codcont.Text = "Risorsa"
    Me.lbOg_codcont.Tooltip = ""
    Me.lbOg_codcont.UseMnemonic = False
    '
    'lbOg_codcope
    '
    Me.lbOg_codcope.AutoSize = True
    Me.lbOg_codcope.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_codcope.Location = New System.Drawing.Point(2, 50)
    Me.lbOg_codcope.Name = "lbOg_codcope"
    Me.lbOg_codcope.NTSDbField = ""
    Me.lbOg_codcope.Size = New System.Drawing.Size(45, 13)
    Me.lbOg_codcope.TabIndex = 23
    Me.lbOg_codcope.Text = "Operaio"
    Me.lbOg_codcope.Tooltip = ""
    Me.lbOg_codcope.UseMnemonic = False
    '
    'edOg_codcont
    '
    Me.edOg_codcont.EditValue = "0"
    Me.edOg_codcont.Location = New System.Drawing.Point(344, 47)
    Me.edOg_codcont.Name = "edOg_codcont"
    Me.edOg_codcont.NTSDbField = ""
    Me.edOg_codcont.NTSFormat = "0"
    Me.edOg_codcont.NTSForzaVisZoom = False
    Me.edOg_codcont.NTSOldValue = ""
    Me.edOg_codcont.Properties.Appearance.Options.UseTextOptions = True
    Me.edOg_codcont.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edOg_codcont.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_codcont.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_codcont.Properties.AutoHeight = False
    Me.edOg_codcont.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_codcont.Properties.MaxLength = 65536
    Me.edOg_codcont.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_codcont.Size = New System.Drawing.Size(60, 20)
    Me.edOg_codcont.TabIndex = 24
    '
    'lbXx_codcont
    '
    Me.lbXx_codcont.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codcont.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codcont.Location = New System.Drawing.Point(410, 47)
    Me.lbXx_codcont.Name = "lbXx_codcont"
    Me.lbXx_codcont.NTSDbField = ""
    Me.lbXx_codcont.Size = New System.Drawing.Size(138, 21)
    Me.lbXx_codcont.TabIndex = 22
    Me.lbXx_codcont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_codcont.Tooltip = ""
    Me.lbXx_codcont.UseMnemonic = False
    '
    'edOg_codcope
    '
    Me.edOg_codcope.EditValue = "0"
    Me.edOg_codcope.Location = New System.Drawing.Point(65, 47)
    Me.edOg_codcope.Name = "edOg_codcope"
    Me.edOg_codcope.NTSDbField = ""
    Me.edOg_codcope.NTSFormat = "0"
    Me.edOg_codcope.NTSForzaVisZoom = False
    Me.edOg_codcope.NTSOldValue = ""
    Me.edOg_codcope.Properties.Appearance.Options.UseTextOptions = True
    Me.edOg_codcope.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edOg_codcope.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_codcope.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_codcope.Properties.AutoHeight = False
    Me.edOg_codcope.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_codcope.Properties.MaxLength = 65536
    Me.edOg_codcope.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_codcope.Size = New System.Drawing.Size(60, 20)
    Me.edOg_codcope.TabIndex = 24
    '
    'lbXx_codcope
    '
    Me.lbXx_codcope.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codcope.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codcope.Location = New System.Drawing.Point(131, 47)
    Me.lbXx_codcope.Name = "lbXx_codcope"
    Me.lbXx_codcope.NTSDbField = ""
    Me.lbXx_codcope.Size = New System.Drawing.Size(135, 21)
    Me.lbXx_codcope.TabIndex = 22
    Me.lbXx_codcope.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_codcope.Tooltip = ""
    Me.lbXx_codcope.UseMnemonic = False
    '
    'lbOg_contatto
    '
    Me.lbOg_contatto.AutoSize = True
    Me.lbOg_contatto.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_contatto.Location = New System.Drawing.Point(2, 74)
    Me.lbOg_contatto.Name = "lbOg_contatto"
    Me.lbOg_contatto.NTSDbField = ""
    Me.lbOg_contatto.Size = New System.Drawing.Size(50, 13)
    Me.lbOg_contatto.TabIndex = 20
    Me.lbOg_contatto.Text = "Contatto"
    Me.lbOg_contatto.Tooltip = ""
    Me.lbOg_contatto.UseMnemonic = False
    '
    'lbOg_codcage
    '
    Me.lbOg_codcage.AutoSize = True
    Me.lbOg_codcage.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_codcage.Location = New System.Drawing.Point(2, 26)
    Me.lbOg_codcage.Name = "lbOg_codcage"
    Me.lbOg_codcage.NTSDbField = ""
    Me.lbOg_codcage.Size = New System.Drawing.Size(42, 13)
    Me.lbOg_codcage.TabIndex = 20
    Me.lbOg_codcage.Text = "Agente"
    Me.lbOg_codcage.Tooltip = ""
    Me.lbOg_codcage.UseMnemonic = False
    '
    'edOg_contatto
    '
    Me.edOg_contatto.EditValue = "0"
    Me.edOg_contatto.Enabled = False
    Me.edOg_contatto.Location = New System.Drawing.Point(65, 71)
    Me.edOg_contatto.Name = "edOg_contatto"
    Me.edOg_contatto.NTSDbField = ""
    Me.edOg_contatto.NTSFormat = "0"
    Me.edOg_contatto.NTSForzaVisZoom = False
    Me.edOg_contatto.NTSOldValue = ""
    Me.edOg_contatto.Properties.Appearance.Options.UseTextOptions = True
    Me.edOg_contatto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edOg_contatto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_contatto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_contatto.Properties.AutoHeight = False
    Me.edOg_contatto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_contatto.Properties.MaxLength = 65536
    Me.edOg_contatto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_contatto.Size = New System.Drawing.Size(60, 20)
    Me.edOg_contatto.TabIndex = 21
    '
    'edOg_codcage
    '
    Me.edOg_codcage.EditValue = "0"
    Me.edOg_codcage.Location = New System.Drawing.Point(65, 23)
    Me.edOg_codcage.Name = "edOg_codcage"
    Me.edOg_codcage.NTSDbField = ""
    Me.edOg_codcage.NTSFormat = "0"
    Me.edOg_codcage.NTSForzaVisZoom = False
    Me.edOg_codcage.NTSOldValue = ""
    Me.edOg_codcage.Properties.Appearance.Options.UseTextOptions = True
    Me.edOg_codcage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edOg_codcage.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_codcage.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_codcage.Properties.AutoHeight = False
    Me.edOg_codcage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_codcage.Properties.MaxLength = 65536
    Me.edOg_codcage.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_codcage.Size = New System.Drawing.Size(60, 20)
    Me.edOg_codcage.TabIndex = 21
    '
    'lbXx_codcage
    '
    Me.lbXx_codcage.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codcage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codcage.Location = New System.Drawing.Point(131, 23)
    Me.lbXx_codcage.Name = "lbXx_codcage"
    Me.lbXx_codcage.NTSDbField = ""
    Me.lbXx_codcage.Size = New System.Drawing.Size(135, 21)
    Me.lbXx_codcage.TabIndex = 19
    Me.lbXx_codcage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_codcage.Tooltip = ""
    Me.lbXx_codcage.UseMnemonic = False
    '
    'edOg_coperat
    '
    Me.edOg_coperat.EditValue = ""
    Me.edOg_coperat.Location = New System.Drawing.Point(344, 71)
    Me.edOg_coperat.Name = "edOg_coperat"
    Me.edOg_coperat.NTSDbField = ""
    Me.edOg_coperat.NTSForzaVisZoom = False
    Me.edOg_coperat.NTSOldValue = ""
    Me.edOg_coperat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_coperat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_coperat.Properties.AutoHeight = False
    Me.edOg_coperat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_coperat.Properties.MaxLength = 65536
    Me.edOg_coperat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_coperat.Size = New System.Drawing.Size(148, 20)
    Me.edOg_coperat.TabIndex = 18
    '
    'edOg_coddest
    '
    Me.edOg_coddest.EditValue = "0"
    Me.edOg_coddest.Location = New System.Drawing.Point(344, 23)
    Me.edOg_coddest.Name = "edOg_coddest"
    Me.edOg_coddest.NTSDbField = ""
    Me.edOg_coddest.NTSFormat = "0"
    Me.edOg_coddest.NTSForzaVisZoom = False
    Me.edOg_coddest.NTSOldValue = ""
    Me.edOg_coddest.Properties.Appearance.Options.UseTextOptions = True
    Me.edOg_coddest.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edOg_coddest.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edOg_coddest.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edOg_coddest.Properties.AutoHeight = False
    Me.edOg_coddest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edOg_coddest.Properties.MaxLength = 65536
    Me.edOg_coddest.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edOg_coddest.Size = New System.Drawing.Size(60, 20)
    Me.edOg_coddest.TabIndex = 17
    Me.edOg_coddest.Visible = False
    '
    'lbXx_coddest
    '
    Me.lbXx_coddest.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_coddest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_coddest.Location = New System.Drawing.Point(410, 23)
    Me.lbXx_coddest.Name = "lbXx_coddest"
    Me.lbXx_coddest.NTSDbField = ""
    Me.lbXx_coddest.Size = New System.Drawing.Size(138, 20)
    Me.lbXx_coddest.TabIndex = 0
    Me.lbXx_coddest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_coddest.Tooltip = ""
    Me.lbXx_coddest.UseMnemonic = False
    Me.lbXx_coddest.Visible = False
    '
    'lbOg_coperat
    '
    Me.lbOg_coperat.AutoSize = True
    Me.lbOg_coperat.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_coperat.Location = New System.Drawing.Point(274, 74)
    Me.lbOg_coperat.Name = "lbOg_coperat"
    Me.lbOg_coperat.NTSDbField = ""
    Me.lbOg_coperat.Size = New System.Drawing.Size(60, 13)
    Me.lbOg_coperat.TabIndex = 0
    Me.lbOg_coperat.Text = "Utente Bus"
    Me.lbOg_coperat.Tooltip = ""
    Me.lbOg_coperat.UseMnemonic = False
    '
    'lbOg_coddest
    '
    Me.lbOg_coddest.AutoSize = True
    Me.lbOg_coddest.BackColor = System.Drawing.Color.Transparent
    Me.lbOg_coddest.Location = New System.Drawing.Point(274, 27)
    Me.lbOg_coddest.Name = "lbOg_coddest"
    Me.lbOg_coddest.NTSDbField = ""
    Me.lbOg_coddest.Size = New System.Drawing.Size(68, 13)
    Me.lbOg_coddest.TabIndex = 0
    Me.lbOg_coddest.Text = "Destinazione"
    Me.lbOg_coddest.Tooltip = ""
    Me.lbOg_coddest.UseMnemonic = False
    Me.lbOg_coddest.Visible = False
    '
    'FRM__ORGA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(719, 546)
    Me.Controls.Add(Me.pnAll)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRM__ORGA"
    Me.NTSLastControlFocussed = Me.grOrga
    Me.Text = "ORGANIZZAZIONE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grOrga, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvOrga, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAll, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAll.ResumeLayout(False)
    CType(Me.pnPersone, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPersone.ResumeLayout(False)
    CType(Me.pnRicerca, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRicerca.ResumeLayout(False)
    CType(Me.edRicerca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDati, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDati.ResumeLayout(False)
    CType(Me.pnDatiPersona, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDatiPersona.ResumeLayout(False)
    Me.pnDatiPersona.PerformLayout()
    CType(Me.fmStato, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmStato.ResumeLayout(False)
    Me.fmStato.PerformLayout()
    CType(Me.ckOg_old.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_dtiniz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_codstco.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_referente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_dtfine.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmResidenza, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmResidenza.ResumeLayout(False)
    Me.fmResidenza.PerformLayout()
    CType(Me.edOg_indir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_cap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_citta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_prov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_stato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_rep.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_divis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_sede.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_datnasc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbOg_sesso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmRecapiti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmRecapiti.ResumeLayout(False)
    Me.fmRecapiti.PerformLayout()
    CType(Me.edOg_skypeuser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbOg_usaem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_faxpers.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_fax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_emailpers.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_email.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_cellpers.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_cell.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_telefpers.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_telefint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_telef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_mansioni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_descont2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_descont.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_codruaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_titolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmSocialNetwork, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSocialNetwork.ResumeLayout(False)
    Me.fmSocialNetwork.PerformLayout()
    CType(Me.edOg_twitteruser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_fbuser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmContatti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmContatti.ResumeLayout(False)
    Me.fmContatti.PerformLayout()
    CType(Me.edOg_codcont.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_codcope.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_contatto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_codcage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_coperat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edOg_coddest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__ORGA", "BE__ORGA", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128644657492968750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleOrga = CType(oTmp, CLE__ORGA)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__ORGA", strRemoteServer, strRemotePort)
    AddHandler oCleOrga.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleOrga.Init(oApp, oScript, oMenu.oCleComm, "TABMAGA", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
        tlbEmail.GlyphPath = (oApp.ChildImageDir & "\email.gif")
        tlbSkype.GlyphPath = (oApp.ChildImageDir & "\skype.png")

        tlbGeneraGuest.GlyphPath = (oApp.ChildImageDir & "\RETAIL_altricrediti.gif")
        tlbRuoli.GlyphPath = (oApp.ChildImageDir & "\anagraex.gif")
        tlbSalvaNuovo.GlyphPath = (oApp.ChildImageDir & "\savenew.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      Dim dttSesso As New DataTable()
      dttSesso.Columns.Add("cod", GetType(String))
      dttSesso.Columns.Add("val", GetType(String))
      dttSesso.Rows.Add(New Object() {"M", "Maschio"})
      dttSesso.Rows.Add(New Object() {"F", "Femmina"})
      dttSesso.AcceptChanges()
      cbOg_sesso.DataSource = dttSesso
      cbOg_sesso.DisplayMember = "val"
      cbOg_sesso.ValueMember = "cod"


      Dim dttTipoSend As New DataTable()
      dttTipoSend.Columns.Add("cod", GetType(String))
      dttTipoSend.Columns.Add("val", GetType(String))
      dttTipoSend.Rows.Add(New Object() {"S", "E-mail Internet"})
      dttTipoSend.Rows.Add(New Object() {"X", "Fax service Win XP/2003"})
      'dttTipoSend.Rows.Add(New Object() {"Y", "Fax service Win 2000 (locale)"}) non più supportato
      dttTipoSend.Rows.Add(New Object() {"N", "Microsoft Fax (mapi)"})
      dttTipoSend.Rows.Add(New Object() {"Z", "Zetafax MAPI"})
      dttTipoSend.Rows.Add(New Object() {"H", "HylaFAX"})
      dttTipoSend.AcceptChanges()
      cbOg_usaem.DataSource = dttTipoSend
      cbOg_usaem.DisplayMember = "val"
      cbOg_usaem.ValueMember = "cod"


      grvOrga.NTSSetParam(oMenu, "ORGANIZZAZIONE")
      og_coddest.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128387466998936000, "Cod. destinaz."), tabdestdiv)
      edOg_coddest.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130457613915168817, "Cod. destinaz."), tabdestdiv)
      og_sede.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387466999092000, "Sede"), 30, False)
      edOg_sede.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914618459, "Sede"), 30, False)
      og_divis.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387466999248000, "Divisione"), 30, False)
      edOg_divis.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914598446, "Divisione"), 30, False)
      og_rep.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387466999404000, "Reparto"), 30, False)
      edOg_rep.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914578432, "Reparto"), 30, False)
      og_codruaz.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128387466999560000, "Cod. ruolo az."), tabruaz, False)
      edOg_codruaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130457613914918655, "Cod. ruolo az."), tabruaz, False)
      og_telef.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387466999872000, "Tel. az."), 18, True)
      edOg_telef.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914818590, "Tel. az."), 18, True)
      edOg_telefpers.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914798577, "Tel. personale"), 18, True)
      og_telefint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130233757357916714, "Tel. diretto/interno"), 18, True)
      edOg_telefint.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914808581, "Tel. diretto/interno"), 18, True)
      og_fax.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387467000028000, "Fax az."), 18, True)
      edOg_faxpers.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914738537, "Fax Personale"), 18, True)
      edOg_fax.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914748542, "Fax az."), 18, True)
      og_email.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387467000184000, "e-mail az."), 100, True)
      edOg_emailpers.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914758550, "e-mail personale"), 100, True)
      edOg_email.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914768555, "e-mail az."), 100, True)
      og_dtiniz.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128387467000340000, "Data inizio val."), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edOg_dtiniz.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914388306, "Data inizio validità"), True)
      og_dtfine.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128387467000496000, "Data fine val."), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edOg_dtfine.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914448345, "Data fine validità"), True)
      og_cell.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387467000652000, "Tel. cellulare"), 18, True)
      edOg_cellpers.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914778564, "Cellulare personale"), 18, True)
      edOg_cell.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914788572, "Cellulare az."), 18, True)
      og_descont.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387467000808000, "Cognome"), 30, False)
      edOg_descont.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914908647, "Cognome"), 30, False)
      og_descont2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387467000964000, "Nome"), 20, True)
      edOg_descont2.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914898642, "Nome"), 20, True)
      og_titolo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387467001120000, "Titolo"), 8, True)
      edOg_titolo.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914928660, "Titolo"), 8, True)
      og_indir.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387467001276000, "Indir. abitaz."), 70, True)
      edOg_indir.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914488371, "Indir. abitaz."), 70, True)
      og_cap.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387467001432000, "Cap abitaz."), 9, True)
      edOg_cap.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914508384, "Cap abitaz."), 9, True)
      og_citta.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387467001588000, "Città abitaz."), 50, True)
      edOg_citta.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914528398, "Città abitaz."), 50, True)
      og_prov.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387467001744000, "Prov. abitaz."), 2, True)
      edOg_prov.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914548411, "Prov. abitaz."), 2, True)
      og_stato.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128387467001900000, "Stato abitaz."), tabstat, True)
      edOg_stato.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130458298009955438, "Stato abitaz."), tabstat, True)
      og_datnasc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128387467002056000, "Data nascita"), True, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edOg_datnasc.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914638472, "Data nascita"), True)
      og_sesso.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128387467002212000, "Sesso"), dttSesso, "val", "cod")
      cbOg_sesso.NTSSetParam(oApp.Tr(Me, 130457613914648476, "Sesso"))
      og_coperat.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387467002524000, "Operatore Bus"), 20, True)
      edOg_coperat.NTSSetParam(oMenu, oApp.Tr(Me, 130457613915158813, "Operatore Bus"), 20, True)
      og_mansioni.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128387467002680000, "Mansioni"), 0, True)
      edOg_mansioni.NTSSetParam(oMenu, oApp.Tr(Me, 130457613914888634, "Mansioni"), 0, True)
      og_codcope.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128387467002836000, "Cod. operaio"), tabcope)
      edOg_codcope.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130457613915078760, "Cod. operaio"), tabcope)
      og_codcage.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128387467002992000, "Cod agente"), tabcage)
      edOg_codcage.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130457613915138800, "Cod agente"), tabcage)
      og_usaem.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128641249749375000, "Modalità corrispondenza"), dttTipoSend, "val", "cod")
      cbOg_usaem.NTSSetParam(oApp.Tr(Me, 130457613914658485, "Modalità corrispondenza"))
      og_codcont.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128387466999716000, "Cod. risorsa"), tabcont)
      edOg_codcont.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130458263265428835, "Cod. risorsa"), tabcont)
      og_old.NTSSetParamCHK(oMenu, oApp.Tr(Me, 130233757616601373, "Non operativo/obsoleto"), "S", "N")
      ckOg_old.NTSSetParam(oMenu, oApp.Tr(Me, 130458296958494998, "Non operativo/obsoleto"), "S", "N")

      xx_coddest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130457613913857960, "Descr. destin."), 0, True)
      xx_codruaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130457613913907991, "Descr. ruolo"), 0, True)
      xx_codcont.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130457613913928004, "Descr. risorsa / contatto"), 0, True)
      xx_stato.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130457613914078104, "Descr. stato"), 0, True)
      xx_codcope.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130457613914148148, "Descr. operaio"), 0, True)
      xx_codcage.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130457613914168165, "Descr. agente"), 0, True)
      xx_descrizione.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130457613914208188, "Descrizione"), 0, True)
      og_coperat.NTSSetParamZoom("ZOOMOPERAT")
      edOg_coperat.NTSSetParamZoom("ZOOMOPERAT")

      og_codlead.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128387467002368000, "Cod. lead"), tableads)
      og_progr.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128387466998624000, "Codice"), "0", 9, 0, 999999999)

      edOg_codstco.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130457613914408319, "Status Commerciale"), tabstco)

      edOg_referente.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130457613914428332, "Referente"), tabcontatti)
      edOg_contatto.NTSSetParam(oMenu, oApp.Tr(Me, 130457613915118787, "Contatto"), "0")
      edOg_twitteruser.NTSSetParam(oMenu, oApp.Tr(Me, 130457613915028726, "Twitter"), 30)
      edOg_fbuser.NTSSetParam(oMenu, oApp.Tr(Me, 130457613915048739, "Facebook"), 30)
      edOg_skypeuser.NTSSetParam(oMenu, oApp.Tr(Me, 130816124874365743, "Skype"), 30)
      edRicerca.NTSSetParam(oMenu, oApp.Tr(Me, 130457613915328951, "Ricerca"), 0)

      grvOrga.NTSAllowInsert = False

      grvOrga.AddColumnBackColor("backcolor_row")

      edOg_descont.NTSSetRichiesto()
      edOg_codruaz.NTSSetRichiesto()

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

  Public Overridable Sub BindControls()
    Try
      '-------------------------------------------------
      'se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edOg_dtiniz.NTSDbField = "ORGANIG.og_dtiniz"
      edOg_codstco.NTSDbField = "ORGANIG.og_codstco"
      edOg_referente.NTSDbField = "ORGANIG.og_referente"
      lbXx_referente.NTSDbField = "ORGANIG.xx_referente"
      edOg_dtfine.NTSDbField = "ORGANIG.og_dtfine"
      lbXx_codstco.NTSDbField = "ORGANIG.xx_codstco"
      edOg_indir.NTSDbField = "ORGANIG.og_indir"
      edOg_cap.NTSDbField = "ORGANIG.og_cap"
      edOg_citta.NTSDbField = "ORGANIG.og_citta"
      edOg_prov.NTSDbField = "ORGANIG.og_prov"
      lbXx_stato.NTSDbField = "ORGANIG.xx_stato"
      edOg_stato.NTSDbField = "ORGANIG.og_stato"
      edOg_rep.NTSDbField = "ORGANIG.og_rep"
      edOg_divis.NTSDbField = "ORGANIG.og_divis"
      edOg_sede.NTSDbField = "ORGANIG.og_sede"
      edOg_datnasc.NTSDbField = "ORGANIG.og_datnasc"
      cbOg_sesso.NTSDbField = "ORGANIG.og_sesso"
      cbOg_usaem.NTSDbField = "ORGANIG.og_usaem"
      edOg_faxpers.NTSDbField = "ORGANIG.og_faxpers"
      edOg_fax.NTSDbField = "ORGANIG.og_fax"
      edOg_emailpers.NTSDbField = "ORGANIG.og_emailpers"
      edOg_email.NTSDbField = "ORGANIG.og_email"
      edOg_cellpers.NTSDbField = "ORGANIG.og_cellpers"
      edOg_cell.NTSDbField = "ORGANIG.og_cell"
      edOg_skypeuser.NTSDbField = "ORGANIG.og_skypeuser"
      edOg_telefpers.NTSDbField = "ORGANIG.og_telefpers"
      edOg_telefint.NTSDbField = "ORGANIG.og_telefint"
      edOg_telef.NTSDbField = "ORGANIG.og_telef"
      edOg_mansioni.NTSDbField = "ORGANIG.og_mansioni"
      edOg_descont2.NTSDbField = "ORGANIG.og_descont2"
      edOg_descont.NTSDbField = "ORGANIG.og_descont"
      edOg_codruaz.NTSDbField = "ORGANIG.og_codruaz"
      edOg_titolo.NTSDbField = "ORGANIG.og_titolo"
      lbXx_codruaz.NTSDbField = "ORGANIG.xx_codruaz"
      edOg_twitteruser.NTSDbField = "ORGANIG.og_twitteruser"
      edOg_fbuser.NTSDbField = "ORGANIG.og_fbuser"
      edOg_codcope.NTSDbField = "ORGANIG.og_codcope"
      lbXx_codcope.NTSDbField = "ORGANIG.xx_codcope"
      edOg_contatto.NTSDbField = "ORGANIG.og_contatto"
      edOg_codcage.NTSDbField = "ORGANIG.og_codcage"
      lbXx_codcage.NTSDbField = "ORGANIG.xx_codcage"
      edOg_coperat.NTSDbField = "ORGANIG.og_coperat"
      edOg_coddest.NTSDbField = "ORGANIG.og_coddest"
      lbXx_coddest.NTSDbField = "ORGANIG.xx_coddest"
      edOg_codcont.NTSDbField = "ORGANIG.og_codcont"
      lbXx_codcont.NTSDbField = "ORGANIG.xx_codcont"
      ckOg_old.NTSText.NTSDbField = "ORGANIG.og_old"
      lbOg_progr.NTSDbField = "ORGANIG.og_progr"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcOrga, Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    If Not Me.IsMyThrowRemoteEvent() Then Return
    Try
      Select Case e.TipoEvento
        Case "AbilitaFunzioni" : VerificaFunzioniVelociAbilitate()
        Case "AbilitaContatti" : AggiornaTestoContatti()
        Case "AbilitaFunzioniContatto" : AbilitaFunzioniContatto()
        Case "APRIWMAI_:"
          Dim oPar As New CLE__CLDP
          oPar.ctlPar1 = oCleOrga.dttWmai
          oPar.bPar1 = True
          oPar.bPar2 = True
          oPar.bPar3 = True
          oPar.bPar4 = True
          oPar.bPar5 = True
          oMenu.RunChild("NTSInformatica", "FRMEMWMAI", oApp.Tr(Me, 130420316649659527, "Invio e-mail"), DittaCorrente, "", "BNEMWMAI", oPar, "", True, True)
        Case "ChiediConto\Lead"
          Dim frmCont As FRM__CONT = CType(NTSNewFormModal("FRM__CONT"), FRM__CONT)
          If Not frmCont.Init(oMenu, New CLE__CLDP, DittaCorrente) Then Return
          frmCont.InitEntiry(oCleOrga)
          frmCont.dtrOrga = grvOrga.NTSGetCurrentDataRow

          If frmCont.ShowDialog = Windows.Forms.DialogResult.OK Then
            e.InputValue = frmCont.ValoreRitorno()
            e.RetValue = ThMsg.RETVALUE_OK
          Else
            e.RetValue = ThMsg.RETVALUE_CANCEL
          End If
        Case Else : MyBase.GestisciEventiEntity(sender, e)
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub NascondiColonneGriglia()
    Try
      If oMenu.GetSettingBus("BS--ORGA", "RECENT", ".", "ColonneNascoste", "N", ".", "N") = "N" Then
        For Each oColumn As DevExpress.XtraGrid.Columns.GridColumn In grvOrga.Columns
          oColumn.Visible = (oColumn Is xx_descrizione)
        Next
        oMenu.SaveSettingBus("BS--ORGA", "RECENT", ".", "ColonneNascoste", "S", ".", ".S.", "...", "...")
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub AbilitaCampiInMaschera()
    Try
      If dsOrga.Tables("ORGANIG").Rows.Count = 0 Then
        pnDatiPersona.Enabled = False
        pnRicerca.Enabled = False
      Else
        GctlSetVisEnab(pnDatiPersona, False)
        GctlSetVisEnab(pnRicerca, False)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub DescrizioneAziendaInRubrica()
    Try
      'Per mostrare un pò di contesto la descrizione la faccio vedere sempre
      ' If Not oCleOrga.bRubricaCompleta Then Return

      fmContatti.Text = strDescrContatti

      If grvOrga.NTSGetCurrentDataRow Is Nothing Then Return

      With grvOrga.NTSGetCurrentDataRow
        If NTSCInt(!og_conto) = -1 AndAlso NTSCInt(!og_codlead) = -1 Then Return
        'Compone la descrizione in base al tipo di lead
        If NTSCInt(!og_conto) = 0 AndAlso NTSCInt(!og_codlead) = 0 Then
          fmContatti.Text &= oApp.Tr(Me, 130674487634089079, ": membro dell'organizzazione interna")
        ElseIf NTSCInt(!og_conto) <> 0 AndAlso bOrganizzazioneUnica = False Then
          fmContatti.Text &= ": " & NTSCStr(!og_conto) & " " & NTSCStr(!xx_conto)
        ElseIf NTSCInt(!og_codlead) <> 0 Then
          If Not dsOrga.Tables("ORGANIG").Columns.Contains("xx_lead") Then
            dsOrga.Tables("ORGANIG").Columns.Add("xx_lead", GetType(String))
            Dim strDescr As String = ""
            oMenu.ValCodiceDb(NTSCStr(!og_codlead), DittaCorrente, "LEADS", "N", strDescr)
            !xx_lead = strDescr
          End If
          fmContatti.Text &= ": " & NTSCStr(!og_codlead) & " " & NTSCStr(!xx_lead)
        End If
      End With
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub AdattaInterfacciaRubrica()
    Try
      If Not oCleOrga.bRubricaCompleta Then Return

      'Se non c'è nessuna riga selezionata i dati non devono essere gestiti 
      If grvOrga.NTSGetCurrentDataRow Is Nothing Then pnDatiPersona.Visible = False : Return

      pnDatiPersona.Visible = True

      With grvOrga.NTSGetCurrentDataRow
        'Se la riga è associato ad un conto allora mostra la destinazione
        If NTSCInt(!og_conto) = 0 Then
          lbOg_coddest.Visible = False
          edOg_coddest.Visible = False
          lbXx_coddest.Visible = False
        Else
          GctlSetVisible(lbOg_coddest)
          GctlSetVisible(edOg_coddest)
          GctlSetVisible(lbXx_coddest)
        End If

        'Se la riga è associato ad un conto allora mostra la destinazione
        If Not oCleOrga.bModWss Then
          If NTSCInt(!og_conto) = 0 AndAlso NTSCInt(!og_codlead) = 0 Then
            GctlSetVisible(edOg_coperat)
            GctlSetVisible(lbOg_coperat)
          Else
            edOg_coperat.Visible = False
            lbOg_coperat.Visible = False
          End If
        Else
          GctlSetVisible(edOg_coperat)
          GctlSetVisible(lbOg_coperat)
        End If

        'Se la configurazione e-mail deve essere visibile
        If NTSCInt(!og_conto) = 0 AndAlso NTSCInt(!og_codlead) = 0 Then
          GctlSetVisible(tlbEmail)
          tlbGeneraGuest.Visible = False
        Else
          tlbEmail.Visible = False
          If oCleOrga.bModBFP Then
            GctlSetVisible(tlbGeneraGuest)
            GctlSetVisible(edOg_coperat)
            GctlSetVisible(lbOg_coperat)
          Else
            tlbGeneraGuest.Visible = False
          End If
        End If
      End With
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub CaricaOrganizzazioni(ByVal bRiposizionaSuPrimaRiga As Boolean)
    Dim lPos As Integer
    Try
      oCleOrga.CaricaOrganizzazioni(tlbInterna.Down, tlbClienti.Down, tlbFornitori.Down, tlbLeads.Down)

      If Not bRiposizionaSuPrimaRiga Then lPos = dcOrga.position
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      dsOrga = oCleOrga.dsShared
      If bRiposizionaSuPrimaRiga Then
        dcOrga = New BindingSource
        edRicerca.Text = ""
      End If
      dcOrga = New BindingSource
      dcOrga.DataSource = dsOrga.Tables("ORGANIG")
      dsOrga.AcceptChanges()
      BindControls()

      If Not bRiposizionaSuPrimaRiga Then dcOrga.position = lPos

      grOrga.DataSource = dcOrga

      AbilitaCampiInMaschera()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


#Region "Eventi di Form"
  Public Overridable Sub FRM__ORGA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      'oCallParams.Ditta = ditta di be__clie/be__anaz in analisi
      'oCallParams.strPar1 = strTipoConto (C= cliente, F= fornitore, '' = tabanaz)
      'oCallParams.ctlPar1 = dsShared di be__clie  o be__anaz (l'importante è che dentro ci sia la tabella ORGANIG)
      'oCallParams.ctlPar2 = dttOrganigDeleted di be__clie
      'oCallParams.dPar1 = conto di be__clie in analisi
      'oCallParams.dPar2 = lead di be__clie in analisi
      ' Se bPar1 = True -> apri Rubrica Completa e posizionati sul progressivo indicato da dPar1
      oCleOrga.bGesttabcont = CBool(oMenu.GetSettingBus("OPZIONI", ".", ".", "GestTabcont", "0", " ", "0"))

      oCleOrga.bModCRM = CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCRM)
      oCleOrga.bModWss = CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupWSS)
      oCleOrga.bModBFP = CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupBGE)
      If oCleOrga.bModCRM Then oCleOrga.bIsUserCrm = oMenu.IsCrmUser(DittaCorrente)

      oCleOrga.strDittaCorrente = DittaCorrente

      If oMenu.GetSettingBus("BS--ORGA", "OPZIONI", ".", "GrigliaClassica", "0", ".", "0") <> "0" AndAlso bOrganizzazioneUnica = False Then
        Me.MaximizeBox = True
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        grvOrga.OptionsSelection.EnableAppearanceFocusedRow = True
        grvOrga.OptionsView.ShowColumnHeaders = True
        grvOrga.OptionsView.ShowIndicator = True
      End If
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      'Se gli ho passato dei parametri vuol dire che sono in modalità chiamato da un altro programma (BN__CLIE, BN__ANAZ, ecc...)
      'Quindi la gestione dei dati (caricamento\salvataggio\cancellazione) verrà fatta direttamente dal programma chiamante.
      If oCallParams IsNot Nothing AndAlso oCallParams.bPar3 = False Then
        If oCallParams.strParam.StartsWith("NUOV;", StringComparison.CurrentCultureIgnoreCase) OrElse _
           oCallParams.strParam.StartsWith("APRI;", StringComparison.CurrentCultureIgnoreCase) Then
          AttivaRubrica()
        Else
          AttivaOrganizzazione()
        End If
      Else
        AttivaRubrica()
      End If


      strDescrContatti = fmContatti.Text

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      dsOrga = oCleOrga.dsShared
      dcOrga.DataSource = dsOrga.Tables("ORGANIG")
      dsOrga.AcceptChanges()

      grOrga.DataSource = dcOrga

      AbilitaCampiInMaschera()

      BindControls()
      '-------------------------------------------
      'faccio vedere l'operatore solo se chiamato da bs--anaz
      'oCallParams.strPar1 = strTipoConto (C= cliente, F= fornitore, '' = tabanaz)
      'se MODULO WSS (WebSoftService) ATTIVO --> og_coperat visibile anche se chiamato da anagrafica cli/forn
      If oCleOrga.bRubricaCompleta = False Then 'Nella rubrica completa questo controllo va fatto al cambio di riga!
        If Not oCleOrga.bModWss Then
          If oCallParams.strPar1 = "" Then
            'og_coperat.Visible = True
            edOg_coperat.Visible = True
            lbOg_coperat.Visible = True
          Else
            'og_coperat.Visible = False
            edOg_coperat.Visible = False
            lbOg_coperat.Visible = False
          End If
        Else
          edOg_coperat.Visible = True
          lbOg_coperat.Visible = True
        End If
      End If

      tlbRuoli.Visible = CBool(oMenu.GetSettingBus("OPZIONI", ".", ".", "AttivaRuoliMultipli", "0", ".", "0"))
      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      If oCleOrga.bRubricaCompleta Then
        GctlTipoDoc = "T" 'Tutte le organizzazioni, così si ha una configurazione specifica per questo stato
      Else
        GctlTipoDoc = oCallParams.strPar1
      End If
      GctlSetRoules()

      If bOrganizzazioneUnica AndAlso edOg_codruaz.Text.Trim = "" Then GctlApplicaDefaultValue() 'Nuova riga da gestione lead

      NascondiColonneGriglia()

      If oCleOrga.bGesttabcont Then
        GctlSetVisEnab(lbOg_codcont, True)
        GctlSetVisEnab(edOg_codcont, True)
        GctlSetVisEnab(lbXx_codcont, True)
      Else
        lbOg_codcont.Visible = False
        edOg_codcont.Visible = False
        lbXx_codcont.Visible = False
      End If

      'è Configurato il conto ma non il lead, allora posso modificare la destinazione, altrimenti il lead comprende già la destinazione
      If oCleOrga.bRubricaCompleta = False Then 'Se non ho passato dei dati questo controllo va fatto al cambio di riga!
        If oCallParams.dPar1 <> 0 AndAlso Not bOrganizzazioneUnica Then
          GctlSetVisEnab(lbOg_coddest, True)
          GctlSetVisEnab(edOg_coddest, True)
          GctlSetVisEnab(lbXx_coddest, True)
        End If

        If oCleOrga.bModBFP Then
          If oCleOrga.lContoCf <> 0 OrElse oCleOrga.lLead <> 0 Then
            GctlSetVisible(tlbGeneraGuest)
          Else
            tlbGeneraGuest.Visible = False
          End If
        Else
          tlbGeneraGuest.Visible = False
        End If

        'Nella rubrica completa questo controllo va fatto al cambio di riga!
        If oCallParams.strPar1.Trim <> "" Then tlbEmail.Visible = False
      Else
        'Rubrica completa, ho scelto su che record posizionarmi
        If oCallParams IsNot Nothing Then
          If oCallParams.dPar1 <> 0 Then
            'Li devo rendere tutti visibili, altrimenti potrebber non esserci il record che interessa a me
            tlbInterna.Down = True
            tlbFornitori.Down = True
            tlbClienti.Down = True
            tlbLeads.Down = True
            dcOrga.Position = dcOrga.Find("og_progr", oCallParams.dPar1)
          ElseIf oCallParams.strParam.StartsWith("NUOV;", StringComparison.CurrentCultureIgnoreCase) Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
          ElseIf oCallParams.strParam.StartsWith("APRI;", StringComparison.CurrentCultureIgnoreCase) Then
            dcOrga.Position = dcOrga.Find("og_progr", oCallParams.strParam.Replace("APRI;", ""))
          End If
        End If
      End If

      VerificaFunzioniVelociAbilitate()
      AbilitaFunzioniContatto()
      AggiornaTestoContatti()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__ORGA_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__ORGA_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcOrga.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub FRM__ORGA_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      If e.KeyCode = Keys.F1 And e.Alt Then
        If edOg_mansioni.Focused Then
          edOg_mansioni.Text = NTSBigBox(edOg_mansioni.Text, oApp.Tr(Me, 130505070081080374, "Mansioni"), 0, True, False, False)
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      If oCleOrga.bRubricaCompleta Then
        If Not tlbClienti.Down AndAlso Not tlbFornitori.Down AndAlso Not tlbLeads.Down AndAlso Not tlbInterna.Down Then
          oApp.MsgBoxErr(oApp.Tr(Me, 131054476048894355, "Non è possibile aggiungere una nuova organizzazione in quanto non si è scelto che dati visualizzare."))
          Return
        End If
      End If

      If Not Salva() Then Return
      RemoveHandler grvOrga.NTSBeforeRowUpdate, AddressOf grvOrga_NTSBeforeRowUpdate
      RemoveHandler grvOrga.NTSFocusedRowChanged, AddressOf grvOrga_NTSFocusedRowChanged

      edRicerca.Text = ""
      edRicerca.Enabled = False
      cmdRicerca.Enabled = False

      oCleOrga.OrgaNuovo()
      edOg_titolo.Focus()
      dcOrga.MoveLast()

      AddHandler grvOrga.NTSFocusedRowChanged, AddressOf grvOrga_NTSFocusedRowChanged
      AddHandler grvOrga.NTSBeforeRowUpdate, AddressOf grvOrga_NTSBeforeRowUpdate

      AbilitaCampiInMaschera()

      GctlApplicaDefaultValue()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalvaNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalvaNuovo.ItemClick
    Try
      Salva()
      If oCallParams.bPar2 = False Then 'Vuole dire che il programma ha salvato
        oCallParams.bPar4 = True
        Me.Close()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim dtrCanc As DataRow = Nothing
    Dim strOperat As String = ""

    Try
      Select Case oApp.MsgBoxInfoYesNoCancel_DefYes(oApp.Tr(Me, 130459146795225267, "Invece di cancellare il record preferisci renderlo obsoleto?" & vbCrLf & vbCrLf & _
                                                                                    " Si = Rendi obsoleto il record senza cancellarlo." & vbCrLf & _
                                                                                    " No = Cancella il record." & vbCrLf & _
                                                                                    " Annulla = Annulla l'operazione"))
        Case Windows.Forms.DialogResult.Yes
          'Porta a scadenza
          edOg_dtfine.NTSTextDB = Now.ToShortDateString
          ckOg_old.Checked = True
        Case Windows.Forms.DialogResult.No
          '----------------------------------------------------------------------------------------------------------
          '--- SOLO SE CANCELLO FISICAMENTE IL RECORD, ELIMINO APPUNTAMENTI E ATTIVITA' COLLEGATI
          '----------------------------------------------------------------------------------------------------------
          If Not grvOrga.NTSGetCurrentDataRow Is Nothing Then
            strOperat = NTSCStr(grvOrga.NTSGetCurrentDataRow!og_coperat)
          End If
          '----------------------------------------------------------------------------------------------------------
          'Cancella il record
          If Not grvOrga.NTSDeleteRigaCorrente(dcOrga, False, dtrCanc) Then Return

          If Not oCleOrga.dttOrganigDeleted Is Nothing Then oCleOrga.dttOrganigDeleted.ImportRow(dtrCanc) 'memorizzo il record: mi servirà in fase di salvataggio per impostare og_serverdeleted
        Case Else : Return
      End Select

      oCleOrga.OrgaSalva(True)
      '--------------------------------------------------------------------------------------------------------------
      '--- SOLO SE CANCELLO FISICAMENTE IL RECORD, ELIMINO APPUNTAMENTI E ATTIVITA' COLLEGATI
      '--------------------------------------------------------------------------------------------------------------
      oCleOrga.EliminaAttivitàDaOperatoreOrganig(strOperat)
      '--------------------------------------------------------------------------------------------------------------
      If bOrganizzazioneUnica Then ocallParams.bPar2 = False : Me.Close() : Return

      AbilitaCampiInMaschera()
      AggiornaTestoContatti()
      edRicerca.Enabled = True
      cmdRicerca.Enabled = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvOrga.NTSRipristinaRigaCorrenteBefore(dcOrga, True) Then Return
      oCleOrga.OrgaRipristina(dcOrga.Position, dcOrga.Filter)
      grvOrga.NTSRipristinaRigaCorrenteAfter()

      AbilitaCampiInMaschera()
      AggiornaTestoContatti()

      edRicerca.Enabled = True
      cmdRicerca.Enabled = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Dim ds As DataSet = Nothing
    Dim strNomeTab As String = ""
    Try
      'zoom standard
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      'oCleOrga.lContoCf = 0 = chianato da tabanaz, altrimenti chiamato da bn__clie

      If edOg_coddest.Focused Then
        'non posso fare lo zoom standard, visto che potrei selez. una destinaz. diversa appena inserita e non ancora salvata ...
        If oCleOrga.lContoCf <> 0 Then
          strNomeTab = "DESTDIV"
        Else
          strNomeTab = "ANAZUL"
        End If
        If dsOrga.Tables.Contains(strNomeTab) Then
          ds = New DataSet
          ds.Tables.Add(dsOrga.Tables(strNomeTab).Clone)
          ds.Tables(0).TableName = "DESTDIV"
          For i = 0 To dsOrga.Tables(strNomeTab).Rows.Count - 1
            ds.Tables(0).ImportRow(dsOrga.Tables(strNomeTab).Rows(i))
          Next

          If oCleOrga.lContoCf = 0 Then
            'rinomino le colonne per farle uguali a quelle dello zoom
            For i = 0 To ds.Tables("DESTDIV").Columns.Count - 1
              If ds.Tables("DESTDIV").Columns(i).ColumnName.ToLower.Substring(0, 2) = "ul" Then
                ds.Tables("DESTDIV").Columns(i).ColumnName = "dd_" & ds.Tables("DESTDIV").Columns(i).ColumnName.Substring(3)
              End If
            Next
          End If
          ds.Tables(0).AcceptChanges()
        End If

        NTSZOOM.strIn = NTSCStr(edOg_coddest.Text)
        oParam.lContoCF = NTSCInt(grvOrga.NTSGetCurrentDataRow!og_conto)   'passo il conto cliente/fornitore solo per dire che non è zoom su anazul
        oParam.oParam = ds
        NTSZOOM.ZoomStrIn("ZOOMDESTDIV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(edOg_coddest.Text) Then edOg_coddest.NTSTextDB = NTSZOOM.strIn
      ElseIf edOg_codstco.Focused Then
        oParam.strTipo = "C"
        NTSZOOM.ZoomStrIn("ZOOMTABSTCO", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(edOg_codstco.Text) Then edOg_codstco.NTSTextDB = NTSZOOM.strIn
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
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

  Public Overridable Sub tlbRuoli_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRuoli.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If grvOrga.NTSGetCurrentDataRow Is Nothing Then Return

      oPar.dPar1 = NTSCInt(grvOrga.NTSGetCurrentDataRow!og_progr)
      oMenu.RunChild("NTSInformatica", "FRM__ORRU", "", DittaCorrente, "", "BN__ORRU", oPar, "", True, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbEmail_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEmail.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If Not Salva() Then Return

      If grvOrga.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130160398608725583, "Selezionare una riga."))
        Return
      End If

      oPar.strPar1 = NTSCStr(grvOrga.NTSGetCurrentDataRow!og_coperat)
      If oPar.strPar1 = "" Then
        grvOrga.FocusedColumn = grvOrga.Columns("og_coperat")
        oApp.MsgBoxErr(oApp.Tr(Me, 130160400256587217, "Funzione disponibile solo sulle righe che hanno indicato il Nome Operatore Business."))
        Return
      End If

      oMenu.RunChild("NTSInformatica", "FRMEMCSER", "", DittaCorrente, "", "BNEMCSER", oPar, "", True, True)

      'Alla chiusura ricarica i dati delle e-mail, altrimenti uscendo e salvando il programma sostituiva i dati
      oCleOrga.AggiornaDatiEmail(NTSCInt(grvOrga.NTSGetCurrentDataRow!og_progr))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbSkype_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSkype.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If oCleOrga.lContoCf = 0 And oCleOrga.lLead > 0 Then
        oPar.strPar1 = "L"
        oPar.dPar1 = oCleOrga.lLead
      Else
        oPar.strPar1 = "C"
        oPar.dPar1 = oCleOrga.lContoCf
      End If

      oMenu.RunChild("NTSInformatica", "FRM__SKYP", "", DittaCorrente, "", "BN__SKYP", oPar, "", False, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbGeneraGuest_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGeneraGuest.ItemClick
    Try
      If grvOrga.NTSGetCurrentDataRow Is Nothing Then Return

      Me.ValidaLastControl()

      If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 130540565836187722, "Procedere con la generazione di un nuovo utente 'Guest'?")) = Windows.Forms.DialogResult.Yes Then
        oCleOrga.GeneraUtenteGuest(grvOrga.NTSGetCurrentDataRow)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbInterna_DownChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Try
      oMenu.SaveSettingBus("BS__ORGA", "RECENT", ".", "tlbInterna", NTSCStr(IIf(tlbInterna.Down, -1, 0)), ".", "NS.", "...", "...")

      CaricaOrganizzazioni(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub tlbClienti_DownChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Try
      oMenu.SaveSettingBus("BS__ORGA", "RECENT", ".", "tlbClienti", NTSCStr(IIf(tlbClienti.Down, -1, 0)), ".", "NS.", "...", "...")

      CaricaOrganizzazioni(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub tlbFornitori_DownChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Try
      oMenu.SaveSettingBus("BS__ORGA", "RECENT", ".", "tlbFornitori", NTSCStr(IIf(tlbFornitori.Down, -1, 0)), ".", "NS.", "...", "...")

      CaricaOrganizzazioni(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub tlbLeads_DownChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Try
      oMenu.SaveSettingBus("BS__ORGA", "RECENT", ".", "tlbLeads", NTSCStr(IIf(tlbLeads.Down, -1, 0)), ".", "NS.", "...", "...")

      CaricaOrganizzazioni(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi Griglia"
  Public Overridable Sub grvOrga_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvOrga.NTSBeforeRowUpdate
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

  Public Overridable Sub grvOrga_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvOrga.NTSFocusedRowChanged
    Try
      If oCleOrga.bGesttabcont AndAlso NTSCInt(grvOrga.GetFocusedRowCellValue(og_codcont).ToString.Trim) <> 0 Then
        edOg_telef.Enabled = False
        edOg_fax.Enabled = False
        edOg_email.Enabled = False
        edOg_cell.Enabled = False
        edOg_descont.Enabled = False
        edOg_descont2.Enabled = False
        edOg_titolo.Enabled = False
        edOg_indir.Enabled = False
        edOg_cap.Enabled = False
        edOg_citta.Enabled = False
        edOg_prov.Enabled = False
        edOg_stato.Enabled = False
        edOg_datnasc.Enabled = False
        cbOg_sesso.Enabled = False
      Else
        GctlSetVisEnab(edOg_telef, False)
        GctlSetVisEnab(edOg_fax, False)
        GctlSetVisEnab(edOg_email, False)
        GctlSetVisEnab(edOg_cell, False)
        GctlSetVisEnab(edOg_descont, False)
        GctlSetVisEnab(edOg_descont2, False)
        GctlSetVisEnab(edOg_titolo, False)
        GctlSetVisEnab(edOg_indir, False)
        GctlSetVisEnab(edOg_cap, False)
        GctlSetVisEnab(edOg_citta, False)
        GctlSetVisEnab(edOg_prov, False)
        GctlSetVisEnab(edOg_stato, False)
        GctlSetVisEnab(edOg_datnasc, False)
        GctlSetVisEnab(cbOg_sesso, False)
      End If

      VerificaFunzioniVelociAbilitate()
      AbilitaFunzioniContatto()

      AggiornaTestoContatti()
      AdattaInterfacciaRubrica()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region


  Public Overridable Sub AttivaOrganizzazione()
    Try
      oCleOrga.InitEx(CType(oCallParams.ctlPar1, DataSet), CType(oCallParams.ctlPar2, DataTable), DittaCorrente, NTSCInt(oCallParams.dPar1), NTSCInt(oCallParams.dPar2))
      oCleOrga.lCoddest = NTSCInt(oCallParams.dPar3)

      If oCallParams.bPar1 Then
        'Organizzazione unica, nascone la possibilità di selezionare l'organizzazione.
        Me.MinimumSize = New Size(Me.MinimumSize.Width - pnPersone.Width, Me.Height)
        Me.Size = Me.MinimumSize
        bOrganizzazioneUnica = True
        pnPersone.Visible = False
        tlbNuovo.Visible = False
        tlbSalvaNuovo.Visible = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub AttivaRubrica()
    Try
      tlbInterna.Down = oMenu.GetSettingBus("BS__ORGA", "RECENT", ".", "tlbInterna", "0", ".", "0") = "-1"
      tlbFornitori.Down = oMenu.GetSettingBus("BS__ORGA", "RECENT", ".", "tlbFornitori", "-1", ".", "-1") = "-1"
      tlbClienti.Down = oMenu.GetSettingBus("BS__ORGA", "RECENT", ".", "tlbClienti", "-1", ".", "-1") = "-1"
      tlbLeads.Down = oMenu.GetSettingBus("BS__ORGA", "RECENT", ".", "tlbLeads", "-1", ".", "-1") = "-1"

      tlbInterna.Visible = True
      tlbFornitori.Visible = True
      tlbClienti.Visible = True
      If oCleOrga.bModCRM Then
        tlbLeads.Visible = True
      Else
        tlbLeads.Down = False
      End If

      'Li aggancio dopo così non scatenano l'evento di aggiornamento ad ogni applicazione del recent.
      AddHandler tlbInterna.DownChanged, AddressOf tlbInterna_DownChanged
      AddHandler tlbFornitori.DownChanged, AddressOf tlbFornitori_DownChanged
      AddHandler tlbClienti.DownChanged, AddressOf tlbClienti_DownChanged
      AddHandler tlbLeads.DownChanged, AddressOf tlbLeads_DownChanged

      oCleOrga.CaricaOrganizzazioni(tlbInterna.Down, tlbClienti.Down, tlbFornitori.Down, tlbLeads.Down)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      grvOrga.NTSAllowInsert = True
      dRes = grvOrga.NTSSalvaRigaCorrente(dcOrga, oCleOrga.RecordIsChanged, False)
      grvOrga.NTSAllowInsert = False
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If bOrganizzazioneUnica Then
            If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 130468672442704120, "Confermi il salvataggio?")) <> Windows.Forms.DialogResult.Yes Then Return False
          End If

          If Not oCleOrga.OrgaSalva(False) Then Return False
          If oCleOrga.bRubricaCompleta Then
            'In modalità completa ricarico tutti i dati così da avere i dati sempre aggiornati dopo ogni salvataggio
            CaricaOrganizzazioni(False)
          Else
            oCallParams.bPar2 = False
          End If
          edRicerca.Enabled = True
          cmdRicerca.Enabled = True
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleOrga.OrgaRipristina(dcOrga.Position, dcOrga.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      AggiornaTestoContatti()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

#Region "Eventi"
  '--- Gestione tasti apri e nuovo zoom veloce destinazione
  Public Overridable Sub og_coddest_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles og_coddest.NTSZoomGest, edOg_coddest.NTSZoomGest
    Dim oTmp As New Control
    Dim oCallParamsTmp As New CLE__CLDP
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim nCodDestTmp As Integer = 0
    Dim nTipo As Integer = 0
    Dim frmdesg As FRM__DESG = Nothing
    Try
      Me.ValidaLastControl()
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo

      If oCleOrga.bRubricaCompleta = False AndAlso oCallParams.strNomProg = "BN__ANAZ" Then

        If e.TipoEvento = "OPEN" Then
          If IsNumeric(dsOrga.Tables("ORGANIG").Rows(dcOrga.Position)!og_coddest) Then
            nCodDestTmp = NTSCInt(dsOrga.Tables("ORGANIG").Rows(dcOrga.Position)!og_coddest)
          End If
          oCallParamsTmp.strParam = "APRI;" & nCodDestTmp
        Else
          nCodDestTmp = 0
          oCallParamsTmp.strParam = "NUOV;0"
        End If

        oTmp.Text = NTSCStr(nCodDestTmp)

        Dim dsAnaz As DataSet
        dsAnaz = CType(oCallParams.ctlPar1, DataSet)
        Dim oCleAnaz As CLE__ANAZ
        oCleAnaz = CType(oCallParams.ctlPar3, CLE__ANAZ)

        oCallParamsTmp.strPar1 = "Altri indirizzi"
        nTipo = 0
        If nCodDestTmp > 0 Then
          For i = 0 To dsAnaz.Tables("ANAZUL").Rows.Count - 1
            If nCodDestTmp = NTSCInt(dsAnaz.Tables("ANAZUL").Rows(i)!ul_coddest) Then
              Select Case NTSCInt(dsAnaz.Tables("ANAZUL").Rows(i)!ul_coddest)
                Case oCleAnaz.lDestdomf
                  oCallParamsTmp.strPar1 = "Domicilio fiscale per provv. amministr."
                  nTipo = oCleAnaz.lDestdomf
                Case oCleAnaz.lDestsedel
                  oCallParamsTmp.strPar1 = "Resid./Domic. fisc./Sede legale in Italia"
                  nTipo = oCleAnaz.lDestsedel
                Case oCleAnaz.lDestresan
                  oCallParamsTmp.strPar1 = "Residenza/Sede legale estera"
                  nTipo = oCleAnaz.lDestresan
                Case oCleAnaz.lDestcorr
                  oCallParamsTmp.strPar1 = "Luogo di esercizio attiv. all'estero"
                  nTipo = oCleAnaz.lDestcorr
              End Select
            End If
          Next
        End If

        '-------------------------------
        'clono latabella perchè negli altri indirizzi non devo far vedere gli indirizzi collegati alle destinazioni particolari 
        ds.Tables.Add(dsAnaz.Tables("ANAZUL").Clone())
        Select Case nTipo
          Case oCleAnaz.lDestdomf, oCleAnaz.lDestsedel, oCleAnaz.lDestresan, oCleAnaz.lDestcorr
            For i = 0 To dsAnaz.Tables("ANAZUL").Rows.Count - 1
              If NTSCInt(dsAnaz.Tables("ANAZUL").Rows(i)!ul_coddest) = nTipo Then
                ds.Tables("ANAZUL").ImportRow(dsAnaz.Tables("ANAZUL").Rows(i))
                dsAnaz.Tables("ANAZUL").Rows(i).Delete()
              End If
            Next
          Case Else
            For i = 0 To dsAnaz.Tables("ANAZUL").Rows.Count - 1
              If NTSCInt(dsAnaz.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestdomf And _
                  NTSCInt(dsAnaz.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestsedel And _
                  NTSCInt(dsAnaz.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestresan And _
                  NTSCInt(dsAnaz.Tables("ANAZUL").Rows(i)!ul_coddest) <> oCleAnaz.lDestcorr Then
                ds.Tables("ANAZUL").ImportRow(dsAnaz.Tables("ANAZUL").Rows(i))
                dsAnaz.Tables("ANAZUL").Rows(i).Delete()
              End If
            Next
        End Select
        dsAnaz.Tables("ANAZUL").AcceptChanges()

        frmdesg = CType(NTSNewFormModal("FRM__DESG"), FRM__DESG)
        frmdesg.Init(oMenu, oCallParamsTmp, DittaCorrente)
        frmdesg.InitEntity(oCleAnaz, ds, nTipo)
        If NTSCInt(dsAnaz.Tables("TABANAZ").Rows(0)!tb_azcodanag) <> 0 Then frmdesg.tlbCancella.Enabled = False
        frmdesg.ShowDialog()

        '-------------------------------
        'riacquisisco gli indirizzi
        For i = 0 To ds.Tables("ANAZUL").Rows.Count - 1
          If ds.Tables("ANAZUL").Rows(i).RowState <> DataRowState.Deleted Then
            If NTSCInt(ds.Tables("ANAZUL").Rows(i)!ul_coddest) > 0 Then
              dsAnaz.Tables("ANAZUL").ImportRow(ds.Tables("ANAZUL").Rows(i))
            Else
              ds.Tables("ANAZUL").Rows(i).Delete()
            End If
          End If
        Next
        ds.Tables.Clear()
        dsAnaz.Tables("ANAZUL").AcceptChanges()
        oCleAnaz.bHasChanges = True
        'senza la riga sotto se cambio solo le destinazioni diverse non salva
        If dsAnaz.Tables("TABANAZ").Rows(0).RowState = DataRowState.Unchanged Then dsAnaz.Tables("TABANAZ").Rows(0).SetModified()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmdesg Is Nothing Then frmdesg.Dispose()
      frmdesg = Nothing
    End Try
  End Sub
  '---

  Public Overridable Sub cmdOg_telef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOg_telef.Click
    Try
      AvviaSkype(edOg_telef.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdOg_telefpers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOg_telefpers.Click
    Try
      AvviaSkype(edOg_telefpers.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdOg_cell_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOg_cell.Click
    Try
      AvviaSkype(edOg_cell.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdOg_cellpers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOg_cellpers.Click
    Try
      AvviaSkype(edOg_cellpers.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdOg_email_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOg_email.Click
    Try
      ApriEmail(edOg_email.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdOg_emailpers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOg_emailpers.Click
    Try
      ApriEmail(edOg_emailpers.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdOg_fbuser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOg_fbuser.Click
    Try
      ApriFacebook(edOg_fbuser.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdOg_twitteruser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOg_twitteruser.Click
    Try
      ApriTwitter(edOg_twitteruser.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdOg_skypeuser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOg_skypeuser.Click
    Try
      AvviaSkype(edOg_skypeuser.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edRicerca_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edRicerca.Validated
    Try
      cmdRicerca_Click(Nothing, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicerca.Click
    Try
      If edRicerca.Text.Trim = "" Then
        If dcOrga.Filter <> "" Then dcOrga.Filter = ""
        Return
      End If

      Dim strRicerca As String = CStrSQL("%" & edRicerca.Text & "%")

      Dim strFiltro As String = "xx_descrizione LIKE " & strRicerca & _
                                " OR og_telef LIKE " & strRicerca & _
                                " OR og_fax LIKE " & strRicerca & _
                                " OR og_email LIKE " & strRicerca & _
                                " OR og_cell LIKE " & strRicerca & _
                                " OR og_telefpers LIKE " & strRicerca & _
                                " OR og_faxpers LIKE " & strRicerca & _
                                " OR og_emailpers LIKE " & strRicerca & _
                                " OR og_cellpers LIKE " & strRicerca & _
                                " OR og_fbuser LIKE " & strRicerca & _
                                " OR og_twitteruser LIKE " & strRicerca

      If dsOrga.Tables("ORGANIG").Columns.Contains("xx_conto") Then strFiltro &= " OR xx_conto LIKE " & strRicerca
      If dsOrga.Tables("ORGANIG").Columns.Contains("xx_lead") Then strFiltro &= " OR xx_lead LIKE " & strRicerca

      dcOrga.Filter = strFiltro

      VerificaFunzioniVelociAbilitate()
      AggiornaTestoContatti()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Avvio Funzionalità Veloci"
  Public Overridable Sub VerificaFunzioniVelociAbilitate()
    Try
      cmdOg_telef.Enabled = (edOg_telef.Text.Trim <> "")
      cmdOg_cell.Enabled = (edOg_cell.Text.Trim <> "")
      cmdOg_telefpers.Enabled = (edOg_telefpers.Text.Trim <> "")
      cmdOg_cellpers.Enabled = (edOg_cellpers.Text.Trim <> "")
      cmdOg_email.Enabled = (edOg_email.Text.Trim <> "")
      cmdOg_emailpers.Enabled = (edOg_emailpers.Text.Trim <> "")
      cmdOg_fbuser.Enabled = (edOg_fbuser.Text.Trim <> "")
      cmdOg_twitteruser.Enabled = (edOg_twitteruser.Text.Trim <> "")
      cmdOg_skypeuser.Enabled = (edOg_skypeuser.Text.Trim <> "")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub AvviaSkype(ByVal strTelefono As String)
    Try
      Try
        If CLN__STD.IsBis Then
          IS_ExecOnSbc("", "skype:" & strTelefono)
        Else
          NTSProcessStart("skype:" & strTelefono, "")
        End If
      Catch ex As Exception
        oApp.MsgBoxErr(oApp.Tr(Me, 130669097249095436, "Errore in avvio applicazione skype."))
        Return
      End Try

      AvviaAttivitTelefonica()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub ApriEmail(ByVal strEmail As String)
    Try
      oCleOrga.ApriEmail(strEmail)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub ApriFacebook(ByVal strUtente As String)
    Try
      NTSProcessStart("www.facebook.com/" & strUtente, "")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub ApriTwitter(ByVal strUtente As String)
    Try
      If strUtente.StartsWith("@") Then strUtente = strUtente.Substring(1)

      NTSProcessStart("www.twitter.com/" & strUtente, "")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub AvviaAttivitTelefonica()
    Dim oPar As New CLE__CLDP
    Try
      If oCleOrga.lLead = 0 OrElse Not oCleOrga.bIsUserCrm Then Return

      oPar.bPar1 = False      'al ritorno da cratte è true se ho creato l'attività
      oPar.bPar2 = False      'al ritorno da cratte è true se ho creato l'opportunità

      oPar.strParam = oCleOrga.lLead.ToString.PadLeft(9, "0"c) & ";"
      oPar.dPar1 = 0
      oPar.bPar3 = False ' No Modal

      oMenu.RunChild("NTSInformatica", "FRMCRATTE", oApp.Tr(Me, 128922305693088080, "Attività telefonica"), DittaCorrente, "", "BNCRATTE", oPar, "", True, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Gestione Proposta Contatti"
  Public Overridable Sub AggiornaTestoContatti()
    Try
      'Mostro o nascondo la visibilità del "Potrebbe essere xxxxxxx?"
      If oCleOrga.dttPropostaContatti IsNot Nothing Then
        lbProponiContatto.Text = oCleOrga.GeneraTestoPropostaContatti()
        fmContatti.Text = ""
        lbProponiContatto.Visible = True
        cmdProssimo.Visible = True
        cmdCollega.Visible = True
      Else
        lbProponiContatto.Text = ""
        lbProponiContatto.Visible = False
        cmdProssimo.Visible = False
        cmdCollega.Visible = False
        DescrizioneAziendaInRubrica()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub AbilitaFunzioniContatto()
    Try
      If NTSCInt(edOg_contatto.Text) = 0 Then
        cmdCollegaAContatto.Enabled = False
        cmdCreaNuovoContatto.Enabled = False
      Else
        GctlSetVisEnab(cmdCollegaAContatto, False)
        'Se non si tratta di un contatto con più organizzazioni non ha senso poterlo scollegare!
        If oCleOrga.VerificaContattoMultiOrganizzazione(grvOrga.NTSGetCurrentDataRow) Then
          GctlSetEnable(cmdCreaNuovoContatto)
        Else
          cmdCreaNuovoContatto.Enabled = False
        End If
      End If

      AggiornaTestoContatti()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdCollega_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCollega.Click
    Try
      oCleOrga.ConfermaPropostaContatto(grvOrga.NTSGetCurrentDataRow)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdProssimo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProssimo.Click
    Try
      oCleOrga.ProssimaPropostaContatto()
      AggiornaTestoContatti()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdCreaNuovoContatto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreaNuovoContatto.Click
    Try
      'Se non si tratta di un contatto con più organizzazioni non ha senso poterlo scollegare!
      If Not oCleOrga.VerificaContattoMultiOrganizzazione(grvOrga.NTSGetCurrentDataRow) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130465242360608312, "Il contatto è collegato solo questa organizzazione." & vbCrLf & _
                                                       "Non vi è alcuna utilità nel creare un nuovo contatto per questa organizzazione."))
        Return
      End If

      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130467804178533068, "Sei sicuro di voler scollegare la persona dall'attuale contatto e crearne uno nuovo?")) = Windows.Forms.DialogResult.Yes Then
        edOg_contatto.NTSTextDB = "0"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdCollegaAContatto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCollegaAContatto.Click
    Dim oParam As New CLE__PATB
    Try
      'Mostra lo zoom con le informazioni dei contatti e permette di collegare ad un contatto esistente l'organizzazione attuale.
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130467797501105774, "Sei sicuro di voler collegare la persona ad un altro contatto?" & vbCrLf & _
                                                                    "Eventuali campi non coerenti con il contatto selezionato saranno sovrascritti!")) = Windows.Forms.DialogResult.Yes Then
        NTSZOOM.ZoomStrIn("ZOOMCONTATTI", DittaCorrente, oParam)
        If NTSCInt(NTSZOOM.strIn) <> 0 Then
          Dim dttContatto As New DataTable
          oMenu.ValCodiceDb(NTSZOOM.strIn, DittaCorrente, "CONTATTI", "N", , dttContatto)
          oCleOrga.CollegaAContatto(grvOrga.NTSGetCurrentDataRow, dttContatto.Rows(0))
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region
End Class

