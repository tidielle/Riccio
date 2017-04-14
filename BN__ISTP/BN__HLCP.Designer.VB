Partial Public Class FRM__HLCP
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
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents edCampo As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCampo As NTSInformatica.NTSLabel
  Public WithEvents edTabella As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTabella As NTSInformatica.NTSLabel
  Public WithEvents cmdCampo As NTSInformatica.NTSButton
  Public WithEvents cmdTabella As NTSInformatica.NTSButton

  'Required by the Windows Form Designer
  'Private components As System.ComponentModel.IContainer

End Class
