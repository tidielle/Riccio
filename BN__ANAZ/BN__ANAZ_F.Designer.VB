Partial Public Class FRM__ANAZ_F
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
  Public WithEvents lbNota As NTSInformatica.NTSLabel
  Public WithEvents lbTb_latitud As NTSInformatica.NTSLabel
  Public WithEvents edTb_latitud As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_longitud As NTSInformatica.NTSLabel
  Public WithEvents edTb_longitud As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbXx_azcodcaf As NTSInformatica.NTSLabel
  Public WithEvents lbTb_azcodcaf As NTSInformatica.NTSLabel
  Public WithEvents edTb_azcodcaf As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmDatiContabili As NTSInformatica.NTSGroupBox
  Public WithEvents NtsGroupBox1 As NTSInformatica.NTSGroupBox
  Public WithEvents edTb_dtulliqtri As NTSInformatica.NTSTextBoxData
  Public WithEvents edTb_dtulliq As NTSInformatica.NTSTextBoxData
  Public WithEvents lbTb_dtulliq As NTSInformatica.NTSLabel
  Public WithEvents edTb_uffiva As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_uffiva As NTSInformatica.NTSLabel
  Public WithEvents edTb_azrags2 As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdSelImmagine As NTSInformatica.NTSButton
  Public WithEvents lbTb_imagename As NTSInformatica.NTSLabel
  Public WithEvents OpenFileDialog1 As New NTSOpenFileDialog
  Public WithEvents edTb_imagename As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbInfo As NTSInformatica.NTSLabel
  Public WithEvents cmdDelImmagine As NTSInformatica.NTSButton


End Class