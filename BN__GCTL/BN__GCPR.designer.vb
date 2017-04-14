Partial Public Class FRM__GCPR
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

  Public WithEvents lbAnchor As System.Windows.Forms.Label
  Public WithEvents cmdAtop As System.Windows.Forms.Label
  Public WithEvents pnAnchor As NTSPanel
  Public WithEvents cmdAbottom As System.Windows.Forms.Label
  Public WithEvents cmdAleft As System.Windows.Forms.Label
  Public WithEvents cmdAright As System.Windows.Forms.Label
  Public WithEvents lbDock As System.Windows.Forms.Label
  Public WithEvents pnDock As NTSPanel
  Public WithEvents cmdDbottom As System.Windows.Forms.Label
  Public WithEvents cmdDleft As System.Windows.Forms.Label
  Public WithEvents cmdDright As System.Windows.Forms.Label
  Public WithEvents cmdDtop As System.Windows.Forms.Label
  Public WithEvents cmdDfill As System.Windows.Forms.Label
  Public WithEvents cmdDnone As System.Windows.Forms.Label
  Public WithEvents lbName As System.Windows.Forms.Label
  Public WithEvents Label1 As System.Windows.Forms.Label
  Public WithEvents Label2 As System.Windows.Forms.Label
  Public WithEvents Label3 As System.Windows.Forms.Label
  Public WithEvents Label4 As System.Windows.Forms.Label
  Public WithEvents lbEsci As System.Windows.Forms.Label
  Public WithEvents lbSavePosition As System.Windows.Forms.Label
  Public WithEvents ckMultiline As NTSInformatica.NTSCheckBox
  Public WithEvents edTop As NTSInformatica.NTSTextBoxNum
  Public WithEvents edLeft As NTSInformatica.NTSTextBoxNum
  Public WithEvents edHeight As NTSInformatica.NTSTextBoxNum
  Public WithEvents edWidth As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckAutosize As NTSInformatica.NTSCheckBox
  Public WithEvents ckBorder As NTSInformatica.NTSCheckBox
  Public WithEvents lbCancella As System.Windows.Forms.Label
  Public WithEvents ckVisLabel As NTSInformatica.NTSCheckBox
  Public WithEvents edParent As NTSInformatica.NTSTextBoxStr
  Public WithEvents Label5 As System.Windows.Forms.Label

End Class
