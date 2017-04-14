Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGTERM
  Public oCallParams As CLE__CLDP
  Public oCleDocu As CLEMGDOCU

  Public bInLoad As Boolean = False
  Public bOnLoading As Boolean = False
  Public dcImport, dcExport As New BindingSource

#Region "Variabili"
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents ckExecute As NTSInformatica.NTSCheckBox
  Public WithEvents lbCommand As NTSInformatica.NTSLabel
  Public WithEvents cmdCommand As NTSInformatica.NTSButton
  Public WithEvents pnMain As NTSInformatica.NTSPanel
  Public WithEvents frmTipofile As NTSInformatica.NTSGroupBox
  Public WithEvents lbDecSep As NTSInformatica.NTSLabel
  Public WithEvents edSepField As NTSInformatica.NTSTextBoxStr
  Public WithEvents opDelim As NTSInformatica.NTSRadioButton
  Public WithEvents opFixed As NTSInformatica.NTSRadioButton
  Public WithEvents lbNomeFile As NTSInformatica.NTSLabel
  Public WithEvents edNomeFile As NTSInformatica.NTSTextBoxStr
  Public WithEvents cmdNomeFile As NTSInformatica.NTSButton
  Public WithEvents edDecSep As NTSInformatica.NTSTextBoxStr
  Public WithEvents frmCampi As NTSInformatica.NTSGroupBox
  Public WithEvents ckShowLog As NTSInformatica.NTSCheckBox
  Public WithEvents ckIgnore1rk As NTSInformatica.NTSCheckBox
  Public WithEvents ckDelFile As NTSInformatica.NTSCheckBox
  Public WithEvents ckSilent As NTSInformatica.NTSCheckBox
  Public WithEvents opCodarfo As NTSInformatica.NTSRadioButton
  Public WithEvents opBarcode As NTSInformatica.NTSRadioButton
  Public WithEvents opCodart As NTSInformatica.NTSRadioButton
  Public WithEvents lbPosQua As NTSInformatica.NTSLabel
  Public WithEvents lbPosDescr As NTSInformatica.NTSLabel
  Public WithEvents lbPosArt As NTSInformatica.NTSLabel
  Public WithEvents edPosArt As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPosPre As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPosQua As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPosDescr As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckBarcodeConfez As NTSInformatica.NTSCheckBox
  Public WithEvents lbNota As NTSInformatica.NTSLabel
  Public WithEvents ckPrezzo As NTSInformatica.NTSCheckBox
  Public WithEvents ckQuant As NTSInformatica.NTSCheckBox
  Public WithEvents ckDescrizione As NTSInformatica.NTSCheckBox
  Public WithEvents lbCinque As NTSInformatica.NTSLabel
  Public WithEvents lbQuattro As NTSInformatica.NTSLabel
  Public WithEvents lbDue As NTSInformatica.NTSLabel
  Public WithEvents lbUno As NTSInformatica.NTSLabel
  Public WithEvents lbDivPrezzo As NTSInformatica.NTSLabel
  Public WithEvents lbDivQuant As NTSInformatica.NTSLabel
  Public WithEvents edDivPrezzo As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDivQuant As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPreA As NTSInformatica.NTSLabel
  Public WithEvents lbPreDa As NTSInformatica.NTSLabel
  Public WithEvents edPreA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPreDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbQuantA As NTSInformatica.NTSLabel
  Public WithEvents lbQuantDa As NTSInformatica.NTSLabel
  Public WithEvents edQuantA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edQuantDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDescrA As NTSInformatica.NTSLabel
  Public WithEvents lbDescrDa As NTSInformatica.NTSLabel
  Public WithEvents edDescrA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDescrDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbArtA As NTSInformatica.NTSLabel
  Public WithEvents lbArtDa As NTSInformatica.NTSLabel
  Public WithEvents edArtA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edArtDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPosPre As NTSInformatica.NTSLabel
  Public WithEvents lbDivSc4 As NTSInformatica.NTSLabel
  Public WithEvents edDivSc4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSc4A As NTSInformatica.NTSLabel
  Public WithEvents lbSc4Da As NTSInformatica.NTSLabel
  Public WithEvents edSc4A As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSc4Da As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPosSc4 As NTSInformatica.NTSLabel
  Public WithEvents edPosSc4 As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckSconto4 As NTSInformatica.NTSCheckBox
  Public WithEvents lbNove As NTSInformatica.NTSLabel
  Public WithEvents lbDivSc3 As NTSInformatica.NTSLabel
  Public WithEvents edDivSc3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSc3A As NTSInformatica.NTSLabel
  Public WithEvents lbSc3Da As NTSInformatica.NTSLabel
  Public WithEvents edSc3A As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSc3Da As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPosSc3 As NTSInformatica.NTSLabel
  Public WithEvents edPosSc3 As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckSconto3 As NTSInformatica.NTSCheckBox
  Public WithEvents lbOtto As NTSInformatica.NTSLabel
  Public WithEvents lbDivSc2 As NTSInformatica.NTSLabel
  Public WithEvents edDivSc2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSc2A As NTSInformatica.NTSLabel
  Public WithEvents lbSc2Da As NTSInformatica.NTSLabel
  Public WithEvents edSc2A As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSc2Da As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPosSc2 As NTSInformatica.NTSLabel
  Public WithEvents edPosSc2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckSconto2 As NTSInformatica.NTSCheckBox
  Public WithEvents lbSette As NTSInformatica.NTSLabel
  Public WithEvents lbDivSc1 As NTSInformatica.NTSLabel
  Public WithEvents edDivSc1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSc1A As NTSInformatica.NTSLabel
  Public WithEvents lbSc1Da As NTSInformatica.NTSLabel
  Public WithEvents edSc1A As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSc1Da As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPosSc1 As NTSInformatica.NTSLabel
  Public WithEvents edPosSc1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckSconto1 As NTSInformatica.NTSCheckBox
  Public WithEvents lbSei As NTSInformatica.NTSLabel
  Public WithEvents lbDivSc6 As NTSInformatica.NTSLabel
  Public WithEvents edDivSc6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSc6A As NTSInformatica.NTSLabel
  Public WithEvents lbSc6Da As NTSInformatica.NTSLabel
  Public WithEvents edSc6A As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSc6Da As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPosSc6 As NTSInformatica.NTSLabel
  Public WithEvents edPosSc6 As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckSconto6 As NTSInformatica.NTSCheckBox
  Public WithEvents lbUndici As NTSInformatica.NTSLabel
  Public WithEvents lbDivSc5 As NTSInformatica.NTSLabel
  Public WithEvents edDivSc5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSc5A As NTSInformatica.NTSLabel
  Public WithEvents lbSc5Da As NTSInformatica.NTSLabel
  Public WithEvents edSc5A As NTSInformatica.NTSTextBoxNum
  Public WithEvents edSc5Da As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPosSc5 As NTSInformatica.NTSLabel
  Public WithEvents edPosSc5 As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckSconto5 As NTSInformatica.NTSCheckBox
  Public WithEvents lbDieci As NTSInformatica.NTSLabel
  Public WithEvents lbDesintA As NTSInformatica.NTSLabel
  Public WithEvents lbDesintDa As NTSInformatica.NTSLabel
  Public WithEvents edDesintA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDesintDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPosDesint As NTSInformatica.NTSLabel
  Public WithEvents edPosDesint As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckDesint As NTSInformatica.NTSCheckBox
  Public WithEvents lbTre As NTSInformatica.NTSLabel
  Public WithEvents lbDatconsA As NTSInformatica.NTSLabel
  Public WithEvents lbDatconsDa As NTSInformatica.NTSLabel
  Public WithEvents edDatconsA As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDatconsDa As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbPosDatcons As NTSInformatica.NTSLabel
  Public WithEvents edPosDatcons As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckDatcons As NTSInformatica.NTSCheckBox
  Public WithEvents lbDodici As NTSInformatica.NTSLabel
  Public WithEvents lbSeparatoriDate As NTSInformatica.NTSLabel
  Public WithEvents opCodartBarcodeCodarfo As NTSInformatica.NTSRadioButton
  Public WithEvents tcImport As NTSInformatica.NTSTabControl
  Public WithEvents tpTerminale As NTSInformatica.NTSTabPage
  Public WithEvents tpExport As NTSInformatica.NTSTabPage
  Public WithEvents tpImport As NTSInformatica.NTSTabPage
  Public WithEvents ckExecuteI As NTSInformatica.NTSCheckBox
  Public WithEvents lbCommandI As NTSInformatica.NTSLabel
  Public WithEvents ckShowLogI As NTSInformatica.NTSCheckBox
  Public WithEvents cmdCommandI As NTSInformatica.NTSButton
  Public WithEvents ckIgnore1rkI As NTSInformatica.NTSCheckBox
  Public WithEvents cmdNomeFileI As NTSInformatica.NTSButton
  Public WithEvents ckDelFileI As NTSInformatica.NTSCheckBox
  Public WithEvents edNomeFileI As NTSInformatica.NTSTextBoxStr
  Public WithEvents ckSilentI As NTSInformatica.NTSCheckBox
  Public WithEvents lbNomeFileI As NTSInformatica.NTSLabel
  Public WithEvents frmTipofileI As NTSInformatica.NTSGroupBox
  Public WithEvents edDecSepI As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbDecSepI As NTSInformatica.NTSLabel
  Public WithEvents edSepFieldI As NTSInformatica.NTSTextBoxStr
  Public WithEvents opDelimI As NTSInformatica.NTSRadioButton
  Public WithEvents opFixedI As NTSInformatica.NTSRadioButton
  Public WithEvents ckExecuteE As NTSInformatica.NTSCheckBox
  Public WithEvents lbCommandE As NTSInformatica.NTSLabel
  Public WithEvents ckShowLogE As NTSInformatica.NTSCheckBox
  Public WithEvents cmdCommandE As NTSInformatica.NTSButton
  Public WithEvents ckGenTracciatoE As NTSInformatica.NTSCheckBox
  Public WithEvents cmdNomeFileE As NTSInformatica.NTSButton
  Public WithEvents edNomeFileE As NTSInformatica.NTSTextBoxStr
  Public WithEvents ckSilentE As NTSInformatica.NTSCheckBox
  Public WithEvents lbNomeFileE As NTSInformatica.NTSLabel
  Public WithEvents frmTipofileE As NTSInformatica.NTSGroupBox
  Public WithEvents edSepFieldE As NTSInformatica.NTSTextBoxStr
  Public WithEvents opDelimE As NTSInformatica.NTSRadioButton
  Public WithEvents opFixedE As NTSInformatica.NTSRadioButton
  Public WithEvents ckProvvI As NTSInformatica.NTSCheckBox
  Public WithEvents ckScontiI As NTSInformatica.NTSCheckBox
  Public WithEvents ckPrezziI As NTSInformatica.NTSCheckBox
  Public WithEvents grExport As NTSInformatica.NTSGrid
  Public WithEvents grvExport As NTSInformatica.NTSGridView
  Public WithEvents grImport As NTSInformatica.NTSGrid
  Public WithEvents grvImport As NTSInformatica.NTSGridView
  Public WithEvents xx_nomei As NTSInformatica.NTSGridColumn
  Public WithEvents xx_luni As NTSInformatica.NTSGridColumn
  Public WithEvents xx_posi As NTSInformatica.NTSGridColumn
  Public WithEvents xx_divi As NTSInformatica.NTSGridColumn
  Public WithEvents xx_nomee As NTSInformatica.NTSGridColumn
  Public WithEvents xx_lune As NTSInformatica.NTSGridColumn
  Public WithEvents xx_pose As NTSInformatica.NTSGridColumn
  Public WithEvents xx_dive As NTSInformatica.NTSGridColumn
  Public WithEvents pnExport As NTSInformatica.NTSPanel
  Public WithEvents NtsPanel1 As NTSInformatica.NTSPanel
  Public WithEvents NtsPanel2 As NTSInformatica.NTSPanel
  Public WithEvents cmdCancellaI As NTSInformatica.NTSButton
  Public WithEvents cmdCancellaE As NTSInformatica.NTSButton
  Public WithEvents lbQtaTagliaE As NTSInformatica.NTSLabel
  Public WithEvents lbTagliaE As NTSInformatica.NTSLabel
  Public WithEvents ckTagliaE As NTSInformatica.NTSCheckBox
  Public WithEvents lbQtaMatE As NTSInformatica.NTSLabel
  Public WithEvents lbMatrE As NTSInformatica.NTSLabel
  Public WithEvents ckMatricE As NTSInformatica.NTSCheckBox
  Public WithEvents edQtaTagliaE As NTSInformatica.NTSTextBoxNum
  Public WithEvents edQtaMatrE As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTagliaE As NTSInformatica.NTSTextBoxNum
  Public WithEvents edMatrE As NTSInformatica.NTSTextBoxNum
  Public WithEvents edQtaTagliaI As NTSInformatica.NTSTextBoxNum
  Public WithEvents edQtaMatrI As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTagliaI As NTSInformatica.NTSTextBoxNum
  Public WithEvents edMatricI As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbQtaTagliaI As NTSInformatica.NTSLabel
  Public WithEvents lbTagliaI As NTSInformatica.NTSLabel
  Public WithEvents ckTagliaI As NTSInformatica.NTSCheckBox
  Public WithEvents lbQtaMatrI As NTSInformatica.NTSLabel
  Public WithEvents lbMatricI As NTSInformatica.NTSLabel
  Public WithEvents ckMatricI As NTSInformatica.NTSCheckBox
  Public WithEvents lbAvviso As NTSInformatica.NTSLabel
  Public WithEvents ckTipoRigaE As NTSInformatica.NTSCheckBox
  Public WithEvents ckTipoRigaI As NTSInformatica.NTSCheckBox
  Public WithEvents ckRimuoviVbcrlf As NTSInformatica.NTSCheckBox
  Public WithEvents cmdCancella As NTSInformatica.NTSButton
  Public WithEvents cmdAggiungi As NTSInformatica.NTSButton
  Public WithEvents cbImpostaFile As NTSInformatica.NTSComboBox
  Public WithEvents lbImpostaFile As NTSInformatica.NTSLabel
  Public WithEvents cmdAggiorna As NTSInformatica.NTSButton
  Public WithEvents pnProfilo As NTSInformatica.NTSPanel
  Public WithEvents cmdOk As NTSInformatica.NTSButton
