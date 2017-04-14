Partial Public Class FRM__HLPF
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
  Public WithEvents pnAction As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents grHlpf As NTSInformatica.NTSGrid
  Public WithEvents grvHlpf As NTSInformatica.NTSGridView
  Public WithEvents pf_codform As NTSInformatica.NTSGridColumn
  Public WithEvents pf_titstam As NTSInformatica.NTSGridColumn

End Class