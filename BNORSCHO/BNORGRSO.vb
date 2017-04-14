Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORGRSO

#Region "Variabili"
  Public oCleScho As CLEORSCHO
  Public oCallParams As CLE__CLDP
  Public dsGrso As DataSet
  Public dcGrso As BindingSource = New BindingSource()

  Public dsGrid As DataSet
  Public dcGrid As BindingSource = New BindingSource()

  Public bClose As Boolean = False
  Public bNoModal As Boolean = False

  Private components As System.ComponentModel.IContainer

  Public WithEvents grGrso As NTSInformatica.NTSGrid
  Public WithEvents grvGrso As NTSInformatica.NTSGridView
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents lbConto As NTSInformatica.NTSLabel

  Public WithEvents td_datord As NTSInformatica.NTSGridColumn
  Public WithEvents ko_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents ko_serie As NTSInformatica.NTSGridColumn
  Public WithEvents ko_numord As NTSInformatica.NTSGridColumn
  Public WithEvents td_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents td_alfpar As NTSInformatica.NTSGridColumn
  Public WithEvents td_numpar As NTSInformatica.NTSGridColumn
  Public WithEvents td_datpar As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva As NTSInformatica.NTSGridColumn
  Public WithEvents mo_flevas As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quapre As NTSInformatica.NTSGridColumn
  Public WithEvents mo_flevapre As NTSInformatica.NTSGridColumn
  Public WithEvents quaeva As NTSInformatica.NTSGridColumn 'per compatibilita con la store procedure
  Public WithEvents xx_disp As NTSInformatica.NTSGridColumn
  Public WithEvents ko_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents mo_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents mo_valore As NTSInformatica.NTSGridColumn
  Public WithEvents an_descr1 As NTSInformatica.NTSGridColumn
  Public WithEvents PRZNET As NTSInformatica.NTSGridColumn 'per compatibilita con la store procedure
  Public WithEvents mo_scont1 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont2 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont3 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_prelist As NTSInformatica.NTSGridColumn
  Public WithEvents mo_prezvalc As NTSInformatica.NTSGridColumn
  Public WithEvents mo_colli As NTSInformatica.NTSGridColumn
  Public WithEvents mo_coleva As NTSInformatica.NTSGridColumn
  Public WithEvents mo_colpre As NTSInformatica.NTSGridColumn
  Public WithEvents mo_misura1 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_misura2 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_misura3 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_controp As NTSInformatica.NTSGridColumn
  Public WithEvents mo_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents mo_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents mo_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents mo_datconsor As NTSInformatica.NTSGridColumn
  Public WithEvents mo_provv As NTSInformatica.NTSGridColumn
  Public WithEvents mo_vprovv As NTSInformatica.NTSGridColumn
  Public WithEvents mo_provv2 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_vprovv2 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont4 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont5 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont6 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_ubicaz As NTSInformatica.NTSGridColumn
  Public WithEvents T1o As NTSInformatica.NTSGridColumn 'per compatibilita con la store procedure
  Public WithEvents T2o As NTSInformatica.NTSGridColumn
  Public WithEvents T3o As NTSInformatica.NTSGridColumn
  Public WithEvents T4o As NTSInformatica.NTSGridColumn
  Public WithEvents T5o As NTSInformatica.NTSGridColumn
  Public WithEvents T6o As NTSInformatica.NTSGridColumn
  Public WithEvents T7o As NTSInformatica.NTSGridColumn
  Public WithEvents T8o As NTSInformatica.NTSGridColumn
  Public WithEvents T9o As NTSInformatica.NTSGridColumn
  Public WithEvents T11o As NTSInformatica.NTSGridColumn
  Public WithEvents T12o As NTSInformatica.NTSGridColumn
  Public WithEvents T13o As NTSInformatica.NTSGridColumn
  Public WithEvents T14o As NTSInformatica.NTSGridColumn
  Public WithEvents T15o As NTSInformatica.NTSGridColumn
  Public WithEvents T16o As NTSInformatica.NTSGridColumn
  Public WithEvents T17o As NTSInformatica.NTSGridColumn
  Public WithEvents T18o As NTSInformatica.NTSGridColumn
  Public WithEvents T19o As NTSInformatica.NTSGridColumn
  Public WithEvents T20o As NTSInformatica.NTSGridColumn
  Public WithEvents T21o As NTSInformatica.NTSGridColumn
  Public WithEvents T22o As NTSInformatica.NTSGridColumn
  Public WithEvents T23o As NTSInformatica.NTSGridColumn
  Public WithEvents T24o As NTSInformatica.NTSGridColumn
  Public WithEvents T1c As NTSInformatica.NTSGridColumn
  Public WithEvents T2c As NTSInformatica.NTSGridColumn
  Public WithEvents T3c As NTSInformatica.NTSGridColumn
  Public WithEvents T4c As NTSInformatica.NTSGridColumn
  Public WithEvents T5c As NTSInformatica.NTSGridColumn
  Public WithEvents T6c As NTSInformatica.NTSGridColumn
  Public WithEvents T7c As NTSInformatica.NTSGridColumn
  Public WithEvents T8c As NTSInformatica.NTSGridColumn
  Public WithEvents T9c As NTSInformatica.NTSGridColumn
  Public WithEvents T10c As NTSInformatica.NTSGridColumn
  Public WithEvents T11c As NTSInformatica.NTSGridColumn
  Public WithEvents T12c As NTSInformatica.NTSGridColumn
  Public WithEvents T13c As NTSInformatica.NTSGridColumn
  Public WithEvents T14c As NTSInformatica.NTSGridColumn
  Public WithEvents T15c As NTSInformatica.NTSGridColumn
  Public WithEvents T16c As NTSInformatica.NTSGridColumn
  Public WithEvents T17c As NTSInformatica.NTSGridColumn
  Public WithEvents T18c As NTSInformatica.NTSGridColumn
  Public WithEvents T19c As NTSInformatica.NTSGridColumn
  Public WithEvents T20c As NTSInformatica.NTSGridColumn
  Public WithEvents T21c As NTSInformatica.NTSGridColumn
  Public WithEvents T22c As NTSInformatica.NTSGridColumn
  Public WithEvents T23c As NTSInformatica.NTSGridColumn
  Public WithEvents T24c As NTSInformatica.NTSGridColumn
  Public WithEvents T1r As NTSInformatica.NTSGridColumn
  Public WithEvents T2r As NTSInformatica.NTSGridColumn
  Public WithEvents T3r As NTSInformatica.NTSGridColumn
  Public WithEvents T4r As NTSInformatica.NTSGridColumn
  Public WithEvents T5r As NTSInformatica.NTSGridColumn
  Public WithEvents T6r As NTSInformatica.NTSGridColumn
  Public WithEvents T7r As NTSInformatica.NTSGridColumn
  Public WithEvents T8r As NTSInformatica.NTSGridColumn
  Public WithEvents T9r As NTSInformatica.NTSGridColumn
  Public WithEvents T10r As NTSInformatica.NTSGridColumn
  Public WithEvents T11r As NTSInformatica.NTSGridColumn
  Public WithEvents T12r As NTSInformatica.NTSGridColumn
  Public WithEvents T13r As NTSInformatica.NTSGridColumn
  Public WithEvents T14r As NTSInformatica.NTSGridColumn
  Public WithEvents T15r As NTSInformatica.NTSGridColumn
  Public WithEvents T16r As NTSInformatica.NTSGridColumn
  Public WithEvents T17r As NTSInformatica.NTSGridColumn
  Public WithEvents T18r As NTSInformatica.NTSGridColumn
  Public WithEvents T19r As NTSInformatica.NTSGridColumn
  Public WithEvents T20r As NTSInformatica.NTSGridColumn
  Public WithEvents T21r As NTSInformatica.NTSGridColumn
  Public WithEvents T22r As NTSInformatica.NTSGridColumn
  Public WithEvents T23r As NTSInformatica.NTSGridColumn
  Public WithEvents T24r As NTSInformatica.NTSGridColumn

  Public WithEvents T10o As NTSInformatica.NTSGridColumn
  Public WithEvents lbXx_conto As NTSInformatica.NTSLabel
  Public WithEvents lbXx_articolo As NTSInformatica.NTSLabel
  Public WithEvents lbArticolo As NTSInformatica.NTSLabel
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents lbXx_commeca As NTSInformatica.NTSLabel
  Public WithEvents lbCommeca As NTSInformatica.NTSLabel
  Public WithEvents lbXx_Magaz As NTSInformatica.NTSLabel
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
  Public WithEvents lbFase As NTSInformatica.NTSLabel
  Public WithEvents lbUnmis As NTSInformatica.NTSLabel
  Public WithEvents lbAdatcons As NTSInformatica.NTSLabel
  Public WithEvents lbDadatcons As NTSInformatica.NTSLabel
  Public WithEvents lbEsist As NTSInformatica.NTSLabel
  Public WithEvents tlbTaglie As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbNavigazioneDoc As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbNavigazioneMrp As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem

  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edEsist As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDadatcons As NTSInformatica.NTSTextBoxData
  Public WithEvents edAdatcons As NTSInformatica.NTSTextBoxData
  Public WithEvents edArticolo As NTSInformatica.NTSTextBoxStr
  Public WithEvents edFase As NTSInformatica.NTSTextBoxNum
  Public WithEvents edUnmis As NTSInformatica.NTSTextBoxStr
  Public WithEvents edMagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCommeca As NTSInformatica.NTSTextBoxNum
