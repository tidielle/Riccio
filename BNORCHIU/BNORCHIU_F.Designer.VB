Partial Public Class FRMORCHIU_F
  Inherits FRM__CHIL

  <System.Diagnostics.DebuggerNonUserCode()> _
  Public Sub New()
    MyBase.New()

    ''This call is required by the Windows Form Designer.
    'InitializeComponent()

  End Sub

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub
  Public WithEvents lbTesto As NTSInformatica.NTSLabel
  Public WithEvents lbDescodagen As NTSInformatica.NTSLabel
  Public WithEvents edCodagen As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodagen As NTSInformatica.NTSLabel
  Public WithEvents fmEvasioneOrdini As NTSInformatica.NTSGroupBox
  Public WithEvents optDaEvadere As NTSInformatica.NTSRadioButton
  Public WithEvents optParzEvasi As NTSInformatica.NTSRadioButton

  'Required by the Windows Form Designer
  'Private components As System.ComponentModel.IContainer

End Class