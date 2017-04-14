Partial Class FRMSQVALO
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
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grVall As NTSInformatica.NTSGrid
  Public WithEvents grvVall As NTSInformatica.NTSGridView
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents GridColumn10 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn11 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn12 As NTSInformatica.NTSGridColumn
  Public WithEvents alv_descamp As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valore As NTSInformatica.NTSGridColumn
  Public WithEvents alv_valcombo As NTSInformatica.NTSGridColumn
  Public WithEvents alv_desval As NTSInformatica.NTSGridColumn
  Public WithEvents xx_combo As NTSInformatica.NTSGridColumn
End Class