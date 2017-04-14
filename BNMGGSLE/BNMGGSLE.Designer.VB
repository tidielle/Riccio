Partial Public Class FRMMGGSLE
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
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnPanel1 As NTSInformatica.NTSPanel
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents pnPanel2 As NTSInformatica.NTSPanel
  Public WithEvents grGsle As NTSInformatica.NTSGrid
  Public WithEvents grvGsle As NTSInformatica.NTSGridView
  Public WithEvents xx_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents lp_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents lp_fase As NTSInformatica.NTSGridColumn
  Public WithEvents xx_fase As NTSInformatica.NTSGridColumn
  Public WithEvents lp_ubicaz As NTSInformatica.NTSGridColumn
  Public WithEvents lp_esist As NTSInformatica.NTSGridColumn
  Public WithEvents lp_qtalif As NTSInformatica.NTSGridColumn
  Public WithEvents lp_vqtalif As NTSInformatica.NTSGridColumn
  Public WithEvents lp_giaini As NTSInformatica.NTSGridColumn
  Public WithEvents lp_vgiaini As NTSInformatica.NTSGridColumn
  Public WithEvents lp_prenot As NTSInformatica.NTSGridColumn
  Public WithEvents lp_vprenot As NTSInformatica.NTSGridColumn
  Public WithEvents lp_ordin As NTSInformatica.NTSGridColumn
  Public WithEvents lp_vordin As NTSInformatica.NTSGridColumn
  Public WithEvents lp_impeg As NTSInformatica.NTSGridColumn
  Public WithEvents lp_vimpeg As NTSInformatica.NTSGridColumn

End Class