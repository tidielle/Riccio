Partial Public Class FRMVEFADI
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
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents grFadi As NTSInformatica.NTSGrid
  Public WithEvents grvFadi As NTSInformatica.NTSGridView
  Public WithEvents xx_seleziona As NTSInformatica.NTSGridColumn
  Public WithEvents tm_anno As NTSInformatica.NTSGridColumn
  Public WithEvents tm_numdoc As NTSInformatica.NTSGridColumn
  Public WithEvents tm_serie As NTSInformatica.NTSGridColumn
  Public WithEvents tm_datdoc As NTSInformatica.NTSGridColumn
  Public WithEvents tm_totdoc As NTSInformatica.NTSGridColumn
  Public WithEvents tm_conto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRielabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaPdf As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbBolleCollegate As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGeneraConad As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbSelAll As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbDeselAll As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbInvertiSel As NTSInformatica.NTSBarMenuItem
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents xx_rielab As NTSInformatica.NTSGridColumn
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
  Public WithEvents tlbNumerazioni As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbEmail As NTSInformatica.NTSBarButtonItem
  Public WithEvents tm_flagiva_1 As NTSInformatica.NTSGridColumn
End Class
