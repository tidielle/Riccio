Partial Public Class FROVEBOLL
    '   Inherits FRM__CHIL
    Inherits FRMVEBOLL
    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New()
        MyBase.New()

    End Sub
    ' Private components As System.ComponentModel.IContainer

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'If disposing AndAlso components IsNot Nothing Then
        '    components.Dispose()
        'End If
        MyBase.Dispose(disposing)
    End Sub
    Public WithEvents tlb_hhnewdoc As NTSInformatica.NTSBarMenuItem
End Class
