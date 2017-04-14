Partial Public Class FRMORIMPE
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
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbInsRiga As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCopiaRiga As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbIncollaRiga As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbSelezLotto As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbSelezUbicazione As NTSInformatica.NTSBarMenuItem
  Public WithEvents ec_riga As NTSInformatica.NTSGridColumn
  Public WithEvents ec_matric As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ec_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ec_desint As NTSInformatica.NTSGridColumn
  Public WithEvents ec_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents ec_colli As NTSInformatica.NTSGridColumn
  Public WithEvents ec_colpre As NTSInformatica.NTSGridColumn
  Public WithEvents ec_coleva As NTSInformatica.NTSGridColumn
  Public WithEvents ec_ump As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quapre As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quaeva As NTSInformatica.NTSGridColumn
  Public WithEvents ec_flevapre As NTSInformatica.NTSGridColumn
  Public WithEvents ec_flevas As NTSInformatica.NTSGridColumn
  Public WithEvents ec_preziva As NTSInformatica.NTSGridColumn
  Public WithEvents ec_prezvalc As NTSInformatica.NTSGridColumn
  Public WithEvents ec_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents ec_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents ec_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents ec_confermato As NTSInformatica.NTSGridColumn
  Public WithEvents ec_rilasciato As NTSInformatica.NTSGridColumn
  Public WithEvents ec_aperto As NTSInformatica.NTSGridColumn
  Public WithEvents ec_ricimp As NTSInformatica.NTSGridColumn
  Public WithEvents ec_provv As NTSInformatica.NTSGridColumn
  Public WithEvents ec_vprovv As NTSInformatica.NTSGridColumn
  Public WithEvents ec_provv2 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_vprovv2 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_controp As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_controp As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codiva As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codiva As NTSInformatica.NTSGridColumn
  Public WithEvents ec_stasino As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codcfam As NTSInformatica.NTSGridColumn
  Public WithEvents ec_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents ec_subcommeca As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_lottox As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codcena As NTSInformatica.NTSGridColumn
  Public WithEvents ec_note As NTSInformatica.NTSGridColumn
  Public WithEvents ec_magaz2 As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_magaz2 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_valore As NTSInformatica.NTSGridColumn
  Public WithEvents ec_contocontr As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_contocon As NTSInformatica.NTSGridColumn
  Public WithEvents ec_datconsor As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codclie As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codclie As NTSInformatica.NTSGridColumn
  Public WithEvents ec_misura1 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_misura2 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_misura3 As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codarfo As NTSInformatica.NTSGridColumn
  Public WithEvents ec_perqta As NTSInformatica.NTSGridColumn
  Public WithEvents ec_valoremm As NTSInformatica.NTSGridColumn
  Public WithEvents ec_flkit As NTSInformatica.NTSGridColumn
  Public WithEvents ec_ktriga As NTSInformatica.NTSGridColumn
  Public WithEvents ec_umprz As NTSInformatica.NTSGridColumn
  Public WithEvents ec_fase As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_fase As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codlavo As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codlavo As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_darave As NTSInformatica.NTSGridColumn
  Public WithEvents ec_ubicaz As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codtagl As NTSInformatica.NTSGridColumn
  Public WithEvents ec_flprznet As NTSInformatica.NTSGridColumn
  Public WithEvents ec_tctaglia As NTSInformatica.NTSGridColumn
  Public WithEvents ec_datini As NTSInformatica.NTSGridColumn
  Public WithEvents ec_datfin As NTSInformatica.NTSGridColumn
  Public WithEvents pnCorpo As NTSInformatica.NTSPanel
  Public WithEvents edUltCost As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPreList As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDispNetta As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDispon As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDispon As NTSInformatica.NTSLabel
  Public WithEvents lbPreList As NTSInformatica.NTSLabel
  Public WithEvents pnGriglia As NTSInformatica.NTSPanel
  Public WithEvents grRighe As NTSInformatica.NTSGrid
  Public WithEvents grvRighe As NTSInformatica.NTSGridView
  Public WithEvents ec_coddivi As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_coddivi As NTSInformatica.NTSGridColumn
  Public WithEvents tlbSeleziona As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbDaLista As NTSInformatica.NTSBarMenuItem
  Public WithEvents xxo_tctagliaf As NTSInformatica.NTSGridColumn
End Class
