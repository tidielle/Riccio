Partial Public Class FRM__HLCO
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
  Public WithEvents lbCognome As NTSInformatica.NTSLabel
  Public WithEvents pnAction As NTSInformatica.NTSPanel
  Public WithEvents ckOttimistico As NTSInformatica.NTSCheckBox
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents pnTab1Pan1 As NTSInformatica.NTSPanel
  Public WithEvents lbNome As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel5 As NTSInformatica.NTSLabel
  Public WithEvents edComune As NTSInformatica.NTSTextBoxStr
  Public WithEvents edNome As NTSInformatica.NTSTextBoxStr
  Public WithEvents edFax As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel7 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel6 As NTSInformatica.NTSLabel
  Public WithEvents edTelef As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel8 As NTSInformatica.NTSLabel
  Public WithEvents edEmail As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCognome As NTSInformatica.NTSTextBoxStr
  Public WithEvents grZoom As NTSInformatica.NTSGrid
  Public WithEvents grvZoom As NTSInformatica.NTSGridView
  Public WithEvents co_titolo As NTSInformatica.NTSGridColumn
  Public WithEvents co_descont As NTSInformatica.NTSGridColumn
  Public WithEvents co_descont2 As NTSInformatica.NTSGridColumn
  Public WithEvents co_indir As NTSInformatica.NTSGridColumn
  Public WithEvents co_cap As NTSInformatica.NTSGridColumn
  Public WithEvents co_citta As NTSInformatica.NTSGridColumn
  Public WithEvents co_prov As NTSInformatica.NTSGridColumn
  Public WithEvents co_stato As NTSInformatica.NTSGridColumn
  Public WithEvents co_datnasc As NTSInformatica.NTSGridColumn
  Public WithEvents co_fbuser As NTSInformatica.NTSGridColumn
  Public WithEvents co_twitteruser As NTSInformatica.NTSGridColumn
  Public WithEvents edStato As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel22 As NTSInformatica.NTSLabel
  Public WithEvents edCap As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel14 As NTSInformatica.NTSLabel
  Public WithEvents edProvincia As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel13 As NTSInformatica.NTSLabel
  Public WithEvents lbCellulare As NTSInformatica.NTSLabel
  Public WithEvents lbCell As NTSInformatica.NTSLabel
  Public WithEvents edCellulare As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDataNascita As NTSInformatica.NTSLabel
  Public WithEvents lbFacebook As NTSInformatica.NTSLabel
  Public WithEvents edTwitter As NTSInformatica.NTSTextBoxStr
  Public WithEvents edFacebook As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTwitter As NTSInformatica.NTSLabel
  Public WithEvents edDataNasc As NTSInformatica.NTSTextBoxData
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
  Public WithEvents cbStatus As NTSInformatica.NTSComboBox
  Public WithEvents co_telefpers As NTSInformatica.NTSGridColumn
  Public WithEvents co_faxpers As NTSInformatica.NTSGridColumn
  Public WithEvents co_emailpers As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codstco As NTSInformatica.NTSGridColumn
  Public WithEvents co_cellpers As NTSInformatica.NTSGridColumn

End Class
