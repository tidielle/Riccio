Partial Public Class FRMORGRID
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
  Public WithEvents grQuant As NTSInformatica.NTSGrid
  Public WithEvents grvQuant As NTSInformatica.NTSGridView
  Public WithEvents td_codagen As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codagen As NTSInformatica.NTSGridColumn
  Public WithEvents td_tipobf As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tipobf As NTSInformatica.NTSGridColumn
  Public WithEvents td_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents td_anno As NTSInformatica.NTSGridColumn
  Public WithEvents td_serie As NTSInformatica.NTSGridColumn
  Public WithEvents xx_sel As NTSInformatica.NTSGridColumn
  Public WithEvents td_numord As NTSInformatica.NTSGridColumn
  Public WithEvents td_conto As NTSInformatica.NTSGridColumn
  Public WithEvents td_vettor As NTSInformatica.NTSGridColumn
  Public WithEvents xx_vettor As NTSInformatica.NTSGridColumn
  Public WithEvents tlbSelTutto As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDeSelTutto As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents lbDescr As NTSInformatica.NTSLabel
End Class