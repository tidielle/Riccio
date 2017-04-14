Partial Public Class FRM__HLEX
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
  Public WithEvents tsHlex As NTSInformatica.NTSTabControl
  Public WithEvents pnTab1 As NTSInformatica.NTSTabPage
  Public WithEvents pnTab2 As NTSInformatica.NTSTabPage
  Public WithEvents pnCommand As NTSInformatica.NTSPanel
  Public WithEvents pnFilter As NTSInformatica.NTSPanel
  Public WithEvents pnTab3 As NTSInformatica.NTSTabPage
  Public WithEvents pnTab4 As NTSInformatica.NTSTabPage
  Public WithEvents pnTab6 As NTSInformatica.NTSTabPage
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents cmdCancel As NTSInformatica.NTSButton
  Public WithEvents NtsLabel1 As NTSInformatica.NTSLabel
  Public WithEvents pnTab5 As NTSInformatica.NTSTabPage
  Public WithEvents pn1 As NTSInformatica.NTSPanel
  Public WithEvents lbDesEx3 As NTSInformatica.NTSLabel
  Public WithEvents lbDesEx2 As NTSInformatica.NTSLabel
  Public WithEvents edDesex3 As NTSInformatica.NTSMemoBox
  Public WithEvents edDesex2 As NTSInformatica.NTSMemoBox
  Public WithEvents edDesex1 As NTSInformatica.NTSMemoBox
  Public WithEvents lbDesEx1 As NTSInformatica.NTSLabel
  Public WithEvents edTipo3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTipo2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edTipo1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbHelpTipo3 As NTSInformatica.NTSLabel
  Public WithEvents lbHelpTipo2 As NTSInformatica.NTSLabel
  Public WithEvents lbHelpTipo1 As NTSInformatica.NTSLabel
  Public WithEvents lbTipo2 As NTSInformatica.NTSLabel
  Public WithEvents lbTipo3 As NTSInformatica.NTSLabel
  Public WithEvents lbTipo1 As NTSInformatica.NTSLabel
  Public WithEvents pn2 As NTSInformatica.NTSPanel
  Public WithEvents ckData5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckData4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckData3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckData2 As NTSInformatica.NTSCheckBox
  Public WithEvents NtsLabel11 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel10 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel9 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel8 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel7 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel6 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel5 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel4 As NTSInformatica.NTSLabel
  Public WithEvents edData5a As NTSInformatica.NTSTextBoxData
  Public WithEvents edData4a As NTSInformatica.NTSTextBoxData
  Public WithEvents edData3a As NTSInformatica.NTSTextBoxData
  Public WithEvents edData2a As NTSInformatica.NTSTextBoxData
  Public WithEvents edData5da As NTSInformatica.NTSTextBoxData
  Public WithEvents edData4da As NTSInformatica.NTSTextBoxData
  Public WithEvents edData3da As NTSInformatica.NTSTextBoxData
  Public WithEvents edData2da As NTSInformatica.NTSTextBoxData
  Public WithEvents edData1a As NTSInformatica.NTSTextBoxData
  Public WithEvents edData1da As NTSInformatica.NTSTextBoxData
  Public WithEvents NtsLabel3 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel2 As NTSInformatica.NTSLabel
  Public WithEvents ckData1 As NTSInformatica.NTSCheckBox
  Public WithEvents lbDescr10 As NTSInformatica.NTSLabel
  Public WithEvents lbDescr9 As NTSInformatica.NTSLabel
  Public WithEvents lbDescr8 As NTSInformatica.NTSLabel
  Public WithEvents lbDescr7 As NTSInformatica.NTSLabel
  Public WithEvents lbDescr6 As NTSInformatica.NTSLabel
  Public WithEvents lbDescr5 As NTSInformatica.NTSLabel
  Public WithEvents lbDescr4 As NTSInformatica.NTSLabel
  Public WithEvents lbDescr3 As NTSInformatica.NTSLabel
  Public WithEvents lbDescr2 As NTSInformatica.NTSLabel
  Public WithEvents edDescr10 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDescr9 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDescr8 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDescr7 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDescr6 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDescr5 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDescr4 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDescr3 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDescr2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDescr1 As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDescr1 As NTSInformatica.NTSLabel
  Public WithEvents pn3 As NTSInformatica.NTSPanel
  Public WithEvents edMemo1 As NTSInformatica.NTSMemoBox
  Public WithEvents lbMemo1 As NTSInformatica.NTSLabel
  Public WithEvents pn4 As NTSInformatica.NTSPanel
  Public WithEvents edMemo2 As NTSInformatica.NTSMemoBox
  Public WithEvents lbMemo2 As NTSInformatica.NTSLabel
  Public WithEvents pn5 As NTSInformatica.NTSPanel
  Public WithEvents edNum10a As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum9a As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum8a As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum7a As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum6a As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum5a As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum4a As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum3a As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum2a As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum1a As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum10da As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum9da As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum8da As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum7da As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum6da As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum5da As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum4da As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum3da As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum2da As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNum1da As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckNum10 As NTSInformatica.NTSCheckBox
  Public WithEvents ckNum9 As NTSInformatica.NTSCheckBox
  Public WithEvents ckNum8 As NTSInformatica.NTSCheckBox
  Public WithEvents ckNum7 As NTSInformatica.NTSCheckBox
  Public WithEvents NtsLabel22 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel23 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel24 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel25 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel26 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel27 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel28 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel29 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel30 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel31 As NTSInformatica.NTSLabel
  Public WithEvents ckNum6 As NTSInformatica.NTSCheckBox
  Public WithEvents ckNum5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckNum4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckNum3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckNum2 As NTSInformatica.NTSCheckBox
  Public WithEvents NtsLabel12 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel13 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel14 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel15 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel16 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel17 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel18 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel19 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel20 As NTSInformatica.NTSLabel
  Public WithEvents NtsLabel21 As NTSInformatica.NTSLabel
  Public WithEvents ckNum1 As NTSInformatica.NTSCheckBox
  Public WithEvents pn6 As NTSInformatica.NTSPanel
  Public WithEvents lbCombo3 As NTSInformatica.NTSLabel
  Public WithEvents lbCombo2 As NTSInformatica.NTSLabel
  Public WithEvents liList3 As NTSInformatica.NTSListBox
  Public WithEvents lbCombo1 As NTSInformatica.NTSLabel
  Public WithEvents liList2 As NTSInformatica.NTSListBox
  Public WithEvents liList1 As NTSInformatica.NTSListBox
  Public WithEvents lbCheck10 As NTSInformatica.NTSLabel
  Public WithEvents lbCheck8 As NTSInformatica.NTSLabel
  Public WithEvents liCheck10 As NTSInformatica.NTSListBox
  Public WithEvents lbCheck6 As NTSInformatica.NTSLabel
  Public WithEvents liCheck8 As NTSInformatica.NTSListBox
  Public WithEvents lbCheck4 As NTSInformatica.NTSLabel
  Public WithEvents liCheck6 As NTSInformatica.NTSListBox
  Public WithEvents liCheck4 As NTSInformatica.NTSListBox
  Public WithEvents lbCheck2 As NTSInformatica.NTSLabel
  Public WithEvents liCheck2 As NTSInformatica.NTSListBox
  Public WithEvents lbCheck9 As NTSInformatica.NTSLabel
  Public WithEvents lbCheck7 As NTSInformatica.NTSLabel
  Public WithEvents liCheck9 As NTSInformatica.NTSListBox
  Public WithEvents lbCheck5 As NTSInformatica.NTSLabel
  Public WithEvents liCheck7 As NTSInformatica.NTSListBox
  Public WithEvents lbCheck3 As NTSInformatica.NTSLabel
  Public WithEvents liCheck5 As NTSInformatica.NTSListBox
  Public WithEvents liCheck3 As NTSInformatica.NTSListBox
  Public WithEvents lbCheck1 As NTSInformatica.NTSLabel
  Public WithEvents liCheck1 As NTSInformatica.NTSListBox
End Class