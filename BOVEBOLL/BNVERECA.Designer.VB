Partial Class FRMVERECA
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
  Public WithEvents lbInfo As NTSInformatica.NTSLabel
  Public WithEvents pnControlli As NTSInformatica.NTSPanel
  Public WithEvents lbCartella As NTSInformatica.NTSLabel
  Public WithEvents FolderBrowserDialog As NTSFolderBrowserDialog
  Public WithEvents cmdCartella As NTSInformatica.NTSButton
  Public WithEvents lbPorta As NTSInformatica.NTSLabel
  Public WithEvents cbPorta As NTSInformatica.NTSComboBox
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents edCartella As NTSInformatica.NTSTextBoxStr
  Public WithEvents fmOpzioni As NTSInformatica.NTSGroupBox
  Public WithEvents ckEsponiQtaPrezzo As NTSInformatica.NTSCheckBox
  Public WithEvents ckEsponiValoreNetto As NTSInformatica.NTSCheckBox
  Public WithEvents lbMaxCaratteriDescrArt As NTSInformatica.NTSLabel
  Public WithEvents edMaxCaratteriDescrArt As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckEsponiScontiAValore As NTSInformatica.NTSCheckBox
  Public WithEvents edScriptTimeOut As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbScriptTimeOut As NTSInformatica.NTSLabel
  Public WithEvents lbScriptTimeOutDescr As NTSInformatica.NTSLabel
  Public WithEvents lbMaxCaratteriDescrArtDescr As NTSInformatica.NTSLabel

End Class