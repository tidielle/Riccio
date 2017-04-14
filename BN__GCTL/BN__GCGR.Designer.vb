Partial Public Class FRM__GCGR
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
  Public WithEvents cmdPredefinito As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdSalva As NTSInformatica.NTSButton
  Public WithEvents fmParameters As NTSInformatica.NTSGroupBox
  Public WithEvents NtsLabel2 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Public WithEvents edOperat As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTipodoc As NTSInformatica.NTSTextBoxStr
  Public WithEvents edGruppo As NTSInformatica.NTSTextBoxStr
  Public WithEvents NtsLabel3 As NTSInformatica.NTSLabel
  Public WithEvents optOperat As NTSInformatica.NTSRadioButton
  Public WithEvents optGruppo As NTSInformatica.NTSRadioButton
  Public WithEvents fmDepends As NTSInformatica.NTSGroupBox
  Public WithEvents grUi As NTSInformatica.NTSGrid
  Public WithEvents grvUi As NTSInformatica.NTSGridView
  Public WithEvents ui_tag As NTSInformatica.NTSGridColumn
  Public WithEvents ui_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ui_visible As NTSInformatica.NTSGridColumn
  Public WithEvents ui_enable As NTSInformatica.NTSGridColumn
  Public WithEvents ui_order As NTSInformatica.NTSGridColumn
  Public WithEvents ui_colname As NTSInformatica.NTSGridColumn
  Public WithEvents fmMoveColumn As NTSInformatica.NTSGroupBox
  Public WithEvents cmdDown As NTSInformatica.NTSButton
  Public WithEvents cmdUp As NTSInformatica.NTSButton
  Public WithEvents pnCommand As NTSInformatica.NTSPanel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents lbControl As NTSInformatica.NTSLabel
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents cmdDown5 As NTSInformatica.NTSButton
  Public WithEvents cmdUp5 As NTSInformatica.NTSButton
  Public WithEvents xx_color As NTSInformatica.NTSGridColumn
  Public WithEvents lbSpostaNota As NTSInformatica.NTSLabel
  Public WithEvents edRicerca As NTSInformatica.NTSTextBoxStr
  Public WithEvents ui_colwidth As NTSInformatica.NTSGridColumn
  Public WithEvents NtsLabel4 As NTSInformatica.NTSLabel
  Public WithEvents edRowHeight As NTSInformatica.NTSTextBoxNum
End Class
