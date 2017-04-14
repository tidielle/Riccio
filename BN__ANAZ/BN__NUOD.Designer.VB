Partial Class FRM__NUOD
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

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents lbProgr As NTSInformatica.NTSLabel
  Public WithEvents pnNuovo As NTSInformatica.NTSPanel
  Public WithEvents edAnagen As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAnagenlabel As NTSInformatica.NTSLabel
  Public WithEvents lbAnagen As NTSInformatica.NTSLabel
  Public WithEvents edCodditt As NTSInformatica.NTSTextBoxStr

End Class
