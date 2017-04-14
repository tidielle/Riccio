Partial Class FRM__SELE
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
  Public WithEvents edUser As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbTipo As NTSInformatica.NTSComboBox
  Public WithEvents edDeviceName As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents lbUser As NTSInformatica.NTSLabel
  Public WithEvents lbDeviceName As NTSInformatica.NTSLabel
  Public WithEvents lbTipopag As NTSInformatica.NTSLabel
  Public WithEvents NtsPanel1 As NTSInformatica.NTSPanel
  Public WithEvents lbDatScadA As NTSInformatica.NTSLabel
  Public WithEvents lbDatScadDa As NTSInformatica.NTSLabel
  Public WithEvents edDatScadA As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatScadDa As NTSInformatica.NTSTextBoxData

End Class
