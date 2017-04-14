Partial Public Class FRMORIMP1
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
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbNavigazMrp As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grRighe As NTSInformatica.NTSGrid
  Public WithEvents grvRighe As NTSInformatica.NTSGridView
  Public WithEvents ec_riga As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ec_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ec_desint As NTSInformatica.NTSGridColumn
  Public WithEvents ec_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents ec_colli As NTSInformatica.NTSGridColumn
  Public WithEvents ec_ump As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant As NTSInformatica.NTSGridColumn
  Public WithEvents ec_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents ec_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents ec_stato As NTSInformatica.NTSGridColumn
  Public WithEvents ec_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents ec_datord As NTSInformatica.NTSGridColumn
  Public WithEvents ec_controp As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_controp As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codiva As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codiva As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents ec_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents ec_subcommeca As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents ec_note As NTSInformatica.NTSGridColumn
  Public WithEvents ec_valore As NTSInformatica.NTSGridColumn
  Public WithEvents ec_contocontr As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_contocon As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codclie As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codclie As NTSInformatica.NTSGridColumn
  Public WithEvents ec_misura1 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_misura2 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_misura3 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_perqta As NTSInformatica.NTSGridColumn
  Public WithEvents ec_flcom As NTSInformatica.NTSGridColumn
  Public WithEvents ec_flprznet As NTSInformatica.NTSGridColumn
  Public WithEvents ec_flforf As NTSInformatica.NTSGridColumn
  Public WithEvents ec_matric As NTSInformatica.NTSGridColumn
  Public WithEvents ec_umprz As NTSInformatica.NTSGridColumn
  Public WithEvents ec_fase As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_fase As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codlavo As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codlavo As NTSInformatica.NTSGridColumn
  Public WithEvents ec_datini As NTSInformatica.NTSGridColumn
  Public WithEvents ec_datfin As NTSInformatica.NTSGridColumn
  Public WithEvents ec_valorev As NTSInformatica.NTSGridColumn
  Public WithEvents ec_pmtaskid As NTSInformatica.NTSGridColumn
  Public WithEvents ec_pmsalcon As NTSInformatica.NTSGridColumn
  Public WithEvents ec_pmqtadis As NTSInformatica.NTSGridColumn
  Public WithEvents ec_pmvaldis As NTSInformatica.NTSGridColumn
  Public WithEvents ec_rdatipork As NTSInformatica.NTSGridColumn
  Public WithEvents ec_rdaanno As NTSInformatica.NTSGridColumn
  Public WithEvents ec_rdaserie As NTSInformatica.NTSGridColumn
  Public WithEvents ec_rdanum As NTSInformatica.NTSGridColumn
  Public WithEvents ec_rdariga As NTSInformatica.NTSGridColumn
  Public WithEvents ec_offreq As NTSInformatica.NTSGridColumn
  Public WithEvents ec_ortipork As NTSInformatica.NTSGridColumn
  Public WithEvents ec_oranno As NTSInformatica.NTSGridColumn
  Public WithEvents ec_orserie As NTSInformatica.NTSGridColumn
  Public WithEvents ec_ornum As NTSInformatica.NTSGridColumn
  Public WithEvents ec_orriga As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codtagl As NTSInformatica.NTSGridColumn
  Public WithEvents ec_tctaglia As NTSInformatica.NTSGridColumn
  Public WithEvents ec_prezvalc As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_tctagliaf As NTSInformatica.NTSGridColumn
End Class
