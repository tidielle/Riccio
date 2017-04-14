Partial Public Class FRM__SOTC_F
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
  Public WithEvents tlbCambiaDitta As NTSInformatica.NTSBarButtonItem
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents lbAn_contoLabel As NTSInformatica.NTSLabel
  Public WithEvents edAn_descr1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAn_codmastLabel As NTSInformatica.NTSLabel
  Public WithEvents lbAn_codmast As NTSInformatica.NTSLabel
  Public WithEvents lbAn_descr1 As NTSInformatica.NTSLabel
  Public WithEvents lbAn_conto As NTSInformatica.NTSLabel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents pnMain As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents pnPag4 As NTSInformatica.NTSPanel
  Public WithEvents edAn_note2 As NTSInformatica.NTSMemoBox
  Public WithEvents pnPag1 As NTSInformatica.NTSPanel
  Public WithEvents lbXx_controp As NTSInformatica.NTSLabel
  Public WithEvents lbAn_contropLabel As NTSInformatica.NTSLabel
  Public WithEvents edAn_controp As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckAn_partite As NTSInformatica.NTSCheckBox
  Public WithEvents ckAn_scaden As NTSInformatica.NTSCheckBox
  Public WithEvents lbAn_accperi As NTSInformatica.NTSLabel
  Public WithEvents cbAn_accperi As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_note As NTSInformatica.NTSLabel
  Public WithEvents edAn_note As NTSInformatica.NTSTextBoxStr
  Public WithEvents fmComportamento As NTSInformatica.NTSGroupBox
  Public WithEvents edAn_pcconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAn_pccontoLabel As NTSInformatica.NTSLabel
  Public WithEvents tsSotc As NTSInformatica.NTSTabControl
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPartitario As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAnacent As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbAnalink As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbAnacent2 As NTSInformatica.NTSBarMenuItem
  Public WithEvents ckAn_ivainded As NTSInformatica.NTSCheckBox
  Public WithEvents lbAn_ivainded As NTSInformatica.NTSLabel
  Public WithEvents cbAn_colbil As NTSInformatica.NTSComboBox
  Public WithEvents lbAn_colbil As NTSInformatica.NTSLabel
  Public WithEvents fmRiclassificati As NTSInformatica.NTSGroupBox
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
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
End Class