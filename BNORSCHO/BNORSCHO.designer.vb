Partial Public Class FRMORSCHO
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
  Public WithEvents NtsGridColumn1 As NTSInformatica.NTSGridColumn
  Public WithEvents NtsGridColumn2 As NTSInformatica.NTSGridColumn
  Public WithEvents NtsGridColumn3 As NTSInformatica.NTSGridColumn
  Public WithEvents tsScho As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnLeft As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents pnFiltri2 As NTSInformatica.NTSPanel
  Public WithEvents cmdLock As NTSInformatica.NTSButton
  Public WithEvents grFiltri1 As NTSInformatica.NTSGrid
  Public WithEvents grvFiltri1 As NTSInformatica.NTSGridView
  Public WithEvents xx_nome As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valoreda As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valorea As NTSInformatica.NTSGridColumn
  Public WithEvents ckSerie As NTSInformatica.NTSCheckBox
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents cmdApriFiltri As NTSInformatica.NTSButton
  Public WithEvents cbFiltro As NTSInformatica.NTSComboBox
  Public WithEvents lbFiltri As NTSInformatica.NTSLabel
  Public WithEvents lbTipoStampa As NTSInformatica.NTSLabel
  Public WithEvents pnFilterSx As NTSInformatica.NTSPanel
  Public WithEvents lbOrdinamento As NTSInformatica.NTSLabel
  Public WithEvents pnTipoStampa As NTSInformatica.NTSPanel
  Public WithEvents lbVert As NTSInformatica.NTSLabel
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents pnPianificazione As NTSInformatica.NTSPanel
  Public WithEvents cmdClassificaDeleteFilter As NTSInformatica.NTSButton
  Public WithEvents cmdClassifica As NTSInformatica.NTSButton
  Public WithEvents lbClassifica As NTSInformatica.NTSLabel
  Public WithEvents edClassificazioneLivello5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edClassificazioneLivello4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edClassificazioneLivello3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edClassificazioneLivello2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edClassificazioneLivello1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbFaseini As NTSInformatica.NTSLabel
  Public WithEvents lbDescodlsar As NTSInformatica.NTSLabel
  Public WithEvents edCodlsar As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbCodart As NTSInformatica.NTSComboBox
  Public WithEvents cbConto As NTSInformatica.NTSComboBox
  Public WithEvents lbDescodlsel As NTSInformatica.NTSLabel
  Public WithEvents edCodlsel As NTSInformatica.NTSTextBoxNum
  Public WithEvents tlbNoModal As NTSInformatica.NTSBarMenuItem
  Public WithEvents ceFiltriExt As NTSInformatica.NTSXXFILT
  Public WithEvents pnAll As NTSInformatica.NTSPanel
  Public WithEvents tlbAccorpa As NTSInformatica.NTSBarMenuItem

End Class
