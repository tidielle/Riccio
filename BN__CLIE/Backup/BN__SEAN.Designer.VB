Partial Class FRM__SEAN
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
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents pnApri As NTSInformatica.NTSPanel
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents cbTipo As NTSInformatica.NTSComboBox
  Public WithEvents lbTipo As NTSInformatica.NTSLabel

End Class
