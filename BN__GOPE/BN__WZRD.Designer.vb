Partial Public Class FRM__WZRD
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
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdProcedi As NTSInformatica.NTSButton
  Public WithEvents fmControlli As NTSInformatica.NTSGroupBox
  Public WithEvents fmSostituisce As NTSInformatica.NTSGroupBox
  Public WithEvents lbOldAgente As NTSInformatica.NTSLabel
  Public WithEvents lbDesOldAgente As NTSInformatica.NTSLabel
  Public WithEvents edOldAgente As NTSInformatica.NTSTextBoxNum
  Public WithEvents edOldOperatore As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbOldOperatore As NTSInformatica.NTSLabel
  Public WithEvents ckSostituisce As NTSInformatica.NTSCheckBox
  Public WithEvents fmAltriAccessi As NTSInformatica.NTSGroupBox
  Public WithEvents lbAltriAccessi As NTSInformatica.NTSLabel
  Public WithEvents opAltriAccessi3 As NTSInformatica.NTSRadioButton
  Public WithEvents opAltriAccessi2 As NTSInformatica.NTSRadioButton
  Public WithEvents opAltriAccessi1 As NTSInformatica.NTSRadioButton
  Public WithEvents fmOrganizzazioneDitta As NTSInformatica.NTSGroupBox
  Public WithEvents lbCell As NTSInformatica.NTSLabel
  Public WithEvents edCell As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEmail As NTSInformatica.NTSLabel
  Public WithEvents edEmail As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDescodruaz As NTSInformatica.NTSLabel
  Public WithEvents edCodruaz As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCodruaz As NTSInformatica.NTSLabel
  Public WithEvents fmAccessiDitta As NTSInformatica.NTSGroupBox
  Public WithEvents lbDescodcage As NTSInformatica.NTSLabel
  Public WithEvents edCodcage As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckCodcage As NTSInformatica.NTSCheckBox
  Public WithEvents ckAmm As NTSInformatica.NTSCheckBox
  Public WithEvents ckCrmmod As NTSInformatica.NTSCheckBox
  Public WithEvents fmPulsanti As NTSInformatica.NTSGroupBox


  'Required by the Windows Form Designer
  'Private components As System.ComponentModel.IContainer

End Class