Partial Public Class FRMVETPBF
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
  Public WithEvents lbXx_coddivi As NTSInformatica.NTSLabel
  Public WithEvents edTb_coddivi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_coddivi As NTSInformatica.NTSLabel
  Public WithEvents ckTb_fattextrc As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_codcauc As NTSInformatica.NTSLabel
  Public WithEvents edTb_codcauc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_codcauc As NTSInformatica.NTSLabel
  Public WithEvents ckTb_fattrevch As NTSInformatica.NTSCheckBox
  Public WithEvents ckTb_resoclfor As NTSInformatica.NTSCheckBox
  Public WithEvents lbTb_fattrevch As NTSInformatica.NTSLabel
  Public WithEvents cbTb_fattrevch As NTSInformatica.NTSComboBox
  Public WithEvents edTb_tiporkok As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_tiporkok As NTSInformatica.NTSLabel
  Public WithEvents cmdTb_tiporkok As NTSInformatica.NTSButton
  Public WithEvents lbXx_tlistin As NTSInformatica.NTSLabel
  Public WithEvents tlbDuplica As NTSInformatica.NTSBarButtonItem

End Class
