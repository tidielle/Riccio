Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMDBFXOR
#Region "Controlli"
  Public WithEvents optContoSelezione As NTSInformatica.NTSRadioButton
  Public WithEvents optContoTutti As NTSInformatica.NTSRadioButton
  Public WithEvents lbContoFin As NTSInformatica.NTSLabel
  Public WithEvents edDatordIni As NTSInformatica.NTSTextBoxData
  Public WithEvents fmPulsanti As NTSInformatica.NTSGroupBox
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents fmClienti As NTSInformatica.NTSGroupBox
  Public WithEvents optContoAlcuni As NTSInformatica.NTSRadioButton
  Public WithEvents cmdContoSelezione As NTSInformatica.NTSButton
  Public WithEvents edContoIni As NTSInformatica.NTSTextBoxNum
  Public WithEvents edContoFin As NTSInformatica.NTSTextBoxNum
  Public WithEvents fmArticoli As NTSInformatica.NTSGroupBox
  Public WithEvents optArticoloAlcuni As NTSInformatica.NTSRadioButton
  Public WithEvents cmdArticoloSelezione As NTSInformatica.NTSButton
  Public WithEvents optArticoloTutti As NTSInformatica.NTSRadioButton
  Public WithEvents optArticoloSelezione As NTSInformatica.NTSRadioButton
  Public WithEvents lbCodartFin As NTSInformatica.NTSLabel
  Public WithEvents edCodartFin As NTSInformatica.NTSTextBoxStr
  Public WithEvents edCodartIni As NTSInformatica.NTSTextBoxStr
  Public WithEvents fmDatord As NTSInformatica.NTSGroupBox
  Public WithEvents optDatordAlcuni As NTSInformatica.NTSRadioButton
  Public WithEvents optDatordTutti As NTSInformatica.NTSRadioButton
  Public WithEvents lbDatordFin As NTSInformatica.NTSLabel
  Public WithEvents edDatordFin As NTSInformatica.NTSTextBoxData
  Public WithEvents fmDatcons As NTSInformatica.NTSGroupBox
  Public WithEvents edDatconsFin As NTSInformatica.NTSTextBoxData
  Public WithEvents optDatconsAlcuni As NTSInformatica.NTSRadioButton
  Public WithEvents optDatconsTutti As NTSInformatica.NTSRadioButton
  Public WithEvents lbDatconsFin As NTSInformatica.NTSLabel
  Public WithEvents edDatconsIni As NTSInformatica.NTSTextBoxData
  Public WithEvents fmNumord As NTSInformatica.NTSGroupBox
  Public WithEvents edNumordIni As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAnnoIni As NTSInformatica.NTSTextBoxNum
  Public WithEvents optNumordAlcuni As NTSInformatica.NTSRadioButton
  Public WithEvents optNumordTutti As NTSInformatica.NTSRadioButton
  Public WithEvents lbSerieIni As NTSInformatica.NTSLabel
  Public WithEvents edSerieIni As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbNumordIni As NTSInformatica.NTSLabel
  Public WithEvents lbRigaIni As NTSInformatica.NTSLabel
  Public WithEvents edRigaIni As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbRigaFin As NTSInformatica.NTSLabel
  Public WithEvents edRigaFin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbNumordFin As NTSInformatica.NTSLabel
  Public WithEvents edSerieFin As NTSInformatica.NTSTextBoxStr
  Public WithEvents edNumordFin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAnnoFin As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbSerieFin As NTSInformatica.NTSLabel
  Public WithEvents lbAnnoFin As NTSInformatica.NTSLabel
  Public WithEvents fmTipoBF As NTSInformatica.NTSGroupBox
  Public WithEvents edTipoBFFin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTipoBFIni As NTSInformatica.NTSTextBoxNum
  Public WithEvents optTipoBFAlcuni As NTSInformatica.NTSRadioButton
  Public WithEvents optTipoBFTutti As NTSInformatica.NTSRadioButton
  Public WithEvents lbTipoBFFin As NTSInformatica.NTSLabel
  Public WithEvents fmMagaz As NTSInformatica.NTSGroupBox
  Public WithEvents edMagazFin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edMagazIni As NTSInformatica.NTSTextBoxNum
  Public WithEvents optMagazAlcuni As NTSInformatica.NTSRadioButton
  Public WithEvents optMagazTutti As NTSInformatica.NTSRadioButton
  Public WithEvents lbMagazFin As NTSInformatica.NTSLabel
  Public WithEvents ckRigConfermate As NTSInformatica.NTSCheckBox
  Public WithEvents ckImpConfermati As NTSInformatica.NTSCheckBox
  Public WithEvents fmCodiceStagione As NTSInformatica.NTSGroupBox
  Public WithEvents edCodiceStagioneFin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCodiceStagioneIni As NTSInformatica.NTSTextBoxNum
  Public WithEvents optCodiceStagioneAlcuni As NTSInformatica.NTSRadioButton
  Public WithEvents optCodiceStagioneTutti As NTSInformatica.NTSRadioButton
  Public WithEvents lbCodiceStagioneFin As NTSInformatica.NTSLabel
  Public WithEvents fmAnnoStagione As NTSInformatica.NTSGroupBox
  Public WithEvents edAnnoStagioneFin As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAnnoStagioneIni As NTSInformatica.NTSTextBoxNum
  Public WithEvents optAnnoStagioneAlcuni As NTSInformatica.NTSRadioButton
  Public WithEvents optAnnoStagioneTutti As NTSInformatica.NTSRadioButton
  Public WithEvents lbAnnoStagioneFin As NTSInformatica.NTSLabel
#End Region

#Region "Variabili"
  Public oCleGnof As CLEORGNOF
  Public oCallParams As CLE__CLDP

  Public strMsg As String = ""

  Public bInit As Boolean = False
  Public bCancel As Boolean = True
  Public bAll As Boolean = False
  Public strSQLWHERE As String = ""
  Public strSQLWHEREODBC As String = ""
  Public strSQLWHEREIP As String = ""
  Public strSQLWHEREIPODBC As String = ""

  'Clienti
  Public nOpzClienti As Integer = 0
  Public lClientiDal As Integer = 0
  Public lClientiAl As Integer = 0

  'Articoli
  Public nOpzArticoli As Integer = 0
  Public strArticoliDal As String = ""
  Public strArticoliAl As String = ""

  'Data emiss. ordini
  Public nOpzDataEmiss As Integer = 0
  Public dtDataEmissDal As Date = Nothing
  Public dtDataEmissAl As Date = Nothing

  'Data cons. righe ordini
  Public nOpzDataCons As Integer = 0
  Public dtDataConsDal As Date = Nothing
  Public dtDataConsAl As Date = Nothing

  'Numero ordini
  Public nOpzNumeroOrdini As Integer = 0
  Public nAnnoDal As Integer = 0
  Public nAnnoAl As Integer = 0
  Public strSerieDal As String = ""
  Public strSerieAl As String = ""
  Public lNumeroDal As Integer = 0
  Public lNumeroAl As Integer = 0
  Public lRigaDal As Integer = 0
  Public lRigaAl As Integer = 0

  'Tipo bolla/fatt
  Public nOpzTpbf As Integer = 0
  Public nTpbfDal As Integer = 0
  Public nTpbfAl As Integer = 0

  'Magazzino
  Public nOpzMagaz As Integer = 0
  Public nMagazDal As Integer = 0
  Public nMagazAl As Integer = 0

  'Confermati
  Public bOrdiniConfermati As Boolean = False
  Public bRigheConfermate As Boolean = False

  'Taglie e colori
  Public nAnnoStagioneIni As Integer = 0
  Public nAnnoStagioneFin As Integer = 0
  Public nCodiceStagioneIni As Integer = 0
  Public nCodiceStagioneFin As Integer = 0
  'Gestione filtri 
  Public bModTCO As Boolean = False
#End Region

