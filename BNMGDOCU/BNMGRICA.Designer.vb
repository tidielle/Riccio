Partial Public Class FRMMGRICA
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
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents fmCorpo As NTSInformatica.NTSGroupBox
  Public WithEvents fmTestata As NTSInformatica.NTSGroupBox
  Public WithEvents ckRileggiDaAnangra As NTSInformatica.NTSCheckBox
  Public WithEvents opRigheSelezionate As NTSInformatica.NTSRadioButton
  Public WithEvents opRigaCorrente As NTSInformatica.NTSRadioButton
  Public WithEvents opTutteRighe As NTSInformatica.NTSRadioButton
  Public WithEvents ckProvvigioni As NTSInformatica.NTSCheckBox
  Public WithEvents ckSconti As NTSInformatica.NTSCheckBox
  Public WithEvents ckPrezzi As NTSInformatica.NTSCheckBox
  Public WithEvents ckPrezziListino As NTSInformatica.NTSCheckBox
  Public WithEvents ckAncheRigheEvase As NTSInformatica.NTSCheckBox

End Class
