Partial Public Class FRM__ESCL
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
  Public WithEvents GridColumn13 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn14 As NTSInformatica.NTSGridColumn
  Public WithEvents es_escludi As NTSInformatica.NTSGridColumn
  Public WithEvents es_cods As NTSInformatica.NTSGridColumn
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents lbNota As NTSInformatica.NTSLabel
End Class
