Partial Class FRMMGELAR_F
  Inherits FRM__CHIL

  <System.Diagnostics.DebuggerNonUserCode()> _
  Public Sub New()
    MyBase.New()

    ''This call is required by the Windows Form Designer.
    'InitializeComponent()

  End Sub

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer
  Public WithEvents cbTipoElaborazione As NTSInformatica.NTSComboBox
  Public WithEvents fmTipoVariazione As NTSInformatica.NTSGroupBox
  Public WithEvents opScorporaIva As NTSInformatica.NTSRadioButton
  Public WithEvents opRicarico As NTSInformatica.NTSRadioButton
  Public WithEvents opValore As NTSInformatica.NTSRadioButton
  Public WithEvents opPercentuale As NTSInformatica.NTSRadioButton
  Public WithEvents cbPromozioni As NTSInformatica.NTSComboBox

End Class