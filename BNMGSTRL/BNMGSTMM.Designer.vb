Partial Public Class FRMMGSTMM
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
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents cmdCancel As NTSInformatica.NTSButton
  Public WithEvents fmPrinterType As NTSInformatica.NTSGroupBox
  Public WithEvents opPrintPdf As NTSInformatica.NTSRadioButton
  Public WithEvents opPrintPaper As NTSInformatica.NTSRadioButton
  Public WithEvents opPrintMonitor As NTSInformatica.NTSRadioButton
  Public WithEvents cmdSelectAll As NTSInformatica.NTSButton
  Public WithEvents cmdDeselectAll As NTSInformatica.NTSButton
  Public WithEvents grWarehouse As NTSInformatica.NTSGrid
  Public WithEvents grvWarehouse As NTSInformatica.NTSGridView
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents xx_seleziona As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codmaga As NTSInformatica.NTSGridColumn
  Public WithEvents tb_desmaga As NTSInformatica.NTSGridColumn

End Class
