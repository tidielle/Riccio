Partial Public Class FRM__BANC
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
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grBanc As NTSInformatica.NTSGrid
  Public WithEvents grvBanc As NTSInformatica.NTSGridView
  Public WithEvents cba_abi As NTSInformatica.NTSGridColumn
  Public WithEvents cba_cab As NTSInformatica.NTSGridColumn
  Public WithEvents cba_rifriba As NTSInformatica.NTSGridColumn
  Public WithEvents cba_note As NTSInformatica.NTSGridColumn
  Public WithEvents cba_prefiban As NTSInformatica.NTSGridColumn
  Public WithEvents xx_cab As NTSInformatica.NTSGridColumn
  Public WithEvents xx_abi As NTSInformatica.NTSGridColumn
  Public WithEvents cba_cin As NTSInformatica.NTSGridColumn
  Public WithEvents cba_iban As NTSInformatica.NTSGridColumn
  Public WithEvents cba_codvalu As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codvalu As NTSInformatica.NTSGridColumn
  Public WithEvents cba_swift As NTSInformatica.NTSGridColumn
End Class
