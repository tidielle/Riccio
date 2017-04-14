Partial Public Class FRM__DAPA
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
  Public WithEvents lbXx_codvparsg As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codvpar As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codvparsa As NTSInformatica.NTSLabel
  Public WithEvents lbAcs_codvparsa As NTSInformatica.NTSLabel
  Public WithEvents ckAcs_appenas As NTSInformatica.NTSCheckBox
  Public WithEvents ckAcs_appspegen As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_codtpbf As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codrtacp As NTSInformatica.NTSLabel
  Public WithEvents edAcs_codrtacp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAcs_codrtacp As NTSInformatica.NTSLabel
  Public WithEvents lbAcs_codtpbf As NTSInformatica.NTSLabel
  Public WithEvents edAcs_codtpbf As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAcs_codvparsa As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAcs_codvpar As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAcs_codvparsg As NTSInformatica.NTSTextBoxStr
End Class
