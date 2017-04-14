Partial Class FRMMGLOTC
  Inherits FRM__CHIL

  <System.Diagnostics.DebuggerNonUserCode()> _
  Public Sub New()
    MyBase.New()

    ''This call is required by the Windows Form Designer.
    'InitializeComponent()

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
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents lbXx_fase As NTSInformatica.NTSLabel
  Public WithEvents lbXx_magaz As NTSInformatica.NTSLabel
  Public WithEvents lbXx_codart As NTSInformatica.NTSLabel
  Public WithEvents lbAr_fase As NTSInformatica.NTSLabel
  Public WithEvents lbAr_magaz As NTSInformatica.NTSLabel
  Public WithEvents lbAr_codart As NTSInformatica.NTSLabel
  Public WithEvents lbFase As NTSInformatica.NTSLabel
  Public WithEvents lbMagaz As NTSInformatica.NTSLabel
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents grLotc As NTSInformatica.NTSGrid
  Public WithEvents grvLotc As NTSInformatica.NTSGridView
  Public WithEvents apt_esist01 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist02 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist03 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist04 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist05 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist06 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist07 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist08 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist09 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist10 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist11 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist12 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist13 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist14 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist15 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist16 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_79_16 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_579_17 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_532_18 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_141_19 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_766_20 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_407_21 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_32_22 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_657_23 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist17 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist18 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist19 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist20 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist21 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist22 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist23 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_esist24 As NTSInformatica.NTSGridColumn
  Public WithEvents tlbCommessa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUbicazioni As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbLotti As NTSInformatica.NTSBarButtonItem
  Public WithEvents apt_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents apt_lotto As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lotto As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ubicaz As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot01 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot02 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot03 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot04 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot05 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot06 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot07 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot08 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot09 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot10 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot11 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot12 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot13 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot14 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot15 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot16 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot17 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot18 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot19 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot20 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot21 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot22 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot23 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_prenot24 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin01 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin02 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin03 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin04 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin05 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin06 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin07 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin08 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin09 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin10 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin11 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin12 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin13 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin14 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin15 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin16 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin17 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin18 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin19 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin20 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin21 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin22 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin23 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_ordin24 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_751_77 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_95_78 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_469_79 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_860_80 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_188_81 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_594_82 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_0_83 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_375_84 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_813_85 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_203_86 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_625_87 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_0_88 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_422_89 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_843_90 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_265_91 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_640_92 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_77_93 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_515_94 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_15_95 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_499_96 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_936_97 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_343_98 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_749_99 As NTSInformatica.NTSGridColumn
  Public WithEvents GridColumn1_139_100 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg01 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg02 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg03 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg04 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg05 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg06 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg07 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg08 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg09 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg10 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg11 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg12 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg13 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg14 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg15 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg16 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg17 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg18 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg19 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg20 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg21 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg22 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg23 As NTSInformatica.NTSGridColumn
  Public WithEvents apt_impeg24 As NTSInformatica.NTSGridColumn

End Class