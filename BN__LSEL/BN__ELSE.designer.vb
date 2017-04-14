Partial Public Class FRM__ELSE
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
  Public WithEvents tlbDuplica As NTSInformatica.NTSBarButtonItem
  Public WithEvents lse_ogprogr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_emailc As NTSInformatica.NTSGridColumn
  Public WithEvents tlbLocalizzaGoogle As NTSInformatica.NTSBarButtonItem

End Class
