Partial Public Class FRM__ANA2
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

  Public WithEvents pnButtons As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents pnControlli As NTSInformatica.NTSPanel
  Public WithEvents lbEscomp As NTSInformatica.NTSLabel
  Public WithEvents cbSaldo As NTSInformatica.NTSComboBox
  Public WithEvents lbSaldo As NTSInformatica.NTSLabel
  Public WithEvents cbMovimentato As NTSInformatica.NTSComboBox
  Public WithEvents lbMovimentato As NTSInformatica.NTSLabel
  Public WithEvents lbDesescomp As NTSInformatica.NTSLabel
  Public WithEvents edEscomp As NTSInformatica.NTSTextBoxNum

End Class