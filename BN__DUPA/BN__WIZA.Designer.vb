Partial Public Class FRM__WIZA
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
  Public WithEvents opNessuna As NTSInformatica.NTSRadioButton
  Public WithEvents opAttach As NTSInformatica.NTSRadioButton
  Public WithEvents lbLdf As NTSInformatica.NTSLabel
  Public WithEvents lbMdf As NTSInformatica.NTSLabel
  Public WithEvents edLdf As NTSInformatica.NTSTextBoxStr
  Public WithEvents edMdf As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDirnota As NTSInformatica.NTSLabel
  Public WithEvents ckUnicode As NTSInformatica.NTSCheckBox
End Class
