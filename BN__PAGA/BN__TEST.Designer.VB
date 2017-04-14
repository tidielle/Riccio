Partial Class FRM__TEST
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
  Public WithEvents NtsPanel1 As NTSInformatica.NTSPanel
  Public WithEvents edRate As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSpese As NTSInformatica.NTSTextBoxNum
  Public WithEvents edIva As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTotdoc As NTSInformatica.NTSTextBoxNum
  Public WithEvents edData As NTSInformatica.NTSTextBoxData
  Public WithEvents lbRate As NTSInformatica.NTSLabel
  Public WithEvents lbSpese As NTSInformatica.NTSLabel
  Public WithEvents lbIVA As NTSInformatica.NTSLabel
  Public WithEvents lbTotdoc As NTSInformatica.NTSLabel
  Public WithEvents lbData As NTSInformatica.NTSLabel
  Public WithEvents liRate As NTSInformatica.NTSListBox
  Public WithEvents lbCaptionRate As NTSInformatica.NTSLabel
  Public WithEvents edRateCalc As NTSInformatica.NTSMemoBox

End Class
