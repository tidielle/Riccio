Partial Public Class FRMMGLISE
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
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnGriglia As NTSInformatica.NTSPanel
  Public WithEvents pnDatiRiga As NTSInformatica.NTSPanel
  Public WithEvents pnFiltroColonna As NTSInformatica.NTSPanel
  Public WithEvents grList As NTSInformatica.NTSGrid
  Public WithEvents grvList As NTSInformatica.NTSGridView
  Public WithEvents fmDatiRiga As NTSInformatica.NTSGroupBox
  Public WithEvents fmFiltriColonna As NTSInformatica.NTSGroupBox
  Public WithEvents ckSconti4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckSconti3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckSconti2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckSconti1 As NTSInformatica.NTSCheckBox
  Public WithEvents ckListino5 As NTSInformatica.NTSCheckBox
  Public WithEvents ckListino4 As NTSInformatica.NTSCheckBox
  Public WithEvents ckListino3 As NTSInformatica.NTSCheckBox
  Public WithEvents ckListino2 As NTSInformatica.NTSCheckBox
  Public WithEvents ckListino1 As NTSInformatica.NTSCheckBox
  Public WithEvents ls_conto As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codart As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descart As NTSInformatica.NTSGridColumn
  Public WithEvents ls_fase As NTSInformatica.NTSGridColumn
  Public WithEvents xx_fase As NTSInformatica.NTSGridColumn
  Public WithEvents ls_listino1 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_prz1 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_daquant1 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_aquant1 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_perqta As NTSInformatica.NTSGridColumn
  Public WithEvents ls_unmis1 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codlavo1 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desclavo1 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_tipo1 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_dadata1 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_adata1 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codvalu1 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descvalu1 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_listino2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_prz2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_daquant2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_aquant2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_unmis2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codlavo2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desclavo2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_tipo2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_dadata2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_adata2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codvalu2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descvalu2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_listino3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_prz3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_daquant3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_aquant3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_unmis3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codlavo3 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desclavo3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_tipo3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_dadata3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_adata3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codvalu3 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descvalu3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_listino4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_prz4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_daquant4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_aquant4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_unmis4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codlavo4 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desclavo4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_tipo4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_dadata4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_adata4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codvalu4 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descvalu4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_listino5 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_prz5 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_daquant5 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_aquant5 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_unmis5 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codlavo5 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desclavo5 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_tipo5 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_dadata5 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_adata5 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codvalu5 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descvalu5 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_prznet1 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_prznet2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_prznet3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_prznet4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_prznet5 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont1 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont5 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont6 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scdaquant As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scaquant As NTSInformatica.NTSGridColumn
  Public WithEvents ls_dadatasc As NTSInformatica.NTSGridColumn
  Public WithEvents ls_adatasc As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codtpro As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descpromo As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont1_2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont2_2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont3_2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont4_2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont5_2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont6_2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scdaquant_2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scaquant_2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_dadatasc_2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_adatasc_2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codtpro_2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descpromo_2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont1_3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont2_3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont3_3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont4_3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont5_3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont6_3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scdaquant_3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scaquant_3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_dadatasc_3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_adatasc_3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codtpro_3 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descpromo_3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont1_4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont2_4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont3_4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont4_4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont5_4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scont6_4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scdaquant_4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_scaquant_4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_dadatasc_4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_adatasc_4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codtpro_4 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descpromo_4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_clscan As NTSInformatica.NTSGridColumn
  Public WithEvents ls_clscar As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desccli As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codpromo1 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descpromo1 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codpromo2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descpromo2 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codpromo3 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descpromo3 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codpromo4 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descpromo4 As NTSInformatica.NTSGridColumn
  Public WithEvents ls_codpromo5 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_descpromo5 As NTSInformatica.NTSGridColumn
  Public WithEvents ckBlocca As NTSInformatica.NTSCheckBox
  Public WithEvents lbDesFase As NTSInformatica.NTSLabel
  Public WithEvents lbDesArtico As NTSInformatica.NTSLabel
  Public WithEvents lbDesConto As NTSInformatica.NTSLabel
  Public WithEvents edFase As NTSInformatica.NTSTextBoxNum
  Public WithEvents edConto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edArtico As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbFase As NTSInformatica.NTSLabel
  Public WithEvents lbArtico As NTSInformatica.NTSLabel
  Public WithEvents lbConto As NTSInformatica.NTSLabel
  Public WithEvents ls_tiposc As New NTSInformatica.NTSGridColumn
  Public WithEvents ls_tiposc_2 As New NTSInformatica.NTSGridColumn
  Public WithEvents ls_tiposc_3 As New NTSInformatica.NTSGridColumn
  Public WithEvents ls_tiposc_4 As New NTSInformatica.NTSGridColumn
  Public WithEvents xx_clscan As NTSInformatica.NTSGridColumn
  Public WithEvents xx_clscar As NTSInformatica.NTSGridColumn
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbCancellaTutto As NTSInformatica.NTSBarButtonItem
  Public WithEvents tmTick As System.Windows.Forms.Timer
  Public WithEvents ckColoreNero As NTSInformatica.NTSCheckBox
  Public WithEvents tlbElabora As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbRossoDesc As NTSInformatica.NTSLabel
  Public WithEvents lbRosso As NTSInformatica.NTSLabel
  Public WithEvents lbBDesc As NTSInformatica.NTSLabel
  Public WithEvents lbB As NTSInformatica.NTSLabel
  Public WithEvents ar_codart As NTSInformatica.NTSGridColumn
  Public WithEvents xx_desint As NTSInformatica.NTSGridColumn
  Public WithEvents ls_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents tlbVarPrzSc As NTSInformatica.NTSBarMenuItem
End Class
