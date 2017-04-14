Partial Public Class FRMMGELSA
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
  Public WithEvents tlbImpostaStato As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbFileOrdina As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbFileOrdinaDescr As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbFileOrdinaCodalt As NTSInformatica.NTSBarMenuItem
  Public WithEvents lsa_tctaglia As NTSInformatica.NTSGridColumn
  Public WithEvents tlbTrattatoS As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbTrattatoN As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbImpostaTerm As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbImportaTerm As NTSInformatica.NTSBarMenuItem
  Public WithEvents xx_sel As NTSInformatica.NTSGridColumn
  Public WithEvents tlbSeleziona As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDeseleziona As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancellaRigheSel As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSelezLottoUbicaz As NTSInformatica.NTSBarButtonItem

End Class
