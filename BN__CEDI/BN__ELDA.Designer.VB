Partial Class FRM__ELDA
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
  Public WithEvents ckMovc As NTSInformatica.NTSCheckBox
  Public WithEvents ckAnac As NTSInformatica.NTSCheckBox
  Public WithEvents ckEmail As NTSInformatica.NTSCheckBox
  Public WithEvents ckScadEx As NTSInformatica.NTSCheckBox
  Public WithEvents ckStatven As NTSInformatica.NTSCheckBox


  'Required by the Windows Form Designer
  'Private components As System.ComponentModel.IContainer

End Class
