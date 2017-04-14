Partial Public Class FRM__GCTL
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


  Public WithEvents lbControl As NTSInformatica.NTSLabel
  Public WithEvents lbTipoControllo As NTSInformatica.NTSLabel
  Public WithEvents cbCombo As NTSInformatica.NTSComboBox
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cbGriglia As NTSInformatica.NTSComboBox
  Public WithEvents cbLingua As NTSInformatica.NTSComboBox
  Public WithEvents NtsGroupBox1 As NTSInformatica.NTSGroupBox
  Public WithEvents NtsGroupBox2 As NTSInformatica.NTSGroupBox
  Public WithEvents NtsGroupBox3 As NTSInformatica.NTSGroupBox
  Public WithEvents optPrior12 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior11 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior10 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior9 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior8 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior7 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior6 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior5 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior4 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior3 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior2 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior1 As NTSInformatica.NTSRadioButton
  Public WithEvents optPrior13 As NTSInformatica.NTSRadioButton
  Public WithEvents optChecked As NTSInformatica.NTSRadioButton
  Public WithEvents optOutnotequal As NTSInformatica.NTSRadioButton
  Public WithEvents optDefault As NTSInformatica.NTSRadioButton
  Public WithEvents optFormatnumber As NTSInformatica.NTSRadioButton
  Public WithEvents optEnable As NTSInformatica.NTSRadioButton
  Public WithEvents optBold As NTSInformatica.NTSRadioButton
  Public WithEvents optVisible As NTSInformatica.NTSRadioButton
  Public WithEvents optText As NTSInformatica.NTSRadioButton
  Public WithEvents cmdCancRow As NTSInformatica.NTSButton
  Public WithEvents lbTipoDoc As NTSInformatica.NTSLabel
  Public WithEvents cmdRipristina As NTSInformatica.NTSButton
  Public WithEvents optDelete As NTSInformatica.NTSRadioButton
  Public WithEvents optUpdate As NTSInformatica.NTSRadioButton
  Public WithEvents optInsert As NTSInformatica.NTSRadioButton
  Public WithEvents lbNota As NTSInformatica.NTSLabel
  Public WithEvents cmdRemoting As NTSInformatica.NTSButton
  Public WithEvents cbControl As NTSInformatica.NTSComboBox
  Public WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Public WithEvents grUI As NTSInformatica.NTSGrid
  Public WithEvents grvUI As NTSInformatica.NTSGridView
  Public WithEvents ui_db As NTSInformatica.NTSGridColumn
  Public WithEvents ui_ditta As NTSInformatica.NTSGridColumn
  Public WithEvents ui_tipodoc As NTSInformatica.NTSGridColumn
  Public WithEvents ui_ruolo As NTSInformatica.NTSGridColumn
  Public WithEvents ui_opnome As NTSInformatica.NTSGridColumn
  Public WithEvents ui_nomprop As NTSInformatica.NTSGridColumn
  Public WithEvents ui_valprop As NTSInformatica.NTSGridColumn
  Public WithEvents ui_usascript As NTSInformatica.NTSGridColumn
  Public WithEvents ui_script As NTSInformatica.NTSGridColumn
  Public WithEvents ui_gridcol As NTSInformatica.NTSGridColumn
  Public WithEvents ui_comboitem As NTSInformatica.NTSGridColumn
  Public WithEvents ui_codling As NTSInformatica.NTSGridColumn
  Public WithEvents ui_ctrltype As NTSInformatica.NTSGridColumn
  Public WithEvents ui_child As NTSInformatica.NTSGridColumn
  Public WithEvents ui_form As NTSInformatica.NTSGridColumn
  Public WithEvents ui_ctrlname As NTSInformatica.NTSGridColumn
  Public WithEvents lbLingua As NTSInformatica.NTSLabel
  Public WithEvents lbGriglia As NTSInformatica.NTSLabel
  Public WithEvents lbCombo As NTSInformatica.NTSLabel
  Public WithEvents optErrorText As NTSInformatica.NTSRadioButton
  Public WithEvents NtsLabel2 As NTSInformatica.NTSLabel
  Public WithEvents cmdFastVisible As NTSInformatica.NTSButton
  Public WithEvents NtsLabel3 As NTSInformatica.NTSLabel
End Class
