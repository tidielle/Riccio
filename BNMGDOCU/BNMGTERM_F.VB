Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGTERM_F
  Public oCallParams As CLE__CLDP
  Public oCleDocu As CLEMGDOCU

  Public bInLoad As Boolean = False
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
  Public WithEvents lbPosQua As NTSInformatica.NTSLabel
  Public WithEvents lbPosDescr As NTSInformatica.NTSLabel
  Public WithEvents lbPosArt As NTSInformatica.NTSLabel
  Public WithEvents edPosArt As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPosPre As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPosQua As NTSInformatica.NTSTextBoxNum
  Public WithEvents edPosDescr As NTSInformatica.NTSTextBoxNum
  Public WithEvents ckBarcodeConfez As NTSInformatica.NTSCheckBox
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
  Public WithEvents lbAvviso As NTSInformatica.NTSLabel
  Public WithEvents ckTipoRigaE As NTSInformatica.NTSCheckBox
  Public WithEvents ckTipoRigaI As NTSInformatica.NTSCheckBox
  Public WithEvents ckRimuoviVbcrlf As NTSInformatica.NTSCheckBox
  Public WithEvents cmdOk As NTSInformatica.NTSButton
#End Region

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.ckExecute = New NTSInformatica.NTSCheckBox
    Me.lbCommand = New NTSInformatica.NTSLabel
    Me.cmdCommand = New NTSInformatica.NTSButton
    Me.pnMain = New NTSInformatica.NTSPanel
    Me.tcImport = New NTSInformatica.NTSTabControl
    Me.tpTerminale = New NTSInformatica.NTSTabPage
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    Me.frmTipofile = New NTSInformatica.NTSGroupBox
    Me.edDecSep = New NTSInformatica.NTSTextBoxStr
    Me.lbDecSep = New NTSInformatica.NTSLabel
    Me.edSepField = New NTSInformatica.NTSTextBoxStr
    Me.opDelim = New NTSInformatica.NTSRadioButton
    Me.opFixed = New NTSInformatica.NTSRadioButton
    Me.frmCampi = New NTSInformatica.NTSGroupBox
    Me.cbCampi = New NTSInformatica.NTSComboBox
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
    Me.lbAvviso = New NTSInformatica.NTSLabel
    CType(Me.ckExecute.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnMain.SuspendLayout()
    CType(Me.tcImport, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tcImport.SuspendLayout()
    Me.tpTerminale.SuspendLayout()
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.frmTipofile, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frmTipofile.SuspendLayout()
    CType(Me.edDecSep.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSepField.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opDelim.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opFixed.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frmCampi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frmCampi.SuspendLayout()
    CType(Me.cbCampi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatconsA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatconsDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosDatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDatcons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDesintA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDesintDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPosDesint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDesint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    CType(Me.ckSilent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckShowLog.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNomeFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDelFile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckIgnore1rk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tpExport.SuspendLayout()
    CType(Me.pnExport, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnExport.SuspendLayout()
    CType(Me.ckTipoRigaE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(528, 48)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(119, 24)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(528, 24)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(119, 24)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'ckExecute
    '
    Me.ckExecute.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckExecute.Location = New System.Drawing.Point(3, 1)
    Me.ckExecute.Name = "ckExecute"
    Me.ckExecute.NTSCheckValue = "S"
    Me.ckExecute.NTSUnCheckValue = "N"
    Me.ckExecute.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckExecute.Properties.Appearance.Options.UseBackColor = True
    Me.ckExecute.Properties.Caption = "Esegui il comando"
    Me.ckExecute.Size = New System.Drawing.Size(112, 19)
    Me.ckExecute.TabIndex = 3
    '
    'lbCommand
    '
    Me.lbCommand.BackColor = System.Drawing.Color.Transparent
    Me.lbCommand.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCommand.Location = New System.Drawing.Point(121, 0)
    Me.lbCommand.Name = "lbCommand"
    Me.lbCommand.NTSDbField = ""
    Me.lbCommand.Size = New System.Drawing.Size(343, 20)
    Me.lbCommand.TabIndex = 4
    Me.lbCommand.Tooltip = ""
    Me.lbCommand.UseMnemonic = False
    '
    'cmdCommand
    '
    Me.cmdCommand.ImageText = ""
    Me.cmdCommand.Location = New System.Drawing.Point(470, 0)
    Me.cmdCommand.Name = "cmdCommand"
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
    Me.pnMain.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnMain.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnMain.Location = New System.Drawing.Point(0, 0)
    Me.pnMain.Name = "pnMain"
    Me.pnMain.NTSActiveTrasparency = True
    Me.pnMain.Size = New System.Drawing.Size(524, 390)
    Me.pnMain.TabIndex = 6
    Me.pnMain.Text = "NtsPanel1"
    '
    'tcImport
    '
    Me.tcImport.Location = New System.Drawing.Point(5, 0)
    Me.tcImport.Name = "tcImport"
    Me.tcImport.SelectedTabPage = Me.tpTerminale
    Me.tcImport.Size = New System.Drawing.Size(516, 388)
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
    Me.tpTerminale.Size = New System.Drawing.Size(507, 358)
    Me.tpTerminale.Text = "&1 - File terminale"
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
    Me.NtsPanel1.Size = New System.Drawing.Size(507, 358)
    Me.NtsPanel1.TabIndex = 15
    Me.NtsPanel1.Text = "NtsPanel1"
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
    Me.frmTipofile.Location = New System.Drawing.Point(6, 44)
    Me.frmTipofile.Name = "frmTipofile"
    Me.frmTipofile.Size = New System.Drawing.Size(192, 84)
    Me.frmTipofile.TabIndex = 9
    Me.frmTipofile.Text = "Tipo file"
    '
    'edDecSep
    '
    Me.edDecSep.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDecSep.EditValue = ","
    Me.edDecSep.Location = New System.Drawing.Point(158, 60)
    Me.edDecSep.Name = "edDecSep"
    Me.edDecSep.NTSDbField = ""
    Me.edDecSep.NTSForzaVisZoom = False
    Me.edDecSep.NTSOldValue = ","
    Me.edDecSep.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDecSep.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.lbDecSep.Location = New System.Drawing.Point(22, 63)
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
    Me.edSepField.Location = New System.Drawing.Point(158, 39)
    Me.edSepField.Name = "edSepField"
    Me.edSepField.NTSDbField = ""
    Me.edSepField.NTSForzaVisZoom = False
    Me.edSepField.NTSOldValue = ";"
    Me.edSepField.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSepField.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.opDelim.Properties.Caption = "Separatore di campo"
    Me.opDelim.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDelim.Size = New System.Drawing.Size(127, 19)
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
    Me.frmCampi.Controls.Add(Me.cbCampi)
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
    Me.frmCampi.Cursor = System.Windows.Forms.Cursors.Default
    Me.frmCampi.Location = New System.Drawing.Point(4, 129)
    Me.frmCampi.Name = "frmCampi"
    Me.frmCampi.Size = New System.Drawing.Size(499, 225)
    Me.frmCampi.TabIndex = 14
    Me.frmCampi.Text = "Campi"
    '
    'cbCampi
    '
    Me.cbCampi.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbCampi.DataSource = Nothing
    Me.cbCampi.DisplayMember = ""
    Me.cbCampi.Location = New System.Drawing.Point(20, 42)
    Me.cbCampi.Name = "cbCampi"
    Me.cbCampi.NTSDbField = ""
    Me.cbCampi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbCampi.Properties.DropDownRows = 30
    Me.cbCampi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbCampi.SelectedValue = ""
    Me.cbCampi.Size = New System.Drawing.Size(129, 20)
    Me.cbCampi.TabIndex = 675
    Me.cbCampi.ValueMember = ""
    '
    'lbSeparatoriDate
    '
    Me.lbSeparatoriDate.AutoSize = True
    Me.lbSeparatoriDate.BackColor = System.Drawing.Color.Transparent
    Me.lbSeparatoriDate.Location = New System.Drawing.Point(391, 185)
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
    Me.lbDatconsA.Location = New System.Drawing.Point(326, 185)
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
    Me.lbDatconsDa.Location = New System.Drawing.Point(253, 185)
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
    Me.edDatconsA.Location = New System.Drawing.Point(346, 182)
    Me.edDatconsA.Name = "edDatconsA"
    Me.edDatconsA.NTSDbField = ""
    Me.edDatconsA.NTSFormat = "0"
    Me.edDatconsA.NTSForzaVisZoom = False
    Me.edDatconsA.NTSOldValue = "9999"
    Me.edDatconsA.Properties.Appearance.Options.UseTextOptions = True
    Me.edDatconsA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDatconsA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatconsA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatconsA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatconsA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatconsA.Size = New System.Drawing.Size(38, 20)
    Me.edDatconsA.TabIndex = 114
    '
    'edDatconsDa
    '
    Me.edDatconsDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatconsDa.EditValue = "1"
    Me.edDatconsDa.Location = New System.Drawing.Point(278, 182)
    Me.edDatconsDa.Name = "edDatconsDa"
    Me.edDatconsDa.NTSDbField = ""
    Me.edDatconsDa.NTSFormat = "0"
    Me.edDatconsDa.NTSForzaVisZoom = False
    Me.edDatconsDa.NTSOldValue = "1"
    Me.edDatconsDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edDatconsDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDatconsDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatconsDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatconsDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatconsDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatconsDa.Size = New System.Drawing.Size(42, 20)
    Me.edDatconsDa.TabIndex = 113
    '
    'lbPosDatcons
    '
    Me.lbPosDatcons.AutoSize = True
    Me.lbPosDatcons.BackColor = System.Drawing.Color.Transparent
    Me.lbPosDatcons.Location = New System.Drawing.Point(155, 185)
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
    Me.edPosDatcons.Location = New System.Drawing.Point(210, 182)
    Me.edPosDatcons.Name = "edPosDatcons"
    Me.edPosDatcons.NTSDbField = ""
    Me.edPosDatcons.NTSFormat = "0"
    Me.edPosDatcons.NTSForzaVisZoom = False
    Me.edPosDatcons.NTSOldValue = "1"
    Me.edPosDatcons.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosDatcons.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosDatcons.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosDatcons.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosDatcons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosDatcons.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosDatcons.Size = New System.Drawing.Size(37, 20)
    Me.edPosDatcons.TabIndex = 111
    '
    'ckDatcons
    '
    Me.ckDatcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDatcons.Location = New System.Drawing.Point(20, 183)
    Me.ckDatcons.Name = "ckDatcons"
    Me.ckDatcons.NTSCheckValue = "S"
    Me.ckDatcons.NTSUnCheckValue = "N"
    Me.ckDatcons.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDatcons.Properties.Appearance.Options.UseBackColor = True
    Me.ckDatcons.Properties.Caption = "Data consegna"
    Me.ckDatcons.Size = New System.Drawing.Size(121, 19)
    Me.ckDatcons.TabIndex = 110
    '
    'lbDodici
    '
    Me.lbDodici.AutoSize = True
    Me.lbDodici.BackColor = System.Drawing.Color.Transparent
    Me.lbDodici.Location = New System.Drawing.Point(4, 185)
    Me.lbDodici.Name = "lbDodici"
    Me.lbDodici.NTSDbField = ""
    Me.lbDodici.Size = New System.Drawing.Size(17, 13)
    Me.lbDodici.TabIndex = 109
    Me.lbDodici.Text = "8."
    Me.lbDodici.Tooltip = ""
    Me.lbDodici.UseMnemonic = False
    '
    'lbDesintA
    '
    Me.lbDesintA.AutoSize = True
    Me.lbDesintA.BackColor = System.Drawing.Color.Transparent
    Me.lbDesintA.Location = New System.Drawing.Point(326, 85)
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
    Me.lbDesintDa.Location = New System.Drawing.Point(253, 85)
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
    Me.edDesintA.Location = New System.Drawing.Point(346, 82)
    Me.edDesintA.Name = "edDesintA"
    Me.edDesintA.NTSDbField = ""
    Me.edDesintA.NTSFormat = "0"
    Me.edDesintA.NTSForzaVisZoom = False
    Me.edDesintA.NTSOldValue = "9999"
    Me.edDesintA.Properties.Appearance.Options.UseTextOptions = True
    Me.edDesintA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDesintA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDesintA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDesintA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDesintA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDesintA.Size = New System.Drawing.Size(38, 20)
    Me.edDesintA.TabIndex = 106
    '
    'edDesintDa
    '
    Me.edDesintDa.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDesintDa.EditValue = "1"
    Me.edDesintDa.Location = New System.Drawing.Point(278, 82)
    Me.edDesintDa.Name = "edDesintDa"
    Me.edDesintDa.NTSDbField = ""
    Me.edDesintDa.NTSFormat = "0"
    Me.edDesintDa.NTSForzaVisZoom = False
    Me.edDesintDa.NTSOldValue = "1"
    Me.edDesintDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edDesintDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDesintDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDesintDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDesintDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDesintDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDesintDa.Size = New System.Drawing.Size(42, 20)
    Me.edDesintDa.TabIndex = 105
    '
    'lbPosDesint
    '
    Me.lbPosDesint.AutoSize = True
    Me.lbPosDesint.BackColor = System.Drawing.Color.Transparent
    Me.lbPosDesint.Location = New System.Drawing.Point(155, 85)
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
    Me.edPosDesint.Location = New System.Drawing.Point(210, 82)
    Me.edPosDesint.Name = "edPosDesint"
    Me.edPosDesint.NTSDbField = ""
    Me.edPosDesint.NTSFormat = "0"
    Me.edPosDesint.NTSForzaVisZoom = False
    Me.edPosDesint.NTSOldValue = "1"
    Me.edPosDesint.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosDesint.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosDesint.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosDesint.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosDesint.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosDesint.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosDesint.Size = New System.Drawing.Size(37, 20)
    Me.edPosDesint.TabIndex = 103
    '
    'ckDesint
    '
    Me.ckDesint.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDesint.Location = New System.Drawing.Point(20, 83)
    Me.ckDesint.Name = "ckDesint"
    Me.ckDesint.NTSCheckValue = "S"
    Me.ckDesint.NTSUnCheckValue = "N"
    Me.ckDesint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDesint.Properties.Appearance.Options.UseBackColor = True
    Me.ckDesint.Properties.Caption = "Descrizione interna"
    Me.ckDesint.Size = New System.Drawing.Size(121, 19)
    Me.ckDesint.TabIndex = 102
    '
    'lbTre
    '
    Me.lbTre.AutoSize = True
    Me.lbTre.BackColor = System.Drawing.Color.Transparent
    Me.lbTre.Location = New System.Drawing.Point(4, 85)
    Me.lbTre.Name = "lbTre"
    Me.lbTre.NTSDbField = ""
    Me.lbTre.Size = New System.Drawing.Size(17, 13)
    Me.lbTre.TabIndex = 101
    Me.lbTre.Text = "3."
    Me.lbTre.Tooltip = ""
    Me.lbTre.UseMnemonic = False
    '
    'lbDivSc2
    '
    Me.lbDivSc2.AutoSize = True
    Me.lbDivSc2.BackColor = System.Drawing.Color.Transparent
    Me.lbDivSc2.Location = New System.Drawing.Point(391, 165)
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
    Me.edDivSc2.Location = New System.Drawing.Point(444, 162)
    Me.edDivSc2.Name = "edDivSc2"
    Me.edDivSc2.NTSDbField = ""
    Me.edDivSc2.NTSFormat = "0"
    Me.edDivSc2.NTSForzaVisZoom = False
    Me.edDivSc2.NTSOldValue = "1"
    Me.edDivSc2.Properties.Appearance.Options.UseTextOptions = True
    Me.edDivSc2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDivSc2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDivSc2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDivSc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDivSc2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDivSc2.Size = New System.Drawing.Size(47, 20)
    Me.edDivSc2.TabIndex = 59
    '
    'lbSc2A
    '
    Me.lbSc2A.AutoSize = True
    Me.lbSc2A.BackColor = System.Drawing.Color.Transparent
    Me.lbSc2A.Location = New System.Drawing.Point(326, 165)
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
    Me.lbSc2Da.Location = New System.Drawing.Point(253, 165)
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
    Me.edSc2A.Location = New System.Drawing.Point(346, 162)
    Me.edSc2A.Name = "edSc2A"
    Me.edSc2A.NTSDbField = ""
    Me.edSc2A.NTSFormat = "0"
    Me.edSc2A.NTSForzaVisZoom = False
    Me.edSc2A.NTSOldValue = "9999"
    Me.edSc2A.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc2A.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc2A.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc2A.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.edSc2Da.Location = New System.Drawing.Point(278, 162)
    Me.edSc2Da.Name = "edSc2Da"
    Me.edSc2Da.NTSDbField = ""
    Me.edSc2Da.NTSFormat = "0"
    Me.edSc2Da.NTSForzaVisZoom = False
    Me.edSc2Da.NTSOldValue = "1"
    Me.edSc2Da.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc2Da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc2Da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc2Da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSc2Da.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSc2Da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSc2Da.Size = New System.Drawing.Size(42, 20)
    Me.edSc2Da.TabIndex = 55
    '
    'lbPosSc2
    '
    Me.lbPosSc2.AutoSize = True
    Me.lbPosSc2.BackColor = System.Drawing.Color.Transparent
    Me.lbPosSc2.Location = New System.Drawing.Point(155, 165)
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
    Me.edPosSc2.Location = New System.Drawing.Point(210, 162)
    Me.edPosSc2.Name = "edPosSc2"
    Me.edPosSc2.NTSDbField = ""
    Me.edPosSc2.NTSFormat = "0"
    Me.edPosSc2.NTSForzaVisZoom = False
    Me.edPosSc2.NTSOldValue = "1"
    Me.edPosSc2.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosSc2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosSc2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosSc2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosSc2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosSc2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosSc2.Size = New System.Drawing.Size(37, 20)
    Me.edPosSc2.TabIndex = 53
    '
    'ckSconto2
    '
    Me.ckSconto2.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconto2.Location = New System.Drawing.Point(20, 163)
    Me.ckSconto2.Name = "ckSconto2"
    Me.ckSconto2.NTSCheckValue = "S"
    Me.ckSconto2.NTSUnCheckValue = "N"
    Me.ckSconto2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconto2.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconto2.Properties.Caption = "Sconto 2"
    Me.ckSconto2.Size = New System.Drawing.Size(66, 19)
    Me.ckSconto2.TabIndex = 52
    '
    'lbSette
    '
    Me.lbSette.AutoSize = True
    Me.lbSette.BackColor = System.Drawing.Color.Transparent
    Me.lbSette.Location = New System.Drawing.Point(4, 165)
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
    Me.lbDivSc1.Location = New System.Drawing.Point(391, 145)
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
    Me.edDivSc1.Location = New System.Drawing.Point(444, 142)
    Me.edDivSc1.Name = "edDivSc1"
    Me.edDivSc1.NTSDbField = ""
    Me.edDivSc1.NTSFormat = "0"
    Me.edDivSc1.NTSForzaVisZoom = False
    Me.edDivSc1.NTSOldValue = "1"
    Me.edDivSc1.Properties.Appearance.Options.UseTextOptions = True
    Me.edDivSc1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDivSc1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDivSc1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDivSc1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDivSc1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDivSc1.Size = New System.Drawing.Size(47, 20)
    Me.edDivSc1.TabIndex = 49
    '
    'lbSc1A
    '
    Me.lbSc1A.AutoSize = True
    Me.lbSc1A.BackColor = System.Drawing.Color.Transparent
    Me.lbSc1A.Location = New System.Drawing.Point(326, 145)
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
    Me.lbSc1Da.Location = New System.Drawing.Point(253, 145)
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
    Me.edSc1A.Location = New System.Drawing.Point(346, 142)
    Me.edSc1A.Name = "edSc1A"
    Me.edSc1A.NTSDbField = ""
    Me.edSc1A.NTSFormat = "0"
    Me.edSc1A.NTSForzaVisZoom = False
    Me.edSc1A.NTSOldValue = "9999"
    Me.edSc1A.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc1A.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc1A.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc1A.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.edSc1Da.Location = New System.Drawing.Point(278, 142)
    Me.edSc1Da.Name = "edSc1Da"
    Me.edSc1Da.NTSDbField = ""
    Me.edSc1Da.NTSFormat = "0"
    Me.edSc1Da.NTSForzaVisZoom = False
    Me.edSc1Da.NTSOldValue = "1"
    Me.edSc1Da.Properties.Appearance.Options.UseTextOptions = True
    Me.edSc1Da.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSc1Da.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSc1Da.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSc1Da.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSc1Da.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSc1Da.Size = New System.Drawing.Size(42, 20)
    Me.edSc1Da.TabIndex = 45
    '
    'lbPosSc1
    '
    Me.lbPosSc1.AutoSize = True
    Me.lbPosSc1.BackColor = System.Drawing.Color.Transparent
    Me.lbPosSc1.Location = New System.Drawing.Point(155, 145)
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
    Me.edPosSc1.Location = New System.Drawing.Point(210, 142)
    Me.edPosSc1.Name = "edPosSc1"
    Me.edPosSc1.NTSDbField = ""
    Me.edPosSc1.NTSFormat = "0"
    Me.edPosSc1.NTSForzaVisZoom = False
    Me.edPosSc1.NTSOldValue = "1"
    Me.edPosSc1.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosSc1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosSc1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosSc1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosSc1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosSc1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosSc1.Size = New System.Drawing.Size(37, 20)
    Me.edPosSc1.TabIndex = 43
    '
    'ckSconto1
    '
    Me.ckSconto1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSconto1.Location = New System.Drawing.Point(20, 143)
    Me.ckSconto1.Name = "ckSconto1"
    Me.ckSconto1.NTSCheckValue = "S"
    Me.ckSconto1.NTSUnCheckValue = "N"
    Me.ckSconto1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconto1.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconto1.Properties.Caption = "Sconto 1"
    Me.ckSconto1.Size = New System.Drawing.Size(66, 19)
    Me.ckSconto1.TabIndex = 42
    '
    'lbSei
    '
    Me.lbSei.AutoSize = True
    Me.lbSei.BackColor = System.Drawing.Color.Transparent
    Me.lbSei.Location = New System.Drawing.Point(4, 145)
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
    Me.lbDivPrezzo.Location = New System.Drawing.Point(391, 125)
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
    Me.lbDivQuant.Location = New System.Drawing.Point(391, 105)
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
    Me.edDivPrezzo.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edDivPrezzo.EditValue = "1"
    Me.edDivPrezzo.Enabled = False
    Me.edDivPrezzo.Location = New System.Drawing.Point(444, 122)
    Me.edDivPrezzo.Name = "edDivPrezzo"
    Me.edDivPrezzo.NTSDbField = ""
    Me.edDivPrezzo.NTSFormat = "0"
    Me.edDivPrezzo.NTSForzaVisZoom = False
    Me.edDivPrezzo.NTSOldValue = "1"
    Me.edDivPrezzo.Properties.Appearance.Options.UseTextOptions = True
    Me.edDivPrezzo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDivPrezzo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDivPrezzo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDivPrezzo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDivPrezzo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDivPrezzo.Size = New System.Drawing.Size(47, 20)
    Me.edDivPrezzo.TabIndex = 38
    '
    'edDivQuant
    '
    Me.edDivQuant.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDivQuant.EditValue = "1"
    Me.edDivQuant.Location = New System.Drawing.Point(444, 102)
    Me.edDivQuant.Name = "edDivQuant"
    Me.edDivQuant.NTSDbField = ""
    Me.edDivQuant.NTSFormat = "0"
    Me.edDivQuant.NTSForzaVisZoom = False
    Me.edDivQuant.NTSOldValue = "1"
    Me.edDivQuant.Properties.Appearance.Options.UseTextOptions = True
    Me.edDivQuant.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDivQuant.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDivQuant.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDivQuant.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDivQuant.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDivQuant.Size = New System.Drawing.Size(47, 20)
    Me.edDivQuant.TabIndex = 37
    '
    'lbPreA
    '
    Me.lbPreA.AutoSize = True
    Me.lbPreA.BackColor = System.Drawing.Color.Transparent
    Me.lbPreA.Location = New System.Drawing.Point(326, 125)
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
    Me.lbPreDa.Location = New System.Drawing.Point(253, 125)
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
    Me.edPreA.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edPreA.EditValue = "9999"
    Me.edPreA.Enabled = False
    Me.edPreA.Location = New System.Drawing.Point(346, 122)
    Me.edPreA.Name = "edPreA"
    Me.edPreA.NTSDbField = ""
    Me.edPreA.NTSFormat = "0"
    Me.edPreA.NTSForzaVisZoom = False
    Me.edPreA.NTSOldValue = "9999"
    Me.edPreA.Properties.Appearance.Options.UseTextOptions = True
    Me.edPreA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPreA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPreA.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.edPreDa.Location = New System.Drawing.Point(278, 122)
    Me.edPreDa.Name = "edPreDa"
    Me.edPreDa.NTSDbField = ""
    Me.edPreDa.NTSFormat = "0"
    Me.edPreDa.NTSForzaVisZoom = False
    Me.edPreDa.NTSOldValue = "1"
    Me.edPreDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edPreDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPreDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPreDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPreDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPreDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPreDa.Size = New System.Drawing.Size(42, 20)
    Me.edPreDa.TabIndex = 33
    '
    'lbQuantA
    '
    Me.lbQuantA.AutoSize = True
    Me.lbQuantA.BackColor = System.Drawing.Color.Transparent
    Me.lbQuantA.Location = New System.Drawing.Point(326, 105)
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
    Me.lbQuantDa.Location = New System.Drawing.Point(253, 105)
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
    Me.edQuantA.Location = New System.Drawing.Point(346, 102)
    Me.edQuantA.Name = "edQuantA"
    Me.edQuantA.NTSDbField = ""
    Me.edQuantA.NTSFormat = "0"
    Me.edQuantA.NTSForzaVisZoom = False
    Me.edQuantA.NTSOldValue = "9999"
    Me.edQuantA.Properties.Appearance.Options.UseTextOptions = True
    Me.edQuantA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQuantA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQuantA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQuantA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQuantA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQuantA.Size = New System.Drawing.Size(38, 20)
    Me.edQuantA.TabIndex = 30
    '
    'edQuantDa
    '
    Me.edQuantDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edQuantDa.EditValue = "1"
    Me.edQuantDa.Location = New System.Drawing.Point(278, 102)
    Me.edQuantDa.Name = "edQuantDa"
    Me.edQuantDa.NTSDbField = ""
    Me.edQuantDa.NTSFormat = "0"
    Me.edQuantDa.NTSForzaVisZoom = False
    Me.edQuantDa.NTSOldValue = "1"
    Me.edQuantDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edQuantDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edQuantDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edQuantDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edQuantDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edQuantDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edQuantDa.Size = New System.Drawing.Size(42, 20)
    Me.edQuantDa.TabIndex = 29
    '
    'lbDescrA
    '
    Me.lbDescrA.AutoSize = True
    Me.lbDescrA.BackColor = System.Drawing.Color.Transparent
    Me.lbDescrA.Location = New System.Drawing.Point(326, 65)
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
    Me.lbDescrDa.Location = New System.Drawing.Point(253, 65)
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
    Me.edDescrA.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edDescrA.EditValue = "9999"
    Me.edDescrA.Location = New System.Drawing.Point(346, 62)
    Me.edDescrA.Name = "edDescrA"
    Me.edDescrA.NTSDbField = ""
    Me.edDescrA.NTSFormat = "0"
    Me.edDescrA.NTSForzaVisZoom = False
    Me.edDescrA.NTSOldValue = "9999"
    Me.edDescrA.Properties.Appearance.Options.UseTextOptions = True
    Me.edDescrA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDescrA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescrA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescrA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescrA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescrA.Size = New System.Drawing.Size(38, 20)
    Me.edDescrA.TabIndex = 26
    '
    'edDescrDa
    '
    Me.edDescrDa.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edDescrDa.EditValue = "1"
    Me.edDescrDa.Location = New System.Drawing.Point(278, 62)
    Me.edDescrDa.Name = "edDescrDa"
    Me.edDescrDa.NTSDbField = ""
    Me.edDescrDa.NTSFormat = "0"
    Me.edDescrDa.NTSForzaVisZoom = False
    Me.edDescrDa.NTSOldValue = "1"
    Me.edDescrDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edDescrDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDescrDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescrDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescrDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescrDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescrDa.Size = New System.Drawing.Size(42, 20)
    Me.edDescrDa.TabIndex = 25
    '
    'lbArtA
    '
    Me.lbArtA.AutoSize = True
    Me.lbArtA.BackColor = System.Drawing.Color.Transparent
    Me.lbArtA.Location = New System.Drawing.Point(326, 45)
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
    Me.lbArtDa.Location = New System.Drawing.Point(252, 45)
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
    Me.edArtA.Location = New System.Drawing.Point(346, 42)
    Me.edArtA.Name = "edArtA"
    Me.edArtA.NTSDbField = ""
    Me.edArtA.NTSFormat = "0"
    Me.edArtA.NTSForzaVisZoom = False
    Me.edArtA.NTSOldValue = "9999"
    Me.edArtA.Properties.Appearance.Options.UseTextOptions = True
    Me.edArtA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edArtA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArtA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArtA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArtA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArtA.Size = New System.Drawing.Size(38, 20)
    Me.edArtA.TabIndex = 22
    '
    'edArtDa
    '
    Me.edArtDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edArtDa.EditValue = "1"
    Me.edArtDa.Location = New System.Drawing.Point(278, 42)
    Me.edArtDa.Name = "edArtDa"
    Me.edArtDa.NTSDbField = ""
    Me.edArtDa.NTSFormat = "0"
    Me.edArtDa.NTSForzaVisZoom = False
    Me.edArtDa.NTSOldValue = "1"
    Me.edArtDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edArtDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edArtDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArtDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArtDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArtDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArtDa.Size = New System.Drawing.Size(42, 20)
    Me.edArtDa.TabIndex = 21
    '
    'lbPosPre
    '
    Me.lbPosPre.AutoSize = True
    Me.lbPosPre.BackColor = System.Drawing.Color.Transparent
    Me.lbPosPre.Location = New System.Drawing.Point(155, 125)
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
    Me.lbPosQua.Location = New System.Drawing.Point(155, 105)
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
    Me.lbPosDescr.Location = New System.Drawing.Point(155, 65)
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
    Me.lbPosArt.Location = New System.Drawing.Point(155, 45)
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
    Me.edPosArt.Location = New System.Drawing.Point(210, 42)
    Me.edPosArt.Name = "edPosArt"
    Me.edPosArt.NTSDbField = ""
    Me.edPosArt.NTSFormat = "0"
    Me.edPosArt.NTSForzaVisZoom = False
    Me.edPosArt.NTSOldValue = "1"
    Me.edPosArt.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosArt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosArt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosArt.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.edPosPre.Location = New System.Drawing.Point(210, 122)
    Me.edPosPre.Name = "edPosPre"
    Me.edPosPre.NTSDbField = ""
    Me.edPosPre.NTSFormat = "0"
    Me.edPosPre.NTSForzaVisZoom = False
    Me.edPosPre.NTSOldValue = "1"
    Me.edPosPre.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosPre.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosPre.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosPre.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.edPosQua.Location = New System.Drawing.Point(210, 102)
    Me.edPosQua.Name = "edPosQua"
    Me.edPosQua.NTSDbField = ""
    Me.edPosQua.NTSFormat = "0"
    Me.edPosQua.NTSForzaVisZoom = False
    Me.edPosQua.NTSOldValue = "1"
    Me.edPosQua.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosQua.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosQua.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosQua.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosQua.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosQua.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosQua.Size = New System.Drawing.Size(37, 20)
    Me.edPosQua.TabIndex = 14
    '
    'edPosDescr
    '
    Me.edPosDescr.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edPosDescr.EditValue = "1"
    Me.edPosDescr.Enabled = False
    Me.edPosDescr.Location = New System.Drawing.Point(210, 62)
    Me.edPosDescr.Name = "edPosDescr"
    Me.edPosDescr.NTSDbField = ""
    Me.edPosDescr.NTSFormat = "0"
    Me.edPosDescr.NTSForzaVisZoom = False
    Me.edPosDescr.NTSOldValue = "1"
    Me.edPosDescr.Properties.Appearance.Options.UseTextOptions = True
    Me.edPosDescr.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPosDescr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPosDescr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPosDescr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPosDescr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPosDescr.Size = New System.Drawing.Size(37, 20)
    Me.edPosDescr.TabIndex = 13
    '
    'ckBarcodeConfez
    '
    Me.ckBarcodeConfez.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckBarcodeConfez.Location = New System.Drawing.Point(278, 20)
    Me.ckBarcodeConfez.Name = "ckBarcodeConfez"
    Me.ckBarcodeConfez.NTSCheckValue = "S"
    Me.ckBarcodeConfez.NTSUnCheckValue = "N"
    Me.ckBarcodeConfez.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckBarcodeConfez.Properties.Appearance.Options.UseBackColor = True
    Me.ckBarcodeConfez.Properties.Caption = "Rileva la quantità nel campo colli"
    Me.ckBarcodeConfez.Size = New System.Drawing.Size(182, 19)
    Me.ckBarcodeConfez.TabIndex = 12
    '
    'ckPrezzo
    '
    Me.ckPrezzo.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPrezzo.Location = New System.Drawing.Point(20, 125)
    Me.ckPrezzo.Name = "ckPrezzo"
    Me.ckPrezzo.NTSCheckValue = "S"
    Me.ckPrezzo.NTSUnCheckValue = "N"
    Me.ckPrezzo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPrezzo.Properties.Appearance.Options.UseBackColor = True
    Me.ckPrezzo.Properties.Caption = "Prezzo"
    Me.ckPrezzo.Size = New System.Drawing.Size(56, 19)
    Me.ckPrezzo.TabIndex = 10
    '
    'ckQuant
    '
    Me.ckQuant.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckQuant.EditValue = True
    Me.ckQuant.Location = New System.Drawing.Point(20, 104)
    Me.ckQuant.Name = "ckQuant"
    Me.ckQuant.NTSCheckValue = "S"
    Me.ckQuant.NTSUnCheckValue = "N"
    Me.ckQuant.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckQuant.Properties.Appearance.Options.UseBackColor = True
    Me.ckQuant.Properties.Caption = "Quantità"
    Me.ckQuant.Size = New System.Drawing.Size(83, 19)
    Me.ckQuant.TabIndex = 9
    '
    'ckDescrizione
    '
    Me.ckDescrizione.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDescrizione.EditValue = True
    Me.ckDescrizione.Location = New System.Drawing.Point(20, 63)
    Me.ckDescrizione.Name = "ckDescrizione"
    Me.ckDescrizione.NTSCheckValue = "S"
    Me.ckDescrizione.NTSUnCheckValue = "N"
    Me.ckDescrizione.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDescrizione.Properties.Appearance.Options.UseBackColor = True
    Me.ckDescrizione.Properties.Caption = "Descrizione"
    Me.ckDescrizione.Size = New System.Drawing.Size(83, 19)
    Me.ckDescrizione.TabIndex = 8
    '
    'lbCinque
    '
    Me.lbCinque.AutoSize = True
    Me.lbCinque.BackColor = System.Drawing.Color.Transparent
    Me.lbCinque.Location = New System.Drawing.Point(4, 125)
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
    Me.lbQuattro.Location = New System.Drawing.Point(4, 106)
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
    Me.lbDue.Location = New System.Drawing.Point(4, 65)
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
    Me.lbUno.Location = New System.Drawing.Point(4, 45)
    Me.lbUno.Name = "lbUno"
    Me.lbUno.NTSDbField = ""
    Me.lbUno.Size = New System.Drawing.Size(17, 13)
    Me.lbUno.TabIndex = 4
    Me.lbUno.Text = "1."
    Me.lbUno.Tooltip = ""
    Me.lbUno.UseMnemonic = False
    '
    'lbNomeFile
    '
    Me.lbNomeFile.AutoSize = True
    Me.lbNomeFile.BackColor = System.Drawing.Color.Transparent
    Me.lbNomeFile.Location = New System.Drawing.Point(3, 25)
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
    Me.ckSilent.Location = New System.Drawing.Point(214, 47)
    Me.ckSilent.Name = "ckSilent"
    Me.ckSilent.NTSCheckValue = "S"
    Me.ckSilent.NTSUnCheckValue = "N"
    Me.ckSilent.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSilent.Properties.Appearance.Options.UseBackColor = True
    Me.ckSilent.Properties.Caption = "Lancia il comando in modalità nascosta"
    Me.ckSilent.Size = New System.Drawing.Size(212, 19)
    Me.ckSilent.TabIndex = 10
    '
    'ckShowLog
    '
    Me.ckShowLog.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckShowLog.Location = New System.Drawing.Point(214, 107)
    Me.ckShowLog.Name = "ckShowLog"
    Me.ckShowLog.NTSCheckValue = "S"
    Me.ckShowLog.NTSUnCheckValue = "N"
    Me.ckShowLog.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckShowLog.Properties.Appearance.Options.UseBackColor = True
    Me.ckShowLog.Properties.Caption = "Visualizza file log con gli errori di importazione"
    Me.ckShowLog.Size = New System.Drawing.Size(247, 19)
    Me.ckShowLog.TabIndex = 13
    '
    'edNomeFile
    '
    Me.edNomeFile.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNomeFile.Location = New System.Drawing.Point(121, 21)
    Me.edNomeFile.Name = "edNomeFile"
    Me.edNomeFile.NTSDbField = ""
    Me.edNomeFile.NTSForzaVisZoom = False
    Me.edNomeFile.NTSOldValue = ""
    Me.edNomeFile.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNomeFile.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNomeFile.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNomeFile.Properties.MaxLength = 65536
    Me.edNomeFile.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNomeFile.Size = New System.Drawing.Size(343, 20)
    Me.edNomeFile.TabIndex = 7
    '
    'ckDelFile
    '
    Me.ckDelFile.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDelFile.Location = New System.Drawing.Point(214, 67)
    Me.ckDelFile.Name = "ckDelFile"
    Me.ckDelFile.NTSCheckValue = "S"
    Me.ckDelFile.NTSUnCheckValue = "N"
    Me.ckDelFile.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDelFile.Properties.Appearance.Options.UseBackColor = True
    Me.ckDelFile.Properties.Caption = "Elimina il file a fine acquisizione"
    Me.ckDelFile.Size = New System.Drawing.Size(173, 19)
    Me.ckDelFile.TabIndex = 11
    '
    'ckIgnore1rk
    '
    Me.ckIgnore1rk.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckIgnore1rk.Location = New System.Drawing.Point(214, 87)
    Me.ckIgnore1rk.Name = "ckIgnore1rk"
    Me.ckIgnore1rk.NTSCheckValue = "S"
    Me.ckIgnore1rk.NTSUnCheckValue = "N"
    Me.ckIgnore1rk.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckIgnore1rk.Properties.Appearance.Options.UseBackColor = True
    Me.ckIgnore1rk.Properties.Caption = "Ignora primo record"
    Me.ckIgnore1rk.Size = New System.Drawing.Size(125, 19)
    Me.ckIgnore1rk.TabIndex = 12
    '
    'cmdNomeFile
    '
    Me.cmdNomeFile.ImageText = ""
    Me.cmdNomeFile.Location = New System.Drawing.Point(470, 21)
    Me.cmdNomeFile.Name = "cmdNomeFile"
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
    Me.tpExport.Size = New System.Drawing.Size(507, 358)
    Me.tpExport.Text = "&2 - Esportazione righe"
    '
    'pnExport
    '
    Me.pnExport.AllowDrop = True
    Me.pnExport.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnExport.Appearance.Options.UseBackColor = True
    Me.pnExport.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnExport.Controls.Add(Me.ckTipoRigaE)
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
    Me.pnExport.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnExport.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnExport.Name = "pnExport"
    Me.pnExport.NTSActiveTrasparency = True
    Me.pnExport.Size = New System.Drawing.Size(507, 358)
    Me.pnExport.TabIndex = 26
    Me.pnExport.Text = "NtsPanel1"
    '
    'ckTipoRigaE
    '
    Me.ckTipoRigaE.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTipoRigaE.Location = New System.Drawing.Point(277, 64)
    Me.ckTipoRigaE.Name = "ckTipoRigaE"
    Me.ckTipoRigaE.NTSCheckValue = "S"
    Me.ckTipoRigaE.NTSUnCheckValue = "N"
    Me.ckTipoRigaE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTipoRigaE.Properties.Appearance.Options.UseBackColor = True
    Me.ckTipoRigaE.Properties.Caption = "Inserisci tipo riga"
    Me.ckTipoRigaE.Size = New System.Drawing.Size(109, 19)
    Me.ckTipoRigaE.TabIndex = 64
    '
    'cmdCancellaE
    '
    Me.cmdCancellaE.ImageText = ""
    Me.cmdCancellaE.Location = New System.Drawing.Point(6, 121)
    Me.cmdCancellaE.Name = "cmdCancellaE"
    Me.cmdCancellaE.Size = New System.Drawing.Size(162, 26)
    Me.cmdCancellaE.TabIndex = 42
    Me.cmdCancellaE.Text = "Cancella"
    '
    'ckExecuteE
    '
    Me.ckExecuteE.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckExecuteE.Location = New System.Drawing.Point(3, 1)
    Me.ckExecuteE.Name = "ckExecuteE"
    Me.ckExecuteE.NTSCheckValue = "S"
    Me.ckExecuteE.NTSUnCheckValue = "N"
    Me.ckExecuteE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckExecuteE.Properties.Appearance.Options.UseBackColor = True
    Me.ckExecuteE.Properties.Caption = "Esegui il comando"
    Me.ckExecuteE.Size = New System.Drawing.Size(112, 19)
    Me.ckExecuteE.TabIndex = 14
    '
    'grExport
    '
    Me.grExport.EmbeddedNavigator.Name = ""
    Me.grExport.Location = New System.Drawing.Point(0, 149)
    Me.grExport.MainView = Me.grvExport
    Me.grExport.Name = "grExport"
    Me.grExport.Size = New System.Drawing.Size(502, 205)
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
    Me.frmTipofileE.Location = New System.Drawing.Point(6, 44)
    Me.frmTipofileE.Name = "frmTipofileE"
    Me.frmTipofileE.Size = New System.Drawing.Size(162, 74)
    Me.frmTipofileE.TabIndex = 20
    Me.frmTipofileE.Text = "Tipo file"
    '
    'edSepFieldE
    '
    Me.edSepFieldE.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSepFieldE.EditValue = ";"
    Me.edSepFieldE.Enabled = False
    Me.edSepFieldE.Location = New System.Drawing.Point(124, 39)
    Me.edSepFieldE.Name = "edSepFieldE"
    Me.edSepFieldE.NTSDbField = ""
    Me.edSepFieldE.NTSForzaVisZoom = False
    Me.edSepFieldE.NTSOldValue = ";"
    Me.edSepFieldE.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSepFieldE.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSepFieldE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSepFieldE.Properties.MaxLength = 65536
    Me.edSepFieldE.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSepFieldE.Size = New System.Drawing.Size(24, 20)
    Me.edSepFieldE.TabIndex = 2
    '
    'opDelimE
    '
    Me.opDelimE.Cursor = System.Windows.Forms.Cursors.Default
    Me.opDelimE.Location = New System.Drawing.Point(5, 40)
    Me.opDelimE.Name = "opDelimE"
    Me.opDelimE.NTSCheckValue = "S"
    Me.opDelimE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDelimE.Properties.Appearance.Options.UseBackColor = True
    Me.opDelimE.Properties.Caption = "Separat. di campo"
    Me.opDelimE.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDelimE.Size = New System.Drawing.Size(115, 19)
    Me.opDelimE.TabIndex = 1
    '
    'opFixedE
    '
    Me.opFixedE.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFixedE.EditValue = True
    Me.opFixedE.Location = New System.Drawing.Point(5, 20)
    Me.opFixedE.Name = "opFixedE"
    Me.opFixedE.NTSCheckValue = "S"
    Me.opFixedE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFixedE.Properties.Appearance.Options.UseBackColor = True
    Me.opFixedE.Properties.Caption = "Posizioni fisse"
    Me.opFixedE.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFixedE.Size = New System.Drawing.Size(99, 19)
    Me.opFixedE.TabIndex = 0
    '
    'lbNomeFileE
    '
    Me.lbNomeFileE.AutoSize = True
    Me.lbNomeFileE.BackColor = System.Drawing.Color.Transparent
    Me.lbNomeFileE.Location = New System.Drawing.Point(3, 25)
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
    Me.lbCommandE.Location = New System.Drawing.Point(121, 0)
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
    Me.ckSilentE.Location = New System.Drawing.Point(174, 44)
    Me.ckSilentE.Name = "ckSilentE"
    Me.ckSilentE.NTSCheckValue = "S"
    Me.ckSilentE.NTSUnCheckValue = "N"
    Me.ckSilentE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSilentE.Properties.Appearance.Options.UseBackColor = True
    Me.ckSilentE.Properties.Caption = "Lancia il comando in modalità nascosta"
    Me.ckSilentE.Size = New System.Drawing.Size(212, 19)
    Me.ckSilentE.TabIndex = 21
    '
    'ckRimuoviVbcrlf
    '
    Me.ckRimuoviVbcrlf.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRimuoviVbcrlf.EditValue = True
    Me.ckRimuoviVbcrlf.Location = New System.Drawing.Point(174, 104)
    Me.ckRimuoviVbcrlf.Name = "ckRimuoviVbcrlf"
    Me.ckRimuoviVbcrlf.NTSCheckValue = "S"
    Me.ckRimuoviVbcrlf.NTSUnCheckValue = "N"
    Me.ckRimuoviVbcrlf.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRimuoviVbcrlf.Properties.Appearance.Options.UseBackColor = True
    Me.ckRimuoviVbcrlf.Properties.Caption = "Rimuovi ""andare a capo"" dalle note e dalle descrizioni"
    Me.ckRimuoviVbcrlf.Size = New System.Drawing.Size(284, 19)
    Me.ckRimuoviVbcrlf.TabIndex = 24
    '
    'ckShowLogE
    '
    Me.ckShowLogE.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckShowLogE.Location = New System.Drawing.Point(174, 124)
    Me.ckShowLogE.Name = "ckShowLogE"
    Me.ckShowLogE.NTSCheckValue = "S"
    Me.ckShowLogE.NTSUnCheckValue = "N"
    Me.ckShowLogE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckShowLogE.Properties.Appearance.Options.UseBackColor = True
    Me.ckShowLogE.Properties.Caption = "Visualizza file log con gli errori di importazione"
    Me.ckShowLogE.Size = New System.Drawing.Size(247, 19)
    Me.ckShowLogE.TabIndex = 24
    '
    'edNomeFileE
    '
    Me.edNomeFileE.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNomeFileE.Location = New System.Drawing.Point(121, 22)
    Me.edNomeFileE.Name = "edNomeFileE"
    Me.edNomeFileE.NTSDbField = ""
    Me.edNomeFileE.NTSForzaVisZoom = False
    Me.edNomeFileE.NTSOldValue = ""
    Me.edNomeFileE.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNomeFileE.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNomeFileE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNomeFileE.Properties.MaxLength = 65536
    Me.edNomeFileE.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNomeFileE.Size = New System.Drawing.Size(343, 20)
    Me.edNomeFileE.TabIndex = 18
    '
    'cmdCommandE
    '
    Me.cmdCommandE.Enabled = False
    Me.cmdCommandE.ImageText = ""
    Me.cmdCommandE.Location = New System.Drawing.Point(470, 1)
    Me.cmdCommandE.Name = "cmdCommandE"
    Me.cmdCommandE.Size = New System.Drawing.Size(25, 20)
    Me.cmdCommandE.TabIndex = 16
    Me.cmdCommandE.Text = "..."
    '
    'cmdNomeFileE
    '
    Me.cmdNomeFileE.ImageText = ""
    Me.cmdNomeFileE.Location = New System.Drawing.Point(470, 22)
    Me.cmdNomeFileE.Name = "cmdNomeFileE"
    Me.cmdNomeFileE.Size = New System.Drawing.Size(25, 20)
    Me.cmdNomeFileE.TabIndex = 17
    Me.cmdNomeFileE.Text = "..."
    '
    'ckGenTracciatoE
    '
    Me.ckGenTracciatoE.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckGenTracciatoE.Location = New System.Drawing.Point(174, 84)
    Me.ckGenTracciatoE.Name = "ckGenTracciatoE"
    Me.ckGenTracciatoE.NTSCheckValue = "S"
    Me.ckGenTracciatoE.NTSUnCheckValue = "N"
    Me.ckGenTracciatoE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckGenTracciatoE.Properties.Appearance.Options.UseBackColor = True
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
    Me.tpImport.Size = New System.Drawing.Size(507, 358)
    Me.tpImport.Text = "&3 - Importazione file"
    '
    'NtsPanel2
    '
    Me.NtsPanel2.AllowDrop = True
    Me.NtsPanel2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel2.Appearance.Options.UseBackColor = True
    Me.NtsPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel2.Controls.Add(Me.ckTipoRigaI)
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
    Me.NtsPanel2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.NtsPanel2.LookAndFeel.UseDefaultLookAndFeel = False
    Me.NtsPanel2.Name = "NtsPanel2"
    Me.NtsPanel2.NTSActiveTrasparency = True
    Me.NtsPanel2.Size = New System.Drawing.Size(507, 358)
    Me.NtsPanel2.TabIndex = 41
    Me.NtsPanel2.Text = "NtsPanel2"
    '
    'ckTipoRigaI
    '
    Me.ckTipoRigaI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTipoRigaI.Location = New System.Drawing.Point(379, 84)
    Me.ckTipoRigaI.Name = "ckTipoRigaI"
    Me.ckTipoRigaI.NTSCheckValue = "S"
    Me.ckTipoRigaI.NTSUnCheckValue = "N"
    Me.ckTipoRigaI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTipoRigaI.Properties.Appearance.Options.UseBackColor = True
    Me.ckTipoRigaI.Properties.Caption = "Usa tipo riga"
    Me.ckTipoRigaI.Size = New System.Drawing.Size(85, 19)
    Me.ckTipoRigaI.TabIndex = 63
    '
    'cmdCancellaI
    '
    Me.cmdCancellaI.ImageText = ""
    Me.cmdCancellaI.Location = New System.Drawing.Point(6, 130)
    Me.cmdCancellaI.Name = "cmdCancellaI"
    Me.cmdCancellaI.Size = New System.Drawing.Size(162, 26)
    Me.cmdCancellaI.TabIndex = 41
    Me.cmdCancellaI.Text = "Cancella"
    '
    'ckExecuteI
    '
    Me.ckExecuteI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckExecuteI.Location = New System.Drawing.Point(3, 1)
    Me.ckExecuteI.Name = "ckExecuteI"
    Me.ckExecuteI.NTSCheckValue = "S"
    Me.ckExecuteI.NTSUnCheckValue = "N"
    Me.ckExecuteI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckExecuteI.Properties.Appearance.Options.UseBackColor = True
    Me.ckExecuteI.Properties.Caption = "Esegui il comando"
    Me.ckExecuteI.Size = New System.Drawing.Size(112, 19)
    Me.ckExecuteI.TabIndex = 25
    '
    'grImport
    '
    Me.grImport.EmbeddedNavigator.Name = ""
    Me.grImport.Location = New System.Drawing.Point(0, 158)
    Me.grImport.MainView = Me.grvImport
    Me.grImport.Name = "grImport"
    Me.grImport.Size = New System.Drawing.Size(502, 200)
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
    Me.frmTipofileI.Location = New System.Drawing.Point(6, 44)
    Me.frmTipofileI.Name = "frmTipofileI"
    Me.frmTipofileI.Size = New System.Drawing.Size(162, 83)
    Me.frmTipofileI.TabIndex = 31
    Me.frmTipofileI.Text = "Tipo file"
    '
    'edDecSepI
    '
    Me.edDecSepI.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDecSepI.EditValue = ","
    Me.edDecSepI.Location = New System.Drawing.Point(124, 60)
    Me.edDecSepI.Name = "edDecSepI"
    Me.edDecSepI.NTSDbField = ""
    Me.edDecSepI.NTSForzaVisZoom = False
    Me.edDecSepI.NTSOldValue = ","
    Me.edDecSepI.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDecSepI.Properties.AppearanceDisabled.Options.UseForeColor = True
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
    Me.lbDecSepI.Location = New System.Drawing.Point(5, 62)
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
    Me.edSepFieldI.Location = New System.Drawing.Point(124, 39)
    Me.edSepFieldI.Name = "edSepFieldI"
    Me.edSepFieldI.NTSDbField = ""
    Me.edSepFieldI.NTSForzaVisZoom = False
    Me.edSepFieldI.NTSOldValue = ";"
    Me.edSepFieldI.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSepFieldI.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSepFieldI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSepFieldI.Properties.MaxLength = 65536
    Me.edSepFieldI.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSepFieldI.Size = New System.Drawing.Size(24, 20)
    Me.edSepFieldI.TabIndex = 2
    '
    'opDelimI
    '
    Me.opDelimI.Cursor = System.Windows.Forms.Cursors.Default
    Me.opDelimI.Location = New System.Drawing.Point(5, 40)
    Me.opDelimI.Name = "opDelimI"
    Me.opDelimI.NTSCheckValue = "S"
    Me.opDelimI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opDelimI.Properties.Appearance.Options.UseBackColor = True
    Me.opDelimI.Properties.Caption = "Separat. di campo"
    Me.opDelimI.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opDelimI.Size = New System.Drawing.Size(115, 19)
    Me.opDelimI.TabIndex = 1
    '
    'opFixedI
    '
    Me.opFixedI.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFixedI.EditValue = True
    Me.opFixedI.Location = New System.Drawing.Point(5, 20)
    Me.opFixedI.Name = "opFixedI"
    Me.opFixedI.NTSCheckValue = "S"
    Me.opFixedI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFixedI.Properties.Appearance.Options.UseBackColor = True
    Me.opFixedI.Properties.Caption = "Posizioni fisse"
    Me.opFixedI.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFixedI.Size = New System.Drawing.Size(99, 19)
    Me.opFixedI.TabIndex = 0
    '
    'ckProvvI
    '
    Me.ckProvvI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckProvvI.Location = New System.Drawing.Point(379, 124)
    Me.ckProvvI.Name = "ckProvvI"
    Me.ckProvvI.NTSCheckValue = "S"
    Me.ckProvvI.NTSUnCheckValue = "N"
    Me.ckProvvI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckProvvI.Properties.Appearance.Options.UseBackColor = True
    Me.ckProvvI.Properties.Caption = "Ricalcola provvigioni"
    Me.ckProvvI.Size = New System.Drawing.Size(123, 19)
    Me.ckProvvI.TabIndex = 39
    '
    'lbNomeFileI
    '
    Me.lbNomeFileI.AutoSize = True
    Me.lbNomeFileI.BackColor = System.Drawing.Color.Transparent
    Me.lbNomeFileI.Location = New System.Drawing.Point(3, 25)
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
    Me.ckScontiI.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckScontiI.Location = New System.Drawing.Point(277, 124)
    Me.ckScontiI.Name = "ckScontiI"
    Me.ckScontiI.NTSCheckValue = "S"
    Me.ckScontiI.NTSUnCheckValue = "N"
    Me.ckScontiI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckScontiI.Properties.Appearance.Options.UseBackColor = True
    Me.ckScontiI.Properties.Caption = "Ricalcola sconti"
    Me.ckScontiI.Size = New System.Drawing.Size(97, 19)
    Me.ckScontiI.TabIndex = 38
    '
    'ckSilentI
    '
    Me.ckSilentI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSilentI.Enabled = False
    Me.ckSilentI.Location = New System.Drawing.Point(174, 44)
    Me.ckSilentI.Name = "ckSilentI"
    Me.ckSilentI.NTSCheckValue = "S"
    Me.ckSilentI.NTSUnCheckValue = "N"
    Me.ckSilentI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSilentI.Properties.Appearance.Options.UseBackColor = True
    Me.ckSilentI.Properties.Caption = "Lancia il comando in modalità nascosta"
    Me.ckSilentI.Size = New System.Drawing.Size(212, 19)
    Me.ckSilentI.TabIndex = 32
    '
    'ckPrezziI
    '
    Me.ckPrezziI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPrezziI.Location = New System.Drawing.Point(174, 124)
    Me.ckPrezziI.Name = "ckPrezziI"
    Me.ckPrezziI.NTSCheckValue = "S"
    Me.ckPrezziI.NTSUnCheckValue = "N"
    Me.ckPrezziI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPrezziI.Properties.Appearance.Options.UseBackColor = True
    Me.ckPrezziI.Properties.Caption = "Ricalcola prezzi"
    Me.ckPrezziI.Size = New System.Drawing.Size(97, 19)
    Me.ckPrezziI.TabIndex = 37
    '
    'edNomeFileI
    '
    Me.edNomeFileI.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edNomeFileI.Location = New System.Drawing.Point(121, 22)
    Me.edNomeFileI.Name = "edNomeFileI"
    Me.edNomeFileI.NTSDbField = ""
    Me.edNomeFileI.NTSForzaVisZoom = False
    Me.edNomeFileI.NTSOldValue = ""
    Me.edNomeFileI.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNomeFileI.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNomeFileI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNomeFileI.Properties.MaxLength = 65536
    Me.edNomeFileI.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNomeFileI.Size = New System.Drawing.Size(343, 20)
    Me.edNomeFileI.TabIndex = 29
    '
    'ckDelFileI
    '
    Me.ckDelFileI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDelFileI.Location = New System.Drawing.Point(174, 64)
    Me.ckDelFileI.Name = "ckDelFileI"
    Me.ckDelFileI.NTSCheckValue = "S"
    Me.ckDelFileI.NTSUnCheckValue = "N"
    Me.ckDelFileI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDelFileI.Properties.Appearance.Options.UseBackColor = True
    Me.ckDelFileI.Properties.Caption = "Elimina il file a fine acquisizione"
    Me.ckDelFileI.Size = New System.Drawing.Size(173, 19)
    Me.ckDelFileI.TabIndex = 33
    '
    'lbCommandI
    '
    Me.lbCommandI.BackColor = System.Drawing.Color.Transparent
    Me.lbCommandI.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCommandI.Location = New System.Drawing.Point(121, 0)
    Me.lbCommandI.Name = "lbCommandI"
    Me.lbCommandI.NTSDbField = ""
    Me.lbCommandI.Size = New System.Drawing.Size(343, 20)
    Me.lbCommandI.TabIndex = 26
    Me.lbCommandI.Tooltip = ""
    Me.lbCommandI.UseMnemonic = False
    '
    'cmdNomeFileI
    '
    Me.cmdNomeFileI.ImageText = ""
    Me.cmdNomeFileI.Location = New System.Drawing.Point(470, 22)
    Me.cmdNomeFileI.Name = "cmdNomeFileI"
    Me.cmdNomeFileI.Size = New System.Drawing.Size(25, 20)
    Me.cmdNomeFileI.TabIndex = 28
    Me.cmdNomeFileI.Text = "..."
    '
    'ckShowLogI
    '
    Me.ckShowLogI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckShowLogI.Location = New System.Drawing.Point(174, 104)
    Me.ckShowLogI.Name = "ckShowLogI"
    Me.ckShowLogI.NTSCheckValue = "S"
    Me.ckShowLogI.NTSUnCheckValue = "N"
    Me.ckShowLogI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckShowLogI.Properties.Appearance.Options.UseBackColor = True
    Me.ckShowLogI.Properties.Caption = "Visualizza file log con gli errori di importazione"
    Me.ckShowLogI.Size = New System.Drawing.Size(247, 19)
    Me.ckShowLogI.TabIndex = 35
    '
    'ckIgnore1rkI
    '
    Me.ckIgnore1rkI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckIgnore1rkI.Location = New System.Drawing.Point(174, 84)
    Me.ckIgnore1rkI.Name = "ckIgnore1rkI"
    Me.ckIgnore1rkI.NTSCheckValue = "S"
    Me.ckIgnore1rkI.NTSUnCheckValue = "N"
    Me.ckIgnore1rkI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckIgnore1rkI.Properties.Appearance.Options.UseBackColor = True
    Me.ckIgnore1rkI.Properties.Caption = "Ignora primo record"
    Me.ckIgnore1rkI.Size = New System.Drawing.Size(125, 19)
    Me.ckIgnore1rkI.TabIndex = 34
    '
    'cmdCommandI
    '
    Me.cmdCommandI.Enabled = False
    Me.cmdCommandI.ImageText = ""
    Me.cmdCommandI.Location = New System.Drawing.Point(470, 0)
    Me.cmdCommandI.Name = "cmdCommandI"
    Me.cmdCommandI.Size = New System.Drawing.Size(25, 20)
    Me.cmdCommandI.TabIndex = 27
    Me.cmdCommandI.Text = "..."
    '
    'lbAvviso
    '
    Me.lbAvviso.BackColor = System.Drawing.Color.Transparent
    Me.lbAvviso.Location = New System.Drawing.Point(530, 106)
    Me.lbAvviso.Name = "lbAvviso"
    Me.lbAvviso.NTSDbField = ""
    Me.lbAvviso.Size = New System.Drawing.Size(117, 166)
    Me.lbAvviso.TabIndex = 12
    Me.lbAvviso.Text = "L'esportazione o importazione prevede l'inserimento," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "all'inizio di ogni riga, de" & _
        "l tipo riga:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "1 - Riga normale" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Se la quantità non è selezionata" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "viene consid" & _
        "erata" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "pari a 1"
    Me.lbAvviso.Tooltip = ""
    Me.lbAvviso.UseMnemonic = False
    '
    'FRMMGTERM_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(657, 390)
    Me.Controls.Add(Me.lbAvviso)
    Me.Controls.Add(Me.pnMain)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGTERM_F"
    Me.Text = "IMPOSTAZIONI FILE TERMINALE"
    CType(Me.ckExecute.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnMain, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnMain.ResumeLayout(False)
    CType(Me.tcImport, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tcImport.ResumeLayout(False)
    Me.tpTerminale.ResumeLayout(False)
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.NtsPanel1.PerformLayout()
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
    CType(Me.cbCampi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatconsA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatconsDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosDatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDatcons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDesintA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDesintDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPosDesint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDesint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Dim dttCampi As New DataTable()

    Try
      '--------------------------------------------------------------------------------------------------------------
      cbCampi.NTSSetParam(oApp.Tr(Me, 130156928416795976, "Campi"))
      dttCampi.Columns.Add("cod", GetType(String))
      dttCampi.Columns.Add("val", GetType(String))
      dttCampi.Rows.Add(New Object() {"A", "Cod. art o Barcode o Cod. Art. C/F"})
      dttCampi.Rows.Add(New Object() {"B", "Codice articolo"})
      dttCampi.Rows.Add(New Object() {"C", "Barcode"})
      dttCampi.Rows.Add(New Object() {"D", "Cod. articolo C/F"})
      dttCampi.AcceptChanges()
      cbCampi.DataSource = dttCampi
      cbCampi.ValueMember = "cod"
      cbCampi.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      edNomeFile.NTSSetParam(oMenu, oApp.Tr(Me, 128571233823125000, "Nome file"), 0, True)
      edDecSep.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822343750, "Separatore decimale"), 1, True)
      edSepField.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822656250, "Separatore di campo"), 1, True)
      '--------------------------------------------------------------------------------------------------------------
      opFixed.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822968750, "Posizioni fisse"), "F")
      opDelim.NTSSetParam(oMenu, oApp.Tr(Me, 128571233822812500, "Separat. di campo"), "S")
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
      ckDatcons.NTSSetParam(oMenu, oApp.Tr(Me, 129037914812103802, "Data consegna"), "S", "N")
      '--------------------------------------------------------------------------------------------------------------
      edPosArt.NTSSetParam(oMenu, oApp.Tr(Me, 128571233819375000, "Posizione articolo"), "0", 3, 0, 255)
      edPosDescr.NTSSetParam(oMenu, oApp.Tr(Me, 128571233819843750, "Posizione descrizione"), "0", 3, 0, 255)
      edPosDesint.NTSSetParam(oMenu, oApp.Tr(Me, 129037917377728802, "Posizione descrizione interna"), "0", 3, 0, 255)
      edPosPre.NTSSetParam(oMenu, oApp.Tr(Me, 128571233819531250, "Posizione prezzo"), "0", 3, 0, 255)
      edPosQua.NTSSetParam(oMenu, oApp.Tr(Me, 128571233819687500, "Posizione quantità"), "0", 3, 0, 255)
      edPosSc1.NTSSetParam(oMenu, oApp.Tr(Me, 128775271769824000, "Posizione sconto 1"), "0", 3, 0, 255)
      edPosSc2.NTSSetParam(oMenu, oApp.Tr(Me, 128775271768264000, "Posizione sconto 2"), "0", 3, 0, 255)
      edPosDatcons.NTSSetParam(oMenu, oApp.Tr(Me, 129037918707103802, "Posizione data consegna"), "0", 3, 0, 255)
      '--------------------------------------------------------------------------------------------------------------
      edArtDa.NTSSetParam(oMenu, oApp.Tr(Me, 128571233818593750, "Articoo DA"), "0", 4, 0, 9999)
      edDescrDa.NTSSetParam(oMenu, oApp.Tr(Me, 128571233817968750, "Descrizione DA"), "0", 4, 0, 9999)
      edDesintDa.NTSSetParam(oMenu, oApp.Tr(Me, 129037916537416302, "Descrizione interna DA"), "0", 4, 0, 9999)
      edQuantDa.NTSSetParam(oMenu, oApp.Tr(Me, 128571233817343750, "Quantità DA"), "0", 4, 0, 9999)
      edPreDa.NTSSetParam(oMenu, oApp.Tr(Me, 128571233816718750, "Prezzo DA"), "0", 4, 0, 9999)
      edSc1Da.NTSSetParam(oMenu, oApp.Tr(Me, 128775271769512000, "Sconto 1 DA"), "0", 4, 0, 9999)
      edSc2Da.NTSSetParam(oMenu, oApp.Tr(Me, 128775271767952000, "Sconto 2 DA"), "0", 4, 0, 9999)
      edDatconsDa.NTSSetParam(oMenu, oApp.Tr(Me, 129037919355541302, "Data consegna DA"), "0", 4, 0, 9999)
      '--------------------------------------------------------------------------------------------------------------
      edArtA.NTSSetParam(oMenu, oApp.Tr(Me, 128571233818437500, "Articolo A"), "0", 4, 0, 9999)
      edDescrA.NTSSetParam(oMenu, oApp.Tr(Me, 128571233817812500, "Descrizione A"), "0", 4, 0, 9999)
      edDesintA.NTSSetParam(oMenu, oApp.Tr(Me, 129037920091791302, "Descrizione interna A"), "0", 4, 0, 9999)
      edPreA.NTSSetParam(oMenu, oApp.Tr(Me, 128571233816562500, "Prezzo A"), "0", 4, 0, 9999)
      edQuantA.NTSSetParam(oMenu, oApp.Tr(Me, 128571233817187500, "Quantità A"), "0", 4, 0, 9999)
      edSc1A.NTSSetParam(oMenu, oApp.Tr(Me, 128775271769356000, "Sconto 1 A"), "0", 4, 0, 9999)
      edSc2A.NTSSetParam(oMenu, oApp.Tr(Me, 128775271767796000, "Sconto 2 A"), "0", 4, 0, 9999)
      edDatconsA.NTSSetParam(oMenu, oApp.Tr(Me, 129037920469760052, "Data consegna A"), "0", 4, 0, 9999)
      '--------------------------------------------------------------------------------------------------------------
      edDivPrezzo.NTSSetParam(oMenu, oApp.Tr(Me, 128571233815937500, "Dividi prezzo"), "0", 5, 1, 10000)
      edDivQuant.NTSSetParam(oMenu, oApp.Tr(Me, 128571233816093750, "Dividi quantità"), "0", 5, 1, 10000)
      edDivSc1.NTSSetParam(oMenu, oApp.Tr(Me, 128775271768888000, "Dividi sconto 1"), "0", 5, 1, 10000)
      edDivSc2.NTSSetParam(oMenu, oApp.Tr(Me, 128775271767328000, "Dividi sconto 2"), "0", 5, 1, 10000)
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
      ckTipoRigaI.NTSSetParam(oMenu, oApp.Tr(Me, 129253138532031250, "Usa tipo riga"), "S", "N")

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
      If oCleDocu.bTermDatcons Then ckDatcons.Checked = True Else ckDatcons.Checked = False
      '--------------------------------------------------------------------------------------------------------------
      Select Case oCleDocu.strTermTipoArt
        Case "X"  'articolo o barcode
          cbCampi.SelectedValue = "A"
        Case "C" 'codart
          cbCampi.SelectedValue = "B"
        Case "B" 'barcode(default)
          cbCampi.SelectedValue = "C"
        Case Else 'codarfo
          cbCampi.SelectedValue = "D"
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
      edPosDatcons.Text = oCleDocu.nTermDatconsPos.ToString
      '--------------------------------------------------------------------------------------------------------------
      edArtDa.Text = oCleDocu.nTermCodartStart.ToString
      edDescrDa.Text = oCleDocu.nTermDescrStart.ToString
      edDesintDa.Text = oCleDocu.nTermDesintStart.ToString
      edQuantDa.Text = oCleDocu.nTermQuantStart.ToString
      edPreDa.Text = oCleDocu.nTermPrezziStart.ToString
      edSc1Da.Text = oCleDocu.nTermScont1Start.ToString
      edSc2Da.Text = oCleDocu.nTermScont2Start.ToString
      edDatconsDa.Text = oCleDocu.nTermDatconsStart.ToString
      '--------------------------------------------------------------------------------------------------------------
      edArtA.Text = oCleDocu.nTermCodartStop.ToString
      edDescrA.Text = oCleDocu.nTermDescrStop.ToString
      edDesintA.Text = oCleDocu.nTermDesintStop.ToString
      edQuantA.Text = oCleDocu.nTermQuantStop.ToString
      edPreA.Text = oCleDocu.nTermPrezziStop.ToString
      edSc1A.Text = oCleDocu.nTermScont1Stop.ToString
      edSc2A.Text = oCleDocu.nTermScont2Stop.ToString
      edDatconsA.Text = oCleDocu.nTermDatconsStop.ToString
      '--------------------------------------------------------------------------------------------------------------
      edDivQuant.Text = oCleDocu.nTermDivQuant.ToString
      edDivPrezzo.Text = oCleDocu.nTermDivPrezzi.ToString
      edDivSc1.Text = oCleDocu.nTermDivScont1.ToString
      edDivSc2.Text = oCleDocu.nTermDivScont2.ToString
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
#End Region

#Region "Eventi"
  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
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
          Return
        End If
      End If
      If edNomeFile.Text.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129247970954133080, "File terminale: Nome di file obbligatorio."))
        Return
      End If
      If opDelim.Checked Then
        If edSepField.Text = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129247970963195580, "File terminale: Separatore di campo obbligatorio."))
          Return
        End If
      End If
      If edDecSep.Text.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129247970984289330, "File terminale: Separatore decimale obbligatorio."))
        Return
      End If
      If edDecSep.Text <> "," And edDecSep.Text <> "." Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129247970995383080, "File terminale: Il separatore decimale deve essere '.' (punto) o ',' (virgola)."))
        Return
      End If
      'Import
      If tcImport.SelectedTabPageIndex = 2 Then
        If edNomeFileI.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129597725836875000, "Importazione: Nome di file obbligatorio."))
          Return
        End If
        If ckExecuteI.Checked Then
          If lbCommandI.Text.Trim = "" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129247971011945580, "Importazione: Comando da eseguire obbligatorio."))
            Return
          End If
        End If
        If opDelimI.Checked Then
          If edSepField.Text = "" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129247971035070580, "Importazione: Separatore di campo obbligatorio."))
            Return
          End If
        End If
        If edDecSepI.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129247971052414330, "Importazione: Separatore decimale obbligatorio."))
          Return
        End If
        If edDecSepI.Text <> "," And edDecSepI.Text <> "." Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129247971064133080, "Importazione: Il separatore decimale deve essere '.' (punto) o ',' (virgola)."))
          Return
        End If
      End If
      'Export
      If tcImport.SelectedTabPageIndex = 1 Then
        If edNomeFileE.Text.Trim = "" Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129597725827031250, "Esportazione: Nome di file obbligatorio."))
          Return
        End If
        If ckExecuteE.Checked Then
          If lbCommandE.Text.Trim = "" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129247971079445580, "Esportazione: Comando da eseguire obbligatorio."))
            Return
          End If
        End If
        If opDelimE.Checked Then
          If edSepFieldE.Text = "" Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129247971107101830, "Esportazione: Separatore di campo obbligatorio."))
            Return
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
            Return
          End If
        End If
        If ckDesint.Checked Then
          If edPosArt.Text = edPosDesint.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037935620266712, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo quantità."))
            Return
          End If
        End If
        If ckQuant.Checked Then
          If edPosArt.Text = edPosQua.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571245342031250, "La posizione del campo articolo non deve essere uguale alla posizione del campo quantità."))
            Return
          End If
        End If
        If ckPrezzo.Checked Then
          If edPosArt.Text = edPosPre.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260054076000, "La posizione del campo articolo non deve essere uguale alla posizione del campo prezzo."))
            Return
          End If
        End If
        If ckSconto1.Checked Then
          If edPosArt.Text = edPosSc1.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775253933208000, "La posizione del campo articolo non deve essere uguale alla posizione del campo sconto 1."))
            Exit Sub
          End If
        End If
        If ckSconto2.Checked Then
          If edPosArt.Text = edPosSc2.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775253920572000, "La posizione del campo articolo non deve essere uguale alla posizione del campo sconto 2."))
            Exit Sub
          End If
        End If
        If ckDatcons.Checked Then
          If edPosArt.Text = edPosDatcons.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037935938860462, "La posizione del campo articolo non deve essere uguale alla posizione del campo data consegna."))
            Exit Sub
          End If
        End If
        If (ckDescrizione.Checked) And (ckDesint.Checked) Then
          If edPosDescr.Text = edPosDesint.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037936313860462, "La posizione del campo descrizione non deve essere uguale alla posizione del campo descrizione interna."))
            Return
          End If
        End If
        If (ckDescrizione.Checked) And (ckQuant.Checked) Then
          If edPosDescr.Text = edPosQua.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254565320000, "La posizione del campo descrizione non deve essere uguale alla posizione del campo quantità."))
            Return
          End If
        End If
        If (ckDescrizione.Checked) And (ckPrezzo.Checked) Then
          If edPosDescr.Text = edPosPre.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254545508000, "La posizione del campo descrizione non deve essere uguale alla posizione del campo prezzo."))
            Exit Sub
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto1.Checked) Then
          If edPosDescr.Text = edPosSc1.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254533340000, "La posizione del campo descrizione non deve essere uguale alla posizione del campo sconto 1."))
            Exit Sub
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto2.Checked) Then
          If edPosDescr.Text = edPosSc2.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254518052000, "La posizione del campo descrizione non deve essere uguale alla posizione del campo sconto 2."))
            Exit Sub
          End If
        End If
        If (ckDescrizione.Checked) And (ckDatcons.Checked) Then
          If edPosDescr.Text = edPosDatcons.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037936763860462, "La posizione del campo descrizione non deve essere uguale alla posizione del campo data consegna."))
            Return
          End If
        End If
        If (ckQuant.Checked) And (ckDesint.Checked) Then
          If edPosDesint.Text = edPosQua.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037937372922962, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo quantità."))
            Return
          End If
        End If
        If (ckPrezzo.Checked) And (ckDesint.Checked) Then
          If edPosDesint.Text = edPosPre.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037937944641712, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo prezzo."))
            Return
          End If
        End If
        If (ckSconto1.Checked) And (ckDesint.Checked) Then
          If edPosDesint.Text = edPosSc1.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037938254641712, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo sconto 1."))
            Return
          End If
        End If
        If (ckSconto2.Checked) And (ckDesint.Checked) Then
          If edPosDesint.Text = edPosSc2.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037938587766712, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo sconto 2."))
            Return
          End If
        End If
        If (ckDatcons.Checked) And (ckDesint.Checked) Then
          If edPosDesint.Text = edPosDatcons.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037939639016712, "La posizione del campo descrizione interna non deve essere uguale alla posizione del campo data consegna."))
            Return
          End If
        End If
        If (ckPrezzo.Checked) And (ckQuant.Checked) Then
          If edPosQua.Text = edPosPre.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254887304000, "La posizione del campo quantità non deve essere uguale alla posizione del campo prezzo."))
            Return
          End If
        End If
        If (ckSconto1.Checked) And (ckQuant.Checked) Then
          If edPosQua.Text = edPosSc1.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254871080000, "La posizione del campo quantità non deve essere uguale alla posizione del campo sconto 1."))
            Exit Sub
          End If
        End If
        If (ckSconto2.Checked) And (ckQuant.Checked) Then
          If edPosQua.Text = edPosSc2.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775254857352000, "La posizione del campo quantità non deve essere uguale alla posizione del campo sconto 2."))
            Exit Sub
          End If
        End If
        If (ckDatcons.Checked) And (ckQuant.Checked) Then
          If edPosQua.Text = edPosDatcons.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037939900579212, "La posizione del campo quantità non deve essere uguale alla posizione del campo data consegna."))
            Return
          End If
        End If
        If (ckSconto1.Checked) And (ckPrezzo.Checked) Then
          If edPosPre.Text = edPosSc1.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775255545070000, "La posizione del campo prezzo non deve essere uguale alla posizione del campo sconto 1."))
            Exit Sub
          End If
        End If
        If (ckSconto2.Checked) And (ckPrezzo.Checked) Then
          If edPosPre.Text = edPosSc2.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775255530250000, "La posizione del campo prezzo non deve essere uguale alla posizione del campo sconto 2."))
            Exit Sub
          End If
        End If
        If (ckDatcons.Checked) And (ckPrezzo.Checked) Then
          If edPosPre.Text = edPosDatcons.Text Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037940137297962, "La posizione del campo prezzo non deve essere uguale alla posizione del campo data consegna."))
            Exit Sub
          End If
        End If
      Else
        'Controlla validità numeri
        If NTSCInt(edArtDa.Text) > NTSCInt(edArtA.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128571247651250000, "La posizione iniziale del campo articolo non deve superare quella finale."))
          Return
        End If
        If ckDescrizione.Checked Then
          If NTSCInt(edDescrDa.Text) > NTSCInt(edDescrA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571247881875000, "La posizione iniziale del campo descrizione non deve superare quella finale."))
            Return
          End If
        End If
        If ckDesint.Checked Then
          If NTSCInt(edDesintDa.Text) > NTSCInt(edDesintA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037940556204212, "La posizione iniziale del campo descrizione interna non deve superare quella finale."))
            Return
          End If
        End If
        If ckQuant.Checked Then
          If NTSCInt(edQuantDa.Text) > NTSCInt(edQuantA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571248041718750, "La posizione iniziale del campo quantità non deve superare quella finale."))
            Return
          End If
        End If
        If ckPrezzo.Checked Then
          If NTSCInt(edPreDa.Text) > NTSCInt(edPreA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571248171093750, "La posizione iniziale del campo prezzo non deve superare quella finale."))
            Return
          End If
        End If
        If ckSconto1.Checked Then
          If NTSCInt(edSc1Da.Text) > NTSCInt(edSc1A.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256369062000, "La posizione iniziale del campo sconto 1 non deve superare quella finale."))
            Return
          End If
        End If
        If ckSconto2.Checked Then
          If NTSCInt(edSc2Da.Text) > NTSCInt(edSc2A.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256352994000, "La posizione iniziale del campo sconto 2 non deve superare quella finale."))
            Return
          End If
        End If
        If ckDatcons.Checked Then
          If NTSCInt(edDatconsDa.Text) > NTSCInt(edDatconsA.Text) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037940896516712, "La posizione iniziale del campo data consegna non deve superare quella finale."))
            Return
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Controllo congruenza posizioni
        '------------------------------------------------------------------------------------------------------------
        If ckDescrizione.Checked Then
          If (NTSCInt(edDescrDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edDescrDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edDescrA.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edArtDa.Text) <= NTSCInt(edDescrA.Text)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edArtA.Text) <= NTSCInt(edDescrA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256806486000, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return
          End If
        End If
        If ckDesint.Checked Then
          If (NTSCInt(edDesintDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edDesintDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edDesintA.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edArtDa.Text) <= NTSCInt(edDesintA.Text)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edArtA.Text) <= NTSCInt(edDesintA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037942606086852, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return
          End If
        End If
        If ckQuant.Checked Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edQuantDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edQuantA.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edArtDa.Text) <= NTSCInt(edQuantA.Text)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edArtA.Text) <= NTSCInt(edQuantA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256779498000, "La posizione del campo quantità non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return
          End If
        End If
        If ckPrezzo.Checked Then
          If (NTSCInt(edPreDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edPreDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edPreA.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edArtDa.Text) <= NTSCInt(edPreA.Text)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edArtA.Text) <= NTSCInt(edPreA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256761714000, "La posizione del campo prezzo non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Return
          End If
        End If
        If ckSconto1.Checked Then
          If (NTSCInt(edSc1Da.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc1Da.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edSc1A.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc1A.Text) <= NTSCInt(edArtA)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edSc1Da) And NTSCInt(edArtDa.Text) <= NTSCInt(edSc1A)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edSc1Da) And NTSCInt(edArtA.Text) <= NTSCInt(edSc1A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256748766000, "La posizione del campo sconto 1 non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Exit Sub
          End If
        End If
        If ckSconto2.Checked Then
          If (NTSCInt(edSc2Da.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc2Da.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edSc2A.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edSc2A.Text) <= NTSCInt(edArtA)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edSc2Da) And NTSCInt(edArtDa.Text) <= NTSCInt(edSc2A)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edSc2Da) And NTSCInt(edArtA.Text) <= NTSCInt(edSc2A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775256730514000, "La posizione del campo sconto 2 non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Exit Sub
          End If
        End If
        If ckDatcons.Checked Then
          If (NTSCInt(edDatconsDa.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edDatconsDa.Text) <= NTSCInt(edArtA.Text)) Or (NTSCInt(edDatconsA.Text) >= NTSCInt(edArtDa.Text) And NTSCInt(edDatconsA.Text) <= NTSCInt(edArtA)) Or (NTSCInt(edArtDa.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edArtDa.Text) <= NTSCInt(edDatconsA)) Or (NTSCInt(edArtA.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edArtA.Text) <= NTSCInt(edDatconsA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037943243274352, "La posizione del campo data consegna non deve essere inclusa nella posizione del campo articolo e viceversa."))
            Exit Sub
          End If
        End If
        If (ckDescrizione.Checked) And (ckDesint.Checked) Then
          If (NTSCInt(edDesintDa.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edDesintDa.Text) <= NTSCInt(edDescrA.Text)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edDesintA.Text) <= NTSCInt(edDescrA.Text)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edDescrDa.Text) <= NTSCInt(edDesintA.Text)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edDescrA.Text) <= NTSCInt(edDesintA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037943977336852, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo descrizione interna e viceversa."))
            Return
          End If
        End If
        If (ckDescrizione.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edQuantDa.Text) <= NTSCInt(edDescrA.Text)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edQuantA.Text) <= NTSCInt(edDescrA.Text)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edDescrDa.Text) <= NTSCInt(edQuantA.Text)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edDescrA.Text) <= NTSCInt(edQuantA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571248820156250, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo quantità e viceversa."))
            Return
          End If
        End If
        If (ckDescrizione.Checked) And (ckPrezzo.Checked) Then
          If (NTSCInt(edPreDa.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edPreDa.Text) <= NTSCInt(edDescrA.Text)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edDescrDa.Text) And NTSCInt(edPreA.Text) <= NTSCInt(edDescrA.Text)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edDescrDa.Text) <= NTSCInt(edPreA.Text)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edDescrA.Text) <= NTSCInt(edPreA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571249002031250, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Return
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto1.Checked) Then
          If (NTSCInt(edSc1Da.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc1Da.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edSc1A.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc1A.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edSc1Da) And NTSCInt(edDescrDa.Text) <= NTSCInt(edSc1A)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edSc1Da) And NTSCInt(edDescrA.Text) <= NTSCInt(edSc1A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260075292000, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo sconto 1 e viceversa."))
            Exit Sub
          End If
        End If
        If (ckDescrizione.Checked) And (ckSconto2.Checked) Then
          If (NTSCInt(edSc2Da.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc2Da.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edSc2A.Text) >= NTSCInt(edDescrDa) And NTSCInt(edSc2A.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edSc2Da) And NTSCInt(edDescrDa.Text) <= NTSCInt(edSc2A)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edSc2Da) And NTSCInt(edDescrA.Text) <= NTSCInt(edSc2A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260085900000, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo sconto 2 e viceversa."))
            Exit Sub
          End If
        End If
        If (ckDescrizione.Checked) And (ckDatcons.Checked) Then
          If (NTSCInt(edDatconsDa.Text) >= NTSCInt(edDescrDa) And NTSCInt(edDatconsDa.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edDatconsA.Text) >= NTSCInt(edDescrDa) And NTSCInt(edDatconsA.Text) <= NTSCInt(edDescrA)) Or (NTSCInt(edDescrDa.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edDescrDa.Text) <= NTSCInt(edDatconsA)) Or (NTSCInt(edDescrA.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edDescrA.Text) <= NTSCInt(edDatconsA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037945407336852, "La posizione del campo descrizione non deve essere inclusa nella posizione del campo data consegna e viceversa."))
            Exit Sub
          End If
        End If
        If (ckDesint.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edQuantDa.Text) <= NTSCInt(edDesintA.Text)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edQuantA.Text) <= NTSCInt(edDesintA.Text)) Or (NTSCInt(edDesintDa.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edDesintDa.Text) <= NTSCInt(edQuantA.Text)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edDesintA.Text) <= NTSCInt(edQuantA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037946799524352, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo quantità e viceversa."))
            Return
          End If
        End If
        If (ckDesint.Checked) And (ckPrezzo.Checked) Then
          If (NTSCInt(edPreDa.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edPreDa.Text) <= NTSCInt(edDesintA.Text)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edDesintDa.Text) And NTSCInt(edPreA.Text) <= NTSCInt(edDesintA.Text)) Or (NTSCInt(edDesintDa.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edDesintDa.Text) <= NTSCInt(edPreA.Text)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edDesintA.Text) <= NTSCInt(edPreA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037947459993102, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Return
          End If
        End If
        If (ckDesint.Checked) And (ckSconto1.Checked) Then
          If (NTSCInt(edSc1Da.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc1Da.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edSc1A.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc1A.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edDesintDa.Text) >= NTSCInt(edSc1Da) And NTSCInt(edDesintDa.Text) <= NTSCInt(edSc1A)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edSc1Da) And NTSCInt(edDesintA.Text) <= NTSCInt(edSc1A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129037948317961852, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo sconto 1 e viceversa."))
            Exit Sub
          End If
        End If
        If (ckDesint.Checked) And (ckSconto2.Checked) Then
          If (NTSCInt(edSc2Da.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc2Da.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edSc2A.Text) >= NTSCInt(edDesintDa) And NTSCInt(edSc2A.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edDesintDa.Text) >= NTSCInt(edSc2Da) And NTSCInt(edDesintDa.Text) <= NTSCInt(edSc2A)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edSc2Da) And NTSCInt(edDesintA.Text) <= NTSCInt(edSc2A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129038023223806992, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo sconto 2 e viceversa."))
            Exit Sub
          End If
        End If
        If (ckDesint.Checked) And (ckDatcons.Checked) Then
          If (NTSCInt(edDatconsDa.Text) >= NTSCInt(edDesintDa) And NTSCInt(edDatconsDa.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edDatconsA.Text) >= NTSCInt(edDesintDa) And NTSCInt(edDatconsA.Text) <= NTSCInt(edDesintA)) Or (NTSCInt(edDesintDa.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edDesintDa.Text) <= NTSCInt(edDatconsA)) Or (NTSCInt(edDesintA.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edDesintA.Text) <= NTSCInt(edDatconsA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129038027839883104, "La posizione del campo descrizione interna non deve essere inclusa nella posizione del campo data consegna e viceversa."))
            Exit Sub
          End If
        End If
        If (ckPrezzo.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edQuantDa.Text) <= NTSCInt(edPreA.Text)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edPreDa.Text) And NTSCInt(edQuantA.Text) <= NTSCInt(edPreA.Text)) Or (NTSCInt(edPreDa.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edPreDa.Text) <= NTSCInt(edQuantA.Text)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edQuantDa.Text) And NTSCInt(edPreA.Text) <= NTSCInt(edQuantA.Text)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128571249138750000, "La posizione del campo quantità non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Return
          End If
        End If
        If (ckSconto1.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edSc1Da) And NTSCInt(edQuantDa.Text) <= NTSCInt(edSc1A)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edSc1Da) And NTSCInt(edQuantA.Text) <= NTSCInt(edSc1A)) Or (NTSCInt(edSc1Da.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc1Da.Text) <= NTSCInt(edQuantA)) Or (NTSCInt(edSc1A.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc1A.Text) <= NTSCInt(edQuantA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260138628000, "La posizione del campo quantità non deve essere inclusa nella posizione del campo sconto 1 e viceversa."))
            Exit Sub
          End If
        End If
        If (ckSconto2.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edSc2Da) And NTSCInt(edQuantDa.Text) <= NTSCInt(edSc2A)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edSc2Da) And NTSCInt(edQuantA.Text) <= NTSCInt(edSc2A)) Or (NTSCInt(edSc2Da.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc2Da.Text) <= NTSCInt(edQuantA)) Or (NTSCInt(edSc2A.Text) >= NTSCInt(edQuantDa) And NTSCInt(edSc2A.Text) <= NTSCInt(edQuantA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260149392000, "La posizione del campo quantità non deve essere inclusa nella posizione del campo sconto 2 e viceversa."))
            Exit Sub
          End If
        End If
        If (ckDatcons.Checked) And (ckQuant.Checked) Then
          If (NTSCInt(edQuantDa.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edQuantDa.Text) <= NTSCInt(edDatconsA)) Or (NTSCInt(edQuantA.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edQuantA.Text) <= NTSCInt(edDatconsA)) Or (NTSCInt(edDatconsDa.Text) >= NTSCInt(edQuantDa) And NTSCInt(edDatconsDa.Text) <= NTSCInt(edQuantA)) Or (NTSCInt(edDatconsA.Text) >= NTSCInt(edQuantDa) And NTSCInt(edDatconsA.Text) <= NTSCInt(edQuantA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129038029320052039, "La posizione del campo quantità non deve essere inclusa nella posizione del campo data consegna e viceversa."))
            Exit Sub
          End If
        End If
        If (ckPrezzo.Checked) And (ckSconto1.Checked) Then
          If (NTSCInt(edSc1Da.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc1Da.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edSc1A.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc1A.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edPreDa.Text) >= NTSCInt(edSc1Da) And NTSCInt(edPreDa.Text) <= NTSCInt(edSc1A)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edSc1Da) And NTSCInt(edPreA.Text) <= NTSCInt(edSc1A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260192292000, "La posizione del campo sconto 1 non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Exit Sub
          End If
        End If
        If (ckPrezzo.Checked) And (ckSconto2.Checked) Then
          If (NTSCInt(edSc2Da.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc2Da.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edSc2A.Text) >= NTSCInt(edPreDa) And NTSCInt(edSc2A.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edPreDa.Text) >= NTSCInt(edSc2Da) And NTSCInt(edPreDa.Text) <= NTSCInt(edSc2A)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edSc2Da) And NTSCInt(edPreA.Text) <= NTSCInt(edSc2A)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128775260201184000, "La posizione del campo sconto 2 non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Exit Sub
          End If
        End If
        If (ckPrezzo.Checked) And (ckDatcons.Checked) Then
          If (NTSCInt(edDatconsDa.Text) >= NTSCInt(edPreDa) And NTSCInt(edDatconsDa.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edDatconsA.Text) >= NTSCInt(edPreDa) And NTSCInt(edDatconsA.Text) <= NTSCInt(edPreA)) Or (NTSCInt(edPreDa.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edPreDa.Text) <= NTSCInt(edDatconsA)) Or (NTSCInt(edPreA.Text) >= NTSCInt(edDatconsDa) And NTSCInt(edPreA.Text) <= NTSCInt(edDatconsA)) Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129038030402922184, "La posizione del campo data consegna non deve essere inclusa nella posizione del campo prezzo e viceversa."))
            Exit Sub
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
            Return
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
          Return
        ElseIf lCount > 1 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129249492644077986, "Presenti più riferimenti all'articolo in importazione. Specificare solo una voce tra 'Articolo', 'Barcode' o 'Art. C/F'"))
          Return
        End If
      End If
      If edNomeFileE.Text.Trim <> "" Then
        'Ulteriori test pre salvataggio Export
        dtrRowE = oCleDocu.dttExport.Select("", "xx_pose")
        lCount = 0
        For z As Integer = 0 To dtrRowE.Length - 1
          If NTSCInt(dtrRowE(z)!xx_pose) <> z + 1 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 129248848155938667, "Le righe di esportazione non sono in progressione" & vbCrLf & "o contengono dei buchi nella numerazione della posizione."))
            Return
          End If
          Select Case NTSCStr(dtrRowE(z)!xx_nomee)
            Case "art" : lCount += 1
          End Select
        Next
        If lCount = 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129248846566971403, "Non sono stati messi riferimenti all'articolo in esportazione." & vbCrLf & "Specificare su una riga 'Articolo'"))
          Return
        End If
      End If
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
      Dim strTEMP As String = "A"
      Select Case cbCampi.SelectedValue.ToString
        Case "B" : strTEMP = "C"
        Case "C" : strTEMP = "B"
      End Select
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermTipoArt", strTEMP, " ", "...", "...", "...")
      If cbCampi.SelectedValue = "A" Then
        'se ho selezionato sia articolo che 'articolo o barcode'
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermTipoArt", "X", " ", "...", "...", "...")
      End If
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermPrezzi", IIf(ckPrezzo.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermQuant", IIf(ckQuant.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDescr", IIf(ckDescrizione.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermDesint", IIf(ckDesint.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont1", IIf(ckSconto1.Checked, "-1", "0").ToString, " ", "...", "...", "...")
      oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont2", IIf(ckSconto2.Checked, "-1", "0").ToString, " ", "...", "...", "...")
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
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont1Stop", edSc1A.Text, " ", "...", "...", "...")
        oMenu.SaveSettingBus(oCleDocu.strTermPrgOwner, oCleDocu.strTermKey, ".", "TermScont2Stop", edSc2A.Text, " ", "...", "...", "...")
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

      'Import
      If edNomeFileI.Text.Trim <> "" Then
        strReg = IIf(ckExecuteI.Checked, "-1", "0").ToString & "|" & lbCommandI.Text & "|" & IIf(ckSilentI.Checked, "-1", "0").ToString & "|" & _
                 edNomeFileI.Text & "|" & IIf(ckDelFileI.Checked, "-1", "0").ToString & "|" & IIf(ckIgnore1rkI.Checked, "-1", "0").ToString & "|" & _
                 IIf(ckPrezziI.Checked, "-1", "0").ToString & "|" & IIf(ckScontiI.Checked, "-1", "0").ToString & "|" & IIf(ckProvvI.Checked, "-1", "0").ToString & "|" & _
                 IIf(ckShowLogI.Checked, "-1", "0").ToString & "|" & IIf(opDelimI.Checked, "S", "F").ToString & "|" & edSepFieldI.Text & "|" & _
                 edDecSepI.Text & "|0|1|1|0|1|1|" & IIf(ckTipoRigaI.Checked, "-1", "0").ToString & "|"

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
                 "|" & IIf(opDelimE.Checked, "S", "F").ToString & "|" & edSepFieldE.Text & "|0|1|1|0|1|1|" & _
                 IIf(ckTipoRigaE.Checked, "-1", "0").ToString & "|" & IIf(ckRimuoviVbcrlf.Checked, "-1", "0").ToString & "|"

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

      Me.Close()
    Catch ex As Exception
      '------------------------------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '------------------------------------------------------------------------------------
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

  Public Overridable Sub ckSconto3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckSconto4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckSconto5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Try
      ImpostaCampi()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckSconto6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
      If cbCampi.SelectedValue.ToString = "C" Then
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

  Public Overridable Sub cbCampi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCampi.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case cbCampi.SelectedValue.ToString
        Case "B", "C", "D" : ImpostaCampi()
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class