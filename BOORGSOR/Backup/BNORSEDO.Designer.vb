Partial Public Class FRMORSEDO
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

  Public WithEvents cmdSeleziona As NTSButton
  Public WithEvents cmdAnnulla As NTSButton
  Public WithEvents pnfiltri As NTSInformatica.NTSPanel
  Public WithEvents fmEvaso As NTSInformatica.NTSGroupBox
  Public WithEvents optSi As NTSInformatica.NTSRadioButton
  Public WithEvents optTutti As NTSInformatica.NTSRadioButton
  Public WithEvents optNo As NTSInformatica.NTSRadioButton
  Public WithEvents lbAdata As NTSInformatica.NTSLabel
  Public WithEvents lbAdatacons As NTSInformatica.NTSLabel
  Public WithEvents lbDadatacons As NTSInformatica.NTSLabel
  Public WithEvents lbDadata As NTSInformatica.NTSLabel
  Public WithEvents lbAconto As NTSInformatica.NTSLabel
  Public WithEvents lbDaconto As NTSInformatica.NTSLabel
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbSerie As NTSInformatica.NTSLabel
  Public WithEvents edDataA As NTSInformatica.NTSTextBoxData
  Public WithEvents edDataDa As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatConsA As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatConsDa As NTSInformatica.NTSTextBoxData
  Public WithEvents edContoA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edContoDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnCommandbutton As NTSInformatica.NTSPanel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents grSeor As NTSInformatica.NTSGrid
  Public WithEvents grvSeor As NTSInformatica.NTSGridView
  Public WithEvents td_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents td_anno As NTSInformatica.NTSGridColumn
  Public WithEvents td_serie As NTSInformatica.NTSGridColumn
  Public WithEvents td_numord As NTSInformatica.NTSGridColumn
  Public WithEvents td_datord As NTSInformatica.NTSGridColumn
  Public WithEvents td_conto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents td_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents td_totdoc As NTSInformatica.NTSGridColumn
  Public WithEvents td_flevas As NTSInformatica.NTSGridColumn
  Public WithEvents td_confermato As NTSInformatica.NTSGridColumn
  Public WithEvents td_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_destin As NTSInformatica.NTSGridColumn
  Public WithEvents td_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents edRiferim As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbRiferim As NTSInformatica.NTSLabel
  Public WithEvents lbAnno As System.Windows.Forms.Label
  Public WithEvents td_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents td_annpar As NTSInformatica.NTSGridColumn
  Public WithEvents td_alfpar As NTSInformatica.NTSGridColumn
  Public WithEvents td_numpar As NTSInformatica.NTSGridColumn
  Public WithEvents td_datpar As NTSInformatica.NTSGridColumn
  Public WithEvents td_tipobf As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tipobf As NTSInformatica.NTSGridColumn
  Public WithEvents td_flstam As NTSInformatica.NTSGridColumn
  Public WithEvents td_rilasciato As NTSInformatica.NTSGridColumn
  Public WithEvents td_aperto As NTSInformatica.NTSGridColumn
  Public WithEvents td_sospeso As NTSInformatica.NTSGridColumn
  Public WithEvents td_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents xx_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents td_magaz2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_magaz2 As NTSInformatica.NTSGridColumn
  Public WithEvents td_magimp As NTSInformatica.NTSGridColumn
  Public WithEvents xx_magimp As NTSInformatica.NTSGridColumn
  Public WithEvents cbEt_blocco As NTSInformatica.NTSComboBox
  Public WithEvents cbEt_sospeso As NTSInformatica.NTSComboBox
  Public WithEvents lbBlocco As NTSInformatica.NTSLabel
  Public WithEvents lbStato As NTSInformatica.NTSLabel
  Public WithEvents xx_sel As NTSInformatica.NTSGridColumn
  Public WithEvents td_totmerce As NTSInformatica.NTSGridColumn
  Public WithEvents tsSel As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnSel1 As NTSInformatica.NTSPanel
  Public ceFiltriExt As NTSInformatica.NTSXXFILT
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents pnSel2 As NTSInformatica.NTSPanel
  Public WithEvents grFiltri1 As NTSInformatica.NTSGrid
  Public WithEvents grvFiltri1 As NTSInformatica.NTSGridView
  Public WithEvents xx_nome As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valoreda As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valorea As NTSInformatica.NTSGridColumn
  Public WithEvents grFiltri2 As NTSInformatica.NTSGrid
  Public WithEvents grvFiltri2 As NTSInformatica.NTSGridView
  Public WithEvents xx_nome2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valoreda2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valorea2 As NTSInformatica.NTSGridColumn
  Public WithEvents cmdLock As NTSInformatica.NTSButton
  Public WithEvents cmdDesAll As NTSInformatica.NTSButton
  Public WithEvents cmdSelAll As NTSInformatica.NTSButton
  Friend WithEvents NtsGridView1 As NTSInformatica.NTSGridView
  Friend WithEvents NtsGridView2 As NTSInformatica.NTSGridView
End Class
