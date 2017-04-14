Partial Public Class FRMMGSCHE_F
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
  Public WithEvents ckStampaFiltri As NTSInformatica.NTSCheckBox
  Public WithEvents lbXx_codcfam As NTSInformatica.NTSLabel
  Public WithEvents edDatini As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDatini As NTSInformatica.NTSLabel
  Public WithEvents edDatfin As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDatfin As NTSInformatica.NTSLabel
  Public WithEvents ckSaldiIniziali As NTSInformatica.NTSCheckBox
  Public WithEvents ckStorico As NTSInformatica.NTSCheckBox
  Public WithEvents lbDamagaz As NTSInformatica.NTSLabel
  Public WithEvents edDamagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAmagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAmagaz As NTSInformatica.NTSLabel
  Public WithEvents lbCodcfam As NTSInformatica.NTSLabel
  Public WithEvents edCodcfam As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbSep As NTSInformatica.NTSLabel
  Public WithEvents edSottogr As NTSInformatica.NTSTextBoxNum
  Public WithEvents edGruppo As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbGruppo As NTSInformatica.NTSLabel
  Public WithEvents edDacodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDacodart As NTSInformatica.NTSLabel
  Public WithEvents lbAcodart As NTSInformatica.NTSLabel
  Public WithEvents edAcodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents pnTipoStampa As NTSInformatica.NTSPanel
  Public WithEvents opTipoStampa1 As NTSInformatica.NTSRadioButton
  Public WithEvents opTipoStampa0 As NTSInformatica.NTSRadioButton
  Public WithEvents lbTipoStampa As NTSInformatica.NTSLabel
  Public WithEvents cbTipoStampa As NTSInformatica.NTSComboBox
  Public WithEvents lbTipo As NTSInformatica.NTSLabel
  Public WithEvents cbIncludi As NTSInformatica.NTSComboBox
  Public WithEvents lbIncludi As NTSInformatica.NTSLabel
  Public WithEvents edDaconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAconto As NTSInformatica.NTSLabel
  Public WithEvents edAconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDaconto As NTSInformatica.NTSLabel
  Public WithEvents fmCheck As NTSInformatica.NTSGroupBox
  Public WithEvents pnRight As NTSInformatica.NTSPanel

End Class