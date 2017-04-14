Partial Public Class FRMMGGRSC
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
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents lbTotCarichi As NTSInformatica.NTSLabel
  Public WithEvents lbTotscarichi As NTSInformatica.NTSLabel
  Public WithEvents edTotCarichi As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTotScarichi As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents lbArticolo As NTSInformatica.NTSLabel
  Public WithEvents lbContoLabel As NTSInformatica.NTSLabel
  Public WithEvents lbArticoloLabel As NTSInformatica.NTSLabel
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
  Public WithEvents lbMagazLabel As NTSInformatica.NTSLabel
  Public WithEvents lbFase As NTSInformatica.NTSLabel
  Public WithEvents lbFaseLabel As NTSInformatica.NTSLabel
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents grMovim As NTSInformatica.NTSGrid
  Public WithEvents grvMovim As NTSInformatica.NTSGridView
  Public WithEvents km_aammgg As NTSInformatica.NTSGridColumn
  Public WithEvents km_causale As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents km_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents km_numdoc As NTSInformatica.NTSGridColumn
  Public WithEvents km_serie As NTSInformatica.NTSGridColumn
  Public WithEvents km_subcommeca As NTSInformatica.NTSGridColumn
  Public WithEvents km_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents km_ubicaz As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codiva As NTSInformatica.NTSGridColumn
  Public WithEvents mm_codnomc As NTSInformatica.NTSGridColumn
  Public WithEvents mm_colli As NTSInformatica.NTSGridColumn
  Public WithEvents mm_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents mm_controp As NTSInformatica.NTSGridColumn
  Public WithEvents mm_misura1 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_misura2 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_misura3 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_ornum As NTSInformatica.NTSGridColumn
  Public WithEvents mm_perqta As NTSInformatica.NTSGridColumn
  Public WithEvents mm_prelist As NTSInformatica.NTSGridColumn
  Public WithEvents mm_preziva As NTSInformatica.NTSGridColumn
  Public WithEvents mm_prezvalc As NTSInformatica.NTSGridColumn
  Public WithEvents mm_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents mm_provv As NTSInformatica.NTSGridColumn
  Public WithEvents mm_provv2 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont1 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont2 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont3 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont4 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont5 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_scont6 As NTSInformatica.NTSGridColumn
  Public WithEvents mm_valore As NTSInformatica.NTSGridColumn
  Public WithEvents mm_vprovv As NTSInformatica.NTSGridColumn
  Public WithEvents mm_vprovv2 As NTSInformatica.NTSGridColumn
  Public WithEvents tb_descaum As NTSInformatica.NTSGridColumn
  Public WithEvents tm_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents tm_valuta As NTSInformatica.NTSGridColumn
  Public WithEvents xx_scarichi As NTSInformatica.NTSGridColumn
  Public WithEvents xx_carichi As NTSInformatica.NTSGridColumn
  Public WithEvents xx_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents cmdEsci As NTSInformatica.NTSButton
  Public WithEvents km_conto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
End Class
