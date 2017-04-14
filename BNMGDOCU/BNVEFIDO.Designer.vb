Partial Public Class FRMVEFIDO
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
  Public WithEvents lbAdata As NTSInformatica.NTSLabel
  Public WithEvents lbATipobf As NTSInformatica.NTSLabel
  Public WithEvents lbDaTipobf As NTSInformatica.NTSLabel
  Public WithEvents lbDadata As NTSInformatica.NTSLabel
  Public WithEvents lbAconto As NTSInformatica.NTSLabel
  Public WithEvents lbDaconto As NTSInformatica.NTSLabel
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbSerie As NTSInformatica.NTSLabel
  Public WithEvents edDataA As NTSInformatica.NTSTextBoxData
  Public WithEvents edDataDa As NTSInformatica.NTSTextBoxData
  Public WithEvents edContoA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edContoDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents grSedoc As NTSInformatica.NTSGrid
  Public WithEvents grvSedoc As NTSInformatica.NTSGridView
  Public WithEvents tm_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents tm_anno As NTSInformatica.NTSGridColumn
  Public WithEvents tm_serie As NTSInformatica.NTSGridColumn
  Public WithEvents tm_numdoc As NTSInformatica.NTSGridColumn
  Public WithEvents tm_datdoc As NTSInformatica.NTSGridColumn
  Public WithEvents tm_conto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents tm_vistato As NTSInformatica.NTSGridColumn
  Public WithEvents tm_totdoc As NTSInformatica.NTSGridColumn
  Public WithEvents tm_totacceva As NTSInformatica.NTSGridColumn
  Public WithEvents xx_residuo As NTSInformatica.NTSGridColumn
  Public WithEvents tm_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_destin As NTSInformatica.NTSGridColumn
  Public WithEvents tm_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents edRiferim As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbRiferim As NTSInformatica.NTSLabel
  Public WithEvents edDaTipobf As NTSInformatica.NTSTextBoxNum
  Public WithEvents edATipobf As NTSInformatica.NTSTextBoxNum
  Public WithEvents tm_codpaga As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codpaga As NTSInformatica.NTSGridColumn
  Public WithEvents tm_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents tm_annpar As NTSInformatica.NTSGridColumn
  Public WithEvents tm_alfpar As NTSInformatica.NTSGridColumn
  Public WithEvents tm_numpar As NTSInformatica.NTSGridColumn
  Public WithEvents tm_datpar As NTSInformatica.NTSGridColumn
  Public WithEvents lbAnno As System.Windows.Forms.Label
  Public WithEvents tm_tipobf As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tipobf As NTSInformatica.NTSGridColumn
  Public WithEvents edNumpar As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbNumpar As NTSInformatica.NTSLabel
  Public WithEvents xx_sel As NTSInformatica.NTSGridColumn
  Public WithEvents tsSel As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents pnSel1 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnSel0 As NTSInformatica.NTSPanel
  Public WithEvents ceFiltriExt As NTSInformatica.NTSXXFILT
  Public WithEvents pnCommandbutton As NTSInformatica.NTSPanel
  '--- Non più utilizzati ---
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
  Public WithEvents pnSel2 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents cmdDesAll As NTSInformatica.NTSButton
  Public WithEvents cmdSelAll As NTSInformatica.NTSButton
  Public WithEvents NtsGridView1 As NTSInformatica.NTSGridView
  Public WithEvents NtsGridView2 As NTSInformatica.NTSGridView
End Class
