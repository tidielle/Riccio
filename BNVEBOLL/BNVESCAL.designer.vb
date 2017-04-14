Partial Public Class FRMVESCAL
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
  Public WithEvents pnCorpo As NTSInformatica.NTSPanel
  Public WithEvents edUltCost As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPreList As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDispNetta As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDispon As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDispon As NTSInformatica.NTSLabel
  Public WithEvents lbPreList As NTSInformatica.NTSLabel
  Public WithEvents pnGriglia As NTSInformatica.NTSPanel
  Public WithEvents tlbDetMatricole As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbBolleCtoLav As NTSInformatica.NTSBarMenuItem
  Public WithEvents grRighe As NTSInformatica.NTSGrid
  Public WithEvents grvRighe As NTSInformatica.NTSGridView
  Public WithEvents ec_riga As NTSInformatica.NTSGridColumn
  Public WithEvents ec_matric As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ec_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ec_desint As NTSInformatica.NTSGridColumn
  Public WithEvents ec_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents ec_colli As NTSInformatica.NTSGridColumn
  Public WithEvents ec_ump As NTSInformatica.NTSGridColumn
  Public WithEvents ec_quant As NTSInformatica.NTSGridColumn
  Public WithEvents ec_preziva As NTSInformatica.NTSGridColumn
  Public WithEvents ec_prezvalc As NTSInformatica.NTSGridColumn
  Public WithEvents ec_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents ec_causale As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_causale As NTSInformatica.NTSGridColumn
  Public WithEvents ec_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_magaz As NTSInformatica.NTSGridColumn
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
  Public WithEvents ec_valore As NTSInformatica.NTSGridColumn
  Public WithEvents ec_contocontr As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_contocon As NTSInformatica.NTSGridColumn
  Public WithEvents ec_codclie As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codclie As NTSInformatica.NTSGridColumn
  Public WithEvents ec_misura1 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_misura2 As NTSInformatica.NTSGridColumn
  Public WithEvents ec_misura3 As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codarfo As NTSInformatica.NTSGridColumn
  Public WithEvents ec_perqta As NTSInformatica.NTSGridColumn
  Public WithEvents ec_fase As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_fase As NTSInformatica.NTSGridColumn
  Public WithEvents ec_ubicaz As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_codtagl As NTSInformatica.NTSGridColumn
  Public WithEvents ec_ortipo As NTSInformatica.NTSGridColumn
  Public WithEvents ec_oranno As NTSInformatica.NTSGridColumn
  Public WithEvents ec_orserie As NTSInformatica.NTSGridColumn
  Public WithEvents ec_ornum As NTSInformatica.NTSGridColumn
  Public WithEvents ec_orriga As NTSInformatica.NTSGridColumn
  Public WithEvents ec_salcon As NTSInformatica.NTSGridColumn
  Public WithEvents ec_prtipo As NTSInformatica.NTSGridColumn
  Public WithEvents ec_pranno As NTSInformatica.NTSGridColumn
  Public WithEvents ec_prserie As NTSInformatica.NTSGridColumn
  Public WithEvents ec_prnum As NTSInformatica.NTSGridColumn
  Public WithEvents ec_prriga As NTSInformatica.NTSGridColumn
  Public WithEvents ec_cltipo As NTSInformatica.NTSGridColumn
  Public WithEvents ec_clanno As NTSInformatica.NTSGridColumn
  Public WithEvents ec_clserie As NTSInformatica.NTSGridColumn
  Public WithEvents ec_clnum As NTSInformatica.NTSGridColumn
  Public WithEvents ec_clriga As NTSInformatica.NTSGridColumn
  Public WithEvents ec_nptipo As NTSInformatica.NTSGridColumn
  Public WithEvents ec_npanno As NTSInformatica.NTSGridColumn
  Public WithEvents ec_npserie As NTSInformatica.NTSGridColumn
  Public WithEvents ec_npnum As NTSInformatica.NTSGridColumn
  Public WithEvents ec_npriga As NTSInformatica.NTSGridColumn
  Public WithEvents ec_npqtadis As NTSInformatica.NTSGridColumn
  Public WithEvents ec_npcoldis As NTSInformatica.NTSGridColumn
  Public WithEvents ec_npvaldis As NTSInformatica.NTSGridColumn
  Public WithEvents ec_npsalcon As NTSInformatica.NTSGridColumn
  Public WithEvents ec_nprcoleva As NTSInformatica.NTSGridColumn
  Public WithEvents ec_nprquaeva As NTSInformatica.NTSGridColumn
  Public WithEvents ec_nprflevas As NTSInformatica.NTSGridColumn
  Public WithEvents ec_nprvalore As NTSInformatica.NTSGridColumn
  Public WithEvents ec_valorev As NTSInformatica.NTSGridColumn
  Public WithEvents ec_tctaglia As NTSInformatica.NTSGridColumn
  Public WithEvents ec_coddivi As NTSInformatica.NTSGridColumn
  Public WithEvents xxo_coddivi As NTSInformatica.NTSGridColumn
  Public WithEvents tlbSeleziona As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbDaLista As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbRiordina As NTSInformatica.NTSBarButtonItem
  Public xxo_tctagliaf As NTSInformatica.NTSGridColumn
End Class
