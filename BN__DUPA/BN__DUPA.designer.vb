Partial Public Class FRM__DUPA
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
  Public WithEvents tlbRicreaSPArcproc As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbAz_rdsservername As NTSInformatica.NTSLabel
  Public WithEvents edAz_rdsservername As NTSInformatica.NTSTextBoxStr
  Public WithEvents ckAzOpGrup As NTSInformatica.NTSCheckBox
End Class