#End Region

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGTERM))
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.ckExecute = New NTSInformatica.NTSCheckBox
    Me.lbCommand = New NTSInformatica.NTSLabel
    Me.cmdCommand = New NTSInformatica.NTSButton
    Me.pnMain = New NTSInformatica.NTSPanel
    Me.tcImport = New NTSInformatica.NTSTabControl
    Me.tpTerminale = New NTSInformatica.NTSTabPage
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    Me.cmdAggiorna = New NTSInformatica.NTSButton
    Me.cmdCancella = New NTSInformatica.NTSButton
    Me.cmdAggiungi = New NTSInformatica.NTSButton
    Me.cbImpostaFile = New NTSInformatica.NTSComboBox
    Me.lbImpostaFile = New NTSInformatica.NTSLabel
    Me.frmTipofile = New NTSInformatica.NTSGroupBox
    Me.edDecSep = New NTSInformatica.NTSTextBoxStr
    Me.lbDecSep = New NTSInformatica.NTSLabel
    Me.edSepField = New NTSInformatica.NTSTextBoxStr
    Me.opDelim = New NTSInformatica.NTSRadioButton
    Me.opFixed = New NTSInformatica.NTSRadioButton
    Me.frmCampi = New NTSInformatica.NTSGroupBox
    Me.opCodartBarcodeCodarfo = New NTSInformatica.NTSRadioButton
    Me.lbSeparatoriDate = New NTSInformatica.NTSLabel
    Me.lbDatconsA = New NTSInformatica.NTSLabel
    Me.lbDatconsDa = New NTSInformatica.NTSLabel
    Me.edDatconsA = New NTSInformatica.NTSTextBoxNum
    Me.edDatconsDa = New NTSInformatica.NTSTextBoxNum
    Me.lbPosDatcons = New NTSInformatica.NTSLabel
    Me.edPosDatcons = New NTSInformatica.NTSTextBoxNum
    Me.ckDatcons = New NTSInformatica.NTSCheckBox
    Me.lbDodici = New NTSInformatica.NTSLabel
    Me.lbDesintA = New NTSInformatica.NTSLabel
    Me.lbDesintDa = New NTSInformatica.NTSLabel
    Me.edDesintA = New NTSInformatica.NTSTextBoxNum
    Me.edDesintDa = New NTSInformatica.NTSTextBoxNum
    Me.lbPosDesint = New NTSInformatica.NTSLabel
    Me.edPosDesint = New NTSInformatica.NTSTextBoxNum
    Me.ckDesint = New NTSInformatica.NTSCheckBox
    Me.lbTre = New NTSInformatica.NTSLabel
    Me.lbDivSc6 = New NTSInformatica.NTSLabel
    Me.edDivSc6 = New NTSInformatica.NTSTextBoxNum
    Me.lbSc6A = New NTSInformatica.NTSLabel
    Me.lbSc6Da = New NTSInformatica.NTSLabel
    Me.edSc6A = New NTSInformatica.NTSTextBoxNum
    Me.edSc6Da = New NTSInformatica.NTSTextBoxNum
    Me.lbPosSc6 = New NTSInformatica.NTSLabel
    Me.edPosSc6 = New NTSInformatica.NTSTextBoxNum
    Me.ckSconto6 = New NTSInformatica.NTSCheckBox
    Me.lbUndici = New NTSInformatica.NTSLabel
    Me.lbDivSc5 = New NTSInformatica.NTSLabel
    Me.edDivSc5 = New NTSInformatica.NTSTextBoxNum
    Me.lbSc5A = New NTSInformatica.NTSLabel
    Me.lbSc5Da = New NTSInformatica.NTSLabel
    Me.edSc5A = New NTSInformatica.NTSTextBoxNum
    Me.edSc5Da = New NTSInformatica.NTSTextBoxNum
    Me.lbPosSc5 = New NTSInformatica.NTSLabel
    Me.edPosSc5 = New NTSInformatica.NTSTextBoxNum
    Me.ckSconto5 = New NTSInformatica.NTSCheckBox
    Me.lbDieci = New NTSInformatica.NTSLabel
    Me.lbDivSc4 = New NTSInformatica.NTSLabel
    Me.edDivSc4 = New NTSInformatica.NTSTextBoxNum
    Me.lbSc4A = New NTSInformatica.NTSLabel
    Me.lbSc4Da = New NTSInformatica.NTSLabel
    Me.edSc4A = New NTSInformatica.NTSTextBoxNum
    Me.edSc4Da = New NTSInformatica.NTSTextBoxNum
    Me.lbPosSc4 = New NTSInformatica.NTSLabel
    Me.edPosSc4 = New NTSInformatica.NTSTextBoxNum
    Me.ckSconto4 = New NTSInformatica.NTSCheckBox
    Me.lbNove = New NTSInformatica.NTSLabel
    Me.lbDivSc3 = New NTSInformatica.NTSLabel
    Me.edDivSc3 = New NTSInformatica.NTSTextBoxNum
    Me.lbSc3A = New NTSInformatica.NTSLabel
    Me.lbSc3Da = New NTSInformatica.NTSLabel
    Me.edSc3A = New NTSInformatica.NTSTextBoxNum
    Me.edSc3Da = New NTSInformatica.NTSTextBoxNum
    Me.lbPosSc3 = New NTSInformatica.NTSLabel
    Me.edPosSc3 = New NTSInformatica.NTSTextBoxNum
    Me.ckSconto3 = New NTSInformatica.NTSCheckBox
    Me.lbOtto = New NTSInformatica.NTSLabel
    Me.lbDivSc2 = New NTSInformatica.NTSLabel
    Me.edDivSc2 = New NTSInformatica.NTSTextBoxNum
    Me.lbSc2A = New NTSInformatica.NTSLabel
    Me.lbSc2Da = New NTSInformatica.NTSLabel
    Me.edSc2A = New NTSInformatica.NTSTextBoxNum
    Me.edSc2Da = New NTSInformatica.NTSTextBoxNum
    Me.lbPosSc2 = New NTSInformatica.NTSLabel
    Me.edPosSc2 = New NTSInformatica.NTSTextBoxNum
    Me.ckSconto2 = New NTSInformatica.NTSCheckBox
    Me.lbSette = New NTSInformatica.NTSLabel
    Me.lbDivSc1 = New NTSInformatica.NTSLabel
    Me.edDivSc1 = New NTSInformatica.NTSTextBoxNum
    Me.lbSc1A = New NTSInformatica.NTSLabel
    Me.lbSc1Da = New NTSInformatica.NTSLabel
    Me.edSc1A = New NTSInformatica.NTSTextBoxNum
    Me.edSc1Da = New NTSInformatica.NTSTextBoxNum
    Me.lbPosSc1 = New NTSInformatica.NTSLabel
    Me.edPosSc1 = New NTSInformatica.NTSTextBoxNum
    Me.ckSconto1 = New NTSInformatica.NTSCheckBox
    Me.lbSei = New NTSInformatica.NTSLabel
    Me.lbDivPrezzo = New NTSInformatica.NTSLabel
    Me.lbDivQuant = New NTSInformatica.NTSLabel
    Me.edDivPrezzo = New NTSInformatica.NTSTextBoxNum
    Me.edDivQuant = New NTSInformatica.NTSTextBoxNum
    Me.lbPreA = New NTSInformatica.NTSLabel
    Me.lbPreDa = New NTSInformatica.NTSLabel
    Me.edPreA = New NTSInformatica.NTSTextBoxNum
    Me.edPreDa = New NTSInformatica.NTSTextBoxNum
    Me.lbQuantA = New NTSInformatica.NTSLabel
    Me.lbQuantDa = New NTSInformatica.NTSLabel
    Me.edQuantA = New NTSInformatica.NTSTextBoxNum
    Me.edQuantDa = New NTSInformatica.NTSTextBoxNum
    Me.lbDescrA = New NTSInformatica.NTSLabel
    Me.lbDescrDa = New NTSInformatica.NTSLabel
    Me.edDescrA = New NTSInformatica.NTSTextBoxNum
    Me.edDescrDa = New NTSInformatica.NTSTextBoxNum
    Me.lbArtA = New NTSInformatica.NTSLabel
    Me.lbArtDa = New NTSInformatica.NTSLabel
    Me.edArtA = New NTSInformatica.NTSTextBoxNum
    Me.edArtDa = New NTSInformatica.NTSTextBoxNum
    Me.lbPosPre = New NTSInformatica.NTSLabel
    Me.lbPosQua = New NTSInformatica.NTSLabel
    Me.lbPosDescr = New NTSInformatica.NTSLabel
    Me.lbPosArt = New NTSInformatica.NTSLabel
    Me.edPosArt = New NTSInformatica.NTSTextBoxNum
    Me.edPosPre = New NTSInformatica.NTSTextBoxNum
    Me.edPosQua = New NTSInformatica.NTSTextBoxNum
    Me.edPosDescr = New NTSInformatica.NTSTextBoxNum
    Me.ckBarcodeConfez = New NTSInformatica.NTSCheckBox
    Me.ckPrezzo = New NTSInformatica.NTSCheckBox
    Me.ckQuant = New NTSInformatica.NTSCheckBox
    Me.ckDescrizione = New NTSInformatica.NTSCheckBox
    Me.lbCinque = New NTSInformatica.NTSLabel
    Me.lbQuattro = New NTSInformatica.NTSLabel
    Me.lbDue = New NTSInformatica.NTSLabel
    Me.lbUno = New NTSInformatica.NTSLabel
    Me.opCodarfo = New NTSInformatica.NTSRadioButton
    Me.opBarcode = New NTSInformatica.NTSRadioButton
    Me.opCodart = New NTSInformatica.NTSRadioButton
    Me.lbNomeFile = New NTSInformatica.NTSLabel
    Me.ckSilent = New NTSInformatica.NTSCheckBox
    Me.ckShowLog = New NTSInformatica.NTSCheckBox
    Me.edNomeFile = New NTSInformatica.NTSTextBoxStr
    Me.ckDelFile = New NTSInformatica.NTSCheckBox
    Me.ckIgnore1rk = New NTSInformatica.NTSCheckBox
    Me.cmdNomeFile = New NTSInformatica.NTSButton
    Me.tpExport = New NTSInformatica.NTSTabPage
    Me.pnExport = New NTSInformatica.NTSPanel
    Me.ckTipoRigaE = New NTSInformatica.NTSCheckBox
    Me.edQtaTagliaE = New NTSInformatica.NTSTextBoxNum
    Me.edQtaMatrE = New NTSInformatica.NTSTextBoxNum
    Me.edTagliaE = New NTSInformatica.NTSTextBoxNum
    Me.edMatrE = New NTSInformatica.NTSTextBoxNum
    Me.lbQtaTagliaE = New NTSInformatica.NTSLabel
    Me.lbTagliaE = New NTSInformatica.NTSLabel
    Me.ckTagliaE = New NTSInformatica.NTSCheckBox
    Me.lbQtaMatE = New NTSInformatica.NTSLabel
    Me.lbMatrE = New NTSInformatica.NTSLabel
    Me.ckMatricE = New NTSInformatica.NTSCheckBox
    Me.cmdCancellaE = New NTSInformatica.NTSButton
    Me.ckExecuteE = New NTSInformatica.NTSCheckBox
    Me.grExport = New NTSInformatica.NTSGrid
    Me.grvExport = New NTSInformatica.NTSGridView
    Me.xx_nomee = New NTSInformatica.NTSGridColumn
    Me.xx_pose = New NTSInformatica.NTSGridColumn
    Me.xx_lune = New NTSInformatica.NTSGridColumn
    Me.xx_dive = New NTSInformatica.NTSGridColumn
    Me.frmTipofileE = New NTSInformatica.NTSGroupBox
    Me.edSepFieldE = New NTSInformatica.NTSTextBoxStr
    Me.opDelimE = New NTSInformatica.NTSRadioButton
    Me.opFixedE = New NTSInformatica.NTSRadioButton
    Me.lbNomeFileE = New NTSInformatica.NTSLabel
    Me.lbCommandE = New NTSInformatica.NTSLabel
    Me.ckSilentE = New NTSInformatica.NTSCheckBox
    Me.ckRimuoviVbcrlf = New NTSInformatica.NTSCheckBox
    Me.ckShowLogE = New NTSInformatica.NTSCheckBox
    Me.edNomeFileE = New NTSInformatica.NTSTextBoxStr
    Me.cmdCommandE = New NTSInformatica.NTSButton
    Me.cmdNomeFileE = New NTSInformatica.NTSButton
    Me.ckGenTracciatoE = New NTSInformatica.NTSCheckBox
    Me.tpImport = New NTSInformatica.NTSTabPage
    Me.NtsPanel2 = New NTSInformatica.NTSPanel
    Me.ckTipoRigaI = New NTSInformatica.NTSCheckBox
    Me.edQtaTagliaI = New NTSInformatica.NTSTextBoxNum
    Me.edQtaMatrI = New NTSInformatica.NTSTextBoxNum
    Me.edTagliaI = New NTSInformatica.NTSTextBoxNum
    Me.edMatricI = New NTSInformatica.NTSTextBoxNum
    Me.lbQtaTagliaI = New NTSInformatica.NTSLabel
    Me.lbTagliaI = New NTSInformatica.NTSLabel
    Me.ckTagliaI = New NTSInformatica.NTSCheckBox
    Me.lbQtaMatrI = New NTSInformatica.NTSLabel
    Me.lbMatricI = New NTSInformatica.NTSLabel
    Me.ckMatricI = New NTSInformatica.NTSCheckBox
    Me.cmdCancellaI = New NTSInformatica.NTSButton
    Me.ckExecuteI = New NTSInformatica.NTSCheckBox
    Me.grImport = New NTSInformatica.NTSGrid
    Me.grvImport = New NTSInformatica.NTSGridView
    Me.xx_nomei = New NTSInformatica.NTSGridColumn
    Me.xx_posi = New NTSInformatica.NTSGridColumn
    Me.xx_luni = New NTSInformatica.NTSGridColumn
    Me.xx_divi = New NTSInformatica.NTSGridColumn
    Me.frmTipofileI = New NTSInformatica.NTSGroupBox
    Me.edDecSepI = New NTSInformatica.NTSTextBoxStr
    Me.lbDecSepI = New NTSInformatica.NTSLabel
    Me.edSepFieldI = New NTSInformatica.NTSTextBoxStr
    Me.opDelimI = New NTSInformatica.NTSRadioButton
    Me.opFixedI = New NTSInformatica.NTSRadioButton
    Me.ckProvvI = New NTSInformatica.NTSCheckBox
    Me.lbNomeFileI = New NTSInformatica.NTSLabel
    Me.ckScontiI = New NTSInformatica.NTSCheckBox
    Me.ckSilentI = New NTSInformatica.NTSCheckBox
    Me.ckPrezziI = New NTSInformatica.NTSCheckBox
    Me.edNomeFileI = New NTSInformatica.NTSTextBoxStr
    Me.ckDelFileI = New NTSInformatica.NTSCheckBox
    Me.lbCommandI = New NTSInformatica.NTSLabel
    Me.cmdNomeFileI = New NTSInformatica.NTSButton
    Me.ckShowLogI = New NTSInformatica.NTSCheckBox
    Me.ckIgnore1rkI = New NTSInformatica.NTSCheckBox
    Me.cmdCommandI = New NTSInformatica.NTSButton
    Me.lbNota = New NTSInformatica.NTSLabel
    Me.lbAvviso = New NTSInformatica.NTSLabel
    Me.pnProfilo = New NTSInformatica.NTSPanel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckExecute.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMain.SuspendLayout()
    CType(Me.tcImport, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tcImport.SuspendLayout()
    Me.tpTerminale.SuspendLayout()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.cbImpostaFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frmTipofile, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frmTipofile.SuspendLayout()
    CType(Me.edDecSep.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSepField.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opDelim.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFixed.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frmCampi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frmCampi.SuspendLayout()
    CType(Me.opCodartBarcodeCodarfo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatconsA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatconsDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosDatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDesintA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDesintDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosDesint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDesint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDivSc6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSc6A.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSc6Da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosSc6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconto6.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDivSc5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSc5A.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSc5Da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosSc5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconto5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDivSc4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSc4A.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSc4Da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosSc4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconto4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDivSc3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSc3A.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSc3Da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosSc3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconto3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDivSc2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSc2A.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSc2Da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosSc2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconto2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDivSc1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSc1A.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSc1Da.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosSc1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconto1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDivPrezzo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDivQuant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPreA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPreDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQuantA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQuantDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescrA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescrDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArtA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArtDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosArt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosPre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosQua.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosDescr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckBarcodeConfez.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPrezzo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckQuant.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDescrizione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCodarfo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opBarcode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSilent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckShowLog.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNomeFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDelFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckIgnore1rk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tpExport.SuspendLayout()
    CType(Me.pnExport, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnExport.SuspendLayout()
    CType(Me.ckTipoRigaE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQtaTagliaE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQtaMatrE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTagliaE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMatrE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTagliaE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMatricE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckExecuteE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grExport, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvExport, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frmTipofileE, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frmTipofileE.SuspendLayout()
    CType(Me.edSepFieldE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opDelimE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFixedE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSilentE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckRimuoviVbcrlf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckShowLogE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNomeFileE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckGenTracciatoE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tpImport.SuspendLayout()
    CType(Me.NtsPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel2.SuspendLayout()
    CType(Me.ckTipoRigaI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQtaTagliaI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edQtaMatrI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTagliaI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMatricI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTagliaI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMatricI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckExecuteI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grImport, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvImport, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frmTipofileI, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frmTipofileI.SuspendLayout()
    CType(Me.edDecSepI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSepFieldI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opDelimI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFixedI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckProvvI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckScontiI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSilentI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPrezziI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNomeFileI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDelFileI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckShowLogI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckIgnore1rkI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnProfilo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnProfilo.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'frmAuto
    '
    Me.frmAuto.Appearance.BackColor = System.Drawing.Color.Black
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(523, 41)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 24)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImagePath = ""
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(523, 12)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.NTSContextMenu = Nothing
    Me.cmdOk.Size = New System.Drawing.Size(105, 24)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'ckExecute
    '
    Me.ckExecute.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckExecute.Location = New System.Drawing.Point(3, 5)
    Me.ckExecute.Name = "ckExecute"
    Me.ckExecute.NTSCheckValue = "S"
    Me.ckExecute.NTSUnCheckValue = "N"
    Me.ckExecute.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckExecute.Properties.Appearance.Options.UseBackColor = True
    Me.ckExecute.Properties.AutoHeight = False
    Me.ckExecute.Properties.Caption = "Esegui il comando"
    Me.ckExecute.Size = New System.Drawing.Size(112, 19)
    Me.ckExecute.TabIndex = 3
    '
    'lbCommand
    '
    Me.lbCommand.BackColor = System.Drawing.Color.Transparent
    Me.lbCommand.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCommand.Location = New System.Drawing.Point(116, 4)
    Me.lbCommand.Name = "lbCommand"
    Me.lbCommand.NTSDbField = ""
    Me.lbCommand.Size = New System.Drawing.Size(358, 20)
    Me.lbCommand.TabIndex = 4
    Me.lbCommand.Tooltip = ""
    Me.lbCommand.UseMnemonic = False
    '
    'cmdCommand
    '
    Me.cmdCommand.ImagePath = ""
    Me.cmdCommand.ImageText = ""
    Me.cmdCommand.Location = New System.Drawing.Point(479, 4)
    Me.cmdCommand.Name = "cmdCommand"
    Me.cmdCommand.NTSContextMenu = Nothing
    Me.cmdCommand.Size = New System.Drawing.Size(25, 20)
    Me.cmdCommand.TabIndex = 5
    Me.cmdCommand.Text = "..."
    '
    'pnMain
    '
    Me.pnMain.AllowDrop = True
    Me.pnMain.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnMain.Appearance.Options.UseBackColor = True
    Me.pnMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnMain.Controls.Add(Me.tcImport)
    Me.pnMain.Controls.Add(Me.pnProfilo)
    Me.pnMain.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMain.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnMain.Location = New System.Drawing.Point(0, 0)
    Me.pnMain.Name = "pnMain"
    Me.pnMain.NTSActiveTrasparency = True
    Me.pnMain.Size = New System.Drawing.Size(517, 602)
    Me.pnMain.TabIndex = 6
    Me.pnMain.Text = "NtsPanel1"
    '
    'tcImport
    '
    Me.tcImport.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tcImport.Location = New System.Drawing.Point(0, 30)
    Me.tcImport.Name = "tcImport"
    Me.tcImport.SelectedTabPage = Me.tpTerminale
    Me.tcImport.Size = New System.Drawing.Size(517, 572)
    Me.tcImport.TabIndex = 15
    Me.tcImport.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tpTerminale, Me.tpExport, Me.tpImport})
    '
    'tpTerminale
    '
    Me.tpTerminale.AllowDrop = True
    Me.tpTerminale.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.tpTerminale.Appearance.Header.Options.UseFont = True
    Me.tpTerminale.Controls.Add(Me.NtsPanel1)
    Me.tpTerminale.Enable = True
    Me.tpTerminale.Name = "tpTerminale"
    Me.tpTerminale.Size = New System.Drawing.Size(508, 542)
    Me.tpTerminale.Text = "File terminale"
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.ckExecute)
    Me.NtsPanel1.Controls.Add(Me.frmTipofile)
    Me.NtsPanel1.Controls.Add(Me.frmCampi)
    Me.NtsPanel1.Controls.Add(Me.lbNomeFile)
    Me.NtsPanel1.Controls.Add(Me.lbCommand)
    Me.NtsPanel1.Controls.Add(Me.ckSilent)
    Me.NtsPanel1.Controls.Add(Me.ckShowLog)
    Me.NtsPanel1.Controls.Add(Me.edNomeFile)
    Me.NtsPanel1.Controls.Add(Me.cmdCommand)
    Me.NtsPanel1.Controls.Add(Me.ckDelFile)
    Me.NtsPanel1.Controls.Add(Me.ckIgnore1rk)
    Me.NtsPanel1.Controls.Add(Me.cmdNomeFile)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsPanel1.Location = New System.Drawing.Point(0, 0)
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.NTSActiveTrasparency = True
    Me.NtsPanel1.Size = New System.Drawing.Size(508, 542)
    Me.NtsPanel1.TabIndex = 15
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'cmdAggiorna
    '
    Me.cmdAggiorna.ImagePath = ""
    Me.cmdAggiorna.ImageText = ""
    Me.cmdAggiorna.Location = New System.Drawing.Point(366, 4)
    Me.cmdAggiorna.Name = "cmdAggiorna"
    Me.cmdAggiorna.NTSContextMenu = Nothing
    Me.cmdAggiorna.Size = New System.Drawing.Size(70, 23)
    Me.cmdAggiorna.TabIndex = 520
    Me.cmdAggiorna.Text = "Aggiorna"
    '
    'cmdCancella
    '
    Me.cmdCancella.ImagePath = ""
    Me.cmdCancella.ImageText = ""
    Me.cmdCancella.Location = New System.Drawing.Point(437, 4)
    Me.cmdCancella.Name = "cmdCancella"
    Me.cmdCancella.NTSContextMenu = Nothing
    Me.cmdCancella.Size = New System.Drawing.Size(70, 23)
    Me.cmdCancella.TabIndex = 519
    Me.cmdCancella.Text = "Elimina"
    '
    'cmdAggiungi
    '
    Me.cmdAggiungi.ImagePath = ""
    Me.cmdAggiungi.ImageText = ""
    Me.cmdAggiungi.Location = New System.Drawing.Point(295, 4)
    Me.cmdAggiungi.Name = "cmdAggiungi"
    Me.cmdAggiungi.NTSContextMenu = Nothing
    Me.cmdAggiungi.Size = New System.Drawing.Size(70, 23)
    Me.cmdAggiungi.TabIndex = 518
    Me.cmdAggiungi.Text = "Aggiungi"
    '
    'cbImpostaFile
    '
    Me.cbImpostaFile.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbImpostaFile.DataSource = Nothing
    Me.cbImpostaFile.DisplayMember = ""
    Me.cbImpostaFile.Location = New System.Drawing.Point(119, 5)
    Me.cbImpostaFile.Name = "cbImpostaFile"
    Me.cbImpostaFile.NTSDbField = ""
    Me.cbImpostaFile.Properties.AutoHeight = False
    Me.cbImpostaFile.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbImpostaFile.Properties.DropDownRows = 30
    Me.cbImpostaFile.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbImpostaFile.SelectedValue = ""
    Me.cbImpostaFile.Size = New System.Drawing.Size(173, 20)
    Me.cbImpostaFile.TabIndex = 517
    Me.cbImpostaFile.ValueMember = ""
    '
    'lbImpostaFile
    '
    Me.lbImpostaFile.AutoSize = True
    Me.lbImpostaFile.BackColor = System.Drawing.Color.Transparent
    Me.lbImpostaFile.Location = New System.Drawing.Point(8, 8)
    Me.lbImpostaFile.Name = "lbImpostaFile"
    Me.lbImpostaFile.NTSDbField = ""
    Me.lbImpostaFile.Size = New System.Drawing.Size(90, 13)
    Me.lbImpostaFile.TabIndex = 516
    Me.lbImpostaFile.Text = "Impostazione File"
    Me.lbImpostaFile.Tooltip = ""
    Me.lbImpostaFile.UseMnemonic = False
    '
    'frmTipofile
    '
    Me.frmTipofile.AllowDrop = True
    Me.frmTipofile.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frmTipofile.Appearance.Options.UseBackColor = True
    Me.frmTipofile.Controls.Add(Me.edDecSep)
    Me.frmTipofile.Controls.Add(Me.lbDecSep)
    Me.frmTipofile.Controls.Add(Me.edSepField)
    Me.frmTipofile.Controls.Add(Me.opDelim)
    Me.frmTipofile.Controls.Add(Me.opFixed)
    Me.frmTipofile.Cursor = System.Windows.Forms.Cursors.Default
    Me.frmTipofile.Location = New System.Drawing.Point(3, 56)
    Me.frmTipofile.Name = "frmTipofile"
    Me.frmTipofile.Size = New System.Drawing.Size(186, 87)
    Me.frmTipofile.TabIndex = 9
    Me.frmTipofile.Text = "Tipo file"
    '
    'edDecSep
    '
    Me.edDecSep.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDecSep.EditValue = ","
    Me.edDecSep.Location = New System.Drawing.Point(147, 61)
    Me.edDecSep.Name = "edDecSep"
    Me.edDecSep.NTSDbField = ""
    Me.edDecSep.NTSForzaVisZoom = False
    Me.edDecSep.NTSOldValue = ","
    Me.edDecSep.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDecSep.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDecSep.Properties.AutoHeight = False
    Me.edDecSep.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDecSep.Properties.MaxLength = 65536
    Me.edDecSep.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDecSep.Size = New System.Drawing.Size(24, 20)
    Me.edDecSep.TabIndex = 10
    '
    'lbDecSep
    '
    Me.lbDecSep.AutoSize = True
    Me.lbDecSep.BackColor = System.Drawing.Color.Transparent
    Me.lbDecSep.Location = New System.Drawing.Point(2, 64)
    Me.lbDecSep.Name = "lbDecSep"
    Me.lbDecSep.NTSDbField = ""
    Me.lbDecSep.Size = New System.Drawing.Size(105, 13)
    Me.lbDecSep.TabIndex = 9
    Me.lbDecSep.Text = "Separatore decimale"
    Me.lbDecSep.Tooltip = ""
    Me.lbDecSep.UseMnemonic = False
    '
    'edSepField
    '
    Me.edSepField.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSepField.EditValue = ";"
    Me.edSepField.Enabled = False
    Me.edSepField.Location = New System.Drawing.Point(147, 39)
    Me.edSepField.Name = "edSepField"
    Me.edSepField.NTSDbField = ""
    Me.edSepField.NTSForzaVisZoom = False
    Me.edSepField.NTSOldValue = ";"
    Me.edSepField.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSepField.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSepField.Properties.AutoHeight = False
    Me.edSepField.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSepField.Properties.MaxLength = 65536
    Me.edSepField.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSepField.Size = New System.Drawing.Size(24, 20)
    Me.edSepField.TabIndex = 2
    '
    'opDelim
    '
    Me.opDelim.Cursor = System.Windows.Forms.Cursors.Default
    Me.opDelim.Location = New System.Drawing.Point(5, 40)
    Me.opDelim.Name = "opDelim"
    Me.opDelim.NTSCheckValue = "S"
    Me.opDelim.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDelim.Properties.Appearance.Options.UseBackColor = True
    Me.opDelim.Properties.AutoHeight = False
    Me.opDelim.Properties.Caption = "Separatore di campo"
    Me.opDelim.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDelim.Size = New System.Drawing.Size(124, 19)
    Me.opDelim.TabIndex = 1
    '
    'opFixed
    '
    Me.opFixed.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFixed.EditValue = True
    Me.opFixed.Location = New System.Drawing.Point(5, 20)
    Me.opFixed.Name = "opFixed"
    Me.opFixed.NTSCheckValue = "S"
    Me.opFixed.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFixed.Properties.Appearance.Options.UseBackColor = True
    Me.opFixed.Properties.AutoHeight = False
    Me.opFixed.Properties.Caption = "Posizioni fisse"
    Me.opFixed.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFixed.Size = New System.Drawing.Size(99, 19)
    Me.opFixed.TabIndex = 0
    '
    'frmCampi
    '
    Me.frmCampi.AllowDrop = True
    Me.frmCampi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frmCampi.Appearance.Options.UseBackColor = True
    Me.frmCampi.Controls.Add(Me.opCodartBarcodeCodarfo)
    Me.frmCampi.Controls.Add(Me.lbSeparatoriDate)
    Me.frmCampi.Controls.Add(Me.lbDatconsA)
    Me.frmCampi.Controls.Add(Me.lbDatconsDa)
    Me.frmCampi.Controls.Add(Me.edDatconsA)
    Me.frmCampi.Controls.Add(Me.edDatconsDa)
    Me.frmCampi.Controls.Add(Me.lbPosDatcons)
    Me.frmCampi.Controls.Add(Me.edPosDatcons)
    Me.frmCampi.Controls.Add(Me.ckDatcons)
    Me.frmCampi.Controls.Add(Me.lbDodici)
    Me.frmCampi.Controls.Add(Me.lbDesintA)
    Me.frmCampi.Controls.Add(Me.lbDesintDa)
    Me.frmCampi.Controls.Add(Me.edDesintA)
    Me.frmCampi.Controls.Add(Me.edDesintDa)
    Me.frmCampi.Controls.Add(Me.lbPosDesint)
    Me.frmCampi.Controls.Add(Me.edPosDesint)
    Me.frmCampi.Controls.Add(Me.ckDesint)
    Me.frmCampi.Controls.Add(Me.lbTre)
    Me.frmCampi.Controls.Add(Me.lbDivSc6)
    Me.frmCampi.Controls.Add(Me.edDivSc6)
    Me.frmCampi.Controls.Add(Me.lbSc6A)
    Me.frmCampi.Controls.Add(Me.lbSc6Da)
    Me.frmCampi.Controls.Add(Me.edSc6A)
    Me.frmCampi.Controls.Add(Me.edSc6Da)
    Me.frmCampi.Controls.Add(Me.lbPosSc6)
    Me.frmCampi.Controls.Add(Me.edPosSc6)
    Me.frmCampi.Controls.Add(Me.ckSconto6)
    Me.frmCampi.Controls.Add(Me.lbUndici)
    Me.frmCampi.Controls.Add(Me.lbDivSc5)
    Me.frmCampi.Controls.Add(Me.edDivSc5)
    Me.frmCampi.Controls.Add(Me.lbSc5A)
    Me.frmCampi.Controls.Add(Me.lbSc5Da)
    Me.frmCampi.Controls.Add(Me.edSc5A)
    Me.frmCampi.Controls.Add(Me.edSc5Da)
    Me.frmCampi.Controls.Add(Me.lbPosSc5)
    Me.frmCampi.Controls.Add(Me.edPosSc5)
    Me.frmCampi.Controls.Add(Me.ckSconto5)
    Me.frmCampi.Controls.Add(Me.lbDieci)
    Me.frmCampi.Controls.Add(Me.lbDivSc4)
    Me.frmCampi.Controls.Add(Me.edDivSc4)
    Me.frmCampi.Controls.Add(Me.lbSc4A)
    Me.frmCampi.Controls.Add(Me.lbSc4Da)
    Me.frmCampi.Controls.Add(Me.edSc4A)
    Me.frmCampi.Controls.Add(Me.edSc4Da)
    Me.frmCampi.Controls.Add(Me.lbPosSc4)
    Me.frmCampi.Controls.Add(Me.edPosSc4)
    Me.frmCampi.Controls.Add(Me.ckSconto4)
    Me.frmCampi.Controls.Add(Me.lbNove)
    Me.frmCampi.Controls.Add(Me.lbDivSc3)
    Me.frmCampi.Controls.Add(Me.edDivSc3)
    Me.frmCampi.Controls.Add(Me.lbSc3A)
    Me.frmCampi.Controls.Add(Me.lbSc3Da)
    Me.frmCampi.Controls.Add(Me.edSc3A)
    Me.frmCampi.Controls.Add(Me.edSc3Da)
    Me.frmCampi.Controls.Add(Me.lbPosSc3)
    Me.frmCampi.Controls.Add(Me.edPosSc3)
    Me.frmCampi.Controls.Add(Me.ckSconto3)
    Me.frmCampi.Controls.Add(Me.lbOtto)
    Me.frmCampi.Controls.Add(Me.lbDivSc2)
    Me.frmCampi.Controls.Add(Me.edDivSc2)
    Me.frmCampi.Controls.Add(Me.lbSc2A)
    Me.frmCampi.Controls.Add(Me.lbSc2Da)
    Me.frmCampi.Controls.Add(Me.edSc2A)
    Me.frmCampi.Controls.Add(Me.edSc2Da)
    Me.frmCampi.Controls.Add(Me.lbPosSc2)
    Me.frmCampi.Controls.Add(Me.edPosSc2)
    Me.frmCampi.Controls.Add(Me.ckSconto2)
    Me.frmCampi.Controls.Add(Me.lbSette)
    Me.frmCampi.Controls.Add(Me.lbDivSc1)
    Me.frmCampi.Controls.Add(Me.edDivSc1)
    Me.frmCampi.Controls.Add(Me.lbSc1A)
    Me.frmCampi.Controls.Add(Me.lbSc1Da)
    Me.frmCampi.Controls.Add(Me.edSc1A)
    Me.frmCampi.Controls.Add(Me.edSc1Da)
    Me.frmCampi.Controls.Add(Me.lbPosSc1)
    Me.frmCampi.Controls.Add(Me.edPosSc1)
    Me.frmCampi.Controls.Add(Me.ckSconto1)
    Me.frmCampi.Controls.Add(Me.lbSei)
    Me.frmCampi.Controls.Add(Me.lbDivPrezzo)
    Me.frmCampi.Controls.Add(Me.lbDivQuant)
    Me.frmCampi.Controls.Add(Me.edDivPrezzo)
    Me.frmCampi.Controls.Add(Me.edDivQuant)
    Me.frmCampi.Controls.Add(Me.lbPreA)
    Me.frmCampi.Controls.Add(Me.lbPreDa)
    Me.frmCampi.Controls.Add(Me.edPreA)
    Me.frmCampi.Controls.Add(Me.edPreDa)
    Me.frmCampi.Controls.Add(Me.lbQuantA)
    Me.frmCampi.Controls.Add(Me.lbQuantDa)
    Me.frmCampi.Controls.Add(Me.edQuantA)
    Me.frmCampi.Controls.Add(Me.edQuantDa)
    Me.frmCampi.Controls.Add(Me.lbDescrA)
    Me.frmCampi.Controls.Add(Me.lbDescrDa)
    Me.frmCampi.Controls.Add(Me.edDescrA)
    Me.frmCampi.Controls.Add(Me.edDescrDa)
    Me.frmCampi.Controls.Add(Me.lbArtA)
    Me.frmCampi.Controls.Add(Me.lbArtDa)
    Me.frmCampi.Controls.Add(Me.edArtA)
    Me.frmCampi.Controls.Add(Me.edArtDa)
    Me.frmCampi.Controls.Add(Me.lbPosPre)
    Me.frmCampi.Controls.Add(Me.lbPosQua)
    Me.frmCampi.Controls.Add(Me.lbPosDescr)
    Me.frmCampi.Controls.Add(Me.lbPosArt)
    Me.frmCampi.Controls.Add(Me.edPosArt)
    Me.frmCampi.Controls.Add(Me.edPosPre)
    Me.frmCampi.Controls.Add(Me.edPosQua)
    Me.frmCampi.Controls.Add(Me.edPosDescr)
    Me.frmCampi.Controls.Add(Me.ckBarcodeConfez)
    Me.frmCampi.Controls.Add(Me.ckPrezzo)
    Me.frmCampi.Controls.Add(Me.ckQuant)
    Me.frmCampi.Controls.Add(Me.ckDescrizione)
    Me.frmCampi.Controls.Add(Me.lbCinque)
    Me.frmCampi.Controls.Add(Me.lbQuattro)
    Me.frmCampi.Controls.Add(Me.lbDue)
    Me.frmCampi.Controls.Add(Me.lbUno)
    Me.frmCampi.Controls.Add(Me.opCodarfo)
    Me.frmCampi.Controls.Add(Me.opBarcode)
    Me.frmCampi.Controls.Add(Me.opCodart)
    Me.frmCampi.Cursor = System.Windows.Forms.Cursors.Default
    Me.frmCampi.Location = New System.Drawing.Point(4, 149)
    Me.frmCampi.Name = "frmCampi"
    Me.frmCampi.Size = New System.Drawing.Size(499, 365)
    Me.frmCampi.TabIndex = 14
    Me.frmCampi.Text = "Campi"
    '
    'opCodartBarcodeCodarfo
    '
    Me.opCodartBarcodeCodarfo.Cursor = System.Windows.Forms.Cursors.Default
    Me.opCodartBarcodeCodarfo.Location = New System.Drawing.Point(20, 23)
    Me.opCodartBarcodeCodarfo.Name = "opCodartBarcodeCodarfo"
    Me.opCodartBarcodeCodarfo.NTSCheckValue = "S"
    Me.opCodartBarcodeCodarfo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCodartBarcodeCodarfo.Properties.Appearance.Options.UseBackColor = True
    Me.opCodartBarcodeCodarfo.Properties.AutoHeight = False
    Me.opCodartBarcodeCodarfo.Properties.Caption = "Cod. articolo o Barcode o Cod. Art. C/F"
    Me.opCodartBarcodeCodarfo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCodartBarcodeCodarfo.Size = New System.Drawing.Size(218, 19)
    Me.opCodartBarcodeCodarfo.TabIndex = 118
    '
    'lbSeparatoriDate
    '
    Me.lbSeparatoriDate.AutoSize = True
    Me.lbSeparatoriDate.BackColor = System.Drawing.Color.Transparent
    Me.lbSeparatoriDate.Location = New System.Drawing.Point(391, 336)
    Me.lbSeparatoriDate.Name = "lbSeparatoriDate"
    Me.lbSeparatoriDate.NTSDbField = ""
    Me.lbSeparatoriDate.Size = New System.Drawing.Size(51, 13)
    Me.lbSeparatoriDate.TabIndex = 117
    Me.lbSeparatoriDate.Text = "Posizione"
    Me.lbSeparatoriDate.Tooltip = ""
    Me.lbSeparatoriDate.UseMnemonic = False
    '
    'lbDatconsA
    '
    Me.lbDatconsA.AutoSize = True
    Me.lbDatconsA.BackColor = System.Drawing.Color.Transparent
    Me.lbDatconsA.Location = New System.Drawing.Point(326, 336)
    Me.lbDatconsA.Name = "lbDatconsA"
    Me.lbDatconsA.NTSDbField = ""
    Me.lbDatconsA.Size = New System.Drawing.Size(14, 13)
    Me.lbDatconsA.TabIndex = 116
    Me.lbDatconsA.Text = "A"
    Me.lbDatconsA.Tooltip = ""
    Me.lbDatconsA.UseMnemonic = False
    '
    'lbDatconsDa
    '
    Me.lbDatconsDa.AutoSize = True
    Me.lbDatconsDa.BackColor = System.Drawing.Color.Transparent
    Me.lbDatconsDa.Location = New System.Drawing.Point(253, 336)
    Me.lbDatconsDa.Name = "lbDatconsDa"
    Me.lbDatconsDa.NTSDbField = ""
    Me.lbDatconsDa.Size = New System.Drawing.Size(20, 13)
    Me.lbDatconsDa.TabIndex = 115
    Me.lbDatconsDa.Text = "Da"
    Me.lbDatconsDa.Tooltip = ""
    Me.lbDatconsDa.UseMnemonic = False
    '
    'edDatconsA
    '
    Me.edDatconsA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatconsA.EditValue = "9999"
    Me.edDatconsA.Location = New System.Drawing.Point(346, 333)
    Me.edDatconsA.Name = "edDatconsA"
    Me.edDatconsA.NTSDbField = ""
    Me.edDatconsA.NTSFormat = "0"
    Me.edDatconsA.NTSForzaVisZoom = False
    Me.edDatconsA.NTSOldValue = "9999"
    Me.edDatconsA.Properties.Appearance.Options.UseTextOptions = True
    Me.edDatconsA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDatconsA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatconsA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatconsA.Properties.AutoHeight = False
    Me.edDatconsA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatconsA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatconsA.Size = New System.Drawing.Size(38, 20)
    Me.edDatconsA.TabIndex = 114
    '
    'edDatconsDa
    '
    Me.edDatconsDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatconsDa.EditValue = "1"
    Me.edDatconsDa.Location = New System.Drawing.Point(278, 333)
    Me.edDatconsDa.Name = "edDatconsDa"
    Me.edDatconsDa.NTSDbField = ""
    Me.edDatconsDa.NTSFormat = "0"
    Me.edDatconsDa.NTSForzaVisZoom = False
    Me.edDatconsDa.NTSOldValue = "1"
    Me.edDatconsDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edDatconsDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDatconsDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatconsDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatconsDa.Properties.AutoHeight = False
    Me.edDatconsDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatconsDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatconsDa.Size = New System.Drawing.Size(42, 20)
    Me.edDatconsDa.TabIndex = 113
    '
    'lbPosDatcons
    '
    Me.lbPosDatcons.AutoSize = True
    Me.lbPosDatcons.BackColor = System.Drawing.Color.Transparent
    Me.lbPosDatcons.Location = New System.Drawing.Point(155, 336)
    Me.lbPosDatcons.Name = "lbPosDatcons"
    Me.lbPosDatcons.NTSDbField = ""
    Me.lbPosDatcons.Size = New System.Drawing.Size(51, 13)
    Me.lbPosDatcons.TabIndex = 112
    Me.lbPosDatcons.Text = "Posizione"
    Me.lbPosDatcons.Tooltip = ""
    Me.lbPosDatcons.UseMnemonic = False
    '
    'edPosDatcons
    '
    Me.edPosDatcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosDatcons.EditValue = "1"
    Me.edPosDatcons.Enabled = False
    Me.edPosDatcons.Location = New System.Drawing.Point(210, 333)
    Me.edPosDatcons.Name = "edPosDatcons"
    Me.edPosDatcons.NTSDbField = ""
    Me.edPosDatcons.NTSFormat = "0"
    Me.edPosDatcons.NTSForzaVisZoom = False
    Me.edPosDatcons.NTSOldValue = "1"
    Me.edPosDatcons.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosDatcons.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosDatcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosDatcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosDatcons.Properties.AutoHeight = False
    Me.edPosDatcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosDatcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosDatcons.Size = New System.Drawing.Size(37, 20)
    Me.edPosDatcons.TabIndex = 111
    '
    'ckDatcons
    '
    Me.ckDatcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDatcons.Location = New System.Drawing.Point(20, 334)
    Me.ckDatcons.Name = "ckDatcons"
    Me.ckDatcons.NTSCheckValue = "S"
    Me.ckDatcons.NTSUnCheckValue = "N"
    Me.ckDatcons.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDatcons.Properties.Appearance.Options.UseBackColor = True
    Me.ckDatcons.Properties.AutoHeight = False
    Me.ckDatcons.Properties.Caption = "Data consegna"
    Me.ckDatcons.Size = New System.Drawing.Size(121, 19)
    Me.ckDatcons.TabIndex = 110
    '
    'lbDodici
    '
    Me.lbDodici.AutoSize = True
    Me.lbDodici.BackColor = System.Drawing.Color.Transparent
    Me.lbDodici.Location = New System.Drawing.Point(1, 336)
    Me.lbDodici.Name = "lbDodici"
    Me.lbDodici.NTSDbField = ""
    Me.lbDodici.Size = New System.Drawing.Size(23, 13)
    Me.lbDodici.TabIndex = 109
    Me.lbDodici.Text = "12."
    Me.lbDodici.Tooltip = ""
    Me.lbDodici.UseMnemonic = False
    '
    'lbDesintA
    '
    Me.lbDesintA.AutoSize = True
    Me.lbDesintA.BackColor = System.Drawing.Color.Transparent
    Me.lbDesintA.Location = New System.Drawing.Point(326, 134)
    Me.lbDesintA.Name = "lbDesintA"
    Me.lbDesintA.NTSDbField = ""
    Me.lbDesintA.Size = New System.Drawing.Size(14, 13)
    Me.lbDesintA.TabIndex = 108
    Me.lbDesintA.Text = "A"
    Me.lbDesintA.Tooltip = ""
    Me.lbDesintA.UseMnemonic = False
    '
    'lbDesintDa
    '
    Me.lbDesintDa.AutoSize = True
    Me.lbDesintDa.BackColor = System.Drawing.Color.Transparent
    Me.lbDesintDa.Location = New System.Drawing.Point(253, 134)
    Me.lbDesintDa.Name = "lbDesintDa"
    Me.lbDesintDa.NTSDbField = ""
    Me.lbDesintDa.Size = New System.Drawing.Size(20, 13)
    Me.lbDesintDa.TabIndex = 107
    Me.lbDesintDa.Text = "Da"
    Me.lbDesintDa.Tooltip = ""
    Me.lbDesintDa.UseMnemonic = False
    '
    'edDesintA
    '
    Me.edDesintA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDesintA.EditValue = "9999"
    Me.edDesintA.Location = New System.Drawing.Point(346, 131)
    Me.edDesintA.Name = "edDesintA"
    Me.edDesintA.NTSDbField = ""
    Me.edDesintA.NTSFormat = "0"
    Me.edDesintA.NTSForzaVisZoom = False
    Me.edDesintA.NTSOldValue = "9999"
    Me.edDesintA.Properties.Appearance.Options.UseTextOptions = True
    Me.edDesintA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDesintA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDesintA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDesintA.Properties.AutoHeight = False
    Me.edDesintA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDesintA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDesintA.Size = New System.Drawing.Size(38, 20)
    Me.edDesintA.TabIndex = 106
    '
    'edDesintDa
    '
    Me.edDesintDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDesintDa.EditValue = "1"
    Me.edDesintDa.Location = New System.Drawing.Point(278, 131)
    Me.edDesintDa.Name = "edDesintDa"
    Me.edDesintDa.NTSDbField = ""
    Me.edDesintDa.NTSFormat = "0"
    Me.edDesintDa.NTSForzaVisZoom = False
    Me.edDesintDa.NTSOldValue = "1"
    Me.edDesintDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edDesintDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDesintDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDesintDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDesintDa.Properties.AutoHeight = False
    Me.edDesintDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDesintDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDesintDa.Size = New System.Drawing.Size(42, 20)
    Me.edDesintDa.TabIndex = 105
    '
    'lbPosDesint
    '
    Me.lbPosDesint.AutoSize = True
    Me.lbPosDesint.BackColor = System.Drawing.Color.Transparent
    Me.lbPosDesint.Location = New System.Drawing.Point(155, 134)
    Me.lbPosDesint.Name = "lbPosDesint"
    Me.lbPosDesint.NTSDbField = ""
    Me.lbPosDesint.Size = New System.Drawing.Size(51, 13)
    Me.lbPosDesint.TabIndex = 104
    Me.lbPosDesint.Text = "Posizione"
    Me.lbPosDesint.Tooltip = ""
    Me.lbPosDesint.UseMnemonic = False
    '
    'edPosDesint
    '
    Me.edPosDesint.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosDesint.EditValue = "1"
    Me.edPosDesint.Enabled = False
    Me.edPosDesint.Location = New System.Drawing.Point(210, 131)
    Me.edPosDesint.Name = "edPosDesint"
    Me.edPosDesint.NTSDbField = ""
    Me.edPosDesint.NTSFormat = "0"
    Me.edPosDesint.NTSForzaVisZoom = False
    Me.edPosDesint.NTSOldValue = "1"
    Me.edPosDesint.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosDesint.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosDesint.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosDesint.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosDesint.Properties.AutoHeight = False
    Me.edPosDesint.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosDesint.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosDesint.Size = New System.Drawing.Size(37, 20)
    Me.edPosDesint.TabIndex = 103
    '
    'ckDesint
    '
    Me.ckDesint.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDesint.Location = New System.Drawing.Point(20, 132)
    Me.ckDesint.Name = "ckDesint"
    Me.ckDesint.NTSCheckValue = "S"
    Me.ckDesint.NTSUnCheckValue = "N"
    Me.ckDesint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDesint.Properties.Appearance.Options.UseBackColor = True
    Me.ckDesint.Properties.AutoHeight = False
    Me.ckDesint.Properties.Caption = "Descrizione interna"
    Me.ckDesint.Size = New System.Drawing.Size(121, 19)
    Me.ckDesint.TabIndex = 102
    '
    'lbTre
    '
    Me.lbTre.AutoSize = True
    Me.lbTre.BackColor = System.Drawing.Color.Transparent
    Me.lbTre.Location = New System.Drawing.Point(4, 134)
    Me.lbTre.Name = "lbTre"
    Me.lbTre.NTSDbField = ""
    Me.lbTre.Size = New System.Drawing.Size(17, 13)
    Me.lbTre.TabIndex = 101
    Me.lbTre.Text = "3."
    Me.lbTre.Tooltip = ""
    Me.lbTre.UseMnemonic = False
    '
    'lbDivSc6
    '
    Me.lbDivSc6.AutoSize = True
    Me.lbDivSc6.BackColor = System.Drawing.Color.Transparent
    Me.lbDivSc6.Location = New System.Drawing.Point(391, 314)
    Me.lbDivSc6.Name = "lbDivSc6"
    Me.lbDivSc6.NTSDbField = ""
    Me.lbDivSc6.Size = New System.Drawing.Size(51, 13)
    Me.lbDivSc6.TabIndex = 100
    Me.lbDivSc6.Text = "Dividi per"
    Me.lbDivSc6.Tooltip = ""
    Me.lbDivSc6.UseMnemonic = False
    '
    'edDivSc6
    '
    Me.edDivSc6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDivSc6.EditValue = "1"
    Me.edDivSc6.Enabled = False
    Me.edDivSc6.Location = New System.Drawing.Point(444, 311)
    Me.edDivSc6.Name = "edDivSc6"
    Me.edDivSc6.NTSDbField = ""
    Me.edDivSc6.NTSFormat = "0"
    Me.edDivSc6.NTSForzaVisZoom = False
    Me.edDivSc6.NTSOldValue = "1"
    Me.edDivSc6.Properties.Appearance.Options.UseTextOptions = True
    Me.edDivSc6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDivSc6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDivSc6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDivSc6.Properties.AutoHeight = False
    Me.edDivSc6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDivSc6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDivSc6.Size = New System.Drawing.Size(47, 20)
    Me.edDivSc6.TabIndex = 99
    '
    'lbSc6A
    '
    Me.lbSc6A.AutoSize = True
    Me.lbSc6A.BackColor = System.Drawing.Color.Transparent
    Me.lbSc6A.Location = New System.Drawing.Point(326, 314)
    Me.lbSc6A.Name = "lbSc6A"
    Me.lbSc6A.NTSDbField = ""
    Me.lbSc6A.Size = New System.Drawing.Size(14, 13)
    Me.lbSc6A.TabIndex = 98
    Me.lbSc6A.Text = "A"
    Me.lbSc6A.Tooltip = ""
    Me.lbSc6A.UseMnemonic = False
    '
    'lbSc6Da
    '
    Me.lbSc6Da.AutoSize = True
    Me.lbSc6Da.BackColor = System.Drawing.Color.Transparent
    Me.lbSc6Da.Location = New System.Drawing.Point(253, 314)
    Me.lbSc6Da.Name = "lbSc6Da"
    Me.lbSc6Da.NTSDbField = ""
    Me.lbSc6Da.Size = New System.Drawing.Size(20, 13)
    Me.lbSc6Da.TabIndex = 97
    Me.lbSc6Da.Text = "Da"
    Me.lbSc6Da.Tooltip = ""
    Me.lbSc6Da.UseMnemonic = False
    '
    'edSc6A
    '
    Me.edSc6A.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edSc6A.EditValue = "9999"
    Me.edSc6A.Enabled = False
    Me.edSc6A.Location = New System.Drawing.Point(346, 311)
    Me.edSc6A.Name = "edSc6A"
    Me.edSc6A.NTSDbField = ""
    Me.edSc6A.NTSFormat = "0"
    Me.edSc6A.NTSForzaVisZoom = False
    Me.edSc6A.NTSOldValue = "9999"
    Me.edSc6A.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc6A.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc6A.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc6A.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSc6A.Properties.AutoHeight = False
    Me.edSc6A.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSc6A.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSc6A.Size = New System.Drawing.Size(38, 20)
    Me.edSc6A.TabIndex = 96
    '
    'edSc6Da
    '
    Me.edSc6Da.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSc6Da.EditValue = "1"
    Me.edSc6Da.Enabled = False
    Me.edSc6Da.Location = New System.Drawing.Point(278, 311)
    Me.edSc6Da.Name = "edSc6Da"
    Me.edSc6Da.NTSDbField = ""
    Me.edSc6Da.NTSFormat = "0"
    Me.edSc6Da.NTSForzaVisZoom = False
    Me.edSc6Da.NTSOldValue = "1"
    Me.edSc6Da.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc6Da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc6Da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc6Da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSc6Da.Properties.AutoHeight = False
    Me.edSc6Da.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSc6Da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSc6Da.Size = New System.Drawing.Size(42, 20)
    Me.edSc6Da.TabIndex = 95
    '
    'lbPosSc6
    '
    Me.lbPosSc6.AutoSize = True
    Me.lbPosSc6.BackColor = System.Drawing.Color.Transparent
    Me.lbPosSc6.Location = New System.Drawing.Point(155, 314)
    Me.lbPosSc6.Name = "lbPosSc6"
    Me.lbPosSc6.NTSDbField = ""
    Me.lbPosSc6.Size = New System.Drawing.Size(51, 13)
    Me.lbPosSc6.TabIndex = 94
    Me.lbPosSc6.Text = "Posizione"
    Me.lbPosSc6.Tooltip = ""
    Me.lbPosSc6.UseMnemonic = False
    '
    'edPosSc6
    '
    Me.edPosSc6.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosSc6.EditValue = "1"
    Me.edPosSc6.Enabled = False
    Me.edPosSc6.Location = New System.Drawing.Point(210, 311)
    Me.edPosSc6.Name = "edPosSc6"
    Me.edPosSc6.NTSDbField = ""
    Me.edPosSc6.NTSFormat = "0"
    Me.edPosSc6.NTSForzaVisZoom = False
    Me.edPosSc6.NTSOldValue = "1"
    Me.edPosSc6.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosSc6.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosSc6.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosSc6.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosSc6.Properties.AutoHeight = False
    Me.edPosSc6.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosSc6.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosSc6.Size = New System.Drawing.Size(37, 20)
    Me.edPosSc6.TabIndex = 93
    '
    'ckSconto6
    '
    Me.ckSconto6.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconto6.Location = New System.Drawing.Point(20, 312)
    Me.ckSconto6.Name = "ckSconto6"
    Me.ckSconto6.NTSCheckValue = "S"
    Me.ckSconto6.NTSUnCheckValue = "N"
    Me.ckSconto6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconto6.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconto6.Properties.AutoHeight = False
    Me.ckSconto6.Properties.Caption = "Sconto 6"
    Me.ckSconto6.Size = New System.Drawing.Size(66, 19)
    Me.ckSconto6.TabIndex = 92
    '
    'lbUndici
    '
    Me.lbUndici.AutoSize = True
    Me.lbUndici.BackColor = System.Drawing.Color.Transparent
    Me.lbUndici.Location = New System.Drawing.Point(1, 314)
    Me.lbUndici.Name = "lbUndici"
    Me.lbUndici.NTSDbField = ""
    Me.lbUndici.Size = New System.Drawing.Size(23, 13)
    Me.lbUndici.TabIndex = 91
    Me.lbUndici.Text = "11."
    Me.lbUndici.Tooltip = ""
    Me.lbUndici.UseMnemonic = False
    '
    'lbDivSc5
    '
    Me.lbDivSc5.AutoSize = True
    Me.lbDivSc5.BackColor = System.Drawing.Color.Transparent
    Me.lbDivSc5.Location = New System.Drawing.Point(391, 292)
    Me.lbDivSc5.Name = "lbDivSc5"
    Me.lbDivSc5.NTSDbField = ""
    Me.lbDivSc5.Size = New System.Drawing.Size(51, 13)
    Me.lbDivSc5.TabIndex = 90
    Me.lbDivSc5.Text = "Dividi per"
    Me.lbDivSc5.Tooltip = ""
    Me.lbDivSc5.UseMnemonic = False
    '
    'edDivSc5
    '
    Me.edDivSc5.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDivSc5.EditValue = "1"
    Me.edDivSc5.Enabled = False
    Me.edDivSc5.Location = New System.Drawing.Point(444, 289)
    Me.edDivSc5.Name = "edDivSc5"
    Me.edDivSc5.NTSDbField = ""
    Me.edDivSc5.NTSFormat = "0"
    Me.edDivSc5.NTSForzaVisZoom = False
    Me.edDivSc5.NTSOldValue = "1"
    Me.edDivSc5.Properties.Appearance.Options.UseTextOptions = True
    Me.edDivSc5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDivSc5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDivSc5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDivSc5.Properties.AutoHeight = False
    Me.edDivSc5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDivSc5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDivSc5.Size = New System.Drawing.Size(47, 20)
    Me.edDivSc5.TabIndex = 89
    '
    'lbSc5A
    '
    Me.lbSc5A.AutoSize = True
    Me.lbSc5A.BackColor = System.Drawing.Color.Transparent
    Me.lbSc5A.Location = New System.Drawing.Point(326, 292)
    Me.lbSc5A.Name = "lbSc5A"
    Me.lbSc5A.NTSDbField = ""
    Me.lbSc5A.Size = New System.Drawing.Size(14, 13)
    Me.lbSc5A.TabIndex = 88
    Me.lbSc5A.Text = "A"
    Me.lbSc5A.Tooltip = ""
    Me.lbSc5A.UseMnemonic = False
    '
    'lbSc5Da
    '
    Me.lbSc5Da.AutoSize = True
    Me.lbSc5Da.BackColor = System.Drawing.Color.Transparent
    Me.lbSc5Da.Location = New System.Drawing.Point(253, 292)
    Me.lbSc5Da.Name = "lbSc5Da"
    Me.lbSc5Da.NTSDbField = ""
    Me.lbSc5Da.Size = New System.Drawing.Size(20, 13)
    Me.lbSc5Da.TabIndex = 87
    Me.lbSc5Da.Text = "Da"
    Me.lbSc5Da.Tooltip = ""
    Me.lbSc5Da.UseMnemonic = False
    '
    'edSc5A
    '
    Me.edSc5A.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSc5A.EditValue = "9999"
    Me.edSc5A.Enabled = False
    Me.edSc5A.Location = New System.Drawing.Point(346, 289)
    Me.edSc5A.Name = "edSc5A"
    Me.edSc5A.NTSDbField = ""
    Me.edSc5A.NTSFormat = "0"
    Me.edSc5A.NTSForzaVisZoom = False
    Me.edSc5A.NTSOldValue = "9999"
    Me.edSc5A.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc5A.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc5A.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc5A.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSc5A.Properties.AutoHeight = False
    Me.edSc5A.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSc5A.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSc5A.Size = New System.Drawing.Size(38, 20)
    Me.edSc5A.TabIndex = 86
    '
    'edSc5Da
    '
    Me.edSc5Da.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSc5Da.EditValue = "1"
    Me.edSc5Da.Enabled = False
    Me.edSc5Da.Location = New System.Drawing.Point(278, 289)
    Me.edSc5Da.Name = "edSc5Da"
    Me.edSc5Da.NTSDbField = ""
    Me.edSc5Da.NTSFormat = "0"
    Me.edSc5Da.NTSForzaVisZoom = False
    Me.edSc5Da.NTSOldValue = "1"
    Me.edSc5Da.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc5Da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc5Da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc5Da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSc5Da.Properties.AutoHeight = False
    Me.edSc5Da.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSc5Da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSc5Da.Size = New System.Drawing.Size(42, 20)
    Me.edSc5Da.TabIndex = 85
    '
    'lbPosSc5
    '
    Me.lbPosSc5.AutoSize = True
    Me.lbPosSc5.BackColor = System.Drawing.Color.Transparent
    Me.lbPosSc5.Location = New System.Drawing.Point(155, 292)
    Me.lbPosSc5.Name = "lbPosSc5"
    Me.lbPosSc5.NTSDbField = ""
    Me.lbPosSc5.Size = New System.Drawing.Size(51, 13)
    Me.lbPosSc5.TabIndex = 84
    Me.lbPosSc5.Text = "Posizione"
    Me.lbPosSc5.Tooltip = ""
    Me.lbPosSc5.UseMnemonic = False
    '
    'edPosSc5
    '
    Me.edPosSc5.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edPosSc5.EditValue = "1"
    Me.edPosSc5.Enabled = False
    Me.edPosSc5.Location = New System.Drawing.Point(210, 289)
    Me.edPosSc5.Name = "edPosSc5"
    Me.edPosSc5.NTSDbField = ""
    Me.edPosSc5.NTSFormat = "0"
    Me.edPosSc5.NTSForzaVisZoom = False
    Me.edPosSc5.NTSOldValue = "1"
    Me.edPosSc5.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosSc5.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosSc5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosSc5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosSc5.Properties.AutoHeight = False
    Me.edPosSc5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosSc5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosSc5.Size = New System.Drawing.Size(37, 20)
    Me.edPosSc5.TabIndex = 83
    '
    'ckSconto5
    '
    Me.ckSconto5.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconto5.Location = New System.Drawing.Point(20, 290)
    Me.ckSconto5.Name = "ckSconto5"
    Me.ckSconto5.NTSCheckValue = "S"
    Me.ckSconto5.NTSUnCheckValue = "N"
    Me.ckSconto5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconto5.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconto5.Properties.AutoHeight = False
    Me.ckSconto5.Properties.Caption = "Sconto 5"
    Me.ckSconto5.Size = New System.Drawing.Size(66, 19)
    Me.ckSconto5.TabIndex = 82
    '
    'lbDieci
    '
    Me.lbDieci.AutoSize = True
    Me.lbDieci.BackColor = System.Drawing.Color.Transparent
    Me.lbDieci.Location = New System.Drawing.Point(1, 292)
    Me.lbDieci.Name = "lbDieci"
    Me.lbDieci.NTSDbField = ""
    Me.lbDieci.Size = New System.Drawing.Size(23, 13)
    Me.lbDieci.TabIndex = 81
    Me.lbDieci.Text = "10."
    Me.lbDieci.Tooltip = ""
    Me.lbDieci.UseMnemonic = False
    '
    'lbDivSc4
    '
    Me.lbDivSc4.AutoSize = True
    Me.lbDivSc4.BackColor = System.Drawing.Color.Transparent
    Me.lbDivSc4.Location = New System.Drawing.Point(391, 270)
    Me.lbDivSc4.Name = "lbDivSc4"
    Me.lbDivSc4.NTSDbField = ""
    Me.lbDivSc4.Size = New System.Drawing.Size(51, 13)
    Me.lbDivSc4.TabIndex = 80
    Me.lbDivSc4.Text = "Dividi per"
    Me.lbDivSc4.Tooltip = ""
    Me.lbDivSc4.UseMnemonic = False
    '
    'edDivSc4
    '
    Me.edDivSc4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDivSc4.EditValue = "1"
    Me.edDivSc4.Enabled = False
    Me.edDivSc4.Location = New System.Drawing.Point(444, 267)
    Me.edDivSc4.Name = "edDivSc4"
    Me.edDivSc4.NTSDbField = ""
    Me.edDivSc4.NTSFormat = "0"
    Me.edDivSc4.NTSForzaVisZoom = False
    Me.edDivSc4.NTSOldValue = "1"
    Me.edDivSc4.Properties.Appearance.Options.UseTextOptions = True
    Me.edDivSc4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDivSc4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDivSc4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDivSc4.Properties.AutoHeight = False
    Me.edDivSc4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDivSc4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDivSc4.Size = New System.Drawing.Size(47, 20)
    Me.edDivSc4.TabIndex = 79
    '
    'lbSc4A
    '
    Me.lbSc4A.AutoSize = True
    Me.lbSc4A.BackColor = System.Drawing.Color.Transparent
    Me.lbSc4A.Location = New System.Drawing.Point(326, 270)
    Me.lbSc4A.Name = "lbSc4A"
    Me.lbSc4A.NTSDbField = ""
    Me.lbSc4A.Size = New System.Drawing.Size(14, 13)
    Me.lbSc4A.TabIndex = 78
    Me.lbSc4A.Text = "A"
    Me.lbSc4A.Tooltip = ""
    Me.lbSc4A.UseMnemonic = False
    '
    'lbSc4Da
    '
    Me.lbSc4Da.AutoSize = True
    Me.lbSc4Da.BackColor = System.Drawing.Color.Transparent
    Me.lbSc4Da.Location = New System.Drawing.Point(253, 270)
    Me.lbSc4Da.Name = "lbSc4Da"
    Me.lbSc4Da.NTSDbField = ""
    Me.lbSc4Da.Size = New System.Drawing.Size(20, 13)
    Me.lbSc4Da.TabIndex = 77
    Me.lbSc4Da.Text = "Da"
    Me.lbSc4Da.Tooltip = ""
    Me.lbSc4Da.UseMnemonic = False
    '
    'edSc4A
    '
    Me.edSc4A.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSc4A.EditValue = "9999"
    Me.edSc4A.Enabled = False
    Me.edSc4A.Location = New System.Drawing.Point(346, 267)
    Me.edSc4A.Name = "edSc4A"
    Me.edSc4A.NTSDbField = ""
    Me.edSc4A.NTSFormat = "0"
    Me.edSc4A.NTSForzaVisZoom = False
    Me.edSc4A.NTSOldValue = "9999"
    Me.edSc4A.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc4A.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc4A.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc4A.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSc4A.Properties.AutoHeight = False
    Me.edSc4A.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSc4A.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSc4A.Size = New System.Drawing.Size(38, 20)
    Me.edSc4A.TabIndex = 76
    '
    'edSc4Da
    '
    Me.edSc4Da.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSc4Da.EditValue = "1"
    Me.edSc4Da.Enabled = False
    Me.edSc4Da.Location = New System.Drawing.Point(278, 267)
    Me.edSc4Da.Name = "edSc4Da"
    Me.edSc4Da.NTSDbField = ""
    Me.edSc4Da.NTSFormat = "0"
    Me.edSc4Da.NTSForzaVisZoom = False
    Me.edSc4Da.NTSOldValue = "1"
    Me.edSc4Da.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc4Da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc4Da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc4Da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSc4Da.Properties.AutoHeight = False
    Me.edSc4Da.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSc4Da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSc4Da.Size = New System.Drawing.Size(42, 20)
    Me.edSc4Da.TabIndex = 75
    '
    'lbPosSc4
    '
    Me.lbPosSc4.AutoSize = True
    Me.lbPosSc4.BackColor = System.Drawing.Color.Transparent
    Me.lbPosSc4.Location = New System.Drawing.Point(155, 270)
    Me.lbPosSc4.Name = "lbPosSc4"
    Me.lbPosSc4.NTSDbField = ""
    Me.lbPosSc4.Size = New System.Drawing.Size(51, 13)
    Me.lbPosSc4.TabIndex = 74
    Me.lbPosSc4.Text = "Posizione"
    Me.lbPosSc4.Tooltip = ""
    Me.lbPosSc4.UseMnemonic = False
    '
    'edPosSc4
    '
    Me.edPosSc4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosSc4.EditValue = "1"
    Me.edPosSc4.Enabled = False
    Me.edPosSc4.Location = New System.Drawing.Point(210, 267)
    Me.edPosSc4.Name = "edPosSc4"
    Me.edPosSc4.NTSDbField = ""
    Me.edPosSc4.NTSFormat = "0"
    Me.edPosSc4.NTSForzaVisZoom = False
    Me.edPosSc4.NTSOldValue = "1"
    Me.edPosSc4.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosSc4.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosSc4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosSc4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosSc4.Properties.AutoHeight = False
    Me.edPosSc4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosSc4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosSc4.Size = New System.Drawing.Size(37, 20)
    Me.edPosSc4.TabIndex = 73
    '
    'ckSconto4
    '
    Me.ckSconto4.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckSconto4.Location = New System.Drawing.Point(20, 268)
    Me.ckSconto4.Name = "ckSconto4"
    Me.ckSconto4.NTSCheckValue = "S"
    Me.ckSconto4.NTSUnCheckValue = "N"
    Me.ckSconto4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconto4.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconto4.Properties.AutoHeight = False
    Me.ckSconto4.Properties.Caption = "Sconto 4"
    Me.ckSconto4.Size = New System.Drawing.Size(66, 19)
    Me.ckSconto4.TabIndex = 72
    '
    'lbNove
    '
    Me.lbNove.AutoSize = True
    Me.lbNove.BackColor = System.Drawing.Color.Transparent
    Me.lbNove.Location = New System.Drawing.Point(4, 270)
    Me.lbNove.Name = "lbNove"
    Me.lbNove.NTSDbField = ""
    Me.lbNove.Size = New System.Drawing.Size(17, 13)
    Me.lbNove.TabIndex = 71
    Me.lbNove.Text = "9."
    Me.lbNove.Tooltip = ""
    Me.lbNove.UseMnemonic = False
    '
    'lbDivSc3
    '
    Me.lbDivSc3.AutoSize = True
    Me.lbDivSc3.BackColor = System.Drawing.Color.Transparent
    Me.lbDivSc3.Location = New System.Drawing.Point(391, 247)
    Me.lbDivSc3.Name = "lbDivSc3"
    Me.lbDivSc3.NTSDbField = ""
    Me.lbDivSc3.Size = New System.Drawing.Size(51, 13)
    Me.lbDivSc3.TabIndex = 70
    Me.lbDivSc3.Text = "Dividi per"
    Me.lbDivSc3.Tooltip = ""
    Me.lbDivSc3.UseMnemonic = False
    '
    'edDivSc3
    '
    Me.edDivSc3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDivSc3.EditValue = "1"
    Me.edDivSc3.Enabled = False
    Me.edDivSc3.Location = New System.Drawing.Point(444, 244)
    Me.edDivSc3.Name = "edDivSc3"
    Me.edDivSc3.NTSDbField = ""
    Me.edDivSc3.NTSFormat = "0"
    Me.edDivSc3.NTSForzaVisZoom = False
    Me.edDivSc3.NTSOldValue = "1"
    Me.edDivSc3.Properties.Appearance.Options.UseTextOptions = True
    Me.edDivSc3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDivSc3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDivSc3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDivSc3.Properties.AutoHeight = False
    Me.edDivSc3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDivSc3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDivSc3.Size = New System.Drawing.Size(47, 20)
    Me.edDivSc3.TabIndex = 69
    '
    'lbSc3A
    '
    Me.lbSc3A.AutoSize = True
    Me.lbSc3A.BackColor = System.Drawing.Color.Transparent
    Me.lbSc3A.Location = New System.Drawing.Point(326, 247)
    Me.lbSc3A.Name = "lbSc3A"
    Me.lbSc3A.NTSDbField = ""
    Me.lbSc3A.Size = New System.Drawing.Size(14, 13)
    Me.lbSc3A.TabIndex = 68
    Me.lbSc3A.Text = "A"
    Me.lbSc3A.Tooltip = ""
    Me.lbSc3A.UseMnemonic = False
    '
    'lbSc3Da
    '
    Me.lbSc3Da.AutoSize = True
    Me.lbSc3Da.BackColor = System.Drawing.Color.Transparent
    Me.lbSc3Da.Location = New System.Drawing.Point(253, 247)
    Me.lbSc3Da.Name = "lbSc3Da"
    Me.lbSc3Da.NTSDbField = ""
    Me.lbSc3Da.Size = New System.Drawing.Size(20, 13)
    Me.lbSc3Da.TabIndex = 67
    Me.lbSc3Da.Text = "Da"
    Me.lbSc3Da.Tooltip = ""
    Me.lbSc3Da.UseMnemonic = False
    '
    'edSc3A
    '
    Me.edSc3A.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSc3A.EditValue = "9999"
    Me.edSc3A.Enabled = False
    Me.edSc3A.Location = New System.Drawing.Point(346, 244)
    Me.edSc3A.Name = "edSc3A"
    Me.edSc3A.NTSDbField = ""
    Me.edSc3A.NTSFormat = "0"
    Me.edSc3A.NTSForzaVisZoom = False
    Me.edSc3A.NTSOldValue = "9999"
    Me.edSc3A.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc3A.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc3A.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc3A.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSc3A.Properties.AutoHeight = False
    Me.edSc3A.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSc3A.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSc3A.Size = New System.Drawing.Size(38, 20)
    Me.edSc3A.TabIndex = 66
    '
    'edSc3Da
    '
    Me.edSc3Da.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSc3Da.EditValue = "1"
    Me.edSc3Da.Enabled = False
    Me.edSc3Da.Location = New System.Drawing.Point(278, 244)
    Me.edSc3Da.Name = "edSc3Da"
    Me.edSc3Da.NTSDbField = ""
    Me.edSc3Da.NTSFormat = "0"
    Me.edSc3Da.NTSForzaVisZoom = False
    Me.edSc3Da.NTSOldValue = "1"
    Me.edSc3Da.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc3Da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc3Da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc3Da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSc3Da.Properties.AutoHeight = False
    Me.edSc3Da.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSc3Da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSc3Da.Size = New System.Drawing.Size(42, 20)
    Me.edSc3Da.TabIndex = 65
    '
    'lbPosSc3
    '
    Me.lbPosSc3.AutoSize = True
    Me.lbPosSc3.BackColor = System.Drawing.Color.Transparent
    Me.lbPosSc3.Location = New System.Drawing.Point(155, 247)
    Me.lbPosSc3.Name = "lbPosSc3"
    Me.lbPosSc3.NTSDbField = ""
    Me.lbPosSc3.Size = New System.Drawing.Size(51, 13)
    Me.lbPosSc3.TabIndex = 64
    Me.lbPosSc3.Text = "Posizione"
    Me.lbPosSc3.Tooltip = ""
    Me.lbPosSc3.UseMnemonic = False
    '
    'edPosSc3
    '
    Me.edPosSc3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosSc3.EditValue = "1"
    Me.edPosSc3.Enabled = False
    Me.edPosSc3.Location = New System.Drawing.Point(210, 244)
    Me.edPosSc3.Name = "edPosSc3"
    Me.edPosSc3.NTSDbField = ""
    Me.edPosSc3.NTSFormat = "0"
    Me.edPosSc3.NTSForzaVisZoom = False
    Me.edPosSc3.NTSOldValue = "1"
    Me.edPosSc3.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosSc3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosSc3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosSc3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosSc3.Properties.AutoHeight = False
    Me.edPosSc3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosSc3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosSc3.Size = New System.Drawing.Size(37, 20)
    Me.edPosSc3.TabIndex = 63
    '
    'ckSconto3
    '
    Me.ckSconto3.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconto3.Location = New System.Drawing.Point(20, 245)
    Me.ckSconto3.Name = "ckSconto3"
    Me.ckSconto3.NTSCheckValue = "S"
    Me.ckSconto3.NTSUnCheckValue = "N"
    Me.ckSconto3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconto3.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconto3.Properties.AutoHeight = False
    Me.ckSconto3.Properties.Caption = "Sconto 3"
    Me.ckSconto3.Size = New System.Drawing.Size(66, 19)
    Me.ckSconto3.TabIndex = 62
    '
    'lbOtto
    '
    Me.lbOtto.AutoSize = True
    Me.lbOtto.BackColor = System.Drawing.Color.Transparent
    Me.lbOtto.Location = New System.Drawing.Point(4, 247)
    Me.lbOtto.Name = "lbOtto"
    Me.lbOtto.NTSDbField = ""
    Me.lbOtto.Size = New System.Drawing.Size(17, 13)
    Me.lbOtto.TabIndex = 61
    Me.lbOtto.Text = "8."
    Me.lbOtto.Tooltip = ""
    Me.lbOtto.UseMnemonic = False
    '
    'lbDivSc2
    '
    Me.lbDivSc2.AutoSize = True
    Me.lbDivSc2.BackColor = System.Drawing.Color.Transparent
    Me.lbDivSc2.Location = New System.Drawing.Point(391, 225)
    Me.lbDivSc2.Name = "lbDivSc2"
    Me.lbDivSc2.NTSDbField = ""
    Me.lbDivSc2.Size = New System.Drawing.Size(51, 13)
    Me.lbDivSc2.TabIndex = 60
    Me.lbDivSc2.Text = "Dividi per"
    Me.lbDivSc2.Tooltip = ""
    Me.lbDivSc2.UseMnemonic = False
    '
    'edDivSc2
    '
    Me.edDivSc2.AllowDrop = True
    Me.edDivSc2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDivSc2.EditValue = "1"
    Me.edDivSc2.Enabled = False
    Me.edDivSc2.Location = New System.Drawing.Point(444, 222)
    Me.edDivSc2.Name = "edDivSc2"
    Me.edDivSc2.NTSDbField = ""
    Me.edDivSc2.NTSFormat = "0"
    Me.edDivSc2.NTSForzaVisZoom = False
    Me.edDivSc2.NTSOldValue = "1"
    Me.edDivSc2.Properties.Appearance.Options.UseTextOptions = True
    Me.edDivSc2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDivSc2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDivSc2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDivSc2.Properties.AutoHeight = False
    Me.edDivSc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDivSc2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDivSc2.Size = New System.Drawing.Size(47, 20)
    Me.edDivSc2.TabIndex = 59
    '
    'lbSc2A
    '
    Me.lbSc2A.AutoSize = True
    Me.lbSc2A.BackColor = System.Drawing.Color.Transparent
    Me.lbSc2A.Location = New System.Drawing.Point(326, 225)
    Me.lbSc2A.Name = "lbSc2A"
    Me.lbSc2A.NTSDbField = ""
    Me.lbSc2A.Size = New System.Drawing.Size(14, 13)
    Me.lbSc2A.TabIndex = 58
    Me.lbSc2A.Text = "A"
    Me.lbSc2A.Tooltip = ""
    Me.lbSc2A.UseMnemonic = False
    '
    'lbSc2Da
    '
    Me.lbSc2Da.AutoSize = True
    Me.lbSc2Da.BackColor = System.Drawing.Color.Transparent
    Me.lbSc2Da.Location = New System.Drawing.Point(253, 225)
    Me.lbSc2Da.Name = "lbSc2Da"
    Me.lbSc2Da.NTSDbField = ""
    Me.lbSc2Da.Size = New System.Drawing.Size(20, 13)
    Me.lbSc2Da.TabIndex = 57
    Me.lbSc2Da.Text = "Da"
    Me.lbSc2Da.Tooltip = ""
    Me.lbSc2Da.UseMnemonic = False
    '
    'edSc2A
    '
    Me.edSc2A.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSc2A.EditValue = "9999"
    Me.edSc2A.Enabled = False
    Me.edSc2A.Location = New System.Drawing.Point(346, 222)
    Me.edSc2A.Name = "edSc2A"
    Me.edSc2A.NTSDbField = ""
    Me.edSc2A.NTSFormat = "0"
    Me.edSc2A.NTSForzaVisZoom = False
    Me.edSc2A.NTSOldValue = "9999"
    Me.edSc2A.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc2A.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc2A.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc2A.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSc2A.Properties.AutoHeight = False
    Me.edSc2A.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSc2A.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSc2A.Size = New System.Drawing.Size(38, 20)
    Me.edSc2A.TabIndex = 56
    '
    'edSc2Da
    '
    Me.edSc2Da.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSc2Da.EditValue = "1"
    Me.edSc2Da.Enabled = False
    Me.edSc2Da.Location = New System.Drawing.Point(278, 222)
    Me.edSc2Da.Name = "edSc2Da"
    Me.edSc2Da.NTSDbField = ""
    Me.edSc2Da.NTSFormat = "0"
    Me.edSc2Da.NTSForzaVisZoom = False
    Me.edSc2Da.NTSOldValue = "1"
    Me.edSc2Da.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc2Da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc2Da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc2Da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSc2Da.Properties.AutoHeight = False
    Me.edSc2Da.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSc2Da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSc2Da.Size = New System.Drawing.Size(42, 20)
    Me.edSc2Da.TabIndex = 55
    '
    'lbPosSc2
    '
    Me.lbPosSc2.AutoSize = True
    Me.lbPosSc2.BackColor = System.Drawing.Color.Transparent
    Me.lbPosSc2.Location = New System.Drawing.Point(155, 225)
    Me.lbPosSc2.Name = "lbPosSc2"
    Me.lbPosSc2.NTSDbField = ""
    Me.lbPosSc2.Size = New System.Drawing.Size(51, 13)
    Me.lbPosSc2.TabIndex = 54
    Me.lbPosSc2.Text = "Posizione"
    Me.lbPosSc2.Tooltip = ""
    Me.lbPosSc2.UseMnemonic = False
    '
    'edPosSc2
    '
    Me.edPosSc2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosSc2.EditValue = "1"
    Me.edPosSc2.Enabled = False
    Me.edPosSc2.Location = New System.Drawing.Point(210, 222)
    Me.edPosSc2.Name = "edPosSc2"
    Me.edPosSc2.NTSDbField = ""
    Me.edPosSc2.NTSFormat = "0"
    Me.edPosSc2.NTSForzaVisZoom = False
    Me.edPosSc2.NTSOldValue = "1"
    Me.edPosSc2.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosSc2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosSc2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosSc2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosSc2.Properties.AutoHeight = False
    Me.edPosSc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosSc2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosSc2.Size = New System.Drawing.Size(37, 20)
    Me.edPosSc2.TabIndex = 53
    '
    'ckSconto2
    '
    Me.ckSconto2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconto2.Location = New System.Drawing.Point(20, 223)
    Me.ckSconto2.Name = "ckSconto2"
    Me.ckSconto2.NTSCheckValue = "S"
    Me.ckSconto2.NTSUnCheckValue = "N"
    Me.ckSconto2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconto2.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconto2.Properties.AutoHeight = False
    Me.ckSconto2.Properties.Caption = "Sconto 2"
    Me.ckSconto2.Size = New System.Drawing.Size(66, 19)
    Me.ckSconto2.TabIndex = 52
    '
    'lbSette
    '
    Me.lbSette.AutoSize = True
    Me.lbSette.BackColor = System.Drawing.Color.Transparent
    Me.lbSette.Location = New System.Drawing.Point(4, 225)
    Me.lbSette.Name = "lbSette"
    Me.lbSette.NTSDbField = ""
    Me.lbSette.Size = New System.Drawing.Size(17, 13)
    Me.lbSette.TabIndex = 51
    Me.lbSette.Text = "7."
    Me.lbSette.Tooltip = ""
    Me.lbSette.UseMnemonic = False
    '
    'lbDivSc1
    '
    Me.lbDivSc1.AutoSize = True
    Me.lbDivSc1.BackColor = System.Drawing.Color.Transparent
    Me.lbDivSc1.Location = New System.Drawing.Point(391, 202)
    Me.lbDivSc1.Name = "lbDivSc1"
    Me.lbDivSc1.NTSDbField = ""
    Me.lbDivSc1.Size = New System.Drawing.Size(51, 13)
    Me.lbDivSc1.TabIndex = 50
    Me.lbDivSc1.Text = "Dividi per"
    Me.lbDivSc1.Tooltip = ""
    Me.lbDivSc1.UseMnemonic = False
    '
    'edDivSc1
    '
    Me.edDivSc1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDivSc1.EditValue = "1"
    Me.edDivSc1.Enabled = False
    Me.edDivSc1.Location = New System.Drawing.Point(444, 199)
    Me.edDivSc1.Name = "edDivSc1"
    Me.edDivSc1.NTSDbField = ""
    Me.edDivSc1.NTSFormat = "0"
    Me.edDivSc1.NTSForzaVisZoom = False
    Me.edDivSc1.NTSOldValue = "1"
    Me.edDivSc1.Properties.Appearance.Options.UseTextOptions = True
    Me.edDivSc1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDivSc1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDivSc1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDivSc1.Properties.AutoHeight = False
    Me.edDivSc1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDivSc1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDivSc1.Size = New System.Drawing.Size(47, 20)
    Me.edDivSc1.TabIndex = 49
    '
    'lbSc1A
    '
    Me.lbSc1A.AutoSize = True
    Me.lbSc1A.BackColor = System.Drawing.Color.Transparent
    Me.lbSc1A.Location = New System.Drawing.Point(326, 202)
    Me.lbSc1A.Name = "lbSc1A"
    Me.lbSc1A.NTSDbField = ""
    Me.lbSc1A.Size = New System.Drawing.Size(14, 13)
    Me.lbSc1A.TabIndex = 48
    Me.lbSc1A.Text = "A"
    Me.lbSc1A.Tooltip = ""
    Me.lbSc1A.UseMnemonic = False
    '
    'lbSc1Da
    '
    Me.lbSc1Da.AutoSize = True
    Me.lbSc1Da.BackColor = System.Drawing.Color.Transparent
    Me.lbSc1Da.Location = New System.Drawing.Point(253, 202)
    Me.lbSc1Da.Name = "lbSc1Da"
    Me.lbSc1Da.NTSDbField = ""
    Me.lbSc1Da.Size = New System.Drawing.Size(20, 13)
    Me.lbSc1Da.TabIndex = 47
    Me.lbSc1Da.Text = "Da"
    Me.lbSc1Da.Tooltip = ""
    Me.lbSc1Da.UseMnemonic = False
    '
    'edSc1A
    '
    Me.edSc1A.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSc1A.EditValue = "9999"
    Me.edSc1A.Enabled = False
    Me.edSc1A.Location = New System.Drawing.Point(346, 199)
    Me.edSc1A.Name = "edSc1A"
    Me.edSc1A.NTSDbField = ""
    Me.edSc1A.NTSFormat = "0"
    Me.edSc1A.NTSForzaVisZoom = False
    Me.edSc1A.NTSOldValue = "9999"
    Me.edSc1A.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc1A.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc1A.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc1A.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSc1A.Properties.AutoHeight = False
    Me.edSc1A.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSc1A.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSc1A.Size = New System.Drawing.Size(38, 20)
    Me.edSc1A.TabIndex = 46
    '
    'edSc1Da
    '
    Me.edSc1Da.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSc1Da.EditValue = "1"
    Me.edSc1Da.Enabled = False
    Me.edSc1Da.Location = New System.Drawing.Point(278, 199)
    Me.edSc1Da.Name = "edSc1Da"
    Me.edSc1Da.NTSDbField = ""
    Me.edSc1Da.NTSFormat = "0"
    Me.edSc1Da.NTSForzaVisZoom = False
    Me.edSc1Da.NTSOldValue = "1"
    Me.edSc1Da.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc1Da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc1Da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc1Da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSc1Da.Properties.AutoHeight = False
    Me.edSc1Da.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSc1Da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSc1Da.Size = New System.Drawing.Size(42, 20)
    Me.edSc1Da.TabIndex = 45
    '
    'lbPosSc1
    '
    Me.lbPosSc1.AutoSize = True
    Me.lbPosSc1.BackColor = System.Drawing.Color.Transparent
    Me.lbPosSc1.Location = New System.Drawing.Point(155, 202)
    Me.lbPosSc1.Name = "lbPosSc1"
    Me.lbPosSc1.NTSDbField = ""
    Me.lbPosSc1.Size = New System.Drawing.Size(51, 13)
    Me.lbPosSc1.TabIndex = 44
    Me.lbPosSc1.Text = "Posizione"
    Me.lbPosSc1.Tooltip = ""
    Me.lbPosSc1.UseMnemonic = False
    '
    'edPosSc1
    '
    Me.edPosSc1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosSc1.EditValue = "1"
    Me.edPosSc1.Enabled = False
    Me.edPosSc1.Location = New System.Drawing.Point(210, 199)
    Me.edPosSc1.Name = "edPosSc1"
    Me.edPosSc1.NTSDbField = ""
    Me.edPosSc1.NTSFormat = "0"
    Me.edPosSc1.NTSForzaVisZoom = False
    Me.edPosSc1.NTSOldValue = "1"
    Me.edPosSc1.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosSc1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosSc1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosSc1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosSc1.Properties.AutoHeight = False
    Me.edPosSc1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosSc1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosSc1.Size = New System.Drawing.Size(37, 20)
    Me.edPosSc1.TabIndex = 43
    '
    'ckSconto1
    '
    Me.ckSconto1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconto1.Location = New System.Drawing.Point(20, 200)
    Me.ckSconto1.Name = "ckSconto1"
    Me.ckSconto1.NTSCheckValue = "S"
    Me.ckSconto1.NTSUnCheckValue = "N"
    Me.ckSconto1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconto1.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconto1.Properties.AutoHeight = False
    Me.ckSconto1.Properties.Caption = "Sconto 1"
    Me.ckSconto1.Size = New System.Drawing.Size(66, 19)
    Me.ckSconto1.TabIndex = 42
    '
    'lbSei
    '
    Me.lbSei.AutoSize = True
    Me.lbSei.BackColor = System.Drawing.Color.Transparent
    Me.lbSei.Location = New System.Drawing.Point(4, 202)
    Me.lbSei.Name = "lbSei"
    Me.lbSei.NTSDbField = ""
    Me.lbSei.Size = New System.Drawing.Size(17, 13)
    Me.lbSei.TabIndex = 41
    Me.lbSei.Text = "6."
    Me.lbSei.Tooltip = ""
    Me.lbSei.UseMnemonic = False
    '
    'lbDivPrezzo
    '
    Me.lbDivPrezzo.AutoSize = True
    Me.lbDivPrezzo.BackColor = System.Drawing.Color.Transparent
    Me.lbDivPrezzo.Location = New System.Drawing.Point(391, 179)
    Me.lbDivPrezzo.Name = "lbDivPrezzo"
    Me.lbDivPrezzo.NTSDbField = ""
    Me.lbDivPrezzo.Size = New System.Drawing.Size(51, 13)
    Me.lbDivPrezzo.TabIndex = 40
    Me.lbDivPrezzo.Text = "Dividi per"
    Me.lbDivPrezzo.Tooltip = ""
    Me.lbDivPrezzo.UseMnemonic = False
    '
    'lbDivQuant
    '
    Me.lbDivQuant.AutoSize = True
    Me.lbDivQuant.BackColor = System.Drawing.Color.Transparent
    Me.lbDivQuant.Location = New System.Drawing.Point(391, 156)
    Me.lbDivQuant.Name = "lbDivQuant"
    Me.lbDivQuant.NTSDbField = ""
    Me.lbDivQuant.Size = New System.Drawing.Size(51, 13)
    Me.lbDivQuant.TabIndex = 39
    Me.lbDivQuant.Text = "Dividi per"
    Me.lbDivQuant.Tooltip = ""
    Me.lbDivQuant.UseMnemonic = False
    '
    'edDivPrezzo
    '
    Me.edDivPrezzo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDivPrezzo.EditValue = "1"
    Me.edDivPrezzo.Enabled = False
    Me.edDivPrezzo.Location = New System.Drawing.Point(444, 176)
    Me.edDivPrezzo.Name = "edDivPrezzo"
    Me.edDivPrezzo.NTSDbField = ""
    Me.edDivPrezzo.NTSFormat = "0"
    Me.edDivPrezzo.NTSForzaVisZoom = False
    Me.edDivPrezzo.NTSOldValue = "1"
    Me.edDivPrezzo.Properties.Appearance.Options.UseTextOptions = True
    Me.edDivPrezzo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDivPrezzo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDivPrezzo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDivPrezzo.Properties.AutoHeight = False
    Me.edDivPrezzo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDivPrezzo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDivPrezzo.Size = New System.Drawing.Size(47, 20)
    Me.edDivPrezzo.TabIndex = 38
    '
    'edDivQuant
    '
    Me.edDivQuant.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDivQuant.EditValue = "1"
    Me.edDivQuant.Location = New System.Drawing.Point(444, 153)
    Me.edDivQuant.Name = "edDivQuant"
    Me.edDivQuant.NTSDbField = ""
    Me.edDivQuant.NTSFormat = "0"
    Me.edDivQuant.NTSForzaVisZoom = False
    Me.edDivQuant.NTSOldValue = "1"
    Me.edDivQuant.Properties.Appearance.Options.UseTextOptions = True
    Me.edDivQuant.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDivQuant.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDivQuant.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDivQuant.Properties.AutoHeight = False
    Me.edDivQuant.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDivQuant.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDivQuant.Size = New System.Drawing.Size(47, 20)
    Me.edDivQuant.TabIndex = 37
    '
    'lbPreA
    '
    Me.lbPreA.AutoSize = True
    Me.lbPreA.BackColor = System.Drawing.Color.Transparent
    Me.lbPreA.Location = New System.Drawing.Point(326, 179)
    Me.lbPreA.Name = "lbPreA"
    Me.lbPreA.NTSDbField = ""
    Me.lbPreA.Size = New System.Drawing.Size(14, 13)
    Me.lbPreA.TabIndex = 36
    Me.lbPreA.Text = "A"
    Me.lbPreA.Tooltip = ""
    Me.lbPreA.UseMnemonic = False
    '
    'lbPreDa
    '
    Me.lbPreDa.AutoSize = True
    Me.lbPreDa.BackColor = System.Drawing.Color.Transparent
    Me.lbPreDa.Location = New System.Drawing.Point(253, 179)
    Me.lbPreDa.Name = "lbPreDa"
    Me.lbPreDa.NTSDbField = ""
    Me.lbPreDa.Size = New System.Drawing.Size(20, 13)
    Me.lbPreDa.TabIndex = 35
    Me.lbPreDa.Text = "Da"
    Me.lbPreDa.Tooltip = ""
    Me.lbPreDa.UseMnemonic = False
    '
    'edPreA
    '
    Me.edPreA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPreA.EditValue = "9999"
    Me.edPreA.Enabled = False
    Me.edPreA.Location = New System.Drawing.Point(346, 176)
    Me.edPreA.Name = "edPreA"
    Me.edPreA.NTSDbField = ""
    Me.edPreA.NTSFormat = "0"
    Me.edPreA.NTSForzaVisZoom = False
    Me.edPreA.NTSOldValue = "9999"
    Me.edPreA.Properties.Appearance.Options.UseTextOptions = True
    Me.edPreA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPreA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPreA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPreA.Properties.AutoHeight = False
    Me.edPreA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPreA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPreA.Size = New System.Drawing.Size(38, 20)
    Me.edPreA.TabIndex = 34
    '
    'edPreDa
    '
    Me.edPreDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPreDa.EditValue = "1"
    Me.edPreDa.Enabled = False
    Me.edPreDa.Location = New System.Drawing.Point(278, 176)
    Me.edPreDa.Name = "edPreDa"
    Me.edPreDa.NTSDbField = ""
    Me.edPreDa.NTSFormat = "0"
    Me.edPreDa.NTSForzaVisZoom = False
    Me.edPreDa.NTSOldValue = "1"
    Me.edPreDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edPreDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPreDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPreDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPreDa.Properties.AutoHeight = False
    Me.edPreDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPreDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPreDa.Size = New System.Drawing.Size(42, 20)
    Me.edPreDa.TabIndex = 33
    '
    'lbQuantA
    '
    Me.lbQuantA.AutoSize = True
    Me.lbQuantA.BackColor = System.Drawing.Color.Transparent
    Me.lbQuantA.Location = New System.Drawing.Point(326, 156)
    Me.lbQuantA.Name = "lbQuantA"
    Me.lbQuantA.NTSDbField = ""
    Me.lbQuantA.Size = New System.Drawing.Size(14, 13)
    Me.lbQuantA.TabIndex = 32
    Me.lbQuantA.Text = "A"
    Me.lbQuantA.Tooltip = ""
    Me.lbQuantA.UseMnemonic = False
    '
    'lbQuantDa
    '
    Me.lbQuantDa.AutoSize = True
    Me.lbQuantDa.BackColor = System.Drawing.Color.Transparent
    Me.lbQuantDa.Location = New System.Drawing.Point(253, 156)
    Me.lbQuantDa.Name = "lbQuantDa"
    Me.lbQuantDa.NTSDbField = ""
    Me.lbQuantDa.Size = New System.Drawing.Size(20, 13)
    Me.lbQuantDa.TabIndex = 31
    Me.lbQuantDa.Text = "Da"
    Me.lbQuantDa.Tooltip = ""
    Me.lbQuantDa.UseMnemonic = False
    '
    'edQuantA
    '
    Me.edQuantA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQuantA.EditValue = "9999"
    Me.edQuantA.Location = New System.Drawing.Point(346, 153)
    Me.edQuantA.Name = "edQuantA"
    Me.edQuantA.NTSDbField = ""
    Me.edQuantA.NTSFormat = "0"
    Me.edQuantA.NTSForzaVisZoom = False
    Me.edQuantA.NTSOldValue = "9999"
    Me.edQuantA.Properties.Appearance.Options.UseTextOptions = True
    Me.edQuantA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQuantA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQuantA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQuantA.Properties.AutoHeight = False
    Me.edQuantA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQuantA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQuantA.Size = New System.Drawing.Size(38, 20)
    Me.edQuantA.TabIndex = 30
    '
    'edQuantDa
    '
    Me.edQuantDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQuantDa.EditValue = "1"
    Me.edQuantDa.Location = New System.Drawing.Point(278, 153)
    Me.edQuantDa.Name = "edQuantDa"
    Me.edQuantDa.NTSDbField = ""
    Me.edQuantDa.NTSFormat = "0"
    Me.edQuantDa.NTSForzaVisZoom = False
    Me.edQuantDa.NTSOldValue = "1"
    Me.edQuantDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edQuantDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQuantDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQuantDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQuantDa.Properties.AutoHeight = False
    Me.edQuantDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQuantDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQuantDa.Size = New System.Drawing.Size(42, 20)
    Me.edQuantDa.TabIndex = 29
    '
    'lbDescrA
    '
    Me.lbDescrA.AutoSize = True
    Me.lbDescrA.BackColor = System.Drawing.Color.Transparent
    Me.lbDescrA.Location = New System.Drawing.Point(326, 111)
    Me.lbDescrA.Name = "lbDescrA"
    Me.lbDescrA.NTSDbField = ""
    Me.lbDescrA.Size = New System.Drawing.Size(14, 13)
    Me.lbDescrA.TabIndex = 28
    Me.lbDescrA.Text = "A"
    Me.lbDescrA.Tooltip = ""
    Me.lbDescrA.UseMnemonic = False
    '
    'lbDescrDa
    '
    Me.lbDescrDa.AutoSize = True
    Me.lbDescrDa.BackColor = System.Drawing.Color.Transparent
    Me.lbDescrDa.Location = New System.Drawing.Point(253, 111)
    Me.lbDescrDa.Name = "lbDescrDa"
    Me.lbDescrDa.NTSDbField = ""
    Me.lbDescrDa.Size = New System.Drawing.Size(20, 13)
    Me.lbDescrDa.TabIndex = 27
    Me.lbDescrDa.Text = "Da"
    Me.lbDescrDa.Tooltip = ""
    Me.lbDescrDa.UseMnemonic = False
    '
    'edDescrA
    '
    Me.edDescrA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDescrA.EditValue = "9999"
    Me.edDescrA.Location = New System.Drawing.Point(346, 108)
    Me.edDescrA.Name = "edDescrA"
    Me.edDescrA.NTSDbField = ""
    Me.edDescrA.NTSFormat = "0"
    Me.edDescrA.NTSForzaVisZoom = False
    Me.edDescrA.NTSOldValue = "9999"
    Me.edDescrA.Properties.Appearance.Options.UseTextOptions = True
    Me.edDescrA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDescrA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescrA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescrA.Properties.AutoHeight = False
    Me.edDescrA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescrA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescrA.Size = New System.Drawing.Size(38, 20)
    Me.edDescrA.TabIndex = 26
    '
    'edDescrDa
    '
    Me.edDescrDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDescrDa.EditValue = "1"
    Me.edDescrDa.Location = New System.Drawing.Point(278, 108)
    Me.edDescrDa.Name = "edDescrDa"
    Me.edDescrDa.NTSDbField = ""
    Me.edDescrDa.NTSFormat = "0"
    Me.edDescrDa.NTSForzaVisZoom = False
    Me.edDescrDa.NTSOldValue = "1"
    Me.edDescrDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edDescrDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDescrDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescrDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescrDa.Properties.AutoHeight = False
    Me.edDescrDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescrDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescrDa.Size = New System.Drawing.Size(42, 20)
    Me.edDescrDa.TabIndex = 25
    '
    'lbArtA
    '
    Me.lbArtA.AutoSize = True
    Me.lbArtA.BackColor = System.Drawing.Color.Transparent
    Me.lbArtA.Location = New System.Drawing.Point(326, 89)
    Me.lbArtA.Name = "lbArtA"
    Me.lbArtA.NTSDbField = ""
    Me.lbArtA.Size = New System.Drawing.Size(14, 13)
    Me.lbArtA.TabIndex = 24
    Me.lbArtA.Text = "A"
    Me.lbArtA.Tooltip = ""
    Me.lbArtA.UseMnemonic = False
    '
    'lbArtDa
    '
    Me.lbArtDa.AutoSize = True
    Me.lbArtDa.BackColor = System.Drawing.Color.Transparent
    Me.lbArtDa.Location = New System.Drawing.Point(253, 89)
    Me.lbArtDa.Name = "lbArtDa"
    Me.lbArtDa.NTSDbField = ""
    Me.lbArtDa.Size = New System.Drawing.Size(20, 13)
    Me.lbArtDa.TabIndex = 23
    Me.lbArtDa.Text = "Da"
    Me.lbArtDa.Tooltip = ""
    Me.lbArtDa.UseMnemonic = False
    '
    'edArtA
    '
    Me.edArtA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArtA.EditValue = "9999"
    Me.edArtA.Location = New System.Drawing.Point(346, 85)
    Me.edArtA.Name = "edArtA"
    Me.edArtA.NTSDbField = ""
    Me.edArtA.NTSFormat = "0"
    Me.edArtA.NTSForzaVisZoom = False
    Me.edArtA.NTSOldValue = "9999"
    Me.edArtA.Properties.Appearance.Options.UseTextOptions = True
    Me.edArtA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edArtA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArtA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArtA.Properties.AutoHeight = False
    Me.edArtA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArtA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArtA.Size = New System.Drawing.Size(38, 20)
    Me.edArtA.TabIndex = 22
    '
    'edArtDa
    '
    Me.edArtDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArtDa.EditValue = "1"
    Me.edArtDa.Location = New System.Drawing.Point(278, 86)
    Me.edArtDa.Name = "edArtDa"
    Me.edArtDa.NTSDbField = ""
    Me.edArtDa.NTSFormat = "0"
    Me.edArtDa.NTSForzaVisZoom = False
    Me.edArtDa.NTSOldValue = "1"
    Me.edArtDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edArtDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edArtDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArtDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArtDa.Properties.AutoHeight = False
    Me.edArtDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArtDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArtDa.Size = New System.Drawing.Size(42, 20)
    Me.edArtDa.TabIndex = 21
    '
    'lbPosPre
    '
    Me.lbPosPre.AutoSize = True
    Me.lbPosPre.BackColor = System.Drawing.Color.Transparent
    Me.lbPosPre.Location = New System.Drawing.Point(155, 179)
    Me.lbPosPre.Name = "lbPosPre"
    Me.lbPosPre.NTSDbField = ""
    Me.lbPosPre.Size = New System.Drawing.Size(51, 13)
    Me.lbPosPre.TabIndex = 20
    Me.lbPosPre.Text = "Posizione"
    Me.lbPosPre.Tooltip = ""
    Me.lbPosPre.UseMnemonic = False
    '
    'lbPosQua
    '
    Me.lbPosQua.AutoSize = True
    Me.lbPosQua.BackColor = System.Drawing.Color.Transparent
    Me.lbPosQua.Location = New System.Drawing.Point(155, 156)
    Me.lbPosQua.Name = "lbPosQua"
    Me.lbPosQua.NTSDbField = ""
    Me.lbPosQua.Size = New System.Drawing.Size(51, 13)
    Me.lbPosQua.TabIndex = 19
    Me.lbPosQua.Text = "Posizione"
    Me.lbPosQua.Tooltip = ""
    Me.lbPosQua.UseMnemonic = False
    '
    'lbPosDescr
    '
    Me.lbPosDescr.AutoSize = True
    Me.lbPosDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbPosDescr.Location = New System.Drawing.Point(155, 111)
    Me.lbPosDescr.Name = "lbPosDescr"
    Me.lbPosDescr.NTSDbField = ""
    Me.lbPosDescr.Size = New System.Drawing.Size(51, 13)
    Me.lbPosDescr.TabIndex = 18
    Me.lbPosDescr.Text = "Posizione"
    Me.lbPosDescr.Tooltip = ""
    Me.lbPosDescr.UseMnemonic = False
    '
    'lbPosArt
    '
    Me.lbPosArt.AutoSize = True
    Me.lbPosArt.BackColor = System.Drawing.Color.Transparent
    Me.lbPosArt.Location = New System.Drawing.Point(155, 89)
    Me.lbPosArt.Name = "lbPosArt"
    Me.lbPosArt.NTSDbField = ""
    Me.lbPosArt.Size = New System.Drawing.Size(51, 13)
    Me.lbPosArt.TabIndex = 17
    Me.lbPosArt.Text = "Posizione"
    Me.lbPosArt.Tooltip = ""
    Me.lbPosArt.UseMnemonic = False
    '
    'edPosArt
    '
    Me.edPosArt.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosArt.EditValue = "1"
    Me.edPosArt.Enabled = False
    Me.edPosArt.Location = New System.Drawing.Point(210, 86)
    Me.edPosArt.Name = "edPosArt"
    Me.edPosArt.NTSDbField = ""
    Me.edPosArt.NTSFormat = "0"
    Me.edPosArt.NTSForzaVisZoom = False
    Me.edPosArt.NTSOldValue = "1"
    Me.edPosArt.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosArt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosArt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosArt.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosArt.Properties.AutoHeight = False
    Me.edPosArt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosArt.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosArt.Size = New System.Drawing.Size(37, 20)
    Me.edPosArt.TabIndex = 16
    '
    'edPosPre
    '
    Me.edPosPre.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosPre.EditValue = "1"
    Me.edPosPre.Enabled = False
    Me.edPosPre.Location = New System.Drawing.Point(210, 176)
    Me.edPosPre.Name = "edPosPre"
    Me.edPosPre.NTSDbField = ""
    Me.edPosPre.NTSFormat = "0"
    Me.edPosPre.NTSForzaVisZoom = False
    Me.edPosPre.NTSOldValue = "1"
    Me.edPosPre.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosPre.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosPre.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosPre.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosPre.Properties.AutoHeight = False
    Me.edPosPre.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosPre.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosPre.Size = New System.Drawing.Size(37, 20)
    Me.edPosPre.TabIndex = 15
    '
    'edPosQua
    '
    Me.edPosQua.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosQua.EditValue = "1"
    Me.edPosQua.Enabled = False
    Me.edPosQua.Location = New System.Drawing.Point(210, 153)
    Me.edPosQua.Name = "edPosQua"
    Me.edPosQua.NTSDbField = ""
    Me.edPosQua.NTSFormat = "0"
    Me.edPosQua.NTSForzaVisZoom = False
    Me.edPosQua.NTSOldValue = "1"
    Me.edPosQua.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosQua.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosQua.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosQua.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosQua.Properties.AutoHeight = False
    Me.edPosQua.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosQua.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosQua.Size = New System.Drawing.Size(37, 20)
    Me.edPosQua.TabIndex = 14
    '
    'edPosDescr
    '
    Me.edPosDescr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPosDescr.EditValue = "1"
    Me.edPosDescr.Enabled = False
    Me.edPosDescr.Location = New System.Drawing.Point(210, 108)
    Me.edPosDescr.Name = "edPosDescr"
    Me.edPosDescr.NTSDbField = ""
    Me.edPosDescr.NTSFormat = "0"
    Me.edPosDescr.NTSForzaVisZoom = False
    Me.edPosDescr.NTSOldValue = "1"
    Me.edPosDescr.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosDescr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosDescr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosDescr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosDescr.Properties.AutoHeight = False
    Me.edPosDescr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosDescr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosDescr.Size = New System.Drawing.Size(37, 20)
    Me.edPosDescr.TabIndex = 13
    '
    'ckBarcodeConfez
    '
    Me.ckBarcodeConfez.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckBarcodeConfez.Location = New System.Drawing.Point(278, 64)
    Me.ckBarcodeConfez.Name = "ckBarcodeConfez"
    Me.ckBarcodeConfez.NTSCheckValue = "S"
    Me.ckBarcodeConfez.NTSUnCheckValue = "N"
    Me.ckBarcodeConfez.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckBarcodeConfez.Properties.Appearance.Options.UseBackColor = True
    Me.ckBarcodeConfez.Properties.AutoHeight = False
    Me.ckBarcodeConfez.Properties.Caption = "Rileva la quantità nel campo colli"
    Me.ckBarcodeConfez.Size = New System.Drawing.Size(182, 19)
    Me.ckBarcodeConfez.TabIndex = 12
    '
    'ckPrezzo
    '
    Me.ckPrezzo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPrezzo.Location = New System.Drawing.Point(20, 177)
    Me.ckPrezzo.Name = "ckPrezzo"
    Me.ckPrezzo.NTSCheckValue = "S"
    Me.ckPrezzo.NTSUnCheckValue = "N"
    Me.ckPrezzo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPrezzo.Properties.Appearance.Options.UseBackColor = True
    Me.ckPrezzo.Properties.AutoHeight = False
    Me.ckPrezzo.Properties.Caption = "Prezzo"
    Me.ckPrezzo.Size = New System.Drawing.Size(56, 19)
    Me.ckPrezzo.TabIndex = 10
    '
    'ckQuant
    '
    Me.ckQuant.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckQuant.EditValue = True
    Me.ckQuant.Location = New System.Drawing.Point(20, 154)
    Me.ckQuant.Name = "ckQuant"
    Me.ckQuant.NTSCheckValue = "S"
    Me.ckQuant.NTSUnCheckValue = "N"
    Me.ckQuant.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckQuant.Properties.Appearance.Options.UseBackColor = True
    Me.ckQuant.Properties.AutoHeight = False
    Me.ckQuant.Properties.Caption = "Quantità"
    Me.ckQuant.Size = New System.Drawing.Size(83, 19)
    Me.ckQuant.TabIndex = 9
    '
    'ckDescrizione
    '
    Me.ckDescrizione.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDescrizione.EditValue = True
    Me.ckDescrizione.Location = New System.Drawing.Point(20, 109)
    Me.ckDescrizione.Name = "ckDescrizione"
    Me.ckDescrizione.NTSCheckValue = "S"
    Me.ckDescrizione.NTSUnCheckValue = "N"
    Me.ckDescrizione.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDescrizione.Properties.Appearance.Options.UseBackColor = True
    Me.ckDescrizione.Properties.AutoHeight = False
    Me.ckDescrizione.Properties.Caption = "Descrizione"
    Me.ckDescrizione.Size = New System.Drawing.Size(83, 19)
    Me.ckDescrizione.TabIndex = 8
    '
    'lbCinque
    '
    Me.lbCinque.AutoSize = True
    Me.lbCinque.BackColor = System.Drawing.Color.Transparent
    Me.lbCinque.Location = New System.Drawing.Point(4, 179)
    Me.lbCinque.Name = "lbCinque"
    Me.lbCinque.NTSDbField = ""
    Me.lbCinque.Size = New System.Drawing.Size(17, 13)
    Me.lbCinque.TabIndex = 7
    Me.lbCinque.Text = "5."
    Me.lbCinque.Tooltip = ""
    Me.lbCinque.UseMnemonic = False
    '
    'lbQuattro
    '
    Me.lbQuattro.AutoSize = True
    Me.lbQuattro.BackColor = System.Drawing.Color.Transparent
    Me.lbQuattro.Location = New System.Drawing.Point(4, 156)
    Me.lbQuattro.Name = "lbQuattro"
    Me.lbQuattro.NTSDbField = ""
    Me.lbQuattro.Size = New System.Drawing.Size(17, 13)
    Me.lbQuattro.TabIndex = 6
    Me.lbQuattro.Text = "4."
    Me.lbQuattro.Tooltip = ""
    Me.lbQuattro.UseMnemonic = False
    '
    'lbDue
    '
    Me.lbDue.AutoSize = True
    Me.lbDue.BackColor = System.Drawing.Color.Transparent
    Me.lbDue.Location = New System.Drawing.Point(4, 111)
    Me.lbDue.Name = "lbDue"
    Me.lbDue.NTSDbField = ""
    Me.lbDue.Size = New System.Drawing.Size(17, 13)
    Me.lbDue.TabIndex = 5
    Me.lbDue.Text = "2."
    Me.lbDue.Tooltip = ""
    Me.lbDue.UseMnemonic = False
    '
    'lbUno
    '
    Me.lbUno.AutoSize = True
    Me.lbUno.BackColor = System.Drawing.Color.Transparent
    Me.lbUno.Location = New System.Drawing.Point(4, 47)
    Me.lbUno.Name = "lbUno"
    Me.lbUno.NTSDbField = ""
    Me.lbUno.Size = New System.Drawing.Size(17, 13)
    Me.lbUno.TabIndex = 4
    Me.lbUno.Text = "1."
    Me.lbUno.Tooltip = ""
    Me.lbUno.UseMnemonic = False
    '
    'opCodarfo
    '
    Me.opCodarfo.Cursor = System.Windows.Forms.Cursors.Default
    Me.opCodarfo.Location = New System.Drawing.Point(20, 86)
    Me.opCodarfo.Name = "opCodarfo"
    Me.opCodarfo.NTSCheckValue = "S"
    Me.opCodarfo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCodarfo.Properties.Appearance.Options.UseBackColor = True
    Me.opCodarfo.Properties.AutoHeight = False
    Me.opCodarfo.Properties.Caption = "Cod. art. C/F."
    Me.opCodarfo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCodarfo.Size = New System.Drawing.Size(99, 19)
    Me.opCodarfo.TabIndex = 3
    '
    'opBarcode
    '
    Me.opBarcode.Cursor = System.Windows.Forms.Cursors.Default
    Me.opBarcode.Location = New System.Drawing.Point(20, 64)
    Me.opBarcode.Name = "opBarcode"
    Me.opBarcode.NTSCheckValue = "S"
    Me.opBarcode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opBarcode.Properties.Appearance.Options.UseBackColor = True
    Me.opBarcode.Properties.AutoHeight = False
    Me.opBarcode.Properties.Caption = "Barcode"
    Me.opBarcode.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opBarcode.Size = New System.Drawing.Size(66, 19)
    Me.opBarcode.TabIndex = 2
    '
    'opCodart
    '
    Me.opCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.opCodart.EditValue = True
    Me.opCodart.Location = New System.Drawing.Point(20, 45)
    Me.opCodart.Name = "opCodart"
    Me.opCodart.NTSCheckValue = "S"
    Me.opCodart.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCodart.Properties.Appearance.Options.UseBackColor = True
    Me.opCodart.Properties.AutoHeight = False
    Me.opCodart.Properties.Caption = "Cod. articolo"
    Me.opCodart.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCodart.Size = New System.Drawing.Size(89, 19)
    Me.opCodart.TabIndex = 1
    '
    'lbNomeFile
    '
    Me.lbNomeFile.AutoSize = True
    Me.lbNomeFile.BackColor = System.Drawing.Color.Transparent
    Me.lbNomeFile.Location = New System.Drawing.Point(5, 32)
    Me.lbNomeFile.Name = "lbNomeFile"
    Me.lbNomeFile.NTSDbField = ""
    Me.lbNomeFile.Size = New System.Drawing.Size(104, 13)
    Me.lbNomeFile.TabIndex = 8
    Me.lbNomeFile.Text = "Percorso e nome file"
    Me.lbNomeFile.Tooltip = ""
    Me.lbNomeFile.UseMnemonic = False
    '
    'ckSilent
    '
    Me.ckSilent.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSilent.Location = New System.Drawing.Point(213, 61)
    Me.ckSilent.Name = "ckSilent"
    Me.ckSilent.NTSCheckValue = "S"
    Me.ckSilent.NTSUnCheckValue = "N"
    Me.ckSilent.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSilent.Properties.Appearance.Options.UseBackColor = True
    Me.ckSilent.Properties.AutoHeight = False
    Me.ckSilent.Properties.Caption = "Lancia il comando in modalità nascosta"
    Me.ckSilent.Size = New System.Drawing.Size(212, 19)
    Me.ckSilent.TabIndex = 10
    '
    'ckShowLog
    '
    Me.ckShowLog.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckShowLog.Location = New System.Drawing.Point(213, 121)
    Me.ckShowLog.Name = "ckShowLog"
    Me.ckShowLog.NTSCheckValue = "S"
    Me.ckShowLog.NTSUnCheckValue = "N"
    Me.ckShowLog.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckShowLog.Properties.Appearance.Options.UseBackColor = True
    Me.ckShowLog.Properties.AutoHeight = False
    Me.ckShowLog.Properties.Caption = "Visualizza file log con gli errori di importazione"
    Me.ckShowLog.Size = New System.Drawing.Size(247, 19)
    Me.ckShowLog.TabIndex = 13
    '
    'edNomeFile
    '
    Me.edNomeFile.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNomeFile.Location = New System.Drawing.Point(116, 29)
    Me.edNomeFile.Name = "edNomeFile"
    Me.edNomeFile.NTSDbField = ""
    Me.edNomeFile.NTSForzaVisZoom = False
    Me.edNomeFile.NTSOldValue = ""
    Me.edNomeFile.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNomeFile.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNomeFile.Properties.AutoHeight = False
    Me.edNomeFile.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNomeFile.Properties.MaxLength = 65536
    Me.edNomeFile.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNomeFile.Size = New System.Drawing.Size(358, 20)
    Me.edNomeFile.TabIndex = 7
    '
    'ckDelFile
    '
    Me.ckDelFile.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDelFile.Location = New System.Drawing.Point(213, 81)
    Me.ckDelFile.Name = "ckDelFile"
    Me.ckDelFile.NTSCheckValue = "S"
    Me.ckDelFile.NTSUnCheckValue = "N"
    Me.ckDelFile.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDelFile.Properties.Appearance.Options.UseBackColor = True
    Me.ckDelFile.Properties.AutoHeight = False
    Me.ckDelFile.Properties.Caption = "Elimina il file a fine acquisizione"
    Me.ckDelFile.Size = New System.Drawing.Size(173, 19)
    Me.ckDelFile.TabIndex = 11
    '
    'ckIgnore1rk
    '
    Me.ckIgnore1rk.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckIgnore1rk.Location = New System.Drawing.Point(213, 101)
    Me.ckIgnore1rk.Name = "ckIgnore1rk"
    Me.ckIgnore1rk.NTSCheckValue = "S"
    Me.ckIgnore1rk.NTSUnCheckValue = "N"
    Me.ckIgnore1rk.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckIgnore1rk.Properties.Appearance.Options.UseBackColor = True
    Me.ckIgnore1rk.Properties.AutoHeight = False
    Me.ckIgnore1rk.Properties.Caption = "Ignora primo record"
    Me.ckIgnore1rk.Size = New System.Drawing.Size(125, 19)
    Me.ckIgnore1rk.TabIndex = 12
    '
    'cmdNomeFile
    '
    Me.cmdNomeFile.ImagePath = ""
    Me.cmdNomeFile.ImageText = ""
    Me.cmdNomeFile.Location = New System.Drawing.Point(479, 29)
    Me.cmdNomeFile.Name = "cmdNomeFile"
    Me.cmdNomeFile.NTSContextMenu = Nothing
    Me.cmdNomeFile.Size = New System.Drawing.Size(25, 20)
    Me.cmdNomeFile.TabIndex = 6
    Me.cmdNomeFile.Text = "..."
    '
    'tpExport
    '
    Me.tpExport.AllowDrop = True
    Me.tpExport.Controls.Add(Me.pnExport)
    Me.tpExport.Enable = True
    Me.tpExport.Name = "tpExport"
    Me.tpExport.Size = New System.Drawing.Size(508, 544)
    Me.tpExport.Text = "Esportazione righe"
    '
    'pnExport
    '
    Me.pnExport.AllowDrop = True
    Me.pnExport.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnExport.Appearance.Options.UseBackColor = True
    Me.pnExport.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnExport.Controls.Add(Me.ckTipoRigaE)
    Me.pnExport.Controls.Add(Me.edQtaTagliaE)
    Me.pnExport.Controls.Add(Me.edQtaMatrE)
    Me.pnExport.Controls.Add(Me.edTagliaE)
    Me.pnExport.Controls.Add(Me.edMatrE)
    Me.pnExport.Controls.Add(Me.lbQtaTagliaE)
    Me.pnExport.Controls.Add(Me.lbTagliaE)
    Me.pnExport.Controls.Add(Me.ckTagliaE)
    Me.pnExport.Controls.Add(Me.lbQtaMatE)
    Me.pnExport.Controls.Add(Me.lbMatrE)
    Me.pnExport.Controls.Add(Me.ckMatricE)
    Me.pnExport.Controls.Add(Me.cmdCancellaE)
    Me.pnExport.Controls.Add(Me.ckExecuteE)
    Me.pnExport.Controls.Add(Me.grExport)
    Me.pnExport.Controls.Add(Me.frmTipofileE)
    Me.pnExport.Controls.Add(Me.lbNomeFileE)
    Me.pnExport.Controls.Add(Me.lbCommandE)
    Me.pnExport.Controls.Add(Me.ckSilentE)
    Me.pnExport.Controls.Add(Me.ckRimuoviVbcrlf)
    Me.pnExport.Controls.Add(Me.ckShowLogE)
    Me.pnExport.Controls.Add(Me.edNomeFileE)
    Me.pnExport.Controls.Add(Me.cmdCommandE)
    Me.pnExport.Controls.Add(Me.cmdNomeFileE)
    Me.pnExport.Controls.Add(Me.ckGenTracciatoE)
    Me.pnExport.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnExport.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnExport.Location = New System.Drawing.Point(0, 0)
    Me.pnExport.Name = "pnExport"
    Me.pnExport.NTSActiveTrasparency = True
    Me.pnExport.Size = New System.Drawing.Size(508, 544)
    Me.pnExport.TabIndex = 26
    Me.pnExport.Text = "NtsPanel1"
    '
    'ckTipoRigaE
    '
    Me.ckTipoRigaE.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTipoRigaE.Location = New System.Drawing.Point(367, 99)
    Me.ckTipoRigaE.Name = "ckTipoRigaE"
    Me.ckTipoRigaE.NTSCheckValue = "S"
    Me.ckTipoRigaE.NTSUnCheckValue = "N"
    Me.ckTipoRigaE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTipoRigaE.Properties.Appearance.Options.UseBackColor = True
    Me.ckTipoRigaE.Properties.AutoHeight = False
    Me.ckTipoRigaE.Properties.Caption = "Inserisci tipo riga"
    Me.ckTipoRigaE.Size = New System.Drawing.Size(109, 19)
    Me.ckTipoRigaE.TabIndex = 64
    '
    'edQtaTagliaE
    '
    Me.edQtaTagliaE.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQtaTagliaE.EditValue = "1"
    Me.edQtaTagliaE.Enabled = False
    Me.edQtaTagliaE.Location = New System.Drawing.Point(324, 520)
    Me.edQtaTagliaE.Name = "edQtaTagliaE"
    Me.edQtaTagliaE.NTSDbField = ""
    Me.edQtaTagliaE.NTSFormat = "0"
    Me.edQtaTagliaE.NTSForzaVisZoom = False
    Me.edQtaTagliaE.NTSOldValue = "1"
    Me.edQtaTagliaE.Properties.Appearance.Options.UseTextOptions = True
    Me.edQtaTagliaE.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQtaTagliaE.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQtaTagliaE.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQtaTagliaE.Properties.AutoHeight = False
    Me.edQtaTagliaE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQtaTagliaE.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQtaTagliaE.Size = New System.Drawing.Size(42, 20)
    Me.edQtaTagliaE.TabIndex = 52
    '
    'edQtaMatrE
    '
    Me.edQtaMatrE.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQtaMatrE.EditValue = "1"
    Me.edQtaMatrE.Enabled = False
    Me.edQtaMatrE.Location = New System.Drawing.Point(324, 497)
    Me.edQtaMatrE.Name = "edQtaMatrE"
    Me.edQtaMatrE.NTSDbField = ""
    Me.edQtaMatrE.NTSFormat = "0"
    Me.edQtaMatrE.NTSForzaVisZoom = False
    Me.edQtaMatrE.NTSOldValue = "1"
    Me.edQtaMatrE.Properties.Appearance.Options.UseTextOptions = True
    Me.edQtaMatrE.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQtaMatrE.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQtaMatrE.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQtaMatrE.Properties.AutoHeight = False
    Me.edQtaMatrE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQtaMatrE.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQtaMatrE.Size = New System.Drawing.Size(42, 20)
    Me.edQtaMatrE.TabIndex = 51
    '
    'edTagliaE
    '
    Me.edTagliaE.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTagliaE.EditValue = "1"
    Me.edTagliaE.Enabled = False
    Me.edTagliaE.Location = New System.Drawing.Point(204, 520)
    Me.edTagliaE.Name = "edTagliaE"
    Me.edTagliaE.NTSDbField = ""
    Me.edTagliaE.NTSFormat = "0"
    Me.edTagliaE.NTSForzaVisZoom = False
    Me.edTagliaE.NTSOldValue = "1"
    Me.edTagliaE.Properties.Appearance.Options.UseTextOptions = True
    Me.edTagliaE.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTagliaE.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTagliaE.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTagliaE.Properties.AutoHeight = False
    Me.edTagliaE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTagliaE.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTagliaE.Size = New System.Drawing.Size(42, 20)
    Me.edTagliaE.TabIndex = 50
    '
    'edMatrE
    '
    Me.edMatrE.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMatrE.EditValue = "1"
    Me.edMatrE.Enabled = False
    Me.edMatrE.Location = New System.Drawing.Point(204, 497)
    Me.edMatrE.Name = "edMatrE"
    Me.edMatrE.NTSDbField = ""
    Me.edMatrE.NTSFormat = "0"
    Me.edMatrE.NTSForzaVisZoom = False
    Me.edMatrE.NTSOldValue = "1"
    Me.edMatrE.Properties.Appearance.Options.UseTextOptions = True
    Me.edMatrE.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMatrE.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMatrE.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMatrE.Properties.AutoHeight = False
    Me.edMatrE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMatrE.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMatrE.Size = New System.Drawing.Size(42, 20)
    Me.edMatrE.TabIndex = 49
    '
    'lbQtaTagliaE
    '
    Me.lbQtaTagliaE.AutoSize = True
    Me.lbQtaTagliaE.BackColor = System.Drawing.Color.Transparent
    Me.lbQtaTagliaE.Location = New System.Drawing.Point(268, 523)
    Me.lbQtaTagliaE.Name = "lbQtaTagliaE"
    Me.lbQtaTagliaE.NTSDbField = ""
    Me.lbQtaTagliaE.Size = New System.Drawing.Size(50, 13)
    Me.lbQtaTagliaE.TabIndex = 48
    Me.lbQtaTagliaE.Text = "lung. qta"
    Me.lbQtaTagliaE.Tooltip = ""
    Me.lbQtaTagliaE.UseMnemonic = False
    '
    'lbTagliaE
    '
    Me.lbTagliaE.AutoSize = True
    Me.lbTagliaE.BackColor = System.Drawing.Color.Transparent
    Me.lbTagliaE.Location = New System.Drawing.Point(143, 523)
    Me.lbTagliaE.Name = "lbTagliaE"
    Me.lbTagliaE.NTSDbField = ""
    Me.lbTagliaE.Size = New System.Drawing.Size(55, 13)
    Me.lbTagliaE.TabIndex = 47
    Me.lbTagliaE.Text = "lunghezza"
    Me.lbTagliaE.Tooltip = ""
    Me.lbTagliaE.UseMnemonic = False
    '
    'ckTagliaE
    '
    Me.ckTagliaE.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTagliaE.Location = New System.Drawing.Point(3, 521)
    Me.ckTagliaE.Name = "ckTagliaE"
    Me.ckTagliaE.NTSCheckValue = "S"
    Me.ckTagliaE.NTSUnCheckValue = "N"
    Me.ckTagliaE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTagliaE.Properties.Appearance.Options.UseBackColor = True
    Me.ckTagliaE.Properties.AutoHeight = False
    Me.ckTagliaE.Properties.Caption = "Esporta taglia"
    Me.ckTagliaE.Size = New System.Drawing.Size(92, 19)
    Me.ckTagliaE.TabIndex = 46
    '
    'lbQtaMatE
    '
    Me.lbQtaMatE.AutoSize = True
    Me.lbQtaMatE.BackColor = System.Drawing.Color.Transparent
    Me.lbQtaMatE.Location = New System.Drawing.Point(268, 500)
    Me.lbQtaMatE.Name = "lbQtaMatE"
    Me.lbQtaMatE.NTSDbField = ""
    Me.lbQtaMatE.Size = New System.Drawing.Size(50, 13)
    Me.lbQtaMatE.TabIndex = 45
    Me.lbQtaMatE.Text = "lung. qta"
    Me.lbQtaMatE.Tooltip = ""
    Me.lbQtaMatE.UseMnemonic = False
    '
    'lbMatrE
    '
    Me.lbMatrE.AutoSize = True
    Me.lbMatrE.BackColor = System.Drawing.Color.Transparent
    Me.lbMatrE.Location = New System.Drawing.Point(143, 500)
    Me.lbMatrE.Name = "lbMatrE"
    Me.lbMatrE.NTSDbField = ""
    Me.lbMatrE.Size = New System.Drawing.Size(55, 13)
    Me.lbMatrE.TabIndex = 44
    Me.lbMatrE.Text = "lunghezza"
    Me.lbMatrE.Tooltip = ""
    Me.lbMatrE.UseMnemonic = False
    '
    'ckMatricE
    '
    Me.ckMatricE.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMatricE.Location = New System.Drawing.Point(3, 498)
    Me.ckMatricE.Name = "ckMatricE"
    Me.ckMatricE.NTSCheckValue = "S"
    Me.ckMatricE.NTSUnCheckValue = "N"
    Me.ckMatricE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMatricE.Properties.Appearance.Options.UseBackColor = True
    Me.ckMatricE.Properties.AutoHeight = False
    Me.ckMatricE.Properties.Caption = "Esporta matricole"
    Me.ckMatricE.Size = New System.Drawing.Size(112, 19)
    Me.ckMatricE.TabIndex = 43
    '
    'cmdCancellaE
    '
    Me.cmdCancellaE.Image = CType(resources.GetObject("cmdCancellaE.Image"), System.Drawing.Image)
    Me.cmdCancellaE.ImagePath = ""
    Me.cmdCancellaE.ImageText = ""
    Me.cmdCancellaE.Location = New System.Drawing.Point(422, 510)
    Me.cmdCancellaE.Name = "cmdCancellaE"
    Me.cmdCancellaE.NTSContextMenu = Nothing
    Me.cmdCancellaE.Size = New System.Drawing.Size(83, 26)
    Me.cmdCancellaE.TabIndex = 42
    Me.cmdCancellaE.Text = "Cancella"
    '
    'ckExecuteE
    '
    Me.ckExecuteE.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckExecuteE.Location = New System.Drawing.Point(3, 3)
    Me.ckExecuteE.Name = "ckExecuteE"
    Me.ckExecuteE.NTSCheckValue = "S"
    Me.ckExecuteE.NTSUnCheckValue = "N"
    Me.ckExecuteE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckExecuteE.Properties.Appearance.Options.UseBackColor = True
    Me.ckExecuteE.Properties.AutoHeight = False
    Me.ckExecuteE.Properties.Caption = "Esegui il comando"
    Me.ckExecuteE.Size = New System.Drawing.Size(112, 19)
    Me.ckExecuteE.TabIndex = 14
    '
    'grExport
    '
    Me.grExport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grExport.EmbeddedNavigator.Name = ""
    Me.grExport.Location = New System.Drawing.Point(3, 172)
    Me.grExport.MainView = Me.grvExport
    Me.grExport.Name = "grExport"
    Me.grExport.Size = New System.Drawing.Size(502, 323)
    Me.grExport.TabIndex = 25
    Me.grExport.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvExport})
    '
    'grvExport
    '
    Me.grvExport.ActiveFilterEnabled = False
    Me.grvExport.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_nomee, Me.xx_pose, Me.xx_lune, Me.xx_dive})
    Me.grvExport.Enabled = True
    Me.grvExport.GridControl = Me.grExport
    Me.grvExport.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvExport.MinRowHeight = 14
    Me.grvExport.Name = "grvExport"
    Me.grvExport.NTSAllowDelete = True
    Me.grvExport.NTSAllowInsert = True
    Me.grvExport.NTSAllowUpdate = True
    Me.grvExport.NTSMenuContext = Nothing
    Me.grvExport.OptionsCustomization.AllowRowSizing = True
    Me.grvExport.OptionsFilter.AllowFilterEditor = False
    Me.grvExport.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvExport.OptionsNavigation.UseTabKey = False
    Me.grvExport.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvExport.OptionsView.ColumnAutoWidth = False
    Me.grvExport.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvExport.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvExport.OptionsView.ShowGroupPanel = False
    Me.grvExport.RowHeight = 14
    '
    'xx_nomee
    '
    Me.xx_nomee.AppearanceCell.Options.UseBackColor = True
    Me.xx_nomee.AppearanceCell.Options.UseTextOptions = True
    Me.xx_nomee.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_nomee.Caption = "Colonna"
    Me.xx_nomee.Enabled = True
    Me.xx_nomee.FieldName = "xx_nomee"
    Me.xx_nomee.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_nomee.Name = "xx_nomee"
    Me.xx_nomee.NTSRepositoryComboBox = Nothing
    Me.xx_nomee.NTSRepositoryItemCheck = Nothing
    Me.xx_nomee.NTSRepositoryItemMemo = Nothing
    Me.xx_nomee.NTSRepositoryItemText = Nothing
    Me.xx_nomee.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_nomee.OptionsFilter.AllowFilter = False
    Me.xx_nomee.Visible = True
    Me.xx_nomee.VisibleIndex = 0
    '
    'xx_pose
    '
    Me.xx_pose.AppearanceCell.Options.UseBackColor = True
    Me.xx_pose.AppearanceCell.Options.UseTextOptions = True
    Me.xx_pose.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_pose.Caption = "Posizione"
    Me.xx_pose.Enabled = True
    Me.xx_pose.FieldName = "xx_pose"
    Me.xx_pose.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_pose.Name = "xx_pose"
    Me.xx_pose.NTSRepositoryComboBox = Nothing
    Me.xx_pose.NTSRepositoryItemCheck = Nothing
    Me.xx_pose.NTSRepositoryItemMemo = Nothing
    Me.xx_pose.NTSRepositoryItemText = Nothing
    Me.xx_pose.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_pose.OptionsFilter.AllowFilter = False
    Me.xx_pose.Visible = True
    Me.xx_pose.VisibleIndex = 1
    '
    'xx_lune
    '
    Me.xx_lune.AppearanceCell.Options.UseBackColor = True
    Me.xx_lune.AppearanceCell.Options.UseTextOptions = True
    Me.xx_lune.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_lune.Caption = "Lunghezza"
    Me.xx_lune.Enabled = True
    Me.xx_lune.FieldName = "xx_lune"
    Me.xx_lune.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_lune.Name = "xx_lune"
    Me.xx_lune.NTSRepositoryComboBox = Nothing
    Me.xx_lune.NTSRepositoryItemCheck = Nothing
    Me.xx_lune.NTSRepositoryItemMemo = Nothing
    Me.xx_lune.NTSRepositoryItemText = Nothing
    Me.xx_lune.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_lune.OptionsFilter.AllowFilter = False
    Me.xx_lune.Visible = True
    Me.xx_lune.VisibleIndex = 2
    '
    'xx_dive
    '
    Me.xx_dive.AppearanceCell.Options.UseBackColor = True
    Me.xx_dive.AppearanceCell.Options.UseTextOptions = True
    Me.xx_dive.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_dive.Caption = "Dividi per"
    Me.xx_dive.Enabled = True
    Me.xx_dive.FieldName = "xx_dive"
    Me.xx_dive.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_dive.Name = "xx_dive"
    Me.xx_dive.NTSRepositoryComboBox = Nothing
    Me.xx_dive.NTSRepositoryItemCheck = Nothing
    Me.xx_dive.NTSRepositoryItemMemo = Nothing
    Me.xx_dive.NTSRepositoryItemText = Nothing
    Me.xx_dive.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_dive.OptionsFilter.AllowFilter = False
    Me.xx_dive.Visible = True
    Me.xx_dive.VisibleIndex = 3
    '
    'frmTipofileE
    '
    Me.frmTipofileE.AllowDrop = True
    Me.frmTipofileE.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frmTipofileE.Appearance.Options.UseBackColor = True
    Me.frmTipofileE.Controls.Add(Me.edSepFieldE)
    Me.frmTipofileE.Controls.Add(Me.opDelimE)
    Me.frmTipofileE.Controls.Add(Me.opFixedE)
    Me.frmTipofileE.Cursor = System.Windows.Forms.Cursors.Default
    Me.frmTipofileE.Location = New System.Drawing.Point(3, 66)
    Me.frmTipofileE.Name = "frmTipofileE"
    Me.frmTipofileE.Size = New System.Drawing.Size(153, 76)
    Me.frmTipofileE.TabIndex = 20
    Me.frmTipofileE.Text = "Tipo file"
    '
    'edSepFieldE
    '
    Me.edSepFieldE.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSepFieldE.EditValue = ";"
    Me.edSepFieldE.Enabled = False
    Me.edSepFieldE.Location = New System.Drawing.Point(118, 47)
    Me.edSepFieldE.Name = "edSepFieldE"
    Me.edSepFieldE.NTSDbField = ""
    Me.edSepFieldE.NTSForzaVisZoom = False
    Me.edSepFieldE.NTSOldValue = ";"
    Me.edSepFieldE.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSepFieldE.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSepFieldE.Properties.AutoHeight = False
    Me.edSepFieldE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSepFieldE.Properties.MaxLength = 65536
    Me.edSepFieldE.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSepFieldE.Size = New System.Drawing.Size(24, 20)
    Me.edSepFieldE.TabIndex = 2
    '
    'opDelimE
    '
    Me.opDelimE.Cursor = System.Windows.Forms.Cursors.Default
    Me.opDelimE.Location = New System.Drawing.Point(5, 48)
    Me.opDelimE.Name = "opDelimE"
    Me.opDelimE.NTSCheckValue = "S"
    Me.opDelimE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDelimE.Properties.Appearance.Options.UseBackColor = True
    Me.opDelimE.Properties.AutoHeight = False
    Me.opDelimE.Properties.Caption = "Separat. di campo"
    Me.opDelimE.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDelimE.Size = New System.Drawing.Size(115, 19)
    Me.opDelimE.TabIndex = 1
    '
    'opFixedE
    '
    Me.opFixedE.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.opFixedE.EditValue = True
    Me.opFixedE.Location = New System.Drawing.Point(5, 23)
    Me.opFixedE.Name = "opFixedE"
    Me.opFixedE.NTSCheckValue = "S"
    Me.opFixedE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFixedE.Properties.Appearance.Options.UseBackColor = True
    Me.opFixedE.Properties.AutoHeight = False
    Me.opFixedE.Properties.Caption = "Posizioni fisse"
    Me.opFixedE.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFixedE.Size = New System.Drawing.Size(99, 19)
    Me.opFixedE.TabIndex = 0
    '
    'lbNomeFileE
    '
    Me.lbNomeFileE.AutoSize = True
    Me.lbNomeFileE.BackColor = System.Drawing.Color.Transparent
    Me.lbNomeFileE.Location = New System.Drawing.Point(3, 36)
    Me.lbNomeFileE.Name = "lbNomeFileE"
    Me.lbNomeFileE.NTSDbField = ""
    Me.lbNomeFileE.Size = New System.Drawing.Size(104, 13)
    Me.lbNomeFileE.TabIndex = 19
    Me.lbNomeFileE.Text = "Percorso e nome file"
    Me.lbNomeFileE.Tooltip = ""
    Me.lbNomeFileE.UseMnemonic = False
    '
    'lbCommandE
    '
    Me.lbCommandE.BackColor = System.Drawing.Color.Transparent
    Me.lbCommandE.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCommandE.Location = New System.Drawing.Point(121, 4)
    Me.lbCommandE.Name = "lbCommandE"
    Me.lbCommandE.NTSDbField = ""
    Me.lbCommandE.Size = New System.Drawing.Size(343, 20)
    Me.lbCommandE.TabIndex = 15
    Me.lbCommandE.Tooltip = ""
    Me.lbCommandE.UseMnemonic = False
    '
    'ckSilentE
    '
    Me.ckSilentE.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSilentE.Enabled = False
    Me.ckSilentE.Location = New System.Drawing.Point(162, 66)
    Me.ckSilentE.Name = "ckSilentE"
    Me.ckSilentE.NTSCheckValue = "S"
    Me.ckSilentE.NTSUnCheckValue = "N"
    Me.ckSilentE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSilentE.Properties.Appearance.Options.UseBackColor = True
    Me.ckSilentE.Properties.AutoHeight = False
    Me.ckSilentE.Properties.Caption = "Lancia il comando in modalità nascosta"
    Me.ckSilentE.Size = New System.Drawing.Size(212, 19)
    Me.ckSilentE.TabIndex = 21
    '
    'ckRimuoviVbcrlf
    '
    Me.ckRimuoviVbcrlf.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRimuoviVbcrlf.EditValue = True
    Me.ckRimuoviVbcrlf.Location = New System.Drawing.Point(162, 119)
    Me.ckRimuoviVbcrlf.Name = "ckRimuoviVbcrlf"
    Me.ckRimuoviVbcrlf.NTSCheckValue = "S"
    Me.ckRimuoviVbcrlf.NTSUnCheckValue = "N"
    Me.ckRimuoviVbcrlf.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRimuoviVbcrlf.Properties.Appearance.Options.UseBackColor = True
    Me.ckRimuoviVbcrlf.Properties.AutoHeight = False
    Me.ckRimuoviVbcrlf.Properties.Caption = "Rimuovi ""andare a capo"" dalle note e dalle descrizioni"
    Me.ckRimuoviVbcrlf.Size = New System.Drawing.Size(284, 19)
    Me.ckRimuoviVbcrlf.TabIndex = 24
    '
    'ckShowLogE
    '
    Me.ckShowLogE.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckShowLogE.Location = New System.Drawing.Point(162, 140)
    Me.ckShowLogE.Name = "ckShowLogE"
    Me.ckShowLogE.NTSCheckValue = "S"
    Me.ckShowLogE.NTSUnCheckValue = "N"
    Me.ckShowLogE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckShowLogE.Properties.Appearance.Options.UseBackColor = True
    Me.ckShowLogE.Properties.AutoHeight = False
    Me.ckShowLogE.Properties.Caption = "Visualizza file log con gli errori di importazione"
    Me.ckShowLogE.Size = New System.Drawing.Size(247, 19)
    Me.ckShowLogE.TabIndex = 24
    '
    'edNomeFileE
    '
    Me.edNomeFileE.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNomeFileE.Location = New System.Drawing.Point(121, 33)
    Me.edNomeFileE.Name = "edNomeFileE"
    Me.edNomeFileE.NTSDbField = ""
    Me.edNomeFileE.NTSForzaVisZoom = False
    Me.edNomeFileE.NTSOldValue = ""
    Me.edNomeFileE.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNomeFileE.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNomeFileE.Properties.AutoHeight = False
    Me.edNomeFileE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNomeFileE.Properties.MaxLength = 65536
    Me.edNomeFileE.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNomeFileE.Size = New System.Drawing.Size(343, 20)
    Me.edNomeFileE.TabIndex = 18
    '
    'cmdCommandE
    '
    Me.cmdCommandE.Enabled = False
    Me.cmdCommandE.ImagePath = ""
    Me.cmdCommandE.ImageText = ""
    Me.cmdCommandE.Location = New System.Drawing.Point(470, 4)
    Me.cmdCommandE.Name = "cmdCommandE"
    Me.cmdCommandE.NTSContextMenu = Nothing
    Me.cmdCommandE.Size = New System.Drawing.Size(25, 20)
    Me.cmdCommandE.TabIndex = 16
    Me.cmdCommandE.Text = "..."
    '
    'cmdNomeFileE
    '
    Me.cmdNomeFileE.ImagePath = ""
    Me.cmdNomeFileE.ImageText = ""
    Me.cmdNomeFileE.Location = New System.Drawing.Point(470, 33)
    Me.cmdNomeFileE.Name = "cmdNomeFileE"
    Me.cmdNomeFileE.NTSContextMenu = Nothing
    Me.cmdNomeFileE.Size = New System.Drawing.Size(25, 20)
    Me.cmdNomeFileE.TabIndex = 17
    Me.cmdNomeFileE.Text = "..."
    '
    'ckGenTracciatoE
    '
    Me.ckGenTracciatoE.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckGenTracciatoE.Location = New System.Drawing.Point(162, 99)
    Me.ckGenTracciatoE.Name = "ckGenTracciatoE"
    Me.ckGenTracciatoE.NTSCheckValue = "S"
    Me.ckGenTracciatoE.NTSUnCheckValue = "N"
    Me.ckGenTracciatoE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckGenTracciatoE.Properties.Appearance.Options.UseBackColor = True
    Me.ckGenTracciatoE.Properties.AutoHeight = False
    Me.ckGenTracciatoE.Properties.Caption = "Genera tracciato"
    Me.ckGenTracciatoE.Size = New System.Drawing.Size(108, 19)
    Me.ckGenTracciatoE.TabIndex = 23
    '
    'tpImport
    '
    Me.tpImport.AllowDrop = True
    Me.tpImport.Controls.Add(Me.NtsPanel2)
    Me.tpImport.Enable = True
    Me.tpImport.Name = "tpImport"
    Me.tpImport.Size = New System.Drawing.Size(508, 544)
    Me.tpImport.Text = "Importazione file"
    '
    'NtsPanel2
    '
    Me.NtsPanel2.AllowDrop = True
    Me.NtsPanel2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel2.Appearance.Options.UseBackColor = True
    Me.NtsPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel2.Controls.Add(Me.ckTipoRigaI)
    Me.NtsPanel2.Controls.Add(Me.edQtaTagliaI)
    Me.NtsPanel2.Controls.Add(Me.edQtaMatrI)
    Me.NtsPanel2.Controls.Add(Me.edTagliaI)
    Me.NtsPanel2.Controls.Add(Me.edMatricI)
    Me.NtsPanel2.Controls.Add(Me.lbQtaTagliaI)
    Me.NtsPanel2.Controls.Add(Me.lbTagliaI)
    Me.NtsPanel2.Controls.Add(Me.ckTagliaI)
    Me.NtsPanel2.Controls.Add(Me.lbQtaMatrI)
    Me.NtsPanel2.Controls.Add(Me.lbMatricI)
    Me.NtsPanel2.Controls.Add(Me.ckMatricI)
    Me.NtsPanel2.Controls.Add(Me.cmdCancellaI)
    Me.NtsPanel2.Controls.Add(Me.ckExecuteI)
    Me.NtsPanel2.Controls.Add(Me.grImport)
    Me.NtsPanel2.Controls.Add(Me.frmTipofileI)
    Me.NtsPanel2.Controls.Add(Me.ckProvvI)
    Me.NtsPanel2.Controls.Add(Me.lbNomeFileI)
    Me.NtsPanel2.Controls.Add(Me.ckScontiI)
    Me.NtsPanel2.Controls.Add(Me.ckSilentI)
    Me.NtsPanel2.Controls.Add(Me.ckPrezziI)
    Me.NtsPanel2.Controls.Add(Me.edNomeFileI)
    Me.NtsPanel2.Controls.Add(Me.ckDelFileI)
    Me.NtsPanel2.Controls.Add(Me.lbCommandI)
    Me.NtsPanel2.Controls.Add(Me.cmdNomeFileI)
    Me.NtsPanel2.Controls.Add(Me.ckShowLogI)
    Me.NtsPanel2.Controls.Add(Me.ckIgnore1rkI)
    Me.NtsPanel2.Controls.Add(Me.cmdCommandI)
    Me.NtsPanel2.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NtsPanel2.Location = New System.Drawing.Point(0, 0)
    Me.NtsPanel2.Name = "NtsPanel2"
    Me.NtsPanel2.NTSActiveTrasparency = True
    Me.NtsPanel2.Size = New System.Drawing.Size(508, 544)
    Me.NtsPanel2.TabIndex = 41
    Me.NtsPanel2.Text = "NtsPanel2"
    '
    'ckTipoRigaI
    '
    Me.ckTipoRigaI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTipoRigaI.Location = New System.Drawing.Point(367, 114)
    Me.ckTipoRigaI.Name = "ckTipoRigaI"
    Me.ckTipoRigaI.NTSCheckValue = "S"
    Me.ckTipoRigaI.NTSUnCheckValue = "N"
    Me.ckTipoRigaI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTipoRigaI.Properties.Appearance.Options.UseBackColor = True
    Me.ckTipoRigaI.Properties.AutoHeight = False
    Me.ckTipoRigaI.Properties.Caption = "Usa tipo riga"
    Me.ckTipoRigaI.Size = New System.Drawing.Size(85, 19)
    Me.ckTipoRigaI.TabIndex = 63
    '
    'edQtaTagliaI
    '
    Me.edQtaTagliaI.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQtaTagliaI.EditValue = "1"
    Me.edQtaTagliaI.Enabled = False
    Me.edQtaTagliaI.Location = New System.Drawing.Point(324, 520)
    Me.edQtaTagliaI.Name = "edQtaTagliaI"
    Me.edQtaTagliaI.NTSDbField = ""
    Me.edQtaTagliaI.NTSFormat = "0"
    Me.edQtaTagliaI.NTSForzaVisZoom = False
    Me.edQtaTagliaI.NTSOldValue = "1"
    Me.edQtaTagliaI.Properties.Appearance.Options.UseTextOptions = True
    Me.edQtaTagliaI.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQtaTagliaI.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQtaTagliaI.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQtaTagliaI.Properties.AutoHeight = False
    Me.edQtaTagliaI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQtaTagliaI.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQtaTagliaI.Size = New System.Drawing.Size(42, 20)
    Me.edQtaTagliaI.TabIndex = 62
    '
    'edQtaMatrI
    '
    Me.edQtaMatrI.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQtaMatrI.EditValue = "1"
    Me.edQtaMatrI.Enabled = False
    Me.edQtaMatrI.Location = New System.Drawing.Point(324, 497)
    Me.edQtaMatrI.Name = "edQtaMatrI"
    Me.edQtaMatrI.NTSDbField = ""
    Me.edQtaMatrI.NTSFormat = "0"
    Me.edQtaMatrI.NTSForzaVisZoom = False
    Me.edQtaMatrI.NTSOldValue = "1"
    Me.edQtaMatrI.Properties.Appearance.Options.UseTextOptions = True
    Me.edQtaMatrI.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQtaMatrI.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQtaMatrI.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQtaMatrI.Properties.AutoHeight = False
    Me.edQtaMatrI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQtaMatrI.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQtaMatrI.Size = New System.Drawing.Size(42, 20)
    Me.edQtaMatrI.TabIndex = 61
    '
    'edTagliaI
    '
    Me.edTagliaI.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTagliaI.EditValue = "1"
    Me.edTagliaI.Enabled = False
    Me.edTagliaI.Location = New System.Drawing.Point(204, 520)
    Me.edTagliaI.Name = "edTagliaI"
    Me.edTagliaI.NTSDbField = ""
    Me.edTagliaI.NTSFormat = "0"
    Me.edTagliaI.NTSForzaVisZoom = False
    Me.edTagliaI.NTSOldValue = "1"
    Me.edTagliaI.Properties.Appearance.Options.UseTextOptions = True
    Me.edTagliaI.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTagliaI.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTagliaI.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTagliaI.Properties.AutoHeight = False
    Me.edTagliaI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTagliaI.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTagliaI.Size = New System.Drawing.Size(42, 20)
    Me.edTagliaI.TabIndex = 60
    '
    'edMatricI
    '
    Me.edMatricI.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMatricI.EditValue = "1"
    Me.edMatricI.Enabled = False
    Me.edMatricI.Location = New System.Drawing.Point(204, 497)
    Me.edMatricI.Name = "edMatricI"
    Me.edMatricI.NTSDbField = ""
    Me.edMatricI.NTSFormat = "0"
    Me.edMatricI.NTSForzaVisZoom = False
    Me.edMatricI.NTSOldValue = "1"
    Me.edMatricI.Properties.Appearance.Options.UseTextOptions = True
    Me.edMatricI.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMatricI.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMatricI.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMatricI.Properties.AutoHeight = False
    Me.edMatricI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMatricI.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMatricI.Size = New System.Drawing.Size(42, 20)
    Me.edMatricI.TabIndex = 59
    '
    'lbQtaTagliaI
    '
    Me.lbQtaTagliaI.AutoSize = True
    Me.lbQtaTagliaI.BackColor = System.Drawing.Color.Transparent
    Me.lbQtaTagliaI.Location = New System.Drawing.Point(268, 523)
    Me.lbQtaTagliaI.Name = "lbQtaTagliaI"
    Me.lbQtaTagliaI.NTSDbField = ""
    Me.lbQtaTagliaI.Size = New System.Drawing.Size(50, 13)
    Me.lbQtaTagliaI.TabIndex = 58
    Me.lbQtaTagliaI.Text = "lung. qta"
    Me.lbQtaTagliaI.Tooltip = ""
    Me.lbQtaTagliaI.UseMnemonic = False
    '
    'lbTagliaI
    '
    Me.lbTagliaI.AutoSize = True
    Me.lbTagliaI.BackColor = System.Drawing.Color.Transparent
    Me.lbTagliaI.Location = New System.Drawing.Point(143, 523)
    Me.lbTagliaI.Name = "lbTagliaI"
    Me.lbTagliaI.NTSDbField = ""
    Me.lbTagliaI.Size = New System.Drawing.Size(55, 13)
    Me.lbTagliaI.TabIndex = 57
    Me.lbTagliaI.Text = "lunghezza"
    Me.lbTagliaI.Tooltip = ""
    Me.lbTagliaI.UseMnemonic = False
    '
    'ckTagliaI
    '
    Me.ckTagliaI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTagliaI.Location = New System.Drawing.Point(3, 521)
    Me.ckTagliaI.Name = "ckTagliaI"
    Me.ckTagliaI.NTSCheckValue = "S"
    Me.ckTagliaI.NTSUnCheckValue = "N"
    Me.ckTagliaI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTagliaI.Properties.Appearance.Options.UseBackColor = True
    Me.ckTagliaI.Properties.AutoHeight = False
    Me.ckTagliaI.Properties.Caption = "Importa taglia"
    Me.ckTagliaI.Size = New System.Drawing.Size(92, 19)
    Me.ckTagliaI.TabIndex = 56
    '
    'lbQtaMatrI
    '
    Me.lbQtaMatrI.AutoSize = True
    Me.lbQtaMatrI.BackColor = System.Drawing.Color.Transparent
    Me.lbQtaMatrI.Location = New System.Drawing.Point(268, 500)
    Me.lbQtaMatrI.Name = "lbQtaMatrI"
    Me.lbQtaMatrI.NTSDbField = ""
    Me.lbQtaMatrI.Size = New System.Drawing.Size(50, 13)
    Me.lbQtaMatrI.TabIndex = 55
    Me.lbQtaMatrI.Text = "lung. qta"
    Me.lbQtaMatrI.Tooltip = ""
    Me.lbQtaMatrI.UseMnemonic = False
    '
    'lbMatricI
    '
    Me.lbMatricI.AutoSize = True
    Me.lbMatricI.BackColor = System.Drawing.Color.Transparent
    Me.lbMatricI.Location = New System.Drawing.Point(143, 500)
    Me.lbMatricI.Name = "lbMatricI"
    Me.lbMatricI.NTSDbField = ""
    Me.lbMatricI.Size = New System.Drawing.Size(55, 13)
    Me.lbMatricI.TabIndex = 54
    Me.lbMatricI.Text = "lunghezza"
    Me.lbMatricI.Tooltip = ""
    Me.lbMatricI.UseMnemonic = False
    '
    'ckMatricI
    '
    Me.ckMatricI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMatricI.Location = New System.Drawing.Point(3, 498)
    Me.ckMatricI.Name = "ckMatricI"
    Me.ckMatricI.NTSCheckValue = "S"
    Me.ckMatricI.NTSUnCheckValue = "N"
    Me.ckMatricI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMatricI.Properties.Appearance.Options.UseBackColor = True
    Me.ckMatricI.Properties.AutoHeight = False
    Me.ckMatricI.Properties.Caption = "Importa matricole"
    Me.ckMatricI.Size = New System.Drawing.Size(112, 19)
    Me.ckMatricI.TabIndex = 53
    '
    'cmdCancellaI
    '
    Me.cmdCancellaI.Image = CType(resources.GetObject("cmdCancellaI.Image"), System.Drawing.Image)
    Me.cmdCancellaI.ImagePath = ""
    Me.cmdCancellaI.ImageText = ""
    Me.cmdCancellaI.Location = New System.Drawing.Point(422, 510)
    Me.cmdCancellaI.Name = "cmdCancellaI"
    Me.cmdCancellaI.NTSContextMenu = Nothing
    Me.cmdCancellaI.Size = New System.Drawing.Size(83, 26)
    Me.cmdCancellaI.TabIndex = 41
    Me.cmdCancellaI.Text = "Cancella"
    '
    'ckExecuteI
    '
    Me.ckExecuteI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckExecuteI.Location = New System.Drawing.Point(3, 3)
    Me.ckExecuteI.Name = "ckExecuteI"
    Me.ckExecuteI.NTSCheckValue = "S"
    Me.ckExecuteI.NTSUnCheckValue = "N"
    Me.ckExecuteI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckExecuteI.Properties.Appearance.Options.UseBackColor = True
    Me.ckExecuteI.Properties.AutoHeight = False
    Me.ckExecuteI.Properties.Caption = "Esegui il comando"
    Me.ckExecuteI.Size = New System.Drawing.Size(112, 19)
    Me.ckExecuteI.TabIndex = 25
    '
    'grImport
    '
    Me.grImport.EmbeddedNavigator.Name = ""
    Me.grImport.Location = New System.Drawing.Point(3, 194)
    Me.grImport.MainView = Me.grvImport
    Me.grImport.Name = "grImport"
    Me.grImport.Size = New System.Drawing.Size(502, 301)
    Me.grImport.TabIndex = 40
    Me.grImport.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvImport})
    '
    'grvImport
    '
    Me.grvImport.ActiveFilterEnabled = False
    Me.grvImport.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_nomei, Me.xx_posi, Me.xx_luni, Me.xx_divi})
    Me.grvImport.Enabled = True
    Me.grvImport.GridControl = Me.grImport
    Me.grvImport.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvImport.MinRowHeight = 14
    Me.grvImport.Name = "grvImport"
    Me.grvImport.NTSAllowDelete = True
    Me.grvImport.NTSAllowInsert = True
    Me.grvImport.NTSAllowUpdate = True
    Me.grvImport.NTSMenuContext = Nothing
    Me.grvImport.OptionsCustomization.AllowRowSizing = True
    Me.grvImport.OptionsFilter.AllowFilterEditor = False
    Me.grvImport.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvImport.OptionsNavigation.UseTabKey = False
    Me.grvImport.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvImport.OptionsView.ColumnAutoWidth = False
    Me.grvImport.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvImport.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvImport.OptionsView.ShowGroupPanel = False
    Me.grvImport.RowHeight = 14
    '
    'xx_nomei
    '
    Me.xx_nomei.AppearanceCell.Options.UseBackColor = True
    Me.xx_nomei.AppearanceCell.Options.UseTextOptions = True
    Me.xx_nomei.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_nomei.Caption = "Colonna"
    Me.xx_nomei.Enabled = True
    Me.xx_nomei.FieldName = "xx_nomei"
    Me.xx_nomei.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_nomei.Name = "xx_nomei"
    Me.xx_nomei.NTSRepositoryComboBox = Nothing
    Me.xx_nomei.NTSRepositoryItemCheck = Nothing
    Me.xx_nomei.NTSRepositoryItemMemo = Nothing
    Me.xx_nomei.NTSRepositoryItemText = Nothing
    Me.xx_nomei.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_nomei.OptionsFilter.AllowFilter = False
    Me.xx_nomei.Visible = True
    Me.xx_nomei.VisibleIndex = 0
    '
    'xx_posi
    '
    Me.xx_posi.AppearanceCell.Options.UseBackColor = True
    Me.xx_posi.AppearanceCell.Options.UseTextOptions = True
    Me.xx_posi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_posi.Caption = "Posizione"
    Me.xx_posi.Enabled = True
    Me.xx_posi.FieldName = "xx_posi"
    Me.xx_posi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_posi.Name = "xx_posi"
    Me.xx_posi.NTSRepositoryComboBox = Nothing
    Me.xx_posi.NTSRepositoryItemCheck = Nothing
    Me.xx_posi.NTSRepositoryItemMemo = Nothing
    Me.xx_posi.NTSRepositoryItemText = Nothing
    Me.xx_posi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_posi.OptionsFilter.AllowFilter = False
    Me.xx_posi.Visible = True
    Me.xx_posi.VisibleIndex = 1
    '
    'xx_luni
    '
    Me.xx_luni.AppearanceCell.Options.UseBackColor = True
    Me.xx_luni.AppearanceCell.Options.UseTextOptions = True
    Me.xx_luni.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_luni.Caption = "Lunghezza"
    Me.xx_luni.Enabled = True
    Me.xx_luni.FieldName = "xx_luni"
    Me.xx_luni.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_luni.Name = "xx_luni"
    Me.xx_luni.NTSRepositoryComboBox = Nothing
    Me.xx_luni.NTSRepositoryItemCheck = Nothing
    Me.xx_luni.NTSRepositoryItemMemo = Nothing
    Me.xx_luni.NTSRepositoryItemText = Nothing
    Me.xx_luni.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_luni.OptionsFilter.AllowFilter = False
    Me.xx_luni.Visible = True
    Me.xx_luni.VisibleIndex = 2
    '
    'xx_divi
    '
    Me.xx_divi.AppearanceCell.Options.UseBackColor = True
    Me.xx_divi.AppearanceCell.Options.UseTextOptions = True
    Me.xx_divi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_divi.Caption = "Dividi per"
    Me.xx_divi.Enabled = True
    Me.xx_divi.FieldName = "xx_divi"
    Me.xx_divi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_divi.Name = "xx_divi"
    Me.xx_divi.NTSRepositoryComboBox = Nothing
    Me.xx_divi.NTSRepositoryItemCheck = Nothing
    Me.xx_divi.NTSRepositoryItemMemo = Nothing
    Me.xx_divi.NTSRepositoryItemText = Nothing
    Me.xx_divi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_divi.OptionsFilter.AllowFilter = False
    Me.xx_divi.Visible = True
    Me.xx_divi.VisibleIndex = 3
    '
    'frmTipofileI
    '
    Me.frmTipofileI.AllowDrop = True
    Me.frmTipofileI.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frmTipofileI.Appearance.Options.UseBackColor = True
    Me.frmTipofileI.Controls.Add(Me.edDecSepI)
    Me.frmTipofileI.Controls.Add(Me.lbDecSepI)
    Me.frmTipofileI.Controls.Add(Me.edSepFieldI)
    Me.frmTipofileI.Controls.Add(Me.opDelimI)
    Me.frmTipofileI.Controls.Add(Me.opFixedI)
    Me.frmTipofileI.Cursor = System.Windows.Forms.Cursors.Default
    Me.frmTipofileI.Location = New System.Drawing.Point(3, 66)
    Me.frmTipofileI.Name = "frmTipofileI"
    Me.frmTipofileI.Size = New System.Drawing.Size(153, 102)
    Me.frmTipofileI.TabIndex = 31
    Me.frmTipofileI.Text = "Tipo file"
    '
    'edDecSepI
    '
    Me.edDecSepI.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDecSepI.EditValue = ","
    Me.edDecSepI.Location = New System.Drawing.Point(118, 73)
    Me.edDecSepI.Name = "edDecSepI"
    Me.edDecSepI.NTSDbField = ""
    Me.edDecSepI.NTSForzaVisZoom = False
    Me.edDecSepI.NTSOldValue = ","
    Me.edDecSepI.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDecSepI.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDecSepI.Properties.AutoHeight = False
    Me.edDecSepI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDecSepI.Properties.MaxLength = 65536
    Me.edDecSepI.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDecSepI.Size = New System.Drawing.Size(24, 20)
    Me.edDecSepI.TabIndex = 10
    '
    'lbDecSepI
    '
    Me.lbDecSepI.AutoSize = True
    Me.lbDecSepI.BackColor = System.Drawing.Color.Transparent
    Me.lbDecSepI.Location = New System.Drawing.Point(5, 76)
    Me.lbDecSepI.Name = "lbDecSepI"
    Me.lbDecSepI.NTSDbField = ""
    Me.lbDecSepI.Size = New System.Drawing.Size(105, 13)
    Me.lbDecSepI.TabIndex = 9
    Me.lbDecSepI.Text = "Separatore decimale"
    Me.lbDecSepI.Tooltip = ""
    Me.lbDecSepI.UseMnemonic = False
    '
    'edSepFieldI
    '
    Me.edSepFieldI.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edSepFieldI.EditValue = ";"
    Me.edSepFieldI.Enabled = False
    Me.edSepFieldI.Location = New System.Drawing.Point(118, 47)
    Me.edSepFieldI.Name = "edSepFieldI"
    Me.edSepFieldI.NTSDbField = ""
    Me.edSepFieldI.NTSForzaVisZoom = False
    Me.edSepFieldI.NTSOldValue = ";"
    Me.edSepFieldI.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSepFieldI.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSepFieldI.Properties.AutoHeight = False
    Me.edSepFieldI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSepFieldI.Properties.MaxLength = 65536
    Me.edSepFieldI.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSepFieldI.Size = New System.Drawing.Size(24, 20)
    Me.edSepFieldI.TabIndex = 2
    '
    'opDelimI
    '
    Me.opDelimI.Cursor = System.Windows.Forms.Cursors.Hand
    Me.opDelimI.Location = New System.Drawing.Point(5, 48)
    Me.opDelimI.Name = "opDelimI"
    Me.opDelimI.NTSCheckValue = "S"
    Me.opDelimI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDelimI.Properties.Appearance.Options.UseBackColor = True
    Me.opDelimI.Properties.AutoHeight = False
    Me.opDelimI.Properties.Caption = "Separat. di campo"
    Me.opDelimI.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDelimI.Size = New System.Drawing.Size(115, 19)
    Me.opDelimI.TabIndex = 1
    '
    'opFixedI
    '
    Me.opFixedI.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFixedI.EditValue = True
    Me.opFixedI.Location = New System.Drawing.Point(5, 23)
    Me.opFixedI.Name = "opFixedI"
    Me.opFixedI.NTSCheckValue = "S"
    Me.opFixedI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFixedI.Properties.Appearance.Options.UseBackColor = True
    Me.opFixedI.Properties.AutoHeight = False
    Me.opFixedI.Properties.Caption = "Posizioni fisse"
    Me.opFixedI.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFixedI.Size = New System.Drawing.Size(99, 19)
    Me.opFixedI.TabIndex = 0
    '
    'ckProvvI
    '
    Me.ckProvvI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckProvvI.Location = New System.Drawing.Point(367, 164)
    Me.ckProvvI.Name = "ckProvvI"
    Me.ckProvvI.NTSCheckValue = "S"
    Me.ckProvvI.NTSUnCheckValue = "N"
    Me.ckProvvI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckProvvI.Properties.Appearance.Options.UseBackColor = True
    Me.ckProvvI.Properties.AutoHeight = False
    Me.ckProvvI.Properties.Caption = "Ricalcola provvigioni"
    Me.ckProvvI.Size = New System.Drawing.Size(128, 19)
    Me.ckProvvI.TabIndex = 39
    '
    'lbNomeFileI
    '
    Me.lbNomeFileI.AutoSize = True
    Me.lbNomeFileI.BackColor = System.Drawing.Color.Transparent
    Me.lbNomeFileI.Location = New System.Drawing.Point(3, 36)
    Me.lbNomeFileI.Name = "lbNomeFileI"
    Me.lbNomeFileI.NTSDbField = ""
    Me.lbNomeFileI.Size = New System.Drawing.Size(104, 13)
    Me.lbNomeFileI.TabIndex = 30
    Me.lbNomeFileI.Text = "Percorso e nome file"
    Me.lbNomeFileI.Tooltip = ""
    Me.lbNomeFileI.UseMnemonic = False
    '
    'ckScontiI
    '
    Me.ckScontiI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckScontiI.Location = New System.Drawing.Point(265, 164)
    Me.ckScontiI.Name = "ckScontiI"
    Me.ckScontiI.NTSCheckValue = "S"
    Me.ckScontiI.NTSUnCheckValue = "N"
    Me.ckScontiI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckScontiI.Properties.Appearance.Options.UseBackColor = True
    Me.ckScontiI.Properties.AutoHeight = False
    Me.ckScontiI.Properties.Caption = "Ricalcola sconti"
    Me.ckScontiI.Size = New System.Drawing.Size(97, 19)
    Me.ckScontiI.TabIndex = 38
    '
    'ckSilentI
    '
    Me.ckSilentI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSilentI.Enabled = False
    Me.ckSilentI.Location = New System.Drawing.Point(162, 66)
    Me.ckSilentI.Name = "ckSilentI"
    Me.ckSilentI.NTSCheckValue = "S"
    Me.ckSilentI.NTSUnCheckValue = "N"
    Me.ckSilentI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSilentI.Properties.Appearance.Options.UseBackColor = True
    Me.ckSilentI.Properties.AutoHeight = False
    Me.ckSilentI.Properties.Caption = "Lancia il comando in modalità nascosta"
    Me.ckSilentI.Size = New System.Drawing.Size(212, 19)
    Me.ckSilentI.TabIndex = 32
    '
    'ckPrezziI
    '
    Me.ckPrezziI.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckPrezziI.Location = New System.Drawing.Point(162, 164)
    Me.ckPrezziI.Name = "ckPrezziI"
    Me.ckPrezziI.NTSCheckValue = "S"
    Me.ckPrezziI.NTSUnCheckValue = "N"
    Me.ckPrezziI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPrezziI.Properties.Appearance.Options.UseBackColor = True
    Me.ckPrezziI.Properties.AutoHeight = False
    Me.ckPrezziI.Properties.Caption = "Ricalcola prezzi"
    Me.ckPrezziI.Size = New System.Drawing.Size(97, 19)
    Me.ckPrezziI.TabIndex = 37
    '
    'edNomeFileI
    '
    Me.edNomeFileI.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNomeFileI.Location = New System.Drawing.Point(121, 33)
    Me.edNomeFileI.Name = "edNomeFileI"
    Me.edNomeFileI.NTSDbField = ""
    Me.edNomeFileI.NTSForzaVisZoom = False
    Me.edNomeFileI.NTSOldValue = ""
    Me.edNomeFileI.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNomeFileI.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNomeFileI.Properties.AutoHeight = False
    Me.edNomeFileI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNomeFileI.Properties.MaxLength = 65536
    Me.edNomeFileI.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNomeFileI.Size = New System.Drawing.Size(343, 20)
    Me.edNomeFileI.TabIndex = 29
    '
    'ckDelFileI
    '
    Me.ckDelFileI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDelFileI.Location = New System.Drawing.Point(162, 89)
    Me.ckDelFileI.Name = "ckDelFileI"
    Me.ckDelFileI.NTSCheckValue = "S"
    Me.ckDelFileI.NTSUnCheckValue = "N"
    Me.ckDelFileI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDelFileI.Properties.Appearance.Options.UseBackColor = True
    Me.ckDelFileI.Properties.AutoHeight = False
    Me.ckDelFileI.Properties.Caption = "Elimina il file a fine acquisizione"
    Me.ckDelFileI.Size = New System.Drawing.Size(173, 19)
    Me.ckDelFileI.TabIndex = 33
    '
    'lbCommandI
    '
    Me.lbCommandI.BackColor = System.Drawing.Color.Transparent
    Me.lbCommandI.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCommandI.Location = New System.Drawing.Point(121, 4)
    Me.lbCommandI.Name = "lbCommandI"
    Me.lbCommandI.NTSDbField = ""
    Me.lbCommandI.Size = New System.Drawing.Size(343, 20)
    Me.lbCommandI.TabIndex = 26
    Me.lbCommandI.Tooltip = ""
    Me.lbCommandI.UseMnemonic = False
    '
    'cmdNomeFileI
    '
    Me.cmdNomeFileI.ImagePath = ""
    Me.cmdNomeFileI.ImageText = ""
    Me.cmdNomeFileI.Location = New System.Drawing.Point(470, 33)
    Me.cmdNomeFileI.Name = "cmdNomeFileI"
    Me.cmdNomeFileI.NTSContextMenu = Nothing
    Me.cmdNomeFileI.Size = New System.Drawing.Size(25, 20)
    Me.cmdNomeFileI.TabIndex = 28
    Me.cmdNomeFileI.Text = "..."
    '
    'ckShowLogI
    '
    Me.ckShowLogI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckShowLogI.Location = New System.Drawing.Point(162, 140)
    Me.ckShowLogI.Name = "ckShowLogI"
    Me.ckShowLogI.NTSCheckValue = "S"
    Me.ckShowLogI.NTSUnCheckValue = "N"
    Me.ckShowLogI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckShowLogI.Properties.Appearance.Options.UseBackColor = True
    Me.ckShowLogI.Properties.AutoHeight = False
    Me.ckShowLogI.Properties.Caption = "Visualizza file log con gli errori di importazione"
    Me.ckShowLogI.Size = New System.Drawing.Size(247, 19)
    Me.ckShowLogI.TabIndex = 35
    '
    'ckIgnore1rkI
    '
    Me.ckIgnore1rkI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckIgnore1rkI.Location = New System.Drawing.Point(162, 114)
    Me.ckIgnore1rkI.Name = "ckIgnore1rkI"
    Me.ckIgnore1rkI.NTSCheckValue = "S"
    Me.ckIgnore1rkI.NTSUnCheckValue = "N"
    Me.ckIgnore1rkI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckIgnore1rkI.Properties.Appearance.Options.UseBackColor = True
    Me.ckIgnore1rkI.Properties.AutoHeight = False
    Me.ckIgnore1rkI.Properties.Caption = "Ignora primo record"
    Me.ckIgnore1rkI.Size = New System.Drawing.Size(125, 19)
    Me.ckIgnore1rkI.TabIndex = 34
    '
    'cmdCommandI
    '
    Me.cmdCommandI.Enabled = False
    Me.cmdCommandI.ImagePath = ""
    Me.cmdCommandI.ImageText = ""
    Me.cmdCommandI.Location = New System.Drawing.Point(470, 4)
    Me.cmdCommandI.Name = "cmdCommandI"
    Me.cmdCommandI.NTSContextMenu = Nothing
    Me.cmdCommandI.Size = New System.Drawing.Size(25, 20)
    Me.cmdCommandI.TabIndex = 27
    Me.cmdCommandI.Text = "..."
    '
    'lbNota
    '
    Me.lbNota.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbNota.BackColor = System.Drawing.Color.Transparent
    Me.lbNota.Location = New System.Drawing.Point(520, 556)
    Me.lbNota.Name = "lbNota"
    Me.lbNota.NTSDbField = ""
    Me.lbNota.Size = New System.Drawing.Size(117, 39)
    Me.lbNota.TabIndex = 11
    Me.lbNota.Text = "Se la quantità non è selezionata viene considerata pari a 1"
    Me.lbNota.Tooltip = ""
    Me.lbNota.UseMnemonic = False
    '
    'lbAvviso
    '
    Me.lbAvviso.BackColor = System.Drawing.Color.Transparent
    Me.lbAvviso.Location = New System.Drawing.Point(520, 396)
    Me.lbAvviso.Name = "lbAvviso"
    Me.lbAvviso.NTSDbField = ""
    Me.lbAvviso.Size = New System.Drawing.Size(117, 119)
    Me.lbAvviso.TabIndex = 12
    Me.lbAvviso.Text = "L'esportazione o importazione di matricole o taglie prevede l'inserimento all'ini" & _
        "zio di ogni riga del tipo riga:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "1 - Riga normale" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2 - Dettaglio matricole" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "3 - " & _
        "Dettaglio taglie"
    Me.lbAvviso.Tooltip = ""
    Me.lbAvviso.UseMnemonic = False
    '
    'pnProfilo
    '
    Me.pnProfilo.AllowDrop = True
    Me.pnProfilo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnProfilo.Appearance.Options.UseBackColor = True
    Me.pnProfilo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnProfilo.Controls.Add(Me.cmdAggiorna)
    Me.pnProfilo.Controls.Add(Me.cbImpostaFile)
    Me.pnProfilo.Controls.Add(Me.cmdCancella)
    Me.pnProfilo.Controls.Add(Me.lbImpostaFile)
    Me.pnProfilo.Controls.Add(Me.cmdAggiungi)
    Me.pnProfilo.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnProfilo.Location = New System.Drawing.Point(0, 0)
    Me.pnProfilo.Name = "pnProfilo"
    Me.pnProfilo.NTSActiveTrasparency = True
    Me.pnProfilo.Size = New System.Drawing.Size(517, 30)
    Me.pnProfilo.TabIndex = 16
    Me.pnProfilo.Text = "NtsPanel3"
    '
    'FRMMGTERM
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(638, 602)
    Me.Controls.Add(Me.lbAvviso)
    Me.Controls.Add(Me.pnMain)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.Controls.Add(Me.lbNota)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGTERM"
    Me.Text = "IMPOSTAZIONI FILE TERMINALE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckExecute.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMain.ResumeLayout(False)
    CType(Me.tcImport, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tcImport.ResumeLayout(False)
    Me.tpTerminale.ResumeLayout(False)
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.NtsPanel1.PerformLayout()
    CType(Me.cbImpostaFile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frmTipofile, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frmTipofile.ResumeLayout(False)
    Me.frmTipofile.PerformLayout()
    CType(Me.edDecSep.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSepField.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opDelim.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFixed.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frmCampi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frmCampi.ResumeLayout(False)
    Me.frmCampi.PerformLayout()
    CType(Me.opCodartBarcodeCodarfo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatconsA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatconsDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosDatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDesintA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDesintDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosDesint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDesint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDivSc6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSc6A.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSc6Da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosSc6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconto6.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDivSc5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSc5A.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSc5Da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosSc5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconto5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDivSc4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSc4A.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSc4Da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosSc4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconto4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDivSc3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSc3A.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSc3Da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosSc3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconto3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDivSc2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSc2A.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSc2Da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosSc2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconto2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDivSc1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSc1A.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSc1Da.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosSc1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconto1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDivPrezzo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDivQuant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPreA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPreDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQuantA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQuantDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescrA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescrDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArtA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArtDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosArt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosPre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosQua.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosDescr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckBarcodeConfez.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPrezzo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckQuant.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDescrizione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCodarfo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opBarcode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSilent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckShowLog.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNomeFile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDelFile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckIgnore1rk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tpExport.ResumeLayout(False)
    CType(Me.pnExport, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnExport.ResumeLayout(False)
    Me.pnExport.PerformLayout()
    CType(Me.ckTipoRigaE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQtaTagliaE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQtaMatrE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTagliaE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMatrE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTagliaE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMatricE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckExecuteE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grExport, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvExport, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frmTipofileE, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frmTipofileE.ResumeLayout(False)
    Me.frmTipofileE.PerformLayout()
    CType(Me.edSepFieldE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opDelimE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFixedE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSilentE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckRimuoviVbcrlf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckShowLogE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNomeFileE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckGenTracciatoE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tpImport.ResumeLayout(False)
    CType(Me.NtsPanel2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel2.ResumeLayout(False)
    Me.NtsPanel2.PerformLayout()
    CType(Me.ckTipoRigaI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQtaTagliaI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edQtaMatrI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTagliaI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMatricI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTagliaI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMatricI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckExecuteI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grImport, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvImport, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frmTipofileI, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frmTipofileI.ResumeLayout(False)
    Me.frmTipofileI.PerformLayout()
    CType(Me.edDecSepI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSepFieldI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opDelimI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opFixedI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckProvvI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckScontiI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSilentI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPrezziI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNomeFileI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDelFileI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckShowLogI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckIgnore1rkI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnProfilo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnProfilo.ResumeLayout(False)
    Me.pnProfilo.PerformLayout()
    Me.ResumeLayout(False)

  End Sub

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If
    Me.GctlTipoDoc = ""

    InitializeComponent()
    Me.MinimumSize = Me.Size

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGTERM", "BEMGDOCU", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 130086825801304425, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleDocu = CType(oTmp, CLEMGDOCU)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGTERM", strRemoteServer, strRemotePort)
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleDocu.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttCmbI, dttCmbE As New DataTable
    Try
      '--------------------------------------------------------------------------------------------------------------
      cbImpostaFile.NTSSetParam(oApp.Tr(Me, 128759950861372722, "Impostazione File"))
      '--------------------------------------------------------------------------------------------------------------
      edNomeFile.NTSSetParam(oMenu, oApp.Tr(Me, 128571233823125000, "Nome file"), 0, True)
      edDecSep.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822343750, "Separatore decimale"), 1, True)
      edSepField.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822656250, "Separatore di campo"), 1, True)
      '--------------------------------------------------------------------------------------------------------------
      opFixed.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822968750, "Posizioni fisse"), "F")
      opDelim.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822812500, "Separat. di campo"), "S")
      opCodartBarcodeCodarfo.NTSSetParam(oMenu, oApp.Tr(Me, 129161457518144922, "Cod. articolo/Barcode/Cod. art. C/F."), "X")
      opCodart.NTSSetParam(oMenu, oApp.Tr(Me, 128775259974828000, "Cod. articolo"), "C")
      opBarcode.NTSSetParam(oMenu, oApp.Tr(Me, 128571233821562500, "Barcode"), "B")
      opCodarfo.NTSSetParam(oMenu, oApp.Tr(Me, 128571233821406250, "Cod. art. C/F."), "A")
      '--------------------------------------------------------------------------------------------------------------
      ckShowLog.NTSSetParam(oMenu, oApp.Tr(Me, 128571233821718750, "Visualizza file log con gli errori di importazione"), "-1", "0")
      ckIgnore1rk.NTSSetParam(oMenu, oApp.Tr(Me, 128571233821875000, "Ignora primo record"), "-1", "0")
      ckDelFile.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822031250, "Elimina il file a fine acquisizione"), "-1", "0")
      ckSilent.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822187500, "Lancia il comando in modalità nascosta"), "-1", "0")
      ckBarcodeConfez.NTSSetParam(oMenu, oApp.Tr(Me, 128571233820000000, "Rileva la quantità nel campo colli"), "-1", "0")
      ckExecute.NTSSetParam(oMenu, oApp.Tr(Me, 128571233823437500, "Esegui il comando"), "-1", "0")
      '--------------------------------------------------------------------------------------------------------------
      ckDescrizione.NTSSetParam(oMenu, oApp.Tr(Me, 128571233820625000, "Descrizione"), "-1", "0")
      ckDesint.NTSSetParam(oMenu, oApp.Tr(Me, 129037913420961597, "Descrizione interna"), "-1", "0")
      ckQuant.NTSSetParam(oMenu, oApp.Tr(Me, 128571233820468750, "Quantità (*)"), "-1", "0")
      ckPrezzo.NTSSetParam(oMenu, oApp.Tr(Me, 128571233820312500, "Prezzo"), "-1", "0")
      ckSconto1.NTSSetParam(oMenu, oApp.Tr(Me, 128775271769980000, "Sconto 1"), "S", "N")
      ckSconto2.NTSSetParam(oMenu, oApp.Tr(Me, 128775271768420000, "Sconto 2"), "S", "N")
      ckSconto3.NTSSetParam(oMenu, oApp.Tr(Me, 128775271766860000, "Sconto 3"), "S", "N")
      ckSconto4.NTSSetParam(oMenu, oApp.Tr(Me, 128775271765300000, "Sconto 4"), "S", "N")
      ckSconto5.NTSSetParam(oMenu, oApp.Tr(Me, 128775271763740000, "Sconto 5"), "S", "N")
      ckSconto6.NTSSetParam(oMenu, oApp.Tr(Me, 128775271762180000, "Sconto 6"), "S", "N")
      ckDatcons.NTSSetParam(oMenu, oApp.Tr(Me, 129037914812103802, "Data consegna"), "S", "N")
      '--------------------------------------------------------------------------------------------------------------
      edPosArt.NTSSetParam(oMenu, oApp.Tr(Me, 128571233819375000, "Posizione articolo"), "0", 3, 0, 255)
      edPosDescr.NTSSetParam(oMenu, oApp.Tr(Me, 128571233819843750, "Posizione descrizione"), "0", 3, 0, 255)
      edPosDesint.NTSSetParam(oMenu, oApp.Tr(Me, 129037917377728802, "Posizione descrizione interna"), "0", 3, 0, 255)
      edPosPre.NTSSetParam(oMenu, oApp.Tr(Me, 128571233819531250, "Posizione prezzo"), "0", 3, 0, 255)
      edPosQua.NTSSetParam(oMenu, oApp.Tr(Me, 128571233819687500, "Posizione quantità"), "0", 3, 0, 255)
      edPosSc1.NTSSetParam(oMenu, oApp.Tr(Me, 128775271769824000, "Posizione sconto 1"), "0", 3, 0, 255)
      edPosSc2.NTSSetParam(oMenu, oApp.Tr(Me, 128775271768264000, "Posizione sconto 2"), "0", 3, 0, 255)
      edPosSc3.NTSSetParam(oMenu, oApp.Tr(Me, 128775271766704000, "Posizione sconto 3"), "0", 3, 0, 255)
      edPosSc4.NTSSetParam(oMenu, oApp.Tr(Me, 128775271765144000, "Posizione sconto 4"), "0", 3, 0, 255)
      edPosSc5.NTSSetParam(oMenu, oApp.Tr(Me, 128775271763584000, "Posizione sconto 5"), "0", 3, 0, 255)
      edPosSc6.NTSSetParam(oMenu, oApp.Tr(Me, 128775271762024000, "Posizione sconto 6"), "0", 3, 0, 255)
      edPosDatcons.NTSSetParam(oMenu, oApp.Tr(Me, 129037918707103802, "Posizione data consegna"), "0", 3, 0, 255)
      '--------------------------------------------------------------------------------------------------------------
      edArtDa.NTSSetParam(oMenu, oApp.Tr(Me, 128571233818593750, "Articoo DA"), "0", 4, 0, 9999)
      edDescrDa.NTSSetParam(oMenu, oApp.Tr(Me, 128571233817968750, "Descrizione DA"), "0", 4, 0, 9999)
      edDesintDa.NTSSetParam(oMenu, oApp.Tr(Me, 129037916537416302, "Descrizione interna DA"), "0", 4, 0, 9999)
      edQuantDa.NTSSetParam(oMenu, oApp.Tr(Me, 128571233817343750, "Quantità DA"), "0", 4, 0, 9999)
      edPreDa.NTSSetParam(oMenu, oApp.Tr(Me, 128571233816718750, "Prezzo DA"), "0", 4, 0, 9999)
      edSc1Da.NTSSetParam(oMenu, oApp.Tr(Me, 128775271769512000, "Sconto 1 DA"), "0", 4, 0, 9999)
      edSc2Da.NTSSetParam(oMenu, oApp.Tr(Me, 128775271767952000, "Sconto 2 DA"), "0", 4, 0, 9999)
      edSc3Da.NTSSetParam(oMenu, oApp.Tr(Me, 128775271766392000, "Sconto 3 DA"), "0", 4, 0, 9999)
      edSc4Da.NTSSetParam(oMenu, oApp.Tr(Me, 128775271764832000, "Sconto 4 DA"), "0", 4, 0, 9999)
      edSc5Da.NTSSetParam(oMenu, oApp.Tr(Me, 128775271763272000, "Sconto 5 DA"), "0", 4, 0, 9999)
      edSc6Da.NTSSetParam(oMenu, oApp.Tr(Me, 128775271761712000, "Sconto 6 DA"), "0", 4, 0, 9999)
      edDatconsDa.NTSSetParam(oMenu, oApp.Tr(Me, 129037919355541302, "Data consegna DA"), "0", 4, 0, 9999)
      '--------------------------------------------------------------------------------------------------------------
      edArtA.NTSSetParam(oMenu, oApp.Tr(Me, 128571233818437500, "Articolo A"), "0", 4, 0, 9999)
      edDescrA.NTSSetParam(oMenu, oApp.Tr(Me, 128571233817812500, "Descrizione A"), "0", 4, 0, 9999)
      edDesintA.NTSSetParam(oMenu, oApp.Tr(Me, 129037920091791302, "Descrizione interna A"), "0", 4, 0, 9999)
      edPreA.NTSSetParam(oMenu, oApp.Tr(Me, 128571233816562500, "Prezzo A"), "0", 4, 0, 9999)
      edQuantA.NTSSetParam(oMenu, oApp.Tr(Me, 128571233817187500, "Quantità A"), "0", 4, 0, 9999)
      edSc1A.NTSSetParam(oMenu, oApp.Tr(Me, 128775271769356000, "Sconto 1 A"), "0", 4, 0, 9999)
      edSc2A.NTSSetParam(oMenu, oApp.Tr(Me, 128775271767796000, "Sconto 2 A"), "0", 4, 0, 9999)
      edSc3A.NTSSetParam(oMenu, oApp.Tr(Me, 128775271766236000, "Sconto 3 A"), "0", 4, 0, 9999)
      edSc4A.NTSSetParam(oMenu, oApp.Tr(Me, 128775271764676000, "Sconto 4 A"), "0", 4, 0, 9999)
      edSc5A.NTSSetParam(oMenu, oApp.Tr(Me, 128775271763116000, "Sconto 5 A"), "0", 4, 0, 9999)
      edSc6A.NTSSetParam(oMenu, oApp.Tr(Me, 128775271761556000, "Sconto 6 A"), "0", 4, 0, 9999)
      edDatconsA.NTSSetParam(oMenu, oApp.Tr(Me, 129037920469760052, "Data consegna A"), "0", 4, 0, 9999)
      '--------------------------------------------------------------------------------------------------------------
      edDivPrezzo.NTSSetParam(oMenu, oApp.Tr(Me, 128571233815937500, "Dividi prezzo"), "0", 5, 1, 10000)
      edDivQuant.NTSSetParam(oMenu, oApp.Tr(Me, 128571233816093750, "Dividi quantità"), "0", 5, 1, 10000)
      edDivSc1.NTSSetParam(oMenu, oApp.Tr(Me, 128775271768888000, "Dividi sconto 1"), "0", 5, 1, 10000)
      edDivSc2.NTSSetParam(oMenu, oApp.Tr(Me, 128775271767328000, "Dividi sconto 2"), "0", 5, 1, 10000)
      edDivSc3.NTSSetParam(oMenu, oApp.Tr(Me, 128775271765768000, "Dividi sconto 3"), "0", 5, 1, 10000)
      edDivSc4.NTSSetParam(oMenu, oApp.Tr(Me, 128775271764208000, "Dividi sconto 4"), "0", 5, 1, 10000)
      edDivSc5.NTSSetParam(oMenu, oApp.Tr(Me, 128775271762648000, "Dividi sconto 5"), "0", 5, 1, 10000)
      edDivSc6.NTSSetParam(oMenu, oApp.Tr(Me, 128775271761088000, "Dividi sconto 6"), "0", 5, 1, 10000)
      '--------------------------------------------------------------------------------------------------------------
      grvExport.NTSSetParam(oMenu, oApp.Tr(Me, 129247180876718750, "Esportazione"))
      grvImport.NTSSetParam(oMenu, oApp.Tr(Me, 129247180890937500, "Importazione"))
      ckExecuteE.NTSSetParam(oMenu, oApp.Tr(Me, 129247180923593750, "Esegui il comando"), "S", "N")
      ckShowLogE.NTSSetParam(oMenu, oApp.Tr(Me, 129247180923906250, "Visualizza file log con gli errori di importazione"), "S", "N")
      ckGenTracciatoE.NTSSetParam(oMenu, oApp.Tr(Me, 129247180924218750, "Genera tracciato"), "S", "N")
      edNomeFileE.NTSSetParam(oMenu, oApp.Tr(Me, 129247180924531250, "Nome file"), 0)
      ckSilentE.NTSSetParam(oMenu, oApp.Tr(Me, 129247180924687500, "Lancia il comando in modalità nascosta"), "S", "N")
      edSepFieldE.NTSSetParam(oMenu, oApp.Tr(Me, 129247180925312500, "Separatore"), 1)
      opDelimE.NTSSetParam(oMenu, oApp.Tr(Me, 129247180925468750, "Separat. di campo"), "S")
      opFixedE.NTSSetParam(oMenu, oApp.Tr(Me, 129247180925625000, "Posizioni fisse"), "F")
      ckProvvI.NTSSetParam(oMenu, oApp.Tr(Me, 129247180925937500, "Ricalcola provvigioni"), "S", "N")
      ckScontiI.NTSSetParam(oMenu, oApp.Tr(Me, 129247180926093750, "Ricalcola sconti"), "S", "N")
      ckPrezziI.NTSSetParam(oMenu, oApp.Tr(Me, 129247180926250000, "Ricalcola prezzi"), "S", "N")
      ckExecuteI.NTSSetParam(oMenu, oApp.Tr(Me, 129247180926406250, "Esegui il comando"), "S", "N")
      ckShowLogI.NTSSetParam(oMenu, oApp.Tr(Me, 129247180926718750, "Visualizza file log con gli errori di importazione"), "S", "N")
      ckIgnore1rkI.NTSSetParam(oMenu, oApp.Tr(Me, 129247180927031250, "Ignora primo record"), "S", "N")
      ckDelFileI.NTSSetParam(oMenu, oApp.Tr(Me, 129247180927343750, "Elimina il file a fine acquisizione"), "S", "N")
      edNomeFileI.NTSSetParam(oMenu, oApp.Tr(Me, 129247180927500000, "Nome file"), 0)
      ckSilentI.NTSSetParam(oMenu, oApp.Tr(Me, 129247180927656250, "Lancia il comando in modalità nascosta"), "S", "N")
      edDecSepI.NTSSetParam(oMenu, oApp.Tr(Me, 129247180928125000, "Separatore decimale"), 1)
      edSepFieldI.NTSSetParam(oMenu, oApp.Tr(Me, 129247180928437500, "Separatore"), 1)
      opDelimI.NTSSetParam(oMenu, oApp.Tr(Me, 129247180928593750, "Separat. di campo"), "S")
      opFixedI.NTSSetParam(oMenu, oApp.Tr(Me, 129247180928750000, "Posizioni fisse"), "F")
      ckTipoRigaE.NTSSetParam(oMenu, oApp.Tr(Me, 129253138528125000, "Inserisci tipo riga"), "S", "N")
      edQtaTagliaE.NTSSetParam(oMenu, oApp.Tr(Me, 129253138528281250, "Lunghezza quantità taglia"), "0", 4, 0, 9999)
      edQtaMatrE.NTSSetParam(oMenu, oApp.Tr(Me, 129253138528437500, "Lunghezza quantità matricole"), "0", 4, 0, 9999)
      edTagliaE.NTSSetParam(oMenu, oApp.Tr(Me, 129253138528593750, "Lunghezza taglia"), "0", 4, 0, 9999)
      edMatrE.NTSSetParam(oMenu, oApp.Tr(Me, 129253138528750000, "Lunghezza matricole"), "0", 4, 0, 9999)
      ckTagliaE.NTSSetParam(oMenu, oApp.Tr(Me, 129253138529218750, "Esporta taglia"), "S", "N")
      ckMatricE.NTSSetParam(oMenu, oApp.Tr(Me, 129253138529687500, "Esporta matricole"), "S", "N")
      ckTipoRigaI.NTSSetParam(oMenu, oApp.Tr(Me, 129253138532031250, "Usa tipo riga"), "S", "N")
      edQtaTagliaI.NTSSetParam(oMenu, oApp.Tr(Me, 129253138532187500, "Lunghezza quantità taglia"), "0", 4, 0, 9999)
      edQtaMatrI.NTSSetParam(oMenu, oApp.Tr(Me, 129253138532343750, "Lunghezza quantità matricole"), "0", 4, 0, 9999)
      edTagliaI.NTSSetParam(oMenu, oApp.Tr(Me, 129253138532500000, "Lunghezza taglia"), "0", 4, 0, 9999)
      edMatricI.NTSSetParam(oMenu, oApp.Tr(Me, 129253138532656250, "Lunghezza matricole"), "0", 4, 0, 9999)
      ckTagliaI.NTSSetParam(oMenu, oApp.Tr(Me, 129253138533125000, "Importa taglia"), "S", "N")
      ckMatricI.NTSSetParam(oMenu, oApp.Tr(Me, 129253138533593750, "Importa matricole"), "S", "N")

      CaricaCombo(dttCmbI, dttCmbE)

      xx_nomee.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129247797403107666, "Colonna"), dttCmbE, "val", "cod")
      xx_pose.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129247797403263916, "Posizione"), "0", 6, 0, 999999)
      xx_lune.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129247797403420166, "Lunghezza"), "0", 6, 0, 999999)
      xx_dive.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129247797403576416, "divedi per"), "0", 6, 1, 999999)
      xx_nomei.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129247797406232666, "Colonna"), dttCmbI, "val", "cod")
      xx_posi.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129247797406388916, "Posizione"), "0", 6, 0, 999999)
      xx_luni.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129247797406545166, "Lunghezza"), "0", 6, 0, 999999)
      xx_divi.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129247797406701416, "Dividi per"), "0", 6, 1, 999999)

      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub CaricaCombo(ByRef dttCmbI As DataTable, ByRef dttCmbE As DataTable)
    Try
      With dttCmbI
        .Columns.Add("cod", GetType(String))
        .Columns.Add("val", GetType(String))
        .Rows.Add(New Object() {"   ", "(Nessuno)"})
        .Rows.Add(New Object() {"art", "Articolo"})
        .Rows.Add(New Object() {"acf", "Art. C/F"})
        .Rows.Add(New Object() {"bar", "Barcode"})
        .Rows.Add(New Object() {"all", "Art. o Barcode o Art. C/F"})
        .Rows.Add(New Object() {"des", "Descrizione"})
        .Rows.Add(New Object() {"dei", "Desc. interna"})
        .Rows.Add(New Object() {"unm", "Unità di misura"})
        .Rows.Add(New Object() {"col", "Colli"})
        .Rows.Add(New Object() {"qta", "Quantità"})
        .Rows.Add(New Object() {"prz", "Prezzo"})
        .Rows.Add(New Object() {"sc1", "Sconto 1"})
        .Rows.Add(New Object() {"sc2", "Sconto 2"})
        .Rows.Add(New Object() {"sc3", "Sconto 3"})
        .Rows.Add(New Object() {"sc4", "Sconto 4"})
        .Rows.Add(New Object() {"sc5", "Sconto 5"})
        .Rows.Add(New Object() {"sc6", "Sconto 6"})
        .Rows.Add(New Object() {"pr1", "Perc. provvigioni 1"})
        .Rows.Add(New Object() {"pr2", "Perc. provvigioni 2"})
        .Rows.Add(New Object() {"ma1", "Magazzino 1"})
        .Rows.Add(New Object() {"ma2", "Magazzino 2"})
        .Rows.Add(New Object() {"not", "Note"})
        .Rows.Add(New Object() {"cau", "Causale"})
        .Rows.Add(New Object() {"cnp", "Contropartita"})
        .Rows.Add(New Object() {"iva", "Codice iva"})
        .Rows.Add(New Object() {"str", "Stampa riga"})
        .Rows.Add(New Object() {"com", "Commessa"})
        .Rows.Add(New Object() {"lot", "Lotto"})
        .Rows.Add(New Object() {"cda", "Centro di CA"})
        .Rows.Add(New Object() {"cca", "Conto contropartita CA"})
        .Rows.Add(New Object() {"mi1", "Misura 1"})
        .Rows.Add(New Object() {"mi2", "Misura 2"})
        .Rows.Add(New Object() {"mi3", "Misura 3"})
        .Rows.Add(New Object() {"fas", "Fase"})
        .Rows.Add(New Object() {"ubi", "Ubicazione"})
        .Rows.Add(New Object() {"prn", "Prezzo netto"})
        .Rows.Add(New Object() {"pro", "Promozione"})
        .Rows.Add(New Object() {"dic", "Data inizio competenza"})
        .Rows.Add(New Object() {"dif", "Data fine competenza"})
        'Solo ordini
        If oCallParams.strNomProg = "BNORGSOR" Then
          .Rows.Add(New Object() {"dtc", "Data consegna"})
          .Rows.Add(New Object() {"con", "Confermato"})
          .Rows.Add(New Object() {"ril", "Rilasciato"})
        End If
      End With

      With dttCmbE
        .Columns.Add("cod", GetType(String))
        .Columns.Add("val", GetType(String))
        .Rows.Add(New Object() {"art", "Articolo"})
        .Rows.Add(New Object() {"des", "Descrizione"})
        .Rows.Add(New Object() {"dei", "Desc. interna"})
        .Rows.Add(New Object() {"unm", "Unità di misura"})
        .Rows.Add(New Object() {"col", "Colli"})
        .Rows.Add(New Object() {"qta", "Quantità"})
        .Rows.Add(New Object() {"prz", "Prezzo"})
        .Rows.Add(New Object() {"sc1", "Sconto 1"})
        .Rows.Add(New Object() {"sc2", "Sconto 2"})
        .Rows.Add(New Object() {"sc3", "Sconto 3"})
        .Rows.Add(New Object() {"sc4", "Sconto 4"})
        .Rows.Add(New Object() {"sc5", "Sconto 5"})
        .Rows.Add(New Object() {"sc6", "Sconto 6"})
        .Rows.Add(New Object() {"pr1", "Perc. provvigioni 1"})
        .Rows.Add(New Object() {"pr2", "Perc. provvigioni 2"})
        .Rows.Add(New Object() {"ma1", "Magazzino 1"})
        .Rows.Add(New Object() {"ma2", "Magazzino 2"})
        .Rows.Add(New Object() {"not", "Note"})
        .Rows.Add(New Object() {"cau", "Causale"})
        .Rows.Add(New Object() {"cnp", "Contropartita"})
        .Rows.Add(New Object() {"iva", "Codice iva"})
        .Rows.Add(New Object() {"str", "Stampa riga"})
        .Rows.Add(New Object() {"com", "Commessa"})
        .Rows.Add(New Object() {"lot", "Lotto"})
        .Rows.Add(New Object() {"cda", "Centro di CA"})
        .Rows.Add(New Object() {"cca", "Conto contropartita CA"})
        .Rows.Add(New Object() {"mi1", "Misura 1"})
        .Rows.Add(New Object() {"mi2", "Misura 2"})
        .Rows.Add(New Object() {"mi3", "Misura 3"})
        .Rows.Add(New Object() {"fas", "Fase"})
        .Rows.Add(New Object() {"ubi", "Ubicazione"})
        .Rows.Add(New Object() {"prn", "Prezzo netto"})
        .Rows.Add(New Object() {"pro", "Promozione"})
        .Rows.Add(New Object() {"dic", "Data inizio competenza"})
        .Rows.Add(New Object() {"dif", "Data fine competenza"})
        'Solo ordini
        If oCallParams.strNomProg = "BNORGSOR" Then
          .Rows.Add(New Object() {"dtc", "Data consegna"})
          .Rows.Add(New Object() {"con", "Confermato"})
          .Rows.Add(New Object() {"ril", "Rilasciato"})
        End If
      End With

    Catch ex As Exception
      '---------------------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRMMGTERM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Predispongo i controlli
      '--------------------------------------------------------------------------------------------------------------
      InitControls()

      tcImport.SelectedTabPageIndex = 0
      '--------------------------------------------------------------------------------------------------------------
      Dim strDateSep As String = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.DateSeparator
      lbSeparatoriDate.Text = oApp.Tr(Me, 130086830881148175, _
      "Formati: gg|" & strDateSep & "|mm|" & strDateSep & "|aa" & vbCrLf & _
        "gg|" & strDateSep & "|mm|" & strDateSep & "|aaaa")
      '--------------------------------------------------------------------------------------------------------------
      '--- Sempre alla fine di questa funzione: applico le regole della gctl
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub FRMMGTERM_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      '--------------------------------------------------------------------------------------------------------------
      bOnLoading = True
      '--------------------------------------------------------------------------------------------------------------
      CaricaComboLayout("")
      '--------------------------------------------------------------------------------------------------------------
      oCleDocu.strTermKey = cbImpostaFile.SelectedValue
      '--------------------------------------------------------------------------------------------------------------
      CaricaMaschera()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
      bOnLoading = False
    End Try
  End Sub
