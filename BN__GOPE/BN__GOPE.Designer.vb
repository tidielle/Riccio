Partial Public Class FRM__GOPE
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
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbCopiaConfig As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbCopiaMenu As NTSInformatica.NTSBarMenuItem
  Public WithEvents ckOpIscrmus As NTSInformatica.NTSCheckBox
  Public WithEvents edOpDescont2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edOpDescont As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbOpDescont2 As NTSInformatica.NTSLabel
  Public WithEvents lbOpDescont As NTSInformatica.NTSLabel
  Public WithEvents ckOpNetOnly As NTSInformatica.NTSCheckBox
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnTutto As NTSInformatica.NTSPanel
  Public WithEvents fmGoogle As NTSInformatica.NTSGroupBox
  Public WithEvents lbOpGoogleUser As NTSInformatica.NTSLabel
  Public WithEvents edOpGoogleUser As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbOpGooglePwd As NTSInformatica.NTSLabel
  Public WithEvents edOpGooglePwd As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbOpSutipouser As NTSInformatica.NTSLabel
  Public WithEvents cbOpSutipouser As NTSInformatica.NTSComboBox
  Public WithEvents fmSocial As NTSInformatica.NTSGroupBox
  Public WithEvents lbOpSumail As NTSInformatica.NTSLabel
  Public WithEvents edOpSumail As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdCreaRelazioni As NTSInformatica.NTSButton
  Public WithEvents ckOpSulimiti As NTSInformatica.NTSCheckBox

  
  'Required by the Windows Form Designer
  'Private components As System.ComponentModel.IContainer

End Class

