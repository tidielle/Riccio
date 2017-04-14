Partial Public Class FRM__ANEX
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
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tsAnex As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents lbAx_tipo1 As NTSInformatica.NTSLabel
  Public WithEvents edAx_tipo1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAx_tipo2 As NTSInformatica.NTSLabel
  Public WithEvents edAx_tipo2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAx_tipo3 As NTSInformatica.NTSLabel
  Public WithEvents edAx_tipo3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAx_desext1 As NTSInformatica.NTSLabel
  Public WithEvents edAx_desext1 As NTSInformatica.NTSMemoBox
  Public WithEvents lbAx_desext2 As NTSInformatica.NTSLabel
  Public WithEvents edAx_desext2 As NTSInformatica.NTSMemoBox
  Public WithEvents lbAx_desext3 As NTSInformatica.NTSLabel
  Public WithEvents edAx_desext3 As NTSInformatica.NTSMemoBox
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents pnPage2 As NTSInformatica.NTSPanel
  Public WithEvents pnPage1 As NTSInformatica.NTSPanel
  Public WithEvents pnPage3 As NTSInformatica.NTSPanel
  Public WithEvents pnPage4 As NTSInformatica.NTSPanel
  Public WithEvents pnPage5 As NTSInformatica.NTSPanel
  Public WithEvents pnPage6 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage4 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage5 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage6 As NTSInformatica.NTSTabPage
  Public WithEvents ckAx_check2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckAx_check4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckAx_check6 As NTSInformatica.NTSCheckBox
  Public WithEvents ckAx_check8 As NTSInformatica.NTSCheckBox
  Public WithEvents ckAx_check10 As NTSInformatica.NTSCheckBox
  Public WithEvents lbAx_combo1 As NTSInformatica.NTSLabel
  Public WithEvents cbAx_combo1 As NTSInformatica.NTSComboBox
  Public WithEvents lbAx_combo2 As NTSInformatica.NTSLabel
  Public WithEvents cbAx_combo2 As NTSInformatica.NTSComboBox
  Public WithEvents lbAx_combo3 As NTSInformatica.NTSLabel
  Public WithEvents cbAx_combo3 As NTSInformatica.NTSComboBox
  Public WithEvents ckAx_check1 As NTSInformatica.NTSCheckBox
  Public WithEvents ckAx_check3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckAx_check5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckAx_check7 As NTSInformatica.NTSCheckBox
  Public WithEvents ckAx_check9 As NTSInformatica.NTSCheckBox
  Public WithEvents lbAx_memo2 As NTSInformatica.NTSLabel
  Public WithEvents edAx_memo2 As NTSInformatica.NTSMemoBox
  Public WithEvents lbAx_num1 As NTSInformatica.NTSLabel
  Public WithEvents edAx_num1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAx_num2 As NTSInformatica.NTSLabel
  Public WithEvents edAx_num2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAx_num3 As NTSInformatica.NTSLabel
  Public WithEvents edAx_num3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAx_num4 As NTSInformatica.NTSLabel
  Public WithEvents edAx_num4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAx_num5 As NTSInformatica.NTSLabel
  Public WithEvents edAx_num5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAx_num6 As NTSInformatica.NTSLabel
  Public WithEvents edAx_num6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAx_num7 As NTSInformatica.NTSLabel
  Public WithEvents edAx_num7 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAx_num8 As NTSInformatica.NTSLabel
  Public WithEvents edAx_num8 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAx_num9 As NTSInformatica.NTSLabel
  Public WithEvents edAx_num9 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAx_num10 As NTSInformatica.NTSLabel
  Public WithEvents edAx_num10 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAx_memo1 As NTSInformatica.NTSLabel
  Public WithEvents edAx_memo1 As NTSInformatica.NTSMemoBox
  Public WithEvents lbAx_descr1 As NTSInformatica.NTSLabel
  Public WithEvents edAx_descr1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAx_descr2 As NTSInformatica.NTSLabel
  Public WithEvents edAx_descr2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAx_descr3 As NTSInformatica.NTSLabel
  Public WithEvents edAx_descr3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAx_descr4 As NTSInformatica.NTSLabel
  Public WithEvents edAx_descr4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAx_descr5 As NTSInformatica.NTSLabel
  Public WithEvents edAx_descr5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAx_descr6 As NTSInformatica.NTSLabel
  Public WithEvents edAx_descr6 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAx_descr7 As NTSInformatica.NTSLabel
  Public WithEvents edAx_descr7 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAx_descr8 As NTSInformatica.NTSLabel
  Public WithEvents edAx_descr8 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAx_descr9 As NTSInformatica.NTSLabel
  Public WithEvents edAx_descr9 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAx_descr10 As NTSInformatica.NTSLabel
  Public WithEvents edAx_descr10 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAx_data1 As NTSInformatica.NTSLabel
  Public WithEvents edAx_data1 As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAx_data2 As NTSInformatica.NTSLabel
  Public WithEvents edAx_data2 As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAx_data3 As NTSInformatica.NTSLabel
  Public WithEvents edAx_data3 As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAx_data4 As NTSInformatica.NTSLabel
  Public WithEvents edAx_data4 As NTSInformatica.NTSTextBoxData
  Public WithEvents lbAx_data5 As NTSInformatica.NTSLabel
  Public WithEvents edAx_data5 As NTSInformatica.NTSTextBoxData
  Public WithEvents lbHelptipo3 As NTSInformatica.NTSLabel
  Public WithEvents lbHelptipo2 As NTSInformatica.NTSLabel
  Public WithEvents lbHelptipo1 As NTSInformatica.NTSLabel
End Class
