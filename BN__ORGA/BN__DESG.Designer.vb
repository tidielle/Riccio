Partial Public Class FRM__DESG
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
  Public WithEvents pnDx As NTSInformatica.NTSPanel
  Public WithEvents pnSx As NTSInformatica.NTSPanel
  Public WithEvents lbXx_stato As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codcomu As NTSInformatica.NTSLabel
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbTitle As NTSInformatica.NTSLabel
End Class
