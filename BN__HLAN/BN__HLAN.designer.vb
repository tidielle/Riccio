Partial Public Class FRM__HLAN
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
  Public WithEvents lbDescr As NTSInformatica.NTSLabel
  Public WithEvents pnAction As NTSInformatica.NTSPanel
  Public WithEvents ckOttimistico As NTSInformatica.NTSCheckBox
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdGestione As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents cmdEscludi As NTSInformatica.NTSButton
  Public WithEvents cmdNuovoPdc As NTSInformatica.NTSButton
  Public WithEvents tsZoom As NTSInformatica.NTSTabControl
  Public WithEvents TabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents TabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents TabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents pnTab1Pan2 As NTSInformatica.NTSPanel
  Public WithEvents pnTab1Pan1 As NTSInformatica.NTSPanel
  Public WithEvents NtsLabel2 As NTSInformatica.NTSLabel
  Public WithEvents edDescr2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel5 As NTSInformatica.NTSLabel
  Public WithEvents edComune As NTSInformatica.NTSTextBoxStr
  Public WithEvents edSiglaric As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel4 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel3 As NTSInformatica.NTSLabel
  Public WithEvents edCodfisc As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPariva As NTSInformatica.NTSTextBoxStr
  Public WithEvents fmTipo As NTSInformatica.NTSGroupBox
  Public WithEvents cbSottc As NTSInformatica.NTSComboBox
  Public WithEvents optSottoconti As NTSInformatica.NTSRadioButton
  Public WithEvents optFornitori As NTSInformatica.NTSRadioButton
  Public WithEvents optClienti As NTSInformatica.NTSRadioButton
  Public WithEvents ckSoloSemplificata As NTSInformatica.NTSCheckBox
  Public WithEvents edFax As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel7 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel6 As NTSInformatica.NTSLabel
  Public WithEvents edTelef As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel8 As NTSInformatica.NTSLabel
  Public WithEvents edEmail As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel9 As NTSInformatica.NTSLabel
  Public WithEvents edMastro As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckAnagen As NTSInformatica.NTSCheckBox
  Public WithEvents edDescr As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdReset As NTSInformatica.NTSButton
  Public WithEvents pnTab2Pan2 As NTSInformatica.NTSPanel
  Public WithEvents pnTab2Pan1 As NTSInformatica.NTSPanel
  Public WithEvents pnTab3Pan2 As NTSInformatica.NTSPanel
  Public WithEvents edProvinciaDa As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel13 As NTSInformatica.NTSLabel
  Public WithEvents edContoA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edContoDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel12 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel11 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel10 As NTSInformatica.NTSLabel
  Public WithEvents edProvinciaA As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel18 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel17 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel16 As NTSInformatica.NTSLabel
  Public WithEvents edPagamA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAgenteA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCategA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPagamDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAgenteDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCategDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel15 As NTSInformatica.NTSLabel
  Public WithEvents edZonaA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edZonaDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCapA As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCapDa As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel14 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel26 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel25 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel24 As NTSInformatica.NTSLabel
  Public WithEvents edListinoA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edListinoDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel23 As NTSInformatica.NTSLabel
  Public WithEvents edCanaleA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCanaleDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel20 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel21 As NTSInformatica.NTSLabel
  Public WithEvents edRagsocA As NTSInformatica.NTSTextBoxStr
  Public WithEvents edRagsocDa As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel19 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel27 As NTSInformatica.NTSLabel
  Public WithEvents edValutaA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edValutaDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDtAggA As NTSInformatica.NTSTextBoxData
  Public WithEvents edDtAperturaA As NTSInformatica.NTSTextBoxData
  Public WithEvents edDtAggDa As NTSInformatica.NTSTextBoxData
  Public WithEvents edDtAperturaDa As NTSInformatica.NTSTextBoxData
  Public WithEvents NtsLabel36 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel35 As NTSInformatica.NTSLabel
  Public WithEvents liFatturaz As NTSInformatica.NTSListBox
  Public WithEvents liStatus As NTSInformatica.NTSListBox
  Public WithEvents NtsLabel34 As NTSInformatica.NTSLabel
  Public WithEvents liPrivacy As NTSInformatica.NTSListBox
  Public WithEvents NtsLabel37 As NTSInformatica.NTSLabel
  Public WithEvents liGgcons As NTSInformatica.NTSListBox
  Public WithEvents NtsLabel33 As NTSInformatica.NTSLabel
  Public WithEvents edEsenA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edEsenDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnTab3Pan1 As NTSInformatica.NTSPanel
  Public WithEvents NtsLabel39 As NTSInformatica.NTSLabel
  Public WithEvents liPariva As NTSInformatica.NTSListBox
  Public WithEvents edStato As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel22 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel38 As NTSInformatica.NTSLabel
  Public WithEvents liBlocco As NTSInformatica.NTSListBox
  Public WithEvents NtsLabel32 As NTSInformatica.NTSLabel
  Public WithEvents edLinguaA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edLinguaDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel31 As NTSInformatica.NTSLabel
  Public WithEvents edSconA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSconDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel29 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel30 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel28 As NTSInformatica.NTSLabel
  Public WithEvents edProvA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edProvDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdEstensioni As NTSInformatica.NTSButton
  Public WithEvents grZoom As NTSInformatica.NTSGrid
  Public WithEvents grvZoom As NTSInformatica.NTSGridView
  Public WithEvents an_conto As NTSInformatica.NTSGridColumn
  Public WithEvents an_descr1 As NTSInformatica.NTSGridColumn
  Public WithEvents an_descr2 As NTSInformatica.NTSGridColumn
  Public WithEvents an_citta As NTSInformatica.NTSGridColumn
  Public WithEvents an_telef As NTSInformatica.NTSGridColumn
  Public WithEvents an_faxtlx As NTSInformatica.NTSGridColumn
  Public WithEvents an_pariva As NTSInformatica.NTSGridColumn
  Public WithEvents an_codfis As NTSInformatica.NTSGridColumn
  Public WithEvents an_contatt As NTSInformatica.NTSGridColumn
  Public WithEvents lbUsaem As NTSInformatica.NTSLabel
  Public WithEvents liUsaem As NTSInformatica.NTSListBox
  Public WithEvents an_flci As NTSInformatica.NTSGridColumn
  Public WithEvents an_accperi As NTSInformatica.NTSGridColumn
  Public WithEvents cmdLastfilter As NTSInformatica.NTSButton
  Public WithEvents cbPrivato As NTSInformatica.NTSComboBox
  Public WithEvents ckAbituali As NTSInformatica.NTSCheckBox

End Class
