Partial Public Class FRMVECAST
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

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grControp As NTSInformatica.NTSGrid
  Public WithEvents grvControp As NTSInformatica.NTSGridView
  Public WithEvents xx_ccontr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_impcont As NTSInformatica.NTSGridColumn
  Public WithEvents xx_impcontv As NTSInformatica.NTSGridColumn
  Public WithEvents fmControp As NTSInformatica.NTSGroupBox
  Public WithEvents fmIva As NTSInformatica.NTSGroupBox
  Public WithEvents lbDiffrighecorpo As NTSInformatica.NTSLabel
  Public WithEvents edDiffrighecorpo As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDiffDA As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDiffDA As NTSInformatica.NTSLabel
  Public WithEvents edDtcomiva As NTSInformatica.NTSTextBoxData
  Public WithEvents cmdControlla As NTSInformatica.NTSButton
  Public WithEvents edDiffIva As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDtcomiva As NTSInformatica.NTSLabel
  Public WithEvents lbDiffIva As NTSInformatica.NTSLabel
  Public WithEvents grIva As NTSInformatica.NTSGrid
  Public WithEvents grvIva As NTSInformatica.NTSGridView
  Public WithEvents xx_codiva As NTSInformatica.NTSGridColumn
  Public WithEvents xx_imponib As NTSInformatica.NTSGridColumn
  Public WithEvents xx_imposta As NTSInformatica.NTSGridColumn
  Public WithEvents xx_imponibv As NTSInformatica.NTSGridColumn
  Public WithEvents xx_impostav As NTSInformatica.NTSGridColumn
  Public WithEvents xx_id As NTSInformatica.NTSGridColumn
  Public WithEvents xx_idiva As NTSInformatica.NTSGridColumn
End Class
