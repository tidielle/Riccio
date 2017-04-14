Partial Public Class FRMMGCLAS
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
  Public WithEvents tsClas As NTSInformatica.NTSTabControl
  Public WithEvents NtsTabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents NtsTabPage2 As NTSInformatica.NTSTabPage
  Public WithEvents NtsPanel2 As NTSInformatica.NTSPanel
  Public WithEvents grArti As NTSInformatica.NTSGrid
  Public WithEvents grvArti As NTSInformatica.NTSGridView
  Public WithEvents xx_seleziona As NTSInformatica.NTSGridColumn
  Public WithEvents ar_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ar_descr As NTSInformatica.NTSGridColumn
  Public WithEvents ar_desint As NTSInformatica.NTSGridColumn
  Public WithEvents codditt As NTSInformatica.NTSGridColumn
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSelAll As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbDesAll As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRicarica As NTSInformatica.NTSBarButtonItem
  Public WithEvents NtsPanel1 As NTSInformatica.NTSPanel
  Public WithEvents grClas As NTSInformatica.NTSGrid
  Public WithEvents grvClas As NTSInformatica.NTSGridView
  Public WithEvents lbDescr As NTSInformatica.NTSLabel
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents tlbDesLingua As NTSInformatica.NTSBarMenuItem
  Public WithEvents GridColumn1_112_4 As NTSInformatica.NTSGridColumn
  Public WithEvents acl_ordin As NTSInformatica.NTSGridColumn
  Public WithEvents tlbRiordinaAlbero As NTSInformatica.NTSBarButtonItem

End Class