#Region "Inizializzazione Controlli"
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

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '----------------------------------------------------------------------------------
      '--- TextBox
      '----------------------------------------------------------------------------------
      edContoIni.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128789227018907212, "Dal codice conto"), tabanagrac)
      edContoFin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128789227051250962, "al codice conto"), tabanagrac)
      edCodartIni.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128789227071094712, "Dal codice articolo"), tabartico, True)
      edCodartFin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128789227086250962, "al codice articolo"), tabartico, True)
      edDatordIni.NTSSetParam(oMenu, oApp.Tr(Me, 128789227104844712, "Dalla data ordine"), True)
      edDatordFin.NTSSetParam(oMenu, oApp.Tr(Me, 128789227120782212, "Dalla data ordine"), True)
      edDatconsIni.NTSSetParam(oMenu, oApp.Tr(Me, 128789227141250962, "Dalla data consegna"), True)
      edDatconsFin.NTSSetParam(oMenu, oApp.Tr(Me, 128789227158438462, "alla data consegna"), True)
      edTipoBFIni.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128789227178125962, "Dal tipo bolla/fattura"), tabtpbf)
      edTipoBFFin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128789227200469712, "al tipo bolla/fattura"), tabtpbf)
      edMagazIni.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128789227217344712, "Dal magazzino"), tabmaga)
      edMagazFin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128789227237813462, "al magazzino"), tabmaga)
      edAnnoIni.NTSSetParam(oMenu, oApp.Tr(Me, 128789227255313462, "Dall'anno"), "0", 4, 0, 2099)
      edAnnoFin.NTSSetParam(oMenu, oApp.Tr(Me, 128789227276719712, "all'anno"), "0", 4, 0, 2099)
      edSerieIni.NTSSetParam(oMenu, oApp.Tr(Me, 128789227320625962, "Dalla serie"), CLN__STD.SerieMaxLen, False)
      edSerieFin.NTSSetParam(oMenu, oApp.Tr(Me, 128789227347032212, "alla serie"), CLN__STD.SerieMaxLen, False)
      edNumordIni.NTSSetParam(oMenu, oApp.Tr(Me, 128789227366094712, "Dal numero"), "0", 9, 0, 999999999)
      edNumordFin.NTSSetParam(oMenu, oApp.Tr(Me, 128789227385938462, "Dal numero"), "0", 9, 0, 999999999)
      edRigaIni.NTSSetParam(oMenu, oApp.Tr(Me, 128789227404844712, "Dalla riga"), "0", 9, 0, 999999999)
      edRigaFin.NTSSetParam(oMenu, oApp.Tr(Me, 128789227420000962, "alla riga"), "0", 9, 0, 999999999)
      edAnnoStagioneIni.NTSSetParam(oMenu, oApp.Tr(Me, 130246840382505844, "Dall'anno stagione"), "0", 4, 0, 2099)
      edAnnoStagioneFin.NTSSetParam(oMenu, oApp.Tr(Me, 130246840573290757, "all'anno stagione"), "0", 4, 0, 2099)
      edCodiceStagioneIni.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130246841959411120, "Dal codice stagione taglie e colori"), tabstag)
      edCodiceStagioneFin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130246846067302490, "Al codice stagione taglie e colori"), tabstag)
      '----------------------------------------------------------------------------------
      '--- OptionButton
      '----------------------------------------------------------------------------------
      optContoTutti.NTSSetParam(oMenu, oApp.Tr(Me, 128789104206454692, "Tutti (Clienti/Fornitori)"), "S")
      optContoSelezione.NTSSetParam(oMenu, oApp.Tr(Me, 128789104309110942, "Selezione (Clienti/Fornitori)"), "S")
      optContoAlcuni.NTSSetParam(oMenu, oApp.Tr(Me, 128789104628954692, "Alcuni (Clienti/Fornitori)"), "S")
      optArticoloTutti.NTSSetParam(oMenu, oApp.Tr(Me, 128789104919423442, "Tutti (Articoli)"), "S")
      optArticoloSelezione.NTSSetParam(oMenu, oApp.Tr(Me, 128789104941610942, "Selezione (Articoli)"), "S")
      optArticoloAlcuni.NTSSetParam(oMenu, oApp.Tr(Me, 128789104968329692, "Alcuni (Articoli)"), "S")
      optDatordTutti.NTSSetParam(oMenu, oApp.Tr(Me, 128789105361610942, "Qualsiasi (Data di emissione Ordini/Impegni))"), "S")
      optDatordAlcuni.NTSSetParam(oMenu, oApp.Tr(Me, 128789105382392192, "Alcune (Data di emissione Ordini/Impegni)"), "S")
      optDatconsTutti.NTSSetParam(oMenu, oApp.Tr(Me, 128789105934110942, "Qualsiasi (Data di consegna righe))"), "S")
      optDatconsAlcuni.NTSSetParam(oMenu, oApp.Tr(Me, 128789105956142192, "Alcune (Data di consegna righe)"), "S")
      optTipoBFTutti.NTSSetParam(oMenu, oApp.Tr(Me, 128789106404892192, "Tutti (Tipo Bolla/Fattura))"), "S")
      optTipoBFAlcuni.NTSSetParam(oMenu, oApp.Tr(Me, 128789106430360942, "Alcuni (Tipo Bolla/Fattura)"), "S")
      optMagazTutti.NTSSetParam(oMenu, oApp.Tr(Me, 128789106805048442, "Tutti (Magazzini))"), "S")
      optMagazAlcuni.NTSSetParam(oMenu, oApp.Tr(Me, 128789106826142192, "Alcuni (Magazzini)"), "S")
      optNumordTutti.NTSSetParam(oMenu, oApp.Tr(Me, 128789107384110942, "Tutti (Numero Ordini/Impegni))"), "S")
      optNumordAlcuni.NTSSetParam(oMenu, oApp.Tr(Me, 128789107404892192, "Alcuni (Numero Ordini/Impegni)"), "S")
      optAnnoStagioneTutti.NTSSetParam(oMenu, oApp.Tr(Me, 130246838853726492, "Tutti (Anno Stagione))"), "S")
      optAnnoStagioneAlcuni.NTSSetParam(oMenu, oApp.Tr(Me, 130246839365611320, "Alcuni (Anno Stagione))"), "S")
      optCodiceStagioneTutti.NTSSetParam(oMenu, oApp.Tr(Me, 130246839090137281, "Tutti (Codice Stagione))"), "S")
      optCodiceStagioneAlcuni.NTSSetParam(oMenu, oApp.Tr(Me, 130246839582802990, "Alcuni (Codice Stagione))"), "S")
      '----------------------------------------------------------------------------------
      '--- CheckBox
      '----------------------------------------------------------------------------------
      ckImpConfermati.NTSSetParam(oMenu, oApp.Tr(Me, 128789108425048442, "Impegni confermate"), "S", "N")
      ckRigConfermate.NTSSetParam(oMenu, oApp.Tr(Me, 128789107996454692, "Righe confermate"), "S", "N")
      '----------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub InitEntity(ByRef cleGnof As CLEORGNOF)
    oCleGnof = cleGnof
    AddHandler oCleGnof.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Private Sub InitializeComponent()
    Me.optContoSelezione = New NTSInformatica.NTSRadioButton
    Me.optContoTutti = New NTSInformatica.NTSRadioButton
    Me.lbContoFin = New NTSInformatica.NTSLabel
    Me.edDatordIni = New NTSInformatica.NTSTextBoxData
    Me.fmPulsanti = New NTSInformatica.NTSGroupBox
    Me.ckRigConfermate = New NTSInformatica.NTSCheckBox
    Me.ckImpConfermati = New NTSInformatica.NTSCheckBox
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.fmClienti = New NTSInformatica.NTSGroupBox
    Me.edContoFin = New NTSInformatica.NTSTextBoxNum
    Me.edContoIni = New NTSInformatica.NTSTextBoxNum
    Me.optContoAlcuni = New NTSInformatica.NTSRadioButton
    Me.cmdContoSelezione = New NTSInformatica.NTSButton
    Me.fmArticoli = New NTSInformatica.NTSGroupBox
    Me.edCodartFin = New NTSInformatica.NTSTextBoxStr
    Me.optArticoloAlcuni = New NTSInformatica.NTSRadioButton
    Me.edCodartIni = New NTSInformatica.NTSTextBoxStr
    Me.cmdArticoloSelezione = New NTSInformatica.NTSButton
    Me.optArticoloTutti = New NTSInformatica.NTSRadioButton
    Me.optArticoloSelezione = New NTSInformatica.NTSRadioButton
    Me.lbCodartFin = New NTSInformatica.NTSLabel
    Me.fmDatord = New NTSInformatica.NTSGroupBox
    Me.edDatordFin = New NTSInformatica.NTSTextBoxData
    Me.optDatordAlcuni = New NTSInformatica.NTSRadioButton
    Me.optDatordTutti = New NTSInformatica.NTSRadioButton
    Me.lbDatordFin = New NTSInformatica.NTSLabel
    Me.fmDatcons = New NTSInformatica.NTSGroupBox
    Me.edDatconsFin = New NTSInformatica.NTSTextBoxData
    Me.optDatconsAlcuni = New NTSInformatica.NTSRadioButton
    Me.optDatconsTutti = New NTSInformatica.NTSRadioButton
    Me.lbDatconsFin = New NTSInformatica.NTSLabel
    Me.edDatconsIni = New NTSInformatica.NTSTextBoxData
    Me.fmNumord = New NTSInformatica.NTSGroupBox
    Me.lbAnnoFin = New NTSInformatica.NTSLabel
    Me.lbRigaFin = New NTSInformatica.NTSLabel
    Me.edRigaFin = New NTSInformatica.NTSTextBoxNum
    Me.lbNumordFin = New NTSInformatica.NTSLabel
    Me.edSerieFin = New NTSInformatica.NTSTextBoxStr
    Me.edNumordFin = New NTSInformatica.NTSTextBoxNum
    Me.edAnnoFin = New NTSInformatica.NTSTextBoxNum
    Me.lbSerieFin = New NTSInformatica.NTSLabel
    Me.lbRigaIni = New NTSInformatica.NTSLabel
    Me.edRigaIni = New NTSInformatica.NTSTextBoxNum
    Me.lbNumordIni = New NTSInformatica.NTSLabel
    Me.edSerieIni = New NTSInformatica.NTSTextBoxStr
    Me.edNumordIni = New NTSInformatica.NTSTextBoxNum
    Me.edAnnoIni = New NTSInformatica.NTSTextBoxNum
    Me.optNumordAlcuni = New NTSInformatica.NTSRadioButton
    Me.optNumordTutti = New NTSInformatica.NTSRadioButton
    Me.lbSerieIni = New NTSInformatica.NTSLabel
    Me.fmTipoBF = New NTSInformatica.NTSGroupBox
    Me.edTipoBFFin = New NTSInformatica.NTSTextBoxNum
    Me.edTipoBFIni = New NTSInformatica.NTSTextBoxNum
    Me.optTipoBFAlcuni = New NTSInformatica.NTSRadioButton
    Me.optTipoBFTutti = New NTSInformatica.NTSRadioButton
    Me.lbTipoBFFin = New NTSInformatica.NTSLabel
    Me.fmMagaz = New NTSInformatica.NTSGroupBox
    Me.edMagazFin = New NTSInformatica.NTSTextBoxNum
    Me.edMagazIni = New NTSInformatica.NTSTextBoxNum
    Me.optMagazAlcuni = New NTSInformatica.NTSRadioButton
    Me.optMagazTutti = New NTSInformatica.NTSRadioButton
    Me.lbMagazFin = New NTSInformatica.NTSLabel
    Me.fmCodiceStagione = New NTSInformatica.NTSGroupBox
    Me.edCodiceStagioneFin = New NTSInformatica.NTSTextBoxNum
    Me.edCodiceStagioneIni = New NTSInformatica.NTSTextBoxNum
    Me.optCodiceStagioneAlcuni = New NTSInformatica.NTSRadioButton
    Me.optCodiceStagioneTutti = New NTSInformatica.NTSRadioButton
    Me.lbCodiceStagioneFin = New NTSInformatica.NTSLabel
    Me.fmAnnoStagione = New NTSInformatica.NTSGroupBox
    Me.edAnnoStagioneFin = New NTSInformatica.NTSTextBoxNum
    Me.edAnnoStagioneIni = New NTSInformatica.NTSTextBoxNum
    Me.optAnnoStagioneAlcuni = New NTSInformatica.NTSRadioButton
    Me.optAnnoStagioneTutti = New NTSInformatica.NTSRadioButton
    Me.lbAnnoStagioneFin = New NTSInformatica.NTSLabel
    CType(Me.optContoSelezione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optContoTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatordIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmPulsanti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPulsanti.SuspendLayout()
    CType(Me.ckRigConfermate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckImpConfermati.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmClienti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmClienti.SuspendLayout()
    CType(Me.edContoFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContoIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optContoAlcuni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmArticoli, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmArticoli.SuspendLayout()
    CType(Me.edCodartFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optArticoloAlcuni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodartIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optArticoloTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optArticoloSelezione.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDatord, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDatord.SuspendLayout()
    CType(Me.edDatordFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optDatordAlcuni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optDatordTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmDatcons, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDatcons.SuspendLayout()
    CType(Me.edDatconsFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optDatconsAlcuni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optDatconsTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatconsIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmNumord, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmNumord.SuspendLayout()
    CType(Me.edRigaFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerieFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumordFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnoFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRigaIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerieIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edNumordIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnoIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optNumordAlcuni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optNumordTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTipoBF, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTipoBF.SuspendLayout()
    CType(Me.edTipoBFFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTipoBFIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optTipoBFAlcuni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optTipoBFTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmMagaz, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmMagaz.SuspendLayout()
    CType(Me.edMagazFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMagazIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optMagazAlcuni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optMagazTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmCodiceStagione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCodiceStagione.SuspendLayout()
    CType(Me.edCodiceStagioneFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodiceStagioneIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optCodiceStagioneAlcuni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optCodiceStagioneTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmAnnoStagione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmAnnoStagione.SuspendLayout()
    CType(Me.edAnnoStagioneFin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnoStagioneIni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optAnnoStagioneAlcuni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optAnnoStagioneTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'optContoSelezione
    '
    Me.optContoSelezione.Cursor = System.Windows.Forms.Cursors.Default
    Me.optContoSelezione.Enabled = False
    Me.optContoSelezione.Location = New System.Drawing.Point(12, 45)
    Me.optContoSelezione.Name = "optContoSelezione"
    Me.optContoSelezione.NTSCheckValue = "S"
    Me.optContoSelezione.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optContoSelezione.Properties.Appearance.Options.UseBackColor = True
    Me.optContoSelezione.Properties.Caption = "Selezione"
    Me.optContoSelezione.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optContoSelezione.Size = New System.Drawing.Size(75, 19)
    Me.optContoSelezione.TabIndex = 39
    '
    'optContoTutti
    '
    Me.optContoTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.optContoTutti.EditValue = True
    Me.optContoTutti.Location = New System.Drawing.Point(12, 23)
    Me.optContoTutti.Name = "optContoTutti"
    Me.optContoTutti.NTSCheckValue = "S"
    Me.optContoTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optContoTutti.Properties.Appearance.Options.UseBackColor = True
    Me.optContoTutti.Properties.Caption = "Tutti"
    Me.optContoTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optContoTutti.Size = New System.Drawing.Size(48, 19)
    Me.optContoTutti.TabIndex = 38
    '
    'lbContoFin
    '
    Me.lbContoFin.AutoSize = True
    Me.lbContoFin.BackColor = System.Drawing.Color.Transparent
    Me.lbContoFin.Enabled = False
    Me.lbContoFin.Location = New System.Drawing.Point(187, 69)
    Me.lbContoFin.Name = "lbContoFin"
    Me.lbContoFin.NTSDbField = ""
    Me.lbContoFin.Size = New System.Drawing.Size(49, 13)
    Me.lbContoFin.TabIndex = 7
    Me.lbContoFin.Text = "Al codice"
    Me.lbContoFin.Tooltip = ""
    Me.lbContoFin.UseMnemonic = False
    '
    'edDatordIni
    '
    Me.edDatordIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatordIni.Enabled = False
    Me.edDatordIni.Location = New System.Drawing.Point(91, 46)
    Me.edDatordIni.Name = "edDatordIni"
    Me.edDatordIni.NTSDbField = ""
    Me.edDatordIni.NTSForzaVisZoom = False
    Me.edDatordIni.NTSOldValue = ""
    Me.edDatordIni.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDatordIni.Properties.Appearance.Options.UseBackColor = True
    Me.edDatordIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatordIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatordIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatordIni.Properties.MaxLength = 65536
    Me.edDatordIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatordIni.Size = New System.Drawing.Size(88, 20)
    Me.edDatordIni.TabIndex = 6
    '
    'fmPulsanti
    '
    Me.fmPulsanti.AllowDrop = True
    Me.fmPulsanti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPulsanti.Appearance.Options.UseBackColor = True
    Me.fmPulsanti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.fmPulsanti.Controls.Add(Me.ckRigConfermate)
    Me.fmPulsanti.Controls.Add(Me.ckImpConfermati)
    Me.fmPulsanti.Controls.Add(Me.cmdAnnulla)
    Me.fmPulsanti.Controls.Add(Me.cmdOk)
    Me.fmPulsanti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPulsanti.Location = New System.Drawing.Point(507, 347)
    Me.fmPulsanti.Name = "fmPulsanti"
    Me.fmPulsanti.ShowCaption = False
    Me.fmPulsanti.Size = New System.Drawing.Size(193, 93)
    Me.fmPulsanti.TabIndex = 35
    '
    'ckRigConfermate
    '
    Me.ckRigConfermate.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckRigConfermate.Location = New System.Drawing.Point(6, 22)
    Me.ckRigConfermate.Name = "ckRigConfermate"
    Me.ckRigConfermate.NTSCheckValue = "S"
    Me.ckRigConfermate.NTSUnCheckValue = "N"
    Me.ckRigConfermate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRigConfermate.Properties.Appearance.Options.UseBackColor = True
    Me.ckRigConfermate.Properties.Caption = "Righe confermate"
    Me.ckRigConfermate.Size = New System.Drawing.Size(114, 19)
    Me.ckRigConfermate.TabIndex = 38
    '
    'ckImpConfermati
    '
    Me.ckImpConfermati.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckImpConfermati.Location = New System.Drawing.Point(6, 3)
    Me.ckImpConfermati.Name = "ckImpConfermati"
    Me.ckImpConfermati.NTSCheckValue = "S"
    Me.ckImpConfermati.NTSUnCheckValue = "N"
    Me.ckImpConfermati.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckImpConfermati.Properties.Appearance.Options.UseBackColor = True
    Me.ckImpConfermati.Properties.Caption = "Impegni confermati"
    Me.ckImpConfermati.Size = New System.Drawing.Size(125, 19)
    Me.ckImpConfermati.TabIndex = 37
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 68)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(187, 24)
    Me.cmdAnnulla.TabIndex = 5
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(3, 43)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(187, 24)
    Me.cmdOk.TabIndex = 4
    Me.cmdOk.Text = "Ok"
    '
    'fmClienti
    '
    Me.fmClienti.AllowDrop = True
    Me.fmClienti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmClienti.Appearance.Options.UseBackColor = True
    Me.fmClienti.Controls.Add(Me.edContoFin)
    Me.fmClienti.Controls.Add(Me.edContoIni)
    Me.fmClienti.Controls.Add(Me.optContoAlcuni)
    Me.fmClienti.Controls.Add(Me.cmdContoSelezione)
    Me.fmClienti.Controls.Add(Me.optContoTutti)
    Me.fmClienti.Controls.Add(Me.optContoSelezione)
    Me.fmClienti.Controls.Add(Me.lbContoFin)
    Me.fmClienti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmClienti.Location = New System.Drawing.Point(12, 8)
    Me.fmClienti.Name = "fmClienti"
    Me.fmClienti.Size = New System.Drawing.Size(341, 94)
    Me.fmClienti.TabIndex = 40
    Me.fmClienti.Text = "Clienti/Fornitori"
    '
    'edContoFin
    '
    Me.edContoFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContoFin.EditValue = ""
    Me.edContoFin.Enabled = False
    Me.edContoFin.Location = New System.Drawing.Point(240, 66)
    Me.edContoFin.Name = "edContoFin"
    Me.edContoFin.NTSDbField = ""
    Me.edContoFin.NTSFormat = "0"
    Me.edContoFin.NTSForzaVisZoom = False
    Me.edContoFin.NTSOldValue = ""
    Me.edContoFin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edContoFin.Properties.Appearance.Options.UseBackColor = True
    Me.edContoFin.Properties.Appearance.Options.UseTextOptions = True
    Me.edContoFin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edContoFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edContoFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edContoFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edContoFin.Properties.MaxLength = 65536
    Me.edContoFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edContoFin.Size = New System.Drawing.Size(88, 20)
    Me.edContoFin.TabIndex = 59
    '
    'edContoIni
    '
    Me.edContoIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContoIni.EditValue = ""
    Me.edContoIni.Enabled = False
    Me.edContoIni.Location = New System.Drawing.Point(93, 66)
    Me.edContoIni.Name = "edContoIni"
    Me.edContoIni.NTSDbField = ""
    Me.edContoIni.NTSFormat = "0"
    Me.edContoIni.NTSForzaVisZoom = False
    Me.edContoIni.NTSOldValue = ""
    Me.edContoIni.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edContoIni.Properties.Appearance.Options.UseBackColor = True
    Me.edContoIni.Properties.Appearance.Options.UseTextOptions = True
    Me.edContoIni.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edContoIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edContoIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edContoIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edContoIni.Properties.MaxLength = 65536
    Me.edContoIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edContoIni.Size = New System.Drawing.Size(88, 20)
    Me.edContoIni.TabIndex = 58
    '
    'optContoAlcuni
    '
    Me.optContoAlcuni.Cursor = System.Windows.Forms.Cursors.Default
    Me.optContoAlcuni.Location = New System.Drawing.Point(12, 67)
    Me.optContoAlcuni.Name = "optContoAlcuni"
    Me.optContoAlcuni.NTSCheckValue = "S"
    Me.optContoAlcuni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optContoAlcuni.Properties.Appearance.Options.UseBackColor = True
    Me.optContoAlcuni.Properties.Caption = "Dal codice"
    Me.optContoAlcuni.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optContoAlcuni.Size = New System.Drawing.Size(75, 19)
    Me.optContoAlcuni.TabIndex = 41
    '
    'cmdContoSelezione
    '
    Me.cmdContoSelezione.Enabled = False
    Me.cmdContoSelezione.ImageText = ""
    Me.cmdContoSelezione.Location = New System.Drawing.Point(93, 44)
    Me.cmdContoSelezione.Name = "cmdContoSelezione"
    Me.cmdContoSelezione.Size = New System.Drawing.Size(168, 20)
    Me.cmdContoSelezione.TabIndex = 40
    Me.cmdContoSelezione.Text = "Seleziona Clienti/Fornitori"
    '
    'fmArticoli
    '
    Me.fmArticoli.AllowDrop = True
    Me.fmArticoli.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmArticoli.Appearance.Options.UseBackColor = True
    Me.fmArticoli.Controls.Add(Me.edCodartFin)
    Me.fmArticoli.Controls.Add(Me.optArticoloAlcuni)
    Me.fmArticoli.Controls.Add(Me.edCodartIni)
    Me.fmArticoli.Controls.Add(Me.cmdArticoloSelezione)
    Me.fmArticoli.Controls.Add(Me.optArticoloTutti)
    Me.fmArticoli.Controls.Add(Me.optArticoloSelezione)
    Me.fmArticoli.Controls.Add(Me.lbCodartFin)
    Me.fmArticoli.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmArticoli.Location = New System.Drawing.Point(361, 8)
    Me.fmArticoli.Name = "fmArticoli"
    Me.fmArticoli.Size = New System.Drawing.Size(341, 94)
    Me.fmArticoli.TabIndex = 41
    Me.fmArticoli.Text = "Articoli"
    '
    'edCodartFin
    '
    Me.edCodartFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodartFin.Enabled = False
    Me.edCodartFin.Location = New System.Drawing.Point(240, 67)
    Me.edCodartFin.Name = "edCodartFin"
    Me.edCodartFin.NTSDbField = ""
    Me.edCodartFin.NTSForzaVisZoom = False
    Me.edCodartFin.NTSOldValue = ""
    Me.edCodartFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodartFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodartFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodartFin.Properties.MaxLength = 65536
    Me.edCodartFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodartFin.Size = New System.Drawing.Size(88, 20)
    Me.edCodartFin.TabIndex = 43
    '
    'optArticoloAlcuni
    '
    Me.optArticoloAlcuni.Cursor = System.Windows.Forms.Cursors.Hand
    Me.optArticoloAlcuni.Location = New System.Drawing.Point(12, 67)
    Me.optArticoloAlcuni.Name = "optArticoloAlcuni"
    Me.optArticoloAlcuni.NTSCheckValue = "S"
    Me.optArticoloAlcuni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optArticoloAlcuni.Properties.Appearance.Options.UseBackColor = True
    Me.optArticoloAlcuni.Properties.Caption = "Dal codice"
    Me.optArticoloAlcuni.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optArticoloAlcuni.Size = New System.Drawing.Size(75, 19)
    Me.optArticoloAlcuni.TabIndex = 41
    '
    'edCodartIni
    '
    Me.edCodartIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodartIni.Enabled = False
    Me.edCodartIni.Location = New System.Drawing.Point(93, 67)
    Me.edCodartIni.Name = "edCodartIni"
    Me.edCodartIni.NTSDbField = ""
    Me.edCodartIni.NTSForzaVisZoom = False
    Me.edCodartIni.NTSOldValue = ""
    Me.edCodartIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodartIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodartIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodartIni.Properties.MaxLength = 65536
    Me.edCodartIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodartIni.Size = New System.Drawing.Size(88, 20)
    Me.edCodartIni.TabIndex = 42
    '
    'cmdArticoloSelezione
    '
    Me.cmdArticoloSelezione.Enabled = False
    Me.cmdArticoloSelezione.ImageText = ""
    Me.cmdArticoloSelezione.Location = New System.Drawing.Point(93, 44)
    Me.cmdArticoloSelezione.Name = "cmdArticoloSelezione"
    Me.cmdArticoloSelezione.Size = New System.Drawing.Size(168, 20)
    Me.cmdArticoloSelezione.TabIndex = 40
    Me.cmdArticoloSelezione.Text = "Seleziona Articoli"
    '
    'optArticoloTutti
    '
    Me.optArticoloTutti.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.optArticoloTutti.EditValue = True
    Me.optArticoloTutti.Location = New System.Drawing.Point(12, 23)
    Me.optArticoloTutti.Name = "optArticoloTutti"
    Me.optArticoloTutti.NTSCheckValue = "S"
    Me.optArticoloTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optArticoloTutti.Properties.Appearance.Options.UseBackColor = True
    Me.optArticoloTutti.Properties.Caption = "Tutti"
    Me.optArticoloTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optArticoloTutti.Size = New System.Drawing.Size(48, 19)
    Me.optArticoloTutti.TabIndex = 38
    '
    'optArticoloSelezione
    '
    Me.optArticoloSelezione.Cursor = System.Windows.Forms.Cursors.Default
    Me.optArticoloSelezione.Enabled = False
    Me.optArticoloSelezione.Location = New System.Drawing.Point(12, 45)
    Me.optArticoloSelezione.Name = "optArticoloSelezione"
    Me.optArticoloSelezione.NTSCheckValue = "S"
    Me.optArticoloSelezione.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optArticoloSelezione.Properties.Appearance.Options.UseBackColor = True
    Me.optArticoloSelezione.Properties.Caption = "Selezione"
    Me.optArticoloSelezione.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optArticoloSelezione.Size = New System.Drawing.Size(75, 19)
    Me.optArticoloSelezione.TabIndex = 39
    '
    'lbCodartFin
    '
    Me.lbCodartFin.AutoSize = True
    Me.lbCodartFin.BackColor = System.Drawing.Color.Transparent
    Me.lbCodartFin.Enabled = False
    Me.lbCodartFin.Location = New System.Drawing.Point(187, 70)
    Me.lbCodartFin.Name = "lbCodartFin"
    Me.lbCodartFin.NTSDbField = ""
    Me.lbCodartFin.Size = New System.Drawing.Size(49, 13)
    Me.lbCodartFin.TabIndex = 7
    Me.lbCodartFin.Text = "Al codice"
    Me.lbCodartFin.Tooltip = ""
    Me.lbCodartFin.UseMnemonic = False
    '
    'fmDatord
    '
    Me.fmDatord.AllowDrop = True
    Me.fmDatord.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDatord.Appearance.Options.UseBackColor = True
    Me.fmDatord.Controls.Add(Me.edDatordFin)
    Me.fmDatord.Controls.Add(Me.optDatordAlcuni)
    Me.fmDatord.Controls.Add(Me.optDatordTutti)
    Me.fmDatord.Controls.Add(Me.lbDatordFin)
    Me.fmDatord.Controls.Add(Me.edDatordIni)
    Me.fmDatord.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDatord.Location = New System.Drawing.Point(14, 106)
    Me.fmDatord.Name = "fmDatord"
    Me.fmDatord.Size = New System.Drawing.Size(341, 74)
    Me.fmDatord.TabIndex = 42
    Me.fmDatord.Text = "Data di emissione Ordini/Impegni"
    '
    'edDatordFin
    '
    Me.edDatordFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatordFin.Enabled = False
    Me.edDatordFin.Location = New System.Drawing.Point(238, 46)
    Me.edDatordFin.Name = "edDatordFin"
    Me.edDatordFin.NTSDbField = ""
    Me.edDatordFin.NTSForzaVisZoom = False
    Me.edDatordFin.NTSOldValue = ""
    Me.edDatordFin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDatordFin.Properties.Appearance.Options.UseBackColor = True
    Me.edDatordFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatordFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatordFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatordFin.Properties.MaxLength = 65536
    Me.edDatordFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatordFin.Size = New System.Drawing.Size(88, 20)
    Me.edDatordFin.TabIndex = 42
    '
    'optDatordAlcuni
    '
    Me.optDatordAlcuni.Cursor = System.Windows.Forms.Cursors.Default
    Me.optDatordAlcuni.Location = New System.Drawing.Point(12, 47)
    Me.optDatordAlcuni.Name = "optDatordAlcuni"
    Me.optDatordAlcuni.NTSCheckValue = "S"
    Me.optDatordAlcuni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optDatordAlcuni.Properties.Appearance.Options.UseBackColor = True
    Me.optDatordAlcuni.Properties.Caption = "Dalla data"
    Me.optDatordAlcuni.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optDatordAlcuni.Size = New System.Drawing.Size(75, 19)
    Me.optDatordAlcuni.TabIndex = 41
    '
    'optDatordTutti
    '
    Me.optDatordTutti.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.optDatordTutti.EditValue = True
    Me.optDatordTutti.Location = New System.Drawing.Point(12, 23)
    Me.optDatordTutti.Name = "optDatordTutti"
    Me.optDatordTutti.NTSCheckValue = "S"
    Me.optDatordTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optDatordTutti.Properties.Appearance.Options.UseBackColor = True
    Me.optDatordTutti.Properties.Caption = "Qualsiasi"
    Me.optDatordTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optDatordTutti.Size = New System.Drawing.Size(66, 19)
    Me.optDatordTutti.TabIndex = 38
    '
    'lbDatordFin
    '
    Me.lbDatordFin.AutoSize = True
    Me.lbDatordFin.BackColor = System.Drawing.Color.Transparent
    Me.lbDatordFin.Enabled = False
    Me.lbDatordFin.Location = New System.Drawing.Point(187, 49)
    Me.lbDatordFin.Name = "lbDatordFin"
    Me.lbDatordFin.NTSDbField = ""
    Me.lbDatordFin.Size = New System.Drawing.Size(48, 13)
    Me.lbDatordFin.TabIndex = 7
    Me.lbDatordFin.Text = "alla data"
    Me.lbDatordFin.Tooltip = ""
    Me.lbDatordFin.UseMnemonic = False
    '
    'fmDatcons
    '
    Me.fmDatcons.AllowDrop = True
    Me.fmDatcons.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDatcons.Appearance.Options.UseBackColor = True
    Me.fmDatcons.Controls.Add(Me.edDatconsFin)
    Me.fmDatcons.Controls.Add(Me.optDatconsAlcuni)
    Me.fmDatcons.Controls.Add(Me.optDatconsTutti)
    Me.fmDatcons.Controls.Add(Me.lbDatconsFin)
    Me.fmDatcons.Controls.Add(Me.edDatconsIni)
    Me.fmDatcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDatcons.Location = New System.Drawing.Point(361, 106)
    Me.fmDatcons.Name = "fmDatcons"
    Me.fmDatcons.Size = New System.Drawing.Size(341, 74)
    Me.fmDatcons.TabIndex = 43
    Me.fmDatcons.Text = "Data di consegna righe"
    '
    'edDatconsFin
    '
    Me.edDatconsFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatconsFin.Enabled = False
    Me.edDatconsFin.Location = New System.Drawing.Point(240, 46)
    Me.edDatconsFin.Name = "edDatconsFin"
    Me.edDatconsFin.NTSDbField = ""
    Me.edDatconsFin.NTSForzaVisZoom = False
    Me.edDatconsFin.NTSOldValue = ""
    Me.edDatconsFin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDatconsFin.Properties.Appearance.Options.UseBackColor = True
    Me.edDatconsFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatconsFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatconsFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatconsFin.Properties.MaxLength = 65536
    Me.edDatconsFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatconsFin.Size = New System.Drawing.Size(88, 20)
    Me.edDatconsFin.TabIndex = 42
    '
    'optDatconsAlcuni
    '
    Me.optDatconsAlcuni.Cursor = System.Windows.Forms.Cursors.Default
    Me.optDatconsAlcuni.Location = New System.Drawing.Point(12, 47)
    Me.optDatconsAlcuni.Name = "optDatconsAlcuni"
    Me.optDatconsAlcuni.NTSCheckValue = "S"
    Me.optDatconsAlcuni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optDatconsAlcuni.Properties.Appearance.Options.UseBackColor = True
    Me.optDatconsAlcuni.Properties.Caption = "Dalla data"
    Me.optDatconsAlcuni.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optDatconsAlcuni.Size = New System.Drawing.Size(75, 19)
    Me.optDatconsAlcuni.TabIndex = 41
    '
    'optDatconsTutti
    '
    Me.optDatconsTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.optDatconsTutti.EditValue = True
    Me.optDatconsTutti.Location = New System.Drawing.Point(12, 23)
    Me.optDatconsTutti.Name = "optDatconsTutti"
    Me.optDatconsTutti.NTSCheckValue = "S"
    Me.optDatconsTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optDatconsTutti.Properties.Appearance.Options.UseBackColor = True
    Me.optDatconsTutti.Properties.Caption = "Qualsiasi"
    Me.optDatconsTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optDatconsTutti.Size = New System.Drawing.Size(64, 19)
    Me.optDatconsTutti.TabIndex = 38
    '
    'lbDatconsFin
    '
    Me.lbDatconsFin.AutoSize = True
    Me.lbDatconsFin.BackColor = System.Drawing.Color.Transparent
    Me.lbDatconsFin.Enabled = False
    Me.lbDatconsFin.Location = New System.Drawing.Point(187, 49)
    Me.lbDatconsFin.Name = "lbDatconsFin"
    Me.lbDatconsFin.NTSDbField = ""
    Me.lbDatconsFin.Size = New System.Drawing.Size(48, 13)
    Me.lbDatconsFin.TabIndex = 7
    Me.lbDatconsFin.Text = "alla data"
    Me.lbDatconsFin.Tooltip = ""
    Me.lbDatconsFin.UseMnemonic = False
    '
    'edDatconsIni
    '
    Me.edDatconsIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatconsIni.Enabled = False
    Me.edDatconsIni.Location = New System.Drawing.Point(93, 46)
    Me.edDatconsIni.Name = "edDatconsIni"
    Me.edDatconsIni.NTSDbField = ""
    Me.edDatconsIni.NTSForzaVisZoom = False
    Me.edDatconsIni.NTSOldValue = ""
    Me.edDatconsIni.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDatconsIni.Properties.Appearance.Options.UseBackColor = True
    Me.edDatconsIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatconsIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatconsIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatconsIni.Properties.MaxLength = 65536
    Me.edDatconsIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatconsIni.Size = New System.Drawing.Size(88, 20)
    Me.edDatconsIni.TabIndex = 6
    '
    'fmNumord
    '
    Me.fmNumord.AllowDrop = True
    Me.fmNumord.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmNumord.Appearance.Options.UseBackColor = True
    Me.fmNumord.Controls.Add(Me.lbAnnoFin)
    Me.fmNumord.Controls.Add(Me.lbRigaFin)
    Me.fmNumord.Controls.Add(Me.edRigaFin)
    Me.fmNumord.Controls.Add(Me.lbNumordFin)
    Me.fmNumord.Controls.Add(Me.edSerieFin)
    Me.fmNumord.Controls.Add(Me.edNumordFin)
    Me.fmNumord.Controls.Add(Me.edAnnoFin)
    Me.fmNumord.Controls.Add(Me.lbSerieFin)
    Me.fmNumord.Controls.Add(Me.lbRigaIni)
    Me.fmNumord.Controls.Add(Me.edRigaIni)
    Me.fmNumord.Controls.Add(Me.lbNumordIni)
    Me.fmNumord.Controls.Add(Me.edSerieIni)
    Me.fmNumord.Controls.Add(Me.edNumordIni)
    Me.fmNumord.Controls.Add(Me.edAnnoIni)
    Me.fmNumord.Controls.Add(Me.optNumordAlcuni)
    Me.fmNumord.Controls.Add(Me.optNumordTutti)
    Me.fmNumord.Controls.Add(Me.lbSerieIni)
    Me.fmNumord.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmNumord.Location = New System.Drawing.Point(12, 346)
    Me.fmNumord.Name = "fmNumord"
    Me.fmNumord.Size = New System.Drawing.Size(489, 94)
    Me.fmNumord.TabIndex = 44
    Me.fmNumord.Text = "Numero Ordini/Impegni"
    '
    'lbAnnoFin
    '
    Me.lbAnnoFin.AutoSize = True
    Me.lbAnnoFin.BackColor = System.Drawing.Color.Transparent
    Me.lbAnnoFin.Enabled = False
    Me.lbAnnoFin.Location = New System.Drawing.Point(32, 69)
    Me.lbAnnoFin.Name = "lbAnnoFin"
    Me.lbAnnoFin.NTSDbField = ""
    Me.lbAnnoFin.Size = New System.Drawing.Size(46, 13)
    Me.lbAnnoFin.TabIndex = 71
    Me.lbAnnoFin.Text = "ad anno"
    Me.lbAnnoFin.Tooltip = ""
    Me.lbAnnoFin.UseMnemonic = False
    '
    'lbRigaFin
    '
    Me.lbRigaFin.AutoSize = True
    Me.lbRigaFin.BackColor = System.Drawing.Color.Transparent
    Me.lbRigaFin.Enabled = False
    Me.lbRigaFin.Location = New System.Drawing.Point(360, 69)
    Me.lbRigaFin.Name = "lbRigaFin"
    Me.lbRigaFin.NTSDbField = ""
    Me.lbRigaFin.Size = New System.Drawing.Size(28, 13)
    Me.lbRigaFin.TabIndex = 70
    Me.lbRigaFin.Text = "Riga"
    Me.lbRigaFin.Tooltip = ""
    Me.lbRigaFin.UseMnemonic = False
    '
    'edRigaFin
    '
    Me.edRigaFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRigaFin.EditValue = ""
    Me.edRigaFin.Enabled = False
    Me.edRigaFin.Location = New System.Drawing.Point(390, 66)
    Me.edRigaFin.Name = "edRigaFin"
    Me.edRigaFin.NTSDbField = ""
    Me.edRigaFin.NTSFormat = "0"
    Me.edRigaFin.NTSForzaVisZoom = False
    Me.edRigaFin.NTSOldValue = ""
    Me.edRigaFin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edRigaFin.Properties.Appearance.Options.UseBackColor = True
    Me.edRigaFin.Properties.Appearance.Options.UseTextOptions = True
    Me.edRigaFin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edRigaFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRigaFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRigaFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRigaFin.Properties.MaxLength = 65536
    Me.edRigaFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRigaFin.Size = New System.Drawing.Size(88, 20)
    Me.edRigaFin.TabIndex = 69
    '
    'lbNumordFin
    '
    Me.lbNumordFin.AutoSize = True
    Me.lbNumordFin.BackColor = System.Drawing.Color.Transparent
    Me.lbNumordFin.Enabled = False
    Me.lbNumordFin.Location = New System.Drawing.Point(220, 69)
    Me.lbNumordFin.Name = "lbNumordFin"
    Me.lbNumordFin.NTSDbField = ""
    Me.lbNumordFin.Size = New System.Drawing.Size(44, 13)
    Me.lbNumordFin.TabIndex = 68
    Me.lbNumordFin.Text = "Numero"
    Me.lbNumordFin.Tooltip = ""
    Me.lbNumordFin.UseMnemonic = False
    '
    'edSerieFin
    '
    Me.edSerieFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerieFin.Enabled = False
    Me.edSerieFin.Location = New System.Drawing.Point(180, 66)
    Me.edSerieFin.Name = "edSerieFin"
    Me.edSerieFin.NTSDbField = ""
    Me.edSerieFin.NTSForzaVisZoom = False
    Me.edSerieFin.NTSOldValue = ""
    Me.edSerieFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerieFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerieFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerieFin.Properties.MaxLength = 65536
    Me.edSerieFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerieFin.Size = New System.Drawing.Size(45, 20)
    Me.edSerieFin.TabIndex = 67
    '
    'edNumordFin
    '
    Me.edNumordFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumordFin.EditValue = ""
    Me.edNumordFin.Enabled = False
    Me.edNumordFin.Location = New System.Drawing.Point(266, 66)
    Me.edNumordFin.Name = "edNumordFin"
    Me.edNumordFin.NTSDbField = ""
    Me.edNumordFin.NTSFormat = "0"
    Me.edNumordFin.NTSForzaVisZoom = False
    Me.edNumordFin.NTSOldValue = ""
    Me.edNumordFin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edNumordFin.Properties.Appearance.Options.UseBackColor = True
    Me.edNumordFin.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumordFin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumordFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumordFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumordFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumordFin.Properties.MaxLength = 65536
    Me.edNumordFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumordFin.Size = New System.Drawing.Size(88, 20)
    Me.edNumordFin.TabIndex = 66
    '
    'edAnnoFin
    '
    Me.edAnnoFin.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAnnoFin.EditValue = ""
    Me.edAnnoFin.Enabled = False
    Me.edAnnoFin.Location = New System.Drawing.Point(91, 66)
    Me.edAnnoFin.Name = "edAnnoFin"
    Me.edAnnoFin.NTSDbField = ""
    Me.edAnnoFin.NTSFormat = "0"
    Me.edAnnoFin.NTSForzaVisZoom = False
    Me.edAnnoFin.NTSOldValue = ""
    Me.edAnnoFin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAnnoFin.Properties.Appearance.Options.UseBackColor = True
    Me.edAnnoFin.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnoFin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnoFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnoFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnoFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnoFin.Properties.MaxLength = 65536
    Me.edAnnoFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnoFin.Size = New System.Drawing.Size(47, 20)
    Me.edAnnoFin.TabIndex = 65
    '
    'lbSerieFin
    '
    Me.lbSerieFin.AutoSize = True
    Me.lbSerieFin.BackColor = System.Drawing.Color.Transparent
    Me.lbSerieFin.Enabled = False
    Me.lbSerieFin.Location = New System.Drawing.Point(148, 69)
    Me.lbSerieFin.Name = "lbSerieFin"
    Me.lbSerieFin.NTSDbField = ""
    Me.lbSerieFin.Size = New System.Drawing.Size(31, 13)
    Me.lbSerieFin.TabIndex = 64
    Me.lbSerieFin.Text = "Serie"
    Me.lbSerieFin.Tooltip = ""
    Me.lbSerieFin.UseMnemonic = False
    '
    'lbRigaIni
    '
    Me.lbRigaIni.AutoSize = True
    Me.lbRigaIni.BackColor = System.Drawing.Color.Transparent
    Me.lbRigaIni.Enabled = False
    Me.lbRigaIni.Location = New System.Drawing.Point(360, 47)
    Me.lbRigaIni.Name = "lbRigaIni"
    Me.lbRigaIni.NTSDbField = ""
    Me.lbRigaIni.Size = New System.Drawing.Size(28, 13)
    Me.lbRigaIni.TabIndex = 63
    Me.lbRigaIni.Text = "Riga"
    Me.lbRigaIni.Tooltip = ""
    Me.lbRigaIni.UseMnemonic = False
    '
    'edRigaIni
    '
    Me.edRigaIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRigaIni.EditValue = ""
    Me.edRigaIni.Enabled = False
    Me.edRigaIni.Location = New System.Drawing.Point(390, 44)
    Me.edRigaIni.Name = "edRigaIni"
    Me.edRigaIni.NTSDbField = ""
    Me.edRigaIni.NTSFormat = "0"
    Me.edRigaIni.NTSForzaVisZoom = False
    Me.edRigaIni.NTSOldValue = ""
    Me.edRigaIni.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edRigaIni.Properties.Appearance.Options.UseBackColor = True
    Me.edRigaIni.Properties.Appearance.Options.UseTextOptions = True
    Me.edRigaIni.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edRigaIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRigaIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRigaIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRigaIni.Properties.MaxLength = 65536
    Me.edRigaIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRigaIni.Size = New System.Drawing.Size(88, 20)
    Me.edRigaIni.TabIndex = 62
    '
    'lbNumordIni
    '
    Me.lbNumordIni.AutoSize = True
    Me.lbNumordIni.BackColor = System.Drawing.Color.Transparent
    Me.lbNumordIni.Enabled = False
    Me.lbNumordIni.Location = New System.Drawing.Point(220, 47)
    Me.lbNumordIni.Name = "lbNumordIni"
    Me.lbNumordIni.NTSDbField = ""
    Me.lbNumordIni.Size = New System.Drawing.Size(44, 13)
    Me.lbNumordIni.TabIndex = 61
    Me.lbNumordIni.Text = "Numero"
    Me.lbNumordIni.Tooltip = ""
    Me.lbNumordIni.UseMnemonic = False
    '
    'edSerieIni
    '
    Me.edSerieIni.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edSerieIni.Enabled = False
    Me.edSerieIni.Location = New System.Drawing.Point(180, 44)
    Me.edSerieIni.Name = "edSerieIni"
    Me.edSerieIni.NTSDbField = ""
    Me.edSerieIni.NTSForzaVisZoom = False
    Me.edSerieIni.NTSOldValue = ""
    Me.edSerieIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerieIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerieIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerieIni.Properties.MaxLength = 65536
    Me.edSerieIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerieIni.Size = New System.Drawing.Size(45, 20)
    Me.edSerieIni.TabIndex = 60
    '
    'edNumordIni
    '
    Me.edNumordIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumordIni.EditValue = ""
    Me.edNumordIni.Enabled = False
    Me.edNumordIni.Location = New System.Drawing.Point(266, 44)
    Me.edNumordIni.Name = "edNumordIni"
    Me.edNumordIni.NTSDbField = ""
    Me.edNumordIni.NTSFormat = "0"
    Me.edNumordIni.NTSForzaVisZoom = False
    Me.edNumordIni.NTSOldValue = ""
    Me.edNumordIni.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edNumordIni.Properties.Appearance.Options.UseBackColor = True
    Me.edNumordIni.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumordIni.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumordIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumordIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumordIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumordIni.Properties.MaxLength = 65536
    Me.edNumordIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumordIni.Size = New System.Drawing.Size(88, 20)
    Me.edNumordIni.TabIndex = 59
    '
    'edAnnoIni
    '
    Me.edAnnoIni.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edAnnoIni.EditValue = ""
    Me.edAnnoIni.Enabled = False
    Me.edAnnoIni.Location = New System.Drawing.Point(91, 44)
    Me.edAnnoIni.Name = "edAnnoIni"
    Me.edAnnoIni.NTSDbField = ""
    Me.edAnnoIni.NTSFormat = "0"
    Me.edAnnoIni.NTSForzaVisZoom = False
    Me.edAnnoIni.NTSOldValue = ""
    Me.edAnnoIni.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAnnoIni.Properties.Appearance.Options.UseBackColor = True
    Me.edAnnoIni.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnoIni.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnoIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnoIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnoIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnoIni.Properties.MaxLength = 65536
    Me.edAnnoIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnoIni.Size = New System.Drawing.Size(47, 20)
    Me.edAnnoIni.TabIndex = 58
    '
    'optNumordAlcuni
    '
    Me.optNumordAlcuni.Cursor = System.Windows.Forms.Cursors.Default
    Me.optNumordAlcuni.Location = New System.Drawing.Point(12, 45)
    Me.optNumordAlcuni.Name = "optNumordAlcuni"
    Me.optNumordAlcuni.NTSCheckValue = "S"
    Me.optNumordAlcuni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optNumordAlcuni.Properties.Appearance.Options.UseBackColor = True
    Me.optNumordAlcuni.Properties.Caption = "Da anno"
    Me.optNumordAlcuni.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optNumordAlcuni.Size = New System.Drawing.Size(75, 19)
    Me.optNumordAlcuni.TabIndex = 41
    '
    'optNumordTutti
    '
    Me.optNumordTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.optNumordTutti.EditValue = True
    Me.optNumordTutti.Location = New System.Drawing.Point(12, 23)
    Me.optNumordTutti.Name = "optNumordTutti"
    Me.optNumordTutti.NTSCheckValue = "S"
    Me.optNumordTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optNumordTutti.Properties.Appearance.Options.UseBackColor = True
    Me.optNumordTutti.Properties.Caption = "Tutti"
    Me.optNumordTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optNumordTutti.Size = New System.Drawing.Size(48, 19)
    Me.optNumordTutti.TabIndex = 38
    '
    'lbSerieIni
    '
    Me.lbSerieIni.AutoSize = True
    Me.lbSerieIni.BackColor = System.Drawing.Color.Transparent
    Me.lbSerieIni.Enabled = False
    Me.lbSerieIni.Location = New System.Drawing.Point(148, 47)
    Me.lbSerieIni.Name = "lbSerieIni"
    Me.lbSerieIni.NTSDbField = ""
    Me.lbSerieIni.Size = New System.Drawing.Size(31, 13)
    Me.lbSerieIni.TabIndex = 7
    Me.lbSerieIni.Text = "Serie"
    Me.lbSerieIni.Tooltip = ""
    Me.lbSerieIni.UseMnemonic = False
    '
    'fmTipoBF
    '
    Me.fmTipoBF.AllowDrop = True
    Me.fmTipoBF.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTipoBF.Appearance.Options.UseBackColor = True
    Me.fmTipoBF.Controls.Add(Me.edTipoBFFin)
    Me.fmTipoBF.Controls.Add(Me.edTipoBFIni)
    Me.fmTipoBF.Controls.Add(Me.optTipoBFAlcuni)
    Me.fmTipoBF.Controls.Add(Me.optTipoBFTutti)
    Me.fmTipoBF.Controls.Add(Me.lbTipoBFFin)
    Me.fmTipoBF.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTipoBF.Location = New System.Drawing.Point(14, 184)
    Me.fmTipoBF.Name = "fmTipoBF"
    Me.fmTipoBF.Size = New System.Drawing.Size(341, 74)
    Me.fmTipoBF.TabIndex = 46
    Me.fmTipoBF.Text = "Tipo Bolla/Fattura"
    '
    'edTipoBFFin
    '
    Me.edTipoBFFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTipoBFFin.EditValue = ""
    Me.edTipoBFFin.Enabled = False
    Me.edTipoBFFin.Location = New System.Drawing.Point(240, 46)
    Me.edTipoBFFin.Name = "edTipoBFFin"
    Me.edTipoBFFin.NTSDbField = ""
    Me.edTipoBFFin.NTSFormat = "0"
    Me.edTipoBFFin.NTSForzaVisZoom = False
    Me.edTipoBFFin.NTSOldValue = ""
    Me.edTipoBFFin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTipoBFFin.Properties.Appearance.Options.UseBackColor = True
    Me.edTipoBFFin.Properties.Appearance.Options.UseTextOptions = True
    Me.edTipoBFFin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTipoBFFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTipoBFFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTipoBFFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTipoBFFin.Properties.MaxLength = 65536
    Me.edTipoBFFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTipoBFFin.Size = New System.Drawing.Size(88, 20)
    Me.edTipoBFFin.TabIndex = 59
    '
    'edTipoBFIni
    '
    Me.edTipoBFIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTipoBFIni.EditValue = ""
    Me.edTipoBFIni.Enabled = False
    Me.edTipoBFIni.Location = New System.Drawing.Point(93, 46)
    Me.edTipoBFIni.Name = "edTipoBFIni"
    Me.edTipoBFIni.NTSDbField = ""
    Me.edTipoBFIni.NTSFormat = "0"
    Me.edTipoBFIni.NTSForzaVisZoom = False
    Me.edTipoBFIni.NTSOldValue = ""
    Me.edTipoBFIni.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTipoBFIni.Properties.Appearance.Options.UseBackColor = True
    Me.edTipoBFIni.Properties.Appearance.Options.UseTextOptions = True
    Me.edTipoBFIni.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTipoBFIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTipoBFIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTipoBFIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTipoBFIni.Properties.MaxLength = 65536
    Me.edTipoBFIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTipoBFIni.Size = New System.Drawing.Size(88, 20)
    Me.edTipoBFIni.TabIndex = 58
    '
    'optTipoBFAlcuni
    '
    Me.optTipoBFAlcuni.Cursor = System.Windows.Forms.Cursors.Default
    Me.optTipoBFAlcuni.Location = New System.Drawing.Point(12, 47)
    Me.optTipoBFAlcuni.Name = "optTipoBFAlcuni"
    Me.optTipoBFAlcuni.NTSCheckValue = "S"
    Me.optTipoBFAlcuni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optTipoBFAlcuni.Properties.Appearance.Options.UseBackColor = True
    Me.optTipoBFAlcuni.Properties.Caption = "Dal codice"
    Me.optTipoBFAlcuni.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optTipoBFAlcuni.Size = New System.Drawing.Size(75, 19)
    Me.optTipoBFAlcuni.TabIndex = 41
    '
    'optTipoBFTutti
    '
    Me.optTipoBFTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.optTipoBFTutti.EditValue = True
    Me.optTipoBFTutti.Location = New System.Drawing.Point(12, 23)
    Me.optTipoBFTutti.Name = "optTipoBFTutti"
    Me.optTipoBFTutti.NTSCheckValue = "S"
    Me.optTipoBFTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optTipoBFTutti.Properties.Appearance.Options.UseBackColor = True
    Me.optTipoBFTutti.Properties.Caption = "Tutti"
    Me.optTipoBFTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optTipoBFTutti.Size = New System.Drawing.Size(48, 19)
    Me.optTipoBFTutti.TabIndex = 38
    '
    'lbTipoBFFin
    '
    Me.lbTipoBFFin.AutoSize = True
    Me.lbTipoBFFin.BackColor = System.Drawing.Color.Transparent
    Me.lbTipoBFFin.Enabled = False
    Me.lbTipoBFFin.Location = New System.Drawing.Point(187, 49)
    Me.lbTipoBFFin.Name = "lbTipoBFFin"
    Me.lbTipoBFFin.NTSDbField = ""
    Me.lbTipoBFFin.Size = New System.Drawing.Size(49, 13)
    Me.lbTipoBFFin.TabIndex = 7
    Me.lbTipoBFFin.Text = "Al codice"
    Me.lbTipoBFFin.Tooltip = ""
    Me.lbTipoBFFin.UseMnemonic = False
    '
    'fmMagaz
    '
    Me.fmMagaz.AllowDrop = True
    Me.fmMagaz.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmMagaz.Appearance.Options.UseBackColor = True
    Me.fmMagaz.Controls.Add(Me.edMagazFin)
    Me.fmMagaz.Controls.Add(Me.edMagazIni)
    Me.fmMagaz.Controls.Add(Me.optMagazAlcuni)
    Me.fmMagaz.Controls.Add(Me.optMagazTutti)
    Me.fmMagaz.Controls.Add(Me.lbMagazFin)
    Me.fmMagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmMagaz.Location = New System.Drawing.Point(361, 184)
    Me.fmMagaz.Name = "fmMagaz"
    Me.fmMagaz.Size = New System.Drawing.Size(341, 74)
    Me.fmMagaz.TabIndex = 47
    Me.fmMagaz.Text = "Magazzini"
    '
    'edMagazFin
    '
    Me.edMagazFin.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edMagazFin.EditValue = ""
    Me.edMagazFin.Enabled = False
    Me.edMagazFin.Location = New System.Drawing.Point(240, 46)
    Me.edMagazFin.Name = "edMagazFin"
    Me.edMagazFin.NTSDbField = ""
    Me.edMagazFin.NTSFormat = "0"
    Me.edMagazFin.NTSForzaVisZoom = False
    Me.edMagazFin.NTSOldValue = ""
    Me.edMagazFin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edMagazFin.Properties.Appearance.Options.UseBackColor = True
    Me.edMagazFin.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagazFin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagazFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagazFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagazFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagazFin.Properties.MaxLength = 65536
    Me.edMagazFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagazFin.Size = New System.Drawing.Size(88, 20)
    Me.edMagazFin.TabIndex = 59
    '
    'edMagazIni
    '
    Me.edMagazIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagazIni.EditValue = ""
    Me.edMagazIni.Enabled = False
    Me.edMagazIni.Location = New System.Drawing.Point(93, 46)
    Me.edMagazIni.Name = "edMagazIni"
    Me.edMagazIni.NTSDbField = ""
    Me.edMagazIni.NTSFormat = "0"
    Me.edMagazIni.NTSForzaVisZoom = False
    Me.edMagazIni.NTSOldValue = ""
    Me.edMagazIni.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edMagazIni.Properties.Appearance.Options.UseBackColor = True
    Me.edMagazIni.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagazIni.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagazIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagazIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagazIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagazIni.Properties.MaxLength = 65536
    Me.edMagazIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagazIni.Size = New System.Drawing.Size(88, 20)
    Me.edMagazIni.TabIndex = 58
    '
    'optMagazAlcuni
    '
    Me.optMagazAlcuni.Cursor = System.Windows.Forms.Cursors.Default
    Me.optMagazAlcuni.Location = New System.Drawing.Point(12, 47)
    Me.optMagazAlcuni.Name = "optMagazAlcuni"
    Me.optMagazAlcuni.NTSCheckValue = "S"
    Me.optMagazAlcuni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optMagazAlcuni.Properties.Appearance.Options.UseBackColor = True
    Me.optMagazAlcuni.Properties.Caption = "Dal codice"
    Me.optMagazAlcuni.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optMagazAlcuni.Size = New System.Drawing.Size(75, 19)
    Me.optMagazAlcuni.TabIndex = 41
    '
    'optMagazTutti
    '
    Me.optMagazTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.optMagazTutti.EditValue = True
    Me.optMagazTutti.Location = New System.Drawing.Point(12, 23)
    Me.optMagazTutti.Name = "optMagazTutti"
    Me.optMagazTutti.NTSCheckValue = "S"
    Me.optMagazTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optMagazTutti.Properties.Appearance.Options.UseBackColor = True
    Me.optMagazTutti.Properties.Caption = "Tutti"
    Me.optMagazTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optMagazTutti.Size = New System.Drawing.Size(48, 19)
    Me.optMagazTutti.TabIndex = 38
    '
    'lbMagazFin
    '
    Me.lbMagazFin.AutoSize = True
    Me.lbMagazFin.BackColor = System.Drawing.Color.Transparent
    Me.lbMagazFin.Enabled = False
    Me.lbMagazFin.Location = New System.Drawing.Point(187, 49)
    Me.lbMagazFin.Name = "lbMagazFin"
    Me.lbMagazFin.NTSDbField = ""
    Me.lbMagazFin.Size = New System.Drawing.Size(49, 13)
    Me.lbMagazFin.TabIndex = 7
    Me.lbMagazFin.Text = "Al codice"
    Me.lbMagazFin.Tooltip = ""
    Me.lbMagazFin.UseMnemonic = False
    '
    'fmCodiceStagione
    '
    Me.fmCodiceStagione.AllowDrop = True
    Me.fmCodiceStagione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCodiceStagione.Appearance.Options.UseBackColor = True
    Me.fmCodiceStagione.Controls.Add(Me.edCodiceStagioneFin)
    Me.fmCodiceStagione.Controls.Add(Me.edCodiceStagioneIni)
    Me.fmCodiceStagione.Controls.Add(Me.optCodiceStagioneAlcuni)
    Me.fmCodiceStagione.Controls.Add(Me.optCodiceStagioneTutti)
    Me.fmCodiceStagione.Controls.Add(Me.lbCodiceStagioneFin)
    Me.fmCodiceStagione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCodiceStagione.Location = New System.Drawing.Point(14, 264)
    Me.fmCodiceStagione.Name = "fmCodiceStagione"
    Me.fmCodiceStagione.Size = New System.Drawing.Size(341, 74)
    Me.fmCodiceStagione.TabIndex = 49
    Me.fmCodiceStagione.Text = "Codice stagione"
    '
    'edCodiceStagioneFin
    '
    Me.edCodiceStagioneFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodiceStagioneFin.EditValue = ""
    Me.edCodiceStagioneFin.Enabled = False
    Me.edCodiceStagioneFin.Location = New System.Drawing.Point(240, 46)
    Me.edCodiceStagioneFin.Name = "edCodiceStagioneFin"
    Me.edCodiceStagioneFin.NTSDbField = ""
    Me.edCodiceStagioneFin.NTSFormat = "0"
    Me.edCodiceStagioneFin.NTSForzaVisZoom = False
    Me.edCodiceStagioneFin.NTSOldValue = ""
    Me.edCodiceStagioneFin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodiceStagioneFin.Properties.Appearance.Options.UseBackColor = True
    Me.edCodiceStagioneFin.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodiceStagioneFin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodiceStagioneFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodiceStagioneFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodiceStagioneFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodiceStagioneFin.Properties.MaxLength = 65536
    Me.edCodiceStagioneFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodiceStagioneFin.Size = New System.Drawing.Size(88, 20)
    Me.edCodiceStagioneFin.TabIndex = 59
    '
    'edCodiceStagioneIni
    '
    Me.edCodiceStagioneIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodiceStagioneIni.EditValue = ""
    Me.edCodiceStagioneIni.Enabled = False
    Me.edCodiceStagioneIni.Location = New System.Drawing.Point(93, 46)
    Me.edCodiceStagioneIni.Name = "edCodiceStagioneIni"
    Me.edCodiceStagioneIni.NTSDbField = ""
    Me.edCodiceStagioneIni.NTSFormat = "0"
    Me.edCodiceStagioneIni.NTSForzaVisZoom = False
    Me.edCodiceStagioneIni.NTSOldValue = ""
    Me.edCodiceStagioneIni.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodiceStagioneIni.Properties.Appearance.Options.UseBackColor = True
    Me.edCodiceStagioneIni.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodiceStagioneIni.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodiceStagioneIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodiceStagioneIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodiceStagioneIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodiceStagioneIni.Properties.MaxLength = 65536
    Me.edCodiceStagioneIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodiceStagioneIni.Size = New System.Drawing.Size(88, 20)
    Me.edCodiceStagioneIni.TabIndex = 58
    '
    'optCodiceStagioneAlcuni
    '
    Me.optCodiceStagioneAlcuni.Cursor = System.Windows.Forms.Cursors.Default
    Me.optCodiceStagioneAlcuni.Location = New System.Drawing.Point(12, 47)
    Me.optCodiceStagioneAlcuni.Name = "optCodiceStagioneAlcuni"
    Me.optCodiceStagioneAlcuni.NTSCheckValue = "S"
    Me.optCodiceStagioneAlcuni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optCodiceStagioneAlcuni.Properties.Appearance.Options.UseBackColor = True
    Me.optCodiceStagioneAlcuni.Properties.Caption = "Dal codice"
    Me.optCodiceStagioneAlcuni.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optCodiceStagioneAlcuni.Size = New System.Drawing.Size(75, 19)
    Me.optCodiceStagioneAlcuni.TabIndex = 41
    '
    'optCodiceStagioneTutti
    '
    Me.optCodiceStagioneTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.optCodiceStagioneTutti.EditValue = True
    Me.optCodiceStagioneTutti.Location = New System.Drawing.Point(12, 23)
    Me.optCodiceStagioneTutti.Name = "optCodiceStagioneTutti"
    Me.optCodiceStagioneTutti.NTSCheckValue = "S"
    Me.optCodiceStagioneTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optCodiceStagioneTutti.Properties.Appearance.Options.UseBackColor = True
    Me.optCodiceStagioneTutti.Properties.Caption = "Tutti"
    Me.optCodiceStagioneTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optCodiceStagioneTutti.Size = New System.Drawing.Size(48, 19)
    Me.optCodiceStagioneTutti.TabIndex = 38
    '
    'lbCodiceStagioneFin
    '
    Me.lbCodiceStagioneFin.AutoSize = True
    Me.lbCodiceStagioneFin.BackColor = System.Drawing.Color.Transparent
    Me.lbCodiceStagioneFin.Enabled = False
    Me.lbCodiceStagioneFin.Location = New System.Drawing.Point(187, 49)
    Me.lbCodiceStagioneFin.Name = "lbCodiceStagioneFin"
    Me.lbCodiceStagioneFin.NTSDbField = ""
    Me.lbCodiceStagioneFin.Size = New System.Drawing.Size(49, 13)
    Me.lbCodiceStagioneFin.TabIndex = 7
    Me.lbCodiceStagioneFin.Text = "Al codice"
    Me.lbCodiceStagioneFin.Tooltip = ""
    Me.lbCodiceStagioneFin.UseMnemonic = False
    '
    'fmAnnoStagione
    '
    Me.fmAnnoStagione.AllowDrop = True
    Me.fmAnnoStagione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmAnnoStagione.Appearance.Options.UseBackColor = True
    Me.fmAnnoStagione.Controls.Add(Me.edAnnoStagioneFin)
    Me.fmAnnoStagione.Controls.Add(Me.edAnnoStagioneIni)
    Me.fmAnnoStagione.Controls.Add(Me.optAnnoStagioneAlcuni)
    Me.fmAnnoStagione.Controls.Add(Me.optAnnoStagioneTutti)
    Me.fmAnnoStagione.Controls.Add(Me.lbAnnoStagioneFin)
    Me.fmAnnoStagione.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmAnnoStagione.Location = New System.Drawing.Point(361, 264)
    Me.fmAnnoStagione.Name = "fmAnnoStagione"
    Me.fmAnnoStagione.Size = New System.Drawing.Size(341, 74)
    Me.fmAnnoStagione.TabIndex = 48
    Me.fmAnnoStagione.Text = "Anno stagione"
    '
    'edAnnoStagioneFin
    '
    Me.edAnnoStagioneFin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnnoStagioneFin.EditValue = ""
    Me.edAnnoStagioneFin.Enabled = False
    Me.edAnnoStagioneFin.Location = New System.Drawing.Point(240, 46)
    Me.edAnnoStagioneFin.Name = "edAnnoStagioneFin"
    Me.edAnnoStagioneFin.NTSDbField = ""
    Me.edAnnoStagioneFin.NTSFormat = "0"
    Me.edAnnoStagioneFin.NTSForzaVisZoom = False
    Me.edAnnoStagioneFin.NTSOldValue = ""
    Me.edAnnoStagioneFin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAnnoStagioneFin.Properties.Appearance.Options.UseBackColor = True
    Me.edAnnoStagioneFin.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnoStagioneFin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnoStagioneFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnoStagioneFin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnoStagioneFin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnoStagioneFin.Properties.MaxLength = 65536
    Me.edAnnoStagioneFin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnoStagioneFin.Size = New System.Drawing.Size(88, 20)
    Me.edAnnoStagioneFin.TabIndex = 59
    '
    'edAnnoStagioneIni
    '
    Me.edAnnoStagioneIni.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnnoStagioneIni.EditValue = ""
    Me.edAnnoStagioneIni.Enabled = False
    Me.edAnnoStagioneIni.Location = New System.Drawing.Point(93, 46)
    Me.edAnnoStagioneIni.Name = "edAnnoStagioneIni"
    Me.edAnnoStagioneIni.NTSDbField = ""
    Me.edAnnoStagioneIni.NTSFormat = "0"
    Me.edAnnoStagioneIni.NTSForzaVisZoom = False
    Me.edAnnoStagioneIni.NTSOldValue = ""
    Me.edAnnoStagioneIni.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAnnoStagioneIni.Properties.Appearance.Options.UseBackColor = True
    Me.edAnnoStagioneIni.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnoStagioneIni.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnoStagioneIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnoStagioneIni.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnoStagioneIni.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnoStagioneIni.Properties.MaxLength = 65536
    Me.edAnnoStagioneIni.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnoStagioneIni.Size = New System.Drawing.Size(88, 20)
    Me.edAnnoStagioneIni.TabIndex = 58
    '
    'optAnnoStagioneAlcuni
    '
    Me.optAnnoStagioneAlcuni.Cursor = System.Windows.Forms.Cursors.Default
    Me.optAnnoStagioneAlcuni.Location = New System.Drawing.Point(12, 47)
    Me.optAnnoStagioneAlcuni.Name = "optAnnoStagioneAlcuni"
    Me.optAnnoStagioneAlcuni.NTSCheckValue = "S"
    Me.optAnnoStagioneAlcuni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optAnnoStagioneAlcuni.Properties.Appearance.Options.UseBackColor = True
    Me.optAnnoStagioneAlcuni.Properties.Caption = "Dall'anno"
    Me.optAnnoStagioneAlcuni.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optAnnoStagioneAlcuni.Size = New System.Drawing.Size(75, 19)
    Me.optAnnoStagioneAlcuni.TabIndex = 41
    '
    'optAnnoStagioneTutti
    '
    Me.optAnnoStagioneTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.optAnnoStagioneTutti.EditValue = True
    Me.optAnnoStagioneTutti.Location = New System.Drawing.Point(12, 23)
    Me.optAnnoStagioneTutti.Name = "optAnnoStagioneTutti"
    Me.optAnnoStagioneTutti.NTSCheckValue = "S"
    Me.optAnnoStagioneTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optAnnoStagioneTutti.Properties.Appearance.Options.UseBackColor = True
    Me.optAnnoStagioneTutti.Properties.Caption = "Tutti"
    Me.optAnnoStagioneTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optAnnoStagioneTutti.Size = New System.Drawing.Size(48, 19)
    Me.optAnnoStagioneTutti.TabIndex = 38
    '
    'lbAnnoStagioneFin
    '
    Me.lbAnnoStagioneFin.AutoSize = True
    Me.lbAnnoStagioneFin.BackColor = System.Drawing.Color.Transparent
    Me.lbAnnoStagioneFin.Enabled = False
    Me.lbAnnoStagioneFin.Location = New System.Drawing.Point(187, 49)
    Me.lbAnnoStagioneFin.Name = "lbAnnoStagioneFin"
    Me.lbAnnoStagioneFin.NTSDbField = ""
    Me.lbAnnoStagioneFin.Size = New System.Drawing.Size(44, 13)
    Me.lbAnnoStagioneFin.TabIndex = 7
    Me.lbAnnoStagioneFin.Text = "All'anno"
    Me.lbAnnoStagioneFin.Tooltip = ""
    Me.lbAnnoStagioneFin.UseMnemonic = False
    '
    'FRMDBFXOR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(714, 447)
    Me.Controls.Add(Me.fmCodiceStagione)
    Me.Controls.Add(Me.fmAnnoStagione)
    Me.Controls.Add(Me.fmMagaz)
    Me.Controls.Add(Me.fmTipoBF)
    Me.Controls.Add(Me.fmNumord)
    Me.Controls.Add(Me.fmDatcons)
    Me.Controls.Add(Me.fmPulsanti)
    Me.Controls.Add(Me.fmDatord)
    Me.Controls.Add(Me.fmArticoli)
    Me.Controls.Add(Me.fmClienti)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMDBFXOR"
    Me.Text = "SELEZIONE RIGHE ORDINI/IMPEGNI"
    CType(Me.optContoSelezione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optContoTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatordIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmPulsanti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPulsanti.ResumeLayout(False)
    Me.fmPulsanti.PerformLayout()
    CType(Me.ckRigConfermate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckImpConfermati.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmClienti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmClienti.ResumeLayout(False)
    Me.fmClienti.PerformLayout()
    CType(Me.edContoFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContoIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optContoAlcuni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmArticoli, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmArticoli.ResumeLayout(False)
    Me.fmArticoli.PerformLayout()
    CType(Me.edCodartFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optArticoloAlcuni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodartIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optArticoloTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optArticoloSelezione.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDatord, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDatord.ResumeLayout(False)
    Me.fmDatord.PerformLayout()
    CType(Me.edDatordFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optDatordAlcuni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optDatordTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmDatcons, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDatcons.ResumeLayout(False)
    Me.fmDatcons.PerformLayout()
    CType(Me.edDatconsFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optDatconsAlcuni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optDatconsTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatconsIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmNumord, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmNumord.ResumeLayout(False)
    Me.fmNumord.PerformLayout()
    CType(Me.edRigaFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerieFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumordFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnoFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRigaIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerieIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edNumordIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnoIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optNumordAlcuni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optNumordTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTipoBF, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTipoBF.ResumeLayout(False)
    Me.fmTipoBF.PerformLayout()
    CType(Me.edTipoBFFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTipoBFIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optTipoBFAlcuni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optTipoBFTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmMagaz, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmMagaz.ResumeLayout(False)
    Me.fmMagaz.PerformLayout()
    CType(Me.edMagazFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMagazIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optMagazAlcuni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optMagazTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmCodiceStagione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCodiceStagione.ResumeLayout(False)
    Me.fmCodiceStagione.PerformLayout()
    CType(Me.edCodiceStagioneFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodiceStagioneIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optCodiceStagioneAlcuni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optCodiceStagioneTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmAnnoStagione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmAnnoStagione.ResumeLayout(False)
    Me.fmAnnoStagione.PerformLayout()
    CType(Me.edAnnoStagioneFin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnoStagioneIni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optAnnoStagioneAlcuni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optAnnoStagioneTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRMDBFXOR_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      'nascondo le voci di menu non gestite
      If CBool(oCleGnof.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCP) = False And _
         CBool(oCleGnof.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO) = False Then
        fmAnnoStagione.Enabled = False
        fmCodiceStagione.Enabled = False
      Else
        bModTCO = True
      End If

      GctlApplicaDefaultValue()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMDBFXOR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '----------------------------------------------------------------------------------
      InitControls()
      '----------------------------------------------------------------------------------
      If bInit = True Then
        Select Case nOpzClienti
          Case 0 : optContoTutti.Checked = True
          Case 2
            optContoAlcuni.Checked = True
            edContoIni.Text = lClientiDal.ToString
            edContoFin.Text = lClientiAl.ToString
        End Select
        Select Case nOpzArticoli
          Case 0 : optArticoloTutti.Checked = True
          Case 2
            optArticoloAlcuni.Checked = True
            edCodartIni.Text = strArticoliDal
            edCodartFin.Text = strArticoliAl
        End Select
        Select Case nOpzDataEmiss
          Case 0 : optDatordTutti.Checked = True
          Case 1
            optDatordAlcuni.Checked = True
            edDatordIni.Text = dtDataEmissDal.ToString
            edDatordFin.Text = dtDataEmissAl.ToString
        End Select
        Select Case nOpzDataCons
          Case 0 : optDatconsTutti.Checked = True
          Case 1
            optDatconsAlcuni.Checked = True
            edDatconsIni.Text = dtDataConsDal.ToString
            edDatconsFin.Text = dtDataConsAl.ToString
        End Select
        Select Case nOpzNumeroOrdini
          Case 0 : optNumordTutti.Checked = True
          Case 1
            optNumordAlcuni.Checked = True
            edAnnoIni.Text = nAnnoDal.ToString
            edAnnoFin.Text = nAnnoAl.ToString
            edSerieIni.Text = strSerieDal.ToString
            edSerieFin.Text = strSerieAl.ToString
            edNumordIni.Text = lNumeroDal.ToString
            edNumordFin.Text = lNumeroAl.ToString
            edRigaIni.Text = lRigaDal.ToString
            edRigaFin.Text = lRigaAl.ToString
        End Select
        Select Case nOpzTpbf
          Case 0 : optTipoBFTutti.Checked = True
          Case 1
            optTipoBFAlcuni.Checked = True
            edTipoBFIni.Text = nTpbfDal.ToString
            edTipoBFFin.Text = nTpbfAl.ToString
        End Select
        Select Case nOpzMagaz
          Case 0 : optMagazTutti.Checked = True
          Case 1
            optMagazAlcuni.Checked = True
            edMagazIni.Text = nMagazDal.ToString
            edMagazFin.Text = nMagazAl.ToString
        End Select
        ckImpConfermati.Checked = bOrdiniConfermati
        ckRigConfermate.Checked = bRigheConfermate
      End If
      '----------------------------------------------------------------------------------
      If bInit = False Then bInit = True
      '----------------------------------------------------------------------------------
      GctlSetRoules()
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMDBFXOR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      '----------------------------------------------------------------------------------
      '--- Gestione dello zoom:
      '--- eseguo la Zoom, tanto se per il controllo non deve venir eseguito 
      '--- uno zoom particolare, la routine non fa nulla e viene processato 
      '--- lo zoom standard del controllo, settato con la NTSSetParamZoom
      '----------------------------------------------------------------------------------
      If (e.KeyCode = Keys.F5) And (e.Control = False) And (e.Alt = False) And (e.Shift = False) Then
        Zoom()
        e.Handled = True
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "TextBox"
  Public Overridable Sub edContoIni_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edContoIni.Validated
    Try
      If edContoIni.Enabled = False Then Return
      If (edContoIni.Text <> "0") And _
         ((edContoFin.Text = "0") Or (edContoFin.Text = "".PadLeft(9, "9"c))) Then
        edContoFin.Text = edContoIni.Text
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edCodartIni_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodartIni.Validated
    Try
      If edCodartIni.Enabled = False Then Return
      If edCodartIni.Text.Trim = "" Then
        edCodartIni.Text = "".PadLeft(CLN__STD.CodartMaxLen)
      Else
        edCodartIni.Text = edCodartIni.Text.ToUpper
      End If
      If (edCodartIni.Text.Trim <> "") And _
         ((edCodartFin.Text.Trim = "") Or (edCodartFin.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c))) Then
        edCodartFin.Text = edCodartIni.Text
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edCodartFin_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodartFin.Validated
    Try
      If edCodartFin.Enabled = False Then Return
      If (edCodartFin.Text.Trim <> "") And (edCodartFin.Text <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
        edCodartFin.Text = edCodartFin.Text.ToUpper
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edSerieIni_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerieIni.Validated
    Dim strTmp As String = ""
    Try
      If edSerieIni.Enabled = False Then Return

      strTmp = TestSerieMaxLen(edSerieIni.Text, False)
      If strTmp <> edSerieIni.Text Then edSerieIni.Text = strTmp

      If edSerieIni.Text.Trim = "" Then edSerieIni.Text = " "
      edSerieIni.Text = edSerieIni.Text.ToUpper
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edSerieFin_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerieFin.Validated
    Dim strTmp As String = ""
    Try
      If edSerieFin.Enabled = False Then Return

      strTmp = TestSerieMaxLen(edSerieFin.Text, False)
      If strTmp <> edSerieFin.Text Then edSerieFin.Text = strTmp

      If edSerieFin.Text = "" Then edSerieFin.Text = "z".PadRight(CLN__STD.SerieMaxLen, "z"c)
      edSerieFin.Text = edSerieFin.Text.ToUpper
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Pulsanti"
  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      If Not CheckSelection() Then Return
      '----------------------------------------------------------------------------------
      '--- Espone le variabili all'esterno e costruisce la WHERE
      '----------------------------------------------------------------------------------
      strSQLWHERE = ""
      strSQLWHEREODBC = ""
      strSQLWHEREIP = ""
      strSQLWHEREIPODBC = ""
      '----------------------------------------------------------------------------------
      If optContoTutti.Checked = True Then nOpzClienti = 0
      If optContoSelezione.Checked = True Then nOpzClienti = 1
      If optContoAlcuni.Checked = True Then
        nOpzClienti = 2
        lClientiDal = NTSCInt(edContoIni.Text)
        lClientiAl = NTSCInt(edContoFin.Text)
        strSQLWHERE += " AND td_conto BETWEEN " & lClientiDal & " AND " & lClientiAl
        strSQLWHEREIP += " AND td_conto BETWEEN " & lClientiDal & " AND " & lClientiAl
      End If
      '----------------------------------------------------------------------------------
      If optArticoloTutti.Checked = True Then nOpzArticoli = 0
      If optArticoloSelezione.Checked = True Then nOpzArticoli = 1
      If optArticoloAlcuni.Checked = True Then
        nOpzArticoli = 2
        strArticoliDal = edCodartIni.Text
        strArticoliAl = edCodartFin.Text
        strSQLWHERE += " AND mo_codart BETWEEN " & CStrSQL(strArticoliDal) & " AND " & CStrSQL(strArticoliAl)
        strSQLWHEREIP += " AND T1.mo_codart BETWEEN " & CStrSQL(strArticoliDal) & " AND " & CStrSQL(strArticoliAl)
      End If
      '----------------------------------------------------------------------------------
      strSQLWHEREODBC = strSQLWHERE
      strSQLWHEREIPODBC = strSQLWHEREIP
      '----------------------------------------------------------------------------------
      If optDatordTutti.Checked = True Then
        nOpzDataEmiss = 0
      Else
        nOpzDataEmiss = 1
        dtDataEmissDal = CDate(edDatordIni.Text)
        dtDataEmissAl = CDate(edDatordFin.Text)

        strSQLWHERE += " AND td_datord BETWEEN " & CDataSQL(dtDataEmissDal) & " AND " & CDataSQL(dtDataEmissAl)
        strSQLWHEREODBC += " AND td_datord BETWEEN " & CDataSQL(dtDataEmissDal) & " AND " & CDataSQL(dtDataEmissAl)
        strSQLWHEREIP += " AND td_datord BETWEEN " & CDataSQL(dtDataEmissDal) & " AND " & CDataSQL(dtDataEmissAl)
        strSQLWHEREIPODBC += " AND td_datord BETWEEN " & CDataSQL(dtDataEmissDal) & " AND " & CDataSQL(dtDataEmissAl)
      End If
      '----------------------------------------------------------------------------------
      If optDatconsTutti.Checked = True Then
        nOpzDataCons = 0
      Else
        nOpzDataCons = 1
        dtDataConsDal = CDate(edDatconsIni.Text)
        dtDataConsAl = CDate(edDatconsFin.Text)

        strSQLWHERE += " AND mo_datcons BETWEEN " & CDataSQL(dtDataConsDal) & " AND " & CDataSQL(dtDataConsAl)
        strSQLWHEREODBC += " AND mo_datcons BETWEEN " & CDataSQL(dtDataConsDal) & " AND " & CDataSQL(dtDataConsAl)
        strSQLWHEREIP += " AND T1.mo_datcons BETWEEN " & CDataSQL(dtDataConsDal) & " AND " & CDataSQL(dtDataConsAl)
        strSQLWHEREIPODBC += " AND T1.mo_datcons BETWEEN " & CDataSQL(dtDataConsDal) & " AND " & CDataSQL(dtDataConsAl)
      End If
      '----------------------------------------------------------------------------------
      If optNumordTutti.Checked = True Then
        nOpzNumeroOrdini = 0
      Else
        nOpzNumeroOrdini = 1
        nAnnoDal = NTSCInt(edAnnoIni.Text)
        nAnnoAl = NTSCInt(edAnnoFin.Text)
        strSerieDal = edSerieIni.Text
        strSerieAl = edSerieFin.Text
        lNumeroDal = NTSCInt(edNumordIni.Text)
        lNumeroAl = NTSCInt(edNumordFin.Text)
        lRigaDal = NTSCInt(edRigaIni.Text)
        lRigaAl = NTSCInt(edRigaFin.Text)
        strSQLWHERE += " AND movord.mo_anno BETWEEN " & nAnnoDal & " AND " & nAnnoAl & _
          " AND movord.mo_serie BETWEEN " & CStrSQL(strSerieDal) & " AND " & CStrSQL(strSerieAl) & _
          " AND movord.mo_numord BETWEEN " & lNumeroDal & " AND " & lNumeroAl & _
          " AND movord.mo_riga BETWEEN " & lRigaDal & " AND " & lRigaAl
        strSQLWHEREODBC += " AND movord.mo_anno BETWEEN " & nAnnoDal & " AND " & nAnnoAl & _
          " AND movord.mo_serie BETWEEN " & CStrSQL(strSerieDal) & " AND " & CStrSQL(strSerieAl) & _
          " AND movord.mo_numord BETWEEN " & lNumeroDal & " AND " & lNumeroAl & _
          " AND movord.mo_riga BETWEEN " & lRigaDal & " AND " & lRigaAl
        strSQLWHEREIP += " AND T1.mo_anno BETWEEN " & nAnnoDal & " AND " & nAnnoAl & _
          " AND T1.mo_serie BETWEEN " & CStrSQL(strSerieDal) & " AND " & CStrSQL(strSerieAl) & _
          " AND T1.mo_numord BETWEEN " & lNumeroDal & " AND " & lNumeroAl & _
          " AND T1.mo_riga BETWEEN " & lRigaDal & " AND " & lRigaAl
        strSQLWHEREIPODBC += " AND T1.mo_anno BETWEEN " & nAnnoDal & " AND " & nAnnoAl & _
          " AND T1.mo_serie BETWEEN " & CStrSQL(strSerieDal) & " AND " & CStrSQL(strSerieAl) & _
          " AND T1.mo_numord BETWEEN " & lNumeroDal & " AND " & lNumeroAl & _
          " AND T1.mo_riga BETWEEN " & lRigaDal & " AND " & lRigaAl
      End If
      '----------------------------------------------------------------------------------
      If optTipoBFTutti.Checked = True Then
        nOpzTpbf = 0
      Else
        nOpzTpbf = 1
        nTpbfDal = NTSCInt(edTipoBFIni.Text)
        nTpbfAl = NTSCInt(edTipoBFFin.Text)
        strSQLWHERE += " AND td_tipobf BETWEEN " & nTpbfDal & " AND " & nTpbfAl
        strSQLWHEREODBC += " AND td_tipobf BETWEEN " & nTpbfDal & " AND " & nTpbfAl
        strSQLWHEREIP += " AND td_tipobf BETWEEN " & nTpbfDal & " AND " & nTpbfAl
        strSQLWHEREIPODBC += " AND td_tipobf BETWEEN " & nTpbfDal & " AND " & nTpbfAl
      End If
      '----------------------------------------------------------------------------------
      If optMagazTutti.Checked = True Then
        nOpzMagaz = 0
      Else
        nOpzMagaz = 1
        nMagazDal = NTSCInt(edMagazIni.Text)
        nMagazAl = NTSCInt(edMagazFin.Text)
        strSQLWHERE += " AND mo_magaz BETWEEN " & nMagazDal & " AND " & nMagazAl
        strSQLWHEREODBC += " AND mo_magaz BETWEEN " & nMagazDal & " AND " & nMagazAl
        strSQLWHEREIP += " AND T1.mo_magaz BETWEEN " & nMagazDal & " AND " & nMagazAl
        strSQLWHEREIPODBC += " AND T1.mo_magaz BETWEEN " & nMagazDal & " AND " & nMagazAl
      End If
      '----------------------------------------------------------------------------------
      If ckImpConfermati.Checked = True Then
        bOrdiniConfermati = True
        strSQLWHERE += " AND td_confermato = 'S'"
        strSQLWHEREODBC += " AND td_confermato = 'S'"
        strSQLWHEREIP += " AND td_confermato = 'S'"
        strSQLWHEREIPODBC += " AND td_confermato = 'S'"
      Else
        bOrdiniConfermati = False
      End If
      '----------------------------------------------------------------------------------
      If ckRigConfermate.Checked = True Then
        bRigheConfermate = True
        strSQLWHERE += " AND mo_confermato = 'S'"
        strSQLWHEREODBC += " AND mo_confermato = 'S'"
        strSQLWHEREIP += " AND mo_confermato = 'S'"
        strSQLWHEREIPODBC += " AND mo_confermato = 'S'"
      Else
        bRigheConfermate = False
      End If
      '----------------------------------------------------------------------------------
      If optAnnoStagioneAlcuni.Checked = True Then
        nAnnoStagioneIni = NTSCInt(edAnnoStagioneIni.Text)
        nAnnoStagioneFin = NTSCInt(edAnnoStagioneFin.Text)

        strSQLWHERE += " AND td_annotco BETWEEN " & nAnnoStagioneIni & " AND " & nAnnoStagioneFin
        strSQLWHEREODBC += " AND td_annotco BETWEEN " & nAnnoStagioneIni & " AND " & nAnnoStagioneFin
        strSQLWHEREIP += " AND td_annotco BETWEEN " & nAnnoStagioneIni & " AND " & nAnnoStagioneFin
        strSQLWHEREIPODBC += " AND td_annotco BETWEEN " & nAnnoStagioneIni & " AND " & nAnnoStagioneFin
      End If
      '----------------------------------------------------------------------------------
      If optCodiceStagioneAlcuni.Checked = True Then
        nCodiceStagioneIni = NTSCInt(edCodiceStagioneIni.Text)
        nCodiceStagioneFin = NTSCInt(edCodiceStagioneFin.Text)

        strSQLWHERE += " AND td_codstag BETWEEN " & nCodiceStagioneIni & " AND " & nCodiceStagioneFin
        strSQLWHEREODBC += " AND td_codstag BETWEEN " & nCodiceStagioneIni & " AND " & nCodiceStagioneFin
        strSQLWHEREIP += " AND td_codstag BETWEEN " & nCodiceStagioneIni & " AND " & nCodiceStagioneFin
        strSQLWHEREIPODBC += " AND td_codstag BETWEEN " & nCodiceStagioneIni & " AND " & nCodiceStagioneFin
      End If
      '----------------------------------------------------------------------------------
      If strSQLWHERE = "" Then
        bAll = True
      Else
        '--- Toglie il primo AND
        bAll = False
        strSQLWHERE = Mid(strSQLWHERE, 5)
      End If
      '--- Toglie il primo AND
      If strSQLWHEREODBC <> "" Then strSQLWHEREODBC = Mid(strSQLWHEREODBC, 5)
      '--- Toglie il primo AND
      If strSQLWHEREIP <> "" Then strSQLWHEREIP = Mid(strSQLWHEREIP, 5)
      '--- Toglie il primo AND
      If strSQLWHEREIPODBC <> "" Then strSQLWHEREIPODBC = Mid(strSQLWHEREIPODBC, 5)
      '----------------------------------------------------------------------------------
      If strSQLWHERE <> "" Then strSQLWHERE += " AND movord.codditt = " & CStrSQL(DittaCorrente)
      If strSQLWHEREODBC <> "" Then strSQLWHEREODBC += " AND movord.codditt = " & CStrSQL(DittaCorrente)
      If strSQLWHEREIP <> "" Then strSQLWHEREIP += " AND T1.codditt = " & CStrSQL(DittaCorrente)
      If strSQLWHEREIPODBC <> "" Then strSQLWHEREIPODBC += " AND T1.codditt = " & CStrSQL(DittaCorrente)
      '----------------------------------------------------------------------------------
      bCancel = False
      '----------------------------------------------------------------------------------
      Me.Close()
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      bCancel = True
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "OptionButton"
  '--- CLIENTI/FORNITORI
  Public Overridable Sub optContoTutti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optContoTutti.CheckedChanged
    Try
      If optContoTutti.Checked = True Then
        cmdContoSelezione.Enabled = False
        edContoIni.Enabled = False
        lbContoFin.Enabled = False
        edContoFin.Enabled = False
        edContoIni.Text = "0"
        edContoFin.Text = "".PadLeft(9, "9"c)
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub optContoSelezione_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optContoSelezione.CheckedChanged
    Try
      If optContoSelezione.Checked = True Then
        GctlSetVisEnab(cmdContoSelezione, False)
        edContoIni.Enabled = False
        lbContoFin.Enabled = False
        edContoFin.Enabled = False
        edContoIni.Text = "0"
        edContoFin.Text = "".PadLeft(9, "9"c)
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub optContoAlcuni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optContoAlcuni.CheckedChanged
    Try
      If optContoAlcuni.Checked = True Then
        edContoIni.Text = "0"
        edContoFin.Text = "".PadLeft(9, "9"c)
        cmdContoSelezione.Enabled = False
        GctlSetVisEnab(edContoIni, False)
        GctlSetVisEnab(lbContoFin, False)
        GctlSetVisEnab(edContoFin, False)
        edContoIni.Focus()
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  '--- ARTICOLI
  Public Overridable Sub optArticoloTutti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optArticoloTutti.CheckedChanged
    Try
      If optArticoloTutti.Checked = True Then
        cmdArticoloSelezione.Enabled = False
        edCodartIni.Enabled = False
        lbCodartFin.Enabled = False
        edCodartFin.Enabled = False
        edCodartIni.Text = "".PadLeft(CLN__STD.CodartMaxLen)
        edCodartFin.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub optArticoloSelezione_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optArticoloSelezione.CheckedChanged
    Try
      If optArticoloSelezione.Checked = True Then
        GctlSetVisEnab(cmdArticoloSelezione, False)
        edCodartIni.Enabled = False
        lbCodartFin.Enabled = False
        edCodartFin.Enabled = False
        edCodartIni.Text = "".PadLeft(CLN__STD.CodartMaxLen)
        edCodartFin.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub optArticoloAlcuni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optArticoloAlcuni.CheckedChanged
    Try
      If optArticoloAlcuni.Checked = True Then
        edCodartIni.Text = "".PadLeft(CLN__STD.CodartMaxLen)
        edCodartFin.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
        cmdArticoloSelezione.Enabled = False
        GctlSetVisEnab(edCodartIni, False)
        GctlSetVisEnab(lbCodartFin, False)
        GctlSetVisEnab(edCodartFin, False)
        edCodartIni.Focus()
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  '--- DATA DI EMISSIONE ORDINI/IMPEGNI
  Public Overridable Sub optDatordTutti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDatordTutti.CheckedChanged
    Try
      If optDatordTutti.Checked = True Then
        edDatordIni.Enabled = False
        lbDatordFin.Enabled = False
        edDatordFin.Enabled = False
        edDatordIni.Text = IntSetDate("01/01/1900")
        edDatordFin.Text = IntSetDate("31/12/2099")
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub optDatordAlcuni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDatordAlcuni.CheckedChanged
    Try
      If optDatordAlcuni.Checked = True Then
        edDatordIni.Text = IntSetDate("01/01/1900")
        edDatordFin.Text = IntSetDate("31/12/2099")
        GctlSetVisEnab(edDatordIni, False)
        GctlSetVisEnab(lbDatordFin, False)
        GctlSetVisEnab(edDatordFin, False)
        edDatordIni.Focus()
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  '--- DATA DI CONSEGNA RIGHE
  Public Overridable Sub optDatconsTutti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDatconsTutti.CheckedChanged
    Try
      If optDatconsTutti.Checked = True Then
        edDatconsIni.Enabled = False
        lbDatconsFin.Enabled = False
        edDatconsFin.Enabled = False
        edDatconsIni.Text = IntSetDate("01/01/1900")
        edDatconsFin.Text = IntSetDate("31/12/2099")
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub optDatconsAlcuni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDatconsAlcuni.CheckedChanged
    Try
      If optDatconsAlcuni.Checked = True Then
        edDatconsIni.Text = IntSetDate("01/01/1900")
        edDatconsFin.Text = IntSetDate("31/12/2099")
        GctlSetVisEnab(edDatconsIni, False)
        GctlSetVisEnab(lbDatconsFin, False)
        GctlSetVisEnab(edDatconsFin, False)
        edDatconsIni.Focus()
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  '--- TIPO BOLLA/FATTURA
  Public Overridable Sub optTipoBFTutti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTipoBFTutti.CheckedChanged
    Try
      If optTipoBFTutti.Checked = True Then
        edTipoBFIni.Enabled = False
        lbTipoBFFin.Enabled = False
        edTipoBFFin.Enabled = False
        edTipoBFIni.Text = "0"
        edTipoBFFin.Text = "".PadLeft(4, "9"c)
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub optTipoBFAlcuni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTipoBFAlcuni.CheckedChanged
    Try
      If optTipoBFAlcuni.Checked = True Then
        edTipoBFIni.Text = "0"
        edTipoBFFin.Text = "".PadLeft(4, "9"c)
        GctlSetVisEnab(edTipoBFIni, False)
        GctlSetVisEnab(lbTipoBFFin, False)
        GctlSetVisEnab(edTipoBFFin, False)
        edTipoBFIni.Focus()
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  '--- MAGAZZINI
  Public Overridable Sub optMagazTutti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optMagazTutti.CheckedChanged
    Try
      If optMagazTutti.Checked = True Then
        edMagazIni.Enabled = False
        lbMagazFin.Enabled = False
        edMagazFin.Enabled = False
        edMagazIni.Text = "0"
        edMagazFin.Text = "".PadLeft(4, "9"c)
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub optMagazAlcuni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optMagazAlcuni.CheckedChanged
    Try
      If optMagazAlcuni.Checked = True Then
        edMagazIni.Text = "0"
        edMagazFin.Text = "".PadLeft(4, "9"c)
        GctlSetVisEnab(edMagazIni, False)
        GctlSetVisEnab(lbMagazFin, False)
        GctlSetVisEnab(edMagazFin, False)
        edMagazIni.Focus()
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  '--- NUMERO ORDINI/IMPEGNI
  Public Overridable Sub optNumordTutti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optNumordTutti.CheckedChanged
    Try
      If optNumordTutti.Checked = True Then
        lbAnnoFin.Enabled = False
        lbSerieIni.Enabled = False
        lbSerieFin.Enabled = False
        lbNumordIni.Enabled = False
        lbNumordFin.Enabled = False
        lbRigaIni.Enabled = False
        lbRigaFin.Enabled = False
        edAnnoIni.Enabled = False
        edAnnoFin.Enabled = False
        edSerieIni.Enabled = False
        edSerieFin.Enabled = False
        edNumordIni.Enabled = False
        edNumordFin.Enabled = False
        edRigaIni.Enabled = False
        edRigaFin.Enabled = False
        edAnnoIni.Text = "1900"
        edAnnoFin.Text = "2099"
        edSerieIni.Text = " "
        edSerieFin.Text = "Z".PadRight(CLN__STD.SerieMaxLen, "Z"c)
        edNumordIni.Text = "0"
        edNumordFin.Text = "".PadLeft(9, "9"c)
        edRigaIni.Text = "0"
        edRigaFin.Text = "".PadLeft(9, "9"c)
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub optNumordAlcuni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optNumordAlcuni.CheckedChanged
    Try
      If optNumordAlcuni.Checked = True Then
        edAnnoIni.Text = "1900"
        edAnnoFin.Text = "2099"
        edSerieIni.Text = " "
        edSerieFin.Text = "Z".PadRight(CLN__STD.SerieMaxLen, "Z"c)
        edNumordIni.Text = "0"
        edNumordFin.Text = "".PadLeft(9, "9"c)
        edRigaIni.Text = "0"
        edRigaFin.Text = "".PadLeft(9, "9"c)
        GctlSetVisEnab(lbAnnoFin, False)
        GctlSetVisEnab(lbSerieIni, False)
        GctlSetVisEnab(lbSerieFin, False)
        GctlSetVisEnab(lbNumordIni, False)
        GctlSetVisEnab(lbNumordFin, False)
        GctlSetVisEnab(lbRigaIni, False)
        GctlSetVisEnab(lbRigaFin, False)
        GctlSetVisEnab(edAnnoIni, False)
        GctlSetVisEnab(edAnnoFin, False)
        GctlSetVisEnab(edSerieIni, False)
        GctlSetVisEnab(edSerieFin, False)
        GctlSetVisEnab(edNumordIni, False)
        GctlSetVisEnab(edNumordFin, False)
        GctlSetVisEnab(edRigaIni, False)
        GctlSetVisEnab(edRigaFin, False)
        edAnnoIni.Focus()
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  '--- CODICE STAGIONE TAGLIE E COLORI
  Public Overridable Sub optCodiceStagioneTutti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCodiceStagioneTutti.CheckedChanged
    Try
      If optCodiceStagioneTutti.Checked Then
        edCodiceStagioneIni.Enabled = False
        edCodiceStagioneFin.Enabled = False
        edCodiceStagioneIni.Text = "0"
        edCodiceStagioneFin.Text = "".PadLeft(4, "9"c)
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub optCodiceStagioneAlcuni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCodiceStagioneAlcuni.CheckedChanged
    Try
      If optCodiceStagioneAlcuni.Checked Then
        GctlSetVisEnab(edCodiceStagioneIni, False)
        GctlSetVisEnab(edCodiceStagioneFin, False)
        edCodiceStagioneIni.Text = "0"
        edCodiceStagioneFin.Text = "".PadLeft(4, "9"c)
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  '--- ANNO STAGIONE TAGLIE E COLORI
  Public Overridable Sub optAnnoStagioneTutti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAnnoStagioneTutti.CheckedChanged
    Try
      If optAnnoStagioneTutti.Checked = True Then
        edAnnoStagioneIni.Enabled = False
        edAnnoStagioneFin.Enabled = False
        edAnnoStagioneIni.Text = "1900"
        edAnnoStagioneFin.Text = "2099"
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub optAnnoStagioneAlcuni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAnnoStagioneAlcuni.CheckedChanged
    Try
      If optAnnoStagioneAlcuni.Checked = True Then
        GctlSetVisEnab(edAnnoStagioneIni, False)
        GctlSetVisEnab(edAnnoStagioneFin, False)
        edAnnoStagioneIni.Text = "1900"
        edAnnoStagioneFin.Text = "2099"
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Funzioni"
  Public Overridable Function CheckSelection() As Boolean
    Try
      '----------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '----------------------------------------------------------------------------------
      If optContoAlcuni.Checked = True Then
        If NTSCInt(edContoIni.Text) > NTSCInt(edContoFin.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128789226025782212, "Attenzione!" & vbCrLf & _
            "Il codice conto iniziale non può essere superiore a quello finale."))
          edContoIni.Text = "0"
          edContoFin.Text = "".PadLeft(9, "9"c)
          edContoIni.Focus()
          Return False
        End If
      End If
      '----------------------------------------------------------------------------------      
      If optArticoloAlcuni.Checked = True Then
        If edCodartIni.Text > edCodartFin.Text Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128789226064063462, "Attenzione!" & vbCrLf & _
            "Il codice articolo iniziale non può essere superiore a quello finale."))
          edCodartIni.Text = "".PadLeft(CLN__STD.CodartMaxLen)
          edCodartFin.Text = "".PadLeft(CLN__STD.CodartMaxLen, "z"c)
          edCodartIni.Focus()
          Return False
        End If
      End If
      '----------------------------------------------------------------------------------      
      If optDatordAlcuni.Checked = True Then
        If NTSCDate(edDatordIni.Text) > NTSCDate(edDatordFin.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128789226108125962, "Attenzione!" & vbCrLf & _
            "La data di emissione ordine/impegno iniziale non può essere superiore a quella finale."))
          edDatordIni.Text = IntSetDate("01/01/1900")
          edDatordFin.Text = IntSetDate("31/12/2099")
          edDatordIni.Focus()
          Return False
        End If
      End If
      '----------------------------------------------------------------------------------      
      If optDatconsAlcuni.Checked = True Then
        If NTSCDate(edDatconsIni.Text) > NTSCDate(edDatconsFin.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128789226146563462, "Attenzione!" & vbCrLf & _
            "La data di consegna righe iniziale non può essere superiore a quella finale."))
          edDatconsIni.Text = IntSetDate("01/01/1900")
          edDatconsFin.Text = IntSetDate("31/12/2099")
          edDatconsIni.Focus()
          Return False
        End If
      End If
      '----------------------------------------------------------------------------------
      If optTipoBFAlcuni.Checked = True Then
        If NTSCInt(edTipoBFIni.Text) > NTSCInt(edTipoBFFin.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128789226179219712, "Attenzione!" & vbCrLf & _
            "Il tipo bolla/fattura iniziale non può essere superiore a quello finale."))
          edTipoBFIni.Text = "0"
          edTipoBFFin.Text = "".PadLeft(4, "9"c)
          edTipoBFIni.Focus()
          Return False
        End If
      End If
      '----------------------------------------------------------------------------------
      If optMagazAlcuni.Checked = True Then
        If NTSCInt(edMagazIni.Text) > NTSCInt(edMagazFin.Text) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128789226210938462, "Attenzione!" & vbCrLf & _
            "Il magazzino iniziale non può essere superiore a quello finale."))
          edMagazIni.Text = "0"
          edMagazFin.Text = "".PadLeft(4, "9"c)
          edMagazIni.Focus()
          Return False
        End If
      End If
      '----------------------------------------------------------------------------------
      If (NTSCInt(edAnnoIni.Text) > NTSCInt(edAnnoFin.Text)) Or _
         ((NTSCInt(edAnnoIni.Text) = NTSCInt(edAnnoFin.Text)) And _
         (edSerieIni.Text > edSerieFin.Text)) Or _
         ((NTSCInt(edAnnoIni.Text) = NTSCInt(edAnnoFin.Text)) And _
         (edSerieIni.Text = edSerieFin.Text) And _
         (NTSCInt(edNumordIni.Text) > NTSCInt(edNumordFin.Text))) Or _
         ((NTSCInt(edAnnoIni.Text) = NTSCInt(edAnnoFin.Text)) And _
         (edSerieIni.Text = edSerieFin.Text) And _
         (NTSCInt(edNumordIni.Text) = NTSCInt(edNumordFin.Text)) And _
         (NTSCInt(edRigaIni.Text) > NTSCInt(edRigaFin.Text))) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128789226242188462, "Attenzione!" & vbCrLf & _
          "La combinazione 'anno/serie/numero/riga' iniziale supera quello finale."))
        edAnnoIni.Focus()
        Return False
      End If
      If (NTSCInt(edAnnoStagioneIni.Text) > NTSCInt(edAnnoStagioneFin.Text)) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130247411288542570, "Attenzione!" & vbCrLf & _
              "L'anno stagione iniziale supera quello finale."))
        edAnnoStagioneIni.Text = "0"
        edAnnoStagioneFin.Text = "2099"
        edAnnoStagioneIni.Focus()
        Return False
      End If
      If (NTSCInt(edCodiceStagioneIni.Text) > NTSCInt(edCodiceStagioneFin.Text)) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130247418827593567, "Attenzione!" & vbCrLf & _
              "Il codice stagione iniziale supera quello finale."))
        edCodiceStagioneIni.Text = "0"
        edCodiceStagioneFin.Text = "".PadLeft(4, "9"c)
        edCodiceStagioneIni.Focus()
        Return False
      End If
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
#End Region

#Region "Routines"
  Public Overridable Sub Zoom()
    Dim ctlLastControl As Control

    Try
      '----------------------------------------------------------------------------------
      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return
      '----------------------------------------------------------------------------------
      NTSCallStandardZoom()
      '----------------------------------------------------------------------------------
      ctlLastControl.Focus()
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region
End Class