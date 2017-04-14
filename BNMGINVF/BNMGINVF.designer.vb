Partial Public Class FRMMGINVF
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
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents edCodmaga As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodmagaLabel As NTSInformatica.NTSLabel
  Public WithEvents edDatainv As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDatainv As NTSInformatica.NTSLabel
  Public WithEvents fmOrigine As NTSInformatica.NTSGroupBox
  Public WithEvents opDocmagOrig As NTSInformatica.NTSRadioButton
  Public WithEvents opLselOrig As NTSInformatica.NTSRadioButton
  Public WithEvents lbCodmaga As NTSInformatica.NTSLabel
  Public WithEvents lbCausInvLabel As NTSInformatica.NTSLabel
  Public WithEvents lbCausInv As NTSInformatica.NTSLabel
  Public WithEvents edCausInv As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAlistaOrig As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmSelezione As NTSInformatica.NTSGroupBox
  Public WithEvents edAlistaDest As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDalistaDest As NTSInformatica.NTSTextBoxNum
  Public WithEvents opArtBloc As NTSInformatica.NTSRadioButton
  Public WithEvents opLselDest As NTSInformatica.NTSRadioButton
  Public WithEvents edDalistaOrig As NTSInformatica.NTSTextBoxNum
  Public WithEvents cmdArtSel As NTSInformatica.NTSButton
  Public WithEvents opArtSel As NTSInformatica.NTSRadioButton
  Public WithEvents fmDoc As NTSInformatica.NTSGroupBox
  Public WithEvents lbCodcausmeno As NTSInformatica.NTSLabel
  Public WithEvents lbCodcauspiu As NTSInformatica.NTSLabel
  Public WithEvents lbCodconto As NTSInformatica.NTSLabel
  Public WithEvents lbCodtpbf As NTSInformatica.NTSLabel
  Public WithEvents lbCodcausmenoLabel As NTSInformatica.NTSLabel
  Public WithEvents lbCodcauspiuLabel As NTSInformatica.NTSLabel
  Public WithEvents lbCodcontoLabel As NTSInformatica.NTSLabel
  Public WithEvents lbCodtpbfLabel As NTSInformatica.NTSLabel
  Public WithEvents edCodcausmeno As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCodcauspiu As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCodtpbf As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCodconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edNumdoc As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbEstremiDoc As NTSInformatica.NTSLabel
  Public WithEvents edListino As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipVal As NTSInformatica.NTSComboBox
  Public WithEvents lbTipVal As NTSInformatica.NTSLabel
  Public WithEvents ckElabora As NTSInformatica.NTSCheckBox
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
  Public WithEvents edAnno As NTSInformatica.NTSTextBoxNum
  Public WithEvents opDaDocOrig As NTSInformatica.NTSRadioButton
End Class
