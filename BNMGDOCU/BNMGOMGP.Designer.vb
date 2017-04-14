Partial Class FRMMGOMGP
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

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnBottoni As NTSInformatica.NTSPanel
  Public WithEvents cmdEsci As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents pnGiacenza As NTSInformatica.NTSPanel
  Public WithEvents grOmgp As NTSInformatica.NTSGrid
  Public WithEvents grvOmgp As NTSInformatica.NTSGridView
  Public WithEvents xx_sel As NTSInformatica.NTSGridColumn
  Public WithEvents ar_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ar_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ar_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents xx_quant As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codrepr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desrepr As NTSInformatica.NTSGridColumn
  Public WithEvents pnBottomNoEdit As NTSInformatica.NTSPanel
  Public WithEvents cmdDx As NTSInformatica.NTSButton
  Public WithEvents cmdGiu As NTSInformatica.NTSButton
  Public WithEvents cmdSu As NTSInformatica.NTSButton
  Public WithEvents cmdSx As NTSInformatica.NTSButton
  Public WithEvents xx_maxquant As NTSInformatica.NTSGridColumn

End Class
