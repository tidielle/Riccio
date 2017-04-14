Partial Public Class FRM__HLTB
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
  Public WithEvents pnAction As NTSInformatica.NTSPanel
  Public WithEvents pnDescr As NTSInformatica.NTSPanel
  Public WithEvents lbDescr As NTSInformatica.NTSLabel
  Public WithEvents edDescr As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdGestione As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents ckOttimistico As NTSInformatica.NTSCheckBox
  Public WithEvents grZoom As NTSInformatica.NTSGrid
  Public WithEvents grvZoom As NTSInformatica.NTSGridView
  Public WithEvents GridColumn10 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn11 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codice As NTSInformatica.NTSGridColumn
  Public WithEvents tb_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ckIgnoraEsclusioni As NTSInformatica.NTSCheckBox
  Public WithEvents cmdEsclusioni As NTSInformatica.NTSButton

End Class
