Partial Class FRMMGMPRO
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

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnPulsanti As NTSInformatica.NTSPanel
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents pnControlli As NTSInformatica.NTSPanel
  Public WithEvents ckEsistMagZero As NTSInformatica.NTSCheckBox
  Public WithEvents ckEsplComb As NTSInformatica.NTSCheckBox
  Public WithEvents ckMovimentazione As NTSInformatica.NTSCheckBox
  Public WithEvents ckEsplFasi As NTSInformatica.NTSCheckBox
  Public WithEvents ckEsistMinZero As NTSInformatica.NTSCheckBox

End Class