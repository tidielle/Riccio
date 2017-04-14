Partial Public Class FRM__ANIV_F
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
  Public WithEvents tlbAnno As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbAttivita As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRegistri As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbAi_sezliqriep As NTSInformatica.NTSLabel
  Public WithEvents cbAi_sezliqriep As NTSInformatica.NTSComboBox
  Public WithEvents lbAi_verdociva As NTSInformatica.NTSLabel
  Public WithEvents cbAi_verdociva As NTSInformatica.NTSComboBox
  Public WithEvents lbAi_calcacc12 As NTSInformatica.NTSLabel
  Public WithEvents cbAi_calcacc12 As NTSInformatica.NTSComboBox
  Public WithEvents lbAi_pgulrir As NTSInformatica.NTSLabel
  Public WithEvents edAi_pgulrir As NTSInformatica.NTSTextBoxNum
  Public WithEvents pnControlli As NTSInformatica.NTSPanel
  Public WithEvents fmWeb As NTSInformatica.NTSGroupBox
  Public WithEvents ckAi_comelfl As NTSInformatica.NTSCheckBox
  Public WithEvents lbAi_comelind As NTSInformatica.NTSLabel
  Public WithEvents edAi_comelisp As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAi_comelisp As NTSInformatica.NTSLabel
  Public WithEvents edAi_comelind As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbAi_comeltis As NTSInformatica.NTSLabel
  Public WithEvents cbAi_comeltis As NTSInformatica.NTSComboBox
End Class