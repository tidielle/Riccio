Partial Public Class FRMMGARTI_F
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
  Public WithEvents lbXx_clascon As NTSInformatica.NTSLabel
  Public WithEvents lbXx_claprov As NTSInformatica.NTSLabel
  Public WithEvents lbAr_claprov As NTSInformatica.NTSLabel
  Public WithEvents edAr_claprov As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_clascon As NTSInformatica.NTSLabel
  Public WithEvents edAr_clascon As NTSInformatica.NTSTextBoxNum
  Public WithEvents edFocus As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsGroupBox1 As NTSInformatica.NTSGroupBox
  Public WithEvents lbXx_codpdon As NTSInformatica.NTSLabel
  Public WithEvents lbAr_tipokit As NTSInformatica.NTSLabel
  Public WithEvents cbAr_tipokit As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_codpdon As NTSInformatica.NTSLabel
  Public WithEvents edAr_codpdon As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_reparto As NTSInformatica.NTSLabel
  Public WithEvents edAr_reparto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_perqta As NTSInformatica.NTSLabel
  Public WithEvents edAr_perqta As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmAcquisti As NTSInformatica.NTSGroupBox
  Public WithEvents lbXx_forn As NTSInformatica.NTSLabel
  Public WithEvents lbXx_forn2 As NTSInformatica.NTSLabel
  Public WithEvents lbAr_scomax As NTSInformatica.NTSLabel
  Public WithEvents lbAr_scomin As NTSInformatica.NTSLabel
  Public WithEvents edAr_scomin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_scomax As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_forn As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_forn As NTSInformatica.NTSLabel
  Public WithEvents edAr_forn2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_forn2 As NTSInformatica.NTSLabel
  Public WithEvents cmdArtgif2 As NTSInformatica.NTSButton
  Public WithEvents cmdArtGif1 As NTSInformatica.NTSButton
  Public WithEvents cmdVisGif2 As NTSInformatica.NTSButton
  Public WithEvents cmdVisGif1 As NTSInformatica.NTSButton
  Public WithEvents edAr_gif2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_gif2 As NTSInformatica.NTSLabel
  Public WithEvents edAr_gif1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_gif1 As NTSInformatica.NTSLabel
  Public WithEvents lbAr_flricmar As NTSInformatica.NTSLabel
  Public WithEvents cbAr_flricmar As NTSInformatica.NTSComboBox
  Public WithEvents edAr_ricar1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_ricar2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents imArtGif As NTSInformatica.NTSPictureBox
  Public WithEvents tlbBarcode As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbXx_reparto As NTSInformatica.NTSLabel
  Public WithEvents tlbSimula As NTSInformatica.NTSBarMenuItem

End Class