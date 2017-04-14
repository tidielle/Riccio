Partial Public Class FRMMGORSC
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

  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents lbContoLabel As NTSInformatica.NTSLabel
  Public WithEvents cmdEsci As NTSInformatica.NTSButton
  Public WithEvents ckVisAll As NTSInformatica.NTSCheckBox
  Public WithEvents grOrdin As NTSInformatica.NTSGrid
  Public WithEvents grvOrdin As NTSInformatica.NTSGridView
  Public WithEvents tt_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents tt_anno As NTSInformatica.NTSGridColumn
  Public WithEvents tt_serie As NTSInformatica.NTSGridColumn
  Public WithEvents tt_numero As NTSInformatica.NTSGridColumn
  Public WithEvents tt_riga As NTSInformatica.NTSGridColumn
  Public WithEvents tt_codart As NTSInformatica.NTSGridColumn
  Public WithEvents tt_desart As NTSInformatica.NTSGridColumn
  Public WithEvents tt_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents tt_desmaga As NTSInformatica.NTSGridColumn
  Public WithEvents tt_datord As NTSInformatica.NTSGridColumn
  Public WithEvents tt_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents tt_quant As NTSInformatica.NTSGridColumn
  Public WithEvents tt_quapre As NTSInformatica.NTSGridColumn
  Public WithEvents tt_quaeva As NTSInformatica.NTSGridColumn
  Public WithEvents tt_annpar As NTSInformatica.NTSGridColumn
  Public WithEvents tt_alfpar As NTSInformatica.NTSGridColumn
  Public WithEvents tt_numpar As NTSInformatica.NTSGridColumn
  Public WithEvents tt_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents tt_note As NTSInformatica.NTSGridColumn
  Public WithEvents tt_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents tt_scont1 As NTSInformatica.NTSGridColumn
  Public WithEvents tt_scont2 As NTSInformatica.NTSGridColumn
  Public WithEvents tt_scont3 As NTSInformatica.NTSGridColumn
  Public WithEvents tt_valres As NTSInformatica.NTSGridColumn
  Public WithEvents tt_qtares As NTSInformatica.NTSGridColumn
  Public WithEvents tt_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents tt_subcommeca As NTSInformatica.NTSGridColumn
  Public WithEvents tt_scont4 As NTSInformatica.NTSGridColumn
  Public WithEvents tt_scont5 As NTSInformatica.NTSGridColumn
  Public WithEvents tt_scont6 As NTSInformatica.NTSGridColumn
  Public WithEvents tt_fase As NTSInformatica.NTSGridColumn
  Public WithEvents tt_codappr As NTSInformatica.NTSGridColumn
End Class
