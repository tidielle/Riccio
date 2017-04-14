Partial Public Class FRMMGHLAR
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

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnDescr As NTSInformatica.NTSPanel
  Public WithEvents pnAction As NTSInformatica.NTSPanel
  Public WithEvents ckOttimistico As NTSInformatica.NTSCheckBox
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdGestione As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents cmdListini As NTSInformatica.NTSButton
  Public WithEvents cmdMovimenti As NTSInformatica.NTSButton
  Public WithEvents tsZoom As NTSInformatica.NTSTabControl
  Public WithEvents TabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents TabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents TabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents pnTab1Pan2 As NTSInformatica.NTSPanel
  Public WithEvents pnTab2Pan2 As NTSInformatica.NTSPanel
  Public WithEvents pnTab2Pan1 As NTSInformatica.NTSPanel
  Public WithEvents pnTab3Pan2 As NTSInformatica.NTSPanel
  Public WithEvents pnTab3Pan1 As NTSInformatica.NTSPanel
  Public WithEvents cmdOrdini As NTSInformatica.NTSButton
  Public WithEvents grZoom As NTSInformatica.NTSGrid
  Public WithEvents grvZoom As NTSInformatica.NTSGridView
  Public WithEvents ar_codart As NTSInformatica.NTSGridColumn
  Public WithEvents xx_nome As NTSInformatica.NTSGridColumn
  Public WithEvents xx_valore As NTSInformatica.NTSGridColumn
  Public WithEvents pnTab1Pan1 As NTSInformatica.NTSPanel
  Public WithEvents grFiltri1 As NTSInformatica.NTSGrid
  Public WithEvents grvFiltri1 As NTSInformatica.NTSGridView
  Public WithEvents lbCodarfo As NTSInformatica.NTSLabel
  Public WithEvents edCodarfo As NTSInformatica.NTSTextBoxStr
  Public WithEvents edBarcode As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbBarcode As NTSInformatica.NTSLabel
  Public WithEvents cmdLock As NTSInformatica.NTSButton
  Public WithEvents edMagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
  Public WithEvents cbTestdb As NTSInformatica.NTSComboBox
  Public WithEvents lbTestdb As NTSInformatica.NTSLabel
  Public WithEvents fmSuccedanei As NTSInformatica.NTSGroupBox
  Public WithEvents ckSuccedanei As NTSInformatica.NTSCheckBox
  Public WithEvents fmCliforn As NTSInformatica.NTSGroupBox
  Public WithEvents ckFiltraConto As NTSInformatica.NTSCheckBox
  Public WithEvents ckFiltraMovmag As NTSInformatica.NTSCheckBox
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCodartAcc As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCodartAcc As NTSInformatica.NTSLabel
  Public WithEvents optSuccedanei As NTSInformatica.NTSRadioButton
  Public WithEvents optAccessori As NTSInformatica.NTSRadioButton
  Public WithEvents fmPrezzi As NTSInformatica.NTSGroupBox
  Public WithEvents lbListino As NTSInformatica.NTSLabel
  Public WithEvents ckVisprezzi As NTSInformatica.NTSCheckBox
  Public WithEvents edDtvalid As NTSInformatica.NTSTextBoxData
  Public WithEvents lbListvalidita As NTSInformatica.NTSLabel
  Public WithEvents edListino As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel11 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel10 As NTSInformatica.NTSLabel
  Public WithEvents edCodarta As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCodartd As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCodartda As NTSInformatica.NTSLabel
  Public WithEvents edCodalta As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCodaltd As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCodaltda As NTSInformatica.NTSLabel
  Public WithEvents lbSottogruppoda As NTSInformatica.NTSLabel
  Public WithEvents edSotta As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSottd As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbGruppoda As NTSInformatica.NTSLabel
  Public WithEvents edGruppoa As NTSInformatica.NTSTextBoxNum
  Public WithEvents edGruppod As NTSInformatica.NTSTextBoxNum
  Public WithEvents edFamproda As NTSInformatica.NTSTextBoxStr
  Public WithEvents edFamprodd As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbFamigliada As NTSInformatica.NTSLabel
  Public WithEvents lbCodtipada As NTSInformatica.NTSLabel
  Public WithEvents edCodtipaa As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCodtipad As NTSInformatica.NTSTextBoxNum
  Public WithEvents edForna As NTSInformatica.NTSTextBoxNum
  Public WithEvents edFornd As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbFornda As NTSInformatica.NTSLabel
  Public WithEvents edDescra As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDescrd As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel2 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel3 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel4 As NTSInformatica.NTSLabel
  Public WithEvents lbScontida As NTSInformatica.NTSLabel
  Public WithEvents lbProvvda As NTSInformatica.NTSLabel
  Public WithEvents edScontia As NTSInformatica.NTSTextBoxNum
  Public WithEvents edProvva As NTSInformatica.NTSTextBoxNum
  Public WithEvents edScontid As NTSInformatica.NTSTextBoxNum
  Public WithEvents edProvvd As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbMarcada As NTSInformatica.NTSLabel
  Public WithEvents edMarcaa As NTSInformatica.NTSTextBoxNum
  Public WithEvents edMarcad As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodIvada As NTSInformatica.NTSLabel
  Public WithEvents lbApprovda As NTSInformatica.NTSLabel
  Public WithEvents edCodIvaa As NTSInformatica.NTSTextBoxNum
  Public WithEvents edApprova As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCodIvad As NTSInformatica.NTSTextBoxNum
  Public WithEvents edApprovd As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbMagprod As NTSInformatica.NTSLabel
  Public WithEvents edMagprodfin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edMagprodini As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbMagstock As NTSInformatica.NTSLabel
  Public WithEvents edMagstockfin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edMagstockini As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDataUltaga As NTSInformatica.NTSTextBoxData
  Public WithEvents edDataUltagd As NTSInformatica.NTSTextBoxData
  Public WithEvents NtsLabel26 As NTSInformatica.NTSLabel
  Public WithEvents lbCodtaglda As NTSInformatica.NTSLabel
  Public WithEvents edCodtagla As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCodtagld As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodstagda As NTSInformatica.NTSLabel
  Public WithEvents edCodstaga As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCodstagd As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAnnoda As NTSInformatica.NTSLabel
  Public WithEvents edAnnoa As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAnnod As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbAfasi As NTSInformatica.NTSComboBox
  Public WithEvents cbCritico As NTSInformatica.NTSComboBox
  Public WithEvents cbAlistino As NTSInformatica.NTSComboBox
  Public WithEvents cbInesaur As NTSInformatica.NTSComboBox
  Public WithEvents lbAfasi As NTSInformatica.NTSLabel
  Public WithEvents lbCritico As NTSInformatica.NTSLabel
  Public WithEvents lbAlistino As NTSInformatica.NTSLabel
  Public WithEvents lbInesaur As NTSInformatica.NTSLabel
  Public WithEvents cbAvarianti As NTSInformatica.NTSComboBox
  Public WithEvents cbUbicaz As NTSInformatica.NTSComboBox
  Public WithEvents cbMatricole As NTSInformatica.NTSComboBox
  Public WithEvents cbCommessa As NTSInformatica.NTSComboBox
  Public WithEvents cbLotti As NTSInformatica.NTSComboBox
  Public WithEvents lbAvarianti As NTSInformatica.NTSLabel
  Public WithEvents lbUbicaz As NTSInformatica.NTSLabel
  Public WithEvents lbMatricole As NTSInformatica.NTSLabel
  Public WithEvents lbCommessa As NTSInformatica.NTSLabel
  Public WithEvents lbLotti As NTSInformatica.NTSLabel
  Public WithEvents ckBloccati As NTSInformatica.NTSCheckBox
  Public WithEvents xx_codarfo As NTSInformatica.NTSGridColumn
  Public WithEvents ar_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ar_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents xx_esist As NTSInformatica.NTSGridColumn
  Public WithEvents xx_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents ar_codalt As NTSInformatica.NTSGridColumn
  Public WithEvents xx_code As NTSInformatica.NTSGridColumn
  Public WithEvents xx_prenot As NTSInformatica.NTSGridColumn
  Public WithEvents xx_ordin As NTSInformatica.NTSGridColumn
  Public WithEvents xx_impegn As NTSInformatica.NTSGridColumn
  Public WithEvents xx_dispnet As NTSInformatica.NTSGridColumn
  Public WithEvents xx_dispon As NTSInformatica.NTSGridColumn
  Public WithEvents xx_dispo2 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_desint As NTSInformatica.NTSGridColumn
  Public WithEvents ar_sostit As NTSInformatica.NTSGridColumn
  Public WithEvents ar_sostituito As NTSInformatica.NTSGridColumn
  Public WithEvents ar_inesaur As NTSInformatica.NTSGridColumn
  Public WithEvents ar_stalist As NTSInformatica.NTSGridColumn
  Public WithEvents ar_note As NTSInformatica.NTSGridColumn
  Public WithEvents xx_fase As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descr As NTSInformatica.NTSGridColumn
  Public WithEvents xx_seleziona As NTSInformatica.NTSGridColumn
  Public WithEvents cmdProgressivi As NTSInformatica.NTSButton
  Public WithEvents cmdLastfilter As NTSInformatica.NTSButton
  Public WithEvents lbUbicLike As NTSInformatica.NTSLabel
  Public WithEvents edUbicLike As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDescrLike As NTSInformatica.NTSLabel
  Public WithEvents edDescrLike As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTipo As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTipo As NTSInformatica.NTSLabel
  Public WithEvents TabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents pnTab4Pan1 As NTSInformatica.NTSPanel
  Public WithEvents imArtGif As NTSInformatica.NTSPictureBox
  Public WithEvents lbDBLike As NTSInformatica.NTSLabel
  Public WithEvents edDBLike As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel5 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Public WithEvents edListsar As NTSInformatica.NTSTextBoxNum
  Public WithEvents edArtprom As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodartLike As NTSInformatica.NTSLabel
  Public WithEvents edCodartLike As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbTipologia As NTSInformatica.NTSComboBox
  Public WithEvents lbTipologia As NTSInformatica.NTSLabel
  Public WithEvents cbTestArt As NTSInformatica.NTSComboBox
  Public WithEvents lbTestArt As NTSInformatica.NTSLabel
  Public WithEvents cmdClassifica As NTSInformatica.NTSButton
  Public WithEvents lbClassifica As NTSInformatica.NTSLabel
  Public WithEvents cmdClassificaDeleteFilter As NTSInformatica.NTSButton
  Public WithEvents NtsTextBoxStr1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edClassificazioneLivello1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edClassificazioneLivello5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edClassificazioneLivello4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edClassificazioneLivello3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edClassificazioneLivello2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents ckAbituali As NTSInformatica.NTSCheckBox
  Public WithEvents pnClassificazione As NTSInformatica.NTSPanel
  Public WithEvents trClass As NTSInformatica.NTSTreeView
  Public WithEvents cmdFiltriClassificazione As NTSInformatica.NTSButton
  Public WithEvents ar_blocco As NTSInformatica.NTSGridColumn
  Public WithEvents cmdEstensioni As NTSInformatica.NTSButton
  Public WithEvents ckCodiciRoot As NTSInformatica.NTSCheckBox
End Class
