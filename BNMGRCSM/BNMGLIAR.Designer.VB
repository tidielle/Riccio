Partial Class FRMMGLIAR
  Inherits FRM__CHIL

  <System.Diagnostics.DebuggerNonUserCode()> _
  Public Sub New()
    MyBase.New()
  End Sub

  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents grLiar As NTSInformatica.NTSGrid
  Public WithEvents grvLiar As NTSInformatica.NTSGridView
  Public WithEvents ar_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ar_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ar_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents ar_conver As NTSInformatica.NTSGridColumn
  Public WithEvents ar_qtacon2 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_pesoca As NTSInformatica.NTSGridColumn
  Public WithEvents ar_scomin As NTSInformatica.NTSGridColumn
  Public WithEvents ar_rrfence As NTSInformatica.NTSGridColumn
  Public WithEvents ar_polriord As NTSInformatica.NTSGridColumn
  Public WithEvents ar_minord As NTSInformatica.NTSGridColumn
  Public WithEvents ar_sublotto As NTSInformatica.NTSGridColumn
  Public WithEvents ar_scomax As NTSInformatica.NTSGridColumn
  Public WithEvents ar_misura1 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_misura2 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_ricar1 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_ricar2 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_misura3 As NTSInformatica.NTSGridColumn
  Public WithEvents pnPanel1 As NTSInformatica.NTSPanel
  Public WithEvents pnPanel2 As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents ar_pesonet As NTSInformatica.NTSGridColumn
  Public WithEvents ar_volume As NTSInformatica.NTSGridColumn
  Public WithEvents fmTrend As NTSInformatica.NTSGroupBox
  Public WithEvents lbSecondoPeriodoFin As NTSInformatica.NTSLabel
  Public WithEvents lbPrimoPeriodoFin As NTSInformatica.NTSLabel
  Public WithEvents lbSecondoPeriodoIni As NTSInformatica.NTSLabel
  Public WithEvents lbPrimoPeriodoIni As NTSInformatica.NTSLabel
  Public WithEvents edPrimoPeriodoFin As NTSInformatica.NTSLabel
  Public WithEvents edPrimoPeriodoIni As NTSInformatica.NTSLabel
  Public WithEvents edSecondoPeriodoFin As NTSInformatica.NTSLabel
  Public WithEvents edSecondoPeriodoIni As NTSInformatica.NTSLabel
  Public WithEvents pnPulsanti As NTSInformatica.NTSPanel
  Public WithEvents edAammggIni As NTSInformatica.NTSLabel
  Public WithEvents lbAammggFin As NTSInformatica.NTSLabel
  Public WithEvents lbAammggIni As NTSInformatica.NTSLabel
  Public WithEvents edAammggFin As NTSInformatica.NTSLabel
  Public WithEvents ar_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents xx_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents ar_fase As NTSInformatica.NTSGridColumn
  Public WithEvents xx_fase As NTSInformatica.NTSGridColumn
End Class