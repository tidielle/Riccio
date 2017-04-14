Partial Public Class FRM__ETIM
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
  Public WithEvents lbDataelab As NTSInformatica.NTSLabel
  Public WithEvents fmDitta As NTSInformatica.NTSGroupBox
  Public WithEvents edDitta As NTSInformatica.NTSTextBoxStr
  Public WithEvents opDitteuna As NTSInformatica.NTSRadioButton
  Public WithEvents opDittetutte As NTSInformatica.NTSRadioButton
  Public WithEvents edDataelab As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDitta As NTSInformatica.NTSLabel
  Public WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
  Public WithEvents Timer1 As System.Windows.Forms.Timer
  Public WithEvents ckNoAggData As NTSInformatica.NTSCheckBox
  Public WithEvents ckBatch1Giro As NTSInformatica.NTSCheckBox
  Public WithEvents fmAlert As NTSInformatica.NTSGroupBox
  Public WithEvents opAlertalcuni As NTSInformatica.NTSRadioButton
  Public WithEvents opAlerttutti As NTSInformatica.NTSRadioButton
  Public WithEvents cmdDesel As NTSInformatica.NTSButton
  Public WithEvents cmdSel As NTSInformatica.NTSButton
  Public WithEvents liAlert As NTSInformatica.NTSListBox
  Public WithEvents fmSoloalert As NTSInformatica.NTSGroupBox
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbGeneraBub As NTSInformatica.NTSBarMenuItem
End Class
