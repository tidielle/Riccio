Partial Public Class FRMMGSELI
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

  Public WithEvents GridColumn16 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn17 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn18 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn19 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn110 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn111 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn112 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn113 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn114 As NTSInformatica.NTSGridColumn
  Public WithEvents pnCommandbutton As NTSInformatica.NTSPanel
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents ckCancella As NTSInformatica.NTSCheckBox
  Public WithEvents grList As NTSInformatica.NTSGrid
  Public WithEvents grvList As NTSInformatica.NTSGridView
  Public WithEvents tb_lsarnote As NTSInformatica.NTSGridColumn
  Public WithEvents tb_codlsar As NTSInformatica.NTSGridColumn
  Public WithEvents tb_deslsar As NTSInformatica.NTSGridColumn
  Public WithEvents tb_opnomest As NTSInformatica.NTSGridColumn
  Public WithEvents lbNome As NTSInformatica.NTSLabel
  Public WithEvents lbDescr As NTSInformatica.NTSLabel
  Public WithEvents edOperatore As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDescr As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmRicerca As NTSInformatica.NTSButton
  Public WithEvents lbInfo As NTSInformatica.NTSLabel
End Class
