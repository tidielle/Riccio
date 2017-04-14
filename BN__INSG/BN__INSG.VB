Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__INSG
  Public dsInsg As DataSet
  Public dcInsg As BindingSource = New BindingSource

  Public oCleInsg As CLE__INSG
  Public oCallParams As CLE__CLDP

  Public bInLoad As Boolean = True
  Public nVecchiaLenSerie As Integer = 0
  Public nVecchiaLenCodart As Integer = 0
  Public nSerieEsistente As Integer = 0
  Public nCodartEsistente As Integer = 0

#Region "Moduli"
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
#End Region

#Region "Dichiarazione Controlli"
  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tsInsg As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage5 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage6 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage7 As NTSInformatica.NTSTabPage
  Public WithEvents pnPag1 As NTSInformatica.NTSPanel
  Public WithEvents pnPag1Left As NTSInformatica.NTSPanel
  Public WithEvents pnPag1Right As NTSInformatica.NTSPanel
  Public WithEvents ckTb_mod2_15 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod2_14 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod2_13 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod2_12 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod2_11 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod2_10 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod2_9 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod2_8 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod2_7 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod2_6 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag1Center As NTSInformatica.NTSPanel
  Public WithEvents ckTb_mod2_5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod2_4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod2_3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod2_2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod2_1 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_15 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_14 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_13 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_12 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_11 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_10 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_9 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_8 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_7 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_6 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod_1 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag2 As NTSInformatica.NTSPanel
  Public WithEvents pnPag2Right As NTSInformatica.NTSPanel
  Public WithEvents ckTb_mod4_15 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod4_14 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod4_13 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod4_12 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod4_11 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod4_10 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod4_9 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod4_8 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod4_7 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod4_6 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag2Center As NTSInformatica.NTSPanel
  Public WithEvents ckTb_mod4_5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod4_4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod4_3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod4_2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod4_1 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod3_15 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod3_14 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod3_13 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod3_12 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod3_11 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag2Left As NTSInformatica.NTSPanel
  Public WithEvents ckTb_mod3_10 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod3_9 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod3_8 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod3_7 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod3_6 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod3_5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod3_4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod3_3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod3_2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_mod3_1 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag3 As NTSInformatica.NTSPanel
  Public WithEvents edTb_annopaghe As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_anno770 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_annodich As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTb_opz_1 As NTSInformatica.NTSComboBox
  Public WithEvents edTb_destcorr As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_destresan As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_destsedel As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_destdomf As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckXx_codditcsc As NTSInformatica.NTSCheckBox
  Public WithEvents lbTb_anno770 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_annodich As NTSInformatica.NTSLabel
  Public WithEvents lbTb_opz_1 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_destcorr As NTSInformatica.NTSLabel
  Public WithEvents lbTb_destresan As NTSInformatica.NTSLabel
  Public WithEvents lbTb_destsedel As NTSInformatica.NTSLabel
  Public WithEvents lbTb_destdomf As NTSInformatica.NTSLabel
  Public WithEvents pnPag4 As NTSInformatica.NTSPanel
  Public WithEvents pnPag4Right As NTSInformatica.NTSPanel
  Public WithEvents ckTb_modsup_30 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_29 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_28 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_27 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_26 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_25 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_24 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_23 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_22 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_21 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag4Center As NTSInformatica.NTSPanel
  Public WithEvents ckTb_modsup_20 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_19 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_18 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_17 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_16 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_15 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_14 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_13 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_12 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_11 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag4Left As NTSInformatica.NTSPanel
  Public WithEvents ckTb_modsup_10 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_9 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_8 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_7 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_6 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsup_1 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag5 As NTSInformatica.NTSPanel
  Public WithEvents pnPag5Right As NTSInformatica.NTSPanel
  Public WithEvents ckTb_modsupext_30 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_29 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_28 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_27 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_26 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_25 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_24 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_23 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_22 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_21 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag5Center As NTSInformatica.NTSPanel
  Public WithEvents ckTb_modsupext_20 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_19 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_18 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_17 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_16 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_15 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_14 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_13 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_12 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_11 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag5Left As NTSInformatica.NTSPanel
  Public WithEvents ckTb_modsupext_10 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_9 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_8 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_7 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_6 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modsupext_1 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag6 As NTSInformatica.NTSPanel
  Public WithEvents pnPag6Right As NTSInformatica.NTSPanel
  Public WithEvents ckTb_modptn_30 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_29 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_28 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_27 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_26 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_25 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_24 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_23 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_22 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_21 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag6Center As NTSInformatica.NTSPanel
  Public WithEvents ckTb_modptn_20 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_19 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_18 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_17 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_16 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_15 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_14 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_13 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_12 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_11 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag6Left As NTSInformatica.NTSPanel
  Public WithEvents ckTb_modptn_10 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_9 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_8 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_7 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_6 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptn_1 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag7 As NTSInformatica.NTSPanel
  Public WithEvents pnPag7Right As NTSInformatica.NTSPanel
  Public WithEvents ckTb_modptnext_30 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_29 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_28 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_27 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_26 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_25 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_24 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_23 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_22 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_21 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag7Center As NTSInformatica.NTSPanel
  Public WithEvents ckTb_modptnext_20 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_19 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_18 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_17 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_16 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_15 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_14 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_13 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_12 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_11 As NTSInformatica.NTSCheckBox
  Public WithEvents pnPag7Left As NTSInformatica.NTSPanel
  Public WithEvents ckTb_modptnext_10 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_9 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_8 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_7 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_6 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_modptnext_1 As NTSInformatica.NTSCheckBox
  Public WithEvents lbTb_seriemaxlen As NTSInformatica.NTSLabel
  Public WithEvents edTb_seriemaxlen As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsTabPage8 As NTSInformatica.NTSTabPage
  Public WithEvents pnPag8 As NTSInformatica.NTSPanel
  Public WithEvents cbSc6 As NTSInformatica.NTSComboBox
  Public WithEvents cbSc5 As NTSInformatica.NTSComboBox
  Public WithEvents cbSc4 As NTSInformatica.NTSComboBox
  Public WithEvents cbSc3 As NTSInformatica.NTSComboBox
  Public WithEvents cbSc2 As NTSInformatica.NTSComboBox
  Public WithEvents cbSc1 As NTSInformatica.NTSComboBox
  Public WithEvents ckSc6 As NTSInformatica.NTSCheckBox
  Public WithEvents ckSc5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckSc4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckSc3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckSc2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckSc1 As NTSInformatica.NTSCheckBox
  Public WithEvents lbPrior As NTSInformatica.NTSLabel
  Public WithEvents cbScPrior As NTSInformatica.NTSComboBox
  Public WithEvents ckSconti As NTSInformatica.NTSCheckBox
  Public WithEvents ckSconClCliDaList As NTSInformatica.NTSCheckBox
  Public WithEvents ckSconSpecNoCum As NTSInformatica.NTSCheckBox
  Public WithEvents lbSc1 As NTSInformatica.NTSLabel
  Public WithEvents lbSc6 As NTSInformatica.NTSLabel
  Public WithEvents lbSc5 As NTSInformatica.NTSLabel
  Public WithEvents lbSc4 As NTSInformatica.NTSLabel
  Public WithEvents lbSc3 As NTSInformatica.NTSLabel
  Public WithEvents lbSc2 As NTSInformatica.NTSLabel
  Public WithEvents ckSconGenex As NTSInformatica.NTSCheckBox
  Public WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_maxlencodart As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_maxlencodart As NTSInformatica.NTSLabel
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
#End Region

