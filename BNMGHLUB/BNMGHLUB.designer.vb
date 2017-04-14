Partial Public Class FRMMGHLUB
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
  Public WithEvents lbArticolo As NTSInformatica.NTSLabel
  Public WithEvents pnAction As NTSInformatica.NTSPanel
  Public WithEvents pnDescr As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents grZoom As NTSInformatica.NTSGrid
  Public WithEvents grvZoom As NTSInformatica.NTSGridView
  Public WithEvents tt_ubicaz As NTSInformatica.NTSGridColumn
  Public WithEvents tt_quant As NTSInformatica.NTSGridColumn
  Public WithEvents tt_qtacar As NTSInformatica.NTSGridColumn
  Public WithEvents edCodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbArticoloLabel As NTSInformatica.NTSLabel
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents ckAnaubic As NTSInformatica.NTSCheckBox

End Class