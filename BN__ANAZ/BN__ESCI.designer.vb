Partial Public Class FRM__ESCI
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
  Public WithEvents lbXx_numestr As NTSInformatica.NTSLabel
  Public WithEvents lbXx_forfctre As NTSInformatica.NTSLabel
  Public WithEvents lbXx_cdtsitso As NTSInformatica.NTSLabel
  Public WithEvents edTb_codescg As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_codescg As NTSInformatica.NTSLabel
  Public WithEvents lbXX_codescg As NTSInformatica.NTSLabel
  Public WithEvents ckTb_gestcadp As NTSInformatica.NTSCheckBox
End Class
