Partial Public Class FRM__ISTF
  Inherits FRM__CHIL

  <System.Diagnostics.DebuggerNonUserCode()> _
  Public Sub New()
    MyBase.New()

    ''This call is required by the Windows Form Designer.
    'InitializeComponent()

  End Sub

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub
  Public WithEvents ckPf_statot As NTSInformatica.NTSCheckBox
  Public WithEvents cbPf_order As NTSInformatica.NTSComboBox
  Public WithEvents cbPf_codquery As NTSInformatica.NTSComboBox
  Public WithEvents edPf_maxcolo As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPf_titstam As NTSInformatica.NTSTextBoxStr
  Public WithEvents edPf_codform As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSelezione As NTSInformatica.NTSLabel
  Public WithEvents lbPf_order As NTSInformatica.NTSLabel
  Public WithEvents lbPf_maxcolo As NTSInformatica.NTSLabel
  Public WithEvents lbPf_codquery As NTSInformatica.NTSLabel
  Public WithEvents lbPf_codform As NTSInformatica.NTSLabel
  Public WithEvents grStca As NTSInformatica.NTSGrid
  Public WithEvents grvStca As NTSInformatica.NTSGridView
  Public WithEvents pnFill As NTSInformatica.NTSPanel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents lbPf_titstam As NTSInformatica.NTSLabel
  Public WithEvents cmdFiltri As NTSInformatica.NTSButton
  Public WithEvents pfc_riga As NTSInformatica.NTSGridColumn
  Public WithEvents pfc_nomcampo As NTSInformatica.NTSGridColumn
  Public WithEvents pfc_size As NTSInformatica.NTSGridColumn
  Public WithEvents tlbRecordNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRecordCancella As NTSInformatica.NTSBarButtonItem

  'Required by the Windows Form Designer
  'Private components As System.ComponentModel.IContainer

End Class
