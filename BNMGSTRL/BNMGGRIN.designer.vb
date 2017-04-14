Partial Public Class FRMMGGRIN
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

  Public WithEvents grGrin As NTSInformatica.NTSGrid
  Public WithEvents grvGrin As NTSInformatica.NTSGridView
  Public WithEvents in_codart As NTSInformatica.NTSGridColumn
  Public WithEvents in_desart As NTSInformatica.NTSGridColumn
  Public WithEvents in_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents in_giaini As NTSInformatica.NTSGridColumn
  Public WithEvents in_vgiaini As NTSInformatica.NTSGridColumn
  Public WithEvents in_incdec As NTSInformatica.NTSGridColumn
  Public WithEvents in_costo As NTSInformatica.NTSGridColumn
  Public WithEvents in_val As NTSInformatica.NTSGridColumn
  Public WithEvents in_esist As NTSInformatica.NTSGridColumn
  Public WithEvents in_vesist As NTSInformatica.NTSGridColumn
  Public WithEvents in_perqta As NTSInformatica.NTSGridColumn
  Public WithEvents in_fase As NTSInformatica.NTSGridColumn
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents cmdEsci As NTSInformatica.NTSButton
  Public WithEvents lbTipval As NTSInformatica.NTSLabel
  Public WithEvents lbTipvalLabel As NTSInformatica.NTSLabel
  Public WithEvents lbTipoelab As NTSInformatica.NTSLabel
  Public WithEvents lbTipomerceLabel As NTSInformatica.NTSLabel
  Public WithEvents lbTipomagazLabel As NTSInformatica.NTSLabel
  Public WithEvents lbTipoelabLabel As NTSInformatica.NTSLabel
  Public WithEvents lbValore As NTSInformatica.NTSLabel
  Public WithEvents lbTipomerce As NTSInformatica.NTSLabel
  Public WithEvents lbTipomagaz As NTSInformatica.NTSLabel
  Public WithEvents lbValoreLabel As NTSInformatica.NTSLabel
  Public WithEvents cmdStampaVideo As NTSInformatica.NTSButton
  Public WithEvents in_desint As NTSInformatica.NTSGridColumn
End Class
