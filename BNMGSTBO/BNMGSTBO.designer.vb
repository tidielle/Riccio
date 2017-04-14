Partial Public Class FRMMGSTBO
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
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents cmdApriFiltri As NTSInformatica.NTSButton
  Public WithEvents cbFiltro As NTSInformatica.NTSComboBox
  Public WithEvents lbFiltri As NTSInformatica.NTSLabel
  Public WithEvents lbDescodlsel As NTSInformatica.NTSLabel
  Public WithEvents lbCodlsel As NTSInformatica.NTSLabel
  Public WithEvents edCodlsel As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnFiltriExt As NTSInformatica.NTSPanel
  Public WithEvents ceFiltriExt As NTSInformatica.NTSXXFILT

End Class
