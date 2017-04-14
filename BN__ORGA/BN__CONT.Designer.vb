Partial Public Class FRM__CONT
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
  Public WithEvents lbDesLead As NTSInformatica.NTSLabel
  Public WithEvents lbDesConto As NTSInformatica.NTSLabel
  Public WithEvents edLead As NTSInformatica.NTSTextBoxNum
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents opLead As NTSInformatica.NTSRadioButton
  Public WithEvents opConto As NTSInformatica.NTSRadioButton
  Public WithEvents opInterno As NTSInformatica.NTSRadioButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdCreaAnagrafica As NTSInformatica.NTSButton
End Class
