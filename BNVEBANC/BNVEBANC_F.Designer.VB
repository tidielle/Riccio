Partial Public Class FRMVEBANC_F
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
  Public WithEvents lbTb_cin As NTSInformatica.NTSLabel
  Public WithEvents lbTb_swift As NTSInformatica.NTSLabel
  Public WithEvents edTb_swift As NTSInformatica.NTSTextBoxStr

  'Required by the Windows Form Designer
  'Private components As System.ComponentModel.IContainer
End Class