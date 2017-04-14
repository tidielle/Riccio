Partial Public Class FRMMGGRCF
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
  Public WithEvents km_um As NTSInformatica.NTSGridColumn
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents pbBottom As NTSInformatica.NTSPanel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents ar_desint As NTSInformatica.NTSGridColumn
 
  'Required by the Windows Form Designer
  'Private components As System.ComponentModel.IContainer

End Class
