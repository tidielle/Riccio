Partial Public Class FRM__HIMG
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
  Public WithEvents pnDescr As NTSInformatica.NTSPanel
  Public WithEvents pnAction As NTSInformatica.NTSPanel
  Public WithEvents cmdSucc As NTSInformatica.NTSButton
  Public WithEvents cmdPrec As NTSInformatica.NTSButton
  Public WithEvents imArtGif As NTSInformatica.NTSPictureBox
  Public WithEvents pnIMG As NTSInformatica.NTSPanel
  Public WithEvents cmdInserisci As NTSInformatica.NTSButton
  Public WithEvents cmdInsIMGAppunti As NTSInformatica.NTSButton
  Public WithEvents cmdClipSost As NTSInformatica.NTSButton
  Public WithEvents cmdSfogliaSost As NTSInformatica.NTSButton
  Public WithEvents lbSostitusci As NTSInformatica.NTSLabel
  Public WithEvents lbInserisci As NTSInformatica.NTSLabel
  Public WithEvents tsHelp As NTSInformatica.NTSTabControl
  Public WithEvents TabPage0 As NTSInformatica.NTSTabPage
  Public WithEvents pnOrigine As NTSInformatica.NTSPanel
  Public WithEvents TabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnDestinazione As NTSInformatica.NTSPanel
  Public WithEvents pnIMGDest As NTSInformatica.NTSPanel
  Public WithEvents imArtGifDest As NTSInformatica.NTSPictureBox
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
End Class
