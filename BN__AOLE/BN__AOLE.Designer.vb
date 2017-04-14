Partial Class FRM__AOLE
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
  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRiparti As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApriCon As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grAole As NTSInformatica.NTSGrid
  Public WithEvents grvAole As NTSInformatica.NTSGridView
  Public WithEvents NtsGridView1 As NTSInformatica.NTSGridView
  Public WithEvents ao_tipo As NTSInformatica.NTSGridColumn
  Public WithEvents ao_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents ao_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ao_descr2 As NTSInformatica.NTSGridColumn
  Public WithEvents ao_cartella As NTSInformatica.NTSGridColumn
  Public WithEvents ao_nomedoc As NTSInformatica.NTSGridColumn
  Public WithEvents xx_nomedoc As NTSInformatica.NTSGridColumn
  Public WithEvents ao_argom As NTSInformatica.NTSGridColumn
  Public WithEvents ao_ubicaz As NTSInformatica.NTSGridColumn
  Public WithEvents ao_autore As NTSInformatica.NTSGridColumn
  Public WithEvents ao_redattore As NTSInformatica.NTSGridColumn
  Public WithEvents ao_codice As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr1 As NTSInformatica.NTSGridColumn
  Public WithEvents ao_controp As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descovg As NTSInformatica.NTSGridColumn
  Public WithEvents ao_strcod As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ao_tipodoc As NTSInformatica.NTSGridColumn
  Public WithEvents ao_annodoc As NTSInformatica.NTSGridColumn
  Public WithEvents ao_seriedoc As NTSInformatica.NTSGridColumn
  Public WithEvents ao_numdoc As NTSInformatica.NTSGridColumn
  Public WithEvents ao_rigadoc As NTSInformatica.NTSGridColumn
  Public WithEvents ao_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr1_commess As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents ao_matric As NTSInformatica.NTSGridColumn
  Public WithEvents ao_ultagg As NTSInformatica.NTSGridColumn
  Public WithEvents ao_datins As NTSInformatica.NTSGridColumn
  Public WithEvents ao_codlead As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr1_lead As NTSInformatica.NTSGridColumn
  Public WithEvents ao_codoppo As NTSInformatica.NTSGridColumn
  Public WithEvents xx_oggetto As NTSInformatica.NTSGridColumn
  Public WithEvents ao_codchia As NTSInformatica.NTSGridColumn
  Public WithEvents xx_oggetto_nnchiam As NTSInformatica.NTSGridColumn
  Public WithEvents ao_numcontr As NTSInformatica.NTSGridColumn
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents ao_progress As NTSInformatica.NTSGridColumn
  Public WithEvents ao_classe As NTSInformatica.NTSGridColumn
  Public WithEvents ao_classeole As NTSInformatica.NTSGridColumn
  Public WithEvents ao_ole As NTSInformatica.NTSGridColumn
  Public WithEvents ao_progresl As NTSInformatica.NTSGridColumn
  Public WithEvents FolderBrowserDialog As NTSFolderBrowserDialog
  Public WithEvents xx_oggetto_off As NTSInformatica.NTSGridColumn

End Class
