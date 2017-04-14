Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO

Public Class FRMMGARTV
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

#Region "Variabili"
  Public oCleArtv As CLEMGARTV
  Public dsArtv As DataSet
  Public oCallParams As CLE__CLDP
  Public dcArtv As BindingSource = New BindingSource

  Public nSetStato As Integer
  'Opzioni di registro
  'Limita la dimensione massima delle immagini associabili all'anagrafica articoli.
  Public nDimensioneImmagine As Integer = 0

  Public dsVar1 As DataSet
  Public dcVar1 As BindingSource = New BindingSource
  Public dsVar2 As DataSet
  Public dcVar2 As BindingSource = New BindingSource
  Public dsVar3 As DataSet
  Public dcVar3 As BindingSource = New BindingSource
  Public dsArtico As DataSet
  Public dcArtico As BindingSource = New BindingSource

  Public strOldDEscr As String = ""

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
  Public WithEvents tlbRecordNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordAggiorna As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents pnArtv As NTSInformatica.NTSPanel
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbBarcode As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbKit As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbOle As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaEtichette As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbRicalcolaListini As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbArticoliMagazzino As NTSInformatica.NTSBarMenuItem
  Public WithEvents pnMain As NTSInformatica.NTSPanel
  Public WithEvents tsArtv As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag1 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage4 As NTSInformatica.NTSTabPage
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
  Public WithEvents NtsTabPage5 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag3 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage6 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag4 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage7 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag5 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage8 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag6 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage9 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpag7 As NTSInformatica.NTSPanel
  Public WithEvents edAr_note As NTSInformatica.NTSMemoBox
  Public WithEvents NtsTabPage10 As NTSInformatica.NTSTabPage
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
  Public WithEvents NtsTabPage11 As NTSInformatica.NTSTabPage
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
  Public WithEvents lbXx_clascon As NTSInformatica.NTSLabel
  Public WithEvents lbXx_claprov As NTSInformatica.NTSLabel
  Public WithEvents lbAr_claprov As NTSInformatica.NTSLabel
  Public WithEvents edAr_claprov As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_clascon As NTSInformatica.NTSLabel
  Public WithEvents edAr_clascon As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnListiniTop As NTSInformatica.NTSPanel
  Public WithEvents pnListiniBottom As NTSInformatica.NTSPanel
  Public WithEvents lbArtListini As NTSInformatica.NTSLabel
  Public WithEvents pnProvvigioniBottom As NTSInformatica.NTSPanel
  Public WithEvents pnProvvigioniTop As NTSInformatica.NTSPanel
  Public WithEvents lbArtProvvigioni As NTSInformatica.NTSLabel
  Public WithEvents pnScontiBottom As NTSInformatica.NTSPanel
  Public WithEvents pnScontiTop As NTSInformatica.NTSPanel
  Public WithEvents lbArtSconti As NTSInformatica.NTSLabel

  Public WithEvents pnTabpag9 As NTSInformatica.NTSPanel
  Public WithEvents fmUbiFasi As NTSInformatica.NTSGroupBox
  Public WithEvents ckAr_gesubic As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_codimba As NTSInformatica.NTSLabel
  Public WithEvents lbXx_flmod As NTSInformatica.NTSLabel
  Public WithEvents lbAr_flmod As NTSInformatica.NTSLabel
  Public WithEvents edAr_flmod As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_volume As NTSInformatica.NTSLabel
  Public WithEvents edAr_volume As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_codimba As NTSInformatica.NTSLabel
  Public WithEvents edAr_codimba As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_misura1 As NTSInformatica.NTSLabel
  Public WithEvents edAr_misura1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_misura2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_misura3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnTabpagElencoVar As NTSInformatica.NTSPanel
  Public WithEvents pnTabpagAnaliticoVar As NTSInformatica.NTSPanel
  Public WithEvents grVar1 As NTSInformatica.NTSGrid
  Public WithEvents grvVar1 As NTSInformatica.NTSGridView
  Public WithEvents grVar2 As NTSInformatica.NTSGrid
  Public WithEvents grvVar2 As NTSInformatica.NTSGridView
  Public WithEvents grVar3 As NTSInformatica.NTSGrid
  Public WithEvents grvVar3 As NTSInformatica.NTSGridView
  Public WithEvents lbLivello1 As NTSInformatica.NTSLabel
  Public WithEvents lbLivello3 As NTSInformatica.NTSLabel
  Public WithEvents lbLivello2 As NTSInformatica.NTSLabel
  Public WithEvents cmdEspSel As NTSInformatica.NTSButton
  Public WithEvents cmdAggiungi As NTSInformatica.NTSButton
  Public WithEvents cmdEsplodi As NTSInformatica.NTSButton
  Public WithEvents xx_codvar1 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr1 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codditt1 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codvar2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codditt2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_seleziona1 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_seleziona2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_seleziona3 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codvar3 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr3 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codditt3 As NTSInformatica.NTSGridColumn
  Public WithEvents grArtico As NTSInformatica.NTSGrid
  Public WithEvents grvArtico As NTSInformatica.NTSGridView
  Public WithEvents ar_codvar1 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_codvar2 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_codvar3 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ar_desint As NTSInformatica.NTSGridColumn
  Public WithEvents ar_scomin As NTSInformatica.NTSGridColumn
  Public WithEvents ar_scomax As NTSInformatica.NTSGridColumn
  Public WithEvents ar_minord As NTSInformatica.NTSGridColumn
  Public WithEvents ar_inesaur As NTSInformatica.NTSGridColumn
  Public WithEvents ar_note As NTSInformatica.NTSGridColumn
  Public WithEvents ar_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ar_formula As NTSInformatica.NTSGridColumn
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
#End Region

  Public Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Dim i As Integer = 0
    Dim strTmp() As String

    If Not IsMyThrowRemoteEvent() Then Return
    MyBase.GestisciEventiEntity(sender, e)
    Try
      If e.TipoEvento.Trim.Length < 10 Then Return
      strTmp = e.TipoEvento.Split(CType("|", Char))

      For i = 0 To strTmp.Length - 1
        Select Case strTmp(i).Substring(0, 10)
          Case "ASKVISLOG:"
            If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 130245900853993191, "Attenzione!" & vbCrLf & _
                        "Esistono varianti per le quali NON sono stati generati i Barcode." & vbCrLf & _
                        "Procedere con la visualizzazione del file di LOG contenente l'elenco di tali varianti?")) = Windows.Forms.DialogResult.Yes Then
              NTSProcessStart("notepad", "VARIANTI.txt")
            End If
        End Select
      Next
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGARTV))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbDuplica = New NTSInformatica.NTSBarButtonItem
    Me.tlbApri = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordAggiorna = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbRecordCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbStampaEtichette = New NTSInformatica.NTSBarMenuItem
    Me.tlbRicalcolaListini = New NTSInformatica.NTSBarMenuItem
    Me.tlbArticoliMagazzino = New NTSInformatica.NTSBarMenuItem
    Me.tlbAccessoriSuccedanei = New NTSInformatica.NTSBarMenuItem
    Me.tlbLingua = New NTSInformatica.NTSBarMenuItem
    Me.tlbEstensioni = New NTSInformatica.NTSBarMenuItem
    Me.tlbSimula = New NTSInformatica.NTSBarMenuItem
    Me.tlbBarcode = New NTSInformatica.NTSBarButtonItem
    Me.tlbKit = New NTSInformatica.NTSBarButtonItem
    Me.tlbOle = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnArtv = New NTSInformatica.NTSPanel
    Me.pnMain = New NTSInformatica.NTSPanel
    Me.tsArtv = New NTSInformatica.NTSTabControl
    Me.NtsTabPage11 = New NTSInformatica.NTSTabPage
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
    Me.ckAr_gesubic = New NTSInformatica.NTSCheckBox
    Me.lbXx_codimba = New NTSInformatica.NTSLabel
    Me.lbXx_flmod = New NTSInformatica.NTSLabel
    Me.lbAr_flmod = New NTSInformatica.NTSLabel
    Me.edAr_flmod = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_volume = New NTSInformatica.NTSLabel
    Me.edAr_volume = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_codimba = New NTSInformatica.NTSLabel
    Me.edAr_codimba = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_misura1 = New NTSInformatica.NTSLabel
    Me.edAr_misura1 = New NTSInformatica.NTSTextBoxNum
    Me.edAr_misura2 = New NTSInformatica.NTSTextBoxNum
    Me.edAr_misura3 = New NTSInformatica.NTSTextBoxNum
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnTabpagElencoVar = New NTSInformatica.NTSPanel
    Me.cmdEspSel = New NTSInformatica.NTSButton
    Me.cmdAggiungi = New NTSInformatica.NTSButton
    Me.cmdEsplodi = New NTSInformatica.NTSButton
    Me.lbLivello3 = New NTSInformatica.NTSLabel
    Me.lbLivello2 = New NTSInformatica.NTSLabel
    Me.lbLivello1 = New NTSInformatica.NTSLabel
    Me.grVar3 = New NTSInformatica.NTSGrid
    Me.grvVar3 = New NTSInformatica.NTSGridView
    Me.xx_seleziona3 = New NTSInformatica.NTSGridColumn
    Me.xx_codvar3 = New NTSInformatica.NTSGridColumn
    Me.xx_descr3 = New NTSInformatica.NTSGridColumn
    Me.xx_codditt3 = New NTSInformatica.NTSGridColumn
    Me.grVar2 = New NTSInformatica.NTSGrid
    Me.grvVar2 = New NTSInformatica.NTSGridView
    Me.xx_seleziona2 = New NTSInformatica.NTSGridColumn
    Me.xx_codvar2 = New NTSInformatica.NTSGridColumn
    Me.xx_descr2 = New NTSInformatica.NTSGridColumn
    Me.xx_codditt2 = New NTSInformatica.NTSGridColumn
    Me.grVar1 = New NTSInformatica.NTSGrid
    Me.grvVar1 = New NTSInformatica.NTSGridView
    Me.xx_seleziona1 = New NTSInformatica.NTSGridColumn
    Me.xx_codvar1 = New NTSInformatica.NTSGridColumn
    Me.xx_descr1 = New NTSInformatica.NTSGridColumn
    Me.xx_codditt1 = New NTSInformatica.NTSGridColumn
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnTabpagAnaliticoVar = New NTSInformatica.NTSPanel
    Me.grArtico = New NTSInformatica.NTSGrid
    Me.grvArtico = New NTSInformatica.NTSGridView
    Me.ar_codvar1 = New NTSInformatica.NTSGridColumn
    Me.ar_codvar2 = New NTSInformatica.NTSGridColumn
    Me.ar_codvar3 = New NTSInformatica.NTSGridColumn
    Me.ar_descr = New NTSInformatica.NTSGridColumn
    Me.ar_desint = New NTSInformatica.NTSGridColumn
    Me.ar_scomin = New NTSInformatica.NTSGridColumn
    Me.ar_scomax = New NTSInformatica.NTSGridColumn
    Me.ar_minord = New NTSInformatica.NTSGridColumn
    Me.ar_inesaur = New NTSInformatica.NTSGridColumn
    Me.ar_note = New NTSInformatica.NTSGridColumn
    Me.ar_codart = New NTSInformatica.NTSGridColumn
    Me.ar_formula = New NTSInformatica.NTSGridColumn
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
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
    Me.NtsTabPage4 = New NTSInformatica.NTSTabPage
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
    Me.NtsTabPage5 = New NTSInformatica.NTSTabPage
    Me.pnTabpag3 = New NTSInformatica.NTSPanel
    Me.pnTabpag3Right = New NTSInformatica.NTSPanel
    Me.cmdClassifica = New NTSInformatica.NTSButton
    Me.lbXx_codvuo = New NTSInformatica.NTSLabel
    Me.lbClassifica = New NTSInformatica.NTSLabel
    Me.fmAltriDati = New NTSInformatica.NTSGroupBox
    Me.ckAr_stainv = New NTSInformatica.NTSCheckBox
    Me.ckAr_stasche = New NTSInformatica.NTSCheckBox
    Me.ckAr_geslotti = New NTSInformatica.NTSCheckBox
    Me.ckAr_inesaur = New NTSInformatica.NTSCheckBox
    Me.ckAr_pesoca = New NTSInformatica.NTSCheckBox
    Me.ckAr_stalist = New NTSInformatica.NTSCheckBox
    Me.ckAr_gestmatr = New NTSInformatica.NTSCheckBox
    Me.lbAr_contriva = New NTSInformatica.NTSLabel
    Me.lbAr_codvuo = New NTSInformatica.NTSLabel
    Me.edAr_codvuo = New NTSInformatica.NTSTextBoxNum
    Me.edAr_contriva = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_pesolor = New NTSInformatica.NTSLabel
    Me.edAr_pesolor = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_pesonet = New NTSInformatica.NTSLabel
    Me.edAr_pesonet = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_catlifo = New NTSInformatica.NTSLabel
    Me.edAr_catlifo = New NTSInformatica.NTSTextBoxNum
    Me.pnTabpag3Left = New NTSInformatica.NTSPanel
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
    Me.NtsTabPage6 = New NTSInformatica.NTSTabPage
    Me.pnTabpag4 = New NTSInformatica.NTSPanel
    Me.pnListiniBottom = New NTSInformatica.NTSPanel
    Me.ceListini = New NTSInformatica.NTSXXLIST
    Me.pnListiniTop = New NTSInformatica.NTSPanel
    Me.lbArtListini = New NTSInformatica.NTSLabel
    Me.NtsTabPage7 = New NTSInformatica.NTSTabPage
    Me.pnTabpag5 = New NTSInformatica.NTSPanel
    Me.pnScontiBottom = New NTSInformatica.NTSPanel
    Me.ceSconti = New NTSInformatica.NTSXXSCON
    Me.pnScontiTop = New NTSInformatica.NTSPanel
    Me.lbArtSconti = New NTSInformatica.NTSLabel
    Me.NtsTabPage8 = New NTSInformatica.NTSTabPage
    Me.pnTabpag6 = New NTSInformatica.NTSPanel
    Me.pnProvvigioniBottom = New NTSInformatica.NTSPanel
    Me.ceProvvig = New NTSInformatica.NTSXXPROV
    Me.pnProvvigioniTop = New NTSInformatica.NTSPanel
    Me.lbArtProvvigioni = New NTSInformatica.NTSLabel
    Me.NtsTabPage9 = New NTSInformatica.NTSTabPage
    Me.pnTabpag7 = New NTSInformatica.NTSPanel
    Me.edAr_note = New NTSInformatica.NTSMemoBox
    Me.NtsTabPage10 = New NTSInformatica.NTSTabPage
    Me.pnTabpag8 = New NTSInformatica.NTSPanel
    Me.cmdArtgif2 = New NTSInformatica.NTSButton
    Me.cmdArtGif1 = New NTSInformatica.NTSButton
    Me.cmdVisGif2 = New NTSInformatica.NTSButton
    Me.cmdVisGif1 = New NTSInformatica.NTSButton
    Me.lbAr_um4 = New NTSInformatica.NTSLabel
    Me.edAr_um4 = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_formula = New NTSInformatica.NTSLabel
    Me.edAr_formula = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_umpdapr = New NTSInformatica.NTSLabel
    Me.cbAr_umpdapr = New NTSInformatica.NTSComboBox
    Me.lbAr_umpdapra = New NTSInformatica.NTSLabel
    Me.cbAr_umpdapra = New NTSInformatica.NTSComboBox
    Me.lbAr_gif1 = New NTSInformatica.NTSLabel
    Me.edAr_gif1 = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_gif2 = New NTSInformatica.NTSLabel
    Me.edAr_gif2 = New NTSInformatica.NTSTextBoxStr
    Me.lbAr_umdapra = New NTSInformatica.NTSLabel
    Me.cbAr_umdapra = New NTSInformatica.NTSComboBox
    Me.lbXx_magstock = New NTSInformatica.NTSLabel
    Me.lbXx_magprod = New NTSInformatica.NTSLabel
    Me.lbAr_umdapr = New NTSInformatica.NTSLabel
    Me.cbAr_umdapr = New NTSInformatica.NTSComboBox
    Me.lbAr_magstock = New NTSInformatica.NTSLabel
    Me.edAr_magstock = New NTSInformatica.NTSTextBoxNum
    Me.lbAr_magprod = New NTSInformatica.NTSLabel
    Me.edAr_magprod = New NTSInformatica.NTSTextBoxNum
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.pnTopLeft = New NTSInformatica.NTSPanel
    Me.pnTopLeftBut = New NTSInformatica.NTSPanel
    Me.cmdCodarfo = New NTSInformatica.NTSButton
    Me.cmdProgressivi = New NTSInformatica.NTSButton
    Me.cmdValuta = New NTSInformatica.NTSButton
    Me.cmdProgtot = New NTSInformatica.NTSButton
    Me.lbAr_codart = New NTSInformatica.NTSLabel
    Me.edAr_desint = New NTSInformatica.NTSTextBoxStr
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
    Me.edFocus = New NTSInformatica.NTSTextBoxStr
    Me.fmLogisticaPalmare = New NTSInformatica.NTSGroupBox
    Me.lbAr_converp = New NTSInformatica.NTSLabel
    Me.edAr_converp = New NTSInformatica.NTSTextBoxNum
    Me.ckAr_staetip = New NTSInformatica.NTSCheckBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnArtv, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnArtv.SuspendLayout()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMain.SuspendLayout()
    CType(Me.tsArtv, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsArtv.SuspendLayout()
    Me.NtsTabPage11.SuspendLayout()
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
    CType(Me.ckAr_gesubic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_flmod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_volume.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codimba.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_misura1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_misura2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_misura3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnTabpagElencoVar, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpagElencoVar.SuspendLayout()
    CType(Me.grVar3, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvVar3, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grVar2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvVar2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grVar1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvVar1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnTabpagAnaliticoVar, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpagAnaliticoVar.SuspendLayout()
    CType(Me.grArtico, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvArtico, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
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
    Me.NtsTabPage4.SuspendLayout()
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
    Me.NtsTabPage5.SuspendLayout()
    CType(Me.pnTabpag3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag3.SuspendLayout()
    CType(Me.pnTabpag3Right, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag3Right.SuspendLayout()
    CType(Me.fmAltriDati, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAltriDati.SuspendLayout()
    CType(Me.ckAr_stainv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_stasche.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_geslotti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_inesaur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_pesoca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_stalist.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_gestmatr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_codvuo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_contriva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsTabPage6.SuspendLayout()
    CType(Me.pnTabpag4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag4.SuspendLayout()
    CType(Me.pnListiniBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnListiniBottom.SuspendLayout()
    CType(Me.pnListiniTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnListiniTop.SuspendLayout()
    Me.NtsTabPage7.SuspendLayout()
    CType(Me.pnTabpag5, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag5.SuspendLayout()
    CType(Me.pnScontiBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnScontiBottom.SuspendLayout()
    CType(Me.pnScontiTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnScontiTop.SuspendLayout()
    Me.NtsTabPage8.SuspendLayout()
    CType(Me.pnTabpag6, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag6.SuspendLayout()
    CType(Me.pnProvvigioniBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnProvvigioniBottom.SuspendLayout()
    CType(Me.pnProvvigioniTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnProvvigioniTop.SuspendLayout()
    Me.NtsTabPage9.SuspendLayout()
    CType(Me.pnTabpag7, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag7.SuspendLayout()
    CType(Me.edAr_note.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage10.SuspendLayout()
    CType(Me.pnTabpag8, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTabpag8.SuspendLayout()
    CType(Me.edAr_um4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_formula.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_umpdapr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_umpdapra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_gif1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_gif2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_umdapra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAr_umdapr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_magstock.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAr_magprod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.pnTopLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTopLeft.SuspendLayout()
    CType(Me.pnTopLeftBut, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTopLeftBut.SuspendLayout()
    CType(Me.edAr_desint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    CType(Me.fmLogisticaPalmare, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmLogisticaPalmare.SuspendLayout()
    CType(Me.edAr_converp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAr_staetip.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbRecordNuovo, Me.tlbRecordAggiorna, Me.tlbRecordRipristina, Me.tlbGuida, Me.tlbEsci, Me.tlbRecordCancella, Me.tlbStrumenti, Me.tlbApri, Me.tlbBarcode, Me.tlbKit, Me.tlbOle, Me.tlbStampaEtichette, Me.tlbRicalcolaListini, Me.tlbArticoliMagazzino, Me.tlbDuplica, Me.tlbLingua, Me.tlbAccessoriSuccedanei, Me.tlbEstensioni, Me.tlbSimula})
    Me.NtsBarManager1.MaxItemId = 49
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbDuplica), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbApri), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordNuovo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordAggiorna), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRecordCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbBarcode, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbKit), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbOle), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbDuplica.Id = 44
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
    'tlbRecordNuovo
    '
    Me.tlbRecordNuovo.Caption = "RecordNuovo"
    Me.tlbRecordNuovo.Glyph = CType(resources.GetObject("tlbRecordNuovo.Glyph"), System.Drawing.Image)
    Me.tlbRecordNuovo.GlyphPath = ""
    Me.tlbRecordNuovo.Id = 5
    Me.tlbRecordNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F2))
    Me.tlbRecordNuovo.Name = "tlbRecordNuovo"
    Me.tlbRecordNuovo.Visible = True
    '
    'tlbRecordAggiorna
    '
    Me.tlbRecordAggiorna.Caption = "RecordAggiorna"
    Me.tlbRecordAggiorna.Glyph = CType(resources.GetObject("tlbRecordAggiorna.Glyph"), System.Drawing.Image)
    Me.tlbRecordAggiorna.GlyphPath = ""
    Me.tlbRecordAggiorna.Id = 6
    Me.tlbRecordAggiorna.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F9))
    Me.tlbRecordAggiorna.Name = "tlbRecordAggiorna"
    Me.tlbRecordAggiorna.Visible = True
    '
    'tlbRecordRipristina
    '
    Me.tlbRecordRipristina.Caption = "RecordRipristina"
    Me.tlbRecordRipristina.Glyph = CType(resources.GetObject("tlbRecordRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRecordRipristina.GlyphPath = ""
    Me.tlbRecordRipristina.Id = 7
    Me.tlbRecordRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F8))
    Me.tlbRecordRipristina.Name = "tlbRecordRipristina"
    Me.tlbRecordRipristina.Visible = True
    '
    'tlbRecordCancella
    '
    Me.tlbRecordCancella.Caption = "RecordCancella"
    Me.tlbRecordCancella.Glyph = CType(resources.GetObject("tlbRecordCancella.Glyph"), System.Drawing.Image)
    Me.tlbRecordCancella.GlyphPath = ""
    Me.tlbRecordCancella.Id = 20
    Me.tlbRecordCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F4))
    Me.tlbRecordCancella.Name = "tlbRecordCancella"
    Me.tlbRecordCancella.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 22
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaEtichette), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRicalcolaListini), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbArticoliMagazzino), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbAccessoriSuccedanei), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbLingua, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEstensioni, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSimula, True)})
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
    'tlbAccessoriSuccedanei
    '
    Me.tlbAccessoriSuccedanei.Caption = "Accessori/succedanei"
    Me.tlbAccessoriSuccedanei.GlyphPath = ""
    Me.tlbAccessoriSuccedanei.Id = 46
    Me.tlbAccessoriSuccedanei.Name = "tlbAccessoriSuccedanei"
    Me.tlbAccessoriSuccedanei.NTSIsCheckBox = False
    Me.tlbAccessoriSuccedanei.Visible = True
    '
    'tlbLingua
    '
    Me.tlbLingua.Caption = "Descr. in lingua articolo modello"
    Me.tlbLingua.GlyphPath = ""
    Me.tlbLingua.Id = 45
    Me.tlbLingua.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L))
    Me.tlbLingua.Name = "tlbLingua"
    Me.tlbLingua.NTSIsCheckBox = False
    Me.tlbLingua.Visible = True
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
    Me.tlbSimula.Caption = "Simulazione vendite"
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
    'pnArtv
    '
    Me.pnArtv.AllowDrop = True
    Me.pnArtv.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnArtv.Appearance.Options.UseBackColor = True
    Me.pnArtv.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnArtv.Controls.Add(Me.pnMain)
    Me.pnArtv.Controls.Add(Me.pnTop)
    Me.pnArtv.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnArtv.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnArtv.Location = New System.Drawing.Point(0, 30)
    Me.pnArtv.Name = "pnArtv"
    Me.pnArtv.NTSActiveTrasparency = True
    Me.pnArtv.Size = New System.Drawing.Size(799, 476)
    Me.pnArtv.TabIndex = 503
    Me.pnArtv.Text = "NtsPanel1"
    '
    'pnMain
    '
    Me.pnMain.AllowDrop = True
    Me.pnMain.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnMain.Appearance.Options.UseBackColor = True
    Me.pnMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnMain.Controls.Add(Me.tsArtv)
    Me.pnMain.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMain.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnMain.Location = New System.Drawing.Point(0, 125)
    Me.pnMain.Name = "pnMain"
    Me.pnMain.NTSActiveTrasparency = True
    Me.pnMain.Size = New System.Drawing.Size(799, 351)
    Me.pnMain.TabIndex = 576
    Me.pnMain.Text = "NtsPanel2"
    '
    'tsArtv
    '
    Me.tsArtv.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsArtv.Location = New System.Drawing.Point(0, 0)
    Me.tsArtv.Name = "tsArtv"
    Me.tsArtv.SelectedTabPage = Me.NtsTabPage10
    Me.tsArtv.Size = New System.Drawing.Size(799, 351)
    Me.tsArtv.TabIndex = 582
    Me.tsArtv.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage3, Me.NtsTabPage4, Me.NtsTabPage5, Me.NtsTabPage6, Me.NtsTabPage7, Me.NtsTabPage8, Me.NtsTabPage9, Me.NtsTabPage10, Me.NtsTabPage11})
    Me.tsArtv.Text = "NtsTabControl1"
    '
    'NtsTabPage11
    '
    Me.NtsTabPage11.AllowDrop = True
    Me.NtsTabPage11.Controls.Add(Me.pnTabpag9)
    Me.NtsTabPage11.Enable = True
    Me.NtsTabPage11.Name = "NtsTabPage11"
    Me.NtsTabPage11.Size = New System.Drawing.Size(790, 321)
    Me.NtsTabPage11.Text = "&11 - Dati agg."
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
    Me.pnTabpag9.Controls.Add(Me.lbXx_flmod)
    Me.pnTabpag9.Controls.Add(Me.lbAr_flmod)
    Me.pnTabpag9.Controls.Add(Me.edAr_flmod)
    Me.pnTabpag9.Controls.Add(Me.lbAr_volume)
    Me.pnTabpag9.Controls.Add(Me.edAr_volume)
    Me.pnTabpag9.Controls.Add(Me.lbAr_codimba)
    Me.pnTabpag9.Controls.Add(Me.edAr_codimba)
    Me.pnTabpag9.Controls.Add(Me.lbAr_misura1)
    Me.pnTabpag9.Controls.Add(Me.edAr_misura1)
    Me.pnTabpag9.Controls.Add(Me.edAr_misura2)
    Me.pnTabpag9.Controls.Add(Me.edAr_misura3)
    Me.pnTabpag9.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag9.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag9.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag9.Name = "pnTabpag9"
    Me.pnTabpag9.NTSActiveTrasparency = True
    Me.pnTabpag9.Size = New System.Drawing.Size(790, 321)
    Me.pnTabpag9.TabIndex = 0
    Me.pnTabpag9.Text = "NtsPanel1"
    '
    'lbAr_deterior
    '
    Me.lbAr_deterior.AutoSize = True
    Me.lbAr_deterior.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_deterior.Location = New System.Drawing.Point(9, 184)
    Me.lbAr_deterior.Name = "lbAr_deterior"
    Me.lbAr_deterior.NTSDbField = ""
    Me.lbAr_deterior.Size = New System.Drawing.Size(106, 13)
    Me.lbAr_deterior.TabIndex = 674
    Me.lbAr_deterior.Text = "Articolo deteriorabile"
    Me.lbAr_deterior.Tooltip = ""
    Me.lbAr_deterior.UseMnemonic = False
    '
    'cbAr_deterior
    '
    Me.cbAr_deterior.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_deterior.DataSource = Nothing
    Me.cbAr_deterior.DisplayMember = ""
    Me.cbAr_deterior.Location = New System.Drawing.Point(134, 181)
    Me.cbAr_deterior.Name = "cbAr_deterior"
    Me.cbAr_deterior.NTSDbField = ""
    Me.cbAr_deterior.Properties.AutoHeight = False
    Me.cbAr_deterior.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_deterior.Properties.DropDownRows = 30
    Me.cbAr_deterior.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_deterior.SelectedValue = ""
    Me.cbAr_deterior.Size = New System.Drawing.Size(206, 20)
    Me.cbAr_deterior.TabIndex = 675
    Me.cbAr_deterior.ValueMember = ""
    '
    'edAr_codtlox
    '
    Me.edAr_codtlox.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edAr_codtlox.EditValue = "0"
    Me.edAr_codtlox.Location = New System.Drawing.Point(134, 155)
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
    Me.edAr_codtlox.TabIndex = 673
    '
    'cbAr_tipscarlotx
    '
    Me.cbAr_tipscarlotx.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_tipscarlotx.DataSource = Nothing
    Me.cbAr_tipscarlotx.DisplayMember = ""
    Me.cbAr_tipscarlotx.Location = New System.Drawing.Point(651, 153)
    Me.cbAr_tipscarlotx.Name = "cbAr_tipscarlotx"
    Me.cbAr_tipscarlotx.NTSDbField = ""
    Me.cbAr_tipscarlotx.Properties.AutoHeight = False
    Me.cbAr_tipscarlotx.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_tipscarlotx.Properties.DropDownRows = 30
    Me.cbAr_tipscarlotx.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_tipscarlotx.SelectedValue = ""
    Me.cbAr_tipscarlotx.Size = New System.Drawing.Size(90, 20)
    Me.cbAr_tipscarlotx.TabIndex = 672
    Me.cbAr_tipscarlotx.ValueMember = ""
    '
    'lbAr_tipscarlotx
    '
    Me.lbAr_tipscarlotx.AutoSize = True
    Me.lbAr_tipscarlotx.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_tipscarlotx.Location = New System.Drawing.Point(460, 158)
    Me.lbAr_tipscarlotx.Name = "lbAr_tipscarlotx"
    Me.lbAr_tipscarlotx.NTSDbField = ""
    Me.lbAr_tipscarlotx.Size = New System.Drawing.Size(166, 13)
    Me.lbAr_tipscarlotx.TabIndex = 671
    Me.lbAr_tipscarlotx.Text = "Sistema attribuzione autom. lotto"
    Me.lbAr_tipscarlotx.Tooltip = ""
    Me.lbAr_tipscarlotx.UseMnemonic = False
    '
    'lbXx_codtlox
    '
    Me.lbXx_codtlox.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codtlox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codtlox.Location = New System.Drawing.Point(206, 155)
    Me.lbXx_codtlox.Name = "lbXx_codtlox"
    Me.lbXx_codtlox.NTSDbField = ""
    Me.lbXx_codtlox.Size = New System.Drawing.Size(206, 20)
    Me.lbXx_codtlox.TabIndex = 670
    Me.lbXx_codtlox.Tooltip = ""
    Me.lbXx_codtlox.UseMnemonic = False
    '
    'lbAr_codtlox
    '
    Me.lbAr_codtlox.AutoSize = True
    Me.lbAr_codtlox.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codtlox.Location = New System.Drawing.Point(9, 159)
    Me.lbAr_codtlox.Name = "lbAr_codtlox"
    Me.lbAr_codtlox.NTSDbField = ""
    Me.lbAr_codtlox.Size = New System.Drawing.Size(105, 13)
    Me.lbAr_codtlox.TabIndex = 669
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
    Me.fmCadc.Location = New System.Drawing.Point(454, 15)
    Me.fmCadc.Name = "fmCadc"
    Me.fmCadc.Size = New System.Drawing.Size(330, 90)
    Me.fmCadc.TabIndex = 663
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
    Me.lbXx_codtcdc.Size = New System.Drawing.Size(128, 20)
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
    Me.lbXx_coddicv.Size = New System.Drawing.Size(128, 20)
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
    Me.lbXx_coddica.Size = New System.Drawing.Size(128, 20)
    Me.lbXx_coddica.TabIndex = 656
    Me.lbXx_coddica.Tooltip = ""
    Me.lbXx_coddica.UseMnemonic = False
    '
    'fmUbiFasi
    '
    Me.fmUbiFasi.AllowDrop = True
    Me.fmUbiFasi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmUbiFasi.Appearance.Options.UseBackColor = True
    Me.fmUbiFasi.Controls.Add(Me.ckAr_gesubic)
    Me.fmUbiFasi.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmUbiFasi.Location = New System.Drawing.Point(12, 96)
    Me.fmUbiFasi.Name = "fmUbiFasi"
    Me.fmUbiFasi.Size = New System.Drawing.Size(125, 46)
    Me.fmUbiFasi.TabIndex = 631
    '
    'ckAr_gesubic
    '
    Me.ckAr_gesubic.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_gesubic.Location = New System.Drawing.Point(5, 25)
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
    'lbXx_codimba
    '
    Me.lbXx_codimba.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codimba.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codimba.Location = New System.Drawing.Point(172, 15)
    Me.lbXx_codimba.Name = "lbXx_codimba"
    Me.lbXx_codimba.NTSDbField = ""
    Me.lbXx_codimba.Size = New System.Drawing.Size(240, 20)
    Me.lbXx_codimba.TabIndex = 628
    Me.lbXx_codimba.Tooltip = ""
    Me.lbXx_codimba.UseMnemonic = False
    '
    'lbXx_flmod
    '
    Me.lbXx_flmod.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_flmod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_flmod.Location = New System.Drawing.Point(172, 67)
    Me.lbXx_flmod.Name = "lbXx_flmod"
    Me.lbXx_flmod.NTSDbField = ""
    Me.lbXx_flmod.Size = New System.Drawing.Size(240, 20)
    Me.lbXx_flmod.TabIndex = 627
    Me.lbXx_flmod.Tooltip = ""
    Me.lbXx_flmod.UseMnemonic = False
    Me.lbXx_flmod.Visible = False
    '
    'lbAr_flmod
    '
    Me.lbAr_flmod.AutoSize = True
    Me.lbAr_flmod.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_flmod.Location = New System.Drawing.Point(9, 69)
    Me.lbAr_flmod.Name = "lbAr_flmod"
    Me.lbAr_flmod.NTSDbField = ""
    Me.lbAr_flmod.Size = New System.Drawing.Size(82, 13)
    Me.lbAr_flmod.TabIndex = 610
    Me.lbAr_flmod.Text = "Cod. variante 1"
    Me.lbAr_flmod.Tooltip = ""
    Me.lbAr_flmod.UseMnemonic = False
    Me.lbAr_flmod.Visible = False
    '
    'edAr_flmod
    '
    Me.edAr_flmod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_flmod.EditValue = ""
    Me.edAr_flmod.Location = New System.Drawing.Point(100, 67)
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
    Me.edAr_flmod.Size = New System.Drawing.Size(66, 20)
    Me.edAr_flmod.TabIndex = 611
    Me.edAr_flmod.Visible = False
    '
    'lbAr_volume
    '
    Me.lbAr_volume.AutoSize = True
    Me.lbAr_volume.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_volume.Location = New System.Drawing.Point(248, 99)
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
    Me.edAr_volume.Location = New System.Drawing.Point(312, 96)
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
    Me.lbAr_codimba.Location = New System.Drawing.Point(9, 18)
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
    Me.edAr_codimba.Location = New System.Drawing.Point(100, 15)
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
    Me.lbAr_misura1.Location = New System.Drawing.Point(9, 44)
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
    Me.edAr_misura1.Location = New System.Drawing.Point(100, 41)
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
    Me.edAr_misura2.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAr_misura2.EditValue = "0"
    Me.edAr_misura2.Location = New System.Drawing.Point(206, 41)
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
    Me.edAr_misura3.Location = New System.Drawing.Point(312, 41)
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
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnTabpagElencoVar)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(790, 321)
    Me.NtsTabPage1.Text = "&1 - Elenco varianti"
    '
    'pnTabpagElencoVar
    '
    Me.pnTabpagElencoVar.AllowDrop = True
    Me.pnTabpagElencoVar.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpagElencoVar.Appearance.Options.UseBackColor = True
    Me.pnTabpagElencoVar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpagElencoVar.Controls.Add(Me.cmdEspSel)
    Me.pnTabpagElencoVar.Controls.Add(Me.cmdAggiungi)
    Me.pnTabpagElencoVar.Controls.Add(Me.cmdEsplodi)
    Me.pnTabpagElencoVar.Controls.Add(Me.lbLivello3)
    Me.pnTabpagElencoVar.Controls.Add(Me.lbLivello2)
    Me.pnTabpagElencoVar.Controls.Add(Me.lbLivello1)
    Me.pnTabpagElencoVar.Controls.Add(Me.grVar3)
    Me.pnTabpagElencoVar.Controls.Add(Me.grVar2)
    Me.pnTabpagElencoVar.Controls.Add(Me.grVar1)
    Me.pnTabpagElencoVar.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpagElencoVar.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpagElencoVar.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpagElencoVar.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpagElencoVar.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpagElencoVar.Name = "pnTabpagElencoVar"
    Me.pnTabpagElencoVar.NTSActiveTrasparency = True
    Me.pnTabpagElencoVar.Size = New System.Drawing.Size(790, 321)
    Me.pnTabpagElencoVar.TabIndex = 0
    Me.pnTabpagElencoVar.Text = "NtsPanel1"
    '
    'cmdEspSel
    '
    Me.cmdEspSel.ImagePath = ""
    Me.cmdEspSel.ImageText = ""
    Me.cmdEspSel.Location = New System.Drawing.Point(616, 290)
    Me.cmdEspSel.Name = "cmdEspSel"
    Me.cmdEspSel.NTSContextMenu = Nothing
    Me.cmdEspSel.Size = New System.Drawing.Size(152, 25)
    Me.cmdEspSel.TabIndex = 8
    Me.cmdEspSel.Text = "Esplodi sele&zione"
    '
    'cmdAggiungi
    '
    Me.cmdAggiungi.ImagePath = ""
    Me.cmdAggiungi.ImageText = ""
    Me.cmdAggiungi.Location = New System.Drawing.Point(691, 5)
    Me.cmdAggiungi.Name = "cmdAggiungi"
    Me.cmdAggiungi.NTSContextMenu = Nothing
    Me.cmdAggiungi.Size = New System.Drawing.Size(77, 25)
    Me.cmdAggiungi.TabIndex = 7
    Me.cmdAggiungi.Text = "Aggiungi"
    '
    'cmdEsplodi
    '
    Me.cmdEsplodi.ImagePath = ""
    Me.cmdEsplodi.ImageText = ""
    Me.cmdEsplodi.Location = New System.Drawing.Point(608, 5)
    Me.cmdEsplodi.Name = "cmdEsplodi"
    Me.cmdEsplodi.NTSContextMenu = Nothing
    Me.cmdEsplodi.Size = New System.Drawing.Size(77, 25)
    Me.cmdEsplodi.TabIndex = 6
    Me.cmdEsplodi.Text = "Esplod&i"
    '
    'lbLivello3
    '
    Me.lbLivello3.AutoSize = True
    Me.lbLivello3.BackColor = System.Drawing.Color.Transparent
    Me.lbLivello3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbLivello3.Location = New System.Drawing.Point(524, 13)
    Me.lbLivello3.Name = "lbLivello3"
    Me.lbLivello3.NTSDbField = ""
    Me.lbLivello3.Size = New System.Drawing.Size(56, 13)
    Me.lbLivello3.TabIndex = 5
    Me.lbLivello3.Text = "3° livello"
    Me.lbLivello3.Tooltip = ""
    Me.lbLivello3.UseMnemonic = False
    '
    'lbLivello2
    '
    Me.lbLivello2.AutoSize = True
    Me.lbLivello2.BackColor = System.Drawing.Color.Transparent
    Me.lbLivello2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbLivello2.Location = New System.Drawing.Point(268, 13)
    Me.lbLivello2.Name = "lbLivello2"
    Me.lbLivello2.NTSDbField = ""
    Me.lbLivello2.Size = New System.Drawing.Size(56, 13)
    Me.lbLivello2.TabIndex = 4
    Me.lbLivello2.Text = "2° livello"
    Me.lbLivello2.Tooltip = ""
    Me.lbLivello2.UseMnemonic = False
    '
    'lbLivello1
    '
    Me.lbLivello1.AutoSize = True
    Me.lbLivello1.BackColor = System.Drawing.Color.Transparent
    Me.lbLivello1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbLivello1.Location = New System.Drawing.Point(6, 13)
    Me.lbLivello1.Name = "lbLivello1"
    Me.lbLivello1.NTSDbField = ""
    Me.lbLivello1.Size = New System.Drawing.Size(56, 13)
    Me.lbLivello1.TabIndex = 3
    Me.lbLivello1.Text = "1° livello"
    Me.lbLivello1.Tooltip = ""
    Me.lbLivello1.UseMnemonic = False
    '
    'grVar3
    '
    Me.grVar3.EmbeddedNavigator.Name = ""
    Me.grVar3.Location = New System.Drawing.Point(518, 34)
    Me.grVar3.MainView = Me.grvVar3
    Me.grVar3.Name = "grVar3"
    Me.grVar3.Size = New System.Drawing.Size(250, 253)
    Me.grVar3.TabIndex = 2
    Me.grVar3.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvVar3})
    '
    'grvVar3
    '
    Me.grvVar3.ActiveFilterEnabled = False
    Me.grvVar3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_seleziona3, Me.xx_codvar3, Me.xx_descr3, Me.xx_codditt3})
    Me.grvVar3.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvVar3.Enabled = True
    Me.grvVar3.GridControl = Me.grVar3
    Me.grvVar3.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvVar3.MinRowHeight = 14
    Me.grvVar3.Name = "grvVar3"
    Me.grvVar3.NTSAllowDelete = True
    Me.grvVar3.NTSAllowInsert = True
    Me.grvVar3.NTSAllowUpdate = True
    Me.grvVar3.NTSMenuContext = Nothing
    Me.grvVar3.OptionsCustomization.AllowRowSizing = True
    Me.grvVar3.OptionsFilter.AllowFilterEditor = False
    Me.grvVar3.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvVar3.OptionsNavigation.UseTabKey = False
    Me.grvVar3.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvVar3.OptionsView.ColumnAutoWidth = False
    Me.grvVar3.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvVar3.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvVar3.OptionsView.ShowGroupPanel = False
    Me.grvVar3.RowHeight = 14
    '
    'xx_seleziona3
    '
    Me.xx_seleziona3.AppearanceCell.Options.UseBackColor = True
    Me.xx_seleziona3.AppearanceCell.Options.UseTextOptions = True
    Me.xx_seleziona3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_seleziona3.Caption = "Seleziona"
    Me.xx_seleziona3.Enabled = True
    Me.xx_seleziona3.FieldName = "xx_seleziona3"
    Me.xx_seleziona3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_seleziona3.Name = "xx_seleziona3"
    Me.xx_seleziona3.NTSRepositoryComboBox = Nothing
    Me.xx_seleziona3.NTSRepositoryItemCheck = Nothing
    Me.xx_seleziona3.NTSRepositoryItemMemo = Nothing
    Me.xx_seleziona3.NTSRepositoryItemText = Nothing
    Me.xx_seleziona3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_seleziona3.OptionsFilter.AllowFilter = False
    Me.xx_seleziona3.Visible = True
    Me.xx_seleziona3.VisibleIndex = 0
    '
    'xx_codvar3
    '
    Me.xx_codvar3.AppearanceCell.Options.UseBackColor = True
    Me.xx_codvar3.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codvar3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codvar3.Caption = "Cod."
    Me.xx_codvar3.Enabled = True
    Me.xx_codvar3.FieldName = "xx_codvar3"
    Me.xx_codvar3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codvar3.Name = "xx_codvar3"
    Me.xx_codvar3.NTSRepositoryComboBox = Nothing
    Me.xx_codvar3.NTSRepositoryItemCheck = Nothing
    Me.xx_codvar3.NTSRepositoryItemMemo = Nothing
    Me.xx_codvar3.NTSRepositoryItemText = Nothing
    Me.xx_codvar3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codvar3.OptionsFilter.AllowFilter = False
    Me.xx_codvar3.Visible = True
    Me.xx_codvar3.VisibleIndex = 1
    '
    'xx_descr3
    '
    Me.xx_descr3.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr3.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr3.Caption = "Descr."
    Me.xx_descr3.Enabled = True
    Me.xx_descr3.FieldName = "xx_descr3"
    Me.xx_descr3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr3.Name = "xx_descr3"
    Me.xx_descr3.NTSRepositoryComboBox = Nothing
    Me.xx_descr3.NTSRepositoryItemCheck = Nothing
    Me.xx_descr3.NTSRepositoryItemMemo = Nothing
    Me.xx_descr3.NTSRepositoryItemText = Nothing
    Me.xx_descr3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr3.OptionsFilter.AllowFilter = False
    Me.xx_descr3.Visible = True
    Me.xx_descr3.VisibleIndex = 2
    '
    'xx_codditt3
    '
    Me.xx_codditt3.AppearanceCell.Options.UseBackColor = True
    Me.xx_codditt3.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codditt3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codditt3.Caption = "Codice ditta"
    Me.xx_codditt3.Enabled = False
    Me.xx_codditt3.FieldName = "xx_codditt3"
    Me.xx_codditt3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codditt3.Name = "xx_codditt3"
    Me.xx_codditt3.NTSRepositoryComboBox = Nothing
    Me.xx_codditt3.NTSRepositoryItemCheck = Nothing
    Me.xx_codditt3.NTSRepositoryItemMemo = Nothing
    Me.xx_codditt3.NTSRepositoryItemText = Nothing
    Me.xx_codditt3.OptionsColumn.AllowEdit = False
    Me.xx_codditt3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codditt3.OptionsColumn.ReadOnly = True
    Me.xx_codditt3.OptionsFilter.AllowFilter = False
    '
    'grVar2
    '
    Me.grVar2.EmbeddedNavigator.Name = ""
    Me.grVar2.Location = New System.Drawing.Point(262, 34)
    Me.grVar2.MainView = Me.grvVar2
    Me.grVar2.Name = "grVar2"
    Me.grVar2.Size = New System.Drawing.Size(250, 253)
    Me.grVar2.TabIndex = 1
    Me.grVar2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvVar2})
    '
    'grvVar2
    '
    Me.grvVar2.ActiveFilterEnabled = False
    Me.grvVar2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_seleziona2, Me.xx_codvar2, Me.xx_descr2, Me.xx_codditt2})
    Me.grvVar2.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvVar2.Enabled = True
    Me.grvVar2.GridControl = Me.grVar2
    Me.grvVar2.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvVar2.MinRowHeight = 14
    Me.grvVar2.Name = "grvVar2"
    Me.grvVar2.NTSAllowDelete = True
    Me.grvVar2.NTSAllowInsert = True
    Me.grvVar2.NTSAllowUpdate = True
    Me.grvVar2.NTSMenuContext = Nothing
    Me.grvVar2.OptionsCustomization.AllowRowSizing = True
    Me.grvVar2.OptionsFilter.AllowFilterEditor = False
    Me.grvVar2.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvVar2.OptionsNavigation.UseTabKey = False
    Me.grvVar2.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvVar2.OptionsView.ColumnAutoWidth = False
    Me.grvVar2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvVar2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvVar2.OptionsView.ShowGroupPanel = False
    Me.grvVar2.RowHeight = 14
    '
    'xx_seleziona2
    '
    Me.xx_seleziona2.AppearanceCell.Options.UseBackColor = True
    Me.xx_seleziona2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_seleziona2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_seleziona2.Caption = "Seleziona"
    Me.xx_seleziona2.Enabled = True
    Me.xx_seleziona2.FieldName = "xx_seleziona2"
    Me.xx_seleziona2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_seleziona2.Name = "xx_seleziona2"
    Me.xx_seleziona2.NTSRepositoryComboBox = Nothing
    Me.xx_seleziona2.NTSRepositoryItemCheck = Nothing
    Me.xx_seleziona2.NTSRepositoryItemMemo = Nothing
    Me.xx_seleziona2.NTSRepositoryItemText = Nothing
    Me.xx_seleziona2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_seleziona2.OptionsFilter.AllowFilter = False
    Me.xx_seleziona2.Visible = True
    Me.xx_seleziona2.VisibleIndex = 0
    '
    'xx_codvar2
    '
    Me.xx_codvar2.AppearanceCell.Options.UseBackColor = True
    Me.xx_codvar2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codvar2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codvar2.Caption = "Cod."
    Me.xx_codvar2.Enabled = True
    Me.xx_codvar2.FieldName = "xx_codvar2"
    Me.xx_codvar2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codvar2.Name = "xx_codvar2"
    Me.xx_codvar2.NTSRepositoryComboBox = Nothing
    Me.xx_codvar2.NTSRepositoryItemCheck = Nothing
    Me.xx_codvar2.NTSRepositoryItemMemo = Nothing
    Me.xx_codvar2.NTSRepositoryItemText = Nothing
    Me.xx_codvar2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codvar2.OptionsFilter.AllowFilter = False
    Me.xx_codvar2.Visible = True
    Me.xx_codvar2.VisibleIndex = 1
    '
    'xx_descr2
    '
    Me.xx_descr2.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr2.Caption = "Descr."
    Me.xx_descr2.Enabled = True
    Me.xx_descr2.FieldName = "xx_descr2"
    Me.xx_descr2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr2.Name = "xx_descr2"
    Me.xx_descr2.NTSRepositoryComboBox = Nothing
    Me.xx_descr2.NTSRepositoryItemCheck = Nothing
    Me.xx_descr2.NTSRepositoryItemMemo = Nothing
    Me.xx_descr2.NTSRepositoryItemText = Nothing
    Me.xx_descr2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr2.OptionsFilter.AllowFilter = False
    Me.xx_descr2.Visible = True
    Me.xx_descr2.VisibleIndex = 2
    '
    'xx_codditt2
    '
    Me.xx_codditt2.AppearanceCell.Options.UseBackColor = True
    Me.xx_codditt2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codditt2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codditt2.Caption = "Codice ditta"
    Me.xx_codditt2.Enabled = False
    Me.xx_codditt2.FieldName = "xx_codditt2"
    Me.xx_codditt2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codditt2.Name = "xx_codditt2"
    Me.xx_codditt2.NTSRepositoryComboBox = Nothing
    Me.xx_codditt2.NTSRepositoryItemCheck = Nothing
    Me.xx_codditt2.NTSRepositoryItemMemo = Nothing
    Me.xx_codditt2.NTSRepositoryItemText = Nothing
    Me.xx_codditt2.OptionsColumn.AllowEdit = False
    Me.xx_codditt2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codditt2.OptionsColumn.ReadOnly = True
    Me.xx_codditt2.OptionsFilter.AllowFilter = False
    '
    'grVar1
    '
    Me.grVar1.EmbeddedNavigator.Name = ""
    Me.grVar1.Location = New System.Drawing.Point(6, 34)
    Me.grVar1.MainView = Me.grvVar1
    Me.grVar1.Name = "grVar1"
    Me.grVar1.Size = New System.Drawing.Size(250, 253)
    Me.grVar1.TabIndex = 0
    Me.grVar1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvVar1})
    '
    'grvVar1
    '
    Me.grvVar1.ActiveFilterEnabled = False
    Me.grvVar1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_seleziona1, Me.xx_codvar1, Me.xx_descr1, Me.xx_codditt1})
    Me.grvVar1.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvVar1.Enabled = True
    Me.grvVar1.GridControl = Me.grVar1
    Me.grvVar1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvVar1.MinRowHeight = 14
    Me.grvVar1.Name = "grvVar1"
    Me.grvVar1.NTSAllowDelete = True
    Me.grvVar1.NTSAllowInsert = True
    Me.grvVar1.NTSAllowUpdate = True
    Me.grvVar1.NTSMenuContext = Nothing
    Me.grvVar1.OptionsCustomization.AllowRowSizing = True
    Me.grvVar1.OptionsFilter.AllowFilterEditor = False
    Me.grvVar1.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvVar1.OptionsNavigation.UseTabKey = False
    Me.grvVar1.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvVar1.OptionsView.ColumnAutoWidth = False
    Me.grvVar1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvVar1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvVar1.OptionsView.ShowGroupPanel = False
    Me.grvVar1.RowHeight = 14
    '
    'xx_seleziona1
    '
    Me.xx_seleziona1.AppearanceCell.Options.UseBackColor = True
    Me.xx_seleziona1.AppearanceCell.Options.UseTextOptions = True
    Me.xx_seleziona1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_seleziona1.Caption = "Seleziona"
    Me.xx_seleziona1.Enabled = True
    Me.xx_seleziona1.FieldName = "xx_seleziona1"
    Me.xx_seleziona1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_seleziona1.Name = "xx_seleziona1"
    Me.xx_seleziona1.NTSRepositoryComboBox = Nothing
    Me.xx_seleziona1.NTSRepositoryItemCheck = Nothing
    Me.xx_seleziona1.NTSRepositoryItemMemo = Nothing
    Me.xx_seleziona1.NTSRepositoryItemText = Nothing
    Me.xx_seleziona1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_seleziona1.OptionsFilter.AllowFilter = False
    Me.xx_seleziona1.Visible = True
    Me.xx_seleziona1.VisibleIndex = 0
    '
    'xx_codvar1
    '
    Me.xx_codvar1.AppearanceCell.Options.UseBackColor = True
    Me.xx_codvar1.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codvar1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codvar1.Caption = "Cod."
    Me.xx_codvar1.Enabled = True
    Me.xx_codvar1.FieldName = "xx_codvar1"
    Me.xx_codvar1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codvar1.Name = "xx_codvar1"
    Me.xx_codvar1.NTSRepositoryComboBox = Nothing
    Me.xx_codvar1.NTSRepositoryItemCheck = Nothing
    Me.xx_codvar1.NTSRepositoryItemMemo = Nothing
    Me.xx_codvar1.NTSRepositoryItemText = Nothing
    Me.xx_codvar1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codvar1.OptionsFilter.AllowFilter = False
    Me.xx_codvar1.Visible = True
    Me.xx_codvar1.VisibleIndex = 1
    '
    'xx_descr1
    '
    Me.xx_descr1.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr1.Caption = "Descr."
    Me.xx_descr1.Enabled = True
    Me.xx_descr1.FieldName = "xx_descr1"
    Me.xx_descr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr1.Name = "xx_descr1"
    Me.xx_descr1.NTSRepositoryComboBox = Nothing
    Me.xx_descr1.NTSRepositoryItemCheck = Nothing
    Me.xx_descr1.NTSRepositoryItemMemo = Nothing
    Me.xx_descr1.NTSRepositoryItemText = Nothing
    Me.xx_descr1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr1.OptionsFilter.AllowFilter = False
    Me.xx_descr1.Visible = True
    Me.xx_descr1.VisibleIndex = 2
    '
    'xx_codditt1
    '
    Me.xx_codditt1.AppearanceCell.Options.UseBackColor = True
    Me.xx_codditt1.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codditt1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codditt1.Caption = "Codice ditta"
    Me.xx_codditt1.Enabled = False
    Me.xx_codditt1.FieldName = "xx_codditt1"
    Me.xx_codditt1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codditt1.Name = "xx_codditt1"
    Me.xx_codditt1.NTSRepositoryComboBox = Nothing
    Me.xx_codditt1.NTSRepositoryItemCheck = Nothing
    Me.xx_codditt1.NTSRepositoryItemMemo = Nothing
    Me.xx_codditt1.NTSRepositoryItemText = Nothing
    Me.xx_codditt1.OptionsColumn.AllowEdit = False
    Me.xx_codditt1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codditt1.OptionsColumn.ReadOnly = True
    Me.xx_codditt1.OptionsFilter.AllowFilter = False
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnTabpagAnaliticoVar)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(790, 321)
    Me.NtsTabPage2.Text = "&2 - Analitico varianti"
    '
    'pnTabpagAnaliticoVar
    '
    Me.pnTabpagAnaliticoVar.AllowDrop = True
    Me.pnTabpagAnaliticoVar.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpagAnaliticoVar.Appearance.Options.UseBackColor = True
    Me.pnTabpagAnaliticoVar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpagAnaliticoVar.Controls.Add(Me.grArtico)
    Me.pnTabpagAnaliticoVar.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpagAnaliticoVar.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpagAnaliticoVar.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpagAnaliticoVar.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpagAnaliticoVar.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpagAnaliticoVar.Name = "pnTabpagAnaliticoVar"
    Me.pnTabpagAnaliticoVar.NTSActiveTrasparency = True
    Me.pnTabpagAnaliticoVar.Size = New System.Drawing.Size(790, 321)
    Me.pnTabpagAnaliticoVar.TabIndex = 0
    Me.pnTabpagAnaliticoVar.Text = "NtsPanel1"
    '
    'grArtico
    '
    Me.grArtico.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grArtico.EmbeddedNavigator.Name = ""
    Me.grArtico.Location = New System.Drawing.Point(0, 0)
    Me.grArtico.MainView = Me.grvArtico
    Me.grArtico.Name = "grArtico"
    Me.grArtico.Size = New System.Drawing.Size(790, 321)
    Me.grArtico.TabIndex = 0
    Me.grArtico.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvArtico})
    '
    'grvArtico
    '
    Me.grvArtico.ActiveFilterEnabled = False
    Me.grvArtico.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ar_codvar1, Me.ar_codvar2, Me.ar_codvar3, Me.ar_descr, Me.ar_desint, Me.ar_scomin, Me.ar_scomax, Me.ar_minord, Me.ar_inesaur, Me.ar_note, Me.ar_codart, Me.ar_formula, Me.codditt})
    Me.grvArtico.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvArtico.Enabled = True
    Me.grvArtico.GridControl = Me.grArtico
    Me.grvArtico.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvArtico.MinRowHeight = 14
    Me.grvArtico.Name = "grvArtico"
    Me.grvArtico.NTSAllowDelete = True
    Me.grvArtico.NTSAllowInsert = True
    Me.grvArtico.NTSAllowUpdate = True
    Me.grvArtico.NTSMenuContext = Nothing
    Me.grvArtico.OptionsCustomization.AllowRowSizing = True
    Me.grvArtico.OptionsFilter.AllowFilterEditor = False
    Me.grvArtico.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvArtico.OptionsNavigation.UseTabKey = False
    Me.grvArtico.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvArtico.OptionsView.ColumnAutoWidth = False
    Me.grvArtico.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvArtico.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvArtico.OptionsView.ShowGroupPanel = False
    Me.grvArtico.RowHeight = 14
    '
    'ar_codvar1
    '
    Me.ar_codvar1.AppearanceCell.Options.UseBackColor = True
    Me.ar_codvar1.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codvar1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codvar1.Caption = "1°livello"
    Me.ar_codvar1.Enabled = True
    Me.ar_codvar1.FieldName = "ar_codvar1"
    Me.ar_codvar1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codvar1.Name = "ar_codvar1"
    Me.ar_codvar1.NTSRepositoryComboBox = Nothing
    Me.ar_codvar1.NTSRepositoryItemCheck = Nothing
    Me.ar_codvar1.NTSRepositoryItemMemo = Nothing
    Me.ar_codvar1.NTSRepositoryItemText = Nothing
    Me.ar_codvar1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codvar1.OptionsFilter.AllowFilter = False
    Me.ar_codvar1.Visible = True
    Me.ar_codvar1.VisibleIndex = 0
    '
    'ar_codvar2
    '
    Me.ar_codvar2.AppearanceCell.Options.UseBackColor = True
    Me.ar_codvar2.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codvar2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codvar2.Caption = "2°livello"
    Me.ar_codvar2.Enabled = True
    Me.ar_codvar2.FieldName = "ar_codvar2"
    Me.ar_codvar2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codvar2.Name = "ar_codvar2"
    Me.ar_codvar2.NTSRepositoryComboBox = Nothing
    Me.ar_codvar2.NTSRepositoryItemCheck = Nothing
    Me.ar_codvar2.NTSRepositoryItemMemo = Nothing
    Me.ar_codvar2.NTSRepositoryItemText = Nothing
    Me.ar_codvar2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codvar2.OptionsFilter.AllowFilter = False
    Me.ar_codvar2.Visible = True
    Me.ar_codvar2.VisibleIndex = 1
    '
    'ar_codvar3
    '
    Me.ar_codvar3.AppearanceCell.Options.UseBackColor = True
    Me.ar_codvar3.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codvar3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codvar3.Caption = "3°livello"
    Me.ar_codvar3.Enabled = True
    Me.ar_codvar3.FieldName = "ar_codvar3"
    Me.ar_codvar3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codvar3.Name = "ar_codvar3"
    Me.ar_codvar3.NTSRepositoryComboBox = Nothing
    Me.ar_codvar3.NTSRepositoryItemCheck = Nothing
    Me.ar_codvar3.NTSRepositoryItemMemo = Nothing
    Me.ar_codvar3.NTSRepositoryItemText = Nothing
    Me.ar_codvar3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codvar3.OptionsFilter.AllowFilter = False
    Me.ar_codvar3.Visible = True
    Me.ar_codvar3.VisibleIndex = 2
    '
    'ar_descr
    '
    Me.ar_descr.AppearanceCell.Options.UseBackColor = True
    Me.ar_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ar_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_descr.Caption = "Descrizione"
    Me.ar_descr.Enabled = True
    Me.ar_descr.FieldName = "ar_descr"
    Me.ar_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_descr.Name = "ar_descr"
    Me.ar_descr.NTSRepositoryComboBox = Nothing
    Me.ar_descr.NTSRepositoryItemCheck = Nothing
    Me.ar_descr.NTSRepositoryItemMemo = Nothing
    Me.ar_descr.NTSRepositoryItemText = Nothing
    Me.ar_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_descr.OptionsFilter.AllowFilter = False
    Me.ar_descr.Visible = True
    Me.ar_descr.VisibleIndex = 3
    '
    'ar_desint
    '
    Me.ar_desint.AppearanceCell.Options.UseBackColor = True
    Me.ar_desint.AppearanceCell.Options.UseTextOptions = True
    Me.ar_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_desint.Caption = "Descr.interna"
    Me.ar_desint.Enabled = True
    Me.ar_desint.FieldName = "ar_desint"
    Me.ar_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_desint.Name = "ar_desint"
    Me.ar_desint.NTSRepositoryComboBox = Nothing
    Me.ar_desint.NTSRepositoryItemCheck = Nothing
    Me.ar_desint.NTSRepositoryItemMemo = Nothing
    Me.ar_desint.NTSRepositoryItemText = Nothing
    Me.ar_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_desint.OptionsFilter.AllowFilter = False
    Me.ar_desint.Visible = True
    Me.ar_desint.VisibleIndex = 4
    '
    'ar_scomin
    '
    Me.ar_scomin.AppearanceCell.Options.UseBackColor = True
    Me.ar_scomin.AppearanceCell.Options.UseTextOptions = True
    Me.ar_scomin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_scomin.Caption = "Scorta min."
    Me.ar_scomin.Enabled = True
    Me.ar_scomin.FieldName = "ar_scomin"
    Me.ar_scomin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_scomin.Name = "ar_scomin"
    Me.ar_scomin.NTSRepositoryComboBox = Nothing
    Me.ar_scomin.NTSRepositoryItemCheck = Nothing
    Me.ar_scomin.NTSRepositoryItemMemo = Nothing
    Me.ar_scomin.NTSRepositoryItemText = Nothing
    Me.ar_scomin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_scomin.OptionsFilter.AllowFilter = False
    Me.ar_scomin.Visible = True
    Me.ar_scomin.VisibleIndex = 5
    '
    'ar_scomax
    '
    Me.ar_scomax.AppearanceCell.Options.UseBackColor = True
    Me.ar_scomax.AppearanceCell.Options.UseTextOptions = True
    Me.ar_scomax.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_scomax.Caption = "Scorta max."
    Me.ar_scomax.Enabled = True
    Me.ar_scomax.FieldName = "ar_scomax"
    Me.ar_scomax.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_scomax.Name = "ar_scomax"
    Me.ar_scomax.NTSRepositoryComboBox = Nothing
    Me.ar_scomax.NTSRepositoryItemCheck = Nothing
    Me.ar_scomax.NTSRepositoryItemMemo = Nothing
    Me.ar_scomax.NTSRepositoryItemText = Nothing
    Me.ar_scomax.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_scomax.OptionsFilter.AllowFilter = False
    Me.ar_scomax.Visible = True
    Me.ar_scomax.VisibleIndex = 6
    '
    'ar_minord
    '
    Me.ar_minord.AppearanceCell.Options.UseBackColor = True
    Me.ar_minord.AppearanceCell.Options.UseTextOptions = True
    Me.ar_minord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_minord.Caption = "Qtà lotto stand."
    Me.ar_minord.Enabled = True
    Me.ar_minord.FieldName = "ar_minord"
    Me.ar_minord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_minord.Name = "ar_minord"
    Me.ar_minord.NTSRepositoryComboBox = Nothing
    Me.ar_minord.NTSRepositoryItemCheck = Nothing
    Me.ar_minord.NTSRepositoryItemMemo = Nothing
    Me.ar_minord.NTSRepositoryItemText = Nothing
    Me.ar_minord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_minord.OptionsFilter.AllowFilter = False
    Me.ar_minord.Visible = True
    Me.ar_minord.VisibleIndex = 7
    '
    'ar_inesaur
    '
    Me.ar_inesaur.AppearanceCell.Options.UseBackColor = True
    Me.ar_inesaur.AppearanceCell.Options.UseTextOptions = True
    Me.ar_inesaur.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_inesaur.Caption = "In esaurim."
    Me.ar_inesaur.Enabled = True
    Me.ar_inesaur.FieldName = "ar_inesaur"
    Me.ar_inesaur.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_inesaur.Name = "ar_inesaur"
    Me.ar_inesaur.NTSRepositoryComboBox = Nothing
    Me.ar_inesaur.NTSRepositoryItemCheck = Nothing
    Me.ar_inesaur.NTSRepositoryItemMemo = Nothing
    Me.ar_inesaur.NTSRepositoryItemText = Nothing
    Me.ar_inesaur.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_inesaur.OptionsFilter.AllowFilter = False
    Me.ar_inesaur.Visible = True
    Me.ar_inesaur.VisibleIndex = 8
    '
    'ar_note
    '
    Me.ar_note.AppearanceCell.Options.UseBackColor = True
    Me.ar_note.AppearanceCell.Options.UseTextOptions = True
    Me.ar_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_note.Caption = "Note"
    Me.ar_note.Enabled = True
    Me.ar_note.FieldName = "ar_note"
    Me.ar_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_note.Name = "ar_note"
    Me.ar_note.NTSRepositoryComboBox = Nothing
    Me.ar_note.NTSRepositoryItemCheck = Nothing
    Me.ar_note.NTSRepositoryItemMemo = Nothing
    Me.ar_note.NTSRepositoryItemText = Nothing
    Me.ar_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_note.OptionsFilter.AllowFilter = False
    Me.ar_note.Visible = True
    Me.ar_note.VisibleIndex = 9
    '
    'ar_codart
    '
    Me.ar_codart.AppearanceCell.Options.UseBackColor = True
    Me.ar_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codart.Caption = "AR_CODART"
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
    'ar_formula
    '
    Me.ar_formula.AppearanceCell.Options.UseBackColor = True
    Me.ar_formula.AppearanceCell.Options.UseTextOptions = True
    Me.ar_formula.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_formula.Caption = "Formula"
    Me.ar_formula.Enabled = True
    Me.ar_formula.FieldName = "ar_formula"
    Me.ar_formula.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_formula.Name = "ar_formula"
    Me.ar_formula.NTSRepositoryComboBox = Nothing
    Me.ar_formula.NTSRepositoryItemCheck = Nothing
    Me.ar_formula.NTSRepositoryItemMemo = Nothing
    Me.ar_formula.NTSRepositoryItemText = Nothing
    Me.ar_formula.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_formula.OptionsFilter.AllowFilter = False
    Me.ar_formula.Visible = True
    Me.ar_formula.VisibleIndex = 10
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Caption = "Codice ditta"
    Me.codditt.Enabled = False
    Me.codditt.FieldName = "codditt"
    Me.codditt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.codditt.Name = "codditt"
    Me.codditt.NTSRepositoryComboBox = Nothing
    Me.codditt.NTSRepositoryItemCheck = Nothing
    Me.codditt.NTSRepositoryItemMemo = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Controls.Add(Me.pnTabpag1)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(790, 321)
    Me.NtsTabPage3.Text = "&3 - Vendite"
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
    Me.pnTabpag1.Name = "pnTabpag1"
    Me.pnTabpag1.NTSActiveTrasparency = True
    Me.pnTabpag1.Size = New System.Drawing.Size(790, 321)
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
    Me.pnTabpag1Right.Location = New System.Drawing.Point(361, 0)
    Me.pnTabpag1Right.Name = "pnTabpag1Right"
    Me.pnTabpag1Right.NTSActiveTrasparency = True
    Me.pnTabpag1Right.Size = New System.Drawing.Size(429, 321)
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
    Me.fmEcommerce.Location = New System.Drawing.Point(21, 193)
    Me.fmEcommerce.Name = "fmEcommerce"
    Me.fmEcommerce.Size = New System.Drawing.Size(392, 102)
    Me.fmEcommerce.TabIndex = 651
    Me.fmEcommerce.Text = "e-Commerce"
    '
    'lbXx_codseat
    '
    Me.lbXx_codseat.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codseat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codseat.Location = New System.Drawing.Point(128, 78)
    Me.lbXx_codseat.Name = "lbXx_codseat"
    Me.lbXx_codseat.NTSDbField = ""
    Me.lbXx_codseat.Size = New System.Drawing.Size(250, 20)
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
    Me.lbXx_numecr.Location = New System.Drawing.Point(166, 50)
    Me.lbXx_numecr.Name = "lbXx_numecr"
    Me.lbXx_numecr.NTSDbField = ""
    Me.lbXx_numecr.Size = New System.Drawing.Size(247, 20)
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
    Me.fmIntrastat.Location = New System.Drawing.Point(20, 75)
    Me.fmIntrastat.Name = "fmIntrastat"
    Me.fmIntrastat.Size = New System.Drawing.Size(327, 112)
    Me.fmIntrastat.TabIndex = 648
    Me.fmIntrastat.Text = "INTRASTAT"
    '
    'lbAr_umintra2
    '
    Me.lbAr_umintra2.AutoSize = True
    Me.lbAr_umintra2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_umintra2.Location = New System.Drawing.Point(8, 91)
    Me.lbAr_umintra2.Name = "lbAr_umintra2"
    Me.lbAr_umintra2.NTSDbField = ""
    Me.lbAr_umintra2.Size = New System.Drawing.Size(81, 13)
    Me.lbAr_umintra2.TabIndex = 568
    Me.lbAr_umintra2.Text = "UM secondaria:"
    Me.lbAr_umintra2.Tooltip = ""
    Me.lbAr_umintra2.UseMnemonic = False
    '
    'cbAr_umintra2
    '
    Me.cbAr_umintra2.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_umintra2.DataSource = Nothing
    Me.cbAr_umintra2.DisplayMember = ""
    Me.cbAr_umintra2.Location = New System.Drawing.Point(113, 88)
    Me.cbAr_umintra2.Name = "cbAr_umintra2"
    Me.cbAr_umintra2.NTSDbField = ""
    Me.cbAr_umintra2.Properties.AutoHeight = False
    Me.cbAr_umintra2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_umintra2.Properties.DropDownRows = 30
    Me.cbAr_umintra2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_umintra2.SelectedValue = ""
    Me.cbAr_umintra2.Size = New System.Drawing.Size(131, 20)
    Me.cbAr_umintra2.TabIndex = 569
    Me.cbAr_umintra2.ValueMember = ""
    '
    'lbAr_codnomc
    '
    Me.lbAr_codnomc.AutoSize = True
    Me.lbAr_codnomc.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codnomc.Location = New System.Drawing.Point(8, 69)
    Me.lbAr_codnomc.Name = "lbAr_codnomc"
    Me.lbAr_codnomc.NTSDbField = ""
    Me.lbAr_codnomc.Size = New System.Drawing.Size(99, 13)
    Me.lbAr_codnomc.TabIndex = 550
    Me.lbAr_codnomc.Text = "Codice nom.comb.:"
    Me.lbAr_codnomc.Tooltip = ""
    Me.lbAr_codnomc.UseMnemonic = False
    '
    'edAr_codnomc
    '
    Me.edAr_codnomc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codnomc.EditValue = ""
    Me.edAr_codnomc.Location = New System.Drawing.Point(113, 66)
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
    Me.edAr_codnomc.Size = New System.Drawing.Size(192, 20)
    Me.edAr_codnomc.TabIndex = 551
    '
    'lbAr_percvst
    '
    Me.lbAr_percvst.AutoSize = True
    Me.lbAr_percvst.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_percvst.Location = New System.Drawing.Point(8, 47)
    Me.lbAr_percvst.Name = "lbAr_percvst"
    Me.lbAr_percvst.NTSDbField = ""
    Me.lbAr_percvst.Size = New System.Drawing.Size(86, 13)
    Me.lbAr_percvst.TabIndex = 548
    Me.lbAr_percvst.Text = "% Val.statistico:"
    Me.lbAr_percvst.Tooltip = ""
    Me.lbAr_percvst.UseMnemonic = False
    '
    'edAr_percvst
    '
    Me.edAr_percvst.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_percvst.EditValue = "0"
    Me.edAr_percvst.Location = New System.Drawing.Point(113, 44)
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
    Me.edAr_percvst.Size = New System.Drawing.Size(120, 20)
    Me.edAr_percvst.TabIndex = 549
    '
    'lbAr_prorig
    '
    Me.lbAr_prorig.AutoSize = True
    Me.lbAr_prorig.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_prorig.Location = New System.Drawing.Point(8, 25)
    Me.lbAr_prorig.Name = "lbAr_prorig"
    Me.lbAr_prorig.NTSDbField = ""
    Me.lbAr_prorig.Size = New System.Drawing.Size(102, 13)
    Me.lbAr_prorig.TabIndex = 546
    Me.lbAr_prorig.Text = " Prov\Paese Origine"
    Me.lbAr_prorig.Tooltip = ""
    Me.lbAr_prorig.UseMnemonic = False
    '
    'edAr_prorig
    '
    Me.edAr_prorig.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_prorig.EditValue = ""
    Me.edAr_prorig.Location = New System.Drawing.Point(113, 22)
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
    Me.edAr_paeorig.Location = New System.Drawing.Point(169, 22)
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
    Me.lbAr_sostit.Location = New System.Drawing.Point(17, 8)
    Me.lbAr_sostit.Name = "lbAr_sostit"
    Me.lbAr_sostit.NTSDbField = ""
    Me.lbAr_sostit.Size = New System.Drawing.Size(80, 13)
    Me.lbAr_sostit.TabIndex = 646
    Me.lbAr_sostit.Text = "Art.sostitutivo:"
    Me.lbAr_sostit.Tooltip = ""
    Me.lbAr_sostit.UseMnemonic = False
    '
    'edAr_sostit
    '
    Me.edAr_sostit.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_sostit.EditValue = ""
    Me.edAr_sostit.Location = New System.Drawing.Point(103, 5)
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
    Me.edAr_sostit.Size = New System.Drawing.Size(187, 20)
    Me.edAr_sostit.TabIndex = 647
    '
    'lbAr_sostituito
    '
    Me.lbAr_sostituito.AutoSize = True
    Me.lbAr_sostituito.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_sostituito.Location = New System.Drawing.Point(18, 30)
    Me.lbAr_sostituito.Name = "lbAr_sostituito"
    Me.lbAr_sostituito.NTSDbField = ""
    Me.lbAr_sostituito.Size = New System.Drawing.Size(74, 13)
    Me.lbAr_sostituito.TabIndex = 644
    Me.lbAr_sostituito.Text = "Art.sostituito:"
    Me.lbAr_sostituito.Tooltip = ""
    Me.lbAr_sostituito.UseMnemonic = False
    '
    'edAr_sostituito
    '
    Me.edAr_sostituito.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_sostituito.EditValue = ""
    Me.edAr_sostituito.Location = New System.Drawing.Point(103, 27)
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
    Me.edAr_sostituito.Size = New System.Drawing.Size(187, 20)
    Me.edAr_sostituito.TabIndex = 645
    '
    'lbAr_numecr
    '
    Me.lbAr_numecr.AutoSize = True
    Me.lbAr_numecr.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_numecr.Location = New System.Drawing.Point(17, 52)
    Me.lbAr_numecr.Name = "lbAr_numecr"
    Me.lbAr_numecr.NTSDbField = ""
    Me.lbAr_numecr.Size = New System.Drawing.Size(69, 13)
    Me.lbAr_numecr.TabIndex = 642
    Me.lbAr_numecr.Text = "Centro C.A.:"
    Me.lbAr_numecr.Tooltip = ""
    Me.lbAr_numecr.UseMnemonic = False
    '
    'edAr_numecr
    '
    Me.edAr_numecr.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAr_numecr.EditValue = "0"
    Me.edAr_numecr.Location = New System.Drawing.Point(103, 49)
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
    Me.pnTabpag1Left.Name = "pnTabpag1Left"
    Me.pnTabpag1Left.NTSActiveTrasparency = True
    Me.pnTabpag1Left.Size = New System.Drawing.Size(361, 321)
    Me.pnTabpag1Left.TabIndex = 643
    Me.pnTabpag1Left.Text = "NtsPanel1"
    '
    'lbXx_reparto
    '
    Me.lbXx_reparto.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_reparto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_reparto.Location = New System.Drawing.Point(173, 159)
    Me.lbXx_reparto.Name = "lbXx_reparto"
    Me.lbXx_reparto.NTSDbField = ""
    Me.lbXx_reparto.Size = New System.Drawing.Size(176, 20)
    Me.lbXx_reparto.TabIndex = 666
    Me.lbXx_reparto.Tooltip = ""
    Me.lbXx_reparto.UseMnemonic = False
    '
    'lbXx_codpdon
    '
    Me.lbXx_codpdon.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codpdon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codpdon.Location = New System.Drawing.Point(173, 71)
    Me.lbXx_codpdon.Name = "lbXx_codpdon"
    Me.lbXx_codpdon.NTSDbField = ""
    Me.lbXx_codpdon.Size = New System.Drawing.Size(176, 20)
    Me.lbXx_codpdon.TabIndex = 666
    Me.lbXx_codpdon.Tooltip = ""
    Me.lbXx_codpdon.UseMnemonic = False
    '
    'lbXx_codappr
    '
    Me.lbXx_codappr.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codappr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codappr.Location = New System.Drawing.Point(173, 115)
    Me.lbXx_codappr.Name = "lbXx_codappr"
    Me.lbXx_codappr.NTSDbField = ""
    Me.lbXx_codappr.Size = New System.Drawing.Size(176, 20)
    Me.lbXx_codappr.TabIndex = 665
    Me.lbXx_codappr.Tooltip = ""
    Me.lbXx_codappr.UseMnemonic = False
    '
    'lbXx_codmarc
    '
    Me.lbXx_codmarc.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codmarc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codmarc.Location = New System.Drawing.Point(173, 5)
    Me.lbXx_codmarc.Name = "lbXx_codmarc"
    Me.lbXx_codmarc.NTSDbField = ""
    Me.lbXx_codmarc.Size = New System.Drawing.Size(154, 20)
    Me.lbXx_codmarc.TabIndex = 664
    Me.lbXx_codmarc.Tooltip = ""
    Me.lbXx_codmarc.UseMnemonic = False
    '
    'lbAr_codappr
    '
    Me.lbAr_codappr.AutoSize = True
    Me.lbAr_codappr.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codappr.Location = New System.Drawing.Point(12, 117)
    Me.lbAr_codappr.Name = "lbAr_codappr"
    Me.lbAr_codappr.NTSDbField = ""
    Me.lbAr_codappr.Size = New System.Drawing.Size(100, 13)
    Me.lbAr_codappr.TabIndex = 658
    Me.lbAr_codappr.Text = "Approvvigionatore:"
    Me.lbAr_codappr.Tooltip = ""
    Me.lbAr_codappr.UseMnemonic = False
    '
    'edAr_codappr
    '
    Me.edAr_codappr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codappr.EditValue = "0"
    Me.edAr_codappr.Location = New System.Drawing.Point(117, 115)
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
    Me.lbAr_tipokit.Location = New System.Drawing.Point(12, 140)
    Me.lbAr_tipokit.Name = "lbAr_tipokit"
    Me.lbAr_tipokit.NTSDbField = ""
    Me.lbAr_tipokit.Size = New System.Drawing.Size(45, 13)
    Me.lbAr_tipokit.TabIndex = 659
    Me.lbAr_tipokit.Text = "Tipo kit:"
    Me.lbAr_tipokit.Tooltip = ""
    Me.lbAr_tipokit.UseMnemonic = False
    '
    'cbAr_tipokit
    '
    Me.cbAr_tipokit.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_tipokit.DataSource = Nothing
    Me.cbAr_tipokit.DisplayMember = ""
    Me.cbAr_tipokit.Location = New System.Drawing.Point(117, 137)
    Me.cbAr_tipokit.Name = "cbAr_tipokit"
    Me.cbAr_tipokit.NTSDbField = ""
    Me.cbAr_tipokit.Properties.AutoHeight = False
    Me.cbAr_tipokit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_tipokit.Properties.DropDownRows = 30
    Me.cbAr_tipokit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_tipokit.SelectedValue = ""
    Me.cbAr_tipokit.Size = New System.Drawing.Size(232, 20)
    Me.cbAr_tipokit.TabIndex = 661
    Me.cbAr_tipokit.ValueMember = ""
    '
    'lbAr_flricmar
    '
    Me.lbAr_flricmar.AutoSize = True
    Me.lbAr_flricmar.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_flricmar.Location = New System.Drawing.Point(12, 95)
    Me.lbAr_flricmar.Name = "lbAr_flricmar"
    Me.lbAr_flricmar.NTSDbField = ""
    Me.lbAr_flricmar.Size = New System.Drawing.Size(90, 13)
    Me.lbAr_flricmar.TabIndex = 652
    Me.lbAr_flricmar.Text = "Ricarico/Margine:"
    Me.lbAr_flricmar.Tooltip = ""
    Me.lbAr_flricmar.UseMnemonic = False
    '
    'cbAr_flricmar
    '
    Me.cbAr_flricmar.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_flricmar.DataSource = Nothing
    Me.cbAr_flricmar.DisplayMember = ""
    Me.cbAr_flricmar.Location = New System.Drawing.Point(117, 93)
    Me.cbAr_flricmar.Name = "cbAr_flricmar"
    Me.cbAr_flricmar.NTSDbField = ""
    Me.cbAr_flricmar.Properties.AutoHeight = False
    Me.cbAr_flricmar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_flricmar.Properties.DropDownRows = 30
    Me.cbAr_flricmar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_flricmar.SelectedValue = ""
    Me.cbAr_flricmar.Size = New System.Drawing.Size(100, 20)
    Me.cbAr_flricmar.TabIndex = 654
    Me.cbAr_flricmar.ValueMember = ""
    '
    'lbAr_codpdon
    '
    Me.lbAr_codpdon.AutoSize = True
    Me.lbAr_codpdon.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codpdon.Location = New System.Drawing.Point(12, 74)
    Me.lbAr_codpdon.Name = "lbAr_codpdon"
    Me.lbAr_codpdon.NTSDbField = ""
    Me.lbAr_codpdon.Size = New System.Drawing.Size(83, 13)
    Me.lbAr_codpdon.TabIndex = 653
    Me.lbAr_codpdon.Text = "Relazione listini:"
    Me.lbAr_codpdon.Tooltip = ""
    Me.lbAr_codpdon.UseMnemonic = False
    '
    'edAr_codpdon
    '
    Me.edAr_codpdon.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codpdon.EditValue = "0"
    Me.edAr_codpdon.Location = New System.Drawing.Point(117, 71)
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
    Me.edAr_ricar1.Location = New System.Drawing.Point(223, 93)
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
    Me.edAr_ricar1.Size = New System.Drawing.Size(59, 20)
    Me.edAr_ricar1.TabIndex = 656
    '
    'edAr_ricar2
    '
    Me.edAr_ricar2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ricar2.EditValue = "0"
    Me.edAr_ricar2.Location = New System.Drawing.Point(288, 93)
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
    Me.edAr_ricar2.Size = New System.Drawing.Size(58, 20)
    Me.edAr_ricar2.TabIndex = 657
    '
    'lbAr_reparto
    '
    Me.lbAr_reparto.AutoSize = True
    Me.lbAr_reparto.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_reparto.Location = New System.Drawing.Point(12, 162)
    Me.lbAr_reparto.Name = "lbAr_reparto"
    Me.lbAr_reparto.NTSDbField = ""
    Me.lbAr_reparto.Size = New System.Drawing.Size(81, 13)
    Me.lbAr_reparto.TabIndex = 650
    Me.lbAr_reparto.Text = "Reparto (ECR):"
    Me.lbAr_reparto.Tooltip = ""
    Me.lbAr_reparto.UseMnemonic = False
    '
    'edAr_reparto
    '
    Me.edAr_reparto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_reparto.EditValue = "0"
    Me.edAr_reparto.Location = New System.Drawing.Point(117, 159)
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
    Me.edAr_garacq.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAr_garacq.EditValue = "0"
    Me.edAr_garacq.Location = New System.Drawing.Point(175, 49)
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
    Me.lbAr_garven.Location = New System.Drawing.Point(12, 52)
    Me.lbAr_garven.Name = "lbAr_garven"
    Me.lbAr_garven.NTSDbField = ""
    Me.lbAr_garven.Size = New System.Drawing.Size(104, 13)
    Me.lbAr_garven.TabIndex = 647
    Me.lbAr_garven.Text = "Mesi gar.vend./acq."
    Me.lbAr_garven.Tooltip = ""
    Me.lbAr_garven.UseMnemonic = False
    '
    'edAr_garven
    '
    Me.edAr_garven.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_garven.EditValue = "0"
    Me.edAr_garven.Location = New System.Drawing.Point(117, 49)
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
    Me.lbAr_perqta.Location = New System.Drawing.Point(12, 30)
    Me.lbAr_perqta.Name = "lbAr_perqta"
    Me.lbAr_perqta.NTSDbField = ""
    Me.lbAr_perqta.Size = New System.Drawing.Size(87, 13)
    Me.lbAr_perqta.TabIndex = 645
    Me.lbAr_perqta.Text = "Molt.qtà/prezzo:"
    Me.lbAr_perqta.Tooltip = ""
    Me.lbAr_perqta.UseMnemonic = False
    '
    'edAr_perqta
    '
    Me.edAr_perqta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_perqta.EditValue = "0"
    Me.edAr_perqta.Location = New System.Drawing.Point(117, 27)
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
    Me.edAr_perqta.Size = New System.Drawing.Size(165, 20)
    Me.edAr_perqta.TabIndex = 646
    '
    'lbAr_codmarc
    '
    Me.lbAr_codmarc.AutoSize = True
    Me.lbAr_codmarc.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codmarc.Location = New System.Drawing.Point(12, 8)
    Me.lbAr_codmarc.Name = "lbAr_codmarc"
    Me.lbAr_codmarc.NTSDbField = ""
    Me.lbAr_codmarc.Size = New System.Drawing.Size(40, 13)
    Me.lbAr_codmarc.TabIndex = 643
    Me.lbAr_codmarc.Text = "Marca:"
    Me.lbAr_codmarc.Tooltip = ""
    Me.lbAr_codmarc.UseMnemonic = False
    '
    'edAr_codmarc
    '
    Me.edAr_codmarc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codmarc.EditValue = "0"
    Me.edAr_codmarc.Location = New System.Drawing.Point(117, 5)
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
    'NtsTabPage4
    '
    Me.NtsTabPage4.AllowDrop = True
    Me.NtsTabPage4.Controls.Add(Me.pnTabpag2)
    Me.NtsTabPage4.Enable = True
    Me.NtsTabPage4.Name = "NtsTabPage4"
    Me.NtsTabPage4.Size = New System.Drawing.Size(790, 321)
    Me.NtsTabPage4.Text = "&4 - Acquisti e Produzione"
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
    Me.pnTabpag2.Name = "pnTabpag2"
    Me.pnTabpag2.NTSActiveTrasparency = True
    Me.pnTabpag2.Size = New System.Drawing.Size(790, 321)
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
    Me.fmProduzione.Location = New System.Drawing.Point(335, 3)
    Me.fmProduzione.Name = "fmProduzione"
    Me.fmProduzione.Size = New System.Drawing.Size(323, 312)
    Me.fmProduzione.TabIndex = 601
    Me.fmProduzione.Text = "Produzione"
    '
    'ckAr_consmrp
    '
    Me.ckAr_consmrp.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_consmrp.Location = New System.Drawing.Point(129, 206)
    Me.ckAr_consmrp.Name = "ckAr_consmrp"
    Me.ckAr_consmrp.NTSCheckValue = "S"
    Me.ckAr_consmrp.NTSUnCheckValue = "N"
    Me.ckAr_consmrp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_consmrp.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_consmrp.Properties.AutoHeight = False
    Me.ckAr_consmrp.Properties.Caption = "Considera in &MRP/Distinte Base"
    Me.ckAr_consmrp.Size = New System.Drawing.Size(187, 19)
    Me.ckAr_consmrp.TabIndex = 635
    '
    'ckAr_critico
    '
    Me.ckAr_critico.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_critico.Location = New System.Drawing.Point(9, 206)
    Me.ckAr_critico.Name = "ckAr_critico"
    Me.ckAr_critico.NTSCheckValue = "S"
    Me.ckAr_critico.NTSUnCheckValue = "N"
    Me.ckAr_critico.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_critico.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_critico.Properties.AutoHeight = False
    Me.ckAr_critico.Properties.Caption = "&Componente critico"
    Me.ckAr_critico.Size = New System.Drawing.Size(118, 18)
    Me.ckAr_critico.TabIndex = 633
    '
    'ckAr_blocco
    '
    Me.ckAr_blocco.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_blocco.Location = New System.Drawing.Point(9, 230)
    Me.ckAr_blocco.Name = "ckAr_blocco"
    Me.ckAr_blocco.NTSCheckValue = "S"
    Me.ckAr_blocco.NTSUnCheckValue = "N"
    Me.ckAr_blocco.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_blocco.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_blocco.Properties.AutoHeight = False
    Me.ckAr_blocco.Properties.Caption = "&Blocco"
    Me.ckAr_blocco.Size = New System.Drawing.Size(58, 18)
    Me.ckAr_blocco.TabIndex = 632
    '
    'lbAr_tipoopz
    '
    Me.lbAr_tipoopz.AutoSize = True
    Me.lbAr_tipoopz.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_tipoopz.Location = New System.Drawing.Point(6, 156)
    Me.lbAr_tipoopz.Name = "lbAr_tipoopz"
    Me.lbAr_tipoopz.NTSDbField = ""
    Me.lbAr_tipoopz.Size = New System.Drawing.Size(69, 13)
    Me.lbAr_tipoopz.TabIndex = 628
    Me.lbAr_tipoopz.Text = "Tipo Opzione"
    Me.lbAr_tipoopz.Tooltip = ""
    Me.lbAr_tipoopz.UseMnemonic = False
    '
    'cbAr_tipoopz
    '
    Me.cbAr_tipoopz.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_tipoopz.DataSource = Nothing
    Me.cbAr_tipoopz.DisplayMember = ""
    Me.cbAr_tipoopz.Location = New System.Drawing.Point(119, 153)
    Me.cbAr_tipoopz.Name = "cbAr_tipoopz"
    Me.cbAr_tipoopz.NTSDbField = ""
    Me.cbAr_tipoopz.Properties.AutoHeight = False
    Me.cbAr_tipoopz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_tipoopz.Properties.DropDownRows = 30
    Me.cbAr_tipoopz.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_tipoopz.SelectedValue = ""
    Me.cbAr_tipoopz.Size = New System.Drawing.Size(126, 20)
    Me.cbAr_tipoopz.TabIndex = 630
    Me.cbAr_tipoopz.ValueMember = ""
    '
    'lbAr_gescomm
    '
    Me.lbAr_gescomm.AutoSize = True
    Me.lbAr_gescomm.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gescomm.Location = New System.Drawing.Point(6, 182)
    Me.lbAr_gescomm.Name = "lbAr_gescomm"
    Me.lbAr_gescomm.NTSDbField = ""
    Me.lbAr_gescomm.Size = New System.Drawing.Size(104, 13)
    Me.lbAr_gescomm.TabIndex = 629
    Me.lbAr_gescomm.Text = "Gestione per Comm."
    Me.lbAr_gescomm.Tooltip = ""
    Me.lbAr_gescomm.UseMnemonic = False
    '
    'cbAr_gescomm
    '
    Me.cbAr_gescomm.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_gescomm.DataSource = Nothing
    Me.cbAr_gescomm.DisplayMember = ""
    Me.cbAr_gescomm.Location = New System.Drawing.Point(119, 179)
    Me.cbAr_gescomm.Name = "cbAr_gescomm"
    Me.cbAr_gescomm.NTSDbField = ""
    Me.cbAr_gescomm.Properties.AutoHeight = False
    Me.cbAr_gescomm.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_gescomm.Properties.DropDownRows = 30
    Me.cbAr_gescomm.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_gescomm.SelectedValue = ""
    Me.cbAr_gescomm.Size = New System.Drawing.Size(126, 20)
    Me.cbAr_gescomm.TabIndex = 631
    Me.cbAr_gescomm.ValueMember = ""
    '
    'lbAr_fpfence
    '
    Me.lbAr_fpfence.AutoSize = True
    Me.lbAr_fpfence.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_fpfence.Location = New System.Drawing.Point(180, 103)
    Me.lbAr_fpfence.Name = "lbAr_fpfence"
    Me.lbAr_fpfence.NTSDbField = ""
    Me.lbAr_fpfence.Size = New System.Drawing.Size(65, 13)
    Me.lbAr_fpfence.TabIndex = 624
    Me.lbAr_fpfence.Text = "Non usato 2"
    Me.lbAr_fpfence.Tooltip = ""
    Me.lbAr_fpfence.UseMnemonic = False
    '
    'edAr_fpfence
    '
    Me.edAr_fpfence.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_fpfence.EditValue = "0"
    Me.edAr_fpfence.Location = New System.Drawing.Point(250, 100)
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
    Me.edAr_fpfence.Size = New System.Drawing.Size(66, 20)
    Me.edAr_fpfence.TabIndex = 626
    '
    'lbAr_rrfence
    '
    Me.lbAr_rrfence.AutoSize = True
    Me.lbAr_rrfence.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_rrfence.Location = New System.Drawing.Point(6, 104)
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
    Me.edAr_rrfence.Location = New System.Drawing.Point(119, 101)
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
    Me.lbAr_ggrior.Location = New System.Drawing.Point(6, 78)
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
    Me.edAr_ggrior.Location = New System.Drawing.Point(119, 75)
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
    Me.lbAr_fcorrlt.Location = New System.Drawing.Point(5, 130)
    Me.lbAr_fcorrlt.Name = "lbAr_fcorrlt"
    Me.lbAr_fcorrlt.NTSDbField = ""
    Me.lbAr_fcorrlt.Size = New System.Drawing.Size(102, 13)
    Me.lbAr_fcorrlt.TabIndex = 618
    Me.lbAr_fcorrlt.Text = "Fattore correz. L.T."
    Me.lbAr_fcorrlt.Tooltip = ""
    Me.lbAr_fcorrlt.UseMnemonic = False
    '
    'edAr_fcorrlt
    '
    Me.edAr_fcorrlt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_fcorrlt.EditValue = "0"
    Me.edAr_fcorrlt.Location = New System.Drawing.Point(119, 127)
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
    Me.edAr_fcorrlt.Size = New System.Drawing.Size(126, 20)
    Me.edAr_fcorrlt.TabIndex = 620
    '
    'lbAr_verdb
    '
    Me.lbAr_verdb.AutoSize = True
    Me.lbAr_verdb.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_verdb.Location = New System.Drawing.Point(6, 52)
    Me.lbAr_verdb.Name = "lbAr_verdb"
    Me.lbAr_verdb.NTSDbField = ""
    Me.lbAr_verdb.Size = New System.Drawing.Size(97, 13)
    Me.lbAr_verdb.TabIndex = 619
    Me.lbAr_verdb.Text = "Vers. Distinta Base"
    Me.lbAr_verdb.Tooltip = ""
    Me.lbAr_verdb.UseMnemonic = False
    '
    'edAr_verdb
    '
    Me.edAr_verdb.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_verdb.EditValue = "0"
    Me.edAr_verdb.Location = New System.Drawing.Point(119, 49)
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
    Me.lbAr_livmindb.Size = New System.Drawing.Size(47, 13)
    Me.lbAr_livmindb.TabIndex = 614
    Me.lbAr_livmindb.Text = "Liv. Min."
    Me.lbAr_livmindb.Tooltip = ""
    Me.lbAr_livmindb.UseMnemonic = False
    '
    'edAr_livmindb
    '
    Me.edAr_livmindb.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_livmindb.EditValue = "0"
    Me.edAr_livmindb.Location = New System.Drawing.Point(251, 49)
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
    Me.edAr_livmindb.Size = New System.Drawing.Size(65, 20)
    Me.edAr_livmindb.TabIndex = 616
    '
    'lbAr_coddb
    '
    Me.lbAr_coddb.AutoSize = True
    Me.lbAr_coddb.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_coddb.Location = New System.Drawing.Point(5, 26)
    Me.lbAr_coddb.Name = "lbAr_coddb"
    Me.lbAr_coddb.NTSDbField = ""
    Me.lbAr_coddb.Size = New System.Drawing.Size(95, 13)
    Me.lbAr_coddb.TabIndex = 615
    Me.lbAr_coddb.Text = "Cod. Distinta Base"
    Me.lbAr_coddb.Tooltip = ""
    Me.lbAr_coddb.UseMnemonic = False
    '
    'edAr_coddb
    '
    Me.edAr_coddb.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_coddb.EditValue = ""
    Me.edAr_coddb.Location = New System.Drawing.Point(119, 23)
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
    Me.edAr_coddb.Size = New System.Drawing.Size(197, 20)
    Me.edAr_coddb.TabIndex = 617
    '
    'fmAcquisti
    '
    Me.fmAcquisti.AllowDrop = True
    Me.fmAcquisti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAcquisti.Appearance.Options.UseBackColor = True
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
    Me.fmAcquisti.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.fmAcquisti.Location = New System.Drawing.Point(6, 3)
    Me.fmAcquisti.Name = "fmAcquisti"
    Me.fmAcquisti.Size = New System.Drawing.Size(323, 312)
    Me.fmAcquisti.TabIndex = 600
    Me.fmAcquisti.Text = "Acquisti"
    '
    'lbXx_forn
    '
    Me.lbXx_forn.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_forn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_forn.Location = New System.Drawing.Point(208, 25)
    Me.lbXx_forn.Name = "lbXx_forn"
    Me.lbXx_forn.NTSDbField = ""
    Me.lbXx_forn.Size = New System.Drawing.Size(110, 20)
    Me.lbXx_forn.TabIndex = 627
    Me.lbXx_forn.Tooltip = ""
    Me.lbXx_forn.UseMnemonic = False
    '
    'lbXx_forn2
    '
    Me.lbXx_forn2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_forn2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_forn2.Location = New System.Drawing.Point(208, 49)
    Me.lbXx_forn2.Name = "lbXx_forn2"
    Me.lbXx_forn2.NTSDbField = ""
    Me.lbXx_forn2.Size = New System.Drawing.Size(110, 20)
    Me.lbXx_forn2.TabIndex = 628
    Me.lbXx_forn2.Tooltip = ""
    Me.lbXx_forn2.UseMnemonic = False
    '
    'lbAr_ggant
    '
    Me.lbAr_ggant.AutoSize = True
    Me.lbAr_ggant.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_ggant.Location = New System.Drawing.Point(6, 208)
    Me.lbAr_ggant.Name = "lbAr_ggant"
    Me.lbAr_ggant.NTSDbField = ""
    Me.lbAr_ggant.Size = New System.Drawing.Size(95, 13)
    Me.lbAr_ggant.TabIndex = 623
    Me.lbAr_ggant.Text = "Giorni di:   anticipo"
    Me.lbAr_ggant.Tooltip = ""
    Me.lbAr_ggant.UseMnemonic = False
    '
    'edAr_ggant
    '
    Me.edAr_ggant.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ggant.EditValue = "0"
    Me.edAr_ggant.Location = New System.Drawing.Point(110, 205)
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
    Me.edAr_ggant.Size = New System.Drawing.Size(59, 20)
    Me.edAr_ggant.TabIndex = 625
    '
    'lbAr_ggpost
    '
    Me.lbAr_ggpost.AutoSize = True
    Me.lbAr_ggpost.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_ggpost.Location = New System.Drawing.Point(205, 208)
    Me.lbAr_ggpost.Name = "lbAr_ggpost"
    Me.lbAr_ggpost.NTSDbField = ""
    Me.lbAr_ggpost.Size = New System.Drawing.Size(49, 13)
    Me.lbAr_ggpost.TabIndex = 624
    Me.lbAr_ggpost.Text = "posticipo"
    Me.lbAr_ggpost.Tooltip = ""
    Me.lbAr_ggpost.UseMnemonic = False
    '
    'edAr_ggpost
    '
    Me.edAr_ggpost.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ggpost.EditValue = "0"
    Me.edAr_ggpost.Location = New System.Drawing.Point(260, 205)
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
    Me.lbAr_ggragg.Location = New System.Drawing.Point(212, 182)
    Me.lbAr_ggragg.Name = "lbAr_ggragg"
    Me.lbAr_ggragg.NTSDbField = ""
    Me.lbAr_ggragg.Size = New System.Drawing.Size(42, 13)
    Me.lbAr_ggragg.TabIndex = 619
    Me.lbAr_ggragg.Text = "g. ragg"
    Me.lbAr_ggragg.Tooltip = ""
    Me.lbAr_ggragg.UseMnemonic = False
    '
    'edAr_ggragg
    '
    Me.edAr_ggragg.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAr_ggragg.EditValue = "0"
    Me.edAr_ggragg.Location = New System.Drawing.Point(260, 179)
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
    Me.lbAr_perragg.Location = New System.Drawing.Point(6, 182)
    Me.lbAr_perragg.Name = "lbAr_perragg"
    Me.lbAr_perragg.NTSDbField = ""
    Me.lbAr_perragg.Size = New System.Drawing.Size(75, 13)
    Me.lbAr_perragg.TabIndex = 620
    Me.lbAr_perragg.Text = "Periodo Ragg."
    Me.lbAr_perragg.Tooltip = ""
    Me.lbAr_perragg.UseMnemonic = False
    '
    'cbAr_perragg
    '
    Me.cbAr_perragg.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_perragg.DataSource = Nothing
    Me.cbAr_perragg.DisplayMember = ""
    Me.cbAr_perragg.Location = New System.Drawing.Point(110, 179)
    Me.cbAr_perragg.Name = "cbAr_perragg"
    Me.cbAr_perragg.NTSDbField = ""
    Me.cbAr_perragg.Properties.AutoHeight = False
    Me.cbAr_perragg.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAr_perragg.Properties.DropDownRows = 30
    Me.cbAr_perragg.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAr_perragg.SelectedValue = ""
    Me.cbAr_perragg.Size = New System.Drawing.Size(91, 20)
    Me.cbAr_perragg.TabIndex = 622
    Me.cbAr_perragg.ValueMember = ""
    '
    'lbAr_scomax
    '
    Me.lbAr_scomax.AutoSize = True
    Me.lbAr_scomax.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_scomax.Location = New System.Drawing.Point(212, 130)
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
    Me.lbAr_maxlotto.Location = New System.Drawing.Point(212, 156)
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
    Me.lbAr_scomin.Size = New System.Drawing.Size(81, 13)
    Me.lbAr_scomin.TabIndex = 613
    Me.lbAr_scomin.Text = "Scorta min/max"
    Me.lbAr_scomin.Tooltip = ""
    Me.lbAr_scomin.UseMnemonic = False
    '
    'edAr_scomin
    '
    Me.edAr_scomin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_scomin.EditValue = "0"
    Me.edAr_scomin.Location = New System.Drawing.Point(110, 127)
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
    Me.edAr_scomax.Location = New System.Drawing.Point(229, 127)
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
    Me.lbAr_sublotto.Size = New System.Drawing.Size(92, 13)
    Me.lbAr_sublotto.TabIndex = 609
    Me.lbAr_sublotto.Text = "S.lotto/lotto max."
    Me.lbAr_sublotto.Tooltip = ""
    Me.lbAr_sublotto.UseMnemonic = False
    '
    'edAr_sublotto
    '
    Me.edAr_sublotto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_sublotto.EditValue = "0"
    Me.edAr_sublotto.Location = New System.Drawing.Point(110, 153)
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
    Me.edAr_maxlotto.Location = New System.Drawing.Point(229, 153)
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
    Me.ckAr_ripriord.Location = New System.Drawing.Point(190, 101)
    Me.ckAr_ripriord.Name = "ckAr_ripriord"
    Me.ckAr_ripriord.NTSCheckValue = "S"
    Me.ckAr_ripriord.NTSUnCheckValue = "N"
    Me.ckAr_ripriord.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_ripriord.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_ripriord.Properties.AutoHeight = False
    Me.ckAr_ripriord.Properties.Caption = "Rip. su più fornitori"
    Me.ckAr_ripriord.Size = New System.Drawing.Size(116, 18)
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
    Me.lbAr_minord.Text = "Qta Lotto std pr/ac."
    Me.lbAr_minord.Tooltip = ""
    Me.lbAr_minord.UseMnemonic = False
    '
    'edAr_minord
    '
    Me.edAr_minord.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_minord.EditValue = "0"
    Me.edAr_minord.Location = New System.Drawing.Point(110, 101)
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
    Me.edAr_minord.Size = New System.Drawing.Size(74, 20)
    Me.edAr_minord.TabIndex = 607
    '
    'lbAr_polriord
    '
    Me.lbAr_polriord.AutoSize = True
    Me.lbAr_polriord.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_polriord.Location = New System.Drawing.Point(6, 78)
    Me.lbAr_polriord.Name = "lbAr_polriord"
    Me.lbAr_polriord.NTSDbField = ""
    Me.lbAr_polriord.Size = New System.Drawing.Size(93, 13)
    Me.lbAr_polriord.TabIndex = 604
    Me.lbAr_polriord.Text = "Politica di Riordino"
    Me.lbAr_polriord.Tooltip = ""
    Me.lbAr_polriord.UseMnemonic = False
    '
    'cbAr_polriord
    '
    Me.cbAr_polriord.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_polriord.DataSource = Nothing
    Me.cbAr_polriord.DisplayMember = ""
    Me.cbAr_polriord.Location = New System.Drawing.Point(110, 75)
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
    Me.edAr_forn.Location = New System.Drawing.Point(110, 23)
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
    Me.lbAr_forn.Location = New System.Drawing.Point(6, 26)
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
    Me.edAr_forn2.Location = New System.Drawing.Point(110, 49)
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
    Me.lbAr_forn2.Location = New System.Drawing.Point(6, 52)
    Me.lbAr_forn2.Name = "lbAr_forn2"
    Me.lbAr_forn2.NTSDbField = ""
    Me.lbAr_forn2.Size = New System.Drawing.Size(60, 13)
    Me.lbAr_forn2.TabIndex = 601
    Me.lbAr_forn2.Text = "Fornitore 2"
    Me.lbAr_forn2.Tooltip = ""
    Me.lbAr_forn2.UseMnemonic = False
    '
    'NtsTabPage5
    '
    Me.NtsTabPage5.AllowDrop = True
    Me.NtsTabPage5.Controls.Add(Me.pnTabpag3)
    Me.NtsTabPage5.Enable = True
    Me.NtsTabPage5.Name = "NtsTabPage5"
    Me.NtsTabPage5.Size = New System.Drawing.Size(790, 321)
    Me.NtsTabPage5.Text = "&5 - Altri Dati"
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
    Me.pnTabpag3.Name = "pnTabpag3"
    Me.pnTabpag3.NTSActiveTrasparency = True
    Me.pnTabpag3.Size = New System.Drawing.Size(790, 321)
    Me.pnTabpag3.TabIndex = 0
    Me.pnTabpag3.Text = "NtsPanel1"
    '
    'pnTabpag3Right
    '
    Me.pnTabpag3Right.AllowDrop = True
    Me.pnTabpag3Right.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag3Right.Appearance.Options.UseBackColor = True
    Me.pnTabpag3Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag3Right.Controls.Add(Me.cmdClassifica)
    Me.pnTabpag3Right.Controls.Add(Me.lbXx_codvuo)
    Me.pnTabpag3Right.Controls.Add(Me.lbClassifica)
    Me.pnTabpag3Right.Controls.Add(Me.fmAltriDati)
    Me.pnTabpag3Right.Controls.Add(Me.lbAr_contriva)
    Me.pnTabpag3Right.Controls.Add(Me.lbAr_codvuo)
    Me.pnTabpag3Right.Controls.Add(Me.edAr_codvuo)
    Me.pnTabpag3Right.Controls.Add(Me.edAr_contriva)
    Me.pnTabpag3Right.Controls.Add(Me.lbAr_pesolor)
    Me.pnTabpag3Right.Controls.Add(Me.edAr_pesolor)
    Me.pnTabpag3Right.Controls.Add(Me.lbAr_pesonet)
    Me.pnTabpag3Right.Controls.Add(Me.edAr_pesonet)
    Me.pnTabpag3Right.Controls.Add(Me.lbAr_catlifo)
    Me.pnTabpag3Right.Controls.Add(Me.edAr_catlifo)
    Me.pnTabpag3Right.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag3Right.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag3Right.Location = New System.Drawing.Point(364, 0)
    Me.pnTabpag3Right.Name = "pnTabpag3Right"
    Me.pnTabpag3Right.NTSActiveTrasparency = True
    Me.pnTabpag3Right.Size = New System.Drawing.Size(426, 321)
    Me.pnTabpag3Right.TabIndex = 670
    Me.pnTabpag3Right.Text = "NtsPanel1"
    '
    'cmdClassifica
    '
    Me.cmdClassifica.ImagePath = ""
    Me.cmdClassifica.ImageText = ""
    Me.cmdClassifica.Location = New System.Drawing.Point(14, 256)
    Me.cmdClassifica.Name = "cmdClassifica"
    Me.cmdClassifica.NTSContextMenu = Nothing
    Me.cmdClassifica.Size = New System.Drawing.Size(113, 22)
    Me.cmdClassifica.TabIndex = 683
    Me.cmdClassifica.Text = "Classifica"
    '
    'lbXx_codvuo
    '
    Me.lbXx_codvuo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codvuo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codvuo.Location = New System.Drawing.Point(144, 10)
    Me.lbXx_codvuo.Name = "lbXx_codvuo"
    Me.lbXx_codvuo.NTSDbField = ""
    Me.lbXx_codvuo.Size = New System.Drawing.Size(145, 20)
    Me.lbXx_codvuo.TabIndex = 657
    Me.lbXx_codvuo.Tooltip = ""
    Me.lbXx_codvuo.UseMnemonic = False
    '
    'lbClassifica
    '
    Me.lbClassifica.BackColor = System.Drawing.Color.Transparent
    Me.lbClassifica.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbClassifica.Location = New System.Drawing.Point(14, 281)
    Me.lbClassifica.Name = "lbClassifica"
    Me.lbClassifica.NTSDbField = ""
    Me.lbClassifica.Size = New System.Drawing.Size(320, 29)
    Me.lbClassifica.TabIndex = 682
    Me.lbClassifica.Tooltip = ""
    Me.lbClassifica.UseMnemonic = False
    '
    'fmAltriDati
    '
    Me.fmAltriDati.AllowDrop = True
    Me.fmAltriDati.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAltriDati.Appearance.Options.UseBackColor = True
    Me.fmAltriDati.Controls.Add(Me.ckAr_stainv)
    Me.fmAltriDati.Controls.Add(Me.ckAr_stasche)
    Me.fmAltriDati.Controls.Add(Me.ckAr_geslotti)
    Me.fmAltriDati.Controls.Add(Me.ckAr_inesaur)
    Me.fmAltriDati.Controls.Add(Me.ckAr_pesoca)
    Me.fmAltriDati.Controls.Add(Me.ckAr_stalist)
    Me.fmAltriDati.Controls.Add(Me.ckAr_gestmatr)
    Me.fmAltriDati.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAltriDati.Location = New System.Drawing.Point(35, 86)
    Me.fmAltriDati.Name = "fmAltriDati"
    Me.fmAltriDati.Size = New System.Drawing.Size(254, 164)
    Me.fmAltriDati.TabIndex = 656
    '
    'ckAr_stainv
    '
    Me.ckAr_stainv.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_stainv.Location = New System.Drawing.Point(6, 60)
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
    Me.ckAr_stasche.Location = New System.Drawing.Point(6, 80)
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
    Me.ckAr_geslotti.Location = New System.Drawing.Point(6, 100)
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
    Me.ckAr_inesaur.Location = New System.Drawing.Point(6, 120)
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
    Me.ckAr_pesoca.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckAr_pesoca.Location = New System.Drawing.Point(6, 22)
    Me.ckAr_pesoca.Name = "ckAr_pesoca"
    Me.ckAr_pesoca.NTSCheckValue = "S"
    Me.ckAr_pesoca.NTSUnCheckValue = "N"
    Me.ckAr_pesoca.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_pesoca.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_pesoca.Properties.AutoHeight = False
    Me.ckAr_pesoca.Properties.Caption = "Non proporre le note art.sulle righe docum."
    Me.ckAr_pesoca.Size = New System.Drawing.Size(233, 19)
    Me.ckAr_pesoca.TabIndex = 635
    '
    'ckAr_stalist
    '
    Me.ckAr_stalist.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_stalist.Location = New System.Drawing.Point(6, 40)
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
    Me.ckAr_gestmatr.Location = New System.Drawing.Point(6, 140)
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
    Me.lbAr_contriva.Location = New System.Drawing.Point(161, 38)
    Me.lbAr_contriva.Name = "lbAr_contriva"
    Me.lbAr_contriva.NTSDbField = ""
    Me.lbAr_contriva.Size = New System.Drawing.Size(59, 13)
    Me.lbAr_contriva.TabIndex = 652
    Me.lbAr_contriva.Text = "Contr.IVA:"
    Me.lbAr_contriva.Tooltip = ""
    Me.lbAr_contriva.UseMnemonic = False
    '
    'lbAr_codvuo
    '
    Me.lbAr_codvuo.AutoSize = True
    Me.lbAr_codvuo.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codvuo.Location = New System.Drawing.Point(10, 13)
    Me.lbAr_codvuo.Name = "lbAr_codvuo"
    Me.lbAr_codvuo.NTSDbField = ""
    Me.lbAr_codvuo.Size = New System.Drawing.Size(62, 13)
    Me.lbAr_codvuo.TabIndex = 653
    Me.lbAr_codvuo.Text = "Cod.vuoto:"
    Me.lbAr_codvuo.Tooltip = ""
    Me.lbAr_codvuo.UseMnemonic = False
    '
    'edAr_codvuo
    '
    Me.edAr_codvuo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codvuo.EditValue = "0"
    Me.edAr_codvuo.Location = New System.Drawing.Point(80, 10)
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
    Me.edAr_codvuo.Size = New System.Drawing.Size(60, 20)
    Me.edAr_codvuo.TabIndex = 655
    '
    'edAr_contriva
    '
    Me.edAr_contriva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_contriva.EditValue = ""
    Me.edAr_contriva.Location = New System.Drawing.Point(229, 35)
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
    Me.edAr_contriva.Size = New System.Drawing.Size(60, 20)
    Me.edAr_contriva.TabIndex = 654
    '
    'lbAr_pesolor
    '
    Me.lbAr_pesolor.AutoSize = True
    Me.lbAr_pesolor.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_pesolor.Location = New System.Drawing.Point(10, 63)
    Me.lbAr_pesolor.Name = "lbAr_pesolor"
    Me.lbAr_pesolor.NTSDbField = ""
    Me.lbAr_pesolor.Size = New System.Drawing.Size(61, 13)
    Me.lbAr_pesolor.TabIndex = 648
    Me.lbAr_pesolor.Text = "Peso lordo:"
    Me.lbAr_pesolor.Tooltip = ""
    Me.lbAr_pesolor.UseMnemonic = False
    '
    'edAr_pesolor
    '
    Me.edAr_pesolor.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_pesolor.EditValue = "0"
    Me.edAr_pesolor.Location = New System.Drawing.Point(80, 60)
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
    Me.edAr_pesolor.Size = New System.Drawing.Size(75, 20)
    Me.edAr_pesolor.TabIndex = 650
    '
    'lbAr_pesonet
    '
    Me.lbAr_pesonet.AutoSize = True
    Me.lbAr_pesonet.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_pesonet.Location = New System.Drawing.Point(161, 63)
    Me.lbAr_pesonet.Name = "lbAr_pesonet"
    Me.lbAr_pesonet.NTSDbField = ""
    Me.lbAr_pesonet.Size = New System.Drawing.Size(47, 13)
    Me.lbAr_pesonet.TabIndex = 649
    Me.lbAr_pesonet.Text = "P.netto:"
    Me.lbAr_pesonet.Tooltip = ""
    Me.lbAr_pesonet.UseMnemonic = False
    '
    'edAr_pesonet
    '
    Me.edAr_pesonet.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_pesonet.EditValue = "0"
    Me.edAr_pesonet.Location = New System.Drawing.Point(229, 60)
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
    Me.edAr_pesonet.Size = New System.Drawing.Size(60, 20)
    Me.edAr_pesonet.TabIndex = 651
    '
    'lbAr_catlifo
    '
    Me.lbAr_catlifo.AutoSize = True
    Me.lbAr_catlifo.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_catlifo.Location = New System.Drawing.Point(10, 38)
    Me.lbAr_catlifo.Name = "lbAr_catlifo"
    Me.lbAr_catlifo.NTSDbField = ""
    Me.lbAr_catlifo.Size = New System.Drawing.Size(69, 13)
    Me.lbAr_catlifo.TabIndex = 646
    Me.lbAr_catlifo.Text = "Coeffic. c.f.:"
    Me.lbAr_catlifo.Tooltip = ""
    Me.lbAr_catlifo.UseMnemonic = False
    '
    'edAr_catlifo
    '
    Me.edAr_catlifo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_catlifo.EditValue = "0"
    Me.edAr_catlifo.Location = New System.Drawing.Point(80, 35)
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
    Me.edAr_catlifo.Size = New System.Drawing.Size(59, 20)
    Me.edAr_catlifo.TabIndex = 647
    '
    'pnTabpag3Left
    '
    Me.pnTabpag3Left.AllowDrop = True
    Me.pnTabpag3Left.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag3Left.Appearance.Options.UseBackColor = True
    Me.pnTabpag3Left.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
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
    Me.pnTabpag3Left.Name = "pnTabpag3Left"
    Me.pnTabpag3Left.NTSActiveTrasparency = True
    Me.pnTabpag3Left.Size = New System.Drawing.Size(364, 321)
    Me.pnTabpag3Left.TabIndex = 646
    Me.pnTabpag3Left.Text = "NtsPanel1"
    '
    'lbXx_clascon
    '
    Me.lbXx_clascon.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_clascon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_clascon.Location = New System.Drawing.Point(188, 222)
    Me.lbXx_clascon.Name = "lbXx_clascon"
    Me.lbXx_clascon.NTSDbField = ""
    Me.lbXx_clascon.Size = New System.Drawing.Size(163, 20)
    Me.lbXx_clascon.TabIndex = 680
    Me.lbXx_clascon.Tooltip = ""
    Me.lbXx_clascon.UseMnemonic = False
    '
    'lbXx_claprov
    '
    Me.lbXx_claprov.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_claprov.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_claprov.Location = New System.Drawing.Point(188, 248)
    Me.lbXx_claprov.Name = "lbXx_claprov"
    Me.lbXx_claprov.NTSDbField = ""
    Me.lbXx_claprov.Size = New System.Drawing.Size(163, 20)
    Me.lbXx_claprov.TabIndex = 681
    Me.lbXx_claprov.Tooltip = ""
    Me.lbXx_claprov.UseMnemonic = False
    '
    'lbAr_claprov
    '
    Me.lbAr_claprov.AutoSize = True
    Me.lbAr_claprov.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_claprov.Location = New System.Drawing.Point(13, 251)
    Me.lbAr_claprov.Name = "lbAr_claprov"
    Me.lbAr_claprov.NTSDbField = ""
    Me.lbAr_claprov.Size = New System.Drawing.Size(52, 13)
    Me.lbAr_claprov.TabIndex = 678
    Me.lbAr_claprov.Text = "Cl.provv."
    Me.lbAr_claprov.Tooltip = ""
    Me.lbAr_claprov.UseMnemonic = False
    '
    'edAr_claprov
    '
    Me.edAr_claprov.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_claprov.EditValue = "0"
    Me.edAr_claprov.Location = New System.Drawing.Point(118, 248)
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
    Me.edAr_claprov.TabIndex = 679
    '
    'lbAr_clascon
    '
    Me.lbAr_clascon.AutoSize = True
    Me.lbAr_clascon.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_clascon.Location = New System.Drawing.Point(13, 225)
    Me.lbAr_clascon.Name = "lbAr_clascon"
    Me.lbAr_clascon.NTSDbField = ""
    Me.lbAr_clascon.Size = New System.Drawing.Size(52, 13)
    Me.lbAr_clascon.TabIndex = 676
    Me.lbAr_clascon.Text = "Cl.sconto"
    Me.lbAr_clascon.Tooltip = ""
    Me.lbAr_clascon.UseMnemonic = False
    '
    'edAr_clascon
    '
    Me.edAr_clascon.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_clascon.EditValue = "0"
    Me.edAr_clascon.Location = New System.Drawing.Point(118, 222)
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
    Me.edAr_clascon.TabIndex = 677
    '
    'lbXx_codiva
    '
    Me.lbXx_codiva.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codiva.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codiva.Location = New System.Drawing.Point(188, 40)
    Me.lbXx_codiva.Name = "lbXx_codiva"
    Me.lbXx_codiva.NTSDbField = ""
    Me.lbXx_codiva.Size = New System.Drawing.Size(163, 20)
    Me.lbXx_codiva.TabIndex = 663
    Me.lbXx_codiva.Tooltip = ""
    Me.lbXx_codiva.UseMnemonic = False
    '
    'lbXx_gruppo
    '
    Me.lbXx_gruppo.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_gruppo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_gruppo.Location = New System.Drawing.Point(188, 66)
    Me.lbXx_gruppo.Name = "lbXx_gruppo"
    Me.lbXx_gruppo.NTSDbField = ""
    Me.lbXx_gruppo.Size = New System.Drawing.Size(163, 20)
    Me.lbXx_gruppo.TabIndex = 664
    Me.lbXx_gruppo.Tooltip = ""
    Me.lbXx_gruppo.UseMnemonic = False
    '
    'lbXx_sotgru
    '
    Me.lbXx_sotgru.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_sotgru.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_sotgru.Location = New System.Drawing.Point(188, 92)
    Me.lbXx_sotgru.Name = "lbXx_sotgru"
    Me.lbXx_sotgru.NTSDbField = ""
    Me.lbXx_sotgru.Size = New System.Drawing.Size(163, 20)
    Me.lbXx_sotgru.TabIndex = 665
    Me.lbXx_sotgru.Tooltip = ""
    Me.lbXx_sotgru.UseMnemonic = False
    '
    'lbXx_controp
    '
    Me.lbXx_controp.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_controp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_controp.Location = New System.Drawing.Point(188, 118)
    Me.lbXx_controp.Name = "lbXx_controp"
    Me.lbXx_controp.NTSDbField = ""
    Me.lbXx_controp.Size = New System.Drawing.Size(163, 20)
    Me.lbXx_controp.TabIndex = 666
    Me.lbXx_controp.Tooltip = ""
    Me.lbXx_controp.UseMnemonic = False
    '
    'lbXx_controa
    '
    Me.lbXx_controa.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_controa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_controa.Location = New System.Drawing.Point(188, 144)
    Me.lbXx_controa.Name = "lbXx_controa"
    Me.lbXx_controa.NTSDbField = ""
    Me.lbXx_controa.Size = New System.Drawing.Size(163, 20)
    Me.lbXx_controa.TabIndex = 667
    Me.lbXx_controa.Tooltip = ""
    Me.lbXx_controa.UseMnemonic = False
    '
    'lbXx_contros
    '
    Me.lbXx_contros.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_contros.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_contros.Location = New System.Drawing.Point(188, 170)
    Me.lbXx_contros.Name = "lbXx_contros"
    Me.lbXx_contros.NTSDbField = ""
    Me.lbXx_contros.Size = New System.Drawing.Size(163, 20)
    Me.lbXx_contros.TabIndex = 668
    Me.lbXx_contros.Tooltip = ""
    Me.lbXx_contros.UseMnemonic = False
    '
    'lbXx_famprod
    '
    Me.lbXx_famprod.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_famprod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_famprod.Location = New System.Drawing.Point(188, 196)
    Me.lbXx_famprod.Name = "lbXx_famprod"
    Me.lbXx_famprod.NTSDbField = ""
    Me.lbXx_famprod.Size = New System.Drawing.Size(163, 20)
    Me.lbXx_famprod.TabIndex = 669
    Me.lbXx_famprod.Tooltip = ""
    Me.lbXx_famprod.UseMnemonic = False
    '
    'lbAr_famprod
    '
    Me.lbAr_famprod.AutoSize = True
    Me.lbAr_famprod.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_famprod.Location = New System.Drawing.Point(13, 199)
    Me.lbAr_famprod.Name = "lbAr_famprod"
    Me.lbAr_famprod.NTSDbField = ""
    Me.lbAr_famprod.Size = New System.Drawing.Size(49, 13)
    Me.lbAr_famprod.TabIndex = 661
    Me.lbAr_famprod.Text = "Famiglia:"
    Me.lbAr_famprod.Tooltip = ""
    Me.lbAr_famprod.UseMnemonic = False
    '
    'edAr_famprod
    '
    Me.edAr_famprod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_famprod.EditValue = ""
    Me.edAr_famprod.Location = New System.Drawing.Point(118, 196)
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
    Me.lbAr_contros.Location = New System.Drawing.Point(13, 173)
    Me.lbAr_contros.Name = "lbAr_contros"
    Me.lbAr_contros.NTSDbField = ""
    Me.lbAr_contros.Size = New System.Drawing.Size(104, 13)
    Me.lbAr_contros.TabIndex = 659
    Me.lbAr_contros.Text = "Controp.scar.prod.:"
    Me.lbAr_contros.Tooltip = ""
    Me.lbAr_contros.UseMnemonic = False
    '
    'edAr_contros
    '
    Me.edAr_contros.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_contros.EditValue = "0"
    Me.edAr_contros.Location = New System.Drawing.Point(118, 170)
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
    Me.lbAr_codiva.Location = New System.Drawing.Point(13, 43)
    Me.lbAr_codiva.Name = "lbAr_codiva"
    Me.lbAr_codiva.NTSDbField = ""
    Me.lbAr_codiva.Size = New System.Drawing.Size(63, 13)
    Me.lbAr_codiva.TabIndex = 649
    Me.lbAr_codiva.Text = "Codice IVA:"
    Me.lbAr_codiva.Tooltip = ""
    Me.lbAr_codiva.UseMnemonic = False
    '
    'edAr_codiva
    '
    Me.edAr_codiva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_codiva.EditValue = "0"
    Me.edAr_codiva.Location = New System.Drawing.Point(118, 40)
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
    Me.lbAr_gruppo.Location = New System.Drawing.Point(13, 69)
    Me.lbAr_gruppo.Name = "lbAr_gruppo"
    Me.lbAr_gruppo.NTSDbField = ""
    Me.lbAr_gruppo.Size = New System.Drawing.Size(76, 13)
    Me.lbAr_gruppo.TabIndex = 650
    Me.lbAr_gruppo.Text = "Gruppo merc.:"
    Me.lbAr_gruppo.Tooltip = ""
    Me.lbAr_gruppo.UseMnemonic = False
    '
    'edAr_gruppo
    '
    Me.edAr_gruppo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_gruppo.EditValue = "0"
    Me.edAr_gruppo.Location = New System.Drawing.Point(118, 66)
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
    Me.lbAr_sotgru.Location = New System.Drawing.Point(13, 95)
    Me.lbAr_sotgru.Name = "lbAr_sotgru"
    Me.lbAr_sotgru.NTSDbField = ""
    Me.lbAr_sotgru.Size = New System.Drawing.Size(101, 13)
    Me.lbAr_sotgru.TabIndex = 651
    Me.lbAr_sotgru.Text = "Sottogruppo merc.:"
    Me.lbAr_sotgru.Tooltip = ""
    Me.lbAr_sotgru.UseMnemonic = False
    '
    'edAr_sotgru
    '
    Me.edAr_sotgru.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_sotgru.EditValue = "0"
    Me.edAr_sotgru.Location = New System.Drawing.Point(118, 92)
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
    Me.lbAr_controp.Location = New System.Drawing.Point(13, 121)
    Me.lbAr_controp.Name = "lbAr_controp"
    Me.lbAr_controp.NTSDbField = ""
    Me.lbAr_controp.Size = New System.Drawing.Size(93, 13)
    Me.lbAr_controp.TabIndex = 652
    Me.lbAr_controp.Text = "Controp. vendite:"
    Me.lbAr_controp.Tooltip = ""
    Me.lbAr_controp.UseMnemonic = False
    '
    'edAr_controp
    '
    Me.edAr_controp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_controp.EditValue = "0"
    Me.edAr_controp.Location = New System.Drawing.Point(118, 118)
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
    Me.lbAr_controa.Location = New System.Drawing.Point(13, 147)
    Me.lbAr_controa.Name = "lbAr_controa"
    Me.lbAr_controa.NTSDbField = ""
    Me.lbAr_controa.Size = New System.Drawing.Size(93, 13)
    Me.lbAr_controa.TabIndex = 653
    Me.lbAr_controa.Text = "Controp. acquisti:"
    Me.lbAr_controa.Tooltip = ""
    Me.lbAr_controa.UseMnemonic = False
    '
    'edAr_controa
    '
    Me.edAr_controa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_controa.EditValue = "0"
    Me.edAr_controa.Location = New System.Drawing.Point(118, 144)
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
    Me.lbAr_tipo.Location = New System.Drawing.Point(13, 17)
    Me.lbAr_tipo.Name = "lbAr_tipo"
    Me.lbAr_tipo.NTSDbField = ""
    Me.lbAr_tipo.Size = New System.Drawing.Size(31, 13)
    Me.lbAr_tipo.TabIndex = 645
    Me.lbAr_tipo.Text = "Tipo:"
    Me.lbAr_tipo.Tooltip = ""
    Me.lbAr_tipo.UseMnemonic = False
    '
    'edAr_tipo
    '
    Me.edAr_tipo.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAr_tipo.EditValue = ""
    Me.edAr_tipo.Location = New System.Drawing.Point(118, 14)
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
    Me.edAr_tipo.Size = New System.Drawing.Size(56, 20)
    Me.edAr_tipo.TabIndex = 647
    '
    'lbAr_ubicaz
    '
    Me.lbAr_ubicaz.AutoSize = True
    Me.lbAr_ubicaz.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_ubicaz.Location = New System.Drawing.Point(185, 17)
    Me.lbAr_ubicaz.Name = "lbAr_ubicaz"
    Me.lbAr_ubicaz.NTSDbField = ""
    Me.lbAr_ubicaz.Size = New System.Drawing.Size(62, 13)
    Me.lbAr_ubicaz.TabIndex = 646
    Me.lbAr_ubicaz.Text = "Ubicazione:"
    Me.lbAr_ubicaz.Tooltip = ""
    Me.lbAr_ubicaz.UseMnemonic = False
    '
    'edAr_ubicaz
    '
    Me.edAr_ubicaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_ubicaz.EditValue = ""
    Me.edAr_ubicaz.Location = New System.Drawing.Point(253, 14)
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
    Me.edAr_ubicaz.Size = New System.Drawing.Size(98, 20)
    Me.edAr_ubicaz.TabIndex = 648
    '
    'NtsTabPage6
    '
    Me.NtsTabPage6.AllowDrop = True
    Me.NtsTabPage6.Controls.Add(Me.pnTabpag4)
    Me.NtsTabPage6.Enable = True
    Me.NtsTabPage6.Name = "NtsTabPage6"
    Me.NtsTabPage6.Size = New System.Drawing.Size(790, 321)
    Me.NtsTabPage6.Text = "&6 - Listini"
    '
    'pnTabpag4
    '
    Me.pnTabpag4.AllowDrop = True
    Me.pnTabpag4.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag4.Appearance.Options.UseBackColor = True
    Me.pnTabpag4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag4.Controls.Add(Me.pnListiniBottom)
    Me.pnTabpag4.Controls.Add(Me.pnListiniTop)
    Me.pnTabpag4.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag4.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag4.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag4.Name = "pnTabpag4"
    Me.pnTabpag4.NTSActiveTrasparency = True
    Me.pnTabpag4.Size = New System.Drawing.Size(790, 321)
    Me.pnTabpag4.TabIndex = 0
    Me.pnTabpag4.Text = "NtsPanel1"
    '
    'pnListiniBottom
    '
    Me.pnListiniBottom.AllowDrop = True
    Me.pnListiniBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnListiniBottom.Appearance.Options.UseBackColor = True
    Me.pnListiniBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnListiniBottom.Controls.Add(Me.ceListini)
    Me.pnListiniBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnListiniBottom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnListiniBottom.Location = New System.Drawing.Point(0, 28)
    Me.pnListiniBottom.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnListiniBottom.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnListiniBottom.Name = "pnListiniBottom"
    Me.pnListiniBottom.NTSActiveTrasparency = True
    Me.pnListiniBottom.Size = New System.Drawing.Size(790, 293)
    Me.pnListiniBottom.TabIndex = 2
    Me.pnListiniBottom.Text = "NtsPanel1"
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
    Me.ceListini.Size = New System.Drawing.Size(790, 294)
    Me.ceListini.strNomeCampo = ""
    Me.ceListini.TabIndex = 0
    '
    'pnListiniTop
    '
    Me.pnListiniTop.AllowDrop = True
    Me.pnListiniTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnListiniTop.Appearance.Options.UseBackColor = True
    Me.pnListiniTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnListiniTop.Controls.Add(Me.lbArtListini)
    Me.pnListiniTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnListiniTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnListiniTop.Location = New System.Drawing.Point(0, 0)
    Me.pnListiniTop.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnListiniTop.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnListiniTop.Name = "pnListiniTop"
    Me.pnListiniTop.NTSActiveTrasparency = True
    Me.pnListiniTop.Size = New System.Drawing.Size(790, 28)
    Me.pnListiniTop.TabIndex = 1
    Me.pnListiniTop.Text = "NtsPanel1"
    '
    'lbArtListini
    '
    Me.lbArtListini.BackColor = System.Drawing.Color.Transparent
    Me.lbArtListini.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbArtListini.Location = New System.Drawing.Point(9, 5)
    Me.lbArtListini.Name = "lbArtListini"
    Me.lbArtListini.NTSDbField = ""
    Me.lbArtListini.Size = New System.Drawing.Size(186, 20)
    Me.lbArtListini.TabIndex = 628
    Me.lbArtListini.Tooltip = ""
    Me.lbArtListini.UseMnemonic = False
    '
    'NtsTabPage7
    '
    Me.NtsTabPage7.AllowDrop = True
    Me.NtsTabPage7.Controls.Add(Me.pnTabpag5)
    Me.NtsTabPage7.Enable = True
    Me.NtsTabPage7.Name = "NtsTabPage7"
    Me.NtsTabPage7.Size = New System.Drawing.Size(790, 321)
    Me.NtsTabPage7.Text = "&7 - Sconti"
    '
    'pnTabpag5
    '
    Me.pnTabpag5.AllowDrop = True
    Me.pnTabpag5.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag5.Appearance.Options.UseBackColor = True
    Me.pnTabpag5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag5.Controls.Add(Me.pnScontiBottom)
    Me.pnTabpag5.Controls.Add(Me.pnScontiTop)
    Me.pnTabpag5.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag5.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag5.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag5.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag5.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag5.Name = "pnTabpag5"
    Me.pnTabpag5.NTSActiveTrasparency = True
    Me.pnTabpag5.Size = New System.Drawing.Size(790, 321)
    Me.pnTabpag5.TabIndex = 0
    Me.pnTabpag5.Text = "NtsPanel1"
    '
    'pnScontiBottom
    '
    Me.pnScontiBottom.AllowDrop = True
    Me.pnScontiBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnScontiBottom.Appearance.Options.UseBackColor = True
    Me.pnScontiBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnScontiBottom.Controls.Add(Me.ceSconti)
    Me.pnScontiBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnScontiBottom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnScontiBottom.Location = New System.Drawing.Point(0, 28)
    Me.pnScontiBottom.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnScontiBottom.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnScontiBottom.Name = "pnScontiBottom"
    Me.pnScontiBottom.NTSActiveTrasparency = True
    Me.pnScontiBottom.Size = New System.Drawing.Size(790, 293)
    Me.pnScontiBottom.TabIndex = 3
    Me.pnScontiBottom.Text = "NtsPanel1"
    '
    'ceSconti
    '
    Me.ceSconti.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceSconti.GridColumn1_954_20 = Nothing
    Me.ceSconti.Location = New System.Drawing.Point(0, 0)
    Me.ceSconti.MinimumSize = New System.Drawing.Size(504, 294)
    Me.ceSconti.Name = "ceSconti"
    Me.ceSconti.Size = New System.Drawing.Size(790, 294)
    Me.ceSconti.SoClasseArt = 0
    Me.ceSconti.SoClasseCli = 0
    Me.ceSconti.SoCodart = ""
    Me.ceSconti.SoCodartRoot = ""
    Me.ceSconti.SoConto = 0
    Me.ceSconti.strNomeCampo = ""
    Me.ceSconti.TabIndex = 0
    Me.ceSconti.TipoSconto = 0
    '
    'pnScontiTop
    '
    Me.pnScontiTop.AllowDrop = True
    Me.pnScontiTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnScontiTop.Appearance.Options.UseBackColor = True
    Me.pnScontiTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnScontiTop.Controls.Add(Me.lbArtSconti)
    Me.pnScontiTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnScontiTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnScontiTop.Location = New System.Drawing.Point(0, 0)
    Me.pnScontiTop.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnScontiTop.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnScontiTop.Name = "pnScontiTop"
    Me.pnScontiTop.NTSActiveTrasparency = True
    Me.pnScontiTop.Size = New System.Drawing.Size(790, 28)
    Me.pnScontiTop.TabIndex = 2
    Me.pnScontiTop.Text = "NtsPanel1"
    '
    'lbArtSconti
    '
    Me.lbArtSconti.BackColor = System.Drawing.Color.Transparent
    Me.lbArtSconti.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbArtSconti.Location = New System.Drawing.Point(9, 5)
    Me.lbArtSconti.Name = "lbArtSconti"
    Me.lbArtSconti.NTSDbField = ""
    Me.lbArtSconti.Size = New System.Drawing.Size(186, 20)
    Me.lbArtSconti.TabIndex = 628
    Me.lbArtSconti.Tooltip = ""
    Me.lbArtSconti.UseMnemonic = False
    '
    'NtsTabPage8
    '
    Me.NtsTabPage8.AllowDrop = True
    Me.NtsTabPage8.Controls.Add(Me.pnTabpag6)
    Me.NtsTabPage8.Enable = True
    Me.NtsTabPage8.Name = "NtsTabPage8"
    Me.NtsTabPage8.Size = New System.Drawing.Size(790, 321)
    Me.NtsTabPage8.Text = "&8 - Provvigioni"
    '
    'pnTabpag6
    '
    Me.pnTabpag6.AllowDrop = True
    Me.pnTabpag6.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag6.Appearance.Options.UseBackColor = True
    Me.pnTabpag6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag6.Controls.Add(Me.pnProvvigioniBottom)
    Me.pnTabpag6.Controls.Add(Me.pnProvvigioniTop)
    Me.pnTabpag6.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag6.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag6.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag6.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTabpag6.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTabpag6.Name = "pnTabpag6"
    Me.pnTabpag6.NTSActiveTrasparency = True
    Me.pnTabpag6.Size = New System.Drawing.Size(790, 321)
    Me.pnTabpag6.TabIndex = 0
    Me.pnTabpag6.Text = "NtsPanel1"
    '
    'pnProvvigioniBottom
    '
    Me.pnProvvigioniBottom.AllowDrop = True
    Me.pnProvvigioniBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnProvvigioniBottom.Appearance.Options.UseBackColor = True
    Me.pnProvvigioniBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnProvvigioniBottom.Controls.Add(Me.ceProvvig)
    Me.pnProvvigioniBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnProvvigioniBottom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnProvvigioniBottom.Location = New System.Drawing.Point(0, 28)
    Me.pnProvvigioniBottom.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnProvvigioniBottom.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnProvvigioniBottom.Name = "pnProvvigioniBottom"
    Me.pnProvvigioniBottom.NTSActiveTrasparency = True
    Me.pnProvvigioniBottom.Size = New System.Drawing.Size(790, 293)
    Me.pnProvvigioniBottom.TabIndex = 4
    Me.pnProvvigioniBottom.Text = "NtsPanel1"
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
    Me.ceProvvig.Size = New System.Drawing.Size(790, 294)
    Me.ceProvvig.strNomeCampo = ""
    Me.ceProvvig.TabIndex = 0
    Me.ceProvvig.TipoProvv = 0
    '
    'pnProvvigioniTop
    '
    Me.pnProvvigioniTop.AllowDrop = True
    Me.pnProvvigioniTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnProvvigioniTop.Appearance.Options.UseBackColor = True
    Me.pnProvvigioniTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnProvvigioniTop.Controls.Add(Me.lbArtProvvigioni)
    Me.pnProvvigioniTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnProvvigioniTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnProvvigioniTop.Location = New System.Drawing.Point(0, 0)
    Me.pnProvvigioniTop.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnProvvigioniTop.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnProvvigioniTop.Name = "pnProvvigioniTop"
    Me.pnProvvigioniTop.NTSActiveTrasparency = True
    Me.pnProvvigioniTop.Size = New System.Drawing.Size(790, 28)
    Me.pnProvvigioniTop.TabIndex = 3
    Me.pnProvvigioniTop.Text = "NtsPanel1"
    '
    'lbArtProvvigioni
    '
    Me.lbArtProvvigioni.BackColor = System.Drawing.Color.Transparent
    Me.lbArtProvvigioni.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbArtProvvigioni.Location = New System.Drawing.Point(9, 5)
    Me.lbArtProvvigioni.Name = "lbArtProvvigioni"
    Me.lbArtProvvigioni.NTSDbField = ""
    Me.lbArtProvvigioni.Size = New System.Drawing.Size(186, 20)
    Me.lbArtProvvigioni.TabIndex = 628
    Me.lbArtProvvigioni.Tooltip = ""
    Me.lbArtProvvigioni.UseMnemonic = False
    '
    'NtsTabPage9
    '
    Me.NtsTabPage9.AllowDrop = True
    Me.NtsTabPage9.Controls.Add(Me.pnTabpag7)
    Me.NtsTabPage9.Enable = True
    Me.NtsTabPage9.Name = "NtsTabPage9"
    Me.NtsTabPage9.Size = New System.Drawing.Size(790, 321)
    Me.NtsTabPage9.Text = "&9 - Note"
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
    Me.pnTabpag7.Size = New System.Drawing.Size(790, 321)
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
    Me.edAr_note.Size = New System.Drawing.Size(790, 321)
    Me.edAr_note.TabIndex = 552
    '
    'NtsTabPage10
    '
    Me.NtsTabPage10.AllowDrop = True
    Me.NtsTabPage10.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.NtsTabPage10.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage10.Controls.Add(Me.pnTabpag8)
    Me.NtsTabPage10.Enable = True
    Me.NtsTabPage10.Name = "NtsTabPage10"
    Me.NtsTabPage10.Size = New System.Drawing.Size(790, 321)
    Me.NtsTabPage10.Text = "&10 - Magazzini/U.M."
    '
    'pnTabpag8
    '
    Me.pnTabpag8.AllowDrop = True
    Me.pnTabpag8.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTabpag8.Appearance.Options.UseBackColor = True
    Me.pnTabpag8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTabpag8.Controls.Add(Me.fmLogisticaPalmare)
    Me.pnTabpag8.Controls.Add(Me.cmdArtgif2)
    Me.pnTabpag8.Controls.Add(Me.cmdArtGif1)
    Me.pnTabpag8.Controls.Add(Me.cmdVisGif2)
    Me.pnTabpag8.Controls.Add(Me.cmdVisGif1)
    Me.pnTabpag8.Controls.Add(Me.lbAr_um4)
    Me.pnTabpag8.Controls.Add(Me.edAr_um4)
    Me.pnTabpag8.Controls.Add(Me.lbAr_formula)
    Me.pnTabpag8.Controls.Add(Me.edAr_formula)
    Me.pnTabpag8.Controls.Add(Me.lbAr_umpdapr)
    Me.pnTabpag8.Controls.Add(Me.cbAr_umpdapr)
    Me.pnTabpag8.Controls.Add(Me.lbAr_umpdapra)
    Me.pnTabpag8.Controls.Add(Me.cbAr_umpdapra)
    Me.pnTabpag8.Controls.Add(Me.lbAr_gif1)
    Me.pnTabpag8.Controls.Add(Me.edAr_gif1)
    Me.pnTabpag8.Controls.Add(Me.lbAr_gif2)
    Me.pnTabpag8.Controls.Add(Me.edAr_gif2)
    Me.pnTabpag8.Controls.Add(Me.lbAr_umdapra)
    Me.pnTabpag8.Controls.Add(Me.cbAr_umdapra)
    Me.pnTabpag8.Controls.Add(Me.lbXx_magstock)
    Me.pnTabpag8.Controls.Add(Me.lbXx_magprod)
    Me.pnTabpag8.Controls.Add(Me.lbAr_umdapr)
    Me.pnTabpag8.Controls.Add(Me.cbAr_umdapr)
    Me.pnTabpag8.Controls.Add(Me.lbAr_magstock)
    Me.pnTabpag8.Controls.Add(Me.edAr_magstock)
    Me.pnTabpag8.Controls.Add(Me.lbAr_magprod)
    Me.pnTabpag8.Controls.Add(Me.edAr_magprod)
    Me.pnTabpag8.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTabpag8.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTabpag8.Location = New System.Drawing.Point(0, 0)
    Me.pnTabpag8.Name = "pnTabpag8"
    Me.pnTabpag8.NTSActiveTrasparency = True
    Me.pnTabpag8.Size = New System.Drawing.Size(790, 321)
    Me.pnTabpag8.TabIndex = 0
    Me.pnTabpag8.Text = "NtsPanel1"
    '
    'cmdArtgif2
    '
    Me.cmdArtgif2.ImagePath = ""
    Me.cmdArtgif2.ImageText = ""
    Me.cmdArtgif2.Location = New System.Drawing.Point(334, 192)
    Me.cmdArtgif2.Name = "cmdArtgif2"
    Me.cmdArtgif2.NTSContextMenu = Nothing
    Me.cmdArtgif2.Size = New System.Drawing.Size(25, 20)
    Me.cmdArtgif2.TabIndex = 640
    Me.cmdArtgif2.Text = "..."
    '
    'cmdArtGif1
    '
    Me.cmdArtGif1.ImagePath = ""
    Me.cmdArtGif1.ImageText = ""
    Me.cmdArtGif1.Location = New System.Drawing.Point(334, 166)
    Me.cmdArtGif1.Name = "cmdArtGif1"
    Me.cmdArtGif1.NTSContextMenu = Nothing
    Me.cmdArtGif1.Size = New System.Drawing.Size(25, 20)
    Me.cmdArtGif1.TabIndex = 639
    Me.cmdArtGif1.Text = "..."
    '
    'cmdVisGif2
    '
    Me.cmdVisGif2.ImagePath = ""
    Me.cmdVisGif2.ImageText = ""
    Me.cmdVisGif2.Location = New System.Drawing.Point(423, 188)
    Me.cmdVisGif2.Name = "cmdVisGif2"
    Me.cmdVisGif2.NTSContextMenu = Nothing
    Me.cmdVisGif2.Size = New System.Drawing.Size(110, 24)
    Me.cmdVisGif2.TabIndex = 638
    Me.cmdVisGif2.Text = "Immagine scheda"
    '
    'cmdVisGif1
    '
    Me.cmdVisGif1.ImagePath = ""
    Me.cmdVisGif1.ImageText = ""
    Me.cmdVisGif1.Location = New System.Drawing.Point(423, 160)
    Me.cmdVisGif1.Name = "cmdVisGif1"
    Me.cmdVisGif1.NTSContextMenu = Nothing
    Me.cmdVisGif1.Size = New System.Drawing.Size(110, 26)
    Me.cmdVisGif1.TabIndex = 637
    Me.cmdVisGif1.Text = "Immagine catalogo"
    '
    'lbAr_um4
    '
    Me.lbAr_um4.AutoSize = True
    Me.lbAr_um4.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_um4.Location = New System.Drawing.Point(10, 221)
    Me.lbAr_um4.Name = "lbAr_um4"
    Me.lbAr_um4.NTSDbField = ""
    Me.lbAr_um4.Size = New System.Drawing.Size(69, 13)
    Me.lbAr_um4.TabIndex = 635
    Me.lbAr_um4.Text = "U.M. formula"
    Me.lbAr_um4.Tooltip = ""
    Me.lbAr_um4.UseMnemonic = False
    '
    'edAr_um4
    '
    Me.edAr_um4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_um4.EditValue = ""
    Me.edAr_um4.Location = New System.Drawing.Point(122, 218)
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
    'lbAr_formula
    '
    Me.lbAr_formula.AutoSize = True
    Me.lbAr_formula.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_formula.Location = New System.Drawing.Point(199, 221)
    Me.lbAr_formula.Name = "lbAr_formula"
    Me.lbAr_formula.NTSDbField = ""
    Me.lbAr_formula.Size = New System.Drawing.Size(150, 13)
    Me.lbAr_formula.TabIndex = 633
    Me.lbAr_formula.Text = "Formula di trasformaz. in UMP"
    Me.lbAr_formula.Tooltip = ""
    Me.lbAr_formula.UseMnemonic = False
    '
    'edAr_formula
    '
    Me.edAr_formula.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_formula.EditValue = ""
    Me.edAr_formula.Location = New System.Drawing.Point(355, 218)
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
    Me.edAr_formula.Size = New System.Drawing.Size(284, 20)
    Me.edAr_formula.TabIndex = 634
    '
    'lbAr_umpdapr
    '
    Me.lbAr_umpdapr.AutoSize = True
    Me.lbAr_umpdapr.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_umpdapr.Location = New System.Drawing.Point(10, 117)
    Me.lbAr_umpdapr.Name = "lbAr_umpdapr"
    Me.lbAr_umpdapr.NTSDbField = ""
    Me.lbAr_umpdapr.Size = New System.Drawing.Size(101, 13)
    Me.lbAr_umpdapr.TabIndex = 625
    Me.lbAr_umpdapr.Text = "U.M.prezzo vendita"
    Me.lbAr_umpdapr.Tooltip = ""
    Me.lbAr_umpdapr.UseMnemonic = False
    '
    'cbAr_umpdapr
    '
    Me.cbAr_umpdapr.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_umpdapr.DataSource = Nothing
    Me.cbAr_umpdapr.DisplayMember = ""
    Me.cbAr_umpdapr.Location = New System.Drawing.Point(122, 114)
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
    'lbAr_umpdapra
    '
    Me.lbAr_umpdapra.AutoSize = True
    Me.lbAr_umpdapra.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_umpdapra.Location = New System.Drawing.Point(10, 143)
    Me.lbAr_umpdapra.Name = "lbAr_umpdapra"
    Me.lbAr_umpdapra.NTSDbField = ""
    Me.lbAr_umpdapra.Size = New System.Drawing.Size(105, 13)
    Me.lbAr_umpdapra.TabIndex = 626
    Me.lbAr_umpdapra.Text = "U.M.prezzo acquisto"
    Me.lbAr_umpdapra.Tooltip = ""
    Me.lbAr_umpdapra.UseMnemonic = False
    '
    'cbAr_umpdapra
    '
    Me.cbAr_umpdapra.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_umpdapra.DataSource = Nothing
    Me.cbAr_umpdapra.DisplayMember = ""
    Me.cbAr_umpdapra.Location = New System.Drawing.Point(122, 140)
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
    'lbAr_gif1
    '
    Me.lbAr_gif1.AutoSize = True
    Me.lbAr_gif1.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gif1.Location = New System.Drawing.Point(10, 169)
    Me.lbAr_gif1.Name = "lbAr_gif1"
    Me.lbAr_gif1.NTSDbField = ""
    Me.lbAr_gif1.Size = New System.Drawing.Size(97, 13)
    Me.lbAr_gif1.TabIndex = 627
    Me.lbAr_gif1.Text = "Immagine catalogo"
    Me.lbAr_gif1.Tooltip = ""
    Me.lbAr_gif1.UseMnemonic = False
    '
    'edAr_gif1
    '
    Me.edAr_gif1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_gif1.EditValue = ""
    Me.edAr_gif1.Location = New System.Drawing.Point(122, 166)
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
    'lbAr_gif2
    '
    Me.lbAr_gif2.AutoSize = True
    Me.lbAr_gif2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_gif2.Location = New System.Drawing.Point(10, 195)
    Me.lbAr_gif2.Name = "lbAr_gif2"
    Me.lbAr_gif2.NTSDbField = ""
    Me.lbAr_gif2.Size = New System.Drawing.Size(90, 13)
    Me.lbAr_gif2.TabIndex = 628
    Me.lbAr_gif2.Text = "Immagine scheda"
    Me.lbAr_gif2.Tooltip = ""
    Me.lbAr_gif2.UseMnemonic = False
    '
    'edAr_gif2
    '
    Me.edAr_gif2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_gif2.EditValue = ""
    Me.edAr_gif2.Location = New System.Drawing.Point(122, 192)
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
    'lbAr_umdapra
    '
    Me.lbAr_umdapra.AutoSize = True
    Me.lbAr_umdapra.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_umdapra.Location = New System.Drawing.Point(10, 91)
    Me.lbAr_umdapra.Name = "lbAr_umdapra"
    Me.lbAr_umdapra.NTSDbField = ""
    Me.lbAr_umdapra.Size = New System.Drawing.Size(60, 13)
    Me.lbAr_umdapra.TabIndex = 623
    Me.lbAr_umdapra.Text = "U.M.carichi"
    Me.lbAr_umdapra.Tooltip = ""
    Me.lbAr_umdapra.UseMnemonic = False
    '
    'cbAr_umdapra
    '
    Me.cbAr_umdapra.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_umdapra.DataSource = Nothing
    Me.cbAr_umdapra.DisplayMember = ""
    Me.cbAr_umdapra.Location = New System.Drawing.Point(122, 88)
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
    'lbXx_magstock
    '
    Me.lbXx_magstock.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_magstock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_magstock.Location = New System.Drawing.Point(186, 10)
    Me.lbXx_magstock.Name = "lbXx_magstock"
    Me.lbXx_magstock.NTSDbField = ""
    Me.lbXx_magstock.Size = New System.Drawing.Size(200, 20)
    Me.lbXx_magstock.TabIndex = 621
    Me.lbXx_magstock.Tooltip = ""
    Me.lbXx_magstock.UseMnemonic = False
    '
    'lbXx_magprod
    '
    Me.lbXx_magprod.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_magprod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_magprod.Location = New System.Drawing.Point(186, 36)
    Me.lbXx_magprod.Name = "lbXx_magprod"
    Me.lbXx_magprod.NTSDbField = ""
    Me.lbXx_magprod.Size = New System.Drawing.Size(200, 20)
    Me.lbXx_magprod.TabIndex = 622
    Me.lbXx_magprod.Tooltip = ""
    Me.lbXx_magprod.UseMnemonic = False
    '
    'lbAr_umdapr
    '
    Me.lbAr_umdapr.AutoSize = True
    Me.lbAr_umdapr.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_umdapr.Location = New System.Drawing.Point(10, 65)
    Me.lbAr_umdapr.Name = "lbAr_umdapr"
    Me.lbAr_umdapr.NTSDbField = ""
    Me.lbAr_umdapr.Size = New System.Drawing.Size(66, 13)
    Me.lbAr_umdapr.TabIndex = 567
    Me.lbAr_umdapr.Text = "U.M.vendite"
    Me.lbAr_umdapr.Tooltip = ""
    Me.lbAr_umdapr.UseMnemonic = False
    '
    'cbAr_umdapr
    '
    Me.cbAr_umdapr.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAr_umdapr.DataSource = Nothing
    Me.cbAr_umdapr.DisplayMember = ""
    Me.cbAr_umdapr.Location = New System.Drawing.Point(122, 62)
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
    'lbAr_magstock
    '
    Me.lbAr_magstock.AutoSize = True
    Me.lbAr_magstock.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_magstock.Location = New System.Drawing.Point(10, 13)
    Me.lbAr_magstock.Name = "lbAr_magstock"
    Me.lbAr_magstock.NTSDbField = ""
    Me.lbAr_magstock.Size = New System.Drawing.Size(85, 13)
    Me.lbAr_magstock.TabIndex = 568
    Me.lbAr_magstock.Text = "Mag. stoccaggio"
    Me.lbAr_magstock.Tooltip = ""
    Me.lbAr_magstock.UseMnemonic = False
    '
    'edAr_magstock
    '
    Me.edAr_magstock.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_magstock.EditValue = "0"
    Me.edAr_magstock.Location = New System.Drawing.Point(122, 10)
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
    'lbAr_magprod
    '
    Me.lbAr_magprod.AutoSize = True
    Me.lbAr_magprod.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_magprod.Location = New System.Drawing.Point(10, 39)
    Me.lbAr_magprod.Name = "lbAr_magprod"
    Me.lbAr_magprod.NTSDbField = ""
    Me.lbAr_magprod.Size = New System.Drawing.Size(87, 13)
    Me.lbAr_magprod.TabIndex = 569
    Me.lbAr_magprod.Text = "Mag. produzione"
    Me.lbAr_magprod.Tooltip = ""
    Me.lbAr_magprod.UseMnemonic = False
    '
    'edAr_magprod
    '
    Me.edAr_magprod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_magprod.EditValue = "0"
    Me.edAr_magprod.Location = New System.Drawing.Point(122, 36)
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
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.pnTopLeft)
    Me.pnTop.Controls.Add(Me.fmUnitamisura)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(799, 125)
    Me.pnTop.TabIndex = 575
    Me.pnTop.Text = "NtsPanel1"
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
    Me.pnTopLeft.Size = New System.Drawing.Size(461, 125)
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
    Me.pnTopLeftBut.Location = New System.Drawing.Point(291, 14)
    Me.pnTopLeftBut.Name = "pnTopLeftBut"
    Me.pnTopLeftBut.NTSActiveTrasparency = True
    Me.pnTopLeftBut.Size = New System.Drawing.Size(165, 47)
    Me.pnTopLeftBut.TabIndex = 510
    Me.pnTopLeftBut.Text = "NtsPanel1"
    '
    'cmdCodarfo
    '
    Me.cmdCodarfo.ImagePath = ""
    Me.cmdCodarfo.ImageText = ""
    Me.cmdCodarfo.Location = New System.Drawing.Point(74, 26)
    Me.cmdCodarfo.Name = "cmdCodarfo"
    Me.cmdCodarfo.NTSContextMenu = Nothing
    Me.cmdCodarfo.Size = New System.Drawing.Size(91, 22)
    Me.cmdCodarfo.TabIndex = 602
    Me.cmdCodarfo.Text = "&Codice art. C/F"
    '
    'cmdProgressivi
    '
    Me.cmdProgressivi.ImagePath = ""
    Me.cmdProgressivi.ImageText = ""
    Me.cmdProgressivi.Location = New System.Drawing.Point(0, 26)
    Me.cmdProgressivi.Name = "cmdProgressivi"
    Me.cmdProgressivi.NTSContextMenu = Nothing
    Me.cmdProgressivi.Size = New System.Drawing.Size(68, 22)
    Me.cmdProgressivi.TabIndex = 601
    Me.cmdProgressivi.Text = "Progressivi"
    '
    'cmdValuta
    '
    Me.cmdValuta.ImagePath = ""
    Me.cmdValuta.ImageText = ""
    Me.cmdValuta.Location = New System.Drawing.Point(74, 0)
    Me.cmdValuta.Name = "cmdValuta"
    Me.cmdValuta.NTSContextMenu = Nothing
    Me.cmdValuta.Size = New System.Drawing.Size(91, 22)
    Me.cmdValuta.TabIndex = 600
    Me.cmdValuta.Text = "Descr. in ling&ua"
    '
    'cmdProgtot
    '
    Me.cmdProgtot.ImagePath = ""
    Me.cmdProgtot.ImageText = ""
    Me.cmdProgtot.Location = New System.Drawing.Point(0, 0)
    Me.cmdProgtot.Name = "cmdProgtot"
    Me.cmdProgtot.NTSContextMenu = Nothing
    Me.cmdProgtot.Size = New System.Drawing.Size(68, 22)
    Me.cmdProgtot.TabIndex = 599
    Me.cmdProgtot.Text = "Prog. &totali"
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
    Me.edAr_desint.Size = New System.Drawing.Size(353, 20)
    Me.edAr_desint.TabIndex = 594
    '
    'lbAr_codalt
    '
    Me.lbAr_codalt.AutoSize = True
    Me.lbAr_codalt.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_codalt.Location = New System.Drawing.Point(6, 43)
    Me.lbAr_codalt.Name = "lbAr_codalt"
    Me.lbAr_codalt.NTSDbField = ""
    Me.lbAr_codalt.Size = New System.Drawing.Size(78, 13)
    Me.lbAr_codalt.TabIndex = 589
    Me.lbAr_codalt.Text = "Codice altern.:"
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
    Me.lbAr_descr.Size = New System.Drawing.Size(65, 13)
    Me.lbAr_descr.TabIndex = 590
    Me.lbAr_descr.Text = "Descrizione:"
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
    Me.edAr_descr.Size = New System.Drawing.Size(353, 20)
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
    Me.fmUnitamisura.Location = New System.Drawing.Point(467, 6)
    Me.fmUnitamisura.Name = "fmUnitamisura"
    Me.fmUnitamisura.Size = New System.Drawing.Size(251, 112)
    Me.fmUnitamisura.TabIndex = 588
    Me.fmUnitamisura.Text = "Unità di misura"
    '
    'edAr_unmis
    '
    Me.edAr_unmis.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_unmis.EditValue = ""
    Me.edAr_unmis.Location = New System.Drawing.Point(81, 22)
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
    Me.lbAr_unmis.Location = New System.Drawing.Point(13, 25)
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
    Me.lbAr_conver.Location = New System.Drawing.Point(163, 25)
    Me.lbAr_conver.Name = "lbAr_conver"
    Me.lbAr_conver.NTSDbField = ""
    Me.lbAr_conver.Size = New System.Drawing.Size(49, 13)
    Me.lbAr_conver.TabIndex = 17
    Me.lbAr_conver.Text = "Quantità"
    Me.lbAr_conver.Tooltip = ""
    Me.lbAr_conver.UseMnemonic = False
    '
    'lbAr_unmis2
    '
    Me.lbAr_unmis2.AutoSize = True
    Me.lbAr_unmis2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_unmis2.Location = New System.Drawing.Point(11, 77)
    Me.lbAr_unmis2.Name = "lbAr_unmis2"
    Me.lbAr_unmis2.NTSDbField = ""
    Me.lbAr_unmis2.Size = New System.Drawing.Size(64, 13)
    Me.lbAr_unmis2.TabIndex = 16
    Me.lbAr_unmis2.Text = "Secondaria:"
    Me.lbAr_unmis2.Tooltip = ""
    Me.lbAr_unmis2.UseMnemonic = False
    '
    'edAr_conver
    '
    Me.edAr_conver.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_conver.EditValue = "0"
    Me.edAr_conver.Location = New System.Drawing.Point(144, 74)
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
    Me.edAr_conver.Size = New System.Drawing.Size(100, 20)
    Me.edAr_conver.TabIndex = 507
    '
    'edAr_confez2
    '
    Me.edAr_confez2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_confez2.EditValue = ""
    Me.edAr_confez2.Location = New System.Drawing.Point(81, 48)
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
    Me.edAr_unmis2.Location = New System.Drawing.Point(81, 74)
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
    Me.edAr_qtacon2.Location = New System.Drawing.Point(144, 48)
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
    Me.edAr_qtacon2.Size = New System.Drawing.Size(100, 20)
    Me.edAr_qtacon2.TabIndex = 509
    '
    'lbAr_confez2
    '
    Me.lbAr_confez2.AutoSize = True
    Me.lbAr_confez2.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_confez2.Location = New System.Drawing.Point(10, 51)
    Me.lbAr_confez2.Name = "lbAr_confez2"
    Me.lbAr_confez2.NTSDbField = ""
    Me.lbAr_confez2.Size = New System.Drawing.Size(65, 13)
    Me.lbAr_confez2.TabIndex = 18
    Me.lbAr_confez2.Text = "Confezione:"
    Me.lbAr_confez2.Tooltip = ""
    Me.lbAr_confez2.UseMnemonic = False
    '
    'edFocus
    '
    Me.edFocus.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edFocus.EditValue = ""
    Me.edFocus.Location = New System.Drawing.Point(-10000, 0)
    Me.edFocus.Name = "edFocus"
    Me.edFocus.NTSDbField = ""
    Me.edFocus.NTSForzaVisZoom = False
    Me.edFocus.NTSOldValue = ""
    Me.edFocus.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFocus.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFocus.Properties.AutoHeight = False
    Me.edFocus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFocus.Properties.MaxLength = 65536
    Me.edFocus.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFocus.Size = New System.Drawing.Size(10, 20)
    Me.edFocus.TabIndex = 504
    '
    'fmLogisticaPalmare
    '
    Me.fmLogisticaPalmare.AllowDrop = True
    Me.fmLogisticaPalmare.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmLogisticaPalmare.Appearance.Options.UseBackColor = True
    Me.fmLogisticaPalmare.Controls.Add(Me.lbAr_converp)
    Me.fmLogisticaPalmare.Controls.Add(Me.edAr_converp)
    Me.fmLogisticaPalmare.Controls.Add(Me.ckAr_staetip)
    Me.fmLogisticaPalmare.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmLogisticaPalmare.Location = New System.Drawing.Point(11, 243)
    Me.fmLogisticaPalmare.Name = "fmLogisticaPalmare"
    Me.fmLogisticaPalmare.Size = New System.Drawing.Size(420, 69)
    Me.fmLogisticaPalmare.TabIndex = 643
    Me.fmLogisticaPalmare.Text = "LOGISTICA DI MAGAZZINO SU PALMARE"
    '
    'lbAr_converp
    '
    Me.lbAr_converp.AutoSize = True
    Me.lbAr_converp.BackColor = System.Drawing.Color.Transparent
    Me.lbAr_converp.Location = New System.Drawing.Point(5, 49)
    Me.lbAr_converp.Name = "lbAr_converp"
    Me.lbAr_converp.NTSDbField = ""
    Me.lbAr_converp.Size = New System.Drawing.Size(116, 13)
    Me.lbAr_converp.TabIndex = 9
    Me.lbAr_converp.Text = "Rapporto di conv. UdC"
    Me.lbAr_converp.Tooltip = ""
    Me.lbAr_converp.UseMnemonic = False
    '
    'edAr_converp
    '
    Me.edAr_converp.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAr_converp.EditValue = "0"
    Me.edAr_converp.Enabled = False
    Me.edAr_converp.Location = New System.Drawing.Point(127, 46)
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
    Me.edAr_converp.Size = New System.Drawing.Size(75, 20)
    Me.edAr_converp.TabIndex = 8
    '
    'ckAr_staetip
    '
    Me.ckAr_staetip.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAr_staetip.Location = New System.Drawing.Point(5, 22)
    Me.ckAr_staetip.Name = "ckAr_staetip"
    Me.ckAr_staetip.NTSCheckValue = "S"
    Me.ckAr_staetip.NTSUnCheckValue = "N"
    Me.ckAr_staetip.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAr_staetip.Properties.Appearance.Options.UseBackColor = True
    Me.ckAr_staetip.Properties.AutoHeight = False
    Me.ckAr_staetip.Properties.Caption = "Stampa etichette unità di carico quando vengono generate"
    Me.ckAr_staetip.Size = New System.Drawing.Size(305, 19)
    Me.ckAr_staetip.TabIndex = 7
    '
    'FRMMGARTV
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(799, 506)
    Me.Controls.Add(Me.pnArtv)
    Me.Controls.Add(Me.edFocus)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGARTV"
    Me.Text = "ANAGRAFICA ARTICOLI CON VARIANTI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnArtv, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnArtv.ResumeLayout(False)
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMain.ResumeLayout(False)
    CType(Me.tsArtv, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsArtv.ResumeLayout(False)
    Me.NtsTabPage11.ResumeLayout(False)
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
    CType(Me.ckAr_gesubic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_flmod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_volume.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codimba.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_misura1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_misura2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_misura3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnTabpagElencoVar, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpagElencoVar.ResumeLayout(False)
    Me.pnTabpagElencoVar.PerformLayout()
    CType(Me.grVar3, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvVar3, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grVar2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvVar2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grVar1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvVar1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnTabpagAnaliticoVar, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpagAnaliticoVar.ResumeLayout(False)
    CType(Me.grArtico, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvArtico, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
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
    Me.NtsTabPage4.ResumeLayout(False)
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
    Me.NtsTabPage5.ResumeLayout(False)
    CType(Me.pnTabpag3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag3.ResumeLayout(False)
    CType(Me.pnTabpag3Right, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag3Right.ResumeLayout(False)
    Me.pnTabpag3Right.PerformLayout()
    CType(Me.fmAltriDati, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAltriDati.ResumeLayout(False)
    Me.fmAltriDati.PerformLayout()
    CType(Me.ckAr_stainv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_stasche.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_geslotti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_inesaur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_pesoca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_stalist.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_gestmatr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_codvuo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_contriva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Me.NtsTabPage6.ResumeLayout(False)
    CType(Me.pnTabpag4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag4.ResumeLayout(False)
    CType(Me.pnListiniBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnListiniBottom.ResumeLayout(False)
    CType(Me.pnListiniTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnListiniTop.ResumeLayout(False)
    Me.NtsTabPage7.ResumeLayout(False)
    CType(Me.pnTabpag5, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag5.ResumeLayout(False)
    CType(Me.pnScontiBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnScontiBottom.ResumeLayout(False)
    CType(Me.pnScontiTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnScontiTop.ResumeLayout(False)
    Me.NtsTabPage8.ResumeLayout(False)
    CType(Me.pnTabpag6, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag6.ResumeLayout(False)
    CType(Me.pnProvvigioniBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnProvvigioniBottom.ResumeLayout(False)
    CType(Me.pnProvvigioniTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnProvvigioniTop.ResumeLayout(False)
    Me.NtsTabPage9.ResumeLayout(False)
    CType(Me.pnTabpag7, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag7.ResumeLayout(False)
    CType(Me.edAr_note.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage10.ResumeLayout(False)
    CType(Me.pnTabpag8, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTabpag8.ResumeLayout(False)
    Me.pnTabpag8.PerformLayout()
    CType(Me.edAr_um4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_formula.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_umpdapr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_umpdapra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_gif1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_gif2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_umdapra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAr_umdapr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_magstock.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAr_magprod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    CType(Me.pnTopLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTopLeft.ResumeLayout(False)
    Me.pnTopLeft.PerformLayout()
    CType(Me.pnTopLeftBut, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTopLeftBut.ResumeLayout(False)
    CType(Me.edAr_desint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    CType(Me.fmLogisticaPalmare, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmLogisticaPalmare.ResumeLayout(False)
    Me.fmLogisticaPalmare.PerformLayout()
    CType(Me.edAr_converp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAr_staetip.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGARTV", "BEMGARTV", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128550728307822408, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleArtv = CType(oTmp, CLEMGARTV)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGARTV", strRemoteServer, strRemotePort)
    AddHandler oCleArtv.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleArtv.Init(oApp, oScript, oMenu.oCleComm, "ARTVCO", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub CaricaCombo()
    Dim dttAr_perragg As New DataTable()
    Dim dttAr_flricmar As New DataTable()
    Dim dttAr_tipokit As New DataTable()
    Dim dttAr_umintra2 As New DataTable()
    Dim dttAr_polriord As New DataTable()
    Dim dttAr_tipoopz As New DataTable()
    Dim dttAr_gescomm As New DataTable()
    Dim dttAr_umdapr As New DataTable()
    Dim dttAr_umdapra As New DataTable()
    Dim dttAr_umpdapr As New DataTable()
    Dim dttAr_umpdapra As New DataTable()
    Dim dttAr_tipscarlotx As New DataTable()
    Dim dttAr_deterior As New DataTable

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
        tlbRecordNuovo.GlyphPath = (oApp.ChildImageDir & "\recnew.gif")
        tlbRecordAggiorna.GlyphPath = (oApp.ChildImageDir & "\recagg.gif")
        tlbRecordRipristina.GlyphPath = (oApp.ChildImageDir & "\recrestore.gif")
        tlbRecordCancella.GlyphPath = (oApp.ChildImageDir & "\recdelete.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbBarcode.GlyphPath = (oApp.ChildImageDir & "\barcode.gif")
        tlbKit.GlyphPath = (oApp.ChildImageDir & "\kit_1.gif")
        tlbOle.GlyphPath = (oApp.ChildImageDir & "\ole_1.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      If oCleArtv.bConver9Dec = False Then
        strFormatAr_conver = oApp.FormatQta
        nLungMaxAr_conver = 12
      Else
        strFormatAr_conver = "#,##0.000000000"
        nLungMaxAr_conver = 18
      End If

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edAr_codart.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802008441037651, "Codice articolo"), tabartico, False)
      edAr_codalt.NTSSetParam(oMenu, oApp.Tr(Me, 128802008460724647, "Codice altern.:"), CLN__STD.CodartMaxLen, True)
      edAr_descr.NTSSetParam(oMenu, oApp.Tr(Me, 128802008489005173, "Descrizione:"), 40, False)
      edAr_desint.NTSSetParam(oMenu, oApp.Tr(Me, 128802008503536051, "Descrizione2"), 40, True)
      edAr_tipo.NTSSetParam(oMenu, oApp.Tr(Me, 128802008520566865, "Tipo:"), 1, True)
      edAr_unmis.NTSSetParam(oMenu, oApp.Tr(Me, 128802008536816449, "Un. misura principale"), 3, True)
      edAr_unmis2.NTSSetParam(oMenu, oApp.Tr(Me, 128802008555253477, "Un. misura secondaria:"), 3, True)
      edAr_conver.NTSSetParam(oMenu, oApp.Tr(Me, 128802008569628109, "Quantità secondaria"), strFormatAr_conver, nLungMaxAr_conver, 0, 99999999)
      edAr_confez2.NTSSetParam(oMenu, oApp.Tr(Me, 128802008593689993, "Un. misura confezione:"), 3, True)
      edAr_qtacon2.NTSSetParam(oMenu, oApp.Tr(Me, 128802008608377117, "Quantità confezione"), oApp.FormatQta, 6, 0, 99999999)
      edAr_forn.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802008622907995, "Fornitore 1"), tabanagraf)
      edAr_forn2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802008638532595, "Fornitore 2"), tabanagraf)
      edAr_codiva.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802008654782179, "Codice IVA:"), tabciva)
      edAr_gruppo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802008670250533, "Gruppo merc.:"), tabgmer)
      edAr_sotgru.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802008685875133, "Sottogruppo merc.:"), tabsgme)
      edAr_controp.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802008703062193, "Controp. vendite:"), tabcove)
      edAr_catlifo.NTSSetParam(oMenu, oApp.Tr(Me, 128802008720874237, "Coeff. ass. c.f.:"), "0", 4, 0, 9999)
      edAr_ubicaz.NTSSetParam(oMenu, oApp.Tr(Me, 128802008749935993, "Ubicazione:"), 18, True)
      edAr_scomin.NTSSetParam(oMenu, oApp.Tr(Me, 128802008766654315, "Scorta min"), oApp.FormatQta, 13, 0, 9999999999)
      edAr_scomax.NTSSetParam(oMenu, oApp.Tr(Me, 128802008778372765, "Scorta max"), oApp.FormatQta, 13, 0, 9999999999)
      edAr_minord.NTSSetParam(oMenu, oApp.Tr(Me, 128802008795716071, "Qta Lotto std pr/ac."), oApp.FormatQta, 13, 0, 9999999999)
      edAr_ggrior.NTSSetParam(oMenu, oApp.Tr(Me, 128802008846652267, "Non usato 1"), "0", 3, 0, 999)
      edAr_sostit.NTSSetParam(oMenu, oApp.Tr(Me, 128802008869151691, "Art.sostitutivo:"), CLN__STD.CodartMaxLen, True)
      edAr_controa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802008923369053, "Controp. acquisti:"), tabcove)
      edAr_reparto.NTSSetParam(oMenu, oApp.Tr(Me, 128802008906963223, "Rep. (ECR):"), "0", 3, 0, 999)
      ckAr_stalist.NTSSetParam(oMenu, oApp.Tr(Me, 128802008944931001, "Stampa articolo nel listino"), "S", "N")
      edAr_contriva.NTSSetParam(oMenu, oApp.Tr(Me, 128802008963993013, "Contr.IVA:"), 3, True)
      edAr_famprod.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802008982898779, "Famiglia:"), tabcfam, False)
      edAr_numecr.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802008996804673, "Centro C.A.:"), tabcena)
      edAr_codvuo.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802009011960535, "Cod.vuoto:"), tabcvuo)
      cbAr_flricmar.NTSSetParam(oApp.Tr(Me, 128802009031491285, "Ricarico/Margine:"))
      edAr_codpdon.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802009050240805, "Relazione listini:"), tabpdon)
      edAr_ricar1.NTSSetParam(oMenu, oApp.Tr(Me, 128802009064615437, "Ricarico/Margine1"), oApp.FormatImporti, 8, 0, 99999999)
      edAr_ricar2.NTSSetParam(oMenu, oApp.Tr(Me, 128802009110083023, "Ricarico/Margine2"), oApp.FormatImporti, 8, 0, 99999999)
      edAr_garacq.NTSSetParam(oMenu, oApp.Tr(Me, 128802009125551377, "Mesi gar.acq."), "0", 3, 0, 999)
      edAr_garven.NTSSetParam(oMenu, oApp.Tr(Me, 128802009141800961, "Mesi gar.vend."), "0", 3, 0, 999)
      edAr_prorig.NTSSetParam(oMenu, oApp.Tr(Me, 128802009155394363, "Prov Origine"), 2, True)
      edAr_percvst.NTSSetParam(oMenu, oApp.Tr(Me, 128802009171331455, "% Val.statistico:"), oApp.FormatSconti, 9, 0, 999999999)
      edAr_codnomc.NTSSetParam(oMenu, oApp.Tr(Me, 128802009186174825, "Codice nom.comb.:"), 10, True)
      edAr_pesolor.NTSSetParam(oMenu, oApp.Tr(Me, 128788388341543283, "Peso lordo:"), "#,##0.000000", 13, 0, 999999)
      edAr_pesonet.NTSSetParam(oMenu, oApp.Tr(Me, 128788388357169083, "P.netto:"), "#,##0.000000", 13, 0, 999999)
      edAr_paeorig.NTSSetParam(oMenu, oApp.Tr(Me, 128802009247110739, "Paese Origine"), 3, True)
      edAr_livmindb.NTSSetParam(oMenu, oApp.Tr(Me, 128802009267891191, "Liv. Min."), "0", 3, 0, 999)
      edAr_coddb.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802009283046859, "Cod. Distinta Base"), tabdistbas, True)
      ckAr_stainv.NTSSetParam(oMenu, oApp.Tr(Me, 128802009299921211, "Stampa articolo nell'inventario"), "S", "N")
      ckAr_stasche.NTSSetParam(oMenu, oApp.Tr(Me, 128802009315545611, "Stampa scheda articolo"), "S", "N")
      ckAr_geslotti.NTSSetParam(oMenu, oApp.Tr(Me, 128802009331170011, "Gestione Lotti"), "S", "N")
      ckAr_inesaur.NTSSetParam(oMenu, oApp.Tr(Me, 128802009341950847, "In Esaurimento"), "S", "N")
      cbAr_tipoopz.NTSSetParam(oApp.Tr(Me, 128802009378824431, "Tipo Opzione"))
      cbAr_polriord.NTSSetParam(oApp.Tr(Me, 128802009392417659, "Politica di Riordino"))
      cbAr_gescomm.NTSSetParam(oApp.Tr(Me, 128802009407573327, "Gestione per Comm."))
      edAr_fpfence.NTSSetParam(oMenu, oApp.Tr(Me, 128802009429134999, "Non usato 2"), "0", 3, 0, 999)
      edAr_rrfence.NTSSetParam(oMenu, oApp.Tr(Me, 128802009446946815, "RR Fence"), "0", 3, 0, 999)
      edAr_sostituito.NTSSetParam(oMenu, oApp.Tr(Me, 128802009461633751, "Art.sostituito:"), CLN__STD.CodartMaxLen, True)
      ckAr_critico.NTSSetParam(oMenu, oApp.Tr(Me, 128802009477414395, "&Componente critico"), "S", "N")
      edAr_formula.NTSSetParam(oMenu, oApp.Tr(Me, 128802009493195039, "Formula di trasformaz. in UMP"), 0, True)
      edAr_contros.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802009508975683, "Controp.scar.prod.:"), tabcove)
      ckAr_pesoca.NTSSetParam(oMenu, oApp.Tr(Me, 128802009538349555, "Non proporre le note art.sulle righe docum."), "1", "0")
      ckAr_gestmatr.NTSSetParam(oMenu, oApp.Tr(Me, 128802009554911419, "Gestione matricole"), "S", "N")
      cbAr_umdapr.NTSSetParam(oApp.Tr(Me, 128802009572410747, "U.M.vendite"))
      edAr_magstock.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802009588347635, "Mag. stoccaggio"), tabmaga)
      edAr_magprod.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802009603972035, "Mag. produzione"), tabmaga)
      cbAr_umintra2.NTSSetParam(oApp.Tr(Me, 128802009618033995, "UM secondaria:"))
      edAr_codappr.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802009635220835, "Approvvigionatore:"), tabappr)
      edAr_codmarc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802009648032843, "Marca:"), tabmarc)
      cbAr_tipokit.NTSSetParam(oApp.Tr(Me, 128802009672094419, "Tipo kit:"))
      edAr_perqta.NTSSetParam(oMenu, oApp.Tr(Me, 128802009689749991, "Molt.qtà/prezzo:"), "0", 12, 0, 999999999999)
      edAr_fcorrlt.NTSSetParam(oMenu, oApp.Tr(Me, 128802009703499463, "Fattore correz. L.T."), "#,##0.000000000", 10, 0, NTSCDec(9999.99999))
      edAr_verdb.NTSSetParam(oMenu, oApp.Tr(Me, 128802009744279147, "Vers. Distinta Base"), "0", 4, 0, 9999)
      ckAr_blocco.NTSSetParam(oMenu, oApp.Tr(Me, 128802009756622423, "&Blocco"), "S", "N")
      edAr_um4.NTSSetParam(oMenu, oApp.Tr(Me, 128802009771778091, "U.M. formula"), 3, True)
      cbAr_umdapra.NTSSetParam(oApp.Tr(Me, 128802009789433663, "U.M.carichi"))
      cbAr_umpdapr.NTSSetParam(oApp.Tr(Me, 128802009816620119, "U.M.prezzo vendita"))
      cbAr_umpdapra.NTSSetParam(oApp.Tr(Me, 128802009838963011, "U.M.prezzo acquisto"))
      edAr_gif1.NTSSetParam(oMenu, oApp.Tr(Me, 128802009866305711, "Immagine catalogo"), 50, True)
      edAr_gif2.NTSSetParam(oMenu, oApp.Tr(Me, 128802009882398843, "Immagine scheda"), 50, True)
      edAr_ggant.NTSSetParam(oMenu, oApp.Tr(Me, 128802009899273195, "Giorni di tolleranza Anticipo M.R.P."), "0", 3, 0, 999)
      edAr_ggpost.NTSSetParam(oMenu, oApp.Tr(Me, 128802009913178911, "Giorni di tolleranza Posticipo M.R.P."), "0", 3, 0, 999)
      edAr_codimba.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802009926459651, "Imballo"), tabimba)
      edAr_misura1.NTSSetParam(oMenu, oApp.Tr(Me, 128802009942084051, "Misure 1"), "#,##0.000000000", 12, 0, 1000000000000)
      edAr_misura2.NTSSetParam(oMenu, oApp.Tr(Me, 128802009957395963, "Misure 2"), "#,##0.000000000", 12, 0, 1000000000000)
      edAr_misura3.NTSSetParam(oMenu, oApp.Tr(Me, 128802009986144859, "Misure 3"), "#,##0.000000000", 12, 0, 1000000000000)
      ckAr_gesubic.NTSSetParam(oMenu, oApp.Tr(Me, 128802010003956675, "Gestione ubicazione"), "S", "N")
      edAr_volume.NTSSetParam(oMenu, oApp.Tr(Me, 128802010019893563, "Volume"), oApp.FormatTempi, 8, 0, 999999999999)
      edAr_sublotto.NTSSetParam(oMenu, oApp.Tr(Me, 128802010033799279, "Sottolotto"), oApp.FormatQta, 13, 0, 9999999999)
      edAr_maxlotto.NTSSetParam(oMenu, oApp.Tr(Me, 128802010046923775, "lotto max."), oApp.FormatQta, 13, 0, 9999999999)
      edAr_ggragg.NTSSetParam(oMenu, oApp.Tr(Me, 128802010061766955, "g. ragg"), "0", 3, 0, 999)
      ckAr_ripriord.NTSSetParam(oMenu, oApp.Tr(Me, 128802010078328819, "Rip. su più fornitori"), "S", "N")
      cbAr_perragg.NTSSetParam(oApp.Tr(Me, 128802010091609559, "Periodo Ragg."))
      edAr_flmod.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802010150982279, "Cod. variante 1"), tabvari, True)
      edAr_note.NTSSetParam(oMenu, oApp.Tr(Me, 128802010164419263, "Note"), 0, True)
      edAr_claprov.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802010179731175, "Cl.provv."), tabcpar)
      edAr_clascon.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128802010203324019, "Cl.sconto"), tabcsar)
      edAr_coddicv.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129321223519798002, "Valore aggregazione budget"), tabdicv, True)
      edAr_coddica.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129321223519954250, "Aggregazione budget"), tabdica, True)
      edAr_codtcdc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129321223520266746, "Tipologia entità"), tabtcdc)
      ckAr_webvis.NTSSetParam(oMenu, oApp.Tr(Me, 130415037778373729, "Articolo visibile dall'applicazione esterna"), "S", "N")
      ckAr_webusat.NTSSetParam(oMenu, oApp.Tr(Me, 130415038096848244, "Articolo usato"), "S", "N")
      ckAr_webvend.NTSSetParam(oMenu, oApp.Tr(Me, 130415038285040564, "Articolo vendibile da applicazione esterna"), "S", "N")
      edAr_codseat.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130415038617667484, "Codice Set di Attributi"), tabseat)
      ckAr_consmrp.NTSSetParam(oMenu, oApp.Tr(Me, 128788388479362039, "Considera in MRP/Distinta Base"), "S", "N")
      cbAr_deterior.NTSSetParam(oApp.Tr(Me, 130951839641336884, "Articolo deteriorabile"))
      ckAr_staetip.NTSSetParam(oMenu, oApp.Tr(Me, 128842551695483520, "Stampa etichette unità di carico quando vengono generate"), "S", "N")
      edAr_converp.NTSSetParam(oMenu, oApp.Tr(Me, 128842551695327266, "Rapporto di conversione"), "0.00", 9, 0, 999999999)

      'griglie
      grvVar3.NTSSetParam(oMenu, oApp.Tr(Me, 128802010452081257, "Livello 3"))
      grvVar2.NTSSetParam(oMenu, oApp.Tr(Me, 128802010475521307, "Livello 2"))
      grvVar1.NTSSetParam(oMenu, oApp.Tr(Me, 128802010491148007, "Livello 1"))
      xx_seleziona1.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128802010505524571, "Seleziona"), "S", "N")
      xx_codvar1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802010429735076, "Cod."), 0, True)
      xx_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802010558655351, "Descr."), 40, True)
      xx_codditt1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802010579595129, "Codice ditta"), 0, False)
      xx_seleziona2.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128802011142312596, "Seleziona"), "S", "N")
      xx_codvar2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128588533188132548, "Cod."), 0, True)
      xx_descr2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128588533211590298, "Descr."), 40, True)
      xx_codditt2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128588533230043728, "Codice ditta"), 0, False)
      xx_seleziona3.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128802011174347331, "Seleziona"), "S", "N")
      xx_codvar3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802011535792902, "Cod."), 0, True)
      xx_descr3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802011793008384, "Descr."), 40, True)
      xx_codditt3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012184300952, "Codice ditta"), 0, False)

      grvArtico.NTSSetParam(oMenu, oApp.Tr(Me, 128588531857599308, "Analitico varianti"))
      ar_codvar1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012348850103, "1°livello"), 8, True)
      ar_codvar2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012361663997, "2°livello"), 8, True)
      ar_codvar3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012384791513, "3°livello"), 8, True)
      ar_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012430734011, "Descrizione"), 40, False)
      ar_desint.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012446048177, "Descr.interna"), 40, True)
      ar_scomin.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128802012489646670, "Scorta min."), "#,##0.000", 15)
      ar_scomax.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128802012503398166, "Scorta max."), "#,##0.000", 15)
      ar_minord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128802012586532210, "Qtà lotto stand."), "#,##0.000", 15)
      ar_inesaur.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128802012627786698, "In esaurim."), "S", "N")
      ar_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012649195277, "Note"), 0, True)
      ar_codart.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012661696637, "AR_CODART"), CLN__STD.CodartMaxLen, False)
      ar_formula.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012678104672, "Formula"), 0, True)
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012695137775, "Codice ditta"), 12, False)

      edAr_codtlox.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129512421717078721, "Cod. modalità di creazione autom. lotto"), tablotx)
      cbAr_tipscarlotx.NTSSetParam(oApp.Tr(Me, 129512424576749075, "Modalità di scarico lotto"))

      ceListini.NTSSetParam(oMenu, "Listini", "BNMGARTV", Nothing)
      ceListini.tlbListEsci.Enabled = False
      ceListini.LcTipo = " "
      ceListini.LcCodart = ""
      ceListini.LcCodartRoot = ""
      ceListini.LcConto = 0
      AddHandler ceListini.VaiScontoCollegato, AddressOf ceListini_VaiScontoCollegato

      ceSconti.NTSSetParam(oMenu, "Sconti", "BNMGARTV", Nothing)
      ceSconti.tlbScontiEsci.Enabled = False
      ceSconti.TipoSconto = 0
      ceSconti.SoCodart = ""
      ceSconti.SoCodartRoot = ""
      ceSconti.SoConto = 0
      ceSconti.SoClasseCli = 0
      ceSconti.SoClasseArt = 0
      AddHandler ceSconti.VaiListinoCollegato, AddressOf ceSconti_VaiListinoCollegato

      ceProvvig.NTSSetParam(oMenu, "Provvigioni", "BNMGARTV", Nothing)
      ceProvvig.tlbProvEsci.Enabled = False
      ceProvvig.TipoProvv = 1
      ceProvvig.PerCodart = ""
      ceProvvig.PerCodartRoot = ""
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
      edAr_reparto.NTSSetParamZoom("ZOOMTABREAR")

      edAr_flmod.Enabled = False

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
      edAr_codimba.NTSDbField = "ARTICO.ar_codimba"
      edAr_misura1.NTSDbField = "ARTICO.ar_misura1"
      edAr_misura2.NTSDbField = "ARTICO.ar_misura2"
      edAr_misura3.NTSDbField = "ARTICO.ar_misura3"
      ckAr_gesubic.NTSText.NTSDbField = "ARTICO.ar_gesubic"
      edAr_volume.NTSDbField = "ARTICO.ar_volume"
      edAr_sublotto.NTSDbField = "ARTICO.ar_sublotto"
      edAr_maxlotto.NTSDbField = "ARTICO.ar_maxlotto"
      edAr_ggragg.NTSDbField = "ARTICO.ar_ggragg"
      ckAr_ripriord.NTSText.NTSDbField = "ARTICO.ar_ripriord"
      cbAr_perragg.NTSDbField = "ARTICO.ar_perragg"
      edAr_flmod.NTSDbField = "ARTICO.ar_flmod"
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
      lbXx_flmod.NTSDbField = "ARTICO.xx_flmod"
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
      lbXx_reparto.NTSDbField = "ARTICO.xx_reparto"
      ckAr_webvis.NTSText.NTSDbField = "ARTICO.ar_webvis"
      ckAr_webusat.NTSText.NTSDbField = "ARTICO.ar_webusat"
      ckAr_webvend.NTSText.NTSDbField = "ARTICO.ar_webvend"
      edAr_codseat.NTSDbField = "ARTICO.ar_codseat"
      lbXx_codseat.NTSDbField = "ARTICO.xx_codseat"
      ckAr_consmrp.NTSText.NTSDbField = "ARTICO.ar_consmrp"
      cbAr_deterior.NTSDbField = "ARTICO.ar_deterior"
      ckAr_staetip.NTSText.NTSDbField = "ARTICO.ar_staetip"
      edAr_converp.NTSDbField = "ARTICO.ar_converp"
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcArtv, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMMGARTV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      'prende, se esistono, le opzioni dal registro
      oCleArtv.nCodiva = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CodIva", "0", " ", "0"))
      oCleArtv.nControa = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Controa", "0", " ", "0"))
      oCleArtv.nControp = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Controp", "0", " ", "0"))
      oCleArtv.nContros = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Contros", "0", " ", "0"))
      oCleArtv.strUnmis = oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "UnMis", "", " ", "")
      oCleArtv.strInizioValListini = NTSCStr(oMenu.GetSettingBus("BSMGARTV", "OPZIONI", ".", "DataInizioValListini", NTSCStr(Now), " ", NTSCStr(Now)))
      oCleArtv.strInizioValSconti = NTSCStr(oMenu.GetSettingBus("BSMGARTV", "OPZIONI", ".", "DataInizioValSconti", NTSCStr(Now), " ", NTSCStr(Now)))
      oCleArtv.strInizioValProvv = NTSCStr(oMenu.GetSettingBus("BSMGARTV", "OPZIONI", ".", "DataInizioValProvv", NTSCStr(Now), " ", NTSCStr(Now)))
      oCleArtv.bCreaBarcodeE13 = CBool(oMenu.GetSettingBus("BSMGARTV", "OPZIONI", ".", "CreaBarcodeE13", "0", " ", "0"))
      oCleArtv.bConver9Dec = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Conver9Dec", "0", " ", "0"))
      oCleArtv.bSbloccaFLotto = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "SbloccaFLotto", "0", " ", "0"))
      oCleArtv.strListiniAbilitati = oMenu.GetSettingBus("BSMGARTV", "OPZIONIUT", ".", "ListiniAbilitati", "", " ", "")
      oCleArtv.bSecondaDescrizione = CBool(oMenu.GetSettingBus("BSMGARTV", "OPZIONI", ".", "SecondaDescrizione", "0", " ", "0"))
      oCleArtv.bGestTabUnmis = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GestTabUnmis", "0", " ", "0"))
      oCleArtv.bValidaNomenclCombin = CBool(NTSCInt(oMenu.GetSettingBus("BSMGARTV", "OPZIONI", ".", "ValidaNomenclCombin", "0", " ", "0"))) 'se abilitata in fase di inserimento delal nomenclatura la valida
      '-------------------------------------------------------------------------------------------------------
      'Opzione globale per la gestione dei prezzi riferiti ad una unità di misura diversa da quella principale
      oCleArtv.bAbilitaPrezzoUM = CBool(oMenu.GetSettingBus("OPZIONI", ".", ".", "AbilitaPrezzoUM", "0", " ", "0"))

      oCleArtv.bIndicod = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "Indicod", "0", " ", "0"))
      oCleArtv.strPrefixEAN13 = Trim(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "PrefixEAN13", "", " ", ""))
      '-------------------------------------------------------------------------------------------------------
      oCleArtv.bSelCodiceNoApri = CBool(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "SelCodiceNoApri", "0", " ", "0"))
      '-------------------------------------------------------------------------------------------------------
      nDimensioneImmagine = NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "ImageSizeLimit", "0", " ", "0"))

      If oCleArtv.bIndicod = True Then
        Select Case Len(oCleArtv.strPrefixEAN13)
          Case 0
            oApp.MsgBoxErr(oApp.Tr(Me, 128802017930909547, "L'impostazione di registro 'PrefixEAN13' non è corretta," & vbCrLf & _
              "pertanto l'eventuale creazione di Barcode EAN13" & _
              "sarà fatta seguendo i normali standard."))
            oCleArtv.bIndicod = False
          Case 1 To 11
            'OK
          Case 12
            oApp.MsgBoxErr(oApp.Tr(Me, 128584497554814492, "L'impostazione di registro 'PrefixEAN13' è di 12 caratteri," & vbCrLf & _
              "pertanto l'eventuale creazione di Barcode EAN13" & _
              "sarà fatta seguendo i normali standard."))
            oCleArtv.bIndicod = False
          Case Else
            oApp.MsgBoxErr(oApp.Tr(Me, 128584497581622412, "L'impostazione di registro 'PrefixEAN13' supera i 12 caratteri," & vbCrLf & _
              "pertanto l'eventuale creazione di Barcode EAN13" & _
              "sarà fatta seguendo i normali standard."))
            oCleArtv.bIndicod = False
        End Select
      End If
      oCleArtv.bGestUbicSenzaLext = CBool(NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "GestUbicSenzaLext", "0", " ", "0"))) 'NON DOCUMENTARE

      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      SetStato(0)

      oCleArtv.nNumliv = 0
      oCleArtv.nLungvar1 = 0
      oCleArtv.nLungvar2 = 0
      oCleArtv.nLungvar3 = 0
      oCleArtv.strPrevar = ""

      '-----------------------------------------------------------------------------------------
      '--- Se l'opzione di registro "BSMGARTV\SecondaDescrizione" (per default = False)
      '--- è = True, abilita la seconda descrizione
      '-----------------------------------------------------------------------------------------
      If oCleArtv.bSecondaDescrizione = True Then
        GctlSetVisEnab(edAr_desint, False)
      Else
        edAr_desint.Enabled = False
      End If

      '-----------------------------------------------------------------------------------------
      oCleArtv.strUnmisOrigine = ""
      oCleArtv.strUnmis2Origine = ""
      oCleArtv.strConfez2Origine = ""
      oCleArtv.strUm4Origine = ""

      '-------------------------------------------------------------------------------------------------------
      '--- Se non c'è il modulo "Logistica Estesa"
      '--- disabilita i campi relativi a: ar_gesubic
      '-----------------------------------------------------------------------------------------
      If (CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtLEX)) Or (CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCP)) Then
        oCleArtv.bLogisticaEstesa = True
      Else
        oCleArtv.bLogisticaEstesa = False
        If Not oCleArtv.bGestUbicSenzaLext Then
          ckAr_gesubic.Enabled = False
        End If
      End If
      '-------------------------------------------------------------------------------------------------------
      '--- Se non c'è il modulo "TCO"
      '--- disabilita le descrizioni in lingua del modello
      '-----------------------------------------------------------------------------------------
      If (CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO)) Then
        oCleArtv.bModTCO = True
      Else
        oCleArtv.bModTCO = False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Per Euro2000:
      '--- Se attiva l'opzione, il valore minimo impostato per i campio Scorta Minima/Massima/Qtà lotto/std
      '--- con Politica di riordino, è zero
      '-----------------------------------------------------------------------------------------
      oCleArtv.bNoMsgCongruenzaPolSconte = CBool(NTSCInt(oMenu.GetSettingBus("BSMGARTI", "OPZIONI", ".", "NoMsgCongruenzaPolSconte", "0", " ", "0")))
      '--------------------------------------------------------------------------------------------------------------
      'Mostro il combo degli articoli deteriorabili solo se ho l'opzione di registro attivata (che sia su opzioni o opzionidoc non importa)
      If oCleArtv.ArticoliDeteriorabili() Then
        GctlSetVisible(cbAr_deterior)
        GctlSetVisible(lbAr_deterior)
      Else
        cbAr_deterior.Visible = False
        lbAr_deterior.Visible = False
      End If
      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "APRI;" Then
          tlbApri_ItemClick(Me, Nothing)
        Else
          tlbNuovo_ItemClick(tlbNuovo, Nothing)
        End If
      End If    'If Not oCallParams Is Nothing Then

      oCallParams = Nothing 'in questo modo se vengo chiamato da bnmghlar, prima apro l'articolo scelto, poi se faccio ripristina posso scegliere un altro articolo 
      oCleArtv.bDaGest = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGARTV_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Dim dttPeca As New DataTable
    Try
      oCleArtv.SetImagesDir()

      '---------------------------------------------------------------------
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCAE) Then
        oMenu.ValCodiceDb("1", DittaCorrente, "TABPECA", "N", , dttPeca)
        If dttPeca.Rows.Count = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129319677734956014, "Tabella delle Personalizzazioni CA-DC (Globale) non configurata. Imposibile continuare"))
          Me.Close()
          Return
        End If
        oCleArtv.bCampiCAEAttivi = CBool(IIf(NTSCStr(dttPeca.Rows(0)!tb_richarti) = "S", True, False))
      End If
      '--------------------------------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And bsModSupCEC) Then
      Else
        fmEcommerce.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGARTV_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
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

  Public Overridable Sub FRMMGARTV_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then
      e.Cancel = True
    End If
  End Sub

  Public Overridable Sub FRMMGARTV_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcArtv.Dispose()
      dsArtv.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub cmdVisGif1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVisGif1.Click
    Dim oPar As New CLE__CLDP
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se non esiste la cartella delle immagini avvisa ed esce
      '-----------------------------------------------------------------------------------------
      If Not Directory.Exists(oCleArtv.strImageDir) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128802017845284547, "La cartella delle immagini non esiste. Impossibile proseguire."))
        Exit Sub
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se non esiste il file indicato nel TextBox relativo, nella cartella delle immagini
      '--- avvisa ed esce
      '-----------------------------------------------------------------------------------------
      If Not File.Exists(oCleArtv.strImageDir & "\" & edAr_gif1.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128559129514857263, "L'immagine speficificata non esiste o è stata rimossa. Impossibile proseguire."))
        edAr_gif1.Text = ""
        edAr_gif1.Focus()
        cmdVisGif1.Enabled = False
        Exit Sub
      End If

      oPar.strPar1 = "BNMGARTV"
      oPar.dPar1 = 1
      oPar.strPar2 = oCleArtv.strImageDir & "\" & edAr_gif1.Text

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
      If Not Directory.Exists(oCleArtv.strImageDir) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128802013500836907, "La cartella delle immagini non esiste. Impossibile proseguire."))
        Exit Sub
      End If
      If Not File.Exists(oCleArtv.strImageDir & "\" & edAr_gif2.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128559129803371577, "L'immagine speficificata non esiste o è stata rimossa. Impossibile proseguire."))
        edAr_gif2.Text = ""
        edAr_gif2.Focus()
        Exit Sub
      End If

      oPar.strPar1 = "BNMGARTV"
      oPar.dPar1 = 2
      oPar.strPar2 = oCleArtv.strImageDir & "\" & edAr_gif2.Text

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
        oCleArtv.bHasChanges = True
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
        oCleArtv.bHasChanges = True
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdProgtot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProgtot.Click
    Dim oPar As New CLE__CLDP
    Dim strCodart As String = ""
    Dim dsTmp As DataSet = Nothing
    Try
      If Not Salva() Then Exit Sub

      If grvArtico.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128588515553722158, "Posizionarsi su una riga valida nell'analitico a varianti prima di chiamare la visualizzazione dei progressivi totali relativi."))
        Exit Sub
      End If
      Select Case oCleArtv.nNumliv
        Case 1 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
        Case 2 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2))
        Case 3 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3))
      End Select

      If Not oCleArtv.GetArtprox(strCodart, dsTmp) Then Exit Sub

      If dsTmp.Tables("ARTPROX").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128588522498253408, "Non esiste un totale dei progressivi per questo articolo."))
        Exit Sub
      End If

      oPar.strPar1 = "BNMGARTV"
      oPar.dPar1 = NTSCDec(edAr_perqta.Text)
      oPar.strPar2 = strCodart
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
    Dim strCodart As String = ""
    Try
      If Not Salva() Then Exit Sub

      If grvArtico.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128588515617472158, "Posizionarsi su una riga valida nell'analitico a varianti prima di chiamare la gestione delle descrizioni in lingua relativa."))
        Exit Sub
      End If
      Select Case oCleArtv.nNumliv
        Case 1 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
        Case 2 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2))
        Case 3 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3))
      End Select

      oPar.strPar1 = "BNMGARTV"
      oPar.strPar2 = strCodart
      oPar.bPar1 = False

      oMenu.RunChild("NTSInformatica", "FRMMGHLAV", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdProgressivi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProgressivi.Click
    Dim oPar As New CLE__CLDP
    Dim strCodart As String = ""
    Dim dsTmp As DataSet = Nothing
    Try
      If Not Salva() Then Exit Sub

      If grvArtico.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128588515107940908, "Posizionarsi su una riga valida nell'analitico a varianti prima di chiamare la visualizzazione dei progressivi relativi."))
        Exit Sub
      End If
      Select Case oCleArtv.nNumliv
        Case 1 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
        Case 2 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2))
        Case 3 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3))
      End Select

      If Not oCleArtv.GetArtpro(strCodart, dsTmp) Then Exit Sub

      If dsTmp.Tables("ARTPRO").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128588516402472158, "Non esistono progressivi per questo articolo."))
        Exit Sub
      End If

      oPar.strPar1 = "BNMGARTV"
      oPar.strPar2 = strCodart
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
    Dim strCodart As String = ""
    Try
      If Not Salva() Then Exit Sub

      If grvArtico.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128588515175284658, "Posizionarsi su una riga valida nell'analitico a varianti prima di chiamare la gestione dei codici articolo cliente/fornitore relativa."))
        Exit Sub
      End If
      Select Case oCleArtv.nNumliv
        Case 1 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
        Case 2 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2))
        Case 3 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3))
      End Select

      oPar.strPar1 = "BNMGARTV"
      oPar.strPar2 = strCodart

      oMenu.RunChild("NTSInformatica", "FRMMGCACF", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdEsplodi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEsplodi.Click
    Try
      If Not Var1Salva() Then Exit Sub

      If dsVar1.Tables("ARTVAR").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128802013625527599, "Inserire almeno una riga al primo livello."))
        Exit Sub
      End If
      If NTSCStr(dsArtv.Tables("ARTICO").Rows(0)!ar_descr) = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128802014041319493, "Descrizione articolo obbligatoria."))
        Exit Sub
      End If
      If Trim(NTSCStr(dsArtv.Tables("ARTICO").Rows(0)!ar_unmis)) = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128802013976474083, "Unità di misura principale obbligatoria."))
        Exit Sub
      End If

      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129538307657529297, "Confermi la generazione di tutte le varianti?")) = Windows.Forms.DialogResult.No Then Return

      Select Case oCleArtv.nNumliv
        Case 1

          oCleArtv.Var1Esplodi(grvVar1.NTSGetCurrentDataRow)
        Case 2
          If Not Var2Salva() Then Exit Sub

          If dsVar2.Tables("ARTVAR").Rows.Count = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128802014277419287, "Inserire almeno una riga al secondo livello."))
            Exit Sub
          End If

          oCleArtv.Var2Esplodi(grvVar2.NTSGetCurrentDataRow)
        Case 3
          If Not Var2Salva() Then Exit Sub
          If Not Var3Salva() Then Exit Sub

          If dsVar3.Tables("ARTVAR").Rows.Count = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128589365635394034, "Inserire almeno una riga al terzo livello."))
            Exit Sub
          End If

          oCleArtv.Var3Esplodi(grvVar3.NTSGetCurrentDataRow)
      End Select

      oCleArtv.CheckNumaArtico()

      tsArtv.SelectedTabPageIndex = 1
      ArticoApri()
      oCleArtv.dsArticoShared.AcceptChanges()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdAggiungi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAggiungi.Click
    Dim dtrTmp As DataRow = Nothing
    Dim nVar As Integer
    Try
      If Not Var1Salva() Then Exit Sub

      If dsVar1.Tables("ARTVAR").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128802014804620283, "Inserire almeno una riga al primo livello."))
        Exit Sub
      End If
      If edAr_descr.Text = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128593681953768756, "Descrizione articolo obbligatoria."))
        Exit Sub
      End If
      If Trim(edAr_unmis.Text) = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128593681975233414, "Unità di misura principale obbligatoria."))
        Exit Sub
      End If

      Select Case oCleArtv.nNumliv
        Case 1
          If Not NTSLastControlFocussed.Equals(grVar1) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128623395155425751, "Posizionarsi su una riga al primo livello."))
            Exit Sub
          Else
            If grvVar1.NTSGetCurrentDataRow Is Nothing Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128593683986716856, "Posizionarsi su una riga al primo livello."))
              Exit Sub
            End If
          End If

          oCleArtv.Var1Aggiungi(grvVar1.NTSGetCurrentDataRow)
        Case 2
          If Not Var2Salva() Then Exit Sub

          If dsVar2.Tables("ARTVAR").Rows.Count = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128802014763525481, "Inserire almeno una riga al secondo livello."))
            Exit Sub
          End If
          If Not (NTSLastControlFocussed.Equals(grVar1) Or NTSLastControlFocussed.Equals(grVar2)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128623395119922951, "Posizionarsi su una riga al primo o secondo livello."))
            Exit Sub
          End If
          If NTSLastControlFocussed.Equals(grVar1) Then
            If grvVar1.NTSGetCurrentDataRow Is Nothing Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128802014956186663, "Posizionarsi su una riga al primo livello."))
              Exit Sub
            End If
          ElseIf NTSLastControlFocussed.Equals(grVar2) Then
            If grvVar2.NTSGetCurrentDataRow Is Nothing Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128802015280882475, "Posizionarsi su una riga al secondo livello."))
              Exit Sub
            End If
          End If

          'a seconda in che griglia sono posizionato
          If NTSLastControlFocussed.Equals(grVar1) Then
            dtrTmp = grvVar1.NTSGetCurrentDataRow
            nVar = 1
          ElseIf NTSLastControlFocussed.Equals(grVar2) Then
            dtrTmp = grvVar2.NTSGetCurrentDataRow
            nVar = 2
          End If

          oCleArtv.Var2Aggiungi(dtrTmp, nVar)
        Case 3
          If Not Var2Salva() Then Exit Sub
          If Not Var3Salva() Then Exit Sub

          If dsVar2.Tables("ARTVAR").Rows.Count = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128802014843058767, "Inserire almeno una riga al secondo livello."))
            Exit Sub
          End If
          If dsVar3.Tables("ARTVAR").Rows.Count = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128593773003468406, "Inserire almeno una riga al terzo livello."))
            Exit Sub
          End If
          If Not (NTSLastControlFocussed.Equals(grVar1) Or NTSLastControlFocussed.Equals(grVar2) Or NTSLastControlFocussed.Equals(grVar3)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128623395513112551, "Posizionarsi su una riga al primo o secondo o terzo livello."))
            Exit Sub
          End If
          If NTSLastControlFocussed.Equals(grVar1) Then
            If grvVar1.NTSGetCurrentDataRow Is Nothing Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128802014988843749, "Posizionarsi su una riga al primo livello."))
              Exit Sub
            End If
          ElseIf NTSLastControlFocussed.Equals(grVar2) Then
            If grvVar2.NTSGetCurrentDataRow Is Nothing Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128802015246194087, "Posizionarsi su una riga al secondo livello."))
              Exit Sub
            End If
          ElseIf NTSLastControlFocussed.Equals(grVar3) Then
            If grvVar3.NTSGetCurrentDataRow Is Nothing Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128623396208779751, "Posizionarsi su una riga al terzo livello."))
              Exit Sub
            End If
          End If

          'a seconda in che griglia sono posizionato
          If NTSLastControlFocussed.Equals(grVar1) Then
            dtrTmp = grvVar1.NTSGetCurrentDataRow
            nVar = 1
          ElseIf NTSLastControlFocussed.Equals(grVar2) Then
            dtrTmp = grvVar2.NTSGetCurrentDataRow
            nVar = 2
          ElseIf NTSLastControlFocussed.Equals(grVar3) Then
            dtrTmp = grvVar3.NTSGetCurrentDataRow
            nVar = 3
          End If

          oCleArtv.Var3Aggiungi(dtrTmp, nVar)
      End Select

      oCleArtv.CheckNumaArtico()

      tsArtv.SelectedTabPageIndex = 1
      ArticoApri() 'refresh griglia analitico varianti
      oCleArtv.dsArticoShared.AcceptChanges()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdEspSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEspSel.Click
    Dim i As Integer
    Dim bRigheSelezionate1 As Boolean
    Dim bRigheSelezionate2 As Boolean
    Dim bRigheSelezionate3 As Boolean
    Try
      If Not Var1Salva() Then Exit Sub

      If dsVar1.Tables("ARTVAR").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128802013658184685, "Inserire almeno una riga al primo livello."))
        Exit Sub
      End If
      If NTSCStr(dsArtv.Tables("ARTICO").Rows(0)!ar_descr) = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128802013930222899, "Descrizione articolo obbligatoria."))
        Exit Sub
      End If
      If Trim(NTSCStr(dsArtv.Tables("ARTICO").Rows(0)!ar_unmis)) = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128802014005224819, "Unità di misura principale obbligatoria."))
        Exit Sub
      End If

      Select Case oCleArtv.nNumliv
        Case 1
          'controllo che sia stata selezionata almeno una riga per griglia
          For i = 0 To dsVar1.Tables("ARTVAR").Rows.Count - 1
            If NTSCStr(dsVar1.Tables("ARTVAR").Rows(i)!xx_seleziona1) = "S" Then bRigheSelezionate1 = True
          Next
          If bRigheSelezionate1 = False Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128624789243836090, "Non sono state selezionate righe al primo livello." & vbCrLf & _
              "Esplosione righe selezionate non possibile."))
            Exit Sub
          End If

          oCleArtv.Var1EspSel(grvVar1.NTSGetCurrentDataRow)
        Case 2
          If Not Var2Salva() Then Exit Sub

          If dsVar2.Tables("ARTVAR").Rows.Count = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128802014310857643, "Inserire almeno una riga al secondo livello."))
            Exit Sub
          End If

          'controllo che sia stata selezionata almeno una riga per griglia
          For i = 0 To dsVar1.Tables("ARTVAR").Rows.Count - 1
            If NTSCStr(dsVar1.Tables("ARTVAR").Rows(i)!xx_seleziona1) = "S" Then bRigheSelezionate1 = True
          Next
          For i = 0 To dsVar2.Tables("ARTVAR").Rows.Count - 1
            If NTSCStr(dsVar2.Tables("ARTVAR").Rows(i)!xx_seleziona2) = "S" Then bRigheSelezionate2 = True
          Next
          If bRigheSelezionate1 = False Or bRigheSelezionate2 = False Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128802015419948535, "Non sono state selezionate le righe in tutti e due i livelli." & vbCrLf & _
              "Esplosione righe selezionate non possibile."))
            Exit Sub
          End If

          oCleArtv.Var2EspSel(grvVar2.NTSGetCurrentDataRow)
        Case 3
          If Not Var2Salva() Then Exit Sub
          If Not Var3Salva() Then Exit Sub

          If dsVar3.Tables("ARTVAR").Rows.Count = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128802014524300607, "Inserire almeno una riga al terzo livello."))
            Exit Sub
          End If

          'controllo che sia stata selezionata almeno una riga per griglia
          For i = 0 To dsVar1.Tables("ARTVAR").Rows.Count - 1
            If NTSCStr(dsVar1.Tables("ARTVAR").Rows(i)!xx_seleziona1) = "S" Then bRigheSelezionate1 = True
          Next
          For i = 0 To dsVar2.Tables("ARTVAR").Rows.Count - 1
            If NTSCStr(dsVar2.Tables("ARTVAR").Rows(i)!xx_seleziona2) = "S" Then bRigheSelezionate2 = True
          Next
          For i = 0 To dsVar3.Tables("ARTVAR").Rows.Count - 1
            If NTSCStr(dsVar3.Tables("ARTVAR").Rows(i)!xx_seleziona3) = "S" Then bRigheSelezionate3 = True
          Next
          If bRigheSelezionate1 = False Or bRigheSelezionate2 = False Or bRigheSelezionate3 = False Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128802015454011907, "Non sono state selezionate le righe in tutti e tre i livelli." & vbCrLf & _
              "Esplosione righe selezionate non possibile."))
            Exit Sub
          End If

          oCleArtv.Var3EspSel(grvVar3.NTSGetCurrentDataRow)
      End Select

      tsArtv.SelectedTabPageIndex = 1
      ArticoApri()
      oCleArtv.dsArticoShared.AcceptChanges()

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
        dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_codcla1 = strT(0)
        dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_codcla2 = strT(1)
        dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_codcla3 = strT(2)
        dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_codcla4 = strT(3)
        dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_codcla5 = strT(4)
      End If
      CaricaClassificazione()

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
        If tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(5)) Then
          Select Case ceListini.cbTipolistino.SelectedValue
            Case "C" : If ceListini.grvList.FocusedColumn.Name = "lc_conto" Then strTabName = "ANAGRA_CLI"
            Case "F" : If ceListini.grvList.FocusedColumn.Name = "lc_conto" Then strTabName = "ANAGRA_FOR"
          End Select
        End If
        If tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(6)) Then
          Select Case NTSCInt(ceSconti.cbTiposconti.SelectedValue)
            Case 4 : If ceSconti.grvSconti.FocusedColumn.Name = "so_conto" Then strTabName = "ANAGRACF"
          End Select
        End If
        If tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(7)) Then
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
      oCleArtv.bDuplica = False
      If Not Apri(True) Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Try
      oCleArtv.bDuplica = True
      If Not Apri(False) Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbApri_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbApri.ItemClick
    Try
      oCleArtv.bDuplica = False
      If Not Apri(False) Then Return
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
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
      If Not Salva() Then Exit Sub

      '-------------------------------------------------
      'cancello la articolo
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128550728307978584, "ATTENZIONE! Confermi la CANCELLAZIONE DEFINITIVA del articolo |" & NTSCStr(edAr_codart.Text) & "| e di tutti gli articoli ad essa collegati?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If Not oCleArtv.TestPreCancella(NTSCStr(dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_codart), _
              NTSCStr(dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_tipoopz), _
              NTSCInt(dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_codtipa)) Then Return

          If dsArtv.Tables("ARTICO").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcArtv.RemoveAt(dcArtv.Position)
          oCleArtv.Salva(True)

          SetStato(0)

          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcArtv, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      Me.ValidaLastControl()          'se non valido il controllo su cui sono, quando modifico il controllo e, senza uscire, faccio 'ripristina' il controllo rimane sporco

      '-------------------------------------------------
      'ripristino la articolo
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
          If dsArtv.Tables("ARTICO").Rows.Count = 1 And dsArtv.Tables("ARTICO").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleArtv.Ripristina(dcArtv.Position, dcArtv.Filter)

          If tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(5)) Then
            ceListini.Ripristina()
          ElseIf tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(6)) Then
            ceSconti.Ripristina()
          ElseIf tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(7)) Then
            ceProvvig.Ripristina()
          End If

          If tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(0)) Then
            oCleArtv.Var1Ripristina(dcVar1.Position, dcVar1.Filter)
            oCleArtv.Var2Ripristina(dcVar2.Position, dcVar2.Filter)
            oCleArtv.Var3Ripristina(dcVar3.Position, dcVar3.Filter)
          End If

          If tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(1)) Then
            oCleArtv.ArticoRipristina(dcArtico.Position, dcArtico.Filter)
          End If

          'se sono in nuovo cancello tutto quello gia inserito
          If oCleArtv.bNew Then
            oCleArtv.ArticoRipristinaCancella()
          End If

          SetStato(0)
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcArtv, Me)
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

      If oCleArtv.bGestTabUnmis = False And _
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
    Me.Close()
  End Sub

  Public Overridable Sub tlbRecordNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordNuovo.ItemClick
    Try
      If grVar1.Focused Then
        grvVar1.NTSNuovo()
      ElseIf grVar2.Focused Then
        grvVar2.NTSNuovo()
      ElseIf grVar3.Focused Then
        grvVar3.NTSNuovo()
      ElseIf grArtico.Focused Then
        grvArtico.NTSNuovo()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRecordAggiorna_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordAggiorna.ItemClick
    Try
      If grVar1.Focused Then
        Var1Salva()
      ElseIf grVar2.Focused Then
        Var2Salva()
      ElseIf grVar3.Focused Then
        Var3Salva()
      ElseIf grArtico.Focused Then
        ArticoSalva()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRecordRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordRipristina.ItemClick
    Try
      If grVar1.Focused Then
        If Not grvVar1.NTSRipristinaRigaCorrenteBefore(dcVar1, True) Then Return
        oCleArtv.Var1Ripristina(dcVar1.Position, dcVar1.Filter)
        grvVar1.NTSRipristinaRigaCorrenteAfter()
      ElseIf grVar2.Focused Then
        If Not grvVar2.NTSRipristinaRigaCorrenteBefore(dcVar2, True) Then Return
        oCleArtv.Var2Ripristina(dcVar2.Position, dcVar2.Filter)
        grvVar2.NTSRipristinaRigaCorrenteAfter()
      ElseIf grVar3.Focused Then
        If Not grvVar3.NTSRipristinaRigaCorrenteBefore(dcVar3, True) Then Return
        oCleArtv.Var3Ripristina(dcVar3.Position, dcVar3.Filter)
        grvVar3.NTSRipristinaRigaCorrenteAfter()
      ElseIf grArtico.Focused Then
        If Not grvArtico.NTSRipristinaRigaCorrenteBefore(dcArtico, True) Then Return
        oCleArtv.ArticoRipristina(dcArtico.Position, dcArtico.Filter)
        grvArtico.NTSRipristinaRigaCorrenteAfter()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRecordCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRecordCancella.ItemClick
    Dim strCodart As String = ""
    Dim dRes As DialogResult

    Try
      If grVar1.Focused Then
        If grvVar1.NTSGetCurrentDataRow Is Nothing Then Return
        If Not oCleArtv.Var1TestPreCancella(grvVar1.NTSGetCurrentDataRow) Then Return

        dRes = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128802015749331967, "ATTENZIONE! Confermi la CANCELLAZIONE DEFINITIVA della variante |" & NTSCStr(grvVar1.NTSGetCurrentDataRow!xx_codvar1) & "| del livello 1 e degli articoli ad essa collegata?"))
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not grvVar1.NTSDeleteRigaCorrente(dcVar1, False) Then Return
          oCleArtv.Var1Salva(True)
          ArticoApri() 'dopo aver cancellato riapro la griglia articoli
        End If
      ElseIf grVar2.Focused Then
        If grvVar2.NTSGetCurrentDataRow Is Nothing Then Return
        If Not oCleArtv.Var2TestPreCancella(grvVar2.NTSGetCurrentDataRow) Then Return

        dRes = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128624206919686249, "ATTENZIONE! Confermi la CANCELLAZIONE DEFINITIVA della variante |" & NTSCStr(grvVar2.NTSGetCurrentDataRow!xx_codvar2) & "| del livello 2 e degli articoli ad essa collegata?"))
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not grvVar2.NTSDeleteRigaCorrente(dcVar2, False) Then Return
          oCleArtv.Var2Salva(True)
          ArticoApri() 'dopo aver cancellato riapro la griglia articoli
        End If
      ElseIf grVar3.Focused Then
        If grvVar3.NTSGetCurrentDataRow Is Nothing Then Return
        If Not oCleArtv.Var3TestPreCancella(grvVar3.NTSGetCurrentDataRow) Then Return

        dRes = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128624206940103909, "ATTENZIONE! Confermi la CANCELLAZIONE DEFINITIVA della variante |" & NTSCStr(grvVar3.NTSGetCurrentDataRow!xx_codvar3) & "| del livello 3 e degli articoli ad essa collegata?"))
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not grvVar3.NTSDeleteRigaCorrente(dcVar3, False) Then Return
          oCleArtv.Var3Salva(True)
          ArticoApri() 'dopo aver cancellato riapro la griglia articoli
        End If
      ElseIf grArtico.Focused Then
        If grvArtico.NTSGetCurrentDataRow Is Nothing Then Return
        If Not oCleArtv.ArticoTestPreCancella(grvArtico.NTSGetCurrentDataRow) Then Return

        dRes = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 128624206961456729, "ATTENZIONE! Confermi la CANCELLAZIONE DEFINITIVA del articolo |" & NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codart) & "| ?"))
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          '----------------------------------------------------------------------------------------------------------
          Select Case oCleArtv.nNumliv
            Case 1 : strCodart = oCleArtv.strArtvCodroot.Trim & _
                                 NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1).Trim
            Case 2 : strCodart = oCleArtv.strArtvCodroot.Trim & _
                                 NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1).Trim & _
                                 NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2).Trim
            Case 3 : strCodart = oCleArtv.strArtvCodroot.Trim & _
                                 NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1).Trim & _
                                 NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2).Trim & _
                                 NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3).Trim
          End Select
          '----------------------------------------------------------------------------------------------------------
          If Not grvArtico.NTSDeleteRigaCorrente(dcArtico, False) Then Return
          oCleArtv.ArticoSalva(True)
          oCleArtv.CancellaArtacce(strCodart)
          ArticoApri() 'se non viene cancellato l'articolo riapro la griglia articoli
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbBarcode_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbBarcode.ItemClick
    Dim oPar As New CLE__CLDP
    Dim dsTmp As DataSet = Nothing
    Dim strCodart As String = ""
    Try
      If Not Salva() Then Exit Sub

      If grvArtico.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128802016149029699, "Posizionarsi su una riga valida nell'analitico a varianti prima di chiamare la gestione del Barcode relativo."))
        Exit Sub
      End If
      Select Case oCleArtv.nNumliv
        Case 1 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
        Case 2 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2))
        Case 3 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3))
      End Select

      oPar.strPar1 = "BNMGARTV"
      oPar.strPar2 = strCodart
      oPar.strPar3 = edAr_unmis.Text
      oPar.strPar4 = edAr_confez2.Text
      oPar.strPar5 = edAr_unmis2.Text
      oPar.dPar1 = NTSCDec(edAr_qtacon2.Text)
      oPar.dPar2 = NTSCDec(edAr_conver.Text)

      oMenu.RunChild("NTSInformatica", "FRMMGBARC", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbKit_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbKit.ItemClick
    Dim oPar As New CLE__CLDP
    Dim strCodart As String = ""
    Try
      If Not Salva() Then Exit Sub

      If grvArtico.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128588515318253408, "Posizionarsi su una riga valida nell'analitico a varianti prima di chiamare la gestione dei KIT relativi."))
        Exit Sub
      End If

      If NTSCStr(dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_tipokit) = " " Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128565244716608415, "Articolo non tipo Kit, apertura Composizione Kit non possibile."))
        Exit Sub
      End If

      Select Case oCleArtv.nNumliv
        Case 1 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
        Case 2 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2))
        Case 3 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3))
      End Select

      oPar.strPar1 = "BNMGARTV"
      oPar.strPar2 = strCodart

      oMenu.RunChild("NTSInformatica", "FRMMGCKIT", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbOle_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbOle.ItemClick
    Dim strParam As String = ""
    Dim strCodart As String = ""
    Try
      If Not Salva() Then Exit Sub

      If grvArtico.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128588515377159658, "Posizionarsi su una riga valida nell'analitico a varianti prima di chiamare la gestione degli oggetti OLE relativi."))
        Exit Sub
      End If

      Select Case oCleArtv.nNumliv
        Case 1 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
        Case 2 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2))
        Case 3 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3))
      End Select

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
    Dim strCodart As String = ""
    Dim strQuant As String
    Try
      If Not Salva() Then Exit Sub

      If grvArtico.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128588515431534658, "Posizionarsi su una riga valida nell'analitico a varianti prima di chiamare la gestione dellle stampe etichette"))
        Exit Sub
      End If

      Select Case oCleArtv.nNumliv
        Case 1 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & "".PadLeft(CLN__STD.CodartMaxLen)
        Case 2 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & "".PadLeft(CLN__STD.CodartMaxLen)
        Case 3 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3)) & "".PadLeft(CLN__STD.CodartMaxLen)
      End Select

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

  Public Overridable Sub tlbRicalcolaListini_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRicalcolaListini.ItemClick
    Dim oPar As New CLE__CLDP
    Dim nCodvalu As Integer = 0
    Dim dPrezzoBase As Decimal = 0
    Dim strCodart As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edAr_codpdon.Text) = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 128578173195743576, "Inserire il codice relazione listini prima di procedere con l'elaborazione."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not Salva() Then Exit Sub
      '--------------------------------------------------------------------------------------------------------------
      '--- Funziona solo se è indicata la data
      '--------------------------------------------------------------------------------------------------------------
      If ceListini.opValDay.Checked = False Then
        ceListini.opValDay.Checked = True
        oApp.MsgBoxInfo(oApp.Tr(Me, 128582485064375819, "Selezionare la data di validità prima di passare al ricalcolo dei listini."))
        Exit Sub
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Determina il codice articolo x il listino
      '--------------------------------------------------------------------------------------------------------------
      Select Case oCleArtv.strPrevar
        Case "N" : strCodart = oCleArtv.strArtvCodroot
        Case "1" : strCodart = Trim(oCleArtv.strArtvCodroot) & _
                               Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
        Case "S" : strCodart = Trim(oCleArtv.strArtvCodroot) & _
                               Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                               Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                               Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3))
      End Select
      '--------------------------------------------------------------------------------------------------------------
      'oPar.Ditta = DittaCorrente
      'oPar.strNomProg = "BSMGARTV"
      'oPar.dPar1 = 0
      'oPar.dPar2 = NTSCDec(IIf(ceListini.opValEur.Checked, 0, NTSCInt(ceListini.edCodvalu.Text)))
      'oPar.dPar3 = NTSCInt(ceListini.grvList.NTSGetCurrentDataRow!lc_fase)
      'oPar.dPar4 = 1
      'oPar.dPar5 = dPrezzoBase
      'oPar.strPar1 = strCodart
      'oPar.strPar2 = NTSCDate(ceListini.grvList.NTSGetCurrentDataRow!lc_datagg).ToShortDateString
      'oPar.strPar3 = NTSCStr(ceListini.grvList.NTSGetCurrentDataRow!lc_unmis)
      'oPar.strPar4 = NTSCStr(ceListini.grvList.NTSGetCurrentDataRow!lc_netto)
      'oMenu.RunChild("NTSInformatica", "FRMMGRCPR", "", DittaCorrente, "", "BNMGRCPR", oPar, "", True, True)
      '--------------------------------------------------------------------------------------------------------------
      If IsNothing(ceListini.grvList.NTSGetCurrentDataRow) Then Return
      If ceListini.opValEur.Checked = False Then nCodvalu = NTSCInt(ceListini.edCodvalu.Text)
      oCleArtv.AggiornaListini(strCodart, nCodvalu, ceListini.edDtval.Text, 1, True, 1, _
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

  Public Overridable Sub tlbArticoliMagazzino_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbArticoliMagazzino.ItemClick
    Dim oPar As New CLE__CLDP
    Dim strCodart As String = ""
    Try
      If Not Salva() Then Exit Sub

      If grvArtico.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128588515495753408, "Posizionarsi su una riga valida nell'analitico a varianti prima di chiamare la gestione degli articoli magazzino"))
        Exit Sub
      End If

      Select Case oCleArtv.nNumliv
        Case 1 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
        Case 2 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2))
        Case 3 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3))
      End Select

      oPar.strPar1 = "BNMGARTV"
      oPar.bPar1 = False
      oPar.strPar2 = "0"
      oPar.strPar3 = strCodart

      oMenu.RunChild("NTSInformatica", "FRMMGARMA", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbLingua_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbLingua.ItemClick
    Dim oPar As New CLE__CLDP
    Try

      If Not Salva() Then Exit Sub

      oPar.strPar1 = "BNMGARTV"
      oPar.strPar2 = edAr_codart.Text
      oPar.strPar3 = edAr_flmod.Text
      oPar.bPar1 = True

      oMenu.RunChild("NTSInformatica", "FRMMGHLAV", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbAccessoriSuccedanei_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbAccessoriSuccedanei.ItemClick
    Dim strCodart As String = ""
    Dim oParam As New CLE__CLDP
    Dim frmArta As FRMMGARTA = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      If (tlbSalva.Enabled = True) And (tlbSalva.Visible = True) Then
        If Salva() = False Then Return
      Else
        If oCleArtv.bNew = True Then Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case oCleArtv.nNumliv
        Case 1 : strCodart = oCleArtv.strArtvCodroot.Trim & _
                             NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1).Trim
        Case 2 : strCodart = oCleArtv.strArtvCodroot.Trim & _
                             NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1).Trim & _
                             NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2).Trim
        Case 3 : strCodart = oCleArtv.strArtvCodroot.Trim & _
                             NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1).Trim & _
                             NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2).Trim & _
                             NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3).Trim
      End Select
      '--------------------------------------------------------------------------------------------------------------
      frmArta = CType(NTSNewFormModal("FRMMGARTA"), FRMMGARTA)
      frmArta.strArtaCodart = strCodart
      frmArta.strArtaDesart = NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_descr)
      frmArta.Init(oMenu, oParam, DittaCorrente)
      frmArta.InitEntity(oCleArtv)
      frmArta.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
      If oCleArtv.GetTabaext(dttTmp) = False Then
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
      dttTmp = dsArtico.Tables("ARTICO").Clone
      dttTmp.Rows.Add(dsArtico.Tables("ARTICO").Rows(dcArtico.Position).ItemArray)
      '--------------------------------------------------------------------------------------------------------------
      frmAnex = CType(NTSNewFormModal("FRMMGANEX"), FRMMGANEX)
      frmAnex.Init(oMenu, oParam, DittaCorrente)
      frmAnex.InitEntity(oCleArtv)
      frmAnex.dtrArti = dsArtico.Tables("ARTICO").Rows(dcArtico.Position)
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
        With dsArtico.Tables("ARTICO").Rows(dcArtico.Position)
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
      If (bDatiCambiati = True) And (dsArtico.Tables("ARTICO").Rows.Count > 1) Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 130441195098991965, "Attenzione!" & vbCrLf & _
          "Sono state modificate le estensioni relative a questa variante." & vbCrLf & _
          "Propagare le modifiche sulle estensioni delle altre varianti?")) = Windows.Forms.DialogResult.Yes Then
          For i As Integer = 0 To (dsArtico.Tables("ARTICO").Rows.Count - 1)
            With dsArtico.Tables("ARTICO").Rows(i)
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
          Next
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bDatiCambiati = True Then
        Dim nTmp As Integer = NTSCInt(edAr_converp.Text)
        edAr_converp.NTSTextDB = "0"
        edAr_converp.NTSTextDB = "1"
        edAr_converp.NTSTextDB = nTmp.ToString

        'Dim strTmp As String = edAr_um4.Text
        'edAr_um4.NTSTextDB = ""
        'edAr_um4.NTSTextDB = "."
        'edAr_um4.NTSTextDB = strTmp
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
      If ArticoSalva() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If tsArtv.SelectedTabPageIndex <> 1 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130655508117022714, "Attenzione!" & vbCrLf & _
          "Posizionarsi su una riga valida nell'analitico a varianti prima di chiamare la simulazione delle vendite."))
        tsArtv.SelectedTabPageIndex = 1
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130655497148323226, "Attenzione!" & vbCrLf & _
          "Inserire almeno una riga nell'analitico varianti."))
        tsArtv.SelectedTabPageIndex = 1
        Return
      End If
      If grvArtico.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130655497351626506, "Attenzione!" & vbCrLf & _
          "Posizionarsi su una riga valida prima di chiamare la simulazione delle vendite."))
        tsArtv.SelectedTabPageIndex = 1
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case oCleArtv.nNumliv
        Case 1 : oPar.strPar1 = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
        Case 2 : oPar.strPar1 = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                                Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2))
        Case 3 : oPar.strPar1 = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                                Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                                Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3))
      End Select
      '--------------------------------------------------------------------------------------------------------------
      oMenu.RunChild("NTSInformatica", "FRMMGSIMU", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region


#Region "EventiGriglia"
  Public Overridable Sub grvVar1_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvVar1.NTSBeforeRowUpdate
    Try
      If Not Var1Salva() Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvVar1_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvVar1.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleArtv Is Nothing Then Return

      dtrT = grvVar1.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        xx_codvar1.Enabled = True
        Return
      End If

      If NTSCStr(dtrT!xx_codvar1) <> "" Then
        xx_codvar1.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvVar2_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvVar2.NTSBeforeRowUpdate
    Try
      If Not Var2Salva() Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvVar2_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvVar2.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleArtv Is Nothing Then Return

      dtrT = grvVar2.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        xx_codvar2.Enabled = True
        Return
      End If

      If NTSCStr(dtrT!xx_codvar2) <> "" Then
        xx_codvar2.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvVar3_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvVar3.NTSBeforeRowUpdate
    Try
      If Not Var3Salva() Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvVar3_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvVar3.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleArtv Is Nothing Then Return

      dtrT = grvVar3.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        xx_codvar3.Enabled = True
        Return
      End If

      If NTSCStr(dtrT!xx_codvar3) <> "" Then
        xx_codvar3.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvArtico_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvArtico.NTSBeforeRowUpdate
    Try
      If Not ArticoSalva() Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvArtico_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvArtico.NTSFocusedRowChanged
    'blocco le colonne non modificabili
    Dim dtrT As DataRow = Nothing
    Try
      If oCleArtv Is Nothing Then Return

      dtrT = grvArtico.NTSGetCurrentDataRow
      '------------------------------------
      'sono su una nuova riga
      If dtrT Is Nothing Then
        ar_codvar1.Enabled = True
        ar_codvar2.Enabled = True
        ar_codvar3.Enabled = True
        Return
      End If

      If oCleArtv.nNumliv = 1 Then
        If NTSCStr(dtrT!ar_codvar1) <> "" Then
          ar_codvar1.Enabled = False
        End If
      ElseIf oCleArtv.nNumliv = 2 Then
        If NTSCStr(dtrT!ar_codvar1) <> "" Or NTSCStr(dtrT!ar_codvar2) <> "" Then
          ar_codvar1.Enabled = False
          ar_codvar2.Enabled = False
        End If
      ElseIf oCleArtv.nNumliv = 3 Then
        If NTSCStr(dtrT!ar_codvar1) <> "" Or NTSCStr(dtrT!ar_codvar2) <> "" Or NTSCStr(dtrT!ar_codvar3) <> "" Then
          ar_codvar1.Enabled = False
          ar_codvar2.Enabled = False
          ar_codvar3.Enabled = False
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

  Public Overridable Function Apri(ByVal bNew As Boolean) As Boolean
    Dim bResult As Boolean = False
    Dim oParam As New CLE__CLDP
    Dim frmAarv As FRMMGAARV = Nothing
    Dim frmNarv As FRMMGNARV = Nothing
    Dim dttTmp As New DataTable
    Dim strCodRootDaGest As String = ""
    Try

      If Not oCallParams Is Nothing Then
        'APRI DA GEST
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "APRI;" Then

          oCleArtv.strCodart = NTSCStr(Microsoft.VisualBasic.Mid(oCallParams.strParam, 6))
          bNew = False
          'se richiamato apri da gestione con codice articolo salta la modale di apertura
          If oCleArtv.strCodart <> "" Then
            oCleArtv.bDaGest = True
            oCleArtv.GetCodroot(oCleArtv.strCodart, strCodRootDaGest)
            If strCodRootDaGest <> "" Then
              oCleArtv.strArtvCodroot = strCodRootDaGest
            Else
              oCleArtv.strArtvCodroot = oCleArtv.strCodart
            End If
          End If

        End If
      End If

      If bNew Then
        'NUOVO

        '-------------------------------------------------
        'creo un nuovo articolo
        If Not Salva() Then Return False
        SetStato(0)

        oCleArtv.strArtvCodroot = ""
        oCleArtv.nArtvNumliv = 1
        oCleArtv.nArtvLungroot = 1
        oCleArtv.nArtvLungvar1 = 1
        oCleArtv.nArtvLungvar2 = 0
        oCleArtv.nArtvLungvar3 = 0
        oCleArtv.strArtvCodroot = ""
        oCleArtv.strArtvPrevar = "N"
        oCleArtv.strNarvCodvari1 = ""
        oCleArtv.strNarvCodvari2 = ""
        oCleArtv.strNarvCodvari3 = ""

        frmNarv = CType(NTSNewFormModal("FRMMGNARV"), FRMMGNARV)
        frmNarv.Init(oMenu, oParam, DittaCorrente)
        frmNarv.InitEntity(oCleArtv)
        frmNarv.ShowDialog()

        If frmNarv.bNarvAnnullato = True Then Return False

        If oCleArtv.strArtvCodroot = "" Then Return False

        oCleArtv.nNumliv = oCleArtv.nArtvNumliv
        oCleArtv.nLungvar1 = oCleArtv.nArtvLungvar1
        oCleArtv.nLungvar2 = oCleArtv.nArtvLungvar2
        oCleArtv.nLungvar3 = oCleArtv.nArtvLungvar3
        oCleArtv.strPrevar = oCleArtv.strArtvPrevar

        If Not VarNuovo() Then Return False
        If Not ArticoNuovo() Then Return False

        oCleArtv.Nuovo(dsArtv)

        '-------------------------------------------------
        'leggo dal database i dati e collego il NTSBinding
        dcArtv.DataSource = dsArtv.Tables("ARTICO")
        dcArtv.MoveLast()

        '-------------------------------------------------
        'collego il BindingSource ai vari controlli 
        Bindcontrols()
        dcArtv.ResetBindings(False)

        oCleArtv.VarNuovoSalva()

        SetStato(1)

        GctlSetVisEnab(edAr_unmis, False)
        GctlSetVisEnab(edAr_confez2, False)
        GctlSetVisEnab(edAr_unmis2, False)
        GctlSetVisEnab(edAr_um4, False)
        GctlSetVisEnab(edAr_perqta, False)
        If oCleArtv.bLogisticaEstesa Or oCleArtv.bGestUbicSenzaLext Then
          GctlSetVisEnab(ckAr_gesubic, False)
        Else
          ckAr_gesubic.Enabled = False
        End If
        GctlSetVisEnab(ckAr_geslotti, False)
        GctlSetVisEnab(ckAr_gestmatr, False)
        GctlSetVisEnab(cbAr_gescomm, False)
        edAr_codalt.Focus()

      Else
        'APRI
        '-------------------------------------------------
        'apro un nuovo articolo
        If Not Salva() Then Return False
        SetStato(0)

        If (oCleArtv.bDaGest = False) And (Not (oCleArtv.bDuplica = True And oCleArtv.strArtvCodroot <> "")) Then 'SALTA la form modale se chiamato in apertura da gestione APRI;
          frmAarv = CType(NTSNewFormModal("FRMMGAARV"), FRMMGAARV)
          frmAarv.Init(oMenu, oParam, DittaCorrente)
          frmAarv.InitEntity(oCleArtv)
          frmAarv.ShowDialog()

          If frmAarv.bAarvAnnullato = True Then Return False
        End If

        If (oCleArtv.strArtvCodroot = "") Then Return False

        If Not oCleArtv.CheckCodroot(oCleArtv.strArtvCodroot) Then Return False

        ceSconti.TipoSconto = 1
        ceProvvig.TipoProvv = 4
        GctlApplicaDefaultValue()

        If Not VarApri() Then Return False
        If Not ArticoApri() Then Return False

        '-------------------------------------------------
        'leggo dal database i dati e collego il NTSBinding
        If Not oCleArtv.Apri(DittaCorrente, dsArtv) Then Return False
        If dsArtv.Tables("ARTICO").Rows.Count = 0 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128553319361865862, "Nessun articolo trovato."))
          Return False
        End If
        dcArtv.DataSource = dsArtv.Tables("ARTICO")
        dsArtv.AcceptChanges()

        '-------------------------------------------------
        'collego il BindingSource ai vari controlli 
        Bindcontrols()
        dcArtv.ResetBindings(False)
        dcArtv.MoveFirst()

        oCleArtv.strNarvCodvari1 = edAr_flmod.Text
        oCleArtv.strNarvCodvari2 = ""
        oCleArtv.strNarvCodvari3 = ""

        ApriListSconProv()

        SetStato(1)

        edAr_codart.Enabled = False
        edAr_unmis.Enabled = False
        ckAr_gesubic.Enabled = False
        If oCleArtv.bSbloccaFLotto = False Then ckAr_geslotti.Enabled = False
        ckAr_gestmatr.Enabled = False
        'cbAr_gescomm.Enabled = False
        If Not NTSCStr(dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_confez2) = "" Then
          edAr_confez2.Enabled = False
        End If
        If Not NTSCStr(dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_unmis2) = "" Then
          edAr_unmis2.Enabled = False
        End If
        If Not NTSCStr(dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_um4) = "" Then
          edAr_um4.Enabled = False
        End If
        edAr_descr.Focus()

        AbilitaControlli()
        CaricaClassificazione()

      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleArtv.bDuplica = True Then
        '------------------------------------------------------------------------------------------------------------
        '--- Se l'articolo non è stato modificato, prima della duplicazione, forzo lo stato a "Modificato"
        '--- in modo da poter richiamare la TestPresalva.
        '--- In ogni caso, poi, ripristino le modifiche
        '------------------------------------------------------------------------------------------------------------
        If dsArtico.Tables("ARTICO").Rows(0).RowState = DataRowState.Unchanged Then
          oCleArtv.dsShared.Tables("ARTICO").Rows(0).SetModified()
          '----------------------------------------------------------------------------------------------------------
          bResult = oCleArtv.TestPreSalva()
          '----------------------------------------------------------------------------------------------------------
          If tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(5)) Then
            ceListini.Ripristina()
          ElseIf tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(6)) Then
            ceSconti.Ripristina()
          ElseIf tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(7)) Then
            ceProvvig.Ripristina()
          End If
          If tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(0)) Then
            oCleArtv.Var1Ripristina(dcVar1.Position, dcVar1.Filter)
            oCleArtv.Var2Ripristina(dcVar2.Position, dcVar2.Filter)
            oCleArtv.Var3Ripristina(dcVar3.Position, dcVar3.Filter)
          End If
          If tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(1)) Then
            oCleArtv.ArticoRipristina(dcArtico.Position, dcArtico.Filter)
          End If
          If bResult = False Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 129882201316074998, "Duplicazione articolo annullata."))
            Return False
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
        oCleArtv.bDuplicazioneInCorso = True
        Dim frmDuar As FRMMGDUAR = Nothing
        frmDuar = CType(NTSNewFormModal("FRMMGDUAR"), FRMMGDUAR)

        oCleArtv.strCodartDuar = ""
        frmDuar.Init(oMenu, Nothing, DittaCorrente)
        frmDuar.oCleArtv = oCleArtv
        frmDuar.ShowDialog()
        If frmDuar.bOk = False Or oCleArtv.strCodartDuar = "" Then
          'annullato
          tlbRipristina_ItemClick(tlbRipristina, Nothing)
          oCleArtv.bDuplicazioneInCorso = False
          Return False
        End If

        oCleArtv.nArtvLungroot = dsArtico.Tables("ARTICO").Rows(0)!ar_codroot.ToString.Length
        oCleArtv.nArtvLungrootOld = oCleArtv.nArtvLungroot
        oCleArtv.nArtvLungroot = Len(oCleArtv.strCodartDuar)

        SetStato(1)

        GctlTipoDoc = " "
        GctlSetRoules()
        ceListini.LcTipo = GctlTipoDoc   '(sempre dopo la gctlsetroules) serve per forzare la visualizzaz della colonna conto e/o articolo e/o listino
        ceSconti.TipoSconto = 0
        ceProvvig.TipoProvv = 1
        GctlApplicaDefaultValue()

        If Not oCleArtv.DuplicaArticolo(oCleArtv.strCodartDuar, _
                                     frmDuar.ckListini.Checked, frmDuar.ckSconti.Checked, _
                                     frmDuar.ckProvvigioni.Checked, frmDuar.ckArtfasi.Checked) Then
          'ripristino
          oCleArtv.Ripristina(dcArtv.Position, dcArtv.Filter)
          If tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(5)) Then
            ceListini.Ripristina()
          ElseIf tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(6)) Then
            ceSconti.Ripristina()
          ElseIf tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(7)) Then
            ceProvvig.Ripristina()
          End If

          SetStato(0)

          oCleArtv.bDuplicazioneInCorso = False

          Return False
        End If

        SetStatoCopia(1)

        oCleArtv.bDuplicazioneInCorso = False

        CaricaClassificazione()

        oApp.MsgBoxInfo(oApp.Tr(Me, 128473853444846662, "Duplicazione terminata"))
      End If
      '--------------------------------------------------------------------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      If bNew Then
        oCleArtv.bHasChanges = True
        Me.GctlApplicaDefaultValue()
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmAarv Is Nothing Then frmAarv.Dispose()
      frmAarv = Nothing

      If Not frmNarv Is Nothing Then frmNarv.Dispose()
      frmNarv = Nothing
    End Try
  End Function

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If Not Var1Salva() Then Return False
      If oCleArtv.nNumliv >= 2 Then
        If Not Var2Salva() Then Return False
      ElseIf oCleArtv.nNumliv = 3 Then
        If Not Var3Salva() Then Return False
      End If
      If Not ArticoSalva() Then Return False

      If oCleArtv.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128802015710424721, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.No Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleArtv.Salva(False) Then
            Return False
          Else
            AbilitaControlli()
            CaricaClassificazione()
          End If
        End If
      Else
        If Not dsArtico Is Nothing Then
          If nSetStato > 0 Then
            If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128588515596378408, "Inserire almeno una riga nell'analitico varianti."))
              Exit Function
            End If
          End If
        End If
      End If

      '-------------------------------------------------
      'salvo i listini/sconti/provvigioni
      If Not ceListini.Salva Then Return False
      If Not ceSconti.Salva Then Return False
      If Not ceProvvig.Salva Then Return False

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function Var1Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvVar1.NTSSalvaRigaCorrente(dcVar1, oCleArtv.Var1RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleArtv.Var1Salva(False) Then
            Return False
          End If

          ArticoApri() 'dopo aver salvato riapro la griglia articoli articoli

        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleArtv.Var1Ripristina(dcVar1.Position, dcVar1.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function Var2Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvVar2.NTSSalvaRigaCorrente(dcVar2, oCleArtv.Var2RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleArtv.Var2Salva(False) Then
            Return False
          End If

          ArticoApri() 'dopo aver salvato riapro la griglia articoli articoli

        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleArtv.Var2Ripristina(dcVar2.Position, dcVar2.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function Var3Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvVar3.NTSSalvaRigaCorrente(dcVar3, oCleArtv.Var3RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleArtv.Var3Salva(False) Then
            Return False
          End If

          ArticoApri() 'dopo aver salvato riapro la griglia articoli articoli

        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleArtv.Var3Ripristina(dcVar3.Position, dcVar3.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function ArticoSalva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvArtico.NTSSalvaRigaCorrente(dcArtico, oCleArtv.ArticoRecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleArtv.ArticoSalva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleArtv.ArticoRipristina(dcArtico.Position, dcArtico.Filter)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub SetStato(ByVal nStato As Integer)
    Try
      nSetStato = nStato
      '----------------------------------
      '0 = nessuna registraz aperta
      If nSetStato = 0 Then

        ceListini.LcCodart = ""
        ceListini.LcCodartRoot = ""
        ceSconti.SoCodart = ""
        ceSconti.SoCodartRoot = ""
        ceProvvig.PerCodart = ""
        ceProvvig.PerCodartRoot = ""

        '---------------------------------------
        'gestione dei tasti di scelta rapida di listini/sconti/provvigioni
        'visto che i tasti di scelta rapida sono gli stessi, per fare in modo che operino correttamente 
        'devo disabilitare i controlli listini/sconti/provvig che non sono visibili, in questo modo 
        'è abilitata una sola toolbar per volta a parità di tasti di scelta rapida
        ceListini.Enabled = False
        ceSconti.Enabled = False
        ceProvvig.Enabled = False

        pnArtv.Visible = False

        GctlSetVisEnab(tlbNuovo, False)
        GctlSetVisEnab(tlbDuplica, False)

        tlbSalva.Enabled = False
        tlbCancella.Enabled = False
        tlbRipristina.Enabled = False
        tlbZoom.Enabled = False
        tlbRecordNuovo.Enabled = False
        tlbRecordAggiorna.Enabled = False
        tlbRecordRipristina.Enabled = False
        tlbRecordCancella.Enabled = False
        tlbBarcode.Enabled = False
        tlbOle.Enabled = False
        tlbKit.Enabled = False
        tlbStampaEtichette.Enabled = False
        tlbRicalcolaListini.Enabled = False
        tlbArticoliMagazzino.Enabled = False
        tlbLingua.Enabled = False
        tlbSimula.Enabled = False
        tlbAccessoriSuccedanei.Enabled = False

        If Not dsArtv Is Nothing Then
          dsArtv.AcceptChanges()
          oCleArtv.bHasChanges = False
        End If

      Else
        tsArtv.SelectedTabPageIndex = 0

        GctlSetVisEnab(pnArtv, True)

        GctlSetVisEnab(pnArtv, False)
        GctlSetVisEnab(tlbNuovo, False)
        GctlSetVisEnab(tlbApri, False)
        GctlSetVisEnab(tlbSalva, False)
        GctlSetVisEnab(tlbCancella, False)
        GctlSetVisEnab(tlbRipristina, False)
        GctlSetVisEnab(tlbZoom, False)
        GctlSetVisEnab(tlbRecordNuovo, False)
        GctlSetVisEnab(tlbRecordAggiorna, False)
        GctlSetVisEnab(tlbRecordRipristina, False)
        GctlSetVisEnab(tlbRecordCancella, False)
        GctlSetVisEnab(tlbBarcode, False)
        GctlSetVisEnab(tlbOle, False)
        GctlSetVisEnab(tlbKit, False)
        GctlSetVisEnab(tlbStampaEtichette, False)
        tlbRicalcolaListini.Enabled = False
        GctlSetVisEnab(tlbArticoliMagazzino, False)
        GctlSetVisEnab(tlbSimula, False)
        GctlSetVisEnab(tlbAccessoriSuccedanei, False)

        If oCleArtv.bCampiCAEAttivi Then
          GctlSetVisEnab(fmCadc, True)
        Else
          fmCadc.Visible = False
        End If

        If oCleArtv.bModTCO Then
          If oCleArtv.nNumliv = 1 Then GctlSetVisEnab(tlbLingua, False)
          GctlSetVisible(lbAr_flmod)
          GctlSetVisible(edAr_flmod)
          GctlSetVisible(lbXx_flmod)
        End If

        'rende visibili o meno le griglie del primo tab e le colonne del secondo
        'a seconda del numero di livelli selezionati in file nuovo
        Select Case oCleArtv.nNumliv
          Case 1
            grVar2.Visible = False
            grVar3.Visible = False
            lbLivello2.Visible = False
            lbLivello3.Visible = False
            grvArtico.Columns("ar_codvar1").Visible = True
            grvArtico.Columns("ar_codvar2").Visible = False
            grvArtico.Columns("ar_codvar3").Visible = False
            xx_codvar1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802010623349889, "Cod."), oCleArtv.nLungvar1, True)
            ar_codvar1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012733891991, "1°livello"), oCleArtv.nLungvar1, True)
          Case 2
            grVar2.Visible = True
            grVar3.Visible = False
            GctlSetVisEnab(lbLivello2, True)
            lbLivello3.Visible = False
            grvArtico.Columns("ar_codvar1").Visible = True
            grvArtico.Columns("ar_codvar2").Visible = True
            grvArtico.Columns("ar_codvar3").Visible = False
            xx_codvar1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802010655072090, "Cod."), oCleArtv.nLungvar1, True)
            xx_codvar2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802011564858564, "Cod."), oCleArtv.nLungvar2, True)
            ar_codvar1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012766708061, "1°livello"), oCleArtv.nLungvar1, True)
            ar_codvar2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012782803562, "2°livello"), oCleArtv.nLungvar2, True)
          Case 3
            grVar2.Visible = True
            grVar3.Visible = True
            GctlSetVisEnab(lbLivello2, True)
            GctlSetVisEnab(lbLivello3, True)
            grvArtico.Columns("ar_codvar1").Visible = True
            grvArtico.Columns("ar_codvar2").Visible = True
            grvArtico.Columns("ar_codvar3").Visible = True
            xx_codvar1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802010690700966, "Cod."), oCleArtv.nLungvar1, True)
            xx_codvar2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802011608769591, "Cod."), oCleArtv.nLungvar2, True)
            xx_codvar3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802011639554190, "Cod."), oCleArtv.nLungvar3, True)
            ar_codvar1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012812494292, "1°livello"), oCleArtv.nLungvar1, True)
            ar_codvar2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012827183390, "2°livello"), oCleArtv.nLungvar2, True)
            ar_codvar3.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128802012842653823, "3°livello"), oCleArtv.nLungvar3, True)
        End Select

      End If

      edAr_codart.Enabled = False

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
      ckAr_gesubic.Enabled = False
      If oCleArtv.bSbloccaFLotto = False Then ckAr_geslotti.Enabled = False
      ckAr_gestmatr.Enabled = False
      'cbAr_gescomm.Enabled = False
      If Not NTSCStr(dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_confez2) = "" Then
        edAr_confez2.Enabled = False
      Else
        GctlSetVisEnab(edAr_confez2, False)
      End If
      If Not NTSCStr(dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_unmis2) = "" Then
        edAr_unmis2.Enabled = False
      Else
        GctlSetVisEnab(edAr_unmis2, False)
      End If
      If Not NTSCStr(dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_um4) = "" Then
        edAr_um4.Enabled = False
      Else
        GctlSetVisEnab(edAr_um4, False)
      End If
      edAr_perqta.Enabled = False
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

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ApriListSconProv()
    Dim strListScontProvArt As String = ""
    Try
      'setto l'articolo a seconda delle varianti
      Select Case oCleArtv.nNumliv
        Case 1 : strListScontProvArt = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
        Case 2 : strListScontProvArt = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2))
        Case 3 : strListScontProvArt = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                             Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3))
      End Select

      '-------------------------------
      'se serve ricarico i listini
      If tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(5)) Then
        If ceListini.LcCodart <> strListScontProvArt Then
          ceListini.LcCodart = strListScontProvArt
          ceListini.LcCodartRoot = Trim(oCleArtv.strArtvCodroot)
          ceListini.ApriListini()
        End If

        GctlSetVisEnab(ceListini, False)
        GctlSetVisEnab(ceListini.grvList, False)
        ceListini.tlbMain.Visible = True
      End If

      '-------------------------------
      'se serve ricarico gli sconti
      If tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(6)) Then
        If ceSconti.SoCodart <> strListScontProvArt Then
          ceSconti.SoCodart = strListScontProvArt
          ceSconti.SoCodartRoot = Trim(oCleArtv.strArtvCodroot)
          ceSconti.ApriSconti()
        End If

        GctlSetVisEnab(ceSconti, False)
        GctlSetVisEnab(ceSconti.grvSconti, False)
        ceSconti.tlbMain.Visible = True
      End If

      '-------------------------------
      'se serve ricarico le provvigioni
      If tsArtv.SelectedTabPage.Equals(tsArtv.TabPages(7)) Then
        If ceProvvig.PerCodart <> strListScontProvArt Then
          ceProvvig.PerCodart = strListScontProvArt
          ceProvvig.PerCodartRoot = Trim(oCleArtv.strArtvCodroot)
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

  Public Overridable Sub cbAr_perragg_SelectedvalueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAr_perragg.SelectedValueChanged
    Try
      If dsArtv Is Nothing Then Return
      If cbAr_perragg.SelectedValue Is Nothing Then Return
      'dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_perragg = cbAr_perragg.SelectedValue
      If NTSCStr(cbAr_perragg.SelectedValue) = "G" Then
        dsArtv.Tables("ARTICO").Rows(dcArtv.Position)!ar_ggragg = "1"
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

  Public Overridable Sub edAr_descr_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles edAr_descr.Enter
    Try
      strOldDEscr = edAr_descr.Text
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub edAr_descr_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles edAr_descr.Validating
    Dim strTmp As String = ""
    Dim dtrTmp() As DataRow = Nothing

    Try
      If strOldDEscr = "" Then Return
      If NTSCStr(edAr_descr.Text).Trim = "" Then Return
      If NTSCStr(edAr_descr.Text).Trim.ToLower = NTSCStr(strOldDEscr).Trim.ToLower Then Return
      If dsArtico.Tables("ARTICO").Rows.Count > 0 Then
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129538312858203125, "Modifico la descrizione su tutte le varianti?")) = Windows.Forms.DialogResult.No Then Return
        For Each dtrT As DataRow In dsArtico.Tables("ARTICO").Rows
          strTmp = edAr_descr.Text
          If NTSCStr(dtrT!ar_codvar1).Trim <> "" Then
            dtrTmp = dsVar1.Tables("ARTVAR").Select("xx_codvar1 = " & CStrSQL(dtrT!ar_codvar1))
            If dtrTmp.Length > 0 Then strTmp += " " & NTSCStr(dtrTmp(0)!xx_descr1).Trim
          End If
          If NTSCStr(dtrT!ar_codvar2).Trim <> "" Then
            dtrTmp = dsVar2.Tables("ARTVAR").Select("xx_codvar2 = " & CStrSQL(dtrT!ar_codvar2))
            If dtrTmp.Length > 0 Then strTmp += " " & NTSCStr(dtrTmp(0)!xx_descr2).Trim
          End If
          If NTSCStr(dtrT!ar_codvar3).Trim <> "" Then
            dtrTmp = dsVar3.Tables("ARTVAR").Select("xx_codvar3 = " & CStrSQL(dtrT!ar_codvar3))
            If dtrTmp.Length > 0 Then strTmp += " " & NTSCStr(dtrTmp(0)!xx_descr3).Trim
          End If
          strTmp = strTmp.Trim
          dtrT!ar_descr = Microsoft.VisualBasic.Left(strTmp, 40).Trim
          dtrT!ar_desint = Microsoft.VisualBasic.Mid(strTmp, 41).Trim
        Next
        ArticoSalva()
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
    Dim dRes As DialogResult
    Dim strNomeFile As String = ""
    Dim strPathFile As String = ""
    Dim nPosSep As Integer = 0
    Dim i As Integer
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se non esiste la cartella delle immagini chiede di crearla
      '-----------------------------------------------------------------------------------------
      If Not Directory.Exists(oCleArtv.strImageDir) Then
        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128559133252059319, "La cartella delle immagini non esiste. Crearla?"))
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          Try
            MkDir(oCleArtv.strImageDir)
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
      OpenFileDialog1.InitialDirectory = oCleArtv.strImageDir
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
        If strNomeFile.Length > 50 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130374521581617698, "Attenzione!" & vbCrLf & _
            "Il nome/estensione de file indicato non può surerare i 50 caratteri."))
          Exit Function
        End If
        '---------------------------------------------------------------------------------------
        '--- Inseridce nel TextBox relativo il file immagine selezionato
        '---------------------------------------------------------------------------------------
        If nQualegif = 1 Then
          edAr_gif1.NTSTextDB = strNomeFile
        Else ' = 2
          edAr_gif2.NTSTextDB = strNomeFile
        End If

        '---------------------------------------------------------------------------------------
        '--- Se l'immagine selezionata non è nella cartella delle immagini
        '--- allora la copio nella cartella delle immagini
        '---------------------------------------------------------------------------------------
        If UCase(strPathFile) <> UCase(oCleArtv.strImageDir) Then
          If Not System.IO.File.Exists(oCleArtv.strImageDir & strNomeFile) Then
            Try
              FileCopy(OpenFileDialog1.FileName, oCleArtv.strImageDir & strNomeFile)
            Catch ex As Exception
              Exit Function
            End Try
          End If
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub tsArtv_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tsArtv.SelectedPageChanged
    Dim strCodart As String = ""
    Dim strCodartVar As String = ""
    Try
      '---------------------------------------
      'gestione dei tasti di scelta rapida di listini/sconti/provvigioni
      'visto che i tasti di scelta rapida sono gli stessi, per fare in modo che operino correttamente 
      'devo disabilitare i controlli listini/sconti/provvig che non sono visibili, in questo modo 
      'è abilitata una sola toolbar per volta a parità di tasti di scelta rapida

      If e.Page.Equals(tsArtv.TabPages(5)) Then
        GctlSetVisEnab(tlbRicalcolaListini, False) 'abilita il ricalcolo listini
      Else
        tlbRicalcolaListini.Enabled = False
      End If

      '-----------------------------
      'pagina dei listini
      If e.Page.Equals(tsArtv.TabPages(5)) Then
        If oCleArtv.bNew Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128466988465551088, "Salvare il nuovo articolo prima di passare alla cartella LISTINI"))
          tsArtv.SelectedTabPageIndex = 0
          Return
        End If

        'controllo di essere posizionati su una riga del analitico varianti
        If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128802016358097047, "Inserire almeno una riga nell'analitico varianti."))
          tsArtv.SelectedTabPageIndex = 1
          Return
        End If
        If grvArtico.NTSGetCurrentDataRow Is Nothing Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128802016180124245, "Posizionarsi su una riga valida nell'analitico a varianti prima di chiamare la gestione dei listini."))
          tsArtv.SelectedTabPageIndex = 1
          Return
        End If
        'setto l'articolo a seconda delle varianti
        Select Case oCleArtv.nNumliv
          Case 1 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
          Case 2 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                               Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2))
          Case 3 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                               Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                               Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3))
        End Select

        Select Case NTSCStr(dsArtv.Tables("ARTICO").Rows(0)!ar_prevar)
          Case "N" : strCodartVar = Trim(oCleArtv.strArtvCodroot)
          Case "S" : strCodartVar = strCodart
          Case "1" : strCodartVar = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
        End Select

        lbArtListini.Text = oApp.Tr(Me, 130427164686175968, "Art.: |" & NTSCStr(strCodartVar) & "|")

        ceListini.LcCodart = NTSCStr(strCodart)
        ceListini.LcCodartRoot = Trim(oCleArtv.strArtvCodroot)
        ceListini.ApriListini()

        GctlSetVisEnab(ceListini, False)
        GctlSetVisEnab(ceListini.grvList, False)
        ceListini.tlbMain.Visible = True

      End If

      If e.PrevPage.Equals(tsArtv.TabPages(5)) Then
        If Not ceListini.Salva Then
          tsArtv.SelectedTabPageIndex = 5
        Else
          ceListini.Enabled = False
        End If
      End If

      '-----------------------------
      'pagina degli sconti
      If e.Page.Equals(tsArtv.TabPages(6)) Then
        If oCleArtv.bNew Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128473931444347256, "Salvare il nuovo articolo prima di passare alla cartella SCONTI"))
          tsArtv.SelectedTabPageIndex = 0
          Return
        End If

        'controllo di essere posizionati su una riga del analitico varianti
        If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128802016411378297, "Inserire almeno una riga nell'analitico varianti."))
          tsArtv.SelectedTabPageIndex = 1
          Return
        End If
        If grvArtico.NTSGetCurrentDataRow Is Nothing Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128802016083246765, "Posizionarsi su una riga valida nell'analitico a varianti prima di chiamare la gestione degli sconti."))
          tsArtv.SelectedTabPageIndex = 1
          Return
        End If

        'setto l'articolo a seconda delle varianti
        Select Case oCleArtv.nNumliv
          Case 1 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
          Case 2 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                               Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2))
          Case 3 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                               Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                               Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3))
        End Select

        Select Case NTSCStr(dsArtv.Tables("ARTICO").Rows(0)!ar_prevar)
          Case "N" : strCodartVar = Trim(oCleArtv.strArtvCodroot)
          Case "S" : strCodartVar = strCodart
          Case "1" : strCodartVar = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
        End Select

        lbArtSconti.Text = oApp.Tr(Me, 130427182198631797, "Art.: |" & NTSCStr(strCodartVar) & "|")

        If ceSconti.SoCodart <> NTSCStr(strCodart) Then
          ceSconti.SoCodart = NTSCStr(strCodart)
          ceSconti.SoCodartRoot = Trim(oCleArtv.strArtvCodroot)
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

      If e.PrevPage.Equals(tsArtv.TabPages(6)) Then
        If Not ceSconti.Salva Then
          tsArtv.SelectedTabPageIndex = 6
        Else
          ceSconti.Enabled = False
        End If
      End If

      '-----------------------------
      'pagina delle provvigioni
      If e.Page.Equals(tsArtv.TabPages(7)) Then
        If oCleArtv.bNew Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 128473931950723256, "Salvare il nuovo articolo prima di passare alla cartella PROVVIGIONI"))
          tsArtv.SelectedTabPageIndex = 0
          Return
        End If

        'controllo di essere posizionati su una riga del analitico varianti
        If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128802016324346687, "Inserire almeno una riga nell'analitico varianti."))
          tsArtv.SelectedTabPageIndex = 1
          Return
        End If
        If grvArtico.NTSGetCurrentDataRow Is Nothing Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128802016117153883, "Posizionarsi su una riga valida nell'analitico a varianti prima di chiamare la gestione delle provvigioni."))
          tsArtv.SelectedTabPageIndex = 1
          Return
        End If
        'setto l'articolo a seconda delle varianti
        Select Case oCleArtv.nNumliv
          Case 1 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
          Case 2 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                               Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2))
          Case 3 : strCodart = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1)) & _
                               Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar2)) & _
                               Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar3))
        End Select

        Select Case NTSCStr(dsArtv.Tables("ARTICO").Rows(0)!ar_prevar)
          Case "N" : strCodartVar = Trim(oCleArtv.strArtvCodroot)
          Case "S" : strCodartVar = strCodart
          Case "1" : strCodartVar = Trim(oCleArtv.strArtvCodroot) & Trim(NTSCStr(grvArtico.NTSGetCurrentDataRow!ar_codvar1))
        End Select

        lbArtProvvigioni.Text = oApp.Tr(Me, 130427165260242142, "Art.: |" & NTSCStr(strCodartVar) & "|")

        If ceProvvig.PerCodart <> NTSCStr(strCodart) Then
          ceProvvig.PerCodart = NTSCStr(strCodart)
          ceProvvig.PerCodartRoot = Trim(oCleArtv.strArtvCodroot)
          ceProvvig.TipoProvv = 4   'serve per far applicare le impostazioni di griglia e ricaricare le provvigioni
        Else
          ceProvvig.ApriProvvigioni()
        End If

        GctlSetVisEnab(ceProvvig, False)
        GctlSetVisEnab(ceProvvig.grvProv, False)
        ceProvvig.tlbMain.Visible = True
      End If

      If e.PrevPage.Equals(tsArtv.TabPages(7)) Then
        If Not ceProvvig.Salva Then
          tsArtv.SelectedTabPageIndex = 7
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

  Public Overridable Function VarNuovo() As Boolean
    Try
      'carico i dati nelle griglie varianti
      If Not oCleArtv.VarNuovo(dsVar1, dsVar2, dsVar3) Then Return False

      dcVar1.DataSource = Nothing
      dcVar1.DataSource = dsVar1.Tables("ARTVAR")
      dsVar1.AcceptChanges()
      grVar1.DataSource = dcVar1

      If oCleArtv.nNumliv >= 2 Then
        dcVar2.DataSource = Nothing
        dcVar2.DataSource = dsVar2.Tables("ARTVAR")
        dsVar2.AcceptChanges()
        grVar2.DataSource = dcVar2
      End If

      If oCleArtv.nNumliv = 3 Then
        dcVar3.DataSource = Nothing
        dcVar3.DataSource = dsVar3.Tables("ARTVAR")
        dsVar3.AcceptChanges()
        grVar3.DataSource = dcVar3
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function ArticoNuovo() As Boolean
    Try
      'carico i dati nella griglia di analitico varianti
      If Not oCleArtv.ArticoNuovo(dsArtico) Then Return False

      dcArtico.DataSource = Nothing
      dcArtico.DataSource = dsArtico.Tables("ARTICO")
      dsArtico.AcceptChanges()
      grArtico.DataSource = dcArtico

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Function VarApri() As Boolean
    Try
      'carico i dati nelle griglie varianti
      If Not oCleArtv.VarApri(DittaCorrente, dsVar1, dsVar2, dsVar3) Then Return False

      dcVar1.DataSource = Nothing
      dcVar1.DataSource = dsVar1.Tables("ARTVAR")
      dsVar1.AcceptChanges()
      grVar1.DataSource = dcVar1

      If oCleArtv.nNumliv >= 2 Then
        dcVar2.DataSource = Nothing
        dcVar2.DataSource = dsVar2.Tables("ARTVAR")
        dsVar2.AcceptChanges()
        grVar2.DataSource = dcVar2
      End If

      If oCleArtv.nNumliv = 3 Then
        dcVar3.DataSource = Nothing
        dcVar3.DataSource = dsVar3.Tables("ARTVAR")
        dsVar3.AcceptChanges()
        grVar3.DataSource = dcVar3
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function ArticoApri() As Boolean
    Try
      'carico i dati nella griglia di analitico varianti
      If Not oCleArtv.ArticoApri(DittaCorrente, dsArtico) Then Return False

      dcArtico.DataSource = Nothing
      dcArtico.DataSource = dsArtico.Tables("ARTICO")
      dsArtico.AcceptChanges()
      grArtico.DataSource = dcArtico

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub edAr_coddb_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edAr_coddb.NTSZoomGest
    Dim strParam As String
    Dim strCodart As String
    Try

      If e.TipoEvento = "OPEN" Then

        If Not Salva() Then
          e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
          Exit Sub
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

  Public Overridable Sub ckAr_staetip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAr_staetip.CheckedChanged
    Try
      edAr_converp.Enabled = ckAr_staetip.Checked
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub SetStatoCopia(ByVal nStato As Integer)
    Try
      If nSetStato = 0 Then
      Else
        Select Case oCleArtv.nNumliv
          Case 1
            grvArtico.Columns("ar_codvar1").Visible = True
            grvArtico.Columns("ar_codvar2").Visible = False
            grvArtico.Columns("ar_codvar3").Visible = False
          Case 2
            grvArtico.Columns("ar_codvar1").Visible = True
            grvArtico.Columns("ar_codvar2").Visible = True
            grvArtico.Columns("ar_codvar3").Visible = False
          Case 3
            grvArtico.Columns("ar_codvar1").Visible = True
            grvArtico.Columns("ar_codvar2").Visible = True
            grvArtico.Columns("ar_codvar3").Visible = True
        End Select
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function CaricaClassificazione() As Boolean
    'dati i campi di artico decodifico la classificazione
    Try
      With dsArtv.Tables("ARTICO").Rows(dcArtv.Position)
        lbClassifica.Text = oCleArtv.GetArtclasDescr(NTSCStr(!ar_codcla1).Trim, NTSCStr(!ar_codcla2).Trim, _
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

      tsArtv.SelectedTabPage = NtsTabPage7
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

      tsArtv.SelectedTabPage = NtsTabPage6
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