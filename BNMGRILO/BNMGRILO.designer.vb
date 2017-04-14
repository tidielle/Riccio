Partial Public Class FRMMGRILO
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
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents pnSx As NTSInformatica.NTSPanel
  Public WithEvents pnDx As NTSInformatica.NTSPanel
  Public WithEvents NtsSplitter1 As NTSInformatica.NTSSplitter
  Public WithEvents cbRecent As NTSInformatica.NTSComboBox
  Public WithEvents lbRecent As NTSInformatica.NTSLabel
  Public WithEvents lbNota As NTSInformatica.NTSLabel
  Public WithEvents edCodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents edLotto As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbLotto As NTSInformatica.NTSLabel
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents lbDesart As NTSInformatica.NTSLabel
  Public WithEvents grCp As NTSInformatica.NTSGrid
  Public WithEvents lbNota2 As NTSInformatica.NTSLabel
  Public WithEvents pnMain As NTSInformatica.NTSPanel
  Public WithEvents grvCp As NTSInformatica.NTSGridView
  Public WithEvents grvRilo As NTSInformatica.NTSGridView
  Public WithEvents tm_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents tm_datdoc As NTSInformatica.NTSGridColumn
  Public WithEvents tm_numdoc As NTSInformatica.NTSGridColumn
  Public WithEvents tm_serie As NTSInformatica.NTSGridColumn
  Public WithEvents tm_conto As NTSInformatica.NTSGridColumn
  Public WithEvents an_descr1 As NTSInformatica.NTSGridColumn
  Public WithEvents km_causale As NTSInformatica.NTSGridColumn
  Public WithEvents tb_descaum As NTSInformatica.NTSGridColumn
  Public WithEvents mm_ump As NTSInformatica.NTSGridColumn
  Public WithEvents mm_quant As NTSInformatica.NTSGridColumn
  Public WithEvents tm_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents omm_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents oxx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents omm_codart As NTSInformatica.NTSGridColumn
  Public WithEvents omm_descr As NTSInformatica.NTSGridColumn
  Public WithEvents omm_riga As NTSInformatica.NTSGridColumn
  Public WithEvents omm_ump As NTSInformatica.NTSGridColumn
  Public WithEvents omm_quant As NTSInformatica.NTSGridColumn
  Public WithEvents mm_riga As NTSInformatica.NTSGridColumn
  Public WithEvents tlbCerca2 As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbMatricola As NTSInformatica.NTSLabel
  Public WithEvents edMatricola As NTSInformatica.NTSTextBoxStr
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbLottoMatr As NTSInformatica.NTSBarMenuItem
  Public WithEvents omma_matric As NTSInformatica.NTSGridColumn
  Public WithEvents omma_quant As NTSInformatica.NTSGridColumn

End Class
