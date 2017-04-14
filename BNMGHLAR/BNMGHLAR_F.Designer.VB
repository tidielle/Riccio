Partial Public Class FRMMGHLAR_F
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
  Public WithEvents pnTab1Pan2 As NTSInformatica.NTSPanel
  Public WithEvents pnTab2Pan1 As NTSInformatica.NTSPanel
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
  Public WithEvents edMagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
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
  Public WithEvents edForna As NTSInformatica.NTSTextBoxNum
  Public WithEvents edFornd As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbFornda As NTSInformatica.NTSLabel
  Public WithEvents edDescra As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDescrd As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel2 As NTSInformatica.NTSLabel
  Public WithEvents lbScontida As NTSInformatica.NTSLabel
  Public WithEvents lbProvvda As NTSInformatica.NTSLabel
  Public WithEvents edScontia As NTSInformatica.NTSTextBoxNum
  Public WithEvents edProvva As NTSInformatica.NTSTextBoxNum
  Public WithEvents edScontid As NTSInformatica.NTSTextBoxNum
  Public WithEvents edProvvd As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodIvada As NTSInformatica.NTSLabel
  Public WithEvents edCodIvaa As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCodIvad As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckBloccati As NTSInformatica.NTSCheckBox
  Public WithEvents xx_codarfo As NTSInformatica.NTSGridColumn
  Public WithEvents ar_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ar_unmis As NTSInformatica.NTSGridColumn
  Public WithEvents xx_esist As NTSInformatica.NTSGridColumn
  Public WithEvents xx_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents ar_codalt As NTSInformatica.NTSGridColumn
  Public WithEvents xx_code As NTSInformatica.NTSGridColumn
  Public WithEvents xx_ordin As NTSInformatica.NTSGridColumn
  Public WithEvents xx_impegn As NTSInformatica.NTSGridColumn
  Public WithEvents xx_dispon As NTSInformatica.NTSGridColumn
  Public WithEvents xx_dispo2 As NTSInformatica.NTSGridColumn
  Public WithEvents ar_desint As NTSInformatica.NTSGridColumn
  Public WithEvents ar_stalist As NTSInformatica.NTSGridColumn
  Public WithEvents ar_note As NTSInformatica.NTSGridColumn
  Public WithEvents xx_seleziona As NTSInformatica.NTSGridColumn
  Public WithEvents cmdProgressivi As NTSInformatica.NTSButton
  Public WithEvents cmdLastfilter As NTSInformatica.NTSButton
  Public WithEvents TabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents pnTab4Pan1 As NTSInformatica.NTSPanel
  Public WithEvents imArtGif As NTSInformatica.NTSPictureBox
  Public WithEvents pnTab2Pan2 As NTSInformatica.NTSPanel
  Public WithEvents fmPrezzi As NTSInformatica.NTSGroupBox
  Public WithEvents edDtvalid As NTSInformatica.NTSTextBoxData
  Public WithEvents lbListvalidita As NTSInformatica.NTSLabel
  Public WithEvents edListino As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbListino As NTSInformatica.NTSLabel
  Public WithEvents ckVisprezzi As NTSInformatica.NTSCheckBox
  Public WithEvents fmCliforn As NTSInformatica.NTSGroupBox
  Public WithEvents ckFiltraConto As NTSInformatica.NTSCheckBox
  Public WithEvents ckFiltraMovmag As NTSInformatica.NTSCheckBox
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Public WithEvents edArtprom As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTipo As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTipo As NTSInformatica.NTSLabel
  Public WithEvents ckAbituali As NTSInformatica.NTSCheckBox
  Public WithEvents ckCodiciRoot As NTSInformatica.NTSCheckBox
End Class