#End Region

#Region "Eventi"
  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Salva() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      Me.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdCommand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCommand.Click
    Try
      Dim oDlg As New NTSOpenFileDialog
      oDlg.Multiselect = False
      oDlg.ShowHelp = False
      oDlg.Title = oApp.Tr(Me, 129247728571380702, "File da eseguire")
      oDlg.DefaultExt = "EXE"
      oDlg.Filter = "Applicazioni (*.exe)|*.exe|Tutti i file (*.*)|*.*"
      oDlg.InitialDirectory = oApp.CmbDir
      oDlg.FileName = ""
      oDlg.oMenu = oMenu
      oDlg.ShowDialog()
      If oDlg.FileName <> "" Then lbCommand.Text = oDlg.FileName
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdNomeFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNomeFile.Click
    Try
      Dim oDlg As New NTSOpenFileDialog
      oDlg.Multiselect = False
      oDlg.ShowHelp = False
      oDlg.Title = oApp.Tr(Me, 129247728792619332, "File da acquisire")
      oDlg.DefaultExt = "TXT"
      oDlg.Filter = "File di testo (*.txt)|*.txt|Tutti i file (*.*)|*.*"
      oDlg.InitialDirectory = oApp.AscDir
      oDlg.FileName = ""
      oDlg.oMenu = oMenu
      oDlg.ShowDialog()
      If oDlg.FileName <> "" Then edNomeFile.Text = oDlg.FileName
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdCommandI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCommandI.Click
    Try
      Dim oDlg As New NTSOpenFileDialog
      oDlg.Multiselect = False
      oDlg.ShowHelp = False
      oDlg.Title = oApp.Tr(Me, 129247728615152646, "File da eseguire")
      oDlg.DefaultExt = "EXE"
      oDlg.Filter = "Applicazioni (*.exe)|*.exe|Tutti i file (*.*)|*.*"
      oDlg.InitialDirectory = oApp.CmbDir
      oDlg.FileName = ""
      oDlg.oMenu = oMenu
      oDlg.ShowDialog()
      If oDlg.FileName <> "" Then lbCommandI.Text = oDlg.FileName
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdNomeFileI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNomeFileI.Click
    Try
      Dim oDlg As New NTSOpenFileDialog
      oDlg.Multiselect = False
      oDlg.ShowHelp = False
      oDlg.Title = oApp.Tr(Me, 129247729439682852, "File da acquisire")
      oDlg.DefaultExt = "TXT"
      oDlg.Filter = "File di testo (*.txt)|*.txt|Tutti i file (*.*)|*.*"
      oDlg.InitialDirectory = oApp.AscDir
      oDlg.FileName = ""
      oDlg.oMenu = oMenu
      oDlg.ShowDialog()
      If oDlg.FileName <> "" Then edNomeFileI.Text = oDlg.FileName
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdCancellaI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancellaI.Click
    Try
      If Not grvImport.NTSGetCurrentDataRow Is Nothing Then
        grvImport.NTSGetCurrentDataRow.Delete()
      End If
    Catch ex As Exception
      '--------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdCommandE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCommandE.Click
    Try
      Dim oDlg As New NTSOpenFileDialog
      oDlg.Multiselect = False
      oDlg.ShowHelp = False
      oDlg.Title = oApp.Tr(Me, 128571255124531250, "File da eseguire")
      oDlg.DefaultExt = "EXE"
      oDlg.Filter = "Applicazioni (*.exe)|*.exe|Tutti i file (*.*)|*.*"
      oDlg.InitialDirectory = oApp.CmbDir
      oDlg.FileName = ""
      oDlg.oMenu = oMenu
      oDlg.ShowDialog()
      If oDlg.FileName <> "" Then lbCommandE.Text = oDlg.FileName
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdNomeFileE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNomeFileE.Click
    Try
      Dim oDlg As New NTSOpenFileDialog
      oDlg.Multiselect = False
      oDlg.ShowHelp = False
      oDlg.Title = oApp.Tr(Me, 130086829084741925, "File da acquisire")
      oDlg.DefaultExt = "TXT"
      oDlg.Filter = "File di testo (*.txt)|*.txt|Tutti i file (*.*)|*.*"
      oDlg.InitialDirectory = oApp.AscDir
      oDlg.FileName = ""
      oDlg.oMenu = oMenu
      oDlg.ShowDialog()
      If oDlg.FileName <> "" Then edNomeFileE.Text = oDlg.FileName
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdCancellaE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancellaE.Click
    Try
      If Not grvExport.NTSGetCurrentDataRow Is Nothing Then
        grvExport.NTSGetCurrentDataRow.Delete()
      End If
    Catch ex As Exception
      '--------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdAggiungi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAggiungi.Click
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Salva() = False Then Return
      '--------------------------------------------------------------------------------------------------------------
