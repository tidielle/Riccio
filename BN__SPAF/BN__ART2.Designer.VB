Partial Public Class FRM__ART2
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
  Public WithEvents pnButtons As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents pnControlliTop As NTSInformatica.NTSPanel
  Public WithEvents edCodvalu As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbEsistenza As NTSInformatica.NTSComboBox
  Public WithEvents lbEsistenza As NTSInformatica.NTSLabel
  Public WithEvents lbDescodvalu As NTSInformatica.NTSLabel
  Public WithEvents cbScorta As NTSInformatica.NTSComboBox
  Public WithEvents lbScorta As NTSInformatica.NTSLabel
  Public WithEvents lbCodvalu As NTSInformatica.NTSLabel
  Public WithEvents cbData As NTSInformatica.NTSComboBox
  Public WithEvents lbTestData As NTSInformatica.NTSLabel
  Public WithEvents edData As NTSInformatica.NTSTextBoxData
  Public WithEvents lbData As NTSInformatica.NTSLabel
  Public WithEvents edMagazfin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbMagazfin As NTSInformatica.NTSLabel
  Public WithEvents edMagazini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbMagazini As NTSInformatica.NTSLabel
  Public WithEvents pnControlliButtom As NTSInformatica.NTSPanel
  Public WithEvents fmListino As NTSInformatica.NTSGroupBox
  Public WithEvents edListino As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbListino As NTSInformatica.NTSLabel
  Public WithEvents edContoini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbContoini As NTSInformatica.NTSLabel
  Public WithEvents cbTipolistini As NTSInformatica.NTSComboBox
  Public WithEvents edContofin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbContofin As NTSInformatica.NTSLabel
  Public WithEvents fmPrimoliv As NTSInformatica.NTSGroupBox
  Public WithEvents fmPromozione As NTSInformatica.NTSGroupBox
  Public WithEvents ckPromozione As NTSInformatica.NTSCheckBox
  Public WithEvents edCodtpro As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDescodtpro As NTSInformatica.NTSLabel
  Public WithEvents lbCodtpro As NTSInformatica.NTSLabel
  Public WithEvents ckPrimoliv As NTSInformatica.NTSCheckBox

End Class