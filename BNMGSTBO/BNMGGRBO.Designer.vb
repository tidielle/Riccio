Partial Public Class FRMMGGRBO
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
  Public WithEvents lbTotaleDocNoOmag As NTSInformatica.NTSLabel
  Public WithEvents lbTotaleOmag As NTSInformatica.NTSLabel
  Public WithEvents edTd_totdocnoomag As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTd_totomag As NTSInformatica.NTSTextBoxNum
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
End Class
