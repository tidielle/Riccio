Partial Public Class FRM__GCRE
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


  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents NtsGroupBox1 As NTSInformatica.NTSGroupBox
  Public WithEvents NtsGroupBox3 As NTSInformatica.NTSGroupBox
  Public WithEvents optPrior4 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior2 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior3 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior1 As NTSInformatica.NTSRadioButton
  Public WithEvents cmdCancRow As NTSInformatica.NTSButton
  Public WithEvents NtsLabel2 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel3 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel4 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel5 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel6 As NTSInformatica.NTSLabel
  Public WithEvents grPC As NTSInformatica.NTSGrid
  Public WithEvents grvPC As NTSInformatica.NTSGridView
  Public WithEvents pc_pcname As NTSInformatica.NTSGridColumn
  Public WithEvents pc_dll As NTSInformatica.NTSGridColumn
  Public WithEvents pc_nomprop As NTSInformatica.NTSGridColumn
  Public WithEvents pc_valprop As NTSInformatica.NTSGridColumn
  Public WithEvents cmdRipristina As NTSInformatica.NTSButton
End Class
