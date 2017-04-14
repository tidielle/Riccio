Partial Public Class FRMVEPNFA_F
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

  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbVislog As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbIntegr As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbNoteData As NTSInformatica.NTSLabel
  Public WithEvents lbDatreg As NTSInformatica.NTSLabel
  Public WithEvents lbEscompLabel As NTSInformatica.NTSLabel
  Public WithEvents edEscomp As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDatreg As NTSInformatica.NTSTextBoxData
  Public WithEvents lbEscomp As NTSInformatica.NTSLabel
  Public WithEvents fmSelezione As NTSInformatica.NTSGroupBox
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
  Public WithEvents ckTiporkP As NTSInformatica.NTSCheckBox
  Public WithEvents ckTiporkS As NTSInformatica.NTSCheckBox
  Public WithEvents ckTiporkI As NTSInformatica.NTSCheckBox
  Public WithEvents ckTiporkF As NTSInformatica.NTSCheckBox
  Public WithEvents ckTiporkN As NTSInformatica.NTSCheckBox
  Public WithEvents ckTiporkC As NTSInformatica.NTSCheckBox
  Public WithEvents ckTiporkD As NTSInformatica.NTSCheckBox
  Public WithEvents ckTiporkA As NTSInformatica.NTSCheckBox
  Public WithEvents pnSelexDx As NTSInformatica.NTSPanel
  Public WithEvents pnSelexSx As NTSInformatica.NTSPanel
  Public WithEvents lbBarra As NTSInformatica.NTSLabel
  Public WithEvents lbTipobfLabel As NTSInformatica.NTSLabel
  Public WithEvents lbNumdocDaa As NTSInformatica.NTSLabel
  Public WithEvents lbDatadaa As NTSInformatica.NTSLabel
  Public WithEvents lbAnnodoc As NTSInformatica.NTSLabel
  Public WithEvents ckRielab As NTSInformatica.NTSCheckBox
  Public WithEvents ckContinc As NTSInformatica.NTSCheckBox
  Public WithEvents ckTiporkK As NTSInformatica.NTSCheckBox
  Public WithEvents ckTiporkL As NTSInformatica.NTSCheckBox
  Public WithEvents edAnnodoc As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSeriedoc As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDatdocA As NTSInformatica.NTSTextBoxData
  Public WithEvents edDatdocDa As NTSInformatica.NTSTextBoxData
  Public WithEvents edNumdocA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNumdocDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTipobf As NTSInformatica.NTSLabel
  Public WithEvents edTipobf As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckSerie As NTSInformatica.NTSCheckBox
  Public WithEvents ckContCompensDDT As NTSInformatica.NTSCheckBox
  Public WithEvents ckContCompensNoteAcc As NTSInformatica.NTSCheckBox
  Public WithEvents ckContIncDDT As NTSInformatica.NTSCheckBox
  Public WithEvents ckTiporkB As NTSInformatica.NTSCheckBox
  Public WithEvents lbContCompensDDT As NTSInformatica.NTSLabel
  Public WithEvents ckTiporkJ As NTSInformatica.NTSCheckBox
  Public WithEvents ckTiporkE As NTSInformatica.NTSCheckBox
  Public WithEvents lbBarra2 As NTSInformatica.NTSLabel
  Public WithEvents ckTiporkJDiff As NTSInformatica.NTSCheckBox
  Public WithEvents ckTiporkNDiff As NTSInformatica.NTSCheckBox
  Public WithEvents tlbStampavideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents ckCheckProtocolli As NTSInformatica.NTSCheckBox
  Public WithEvents lbNumdocA As NTSInformatica.NTSLabel
  Public WithEvents lbDatada As NTSInformatica.NTSLabel
  Public WithEvents ckContabAncheConScadSald As NTSInformatica.NTSCheckBox
  Public WithEvents ckDelEffettiNoPres As NTSInformatica.NTSCheckBox
  Public WithEvents ckGenEffetti As NTSInformatica.NTSCheckBox
End Class