Partial Public Class FRMMGARTV
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
  Public WithEvents fmCadc As NTSInformatica.NTSGroupBox
  Public WithEvents edAr_coddicv As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAr_coddica As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAr_codtcdc As NTSInformatica.NTSLabel
  Public WithEvents edAr_codtcdc As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_codtcdc As NTSInformatica.NTSLabel
  Public WithEvents lbXx_coddicv As NTSInformatica.NTSLabel
  Public WithEvents lbAr_coddica As NTSInformatica.NTSLabel
  Public WithEvents lbAr_coddicv As NTSInformatica.NTSLabel
  Public WithEvents lbXx_coddica As NTSInformatica.NTSLabel
  Public WithEvents edAr_codtlox As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbAr_tipscarlotx As NTSInformatica.NTSComboBox
  Public WithEvents lbAr_tipscarlotx As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codtlox As NTSInformatica.NTSLabel
  Public WithEvents lbAr_codtlox As NTSInformatica.NTSLabel
  Public WithEvents edFocus As NTSInformatica.NTSTextBoxStr
  Public WithEvents tlbDuplica As NTSInformatica.NTSBarButtonItem
  Public WithEvents cmdClassifica As NTSInformatica.NTSButton
  Public WithEvents lbClassifica As NTSInformatica.NTSLabel
  Public WithEvents tlbLingua As NTSInformatica.NTSBarMenuItem
  Public WithEvents lbXx_reparto As NTSInformatica.NTSLabel
  Public WithEvents fmEcommerce As NTSInformatica.NTSGroupBox
  Public WithEvents lbXx_codseat As NTSInformatica.NTSLabel
  Public WithEvents ckAr_webvend As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_webusat As NTSInformatica.NTSCheckBox
  Public WithEvents ckAr_webvis As NTSInformatica.NTSCheckBox
  Public WithEvents lbAr_codseat As NTSInformatica.NTSLabel
  Public WithEvents edAr_codseat As NTSInformatica.NTSTextBoxNum
  Public WithEvents tlbAccessoriSuccedanei As NTSInformatica.NTSBarMenuItem
  Public WithEvents ckAr_consmrp As NTSInformatica.NTSCheckBox
  Public WithEvents tlbEstensioni As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbSimula As NTSInformatica.NTSBarMenuItem
  Public WithEvents lbAr_deterior As NTSInformatica.NTSLabel
  Public WithEvents cbAr_deterior As NTSInformatica.NTSComboBox
  Public WithEvents fmLogisticaPalmare As NTSInformatica.NTSGroupBox
  Public WithEvents lbAr_converp As NTSInformatica.NTSLabel
  Public WithEvents edAr_converp As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckAr_staetip As NTSInformatica.NTSCheckBox

End Class
