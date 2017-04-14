Partial Public Class FRM__IFIL
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
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents fmOperatori As NTSInformatica.NTSGroupBox
  Public WithEvents opSoloO As NTSInformatica.NTSRadioButton
  Public WithEvents opTuttiO As NTSInformatica.NTSRadioButton
  Public WithEvents edTb_desifil As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTb_codifil As NTSInformatica.NTSTextBoxNum
  Public WithEvents opTranneO As NTSInformatica.NTSRadioButton
  Public WithEvents fmDitta As NTSInformatica.NTSGroupBox
  Public WithEvents lbOp As NTSInformatica.NTSLabel
  Public WithEvents opTranneD As NTSInformatica.NTSRadioButton
  Public WithEvents opSoloD As NTSInformatica.NTSRadioButton
  Public WithEvents opTutteD As NTSInformatica.NTSRadioButton
  Public WithEvents lbDitta As NTSInformatica.NTSLabel
  Public WithEvents edTb_users As NTSInformatica.NTSMemoBox
  Public WithEvents edTb_ditte As NTSInformatica.NTSMemoBox
  Public WithEvents lbCodice As NTSInformatica.NTSLabel
  Public WithEvents lbDescr As NTSInformatica.NTSLabel
  Public WithEvents tlbDuplica As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecRestore As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents lbInfo As NTSInformatica.NTSLabel
  Public WithEvents cbData As NTSInformatica.NTSComboBox

End Class
