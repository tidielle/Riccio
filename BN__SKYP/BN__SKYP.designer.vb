Partial Public Class FRM__SKYP
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
  Public WithEvents pnAll As NTSInformatica.NTSPanel
  Public WithEvents fmOrganig As NTSInformatica.NTSGroupBox
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents lbDesConto As NTSInformatica.NTSLabel
  Public WithEvents lbCliente As NTSInformatica.NTSLabel
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdEsci As NTSInformatica.NTSButton
  Public WithEvents pnOrgaBottom As NTSInformatica.NTSPanel
  Public WithEvents pnOrganig As NTSInformatica.NTSPanel
  Public WithEvents grOrga As NTSInformatica.NTSGrid
  Public WithEvents grvOrga As NTSInformatica.NTSGridView
  Public WithEvents og_descont2 As NTSInformatica.NTSGridColumn
  Public WithEvents og_descont As NTSInformatica.NTSGridColumn
  Public WithEvents og_codruaz As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codruaz As NTSInformatica.NTSGridColumn
  Public WithEvents og_telef As NTSInformatica.NTSGridColumn
  Public WithEvents og_cell As NTSInformatica.NTSGridColumn
  Public WithEvents cmdChiamaCellOrg As NTSInformatica.NTSButton
  Public WithEvents cmdChiamaTelOrg As NTSInformatica.NTSButton
  Public WithEvents pnConto As NTSInformatica.NTSPanel
  Public WithEvents cbTipo As NTSInformatica.NTSComboBox
End Class
