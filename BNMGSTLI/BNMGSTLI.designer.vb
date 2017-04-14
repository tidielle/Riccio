Partial Public Class FRMMGSTLI
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
  Public WithEvents ckNoSoloSconti As NTSInformatica.NTSCheckBox
  Public WithEvents ckSeleziona As NTSInformatica.NTSCheckBox
  Public WithEvents lbCodling As NTSInformatica.NTSLabel
  Public WithEvents edCodling As NTSInformatica.NTSTextBoxNum
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDatagg As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDatagg As NTSInformatica.NTSLabel
  Public WithEvents opSelezione0 As NTSInformatica.NTSRadioButton
  Public WithEvents lbXx_Conto As NTSInformatica.NTSLabel
  Public WithEvents cbTipo1 As NTSInformatica.NTSComboBox
  Public WithEvents edListino1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codling As NTSInformatica.NTSLabel
  Public WithEvents lbXx_Codvalu1 As NTSInformatica.NTSLabel
  Public WithEvents edCodvalu1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codlavo1 As NTSInformatica.NTSLabel
  Public WithEvents edCodlavo1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbListino As NTSInformatica.NTSLabel
  Public WithEvents edQuantScont1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codtpro1 As NTSInformatica.NTSLabel
  Public WithEvents edCodtpro1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipoSconto1 As NTSInformatica.NTSComboBox
  Public WithEvents edQuant4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codpromo4 As NTSInformatica.NTSLabel
  Public WithEvents edCodpromo4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codvalu4 As NTSInformatica.NTSLabel
  Public WithEvents edCodvalu4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codlavo4 As NTSInformatica.NTSLabel
  Public WithEvents edCodlavo4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edListino4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipo4 As NTSInformatica.NTSComboBox
  Public WithEvents ckQuartoPrezzo As NTSInformatica.NTSCheckBox
  Public WithEvents edQuant3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codpromo3 As NTSInformatica.NTSLabel
  Public WithEvents edCodpromo3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codvalu3 As NTSInformatica.NTSLabel
  Public WithEvents edCodvalu3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codlavo3 As NTSInformatica.NTSLabel
  Public WithEvents edCodlavo3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edListino3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipo3 As NTSInformatica.NTSComboBox
  Public WithEvents ckTerzoPrezzo As NTSInformatica.NTSCheckBox
  Public WithEvents edQuant2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codpromo2 As NTSInformatica.NTSLabel
  Public WithEvents edCodpromo2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codvalu2 As NTSInformatica.NTSLabel
  Public WithEvents edCodvalu2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codlavo2 As NTSInformatica.NTSLabel
  Public WithEvents edCodlavo2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edListino2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipo2 As NTSInformatica.NTSComboBox
  Public WithEvents ckSecondoPrezzo As NTSInformatica.NTSCheckBox
  Public WithEvents edQuant1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codpromo1 As NTSInformatica.NTSLabel
  Public WithEvents edCodpromo1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckSconti1 As NTSInformatica.NTSCheckBox
  Public WithEvents lbQuant As NTSInformatica.NTSLabel
  Public WithEvents lbTipo As NTSInformatica.NTSLabel
  Public WithEvents lbCodpromo As NTSInformatica.NTSLabel
  Public WithEvents lbCodvalu As NTSInformatica.NTSLabel
  Public WithEvents lbCodlavo As NTSInformatica.NTSLabel
  Public WithEvents fmSelezionaArt As NTSInformatica.NTSGroupBox
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents lbXx_Codlsar As NTSInformatica.NTSLabel
  Public WithEvents edCodlsar As NTSInformatica.NTSTextBoxNum
  Public WithEvents opSelezione1 As NTSInformatica.NTSRadioButton
  Public WithEvents edQuant5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codpromo5 As NTSInformatica.NTSLabel
  Public WithEvents edCodpromo5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codvalu5 As NTSInformatica.NTSLabel
  Public WithEvents edCodvalu5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codlavo5 As NTSInformatica.NTSLabel
  Public WithEvents edCodlavo5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents edListino5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipo5 As NTSInformatica.NTSComboBox
  Public WithEvents ckQuintoPrezzo As NTSInformatica.NTSCheckBox
  Public WithEvents fmSconti As NTSInformatica.NTSGroupBox
  Public WithEvents fmPrezzi As NTSInformatica.NTSGroupBox
  Public WithEvents cbTipoSconto4 As NTSInformatica.NTSComboBox
  Public WithEvents edCodtpro4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codtpro4 As NTSInformatica.NTSLabel
  Public WithEvents edQuantScont4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckSconti4 As NTSInformatica.NTSCheckBox
  Public WithEvents cbTipoSconto3 As NTSInformatica.NTSComboBox
  Public WithEvents edCodtpro3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codtpro3 As NTSInformatica.NTSLabel
  Public WithEvents edQuantScont3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckSconti3 As NTSInformatica.NTSCheckBox
  Public WithEvents cbTipoSconto2 As NTSInformatica.NTSComboBox
  Public WithEvents edCodtpro2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_Codtpro2 As NTSInformatica.NTSLabel
  Public WithEvents edQuantScont2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckSconti2 As NTSInformatica.NTSCheckBox
  Public WithEvents fmCliFor As NTSInformatica.NTSGroupBox
  Public WithEvents opMultiClie As NTSInformatica.NTSRadioButton
  Public WithEvents opSoloClie As NTSInformatica.NTSRadioButton
  Public WithEvents opNessunoClie As NTSInformatica.NTSRadioButton
  Public WithEvents cmdSelCliFor As NTSInformatica.NTSButton
  Public WithEvents tlbStampaGriglia As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbPrezzi1 As NTSInformatica.NTSLabel
  Public WithEvents tlbCaricaGriglia As NTSInformatica.NTSBarButtonItem
  Public WithEvents NtsPanel1 As NTSInformatica.NTSPanel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents cmdApriFiltri As NTSInformatica.NTSButton
  Public WithEvents cbFiltro As NTSInformatica.NTSComboBox
  Public WithEvents lbFiltri As NTSInformatica.NTSLabel
  Public WithEvents pnCheckPrezzi As NTSInformatica.NTSPanel
  Public WithEvents pnCheckSconti As NTSInformatica.NTSPanel
  Public WithEvents tlbImportExcel As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnOparticoli As NTSInformatica.NTSPanel
  Public WithEvents cbOrdinaPer As NTSInformatica.NTSComboBox
  Public WithEvents lbOrdinaPer As NTSInformatica.NTSLabel
  Public WithEvents lbDescodlsel As NTSInformatica.NTSLabel
  Public WithEvents edCodlsel As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodlsel As NTSInformatica.NTSLabel
  Public WithEvents lbDescoddest As NTSInformatica.NTSLabel
  Public WithEvents lbCoddest As NTSInformatica.NTSLabel
  Public WithEvents edCoddest As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckNoDestdiv As NTSInformatica.NTSCheckBox

End Class