#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMORGRSO))
    Me.grGrso = New NTSInformatica.NTSGrid
    Me.grvGrso = New NTSInformatica.NTSGridView
    Me.td_datord = New NTSInformatica.NTSGridColumn
    Me.ko_tipork = New NTSInformatica.NTSGridColumn
    Me.ko_serie = New NTSInformatica.NTSGridColumn
    Me.ko_numord = New NTSInformatica.NTSGridColumn
    Me.td_riferim = New NTSInformatica.NTSGridColumn
    Me.td_alfpar = New NTSInformatica.NTSGridColumn
    Me.td_numpar = New NTSInformatica.NTSGridColumn
    Me.td_datpar = New NTSInformatica.NTSGridColumn
    Me.mo_quant = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva = New NTSInformatica.NTSGridColumn
    Me.mo_flevas = New NTSInformatica.NTSGridColumn
    Me.mo_quapre = New NTSInformatica.NTSGridColumn
    Me.mo_flevapre = New NTSInformatica.NTSGridColumn
    Me.quaeva = New NTSInformatica.NTSGridColumn
    Me.xx_disp = New NTSInformatica.NTSGridColumn
    Me.ko_datcons = New NTSInformatica.NTSGridColumn
    Me.mo_prezzo = New NTSInformatica.NTSGridColumn
    Me.mo_valore = New NTSInformatica.NTSGridColumn
    Me.an_descr1 = New NTSInformatica.NTSGridColumn
    Me.PRZNET = New NTSInformatica.NTSGridColumn
    Me.mo_scont1 = New NTSInformatica.NTSGridColumn
    Me.mo_scont2 = New NTSInformatica.NTSGridColumn
    Me.mo_scont3 = New NTSInformatica.NTSGridColumn
    Me.mo_prelist = New NTSInformatica.NTSGridColumn
    Me.mo_prezvalc = New NTSInformatica.NTSGridColumn
    Me.mo_colli = New NTSInformatica.NTSGridColumn
    Me.mo_coleva = New NTSInformatica.NTSGridColumn
    Me.mo_colpre = New NTSInformatica.NTSGridColumn
    Me.mo_misura1 = New NTSInformatica.NTSGridColumn
    Me.mo_misura2 = New NTSInformatica.NTSGridColumn
    Me.mo_misura3 = New NTSInformatica.NTSGridColumn
    Me.mo_controp = New NTSInformatica.NTSGridColumn
    Me.mo_commeca = New NTSInformatica.NTSGridColumn
    Me.mo_codcena = New NTSInformatica.NTSGridColumn
    Me.mo_codcfam = New NTSInformatica.NTSGridColumn
    Me.mo_datconsor = New NTSInformatica.NTSGridColumn
    Me.mo_provv = New NTSInformatica.NTSGridColumn
    Me.mo_vprovv = New NTSInformatica.NTSGridColumn
    Me.mo_provv2 = New NTSInformatica.NTSGridColumn
    Me.mo_vprovv2 = New NTSInformatica.NTSGridColumn
    Me.mo_scont4 = New NTSInformatica.NTSGridColumn
    Me.mo_scont5 = New NTSInformatica.NTSGridColumn
    Me.mo_scont6 = New NTSInformatica.NTSGridColumn
    Me.mo_ubicaz = New NTSInformatica.NTSGridColumn
    Me.T1o = New NTSInformatica.NTSGridColumn
    Me.T2o = New NTSInformatica.NTSGridColumn
    Me.T3o = New NTSInformatica.NTSGridColumn
    Me.T4o = New NTSInformatica.NTSGridColumn
    Me.T5o = New NTSInformatica.NTSGridColumn
    Me.T6o = New NTSInformatica.NTSGridColumn
    Me.T7o = New NTSInformatica.NTSGridColumn
    Me.T8o = New NTSInformatica.NTSGridColumn
    Me.T9o = New NTSInformatica.NTSGridColumn
    Me.T10o = New NTSInformatica.NTSGridColumn
    Me.T11o = New NTSInformatica.NTSGridColumn
    Me.T12o = New NTSInformatica.NTSGridColumn
    Me.T13o = New NTSInformatica.NTSGridColumn
    Me.T14o = New NTSInformatica.NTSGridColumn
    Me.T15o = New NTSInformatica.NTSGridColumn
    Me.T16o = New NTSInformatica.NTSGridColumn
    Me.T17o = New NTSInformatica.NTSGridColumn
    Me.T18o = New NTSInformatica.NTSGridColumn
    Me.T19o = New NTSInformatica.NTSGridColumn
    Me.T20o = New NTSInformatica.NTSGridColumn
    Me.T21o = New NTSInformatica.NTSGridColumn
    Me.T22o = New NTSInformatica.NTSGridColumn
    Me.T23o = New NTSInformatica.NTSGridColumn
    Me.T24o = New NTSInformatica.NTSGridColumn
    Me.T1c = New NTSInformatica.NTSGridColumn
    Me.T2c = New NTSInformatica.NTSGridColumn
    Me.T3c = New NTSInformatica.NTSGridColumn
    Me.T4c = New NTSInformatica.NTSGridColumn
    Me.T5c = New NTSInformatica.NTSGridColumn
    Me.T6c = New NTSInformatica.NTSGridColumn
    Me.T7c = New NTSInformatica.NTSGridColumn
    Me.T8c = New NTSInformatica.NTSGridColumn
    Me.T9c = New NTSInformatica.NTSGridColumn
    Me.T10c = New NTSInformatica.NTSGridColumn
    Me.T11c = New NTSInformatica.NTSGridColumn
    Me.T12c = New NTSInformatica.NTSGridColumn
    Me.T13c = New NTSInformatica.NTSGridColumn
    Me.T14c = New NTSInformatica.NTSGridColumn
    Me.T15c = New NTSInformatica.NTSGridColumn
    Me.T16c = New NTSInformatica.NTSGridColumn
    Me.T17c = New NTSInformatica.NTSGridColumn
    Me.T18c = New NTSInformatica.NTSGridColumn
    Me.T19c = New NTSInformatica.NTSGridColumn
    Me.T20c = New NTSInformatica.NTSGridColumn
    Me.T21c = New NTSInformatica.NTSGridColumn
    Me.T22c = New NTSInformatica.NTSGridColumn
    Me.T23c = New NTSInformatica.NTSGridColumn
    Me.T24c = New NTSInformatica.NTSGridColumn
    Me.T1r = New NTSInformatica.NTSGridColumn
    Me.T2r = New NTSInformatica.NTSGridColumn
    Me.T3r = New NTSInformatica.NTSGridColumn
    Me.T4r = New NTSInformatica.NTSGridColumn
    Me.T5r = New NTSInformatica.NTSGridColumn
    Me.T6r = New NTSInformatica.NTSGridColumn
    Me.T7r = New NTSInformatica.NTSGridColumn
    Me.T8r = New NTSInformatica.NTSGridColumn
    Me.T9r = New NTSInformatica.NTSGridColumn
    Me.T10r = New NTSInformatica.NTSGridColumn
    Me.T11r = New NTSInformatica.NTSGridColumn
    Me.T12r = New NTSInformatica.NTSGridColumn
    Me.T13r = New NTSInformatica.NTSGridColumn
    Me.T14r = New NTSInformatica.NTSGridColumn
    Me.T15r = New NTSInformatica.NTSGridColumn
    Me.T16r = New NTSInformatica.NTSGridColumn
    Me.T17r = New NTSInformatica.NTSGridColumn
    Me.T18r = New NTSInformatica.NTSGridColumn
    Me.T19r = New NTSInformatica.NTSGridColumn
    Me.T20r = New NTSInformatica.NTSGridColumn
    Me.T21r = New NTSInformatica.NTSGridColumn
    Me.T22r = New NTSInformatica.NTSGridColumn
    Me.T23r = New NTSInformatica.NTSGridColumn
    Me.T24r = New NTSInformatica.NTSGridColumn
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.edCommeca = New NTSInformatica.NTSTextBoxNum
    Me.edMagaz = New NTSInformatica.NTSTextBoxNum
    Me.edUnmis = New NTSInformatica.NTSTextBoxStr
    Me.edFase = New NTSInformatica.NTSTextBoxNum
    Me.edArticolo = New NTSInformatica.NTSTextBoxStr
    Me.edAdatcons = New NTSInformatica.NTSTextBoxData
    Me.edDadatcons = New NTSInformatica.NTSTextBoxData
    Me.edEsist = New NTSInformatica.NTSTextBoxNum
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.lbEsist = New NTSInformatica.NTSLabel
    Me.lbFase = New NTSInformatica.NTSLabel
    Me.lbUnmis = New NTSInformatica.NTSLabel
    Me.lbAdatcons = New NTSInformatica.NTSLabel
    Me.lbDadatcons = New NTSInformatica.NTSLabel
    Me.lbXx_commeca = New NTSInformatica.NTSLabel
    Me.lbCommeca = New NTSInformatica.NTSLabel
    Me.lbXx_Magaz = New NTSInformatica.NTSLabel
    Me.lbMagaz = New NTSInformatica.NTSLabel
    Me.lbXx_articolo = New NTSInformatica.NTSLabel
    Me.lbArticolo = New NTSInformatica.NTSLabel
    Me.lbXx_conto = New NTSInformatica.NTSLabel
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbTaglie = New NTSInformatica.NTSBarButtonItem
    Me.tlbOrdini = New NTSInformatica.NTSBarButtonItem
    Me.tlbNavigazioneDoc = New NTSInformatica.NTSBarButtonItem
    Me.tlbNavigazioneMrp = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarMenuItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    CType(Me.grGrso, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGrso, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edCommeca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUnmis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAdatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDadatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEsist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'grGrso
    '
    Me.grGrso.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grGrso.EmbeddedNavigator.Name = ""
    Me.grGrso.Location = New System.Drawing.Point(0, 0)
    Me.grGrso.MainView = Me.grvGrso
    Me.grGrso.Name = "grGrso"
    Me.grGrso.Size = New System.Drawing.Size(660, 310)
    Me.grGrso.TabIndex = 5
    Me.grGrso.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGrso})
    '
    'grvGrso
    '
    Me.grvGrso.ActiveFilterEnabled = False
    Me.grvGrso.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.td_datord, Me.ko_tipork, Me.ko_serie, Me.ko_numord, Me.td_riferim, Me.td_alfpar, Me.td_numpar, Me.td_datpar, Me.mo_quant, Me.mo_quaeva, Me.mo_flevas, Me.mo_quapre, Me.mo_flevapre, Me.quaeva, Me.xx_disp, Me.ko_datcons, Me.mo_prezzo, Me.mo_valore, Me.an_descr1, Me.PRZNET, Me.mo_scont1, Me.mo_scont2, Me.mo_scont3, Me.mo_prelist, Me.mo_prezvalc, Me.mo_colli, Me.mo_coleva, Me.mo_colpre, Me.mo_misura1, Me.mo_misura2, Me.mo_misura3, Me.mo_controp, Me.mo_commeca, Me.mo_codcena, Me.mo_codcfam, Me.mo_datconsor, Me.mo_provv, Me.mo_vprovv, Me.mo_provv2, Me.mo_vprovv2, Me.mo_scont4, Me.mo_scont5, Me.mo_scont6, Me.mo_ubicaz, Me.T1o, Me.T2o, Me.T3o, Me.T4o, Me.T5o, Me.T6o, Me.T7o, Me.T8o, Me.T9o, Me.T10o, Me.T11o, Me.T12o, Me.T13o, Me.T14o, Me.T15o, Me.T16o, Me.T17o, Me.T18o, Me.T19o, Me.T20o, Me.T21o, Me.T22o, Me.T23o, Me.T24o, Me.T1c, Me.T2c, Me.T3c, Me.T4c, Me.T5c, Me.T6c, Me.T7c, Me.T8c, Me.T9c, Me.T10c, Me.T11c, Me.T12c, Me.T13c, Me.T14c, Me.T15c, Me.T16c, Me.T17c, Me.T18c, Me.T19c, Me.T20c, Me.T21c, Me.T22c, Me.T23c, Me.T24c, Me.T1r, Me.T2r, Me.T3r, Me.T4r, Me.T5r, Me.T6r, Me.T7r, Me.T8r, Me.T9r, Me.T10r, Me.T11r, Me.T12r, Me.T13r, Me.T14r, Me.T15r, Me.T16r, Me.T17r, Me.T18r, Me.T19r, Me.T20r, Me.T21r, Me.T22r, Me.T23r, Me.T24r})
    Me.grvGrso.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGrso.Enabled = True
    Me.grvGrso.GridControl = Me.grGrso
    Me.grvGrso.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGrso.MinRowHeight = 14
    Me.grvGrso.Name = "grvGrso"
    Me.grvGrso.NTSAllowDelete = True
    Me.grvGrso.NTSAllowInsert = True
    Me.grvGrso.NTSAllowUpdate = True
    Me.grvGrso.NTSMenuContext = Nothing
    Me.grvGrso.OptionsCustomization.AllowRowSizing = True
    Me.grvGrso.OptionsFilter.AllowFilterEditor = False
    Me.grvGrso.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGrso.OptionsNavigation.UseTabKey = False
    Me.grvGrso.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGrso.OptionsView.ColumnAutoWidth = False
    Me.grvGrso.OptionsView.EnableAppearanceEvenRow = True
    Me.grvGrso.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGrso.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGrso.OptionsView.ShowGroupPanel = False
    Me.grvGrso.RowHeight = 16
    '
    'td_datord
    '
    Me.td_datord.AppearanceCell.Options.UseBackColor = True
    Me.td_datord.AppearanceCell.Options.UseTextOptions = True
    Me.td_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_datord.Caption = "Data Ord."
    Me.td_datord.Enabled = True
    Me.td_datord.FieldName = "td_datord"
    Me.td_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_datord.Name = "td_datord"
    Me.td_datord.NTSRepositoryComboBox = Nothing
    Me.td_datord.NTSRepositoryItemCheck = Nothing
    Me.td_datord.NTSRepositoryItemMemo = Nothing
    Me.td_datord.NTSRepositoryItemText = Nothing
    Me.td_datord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_datord.OptionsFilter.AllowFilter = False
    Me.td_datord.Visible = True
    Me.td_datord.VisibleIndex = 0
    '
    'ko_tipork
    '
    Me.ko_tipork.AppearanceCell.Options.UseBackColor = True
    Me.ko_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.ko_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ko_tipork.Caption = "Tipo"
    Me.ko_tipork.Enabled = True
    Me.ko_tipork.FieldName = "ko_tipork"
    Me.ko_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ko_tipork.Name = "ko_tipork"
    Me.ko_tipork.NTSRepositoryComboBox = Nothing
    Me.ko_tipork.NTSRepositoryItemCheck = Nothing
    Me.ko_tipork.NTSRepositoryItemMemo = Nothing
    Me.ko_tipork.NTSRepositoryItemText = Nothing
    Me.ko_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ko_tipork.OptionsFilter.AllowFilter = False
    Me.ko_tipork.Visible = True
    Me.ko_tipork.VisibleIndex = 1
    '
    'ko_serie
    '
    Me.ko_serie.AppearanceCell.Options.UseBackColor = True
    Me.ko_serie.AppearanceCell.Options.UseTextOptions = True
    Me.ko_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ko_serie.Caption = "Serie"
    Me.ko_serie.Enabled = True
    Me.ko_serie.FieldName = "ko_serie"
    Me.ko_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ko_serie.Name = "ko_serie"
    Me.ko_serie.NTSRepositoryComboBox = Nothing
    Me.ko_serie.NTSRepositoryItemCheck = Nothing
    Me.ko_serie.NTSRepositoryItemMemo = Nothing
    Me.ko_serie.NTSRepositoryItemText = Nothing
    Me.ko_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ko_serie.OptionsFilter.AllowFilter = False
    Me.ko_serie.Visible = True
    Me.ko_serie.VisibleIndex = 2
    '
    'ko_numord
    '
    Me.ko_numord.AppearanceCell.Options.UseBackColor = True
    Me.ko_numord.AppearanceCell.Options.UseTextOptions = True
    Me.ko_numord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ko_numord.Caption = "N°Ord."
    Me.ko_numord.Enabled = True
    Me.ko_numord.FieldName = "ko_numord"
    Me.ko_numord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ko_numord.Name = "ko_numord"
    Me.ko_numord.NTSRepositoryComboBox = Nothing
    Me.ko_numord.NTSRepositoryItemCheck = Nothing
    Me.ko_numord.NTSRepositoryItemMemo = Nothing
    Me.ko_numord.NTSRepositoryItemText = Nothing
    Me.ko_numord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ko_numord.OptionsFilter.AllowFilter = False
    Me.ko_numord.Visible = True
    Me.ko_numord.VisibleIndex = 3
    '
    'td_riferim
    '
    Me.td_riferim.AppearanceCell.Options.UseBackColor = True
    Me.td_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.td_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_riferim.Caption = "Riferim."
    Me.td_riferim.Enabled = True
    Me.td_riferim.FieldName = "td_riferim"
    Me.td_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_riferim.Name = "td_riferim"
    Me.td_riferim.NTSRepositoryComboBox = Nothing
    Me.td_riferim.NTSRepositoryItemCheck = Nothing
    Me.td_riferim.NTSRepositoryItemMemo = Nothing
    Me.td_riferim.NTSRepositoryItemText = Nothing
    Me.td_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_riferim.OptionsFilter.AllowFilter = False
    Me.td_riferim.Visible = True
    Me.td_riferim.VisibleIndex = 4
    '
    'td_alfpar
    '
    Me.td_alfpar.AppearanceCell.Options.UseBackColor = True
    Me.td_alfpar.AppearanceCell.Options.UseTextOptions = True
    Me.td_alfpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_alfpar.Caption = "A.Par."
    Me.td_alfpar.Enabled = True
    Me.td_alfpar.FieldName = "td_alfpar"
    Me.td_alfpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_alfpar.Name = "td_alfpar"
    Me.td_alfpar.NTSRepositoryComboBox = Nothing
    Me.td_alfpar.NTSRepositoryItemCheck = Nothing
    Me.td_alfpar.NTSRepositoryItemMemo = Nothing
    Me.td_alfpar.NTSRepositoryItemText = Nothing
    Me.td_alfpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_alfpar.OptionsFilter.AllowFilter = False
    Me.td_alfpar.Visible = True
    Me.td_alfpar.VisibleIndex = 5
    '
    'td_numpar
    '
    Me.td_numpar.AppearanceCell.Options.UseBackColor = True
    Me.td_numpar.AppearanceCell.Options.UseTextOptions = True
    Me.td_numpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_numpar.Caption = "N°Par."
    Me.td_numpar.Enabled = True
    Me.td_numpar.FieldName = "td_numpar"
    Me.td_numpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_numpar.Name = "td_numpar"
    Me.td_numpar.NTSRepositoryComboBox = Nothing
    Me.td_numpar.NTSRepositoryItemCheck = Nothing
    Me.td_numpar.NTSRepositoryItemMemo = Nothing
    Me.td_numpar.NTSRepositoryItemText = Nothing
    Me.td_numpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_numpar.OptionsFilter.AllowFilter = False
    Me.td_numpar.Visible = True
    Me.td_numpar.VisibleIndex = 6
    '
    'td_datpar
    '
    Me.td_datpar.AppearanceCell.Options.UseBackColor = True
    Me.td_datpar.AppearanceCell.Options.UseTextOptions = True
    Me.td_datpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_datpar.Caption = "Dt.Par."
    Me.td_datpar.Enabled = True
    Me.td_datpar.FieldName = "td_datpar"
    Me.td_datpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_datpar.Name = "td_datpar"
    Me.td_datpar.NTSRepositoryComboBox = Nothing
    Me.td_datpar.NTSRepositoryItemCheck = Nothing
    Me.td_datpar.NTSRepositoryItemMemo = Nothing
    Me.td_datpar.NTSRepositoryItemText = Nothing
    Me.td_datpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_datpar.OptionsFilter.AllowFilter = False
    Me.td_datpar.Visible = True
    Me.td_datpar.VisibleIndex = 7
    '
    'mo_quant
    '
    Me.mo_quant.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant.Caption = "Qta.Ord."
    Me.mo_quant.Enabled = True
    Me.mo_quant.FieldName = "mo_quant"
    Me.mo_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant.Name = "mo_quant"
    Me.mo_quant.NTSRepositoryComboBox = Nothing
    Me.mo_quant.NTSRepositoryItemCheck = Nothing
    Me.mo_quant.NTSRepositoryItemMemo = Nothing
    Me.mo_quant.NTSRepositoryItemText = Nothing
    Me.mo_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant.OptionsFilter.AllowFilter = False
    Me.mo_quant.Visible = True
    Me.mo_quant.VisibleIndex = 8
    '
    'mo_quaeva
    '
    Me.mo_quaeva.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva.Caption = "Qta.Sped."
    Me.mo_quaeva.Enabled = True
    Me.mo_quaeva.FieldName = "mo_quaeva"
    Me.mo_quaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva.Name = "mo_quaeva"
    Me.mo_quaeva.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva.NTSRepositoryItemText = Nothing
    Me.mo_quaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva.OptionsFilter.AllowFilter = False
    Me.mo_quaeva.Visible = True
    Me.mo_quaeva.VisibleIndex = 9
    '
    'mo_flevas
    '
    Me.mo_flevas.AppearanceCell.Options.UseBackColor = True
    Me.mo_flevas.AppearanceCell.Options.UseTextOptions = True
    Me.mo_flevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_flevas.Caption = "Ev."
    Me.mo_flevas.Enabled = True
    Me.mo_flevas.FieldName = "mo_flevas"
    Me.mo_flevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_flevas.Name = "mo_flevas"
    Me.mo_flevas.NTSRepositoryComboBox = Nothing
    Me.mo_flevas.NTSRepositoryItemCheck = Nothing
    Me.mo_flevas.NTSRepositoryItemMemo = Nothing
    Me.mo_flevas.NTSRepositoryItemText = Nothing
    Me.mo_flevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_flevas.OptionsFilter.AllowFilter = False
    Me.mo_flevas.Visible = True
    Me.mo_flevas.VisibleIndex = 10
    '
    'mo_quapre
    '
    Me.mo_quapre.AppearanceCell.Options.UseBackColor = True
    Me.mo_quapre.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quapre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quapre.Caption = "Qta.Pren."
    Me.mo_quapre.Enabled = True
    Me.mo_quapre.FieldName = "mo_quapre"
    Me.mo_quapre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quapre.Name = "mo_quapre"
    Me.mo_quapre.NTSRepositoryComboBox = Nothing
    Me.mo_quapre.NTSRepositoryItemCheck = Nothing
    Me.mo_quapre.NTSRepositoryItemMemo = Nothing
    Me.mo_quapre.NTSRepositoryItemText = Nothing
    Me.mo_quapre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quapre.OptionsFilter.AllowFilter = False
    Me.mo_quapre.Visible = True
    Me.mo_quapre.VisibleIndex = 11
    '
    'mo_flevapre
    '
    Me.mo_flevapre.AppearanceCell.Options.UseBackColor = True
    Me.mo_flevapre.AppearanceCell.Options.UseTextOptions = True
    Me.mo_flevapre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_flevapre.Caption = "Pr."
    Me.mo_flevapre.Enabled = True
    Me.mo_flevapre.FieldName = "mo_flevapre"
    Me.mo_flevapre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_flevapre.Name = "mo_flevapre"
    Me.mo_flevapre.NTSRepositoryComboBox = Nothing
    Me.mo_flevapre.NTSRepositoryItemCheck = Nothing
    Me.mo_flevapre.NTSRepositoryItemMemo = Nothing
    Me.mo_flevapre.NTSRepositoryItemText = Nothing
    Me.mo_flevapre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_flevapre.OptionsFilter.AllowFilter = False
    Me.mo_flevapre.Visible = True
    Me.mo_flevapre.VisibleIndex = 12
    '
    'quaeva
    '
    Me.quaeva.AppearanceCell.Options.UseBackColor = True
    Me.quaeva.AppearanceCell.Options.UseTextOptions = True
    Me.quaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.quaeva.Caption = "Qtà.residua"
    Me.quaeva.Enabled = True
    Me.quaeva.FieldName = "quaeva"
    Me.quaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.quaeva.Name = "quaeva"
    Me.quaeva.NTSRepositoryComboBox = Nothing
    Me.quaeva.NTSRepositoryItemCheck = Nothing
    Me.quaeva.NTSRepositoryItemMemo = Nothing
    Me.quaeva.NTSRepositoryItemText = Nothing
    Me.quaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.quaeva.OptionsFilter.AllowFilter = False
    Me.quaeva.Visible = True
    Me.quaeva.VisibleIndex = 13
    '
    'xx_disp
    '
    Me.xx_disp.AppearanceCell.Options.UseBackColor = True
    Me.xx_disp.AppearanceCell.Options.UseTextOptions = True
    Me.xx_disp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_disp.Caption = "Disp."
    Me.xx_disp.Enabled = True
    Me.xx_disp.FieldName = "xx_disp"
    Me.xx_disp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_disp.Name = "xx_disp"
    Me.xx_disp.NTSRepositoryComboBox = Nothing
    Me.xx_disp.NTSRepositoryItemCheck = Nothing
    Me.xx_disp.NTSRepositoryItemMemo = Nothing
    Me.xx_disp.NTSRepositoryItemText = Nothing
    Me.xx_disp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_disp.OptionsFilter.AllowFilter = False
    Me.xx_disp.Visible = True
    Me.xx_disp.VisibleIndex = 14
    '
    'ko_datcons
    '
    Me.ko_datcons.AppearanceCell.Options.UseBackColor = True
    Me.ko_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.ko_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ko_datcons.Caption = "Consegna"
    Me.ko_datcons.Enabled = True
    Me.ko_datcons.FieldName = "ko_datcons"
    Me.ko_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ko_datcons.Name = "ko_datcons"
    Me.ko_datcons.NTSRepositoryComboBox = Nothing
    Me.ko_datcons.NTSRepositoryItemCheck = Nothing
    Me.ko_datcons.NTSRepositoryItemMemo = Nothing
    Me.ko_datcons.NTSRepositoryItemText = Nothing
    Me.ko_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ko_datcons.OptionsFilter.AllowFilter = False
    Me.ko_datcons.Visible = True
    Me.ko_datcons.VisibleIndex = 15
    '
    'mo_prezzo
    '
    Me.mo_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.mo_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.mo_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_prezzo.Caption = "Prezzo"
    Me.mo_prezzo.Enabled = True
    Me.mo_prezzo.FieldName = "mo_prezzo"
    Me.mo_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_prezzo.Name = "mo_prezzo"
    Me.mo_prezzo.NTSRepositoryComboBox = Nothing
    Me.mo_prezzo.NTSRepositoryItemCheck = Nothing
    Me.mo_prezzo.NTSRepositoryItemMemo = Nothing
    Me.mo_prezzo.NTSRepositoryItemText = Nothing
    Me.mo_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_prezzo.OptionsFilter.AllowFilter = False
    Me.mo_prezzo.Visible = True
    Me.mo_prezzo.VisibleIndex = 16
    '
    'mo_valore
    '
    Me.mo_valore.AppearanceCell.Options.UseBackColor = True
    Me.mo_valore.AppearanceCell.Options.UseTextOptions = True
    Me.mo_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_valore.Caption = "Valore"
    Me.mo_valore.Enabled = True
    Me.mo_valore.FieldName = "mo_valore"
    Me.mo_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_valore.Name = "mo_valore"
    Me.mo_valore.NTSRepositoryComboBox = Nothing
    Me.mo_valore.NTSRepositoryItemCheck = Nothing
    Me.mo_valore.NTSRepositoryItemMemo = Nothing
    Me.mo_valore.NTSRepositoryItemText = Nothing
    Me.mo_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_valore.OptionsFilter.AllowFilter = False
    Me.mo_valore.Visible = True
    Me.mo_valore.VisibleIndex = 17
    '
    'an_descr1
    '
    Me.an_descr1.AppearanceCell.Options.UseBackColor = True
    Me.an_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.an_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_descr1.Caption = "Cliente/Fornitore"
    Me.an_descr1.Enabled = True
    Me.an_descr1.FieldName = "an_descr1"
    Me.an_descr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_descr1.Name = "an_descr1"
    Me.an_descr1.NTSRepositoryComboBox = Nothing
    Me.an_descr1.NTSRepositoryItemCheck = Nothing
    Me.an_descr1.NTSRepositoryItemMemo = Nothing
    Me.an_descr1.NTSRepositoryItemText = Nothing
    Me.an_descr1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.an_descr1.OptionsFilter.AllowFilter = False
    Me.an_descr1.Visible = True
    Me.an_descr1.VisibleIndex = 18
    '
    'PRZNET
    '
    Me.PRZNET.AppearanceCell.Options.UseBackColor = True
    Me.PRZNET.AppearanceCell.Options.UseTextOptions = True
    Me.PRZNET.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.PRZNET.Caption = "Prezzo N."
    Me.PRZNET.Enabled = True
    Me.PRZNET.FieldName = "PRZNET"
    Me.PRZNET.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.PRZNET.Name = "PRZNET"
    Me.PRZNET.NTSRepositoryComboBox = Nothing
    Me.PRZNET.NTSRepositoryItemCheck = Nothing
    Me.PRZNET.NTSRepositoryItemMemo = Nothing
    Me.PRZNET.NTSRepositoryItemText = Nothing
    Me.PRZNET.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.PRZNET.OptionsFilter.AllowFilter = False
    Me.PRZNET.Visible = True
    Me.PRZNET.VisibleIndex = 19
    '
    'mo_scont1
    '
    Me.mo_scont1.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont1.Caption = "Sc.1"
    Me.mo_scont1.Enabled = True
    Me.mo_scont1.FieldName = "mo_scont1"
    Me.mo_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont1.Name = "mo_scont1"
    Me.mo_scont1.NTSRepositoryComboBox = Nothing
    Me.mo_scont1.NTSRepositoryItemCheck = Nothing
    Me.mo_scont1.NTSRepositoryItemMemo = Nothing
    Me.mo_scont1.NTSRepositoryItemText = Nothing
    Me.mo_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont1.OptionsFilter.AllowFilter = False
    Me.mo_scont1.Visible = True
    Me.mo_scont1.VisibleIndex = 20
    '
    'mo_scont2
    '
    Me.mo_scont2.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont2.Caption = "Sc.2"
    Me.mo_scont2.Enabled = True
    Me.mo_scont2.FieldName = "mo_scont2"
    Me.mo_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont2.Name = "mo_scont2"
    Me.mo_scont2.NTSRepositoryComboBox = Nothing
    Me.mo_scont2.NTSRepositoryItemCheck = Nothing
    Me.mo_scont2.NTSRepositoryItemMemo = Nothing
    Me.mo_scont2.NTSRepositoryItemText = Nothing
    Me.mo_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont2.OptionsFilter.AllowFilter = False
    Me.mo_scont2.Visible = True
    Me.mo_scont2.VisibleIndex = 21
    '
    'mo_scont3
    '
    Me.mo_scont3.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont3.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont3.Caption = "Sc.3"
    Me.mo_scont3.Enabled = True
    Me.mo_scont3.FieldName = "mo_scont3"
    Me.mo_scont3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont3.Name = "mo_scont3"
    Me.mo_scont3.NTSRepositoryComboBox = Nothing
    Me.mo_scont3.NTSRepositoryItemCheck = Nothing
    Me.mo_scont3.NTSRepositoryItemMemo = Nothing
    Me.mo_scont3.NTSRepositoryItemText = Nothing
    Me.mo_scont3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont3.OptionsFilter.AllowFilter = False
    Me.mo_scont3.Visible = True
    Me.mo_scont3.VisibleIndex = 22
    '
    'mo_prelist
    '
    Me.mo_prelist.AppearanceCell.Options.UseBackColor = True
    Me.mo_prelist.AppearanceCell.Options.UseTextOptions = True
    Me.mo_prelist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_prelist.Caption = "Prezzo List."
    Me.mo_prelist.Enabled = True
    Me.mo_prelist.FieldName = "mo_prelist"
    Me.mo_prelist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_prelist.Name = "mo_prelist"
    Me.mo_prelist.NTSRepositoryComboBox = Nothing
    Me.mo_prelist.NTSRepositoryItemCheck = Nothing
    Me.mo_prelist.NTSRepositoryItemMemo = Nothing
    Me.mo_prelist.NTSRepositoryItemText = Nothing
    Me.mo_prelist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_prelist.OptionsFilter.AllowFilter = False
    Me.mo_prelist.Visible = True
    Me.mo_prelist.VisibleIndex = 23
    '
    'mo_prezvalc
    '
    Me.mo_prezvalc.AppearanceCell.Options.UseBackColor = True
    Me.mo_prezvalc.AppearanceCell.Options.UseTextOptions = True
    Me.mo_prezvalc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_prezvalc.Caption = "Prz.Val."
    Me.mo_prezvalc.Enabled = True
    Me.mo_prezvalc.FieldName = "mo_prezvalc"
    Me.mo_prezvalc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_prezvalc.Name = "mo_prezvalc"
    Me.mo_prezvalc.NTSRepositoryComboBox = Nothing
    Me.mo_prezvalc.NTSRepositoryItemCheck = Nothing
    Me.mo_prezvalc.NTSRepositoryItemMemo = Nothing
    Me.mo_prezvalc.NTSRepositoryItemText = Nothing
    Me.mo_prezvalc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_prezvalc.OptionsFilter.AllowFilter = False
    Me.mo_prezvalc.Visible = True
    Me.mo_prezvalc.VisibleIndex = 24
    '
    'mo_colli
    '
    Me.mo_colli.AppearanceCell.Options.UseBackColor = True
    Me.mo_colli.AppearanceCell.Options.UseTextOptions = True
    Me.mo_colli.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_colli.Caption = "Colli"
    Me.mo_colli.Enabled = True
    Me.mo_colli.FieldName = "mo_colli"
    Me.mo_colli.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_colli.Name = "mo_colli"
    Me.mo_colli.NTSRepositoryComboBox = Nothing
    Me.mo_colli.NTSRepositoryItemCheck = Nothing
    Me.mo_colli.NTSRepositoryItemMemo = Nothing
    Me.mo_colli.NTSRepositoryItemText = Nothing
    Me.mo_colli.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_colli.OptionsFilter.AllowFilter = False
    Me.mo_colli.Visible = True
    Me.mo_colli.VisibleIndex = 25
    '
    'mo_coleva
    '
    Me.mo_coleva.AppearanceCell.Options.UseBackColor = True
    Me.mo_coleva.AppearanceCell.Options.UseTextOptions = True
    Me.mo_coleva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_coleva.Caption = "Col.evasi"
    Me.mo_coleva.Enabled = True
    Me.mo_coleva.FieldName = "mo_coleva"
    Me.mo_coleva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_coleva.Name = "mo_coleva"
    Me.mo_coleva.NTSRepositoryComboBox = Nothing
    Me.mo_coleva.NTSRepositoryItemCheck = Nothing
    Me.mo_coleva.NTSRepositoryItemMemo = Nothing
    Me.mo_coleva.NTSRepositoryItemText = Nothing
    Me.mo_coleva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_coleva.OptionsFilter.AllowFilter = False
    Me.mo_coleva.Visible = True
    Me.mo_coleva.VisibleIndex = 26
    '
    'mo_colpre
    '
    Me.mo_colpre.AppearanceCell.Options.UseBackColor = True
    Me.mo_colpre.AppearanceCell.Options.UseTextOptions = True
    Me.mo_colpre.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_colpre.Caption = "Col.Pren."
    Me.mo_colpre.Enabled = True
    Me.mo_colpre.FieldName = "mo_colpre"
    Me.mo_colpre.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_colpre.Name = "mo_colpre"
    Me.mo_colpre.NTSRepositoryComboBox = Nothing
    Me.mo_colpre.NTSRepositoryItemCheck = Nothing
    Me.mo_colpre.NTSRepositoryItemMemo = Nothing
    Me.mo_colpre.NTSRepositoryItemText = Nothing
    Me.mo_colpre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_colpre.OptionsFilter.AllowFilter = False
    Me.mo_colpre.Visible = True
    Me.mo_colpre.VisibleIndex = 27
    '
    'mo_misura1
    '
    Me.mo_misura1.AppearanceCell.Options.UseBackColor = True
    Me.mo_misura1.AppearanceCell.Options.UseTextOptions = True
    Me.mo_misura1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_misura1.Caption = "Mis.1"
    Me.mo_misura1.Enabled = True
    Me.mo_misura1.FieldName = "mo_misura1"
    Me.mo_misura1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_misura1.Name = "mo_misura1"
    Me.mo_misura1.NTSRepositoryComboBox = Nothing
    Me.mo_misura1.NTSRepositoryItemCheck = Nothing
    Me.mo_misura1.NTSRepositoryItemMemo = Nothing
    Me.mo_misura1.NTSRepositoryItemText = Nothing
    Me.mo_misura1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_misura1.OptionsFilter.AllowFilter = False
    Me.mo_misura1.Visible = True
    Me.mo_misura1.VisibleIndex = 28
    '
    'mo_misura2
    '
    Me.mo_misura2.AppearanceCell.Options.UseBackColor = True
    Me.mo_misura2.AppearanceCell.Options.UseTextOptions = True
    Me.mo_misura2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_misura2.Caption = "Mis.2"
    Me.mo_misura2.Enabled = True
    Me.mo_misura2.FieldName = "mo_misura2"
    Me.mo_misura2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_misura2.Name = "mo_misura2"
    Me.mo_misura2.NTSRepositoryComboBox = Nothing
    Me.mo_misura2.NTSRepositoryItemCheck = Nothing
    Me.mo_misura2.NTSRepositoryItemMemo = Nothing
    Me.mo_misura2.NTSRepositoryItemText = Nothing
    Me.mo_misura2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_misura2.OptionsFilter.AllowFilter = False
    Me.mo_misura2.Visible = True
    Me.mo_misura2.VisibleIndex = 29
    '
    'mo_misura3
    '
    Me.mo_misura3.AppearanceCell.Options.UseBackColor = True
    Me.mo_misura3.AppearanceCell.Options.UseTextOptions = True
    Me.mo_misura3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_misura3.Caption = "Mis.3"
    Me.mo_misura3.Enabled = True
    Me.mo_misura3.FieldName = "mo_misura3"
    Me.mo_misura3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_misura3.Name = "mo_misura3"
    Me.mo_misura3.NTSRepositoryComboBox = Nothing
    Me.mo_misura3.NTSRepositoryItemCheck = Nothing
    Me.mo_misura3.NTSRepositoryItemMemo = Nothing
    Me.mo_misura3.NTSRepositoryItemText = Nothing
    Me.mo_misura3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_misura3.OptionsFilter.AllowFilter = False
    Me.mo_misura3.Visible = True
    Me.mo_misura3.VisibleIndex = 30
    '
    'mo_controp
    '
    Me.mo_controp.AppearanceCell.Options.UseBackColor = True
    Me.mo_controp.AppearanceCell.Options.UseTextOptions = True
    Me.mo_controp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_controp.Caption = "Controp."
    Me.mo_controp.Enabled = True
    Me.mo_controp.FieldName = "mo_controp"
    Me.mo_controp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_controp.Name = "mo_controp"
    Me.mo_controp.NTSRepositoryComboBox = Nothing
    Me.mo_controp.NTSRepositoryItemCheck = Nothing
    Me.mo_controp.NTSRepositoryItemMemo = Nothing
    Me.mo_controp.NTSRepositoryItemText = Nothing
    Me.mo_controp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_controp.OptionsFilter.AllowFilter = False
    Me.mo_controp.Visible = True
    Me.mo_controp.VisibleIndex = 31
    '
    'mo_commeca
    '
    Me.mo_commeca.AppearanceCell.Options.UseBackColor = True
    Me.mo_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.mo_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_commeca.Caption = "Commessa"
    Me.mo_commeca.Enabled = True
    Me.mo_commeca.FieldName = "mo_commeca"
    Me.mo_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_commeca.Name = "mo_commeca"
    Me.mo_commeca.NTSRepositoryComboBox = Nothing
    Me.mo_commeca.NTSRepositoryItemCheck = Nothing
    Me.mo_commeca.NTSRepositoryItemMemo = Nothing
    Me.mo_commeca.NTSRepositoryItemText = Nothing
    Me.mo_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_commeca.OptionsFilter.AllowFilter = False
    Me.mo_commeca.Visible = True
    Me.mo_commeca.VisibleIndex = 32
    '
    'mo_codcena
    '
    Me.mo_codcena.AppearanceCell.Options.UseBackColor = True
    Me.mo_codcena.AppearanceCell.Options.UseTextOptions = True
    Me.mo_codcena.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_codcena.Caption = "C.Centro"
    Me.mo_codcena.Enabled = True
    Me.mo_codcena.FieldName = "mo_codcena"
    Me.mo_codcena.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_codcena.Name = "mo_codcena"
    Me.mo_codcena.NTSRepositoryComboBox = Nothing
    Me.mo_codcena.NTSRepositoryItemCheck = Nothing
    Me.mo_codcena.NTSRepositoryItemMemo = Nothing
    Me.mo_codcena.NTSRepositoryItemText = Nothing
    Me.mo_codcena.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_codcena.OptionsFilter.AllowFilter = False
    Me.mo_codcena.Visible = True
    Me.mo_codcena.VisibleIndex = 33
    '
    'mo_codcfam
    '
    Me.mo_codcfam.AppearanceCell.Options.UseBackColor = True
    Me.mo_codcfam.AppearanceCell.Options.UseTextOptions = True
    Me.mo_codcfam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_codcfam.Caption = "Linea"
    Me.mo_codcfam.Enabled = True
    Me.mo_codcfam.FieldName = "mo_codcfam"
    Me.mo_codcfam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_codcfam.Name = "mo_codcfam"
    Me.mo_codcfam.NTSRepositoryComboBox = Nothing
    Me.mo_codcfam.NTSRepositoryItemCheck = Nothing
    Me.mo_codcfam.NTSRepositoryItemMemo = Nothing
    Me.mo_codcfam.NTSRepositoryItemText = Nothing
    Me.mo_codcfam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_codcfam.OptionsFilter.AllowFilter = False
    Me.mo_codcfam.Visible = True
    Me.mo_codcfam.VisibleIndex = 34
    '
    'mo_datconsor
    '
    Me.mo_datconsor.AppearanceCell.Options.UseBackColor = True
    Me.mo_datconsor.AppearanceCell.Options.UseTextOptions = True
    Me.mo_datconsor.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_datconsor.Caption = "Dt.Cons.Or."
    Me.mo_datconsor.Enabled = True
    Me.mo_datconsor.FieldName = "mo_datconsor"
    Me.mo_datconsor.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_datconsor.Name = "mo_datconsor"
    Me.mo_datconsor.NTSRepositoryComboBox = Nothing
    Me.mo_datconsor.NTSRepositoryItemCheck = Nothing
    Me.mo_datconsor.NTSRepositoryItemMemo = Nothing
    Me.mo_datconsor.NTSRepositoryItemText = Nothing
    Me.mo_datconsor.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_datconsor.OptionsFilter.AllowFilter = False
    Me.mo_datconsor.Visible = True
    Me.mo_datconsor.VisibleIndex = 35
    '
    'mo_provv
    '
    Me.mo_provv.AppearanceCell.Options.UseBackColor = True
    Me.mo_provv.AppearanceCell.Options.UseTextOptions = True
    Me.mo_provv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_provv.Caption = "Provv."
    Me.mo_provv.Enabled = True
    Me.mo_provv.FieldName = "mo_provv"
    Me.mo_provv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_provv.Name = "mo_provv"
    Me.mo_provv.NTSRepositoryComboBox = Nothing
    Me.mo_provv.NTSRepositoryItemCheck = Nothing
    Me.mo_provv.NTSRepositoryItemMemo = Nothing
    Me.mo_provv.NTSRepositoryItemText = Nothing
    Me.mo_provv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_provv.OptionsFilter.AllowFilter = False
    Me.mo_provv.Visible = True
    Me.mo_provv.VisibleIndex = 36
    '
    'mo_vprovv
    '
    Me.mo_vprovv.AppearanceCell.Options.UseBackColor = True
    Me.mo_vprovv.AppearanceCell.Options.UseTextOptions = True
    Me.mo_vprovv.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_vprovv.Caption = "V.Provv."
    Me.mo_vprovv.Enabled = True
    Me.mo_vprovv.FieldName = "mo_vprovv"
    Me.mo_vprovv.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_vprovv.Name = "mo_vprovv"
    Me.mo_vprovv.NTSRepositoryComboBox = Nothing
    Me.mo_vprovv.NTSRepositoryItemCheck = Nothing
    Me.mo_vprovv.NTSRepositoryItemMemo = Nothing
    Me.mo_vprovv.NTSRepositoryItemText = Nothing
    Me.mo_vprovv.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_vprovv.OptionsFilter.AllowFilter = False
    Me.mo_vprovv.Visible = True
    Me.mo_vprovv.VisibleIndex = 37
    '
    'mo_provv2
    '
    Me.mo_provv2.AppearanceCell.Options.UseBackColor = True
    Me.mo_provv2.AppearanceCell.Options.UseTextOptions = True
    Me.mo_provv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_provv2.Caption = "Provv.2"
    Me.mo_provv2.Enabled = True
    Me.mo_provv2.FieldName = "mo_provv2"
    Me.mo_provv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_provv2.Name = "mo_provv2"
    Me.mo_provv2.NTSRepositoryComboBox = Nothing
    Me.mo_provv2.NTSRepositoryItemCheck = Nothing
    Me.mo_provv2.NTSRepositoryItemMemo = Nothing
    Me.mo_provv2.NTSRepositoryItemText = Nothing
    Me.mo_provv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_provv2.OptionsFilter.AllowFilter = False
    Me.mo_provv2.Visible = True
    Me.mo_provv2.VisibleIndex = 38
    '
    'mo_vprovv2
    '
    Me.mo_vprovv2.AppearanceCell.Options.UseBackColor = True
    Me.mo_vprovv2.AppearanceCell.Options.UseTextOptions = True
    Me.mo_vprovv2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_vprovv2.Caption = "V.Provv.2"
    Me.mo_vprovv2.Enabled = True
    Me.mo_vprovv2.FieldName = "mo_vprovv2"
    Me.mo_vprovv2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_vprovv2.Name = "mo_vprovv2"
    Me.mo_vprovv2.NTSRepositoryComboBox = Nothing
    Me.mo_vprovv2.NTSRepositoryItemCheck = Nothing
    Me.mo_vprovv2.NTSRepositoryItemMemo = Nothing
    Me.mo_vprovv2.NTSRepositoryItemText = Nothing
    Me.mo_vprovv2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_vprovv2.OptionsFilter.AllowFilter = False
    Me.mo_vprovv2.Visible = True
    Me.mo_vprovv2.VisibleIndex = 39
    '
    'mo_scont4
    '
    Me.mo_scont4.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont4.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont4.Caption = "Sconto 4"
    Me.mo_scont4.Enabled = True
    Me.mo_scont4.FieldName = "mo_scont4"
    Me.mo_scont4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont4.Name = "mo_scont4"
    Me.mo_scont4.NTSRepositoryComboBox = Nothing
    Me.mo_scont4.NTSRepositoryItemCheck = Nothing
    Me.mo_scont4.NTSRepositoryItemMemo = Nothing
    Me.mo_scont4.NTSRepositoryItemText = Nothing
    Me.mo_scont4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont4.OptionsFilter.AllowFilter = False
    '
    'mo_scont5
    '
    Me.mo_scont5.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont5.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont5.Caption = "Sconto 5"
    Me.mo_scont5.Enabled = True
    Me.mo_scont5.FieldName = "mo_scont5"
    Me.mo_scont5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont5.Name = "mo_scont5"
    Me.mo_scont5.NTSRepositoryComboBox = Nothing
    Me.mo_scont5.NTSRepositoryItemCheck = Nothing
    Me.mo_scont5.NTSRepositoryItemMemo = Nothing
    Me.mo_scont5.NTSRepositoryItemText = Nothing
    Me.mo_scont5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont5.OptionsFilter.AllowFilter = False
    '
    'mo_scont6
    '
    Me.mo_scont6.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont6.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont6.Caption = "Sconto 6 "
    Me.mo_scont6.Enabled = True
    Me.mo_scont6.FieldName = "mo_scont6"
    Me.mo_scont6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont6.Name = "mo_scont6"
    Me.mo_scont6.NTSRepositoryComboBox = Nothing
    Me.mo_scont6.NTSRepositoryItemCheck = Nothing
    Me.mo_scont6.NTSRepositoryItemMemo = Nothing
    Me.mo_scont6.NTSRepositoryItemText = Nothing
    Me.mo_scont6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont6.OptionsFilter.AllowFilter = False
    '
    'mo_ubicaz
    '
    Me.mo_ubicaz.AppearanceCell.Options.UseBackColor = True
    Me.mo_ubicaz.AppearanceCell.Options.UseTextOptions = True
    Me.mo_ubicaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_ubicaz.Caption = "Ubicaz."
    Me.mo_ubicaz.Enabled = True
    Me.mo_ubicaz.FieldName = "mo_ubicaz"
    Me.mo_ubicaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_ubicaz.Name = "mo_ubicaz"
    Me.mo_ubicaz.NTSRepositoryComboBox = Nothing
    Me.mo_ubicaz.NTSRepositoryItemCheck = Nothing
    Me.mo_ubicaz.NTSRepositoryItemMemo = Nothing
    Me.mo_ubicaz.NTSRepositoryItemText = Nothing
    Me.mo_ubicaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_ubicaz.OptionsFilter.AllowFilter = False
    Me.mo_ubicaz.Visible = True
    Me.mo_ubicaz.VisibleIndex = 40
    '
    'T1o
    '
    Me.T1o.AppearanceCell.Options.UseBackColor = True
    Me.T1o.AppearanceCell.Options.UseTextOptions = True
    Me.T1o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T1o.Caption = "T1o"
    Me.T1o.Enabled = True
    Me.T1o.FieldName = "T1o"
    Me.T1o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T1o.Name = "T1o"
    Me.T1o.NTSRepositoryComboBox = Nothing
    Me.T1o.NTSRepositoryItemCheck = Nothing
    Me.T1o.NTSRepositoryItemMemo = Nothing
    Me.T1o.NTSRepositoryItemText = Nothing
    Me.T1o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T1o.OptionsFilter.AllowFilter = False
    Me.T1o.Visible = True
    Me.T1o.VisibleIndex = 41
    '
    'T2o
    '
    Me.T2o.AppearanceCell.Options.UseBackColor = True
    Me.T2o.AppearanceCell.Options.UseTextOptions = True
    Me.T2o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T2o.Caption = "T2o"
    Me.T2o.Enabled = True
    Me.T2o.FieldName = "T2o"
    Me.T2o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T2o.Name = "T2o"
    Me.T2o.NTSRepositoryComboBox = Nothing
    Me.T2o.NTSRepositoryItemCheck = Nothing
    Me.T2o.NTSRepositoryItemMemo = Nothing
    Me.T2o.NTSRepositoryItemText = Nothing
    Me.T2o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T2o.OptionsFilter.AllowFilter = False
    Me.T2o.Visible = True
    Me.T2o.VisibleIndex = 42
    '
    'T3o
    '
    Me.T3o.AppearanceCell.Options.UseBackColor = True
    Me.T3o.AppearanceCell.Options.UseTextOptions = True
    Me.T3o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T3o.Caption = "T3o"
    Me.T3o.Enabled = True
    Me.T3o.FieldName = "T3o"
    Me.T3o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T3o.Name = "T3o"
    Me.T3o.NTSRepositoryComboBox = Nothing
    Me.T3o.NTSRepositoryItemCheck = Nothing
    Me.T3o.NTSRepositoryItemMemo = Nothing
    Me.T3o.NTSRepositoryItemText = Nothing
    Me.T3o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T3o.OptionsFilter.AllowFilter = False
    Me.T3o.Visible = True
    Me.T3o.VisibleIndex = 43
    '
    'T4o
    '
    Me.T4o.AppearanceCell.Options.UseBackColor = True
    Me.T4o.AppearanceCell.Options.UseTextOptions = True
    Me.T4o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T4o.Caption = "T4o"
    Me.T4o.Enabled = True
    Me.T4o.FieldName = "T4o"
    Me.T4o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T4o.Name = "T4o"
    Me.T4o.NTSRepositoryComboBox = Nothing
    Me.T4o.NTSRepositoryItemCheck = Nothing
    Me.T4o.NTSRepositoryItemMemo = Nothing
    Me.T4o.NTSRepositoryItemText = Nothing
    Me.T4o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T4o.OptionsFilter.AllowFilter = False
    Me.T4o.Visible = True
    Me.T4o.VisibleIndex = 44
    '
    'T5o
    '
    Me.T5o.AppearanceCell.Options.UseBackColor = True
    Me.T5o.AppearanceCell.Options.UseTextOptions = True
    Me.T5o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T5o.Caption = "T5o"
    Me.T5o.Enabled = True
    Me.T5o.FieldName = "T5o"
    Me.T5o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T5o.Name = "T5o"
    Me.T5o.NTSRepositoryComboBox = Nothing
    Me.T5o.NTSRepositoryItemCheck = Nothing
    Me.T5o.NTSRepositoryItemMemo = Nothing
    Me.T5o.NTSRepositoryItemText = Nothing
    Me.T5o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T5o.OptionsFilter.AllowFilter = False
    Me.T5o.Visible = True
    Me.T5o.VisibleIndex = 45
    '
    'T6o
    '
    Me.T6o.AppearanceCell.Options.UseBackColor = True
    Me.T6o.AppearanceCell.Options.UseTextOptions = True
    Me.T6o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T6o.Caption = "T6o"
    Me.T6o.Enabled = True
    Me.T6o.FieldName = "T6o"
    Me.T6o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T6o.Name = "T6o"
    Me.T6o.NTSRepositoryComboBox = Nothing
    Me.T6o.NTSRepositoryItemCheck = Nothing
    Me.T6o.NTSRepositoryItemMemo = Nothing
    Me.T6o.NTSRepositoryItemText = Nothing
    Me.T6o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T6o.OptionsFilter.AllowFilter = False
    Me.T6o.Visible = True
    Me.T6o.VisibleIndex = 46
    '
    'T7o
    '
    Me.T7o.AppearanceCell.Options.UseBackColor = True
    Me.T7o.AppearanceCell.Options.UseTextOptions = True
    Me.T7o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T7o.Caption = "T7o"
    Me.T7o.Enabled = True
    Me.T7o.FieldName = "T7o"
    Me.T7o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T7o.Name = "T7o"
    Me.T7o.NTSRepositoryComboBox = Nothing
    Me.T7o.NTSRepositoryItemCheck = Nothing
    Me.T7o.NTSRepositoryItemMemo = Nothing
    Me.T7o.NTSRepositoryItemText = Nothing
    Me.T7o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T7o.OptionsFilter.AllowFilter = False
    Me.T7o.Visible = True
    Me.T7o.VisibleIndex = 47
    '
    'T8o
    '
    Me.T8o.AppearanceCell.Options.UseBackColor = True
    Me.T8o.AppearanceCell.Options.UseTextOptions = True
    Me.T8o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T8o.Caption = "T8o"
    Me.T8o.Enabled = True
    Me.T8o.FieldName = "T8o"
    Me.T8o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T8o.Name = "T8o"
    Me.T8o.NTSRepositoryComboBox = Nothing
    Me.T8o.NTSRepositoryItemCheck = Nothing
    Me.T8o.NTSRepositoryItemMemo = Nothing
    Me.T8o.NTSRepositoryItemText = Nothing
    Me.T8o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T8o.OptionsFilter.AllowFilter = False
    Me.T8o.Visible = True
    Me.T8o.VisibleIndex = 48
    '
    'T9o
    '
    Me.T9o.AppearanceCell.Options.UseBackColor = True
    Me.T9o.AppearanceCell.Options.UseTextOptions = True
    Me.T9o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T9o.Caption = "T9o"
    Me.T9o.Enabled = True
    Me.T9o.FieldName = "T9o"
    Me.T9o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T9o.Name = "T9o"
    Me.T9o.NTSRepositoryComboBox = Nothing
    Me.T9o.NTSRepositoryItemCheck = Nothing
    Me.T9o.NTSRepositoryItemMemo = Nothing
    Me.T9o.NTSRepositoryItemText = Nothing
    Me.T9o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T9o.OptionsFilter.AllowFilter = False
    Me.T9o.Visible = True
    Me.T9o.VisibleIndex = 49
    '
    'T10o
    '
    Me.T10o.AppearanceCell.Options.UseBackColor = True
    Me.T10o.AppearanceCell.Options.UseTextOptions = True
    Me.T10o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T10o.Caption = "T10o"
    Me.T10o.Enabled = True
    Me.T10o.FieldName = "T10o"
    Me.T10o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T10o.Name = "T10o"
    Me.T10o.NTSRepositoryComboBox = Nothing
    Me.T10o.NTSRepositoryItemCheck = Nothing
    Me.T10o.NTSRepositoryItemMemo = Nothing
    Me.T10o.NTSRepositoryItemText = Nothing
    Me.T10o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T10o.OptionsFilter.AllowFilter = False
    Me.T10o.Visible = True
    Me.T10o.VisibleIndex = 50
    '
    'T11o
    '
    Me.T11o.AppearanceCell.Options.UseBackColor = True
    Me.T11o.AppearanceCell.Options.UseTextOptions = True
    Me.T11o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T11o.Caption = "T11o"
    Me.T11o.Enabled = True
    Me.T11o.FieldName = "T11o"
    Me.T11o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T11o.Name = "T11o"
    Me.T11o.NTSRepositoryComboBox = Nothing
    Me.T11o.NTSRepositoryItemCheck = Nothing
    Me.T11o.NTSRepositoryItemMemo = Nothing
    Me.T11o.NTSRepositoryItemText = Nothing
    Me.T11o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T11o.OptionsFilter.AllowFilter = False
    Me.T11o.Visible = True
    Me.T11o.VisibleIndex = 51
    '
    'T12o
    '
    Me.T12o.AppearanceCell.Options.UseBackColor = True
    Me.T12o.AppearanceCell.Options.UseTextOptions = True
    Me.T12o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T12o.Caption = "T12o"
    Me.T12o.Enabled = True
    Me.T12o.FieldName = "T12o"
    Me.T12o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T12o.Name = "T12o"
    Me.T12o.NTSRepositoryComboBox = Nothing
    Me.T12o.NTSRepositoryItemCheck = Nothing
    Me.T12o.NTSRepositoryItemMemo = Nothing
    Me.T12o.NTSRepositoryItemText = Nothing
    Me.T12o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T12o.OptionsFilter.AllowFilter = False
    Me.T12o.Visible = True
    Me.T12o.VisibleIndex = 52
    '
    'T13o
    '
    Me.T13o.AppearanceCell.Options.UseBackColor = True
    Me.T13o.AppearanceCell.Options.UseTextOptions = True
    Me.T13o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T13o.Caption = "T13o"
    Me.T13o.Enabled = True
    Me.T13o.FieldName = "T13o"
    Me.T13o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T13o.Name = "T13o"
    Me.T13o.NTSRepositoryComboBox = Nothing
    Me.T13o.NTSRepositoryItemCheck = Nothing
    Me.T13o.NTSRepositoryItemMemo = Nothing
    Me.T13o.NTSRepositoryItemText = Nothing
    Me.T13o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T13o.OptionsFilter.AllowFilter = False
    Me.T13o.Visible = True
    Me.T13o.VisibleIndex = 53
    '
    'T14o
    '
    Me.T14o.AppearanceCell.Options.UseBackColor = True
    Me.T14o.AppearanceCell.Options.UseTextOptions = True
    Me.T14o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T14o.Caption = "T14o"
    Me.T14o.Enabled = True
    Me.T14o.FieldName = "T14o"
    Me.T14o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T14o.Name = "T14o"
    Me.T14o.NTSRepositoryComboBox = Nothing
    Me.T14o.NTSRepositoryItemCheck = Nothing
    Me.T14o.NTSRepositoryItemMemo = Nothing
    Me.T14o.NTSRepositoryItemText = Nothing
    Me.T14o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T14o.OptionsFilter.AllowFilter = False
    Me.T14o.Visible = True
    Me.T14o.VisibleIndex = 54
    '
    'T15o
    '
    Me.T15o.AppearanceCell.Options.UseBackColor = True
    Me.T15o.AppearanceCell.Options.UseTextOptions = True
    Me.T15o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T15o.Caption = "T15o"
    Me.T15o.Enabled = True
    Me.T15o.FieldName = "T15o"
    Me.T15o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T15o.Name = "T15o"
    Me.T15o.NTSRepositoryComboBox = Nothing
    Me.T15o.NTSRepositoryItemCheck = Nothing
    Me.T15o.NTSRepositoryItemMemo = Nothing
    Me.T15o.NTSRepositoryItemText = Nothing
    Me.T15o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T15o.OptionsFilter.AllowFilter = False
    Me.T15o.Visible = True
    Me.T15o.VisibleIndex = 55
    '
    'T16o
    '
    Me.T16o.AppearanceCell.Options.UseBackColor = True
    Me.T16o.AppearanceCell.Options.UseTextOptions = True
    Me.T16o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T16o.Caption = "T16o"
    Me.T16o.Enabled = True
    Me.T16o.FieldName = "T16o"
    Me.T16o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T16o.Name = "T16o"
    Me.T16o.NTSRepositoryComboBox = Nothing
    Me.T16o.NTSRepositoryItemCheck = Nothing
    Me.T16o.NTSRepositoryItemMemo = Nothing
    Me.T16o.NTSRepositoryItemText = Nothing
    Me.T16o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T16o.OptionsFilter.AllowFilter = False
    Me.T16o.Visible = True
    Me.T16o.VisibleIndex = 56
    '
    'T17o
    '
    Me.T17o.AppearanceCell.Options.UseBackColor = True
    Me.T17o.AppearanceCell.Options.UseTextOptions = True
    Me.T17o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T17o.Caption = "T17o"
    Me.T17o.Enabled = True
    Me.T17o.FieldName = "T17o"
    Me.T17o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T17o.Name = "T17o"
    Me.T17o.NTSRepositoryComboBox = Nothing
    Me.T17o.NTSRepositoryItemCheck = Nothing
    Me.T17o.NTSRepositoryItemMemo = Nothing
    Me.T17o.NTSRepositoryItemText = Nothing
    Me.T17o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T17o.OptionsFilter.AllowFilter = False
    Me.T17o.Visible = True
    Me.T17o.VisibleIndex = 57
    '
    'T18o
    '
    Me.T18o.AppearanceCell.Options.UseBackColor = True
    Me.T18o.AppearanceCell.Options.UseTextOptions = True
    Me.T18o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T18o.Caption = "T18o"
    Me.T18o.Enabled = True
    Me.T18o.FieldName = "T18o"
    Me.T18o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T18o.Name = "T18o"
    Me.T18o.NTSRepositoryComboBox = Nothing
    Me.T18o.NTSRepositoryItemCheck = Nothing
    Me.T18o.NTSRepositoryItemMemo = Nothing
    Me.T18o.NTSRepositoryItemText = Nothing
    Me.T18o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T18o.OptionsFilter.AllowFilter = False
    Me.T18o.Visible = True
    Me.T18o.VisibleIndex = 58
    '
    'T19o
    '
    Me.T19o.AppearanceCell.Options.UseBackColor = True
    Me.T19o.AppearanceCell.Options.UseTextOptions = True
    Me.T19o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T19o.Caption = "T19o"
    Me.T19o.Enabled = True
    Me.T19o.FieldName = "T19o"
    Me.T19o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T19o.Name = "T19o"
    Me.T19o.NTSRepositoryComboBox = Nothing
    Me.T19o.NTSRepositoryItemCheck = Nothing
    Me.T19o.NTSRepositoryItemMemo = Nothing
    Me.T19o.NTSRepositoryItemText = Nothing
    Me.T19o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T19o.OptionsFilter.AllowFilter = False
    Me.T19o.Visible = True
    Me.T19o.VisibleIndex = 59
    '
    'T20o
    '
    Me.T20o.AppearanceCell.Options.UseBackColor = True
    Me.T20o.AppearanceCell.Options.UseTextOptions = True
    Me.T20o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T20o.Caption = "T20o"
    Me.T20o.Enabled = True
    Me.T20o.FieldName = "T20o"
    Me.T20o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T20o.Name = "T20o"
    Me.T20o.NTSRepositoryComboBox = Nothing
    Me.T20o.NTSRepositoryItemCheck = Nothing
    Me.T20o.NTSRepositoryItemMemo = Nothing
    Me.T20o.NTSRepositoryItemText = Nothing
    Me.T20o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T20o.OptionsFilter.AllowFilter = False
    Me.T20o.Visible = True
    Me.T20o.VisibleIndex = 60
    '
    'T21o
    '
    Me.T21o.AppearanceCell.Options.UseBackColor = True
    Me.T21o.AppearanceCell.Options.UseTextOptions = True
    Me.T21o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T21o.Caption = "T21o"
    Me.T21o.Enabled = True
    Me.T21o.FieldName = "T21o"
    Me.T21o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T21o.Name = "T21o"
    Me.T21o.NTSRepositoryComboBox = Nothing
    Me.T21o.NTSRepositoryItemCheck = Nothing
    Me.T21o.NTSRepositoryItemMemo = Nothing
    Me.T21o.NTSRepositoryItemText = Nothing
    Me.T21o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T21o.OptionsFilter.AllowFilter = False
    Me.T21o.Visible = True
    Me.T21o.VisibleIndex = 61
    '
    'T22o
    '
    Me.T22o.AppearanceCell.Options.UseBackColor = True
    Me.T22o.AppearanceCell.Options.UseTextOptions = True
    Me.T22o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T22o.Caption = "T22o"
    Me.T22o.Enabled = True
    Me.T22o.FieldName = "T22o"
    Me.T22o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T22o.Name = "T22o"
    Me.T22o.NTSRepositoryComboBox = Nothing
    Me.T22o.NTSRepositoryItemCheck = Nothing
    Me.T22o.NTSRepositoryItemMemo = Nothing
    Me.T22o.NTSRepositoryItemText = Nothing
    Me.T22o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T22o.OptionsFilter.AllowFilter = False
    Me.T22o.Visible = True
    Me.T22o.VisibleIndex = 62
    '
    'T23o
    '
    Me.T23o.AppearanceCell.Options.UseBackColor = True
    Me.T23o.AppearanceCell.Options.UseTextOptions = True
    Me.T23o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T23o.Caption = "T23o"
    Me.T23o.Enabled = True
    Me.T23o.FieldName = "T23o"
    Me.T23o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T23o.Name = "T23o"
    Me.T23o.NTSRepositoryComboBox = Nothing
    Me.T23o.NTSRepositoryItemCheck = Nothing
    Me.T23o.NTSRepositoryItemMemo = Nothing
    Me.T23o.NTSRepositoryItemText = Nothing
    Me.T23o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T23o.OptionsFilter.AllowFilter = False
    Me.T23o.Visible = True
    Me.T23o.VisibleIndex = 63
    '
    'T24o
    '
    Me.T24o.AppearanceCell.Options.UseBackColor = True
    Me.T24o.AppearanceCell.Options.UseTextOptions = True
    Me.T24o.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T24o.Caption = "T24o"
    Me.T24o.Enabled = True
    Me.T24o.FieldName = "T24o"
    Me.T24o.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T24o.Name = "T24o"
    Me.T24o.NTSRepositoryComboBox = Nothing
    Me.T24o.NTSRepositoryItemCheck = Nothing
    Me.T24o.NTSRepositoryItemMemo = Nothing
    Me.T24o.NTSRepositoryItemText = Nothing
    Me.T24o.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T24o.OptionsFilter.AllowFilter = False
    Me.T24o.Visible = True
    Me.T24o.VisibleIndex = 64
    '
    'T1c
    '
    Me.T1c.AppearanceCell.Options.UseBackColor = True
    Me.T1c.AppearanceCell.Options.UseTextOptions = True
    Me.T1c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T1c.Caption = "T1c"
    Me.T1c.Enabled = True
    Me.T1c.FieldName = "T1c"
    Me.T1c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T1c.Name = "T1c"
    Me.T1c.NTSRepositoryComboBox = Nothing
    Me.T1c.NTSRepositoryItemCheck = Nothing
    Me.T1c.NTSRepositoryItemMemo = Nothing
    Me.T1c.NTSRepositoryItemText = Nothing
    Me.T1c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T1c.OptionsFilter.AllowFilter = False
    Me.T1c.Visible = True
    Me.T1c.VisibleIndex = 65
    '
    'T2c
    '
    Me.T2c.AppearanceCell.Options.UseBackColor = True
    Me.T2c.AppearanceCell.Options.UseTextOptions = True
    Me.T2c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T2c.Caption = "T2c"
    Me.T2c.Enabled = True
    Me.T2c.FieldName = "T2c"
    Me.T2c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T2c.Name = "T2c"
    Me.T2c.NTSRepositoryComboBox = Nothing
    Me.T2c.NTSRepositoryItemCheck = Nothing
    Me.T2c.NTSRepositoryItemMemo = Nothing
    Me.T2c.NTSRepositoryItemText = Nothing
    Me.T2c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T2c.OptionsFilter.AllowFilter = False
    Me.T2c.Visible = True
    Me.T2c.VisibleIndex = 66
    '
    'T3c
    '
    Me.T3c.AppearanceCell.Options.UseBackColor = True
    Me.T3c.AppearanceCell.Options.UseTextOptions = True
    Me.T3c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T3c.Caption = "T3c"
    Me.T3c.Enabled = True
    Me.T3c.FieldName = "T3c"
    Me.T3c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T3c.Name = "T3c"
    Me.T3c.NTSRepositoryComboBox = Nothing
    Me.T3c.NTSRepositoryItemCheck = Nothing
    Me.T3c.NTSRepositoryItemMemo = Nothing
    Me.T3c.NTSRepositoryItemText = Nothing
    Me.T3c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T3c.OptionsFilter.AllowFilter = False
    Me.T3c.Visible = True
    Me.T3c.VisibleIndex = 67
    '
    'T4c
    '
    Me.T4c.AppearanceCell.Options.UseBackColor = True
    Me.T4c.AppearanceCell.Options.UseTextOptions = True
    Me.T4c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T4c.Caption = "T4c"
    Me.T4c.Enabled = True
    Me.T4c.FieldName = "T4c"
    Me.T4c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T4c.Name = "T4c"
    Me.T4c.NTSRepositoryComboBox = Nothing
    Me.T4c.NTSRepositoryItemCheck = Nothing
    Me.T4c.NTSRepositoryItemMemo = Nothing
    Me.T4c.NTSRepositoryItemText = Nothing
    Me.T4c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T4c.OptionsFilter.AllowFilter = False
    Me.T4c.Visible = True
    Me.T4c.VisibleIndex = 68
    '
    'T5c
    '
    Me.T5c.AppearanceCell.Options.UseBackColor = True
    Me.T5c.AppearanceCell.Options.UseTextOptions = True
    Me.T5c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T5c.Caption = "T5c"
    Me.T5c.Enabled = True
    Me.T5c.FieldName = "T5c"
    Me.T5c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T5c.Name = "T5c"
    Me.T5c.NTSRepositoryComboBox = Nothing
    Me.T5c.NTSRepositoryItemCheck = Nothing
    Me.T5c.NTSRepositoryItemMemo = Nothing
    Me.T5c.NTSRepositoryItemText = Nothing
    Me.T5c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T5c.OptionsFilter.AllowFilter = False
    Me.T5c.Visible = True
    Me.T5c.VisibleIndex = 69
    '
    'T6c
    '
    Me.T6c.AppearanceCell.Options.UseBackColor = True
    Me.T6c.AppearanceCell.Options.UseTextOptions = True
    Me.T6c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T6c.Caption = "T6c"
    Me.T6c.Enabled = True
    Me.T6c.FieldName = "T6c"
    Me.T6c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T6c.Name = "T6c"
    Me.T6c.NTSRepositoryComboBox = Nothing
    Me.T6c.NTSRepositoryItemCheck = Nothing
    Me.T6c.NTSRepositoryItemMemo = Nothing
    Me.T6c.NTSRepositoryItemText = Nothing
    Me.T6c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T6c.OptionsFilter.AllowFilter = False
    Me.T6c.Visible = True
    Me.T6c.VisibleIndex = 70
    '
    'T7c
    '
    Me.T7c.AppearanceCell.Options.UseBackColor = True
    Me.T7c.AppearanceCell.Options.UseTextOptions = True
    Me.T7c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T7c.Caption = "T7c"
    Me.T7c.Enabled = True
    Me.T7c.FieldName = "T7c"
    Me.T7c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T7c.Name = "T7c"
    Me.T7c.NTSRepositoryComboBox = Nothing
    Me.T7c.NTSRepositoryItemCheck = Nothing
    Me.T7c.NTSRepositoryItemMemo = Nothing
    Me.T7c.NTSRepositoryItemText = Nothing
    Me.T7c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T7c.OptionsFilter.AllowFilter = False
    Me.T7c.Visible = True
    Me.T7c.VisibleIndex = 71
    '
    'T8c
    '
    Me.T8c.AppearanceCell.Options.UseBackColor = True
    Me.T8c.AppearanceCell.Options.UseTextOptions = True
    Me.T8c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T8c.Caption = "T8c"
    Me.T8c.Enabled = True
    Me.T8c.FieldName = "T8c"
    Me.T8c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T8c.Name = "T8c"
    Me.T8c.NTSRepositoryComboBox = Nothing
    Me.T8c.NTSRepositoryItemCheck = Nothing
    Me.T8c.NTSRepositoryItemMemo = Nothing
    Me.T8c.NTSRepositoryItemText = Nothing
    Me.T8c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T8c.OptionsFilter.AllowFilter = False
    Me.T8c.Visible = True
    Me.T8c.VisibleIndex = 72
    '
    'T9c
    '
    Me.T9c.AppearanceCell.Options.UseBackColor = True
    Me.T9c.AppearanceCell.Options.UseTextOptions = True
    Me.T9c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T9c.Caption = "T9c"
    Me.T9c.Enabled = True
    Me.T9c.FieldName = "T9c"
    Me.T9c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T9c.Name = "T9c"
    Me.T9c.NTSRepositoryComboBox = Nothing
    Me.T9c.NTSRepositoryItemCheck = Nothing
    Me.T9c.NTSRepositoryItemMemo = Nothing
    Me.T9c.NTSRepositoryItemText = Nothing
    Me.T9c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T9c.OptionsFilter.AllowFilter = False
    Me.T9c.Visible = True
    Me.T9c.VisibleIndex = 73
    '
    'T10c
    '
    Me.T10c.AppearanceCell.Options.UseBackColor = True
    Me.T10c.AppearanceCell.Options.UseTextOptions = True
    Me.T10c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T10c.Caption = "T10c"
    Me.T10c.Enabled = True
    Me.T10c.FieldName = "T10c"
    Me.T10c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T10c.Name = "T10c"
    Me.T10c.NTSRepositoryComboBox = Nothing
    Me.T10c.NTSRepositoryItemCheck = Nothing
    Me.T10c.NTSRepositoryItemMemo = Nothing
    Me.T10c.NTSRepositoryItemText = Nothing
    Me.T10c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T10c.OptionsFilter.AllowFilter = False
    Me.T10c.Visible = True
    Me.T10c.VisibleIndex = 74
    '
    'T11c
    '
    Me.T11c.AppearanceCell.Options.UseBackColor = True
    Me.T11c.AppearanceCell.Options.UseTextOptions = True
    Me.T11c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T11c.Caption = "T11c"
    Me.T11c.Enabled = True
    Me.T11c.FieldName = "T11c"
    Me.T11c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T11c.Name = "T11c"
    Me.T11c.NTSRepositoryComboBox = Nothing
    Me.T11c.NTSRepositoryItemCheck = Nothing
    Me.T11c.NTSRepositoryItemMemo = Nothing
    Me.T11c.NTSRepositoryItemText = Nothing
    Me.T11c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T11c.OptionsFilter.AllowFilter = False
    Me.T11c.Visible = True
    Me.T11c.VisibleIndex = 75
    '
    'T12c
    '
    Me.T12c.AppearanceCell.Options.UseBackColor = True
    Me.T12c.AppearanceCell.Options.UseTextOptions = True
    Me.T12c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T12c.Caption = "T12c"
    Me.T12c.Enabled = True
    Me.T12c.FieldName = "T12c"
    Me.T12c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T12c.Name = "T12c"
    Me.T12c.NTSRepositoryComboBox = Nothing
    Me.T12c.NTSRepositoryItemCheck = Nothing
    Me.T12c.NTSRepositoryItemMemo = Nothing
    Me.T12c.NTSRepositoryItemText = Nothing
    Me.T12c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T12c.OptionsFilter.AllowFilter = False
    Me.T12c.Visible = True
    Me.T12c.VisibleIndex = 76
    '
    'T13c
    '
    Me.T13c.AppearanceCell.Options.UseBackColor = True
    Me.T13c.AppearanceCell.Options.UseTextOptions = True
    Me.T13c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T13c.Caption = "T13c"
    Me.T13c.Enabled = True
    Me.T13c.FieldName = "T13c"
    Me.T13c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T13c.Name = "T13c"
    Me.T13c.NTSRepositoryComboBox = Nothing
    Me.T13c.NTSRepositoryItemCheck = Nothing
    Me.T13c.NTSRepositoryItemMemo = Nothing
    Me.T13c.NTSRepositoryItemText = Nothing
    Me.T13c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T13c.OptionsFilter.AllowFilter = False
    Me.T13c.Visible = True
    Me.T13c.VisibleIndex = 77
    '
    'T14c
    '
    Me.T14c.AppearanceCell.Options.UseBackColor = True
    Me.T14c.AppearanceCell.Options.UseTextOptions = True
    Me.T14c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T14c.Caption = "T14c"
    Me.T14c.Enabled = True
    Me.T14c.FieldName = "T14c"
    Me.T14c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T14c.Name = "T14c"
    Me.T14c.NTSRepositoryComboBox = Nothing
    Me.T14c.NTSRepositoryItemCheck = Nothing
    Me.T14c.NTSRepositoryItemMemo = Nothing
    Me.T14c.NTSRepositoryItemText = Nothing
    Me.T14c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T14c.OptionsFilter.AllowFilter = False
    Me.T14c.Visible = True
    Me.T14c.VisibleIndex = 78
    '
    'T15c
    '
    Me.T15c.AppearanceCell.Options.UseBackColor = True
    Me.T15c.AppearanceCell.Options.UseTextOptions = True
    Me.T15c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T15c.Caption = "T15c"
    Me.T15c.Enabled = True
    Me.T15c.FieldName = "T15c"
    Me.T15c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T15c.Name = "T15c"
    Me.T15c.NTSRepositoryComboBox = Nothing
    Me.T15c.NTSRepositoryItemCheck = Nothing
    Me.T15c.NTSRepositoryItemMemo = Nothing
    Me.T15c.NTSRepositoryItemText = Nothing
    Me.T15c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T15c.OptionsFilter.AllowFilter = False
    Me.T15c.Visible = True
    Me.T15c.VisibleIndex = 79
    '
    'T16c
    '
    Me.T16c.AppearanceCell.Options.UseBackColor = True
    Me.T16c.AppearanceCell.Options.UseTextOptions = True
    Me.T16c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T16c.Caption = "T16c"
    Me.T16c.Enabled = True
    Me.T16c.FieldName = "T16c"
    Me.T16c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T16c.Name = "T16c"
    Me.T16c.NTSRepositoryComboBox = Nothing
    Me.T16c.NTSRepositoryItemCheck = Nothing
    Me.T16c.NTSRepositoryItemMemo = Nothing
    Me.T16c.NTSRepositoryItemText = Nothing
    Me.T16c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T16c.OptionsFilter.AllowFilter = False
    Me.T16c.Visible = True
    Me.T16c.VisibleIndex = 80
    '
    'T17c
    '
    Me.T17c.AppearanceCell.Options.UseBackColor = True
    Me.T17c.AppearanceCell.Options.UseTextOptions = True
    Me.T17c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T17c.Caption = "T17c"
    Me.T17c.Enabled = True
    Me.T17c.FieldName = "T17c"
    Me.T17c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T17c.Name = "T17c"
    Me.T17c.NTSRepositoryComboBox = Nothing
    Me.T17c.NTSRepositoryItemCheck = Nothing
    Me.T17c.NTSRepositoryItemMemo = Nothing
    Me.T17c.NTSRepositoryItemText = Nothing
    Me.T17c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T17c.OptionsFilter.AllowFilter = False
    Me.T17c.Visible = True
    Me.T17c.VisibleIndex = 81
    '
    'T18c
    '
    Me.T18c.AppearanceCell.Options.UseBackColor = True
    Me.T18c.AppearanceCell.Options.UseTextOptions = True
    Me.T18c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T18c.Caption = "T18c"
    Me.T18c.Enabled = True
    Me.T18c.FieldName = "T18c"
    Me.T18c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T18c.Name = "T18c"
    Me.T18c.NTSRepositoryComboBox = Nothing
    Me.T18c.NTSRepositoryItemCheck = Nothing
    Me.T18c.NTSRepositoryItemMemo = Nothing
    Me.T18c.NTSRepositoryItemText = Nothing
    Me.T18c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T18c.OptionsFilter.AllowFilter = False
    Me.T18c.Visible = True
    Me.T18c.VisibleIndex = 82
    '
    'T19c
    '
    Me.T19c.AppearanceCell.Options.UseBackColor = True
    Me.T19c.AppearanceCell.Options.UseTextOptions = True
    Me.T19c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T19c.Caption = "T19c"
    Me.T19c.Enabled = True
    Me.T19c.FieldName = "T19c"
    Me.T19c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T19c.Name = "T19c"
    Me.T19c.NTSRepositoryComboBox = Nothing
    Me.T19c.NTSRepositoryItemCheck = Nothing
    Me.T19c.NTSRepositoryItemMemo = Nothing
    Me.T19c.NTSRepositoryItemText = Nothing
    Me.T19c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T19c.OptionsFilter.AllowFilter = False
    Me.T19c.Visible = True
    Me.T19c.VisibleIndex = 83
    '
    'T20c
    '
    Me.T20c.AppearanceCell.Options.UseBackColor = True
    Me.T20c.AppearanceCell.Options.UseTextOptions = True
    Me.T20c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T20c.Caption = "T20c"
    Me.T20c.Enabled = True
    Me.T20c.FieldName = "T20c"
    Me.T20c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T20c.Name = "T20c"
    Me.T20c.NTSRepositoryComboBox = Nothing
    Me.T20c.NTSRepositoryItemCheck = Nothing
    Me.T20c.NTSRepositoryItemMemo = Nothing
    Me.T20c.NTSRepositoryItemText = Nothing
    Me.T20c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T20c.OptionsFilter.AllowFilter = False
    Me.T20c.Visible = True
    Me.T20c.VisibleIndex = 84
    '
    'T21c
    '
    Me.T21c.AppearanceCell.Options.UseBackColor = True
    Me.T21c.AppearanceCell.Options.UseTextOptions = True
    Me.T21c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T21c.Caption = "T21c"
    Me.T21c.Enabled = True
    Me.T21c.FieldName = "T21c"
    Me.T21c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T21c.Name = "T21c"
    Me.T21c.NTSRepositoryComboBox = Nothing
    Me.T21c.NTSRepositoryItemCheck = Nothing
    Me.T21c.NTSRepositoryItemMemo = Nothing
    Me.T21c.NTSRepositoryItemText = Nothing
    Me.T21c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T21c.OptionsFilter.AllowFilter = False
    Me.T21c.Visible = True
    Me.T21c.VisibleIndex = 85
    '
    'T22c
    '
    Me.T22c.AppearanceCell.Options.UseBackColor = True
    Me.T22c.AppearanceCell.Options.UseTextOptions = True
    Me.T22c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T22c.Caption = "T22c"
    Me.T22c.Enabled = True
    Me.T22c.FieldName = "T22c"
    Me.T22c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T22c.Name = "T22c"
    Me.T22c.NTSRepositoryComboBox = Nothing
    Me.T22c.NTSRepositoryItemCheck = Nothing
    Me.T22c.NTSRepositoryItemMemo = Nothing
    Me.T22c.NTSRepositoryItemText = Nothing
    Me.T22c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T22c.OptionsFilter.AllowFilter = False
    Me.T22c.Visible = True
    Me.T22c.VisibleIndex = 86
    '
    'T23c
    '
    Me.T23c.AppearanceCell.Options.UseBackColor = True
    Me.T23c.AppearanceCell.Options.UseTextOptions = True
    Me.T23c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T23c.Caption = "T23c"
    Me.T23c.Enabled = True
    Me.T23c.FieldName = "T23c"
    Me.T23c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T23c.Name = "T23c"
    Me.T23c.NTSRepositoryComboBox = Nothing
    Me.T23c.NTSRepositoryItemCheck = Nothing
    Me.T23c.NTSRepositoryItemMemo = Nothing
    Me.T23c.NTSRepositoryItemText = Nothing
    Me.T23c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T23c.OptionsFilter.AllowFilter = False
    Me.T23c.Visible = True
    Me.T23c.VisibleIndex = 87
    '
    'T24c
    '
    Me.T24c.AppearanceCell.Options.UseBackColor = True
    Me.T24c.AppearanceCell.Options.UseTextOptions = True
    Me.T24c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T24c.Caption = "T24c"
    Me.T24c.Enabled = True
    Me.T24c.FieldName = "T24c"
    Me.T24c.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T24c.Name = "T24c"
    Me.T24c.NTSRepositoryComboBox = Nothing
    Me.T24c.NTSRepositoryItemCheck = Nothing
    Me.T24c.NTSRepositoryItemMemo = Nothing
    Me.T24c.NTSRepositoryItemText = Nothing
    Me.T24c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T24c.OptionsFilter.AllowFilter = False
    Me.T24c.Visible = True
    Me.T24c.VisibleIndex = 88
    '
    'T1r
    '
    Me.T1r.AppearanceCell.Options.UseBackColor = True
    Me.T1r.AppearanceCell.Options.UseTextOptions = True
    Me.T1r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T1r.Caption = "T1r"
    Me.T1r.Enabled = True
    Me.T1r.FieldName = "T1r"
    Me.T1r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T1r.Name = "T1r"
    Me.T1r.NTSRepositoryComboBox = Nothing
    Me.T1r.NTSRepositoryItemCheck = Nothing
    Me.T1r.NTSRepositoryItemMemo = Nothing
    Me.T1r.NTSRepositoryItemText = Nothing
    Me.T1r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T1r.OptionsFilter.AllowFilter = False
    Me.T1r.Visible = True
    Me.T1r.VisibleIndex = 89
    '
    'T2r
    '
    Me.T2r.AppearanceCell.Options.UseBackColor = True
    Me.T2r.AppearanceCell.Options.UseTextOptions = True
    Me.T2r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T2r.Caption = "T2r"
    Me.T2r.Enabled = True
    Me.T2r.FieldName = "T2r"
    Me.T2r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T2r.Name = "T2r"
    Me.T2r.NTSRepositoryComboBox = Nothing
    Me.T2r.NTSRepositoryItemCheck = Nothing
    Me.T2r.NTSRepositoryItemMemo = Nothing
    Me.T2r.NTSRepositoryItemText = Nothing
    Me.T2r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T2r.OptionsFilter.AllowFilter = False
    Me.T2r.Visible = True
    Me.T2r.VisibleIndex = 90
    '
    'T3r
    '
    Me.T3r.AppearanceCell.Options.UseBackColor = True
    Me.T3r.AppearanceCell.Options.UseTextOptions = True
    Me.T3r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T3r.Caption = "T3r"
    Me.T3r.Enabled = True
    Me.T3r.FieldName = "T3r"
    Me.T3r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T3r.Name = "T3r"
    Me.T3r.NTSRepositoryComboBox = Nothing
    Me.T3r.NTSRepositoryItemCheck = Nothing
    Me.T3r.NTSRepositoryItemMemo = Nothing
    Me.T3r.NTSRepositoryItemText = Nothing
    Me.T3r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T3r.OptionsFilter.AllowFilter = False
    Me.T3r.Visible = True
    Me.T3r.VisibleIndex = 91
    '
    'T4r
    '
    Me.T4r.AppearanceCell.Options.UseBackColor = True
    Me.T4r.AppearanceCell.Options.UseTextOptions = True
    Me.T4r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T4r.Caption = "T4r"
    Me.T4r.Enabled = True
    Me.T4r.FieldName = "T4r"
    Me.T4r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T4r.Name = "T4r"
    Me.T4r.NTSRepositoryComboBox = Nothing
    Me.T4r.NTSRepositoryItemCheck = Nothing
    Me.T4r.NTSRepositoryItemMemo = Nothing
    Me.T4r.NTSRepositoryItemText = Nothing
    Me.T4r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T4r.OptionsFilter.AllowFilter = False
    Me.T4r.Visible = True
    Me.T4r.VisibleIndex = 92
    '
    'T5r
    '
    Me.T5r.AppearanceCell.Options.UseBackColor = True
    Me.T5r.AppearanceCell.Options.UseTextOptions = True
    Me.T5r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T5r.Caption = "T5r"
    Me.T5r.Enabled = True
    Me.T5r.FieldName = "T5r"
    Me.T5r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T5r.Name = "T5r"
    Me.T5r.NTSRepositoryComboBox = Nothing
    Me.T5r.NTSRepositoryItemCheck = Nothing
    Me.T5r.NTSRepositoryItemMemo = Nothing
    Me.T5r.NTSRepositoryItemText = Nothing
    Me.T5r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T5r.OptionsFilter.AllowFilter = False
    Me.T5r.Visible = True
    Me.T5r.VisibleIndex = 93
    '
    'T6r
    '
    Me.T6r.AppearanceCell.Options.UseBackColor = True
    Me.T6r.AppearanceCell.Options.UseTextOptions = True
    Me.T6r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T6r.Caption = "T6r"
    Me.T6r.Enabled = True
    Me.T6r.FieldName = "T6r"
    Me.T6r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T6r.Name = "T6r"
    Me.T6r.NTSRepositoryComboBox = Nothing
    Me.T6r.NTSRepositoryItemCheck = Nothing
    Me.T6r.NTSRepositoryItemMemo = Nothing
    Me.T6r.NTSRepositoryItemText = Nothing
    Me.T6r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T6r.OptionsFilter.AllowFilter = False
    Me.T6r.Visible = True
    Me.T6r.VisibleIndex = 94
    '
    'T7r
    '
    Me.T7r.AppearanceCell.Options.UseBackColor = True
    Me.T7r.AppearanceCell.Options.UseTextOptions = True
    Me.T7r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T7r.Caption = "T7r"
    Me.T7r.Enabled = True
    Me.T7r.FieldName = "T7r"
    Me.T7r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T7r.Name = "T7r"
    Me.T7r.NTSRepositoryComboBox = Nothing
    Me.T7r.NTSRepositoryItemCheck = Nothing
    Me.T7r.NTSRepositoryItemMemo = Nothing
    Me.T7r.NTSRepositoryItemText = Nothing
    Me.T7r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T7r.OptionsFilter.AllowFilter = False
    Me.T7r.Visible = True
    Me.T7r.VisibleIndex = 95
    '
    'T8r
    '
    Me.T8r.AppearanceCell.Options.UseBackColor = True
    Me.T8r.AppearanceCell.Options.UseTextOptions = True
    Me.T8r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T8r.Caption = "T8r"
    Me.T8r.Enabled = True
    Me.T8r.FieldName = "T8r"
    Me.T8r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T8r.Name = "T8r"
    Me.T8r.NTSRepositoryComboBox = Nothing
    Me.T8r.NTSRepositoryItemCheck = Nothing
    Me.T8r.NTSRepositoryItemMemo = Nothing
    Me.T8r.NTSRepositoryItemText = Nothing
    Me.T8r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T8r.OptionsFilter.AllowFilter = False
    Me.T8r.Visible = True
    Me.T8r.VisibleIndex = 96
    '
    'T9r
    '
    Me.T9r.AppearanceCell.Options.UseBackColor = True
    Me.T9r.AppearanceCell.Options.UseTextOptions = True
    Me.T9r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T9r.Caption = "T9r"
    Me.T9r.Enabled = True
    Me.T9r.FieldName = "T9r"
    Me.T9r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T9r.Name = "T9r"
    Me.T9r.NTSRepositoryComboBox = Nothing
    Me.T9r.NTSRepositoryItemCheck = Nothing
    Me.T9r.NTSRepositoryItemMemo = Nothing
    Me.T9r.NTSRepositoryItemText = Nothing
    Me.T9r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T9r.OptionsFilter.AllowFilter = False
    Me.T9r.Visible = True
    Me.T9r.VisibleIndex = 97
    '
    'T10r
    '
    Me.T10r.AppearanceCell.Options.UseBackColor = True
    Me.T10r.AppearanceCell.Options.UseTextOptions = True
    Me.T10r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T10r.Caption = "T10r"
    Me.T10r.Enabled = True
    Me.T10r.FieldName = "T10r"
    Me.T10r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T10r.Name = "T10r"
    Me.T10r.NTSRepositoryComboBox = Nothing
    Me.T10r.NTSRepositoryItemCheck = Nothing
    Me.T10r.NTSRepositoryItemMemo = Nothing
    Me.T10r.NTSRepositoryItemText = Nothing
    Me.T10r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T10r.OptionsFilter.AllowFilter = False
    Me.T10r.Visible = True
    Me.T10r.VisibleIndex = 98
    '
    'T11r
    '
    Me.T11r.AppearanceCell.Options.UseBackColor = True
    Me.T11r.AppearanceCell.Options.UseTextOptions = True
    Me.T11r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T11r.Caption = "T11r"
    Me.T11r.Enabled = True
    Me.T11r.FieldName = "T11r"
    Me.T11r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T11r.Name = "T11r"
    Me.T11r.NTSRepositoryComboBox = Nothing
    Me.T11r.NTSRepositoryItemCheck = Nothing
    Me.T11r.NTSRepositoryItemMemo = Nothing
    Me.T11r.NTSRepositoryItemText = Nothing
    Me.T11r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T11r.OptionsFilter.AllowFilter = False
    Me.T11r.Visible = True
    Me.T11r.VisibleIndex = 99
    '
    'T12r
    '
    Me.T12r.AppearanceCell.Options.UseBackColor = True
    Me.T12r.AppearanceCell.Options.UseTextOptions = True
    Me.T12r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T12r.Caption = "T12r"
    Me.T12r.Enabled = True
    Me.T12r.FieldName = "T12r"
    Me.T12r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T12r.Name = "T12r"
    Me.T12r.NTSRepositoryComboBox = Nothing
    Me.T12r.NTSRepositoryItemCheck = Nothing
    Me.T12r.NTSRepositoryItemMemo = Nothing
    Me.T12r.NTSRepositoryItemText = Nothing
    Me.T12r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T12r.OptionsFilter.AllowFilter = False
    Me.T12r.Visible = True
    Me.T12r.VisibleIndex = 100
    '
    'T13r
    '
    Me.T13r.AppearanceCell.Options.UseBackColor = True
    Me.T13r.AppearanceCell.Options.UseTextOptions = True
    Me.T13r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T13r.Caption = "T13r"
    Me.T13r.Enabled = True
    Me.T13r.FieldName = "T13r"
    Me.T13r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T13r.Name = "T13r"
    Me.T13r.NTSRepositoryComboBox = Nothing
    Me.T13r.NTSRepositoryItemCheck = Nothing
    Me.T13r.NTSRepositoryItemMemo = Nothing
    Me.T13r.NTSRepositoryItemText = Nothing
    Me.T13r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T13r.OptionsFilter.AllowFilter = False
    Me.T13r.Visible = True
    Me.T13r.VisibleIndex = 101
    '
    'T14r
    '
    Me.T14r.AppearanceCell.Options.UseBackColor = True
    Me.T14r.AppearanceCell.Options.UseTextOptions = True
    Me.T14r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T14r.Caption = "T14r"
    Me.T14r.Enabled = True
    Me.T14r.FieldName = "T14r"
    Me.T14r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T14r.Name = "T14r"
    Me.T14r.NTSRepositoryComboBox = Nothing
    Me.T14r.NTSRepositoryItemCheck = Nothing
    Me.T14r.NTSRepositoryItemMemo = Nothing
    Me.T14r.NTSRepositoryItemText = Nothing
    Me.T14r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T14r.OptionsFilter.AllowFilter = False
    Me.T14r.Visible = True
    Me.T14r.VisibleIndex = 102
    '
    'T15r
    '
    Me.T15r.AppearanceCell.Options.UseBackColor = True
    Me.T15r.AppearanceCell.Options.UseTextOptions = True
    Me.T15r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T15r.Caption = "T15r"
    Me.T15r.Enabled = True
    Me.T15r.FieldName = "T15r"
    Me.T15r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T15r.Name = "T15r"
    Me.T15r.NTSRepositoryComboBox = Nothing
    Me.T15r.NTSRepositoryItemCheck = Nothing
    Me.T15r.NTSRepositoryItemMemo = Nothing
    Me.T15r.NTSRepositoryItemText = Nothing
    Me.T15r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T15r.OptionsFilter.AllowFilter = False
    Me.T15r.Visible = True
    Me.T15r.VisibleIndex = 103
    '
    'T16r
    '
    Me.T16r.AppearanceCell.Options.UseBackColor = True
    Me.T16r.AppearanceCell.Options.UseTextOptions = True
    Me.T16r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T16r.Caption = "T16r"
    Me.T16r.Enabled = True
    Me.T16r.FieldName = "T16r"
    Me.T16r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T16r.Name = "T16r"
    Me.T16r.NTSRepositoryComboBox = Nothing
    Me.T16r.NTSRepositoryItemCheck = Nothing
    Me.T16r.NTSRepositoryItemMemo = Nothing
    Me.T16r.NTSRepositoryItemText = Nothing
    Me.T16r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T16r.OptionsFilter.AllowFilter = False
    Me.T16r.Visible = True
    Me.T16r.VisibleIndex = 104
    '
    'T17r
    '
    Me.T17r.AppearanceCell.Options.UseBackColor = True
    Me.T17r.AppearanceCell.Options.UseTextOptions = True
    Me.T17r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T17r.Caption = "T17r"
    Me.T17r.Enabled = True
    Me.T17r.FieldName = "T17r"
    Me.T17r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T17r.Name = "T17r"
    Me.T17r.NTSRepositoryComboBox = Nothing
    Me.T17r.NTSRepositoryItemCheck = Nothing
    Me.T17r.NTSRepositoryItemMemo = Nothing
    Me.T17r.NTSRepositoryItemText = Nothing
    Me.T17r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T17r.OptionsFilter.AllowFilter = False
    Me.T17r.Visible = True
    Me.T17r.VisibleIndex = 105
    '
    'T18r
    '
    Me.T18r.AppearanceCell.Options.UseBackColor = True
    Me.T18r.AppearanceCell.Options.UseTextOptions = True
    Me.T18r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T18r.Caption = "T18r"
    Me.T18r.Enabled = True
    Me.T18r.FieldName = "T18r"
    Me.T18r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T18r.Name = "T18r"
    Me.T18r.NTSRepositoryComboBox = Nothing
    Me.T18r.NTSRepositoryItemCheck = Nothing
    Me.T18r.NTSRepositoryItemMemo = Nothing
    Me.T18r.NTSRepositoryItemText = Nothing
    Me.T18r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T18r.OptionsFilter.AllowFilter = False
    Me.T18r.Visible = True
    Me.T18r.VisibleIndex = 106
    '
    'T19r
    '
    Me.T19r.AppearanceCell.Options.UseBackColor = True
    Me.T19r.AppearanceCell.Options.UseTextOptions = True
    Me.T19r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T19r.Caption = "T19r"
    Me.T19r.Enabled = True
    Me.T19r.FieldName = "T19r"
    Me.T19r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T19r.Name = "T19r"
    Me.T19r.NTSRepositoryComboBox = Nothing
    Me.T19r.NTSRepositoryItemCheck = Nothing
    Me.T19r.NTSRepositoryItemMemo = Nothing
    Me.T19r.NTSRepositoryItemText = Nothing
    Me.T19r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T19r.OptionsFilter.AllowFilter = False
    Me.T19r.Visible = True
    Me.T19r.VisibleIndex = 107
    '
    'T20r
    '
    Me.T20r.AppearanceCell.Options.UseBackColor = True
    Me.T20r.AppearanceCell.Options.UseTextOptions = True
    Me.T20r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T20r.Caption = "T20r"
    Me.T20r.Enabled = True
    Me.T20r.FieldName = "T20r"
    Me.T20r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T20r.Name = "T20r"
    Me.T20r.NTSRepositoryComboBox = Nothing
    Me.T20r.NTSRepositoryItemCheck = Nothing
    Me.T20r.NTSRepositoryItemMemo = Nothing
    Me.T20r.NTSRepositoryItemText = Nothing
    Me.T20r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T20r.OptionsFilter.AllowFilter = False
    Me.T20r.Visible = True
    Me.T20r.VisibleIndex = 108
    '
    'T21r
    '
    Me.T21r.AppearanceCell.Options.UseBackColor = True
    Me.T21r.AppearanceCell.Options.UseTextOptions = True
    Me.T21r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T21r.Caption = "T21r"
    Me.T21r.Enabled = True
    Me.T21r.FieldName = "T21r"
    Me.T21r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T21r.Name = "T21r"
    Me.T21r.NTSRepositoryComboBox = Nothing
    Me.T21r.NTSRepositoryItemCheck = Nothing
    Me.T21r.NTSRepositoryItemMemo = Nothing
    Me.T21r.NTSRepositoryItemText = Nothing
    Me.T21r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T21r.OptionsFilter.AllowFilter = False
    Me.T21r.Visible = True
    Me.T21r.VisibleIndex = 109
    '
    'T22r
    '
    Me.T22r.AppearanceCell.Options.UseBackColor = True
    Me.T22r.AppearanceCell.Options.UseTextOptions = True
    Me.T22r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T22r.Caption = "T22r"
    Me.T22r.Enabled = True
    Me.T22r.FieldName = "T22r"
    Me.T22r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T22r.Name = "T22r"
    Me.T22r.NTSRepositoryComboBox = Nothing
    Me.T22r.NTSRepositoryItemCheck = Nothing
    Me.T22r.NTSRepositoryItemMemo = Nothing
    Me.T22r.NTSRepositoryItemText = Nothing
    Me.T22r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T22r.OptionsFilter.AllowFilter = False
    Me.T22r.Visible = True
    Me.T22r.VisibleIndex = 110
    '
    'T23r
    '
    Me.T23r.AppearanceCell.Options.UseBackColor = True
    Me.T23r.AppearanceCell.Options.UseTextOptions = True
    Me.T23r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T23r.Caption = "T23r"
    Me.T23r.Enabled = True
    Me.T23r.FieldName = "T23r"
    Me.T23r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T23r.Name = "T23r"
    Me.T23r.NTSRepositoryComboBox = Nothing
    Me.T23r.NTSRepositoryItemCheck = Nothing
    Me.T23r.NTSRepositoryItemMemo = Nothing
    Me.T23r.NTSRepositoryItemText = Nothing
    Me.T23r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T23r.OptionsFilter.AllowFilter = False
    Me.T23r.Visible = True
    Me.T23r.VisibleIndex = 111
    '
    'T24r
    '
    Me.T24r.AppearanceCell.Options.UseBackColor = True
    Me.T24r.AppearanceCell.Options.UseTextOptions = True
    Me.T24r.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.T24r.Caption = "T24r"
    Me.T24r.Enabled = True
    Me.T24r.FieldName = "T24r"
    Me.T24r.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.T24r.Name = "T24r"
    Me.T24r.NTSRepositoryComboBox = Nothing
    Me.T24r.NTSRepositoryItemCheck = Nothing
    Me.T24r.NTSRepositoryItemMemo = Nothing
    Me.T24r.NTSRepositoryItemText = Nothing
    Me.T24r.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.T24r.OptionsFilter.AllowFilter = False
    Me.T24r.Visible = True
    Me.T24r.VisibleIndex = 112
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.edCommeca)
    Me.pnTop.Controls.Add(Me.edMagaz)
    Me.pnTop.Controls.Add(Me.edUnmis)
    Me.pnTop.Controls.Add(Me.edFase)
    Me.pnTop.Controls.Add(Me.edArticolo)
    Me.pnTop.Controls.Add(Me.edAdatcons)
    Me.pnTop.Controls.Add(Me.edDadatcons)
    Me.pnTop.Controls.Add(Me.edEsist)
    Me.pnTop.Controls.Add(Me.edConto)
    Me.pnTop.Controls.Add(Me.lbEsist)
    Me.pnTop.Controls.Add(Me.lbFase)
    Me.pnTop.Controls.Add(Me.lbUnmis)
    Me.pnTop.Controls.Add(Me.lbAdatcons)
    Me.pnTop.Controls.Add(Me.lbDadatcons)
    Me.pnTop.Controls.Add(Me.lbXx_commeca)
    Me.pnTop.Controls.Add(Me.lbCommeca)
    Me.pnTop.Controls.Add(Me.lbXx_Magaz)
    Me.pnTop.Controls.Add(Me.lbMagaz)
    Me.pnTop.Controls.Add(Me.lbXx_articolo)
    Me.pnTop.Controls.Add(Me.lbArticolo)
    Me.pnTop.Controls.Add(Me.lbXx_conto)
    Me.pnTop.Controls.Add(Me.lbConto)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 30)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(660, 102)
    Me.pnTop.TabIndex = 6
    Me.pnTop.Text = "NtsPanel1"
    '
    'edCommeca
    '
    Me.edCommeca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommeca.Location = New System.Drawing.Point(62, 63)
    Me.edCommeca.Name = "edCommeca"
    Me.edCommeca.NTSDbField = ""
    Me.edCommeca.NTSFormat = "0"
    Me.edCommeca.NTSForzaVisZoom = False
    Me.edCommeca.NTSOldValue = ""
    Me.edCommeca.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommeca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommeca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommeca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommeca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCommeca.Properties.MaxLength = 65536
    Me.edCommeca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCommeca.Size = New System.Drawing.Size(74, 20)
    Me.edCommeca.TabIndex = 32
    '
    'edMagaz
    '
    Me.edMagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagaz.Location = New System.Drawing.Point(62, 43)
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
    Me.edMagaz.Size = New System.Drawing.Size(53, 20)
    Me.edMagaz.TabIndex = 31
    '
    'edUnmis
    '
    Me.edUnmis.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUnmis.Location = New System.Drawing.Point(594, 24)
    Me.edUnmis.Name = "edUnmis"
    Me.edUnmis.NTSDbField = ""
    Me.edUnmis.NTSForzaVisZoom = False
    Me.edUnmis.NTSOldValue = ""
    Me.edUnmis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUnmis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUnmis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUnmis.Properties.MaxLength = 65536
    Me.edUnmis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUnmis.Size = New System.Drawing.Size(54, 20)
    Me.edUnmis.TabIndex = 30
    '
    'edFase
    '
    Me.edFase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFase.Location = New System.Drawing.Point(560, 44)
    Me.edFase.Name = "edFase"
    Me.edFase.NTSDbField = ""
    Me.edFase.NTSFormat = "0"
    Me.edFase.NTSForzaVisZoom = False
    Me.edFase.NTSOldValue = ""
    Me.edFase.Properties.Appearance.Options.UseTextOptions = True
    Me.edFase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFase.Properties.MaxLength = 65536
    Me.edFase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFase.Size = New System.Drawing.Size(88, 20)
    Me.edFase.TabIndex = 29
    '
    'edArticolo
    '
    Me.edArticolo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArticolo.Location = New System.Drawing.Point(62, 24)
    Me.edArticolo.Name = "edArticolo"
    Me.edArticolo.NTSDbField = ""
    Me.edArticolo.NTSForzaVisZoom = False
    Me.edArticolo.NTSOldValue = ""
    Me.edArticolo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArticolo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArticolo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArticolo.Properties.MaxLength = 65536
    Me.edArticolo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArticolo.Size = New System.Drawing.Size(131, 20)
    Me.edArticolo.TabIndex = 26
    '
    'edAdatcons
    '
    Me.edAdatcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAdatcons.Location = New System.Drawing.Point(560, 4)
    Me.edAdatcons.Name = "edAdatcons"
    Me.edAdatcons.NTSDbField = ""
    Me.edAdatcons.NTSForzaVisZoom = False
    Me.edAdatcons.NTSOldValue = ""
    Me.edAdatcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAdatcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAdatcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAdatcons.Properties.MaxLength = 65536
    Me.edAdatcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAdatcons.Size = New System.Drawing.Size(88, 20)
    Me.edAdatcons.TabIndex = 25
    '
    'edDadatcons
    '
    Me.edDadatcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDadatcons.Location = New System.Drawing.Point(405, 4)
    Me.edDadatcons.Name = "edDadatcons"
    Me.edDadatcons.NTSDbField = ""
    Me.edDadatcons.NTSForzaVisZoom = False
    Me.edDadatcons.NTSOldValue = ""
    Me.edDadatcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDadatcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDadatcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDadatcons.Properties.MaxLength = 65536
    Me.edDadatcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDadatcons.Size = New System.Drawing.Size(88, 20)
    Me.edDadatcons.TabIndex = 24
    '
    'edEsist
    '
    Me.edEsist.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEsist.Location = New System.Drawing.Point(199, 4)
    Me.edEsist.Name = "edEsist"
    Me.edEsist.NTSDbField = ""
    Me.edEsist.NTSFormat = "0"
    Me.edEsist.NTSForzaVisZoom = False
    Me.edEsist.NTSOldValue = ""
    Me.edEsist.Properties.Appearance.Options.UseTextOptions = True
    Me.edEsist.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEsist.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEsist.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEsist.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEsist.Properties.MaxLength = 65536
    Me.edEsist.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEsist.Size = New System.Drawing.Size(137, 20)
    Me.edEsist.TabIndex = 23
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.Location = New System.Drawing.Point(62, 4)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(74, 20)
    Me.edConto.TabIndex = 22
    '
    'lbEsist
    '
    Me.lbEsist.AutoSize = True
    Me.lbEsist.BackColor = System.Drawing.Color.Transparent
    Me.lbEsist.Location = New System.Drawing.Point(142, 4)
    Me.lbEsist.Name = "lbEsist"
    Me.lbEsist.NTSDbField = ""
    Me.lbEsist.Size = New System.Drawing.Size(52, 13)
    Me.lbEsist.TabIndex = 20
    Me.lbEsist.Text = "Esistenza"
    Me.lbEsist.Tooltip = ""
    Me.lbEsist.UseMnemonic = False
    '
    'lbFase
    '
    Me.lbFase.AutoSize = True
    Me.lbFase.BackColor = System.Drawing.Color.Transparent
    Me.lbFase.Location = New System.Drawing.Point(499, 44)
    Me.lbFase.Name = "lbFase"
    Me.lbFase.NTSDbField = ""
    Me.lbFase.Size = New System.Drawing.Size(34, 13)
    Me.lbFase.TabIndex = 18
    Me.lbFase.Text = "Fase:"
    Me.lbFase.Tooltip = ""
    Me.lbFase.UseMnemonic = False
    '
    'lbUnmis
    '
    Me.lbUnmis.AutoSize = True
    Me.lbUnmis.BackColor = System.Drawing.Color.Transparent
    Me.lbUnmis.Location = New System.Drawing.Point(499, 24)
    Me.lbUnmis.Name = "lbUnmis"
    Me.lbUnmis.NTSDbField = ""
    Me.lbUnmis.Size = New System.Drawing.Size(77, 13)
    Me.lbUnmis.TabIndex = 16
    Me.lbUnmis.Text = "Unità di misura"
    Me.lbUnmis.Tooltip = ""
    Me.lbUnmis.UseMnemonic = False
    '
    'lbAdatcons
    '
    Me.lbAdatcons.AutoSize = True
    Me.lbAdatcons.BackColor = System.Drawing.Color.Transparent
    Me.lbAdatcons.Location = New System.Drawing.Point(499, 4)
    Me.lbAdatcons.Name = "lbAdatcons"
    Me.lbAdatcons.NTSDbField = ""
    Me.lbAdatcons.Size = New System.Drawing.Size(22, 13)
    Me.lbAdatcons.TabIndex = 14
    Me.lbAdatcons.Text = "al :"
    Me.lbAdatcons.Tooltip = ""
    Me.lbAdatcons.UseMnemonic = False
    '
    'lbDadatcons
    '
    Me.lbDadatcons.AutoSize = True
    Me.lbDadatcons.BackColor = System.Drawing.Color.Transparent
    Me.lbDadatcons.Location = New System.Drawing.Point(343, 4)
    Me.lbDadatcons.Name = "lbDadatcons"
    Me.lbDadatcons.NTSDbField = ""
    Me.lbDadatcons.Size = New System.Drawing.Size(56, 13)
    Me.lbDadatcons.TabIndex = 12
    Me.lbDadatcons.Text = "Cons.dal :"
    Me.lbDadatcons.Tooltip = ""
    Me.lbDadatcons.UseMnemonic = False
    '
    'lbXx_commeca
    '
    Me.lbXx_commeca.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_commeca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_commeca.Location = New System.Drawing.Point(145, 63)
    Me.lbXx_commeca.Name = "lbXx_commeca"
    Me.lbXx_commeca.NTSDbField = ""
    Me.lbXx_commeca.Size = New System.Drawing.Size(348, 20)
    Me.lbXx_commeca.TabIndex = 11
    Me.lbXx_commeca.Tooltip = ""
    Me.lbXx_commeca.UseMnemonic = False
    '
    'lbCommeca
    '
    Me.lbCommeca.AutoSize = True
    Me.lbCommeca.BackColor = System.Drawing.Color.Transparent
    Me.lbCommeca.Location = New System.Drawing.Point(7, 64)
    Me.lbCommeca.Name = "lbCommeca"
    Me.lbCommeca.NTSDbField = ""
    Me.lbCommeca.Size = New System.Drawing.Size(40, 13)
    Me.lbCommeca.TabIndex = 9
    Me.lbCommeca.Text = "Comm."
    Me.lbCommeca.Tooltip = ""
    Me.lbCommeca.UseMnemonic = False
    '
    'lbXx_Magaz
    '
    Me.lbXx_Magaz.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Magaz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Magaz.Location = New System.Drawing.Point(121, 43)
    Me.lbXx_Magaz.Name = "lbXx_Magaz"
    Me.lbXx_Magaz.NTSDbField = ""
    Me.lbXx_Magaz.Size = New System.Drawing.Size(372, 20)
    Me.lbXx_Magaz.TabIndex = 8
    Me.lbXx_Magaz.Tooltip = ""
    Me.lbXx_Magaz.UseMnemonic = False
    '
    'lbMagaz
    '
    Me.lbMagaz.AutoSize = True
    Me.lbMagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbMagaz.Location = New System.Drawing.Point(7, 44)
    Me.lbMagaz.Name = "lbMagaz"
    Me.lbMagaz.NTSDbField = ""
    Me.lbMagaz.Size = New System.Drawing.Size(47, 13)
    Me.lbMagaz.TabIndex = 6
    Me.lbMagaz.Text = "Magazz."
    Me.lbMagaz.Tooltip = ""
    Me.lbMagaz.UseMnemonic = False
    '
    'lbXx_articolo
    '
    Me.lbXx_articolo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_articolo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_articolo.Location = New System.Drawing.Point(199, 23)
    Me.lbXx_articolo.Name = "lbXx_articolo"
    Me.lbXx_articolo.NTSDbField = ""
    Me.lbXx_articolo.Size = New System.Drawing.Size(294, 20)
    Me.lbXx_articolo.TabIndex = 27
    Me.lbXx_articolo.Tooltip = ""
    Me.lbXx_articolo.UseMnemonic = False
    '
    'lbArticolo
    '
    Me.lbArticolo.AutoSize = True
    Me.lbArticolo.BackColor = System.Drawing.Color.Transparent
    Me.lbArticolo.Location = New System.Drawing.Point(7, 24)
    Me.lbArticolo.Name = "lbArticolo"
    Me.lbArticolo.NTSDbField = ""
    Me.lbArticolo.Size = New System.Drawing.Size(43, 13)
    Me.lbArticolo.TabIndex = 3
    Me.lbArticolo.Text = "Articolo"
    Me.lbArticolo.Tooltip = ""
    Me.lbArticolo.UseMnemonic = False
    '
    'lbXx_conto
    '
    Me.lbXx_conto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_conto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_conto.Location = New System.Drawing.Point(134, 3)
    Me.lbXx_conto.Name = "lbXx_conto"
    Me.lbXx_conto.NTSDbField = ""
    Me.lbXx_conto.Size = New System.Drawing.Size(202, 20)
    Me.lbXx_conto.TabIndex = 2
    Me.lbXx_conto.Tooltip = ""
    Me.lbXx_conto.UseMnemonic = False
    '
    'lbConto
    '
    Me.lbConto.AutoSize = True
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.Location = New System.Drawing.Point(7, 4)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(36, 13)
    Me.lbConto.TabIndex = 0
    Me.lbConto.Text = "Conto"
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grGrso)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 132)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(660, 310)
    Me.pnGrid.TabIndex = 7
    Me.pnGrid.Text = "NtsPanel1"
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbTaglie, Me.tlbNavigazioneDoc, Me.tlbNavigazioneMrp, Me.tlbEsci, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbUltimo, Me.tlbOrdini, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaVideo, Me.tlbStampa})
    Me.NtsBarManager1.MaxItemId = 18
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbTaglie, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOrdini, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNavigazioneDoc), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNavigazioneMrp), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci, True)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.Id = 9
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.Id = 10
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.Id = 11
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.Id = 12
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbTaglie
    '
    Me.tlbTaglie.Caption = "Taglie"
    Me.tlbTaglie.Glyph = CType(resources.GetObject("tlbTaglie.Glyph"), System.Drawing.Image)
    Me.tlbTaglie.Id = 0
    Me.tlbTaglie.Name = "tlbTaglie"
    Me.tlbTaglie.Visible = True
    '
    'tlbOrdini
    '
    Me.tlbOrdini.Caption = "Apri ordine"
    Me.tlbOrdini.Glyph = CType(resources.GetObject("tlbOrdini.Glyph"), System.Drawing.Image)
    Me.tlbOrdini.Id = 13
    Me.tlbOrdini.Name = "tlbOrdini"
    Me.tlbOrdini.Visible = True
    '
    'tlbNavigazioneDoc
    '
    Me.tlbNavigazioneDoc.Caption = "NavigazioneDoc"
    Me.tlbNavigazioneDoc.Glyph = CType(resources.GetObject("tlbNavigazioneDoc.Glyph"), System.Drawing.Image)
    Me.tlbNavigazioneDoc.Id = 4
    Me.tlbNavigazioneDoc.Name = "tlbNavigazioneDoc"
    Me.tlbNavigazioneDoc.Visible = True
    '
    'tlbNavigazioneMrp
    '
    Me.tlbNavigazioneMrp.Caption = "NavigazioneMrp"
    Me.tlbNavigazioneMrp.Glyph = CType(resources.GetObject("tlbNavigazioneMrp.Glyph"), System.Drawing.Image)
    Me.tlbNavigazioneMrp.Id = 5
    Me.tlbNavigazioneMrp.Name = "tlbNavigazioneMrp"
    Me.tlbNavigazioneMrp.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 14
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 15
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.NTSIsCheckBox = False
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa a video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 16
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 17
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 8
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'FRMORGRSO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(660, 442)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MinimizeBox = False
    Me.Name = "FRMORGRSO"
    Me.NTSLastControlFocussed = Me.grGrso
    Me.Text = "STAMPA / VISUALIZZAZIONE SCHEDE ORDINI"
    CType(Me.grGrso, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGrso, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edCommeca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUnmis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArticolo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAdatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDadatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEsist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
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

  Public Overridable Sub InitEntity(ByVal cleScho As CLEORSCHO)
    oCleScho = cleScho
    AddHandler oCleScho.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub


  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTipork As New DataTable()
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbTaglie.GlyphPath = (oApp.ChildImageDir & "\tc.gif")
        tlbNavigazioneDoc.GlyphPath = (oApp.ChildImageDir & "\navigazione.gif")
        tlbNavigazioneMrp.GlyphPath = (oApp.ChildImageDir & "\movmrp.gif")
        tlbOrdini.GlyphPath = (oApp.ChildImageDir & "\ordini.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      edCommeca.NTSSetParam(oMenu, oApp.Tr(Me, 128607821330642511, "Comm."), "0")
      edMagaz.NTSSetParam(oMenu, oApp.Tr(Me, 128607821330798371, "Magazz."), "0")
      edUnmis.NTSSetParam(oMenu, oApp.Tr(Me, 128607821330954231, "Unità di misura"), 0)
      edFase.NTSSetParam(oMenu, oApp.Tr(Me, 128607821331110091, "Fase:"), "0")
      edArticolo.NTSSetParam(oMenu, oApp.Tr(Me, 128607821331265951, "Articolo"), 0)
      edAdatcons.NTSSetParam(oMenu, oApp.Tr(Me, 128607821331421811, "al :"), True)
      edDadatcons.NTSSetParam(oMenu, oApp.Tr(Me, 128607821331577671, "Cons.dal :"), True)
      edEsist.NTSSetParam(oMenu, oApp.Tr(Me, 128607821331733531, "Esistenza"), oApp.FormatImporti)
      edConto.NTSSetParam(oMenu, oApp.Tr(Me, 128607821331889391, "Conto"), "0")

      grvGrso.NTSSetParam(oMenu, "Stampa / Visualizzazione Schede Ordini")

      dttTipork.Columns.Add("cod", GetType(String))
      dttTipork.Columns.Add("val", GetType(String))
      dttTipork.Rows.Add(New Object() {"R", "IC"})
      dttTipork.Rows.Add(New Object() {"O", "OF"})
      dttTipork.Rows.Add(New Object() {"H", "OP"})
      dttTipork.Rows.Add(New Object() {"Y", "IP"})
      dttTipork.Rows.Add(New Object() {"Q", "PR"})
      dttTipork.Rows.Add(New Object() {"X", "IT"})
      dttTipork.Rows.Add(New Object() {"$", "OFA"})
      dttTipork.Rows.Add(New Object() {"V", "ICA"})
      dttTipork.Rows.Add(New Object() {"#", "ICO"})
      dttTipork.AcceptChanges()

      td_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128605197898348342, "Data Ord."), True)
      ko_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128605197898504202, "Tipo"), dttTipork, "val", "cod")
      ko_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128605197898660062, "Serie"), CLN__STD.SerieMaxLen, True)
      ko_numord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197898815922, "N°Ord."), "0", 9, 0, 999999999)
      td_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128605197898971782, "Riferim."), 0, True)
      td_alfpar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128605197899127642, "A.Par."), CLN__STD.SerieMaxLen, True)
      td_numpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197899283502, "N°Par."), "0", 9, 0, 999999999)
      td_datpar.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128605197899439362, "Dt.Par."), True)
      mo_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197899595222, "Qta.Ord."), "#,##0.000", 15)
      mo_quaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197899751082, "Qta.Sped."), "#,##0.000", 15)
      mo_flevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128605197899906942, "Ev."), "S", "C")
      mo_quapre.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197900062802, "Qta.Pren."), "#,##0.000", 15)
      mo_flevapre.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128605197900218662, "Pr."), 0, True)
      ko_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128605197900686242, "Consegna"), True)
      mo_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197900842102, "Prezzo"), TrovaFmtPrz, 15)
      mo_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197900997962, "Valore"), "#,##0.00", 15)
      an_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128605197901153822, "Cliente/Fornitore"), 0, True)
      mo_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197901465542, "Sc.1"), "#,##0.00", 15)
      mo_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197901621402, "Sc.2"), "#,##0.00", 15)
      mo_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197901777262, "Sc.3"), "#,##0.00", 15)
      mo_prelist.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197901933122, "Prezzo List."), TrovaFmtPrz, 15)
      mo_prezvalc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197902088982, "Prz.Val."), TrovaFmtPrz(1), 15)
      mo_colli.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197902244842, "Colli"), "#,##0.000", 15)
      mo_coleva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197902400702, "Col.evasi"), "#,##0.000", 15)
      mo_colpre.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197902556562, "Col.Pren."), "#,##0.000", 15)
      mo_misura1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197902712422, "Mis.1"), "#,##0.000", 15)
      mo_misura2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197902868282, "Mis.2"), "#,##0.000", 15)
      mo_misura3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197903024142, "Mis.3"), "#,##0.000", 15)
      mo_controp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197903180002, "Controp."), "0", 4, 0, 9999)
      mo_commeca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197903335862, "Commessa"), "0", 9, 0, 999999999)
      mo_codcena.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197903491722, "C.Centro"), "0", 9, 0, 999999999)
      mo_codcfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128605197903647582, "Linea"), 0, True)
      mo_datconsor.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128605197903803442, "Dt.Cons.Or."), True)
      mo_provv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197903959302, "Provv."), "#,##0.000", 15)
      mo_vprovv.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197904115162, "V.Provv."), "#,##0.00", 15)
      mo_provv2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197904271022, "Provv.2"), "#,##0.000", 15)
      mo_vprovv2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197904426882, "V.Provv.2"), "#,##0.00", 15)
      mo_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197904582742, "Sconto 4"), "#,##0.00", 15)
      mo_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197904738602, "Sconto 5"), "#,##0.00", 15)
      mo_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605197904894462, "Sconto 6 "), "#,##0.00", 15)
      quaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255690055184, "Qtà.residua"), "#,##0.000", 15)
      xx_disp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255690213960, "Disp."), "#,##0.000", 15)
      PRZNET.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255691007840, "Prezzo N."), TrovaFmtPrz, 15)
      T1o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255694977240, "T1o"), "#,##0.00", 15)
      T2o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255695136016, "T2o"), "#,##0.00", 15)
      T3o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255695294792, "T3o"), "#,##0.00", 15)
      T4o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255695453568, "T4o"), "#,##0.00", 15)
      T5o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255695612344, "T5o"), "#,##0.00", 15)
      T6o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255695771120, "T6o"), "#,##0.00", 15)
      T7o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255695929896, "T7o"), "#,##0.00", 15)
      T8o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255696088672, "T8o"), "#,##0.00", 15)
      T9o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255696247448, "T9o"), "#,##0.00", 15)
      T10o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255696406224, "T10o"), "#,##0.00", 15)
      T11o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255696565000, "T11o"), "#,##0.00", 15)
      T12o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255696723776, "T12o"), "#,##0.00", 15)
      T13o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255696882552, "T13o"), "#,##0.00", 15)
      T14o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255697041328, "T14o"), "#,##0.00", 15)
      T15o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255697200104, "T15o"), "#,##0.00", 15)
      T16o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255697358880, "T16o"), "#,##0.00", 15)
      T17o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255697517656, "T17o"), "#,##0.00", 15)
      T18o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255697676432, "T18o"), "#,##0.00", 15)
      T19o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255697835208, "T19o"), "#,##0.00", 15)
      T20o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255697993984, "T20o"), "#,##0.00", 15)
      T21o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255698152760, "T21o"), "#,##0.00", 15)
      T22o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255698311536, "T22o"), "#,##0.00", 15)
      T23o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255698470312, "T23o"), "#,##0.00", 15)
      T24o.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255698629088, "T24o"), "#,##0.00", 15)
      T1c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255698787864, "T1c"), "#,##0.00", 15)
      T2c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255698946640, "T2c"), "#,##0.00", 15)
      T3c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255699105416, "T3c"), "#,##0.00", 15)
      T4c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255699264192, "T4c"), "#,##0.00", 15)
      T5c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255699422968, "T5c"), "#,##0.00", 15)
      T6c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255699581744, "T6c"), "#,##0.00", 15)
      T7c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255699740520, "T7c"), "#,##0.00", 15)
      T8c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255699899296, "T8c"), "#,##0.00", 15)
      T9c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255700058072, "T9c"), "#,##0.00", 15)
      T10c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255700216848, "T10c"), "#,##0.00", 15)
      T11c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255700375624, "T11c"), "#,##0.00", 15)
      T12c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255700534400, "T12c"), "#,##0.00", 15)
      T13c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255700693176, "T13c"), "#,##0.00", 15)
      T14c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255700851952, "T14c"), "#,##0.00", 15)
      T15c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255701010728, "T15c"), "#,##0.00", 15)
      T16c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255701169504, "T16c"), "#,##0.00", 15)
      T17c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255701328280, "T17c"), "#,##0.00", 15)
      T18c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255701487056, "T18c"), "#,##0.00", 15)
      T19c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255701645832, "T19c"), "#,##0.00", 15)
      T20c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255701804608, "T20c"), "#,##0.00", 15)
      T21c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255701963384, "T21c"), "#,##0.00", 15)
      T22c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255702122160, "T22c"), "#,##0.00", 15)
      T23c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255702280936, "T23c"), "#,##0.00", 15)
      T24c.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255702439712, "T24c"), "#,##0.00", 15)
      T1r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255702598488, "T1r"), "#,##0.00", 15)
      T2r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255702757264, "T2r"), "#,##0.00", 15)
      T3r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255702916040, "T3r"), "#,##0.00", 15)
      T4r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255703074816, "T4r"), "#,##0.00", 15)
      T5r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255703233592, "T5r"), "#,##0.00", 15)
      T6r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255703392368, "T6r"), "#,##0.00", 15)
      T7r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255703551144, "T7r"), "#,##0.00", 15)
      T8r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255703709920, "T8r"), "#,##0.00", 15)
      T9r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255703868696, "T9r"), "#,##0.00", 15)
      T10r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255704027472, "T10r"), "#,##0.00", 15)
      T11r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255704186248, "T11r"), "#,##0.00", 15)
      T12r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255704345024, "T12r"), "#,##0.00", 15)
      T13r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255704503800, "T13r"), "#,##0.00", 15)
      T14r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255704662576, "T14r"), "#,##0.00", 15)
      T15r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255704821352, "T15r"), "#,##0.00", 15)
      T16r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255704980128, "T16r"), "#,##0.00", 15)
      T17r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255705138904, "T17r"), "#,##0.00", 15)
      T18r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255705297680, "T18r"), "#,##0.00", 15)
      T19r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255705456456, "T19r"), "#,##0.00", 15)
      T20r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255705615232, "T20r"), "#,##0.00", 15)
      T21r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255705774008, "T21r"), "#,##0.00", 15)
      T22r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255705932784, "T22r"), "#,##0.00", 15)
      T23r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255706091560, "T23r"), "#,##0.00", 15)
      T24r.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128605255706250336, "T24r"), "#,##0.00", 15)
      mo_ubicaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128605209925441102, "Ubicaz."), 0, True)
      grvGrso.Enabled = False
      grvGrso.NTSAllowInsert = False

      edConto.Enabled = False
      edEsist.Enabled = False
      edDadatcons.Enabled = False
      edAdatcons.Enabled = False
      edArticolo.Enabled = False
      edUnmis.Enabled = False
      edMagaz.Enabled = False
      edFase.Enabled = False
      edCommeca.Enabled = False

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
  Public Overridable Sub FRMORGRSO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      edDadatcons.Text = oCleScho.strSchoDadatcons
      edAdatcons.Text = oCleScho.strSchoAdatcons
      Select Case oCleScho.strSchoOrdin
        Case "A"
          lbConto.Visible = False
          edConto.Visible = False
          lbXx_conto.Visible = False
          lbCommeca.Visible = False
          edCommeca.Visible = False
          lbXx_commeca.Visible = False
        Case "C"
          lbEsist.Visible = False
          edEsist.Visible = False
          lbCommeca.Visible = False
          edCommeca.Visible = False
          lbXx_commeca.Visible = False
        Case "X"
          lbConto.Visible = False
          edConto.Visible = False
          lbXx_conto.Visible = False
      End Select
      If Not Apri() Then Exit Sub
      ApriRecordset()
      RiempiLabel()
      If oCleScho.strSchoOrdin = "C" Then
        xx_disp.Visible = False
        an_descr1.Visible = False
      End If
      '---------------------------------------------
      'Navigazione M.R.P.
      '---------------------------------------------
      If oCleScho.strSchoOrdin <> "A" Then
        tlbNavigazioneMrp.Enabled = False
      End If
      '-----------------------------------------------------------------------------------------
      If oCleScho.bGrsoModTCO = False Then tlbTaglie.Visible = False
      '-----------------------------------------------------------------------------------------
      If Not oCleScho.GetRelease() Then
        tlbNavigazioneDoc.Visible = True
      Else
        tlbNavigazioneDoc.Visible = False
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcune colonne
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tlbTaglie.Visible = False
        tlbNavigazioneMrp.Visible = False
        mo_scont3.Visible = False
        mo_scont4.Visible = False
        mo_scont5.Visible = False
        mo_scont6.Visible = False
        mo_colpre.Visible = False
        mo_codcena.Visible = False
        mo_commeca.Visible = False
        mo_provv2.Visible = False
        mo_vprovv2.Visible = False
        mo_ubicaz.Visible = False
        T1o.Visible = False : T2o.Visible = False : T3o.Visible = False : T4o.Visible = False
        T5o.Visible = False : T6o.Visible = False : T7o.Visible = False : T8o.Visible = False
        T9o.Visible = False : T10o.Visible = False : T11o.Visible = False : T12o.Visible = False
        T13o.Visible = False : T14o.Visible = False : T15o.Visible = False : T16o.Visible = False
        T17o.Visible = False : T18o.Visible = False : T19o.Visible = False : T20o.Visible = False
        T21o.Visible = False : T22o.Visible = False : T23o.Visible = False : T24o.Visible = False
        T1c.Visible = False : T2c.Visible = False : T3c.Visible = False : T4c.Visible = False
        T5c.Visible = False : T6c.Visible = False : T7c.Visible = False : T8c.Visible = False
        T9c.Visible = False : T10c.Visible = False : T11c.Visible = False : T12c.Visible = False
        T13c.Visible = False : T14c.Visible = False : T15c.Visible = False : T16c.Visible = False
        T17c.Visible = False : T18c.Visible = False : T19c.Visible = False : T20c.Visible = False
        T21c.Visible = False : T22c.Visible = False : T23c.Visible = False : T24c.Visible = False
        T1r.Visible = False : T2r.Visible = False : T3r.Visible = False : T4r.Visible = False
        T5r.Visible = False : T6r.Visible = False : T7r.Visible = False : T8r.Visible = False
        T9r.Visible = False : T10r.Visible = False : T11r.Visible = False : T12r.Visible = False
        T13r.Visible = False : T14r.Visible = False : T15r.Visible = False : T16r.Visible = False
        T17r.Visible = False : T18r.Visible = False : T19r.Visible = False : T20r.Visible = False
        T21r.Visible = False : T22r.Visible = False : T23r.Visible = False : T24r.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGRSO_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      If bClose = True Then Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMORGRSO_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bNoModal = True Then oMenu.ResetTblInstId("TTSCHO", False, oCleScho.lIITTScho)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMORGRSO_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGrso.Dispose()
      dsGrso.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi di Toolbar"
  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      dcGrso.MoveFirst()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      dcGrso.MovePrevious()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      dcGrso.MoveNext()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      dcGrso.MoveLast()
      ApriRecordset()
      RiempiLabel()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbTaglie_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbTaglie.ItemClick
    Dim oPar As New CLE__CLDP
    Dim dsArtico As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se non è attivo il modulo Taglie e colori esce
      '-----------------------------------------------------------------------------------------
      If oCleScho.bGrsoModTCO = False Then Exit Sub

      If grvGrso.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
      '-----------------------------------------------------------------------------------------
      '--- Se l'articolo non è gestito per taglia, avvisa ed esce
      '-----------------------------------------------------------------------------------------
      If oCleScho.bGrsoModTCO = True Then
        If Not oCleScho.CheckArticotaglie(NTSCStr(grvGrso.NTSGetCurrentDataRow!ko_codart)) Then
          Exit Sub
        End If
      End If

      '------------------------------
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BSORSCHO"
      oPar.strParam = "".PadLeft(12) & "|" & _
               "".PadLeft(12, "z"c) & "|" & _
               NTSCStr(grvGrso.NTSGetCurrentDataRow!ko_codart) & "|" & _
               NTSCStr(grvGrso.NTSGetCurrentDataRow!ko_codart) & "|" & _
               "0" & "|" & _
               "9999" & "|" & _
               "".PadLeft(18) & "|" & _
               "".PadLeft(18, "z"c) & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               "0" & "|" & _
               "999999999" & "|" & _
               NTSCStr(grvGrso.NTSGetCurrentDataRow!ko_fase) & "|" & _
               NTSCStr(grvGrso.NTSGetCurrentDataRow!ko_fase) & "|" & _
               NTSCStr(grvGrso.NTSGetCurrentDataRow!ko_tipork) & ";" & _
               Microsoft.VisualBasic.Right("0000" & NTSCStr(grvGrso.NTSGetCurrentDataRow!ko_anno), 4) & ";" & _
               NTSCStr(grvGrso.NTSGetCurrentDataRow!ko_serie) & ";" & _
               Microsoft.VisualBasic.Right("000000000" & NTSCStr(grvGrso.NTSGetCurrentDataRow!ko_numord), 9) & ";" & _
               Microsoft.VisualBasic.Right("000000000" & NTSCStr(grvGrso.NTSGetCurrentDataRow!ko_riga), 9) & ";|" & _
               "".PadLeft(6, "S"c) & "".PadLeft(14, "N"c) & "|" & _
               "0" & "|" & _
               "0"
      oMenu.RunChild("NTSInformatica", "FRMTCDIPT", "", DittaCorrente, "", "BNTCDIPT", oPar, "", Not bNoModal, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbOrdini_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOrdini.ItemClick
    Dim strTipork As String
    Dim nAnno As Integer
    Dim strSerie As String
    Dim lNumdoc As Integer
    Dim strParam As String
    Try
      If grvGrso.NTSGetCurrentDataRow() Is Nothing Then Exit Sub
      '-----------------------------------------------------------------------------------------
      strTipork = NTSCStr(grvGrso.NTSGetCurrentDataRow()!ko_tipork)
      nAnno = NTSCInt(grvGrso.NTSGetCurrentDataRow()!ko_anno)
      strSerie = NTSCStr(grvGrso.NTSGetCurrentDataRow()!ko_serie)
      lNumdoc = NTSCInt(grvGrso.NTSGetCurrentDataRow()!ko_numord)
      '-------------------------------------------------------------------------------------
      strParam = "APRI;" & strTipork & ";" & _
           nAnno.ToString.PadLeft(4, "0"c) & ";" & _
           strSerie & ";" & _
           lNumdoc.ToString.PadLeft(9, "0"c) & ";"
      oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 128782966865846000, "Gestione ordini/impegno"), DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbNavigazioneDoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNavigazioneDoc.ItemClick
    Dim strParam As String
    Try
      If grvGrso.NTSGetCurrentDataRow() Is Nothing Then Exit Sub

      strParam = "APRI;" & Microsoft.VisualBasic.Right(" " & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_tipork), 1) & ";" & _
          Microsoft.VisualBasic.Right("0000" & NTSCStr(Year(NTSCDate(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!td_datord))), 4) & ";" & _
          NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_serie).PadLeft(1) & ";" & _
          Microsoft.VisualBasic.Right("000000000" & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_numord), 9) & ";" & _
          "000000000;" & Microsoft.VisualBasic.Right("          " & NTSCDate(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!td_datord).ToShortDateString, 10) & _
          ";000000000;0000;0000; ;000000000;0000;1"

      oMenu.RunChild("BS__FLDO", "CLS__FLDO", oApp.Tr(Me, 128233597953154876, "Navigazione Documentale"), DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbNavigazioneMrp_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNavigazioneMrp.ItemClick
    Dim strParam As String = ""
    Try
      If grvGrso.NTSGetCurrentDataRow() Is Nothing Then Exit Sub

      If NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_tipork) <> "$" And _
         NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_tipork) <> "O" And _
         NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_tipork) <> "H" And _
         NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_tipork) <> "Y" And _
         NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_tipork) <> "X" And _
         NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_tipork) <> "R" And _
         NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_tipork) <> "V" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128601706234826627, "Funzione non disponibile per questo tipo di ordine/impegno."))
        Exit Sub
      End If

      strParam = "O;" & Microsoft.VisualBasic.Right(" " & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_tipork), 1) & ";" & _
        Microsoft.VisualBasic.Right("0000" & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_anno), 4) & ";" & _
        Microsoft.VisualBasic.Right(" " & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_serie), 1) & ";" & _
        Microsoft.VisualBasic.Right("000000000" & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_numord), 9) & ";" & _
        Microsoft.VisualBasic.Right("000000000" & NTSCStr(dsGrid.Tables("MOVORD").Rows(dcGrid.Position)!ko_riga), 9) & ";" & _
        Microsoft.VisualBasic.Right("".PadLeft(CLN__STD.CodartMaxLen) & NTSCStr(edArticolo.Text), CLN__STD.CodartMaxLen) & ";" & _
        Microsoft.VisualBasic.Right("0000" & NTSCStr(edFase.Text), 4)

      oMenu.RunChild("BSDBNMRP", "CLSDBNMRP", " ", DittaCorrente, "", "", Nothing, strParam, Not bNoModal, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    Try
      oMenu.ReportImposta(Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      StampaGriglia(0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      StampaGriglia(1)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
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

  Public Overridable Sub SetColonneTaglie()
    Dim strPref As String
    Dim strSuf As String
    Dim strTaglia As String
    Dim strCampoTaglia As String
    Dim strTagliaDescr As String
    Dim bTagliaPresente As Boolean
    Dim bArticoloTC As Boolean
    Dim i As Integer
    Dim dsTaglie As DataSet = Nothing
    Try
      strPref = "T"

      oCleScho.GetTaglie(NTSCStr(dsGrso.Tables("MOVORD").Rows(dcGrso.Position)!ko_codart), dsTaglie)

      If dsTaglie.Tables("ARTICO").Rows.Count = 0 Then
        bArticoloTC = False
      Else
        bArticoloTC = True
      End If

      If (oCleScho.strSchoOrdin = "A" Or oCleScho.strSchoOrdin = "C") And oCleScho.bGrsoModTCO = True And bArticoloTC = True Then
        For i = 1 To 24 'se settata taglia in scala taglie col visibile else invisibile
          strCampoTaglia = "tb_dest" & Format(i, "00")

          If NTSCStr(dsTaglie.Tables("ARTICO").Rows(0)(strCampoTaglia)) <> " " Then
            bTagliaPresente = True
            strTagliaDescr = NTSCStr(dsTaglie.Tables("ARTICO").Rows(0)(strCampoTaglia))
          Else
            bTagliaPresente = False
            strTagliaDescr = ""
          End If

          strSuf = "o"
          strTaglia = strPref & i & strSuf
          grvGrso.NTSGetColumnByName(strTaglia).Visible = bTagliaPresente
          grvGrso.NTSGetColumnByName(strTaglia).Enabled = False
          grvGrso.NTSGetColumnByName(strTaglia).Caption = strTagliaDescr

          strSuf = "c"
          strTaglia = strPref & i & strSuf
          grvGrso.NTSGetColumnByName(strTaglia).Visible = bTagliaPresente
          grvGrso.NTSGetColumnByName(strTaglia).Enabled = False
          grvGrso.NTSGetColumnByName(strTaglia).Caption = strTagliaDescr

          strSuf = "r"
          strTaglia = strPref & i & strSuf
          grvGrso.NTSGetColumnByName(strTaglia).Visible = bTagliaPresente
          grvGrso.NTSGetColumnByName(strTaglia).Enabled = False
          grvGrso.NTSGetColumnByName(strTaglia).Caption = strTagliaDescr
        Next
      Else
        For i = 1 To 24
          strSuf = "o"
          strTaglia = strPref & i & strSuf
          grvGrso.NTSGetColumnByName(strTaglia).Visible = False
          grvGrso.NTSGetColumnByName(strTaglia).Enabled = False

          strSuf = "c"
          strTaglia = strPref & i & strSuf
          grvGrso.NTSGetColumnByName(strTaglia).Visible = False
          grvGrso.NTSGetColumnByName(strTaglia).Enabled = False

          strSuf = "r"
          strTaglia = strPref & i & strSuf
          grvGrso.NTSGetColumnByName(strTaglia).Visible = False
          grvGrso.NTSGetColumnByName(strTaglia).Enabled = False
        Next
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ApriRecordset()
    Try
      SetColonneTaglie()
      If (oCleScho.bModuloCRM = True) And (oCleScho.bIsCRMUser = True) And Not (oCleScho.strAccvis = "T" And oCleScho.bAmm = True) Then
        ComponiStringa()
      Else
        ComponiStringaSQL()
      End If

      CaricaColonneUnbound()

      dcGrid.DataSource = dsGrid.Tables("MOVORD")
      dsGrid.AcceptChanges()

      grGrso.DataSource = dcGrid

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub RiempiLabel()
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If dsGrso.Tables("MOVORD").Rows.Count = 0 Then Exit Sub

      edArticolo.Text = NTSCStr(dsGrso.Tables("MOVORD").Rows(dcGrso.Position)!ko_codart)
      If Not oCleScho.Grs1lbArticolo_Validated(edArticolo.Text, strTmp, dttTmp) Then
        lbXx_articolo.Text = ""
      Else
        lbXx_articolo.Text = strTmp & " " & NTSCStr(dttTmp.rows(0)!ar_desint)
      End If

      edFase.Text = NTSCStr(dsGrso.Tables("MOVORD").Rows(dcGrso.Position)!ko_fase)
      If oCleScho.strSchoOrdin = "C" Then
        edConto.Text = NTSCStr(dsGrso.Tables("MOVORD").Rows(dcGrso.Position)!ko_conto)
        If Not oCleScho.lbConto_Validated(NTSCInt(edConto.Text), strTmp) Then
          lbXx_conto.Text = ""
        Else
          lbXx_conto.Text = strTmp
        End If
      End If
      edMagaz.Text = NTSCStr(dsGrso.Tables("MOVORD").Rows(dcGrso.Position)!ko_magaz)
      If Not oCleScho.lbMagaz_Validated(NTSCInt(edMagaz.Text), strTmp) Then
        lbXx_Magaz.Text = ""
      Else
        lbXx_Magaz.Text = strTmp
      End If

      If Not NTSCStr(dsGrso.Tables("MOVORD").Rows(dcGrso.Position)!mo_ump) = "" Then edUnmis.Text = NTSCStr(dsGrso.Tables("MOVORD").Rows(dcGrso.Position)!mo_ump)
      If oCleScho.strSchoOrdin = "X" Then
        edCommeca.Text = NTSCStr(dsGrso.Tables("MOVORD").Rows(dcGrso.Position)!ko_commecap)
        If Not oCleScho.lbCommeca_Validated(NTSCInt(edCommeca.Text), strTmp) Then
          lbXx_commeca.Text = ""
        Else
          lbXx_commeca.Text = strTmp
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ComponiStringaSQL()
    Dim dEsist As Decimal
    Dim ds As DataSet = Nothing
    Try
      If oCleScho.ComponiStringaSQL(dsGrso.Tables("MOVORD").Rows(dcGrso.Position), dsGrid, dEsist) Then
        edEsist.Text = NTSCStr(dEsist)
      End If

      RiempiLabel()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ComponiStringa()
    Dim dEsist As Decimal
    Dim ds As DataSet = Nothing
    Try
      If oCleScho.ComponiStringa(dsGrso.Tables("MOVORD").Rows(dcGrso.Position), dsGrid, dEsist) Then
        edEsist.Text = NTSCStr(dEsist)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Apri() As Boolean
    Try
      If Not oCleScho.Apri(dsGrso) Then
        bClose = True
        Return False
      End If

      dcGrso.DataSource = dsGrso.Tables("MOVORD")
      dsGrso.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function CaricaColonneUnbound() As Boolean
    Dim i As Integer
    Try
      For i = 0 To dsGrid.Tables("MOVORD").Rows.Count - 1
        If oCleScho.strSchoOrdin <> "C" Then
          dsGrid.Tables("MOVORD").Rows(i)!xx_disp = oCleScho.dDisp(i)
        End If
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaFmtPrz(Optional ByVal nCodvalu As Integer = 0) As String
    Dim i As Integer
    Try
      TrovaFmtPrz = "#,##0"
      If nCodvalu = 0 Then
        If oApp.NDecPrzUn > 0 Then TrovaFmtPrz = TrovaFmtPrz & "."
        For i = 1 To oApp.NDecPrzUn
          TrovaFmtPrz = TrovaFmtPrz & "0"
        Next
      Else
        If oApp.NDecPrzUnVal > 0 Then
          TrovaFmtPrz = TrovaFmtPrz & "."
          For i = 1 To oApp.NDecPrzUnVal
            TrovaFmtPrz = TrovaFmtPrz & "0"
          Next
        End If
      End If

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Sub StampaGriglia(ByVal nDestinazione As Integer)
    '----------------------------------------------------------------------------------------------------------------
    '--- nDestinazione:
    '----- 0 --> video
    '----- 1 --> stampante
    '----------------------------------------------------------------------------------------------------------------
    Dim strCaption As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case oCleScho.strSchoOrdin
        Case "A"
          strCaption = "ESISTENZA: " & edEsist.Text & vbCrLf & _
            "   ARTICOLO: " & edArticolo.Text & " " & lbXx_articolo.Text.ToUpper & vbCrLf & _
            "   MAGAZ: " & edMagaz.Text & " " & lbXx_Magaz.Text.ToUpper
        Case "C"
          strCaption = "CONTO:  " & edConto.Text & " " & lbXx_conto.Text.ToUpper & vbCrLf & _
            "   ARTICOLO: " & edArticolo.Text & " " & lbXx_articolo.Text.ToUpper & vbCrLf & _
            "   MAGAZ: " & edMagaz.Text & " " & lbXx_Magaz.Text.ToUpper
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      If grGrso.IsPrintingAvailable Then
        grvGrso.OptionsPrint.AutoWidth = False
        grvGrso.OptionsPrint.UsePrintStyles = True
        grvGrso.OptionsPrint.EnableAppearanceEvenRow = True
        grvGrso.AppearancePrint.EvenRow.BackColor = Color.White
        Dim PrintingSystem1 As New DevExpress.XtraPrinting.PrintingSystem
        Dim PrintableComponentLink1 As New DevExpress.XtraPrinting.PrintableComponentLink
        PrintingSystem1.Links.AddRange(New Object() {PrintableComponentLink1})
        PrintableComponentLink1.Component = grvGrso.GridControl
        PrintableComponentLink1.PageHeaderFooter = New DevExpress.XtraPrinting.PageHeaderFooter(New DevExpress.XtraPrinting.PageHeaderArea(New String() {strCaption, "", DateTime.Now.ToShortDateString}, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), DevExpress.XtraPrinting.BrickAlignment.Near), New DevExpress.XtraPrinting.PageFooterArea(New String() {"", "", "[Page # of Pages #]"}, New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), DevExpress.XtraPrinting.BrickAlignment.Near))
        PrintableComponentLink1.Margins = New System.Drawing.Printing.Margins(20, 20, 70, 30)
        PrintableComponentLink1.Landscape = True
        PrintableComponentLink1.PaperKind = Printing.PaperKind.A4
        PrintableComponentLink1.PrintingSystem = PrintingSystem1
        PrintableComponentLink1.CreateDocument()
        '------------------------------------------------------------------------------------------------------------
        Select Case nDestinazione
          Case 0 '--- VIDEO
            PrintableComponentLink1.ShowPreview()
          Case 1 '--- STAMPANTE
            Dim pd As New System.Drawing.Printing.PrintDocument()
            PrintableComponentLink1.Print(pd.PrinterSettings.PrinterName)
            pd.Dispose()
        End Select
        '------------------------------------------------------------------------------------------------------------
      Else
        oApp.MsgBoxInfo(oApp.Tr(Me, 128357955083648000, "Stampa griglia non abilitata. File DevExpress.XtraPrinting non trovato"))
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub grGrso_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grGrso.MouseDoubleClick
    Try
      If tlbOrdini.Enabled And tlbOrdini.Visible Then tlbOrdini_ItemClick(tlbOrdini, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

End Class
