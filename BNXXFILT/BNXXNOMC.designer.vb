Partial Public Class FRMXXNOMC
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



  Public WithEvents grCampi As NTSInformatica.NTSGrid
  Public WithEvents grvCampi As NTSInformatica.NTSGridView
  Public WithEvents cb_destab As NTSInformatica.NTSGridColumn
  Public WithEvents cb_tipocampo As NTSInformatica.NTSGridColumn
  Public WithEvents cb_descampo As NTSInformatica.NTSGridColumn
  Public WithEvents pnGriglia As NTSInformatica.NTSPanel
  Public WithEvents pnSeleziona As NTSInformatica.NTSPanel
  Public WithEvents pnRicerca As NTSInformatica.NTSPanel
  Public WithEvents cb_nomcampo As NTSInformatica.NTSGridColumn
  Public WithEvents lbRicerca As NTSInformatica.NTSLabel
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents edRicerca As NTSInformatica.NTSTextBoxStr


End Class
