Partial Class FRMMGDEPR
  Inherits FRM__CHIL

  <System.Diagnostics.DebuggerNonUserCode()> _
  Public Sub New()
    MyBase.New()

    ''This call is required by the Windows Form Designer.
    'InitializeComponent()

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
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents lbFase As NTSInformatica.NTSLabel
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents lbXx_fase As NTSInformatica.NTSLabel
  Public WithEvents lbXx_magaz As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codart As NTSInformatica.NTSLabel
  Public WithEvents lbAr_fase As NTSInformatica.NTSLabel
  Public WithEvents lbAr_magaz As NTSInformatica.NTSLabel
  Public WithEvents lbAr_codart As NTSInformatica.NTSLabel
  Public WithEvents grDepr As NTSInformatica.NTSGrid
  Public WithEvents grvDepr As NTSInformatica.NTSGridView
  Public WithEvents lp_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents lp_lotto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lotto As NTSInformatica.NTSGridColumn
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
  Public WithEvents tlbCommessa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUbicazioni As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbLotti As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDettTCO As NTSInformatica.NTSBarButtonItem

  'Required by the Windows Form Designer
  'Private components As System.ComponentModel.IContainer
End Class