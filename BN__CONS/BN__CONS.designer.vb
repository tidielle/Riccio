Partial Public Class FRM__CONS
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
  Public WithEvents tlbClienti As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbFornitori As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAnalisi As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbArticoli As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbDataDal As NTSInformatica.NTSLabel
  Public WithEvents lbDataAl As NTSInformatica.NTSLabel
  Public WithEvents edDatini As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatfin As NTSInformatica.NTSTextBoxData
  Public WithEvents ckFiltri As NTSInformatica.NTSCheckBox
  Public WithEvents cmdStatistiche As NTSInformatica.NTSButton
  Public WithEvents cmdDatiCont As NTSInformatica.NTSButton
  Public WithEvents cmdArtprox As NTSInformatica.NTSButton
  Public WithEvents cmdArtpro As NTSInformatica.NTSButton
  Public WithEvents cmdVisScad As NTSInformatica.NTSButton
  Public WithEvents cmdVisPart As NTSInformatica.NTSButton
  Public WithEvents cmdVisMovmag As NTSInformatica.NTSButton
  Public WithEvents cmdVisOrd As NTSInformatica.NTSButton
  Public WithEvents cmdNuovoDoc As NTSInformatica.NTSButton
  Public WithEvents cmdApriDoc As NTSInformatica.NTSButton
  Public WithEvents cmdNuovaAnagraf As NTSInformatica.NTSButton
  Public WithEvents cmdApriAnagraf As NTSInformatica.NTSButton
  Public WithEvents lbParentesi As NTSInformatica.NTSLabel
  Public WithEvents lbContoLabel As NTSInformatica.NTSLabel
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbArticolo As NTSInformatica.NTSLabel
  Public WithEvents edArticolo As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbArticoloLabel As NTSInformatica.NTSLabel
  Public WithEvents fmFiltri As NTSInformatica.NTSGroupBox
  Public WithEvents lbNota As NTSInformatica.NTSLabel
  Public WithEvents lbAnnoDoc As NTSInformatica.NTSLabel
  Public WithEvents lbtipoDoc As NTSInformatica.NTSLabel
  Public WithEvents edAnnoDoc As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipoDoc As NTSInformatica.NTSComboBox
  Public WithEvents lbEscomp As NTSInformatica.NTSLabel
  Public WithEvents cbEscomp As NTSInformatica.NTSComboBox
  Public WithEvents edEsaltro As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnMain As NTSInformatica.NTSPanel
End Class
