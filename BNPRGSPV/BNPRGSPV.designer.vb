Partial Public Class FRMPRGSPV
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
  Public WithEvents pv_segno As NTSInformatica.NTSGridColumn
  Public WithEvents pv_datmatu As NTSInformatica.NTSGridColumn
  Public WithEvents pv_datcorr As NTSInformatica.NTSGridColumn
  Public WithEvents pv_note As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn138 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn139 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn140 As NTSInformatica.NTSGridColumn
  Public WithEvents pv_datscadeff As NTSInformatica.NTSGridColumn
  Public WithEvents pv_scflsaldato As NTSInformatica.NTSGridColumn
  Public WithEvents pv_dtsaldato As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn141 As NTSInformatica.NTSGridColumn
  Public WithEvents pv_impscad As NTSInformatica.NTSGridColumn
  Public WithEvents cmdPaga As NTSInformatica.NTSButton
End Class
