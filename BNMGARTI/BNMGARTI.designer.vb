Partial Public Class FRMMGARTI
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
  Public WithEvents lbXx_clascon As NTSInformatica.NTSLabel
  Public WithEvents lbXx_claprov As NTSInformatica.NTSLabel
  Public WithEvents lbAr_claprov As NTSInformatica.NTSLabel
  Public WithEvents edAr_claprov As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_clascon As NTSInformatica.NTSLabel
  Public WithEvents edAr_clascon As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnMaga1 As NTSInformatica.NTSPanel
  Public WithEvents pnMaga2 As NTSInformatica.NTSPanel
  Public WithEvents fmLogisticaPalmare As NTSInformatica.NTSGroupBox
  Public WithEvents ckAr_staetip As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_staeti As NTSInformatica.NTSCheckBox
  Public WithEvents lbAr_codgrlo As NTSInformatica.NTSLabel
  Public WithEvents lbAr_scominpk As NTSInformatica.NTSLabel
  Public WithEvents lbAr_indrot As NTSInformatica.NTSLabel
  Public WithEvents lbAr_converp As NTSInformatica.NTSLabel
  Public WithEvents edAr_indrot As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_scominpk As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_codgrlo As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_converp As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_scosic As NTSInformatica.NTSLabel
  Public WithEvents edAr_scosic As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_ubicus As NTSInformatica.NTSLabel
  Public WithEvents lbAr_ubicri As NTSInformatica.NTSLabel
  Public WithEvents lbAr_ubicpr As NTSInformatica.NTSLabel
  Public WithEvents lbAr_ubicst As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codgrlo As NTSInformatica.NTSLabel
  Public WithEvents edAr_ubicpr As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_ubicri As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_ubicus As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_ubicst As NTSInformatica.NTSTextBoxStr
  Public WithEvents edFocus As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAr_coddica As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_coddicv As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_codtcdc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAr_coddica As NTSInformatica.NTSLabel
  Public WithEvents lbAr_coddicv As NTSInformatica.NTSLabel
  Public WithEvents lbAr_codtcdc As NTSInformatica.NTSLabel
  Public WithEvents lbXx_coddica As NTSInformatica.NTSLabel
  Public WithEvents lbXx_coddicv As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codtcdc As NTSInformatica.NTSLabel
  Public WithEvents fmCadc As NTSInformatica.NTSGroupBox
  Public WithEvents cbAr_tipscarlotx As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_tipscarlotx As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codtlox As NTSInformatica.NTSLabel
  Public WithEvents lbAr_codtlox As NTSInformatica.NTSLabel
  Public WithEvents edAr_codtlox As NTSInformatica.NTSTextBoxNum
  Public WithEvents tlbOption As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbApriUltimaRicerca As NTSInformatica.NTSBarMenuItem
  Public WithEvents ckAr_flgift As NTSInformatica.NTSCheckBox
  Public WithEvents tlbGift As NTSInformatica.NTSBarButtonItem
  Public WithEvents ceColl As NTSInformatica.NTSXXCOLL
  Public WithEvents cmdClassifica As NTSInformatica.NTSButton
  Public WithEvents lbClassifica As NTSInformatica.NTSLabel
  Public WithEvents cmdClassificaDeleteFilter As NTSInformatica.NTSButton
  Public WithEvents lbXx_reparto As NTSInformatica.NTSLabel
  Public WithEvents ckAr_webvis As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_codseat As NTSInformatica.NTSLabel
  Public WithEvents lbAr_codseat As NTSInformatica.NTSLabel
  Public WithEvents edAr_codseat As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckAr_webusat As NTSInformatica.NTSCheckBox
  Public WithEvents fmEcommerce As NTSInformatica.NTSGroupBox
  Public WithEvents ckAr_webvend As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_consmrp As NTSInformatica.NTSCheckBox
  Public WithEvents tlbEstensioni As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbSimula As NTSInformatica.NTSBarMenuItem
  Public WithEvents lbAr_deterior As NTSInformatica.NTSLabel
  Public WithEvents cbAr_deterior As NTSInformatica.NTSComboBox

End Class
