Partial Class FRM__DTRI
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
  Public WithEvents pnPulsanti As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents lbDescr As NTSInformatica.NTSLabel
  Public WithEvents edCodtric As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodtric As NTSInformatica.NTSLabel
  Public WithEvents pnControlli As NTSInformatica.NTSPanel
  Public WithEvents edDestric As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPdc As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbPdc As NTSInformatica.NTSLabel
  Public WithEvents opCopia3 As NTSInformatica.NTSRadioButton
  Public WithEvents opCopia2 As NTSInformatica.NTSRadioButton
  Public WithEvents opCopia1 As NTSInformatica.NTSRadioButton

End Class