Partial Public Class FRMMGSTRL
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
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaGriglia As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbTipoElab As NTSInformatica.NTSLabel
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
  Public WithEvents edMagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipoElab As NTSInformatica.NTSComboBox
  Public WithEvents fmListini As NTSInformatica.NTSGroupBox
  Public WithEvents lbSalvaListData As NTSInformatica.NTSLabel
  Public WithEvents lbSalvaListino As NTSInformatica.NTSLabel
  Public WithEvents ckSalvaListini As NTSInformatica.NTSCheckBox
  Public WithEvents ckInvFinale As NTSInformatica.NTSCheckBox
  Public WithEvents edDtelab As NTSInformatica.NTSTextBoxData
  Public WithEvents edSalvaListData As NTSInformatica.NTSTextBoxData
  Public WithEvents cbGiacenze As NTSInformatica.NTSComboBox
  Public WithEvents lbGiacenze As NTSInformatica.NTSLabel
  Public WithEvents edSalvaListino As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckSalvaCostiZero As NTSInformatica.NTSCheckBox
  Public WithEvents opInventarioFinale As NTSInformatica.NTSRadioButton
  Public WithEvents opUltimoCostoacc As NTSInformatica.NTSRadioButton
  Public WithEvents opFifo As NTSInformatica.NTSRadioButton
  Public WithEvents opListino As NTSInformatica.NTSRadioButton
  Public WithEvents opUltimoCosto As NTSInformatica.NTSRadioButton
  Public WithEvents opMedioGlobale As NTSInformatica.NTSRadioButton
  Public WithEvents opMedio As NTSInformatica.NTSRadioButton
  Public WithEvents opLifo As NTSInformatica.NTSRadioButton
  Public WithEvents ckUsacostiglob As NTSInformatica.NTSCheckBox
  Public WithEvents ckLifoAnniPrec As NTSInformatica.NTSCheckBox
  Public WithEvents fmMagazzini As NTSInformatica.NTSGroupBox
  Public WithEvents opMagMerceProp As NTSInformatica.NTSRadioButton
  Public WithEvents opMagazAltrui As NTSInformatica.NTSRadioButton
  Public WithEvents opMagazUno As NTSInformatica.NTSRadioButton
  Public WithEvents opMagazTutti As NTSInformatica.NTSRadioButton
  Public WithEvents fmValorizzazione As NTSInformatica.NTSGroupBox
  Public WithEvents ckUsaListForn As NTSInformatica.NTSCheckBox
  Public WithEvents ckSoloPrezziListino As NTSInformatica.NTSCheckBox
  Public WithEvents edListino As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdSelArt As NTSInformatica.NTSButton
  Public WithEvents ckDettaglioTCO As NTSInformatica.NTSCheckBox
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
  Public WithEvents ckEscludiArticoliNonMov As NTSInformatica.NTSCheckBox
  Public WithEvents fmGeneraListaSelezionata As NTSInformatica.NTSGroupBox
  Public WithEvents ckGeneraListaSelezionata As NTSInformatica.NTSCheckBox
  Public WithEvents edCodlsar As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDeslsar As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbNegozio As NTSInformatica.NTSLabel
  Public WithEvents edNegozio As NTSInformatica.NTSTextBoxNum
  Public WithEvents opMagazNegozio As NTSInformatica.NTSRadioButton
  Public WithEvents ckTcoEsitTaglia As NTSInformatica.NTSCheckBox
  Public WithEvents tlbElabMultiMagaz As NTSInformatica.NTSBarMenuItem
  Public WithEvents ckQta0 As NTSInformatica.NTSCheckBox
End Class
