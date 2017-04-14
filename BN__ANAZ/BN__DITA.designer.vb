Partial Public Class FRM__DITA
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
  Public WithEvents ckAc_gprincomp As NTSInformatica.NTSCheckBox
  Public WithEvents ckAc_provvig2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckAc_lotti2 As NTSInformatica.NTSCheckBox
  Public WithEvents lbAc_contabft As NTSInformatica.NTSLabel
  Public WithEvents cbAc_contabft As NTSInformatica.NTSComboBox
  Public WithEvents ckAc_mgdi As NTSInformatica.NTSCheckBox

End Class
