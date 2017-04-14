Partial Public Class FRMORSCHO_F
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
  Public WithEvents NtsGridColumn1 As NTSInformatica.NTSGridColumn
  Public WithEvents NtsGridColumn2 As NTSInformatica.NTSGridColumn
  Public WithEvents NtsGridColumn3 As NTSInformatica.NTSGridColumn
  Public WithEvents ckSerie As NTSInformatica.NTSCheckBox
  Public WithEvents lbTipoStampa As NTSInformatica.NTSLabel
  Public WithEvents lbOrdinamento As NTSInformatica.NTSLabel
  Public WithEvents pnTipoStampa As NTSInformatica.NTSPanel
  Public WithEvents fmOpzioni As NTSInformatica.NTSGroupBox

End Class