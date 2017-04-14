Partial Public Class FRM__GESF
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
  Public WithEvents cmdUpload As NTSInformatica.NTSButton
  Public WithEvents ckAccessoCompleto As NTSInformatica.NTSCheckBox
  Public WithEvents lbTitolo As NTSInformatica.NTSLabel
  Public WithEvents cmdRinomina As NTSInformatica.NTSButton
  Public WithEvents cmdIncolla As NTSInformatica.NTSButton
  Public WithEvents cmdCopia As NTSInformatica.NTSButton

End Class
