Partial Public Class FRM__UICF
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
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents grUicf As NTSInformatica.NTSGrid
  Public WithEvents grvUicf As NTSInformatica.NTSGridView
  Public WithEvents ui_db As NTSInformatica.NTSGridColumn
  Public WithEvents ui_ditta As NTSInformatica.NTSGridColumn
  Public WithEvents ui_tipodoc As NTSInformatica.NTSGridColumn
  Public WithEvents ui_ruolo As NTSInformatica.NTSGridColumn
  Public WithEvents ui_opnome As NTSInformatica.NTSGridColumn
  Public WithEvents ui_codling As NTSInformatica.NTSGridColumn
  Public WithEvents ui_nomprop As NTSInformatica.NTSGridColumn
  Public WithEvents ui_valprop As NTSInformatica.NTSGridColumn
  Public WithEvents ui_usascript As NTSInformatica.NTSGridColumn
  Public WithEvents ui_script As NTSInformatica.NTSGridColumn
  Public WithEvents ui_parent As NTSInformatica.NTSGridColumn
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents pnSx As NTSInformatica.NTSPanel
  Public WithEvents trUicf As NTSInformatica.NTSTreeView
  Public WithEvents ImageList1 As System.Windows.Forms.ImageList
  Public WithEvents tlbTrova As NTSInformatica.NTSBarMenuItem
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents cbVisualizza As NTSInformatica.NTSComboBox
  Public WithEvents lbVisualizza As NTSInformatica.NTSLabel
  Public WithEvents tlbEsporta As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbImporta As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbCancellaCartella As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbEsportaAgg As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsportaGriglia As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPersDelete As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbPersImport As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbPersExport As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbTrasferisciConf As NTSInformatica.NTSBarButtonItem
End Class
