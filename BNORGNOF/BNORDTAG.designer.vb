Partial Public Class FRMORDTAG
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
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grTco As NTSInformatica.NTSGrid
  Public WithEvents grvTco As NTSInformatica.NTSGridView
  Public WithEvents mm_quant01 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant02 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant03 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant04 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant05 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant06 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant07 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant08 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant09 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant10 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant11 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant12 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant13 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant14 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant15 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant16 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant17 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant18 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant19 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant20 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant21 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant22 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant23 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant24 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_caption As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codart As NTSInformatica.NTSGridColumn
  Public WithEvents mm_descr As NTSInformatica.NTSGridColumn
  Public WithEvents mm_riga As NTSInformatica.NTSGridColumn
  Public WithEvents mm_fase As NTSInformatica.NTSGridColumn
  Public WithEvents mm_desfase As NTSInformatica.NTSGridColumn
End Class
