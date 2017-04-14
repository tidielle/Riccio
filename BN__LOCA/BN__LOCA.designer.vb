Partial Public Class FRM__LOCA
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
  Public WithEvents edEnd As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel2 As NTSInformatica.NTSLabel
  Public WithEvents edStart As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents lbNota As NTSInformatica.NTSLabel

End Class