#Region "Inizializzazione"
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__INSG", "BE__INSG", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128300150998106752, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleInsg = CType(oTmp, CLE__INSG)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__INSG", strRemoteServer, strRemotePort)
    AddHandler oCleInsg.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleInsg.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------

    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__INSG))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tsInsg = New NTSInformatica.NTSTabControl
    Me.NtsTabPage8 = New NTSInformatica.NTSTabPage
    Me.pnPag8 = New NTSInformatica.NTSPanel
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    Me.ckSconGenex = New NTSInformatica.NTSCheckBox
    Me.lbSc6 = New NTSInformatica.NTSLabel
    Me.lbSc5 = New NTSInformatica.NTSLabel
    Me.lbSc4 = New NTSInformatica.NTSLabel
    Me.lbSc3 = New NTSInformatica.NTSLabel
    Me.lbSc2 = New NTSInformatica.NTSLabel
    Me.lbSc1 = New NTSInformatica.NTSLabel
    Me.ckSconClCliDaList = New NTSInformatica.NTSCheckBox
    Me.ckSconSpecNoCum = New NTSInformatica.NTSCheckBox
    Me.ckSconti = New NTSInformatica.NTSCheckBox
    Me.lbPrior = New NTSInformatica.NTSLabel
    Me.cbScPrior = New NTSInformatica.NTSComboBox
    Me.cbSc6 = New NTSInformatica.NTSComboBox
    Me.cbSc5 = New NTSInformatica.NTSComboBox
    Me.cbSc4 = New NTSInformatica.NTSComboBox
    Me.cbSc3 = New NTSInformatica.NTSComboBox
    Me.cbSc2 = New NTSInformatica.NTSComboBox
    Me.cbSc1 = New NTSInformatica.NTSComboBox
    Me.ckSc6 = New NTSInformatica.NTSCheckBox
    Me.ckSc5 = New NTSInformatica.NTSCheckBox
    Me.ckSc4 = New NTSInformatica.NTSCheckBox
    Me.ckSc3 = New NTSInformatica.NTSCheckBox
    Me.ckSc2 = New NTSInformatica.NTSCheckBox
    Me.ckSc1 = New NTSInformatica.NTSCheckBox
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnPag3 = New NTSInformatica.NTSPanel
    Me.edTb_maxlencodart = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_maxlencodart = New NTSInformatica.NTSLabel
    Me.edTb_seriemaxlen = New NTSInformatica.NTSTextBoxNum
    Me.edTb_annopaghe = New NTSInformatica.NTSTextBoxNum
    Me.edTb_anno770 = New NTSInformatica.NTSTextBoxNum
    Me.edTb_annodich = New NTSInformatica.NTSTextBoxNum
    Me.cbTb_opz_1 = New NTSInformatica.NTSComboBox
    Me.edTb_destcorr = New NTSInformatica.NTSTextBoxNum
    Me.edTb_destresan = New NTSInformatica.NTSTextBoxNum
    Me.edTb_destsedel = New NTSInformatica.NTSTextBoxNum
    Me.edTb_destdomf = New NTSInformatica.NTSTextBoxNum
    Me.ckXx_codditcsc = New NTSInformatica.NTSCheckBox
    Me.lbTb_anno770 = New NTSInformatica.NTSLabel
    Me.lbTb_annodich = New NTSInformatica.NTSLabel
    Me.lbTb_opz_1 = New NTSInformatica.NTSLabel
    Me.lbTb_destcorr = New NTSInformatica.NTSLabel
    Me.lbTb_destresan = New NTSInformatica.NTSLabel
    Me.lbTb_destsedel = New NTSInformatica.NTSLabel
    Me.lbTb_seriemaxlen = New NTSInformatica.NTSLabel
    Me.lbTb_destdomf = New NTSInformatica.NTSLabel
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnPag1 = New NTSInformatica.NTSPanel
    Me.pnPag1Right = New NTSInformatica.NTSPanel
    Me.ckTb_mod2_15 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod2_14 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod2_13 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod2_12 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod2_11 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod2_10 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod2_9 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod2_8 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod2_7 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod2_6 = New NTSInformatica.NTSCheckBox
    Me.pnPag1Center = New NTSInformatica.NTSPanel
    Me.ckTb_mod2_5 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod2_4 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod2_3 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod2_2 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod2_1 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod_15 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod_14 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod_13 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod_12 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod_11 = New NTSInformatica.NTSCheckBox
    Me.pnPag1Left = New NTSInformatica.NTSPanel
    Me.ckTb_mod_10 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod_9 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod_8 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod_7 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod_6 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod_5 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod_4 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod_3 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod_2 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod_1 = New NTSInformatica.NTSCheckBox
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnPag2 = New NTSInformatica.NTSPanel
    Me.pnPag2Right = New NTSInformatica.NTSPanel
    Me.ckTb_mod4_15 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod4_14 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod4_13 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod4_12 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod4_11 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod4_10 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod4_9 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod4_8 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod4_7 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod4_6 = New NTSInformatica.NTSCheckBox
    Me.pnPag2Center = New NTSInformatica.NTSPanel
    Me.ckTb_mod4_5 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod4_4 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod4_3 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod4_2 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod4_1 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod3_15 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod3_14 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod3_13 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod3_12 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod3_11 = New NTSInformatica.NTSCheckBox
    Me.pnPag2Left = New NTSInformatica.NTSPanel
    Me.ckTb_mod3_10 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod3_9 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod3_8 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod3_7 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod3_6 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod3_5 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod3_4 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod3_3 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod3_2 = New NTSInformatica.NTSCheckBox
    Me.ckTb_mod3_1 = New NTSInformatica.NTSCheckBox
    Me.NtsTabPage4 = New NTSInformatica.NTSTabPage
    Me.pnPag4 = New NTSInformatica.NTSPanel
    Me.pnPag4Right = New NTSInformatica.NTSPanel
    Me.ckTb_modsup_30 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_29 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_28 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_27 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_26 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_25 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_24 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_23 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_22 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_21 = New NTSInformatica.NTSCheckBox
    Me.pnPag4Center = New NTSInformatica.NTSPanel
    Me.ckTb_modsup_20 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_19 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_18 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_17 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_16 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_15 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_14 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_13 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_12 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_11 = New NTSInformatica.NTSCheckBox
    Me.pnPag4Left = New NTSInformatica.NTSPanel
    Me.ckTb_modsup_10 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_9 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_8 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_7 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_6 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_5 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_4 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_3 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_2 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsup_1 = New NTSInformatica.NTSCheckBox
    Me.NtsTabPage5 = New NTSInformatica.NTSTabPage
    Me.pnPag5 = New NTSInformatica.NTSPanel
    Me.pnPag5Right = New NTSInformatica.NTSPanel
    Me.ckTb_modsupext_30 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_29 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_28 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_27 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_26 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_25 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_24 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_23 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_22 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_21 = New NTSInformatica.NTSCheckBox
    Me.pnPag5Center = New NTSInformatica.NTSPanel
    Me.ckTb_modsupext_20 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_19 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_18 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_17 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_16 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_15 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_14 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_13 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_12 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_11 = New NTSInformatica.NTSCheckBox
    Me.pnPag5Left = New NTSInformatica.NTSPanel
    Me.ckTb_modsupext_10 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_9 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_8 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_7 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_6 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_5 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_4 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_3 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_2 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modsupext_1 = New NTSInformatica.NTSCheckBox
    Me.NtsTabPage6 = New NTSInformatica.NTSTabPage
    Me.pnPag6 = New NTSInformatica.NTSPanel
    Me.pnPag6Right = New NTSInformatica.NTSPanel
    Me.ckTb_modptn_30 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_29 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_28 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_27 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_26 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_25 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_24 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_23 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_22 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_21 = New NTSInformatica.NTSCheckBox
    Me.pnPag6Center = New NTSInformatica.NTSPanel
    Me.ckTb_modptn_20 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_19 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_18 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_17 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_16 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_15 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_14 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_13 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_12 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_11 = New NTSInformatica.NTSCheckBox
    Me.pnPag6Left = New NTSInformatica.NTSPanel
    Me.ckTb_modptn_10 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_9 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_8 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_7 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_6 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_5 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_4 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_3 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_2 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptn_1 = New NTSInformatica.NTSCheckBox
    Me.NtsTabPage7 = New NTSInformatica.NTSTabPage
    Me.pnPag7 = New NTSInformatica.NTSPanel
    Me.pnPag7Right = New NTSInformatica.NTSPanel
    Me.ckTb_modptnext_30 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_29 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_28 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_27 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_26 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_25 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_24 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_23 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_22 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_21 = New NTSInformatica.NTSCheckBox
    Me.pnPag7Center = New NTSInformatica.NTSPanel
    Me.ckTb_modptnext_20 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_19 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_18 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_17 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_16 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_15 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_14 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_13 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_12 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_11 = New NTSInformatica.NTSCheckBox
    Me.pnPag7Left = New NTSInformatica.NTSPanel
    Me.ckTb_modptnext_10 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_9 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_8 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_7 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_6 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_5 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_4 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_3 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_2 = New NTSInformatica.NTSCheckBox
    Me.ckTb_modptnext_1 = New NTSInformatica.NTSCheckBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsInsg, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsInsg.SuspendLayout()
    Me.NtsTabPage8.SuspendLayout()
    CType(Me.pnPag8, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag8.SuspendLayout()
    CType(Me.ckSconGenex.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconClCliDaList.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconSpecNoCum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbScPrior.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbSc6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbSc5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbSc4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbSc3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbSc2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbSc1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSc6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSc5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSc4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSc3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSc2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSc1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage3.SuspendLayout()
    CType(Me.pnPag3, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag3.SuspendLayout()
    CType(Me.edTb_maxlencodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_seriemaxlen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_annopaghe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_anno770.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_annodich.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_opz_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_destcorr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_destresan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_destsedel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_destdomf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckXx_codditcsc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnPag1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1.SuspendLayout()
    CType(Me.pnPag1Right, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1Right.SuspendLayout()
    CType(Me.ckTb_mod2_15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod2_14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod2_13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod2_12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod2_11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod2_10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod2_9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod2_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod2_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod2_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag1Center, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1Center.SuspendLayout()
    CType(Me.ckTb_mod2_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod2_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod2_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod2_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod2_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod_15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod_14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod_13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod_12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod_11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag1Left, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag1Left.SuspendLayout()
    CType(Me.ckTb_mod_10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod_9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnPag2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag2.SuspendLayout()
    CType(Me.pnPag2Right, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag2Right.SuspendLayout()
    CType(Me.ckTb_mod4_15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod4_14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod4_13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod4_12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod4_11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod4_10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod4_9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod4_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod4_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod4_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag2Center, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag2Center.SuspendLayout()
    CType(Me.ckTb_mod4_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod4_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod4_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod4_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod4_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod3_15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod3_14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod3_13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod3_12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod3_11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag2Left, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag2Left.SuspendLayout()
    CType(Me.ckTb_mod3_10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod3_9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod3_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod3_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod3_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod3_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod3_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod3_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod3_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_mod3_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage4.SuspendLayout()
    CType(Me.pnPag4, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag4.SuspendLayout()
    CType(Me.pnPag4Right, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag4Right.SuspendLayout()
    CType(Me.ckTb_modsup_30.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_29.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_28.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_27.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_26.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_25.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_24.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_23.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_22.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_21.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag4Center, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag4Center.SuspendLayout()
    CType(Me.ckTb_modsup_20.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_19.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_18.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_17.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_16.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag4Left, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag4Left.SuspendLayout()
    CType(Me.ckTb_modsup_10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsup_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage5.SuspendLayout()
    CType(Me.pnPag5, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag5.SuspendLayout()
    CType(Me.pnPag5Right, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag5Right.SuspendLayout()
    CType(Me.ckTb_modsupext_30.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_29.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_28.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_27.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_26.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_25.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_24.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_23.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_22.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_21.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag5Center, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag5Center.SuspendLayout()
    CType(Me.ckTb_modsupext_20.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_19.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_18.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_17.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_16.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag5Left, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag5Left.SuspendLayout()
    CType(Me.ckTb_modsupext_10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modsupext_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage6.SuspendLayout()
    CType(Me.pnPag6, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag6.SuspendLayout()
    CType(Me.pnPag6Right, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag6Right.SuspendLayout()
    CType(Me.ckTb_modptn_30.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_29.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_28.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_27.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_26.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_25.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_24.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_23.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_22.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_21.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag6Center, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag6Center.SuspendLayout()
    CType(Me.ckTb_modptn_20.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_19.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_18.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_17.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_16.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag6Left, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag6Left.SuspendLayout()
    CType(Me.ckTb_modptn_10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptn_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage7.SuspendLayout()
    CType(Me.pnPag7, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag7.SuspendLayout()
    CType(Me.pnPag7Right, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag7Right.SuspendLayout()
    CType(Me.ckTb_modptnext_30.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_29.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_28.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_27.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_26.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_25.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_24.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_23.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_22.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_21.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag7Center, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag7Center.SuspendLayout()
    CType(Me.ckTb_modptnext_20.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_19.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_18.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_17.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_16.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_15.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_14.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_13.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_12.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_11.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPag7Left, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPag7Left.SuspendLayout()
    CType(Me.ckTb_modptnext_10.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_9.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_8.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_7.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTb_modptnext_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 4
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 0
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 1
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 2
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 3
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tsInsg
    '
    Me.tsInsg.Location = New System.Drawing.Point(0, 29)
    Me.tsInsg.Name = "tsInsg"
    Me.tsInsg.SelectedTabPage = Me.NtsTabPage5
    Me.tsInsg.Size = New System.Drawing.Size(618, 297)
    Me.tsInsg.TabIndex = 4
    Me.tsInsg.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage8, Me.NtsTabPage3, Me.NtsTabPage1, Me.NtsTabPage2, Me.NtsTabPage4, Me.NtsTabPage5, Me.NtsTabPage6, Me.NtsTabPage7})
    Me.tsInsg.Text = "NtsTabControl1"
    '
    'NtsTabPage8
    '
    Me.NtsTabPage8.AllowDrop = True
    Me.NtsTabPage8.Controls.Add(Me.pnPag8)
    Me.NtsTabPage8.Enable = True
    Me.NtsTabPage8.Name = "NtsTabPage8"
    Me.NtsTabPage8.Size = New System.Drawing.Size(609, 267)
    Me.NtsTabPage8.Text = "Gestione sconti"
    '
    'pnPag8
    '
    Me.pnPag8.AllowDrop = True
    Me.pnPag8.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag8.Appearance.Options.UseBackColor = True
    Me.pnPag8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag8.Controls.Add(Me.NtsLabel1)
    Me.pnPag8.Controls.Add(Me.ckSconGenex)
    Me.pnPag8.Controls.Add(Me.lbSc6)
    Me.pnPag8.Controls.Add(Me.lbSc5)
    Me.pnPag8.Controls.Add(Me.lbSc4)
    Me.pnPag8.Controls.Add(Me.lbSc3)
    Me.pnPag8.Controls.Add(Me.lbSc2)
    Me.pnPag8.Controls.Add(Me.lbSc1)
    Me.pnPag8.Controls.Add(Me.ckSconClCliDaList)
    Me.pnPag8.Controls.Add(Me.ckSconSpecNoCum)
    Me.pnPag8.Controls.Add(Me.ckSconti)
    Me.pnPag8.Controls.Add(Me.lbPrior)
    Me.pnPag8.Controls.Add(Me.cbScPrior)
    Me.pnPag8.Controls.Add(Me.cbSc6)
    Me.pnPag8.Controls.Add(Me.cbSc5)
    Me.pnPag8.Controls.Add(Me.cbSc4)
    Me.pnPag8.Controls.Add(Me.cbSc3)
    Me.pnPag8.Controls.Add(Me.cbSc2)
    Me.pnPag8.Controls.Add(Me.cbSc1)
    Me.pnPag8.Controls.Add(Me.ckSc6)
    Me.pnPag8.Controls.Add(Me.ckSc5)
    Me.pnPag8.Controls.Add(Me.ckSc4)
    Me.pnPag8.Controls.Add(Me.ckSc3)
    Me.pnPag8.Controls.Add(Me.ckSc2)
    Me.pnPag8.Controls.Add(Me.ckSc1)
    Me.pnPag8.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag8.Location = New System.Drawing.Point(0, -3)
    Me.pnPag8.Name = "pnPag8"
    Me.pnPag8.NTSActiveTrasparency = True
    Me.pnPag8.Size = New System.Drawing.Size(609, 273)
    Me.pnPag8.TabIndex = 17
    Me.pnPag8.Text = "NtsPanel1"
    '
    'NtsLabel1
    '
    Me.NtsLabel1.AutoSize = True
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(409, 78)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(199, 104)
    Me.NtsLabel1.TabIndex = 48
    Me.NtsLabel1.Text = resources.GetString("NtsLabel1.Text")
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'ckSconGenex
    '
    Me.ckSconGenex.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconGenex.Location = New System.Drawing.Point(391, 76)
    Me.ckSconGenex.Name = "ckSconGenex"
    Me.ckSconGenex.NTSCheckValue = "S"
    Me.ckSconGenex.NTSUnCheckValue = " "
    Me.ckSconGenex.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconGenex.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconGenex.Properties.AutoHeight = False
    Me.ckSconGenex.Properties.Caption = " "
    Me.ckSconGenex.Size = New System.Drawing.Size(31, 19)
    Me.ckSconGenex.TabIndex = 47
    '
    'lbSc6
    '
    Me.lbSc6.AutoSize = True
    Me.lbSc6.BackColor = System.Drawing.Color.Transparent
    Me.lbSc6.Location = New System.Drawing.Point(510, 231)
    Me.lbSc6.Name = "lbSc6"
    Me.lbSc6.NTSDbField = ""
    Me.lbSc6.Size = New System.Drawing.Size(49, 13)
    Me.lbSc6.TabIndex = 46
    Me.lbSc6.Text = "Sconto 6"
    Me.lbSc6.Tooltip = ""
    Me.lbSc6.UseMnemonic = False
    '
    'lbSc5
    '
    Me.lbSc5.AutoSize = True
    Me.lbSc5.BackColor = System.Drawing.Color.Transparent
    Me.lbSc5.Location = New System.Drawing.Point(412, 231)
    Me.lbSc5.Name = "lbSc5"
    Me.lbSc5.NTSDbField = ""
    Me.lbSc5.Size = New System.Drawing.Size(49, 13)
    Me.lbSc5.TabIndex = 45
    Me.lbSc5.Text = "Sconto 5"
    Me.lbSc5.Tooltip = ""
    Me.lbSc5.UseMnemonic = False
    '
    'lbSc4
    '
    Me.lbSc4.AutoSize = True
    Me.lbSc4.BackColor = System.Drawing.Color.Transparent
    Me.lbSc4.Location = New System.Drawing.Point(315, 231)
    Me.lbSc4.Name = "lbSc4"
    Me.lbSc4.NTSDbField = ""
    Me.lbSc4.Size = New System.Drawing.Size(49, 13)
    Me.lbSc4.TabIndex = 44
    Me.lbSc4.Text = "Sconto 4"
    Me.lbSc4.Tooltip = ""
    Me.lbSc4.UseMnemonic = False
    '
    'lbSc3
    '
    Me.lbSc3.AutoSize = True
    Me.lbSc3.BackColor = System.Drawing.Color.Transparent
    Me.lbSc3.Location = New System.Drawing.Point(220, 231)
    Me.lbSc3.Name = "lbSc3"
    Me.lbSc3.NTSDbField = ""
    Me.lbSc3.Size = New System.Drawing.Size(49, 13)
    Me.lbSc3.TabIndex = 43
    Me.lbSc3.Text = "Sconto 3"
    Me.lbSc3.Tooltip = ""
    Me.lbSc3.UseMnemonic = False
    '
    'lbSc2
    '
    Me.lbSc2.AutoSize = True
    Me.lbSc2.BackColor = System.Drawing.Color.Transparent
    Me.lbSc2.Location = New System.Drawing.Point(125, 231)
    Me.lbSc2.Name = "lbSc2"
    Me.lbSc2.NTSDbField = ""
    Me.lbSc2.Size = New System.Drawing.Size(49, 13)
    Me.lbSc2.TabIndex = 42
    Me.lbSc2.Text = "Sconto 2"
    Me.lbSc2.Tooltip = ""
    Me.lbSc2.UseMnemonic = False
    '
    'lbSc1
    '
    Me.lbSc1.AutoSize = True
    Me.lbSc1.BackColor = System.Drawing.Color.Transparent
    Me.lbSc1.Location = New System.Drawing.Point(26, 231)
    Me.lbSc1.Name = "lbSc1"
    Me.lbSc1.NTSDbField = ""
    Me.lbSc1.Size = New System.Drawing.Size(49, 13)
    Me.lbSc1.TabIndex = 41
    Me.lbSc1.Text = "Sconto 1"
    Me.lbSc1.Tooltip = ""
    Me.lbSc1.UseMnemonic = False
    '
    'ckSconClCliDaList
    '
    Me.ckSconClCliDaList.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconClCliDaList.Location = New System.Drawing.Point(9, 184)
    Me.ckSconClCliDaList.Name = "ckSconClCliDaList"
    Me.ckSconClCliDaList.NTSCheckValue = "S"
    Me.ckSconClCliDaList.NTSUnCheckValue = " "
    Me.ckSconClCliDaList.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconClCliDaList.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconClCliDaList.Properties.AutoHeight = False
    Me.ckSconClCliDaList.Properties.Caption = "Classe sconto cliente presa da numero listino"
    Me.ckSconClCliDaList.Size = New System.Drawing.Size(249, 19)
    Me.ckSconClCliDaList.TabIndex = 40
    '
    'ckSconSpecNoCum
    '
    Me.ckSconSpecNoCum.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconSpecNoCum.Location = New System.Drawing.Point(9, 204)
    Me.ckSconSpecNoCum.Name = "ckSconSpecNoCum"
    Me.ckSconSpecNoCum.NTSCheckValue = "S"
    Me.ckSconSpecNoCum.NTSUnCheckValue = " "
    Me.ckSconSpecNoCum.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconSpecNoCum.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconSpecNoCum.Properties.AutoHeight = False
    Me.ckSconSpecNoCum.Properties.Caption = "Ignora altri sconti se trovato sconto specifico cliente / articolo"
    Me.ckSconSpecNoCum.Size = New System.Drawing.Size(329, 19)
    Me.ckSconSpecNoCum.TabIndex = 39
    '
    'ckSconti
    '
    Me.ckSconti.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconti.Location = New System.Drawing.Point(9, 13)
    Me.ckSconti.Name = "ckSconti"
    Me.ckSconti.NTSCheckValue = "S"
    Me.ckSconti.NTSUnCheckValue = " "
    Me.ckSconti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconti.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconti.Properties.AutoHeight = False
    Me.ckSconti.Properties.Caption = "Gestisci gli sconti"
    Me.ckSconti.Size = New System.Drawing.Size(119, 19)
    Me.ckSconti.TabIndex = 38
    '
    'lbPrior
    '
    Me.lbPrior.AutoSize = True
    Me.lbPrior.BackColor = System.Drawing.Color.Transparent
    Me.lbPrior.Location = New System.Drawing.Point(388, 15)
    Me.lbPrior.Name = "lbPrior"
    Me.lbPrior.NTSDbField = ""
    Me.lbPrior.Size = New System.Drawing.Size(41, 13)
    Me.lbPrior.TabIndex = 37
    Me.lbPrior.Text = "Priorità"
    Me.lbPrior.Tooltip = ""
    Me.lbPrior.UseMnemonic = False
    '
    'cbScPrior
    '
    Me.cbScPrior.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbScPrior.DataSource = Nothing
    Me.cbScPrior.DisplayMember = ""
    Me.cbScPrior.Location = New System.Drawing.Point(472, 12)
    Me.cbScPrior.Name = "cbScPrior"
    Me.cbScPrior.NTSDbField = ""
    Me.cbScPrior.Properties.AutoHeight = False
    Me.cbScPrior.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbScPrior.Properties.DropDownRows = 30
    Me.cbScPrior.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbScPrior.SelectedValue = ""
    Me.cbScPrior.Size = New System.Drawing.Size(113, 20)
    Me.cbScPrior.TabIndex = 36
    Me.cbScPrior.ValueMember = ""
    '
    'cbSc6
    '
    Me.cbSc6.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbSc6.DataSource = Nothing
    Me.cbSc6.DisplayMember = ""
    Me.cbSc6.Location = New System.Drawing.Point(494, 247)
    Me.cbSc6.Name = "cbSc6"
    Me.cbSc6.NTSDbField = ""
    Me.cbSc6.Properties.AutoHeight = False
    Me.cbSc6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSc6.Properties.DropDownRows = 30
    Me.cbSc6.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSc6.SelectedValue = ""
    Me.cbSc6.Size = New System.Drawing.Size(91, 20)
    Me.cbSc6.TabIndex = 35
    Me.cbSc6.ValueMember = ""
    '
    'cbSc5
    '
    Me.cbSc5.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbSc5.DataSource = Nothing
    Me.cbSc5.DisplayMember = ""
    Me.cbSc5.Location = New System.Drawing.Point(397, 247)
    Me.cbSc5.Name = "cbSc5"
    Me.cbSc5.NTSDbField = ""
    Me.cbSc5.Properties.AutoHeight = False
    Me.cbSc5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSc5.Properties.DropDownRows = 30
    Me.cbSc5.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSc5.SelectedValue = ""
    Me.cbSc5.Size = New System.Drawing.Size(91, 20)
    Me.cbSc5.TabIndex = 34
    Me.cbSc5.ValueMember = ""
    '
    'cbSc4
    '
    Me.cbSc4.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbSc4.DataSource = Nothing
    Me.cbSc4.DisplayMember = ""
    Me.cbSc4.Location = New System.Drawing.Point(300, 247)
    Me.cbSc4.Name = "cbSc4"
    Me.cbSc4.NTSDbField = ""
    Me.cbSc4.Properties.AutoHeight = False
    Me.cbSc4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSc4.Properties.DropDownRows = 30
    Me.cbSc4.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSc4.SelectedValue = ""
    Me.cbSc4.Size = New System.Drawing.Size(91, 20)
    Me.cbSc4.TabIndex = 33
    Me.cbSc4.ValueMember = ""
    '
    'cbSc3
    '
    Me.cbSc3.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbSc3.DataSource = Nothing
    Me.cbSc3.DisplayMember = ""
    Me.cbSc3.Location = New System.Drawing.Point(203, 247)
    Me.cbSc3.Name = "cbSc3"
    Me.cbSc3.NTSDbField = ""
    Me.cbSc3.Properties.AutoHeight = False
    Me.cbSc3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSc3.Properties.DropDownRows = 30
    Me.cbSc3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSc3.SelectedValue = ""
    Me.cbSc3.Size = New System.Drawing.Size(91, 20)
    Me.cbSc3.TabIndex = 32
    Me.cbSc3.ValueMember = ""
    '
    'cbSc2
    '
    Me.cbSc2.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbSc2.DataSource = Nothing
    Me.cbSc2.DisplayMember = ""
    Me.cbSc2.Location = New System.Drawing.Point(106, 247)
    Me.cbSc2.Name = "cbSc2"
    Me.cbSc2.NTSDbField = ""
    Me.cbSc2.Properties.AutoHeight = False
    Me.cbSc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSc2.Properties.DropDownRows = 30
    Me.cbSc2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSc2.SelectedValue = ""
    Me.cbSc2.Size = New System.Drawing.Size(91, 20)
    Me.cbSc2.TabIndex = 31
    Me.cbSc2.ValueMember = ""
    '
    'cbSc1
    '
    Me.cbSc1.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbSc1.DataSource = Nothing
    Me.cbSc1.DisplayMember = ""
    Me.cbSc1.Location = New System.Drawing.Point(9, 247)
    Me.cbSc1.Name = "cbSc1"
    Me.cbSc1.NTSDbField = ""
    Me.cbSc1.Properties.AutoHeight = False
    Me.cbSc1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSc1.Properties.DropDownRows = 30
    Me.cbSc1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSc1.SelectedValue = ""
    Me.cbSc1.Size = New System.Drawing.Size(91, 20)
    Me.cbSc1.TabIndex = 30
    Me.cbSc1.ValueMember = ""
    '
    'ckSc6
    '
    Me.ckSc6.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSc6.Location = New System.Drawing.Point(9, 132)
    Me.ckSc6.Name = "ckSc6"
    Me.ckSc6.NTSCheckValue = "S"
    Me.ckSc6.NTSUnCheckValue = " "
    Me.ckSc6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSc6.Properties.Appearance.Options.UseBackColor = True
    Me.ckSc6.Properties.AutoHeight = False
    Me.ckSc6.Properties.Caption = "Gestisci sconto F - generico cliente"
    Me.ckSc6.Size = New System.Drawing.Size(203, 19)
    Me.ckSc6.TabIndex = 29
    '
    'ckSc5
    '
    Me.ckSc5.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSc5.Location = New System.Drawing.Point(9, 114)
    Me.ckSc5.Name = "ckSc5"
    Me.ckSc5.NTSCheckValue = "S"
    Me.ckSc5.NTSUnCheckValue = " "
    Me.ckSc5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSc5.Properties.Appearance.Options.UseBackColor = True
    Me.ckSc5.Properties.AutoHeight = False
    Me.ckSc5.Properties.Caption = "Gestisci sconto E - generico articolo"
    Me.ckSc5.Size = New System.Drawing.Size(203, 19)
    Me.ckSc5.TabIndex = 28
    '
    'ckSc4
    '
    Me.ckSc4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSc4.Location = New System.Drawing.Point(9, 96)
    Me.ckSc4.Name = "ckSc4"
    Me.ckSc4.NTSCheckValue = "S"
    Me.ckSc4.NTSUnCheckValue = " "
    Me.ckSc4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSc4.Properties.Appearance.Options.UseBackColor = True
    Me.ckSc4.Properties.AutoHeight = False
    Me.ckSc4.Properties.Caption = "Gestisci sconto D - classe articolo / classe cliente"
    Me.ckSc4.Size = New System.Drawing.Size(265, 19)
    Me.ckSc4.TabIndex = 27
    '
    'ckSc3
    '
    Me.ckSc3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSc3.Location = New System.Drawing.Point(9, 78)
    Me.ckSc3.Name = "ckSc3"
    Me.ckSc3.NTSCheckValue = "S"
    Me.ckSc3.NTSUnCheckValue = " "
    Me.ckSc3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSc3.Properties.Appearance.Options.UseBackColor = True
    Me.ckSc3.Properties.AutoHeight = False
    Me.ckSc3.Properties.Caption = "Gestisci sconto C - cliente / classe articolo"
    Me.ckSc3.Size = New System.Drawing.Size(242, 19)
    Me.ckSc3.TabIndex = 26
    '
    'ckSc2
    '
    Me.ckSc2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSc2.Location = New System.Drawing.Point(9, 60)
    Me.ckSc2.Name = "ckSc2"
    Me.ckSc2.NTSCheckValue = "S"
    Me.ckSc2.NTSUnCheckValue = " "
    Me.ckSc2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSc2.Properties.Appearance.Options.UseBackColor = True
    Me.ckSc2.Properties.AutoHeight = False
    Me.ckSc2.Properties.Caption = "Gestisci sconto B - articolo / classe cliente"
    Me.ckSc2.Size = New System.Drawing.Size(242, 19)
    Me.ckSc2.TabIndex = 25
    '
    'ckSc1
    '
    Me.ckSc1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSc1.Location = New System.Drawing.Point(9, 42)
    Me.ckSc1.Name = "ckSc1"
    Me.ckSc1.NTSCheckValue = "S"
    Me.ckSc1.NTSUnCheckValue = " "
    Me.ckSc1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSc1.Properties.Appearance.Options.UseBackColor = True
    Me.ckSc1.Properties.AutoHeight = False
    Me.ckSc1.Properties.Caption = "Gestisci sconto A - specifico cliente / articolo"
    Me.ckSc1.Size = New System.Drawing.Size(242, 19)
    Me.ckSc1.TabIndex = 24
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Controls.Add(Me.pnPag3)
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(609, 267)
    Me.NtsTabPage3.Text = "&Altro"
    Me.NtsTabPage3.Tooltip = "Altri Files di Personalizzazione"
    '
    'pnPag3
    '
    Me.pnPag3.AllowDrop = True
    Me.pnPag3.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag3.Appearance.Options.UseBackColor = True
    Me.pnPag3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag3.Controls.Add(Me.edTb_maxlencodart)
    Me.pnPag3.Controls.Add(Me.lbTb_maxlencodart)
    Me.pnPag3.Controls.Add(Me.edTb_seriemaxlen)
    Me.pnPag3.Controls.Add(Me.edTb_annopaghe)
    Me.pnPag3.Controls.Add(Me.edTb_anno770)
    Me.pnPag3.Controls.Add(Me.edTb_annodich)
    Me.pnPag3.Controls.Add(Me.cbTb_opz_1)
    Me.pnPag3.Controls.Add(Me.edTb_destcorr)
    Me.pnPag3.Controls.Add(Me.edTb_destresan)
    Me.pnPag3.Controls.Add(Me.edTb_destsedel)
    Me.pnPag3.Controls.Add(Me.edTb_destdomf)
    Me.pnPag3.Controls.Add(Me.ckXx_codditcsc)
    Me.pnPag3.Controls.Add(Me.lbTb_anno770)
    Me.pnPag3.Controls.Add(Me.lbTb_annodich)
    Me.pnPag3.Controls.Add(Me.lbTb_opz_1)
    Me.pnPag3.Controls.Add(Me.lbTb_destcorr)
    Me.pnPag3.Controls.Add(Me.lbTb_destresan)
    Me.pnPag3.Controls.Add(Me.lbTb_destsedel)
    Me.pnPag3.Controls.Add(Me.lbTb_seriemaxlen)
    Me.pnPag3.Controls.Add(Me.lbTb_destdomf)
    Me.pnPag3.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag3.Location = New System.Drawing.Point(0, 0)
    Me.pnPag3.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag3.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag3.Name = "pnPag3"
    Me.pnPag3.NTSActiveTrasparency = True
    Me.pnPag3.Size = New System.Drawing.Size(609, 273)
    Me.pnPag3.TabIndex = 16
    Me.pnPag3.Text = "NtsPanel1"
    '
    'edTb_maxlencodart
    '
    Me.edTb_maxlencodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_maxlencodart.EditValue = "18"
    Me.edTb_maxlencodart.Location = New System.Drawing.Point(276, 188)
    Me.edTb_maxlencodart.Name = "edTb_maxlencodart"
    Me.edTb_maxlencodart.NTSDbField = ""
    Me.edTb_maxlencodart.NTSFormat = "0"
    Me.edTb_maxlencodart.NTSForzaVisZoom = False
    Me.edTb_maxlencodart.NTSOldValue = "18"
    Me.edTb_maxlencodart.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_maxlencodart.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_maxlencodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_maxlencodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_maxlencodart.Properties.AutoHeight = False
    Me.edTb_maxlencodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_maxlencodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_maxlencodart.Size = New System.Drawing.Size(25, 20)
    Me.edTb_maxlencodart.TabIndex = 34
    '
    'lbTb_maxlencodart
    '
    Me.lbTb_maxlencodart.AutoSize = True
    Me.lbTb_maxlencodart.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_maxlencodart.Location = New System.Drawing.Point(17, 191)
    Me.lbTb_maxlencodart.Name = "lbTb_maxlencodart"
    Me.lbTb_maxlencodart.NTSDbField = ""
    Me.lbTb_maxlencodart.Size = New System.Drawing.Size(129, 13)
    Me.lbTb_maxlencodart.TabIndex = 33
    Me.lbTb_maxlencodart.Text = "Lunghezza codice articolo"
    Me.lbTb_maxlencodart.Tooltip = ""
    Me.lbTb_maxlencodart.UseMnemonic = False
    '
    'edTb_seriemaxlen
    '
    Me.edTb_seriemaxlen.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_seriemaxlen.EditValue = "1"
    Me.edTb_seriemaxlen.Location = New System.Drawing.Point(276, 166)
    Me.edTb_seriemaxlen.Name = "edTb_seriemaxlen"
    Me.edTb_seriemaxlen.NTSDbField = ""
    Me.edTb_seriemaxlen.NTSFormat = "0"
    Me.edTb_seriemaxlen.NTSForzaVisZoom = False
    Me.edTb_seriemaxlen.NTSOldValue = "1"
    Me.edTb_seriemaxlen.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_seriemaxlen.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_seriemaxlen.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_seriemaxlen.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_seriemaxlen.Properties.AutoHeight = False
    Me.edTb_seriemaxlen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_seriemaxlen.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_seriemaxlen.Size = New System.Drawing.Size(25, 20)
    Me.edTb_seriemaxlen.TabIndex = 32
    '
    'edTb_annopaghe
    '
    Me.edTb_annopaghe.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_annopaghe.EditValue = "0"
    Me.edTb_annopaghe.Location = New System.Drawing.Point(363, 144)
    Me.edTb_annopaghe.Name = "edTb_annopaghe"
    Me.edTb_annopaghe.NTSDbField = ""
    Me.edTb_annopaghe.NTSFormat = "0"
    Me.edTb_annopaghe.NTSForzaVisZoom = False
    Me.edTb_annopaghe.NTSOldValue = ""
    Me.edTb_annopaghe.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_annopaghe.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_annopaghe.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_annopaghe.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_annopaghe.Properties.AutoHeight = False
    Me.edTb_annopaghe.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_annopaghe.Properties.MaxLength = 65536
    Me.edTb_annopaghe.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_annopaghe.Size = New System.Drawing.Size(57, 20)
    Me.edTb_annopaghe.TabIndex = 31
    '
    'edTb_anno770
    '
    Me.edTb_anno770.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_anno770.EditValue = "0"
    Me.edTb_anno770.Location = New System.Drawing.Point(276, 144)
    Me.edTb_anno770.Name = "edTb_anno770"
    Me.edTb_anno770.NTSDbField = ""
    Me.edTb_anno770.NTSFormat = "0"
    Me.edTb_anno770.NTSForzaVisZoom = False
    Me.edTb_anno770.NTSOldValue = ""
    Me.edTb_anno770.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_anno770.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_anno770.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_anno770.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_anno770.Properties.AutoHeight = False
    Me.edTb_anno770.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_anno770.Properties.MaxLength = 65536
    Me.edTb_anno770.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_anno770.Size = New System.Drawing.Size(81, 20)
    Me.edTb_anno770.TabIndex = 30
    '
    'edTb_annodich
    '
    Me.edTb_annodich.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edTb_annodich.EditValue = "0"
    Me.edTb_annodich.Location = New System.Drawing.Point(276, 122)
    Me.edTb_annodich.Name = "edTb_annodich"
    Me.edTb_annodich.NTSDbField = ""
    Me.edTb_annodich.NTSFormat = "0"
    Me.edTb_annodich.NTSForzaVisZoom = False
    Me.edTb_annodich.NTSOldValue = ""
    Me.edTb_annodich.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_annodich.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_annodich.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_annodich.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_annodich.Properties.AutoHeight = False
    Me.edTb_annodich.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_annodich.Properties.MaxLength = 65536
    Me.edTb_annodich.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_annodich.Size = New System.Drawing.Size(81, 20)
    Me.edTb_annodich.TabIndex = 29
    '
    'cbTb_opz_1
    '
    Me.cbTb_opz_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTb_opz_1.DataSource = Nothing
    Me.cbTb_opz_1.DisplayMember = ""
    Me.cbTb_opz_1.Location = New System.Drawing.Point(276, 100)
    Me.cbTb_opz_1.Name = "cbTb_opz_1"
    Me.cbTb_opz_1.NTSDbField = ""
    Me.cbTb_opz_1.Properties.AutoHeight = False
    Me.cbTb_opz_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_opz_1.Properties.DropDownRows = 30
    Me.cbTb_opz_1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_opz_1.SelectedValue = ""
    Me.cbTb_opz_1.Size = New System.Drawing.Size(186, 20)
    Me.cbTb_opz_1.TabIndex = 28
    Me.cbTb_opz_1.ValueMember = ""
    '
    'edTb_destcorr
    '
    Me.edTb_destcorr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_destcorr.EditValue = "0"
    Me.edTb_destcorr.Location = New System.Drawing.Point(276, 78)
    Me.edTb_destcorr.Name = "edTb_destcorr"
    Me.edTb_destcorr.NTSDbField = ""
    Me.edTb_destcorr.NTSFormat = "0"
    Me.edTb_destcorr.NTSForzaVisZoom = False
    Me.edTb_destcorr.NTSOldValue = ""
    Me.edTb_destcorr.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_destcorr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_destcorr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_destcorr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_destcorr.Properties.AutoHeight = False
    Me.edTb_destcorr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_destcorr.Properties.MaxLength = 65536
    Me.edTb_destcorr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_destcorr.Size = New System.Drawing.Size(102, 20)
    Me.edTb_destcorr.TabIndex = 27
    '
    'edTb_destresan
    '
    Me.edTb_destresan.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_destresan.EditValue = "0"
    Me.edTb_destresan.Location = New System.Drawing.Point(276, 56)
    Me.edTb_destresan.Name = "edTb_destresan"
    Me.edTb_destresan.NTSDbField = ""
    Me.edTb_destresan.NTSFormat = "0"
    Me.edTb_destresan.NTSForzaVisZoom = False
    Me.edTb_destresan.NTSOldValue = ""
    Me.edTb_destresan.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_destresan.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_destresan.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_destresan.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_destresan.Properties.AutoHeight = False
    Me.edTb_destresan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_destresan.Properties.MaxLength = 65536
    Me.edTb_destresan.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_destresan.Size = New System.Drawing.Size(102, 20)
    Me.edTb_destresan.TabIndex = 26
    '
    'edTb_destsedel
    '
    Me.edTb_destsedel.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_destsedel.EditValue = "0"
    Me.edTb_destsedel.Location = New System.Drawing.Point(276, 34)
    Me.edTb_destsedel.Name = "edTb_destsedel"
    Me.edTb_destsedel.NTSDbField = ""
    Me.edTb_destsedel.NTSFormat = "0"
    Me.edTb_destsedel.NTSForzaVisZoom = False
    Me.edTb_destsedel.NTSOldValue = ""
    Me.edTb_destsedel.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_destsedel.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_destsedel.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_destsedel.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_destsedel.Properties.AutoHeight = False
    Me.edTb_destsedel.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_destsedel.Properties.MaxLength = 65536
    Me.edTb_destsedel.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_destsedel.Size = New System.Drawing.Size(103, 20)
    Me.edTb_destsedel.TabIndex = 25
    '
    'edTb_destdomf
    '
    Me.edTb_destdomf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_destdomf.EditValue = "0"
    Me.edTb_destdomf.Location = New System.Drawing.Point(276, 12)
    Me.edTb_destdomf.Name = "edTb_destdomf"
    Me.edTb_destdomf.NTSDbField = ""
    Me.edTb_destdomf.NTSFormat = "0"
    Me.edTb_destdomf.NTSForzaVisZoom = False
    Me.edTb_destdomf.NTSOldValue = ""
    Me.edTb_destdomf.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_destdomf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_destdomf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_destdomf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_destdomf.Properties.AutoHeight = False
    Me.edTb_destdomf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_destdomf.Properties.MaxLength = 65536
    Me.edTb_destdomf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_destdomf.Size = New System.Drawing.Size(103, 20)
    Me.edTb_destdomf.TabIndex = 24
    '
    'ckXx_codditcsc
    '
    Me.ckXx_codditcsc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckXx_codditcsc.Location = New System.Drawing.Point(20, 218)
    Me.ckXx_codditcsc.Name = "ckXx_codditcsc"
    Me.ckXx_codditcsc.NTSCheckValue = "S"
    Me.ckXx_codditcsc.NTSUnCheckValue = " "
    Me.ckXx_codditcsc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckXx_codditcsc.Properties.Appearance.Options.UseBackColor = True
    Me.ckXx_codditcsc.Properties.AutoHeight = False
    Me.ckXx_codditcsc.Properties.Caption = "Crea automaticamente contropartite generiche"
    Me.ckXx_codditcsc.Size = New System.Drawing.Size(255, 19)
    Me.ckXx_codditcsc.TabIndex = 23
    Me.ckXx_codditcsc.Visible = False
    '
    'lbTb_anno770
    '
    Me.lbTb_anno770.AutoSize = True
    Me.lbTb_anno770.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_anno770.Location = New System.Drawing.Point(17, 147)
    Me.lbTb_anno770.Name = "lbTb_anno770"
    Me.lbTb_anno770.NTSDbField = ""
    Me.lbTb_anno770.Size = New System.Drawing.Size(226, 13)
    Me.lbTb_anno770.TabIndex = 22
    Me.lbTb_anno770.Text = "Anno corrente dichiarazioni sostituti d'imposta"
    Me.lbTb_anno770.Tooltip = ""
    Me.lbTb_anno770.UseMnemonic = False
    '
    'lbTb_annodich
    '
    Me.lbTb_annodich.AutoSize = True
    Me.lbTb_annodich.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_annodich.Location = New System.Drawing.Point(17, 125)
    Me.lbTb_annodich.Name = "lbTb_annodich"
    Me.lbTb_annodich.NTSDbField = ""
    Me.lbTb_annodich.Size = New System.Drawing.Size(170, 13)
    Me.lbTb_annodich.TabIndex = 21
    Me.lbTb_annodich.Text = "Anno corrente dichiarazioni redditi"
    Me.lbTb_annodich.Tooltip = ""
    Me.lbTb_annodich.UseMnemonic = False
    '
    'lbTb_opz_1
    '
    Me.lbTb_opz_1.AutoSize = True
    Me.lbTb_opz_1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_opz_1.Location = New System.Drawing.Point(17, 103)
    Me.lbTb_opz_1.Name = "lbTb_opz_1"
    Me.lbTb_opz_1.NTSDbField = ""
    Me.lbTb_opz_1.Size = New System.Drawing.Size(78, 13)
    Me.lbTb_opz_1.TabIndex = 20
    Me.lbTb_opz_1.Text = "Valuta Azienda"
    Me.lbTb_opz_1.Tooltip = ""
    Me.lbTb_opz_1.UseMnemonic = False
    '
    'lbTb_destcorr
    '
    Me.lbTb_destcorr.AutoSize = True
    Me.lbTb_destcorr.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_destcorr.Location = New System.Drawing.Point(17, 81)
    Me.lbTb_destcorr.Name = "lbTb_destcorr"
    Me.lbTb_destcorr.NTSDbField = ""
    Me.lbTb_destcorr.Size = New System.Drawing.Size(163, 13)
    Me.lbTb_destcorr.TabIndex = 19
    Me.lbTb_destcorr.Text = "Luogo esercizio attività all'estero"
    Me.lbTb_destcorr.Tooltip = ""
    Me.lbTb_destcorr.UseMnemonic = False
    '
    'lbTb_destresan
    '
    Me.lbTb_destresan.AutoSize = True
    Me.lbTb_destresan.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_destresan.Location = New System.Drawing.Point(17, 59)
    Me.lbTb_destresan.Name = "lbTb_destresan"
    Me.lbTb_destresan.NTSDbField = ""
    Me.lbTb_destresan.Size = New System.Drawing.Size(155, 13)
    Me.lbTb_destresan.TabIndex = 18
    Me.lbTb_destresan.Text = "Residenza / Sede legale estera"
    Me.lbTb_destresan.Tooltip = ""
    Me.lbTb_destresan.UseMnemonic = False
    '
    'lbTb_destsedel
    '
    Me.lbTb_destsedel.AutoSize = True
    Me.lbTb_destsedel.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_destsedel.Location = New System.Drawing.Point(17, 37)
    Me.lbTb_destsedel.Name = "lbTb_destsedel"
    Me.lbTb_destsedel.NTSDbField = ""
    Me.lbTb_destsedel.Size = New System.Drawing.Size(208, 13)
    Me.lbTb_destsedel.TabIndex = 17
    Me.lbTb_destsedel.Text = "Residenza / Domicilio / Sede legale italiana"
    Me.lbTb_destsedel.Tooltip = ""
    Me.lbTb_destsedel.UseMnemonic = False
    '
    'lbTb_seriemaxlen
    '
    Me.lbTb_seriemaxlen.AutoSize = True
    Me.lbTb_seriemaxlen.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_seriemaxlen.Location = New System.Drawing.Point(17, 169)
    Me.lbTb_seriemaxlen.Name = "lbTb_seriemaxlen"
    Me.lbTb_seriemaxlen.NTSDbField = ""
    Me.lbTb_seriemaxlen.Size = New System.Drawing.Size(247, 13)
    Me.lbTb_seriemaxlen.TabIndex = 16
    Me.lbTb_seriemaxlen.Text = "Lunghezza serie documenti\ordini\scadenze\ecc..."
    Me.lbTb_seriemaxlen.Tooltip = ""
    Me.lbTb_seriemaxlen.UseMnemonic = False
    '
    'lbTb_destdomf
    '
    Me.lbTb_destdomf.AutoSize = True
    Me.lbTb_destdomf.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_destdomf.Location = New System.Drawing.Point(17, 15)
    Me.lbTb_destdomf.Name = "lbTb_destdomf"
    Me.lbTb_destdomf.NTSDbField = ""
    Me.lbTb_destdomf.Size = New System.Drawing.Size(82, 13)
    Me.lbTb_destdomf.TabIndex = 16
    Me.lbTb_destdomf.Text = "Domicilio Fiscale"
    Me.lbTb_destdomf.Tooltip = ""
    Me.lbTb_destdomf.UseMnemonic = False
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnPag1)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(609, 267)
    Me.NtsTabPage1.Text = "Moduli &1"
    Me.NtsTabPage1.Tooltip = "Moduli"
    '
    'pnPag1
    '
    Me.pnPag1.AllowDrop = True
    Me.pnPag1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1.Appearance.Options.UseBackColor = True
    Me.pnPag1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1.Controls.Add(Me.pnPag1Right)
    Me.pnPag1.Controls.Add(Me.pnPag1Center)
    Me.pnPag1.Controls.Add(Me.pnPag1Left)
    Me.pnPag1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1.Location = New System.Drawing.Point(0, 0)
    Me.pnPag1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag1.Name = "pnPag1"
    Me.pnPag1.NTSActiveTrasparency = True
    Me.pnPag1.Size = New System.Drawing.Size(609, 273)
    Me.pnPag1.TabIndex = 0
    Me.pnPag1.Text = "NtsPanel1"
    '
    'pnPag1Right
    '
    Me.pnPag1Right.AllowDrop = True
    Me.pnPag1Right.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1Right.Appearance.Options.UseBackColor = True
    Me.pnPag1Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1Right.Controls.Add(Me.ckTb_mod2_15)
    Me.pnPag1Right.Controls.Add(Me.ckTb_mod2_14)
    Me.pnPag1Right.Controls.Add(Me.ckTb_mod2_13)
    Me.pnPag1Right.Controls.Add(Me.ckTb_mod2_12)
    Me.pnPag1Right.Controls.Add(Me.ckTb_mod2_11)
    Me.pnPag1Right.Controls.Add(Me.ckTb_mod2_10)
    Me.pnPag1Right.Controls.Add(Me.ckTb_mod2_9)
    Me.pnPag1Right.Controls.Add(Me.ckTb_mod2_8)
    Me.pnPag1Right.Controls.Add(Me.ckTb_mod2_7)
    Me.pnPag1Right.Controls.Add(Me.ckTb_mod2_6)
    Me.pnPag1Right.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1Right.Location = New System.Drawing.Point(409, 3)
    Me.pnPag1Right.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag1Right.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag1Right.Name = "pnPag1Right"
    Me.pnPag1Right.NTSActiveTrasparency = True
    Me.pnPag1Right.Size = New System.Drawing.Size(197, 264)
    Me.pnPag1Right.TabIndex = 23
    Me.pnPag1Right.Text = "NtsPanel1"
    '
    'ckTb_mod2_15
    '
    Me.ckTb_mod2_15.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_15.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_mod2_15.Name = "ckTb_mod2_15"
    Me.ckTb_mod2_15.NTSCheckValue = "S"
    Me.ckTb_mod2_15.NTSUnCheckValue = "N"
    Me.ckTb_mod2_15.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_15.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_15.Properties.AutoHeight = False
    Me.ckTb_mod2_15.Properties.Caption = "(a disposizione - 2)"
    Me.ckTb_mod2_15.Size = New System.Drawing.Size(123, 19)
    Me.ckTb_mod2_15.TabIndex = 30
    '
    'ckTb_mod2_14
    '
    Me.ckTb_mod2_14.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_14.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_mod2_14.Name = "ckTb_mod2_14"
    Me.ckTb_mod2_14.NTSCheckValue = "S"
    Me.ckTb_mod2_14.NTSUnCheckValue = "N"
    Me.ckTb_mod2_14.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_14.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_14.Properties.AutoHeight = False
    Me.ckTb_mod2_14.Properties.Caption = "(a disposizione - 1)"
    Me.ckTb_mod2_14.Size = New System.Drawing.Size(123, 19)
    Me.ckTb_mod2_14.TabIndex = 29
    '
    'ckTb_mod2_13
    '
    Me.ckTb_mod2_13.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_13.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_mod2_13.Name = "ckTb_mod2_13"
    Me.ckTb_mod2_13.NTSCheckValue = "S"
    Me.ckTb_mod2_13.NTSUnCheckValue = "N"
    Me.ckTb_mod2_13.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_13.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_13.Properties.AutoHeight = False
    Me.ckTb_mod2_13.Properties.Caption = "Sistema Qualità"
    Me.ckTb_mod2_13.Size = New System.Drawing.Size(104, 19)
    Me.ckTb_mod2_13.TabIndex = 28
    '
    'ckTb_mod2_12
    '
    Me.ckTb_mod2_12.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_12.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_mod2_12.Name = "ckTb_mod2_12"
    Me.ckTb_mod2_12.NTSCheckValue = "S"
    Me.ckTb_mod2_12.NTSUnCheckValue = "N"
    Me.ckTb_mod2_12.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_12.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_12.Properties.AutoHeight = False
    Me.ckTb_mod2_12.Properties.Caption = "Configuratore di prodotto"
    Me.ckTb_mod2_12.Size = New System.Drawing.Size(153, 19)
    Me.ckTb_mod2_12.TabIndex = 27
    '
    'ckTb_mod2_11
    '
    Me.ckTb_mod2_11.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_11.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_mod2_11.Name = "ckTb_mod2_11"
    Me.ckTb_mod2_11.NTSCheckValue = "S"
    Me.ckTb_mod2_11.NTSUnCheckValue = "N"
    Me.ckTb_mod2_11.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_11.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_11.Properties.AutoHeight = False
    Me.ckTb_mod2_11.Properties.Caption = "RSM"
    Me.ckTb_mod2_11.Size = New System.Drawing.Size(55, 19)
    Me.ckTb_mod2_11.TabIndex = 26
    '
    'ckTb_mod2_10
    '
    Me.ckTb_mod2_10.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_10.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_mod2_10.Name = "ckTb_mod2_10"
    Me.ckTb_mod2_10.NTSCheckValue = "S"
    Me.ckTb_mod2_10.NTSUnCheckValue = "N"
    Me.ckTb_mod2_10.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_10.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_10.Properties.AutoHeight = False
    Me.ckTb_mod2_10.Properties.Caption = "(a disposizione - 5)"
    Me.ckTb_mod2_10.Size = New System.Drawing.Size(123, 19)
    Me.ckTb_mod2_10.TabIndex = 25
    '
    'ckTb_mod2_9
    '
    Me.ckTb_mod2_9.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_9.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_mod2_9.Name = "ckTb_mod2_9"
    Me.ckTb_mod2_9.NTSCheckValue = "S"
    Me.ckTb_mod2_9.NTSUnCheckValue = "N"
    Me.ckTb_mod2_9.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_9.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_9.Properties.AutoHeight = False
    Me.ckTb_mod2_9.Properties.Caption = "Tesoreria"
    Me.ckTb_mod2_9.Size = New System.Drawing.Size(77, 19)
    Me.ckTb_mod2_9.TabIndex = 24
    '
    'ckTb_mod2_8
    '
    Me.ckTb_mod2_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_8.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_mod2_8.Name = "ckTb_mod2_8"
    Me.ckTb_mod2_8.NTSCheckValue = "S"
    Me.ckTb_mod2_8.NTSUnCheckValue = "N"
    Me.ckTb_mod2_8.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_8.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_8.Properties.AutoHeight = False
    Me.ckTb_mod2_8.Properties.Caption = "(a disposizione - 4)"
    Me.ckTb_mod2_8.Size = New System.Drawing.Size(123, 19)
    Me.ckTb_mod2_8.TabIndex = 23
    '
    'ckTb_mod2_7
    '
    Me.ckTb_mod2_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_7.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_mod2_7.Name = "ckTb_mod2_7"
    Me.ckTb_mod2_7.NTSCheckValue = "S"
    Me.ckTb_mod2_7.NTSUnCheckValue = "N"
    Me.ckTb_mod2_7.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_7.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_7.Properties.AutoHeight = False
    Me.ckTb_mod2_7.Properties.Caption = "(a disposizione - 3)"
    Me.ckTb_mod2_7.Size = New System.Drawing.Size(123, 19)
    Me.ckTb_mod2_7.TabIndex = 22
    '
    'ckTb_mod2_6
    '
    Me.ckTb_mod2_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_6.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_mod2_6.Name = "ckTb_mod2_6"
    Me.ckTb_mod2_6.NTSCheckValue = "S"
    Me.ckTb_mod2_6.NTSUnCheckValue = "N"
    Me.ckTb_mod2_6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_6.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_6.Properties.AutoHeight = False
    Me.ckTb_mod2_6.Properties.Caption = "Ritenute Acconto"
    Me.ckTb_mod2_6.Size = New System.Drawing.Size(123, 19)
    Me.ckTb_mod2_6.TabIndex = 21
    '
    'pnPag1Center
    '
    Me.pnPag1Center.AllowDrop = True
    Me.pnPag1Center.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1Center.Appearance.Options.UseBackColor = True
    Me.pnPag1Center.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1Center.Controls.Add(Me.ckTb_mod2_5)
    Me.pnPag1Center.Controls.Add(Me.ckTb_mod2_4)
    Me.pnPag1Center.Controls.Add(Me.ckTb_mod2_3)
    Me.pnPag1Center.Controls.Add(Me.ckTb_mod2_2)
    Me.pnPag1Center.Controls.Add(Me.ckTb_mod2_1)
    Me.pnPag1Center.Controls.Add(Me.ckTb_mod_15)
    Me.pnPag1Center.Controls.Add(Me.ckTb_mod_14)
    Me.pnPag1Center.Controls.Add(Me.ckTb_mod_13)
    Me.pnPag1Center.Controls.Add(Me.ckTb_mod_12)
    Me.pnPag1Center.Controls.Add(Me.ckTb_mod_11)
    Me.pnPag1Center.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1Center.Location = New System.Drawing.Point(206, 3)
    Me.pnPag1Center.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag1Center.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag1Center.Name = "pnPag1Center"
    Me.pnPag1Center.NTSActiveTrasparency = True
    Me.pnPag1Center.Size = New System.Drawing.Size(197, 264)
    Me.pnPag1Center.TabIndex = 22
    Me.pnPag1Center.Text = "NtsPanel1"
    '
    'ckTb_mod2_5
    '
    Me.ckTb_mod2_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_5.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_mod2_5.Name = "ckTb_mod2_5"
    Me.ckTb_mod2_5.NTSCheckValue = "S"
    Me.ckTb_mod2_5.NTSUnCheckValue = "N"
    Me.ckTb_mod2_5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_5.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_5.Properties.AutoHeight = False
    Me.ckTb_mod2_5.Properties.Caption = "Generaz. PDF da documenti"
    Me.ckTb_mod2_5.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_mod2_5.TabIndex = 30
    '
    'ckTb_mod2_4
    '
    Me.ckTb_mod2_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_4.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_mod2_4.Name = "ckTb_mod2_4"
    Me.ckTb_mod2_4.NTSCheckValue = "S"
    Me.ckTb_mod2_4.NTSUnCheckValue = "N"
    Me.ckTb_mod2_4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_4.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_4.Properties.AutoHeight = False
    Me.ckTb_mod2_4.Properties.Caption = "Intrastat"
    Me.ckTb_mod2_4.Size = New System.Drawing.Size(74, 19)
    Me.ckTb_mod2_4.TabIndex = 29
    '
    'ckTb_mod2_3
    '
    Me.ckTb_mod2_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_3.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_mod2_3.Name = "ckTb_mod2_3"
    Me.ckTb_mod2_3.NTSCheckValue = "S"
    Me.ckTb_mod2_3.NTSUnCheckValue = "N"
    Me.ckTb_mod2_3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_3.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_3.Properties.AutoHeight = False
    Me.ckTb_mod2_3.Properties.Caption = "Project management"
    Me.ckTb_mod2_3.Size = New System.Drawing.Size(137, 19)
    Me.ckTb_mod2_3.TabIndex = 28
    '
    'ckTb_mod2_2
    '
    Me.ckTb_mod2_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_2.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_mod2_2.Name = "ckTb_mod2_2"
    Me.ckTb_mod2_2.NTSCheckValue = "S"
    Me.ckTb_mod2_2.NTSUnCheckValue = "N"
    Me.ckTb_mod2_2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_2.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_2.Properties.AutoHeight = False
    Me.ckTb_mod2_2.Properties.Caption = "Tentata vendita"
    Me.ckTb_mod2_2.Size = New System.Drawing.Size(109, 19)
    Me.ckTb_mod2_2.TabIndex = 27
    '
    'ckTb_mod2_1
    '
    Me.ckTb_mod2_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod2_1.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_mod2_1.Name = "ckTb_mod2_1"
    Me.ckTb_mod2_1.NTSCheckValue = "S"
    Me.ckTb_mod2_1.NTSUnCheckValue = "N"
    Me.ckTb_mod2_1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod2_1.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod2_1.Properties.AutoHeight = False
    Me.ckTb_mod2_1.Properties.Caption = "Richiesta d'acquisto"
    Me.ckTb_mod2_1.Size = New System.Drawing.Size(128, 19)
    Me.ckTb_mod2_1.TabIndex = 26
    '
    'ckTb_mod_15
    '
    Me.ckTb_mod_15.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_15.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_mod_15.Name = "ckTb_mod_15"
    Me.ckTb_mod_15.NTSCheckValue = "S"
    Me.ckTb_mod_15.NTSUnCheckValue = "N"
    Me.ckTb_mod_15.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_15.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_15.Properties.AutoHeight = False
    Me.ckTb_mod_15.Properties.Caption = "Import/Export dati"
    Me.ckTb_mod_15.Size = New System.Drawing.Size(128, 19)
    Me.ckTb_mod_15.TabIndex = 25
    '
    'ckTb_mod_14
    '
    Me.ckTb_mod_14.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_14.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_mod_14.Name = "ckTb_mod_14"
    Me.ckTb_mod_14.NTSCheckValue = "S"
    Me.ckTb_mod_14.NTSUnCheckValue = "N"
    Me.ckTb_mod_14.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_14.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_14.Properties.AutoHeight = False
    Me.ckTb_mod_14.Properties.Caption = "Produzione"
    Me.ckTb_mod_14.Size = New System.Drawing.Size(85, 19)
    Me.ckTb_mod_14.TabIndex = 24
    '
    'ckTb_mod_13
    '
    Me.ckTb_mod_13.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_13.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_mod_13.Name = "ckTb_mod_13"
    Me.ckTb_mod_13.NTSCheckValue = "S"
    Me.ckTb_mod_13.NTSUnCheckValue = "N"
    Me.ckTb_mod_13.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_13.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_13.Properties.AutoHeight = False
    Me.ckTb_mod_13.Properties.Caption = "Customer Service"
    Me.ckTb_mod_13.Size = New System.Drawing.Size(128, 19)
    Me.ckTb_mod_13.TabIndex = 23
    '
    'ckTb_mod_12
    '
    Me.ckTb_mod_12.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_12.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_mod_12.Name = "ckTb_mod_12"
    Me.ckTb_mod_12.NTSCheckValue = "S"
    Me.ckTb_mod_12.NTSUnCheckValue = "N"
    Me.ckTb_mod_12.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_12.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_12.Properties.AutoHeight = False
    Me.ckTb_mod_12.Properties.Caption = "Conai"
    Me.ckTb_mod_12.Size = New System.Drawing.Size(65, 19)
    Me.ckTb_mod_12.TabIndex = 22
    '
    'ckTb_mod_11
    '
    Me.ckTb_mod_11.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_11.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_mod_11.Name = "ckTb_mod_11"
    Me.ckTb_mod_11.NTSCheckValue = "S"
    Me.ckTb_mod_11.NTSUnCheckValue = "N"
    Me.ckTb_mod_11.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_11.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_11.Properties.AutoHeight = False
    Me.ckTb_mod_11.Properties.Caption = "Source Extender"
    Me.ckTb_mod_11.Size = New System.Drawing.Size(109, 19)
    Me.ckTb_mod_11.TabIndex = 21
    '
    'pnPag1Left
    '
    Me.pnPag1Left.AllowDrop = True
    Me.pnPag1Left.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag1Left.Appearance.Options.UseBackColor = True
    Me.pnPag1Left.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag1Left.Controls.Add(Me.ckTb_mod_10)
    Me.pnPag1Left.Controls.Add(Me.ckTb_mod_9)
    Me.pnPag1Left.Controls.Add(Me.ckTb_mod_8)
    Me.pnPag1Left.Controls.Add(Me.ckTb_mod_7)
    Me.pnPag1Left.Controls.Add(Me.ckTb_mod_6)
    Me.pnPag1Left.Controls.Add(Me.ckTb_mod_5)
    Me.pnPag1Left.Controls.Add(Me.ckTb_mod_4)
    Me.pnPag1Left.Controls.Add(Me.ckTb_mod_3)
    Me.pnPag1Left.Controls.Add(Me.ckTb_mod_2)
    Me.pnPag1Left.Controls.Add(Me.ckTb_mod_1)
    Me.pnPag1Left.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag1Left.Location = New System.Drawing.Point(3, 3)
    Me.pnPag1Left.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag1Left.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag1Left.Name = "pnPag1Left"
    Me.pnPag1Left.NTSActiveTrasparency = True
    Me.pnPag1Left.Size = New System.Drawing.Size(197, 264)
    Me.pnPag1Left.TabIndex = 21
    Me.pnPag1Left.Text = "NtsPanel1"
    '
    'ckTb_mod_10
    '
    Me.ckTb_mod_10.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_10.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_mod_10.Name = "ckTb_mod_10"
    Me.ckTb_mod_10.NTSCheckValue = "S"
    Me.ckTb_mod_10.NTSUnCheckValue = "N"
    Me.ckTb_mod_10.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_10.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_10.Properties.AutoHeight = False
    Me.ckTb_mod_10.Properties.Caption = "Verticale 1"
    Me.ckTb_mod_10.Size = New System.Drawing.Size(87, 19)
    Me.ckTb_mod_10.TabIndex = 30
    '
    'ckTb_mod_9
    '
    Me.ckTb_mod_9.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_9.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_mod_9.Name = "ckTb_mod_9"
    Me.ckTb_mod_9.NTSCheckValue = "S"
    Me.ckTb_mod_9.NTSUnCheckValue = "N"
    Me.ckTb_mod_9.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_9.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_9.Properties.AutoHeight = False
    Me.ckTb_mod_9.Properties.Caption = "Cespiti"
    Me.ckTb_mod_9.Size = New System.Drawing.Size(62, 19)
    Me.ckTb_mod_9.TabIndex = 29
    '
    'ckTb_mod_8
    '
    Me.ckTb_mod_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_8.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_mod_8.Name = "ckTb_mod_8"
    Me.ckTb_mod_8.NTSCheckValue = "S"
    Me.ckTb_mod_8.NTSUnCheckValue = "N"
    Me.ckTb_mod_8.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_8.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_8.Properties.AutoHeight = False
    Me.ckTb_mod_8.Properties.Caption = "Contabilità Analitica"
    Me.ckTb_mod_8.Size = New System.Drawing.Size(120, 19)
    Me.ckTb_mod_8.TabIndex = 28
    '
    'ckTb_mod_7
    '
    Me.ckTb_mod_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_7.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_mod_7.Name = "ckTb_mod_7"
    Me.ckTb_mod_7.NTSCheckValue = "S"
    Me.ckTb_mod_7.NTSUnCheckValue = "N"
    Me.ckTb_mod_7.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_7.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_7.Properties.AutoHeight = False
    Me.ckTb_mod_7.Properties.Caption = "Distinta Base"
    Me.ckTb_mod_7.Size = New System.Drawing.Size(103, 19)
    Me.ckTb_mod_7.TabIndex = 27
    '
    'ckTb_mod_6
    '
    Me.ckTb_mod_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_6.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_mod_6.Name = "ckTb_mod_6"
    Me.ckTb_mod_6.NTSCheckValue = "S"
    Me.ckTb_mod_6.NTSUnCheckValue = "N"
    Me.ckTb_mod_6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_6.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_6.Properties.AutoHeight = False
    Me.ckTb_mod_6.Properties.Caption = "Magazzino"
    Me.ckTb_mod_6.Size = New System.Drawing.Size(87, 19)
    Me.ckTb_mod_6.TabIndex = 26
    '
    'ckTb_mod_5
    '
    Me.ckTb_mod_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_5.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_mod_5.Name = "ckTb_mod_5"
    Me.ckTb_mod_5.NTSCheckValue = "S"
    Me.ckTb_mod_5.NTSUnCheckValue = "N"
    Me.ckTb_mod_5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_5.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_5.Properties.AutoHeight = False
    Me.ckTb_mod_5.Properties.Caption = "Statistiche"
    Me.ckTb_mod_5.Size = New System.Drawing.Size(87, 19)
    Me.ckTb_mod_5.TabIndex = 25
    '
    'ckTb_mod_4
    '
    Me.ckTb_mod_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_4.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_mod_4.Name = "ckTb_mod_4"
    Me.ckTb_mod_4.NTSCheckValue = "S"
    Me.ckTb_mod_4.NTSUnCheckValue = "N"
    Me.ckTb_mod_4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_4.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_4.Properties.AutoHeight = False
    Me.ckTb_mod_4.Properties.Caption = "Provvigioni"
    Me.ckTb_mod_4.Size = New System.Drawing.Size(87, 19)
    Me.ckTb_mod_4.TabIndex = 24
    '
    'ckTb_mod_3
    '
    Me.ckTb_mod_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_3.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_mod_3.Name = "ckTb_mod_3"
    Me.ckTb_mod_3.NTSCheckValue = "S"
    Me.ckTb_mod_3.NTSUnCheckValue = "N"
    Me.ckTb_mod_3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_3.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_3.Properties.AutoHeight = False
    Me.ckTb_mod_3.Properties.Caption = "Vendite"
    Me.ckTb_mod_3.Size = New System.Drawing.Size(87, 19)
    Me.ckTb_mod_3.TabIndex = 23
    '
    'ckTb_mod_2
    '
    Me.ckTb_mod_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_2.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_mod_2.Name = "ckTb_mod_2"
    Me.ckTb_mod_2.NTSCheckValue = "S"
    Me.ckTb_mod_2.NTSUnCheckValue = "N"
    Me.ckTb_mod_2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_2.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_2.Properties.AutoHeight = False
    Me.ckTb_mod_2.Properties.Caption = "Ordini"
    Me.ckTb_mod_2.Size = New System.Drawing.Size(62, 19)
    Me.ckTb_mod_2.TabIndex = 22
    '
    'ckTb_mod_1
    '
    Me.ckTb_mod_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod_1.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_mod_1.Name = "ckTb_mod_1"
    Me.ckTb_mod_1.NTSCheckValue = "S"
    Me.ckTb_mod_1.NTSUnCheckValue = "N"
    Me.ckTb_mod_1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod_1.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod_1.Properties.AutoHeight = False
    Me.ckTb_mod_1.Properties.Caption = "Contabilità Generale/semplificata"
    Me.ckTb_mod_1.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_mod_1.TabIndex = 21
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnPag2)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(609, 267)
    Me.NtsTabPage2.Text = "Moduli &2"
    Me.NtsTabPage2.Tooltip = "Opzioni Generali"
    '
    'pnPag2
    '
    Me.pnPag2.AllowDrop = True
    Me.pnPag2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag2.Appearance.Options.UseBackColor = True
    Me.pnPag2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag2.Controls.Add(Me.pnPag2Right)
    Me.pnPag2.Controls.Add(Me.pnPag2Center)
    Me.pnPag2.Controls.Add(Me.pnPag2Left)
    Me.pnPag2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag2.Location = New System.Drawing.Point(0, 0)
    Me.pnPag2.Name = "pnPag2"
    Me.pnPag2.NTSActiveTrasparency = True
    Me.pnPag2.Size = New System.Drawing.Size(609, 273)
    Me.pnPag2.TabIndex = 1
    Me.pnPag2.Text = "NtsPanel1"
    '
    'pnPag2Right
    '
    Me.pnPag2Right.AllowDrop = True
    Me.pnPag2Right.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag2Right.Appearance.Options.UseBackColor = True
    Me.pnPag2Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag2Right.Controls.Add(Me.ckTb_mod4_15)
    Me.pnPag2Right.Controls.Add(Me.ckTb_mod4_14)
    Me.pnPag2Right.Controls.Add(Me.ckTb_mod4_13)
    Me.pnPag2Right.Controls.Add(Me.ckTb_mod4_12)
    Me.pnPag2Right.Controls.Add(Me.ckTb_mod4_11)
    Me.pnPag2Right.Controls.Add(Me.ckTb_mod4_10)
    Me.pnPag2Right.Controls.Add(Me.ckTb_mod4_9)
    Me.pnPag2Right.Controls.Add(Me.ckTb_mod4_8)
    Me.pnPag2Right.Controls.Add(Me.ckTb_mod4_7)
    Me.pnPag2Right.Controls.Add(Me.ckTb_mod4_6)
    Me.pnPag2Right.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag2Right.Location = New System.Drawing.Point(409, 3)
    Me.pnPag2Right.Name = "pnPag2Right"
    Me.pnPag2Right.NTSActiveTrasparency = True
    Me.pnPag2Right.Size = New System.Drawing.Size(197, 264)
    Me.pnPag2Right.TabIndex = 23
    Me.pnPag2Right.Text = "NtsPanel1"
    '
    'ckTb_mod4_15
    '
    Me.ckTb_mod4_15.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_15.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_mod4_15.Name = "ckTb_mod4_15"
    Me.ckTb_mod4_15.NTSCheckValue = "S"
    Me.ckTb_mod4_15.NTSUnCheckValue = "N"
    Me.ckTb_mod4_15.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_15.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_15.Properties.AutoHeight = False
    Me.ckTb_mod4_15.Properties.Caption = "Moduli Supplementari Partner"
    Me.ckTb_mod4_15.Size = New System.Drawing.Size(162, 19)
    Me.ckTb_mod4_15.TabIndex = 30
    '
    'ckTb_mod4_14
    '
    Me.ckTb_mod4_14.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_14.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_mod4_14.Name = "ckTb_mod4_14"
    Me.ckTb_mod4_14.NTSCheckValue = "S"
    Me.ckTb_mod4_14.NTSUnCheckValue = "N"
    Me.ckTb_mod4_14.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_14.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_14.Properties.AutoHeight = False
    Me.ckTb_mod4_14.Properties.Caption = "(a disposizione - 7)"
    Me.ckTb_mod4_14.Size = New System.Drawing.Size(126, 19)
    Me.ckTb_mod4_14.TabIndex = 29
    '
    'ckTb_mod4_13
    '
    Me.ckTb_mod4_13.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_13.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_mod4_13.Name = "ckTb_mod4_13"
    Me.ckTb_mod4_13.NTSCheckValue = "S"
    Me.ckTb_mod4_13.NTSUnCheckValue = "N"
    Me.ckTb_mod4_13.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_13.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_13.Properties.AutoHeight = False
    Me.ckTb_mod4_13.Properties.Caption = "(a disposizione - 6)"
    Me.ckTb_mod4_13.Size = New System.Drawing.Size(126, 19)
    Me.ckTb_mod4_13.TabIndex = 28
    '
    'ckTb_mod4_12
    '
    Me.ckTb_mod4_12.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_12.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_mod4_12.Name = "ckTb_mod4_12"
    Me.ckTb_mod4_12.NTSCheckValue = "S"
    Me.ckTb_mod4_12.NTSUnCheckValue = "N"
    Me.ckTb_mod4_12.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_12.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_12.Properties.AutoHeight = False
    Me.ckTb_mod4_12.Properties.Caption = "Data-Warehouse"
    Me.ckTb_mod4_12.Size = New System.Drawing.Size(114, 19)
    Me.ckTb_mod4_12.TabIndex = 27
    '
    'ckTb_mod4_11
    '
    Me.ckTb_mod4_11.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_11.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_mod4_11.Name = "ckTb_mod4_11"
    Me.ckTb_mod4_11.NTSCheckValue = "S"
    Me.ckTb_mod4_11.NTSUnCheckValue = "N"
    Me.ckTb_mod4_11.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_11.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_11.Properties.AutoHeight = False
    Me.ckTb_mod4_11.Properties.Caption = "Moduli Supplementari NTS"
    Me.ckTb_mod4_11.Size = New System.Drawing.Size(153, 19)
    Me.ckTb_mod4_11.TabIndex = 26
    '
    'ckTb_mod4_10
    '
    Me.ckTb_mod4_10.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_10.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_mod4_10.Name = "ckTb_mod4_10"
    Me.ckTb_mod4_10.NTSCheckValue = "S"
    Me.ckTb_mod4_10.NTSUnCheckValue = "N"
    Me.ckTb_mod4_10.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_10.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_10.Properties.AutoHeight = False
    Me.ckTb_mod4_10.Properties.Caption = "BUSNET Frame"
    Me.ckTb_mod4_10.Size = New System.Drawing.Size(100, 19)
    Me.ckTb_mod4_10.TabIndex = 25
    '
    'ckTb_mod4_9
    '
    Me.ckTb_mod4_9.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_9.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_mod4_9.Name = "ckTb_mod4_9"
    Me.ckTb_mod4_9.NTSCheckValue = "S"
    Me.ckTb_mod4_9.NTSUnCheckValue = "N"
    Me.ckTb_mod4_9.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_9.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_9.Properties.AutoHeight = False
    Me.ckTb_mod4_9.Properties.Caption = "......................................"
    Me.ckTb_mod4_9.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_mod4_9.TabIndex = 24
    '
    'ckTb_mod4_8
    '
    Me.ckTb_mod4_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_8.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_mod4_8.Name = "ckTb_mod4_8"
    Me.ckTb_mod4_8.NTSCheckValue = "S"
    Me.ckTb_mod4_8.NTSUnCheckValue = "N"
    Me.ckTb_mod4_8.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_8.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_8.Properties.AutoHeight = False
    Me.ckTb_mod4_8.Properties.Caption = "......................................"
    Me.ckTb_mod4_8.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_mod4_8.TabIndex = 23
    '
    'ckTb_mod4_7
    '
    Me.ckTb_mod4_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_7.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_mod4_7.Name = "ckTb_mod4_7"
    Me.ckTb_mod4_7.NTSCheckValue = "S"
    Me.ckTb_mod4_7.NTSUnCheckValue = "N"
    Me.ckTb_mod4_7.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_7.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_7.Properties.AutoHeight = False
    Me.ckTb_mod4_7.Properties.Caption = "Produzione Taglie e Colori"
    Me.ckTb_mod4_7.Size = New System.Drawing.Size(162, 19)
    Me.ckTb_mod4_7.TabIndex = 22
    '
    'ckTb_mod4_6
    '
    Me.ckTb_mod4_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_6.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_mod4_6.Name = "ckTb_mod4_6"
    Me.ckTb_mod4_6.NTSCheckValue = "S"
    Me.ckTb_mod4_6.NTSUnCheckValue = "N"
    Me.ckTb_mod4_6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_6.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_6.Properties.AutoHeight = False
    Me.ckTb_mod4_6.Properties.Caption = "Elenchi Iva Clienti-Fornitori"
    Me.ckTb_mod4_6.Size = New System.Drawing.Size(162, 19)
    Me.ckTb_mod4_6.TabIndex = 21
    '
    'pnPag2Center
    '
    Me.pnPag2Center.AllowDrop = True
    Me.pnPag2Center.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag2Center.Appearance.Options.UseBackColor = True
    Me.pnPag2Center.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag2Center.Controls.Add(Me.ckTb_mod4_5)
    Me.pnPag2Center.Controls.Add(Me.ckTb_mod4_4)
    Me.pnPag2Center.Controls.Add(Me.ckTb_mod4_3)
    Me.pnPag2Center.Controls.Add(Me.ckTb_mod4_2)
    Me.pnPag2Center.Controls.Add(Me.ckTb_mod4_1)
    Me.pnPag2Center.Controls.Add(Me.ckTb_mod3_15)
    Me.pnPag2Center.Controls.Add(Me.ckTb_mod3_14)
    Me.pnPag2Center.Controls.Add(Me.ckTb_mod3_13)
    Me.pnPag2Center.Controls.Add(Me.ckTb_mod3_12)
    Me.pnPag2Center.Controls.Add(Me.ckTb_mod3_11)
    Me.pnPag2Center.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag2Center.Location = New System.Drawing.Point(206, 3)
    Me.pnPag2Center.Name = "pnPag2Center"
    Me.pnPag2Center.NTSActiveTrasparency = True
    Me.pnPag2Center.Size = New System.Drawing.Size(197, 264)
    Me.pnPag2Center.TabIndex = 22
    Me.pnPag2Center.Text = "NtsPanel1"
    '
    'ckTb_mod4_5
    '
    Me.ckTb_mod4_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_5.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_mod4_5.Name = "ckTb_mod4_5"
    Me.ckTb_mod4_5.NTSCheckValue = "S"
    Me.ckTb_mod4_5.NTSUnCheckValue = "N"
    Me.ckTb_mod4_5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_5.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_5.Properties.AutoHeight = False
    Me.ckTb_mod4_5.Properties.Caption = "Commesse light"
    Me.ckTb_mod4_5.Size = New System.Drawing.Size(109, 19)
    Me.ckTb_mod4_5.TabIndex = 30
    '
    'ckTb_mod4_4
    '
    Me.ckTb_mod4_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_4.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_mod4_4.Name = "ckTb_mod4_4"
    Me.ckTb_mod4_4.NTSCheckValue = "S"
    Me.ckTb_mod4_4.NTSUnCheckValue = "N"
    Me.ckTb_mod4_4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_4.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_4.Properties.AutoHeight = False
    Me.ckTb_mod4_4.Properties.Caption = "Non Solo Shop"
    Me.ckTb_mod4_4.Size = New System.Drawing.Size(109, 19)
    Me.ckTb_mod4_4.TabIndex = 29
    '
    'ckTb_mod4_3
    '
    Me.ckTb_mod4_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_3.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_mod4_3.Name = "ckTb_mod4_3"
    Me.ckTb_mod4_3.NTSCheckValue = "S"
    Me.ckTb_mod4_3.NTSUnCheckValue = "N"
    Me.ckTb_mod4_3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_3.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_3.Properties.AutoHeight = False
    Me.ckTb_mod4_3.Properties.Caption = "International"
    Me.ckTb_mod4_3.Size = New System.Drawing.Size(93, 19)
    Me.ckTb_mod4_3.TabIndex = 28
    '
    'ckTb_mod4_2
    '
    Me.ckTb_mod4_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_2.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_mod4_2.Name = "ckTb_mod4_2"
    Me.ckTb_mod4_2.NTSCheckValue = "S"
    Me.ckTb_mod4_2.NTSUnCheckValue = "N"
    Me.ckTb_mod4_2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_2.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_2.Properties.AutoHeight = False
    Me.ckTb_mod4_2.Properties.Caption = "Verticale 2"
    Me.ckTb_mod4_2.Size = New System.Drawing.Size(88, 19)
    Me.ckTb_mod4_2.TabIndex = 27
    '
    'ckTb_mod4_1
    '
    Me.ckTb_mod4_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod4_1.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_mod4_1.Name = "ckTb_mod4_1"
    Me.ckTb_mod4_1.NTSCheckValue = "S"
    Me.ckTb_mod4_1.NTSUnCheckValue = "N"
    Me.ckTb_mod4_1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod4_1.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod4_1.Properties.AutoHeight = False
    Me.ckTb_mod4_1.Properties.Caption = "Logistica Extended"
    Me.ckTb_mod4_1.Size = New System.Drawing.Size(128, 19)
    Me.ckTb_mod4_1.TabIndex = 26
    '
    'ckTb_mod3_15
    '
    Me.ckTb_mod3_15.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_15.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_mod3_15.Name = "ckTb_mod3_15"
    Me.ckTb_mod3_15.NTSCheckValue = "S"
    Me.ckTb_mod3_15.NTSUnCheckValue = "N"
    Me.ckTb_mod3_15.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_15.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_15.Properties.AutoHeight = False
    Me.ckTb_mod3_15.Properties.Caption = "F24 BUSINESS"
    Me.ckTb_mod3_15.Size = New System.Drawing.Size(109, 19)
    Me.ckTb_mod3_15.TabIndex = 25
    '
    'ckTb_mod3_14
    '
    Me.ckTb_mod3_14.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_14.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_mod3_14.Name = "ckTb_mod3_14"
    Me.ckTb_mod3_14.NTSCheckValue = "S"
    Me.ckTb_mod3_14.NTSUnCheckValue = "N"
    Me.ckTb_mod3_14.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_14.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_14.Properties.AutoHeight = False
    Me.ckTb_mod3_14.Properties.Caption = "Contab. professionisti"
    Me.ckTb_mod3_14.Size = New System.Drawing.Size(141, 19)
    Me.ckTb_mod3_14.TabIndex = 24
    '
    'ckTb_mod3_13
    '
    Me.ckTb_mod3_13.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_13.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_mod3_13.Name = "ckTb_mod3_13"
    Me.ckTb_mod3_13.NTSCheckValue = "S"
    Me.ckTb_mod3_13.NTSUnCheckValue = "N"
    Me.ckTb_mod3_13.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_13.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_13.Properties.AutoHeight = False
    Me.ckTb_mod3_13.Properties.Caption = "Magazzino EASY"
    Me.ckTb_mod3_13.Size = New System.Drawing.Size(109, 19)
    Me.ckTb_mod3_13.TabIndex = 23
    '
    'ckTb_mod3_12
    '
    Me.ckTb_mod3_12.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_12.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_mod3_12.Name = "ckTb_mod3_12"
    Me.ckTb_mod3_12.NTSCheckValue = "S"
    Me.ckTb_mod3_12.NTSUnCheckValue = "N"
    Me.ckTb_mod3_12.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_12.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_12.Properties.AutoHeight = False
    Me.ckTb_mod3_12.Properties.Caption = "Ordini EASY"
    Me.ckTb_mod3_12.Size = New System.Drawing.Size(88, 19)
    Me.ckTb_mod3_12.TabIndex = 22
    '
    'ckTb_mod3_11
    '
    Me.ckTb_mod3_11.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_11.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_mod3_11.Name = "ckTb_mod3_11"
    Me.ckTb_mod3_11.NTSCheckValue = "S"
    Me.ckTb_mod3_11.NTSUnCheckValue = "N"
    Me.ckTb_mod3_11.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_11.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_11.Properties.AutoHeight = False
    Me.ckTb_mod3_11.Properties.Caption = "Contabilità generale EASY"
    Me.ckTb_mod3_11.Size = New System.Drawing.Size(152, 19)
    Me.ckTb_mod3_11.TabIndex = 21
    '
    'pnPag2Left
    '
    Me.pnPag2Left.AllowDrop = True
    Me.pnPag2Left.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag2Left.Appearance.Options.UseBackColor = True
    Me.pnPag2Left.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag2Left.Controls.Add(Me.ckTb_mod3_10)
    Me.pnPag2Left.Controls.Add(Me.ckTb_mod3_9)
    Me.pnPag2Left.Controls.Add(Me.ckTb_mod3_8)
    Me.pnPag2Left.Controls.Add(Me.ckTb_mod3_7)
    Me.pnPag2Left.Controls.Add(Me.ckTb_mod3_6)
    Me.pnPag2Left.Controls.Add(Me.ckTb_mod3_5)
    Me.pnPag2Left.Controls.Add(Me.ckTb_mod3_4)
    Me.pnPag2Left.Controls.Add(Me.ckTb_mod3_3)
    Me.pnPag2Left.Controls.Add(Me.ckTb_mod3_2)
    Me.pnPag2Left.Controls.Add(Me.ckTb_mod3_1)
    Me.pnPag2Left.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag2Left.Location = New System.Drawing.Point(3, 3)
    Me.pnPag2Left.Name = "pnPag2Left"
    Me.pnPag2Left.NTSActiveTrasparency = True
    Me.pnPag2Left.Size = New System.Drawing.Size(197, 264)
    Me.pnPag2Left.TabIndex = 21
    Me.pnPag2Left.Text = "NtsPanel1"
    '
    'ckTb_mod3_10
    '
    Me.ckTb_mod3_10.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_10.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_mod3_10.Name = "ckTb_mod3_10"
    Me.ckTb_mod3_10.NTSCheckValue = "S"
    Me.ckTb_mod3_10.NTSUnCheckValue = "N"
    Me.ckTb_mod3_10.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_10.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_10.Properties.AutoHeight = False
    Me.ckTb_mod3_10.Properties.Caption = "Anagrafiche Generali"
    Me.ckTb_mod3_10.Size = New System.Drawing.Size(133, 19)
    Me.ckTb_mod3_10.TabIndex = 30
    '
    'ckTb_mod3_9
    '
    Me.ckTb_mod3_9.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_9.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_mod3_9.Name = "ckTb_mod3_9"
    Me.ckTb_mod3_9.NTSCheckValue = "S"
    Me.ckTb_mod3_9.NTSUnCheckValue = "N"
    Me.ckTb_mod3_9.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_9.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_9.Properties.AutoHeight = False
    Me.ckTb_mod3_9.Properties.Caption = "Telematico Oper. Rilevanti Iva"
    Me.ckTb_mod3_9.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_mod3_9.TabIndex = 29
    '
    'ckTb_mod3_8
    '
    Me.ckTb_mod3_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_8.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_mod3_8.Name = "ckTb_mod3_8"
    Me.ckTb_mod3_8.NTSCheckValue = "S"
    Me.ckTb_mod3_8.NTSUnCheckValue = "N"
    Me.ckTb_mod3_8.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_8.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_8.Properties.AutoHeight = False
    Me.ckTb_mod3_8.Properties.Caption = "Parcellazione"
    Me.ckTb_mod3_8.Size = New System.Drawing.Size(94, 19)
    Me.ckTb_mod3_8.TabIndex = 28
    '
    'ckTb_mod3_7
    '
    Me.ckTb_mod3_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_7.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_mod3_7.Name = "ckTb_mod3_7"
    Me.ckTb_mod3_7.NTSCheckValue = "S"
    Me.ckTb_mod3_7.NTSUnCheckValue = "N"
    Me.ckTb_mod3_7.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_7.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_7.Properties.AutoHeight = False
    Me.ckTb_mod3_7.Properties.Caption = "Picking"
    Me.ckTb_mod3_7.Size = New System.Drawing.Size(59, 19)
    Me.ckTb_mod3_7.TabIndex = 27
    '
    'ckTb_mod3_6
    '
    Me.ckTb_mod3_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_6.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_mod3_6.Name = "ckTb_mod3_6"
    Me.ckTb_mod3_6.NTSCheckValue = "S"
    Me.ckTb_mod3_6.NTSUnCheckValue = "N"
    Me.ckTb_mod3_6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_6.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_6.Properties.AutoHeight = False
    Me.ckTb_mod3_6.Properties.Caption = "CRM"
    Me.ckTb_mod3_6.Size = New System.Drawing.Size(50, 19)
    Me.ckTb_mod3_6.TabIndex = 26
    '
    'ckTb_mod3_5
    '
    Me.ckTb_mod3_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_5.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_mod3_5.Name = "ckTb_mod3_5"
    Me.ckTb_mod3_5.NTSCheckValue = "S"
    Me.ckTb_mod3_5.NTSUnCheckValue = "N"
    Me.ckTb_mod3_5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_5.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_5.Properties.AutoHeight = False
    Me.ckTb_mod3_5.Properties.Caption = "Black List"
    Me.ckTb_mod3_5.Size = New System.Drawing.Size(78, 19)
    Me.ckTb_mod3_5.TabIndex = 25
    '
    'ckTb_mod3_4
    '
    Me.ckTb_mod3_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_4.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_mod3_4.Name = "ckTb_mod3_4"
    Me.ckTb_mod3_4.NTSCheckValue = "S"
    Me.ckTb_mod3_4.NTSUnCheckValue = "N"
    Me.ckTb_mod3_4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_4.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_4.Properties.AutoHeight = False
    Me.ckTb_mod3_4.Properties.Caption = "Bilancio UE e nota integrativa"
    Me.ckTb_mod3_4.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_mod3_4.TabIndex = 24
    '
    'ckTb_mod3_3
    '
    Me.ckTb_mod3_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_3.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_mod3_3.Name = "ckTb_mod3_3"
    Me.ckTb_mod3_3.NTSCheckValue = "S"
    Me.ckTb_mod3_3.NTSUnCheckValue = "N"
    Me.ckTb_mod3_3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_3.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_3.Properties.AutoHeight = False
    Me.ckTb_mod3_3.Properties.Caption = "Taglie e colori"
    Me.ckTb_mod3_3.Size = New System.Drawing.Size(94, 19)
    Me.ckTb_mod3_3.TabIndex = 23
    '
    'ckTb_mod3_2
    '
    Me.ckTb_mod3_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_2.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_mod3_2.Name = "ckTb_mod3_2"
    Me.ckTb_mod3_2.NTSCheckValue = "S"
    Me.ckTb_mod3_2.NTSUnCheckValue = "N"
    Me.ckTb_mod3_2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_2.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_2.Properties.AutoHeight = False
    Me.ckTb_mod3_2.Properties.Caption = "Retail"
    Me.ckTb_mod3_2.Size = New System.Drawing.Size(59, 19)
    Me.ckTb_mod3_2.TabIndex = 22
    '
    'ckTb_mod3_1
    '
    Me.ckTb_mod3_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_mod3_1.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_mod3_1.Name = "ckTb_mod3_1"
    Me.ckTb_mod3_1.NTSCheckValue = "S"
    Me.ckTb_mod3_1.NTSUnCheckValue = "N"
    Me.ckTb_mod3_1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_mod3_1.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_mod3_1.Properties.AutoHeight = False
    Me.ckTb_mod3_1.Properties.Caption = "Configuratore su distinta neutra"
    Me.ckTb_mod3_1.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_mod3_1.TabIndex = 21
    '
    'NtsTabPage4
    '
    Me.NtsTabPage4.AllowDrop = True
    Me.NtsTabPage4.Controls.Add(Me.pnPag4)
    Me.NtsTabPage4.Enable = True
    Me.NtsTabPage4.Name = "NtsTabPage4"
    Me.NtsTabPage4.Size = New System.Drawing.Size(609, 267)
    Me.NtsTabPage4.Text = "Mod Supl"
    Me.NtsTabPage4.Tooltip = "Moduli supplementari"
    '
    'pnPag4
    '
    Me.pnPag4.AllowDrop = True
    Me.pnPag4.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag4.Appearance.Options.UseBackColor = True
    Me.pnPag4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag4.Controls.Add(Me.pnPag4Right)
    Me.pnPag4.Controls.Add(Me.pnPag4Center)
    Me.pnPag4.Controls.Add(Me.pnPag4Left)
    Me.pnPag4.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag4.Location = New System.Drawing.Point(0, 0)
    Me.pnPag4.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag4.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag4.Name = "pnPag4"
    Me.pnPag4.NTSActiveTrasparency = True
    Me.pnPag4.Size = New System.Drawing.Size(609, 273)
    Me.pnPag4.TabIndex = 2
    Me.pnPag4.Text = "NtsPanel1"
    '
    'pnPag4Right
    '
    Me.pnPag4Right.AllowDrop = True
    Me.pnPag4Right.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag4Right.Appearance.Options.UseBackColor = True
    Me.pnPag4Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag4Right.Controls.Add(Me.ckTb_modsup_30)
    Me.pnPag4Right.Controls.Add(Me.ckTb_modsup_29)
    Me.pnPag4Right.Controls.Add(Me.ckTb_modsup_28)
    Me.pnPag4Right.Controls.Add(Me.ckTb_modsup_27)
    Me.pnPag4Right.Controls.Add(Me.ckTb_modsup_26)
    Me.pnPag4Right.Controls.Add(Me.ckTb_modsup_25)
    Me.pnPag4Right.Controls.Add(Me.ckTb_modsup_24)
    Me.pnPag4Right.Controls.Add(Me.ckTb_modsup_23)
    Me.pnPag4Right.Controls.Add(Me.ckTb_modsup_22)
    Me.pnPag4Right.Controls.Add(Me.ckTb_modsup_21)
    Me.pnPag4Right.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag4Right.Location = New System.Drawing.Point(409, 3)
    Me.pnPag4Right.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag4Right.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag4Right.Name = "pnPag4Right"
    Me.pnPag4Right.NTSActiveTrasparency = True
    Me.pnPag4Right.Size = New System.Drawing.Size(197, 264)
    Me.pnPag4Right.TabIndex = 23
    Me.pnPag4Right.Text = "NtsPanel1"
    '
    'ckTb_modsup_30
    '
    Me.ckTb_modsup_30.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_30.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_modsup_30.Name = "ckTb_modsup_30"
    Me.ckTb_modsup_30.NTSCheckValue = "S"
    Me.ckTb_modsup_30.NTSUnCheckValue = "N"
    Me.ckTb_modsup_30.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_30.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_30.Properties.AutoHeight = False
    Me.ckTb_modsup_30.Properties.Caption = "Friendly"
    Me.ckTb_modsup_30.Size = New System.Drawing.Size(22, 19)
    Me.ckTb_modsup_30.TabIndex = 30
    Me.ckTb_modsup_30.Visible = False
    '
    'ckTb_modsup_29
    '
    Me.ckTb_modsup_29.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_29.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_modsup_29.Name = "ckTb_modsup_29"
    Me.ckTb_modsup_29.NTSCheckValue = "S"
    Me.ckTb_modsup_29.NTSUnCheckValue = "N"
    Me.ckTb_modsup_29.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_29.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_29.Properties.AutoHeight = False
    Me.ckTb_modsup_29.Properties.Caption = "Smart Business Framework"
    Me.ckTb_modsup_29.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsup_29.TabIndex = 29
    '
    'ckTb_modsup_28
    '
    Me.ckTb_modsup_28.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_28.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_modsup_28.Name = "ckTb_modsup_28"
    Me.ckTb_modsup_28.NTSCheckValue = "S"
    Me.ckTb_modsup_28.NTSUnCheckValue = "N"
    Me.ckTb_modsup_28.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_28.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_28.Properties.AutoHeight = False
    Me.ckTb_modsup_28.Properties.Caption = "Crystal Reports 64bit server"
    Me.ckTb_modsup_28.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsup_28.TabIndex = 28
    '
    'ckTb_modsup_27
    '
    Me.ckTb_modsup_27.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_27.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_modsup_27.Name = "ckTb_modsup_27"
    Me.ckTb_modsup_27.NTSCheckValue = "S"
    Me.ckTb_modsup_27.NTSUnCheckValue = "N"
    Me.ckTb_modsup_27.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_27.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_27.Properties.AutoHeight = False
    Me.ckTb_modsup_27.Properties.Caption = "......................................"
    Me.ckTb_modsup_27.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsup_27.TabIndex = 27
    '
    'ckTb_modsup_26
    '
    Me.ckTb_modsup_26.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_26.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_modsup_26.Name = "ckTb_modsup_26"
    Me.ckTb_modsup_26.NTSCheckValue = "S"
    Me.ckTb_modsup_26.NTSUnCheckValue = "N"
    Me.ckTb_modsup_26.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_26.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_26.Properties.AutoHeight = False
    Me.ckTb_modsup_26.Properties.Caption = "Contratti e Condizioni Commerciali"
    Me.ckTb_modsup_26.Size = New System.Drawing.Size(191, 19)
    Me.ckTb_modsup_26.TabIndex = 26
    '
    'ckTb_modsup_25
    '
    Me.ckTb_modsup_25.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_25.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_modsup_25.Name = "ckTb_modsup_25"
    Me.ckTb_modsup_25.NTSCheckValue = "S"
    Me.ckTb_modsup_25.NTSUnCheckValue = "N"
    Me.ckTb_modsup_25.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_25.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_25.Properties.AutoHeight = False
    Me.ckTb_modsup_25.Properties.Caption = "Customer Service Map View"
    Me.ckTb_modsup_25.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsup_25.TabIndex = 25
    '
    'ckTb_modsup_24
    '
    Me.ckTb_modsup_24.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_24.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_modsup_24.Name = "ckTb_modsup_24"
    Me.ckTb_modsup_24.NTSCheckValue = "S"
    Me.ckTb_modsup_24.NTSUnCheckValue = "N"
    Me.ckTb_modsup_24.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_24.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_24.Properties.AutoHeight = False
    Me.ckTb_modsup_24.Properties.Caption = "Business For People Guest Ext."
    Me.ckTb_modsup_24.Size = New System.Drawing.Size(191, 19)
    Me.ckTb_modsup_24.TabIndex = 24
    '
    'ckTb_modsup_23
    '
    Me.ckTb_modsup_23.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_23.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_modsup_23.Name = "ckTb_modsup_23"
    Me.ckTb_modsup_23.NTSCheckValue = "S"
    Me.ckTb_modsup_23.NTSUnCheckValue = "N"
    Me.ckTb_modsup_23.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_23.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_23.Properties.AutoHeight = False
    Me.ckTb_modsup_23.Properties.Caption = "Collegamento a e-Commerce"
    Me.ckTb_modsup_23.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsup_23.TabIndex = 23
    '
    'ckTb_modsup_22
    '
    Me.ckTb_modsup_22.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_22.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_modsup_22.Name = "ckTb_modsup_22"
    Me.ckTb_modsup_22.NTSCheckValue = "S"
    Me.ckTb_modsup_22.NTSUnCheckValue = "N"
    Me.ckTb_modsup_22.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_22.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_22.Properties.AutoHeight = False
    Me.ckTb_modsup_22.Properties.Caption = "Dichiarazione di intenti"
    Me.ckTb_modsup_22.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsup_22.TabIndex = 22
    '
    'ckTb_modsup_21
    '
    Me.ckTb_modsup_21.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_21.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_modsup_21.Name = "ckTb_modsup_21"
    Me.ckTb_modsup_21.NTSCheckValue = "S"
    Me.ckTb_modsup_21.NTSUnCheckValue = "N"
    Me.ckTb_modsup_21.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_21.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_21.Properties.AutoHeight = False
    Me.ckTb_modsup_21.Properties.Caption = "Pec-mail"
    Me.ckTb_modsup_21.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsup_21.TabIndex = 21
    '
    'pnPag4Center
    '
    Me.pnPag4Center.AllowDrop = True
    Me.pnPag4Center.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag4Center.Appearance.Options.UseBackColor = True
    Me.pnPag4Center.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag4Center.Controls.Add(Me.ckTb_modsup_20)
    Me.pnPag4Center.Controls.Add(Me.ckTb_modsup_19)
    Me.pnPag4Center.Controls.Add(Me.ckTb_modsup_18)
    Me.pnPag4Center.Controls.Add(Me.ckTb_modsup_17)
    Me.pnPag4Center.Controls.Add(Me.ckTb_modsup_16)
    Me.pnPag4Center.Controls.Add(Me.ckTb_modsup_15)
    Me.pnPag4Center.Controls.Add(Me.ckTb_modsup_14)
    Me.pnPag4Center.Controls.Add(Me.ckTb_modsup_13)
    Me.pnPag4Center.Controls.Add(Me.ckTb_modsup_12)
    Me.pnPag4Center.Controls.Add(Me.ckTb_modsup_11)
    Me.pnPag4Center.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag4Center.Location = New System.Drawing.Point(206, 3)
    Me.pnPag4Center.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag4Center.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag4Center.Name = "pnPag4Center"
    Me.pnPag4Center.NTSActiveTrasparency = True
    Me.pnPag4Center.Size = New System.Drawing.Size(197, 264)
    Me.pnPag4Center.TabIndex = 22
    Me.pnPag4Center.Text = "NtsPanel1"
    '
    'ckTb_modsup_20
    '
    Me.ckTb_modsup_20.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_20.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_modsup_20.Name = "ckTb_modsup_20"
    Me.ckTb_modsup_20.NTSCheckValue = "S"
    Me.ckTb_modsup_20.NTSUnCheckValue = "N"
    Me.ckTb_modsup_20.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_20.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_20.Properties.AutoHeight = False
    Me.ckTb_modsup_20.Properties.Caption = "Analisi Fattibilità Impegni"
    Me.ckTb_modsup_20.Size = New System.Drawing.Size(145, 19)
    Me.ckTb_modsup_20.TabIndex = 30
    '
    'ckTb_modsup_19
    '
    Me.ckTb_modsup_19.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_19.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_modsup_19.Name = "ckTb_modsup_19"
    Me.ckTb_modsup_19.NTSCheckValue = "S"
    Me.ckTb_modsup_19.NTSUnCheckValue = "N"
    Me.ckTb_modsup_19.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_19.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_19.Properties.AutoHeight = False
    Me.ckTb_modsup_19.Properties.Caption = "Bus4People Extented"
    Me.ckTb_modsup_19.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsup_19.TabIndex = 29
    '
    'ckTb_modsup_18
    '
    Me.ckTb_modsup_18.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_18.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_modsup_18.Name = "ckTb_modsup_18"
    Me.ckTb_modsup_18.NTSCheckValue = "S"
    Me.ckTb_modsup_18.NTSUnCheckValue = "N"
    Me.ckTb_modsup_18.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_18.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_18.Properties.AutoHeight = False
    Me.ckTb_modsup_18.Properties.Caption = "Collegamento a NetPro Classic"
    Me.ckTb_modsup_18.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsup_18.TabIndex = 28
    '
    'ckTb_modsup_17
    '
    Me.ckTb_modsup_17.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_17.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_modsup_17.Name = "ckTb_modsup_17"
    Me.ckTb_modsup_17.NTSCheckValue = "S"
    Me.ckTb_modsup_17.NTSUnCheckValue = "N"
    Me.ckTb_modsup_17.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_17.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_17.Properties.AutoHeight = False
    Me.ckTb_modsup_17.Properties.Caption = "......................................"
    Me.ckTb_modsup_17.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsup_17.TabIndex = 27
    '
    'ckTb_modsup_16
    '
    Me.ckTb_modsup_16.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_16.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_modsup_16.Name = "ckTb_modsup_16"
    Me.ckTb_modsup_16.NTSCheckValue = "S"
    Me.ckTb_modsup_16.NTSUnCheckValue = "N"
    Me.ckTb_modsup_16.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_16.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_16.Properties.AutoHeight = False
    Me.ckTb_modsup_16.Properties.Caption = "Gestione incassi/pagam. differiti"
    Me.ckTb_modsup_16.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsup_16.TabIndex = 26
    '
    'ckTb_modsup_15
    '
    Me.ckTb_modsup_15.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_15.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_modsup_15.Name = "ckTb_modsup_15"
    Me.ckTb_modsup_15.NTSCheckValue = "S"
    Me.ckTb_modsup_15.NTSUnCheckValue = "N"
    Me.ckTb_modsup_15.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_15.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_15.Properties.AutoHeight = False
    Me.ckTb_modsup_15.Properties.Caption = "Gestione punti vendita recovery"
    Me.ckTb_modsup_15.Size = New System.Drawing.Size(191, 19)
    Me.ckTb_modsup_15.TabIndex = 25
    '
    'ckTb_modsup_14
    '
    Me.ckTb_modsup_14.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_14.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_modsup_14.Name = "ckTb_modsup_14"
    Me.ckTb_modsup_14.NTSCheckValue = "S"
    Me.ckTb_modsup_14.NTSUnCheckValue = "N"
    Me.ckTb_modsup_14.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_14.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_14.Properties.AutoHeight = False
    Me.ckTb_modsup_14.Properties.Caption = "Gestione punti vendita extended"
    Me.ckTb_modsup_14.Size = New System.Drawing.Size(191, 19)
    Me.ckTb_modsup_14.TabIndex = 24
    '
    'ckTb_modsup_13
    '
    Me.ckTb_modsup_13.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_13.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_modsup_13.Name = "ckTb_modsup_13"
    Me.ckTb_modsup_13.NTSCheckValue = "S"
    Me.ckTb_modsup_13.NTSUnCheckValue = "N"
    Me.ckTb_modsup_13.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_13.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_13.Properties.AutoHeight = False
    Me.ckTb_modsup_13.Properties.Caption = "Gestione punti vendita"
    Me.ckTb_modsup_13.Size = New System.Drawing.Size(145, 19)
    Me.ckTb_modsup_13.TabIndex = 23
    '
    'ckTb_modsup_12
    '
    Me.ckTb_modsup_12.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_12.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_modsup_12.Name = "ckTb_modsup_12"
    Me.ckTb_modsup_12.NTSCheckValue = "S"
    Me.ckTb_modsup_12.NTSUnCheckValue = "N"
    Me.ckTb_modsup_12.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_12.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_12.Properties.AutoHeight = False
    Me.ckTb_modsup_12.Properties.Caption = "Tesoreria Extended"
    Me.ckTb_modsup_12.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsup_12.TabIndex = 22
    '
    'ckTb_modsup_11
    '
    Me.ckTb_modsup_11.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_11.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_modsup_11.Name = "ckTb_modsup_11"
    Me.ckTb_modsup_11.NTSCheckValue = "S"
    Me.ckTb_modsup_11.NTSUnCheckValue = "N"
    Me.ckTb_modsup_11.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_11.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_11.Properties.AutoHeight = False
    Me.ckTb_modsup_11.Properties.Caption = "Tesoreria e flussi finanziari"
    Me.ckTb_modsup_11.Size = New System.Drawing.Size(160, 19)
    Me.ckTb_modsup_11.TabIndex = 21
    '
    'pnPag4Left
    '
    Me.pnPag4Left.AllowDrop = True
    Me.pnPag4Left.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag4Left.Appearance.Options.UseBackColor = True
    Me.pnPag4Left.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag4Left.Controls.Add(Me.ckTb_modsup_10)
    Me.pnPag4Left.Controls.Add(Me.ckTb_modsup_9)
    Me.pnPag4Left.Controls.Add(Me.ckTb_modsup_8)
    Me.pnPag4Left.Controls.Add(Me.ckTb_modsup_7)
    Me.pnPag4Left.Controls.Add(Me.ckTb_modsup_6)
    Me.pnPag4Left.Controls.Add(Me.ckTb_modsup_5)
    Me.pnPag4Left.Controls.Add(Me.ckTb_modsup_4)
    Me.pnPag4Left.Controls.Add(Me.ckTb_modsup_3)
    Me.pnPag4Left.Controls.Add(Me.ckTb_modsup_2)
    Me.pnPag4Left.Controls.Add(Me.ckTb_modsup_1)
    Me.pnPag4Left.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag4Left.Location = New System.Drawing.Point(3, 3)
    Me.pnPag4Left.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag4Left.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag4Left.Name = "pnPag4Left"
    Me.pnPag4Left.NTSActiveTrasparency = True
    Me.pnPag4Left.Size = New System.Drawing.Size(197, 264)
    Me.pnPag4Left.TabIndex = 21
    Me.pnPag4Left.Text = "NtsPanel1"
    '
    'ckTb_modsup_10
    '
    Me.ckTb_modsup_10.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_10.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_modsup_10.Name = "ckTb_modsup_10"
    Me.ckTb_modsup_10.NTSCheckValue = "S"
    Me.ckTb_modsup_10.NTSUnCheckValue = "N"
    Me.ckTb_modsup_10.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_10.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_10.Properties.AutoHeight = False
    Me.ckTb_modsup_10.Properties.Caption = "Desktop Consolle"
    Me.ckTb_modsup_10.Size = New System.Drawing.Size(115, 19)
    Me.ckTb_modsup_10.TabIndex = 30
    '
    'ckTb_modsup_9
    '
    Me.ckTb_modsup_9.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_9.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_modsup_9.Name = "ckTb_modsup_9"
    Me.ckTb_modsup_9.NTSCheckValue = "S"
    Me.ckTb_modsup_9.NTSUnCheckValue = "N"
    Me.ckTb_modsup_9.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_9.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_9.Properties.AutoHeight = False
    Me.ckTb_modsup_9.Properties.Caption = "Budget e controllo di gestione"
    Me.ckTb_modsup_9.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsup_9.TabIndex = 29
    '
    'ckTb_modsup_8
    '
    Me.ckTb_modsup_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_8.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_modsup_8.Name = "ckTb_modsup_8"
    Me.ckTb_modsup_8.NTSCheckValue = "S"
    Me.ckTb_modsup_8.NTSUnCheckValue = "N"
    Me.ckTb_modsup_8.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_8.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_8.Properties.AutoHeight = False
    Me.ckTb_modsup_8.Properties.Caption = "Contabilità Analitica DC"
    Me.ckTb_modsup_8.Size = New System.Drawing.Size(143, 19)
    Me.ckTb_modsup_8.TabIndex = 28
    '
    'ckTb_modsup_7
    '
    Me.ckTb_modsup_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_7.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_modsup_7.Name = "ckTb_modsup_7"
    Me.ckTb_modsup_7.NTSCheckValue = "S"
    Me.ckTb_modsup_7.NTSUnCheckValue = "N"
    Me.ckTb_modsup_7.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_7.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_7.Properties.AutoHeight = False
    Me.ckTb_modsup_7.Properties.Caption = "Datawarehouse  Extended"
    Me.ckTb_modsup_7.Size = New System.Drawing.Size(158, 19)
    Me.ckTb_modsup_7.TabIndex = 27
    '
    'ckTb_modsup_6
    '
    Me.ckTb_modsup_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_6.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_modsup_6.Name = "ckTb_modsup_6"
    Me.ckTb_modsup_6.NTSCheckValue = "S"
    Me.ckTb_modsup_6.NTSUnCheckValue = "N"
    Me.ckTb_modsup_6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_6.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_6.Properties.AutoHeight = False
    Me.ckTb_modsup_6.Properties.Caption = "E-mail integrata"
    Me.ckTb_modsup_6.Size = New System.Drawing.Size(107, 19)
    Me.ckTb_modsup_6.TabIndex = 26
    '
    'ckTb_modsup_5
    '
    Me.ckTb_modsup_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_5.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_modsup_5.Name = "ckTb_modsup_5"
    Me.ckTb_modsup_5.NTSCheckValue = "S"
    Me.ckTb_modsup_5.NTSUnCheckValue = "N"
    Me.ckTb_modsup_5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_5.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_5.Properties.AutoHeight = False
    Me.ckTb_modsup_5.Properties.Caption = "......................................"
    Me.ckTb_modsup_5.Size = New System.Drawing.Size(115, 19)
    Me.ckTb_modsup_5.TabIndex = 25
    '
    'ckTb_modsup_4
    '
    Me.ckTb_modsup_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_4.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_modsup_4.Name = "ckTb_modsup_4"
    Me.ckTb_modsup_4.NTSCheckValue = "S"
    Me.ckTb_modsup_4.NTSUnCheckValue = "N"
    Me.ckTb_modsup_4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_4.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_4.Properties.AutoHeight = False
    Me.ckTb_modsup_4.Properties.Caption = "......................................"
    Me.ckTb_modsup_4.Size = New System.Drawing.Size(85, 19)
    Me.ckTb_modsup_4.TabIndex = 24
    '
    'ckTb_modsup_3
    '
    Me.ckTb_modsup_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_3.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_modsup_3.Name = "ckTb_modsup_3"
    Me.ckTb_modsup_3.NTSCheckValue = "S"
    Me.ckTb_modsup_3.NTSUnCheckValue = "N"
    Me.ckTb_modsup_3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_3.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_3.Properties.AutoHeight = False
    Me.ckTb_modsup_3.Properties.Caption = "Logistica su palmari"
    Me.ckTb_modsup_3.Size = New System.Drawing.Size(128, 19)
    Me.ckTb_modsup_3.TabIndex = 23
    '
    'ckTb_modsup_2
    '
    Me.ckTb_modsup_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_2.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_modsup_2.Name = "ckTb_modsup_2"
    Me.ckTb_modsup_2.NTSCheckValue = "S"
    Me.ckTb_modsup_2.NTSUnCheckValue = "N"
    Me.ckTb_modsup_2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_2.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_2.Properties.AutoHeight = False
    Me.ckTb_modsup_2.Properties.Caption = "Controllo ric. merce su palmare"
    Me.ckTb_modsup_2.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsup_2.TabIndex = 22
    '
    'ckTb_modsup_1
    '
    Me.ckTb_modsup_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsup_1.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_modsup_1.Name = "ckTb_modsup_1"
    Me.ckTb_modsup_1.NTSCheckValue = "S"
    Me.ckTb_modsup_1.NTSUnCheckValue = "N"
    Me.ckTb_modsup_1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsup_1.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsup_1.Properties.AutoHeight = False
    Me.ckTb_modsup_1.Properties.Caption = "Inventari su palmare"
    Me.ckTb_modsup_1.Size = New System.Drawing.Size(128, 19)
    Me.ckTb_modsup_1.TabIndex = 21
    '
    'NtsTabPage5
    '
    Me.NtsTabPage5.AllowDrop = True
    Me.NtsTabPage5.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.NtsTabPage5.Appearance.Header.Options.UseFont = True
    Me.NtsTabPage5.Controls.Add(Me.pnPag5)
    Me.NtsTabPage5.Enable = True
    Me.NtsTabPage5.Name = "NtsTabPage5"
    Me.NtsTabPage5.Size = New System.Drawing.Size(609, 267)
    Me.NtsTabPage5.Text = "Mod Supl Ext"
    Me.NtsTabPage5.Tooltip = "Moduli supplementari Estesi"
    '
    'pnPag5
    '
    Me.pnPag5.AllowDrop = True
    Me.pnPag5.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag5.Appearance.Options.UseBackColor = True
    Me.pnPag5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag5.Controls.Add(Me.pnPag5Right)
    Me.pnPag5.Controls.Add(Me.pnPag5Center)
    Me.pnPag5.Controls.Add(Me.pnPag5Left)
    Me.pnPag5.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag5.Location = New System.Drawing.Point(0, 0)
    Me.pnPag5.Name = "pnPag5"
    Me.pnPag5.NTSActiveTrasparency = True
    Me.pnPag5.Size = New System.Drawing.Size(609, 273)
    Me.pnPag5.TabIndex = 3
    Me.pnPag5.Text = "NtsPanel1"
    '
    'pnPag5Right
    '
    Me.pnPag5Right.AllowDrop = True
    Me.pnPag5Right.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag5Right.Appearance.Options.UseBackColor = True
    Me.pnPag5Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag5Right.Controls.Add(Me.ckTb_modsupext_30)
    Me.pnPag5Right.Controls.Add(Me.ckTb_modsupext_29)
    Me.pnPag5Right.Controls.Add(Me.ckTb_modsupext_28)
    Me.pnPag5Right.Controls.Add(Me.ckTb_modsupext_27)
    Me.pnPag5Right.Controls.Add(Me.ckTb_modsupext_26)
    Me.pnPag5Right.Controls.Add(Me.ckTb_modsupext_25)
    Me.pnPag5Right.Controls.Add(Me.ckTb_modsupext_24)
    Me.pnPag5Right.Controls.Add(Me.ckTb_modsupext_23)
    Me.pnPag5Right.Controls.Add(Me.ckTb_modsupext_22)
    Me.pnPag5Right.Controls.Add(Me.ckTb_modsupext_21)
    Me.pnPag5Right.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag5Right.Location = New System.Drawing.Point(409, 3)
    Me.pnPag5Right.Name = "pnPag5Right"
    Me.pnPag5Right.NTSActiveTrasparency = True
    Me.pnPag5Right.Size = New System.Drawing.Size(197, 264)
    Me.pnPag5Right.TabIndex = 23
    Me.pnPag5Right.Text = "NtsPanel1"
    '
    'ckTb_modsupext_30
    '
    Me.ckTb_modsupext_30.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_30.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_modsupext_30.Name = "ckTb_modsupext_30"
    Me.ckTb_modsupext_30.NTSCheckValue = "S"
    Me.ckTb_modsupext_30.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_30.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_30.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_30.Properties.AutoHeight = False
    Me.ckTb_modsupext_30.Properties.Caption = "......................................"
    Me.ckTb_modsupext_30.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_30.TabIndex = 30
    '
    'ckTb_modsupext_29
    '
    Me.ckTb_modsupext_29.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_29.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_modsupext_29.Name = "ckTb_modsupext_29"
    Me.ckTb_modsupext_29.NTSCheckValue = "S"
    Me.ckTb_modsupext_29.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_29.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_29.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_29.Properties.AutoHeight = False
    Me.ckTb_modsupext_29.Properties.Caption = "......................................"
    Me.ckTb_modsupext_29.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_29.TabIndex = 29
    '
    'ckTb_modsupext_28
    '
    Me.ckTb_modsupext_28.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_28.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_modsupext_28.Name = "ckTb_modsupext_28"
    Me.ckTb_modsupext_28.NTSCheckValue = "S"
    Me.ckTb_modsupext_28.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_28.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_28.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_28.Properties.AutoHeight = False
    Me.ckTb_modsupext_28.Properties.Caption = "......................................"
    Me.ckTb_modsupext_28.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_28.TabIndex = 28
    '
    'ckTb_modsupext_27
    '
    Me.ckTb_modsupext_27.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_27.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_modsupext_27.Name = "ckTb_modsupext_27"
    Me.ckTb_modsupext_27.NTSCheckValue = "S"
    Me.ckTb_modsupext_27.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_27.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_27.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_27.Properties.AutoHeight = False
    Me.ckTb_modsupext_27.Properties.Caption = "......................................"
    Me.ckTb_modsupext_27.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_27.TabIndex = 27
    '
    'ckTb_modsupext_26
    '
    Me.ckTb_modsupext_26.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_26.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_modsupext_26.Name = "ckTb_modsupext_26"
    Me.ckTb_modsupext_26.NTSCheckValue = "S"
    Me.ckTb_modsupext_26.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_26.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_26.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_26.Properties.AutoHeight = False
    Me.ckTb_modsupext_26.Properties.Caption = "......................................"
    Me.ckTb_modsupext_26.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_26.TabIndex = 26
    '
    'ckTb_modsupext_25
    '
    Me.ckTb_modsupext_25.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_25.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_modsupext_25.Name = "ckTb_modsupext_25"
    Me.ckTb_modsupext_25.NTSCheckValue = "S"
    Me.ckTb_modsupext_25.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_25.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_25.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_25.Properties.AutoHeight = False
    Me.ckTb_modsupext_25.Properties.Caption = "......................................"
    Me.ckTb_modsupext_25.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_25.TabIndex = 25
    '
    'ckTb_modsupext_24
    '
    Me.ckTb_modsupext_24.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_24.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_modsupext_24.Name = "ckTb_modsupext_24"
    Me.ckTb_modsupext_24.NTSCheckValue = "S"
    Me.ckTb_modsupext_24.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_24.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_24.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_24.Properties.AutoHeight = False
    Me.ckTb_modsupext_24.Properties.Caption = "......................................"
    Me.ckTb_modsupext_24.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_24.TabIndex = 24
    '
    'ckTb_modsupext_23
    '
    Me.ckTb_modsupext_23.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_23.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_modsupext_23.Name = "ckTb_modsupext_23"
    Me.ckTb_modsupext_23.NTSCheckValue = "S"
    Me.ckTb_modsupext_23.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_23.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_23.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_23.Properties.AutoHeight = False
    Me.ckTb_modsupext_23.Properties.Caption = "......................................"
    Me.ckTb_modsupext_23.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_23.TabIndex = 23
    '
    'ckTb_modsupext_22
    '
    Me.ckTb_modsupext_22.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_22.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_modsupext_22.Name = "ckTb_modsupext_22"
    Me.ckTb_modsupext_22.NTSCheckValue = "S"
    Me.ckTb_modsupext_22.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_22.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_22.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_22.Properties.AutoHeight = False
    Me.ckTb_modsupext_22.Properties.Caption = "......................................"
    Me.ckTb_modsupext_22.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_22.TabIndex = 22
    '
    'ckTb_modsupext_21
    '
    Me.ckTb_modsupext_21.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_21.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_modsupext_21.Name = "ckTb_modsupext_21"
    Me.ckTb_modsupext_21.NTSCheckValue = "S"
    Me.ckTb_modsupext_21.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_21.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_21.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_21.Properties.AutoHeight = False
    Me.ckTb_modsupext_21.Properties.Caption = "......................................"
    Me.ckTb_modsupext_21.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_21.TabIndex = 21
    '
    'pnPag5Center
    '
    Me.pnPag5Center.AllowDrop = True
    Me.pnPag5Center.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag5Center.Appearance.Options.UseBackColor = True
    Me.pnPag5Center.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag5Center.Controls.Add(Me.ckTb_modsupext_20)
    Me.pnPag5Center.Controls.Add(Me.ckTb_modsupext_19)
    Me.pnPag5Center.Controls.Add(Me.ckTb_modsupext_18)
    Me.pnPag5Center.Controls.Add(Me.ckTb_modsupext_17)
    Me.pnPag5Center.Controls.Add(Me.ckTb_modsupext_16)
    Me.pnPag5Center.Controls.Add(Me.ckTb_modsupext_15)
    Me.pnPag5Center.Controls.Add(Me.ckTb_modsupext_14)
    Me.pnPag5Center.Controls.Add(Me.ckTb_modsupext_13)
    Me.pnPag5Center.Controls.Add(Me.ckTb_modsupext_12)
    Me.pnPag5Center.Controls.Add(Me.ckTb_modsupext_11)
    Me.pnPag5Center.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag5Center.Location = New System.Drawing.Point(206, 3)
    Me.pnPag5Center.Name = "pnPag5Center"
    Me.pnPag5Center.NTSActiveTrasparency = True
    Me.pnPag5Center.Size = New System.Drawing.Size(197, 264)
    Me.pnPag5Center.TabIndex = 22
    Me.pnPag5Center.Text = "NtsPanel1"
    '
    'ckTb_modsupext_20
    '
    Me.ckTb_modsupext_20.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_20.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_modsupext_20.Name = "ckTb_modsupext_20"
    Me.ckTb_modsupext_20.NTSCheckValue = "S"
    Me.ckTb_modsupext_20.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_20.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_20.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_20.Properties.AutoHeight = False
    Me.ckTb_modsupext_20.Properties.Caption = "......................................"
    Me.ckTb_modsupext_20.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_20.TabIndex = 30
    '
    'ckTb_modsupext_19
    '
    Me.ckTb_modsupext_19.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_19.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_modsupext_19.Name = "ckTb_modsupext_19"
    Me.ckTb_modsupext_19.NTSCheckValue = "S"
    Me.ckTb_modsupext_19.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_19.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_19.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_19.Properties.AutoHeight = False
    Me.ckTb_modsupext_19.Properties.Caption = "......................................"
    Me.ckTb_modsupext_19.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_19.TabIndex = 29
    '
    'ckTb_modsupext_18
    '
    Me.ckTb_modsupext_18.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_18.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_modsupext_18.Name = "ckTb_modsupext_18"
    Me.ckTb_modsupext_18.NTSCheckValue = "S"
    Me.ckTb_modsupext_18.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_18.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_18.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_18.Properties.AutoHeight = False
    Me.ckTb_modsupext_18.Properties.Caption = "......................................"
    Me.ckTb_modsupext_18.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_18.TabIndex = 28
    '
    'ckTb_modsupext_17
    '
    Me.ckTb_modsupext_17.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_17.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_modsupext_17.Name = "ckTb_modsupext_17"
    Me.ckTb_modsupext_17.NTSCheckValue = "S"
    Me.ckTb_modsupext_17.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_17.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_17.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_17.Properties.AutoHeight = False
    Me.ckTb_modsupext_17.Properties.Caption = "......................................"
    Me.ckTb_modsupext_17.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_17.TabIndex = 27
    '
    'ckTb_modsupext_16
    '
    Me.ckTb_modsupext_16.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_16.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_modsupext_16.Name = "ckTb_modsupext_16"
    Me.ckTb_modsupext_16.NTSCheckValue = "S"
    Me.ckTb_modsupext_16.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_16.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_16.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_16.Properties.AutoHeight = False
    Me.ckTb_modsupext_16.Properties.Caption = "......................................"
    Me.ckTb_modsupext_16.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_16.TabIndex = 26
    '
    'ckTb_modsupext_15
    '
    Me.ckTb_modsupext_15.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_15.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_modsupext_15.Name = "ckTb_modsupext_15"
    Me.ckTb_modsupext_15.NTSCheckValue = "S"
    Me.ckTb_modsupext_15.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_15.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_15.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_15.Properties.AutoHeight = False
    Me.ckTb_modsupext_15.Properties.Caption = "......................................"
    Me.ckTb_modsupext_15.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_15.TabIndex = 25
    '
    'ckTb_modsupext_14
    '
    Me.ckTb_modsupext_14.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_14.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_modsupext_14.Name = "ckTb_modsupext_14"
    Me.ckTb_modsupext_14.NTSCheckValue = "S"
    Me.ckTb_modsupext_14.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_14.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_14.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_14.Properties.AutoHeight = False
    Me.ckTb_modsupext_14.Properties.Caption = "......................................"
    Me.ckTb_modsupext_14.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_14.TabIndex = 24
    '
    'ckTb_modsupext_13
    '
    Me.ckTb_modsupext_13.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_13.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_modsupext_13.Name = "ckTb_modsupext_13"
    Me.ckTb_modsupext_13.NTSCheckValue = "S"
    Me.ckTb_modsupext_13.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_13.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_13.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_13.Properties.AutoHeight = False
    Me.ckTb_modsupext_13.Properties.Caption = "......................................"
    Me.ckTb_modsupext_13.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_13.TabIndex = 23
    '
    'ckTb_modsupext_12
    '
    Me.ckTb_modsupext_12.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_12.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_modsupext_12.Name = "ckTb_modsupext_12"
    Me.ckTb_modsupext_12.NTSCheckValue = "S"
    Me.ckTb_modsupext_12.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_12.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_12.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_12.Properties.AutoHeight = False
    Me.ckTb_modsupext_12.Properties.Caption = "......................................"
    Me.ckTb_modsupext_12.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_12.TabIndex = 22
    '
    'ckTb_modsupext_11
    '
    Me.ckTb_modsupext_11.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_11.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_modsupext_11.Name = "ckTb_modsupext_11"
    Me.ckTb_modsupext_11.NTSCheckValue = "S"
    Me.ckTb_modsupext_11.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_11.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_11.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_11.Properties.AutoHeight = False
    Me.ckTb_modsupext_11.Properties.Caption = "......................................"
    Me.ckTb_modsupext_11.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_11.TabIndex = 21
    '
    'pnPag5Left
    '
    Me.pnPag5Left.AllowDrop = True
    Me.pnPag5Left.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag5Left.Appearance.Options.UseBackColor = True
    Me.pnPag5Left.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag5Left.Controls.Add(Me.ckTb_modsupext_10)
    Me.pnPag5Left.Controls.Add(Me.ckTb_modsupext_9)
    Me.pnPag5Left.Controls.Add(Me.ckTb_modsupext_8)
    Me.pnPag5Left.Controls.Add(Me.ckTb_modsupext_7)
    Me.pnPag5Left.Controls.Add(Me.ckTb_modsupext_6)
    Me.pnPag5Left.Controls.Add(Me.ckTb_modsupext_5)
    Me.pnPag5Left.Controls.Add(Me.ckTb_modsupext_4)
    Me.pnPag5Left.Controls.Add(Me.ckTb_modsupext_3)
    Me.pnPag5Left.Controls.Add(Me.ckTb_modsupext_2)
    Me.pnPag5Left.Controls.Add(Me.ckTb_modsupext_1)
    Me.pnPag5Left.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag5Left.Location = New System.Drawing.Point(3, 3)
    Me.pnPag5Left.Name = "pnPag5Left"
    Me.pnPag5Left.NTSActiveTrasparency = True
    Me.pnPag5Left.Size = New System.Drawing.Size(197, 264)
    Me.pnPag5Left.TabIndex = 21
    Me.pnPag5Left.Text = "NtsPanel1"
    '
    'ckTb_modsupext_10
    '
    Me.ckTb_modsupext_10.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_10.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_modsupext_10.Name = "ckTb_modsupext_10"
    Me.ckTb_modsupext_10.NTSCheckValue = "S"
    Me.ckTb_modsupext_10.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_10.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_10.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_10.Properties.AutoHeight = False
    Me.ckTb_modsupext_10.Properties.Caption = "......................................"
    Me.ckTb_modsupext_10.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_10.TabIndex = 30
    '
    'ckTb_modsupext_9
    '
    Me.ckTb_modsupext_9.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_9.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_modsupext_9.Name = "ckTb_modsupext_9"
    Me.ckTb_modsupext_9.NTSCheckValue = "S"
    Me.ckTb_modsupext_9.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_9.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_9.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_9.Properties.AutoHeight = False
    Me.ckTb_modsupext_9.Properties.Caption = "......................................"
    Me.ckTb_modsupext_9.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_9.TabIndex = 29
    '
    'ckTb_modsupext_8
    '
    Me.ckTb_modsupext_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_8.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_modsupext_8.Name = "ckTb_modsupext_8"
    Me.ckTb_modsupext_8.NTSCheckValue = "S"
    Me.ckTb_modsupext_8.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_8.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_8.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_8.Properties.AutoHeight = False
    Me.ckTb_modsupext_8.Properties.Caption = "......................................"
    Me.ckTb_modsupext_8.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_8.TabIndex = 28
    '
    'ckTb_modsupext_7
    '
    Me.ckTb_modsupext_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_7.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_modsupext_7.Name = "ckTb_modsupext_7"
    Me.ckTb_modsupext_7.NTSCheckValue = "S"
    Me.ckTb_modsupext_7.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_7.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_7.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_7.Properties.AutoHeight = False
    Me.ckTb_modsupext_7.Properties.Caption = "......................................"
    Me.ckTb_modsupext_7.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_7.TabIndex = 27
    '
    'ckTb_modsupext_6
    '
    Me.ckTb_modsupext_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_6.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_modsupext_6.Name = "ckTb_modsupext_6"
    Me.ckTb_modsupext_6.NTSCheckValue = "S"
    Me.ckTb_modsupext_6.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_6.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_6.Properties.AutoHeight = False
    Me.ckTb_modsupext_6.Properties.Caption = "......................................"
    Me.ckTb_modsupext_6.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_6.TabIndex = 26
    '
    'ckTb_modsupext_5
    '
    Me.ckTb_modsupext_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_5.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_modsupext_5.Name = "ckTb_modsupext_5"
    Me.ckTb_modsupext_5.NTSCheckValue = "S"
    Me.ckTb_modsupext_5.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_5.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_5.Properties.AutoHeight = False
    Me.ckTb_modsupext_5.Properties.Caption = "Analisi di bilancio"
    Me.ckTb_modsupext_5.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_5.TabIndex = 25
    '
    'ckTb_modsupext_4
    '
    Me.ckTb_modsupext_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_4.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_modsupext_4.Name = "ckTb_modsupext_4"
    Me.ckTb_modsupext_4.NTSCheckValue = "S"
    Me.ckTb_modsupext_4.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_4.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_4.Properties.AutoHeight = False
    Me.ckTb_modsupext_4.Properties.Caption = "......................................"
    Me.ckTb_modsupext_4.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_4.TabIndex = 24
    '
    'ckTb_modsupext_3
    '
    Me.ckTb_modsupext_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_3.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_modsupext_3.Name = "ckTb_modsupext_3"
    Me.ckTb_modsupext_3.NTSCheckValue = "S"
    Me.ckTb_modsupext_3.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_3.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_3.Properties.AutoHeight = False
    Me.ckTb_modsupext_3.Properties.Caption = "......................................"
    Me.ckTb_modsupext_3.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_3.TabIndex = 23
    '
    'ckTb_modsupext_2
    '
    Me.ckTb_modsupext_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_2.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_modsupext_2.Name = "ckTb_modsupext_2"
    Me.ckTb_modsupext_2.NTSCheckValue = "S"
    Me.ckTb_modsupext_2.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_2.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_2.Properties.AutoHeight = False
    Me.ckTb_modsupext_2.Properties.Caption = "......................................"
    Me.ckTb_modsupext_2.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_2.TabIndex = 22
    '
    'ckTb_modsupext_1
    '
    Me.ckTb_modsupext_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modsupext_1.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_modsupext_1.Name = "ckTb_modsupext_1"
    Me.ckTb_modsupext_1.NTSCheckValue = "S"
    Me.ckTb_modsupext_1.NTSUnCheckValue = "N"
    Me.ckTb_modsupext_1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modsupext_1.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modsupext_1.Properties.AutoHeight = False
    Me.ckTb_modsupext_1.Properties.Caption = "Gest. Mercati Ortofrutta"
    Me.ckTb_modsupext_1.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modsupext_1.TabIndex = 21
    '
    'NtsTabPage6
    '
    Me.NtsTabPage6.AllowDrop = True
    Me.NtsTabPage6.Controls.Add(Me.pnPag6)
    Me.NtsTabPage6.Enable = True
    Me.NtsTabPage6.Name = "NtsTabPage6"
    Me.NtsTabPage6.Size = New System.Drawing.Size(609, 267)
    Me.NtsTabPage6.Text = "Mod Partner"
    Me.NtsTabPage6.Tooltip = "Moduli Partner"
    '
    'pnPag6
    '
    Me.pnPag6.AllowDrop = True
    Me.pnPag6.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag6.Appearance.Options.UseBackColor = True
    Me.pnPag6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag6.Controls.Add(Me.pnPag6Right)
    Me.pnPag6.Controls.Add(Me.pnPag6Center)
    Me.pnPag6.Controls.Add(Me.pnPag6Left)
    Me.pnPag6.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag6.Location = New System.Drawing.Point(0, 0)
    Me.pnPag6.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag6.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag6.Name = "pnPag6"
    Me.pnPag6.NTSActiveTrasparency = True
    Me.pnPag6.Size = New System.Drawing.Size(609, 273)
    Me.pnPag6.TabIndex = 4
    Me.pnPag6.Text = "NtsPanel1"
    '
    'pnPag6Right
    '
    Me.pnPag6Right.AllowDrop = True
    Me.pnPag6Right.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag6Right.Appearance.Options.UseBackColor = True
    Me.pnPag6Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag6Right.Controls.Add(Me.ckTb_modptn_30)
    Me.pnPag6Right.Controls.Add(Me.ckTb_modptn_29)
    Me.pnPag6Right.Controls.Add(Me.ckTb_modptn_28)
    Me.pnPag6Right.Controls.Add(Me.ckTb_modptn_27)
    Me.pnPag6Right.Controls.Add(Me.ckTb_modptn_26)
    Me.pnPag6Right.Controls.Add(Me.ckTb_modptn_25)
    Me.pnPag6Right.Controls.Add(Me.ckTb_modptn_24)
    Me.pnPag6Right.Controls.Add(Me.ckTb_modptn_23)
    Me.pnPag6Right.Controls.Add(Me.ckTb_modptn_22)
    Me.pnPag6Right.Controls.Add(Me.ckTb_modptn_21)
    Me.pnPag6Right.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag6Right.Location = New System.Drawing.Point(409, 3)
    Me.pnPag6Right.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag6Right.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag6Right.Name = "pnPag6Right"
    Me.pnPag6Right.NTSActiveTrasparency = True
    Me.pnPag6Right.Size = New System.Drawing.Size(197, 264)
    Me.pnPag6Right.TabIndex = 23
    Me.pnPag6Right.Text = "NtsPanel1"
    '
    'ckTb_modptn_30
    '
    Me.ckTb_modptn_30.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_30.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_modptn_30.Name = "ckTb_modptn_30"
    Me.ckTb_modptn_30.NTSCheckValue = "S"
    Me.ckTb_modptn_30.NTSUnCheckValue = "N"
    Me.ckTb_modptn_30.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_30.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_30.Properties.AutoHeight = False
    Me.ckTb_modptn_30.Properties.Caption = "......................................"
    Me.ckTb_modptn_30.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_30.TabIndex = 30
    '
    'ckTb_modptn_29
    '
    Me.ckTb_modptn_29.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_29.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_modptn_29.Name = "ckTb_modptn_29"
    Me.ckTb_modptn_29.NTSCheckValue = "S"
    Me.ckTb_modptn_29.NTSUnCheckValue = "N"
    Me.ckTb_modptn_29.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_29.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_29.Properties.AutoHeight = False
    Me.ckTb_modptn_29.Properties.Caption = "......................................"
    Me.ckTb_modptn_29.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_29.TabIndex = 29
    '
    'ckTb_modptn_28
    '
    Me.ckTb_modptn_28.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_28.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_modptn_28.Name = "ckTb_modptn_28"
    Me.ckTb_modptn_28.NTSCheckValue = "S"
    Me.ckTb_modptn_28.NTSUnCheckValue = "N"
    Me.ckTb_modptn_28.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_28.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_28.Properties.AutoHeight = False
    Me.ckTb_modptn_28.Properties.Caption = "......................................"
    Me.ckTb_modptn_28.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_28.TabIndex = 28
    '
    'ckTb_modptn_27
    '
    Me.ckTb_modptn_27.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_27.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_modptn_27.Name = "ckTb_modptn_27"
    Me.ckTb_modptn_27.NTSCheckValue = "S"
    Me.ckTb_modptn_27.NTSUnCheckValue = "N"
    Me.ckTb_modptn_27.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_27.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_27.Properties.AutoHeight = False
    Me.ckTb_modptn_27.Properties.Caption = "......................................"
    Me.ckTb_modptn_27.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_27.TabIndex = 27
    '
    'ckTb_modptn_26
    '
    Me.ckTb_modptn_26.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_26.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_modptn_26.Name = "ckTb_modptn_26"
    Me.ckTb_modptn_26.NTSCheckValue = "S"
    Me.ckTb_modptn_26.NTSUnCheckValue = "N"
    Me.ckTb_modptn_26.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_26.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_26.Properties.AutoHeight = False
    Me.ckTb_modptn_26.Properties.Caption = "......................................"
    Me.ckTb_modptn_26.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_26.TabIndex = 26
    '
    'ckTb_modptn_25
    '
    Me.ckTb_modptn_25.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_25.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_modptn_25.Name = "ckTb_modptn_25"
    Me.ckTb_modptn_25.NTSCheckValue = "S"
    Me.ckTb_modptn_25.NTSUnCheckValue = "N"
    Me.ckTb_modptn_25.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_25.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_25.Properties.AutoHeight = False
    Me.ckTb_modptn_25.Properties.Caption = "......................................"
    Me.ckTb_modptn_25.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_25.TabIndex = 25
    '
    'ckTb_modptn_24
    '
    Me.ckTb_modptn_24.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_24.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_modptn_24.Name = "ckTb_modptn_24"
    Me.ckTb_modptn_24.NTSCheckValue = "S"
    Me.ckTb_modptn_24.NTSUnCheckValue = "N"
    Me.ckTb_modptn_24.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_24.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_24.Properties.AutoHeight = False
    Me.ckTb_modptn_24.Properties.Caption = "......................................"
    Me.ckTb_modptn_24.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_24.TabIndex = 24
    '
    'ckTb_modptn_23
    '
    Me.ckTb_modptn_23.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_23.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_modptn_23.Name = "ckTb_modptn_23"
    Me.ckTb_modptn_23.NTSCheckValue = "S"
    Me.ckTb_modptn_23.NTSUnCheckValue = "N"
    Me.ckTb_modptn_23.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_23.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_23.Properties.AutoHeight = False
    Me.ckTb_modptn_23.Properties.Caption = "......................................"
    Me.ckTb_modptn_23.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_23.TabIndex = 23
    '
    'ckTb_modptn_22
    '
    Me.ckTb_modptn_22.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_22.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_modptn_22.Name = "ckTb_modptn_22"
    Me.ckTb_modptn_22.NTSCheckValue = "S"
    Me.ckTb_modptn_22.NTSUnCheckValue = "N"
    Me.ckTb_modptn_22.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_22.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_22.Properties.AutoHeight = False
    Me.ckTb_modptn_22.Properties.Caption = "......................................"
    Me.ckTb_modptn_22.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_22.TabIndex = 22
    '
    'ckTb_modptn_21
    '
    Me.ckTb_modptn_21.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_21.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_modptn_21.Name = "ckTb_modptn_21"
    Me.ckTb_modptn_21.NTSCheckValue = "S"
    Me.ckTb_modptn_21.NTSUnCheckValue = "N"
    Me.ckTb_modptn_21.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_21.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_21.Properties.AutoHeight = False
    Me.ckTb_modptn_21.Properties.Caption = "......................................"
    Me.ckTb_modptn_21.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_21.TabIndex = 21
    '
    'pnPag6Center
    '
    Me.pnPag6Center.AllowDrop = True
    Me.pnPag6Center.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag6Center.Appearance.Options.UseBackColor = True
    Me.pnPag6Center.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag6Center.Controls.Add(Me.ckTb_modptn_20)
    Me.pnPag6Center.Controls.Add(Me.ckTb_modptn_19)
    Me.pnPag6Center.Controls.Add(Me.ckTb_modptn_18)
    Me.pnPag6Center.Controls.Add(Me.ckTb_modptn_17)
    Me.pnPag6Center.Controls.Add(Me.ckTb_modptn_16)
    Me.pnPag6Center.Controls.Add(Me.ckTb_modptn_15)
    Me.pnPag6Center.Controls.Add(Me.ckTb_modptn_14)
    Me.pnPag6Center.Controls.Add(Me.ckTb_modptn_13)
    Me.pnPag6Center.Controls.Add(Me.ckTb_modptn_12)
    Me.pnPag6Center.Controls.Add(Me.ckTb_modptn_11)
    Me.pnPag6Center.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag6Center.Location = New System.Drawing.Point(206, 3)
    Me.pnPag6Center.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag6Center.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag6Center.Name = "pnPag6Center"
    Me.pnPag6Center.NTSActiveTrasparency = True
    Me.pnPag6Center.Size = New System.Drawing.Size(197, 264)
    Me.pnPag6Center.TabIndex = 22
    Me.pnPag6Center.Text = "NtsPanel1"
    '
    'ckTb_modptn_20
    '
    Me.ckTb_modptn_20.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_20.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_modptn_20.Name = "ckTb_modptn_20"
    Me.ckTb_modptn_20.NTSCheckValue = "S"
    Me.ckTb_modptn_20.NTSUnCheckValue = "N"
    Me.ckTb_modptn_20.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_20.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_20.Properties.AutoHeight = False
    Me.ckTb_modptn_20.Properties.Caption = "......................................"
    Me.ckTb_modptn_20.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_20.TabIndex = 30
    '
    'ckTb_modptn_19
    '
    Me.ckTb_modptn_19.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_19.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_modptn_19.Name = "ckTb_modptn_19"
    Me.ckTb_modptn_19.NTSCheckValue = "S"
    Me.ckTb_modptn_19.NTSUnCheckValue = "N"
    Me.ckTb_modptn_19.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_19.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_19.Properties.AutoHeight = False
    Me.ckTb_modptn_19.Properties.Caption = "......................................"
    Me.ckTb_modptn_19.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_19.TabIndex = 29
    '
    'ckTb_modptn_18
    '
    Me.ckTb_modptn_18.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_18.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_modptn_18.Name = "ckTb_modptn_18"
    Me.ckTb_modptn_18.NTSCheckValue = "S"
    Me.ckTb_modptn_18.NTSUnCheckValue = "N"
    Me.ckTb_modptn_18.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_18.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_18.Properties.AutoHeight = False
    Me.ckTb_modptn_18.Properties.Caption = "......................................"
    Me.ckTb_modptn_18.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_18.TabIndex = 28
    '
    'ckTb_modptn_17
    '
    Me.ckTb_modptn_17.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_17.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_modptn_17.Name = "ckTb_modptn_17"
    Me.ckTb_modptn_17.NTSCheckValue = "S"
    Me.ckTb_modptn_17.NTSUnCheckValue = "N"
    Me.ckTb_modptn_17.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_17.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_17.Properties.AutoHeight = False
    Me.ckTb_modptn_17.Properties.Caption = "......................................"
    Me.ckTb_modptn_17.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_17.TabIndex = 27
    '
    'ckTb_modptn_16
    '
    Me.ckTb_modptn_16.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_16.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_modptn_16.Name = "ckTb_modptn_16"
    Me.ckTb_modptn_16.NTSCheckValue = "S"
    Me.ckTb_modptn_16.NTSUnCheckValue = "N"
    Me.ckTb_modptn_16.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_16.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_16.Properties.AutoHeight = False
    Me.ckTb_modptn_16.Properties.Caption = "......................................"
    Me.ckTb_modptn_16.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_16.TabIndex = 26
    '
    'ckTb_modptn_15
    '
    Me.ckTb_modptn_15.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_15.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_modptn_15.Name = "ckTb_modptn_15"
    Me.ckTb_modptn_15.NTSCheckValue = "S"
    Me.ckTb_modptn_15.NTSUnCheckValue = "N"
    Me.ckTb_modptn_15.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_15.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_15.Properties.AutoHeight = False
    Me.ckTb_modptn_15.Properties.Caption = "......................................"
    Me.ckTb_modptn_15.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_15.TabIndex = 25
    '
    'ckTb_modptn_14
    '
    Me.ckTb_modptn_14.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_14.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_modptn_14.Name = "ckTb_modptn_14"
    Me.ckTb_modptn_14.NTSCheckValue = "S"
    Me.ckTb_modptn_14.NTSUnCheckValue = "N"
    Me.ckTb_modptn_14.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_14.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_14.Properties.AutoHeight = False
    Me.ckTb_modptn_14.Properties.Caption = "......................................"
    Me.ckTb_modptn_14.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_14.TabIndex = 24
    '
    'ckTb_modptn_13
    '
    Me.ckTb_modptn_13.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_13.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_modptn_13.Name = "ckTb_modptn_13"
    Me.ckTb_modptn_13.NTSCheckValue = "S"
    Me.ckTb_modptn_13.NTSUnCheckValue = "N"
    Me.ckTb_modptn_13.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_13.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_13.Properties.AutoHeight = False
    Me.ckTb_modptn_13.Properties.Caption = "......................................"
    Me.ckTb_modptn_13.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_13.TabIndex = 23
    '
    'ckTb_modptn_12
    '
    Me.ckTb_modptn_12.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_12.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_modptn_12.Name = "ckTb_modptn_12"
    Me.ckTb_modptn_12.NTSCheckValue = "S"
    Me.ckTb_modptn_12.NTSUnCheckValue = "N"
    Me.ckTb_modptn_12.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_12.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_12.Properties.AutoHeight = False
    Me.ckTb_modptn_12.Properties.Caption = "......................................"
    Me.ckTb_modptn_12.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_12.TabIndex = 22
    '
    'ckTb_modptn_11
    '
    Me.ckTb_modptn_11.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_11.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_modptn_11.Name = "ckTb_modptn_11"
    Me.ckTb_modptn_11.NTSCheckValue = "S"
    Me.ckTb_modptn_11.NTSUnCheckValue = "N"
    Me.ckTb_modptn_11.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_11.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_11.Properties.AutoHeight = False
    Me.ckTb_modptn_11.Properties.Caption = "......................................"
    Me.ckTb_modptn_11.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_11.TabIndex = 21
    '
    'pnPag6Left
    '
    Me.pnPag6Left.AllowDrop = True
    Me.pnPag6Left.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag6Left.Appearance.Options.UseBackColor = True
    Me.pnPag6Left.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag6Left.Controls.Add(Me.ckTb_modptn_10)
    Me.pnPag6Left.Controls.Add(Me.ckTb_modptn_9)
    Me.pnPag6Left.Controls.Add(Me.ckTb_modptn_8)
    Me.pnPag6Left.Controls.Add(Me.ckTb_modptn_7)
    Me.pnPag6Left.Controls.Add(Me.ckTb_modptn_6)
    Me.pnPag6Left.Controls.Add(Me.ckTb_modptn_5)
    Me.pnPag6Left.Controls.Add(Me.ckTb_modptn_4)
    Me.pnPag6Left.Controls.Add(Me.ckTb_modptn_3)
    Me.pnPag6Left.Controls.Add(Me.ckTb_modptn_2)
    Me.pnPag6Left.Controls.Add(Me.ckTb_modptn_1)
    Me.pnPag6Left.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag6Left.Location = New System.Drawing.Point(3, 3)
    Me.pnPag6Left.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag6Left.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag6Left.Name = "pnPag6Left"
    Me.pnPag6Left.NTSActiveTrasparency = True
    Me.pnPag6Left.Size = New System.Drawing.Size(197, 264)
    Me.pnPag6Left.TabIndex = 21
    Me.pnPag6Left.Text = "NtsPanel1"
    '
    'ckTb_modptn_10
    '
    Me.ckTb_modptn_10.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_10.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_modptn_10.Name = "ckTb_modptn_10"
    Me.ckTb_modptn_10.NTSCheckValue = "S"
    Me.ckTb_modptn_10.NTSUnCheckValue = "N"
    Me.ckTb_modptn_10.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_10.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_10.Properties.AutoHeight = False
    Me.ckTb_modptn_10.Properties.Caption = "......................................"
    Me.ckTb_modptn_10.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_10.TabIndex = 30
    '
    'ckTb_modptn_9
    '
    Me.ckTb_modptn_9.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_9.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_modptn_9.Name = "ckTb_modptn_9"
    Me.ckTb_modptn_9.NTSCheckValue = "S"
    Me.ckTb_modptn_9.NTSUnCheckValue = "N"
    Me.ckTb_modptn_9.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_9.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_9.Properties.AutoHeight = False
    Me.ckTb_modptn_9.Properties.Caption = "......................................"
    Me.ckTb_modptn_9.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_9.TabIndex = 29
    '
    'ckTb_modptn_8
    '
    Me.ckTb_modptn_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_8.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_modptn_8.Name = "ckTb_modptn_8"
    Me.ckTb_modptn_8.NTSCheckValue = "S"
    Me.ckTb_modptn_8.NTSUnCheckValue = "N"
    Me.ckTb_modptn_8.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_8.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_8.Properties.AutoHeight = False
    Me.ckTb_modptn_8.Properties.Caption = "......................................"
    Me.ckTb_modptn_8.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_8.TabIndex = 28
    '
    'ckTb_modptn_7
    '
    Me.ckTb_modptn_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_7.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_modptn_7.Name = "ckTb_modptn_7"
    Me.ckTb_modptn_7.NTSCheckValue = "S"
    Me.ckTb_modptn_7.NTSUnCheckValue = "N"
    Me.ckTb_modptn_7.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_7.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_7.Properties.AutoHeight = False
    Me.ckTb_modptn_7.Properties.Caption = "......................................"
    Me.ckTb_modptn_7.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_7.TabIndex = 27
    '
    'ckTb_modptn_6
    '
    Me.ckTb_modptn_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_6.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_modptn_6.Name = "ckTb_modptn_6"
    Me.ckTb_modptn_6.NTSCheckValue = "S"
    Me.ckTb_modptn_6.NTSUnCheckValue = "N"
    Me.ckTb_modptn_6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_6.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_6.Properties.AutoHeight = False
    Me.ckTb_modptn_6.Properties.Caption = "......................................"
    Me.ckTb_modptn_6.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_6.TabIndex = 26
    '
    'ckTb_modptn_5
    '
    Me.ckTb_modptn_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_5.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_modptn_5.Name = "ckTb_modptn_5"
    Me.ckTb_modptn_5.NTSCheckValue = "S"
    Me.ckTb_modptn_5.NTSUnCheckValue = "N"
    Me.ckTb_modptn_5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_5.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_5.Properties.AutoHeight = False
    Me.ckTb_modptn_5.Properties.Caption = "......................................"
    Me.ckTb_modptn_5.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_5.TabIndex = 25
    '
    'ckTb_modptn_4
    '
    Me.ckTb_modptn_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_4.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_modptn_4.Name = "ckTb_modptn_4"
    Me.ckTb_modptn_4.NTSCheckValue = "S"
    Me.ckTb_modptn_4.NTSUnCheckValue = "N"
    Me.ckTb_modptn_4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_4.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_4.Properties.AutoHeight = False
    Me.ckTb_modptn_4.Properties.Caption = "......................................"
    Me.ckTb_modptn_4.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_4.TabIndex = 24
    '
    'ckTb_modptn_3
    '
    Me.ckTb_modptn_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_3.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_modptn_3.Name = "ckTb_modptn_3"
    Me.ckTb_modptn_3.NTSCheckValue = "S"
    Me.ckTb_modptn_3.NTSUnCheckValue = "N"
    Me.ckTb_modptn_3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_3.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_3.Properties.AutoHeight = False
    Me.ckTb_modptn_3.Properties.Caption = "......................................"
    Me.ckTb_modptn_3.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_3.TabIndex = 23
    '
    'ckTb_modptn_2
    '
    Me.ckTb_modptn_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_2.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_modptn_2.Name = "ckTb_modptn_2"
    Me.ckTb_modptn_2.NTSCheckValue = "S"
    Me.ckTb_modptn_2.NTSUnCheckValue = "N"
    Me.ckTb_modptn_2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_2.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_2.Properties.AutoHeight = False
    Me.ckTb_modptn_2.Properties.Caption = "......................................"
    Me.ckTb_modptn_2.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_2.TabIndex = 22
    '
    'ckTb_modptn_1
    '
    Me.ckTb_modptn_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptn_1.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_modptn_1.Name = "ckTb_modptn_1"
    Me.ckTb_modptn_1.NTSCheckValue = "S"
    Me.ckTb_modptn_1.NTSUnCheckValue = "N"
    Me.ckTb_modptn_1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptn_1.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptn_1.Properties.AutoHeight = False
    Me.ckTb_modptn_1.Properties.Caption = "......................................"
    Me.ckTb_modptn_1.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptn_1.TabIndex = 21
    '
    'NtsTabPage7
    '
    Me.NtsTabPage7.AllowDrop = True
    Me.NtsTabPage7.Controls.Add(Me.pnPag7)
    Me.NtsTabPage7.Enable = True
    Me.NtsTabPage7.Name = "NtsTabPage7"
    Me.NtsTabPage7.Size = New System.Drawing.Size(609, 267)
    Me.NtsTabPage7.Text = "Mod Partner Ext"
    Me.NtsTabPage7.Tooltip = "Moduli Partner Estesi"
    '
    'pnPag7
    '
    Me.pnPag7.AllowDrop = True
    Me.pnPag7.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag7.Appearance.Options.UseBackColor = True
    Me.pnPag7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag7.Controls.Add(Me.pnPag7Right)
    Me.pnPag7.Controls.Add(Me.pnPag7Center)
    Me.pnPag7.Controls.Add(Me.pnPag7Left)
    Me.pnPag7.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag7.Location = New System.Drawing.Point(0, 0)
    Me.pnPag7.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag7.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag7.Name = "pnPag7"
    Me.pnPag7.NTSActiveTrasparency = True
    Me.pnPag7.Size = New System.Drawing.Size(609, 273)
    Me.pnPag7.TabIndex = 5
    Me.pnPag7.Text = "NtsPanel1"
    '
    'pnPag7Right
    '
    Me.pnPag7Right.AllowDrop = True
    Me.pnPag7Right.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag7Right.Appearance.Options.UseBackColor = True
    Me.pnPag7Right.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag7Right.Controls.Add(Me.ckTb_modptnext_30)
    Me.pnPag7Right.Controls.Add(Me.ckTb_modptnext_29)
    Me.pnPag7Right.Controls.Add(Me.ckTb_modptnext_28)
    Me.pnPag7Right.Controls.Add(Me.ckTb_modptnext_27)
    Me.pnPag7Right.Controls.Add(Me.ckTb_modptnext_26)
    Me.pnPag7Right.Controls.Add(Me.ckTb_modptnext_25)
    Me.pnPag7Right.Controls.Add(Me.ckTb_modptnext_24)
    Me.pnPag7Right.Controls.Add(Me.ckTb_modptnext_23)
    Me.pnPag7Right.Controls.Add(Me.ckTb_modptnext_22)
    Me.pnPag7Right.Controls.Add(Me.ckTb_modptnext_21)
    Me.pnPag7Right.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag7Right.Location = New System.Drawing.Point(409, 3)
    Me.pnPag7Right.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag7Right.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag7Right.Name = "pnPag7Right"
    Me.pnPag7Right.NTSActiveTrasparency = True
    Me.pnPag7Right.Size = New System.Drawing.Size(197, 264)
    Me.pnPag7Right.TabIndex = 23
    Me.pnPag7Right.Text = "NtsPanel1"
    '
    'ckTb_modptnext_30
    '
    Me.ckTb_modptnext_30.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_30.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_modptnext_30.Name = "ckTb_modptnext_30"
    Me.ckTb_modptnext_30.NTSCheckValue = "S"
    Me.ckTb_modptnext_30.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_30.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_30.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_30.Properties.AutoHeight = False
    Me.ckTb_modptnext_30.Properties.Caption = "......................................"
    Me.ckTb_modptnext_30.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_30.TabIndex = 30
    '
    'ckTb_modptnext_29
    '
    Me.ckTb_modptnext_29.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_29.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_modptnext_29.Name = "ckTb_modptnext_29"
    Me.ckTb_modptnext_29.NTSCheckValue = "S"
    Me.ckTb_modptnext_29.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_29.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_29.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_29.Properties.AutoHeight = False
    Me.ckTb_modptnext_29.Properties.Caption = "......................................"
    Me.ckTb_modptnext_29.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_29.TabIndex = 29
    '
    'ckTb_modptnext_28
    '
    Me.ckTb_modptnext_28.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_28.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_modptnext_28.Name = "ckTb_modptnext_28"
    Me.ckTb_modptnext_28.NTSCheckValue = "S"
    Me.ckTb_modptnext_28.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_28.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_28.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_28.Properties.AutoHeight = False
    Me.ckTb_modptnext_28.Properties.Caption = "......................................"
    Me.ckTb_modptnext_28.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_28.TabIndex = 28
    '
    'ckTb_modptnext_27
    '
    Me.ckTb_modptnext_27.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_27.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_modptnext_27.Name = "ckTb_modptnext_27"
    Me.ckTb_modptnext_27.NTSCheckValue = "S"
    Me.ckTb_modptnext_27.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_27.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_27.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_27.Properties.AutoHeight = False
    Me.ckTb_modptnext_27.Properties.Caption = "......................................"
    Me.ckTb_modptnext_27.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_27.TabIndex = 27
    '
    'ckTb_modptnext_26
    '
    Me.ckTb_modptnext_26.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_26.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_modptnext_26.Name = "ckTb_modptnext_26"
    Me.ckTb_modptnext_26.NTSCheckValue = "S"
    Me.ckTb_modptnext_26.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_26.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_26.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_26.Properties.AutoHeight = False
    Me.ckTb_modptnext_26.Properties.Caption = "......................................"
    Me.ckTb_modptnext_26.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_26.TabIndex = 26
    '
    'ckTb_modptnext_25
    '
    Me.ckTb_modptnext_25.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_25.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_modptnext_25.Name = "ckTb_modptnext_25"
    Me.ckTb_modptnext_25.NTSCheckValue = "S"
    Me.ckTb_modptnext_25.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_25.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_25.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_25.Properties.AutoHeight = False
    Me.ckTb_modptnext_25.Properties.Caption = "......................................"
    Me.ckTb_modptnext_25.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_25.TabIndex = 25
    '
    'ckTb_modptnext_24
    '
    Me.ckTb_modptnext_24.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_24.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_modptnext_24.Name = "ckTb_modptnext_24"
    Me.ckTb_modptnext_24.NTSCheckValue = "S"
    Me.ckTb_modptnext_24.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_24.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_24.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_24.Properties.AutoHeight = False
    Me.ckTb_modptnext_24.Properties.Caption = "......................................"
    Me.ckTb_modptnext_24.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_24.TabIndex = 24
    '
    'ckTb_modptnext_23
    '
    Me.ckTb_modptnext_23.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_23.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_modptnext_23.Name = "ckTb_modptnext_23"
    Me.ckTb_modptnext_23.NTSCheckValue = "S"
    Me.ckTb_modptnext_23.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_23.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_23.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_23.Properties.AutoHeight = False
    Me.ckTb_modptnext_23.Properties.Caption = "......................................"
    Me.ckTb_modptnext_23.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_23.TabIndex = 23
    '
    'ckTb_modptnext_22
    '
    Me.ckTb_modptnext_22.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_22.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_modptnext_22.Name = "ckTb_modptnext_22"
    Me.ckTb_modptnext_22.NTSCheckValue = "S"
    Me.ckTb_modptnext_22.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_22.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_22.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_22.Properties.AutoHeight = False
    Me.ckTb_modptnext_22.Properties.Caption = "......................................"
    Me.ckTb_modptnext_22.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_22.TabIndex = 22
    '
    'ckTb_modptnext_21
    '
    Me.ckTb_modptnext_21.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_21.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_modptnext_21.Name = "ckTb_modptnext_21"
    Me.ckTb_modptnext_21.NTSCheckValue = "S"
    Me.ckTb_modptnext_21.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_21.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_21.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_21.Properties.AutoHeight = False
    Me.ckTb_modptnext_21.Properties.Caption = "......................................"
    Me.ckTb_modptnext_21.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_21.TabIndex = 21
    '
    'pnPag7Center
    '
    Me.pnPag7Center.AllowDrop = True
    Me.pnPag7Center.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag7Center.Appearance.Options.UseBackColor = True
    Me.pnPag7Center.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag7Center.Controls.Add(Me.ckTb_modptnext_20)
    Me.pnPag7Center.Controls.Add(Me.ckTb_modptnext_19)
    Me.pnPag7Center.Controls.Add(Me.ckTb_modptnext_18)
    Me.pnPag7Center.Controls.Add(Me.ckTb_modptnext_17)
    Me.pnPag7Center.Controls.Add(Me.ckTb_modptnext_16)
    Me.pnPag7Center.Controls.Add(Me.ckTb_modptnext_15)
    Me.pnPag7Center.Controls.Add(Me.ckTb_modptnext_14)
    Me.pnPag7Center.Controls.Add(Me.ckTb_modptnext_13)
    Me.pnPag7Center.Controls.Add(Me.ckTb_modptnext_12)
    Me.pnPag7Center.Controls.Add(Me.ckTb_modptnext_11)
    Me.pnPag7Center.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnPag7Center.Location = New System.Drawing.Point(206, 3)
    Me.pnPag7Center.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag7Center.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag7Center.Name = "pnPag7Center"
    Me.pnPag7Center.NTSActiveTrasparency = True
    Me.pnPag7Center.Size = New System.Drawing.Size(197, 264)
    Me.pnPag7Center.TabIndex = 22
    Me.pnPag7Center.Text = "NtsPanel1"
    '
    'ckTb_modptnext_20
    '
    Me.ckTb_modptnext_20.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_20.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_modptnext_20.Name = "ckTb_modptnext_20"
    Me.ckTb_modptnext_20.NTSCheckValue = "S"
    Me.ckTb_modptnext_20.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_20.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_20.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_20.Properties.AutoHeight = False
    Me.ckTb_modptnext_20.Properties.Caption = "......................................"
    Me.ckTb_modptnext_20.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_20.TabIndex = 30
    '
    'ckTb_modptnext_19
    '
    Me.ckTb_modptnext_19.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_19.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_modptnext_19.Name = "ckTb_modptnext_19"
    Me.ckTb_modptnext_19.NTSCheckValue = "S"
    Me.ckTb_modptnext_19.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_19.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_19.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_19.Properties.AutoHeight = False
    Me.ckTb_modptnext_19.Properties.Caption = "......................................"
    Me.ckTb_modptnext_19.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_19.TabIndex = 29
    '
    'ckTb_modptnext_18
    '
    Me.ckTb_modptnext_18.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_18.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_modptnext_18.Name = "ckTb_modptnext_18"
    Me.ckTb_modptnext_18.NTSCheckValue = "S"
    Me.ckTb_modptnext_18.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_18.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_18.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_18.Properties.AutoHeight = False
    Me.ckTb_modptnext_18.Properties.Caption = "......................................"
    Me.ckTb_modptnext_18.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_18.TabIndex = 28
    '
    'ckTb_modptnext_17
    '
    Me.ckTb_modptnext_17.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_17.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_modptnext_17.Name = "ckTb_modptnext_17"
    Me.ckTb_modptnext_17.NTSCheckValue = "S"
    Me.ckTb_modptnext_17.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_17.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_17.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_17.Properties.AutoHeight = False
    Me.ckTb_modptnext_17.Properties.Caption = "......................................"
    Me.ckTb_modptnext_17.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_17.TabIndex = 27
    '
    'ckTb_modptnext_16
    '
    Me.ckTb_modptnext_16.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_16.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_modptnext_16.Name = "ckTb_modptnext_16"
    Me.ckTb_modptnext_16.NTSCheckValue = "S"
    Me.ckTb_modptnext_16.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_16.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_16.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_16.Properties.AutoHeight = False
    Me.ckTb_modptnext_16.Properties.Caption = "......................................"
    Me.ckTb_modptnext_16.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_16.TabIndex = 26
    '
    'ckTb_modptnext_15
    '
    Me.ckTb_modptnext_15.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_15.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_modptnext_15.Name = "ckTb_modptnext_15"
    Me.ckTb_modptnext_15.NTSCheckValue = "S"
    Me.ckTb_modptnext_15.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_15.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_15.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_15.Properties.AutoHeight = False
    Me.ckTb_modptnext_15.Properties.Caption = "......................................"
    Me.ckTb_modptnext_15.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_15.TabIndex = 25
    '
    'ckTb_modptnext_14
    '
    Me.ckTb_modptnext_14.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_14.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_modptnext_14.Name = "ckTb_modptnext_14"
    Me.ckTb_modptnext_14.NTSCheckValue = "S"
    Me.ckTb_modptnext_14.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_14.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_14.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_14.Properties.AutoHeight = False
    Me.ckTb_modptnext_14.Properties.Caption = "......................................"
    Me.ckTb_modptnext_14.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_14.TabIndex = 24
    '
    'ckTb_modptnext_13
    '
    Me.ckTb_modptnext_13.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_13.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_modptnext_13.Name = "ckTb_modptnext_13"
    Me.ckTb_modptnext_13.NTSCheckValue = "S"
    Me.ckTb_modptnext_13.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_13.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_13.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_13.Properties.AutoHeight = False
    Me.ckTb_modptnext_13.Properties.Caption = "......................................"
    Me.ckTb_modptnext_13.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_13.TabIndex = 23
    '
    'ckTb_modptnext_12
    '
    Me.ckTb_modptnext_12.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_12.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_modptnext_12.Name = "ckTb_modptnext_12"
    Me.ckTb_modptnext_12.NTSCheckValue = "S"
    Me.ckTb_modptnext_12.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_12.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_12.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_12.Properties.AutoHeight = False
    Me.ckTb_modptnext_12.Properties.Caption = "......................................"
    Me.ckTb_modptnext_12.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_12.TabIndex = 22
    '
    'ckTb_modptnext_11
    '
    Me.ckTb_modptnext_11.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_11.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_modptnext_11.Name = "ckTb_modptnext_11"
    Me.ckTb_modptnext_11.NTSCheckValue = "S"
    Me.ckTb_modptnext_11.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_11.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_11.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_11.Properties.AutoHeight = False
    Me.ckTb_modptnext_11.Properties.Caption = "......................................"
    Me.ckTb_modptnext_11.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_11.TabIndex = 21
    '
    'pnPag7Left
    '
    Me.pnPag7Left.AllowDrop = True
    Me.pnPag7Left.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPag7Left.Appearance.Options.UseBackColor = True
    Me.pnPag7Left.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPag7Left.Controls.Add(Me.ckTb_modptnext_10)
    Me.pnPag7Left.Controls.Add(Me.ckTb_modptnext_9)
    Me.pnPag7Left.Controls.Add(Me.ckTb_modptnext_8)
    Me.pnPag7Left.Controls.Add(Me.ckTb_modptnext_7)
    Me.pnPag7Left.Controls.Add(Me.ckTb_modptnext_6)
    Me.pnPag7Left.Controls.Add(Me.ckTb_modptnext_5)
    Me.pnPag7Left.Controls.Add(Me.ckTb_modptnext_4)
    Me.pnPag7Left.Controls.Add(Me.ckTb_modptnext_3)
    Me.pnPag7Left.Controls.Add(Me.ckTb_modptnext_2)
    Me.pnPag7Left.Controls.Add(Me.ckTb_modptnext_1)
    Me.pnPag7Left.Cursor = System.Windows.Forms.Cursors.Hand
    Me.pnPag7Left.Location = New System.Drawing.Point(3, 3)
    Me.pnPag7Left.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnPag7Left.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnPag7Left.Name = "pnPag7Left"
    Me.pnPag7Left.NTSActiveTrasparency = True
    Me.pnPag7Left.Size = New System.Drawing.Size(197, 264)
    Me.pnPag7Left.TabIndex = 21
    Me.pnPag7Left.Text = "NtsPanel1"
    '
    'ckTb_modptnext_10
    '
    Me.ckTb_modptnext_10.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_10.Location = New System.Drawing.Point(3, 229)
    Me.ckTb_modptnext_10.Name = "ckTb_modptnext_10"
    Me.ckTb_modptnext_10.NTSCheckValue = "S"
    Me.ckTb_modptnext_10.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_10.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_10.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_10.Properties.AutoHeight = False
    Me.ckTb_modptnext_10.Properties.Caption = "......................................"
    Me.ckTb_modptnext_10.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_10.TabIndex = 30
    '
    'ckTb_modptnext_9
    '
    Me.ckTb_modptnext_9.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_9.Location = New System.Drawing.Point(3, 205)
    Me.ckTb_modptnext_9.Name = "ckTb_modptnext_9"
    Me.ckTb_modptnext_9.NTSCheckValue = "S"
    Me.ckTb_modptnext_9.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_9.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_9.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_9.Properties.AutoHeight = False
    Me.ckTb_modptnext_9.Properties.Caption = "......................................"
    Me.ckTb_modptnext_9.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_9.TabIndex = 29
    '
    'ckTb_modptnext_8
    '
    Me.ckTb_modptnext_8.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_8.Location = New System.Drawing.Point(3, 181)
    Me.ckTb_modptnext_8.Name = "ckTb_modptnext_8"
    Me.ckTb_modptnext_8.NTSCheckValue = "S"
    Me.ckTb_modptnext_8.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_8.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_8.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_8.Properties.AutoHeight = False
    Me.ckTb_modptnext_8.Properties.Caption = "......................................"
    Me.ckTb_modptnext_8.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_8.TabIndex = 28
    '
    'ckTb_modptnext_7
    '
    Me.ckTb_modptnext_7.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_7.Location = New System.Drawing.Point(3, 157)
    Me.ckTb_modptnext_7.Name = "ckTb_modptnext_7"
    Me.ckTb_modptnext_7.NTSCheckValue = "S"
    Me.ckTb_modptnext_7.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_7.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_7.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_7.Properties.AutoHeight = False
    Me.ckTb_modptnext_7.Properties.Caption = "......................................"
    Me.ckTb_modptnext_7.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_7.TabIndex = 27
    '
    'ckTb_modptnext_6
    '
    Me.ckTb_modptnext_6.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_6.Location = New System.Drawing.Point(3, 133)
    Me.ckTb_modptnext_6.Name = "ckTb_modptnext_6"
    Me.ckTb_modptnext_6.NTSCheckValue = "S"
    Me.ckTb_modptnext_6.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_6.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_6.Properties.AutoHeight = False
    Me.ckTb_modptnext_6.Properties.Caption = "......................................"
    Me.ckTb_modptnext_6.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_6.TabIndex = 26
    '
    'ckTb_modptnext_5
    '
    Me.ckTb_modptnext_5.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_5.Location = New System.Drawing.Point(3, 109)
    Me.ckTb_modptnext_5.Name = "ckTb_modptnext_5"
    Me.ckTb_modptnext_5.NTSCheckValue = "S"
    Me.ckTb_modptnext_5.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_5.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_5.Properties.AutoHeight = False
    Me.ckTb_modptnext_5.Properties.Caption = "......................................"
    Me.ckTb_modptnext_5.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_5.TabIndex = 25
    '
    'ckTb_modptnext_4
    '
    Me.ckTb_modptnext_4.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_4.Location = New System.Drawing.Point(3, 85)
    Me.ckTb_modptnext_4.Name = "ckTb_modptnext_4"
    Me.ckTb_modptnext_4.NTSCheckValue = "S"
    Me.ckTb_modptnext_4.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_4.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_4.Properties.AutoHeight = False
    Me.ckTb_modptnext_4.Properties.Caption = "......................................"
    Me.ckTb_modptnext_4.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_4.TabIndex = 24
    '
    'ckTb_modptnext_3
    '
    Me.ckTb_modptnext_3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_3.Location = New System.Drawing.Point(3, 61)
    Me.ckTb_modptnext_3.Name = "ckTb_modptnext_3"
    Me.ckTb_modptnext_3.NTSCheckValue = "S"
    Me.ckTb_modptnext_3.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_3.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_3.Properties.AutoHeight = False
    Me.ckTb_modptnext_3.Properties.Caption = "......................................"
    Me.ckTb_modptnext_3.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_3.TabIndex = 23
    '
    'ckTb_modptnext_2
    '
    Me.ckTb_modptnext_2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_2.Location = New System.Drawing.Point(3, 37)
    Me.ckTb_modptnext_2.Name = "ckTb_modptnext_2"
    Me.ckTb_modptnext_2.NTSCheckValue = "S"
    Me.ckTb_modptnext_2.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_2.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_2.Properties.AutoHeight = False
    Me.ckTb_modptnext_2.Properties.Caption = "......................................"
    Me.ckTb_modptnext_2.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_2.TabIndex = 22
    '
    'ckTb_modptnext_1
    '
    Me.ckTb_modptnext_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTb_modptnext_1.Location = New System.Drawing.Point(3, 13)
    Me.ckTb_modptnext_1.Name = "ckTb_modptnext_1"
    Me.ckTb_modptnext_1.NTSCheckValue = "S"
    Me.ckTb_modptnext_1.NTSUnCheckValue = "N"
    Me.ckTb_modptnext_1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTb_modptnext_1.Properties.Appearance.Options.UseBackColor = True
    Me.ckTb_modptnext_1.Properties.AutoHeight = False
    Me.ckTb_modptnext_1.Properties.Caption = "......................................"
    Me.ckTb_modptnext_1.Size = New System.Drawing.Size(180, 19)
    Me.ckTb_modptnext_1.TabIndex = 21
    '
    'FRM__INSG
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(618, 326)
    Me.Controls.Add(Me.tsInsg)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FRM__INSG"
    Me.Text = "INIZIALIZZAZIONE DATABASE AZIENDA"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsInsg, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsInsg.ResumeLayout(False)
    Me.NtsTabPage8.ResumeLayout(False)
    CType(Me.pnPag8, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag8.ResumeLayout(False)
    Me.pnPag8.PerformLayout()
    CType(Me.ckSconGenex.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconClCliDaList.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconSpecNoCum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbScPrior.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbSc6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbSc5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbSc4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbSc3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbSc2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbSc1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSc6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSc5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSc4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSc3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSc2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSc1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage3.ResumeLayout(False)
    CType(Me.pnPag3, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag3.ResumeLayout(False)
    Me.pnPag3.PerformLayout()
    CType(Me.edTb_maxlencodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_seriemaxlen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_annopaghe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_anno770.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_annodich.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_opz_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_destcorr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_destresan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_destsedel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_destdomf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckXx_codditcsc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnPag1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1.ResumeLayout(False)
    CType(Me.pnPag1Right, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1Right.ResumeLayout(False)
    Me.pnPag1Right.PerformLayout()
    CType(Me.ckTb_mod2_15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod2_14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod2_13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod2_12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod2_11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod2_10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod2_9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod2_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod2_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod2_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag1Center, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1Center.ResumeLayout(False)
    Me.pnPag1Center.PerformLayout()
    CType(Me.ckTb_mod2_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod2_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod2_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod2_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod2_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod_15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod_14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod_13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod_12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod_11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag1Left, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag1Left.ResumeLayout(False)
    Me.pnPag1Left.PerformLayout()
    CType(Me.ckTb_mod_10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod_9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnPag2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag2.ResumeLayout(False)
    CType(Me.pnPag2Right, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag2Right.ResumeLayout(False)
    Me.pnPag2Right.PerformLayout()
    CType(Me.ckTb_mod4_15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod4_14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod4_13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod4_12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod4_11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod4_10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod4_9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod4_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod4_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod4_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag2Center, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag2Center.ResumeLayout(False)
    Me.pnPag2Center.PerformLayout()
    CType(Me.ckTb_mod4_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod4_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod4_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod4_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod4_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod3_15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod3_14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod3_13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod3_12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod3_11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag2Left, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag2Left.ResumeLayout(False)
    Me.pnPag2Left.PerformLayout()
    CType(Me.ckTb_mod3_10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod3_9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod3_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod3_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod3_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod3_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod3_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod3_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod3_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_mod3_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage4.ResumeLayout(False)
    CType(Me.pnPag4, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag4.ResumeLayout(False)
    CType(Me.pnPag4Right, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag4Right.ResumeLayout(False)
    Me.pnPag4Right.PerformLayout()
    CType(Me.ckTb_modsup_30.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_29.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_28.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_27.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_26.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_25.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_24.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_23.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_22.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_21.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag4Center, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag4Center.ResumeLayout(False)
    Me.pnPag4Center.PerformLayout()
    CType(Me.ckTb_modsup_20.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_19.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_18.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_17.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_16.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag4Left, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag4Left.ResumeLayout(False)
    Me.pnPag4Left.PerformLayout()
    CType(Me.ckTb_modsup_10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsup_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage5.ResumeLayout(False)
    CType(Me.pnPag5, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag5.ResumeLayout(False)
    CType(Me.pnPag5Right, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag5Right.ResumeLayout(False)
    Me.pnPag5Right.PerformLayout()
    CType(Me.ckTb_modsupext_30.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_29.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_28.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_27.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_26.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_25.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_24.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_23.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_22.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_21.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag5Center, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag5Center.ResumeLayout(False)
    Me.pnPag5Center.PerformLayout()
    CType(Me.ckTb_modsupext_20.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_19.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_18.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_17.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_16.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag5Left, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag5Left.ResumeLayout(False)
    Me.pnPag5Left.PerformLayout()
    CType(Me.ckTb_modsupext_10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modsupext_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage6.ResumeLayout(False)
    CType(Me.pnPag6, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag6.ResumeLayout(False)
    CType(Me.pnPag6Right, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag6Right.ResumeLayout(False)
    Me.pnPag6Right.PerformLayout()
    CType(Me.ckTb_modptn_30.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_29.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_28.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_27.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_26.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_25.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_24.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_23.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_22.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_21.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag6Center, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag6Center.ResumeLayout(False)
    Me.pnPag6Center.PerformLayout()
    CType(Me.ckTb_modptn_20.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_19.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_18.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_17.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_16.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag6Left, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag6Left.ResumeLayout(False)
    Me.pnPag6Left.PerformLayout()
    CType(Me.ckTb_modptn_10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptn_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage7.ResumeLayout(False)
    CType(Me.pnPag7, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag7.ResumeLayout(False)
    CType(Me.pnPag7Right, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag7Right.ResumeLayout(False)
    Me.pnPag7Right.PerformLayout()
    CType(Me.ckTb_modptnext_30.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_29.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_28.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_27.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_26.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_25.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_24.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_23.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_22.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_21.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag7Center, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag7Center.ResumeLayout(False)
    Me.pnPag7Center.PerformLayout()
    CType(Me.ckTb_modptnext_20.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_19.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_18.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_17.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_16.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_15.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_14.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_13.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_12.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_11.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPag7Left, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPag7Left.ResumeLayout(False)
    Me.pnPag7Left.PerformLayout()
    CType(Me.ckTb_modptnext_10.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_9.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_8.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_7.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTb_modptnext_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()
      '-------------------------------------------------
      ckTb_mod2_15.NTSSetParam(oMenu, oApp.Tr(Me, 128302548687956831, "(a disposizione - 2)"), "S", "N")
      ckTb_mod2_14.NTSSetParam(oMenu, oApp.Tr(Me, 128302548688113083, "(a disposizione - 1)"), "S", "N")
      ckTb_mod2_13.NTSSetParam(oMenu, oApp.Tr(Me, 128302548688269335, "Sistema Qualità"), "S", "N")
      ckTb_mod2_12.NTSSetParam(oMenu, oApp.Tr(Me, 128302548688425587, "Configuratore di prodotto"), "S", "N")
      ckTb_mod2_11.NTSSetParam(oMenu, oApp.Tr(Me, 128302548688581839, "RSM"), "S", "N")
      ckTb_mod2_10.NTSSetParam(oMenu, oApp.Tr(Me, 128302548688738091, "(a disposizione - 5)"), "S", "N")
      ckTb_mod2_9.NTSSetParam(oMenu, oApp.Tr(Me, 128302548688894343, "Tesoreria"), "S", "N")
      ckTb_mod2_8.NTSSetParam(oMenu, oApp.Tr(Me, 128302548689050595, "(a disposizione - 4)"), "S", "N")
      ckTb_mod2_7.NTSSetParam(oMenu, oApp.Tr(Me, 128302548689206847, "(a disposizione - 3)"), "S", "N")
      ckTb_mod2_6.NTSSetParam(oMenu, oApp.Tr(Me, 128302548689363099, "Ritenute Acconto"), "S", "N")
      ckTb_mod2_5.NTSSetParam(oMenu, oApp.Tr(Me, 128783055625150021, "Generaz. PDF da documenti"), "S", "N")
      ckTb_mod2_4.NTSSetParam(oMenu, oApp.Tr(Me, 128302548689519351, "Intrastat"), "S", "N")
      ckTb_mod2_3.NTSSetParam(oMenu, oApp.Tr(Me, 128302548689675603, "Project management"), "S", "N")
      ckTb_mod2_2.NTSSetParam(oMenu, oApp.Tr(Me, 128302548689831855, "Tentata vendita"), "S", "N")
      ckTb_mod2_1.NTSSetParam(oMenu, oApp.Tr(Me, 128302548689988107, "Richiesta d'acquisto"), "S", "N")
      ckTb_mod_15.NTSSetParam(oMenu, oApp.Tr(Me, 128302548690144359, "Import/Export dati"), "S", "N")
      ckTb_mod_14.NTSSetParam(oMenu, oApp.Tr(Me, 128302548690300611, "Produzione"), "S", "N")
      ckTb_mod_13.NTSSetParam(oMenu, oApp.Tr(Me, 128302548690456863, "Customer Service"), "S", "N")
      ckTb_mod_12.NTSSetParam(oMenu, oApp.Tr(Me, 128302548690613115, "Conai"), "S", "N")
      ckTb_mod_11.NTSSetParam(oMenu, oApp.Tr(Me, 128302548690769367, "Source Extender"), "S", "N")
      ckTb_mod_10.NTSSetParam(oMenu, oApp.Tr(Me, 128783055777640261, "Verticale 1"), "S", "N")
      ckTb_mod_9.NTSSetParam(oMenu, oApp.Tr(Me, 128302548690925619, "Cespiti"), "S", "N")
      ckTb_mod_8.NTSSetParam(oMenu, oApp.Tr(Me, 128302548691081871, "Contabilità Analitica"), "S", "N")
      ckTb_mod_7.NTSSetParam(oMenu, oApp.Tr(Me, 128302548691238123, "Distinta Base"), "S", "N")
      ckTb_mod_6.NTSSetParam(oMenu, oApp.Tr(Me, 128302548691394375, "Magazzino"), "S", "N")
      ckTb_mod_5.NTSSetParam(oMenu, oApp.Tr(Me, 128302548691550627, "Statistiche"), "S", "N")
      ckTb_mod_4.NTSSetParam(oMenu, oApp.Tr(Me, 128302548691706879, "Provvigioni"), "S", "N")
      ckTb_mod_3.NTSSetParam(oMenu, oApp.Tr(Me, 128302548691863131, "Vendite"), "S", "N")
      ckTb_mod_2.NTSSetParam(oMenu, oApp.Tr(Me, 128302548692019383, "Ordini"), "S", "N")
      ckTb_mod_1.NTSSetParam(oMenu, oApp.Tr(Me, 128302548692175635, "Contabilità Gen/sempl."), "S", "N")
      ckTb_mod4_15.NTSSetParam(oMenu, oApp.Tr(Me, 128783055903569701, "Moduli Supplementari Partner"), "S", "N")
      ckTb_mod4_14.NTSSetParam(oMenu, oApp.Tr(Me, 128302548692331887, "(a disposizione - 7)"), "S", "N")
      ckTb_mod4_13.NTSSetParam(oMenu, oApp.Tr(Me, 128302548692488139, "(a disposizione - 6)"), "S", "N")
      ckTb_mod4_12.NTSSetParam(oMenu, oApp.Tr(Me, 128302548692644391, "Data-Warehouse"), "S", "N")
      ckTb_mod4_11.NTSSetParam(oMenu, oApp.Tr(Me, 128302548692800643, "Moduli Supplementari NTS"), "S", "N")
      ckTb_mod4_10.NTSSetParam(oMenu, oApp.Tr(Me, 128302548692956895, "BUSNET Frame"), "S", "N")
      ckTb_mod4_9.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_mod4_8.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_mod4_7.NTSSetParam(oMenu, oApp.Tr(Me, 128302548693425651, "Produzione Taglie colori"), "S", "N")
      ckTb_mod4_6.NTSSetParam(oMenu, oApp.Tr(Me, 128302548693581903, "Elenchi Iva Cli-for"), "S", "N")
      ckTb_mod4_5.NTSSetParam(oMenu, oApp.Tr(Me, 128783056039342261, "Commesse light"), "S", "N")
      ckTb_mod4_4.NTSSetParam(oMenu, oApp.Tr(Me, 128302548693738155, "Non Solo Shop"), "S", "N")
      ckTb_mod4_3.NTSSetParam(oMenu, oApp.Tr(Me, 128302548693894407, "International"), "S", "N")
      ckTb_mod4_2.NTSSetParam(oMenu, oApp.Tr(Me, 128302548694050659, "Verticale 2"), "S", "N")
      ckTb_mod4_1.NTSSetParam(oMenu, oApp.Tr(Me, 128302548694206911, "Logistica Extended"), "S", "N")
      ckTb_mod3_15.NTSSetParam(oMenu, oApp.Tr(Me, 128302548694363163, "F24 BUSINESS"), "S", "N")
      ckTb_mod3_14.NTSSetParam(oMenu, oApp.Tr(Me, 128302548694519415, "Contab. professionisti"), "S", "N")
      ckTb_mod3_13.NTSSetParam(oMenu, oApp.Tr(Me, 128302548694675667, "Magazzino EASY"), "S", "N")
      ckTb_mod3_12.NTSSetParam(oMenu, oApp.Tr(Me, 128302548694831919, "Ordini EASY"), "S", "N")
      ckTb_mod3_11.NTSSetParam(oMenu, oApp.Tr(Me, 128302548694988171, "Contabilità generale EASY"), "S", "N")
      ckTb_mod3_10.NTSSetParam(oMenu, oApp.Tr(Me, 128783056132461301, "Anagrafiche Generali"), "S", "N")
      ckTb_mod3_9.NTSSetParam(oMenu, oApp.Tr(Me, 128302548695144423, "Telematico Oper. Rilevanti Iva"), "S", "N")
      ckTb_mod3_8.NTSSetParam(oMenu, oApp.Tr(Me, 128302548695300675, "Parcellazione"), "S", "N")
      ckTb_mod3_7.NTSSetParam(oMenu, oApp.Tr(Me, 128302548695456927, "Picking"), "S", "N")
      ckTb_mod3_6.NTSSetParam(oMenu, oApp.Tr(Me, 128302548695613179, "CRM"), "S", "N")
      ckTb_mod3_5.NTSSetParam(oMenu, oApp.Tr(Me, 128302548695769431, "Black List"), "S", "N")
      ckTb_mod3_4.NTSSetParam(oMenu, oApp.Tr(Me, 128302548695925683, "Bilancio UE e nota integr."), "S", "N")
      ckTb_mod3_3.NTSSetParam(oMenu, oApp.Tr(Me, 128302548696081935, "Taglie e colori"), "S", "N")
      ckTb_mod3_2.NTSSetParam(oMenu, oApp.Tr(Me, 128302548696238187, "Retail"), "S", "N")
      ckTb_mod3_1.NTSSetParam(oMenu, oApp.Tr(Me, 128302548696394439, "Configuratore su distinta neutra"), "S", "N")
      'edTb_annopaghe.NTSSetParam(oMenu, oApp.Tr(Me, 128302548696394439, "Anno corrente gestione paghe"), "0", 4, 0, 2099)
      edTb_anno770.NTSSetParam(oMenu, oApp.Tr(Me, 128302548696550691, "Anno corrente dichiarazioni sostituti d'imposta"), "0", 4, 0, 2099)
      edTb_annodich.NTSSetParam(oMenu, oApp.Tr(Me, 128302548696706943, "Anno corrente dichiarazioni redditi"), "0", 4, 0, 2099)
      cbTb_opz_1.NTSSetParam(oApp.Tr(Me, 128302548696863195, "Valuta Azienda"))
      edTb_destcorr.NTSSetParam(oMenu, oApp.Tr(Me, 128302548697019447, "Luogo esercizio attività all'estero"), "0", 9, 0, 999999999)
      edTb_destresan.NTSSetParam(oMenu, oApp.Tr(Me, 128302548697175699, "Residenza / Sede legale estera"), "0", 9, 0, 999999999)
      edTb_destsedel.NTSSetParam(oMenu, oApp.Tr(Me, 128302548697331951, "Residenza / Domicilio / Sede legale italiana"), "0", 9, 0, 999999999)
      edTb_destdomf.NTSSetParam(oMenu, oApp.Tr(Me, 128302548697488203, "Domicilio Fiscale"), "0", 9, 0, 999999999)
      ckXx_codditcsc.NTSSetParam(oMenu, oApp.Tr(Me, 128302548697644455, "Crea automaticamente contropartite generiche"), "S", " ")
      ckTb_modsup_30.NTSSetParam(oMenu, oApp.Tr(Me, 130275942900378649, "Friendly"), "S", "N")
      ckTb_modsup_29.NTSSetParam(oMenu, oApp.Tr(Me, 130258904594081662, "Smart Business Framework"), "S", "N")
      ckTb_modsup_28.NTSSetParam(oMenu, oApp.Tr(Me, 130403892896136881, "Crystal Reports 64bit server"), "S", "N")
      ckTb_modsup_27.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsup_26.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsup_25.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsup_24.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsup_23.NTSSetParam(oMenu, "Collegamento a sistema e-Commerce", "S", "N")
      ckTb_modsup_22.NTSSetParam(oMenu, oApp.Tr(Me, 130275943071526539, "Dichiarazione di intenti"), "S", "N")
      ckTb_modsup_21.NTSSetParam(oMenu, oApp.Tr(Me, 130275943071526531, "Pec-mail"), "S", "N")
      ckTb_modsup_20.NTSSetParam(oMenu, oApp.Tr(Me, 130204259819415191, "Analisi Fattibilità Impegni"), "S", "N")
      ckTb_modsup_19.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsup_18.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsup_17.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsup_16.NTSSetParam(oMenu, oApp.Tr(Me, 129611584369522712, "Incassi e pagamenti differiri"), "S", "N")
      ckTb_modsup_15.NTSSetParam(oMenu, oApp.Tr(Me, 129611584543621194, "Gestione punti vendita recovery"), "S", "N")
      ckTb_modsup_14.NTSSetParam(oMenu, oApp.Tr(Me, 129611584673225780, "Gestione punti vendita extended"), "S", "N")
      ckTb_modsup_13.NTSSetParam(oMenu, oApp.Tr(Me, 129470975978236255, "Gestione punti vendita (nuovo retail)"), "S", "N")
      ckTb_modsup_12.NTSSetParam(oMenu, oApp.Tr(Me, 129470975964798583, "Tesorerira extended"), "S", "N")
      ckTb_modsup_11.NTSSetParam(oMenu, oApp.Tr(Me, 129470975990736415, "Tesoreria e flussi finaziari"), "S", "N")
      ckTb_modsup_10.NTSSetParam(oMenu, oApp.Tr(Me, 129470975997455251, "Desktop Consolle"), "S", "N")
      ckTb_modsup_9.NTSSetParam(oMenu, oApp.Tr(Me, 129470976008861647, "Budget e controllo di gestione"), "S", "N")
      ckTb_modsup_8.NTSSetParam(oMenu, oApp.Tr(Me, 129470976036361999, "Contabilità Analitica DC"), "S", "N")
      ckTb_modsup_7.NTSSetParam(oMenu, oApp.Tr(Me, 129470974677438355, "Datawarehouse extend"), "S", "N")
      ckTb_modsup_6.NTSSetParam(oMenu, oApp.Tr(Me, 128302548702175763, "E-mail integrata"), "S", "N")
      ckTb_modsup_5.NTSSetParam(oMenu, oApp.Tr(Me, 128302548702332015, "......................................"), "S", "N")
      ckTb_modsup_4.NTSSetParam(oMenu, oApp.Tr(Me, 128302548702488267, "......................................"), "S", "N")
      ckTb_modsup_3.NTSSetParam(oMenu, oApp.Tr(Me, 128302548702644519, "Logistica su palmari"), "S", "N")
      ckTb_modsup_2.NTSSetParam(oMenu, oApp.Tr(Me, 128302548702800771, "Controllo ric. merce su palmare"), "S", "N")
      ckTb_modsup_1.NTSSetParam(oMenu, oApp.Tr(Me, 128302548702957023, "Inventari su palmare"), "S", "N")
      ckTb_modsupext_30.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_29.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_28.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_27.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_26.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_25.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_24.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_23.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_22.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_21.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_20.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_19.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_18.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_17.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_16.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_15.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_14.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_13.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_12.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_11.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_10.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_9.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_8.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_7.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_6.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_5.NTSSetParam(oMenu, "Analisi di bilancio", "S", "N")
      ckTb_modsupext_4.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_3.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_2.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modsupext_1.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_30.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_29.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_28.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_27.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_26.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_25.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_24.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_23.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_22.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_21.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_20.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_19.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_18.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_17.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_16.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_15.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_14.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_13.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_12.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_11.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_10.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_9.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_8.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_7.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_6.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_5.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_4.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_2.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptn_1.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_30.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_29.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_28.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_27.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_26.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_25.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_24.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_23.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_22.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_21.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_20.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_19.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_18.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_17.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_16.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_15.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_14.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_13.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_12.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_11.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_10.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_9.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_8.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_7.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_6.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_5.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_4.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_3.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_2.NTSSetParam(oMenu, "......................................", "S", "N")
      ckTb_modptnext_1.NTSSetParam(oMenu, "......................................", "S", "N")
      edTb_seriemaxlen.NTSSetParam(oMenu, oApp.Tr(Me, 130071244621821070, "Lunghezza serie"), "0", 1, 1, 3)
      edTb_maxlencodart.NTSSetParam(oMenu, oApp.Tr(Me, 130541237432716164, "Lunghezza codice articolo"), "0", 2, 18, 50)

      'non faccio lanciare lo zoom dal controllo visto che non devo cambiare il contenuto del campo ma riposizionare il dataset!!!
      'edTb_codInsg.NTSForzaVisZoom = False
      'edTb_desInsg.NTSForzaVisZoom = True 'controllare

      'edTb_desInsg.NTSSetParamZoom("") 'controllare

      'edTb_codInsg.NTSSetRichiesto()
      'edTb_desInsg.NTSSetRichiesto()

      edTb_annopaghe.Visible = False
      edTb_annopaghe.Enabled = False

      Dim dttPriorSc As New DataTable()
      dttPriorSc.Columns.Add("cod", GetType(String))
      dttPriorSc.Columns.Add("val", GetType(String))
      dttPriorSc.Rows.Add(New Object() {"ABCDEF", "ABCDEF"})
      dttPriorSc.Rows.Add(New Object() {"ACBDFE", "ACBDFE"})
      dttPriorSc.AcceptChanges()
      cbScPrior.DataSource = dttPriorSc : cbScPrior.ValueMember = "cod" : cbScPrior.DisplayMember = "val"

      Dim dttCumSos As New DataTable()
      dttCumSos.Columns.Add("cod", GetType(String))
      dttCumSos.Columns.Add("val", GetType(String))
      dttCumSos.Rows.Add(New Object() {"C", "Cumulativo"})
      dttCumSos.Rows.Add(New Object() {"S", "Sostitutivo"})
      dttCumSos.AcceptChanges()
      cbSc1.DataSource = dttCumSos : cbSc1.ValueMember = "cod" : cbSc1.DisplayMember = "val"
      cbSc2.DataSource = dttCumSos : cbSc2.ValueMember = "cod" : cbSc2.DisplayMember = "val"
      cbSc3.DataSource = dttCumSos : cbSc3.ValueMember = "cod" : cbSc3.DisplayMember = "val"
      cbSc4.DataSource = dttCumSos : cbSc4.ValueMember = "cod" : cbSc4.DisplayMember = "val"
      cbSc5.DataSource = dttCumSos : cbSc5.ValueMember = "cod" : cbSc5.DisplayMember = "val"
      cbSc6.DataSource = dttCumSos : cbSc6.ValueMember = "cod" : cbSc6.DisplayMember = "val"

      ckSconti.NTSSetParam(oMenu, oApp.Tr(Me, 130428245666847345, "Gestisci gli sconti"), "S", "N")
      cbScPrior.NTSSetParam(oApp.Tr(Me, 130428245667159833, "Priorità sconti"))
      cbSc6.NTSSetParam(oApp.Tr(Me, 130428245667316108, "Sconto cumulativo/sostitutivo 6"))
      cbSc5.NTSSetParam(oApp.Tr(Me, 130428245667472341, "Sconto cumulativo/sostitutivo 5"))
      cbSc4.NTSSetParam(oApp.Tr(Me, 130428245667628617, "Sconto cumulativo/sostitutivo 4"))
      cbSc3.NTSSetParam(oApp.Tr(Me, 130428245667784872, "Sconto cumulativo/sostitutivo 3"))
      cbSc2.NTSSetParam(oApp.Tr(Me, 130428245667941105, "Sconto cumulativo/sostitutivo 2"))
      cbSc1.NTSSetParam(oApp.Tr(Me, 130428245668097355, "Sconto cumulativo/sostitutivo 1"))
      ckSc6.NTSSetParam(oMenu, oApp.Tr(Me, 130428245668253609, "Gestisci sconto 6 / F"), "S", "N")
      ckSc5.NTSSetParam(oMenu, oApp.Tr(Me, 130428245668409868, "Gestisci sconto 5 / E"), "S", "N")
      ckSc4.NTSSetParam(oMenu, oApp.Tr(Me, 130428245668566131, "Gestisci sconto 4 / D"), "S", "N")
      ckSc3.NTSSetParam(oMenu, oApp.Tr(Me, 130428245668722389, "Gestisci sconto 3 / C"), "S", "N")
      ckSc2.NTSSetParam(oMenu, oApp.Tr(Me, 130428245668878640, "Gestisci sconto 2 / B"), "S", "N")
      ckSc1.NTSSetParam(oMenu, oApp.Tr(Me, 130428245669034907, "Gestisci sconto 1 / A"), "S", "N")
      ckSconGenex.NTSSetParam(oMenu, oApp.Tr(Me, 130431647863871461, "Gestisci sconti generici clienti con sconti generici articoli"), "S", "N")
      ckSconSpecNoCum.NTSSetParam(oMenu, oApp.Tr(Me, 130428271280202485, "Ignora altri sconti se trovato sconto specifico cliente / articolo"), "S", "N")
      ckSconClCliDaList.NTSSetParam(oMenu, oApp.Tr(Me, 130428271266488844, "Classe sconto cliente presa da numero listino"), "S", "N")

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
#End Region
  
#Region "Eventi di Form"
  Public Overridable Sub FRM__INSG_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim bOut As Boolean
    Try
      '-------------------------------------------------
      'carico i combobox
      CaricaCombo()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      tsInsg.SelectedTabPageIndex = 0

      bOut = Apri()

      nVecchiaLenSerie = NTSCInt(edTb_seriemaxlen.Text)
      nVecchiaLenCodart = NTSCInt(edTb_maxlencodart.Text)
      nSerieEsistente = nVecchiaLenSerie
      nCodartEsistente = nVecchiaLenCodart

      'campi letti da opzioni di registro di Business
      ckSconti.Checked = CBool(oApp.oGvar.strGestioneSconti.ToUpper = "S")
      ckSc1.Checked = CBool(oApp.oGvar.strTrattaScA.ToUpper = "S")
      ckSc2.Checked = CBool(oApp.oGvar.strTrattaScB.ToUpper = "S")
      ckSc3.Checked = CBool(oApp.oGvar.strTrattaScC.ToUpper = "S")
      ckSc4.Checked = CBool(oApp.oGvar.strTrattaScD.ToUpper = "S")
      ckSc5.Checked = CBool(oApp.oGvar.strTrattaScE.ToUpper = "S")
      ckSc6.Checked = CBool(oApp.oGvar.strTrattaScF.ToUpper = "S")
      ckSconGenex.Checked = CBool(oApp.oGvar.strSconGenex.ToUpper = "S")
      cbSc1.SelectedValue = oApp.oGvar.strTrattaSc1
      cbSc2.SelectedValue = oApp.oGvar.strTrattaSc2
      cbSc3.SelectedValue = oApp.oGvar.strTrattaSc3
      cbSc4.SelectedValue = oApp.oGvar.strTrattaSc4
      cbSc5.SelectedValue = oApp.oGvar.strTrattaSc5
      cbSc6.SelectedValue = oApp.oGvar.strTrattaSc6
      cbScPrior.SelectedValue = oApp.oGvar.strPriorSconti
      ckSconSpecNoCum.Checked = CBool(oApp.oGvar.strSconSpecNoCum.ToUpper = "S")
      ckSconClCliDaList.Checked = CBool(oApp.oGvar.strSconClCliDaList.ToUpper = "S")

      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi ToolBar"
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
  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      Me.ValidaLastControl()          'se non valido il controllo su cui sono, quando modifico il controllo e, senza uscire, faccio 'ripristina' il controllo rimane sporco

      '-------------------------------------------------
      'Ripristino l'inizializzazione database azienda
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128300183072189954, "Ripristinare le modifiche apportate all'inizializzazione database azienda?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          oCleInsg.Ripristina(dcInsg.Position, dcInsg.Filter)

          'campi letti da opzioni di registro di Business
          ckSconti.Checked = CBool(oApp.oGvar.strGestioneSconti.ToUpper = "S")
          ckSc1.Checked = CBool(oApp.oGvar.strTrattaScA.ToUpper = "S")
          ckSc2.Checked = CBool(oApp.oGvar.strTrattaScB.ToUpper = "S")
          ckSc3.Checked = CBool(oApp.oGvar.strTrattaScC.ToUpper = "S")
          ckSc4.Checked = CBool(oApp.oGvar.strTrattaScD.ToUpper = "S")
          ckSc5.Checked = CBool(oApp.oGvar.strTrattaScE.ToUpper = "S")
          ckSc6.Checked = CBool(oApp.oGvar.strTrattaScF.ToUpper = "S")
          ckSconGenex.Checked = CBool(oApp.oGvar.strSconGenex.ToUpper = "S")
          cbSc1.SelectedValue = oApp.oGvar.strTrattaSc1
          cbSc2.SelectedValue = oApp.oGvar.strTrattaSc2
          cbSc3.SelectedValue = oApp.oGvar.strTrattaSc3
          cbSc4.SelectedValue = oApp.oGvar.strTrattaSc4
          cbSc5.SelectedValue = oApp.oGvar.strTrattaSc5
          cbSc6.SelectedValue = oApp.oGvar.strTrattaSc6
          cbScPrior.SelectedValue = oApp.oGvar.strPriorSconti
          ckSconSpecNoCum.Checked = CBool(oApp.oGvar.strSconSpecNoCum.ToUpper = "S")
          ckSconClCliDaList.Checked = CBool(oApp.oGvar.strSconClCliDaList.ToUpper = "S")
      End Select

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
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi TextBox"
  Public Overridable Sub edTb_seriemaxlen_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_seriemaxlen.ValidatedAndChanged
    Try
      If nVecchiaLenSerie > NTSCInt(edTb_seriemaxlen.Text) Then
        'se la serie è diventata più piccola deve verificare che non siano state usate serie più grandi del nuovo limite di caratteri.
        If Not oCleInsg.VerificaLenSerie(NTSCInt(edTb_seriemaxlen.Text)) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130071256573521923, "Non è possibile usare una serie con lunghezza " & edTb_seriemaxlen.Text & _
                                                         " in quanto è già stata usata in dei documenti\ordini\scadenze\ecc... una serie con lunghezza maggiore"))
          edTb_seriemaxlen.NTSTextDB = nVecchiaLenSerie.ToString()
        End If
      End If
      nVecchiaLenSerie = NTSCInt(edTb_seriemaxlen.Text)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edTb_maxlencodart_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTb_maxlencodart.ValidatedAndChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If nVecchiaLenCodart > NTSCInt(edTb_maxlencodart.Text) Then
        '------------------------------------------------------------------------------------------------------------
        '--- Se la lunghezza del codice articolo è diventata più piccola deve verificare che non siano stati usati
        '--- codici articoli più grandi del nuovo limite di caratteri.
        '------------------------------------------------------------------------------------------------------------
        If Not oCleInsg.VerificaLenCodart(NTSCInt(edTb_maxlencodart.Text)) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130541233414969626, "Attenzione!" & vbCrLf & _
            "Non è possibile indicare una lunghezza codice articolo pari a '|" & edTb_maxlencodart.Text & "|'" & vbCrLf & _
            "in quanto esistono già articoli in Anagrafica Articoli con lunghezza maggiore."))
          edTb_seriemaxlen.NTSTextDB = nVecchiaLenSerie.ToString()
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      nVecchiaLenSerie = NTSCInt(edTb_seriemaxlen.Text)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi CheckBox"
  Public Overridable Sub ckTb_mod_1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_1.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod_2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_2.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod_3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_3.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod_4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_4.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod_5_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_5.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod_6_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_6.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod_7_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_7.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod_8_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_8.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod_9_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_9.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod_10_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_10.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod_11_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_11.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod_12_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_12.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod_13_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_13.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod_14_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_14.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod_15_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod_15.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckTb_mod2_1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_1.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod2_2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_2.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod2_3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_3.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod2_4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_4.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod2_5_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_5.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod2_6_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_6.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod2_7_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_7.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod2_8_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_8.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod2_9_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_9.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod2_10_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_10.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod2_11_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_11.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod2_12_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_12.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod2_13_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_13.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod2_14_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_14.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod2_15_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod2_15.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckTb_mod3_1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_1.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod3_2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_2.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod3_3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_3.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod3_4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_4.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod3_5_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_5.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod3_6_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_6.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod3_7_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_7.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod3_8_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_8.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod3_9_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_9.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod3_10_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_10.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod3_11_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_11.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod3_12_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_12.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod3_13_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_13.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod3_14_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_14.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod3_15_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod3_15.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckTb_mod4_1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_1.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod4_2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_2.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod4_3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_3.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod4_4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_4.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod4_5_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_5.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod4_6_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_6.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod4_7_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_7.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod4_8_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_8.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod4_9_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_9.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod4_10_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_10.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod4_11_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_11.CheckedChanged
    Try
      If Not ckTb_mod4_11.Checked Then
        oCleInsg.DisabilitaSupl()
      End If
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod4_12_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_12.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod4_13_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_13.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod4_14_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_14.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_mod4_15_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_mod4_15.CheckedChanged
    Try
      If Not ckTb_mod4_15.Checked Then
        oCleInsg.DisabilitaPartner()
      End If
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckTb_modsup_1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_1.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_2.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_3.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_4.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_5_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_5.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_6_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_6.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_7_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_7.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_8_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_8.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_9_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_9.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_10_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_10.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_11_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_11.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_12_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_12.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_13_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_13.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_14_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_14.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_15_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_15.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_16_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_16.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_17_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_17.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_18_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_18.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_19_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_19.Click
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_20_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_20.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_21_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_21.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_22_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_22.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_23_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_23.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_24_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_24.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_25_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_25.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_26_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_26.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_27_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_27.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_28_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_28.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_29_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_29.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsup_30_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsup_30.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckTb_modsupext_1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_1.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_2.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_3.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_4.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_5_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_5.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_6_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_6.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_7_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_7.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_8_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_8.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_9_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_9.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_10_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_10.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_11_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_11.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_12_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_12.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_13_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_13.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_14_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_14.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_15_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_15.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_16_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_16.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_17_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_17.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_18_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_18.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_19_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_19.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_20_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_20.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_21_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_21.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_22_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_22.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_23_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_23.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_24_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_24.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_25_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_25.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_26_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_26.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_27_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_27.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_28_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_28.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_29_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_29.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modsupext_30_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modsupext_30.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckTb_modptn_1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_1.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_2.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_3.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_4.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_5_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_5.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_6_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_6.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_7_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_7.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_8_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_8.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_9_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_9.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_10_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_10.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_11_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_11.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_12_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_12.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_13_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_13.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_14_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_14.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_15_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_15.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_16_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_16.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_17_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_17.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_18_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_18.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_19_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_19.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_20_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_20.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_21_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_21.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_22_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_22.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_23_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_23.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_24_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_24.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_25_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_25.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_26_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_26.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_27_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_27.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_28_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_28.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_29_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_29.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptn_30_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptn_30.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckTb_modptnext_1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_1.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_2.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_3.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_4.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_5_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_5.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_6_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_6.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_7_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_7.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_8_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_8.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_9_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_9.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_10_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_10.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_11_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_11.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_12_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_12.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_13_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_13.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_14_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_14.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_15_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_15.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_16_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_16.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_17_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_17.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_18_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_18.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_19_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_19.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_20_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_20.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_21_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_21.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_22_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_22.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_23_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_23.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_24_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_24.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_25_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_25.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_26_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_26.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_27_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_27.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_28_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_28.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_29_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_29.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTb_modptnext_30_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTb_modptnext_30.CheckedChanged
    Try
      ColoraCheckBox()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Function Apri() As Boolean
    Try

      oCleInsg.bInternazionale = CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtINT)

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      If Not oCleInsg.Apri(DittaCorrente, dsInsg) Then Me.Close()
      dcInsg.DataSource = dsInsg.Tables("TABINSG")
      dsInsg.AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      bInLoad = True
      Bindcontrols()
      bInLoad = False

      If dsInsg.Tables("TABINSG").Rows.Count = 0 Then
        oCleInsg.Nuovo()
        dcInsg.MoveLast()

        '-------------------------------------------------
        'imposto i valori di default come impostato nella GCTL
        Me.GctlApplicaDefaultValue()

        oCleInsg.Salva(False)
      End If

      dcInsg.ResetBindings(False)
      dcInsg.MoveFirst()

      AssegnaNomiPartner()

      ColoraCheckBox()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      bInLoad = False
    End Try
  End Function

  Public Overridable Sub AssegnaNomiPartner()
    Dim dttPtn As New DataTable
    Dim dttPtnExt As New DataTable
    Dim i As Integer
    Dim strCod As String

    Try
      oCleInsg.AssegnaNomiPartner(dttPtn, dttPtnExt)

      For i = 0 To dttPtn.Rows.Count - 1
        strCod = dttPtn.Rows(i)!cod.ToString()
        Select Case i
          Case 0 : ckTb_modptn_1.Text = strCod
          Case 1 : ckTb_modptn_2.Text = strCod
          Case 2 : ckTb_modptn_3.Text = strCod
          Case 3 : ckTb_modptn_4.Text = strCod
          Case 4 : ckTb_modptn_5.Text = strCod
          Case 5 : ckTb_modptn_6.Text = strCod
          Case 6 : ckTb_modptn_7.Text = strCod
          Case 7 : ckTb_modptn_8.Text = strCod
          Case 8 : ckTb_modptn_9.Text = strCod
          Case 9 : ckTb_modptn_10.Text = strCod
          Case 10 : ckTb_modptn_11.Text = strCod
          Case 11 : ckTb_modptn_12.Text = strCod
          Case 12 : ckTb_modptn_13.Text = strCod
          Case 13 : ckTb_modptn_14.Text = strCod
          Case 14 : ckTb_modptn_15.Text = strCod
          Case 15 : ckTb_modptn_16.Text = strCod
          Case 16 : ckTb_modptn_17.Text = strCod
          Case 17 : ckTb_modptn_18.Text = strCod
          Case 18 : ckTb_modptn_19.Text = strCod
          Case 19 : ckTb_modptn_20.Text = strCod
          Case 20 : ckTb_modptn_21.Text = strCod
          Case 21 : ckTb_modptn_22.Text = strCod
          Case 22 : ckTb_modptn_23.Text = strCod
          Case 23 : ckTb_modptn_24.Text = strCod
          Case 24 : ckTb_modptn_25.Text = strCod
          Case 25 : ckTb_modptn_26.Text = strCod
          Case 26 : ckTb_modptn_27.Text = strCod
          Case 27 : ckTb_modptn_28.Text = strCod
          Case 28 : ckTb_modptn_29.Text = strCod
          Case 29 : ckTb_modptn_30.Text = strCod
        End Select
      Next
      For i = 0 To dttPtnExt.Rows.Count - 1
        strCod = dttPtnExt.Rows(i)!cod.ToString()
        Select Case i
          Case 0 : ckTb_modptnext_1.Text = strCod
          Case 1 : ckTb_modptnext_2.Text = strCod
          Case 2 : ckTb_modptnext_3.Text = strCod
          Case 3 : ckTb_modptnext_4.Text = strCod
          Case 4 : ckTb_modptnext_5.Text = strCod
          Case 5 : ckTb_modptnext_6.Text = strCod
          Case 6 : ckTb_modptnext_7.Text = strCod
          Case 7 : ckTb_modptnext_8.Text = strCod
          Case 8 : ckTb_modptnext_9.Text = strCod
          Case 9 : ckTb_modptnext_10.Text = strCod
          Case 10 : ckTb_modptnext_11.Text = strCod
          Case 11 : ckTb_modptnext_12.Text = strCod
          Case 12 : ckTb_modptnext_13.Text = strCod
          Case 13 : ckTb_modptnext_14.Text = strCod
          Case 14 : ckTb_modptnext_15.Text = strCod
          Case 15 : ckTb_modptnext_16.Text = strCod
          Case 16 : ckTb_modptnext_17.Text = strCod
          Case 17 : ckTb_modptnext_18.Text = strCod
          Case 18 : ckTb_modptnext_19.Text = strCod
          Case 19 : ckTb_modptnext_20.Text = strCod
          Case 20 : ckTb_modptnext_21.Text = strCod
          Case 21 : ckTb_modptnext_22.Text = strCod
          Case 22 : ckTb_modptnext_23.Text = strCod
          Case 23 : ckTb_modptnext_24.Text = strCod
          Case 24 : ckTb_modptnext_25.Text = strCod
          Case 25 : ckTb_modptnext_26.Text = strCod
          Case 26 : ckTb_modptnext_27.Text = strCod
          Case 27 : ckTb_modptnext_28.Text = strCod
          Case 28 : ckTb_modptnext_29.Text = strCod
          Case 29 : ckTb_modptnext_30.Text = strCod
        End Select
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      ckTb_mod2_15.NTSText.NTSDbField = "tabinsg.tb_mod2_15"
      ckTb_mod2_14.NTSText.NTSDbField = "tabinsg.tb_mod2_14"
      ckTb_mod2_13.NTSText.NTSDbField = "tabinsg.tb_mod2_13"
      ckTb_mod2_12.NTSText.NTSDbField = "tabinsg.tb_mod2_12"
      ckTb_mod2_11.NTSText.NTSDbField = "tabinsg.tb_mod2_11"
      ckTb_mod2_10.NTSText.NTSDbField = "tabinsg.tb_mod2_10"
      ckTb_mod2_9.NTSText.NTSDbField = "tabinsg.tb_mod2_9"
      ckTb_mod2_8.NTSText.NTSDbField = "tabinsg.tb_mod2_8"
      ckTb_mod2_7.NTSText.NTSDbField = "tabinsg.tb_mod2_7"
      ckTb_mod2_6.NTSText.NTSDbField = "tabinsg.tb_mod2_6"
      ckTb_mod2_5.NTSText.NTSDbField = "tabinsg.tb_mod2_5"
      ckTb_mod2_4.NTSText.NTSDbField = "tabinsg.tb_mod2_4"
      ckTb_mod2_3.NTSText.NTSDbField = "tabinsg.tb_mod2_3"
      ckTb_mod2_2.NTSText.NTSDbField = "tabinsg.tb_mod2_2"
      ckTb_mod2_1.NTSText.NTSDbField = "tabinsg.tb_mod2_1"
      ckTb_mod_15.NTSText.NTSDbField = "tabinsg.tb_mod_15"
      ckTb_mod_14.NTSText.NTSDbField = "tabinsg.tb_mod_14"
      ckTb_mod_13.NTSText.NTSDbField = "tabinsg.tb_mod_13"
      ckTb_mod_12.NTSText.NTSDbField = "tabinsg.tb_mod_12"
      ckTb_mod_11.NTSText.NTSDbField = "tabinsg.tb_mod_11"
      ckTb_mod_10.NTSText.NTSDbField = "tabinsg.tb_mod_10"
      ckTb_mod_9.NTSText.NTSDbField = "tabinsg.tb_mod_9"
      ckTb_mod_8.NTSText.NTSDbField = "tabinsg.tb_mod_8"
      ckTb_mod_7.NTSText.NTSDbField = "tabinsg.tb_mod_7"
      ckTb_mod_6.NTSText.NTSDbField = "tabinsg.tb_mod_6"
      ckTb_mod_5.NTSText.NTSDbField = "tabinsg.tb_mod_5"
      ckTb_mod_4.NTSText.NTSDbField = "tabinsg.tb_mod_4"
      ckTb_mod_3.NTSText.NTSDbField = "tabinsg.tb_mod_3"
      ckTb_mod_2.NTSText.NTSDbField = "tabinsg.tb_mod_2"
      ckTb_mod_1.NTSText.NTSDbField = "tabinsg.tb_mod_1"
      ckTb_mod4_15.NTSText.NTSDbField = "tabinsg.tb_mod4_15"
      ckTb_mod4_14.NTSText.NTSDbField = "tabinsg.tb_mod4_14"
      ckTb_mod4_13.NTSText.NTSDbField = "tabinsg.tb_mod4_13"
      ckTb_mod4_12.NTSText.NTSDbField = "tabinsg.tb_mod4_12"
      ckTb_mod4_11.NTSText.NTSDbField = "tabinsg.tb_mod4_11"
      ckTb_mod4_10.NTSText.NTSDbField = "tabinsg.tb_mod4_10"
      ckTb_mod4_9.NTSText.NTSDbField = "tabinsg.tb_mod4_9"
      ckTb_mod4_8.NTSText.NTSDbField = "tabinsg.tb_mod4_8"
      ckTb_mod4_7.NTSText.NTSDbField = "tabinsg.tb_mod4_7"
      ckTb_mod4_6.NTSText.NTSDbField = "tabinsg.tb_mod4_6"
      ckTb_mod4_5.NTSText.NTSDbField = "tabinsg.tb_mod4_5"
      ckTb_mod4_4.NTSText.NTSDbField = "tabinsg.tb_mod4_4"
      ckTb_mod4_3.NTSText.NTSDbField = "tabinsg.tb_mod4_3"
      ckTb_mod4_2.NTSText.NTSDbField = "tabinsg.tb_mod4_2"
      ckTb_mod4_1.NTSText.NTSDbField = "tabinsg.tb_mod4_1"
      ckTb_mod3_15.NTSText.NTSDbField = "tabinsg.tb_mod3_15"
      ckTb_mod3_14.NTSText.NTSDbField = "tabinsg.tb_mod3_14"
      ckTb_mod3_13.NTSText.NTSDbField = "tabinsg.tb_mod3_13"
      ckTb_mod3_12.NTSText.NTSDbField = "tabinsg.tb_mod3_12"
      ckTb_mod3_11.NTSText.NTSDbField = "tabinsg.tb_mod3_11"
      ckTb_mod3_10.NTSText.NTSDbField = "tabinsg.tb_mod3_10"
      ckTb_mod3_9.NTSText.NTSDbField = "tabinsg.tb_mod3_9"
      ckTb_mod3_8.NTSText.NTSDbField = "tabinsg.tb_mod3_8"
      ckTb_mod3_7.NTSText.NTSDbField = "tabinsg.tb_mod3_7"
      ckTb_mod3_6.NTSText.NTSDbField = "tabinsg.tb_mod3_6"
      ckTb_mod3_5.NTSText.NTSDbField = "tabinsg.tb_mod3_5"
      ckTb_mod3_4.NTSText.NTSDbField = "tabinsg.tb_mod3_4"
      ckTb_mod3_3.NTSText.NTSDbField = "tabinsg.tb_mod3_3"
      ckTb_mod3_2.NTSText.NTSDbField = "tabinsg.tb_mod3_2"
      ckTb_mod3_1.NTSText.NTSDbField = "tabinsg.tb_mod3_1"
      edTb_annopaghe.NTSDbField = "tabinsg.tb_annopaghe"
      edTb_anno770.NTSDbField = "tabinsg.tb_anno770"
      edTb_annodich.NTSDbField = "tabinsg.tb_annodich"
      cbTb_opz_1.NTSDbField = "tabinsg.tb_opz_1"
      edTb_destcorr.NTSDbField = "tabinsg.tb_destcorr"
      edTb_destresan.NTSDbField = "tabinsg.tb_destresan"
      edTb_destsedel.NTSDbField = "tabinsg.tb_destsedel"
      edTb_destdomf.NTSDbField = "tabinsg.tb_destdomf"
      ckXx_codditcsc.NTSText.NTSDbField = "tabinsg.xx_codditcsc"
      ckTb_modsup_30.NTSText.NTSDbField = "tabinsg.tb_modsup_30"
      ckTb_modsup_29.NTSText.NTSDbField = "tabinsg.tb_modsup_29"
      ckTb_modsup_28.NTSText.NTSDbField = "tabinsg.tb_modsup_28"
      ckTb_modsup_27.NTSText.NTSDbField = "tabinsg.tb_modsup_27"
      ckTb_modsup_26.NTSText.NTSDbField = "tabinsg.tb_modsup_26"
      ckTb_modsup_25.NTSText.NTSDbField = "tabinsg.tb_modsup_25"
      ckTb_modsup_24.NTSText.NTSDbField = "tabinsg.tb_modsup_24"
      ckTb_modsup_23.NTSText.NTSDbField = "tabinsg.tb_modsup_23"
      ckTb_modsup_22.NTSText.NTSDbField = "tabinsg.tb_modsup_22"
      ckTb_modsup_21.NTSText.NTSDbField = "tabinsg.tb_modsup_21"
      ckTb_modsup_20.NTSText.NTSDbField = "tabinsg.tb_modsup_20"
      ckTb_modsup_19.NTSText.NTSDbField = "tabinsg.tb_modsup_19"
      ckTb_modsup_18.NTSText.NTSDbField = "tabinsg.tb_modsup_18"
      ckTb_modsup_17.NTSText.NTSDbField = "tabinsg.tb_modsup_17"
      ckTb_modsup_16.NTSText.NTSDbField = "tabinsg.tb_modsup_16"
      ckTb_modsup_15.NTSText.NTSDbField = "tabinsg.tb_modsup_15"
      ckTb_modsup_14.NTSText.NTSDbField = "tabinsg.tb_modsup_14"
      ckTb_modsup_13.NTSText.NTSDbField = "tabinsg.tb_modsup_13"
      ckTb_modsup_12.NTSText.NTSDbField = "tabinsg.tb_modsup_12"
      ckTb_modsup_11.NTSText.NTSDbField = "tabinsg.tb_modsup_11"
      ckTb_modsup_10.NTSText.NTSDbField = "tabinsg.tb_modsup_10"
      ckTb_modsup_9.NTSText.NTSDbField = "tabinsg.tb_modsup_9"
      ckTb_modsup_8.NTSText.NTSDbField = "tabinsg.tb_modsup_8"
      ckTb_modsup_7.NTSText.NTSDbField = "tabinsg.tb_modsup_7"
      ckTb_modsup_6.NTSText.NTSDbField = "tabinsg.tb_modsup_6"
      ckTb_modsup_5.NTSText.NTSDbField = "tabinsg.tb_modsup_5"
      ckTb_modsup_4.NTSText.NTSDbField = "tabinsg.tb_modsup_4"
      ckTb_modsup_3.NTSText.NTSDbField = "tabinsg.tb_modsup_3"
      ckTb_modsup_2.NTSText.NTSDbField = "tabinsg.tb_modsup_2"
      ckTb_modsup_1.NTSText.NTSDbField = "tabinsg.tb_modsup_1"
      ckTb_modsupext_30.NTSText.NTSDbField = "tabinsg.tb_modsupext_30"
      ckTb_modsupext_29.NTSText.NTSDbField = "tabinsg.tb_modsupext_29"
      ckTb_modsupext_28.NTSText.NTSDbField = "tabinsg.tb_modsupext_28"
      ckTb_modsupext_27.NTSText.NTSDbField = "tabinsg.tb_modsupext_27"
      ckTb_modsupext_26.NTSText.NTSDbField = "tabinsg.tb_modsupext_26"
      ckTb_modsupext_25.NTSText.NTSDbField = "tabinsg.tb_modsupext_25"
      ckTb_modsupext_24.NTSText.NTSDbField = "tabinsg.tb_modsupext_24"
      ckTb_modsupext_23.NTSText.NTSDbField = "tabinsg.tb_modsupext_23"
      ckTb_modsupext_22.NTSText.NTSDbField = "tabinsg.tb_modsupext_22"
      ckTb_modsupext_21.NTSText.NTSDbField = "tabinsg.tb_modsupext_21"
      ckTb_modsupext_20.NTSText.NTSDbField = "tabinsg.tb_modsupext_20"
      ckTb_modsupext_19.NTSText.NTSDbField = "tabinsg.tb_modsupext_19"
      ckTb_modsupext_18.NTSText.NTSDbField = "tabinsg.tb_modsupext_18"
      ckTb_modsupext_17.NTSText.NTSDbField = "tabinsg.tb_modsupext_17"
      ckTb_modsupext_16.NTSText.NTSDbField = "tabinsg.tb_modsupext_16"
      ckTb_modsupext_15.NTSText.NTSDbField = "tabinsg.tb_modsupext_15"
      ckTb_modsupext_14.NTSText.NTSDbField = "tabinsg.tb_modsupext_14"
      ckTb_modsupext_13.NTSText.NTSDbField = "tabinsg.tb_modsupext_13"
      ckTb_modsupext_12.NTSText.NTSDbField = "tabinsg.tb_modsupext_12"
      ckTb_modsupext_11.NTSText.NTSDbField = "tabinsg.tb_modsupext_11"
      ckTb_modsupext_10.NTSText.NTSDbField = "tabinsg.tb_modsupext_10"
      ckTb_modsupext_9.NTSText.NTSDbField = "tabinsg.tb_modsupext_9"
      ckTb_modsupext_8.NTSText.NTSDbField = "tabinsg.tb_modsupext_8"
      ckTb_modsupext_7.NTSText.NTSDbField = "tabinsg.tb_modsupext_7"
      ckTb_modsupext_6.NTSText.NTSDbField = "tabinsg.tb_modsupext_6"
      ckTb_modsupext_5.NTSText.NTSDbField = "tabinsg.tb_modsupext_5"
      ckTb_modsupext_4.NTSText.NTSDbField = "tabinsg.tb_modsupext_4"
      ckTb_modsupext_3.NTSText.NTSDbField = "tabinsg.tb_modsupext_3"
      ckTb_modsupext_2.NTSText.NTSDbField = "tabinsg.tb_modsupext_2"
      ckTb_modsupext_1.NTSText.NTSDbField = "tabinsg.tb_modsupext_1"
      ckTb_modptn_30.NTSText.NTSDbField = "tabinsg.tb_modptn_30"
      ckTb_modptn_29.NTSText.NTSDbField = "tabinsg.tb_modptn_29"
      ckTb_modptn_28.NTSText.NTSDbField = "tabinsg.tb_modptn_28"
      ckTb_modptn_27.NTSText.NTSDbField = "tabinsg.tb_modptn_27"
      ckTb_modptn_26.NTSText.NTSDbField = "tabinsg.tb_modptn_26"
      ckTb_modptn_25.NTSText.NTSDbField = "tabinsg.tb_modptn_25"
      ckTb_modptn_24.NTSText.NTSDbField = "tabinsg.tb_modptn_24"
      ckTb_modptn_23.NTSText.NTSDbField = "tabinsg.tb_modptn_23"
      ckTb_modptn_22.NTSText.NTSDbField = "tabinsg.tb_modptn_22"
      ckTb_modptn_21.NTSText.NTSDbField = "tabinsg.tb_modptn_21"
      ckTb_modptn_20.NTSText.NTSDbField = "tabinsg.tb_modptn_20"
      ckTb_modptn_19.NTSText.NTSDbField = "tabinsg.tb_modptn_19"
      ckTb_modptn_18.NTSText.NTSDbField = "tabinsg.tb_modptn_18"
      ckTb_modptn_17.NTSText.NTSDbField = "tabinsg.tb_modptn_17"
      ckTb_modptn_16.NTSText.NTSDbField = "tabinsg.tb_modptn_16"
      ckTb_modptn_15.NTSText.NTSDbField = "tabinsg.tb_modptn_15"
      ckTb_modptn_14.NTSText.NTSDbField = "tabinsg.tb_modptn_14"
      ckTb_modptn_13.NTSText.NTSDbField = "tabinsg.tb_modptn_13"
      ckTb_modptn_12.NTSText.NTSDbField = "tabinsg.tb_modptn_12"
      ckTb_modptn_11.NTSText.NTSDbField = "tabinsg.tb_modptn_11"
      ckTb_modptn_10.NTSText.NTSDbField = "tabinsg.tb_modptn_10"
      ckTb_modptn_9.NTSText.NTSDbField = "tabinsg.tb_modptn_9"
      ckTb_modptn_8.NTSText.NTSDbField = "tabinsg.tb_modptn_8"
      ckTb_modptn_7.NTSText.NTSDbField = "tabinsg.tb_modptn_7"
      ckTb_modptn_6.NTSText.NTSDbField = "tabinsg.tb_modptn_6"
      ckTb_modptn_5.NTSText.NTSDbField = "tabinsg.tb_modptn_5"
      ckTb_modptn_4.NTSText.NTSDbField = "tabinsg.tb_modptn_4"
      ckTb_modptn_3.NTSText.NTSDbField = "tabinsg.tb_modptn_3"
      ckTb_modptn_2.NTSText.NTSDbField = "tabinsg.tb_modptn_2"
      ckTb_modptn_1.NTSText.NTSDbField = "tabinsg.tb_modptn_1"
      ckTb_modptnext_30.NTSText.NTSDbField = "tabinsg.tb_modptnext_30"
      ckTb_modptnext_29.NTSText.NTSDbField = "tabinsg.tb_modptnext_29"
      ckTb_modptnext_28.NTSText.NTSDbField = "tabinsg.tb_modptnext_28"
      ckTb_modptnext_27.NTSText.NTSDbField = "tabinsg.tb_modptnext_27"
      ckTb_modptnext_26.NTSText.NTSDbField = "tabinsg.tb_modptnext_26"
      ckTb_modptnext_25.NTSText.NTSDbField = "tabinsg.tb_modptnext_25"
      ckTb_modptnext_24.NTSText.NTSDbField = "tabinsg.tb_modptnext_24"
      ckTb_modptnext_23.NTSText.NTSDbField = "tabinsg.tb_modptnext_23"
      ckTb_modptnext_22.NTSText.NTSDbField = "tabinsg.tb_modptnext_22"
      ckTb_modptnext_21.NTSText.NTSDbField = "tabinsg.tb_modptnext_21"
      ckTb_modptnext_20.NTSText.NTSDbField = "tabinsg.tb_modptnext_20"
      ckTb_modptnext_19.NTSText.NTSDbField = "tabinsg.tb_modptnext_19"
      ckTb_modptnext_18.NTSText.NTSDbField = "tabinsg.tb_modptnext_18"
      ckTb_modptnext_17.NTSText.NTSDbField = "tabinsg.tb_modptnext_17"
      ckTb_modptnext_16.NTSText.NTSDbField = "tabinsg.tb_modptnext_16"
      ckTb_modptnext_15.NTSText.NTSDbField = "tabinsg.tb_modptnext_15"
      ckTb_modptnext_14.NTSText.NTSDbField = "tabinsg.tb_modptnext_14"
      ckTb_modptnext_13.NTSText.NTSDbField = "tabinsg.tb_modptnext_13"
      ckTb_modptnext_12.NTSText.NTSDbField = "tabinsg.tb_modptnext_12"
      ckTb_modptnext_11.NTSText.NTSDbField = "tabinsg.tb_modptnext_11"
      ckTb_modptnext_10.NTSText.NTSDbField = "tabinsg.tb_modptnext_10"
      ckTb_modptnext_9.NTSText.NTSDbField = "tabinsg.tb_modptnext_9"
      ckTb_modptnext_8.NTSText.NTSDbField = "tabinsg.tb_modptnext_8"
      ckTb_modptnext_7.NTSText.NTSDbField = "tabinsg.tb_modptnext_7"
      ckTb_modptnext_6.NTSText.NTSDbField = "tabinsg.tb_modptnext_6"
      ckTb_modptnext_5.NTSText.NTSDbField = "tabinsg.tb_modptnext_5"
      ckTb_modptnext_4.NTSText.NTSDbField = "tabinsg.tb_modptnext_4"
      ckTb_modptnext_3.NTSText.NTSDbField = "tabinsg.tb_modptnext_3"
      ckTb_modptnext_2.NTSText.NTSDbField = "tabinsg.tb_modptnext_2"
      ckTb_modptnext_1.NTSText.NTSDbField = "tabinsg.tb_modptnext_1"
      edTb_seriemaxlen.NTSDbField = "tabinsg.tb_seriemaxlen"
      edTb_maxlencodart.NTSDbField = "tabinsg.tb_maxlencodart"
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcInsg, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()

    Try
      Dim dttValuta As New DataTable()

      dttValuta.Columns.Add("cod", GetType(String))
      dttValuta.Columns.Add("val", GetType(String))
      dttValuta.Rows.Add(New Object() {" ", "(indefinita)"})
      dttValuta.Rows.Add(New Object() {"E", "EUR Euro"})
      dttValuta.Rows.Add(New Object() {"L", "LIT Lire italiane"})
      dttValuta.Rows.Add(New Object() {"C", "CHF Franco svizzero"})
      dttValuta.Rows.Add(New Object() {"S", "GBP Lira sterlina"})
      dttValuta.Rows.Add(New Object() {"Y", "JPY Yen giapponese"})
      dttValuta.Rows.Add(New Object() {"U", "USD Dollaro U.S.A."})
      dttValuta.Rows.Add(New Object() {"A", "Altra valuta"})
      dttValuta.AcceptChanges()

      cbTb_opz_1.DataSource = dttValuta
      cbTb_opz_1.ValueMember = "cod"
      cbTb_opz_1.DisplayMember = "val"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ColoraCheckBox()
    Dim i As Integer
    Dim strModuli As String = ""
    Dim bModulo As Boolean
    Dim strNomemod As String
    Dim strRiemp As String
    Try
      '-----------------------------------------------------------------------------------------
      '--- Colora di rosso le descrizioni dei CheckBox selezionati
      '--- che NON sono, però, attivi nella chiave di attivazione
      '-----------------------------------------------------------------------------------------
      'col1 = Color.Red
      'col2 = Color.Black
      If bInLoad Then Return

      strRiemp = " *"

      oCleInsg.GetModuliAcq(strModuli)

      For i = 0 To Len(strModuli) - 1
        If (UCase(Mid(strModuli, i + 1, 1)) = "N") Then
          bModulo = True
        Else
          bModulo = False
        End If

        strNomemod = oCleInsg.OttieniDesModuloAcq(i + 1)

        Select Case i
          Case 0 : If ckTb_mod_1.Checked And bModulo Then ckTb_mod_1.Text = strNomemod & strRiemp Else ckTb_mod_1.Text = strNomemod
          Case 1 : If ckTb_mod_2.Checked And bModulo Then ckTb_mod_2.Text = strNomemod & strRiemp Else ckTb_mod_2.Text = strNomemod
          Case 2 : If ckTb_mod_3.Checked And bModulo Then ckTb_mod_3.Text = strNomemod & strRiemp Else ckTb_mod_3.Text = strNomemod
          Case 3 : If ckTb_mod_4.Checked And bModulo Then ckTb_mod_4.Text = strNomemod & strRiemp Else ckTb_mod_4.Text = strNomemod
          Case 4 : If ckTb_mod_5.Checked And bModulo Then ckTb_mod_5.Text = strNomemod & strRiemp Else ckTb_mod_5.Text = strNomemod
          Case 5 : If ckTb_mod_6.Checked And bModulo Then ckTb_mod_6.Text = strNomemod & strRiemp Else ckTb_mod_6.Text = strNomemod
          Case 6 : If ckTb_mod_7.Checked And bModulo Then ckTb_mod_7.Text = strNomemod & strRiemp Else ckTb_mod_7.Text = strNomemod
          Case 7 : If ckTb_mod_8.Checked And bModulo Then ckTb_mod_8.Text = strNomemod & strRiemp Else ckTb_mod_8.Text = strNomemod
          Case 8 : If ckTb_mod_9.Checked And bModulo Then ckTb_mod_9.Text = strNomemod & strRiemp Else ckTb_mod_9.Text = strNomemod
          Case 9 : If ckTb_mod_10.Checked And bModulo Then ckTb_mod_10.Text = strNomemod & strRiemp Else ckTb_mod_10.Text = strNomemod
          Case 10 : If ckTb_mod_11.Checked And bModulo Then ckTb_mod_11.Text = strNomemod & strRiemp Else ckTb_mod_11.Text = strNomemod
          Case 11 : If ckTb_mod_12.Checked And bModulo Then ckTb_mod_12.Text = strNomemod & strRiemp Else ckTb_mod_12.Text = strNomemod
          Case 12 : If ckTb_mod_13.Checked And bModulo Then ckTb_mod_13.Text = strNomemod & strRiemp Else ckTb_mod_13.Text = strNomemod
          Case 13 : If ckTb_mod_14.Checked And bModulo Then ckTb_mod_14.Text = strNomemod & strRiemp Else ckTb_mod_14.Text = strNomemod
          Case 14 : If ckTb_mod_15.Checked And bModulo Then ckTb_mod_15.Text = strNomemod & strRiemp Else ckTb_mod_15.Text = strNomemod

          Case 15 : If ckTb_mod2_1.Checked And bModulo Then ckTb_mod2_1.Text = strNomemod & strRiemp Else ckTb_mod2_1.Text = strNomemod
          Case 16 : If ckTb_mod2_2.Checked And bModulo Then ckTb_mod2_2.Text = strNomemod & strRiemp Else ckTb_mod2_2.Text = strNomemod
          Case 17 : If ckTb_mod2_3.Checked And bModulo Then ckTb_mod2_3.Text = strNomemod & strRiemp Else ckTb_mod2_3.Text = strNomemod
          Case 18 : If ckTb_mod2_4.Checked And bModulo Then ckTb_mod2_4.Text = strNomemod & strRiemp Else ckTb_mod2_4.Text = strNomemod
          Case 19 : If ckTb_mod2_5.Checked And bModulo Then ckTb_mod2_5.Text = strNomemod & strRiemp Else ckTb_mod2_5.Text = strNomemod
          Case 20 : If ckTb_mod2_6.Checked And bModulo Then ckTb_mod2_6.Text = strNomemod & strRiemp Else ckTb_mod2_6.Text = strNomemod
          Case 21 : If ckTb_mod2_7.Checked And bModulo Then ckTb_mod2_7.Text = strNomemod & strRiemp Else ckTb_mod2_7.Text = strNomemod
          Case 22 : If ckTb_mod2_8.Checked And bModulo Then ckTb_mod2_8.Text = strNomemod & strRiemp Else ckTb_mod2_8.Text = strNomemod
          Case 23 : If ckTb_mod2_9.Checked And bModulo Then ckTb_mod2_9.Text = strNomemod & strRiemp Else ckTb_mod2_9.Text = strNomemod
          Case 24 : If ckTb_mod2_10.Checked And bModulo Then ckTb_mod2_10.Text = strNomemod & strRiemp Else ckTb_mod2_10.Text = strNomemod
          Case 25 : If ckTb_mod2_11.Checked And bModulo Then ckTb_mod2_11.Text = strNomemod & strRiemp Else ckTb_mod2_11.Text = strNomemod
          Case 26 : If ckTb_mod2_12.Checked And bModulo Then ckTb_mod2_12.Text = strNomemod & strRiemp Else ckTb_mod2_12.Text = strNomemod
          Case 27 : If ckTb_mod2_13.Checked And bModulo Then ckTb_mod2_13.Text = strNomemod & strRiemp Else ckTb_mod2_13.Text = strNomemod
          Case 28 : If ckTb_mod2_14.Checked And bModulo Then ckTb_mod2_14.Text = strNomemod & strRiemp Else ckTb_mod2_14.Text = strNomemod
          Case 29 : If ckTb_mod2_15.Checked And bModulo Then ckTb_mod2_15.Text = strNomemod & strRiemp Else ckTb_mod2_15.Text = strNomemod

          Case 30 : If ckTb_mod3_1.Checked And bModulo Then ckTb_mod3_1.Text = strNomemod & strRiemp Else ckTb_mod3_1.Text = strNomemod
          Case 31 : If ckTb_mod3_2.Checked And bModulo Then ckTb_mod3_2.Text = strNomemod & strRiemp Else ckTb_mod3_2.Text = strNomemod
          Case 32 : If ckTb_mod3_3.Checked And bModulo Then ckTb_mod3_3.Text = strNomemod & strRiemp Else ckTb_mod3_3.Text = strNomemod
          Case 33 : If ckTb_mod3_4.Checked And bModulo Then ckTb_mod3_4.Text = strNomemod & strRiemp Else ckTb_mod3_4.Text = strNomemod
          Case 34 : If ckTb_mod3_5.Checked And bModulo Then ckTb_mod3_5.Text = strNomemod & strRiemp Else ckTb_mod3_5.Text = strNomemod
          Case 35 : If ckTb_mod3_6.Checked And bModulo Then ckTb_mod3_6.Text = strNomemod & strRiemp Else ckTb_mod3_6.Text = strNomemod
          Case 36 : If ckTb_mod3_7.Checked And bModulo Then ckTb_mod3_7.Text = strNomemod & strRiemp Else ckTb_mod3_7.Text = strNomemod
          Case 37 : If ckTb_mod3_8.Checked And bModulo Then ckTb_mod3_8.Text = strNomemod & strRiemp Else ckTb_mod3_8.Text = strNomemod
          Case 38 : If ckTb_mod3_9.Checked And bModulo Then ckTb_mod3_9.Text = strNomemod & strRiemp Else ckTb_mod3_9.Text = strNomemod
          Case 39 : If ckTb_mod3_10.Checked And bModulo Then ckTb_mod3_10.Text = strNomemod & strRiemp Else ckTb_mod3_10.Text = strNomemod
          Case 40 : If ckTb_mod3_11.Checked And bModulo Then ckTb_mod3_11.Text = strNomemod & strRiemp Else ckTb_mod3_11.Text = strNomemod
          Case 41 : If ckTb_mod3_12.Checked And bModulo Then ckTb_mod3_12.Text = strNomemod & strRiemp Else ckTb_mod3_12.Text = strNomemod
          Case 42 : If ckTb_mod3_13.Checked And bModulo Then ckTb_mod3_13.Text = strNomemod & strRiemp Else ckTb_mod3_13.Text = strNomemod
          Case 43 : If ckTb_mod3_14.Checked And bModulo Then ckTb_mod3_14.Text = strNomemod & strRiemp Else ckTb_mod3_14.Text = strNomemod
          Case 44 : If ckTb_mod3_15.Checked And bModulo Then ckTb_mod3_15.Text = strNomemod & strRiemp Else ckTb_mod3_15.Text = strNomemod

          Case 45 : If ckTb_mod4_1.Checked And bModulo Then ckTb_mod4_1.Text = strNomemod & strRiemp Else ckTb_mod4_1.Text = strNomemod
          Case 46 : If ckTb_mod4_2.Checked And bModulo Then ckTb_mod4_2.Text = strNomemod & strRiemp Else ckTb_mod4_2.Text = strNomemod
          Case 47 : If ckTb_mod4_3.Checked And bModulo Then ckTb_mod4_3.Text = strNomemod & strRiemp Else ckTb_mod4_3.Text = strNomemod
          Case 48 : If ckTb_mod4_4.Checked And bModulo Then ckTb_mod4_4.Text = strNomemod & strRiemp Else ckTb_mod4_4.Text = strNomemod
          Case 49 : If ckTb_mod4_5.Checked And bModulo Then ckTb_mod4_5.Text = strNomemod & strRiemp Else ckTb_mod4_5.Text = strNomemod
          Case 50 : If ckTb_mod4_6.Checked And bModulo Then ckTb_mod4_6.Text = strNomemod & strRiemp Else ckTb_mod4_6.Text = strNomemod
          Case 51 : If ckTb_mod4_7.Checked And bModulo Then ckTb_mod4_7.Text = strNomemod & strRiemp Else ckTb_mod4_7.Text = strNomemod
          Case 52 : If ckTb_mod4_8.Checked And bModulo Then ckTb_mod4_8.Text = strNomemod & strRiemp Else ckTb_mod4_8.Text = strNomemod
          Case 53 : If ckTb_mod4_9.Checked And bModulo Then ckTb_mod4_9.Text = strNomemod & strRiemp Else ckTb_mod4_9.Text = strNomemod
          Case 54 : If ckTb_mod4_10.Checked And bModulo Then ckTb_mod4_10.Text = strNomemod & strRiemp Else ckTb_mod4_10.Text = strNomemod
          Case 55 : If ckTb_mod4_11.Checked And bModulo Then ckTb_mod4_11.Text = strNomemod & strRiemp Else ckTb_mod4_11.Text = strNomemod
          Case 56 : If ckTb_mod4_12.Checked And bModulo Then ckTb_mod4_12.Text = strNomemod & strRiemp Else ckTb_mod4_12.Text = strNomemod
          Case 57 : If ckTb_mod4_13.Checked And bModulo Then ckTb_mod4_13.Text = strNomemod & strRiemp Else ckTb_mod4_13.Text = strNomemod
          Case 58 : If ckTb_mod4_14.Checked And bModulo Then ckTb_mod4_14.Text = strNomemod & strRiemp Else ckTb_mod4_14.Text = strNomemod
          Case 59 : If ckTb_mod4_15.Checked And bModulo Then ckTb_mod4_15.Text = strNomemod & strRiemp Else ckTb_mod4_15.Text = strNomemod

          Case 60 : If ckTb_modsup_1.Checked And bModulo Then ckTb_modsup_1.Text = strNomemod & strRiemp Else ckTb_modsup_1.Text = strNomemod
          Case 61 : If ckTb_modsup_2.Checked And bModulo Then ckTb_modsup_2.Text = strNomemod & strRiemp Else ckTb_modsup_2.Text = strNomemod
          Case 62 : If ckTb_modsup_3.Checked And bModulo Then ckTb_modsup_3.Text = strNomemod & strRiemp Else ckTb_modsup_3.Text = strNomemod
          Case 63 : If ckTb_modsup_4.Checked And bModulo Then ckTb_modsup_4.Text = strNomemod & strRiemp Else ckTb_modsup_4.Text = strNomemod
          Case 64 : If ckTb_modsup_5.Checked And bModulo Then ckTb_modsup_5.Text = strNomemod & strRiemp Else ckTb_modsup_5.Text = strNomemod
          Case 65 : If ckTb_modsup_6.Checked And bModulo Then ckTb_modsup_6.Text = strNomemod & strRiemp Else ckTb_modsup_6.Text = strNomemod
          Case 66 : If ckTb_modsup_7.Checked And bModulo Then ckTb_modsup_7.Text = strNomemod & strRiemp Else ckTb_modsup_7.Text = strNomemod
          Case 67 : If ckTb_modsup_8.Checked And bModulo Then ckTb_modsup_8.Text = strNomemod & strRiemp Else ckTb_modsup_8.Text = strNomemod
          Case 68 : If ckTb_modsup_9.Checked And bModulo Then ckTb_modsup_9.Text = strNomemod & strRiemp Else ckTb_modsup_9.Text = strNomemod
          Case 69 : If ckTb_modsup_10.Checked And bModulo Then ckTb_modsup_10.Text = strNomemod & strRiemp Else ckTb_modsup_10.Text = strNomemod
          Case 70 : If ckTb_modsup_11.Checked And bModulo Then ckTb_modsup_11.Text = strNomemod & strRiemp Else ckTb_modsup_11.Text = strNomemod
          Case 71 : If ckTb_modsup_12.Checked And bModulo Then ckTb_modsup_12.Text = strNomemod & strRiemp Else ckTb_modsup_12.Text = strNomemod
          Case 72 : If ckTb_modsup_13.Checked And bModulo Then ckTb_modsup_13.Text = strNomemod & strRiemp Else ckTb_modsup_13.Text = strNomemod
          Case 73 : If ckTb_modsup_14.Checked And bModulo Then ckTb_modsup_14.Text = strNomemod & strRiemp Else ckTb_modsup_14.Text = strNomemod
          Case 74 : If ckTb_modsup_15.Checked And bModulo Then ckTb_modsup_15.Text = strNomemod & strRiemp Else ckTb_modsup_15.Text = strNomemod
          Case 75 : If ckTb_modsup_16.Checked And bModulo Then ckTb_modsup_16.Text = strNomemod & strRiemp Else ckTb_modsup_16.Text = strNomemod
          Case 76 : If ckTb_modsup_17.Checked And bModulo Then ckTb_modsup_17.Text = strNomemod & strRiemp Else ckTb_modsup_17.Text = strNomemod
          Case 77 : If ckTb_modsup_18.Checked And bModulo Then ckTb_modsup_18.Text = strNomemod & strRiemp Else ckTb_modsup_18.Text = strNomemod
          Case 78 : If ckTb_modsup_19.Checked And bModulo Then ckTb_modsup_19.Text = strNomemod & strRiemp Else ckTb_modsup_19.Text = strNomemod
          Case 79 : If ckTb_modsup_20.Checked And bModulo Then ckTb_modsup_20.Text = strNomemod & strRiemp Else ckTb_modsup_20.Text = strNomemod
          Case 80 : If ckTb_modsup_21.Checked And bModulo Then ckTb_modsup_21.Text = strNomemod & strRiemp Else ckTb_modsup_21.Text = strNomemod
          Case 81 : If ckTb_modsup_22.Checked And bModulo Then ckTb_modsup_22.Text = strNomemod & strRiemp Else ckTb_modsup_22.Text = strNomemod
          Case 82 : If ckTb_modsup_23.Checked And bModulo Then ckTb_modsup_23.Text = strNomemod & strRiemp Else ckTb_modsup_23.Text = strNomemod
          Case 83 : If ckTb_modsup_24.Checked And bModulo Then ckTb_modsup_24.Text = strNomemod & strRiemp Else ckTb_modsup_24.Text = strNomemod
          Case 84 : If ckTb_modsup_25.Checked And bModulo Then ckTb_modsup_25.Text = strNomemod & strRiemp Else ckTb_modsup_25.Text = strNomemod
          Case 85 : If ckTb_modsup_26.Checked And bModulo Then ckTb_modsup_26.Text = strNomemod & strRiemp Else ckTb_modsup_26.Text = strNomemod
          Case 86 : If ckTb_modsup_27.Checked And bModulo Then ckTb_modsup_27.Text = strNomemod & strRiemp Else ckTb_modsup_27.Text = strNomemod
          Case 87 : If ckTb_modsup_28.Checked And bModulo Then ckTb_modsup_28.Text = strNomemod & strRiemp Else ckTb_modsup_28.Text = strNomemod
          Case 88 : If ckTb_modsup_29.Checked And bModulo Then ckTb_modsup_29.Text = strNomemod & strRiemp Else ckTb_modsup_29.Text = strNomemod
          Case 89 : If ckTb_modsup_30.Checked And bModulo Then ckTb_modsup_30.Text = strNomemod & strRiemp Else ckTb_modsup_30.Text = strNomemod

          Case 90 : If ckTb_modsupext_1.Checked And bModulo Then ckTb_modsupext_1.Text = strNomemod & strRiemp Else ckTb_modsupext_1.Text = strNomemod
          Case 91 : If ckTb_modsupext_2.Checked And bModulo Then ckTb_modsupext_2.Text = strNomemod & strRiemp Else ckTb_modsupext_2.Text = strNomemod
          Case 92 : If ckTb_modsupext_3.Checked And bModulo Then ckTb_modsupext_3.Text = strNomemod & strRiemp Else ckTb_modsupext_3.Text = strNomemod
          Case 93 : If ckTb_modsupext_4.Checked And bModulo Then ckTb_modsupext_4.Text = strNomemod & strRiemp Else ckTb_modsupext_4.Text = strNomemod
          Case 94 : If ckTb_modsupext_5.Checked And bModulo Then ckTb_modsupext_5.Text = strNomemod & strRiemp Else ckTb_modsupext_5.Text = strNomemod
          Case 95 : If ckTb_modsupext_6.Checked And bModulo Then ckTb_modsupext_6.Text = strNomemod & strRiemp Else ckTb_modsupext_6.Text = strNomemod
          Case 96 : If ckTb_modsupext_7.Checked And bModulo Then ckTb_modsupext_7.Text = strNomemod & strRiemp Else ckTb_modsupext_7.Text = strNomemod
          Case 97 : If ckTb_modsupext_8.Checked And bModulo Then ckTb_modsupext_8.Text = strNomemod & strRiemp Else ckTb_modsupext_8.Text = strNomemod
          Case 98 : If ckTb_modsupext_9.Checked And bModulo Then ckTb_modsupext_9.Text = strNomemod & strRiemp Else ckTb_modsupext_9.Text = strNomemod
          Case 99 : If ckTb_modsupext_10.Checked And bModulo Then ckTb_modsupext_10.Text = strNomemod & strRiemp Else ckTb_modsupext_10.Text = strNomemod
          Case 100 : If ckTb_modsupext_11.Checked And bModulo Then ckTb_modsupext_11.Text = strNomemod & strRiemp Else ckTb_modsupext_11.Text = strNomemod
          Case 101 : If ckTb_modsupext_12.Checked And bModulo Then ckTb_modsupext_12.Text = strNomemod & strRiemp Else ckTb_modsupext_12.Text = strNomemod
          Case 102 : If ckTb_modsupext_13.Checked And bModulo Then ckTb_modsupext_13.Text = strNomemod & strRiemp Else ckTb_modsupext_13.Text = strNomemod
          Case 103 : If ckTb_modsupext_14.Checked And bModulo Then ckTb_modsupext_14.Text = strNomemod & strRiemp Else ckTb_modsupext_14.Text = strNomemod
          Case 104 : If ckTb_modsupext_15.Checked And bModulo Then ckTb_modsupext_15.Text = strNomemod & strRiemp Else ckTb_modsupext_15.Text = strNomemod
          Case 105 : If ckTb_modsupext_16.Checked And bModulo Then ckTb_modsupext_16.Text = strNomemod & strRiemp Else ckTb_modsupext_16.Text = strNomemod
          Case 106 : If ckTb_modsupext_17.Checked And bModulo Then ckTb_modsupext_17.Text = strNomemod & strRiemp Else ckTb_modsupext_17.Text = strNomemod
          Case 107 : If ckTb_modsupext_18.Checked And bModulo Then ckTb_modsupext_18.Text = strNomemod & strRiemp Else ckTb_modsupext_18.Text = strNomemod
          Case 108 : If ckTb_modsupext_19.Checked And bModulo Then ckTb_modsupext_19.Text = strNomemod & strRiemp Else ckTb_modsupext_19.Text = strNomemod
          Case 109 : If ckTb_modsupext_20.Checked And bModulo Then ckTb_modsupext_20.Text = strNomemod & strRiemp Else ckTb_modsupext_20.Text = strNomemod
          Case 110 : If ckTb_modsupext_21.Checked And bModulo Then ckTb_modsupext_21.Text = strNomemod & strRiemp Else ckTb_modsupext_21.Text = strNomemod
          Case 111 : If ckTb_modsupext_22.Checked And bModulo Then ckTb_modsupext_22.Text = strNomemod & strRiemp Else ckTb_modsupext_22.Text = strNomemod
          Case 112 : If ckTb_modsupext_23.Checked And bModulo Then ckTb_modsupext_23.Text = strNomemod & strRiemp Else ckTb_modsupext_23.Text = strNomemod
          Case 113 : If ckTb_modsupext_24.Checked And bModulo Then ckTb_modsupext_24.Text = strNomemod & strRiemp Else ckTb_modsupext_24.Text = strNomemod
          Case 114 : If ckTb_modsupext_25.Checked And bModulo Then ckTb_modsupext_25.Text = strNomemod & strRiemp Else ckTb_modsupext_25.Text = strNomemod
          Case 115 : If ckTb_modsupext_26.Checked And bModulo Then ckTb_modsupext_26.Text = strNomemod & strRiemp Else ckTb_modsupext_26.Text = strNomemod
          Case 116 : If ckTb_modsupext_27.Checked And bModulo Then ckTb_modsupext_27.Text = strNomemod & strRiemp Else ckTb_modsupext_27.Text = strNomemod
          Case 117 : If ckTb_modsupext_28.Checked And bModulo Then ckTb_modsupext_28.Text = strNomemod & strRiemp Else ckTb_modsupext_28.Text = strNomemod
          Case 118 : If ckTb_modsupext_29.Checked And bModulo Then ckTb_modsupext_29.Text = strNomemod & strRiemp Else ckTb_modsupext_29.Text = strNomemod
          Case 119 : If ckTb_modsupext_30.Checked And bModulo Then ckTb_modsupext_30.Text = strNomemod & strRiemp Else ckTb_modsupext_30.Text = strNomemod

          Case 120 : If ckTb_modptn_1.Checked And bModulo Then ckTb_modptn_1.Text = strNomemod & strRiemp Else ckTb_modptn_1.Text = strNomemod
          Case 121 : If ckTb_modptn_2.Checked And bModulo Then ckTb_modptn_2.Text = strNomemod & strRiemp Else ckTb_modptn_2.Text = strNomemod
          Case 122 : If ckTb_modptn_3.Checked And bModulo Then ckTb_modptn_3.Text = strNomemod & strRiemp Else ckTb_modptn_3.Text = strNomemod
          Case 123 : If ckTb_modptn_4.Checked And bModulo Then ckTb_modptn_4.Text = strNomemod & strRiemp Else ckTb_modptn_4.Text = strNomemod
          Case 124 : If ckTb_modptn_5.Checked And bModulo Then ckTb_modptn_5.Text = strNomemod & strRiemp Else ckTb_modptn_5.Text = strNomemod
          Case 125 : If ckTb_modptn_6.Checked And bModulo Then ckTb_modptn_6.Text = strNomemod & strRiemp Else ckTb_modptn_6.Text = strNomemod
          Case 126 : If ckTb_modptn_7.Checked And bModulo Then ckTb_modptn_7.Text = strNomemod & strRiemp Else ckTb_modptn_7.Text = strNomemod
          Case 127 : If ckTb_modptn_8.Checked And bModulo Then ckTb_modptn_8.Text = strNomemod & strRiemp Else ckTb_modptn_8.Text = strNomemod
          Case 128 : If ckTb_modptn_9.Checked And bModulo Then ckTb_modptn_9.Text = strNomemod & strRiemp Else ckTb_modptn_9.Text = strNomemod
          Case 129 : If ckTb_modptn_10.Checked And bModulo Then ckTb_modptn_10.Text = strNomemod & strRiemp Else ckTb_modptn_10.Text = strNomemod
          Case 130 : If ckTb_modptn_11.Checked And bModulo Then ckTb_modptn_11.Text = strNomemod & strRiemp Else ckTb_modptn_11.Text = strNomemod
          Case 131 : If ckTb_modptn_12.Checked And bModulo Then ckTb_modptn_12.Text = strNomemod & strRiemp Else ckTb_modptn_12.Text = strNomemod
          Case 132 : If ckTb_modptn_13.Checked And bModulo Then ckTb_modptn_13.Text = strNomemod & strRiemp Else ckTb_modptn_13.Text = strNomemod
          Case 133 : If ckTb_modptn_14.Checked And bModulo Then ckTb_modptn_14.Text = strNomemod & strRiemp Else ckTb_modptn_14.Text = strNomemod
          Case 134 : If ckTb_modptn_15.Checked And bModulo Then ckTb_modptn_15.Text = strNomemod & strRiemp Else ckTb_modptn_15.Text = strNomemod
          Case 135 : If ckTb_modptn_16.Checked And bModulo Then ckTb_modptn_16.Text = strNomemod & strRiemp Else ckTb_modptn_16.Text = strNomemod
          Case 136 : If ckTb_modptn_17.Checked And bModulo Then ckTb_modptn_17.Text = strNomemod & strRiemp Else ckTb_modptn_17.Text = strNomemod
          Case 137 : If ckTb_modptn_18.Checked And bModulo Then ckTb_modptn_18.Text = strNomemod & strRiemp Else ckTb_modptn_18.Text = strNomemod
          Case 138 : If ckTb_modptn_19.Checked And bModulo Then ckTb_modptn_19.Text = strNomemod & strRiemp Else ckTb_modptn_19.Text = strNomemod
          Case 139 : If ckTb_modptn_20.Checked And bModulo Then ckTb_modptn_20.Text = strNomemod & strRiemp Else ckTb_modptn_20.Text = strNomemod
          Case 140 : If ckTb_modptn_21.Checked And bModulo Then ckTb_modptn_21.Text = strNomemod & strRiemp Else ckTb_modptn_21.Text = strNomemod
          Case 141 : If ckTb_modptn_22.Checked And bModulo Then ckTb_modptn_22.Text = strNomemod & strRiemp Else ckTb_modptn_22.Text = strNomemod
          Case 142 : If ckTb_modptn_23.Checked And bModulo Then ckTb_modptn_23.Text = strNomemod & strRiemp Else ckTb_modptn_23.Text = strNomemod
          Case 143 : If ckTb_modptn_24.Checked And bModulo Then ckTb_modptn_24.Text = strNomemod & strRiemp Else ckTb_modptn_24.Text = strNomemod
          Case 144 : If ckTb_modptn_25.Checked And bModulo Then ckTb_modptn_25.Text = strNomemod & strRiemp Else ckTb_modptn_25.Text = strNomemod
          Case 145 : If ckTb_modptn_26.Checked And bModulo Then ckTb_modptn_26.Text = strNomemod & strRiemp Else ckTb_modptn_26.Text = strNomemod
          Case 146 : If ckTb_modptn_27.Checked And bModulo Then ckTb_modptn_27.Text = strNomemod & strRiemp Else ckTb_modptn_27.Text = strNomemod
          Case 147 : If ckTb_modptn_28.Checked And bModulo Then ckTb_modptn_28.Text = strNomemod & strRiemp Else ckTb_modptn_28.Text = strNomemod
          Case 148 : If ckTb_modptn_29.Checked And bModulo Then ckTb_modptn_29.Text = strNomemod & strRiemp Else ckTb_modptn_29.Text = strNomemod
          Case 149 : If ckTb_modptn_30.Checked And bModulo Then ckTb_modptn_30.Text = strNomemod & strRiemp Else ckTb_modptn_30.Text = strNomemod

          Case 150 : If ckTb_modptnext_1.Checked And bModulo Then ckTb_modptnext_1.Text = strNomemod & strRiemp Else ckTb_modptnext_1.Text = strNomemod
          Case 151 : If ckTb_modptnext_2.Checked And bModulo Then ckTb_modptnext_2.Text = strNomemod & strRiemp Else ckTb_modptnext_2.Text = strNomemod
          Case 152 : If ckTb_modptnext_3.Checked And bModulo Then ckTb_modptnext_3.Text = strNomemod & strRiemp Else ckTb_modptnext_3.Text = strNomemod
          Case 153 : If ckTb_modptnext_4.Checked And bModulo Then ckTb_modptnext_4.Text = strNomemod & strRiemp Else ckTb_modptnext_4.Text = strNomemod
          Case 154 : If ckTb_modptnext_5.Checked And bModulo Then ckTb_modptnext_5.Text = strNomemod & strRiemp Else ckTb_modptnext_5.Text = strNomemod
          Case 155 : If ckTb_modptnext_6.Checked And bModulo Then ckTb_modptnext_6.Text = strNomemod & strRiemp Else ckTb_modptnext_6.Text = strNomemod
          Case 156 : If ckTb_modptnext_7.Checked And bModulo Then ckTb_modptnext_7.Text = strNomemod & strRiemp Else ckTb_modptnext_7.Text = strNomemod
          Case 157 : If ckTb_modptnext_8.Checked And bModulo Then ckTb_modptnext_8.Text = strNomemod & strRiemp Else ckTb_modptnext_8.Text = strNomemod
          Case 158 : If ckTb_modptnext_9.Checked And bModulo Then ckTb_modptnext_9.Text = strNomemod & strRiemp Else ckTb_modptnext_9.Text = strNomemod
          Case 159 : If ckTb_modptnext_10.Checked And bModulo Then ckTb_modptnext_10.Text = strNomemod & strRiemp Else ckTb_modptnext_10.Text = strNomemod
          Case 160 : If ckTb_modptnext_11.Checked And bModulo Then ckTb_modptnext_11.Text = strNomemod & strRiemp Else ckTb_modptnext_11.Text = strNomemod
          Case 161 : If ckTb_modptnext_12.Checked And bModulo Then ckTb_modptnext_12.Text = strNomemod & strRiemp Else ckTb_modptnext_12.Text = strNomemod
          Case 162 : If ckTb_modptnext_13.Checked And bModulo Then ckTb_modptnext_13.Text = strNomemod & strRiemp Else ckTb_modptnext_13.Text = strNomemod
          Case 163 : If ckTb_modptnext_14.Checked And bModulo Then ckTb_modptnext_14.Text = strNomemod & strRiemp Else ckTb_modptnext_14.Text = strNomemod
          Case 164 : If ckTb_modptnext_15.Checked And bModulo Then ckTb_modptnext_15.Text = strNomemod & strRiemp Else ckTb_modptnext_15.Text = strNomemod
          Case 165 : If ckTb_modptnext_16.Checked And bModulo Then ckTb_modptnext_16.Text = strNomemod & strRiemp Else ckTb_modptnext_16.Text = strNomemod
          Case 166 : If ckTb_modptnext_17.Checked And bModulo Then ckTb_modptnext_17.Text = strNomemod & strRiemp Else ckTb_modptnext_17.Text = strNomemod
          Case 167 : If ckTb_modptnext_18.Checked And bModulo Then ckTb_modptnext_18.Text = strNomemod & strRiemp Else ckTb_modptnext_18.Text = strNomemod
          Case 168 : If ckTb_modptnext_19.Checked And bModulo Then ckTb_modptnext_19.Text = strNomemod & strRiemp Else ckTb_modptnext_19.Text = strNomemod
          Case 169 : If ckTb_modptnext_20.Checked And bModulo Then ckTb_modptnext_20.Text = strNomemod & strRiemp Else ckTb_modptnext_20.Text = strNomemod
          Case 170 : If ckTb_modptnext_21.Checked And bModulo Then ckTb_modptnext_21.Text = strNomemod & strRiemp Else ckTb_modptnext_21.Text = strNomemod
          Case 171 : If ckTb_modptnext_22.Checked And bModulo Then ckTb_modptnext_22.Text = strNomemod & strRiemp Else ckTb_modptnext_22.Text = strNomemod
          Case 172 : If ckTb_modptnext_23.Checked And bModulo Then ckTb_modptnext_23.Text = strNomemod & strRiemp Else ckTb_modptnext_23.Text = strNomemod
          Case 173 : If ckTb_modptnext_24.Checked And bModulo Then ckTb_modptnext_24.Text = strNomemod & strRiemp Else ckTb_modptnext_24.Text = strNomemod
          Case 174 : If ckTb_modptnext_25.Checked And bModulo Then ckTb_modptnext_25.Text = strNomemod & strRiemp Else ckTb_modptnext_25.Text = strNomemod
          Case 175 : If ckTb_modptnext_26.Checked And bModulo Then ckTb_modptnext_26.Text = strNomemod & strRiemp Else ckTb_modptnext_26.Text = strNomemod
          Case 176 : If ckTb_modptnext_27.Checked And bModulo Then ckTb_modptnext_27.Text = strNomemod & strRiemp Else ckTb_modptnext_27.Text = strNomemod
          Case 177 : If ckTb_modptnext_28.Checked And bModulo Then ckTb_modptnext_28.Text = strNomemod & strRiemp Else ckTb_modptnext_28.Text = strNomemod
          Case 178 : If ckTb_modptnext_29.Checked And bModulo Then ckTb_modptnext_29.Text = strNomemod & strRiemp Else ckTb_modptnext_29.Text = strNomemod
          Case 179 : If ckTb_modptnext_30.Checked And bModulo Then ckTb_modptnext_30.Text = strNomemod & strRiemp Else ckTb_modptnext_30.Text = strNomemod
        End Select
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    '-----------------------------------------------------------------------------------------
  End Sub

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Dim nOldCodartSize As Integer = 18
    Dim i As Integer = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------

      'controlli su flag ckSconGenex selezionato: può esserlo solo se sono soddisfatte le richieste sottostanti
      If ckSconGenex.Checked Then
        If ckSc3.Checked = True Or ckSc4.Checked = True Or ckSconClCliDaList.Checked = False Or _
           cbSc1.SelectedValue <> "S" Or cbSc2.SelectedValue <> "S" Or cbSc3.SelectedValue <> "S" Or cbSc4.SelectedValue <> "S" Or _
           cbSc5.SelectedValue <> "C" Or cbSc6.SelectedValue <> "C" Or _
           ckSconSpecNoCum.Checked = False Or _
           cbScPrior.SelectedValue <> "ABCDEF" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130431654866172051, _
                      "Il flag 'Gestisci sconti generici cliente con sconti generici articolo' può essere spuntato solo SE:" & vbCrLf & _
                      "- non è spuntata la gestione degli sconti per classi articolo (ovvero C, D)" & vbCrLf & _
                      "- gli sconti da 1 a 4 sono impostati su 'sostitutivo' e 5 e 6 su 'cumulativo'" & vbCrLf & _
                      "- è spuntato il flag 'Classe sconto cliente presa da numero listino'" & vbCrLf & _
                      "- è spuntato il flag 'Ignora altri sconti se trovato sconto specifico cliente/articolo'" & vbCrLf & _
                      "- la priorità sconti deve essere 'ABCDEF'"))
          Return False
        End If
      End If

      '------------------------------------------------------------------------------------------------------------
      If GctlControllaOutNotEqual() = False Then Return False
      '------------------------------------------------------------------------------------------------------------
      If edTb_seriemaxlen.Text = nSerieEsistente.ToString Then
        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 128300183335593354, "Salvo l'inizializzazione database azienda?"))
      Else
        dRes = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130372941942149735, "Attenzione!" & vbCrLf & _
          "Si sta variando la lunghezza della serie" & vbCrLf & _
          "   (da |" & nSerieEsistente.ToString & "| a |" & edTb_seriemaxlen.Text & "| caratteri)" & vbCrLf & _
          "e non sarà più possibile ritornare all'impostazione precedente una volta creati movimenti." & vbCrLf & _
          "Salvare l'inizializzazione database azienda?"))
      End If

      '------------------------------------------------------------------------------------------------------------
      If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
      If dRes = System.Windows.Forms.DialogResult.Yes Then

        'test su cambio dimensione cod. articolo
        If dsInsg.Tables("TABINSG").Rows.Count > 0 Then
          nOldCodartSize = NTSCInt(dsInsg.Tables("TABINSG").Rows(0)("tb_maxlencodart", DataRowVersion.Original))
        End If

        If NTSCInt(edTb_maxlencodart.Text) <> nOldCodartSize Then

          If NTSCInt(edTb_maxlencodart.Text) < nOldCodartSize Then
            'non posso restringere se ci sono articoli con lunghezza maggiore di quella che si vuole
            i = oCleInsg.GetMaxCodartLen
            If i > NTSCInt(edTb_maxlencodart.Text) Then
              oApp.MsgBoxErr(oApp.Tr(Me, 130547459499245277, "Attenzione. Sono presenti codici articolo o codici alternativi " & _
                                                             " articoli di lunghezza |" & i.ToString & _
                                                             "|. Impossibile salvare in quanto è stato dichiarato che la lunghezza " & _
                                                             " massima per il codice articolo deve essere di |" & edTb_maxlencodart.Text & "| caratteri"))
              Return False
            End If
          End If

          dRes = oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130547450994071564, "Attenzione!" & vbCrLf & _
          "Si sta variando la lunghezza del codice articolo da |" & nOldCodartSize.ToString & "| a |" & edTb_maxlencodart.Text & "| caratteri." & vbCrLf & _
          "Dovranno essere verificati da parte dell'installatore e, al bisogno, aggiornati i seguenti componenti personalizzati (se presenti):" & vbCrLf & _
          "- report (verifica database solo se il campo è stato dichiarato più grande), " & vbCrLf & _
          "- stored procedure (se contenenti variabili che trattano il cod. articolo ed il campo è stato allargato), " & vbCrLf & _
          "- campi di testo / colonne di griglia aggiunti in form che trattano il cod. articolo" & vbCrLf & _
          "- campi di database personalizzati che memorizzano il cod. articolo (se il campo è stato allargato)" & vbCrLf & vbCrLf & _
          "Salvare l'inizializzazione database azienda?"))
          If dRes = Windows.Forms.DialogResult.No Then Return False
        End If

        If Not oCleInsg.Salva(False) Then Return False
        nSerieEsistente = NTSCInt(edTb_seriemaxlen.Text)

        'campi letti da opzioni di registro di Business
        oApp.oGvar.strGestioneSconti = IIf(ckSconti.Checked, "S", "N").ToString
        oApp.oGvar.strTrattaScA = IIf(ckSc1.Checked, "S", "N").ToString
        oApp.oGvar.strTrattaScB = IIf(ckSc2.Checked, "S", "N").ToString
        oApp.oGvar.strTrattaScC = IIf(ckSc3.Checked, "S", "N").ToString
        oApp.oGvar.strTrattaScD = IIf(ckSc4.Checked, "S", "N").ToString
        oApp.oGvar.strTrattaScE = IIf(ckSc5.Checked, "S", "N").ToString
        oApp.oGvar.strTrattaScF = IIf(ckSc6.Checked, "S", "N").ToString
        oApp.oGvar.strSconGenex = IIf(ckSconGenex.Checked, "S", "N").ToString
        oApp.oGvar.strTrattaSc1 = cbSc1.SelectedValue
        oApp.oGvar.strTrattaSc2 = cbSc2.SelectedValue
        oApp.oGvar.strTrattaSc3 = cbSc3.SelectedValue
        oApp.oGvar.strTrattaSc4 = cbSc4.SelectedValue
        oApp.oGvar.strTrattaSc5 = cbSc5.SelectedValue
        oApp.oGvar.strTrattaSc6 = cbSc6.SelectedValue
        oApp.oGvar.strPriorSconti = cbScPrior.SelectedValue
        oApp.oGvar.strSconSpecNoCum = IIf(ckSconSpecNoCum.Checked, "S", "N").ToString
        oApp.oGvar.strSconClCliDaList = IIf(ckSconClCliDaList.Checked, "S", "N").ToString

        oMenu.SaveSettingBus("OPZIONI", ".", ".", "GestioneSconti", oApp.oGvar.strGestioneSconti, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "TrattaSc1", oApp.oGvar.strTrattaSc1, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "TrattaSc2", oApp.oGvar.strTrattaSc2, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "TrattaSc3", oApp.oGvar.strTrattaSc3, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "TrattaSc4", oApp.oGvar.strTrattaSc4, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "TrattaSc5", oApp.oGvar.strTrattaSc5, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "TrattaSc6", oApp.oGvar.strTrattaSc6, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "TrattaScA", oApp.oGvar.strTrattaScA, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "TrattaScB", oApp.oGvar.strTrattaScB, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "TrattaScC", oApp.oGvar.strTrattaScC, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "TrattaScD", oApp.oGvar.strTrattaScD, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "TrattaScE", oApp.oGvar.strTrattaScE, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "TrattaScF", oApp.oGvar.strTrattaScF, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "PriorSconti", oApp.oGvar.strPriorSconti, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "SconSpecNoCum", oApp.oGvar.strSconSpecNoCum, "", "N", "N", "N")
        oMenu.SaveSettingBus("OPZIONI", ".", ".", "SconClCliDaList", oApp.oGvar.strSconClCliDaList, "", "N", "N", "N")

        oMenu.SaveSettingBus("OPZIONI", ".", ".", "SconGenex", oApp.oGvar.strSconGenex, "", "N", "N", "N")
      End If
      If dRes = System.Windows.Forms.DialogResult.No Then tlbRipristina_ItemClick(Nothing, Nothing)

      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

End Class
