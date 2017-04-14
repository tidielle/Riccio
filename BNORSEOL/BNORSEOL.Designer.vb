Partial Public Class FRMORSEOL
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

  Public WithEvents fmTipo As NTSInformatica.NTSGroupBox
  Public WithEvents ckOpInterni As NTSInformatica.NTSCheckBox
  Public WithEvents opIT As NTSInformatica.NTSRadioButton
  Public WithEvents opOP As NTSInformatica.NTSRadioButton
  Public WithEvents opOF As NTSInformatica.NTSRadioButton
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCommeca As NTSInformatica.NTSTextBoxNum
  Public WithEvents edMagaz As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDatconsDa As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatordA As NTSInformatica.NTSTextBoxData
  Public WithEvents lbContoLabel As NTSInformatica.NTSLabel
  Public WithEvents lbCommecaLabel As NTSInformatica.NTSLabel
  Public WithEvents lbMagazLabel As NTSInformatica.NTSLabel
  Public WithEvents lbDatcons As NTSInformatica.NTSLabel
  Public WithEvents lbDatord As NTSInformatica.NTSLabel
  Public WithEvents edDatconsA As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatordDa As NTSInformatica.NTSTextBoxData
  Public WithEvents fmStato As NTSInformatica.NTSGroupBox
  Public WithEvents ckEmRDO As NTSInformatica.NTSCheckBox
  Public WithEvents ckCongelato As NTSInformatica.NTSCheckBox
  Public WithEvents ckEmOrdine As NTSInformatica.NTSCheckBox
  Public WithEvents ckConfermato As NTSInformatica.NTSCheckBox
  Public WithEvents ckAppRDA As NTSInformatica.NTSCheckBox
  Public WithEvents ckEmRDA As NTSInformatica.NTSCheckBox
  Public WithEvents ckGenerato As NTSInformatica.NTSCheckBox
  Public WithEvents pnSx As NTSInformatica.NTSPanel
  Public WithEvents cmdEsci As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
  Public WithEvents lbCommeca As NTSInformatica.NTSLabel
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents NtsPanel1 As NTSInformatica.NTSPanel
  Public WithEvents fmOrdinamento As NTSInformatica.NTSGroupBox
  Public WithEvents opDesArtFase As NTSInformatica.NTSRadioButton
  Public WithEvents opCodArtFase As NTSInformatica.NTSRadioButton
  Public WithEvents opProg As NTSInformatica.NTSRadioButton
  Public WithEvents opCentro As NTSInformatica.NTSRadioButton
  Public WithEvents opLineaFam As NTSInformatica.NTSRadioButton
  Public WithEvents opMagaz As NTSInformatica.NTSRadioButton
  Public WithEvents opDatcons As NTSInformatica.NTSRadioButton
  Public WithEvents opDesForn As NTSInformatica.NTSRadioButton
  Public WithEvents opCodForn As NTSInformatica.NTSRadioButton
End Class
