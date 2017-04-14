Partial Public Class FRM__CLIE
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
  Public WithEvents edAn_codpagadet3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_codpagadet3 As NTSInformatica.NTSLabel
  Public WithEvents edAn_codpagadet2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_codpagadet2 As NTSInformatica.NTSLabel
  Public WithEvents edAn_codpagadet As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_codpagadet As NTSInformatica.NTSLabel
  Public WithEvents lbDeteriorabili As NTSInformatica.NTSLabel

End Class
