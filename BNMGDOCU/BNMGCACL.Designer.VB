Partial Class FRMMGCACL
  Inherits FRM__CHIL

  <System.Diagnostics.DebuggerNonUserCode()> _
  Public Sub New()
    MyBase.New()

    ''This call is required by the Windows Form Designer.
    'InitializeComponent()

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
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents lbInfo As NTSInformatica.NTSLabel
  Public WithEvents grCacl As NTSInformatica.NTSGrid
  Public WithEvents grvCacl As NTSInformatica.NTSGridView
  Public WithEvents ae_forn As NTSInformatica.NTSGridColumn
  Public WithEvents xx_forn As NTSInformatica.NTSGridColumn
  Public WithEvents ae_siglaforn As NTSInformatica.NTSGridColumn
  Public WithEvents ae_codmarc As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codmarc As NTSInformatica.NTSGridColumn
  Public WithEvents ae_status As NTSInformatica.NTSGridColumn
  Public WithEvents ae_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ae_desint As NTSInformatica.NTSGridColumn

  'Required by the Windows Form Designer
  'Private components As System.ComponentModel.IContainer
End Class