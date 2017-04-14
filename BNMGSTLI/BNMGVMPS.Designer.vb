Partial Class FRMMGVMPS
  Inherits FRM__CHIL

  ''Form overrides dispose to clean up the component list.
  '<System.Diagnostics.DebuggerNonUserCode()> _
  'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
  '  Try
  '    If disposing AndAlso components IsNot Nothing Then
  '      components.Dispose()
  '    End If
  '  Finally
  '    MyBase.Dispose(disposing)
  '  End Try
  'End Sub

  ''Required by the Windows Form Designer
  'Private components As System.ComponentModel.IContainer
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
  Public WithEvents pnPrezzo As NTSInformatica.NTSPanel
  Public WithEvents edPrezzo As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Public WithEvents pnSconto As NTSInformatica.NTSPanel
  Public WithEvents edSconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel2 As NTSInformatica.NTSLabel
  Public WithEvents opAggiungi As NTSInformatica.NTSRadioButton
  Public WithEvents opSostituisci As NTSInformatica.NTSRadioButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents pnVmps As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton

End Class
