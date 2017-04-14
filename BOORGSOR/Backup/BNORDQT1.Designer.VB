Partial Public Class FRMORDQT1
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
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grTco As NTSInformatica.NTSGrid
  Public WithEvents grvTco As NTSInformatica.NTSGridView
  Public WithEvents ec_quant01 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant02 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant03 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant04 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant05 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant06 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant07 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant08 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant09 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant10 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant11 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant12 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant13 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant14 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant15 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant16 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant17 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant18 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant19 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant20 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant21 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant22 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant23 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant24 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_caption As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ec_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ec_riga As NTSInformatica.NTSGridColumn
  Public WithEvents ec_fase As NTSInformatica.NTSGridColumn
  Public WithEvents ec_desfase As NTSInformatica.NTSGridColumn
  Public WithEvents grTest As NTSInformatica.NTSGrid
  Public WithEvents grvTest As NTSInformatica.NTSGridView
  Public WithEvents xxo_codtagl As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_destagl As NTSInformatica.NTSGridColumn
  Public WithEvents SplitCliHoriz As NTSInformatica.NTSSplitter
End Class