Richiedi:
      '--------------------------------------------------------------------------------------------------------------
      strTmp = oApp.InputBoxNew(oApp.Tr(Me, 130438354805533977, _
        "Indicare il nome del nuovo layout (senza spazi)" & vbCrLf & _
        "(max 10 caratteri compresi fra A-Z, a-z, 0-9):" & vbCrLf))
      '--------------------------------------------------------------------------------------------------------------
      If strTmp.Trim = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      If strTmp.Length > 10 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130438357996758353, "Attenzione!" & vbCrLf & _
          "Il nome del nuovo layout non deve superare i 10 caratteri."))
        GoTo Richiedi
      End If
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 1 To strTmp.Length
        If Asc(Mid(strTmp, i, 1)) = 32 Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130438389974528425, "Attenzione!" & vbCrLf & _
            "Il nome del nuovo layout NON può contenere spazi."))
          GoTo Richiedi
        End If
      Next
      For i As Integer = 1 To strTmp.Length
        If (Not (Asc(Mid(strTmp, i, 1)) >= 65 And Asc(Mid(strTmp, i, 1)) <= 90)) And _
           (Not (Asc(Mid(strTmp, i, 1)) >= 97 And Asc(Mid(strTmp, i, 1)) <= 122)) And _
           (Not (Asc(Mid(strTmp, i, 1)) >= 48 And Asc(Mid(strTmp, i, 1)) <= 57)) Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130438361590900688, "Attenzione!" & vbCrLf & _
            "Il nome del nuovo layout può contenere SOLO caratteri compresi fra:" & vbCrLf & _
            " . A - Z" & vbCrLf & _
            " . a - z" & vbCrLf & _
            " . 0 - 9"))
          GoTo Richiedi
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
      strTmp = "Terminale_" & strTmp
      '--------------------------------------------------------------------------------------------------------------
      If oCleDocu.ImpostazioneFileGiaEsistente(oCallParams.strNomProg, strTmp) = True Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130438366359074360, "Attenzione!" & vbCrLf & _
          "Esiste già un layout file con questo nome." & vbCrLf & _
          "Ripetere."))
        GoTo Richiedi
      End If
      '--------------------------------------------------------------------------------------------------------------
      bOnLoading = True
      oCleDocu.strTermKey = strTmp
      CaricaComboLayout(strTmp)
      CaricaMaschera()
      bOnLoading = False
      cbImpostaFile.SelectedValue = strTmp
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      bOnLoading = False
    End Try
  End Sub
  Public Overridable Sub cmdAggiorna_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAggiorna.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      Salva()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdCancella_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancella.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      If cbImpostaFile.SelectedValue.ToLower = "terminale" Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130438375936652917, "Attenzione!" & vbCrLf & _
          "Il layout '(Predefinito)' non può essere eliminato."))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130438378110868306, _
        "Confermare l'eliminazione del layout '|" & cbImpostaFile.SelectedItem.ToString & "|'?")) = Windows.Forms.DialogResult.No Then
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCleDocu.DeleteImpostazioniFile(oCallParams.strNomProg, cbImpostaFile.SelectedValue) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      cbImpostaFile.SelectedValue = "Terminale"
      CaricaComboLayout("")
      CaricaMaschera()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub ckMatricI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckMatricI.CheckedChanged
    Try
      If ckMatricI.Checked Then
        edMatricI.Enabled = opFixedI.Checked
        edQtaMatrI.Enabled = opFixedI.Checked
        ckTipoRigaI.Checked = True
        ckTipoRigaI.Enabled = False
      Else
        edMatricI.Enabled = False
        edQtaMatrI.Enabled = False
        If Not ckTagliaI.Checked Then
          ckTipoRigaI.Checked = False
          ckTipoRigaI.Enabled = True
        End If
      End If
    Catch ex As Exception
      '--------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTagliaI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTagliaI.CheckedChanged
    Try
      If ckTagliaI.Checked Then
        edTagliaI.Enabled = opFixedI.Checked
        edQtaTagliaI.Enabled = opFixedI.Checked
        ckTipoRigaI.Checked = True
        ckTipoRigaI.Enabled = False
      Else
        edTagliaI.Enabled = False
        edQtaTagliaI.Enabled = False
        If Not ckMatricI.Checked Then
          ckTipoRigaI.Checked = False
          ckTipoRigaI.Enabled = True
        End If
      End If
    Catch ex As Exception
      '--------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckMatricE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckMatricE.CheckedChanged
    Try
      If ckMatricE.Checked Then
        edMatrE.Enabled = opFixedE.Checked
        edQtaMatrE.Enabled = opFixedE.Checked
        ckTipoRigaE.Checked = True
        ckTipoRigaE.Enabled = False
      Else
        edMatrE.Enabled = False
        edQtaMatrE.Enabled = False
        If Not ckTagliaE.Checked Then
          ckTipoRigaE.Checked = False
          ckTipoRigaE.Enabled = True
        End If
      End If
    Catch ex As Exception
      '--------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckTagliaE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTagliaE.CheckedChanged
    Try
      If ckTagliaE.Checked Then
        edTagliaE.Enabled = opFixedE.Checked
        edQtaTagliaE.Enabled = opFixedE.Checked
        ckTipoRigaE.Checked = True
        ckTipoRigaE.Enabled = False
      Else
        edTagliaE.Enabled = False
        edQtaTagliaE.Enabled = False
        If Not ckMatricE.Checked Then
          ckTipoRigaE.Checked = False
          ckTipoRigaE.Enabled = True
        End If
      End If
    Catch ex As Exception
      '--------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edSepFieldI_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSepFieldI.Validated
    Try
      If edSepFieldI.Text = "|" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129284086325858519, "Il Carattere inserito non è valido, verrà sostituito con §"))
        edSepFieldI.Text = "§"
      End If
    Catch ex As Exception
      '---------------------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edSepFieldE_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSepFieldE.Validated
    Try
      If edSepFieldE.Text = "|" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129284086855808323, "Il Carattere inserito non è valido, verrà sostituito con §"))
        edSepFieldE.Text = "§"
      End If
    Catch ex As Exception
      '---------------------------------------------------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbImpostaFile_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbImpostaFile.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      oCleDocu.strTermKey = cbImpostaFile.SelectedValue
      '--------------------------------------------------------------------------------------------------------------
      CaricaMaschera()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "per bloccare/sbloccare i controlli a video"
  Public Overridable Sub ckExecute_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckExecute.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opFixed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opFixed.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opCodart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opCodart.CheckedChanged
    Try
      If opCodart.Checked Then ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opBarcode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opBarcode.CheckedChanged
    Try
      If opBarcode.Checked Then ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub opCodarfo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opCodarfo.CheckedChanged
    Try
      If opCodarfo.Checked Then ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckDescrizione_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckDescrizione.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckDesint_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckDesint.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub ckQuant_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckQuant.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckPrezzo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckPrezzo.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckSconto1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSconto1.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckSconto2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSconto2.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckSconto3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSconto3.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckSconto4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSconto4.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckSconto5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSconto5.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckSconto6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSconto6.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckDatcons_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckDatcons.CheckedChanged
    Try
      ImpostaCampi()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function ImpostaCampi() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bInLoad Then Return True
      '--------------------------------------------------------------------------------------------------------------
      If ckExecute.Checked Then
        ckSilent.Enabled = True
        cmdCommand.Enabled = True
      Else
        ckSilent.Enabled = False
        cmdCommand.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If opBarcode.Checked Then
        ckBarcodeConfez.Enabled = True
      Else
        ckBarcodeConfez.Checked = False
        ckBarcodeConfez.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If opFixed.Checked Then
        edPosArt.Text = "0"
        edPosArt.Enabled = False
        edArtDa.Enabled = True
        edArtA.Enabled = True
        '--- Descrizione
        If ckDescrizione.Checked Then
          edPosDescr.Text = "0"
          edPosDescr.Enabled = False
          edDescrDa.Enabled = True
          edDescrA.Enabled = True
        Else
          edPosDescr.Text = "0"
          edPosDescr.Enabled = False
          edDescrDa.Text = "0"
          edDescrDa.Enabled = False
          edDescrA.Text = "0"
          edDescrA.Enabled = False
        End If
        '--- Descrizione interna
        If ckDesint.Checked Then
          edPosDesint.Text = "0"
          edPosDesint.Enabled = False
          edDesintDa.Enabled = True
          edDesintA.Enabled = True
        Else
          edPosDesint.Text = "0"
          edPosDesint.Enabled = False
          edDesintDa.Text = "0"
          edDesintDa.Enabled = False
          edDesintA.Text = "0"
          edDesintA.Enabled = False
        End If
        'quant
        If ckQuant.Checked Then
          edPosQua.Text = "0"
          edPosQua.Enabled = False
          edQuantDa.Enabled = True
          edQuantA.Enabled = True
          edDivQuant.Enabled = True
        Else
          edPosQua.Text = "0"
          edPosQua.Enabled = False
          edQuantDa.Text = "0"
          edQuantDa.Enabled = False
          edQuantA.Text = "0"
          edQuantA.Enabled = False
          edDivQuant.Enabled = False
        End If
        'Prezzi
        If ckPrezzo.Checked Then
          edPosPre.Text = "0"
          edPosPre.Enabled = False
          edPreDa.Enabled = True
          edPreA.Enabled = True
          edDivPrezzo.Enabled = True
        Else
          edPosPre.Text = "0"
          edPosPre.Enabled = False
          edPreDa.Text = "0"
          edPreDa.Enabled = False
          edPreA.Text = "0"
          edPreA.Enabled = False
          edDivPrezzo.Enabled = False
        End If
        'sconti
        If ckSconto1.Checked Then
          edPosSc1.Text = "0"
          edPosSc1.Enabled = False
          edSc1Da.Enabled = True
          edSc1A.Enabled = True
          edDivSc1.Enabled = True
        Else
          edPosSc1.Text = "0"
          edPosSc1.Enabled = False
          edSc1Da.Text = "0"
          edSc1Da.Enabled = False
          edSc1A.Text = "0"
          edSc1A.Enabled = False
          edDivSc1.Enabled = False
        End If
        If ckSconto2.Checked Then
          edPosSc2.Text = "0"
          edPosSc2.Enabled = False
          edSc2Da.Enabled = True
          edSc2A.Enabled = True
          edDivSc2.Enabled = True
        Else
          edPosSc2.Text = "0"
          edPosSc2.Enabled = False
          edSc2Da.Text = "0"
          edSc2Da.Enabled = False
          edSc2A.Text = "0"
          edSc2A.Enabled = False
          edDivSc2.Enabled = False
        End If
        If ckSconto3.Checked Then
          edPosSc3.Text = "0"
          edPosSc3.Enabled = False
          edSc3Da.Enabled = True
          edSc3A.Enabled = True
          edDivSc3.Enabled = True
        Else
          edPosSc3.Text = "0"
          edPosSc3.Enabled = False
          edSc3Da.Text = "0"
          edSc3Da.Enabled = False
          edSc3A.Text = "0"
          edSc3A.Enabled = False
          edDivSc3.Enabled = False
        End If
        If ckSconto4.Checked Then
          edPosSc4.Text = "0"
          edPosSc4.Enabled = False
          edSc4Da.Enabled = True
          edSc4A.Enabled = True
          edDivSc4.Enabled = True
        Else
          edPosSc4.Text = "0"
          edPosSc4.Enabled = False
          edSc4Da.Text = "0"
          edSc4Da.Enabled = False
          edSc4A.Text = "0"
          edSc4A.Enabled = False
          edDivSc4.Enabled = False
        End If
        If ckSconto5.Checked Then
          edPosSc5.Text = "0"
          edPosSc5.Enabled = False
          edSc5Da.Enabled = True
          edSc5A.Enabled = True
          edDivSc5.Enabled = True
        Else
          edPosSc5.Text = "0"
          edPosSc5.Enabled = False
          edSc5Da.Text = "0"
          edSc5Da.Enabled = False
          edSc5A.Text = "0"
          edSc5A.Enabled = False
          edDivSc5.Enabled = False
        End If
        If ckSconto6.Checked Then
          edPosSc6.Text = "0"
          edPosSc6.Enabled = False
          edSc6Da.Enabled = True
          edSc6A.Enabled = True
          edDivSc6.Enabled = True
        Else
          edPosSc6.Text = "0"
          edPosSc6.Enabled = False
          edSc6Da.Text = "0"
          edSc6Da.Enabled = False
          edSc6A.Text = "0"
          edSc6A.Enabled = False
          edDivSc6.Enabled = False
        End If
        If ckDatcons.Checked Then
          edPosDatcons.Text = "0"
          edPosDatcons.Enabled = False
          edDatconsDa.Enabled = True
          edDatconsA.Enabled = True
        Else
          edPosDatcons.Text = "0"
          edPosDatcons.Enabled = False
          edDatconsDa.Text = "0"
          edDatconsDa.Enabled = False
          edDatconsA.Text = "0"
          edDatconsA.Enabled = False
        End If
        edSepField.Text = ";"
        edSepField.Enabled = False
      Else
        'codart
        edPosArt.Enabled = True
        edArtDa.Text = "0"
        edArtDa.Enabled = False
        edArtA.Text = "0"
        edArtA.Enabled = False
        '--- Descrizione
        If ckDescrizione.Checked Then
          edPosDescr.Enabled = True
          edDescrDa.Text = "0"
          edDescrDa.Enabled = False
          edDescrA.Text = "0"
          edDescrA.Enabled = False
        Else
          edPosDescr.Text = "0"
          edPosDescr.Enabled = False
          edDescrDa.Text = "0"
          edDescrDa.Enabled = False
          edDescrA.Text = "0"
          edDescrA.Enabled = False
        End If
        '--- Descrizione interna
        If ckDesint.Checked Then
          edPosDesint.Enabled = True
          edDesintDa.Text = "0"
          edDesintDa.Enabled = False
          edDesintA.Text = "0"
          edDesintA.Enabled = False
        Else
          edPosDesint.Text = "0"
          edPosDesint.Enabled = False
          edDesintDa.Text = "0"
          edDesintDa.Enabled = False
          edDesintA.Text = "0"
          edDesintA.Enabled = False
        End If
        'quant
        If ckQuant.Checked Then
          edPosQua.Enabled = True
          edQuantDa.Text = "0"
          edQuantDa.Enabled = False
          edQuantA.Text = "0"
          edQuantA.Enabled = False
          edDivQuant.Enabled = True
        Else
          edPosQua.Text = "0"
          edPosQua.Enabled = False
          edQuantDa.Text = "0"
          edQuantDa.Enabled = False
          edQuantA.Text = "0"
          edQuantA.Enabled = False
          edDivQuant.Enabled = False
        End If
        'Prezzi
        If ckPrezzo.Checked Then
          edPosPre.Enabled = True
          edPreDa.Text = "0"
          edPreDa.Enabled = False
          edPreA.Text = "0"
          edPreA.Enabled = False
          edDivPrezzo.Enabled = True
        Else
          edPosPre.Text = "0"
          edPosPre.Enabled = False
          edPreDa.Text = "0"
          edPreDa.Enabled = False
          edPreA.Text = "0"
          edPreA.Enabled = False
          edDivPrezzo.Enabled = False
        End If
        'sconti
        If ckSconto1.Checked Then
          edPosSc1.Enabled = True
          edSc1Da.Text = "0"
          edSc1Da.Enabled = False
          edSc1A.Text = "0"
          edSc1A.Enabled = False
          edDivSc1.Enabled = True
        Else
          edPosSc1.Text = "0"
          edPosSc1.Enabled = False
          edSc1Da.Text = "0"
          edSc1Da.Enabled = False
          edSc1A.Text = "0"
          edSc1A.Enabled = False
          edDivSc1.Enabled = False
        End If
        If ckSconto2.Checked Then
          edPosSc2.Enabled = True
          edSc2Da.Text = "0"
          edSc2Da.Enabled = False
          edSc2A.Text = "0"
          edSc2A.Enabled = False
          edDivSc2.Enabled = True
        Else
          edPosSc2.Text = "0"
          edPosSc2.Enabled = False
          edSc2Da.Text = "0"
          edSc2Da.Enabled = False
          edSc2A.Text = "0"
          edSc2A.Enabled = False
          edDivSc2.Enabled = False
        End If
        If ckSconto3.Checked Then
          edPosSc3.Enabled = True
          edSc3Da.Text = "0"
          edSc3Da.Enabled = False
          edSc3A.Text = "0"
          edSc3A.Enabled = False
          edDivSc3.Enabled = True
        Else
          edPosSc3.Text = "0"
          edPosSc3.Enabled = False
          edSc3Da.Text = "0"
          edSc3Da.Enabled = False
          edSc3A.Text = "0"
          edSc3A.Enabled = False
          edDivSc3.Enabled = False
        End If
        If ckSconto4.Checked Then
          edPosSc4.Enabled = True
          edSc4Da.Text = "0"
          edSc4Da.Enabled = False
          edSc4A.Text = "0"
          edSc4A.Enabled = False
          edDivSc4.Enabled = True
        Else
          edPosSc4.Text = "0"
          edPosSc4.Enabled = False
          edSc4Da.Text = "0"
          edSc4Da.Enabled = False
          edSc4A.Text = "0"
          edSc4A.Enabled = False
          edDivSc4.Enabled = False
        End If
        If ckSconto5.Checked Then
          edPosSc5.Enabled = True
          edSc5Da.Text = "0"
          edSc5Da.Enabled = False
          edSc5A.Text = "0"
          edSc5A.Enabled = False
          edDivSc5.Enabled = True
        Else
          edPosSc5.Text = "0"
          edPosSc5.Enabled = False
          edSc5Da.Text = "0"
          edSc5Da.Enabled = False
          edSc5A.Text = "0"
          edSc5A.Enabled = False
          edDivSc5.Enabled = False
        End If
        If ckSconto6.Checked Then
          edPosSc6.Enabled = True
          edSc6Da.Text = "0"
          edSc6Da.Enabled = False
          edSc6A.Text = "0"
          edSc6A.Enabled = False
          edDivSc6.Enabled = True
        Else
          edPosSc6.Text = "0"
          edPosSc6.Enabled = False
          edSc6Da.Text = "0"
          edSc6Da.Enabled = False
          edSc6A.Text = "0"
          edSc6A.Enabled = False
          edDivSc6.Enabled = False
        End If
        '--- Data consegna
        If ckDatcons.Checked Then
          edPosDatcons.Enabled = True
          edDatconsDa.Text = "0"
          edDatconsDa.Enabled = False
          edDatconsA.Text = "0"
          edDatconsA.Enabled = False
        Else
          edPosDatcons.Text = "0"
          edPosDatcons.Enabled = False
          edDatconsDa.Text = "0"
          edDatconsDa.Enabled = False
          edDatconsA.Text = "0"
          edDatconsA.Enabled = False
        End If
        edSepField.Enabled = True
      End If    'If opFixed.Checked Then

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub ckExecuteI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckExecuteI.CheckedChanged
    Try
      ckSilentI.Enabled = ckExecuteI.Checked
      cmdCommandI.Enabled = ckExecuteI.Checked
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opFixedI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opFixedI.CheckedChanged
    Try
      xx_luni.Visible = opFixedI.Checked
      edSepFieldI.Enabled = Not opFixedI.Checked
      edMatricI.Enabled = opFixedI.Checked And ckMatricI.Checked
      edQtaMatrI.Enabled = opFixedI.Checked And ckMatricI.Checked
      edTagliaI.Enabled = opFixedI.Checked And ckTagliaI.Checked
      edQtaTagliaI.Enabled = opFixedI.Checked And ckTagliaI.Checked
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckExecuteE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckExecuteE.CheckedChanged
    Try
      ckSilentE.Enabled = ckExecuteE.Checked
      cmdCommandE.Enabled = ckExecuteE.Checked
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub opFixedE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles opFixedE.CheckedChanged
    Try
      xx_lune.Visible = opFixedE.Checked
      edSepFieldE.Enabled = Not opFixedE.Checked
      edMatrE.Enabled = opFixedE.Checked And ckMatricE.Checked
      edQtaMatrE.Enabled = opFixedE.Checked And ckMatricE.Checked
      edTagliaE.Enabled = opFixedE.Checked And ckTagliaE.Checked
      edQtaTagliaE.Enabled = opFixedE.Checked And ckTagliaE.Checked
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi griglia"
  Public Overridable Sub grvExport_NTSCellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvExport.NTSCellValueChanged
    Dim dtrRow() As DataRow
    Try
      If e.Column.Name = "xx_nomee" Then
        'Propongo la posizione
        If NTSCStr(grvExport.NTSGetCurrentDataRow()!xx_pose) = "0" Then
          dtrRow = oCleDocu.dttExport.Select("xx_pose = MAX(xx_pose)")
          If dtrRow.Length <> 0 Then
            grvExport.NTSGetCurrentDataRow()!xx_pose = NTSCInt(dtrRow(0)!xx_pose) + 1
          Else
            grvExport.NTSGetCurrentDataRow()!xx_pose = 1
          End If
        End If
      End If
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvExport_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvExport.NTSBeforeRowUpdate
    Try
      If grvExport.NTSGetCurrentDataRow Is Nothing Then Return

      If oCleDocu.dttExport.Select("xx_nomee = '" & NTSCStr(grvExport.NTSGetCurrentDataRow!xx_nomee) & "'").Length > 1 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129247856679599804, "La colonna selezionata è già stata configurata"))
        e.Allow = False
      End If
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvImport_NTSCellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvImport.NTSCellValueChanged
    Dim dtrRow() As DataRow
    Try
      If e.Column.Name = "xx_nomei" Then
        'Propongo la posizione
        If NTSCStr(grvImport.NTSGetCurrentDataRow()!xx_posi) = "0" Then
          dtrRow = oCleDocu.dttImport.Select("xx_posi = MAX(xx_posi)")
          If dtrRow.Length <> 0 Then
            grvImport.NTSGetCurrentDataRow()!xx_posi = NTSCInt(dtrRow(0)!xx_posi) + 1
          Else
            grvImport.NTSGetCurrentDataRow()!xx_posi = 1
          End If
        End If
      End If
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub grvImport_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvImport.NTSBeforeRowUpdate
    Try
      If grvImport.NTSGetCurrentDataRow Is Nothing Then Return

      If oCleDocu.dttImport.Select("xx_nomei = '" & NTSCStr(grvImport.NTSGetCurrentDataRow!xx_nomei) & "' AND xx_nomei <> '   '").Length > 1 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129247859549186149, "La colonna selezionata è già stata configurata"))
        e.Allow = False
      End If
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub CaricaComboLayout(ByVal strNew As String)
    Dim dttImpostaFile As New DataTable()
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      dttImpostaFile.Columns.Add("cod", GetType(String))
      dttImpostaFile.Columns.Add("val", GetType(String))
      dttImpostaFile.Rows.Add(New Object() {"Terminale", "(Predefinito)"})
      If oCleDocu.GetImpostazioniFile(oCallParams.strNomProg, dttTmp) = True Then
        For i As Integer = 0 To (dttTmp.Rows.Count - 1)
          With dttTmp.Rows(i)
            dttImpostaFile.Rows.Add(New Object() {NTSCStr(!rp_liv2), Mid(NTSCStr(!rp_liv2), 11)})
          End With
        Next
      End If
      If strNew.Trim <> "" Then dttImpostaFile.Rows.Add(New Object() {strNew, Mid(strNew, 11)})
      dttImpostaFile.AcceptChanges()
      cbImpostaFile.DataSource = dttImpostaFile
      cbImpostaFile.ValueMember = "cod"
      cbImpostaFile.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub
  Public Overridable Sub CaricaMaschera()
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Imposta la finestra con le opzioni di registro come eventualmente indicato
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      If Not oCleDocu.TerminaleLeggiCheck(oCallParams.strNomProg) Then
        Me.Close()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Ora imposta la maschera
      '--------------------------------------------------------------------------------------------------------------
      bInLoad = True
      If oCleDocu.bTermExecute Then
        ckExecute.Checked = True
        lbCommand.Text = oCleDocu.strTermCommand
        If oCleDocu.bTermSilent Then ckSilent.Checked = True Else ckSilent.Checked = False
      Else
        ckExecute.Checked = False
      End If
      edNomeFile.Text = oCleDocu.strTermFileName
      If oCleDocu.bTermDeleteAfter Then ckDelFile.Checked = True Else ckDelFile.Checked = False
      If oCleDocu.bTermIgnoreFirstRow Then ckIgnore1rk.Checked = True Else ckIgnore1rk.Checked = False
      If oCleDocu.bTermShowLogErr Then ckShowLog.Checked = True Else ckShowLog.Checked = False
      If oCleDocu.bTermDescr Then ckDescrizione.Checked = True Else ckDescrizione.Checked = False
      If oCleDocu.bTermDesint Then ckDesint.Checked = True Else ckDesint.Checked = False
      If oCleDocu.bTermQuant Then ckQuant.Checked = True Else ckQuant.Checked = False
      If oCleDocu.bTermPrezzi Then ckPrezzo.Checked = True Else ckPrezzo.Checked = False
      If oCleDocu.bTermScont1 Then ckSconto1.Checked = True Else ckSconto1.Checked = False
      If oCleDocu.bTermScont2 Then ckSconto2.Checked = True Else ckSconto2.Checked = False
      If oCleDocu.bTermScont3 Then ckSconto3.Checked = True Else ckSconto3.Checked = False
      If oCleDocu.bTermScont4 Then ckSconto4.Checked = True Else ckSconto4.Checked = False
      If oCleDocu.bTermScont5 Then ckSconto5.Checked = True Else ckSconto5.Checked = False
      If oCleDocu.bTermScont6 Then ckSconto6.Checked = True Else ckSconto6.Checked = False
      If oCleDocu.bTermDatcons Then ckDatcons.Checked = True Else ckDatcons.Checked = False
      '--------------------------------------------------------------------------------------------------------------
      Select Case oCleDocu.strTermTipoArt
        Case "X"  'articolo o barcode
          opCodartBarcodeCodarfo.Checked = True
        Case "C" 'codart
          opCodart.Checked = True
        Case "B" 'barcode(default)
          opBarcode.Checked = True
        Case Else 'codarfo
          opCodarfo.Checked = True
      End Select
      If oCleDocu.bTermBarcodeConfez Then ckBarcodeConfez.Checked = True Else ckBarcodeConfez.Checked = False
      edDecSep.Text = oCleDocu.strTermDecimalSep
      Select Case oCleDocu.strTermFileType
        Case "F" 'fixed(default)
          opFixed.Checked = True
        Case Else 'S=carattere separatore
          opDelim.Checked = True
          edSepField.Text = oCleDocu.strTermFieldSep
      End Select
      '--------------------------------------------------------------------------------------------------------------
      edPosArt.Text = oCleDocu.nTermCodartPos.ToString
      '--------------------------------------------------------------------------------------------------------------
      edPosDescr.Text = oCleDocu.nTermDescrPos.ToString
      edPosDesint.Text = oCleDocu.nTermDesintPos.ToString
      edPosQua.Text = oCleDocu.nTermQuantPos.ToString
      edPosPre.Text = oCleDocu.nTermPrezziPos.ToString
      edPosSc1.Text = oCleDocu.nTermScont1Pos.ToString
      edPosSc2.Text = oCleDocu.nTermScont2Pos.ToString
      edPosSc3.Text = oCleDocu.nTermScont3Pos.ToString
      edPosSc4.Text = oCleDocu.nTermScont4Pos.ToString
      edPosSc5.Text = oCleDocu.nTermScont5Pos.ToString
      edPosSc6.Text = oCleDocu.nTermScont6Pos.ToString
      edPosDatcons.Text = oCleDocu.nTermDatconsPos.ToString
      '--------------------------------------------------------------------------------------------------------------
      edArtDa.Text = oCleDocu.nTermCodartStart.ToString
      edDescrDa.Text = oCleDocu.nTermDescrStart.ToString
      edDesintDa.Text = oCleDocu.nTermDesintStart.ToString
      edQuantDa.Text = oCleDocu.nTermQuantStart.ToString
      edPreDa.Text = oCleDocu.nTermPrezziStart.ToString
      edSc1Da.Text = oCleDocu.nTermScont1Start.ToString
      edSc2Da.Text = oCleDocu.nTermScont2Start.ToString
      edSc3Da.Text = oCleDocu.nTermScont3Start.ToString
      edSc4Da.Text = oCleDocu.nTermScont4Start.ToString
      edSc5Da.Text = oCleDocu.nTermScont5Start.ToString
      edSc6Da.Text = oCleDocu.nTermScont6Start.ToString
      edDatconsDa.Text = oCleDocu.nTermDatconsStart.ToString
      '--------------------------------------------------------------------------------------------------------------
      edArtA.Text = oCleDocu.nTermCodartStop.ToString
      edDescrA.Text = oCleDocu.nTermDescrStop.ToString
      edDesintA.Text = oCleDocu.nTermDesintStop.ToString
      edQuantA.Text = oCleDocu.nTermQuantStop.ToString
      edPreA.Text = oCleDocu.nTermPrezziStop.ToString
      edSc1A.Text = oCleDocu.nTermScont1Stop.ToString
      edSc2A.Text = oCleDocu.nTermScont2Stop.ToString
      edSc3A.Text = oCleDocu.nTermScont3Stop.ToString
      edSc4A.Text = oCleDocu.nTermScont4Stop.ToString
      edSc5A.Text = oCleDocu.nTermScont5Stop.ToString
      edSc6A.Text = oCleDocu.nTermScont6Stop.ToString
      edDatconsA.Text = oCleDocu.nTermDatconsStop.ToString
      '--------------------------------------------------------------------------------------------------------------
      edDivQuant.Text = oCleDocu.nTermDivQuant.ToString
      edDivPrezzo.Text = oCleDocu.nTermDivPrezzi.ToString
      edDivSc1.Text = oCleDocu.nTermDivScont1.ToString
      edDivSc2.Text = oCleDocu.nTermDivScont2.ToString
      edDivSc3.Text = oCleDocu.nTermDivScont3.ToString
      edDivSc4.Text = oCleDocu.nTermDivScont4.ToString
      edDivSc5.Text = oCleDocu.nTermDivScont5.ToString
      edDivSc6.Text = oCleDocu.nTermDivScont6.ToString
      '--------------------------------------------------------------------------------------------------------------
      '---Import---
      ckExecuteI.Checked = oCleDocu.bTermExecuteI
      lbCommandI.Text = oCleDocu.strTermCommandI
      ckSilentI.Checked = oCleDocu.bTermSilentI
      edNomeFileI.Text = oCleDocu.strTermFileNameI
      ckDelFileI.Checked = oCleDocu.bTermDeleteAfterI
      ckIgnore1rkI.Checked = oCleDocu.bTermIgnoreFirstRowI
      ckPrezziI.Checked = oCleDocu.bTermRicalPrezI
      ckScontiI.Checked = oCleDocu.bTermRicalScontI
      ckProvvI.Checked = oCleDocu.bTermRicalProvvI
      ckShowLogI.Checked = oCleDocu.bTermShowLogErrI
      Select Case oCleDocu.strTermFileTypeI
        Case "S" : opDelimI.Checked = True
        Case Else : opFixedI.Checked = True
      End Select
      edSepFieldI.Text = oCleDocu.strTermFieldSepI
      edDecSepI.Text = oCleDocu.strTermDecimalSepI
      ckMatricI.Checked = oCleDocu.bMatricI
      edMatricI.Text = NTSCStr(oCleDocu.lMatricI)
      edQtaMatrI.Text = NTSCStr(oCleDocu.lQtaMatrI)
      ckTagliaI.Checked = oCleDocu.bTagliaI
      edTagliaI.Text = NTSCStr(oCleDocu.lTagliaI)
      edQtaTagliaI.Text = NTSCStr(oCleDocu.lQtaTagliaI)
      ckTipoRigaI.Checked = oCleDocu.bTipoRigaI
      dcImport.DataSource = oCleDocu.dttImport
      grImport.DataSource = dcImport
      '---Export---
      ckExecuteE.Checked = oCleDocu.bTermExecuteE
      lbCommandE.Text = oCleDocu.strTermCommandE
      ckSilentE.Checked = oCleDocu.bTermSilentE
      edNomeFileE.Text = oCleDocu.strTermFileNameE
      ckGenTracciatoE.Checked = oCleDocu.bGeneraTracE
      ckShowLogE.Checked = oCleDocu.bTermShowLogErrE
      Select Case oCleDocu.strTermFileTypeE
        Case "S" : opDelimE.Checked = True
        Case Else : opFixedE.Checked = True
      End Select
      edSepFieldE.Text = oCleDocu.strTermFieldSepE
      ckMatricE.Checked = oCleDocu.bMatricE
      edMatrE.Text = NTSCStr(oCleDocu.lMatricE)
      edQtaMatrE.Text = NTSCStr(oCleDocu.lQtaMatrE)
      ckTagliaE.Checked = oCleDocu.bTagliaE
      edTagliaE.Text = NTSCStr(oCleDocu.lTagliaE)
      edQtaTagliaE.Text = NTSCStr(oCleDocu.lQtaTagliaE)
      ckTipoRigaE.Checked = oCleDocu.bTipoRigaE
      dcExport.DataSource = oCleDocu.dttExport
      grExport.DataSource = dcExport
      '--------------------------------------------------------------------------------------------------------------
      bInLoad = False
      '--------------------------------------------------------------------------------------------------------------
      ImpostaCampi()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Function Salva() As Boolean
    Dim strReg As String
    Dim dtrRowI() As DataRow = Nothing
    Dim dtrRowE() As DataRow = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlli preliminari
      '--------------------------------------------------------------------------------------------------------------
      If ckExecute.Checked Then
        If lbCommand.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129247970939133080, "File terminale: Comando da eseguire obbligatorio."))
          Return False
        End If
      End If
      If edNomeFile.Text.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129247970954133080, "File terminale: Nome di file obbligatorio."))
        Return False
      End If
      If opDelim.Checked Then
        If edSepField.Text = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129247970963195580, "File terminale: Separatore di campo obbligatorio."))
          Return False
        End If
      End If
      If edDecSep.Text.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129247970984289330, "File terminale: Separatore decimale obbligatorio."))
        Return False
      End If
      If edDecSep.Text <> "," And edDecSep.Text <> "." Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129247970995383080, "File terminale: Il separatore decimale deve essere '.' (punto) o ',' (virgola)."))
        Return False
      End If
      'Import
      If tcImport.SelectedTabPageIndex = 2 Then
        If edNomeFileI.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129597725836875000, "Importazione: Nome di file obbligatorio."))
          Return False
        End If
        If ckExecuteI.Checked Then
          If lbCommandI.Text.Trim = "" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129247971011945580, "Importazione: Comando da eseguire obbligatorio."))
            Return False
          End If
        End If
        If opDelimI.Checked Then
          If edSepField.Text = "" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129247971035070580, "Importazione: Separatore di campo obbligatorio."))
            Return False
          End If
        End If
        If edDecSepI.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129247971052414330, "Importazione: Separatore decimale obbligatorio."))
          Return False
        End If
        If edDecSepI.Text <> "," And edDecSepI.Text <> "." Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129247971064133080, "Importazione: Il separatore decimale deve essere '.' (punto) o ',' (virgola)."))
          Return False
        End If
      End If
      'Export
      If tcImport.SelectedTabPageIndex = 1 Then
        If edNomeFileE.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129597725827031250, "Esportazione: Nome di file obbligatorio."))
          Return False
        End If
        If ckExecuteE.Checked Then
          If lbCommandE.Text.Trim = "" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129247971079445580, "Esportazione: Comando da eseguire obbligatorio."))
            Return False
          End If
        End If
        If opDelimE.Checked Then
          If edSepFieldE.Text = "" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129247971107101830, "Esportazione: Separatore di campo obbligatorio."))
            Return False
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not opFixed.Checked Then
        '------------------------------------------------------------------------------------------------------------
        '--- Controlla validità numeri
        '--- Controlla congruenza
        '------------------------------------------------------------------------------------------------------------
        If ckDescrizione.Checked Then
          If edPosArt.Text = edPosDescr.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571245200312500, "La posizione del campo descrizione non deve essere uguale alla posizione del campo quantità."))
            Return False
          End If
        End If
        If ckDesint.Checked Then
          If edPosArt.Text = edPosDesint.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037935620266712, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo quantità."))
            Return False
          End If
        End If
        If ckQuant.Checked Then
          If edPosArt.Text = edPosQua.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571245342031250, "La posizione del campo articolo non deve essere uguale alla posizione del campo quantità."))
            Return False
          End If
        End If
        If ckPrezzo.Checked Then
          If edPosArt.Text = edPosPre.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260054076000, "La posizione del campo articolo non deve essere uguale alla posizione del campo prezzo."))
            Return False
          End If
        End If
        If ckSconto1.Checked Then
          If edPosArt.Text = edPosSc1.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775253933208000, "La posizione del campo articolo non deve essere uguale alla posizione del campo sconto 1."))
            Return False
          End If
        End If
        If ckSconto2.Checked Then
          If edPosArt.Text = edPosSc2.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775253920572000, "La posizione del campo articolo non deve essere uguale alla posizione del campo sconto 2."))
            Return False
          End If
        End If
        If ckSconto3.Checked Then
          If edPosArt.Text = edPosSc3.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775253900292000, "La posizione del campo articolo non deve essere uguale alla posizione del campo sconto 3."))
            Return False
          End If
        End If
        If ckSconto4.Checked Then
          If edPosArt.Text = edPosSc4.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775253884692000, "La posizione del campo articolo non deve essere uguale alla posizione del campo sconto 4."))
            Return False
          End If
        End If
        If ckSconto5.Checked Then
          If edPosArt.Text = edPosSc5.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775253864256000, "La posizione del campo articolo non deve essere uguale alla posizione del campo sconto 5."))
            Return False
          End If
        End If
        If ckSconto6.Checked Then
          If edPosArt.Text = edPosSc6.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775253849904000, "La posizione del campo articolo non deve essere uguale alla posizione del campo sconto 6."))
            Return False
          End If
        End If
        If ckDatcons.Checked Then
          If edPosArt.Text = edPosDatcons.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037935938860462, "La posizione del campo articolo non deve essere uguale alla posizione del campo data consegna."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckDesint.Checked) Then
          If edPosDescr.Text = edPosDesint.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037936313860462, "La posizione del campo descrizione non deve essere uguale alla posizione del campo descrizione interna."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckQuant.Checked) Then
          If edPosDescr.Text = edPosQua.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254565320000, "La posizione del campo descrizione non deve essere uguale alla posizione del campo quantità."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckPrezzo.Checked) Then
          If edPosDescr.Text = edPosPre.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254545508000, "La posizione del campo descrizione non deve essere uguale alla posizione del campo prezzo."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto1.Checked) Then
          If edPosDescr.Text = edPosSc1.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254533340000, "La posizione del campo descrizione non deve essere uguale alla posizione del campo sconto 1."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto2.Checked) Then
          If edPosDescr.Text = edPosSc2.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254518052000, "La posizione del campo descrizione non deve essere uguale alla posizione del campo sconto 2."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto3.Checked) Then
          If edPosDescr.Text = edPosSc3.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254499800000, "La posizione del campo descrizione non deve essere uguale alla posizione del campo sconto 3."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto4.Checked) Then
          If edPosDescr.Text = edPosSc4.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254487164000, "La posizione del campo descrizione non deve essere uguale alla posizione del campo sconto 4."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto5.Checked) Then
          If edPosDescr.Text = edPosSc5.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254473904000, "La posizione del campo descrizione non deve essere uguale alla posizione del campo sconto 5."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto6.Checked) Then
          If edPosDescr.Text = edPosSc6.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254460800000, "La posizione del campo descrizione non deve essere uguale alla posizione del campo sconto 6."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckDatcons.Checked) Then
          If edPosDescr.Text = edPosDatcons.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037936763860462, "La posizione del campo descrizione non deve essere uguale alla posizione del campo data consegna."))
            Return False
          End If
        End If
        If (ckQuant.Checked) And (ckDesint.Checked) Then
          If edPosDesint.Text = edPosQua.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037937372922962, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo quantità."))
            Return False
          End If
        End If
        If (ckPrezzo.Checked) And (ckDesint.Checked) Then
          If edPosDesint.Text = edPosPre.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037937944641712, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo prezzo."))
            Return False
          End If
        End If
        If (ckSconto1.Checked) And (ckDesint.Checked) Then
          If edPosDesint.Text = edPosSc1.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037938254641712, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo sconto 1."))
            Return False
          End If
        End If
        If (ckSconto2.Checked) And (ckDesint.Checked) Then
          If edPosDesint.Text = edPosSc2.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037938587766712, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo sconto 2."))
            Return False
          End If
        End If
        If (ckSconto3.Checked) And (ckDesint.Checked) Then
          If edPosDesint.Text = edPosSc3.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037938708704212, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo sconto 3."))
            Return False
          End If
        End If
        If (ckSconto4.Checked) And (ckDesint.Checked) Then
          If edPosDesint.Text = edPosSc4.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037938837141712, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo sconto 4."))
            Return False
          End If
        End If
        If (ckSconto5.Checked) And (ckDesint.Checked) Then
          If edPosDesint.Text = edPosSc5.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037939001829212, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo sconto 5."))
            Return False
          End If
        End If
        If (ckSconto6.Checked) And (ckDesint.Checked) Then
          If edPosDesint.Text = edPosSc6.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037939156516712, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo sconto 6."))
            Return False
          End If
        End If
        If (ckDatcons.Checked) And (ckDesint.Checked) Then
          If edPosDesint.Text = edPosDatcons.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037939639016712, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo data consegna."))
            Return False
          End If
        End If
        If (ckPrezzo.Checked) And (ckQuant.Checked) Then
          If edPosQua.Text = edPosPre.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254887304000, "La posizione del campo quantità non deve essere uguale alla posizione del campo prezzo."))
            Return False
          End If
        End If
        If (ckSconto1.Checked) And (ckQuant.Checked) Then
          If edPosQua.Text = edPosSc1.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254871080000, "La posizione del campo quantità non deve essere uguale alla posizione del campo sconto 1."))
            Return False
          End If
        End If
        If (ckSconto2.Checked) And (ckQuant.Checked) Then
          If edPosQua.Text = edPosSc2.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254857352000, "La posizione del campo quantità non deve essere uguale alla posizione del campo sconto 2."))
            Return False
          End If
        End If
        If (ckSconto3.Checked) And (ckQuant.Checked) Then
          If edPosQua.Text = edPosSc3.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254843624000, "La posizione del campo quantità non deve essere uguale alla posizione del campo sconto 3."))
            Return False
          End If
        End If
        If (ckSconto4.Checked) And (ckQuant.Checked) Then
          If edPosQua.Text = edPosSc4.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254830052000, "La posizione del campo quantità non deve essere uguale alla posizione del campo sconto 4."))
            Return False
          End If
        End If
        If (ckSconto5.Checked) And (ckQuant.Checked) Then
          If edPosQua.Text = edPosSc5.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254817416000, "La posizione del campo quantità non deve essere uguale alla posizione del campo sconto 5."))
            Return False
          End If
        End If
        If (ckSconto6.Checked) And (ckQuant.Checked) Then
          If edPosQua.Text = edPosSc6.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254798696000, "La posizione del campo quantità non deve essere uguale alla posizione del campo sconto 6."))
            Return False
          End If
        End If
        If (ckDatcons.Checked) And (ckQuant.Checked) Then
          If edPosQua.Text = edPosDatcons.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037939900579212, "La posizione del campo quantità non deve essere uguale alla posizione del campo data consegna."))
            Return False
          End If
        End If
        If (ckSconto1.Checked) And (ckPrezzo.Checked) Then
          If edPosPre.Text = edPosSc1.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775255545070000, "La posizione del campo prezzo non deve essere uguale alla posizione del campo sconto 1."))
            Return False
          End If
        End If
        If (ckSconto2.Checked) And (ckPrezzo.Checked) Then
          If edPosPre.Text = edPosSc2.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775255530250000, "La posizione del campo prezzo non deve essere uguale alla posizione del campo sconto 2."))
            Return False
          End If
        End If
        If (ckSconto3.Checked) And (ckPrezzo.Checked) Then
          If edPosPre.Text = edPosSc3.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775255520578000, "La posizione del campo prezzo non deve essere uguale alla posizione del campo sconto 3."))
            Return False
          End If
        End If
        If (ckSconto4.Checked) And (ckPrezzo.Checked) Then
          If edPosPre.Text = edPosSc4.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775255509034000, "La posizione del campo prezzo non deve essere uguale alla posizione del campo sconto 4."))
            Return False
          End If
        End If
        If (ckSconto5.Checked) And (ckPrezzo.Checked) Then
          If edPosPre.Text = edPosSc5.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775255497490000, "La posizione del campo prezzo non deve essere uguale alla posizione del campo sconto 5."))
            Return False
          End If
        End If
        If (ckSconto6.Checked) And (ckPrezzo.Checked) Then
          If edPosPre.Text = edPosSc6.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775255482670000, "La posizione del campo prezzo non deve essere uguale alla posizione del campo sconto 6."))
            Return False
          End If
        End If
        If (ckDatcons.Checked) And (ckPrezzo.Checked) Then
          If edPosPre.Text = edPosDatcons.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037940137297962, "La posizione del campo prezzo non deve essere uguale alla posizione del campo data consegna."))
            Return False
          End If
        End If
      Else
        'Controlla validità numeri
        If NTSCInt(edArtDa.Text) > NTSCInt(edArtA.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128571247651250000, "La posizione iniziale del campo articolo non deve superare quella finale."))
          Return False
        End If
        If ckDescrizione.Checked Then
          If NTSCInt(edDescrDa.Text) > NTSCInt(edDescrA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571247881875000, "La posizione iniziale del campo descrizione non deve superare quella finale."))
            Return False
          End If
        End If
        If ckDesint.Checked Then
          If NTSCInt(edDesintDa.Text) > NTSCInt(edDesintA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037940556204212, "La posizione iniziale del campo descrizione interna non deve superare quella finale."))
            Return False
          End If
        End If
        If ckQuant.Checked Then
          If NTSCInt(edQuantDa.Text) > NTSCInt(edQuantA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571248041718750, "La posizione iniziale del campo quantità non deve superare quella finale."))
            Return False
          End If
        End If
        If ckPrezzo.Checked Then
          If NTSCInt(edPreDa.Text) > NTSCInt(edPreA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571248171093750, "La posizione iniziale del campo prezzo non deve superare quella finale."))
            Return False
          End If
        End If
        If ckSconto1.Checked Then
          If NTSCInt(edSc1Da.Text) > NTSCInt(edSc1A.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256369062000, "La posizione iniziale del campo sconto 1 non deve superare quella finale."))
            Return False
          End If
        End If
        If ckSconto2.Checked Then
          If NTSCInt(edSc2Da.Text) > NTSCInt(edSc2A.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256352994000, "La posizione iniziale del campo sconto 2 non deve superare quella finale."))
            Return False
          End If
        End If
        If ckSconto3.Checked Then
          If NTSCInt(edSc3Da.Text) > NTSCInt(edSc3A.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256340982000, "La posizione iniziale del campo sconto 3 non deve superare quella finale."))
            Return False
          End If
        End If
        If ckSconto4.Checked Then
          If NTSCInt(edSc4Da.Text) > NTSCInt(edSc4A.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256326630000, "La posizione iniziale del campo sconto 4 non deve superare quella finale."))
            Return False
          End If
        End If
        If ckSconto5.Checked Then
          If NTSCInt(edSc5Da.Text) > NTSCInt(edSc5A.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256314774000, "La posizione iniziale del campo sconto 5 non deve superare quella finale."))
            Return False
          End If
        End If
        If ckSconto6.Checked Then
          If NTSCInt(edSc6Da.Text) > NTSCInt(edSc6A.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256304322000, "La posizione iniziale del campo sconto 6 non deve superare quella finale."))
            Return False
          End If
        End If
        If ckDatcons.Checked Then
          If NTSCInt(edDatconsDa.Text) > NTSCInt(edDatconsA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037940896516712, "La posizione iniziale del campo data consegna non deve superare quella finale."))
            Return False
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Controllo congruenza posizioni
        '------------------------------------------------------------------------------------------------------------
        If ckDescrizione.Checked Then
          If (NTSCInt(edDescrDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edDescrDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edDescrA.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edArtDa.Text) <= NTSCInt(edDescrA.Text)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edArtA.Text) <= NTSCInt(edDescrA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256806486000, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckDesint.Checked Then
          If (NTSCInt(edDesintDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edDesintDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edDesintA.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edArtDa.Text) <= NTSCInt(edDesintA.Text)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edArtA.Text) <= NTSCInt(edDesintA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037942606086852, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckQuant.Checked Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edQuantDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edQuantA.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edArtDa.Text) <= NTSCInt(edQuantA.Text)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edArtA.Text) <= NTSCInt(edQuantA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256779498000, "La posizione del campo quantità non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckPrezzo.Checked Then
          If (NTSCInt(edPreDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edPreDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edPreA.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edArtDa.Text) <= NTSCInt(edPreA.Text)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edArtA.Text) <= NTSCInt(edPreA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256761714000, "La posizione del campo prezzo non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckSconto1.Checked Then
          If (NTSCInt(edSc1Da.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc1Da.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edSc1A.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc1A.Text) <= NTSCInt(edArtA)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edSc1Da) And NTSCInt(edArtDa.Text) <= NTSCInt(edSc1A)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edSc1Da) And NTSCInt(edArtA.Text) <= NTSCInt(edSc1A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256748766000, "La posizione del campo sconto 1 non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckSconto2.Checked Then
          If (NTSCInt(edSc2Da.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc2Da.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edSc2A.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc2A.Text) <= NTSCInt(edArtA)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edSc2Da) And NTSCInt(edArtDa.Text) <= NTSCInt(edSc2A)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edSc2Da) And NTSCInt(edArtA.Text) <= NTSCInt(edSc2A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256730514000, "La posizione del campo sconto 2 non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckSconto3.Checked Then
          If (NTSCInt(edSc3Da.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc3Da.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edSc3A.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc3A.Text) <= NTSCInt(edArtA)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edSc3Da) And NTSCInt(edArtDa.Text) <= NTSCInt(edSc3A)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edSc3Da) And NTSCInt(edArtA.Text) <= NTSCInt(edSc3A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256720374000, "La posizione del campo sconto 3 non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckSconto4.Checked Then
          If (NTSCInt(edSc4Da.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc4Da.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edSc4A.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc4A.Text) <= NTSCInt(edArtA)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edSc4Da) And NTSCInt(edArtDa.Text) <= NTSCInt(edSc4A)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edSc4Da) And NTSCInt(edArtA.Text) <= NTSCInt(edSc4A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256711326000, "La posizione del campo sconto 4 non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckSconto5.Checked Then
          If (NTSCInt(edSc5Da.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc5Da.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edSc5A.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc5A.Text) <= NTSCInt(edArtA)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edSc5Da) And NTSCInt(edArtDa.Text) <= NTSCInt(edSc5A)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edSc5Da) And NTSCInt(edArtA.Text) <= NTSCInt(edSc5A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260064840000, "La posizione del campo sconto 5 non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckSconto6.Checked Then
          If (NTSCInt(edSc6Da.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc6Da.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edSc6A.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc6A.Text) <= NTSCInt(edArtA)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edSc6Da) And NTSCInt(edArtDa.Text) <= NTSCInt(edSc6A)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edSc6Da) And NTSCInt(edArtA.Text) <= NTSCInt(edSc6A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256685586000, "La posizione del campo sconto 6 non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If ckDatcons.Checked Then
          If (NTSCInt(edDatconsDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edDatconsDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edDatconsA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edDatconsA.Text) <= NTSCInt(edArtA)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edArtDa.Text) <= NTSCInt(edDatconsA)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edArtA.Text) <= NTSCInt(edDatconsA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037943243274352, "La posizione del campo data consegna non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckDesint.Checked) Then
          If (NTSCInt(edDesintDa.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edDesintDa.Text) <= NTSCInt(edDescrA.Text)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edDesintA.Text) <= NTSCInt(edDescrA.Text)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edDescrDa.Text) <= NTSCInt(edDesintA.Text)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edDescrA.Text) <= NTSCInt(edDesintA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037943977336852, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo descrizione interna e viceversa."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edQuantDa.Text) <= NTSCInt(edDescrA.Text)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edQuantA.Text) <= NTSCInt(edDescrA.Text)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edDescrDa.Text) <= NTSCInt(edQuantA.Text)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edDescrA.Text) <= NTSCInt(edQuantA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571248820156250, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo quantità e viceversa."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckPrezzo.Checked) Then
          If (NTSCInt(edPreDa.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edPreDa.Text) <= NTSCInt(edDescrA.Text)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edPreA.Text) <= NTSCInt(edDescrA.Text)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edDescrDa.Text) <= NTSCInt(edPreA.Text)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edDescrA.Text) <= NTSCInt(edPreA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571249002031250, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto1.Checked) Then
          If (NTSCInt(edSc1Da.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc1Da.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edSc1A.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc1A.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edSc1Da) And NTSCInt(edDescrDa.Text) <= NTSCInt(edSc1A)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edSc1Da) And NTSCInt(edDescrA.Text) <= NTSCInt(edSc1A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260075292000, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo sconto 1 e viceversa."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto2.Checked) Then
          If (NTSCInt(edSc2Da.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc2Da.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edSc2A.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc2A.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edSc2Da) And NTSCInt(edDescrDa.Text) <= NTSCInt(edSc2A)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edSc2Da) And NTSCInt(edDescrA.Text) <= NTSCInt(edSc2A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260085900000, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo sconto 2 e viceversa."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto3.Checked) Then
          If (NTSCInt(edSc3Da.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc3Da.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edSc3A.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc3A.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edSc3Da) And NTSCInt(edDescrDa.Text) <= NTSCInt(edSc3A)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edSc3Da) And NTSCInt(edDescrA.Text) <= NTSCInt(edSc3A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260096196000, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo sconto 3 e viceversa."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto4.Checked) Then
          If (NTSCInt(edSc4Da.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc4Da.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edSc4A.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc4A.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edSc4Da) And NTSCInt(edDescrDa.Text) <= NTSCInt(edSc4A)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edSc4Da) And NTSCInt(edDescrA.Text) <= NTSCInt(edSc4A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260105244000, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo sconto 4 e viceversa."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto5.Checked) Then
          If (NTSCInt(edSc5Da.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc5Da.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edSc5A.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc5A.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edSc5Da) And NTSCInt(edDescrDa.Text) <= NTSCInt(edSc5A)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edSc5Da) And NTSCInt(edDescrA.Text) <= NTSCInt(edSc5A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260116476000, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo sconto 5 e viceversa."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto6.Checked) Then
          If (NTSCInt(edSc6Da.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc6Da.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edSc6A.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc6A.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edSc6Da) And NTSCInt(edDescrDa.Text) <= NTSCInt(edSc6A)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edSc6Da) And NTSCInt(edDescrA.Text) <= NTSCInt(edSc6A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260123028000, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo sconto 6 e viceversa."))
            Return False
          End If
        End If
        If (ckDescrizione.Checked) And (ckDatcons.Checked) Then
          If (NTSCInt(edDatconsDa.Text) >= NTSCInt(edDescrDa) And NTSCInt(edDatconsDa.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edDatconsA.Text) >= NTSCInt(edDescrDa) And NTSCInt(edDatconsA.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edDescrDa.Text) <= NTSCInt(edDatconsA)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edDescrA.Text) <= NTSCInt(edDatconsA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037945407336852, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo data consegna e viceversa."))
            Return False
          End If
        End If
        If (ckDesint.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edQuantDa.Text) <= NTSCInt(edDesintA.Text)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edQuantA.Text) <= NTSCInt(edDesintA.Text)) Or (NTSCInt(edDesintDa.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edDesintDa.Text) <= NTSCInt(edQuantA.Text)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edDesintA.Text) <= NTSCInt(edQuantA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037946799524352, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo quantità e viceversa."))
            Return False
          End If
        End If
        If (ckDesint.Checked) And (ckPrezzo.Checked) Then
          If (NTSCInt(edPreDa.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edPreDa.Text) <= NTSCInt(edDesintA.Text)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edPreA.Text) <= NTSCInt(edDesintA.Text)) Or (NTSCInt(edDesintDa.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edDesintDa.Text) <= NTSCInt(edPreA.Text)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edDesintA.Text) <= NTSCInt(edPreA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037947459993102, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Return False
          End If
        End If
        If (ckDesint.Checked) And (ckSconto1.Checked) Then
          If (NTSCInt(edSc1Da.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc1Da.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edSc1A.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc1A.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edDesintDa.Text) >= NTSCInt(edSc1Da) And NTSCInt(edDesintDa.Text) <= NTSCInt(edSc1A)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edSc1Da) And NTSCInt(edDesintA.Text) <= NTSCInt(edSc1A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037948317961852, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo sconto 1 e viceversa."))
            Return False
          End If
        End If
        If (ckDesint.Checked) And (ckSconto2.Checked) Then
          If (NTSCInt(edSc2Da.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc2Da.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edSc2A.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc2A.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edDesintDa.Text) >= NTSCInt(edSc2Da) And NTSCInt(edDesintDa.Text) <= NTSCInt(edSc2A)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edSc2Da) And NTSCInt(edDesintA.Text) <= NTSCInt(edSc2A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129038023223806992, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo sconto 2 e viceversa."))
            Return False
          End If
        End If
        If (ckDesint.Checked) And (ckSconto3.Checked) Then
          If (NTSCInt(edSc3Da.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc3Da.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edSc3A.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc3A.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edDesintDa.Text) >= NTSCInt(edSc3Da) And NTSCInt(edDesintDa.Text) <= NTSCInt(edSc3A)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edSc3Da) And NTSCInt(edDesintA.Text) <= NTSCInt(edSc3A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129038024774765899, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo sconto 3 e viceversa."))
            Return False
          End If
        End If
        If (ckDesint.Checked) And (ckSconto4.Checked) Then
          If (NTSCInt(edSc4Da.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc4Da.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edSc4A.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc4A.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edDesintDa.Text) >= NTSCInt(edSc4Da) And NTSCInt(edDesintDa.Text) <= NTSCInt(edSc4A)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edSc4Da) And NTSCInt(edDesintA.Text) <= NTSCInt(edSc4A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129038025613061854, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo sconto 4 e viceversa."))
            Return False
          End If
        End If
        If (ckDesint.Checked) And (ckSconto5.Checked) Then
          If (NTSCInt(edSc5Da.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc5Da.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edSc5A.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc5A.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edDesintDa.Text) >= NTSCInt(edSc5Da) And NTSCInt(edDesintDa.Text) <= NTSCInt(edSc5A)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edSc5Da) And NTSCInt(edDesintA.Text) <= NTSCInt(edSc5A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129038026162059919, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo sconto 5 e viceversa."))
            Return False
          End If
        End If
        If (ckDesint.Checked) And (ckSconto6.Checked) Then
          If (NTSCInt(edSc6Da.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc6Da.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edSc6A.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc6A.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edDesintDa.Text) >= NTSCInt(edSc6Da) And NTSCInt(edDesintDa.Text) <= NTSCInt(edSc6A)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edSc6Da) And NTSCInt(edDesintA.Text) <= NTSCInt(edSc6A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129038026727824489, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo sconto 6 e viceversa."))
            Return False
          End If
        End If
        If (ckDesint.Checked) And (ckDatcons.Checked) Then
          If (NTSCInt(edDatconsDa.Text) >= NTSCInt(edDesintDa) And NTSCInt(edDatconsDa.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edDatconsA.Text) >= NTSCInt(edDesintDa) And NTSCInt(edDatconsA.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edDesintDa.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edDesintDa.Text) <= NTSCInt(edDatconsA)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edDesintA.Text) <= NTSCInt(edDatconsA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129038027839883104, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo data consegna e viceversa."))
            Return False
          End If
        End If
        If (ckPrezzo.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edQuantDa.Text) <= NTSCInt(edPreA.Text)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edQuantA.Text) <= NTSCInt(edPreA.Text)) Or (NTSCInt(edPreDa.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edPreDa.Text) <= NTSCInt(edQuantA.Text)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edPreA.Text) <= NTSCInt(edQuantA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571249138750000, "La posizione del campo quantità non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Return False
          End If
        End If
        If (ckSconto1.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edSc1Da) And NTSCInt(edQuantDa.Text) <= NTSCInt(edSc1A)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edSc1Da) And NTSCInt(edQuantA.Text) <= NTSCInt(edSc1A)) Or (NTSCInt(edSc1Da.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc1Da.Text) <= NTSCInt(edQuantA)) Or (NTSCInt(edSc1A.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc1A.Text) <= NTSCInt(edQuantA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260138628000, "La posizione del campo quantità non deve essere inclusa nella posizione del campo sconto 1 e viceversa."))
            Return False
          End If
        End If
        If (ckSconto2.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edSc2Da) And NTSCInt(edQuantDa.Text) <= NTSCInt(edSc2A)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edSc2Da) And NTSCInt(edQuantA.Text) <= NTSCInt(edSc2A)) Or (NTSCInt(edSc2Da.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc2Da.Text) <= NTSCInt(edQuantA)) Or (NTSCInt(edSc2A.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc2A.Text) <= NTSCInt(edQuantA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260149392000, "La posizione del campo quantità non deve essere inclusa nella posizione del campo sconto 2 e viceversa."))
            Return False
          End If
        End If
        If (ckSconto3.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edSc3Da) And NTSCInt(edQuantDa.Text) <= NTSCInt(edSc3A)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edSc3Da) And NTSCInt(edQuantA.Text) <= NTSCInt(edSc3A)) Or (NTSCInt(edSc3Da.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc3Da.Text) <= NTSCInt(edQuantA)) Or (NTSCInt(edSc3A.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc3A.Text) <= NTSCInt(edQuantA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260157504000, "La posizione del campo quantità non deve essere inclusa nella posizione del campo sconto 3 e viceversa."))
            Return False
          End If
        End If
        If (ckSconto4.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edSc4Da) And NTSCInt(edQuantDa.Text) <= NTSCInt(edSc4A)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edSc4Da) And NTSCInt(edQuantA.Text) <= NTSCInt(edSc4A)) Or (NTSCInt(edSc4Da.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc4Da.Text) <= NTSCInt(edQuantA)) Or (NTSCInt(edSc4A.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc4A.Text) <= NTSCInt(edQuantA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260165460000, "La posizione del campo quantità non deve essere inclusa nella posizione del campo sconto 4 e viceversa."))
            Return False
          End If
        End If
        If (ckSconto5.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edSc5Da) And NTSCInt(edQuantDa.Text) <= NTSCInt(edSc5A)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edSc5Da) And NTSCInt(edQuantA.Text) <= NTSCInt(edSc5A)) Or (NTSCInt(edSc5Da.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc5Da.Text) <= NTSCInt(edQuantA)) Or (NTSCInt(edSc5A.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc5A.Text) <= NTSCInt(edQuantA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260174664000, "La posizione del campo quantità non deve essere inclusa nella posizione del campo sconto 5 e viceversa."))
            Return False
          End If
        End If
        If (ckSconto6.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edSc6Da) And NTSCInt(edQuantDa.Text) <= NTSCInt(edSc6A)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edSc6Da) And NTSCInt(edQuantA.Text) <= NTSCInt(edSc6A)) Or (NTSCInt(edSc6Da.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc6Da.Text) <= NTSCInt(edQuantA)) Or (NTSCInt(edSc6A.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc6A.Text) <= NTSCInt(edQuantA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260183400000, "La posizione del campo quantità non deve essere inclusa nella posizione del campo sconto 6 e viceversa."))
            Return False
          End If
        End If
        If (ckDatcons.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edQuantDa.Text) <= NTSCInt(edDatconsA)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edQuantA.Text) <= NTSCInt(edDatconsA)) Or (NTSCInt(edDatconsDa.Text) >= NTSCInt(edQuantDa) And NTSCInt(edDatconsDa.Text) <= NTSCInt(edQuantA)) Or (NTSCInt(edDatconsA.Text) >= NTSCInt(edQuantDa) And NTSCInt(edDatconsA.Text) <= NTSCInt(edQuantA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129038029320052039, "La posizione del campo quantità non deve essere inclusa nella posizione del campo data consegna e viceversa."))
            Return False
          End If
        End If
        If (ckPrezzo.Checked) And (ckSconto1.Checked) Then
          If (NTSCInt(edSc1Da.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc1Da.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edSc1A.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc1A.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edPreDa.Text) >= NTSCInt(edSc1Da) And NTSCInt(edPreDa.Text) <= NTSCInt(edSc1A)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edSc1Da) And NTSCInt(edPreA.Text) <= NTSCInt(edSc1A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260192292000, "La posizione del campo sconto 1 non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Return False
          End If
        End If
        If (ckPrezzo.Checked) And (ckSconto2.Checked) Then
          If (NTSCInt(edSc2Da.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc2Da.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edSc2A.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc2A.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edPreDa.Text) >= NTSCInt(edSc2Da) And NTSCInt(edPreDa.Text) <= NTSCInt(edSc2A)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edSc2Da) And NTSCInt(edPreA.Text) <= NTSCInt(edSc2A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260201184000, "La posizione del campo sconto 2 non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Return False
          End If
        End If
        If (ckPrezzo.Checked) And (ckSconto3.Checked) Then
          If (NTSCInt(edSc3Da.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc3Da.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edSc3A.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc3A.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edPreDa.Text) >= NTSCInt(edSc3Da) And NTSCInt(edPreDa.Text) <= NTSCInt(edSc3A)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edSc3Da) And NTSCInt(edPreA.Text) <= NTSCInt(edSc3A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260210388000, "La posizione del campo sconto 3 non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Return False
          End If
        End If
        If (ckPrezzo.Checked) And (ckSconto4.Checked) Then
          If (NTSCInt(edSc4Da.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc4Da.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edSc4A.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc4A.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edPreDa.Text) >= NTSCInt(edSc4Da) And NTSCInt(edPreDa.Text) <= NTSCInt(edSc4A)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edSc4Da) And NTSCInt(edPreA.Text) <= NTSCInt(edSc4A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260259216000, "La posizione del campo sconto 4 non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Return False
          End If
        End If
        If (ckPrezzo.Checked) And (ckSconto5.Checked) Then
          If (NTSCInt(edSc5Da.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc5Da.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edSc5A.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc5A.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edPreDa.Text) >= NTSCInt(edSc5Da) And NTSCInt(edPreDa.Text) <= NTSCInt(edSc5A)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edSc5Da) And NTSCInt(edPreA.Text) <= NTSCInt(edSc5A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260223648000, "La posizione del campo sconto 5 non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Return False
          End If
        End If
        If (ckPrezzo.Checked) And (ckSconto6.Checked) Then
          If (NTSCInt(edSc6Da.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc6Da.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edSc6A.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc6A.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edPreDa.Text) >= NTSCInt(edSc6Da) And NTSCInt(edPreDa.Text) <= NTSCInt(edSc6A)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edSc6Da) And NTSCInt(edPreA.Text) <= NTSCInt(edSc6A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260249232000, "La posizione del campo sconto 6 non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Return False
          End If
        End If
        If (ckPrezzo.Checked) And (ckDatcons.Checked) Then
          If (NTSCInt(edDatconsDa.Text) >= NTSCInt(edPreDa) And NTSCInt(edDatconsDa.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edDatconsA.Text) >= NTSCInt(edPreDa) And NTSCInt(edDatconsA.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edPreDa.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edPreDa.Text) <= NTSCInt(edDatconsA)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edPreA.Text) <= NTSCInt(edDatconsA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129038030402922184, "La posizione del campo data consegna non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Return False
          End If
        End If
      End If    'If Not opFixed.Checked Then

      Dim lCount As Integer = 0
      'Ulteriori test pre salvataggio Import
      If edNomeFileI.Text.Trim <> "" Then
        dtrRowI = oCleDocu.dttImport.Select("", "xx_posi")
        For z As Integer = 0 To dtrRowI.Length - 1
          If NTSCInt(dtrRowI(z)!xx_posi) <> z + 1 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129249492603606646, "Le righe di importazione non sono in progressione" & vbCrLf & "o contengono dei buchi nella numerazione della posizione."))
            Return False
          End If
          Select Case NTSCStr(dtrRowI(z)!xx_nomei)
            Case "art" : lCount += 1
            Case "acf" : lCount += 1
            Case "bar" : lCount += 1
            Case "all" : lCount += 1
          End Select
        Next
        ' Non è stato scelto di importare il codice articolo o ne sono stati messi troppi di riferimetno all'articolo
        If lCount = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129249492626420606, "Non sono stati messi riferimenti all'articolo in importazione." & vbCrLf & "Specificare una voce tra 'Articolo', 'Barcode' o 'Art. C/F'"))
          Return False
        ElseIf lCount > 1 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129249492644077986, "Presenti più riferimenti all'articolo in importazione. Specificare solo una voce tra 'Articolo', 'Barcode' o 'Art. C/F'"))
          Return False
        End If
      End If
      If edNomeFileE.Text.Trim <> "" Then
        'Ulteriori test pre salvataggio Export
        dtrRowE = oCleDocu.dttExport.Select("", "xx_pose")
        lCount = 0
        For z As Integer = 0 To dtrRowE.Length - 1
          If NTSCInt(dtrRowE(z)!xx_pose) <> z + 1 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129248848155938667, "Le righe di esportazione non sono in progressione" & vbCrLf & "o contengono dei buchi nella numerazione della posizione."))
            Return False
          End If
          Select Case NTSCStr(dtrRowE(z)!xx_nomee)
            Case "art" : lCount += 1
          End Select
        Next
        If lCount = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129248846566971403, "Non sono stati messi riferimenti all'articolo in esportazione." & vbCrLf & "Specificare su una riga 'Articolo'"))
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If CheckSelection() = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      '--- Ora è il momento di salvare le informazioni nel registro
      '--- nessuna dipendenza operatore/azienda/tipodocumento
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermExecute", IIf(ckExecute.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      If ckExecute.Checked Then
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermCommand", lbCommand.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermSilent", IIf(ckSilent.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      End If
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermFileName", edNomeFile.Text, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDeleteAfter", IIf(ckDelFile.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermIgnoreFirstRow", IIf(ckIgnore1rk.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermShowLogErr", IIf(ckShowLog.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermFileType", IIf(opFixed.Checked, "F", "S").ToString, " ", "...", "...", "...")
      If opDelim.Checked Then
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermFieldSep", edSepField.Text, " ", "...", "...", "...")
      End If
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDecimalSep", edDecSep.Text, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermTipoArt", IIf(opCodart.Checked, "C", IIf(opBarcode.Checked, "B", "A")).ToString, " ", "...", "...", "...")
      If opCodartBarcodeCodarfo.Checked Then
        'se ho selezionato sia articolo che 'articolo o barcode'
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermTipoArt", "X", " ", "...", "...", "...")
      End If
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermPrezzi", IIf(ckPrezzo.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermQuant", IIf(ckQuant.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDescr", IIf(ckDescrizione.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDesint", IIf(ckDesint.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont1", IIf(ckSconto1.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont2", IIf(ckSconto2.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont3", IIf(ckSconto3.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont4", IIf(ckSconto4.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont5", IIf(ckSconto5.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont6", IIf(ckSconto6.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDatcons", IIf(ckDatcons.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      If Not opFixed.Checked Then
        'posizioni
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermCodartPos", edPosArt.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDescrPos", edPosDescr.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDesintPos", edPosDesint.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermQuantPos", edPosQua.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermPrezziPos", edPosPre.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont1Pos", edPosSc1.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont2Pos", edPosSc2.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont3Pos", edPosSc3.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont4Pos", edPosSc4.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont5Pos", edPosSc5.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont6Pos", edPosSc6.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDatconsPos", edPosDatcons.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermCodartStart", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermCodartStop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDescrStart", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDescrStop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDesintStart", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDesintStop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermQuantStart", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermQuantStop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermPrezziStart", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermPrezziStop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont1Start", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont2Start", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont3Start", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont4Start", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont5Start", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont6Start", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont1Stop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont2Stop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont3Stop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont4Stop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont5Stop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont6Stop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDatconsStop", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDatconsStop", "0", " ", "...", "...", "...")
      Else
        'da, a 
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermCodartPos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDescrPos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDesintPos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermQuantPos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermPrezziPos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont1Pos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont2Pos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont3Pos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont4Pos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont5Pos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont6Pos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDatconsPos", "0", " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermCodartStart", edArtDa.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermCodartStop", edArtA.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDescrStart", edDescrDa.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDescrStop", edDescrA.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDesintStart", edDesintDa.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDesintStop", edDesintA.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermQuantStart", edQuantDa.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermQuantStop", edQuantA.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermPrezziStart", edPreDa.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermPrezziStop", edPreA.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont1Start", edSc1Da.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont2Start", edSc2Da.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont3Start", edSc3Da.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont4Start", edSc4Da.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont5Start", edSc5Da.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont6Start", edSc6Da.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont1Stop", edSc1A.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont2Stop", edSc2A.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont3Stop", edSc3A.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont4Stop", edSc4A.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont5Stop", edSc5A.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont6Stop", edSc6A.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDatconsStart", edDatconsDa.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDatconsStop", edDatconsA.Text, " ", "...", "...", "...")
      End If
      'scrive i divisori
      If ckQuant.Checked Then
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivQuant", edDivQuant.Text, " ", "...", "...", "...")
      Else
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivQuant", "1", " ", "...", "...", "...")
      End If
      If ckPrezzo.Checked Then
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivPrezzi", edDivPrezzo.Text, " ", "...", "...", "...")
      Else
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivPrezzi", "1", " ", "...", "...", "...")
      End If
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermBarcodeConfez", IIf(ckBarcodeConfez.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      If ckSconto1.Checked Then
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivScont1", edDivSc1.Text, " ", "...", "...", "...")
      Else
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivScont1", "1", " ", "...", "...", "...")
      End If
      If ckSconto2.Checked Then
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivScont2", edDivSc2.Text, " ", "...", "...", "...")
      Else
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivScont2", "2", " ", "...", "...", "...")
      End If
      If ckSconto3.Checked Then
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivScont3", edDivSc3.Text, " ", "...", "...", "...")
      Else
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivScont3", "3", " ", "...", "...", "...")
      End If
      If ckSconto4.Checked Then
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivScont4", edDivSc4.Text, " ", "...", "...", "...")
      Else
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivScont4", "4", " ", "...", "...", "...")
      End If
      If ckSconto5.Checked Then
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivScont5", edDivSc5.Text, " ", "...", "...", "...")
      Else
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivScont5", "5", " ", "...", "...", "...")
      End If
      If ckSconto6.Checked Then
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivScont6", edDivSc6.Text, " ", "...", "...", "...")
      Else
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDivScont6", "6", " ", "...", "...", "...")
      End If

      'Import
      If edNomeFileI.Text.Trim <> "" Then
        strReg = IIf(ckExecuteI.Checked, "-1", "0").ToString & "|" & lbCommandI.Text & "|" & IIf(ckSilentI.Checked, "-1", "0").ToString & "|" & _
                 edNomeFileI.Text & "|" & IIf(ckDelFileI.Checked, "-1", "0").ToString & "|" & IIf(ckIgnore1rkI.Checked, "-1", "0").ToString & "|" & _
                 IIf(ckPrezziI.Checked, "-1", "0").ToString & "|" & IIf(ckScontiI.Checked, "-1", "0").ToString & "|" & IIf(ckProvvI.Checked, "-1", "0").ToString & "|" & _
                 IIf(ckShowLogI.Checked, "-1", "0").ToString & "|" & IIf(opDelimI.Checked, "S", "F").ToString & "|" & edSepFieldI.Text & "|" & _
                 edDecSepI.Text & "|" & IIf(ckMatricI.Checked, "-1", "0").ToString & "|" & edMatricI.Text & "|" & edQtaMatrI.Text & "|" & _
                 IIf(ckTagliaI.Checked, "-1", "0").ToString & "|" & edTagliaI.Text & "|" & edQtaTagliaI.Text & "|" & IIf(ckTipoRigaI.Checked, "-1", "0").ToString & "|"

        'Righe import
        For z As Integer = 0 To dtrRowI.Length - 1
          If z <> 0 Then strReg &= "$"
          With dtrRowI(z)
            strReg &= NTSCStr(!xx_nomei) & "-" & NTSCInt(!xx_posi) & "-" & NTSCInt(!xx_luni) & "-" & NTSCInt(!xx_divi)
          End With
        Next
      Else
        strReg = ""
      End If
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "Import01", strReg, " ", "...", "...", "...")

      'Export
      'Ulteriori test pre salvataggio Export
      If edNomeFileE.Text.Trim <> "" Then
        strReg = IIf(ckExecuteE.Checked, "-1", "0").ToString & "|" & lbCommandE.Text & "|" & IIf(ckSilentE.Checked, "-1", "0").ToString & "|" & _
                 edNomeFileE.Text & "|" & IIf(ckGenTracciatoE.Checked, "-1", "0").ToString & "|" & IIf(ckShowLogE.Checked, "-1", "0").ToString & _
                 "|" & IIf(opDelimE.Checked, "S", "F").ToString & "|" & edSepFieldE.Text & "|" & IIf(ckMatricE.Checked, "-1", "0").ToString & _
                 "|" & edMatrE.Text & "|" & edQtaMatrE.Text & "|" & IIf(ckTagliaE.Checked, "-1", "0").ToString & "|" & _
                 edTagliaE.Text & "|" & edQtaTagliaE.Text & "|" & IIf(ckTipoRigaE.Checked, "-1", "0").ToString & "|" & IIf(ckRimuoviVbcrlf.Checked, "-1", "0").ToString & "|"

        'Righe export
        For z As Integer = 0 To dtrRowE.Length - 1
          If z <> 0 Then strReg &= "$"
          With dtrRowE(z)
            strReg &= NTSCStr(!xx_nomee) & "-" & NTSCInt(!xx_pose) & "-" & NTSCInt(!xx_lune) & "-" & NTSCInt(!xx_dive)
          End With
        Next
      Else
        strReg = ""
      End If
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "Export01", strReg, " ", "...", "...", "...")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Function

  Public Overridable Function CheckSelection() As Boolean
    Dim strTmp As String = ""
    Dim strT As String = oApp.Tr(Me, 130689231964449661, " non può essere zero.")

    Try
      '--------------------------------------------------------------------------------------------------------------
      If edArtDa.Enabled = True Then
        If opCodartBarcodeCodarfo.Checked = True Then strTmp = opCodartBarcodeCodarfo.Text
        If opCodart.Checked = True Then strTmp = opCodart.Text
        If opBarcode.Checked = True Then strTmp = opBarcode.Text
        If opCodarfo.Checked = True Then strTmp = opCodarfo.Text
      End If
      If edArtDa.Enabled = True Then
        If edArtDa.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689229609229311, "Attenzione!" & vbCrLf & _
            lbArtDa.Text & " " & strTmp & strT))
          edArtDa.Focus()
          Return False
        End If
      End If
      If edArtA.Enabled = True Then
        If edArtA.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689233049888001, "Attenzione!" & vbCrLf & _
            lbArtA.Text & " " & strTmp & strT))
          edArtA.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edDescrDa.Enabled = True Then
        If edDescrDa.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689233712482311, "Attenzione!" & vbCrLf & _
            lbDescrDa.Text & " " & strTmp & strT))
          edDescrDa.Focus()
          Return False
        End If
      End If
      If edDescrA.Enabled = True Then
        If edDescrA.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689233810981866, "Attenzione!" & vbCrLf & _
            lbDescrA.Text & " " & strTmp & strT))
          edDescrA.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edDesintDa.Enabled = True Then
        If edDesintDa.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689234020402056, "Attenzione!" & vbCrLf & _
            lbDesintDa.Text & " " & strTmp & strT))
          edDesintDa.Focus()
          Return False
        End If
      End If
      If edDesintA.Enabled = True Then
        If edDesintA.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689234301839121, "Attenzione!" & vbCrLf & _
            lbDesintA.Text & " " & strTmp & strT))
          edDesintA.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edQuantDa.Enabled = True Then
        If edQuantDa.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689234632657791, "Attenzione!" & vbCrLf & _
            lbQuantDa.Text & " " & strTmp & strT))
          edQuantDa.Focus()
          Return False
        End If
      End If
      If edQuantA.Enabled = True Then
        If edQuantA.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689234812792746, "Attenzione!" & vbCrLf & _
            lbQuantA.Text & " " & strTmp & strT))
          edQuantA.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edPreDa.Enabled = True Then
        If edPreDa.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689235035132031, "Attenzione!" & vbCrLf & _
            lbPreDa.Text & " " & strTmp & strT))
          edPreDa.Focus()
          Return False
        End If
      End If
      If edPreA.Enabled = True Then
        If edPreA.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689235207386631, "Attenzione!" & vbCrLf & _
            lbPreA.Text & " " & strTmp & strT))
          edPreA.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edSc1Da.Enabled = True Then
        If edSc1Da.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689235509740326, "Attenzione!" & vbCrLf & _
            lbSc1Da.Text & " " & strTmp & strT))
          edSc1Da.Focus()
          Return False
        End If
      End If
      If edSc1A.Enabled = True Then
        If edSc1A.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689235672024861, "Attenzione!" & vbCrLf & _
            lbSc1A.Text & " " & strTmp & strT))
          edSc1A.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edSc2Da.Enabled = True Then
        If edSc2Da.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689235934967016, "Attenzione!" & vbCrLf & _
            lbSc2Da.Text & " " & strTmp & strT))
          edSc2Da.Focus()
          Return False
        End If
      End If
      If edSc2A.Enabled = True Then
        If edSc2A.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689235959135391, "Attenzione!" & vbCrLf & _
            lbSc2A.Text & " " & strTmp & strT))
          edSc2A.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edSc3Da.Enabled = True Then
        If edSc3Da.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689236170850356, "Attenzione!" & vbCrLf & _
            lbSc3Da.Text & " " & strTmp & strT))
          edSc3Da.Focus()
          Return False
        End If
      End If
      If edSc3A.Enabled = True Then
        If edSc3A.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689236195790166, "Attenzione!" & vbCrLf & _
            lbSc3A.Text & " " & strTmp & strT))
          edSc3A.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edSc4Da.Enabled = True Then
        If edSc4Da.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689236519704981, "Attenzione!" & vbCrLf & _
            lbSc4Da.Text & " " & strTmp & strT))
          edSc4Da.Focus()
          Return False
        End If
      End If
      If edSc4A.Enabled = True Then
        If edSc4A.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689236542174246, "Attenzione!" & vbCrLf & _
            lbSc4A.Text & " " & strTmp & strT))
          edSc4A.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edSc5Da.Enabled = True Then
        If edSc5Da.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689236570248621, "Attenzione!" & vbCrLf & _
            lbSc5Da.Text & " " & strTmp & strT))
          edSc5Da.Focus()
          Return False
        End If
      End If
      If edSc5A.Enabled = True Then
        If edSc5A.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689236599426441, "Attenzione!" & vbCrLf & _
            lbSc5A.Text & " " & strTmp & strT))
          edSc5A.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edSc6Da.Enabled = True Then
        If edSc6Da.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689236629502641, "Attenzione!" & vbCrLf & _
            lbSc6Da.Text & " " & strTmp & strT))
          edSc6Da.Focus()
          Return False
        End If
      End If
      If edSc6A.Enabled = True Then
        If edSc6A.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689236650526686, "Attenzione!" & vbCrLf & _
            lbSc6A.Text & " " & strTmp & strT))
          edSc6A.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If edDatconsDa.Enabled = True Then
        If edDatconsDa.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689237503284606, "Attenzione!" & vbCrLf & _
            lbDatconsDa.Text & " " & strTmp & strT))
          edDatconsDa.Focus()
          Return False
        End If
      End If
      If edDatconsA.Enabled = True Then
        If edDatconsA.Text = "0" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130689237527609221, "Attenzione!" & vbCrLf & _
            lbDatconsA.Text & " " & strTmp & strT))
          edDatconsA.Focus()
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

End Class
