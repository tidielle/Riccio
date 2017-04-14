Partial Class FRMMGSIMU
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
  Public WithEvents pnControlli As NTSInformatica.NTSPanel
  Public WithEvents pnPulsanti As NTSInformatica.NTSPanel
  Public WithEvents cmdEsci As NTSInformatica.NTSButton
  Public WithEvents cmdSimula As NTSInformatica.NTSButton
  Public WithEvents lbDesconto As NTSInformatica.NTSLabel
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents lbDeslistino As NTSInformatica.NTSLabel
  Public WithEvents edListino As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbListino As NTSInformatica.NTSLabel
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDescodart As NTSInformatica.NTSLabel
  Public WithEvents edCodart As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents edQuant As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbQuant As NTSInformatica.NTSLabel
  Public WithEvents edCodmaga As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDescodmaga As NTSInformatica.NTSLabel
  Public WithEvents lbCodmaga As NTSInformatica.NTSLabel
  Public WithEvents lbDesprezzo As NTSInformatica.NTSLabel
  Public WithEvents lbDessconto1 As NTSInformatica.NTSLabel
  Public WithEvents lbSconto1 As NTSInformatica.NTSLabel
  Public WithEvents lbSconto2 As NTSInformatica.NTSLabel
  Public WithEvents lbDessconto6 As NTSInformatica.NTSLabel
  Public WithEvents lbDessconto5 As NTSInformatica.NTSLabel
  Public WithEvents lbDessconto4 As NTSInformatica.NTSLabel
  Public WithEvents lbDessconto3 As NTSInformatica.NTSLabel
  Public WithEvents lbDessconto2 As NTSInformatica.NTSLabel
  Public WithEvents lbSconto6 As NTSInformatica.NTSLabel
  Public WithEvents lbSconto5 As NTSInformatica.NTSLabel
  Public WithEvents lbSconto4 As NTSInformatica.NTSLabel
  Public WithEvents lbSconto3 As NTSInformatica.NTSLabel
  Public WithEvents lbDesgiacenza As NTSInformatica.NTSLabel
  Public WithEvents lbGiacenza As NTSInformatica.NTSLabel
  Public WithEvents lbDesvalore As NTSInformatica.NTSLabel
  Public WithEvents lbValore As NTSInformatica.NTSLabel
  Public WithEvents lbDesvaloreUnitario As NTSInformatica.NTSLabel
  Public WithEvents lbValoreUnitario As NTSInformatica.NTSLabel
  Public WithEvents fmSconti As NTSInformatica.NTSGroupBox
  Public WithEvents fmPrezzo As NTSInformatica.NTSGroupBox
  Public WithEvents lbPrezzo As NTSInformatica.NTSLabel
  Public WithEvents fmValori As NTSInformatica.NTSGroupBox
  Public WithEvents fmEsist As NTSInformatica.NTSGroupBox
  Public WithEvents lbDesdisponibilitanetta As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel10 As NTSInformatica.NTSLabel
  Public WithEvents lbDesdisponibilita As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel8 As NTSInformatica.NTSLabel
  Public WithEvents lbDesprenotato As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel6 As NTSInformatica.NTSLabel
  Public WithEvents lbDesordinato As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel4 As NTSInformatica.NTSLabel
  Public WithEvents lbDesimpegnato As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel2 As NTSInformatica.NTSLabel

End Class