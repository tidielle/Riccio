Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO

Public Class FRMMGARTI
  Private Moduli_P As Integer = bsModMG + bsModVE + bsModOR + bsModPM
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtORE + bsModExtCRM
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
  Public oCleArti As CLEMGARTI
  Public dsArti As DataSet
  Public oCallParams As CLE__CLDP
  Public dcArti As BindingSource = New BindingSource

  Public nStato As Integer
  'Opzioni di registro
  'Limita la dimensione massima delle immagini associabili all'anagrafica articoli.
  Public nDimensioneImmagine As Integer = 0

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents pnArti As NTSInformatica.NTSPanel
  Public WithEvents tlbDuplica As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbBarcode As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbKit As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbConai As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbOle As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaEtichette As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbAcquisisciDaCatForn As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbRicalcolaListini As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbZoomRigheOffertePerArt As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbVisualizzaMovimenti As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbControlloQualita As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbArticoliMagazzino As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbAttributiArticolo As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbFasiArticolo As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbPromozioni As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbAccessoriSuccedanei As NTSInformatica.NTSBarMenuItem
  Public WithEvents pnMain As NTSInformatica.NTSPanel
  Public WithEvents tsArti As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag1 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag2 As NTSInformatica.NTSPanel
  Public WithEvents fmProduzione As NTSInformatica.NTSGroupBox
  Public WithEvents ckAr_critico As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_blocco As NTSInformatica.NTSCheckBox
  Public WithEvents lbAr_tipoopz As NTSInformatica.NTSLabel
  Public WithEvents cbAr_tipoopz As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_gescomm As NTSInformatica.NTSLabel
  Public WithEvents cbAr_gescomm As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_fpfence As NTSInformatica.NTSLabel
  Public WithEvents edAr_fpfence As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_rrfence As NTSInformatica.NTSLabel
  Public WithEvents edAr_rrfence As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_ggrior As NTSInformatica.NTSLabel
  Public WithEvents edAr_ggrior As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_fcorrlt As NTSInformatica.NTSLabel
  Public WithEvents edAr_fcorrlt As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_verdb As NTSInformatica.NTSLabel
  Public WithEvents edAr_verdb As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_livmindb As NTSInformatica.NTSLabel
  Public WithEvents edAr_livmindb As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_coddb As NTSInformatica.NTSLabel
  Public WithEvents edAr_coddb As NTSInformatica.NTSTextBoxStr
  Public WithEvents fmAcquisti As NTSInformatica.NTSGroupBox
  Public WithEvents lbXx_forn As NTSInformatica.NTSLabel
  Public WithEvents lbXx_forn2 As NTSInformatica.NTSLabel
  Public WithEvents lbAr_ggant As NTSInformatica.NTSLabel
  Public WithEvents edAr_ggant As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_ggpost As NTSInformatica.NTSLabel
  Public WithEvents edAr_ggpost As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_ggragg As NTSInformatica.NTSLabel
  Public WithEvents edAr_ggragg As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_perragg As NTSInformatica.NTSLabel
  Public WithEvents cbAr_perragg As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_scomax As NTSInformatica.NTSLabel
  Public WithEvents lbAr_maxlotto As NTSInformatica.NTSLabel
  Public WithEvents lbAr_scomin As NTSInformatica.NTSLabel
  Public WithEvents edAr_scomin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_scomax As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_sublotto As NTSInformatica.NTSLabel
  Public WithEvents edAr_sublotto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_maxlotto As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckAr_ripriord As NTSInformatica.NTSCheckBox
  Public WithEvents lbAr_minord As NTSInformatica.NTSLabel
  Public WithEvents edAr_minord As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_polriord As NTSInformatica.NTSLabel
  Public WithEvents cbAr_polriord As NTSInformatica.NTSComboBox
  Public WithEvents edAr_forn As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_forn As NTSInformatica.NTSLabel
  Public WithEvents edAr_forn2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_forn2 As NTSInformatica.NTSLabel
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag3 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag4 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage5 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag5 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage6 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag6 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage7 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag7 As NTSInformatica.NTSPanel
  Public WithEvents edAr_note As NTSInformatica.NTSMemoBox
  Public WithEvents NtsTabPage8 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag8 As NTSInformatica.NTSPanel
  Public WithEvents cmdVisGif2 As NTSInformatica.NTSButton
  Public WithEvents cmdVisGif1 As NTSInformatica.NTSButton
  Public WithEvents lbAr_um4 As NTSInformatica.NTSLabel
  Public WithEvents edAr_um4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_formula As NTSInformatica.NTSLabel
  Public WithEvents edAr_formula As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_umpdapr As NTSInformatica.NTSLabel
  Public WithEvents cbAr_umpdapr As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_umpdapra As NTSInformatica.NTSLabel
  Public WithEvents cbAr_umpdapra As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_gif1 As NTSInformatica.NTSLabel
  Public WithEvents edAr_gif1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_gif2 As NTSInformatica.NTSLabel
  Public WithEvents edAr_gif2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_umdapra As NTSInformatica.NTSLabel
  Public WithEvents cbAr_umdapra As NTSInformatica.NTSComboBox
  Public WithEvents lbXx_magstock As NTSInformatica.NTSLabel
  Public WithEvents lbXx_magprod As NTSInformatica.NTSLabel
  Public WithEvents lbAr_umdapr As NTSInformatica.NTSLabel
  Public WithEvents cbAr_umdapr As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_magstock As NTSInformatica.NTSLabel
  Public WithEvents edAr_magstock As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_magprod As NTSInformatica.NTSLabel
  Public WithEvents edAr_magprod As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsTabPage9 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag9 As NTSInformatica.NTSPanel
  Public WithEvents fmUbiFasi As NTSInformatica.NTSGroupBox
  Public WithEvents lbAr_ultfase As NTSInformatica.NTSLabel
  Public WithEvents edAr_ultfase As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckAr_gesubic As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_gesfasi As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_codimba As NTSInformatica.NTSLabel
  Public WithEvents lbXx_cartric As NTSInformatica.NTSLabel
  Public WithEvents lbXx_cartcanas As NTSInformatica.NTSLabel
  Public WithEvents lbXx_cartcanol As NTSInformatica.NTSLabel
  Public WithEvents lbAr_cartcanol As NTSInformatica.NTSLabel
  Public WithEvents edAr_cartcanol As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_tipitemcp3 As NTSInformatica.NTSLabel
  Public WithEvents cbAr_tipitemcp3 As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_cartric As NTSInformatica.NTSLabel
  Public WithEvents edAr_cartric As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_cartcanas As NTSInformatica.NTSLabel
  Public WithEvents edAr_cartcanas As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_gestser As NTSInformatica.NTSLabel
  Public WithEvents cbAr_gestser As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_makebuy As NTSInformatica.NTSLabel
  Public WithEvents cbAr_makebuy As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_volume As NTSInformatica.NTSLabel
  Public WithEvents edAr_volume As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_codimba As NTSInformatica.NTSLabel
  Public WithEvents edAr_codimba As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_misura1 As NTSInformatica.NTSLabel
  Public WithEvents edAr_misura1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_misura2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_misura3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_flmod As NTSInformatica.NTSLabel
  Public WithEvents edAr_flmod As NTSInformatica.NTSTextBoxStr
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents fmUnitamisura As NTSInformatica.NTSGroupBox
  Public WithEvents edAr_unmis As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_unmis As NTSInformatica.NTSLabel
  Public WithEvents lbAr_conver As NTSInformatica.NTSLabel
  Public WithEvents lbAr_unmis2 As NTSInformatica.NTSLabel
  Public WithEvents edAr_conver As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_confez2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_unmis2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_qtacon2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_confez2 As NTSInformatica.NTSLabel
  Public WithEvents pnTabpag1Right As NTSInformatica.NTSPanel
  Public WithEvents lbXx_numecr As NTSInformatica.NTSLabel
  Public WithEvents fmIntrastat As NTSInformatica.NTSGroupBox
  Public WithEvents lbAr_umintra2 As NTSInformatica.NTSLabel
  Public WithEvents cbAr_umintra2 As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_codnomc As NTSInformatica.NTSLabel
  Public WithEvents edAr_codnomc As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_percvst As NTSInformatica.NTSLabel
  Public WithEvents edAr_percvst As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_prorig As NTSInformatica.NTSLabel
  Public WithEvents edAr_prorig As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_paeorig As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_sostit As NTSInformatica.NTSLabel
  Public WithEvents edAr_sostit As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_sostituito As NTSInformatica.NTSLabel
  Public WithEvents edAr_sostituito As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_numecr As NTSInformatica.NTSLabel
  Public WithEvents edAr_numecr As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnTabpag1Left As NTSInformatica.NTSPanel
  Public WithEvents lbXx_codpdon As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codappr As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codmarc As NTSInformatica.NTSLabel
  Public WithEvents lbAr_gescon As NTSInformatica.NTSLabel
  Public WithEvents cbAr_gescon As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_codappr As NTSInformatica.NTSLabel
  Public WithEvents edAr_codappr As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_tipokit As NTSInformatica.NTSLabel
  Public WithEvents cbAr_tipokit As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_flricmar As NTSInformatica.NTSLabel
  Public WithEvents cbAr_flricmar As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_codpdon As NTSInformatica.NTSLabel
  Public WithEvents edAr_codpdon As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_ricar1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_ricar2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_reparto As NTSInformatica.NTSLabel
  Public WithEvents edAr_reparto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_garacq As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_garven As NTSInformatica.NTSLabel
  Public WithEvents edAr_garven As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_perqta As NTSInformatica.NTSLabel
  Public WithEvents edAr_perqta As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_codmarc As NTSInformatica.NTSLabel
  Public WithEvents edAr_codmarc As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnTabpag3Right As NTSInformatica.NTSPanel
  Public WithEvents lbXx_codvuo As NTSInformatica.NTSLabel
  Public WithEvents fmAltriDati As NTSInformatica.NTSGroupBox
  Public WithEvents ckAr_stainv As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_stasche As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_geslotti As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_inesaur As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_pesoca As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_stalist As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_gestmatr As NTSInformatica.NTSCheckBox
  Public WithEvents lbAr_contriva As NTSInformatica.NTSLabel
  Public WithEvents edAr_contriva As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_codvuo As NTSInformatica.NTSLabel
  Public WithEvents edAr_codvuo As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_pesolor As NTSInformatica.NTSLabel
  Public WithEvents edAr_pesolor As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_pesonet As NTSInformatica.NTSLabel
  Public WithEvents edAr_pesonet As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_catlifo As NTSInformatica.NTSLabel
  Public WithEvents edAr_catlifo As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnTabpag3Left As NTSInformatica.NTSPanel
  Public WithEvents lbXx_codiva As NTSInformatica.NTSLabel
  Public WithEvents lbXx_gruppo As NTSInformatica.NTSLabel
  Public WithEvents lbXx_sotgru As NTSInformatica.NTSLabel
  Public WithEvents lbXx_controp As NTSInformatica.NTSLabel
  Public WithEvents lbXx_controa As NTSInformatica.NTSLabel
  Public WithEvents lbXx_contros As NTSInformatica.NTSLabel
  Public WithEvents lbXx_famprod As NTSInformatica.NTSLabel
  Public WithEvents lbAr_famprod As NTSInformatica.NTSLabel
  Public WithEvents edAr_famprod As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_contros As NTSInformatica.NTSLabel
  Public WithEvents edAr_contros As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_codiva As NTSInformatica.NTSLabel
  Public WithEvents edAr_codiva As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_gruppo As NTSInformatica.NTSLabel
  Public WithEvents edAr_gruppo As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_sotgru As NTSInformatica.NTSLabel
  Public WithEvents edAr_sotgru As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_controp As NTSInformatica.NTSLabel
  Public WithEvents edAr_controp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_controa As NTSInformatica.NTSLabel
  Public WithEvents edAr_controa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_tipo As NTSInformatica.NTSLabel
  Public WithEvents edAr_tipo As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_ubicaz As NTSInformatica.NTSLabel
  Public WithEvents edAr_ubicaz As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdArtgif2 As NTSInformatica.NTSButton
  Public WithEvents cmdArtGif1 As NTSInformatica.NTSButton
  Public WithEvents pnTopLeft As NTSInformatica.NTSPanel
  Public WithEvents lbAr_codart As NTSInformatica.NTSLabel
  Public WithEvents edAr_desint As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbAr_codtipa As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_codalt As NTSInformatica.NTSLabel
  Public WithEvents lbAr_descr As NTSInformatica.NTSLabel
  Public WithEvents edAr_codalt As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_descr As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_codart As NTSInformatica.NTSTextBoxStr
  Public WithEvents ceListini As NTSInformatica.NTSXXLIST
  Public WithEvents ceSconti As NTSInformatica.NTSXXSCON
  Public WithEvents ceProvvig As NTSInformatica.NTSXXPROV
  Public WithEvents pnTopLeftBut As NTSInformatica.NTSPanel
  Public WithEvents cmdCodarfo As NTSInformatica.NTSButton
  Public WithEvents cmdProgressivi As NTSInformatica.NTSButton
  Public WithEvents cmdValuta As NTSInformatica.NTSButton
  Public WithEvents cmdProgtot As NTSInformatica.NTSButton
  Public WithEvents OpenFileDialog1 As NTSOpenFileDialog
#End Region

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGARTI))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbOption = New NTSInformatica.NTSBarSubItem
    Me.tlbApriUltimaRicerca = New NTSInformatica.NTSBarMenuItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbStampaEtichette = New NTSInformatica.NTSBarMenuItem
    Me.tlbAcquisisciDaCatForn = New NTSInformatica.NTSBarMenuItem
    Me.tlbRicalcolaListini = New NTSInformatica.NTSBarMenuItem
    Me.tlbZoomRigheOffertePerArt = New NTSInformatica.NTSBarMenuItem
    Me.tlbVisualizzaMovimenti = New NTSInformatica.NTSBarMenuItem
    Me.tlbControlloQualita = New NTSInformatica.NTSBarMenuItem
    Me.tlbArticoliMagazzino = New NTSInformatica.NTSBarMenuItem
    Me.tlbAttributiArticolo = New NTSInformatica.NTSBarMenuItem
    Me.tlbFasiArticolo = New NTSInformatica.NTSBarMenuItem
    Me.tlbPromozioni = New NTSInformatica.NTSBarMenuItem
    Me.tlbAccessoriSuccedanei = New NTSInformatica.NTSBarMenuItem
    Me.tlbGift = New NTSInformatica.NTSBarButtonItem
    Me.tlbEstensioni = New NTSInformatica.NTSBarMenuItem
    Me.tlbSimula = New NTSInformatica.NTSBarMenuItem
    Me.tlbBarcode = New NTSInformatica.NTSBarButtonItem
    Me.tlbKit = New NTSInformatica.NTSBarButtonItem
    Me.tlbConai = New NTSInformatica.NTSBarButtonItem
    Me.tlbOle = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnArti = New NTSInformatica.NTSPanel
    Me.pnMain = New NTSInformatica.NTSPanel
    Me.tsArti = New NTSInformatica.NTSTabControl
    Me.NtsTabPage9 = New NTSInformatica.NTSTabPage
    Me.pnTabpag9 = New NTSInformatica.NTSPanel
    Me.lbAr_deterior = New NTSInformatica.NTSLabel
    Me.cbAr_deterior = New NTSInformatica.NTSComboBox
    Me.edAr_codtlox = New NTSInformatica.NTSTextBoxNum
    Me.cbAr_tipscarlotx = New NTSInformatica.NTSComboBox
    Me.lbAr_tipscarlotx = New NTSInformatica.NTSLabel
    Me.lbXx_codtlox = New NTSInformatica.NTSLabel
    Me.lbAr_codtlox = New NTSInformatica.NTSLabel
    Me.fmCadc = New NTSInformatica.NTSGroupBox
    Me.edAr_coddicv = New NTSInformatica.NTSTextBoxStr
    Me.edAr_coddica = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_codtcdc = New NTSInformatica.NTSLabel
    Me.edAr_codtcdc = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codtcdc = New NTSInformatica.NTSLabel
    Me.lbXx_coddicv = New NTSInformatica.NTSLabel
    Me.lbAr_coddica = New NTSInformatica.NTSLabel
    Me.lbAr_coddicv = New NTSInformatica.NTSLabel
    Me.lbXx_coddica = New NTSInformatica.NTSLabel
    Me.fmUbiFasi = New NTSInformatica.NTSGroupBox
    Me.lbAr_ultfase = New NTSInformatica.NTSLabel
    Me.edAr_ultfase = New NTSInformatica.NTSTextBoxNum
    Me.ckAr_gesubic = New NTSInformatica.NTSCheckBox
    Me.ckAr_gesfasi = New NTSInformatica.NTSCheckBox
    Me.lbXx_codimba = New NTSInformatica.NTSLabel
    Me.lbXx_cartric = New NTSInformatica.NTSLabel
    Me.lbXx_cartcanas = New NTSInformatica.NTSLabel
    Me.lbXx_cartcanol = New NTSInformatica.NTSLabel
    Me.lbAr_cartcanol = New NTSInformatica.NTSLabel
    Me.edAr_cartcanol = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_tipitemcp3 = New NTSInformatica.NTSLabel
    Me.cbAr_tipitemcp3 = New NTSInformatica.NTSComboBox
    Me.lbAr_cartric = New NTSInformatica.NTSLabel
    Me.edAr_cartric = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_cartcanas = New NTSInformatica.NTSLabel
    Me.edAr_cartcanas = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_gestser = New NTSInformatica.NTSLabel
    Me.cbAr_gestser = New NTSInformatica.NTSComboBox
    Me.lbAr_makebuy = New NTSInformatica.NTSLabel
    Me.cbAr_makebuy = New NTSInformatica.NTSComboBox
    Me.lbAr_volume = New NTSInformatica.NTSLabel
    Me.edAr_volume = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_codimba = New NTSInformatica.NTSLabel
    Me.edAr_codimba = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_misura1 = New NTSInformatica.NTSLabel
    Me.edAr_misura1 = New NTSInformatica.NTSTextBoxNum
    Me.edAr_misura2 = New NTSInformatica.NTSTextBoxNum
    Me.edAr_misura3 = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_flmod = New NTSInformatica.NTSLabel
    Me.edAr_flmod = New NTSInformatica.NTSTextBoxStr
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnTabpag1 = New NTSInformatica.NTSPanel
    Me.pnTabpag1Right = New NTSInformatica.NTSPanel
    Me.fmEcommerce = New NTSInformatica.NTSGroupBox
    Me.lbXx_codseat = New NTSInformatica.NTSLabel
    Me.ckAr_webvend = New NTSInformatica.NTSCheckBox
    Me.ckAr_webusat = New NTSInformatica.NTSCheckBox
    Me.ckAr_webvis = New NTSInformatica.NTSCheckBox
    Me.lbAr_codseat = New NTSInformatica.NTSLabel
    Me.edAr_codseat = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_numecr = New NTSInformatica.NTSLabel
    Me.fmIntrastat = New NTSInformatica.NTSGroupBox
    Me.lbAr_umintra2 = New NTSInformatica.NTSLabel
    Me.cbAr_umintra2 = New NTSInformatica.NTSComboBox
    Me.lbAr_codnomc = New NTSInformatica.NTSLabel
    Me.edAr_codnomc = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_percvst = New NTSInformatica.NTSLabel
    Me.edAr_percvst = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_prorig = New NTSInformatica.NTSLabel
    Me.edAr_prorig = New NTSInformatica.NTSTextBoxStr
    Me.edAr_paeorig = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_sostit = New NTSInformatica.NTSLabel
    Me.edAr_sostit = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_sostituito = New NTSInformatica.NTSLabel
    Me.edAr_sostituito = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_numecr = New NTSInformatica.NTSLabel
    Me.edAr_numecr = New NTSInformatica.NTSTextBoxNum
    Me.pnTabpag1Left = New NTSInformatica.NTSPanel
    Me.lbXx_reparto = New NTSInformatica.NTSLabel
    Me.lbXx_codpdon = New NTSInformatica.NTSLabel
    Me.lbXx_codappr = New NTSInformatica.NTSLabel
    Me.lbXx_codmarc = New NTSInformatica.NTSLabel
    Me.lbAr_gescon = New NTSInformatica.NTSLabel
    Me.cbAr_gescon = New NTSInformatica.NTSComboBox
    Me.lbAr_codappr = New NTSInformatica.NTSLabel
    Me.edAr_codappr = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_tipokit = New NTSInformatica.NTSLabel
    Me.cbAr_tipokit = New NTSInformatica.NTSComboBox
    Me.lbAr_flricmar = New NTSInformatica.NTSLabel
    Me.cbAr_flricmar = New NTSInformatica.NTSComboBox
    Me.lbAr_codpdon = New NTSInformatica.NTSLabel
    Me.edAr_codpdon = New NTSInformatica.NTSTextBoxNum
    Me.edAr_ricar1 = New NTSInformatica.NTSTextBoxNum
    Me.edAr_ricar2 = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_reparto = New NTSInformatica.NTSLabel
    Me.edAr_reparto = New NTSInformatica.NTSTextBoxNum
    Me.edAr_garacq = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_garven = New NTSInformatica.NTSLabel
    Me.edAr_garven = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_perqta = New NTSInformatica.NTSLabel
    Me.edAr_perqta = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_codmarc = New NTSInformatica.NTSLabel
    Me.edAr_codmarc = New NTSInformatica.NTSTextBoxNum
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnTabpag2 = New NTSInformatica.NTSPanel
    Me.fmProduzione = New NTSInformatica.NTSGroupBox
    Me.ckAr_consmrp = New NTSInformatica.NTSCheckBox
    Me.ckAr_critico = New NTSInformatica.NTSCheckBox
    Me.ckAr_blocco = New NTSInformatica.NTSCheckBox
    Me.lbAr_tipoopz = New NTSInformatica.NTSLabel
    Me.cbAr_tipoopz = New NTSInformatica.NTSComboBox
    Me.lbAr_gescomm = New NTSInformatica.NTSLabel
    Me.cbAr_gescomm = New NTSInformatica.NTSComboBox
    Me.lbAr_fpfence = New NTSInformatica.NTSLabel
    Me.edAr_fpfence = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_rrfence = New NTSInformatica.NTSLabel
    Me.edAr_rrfence = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_ggrior = New NTSInformatica.NTSLabel
    Me.edAr_ggrior = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_fcorrlt = New NTSInformatica.NTSLabel
    Me.edAr_fcorrlt = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_verdb = New NTSInformatica.NTSLabel
    Me.edAr_verdb = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_livmindb = New NTSInformatica.NTSLabel
    Me.edAr_livmindb = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_coddb = New NTSInformatica.NTSLabel
    Me.edAr_coddb = New NTSInformatica.NTSTextBoxStr
    Me.fmAcquisti = New NTSInformatica.NTSGroupBox
    Me.lbAr_scosic = New NTSInformatica.NTSLabel
    Me.edAr_scosic = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_forn = New NTSInformatica.NTSLabel
    Me.lbXx_forn2 = New NTSInformatica.NTSLabel
    Me.lbAr_ggant = New NTSInformatica.NTSLabel
    Me.edAr_ggant = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_ggpost = New NTSInformatica.NTSLabel
    Me.edAr_ggpost = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_ggragg = New NTSInformatica.NTSLabel
    Me.edAr_ggragg = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_perragg = New NTSInformatica.NTSLabel
    Me.cbAr_perragg = New NTSInformatica.NTSComboBox
    Me.lbAr_scomax = New NTSInformatica.NTSLabel
    Me.lbAr_maxlotto = New NTSInformatica.NTSLabel
    Me.lbAr_scomin = New NTSInformatica.NTSLabel
    Me.edAr_scomin = New NTSInformatica.NTSTextBoxNum
    Me.edAr_scomax = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_sublotto = New NTSInformatica.NTSLabel
    Me.edAr_sublotto = New NTSInformatica.NTSTextBoxNum
    Me.edAr_maxlotto = New NTSInformatica.NTSTextBoxNum
    Me.ckAr_ripriord = New NTSInformatica.NTSCheckBox
    Me.lbAr_minord = New NTSInformatica.NTSLabel
    Me.edAr_minord = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_polriord = New NTSInformatica.NTSLabel
    Me.cbAr_polriord = New NTSInformatica.NTSComboBox
    Me.edAr_forn = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_forn = New NTSInformatica.NTSLabel
    Me.edAr_forn2 = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_forn2 = New NTSInformatica.NTSLabel
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnTabpag3 = New NTSInformatica.NTSPanel
    Me.pnTabpag3Right = New NTSInformatica.NTSPanel
    Me.lbXx_codvuo = New NTSInformatica.NTSLabel
    Me.fmAltriDati = New NTSInformatica.NTSGroupBox
    Me.ckAr_flgift = New NTSInformatica.NTSCheckBox
    Me.ckAr_stainv = New NTSInformatica.NTSCheckBox
    Me.ckAr_stasche = New NTSInformatica.NTSCheckBox
    Me.ckAr_geslotti = New NTSInformatica.NTSCheckBox
    Me.ckAr_inesaur = New NTSInformatica.NTSCheckBox
    Me.ckAr_pesoca = New NTSInformatica.NTSCheckBox
    Me.ckAr_stalist = New NTSInformatica.NTSCheckBox
    Me.ckAr_gestmatr = New NTSInformatica.NTSCheckBox
    Me.lbAr_contriva = New NTSInformatica.NTSLabel
    Me.edAr_contriva = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_codvuo = New NTSInformatica.NTSLabel
    Me.edAr_codvuo = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_pesolor = New NTSInformatica.NTSLabel
    Me.edAr_pesolor = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_pesonet = New NTSInformatica.NTSLabel
    Me.edAr_pesonet = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_catlifo = New NTSInformatica.NTSLabel
    Me.edAr_catlifo = New NTSInformatica.NTSTextBoxNum
    Me.pnTabpag3Left = New NTSInformatica.NTSPanel
    Me.cmdClassificaDeleteFilter = New NTSInformatica.NTSButton
    Me.cmdClassifica = New NTSInformatica.NTSButton
    Me.lbClassifica = New NTSInformatica.NTSLabel
    Me.lbXx_clascon = New NTSInformatica.NTSLabel
    Me.lbXx_claprov = New NTSInformatica.NTSLabel
    Me.lbAr_claprov = New NTSInformatica.NTSLabel
    Me.edAr_claprov = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_clascon = New NTSInformatica.NTSLabel
    Me.edAr_clascon = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codiva = New NTSInformatica.NTSLabel
    Me.lbXx_gruppo = New NTSInformatica.NTSLabel
    Me.lbXx_sotgru = New NTSInformatica.NTSLabel
    Me.lbXx_controp = New NTSInformatica.NTSLabel
    Me.lbXx_controa = New NTSInformatica.NTSLabel
    Me.lbXx_contros = New NTSInformatica.NTSLabel
    Me.lbXx_famprod = New NTSInformatica.NTSLabel
    Me.lbAr_famprod = New NTSInformatica.NTSLabel
    Me.edAr_famprod = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_contros = New NTSInformatica.NTSLabel
    Me.edAr_contros = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_codiva = New NTSInformatica.NTSLabel
    Me.edAr_codiva = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_gruppo = New NTSInformatica.NTSLabel
    Me.edAr_gruppo = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_sotgru = New NTSInformatica.NTSLabel
    Me.edAr_sotgru = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_controp = New NTSInformatica.NTSLabel
    Me.edAr_controp = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_controa = New NTSInformatica.NTSLabel
    Me.edAr_controa = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_tipo = New NTSInformatica.NTSLabel
    Me.edAr_tipo = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_ubicaz = New NTSInformatica.NTSLabel
    Me.edAr_ubicaz = New NTSInformatica.NTSTextBoxStr
    Me.NtsTabPage4 = New NTSInformatica.NTSTabPage
    Me.pnTabpag4 = New NTSInformatica.NTSPanel
    Me.ceListini = New NTSInformatica.NTSXXLIST
    Me.NtsTabPage5 = New NTSInformatica.NTSTabPage
    Me.pnTabpag5 = New NTSInformatica.NTSPanel
    Me.ceSconti = New NTSInformatica.NTSXXSCON
    Me.NtsTabPage6 = New NTSInformatica.NTSTabPage
    Me.pnTabpag6 = New NTSInformatica.NTSPanel
    Me.ceProvvig = New NTSInformatica.NTSXXPROV
    Me.NtsTabPage7 = New NTSInformatica.NTSTabPage
    Me.pnTabpag7 = New NTSInformatica.NTSPanel
    Me.edAr_note = New NTSInformatica.NTSMemoBox
    Me.NtsTabPage8 = New NTSInformatica.NTSTabPage
    Me.pnTabpag8 = New NTSInformatica.NTSPanel
    Me.pnMaga2 = New NTSInformatica.NTSPanel
    Me.fmLogisticaPalmare = New NTSInformatica.NTSGroupBox
    Me.lbXx_codgrlo = New NTSInformatica.NTSLabel
    Me.lbAr_ubicus = New NTSInformatica.NTSLabel
    Me.lbAr_ubicri = New NTSInformatica.NTSLabel
    Me.lbAr_ubicpr = New NTSInformatica.NTSLabel
    Me.lbAr_ubicst = New NTSInformatica.NTSLabel
    Me.edAr_ubicpr = New NTSInformatica.NTSTextBoxStr
    Me.edAr_ubicri = New NTSInformatica.NTSTextBoxStr
    Me.edAr_ubicus = New NTSInformatica.NTSTextBoxStr
    Me.edAr_ubicst = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_codgrlo = New NTSInformatica.NTSLabel
    Me.lbAr_scominpk = New NTSInformatica.NTSLabel
    Me.lbAr_indrot = New NTSInformatica.NTSLabel
    Me.lbAr_converp = New NTSInformatica.NTSLabel
    Me.edAr_indrot = New NTSInformatica.NTSTextBoxNum
    Me.edAr_scominpk = New NTSInformatica.NTSTextBoxNum
    Me.edAr_codgrlo = New NTSInformatica.NTSTextBoxNum
    Me.edAr_converp = New NTSInformatica.NTSTextBoxNum
    Me.ckAr_staetip = New NTSInformatica.NTSCheckBox
    Me.ckAr_staeti = New NTSInformatica.NTSCheckBox
    Me.pnMaga1 = New NTSInformatica.NTSPanel
    Me.lbAr_magstock = New NTSInformatica.NTSLabel
    Me.cmdArtgif2 = New NTSInformatica.NTSButton
    Me.edAr_magprod = New NTSInformatica.NTSTextBoxNum
    Me.cmdArtGif1 = New NTSInformatica.NTSButton
    Me.lbAr_magprod = New NTSInformatica.NTSLabel
    Me.cmdVisGif2 = New NTSInformatica.NTSButton
    Me.edAr_magstock = New NTSInformatica.NTSTextBoxNum
    Me.cmdVisGif1 = New NTSInformatica.NTSButton
    Me.cbAr_umdapr = New NTSInformatica.NTSComboBox
    Me.lbAr_um4 = New NTSInformatica.NTSLabel
    Me.lbAr_umdapr = New NTSInformatica.NTSLabel
    Me.edAr_um4 = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_magprod = New NTSInformatica.NTSLabel
    Me.lbAr_formula = New NTSInformatica.NTSLabel
    Me.lbXx_magstock = New NTSInformatica.NTSLabel
    Me.edAr_formula = New NTSInformatica.NTSTextBoxStr
    Me.cbAr_umdapra = New NTSInformatica.NTSComboBox
    Me.lbAr_umpdapr = New NTSInformatica.NTSLabel
    Me.lbAr_umdapra = New NTSInformatica.NTSLabel
    Me.cbAr_umpdapr = New NTSInformatica.NTSComboBox
    Me.edAr_gif2 = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_umpdapra = New NTSInformatica.NTSLabel
    Me.lbAr_gif2 = New NTSInformatica.NTSLabel
    Me.cbAr_umpdapra = New NTSInformatica.NTSComboBox
    Me.edAr_gif1 = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_gif1 = New NTSInformatica.NTSLabel
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.ceColl = New NTSInformatica.NTSXXCOLL
    Me.pnTopLeft = New NTSInformatica.NTSPanel
    Me.pnTopLeftBut = New NTSInformatica.NTSPanel
    Me.cmdCodarfo = New NTSInformatica.NTSButton
    Me.cmdProgressivi = New NTSInformatica.NTSButton
    Me.cmdValuta = New NTSInformatica.NTSButton
    Me.cmdProgtot = New NTSInformatica.NTSButton
    Me.lbAr_codart = New NTSInformatica.NTSLabel
    Me.edAr_desint = New NTSInformatica.NTSTextBoxStr
    Me.cbAr_codtipa = New NTSInformatica.NTSComboBox
    Me.lbAr_codalt = New NTSInformatica.NTSLabel
    Me.lbAr_descr = New NTSInformatica.NTSLabel
    Me.edAr_codalt = New NTSInformatica.NTSTextBoxStr
    Me.edAr_descr = New NTSInformatica.NTSTextBoxStr
    Me.edAr_codart = New NTSInformatica.NTSTextBoxStr
    Me.fmUnitamisura = New NTSInformatica.NTSGroupBox
    Me.edAr_unmis = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_unmis = New NTSInformatica.NTSLabel
    Me.lbAr_conver = New NTSInformatica.NTSLabel
    Me.lbAr_unmis2 = New NTSInformatica.NTSLabel
    Me.edAr_conver = New NTSInformatica.NTSTextBoxNum
    Me.edAr_confez2 = New NTSInformatica.NTSTextBoxStr
    Me.edAr_unmis2 = New NTSInformatica.NTSTextBoxStr
    Me.edAr_qtacon2 = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_confez2 = New NTSInformatica.NTSLabel
    Me.edFocus = New NTSInformatica.NTSTextBoxNum
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnArti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnArti.SuspendLayout()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMain.SuspendLayout()
    CType(Me.tsArti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsArti.SuspendLayout()
    Me.NtsTabPage9.SuspendLayout()
    CType(Me.pnTabpag9, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag9.SuspendLayout()
    CType(Me.cbAr_deterior.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codtlox.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_tipscarlotx.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmCadc, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCadc.SuspendLayout()
    CType(Me.edAr_coddicv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_coddica.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codtcdc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmUbiFasi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmUbiFasi.SuspendLayout()
    CType(Me.edAr_ultfase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_gesubic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_gesfasi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_cartcanol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_tipitemcp3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_cartric.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_cartcanas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_gestser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_makebuy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_volume.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codimba.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_misura1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_misura2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_misura3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_flmod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnTabpag1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag1.SuspendLayout()
    CType(Me.pnTabpag1Right, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag1Right.SuspendLayout()
    CType(Me.fmEcommerce, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmEcommerce.SuspendLayout()
    CType(Me.ckAr_webvend.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_webusat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_webvis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codseat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmIntrastat, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmIntrastat.SuspendLayout()
    CType(Me.cbAr_umintra2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codnomc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_percvst.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_prorig.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_paeorig.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_sostit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_sostituito.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_numecr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTabpag1Left, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag1Left.SuspendLayout()
    CType(Me.cbAr_gescon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codappr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_tipokit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_flricmar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codpdon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_ricar1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_ricar2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_reparto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_garacq.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_garven.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_perqta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codmarc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnTabpag2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag2.SuspendLayout()
    CType(Me.fmProduzione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmProduzione.SuspendLayout()
    CType(Me.ckAr_consmrp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_critico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_blocco.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_tipoopz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_gescomm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_fpfence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_rrfence.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_ggrior.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_fcorrlt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_verdb.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_livmindb.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_coddb.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAcquisti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAcquisti.SuspendLayout()
    CType(Me.edAr_scosic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_ggant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_ggpost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_ggragg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_perragg.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_scomin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_scomax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_sublotto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_maxlotto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_ripriord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_minord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_polriord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_forn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_forn2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.pnTabpag3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag3.SuspendLayout()
    CType(Me.pnTabpag3Right, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag3Right.SuspendLayout()
    CType(Me.fmAltriDati, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAltriDati.SuspendLayout()
    CType(Me.ckAr_flgift.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_stainv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_stasche.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_geslotti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_inesaur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_pesoca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_stalist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_gestmatr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_contriva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codvuo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_pesolor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_pesonet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_catlifo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTabpag3Left, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag3Left.SuspendLayout()
    CType(Me.edAr_claprov.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_clascon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_famprod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_contros.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codiva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_gruppo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_sotgru.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_controp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_controa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_tipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_ubicaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage4.SuspendLayout()
    CType(Me.pnTabpag4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag4.SuspendLayout()
    Me.NtsTabPage5.SuspendLayout()
    CType(Me.pnTabpag5, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag5.SuspendLayout()
    Me.NtsTabPage6.SuspendLayout()
    CType(Me.pnTabpag6, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag6.SuspendLayout()
    Me.NtsTabPage7.SuspendLayout()
    CType(Me.pnTabpag7, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag7.SuspendLayout()
    CType(Me.edAr_note.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage8.SuspendLayout()
    CType(Me.pnTabpag8, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag8.SuspendLayout()
    CType(Me.pnMaga2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMaga2.SuspendLayout()
    CType(Me.fmLogisticaPalmare, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmLogisticaPalmare.SuspendLayout()
    CType(Me.edAr_ubicpr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_ubicri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_ubicus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_ubicst.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_indrot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_scominpk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codgrlo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_converp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_staetip.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_staeti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnMaga1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMaga1.SuspendLayout()
    CType(Me.edAr_magprod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_magstock.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_umdapr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_um4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_formula.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_umdapra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_umpdapr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_gif2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_umpdapra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_gif1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnTopLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTopLeft.SuspendLayout()
    CType(Me.pnTopLeftBut, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTopLeftBut.SuspendLayout()
    CType(Me.edAr_desint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_codtipa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codalt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_descr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmUnitamisura, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmUnitamisura.SuspendLayout()
    CType(Me.edAr_unmis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_conver.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_confez2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_unmis2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_qtacon2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFocus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo, Me.tlbStrumenti, Me.tlbDuplica, Me.tlbApri, Me.tlbBarcode, Me.tlbKit, Me.tlbConai, Me.tlbOle, Me.tlbStampaEtichette, Me.tlbAcquisisciDaCatForn, Me.tlbRicalcolaListini, Me.tlbZoomRigheOffertePerArt, Me.tlbVisualizzaMovimenti, Me.tlbControlloQualita, Me.tlbArticoliMagazzino, Me.tlbAttributiArticolo, Me.tlbFasiArticolo, Me.tlbPromozioni, Me.tlbAccessoriSuccedanei, Me.tlbOption, Me.tlbApriUltimaRicerca, Me.tlbGift, Me.tlbEstensioni, Me.tlbSimula})
    Me.NtsBarManager1.MaxItemId = 49
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOption), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbBarcode, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbKit), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbConai), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOle), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbNuovo.GlyphPath = ""
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbDuplica
    '
    Me.tlbDuplica.Caption = "Duplica"
    Me.tlbDuplica.Glyph = CType(resources.GetObject("tlbDuplica.Glyph"), System.Drawing.Image)
    Me.tlbDuplica.GlyphPath = ""
    Me.tlbDuplica.Id = 26
    Me.tlbDuplica.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F2))
    Me.tlbDuplica.Name = "tlbDuplica"
    Me.tlbDuplica.Visible = True
    '
    'tlbApri
    '
    Me.tlbApri.Caption = "Apri"
    Me.tlbApri.Glyph = CType(resources.GetObject("tlbApri.Glyph"), System.Drawing.Image)
    Me.tlbApri.GlyphPath = ""
    Me.tlbApri.Id = 27
    Me.tlbApri.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3)
    Me.tlbApri.Name = "tlbApri"
    Me.tlbApri.Visible = True
    '
    'tlbOption
    '
    Me.tlbOption.GlyphPath = ""
    Me.tlbOption.Id = 44
    Me.tlbOption.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApriUltimaRicerca)})
    Me.tlbOption.Name = "tlbOption"
    Me.tlbOption.Visible = True
    '
    'tlbApriUltimaRicerca
    '
    Me.tlbApriUltimaRicerca.Caption = "Apri da ultima ricerca"
    Me.tlbApriUltimaRicerca.GlyphPath = ""
    Me.tlbApriUltimaRicerca.Id = 45
    Me.tlbApriUltimaRicerca.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F3))
    Me.tlbApriUltimaRicerca.Name = "tlbApriUltimaRicerca"
    Me.tlbApriUltimaRicerca.NTSIsCheckBox = False
    Me.tlbApriUltimaRicerca.Visible = True
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
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.GlyphPath = ""
    Me.tlbPrimo.Id = 5
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.GlyphPath = ""
    Me.tlbPrecedente.Id = 6
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.GlyphPath = ""
    Me.tlbSuccessivo.Id = 7
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.GlyphPath = ""
    Me.tlbUltimo.Id = 20
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 22
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaEtichette), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAcquisisciDaCatForn), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRicalcolaListini), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoomRigheOffertePerArt), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbVisualizzaMovimenti), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbControlloQualita), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbArticoliMagazzino), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAttributiArticolo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbFasiArticolo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPromozioni), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAccessoriSuccedanei), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGift), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEstensioni, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSimula, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbStampaEtichette
    '
    Me.tlbStampaEtichette.Caption = "Stampa Etichette"
    Me.tlbStampaEtichette.GlyphPath = ""
    Me.tlbStampaEtichette.Id = 32
    Me.tlbStampaEtichette.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E))
    Me.tlbStampaEtichette.Name = "tlbStampaEtichette"
    Me.tlbStampaEtichette.NTSIsCheckBox = False
    Me.tlbStampaEtichette.Visible = True
    '
    'tlbAcquisisciDaCatForn
    '
    Me.tlbAcquisisciDaCatForn.Caption = "Acquisisci da catalogo fornitore"
    Me.tlbAcquisisciDaCatForn.GlyphPath = ""
    Me.tlbAcquisisciDaCatForn.Id = 33
    Me.tlbAcquisisciDaCatForn.Name = "tlbAcquisisciDaCatForn"
    Me.tlbAcquisisciDaCatForn.NTSIsCheckBox = False
    Me.tlbAcquisisciDaCatForn.Visible = True
    '
    'tlbRicalcolaListini
    '
    Me.tlbRicalcolaListini.Caption = "Ricalcola listini"
    Me.tlbRicalcolaListini.GlyphPath = ""
    Me.tlbRicalcolaListini.Id = 34
    Me.tlbRicalcolaListini.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F6))
    Me.tlbRicalcolaListini.Name = "tlbRicalcolaListini"
    Me.tlbRicalcolaListini.NTSIsCheckBox = False
    Me.tlbRicalcolaListini.Visible = True
    '
    'tlbZoomRigheOffertePerArt
    '
    Me.tlbZoomRigheOffertePerArt.Caption = "Zoom righe offerte per articolo"
    Me.tlbZoomRigheOffertePerArt.GlyphPath = ""
    Me.tlbZoomRigheOffertePerArt.Id = 35
    Me.tlbZoomRigheOffertePerArt.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O))
    Me.tlbZoomRigheOffertePerArt.Name = "tlbZoomRigheOffertePerArt"
    Me.tlbZoomRigheOffertePerArt.NTSIsCheckBox = False
    Me.tlbZoomRigheOffertePerArt.Visible = True
    '
    'tlbVisualizzaMovimenti
    '
    Me.tlbVisualizzaMovimenti.Caption = "Visualizza movimenti"
    Me.tlbVisualizzaMovimenti.GlyphPath = ""
    Me.tlbVisualizzaMovimenti.Id = 36
    Me.tlbVisualizzaMovimenti.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F7))
    Me.tlbVisualizzaMovimenti.Name = "tlbVisualizzaMovimenti"
    Me.tlbVisualizzaMovimenti.NTSIsCheckBox = False
    Me.tlbVisualizzaMovimenti.Visible = True
    '
    'tlbControlloQualita
    '
    Me.tlbControlloQualita.Caption = "Controllo Qualità"
    Me.tlbControlloQualita.GlyphPath = ""
    Me.tlbControlloQualita.Id = 38
    Me.tlbControlloQualita.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q))
    Me.tlbControlloQualita.Name = "tlbControlloQualita"
    Me.tlbControlloQualita.NTSIsCheckBox = False
    Me.tlbControlloQualita.Visible = True
    '
    'tlbArticoliMagazzino
    '
    Me.tlbArticoliMagazzino.Caption = "Articoli/Magazzino"
    Me.tlbArticoliMagazzino.GlyphPath = ""
    Me.tlbArticoliMagazzino.Id = 39
    Me.tlbArticoliMagazzino.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F12))
    Me.tlbArticoliMagazzino.Name = "tlbArticoliMagazzino"
    Me.tlbArticoliMagazzino.NTSIsCheckBox = False
    Me.tlbArticoliMagazzino.Visible = True
    '
    'tlbAttributiArticolo
    '
    Me.tlbAttributiArticolo.Caption = "Attributi articolo"
    Me.tlbAttributiArticolo.GlyphPath = ""
    Me.tlbAttributiArticolo.Id = 40
    Me.tlbAttributiArticolo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T))
    Me.tlbAttributiArticolo.Name = "tlbAttributiArticolo"
    Me.tlbAttributiArticolo.NTSIsCheckBox = False
    Me.tlbAttributiArticolo.Visible = True
    '
    'tlbFasiArticolo
    '
    Me.tlbFasiArticolo.Caption = "Fasi Articolo"
    Me.tlbFasiArticolo.GlyphPath = ""
    Me.tlbFasiArticolo.Id = 41
    Me.tlbFasiArticolo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F))
    Me.tlbFasiArticolo.Name = "tlbFasiArticolo"
    Me.tlbFasiArticolo.NTSIsCheckBox = False
    Me.tlbFasiArticolo.Visible = True
    '
    'tlbPromozioni
    '
    Me.tlbPromozioni.Caption = "Promozioni"
    Me.tlbPromozioni.GlyphPath = ""
    Me.tlbPromozioni.Id = 42
    Me.tlbPromozioni.Name = "tlbPromozioni"
    Me.tlbPromozioni.NTSIsCheckBox = False
    Me.tlbPromozioni.Visible = True
    '
    'tlbAccessoriSuccedanei
    '
    Me.tlbAccessoriSuccedanei.Caption = "Accessori/succedanei"
    Me.tlbAccessoriSuccedanei.GlyphPath = ""
    Me.tlbAccessoriSuccedanei.Id = 43
    Me.tlbAccessoriSuccedanei.Name = "tlbAccessoriSuccedanei"
    Me.tlbAccessoriSuccedanei.NTSIsCheckBox = False
    Me.tlbAccessoriSuccedanei.Visible = True
    '
    'tlbGift
    '
    Me.tlbGift.Caption = "Dettaglio Gift Card"
    Me.tlbGift.GlyphPath = ""
    Me.tlbGift.Id = 46
    Me.tlbGift.Name = "tlbGift"
    Me.tlbGift.Visible = True
    '
    'tlbEstensioni
    '
    Me.tlbEstensioni.Caption = "Estensioni anagrafiche"
    Me.tlbEstensioni.GlyphPath = ""
    Me.tlbEstensioni.Id = 47
    Me.tlbEstensioni.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N))
    Me.tlbEstensioni.Name = "tlbEstensioni"
    Me.tlbEstensioni.NTSIsCheckBox = False
    Me.tlbEstensioni.Visible = True
    '
    'tlbSimula
    '
    Me.tlbSimula.Caption = "Simulazione vendita"
    Me.tlbSimula.GlyphPath = ""
    Me.tlbSimula.Id = 48
    Me.tlbSimula.Name = "tlbSimula"
    Me.tlbSimula.NTSIsCheckBox = False
    Me.tlbSimula.Visible = True
    '
    'tlbBarcode
    '
    Me.tlbBarcode.Caption = "Barcode"
    Me.tlbBarcode.Glyph = CType(resources.GetObject("tlbBarcode.Glyph"), System.Drawing.Image)
    Me.tlbBarcode.GlyphPath = ""
    Me.tlbBarcode.Id = 28
    Me.tlbBarcode.Name = "tlbBarcode"
    Me.tlbBarcode.Visible = True
    '
    'tlbKit
    '
    Me.tlbKit.Caption = "Kit"
    Me.tlbKit.Glyph = CType(resources.GetObject("tlbKit.Glyph"), System.Drawing.Image)
    Me.tlbKit.GlyphPath = ""
    Me.tlbKit.Id = 29
    Me.tlbKit.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.K))
    Me.tlbKit.Name = "tlbKit"
    Me.tlbKit.Visible = True
    '
    'tlbConai
    '
    Me.tlbConai.Caption = "Conai"
    Me.tlbConai.Glyph = CType(resources.GetObject("tlbConai.Glyph"), System.Drawing.Image)
    Me.tlbConai.GlyphPath = ""
    Me.tlbConai.Id = 30
    Me.tlbConai.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A))
    Me.tlbConai.Name = "tlbConai"
    Me.tlbConai.Visible = True
    '
    'tlbOle
    '
    Me.tlbOle.Caption = "Ole"
    Me.tlbOle.Glyph = CType(resources.GetObject("tlbOle.Glyph"), System.Drawing.Image)
    Me.tlbOle.GlyphPath = ""
    Me.tlbOle.Id = 31
    Me.tlbOle.Name = "tlbOle"
    Me.tlbOle.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnArti
    '
    Me.pnArti.AllowDrop = True
    Me.pnArti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnArti.Appearance.Options.UseBackColor = True
    Me.pnArti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnArti.Controls.Add(Me.pnMain)
    Me.pnArti.Controls.Add(Me.pnTop)
    Me.pnArti.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnArti.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnArti.Location = New System.Drawing.Point(0, 30)
    Me.pnArti.Name = "pnArti"
    Me.pnArti.NTSActiveTrasparency = True
    Me.pnArti.Size = New System.Drawing.Size(764, 449)
    Me.pnArti.TabIndex = 503
    Me.pnArti.Text = "NtsPanel1"
    '
    'pnMain
    '
    Me.pnMain.AllowDrop = True
    Me.pnMain.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnMain.Appearance.Options.UseBackColor = True
    Me.pnMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnMain.Controls.Add(Me.tsArti)
    Me.pnMain.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMain.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnMain.Location = New System.Drawing.Point(0, 125)
    Me.pnMain.Name = "pnMain"
    Me.pnMain.NTSActiveTrasparency = True
    Me.pnMain.Size = New System.Drawing.Size(764, 324)
    Me.pnMain.TabIndex = 576
    Me.pnMain.Text = "NtsPanel2"
    '
    'tsArti
    '
    Me.tsArti.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsArti.Location = New System.Drawing.Point(0, 0)
    Me.tsArti.Name = "tsArti"
    Me.tsArti.SelectedTabPage = Me.NtsTabPage9
    Me.tsArti.Size = New System.Drawing.Size(764, 324)
    Me.tsArti.TabIndex = 582
    Me.tsArti.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage3, Me.NtsTabPage4, Me.NtsTabPage5, Me.NtsTabPage6, Me.NtsTabPage7, Me.NtsTabPage8, Me.NtsTabPage9})
    Me.tsArti.Text = "NtsTabControl1"
    '
    'NtsTabPage9
    '
    Me.NtsTabPage9.AllowDrop = True
    Me.NtsTabPage9.Controls.Add(Me.pnTabpag9)
    Me.NtsTabPage9.Enable = True
    Me.NtsTabPage9.Name = "NtsTabPage9"
    Me.NtsTabPage9.Size = New System.Drawing.Size(755, 294)
    Me.NtsTabPage9.Text = "&9 - Dati aggiuntivi"
    '
    'pnTabpag9
    '
    Me.pnTabpag9.AllowDrop = True
    Me.pnTabpag9.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag9.Appearance.Options.UseBackColor = True
    Me.pnTabpag9.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag9.Controls.Add(Me.lbAr_deterior)
    Me.pnTabpag9.Controls.Add(Me.cbAr_deterior)
    Me.pnTabpag9.Controls.Add(Me.edAr_codtlox)
    Me.pnTabpag9.Controls.Add(Me.cbAr_tipscarlotx)
    Me.pnTabpag9.Controls.Add(Me.lbAr_tipscarlotx)
    Me.pnTabpag9.Controls.Add(Me.lbXx_codtlox)
    Me.pnTabpag9.Controls.Add(Me.lbAr_codtlox)
    Me.pnTabpag9.Controls.Add(Me.fmCadc)
    Me.pnTabpag9.Controls.Add(Me.fmUbiFasi)
    Me.pnTabpag9.Controls.Add(Me.lbXx_codimba)
    Me.pnTabpag9.Controls.Add(Me.lbXx_cartric)
    Me.pnTabpag9.Controls.Add(Me.lbXx_cartcanas)
    Me.pnTabpag9.Controls.Add(Me.lbXx_cartcanol)
    Me.pnTabpag9.Controls.Add(Me.lbAr_cartcanol)
    Me.pnTabpag9.Controls.Add(Me.edAr_cartcanol)
    Me.pnTabpag9.Controls.Add(Me.lbAr_tipitemcp3)
    Me.pnTabpag9.Controls.Add(Me.cbAr_tipitemcp3)
    Me.pnTabpag9.Controls.Add(Me.lbAr_cartric)
    Me.pnTabpag9.Controls.Add(Me.edAr_cartric)
    Me.pnTabpag9.Controls.Add(Me.lbAr_cartcanas)
    Me.pnTabpag9.Controls.Add(Me.edAr_cartcanas)
    Me.pnTabpag9.Controls.Add(Me.lbAr_gestser)
    Me.pnTabpag9.Controls.Add(Me.cbAr_gestser)
    Me.pnTabpag9.Controls.Add(Me.lbAr_makebuy)
    Me.pnTabpag9.Controls.Add(Me.cbAr_makebuy)
    Me.pnTabpag9.Controls.Add(Me.lbAr_volume)
    Me.pnTabpag9.Controls.Add(Me.edAr_volume)
    Me.pnTabpag9.Controls.Add(Me.lbAr_codimba)
    Me.pnTabpag9.Controls.Add(Me.edAr_codimba)
    Me.pnTabpag9.Controls.Add(Me.lbAr_misura1)
    Me.pnTabpag9.Controls.Add(Me.edAr_misura1)
    Me.pnTabpag9.Controls.Add(Me.edAr_misura2)
    Me.pnTabpag9.Controls.Add(Me.edAr_misura3)
    Me.pnTabpag9.Controls.Add(Me.lbAr_flmod)
    Me.pnTabpag9.Controls.Add(Me.edAr_flmod)
    Me.pnTabpag9.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag9.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag9.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag9.Name = "pnTabpag9"
    Me.pnTabpag9.NTSActiveTrasparency = True
    Me.pnTabpag9.Size = New System.Drawing.Size(755, 294)
    Me.pnTabpag9.TabIndex = 0
    Me.pnTabpag9.Text = "NtsPanel1"
    '
    'lbAr_deterior
    '
    Me.lbAr_deterior.AutoSize = True
    Me.lbAr_deterior.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_deterior.Location = New System.Drawing.Point(6, 175)
    Me.lbAr_deterior.Name = "lbAr_deterior"
    Me.lbAr_deterior.NTSDbField = ""
    Me.lbAr_deterior.Size = New System.Drawing.Size(106, 13)
    Me.lbAr_deterior.TabIndex = 669
    Me.lbAr_deterior.Text = "Articolo deteriorabile"
    Me.lbAr_deterior.Tooltip = ""
    Me.lbAr_deterior.UseMnemonic = False
    '
    'cbAr_deterior
    '
    Me.cbAr_deterior.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_deterior.DataSource = Nothing
    Me.cbAr_deterior.DisplayMember = ""
    Me.cbAr_deterior.Location = New System.Drawing.Point(139, 172)
    Me.cbAr_deterior.Name = "cbAr_deterior"
    Me.cbAr_deterior.NTSDbField = ""
    Me.cbAr_deterior.Properties.AutoHeight = False
    Me.cbAr_deterior.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_deterior.Properties.DropDownRows = 30
    Me.cbAr_deterior.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_deterior.SelectedValue = ""
    Me.cbAr_deterior.Size = New System.Drawing.Size(206, 20)
    Me.cbAr_deterior.TabIndex = 670
    Me.cbAr_deterior.ValueMember = ""
    '
    'edAr_codtlox
    '
    Me.edAr_codtlox.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codtlox.EditValue = "0"
    Me.edAr_codtlox.Location = New System.Drawing.Point(139, 264)
    Me.edAr_codtlox.Name = "edAr_codtlox"
    Me.edAr_codtlox.NTSDbField = ""
    Me.edAr_codtlox.NTSFormat = "0"
    Me.edAr_codtlox.NTSForzaVisZoom = False
    Me.edAr_codtlox.NTSOldValue = ""
    Me.edAr_codtlox.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_codtlox.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_codtlox.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codtlox.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codtlox.Properties.AutoHeight = False
    Me.edAr_codtlox.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codtlox.Properties.MaxLength = 65536
    Me.edAr_codtlox.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codtlox.Size = New System.Drawing.Size(66, 20)
    Me.edAr_codtlox.TabIndex = 668
    '
    'cbAr_tipscarlotx
    '
    Me.cbAr_tipscarlotx.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_tipscarlotx.DataSource = Nothing
    Me.cbAr_tipscarlotx.DisplayMember = ""
    Me.cbAr_tipscarlotx.Location = New System.Drawing.Point(654, 264)
    Me.cbAr_tipscarlotx.Name = "cbAr_tipscarlotx"
    Me.cbAr_tipscarlotx.NTSDbField = ""
    Me.cbAr_tipscarlotx.Properties.AutoHeight = False
    Me.cbAr_tipscarlotx.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_tipscarlotx.Properties.DropDownRows = 30
    Me.cbAr_tipscarlotx.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_tipscarlotx.SelectedValue = ""
    Me.cbAr_tipscarlotx.Size = New System.Drawing.Size(90, 20)
    Me.cbAr_tipscarlotx.TabIndex = 667
    Me.cbAr_tipscarlotx.ValueMember = ""
    '
    'lbAr_tipscarlotx
    '
    Me.lbAr_tipscarlotx.AutoSize = True
    Me.lbAr_tipscarlotx.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_tipscarlotx.Location = New System.Drawing.Point(463, 267)
    Me.lbAr_tipscarlotx.Name = "lbAr_tipscarlotx"
    Me.lbAr_tipscarlotx.NTSDbField = ""
    Me.lbAr_tipscarlotx.Size = New System.Drawing.Size(166, 13)
    Me.lbAr_tipscarlotx.TabIndex = 666
    Me.lbAr_tipscarlotx.Text = "Sistema attribuzione autom. lotto"
    Me.lbAr_tipscarlotx.Tooltip = ""
    Me.lbAr_tipscarlotx.UseMnemonic = False
    '
    'lbXx_codtlox
    '
    Me.lbXx_codtlox.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codtlox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codtlox.Location = New System.Drawing.Point(211, 264)
    Me.lbXx_codtlox.Name = "lbXx_codtlox"
    Me.lbXx_codtlox.NTSDbField = ""
    Me.lbXx_codtlox.Size = New System.Drawing.Size(240, 20)
    Me.lbXx_codtlox.TabIndex = 665
    Me.lbXx_codtlox.Tooltip = ""
    Me.lbXx_codtlox.UseMnemonic = False
    '
    'lbAr_codtlox
    '
    Me.lbAr_codtlox.AutoSize = True
    Me.lbAr_codtlox.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codtlox.Location = New System.Drawing.Point(6, 267)
    Me.lbAr_codtlox.Name = "lbAr_codtlox"
    Me.lbAr_codtlox.NTSDbField = ""
    Me.lbAr_codtlox.Size = New System.Drawing.Size(105, 13)
    Me.lbAr_codtlox.TabIndex = 663
    Me.lbAr_codtlox.Text = "Modalità creaz. lotto"
    Me.lbAr_codtlox.Tooltip = ""
    Me.lbAr_codtlox.UseMnemonic = False
    '
    'fmCadc
    '
    Me.fmCadc.AllowDrop = True
    Me.fmCadc.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCadc.Appearance.Options.UseBackColor = True
    Me.fmCadc.Controls.Add(Me.edAr_coddicv)
    Me.fmCadc.Controls.Add(Me.edAr_coddica)
    Me.fmCadc.Controls.Add(Me.lbAr_codtcdc)
    Me.fmCadc.Controls.Add(Me.edAr_codtcdc)
    Me.fmCadc.Controls.Add(Me.lbXx_codtcdc)
    Me.fmCadc.Controls.Add(Me.lbXx_coddicv)
    Me.fmCadc.Controls.Add(Me.lbAr_coddica)
    Me.fmCadc.Controls.Add(Me.lbAr_coddicv)
    Me.fmCadc.Controls.Add(Me.lbXx_coddica)
    Me.fmCadc.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCadc.Location = New System.Drawing.Point(457, 91)
    Me.fmCadc.Name = "fmCadc"
    Me.fmCadc.Size = New System.Drawing.Size(295, 90)
    Me.fmCadc.TabIndex = 662
    Me.fmCadc.Text = "Contabilità analitica duplice contabile"
    '
    'edAr_coddicv
    '
    Me.edAr_coddicv.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_coddicv.EditValue = ""
    Me.edAr_coddicv.Location = New System.Drawing.Point(107, 65)
    Me.edAr_coddicv.Name = "edAr_coddicv"
    Me.edAr_coddicv.NTSDbField = ""
    Me.edAr_coddicv.NTSForzaVisZoom = False
    Me.edAr_coddicv.NTSOldValue = ""
    Me.edAr_coddicv.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_coddicv.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_coddicv.Properties.AutoHeight = False
    Me.edAr_coddicv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_coddicv.Properties.MaxLength = 65536
    Me.edAr_coddicv.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_coddicv.Size = New System.Drawing.Size(88, 20)
    Me.edAr_coddicv.TabIndex = 665
    '
    'edAr_coddica
    '
    Me.edAr_coddica.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_coddica.EditValue = ""
    Me.edAr_coddica.Location = New System.Drawing.Point(107, 43)
    Me.edAr_coddica.Name = "edAr_coddica"
    Me.edAr_coddica.NTSDbField = ""
    Me.edAr_coddica.NTSForzaVisZoom = False
    Me.edAr_coddica.NTSOldValue = ""
    Me.edAr_coddica.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_coddica.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_coddica.Properties.AutoHeight = False
    Me.edAr_coddica.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_coddica.Properties.MaxLength = 65536
    Me.edAr_coddica.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_coddica.Size = New System.Drawing.Size(88, 20)
    Me.edAr_coddica.TabIndex = 664
    '
    'lbAr_codtcdc
    '
    Me.lbAr_codtcdc.AutoSize = True
    Me.lbAr_codtcdc.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codtcdc.Location = New System.Drawing.Point(6, 24)
    Me.lbAr_codtcdc.Name = "lbAr_codtcdc"
    Me.lbAr_codtcdc.NTSDbField = ""
    Me.lbAr_codtcdc.Size = New System.Drawing.Size(80, 13)
    Me.lbAr_codtcdc.TabIndex = 662
    Me.lbAr_codtcdc.Text = "Tipologia entità"
    Me.lbAr_codtcdc.Tooltip = ""
    Me.lbAr_codtcdc.UseMnemonic = False
    '
    'edAr_codtcdc
    '
    Me.edAr_codtcdc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codtcdc.EditValue = "0"
    Me.edAr_codtcdc.Location = New System.Drawing.Point(142, 21)
    Me.edAr_codtcdc.Name = "edAr_codtcdc"
    Me.edAr_codtcdc.NTSDbField = ""
    Me.edAr_codtcdc.NTSFormat = "0"
    Me.edAr_codtcdc.NTSForzaVisZoom = False
    Me.edAr_codtcdc.NTSOldValue = ""
    Me.edAr_codtcdc.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_codtcdc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_codtcdc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codtcdc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codtcdc.Properties.AutoHeight = False
    Me.edAr_codtcdc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codtcdc.Properties.MaxLength = 65536
    Me.edAr_codtcdc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codtcdc.Size = New System.Drawing.Size(53, 20)
    Me.edAr_codtcdc.TabIndex = 663
    '
    'lbXx_codtcdc
    '
    Me.lbXx_codtcdc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codtcdc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codtcdc.Location = New System.Drawing.Point(197, 21)
    Me.lbXx_codtcdc.Name = "lbXx_codtcdc"
    Me.lbXx_codtcdc.NTSDbField = ""
    Me.lbXx_codtcdc.Size = New System.Drawing.Size(90, 20)
    Me.lbXx_codtcdc.TabIndex = 661
    Me.lbXx_codtcdc.Tooltip = ""
    Me.lbXx_codtcdc.UseMnemonic = False
    '
    'lbXx_coddicv
    '
    Me.lbXx_coddicv.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_coddicv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_coddicv.Location = New System.Drawing.Point(197, 64)
    Me.lbXx_coddicv.Name = "lbXx_coddicv"
    Me.lbXx_coddicv.NTSDbField = ""
    Me.lbXx_coddicv.Size = New System.Drawing.Size(90, 20)
    Me.lbXx_coddicv.TabIndex = 660
    Me.lbXx_coddicv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_coddicv.Tooltip = ""
    Me.lbXx_coddicv.UseMnemonic = False
    '
    'lbAr_coddica
    '
    Me.lbAr_coddica.AutoSize = True
    Me.lbAr_coddica.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_coddica.Location = New System.Drawing.Point(6, 46)
    Me.lbAr_coddica.Name = "lbAr_coddica"
    Me.lbAr_coddica.NTSDbField = ""
    Me.lbAr_coddica.Size = New System.Drawing.Size(71, 13)
    Me.lbAr_coddica.TabIndex = 655
    Me.lbAr_coddica.Text = "Aggr. Budget"
    Me.lbAr_coddica.Tooltip = ""
    Me.lbAr_coddica.UseMnemonic = False
    '
    'lbAr_coddicv
    '
    Me.lbAr_coddicv.AutoSize = True
    Me.lbAr_coddicv.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_coddicv.Location = New System.Drawing.Point(6, 68)
    Me.lbAr_coddicv.Name = "lbAr_coddicv"
    Me.lbAr_coddicv.NTSDbField = ""
    Me.lbAr_coddicv.Size = New System.Drawing.Size(100, 13)
    Me.lbAr_coddicv.TabIndex = 658
    Me.lbAr_coddicv.Text = "Valore Agg. Budget"
    Me.lbAr_coddicv.Tooltip = ""
    Me.lbAr_coddicv.UseMnemonic = False
    '
    'lbXx_coddica
    '
    Me.lbXx_coddica.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_coddica.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_coddica.Location = New System.Drawing.Point(197, 43)
    Me.lbXx_coddica.Name = "lbXx_coddica"
    Me.lbXx_coddica.NTSDbField = ""
    Me.lbXx_coddica.Size = New System.Drawing.Size(90, 20)
    Me.lbXx_coddica.TabIndex = 656
    Me.lbXx_coddica.Tooltip = ""
    Me.lbXx_coddica.UseMnemonic = False
    '
    'fmUbiFasi
    '
    Me.fmUbiFasi.AllowDrop = True
    Me.fmUbiFasi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmUbiFasi.Appearance.Options.UseBackColor = True
    Me.fmUbiFasi.Controls.Add(Me.lbAr_ultfase)
    Me.fmUbiFasi.Controls.Add(Me.edAr_ultfase)
    Me.fmUbiFasi.Controls.Add(Me.ckAr_gesubic)
    Me.fmUbiFasi.Controls.Add(Me.ckAr_gesfasi)
    Me.fmUbiFasi.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmUbiFasi.Location = New System.Drawing.Point(539, 11)
    Me.fmUbiFasi.Name = "fmUbiFasi"
    Me.fmUbiFasi.Size = New System.Drawing.Size(213, 76)
    Me.fmUbiFasi.TabIndex = 631
    Me.fmUbiFasi.Text = "GESTIONE UBICAZIONE/FASI"
    '
    'lbAr_ultfase
    '
    Me.lbAr_ultfase.AutoSize = True
    Me.lbAr_ultfase.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_ultfase.Location = New System.Drawing.Point(95, 52)
    Me.lbAr_ultfase.Name = "lbAr_ultfase"
    Me.lbAr_ultfase.NTSDbField = ""
    Me.lbAr_ultfase.Size = New System.Drawing.Size(60, 13)
    Me.lbAr_ultfase.TabIndex = 631
    Me.lbAr_ultfase.Text = "Ultima fase"
    Me.lbAr_ultfase.Tooltip = ""
    Me.lbAr_ultfase.UseMnemonic = False
    '
    'edAr_ultfase
    '
    Me.edAr_ultfase.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ultfase.EditValue = "0"
    Me.edAr_ultfase.Location = New System.Drawing.Point(161, 48)
    Me.edAr_ultfase.Name = "edAr_ultfase"
    Me.edAr_ultfase.NTSDbField = ""
    Me.edAr_ultfase.NTSFormat = "0"
    Me.edAr_ultfase.NTSForzaVisZoom = False
    Me.edAr_ultfase.NTSOldValue = ""
    Me.edAr_ultfase.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_ultfase.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_ultfase.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_ultfase.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_ultfase.Properties.AutoHeight = False
    Me.edAr_ultfase.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_ultfase.Properties.MaxLength = 65536
    Me.edAr_ultfase.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_ultfase.Size = New System.Drawing.Size(44, 20)
    Me.edAr_ultfase.TabIndex = 632
    '
    'ckAr_gesubic
    '
    Me.ckAr_gesubic.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_gesubic.Location = New System.Drawing.Point(5, 27)
    Me.ckAr_gesubic.Name = "ckAr_gesubic"
    Me.ckAr_gesubic.NTSCheckValue = "S"
    Me.ckAr_gesubic.NTSUnCheckValue = "N"
    Me.ckAr_gesubic.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_gesubic.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_gesubic.Properties.AutoHeight = False
    Me.ckAr_gesubic.Properties.Caption = "Gestione ubicazione"
    Me.ckAr_gesubic.Size = New System.Drawing.Size(121, 19)
    Me.ckAr_gesubic.TabIndex = 629
    '
    'ckAr_gesfasi
    '
    Me.ckAr_gesfasi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_gesfasi.Location = New System.Drawing.Point(5, 49)
    Me.ckAr_gesfasi.Name = "ckAr_gesfasi"
    Me.ckAr_gesfasi.NTSCheckValue = "S"
    Me.ckAr_gesfasi.NTSUnCheckValue = "N"
    Me.ckAr_gesfasi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_gesfasi.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_gesfasi.Properties.AutoHeight = False
    Me.ckAr_gesfasi.Properties.Caption = "Gestione fasi"
    Me.ckAr_gesfasi.Size = New System.Drawing.Size(93, 19)
    Me.ckAr_gesfasi.TabIndex = 630
    '
    'lbXx_codimba
    '
    Me.lbXx_codimba.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codimba.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codimba.Location = New System.Drawing.Point(211, 34)
    Me.lbXx_codimba.Name = "lbXx_codimba"
    Me.lbXx_codimba.NTSDbField = ""
    Me.lbXx_codimba.Size = New System.Drawing.Size(318, 20)
    Me.lbXx_codimba.TabIndex = 628
    Me.lbXx_codimba.Tooltip = ""
    Me.lbXx_codimba.UseMnemonic = False
    '
    'lbXx_cartric
    '
    Me.lbXx_cartric.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_cartric.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_cartric.Location = New System.Drawing.Point(351, 196)
    Me.lbXx_cartric.Name = "lbXx_cartric"
    Me.lbXx_cartric.NTSDbField = ""
    Me.lbXx_cartric.Size = New System.Drawing.Size(402, 20)
    Me.lbXx_cartric.TabIndex = 625
    Me.lbXx_cartric.Tooltip = ""
    Me.lbXx_cartric.UseMnemonic = False
    '
    'lbXx_cartcanas
    '
    Me.lbXx_cartcanas.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_cartcanas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_cartcanas.Location = New System.Drawing.Point(351, 219)
    Me.lbXx_cartcanas.Name = "lbXx_cartcanas"
    Me.lbXx_cartcanas.NTSDbField = ""
    Me.lbXx_cartcanas.Size = New System.Drawing.Size(402, 20)
    Me.lbXx_cartcanas.TabIndex = 626
    Me.lbXx_cartcanas.Tooltip = ""
    Me.lbXx_cartcanas.UseMnemonic = False
    '
    'lbXx_cartcanol
    '
    Me.lbXx_cartcanol.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_cartcanol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_cartcanol.Location = New System.Drawing.Point(351, 242)
    Me.lbXx_cartcanol.Name = "lbXx_cartcanol"
    Me.lbXx_cartcanol.NTSDbField = ""
    Me.lbXx_cartcanol.Size = New System.Drawing.Size(402, 20)
    Me.lbXx_cartcanol.TabIndex = 627
    Me.lbXx_cartcanol.Tooltip = ""
    Me.lbXx_cartcanol.UseMnemonic = False
    '
    'lbAr_cartcanol
    '
    Me.lbAr_cartcanol.AutoSize = True
    Me.lbAr_cartcanol.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_cartcanol.Location = New System.Drawing.Point(6, 243)
    Me.lbAr_cartcanol.Name = "lbAr_cartcanol"
    Me.lbAr_cartcanol.NTSDbField = ""
    Me.lbAr_cartcanol.Size = New System.Drawing.Size(124, 13)
    Me.lbAr_cartcanol.TabIndex = 610
    Me.lbAr_cartcanol.Text = "Articolo canone noleggio"
    Me.lbAr_cartcanol.Tooltip = ""
    Me.lbAr_cartcanol.UseMnemonic = False
    '
    'edAr_cartcanol
    '
    Me.edAr_cartcanol.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_cartcanol.EditValue = ""
    Me.edAr_cartcanol.Location = New System.Drawing.Point(139, 241)
    Me.edAr_cartcanol.Name = "edAr_cartcanol"
    Me.edAr_cartcanol.NTSDbField = ""
    Me.edAr_cartcanol.NTSForzaVisZoom = False
    Me.edAr_cartcanol.NTSOldValue = ""
    Me.edAr_cartcanol.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_cartcanol.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_cartcanol.Properties.AutoHeight = False
    Me.edAr_cartcanol.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_cartcanol.Properties.MaxLength = 65536
    Me.edAr_cartcanol.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_cartcanol.Size = New System.Drawing.Size(206, 20)
    Me.edAr_cartcanol.TabIndex = 611
    '
    'lbAr_tipitemcp3
    '
    Me.lbAr_tipitemcp3.AutoSize = True
    Me.lbAr_tipitemcp3.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_tipitemcp3.Location = New System.Drawing.Point(6, 152)
    Me.lbAr_tipitemcp3.Name = "lbAr_tipitemcp3"
    Me.lbAr_tipitemcp3.NTSDbField = ""
    Me.lbAr_tipitemcp3.Size = New System.Drawing.Size(50, 13)
    Me.lbAr_tipitemcp3.TabIndex = 604
    Me.lbAr_tipitemcp3.Text = "Tipo item"
    Me.lbAr_tipitemcp3.Tooltip = ""
    Me.lbAr_tipitemcp3.UseMnemonic = False
    '
    'cbAr_tipitemcp3
    '
    Me.cbAr_tipitemcp3.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_tipitemcp3.DataSource = Nothing
    Me.cbAr_tipitemcp3.DisplayMember = ""
    Me.cbAr_tipitemcp3.Location = New System.Drawing.Point(139, 149)
    Me.cbAr_tipitemcp3.Name = "cbAr_tipitemcp3"
    Me.cbAr_tipitemcp3.NTSDbField = ""
    Me.cbAr_tipitemcp3.Properties.AutoHeight = False
    Me.cbAr_tipitemcp3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_tipitemcp3.Properties.DropDownRows = 30
    Me.cbAr_tipitemcp3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_tipitemcp3.SelectedValue = ""
    Me.cbAr_tipitemcp3.Size = New System.Drawing.Size(312, 20)
    Me.cbAr_tipitemcp3.TabIndex = 607
    Me.cbAr_tipitemcp3.ValueMember = ""
    '
    'lbAr_cartric
    '
    Me.lbAr_cartric.AutoSize = True
    Me.lbAr_cartric.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_cartric.Location = New System.Drawing.Point(6, 198)
    Me.lbAr_cartric.Name = "lbAr_cartric"
    Me.lbAr_cartric.NTSDbField = ""
    Me.lbAr_cartric.Size = New System.Drawing.Size(124, 13)
    Me.lbAr_cartric.TabIndex = 605
    Me.lbAr_cartric.Text = "Articolo vendita ricambio"
    Me.lbAr_cartric.Tooltip = ""
    Me.lbAr_cartric.UseMnemonic = False
    '
    'edAr_cartric
    '
    Me.edAr_cartric.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_cartric.EditValue = ""
    Me.edAr_cartric.Location = New System.Drawing.Point(139, 195)
    Me.edAr_cartric.Name = "edAr_cartric"
    Me.edAr_cartric.NTSDbField = ""
    Me.edAr_cartric.NTSForzaVisZoom = False
    Me.edAr_cartric.NTSOldValue = ""
    Me.edAr_cartric.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_cartric.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_cartric.Properties.AutoHeight = False
    Me.edAr_cartric.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_cartric.Properties.MaxLength = 65536
    Me.edAr_cartric.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_cartric.Size = New System.Drawing.Size(206, 20)
    Me.edAr_cartric.TabIndex = 608
    '
    'lbAr_cartcanas
    '
    Me.lbAr_cartcanas.AutoSize = True
    Me.lbAr_cartcanas.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_cartcanas.Location = New System.Drawing.Point(6, 221)
    Me.lbAr_cartcanas.Name = "lbAr_cartcanas"
    Me.lbAr_cartcanas.NTSDbField = ""
    Me.lbAr_cartcanas.Size = New System.Drawing.Size(124, 13)
    Me.lbAr_cartcanas.TabIndex = 606
    Me.lbAr_cartcanas.Text = "Artic. canone assistenza"
    Me.lbAr_cartcanas.Tooltip = ""
    Me.lbAr_cartcanas.UseMnemonic = False
    '
    'edAr_cartcanas
    '
    Me.edAr_cartcanas.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_cartcanas.EditValue = ""
    Me.edAr_cartcanas.Location = New System.Drawing.Point(139, 218)
    Me.edAr_cartcanas.Name = "edAr_cartcanas"
    Me.edAr_cartcanas.NTSDbField = ""
    Me.edAr_cartcanas.NTSForzaVisZoom = False
    Me.edAr_cartcanas.NTSOldValue = ""
    Me.edAr_cartcanas.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_cartcanas.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_cartcanas.Properties.AutoHeight = False
    Me.edAr_cartcanas.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_cartcanas.Properties.MaxLength = 65536
    Me.edAr_cartcanas.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_cartcanas.Size = New System.Drawing.Size(206, 20)
    Me.edAr_cartcanas.TabIndex = 609
    '
    'lbAr_gestser
    '
    Me.lbAr_gestser.AutoSize = True
    Me.lbAr_gestser.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gestser.Location = New System.Drawing.Point(6, 129)
    Me.lbAr_gestser.Name = "lbAr_gestser"
    Me.lbAr_gestser.NTSDbField = ""
    Me.lbAr_gestser.Size = New System.Drawing.Size(127, 13)
    Me.lbAr_gestser.TabIndex = 602
    Me.lbAr_gestser.Text = "Gest. cod. combin. (CP2)"
    Me.lbAr_gestser.Tooltip = ""
    Me.lbAr_gestser.UseMnemonic = False
    '
    'cbAr_gestser
    '
    Me.cbAr_gestser.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_gestser.DataSource = Nothing
    Me.cbAr_gestser.DisplayMember = ""
    Me.cbAr_gestser.Location = New System.Drawing.Point(139, 126)
    Me.cbAr_gestser.Name = "cbAr_gestser"
    Me.cbAr_gestser.NTSDbField = ""
    Me.cbAr_gestser.Properties.AutoHeight = False
    Me.cbAr_gestser.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_gestser.Properties.DropDownRows = 30
    Me.cbAr_gestser.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_gestser.SelectedValue = ""
    Me.cbAr_gestser.Size = New System.Drawing.Size(312, 20)
    Me.cbAr_gestser.TabIndex = 603
    Me.cbAr_gestser.ValueMember = ""
    '
    'lbAr_makebuy
    '
    Me.lbAr_makebuy.AutoSize = True
    Me.lbAr_makebuy.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_makebuy.Location = New System.Drawing.Point(6, 106)
    Me.lbAr_makebuy.Name = "lbAr_makebuy"
    Me.lbAr_makebuy.NTSDbField = ""
    Me.lbAr_makebuy.Size = New System.Drawing.Size(68, 13)
    Me.lbAr_makebuy.TabIndex = 600
    Me.lbAr_makebuy.Text = "Make-or-Buy"
    Me.lbAr_makebuy.Tooltip = ""
    Me.lbAr_makebuy.UseMnemonic = False
    '
    'cbAr_makebuy
    '
    Me.cbAr_makebuy.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_makebuy.DataSource = Nothing
    Me.cbAr_makebuy.DisplayMember = ""
    Me.cbAr_makebuy.Location = New System.Drawing.Point(139, 103)
    Me.cbAr_makebuy.Name = "cbAr_makebuy"
    Me.cbAr_makebuy.NTSDbField = ""
    Me.cbAr_makebuy.Properties.AutoHeight = False
    Me.cbAr_makebuy.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_makebuy.Properties.DropDownRows = 30
    Me.cbAr_makebuy.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_makebuy.SelectedValue = ""
    Me.cbAr_makebuy.Size = New System.Drawing.Size(206, 20)
    Me.cbAr_makebuy.TabIndex = 601
    Me.cbAr_makebuy.ValueMember = ""
    '
    'lbAr_volume
    '
    Me.lbAr_volume.AutoSize = True
    Me.lbAr_volume.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_volume.Location = New System.Drawing.Point(6, 83)
    Me.lbAr_volume.Name = "lbAr_volume"
    Me.lbAr_volume.NTSDbField = ""
    Me.lbAr_volume.Size = New System.Drawing.Size(41, 13)
    Me.lbAr_volume.TabIndex = 598
    Me.lbAr_volume.Text = "Volume"
    Me.lbAr_volume.Tooltip = ""
    Me.lbAr_volume.UseMnemonic = False
    '
    'edAr_volume
    '
    Me.edAr_volume.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_volume.EditValue = "0"
    Me.edAr_volume.Location = New System.Drawing.Point(139, 80)
    Me.edAr_volume.Name = "edAr_volume"
    Me.edAr_volume.NTSDbField = ""
    Me.edAr_volume.NTSFormat = "0"
    Me.edAr_volume.NTSForzaVisZoom = False
    Me.edAr_volume.NTSOldValue = ""
    Me.edAr_volume.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_volume.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_volume.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_volume.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_volume.Properties.AutoHeight = False
    Me.edAr_volume.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_volume.Properties.MaxLength = 65536
    Me.edAr_volume.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_volume.Size = New System.Drawing.Size(100, 20)
    Me.edAr_volume.TabIndex = 599
    '
    'lbAr_codimba
    '
    Me.lbAr_codimba.AutoSize = True
    Me.lbAr_codimba.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codimba.Location = New System.Drawing.Point(6, 38)
    Me.lbAr_codimba.Name = "lbAr_codimba"
    Me.lbAr_codimba.NTSDbField = ""
    Me.lbAr_codimba.Size = New System.Drawing.Size(41, 13)
    Me.lbAr_codimba.TabIndex = 590
    Me.lbAr_codimba.Text = "Imballo"
    Me.lbAr_codimba.Tooltip = ""
    Me.lbAr_codimba.UseMnemonic = False
    '
    'edAr_codimba
    '
    Me.edAr_codimba.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codimba.EditValue = "0"
    Me.edAr_codimba.Location = New System.Drawing.Point(139, 34)
    Me.edAr_codimba.Name = "edAr_codimba"
    Me.edAr_codimba.NTSDbField = ""
    Me.edAr_codimba.NTSFormat = "0"
    Me.edAr_codimba.NTSForzaVisZoom = False
    Me.edAr_codimba.NTSOldValue = ""
    Me.edAr_codimba.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_codimba.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_codimba.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codimba.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codimba.Properties.AutoHeight = False
    Me.edAr_codimba.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codimba.Properties.MaxLength = 65536
    Me.edAr_codimba.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codimba.Size = New System.Drawing.Size(66, 20)
    Me.edAr_codimba.TabIndex = 594
    '
    'lbAr_misura1
    '
    Me.lbAr_misura1.AutoSize = True
    Me.lbAr_misura1.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_misura1.Location = New System.Drawing.Point(6, 60)
    Me.lbAr_misura1.Name = "lbAr_misura1"
    Me.lbAr_misura1.NTSDbField = ""
    Me.lbAr_misura1.Size = New System.Drawing.Size(67, 13)
    Me.lbAr_misura1.TabIndex = 591
    Me.lbAr_misura1.Text = "Misure 1/2/3"
    Me.lbAr_misura1.Tooltip = ""
    Me.lbAr_misura1.UseMnemonic = False
    '
    'edAr_misura1
    '
    Me.edAr_misura1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_misura1.EditValue = "0"
    Me.edAr_misura1.Location = New System.Drawing.Point(139, 57)
    Me.edAr_misura1.Name = "edAr_misura1"
    Me.edAr_misura1.NTSDbField = ""
    Me.edAr_misura1.NTSFormat = "0"
    Me.edAr_misura1.NTSForzaVisZoom = False
    Me.edAr_misura1.NTSOldValue = ""
    Me.edAr_misura1.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_misura1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_misura1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_misura1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_misura1.Properties.AutoHeight = False
    Me.edAr_misura1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_misura1.Properties.MaxLength = 65536
    Me.edAr_misura1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_misura1.Size = New System.Drawing.Size(100, 20)
    Me.edAr_misura1.TabIndex = 595
    '
    'edAr_misura2
    '
    Me.edAr_misura2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_misura2.EditValue = "0"
    Me.edAr_misura2.Location = New System.Drawing.Point(245, 57)
    Me.edAr_misura2.Name = "edAr_misura2"
    Me.edAr_misura2.NTSDbField = ""
    Me.edAr_misura2.NTSFormat = "0"
    Me.edAr_misura2.NTSForzaVisZoom = False
    Me.edAr_misura2.NTSOldValue = ""
    Me.edAr_misura2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_misura2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_misura2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_misura2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_misura2.Properties.AutoHeight = False
    Me.edAr_misura2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_misura2.Properties.MaxLength = 65536
    Me.edAr_misura2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_misura2.Size = New System.Drawing.Size(100, 20)
    Me.edAr_misura2.TabIndex = 596
    '
    'edAr_misura3
    '
    Me.edAr_misura3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_misura3.EditValue = "0"
    Me.edAr_misura3.Location = New System.Drawing.Point(351, 57)
    Me.edAr_misura3.Name = "edAr_misura3"
    Me.edAr_misura3.NTSDbField = ""
    Me.edAr_misura3.NTSFormat = "0"
    Me.edAr_misura3.NTSForzaVisZoom = False
    Me.edAr_misura3.NTSOldValue = ""
    Me.edAr_misura3.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_misura3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_misura3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_misura3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_misura3.Properties.AutoHeight = False
    Me.edAr_misura3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_misura3.Properties.MaxLength = 65536
    Me.edAr_misura3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_misura3.Size = New System.Drawing.Size(100, 20)
    Me.edAr_misura3.TabIndex = 597
    '
    'lbAr_flmod
    '
    Me.lbAr_flmod.AutoSize = True
    Me.lbAr_flmod.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_flmod.Location = New System.Drawing.Point(6, 14)
    Me.lbAr_flmod.Name = "lbAr_flmod"
    Me.lbAr_flmod.NTSDbField = ""
    Me.lbAr_flmod.Size = New System.Drawing.Size(68, 13)
    Me.lbAr_flmod.TabIndex = 586
    Me.lbAr_flmod.Text = "Modificabilità"
    Me.lbAr_flmod.Tooltip = ""
    Me.lbAr_flmod.UseMnemonic = False
    '
    'edAr_flmod
    '
    Me.edAr_flmod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_flmod.EditValue = ""
    Me.edAr_flmod.Location = New System.Drawing.Point(139, 11)
    Me.edAr_flmod.Name = "edAr_flmod"
    Me.edAr_flmod.NTSDbField = ""
    Me.edAr_flmod.NTSForzaVisZoom = False
    Me.edAr_flmod.NTSOldValue = ""
    Me.edAr_flmod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_flmod.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_flmod.Properties.AutoHeight = False
    Me.edAr_flmod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_flmod.Properties.MaxLength = 65536
    Me.edAr_flmod.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_flmod.Size = New System.Drawing.Size(390, 20)
    Me.edAr_flmod.TabIndex = 587
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnTabpag1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(755, 294)
    Me.NtsTabPage1.Text = "&1 - Vendite"
    '
    'pnTabpag1
    '
    Me.pnTabpag1.AllowDrop = True
    Me.pnTabpag1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag1.Appearance.Options.UseBackColor = True
    Me.pnTabpag1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag1.Controls.Add(Me.pnTabpag1Right)
    Me.pnTabpag1.Controls.Add(Me.pnTabpag1Left)
    Me.pnTabpag1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag1.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag1.Name = "pnTabpag1"
    Me.pnTabpag1.NTSActiveTrasparency = True
    Me.pnTabpag1.Size = New System.Drawing.Size(755, 294)
    Me.pnTabpag1.TabIndex = 611
    Me.pnTabpag1.Text = "NtsPanel1"
    '
    'pnTabpag1Right
    '
    Me.pnTabpag1Right.AllowDrop = True
    Me.pnTabpag1Right.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag1Right.Appearance.Options.UseBackColor = True
    Me.pnTabpag1Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag1Right.Controls.Add(Me.fmEcommerce)
    Me.pnTabpag1Right.Controls.Add(Me.lbXx_numecr)
    Me.pnTabpag1Right.Controls.Add(Me.fmIntrastat)
    Me.pnTabpag1Right.Controls.Add(Me.lbAr_sostit)
    Me.pnTabpag1Right.Controls.Add(Me.edAr_sostit)
    Me.pnTabpag1Right.Controls.Add(Me.lbAr_sostituito)
    Me.pnTabpag1Right.Controls.Add(Me.edAr_sostituito)
    Me.pnTabpag1Right.Controls.Add(Me.lbAr_numecr)
    Me.pnTabpag1Right.Controls.Add(Me.edAr_numecr)
    Me.pnTabpag1Right.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag1Right.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag1Right.Location = New System.Drawing.Point(429, 0)
    Me.pnTabpag1Right.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag1Right.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag1Right.Name = "pnTabpag1Right"
    Me.pnTabpag1Right.NTSActiveTrasparency = True
    Me.pnTabpag1Right.Size = New System.Drawing.Size(326, 294)
    Me.pnTabpag1Right.TabIndex = 644
    Me.pnTabpag1Right.Text = "NtsPanel1"
    '
    'fmEcommerce
    '
    Me.fmEcommerce.AllowDrop = True
    Me.fmEcommerce.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmEcommerce.Appearance.Options.UseBackColor = True
    Me.fmEcommerce.Controls.Add(Me.lbXx_codseat)
    Me.fmEcommerce.Controls.Add(Me.ckAr_webvend)
    Me.fmEcommerce.Controls.Add(Me.ckAr_webusat)
    Me.fmEcommerce.Controls.Add(Me.ckAr_webvis)
    Me.fmEcommerce.Controls.Add(Me.lbAr_codseat)
    Me.fmEcommerce.Controls.Add(Me.edAr_codseat)
    Me.fmEcommerce.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmEcommerce.Location = New System.Drawing.Point(7, 185)
    Me.fmEcommerce.Name = "fmEcommerce"
    Me.fmEcommerce.Size = New System.Drawing.Size(319, 102)
    Me.fmEcommerce.TabIndex = 650
    Me.fmEcommerce.Text = "e-Commerce"
    '
    'lbXx_codseat
    '
    Me.lbXx_codseat.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codseat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codseat.Location = New System.Drawing.Point(128, 78)
    Me.lbXx_codseat.Name = "lbXx_codseat"
    Me.lbXx_codseat.NTSDbField = ""
    Me.lbXx_codseat.Size = New System.Drawing.Size(189, 20)
    Me.lbXx_codseat.TabIndex = 670
    Me.lbXx_codseat.Tooltip = ""
    Me.lbXx_codseat.UseMnemonic = False
    '
    'ckAr_webvend
    '
    Me.ckAr_webvend.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_webvend.Location = New System.Drawing.Point(8, 38)
    Me.ckAr_webvend.Name = "ckAr_webvend"
    Me.ckAr_webvend.NTSCheckValue = "S"
    Me.ckAr_webvend.NTSUnCheckValue = "N"
    Me.ckAr_webvend.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_webvend.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_webvend.Properties.AutoHeight = False
    Me.ckAr_webvend.Properties.Caption = "Articolo vendibile da applicazione esterna"
    Me.ckAr_webvend.Size = New System.Drawing.Size(220, 19)
    Me.ckAr_webvend.TabIndex = 672
    '
    'ckAr_webusat
    '
    Me.ckAr_webusat.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_webusat.Location = New System.Drawing.Point(8, 56)
    Me.ckAr_webusat.Name = "ckAr_webusat"
    Me.ckAr_webusat.NTSCheckValue = "S"
    Me.ckAr_webusat.NTSUnCheckValue = "N"
    Me.ckAr_webusat.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_webusat.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_webusat.Properties.AutoHeight = False
    Me.ckAr_webusat.Properties.Caption = "Articolo usato"
    Me.ckAr_webusat.Size = New System.Drawing.Size(96, 19)
    Me.ckAr_webusat.TabIndex = 671
    '
    'ckAr_webvis
    '
    Me.ckAr_webvis.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_webvis.Location = New System.Drawing.Point(8, 20)
    Me.ckAr_webvis.Name = "ckAr_webvis"
    Me.ckAr_webvis.NTSCheckValue = "S"
    Me.ckAr_webvis.NTSUnCheckValue = "N"
    Me.ckAr_webvis.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_webvis.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_webvis.Properties.AutoHeight = False
    Me.ckAr_webvis.Properties.Caption = "Articolo visibile dall'applicazione esterna"
    Me.ckAr_webvis.Size = New System.Drawing.Size(220, 19)
    Me.ckAr_webvis.TabIndex = 667
    '
    'lbAr_codseat
    '
    Me.lbAr_codseat.AutoSize = True
    Me.lbAr_codseat.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codseat.Location = New System.Drawing.Point(5, 81)
    Me.lbAr_codseat.Name = "lbAr_codseat"
    Me.lbAr_codseat.NTSDbField = ""
    Me.lbAr_codseat.Size = New System.Drawing.Size(64, 13)
    Me.lbAr_codseat.TabIndex = 668
    Me.lbAr_codseat.Text = "Set attributi"
    Me.lbAr_codseat.Tooltip = ""
    Me.lbAr_codseat.UseMnemonic = False
    '
    'edAr_codseat
    '
    Me.edAr_codseat.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAr_codseat.EditValue = "0"
    Me.edAr_codseat.Location = New System.Drawing.Point(73, 78)
    Me.edAr_codseat.Name = "edAr_codseat"
    Me.edAr_codseat.NTSDbField = ""
    Me.edAr_codseat.NTSFormat = "0"
    Me.edAr_codseat.NTSForzaVisZoom = False
    Me.edAr_codseat.NTSOldValue = ""
    Me.edAr_codseat.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_codseat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_codseat.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codseat.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codseat.Properties.AutoHeight = False
    Me.edAr_codseat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codseat.Properties.MaxLength = 65536
    Me.edAr_codseat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codseat.Size = New System.Drawing.Size(50, 20)
    Me.edAr_codseat.TabIndex = 669
    '
    'lbXx_numecr
    '
    Me.lbXx_numecr.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_numecr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_numecr.Location = New System.Drawing.Point(173, 50)
    Me.lbXx_numecr.Name = "lbXx_numecr"
    Me.lbXx_numecr.NTSDbField = ""
    Me.lbXx_numecr.Size = New System.Drawing.Size(142, 20)
    Me.lbXx_numecr.TabIndex = 649
    Me.lbXx_numecr.Tooltip = ""
    Me.lbXx_numecr.UseMnemonic = False
    '
    'fmIntrastat
    '
    Me.fmIntrastat.AllowDrop = True
    Me.fmIntrastat.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmIntrastat.Appearance.Options.UseBackColor = True
    Me.fmIntrastat.Controls.Add(Me.lbAr_umintra2)
    Me.fmIntrastat.Controls.Add(Me.cbAr_umintra2)
    Me.fmIntrastat.Controls.Add(Me.lbAr_codnomc)
    Me.fmIntrastat.Controls.Add(Me.edAr_codnomc)
    Me.fmIntrastat.Controls.Add(Me.lbAr_percvst)
    Me.fmIntrastat.Controls.Add(Me.edAr_percvst)
    Me.fmIntrastat.Controls.Add(Me.lbAr_prorig)
    Me.fmIntrastat.Controls.Add(Me.edAr_prorig)
    Me.fmIntrastat.Controls.Add(Me.edAr_paeorig)
    Me.fmIntrastat.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmIntrastat.Location = New System.Drawing.Point(6, 72)
    Me.fmIntrastat.Name = "fmIntrastat"
    Me.fmIntrastat.Size = New System.Drawing.Size(318, 110)
    Me.fmIntrastat.TabIndex = 648
    Me.fmIntrastat.Text = "INTRASTAT"
    '
    'lbAr_umintra2
    '
    Me.lbAr_umintra2.AutoSize = True
    Me.lbAr_umintra2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_umintra2.Location = New System.Drawing.Point(8, 89)
    Me.lbAr_umintra2.Name = "lbAr_umintra2"
    Me.lbAr_umintra2.NTSDbField = ""
    Me.lbAr_umintra2.Size = New System.Drawing.Size(132, 13)
    Me.lbAr_umintra2.TabIndex = 568
    Me.lbAr_umintra2.Text = "Unità di misura secondaria"
    Me.lbAr_umintra2.Tooltip = ""
    Me.lbAr_umintra2.UseMnemonic = False
    '
    'cbAr_umintra2
    '
    Me.cbAr_umintra2.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_umintra2.DataSource = Nothing
    Me.cbAr_umintra2.DisplayMember = ""
    Me.cbAr_umintra2.Location = New System.Drawing.Point(167, 86)
    Me.cbAr_umintra2.Name = "cbAr_umintra2"
    Me.cbAr_umintra2.NTSDbField = ""
    Me.cbAr_umintra2.Properties.AutoHeight = False
    Me.cbAr_umintra2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_umintra2.Properties.DropDownRows = 30
    Me.cbAr_umintra2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_umintra2.SelectedValue = ""
    Me.cbAr_umintra2.Size = New System.Drawing.Size(142, 20)
    Me.cbAr_umintra2.TabIndex = 569
    Me.cbAr_umintra2.ValueMember = ""
    '
    'lbAr_codnomc
    '
    Me.lbAr_codnomc.AutoSize = True
    Me.lbAr_codnomc.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codnomc.Location = New System.Drawing.Point(8, 67)
    Me.lbAr_codnomc.Name = "lbAr_codnomc"
    Me.lbAr_codnomc.NTSDbField = ""
    Me.lbAr_codnomc.Size = New System.Drawing.Size(150, 13)
    Me.lbAr_codnomc.TabIndex = 550
    Me.lbAr_codnomc.Text = "Cod. nomenclatura combinata"
    Me.lbAr_codnomc.Tooltip = ""
    Me.lbAr_codnomc.UseMnemonic = False
    '
    'edAr_codnomc
    '
    Me.edAr_codnomc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codnomc.EditValue = ""
    Me.edAr_codnomc.Location = New System.Drawing.Point(167, 64)
    Me.edAr_codnomc.Name = "edAr_codnomc"
    Me.edAr_codnomc.NTSDbField = ""
    Me.edAr_codnomc.NTSForzaVisZoom = False
    Me.edAr_codnomc.NTSOldValue = ""
    Me.edAr_codnomc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codnomc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codnomc.Properties.AutoHeight = False
    Me.edAr_codnomc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codnomc.Properties.MaxLength = 65536
    Me.edAr_codnomc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codnomc.Size = New System.Drawing.Size(142, 20)
    Me.edAr_codnomc.TabIndex = 551
    '
    'lbAr_percvst
    '
    Me.lbAr_percvst.AutoSize = True
    Me.lbAr_percvst.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_percvst.Location = New System.Drawing.Point(8, 46)
    Me.lbAr_percvst.Name = "lbAr_percvst"
    Me.lbAr_percvst.NTSDbField = ""
    Me.lbAr_percvst.Size = New System.Drawing.Size(97, 13)
    Me.lbAr_percvst.TabIndex = 548
    Me.lbAr_percvst.Text = "% Valore statistico"
    Me.lbAr_percvst.Tooltip = ""
    Me.lbAr_percvst.UseMnemonic = False
    '
    'edAr_percvst
    '
    Me.edAr_percvst.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_percvst.EditValue = "0"
    Me.edAr_percvst.Location = New System.Drawing.Point(167, 42)
    Me.edAr_percvst.Name = "edAr_percvst"
    Me.edAr_percvst.NTSDbField = ""
    Me.edAr_percvst.NTSFormat = "0"
    Me.edAr_percvst.NTSForzaVisZoom = False
    Me.edAr_percvst.NTSOldValue = ""
    Me.edAr_percvst.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_percvst.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_percvst.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_percvst.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_percvst.Properties.AutoHeight = False
    Me.edAr_percvst.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_percvst.Properties.MaxLength = 65536
    Me.edAr_percvst.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_percvst.Size = New System.Drawing.Size(142, 20)
    Me.edAr_percvst.TabIndex = 549
    '
    'lbAr_prorig
    '
    Me.lbAr_prorig.AutoSize = True
    Me.lbAr_prorig.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_prorig.Location = New System.Drawing.Point(5, 23)
    Me.lbAr_prorig.Name = "lbAr_prorig"
    Me.lbAr_prorig.NTSDbField = ""
    Me.lbAr_prorig.Size = New System.Drawing.Size(132, 13)
    Me.lbAr_prorig.TabIndex = 546
    Me.lbAr_prorig.Text = " Provincia/Paese di origine"
    Me.lbAr_prorig.Tooltip = ""
    Me.lbAr_prorig.UseMnemonic = False
    '
    'edAr_prorig
    '
    Me.edAr_prorig.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_prorig.EditValue = ""
    Me.edAr_prorig.Location = New System.Drawing.Point(167, 20)
    Me.edAr_prorig.Name = "edAr_prorig"
    Me.edAr_prorig.NTSDbField = ""
    Me.edAr_prorig.NTSForzaVisZoom = False
    Me.edAr_prorig.NTSOldValue = ""
    Me.edAr_prorig.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_prorig.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_prorig.Properties.AutoHeight = False
    Me.edAr_prorig.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_prorig.Properties.MaxLength = 65536
    Me.edAr_prorig.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_prorig.Size = New System.Drawing.Size(50, 20)
    Me.edAr_prorig.TabIndex = 547
    '
    'edAr_paeorig
    '
    Me.edAr_paeorig.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_paeorig.EditValue = ""
    Me.edAr_paeorig.Location = New System.Drawing.Point(245, 20)
    Me.edAr_paeorig.Name = "edAr_paeorig"
    Me.edAr_paeorig.NTSDbField = ""
    Me.edAr_paeorig.NTSForzaVisZoom = False
    Me.edAr_paeorig.NTSOldValue = ""
    Me.edAr_paeorig.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_paeorig.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_paeorig.Properties.AutoHeight = False
    Me.edAr_paeorig.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_paeorig.Properties.MaxLength = 65536
    Me.edAr_paeorig.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_paeorig.Size = New System.Drawing.Size(64, 20)
    Me.edAr_paeorig.TabIndex = 545
    '
    'lbAr_sostit
    '
    Me.lbAr_sostit.AutoSize = True
    Me.lbAr_sostit.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_sostit.Location = New System.Drawing.Point(4, 8)
    Me.lbAr_sostit.Name = "lbAr_sostit"
    Me.lbAr_sostit.NTSDbField = ""
    Me.lbAr_sostit.Size = New System.Drawing.Size(96, 13)
    Me.lbAr_sostit.TabIndex = 646
    Me.lbAr_sostit.Text = "Articolo sostitutivo"
    Me.lbAr_sostit.Tooltip = ""
    Me.lbAr_sostit.UseMnemonic = False
    '
    'edAr_sostit
    '
    Me.edAr_sostit.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_sostit.EditValue = ""
    Me.edAr_sostit.Location = New System.Drawing.Point(110, 5)
    Me.edAr_sostit.Name = "edAr_sostit"
    Me.edAr_sostit.NTSDbField = ""
    Me.edAr_sostit.NTSForzaVisZoom = False
    Me.edAr_sostit.NTSOldValue = ""
    Me.edAr_sostit.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_sostit.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_sostit.Properties.AutoHeight = False
    Me.edAr_sostit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_sostit.Properties.MaxLength = 65536
    Me.edAr_sostit.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_sostit.Size = New System.Drawing.Size(205, 20)
    Me.edAr_sostit.TabIndex = 647
    '
    'lbAr_sostituito
    '
    Me.lbAr_sostituito.AutoSize = True
    Me.lbAr_sostituito.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_sostituito.Location = New System.Drawing.Point(4, 30)
    Me.lbAr_sostituito.Name = "lbAr_sostituito"
    Me.lbAr_sostituito.NTSDbField = ""
    Me.lbAr_sostituito.Size = New System.Drawing.Size(90, 13)
    Me.lbAr_sostituito.TabIndex = 644
    Me.lbAr_sostituito.Text = "Articolo sostituito"
    Me.lbAr_sostituito.Tooltip = ""
    Me.lbAr_sostituito.UseMnemonic = False
    '
    'edAr_sostituito
    '
    Me.edAr_sostituito.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_sostituito.EditValue = ""
    Me.edAr_sostituito.Location = New System.Drawing.Point(110, 27)
    Me.edAr_sostituito.Name = "edAr_sostituito"
    Me.edAr_sostituito.NTSDbField = ""
    Me.edAr_sostituito.NTSForzaVisZoom = False
    Me.edAr_sostituito.NTSOldValue = ""
    Me.edAr_sostituito.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_sostituito.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_sostituito.Properties.AutoHeight = False
    Me.edAr_sostituito.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_sostituito.Properties.MaxLength = 65536
    Me.edAr_sostituito.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_sostituito.Size = New System.Drawing.Size(205, 20)
    Me.edAr_sostituito.TabIndex = 645
    '
    'lbAr_numecr
    '
    Me.lbAr_numecr.AutoSize = True
    Me.lbAr_numecr.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_numecr.Location = New System.Drawing.Point(4, 53)
    Me.lbAr_numecr.Name = "lbAr_numecr"
    Me.lbAr_numecr.NTSDbField = ""
    Me.lbAr_numecr.Size = New System.Drawing.Size(76, 13)
    Me.lbAr_numecr.TabIndex = 642
    Me.lbAr_numecr.Text = "Centro di C.A."
    Me.lbAr_numecr.Tooltip = ""
    Me.lbAr_numecr.UseMnemonic = False
    '
    'edAr_numecr
    '
    Me.edAr_numecr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_numecr.EditValue = "0"
    Me.edAr_numecr.Location = New System.Drawing.Point(110, 50)
    Me.edAr_numecr.Name = "edAr_numecr"
    Me.edAr_numecr.NTSDbField = ""
    Me.edAr_numecr.NTSFormat = "0"
    Me.edAr_numecr.NTSForzaVisZoom = False
    Me.edAr_numecr.NTSOldValue = ""
    Me.edAr_numecr.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_numecr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_numecr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_numecr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_numecr.Properties.AutoHeight = False
    Me.edAr_numecr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_numecr.Properties.MaxLength = 65536
    Me.edAr_numecr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_numecr.Size = New System.Drawing.Size(57, 20)
    Me.edAr_numecr.TabIndex = 643
    '
    'pnTabpag1Left
    '
    Me.pnTabpag1Left.AllowDrop = True
    Me.pnTabpag1Left.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag1Left.Appearance.Options.UseBackColor = True
    Me.pnTabpag1Left.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag1Left.Controls.Add(Me.lbXx_reparto)
    Me.pnTabpag1Left.Controls.Add(Me.lbXx_codpdon)
    Me.pnTabpag1Left.Controls.Add(Me.lbXx_codappr)
    Me.pnTabpag1Left.Controls.Add(Me.lbXx_codmarc)
    Me.pnTabpag1Left.Controls.Add(Me.lbAr_gescon)
    Me.pnTabpag1Left.Controls.Add(Me.cbAr_gescon)
    Me.pnTabpag1Left.Controls.Add(Me.lbAr_codappr)
    Me.pnTabpag1Left.Controls.Add(Me.edAr_codappr)
    Me.pnTabpag1Left.Controls.Add(Me.lbAr_tipokit)
    Me.pnTabpag1Left.Controls.Add(Me.cbAr_tipokit)
    Me.pnTabpag1Left.Controls.Add(Me.lbAr_flricmar)
    Me.pnTabpag1Left.Controls.Add(Me.cbAr_flricmar)
    Me.pnTabpag1Left.Controls.Add(Me.lbAr_codpdon)
    Me.pnTabpag1Left.Controls.Add(Me.edAr_codpdon)
    Me.pnTabpag1Left.Controls.Add(Me.edAr_ricar1)
    Me.pnTabpag1Left.Controls.Add(Me.edAr_ricar2)
    Me.pnTabpag1Left.Controls.Add(Me.lbAr_reparto)
    Me.pnTabpag1Left.Controls.Add(Me.edAr_reparto)
    Me.pnTabpag1Left.Controls.Add(Me.edAr_garacq)
    Me.pnTabpag1Left.Controls.Add(Me.lbAr_garven)
    Me.pnTabpag1Left.Controls.Add(Me.edAr_garven)
    Me.pnTabpag1Left.Controls.Add(Me.lbAr_perqta)
    Me.pnTabpag1Left.Controls.Add(Me.edAr_perqta)
    Me.pnTabpag1Left.Controls.Add(Me.lbAr_codmarc)
    Me.pnTabpag1Left.Controls.Add(Me.edAr_codmarc)
    Me.pnTabpag1Left.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag1Left.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnTabpag1Left.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag1Left.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag1Left.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag1Left.Name = "pnTabpag1Left"
    Me.pnTabpag1Left.NTSActiveTrasparency = True
    Me.pnTabpag1Left.Size = New System.Drawing.Size(429, 294)
    Me.pnTabpag1Left.TabIndex = 643
    Me.pnTabpag1Left.Text = "NtsPanel1"
    '
    'lbXx_reparto
    '
    Me.lbXx_reparto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_reparto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_reparto.Location = New System.Drawing.Point(208, 184)
    Me.lbXx_reparto.Name = "lbXx_reparto"
    Me.lbXx_reparto.NTSDbField = ""
    Me.lbXx_reparto.Size = New System.Drawing.Size(215, 20)
    Me.lbXx_reparto.TabIndex = 666
    Me.lbXx_reparto.Tooltip = ""
    Me.lbXx_reparto.UseMnemonic = False
    '
    'lbXx_codpdon
    '
    Me.lbXx_codpdon.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codpdon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codpdon.Location = New System.Drawing.Point(208, 72)
    Me.lbXx_codpdon.Name = "lbXx_codpdon"
    Me.lbXx_codpdon.NTSDbField = ""
    Me.lbXx_codpdon.Size = New System.Drawing.Size(215, 20)
    Me.lbXx_codpdon.TabIndex = 666
    Me.lbXx_codpdon.Tooltip = ""
    Me.lbXx_codpdon.UseMnemonic = False
    '
    'lbXx_codappr
    '
    Me.lbXx_codappr.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codappr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codappr.Location = New System.Drawing.Point(208, 117)
    Me.lbXx_codappr.Name = "lbXx_codappr"
    Me.lbXx_codappr.NTSDbField = ""
    Me.lbXx_codappr.Size = New System.Drawing.Size(215, 20)
    Me.lbXx_codappr.TabIndex = 665
    Me.lbXx_codappr.Tooltip = ""
    Me.lbXx_codappr.UseMnemonic = False
    '
    'lbXx_codmarc
    '
    Me.lbXx_codmarc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codmarc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codmarc.Location = New System.Drawing.Point(208, 5)
    Me.lbXx_codmarc.Name = "lbXx_codmarc"
    Me.lbXx_codmarc.NTSDbField = ""
    Me.lbXx_codmarc.Size = New System.Drawing.Size(215, 20)
    Me.lbXx_codmarc.TabIndex = 664
    Me.lbXx_codmarc.Tooltip = ""
    Me.lbXx_codmarc.UseMnemonic = False
    '
    'lbAr_gescon
    '
    Me.lbAr_gescon.AutoSize = True
    Me.lbAr_gescon.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gescon.Location = New System.Drawing.Point(9, 165)
    Me.lbAr_gescon.Name = "lbAr_gescon"
    Me.lbAr_gescon.NTSDbField = ""
    Me.lbAr_gescon.Size = New System.Drawing.Size(77, 13)
    Me.lbAr_gescon.TabIndex = 662
    Me.lbAr_gescon.Text = "Applica CONAI"
    Me.lbAr_gescon.Tooltip = ""
    Me.lbAr_gescon.UseMnemonic = False
    '
    'cbAr_gescon
    '
    Me.cbAr_gescon.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_gescon.DataSource = Nothing
    Me.cbAr_gescon.DisplayMember = ""
    Me.cbAr_gescon.Location = New System.Drawing.Point(152, 162)
    Me.cbAr_gescon.Name = "cbAr_gescon"
    Me.cbAr_gescon.NTSDbField = ""
    Me.cbAr_gescon.Properties.AutoHeight = False
    Me.cbAr_gescon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_gescon.Properties.DropDownRows = 30
    Me.cbAr_gescon.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_gescon.SelectedValue = ""
    Me.cbAr_gescon.Size = New System.Drawing.Size(271, 20)
    Me.cbAr_gescon.TabIndex = 663
    Me.cbAr_gescon.ValueMember = ""
    '
    'lbAr_codappr
    '
    Me.lbAr_codappr.AutoSize = True
    Me.lbAr_codappr.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codappr.Location = New System.Drawing.Point(9, 120)
    Me.lbAr_codappr.Name = "lbAr_codappr"
    Me.lbAr_codappr.NTSDbField = ""
    Me.lbAr_codappr.Size = New System.Drawing.Size(96, 13)
    Me.lbAr_codappr.TabIndex = 658
    Me.lbAr_codappr.Text = "Approvvigionatore"
    Me.lbAr_codappr.Tooltip = ""
    Me.lbAr_codappr.UseMnemonic = False
    '
    'edAr_codappr
    '
    Me.edAr_codappr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codappr.EditValue = "0"
    Me.edAr_codappr.Location = New System.Drawing.Point(152, 117)
    Me.edAr_codappr.Name = "edAr_codappr"
    Me.edAr_codappr.NTSDbField = ""
    Me.edAr_codappr.NTSFormat = "0"
    Me.edAr_codappr.NTSForzaVisZoom = False
    Me.edAr_codappr.NTSOldValue = ""
    Me.edAr_codappr.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_codappr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_codappr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codappr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codappr.Properties.AutoHeight = False
    Me.edAr_codappr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codappr.Properties.MaxLength = 65536
    Me.edAr_codappr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codappr.Size = New System.Drawing.Size(50, 20)
    Me.edAr_codappr.TabIndex = 660
    '
    'lbAr_tipokit
    '
    Me.lbAr_tipokit.AutoSize = True
    Me.lbAr_tipokit.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_tipokit.Location = New System.Drawing.Point(9, 143)
    Me.lbAr_tipokit.Name = "lbAr_tipokit"
    Me.lbAr_tipokit.NTSDbField = ""
    Me.lbAr_tipokit.Size = New System.Drawing.Size(46, 13)
    Me.lbAr_tipokit.TabIndex = 659
    Me.lbAr_tipokit.Text = "Tipo KIT"
    Me.lbAr_tipokit.Tooltip = ""
    Me.lbAr_tipokit.UseMnemonic = False
    '
    'cbAr_tipokit
    '
    Me.cbAr_tipokit.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_tipokit.DataSource = Nothing
    Me.cbAr_tipokit.DisplayMember = ""
    Me.cbAr_tipokit.Location = New System.Drawing.Point(152, 140)
    Me.cbAr_tipokit.Name = "cbAr_tipokit"
    Me.cbAr_tipokit.NTSDbField = ""
    Me.cbAr_tipokit.Properties.AutoHeight = False
    Me.cbAr_tipokit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_tipokit.Properties.DropDownRows = 30
    Me.cbAr_tipokit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_tipokit.SelectedValue = ""
    Me.cbAr_tipokit.Size = New System.Drawing.Size(271, 20)
    Me.cbAr_tipokit.TabIndex = 661
    Me.cbAr_tipokit.ValueMember = ""
    '
    'lbAr_flricmar
    '
    Me.lbAr_flricmar.AutoSize = True
    Me.lbAr_flricmar.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_flricmar.Location = New System.Drawing.Point(9, 97)
    Me.lbAr_flricmar.Name = "lbAr_flricmar"
    Me.lbAr_flricmar.NTSDbField = ""
    Me.lbAr_flricmar.Size = New System.Drawing.Size(86, 13)
    Me.lbAr_flricmar.TabIndex = 652
    Me.lbAr_flricmar.Text = "Ricarico/Margine"
    Me.lbAr_flricmar.Tooltip = ""
    Me.lbAr_flricmar.UseMnemonic = False
    '
    'cbAr_flricmar
    '
    Me.cbAr_flricmar.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_flricmar.DataSource = Nothing
    Me.cbAr_flricmar.DisplayMember = ""
    Me.cbAr_flricmar.Location = New System.Drawing.Point(152, 94)
    Me.cbAr_flricmar.Name = "cbAr_flricmar"
    Me.cbAr_flricmar.NTSDbField = ""
    Me.cbAr_flricmar.Properties.AutoHeight = False
    Me.cbAr_flricmar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_flricmar.Properties.DropDownRows = 30
    Me.cbAr_flricmar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_flricmar.SelectedValue = ""
    Me.cbAr_flricmar.Size = New System.Drawing.Size(106, 20)
    Me.cbAr_flricmar.TabIndex = 654
    Me.cbAr_flricmar.ValueMember = ""
    '
    'lbAr_codpdon
    '
    Me.lbAr_codpdon.AutoSize = True
    Me.lbAr_codpdon.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codpdon.Location = New System.Drawing.Point(9, 75)
    Me.lbAr_codpdon.Name = "lbAr_codpdon"
    Me.lbAr_codpdon.NTSDbField = ""
    Me.lbAr_codpdon.Size = New System.Drawing.Size(79, 13)
    Me.lbAr_codpdon.TabIndex = 653
    Me.lbAr_codpdon.Text = "Relazione listini"
    Me.lbAr_codpdon.Tooltip = ""
    Me.lbAr_codpdon.UseMnemonic = False
    '
    'edAr_codpdon
    '
    Me.edAr_codpdon.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codpdon.EditValue = "0"
    Me.edAr_codpdon.Location = New System.Drawing.Point(152, 72)
    Me.edAr_codpdon.Name = "edAr_codpdon"
    Me.edAr_codpdon.NTSDbField = ""
    Me.edAr_codpdon.NTSFormat = "0"
    Me.edAr_codpdon.NTSForzaVisZoom = False
    Me.edAr_codpdon.NTSOldValue = ""
    Me.edAr_codpdon.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_codpdon.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_codpdon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codpdon.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codpdon.Properties.AutoHeight = False
    Me.edAr_codpdon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codpdon.Properties.MaxLength = 65536
    Me.edAr_codpdon.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codpdon.Size = New System.Drawing.Size(50, 20)
    Me.edAr_codpdon.TabIndex = 655
    '
    'edAr_ricar1
    '
    Me.edAr_ricar1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ricar1.EditValue = "0"
    Me.edAr_ricar1.Location = New System.Drawing.Point(271, 94)
    Me.edAr_ricar1.Name = "edAr_ricar1"
    Me.edAr_ricar1.NTSDbField = ""
    Me.edAr_ricar1.NTSFormat = "0"
    Me.edAr_ricar1.NTSForzaVisZoom = False
    Me.edAr_ricar1.NTSOldValue = ""
    Me.edAr_ricar1.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_ricar1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_ricar1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_ricar1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_ricar1.Properties.AutoHeight = False
    Me.edAr_ricar1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_ricar1.Properties.MaxLength = 65536
    Me.edAr_ricar1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_ricar1.Size = New System.Drawing.Size(73, 20)
    Me.edAr_ricar1.TabIndex = 656
    '
    'edAr_ricar2
    '
    Me.edAr_ricar2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ricar2.EditValue = "0"
    Me.edAr_ricar2.Location = New System.Drawing.Point(350, 94)
    Me.edAr_ricar2.Name = "edAr_ricar2"
    Me.edAr_ricar2.NTSDbField = ""
    Me.edAr_ricar2.NTSFormat = "0"
    Me.edAr_ricar2.NTSForzaVisZoom = False
    Me.edAr_ricar2.NTSOldValue = ""
    Me.edAr_ricar2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_ricar2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_ricar2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_ricar2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_ricar2.Properties.AutoHeight = False
    Me.edAr_ricar2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_ricar2.Properties.MaxLength = 65536
    Me.edAr_ricar2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_ricar2.Size = New System.Drawing.Size(73, 20)
    Me.edAr_ricar2.TabIndex = 657
    '
    'lbAr_reparto
    '
    Me.lbAr_reparto.AutoSize = True
    Me.lbAr_reparto.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_reparto.Location = New System.Drawing.Point(9, 187)
    Me.lbAr_reparto.Name = "lbAr_reparto"
    Me.lbAr_reparto.NTSDbField = ""
    Me.lbAr_reparto.Size = New System.Drawing.Size(77, 13)
    Me.lbAr_reparto.TabIndex = 650
    Me.lbAr_reparto.Text = "Reparto (ECR)"
    Me.lbAr_reparto.Tooltip = ""
    Me.lbAr_reparto.UseMnemonic = False
    '
    'edAr_reparto
    '
    Me.edAr_reparto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_reparto.EditValue = "0"
    Me.edAr_reparto.Location = New System.Drawing.Point(152, 184)
    Me.edAr_reparto.Name = "edAr_reparto"
    Me.edAr_reparto.NTSDbField = ""
    Me.edAr_reparto.NTSFormat = "0"
    Me.edAr_reparto.NTSForzaVisZoom = False
    Me.edAr_reparto.NTSOldValue = ""
    Me.edAr_reparto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_reparto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_reparto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_reparto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_reparto.Properties.AutoHeight = False
    Me.edAr_reparto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_reparto.Properties.MaxLength = 65536
    Me.edAr_reparto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_reparto.Size = New System.Drawing.Size(50, 20)
    Me.edAr_reparto.TabIndex = 651
    '
    'edAr_garacq
    '
    Me.edAr_garacq.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAr_garacq.EditValue = "0"
    Me.edAr_garacq.Location = New System.Drawing.Point(208, 50)
    Me.edAr_garacq.Name = "edAr_garacq"
    Me.edAr_garacq.NTSDbField = ""
    Me.edAr_garacq.NTSFormat = "0"
    Me.edAr_garacq.NTSForzaVisZoom = False
    Me.edAr_garacq.NTSOldValue = ""
    Me.edAr_garacq.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_garacq.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_garacq.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_garacq.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_garacq.Properties.AutoHeight = False
    Me.edAr_garacq.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_garacq.Properties.MaxLength = 65536
    Me.edAr_garacq.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_garacq.Size = New System.Drawing.Size(50, 20)
    Me.edAr_garacq.TabIndex = 648
    '
    'lbAr_garven
    '
    Me.lbAr_garven.AutoSize = True
    Me.lbAr_garven.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_garven.Location = New System.Drawing.Point(9, 53)
    Me.lbAr_garven.Name = "lbAr_garven"
    Me.lbAr_garven.NTSDbField = ""
    Me.lbAr_garven.Size = New System.Drawing.Size(139, 13)
    Me.lbAr_garven.TabIndex = 647
    Me.lbAr_garven.Text = "Mesi di garanzia vend./acq."
    Me.lbAr_garven.Tooltip = ""
    Me.lbAr_garven.UseMnemonic = False
    '
    'edAr_garven
    '
    Me.edAr_garven.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_garven.EditValue = "0"
    Me.edAr_garven.Location = New System.Drawing.Point(152, 50)
    Me.edAr_garven.Name = "edAr_garven"
    Me.edAr_garven.NTSDbField = ""
    Me.edAr_garven.NTSFormat = "0"
    Me.edAr_garven.NTSForzaVisZoom = False
    Me.edAr_garven.NTSOldValue = ""
    Me.edAr_garven.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_garven.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_garven.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_garven.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_garven.Properties.AutoHeight = False
    Me.edAr_garven.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_garven.Properties.MaxLength = 65536
    Me.edAr_garven.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_garven.Size = New System.Drawing.Size(50, 20)
    Me.edAr_garven.TabIndex = 649
    '
    'lbAr_perqta
    '
    Me.lbAr_perqta.AutoSize = True
    Me.lbAr_perqta.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_perqta.Location = New System.Drawing.Point(9, 30)
    Me.lbAr_perqta.Name = "lbAr_perqta"
    Me.lbAr_perqta.NTSDbField = ""
    Me.lbAr_perqta.Size = New System.Drawing.Size(125, 13)
    Me.lbAr_perqta.TabIndex = 645
    Me.lbAr_perqta.Text = "Moltiplicatore qtà/prezzo"
    Me.lbAr_perqta.Tooltip = ""
    Me.lbAr_perqta.UseMnemonic = False
    '
    'edAr_perqta
    '
    Me.edAr_perqta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_perqta.EditValue = "0"
    Me.edAr_perqta.Location = New System.Drawing.Point(152, 27)
    Me.edAr_perqta.Name = "edAr_perqta"
    Me.edAr_perqta.NTSDbField = ""
    Me.edAr_perqta.NTSFormat = "0"
    Me.edAr_perqta.NTSForzaVisZoom = False
    Me.edAr_perqta.NTSOldValue = ""
    Me.edAr_perqta.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_perqta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_perqta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_perqta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_perqta.Properties.AutoHeight = False
    Me.edAr_perqta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_perqta.Properties.MaxLength = 65536
    Me.edAr_perqta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_perqta.Size = New System.Drawing.Size(271, 20)
    Me.edAr_perqta.TabIndex = 646
    '
    'lbAr_codmarc
    '
    Me.lbAr_codmarc.AutoSize = True
    Me.lbAr_codmarc.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codmarc.Location = New System.Drawing.Point(9, 8)
    Me.lbAr_codmarc.Name = "lbAr_codmarc"
    Me.lbAr_codmarc.NTSDbField = ""
    Me.lbAr_codmarc.Size = New System.Drawing.Size(36, 13)
    Me.lbAr_codmarc.TabIndex = 643
    Me.lbAr_codmarc.Text = "Marca"
    Me.lbAr_codmarc.Tooltip = ""
    Me.lbAr_codmarc.UseMnemonic = False
    '
    'edAr_codmarc
    '
    Me.edAr_codmarc.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAr_codmarc.EditValue = "0"
    Me.edAr_codmarc.Location = New System.Drawing.Point(152, 5)
    Me.edAr_codmarc.Name = "edAr_codmarc"
    Me.edAr_codmarc.NTSDbField = ""
    Me.edAr_codmarc.NTSFormat = "0"
    Me.edAr_codmarc.NTSForzaVisZoom = False
    Me.edAr_codmarc.NTSOldValue = ""
    Me.edAr_codmarc.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_codmarc.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_codmarc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codmarc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codmarc.Properties.AutoHeight = False
    Me.edAr_codmarc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codmarc.Properties.MaxLength = 65536
    Me.edAr_codmarc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codmarc.Size = New System.Drawing.Size(50, 20)
    Me.edAr_codmarc.TabIndex = 644
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnTabpag2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(755, 294)
    Me.NtsTabPage2.Text = "&2 - Acquisti/Produzione"
    '
    'pnTabpag2
    '
    Me.pnTabpag2.AllowDrop = True
    Me.pnTabpag2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag2.Appearance.Options.UseBackColor = True
    Me.pnTabpag2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag2.Controls.Add(Me.fmProduzione)
    Me.pnTabpag2.Controls.Add(Me.fmAcquisti)
    Me.pnTabpag2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag2.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag2.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag2.Name = "pnTabpag2"
    Me.pnTabpag2.NTSActiveTrasparency = True
    Me.pnTabpag2.Size = New System.Drawing.Size(755, 294)
    Me.pnTabpag2.TabIndex = 550
    Me.pnTabpag2.Text = "NtsPanel1"
    '
    'fmProduzione
    '
    Me.fmProduzione.AllowDrop = True
    Me.fmProduzione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmProduzione.Appearance.Options.UseBackColor = True
    Me.fmProduzione.Controls.Add(Me.ckAr_consmrp)
    Me.fmProduzione.Controls.Add(Me.ckAr_critico)
    Me.fmProduzione.Controls.Add(Me.ckAr_blocco)
    Me.fmProduzione.Controls.Add(Me.lbAr_tipoopz)
    Me.fmProduzione.Controls.Add(Me.cbAr_tipoopz)
    Me.fmProduzione.Controls.Add(Me.lbAr_gescomm)
    Me.fmProduzione.Controls.Add(Me.cbAr_gescomm)
    Me.fmProduzione.Controls.Add(Me.lbAr_fpfence)
    Me.fmProduzione.Controls.Add(Me.edAr_fpfence)
    Me.fmProduzione.Controls.Add(Me.lbAr_rrfence)
    Me.fmProduzione.Controls.Add(Me.edAr_rrfence)
    Me.fmProduzione.Controls.Add(Me.lbAr_ggrior)
    Me.fmProduzione.Controls.Add(Me.edAr_ggrior)
    Me.fmProduzione.Controls.Add(Me.lbAr_fcorrlt)
    Me.fmProduzione.Controls.Add(Me.edAr_fcorrlt)
    Me.fmProduzione.Controls.Add(Me.lbAr_verdb)
    Me.fmProduzione.Controls.Add(Me.edAr_verdb)
    Me.fmProduzione.Controls.Add(Me.lbAr_livmindb)
    Me.fmProduzione.Controls.Add(Me.edAr_livmindb)
    Me.fmProduzione.Controls.Add(Me.lbAr_coddb)
    Me.fmProduzione.Controls.Add(Me.edAr_coddb)
    Me.fmProduzione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmProduzione.Location = New System.Drawing.Point(444, 3)
    Me.fmProduzione.Name = "fmProduzione"
    Me.fmProduzione.Size = New System.Drawing.Size(309, 259)
    Me.fmProduzione.TabIndex = 601
    Me.fmProduzione.Text = "PRODUZIONE"
    '
    'ckAr_consmrp
    '
    Me.ckAr_consmrp.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_consmrp.Location = New System.Drawing.Point(130, 206)
    Me.ckAr_consmrp.Name = "ckAr_consmrp"
    Me.ckAr_consmrp.NTSCheckValue = "S"
    Me.ckAr_consmrp.NTSUnCheckValue = "N"
    Me.ckAr_consmrp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_consmrp.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_consmrp.Properties.AutoHeight = False
    Me.ckAr_consmrp.Properties.Caption = "Considera in &MRP/Distinte Base"
    Me.ckAr_consmrp.Size = New System.Drawing.Size(172, 19)
    Me.ckAr_consmrp.TabIndex = 634
    '
    'ckAr_critico
    '
    Me.ckAr_critico.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_critico.Location = New System.Drawing.Point(6, 206)
    Me.ckAr_critico.Name = "ckAr_critico"
    Me.ckAr_critico.NTSCheckValue = "S"
    Me.ckAr_critico.NTSUnCheckValue = "N"
    Me.ckAr_critico.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_critico.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_critico.Properties.AutoHeight = False
    Me.ckAr_critico.Properties.Caption = "&Componente critico"
    Me.ckAr_critico.Size = New System.Drawing.Size(118, 19)
    Me.ckAr_critico.TabIndex = 633
    '
    'ckAr_blocco
    '
    Me.ckAr_blocco.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckAr_blocco.Location = New System.Drawing.Point(6, 231)
    Me.ckAr_blocco.Name = "ckAr_blocco"
    Me.ckAr_blocco.NTSCheckValue = "S"
    Me.ckAr_blocco.NTSUnCheckValue = "N"
    Me.ckAr_blocco.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_blocco.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_blocco.Properties.AutoHeight = False
    Me.ckAr_blocco.Properties.Caption = "&Blocco"
    Me.ckAr_blocco.Size = New System.Drawing.Size(58, 19)
    Me.ckAr_blocco.TabIndex = 632
    '
    'lbAr_tipoopz
    '
    Me.lbAr_tipoopz.AutoSize = True
    Me.lbAr_tipoopz.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_tipoopz.Location = New System.Drawing.Point(5, 154)
    Me.lbAr_tipoopz.Name = "lbAr_tipoopz"
    Me.lbAr_tipoopz.NTSDbField = ""
    Me.lbAr_tipoopz.Size = New System.Drawing.Size(67, 13)
    Me.lbAr_tipoopz.TabIndex = 628
    Me.lbAr_tipoopz.Text = "Tipo opzione"
    Me.lbAr_tipoopz.Tooltip = ""
    Me.lbAr_tipoopz.UseMnemonic = False
    '
    'cbAr_tipoopz
    '
    Me.cbAr_tipoopz.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_tipoopz.DataSource = Nothing
    Me.cbAr_tipoopz.DisplayMember = ""
    Me.cbAr_tipoopz.Location = New System.Drawing.Point(129, 151)
    Me.cbAr_tipoopz.Name = "cbAr_tipoopz"
    Me.cbAr_tipoopz.NTSDbField = ""
    Me.cbAr_tipoopz.Properties.AutoHeight = False
    Me.cbAr_tipoopz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_tipoopz.Properties.DropDownRows = 30
    Me.cbAr_tipoopz.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_tipoopz.SelectedValue = ""
    Me.cbAr_tipoopz.Size = New System.Drawing.Size(175, 20)
    Me.cbAr_tipoopz.TabIndex = 630
    Me.cbAr_tipoopz.ValueMember = ""
    '
    'lbAr_gescomm
    '
    Me.lbAr_gescomm.AutoSize = True
    Me.lbAr_gescomm.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gescomm.Location = New System.Drawing.Point(5, 179)
    Me.lbAr_gescomm.Name = "lbAr_gescomm"
    Me.lbAr_gescomm.NTSDbField = ""
    Me.lbAr_gescomm.Size = New System.Drawing.Size(120, 13)
    Me.lbAr_gescomm.TabIndex = 629
    Me.lbAr_gescomm.Text = "Gestione per commessa"
    Me.lbAr_gescomm.Tooltip = ""
    Me.lbAr_gescomm.UseMnemonic = False
    '
    'cbAr_gescomm
    '
    Me.cbAr_gescomm.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_gescomm.DataSource = Nothing
    Me.cbAr_gescomm.DisplayMember = ""
    Me.cbAr_gescomm.Location = New System.Drawing.Point(129, 176)
    Me.cbAr_gescomm.Name = "cbAr_gescomm"
    Me.cbAr_gescomm.NTSDbField = ""
    Me.cbAr_gescomm.Properties.AutoHeight = False
    Me.cbAr_gescomm.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_gescomm.Properties.DropDownRows = 30
    Me.cbAr_gescomm.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_gescomm.SelectedValue = ""
    Me.cbAr_gescomm.Size = New System.Drawing.Size(175, 20)
    Me.cbAr_gescomm.TabIndex = 631
    Me.cbAr_gescomm.ValueMember = ""
    '
    'lbAr_fpfence
    '
    Me.lbAr_fpfence.AutoSize = True
    Me.lbAr_fpfence.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_fpfence.Location = New System.Drawing.Point(198, 104)
    Me.lbAr_fpfence.Name = "lbAr_fpfence"
    Me.lbAr_fpfence.NTSDbField = ""
    Me.lbAr_fpfence.Size = New System.Drawing.Size(51, 13)
    Me.lbAr_fpfence.TabIndex = 624
    Me.lbAr_fpfence.Text = "FP Fence"
    Me.lbAr_fpfence.Tooltip = ""
    Me.lbAr_fpfence.UseMnemonic = False
    '
    'edAr_fpfence
    '
    Me.edAr_fpfence.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAr_fpfence.EditValue = "0"
    Me.edAr_fpfence.Location = New System.Drawing.Point(262, 101)
    Me.edAr_fpfence.Name = "edAr_fpfence"
    Me.edAr_fpfence.NTSDbField = ""
    Me.edAr_fpfence.NTSFormat = "0"
    Me.edAr_fpfence.NTSForzaVisZoom = False
    Me.edAr_fpfence.NTSOldValue = ""
    Me.edAr_fpfence.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_fpfence.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_fpfence.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_fpfence.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_fpfence.Properties.AutoHeight = False
    Me.edAr_fpfence.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_fpfence.Properties.MaxLength = 65536
    Me.edAr_fpfence.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_fpfence.Size = New System.Drawing.Size(42, 20)
    Me.edAr_fpfence.TabIndex = 626
    '
    'lbAr_rrfence
    '
    Me.lbAr_rrfence.AutoSize = True
    Me.lbAr_rrfence.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_rrfence.Location = New System.Drawing.Point(5, 104)
    Me.lbAr_rrfence.Name = "lbAr_rrfence"
    Me.lbAr_rrfence.NTSDbField = ""
    Me.lbAr_rrfence.Size = New System.Drawing.Size(53, 13)
    Me.lbAr_rrfence.TabIndex = 625
    Me.lbAr_rrfence.Text = "RR Fence"
    Me.lbAr_rrfence.Tooltip = ""
    Me.lbAr_rrfence.UseMnemonic = False
    '
    'edAr_rrfence
    '
    Me.edAr_rrfence.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_rrfence.EditValue = "0"
    Me.edAr_rrfence.Location = New System.Drawing.Point(129, 101)
    Me.edAr_rrfence.Name = "edAr_rrfence"
    Me.edAr_rrfence.NTSDbField = ""
    Me.edAr_rrfence.NTSFormat = "0"
    Me.edAr_rrfence.NTSForzaVisZoom = False
    Me.edAr_rrfence.NTSOldValue = ""
    Me.edAr_rrfence.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_rrfence.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_rrfence.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_rrfence.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_rrfence.Properties.AutoHeight = False
    Me.edAr_rrfence.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_rrfence.Properties.MaxLength = 65536
    Me.edAr_rrfence.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_rrfence.Size = New System.Drawing.Size(60, 20)
    Me.edAr_rrfence.TabIndex = 627
    '
    'lbAr_ggrior
    '
    Me.lbAr_ggrior.AutoSize = True
    Me.lbAr_ggrior.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_ggrior.Location = New System.Drawing.Point(5, 79)
    Me.lbAr_ggrior.Name = "lbAr_ggrior"
    Me.lbAr_ggrior.NTSDbField = ""
    Me.lbAr_ggrior.Size = New System.Drawing.Size(65, 13)
    Me.lbAr_ggrior.TabIndex = 622
    Me.lbAr_ggrior.Text = "Non usato 1"
    Me.lbAr_ggrior.Tooltip = ""
    Me.lbAr_ggrior.UseMnemonic = False
    '
    'edAr_ggrior
    '
    Me.edAr_ggrior.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ggrior.EditValue = "0"
    Me.edAr_ggrior.Location = New System.Drawing.Point(129, 76)
    Me.edAr_ggrior.Name = "edAr_ggrior"
    Me.edAr_ggrior.NTSDbField = ""
    Me.edAr_ggrior.NTSFormat = "0"
    Me.edAr_ggrior.NTSForzaVisZoom = False
    Me.edAr_ggrior.NTSOldValue = ""
    Me.edAr_ggrior.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_ggrior.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_ggrior.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_ggrior.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_ggrior.Properties.AutoHeight = False
    Me.edAr_ggrior.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_ggrior.Properties.MaxLength = 65536
    Me.edAr_ggrior.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_ggrior.Size = New System.Drawing.Size(60, 20)
    Me.edAr_ggrior.TabIndex = 623
    '
    'lbAr_fcorrlt
    '
    Me.lbAr_fcorrlt.AutoSize = True
    Me.lbAr_fcorrlt.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_fcorrlt.Location = New System.Drawing.Point(5, 129)
    Me.lbAr_fcorrlt.Name = "lbAr_fcorrlt"
    Me.lbAr_fcorrlt.NTSDbField = ""
    Me.lbAr_fcorrlt.Size = New System.Drawing.Size(118, 13)
    Me.lbAr_fcorrlt.TabIndex = 618
    Me.lbAr_fcorrlt.Text = "Fattore correzione L.T."
    Me.lbAr_fcorrlt.Tooltip = ""
    Me.lbAr_fcorrlt.UseMnemonic = False
    '
    'edAr_fcorrlt
    '
    Me.edAr_fcorrlt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_fcorrlt.EditValue = "0"
    Me.edAr_fcorrlt.Location = New System.Drawing.Point(129, 126)
    Me.edAr_fcorrlt.Name = "edAr_fcorrlt"
    Me.edAr_fcorrlt.NTSDbField = ""
    Me.edAr_fcorrlt.NTSFormat = "0"
    Me.edAr_fcorrlt.NTSForzaVisZoom = False
    Me.edAr_fcorrlt.NTSOldValue = ""
    Me.edAr_fcorrlt.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_fcorrlt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_fcorrlt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_fcorrlt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_fcorrlt.Properties.AutoHeight = False
    Me.edAr_fcorrlt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_fcorrlt.Properties.MaxLength = 65536
    Me.edAr_fcorrlt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_fcorrlt.Size = New System.Drawing.Size(175, 20)
    Me.edAr_fcorrlt.TabIndex = 620
    '
    'lbAr_verdb
    '
    Me.lbAr_verdb.AutoSize = True
    Me.lbAr_verdb.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_verdb.Location = New System.Drawing.Point(6, 54)
    Me.lbAr_verdb.Name = "lbAr_verdb"
    Me.lbAr_verdb.NTSDbField = ""
    Me.lbAr_verdb.Size = New System.Drawing.Size(113, 13)
    Me.lbAr_verdb.TabIndex = 619
    Me.lbAr_verdb.Text = "Versione Distinta Base"
    Me.lbAr_verdb.Tooltip = ""
    Me.lbAr_verdb.UseMnemonic = False
    '
    'edAr_verdb
    '
    Me.edAr_verdb.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_verdb.EditValue = "0"
    Me.edAr_verdb.Location = New System.Drawing.Point(129, 51)
    Me.edAr_verdb.Name = "edAr_verdb"
    Me.edAr_verdb.NTSDbField = ""
    Me.edAr_verdb.NTSFormat = "0"
    Me.edAr_verdb.NTSForzaVisZoom = False
    Me.edAr_verdb.NTSOldValue = ""
    Me.edAr_verdb.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_verdb.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_verdb.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_verdb.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_verdb.Properties.AutoHeight = False
    Me.edAr_verdb.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_verdb.Properties.MaxLength = 65536
    Me.edAr_verdb.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_verdb.Size = New System.Drawing.Size(60, 20)
    Me.edAr_verdb.TabIndex = 621
    '
    'lbAr_livmindb
    '
    Me.lbAr_livmindb.AutoSize = True
    Me.lbAr_livmindb.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_livmindb.Location = New System.Drawing.Point(198, 52)
    Me.lbAr_livmindb.Name = "lbAr_livmindb"
    Me.lbAr_livmindb.NTSDbField = ""
    Me.lbAr_livmindb.Size = New System.Drawing.Size(59, 13)
    Me.lbAr_livmindb.TabIndex = 614
    Me.lbAr_livmindb.Text = "Liv. minimo"
    Me.lbAr_livmindb.Tooltip = ""
    Me.lbAr_livmindb.UseMnemonic = False
    '
    'edAr_livmindb
    '
    Me.edAr_livmindb.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAr_livmindb.EditValue = "0"
    Me.edAr_livmindb.Location = New System.Drawing.Point(262, 49)
    Me.edAr_livmindb.Name = "edAr_livmindb"
    Me.edAr_livmindb.NTSDbField = ""
    Me.edAr_livmindb.NTSFormat = "0"
    Me.edAr_livmindb.NTSForzaVisZoom = False
    Me.edAr_livmindb.NTSOldValue = ""
    Me.edAr_livmindb.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_livmindb.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_livmindb.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_livmindb.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_livmindb.Properties.AutoHeight = False
    Me.edAr_livmindb.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_livmindb.Properties.MaxLength = 65536
    Me.edAr_livmindb.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_livmindb.Size = New System.Drawing.Size(42, 20)
    Me.edAr_livmindb.TabIndex = 616
    '
    'lbAr_coddb
    '
    Me.lbAr_coddb.AutoSize = True
    Me.lbAr_coddb.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_coddb.Location = New System.Drawing.Point(6, 29)
    Me.lbAr_coddb.Name = "lbAr_coddb"
    Me.lbAr_coddb.NTSDbField = ""
    Me.lbAr_coddb.Size = New System.Drawing.Size(104, 13)
    Me.lbAr_coddb.TabIndex = 615
    Me.lbAr_coddb.Text = "Codice Distinta Base"
    Me.lbAr_coddb.Tooltip = ""
    Me.lbAr_coddb.UseMnemonic = False
    '
    'edAr_coddb
    '
    Me.edAr_coddb.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_coddb.EditValue = ""
    Me.edAr_coddb.Location = New System.Drawing.Point(129, 26)
    Me.edAr_coddb.Name = "edAr_coddb"
    Me.edAr_coddb.NTSDbField = ""
    Me.edAr_coddb.NTSForzaVisZoom = False
    Me.edAr_coddb.NTSOldValue = ""
    Me.edAr_coddb.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_coddb.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_coddb.Properties.AutoHeight = False
    Me.edAr_coddb.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_coddb.Properties.MaxLength = 65536
    Me.edAr_coddb.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_coddb.Size = New System.Drawing.Size(175, 20)
    Me.edAr_coddb.TabIndex = 617
    '
    'fmAcquisti
    '
    Me.fmAcquisti.AllowDrop = True
    Me.fmAcquisti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAcquisti.Appearance.Options.UseBackColor = True
    Me.fmAcquisti.Controls.Add(Me.lbAr_scosic)
    Me.fmAcquisti.Controls.Add(Me.edAr_scosic)
    Me.fmAcquisti.Controls.Add(Me.lbXx_forn)
    Me.fmAcquisti.Controls.Add(Me.lbXx_forn2)
    Me.fmAcquisti.Controls.Add(Me.lbAr_ggant)
    Me.fmAcquisti.Controls.Add(Me.edAr_ggant)
    Me.fmAcquisti.Controls.Add(Me.lbAr_ggpost)
    Me.fmAcquisti.Controls.Add(Me.edAr_ggpost)
    Me.fmAcquisti.Controls.Add(Me.lbAr_ggragg)
    Me.fmAcquisti.Controls.Add(Me.edAr_ggragg)
    Me.fmAcquisti.Controls.Add(Me.lbAr_perragg)
    Me.fmAcquisti.Controls.Add(Me.cbAr_perragg)
    Me.fmAcquisti.Controls.Add(Me.lbAr_scomax)
    Me.fmAcquisti.Controls.Add(Me.lbAr_maxlotto)
    Me.fmAcquisti.Controls.Add(Me.lbAr_scomin)
    Me.fmAcquisti.Controls.Add(Me.edAr_scomin)
    Me.fmAcquisti.Controls.Add(Me.edAr_scomax)
    Me.fmAcquisti.Controls.Add(Me.lbAr_sublotto)
    Me.fmAcquisti.Controls.Add(Me.edAr_sublotto)
    Me.fmAcquisti.Controls.Add(Me.edAr_maxlotto)
    Me.fmAcquisti.Controls.Add(Me.ckAr_ripriord)
    Me.fmAcquisti.Controls.Add(Me.lbAr_minord)
    Me.fmAcquisti.Controls.Add(Me.edAr_minord)
    Me.fmAcquisti.Controls.Add(Me.lbAr_polriord)
    Me.fmAcquisti.Controls.Add(Me.cbAr_polriord)
    Me.fmAcquisti.Controls.Add(Me.edAr_forn)
    Me.fmAcquisti.Controls.Add(Me.lbAr_forn)
    Me.fmAcquisti.Controls.Add(Me.edAr_forn2)
    Me.fmAcquisti.Controls.Add(Me.lbAr_forn2)
    Me.fmAcquisti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAcquisti.Location = New System.Drawing.Point(6, 3)
    Me.fmAcquisti.Name = "fmAcquisti"
    Me.fmAcquisti.Size = New System.Drawing.Size(437, 259)
    Me.fmAcquisti.TabIndex = 600
    Me.fmAcquisti.Text = "ACQUISTI"
    '
    'lbAr_scosic
    '
    Me.lbAr_scosic.AutoSize = True
    Me.lbAr_scosic.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_scosic.Location = New System.Drawing.Point(6, 230)
    Me.lbAr_scosic.Name = "lbAr_scosic"
    Me.lbAr_scosic.NTSDbField = ""
    Me.lbAr_scosic.Size = New System.Drawing.Size(96, 13)
    Me.lbAr_scosic.TabIndex = 629
    Me.lbAr_scosic.Text = "Scorta di sicurezza"
    Me.lbAr_scosic.Tooltip = ""
    Me.lbAr_scosic.UseMnemonic = False
    '
    'edAr_scosic
    '
    Me.edAr_scosic.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_scosic.EditValue = "0"
    Me.edAr_scosic.Location = New System.Drawing.Point(112, 227)
    Me.edAr_scosic.Name = "edAr_scosic"
    Me.edAr_scosic.NTSDbField = ""
    Me.edAr_scosic.NTSFormat = "0"
    Me.edAr_scosic.NTSForzaVisZoom = False
    Me.edAr_scosic.NTSOldValue = ""
    Me.edAr_scosic.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_scosic.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_scosic.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_scosic.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_scosic.Properties.AutoHeight = False
    Me.edAr_scosic.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_scosic.Properties.MaxLength = 65536
    Me.edAr_scosic.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_scosic.Size = New System.Drawing.Size(89, 20)
    Me.edAr_scosic.TabIndex = 630
    '
    'lbXx_forn
    '
    Me.lbXx_forn.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_forn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_forn.Location = New System.Drawing.Point(210, 26)
    Me.lbXx_forn.Name = "lbXx_forn"
    Me.lbXx_forn.NTSDbField = ""
    Me.lbXx_forn.Size = New System.Drawing.Size(222, 20)
    Me.lbXx_forn.TabIndex = 627
    Me.lbXx_forn.Tooltip = ""
    Me.lbXx_forn.UseMnemonic = False
    '
    'lbXx_forn2
    '
    Me.lbXx_forn2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_forn2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_forn2.Location = New System.Drawing.Point(210, 51)
    Me.lbXx_forn2.Name = "lbXx_forn2"
    Me.lbXx_forn2.NTSDbField = ""
    Me.lbXx_forn2.Size = New System.Drawing.Size(222, 20)
    Me.lbXx_forn2.TabIndex = 628
    Me.lbXx_forn2.Tooltip = ""
    Me.lbXx_forn2.UseMnemonic = False
    '
    'lbAr_ggant
    '
    Me.lbAr_ggant.AutoSize = True
    Me.lbAr_ggant.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_ggant.Location = New System.Drawing.Point(6, 204)
    Me.lbAr_ggant.Name = "lbAr_ggant"
    Me.lbAr_ggant.NTSDbField = ""
    Me.lbAr_ggant.Size = New System.Drawing.Size(85, 13)
    Me.lbAr_ggant.TabIndex = 623
    Me.lbAr_ggant.Text = "Giorni di anticipo"
    Me.lbAr_ggant.Tooltip = ""
    Me.lbAr_ggant.UseMnemonic = False
    '
    'edAr_ggant
    '
    Me.edAr_ggant.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ggant.EditValue = "0"
    Me.edAr_ggant.Location = New System.Drawing.Point(112, 202)
    Me.edAr_ggant.Name = "edAr_ggant"
    Me.edAr_ggant.NTSDbField = ""
    Me.edAr_ggant.NTSFormat = "0"
    Me.edAr_ggant.NTSForzaVisZoom = False
    Me.edAr_ggant.NTSOldValue = ""
    Me.edAr_ggant.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_ggant.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_ggant.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_ggant.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_ggant.Properties.AutoHeight = False
    Me.edAr_ggant.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_ggant.Properties.MaxLength = 65536
    Me.edAr_ggant.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_ggant.Size = New System.Drawing.Size(89, 20)
    Me.edAr_ggant.TabIndex = 625
    '
    'lbAr_ggpost
    '
    Me.lbAr_ggpost.AutoSize = True
    Me.lbAr_ggpost.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_ggpost.Location = New System.Drawing.Point(228, 204)
    Me.lbAr_ggpost.Name = "lbAr_ggpost"
    Me.lbAr_ggpost.NTSDbField = ""
    Me.lbAr_ggpost.Size = New System.Drawing.Size(90, 13)
    Me.lbAr_ggpost.TabIndex = 624
    Me.lbAr_ggpost.Text = "Giorni di posticipo"
    Me.lbAr_ggpost.Tooltip = ""
    Me.lbAr_ggpost.UseMnemonic = False
    '
    'edAr_ggpost
    '
    Me.edAr_ggpost.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ggpost.EditValue = "0"
    Me.edAr_ggpost.Location = New System.Drawing.Point(374, 202)
    Me.edAr_ggpost.Name = "edAr_ggpost"
    Me.edAr_ggpost.NTSDbField = ""
    Me.edAr_ggpost.NTSFormat = "0"
    Me.edAr_ggpost.NTSForzaVisZoom = False
    Me.edAr_ggpost.NTSOldValue = ""
    Me.edAr_ggpost.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_ggpost.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_ggpost.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_ggpost.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_ggpost.Properties.AutoHeight = False
    Me.edAr_ggpost.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_ggpost.Properties.MaxLength = 65536
    Me.edAr_ggpost.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_ggpost.Size = New System.Drawing.Size(58, 20)
    Me.edAr_ggpost.TabIndex = 626
    '
    'lbAr_ggragg
    '
    Me.lbAr_ggragg.AutoSize = True
    Me.lbAr_ggragg.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_ggragg.Location = New System.Drawing.Point(228, 179)
    Me.lbAr_ggragg.Name = "lbAr_ggragg"
    Me.lbAr_ggragg.NTSDbField = ""
    Me.lbAr_ggragg.Size = New System.Drawing.Size(128, 13)
    Me.lbAr_ggragg.TabIndex = 619
    Me.lbAr_ggragg.Text = "Giorni di raggruppamento"
    Me.lbAr_ggragg.Tooltip = ""
    Me.lbAr_ggragg.UseMnemonic = False
    '
    'edAr_ggragg
    '
    Me.edAr_ggragg.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ggragg.EditValue = "0"
    Me.edAr_ggragg.Location = New System.Drawing.Point(374, 176)
    Me.edAr_ggragg.Name = "edAr_ggragg"
    Me.edAr_ggragg.NTSDbField = ""
    Me.edAr_ggragg.NTSFormat = "0"
    Me.edAr_ggragg.NTSForzaVisZoom = False
    Me.edAr_ggragg.NTSOldValue = ""
    Me.edAr_ggragg.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_ggragg.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_ggragg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_ggragg.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_ggragg.Properties.AutoHeight = False
    Me.edAr_ggragg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_ggragg.Properties.MaxLength = 65536
    Me.edAr_ggragg.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_ggragg.Size = New System.Drawing.Size(58, 20)
    Me.edAr_ggragg.TabIndex = 621
    '
    'lbAr_perragg
    '
    Me.lbAr_perragg.AutoSize = True
    Me.lbAr_perragg.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_perragg.Location = New System.Drawing.Point(6, 179)
    Me.lbAr_perragg.Name = "lbAr_perragg"
    Me.lbAr_perragg.NTSDbField = ""
    Me.lbAr_perragg.Size = New System.Drawing.Size(94, 13)
    Me.lbAr_perragg.TabIndex = 620
    Me.lbAr_perragg.Text = "Periodo raggrupp."
    Me.lbAr_perragg.Tooltip = ""
    Me.lbAr_perragg.UseMnemonic = False
    '
    'cbAr_perragg
    '
    Me.cbAr_perragg.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_perragg.DataSource = Nothing
    Me.cbAr_perragg.DisplayMember = ""
    Me.cbAr_perragg.Location = New System.Drawing.Point(112, 176)
    Me.cbAr_perragg.Name = "cbAr_perragg"
    Me.cbAr_perragg.NTSDbField = ""
    Me.cbAr_perragg.Properties.AutoHeight = False
    Me.cbAr_perragg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_perragg.Properties.DropDownRows = 30
    Me.cbAr_perragg.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_perragg.SelectedValue = ""
    Me.cbAr_perragg.Size = New System.Drawing.Size(89, 20)
    Me.cbAr_perragg.TabIndex = 622
    Me.cbAr_perragg.ValueMember = ""
    '
    'lbAr_scomax
    '
    Me.lbAr_scomax.AutoSize = True
    Me.lbAr_scomax.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_scomax.Location = New System.Drawing.Point(214, 130)
    Me.lbAr_scomax.Name = "lbAr_scomax"
    Me.lbAr_scomax.NTSDbField = ""
    Me.lbAr_scomax.Size = New System.Drawing.Size(11, 13)
    Me.lbAr_scomax.TabIndex = 618
    Me.lbAr_scomax.Text = "/"
    Me.lbAr_scomax.Tooltip = ""
    Me.lbAr_scomax.UseMnemonic = False
    '
    'lbAr_maxlotto
    '
    Me.lbAr_maxlotto.AutoSize = True
    Me.lbAr_maxlotto.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_maxlotto.Location = New System.Drawing.Point(214, 154)
    Me.lbAr_maxlotto.Name = "lbAr_maxlotto"
    Me.lbAr_maxlotto.NTSDbField = ""
    Me.lbAr_maxlotto.Size = New System.Drawing.Size(11, 13)
    Me.lbAr_maxlotto.TabIndex = 617
    Me.lbAr_maxlotto.Text = "/"
    Me.lbAr_maxlotto.Tooltip = ""
    Me.lbAr_maxlotto.UseMnemonic = False
    '
    'lbAr_scomin
    '
    Me.lbAr_scomin.AutoSize = True
    Me.lbAr_scomin.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_scomin.Location = New System.Drawing.Point(6, 130)
    Me.lbAr_scomin.Name = "lbAr_scomin"
    Me.lbAr_scomin.NTSDbField = ""
    Me.lbAr_scomin.Size = New System.Drawing.Size(97, 13)
    Me.lbAr_scomin.TabIndex = 613
    Me.lbAr_scomin.Text = "Scorta minima/max"
    Me.lbAr_scomin.Tooltip = ""
    Me.lbAr_scomin.UseMnemonic = False
    '
    'edAr_scomin
    '
    Me.edAr_scomin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_scomin.EditValue = "0"
    Me.edAr_scomin.Location = New System.Drawing.Point(112, 126)
    Me.edAr_scomin.Name = "edAr_scomin"
    Me.edAr_scomin.NTSDbField = ""
    Me.edAr_scomin.NTSFormat = "0"
    Me.edAr_scomin.NTSForzaVisZoom = False
    Me.edAr_scomin.NTSOldValue = ""
    Me.edAr_scomin.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_scomin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_scomin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_scomin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_scomin.Properties.AutoHeight = False
    Me.edAr_scomin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_scomin.Properties.MaxLength = 65536
    Me.edAr_scomin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_scomin.Size = New System.Drawing.Size(89, 20)
    Me.edAr_scomin.TabIndex = 615
    '
    'edAr_scomax
    '
    Me.edAr_scomax.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_scomax.EditValue = "0"
    Me.edAr_scomax.Location = New System.Drawing.Point(231, 126)
    Me.edAr_scomax.Name = "edAr_scomax"
    Me.edAr_scomax.NTSDbField = ""
    Me.edAr_scomax.NTSFormat = "0"
    Me.edAr_scomax.NTSForzaVisZoom = False
    Me.edAr_scomax.NTSOldValue = ""
    Me.edAr_scomax.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_scomax.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_scomax.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_scomax.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_scomax.Properties.AutoHeight = False
    Me.edAr_scomax.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_scomax.Properties.MaxLength = 65536
    Me.edAr_scomax.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_scomax.Size = New System.Drawing.Size(89, 20)
    Me.edAr_scomax.TabIndex = 616
    '
    'lbAr_sublotto
    '
    Me.lbAr_sublotto.AutoSize = True
    Me.lbAr_sublotto.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_sublotto.Location = New System.Drawing.Point(6, 156)
    Me.lbAr_sublotto.Name = "lbAr_sublotto"
    Me.lbAr_sublotto.NTSDbField = ""
    Me.lbAr_sublotto.Size = New System.Drawing.Size(100, 13)
    Me.lbAr_sublotto.TabIndex = 609
    Me.lbAr_sublotto.Text = "Sublotto/lotto max."
    Me.lbAr_sublotto.Tooltip = ""
    Me.lbAr_sublotto.UseMnemonic = False
    '
    'edAr_sublotto
    '
    Me.edAr_sublotto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_sublotto.EditValue = "0"
    Me.edAr_sublotto.Location = New System.Drawing.Point(112, 151)
    Me.edAr_sublotto.Name = "edAr_sublotto"
    Me.edAr_sublotto.NTSDbField = ""
    Me.edAr_sublotto.NTSFormat = "0"
    Me.edAr_sublotto.NTSForzaVisZoom = False
    Me.edAr_sublotto.NTSOldValue = ""
    Me.edAr_sublotto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_sublotto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_sublotto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_sublotto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_sublotto.Properties.AutoHeight = False
    Me.edAr_sublotto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_sublotto.Properties.MaxLength = 65536
    Me.edAr_sublotto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_sublotto.Size = New System.Drawing.Size(89, 20)
    Me.edAr_sublotto.TabIndex = 611
    '
    'edAr_maxlotto
    '
    Me.edAr_maxlotto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_maxlotto.EditValue = "0"
    Me.edAr_maxlotto.Location = New System.Drawing.Point(231, 151)
    Me.edAr_maxlotto.Name = "edAr_maxlotto"
    Me.edAr_maxlotto.NTSDbField = ""
    Me.edAr_maxlotto.NTSFormat = "0"
    Me.edAr_maxlotto.NTSForzaVisZoom = False
    Me.edAr_maxlotto.NTSOldValue = ""
    Me.edAr_maxlotto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_maxlotto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_maxlotto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_maxlotto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_maxlotto.Properties.AutoHeight = False
    Me.edAr_maxlotto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_maxlotto.Properties.MaxLength = 65536
    Me.edAr_maxlotto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_maxlotto.Size = New System.Drawing.Size(89, 20)
    Me.edAr_maxlotto.TabIndex = 612
    '
    'ckAr_ripriord
    '
    Me.ckAr_ripriord.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_ripriord.Location = New System.Drawing.Point(229, 102)
    Me.ckAr_ripriord.Name = "ckAr_ripriord"
    Me.ckAr_ripriord.NTSCheckValue = "S"
    Me.ckAr_ripriord.NTSUnCheckValue = "N"
    Me.ckAr_ripriord.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_ripriord.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_ripriord.Properties.AutoHeight = False
    Me.ckAr_ripriord.Properties.Caption = "Ripartizione su più fornitori"
    Me.ckAr_ripriord.Size = New System.Drawing.Size(163, 19)
    Me.ckAr_ripriord.TabIndex = 608
    '
    'lbAr_minord
    '
    Me.lbAr_minord.AutoSize = True
    Me.lbAr_minord.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_minord.Location = New System.Drawing.Point(6, 104)
    Me.lbAr_minord.Name = "lbAr_minord"
    Me.lbAr_minord.NTSDbField = ""
    Me.lbAr_minord.Size = New System.Drawing.Size(103, 13)
    Me.lbAr_minord.TabIndex = 606
    Me.lbAr_minord.Text = "Qtà Lotto std pr/ac."
    Me.lbAr_minord.Tooltip = ""
    Me.lbAr_minord.UseMnemonic = False
    '
    'edAr_minord
    '
    Me.edAr_minord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_minord.EditValue = "0"
    Me.edAr_minord.Location = New System.Drawing.Point(112, 101)
    Me.edAr_minord.Name = "edAr_minord"
    Me.edAr_minord.NTSDbField = ""
    Me.edAr_minord.NTSFormat = "0"
    Me.edAr_minord.NTSForzaVisZoom = False
    Me.edAr_minord.NTSOldValue = ""
    Me.edAr_minord.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_minord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_minord.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_minord.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_minord.Properties.AutoHeight = False
    Me.edAr_minord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_minord.Properties.MaxLength = 65536
    Me.edAr_minord.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_minord.Size = New System.Drawing.Size(89, 20)
    Me.edAr_minord.TabIndex = 607
    '
    'lbAr_polriord
    '
    Me.lbAr_polriord.AutoSize = True
    Me.lbAr_polriord.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_polriord.Location = New System.Drawing.Point(6, 79)
    Me.lbAr_polriord.Name = "lbAr_polriord"
    Me.lbAr_polriord.NTSDbField = ""
    Me.lbAr_polriord.Size = New System.Drawing.Size(90, 13)
    Me.lbAr_polriord.TabIndex = 604
    Me.lbAr_polriord.Text = "Politica di riordino"
    Me.lbAr_polriord.Tooltip = ""
    Me.lbAr_polriord.UseMnemonic = False
    '
    'cbAr_polriord
    '
    Me.cbAr_polriord.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_polriord.DataSource = Nothing
    Me.cbAr_polriord.DisplayMember = ""
    Me.cbAr_polriord.Location = New System.Drawing.Point(112, 76)
    Me.cbAr_polriord.Name = "cbAr_polriord"
    Me.cbAr_polriord.NTSDbField = ""
    Me.cbAr_polriord.Properties.AutoHeight = False
    Me.cbAr_polriord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_polriord.Properties.DropDownRows = 30
    Me.cbAr_polriord.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_polriord.SelectedValue = ""
    Me.cbAr_polriord.Size = New System.Drawing.Size(208, 20)
    Me.cbAr_polriord.TabIndex = 605
    Me.cbAr_polriord.ValueMember = ""
    '
    'edAr_forn
    '
    Me.edAr_forn.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_forn.EditValue = "0"
    Me.edAr_forn.Location = New System.Drawing.Point(112, 26)
    Me.edAr_forn.Name = "edAr_forn"
    Me.edAr_forn.NTSDbField = ""
    Me.edAr_forn.NTSFormat = "0"
    Me.edAr_forn.NTSForzaVisZoom = False
    Me.edAr_forn.NTSOldValue = ""
    Me.edAr_forn.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_forn.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_forn.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_forn.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_forn.Properties.AutoHeight = False
    Me.edAr_forn.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_forn.Properties.MaxLength = 65536
    Me.edAr_forn.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_forn.Size = New System.Drawing.Size(89, 20)
    Me.edAr_forn.TabIndex = 602
    '
    'lbAr_forn
    '
    Me.lbAr_forn.AutoSize = True
    Me.lbAr_forn.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_forn.Location = New System.Drawing.Point(6, 29)
    Me.lbAr_forn.Name = "lbAr_forn"
    Me.lbAr_forn.NTSDbField = ""
    Me.lbAr_forn.Size = New System.Drawing.Size(60, 13)
    Me.lbAr_forn.TabIndex = 600
    Me.lbAr_forn.Text = "Fornitore 1"
    Me.lbAr_forn.Tooltip = ""
    Me.lbAr_forn.UseMnemonic = False
    '
    'edAr_forn2
    '
    Me.edAr_forn2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_forn2.EditValue = "0"
    Me.edAr_forn2.Location = New System.Drawing.Point(112, 51)
    Me.edAr_forn2.Name = "edAr_forn2"
    Me.edAr_forn2.NTSDbField = ""
    Me.edAr_forn2.NTSFormat = "0"
    Me.edAr_forn2.NTSForzaVisZoom = False
    Me.edAr_forn2.NTSOldValue = ""
    Me.edAr_forn2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_forn2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_forn2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_forn2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_forn2.Properties.AutoHeight = False
    Me.edAr_forn2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_forn2.Properties.MaxLength = 65536
    Me.edAr_forn2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_forn2.Size = New System.Drawing.Size(89, 20)
    Me.edAr_forn2.TabIndex = 603
    '
    'lbAr_forn2
    '
    Me.lbAr_forn2.AutoSize = True
    Me.lbAr_forn2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_forn2.Location = New System.Drawing.Point(6, 54)
    Me.lbAr_forn2.Name = "lbAr_forn2"
    Me.lbAr_forn2.NTSDbField = ""
    Me.lbAr_forn2.Size = New System.Drawing.Size(60, 13)
    Me.lbAr_forn2.TabIndex = 601
    Me.lbAr_forn2.Text = "Fornitore 2"
    Me.lbAr_forn2.Tooltip = ""
    Me.lbAr_forn2.UseMnemonic = False
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Controls.Add(Me.pnTabpag3)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(755, 294)
    Me.NtsTabPage3.Text = "&3 - Altri dati"
    '
    'pnTabpag3
    '
    Me.pnTabpag3.AllowDrop = True
    Me.pnTabpag3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag3.Appearance.Options.UseBackColor = True
    Me.pnTabpag3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag3.Controls.Add(Me.pnTabpag3Right)
    Me.pnTabpag3.Controls.Add(Me.pnTabpag3Left)
    Me.pnTabpag3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag3.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag3.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag3.Name = "pnTabpag3"
    Me.pnTabpag3.NTSActiveTrasparency = True
    Me.pnTabpag3.Size = New System.Drawing.Size(755, 294)
    Me.pnTabpag3.TabIndex = 0
    Me.pnTabpag3.Text = "NtsPanel1"
    '
    'pnTabpag3Right
    '
    Me.pnTabpag3Right.AllowDrop = True
    Me.pnTabpag3Right.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag3Right.Appearance.Options.UseBackColor = True
    Me.pnTabpag3Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag3Right.Controls.Add(Me.lbXx_codvuo)
    Me.pnTabpag3Right.Controls.Add(Me.fmAltriDati)
    Me.pnTabpag3Right.Controls.Add(Me.lbAr_contriva)
    Me.pnTabpag3Right.Controls.Add(Me.edAr_contriva)
    Me.pnTabpag3Right.Controls.Add(Me.lbAr_codvuo)
    Me.pnTabpag3Right.Controls.Add(Me.edAr_codvuo)
    Me.pnTabpag3Right.Controls.Add(Me.lbAr_pesolor)
    Me.pnTabpag3Right.Controls.Add(Me.edAr_pesolor)
    Me.pnTabpag3Right.Controls.Add(Me.lbAr_pesonet)
    Me.pnTabpag3Right.Controls.Add(Me.edAr_pesonet)
    Me.pnTabpag3Right.Controls.Add(Me.lbAr_catlifo)
    Me.pnTabpag3Right.Controls.Add(Me.edAr_catlifo)
    Me.pnTabpag3Right.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag3Right.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag3Right.Location = New System.Drawing.Point(456, 0)
    Me.pnTabpag3Right.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag3Right.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag3Right.Name = "pnTabpag3Right"
    Me.pnTabpag3Right.NTSActiveTrasparency = True
    Me.pnTabpag3Right.Size = New System.Drawing.Size(299, 294)
    Me.pnTabpag3Right.TabIndex = 670
    Me.pnTabpag3Right.Text = "NtsPanel1"
    '
    'lbXx_codvuo
    '
    Me.lbXx_codvuo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codvuo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codvuo.Location = New System.Drawing.Point(152, 10)
    Me.lbXx_codvuo.Name = "lbXx_codvuo"
    Me.lbXx_codvuo.NTSDbField = ""
    Me.lbXx_codvuo.Size = New System.Drawing.Size(145, 20)
    Me.lbXx_codvuo.TabIndex = 657
    Me.lbXx_codvuo.Tooltip = ""
    Me.lbXx_codvuo.UseMnemonic = False
    '
    'fmAltriDati
    '
    Me.fmAltriDati.AllowDrop = True
    Me.fmAltriDati.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAltriDati.Appearance.Options.UseBackColor = True
    Me.fmAltriDati.Controls.Add(Me.ckAr_flgift)
    Me.fmAltriDati.Controls.Add(Me.ckAr_stainv)
    Me.fmAltriDati.Controls.Add(Me.ckAr_stasche)
    Me.fmAltriDati.Controls.Add(Me.ckAr_geslotti)
    Me.fmAltriDati.Controls.Add(Me.ckAr_inesaur)
    Me.fmAltriDati.Controls.Add(Me.ckAr_pesoca)
    Me.fmAltriDati.Controls.Add(Me.ckAr_stalist)
    Me.fmAltriDati.Controls.Add(Me.ckAr_gestmatr)
    Me.fmAltriDati.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAltriDati.Location = New System.Drawing.Point(9, 85)
    Me.fmAltriDati.Name = "fmAltriDati"
    Me.fmAltriDati.Size = New System.Drawing.Size(287, 203)
    Me.fmAltriDati.TabIndex = 656
    Me.fmAltriDati.Text = "OPZIONI VARIE"
    '
    'ckAr_flgift
    '
    Me.ckAr_flgift.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_flgift.Location = New System.Drawing.Point(9, 177)
    Me.ckAr_flgift.Name = "ckAr_flgift"
    Me.ckAr_flgift.NTSCheckValue = "S"
    Me.ckAr_flgift.NTSUnCheckValue = "N"
    Me.ckAr_flgift.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_flgift.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_flgift.Properties.AutoHeight = False
    Me.ckAr_flgift.Properties.Caption = "Gestione gift card"
    Me.ckAr_flgift.Size = New System.Drawing.Size(113, 19)
    Me.ckAr_flgift.TabIndex = 638
    '
    'ckAr_stainv
    '
    Me.ckAr_stainv.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_stainv.Location = New System.Drawing.Point(9, 67)
    Me.ckAr_stainv.Name = "ckAr_stainv"
    Me.ckAr_stainv.NTSCheckValue = "S"
    Me.ckAr_stainv.NTSUnCheckValue = "N"
    Me.ckAr_stainv.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_stainv.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_stainv.Properties.AutoHeight = False
    Me.ckAr_stainv.Properties.Caption = "Stampa articolo nell'inventario"
    Me.ckAr_stainv.Size = New System.Drawing.Size(172, 19)
    Me.ckAr_stainv.TabIndex = 629
    '
    'ckAr_stasche
    '
    Me.ckAr_stasche.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_stasche.Location = New System.Drawing.Point(9, 89)
    Me.ckAr_stasche.Name = "ckAr_stasche"
    Me.ckAr_stasche.NTSCheckValue = "S"
    Me.ckAr_stasche.NTSUnCheckValue = "N"
    Me.ckAr_stasche.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_stasche.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_stasche.Properties.AutoHeight = False
    Me.ckAr_stasche.Properties.Caption = "Stampa scheda articolo"
    Me.ckAr_stasche.Size = New System.Drawing.Size(137, 19)
    Me.ckAr_stasche.TabIndex = 630
    '
    'ckAr_geslotti
    '
    Me.ckAr_geslotti.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_geslotti.Location = New System.Drawing.Point(9, 111)
    Me.ckAr_geslotti.Name = "ckAr_geslotti"
    Me.ckAr_geslotti.NTSCheckValue = "S"
    Me.ckAr_geslotti.NTSUnCheckValue = "N"
    Me.ckAr_geslotti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_geslotti.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_geslotti.Properties.AutoHeight = False
    Me.ckAr_geslotti.Properties.Caption = "Gestione Lotti"
    Me.ckAr_geslotti.Size = New System.Drawing.Size(100, 19)
    Me.ckAr_geslotti.TabIndex = 631
    '
    'ckAr_inesaur
    '
    Me.ckAr_inesaur.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_inesaur.Location = New System.Drawing.Point(9, 133)
    Me.ckAr_inesaur.Name = "ckAr_inesaur"
    Me.ckAr_inesaur.NTSCheckValue = "S"
    Me.ckAr_inesaur.NTSUnCheckValue = "N"
    Me.ckAr_inesaur.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_inesaur.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_inesaur.Properties.AutoHeight = False
    Me.ckAr_inesaur.Properties.Caption = "In Esaurimento"
    Me.ckAr_inesaur.Size = New System.Drawing.Size(100, 19)
    Me.ckAr_inesaur.TabIndex = 632
    '
    'ckAr_pesoca
    '
    Me.ckAr_pesoca.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_pesoca.Location = New System.Drawing.Point(9, 23)
    Me.ckAr_pesoca.Name = "ckAr_pesoca"
    Me.ckAr_pesoca.NTSCheckValue = "S"
    Me.ckAr_pesoca.NTSUnCheckValue = "N"
    Me.ckAr_pesoca.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_pesoca.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_pesoca.Properties.AutoHeight = False
    Me.ckAr_pesoca.Properties.Caption = "Non proporre le note articolo sulle righe dei docum."
    Me.ckAr_pesoca.Size = New System.Drawing.Size(293, 19)
    Me.ckAr_pesoca.TabIndex = 635
    '
    'ckAr_stalist
    '
    Me.ckAr_stalist.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_stalist.Location = New System.Drawing.Point(9, 45)
    Me.ckAr_stalist.Name = "ckAr_stalist"
    Me.ckAr_stalist.NTSCheckValue = "S"
    Me.ckAr_stalist.NTSUnCheckValue = "N"
    Me.ckAr_stalist.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_stalist.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_stalist.Properties.AutoHeight = False
    Me.ckAr_stalist.Properties.Caption = "Stampa articolo nel listino"
    Me.ckAr_stalist.Size = New System.Drawing.Size(147, 19)
    Me.ckAr_stalist.TabIndex = 637
    '
    'ckAr_gestmatr
    '
    Me.ckAr_gestmatr.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_gestmatr.Location = New System.Drawing.Point(9, 155)
    Me.ckAr_gestmatr.Name = "ckAr_gestmatr"
    Me.ckAr_gestmatr.NTSCheckValue = "S"
    Me.ckAr_gestmatr.NTSUnCheckValue = "N"
    Me.ckAr_gestmatr.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_gestmatr.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_gestmatr.Properties.AutoHeight = False
    Me.ckAr_gestmatr.Properties.Caption = "Gestione matricole"
    Me.ckAr_gestmatr.Size = New System.Drawing.Size(113, 19)
    Me.ckAr_gestmatr.TabIndex = 636
    '
    'lbAr_contriva
    '
    Me.lbAr_contriva.AutoSize = True
    Me.lbAr_contriva.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_contriva.Location = New System.Drawing.Point(6, 37)
    Me.lbAr_contriva.Name = "lbAr_contriva"
    Me.lbAr_contriva.NTSDbField = ""
    Me.lbAr_contriva.Size = New System.Drawing.Size(70, 13)
    Me.lbAr_contriva.TabIndex = 652
    Me.lbAr_contriva.Text = "Controp. IVA"
    Me.lbAr_contriva.Tooltip = ""
    Me.lbAr_contriva.UseMnemonic = False
    '
    'edAr_contriva
    '
    Me.edAr_contriva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_contriva.EditValue = ""
    Me.edAr_contriva.Location = New System.Drawing.Point(78, 34)
    Me.edAr_contriva.Name = "edAr_contriva"
    Me.edAr_contriva.NTSDbField = ""
    Me.edAr_contriva.NTSForzaVisZoom = False
    Me.edAr_contriva.NTSOldValue = ""
    Me.edAr_contriva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_contriva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_contriva.Properties.AutoHeight = False
    Me.edAr_contriva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_contriva.Properties.MaxLength = 65536
    Me.edAr_contriva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_contriva.Size = New System.Drawing.Size(68, 20)
    Me.edAr_contriva.TabIndex = 654
    '
    'lbAr_codvuo
    '
    Me.lbAr_codvuo.AutoSize = True
    Me.lbAr_codvuo.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codvuo.Location = New System.Drawing.Point(6, 14)
    Me.lbAr_codvuo.Name = "lbAr_codvuo"
    Me.lbAr_codvuo.NTSDbField = ""
    Me.lbAr_codvuo.Size = New System.Drawing.Size(35, 13)
    Me.lbAr_codvuo.TabIndex = 653
    Me.lbAr_codvuo.Text = "Vuoto"
    Me.lbAr_codvuo.Tooltip = ""
    Me.lbAr_codvuo.UseMnemonic = False
    '
    'edAr_codvuo
    '
    Me.edAr_codvuo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codvuo.EditValue = "0"
    Me.edAr_codvuo.Location = New System.Drawing.Point(78, 10)
    Me.edAr_codvuo.Name = "edAr_codvuo"
    Me.edAr_codvuo.NTSDbField = ""
    Me.edAr_codvuo.NTSFormat = "0"
    Me.edAr_codvuo.NTSForzaVisZoom = False
    Me.edAr_codvuo.NTSOldValue = ""
    Me.edAr_codvuo.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_codvuo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_codvuo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codvuo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codvuo.Properties.AutoHeight = False
    Me.edAr_codvuo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codvuo.Properties.MaxLength = 65536
    Me.edAr_codvuo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codvuo.Size = New System.Drawing.Size(68, 20)
    Me.edAr_codvuo.TabIndex = 655
    '
    'lbAr_pesolor
    '
    Me.lbAr_pesolor.AutoSize = True
    Me.lbAr_pesolor.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_pesolor.Location = New System.Drawing.Point(6, 63)
    Me.lbAr_pesolor.Name = "lbAr_pesolor"
    Me.lbAr_pesolor.NTSDbField = ""
    Me.lbAr_pesolor.Size = New System.Drawing.Size(57, 13)
    Me.lbAr_pesolor.TabIndex = 648
    Me.lbAr_pesolor.Text = "Peso lordo"
    Me.lbAr_pesolor.Tooltip = ""
    Me.lbAr_pesolor.UseMnemonic = False
    '
    'edAr_pesolor
    '
    Me.edAr_pesolor.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_pesolor.EditValue = "0"
    Me.edAr_pesolor.Location = New System.Drawing.Point(78, 59)
    Me.edAr_pesolor.Name = "edAr_pesolor"
    Me.edAr_pesolor.NTSDbField = ""
    Me.edAr_pesolor.NTSFormat = "0"
    Me.edAr_pesolor.NTSForzaVisZoom = False
    Me.edAr_pesolor.NTSOldValue = ""
    Me.edAr_pesolor.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_pesolor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_pesolor.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_pesolor.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_pesolor.Properties.AutoHeight = False
    Me.edAr_pesolor.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_pesolor.Properties.MaxLength = 65536
    Me.edAr_pesolor.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_pesolor.Size = New System.Drawing.Size(68, 20)
    Me.edAr_pesolor.TabIndex = 650
    '
    'lbAr_pesonet
    '
    Me.lbAr_pesonet.AutoSize = True
    Me.lbAr_pesonet.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_pesonet.Location = New System.Drawing.Point(152, 62)
    Me.lbAr_pesonet.Name = "lbAr_pesonet"
    Me.lbAr_pesonet.NTSDbField = ""
    Me.lbAr_pesonet.Size = New System.Drawing.Size(59, 13)
    Me.lbAr_pesonet.TabIndex = 649
    Me.lbAr_pesonet.Text = "Peso netto"
    Me.lbAr_pesonet.Tooltip = ""
    Me.lbAr_pesonet.UseMnemonic = False
    '
    'edAr_pesonet
    '
    Me.edAr_pesonet.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAr_pesonet.EditValue = "0"
    Me.edAr_pesonet.Location = New System.Drawing.Point(252, 59)
    Me.edAr_pesonet.Name = "edAr_pesonet"
    Me.edAr_pesonet.NTSDbField = ""
    Me.edAr_pesonet.NTSFormat = "0"
    Me.edAr_pesonet.NTSForzaVisZoom = False
    Me.edAr_pesonet.NTSOldValue = ""
    Me.edAr_pesonet.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_pesonet.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_pesonet.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_pesonet.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_pesonet.Properties.AutoHeight = False
    Me.edAr_pesonet.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_pesonet.Properties.MaxLength = 65536
    Me.edAr_pesonet.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_pesonet.Size = New System.Drawing.Size(46, 20)
    Me.edAr_pesonet.TabIndex = 651
    '
    'lbAr_catlifo
    '
    Me.lbAr_catlifo.AutoSize = True
    Me.lbAr_catlifo.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_catlifo.Location = New System.Drawing.Point(152, 37)
    Me.lbAr_catlifo.Name = "lbAr_catlifo"
    Me.lbAr_catlifo.NTSDbField = ""
    Me.lbAr_catlifo.Size = New System.Drawing.Size(88, 13)
    Me.lbAr_catlifo.TabIndex = 646
    Me.lbAr_catlifo.Text = "Coeffic. ass. c.f."
    Me.lbAr_catlifo.Tooltip = ""
    Me.lbAr_catlifo.UseMnemonic = False
    '
    'edAr_catlifo
    '
    Me.edAr_catlifo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_catlifo.EditValue = "0"
    Me.edAr_catlifo.Location = New System.Drawing.Point(252, 34)
    Me.edAr_catlifo.Name = "edAr_catlifo"
    Me.edAr_catlifo.NTSDbField = ""
    Me.edAr_catlifo.NTSFormat = "0"
    Me.edAr_catlifo.NTSForzaVisZoom = False
    Me.edAr_catlifo.NTSOldValue = ""
    Me.edAr_catlifo.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_catlifo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_catlifo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_catlifo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_catlifo.Properties.AutoHeight = False
    Me.edAr_catlifo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_catlifo.Properties.MaxLength = 65536
    Me.edAr_catlifo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_catlifo.Size = New System.Drawing.Size(45, 20)
    Me.edAr_catlifo.TabIndex = 647
    '
    'pnTabpag3Left
    '
    Me.pnTabpag3Left.AllowDrop = True
    Me.pnTabpag3Left.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag3Left.Appearance.Options.UseBackColor = True
    Me.pnTabpag3Left.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag3Left.Controls.Add(Me.cmdClassificaDeleteFilter)
    Me.pnTabpag3Left.Controls.Add(Me.cmdClassifica)
    Me.pnTabpag3Left.Controls.Add(Me.lbClassifica)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_clascon)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_claprov)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_claprov)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_claprov)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_clascon)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_clascon)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_codiva)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_gruppo)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_sotgru)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_controp)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_controa)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_contros)
    Me.pnTabpag3Left.Controls.Add(Me.lbXx_famprod)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_famprod)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_famprod)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_contros)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_contros)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_codiva)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_codiva)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_gruppo)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_gruppo)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_sotgru)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_sotgru)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_controp)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_controp)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_controa)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_controa)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_tipo)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_tipo)
    Me.pnTabpag3Left.Controls.Add(Me.lbAr_ubicaz)
    Me.pnTabpag3Left.Controls.Add(Me.edAr_ubicaz)
    Me.pnTabpag3Left.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag3Left.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnTabpag3Left.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag3Left.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag3Left.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag3Left.Name = "pnTabpag3Left"
    Me.pnTabpag3Left.NTSActiveTrasparency = True
    Me.pnTabpag3Left.Size = New System.Drawing.Size(456, 294)
    Me.pnTabpag3Left.TabIndex = 646
    Me.pnTabpag3Left.Text = "NtsPanel1"
    '
    'cmdClassificaDeleteFilter
    '
    Me.cmdClassificaDeleteFilter.Image = CType(resources.GetObject("cmdClassificaDeleteFilter.Image"), System.Drawing.Image)
    Me.cmdClassificaDeleteFilter.ImagePath = ""
    Me.cmdClassificaDeleteFilter.ImageText = ""
    Me.cmdClassificaDeleteFilter.Location = New System.Drawing.Point(69, 269)
    Me.cmdClassificaDeleteFilter.Name = "cmdClassificaDeleteFilter"
    Me.cmdClassificaDeleteFilter.NTSContextMenu = Nothing
    Me.cmdClassificaDeleteFilter.Size = New System.Drawing.Size(28, 22)
    Me.cmdClassificaDeleteFilter.TabIndex = 680
    Me.cmdClassificaDeleteFilter.ToolTip = "Rimuovi la classificazione dall'articolo"
    '
    'cmdClassifica
    '
    Me.cmdClassifica.ImagePath = ""
    Me.cmdClassifica.ImageText = ""
    Me.cmdClassifica.Location = New System.Drawing.Point(9, 244)
    Me.cmdClassifica.Name = "cmdClassifica"
    Me.cmdClassifica.NTSContextMenu = Nothing
    Me.cmdClassifica.Size = New System.Drawing.Size(88, 22)
    Me.cmdClassifica.TabIndex = 677
    Me.cmdClassifica.Text = "Classifica"
    '
    'lbClassifica
    '
    Me.lbClassifica.BackColor = System.Drawing.Color.Transparent
    Me.lbClassifica.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbClassifica.Location = New System.Drawing.Point(100, 244)
    Me.lbClassifica.Name = "lbClassifica"
    Me.lbClassifica.NTSDbField = ""
    Me.lbClassifica.Size = New System.Drawing.Size(350, 47)
    Me.lbClassifica.TabIndex = 676
    Me.lbClassifica.Tooltip = ""
    Me.lbClassifica.UseMnemonic = False
    '
    'lbXx_clascon
    '
    Me.lbXx_clascon.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_clascon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_clascon.Location = New System.Drawing.Point(200, 195)
    Me.lbXx_clascon.Name = "lbXx_clascon"
    Me.lbXx_clascon.NTSDbField = ""
    Me.lbXx_clascon.Size = New System.Drawing.Size(250, 20)
    Me.lbXx_clascon.TabIndex = 674
    Me.lbXx_clascon.Tooltip = ""
    Me.lbXx_clascon.UseMnemonic = False
    '
    'lbXx_claprov
    '
    Me.lbXx_claprov.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_claprov.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_claprov.Location = New System.Drawing.Point(200, 218)
    Me.lbXx_claprov.Name = "lbXx_claprov"
    Me.lbXx_claprov.NTSDbField = ""
    Me.lbXx_claprov.Size = New System.Drawing.Size(250, 20)
    Me.lbXx_claprov.TabIndex = 675
    Me.lbXx_claprov.Tooltip = ""
    Me.lbXx_claprov.UseMnemonic = False
    '
    'lbAr_claprov
    '
    Me.lbAr_claprov.AutoSize = True
    Me.lbAr_claprov.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_claprov.Location = New System.Drawing.Point(6, 221)
    Me.lbAr_claprov.Name = "lbAr_claprov"
    Me.lbAr_claprov.NTSDbField = ""
    Me.lbAr_claprov.Size = New System.Drawing.Size(97, 13)
    Me.lbAr_claprov.TabIndex = 672
    Me.lbAr_claprov.Text = "Classe provvigione"
    Me.lbAr_claprov.Tooltip = ""
    Me.lbAr_claprov.UseMnemonic = False
    '
    'edAr_claprov
    '
    Me.edAr_claprov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_claprov.EditValue = "0"
    Me.edAr_claprov.Location = New System.Drawing.Point(130, 218)
    Me.edAr_claprov.Name = "edAr_claprov"
    Me.edAr_claprov.NTSDbField = ""
    Me.edAr_claprov.NTSFormat = "0"
    Me.edAr_claprov.NTSForzaVisZoom = False
    Me.edAr_claprov.NTSOldValue = ""
    Me.edAr_claprov.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_claprov.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_claprov.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_claprov.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_claprov.Properties.AutoHeight = False
    Me.edAr_claprov.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_claprov.Properties.MaxLength = 65536
    Me.edAr_claprov.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_claprov.Size = New System.Drawing.Size(64, 20)
    Me.edAr_claprov.TabIndex = 673
    '
    'lbAr_clascon
    '
    Me.lbAr_clascon.AutoSize = True
    Me.lbAr_clascon.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_clascon.Location = New System.Drawing.Point(6, 198)
    Me.lbAr_clascon.Name = "lbAr_clascon"
    Me.lbAr_clascon.NTSDbField = ""
    Me.lbAr_clascon.Size = New System.Drawing.Size(84, 13)
    Me.lbAr_clascon.TabIndex = 670
    Me.lbAr_clascon.Text = "Classe di sconto"
    Me.lbAr_clascon.Tooltip = ""
    Me.lbAr_clascon.UseMnemonic = False
    '
    'edAr_clascon
    '
    Me.edAr_clascon.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_clascon.EditValue = "0"
    Me.edAr_clascon.Location = New System.Drawing.Point(130, 195)
    Me.edAr_clascon.Name = "edAr_clascon"
    Me.edAr_clascon.NTSDbField = ""
    Me.edAr_clascon.NTSFormat = "0"
    Me.edAr_clascon.NTSForzaVisZoom = False
    Me.edAr_clascon.NTSOldValue = ""
    Me.edAr_clascon.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_clascon.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_clascon.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_clascon.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_clascon.Properties.AutoHeight = False
    Me.edAr_clascon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_clascon.Properties.MaxLength = 65536
    Me.edAr_clascon.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_clascon.Size = New System.Drawing.Size(64, 20)
    Me.edAr_clascon.TabIndex = 671
    '
    'lbXx_codiva
    '
    Me.lbXx_codiva.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codiva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codiva.Location = New System.Drawing.Point(200, 34)
    Me.lbXx_codiva.Name = "lbXx_codiva"
    Me.lbXx_codiva.NTSDbField = ""
    Me.lbXx_codiva.Size = New System.Drawing.Size(250, 20)
    Me.lbXx_codiva.TabIndex = 663
    Me.lbXx_codiva.Tooltip = ""
    Me.lbXx_codiva.UseMnemonic = False
    '
    'lbXx_gruppo
    '
    Me.lbXx_gruppo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_gruppo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_gruppo.Location = New System.Drawing.Point(200, 57)
    Me.lbXx_gruppo.Name = "lbXx_gruppo"
    Me.lbXx_gruppo.NTSDbField = ""
    Me.lbXx_gruppo.Size = New System.Drawing.Size(250, 20)
    Me.lbXx_gruppo.TabIndex = 664
    Me.lbXx_gruppo.Tooltip = ""
    Me.lbXx_gruppo.UseMnemonic = False
    '
    'lbXx_sotgru
    '
    Me.lbXx_sotgru.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_sotgru.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_sotgru.Location = New System.Drawing.Point(200, 80)
    Me.lbXx_sotgru.Name = "lbXx_sotgru"
    Me.lbXx_sotgru.NTSDbField = ""
    Me.lbXx_sotgru.Size = New System.Drawing.Size(250, 20)
    Me.lbXx_sotgru.TabIndex = 665
    Me.lbXx_sotgru.Tooltip = ""
    Me.lbXx_sotgru.UseMnemonic = False
    '
    'lbXx_controp
    '
    Me.lbXx_controp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_controp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_controp.Location = New System.Drawing.Point(200, 103)
    Me.lbXx_controp.Name = "lbXx_controp"
    Me.lbXx_controp.NTSDbField = ""
    Me.lbXx_controp.Size = New System.Drawing.Size(250, 20)
    Me.lbXx_controp.TabIndex = 666
    Me.lbXx_controp.Tooltip = ""
    Me.lbXx_controp.UseMnemonic = False
    '
    'lbXx_controa
    '
    Me.lbXx_controa.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_controa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_controa.Location = New System.Drawing.Point(200, 126)
    Me.lbXx_controa.Name = "lbXx_controa"
    Me.lbXx_controa.NTSDbField = ""
    Me.lbXx_controa.Size = New System.Drawing.Size(250, 20)
    Me.lbXx_controa.TabIndex = 667
    Me.lbXx_controa.Tooltip = ""
    Me.lbXx_controa.UseMnemonic = False
    '
    'lbXx_contros
    '
    Me.lbXx_contros.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_contros.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_contros.Location = New System.Drawing.Point(200, 149)
    Me.lbXx_contros.Name = "lbXx_contros"
    Me.lbXx_contros.NTSDbField = ""
    Me.lbXx_contros.Size = New System.Drawing.Size(250, 20)
    Me.lbXx_contros.TabIndex = 668
    Me.lbXx_contros.Tooltip = ""
    Me.lbXx_contros.UseMnemonic = False
    '
    'lbXx_famprod
    '
    Me.lbXx_famprod.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_famprod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_famprod.Location = New System.Drawing.Point(200, 172)
    Me.lbXx_famprod.Name = "lbXx_famprod"
    Me.lbXx_famprod.NTSDbField = ""
    Me.lbXx_famprod.Size = New System.Drawing.Size(250, 20)
    Me.lbXx_famprod.TabIndex = 669
    Me.lbXx_famprod.Tooltip = ""
    Me.lbXx_famprod.UseMnemonic = False
    '
    'lbAr_famprod
    '
    Me.lbAr_famprod.AutoSize = True
    Me.lbAr_famprod.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_famprod.Location = New System.Drawing.Point(6, 177)
    Me.lbAr_famprod.Name = "lbAr_famprod"
    Me.lbAr_famprod.NTSDbField = ""
    Me.lbAr_famprod.Size = New System.Drawing.Size(45, 13)
    Me.lbAr_famprod.TabIndex = 661
    Me.lbAr_famprod.Text = "Famiglia"
    Me.lbAr_famprod.Tooltip = ""
    Me.lbAr_famprod.UseMnemonic = False
    '
    'edAr_famprod
    '
    Me.edAr_famprod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_famprod.EditValue = ""
    Me.edAr_famprod.Location = New System.Drawing.Point(130, 172)
    Me.edAr_famprod.Name = "edAr_famprod"
    Me.edAr_famprod.NTSDbField = ""
    Me.edAr_famprod.NTSForzaVisZoom = False
    Me.edAr_famprod.NTSOldValue = ""
    Me.edAr_famprod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_famprod.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_famprod.Properties.AutoHeight = False
    Me.edAr_famprod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_famprod.Properties.MaxLength = 65536
    Me.edAr_famprod.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_famprod.Size = New System.Drawing.Size(64, 20)
    Me.edAr_famprod.TabIndex = 662
    '
    'lbAr_contros
    '
    Me.lbAr_contros.AutoSize = True
    Me.lbAr_contros.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_contros.Location = New System.Drawing.Point(6, 152)
    Me.lbAr_contros.Name = "lbAr_contros"
    Me.lbAr_contros.NTSDbField = ""
    Me.lbAr_contros.Size = New System.Drawing.Size(117, 13)
    Me.lbAr_contros.TabIndex = 659
    Me.lbAr_contros.Text = "Controp. scar. produz."
    Me.lbAr_contros.Tooltip = ""
    Me.lbAr_contros.UseMnemonic = False
    '
    'edAr_contros
    '
    Me.edAr_contros.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_contros.EditValue = "0"
    Me.edAr_contros.Location = New System.Drawing.Point(130, 149)
    Me.edAr_contros.Name = "edAr_contros"
    Me.edAr_contros.NTSDbField = ""
    Me.edAr_contros.NTSFormat = "0"
    Me.edAr_contros.NTSForzaVisZoom = False
    Me.edAr_contros.NTSOldValue = ""
    Me.edAr_contros.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_contros.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_contros.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_contros.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_contros.Properties.AutoHeight = False
    Me.edAr_contros.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_contros.Properties.MaxLength = 65536
    Me.edAr_contros.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_contros.Size = New System.Drawing.Size(64, 20)
    Me.edAr_contros.TabIndex = 660
    '
    'lbAr_codiva
    '
    Me.lbAr_codiva.AutoSize = True
    Me.lbAr_codiva.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codiva.Location = New System.Drawing.Point(6, 37)
    Me.lbAr_codiva.Name = "lbAr_codiva"
    Me.lbAr_codiva.NTSDbField = ""
    Me.lbAr_codiva.Size = New System.Drawing.Size(59, 13)
    Me.lbAr_codiva.TabIndex = 649
    Me.lbAr_codiva.Text = "Codice IVA"
    Me.lbAr_codiva.Tooltip = ""
    Me.lbAr_codiva.UseMnemonic = False
    '
    'edAr_codiva
    '
    Me.edAr_codiva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codiva.EditValue = "0"
    Me.edAr_codiva.Location = New System.Drawing.Point(130, 34)
    Me.edAr_codiva.Name = "edAr_codiva"
    Me.edAr_codiva.NTSDbField = ""
    Me.edAr_codiva.NTSFormat = "0"
    Me.edAr_codiva.NTSForzaVisZoom = False
    Me.edAr_codiva.NTSOldValue = ""
    Me.edAr_codiva.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_codiva.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_codiva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codiva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codiva.Properties.AutoHeight = False
    Me.edAr_codiva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codiva.Properties.MaxLength = 65536
    Me.edAr_codiva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codiva.Size = New System.Drawing.Size(64, 20)
    Me.edAr_codiva.TabIndex = 654
    '
    'lbAr_gruppo
    '
    Me.lbAr_gruppo.AutoSize = True
    Me.lbAr_gruppo.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gruppo.Location = New System.Drawing.Point(6, 59)
    Me.lbAr_gruppo.Name = "lbAr_gruppo"
    Me.lbAr_gruppo.NTSDbField = ""
    Me.lbAr_gruppo.Size = New System.Drawing.Size(107, 13)
    Me.lbAr_gruppo.TabIndex = 650
    Me.lbAr_gruppo.Text = "Gruppo merceologico"
    Me.lbAr_gruppo.Tooltip = ""
    Me.lbAr_gruppo.UseMnemonic = False
    '
    'edAr_gruppo
    '
    Me.edAr_gruppo.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAr_gruppo.EditValue = "0"
    Me.edAr_gruppo.Location = New System.Drawing.Point(130, 57)
    Me.edAr_gruppo.Name = "edAr_gruppo"
    Me.edAr_gruppo.NTSDbField = ""
    Me.edAr_gruppo.NTSFormat = "0"
    Me.edAr_gruppo.NTSForzaVisZoom = False
    Me.edAr_gruppo.NTSOldValue = ""
    Me.edAr_gruppo.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_gruppo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_gruppo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_gruppo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_gruppo.Properties.AutoHeight = False
    Me.edAr_gruppo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_gruppo.Properties.MaxLength = 65536
    Me.edAr_gruppo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_gruppo.Size = New System.Drawing.Size(64, 20)
    Me.edAr_gruppo.TabIndex = 655
    '
    'lbAr_sotgru
    '
    Me.lbAr_sotgru.AutoSize = True
    Me.lbAr_sotgru.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_sotgru.Location = New System.Drawing.Point(6, 83)
    Me.lbAr_sotgru.Name = "lbAr_sotgru"
    Me.lbAr_sotgru.NTSDbField = ""
    Me.lbAr_sotgru.Size = New System.Drawing.Size(111, 13)
    Me.lbAr_sotgru.TabIndex = 651
    Me.lbAr_sotgru.Text = "Sottogruppo merceol."
    Me.lbAr_sotgru.Tooltip = ""
    Me.lbAr_sotgru.UseMnemonic = False
    '
    'edAr_sotgru
    '
    Me.edAr_sotgru.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAr_sotgru.EditValue = "0"
    Me.edAr_sotgru.Location = New System.Drawing.Point(130, 80)
    Me.edAr_sotgru.Name = "edAr_sotgru"
    Me.edAr_sotgru.NTSDbField = ""
    Me.edAr_sotgru.NTSFormat = "0"
    Me.edAr_sotgru.NTSForzaVisZoom = False
    Me.edAr_sotgru.NTSOldValue = ""
    Me.edAr_sotgru.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_sotgru.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_sotgru.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_sotgru.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_sotgru.Properties.AutoHeight = False
    Me.edAr_sotgru.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_sotgru.Properties.MaxLength = 65536
    Me.edAr_sotgru.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_sotgru.Size = New System.Drawing.Size(64, 20)
    Me.edAr_sotgru.TabIndex = 656
    '
    'lbAr_controp
    '
    Me.lbAr_controp.AutoSize = True
    Me.lbAr_controp.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_controp.Location = New System.Drawing.Point(6, 106)
    Me.lbAr_controp.Name = "lbAr_controp"
    Me.lbAr_controp.NTSDbField = ""
    Me.lbAr_controp.Size = New System.Drawing.Size(111, 13)
    Me.lbAr_controp.TabIndex = 652
    Me.lbAr_controp.Text = "Contropartita vendite"
    Me.lbAr_controp.Tooltip = ""
    Me.lbAr_controp.UseMnemonic = False
    '
    'edAr_controp
    '
    Me.edAr_controp.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAr_controp.EditValue = "0"
    Me.edAr_controp.Location = New System.Drawing.Point(130, 103)
    Me.edAr_controp.Name = "edAr_controp"
    Me.edAr_controp.NTSDbField = ""
    Me.edAr_controp.NTSFormat = "0"
    Me.edAr_controp.NTSForzaVisZoom = False
    Me.edAr_controp.NTSOldValue = ""
    Me.edAr_controp.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_controp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_controp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_controp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_controp.Properties.AutoHeight = False
    Me.edAr_controp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_controp.Properties.MaxLength = 65536
    Me.edAr_controp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_controp.Size = New System.Drawing.Size(64, 20)
    Me.edAr_controp.TabIndex = 657
    '
    'lbAr_controa
    '
    Me.lbAr_controa.AutoSize = True
    Me.lbAr_controa.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_controa.Location = New System.Drawing.Point(6, 129)
    Me.lbAr_controa.Name = "lbAr_controa"
    Me.lbAr_controa.NTSDbField = ""
    Me.lbAr_controa.Size = New System.Drawing.Size(111, 13)
    Me.lbAr_controa.TabIndex = 653
    Me.lbAr_controa.Text = "Contropartita acquisti"
    Me.lbAr_controa.Tooltip = ""
    Me.lbAr_controa.UseMnemonic = False
    '
    'edAr_controa
    '
    Me.edAr_controa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_controa.EditValue = "0"
    Me.edAr_controa.Location = New System.Drawing.Point(130, 126)
    Me.edAr_controa.Name = "edAr_controa"
    Me.edAr_controa.NTSDbField = ""
    Me.edAr_controa.NTSFormat = "0"
    Me.edAr_controa.NTSForzaVisZoom = False
    Me.edAr_controa.NTSOldValue = ""
    Me.edAr_controa.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_controa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_controa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_controa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_controa.Properties.AutoHeight = False
    Me.edAr_controa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_controa.Properties.MaxLength = 65536
    Me.edAr_controa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_controa.Size = New System.Drawing.Size(64, 20)
    Me.edAr_controa.TabIndex = 658
    '
    'lbAr_tipo
    '
    Me.lbAr_tipo.AutoSize = True
    Me.lbAr_tipo.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_tipo.Location = New System.Drawing.Point(6, 14)
    Me.lbAr_tipo.Name = "lbAr_tipo"
    Me.lbAr_tipo.NTSDbField = ""
    Me.lbAr_tipo.Size = New System.Drawing.Size(27, 13)
    Me.lbAr_tipo.TabIndex = 645
    Me.lbAr_tipo.Text = "Tipo"
    Me.lbAr_tipo.Tooltip = ""
    Me.lbAr_tipo.UseMnemonic = False
    '
    'edAr_tipo
    '
    Me.edAr_tipo.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAr_tipo.EditValue = ""
    Me.edAr_tipo.Location = New System.Drawing.Point(130, 11)
    Me.edAr_tipo.Name = "edAr_tipo"
    Me.edAr_tipo.NTSDbField = ""
    Me.edAr_tipo.NTSForzaVisZoom = False
    Me.edAr_tipo.NTSOldValue = ""
    Me.edAr_tipo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_tipo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_tipo.Properties.AutoHeight = False
    Me.edAr_tipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_tipo.Properties.MaxLength = 65536
    Me.edAr_tipo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_tipo.Size = New System.Drawing.Size(64, 20)
    Me.edAr_tipo.TabIndex = 647
    '
    'lbAr_ubicaz
    '
    Me.lbAr_ubicaz.AutoSize = True
    Me.lbAr_ubicaz.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_ubicaz.Location = New System.Drawing.Point(200, 14)
    Me.lbAr_ubicaz.Name = "lbAr_ubicaz"
    Me.lbAr_ubicaz.NTSDbField = ""
    Me.lbAr_ubicaz.Size = New System.Drawing.Size(58, 13)
    Me.lbAr_ubicaz.TabIndex = 646
    Me.lbAr_ubicaz.Text = "Ubicazione"
    Me.lbAr_ubicaz.Tooltip = ""
    Me.lbAr_ubicaz.UseMnemonic = False
    '
    'edAr_ubicaz
    '
    Me.edAr_ubicaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ubicaz.EditValue = ""
    Me.edAr_ubicaz.Location = New System.Drawing.Point(264, 11)
    Me.edAr_ubicaz.Name = "edAr_ubicaz"
    Me.edAr_ubicaz.NTSDbField = ""
    Me.edAr_ubicaz.NTSForzaVisZoom = False
    Me.edAr_ubicaz.NTSOldValue = ""
    Me.edAr_ubicaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_ubicaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_ubicaz.Properties.AutoHeight = False
    Me.edAr_ubicaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_ubicaz.Properties.MaxLength = 65536
    Me.edAr_ubicaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_ubicaz.Size = New System.Drawing.Size(186, 20)
    Me.edAr_ubicaz.TabIndex = 648
    '
    'NtsTabPage4
    '
    Me.NtsTabPage4.AllowDrop = True
    Me.NtsTabPage4.Controls.Add(Me.pnTabpag4)
    Me.NtsTabPage4.Enable = True
    Me.NtsTabPage4.Name = "NtsTabPage4"
    Me.NtsTabPage4.Size = New System.Drawing.Size(755, 294)
    Me.NtsTabPage4.Text = "&4 - Listini"
    '
    'pnTabpag4
    '
    Me.pnTabpag4.AllowDrop = True
    Me.pnTabpag4.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag4.Appearance.Options.UseBackColor = True
    Me.pnTabpag4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag4.Controls.Add(Me.ceListini)
    Me.pnTabpag4.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag4.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag4.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag4.Name = "pnTabpag4"
    Me.pnTabpag4.NTSActiveTrasparency = True
    Me.pnTabpag4.Size = New System.Drawing.Size(755, 294)
    Me.pnTabpag4.TabIndex = 0
    Me.pnTabpag4.Text = "NtsPanel1"
    '
    'ceListini
    '
    Me.ceListini.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceListini.LcCodart = ""
    Me.ceListini.LcCodartRoot = ""
    Me.ceListini.LcConto = 0
    Me.ceListini.LcFaseArticolo = 0
    Me.ceListini.LcTipo = ""
    Me.ceListini.Location = New System.Drawing.Point(0, 0)
    Me.ceListini.MinimumSize = New System.Drawing.Size(504, 294)
    Me.ceListini.Name = "ceListini"
    Me.ceListini.Size = New System.Drawing.Size(755, 294)
    Me.ceListini.strNomeCampo = ""
    Me.ceListini.TabIndex = 0
    '
    'NtsTabPage5
    '
    Me.NtsTabPage5.AllowDrop = True
    Me.NtsTabPage5.Controls.Add(Me.pnTabpag5)
    Me.NtsTabPage5.Enable = True
    Me.NtsTabPage5.Name = "NtsTabPage5"
    Me.NtsTabPage5.Size = New System.Drawing.Size(755, 294)
    Me.NtsTabPage5.Text = "&5 - Sconti"
    '
    'pnTabpag5
    '
    Me.pnTabpag5.AllowDrop = True
    Me.pnTabpag5.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag5.Appearance.Options.UseBackColor = True
    Me.pnTabpag5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag5.Controls.Add(Me.ceSconti)
    Me.pnTabpag5.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag5.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag5.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag5.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag5.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag5.Name = "pnTabpag5"
    Me.pnTabpag5.NTSActiveTrasparency = True
    Me.pnTabpag5.Size = New System.Drawing.Size(755, 294)
    Me.pnTabpag5.TabIndex = 0
    Me.pnTabpag5.Text = "NtsPanel1"
    '
    'ceSconti
    '
    Me.ceSconti.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceSconti.GridColumn1_954_20 = Nothing
    Me.ceSconti.Location = New System.Drawing.Point(0, 0)
    Me.ceSconti.MinimumSize = New System.Drawing.Size(504, 294)
    Me.ceSconti.Name = "ceSconti"
    Me.ceSconti.Size = New System.Drawing.Size(755, 294)
    Me.ceSconti.SoClasseArt = 0
    Me.ceSconti.SoClasseCli = 0
    Me.ceSconti.SoCodart = ""
    Me.ceSconti.SoCodartRoot = ""
    Me.ceSconti.SoConto = 0
    Me.ceSconti.strNomeCampo = ""
    Me.ceSconti.TabIndex = 0
    Me.ceSconti.TipoSconto = 0
    '
    'NtsTabPage6
    '
    Me.NtsTabPage6.AllowDrop = True
    Me.NtsTabPage6.Controls.Add(Me.pnTabpag6)
    Me.NtsTabPage6.Enable = True
    Me.NtsTabPage6.Name = "NtsTabPage6"
    Me.NtsTabPage6.Size = New System.Drawing.Size(755, 294)
    Me.NtsTabPage6.Text = "&6 - Provvigioni"
    '
    'pnTabpag6
    '
    Me.pnTabpag6.AllowDrop = True
    Me.pnTabpag6.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag6.Appearance.Options.UseBackColor = True
    Me.pnTabpag6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag6.Controls.Add(Me.ceProvvig)
    Me.pnTabpag6.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag6.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag6.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag6.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag6.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag6.Name = "pnTabpag6"
    Me.pnTabpag6.NTSActiveTrasparency = True
    Me.pnTabpag6.Size = New System.Drawing.Size(755, 294)
    Me.pnTabpag6.TabIndex = 0
    Me.pnTabpag6.Text = "NtsPanel1"
    '
    'ceProvvig
    '
    Me.ceProvvig.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceProvvig.GridColumn1_465_17 = Nothing
    Me.ceProvvig.GridColumn1_715_16 = Nothing
    Me.ceProvvig.Location = New System.Drawing.Point(0, 0)
    Me.ceProvvig.MinimumSize = New System.Drawing.Size(504, 294)
    Me.ceProvvig.Name = "ceProvvig"
    Me.ceProvvig.PerClasseArt = 0
    Me.ceProvvig.PerClasseCli = 0
    Me.ceProvvig.PerCodart = ""
    Me.ceProvvig.PerCodartRoot = ""
    Me.ceProvvig.PerCodcage = 0
    Me.ceProvvig.PerConto = 0
    Me.ceProvvig.Size = New System.Drawing.Size(755, 294)
    Me.ceProvvig.strNomeCampo = ""
    Me.ceProvvig.TabIndex = 0
    Me.ceProvvig.TipoProvv = 0
    '
    'NtsTabPage7
    '
    Me.NtsTabPage7.AllowDrop = True
    Me.NtsTabPage7.Controls.Add(Me.pnTabpag7)
    Me.NtsTabPage7.Enable = True
    Me.NtsTabPage7.Name = "NtsTabPage7"
    Me.NtsTabPage7.Size = New System.Drawing.Size(755, 294)
    Me.NtsTabPage7.Text = "&7 - Note"
    '
    'pnTabpag7
    '
    Me.pnTabpag7.AllowDrop = True
    Me.pnTabpag7.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag7.Appearance.Options.UseBackColor = True
    Me.pnTabpag7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag7.Controls.Add(Me.edAr_note)
    Me.pnTabpag7.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag7.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag7.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag7.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag7.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag7.Name = "pnTabpag7"
    Me.pnTabpag7.NTSActiveTrasparency = True
    Me.pnTabpag7.Size = New System.Drawing.Size(755, 294)
    Me.pnTabpag7.TabIndex = 0
    Me.pnTabpag7.Text = "NtsPanel1"
    '
    'edAr_note
    '
    Me.edAr_note.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_note.Dock = System.Windows.Forms.DockStyle.Fill
    Me.edAr_note.Location = New System.Drawing.Point(0, 0)
    Me.edAr_note.Name = "edAr_note"
    Me.edAr_note.NTSDbField = ""
    Me.edAr_note.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_note.Size = New System.Drawing.Size(755, 294)
    Me.edAr_note.TabIndex = 552
    '
    'NtsTabPage8
    '
    Me.NtsTabPage8.AllowDrop = True
    Me.NtsTabPage8.Controls.Add(Me.pnTabpag8)
    Me.NtsTabPage8.Enable = True
    Me.NtsTabPage8.Name = "NtsTabPage8"
    Me.NtsTabPage8.Size = New System.Drawing.Size(755, 294)
    Me.NtsTabPage8.Text = "&8 - Magazzini/U.M."
    '
    'pnTabpag8
    '
    Me.pnTabpag8.AllowDrop = True
    Me.pnTabpag8.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag8.Appearance.Options.UseBackColor = True
    Me.pnTabpag8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag8.Controls.Add(Me.pnMaga2)
    Me.pnTabpag8.Controls.Add(Me.pnMaga1)
    Me.pnTabpag8.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag8.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag8.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag8.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag8.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag8.Name = "pnTabpag8"
    Me.pnTabpag8.NTSActiveTrasparency = True
    Me.pnTabpag8.Size = New System.Drawing.Size(755, 294)
    Me.pnTabpag8.TabIndex = 0
    Me.pnTabpag8.Text = "NtsPanel1"
    '
    'pnMaga2
    '
    Me.pnMaga2.AllowDrop = True
    Me.pnMaga2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnMaga2.Appearance.Options.UseBackColor = True
    Me.pnMaga2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnMaga2.Controls.Add(Me.fmLogisticaPalmare)
    Me.pnMaga2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMaga2.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnMaga2.Location = New System.Drawing.Point(432, 0)
    Me.pnMaga2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnMaga2.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnMaga2.Name = "pnMaga2"
    Me.pnMaga2.NTSActiveTrasparency = True
    Me.pnMaga2.Size = New System.Drawing.Size(323, 294)
    Me.pnMaga2.TabIndex = 642
    Me.pnMaga2.Text = "NtsPanel1"
    '
    'fmLogisticaPalmare
    '
    Me.fmLogisticaPalmare.AllowDrop = True
    Me.fmLogisticaPalmare.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmLogisticaPalmare.Appearance.Options.UseBackColor = True
    Me.fmLogisticaPalmare.Controls.Add(Me.lbXx_codgrlo)
    Me.fmLogisticaPalmare.Controls.Add(Me.lbAr_ubicus)
    Me.fmLogisticaPalmare.Controls.Add(Me.lbAr_ubicri)
    Me.fmLogisticaPalmare.Controls.Add(Me.lbAr_ubicpr)
    Me.fmLogisticaPalmare.Controls.Add(Me.lbAr_ubicst)
    Me.fmLogisticaPalmare.Controls.Add(Me.edAr_ubicpr)
    Me.fmLogisticaPalmare.Controls.Add(Me.edAr_ubicri)
    Me.fmLogisticaPalmare.Controls.Add(Me.edAr_ubicus)
    Me.fmLogisticaPalmare.Controls.Add(Me.edAr_ubicst)
    Me.fmLogisticaPalmare.Controls.Add(Me.lbAr_codgrlo)
    Me.fmLogisticaPalmare.Controls.Add(Me.lbAr_scominpk)
    Me.fmLogisticaPalmare.Controls.Add(Me.lbAr_indrot)
    Me.fmLogisticaPalmare.Controls.Add(Me.lbAr_converp)
    Me.fmLogisticaPalmare.Controls.Add(Me.edAr_indrot)
    Me.fmLogisticaPalmare.Controls.Add(Me.edAr_scominpk)
    Me.fmLogisticaPalmare.Controls.Add(Me.edAr_codgrlo)
    Me.fmLogisticaPalmare.Controls.Add(Me.edAr_converp)
    Me.fmLogisticaPalmare.Controls.Add(Me.ckAr_staetip)
    Me.fmLogisticaPalmare.Controls.Add(Me.ckAr_staeti)
    Me.fmLogisticaPalmare.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmLogisticaPalmare.Location = New System.Drawing.Point(6, 4)
    Me.fmLogisticaPalmare.Name = "fmLogisticaPalmare"
    Me.fmLogisticaPalmare.Size = New System.Drawing.Size(311, 284)
    Me.fmLogisticaPalmare.TabIndex = 0
    Me.fmLogisticaPalmare.Text = "LOGISTICA DI MAGAZZINO SU PALMARE"
    '
    'lbXx_codgrlo
    '
    Me.lbXx_codgrlo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codgrlo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codgrlo.Location = New System.Drawing.Point(199, 147)
    Me.lbXx_codgrlo.Name = "lbXx_codgrlo"
    Me.lbXx_codgrlo.NTSDbField = ""
    Me.lbXx_codgrlo.Size = New System.Drawing.Size(106, 20)
    Me.lbXx_codgrlo.TabIndex = 18
    Me.lbXx_codgrlo.Tooltip = ""
    Me.lbXx_codgrlo.UseMnemonic = False
    '
    'lbAr_ubicus
    '
    Me.lbAr_ubicus.AutoSize = True
    Me.lbAr_ubicus.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_ubicus.Location = New System.Drawing.Point(6, 250)
    Me.lbAr_ubicus.Name = "lbAr_ubicus"
    Me.lbAr_ubicus.NTSDbField = ""
    Me.lbAr_ubicus.Size = New System.Drawing.Size(100, 13)
    Me.lbAr_ubicus.TabIndex = 17
    Me.lbAr_ubicus.Text = "Ubicazione di uscita"
    Me.lbAr_ubicus.Tooltip = ""
    Me.lbAr_ubicus.UseMnemonic = False
    '
    'lbAr_ubicri
    '
    Me.lbAr_ubicri.AutoSize = True
    Me.lbAr_ubicri.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_ubicri.Location = New System.Drawing.Point(6, 225)
    Me.lbAr_ubicri.Name = "lbAr_ubicri"
    Me.lbAr_ubicri.NTSDbField = ""
    Me.lbAr_ubicri.Size = New System.Drawing.Size(127, 13)
    Me.lbAr_ubicri.TabIndex = 16
    Me.lbAr_ubicri.Text = "Ubicazione di ricevimento"
    Me.lbAr_ubicri.Tooltip = ""
    Me.lbAr_ubicri.UseMnemonic = False
    '
    'lbAr_ubicpr
    '
    Me.lbAr_ubicpr.AutoSize = True
    Me.lbAr_ubicpr.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_ubicpr.Location = New System.Drawing.Point(6, 200)
    Me.lbAr_ubicpr.Name = "lbAr_ubicpr"
    Me.lbAr_ubicpr.NTSDbField = ""
    Me.lbAr_ubicpr.Size = New System.Drawing.Size(125, 13)
    Me.lbAr_ubicpr.TabIndex = 15
    Me.lbAr_ubicpr.Text = "Ubicazione di produzione"
    Me.lbAr_ubicpr.Tooltip = ""
    Me.lbAr_ubicpr.UseMnemonic = False
    '
    'lbAr_ubicst
    '
    Me.lbAr_ubicst.AutoSize = True
    Me.lbAr_ubicst.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_ubicst.Location = New System.Drawing.Point(6, 175)
    Me.lbAr_ubicst.Name = "lbAr_ubicst"
    Me.lbAr_ubicst.NTSDbField = ""
    Me.lbAr_ubicst.Size = New System.Drawing.Size(97, 13)
    Me.lbAr_ubicst.TabIndex = 14
    Me.lbAr_ubicst.Text = "Ubicazione di stock"
    Me.lbAr_ubicst.Tooltip = ""
    Me.lbAr_ubicst.UseMnemonic = False
    '
    'edAr_ubicpr
    '
    Me.edAr_ubicpr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ubicpr.EditValue = ""
    Me.edAr_ubicpr.Location = New System.Drawing.Point(142, 197)
    Me.edAr_ubicpr.Name = "edAr_ubicpr"
    Me.edAr_ubicpr.NTSDbField = ""
    Me.edAr_ubicpr.NTSForzaVisZoom = False
    Me.edAr_ubicpr.NTSOldValue = ""
    Me.edAr_ubicpr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_ubicpr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_ubicpr.Properties.AutoHeight = False
    Me.edAr_ubicpr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_ubicpr.Properties.MaxLength = 65536
    Me.edAr_ubicpr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_ubicpr.Size = New System.Drawing.Size(163, 20)
    Me.edAr_ubicpr.TabIndex = 13
    '
    'edAr_ubicri
    '
    Me.edAr_ubicri.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ubicri.EditValue = ""
    Me.edAr_ubicri.Location = New System.Drawing.Point(142, 222)
    Me.edAr_ubicri.Name = "edAr_ubicri"
    Me.edAr_ubicri.NTSDbField = ""
    Me.edAr_ubicri.NTSForzaVisZoom = False
    Me.edAr_ubicri.NTSOldValue = ""
    Me.edAr_ubicri.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_ubicri.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_ubicri.Properties.AutoHeight = False
    Me.edAr_ubicri.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_ubicri.Properties.MaxLength = 65536
    Me.edAr_ubicri.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_ubicri.Size = New System.Drawing.Size(163, 20)
    Me.edAr_ubicri.TabIndex = 12
    '
    'edAr_ubicus
    '
    Me.edAr_ubicus.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ubicus.EditValue = ""
    Me.edAr_ubicus.Location = New System.Drawing.Point(142, 247)
    Me.edAr_ubicus.Name = "edAr_ubicus"
    Me.edAr_ubicus.NTSDbField = ""
    Me.edAr_ubicus.NTSForzaVisZoom = False
    Me.edAr_ubicus.NTSOldValue = ""
    Me.edAr_ubicus.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_ubicus.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_ubicus.Properties.AutoHeight = False
    Me.edAr_ubicus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_ubicus.Properties.MaxLength = 65536
    Me.edAr_ubicus.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_ubicus.Size = New System.Drawing.Size(163, 20)
    Me.edAr_ubicus.TabIndex = 11
    '
    'edAr_ubicst
    '
    Me.edAr_ubicst.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ubicst.EditValue = ""
    Me.edAr_ubicst.Location = New System.Drawing.Point(142, 172)
    Me.edAr_ubicst.Name = "edAr_ubicst"
    Me.edAr_ubicst.NTSDbField = ""
    Me.edAr_ubicst.NTSForzaVisZoom = False
    Me.edAr_ubicst.NTSOldValue = ""
    Me.edAr_ubicst.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_ubicst.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_ubicst.Properties.AutoHeight = False
    Me.edAr_ubicst.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_ubicst.Properties.MaxLength = 65536
    Me.edAr_ubicst.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_ubicst.Size = New System.Drawing.Size(163, 20)
    Me.edAr_ubicst.TabIndex = 10
    '
    'lbAr_codgrlo
    '
    Me.lbAr_codgrlo.AutoSize = True
    Me.lbAr_codgrlo.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codgrlo.Location = New System.Drawing.Point(6, 150)
    Me.lbAr_codgrlo.Name = "lbAr_codgrlo"
    Me.lbAr_codgrlo.NTSDbField = ""
    Me.lbAr_codgrlo.Size = New System.Drawing.Size(83, 13)
    Me.lbAr_codgrlo.TabIndex = 9
    Me.lbAr_codgrlo.Text = "Gruppo logistico"
    Me.lbAr_codgrlo.Tooltip = ""
    Me.lbAr_codgrlo.UseMnemonic = False
    '
    'lbAr_scominpk
    '
    Me.lbAr_scominpk.AutoSize = True
    Me.lbAr_scominpk.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_scominpk.Location = New System.Drawing.Point(6, 125)
    Me.lbAr_scominpk.Name = "lbAr_scominpk"
    Me.lbAr_scominpk.NTSDbField = ""
    Me.lbAr_scominpk.Size = New System.Drawing.Size(133, 13)
    Me.lbAr_scominpk.TabIndex = 8
    Me.lbAr_scominpk.Text = "Scorta min. ubic. di picking"
    Me.lbAr_scominpk.Tooltip = ""
    Me.lbAr_scominpk.UseMnemonic = False
    '
    'lbAr_indrot
    '
    Me.lbAr_indrot.AutoSize = True
    Me.lbAr_indrot.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_indrot.Location = New System.Drawing.Point(6, 100)
    Me.lbAr_indrot.Name = "lbAr_indrot"
    Me.lbAr_indrot.NTSDbField = ""
    Me.lbAr_indrot.Size = New System.Drawing.Size(95, 13)
    Me.lbAr_indrot.TabIndex = 7
    Me.lbAr_indrot.Text = "Indice di rotazione"
    Me.lbAr_indrot.Tooltip = ""
    Me.lbAr_indrot.UseMnemonic = False
    '
    'lbAr_converp
    '
    Me.lbAr_converp.AutoSize = True
    Me.lbAr_converp.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_converp.Location = New System.Drawing.Point(6, 75)
    Me.lbAr_converp.Name = "lbAr_converp"
    Me.lbAr_converp.NTSDbField = ""
    Me.lbAr_converp.Size = New System.Drawing.Size(116, 13)
    Me.lbAr_converp.TabIndex = 6
    Me.lbAr_converp.Text = "Rapporto di conv. UdC"
    Me.lbAr_converp.Tooltip = ""
    Me.lbAr_converp.UseMnemonic = False
    '
    'edAr_indrot
    '
    Me.edAr_indrot.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_indrot.EditValue = "0"
    Me.edAr_indrot.Location = New System.Drawing.Point(142, 97)
    Me.edAr_indrot.Name = "edAr_indrot"
    Me.edAr_indrot.NTSDbField = ""
    Me.edAr_indrot.NTSFormat = "0"
    Me.edAr_indrot.NTSForzaVisZoom = False
    Me.edAr_indrot.NTSOldValue = ""
    Me.edAr_indrot.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_indrot.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_indrot.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_indrot.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_indrot.Properties.AutoHeight = False
    Me.edAr_indrot.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_indrot.Properties.MaxLength = 65536
    Me.edAr_indrot.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_indrot.Size = New System.Drawing.Size(163, 20)
    Me.edAr_indrot.TabIndex = 5
    '
    'edAr_scominpk
    '
    Me.edAr_scominpk.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_scominpk.EditValue = "0"
    Me.edAr_scominpk.Location = New System.Drawing.Point(142, 122)
    Me.edAr_scominpk.Name = "edAr_scominpk"
    Me.edAr_scominpk.NTSDbField = ""
    Me.edAr_scominpk.NTSFormat = "0"
    Me.edAr_scominpk.NTSForzaVisZoom = False
    Me.edAr_scominpk.NTSOldValue = ""
    Me.edAr_scominpk.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_scominpk.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_scominpk.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_scominpk.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_scominpk.Properties.AutoHeight = False
    Me.edAr_scominpk.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_scominpk.Properties.MaxLength = 65536
    Me.edAr_scominpk.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_scominpk.Size = New System.Drawing.Size(163, 20)
    Me.edAr_scominpk.TabIndex = 4
    '
    'edAr_codgrlo
    '
    Me.edAr_codgrlo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codgrlo.EditValue = "0"
    Me.edAr_codgrlo.Location = New System.Drawing.Point(142, 147)
    Me.edAr_codgrlo.Name = "edAr_codgrlo"
    Me.edAr_codgrlo.NTSDbField = ""
    Me.edAr_codgrlo.NTSFormat = "0"
    Me.edAr_codgrlo.NTSForzaVisZoom = False
    Me.edAr_codgrlo.NTSOldValue = ""
    Me.edAr_codgrlo.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_codgrlo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_codgrlo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codgrlo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codgrlo.Properties.AutoHeight = False
    Me.edAr_codgrlo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codgrlo.Properties.MaxLength = 65536
    Me.edAr_codgrlo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codgrlo.Size = New System.Drawing.Size(51, 20)
    Me.edAr_codgrlo.TabIndex = 3
    '
    'edAr_converp
    '
    Me.edAr_converp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_converp.EditValue = "0"
    Me.edAr_converp.Enabled = False
    Me.edAr_converp.Location = New System.Drawing.Point(142, 72)
    Me.edAr_converp.Name = "edAr_converp"
    Me.edAr_converp.NTSDbField = ""
    Me.edAr_converp.NTSFormat = "0"
    Me.edAr_converp.NTSForzaVisZoom = False
    Me.edAr_converp.NTSOldValue = ""
    Me.edAr_converp.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_converp.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_converp.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_converp.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_converp.Properties.AutoHeight = False
    Me.edAr_converp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_converp.Properties.MaxLength = 65536
    Me.edAr_converp.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_converp.Size = New System.Drawing.Size(163, 20)
    Me.edAr_converp.TabIndex = 2
    '
    'ckAr_staetip
    '
    Me.ckAr_staetip.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_staetip.Location = New System.Drawing.Point(6, 48)
    Me.ckAr_staetip.Name = "ckAr_staetip"
    Me.ckAr_staetip.NTSCheckValue = "S"
    Me.ckAr_staetip.NTSUnCheckValue = "N"
    Me.ckAr_staetip.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_staetip.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_staetip.Properties.AutoHeight = False
    Me.ckAr_staetip.Properties.Caption = "Stampa etichette unità di carico quando vengono generate"
    Me.ckAr_staetip.Size = New System.Drawing.Size(305, 19)
    Me.ckAr_staetip.TabIndex = 1
    '
    'ckAr_staeti
    '
    Me.ckAr_staeti.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_staeti.Location = New System.Drawing.Point(6, 24)
    Me.ckAr_staeti.Name = "ckAr_staeti"
    Me.ckAr_staeti.NTSCheckValue = "S"
    Me.ckAr_staeti.NTSUnCheckValue = "N"
    Me.ckAr_staeti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_staeti.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_staeti.Properties.AutoHeight = False
    Me.ckAr_staeti.Properties.Caption = "Stampa etichette articolo in ricezione merce"
    Me.ckAr_staeti.Size = New System.Drawing.Size(248, 19)
    Me.ckAr_staeti.TabIndex = 0
    '
    'pnMaga1
    '
    Me.pnMaga1.AllowDrop = True
    Me.pnMaga1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnMaga1.Appearance.Options.UseBackColor = True
    Me.pnMaga1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnMaga1.Controls.Add(Me.lbAr_magstock)
    Me.pnMaga1.Controls.Add(Me.cmdArtgif2)
    Me.pnMaga1.Controls.Add(Me.edAr_magprod)
    Me.pnMaga1.Controls.Add(Me.cmdArtGif1)
    Me.pnMaga1.Controls.Add(Me.lbAr_magprod)
    Me.pnMaga1.Controls.Add(Me.cmdVisGif2)
    Me.pnMaga1.Controls.Add(Me.edAr_magstock)
    Me.pnMaga1.Controls.Add(Me.cmdVisGif1)
    Me.pnMaga1.Controls.Add(Me.cbAr_umdapr)
    Me.pnMaga1.Controls.Add(Me.lbAr_um4)
    Me.pnMaga1.Controls.Add(Me.lbAr_umdapr)
    Me.pnMaga1.Controls.Add(Me.edAr_um4)
    Me.pnMaga1.Controls.Add(Me.lbXx_magprod)
    Me.pnMaga1.Controls.Add(Me.lbAr_formula)
    Me.pnMaga1.Controls.Add(Me.lbXx_magstock)
    Me.pnMaga1.Controls.Add(Me.edAr_formula)
    Me.pnMaga1.Controls.Add(Me.cbAr_umdapra)
    Me.pnMaga1.Controls.Add(Me.lbAr_umpdapr)
    Me.pnMaga1.Controls.Add(Me.lbAr_umdapra)
    Me.pnMaga1.Controls.Add(Me.cbAr_umpdapr)
    Me.pnMaga1.Controls.Add(Me.edAr_gif2)
    Me.pnMaga1.Controls.Add(Me.lbAr_umpdapra)
    Me.pnMaga1.Controls.Add(Me.lbAr_gif2)
    Me.pnMaga1.Controls.Add(Me.cbAr_umpdapra)
    Me.pnMaga1.Controls.Add(Me.edAr_gif1)
    Me.pnMaga1.Controls.Add(Me.lbAr_gif1)
    Me.pnMaga1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMaga1.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnMaga1.Location = New System.Drawing.Point(0, 0)
    Me.pnMaga1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnMaga1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnMaga1.Name = "pnMaga1"
    Me.pnMaga1.NTSActiveTrasparency = True
    Me.pnMaga1.Size = New System.Drawing.Size(466, 294)
    Me.pnMaga1.TabIndex = 641
    Me.pnMaga1.Text = "NtsPanel1"
    '
    'lbAr_magstock
    '
    Me.lbAr_magstock.AutoSize = True
    Me.lbAr_magstock.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_magstock.Location = New System.Drawing.Point(6, 14)
    Me.lbAr_magstock.Name = "lbAr_magstock"
    Me.lbAr_magstock.NTSDbField = ""
    Me.lbAr_magstock.Size = New System.Drawing.Size(101, 13)
    Me.lbAr_magstock.TabIndex = 568
    Me.lbAr_magstock.Text = "Magazz. stoccaggio"
    Me.lbAr_magstock.Tooltip = ""
    Me.lbAr_magstock.UseMnemonic = False
    '
    'cmdArtgif2
    '
    Me.cmdArtgif2.ImagePath = ""
    Me.cmdArtgif2.ImageText = ""
    Me.cmdArtgif2.Location = New System.Drawing.Point(327, 186)
    Me.cmdArtgif2.Name = "cmdArtgif2"
    Me.cmdArtgif2.NTSContextMenu = Nothing
    Me.cmdArtgif2.Size = New System.Drawing.Size(25, 20)
    Me.cmdArtgif2.TabIndex = 640
    Me.cmdArtgif2.Text = "..."
    '
    'edAr_magprod
    '
    Me.edAr_magprod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_magprod.EditValue = "0"
    Me.edAr_magprod.Location = New System.Drawing.Point(115, 36)
    Me.edAr_magprod.Name = "edAr_magprod"
    Me.edAr_magprod.NTSDbField = ""
    Me.edAr_magprod.NTSFormat = "0"
    Me.edAr_magprod.NTSForzaVisZoom = False
    Me.edAr_magprod.NTSOldValue = ""
    Me.edAr_magprod.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_magprod.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_magprod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_magprod.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_magprod.Properties.AutoHeight = False
    Me.edAr_magprod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_magprod.Properties.MaxLength = 65536
    Me.edAr_magprod.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_magprod.Size = New System.Drawing.Size(58, 20)
    Me.edAr_magprod.TabIndex = 572
    '
    'cmdArtGif1
    '
    Me.cmdArtGif1.ImagePath = ""
    Me.cmdArtGif1.ImageText = ""
    Me.cmdArtGif1.Location = New System.Drawing.Point(327, 161)
    Me.cmdArtGif1.Name = "cmdArtGif1"
    Me.cmdArtGif1.NTSContextMenu = Nothing
    Me.cmdArtGif1.Size = New System.Drawing.Size(25, 20)
    Me.cmdArtGif1.TabIndex = 639
    Me.cmdArtGif1.Text = "..."
    '
    'lbAr_magprod
    '
    Me.lbAr_magprod.AutoSize = True
    Me.lbAr_magprod.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_magprod.Location = New System.Drawing.Point(6, 39)
    Me.lbAr_magprod.Name = "lbAr_magprod"
    Me.lbAr_magprod.NTSDbField = ""
    Me.lbAr_magprod.Size = New System.Drawing.Size(103, 13)
    Me.lbAr_magprod.TabIndex = 569
    Me.lbAr_magprod.Text = "Magazz. produzione"
    Me.lbAr_magprod.Tooltip = ""
    Me.lbAr_magprod.UseMnemonic = False
    '
    'cmdVisGif2
    '
    Me.cmdVisGif2.ImagePath = ""
    Me.cmdVisGif2.ImageText = ""
    Me.cmdVisGif2.Location = New System.Drawing.Point(358, 184)
    Me.cmdVisGif2.Name = "cmdVisGif2"
    Me.cmdVisGif2.NTSContextMenu = Nothing
    Me.cmdVisGif2.Size = New System.Drawing.Size(103, 20)
    Me.cmdVisGif2.TabIndex = 638
    Me.cmdVisGif2.Text = "Immagine scheda"
    '
    'edAr_magstock
    '
    Me.edAr_magstock.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAr_magstock.EditValue = "0"
    Me.edAr_magstock.Location = New System.Drawing.Point(115, 11)
    Me.edAr_magstock.Name = "edAr_magstock"
    Me.edAr_magstock.NTSDbField = ""
    Me.edAr_magstock.NTSFormat = "0"
    Me.edAr_magstock.NTSForzaVisZoom = False
    Me.edAr_magstock.NTSOldValue = ""
    Me.edAr_magstock.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_magstock.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_magstock.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_magstock.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_magstock.Properties.AutoHeight = False
    Me.edAr_magstock.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_magstock.Properties.MaxLength = 65536
    Me.edAr_magstock.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_magstock.Size = New System.Drawing.Size(58, 20)
    Me.edAr_magstock.TabIndex = 571
    '
    'cmdVisGif1
    '
    Me.cmdVisGif1.ImagePath = ""
    Me.cmdVisGif1.ImageText = ""
    Me.cmdVisGif1.Location = New System.Drawing.Point(358, 161)
    Me.cmdVisGif1.Name = "cmdVisGif1"
    Me.cmdVisGif1.NTSContextMenu = Nothing
    Me.cmdVisGif1.Size = New System.Drawing.Size(103, 20)
    Me.cmdVisGif1.TabIndex = 637
    Me.cmdVisGif1.Text = "Immagine catalogo"
    '
    'cbAr_umdapr
    '
    Me.cbAr_umdapr.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.cbAr_umdapr.DataSource = Nothing
    Me.cbAr_umdapr.DisplayMember = ""
    Me.cbAr_umdapr.Location = New System.Drawing.Point(115, 61)
    Me.cbAr_umdapr.Name = "cbAr_umdapr"
    Me.cbAr_umdapr.NTSDbField = ""
    Me.cbAr_umdapr.Properties.AutoHeight = False
    Me.cbAr_umdapr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_umdapr.Properties.DropDownRows = 30
    Me.cbAr_umdapr.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_umdapr.SelectedValue = ""
    Me.cbAr_umdapr.Size = New System.Drawing.Size(206, 20)
    Me.cbAr_umdapr.TabIndex = 570
    Me.cbAr_umdapr.ValueMember = ""
    '
    'lbAr_um4
    '
    Me.lbAr_um4.AutoSize = True
    Me.lbAr_um4.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_um4.Location = New System.Drawing.Point(6, 214)
    Me.lbAr_um4.Name = "lbAr_um4"
    Me.lbAr_um4.NTSDbField = ""
    Me.lbAr_um4.Size = New System.Drawing.Size(69, 13)
    Me.lbAr_um4.TabIndex = 635
    Me.lbAr_um4.Text = "U.M. formula"
    Me.lbAr_um4.Tooltip = ""
    Me.lbAr_um4.UseMnemonic = False
    '
    'lbAr_umdapr
    '
    Me.lbAr_umdapr.AutoSize = True
    Me.lbAr_umdapr.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_umdapr.Location = New System.Drawing.Point(6, 64)
    Me.lbAr_umdapr.Name = "lbAr_umdapr"
    Me.lbAr_umdapr.NTSDbField = ""
    Me.lbAr_umdapr.Size = New System.Drawing.Size(69, 13)
    Me.lbAr_umdapr.TabIndex = 567
    Me.lbAr_umdapr.Text = "U.M. vendite"
    Me.lbAr_umdapr.Tooltip = ""
    Me.lbAr_umdapr.UseMnemonic = False
    '
    'edAr_um4
    '
    Me.edAr_um4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_um4.EditValue = ""
    Me.edAr_um4.Location = New System.Drawing.Point(115, 211)
    Me.edAr_um4.Name = "edAr_um4"
    Me.edAr_um4.NTSDbField = ""
    Me.edAr_um4.NTSForzaVisZoom = False
    Me.edAr_um4.NTSOldValue = ""
    Me.edAr_um4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_um4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_um4.Properties.AutoHeight = False
    Me.edAr_um4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_um4.Properties.MaxLength = 65536
    Me.edAr_um4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_um4.Size = New System.Drawing.Size(58, 20)
    Me.edAr_um4.TabIndex = 636
    '
    'lbXx_magprod
    '
    Me.lbXx_magprod.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_magprod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_magprod.Location = New System.Drawing.Point(179, 36)
    Me.lbXx_magprod.Name = "lbXx_magprod"
    Me.lbXx_magprod.NTSDbField = ""
    Me.lbXx_magprod.Size = New System.Drawing.Size(282, 20)
    Me.lbXx_magprod.TabIndex = 622
    Me.lbXx_magprod.Tooltip = ""
    Me.lbXx_magprod.UseMnemonic = False
    '
    'lbAr_formula
    '
    Me.lbAr_formula.AutoSize = True
    Me.lbAr_formula.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_formula.Location = New System.Drawing.Point(6, 240)
    Me.lbAr_formula.Name = "lbAr_formula"
    Me.lbAr_formula.NTSDbField = ""
    Me.lbAr_formula.Size = New System.Drawing.Size(103, 13)
    Me.lbAr_formula.TabIndex = 633
    Me.lbAr_formula.Text = "For. di trasf. in UMP"
    Me.lbAr_formula.Tooltip = ""
    Me.lbAr_formula.UseMnemonic = False
    '
    'lbXx_magstock
    '
    Me.lbXx_magstock.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_magstock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_magstock.Location = New System.Drawing.Point(179, 10)
    Me.lbXx_magstock.Name = "lbXx_magstock"
    Me.lbXx_magstock.NTSDbField = ""
    Me.lbXx_magstock.Size = New System.Drawing.Size(282, 20)
    Me.lbXx_magstock.TabIndex = 621
    Me.lbXx_magstock.Tooltip = ""
    Me.lbXx_magstock.UseMnemonic = False
    '
    'edAr_formula
    '
    Me.edAr_formula.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_formula.EditValue = ""
    Me.edAr_formula.Location = New System.Drawing.Point(115, 236)
    Me.edAr_formula.Name = "edAr_formula"
    Me.edAr_formula.NTSDbField = ""
    Me.edAr_formula.NTSForzaVisZoom = False
    Me.edAr_formula.NTSOldValue = ""
    Me.edAr_formula.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_formula.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_formula.Properties.AutoHeight = False
    Me.edAr_formula.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_formula.Properties.MaxLength = 65536
    Me.edAr_formula.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_formula.Size = New System.Drawing.Size(346, 20)
    Me.edAr_formula.TabIndex = 634
    '
    'cbAr_umdapra
    '
    Me.cbAr_umdapra.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_umdapra.DataSource = Nothing
    Me.cbAr_umdapra.DisplayMember = ""
    Me.cbAr_umdapra.Location = New System.Drawing.Point(115, 86)
    Me.cbAr_umdapra.Name = "cbAr_umdapra"
    Me.cbAr_umdapra.NTSDbField = ""
    Me.cbAr_umdapra.Properties.AutoHeight = False
    Me.cbAr_umdapra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_umdapra.Properties.DropDownRows = 30
    Me.cbAr_umdapra.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_umdapra.SelectedValue = ""
    Me.cbAr_umdapra.Size = New System.Drawing.Size(206, 20)
    Me.cbAr_umdapra.TabIndex = 624
    Me.cbAr_umdapra.ValueMember = ""
    '
    'lbAr_umpdapr
    '
    Me.lbAr_umpdapr.AutoSize = True
    Me.lbAr_umpdapr.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_umpdapr.Location = New System.Drawing.Point(6, 114)
    Me.lbAr_umpdapr.Name = "lbAr_umpdapr"
    Me.lbAr_umpdapr.NTSDbField = ""
    Me.lbAr_umpdapr.Size = New System.Drawing.Size(104, 13)
    Me.lbAr_umpdapr.TabIndex = 625
    Me.lbAr_umpdapr.Text = "U.M. prezzo vendita"
    Me.lbAr_umpdapr.Tooltip = ""
    Me.lbAr_umpdapr.UseMnemonic = False
    '
    'lbAr_umdapra
    '
    Me.lbAr_umdapra.AutoSize = True
    Me.lbAr_umdapra.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_umdapra.Location = New System.Drawing.Point(6, 89)
    Me.lbAr_umdapra.Name = "lbAr_umdapra"
    Me.lbAr_umdapra.NTSDbField = ""
    Me.lbAr_umdapra.Size = New System.Drawing.Size(63, 13)
    Me.lbAr_umdapra.TabIndex = 623
    Me.lbAr_umdapra.Text = "U.M. carichi"
    Me.lbAr_umdapra.Tooltip = ""
    Me.lbAr_umdapra.UseMnemonic = False
    '
    'cbAr_umpdapr
    '
    Me.cbAr_umpdapr.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_umpdapr.DataSource = Nothing
    Me.cbAr_umpdapr.DisplayMember = ""
    Me.cbAr_umpdapr.Location = New System.Drawing.Point(115, 111)
    Me.cbAr_umpdapr.Name = "cbAr_umpdapr"
    Me.cbAr_umpdapr.NTSDbField = ""
    Me.cbAr_umpdapr.Properties.AutoHeight = False
    Me.cbAr_umpdapr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_umpdapr.Properties.DropDownRows = 30
    Me.cbAr_umpdapr.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_umpdapr.SelectedValue = ""
    Me.cbAr_umpdapr.Size = New System.Drawing.Size(206, 20)
    Me.cbAr_umpdapr.TabIndex = 629
    Me.cbAr_umpdapr.ValueMember = ""
    '
    'edAr_gif2
    '
    Me.edAr_gif2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_gif2.EditValue = ""
    Me.edAr_gif2.Location = New System.Drawing.Point(115, 186)
    Me.edAr_gif2.Name = "edAr_gif2"
    Me.edAr_gif2.NTSDbField = ""
    Me.edAr_gif2.NTSForzaVisZoom = False
    Me.edAr_gif2.NTSOldValue = ""
    Me.edAr_gif2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_gif2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_gif2.Properties.AutoHeight = False
    Me.edAr_gif2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_gif2.Properties.MaxLength = 65536
    Me.edAr_gif2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_gif2.Size = New System.Drawing.Size(206, 20)
    Me.edAr_gif2.TabIndex = 632
    '
    'lbAr_umpdapra
    '
    Me.lbAr_umpdapra.AutoSize = True
    Me.lbAr_umpdapra.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_umpdapra.Location = New System.Drawing.Point(6, 139)
    Me.lbAr_umpdapra.Name = "lbAr_umpdapra"
    Me.lbAr_umpdapra.NTSDbField = ""
    Me.lbAr_umpdapra.Size = New System.Drawing.Size(108, 13)
    Me.lbAr_umpdapra.TabIndex = 626
    Me.lbAr_umpdapra.Text = "U.M. prezzo acquisto"
    Me.lbAr_umpdapra.Tooltip = ""
    Me.lbAr_umpdapra.UseMnemonic = False
    '
    'lbAr_gif2
    '
    Me.lbAr_gif2.AutoSize = True
    Me.lbAr_gif2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gif2.Location = New System.Drawing.Point(6, 189)
    Me.lbAr_gif2.Name = "lbAr_gif2"
    Me.lbAr_gif2.NTSDbField = ""
    Me.lbAr_gif2.Size = New System.Drawing.Size(90, 13)
    Me.lbAr_gif2.TabIndex = 628
    Me.lbAr_gif2.Text = "Immagine scheda"
    Me.lbAr_gif2.Tooltip = ""
    Me.lbAr_gif2.UseMnemonic = False
    '
    'cbAr_umpdapra
    '
    Me.cbAr_umpdapra.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_umpdapra.DataSource = Nothing
    Me.cbAr_umpdapra.DisplayMember = ""
    Me.cbAr_umpdapra.Location = New System.Drawing.Point(115, 136)
    Me.cbAr_umpdapra.Name = "cbAr_umpdapra"
    Me.cbAr_umpdapra.NTSDbField = ""
    Me.cbAr_umpdapra.Properties.AutoHeight = False
    Me.cbAr_umpdapra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_umpdapra.Properties.DropDownRows = 30
    Me.cbAr_umpdapra.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_umpdapra.SelectedValue = ""
    Me.cbAr_umpdapra.Size = New System.Drawing.Size(206, 20)
    Me.cbAr_umpdapra.TabIndex = 630
    Me.cbAr_umpdapra.ValueMember = ""
    '
    'edAr_gif1
    '
    Me.edAr_gif1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_gif1.EditValue = ""
    Me.edAr_gif1.Location = New System.Drawing.Point(115, 161)
    Me.edAr_gif1.Name = "edAr_gif1"
    Me.edAr_gif1.NTSDbField = ""
    Me.edAr_gif1.NTSForzaVisZoom = False
    Me.edAr_gif1.NTSOldValue = ""
    Me.edAr_gif1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_gif1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_gif1.Properties.AutoHeight = False
    Me.edAr_gif1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_gif1.Properties.MaxLength = 65536
    Me.edAr_gif1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_gif1.Size = New System.Drawing.Size(206, 20)
    Me.edAr_gif1.TabIndex = 631
    '
    'lbAr_gif1
    '
    Me.lbAr_gif1.AutoSize = True
    Me.lbAr_gif1.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gif1.Location = New System.Drawing.Point(6, 164)
    Me.lbAr_gif1.Name = "lbAr_gif1"
    Me.lbAr_gif1.NTSDbField = ""
    Me.lbAr_gif1.Size = New System.Drawing.Size(97, 13)
    Me.lbAr_gif1.TabIndex = 627
    Me.lbAr_gif1.Text = "Immagine catalogo"
    Me.lbAr_gif1.Tooltip = ""
    Me.lbAr_gif1.UseMnemonic = False
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.ceColl)
    Me.pnTop.Controls.Add(Me.pnTopLeft)
    Me.pnTop.Controls.Add(Me.fmUnitamisura)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(764, 125)
    Me.pnTop.TabIndex = 575
    Me.pnTop.Text = "NtsPanel1"
    '
    'ceColl
    '
    Me.ceColl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ceColl.Location = New System.Drawing.Point(661, 4)
    Me.ceColl.Name = "ceColl"
    Me.ceColl.Size = New System.Drawing.Size(97, 22)
    Me.ceColl.strNomeCampo = ""
    Me.ceColl.TabIndex = 590
    Me.ceColl.Visible = False
    '
    'pnTopLeft
    '
    Me.pnTopLeft.AllowDrop = True
    Me.pnTopLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTopLeft.Appearance.Options.UseBackColor = True
    Me.pnTopLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTopLeft.Controls.Add(Me.pnTopLeftBut)
    Me.pnTopLeft.Controls.Add(Me.lbAr_codart)
    Me.pnTopLeft.Controls.Add(Me.edAr_desint)
    Me.pnTopLeft.Controls.Add(Me.cbAr_codtipa)
    Me.pnTopLeft.Controls.Add(Me.lbAr_codalt)
    Me.pnTopLeft.Controls.Add(Me.lbAr_descr)
    Me.pnTopLeft.Controls.Add(Me.edAr_codalt)
    Me.pnTopLeft.Controls.Add(Me.edAr_descr)
    Me.pnTopLeft.Controls.Add(Me.edAr_codart)
    Me.pnTopLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTopLeft.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnTopLeft.Location = New System.Drawing.Point(0, 0)
    Me.pnTopLeft.Name = "pnTopLeft"
    Me.pnTopLeft.NTSActiveTrasparency = True
    Me.pnTopLeft.Size = New System.Drawing.Size(536, 125)
    Me.pnTopLeft.TabIndex = 589
    Me.pnTopLeft.Text = "NtsPanel1"
    '
    'pnTopLeftBut
    '
    Me.pnTopLeftBut.AllowDrop = True
    Me.pnTopLeftBut.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTopLeftBut.Appearance.Options.UseBackColor = True
    Me.pnTopLeftBut.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTopLeftBut.Controls.Add(Me.cmdCodarfo)
    Me.pnTopLeftBut.Controls.Add(Me.cmdProgressivi)
    Me.pnTopLeftBut.Controls.Add(Me.cmdValuta)
    Me.pnTopLeftBut.Controls.Add(Me.cmdProgtot)
    Me.pnTopLeftBut.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTopLeftBut.Location = New System.Drawing.Point(291, 13)
    Me.pnTopLeftBut.Name = "pnTopLeftBut"
    Me.pnTopLeftBut.NTSActiveTrasparency = True
    Me.pnTopLeftBut.Size = New System.Drawing.Size(242, 47)
    Me.pnTopLeftBut.TabIndex = 510
    Me.pnTopLeftBut.Text = "NtsPanel1"
    '
    'cmdCodarfo
    '
    Me.cmdCodarfo.ImagePath = ""
    Me.cmdCodarfo.ImageText = ""
    Me.cmdCodarfo.Location = New System.Drawing.Point(116, 24)
    Me.cmdCodarfo.Name = "cmdCodarfo"
    Me.cmdCodarfo.NTSContextMenu = Nothing
    Me.cmdCodarfo.Size = New System.Drawing.Size(126, 22)
    Me.cmdCodarfo.TabIndex = 602
    Me.cmdCodarfo.Text = "&Codice articolo C/F"
    '
    'cmdProgressivi
    '
    Me.cmdProgressivi.ImagePath = ""
    Me.cmdProgressivi.ImageText = ""
    Me.cmdProgressivi.Location = New System.Drawing.Point(0, 24)
    Me.cmdProgressivi.Name = "cmdProgressivi"
    Me.cmdProgressivi.NTSContextMenu = Nothing
    Me.cmdProgressivi.Size = New System.Drawing.Size(113, 22)
    Me.cmdProgressivi.TabIndex = 601
    Me.cmdProgressivi.Text = "Progressivi"
    '
    'cmdValuta
    '
    Me.cmdValuta.ImagePath = ""
    Me.cmdValuta.ImageText = ""
    Me.cmdValuta.Location = New System.Drawing.Point(116, 0)
    Me.cmdValuta.Name = "cmdValuta"
    Me.cmdValuta.NTSContextMenu = Nothing
    Me.cmdValuta.Size = New System.Drawing.Size(126, 22)
    Me.cmdValuta.TabIndex = 600
    Me.cmdValuta.Text = "Descrizioni in ling&ua"
    '
    'cmdProgtot
    '
    Me.cmdProgtot.ImagePath = ""
    Me.cmdProgtot.ImageText = ""
    Me.cmdProgtot.Location = New System.Drawing.Point(0, 0)
    Me.cmdProgtot.Name = "cmdProgtot"
    Me.cmdProgtot.NTSContextMenu = Nothing
    Me.cmdProgtot.Size = New System.Drawing.Size(113, 22)
    Me.cmdProgtot.TabIndex = 599
    Me.cmdProgtot.Text = "Progressivi &totali"
    '
    'lbAr_codart
    '
    Me.lbAr_codart.AutoSize = True
    Me.lbAr_codart.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codart.Location = New System.Drawing.Point(6, 17)
    Me.lbAr_codart.Name = "lbAr_codart"
    Me.lbAr_codart.NTSDbField = ""
    Me.lbAr_codart.Size = New System.Drawing.Size(77, 13)
    Me.lbAr_codart.TabIndex = 588
    Me.lbAr_codart.Text = "Codice articolo"
    Me.lbAr_codart.Tooltip = ""
    Me.lbAr_codart.UseMnemonic = False
    '
    'edAr_desint
    '
    Me.edAr_desint.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_desint.EditValue = ""
    Me.edAr_desint.Location = New System.Drawing.Point(103, 93)
    Me.edAr_desint.Name = "edAr_desint"
    Me.edAr_desint.NTSDbField = ""
    Me.edAr_desint.NTSForzaVisZoom = False
    Me.edAr_desint.NTSOldValue = ""
    Me.edAr_desint.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_desint.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_desint.Properties.AutoHeight = False
    Me.edAr_desint.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_desint.Properties.MaxLength = 65536
    Me.edAr_desint.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_desint.Size = New System.Drawing.Size(429, 20)
    Me.edAr_desint.TabIndex = 594
    '
    'cbAr_codtipa
    '
    Me.cbAr_codtipa.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_codtipa.DataSource = Nothing
    Me.cbAr_codtipa.DisplayMember = ""
    Me.cbAr_codtipa.Location = New System.Drawing.Point(9, 93)
    Me.cbAr_codtipa.Name = "cbAr_codtipa"
    Me.cbAr_codtipa.NTSDbField = ""
    Me.cbAr_codtipa.Properties.AutoHeight = False
    Me.cbAr_codtipa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_codtipa.Properties.DropDownRows = 30
    Me.cbAr_codtipa.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_codtipa.SelectedValue = ""
    Me.cbAr_codtipa.Size = New System.Drawing.Size(88, 20)
    Me.cbAr_codtipa.TabIndex = 599
    Me.cbAr_codtipa.ValueMember = ""
    '
    'lbAr_codalt
    '
    Me.lbAr_codalt.AutoSize = True
    Me.lbAr_codalt.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codalt.Location = New System.Drawing.Point(6, 43)
    Me.lbAr_codalt.Name = "lbAr_codalt"
    Me.lbAr_codalt.NTSDbField = ""
    Me.lbAr_codalt.Size = New System.Drawing.Size(94, 13)
    Me.lbAr_codalt.TabIndex = 589
    Me.lbAr_codalt.Text = "Codice alternativo"
    Me.lbAr_codalt.Tooltip = ""
    Me.lbAr_codalt.UseMnemonic = False
    '
    'lbAr_descr
    '
    Me.lbAr_descr.AutoSize = True
    Me.lbAr_descr.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_descr.Location = New System.Drawing.Point(6, 70)
    Me.lbAr_descr.Name = "lbAr_descr"
    Me.lbAr_descr.NTSDbField = ""
    Me.lbAr_descr.Size = New System.Drawing.Size(61, 13)
    Me.lbAr_descr.TabIndex = 590
    Me.lbAr_descr.Text = "Descrizione"
    Me.lbAr_descr.Tooltip = ""
    Me.lbAr_descr.UseMnemonic = False
    '
    'edAr_codalt
    '
    Me.edAr_codalt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codalt.EditValue = ""
    Me.edAr_codalt.Location = New System.Drawing.Point(103, 40)
    Me.edAr_codalt.Name = "edAr_codalt"
    Me.edAr_codalt.NTSDbField = ""
    Me.edAr_codalt.NTSForzaVisZoom = False
    Me.edAr_codalt.NTSOldValue = ""
    Me.edAr_codalt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codalt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codalt.Properties.AutoHeight = False
    Me.edAr_codalt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codalt.Properties.MaxLength = 65536
    Me.edAr_codalt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codalt.Size = New System.Drawing.Size(182, 20)
    Me.edAr_codalt.TabIndex = 592
    '
    'edAr_descr
    '
    Me.edAr_descr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_descr.EditValue = ""
    Me.edAr_descr.Location = New System.Drawing.Point(103, 67)
    Me.edAr_descr.Name = "edAr_descr"
    Me.edAr_descr.NTSDbField = ""
    Me.edAr_descr.NTSForzaVisZoom = False
    Me.edAr_descr.NTSOldValue = ""
    Me.edAr_descr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_descr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_descr.Properties.AutoHeight = False
    Me.edAr_descr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_descr.Properties.MaxLength = 65536
    Me.edAr_descr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_descr.Size = New System.Drawing.Size(429, 20)
    Me.edAr_descr.TabIndex = 593
    '
    'edAr_codart
    '
    Me.edAr_codart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codart.EditValue = ""
    Me.edAr_codart.Enabled = False
    Me.edAr_codart.Location = New System.Drawing.Point(103, 14)
    Me.edAr_codart.Name = "edAr_codart"
    Me.edAr_codart.NTSDbField = ""
    Me.edAr_codart.NTSForzaVisZoom = False
    Me.edAr_codart.NTSOldValue = ""
    Me.edAr_codart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_codart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_codart.Properties.AutoHeight = False
    Me.edAr_codart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_codart.Properties.MaxLength = 65536
    Me.edAr_codart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_codart.Size = New System.Drawing.Size(182, 20)
    Me.edAr_codart.TabIndex = 591
    '
    'fmUnitamisura
    '
    Me.fmUnitamisura.AllowDrop = True
    Me.fmUnitamisura.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmUnitamisura.Appearance.Options.UseBackColor = True
    Me.fmUnitamisura.Controls.Add(Me.edAr_unmis)
    Me.fmUnitamisura.Controls.Add(Me.lbAr_unmis)
    Me.fmUnitamisura.Controls.Add(Me.lbAr_conver)
    Me.fmUnitamisura.Controls.Add(Me.lbAr_unmis2)
    Me.fmUnitamisura.Controls.Add(Me.edAr_conver)
    Me.fmUnitamisura.Controls.Add(Me.edAr_confez2)
    Me.fmUnitamisura.Controls.Add(Me.edAr_unmis2)
    Me.fmUnitamisura.Controls.Add(Me.edAr_qtacon2)
    Me.fmUnitamisura.Controls.Add(Me.lbAr_confez2)
    Me.fmUnitamisura.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmUnitamisura.Location = New System.Drawing.Point(544, 28)
    Me.fmUnitamisura.Name = "fmUnitamisura"
    Me.fmUnitamisura.Size = New System.Drawing.Size(214, 92)
    Me.fmUnitamisura.TabIndex = 588
    Me.fmUnitamisura.Text = "UNITA' DI MISURA"
    '
    'edAr_unmis
    '
    Me.edAr_unmis.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_unmis.EditValue = ""
    Me.edAr_unmis.Location = New System.Drawing.Point(81, 21)
    Me.edAr_unmis.Name = "edAr_unmis"
    Me.edAr_unmis.NTSDbField = ""
    Me.edAr_unmis.NTSForzaVisZoom = False
    Me.edAr_unmis.NTSOldValue = ""
    Me.edAr_unmis.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_unmis.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_unmis.Properties.AutoHeight = False
    Me.edAr_unmis.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_unmis.Properties.MaxLength = 65536
    Me.edAr_unmis.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_unmis.Size = New System.Drawing.Size(57, 20)
    Me.edAr_unmis.TabIndex = 505
    '
    'lbAr_unmis
    '
    Me.lbAr_unmis.AutoSize = True
    Me.lbAr_unmis.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_unmis.Location = New System.Drawing.Point(11, 24)
    Me.lbAr_unmis.Name = "lbAr_unmis"
    Me.lbAr_unmis.NTSDbField = ""
    Me.lbAr_unmis.Size = New System.Drawing.Size(52, 13)
    Me.lbAr_unmis.TabIndex = 15
    Me.lbAr_unmis.Text = "Principale"
    Me.lbAr_unmis.Tooltip = ""
    Me.lbAr_unmis.UseMnemonic = False
    '
    'lbAr_conver
    '
    Me.lbAr_conver.AutoSize = True
    Me.lbAr_conver.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_conver.Location = New System.Drawing.Point(144, 24)
    Me.lbAr_conver.Name = "lbAr_conver"
    Me.lbAr_conver.NTSDbField = ""
    Me.lbAr_conver.Size = New System.Drawing.Size(61, 13)
    Me.lbAr_conver.TabIndex = 17
    Me.lbAr_conver.Text = "QUANTITA'"
    Me.lbAr_conver.Tooltip = ""
    Me.lbAr_conver.UseMnemonic = False
    '
    'lbAr_unmis2
    '
    Me.lbAr_unmis2.AutoSize = True
    Me.lbAr_unmis2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_unmis2.Location = New System.Drawing.Point(11, 72)
    Me.lbAr_unmis2.Name = "lbAr_unmis2"
    Me.lbAr_unmis2.NTSDbField = ""
    Me.lbAr_unmis2.Size = New System.Drawing.Size(60, 13)
    Me.lbAr_unmis2.TabIndex = 16
    Me.lbAr_unmis2.Text = "Secondaria"
    Me.lbAr_unmis2.Tooltip = ""
    Me.lbAr_unmis2.UseMnemonic = False
    '
    'edAr_conver
    '
    Me.edAr_conver.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAr_conver.EditValue = "0"
    Me.edAr_conver.Location = New System.Drawing.Point(144, 69)
    Me.edAr_conver.Name = "edAr_conver"
    Me.edAr_conver.NTSDbField = ""
    Me.edAr_conver.NTSFormat = "0"
    Me.edAr_conver.NTSForzaVisZoom = False
    Me.edAr_conver.NTSOldValue = ""
    Me.edAr_conver.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_conver.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_conver.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_conver.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_conver.Properties.AutoHeight = False
    Me.edAr_conver.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_conver.Properties.MaxLength = 65536
    Me.edAr_conver.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_conver.Size = New System.Drawing.Size(61, 20)
    Me.edAr_conver.TabIndex = 507
    '
    'edAr_confez2
    '
    Me.edAr_confez2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_confez2.EditValue = ""
    Me.edAr_confez2.Location = New System.Drawing.Point(81, 45)
    Me.edAr_confez2.Name = "edAr_confez2"
    Me.edAr_confez2.NTSDbField = ""
    Me.edAr_confez2.NTSForzaVisZoom = False
    Me.edAr_confez2.NTSOldValue = ""
    Me.edAr_confez2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_confez2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_confez2.Properties.AutoHeight = False
    Me.edAr_confez2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_confez2.Properties.MaxLength = 65536
    Me.edAr_confez2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_confez2.Size = New System.Drawing.Size(57, 20)
    Me.edAr_confez2.TabIndex = 508
    '
    'edAr_unmis2
    '
    Me.edAr_unmis2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_unmis2.EditValue = ""
    Me.edAr_unmis2.Location = New System.Drawing.Point(81, 69)
    Me.edAr_unmis2.Name = "edAr_unmis2"
    Me.edAr_unmis2.NTSDbField = ""
    Me.edAr_unmis2.NTSForzaVisZoom = False
    Me.edAr_unmis2.NTSOldValue = ""
    Me.edAr_unmis2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_unmis2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_unmis2.Properties.AutoHeight = False
    Me.edAr_unmis2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_unmis2.Properties.MaxLength = 65536
    Me.edAr_unmis2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_unmis2.Size = New System.Drawing.Size(57, 20)
    Me.edAr_unmis2.TabIndex = 506
    '
    'edAr_qtacon2
    '
    Me.edAr_qtacon2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_qtacon2.EditValue = "0"
    Me.edAr_qtacon2.Location = New System.Drawing.Point(144, 45)
    Me.edAr_qtacon2.Name = "edAr_qtacon2"
    Me.edAr_qtacon2.NTSDbField = ""
    Me.edAr_qtacon2.NTSFormat = "0"
    Me.edAr_qtacon2.NTSForzaVisZoom = False
    Me.edAr_qtacon2.NTSOldValue = ""
    Me.edAr_qtacon2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAr_qtacon2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAr_qtacon2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAr_qtacon2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAr_qtacon2.Properties.AutoHeight = False
    Me.edAr_qtacon2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAr_qtacon2.Properties.MaxLength = 65536
    Me.edAr_qtacon2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAr_qtacon2.Size = New System.Drawing.Size(61, 20)
    Me.edAr_qtacon2.TabIndex = 509
    '
    'lbAr_confez2
    '
    Me.lbAr_confez2.AutoSize = True
    Me.lbAr_confez2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_confez2.Location = New System.Drawing.Point(11, 48)
    Me.lbAr_confez2.Name = "lbAr_confez2"
    Me.lbAr_confez2.NTSDbField = ""
    Me.lbAr_confez2.Size = New System.Drawing.Size(61, 13)
    Me.lbAr_confez2.TabIndex = 18
    Me.lbAr_confez2.Text = "Confezione"
    Me.lbAr_confez2.Tooltip = ""
    Me.lbAr_confez2.UseMnemonic = False
    '
    'edFocus
    '
    Me.edFocus.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFocus.EditValue = "0"
    Me.edFocus.Location = New System.Drawing.Point(-1000, -1000)
    Me.edFocus.Name = "edFocus"
    Me.edFocus.NTSDbField = ""
    Me.edFocus.NTSFormat = "0"
    Me.edFocus.NTSForzaVisZoom = False
    Me.edFocus.NTSOldValue = ""
    Me.edFocus.Properties.Appearance.Options.UseTextOptions = True
    Me.edFocus.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFocus.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFocus.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFocus.Properties.AutoHeight = False
    Me.edFocus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFocus.Properties.MaxLength = 65536
    Me.edFocus.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFocus.Size = New System.Drawing.Size(10, 20)
    Me.edFocus.TabIndex = 504
    '
    'FRMMGARTI
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(764, 479)
    Me.Controls.Add(Me.edFocus)
    Me.Controls.Add(Me.pnArti)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGARTI"
    Me.Text = "ANAGRAFICA ARTICOLI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnArti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnArti.ResumeLayout(False)
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMain.ResumeLayout(False)
    CType(Me.tsArti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsArti.ResumeLayout(False)
    Me.NtsTabPage9.ResumeLayout(False)
    CType(Me.pnTabpag9, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag9.ResumeLayout(False)
    Me.pnTabpag9.PerformLayout()
    CType(Me.cbAr_deterior.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codtlox.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_tipscarlotx.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmCadc, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCadc.ResumeLayout(False)
    Me.fmCadc.PerformLayout()
    CType(Me.edAr_coddicv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_coddica.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codtcdc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmUbiFasi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmUbiFasi.ResumeLayout(False)
    Me.fmUbiFasi.PerformLayout()
    CType(Me.edAr_ultfase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_gesubic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_gesfasi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_cartcanol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_tipitemcp3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_cartric.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_cartcanas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_gestser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_makebuy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_volume.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codimba.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_misura1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_misura2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_misura3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_flmod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnTabpag1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag1.ResumeLayout(False)
    CType(Me.pnTabpag1Right, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag1Right.ResumeLayout(False)
    Me.pnTabpag1Right.PerformLayout()
    CType(Me.fmEcommerce, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmEcommerce.ResumeLayout(False)
    Me.fmEcommerce.PerformLayout()
    CType(Me.ckAr_webvend.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_webusat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_webvis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codseat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmIntrastat, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmIntrastat.ResumeLayout(False)
    Me.fmIntrastat.PerformLayout()
    CType(Me.cbAr_umintra2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codnomc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_percvst.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_prorig.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_paeorig.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_sostit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_sostituito.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_numecr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTabpag1Left, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag1Left.ResumeLayout(False)
    Me.pnTabpag1Left.PerformLayout()
    CType(Me.cbAr_gescon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codappr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_tipokit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_flricmar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codpdon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_ricar1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_ricar2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_reparto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_garacq.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_garven.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_perqta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codmarc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnTabpag2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag2.ResumeLayout(False)
    CType(Me.fmProduzione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmProduzione.ResumeLayout(False)
    Me.fmProduzione.PerformLayout()
    CType(Me.ckAr_consmrp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_critico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_blocco.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_tipoopz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_gescomm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_fpfence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_rrfence.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_ggrior.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_fcorrlt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_verdb.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_livmindb.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_coddb.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAcquisti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAcquisti.ResumeLayout(False)
    Me.fmAcquisti.PerformLayout()
    CType(Me.edAr_scosic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_ggant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_ggpost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_ggragg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_perragg.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_scomin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_scomax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_sublotto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_maxlotto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_ripriord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_minord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_polriord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_forn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_forn2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.pnTabpag3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag3.ResumeLayout(False)
    CType(Me.pnTabpag3Right, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag3Right.ResumeLayout(False)
    Me.pnTabpag3Right.PerformLayout()
    CType(Me.fmAltriDati, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAltriDati.ResumeLayout(False)
    Me.fmAltriDati.PerformLayout()
    CType(Me.ckAr_flgift.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_stainv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_stasche.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_geslotti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_inesaur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_pesoca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_stalist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_gestmatr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_contriva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codvuo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_pesolor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_pesonet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_catlifo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTabpag3Left, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag3Left.ResumeLayout(False)
    Me.pnTabpag3Left.PerformLayout()
    CType(Me.edAr_claprov.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_clascon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_famprod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_contros.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codiva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_gruppo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_sotgru.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_controp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_controa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_tipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_ubicaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage4.ResumeLayout(False)
    CType(Me.pnTabpag4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag4.ResumeLayout(False)
    Me.NtsTabPage5.ResumeLayout(False)
    CType(Me.pnTabpag5, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag5.ResumeLayout(False)
    Me.NtsTabPage6.ResumeLayout(False)
    CType(Me.pnTabpag6, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag6.ResumeLayout(False)
    Me.NtsTabPage7.ResumeLayout(False)
    CType(Me.pnTabpag7, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag7.ResumeLayout(False)
    CType(Me.edAr_note.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage8.ResumeLayout(False)
    CType(Me.pnTabpag8, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag8.ResumeLayout(False)
    CType(Me.pnMaga2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMaga2.ResumeLayout(False)
    CType(Me.fmLogisticaPalmare, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmLogisticaPalmare.ResumeLayout(False)
    Me.fmLogisticaPalmare.PerformLayout()
    CType(Me.edAr_ubicpr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_ubicri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_ubicus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_ubicst.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_indrot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_scominpk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codgrlo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_converp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_staetip.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_staeti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnMaga1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMaga1.ResumeLayout(False)
    Me.pnMaga1.PerformLayout()
    CType(Me.edAr_magprod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_magstock.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_umdapr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_um4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_formula.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_umdapra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_umpdapr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_gif2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_umpdapra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_gif1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    CType(Me.pnTopLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTopLeft.ResumeLayout(False)
    Me.pnTopLeft.PerformLayout()
    CType(Me.pnTopLeftBut, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTopLeftBut.ResumeLayout(False)
    CType(Me.edAr_desint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_codtipa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codalt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_descr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmUnitamisura, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmUnitamisura.ResumeLayout(False)
    Me.fmUnitamisura.PerformLayout()
    CType(Me.edAr_unmis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_conver.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_confez2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_unmis2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_qtacon2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFocus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    'creo e attivo l'entity e inizializzo la funzione che dovrÃ  rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGARTI", "BEMGARTI", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128550728307822408, "ERRORE in fase di creazione Entity:" & vbCrLf) & strErr)
      Return False
    End If
    oCleArti = CType(oTmp, CLEMGARTI)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGARTI", strRemoteServer, strRemotePort)
    AddHandler oCleArti.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleArti.Init(oApp, oScript, oMenu.oCleComm, "ARTICO", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub CaricaCombo()
    Dim dttAr_perragg As New DataTable()
    Dim dttAr_flricmar As New DataTable()
    Dim dttAr_tipokit As New DataTable()
    Dim dttAr_gescon As New DataTable()
    Dim dttAr_umintra2 As New DataTable()
    Dim dttAr_polriord As New DataTable()
    Dim dttAr_tipoopz As New DataTable()
    Dim dttAr_gescomm As New DataTable()
    Dim dttAr_umdapr As New DataTable()
    Dim dttAr_umdapra As New DataTable()
    Dim dttAr_umpdapr As New DataTable()
    Dim dttAr_umpdapra As New DataTable()
    Dim dttAr_makebuy As New DataTable()
    Dim dttAr_gestser As New DataTable()
    Dim dttAr_tipitemcp3 As New DataTable()
    Dim dttAr_tipscarlotx As New DataTable()
    Dim dttAr_deterior As New DataTable()
    Try
      dttAr_perragg.Columns.Add("cod", GetType(String))
      dttAr_perragg.Columns.Add("val", GetType(String))
      dttAr_perragg.Rows.Add(New Object() {"G", "Giorno"})
      dttAr_perragg.Rows.Add(New Object() {"S", "Settimana"})
      dttAr_perragg.Rows.Add(New Object() {"D", "Decade"})
      dttAr_perragg.Rows.Add(New Object() {"Q", "Quindicina"})
      dttAr_perragg.Rows.Add(New Object() {"M", "Mese"})
      dttAr_perragg.Rows.Add(New Object() {"B", "Bimestre"})
      dttAr_perragg.Rows.Add(New Object() {"T", "Trimestre"})
      dttAr_perragg.Rows.Add(New Object() {"R", "Quadrimestre"})
      dttAr_perragg.Rows.Add(New Object() {"U", "Semestre"})
      dttAr_perragg.Rows.Add(New Object() {"A", "Anno"})
      dttAr_perragg.AcceptChanges()

      cbAr_perragg.DataSource = dttAr_perragg
      cbAr_perragg.ValueMember = "cod"
      cbAr_perragg.DisplayMember = "val"

      dttAr_flricmar.Columns.Add("cod", GetType(String))
      dttAr_flricmar.Columns.Add("val", GetType(String))
      dttAr_flricmar.Rows.Add(New Object() {"M", "Margine"})
      dttAr_flricmar.Rows.Add(New Object() {"R", "Ricarico"})
      dttAr_flricmar.AcceptChanges()

      cbAr_flricmar.DataSource = dttAr_flricmar
      cbAr_flricmar.ValueMember = "cod"
      cbAr_flricmar.DisplayMember = "val"

      dttAr_tipokit.Columns.Add("cod", GetType(String))
      dttAr_tipokit.Columns.Add("val", GetType(String))
      dttAr_tipokit.Rows.Add(New Object() {" ", "(Nessuno)"})
      dttAr_tipokit.Rows.Add(New Object() {"A", "Prezzi analit."})
      dttAr_tipokit.Rows.Add(New Object() {"S", "Prezzo sintet."})
      dttAr_tipokit.AcceptChanges()

      cbAr_tipokit.DataSource = dttAr_tipokit
      cbAr_tipokit.ValueMember = "cod"
      cbAr_tipokit.DisplayMember = "val"

      dttAr_gescon.Columns.Add("cod", GetType(String))
      dttAr_gescon.Columns.Add("val", GetType(String))
      dttAr_gescon.Rows.Add(New Object() {"N", "Nessuna"})
      dttAr_gescon.Rows.Add(New Object() {"P", "Prima cess."})
      dttAr_gescon.Rows.Add(New Object() {"S", "Succ. cessione"})
      dttAr_gescon.AcceptChanges()

      cbAr_gescon.DataSource = dttAr_gescon
      cbAr_gescon.ValueMember = "cod"
      cbAr_gescon.DisplayMember = "val"

      dttAr_umintra2.Columns.Add("cod", GetType(String))
      dttAr_umintra2.Columns.Add("val", GetType(String))
      dttAr_umintra2.Rows.Add(New Object() {"C", "Confezione"})
      dttAr_umintra2.Rows.Add(New Object() {"N", "(Nessuna)"})
      dttAr_umintra2.Rows.Add(New Object() {"P", "Principale"})
      dttAr_umintra2.Rows.Add(New Object() {"Q", "Formula"})
      dttAr_umintra2.Rows.Add(New Object() {"S", "Secondaria"})
      dttAr_umintra2.AcceptChanges()

      cbAr_umintra2.DataSource = dttAr_umintra2
      cbAr_umintra2.ValueMember = "cod"
      cbAr_umintra2.DisplayMember = "val"

      dttAr_polriord.Columns.Add("cod", GetType(String))
      dttAr_polriord.Columns.Add("val", GetType(String))
      dttAr_polriord.Rows.Add(New Object() {"F", "Su fabbisogno con lotto"})
      dttAr_polriord.Rows.Add(New Object() {"G", "Su fabbisogno puro"})
      dttAr_polriord.Rows.Add(New Object() {"M", "A punto di riordino con lotto"})
      dttAr_polriord.Rows.Add(New Object() {"N", "A punto di riord. a ric.scorta"})
      dttAr_polriord.Rows.Add(New Object() {"O", "Su fabbisogno con lotto min."})
      dttAr_polriord.AcceptChanges()

      cbAr_polriord.DataSource = dttAr_polriord
      cbAr_polriord.ValueMember = "cod"
      cbAr_polriord.DisplayMember = "val"

      dttAr_tipoopz.Columns.Add("cod", GetType(String))
      dttAr_tipoopz.Columns.Add("val", GetType(String))
      dttAr_tipoopz.Rows.Add(New Object() {" ", "MP/SL Norm. (Reale)"})
      dttAr_tipoopz.Rows.Add(New Object() {"B", "Neutro MP (CP2)"})
      dttAr_tipoopz.Rows.Add(New Object() {"C", "SL/PF Indef. (Fitt.)"})
      dttAr_tipoopz.Rows.Add(New Object() {"G", "Gruppo PF (fittizio)"})
      dttAr_tipoopz.Rows.Add(New Object() {"O", "Prod.Finito (Reale)"})
      dttAr_tipoopz.Rows.Add(New Object() {"Q", "Neutro PF/SL (CP2)"})
      dttAr_tipoopz.AcceptChanges()

      cbAr_tipoopz.DataSource = dttAr_tipoopz
      cbAr_tipoopz.ValueMember = "cod"
      cbAr_tipoopz.DisplayMember = "val"

      dttAr_gescomm.Columns.Add("cod", GetType(String))
      dttAr_gescomm.Columns.Add("val", GetType(String))
      dttAr_gescomm.Rows.Add(New Object() {"N", "No"})
      dttAr_gescomm.Rows.Add(New Object() {"O", "Ordini/Impegni"})
      dttAr_gescomm.Rows.Add(New Object() {"S", "Ordini/Imp./Magaz."})
      dttAr_gescomm.AcceptChanges()

      cbAr_gescomm.DataSource = dttAr_gescomm
      cbAr_gescomm.ValueMember = "cod"
      cbAr_gescomm.DisplayMember = "val"

      dttAr_umdapr.Columns.Add("cod", GetType(String))
      dttAr_umdapr.Columns.Add("val", GetType(String))
      dttAr_umdapr.Rows.Add(New Object() {"C", "Confezione"})
      dttAr_umdapr.Rows.Add(New Object() {"P", "Principale"})
      dttAr_umdapr.Rows.Add(New Object() {"Q", "Formula"})
      dttAr_umdapr.Rows.Add(New Object() {"S", "Secondaria"})
      dttAr_umdapr.AcceptChanges()

      cbAr_umdapr.DataSource = dttAr_umdapr
      cbAr_umdapr.ValueMember = "cod"
      cbAr_umdapr.DisplayMember = "val"

      dttAr_umdapra.Columns.Add("cod", GetType(String))
      dttAr_umdapra.Columns.Add("val", GetType(String))
      dttAr_umdapra.Rows.Add(New Object() {"C", "Confezione"})
      dttAr_umdapra.Rows.Add(New Object() {"P", "Principale"})
      dttAr_umdapra.Rows.Add(New Object() {"Q", "Formula"})
      dttAr_umdapra.Rows.Add(New Object() {"S", "Secondaria"})
      dttAr_umdapra.AcceptChanges()

      cbAr_umdapra.DataSource = dttAr_umdapra
      cbAr_umdapra.ValueMember = "cod"
      cbAr_umdapra.DisplayMember = "val"

      dttAr_umpdapr.Columns.Add("cod", GetType(String))
      dttAr_umpdapr.Columns.Add("val", GetType(String))
      dttAr_umpdapr.Rows.Add(New Object() {"C", "Confezione"})
      dttAr_umpdapr.Rows.Add(New Object() {"P", "Principale"})
      dttAr_umpdapr.Rows.Add(New Object() {"Q", "Formula"})
      dttAr_umpdapr.Rows.Add(New Object() {"S", "Secondaria"})
      dttAr_umpdapr.AcceptChanges()

      cbAr_umpdapr.DataSource = dttAr_umpdapr
      cbAr_umpdapr.ValueMember = "cod"
      cbAr_umpdapr.DisplayMember = "val"

      dttAr_umpdapra.Columns.Add("cod", GetType(String))
      dttAr_umpdapra.Columns.Add("val", GetType(String))
      dttAr_umpdapra.Rows.Add(New Object() {"C", "Confezione"})
      dttAr_umpdapra.Rows.Add(New Object() {"P", "Principale"})
      dttAr_umpdapra.Rows.Add(New Object() {"Q", "Formula"})
      dttAr_umpdapra.Rows.Add(New Object() {"S", "Secondaria"})
      dttAr_umpdapra.AcceptChanges()

      cbAr_umpdapra.DataSource = dttAr_umpdapra
      cbAr_umpdapra.ValueMember = "cod"
      cbAr_umpdapra.DisplayMember = "val"

      dttAr_makebuy.Columns.Add("cod", GetType(String))
      dttAr_makebuy.Columns.Add("val", GetType(String))
      dttAr_makebuy.Rows.Add(New Object() {" ", "(Come da distinta)"})
      dttAr_makebuy.Rows.Add(New Object() {"M", "Make"})
      dttAr_makebuy.Rows.Add(New Object() {"B", "Buy"})
      dttAr_makebuy.AcceptChanges()

      cbAr_makebuy.DataSource = dttAr_makebuy
      cbAr_makebuy.ValueMember = "cod"
      cbAr_makebuy.DisplayMember = "val"

      dttAr_gestser.Columns.Add("cod", GetType(String))
      dttAr_gestser.Columns.Add("val", GetType(String))
      dttAr_gestser.Rows.Add(New Object() {"N", "No"})
      dttAr_gestser.Rows.Add(New Object() {"A", "Automatico in coda"})
      dttAr_gestser.Rows.Add(New Object() {"R", "Come da funzione"})
      dttAr_gestser.Rows.Add(New Object() {"X", "Come da funzione solo speciali"})
      dttAr_gestser.Rows.Add(New Object() {"Y", "Automatico in coda solo speciali"})
      dttAr_gestser.Rows.Add(New Object() {"O", "No, sempre non speciale"})
      dttAr_gestser.AcceptChanges()

      cbAr_gestser.DataSource = dttAr_gestser
      cbAr_gestser.ValueMember = "cod"
      cbAr_gestser.DisplayMember = "val"

      dttAr_tipitemcp3.Columns.Add("cod", GetType(String))
      dttAr_tipitemcp3.Columns.Add("val", GetType(String))
      dttAr_tipitemcp3.Rows.Add(New Object() {"R", "Reale"})
      dttAr_tipitemcp3.Rows.Add(New Object() {"Y", "Indeterminato"})
      dttAr_tipitemcp3.Rows.Add(New Object() {"T", "Configurato tecnico"})
      dttAr_tipitemcp3.Rows.Add(New Object() {"A", "Virtuale aperto"})
      dttAr_tipitemcp3.Rows.Add(New Object() {"C", "Virtuale chiuso"})
      dttAr_tipitemcp3.Rows.Add(New Object() {"E", "Extralogistico"})
      '-------------------------------------------------------------------------------------------------------------
      '--- Legge l'opzione globale per l'abilitazione/disabilitazione del campo del Conf. Tecnico(3)/Conf. Comm.(4):
      '--- cbTipitemcp3 (ARTICO.ar_tipitemcp3)
      '-------------------------------------------------------------------------------------------------------------
      oCleArti.strTipoConfiguratore = oMenu.GetSettingBus("OPZIONI", ".", ".", "TipoConfiguratore", "1", " ", "1")
      If oCleArti.strTipoConfiguratore = "4" Then
        dttAr_tipitemcp3.Rows.Add(New Object() {"X", "Superpadre"})
        dttAr_tipitemcp3.Rows.Add(New Object() {"F", "Voce commerciale fittizia"})
        dttAr_tipitemcp3.Rows.Add(New Object() {"V", "Voce commerciale normale"})
        dttAr_tipitemcp3.Rows.Add(New Object() {"L", "Codice identificativo voce su listino"})
        dttAr_tipitemcp3.Rows.Add(New Object() {"S", "Voce commerciale speciale"})
        dttAr_tipitemcp3.Rows.Add(New Object() {"P", "Configurato principale"})
      End If
      dttAr_tipitemcp3.AcceptChanges()

      cbAr_tipitemcp3.DataSource = dttAr_tipitemcp3
      cbAr_tipitemcp3.ValueMember = "cod"
      cbAr_tipitemcp3.DisplayMember = "val"

      dttAr_tipscarlotx.Columns.Add("cod", GetType(String))
      dttAr_tipscarlotx.Columns.Add("val", GetType(String))
      dttAr_tipscarlotx.Rows.Add(New Object() {"M", "Manuale"})
      dttAr_tipscarlotx.Rows.Add(New Object() {"D", "Data scadenza"})
      dttAr_tipscarlotx.Rows.Add(New Object() {"U", "Data scad. unico lotto"})
      dttAr_tipscarlotx.Rows.Add(New Object() {"F", "Fifo"})
      dttAr_tipscarlotx.Rows.Add(New Object() {"L", "Lifo"})
      dttAr_tipscarlotx.AcceptChanges()

      cbAr_tipscarlotx.DataSource = dttAr_tipscarlotx
      cbAr_tipscarlotx.ValueMember = "cod"
      cbAr_tipscarlotx.DisplayMember = "val"


      dttAr_deterior.Columns.Add("cod", GetType(String))
      dttAr_deterior.Columns.Add("val", GetType(String))
      dttAr_deterior.Rows.Add(New Object() {"S", "Prodotto alimentare deteriorabile"})
      dttAr_deterior.Rows.Add(New Object() {"N", "Prodotto alimentare non deteriorabile"})
      dttAr_deterior.Rows.Add(New Object() {" ", "Altro"})

      cbAr_deterior.DataSource = dttAr_deterior
      cbAr_deterior.ValueMember = "cod"
      cbAr_deterior.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Dim strFormatAr_conver As String
    Dim nLungMaxAr_conver As Integer
    Dim strFormatAr_sublotto As String
    Dim nLungMaxAr_sublotto As Integer
    Dim strNomeCampoAr_sublotto As String
    Dim strNomeCampoAr_rrfence As String
    Dim strNomeCampoAr_ggant As String
    Dim strNomeCampoAr_ggpost As String
    Dim strNomeCampoAr_forn As String
    Dim strFormatAr_scomin As String
    Dim strFormatAr_minord As String
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbDuplica.GlyphPath = (oApp.ChildImageDir & "\duplica.gif")
        tlbApri.GlyphPath = (oApp.ChildImageDir & "\open.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbBarcode.GlyphPath = (oApp.ChildImageDir & "\barcode.gif")
        tlbKit.GlyphPath = (oApp.ChildImageDir & "\kit_1.gif")
        tlbConai.GlyphPath = (oApp.ChildImageDir & "\conai.gif")
        tlbOle.GlyphPath = (oApp.ChildImageDir & "\ole_1.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      If oCleArti.bConver9Dec = False Then
        strFormatAr_conver = oApp.FormatQta
        nLungMaxAr_conver = 12
      Else
        strFormatAr_conver = "#,##0.000000000"
        nLungMaxAr_conver = 18
      End If

      If oCleArti.strTipoConfiguratore > "2" Then
        strNomeCampoAr_sublotto = oApp.Tr(Me, 128788387403213993, "Lotto fisso")
        strFormatAr_sublotto = "0"
        nLungMaxAr_sublotto = 10
      Else
        strNomeCampoAr_sublotto = oApp.Tr(Me, 128788387442591009, "Sottolotto")
        strFormatAr_sublotto = oApp.FormatQta
        nLungMaxAr_sublotto = 13
      End If

      If oCleArti.strTipoConfiguratore > "2" Then
        strNomeCampoAr_rrfence = oApp.Tr(Me, 128554260471611269, "Lead Time standard")
      Else
        strNomeCampoAr_rrfence = oApp.Tr(Me, 128554260491717209, "RR Fence")
      End If

      If oCleArti.strTipoConfiguratore > "2" Then
        strNomeCampoAr_ggant = oApp.Tr(Me, 128554262098010369, "N° giorni orizzonte per rilascio")
      Else
        strNomeCampoAr_ggant = oApp.Tr(Me, 128554262119674909, "Giorni di tolleranza Anticipo M.R.P.")
      End If

      If oCleArti.strTipoConfiguratore > "2" Then
        strNomeCampoAr_ggpost = oApp.Tr(Me, 128556850606120748, "N° giorni orizzonte di controllo preventivo consegna")
      Else
        strNomeCampoAr_ggpost = oApp.Tr(Me, 128556850631449934, "Giorni di tolleranza Posticipo M.R.P.")
      End If

      If oCleArti.strTipoConfiguratore > "2" Then
        strNomeCampoAr_forn = oApp.Tr(Me, 128557487229732616, "Fornitore standard")
      Else
        strNomeCampoAr_forn = oApp.Tr(Me, 128557487270680334, "Fornitore 1")
      End If

      If oCleArti.strTipoConfiguratore > "2" Then
        strFormatAr_scomin = "0"
      Else
        strFormatAr_scomin = oApp.FormatQta
      End If

      If oCleArti.strTipoConfiguratore > "2" Then
        strFormatAr_minord = "0"
      Else
        strFormatAr_minord = oApp.FormatQta
      End If

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edAr_codart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788387525564007, "Codice articolo"), tabartico, False)
      edAr_codalt.NTSSetParam(oMenu, oApp.Tr(Me, 128788387549471481, "Codice altern.:"), CLN__STD.CodartMaxLen, True)
      edAr_descr.NTSSetParam(oMenu, oApp.Tr(Me, 128788387568222441, "Descrizione:"), 40, False)
      edAr_desint.NTSSetParam(oMenu, oApp.Tr(Me, 128788387583535725, "Descrizione2"), 40, True)
      edAr_tipo.NTSSetParam(oMenu, oApp.Tr(Me, 128788387601974169, "Tipo:"), 1, True)
      edAr_unmis.NTSSetParam(oMenu, oApp.Tr(Me, 128788387619631323, "Un. misura principale"), 3, True)
      edAr_unmis2.NTSSetParam(oMenu, oApp.Tr(Me, 128788387636194671, "Un. misura secondaria:"), 3, True)
      edAr_conver.NTSSetParam(oMenu, oApp.Tr(Me, 128788387652289245, "Quantità secondaria"), strFormatAr_conver, nLungMaxAr_conver, 0, 99999999)
      edAr_confez2.NTSSetParam(oMenu, oApp.Tr(Me, 128788387686822263, "Un. misura confezione:"), 3, True)
      edAr_qtacon2.NTSSetParam(oMenu, oApp.Tr(Me, 128788387704323159, "Quantità confezione"), oApp.FormatQta, 6, 0, 99999999)
      edAr_forn.NTSSetParamTabe(oMenu, strNomeCampoAr_forn, tabanagraf)
      edAr_forn2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788387722761603, "Fornitore 2"), tabanagraf)
      edAr_codiva.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788387744793981, "Codice IVA:"), tabciva)
      edAr_gruppo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788387766201327, "Gruppo merc.:"), tabgmer)
      edAr_sotgru.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788387787921189, "Sottogruppo merc.:"), tabsgme)
      edAr_controp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788387801828151, "Controp. vendite:"), tabcove)
      edAr_catlifo.NTSSetParam(oMenu, oApp.Tr(Me, 128788387829642075, "Coeff. ass. c.f.:"), "0", 4, 0, 9999)
      edAr_ubicaz.NTSSetParam(oMenu, oApp.Tr(Me, 128788387978712207, "Ubicazione:"), 18, True)
      edAr_scomin.NTSSetParam(oMenu, oApp.Tr(Me, 128788387936210031, "Scorta min"), strFormatAr_scomin, 13, 0, 9999999999)
      edAr_scomax.NTSSetParam(oMenu, oApp.Tr(Me, 128788387961367569, "Scorta max"), oApp.FormatQta, 13, 0, 9999999999)
      edAr_minord.NTSSetParam(oMenu, oApp.Tr(Me, 128788387994338007, "Qta Lotto std pr/ac."), strFormatAr_minord, 13, 0, 9999999999)
      edAr_ggrior.NTSSetParam(oMenu, oApp.Tr(Me, 128788388010745097, "Non usato 1"), "0", 3, 0, 999)
      edAr_sostit.NTSSetParam(oMenu, oApp.Tr(Me, 128788388028402251, "Art.sostitutivo:"), CLN__STD.CodartMaxLen, True)
      edAr_controa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788388043090503, "Controp. acquisti:"), tabcove)
      edAr_reparto.NTSSetParam(oMenu, oApp.Tr(Me, 128788388068404299, "Rep. (ECR):"), "0", 3, 0, 999)
      ckAr_stalist.NTSSetParam(oMenu, oApp.Tr(Me, 128788388084967647, "Stampa articolo nel listino"), "S", "N")
      edAr_contriva.NTSSetParam(oMenu, oApp.Tr(Me, 128788388101843511, "Contr.IVA:"), 3, True)
      edAr_famprod.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788388116219247, "Famiglia:"), tabcfam, True)
      edAr_numecr.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788388132001305, "Centro C.A.:"), tabcena)
      edAr_codvuo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788388148720911, "Cod.vuoto:"), tabcvuo)
      cbAr_flricmar.NTSSetParam(oApp.Tr(Me, 128788388166846839, "Ricarico/Margine:"))
      edAr_codpdon.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788388217318173, "Relazione listini:"), tabpdon)
      edAr_ricar1.NTSSetParam(oMenu, oApp.Tr(Me, 128788388233412747, "Ricarico/Margine1"), oApp.FormatImporti, 8, 0, 99999999)
      edAr_ricar2.NTSSetParam(oMenu, oApp.Tr(Me, 128788388246382161, "Ricarico/Margine2"), oApp.FormatImporti, 8, 0, 99999999)
      edAr_garacq.NTSSetParam(oMenu, oApp.Tr(Me, 128788388260757897, "Mesi gar.acq."), "0", 3, 0, 999)
      edAr_garven.NTSSetParam(oMenu, oApp.Tr(Me, 128788388276227439, "Mesi gar.vend."), "0", 3, 0, 999)
      edAr_prorig.NTSSetParam(oMenu, oApp.Tr(Me, 128788388294040851, "Prov Origine"), 2, True)
      edAr_percvst.NTSSetParam(oMenu, oApp.Tr(Me, 128788388307322781, "% Val.statistico:"), oApp.FormatSconti, 9, 0, 999999999)
      edAr_codnomc.NTSSetParam(oMenu, oApp.Tr(Me, 128788388323104839, "Codice nom.comb.:"), 10, True)
      edAr_pesolor.NTSSetParam(oMenu, oApp.Tr(Me, 128788388341543283, "Peso lordo:"), "#,##0.000000", 13, 0, 999999)
      edAr_pesonet.NTSSetParam(oMenu, oApp.Tr(Me, 128788388357169083, "P.netto:"), "#,##0.000000", 13, 0, 999999)
      edAr_paeorig.NTSSetParam(oMenu, oApp.Tr(Me, 128788388375763785, "Paese Origine"), 2, True)
      edAr_livmindb.NTSSetParam(oMenu, oApp.Tr(Me, 128788388389670747, "Liv. Min."), "0", 3, 0, 999)
      edAr_coddb.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788388433422987, "Cod. Distinta Base"), tabdistbas, True)
      ckAr_stainv.NTSSetParam(oMenu, oApp.Tr(Me, 128788388479362839, "Stampa articolo nell'inventario"), "S", "N")
      ckAr_stasche.NTSSetParam(oMenu, oApp.Tr(Me, 128788388521083725, "Stampa scheda articolo"), "S", "N")
      ckAr_geslotti.NTSSetParam(oMenu, oApp.Tr(Me, 128788388559210677, "Gestione Lotti"), "S", "N")
      ckAr_inesaur.NTSSetParam(oMenu, oApp.Tr(Me, 128788388573273897, "In Esaurimento"), "S", "N")
      cbAr_tipoopz.NTSSetParam(oApp.Tr(Me, 128788388587962149, "Tipo Opzione"))
      cbAr_polriord.NTSSetParam(oApp.Tr(Me, 128788388602962917, "Politica di Riordino"))
      cbAr_gescomm.NTSSetParam(oApp.Tr(Me, 128788388617963685, "Gestione per Comm."))
      edAr_fpfence.NTSSetParam(oMenu, oApp.Tr(Me, 128788388633276969, "Non usato 2"), "0", 3, 0, 999)
      edAr_rrfence.NTSSetParam(oMenu, strNomeCampoAr_rrfence, "0", 3, 0, 999)
      edAr_sostituito.NTSSetParam(oMenu, oApp.Tr(Me, 128788388673122759, "Art.sostituito:"), CLN__STD.CodartMaxLen, True)
      ckAr_critico.NTSSetParam(oMenu, oApp.Tr(Me, 128788388687029721, "&Componente critico"), "S", "N")
      edAr_formula.NTSSetParam(oMenu, oApp.Tr(Me, 128788388701561715, "Formula di trasformaz. in UMP"), 0, True)
      edAr_contros.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788388716874999, "Controp.scar.prod.:"), tabcove)
      ckAr_pesoca.NTSSetParam(oMenu, oApp.Tr(Me, 128788388736868733, "Non proporre le note art.sulle righe docum."), "1", "0")
      ckAr_gestmatr.NTSSetParam(oMenu, oApp.Tr(Me, 128788388759670575, "Gestione matricole"), "S", "N")
      cbAr_umdapr.NTSSetParam(oApp.Tr(Me, 128788388778099461, "U.M.vendite"))
      edAr_magstock.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788388797933940, "Mag. stoccaggio"), tabmaga)
      edAr_magprod.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788388812770755, "Mag. produzione"), tabmaga)
      cbAr_umintra2.NTSSetParam(oApp.Tr(Me, 128788388861497979, "UM secondaria:"))
      edAr_codappr.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788388879614511, "Approvvigionatore:"), tabappr)
      edAr_codmarc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788388893826618, "Marca:"), tabmarc)
      cbAr_tipokit.NTSSetParam(oApp.Tr(Me, 128788388911318442, "Tipo kit:"))
      edAr_perqta.NTSSetParam(oMenu, oApp.Tr(Me, 128788388932870868, "Molt.qtà/prezzo:"), "0", 12, 0, 999999999999)
      edAr_fcorrlt.NTSSetParam(oMenu, oApp.Tr(Me, 128788388949737984, "Fattore correz. L.T."), "#,##0.000000000", 10, 0, NTSCDec(9999.99999))
      cbAr_codtipa.NTSSetParam(oApp.Tr(Me, 128788388965043330, "Tipologia art."))
      edAr_verdb.NTSSetParam(oMenu, oApp.Tr(Me, 128788388981285738, "Vers. Distinta Base"), "0", 4, 0, 9999)
      ckAr_blocco.NTSSetParam(oMenu, oApp.Tr(Me, 128788388998309031, "&Blocco"), "S", "N")
      edAr_um4.NTSSetParam(oMenu, oApp.Tr(Me, 128788389013926731, "U.M. formula"), 3, True)
      cbAr_umdapra.NTSSetParam(oApp.Tr(Me, 128788389046880078, "U.M.carichi"))
      cbAr_umpdapr.NTSSetParam(oApp.Tr(Me, 128788389069681920, "U.M.prezzo vendita"))
      cbAr_umpdapra.NTSSetParam(oApp.Tr(Me, 128788389085143443, "U.M.prezzo acquisto"))
      edAr_gif1.NTSSetParam(oMenu, oApp.Tr(Me, 128788389100292612, "Immagine catalogo"), 50, True)
      edAr_gif2.NTSSetParam(oMenu, oApp.Tr(Me, 128788389121064153, "Immagine scheda"), 50, True)
      edAr_ggant.NTSSetParam(oMenu, strNomeCampoAr_ggant, "0", 3, 0, 999)
      edAr_ggpost.NTSSetParam(oMenu, strNomeCampoAr_ggpost, "0", 3, 0, 999)
      cbAr_gescon.NTSSetParam(oApp.Tr(Me, 128788389169791377, "Applica CONAI:"))
      edAr_flmod.NTSSetParam(oMenu, oApp.Tr(Me, 128788389189157325, "Modificabilità"), 20, True)
      edAr_codimba.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788389232262177, "Imballo"), tabimba)
      edAr_misura1.NTSSetParam(oMenu, oApp.Tr(Me, 128788389246786638, "Misure 1"), "#,##0.000000000", 12, 0, 1000000000000)
      edAr_misura2.NTSSetParam(oMenu, oApp.Tr(Me, 128788389262872869, "Misure 2"), "#,##0.000000000", 12, 0, 1000000000000)
      edAr_misura3.NTSSetParam(oMenu, oApp.Tr(Me, 128788389277709684, "Misure 3"), "#,##0.000000000", 12, 0, 1000000000000)
      ckAr_gesubic.NTSSetParam(oMenu, oApp.Tr(Me, 128788389293952092, "Gestione ubicazione"), "S", "N")
      ckAr_gesfasi.NTSSetParam(oMenu, oApp.Tr(Me, 128788389308320376, "Gestione fasi"), "S", "N")
      edAr_ultfase.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788389322220129, "Ultima fase"), tabartfasi)
      edAr_volume.NTSSetParam(oMenu, oApp.Tr(Me, 128788389340492838, "Volume"), oApp.FormatTempi, 8, 0, 999999999999)
      cbAr_makebuy.NTSSetParam(oApp.Tr(Me, 128788389366574397, "Make-or-Buy"))
      edAr_sublotto.NTSSetParam(oMenu, strNomeCampoAr_sublotto, strFormatAr_sublotto, nLungMaxAr_sublotto, 0, 9999999999)
      edAr_maxlotto.NTSSetParam(oMenu, oApp.Tr(Me, 128788389409054541, "lotto max."), oApp.FormatQta, 13, 0, 9999999999)
      edAr_ggragg.NTSSetParam(oMenu, oApp.Tr(Me, 128788389445912313, "g. ragg"), "0", 3, 0, 999)
      ckAr_ripriord.NTSSetParam(oMenu, oApp.Tr(Me, 128788389425609303, "Rip. su più fornitori"), "S", "N")
      cbAr_perragg.NTSSetParam(oApp.Tr(Me, 128788389456376172, "Periodo Ragg."))
      cbAr_gestser.NTSSetParam(oApp.Tr(Me, 128788389759671906, "Gestione codice combinazione (CP2)"))
      cbAr_tipitemcp3.NTSSetParam(oApp.Tr(Me, 128788389487143041, "Tipo item"))
      edAr_cartric.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788389501511325, "Art. vendita ricambio"), tabartico, True)
      edAr_cartcanas.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788389532122017, "Art. canone assistenza"), tabartico, True)
      edAr_cartcanol.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788389570541559, "Art. canone noleggio"), tabartico, True)
      edAr_note.NTSSetParam(oMenu, oApp.Tr(Me, 128788389591781631, "Note"), 0, True)
      edAr_claprov.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788389609429632, "Cl.provv."), tabcpar)
      edAr_clascon.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128788389631606766, "Cl.sconto"), tabcsar)
      edAr_scosic.NTSSetParam(oMenu, oApp.Tr(Me, 128842551672201674, "Scorta di sicurezza"), "0.00", 9, 0, 999999999)
      edAr_coddicv.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129319739858685136, "Valori aggregazione budget"), tabdicv, True)
      edAr_coddica.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129319739858841383, "Aggregazione budget"), tabdica, True)
      edAr_codtcdc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129319739859153877, "Tipologia entità"), tabtcdc)
      edAr_codtlox.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129512421717078721, "Cod. modalità di creazione autom. lotto"), tablotx)
      cbAr_tipscarlotx.NTSSetParam(oApp.Tr(Me, 129512424576749075, "Modalità di scarico lotto"))
      ckAr_flgift.NTSSetParam(oMenu, oApp.Tr(Me, 129991920472831843, "Gestione gift card"), "S", "N")
      ckAr_webvis.NTSSetParam(oMenu, oApp.Tr(Me, 130415037778373729, "Articolo visibile dall'applicazione esterna"), "S", "N")
      ckAr_webusat.NTSSetParam(oMenu, oApp.Tr(Me, 130415038096848244, "Articolo usato"), "S", "N")
      ckAr_webvend.NTSSetParam(oMenu, oApp.Tr(Me, 130415038285040564, "Articolo vendibile da applicazione esterna"), "S", "N")
      edAr_codseat.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130415038617667484, "Codice Set di Attributi"), tabseat)
      ckAr_consmrp.NTSSetParam(oMenu, oApp.Tr(Me, 128788388479362039, "Considera in MRP/Distinte Base"), "S", "N")
      cbAr_deterior.NTSSetParam(oApp.Tr(Me, 130951839641336884, "Articolo deteriorabile"))

      'Logistica magazzino su palmare
      edAr_ubicpr.NTSSetParam(oMenu, oApp.Tr(Me, 128842551693608472, "Ubicazione di produzione"), 18)
      edAr_ubicri.NTSSetParam(oMenu, oApp.Tr(Me, 128842551693764726, "Ubicazione di ricezione"), 18)
      edAr_ubicus.NTSSetParam(oMenu, oApp.Tr(Me, 128842551693920980, "Ubicazione di uscita"), 18)
      edAr_ubicst.NTSSetParam(oMenu, oApp.Tr(Me, 128842551694077234, "Ubicazione di stock"), 18)
      edAr_indrot.NTSSetParam(oMenu, oApp.Tr(Me, 128842551694858504, "Indice di rotazione"), "0.00", 9, 0, 999999999)
      edAr_scominpk.NTSSetParam(oMenu, oApp.Tr(Me, 128842551695014758, "Scorta minima ubicazione di picking"), oApp.FormatQta, 9, 0, 999999999)
      edAr_codgrlo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128842551695171012, "Gruppo logistico"), tabgrlo)
      edAr_converp.NTSSetParam(oMenu, oApp.Tr(Me, 128842551695327266, "Rapporto di conversione"), "0.00", 9, 0, 999999999)
      ckAr_staetip.NTSSetParam(oMenu, oApp.Tr(Me, 128842551695483520, "Stampa etichette unità di carico quando vengono generate"), "S", "N")
      ckAr_staeti.NTSSetParam(oMenu, oApp.Tr(Me, 128842551695639774, "Stampa etichette articolo in ricezione merce"), "S", "N")

      ceListini.NTSSetParam(oMenu, "Listini", "BNMGARTI", Nothing)
      ceListini.tlbListEsci.Enabled = False
      ceListini.LcTipo = " "
      ceListini.LcCodart = ""
      ceListini.LcConto = 0
      AddHandler ceListini.VaiScontoCollegato, AddressOf ceListini_VaiScontoCollegato

      ceSconti.NTSSetParam(oMenu, "Sconti", "BNMGARTI", Nothing)
      ceSconti.tlbScontiEsci.Enabled = False
      ceSconti.TipoSconto = 0
      ceSconti.SoCodart = ""
      ceSconti.SoConto = 0
      ceSconti.SoClasseCli = 0
      ceSconti.SoClasseArt = 0
      AddHandler ceSconti.VaiListinoCollegato, AddressOf ceSconti_VaiListinoCollegato

      ceProvvig.NTSSetParam(oMenu, "Provvigioni", "BNMGARTI", Nothing)
      ceProvvig.tlbProvEsci.Enabled = False
      ceProvvig.TipoProvv = 1
      ceProvvig.PerCodart = ""
      ceProvvig.PerConto = 0
      ceProvvig.PerCodcage = 0
      ceProvvig.PerClasseCli = 0
      ceProvvig.PerClasseArt = 0

      edAr_descr.NTSSetRichiesto()

      edAr_unmis.NTSSetParamZoom("ZOOMTABUMIS")
      edAr_unmis2.NTSSetParamZoom("ZOOMTABUMIS")
      edAr_confez2.NTSSetParamZoom("ZOOMTABUMIS")
      edAr_sostit.NTSSetParamZoom("ZOOMARTICO")
      edAr_codnomc.NTSSetParamZoom("ZOOMTARIC")
      edAr_sostituito.NTSSetParamZoom("ZOOMARTICO")
      edAr_um4.NTSSetParamZoom("ZOOMTABUMIS")
      edAr_codtlox.NTSSetParamZoom("ZOOMTABLOTX")
      edAr_reparto.NTSSetParamZoom("ZOOMTABREAR")

      cbAr_tipitemcp3.Visible = False
      lbAr_tipitemcp3.Visible = False

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
      'se i controlli erano giÃ  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edAr_codart.NTSDbField = "ARTICO.ar_codart"
      edAr_codalt.NTSDbField = "ARTICO.ar_codalt"
      edAr_descr.NTSDbField = "ARTICO.ar_descr"
      edAr_desint.NTSDbField = "ARTICO.ar_desint"
      edAr_tipo.NTSDbField = "ARTICO.ar_tipo"
      edAr_unmis.NTSDbField = "ARTICO.ar_unmis"
      edAr_unmis2.NTSDbField = "ARTICO.ar_unmis2"
      edAr_conver.NTSDbField = "ARTICO.ar_conver"
      edAr_confez2.NTSDbField = "ARTICO.ar_confez2"
      edAr_qtacon2.NTSDbField = "ARTICO.ar_qtacon2"
      edAr_forn.NTSDbField = "ARTICO.ar_forn"
      edAr_forn2.NTSDbField = "ARTICO.ar_forn2"
      edAr_codiva.NTSDbField = "ARTICO.ar_codiva"
      edAr_gruppo.NTSDbField = "ARTICO.ar_gruppo"
      edAr_sotgru.NTSDbField = "ARTICO.ar_sotgru"
      edAr_controp.NTSDbField = "ARTICO.ar_controp"
      edAr_catlifo.NTSDbField = "ARTICO.ar_catlifo"
      edAr_ubicaz.NTSDbField = "ARTICO.ar_ubicaz"
      edAr_scomin.NTSDbField = "ARTICO.ar_scomin"
      edAr_scomax.NTSDbField = "ARTICO.ar_scomax"
      edAr_minord.NTSDbField = "ARTICO.ar_minord"
      edAr_ggrior.NTSDbField = "ARTICO.ar_ggrior"
      edAr_sostit.NTSDbField = "ARTICO.ar_sostit"
      edAr_controa.NTSDbField = "ARTICO.ar_controa"
      edAr_reparto.NTSDbField = "ARTICO.ar_reparto"
      ckAr_stalist.NTSText.NTSDbField = "ARTICO.ar_stalist"
      edAr_contriva.NTSDbField = "ARTICO.ar_contriva"
      edAr_famprod.NTSDbField = "ARTICO.ar_famprod"
      edAr_numecr.NTSDbField = "ARTICO.ar_numecr"
      edAr_codvuo.NTSDbField = "ARTICO.ar_codvuo"
      cbAr_flricmar.NTSDbField = "ARTICO.ar_flricmar"
      edAr_codpdon.NTSDbField = "ARTICO.ar_codpdon"
      edAr_ricar1.NTSDbField = "ARTICO.ar_ricar1"
      edAr_ricar2.NTSDbField = "ARTICO.ar_ricar2"
      edAr_garacq.NTSDbField = "ARTICO.ar_garacq"
      edAr_garven.NTSDbField = "ARTICO.ar_garven"
      edAr_prorig.NTSDbField = "ARTICO.ar_prorig"
      edAr_percvst.NTSDbField = "ARTICO.ar_percvst"
      edAr_codnomc.NTSDbField = "ARTICO.ar_codnomc"
      edAr_pesolor.NTSDbField = "ARTICO.ar_pesolor"
      edAr_pesonet.NTSDbField = "ARTICO.ar_pesonet"
      edAr_paeorig.NTSDbField = "ARTICO.ar_paeorig"
      edAr_livmindb.NTSDbField = "ARTICO.ar_livmindb"
      edAr_coddb.NTSDbField = "ARTICO.ar_coddb"
      ckAr_stainv.NTSText.NTSDbField = "ARTICO.ar_stainv"
      ckAr_stasche.NTSText.NTSDbField = "ARTICO.ar_stasche"
      ckAr_geslotti.NTSText.NTSDbField = "ARTICO.ar_geslotti"
      ckAr_inesaur.NTSText.NTSDbField = "ARTICO.ar_inesaur"
      edAr_note.NTSDbField = "ARTICO.ar_note"
      cbAr_tipoopz.NTSDbField = "ARTICO.ar_tipoopz"
      cbAr_polriord.NTSDbField = "ARTICO.ar_polriord"
      cbAr_gescomm.NTSDbField = "ARTICO.ar_gescomm"
      edAr_fpfence.NTSDbField = "ARTICO.ar_fpfence"
      edAr_rrfence.NTSDbField = "ARTICO.ar_rrfence"
      edAr_sostituito.NTSDbField = "ARTICO.ar_sostituito"
      ckAr_critico.NTSText.NTSDbField = "ARTICO.ar_critico"
      edAr_formula.NTSDbField = "ARTICO.ar_formula"
      edAr_contros.NTSDbField = "ARTICO.ar_contros"
      ckAr_pesoca.NTSText.NTSDbField = "ARTICO.ar_pesoca"
      ckAr_gestmatr.NTSText.NTSDbField = "ARTICO.ar_gestmatr"
      cbAr_umdapr.NTSDbField = "ARTICO.ar_umdapr"
      edAr_magstock.NTSDbField = "ARTICO.ar_magstock"
      edAr_magprod.NTSDbField = "ARTICO.ar_magprod"
      cbAr_umintra2.NTSDbField = "ARTICO.ar_umintra2"
      edAr_codappr.NTSDbField = "ARTICO.ar_codappr"
      edAr_codmarc.NTSDbField = "ARTICO.ar_codmarc"
      cbAr_tipokit.NTSDbField = "ARTICO.ar_tipokit"
      edAr_perqta.NTSDbField = "ARTICO.ar_perqta"
      edAr_fcorrlt.NTSDbField = "ARTICO.ar_fcorrlt"
      cbAr_codtipa.NTSDbField = "ARTICO.ar_codtipa"
      edAr_verdb.NTSDbField = "ARTICO.ar_verdb"
      ckAr_blocco.NTSText.NTSDbField = "ARTICO.ar_blocco"
      edAr_um4.NTSDbField = "ARTICO.ar_um4"
      cbAr_umdapra.NTSDbField = "ARTICO.ar_umdapra"
      cbAr_umpdapr.NTSDbField = "ARTICO.ar_umpdapr"
      cbAr_umpdapra.NTSDbField = "ARTICO.ar_umpdapra"
      edAr_gif1.NTSDbField = "ARTICO.ar_gif1"
      edAr_gif2.NTSDbField = "ARTICO.ar_gif2"
      edAr_ggant.NTSDbField = "ARTICO.ar_ggant"
      edAr_ggpost.NTSDbField = "ARTICO.ar_ggpost"
      cbAr_gescon.NTSDbField = "ARTICO.ar_gescon"
      edAr_flmod.NTSDbField = "ARTICO.ar_flmod"
      edAr_codimba.NTSDbField = "ARTICO.ar_codimba"
      edAr_misura1.NTSDbField = "ARTICO.ar_misura1"
      edAr_misura2.NTSDbField = "ARTICO.ar_misura2"
      edAr_misura3.NTSDbField = "ARTICO.ar_misura3"
      ckAr_gesubic.NTSText.NTSDbField = "ARTICO.ar_gesubic"
      ckAr_gesfasi.NTSText.NTSDbField = "ARTICO.ar_gesfasi"
      edAr_ultfase.NTSDbField = "ARTICO.ar_ultfase"
      edAr_volume.NTSDbField = "ARTICO.ar_volume"
      cbAr_makebuy.NTSDbField = "ARTICO.ar_makebuy"
      edAr_sublotto.NTSDbField = "ARTICO.ar_sublotto"
      edAr_maxlotto.NTSDbField = "ARTICO.ar_maxlotto"
      edAr_ggragg.NTSDbField = "ARTICO.ar_ggragg"
      ckAr_ripriord.NTSText.NTSDbField = "ARTICO.ar_ripriord"
      cbAr_perragg.NTSDbField = "ARTICO.ar_perragg"
      cbAr_gestser.NTSDbField = "ARTICO.ar_gestser"
      cbAr_tipitemcp3.NTSDbField = "ARTICO.ar_tipitemcp3"
      edAr_cartric.NTSDbField = "ARTICO.ar_cartric"
      edAr_cartcanas.NTSDbField = "ARTICO.ar_cartcanas"
      edAr_cartcanol.NTSDbField = "ARTICO.ar_cartcanol"
      lbXx_codmarc.NTSDbField = "ARTICO.xx_codmarc"
      lbXx_codpdon.NTSDbField = "ARTICO.xx_codpdon"
      lbXx_codappr.NTSDbField = "ARTICO.xx_codappr"
      lbXx_numecr.NTSDbField = "ARTICO.xx_numecr"
      lbXx_forn.NTSDbField = "ARTICO.xx_forn"
      lbXx_forn2.NTSDbField = "ARTICO.xx_forn2"
      lbXx_codiva.NTSDbField = "ARTICO.xx_codiva"
      lbXx_gruppo.NTSDbField = "ARTICO.xx_gruppo"
      lbXx_sotgru.NTSDbField = "ARTICO.xx_sotgru"
      lbXx_controp.NTSDbField = "ARTICO.xx_controp"
      lbXx_controa.NTSDbField = "ARTICO.xx_controa"
      lbXx_contros.NTSDbField = "ARTICO.xx_contros"
      lbXx_famprod.NTSDbField = "ARTICO.xx_famprod"
      lbXx_codvuo.NTSDbField = "ARTICO.xx_codvuo"
      lbXx_magstock.NTSDbField = "ARTICO.xx_magstock"
      lbXx_magprod.NTSDbField = "ARTICO.xx_magprod"
      lbXx_codimba.NTSDbField = "ARTICO.xx_codimba"
      lbXx_cartric.NTSDbField = "ARTICO.xx_cartric"
      lbXx_cartcanas.NTSDbField = "ARTICO.xx_cartcanas"
      lbXx_cartcanol.NTSDbField = "ARTICO.xx_cartcanol"
      edAr_claprov.NTSDbField = "ARTICO.ar_claprov"
      edAr_clascon.NTSDbField = "ARTICO.ar_clascon"
      lbXx_clascon.NTSDbField = "ARTICO.xx_clascon"
      lbXx_claprov.NTSDbField = "ARTICO.xx_claprov"
      edAr_coddicv.NTSDbField = "ARTICO.ar_coddicv"
      edAr_coddica.NTSDbField = "ARTICO.ar_coddica"
      edAr_codtcdc.NTSDbField = "ARTICO.ar_codtcdc"
      lbXx_codtcdc.NTSDbField = "ARTICO.xx_codtcdc"
      lbXx_coddicv.NTSDbField = "ARTICO.xx_coddicv"
      lbXx_coddica.NTSDbField = "ARTICO.xx_coddica"
      edAr_codtlox.NTSDbField = "ARTICO.ar_codtlox"
      lbXx_codtlox.NTSDbField = "ARTICO.xx_codtlox"
      cbAr_tipscarlotx.NTSDbField = "ARTICO.ar_tipscarlotx"
      ckAr_flgift.NTSText.NTSDbField = "ARTICO.ar_flgift"
      lbXx_reparto.NTSDbField = "ARTICO.xx_reparto"
      cbAr_deterior.NTSDbField = "ARTICO.ar_deterior"

      edAr_scosic.NTSDbField = "ARTICO.ar_scosic"
      edAr_ubicpr.NTSDbField = "ARTICO.ar_ubicpr"
      edAr_ubicri.NTSDbField = "ARTICO.ar_ubicri"
      edAr_ubicus.NTSDbField = "ARTICO.ar_ubicus"
      edAr_ubicst.NTSDbField = "ARTICO.ar_ubicst"
      edAr_indrot.NTSDbField = "ARTICO.ar_indrot"
      edAr_scominpk.NTSDbField = "ARTICO.ar_scominpk"
      edAr_codgrlo.NTSDbField = "ARTICO.ar_codgrlo"
      edAr_converp.NTSDbField = "ARTICO.ar_converp"
      ckAr_staetip.NTSText.NTSDbField = "ARTICO.ar_staetip"
      ckAr_staeti.NTSText.NTSDbField = "ARTICO.ar_staeti"

      ckAr_webvis.NTSText.NTSDbField = "ARTICO.ar_webvis"
      ckAr_webusat.NTSText.NTSDbField = "ARTICO.ar_webusat"
      ckAr_webvend.NTSText.NTSDbField = "ARTICO.ar_webvend"
      edAr_codseat.NTSDbField = "ARTICO.ar_codseat"
      lbXx_codseat.NTSDbField = "ARTICO.xx_codseat"

      ckAr_consmrp.NTSText.NTSDbField = "ARTICO.ar_consmrp"
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcArti, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMMGARTI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      If Me.Modal = True Then Me.MinimizeBox = False

      If bPreload Then Return

      'prende, se esistono, le opzioni dal registro
      oCleArti.nCodiva = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CodIva", "0", " ", "0"))
      oCleArti.nControa = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Controa", "0", " ", "0"))
      oCleArti.nControp = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Controp", "0", " ", "0"))
      oCleArti.nContros = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Contros", "0", " ", "0"))
      oCleArti.strUnmis = oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "UnMis", " ", " ", " ")
      oCleArti.strInizioValListini = NTSCStr(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DataInizioValListini", NTSCStr(Now), " ", NTSCStr(Now)))
      oCleArti.strInizioValSconti = NTSCStr(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DataInizioValSconti", NTSCStr(Now), " ", NTSCStr(Now)))
      oCleArti.strInizioValProvv = NTSCStr(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DataInizioValProvv", NTSCStr(Now), " ", NTSCStr(Now)))
      oCleArti.bConver9Dec = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Conver9Dec", "0", " ", "0"))
      oCleArti.bSbloccaFLotto = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "SbloccaFLotto", "0", " ", "0"))
      oCleArti.strListiniAbilitati = oMenu.GetSettingBus("BSMGARTI", "OPZIONIUT", ".", "ListiniAbilitati", "", " ", "")
      oCleArti.bGestTabUnmis = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GestTabUnmis", "0", " ", "0"))
      oCleArti.nCodArtDaCat = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoTipoGen", "0", " ", "0"))
      oCleArti.nCodArtDaCatNListPubb = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoNListPubb", "11", " ", "11"))
      oCleArti.nCodArtDaCatNListIngr = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoNListIngr", "12", " ", "12"))
      '----------------------------------------------------------------------
      '--- Opzioni di registro per la gestione dei flag nella modale relativa
      '--- alla duplicazione dell'articolo
      '----------------------------------------------------------------------
      oCleArti.bDuplicaListini = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DuplicaListini", "-1", " ", "-1"))
      oCleArti.bDuplicaSconti = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DuplicaSconti", "-1", " ", "-1"))
      oCleArti.bDuplicaProvvigioni = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DuplicaProvvigioni", "-1", " ", "-1"))
      oCleArti.bDuplicaDescrLingua = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DuplicaDescrLingua", "-1", " ", "-1"))
      oCleArti.bDuplicaKit = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DuplicaKit", "-1", " ", "-1"))
      oCleArti.bDuplicaConai = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DuplicaConai", "-1", " ", "-1"))
      oCleArti.bDuplicaFasi = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "DuplicaFasi", "-1", " ", "-1"))
      oCleArti.bGestUbicSenzaLext = CBool(NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GestUbicSenzaLext", "0", " ", "0"))) 'NON DOCUMENTARE
      '-------------------------------------------------------------------------------------------------------
      'Opzione globale per la gestione dei prezzi riferiti ad una unità di misura diversa da quella principale
      oCleArti.bAbilitaPrezzoUM = CBool(oMenu.GetSettingBus("OPZIONI", ".", ".", "AbilitaPrezzoUM", "0", " ", "0"))
      oCleArti.bValidaNomenclCombin = CBool(NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "ValidaNomenclCombin", "0", " ", "0"))) 'se abilitata in fase di inserimento delal nomenclatura la valida
      oCleArti.strAltezzaMaxImg = oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "AltezzaMaxImg", "0", " ", "0")

      '-------------------------------------------------------------------------------------------------------
      nDimensioneImmagine = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "ImageSizeLimit", "0", " ", "0"))
      'generazione barcode
      oCleArti.bCreaBarcodeE13 = CBool(Val(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CreaBarcodeE13", "0", " ", "0")))
      oCleArti.bIndicod = CBool(Val(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Indicod", "0", " ", "0")))
      oCleArti.strPrefixEAN13 = Trim(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "PrefixEAN13", "", " ", ""))
      '-------------------------------------------------------------------------------------------------------
      oCleArti.bSelCodiceNoApri = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "SelCodiceNoApri", "0", " ", "0"))
      '-------------------------------------------------------------------------------------------------------
      If oCleArti.bIndicod = True Then
        Select Case Len(oCleArti.strPrefixEAN13)
          Case 0
            oApp.MsgBoxErr(oApp.Tr(Me, 128802017930909547, "L'impostazione di registro 'PrefixEAN13' non è corretta," & vbCrLf & _
              "pertanto l'eventuale creazione di Barcode EAN13" & _
              "sarà fatta seguendo i normali standard."))
            oCleArti.bIndicod = False
          Case 1 To 11
            'OK
          Case 12
            oApp.MsgBoxErr(oApp.Tr(Me, 128584497554814492, "L'impostazione di registro 'PrefixEAN13' è di 12 caratteri," & vbCrLf & _
              "pertanto l'eventuale creazione di Barcode EAN13" & _
              "sarà fatta seguendo i normali standard."))
            oCleArti.bIndicod = False
          Case Else
            oApp.MsgBoxErr(oApp.Tr(Me, 128584497581622412, "L'impostazione di registro 'PrefixEAN13' supera i 12 caratteri," & vbCrLf & _
              "pertanto l'eventuale creazione di Barcode EAN13" & _
              "sarà fatta seguendo i normali standard."))
            oCleArti.bIndicod = False
        End Select
      End If
      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If oCleArti.strTipoConfiguratore > "2" Then
        GctlSetVisEnab(lbAr_tipitemcp3, False)
        GctlSetVisEnab(lbAr_tipitemcp3, True)
        GctlSetVisEnab(cbAr_tipitemcp3, False)
        GctlSetVisEnab(cbAr_tipitemcp3, True)
      End If
      '-------------------------------------------------------------------------------------------------------
      '--- Se c'è il modulo "Config. di Prodotto su D.B. Neutra"
      '--- abilita il campo relativo a 'Gestione Serial': ar_gestser
      '-----------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCP2) Then
        oCleArti.bCP2 = True
        GctlSetVisEnab(lbAr_gestser, False)
        GctlSetVisEnab(cbAr_gestser, False)
      Else
        oCleArti.bCP2 = False
        lbAr_gestser.Enabled = False
        cbAr_gestser.Enabled = False
      End If

      '-------------------------------------------------------------------------------------------------------
      '--- Se non c'è il modulo "Logistica Estesa"
      '--- disabilita i campi relativi a: ar_gesubic, ar_gesfasi, ar_ultfase
      '--- e la voce di menu per la gestione di ATRFASI
      '-----------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtLEX) Or CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCP) Then
        oCleArti.bLogisticaEstesa = True
      Else
        oCleArti.bLogisticaEstesa = False
        tlbFasiArticolo.Enabled = False
        tlbFasiArticolo.Visible = False
        If Not oCleArti.bGestUbicSenzaLext Then
          ckAr_gesubic.Enabled = False
        End If
        ckAr_gesfasi.Enabled = False
        edAr_ultfase.Enabled = False
        lbAr_ultfase.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      oCleArti.bModuloORTO = False
      If CBool(oMenu.ModuliSupExtDittaDitt(DittaCorrente) And bsModSupExtJOR) Then oCleArti.bModuloORTO = True
      '--------------------------------------------------------------------------------------------------------------
      'Il catalogo fornitori non è disponibile se si ha solo il magazzino easy
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtMGE) AndAlso Not CBool(oMenu.ModuliDittaDitt(DittaCorrente) And bsModMG) Then tlbAcquisisciDaCatForn.Visible = False
      '--------------------------------------------------------------------------------------------------------------
      'Mostro il combo degli articoli deteriorabili solo se ho l'opzione di registro attivata (che sia su opzioni o opzionidoc non importa)
      If oCleArti.ArticoliDeteriorabili() Then
        GctlSetVisible(cbAr_deterior)
        GctlSetVisible(lbAr_deterior)
      Else
        cbAr_deterior.Visible = False
        lbAr_deterior.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      SetStato(0)

      oCleArti.bDuplicaArt = False
      '-----------------------------------------------------------------------------------------
      oCleArti.strUnmisOrigine = ""
      oCleArti.strUnmis2Origine = ""
      oCleArti.strConfez2Origine = ""
      oCleArti.strUm4Origine = ""
      '-----------------------------------------------------------------------------------------
      '--- Per Euro2000:
      '--- Se attiva l'opzione, il valore minimo impostato per i campio Scorta Minima/Massima/Qtà lotto/std
      '--- con Politica di riordino, è zero
      '-----------------------------------------------------------------------------------------
      oCleArti.bNoMsgCongruenzaPolSconte = CBool(NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "NoMsgCongruenzaPolScorte", "0", " ", "0")))
      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGARTI_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Dim i As Integer
    Dim nFase As Integer = 0
    Dim dsTmp As DataSet = Nothing
    Dim dttAr_codtipa As New DataTable()
    Dim dttPeca As New DataTable
    Try
      '----------------------------------------------------------------------------------------
      '--- Se non esiste almeno la tipologia 1 chiude
      '----------------------------------------------------------------------------------------
      If Not oCleArti.GetTabTipa1() Then
        Me.Close()
        Return
      End If
      '----------------------------------------------------------------------------------------
      oCleArti.GetTabTipa(dsTmp)

      dttAr_codtipa.Columns.Add("cod", GetType(String))
      dttAr_codtipa.Columns.Add("val", GetType(String))

      For i = 0 To dsTmp.Tables("TABTIPA").Rows.Count - 1
        dttAr_codtipa.Rows.Add(New Object() {NTSCStr(dsTmp.Tables("TABTIPA").Rows(i)!tb_codtipa), NTSCStr(dsTmp.Tables("TABTIPA").Rows(i)!tb_destipa)})
        dttAr_codtipa.AcceptChanges()
      Next

      cbAr_codtipa.DataSource = dttAr_codtipa
      cbAr_codtipa.ValueMember = "cod"
      cbAr_codtipa.DisplayMember = "val"

      oCleArti.SetImagesDir()
      NTSFormClearDataBinding(Me)

      '---------------------------------------------------------------------
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCAE) Then
        oMenu.ValCodiceDb("1", DittaCorrente, "TABPECA", "N", , dttPeca)
        If dttPeca.Rows.Count = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129319677734956014, "Tabella delle Personalizzazioni CA-DC (Globale) non configurata. Imposibile continuare"))
          Me.Close()
          Return
        End If
        oCleArti.bCampiCAEAttivi = CBool(IIf(NTSCStr(dttPeca.Rows(0)!tb_richarti) = "S", True, False))
      End If
      '--------------------------------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCEC) Then
      Else
        fmEcommerce.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "APRI;" Then
          tlbApri_ItemClick(Me, Nothing)
        ElseIf Microsoft.VisualBasic.Left(oCallParams.strParam, 7) = "APRI_F;" Then
          'apro le fasi articolo
          oCallParams.strParam = "APRI;" & oCallParams.strParam.Substring(7)
          i = oCallParams.strParam.IndexOf(";"c, 6)
          nFase = NTSCInt(oCallParams.strParam.Substring(i + 1)) 'cerco la fase
          oCallParams.strParam = oCallParams.strParam.Substring(0, i) 'tolgo la fase per lasciare la chiamata come se fosse stato cghiesto di aprire solo il cod. articolo
          tlbApri_ItemClick(Me, Nothing)
          tlbFasiArticolo_ItemClick(tlbFasiArticolo, Nothing)
        ElseIf oCallParams.strParam <> "" And Microsoft.VisualBasic.Mid(oCallParams.strParam, 5) <> "DUPL;" Then
          'Se 'Nuovo' non è abilitato, non permetto di creare un nuovo articolo 
          If tlbNuovo.Enabled And tlbNuovo.Visible Then
            tlbDuplica_ItemClick(Me, Nothing)
          End If
        ElseIf oCallParams.strPar1 <> "" Then
          'da BNDKKONS
          oCleArti.strWhereFiar = oCallParams.strPar1
          oCleArti.strOrderBy = oCallParams.strPar2
          ApriArticoli()
        End If
      End If    'If Not oCallParams Is Nothing Then

      oCallParams = Nothing 'in questo modo se vengo chiamato da bnmghlar, prima apro l'articolo scelto, poi se faccio ripristina posso scegliere un altro articolo 
      oCleArti.bDaGest = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGARTI_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
    Try
      If edAr_note.Focused And e.KeyCode = Keys.Return Then
        edAr_note.Focus()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGARTI_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then
          e.Cancel = True
          Return
        End If
      End If
      oCleArti.Ripristina(dcArti.Position, dcArti.Filter)
      If tsArti.SelectedTabPage.Equals(tsArti.TabPages(3)) Then
        ceListini.Ripristina()
      ElseIf tsArti.SelectedTabPage.Equals(tsArti.TabPages(4)) Then
        ceSconti.Ripristina()
      ElseIf tsArti.SelectedTabPage.Equals(tsArti.TabPages(5)) Then
        ceProvvig.Ripristina()
      End If
      SetStato(0)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRMMGARTI_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcArti.Dispose()
      dsArti.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub cmdVisGif1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVisGif1.Click
    Dim oPar As New CLE__CLDP
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se non esiste la cartella delle immagini avvisa ed esce
      '-----------------------------------------------------------------------------------------
      If Not Directory.Exists(oCleArti.strImageDir) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128559129301475003, "La cartella delle immagini non esiste. Impossibile proseguire."))
        Exit Sub
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se non esiste il file indicato nel TextBox relativo, nella cartella delle immagini
      '--- avvisa ed esce
      '-----------------------------------------------------------------------------------------
      If Not File.Exists(oCleArti.strImageDir & "\" & edAr_gif1.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128559129514857263, "L'immagine speficificata non esiste o è stata rimossa. Impossibile proseguire."))
        edAr_gif1.Text = ""
        edAr_gif1.Focus()
        cmdVisGif1.Enabled = False
        Exit Sub
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.dPar1 = 1
      oPar.strPar2 = oCleArti.strImageDir & "\" & edAr_gif1.Text
      oPar.strPar3 = oCleArti.strAltezzaMaxImg

      oMenu.RunChild("NTSInformatica", "FRMMGVGIF", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdVisGif2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVisGif2.Click
    Dim oPar As New CLE__CLDP
    Try
      If Not Directory.Exists(oCleArti.strImageDir) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128559129803371577, "La cartella delle immagini non esiste. Impossibile proseguire."))
        Exit Sub
      End If
      If Not File.Exists(oCleArti.strImageDir & "\" & edAr_gif2.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128788389897732374, "L'immagine speficificata non esiste o è stata rimossa. Impossibile proseguire."))
        edAr_gif2.Text = ""
        edAr_gif2.Focus()
        Exit Sub
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.dPar1 = 2
      oPar.strPar2 = oCleArti.strImageDir & "\" & edAr_gif2.Text
      oPar.strPar3 = oCleArti.strAltezzaMaxImg

      oMenu.RunChild("NTSInformatica", "FRMMGVGIF", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdArtGif1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdArtGif1.Click
    Dim nArtgif As Integer
    Try
      nArtgif = 1
      ApriGif(nArtgif)
      If edAr_gif1.Text <> "" Then
        cmdVisGif1.Enabled = True
        oCleArti.bHasChanges = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdArtgif2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdArtgif2.Click
    Dim nArtgif As Integer
    Try
      nArtgif = 2
      ApriGif(nArtgif)
      If edAr_gif2.Text <> "" Then
        cmdVisGif2.Enabled = True
        oCleArti.bHasChanges = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdProgtot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProgtot.Click
    Dim oPar As New CLE__CLDP
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      If ckAr_gesfasi.Checked = True Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128566093930633554, "L'articolo e' gestito a fasi, i progressivi totali possono essere inseriti solo dalla finestra Fasi Articolo."))
        Exit Sub
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.dPar1 = NTSCDec(edAr_perqta.Text)
      oPar.strPar2 = edAr_codart.Text
      oPar.dPar2 = 0

      oMenu.RunChild("NTSInformatica", "FRMMGHLAT", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdValuta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdValuta.Click
    Dim oPar As New CLE__CLDP
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.strPar2 = edAr_codart.Text

      oMenu.RunChild("NTSInformatica", "FRMMGHLAV", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdProgressivi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProgressivi.Click
    Dim oPar As New CLE__CLDP
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      If ckAr_gesfasi.Checked = True Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128566095564998094, "L'articolo e' gestito a fasi, i progressivi per magazzino possono essere inseriti solo dalla finestra Fasi Articolo."))
        Exit Sub
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.strPar2 = edAr_codart.Text
      oPar.dPar1 = 0

      oMenu.RunChild("NTSInformatica", "FRMMGHLAP", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdCodarfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCodarfo.Click
    Dim oPar As New CLE__CLDP
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.strPar2 = edAr_codart.Text

      oMenu.RunChild("NTSInformatica", "FRMMGCACF", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdClassifica_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClassifica.Click
    Dim oParam As New CLE__PATB
    Dim strT() As String = Nothing
    Try
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTCLAS", DittaCorrente, oParam)
      If NTSZOOM.strIn <> "" Then
        strT = NTSZOOM.strIn.Split("|"c)
        dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codcla1 = strT(0)
        dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codcla2 = strT(1)
        dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codcla3 = strT(2)
        dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codcla4 = strT(3)
        dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codcla5 = strT(4)
      End If
      CaricaClassificazione()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdClassificaDeleteFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClassificaDeleteFilter.Click
    Try
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129991932271878338, "Rimuovere la classificazione?")) = Windows.Forms.DialogResult.Yes Then
        dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codcla1 = " "
        dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codcla2 = " "
        dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codcla3 = " "
        dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codcla4 = " "
        dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codcla5 = " "
        lbClassifica.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overrides Function NTSGetDataAutocompletamento(ByVal strTabName As String, ByVal strDescr As String, _
    ByVal IsCrmUser As Boolean, ByRef dsOut As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If edAr_forn.ContainsFocus Or edAr_forn2.ContainsFocus Then
        strTabName = "ANAGRA_FOR"
      Else
        If tsArti.SelectedTabPage.Equals(tsArti.TabPages(3)) Then
          Select Case ceListini.cbTipolistino.SelectedValue
            Case "C" : If ceListini.grvList.FocusedColumn.Name = "lc_conto" Then strTabName = "ANAGRA_CLI"
            Case "F" : If ceListini.grvList.FocusedColumn.Name = "lc_conto" Then strTabName = "ANAGRA_FOR"
          End Select
        End If
        If tsArti.SelectedTabPage.Equals(tsArti.TabPages(4)) Then
          Select Case NTSCInt(ceSconti.cbTiposconti.SelectedValue)
            Case 4 : If ceSconti.grvSconti.FocusedColumn.Name = "so_conto" Then strTabName = "ANAGRACF"
          End Select
        End If
        If tsArti.SelectedTabPage.Equals(tsArti.TabPages(5)) Then
          Select Case NTSCInt(ceProvvig.cbTipoProv.SelectedValue)
            Case 3 : If ceProvvig.grvProv.FocusedColumn.Name = "per_conto" Then strTabName = "ANAGRACF"
          End Select
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return MyBase.NTSGetDataAutocompletamento(strTabName, strDescr, IsCrmUser, dsOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      If pnTop.Visible Then
        If tlbSalva.Enabled And tlbSalva.Visible Then
          If Not Salva() Then Return
        End If
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      If Not Apri(True, False) Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Try
      If pnTop.Visible Then
        If tlbSalva.Enabled And tlbSalva.Visible Then
          If Not Salva() Then Return
        End If
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      If Not Apri(False, True) Then Return

      ceSconti.cbTiposconti.SelectedValue = "1"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Try
      If pnTop.Visible Then
        If tlbSalva.Enabled And tlbSalva.Visible Then
          If Not Salva() Then Return
        End If
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      If Not Apri(False, False) Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbApriUltimaRicerca_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApriUltimaRicerca.ItemClick
    Dim oParam As New CLE__PATB

    Try
      '--------------------------------------------------------------------------------------------------------------
      If pnTop.Visible = True Then
        If (tlbSalva.Enabled = True) And (tlbSalva.Visible = True) Then
          If Salva() = False Then Return
        End If
        tlbRipristina_ItemClick(Nothing, Nothing)
      End If
      '--------------------------------------------------------------------------------------------------------------
      SetStato(0)
      '--------------------------------------------------------------------------------------------------------------
      oParam.bVisGriglia = True
      oParam.strTipoArticolo = "N"
      oParam.strAlfpar = "BNMGARTI"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
      '--------------------------------------------------------------------------------------------------------------
      If oParam.strOut.Trim = "" Then Return
      oCleArti.strCodart = oParam.strOut
      '--------------------------------------------------------------------------------------------------------------
      ceSconti.TipoSconto = 1
      ceProvvig.TipoProvv = 4
      '--------------------------------------------------------------------------------------------------------------
      '--- Leggo dal database i dati e collego il NTSBinding
      '--------------------------------------------------------------------------------------------------------------
      If oCleArti.Apri(DittaCorrente, dsArti) = False Then Return
      If dsArti.Tables("ARTICO").Rows.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129842224376005859, "Nessun articolo trovato."))
        Return
      End If
      dcArti.DataSource = dsArti.Tables("ARTICO")
      dsArti.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      '--- Collego il BindingSource ai vari controlli 
      '--------------------------------------------------------------------------------------------------------------
      Bindcontrols()
      dcArti.ResetBindings(False)
      dcArti.MoveFirst()
      NumeroPost()
      '--------------------------------------------------------------------------------------------------------------
      ApriListSconProv()
      '--------------------------------------------------------------------------------------------------------------
      SetStato(1)
      '--------------------------------------------------------------------------------------------------------------
      edAr_codart.Enabled = False
      edAr_unmis.Enabled = False
      ckAr_gesfasi.Enabled = False
      ckAr_gesubic.Enabled = False
      If oCleArti.bSbloccaFLotto = False Then ckAr_geslotti.Enabled = False
      ckAr_gestmatr.Enabled = False
      'cbAr_gescomm.Enabled = False
      ckAr_flgift.Enabled = False
      With dsArti.Tables("ARTICO").Rows(dcArti.Position)
        If NTSCStr(!ar_confez2) <> "" Then edAr_confez2.Enabled = False
        If NTSCStr(!ar_unmis2) <> "" Then edAr_unmis2.Enabled = False
        If NTSCStr(!ar_um4) <> "" Then edAr_um4.Enabled = False
      End With
      If oCleArti.strTipoConfiguratore > "2" Then
        cbAr_tipitemcp3.Enabled = False
        lbAr_tipitemcp3.Enabled = False
      End If
      edAr_descr.Focus()
      oCleArti.bValarti = False
      '--------------------------------------------------------------------------------------------------------------
      AbilitaControlli()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      '-------------------------------------------------
      'prima di salvare simulo una lostfocus del campo su cui mi trovo, altrimenti potrei salvare un dato non corretto
      Salva()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      '-------------------------------------------------
      'cancello la forma di pagamento
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128550728307978584, "Confermi la cancellazione?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If Not oCleArti.TestPreCancella(NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codart), _
              NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_tipoopz), _
              NTSCInt(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codtipa)) Then Return

          If dsArti.Tables("ARTICO").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          'Caso particolare: ero in aggiunta di una nuova riga, senza salvarla cancello l'articolo:
          ' La validazione viene fatta all'apertura dell'articolo successivo, generando diversi errori
          ceListini.Ripristina()
          ceSconti.Ripristina()
          ceProvvig.Ripristina()

          dcArti.RemoveAt(dcArti.Position)
          oCleArti.Salva(True)

          If dsArti.Tables("ARTICO").Rows.Count = 0 Then SetStato(0)

          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcArti, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      If nStato = 0 Then Return

      Me.ValidaLastControl()          'se non valido il controllo su cui sono, quando modifico il controllo e, senza uscire, faccio 'ripristina' il controllo rimane sporco

      '-------------------------------------------------
      'ripristino la forma di pagamento
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128550728308134760, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsArti.Tables("ARTICO").Rows.Count = 1 Then
            If dsArti.Tables("ARTICO").Rows(0).RowState = DataRowState.Added Then
              bRemovBinding = True
              NTSFormClearDataBinding(Me)
            End If
          End If

          oCleArti.Ripristina(dcArti.Position, dcArti.Filter)

          If tsArti.SelectedTabPage.Equals(tsArti.TabPages(3)) Then
            ceListini.Ripristina()
          ElseIf tsArti.SelectedTabPage.Equals(tsArti.TabPages(4)) Then
            ceSconti.Ripristina()
          ElseIf tsArti.SelectedTabPage.Equals(tsArti.TabPages(5)) Then
            ceProvvig.Ripristina()
          End If
          If Not sender Is Nothing Then
            SetStato(0)
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcArti, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      If oCleArti.bGestTabUnmis = False And _
      (edAr_unmis.Focused Or edAr_confez2.Focused Or edAr_unmis2.Focused Or edAr_um4.Focused) Then Return

      If ceListini.grList.Focused Then
        '------------------------------------
        'oggetto listini: giro il tutto all'oggetto specifico
        ceListini.tlbListZoom_Click(ceListini.tlbListZoom, Nothing)

      ElseIf ceSconti.grSconti.Focused Then
        '------------------------------------
        'oggetto listini: giro il tutto all'oggetto specifico
        ceSconti.tlbScontiZoom_Click(ceSconti.tlbScontiZoom, Nothing)

      ElseIf ceProvvig.grProv.Focused Then
        '------------------------------------
        'oggetto listini: giro il tutto all'oggetto specifico
        ceProvvig.tlbProvZoom_Click(ceProvvig.tlbProvZoom, Nothing)

      ElseIf edAr_ultfase.Focused Then
        If edAr_codart.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128547960324296557, "Indicare un codice articolo valido prima di passare alla selezione delle fasi"))
          Return
        End If
        SetFastZoom(edAr_ultfase.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(edAr_ultfase.Text)
        oParam.strTipo = edAr_codart.Text
        NTSZOOM.ZoomStrIn("ZOOMARTFASI", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAr_ultfase.Text Then edAr_ultfase.NTSTextDB = NTSZOOM.strIn

      ElseIf edAr_codtcdc.Focused Then
        SetFastZoom(edAr_codtcdc.Text, oParam)
        NTSZOOM.strIn = NTSCStr(edAr_codtcdc.Text)
        oParam.strTipo = "A"
        NTSZOOM.ZoomStrIn("ZOOMTABTCDC", "", oParam)
        If NTSZOOM.strIn <> NTSCStr(edAr_codtcdc.Text) Then edAr_codtcdc.Text = NTSZOOM.strIn
      ElseIf edAr_coddica.Focused Then
        SetFastZoom(edAr_coddica.Text, oParam)
        NTSZOOM.strIn = NTSCStr(edAr_coddica.Text)
        oParam.strTipo = "A"
        NTSZOOM.ZoomStrIn("ZOOMTABDICA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(edAr_coddica.Text) Then edAr_coddica.Text = NTSZOOM.strIn
      ElseIf edAr_coddicv.Focused Then
        If edAr_coddica.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129309316650792127, "Prima specificare il codice di Aggregazione Budget"))
          Return
        End If

        SetFastZoom(edAr_coddicv.Text, oParam)
        oParam.strCodice = edAr_coddica.Text
        NTSZOOM.strIn = edAr_coddicv.Text
        NTSZOOM.ZoomStrIn("ZOOMTABDICV", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAr_coddicv.Text Then edAr_coddicv.Text = NTSZOOM.strIn
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

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    If tlbSalva.Enabled And tlbSalva.Visible Then
      If Not Salva() Then Return
    Else
      tlbRipristina_ItemClick(Nothing, Nothing)
    End If
    Me.Close()
  End Sub

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul primo record
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      End If
      dcArti.MoveFirst()
      NumeroPost()
      AbilitaControlli()
      CaricaClassificazione()
      ApriListSconProv()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      '-------------------------------------------------
      'vado sul record precedente
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      End If
      dcArti.MovePrevious()
      NumeroPost()
      AbilitaControlli()
      CaricaClassificazione()
      ApriListSconProv()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul record successivo
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      End If
      dcArti.MoveNext()
      NumeroPost()
      AbilitaControlli()
      CaricaClassificazione()
      ApriListSconProv()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sull'ultimo record
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      End If
      dcArti.MoveLast()
      NumeroPost()
      AbilitaControlli()
      CaricaClassificazione()
      ApriListSconProv()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbBarcode_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbBarcode.ItemClick
    Dim oPar As New CLE__CLDP
    Dim dsTmp As DataSet = Nothing
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      '-------------------------------------------------------------------------------------
      '--- Se è stata indicata la gestione a fasi, controlla che esista almeno una fase
      '-----------------------------------------------------------------------------------
      If ckAr_gesfasi.Checked = True Then
        oCleArti.GetArtfasi(edAr_codart.Text, dsTmp)
        If dsTmp.Tables("ARTFASI").Rows.Count = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128570394823979162, "Attenzione!" & vbCrLf & _
                "L'articolo è gestito a 'Fasi', mentre non sono stati indicati dati validi in 'Fasi articolo'." & vbCrLf & _
                "Indicare le fasi prima di passare alla gestione dei 'Codici a barre'."))
          Exit Sub
        End If
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.strPar2 = edAr_codart.Text
      oPar.strPar3 = edAr_unmis.Text
      oPar.strPar4 = edAr_confez2.Text
      oPar.strPar5 = edAr_unmis2.Text
      oPar.dPar1 = NTSCDec(edAr_qtacon2.Text)
      oPar.dPar2 = NTSCDec(edAr_conver.Text)
      oPar.bPar1 = CBool(IIf(ckAr_gesfasi.Checked = True, True, False))
      oPar.dPar3 = NTSCDec(edAr_ultfase.Text)

      oMenu.RunChild("NTSInformatica", "FRMMGBARC", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGift_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGift.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      oPar.strPar1 = "BNMGARTI"
      oPar.strPar2 = edAr_codart.Text

      oMenu.RunChild("NTSInformatica", "FRMMGGIFT", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbKit_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbKit.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      If NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_tipokit) = " " Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128565244716608415, "Articolo non tipo Kit, apertura Composizione Kit non possibile."))
        Exit Sub
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.strPar2 = edAr_codart.Text

      oMenu.RunChild("NTSInformatica", "FRMMGCKIT", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbConai_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbConai.ItemClick
    Dim oParam As New CLE__CLDP
    Dim frmCona As FRMMGCONA = Nothing
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      If NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_gescon) = "N" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128563518804012853, "Articolo senza applicazione Conai, apertura Composizione Conai non possibile."))
        Exit Sub
      End If

      frmCona = CType(NTSNewFormModal("FRMMGCONA"), FRMMGCONA)
      frmCona.strConaCodart = edAr_codart.Text
      frmCona.Init(oMenu, oParam, DittaCorrente)
      frmCona.InitEntity(oCleArti)
      frmCona.ShowDialog()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmCona Is Nothing Then frmCona.Dispose()
      frmCona = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbOle_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOle.ItemClick
    Dim strParam As String = ""
    Dim strCodart As String
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      strCodart = edAr_codart.Text
      If strCodart.Trim <> "" Then
        strParam = "APRI§A§0§" & strCodart
        oMenu.RunChild("BS__AOLE", "CLS__AOLE", oApp.Tr(Me, 128575012873982601, "Oggetti associati"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaEtichette_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaEtichette.ItemClick
    Dim strParam As String
    Dim strCodart As String
    Dim strQuant As String
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      strCodart = edAr_codart.Text
      If strCodart.Trim <> "" Then
        strCodart = strCodart.PadRight(CLN__STD.CodartMaxLen).Substring(0, CLN__STD.CodartMaxLen)
        strQuant = NTSCInt(1).ToString.PadLeft(9)
        strQuant = Microsoft.VisualBasic.Right(strQuant, 9)
        strParam = "ART;" & strCodart & ";" & strQuant & ";"
        oMenu.RunChild("BSMGETTE", "CLSMGETTE", oApp.Tr(Me, 128575004651798150, "Stampa etichette articoli"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAcquisisciDaCatForn_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAcquisisciDaCatForn.ItemClick
    Dim oParam As New CLE__PATB
    Dim dttTmp As DataTable
    Try
      '-----------------------------------------------------------------------------------------
      'richiama zoom multi selezione su artest
      NTSZOOM.ZoomStrIn("ZOOMARTEST", DittaCorrente, oParam)

      'datatable restituito dallo zoom contenente la chiave dei record di artest per l'importazione
      dttTmp = CType(oParam.oParam, DataTable)
      If dttTmp Is Nothing Then Return

      If oCleArti.ImportaCatalogoFornitori(dttTmp) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128576409722642786, "Elaborazione terminata"))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRicalcolaListini_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRicalcolaListini.ItemClick
    Dim oPar As New CLE__CLDP
    Dim nCodvalu As Integer = 0
    Dim dPrezzoBase As Decimal = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edAr_codpdon.Text) = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128578173195743576, "Inserire il codice relazione listini prima di procedere con l'elaborazione."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (tlbSalva.Enabled = True) And (tlbSalva.Visible = True) Then
        If Salva() = False Then Return
      Else
        If oCleArti.bNew = True Then Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Funziona solo se è indicata la data
      '--------------------------------------------------------------------------------------------------------------
      If ceListini.opValDay.Checked = False Then
        ceListini.opValDay.Checked = True
        oApp.MsgBoxInfo(oApp.Tr(Me, 128582485064375819, "Selezionare la data di validità prima di passare al ricalcolo dei listini."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      'oPar.Ditta = DittaCorrente
      'oPar.strNomProg = "BSMGARTI"
      'oPar.dPar1 = 0
      'oPar.dPar2 = NTSCDec(IIf(ceListini.opValEur.Checked, 0, NTSCInt(ceListini.edCodvalu.Text)))
      'oPar.dPar4 = 1
      'oPar.dPar5 = dPrezzoBase
      'oPar.strPar1 = edAr_codart.Text
      'If IsNothing(ceListini.grvList.NTSGetCurrentDataRow) = False Then
      '  oPar.dPar3 = NTSCInt(ceListini.grvList.NTSGetCurrentDataRow!lc_fase)
      '  oPar.strPar2 = NTSCDate(ceListini.grvList.NTSGetCurrentDataRow!lc_datagg).ToShortDateString
      '  oPar.strPar3 = NTSCStr(ceListini.grvList.NTSGetCurrentDataRow!lc_unmis)
      '  oPar.strPar4 = NTSCStr(ceListini.grvList.NTSGetCurrentDataRow!lc_netto)
      'Else
      '  If ckAr_gesfasi.Checked = True Then oPar.dPar3 = NTSCInt(edAr_ultfase.Text) Else oPar.dPar3 = 0
      '  oPar.strPar2 = ceListini.edDtval.Text
      '  oPar.strPar3 = edAr_unmis.Text
      '  oPar.strPar4 = "N"
      'End If
      'oMenu.RunChild("NTSInformatica", "FRMMGRCPR", "", DittaCorrente, "", "BNMGRCPR", oPar, "", True, True)
      '--------------------------------------------------------------------------------------------------------------      
      If IsNothing(ceListini.grvList.NTSGetCurrentDataRow) Then Return
      If ceListini.opValEur.Checked = False Then nCodvalu = NTSCInt(ceListini.edCodvalu.Text)
      oCleArti.AggiornaListini(edAr_codart.Text, nCodvalu, ceListini.edDtval.Text, 1, True, 1, _
        NTSCDate(ceListini.grvList.NTSGetCurrentDataRow!lc_datagg).ToShortDateString, NTSCInt(edAr_codpdon.Text), _
        NTSCInt(ceListini.grvList.NTSGetCurrentDataRow!lc_codlavo), _
        NTSCInt(ceListini.grvList.NTSGetCurrentDataRow!lc_fase), _
        NTSCStr(ceListini.grvList.NTSGetCurrentDataRow!lc_unmis), _
        NTSCStr(ceListini.grvList.NTSGetCurrentDataRow!lc_netto))
      '--------------------------------------------------------------------------------------------------------------
      ceListini.ApriListini()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbZoomRigheOffertePerArt_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoomRigheOffertePerArt.ItemClick
    Dim oParam As New CLE__PATB
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      '-----------------------------------------------------------------------------------------
      'richiama zoom articolo righe offerte
      oParam.strDescr = "BNMGARTI"
      oParam.strCodartAcc = edAr_codart.Text
      oParam.lCommessa = NTSCInt(edAr_ultfase.Text)
      oParam.lContoCF = 0

      NTSZOOM.ZoomStrIn("ZOOMARTRIGHEOFF", DittaCorrente, oParam)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbVisualizzaMovimenti_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbVisualizzaMovimenti.ItemClick
    Dim strParam As String
    Dim strCodart As String
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      strCodart = edAr_codart.Text
      If strCodart.Trim <> "" Then
        strCodart = strCodart.PadRight(CLN__STD.CodartMaxLen).Substring(0, CLN__STD.CodartMaxLen)
        strParam = "APRI:" & strCodart & ";0000;000000000;A"
        oMenu.RunChild("BSMGSCHE", "CLSMGSCHE", oApp.Tr(Me, 128575010044763178, "Stampa schede articoli"), DittaCorrente, "", "", Nothing, strParam, True, True)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbControlloQualità_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbControlloQualita.ItemClick
    Dim strParam As String
    Try
      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And bsModSQ) Then
        If tlbSalva.Enabled And tlbSalva.Visible Then
          If Not Salva() Then Return
        Else
          If oCleArti.bNew Then Return
        End If
        If ckAr_gesfasi.Checked = True Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128580981604640867, "L'articolo e' gestito a fasi, il controllo qualità puo' essere eseguito solo dalla finestra Fasi Articolo."))
        Else
          strParam = "APRI;0;0;" & edAr_codart.Text & ";0;0;0;0;" & Now.ToShortDateString & ";B;"
          oMenu.RunChild("NTSInformatica", "FRMSQCOQU", oApp.Tr(Me, 128580982589945330, "Controlli di Qualità"), DittaCorrente, "", "BNSQCOQU", Nothing, strParam, True, True)
        End If
      Else
        oApp.MsgBoxInfo(oApp.Tr(Me, 128580980054173938, "Modulo Sistema Qualità non abilitato." & vbCrLf & "Impossibile continuare."))
        Exit Sub
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbArticoliMagazzino_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbArticoliMagazzino.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.bPar1 = ckAr_gesfasi.Checked
      oPar.strPar2 = edAr_ultfase.Text
      oPar.strPar3 = edAr_codart.Text

      oMenu.RunChild("NTSInformatica", "FRMMGARMA", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAttributiArticolo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAttributiArticolo.ItemClick
    Dim oParam As New CLE__CLDP
    Dim frmVala As FRMMGVALA = Nothing
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      If Not oCleArti.CheckAttributi(NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codtipa)) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128581036419181338, "La tipologia associata all'articolo non prevede la gestione di attributi."))
        Exit Sub
      End If
      If NTSCInt(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codtipa) = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128581036253296938, "Articolo senza tipologia associata, apertura Attributi Articolo non possibile."))
        Exit Sub
      End If

      oCleArti.strValaCodart = edAr_codart.Text
      oCleArti.strValaDesart = edAr_descr.Text
      oCleArti.nValaCodtipa = NTSCInt(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codtipa)
      frmVala = CType(NTSNewFormModal("FRMMGVALA"), FRMMGVALA)
      frmVala.Init(oMenu, oParam, DittaCorrente)
      frmVala.InitEntity(oCleArti)
      frmVala.ShowDialog()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmVala Is Nothing Then frmVala.Dispose()
      frmVala = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbFasiArticolo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbFasiArticolo.ItemClick
    Dim oPar As New CLE__CLDP
    Try
      If oCleArti.bLogisticaEstesa = False Then Exit Sub

      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      If ckAr_gesfasi.Checked = False Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128566047884303214, "Articolo '|" & edAr_codart.Text & "|' non gestito a fasi." & vbCrLf & _
          "Apertura fasi articolo non possibile."))
        Exit Sub
      End If

      oPar.strPar1 = "BNMGARTI"
      oPar.dPar1 = NTSCDec(edAr_perqta.Text)
      oPar.strPar2 = edAr_codart.Text

      oMenu.RunChild("NTSInformatica", "FRMMGFASI", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPromozioni_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPromozioni.ItemClick
    Dim oParam As New CLE__CLDP
    Dim frmApro As FRMMGAPRO = Nothing
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      frmApro = CType(NTSNewFormModal("FRMMGAPRO"), FRMMGAPRO)
      frmApro.strAproCodart = edAr_codart.Text
      frmApro.strAproDesart = edAr_descr.Text
      frmApro.Init(oMenu, oParam, DittaCorrente)
      frmApro.InitEntity(oCleArti)
      frmApro.ShowDialog()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmApro Is Nothing Then frmApro.Dispose()
      frmApro = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbAccessoriSuccedanei_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAccessoriSuccedanei.ItemClick
    Dim oParam As New CLE__CLDP
    Dim frmArta As FRMMGARTA = Nothing
    Try
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return
      Else
        If oCleArti.bNew Then Return
      End If

      frmArta = CType(NTSNewFormModal("FRMMGARTA"), FRMMGARTA)
      frmArta.strArtaCodart = edAr_codart.Text
      oCleArti.strArtAccSuccCodart = edAr_codart.Text
      frmArta.strArtaDesart = edAr_descr.Text
      frmArta.Init(oMenu, oParam, DittaCorrente)
      frmArta.InitEntity(oCleArti)
      frmArta.ShowDialog()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmArta Is Nothing Then frmArta.Dispose()
      frmArta = Nothing
    End Try
  End Sub

  Public Overridable Sub tlbEstensioni_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEstensioni.ItemClick
    Dim bDatiCambiati As Boolean = False
    Dim oParam As New CLE__CLDP
    Dim frmAnex As FRMMGANEX = Nothing
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleArti.GetTabaext(dttTmp) = False Then
        dttTmp.Clear()
        dttTmp.Dispose()
        oApp.MsgBoxInfo(oApp.Tr(Me, 130440333944294653, "Attenzione!" & vbCrLf & _
          "Non sono state configurate le estensioni per gli articoli." & vbCrLf & _
          "Apertura finestra relativa non possibile."))
        Return
      Else
        dttTmp.Clear()
        dttTmp.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Faccio una fotografia dei dati relativi alle estensioni, prima della chiamata alla modale 
      '--------------------------------------------------------------------------------------------------------------
      dttTmp = dsArti.Tables("ARTICO").Clone
      dttTmp.Rows.Add(dsArti.Tables("ARTICO").Rows(dcArti.Position).ItemArray)
      '--------------------------------------------------------------------------------------------------------------
      frmAnex = CType(NTSNewFormModal("FRMMGANEX"), FRMMGANEX)
      frmAnex.Init(oMenu, oParam, DittaCorrente)
      frmAnex.InitEntity(oCleArti)
      frmAnex.dtrArti = dsArti.Tables("ARTICO").Rows(dcArti.Position)
      frmAnex.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
      With dttTmp.Rows(0)
        If NTSCStr(!ar_xtipo1) <> frmAnex.edAx_tipo1.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xtipo2) <> frmAnex.edAx_tipo2.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xtipo3) <> frmAnex.edAx_tipo3.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xdesext1) <> frmAnex.edAx_desext1.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xdesext2) <> frmAnex.edAx_desext2.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xdesext3) <> frmAnex.edAx_desext3.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xdescr1) <> frmAnex.edAx_descr1.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xdescr2) <> frmAnex.edAx_descr2.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xdescr3) <> frmAnex.edAx_descr3.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xdescr4) <> frmAnex.edAx_descr4.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xdescr5) <> frmAnex.edAx_descr5.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xdescr6) <> frmAnex.edAx_descr6.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xdescr7) <> frmAnex.edAx_descr7.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xdescr8) <> frmAnex.edAx_descr8.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xdescr9) <> frmAnex.edAx_descr9.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xdescr10) <> frmAnex.edAx_descr10.Text Then bDatiCambiati = True
        If DatiCambiati(NTSCStr(!ar_xdata1), frmAnex.edAx_data1.Text) = True Then bDatiCambiati = True
        If DatiCambiati(NTSCStr(!ar_xdata2), frmAnex.edAx_data2.Text) = True Then bDatiCambiati = True
        If DatiCambiati(NTSCStr(!ar_xdata3), frmAnex.edAx_data3.Text) = True Then bDatiCambiati = True
        If DatiCambiati(NTSCStr(!ar_xdata4), frmAnex.edAx_data4.Text) = True Then bDatiCambiati = True
        If DatiCambiati(NTSCStr(!ar_xdata5), frmAnex.edAx_data5.Text) = True Then bDatiCambiati = True
        If NTSCStr(!ar_xmemo1) <> frmAnex.edAx_memo1.Text Then bDatiCambiati = True
        If NTSCStr(!ar_xmemo2) <> frmAnex.edAx_memo2.Text Then bDatiCambiati = True
        If NTSCDec(!ar_xnum1) <> NTSCDec(frmAnex.edAx_num1.Text) Then bDatiCambiati = True
        If NTSCDec(!ar_xnum2) <> NTSCDec(frmAnex.edAx_num2.Text) Then bDatiCambiati = True
        If NTSCDec(!ar_xnum3) <> NTSCDec(frmAnex.edAx_num3.Text) Then bDatiCambiati = True
        If NTSCDec(!ar_xnum4) <> NTSCDec(frmAnex.edAx_num4.Text) Then bDatiCambiati = True
        If NTSCDec(!ar_xnum5) <> NTSCDec(frmAnex.edAx_num5.Text) Then bDatiCambiati = True
        If NTSCDec(!ar_xnum6) <> NTSCDec(frmAnex.edAx_num6.Text) Then bDatiCambiati = True
        If NTSCDec(!ar_xnum7) <> NTSCDec(frmAnex.edAx_num7.Text) Then bDatiCambiati = True
        If NTSCDec(!ar_xnum8) <> NTSCDec(frmAnex.edAx_num8.Text) Then bDatiCambiati = True
        If NTSCDec(!ar_xnum9) <> NTSCDec(frmAnex.edAx_num9.Text) Then bDatiCambiati = True
        If NTSCDec(!ar_xnum10) <> NTSCDec(frmAnex.edAx_num10.Text) Then bDatiCambiati = True
        If NTSCStr(!ar_xcombo1) <> frmAnex.cbAx_combo1.SelectedValue Then bDatiCambiati = True
        If NTSCStr(!ar_xcombo2) <> frmAnex.cbAx_combo2.SelectedValue Then bDatiCambiati = True
        If NTSCStr(!ar_xcombo3) <> frmAnex.cbAx_combo3.SelectedValue Then bDatiCambiati = True
        If NTSCStr(!ar_xcheck1) <> IIf(frmAnex.ckAx_check1.Checked = True, "S", "N").ToString Then bDatiCambiati = True
        If NTSCStr(!ar_xcheck2) <> IIf(frmAnex.ckAx_check2.Checked = True, "S", "N").ToString Then bDatiCambiati = True
        If NTSCStr(!ar_xcheck3) <> IIf(frmAnex.ckAx_check3.Checked = True, "S", "N").ToString Then bDatiCambiati = True
        If NTSCStr(!ar_xcheck4) <> IIf(frmAnex.ckAx_check4.Checked = True, "S", "N").ToString Then bDatiCambiati = True
        If NTSCStr(!ar_xcheck5) <> IIf(frmAnex.ckAx_check5.Checked = True, "S", "N").ToString Then bDatiCambiati = True
        If NTSCStr(!ar_xcheck6) <> IIf(frmAnex.ckAx_check6.Checked = True, "S", "N").ToString Then bDatiCambiati = True
        If NTSCStr(!ar_xcheck7) <> IIf(frmAnex.ckAx_check7.Checked = True, "S", "N").ToString Then bDatiCambiati = True
        If NTSCStr(!ar_xcheck8) <> IIf(frmAnex.ckAx_check8.Checked = True, "S", "N").ToString Then bDatiCambiati = True
        If NTSCStr(!ar_xcheck9) <> IIf(frmAnex.ckAx_check9.Checked = True, "S", "N").ToString Then bDatiCambiati = True
        If NTSCStr(!ar_xcheck10) <> IIf(frmAnex.ckAx_check10.Checked = True, "S", "N").ToString Then bDatiCambiati = True
      End With
      '--------------------------------------------------------------------------------------------------------------
      If bDatiCambiati = True Then
        With dsArti.Tables("ARTICO").Rows(dcArti.Position)
          !ar_xtipo1 = frmAnex.edAx_tipo1.Text
          !ar_xtipo2 = frmAnex.edAx_tipo2.Text
          !ar_xtipo3 = frmAnex.edAx_tipo3.Text
          !ar_xdesext1 = frmAnex.edAx_desext1.Text
          !ar_xdesext2 = frmAnex.edAx_desext2.Text
          !ar_xdesext3 = frmAnex.edAx_desext3.Text
          !ar_xdescr1 = frmAnex.edAx_descr1.Text
          !ar_xdescr2 = frmAnex.edAx_descr2.Text
          !ar_xdescr3 = frmAnex.edAx_descr3.Text
          !ar_xdescr4 = frmAnex.edAx_descr4.Text
          !ar_xdescr5 = frmAnex.edAx_descr5.Text
          !ar_xdescr6 = frmAnex.edAx_descr6.Text
          !ar_xdescr7 = frmAnex.edAx_descr7.Text
          !ar_xdescr8 = frmAnex.edAx_descr8.Text
          !ar_xdescr9 = frmAnex.edAx_descr9.Text
          !ar_xdescr10 = frmAnex.edAx_descr10.Text
          If frmAnex.edAx_data1.Text.Trim = "" Then !ar_xdata1 = DBNull.Value Else !ar_xdata1 = frmAnex.edAx_data1.Text
          If frmAnex.edAx_data2.Text.Trim = "" Then !ar_xdata2 = DBNull.Value Else !ar_xdata2 = frmAnex.edAx_data2.Text
          If frmAnex.edAx_data3.Text.Trim = "" Then !ar_xdata3 = DBNull.Value Else !ar_xdata3 = frmAnex.edAx_data3.Text
          If frmAnex.edAx_data4.Text.Trim = "" Then !ar_xdata4 = DBNull.Value Else !ar_xdata4 = frmAnex.edAx_data4.Text
          If frmAnex.edAx_data5.Text.Trim = "" Then !ar_xdata5 = DBNull.Value Else !ar_xdata5 = frmAnex.edAx_data5.Text
          !ar_xmemo1 = frmAnex.edAx_memo1.Text
          !ar_xmemo2 = frmAnex.edAx_memo2.Text
          !ar_xnum1 = NTSCDec(frmAnex.edAx_num1.Text)
          !ar_xnum2 = NTSCDec(frmAnex.edAx_num2.Text)
          !ar_xnum3 = NTSCDec(frmAnex.edAx_num3.Text)
          !ar_xnum4 = NTSCDec(frmAnex.edAx_num4.Text)
          !ar_xnum5 = NTSCDec(frmAnex.edAx_num5.Text)
          !ar_xnum6 = NTSCDec(frmAnex.edAx_num6.Text)
          !ar_xnum7 = NTSCDec(frmAnex.edAx_num7.Text)
          !ar_xnum8 = NTSCDec(frmAnex.edAx_num8.Text)
          !ar_xnum9 = NTSCDec(frmAnex.edAx_num9.Text)
          !ar_xnum10 = NTSCDec(frmAnex.edAx_num10.Text)
          !ar_xcombo1 = frmAnex.cbAx_combo1.SelectedValue
          !ar_xcombo2 = frmAnex.cbAx_combo2.SelectedValue
          !ar_xcombo3 = frmAnex.cbAx_combo3.SelectedValue
          !ar_xcheck1 = IIf(frmAnex.ckAx_check1.Checked = True, "S", "N").ToString
          !ar_xcheck2 = IIf(frmAnex.ckAx_check1.Checked = True, "S", "N").ToString
          !ar_xcheck3 = IIf(frmAnex.ckAx_check3.Checked = True, "S", "N").ToString
          !ar_xcheck4 = IIf(frmAnex.ckAx_check4.Checked = True, "S", "N").ToString
          !ar_xcheck5 = IIf(frmAnex.ckAx_check5.Checked = True, "S", "N").ToString
          !ar_xcheck6 = IIf(frmAnex.ckAx_check6.Checked = True, "S", "N").ToString
          !ar_xcheck7 = IIf(frmAnex.ckAx_check7.Checked = True, "S", "N").ToString
          !ar_xcheck8 = IIf(frmAnex.ckAx_check8.Checked = True, "S", "N").ToString
          !ar_xcheck9 = IIf(frmAnex.ckAx_check9.Checked = True, "S", "N").ToString
          !ar_xcheck10 = IIf(frmAnex.ckAx_check10.Checked = True, "S", "N").ToString
        End With
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      If Not frmAnex Is Nothing Then frmAnex.Dispose()
      frmAnex = Nothing
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSimula_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSimula.ItemClick
    Dim oPar As New CLE__CLDP

    Try
      '--------------------------------------------------------------------------------------------------------------
      If (tlbSalva.Enabled = True) And (tlbSalva.Visible = True) Then
        If Salva() = False Then Return
      Else
        If oCleArti.bNew = True Then Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      oPar.strPar1 = edAr_codart.Text
      oMenu.RunChild("NTSInformatica", "FRMMGSIMU", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function Apri(ByVal bNew As Boolean, ByVal bDuplica As Boolean) As Boolean
    Dim oParam As New CLE__CLDP
    Dim frmCoar As FRMMGCOAR = Nothing
    Dim bDuplEster As Boolean
    Dim strArtProposto As String = ""
    Try

      If Not oCallParams Is Nothing Then
        'APRI DA GEST
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "APRI;" Then

          oCleArti.strCodart = NTSCStr(Microsoft.VisualBasic.Mid(oCallParams.strParam, 6))
          bNew = False
          'se richiamato apri da gestione con codice articolo salta la modale di apertura
          If oCleArti.strCodart <> "" Then oCleArti.bDaGest = True

          'non apre un articolo a varianti
          If oCleArti.strCodart <> "" Then
            If Not oCleArti.CheckArtVarianti(oCleArti.strCodart) Then Return False
          End If

          'DUPL DA GEST
        ElseIf Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "DUPL;" Then

          oCleArti.strArtiDescr = Trim(Mid(oCallParams.strParam, 6, 40))
          oCleArti.strArtiDesint = Trim(Mid(oCallParams.strParam, 47, 40))
          oCleArti.lArtiForn = NTSCInt(Mid(oCallParams.strParam, 88, 9))
          oCleArti.strArtiNote = Trim(Mid(oCallParams.strParam, 98))

          bNew = False
          bDuplica = True
          bDuplEster = True

          'NUOV DA GEST
        ElseIf Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then

          oCleArti.strCodart = NTSCStr(Microsoft.VisualBasic.Mid(oCallParams.strParam, 6))
          bNew = True
          bDuplica = False
          'se richiamato nuovo da gestione con codice articolo salta la modale di apertura
          If oCleArti.strCodart <> "" AndAlso oCleArti.strCodart <> "0" Then
            If oCleArti.strCodart.startswith("0§") Then
              strArtProposto = oCleArti.strCodart.Substring(2)
            Else
              oCleArti.bDaGest = True
            End If
          End If
        End If
      End If

      If bNew Then
        'NUOVO

        '-------------------------------------------------
        'creo un nuovo articolo
        If tlbSalva.Enabled And tlbSalva.Visible Then
          If Not Salva() Then Return False
        End If
        SetStato(0)

        If oCleArti.bDaGest = False Then 'SALTA la form modale se chiamato in apertura da gestione NUOVO;

          oCleArti.bCoarGeneraArticoli = True
          oCleArti.nCoarEditmode = 2
          oCleArti.strCodart = ""

          frmCoar = CType(NTSNewFormModal("FRMMGCOAR"), FRMMGCOAR)
          frmCoar.Init(oMenu, oParam, DittaCorrente)
          frmCoar.InitEntity(oCleArti)
          frmCoar.bCoarDuplica = False
          frmCoar.edCodart.Text = strArtProposto
          frmCoar.ShowDialog()

          If frmCoar.bCoarAnnullato = True Then Return False
        End If

        If oCleArti.strCodart = "" Then Return False
        If oCleArti.strModello.Trim = "" Then
          If Not NuovoArticolo() Then Return False
        Else
          bDuplica = True
          Dim strArt As String = oCleArti.strCodart
          oCleArti.strCodart = oCleArti.strModello
          If Not ApriArticolo(bDuplica, 1) Then Return False
          If Not DuplicaArticolo(bDuplEster, strArt) Then Return False
          bDuplica = False
        End If
      Else
        If Not ApriArticolo(bDuplica, nStato) Then Return False
      End If

      If bDuplica Then DuplicaArticolo(bDuplEster, "")

      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      If bNew And Not bDuplica Then
        oCleArti.bHasChanges = True
        Me.GctlApplicaDefaultValue()
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmCoar Is Nothing Then frmCoar.Dispose()
      frmCoar = Nothing
    End Try
  End Function
  Public Overridable Function ApriArticoli() As Boolean
    Try
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      If Not oCleArti.Apri(DittaCorrente, dsArti) Then Return False
      If dsArti.Tables("ARTICO").Rows.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130512096038995247, "Nessun articolo trovato."))
        Return False
      End If
      dcArti.DataSource = dsArti.Tables("ARTICO")
      dsArti.AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()
      dcArti.ResetBindings(False)
      dcArti.MoveFirst()
      If Not oCallParams Is Nothing AndAlso oCallParams.strPar3 <> "" Then
        'Cerca di posizionarsi sullo stesso articolo che è stato passato
        Dim lPos As Integer = dcArti.Find("ar_codart", oCallParams.strPar3)
        If lPos >= 0 Then dcArti.Position = lPos
      End If

      ApriListSconProv()

      SetStato(1)

      edAr_codart.Enabled = False
      edAr_unmis.Enabled = False
      If Not NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_confez2) = "" Then
        edAr_confez2.Enabled = False
      End If
      If Not NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_unmis2) = "" Then
        edAr_unmis2.Enabled = False
      End If
      edAr_descr.Focus()
      oCleArti.bValarti = False

      AbilitaControlli()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function



  Public Overridable Function NuovoArticolo() As Boolean
    Try
      oCleArti.Nuovo(dsArti)
      lbClassifica.Text = ""
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      dcArti = New BindingSource
      dcArti.DataSource = dsArti.Tables("ARTICO")
      dcArti.MoveLast()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()
      dcArti.ResetBindings(False)
      NumeroPost()

      SetStato(1)

      GctlSetVisEnab(edAr_unmis, False)
      GctlSetVisEnab(edAr_confez2, False)
      GctlSetVisEnab(edAr_unmis2, False)
      GctlSetVisEnab(edAr_um4, False)
      GctlSetVisEnab(edAr_perqta, False)
      GctlSetVisEnab(cbAr_codtipa, False)

      If oCleArti.bLogisticaEstesa Then
        GctlSetVisEnab(ckAr_gesfasi, False)
        GctlSetVisEnab(ckAr_gesubic, False)
      Else
        ckAr_gesfasi.Enabled = False
        ckAr_gesubic.Enabled = False
        If oCleArti.bGestUbicSenzaLext Then
          GctlSetVisEnab(ckAr_gesubic, False)
        Else
          ckAr_gesubic.Enabled = False
        End If
      End If

      GctlSetVisEnab(ckAr_geslotti, False)
      GctlSetVisEnab(ckAr_flgift, False)
      GctlSetVisEnab(ckAr_gestmatr, False)
      GctlSetVisEnab(cbAr_gescomm, False)
      If oCleArti.strTipoConfiguratore > "2" Then
        GctlSetVisEnab(cbAr_tipitemcp3, False)
        GctlSetVisEnab(lbAr_tipitemcp3, False)
      End If
      edAr_codalt.Focus()
      oCleArti.bValarti = False
      tlbZoomRigheOffertePerArt.Enabled = False
      tlbGift.Enabled = False
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupGPE) Then
        GctlSetVisEnab(ckAr_flgift, False)
      Else
        ckAr_flgift.Enabled = False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function DuplicaArticolo(ByVal bDuplEster As Boolean, ByVal strCodart As String) As Boolean
    Dim frmDuar As FRMMGDUAR = Nothing
    Try
      '------------------------------------------------
      oCleArti.bDuplicazioneInCorso = True
      oCleArti.strCodartDuar = ""
      frmDuar = CType(NTSNewFormModal("FRMMGDUAR"), FRMMGDUAR)
      frmDuar.Init(oMenu, Nothing, DittaCorrente)
      frmDuar.oCleArti = oCleArti
      frmDuar.edCodart.Text = strCodart
      frmDuar.ShowDialog()
      If frmDuar.bOk = False Or oCleArti.strCodartDuar = "" Then
        'annullato
        tlbRipristina_ItemClick(tlbRipristina, Nothing)
        oCleArti.bDuplicazioneInCorso = False
        Return False
      End If

      '-----------------------------------------------------------------------------------------
      '--- Se ar_gesvar = "K" non permette la duplicazione
      '-----------------------------------------------------------------------------------------
      If NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_gesvar) = "K" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128560027153593750, "Articolo configurato non duplicabile."))
        'annullato
        tlbRipristina_ItemClick(tlbRipristina, Nothing)
        oCleArti.bDuplicazioneInCorso = False
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se ar_tipitemcp3 = "E" non permette la duplicazione
      '-----------------------------------------------------------------------------------------
      If NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_tipitemcp3) = "E" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128560027876562500, "Articolo extralogistico non duplicabile."))
        'annullato
        tlbRipristina_ItemClick(tlbRipristina, Nothing)
        oCleArti.bDuplicazioneInCorso = False
        Return False
      End If
      '------------

      SetStato(1)

      GctlTipoDoc = " "
      GctlSetRoules()
      ceListini.LcTipo = GctlTipoDoc   '(sempre dopo la gctlsetroules) serve per forzare la visualizzaz della colonna conto e/o articolo e/o listino
      ceSconti.TipoSconto = 0
      ceProvvig.TipoProvv = 1
      GctlApplicaDefaultValue()

      If Not oCleArti.DuplicaArticolo(oCleArti.strCodartDuar, _
                                      frmDuar.ckListini.Checked, frmDuar.ckSconti.Checked, _
                                      frmDuar.ckProvvigioni.Checked, frmDuar.ckArtval.Checked, _
                                      frmDuar.ckKit.Checked, frmDuar.ckConai.Checked, _
                                      frmDuar.ckArtfasi.Checked, bDuplEster, frmDuar.ckValoriAttributi.Checked, frmDuar.ckArtmaga.Checked) Then
        'ripristino
        If dsArti.Tables("ARTICO").Rows.Count = 1 And dsArti.Tables("ARTICO").Rows(0).RowState = DataRowState.Added Then
          NTSFormClearDataBinding(Me)
        End If
        oCleArti.Ripristina(dcArti.Position, dcArti.Filter)
        If tsArti.SelectedTabPage.Equals(tsArti.TabPages(3)) Then
          ceListini.Ripristina()
        ElseIf tsArti.SelectedTabPage.Equals(tsArti.TabPages(4)) Then
          ceSconti.Ripristina()
        ElseIf tsArti.SelectedTabPage.Equals(tsArti.TabPages(5)) Then
          ceProvvig.Ripristina()
        End If

        SetStato(0)

        oCleArti.bDuplicazioneInCorso = False

        Return False
      End If

      AbilitaControlli()
      CaricaClassificazione()
      oCleArti.bDuplicazioneInCorso = False

      oApp.MsgBoxInfo(oApp.Tr(Me, 128473853444846662, "Duplicazione terminata"))
      oCleArti.strCodart = oCleArti.strCodartDuar

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmDuar Is Nothing Then frmDuar.Dispose()
      frmDuar = Nothing
    End Try
  End Function
  Public Overridable Function ApriArticolo(ByVal bDuplica As Boolean, ByVal nPrecStato As Integer) As Boolean
    Dim frmCoar As FRMMGCOAR = Nothing
    Dim oParam As New CLE__CLDP
    Try
      'APRI
      '-------------------------------------------------
      'apro un nuovo articolo
      If tlbSalva.Enabled And tlbSalva.Visible Then
        If Not Salva() Then Return False
      End If
      SetStato(0)

      'SALTA la form modale se chiamato in apertura da gestione APRI;
      'SALTA la form modale se chiamato in apertura da duplica in stato 1
      If oCleArti.bDaGest = False And (Not (bDuplica = True And nPrecStato = 1 And oCleArti.strCodart <> "")) Then

        oCleArti.nCoarEditmode = 1
        oCleArti.strCodart = ""
        oCleArti.strWhereFiar = ""

        frmCoar = CType(NTSNewFormModal("FRMMGCOAR"), FRMMGCOAR)
        frmCoar.Init(oMenu, oParam, DittaCorrente)
        frmCoar.InitEntity(oCleArti)
        If bDuplica Then
          frmCoar.bCoarDuplica = True
          frmCoar.cmdSeleziona.Enabled = False
        End If
        frmCoar.ShowDialog()

        If frmCoar.bCoarAnnullato = True Then Return False

        If (oCleArti.strCodart = "") And (oCleArti.strWhereFiar = "") Then Return False

      End If

      ceSconti.TipoSconto = 1
      ceProvvig.TipoProvv = 4
      GctlApplicaDefaultValue()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      If Not oCleArti.Apri(DittaCorrente, dsArti) Then Return False
      If dsArti.Tables("ARTICO").Rows.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128553319361865862, "Nessun articolo trovato."))
        Return False
      End If
      dcArti.DataSource = dsArti.Tables("ARTICO")
      dsArti.AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()
      dcArti.ResetBindings(False)
      dcArti.MoveFirst()

      NumeroPost()
      ApriListSconProv()

      SetStato(1)

      edAr_codart.Enabled = False
      edAr_unmis.Enabled = False
      ckAr_gesfasi.Enabled = False
      ckAr_gesubic.Enabled = False
      If oCleArti.bSbloccaFLotto = False Then ckAr_geslotti.Enabled = False
      ckAr_gestmatr.Enabled = False
      'cbAr_gescomm.Enabled = False
      ckAr_flgift.Enabled = False
      If Not NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_confez2) = "" Then
        edAr_confez2.Enabled = False
      End If
      If Not NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_unmis2) = "" Then
        edAr_unmis2.Enabled = False
      End If
      If Not NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_um4) = "" Then
        edAr_um4.Enabled = False
      End If
      '-- Blocchi GIFT CARD
      If NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_flgift) = "S" Then
        Dim dttArtGift As New DataTable
        oMenu.ValCodiceDb(NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codart), DittaCorrente, "ARTICOGIFT", "S", "", dttArtGift)

        If dttArtGift.Rows.Count = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129999772017209194, "ATTENZIONE! Per poter utilizzare questa Gitf Card è necessario accedere al 'Dettaglio Gift Card' nel menù strumenti."))
        End If

        ckAr_gesfasi.Enabled = False
        ckAr_geslotti.Enabled = False
        ckAr_gestmatr.Enabled = False
        cbAr_gescomm.Enabled = False
        cbAr_gescon.Enabled = False
        cbAr_tipokit.Enabled = False

        GctlSetVisEnab(tlbGift, False)
      Else
        tlbGift.Enabled = False
      End If
      ckAr_flgift.Enabled = False
      '--
      If oCleArti.strTipoConfiguratore > "2" Then
        cbAr_tipitemcp3.Enabled = False
        lbAr_tipitemcp3.Enabled = False
      End If
      edAr_descr.Focus()
      oCleArti.bValarti = False

      AbilitaControlli()
      CaricaClassificazione()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If Not frmCoar Is Nothing Then frmCoar.Dispose()
      frmCoar = Nothing
    End Try
  End Function



  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Dim bNew As Boolean = False
    Dim dttTmp As New DataTable

    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus
      '--------------------------------------------------------------------------------------------------------------
      '--- Se il contenuto del TextBox relativo alla Distinta Base NON è cambiato rispetto al dato nel database
      '--- e nel database il codice è diverso, mette il contenuto presente nel database nel Textbox relativo
      '--- (questo prima del salvataggio)
      '--------------------------------------------------------------------------------------------------------------
      If oCleArti.bNew = False Then
        If NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)("ar_coddb", DataRowVersion.Original)).ToUpper() = NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_coddb).ToUpper Then
          If oMenu.ValCodiceDb(edAr_codart.Text, DittaCorrente, "ARTICO", "S", "", dttTmp) = True Then
            If NTSCStr(dttTmp.Rows(0)!ar_coddb).ToUpper <> NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_coddb).ToUpper Then
              dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_coddb = NTSCStr(dttTmp.Rows(0)!ar_coddb)
            End If
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleArti.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128550728308290936, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.No Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          bNew = oCleArti.bNew
          If Not oCleArti.Salva(False) Then
            Return False
          Else
            If bNew Then
              'memorizzo il nuovo conto negli appunti, così se sono stato chiamato da ALT+F2 posso acquisire in automatico il nuovo conto
              oApp.NTSClipboard = "NTSNEW:ARTICO:" & edAr_codart.Text
            End If
            AbilitaControlli()
            CaricaClassificazione()
          End If
        End If
      End If

      '-------------------------------------------------
      'salvo i listini/sconti/provvigioni
      If Not ceListini.Salva Then Return False
      If Not ceSconti.Salva Then Return False
      If Not ceProvvig.Salva Then Return False
      '--------------------------------------------------------------------------------------------------------------
      AbilitaDisabilitaTipo()
      '--------------------------------------------------------------------------------------------------------------
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Sub SetStato(ByVal nSetStato As Integer)
    Try
      '----------------------------------
      '0 = nessuna registraz aperta
      If nSetStato = 0 Then
        nStato = 0

        ceListini.LcCodart = ""
        ceSconti.SoCodart = ""
        ceProvvig.PerCodart = ""

        '---------------------------------------
        'gestione dei tasti di scelta rapida di listini/sconti/provvigioni
        'visto che i tasti di scelta rapida sono gli stessi, per fare in modo che operino correttamente 
        'devo disabilitare i controlli listini/sconti/provvig che non sono visibili, in questo modo 
        'è abilitata una sola toolbar per volta a parità di tasti di scelta rapida
        ceListini.Enabled = False
        ceSconti.Enabled = False
        ceProvvig.Enabled = False

        pnArti.visible = False

        GctlSetVisEnab(tlbNuovo, False)
        GctlSetVisEnab(tlbDuplica, False)

        tlbSalva.Enabled = False
        tlbCancella.Enabled = False
        tlbRipristina.Enabled = False
        tlbZoom.Enabled = False
        tlbPrimo.Enabled = False
        tlbPrecedente.Enabled = False
        tlbSuccessivo.Enabled = False
        tlbUltimo.Enabled = False
        tlbBarcode.Enabled = False
        tlbOle.Enabled = False
        tlbKit.Enabled = False
        tlbConai.Enabled = False
        tlbStampaEtichette.Enabled = False
        GctlSetVisEnab(tlbAcquisisciDaCatForn, False)
        tlbRicalcolaListini.Enabled = False
        tlbZoomRigheOffertePerArt.Enabled = False
        tlbVisualizzaMovimenti.Enabled = False
        tlbControlloQualita.Enabled = False
        tlbAttributiArticolo.Enabled = False
        tlbArticoliMagazzino.Enabled = False
        tlbFasiArticolo.Enabled = False
        tlbPromozioni.Enabled = False
        tlbAccessoriSuccedanei.Enabled = False
        tlbEstensioni.Enabled = False
        tlbSimula.Enabled = False
        tlbGift.Enabled = False

        If Not dsArti Is Nothing Then
          dsArti.AcceptChanges()
          oCleArti.bHasChanges = False
        End If
        edFocus.Enabled = True
        If Me.NTSActiveFirstOccured Then edFocus.Focus()
      Else
        nStato = 1

        tsArti.SelectedTabPageIndex = 0

        GctlSetVisEnab(pnArti, True)

        GctlSetVisEnab(pnArti, False)
        GctlSetVisEnab(tlbNuovo, False)
        GctlSetVisEnab(tlbApri, False)
        GctlSetVisEnab(tlbSalva, False)
        GctlSetVisEnab(tlbCancella, False)
        GctlSetVisEnab(tlbRipristina, False)
        GctlSetVisEnab(tlbZoom, False)
        GctlSetVisEnab(tlbPrimo, False)
        GctlSetVisEnab(tlbPrecedente, False)
        GctlSetVisEnab(tlbSuccessivo, False)
        GctlSetVisEnab(tlbUltimo, False)
        GctlSetVisEnab(tlbBarcode, False)
        GctlSetVisEnab(tlbOle, False)
        GctlSetVisEnab(tlbKit, False)
        GctlSetVisEnab(tlbConai, False)
        GctlSetVisEnab(tlbStampaEtichette, False)
        tlbAcquisisciDaCatForn.Enabled = False
        tlbRicalcolaListini.Enabled = False
        GctlSetVisEnab(tlbZoomRigheOffertePerArt, False)
        GctlSetVisEnab(tlbVisualizzaMovimenti, False)
        GctlSetVisEnab(tlbControlloQualita, False)
        GctlSetVisEnab(tlbAttributiArticolo, False)
        GctlSetVisEnab(tlbArticoliMagazzino, False)
        If oCleArti.bLogisticaEstesa = True Then
          GctlSetVisEnab(tlbFasiArticolo, False)
        End If
        GctlSetVisEnab(tlbPromozioni, False)
        GctlSetVisEnab(tlbAccessoriSuccedanei, False)
        GctlSetVisEnab(tlbEstensioni, False)

        If oCleArti.bCampiCAEAttivi Then
          GctlSetVisEnab(fmCadc, True)
        Else
          fmCadc.Visible = False
        End If

        GctlSetVisEnab(pnMaga2, False)
        GctlSetVisEnab(lbAr_scosic, False)
        GctlSetVisEnab(edAr_scosic, False)
        GctlSetVisEnab(tlbSimula, False)
        GctlSetVisEnab(tlbGift, False)

        edFocus.Enabled = False
      End If

      edAr_codart.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      AbilitaDisabilitaTipo()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub AbilitaControlli()
    Try
      If NTSCStr(cbAr_perragg.SelectedValue) = "G" Then
        edAr_ggragg.Enabled = False
      Else
        GctlSetVisEnab(edAr_ggragg, False)
      End If
      edAr_unmis.Enabled = False
      ckAr_gesfasi.Enabled = False
      ckAr_gesubic.Enabled = False
      If oCleArti.bSbloccaFLotto = False Then ckAr_geslotti.Enabled = False
      ckAr_gestmatr.Enabled = False
      'cbAr_gescomm.Enabled = False
      ckAr_flgift.Enabled = False
      If oCleArti.strTipoConfiguratore > "2" Then
        cbAr_tipitemcp3.Enabled = False
        lbAr_tipitemcp3.Enabled = False
      End If
      If Not NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_confez2) = "" Then
        edAr_confez2.Enabled = False
      Else
        GctlSetVisEnab(edAr_confez2, False)
      End If
      If Not NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_unmis2) = "" Then
        edAr_unmis2.Enabled = False
      Else
        GctlSetVisEnab(edAr_unmis2, False)
      End If
      If Not NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_um4) = "" Then
        edAr_um4.Enabled = False
      Else
        GctlSetVisEnab(edAr_um4, False)
      End If
      cbAr_codtipa.Enabled = False
      edAr_perqta.Enabled = False
      '-- Blocchi GIFT CARD
      If NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_flgift) = "S" Then
        ckAr_gesfasi.Enabled = False
        ckAr_geslotti.Enabled = False
        ckAr_gestmatr.Enabled = False
        cbAr_gescomm.Enabled = False
        cbAr_gescon.Enabled = False
        cbAr_tipokit.Enabled = False

        GctlSetVisEnab(tlbGift, False)
      Else
        tlbGift.Enabled = False
      End If
      ckAr_flgift.Enabled = False
      '---------------------------------------------------------------------------------------
      Select Case NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_gesfasi)
        Case "S"
          GctlSetVisEnab(edAr_ultfase, False)
          GctlSetVisEnab(lbAr_ultfase, False)
        Case "N"
          edAr_ultfase.Enabled = False
          lbAr_ultfase.Enabled = False
      End Select
      If Trim(edAr_gif1.Text) = "" Then
        cmdVisGif1.Enabled = False
      Else
        GctlSetVisEnab(edAr_gif1, False)
      End If
      If Trim(edAr_gif2.Text) = "" Then
        cmdVisGif2.Enabled = False
      Else
        GctlSetVisEnab(edAr_gif2, False)
      End If
      tlbZoomRigheOffertePerArt.Enabled = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ApriListSconProv()
    Try
      '-------------------------------
      'se serve ricarico i listini
      If tsArti.SelectedTabPage.Equals(tsArti.TabPages(3)) Then
        If ceListini.LcCodart <> NTSCStr(edAr_codart.Text) Then
          ceListini.LcCodart = NTSCStr(edAr_codart.Text)
          ceListini.ApriListini()
        End If

        GctlSetVisEnab(ceListini, False)
        GctlSetVisEnab(ceListini.grvList, False)
        ceListini.tlbMain.Visible = True
      End If

      '-------------------------------
      'se serve ricarico gli sconti
      If tsArti.SelectedTabPage.Equals(tsArti.TabPages(4)) Then
        If ceSconti.SoCodart <> NTSCStr(edAr_codart.Text) Then
          ceSconti.SoCodart = NTSCStr(edAr_codart.Text)
          ceSconti.ApriSconti()
        End If

        GctlSetVisEnab(ceSconti, False)
        GctlSetVisEnab(ceSconti.grvSconti, False)
        ceSconti.tlbMain.Visible = True
      End If

      '-------------------------------
      'se serve ricarico le provvigioni
      If tsArti.SelectedTabPage.Equals(tsArti.TabPages(5)) Then
        If ceProvvig.PerCodart <> NTSCStr(edAr_codart.Text) Then
          ceProvvig.PerCodart = NTSCStr(edAr_codart.Text)
          ceProvvig.ApriProvvigioni()
        End If

        GctlSetVisEnab(ceProvvig, False)
        GctlSetVisEnab(ceProvvig.grvProv, False)
        ceProvvig.tlbMain.Visible = True
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbAr_perragg_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAr_perragg.SelectedValueChanged
    Try
      If dsArti Is Nothing Then Return
      If cbAr_perragg.SelectedValue Is Nothing Then Return
      dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_perragg = cbAr_perragg.SelectedValue
      If NTSCStr(cbAr_perragg.SelectedValue) = "G" Then
        edAr_ggragg.NTSTextDB = "1"
        edAr_ggragg.Enabled = False
      Else
        GctlSetVisEnab(edAr_ggragg, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAr_gif1_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAr_gif1.Validated
    Try
      If Trim(NTSCStr(edAr_gif1.Text)) = "" Then
        cmdVisGif1.Enabled = False
      Else
        GctlSetVisEnab(cmdVisGif1, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAr_gif2_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAr_gif2.Validated
    Try
      If Trim(NTSCStr(edAr_gif2.Text)) = "" Then
        cmdVisGif2.Enabled = False
      Else
        GctlSetVisEnab(cmdVisGif2, False)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function IsDimensioniImmagineNeiLimiti(ByVal strPathFile As String) As Boolean
    Dim fInfo As FileInfo = Nothing
    Dim lFileKb As Integer = 0
    Try
      If nDimensioneImmagine > 0 Then
        If System.IO.File.Exists(strPathFile) Then

          fInfo = New FileInfo(strPathFile)
          lFileKb = NTSCInt(fInfo.Length / 1024)
          If lFileKb > nDimensioneImmagine Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 130415974728281250, "Le dimensioni dell'immagine da allegare superano il limite impostato tramite l'opzione di registro."))
            Return False
          End If
        End If
      End If

      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function ApriGif(ByVal nQualegif As Integer) As Boolean
    Dim bOk As Boolean = False
    Dim dRes As DialogResult
    Dim strNomeFile As String = ""
    Dim strPathFile As String = ""
    Dim nPosSep As Integer = 0
    Dim i As Integer = 0
    Dim strFileTmp As String = ""
    Dim strExtension As String = ""

    Try
      '-----------------------------------------------------------------------------------------
      '--- Se non esiste la cartella delle immagini chiede di crearla
      '-----------------------------------------------------------------------------------------
      If Not Directory.Exists(oCleArti.strImageDir) Then
        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128559133252059319, "La cartella delle immagini non esiste. Crearla?"))
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          Try
            MkDir(oCleArti.strImageDir)
          Catch ex As Exception
            Exit Function
          End Try
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Adesso apre la Common Dialog sulla cartella delle immagini
      '-----------------------------------------------------------------------------------------
      If OpenFileDialog1 Is Nothing Then OpenFileDialog1 = New NTSOpenFileDialog
      OpenFileDialog1.CheckFileExists = True
      OpenFileDialog1.ShowReadOnly = False
      OpenFileDialog1.ShowHelp = False
      OpenFileDialog1.DefaultExt = "gif"
      OpenFileDialog1.Title = "Selezione immagine"
      OpenFileDialog1.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"
      OpenFileDialog1.InitialDirectory = oCleArti.strImageDir
      OpenFileDialog1.FileName = ""
      OpenFileDialog1.oMenu = oMenu
      OpenFileDialog1.ShowDialog()

      '-----------------------------------------------------------------------------------------
      If OpenFileDialog1.FileName <> "" Then
        'cerca l'ultimo simbolo \ per dividere il path dal nome del file
        'ATTENZIONE il nome del file non deve contenere il simbolo \
        For i = 1 To Len(OpenFileDialog1.FileName)
          If Mid(OpenFileDialog1.FileName, i, 1) = "\" Then
            nPosSep = i
          End If
        Next
        If nPosSep > 0 Then
          strNomeFile = Microsoft.VisualBasic.Mid(OpenFileDialog1.FileName, nPosSep + 1)
          strPathFile = Microsoft.VisualBasic.Left(OpenFileDialog1.FileName, nPosSep)
        Else
          strNomeFile = OpenFileDialog1.FileName
        End If

        'Se e stata impostata l'apposita opzione di registro 
        'verra effettuato il controllo sulle dimensioni dell'immagine da allegare
        If Not IsDimensioniImmagineNeiLimiti(strPathFile & strNomeFile) Then
          Return False
        End If

        '---------------------------------------------------------------------------------------
        '--- Se l'immagine selezionata non è nella cartella delle immagini
        '--- allora la copio nella cartella delle immagini
        '---------------------------------------------------------------------------------------
        If UCase(strPathFile) <> UCase(oCleArti.strImageDir) Then
          If Not System.IO.File.Exists(oCleArti.strImageDir & strNomeFile) Then
            Try
              FileCopy(OpenFileDialog1.FileName, oCleArti.strImageDir & strNomeFile)
            Catch ex As Exception
              Exit Function
            End Try
          Else
            strFileTmp = oCleArti.strImageDir & strNomeFile
            strExtension = System.IO.Path.GetExtension(strFileTmp)
            strFileTmp = Mid(strFileTmp, 1, strFileTmp.Length - strExtension.Length)
            For i = 1 To 1000
              If System.IO.File.Exists(strFileTmp & "_" & i.ToString & strExtension) = False Then
                FileCopy(OpenFileDialog1.FileName, strFileTmp & "_" & i.ToString & strExtension)
                strNomeFile = strFileTmp & "_" & i.ToString & strExtension
                bOk = True
                Exit For
              End If
            Next
            If bOk = False Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 130421312902636697, "Attenzione!" & vbCrLf & _
                "Non è possibile selezionare questo file."))
              Return False
            Else
              For i = 1 To strNomeFile.Length
                If Mid(strNomeFile, i, 1) = "\" Then nPosSep = i
              Next
              If nPosSep > 0 Then strNomeFile = Microsoft.VisualBasic.Mid(strNomeFile, nPosSep + 1)
            End If
          End If
        End If
        '---------------------------------------------------------------------------------------
        If strNomeFile.Length > 50 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130374521581617698, "Attenzione!" & vbCrLf & _
            "Il nome/estensione de file indicato non può surerare i 50 caratteri."))
          Exit Function
        End If
        '---------------------------------------------------------------------------------------
        '--- Inseridce nel TextBox relativo il file immagine selezionato
        'Lo inserisco dopo aver ricopiato l'immagine se no da msg la BeforeColUpdate
        '---------------------------------------------------------------------------------------
        If nQualegif = 1 Then
          edAr_gif1.NTSTextDB = strNomeFile
        Else ' = 2
          edAr_gif2.NTSTextDB = strNomeFile
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub tsArti_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tsArti.SelectedPageChanged
    Try
      '---------------------------------------
      'gestione dei tasti di scelta rapida di listini/sconti/provvigioni
      'visto che i tasti di scelta rapida sono gli stessi, per fare in modo che operino correttamente 
      'devo disabilitare i controlli listini/sconti/provvig che non sono visibili, in questo modo 
      'è abilitata una sola toolbar per volta a parità di tasti di scelta rapida

      If e.Page.Equals(tsArti.TabPages(3)) Then
        GctlSetVisEnab(tlbRicalcolaListini, False) 'abilita il ricalcolo listini
      Else
        tlbRicalcolaListini.Enabled = False
      End If

      '-----------------------------
      'pagina dei listini
      If e.Page.Equals(tsArti.TabPages(3)) Then
        If oCleArti.bNew Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128466988465551088, "Salvare il nuovo articolo prima di passare alla cartella LISTINI"))
          tsArti.SelectedTabPageIndex = 0
          Return
        End If
        If ckAr_flgift.Checked Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129991950584879473, "Gli articoli di tipo Gift Card non possono accedere ai listini"))
          tsArti.SelectedTabPageIndex = 0
          Return
        End If

        ceListini.LcCodart = NTSCStr(edAr_codart.Text)
        ceListini.ApriListini()

        GctlSetVisEnab(ceListini, False)
        GctlSetVisEnab(ceListini.grvList, False)
        ceListini.tlbMain.Visible = True

        GctlSetVisEnab(tlbRicalcolaListini, False) 'abilita il ricalcolo listini
      End If

      If e.PrevPage.Equals(tsArti.TabPages(3)) Then
        If Not ceListini.Salva Then
          tsArti.SelectedTabPageIndex = 3
        Else
          ceListini.Enabled = False
        End If
      End If

      '-----------------------------
      'pagina degli sconti
      If e.Page.Equals(tsArti.TabPages(4)) Then
        If oCleArti.bNew Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128473931444347256, "Salvare il nuovo articolo prima di passare alla cartella SCONTI"))
          tsArti.SelectedTabPageIndex = 0
          Return
        End If
        If ckAr_flgift.Checked Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129991950996686578, "Gli articoli di tipo Gift Card non possono accedere agli sconti"))
          tsArti.SelectedTabPageIndex = 0
          Return
        End If

        If ceSconti.SoCodart <> NTSCStr(edAr_codart.Text) Then
          ceSconti.SoCodart = NTSCStr(edAr_codart.Text)
          Dim i As Integer = ceSconti.TipoSconto
          ceSconti.TipoSconto = 1   'serve per far applicare le impostazioni di griglia e ricaricare gli sconti
          ceSconti.TipoSconto = i
        Else
          ceSconti.ApriSconti()
        End If

        GctlSetVisEnab(ceSconti, False)
        GctlSetVisEnab(ceSconti.grvSconti, False)
        ceSconti.tlbMain.Visible = True
      End If

      If e.PrevPage.Equals(tsArti.TabPages(4)) Then
        If Not ceSconti.Salva Then
          tsArti.SelectedTabPageIndex = 4
        Else
          ceSconti.Enabled = False
        End If
      End If

      '-----------------------------
      'pagina delle provvigioni
      If e.Page.Equals(tsArti.TabPages(5)) Then
        If oCleArti.bNew Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128473931950723256, "Salvare il nuovo articolo prima di passare alla cartella PROVVIGIONI"))
          tsArti.SelectedTabPageIndex = 0
          Return
        End If
        If ckAr_flgift.Checked Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 129991951117558288, "Gli articoli di tipo Gift Card non possono accedere alle provvigioni"))
          tsArti.SelectedTabPageIndex = 0
          Return
        End If

        If ceProvvig.PerCodart <> NTSCStr(edAr_codart.Text) Then
          ceProvvig.PerCodart = NTSCStr(edAr_codart.Text)
          ceProvvig.TipoProvv = 4   'serve per far applicare le impostazioni di griglia e ricaricare le provvigioni
        Else
          ceProvvig.ApriProvvigioni()
        End If

        GctlSetVisEnab(ceProvvig, False)
        GctlSetVisEnab(ceProvvig.grvProv, False)
        ceProvvig.tlbMain.Visible = True
      End If

      If e.PrevPage.Equals(tsArti.TabPages(5)) Then
        If Not ceProvvig.Salva Then
          tsArti.SelectedTabPageIndex = 5
        Else
          ceProvvig.Enabled = False
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAr_coddb_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edAr_coddb.NTSZoomGest
    Dim strParam As String
    Dim strCodart As String
    Try

      If e.TipoEvento = "OPEN" Then

        If tlbSalva.Enabled And tlbSalva.Visible Then
          If Not Salva() Then
            e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
            Exit Sub
          End If
        Else
          If oCleArti.bNew Then Return
        End If

        strCodart = edAr_codart.Text
        If strCodart.Trim <> "" Then

          strParam = "APRI;" & strCodart.PadRight(CLN__STD.CodartMaxLen).Substring(0, CLN__STD.CodartMaxLen) & "," & _
            NTSCDate(Now).ToString("dd/MM/yyyy") & "," & _
            Microsoft.VisualBasic.Right(NTSCInt("0").ToString.PadLeft(9, "0"c), 9) & "," & _
            "000000000"
          oMenu.RunChild("BSDBDIBA", "CLSDBDIBA", oApp.Tr(Me, 128557642245856788, "Distinta Base"), DittaCorrente, "", "", Nothing, strParam, True, True)
        End If

        e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAr_note_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAr_note.Enter
    Try
      Me.NTSDisableEnterComeTab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edAr_note_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAr_note.Leave
    Try
      Me.NTSEnableEnterComeTab()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckAr_flgift_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAr_flgift.CheckedChanged
    Try
      If oCleArti.bNew Then
        If ckAr_flgift.Checked Then
          ckAr_gesfasi.Enabled = False
          ckAr_geslotti.Enabled = False
          ckAr_gestmatr.Enabled = False
          cbAr_gescomm.Enabled = False
          cbAr_gescon.Enabled = False
          cbAr_tipokit.Enabled = False

          ckAr_gesfasi.Checked = False
          ckAr_geslotti.Checked = False
          ckAr_gestmatr.Checked = False
          cbAr_gescomm.SelectedValue = "N"
          cbAr_gescon.SelectedValue = "N"
          cbAr_tipokit.SelectedValue = " "

          oApp.MsgBoxInfo(oApp.Tr(Me, 129991932271878418, "Attivando la gestione Gift Card verranno disattivati per questo articolo:" & vbCrLf & _
                                                          " - Lotti " & vbCrLf & _
                                                          " - Matricole " & vbCrLf & _
                                                          " - Fasi " & vbCrLf & _
                                                          " - Commesse " & vbCrLf & _
                                                          " - Kit " & vbCrLf & _
                                                          " - Conai " & vbCrLf & _
                                                          " - Listini " & vbCrLf & _
                                                          " - Sconti " & vbCrLf & _
                                                          " - Provvigioni"))
        Else
          GctlSetVisEnab(ckAr_gesfasi, False)
          GctlSetVisEnab(ckAr_geslotti, False)
          GctlSetVisEnab(ckAr_gestmatr, False)
          GctlSetVisEnab(cbAr_gescomm, False)
          GctlSetVisEnab(cbAr_gescon, False)
          GctlSetVisEnab(cbAr_tipokit, False)
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub AbilitaDisabilitaTipo()
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se Ã¨ attivo il modulo ORTO, in apertura di un articolo esistente, disabilita il campo "Tipo"
      '--------------------------------------------------------------------------------------------------------------
      GctlSetVisEnab(edAr_tipo, False)
      If (oCleArti.bNew = False) And (oCleArti.bModuloORTO = True) Then edAr_tipo.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#Region "logistica di magazzino su palmare"
  Public Overridable Sub edAr_codgrlo_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAr_codgrlo.Validated
    Try
      If NTSCInt(edAr_codgrlo.Text) = 0 Then
        lbXx_codgrlo.Text = ""
        Return
      End If

      If Not oMenu.ValCodiceDb(edAr_codgrlo.Text, DittaCorrente, "TABGRLO", "N", lbXx_codgrlo.Text) Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckAr_staetip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAr_staetip.CheckedChanged
    Try
      edAr_converp.Enabled = ckAr_staetip.Checked
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub NumeroPost()
    Try
      ceColl.Visible = False 'Sempre invisibile, se è un utente social ci penserà lui ad apparire completato l'aggiornamento dei dati

      If oApp.User.SocialUser = "I" Then
        Dim oPar As New CLE__CLDP
        Dim dttDati As New DataTable
        dttDati.Columns.Add("codditt")
        dttDati.Columns.Add("spd_tipo")
        dttDati.Columns.Add("spd_codart")
        dttDati.Rows.Add(New Object() {DittaCorrente, "A", NTSCStr(dsArti.Tables("ARTICO").Rows(dcArti.Position)!ar_codart)})
        oPar.ctlPar1 = dttDati

        ceColl.NTSSetParam(oMenu, "", "BN__CLIE", oPar)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function CaricaClassificazione() As Boolean
    'dati i campi di artico decodifico la classificazione
    Try
      With dsArti.Tables("ARTICO").Rows(dcArti.Position)
        lbClassifica.Text = oCleArti.GetArtclasDescr(NTSCStr(!ar_codcla1).Trim, NTSCStr(!ar_codcla2).Trim, _
                                                     NTSCStr(!ar_codcla3).Trim, NTSCStr(!ar_codcla4).Trim, _
                                                     NTSCStr(!ar_codcla5).Trim)
      End With

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub ceListini_VaiScontoCollegato(ByVal sender As Object, ByRef e As NTSEventArgs)
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Try
      'mi posiziono sul tab 'sconti', imposto il combo su 'specifico cli/arti', 
      'setto la data validità, imposto la promozione e cerco di
      'posizionarmi sulla riga con sconto cli/arti con cli e arti = quello passato da bnxxlist
      strT = e.Message.Split("§"c)

      tsArti.SelectedTabPage = NtsTabPage5
      ceSconti.ckPromo.Checked = CBool(strT(2))
      ceSconti.opValDay.Checked = True
      ceSconti.edDtval.Text = strT(4)
      ceSconti.cbTiposconti.SelectedValue = "4"
      'mi devo posizionare sul record giusto
      For i = 0 To ceSconti.dcScon.List.Count - 1
        If CType(ceSconti.dcScon.Item(i), DataRowView)!so_conto.ToString = strT(0) And _
           CType(ceSconti.dcScon.Item(i), DataRowView)!so_codart.ToString = strT(1) And _
           NTSCDec(CType(ceSconti.dcScon.Item(i), DataRowView)!so_daquant) <= NTSCDec(strT(3)) And _
           NTSCDec(CType(ceSconti.dcScon.Item(i), DataRowView)!so_aquant) >= NTSCDec(strT(3)) Then
          ceSconti.dcScon.Position = i
          Exit For
        End If
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ceSconti_VaiListinoCollegato(ByVal sender As Object, ByRef e As NTSEventArgs)
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Dim dttTmp As New DataTable
    Try
      'mi posiziono sul tab 'listini', imposto il combo su 'specifico cli/arti', 
      'setto la data validità, imposto la promozione, valuta fissa = 0 e cerco di
      'posizionarmi sulla riga con listino cli/arti con cli e arti = quello passato da bnxxscon
      strT = e.Message.Split("§"c)

      tsArti.SelectedTabPage = NtsTabPage4
      ceListini.ckPromo.Checked = CBool(strT(2))
      ceListini.opValDay.Checked = True
      ceListini.edDtval.Text = strT(4)
      ceListini.edCodvalu.Text = "0"
      ceListini.opValEur.Checked = True
      ceListini.cbTipolistino.SelectedValue = "C"
      oMenu.ValCodiceDb(strT(0), DittaCorrente, "ANAGRA", "N", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then ceListini.cbTipolistino.SelectedValue = dttTmp.Rows(0)!an_tipo.ToString
      dttTmp.Clear()
      'mi devo posizionare sul record giusto
      For i = 0 To ceListini.dcList.List.Count - 1
        If CType(ceListini.dcList.Item(i), DataRowView)!lc_conto.ToString = strT(0) And _
           CType(ceListini.dcList.Item(i), DataRowView)!lc_codart.ToString = strT(1) And _
           NTSCDec(CType(ceListini.dcList.Item(i), DataRowView)!lc_daquant) <= NTSCDec(strT(3)) And _
           NTSCDec(CType(ceListini.dcList.Item(i), DataRowView)!lc_aquant) >= NTSCDec(strT(3)) Then
          ceListini.dcList.Position = i
          Exit For
        End If
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function DatiCambiati(ByVal str1 As String, ByVal str2 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If (str1.Trim = "") And (str2.Trim = "") Then Return False
      '--------------------------------------------------------------------------------------------------------------
      If (str1.Trim <> "") And (str2.Trim = "") Then Return True
      '--------------------------------------------------------------------------------------------------------------
      If (str1.Trim = "") And (str2.Trim <> "") Then Return True
      '--------------------------------------------------------------------------------------------------------------
      If NTSCDate(str1).ToShortDateString <> NTSCDate(str2).ToShortDateString Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

End Class

