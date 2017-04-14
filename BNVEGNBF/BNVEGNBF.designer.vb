Partial Public Class FRMVEGNBF
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
  Public WithEvents fd_ornum As NTSInformatica.NTSGridColumn
  Public WithEvents fd_orserie As NTSInformatica.NTSGridColumn
  Public WithEvents fd_oranno As NTSInformatica.NTSGridColumn
  Public WithEvents fd_ordata As NTSInformatica.NTSGridColumn
  Public WithEvents fd_soloasa As NTSInformatica.NTSGridColumn
  Public WithEvents fd_tdflevas As NTSInformatica.NTSGridColumn
  Public WithEvents fd_codpaga As NTSInformatica.NTSGridColumn
  Public WithEvents fd_despaga As NTSInformatica.NTSGridColumn
  Public WithEvents fd_codtpbf As NTSInformatica.NTSGridColumn
  Public WithEvents fd_destpbf As NTSInformatica.NTSGridColumn
End Class
