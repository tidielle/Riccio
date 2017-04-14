Partial Public Class FRM__HLCE
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
  Public WithEvents pnAction As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents grZoom As NTSInformatica.NTSGrid
  Public WithEvents grvZoom As NTSInformatica.NTSGridView
  Public WithEvents fmSezione As NTSInformatica.NTSGroupBox
  Public WithEvents optContiDo As NTSInformatica.NTSRadioButton
  Public WithEvents optContoEc As NTSInformatica.NTSRadioButton
  Public WithEvents optPassivo As NTSInformatica.NTSRadioButton
  Public WithEvents optAttivo As NTSInformatica.NTSRadioButton
  Public WithEvents f1 As NTSInformatica.NTSGridColumn
  Public WithEvents f2 As NTSInformatica.NTSGridColumn
  Public WithEvents f3 As NTSInformatica.NTSGridColumn
  Public WithEvents f4 As NTSInformatica.NTSGridColumn
  Public WithEvents f5 As NTSInformatica.NTSGridColumn
  Public WithEvents f6 As NTSInformatica.NTSGridColumn
  Public WithEvents cmdCerca As NTSInformatica.NTSButton
  Public WithEvents edCerca As NTSInformatica.NTSTextBoxStr
End Class
