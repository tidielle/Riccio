Partial Public Class FRMMGSCHE
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
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents cmdApriFiltri As NTSInformatica.NTSButton
  Public WithEvents cbFiltro As NTSInformatica.NTSComboBox
  Public WithEvents lbFiltri As NTSInformatica.NTSLabel
  Public WithEvents ckStampaFiltri As NTSInformatica.NTSCheckBox
  Public WithEvents pnRight As NTSInformatica.NTSPanel
  Public WithEvents pnPanel1Left As NTSInformatica.NTSPanel
  Public WithEvents lbXx_codcfam As NTSInformatica.NTSLabel
  Public WithEvents edCausale As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_causale As NTSInformatica.NTSLabel
  Public WithEvents lbCausale As NTSInformatica.NTSLabel
  Public WithEvents edUbicazini As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbUbicazini As NTSInformatica.NTSLabel
  Public WithEvents lbUbicazfin As NTSInformatica.NTSLabel
  Public WithEvents edUbicazfin As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDamatr As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDamatr As NTSInformatica.NTSLabel
  Public WithEvents lbAmatr As NTSInformatica.NTSLabel
  Public WithEvents edAmatr As NTSInformatica.NTSTextBoxStr
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
  Public WithEvents edLottoDa As NTSInformatica.NTSTextBoxStr
  Public WithEvents edLottoA As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbInizio As NTSInformatica.NTSLabel
  Public WithEvents lbFine As NTSInformatica.NTSLabel
  Public WithEvents lbFaseini As NTSInformatica.NTSLabel
  Public WithEvents edFaseini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbFasefin As NTSInformatica.NTSLabel
  Public WithEvents edFasefin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAconto As NTSInformatica.NTSLabel
  Public WithEvents edAconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCodmarcfin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodmarcfin As NTSInformatica.NTSLabel
  Public WithEvents edCodmarcini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodmarcini As NTSInformatica.NTSLabel
  Public WithEvents lbAcomme As NTSInformatica.NTSLabel
  Public WithEvents edDacomme As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDacomme As NTSInformatica.NTSLabel
  Public WithEvents edAcomme As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdClassificaDeleteFilter As NTSInformatica.NTSButton
  Public WithEvents cmdClassifica As NTSInformatica.NTSButton
  Public WithEvents lbClassifica As NTSInformatica.NTSLabel
  Public WithEvents edClassificazioneLivello5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edClassificazioneLivello4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edClassificazioneLivello3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edClassificazioneLivello2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edClassificazioneLivello1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edNonMovimentati As NTSInformatica.NTSTextBoxData
  Public WithEvents ckNonMovimentati As NTSInformatica.NTSCheckBox
  Public WithEvents ckPerMagazzino As NTSInformatica.NTSCheckBox
  Public WithEvents fmSoloMovimentati As NTSInformatica.NTSGroupBox
  Public WithEvents edCodlsar As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbCodart As NTSInformatica.NTSComboBox
  Public WithEvents lbDescodlsar As NTSInformatica.NTSLabel
  Public WithEvents cbConto As NTSInformatica.NTSComboBox
  Public WithEvents lbDescodlsel As NTSInformatica.NTSLabel
  Public WithEvents edCodlsel As NTSInformatica.NTSTextBoxNum
  Public WithEvents tlbNoModal As NTSInformatica.NTSBarMenuItem
  Public WithEvents ceFiltriExt As NTSInformatica.NTSXXFILT
  Public WithEvents pnFiltriExt As NTSInformatica.NTSPanel

End Class
