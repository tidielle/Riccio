Partial Class FRMMGPEAC_F
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
  Public WithEvents NtsTabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents NtsPanel1 As NTSInformatica.NTSPanel
  Public WithEvents lbXx_tipobfrko As NTSInformatica.NTSLabel
  Public WithEvents edTb_tipobfrko As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tipobfrko As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tipobfrkm As NTSInformatica.NTSLabel
  Public WithEvents edTb_tipobfrkm As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tipobfrkm As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tipobfrkl As NTSInformatica.NTSLabel
  Public WithEvents edTb_tipobfrkl As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tipobfrkl As NTSInformatica.NTSLabel
  Public WithEvents lbXx_tipobfrkj As NTSInformatica.NTSLabel
  Public WithEvents edTb_tipobfrkj As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_tipobfrkj As NTSInformatica.NTSLabel

End Class