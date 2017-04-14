Partial Public Class FRM__DESG
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
  Public WithEvents pnDx As NTSInformatica.NTSPanel
  Public WithEvents pnSx As NTSInformatica.NTSPanel
  Public WithEvents lbXx_stato As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codcomu As NTSInformatica.NTSLabel
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tsDesg As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnTab1 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents pnTab2 As NTSInformatica.NTSPanel
  Public WithEvents NtsTabPage3 As NTSInformatica.NTSTabPage
  Public WithEvents pnTab3 As NTSInformatica.NTSPanel
  Public WithEvents edDd_note As NTSInformatica.NTSMemoBox
  Public WithEvents pbTab2Sx As NTSInformatica.NTSPanel
  Public WithEvents lbXx_codzona As NTSInformatica.NTSLabel
  Public WithEvents edDd_codzona As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDd_codzona As NTSInformatica.NTSLabel
  Public WithEvents lbXx_agente As NTSInformatica.NTSLabel
  Public WithEvents edDd_agente As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDd_agente As NTSInformatica.NTSLabel
  Public WithEvents lbXx_agente2 As NTSInformatica.NTSLabel
  Public WithEvents edDd_agente2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDd_agente2 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_vett2 As NTSInformatica.NTSLabel
  Public WithEvents edDd_vett2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDd_vett2 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_vett As NTSInformatica.NTSLabel
  Public WithEvents edDd_vett As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDd_vett As NTSInformatica.NTSLabel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents cmdCreaDaLead As NTSInformatica.NTSButton
  Public WithEvents lbLead As NTSInformatica.NTSLabel
  Public WithEvents lbDd_codlead As NTSInformatica.NTSLabel
  Public WithEvents cmdEstensioni As NTSInformatica.NTSButton
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImportaIndir As NTSInformatica.NTSBarMenuItem
  Public WithEvents lbTitle As NTSInformatica.NTSLabel
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbApriLead As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbDd_longitud As NTSInformatica.NTSLabel
  Public WithEvents lbDd_latitud As NTSInformatica.NTSLabel
  Public WithEvents edDd_longitud As NTSInformatica.NTSTextBoxStr
  Public WithEvents edDd_latitud As NTSInformatica.NTSTextBoxStr
  Public WithEvents cbDd_status As NTSInformatica.NTSComboBox
  Public WithEvents lbDd_status As NTSInformatica.NTSLabel
  Public WithEvents lbDd_listino As NTSInformatica.NTSLabel
  Public WithEvents edDd_listino As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_listinoDes As NTSInformatica.NTSLabel
  Public WithEvents lbXx_listino As NTSInformatica.NTSLabel
  Public WithEvents lbDd_coduffpa As NTSInformatica.NTSLabel
  Public WithEvents edDd_coduffpa As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_acuradi As NTSInformatica.NTSLabel
  Public WithEvents cbDd_acuradi As NTSInformatica.NTSComboBox
  Public WithEvents lbXx_porto As NTSInformatica.NTSLabel
  Public WithEvents edDd_porto As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDd_porto As NTSInformatica.NTSLabel
  Public WithEvents tlbGoogleMaps As NTSInformatica.NTSBarButtonItem
End Class
