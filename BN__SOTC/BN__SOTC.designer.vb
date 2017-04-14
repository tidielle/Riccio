Partial Public Class FRM__SOTC
  Inherits FRM__CHIL

  <System.Diagnostics.DebuggerNonUserCode()> _
  Public Sub New()
    MyBase.New()
  End Sub

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

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
  Public WithEvents tlbCambiaDitta As NTSInformatica.NTSBarButtonItem
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents lbAn_contoLabel As NTSInformatica.NTSLabel
  Public WithEvents edAn_descr1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAn_descr2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_codmastLabel As NTSInformatica.NTSLabel
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents lbAn_codmast As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codmast As NTSInformatica.NTSLabel
  Public WithEvents lbAn_descr2 As NTSInformatica.NTSLabel
  Public WithEvents lbAn_descr1 As NTSInformatica.NTSLabel
  Public WithEvents lbAn_conto As NTSInformatica.NTSLabel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents pnMain As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents pnPag3 As NTSInformatica.NTSPanel
  Public WithEvents pnPag4 As NTSInformatica.NTSPanel
  Public WithEvents edAn_note2 As NTSInformatica.NTSMemoBox
  Public WithEvents pnPag2 As NTSInformatica.NTSPanel
  Public WithEvents lbAn_cosvend As NTSInformatica.NTSLabel
  Public WithEvents cbAn_cosvend As NTSInformatica.NTSComboBox
  Public WithEvents pnPag1 As NTSInformatica.NTSPanel
  Public WithEvents edAn_datini As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAn_datfin As NTSInformatica.NTSLabel
  Public WithEvents edAn_datfin As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAn_datini As NTSInformatica.NTSLabel
  Public WithEvents ckAn_flci As NTSInformatica.NTSCheckBox
  Public WithEvents ckAn_sosppr As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_funzion As NTSInformatica.NTSLabel
  Public WithEvents lbXx_controp As NTSInformatica.NTSLabel
  Public WithEvents lbAn_contropLabel As NTSInformatica.NTSLabel
  Public WithEvents edAn_controp As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckAn_partite As NTSInformatica.NTSCheckBox
  Public WithEvents ckAn_scaden As NTSInformatica.NTSCheckBox
  Public WithEvents lbAn_accperi As NTSInformatica.NTSLabel
  Public WithEvents cbAn_accperi As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_note As NTSInformatica.NTSLabel
  Public WithEvents edAn_note As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_funzionLabel As NTSInformatica.NTSLabel
  Public WithEvents edAn_funzion As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_cksegno As NTSInformatica.NTSLabel
  Public WithEvents cbAn_cksegno As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_tipacq As NTSInformatica.NTSLabel
  Public WithEvents cbAn_tipacq As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_conprof As NTSInformatica.NTSLabel
  Public WithEvents cbAn_conprof As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_totcron As NTSInformatica.NTSLabel
  Public WithEvents cbAn_totcron As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_contrsemp As NTSInformatica.NTSLabel
  Public WithEvents cbAn_contrsemp As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_manrip As NTSInformatica.NTSLabel
  Public WithEvents cbAn_manrip As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_percman As NTSInformatica.NTSLabel
  Public WithEvents edAn_percman As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_colbil As NTSInformatica.NTSLabel
  Public WithEvents cbAn_colbil As NTSInformatica.NTSComboBox
  Public WithEvents fmValidita As NTSInformatica.NTSGroupBox
  Public WithEvents fmComportamento As NTSInformatica.NTSGroupBox
  Public WithEvents fmCollegamenti As NTSInformatica.NTSGroupBox
  Public WithEvents fmImposte As NTSInformatica.NTSGroupBox
  Public WithEvents fmIrap As NTSInformatica.NTSGroupBox
  Public WithEvents lbXx_voceirap As NTSInformatica.NTSLabel
  Public WithEvents edAn_pervari As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_pervari As NTSInformatica.NTSLabel
  Public WithEvents lbAn_varirap As NTSInformatica.NTSLabel
  Public WithEvents edAn_voceirap As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbAn_varirap As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_voceirapLabel As NTSInformatica.NTSLabel
  Public WithEvents edAn_indirap As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_indirap As NTSInformatica.NTSLabel
  Public WithEvents lbAn_indiidd As NTSInformatica.NTSLabel
  Public WithEvents edAn_indiidd As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_azcom As NTSInformatica.NTSLabel
  Public WithEvents edAn_indiiddsit As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbAn_azcom As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_indiiddsit As NTSInformatica.NTSLabel
  Public WithEvents fmRiclassificati As NTSInformatica.NTSGroupBox
  Public WithEvents cmdRiclassificazioni As NTSInformatica.NTSButton
  Public WithEvents lbRiclassif As NTSInformatica.NTSLabel
  Public WithEvents lbCee As NTSInformatica.NTSLabel
  Public WithEvents edAn_kpccee As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAn_kpccee2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_kpccee2 As NTSInformatica.NTSLabel
  Public WithEvents lbAn_kpccee As NTSInformatica.NTSLabel
  Public WithEvents lbAn_rifrica As NTSInformatica.NTSLabel
  Public WithEvents edAn_rifrica As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAn_rifricd As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_rifricd As NTSInformatica.NTSLabel
  Public WithEvents fmRicavometro As NTSInformatica.NTSGroupBox
  Public WithEvents ckAn_ricmimp As NTSInformatica.NTSCheckBox
  Public WithEvents ckAn_ricmpro As NTSInformatica.NTSCheckBox
  Public WithEvents ckAn_intragr As NTSInformatica.NTSCheckBox
  Public WithEvents fmStudi As NTSInformatica.NTSGroupBox
  Public WithEvents lbAn_stseimp As NTSInformatica.NTSLabel
  Public WithEvents cbAn_stseimp As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_stsepro As NTSInformatica.NTSLabel
  Public WithEvents cbAn_stsepro As NTSInformatica.NTSComboBox
  Public WithEvents fmUsaContoFunz As NTSInformatica.NTSGroupBox
  Public WithEvents ckOpzgest4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckOpzgest3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckOpzgest2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckOpzgest5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckOpzgest6 As NTSInformatica.NTSCheckBox
  Public WithEvents ckOpzgest1 As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_pccontodescr As NTSInformatica.NTSLabel
  Public WithEvents edAn_pcconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_pccontoLabel As NTSInformatica.NTSLabel
  Public WithEvents tsSotc As NTSInformatica.NTSTabControl
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbAn_codpcon As NTSInformatica.NTSLabel
  Public WithEvents lbAn_codpconLabel As NTSInformatica.NTSLabel
  Public WithEvents lbXx_valuta As NTSInformatica.NTSLabel
  Public WithEvents edAn_valuta As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_valuta As NTSInformatica.NTSLabel
  Public WithEvents lbHelp As NTSInformatica.NTSLabel
  Public WithEvents tlbPartitario As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnDati2Top As NTSInformatica.NTSPanel
  Public WithEvents tlbAnacent As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbAnalink As NTSInformatica.NTSBarMenuItem
  Public WithEvents fmTesoreria As NTSInformatica.NTSGroupBox
  Public WithEvents lbXx_codvfde As NTSInformatica.NTSLabel
  Public WithEvents cbAn_trating As NTSInformatica.NTSComboBox
  Public WithEvents edAn_codvfde As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_codvfde As NTSInformatica.NTSLabel
  Public WithEvents lbAn_trating As NTSInformatica.NTSLabel
  Public WithEvents pnTesoreria As NTSInformatica.NTSPanel
  Public WithEvents lbHelp2 As NTSInformatica.NTSLabel
  Public WithEvents tlbAnacent2 As NTSInformatica.NTSBarMenuItem
  Public WithEvents ckAn_fldespdc As NTSInformatica.NTSCheckBox
  Public WithEvents ckAn_ivainded As NTSInformatica.NTSCheckBox
  Public WithEvents lbAn_ivainded As NTSInformatica.NTSLabel
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
End Class
