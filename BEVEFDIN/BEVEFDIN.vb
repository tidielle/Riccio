Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEVEFDIN
  Inherits CLE__BASN

  Private Moduli_P As Integer = CLN__STD.bsModMG Or CLN__STD.bsModVE
  Private ModuliExt_P As Integer = CLN__STD.bsModExtMGE
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property

  Public oCldFdin As CLDVEFDIN = Nothing
  Public oClePnfa As CLEVEPNFA = Nothing

  Public strActLog As String = ""
  Public strActLogProg As String = ""
  Public strActLogNomOggLog As String = ""
  Public strActLogDesLog As String = ""

  Public bHasChanges As Boolean
  Public dsShared As DataSet
  Public bNonValidare As Boolean = False
  Public bInUnload As Boolean = False
  Public bInCalcolaTotali As Boolean = False
  Public dttET As DataTable             'testmag del documento in analisi
  Public dttETC As DataTable            'testmagc
  Public strLastTiporkRegistroDoc As String = ""
  Public bDocNonModificabile As Boolean = False
  Public bNew As Boolean = True
  Public dttEt_conto As New DataTable       'contiene anagra del cliente/fornitore
  Public bModifCastScad As Boolean = False
  Public bForzaValuteCambiDiv As Boolean = False
  Const FdinModifCastScad As Integer = 1
  Public bModPM As Boolean = False
  Public bModTCO As Boolean = False
  Public bModRSM As Boolean = False
  Public bModDII As Boolean = False
  Public lContoCF As Integer = 0
  Public bProgrCambiato As Boolean = False
  Public oCleBoll As CLEVEBOLL = Nothing
  Public bVefadi As Boolean = False   'se true sono stato chiamato da BEVEFADI: non devo fare alcuni test

  Public bCalcolaScadUsaSempreDatdoc As Boolean = False

  Public nRegiva As Integer = 0       'Numero di registro attribuito automaticamente
  Public lNumprot As Integer = 0      'Numero di protocollo attribuito automaticamente
  Public strAlfpro As String = " "     'Serie protocollo attribuito automaticamente

  'opzioni di registro
  Public bProteggiDocContab As Boolean = True
  Public bGestPVR As Boolean = False
  Public strAutoNumProt As String = ""
  Public bUsaKeyMag As Boolean = False
  Public strNumcliPVR As String = ""
  Public strPrefixFreeStringPVR As String = ""
  Public bNoUpdateTipobf As Boolean = False
  Public nNumMaxDDT As Integer = 200
  Public bOltre200 As Boolean = False
  Public bORTO_ChiamaBsjoboll As Boolean = False
  Public bReprintDoc As Boolean = False
  Public strNoteFattDiff As String = ""
  Public bRifBolleInNote As Boolean = False
  Public bIgnoraDestDiv As Boolean = False
  Public bIgnoraSegueFatturaSuDocRic As Boolean = False
  Public bRifBolleInNoteNoHeader As Boolean = False
  Public strCalcPesi As String = "N"
  Public bNoPesiSuRigheKitFittizie As Boolean = False
  Public nGestScostAcconti As Integer = 0
  Public dImpoScostAcconti As Decimal = 0
  Public bNonRiportaNote1DDT As Boolean = False
  Public bConsentiModifCodPagaSc As Boolean = False
  Public bConsentiModifFlspinc As Boolean = False
  Public strVisNoteConto As String = "N"
  Public bUsaContoFatt As Boolean = False
  Public strUsaContoFattDoc As String = "0"
  Public bGestAlert As Boolean = False
  Public bDeterminaBolliSuOperazEsenti As Boolean = False
  Public bDisabilitaCheckDateAnteriori As Boolean = False
  Public bInImportDDT As Boolean = False
  Public bCalcolaRagg As Boolean = False   'se = true il calcolo del documento avviene facendo una query sum(mm_quant) di tutti i ddt collegati alla fattura e e non come loot che somma tutte le singole righe
  Public bDaModificaDocumento As Boolean = False
  Public strControlloPIvaCodFis As String = ""
  Public bCollega_MG_DI As Boolean = False
  Public bAggiornaRiferimSoloSeVuoti As Boolean = False

  'Variabili per personalizzazioni TABPEVE
  Public nPeveIva15 As Integer
  Public nPeveIvaInc As Integer       'codice IVA per spese di trasporto valido fino alla data strPeveIvaIncFine
  Public strPeveIvaInc3Fine As String  'data di fine validità nPeveIvaInc3 (dopo questa data si usa nPeveIvaInc)
  Public nPeveIvaInc2 As Integer      'codice IVA per spese di trasporto valido da data strPeveIvaIncFine a data strPeveIvaInc2Fine
  Public strPeveIvaInc2Fine As String 'data di fine validità nPeveIvaInc2 (dopo questa data si usa nPeveIvaInc)
  Public nPeveIvaInc3 As Integer      'codice IVA per spese di trasporto valido da data strPeveIvaInc2Fine
  Public nPeveTipobf As Integer
  Public lPeveContro As Integer
  Public nPeveMagazz As Integer
  Public nPeveCaumag As Integer
  Public nPeveListin As Integer
  Public strPeveScorpo As String 'S o N
  Public nPeveCautra As Integer
  Public strPeveAcurad As String
  Public strPeveDatora As String 'S o N
  Public lConclpriv As Integer

  'Variabili per personalizzazioni TABPEAC
  Public nPeacIva15 As Integer
  Public nPeacIvainc As Integer       'codice IVA per spese di trasporto valido fino alla data strPeacIvaIncFine
  Public strPeacIvaInc3Fine As String  'data di fine validità nPeacIvaInc3 (dopo questa data si usa nPeacIvaInc2)
  Public nPeacIvaInc2 As Integer      'codice IVA per spese di trasporto valido da data strPeacIvaIncFine a data strPeacIvaInc2Fine
  Public strPeacIvaInc2Fine As String 'data di fine validità nPeacIvaInc2 (dopo questa data si usa nPeacIvaInc)
  Public nPeacIvaInc3 As Integer      'codice IVA per spese di trasporto valido da data strPeacIvaInc2Fine
  Public nPeacTipobf As Integer
  Public lPeacContro As Integer
  Public nPeacMagazz As Integer
  Public nPeacCaumag As Integer
  Public nPeacListin As Integer
  Public nPeacListinCStd As Integer
  Public nPeacScapro As Integer
  Public strPeacScorpo As String 'S o N

  Public bCallPnfa As Boolean = False   'se true, al salva in new/update di documenti gestiti da bnvepnfa (eccetto corrispettivi), dopo il salvataggio lancio pnfa per la contabilizzazione
  Public strCallPnfa As String = "N"    'se bCallPnfa = true, "S" = documenti sia attivi che passivi, A = solo ciclo attivo, P = solo ciclo passivo
  Public bCancellaRegCG As Boolean = False  'se true, prima di cancellare una fattura la cancella in CG (in modifica non serve, visto che verrà rilanciato bnvepnfa)
  Public nGLOBPeveTipobf As Integer
  Public nGLOBPeacTipobf As Integer

  '--- Personalizzazione ELEMAC
  Public bCastIvaCalcoloImposta As Boolean = False
  Public bCastContropVariaCastiva As Boolean = False

  'gestione di accconf (limitazioni funzionali su tipobf/causale di magaz/magaz di testata dipendneti da tipork, serie, opreatore,gruppo operat
  Public dttAccconf As New DataTable            'datatable contenente i blocchi su vis/mod/canc per l'utente/gruppo a cui appartiene l'utente (per CG blocco solo su causale contabile)
  Public nCodtpbfOpen As Integer = 0

  Public bRiproponiDataDoc As Boolean = False
  Public dtDataNewDoc As DateTime = NTSCDate(Now.ToShortDateString)

  Public bStornaDDTResoForn As Boolean = False

  Public bCodicePagamentoRicalcolato As Boolean = False
  Public strNomeProgrammaChiamante As String = ""

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDVEFDIN"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldFdin = CType(MyBase.ocldBase, CLDVEFDIN)
    oCldFdin.Init(oApp)

    Return True
  End Function

  Public Overridable Function LeggePevePeac() As Boolean
    Dim dttTmp As DataTable = New DataTable
    Try
      bCallPnfa = False
      strCallPnfa = "N"
      oCldFdin.ValCodiceDb(strDittaCorrente, strDittaCorrente, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        strCallPnfa = NTSCStr(dttTmp.Rows(0)!ac_contabft)
        bCallPnfa = CBool(IIf(strCallPnfa = "N", False, True))
      End If
      dttTmp.Clear()

      '---------------------------------
      'leggo tabpeve
      If Not oCldFdin.ValCodiceDb("1", strDittaCorrente, "TABPEVE", "N", "", dttTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101093750, "Tabella 'Personalizzazione Vendite' non compilata. Impostarla prima di proseguire")))
        Return False
      Else
        nPeveIva15 = NTSCInt(dttTmp.Rows(0)!tb_iva15)
        nPeveIvaInc = NTSCInt(dttTmp.Rows(0)!tb_ivainc)
        nPeveIvaInc2 = NTSCInt(dttTmp.Rows(0)!tb_ivainc2)
        nPeveIvaInc3 = NTSCInt(dttTmp.Rows(0)!tb_ivainc3)
        strPeveIvaInc3Fine = NTSCDate(dttTmp.Rows(0)!tb_datfinivainc3).ToShortDateString
        strPeveIvaInc2Fine = NTSCDate(dttTmp.Rows(0)!tb_datfinivainc2).ToShortDateString
        nPeveTipobf = NTSCInt(dttTmp.Rows(0)!tb_vtipobf)
        lPeveContro = NTSCInt(dttTmp.Rows(0)!tb_vcontro)
        nPeveMagazz = NTSCInt(dttTmp.Rows(0)!tb_vmagazz)
        nPeveCaumag = NTSCInt(dttTmp.Rows(0)!tb_vcaumag)
        nPeveListin = NTSCInt(dttTmp.Rows(0)!tb_vlistin)
        strPeveScorpo = dttTmp.Rows(0)!tb_vscorpo.ToString
        nPeveCautra = NTSCInt(dttTmp.Rows(0)!tb_vcautra)
        strPeveAcurad = dttTmp.Rows(0)!tb_vacurad.ToString
        strPeveDatora = dttTmp.Rows(0)!tb_vdatora.ToString
        lConclpriv = NTSCInt(dttTmp.Rows(0)!tb_conclpriv)
      End If

      '---------------------------------
      'leggo tabpeac
      If Not oCldFdin.ValCodiceDb("1", strDittaCorrente, "TABPEAC", "N", "", dttTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101250000, "Tabella 'Personalizzazione Acquisti' non compilata. Impostarla prima di proseguire")))
        Return False
      Else
        nPeacIva15 = NTSCInt(dttTmp.Rows(0)!tb_aiva15)
        nPeacIvainc = NTSCInt(dttTmp.Rows(0)!tb_aivainc)
        nPeacIvaInc2 = NTSCInt(dttTmp.Rows(0)!tb_aivainc2)
        nPeacIvaInc3 = NTSCInt(dttTmp.Rows(0)!tb_aivainc3)
        strPeacIvaInc3Fine = NTSCDate(dttTmp.Rows(0)!tb_adatfinivainc3).ToShortDateString
        strPeacIvaInc2Fine = NTSCDate(dttTmp.Rows(0)!tb_adatfinivainc2).ToShortDateString
        nPeacTipobf = NTSCInt(dttTmp.Rows(0)!tb_atipobf)
        lPeacContro = NTSCInt(dttTmp.Rows(0)!tb_acontro)
        nPeacMagazz = NTSCInt(dttTmp.Rows(0)!tb_amagazz)
        nPeacCaumag = NTSCInt(dttTmp.Rows(0)!tb_acaumag)
        nPeacListin = NTSCInt(dttTmp.Rows(0)!tb_alistin)
        nPeacListinCStd = NTSCInt(dttTmp.Rows(0)!tb_listcstd)
        nPeacScapro = NTSCInt(dttTmp.Rows(0)!tb_causcapro)
        strPeacScorpo = dttTmp.Rows(0)!tb_ascorpo.ToString
      End If

      nGLOBPeveTipobf = nPeveTipobf
      nGLOBPeacTipobf = nPeacTipobf

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function LeggiRegistro() As Boolean
    Dim lTmp As Integer = 0
    Try
      bModPM = False
      If CBool(ModuliDittaDitt(strDittaCorrente) And CLN__STD.bsModPM) Then bModPM = True
      bModTCO = False
      If CBool(ModuliExtDittaDitt(strDittaCorrente) And CLN__STD.bsModExtTCO) Then bModTCO = True
      bModRSM = False
      If CBool(ModuliDittaDitt(strDittaCorrente) And bsModSM) Then bModRSM = True
      bModDII = False
      If CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupDII) Then bModDII = True

      strNumcliPVR = oCldFdin.GetSettingBus("OPZIONI", ".", ".", "NsNumCliPVR", "", " ", "")
      strPrefixFreeStringPVR = oCldFdin.GetSettingBus("OPZIONI", ".", ".", "PrefixFreeStringPVR", "", " ", "")  ' NON DOCUMENTARE
      bNoUpdateTipobf = CBool(oCldFdin.GetSettingBus("BSVEFDIN", "OPZIONI", ".", "NoUpdateTipobf", "0", " ", "0"))
      'lTmp = NTSCInt(oCldFdin.GetSettingBus("BSVEFDIN", "OPZIONI", ".", "NumMaxDDT", "200", " ", "200")) 'opzione di registro con la quale si parametrizza il num. massimo di DDT gestiti dalla fattura diff. interattiva
      'If lTmp < 200 Or lTmp > 1000 Then
      '  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128714905625937500, "Nella opzione di registro \Bsvefdin\Opzioni\NumMaxDDT è indicato il valore non ammesso |" & lTmp.ToString & "|. Inicare un valore compreso tra 200 a 1000. Si procede con il valore predefinito 200.")))
      '  nNumMaxDDT = 200
      'Else
      '  nNumMaxDDT = lTmp
      'End If
      bORTO_ChiamaBsjoboll = CBool(Val(oCldFdin.GetSettingBus("BSVEFDIN", "OPZIONI", ".", "ORTO_ChiamaBsjoboll", "0", " ", "0"))) '-1 = Chiama BSJOBOLL in Modifica documento (anziché BSVEBOLL o BSREGDOC)   ' NON DOCUMENTARE
      strCalcPesi = oCldFdin.GetSettingBus("BSVEFDIN", "OPZIONI", ".", "Calc_pesi_in_doc", "N", " ", "N") 'blank,N,S
      bNoPesiSuRigheKitFittizie = CBool(oCldFdin.GetSettingBus("BSVEFDIN", "Opzioni", ".", "NoPesiSuRigheKitFittizie", "0", " ", "0"))
      strAutoNumProt = oCldFdin.GetSettingBus("BSVEFDIN", "OPZIONI", ".", "Prot_su_doc_ric", "N", " ", "N") 'blank,N,S
      bUsaKeyMag = CBool(Val(oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "UsaKeymag", "0", " ", "0")))
      bReprintDoc = CBool(Val(oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "ConfermaRistampa", "0", " ", "0")))
      strNoteFattDiff = Trim$(oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "NoteNuoviDocumenti", "", " ", ""))
      bRifBolleInNote = CBool(oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "RiferimBolleSuNote", "0", " ", "0")) '0,-1
      bIgnoraDestDiv = CBool(oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "IgnoraDestDiv", "0", " ", "0")) '0,-1 =non riporta sulla fatdiff la destinazione delle 1° bolla e non controlla conguenza sulle bolle successiva
      nGestScostAcconti = CInt(Val(oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "GestScostAcconti", "0", " ", "0"))) '0=nessuno, 1=come abbuono, 2=rettifica acconto
      dImpoScostAcconti = NTSCDec(oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "ImpoScostAcconti", IntSetNum("0,05"), " ", IntSetNum("0,05"))) 'soglia massima azione precedente proprietà
      bProteggiDocContab = CBool(Val(oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "ProteggiDocContab", "-1", " ", "-1"))) '-1=chiede la password x i documenti contabilizzati
      bIgnoraSegueFatturaSuDocRic = CBool(Val(oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "IgnoraSegueFatturaSuDocRic", "0", " ", "0")))  '-1=ignora il flag 'segue fattura nel tipo bolla/fattura dei DDT ricevuti (compatibilità 4.0, 5.0)
      bRifBolleInNoteNoHeader = CBool(oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "RiferimBolleSuNoteNoHeader", "0", " ", "0")) '0 default= antepone la scritta "Rif. DDT",-1 = non antepone alcuna stringa di riferimento
      bNonRiportaNote1DDT = CBool(oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "NonRiportaNote1DDT", "0", " ", "0")) '0 default= non riporta le note dai DDT selezionati
      strVisNoteConto = oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "Vis_note_conto_in_doc", "N", " ", "N")
      bConsentiModifCodPagaSc = CBool(Val(oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "ConsentiModifCodPagaSc", "0", " ", "0"))) 'Se abilitata a -1 nel doc. è ammessa la modifica del cod. pagamento e sconto pagamento, ed è ammesso collegate anche DDT con cod. pagam. e scopag diversi. Quando il doc viene salvato viene modificato lo scopag sui ddt e i prezzi di riga...
      bConsentiModifFlspinc = CBool(Val(oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "ConsentiModifFlspinc", "0", " ", "0"))) 'Se abilitata a -1 nel doc. è ammessa la modifica del flag 'addeb. spese incasso'. Quando il doc viene salvato viene modificato lo scopag sui ddt e i prezzi di riga...
      bCalcolaScadUsaSempreDatdoc = CBool(Val(oCldFdin.GetSettingBusDitt(strDittaCorrente, "Bsveboll", "Opzioni", ".", "CalcolaScadUsaSempreDatdoc", "0", " ", "0")))
      'bConsentiModifCodPagaSc non può operare con il modulo PM abilitato
      If bConsentiModifCodPagaSc And bModPM Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128714908080781250, "Non è possibile abilitare l'opzione di registro \Bsvefdin\Opzioni\ConsentiModifCodPagaSc a -1 in presenza del modulo Project Management. L'opzione verra disabilitata.")))
        bConsentiModifCodPagaSc = False
      End If
      If bConsentiModifFlspinc And bModPM Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129767296426963240, "Non è possibile abilitare l'opzione di registro \Bsvefdin\Opzioni\ConsentiModifFlspinc a -1 in presenza del modulo Project Management. L'opzione verra disabilitata.")))
        bConsentiModifFlspinc = False
      End If
      'leggo da opzioni generali se l'azienda gestisce i PVR
      bGestPVR = CBool(oCldFdin.GetSettingBus("OPZIONI", ".", ".", "GestPVR", "0", " ", "0"))
      bUsaContoFatt = CBool(Val(oCldFdin.GetSettingBus("BSVEFDIN", "OPZIONI", ".", "UsaContoFatt", "0", " ", "0")))
      strUsaContoFattDoc = oCldFdin.GetSettingBus("BSVEFADI", "OPZIONI", ".", "UsaContoFattDoc", "0", " ", "0") 'Se impostata, preleva il conto fatturazione del documento al posto di quello indicato in anagrafica
      bGestAlert = CBool(Val(oCldFdin.GetSettingBus("BSVEFDIN", "OPZIONI", ".", "Abilita_Alert", "0", " ", "0")))
      bDeterminaBolliSuOperazEsenti = CBool(Val(oCldFdin.GetSettingBus("Opzioni", ".", ".", "DeterminaBolliSuOperazEsenti", "0", " ", "0"))) 'Se attiva il bollo non viene determinato solo se in TESTMAGta vi è il codice di esenzione, ma se la somma delle operazioni esenti del documenti (righe e spese di piede) supera la soglia minima in TABBOTR  ' NON DOCUMENTARE
      bDisabilitaCheckDateAnteriori = CBool(Val(oCldFdin.GetSettingBus("Bsvefdin", "Opzioni", ".", "DisabilitaCheckDateAnteriori", "0", " ", "0"))) 'Se abilitata, disabilia il controllo che fa apparire il messaggio della mancata consecutività delle date di documento
      bCalcolaRagg = CBool(oCldFdin.GetSettingBus("Bsvefadi", "Opzioni", ".", "CalcolaTotaliRaggruppandoMM", "0", " ", "0")) '0 default,-1 = calcola i totali raggruppando per movmag iva, controp, ...  ' NON DOCUMENTARE
      strControlloPIvaCodFis = oCldFdin.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "ControlloPIvaCodFis", NTSCStr(IIf(bModRSM, "0", "1")), " ", NTSCStr(IIf(bModRSM, "0", "1")))
      bCancellaRegCG = CBool(oCldFdin.GetSettingBus("Bsvefadi", "Opzioni", ".", "CancellaRegCG", "0", " ", "0"))
      bAggiornaRiferimSoloSeVuoti = CBool(oCldFdin.GetSettingBus("BSVEFDIN", "OPZIONI", ".", "AggiornaRiferimDaPrimoDDTSoloSeVuoti", "0", ".", "0"))
      'bCollega_MG_DI = CBool(oCldFdin.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "Collega_MG_DI", "0", " ", "0"))
      'Cambiamento da NET 2015, si legge da ANADITAC (dati aggiuntivi contabilità):
      bCollega_MG_DI = oCldFdin.CollegaMGDI(strDittaCorrente)

      '--------------------------------------
      'Se l'opzione di registro bConsentiModifCodPagaSc è abilitata apre l'oggetto objAgdo
      If bConsentiModifCodPagaSc Or bConsentiModifFlspinc Then
        Dim strErr As String = ""
        Dim oTmp As Object = Nothing
        If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEVEFDIN", "BEVEBOLL", oTmp, strErr, False, "", "") = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128722560569218750, "ERRORE in fase di creazione Entity 'BEVEBOLL', l'opzione di registro \Bsvefdin\Opzioni\ConsentiModifCodPagaSc è stata disabilitata." & vbCrLf & "Errore:" & vbCrLf) & strErr))
          bConsentiModifCodPagaSc = False
          bConsentiModifFlspinc = False
        End If
        oCleBoll = CType(oTmp, CLEVEBOLL)
        AddHandler oCleBoll.RemoteEvent, AddressOf VebollGestisciEventiEntity
        If oCleBoll.Init(oApp, MyBase.oScript, MyBase.oCleComm, "", False, "", "") = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740017967775000, "Impossibile caricare il componente che effettua il ricolacolo dei documenti (""BEVEBOLL""), l'opzione di registro \Bsvefdin\Opzioni\ConsentiModifCodPagaSc è stata disabilitata.")))
          bConsentiModifCodPagaSc = False
          bConsentiModifFlspinc = False
        End If
        If Not oCleBoll.InitExt() Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128722556053593750, "Impossibile caricare il componente che effettua il ricolacolo dei documenti (""BEVEBOLL""), l'opzione di registro \Bsvefdin\Opzioni\ConsentiModifCodPagaSc è stata disabilitata.")))
          bConsentiModifCodPagaSc = False
          bConsentiModifFlspinc = False
        End If
        oCleBoll.bIsCRMUser = False     'se posso fare la fattura posso anche modificare il ddt!!!!
      End If    'If bConsentiModifCodPagaSc Then
      '--------------------------------------------------------------------------------------------------------------
      '--- Personalizzazione ELEMAC (da non documentare)
      '--------------------------------------------------------------------------------------------------------------
      bCastIvaCalcoloImposta = CBool(Val(oCldFdin.GetSettingBus("BSVEFDIN", "OPZIONI", ".", "CastIvaCalcoloImposta", "-1", " ", "-1")))
      bCastContropVariaCastiva = CBool(Val(oCldFdin.GetSettingBus("BSVEFDIN", "OPZIONI", ".", "CastContropVariaCastiva", "0", " ", "0")))
      '--------------------------------------------------------------------------------------------------------------
      bRiproponiDataDoc = CBool(Val(oCldFdin.GetSettingBusDitt(strDittaCorrente, "BSVEFDIN", "OPZIONI", ".", "RiproponiDataDoc", "0", " ", "0"))) '-1=proponi la data del prec. documento creato, 0=proponi sempre la data del sistema
      '--------------------------------------------------------------------------------------------------------------
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub VebollGestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      If e.TipoEvento <> "GRIAGG" Then
        ThrowRemoteEvent(e)
      End If
      'Throw New NTSException(e.Message)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function ResetVar() As Boolean
    Try
      bDocNonModificabile = False
      bInUnload = False
      bNonValidare = False
      bHasChanges = False
      bNew = False
      lContoCF = 0
      nRegiva = 0
      lNumprot = 0
      strAlfpro = " "
      bModifCastScad = False
      bProgrCambiato = False
      bOltre200 = False
      bInImportDDT = False
      bForzaValuteCambiDiv = False
      bInCalcolaTotali = False
      nCodtpbfOpen = 0

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

#Region "NUOVO / APRI / SALVA / CANCELLA"
  Public Overridable Function NuovoDocumento(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                             ByVal nAnno As Integer, ByVal strSerie As String, _
                                             ByVal lNumdoc As Integer) As Boolean
    Dim i As Integer = 0
    Try
      '----------------------------------------
      'verifico se esiste già un Documento con le stesse caratteristiche
      If oCldFdin.EsisteDoc(strDitta, strTipoDoc, nAnno, strSerie, lNumdoc) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222099375000, "Esiste già un documento con le stesse caratteristiche di quello che si desidera creare.")))
        Return False
      End If

      ResetVar()
      bNew = True

      CType(oCleComm, CLELBMENU).AccconfGetBlocchi(strDittaCorrente, strTipoDoc, dttAccconf)

      dsShared.Tables("TESTMAGC").Clear()
      dsShared.Tables("TESTMAGC").AcceptChanges()

      dsShared.Tables("ELENCODDT").Clear()
      dsShared.Tables("ELENCODDT").AcceptChanges()

      '----------------------------------------
      'ricollego i puntatori
      dttET = dsShared.Tables("TESTMAG")
      dttETC = dsShared.Tables("TESTMAGC")

      '----------------------------------------
      'creo una nuova riga di testmag
      dttET.Rows.Add(dsShared.Tables("TESTMAG").NewRow)
      dttET.Rows(dttET.Rows.Count - 1)!codditt = strDittaCorrente
      dttET.Rows(dttET.Rows.Count - 1)!tm_tipork = strTipoDoc
      dttET.Rows(dttET.Rows.Count - 1)!tm_anno = nAnno
      dttET.Rows(dttET.Rows.Count - 1)!tm_serie = strSerie
      dttET.Rows(dttET.Rows.Count - 1)!tm_numdoc = lNumdoc
      If bRiproponiDataDoc = True Then dttET.Rows(dttET.Rows.Count - 1)!tm_datdoc = dtDataNewDoc

      '----------------------------------------
      'nel datatable c'è ancora il vecchio documento in memoria: lo elimino 
      '(visto chce non l'ho potuto fare nella Salva o nella Ripristina perchè diversamente nella tabella
      'a cui sono collegati i controlli non ci sarebbe stato alcun record e tutti i controlli avrebbero dato il messaggio
      'di valore impostato non corretto)
      dsShared.Tables("TESTMAG").AcceptChanges()
      If dttET.Rows.Count > 1 Then
        For i = 0 To dttET.Rows.Count - 2
          dttET.Rows(0).Delete()
        Next
        dsShared.Tables("TESTMAG").AcceptChanges()
      End If

      '----------------------------------------
      dttETC.Rows.Add(dsShared.Tables("TESTMAGC").NewRow)
      dttETC.Rows(dttETC.Rows.Count - 1)!codditt = strDittaCorrente
      dttETC.Rows(dttETC.Rows.Count - 1)!tm_tipork = strTipoDoc
      dttETC.Rows(dttETC.Rows.Count - 1)!tm_anno = nAnno
      dttETC.Rows(dttETC.Rows.Count - 1)!tm_serie = strSerie
      dttETC.Rows(dttETC.Rows.Count - 1)!tm_numdoc = lNumdoc
      dttETC.AcceptChanges()
      If dttETC.Rows.Count > 1 Then
        For i = 0 To dttETC.Rows.Count - 2
          dttETC.Rows(0).Delete()
        Next
        dttETC.AcceptChanges()
      End If
      dsShared.Tables("TESTMAGC").AcceptChanges()
      If dttETC.Rows.Count > 1 Then
        For i = 0 To dttETC.Rows.Count - 2
          dttETC.Rows(0).Delete()
        Next
        dsShared.Tables("TESTMAGC").AcceptChanges()
      End If

      dttET.Rows(0)!tm_dtcomiva = dttET.Rows(0)!tm_datdoc

      bHasChanges = True

      If NTSCInt(dttET.Rows(0)!tm_numdoc) <> LegNuma(dttET.Rows(0)!tm_tipork.ToString, dttET.Rows(0)!tm_serie.ToString, NTSCInt(dttET.Rows(0)!tm_anno)) Then
        bProgrCambiato = True
      Else
        bProgrCambiato = False
      End If

      '-----------------------------------
      'se documento ricevuto cerco di proporre il num di protocollo
      InizProtocollo()

      '-----------------------------------
      'Mette a posto le note sul documento
      If IsDocumentoEmesso(dttET.Rows(0)!tm_tipork.ToString) Then
        If bRifBolleInNote Then
          dttET.Rows(0)!tm_note = DBNull.Value
        Else
          If strNoteFattDiff <> "" Then
            dttET.Rows(0)!tm_note = strNoteFattDiff
          Else
            dttET.Rows(0)!tm_note = DBNull.Value
          End If
        End If
      Else
        dttET.Rows(0)!tm_note = DBNull.Value
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ApriDoc(ByVal strDitta As String, ByVal bNew As Boolean, ByVal strTipoDoc As String, _
                                       ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                       ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim bOk As Boolean = False
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Dim evnt As NTSEventArgs = Nothing
    Try
      ResetVar()

      CType(oCleComm, CLELBMENU).AccconfGetBlocchi(strDittaCorrente, strTipoDoc, dttAccconf)

      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldFdin.ApriDocumento(strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, ds)

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      SetDefaultValueTestmag(ds)

      '--------------------------------------
      'compilo la descrizione del cli/forn
      If ds.Tables("TESTMAG").Rows.Count > 0 Then
        lContoCF = NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_conto)
        bOk = oCldFdin.ValCodiceDb(ds.Tables("TESTMAG").Rows(0)!tm_conto.ToString, strDittaCorrente, "ANAGRACF", "N", strTmp, dttEt_conto)
        If bOk Then
          If dttEt_conto.Rows.Count > 0 Then
            ds.Tables("TESTMAG").Rows(0)!xx_tipo = dttEt_conto.Rows(0)!an_tipo.ToString
            ds.Tables("TESTMAG").Rows(0)!xx_conto = dttEt_conto.Rows(0)!an_descr1.ToString & " " & _
                                dttEt_conto.Rows(0)!an_descr2.ToString & vbCrLf & _
                                dttEt_conto.Rows(0)!an_indir.ToString & vbCrLf & _
                                dttEt_conto.Rows(0)!an_cap.ToString & " " & _
                                dttEt_conto.Rows(0)!an_citta.ToString & " (" & _
                                dttEt_conto.Rows(0)!an_prov.ToString & ")   (" & _
                                dttEt_conto.Rows(0)!an_stato.ToString & ")"
          End If
        End If
      End If    'If ds.Tables("TESTMAG").Rows.Count > 0 Then

      bNew = False

      If dReturn = False Then Return False

      '------------------------------
      If Not dsShared Is Nothing Then
        RemoveHandler dsShared.Tables("TESTMAG").ColumnChanging, AddressOf BeforeColUpdate
        RemoveHandler dsShared.Tables("TESTMAG").ColumnChanged, AddressOf AfterColUpdate
      End If

      '--------------------------------------
      'memorizzo i puntatori locali
      dsShared = ds
      dttET = dsShared.Tables("TESTMAG")
      dttETC = dsShared.Tables("TESTMAGC")

      AddHandler dsShared.Tables("TESTMAG").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("TESTMAG").ColumnChanged, AddressOf AfterColUpdate

      '------------------------------
      If ds.Tables("TESTMAG").Rows.Count > 0 And ds.Tables("TESTMAGC").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128657033946875000, "La tabella TESTMAGC non è allineata con la rispettiva tabella. (TESTMAG). Procedo ugualmente.")))
        Return False
      End If

      If dttET.Rows.Count > 0 Then
        If Not Accconf_CheckVis() Then Return False

        '------------------------------
        'Modificabilità rate scadenze
        If CBool(NTSCInt(dttET.Rows(0)!tm_ccambiati) And FdinModifCastScad) Then bModifCastScad = True Else bModifCastScad = False

        If strUsaContoFattDoc <> "1" Then
          dttET.Rows(0)!tm_contfatt = 0
        End If

        '-----------------------------------
        'Mette a posto le note sul documento
        If IsDocumentoEmesso(dttET.Rows(0)!tm_tipork.ToString) Then
          If bRifBolleInNote Then
            'dttET.Rows(0)!tm_note = DBNull.Value
          End If
        End If
      End If

      '------------------------------
      'se è la lettura iniziale in form_load non faccio altro ed esco
      If lNumdoc = 0 Then
        ds.AcceptChanges()
        Return True
      End If

      bHasChanges = False 'imposto che il documento non è cambiato
      ds.AcceptChanges()        'confermo tutto

      If bNew = False And ds.Tables("TESTMAG").Rows.Count > 0 Then
        If ds.Tables("ELENCODDT").Rows.Count = 0 Then
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128720129916562500, _
                      "Attenzione! La Fattura/nota accred differita |" & lNumdoc & "| aperta non è collegata a nessun DDT/Ricevuta fisc." & _
                      vbCrLf & vbCrLf & "Provvedere alla cancellazione di questo documento?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
            SalvaDocumento("E")
          End If    'If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
          Return False
        Else
          ' setta il flag di forzatura cambi/valute
          i = 0
          For Each dtrT As DataRow In ds.Tables("ELENCODDT").Rows
            i += 1
            If NTSCDec(dtrT!tm_cambio) <> NTSCDec(ds.Tables("TESTMAG").Rows(0)!tm_cambio) Or _
               NTSCInt(dtrT!tm_valuta) <> NTSCInt(ds.Tables("TESTMAG").Rows(0)!tm_valuta) Then
              bForzaValuteCambiDiv = True
              Exit For
            End If
            'If i = nNumMaxDDT Then
            '  bOltre200 = True
            '  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128720138980312500, "Impossibile gestire fatture con più di |" & nNumMaxDDT & "| Bolle/DDT collegati.")))
            '  'Continua ugualmente a aprire una fattura senza bolle.
            '  Exit For
            'End If
          Next
        End If    'If ds.Tables("ELENCODDT").Rows.Count = 0 Then
      End If    'If bNew = False And ds.Tables("TESTMAG").Rows.Count > 0 Then
      '--------------------------------------------------------------------------------------------------------------
      If Not CalcolaTotali(True) Then Return False
      '--------------------------------------------------------------------------------------------------------------

      If ds.Tables("TESTMAG").Rows.Count > 0 Then
        If Not TestaBlocchi(False) Then Return False
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function TestaBlocchi(ByVal bNew As Boolean) As Boolean
    Dim strPwd As String = ""
    Dim evnt As NTSEventArgs = Nothing
    Dim strMsg As String = ""

    'stampe definitive su registri fiscali
    Dim nEscomp As Integer = 0
    Dim strTipoChiusure As String = "S"
    Dim bOk As Boolean = True
    Dim dttTmp As New DataTable

    Try
      '----------------------------------------------------
      'Avverte che il documento è contabilizzato
      If dttET.Rows(0)!tm_flcont.ToString <> "N" Then
        With dttET.Rows(0)

          '----------------------------
          'controllo se già stampato su CG o RI
          oCldFdin.EscompFromDate(strDittaCorrente, NTSCDate(dttET.Rows(0)!tm_datregef), nEscomp, Nothing)
          If nEscomp <> 0 Then
            oCldFdin.ValCodiceDb(strDittaCorrente, strDittaCorrente, "ANADITAC", "S", "", dttTmp)
            strTipoChiusure = dttTmp.Rows(0)!ac_flgiobol.ToString
            dttTmp.Clear()
            'test su libro giornale
            oCldFdin.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", "", dttTmp)
            With dttTmp.Rows(0)
              If strTipoChiusure <> "S" And nEscomp <> NTSCInt(dttTmp.Rows(0)!tb_escomp) Then
                If NTSCDate(dttET.Rows(0)!tm_datregef) < NTSCDate(dttTmp.Rows(0)!tb_dtullgp) Or (NTSCDate(dttET.Rows(0)!tm_datregef) = NTSCDate(dttTmp.Rows(0)!tb_dtullgp) And NTSCInt(dttET.Rows(0)!tm_numregef) <= NTSCInt(dttTmp.Rows(0)!tb_rgullgp)) Then
                  bOk = False
                End If
              Else
                If NTSCDate(dttET.Rows(0)!tm_datregef) < NTSCDate(dttTmp.Rows(0)!tb_dtullg) Or (NTSCDate(dttET.Rows(0)!tm_datregef) = NTSCDate(dttTmp.Rows(0)!tb_dtullg) And NTSCInt(dttET.Rows(0)!tm_numregef) <= NTSCInt(dttTmp.Rows(0)!tb_rgullg)) Then
                  bOk = False
                End If
              End If
            End With
            dttTmp.Clear()
          End If
          If bOk Then
            'test su reg. iva
            If Not oCldFdin.GetTabduri(strDittaCorrente, NTSCDate(dttET.Rows(0)!tm_datregef).ToShortDateString, _
                                       NTSCInt(dttET.Rows(0)!tm_numregef), dttTmp) Then Return False
            'ritorna dei record se la registraz. iva è stata stampata sui registri IVA
            If dttTmp.Rows.Count > 0 Then
              bOk = False
            End If
          End If
          dttTmp.Clear()

          If bOk = False Then
            If bProteggiDocContab = False Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130415926494411213, "Il documento è collegato ad una registrazione di contabilità già stampata su Libro Giornale e/o registri IVA. Il documento non potrà essere salvato o cancellato.")))
              bDocNonModificabile = True
              Return True
            Else
Riprova1:
              strMsg = oApp.Tr(Me, 130485975647490754, "Attenzione!" & vbCrLf & _
                "Il documento è collegato ad una registrazione di contabilità già stampata su Libro Giornale e/o registri IVA")
              strMsg &= oApp.Tr(Me, 129738611535033325, vbCrLf & "Inserire la Password per sbloccare l'apertura:")
              evnt = New NTSEventArgs(CLN__STD.ThMsg.INPUTPWD, strMsg)
              ThrowRemoteEvent(evnt)
              strPwd = UCase(oCldFdin.GetSettingBus("BSVEFDIN", "Opzioni", ".", "PwdDocContab", "nts", " ", "nts")).ToUpper
              If evnt.RetValue.Trim <> "" Then
                'Controlla PWD
                If evnt.RetValue.ToUpper <> strPwd Then GoTo Riprova1
              Else
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130485983917825098, "Non è stata digitata (o non è valida) la password per lo sblocco documenti. Il documento non potrà essere salvato o cancellato.")))
                bDocNonModificabile = True
                Return True
              End If
            End If
          End If    'If bOk = False Then

          '---------------------------------
          If bOk Then 'se registrazione so su lg o ri ...
            If bProteggiDocContab = False Then
              strMsg = oApp.Tr(Me, 129738611422509473, "Attenzione!" & vbCrLf & _
                "Il documento aperto è già contabilizzato con la registrazione n° |" & !tm_numregef.ToString & "| del |" & NTSCDate(!tm_datregef).ToShortDateString & "|")
              If NTSCInt(!tm_numrgin) > 0 Then
                strMsg &= oApp.Tr(Me, 129738611448887439, vbCrLf & "e relativo incasso/pagamento" & vbCrLf & "con la registrazione n° |" & !tm_numrgin.ToString & "| del |" & NTSCDate(!tm_datregin).ToShortDateString & "|")
              End If
              ThrowRemoteEvent(New NTSEventArgs("", strMsg))
            Else
Riprova:
              strPwd = UCase(oCldFdin.GetSettingBus("BSVEFDIN", "Opzioni", ".", "PwdDocContab", "nts", " ", "nts")).ToUpper

              strMsg = oApp.Tr(Me, 129738611478683505, "Attenzione!" & vbCrLf & _
                "Il documento aperto è già contabilizzato con la registrazione n° |" & !tm_numregef.ToString & "| del |" & NTSCDate(!tm_datregef).ToShortDateString & "|")
              If NTSCInt(!tm_numrgin) > 0 Then
                strMsg &= oApp.Tr(Me, 129738611509085063, vbCrLf & "e relativo incasso/pagamento" & vbCrLf & "con la registrazione n° |" & !tm_numrgin.ToString & "| del |" & NTSCDate(!tm_datregin).ToShortDateString & "|")
              End If
              strMsg &= oApp.Tr(Me, 130485983839185675, vbCrLf & "Inserire la Password per sbloccare l'apertura:")
              evnt = New NTSEventArgs(CLN__STD.ThMsg.INPUTPWD, strMsg)
              ThrowRemoteEvent(evnt)
              If evnt.RetValue.Trim <> "" Then
                'Controlla PWD
                If evnt.RetValue.ToUpper <> strPwd Then GoTo Riprova
              Else
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129738611578931495, "Non è stata digitata (o non è valida) la password per lo sblocco documenti contabilizzati. Il documento non potrà essere salvato o cancellato.")))
                bDocNonModificabile = True
              End If
            End If
          End If    'If bOk Then
        End With    'With dttET.Rows(0)
      End If    'If dttET.Rows(0)!tm_flcont.ToString <> "N" Then

      '----------------------------------------------------
      'Avverte che sono già state estratte le provvigioni agli agenti
      If dttET.Rows(0)!tm_flprov.ToString <> "N" Then
        ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 128655248081093750, "Attenzione! Sono già state estratte le provvigioni agli agenti per il documento aperto.")))
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function



  Public Overridable Function RipristinaDocumento() As Boolean
    Try
      'non decommentare la riga sotto: in fase di ripristino di un nuovo documento i controlli di testata 
      'rimarrebbero senza nessun record e darebbero una sfilza di errori!!
      'dttET.Rows(nRow).RejectChanges()
      bHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaDocumento(ByVal strState As String) As Boolean
    'strState: N = new, D = delete, U = update, E = cancella senza fare test pre-delete (chiamato solo da Apridoc quando collegata alla fattura non c'è nessun DDT)
    'strOrdlistCollegati = elenco degli ordlist separati da ',' che sono serviti per generare il documento (da BNORGNOR): al salva devo aggiornare anche ordlist/movrdo
    Dim bResult As Boolean = False
    Dim strErrore As String = ""
    Dim strTipork As String = ""
    Dim dtUltaggTmp As Date = NTSCDate(dttET.Rows(0)!tm_ultagg)
    Dim strOpnomeTmp As String = NTSCStr(dttET.Rows(0)!tm_opnome)
    Dim dttTmp As New DataTable
    Dim bAggNumprot As Boolean = False
    Dim strPVR As String = ""
    Dim i As Integer = 0
    Dim bCa2 As Boolean = False
    Dim nEscomp As Integer = 0
    Dim dttScad As New DataTable
    Dim lConeff As Integer = 0  'conto effetti attivi
    Dim bSostituisciCodicePagamento As Boolean = False

    Try
      If CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupCAE) Then bCa2 = True


      '----------------------------------------
      If strState = "E" Or strState = "D" Then
        If Not Accconf_CheckSalvaCancella("CANC") Then Return False
      Else
        If Not Accconf_CheckSalvaCancella(IIf(bNew, "INS", "MOD").ToString) Then Return False
      End If

      If strState = "E" Then
        strState = "D"
        GoTo CANCELLA
      End If

      strTipork = dttET.Rows(0)!tm_tipork.ToString

      '----------------------------------------
      'controllo se il documento aperto in modifica/cancellaz è stato variato nel frattempo da un altro utente
      If Not bNew Then
        If Not CheckTimeStamp(strState) Then Return False
      End If

      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If strState = "D" Or strState = "E" Then
        'non devo fare controlli (tranne quelli sulla CA2) ...
        If bCa2 Then
          If Not TestPreSalva_CheckCa2(strState) Then Return False
        End If
      Else
        If NTSCInt(dsShared.Tables("TESTMAG").Rows(0)!tm_conto) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128721059361093750, "Il conto cliente/fornitore è obbligatorio. Inserirlo prima di salvare")))
          Return False
        End If

        If NTSCDate(dsShared.Tables("TESTMAG").Rows(0)!tm_datdoc).Year <> NTSCInt(dsShared.Tables("TESTMAG").Rows(0)!tm_anno) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128782950889798000, "La data del documento non è inclusa nell'anno del documento. Correggere la data prima di salvare")))
          Return False
        End If

        '-------------------------------------
        'Almeno una bolla deve essere agganciata
        If dsShared.Tables("ELENCODDT").Rows.Count = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128721059798906250, "Almeno un DDT deve essere agganciato alla Fattura/nota accred. differita prima di salvare.")))
          Return False
        End If

        If NTSCInt(dsShared.Tables("TESTMAG").Rows(0)!tm_codpaga) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128725960695312500, "Il codice pagamento è obbligatorio. Inserirlo prima di salvare")))
          Return False
        End If

        ''-------------------------------------
        'If bNew = False And bOltre200 Then
        '  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128721060015000000, "Impossibile salvare documenti aperti con più di |" & nNumMaxDDT & "| DDT/Ric. Fisc collegate.")))
        '  Return False
        'End If

        If NTSCStr(dsShared.Tables("TESTMAG").Rows(0)!tm_datpar).Trim <> "" AndAlso NTSCDate(dsShared.Tables("TESTMAG").Rows(0)!tm_datdoc) < NTSCDate(dsShared.Tables("TESTMAG").Rows(0)!tm_datpar) Then
          ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 130220045080324629, "Attenzione: la data del documento è antecedente alla data della partita/documento ricevuto")))
          Return False
        End If

        '-------------------------------------
        If bNew And (strTipork = "K" Or strTipork = "(") Then
          If NTSCInt(dttET.Rows(0)!tm_numpar) > 0 And NTSCInt(dttET.Rows(0)!tm_annpar) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128721061583125000, "Nei nuovi documenti ricevuti se si indica il numero partita è obbligatorio specificare un anno partita valido (compreso tra 1900 e 2099).")))
            Return False
          End If
        End If

        '--------------------------------------------------------------------------------------------------------------
        '--- Controlla se esistono spese di piede
        '--- Se documento emesso, controlla che esista un codice Iva valido in tabella TABPEVE
        '--- Se documento ricevuto, controlla che esista un codice Iva valido in tabella TABPEAC
        '--------------------------------------------------------------------------------------------------------------
        With dttET.Rows(0)
          If (NTSCDec(!tm_speacc) + NTSCDec(!tm_speimb) + NTSCDec(!tm_speinc) + NTSCDec(!tm_bolli) + NTSCDec(!tm_speaccv) + NTSCDec(!tm_speimbv) + NTSCDec(!tm_speincv) + NTSCDec(!tm_bolliv)) <> 0 Then
            If strTipork <> "K" And strTipork <> "(" Then
              If (NTSCDate(dttET.Rows(0)!tm_datdoc) < NTSCDate(strPeveIvaInc3Fine) And nPeveIvaInc3 = 0) Or _
                 (NTSCDate(dttET.Rows(0)!tm_datdoc) >= NTSCDate(strPeveIvaInc3Fine) And NTSCDate(dttET.Rows(0)!tm_datdoc) < NTSCDate(strPeveIvaInc2Fine) And nPeveIvaInc2 = 0) Or _
                 (NTSCDate(dttET.Rows(0)!tm_datdoc) >= NTSCDate(strPeveIvaInc2Fine) And nPeveIvaInc = 0) Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129296153374657049, "Attenzione!" & vbCrLf & _
                  "Esitono delle spese di piede, mentre non è stato indicato un codice Iva valido alla data del documento in 'Personalizzazione Vendite'." & vbCrLf & _
                  "Impossibile salvare il documento.")))
                Return False
              End If
            Else
              If (NTSCDate(dttET.Rows(0)!tm_datdoc) < NTSCDate(strPeacIvaInc3Fine) And nPeacIvaInc3 = 0) Or _
                 (NTSCDate(dttET.Rows(0)!tm_datdoc) >= NTSCDate(strPeacIvaInc3Fine) And NTSCDate(dttET.Rows(0)!tm_datdoc) < NTSCDate(strPeacIvaInc2Fine) And nPeacIvaInc2 = 0) Or _
                 (NTSCDate(dttET.Rows(0)!tm_datdoc) >= NTSCDate(strPeacIvaInc2Fine) And nPeacIvainc = 0) Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129636574972998047, "Attenzione!" & vbCrLf & _
                  "Esitono delle spese di piede, mentre non è stato indicato un codice Iva valido alla data del documento in 'Personalizzazione Acquisti'." & vbCrLf & _
                  "Impossibile salvare il documento.")))
                Return False
              End If
            End If
          End If
        End With

        '-------------------------------------
        'Ricalcola il documento
        If Not CalcolaTotali(True) Then Return False

        '----------------------------------------
        'Controlli finali se castelletti sbloccati
        If NTSCStr(dttET.Rows(0)!tm_flscdb) = "S" Then
          If Not TestPreSalva_ControllaCastelletti() Then Return False
        End If

        dttET.Rows(0)!tm_conto2 = dttET.Rows(0)!tm_conto
        dsShared.AcceptChanges()

        '-------------------------------------
        'verifico se devo aggiornare o meno il numero di protocollo acquisti
        If bNew And _
           nRegiva = NTSCInt(dttET.Rows(0)!tm_nregiva) And _
           lNumprot = NTSCInt(dttET.Rows(0)!tm_numprot) And _
           strAlfpro = NTSCStr(dttET.Rows(0)!tm_alfpro) And _
           strAutoNumProt = "S" And _
           (dttET.Rows(0)!tm_tipork.ToString = "K" Or dttET.Rows(0)!tm_tipork.ToString = "(") Then
          bAggNumprot = True
        End If

        '----------------------------------------
        'fatture extracee: in testata è obbligatorio un codice di esenzione IVA, diversamente nelle spese di piede applicherebbe l'iva 
        'presa da peac
        If strAutoNumProt = "S" Then
          oCldFdin.ValCodiceDb(NTSCStr(dttET.Rows(0)!tm_tipobf), strDittaCorrente, "TABTPBF", "N", "", dttTmp)
          If NTSCStr(dttTmp.Rows(0)!tb_fattextrc) = "S" Then
            If NTSCInt(dttET.Rows(0)!tm_codese) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129461135723017578, "Con tipo bolla/fattura Extracee è obbligatorio indicare nella testata del documento un codice di esenzione IVA")))
              Return False
            Else
              dttTmp.Clear()
              oCldFdin.ValCodiceDb(NTSCStr(dttET.Rows(0)!tm_codese), strDittaCorrente, "TABCIVA", "N", "", dttTmp)
              If NTSCInt(dttTmp.Rows(0)!tb_aliq) <> 0 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129461136769726562, "Con tipo bolla/fattura Extracee è obbligatorio indicare nella testata del documento un codice di esenzione IVA")))
                Return False
              End If
              dttTmp.Clear()
            End If
            'NON DEVO MOVIMENTARE I PROTOCOLLI IVA
            dttET.Rows(0)!tm_nregiva = 0
            dttET.Rows(0)!tm_numprot = 0
            dttET.Rows(0)!tm_alfpro = " "
            bAggNumprot = False
          End If
        End If

        '----------------------------------------
        'Controllo scadenze modificate
        If bModifCastScad Then
          If Not ApriScadenze() Then Return False
        End If
        'in vb6 lFlagModif
        If CBool(NTSCInt(dttET.Rows(0)!tm_ccambiati) And FdinModifCastScad) Then
          If Not bModifCastScad Then dttET.Rows(0)!tm_ccambiati = NTSCInt(dttET.Rows(0)!tm_ccambiati) - FdinModifCastScad
        Else
          If bModifCastScad Then dttET.Rows(0)!tm_ccambiati = NTSCInt(dttET.Rows(0)!tm_ccambiati) + FdinModifCastScad
        End If

        '-------------------------------------
        'calcolo la stringa per i PVR
        If bGestPVR And (strTipork = "D" Or strTipork = "K") Then
          strPVR = CalcolaStringaPVR()
          dttET.Rows(0)!tm_andescr2 = Left(strPVR, 30)
          dttET.Rows(0)!tm_anindir = Mid(strPVR, 31)
        End If

        'resetto il flag di 'ddt modificato - rielaborare fattura'
        'viene impostato nella stored procedure bsveboll_9fcancella
        dttET.Rows(0)!tm_flagiva_1 = DBNull.Value

        '--------------------------------------------------------------------------------------------------------------
        '--- Se si è in inserimento di un nuovo documento e questo possiede la data anteriore
        '--- all'eventuale ultimo documento inserito a parità di tipo/anno/serie
        '--- chiede conferma prima di proseguire
        '--------------------------------------------------------------------------------------------------------------
        If bDisabilitaCheckDateAnteriori = False Then
          If ConfermaDocConDataAnteriore(dttET.Rows(0)!tm_tipork.ToString, NTSCInt(dttET.Rows(0)!tm_anno), _
                                       dttET.Rows(0)!tm_serie.ToString, NTSCStr(dsShared.Tables("TESTMAG").Rows(0)!tm_datdoc), _
                                       NTSCInt(dttET.Rows(0)!tm_numdoc)) = False Then
            Return False
          End If
        End If

        '-------------------------------------
        'calcolo le scadenze
        If CBool(NTSCInt(dttET.Rows(0)!tm_ccambiati) And FdinModifCastScad) Then
          'non devo fare nulla: le scadenze sono già state impostate da form di 'modifica scadenze'
        Else
          'devo ricalcolarle
          For i = 1 To 8
            dttET.Rows(0)("tm_tippaga_" & i.ToString) = 0
            dttET.Rows(0)("tm_datsca_" & i.ToString) = DBNull.Value
            dttET.Rows(0)("tm_impsca_" & i.ToString) = 0
            dttET.Rows(0)("tm_impscav_" & i.ToString) = 0
          Next
          'calcolo le scadenze sul residuo da pagare
          If Not CalcolaScadenzeStandard(False) Then Return False
        End If    'If CBool(NTSCInt(dttET.Rows(0)!tm_ccambiati) And bsVEBOLLmodifCastScad) Then
      End If    'If strState = "D" Then

      If bCa2 Then
        If Not TestPreSalva_CheckCa2(strState) Then Return False
      End If

      '-------------------------------------
      'aggiorno le note di testata
      '--------------------------------------------------------------------------------------------------------------
      '--- NOTE: se il programma chiamante è Fatturazione Differita, non aggiungo eventuali riferimenti,
      '--- nelle note del documento, perché potrebbe averlo fatto (o meno), in BEVEFADI.
      '--------------------------------------------------------------------------------------------------------------
      If (dttET.Rows(0)!tm_tipork.ToString = "D" Or dttET.Rows(0)!tm_tipork.ToString = "P") And _
         (bRifBolleInNote = True) And (strNomeProgrammaChiamante.ToUpper <> "BNVEFADI") Then
        If bRifBolleInNoteNoHeader Then
          dttET.Rows(0)!tm_note = ""
        Else
          dttET.Rows(0)!tm_note = "Rif. DDT"
        End If
        For Each dtrT As DataRow In dsShared.Tables("ELENCODDT").Rows
          Dim strSerie As String = ""
          If NTSCStr(dtrT!tm_serie).Trim <> "" Then strSerie = "/" & NTSCStr(dtrT!tm_serie)
          dttET.Rows(0)!tm_note = dttET.Rows(0)!tm_note.ToString & " n. " & NTSCInt(dtrT!tm_numdoc).ToString & strSerie & " del " & NTSCDate(dtrT!tm_datdoc).ToShortDateString & " "
        Next
      End If

      dttET.Rows(0)!tm_ultagg = DateTime.Now
      dttET.Rows(0)!tm_opnome = oApp.User.Nome
CANCELLA:

      If strState = "D" Then
        'cancello prima la registrazione contabile, se presente: nell'ordine: abbuono, incasso, fattura
        'questo perche bsvepnfa registra in prima nota sempre prima la fattura, poi l'evenutale incasso, poi l'eventuale abbuono...
        If (bCancellaRegCG Or ChiamaPnfa()) And NTSCStr(dttET.Rows(0)!tm_datregef).Trim <> "" Then
          'verifico se posso cancellare: 
          '---------------------------
          'abbuono
          If NTSCInt(dttET.Rows(0)!tm_numregom) <> 0 Then
            dttScad.Clear()
            If Not CType(oCleComm, CLELBMENU).TestPreCancellaRegistrazioneEx(strDittaCorrente, NTSCDate(dttET.Rows(0)!tm_datregef).ToShortDateString, NTSCInt(dttET.Rows(0)!tm_numregom), "N", False, strErrore, dttScad) Then
              If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
              Return False
            End If
          End If
          '---------------------------
          'incassato
          If NTSCInt(dttET.Rows(0)!tm_numrgin) <> 0 Then
            dttScad.Clear()
            If Not CType(oCleComm, CLELBMENU).TestPreCancellaRegistrazioneEx(strDittaCorrente, NTSCDate(dttET.Rows(0)!tm_datregef).ToShortDateString, NTSCInt(dttET.Rows(0)!tm_numrgin), "N", False, strErrore, dttScad) Then
              If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
              Return False
            End If
          End If
          '---------------------------
          'fattura
          If NTSCInt(dttET.Rows(0)!tm_numregef) <> 0 Then
            dttScad.Clear()
            If Not CType(oCleComm, CLELBMENU).TestPreCancellaRegistrazioneEx(strDittaCorrente, NTSCDate(dttET.Rows(0)!tm_datregef).ToShortDateString, NTSCInt(dttET.Rows(0)!tm_numregef), "N", False, strErrore, dttScad) Then
              'devo scartare la registrazione degli omaggi e di incasso contestuale
              If NTSCInt(dttET.Rows(0)!tm_numregom) <> 0 Then
                For Each dtrT1 As DataRow In dttScad.Select("sc_flsaldato = 'S' AND sc_dtsaldato = " & CDataSQL(NTSCDate(dttET.Rows(0)!tm_datregef)) & " AND sc_rgsaldato = " & NTSCInt(dttET.Rows(0)!tm_numregom))
                  dtrT1.Delete()
                Next
                dttScad.AcceptChanges()
              End If
              If NTSCInt(dttET.Rows(0)!tm_numrgin) <> 0 Then
                For Each dtrT1 As DataRow In dttScad.Select("sc_flsaldato = 'S' AND sc_dtsaldato = " & CDataSQL(NTSCDate(dttET.Rows(0)!tm_datregef)) & " AND sc_rgsaldato = " & NTSCInt(dttET.Rows(0)!tm_numrgin))
                  dtrT1.Delete()
                Next
                dttScad.AcceptChanges()
              End If

              'scarto le scadenze generate e saldate all'interno della stessa registrazione 'nota accredito emessa che compensa fattura emessa'
              For Each dtrT1 As DataRow In dttScad.Select("sc_flsaldato = 'S' AND sc_dtsaldato = sc_datreg AND sc_rgsaldato = sc_numreg")
                dtrT1.Delete()
              Next
              dttScad.AcceptChanges()

              'se ho scadenze collegate ad incassi differiti o no RB/tratte, avviso subito che non posso cancellare
              If dttScad.Select("sc_codincdiff > 0 or sc_tippaga > 2").Length > 0 Then
                If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
                Return False
              End If

              'possono essere solo scadenze di effetti: verifico per ogni record se posso cancellare la registrazione:
              'se posso farlo (perche emesso gli effetti ma non presentati in banca, cancellerà anche queste registrazioni
              'le scadenze, sia con che senza chiusura cliente, sono quelle sul conto EFFETTI ATTIVI con stessi estremi di partita del cliente
              oCldFdin.ValCodiceDb("1", strDittaCorrente, "TABPECG", "N", "", dttTmp)
              If dttTmp.Rows.Count > 0 Then
                lConeff = oCldFdin.TrovaContoDaContrEConto(strDittaCorrente, NTSCInt(dttTmp.Rows(0)!tb_contreff), NTSCInt(dttTmp.Rows(0)!tb_coneff))
              End If
              dttTmp.Clear()
              If lConeff = 0 Then
                If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
                Return False
              End If

              For Each dtrT1 As DataRow In dttScad.Rows
                If Not oCldFdin.GetScadEffetti(strDittaCorrente, lConeff, NTSCInt(dtrT1!sc_annpar), _
                                               NTSCStr(dtrT1!sc_alfpar), NTSCInt(dtrT1!sc_numpar), _
                                               NTSCInt(dtrT1!sc_numrata), dttTmp) Then
                  If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
                  Return False
                End If
                If dttTmp.Rows.Count = 0 Then
                  'non c'è emissione effetti, ma se sono qui è perchè le scadenze sono saldate: blocco
                  'attenzione: passa di qui anche nel caso di emissione effetti senza chiusura cliente
                  'se genero gli effetti e poi cancello la generazione effetti. sulla scadenza cliente rimane sc_fldis = "S"
                  'stessa cosa se spezzo la scadenza quando sc_fldis = "S"! la scadenza non è saldata e non c'è la generazione effetti
                  'per ora blocco solo se la scadenza è saldata! se non è saldata, potrei aver cancellato la scadenza a mano da prima nota
                  If NTSCStr(dtrT1!sc_flsaldato) = "S" Then
                    If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
                    Return False
                  Else
                    'così non cercherò di cancellare la registrazione che ha saldato questo record, perchè già non esistente
                    dtrT1!sc_flsaldato = DBNull.Value
                    dtrT1!sc_rgsaldato = 0
                  End If
                Else
                  If NTSCStr(dttTmp.Rows(0)!sc_flsaldato) = "S" Then
                    'già presentata in banca
                    If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
                    Return False
                  Else
                    'farà cancellare la registrazione
                    dtrT1!sc_dtsaldato = NTSCDate(dttTmp.Rows(0)!sc_datreg)
                    dtrT1!sc_rgsaldato = NTSCInt(dttTmp.Rows(0)!sc_numreg)
                  End If
                End If

                If NTSCInt(dtrT1!sc_rgsaldato) <> 0 Then
                  If Not CType(oCleComm, CLELBMENU).TestPreCancellaRegistrazione(strDittaCorrente, NTSCDate(dtrT1!sc_dtsaldato).ToShortDateString, NTSCInt(dtrT1!sc_rgsaldato), "N", False, "") Then
                    If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
                    Return False
                  End If
                End If
              Next    'For Each dtrT1 As DataRow In dttScad.Rows
              dttScad.AcceptChanges()
            End If
          End If    'If NTSCInt(dttET.Rows(0)!tm_numregef) <> 0 Then

          '---------------------------
          'eseguo la cancellazione
          'prima cancello eventuali registrazioni di emissione effetti
          For Each dtrT1 As DataRow In dttScad.Rows
            If NTSCStr(dtrT1!sc_dtsaldato) <> "" Then
              If Not CType(oCleComm, CLELBMENU).CancellaRegistrazione(strDittaCorrente, NTSCDate(dtrT1!sc_dtsaldato).ToShortDateString, NTSCInt(dtrT1!sc_rgsaldato), "N") Then
                Return False
              End If
            End If
          Next
          'ora cancello le registrazioni effettivamente collegate alla fattura
          If Not CType(oCleComm, CLELBMENU).CancellaRegistrazione(strDittaCorrente, NTSCDate(dttET.Rows(0)!tm_datregef).ToShortDateString, NTSCInt(dttET.Rows(0)!tm_numregom), "N") Then
            Return False
          End If
          If Not CType(oCleComm, CLELBMENU).CancellaRegistrazione(strDittaCorrente, NTSCDate(dttET.Rows(0)!tm_datregef).ToShortDateString, NTSCInt(dttET.Rows(0)!tm_numrgin), "N") Then
            Return False
          End If
          If Not CType(oCleComm, CLELBMENU).CancellaRegistrazione(strDittaCorrente, NTSCDate(dttET.Rows(0)!tm_datregef).ToShortDateString, NTSCInt(dttET.Rows(0)!tm_numregef), "N") Then
            Return False
          End If
          dttET.Rows(0)!tm_flcont = "N"
          strErrore = ""  'pulisco dall'eventuale errore di scadenze saldate
        End If    'If bCancellaRegCG And NTSCStr(dtrT!tm_datregef).Trim <> "" Then
      End If    'If strState = "D" Then
      '--------------------------------------------------------------------------------------------------------------
      '--- Se il totale della Fattura Differita Ricevuta è negativo ed è attiva l'opzione di registro
      '--------------------------------------------------------------------------------------------------------------
      If (strState <> "D") And _
         (NTSCStr(dttET.Rows(0)!tm_tipork).ToUpper = "K") And (bStornaDDTResoForn = True) And _
         (NTSCDec(dttET.Rows(0)!tm_totdoc) < 0 Or NTSCDec(dttET.Rows(0)!tm_totdocv) < 0) Then
        Dim evnt As New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 130485965988206623, "Attenzione!" & vbCrLf & _
          "Il totale documento della Fattura Differita Ricevuta è negativo." & vbCrLf & _
          "Confermare ugualmente?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla se la rimanenza (diversa da zero) documento rientra negli scaglioni indicati in ANAGRA
      '--- (solo per determinati documenti e solo se, per il documento corrente, non è stata chiesta
      '--- l'eventuale sostituzione del codice pagamento e successivo ricalcolo del documento)
      '--------------------------------------------------------------------------------------------------------------
        If (strState = "N" Or strState = "U") And (CType(oCleComm, CLELBMENU).oTestata.dRimanenza > 0) And _
         (bCodicePagamentoRicalcolato = False) And (bModPM = False) Then
        Dim nCodpagaOut As Integer = 0
        Dim bInferioreAImportoMinimo As Boolean = False
        Dim bInferioreAImportoMassimo As Boolean = False
        Dim strDespagaOut As String = ""
        Dim lImportoOut As Decimal = 0
        Dim lImportoOut2 As Decimal = 0
        Dim evnt As NTSEventArgs = Nothing
        With dttET.Rows(0)
          If RitornaCodicePagamentoPerScaglioni(NTSCInt(!tm_conto), _
            CType(oCleComm, CLELBMENU).oTestata.dRimanenza, lImportoOut, NTSCInt(!tm_codpaga), _
            nCodpagaOut, strDespagaOut, bInferioreAImportoMinimo, lImportoOut2, bInferioreAImportoMassimo) = True Then
            Select Case strNomeProgrammaChiamante.ToUpper
              Case "BNVEFADI" : bSostituisciCodicePagamento = True
              Case Else
                If lImportoOut2 <> 0 Then
                  evnt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 130566221640354825, "Attenzione!" & vbCrLf & _
                    "La rimanenza del documento corrente (|" & Format(CType(oCleComm, CLELBMENU).oTestata.dRimanenza, oApp.FormatImporti) & "|)" & vbCrLf & _
                    "risulta superiore al 'Limite importo minimo' (|" & Format(lImportoOut, oApp.FormatImporti) & "|) e inferiore al 'Limite grandi importi' (|" & Format(lImportoOut2, oApp.FormatImporti) & "|)" & vbCrLf & _
                    "indicato nell'Anagrafica relativa al conto intestatario del documento." & vbCrLf & _
                    "Applicare il codice pagamento '|" & nCodpagaOut.ToString & "|' - |" & strDespagaOut & "|" & vbCrLf & _
                    "relativo allo scaglione?"))
                Else
                  evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 130566320805210873, "Attenzione!" & vbCrLf & _
                    "La rimanenza del documento corrente (|" & Format(CType(oCleComm, CLELBMENU).oTestata.dRimanenza, oApp.FormatImporti) & "|)" & vbCrLf & _
                    "risulta |" & IIf(bInferioreAImportoMinimo = True, "inferiore al 'Limite importo minimo'", "superiore al 'Limite grandi importi'").ToString & "| (|" & Format(lImportoOut, oApp.FormatImporti) & "|)" & vbCrLf & _
                    "indicato nell'Anagrafica relativa al conto intestatario del documento." & vbCrLf & _
                    "Applicare il codice pagamento '|" & nCodpagaOut.ToString & "|' - |" & strDespagaOut & "|" & vbCrLf & _
                    "relativo allo scaglione?"))
                End If

                ThrowRemoteEvent(evnt)
                If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then bSostituisciCodicePagamento = True
            End Select
            If bSostituisciCodicePagamento = True Then
              If strNomeProgrammaChiamante.ToUpper <> "BNVEFADI" Then bCodicePagamentoRicalcolato = True
              !tm_codpaga = nCodpagaOut
              .AcceptChanges()
              CalcolaTotali(True)

              'devo ricalcolarle le scadenze sulla nuovo cod. pagamento (se le avevo forzate, perdo la modifica)
              dttET.Rows(0)!tm_ccambiati = 0
              For i = 1 To 8
                dttET.Rows(0)("tm_tippaga_" & i.ToString) = 0
                dttET.Rows(0)("tm_datsca_" & i.ToString) = DBNull.Value
                dttET.Rows(0)("tm_impsca_" & i.ToString) = 0
                dttET.Rows(0)("tm_impscav_" & i.ToString) = 0
              Next
              'calcolo le scadenze sul residuo da pagare
              If Not CalcolaScadenzeStandard(False) Then Return False
            End If
          End If
        End With
      End If
      '--------------------------------------------------------------------------------------------------------------
      bResult = oCldFdin.Salvadocumento(dsShared, strState, bProgrCambiato, bAggNumprot, bConsentiModifCodPagaSc, _
        bNoUpdateTipobf, strErrore, bCa2, bConsentiModifFlspinc, bSostituisciCodicePagamento)
      If bResult Then
        bHasChanges = False
      Else
        dttET.Rows(0)!tm_ultagg = dtUltaggTmp    'altrimenti a risalvataggio avvisa che un altro utente ha cambiato l'anagrafica ditta ...
        dttET.Rows(0)!tm_opnome = strOpnomeTmp
      End If
      'comunico gli errori all'UI
      If strErrore <> "" Then
        If strErrore.Substring(0, 3) = "*N*" Then
          'cambio il numero documento in tutti i posti interessati
          strErrore = strErrore.Substring(3)
          CambiaNumdoc(dsShared, NTSCInt(dttET.Rows(0)!tm_numdoc))
        End If
        ThrowRemoteEvent(New NTSEventArgs("", strErrore))
      End If

      '---------------------------------
      ' Aggiorno lo sconto pagamento sui DDT collegati, se richiesto
      If bResult And (strState = "N" Or strState = "U") And (bConsentiModifCodPagaSc Or bConsentiModifFlspinc) And Not oCleBoll Is Nothing Then
        If Not AggiornaBolScopag() Then
          'Anche se non riesce a farlo comunque non esco visto che la fatt. diff è già salvata
          'Eventualmente vengono segnalati dei messaggi
        End If
      End If

      If bResult And strState = "U" And dttET.Rows(0)!tm_flcont.ToString = "S" And bGestAlert Then
        'ALERT Documento già contabilizzato
        AlertContabilizzato()
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Azzera il campo TESTMAG.tm_flagiva_1 dei DDT collegati
      '--------------------------------------------------------------------------------------------------------------
      oCldFdin.SalvaDocumentoAggiornaBollAfterAll(dsShared)
      '--------------------------------------------------------------------------------------------------------------
      If bNew = True Then dtDataNewDoc = NTSCDate(dttET.Rows(0)!tm_datdoc)
      '--------------------------------------------------------------------------------------------------------------

      'imposto il valore di uscita dalla funzione
      SalvaDocumento = bResult

      '-------------------------------
      'faccio contabilizzare il documento
      'se non andrà a buon fine la contabilizzazione pazienza, verranno dati dei messaggi a 
      'video o visualizzerò il file di log di pnfa, ma mantengo il doc in magaz
      'non genero nulla se il num. documento è negativo (ovvero elaborazione di prova)
      If bResult And ChiamaPnfa() And (strState = "N" Or strState = "U") And NTSCInt(dttET.Rows(0)!tm_numdoc) > 0 Then
        oCldFdin.EscompFromDate(strDittaCorrente, NTSCDate(dttET.Rows(0)!tm_datdoc), nEscomp, Nothing)
        If CreaPnfa() And nEscomp <> 0 Then
          oClePnfa.ElaboraSingoloDoc(strDittaCorrente, NTSCStr(dttET.Rows(0)!tm_tipork), NTSCInt(dttET.Rows(0)!tm_anno), _
                                    NTSCStr(dttET.Rows(0)!tm_serie), NTSCInt(dttET.Rows(0)!tm_numdoc), _
                                    NTSCDate(dttET.Rows(0)!tm_datdoc).ToShortDateString, nEscomp, CBool(strState = "U"))

          'ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 127939835583750000, "Contabilizzazione documenti terminata.")))
          If oClePnfa.LogError = True Then
            'faccio aprire alla ui il file di log
            Dim evnt As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 127940796626250000, "Esistono dei messaggi nel file di log del programma di contabilizzazione. Visualizzare il file?"))
            ThrowRemoteEvent(evnt)
            If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.CALL_PROCESSSTART, oClePnfa.LogFileName))
            End If
          End If
        End If    'If CreaPnfa() Then
      End If    'If bResult And ChiamaPnfa Then
    Catch ex As Exception
      If SalvaDocumento = False Then
        dttET.Rows(0)!tm_ultagg = dtUltaggTmp    'altrimenti a risalvataggio avvisa che un altro utente ha cambiato l'anagrafica ditta ...
        dttET.Rows(0)!tm_opnome = strOpnomeTmp
      Else
        'il documento è già stato salvato, per cui non devo ricaricare tm_ultagg e tm_opnome
      End If
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttScad.Clear()
    End Try
  End Function

  Public Overridable Function TestPreSalva_ControllaCastelletti() As Boolean
    Dim dTotImpIVA As Decimal = 0
    Dim dTotImpCont As Decimal = 0
    Dim dTotImpIVAval As Decimal = 0
    Dim dTotImpContval As Decimal = 0
    Dim i As Integer = 0

    Try
      dTotImpIVA = 0
      dTotImpIVAval = 0
      For i = 1 To 8
        If NTSCInt(dttET.Rows(0)("tm_codiva_" & i.ToString)) <> 0 Then
          dTotImpIVA = dTotImpIVA + NTSCDec(dttET.Rows(0)("tm_imponib_" & i.ToString))
          dTotImpIVAval = dTotImpIVAval + NTSCDec(dttET.Rows(0)("tm_imponibv_" & i.ToString))
        Else
          Exit For
        End If
      Next

      dTotImpCont = 0
      dTotImpContval = 0
      For i = 1 To 20
        If NTSCInt(dttETC.Rows(0)("tm_ccontr_" & i.ToString)) <> 0 Then
          dTotImpCont = dTotImpCont + NTSCDec(dttETC.Rows(0)("tm_impcont_" & i.ToString))
          dTotImpContval = dTotImpContval + NTSCDec(dttETC.Rows(0)("tm_impcontv_" & i.ToString))
        Else
          Exit For
        End If
      Next

      Return ControllaCastelletti(dTotImpIVA, dTotImpCont, dTotImpIVAval, dTotImpContval)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try

  End Function
  Public Overridable Function TestPreSalva_CheckCa2(ByVal strState As String) As Boolean
    'strState: N = new, D = delete, U = update
    Dim dttTmp As New DataTable
    Try
      'non gestite fatt diff ric. fiscali.
      If NTSCStr(dsShared.Tables("TESTMAG").Rows(0)!tm_tipork) = "P" Then Return True
      oCldFdin.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", "", dttTmp)

      '----------------------------
      'non posso modificare/cancellare se in tabanaz la data congelamento CA è successiva alla data del documento
      If NTSCDate(dttTmp.Rows(0)!tb_dtulaca) >= NTSCDate(NTSCStr(dsShared.Tables("TESTMAG").Rows(0)!tm_datdoc)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129276269539743438, "ATTENZIONE: con il modulo della 'Contabilità analitica duplice contabile' attivato non è possibile inserire/modificare/cancellare documenti con data NON SUCCESSIVA alla 'data ultimo aggiornamento CA' indicata in anagrafica ditta(|" & NTSCDate(dttTmp.Rows(0)!tb_dtulaca).ToShortDateString & "|)")))
        Return False
      End If

      If Not bNew Then
        'la fattura non deve essere registrata in CG!!!!
        If NTSCInt(dsShared.Tables("TESTMAG").Rows(0)!tm_numregef) <> 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129276268560241484, "ATTENZIONE: con il modulo della 'Contabilità analitica duplice contabile' attivato non è possibile modificare/cancellare fatture già contabilizzate. Provvedere prima a cancellare la fattura in Contabilità generale")))
          Return False
        End If
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function ControllaCastelletti(ByVal dTotImpIVA As Decimal, ByVal dTotImpCont As Decimal, ByVal dTotImpIVAval As Decimal, ByVal dTotImpContval As Decimal) As Boolean
    Dim evnt As NTSEventArgs = Nothing
    Dim dDiff As Decimal = 0
    Dim dMerce As Decimal = 0
    Dim i As Integer = 0
    Try
      ' tot impon. iva con totale controp + spese
      With dttET.Rows(0)
        If ArrDbl(dTotImpIVA, oCldFdin.TrovaNdec(0)) <> ArrDbl((dTotImpCont + NTSCDec(!tm_speinc) + NTSCDec(!tm_speacc) + NTSCDec(!tm_bolli) + NTSCDec(!tm_speimb)), oCldFdin.TrovaNdec(0)) Then
          dDiff = ArrDbl(ArrDbl(dTotImpIVA, oCldFdin.TrovaNdec(0)) - ArrDbl((dTotImpCont + NTSCDec(!tm_speinc) + NTSCDec(!tm_speacc) + NTSCDec(!tm_bolli) + NTSCDec(!tm_speimb)), oCldFdin.TrovaNdec(0)), oCldFdin.TrovaNdec(0))

          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 128666546457343750, _
                              "La somma degli imponibili IVA (|" & dTotImpIVA.ToString(oApp.FormatImporti) & _
                              "|) è diversa dalla somma tra gli importi delle contropartite (|" & dTotImpCont.ToString(oApp.FormatImporti) & _
                              "|) + spese trasporti (|" & NTSCDec(!tm_speacc).ToString(oApp.FormatImporti) & _
                              "|) + spese incasso (|" & NTSCDec(!tm_speinc).ToString(oApp.FormatImporti) & _
                              "|) + spese bolli (|" & NTSCDec(!tm_bolli).ToString(oApp.FormatImporti) & _
                              "|) + spese imballi (|" & NTSCDec(!tm_speimb).ToString(oApp.FormatImporti) & _
                              "|)." & vbCrLf & vbCrLf & "La differenza è di |" & dDiff.ToString(oApp.FormatImporti) & _
                              "|." & vbCrLf & vbCrLf & "Procedere ugualmente?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
        End If

        If NTSCInt(dttET.Rows(0)!tm_valuta) <> 0 Then
          If ArrDbl(dTotImpIVAval, oCldFdin.TrovaNdec(NTSCInt(dttET.Rows(0)!tm_valuta))) <> ArrDbl((dTotImpContval + NTSCDec(!tm_speincv) + NTSCDec(!tm_speaccv) + NTSCDec(!tm_bolliv) + NTSCDec(!tm_speimbv)), oCldFdin.TrovaNdec(NTSCInt(dttET.Rows(0)!tm_valuta))) Then
            dDiff = ArrDbl(dTotImpIVAval, oCldFdin.TrovaNdec(NTSCInt(dttET.Rows(0)!tm_valuta))) - ArrDbl((dTotImpContval + NTSCDec(!tm_speincv) + NTSCDec(!tm_speaccv) + NTSCDec(!tm_bolliv) + NTSCDec(!tm_speimbv)), oCldFdin.TrovaNdec(NTSCInt(dttET.Rows(0)!tm_valuta)))

            evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 128666549587968750, _
                    "La somma degli imponibili IVA (|" & dTotImpIVAval.ToString(oApp.FormatImpVal) & _
                    "|) è diversa dalla somma tra gli importi delle contropartite (|" & dTotImpContval.ToString(oApp.FormatImpVal) & _
                    "|) + spese trasporti (|" & NTSCDec(!tm_speaccv).ToString(oApp.FormatImpVal) & _
                    "|) + spese incasso (|" & NTSCDec(!tm_speincv).ToString(oApp.FormatImpVal) & _
                    "|) + spese bolli (|" & NTSCDec(!tm_bolliv).ToString(oApp.FormatImpVal) & _
                    "|) + spese imballi (|" & NTSCDec(!tm_speimbv).ToString(oApp.FormatImpVal) & _
                    "|)." & vbCrLf & vbCrLf & "La differenza è di |" & dDiff.ToString(oApp.FormatImpVal) & _
                    "|." & vbCrLf & vbCrLf & "Procedere ugualmente?"))
            ThrowRemoteEvent(evnt)
            If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
          End If
        End If
      End With

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function AggiornaBolScopag() As Boolean
    'alla fine del salvataggio, se serve e se posso aggiorno sui ddt lo sconto pagamento 
    'impostandolo uguale a quello della fattura differita
    Dim ds As New DataSet
    Try
      For Each dtrT As DataRow In dsShared.Tables("ELENCODDT").Select("tm_tipork <> 'T' AND (tm_scopag <> " & CDblSQL(NTSCDec(dsShared.Tables("TESTMAG").Rows(0)!tm_scopag)) & " OR tm_flspinc <> " & CStrSQL(dsShared.Tables("TESTMAG").Rows(0)!tm_flspinc) & ")")
        'ho aggiornato lo sconto pagamento: devo ricalcolare il totale documento
        oCleBoll.bCallFromFdin = True
        If oCleBoll.ApriDoc(strDittaCorrente, False, dtrT!tm_tipork.ToString, NTSCInt(dtrT!tm_anno), dtrT!tm_serie.ToString, NTSCInt(dtrT!tm_numdoc), ds) Then
          If bConsentiModifCodPagaSc Then ds.Tables("TESTA").Rows(0)!et_scopag = NTSCDec(dsShared.Tables("TESTMAG").Rows(0)!tm_scopag)
          If bConsentiModifFlspinc Then
            ds.Tables("TESTA").Rows(0)!et_flspinc = NTSCStr(dsShared.Tables("TESTMAG").Rows(0)!tm_flspinc)
            ds.Tables("TESTA").Rows(0)!et_speinc = NTSCDec(dsShared.Tables("TESTMAG").Rows(0)!tm_speinc)
          End If
          If Not oCleBoll.SalvaDocumento("U") Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740018036457000, "Il ricalcolo del documento numero |" & dtrT!tm_numdoc.ToString & "|, serie '|" & dtrT!tm_serie.ToString & "|' non è avvenuto. Aprire il documento con 'Gestione documenti' e risalvarlo")))
          End If
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128722571666718750, "Il ricalcolo del documento numero |" & dtrT!tm_numdoc.ToString & "|, serie '|" & dtrT!tm_serie.ToString & "|' non è avvenuto. Aprire il documento con 'Gestione documenti' e risalvarlo")))
        End If    'If oCleBoll.ApriDoc(strDittaCorrente, False, dtrT!tm_tipork...
      Next    'For Each dtrT As DataRow In dsShared.Tables("ELENCOBOLLE").Select("tm_tipork <> 'T'")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
#End Region



  Public Overridable Function IsDocumentoEmesso(ByVal strTipoDoc As String) As Boolean
    Try
      Select Case strTipoDoc
        Case "K", "(" : Return False
      End Select
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Sub SetDefaultValueTestmag(ByRef ds As DataSet)
    Try

      '--------------------------------------------------------------
      'imposto i valori di default di movord: con la riga che seguen prendo prima i valori dal database
      oCldFdin.SetTableDefaultValueFromDB("TESTMAG", ds)
      'ora imposto i valori di default diversi da quelli impostati nel database
      With ds.Tables("TESTMAG")
        .Columns("codditt").DefaultValue = strDittaCorrente
        .Columns("tm_serie").DefaultValue = " "
        .Columns("tm_datdoc").DefaultValue = DateTime.Now.ToString
        .Columns("tm_datapag").DefaultValue = DateTime.Now.ToString
        .Columns("tm_acuradi").DefaultValue = " "
        .Columns("tm_alfpar").DefaultValue = " "
        .Columns("tm_subcommeca").DefaultValue = " "
        .Columns("tm_ultagg").DefaultValue = IntSetDate("01/01/1900")
        .Columns("tm_opnome").DefaultValue = oApp.User.Nome
        .Columns("tm_codcfam").DefaultValue = " "
        .Columns("tm_flscdb").DefaultValue = "N"
      End With
      oCldFdin.SetTableDefaultValueFromDB("TESTMAGC", ds)
      With ds.Tables("TESTMAGC")
        .Columns("codditt").DefaultValue = strDittaCorrente
        .Columns("tm_serie").DefaultValue = " "
      End With

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function CheckTimeStamp(ByVal strState As String) As Boolean
    Dim dtTmp As DateTime = Nothing
    Try
      'se è un documento aperto controllo se è stato modificato
      If NTSCDate(dttET.Rows(0)!tm_ultagg) <> oCldFdin.GetTimeStamp(strDittaCorrente, NTSCStr(dttET.Rows(0)!tm_tipork), _
                                                              NTSCInt(dttET.Rows(0)!tm_anno), NTSCStr(dttET.Rows(0)!tm_serie), _
                                                              NTSCInt(dttET.Rows(0)!tm_numdoc)) Then
        If strState = "D" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523808025230000, "Il documento che si cerca di eliminare è stato modificato da un altro utente o sessione. E' possibile solo ripristinare.")))
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523810613114000, "Il documento che si cerca di salvare è stato modificato da un altro utente o sessione. E' possibile solo ripristinare.")))
        End If
        Return False
      End If

      'Controlla le bolle agganciate se sono state modificate
      For Each dtrT As DataRow In dsShared.Tables("ELENCODDT").Rows
        dtTmp = oCldFdin.GetTimeStamp(strDittaCorrente, NTSCStr(dtrT!tm_tipork), NTSCInt(dtrT!tm_anno), NTSCStr(dtrT!tm_serie), NTSCInt(dtrT!tm_numdoc))
        If NTSCDate(dtrT!tm_ultagg) <> dtTmp Then
          If dtTmp.ToShortDateString = NTSCDate(IntSetDate("01/01/1900")).ToShortDateString Then
            If strState <> "D" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128721020971250000, "DDT/Ricevuta fisc. n° |" & IIf(NTSCStr(dtrT!tm_serie).Trim = "", NTSCInt(dtrT!tm_numdoc), NTSCInt(dtrT!tm_numdoc) & "/" & NTSCStr(dtrT!tm_serie)).ToString & "| eliminato dall'archivio. Scollegarlo dalla presente Fattura differita prima di salvare.")))
              Return False
            End If
          Else
            If strState = "D" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128721019705937500, "DDT/Ricevuta fisc. n° |" & IIf(NTSCStr(dtrT!tm_serie).Trim = "", NTSCInt(dtrT!tm_numdoc), NTSCInt(dtrT!tm_numdoc) & "/" & NTSCStr(dtrT!tm_serie)).ToString & "| agganciato alla presente Fattura differita è stato modificato da un altro programma, utente o sessione di Business. Ripristinare e riaprire la Fattura differita prima di eliminarla.")))
            Else
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128721019788281250, "DDT/Ricevuta fisc. n° |" & IIf(NTSCStr(dtrT!tm_serie).Trim = "", NTSCInt(dtrT!tm_numdoc), NTSCInt(dtrT!tm_numdoc) & "/" & NTSCStr(dtrT!tm_serie)).ToString & "| agganciato alla presente Fattura differita è stato modificato da un altro programma, utente o sessione di Business. Scollegare e ricollegare il documento per acquisirne le eventuali modifiche prima di salvare.")))
            End If
            Return False
          End If
        End If
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function ValCodice(ByRef e As DataColumnChangeEventArgs, ByVal strTabella As String, ByVal strDescr As String, _
                                        ByVal strErrorMessage As String, Optional ByVal strTipoCod As String = "N", Optional ByRef dttTmp As DataTable = Nothing) As Boolean
    Dim bOk As Boolean = False
    Dim strTmp As String = ""
    Try
      If strTipoCod = "N" Then
        If NTSCInt(e.ProposedValue) = 0 Then
          If strDescr <> "" Then e.Row(strDescr) = ""
          Return True
        End If
      Else
        If e.ProposedValue.ToString.Trim = "" Then
          If strDescr <> "" Then e.Row(strDescr) = ""
          Return True
        End If
      End If
      bOk = oCldFdin.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, strTabella, strTipoCod, strTmp, dttTmp)
      If bOk = False Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", strErrorMessage))
        Return False
      Else
        If strDescr <> "" Then e.Row(strDescr) = strTmp
      End If
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return False
    End Try
  End Function

  Public Overridable Function GetTipoPag(ByVal strCodpaga As String) As Integer
    Dim dttTmp As DataTable = New DataTable
    Dim bOk As Boolean
    Try
      bOk = oCldFdin.ValCodiceDb(strCodpaga, strDittaCorrente, "PAGA", "N", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        Return NTSCInt(dttTmp.Rows(0)!tb_decpaga)
        dttTmp.Dispose()
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function Validacodpaga(ByVal nCodpaga As Integer, ByVal bInImportDDT As Boolean) As Boolean
    Dim dttTmp As New DataTable
    Try
      If nCodpaga = 0 Then Return True

      oCldFdin.ValCodiceDb(nCodpaga.ToString, strDittaCorrente, "TABPAGA", "N", "", dttTmp)
      'Ricerca lo sconto pagamento
      If bInImportDDT = False Then
        'prendo lo sconto pagamento da tabpaga solo se ho cambiato il codpaga a mano in fdin.
        'in import ddt lo sconto pag viene preso dal primo ddt
        dttET.Rows(0)!tm_scopag = NTSCDec(dttTmp.Rows(0)!tb_scopaga)
      End If
      If dttET.Rows(0)!tm_flspinc.ToString = "S" Then
        dttET.Rows(0)!tm_speinc = NTSCDec(dttTmp.Rows(0)!tb_speinca)
        If NTSCInt(dttET.Rows(0)!tm_valuta) <> 0 Then
          dttET.Rows(0)!tm_speincv = oCldFdin.ConvImpEur(False, NTSCDec(dttTmp.Rows(0)!tb_speinca), _
                                                  NTSCInt(dttET.Rows(0)!tm_valuta), NTSCDate(dttET.Rows(0)!tm_datdoc), _
                                                  NTSCDec(dttET.Rows(0)!tm_cambio))
        End If
      Else
        dttET.Rows(0)!tm_speinc = 0
        dttET.Rows(0)!tm_speincv = 0
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function InizProtocollo() As Boolean
    Dim strAlfp As String = ""
    Try

      If (dttET.Rows(0)!tm_tipork.ToString = "K" Or dttET.Rows(0)!tm_tipork.ToString = "(") And strAutoNumProt = "S" Then
        'Rileva il n. di registro
        nRegiva = oCldFdin.GetNregivaPerNumprot(strDittaCorrente, dttET.Rows(0)!tm_serie.ToString, NTSCInt(dttET.Rows(0)!tm_anno), strAlfp)
        If nRegiva = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130631255784459802, "ATTENZIONE: nella tabella delle numerazioni ditta non è stato indicato il numero del reg. IVA da utilizzare per questo tipo di documento")))
        End If
        If strAlfp = "" Then strAlfp = " "
        'Rileva il n. di protocollo
        If nRegiva > 0 Then
          strAlfpro = strAlfp
          lNumprot = oCldFdin.LegNuma(strDittaCorrente, "P", "A" & strAlfpro & nRegiva.ToString("000"), NTSCInt(dttET.Rows(0)!tm_anno), True)
        End If
        dttET.Rows(0)!tm_nregiva = nRegiva
        dttET.Rows(0)!tm_alfpro = strAlfpro
        dttET.Rows(0)!tm_numprot = lNumprot
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

#Region "Before / AfterUpdate"
  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return
    Dim strErr As String = ""
    Try
      'memorizzo il valore corrente di cella per TESTMAGrlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BeforeColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Function BeforeColUpdate_tm_datdoc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strThrowEvent As String = ""      'contiene tutti gli eventi che devono essere eseguiti dall'ui
    Dim evnt As NTSEventArgs = Nothing
    Try
      If NTSCInt(e.Row!tm_anno) <> NTSCDate(e.ProposedValue).Year Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222102500000, "La data del documento non è inclusa nell'anno indicato")))
        Return True
      End If

      ' If NTSCDate(dttET.Rows(0)!tm_datapag) = NTSCDate(dttET.Rows(0)!tm_datdoc) Then dttET.Rows(0)!tm_datapag = NTSCDate(e.ProposedValue)
      If NTSCDate(dttET.Rows(0)!tm_datapag) = NTSCDate(dttET.Rows(0)!tm_datpar) Or NTSCDate(dttET.Rows(0)!tm_datapag) = NTSCDate(dttET.Rows(0)!tm_datdoc) Then
        dttET.Rows(0)!tm_datapag = CalcolaDataScadenza(NTSCStr(dttET.Rows(0)!tm_tipork), NTSCStr(dttET.Rows(0)!tm_datpar), NTSCDate(e.ProposedValue).ToShortDateString)
      End If

      '---------------------------
      'propongo il cambio della data competenza IVA
      'se da vefadi no, altrimenti darebbe un messaggio per ogni documento
      If NTSCDate(e.ProposedValue).ToShortDateString <> NTSCDate(e.Row!tm_dtcomiva).ToShortDateString Then
        If bVefadi Then
          e.Row!tm_dtcomiva = e.ProposedValue
        Else
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128661946337343750, "Utilizzare la data |" & NTSCDate(e.ProposedValue).ToShortDateString & "| anche come data di competenza IVA?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
            e.Row!tm_dtcomiva = e.ProposedValue
          Else
            If Not IsDate(e.Row!tm_dtcomiva) Then e.Row!tm_dtcomiva = e.ProposedValue
          End If
        End If
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_conto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp1 As DataTable = New DataTable()

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_conto = ""
        Return True
      End If

      'valido il conto, indicando che prevalentemente dovrebbe essere un fornitore
      If Not oCldFdin.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", "", dttEt_conto) Then
        e.ProposedValue = e.Row!tm_conto
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128723664772968750, "Il codice del Cliente o Fornitore inserito non è valido")))
        Return False
      End If

      If Not dttEt_conto Is Nothing Then
        If dttEt_conto.Rows.Count > 0 Then
          '--------------------------------
          'no sottoconti
          If dttEt_conto.Rows(0)!an_tipo.ToString = "S" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101562500, "Il codice del Cliente o Fornitore |" & NTSCInt(e.ProposedValue).ToString & "| non è valido in quanto appartiene ai sottoconti.")))
            e.ProposedValue = e.Row!tm_conto
            Return False
          End If

          If Not CheckPIvaCFisCliente(dttEt_conto) Then
            e.ProposedValue = e.Row!tm_conto
            Return False
          End If

          '--------------------------------
          'memorizzo il conto cliente per la successiva decodifica della destinazione diversa
          lContoCF = NTSCInt(e.ProposedValue)
          e.Row!xx_tipo = dttEt_conto.Rows(0)!an_tipo.ToString
          e.Row!xx_conto = dttEt_conto.Rows(0)!an_descr1.ToString & " " & _
                          dttEt_conto.Rows(0)!an_descr2.ToString & vbCrLf & _
                          dttEt_conto.Rows(0)!an_indir.ToString & vbCrLf & _
                          dttEt_conto.Rows(0)!an_cap.ToString & " " & _
                          dttEt_conto.Rows(0)!an_citta.ToString & " (" & _
                          dttEt_conto.Rows(0)!an_prov.ToString & ")   (" & _
                          dttEt_conto.Rows(0)!an_stato.ToString & ")"

          '--------------------------------
          'azzero comunque la destinaz diversa
          e.Row!tm_coddest = 0
          e.Row!tm_coddest2 = 0

          If IsDocumentoEmesso(dttET.Rows(0)!tm_tipork.ToString) And dttEt_conto.Rows(0)!an_tipo.ToString <> "C" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101718750, "ATTENZIONE: si sta emettendo un documento emesso ad un fornitore")))
          ElseIf IsDocumentoEmesso(dttET.Rows(0)!tm_tipork.ToString) = False And dttEt_conto.Rows(0)!an_tipo.ToString <> "F" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101875000, "ATTENZIONE: si sta emettendo un documento ricevuto ad un cliente")))
          End If

          If strVisNoteConto = "S" And bNew Then
            '--------------------------------------------
            'visualizzare le note del cliente in uscita dal codice conto
            If NTSCStr(dttEt_conto.Rows(0)!an_note2).Trim <> "" Then
              ThrowRemoteEvent(New NTSEventArgs("VNoteConto:" & NTSCStr(dttEt_conto.Rows(0)!an_note2).Trim, ""))
            End If
          End If

        End If    'If dttEt_conto.Rows.Count > 0 Then 
      End If    'If Not dttEt_conto Is Nothing Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_codpaga(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As New DataTable
    Dim strThrowEvent As String = ""      'contiene tutti gli eventi che devono essere eseguiti dall'ui

    Try
      ValCodice(e, "PAGA", "xx_codpaga", oApp.Tr(Me, 127792254824687500, "Codice pagamento |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"), "N", dttTmp)
      If dttTmp.Rows.Count > 0 Then

        '-----------------------------------------------
        'gestisco l'editabilità del campo 'data primo pagamento'
        If NTSCInt(dttTmp.Rows(0)!tb_decpaga.ToString) = 4 Then
          strThrowEvent = strThrowEvent & "edDtPrimoP:S" & "|"
        Else
          e.Row!tm_datapag = CalcolaDataScadenza(NTSCStr(e.Row!tm_tipork), NTSCStr(e.Row!tm_datpar), NTSCDate(e.Row!tm_datdoc).ToShortDateString)
          strThrowEvent = strThrowEvent & "edDtPrimoP:N" & "|"
        End If

        Validacodpaga(NTSCInt(e.ProposedValue), False)
      Else
        strThrowEvent = strThrowEvent & "edDtPrimoP:N" & "|"
        e.Row!tm_scopag = 0
        e.Row!tm_speinc = 0
        e.Row!tm_speincv = 0
      End If

      '-----------------------------------------------
      'avviso l'UI delle operazioni grafiche che devono essere eseguite a causa della validazione del conto
      If strThrowEvent <> "" Then ThrowRemoteEvent(New NTSEventArgs(strThrowEvent.Substring(0, strThrowEvent.Length - 1), ""))

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_scopag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCDec(e.ProposedValue) <> NTSCDec(e.Row!tm_scopag) And bInImportDDT = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128725964890468750, "La variazione dello sconto pagamento comporterà la variazione di tale informazione sui documenti collegati.")))
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_flspinc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCStr(e.ProposedValue) <> NTSCStr(e.Row!tm_flspinc) And bInImportDDT = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129767307117174040, "La variazione del flag 'Add. spese incasso' comporterà la variazione di tale informazione sui documenti collegati.")))
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_coddest(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim bOk As Boolean = False
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_coddest = ""
      Else
        bOk = oCldFdin.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "DESTDIV", "N", strTmp, dttTmp, lContoCF.ToString)
        If bOk = False Then
          If bVefadi Then
            'elaborazione massiva: azzero e non avviso ...
            'probabilmente è il caso del conto fatturazione impostato (che ovviamente non ha le destinaz. diverse)
            e.ProposedValue = 0
            e.Row!xx_coddest = ""
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128725922739375000, "Codice destinazione diversa |" & e.ProposedValue.ToString & "| inesistente (oppure codice cliente/fornitore non impostato - |" & lContoCF.ToString & "|)")))
            Return False
          End If
        Else
          '----------------------------------------------------------------------------------------------------------
          With dttTmp.Rows(0)
            strTmp = NTSCStr(!dd_nomdest).Trim & _
              IIf(NTSCStr(!dd_inddest).Trim <> "", " - " & NTSCStr(!dd_inddest).Trim, "").ToString & _
              IIf(NTSCStr(!dd_locdest).Trim <> "", " - " & NTSCStr(!dd_locdest).Trim, "").ToString
          End With
          '----------------------------------------------------------------------------------------------------------
          e.Row!xx_coddest = strTmp
        End If
      End If
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_coddest2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim bOk As Boolean = False
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_coddest2 = ""
      Else
        bOk = oCldFdin.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "DESTDIV", "N", strTmp, dttTmp, lContoCF.ToString)
        If bOk = False Then
          If bVefadi Then
            'elaborazione massiva: azzero e non avviso ...
            'probabilmente è il caso del conto fatturazione impostato (che ovviamente non ha le destinaz. diverse)
            e.ProposedValue = 0
            e.Row!xx_coddest2 = ""
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222104062500, "Codice destinazione diversa 2 inesistente (oppure codice cliente/fornitore non impostato)")))
            Return False
          End If
        Else
          '----------------------------------------------------------------------------------------------------------
          With dttTmp.Rows(0)
            strTmp = NTSCStr(!dd_nomdest).Trim & _
              IIf(NTSCStr(!dd_inddest).Trim <> "", " - " & NTSCStr(!dd_inddest).Trim, "").ToString & _
              IIf(NTSCStr(!dd_locdest).Trim <> "", " - " & NTSCStr(!dd_locdest).Trim, "").ToString
          End With
          '----------------------------------------------------------------------------------------------------------
          e.Row!xx_coddest2 = strTmp
        End If
      End If
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_abi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!tm_banc1 = ""
        e.Row!tm_cab = "0"
        e.Row!tm_banc2 = ""
      Else
        bOk = oCldFdin.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ABI", "N", strTmp)
        If bOk = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222104531250, "Codice ABI |" & NTSCInt(e.ProposedValue).ToString & "| non corretto")))
          e.ProposedValue = e.Row(e.Column.ColumnName)
          Return False
        Else
          e.Row!tm_banc1 = strTmp
          e.Row!tm_cab = "0"
          e.Row!tm_banc2 = ""
        End If
      End If
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_cab(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!tm_banc2 = ""
      Else
        bOk = oCldFdin.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "CAB", "N", strTmp, Nothing, e.Row!tm_abi.ToString)
        If bOk = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222104687500, "Codice CAB |" & NTSCInt(e.ProposedValue).ToString & "| non corretto")))
          e.ProposedValue = e.Row(e.Column.ColumnName)
          Return False
        Else
          e.Row!tm_banc2 = strTmp
        End If
      End If
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_codcena(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ValCodice(e, "CENA", "xx_codcena", oApp.Tr(Me, 127792255357343750, "Codice centro |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"))
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_codbanc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ValCodice(e, "BANC", "xx_codbanc", oApp.Tr(Me, 127792255474843750, "Codice nostra banca |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"))
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_porto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ValCodice(e, "PORT", "xx_porto", oApp.Tr(Me, 127792255153906250, "Codice porto '|" & e.ProposedValue.ToString & "|' non corretto"), "S")
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_codaspe(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If e.ProposedValue.ToString <> "" Then ValCodice(e, "ASPE", "tm_aspetto", oApp.Tr(Me, 127792255281250000, "Codice aspetto esteriore dei beni |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"))
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_vettor(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ValCodice(e, "VETT", "xx_vettor", oApp.Tr(Me, 127792255031718750, "Codice vettore |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"))
      If NTSCInt(e.ProposedValue) <> 0 Then e.Row!tm_acuradi = "V"

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_vettor2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ValCodice(e, "VETT", "xx_vettor2", oApp.Tr(Me, 127792255442031250, "Codice vettore 2 |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"))
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_caustra(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ValCodice(e, "CAUM", "xx_caustra", oApp.Tr(Me, 127792255113281250, "Causale trasporto |" & NTSCInt(e.ProposedValue).ToString & "| non corretta"))
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_nregiva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strSerieProt As String = ""
    Try
      If (dttET.Rows(0)!tm_tipork.ToString = "K" Or dttET.Rows(0)!tm_tipork.ToString = "(") And strAutoNumProt = "S" Then
        strSerieProt = NTSCStr(dttET.Rows(0)!tm_alfpro)
        If strSerieProt = "" Then strSerieProt = " "
        'Rileva il n. di protocollo
        If NTSCInt(e.ProposedValue) > 0 Then
          lNumprot = oCldFdin.LegNuma(strDittaCorrente, "P", "A" & strSerieProt & NTSCInt(e.ProposedValue).ToString("000"), NTSCInt(dttET.Rows(0)!tm_anno), True)
        Else
          lNumprot = 0
        End If
        nRegiva = NTSCInt(e.ProposedValue)
        dttET.Rows(0)!tm_numprot = lNumprot
      End If
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_alfpro(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strSerieProt As String = ""
    Try
      If (dttET.Rows(0)!tm_tipork.ToString = "K" Or dttET.Rows(0)!tm_tipork.ToString = "(") And strAutoNumProt = "S" Then
        strSerieProt = NTSCStr(e.ProposedValue)
        If strSerieProt = "" Then strSerieProt = " "
        'Rileva il n. di protocollo
        lNumprot = oCldFdin.LegNuma(strDittaCorrente, "P", "A" & strSerieProt & NTSCInt(dttET.Rows(0)!tm_nregiva).ToString("000"), NTSCInt(dttET.Rows(0)!tm_anno), True)

        strAlfpro = strSerieProt
        dttET.Rows(0)!tm_numprot = lNumprot
      End If
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_tm_datpar(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCDate(dttET.Rows(0)!tm_datapag) = NTSCDate(dttET.Rows(0)!tm_datpar) Or NTSCDate(dttET.Rows(0)!tm_datapag) = NTSCDate(dttET.Rows(0)!tm_datdoc) Then
        dttET.Rows(0)!tm_datapag = CalcolaDataScadenza(NTSCStr(dttET.Rows(0)!tm_tipork), NTSCStr(e.ProposedValue), NTSCDate(dttET.Rows(0)!tm_datdoc).ToShortDateString)
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return

    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Function AfterColUpdate_tm_codpaga(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        If NTSCStr(e.Row!tm_datdoc).Trim <> "" Then
          e.Row!tm_datapag = CalcolaDataScadenza(NTSCStr(e.Row!tm_tipork), NTSCStr(e.Row!tm_datpar), NTSCDate(e.Row!tm_datdoc).ToShortDateString)
        End If
      Else
        If GetTipoPag(NTSCInt(e.ProposedValue).ToString) <> 4 Then
          If NTSCStr(e.Row!tm_datdoc).Trim <> "" Then
            e.Row!tm_datapag = CalcolaDataScadenza(NTSCStr(e.Row!tm_tipork), NTSCStr(e.Row!tm_datpar), NTSCDate(e.Row!tm_datdoc).ToShortDateString)
          End If
        End If
      End If

      If bInCalcolaTotali = False Then
        CalcolaTotali(True)
      End If

      'Azzera la variabile di rate scadenze modificate
      bModifCastScad = False
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AfterColUpdate_tm_scopag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If bInCalcolaTotali = False Then
        CalcolaTotali(True)
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AfterColUpdate_tm_flspinc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If bInCalcolaTotali = False Then
        Validacodpaga(NTSCInt(dttET.Rows(0)!tm_codpaga), True)  'in belbmenu.calcolatotali vengono prese le spese indicate in questo documento, per cui al cambio del flag di addebito devo rideterminarle
        CalcolaTotali(True)
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  'sblocca castelletti 
  Public Overridable Function AfterColUpdate_tm_flscdb(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    'sblocca castelletti
    Try
      If bInCalcolaTotali = False Then
        CalcolaTotali(True)
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AfterColUpdate_tm_speincv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(dttET.Rows(0)!tm_valuta) <> 0 Then
        dttET.Rows(0)!tm_speinc = oCldFdin.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttET.Rows(0)!tm_speincv), _
                                                        NTSCInt(dttET.Rows(0)!tm_valuta), NTSCDate(dttET.Rows(0)!tm_datdoc), _
                                                        NTSCDec(dttET.Rows(0)!tm_cambio))
      End If
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AfterColUpdate_tm_speinc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCDec(e.ProposedValue) <> 0 And e.Row!tm_flspinc.ToString <> "S" Then
        e.Row!tm_speincv = 0
        e.ProposedValue = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128665680036250000, "Le spese incasso devono essere a 0 quando è disattivata l'opzione di addebito delle spese incasso.")))
      End If

      If bInCalcolaTotali = False Then
        CalcolaTotali(True)
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AfterColUpdate_tm_bolliv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(dttET.Rows(0)!tm_valuta) <> 0 Then
        dttET.Rows(0)!tm_bolli = oCldFdin.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttET.Rows(0)!tm_bolliv), _
                                                        NTSCInt(dttET.Rows(0)!tm_valuta), NTSCDate(dttET.Rows(0)!tm_datdoc), _
                                                        NTSCDec(dttET.Rows(0)!tm_cambio))
      End If
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AfterColUpdate_tm_bolli(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If dttET.Rows(0)!tm_flscdb.ToString = "S" Then
        If bInCalcolaTotali = False Then
          CalcolaTotali(True)
        End If
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AfterColUpdate_tm_totomagv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(dttET.Rows(0)!tm_valuta) <> 0 Then
        dttET.Rows(0)!tm_totomag = oCldFdin.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttET.Rows(0)!tm_totomagv), _
                                                        NTSCInt(dttET.Rows(0)!tm_valuta), NTSCDate(dttET.Rows(0)!tm_datdoc), _
                                                        NTSCDec(dttET.Rows(0)!tm_cambio))
      End If
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AfterColUpdate_tm_totomag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If dttET.Rows(0)!tm_flscdb.ToString = "S" Then
        If bInCalcolaTotali = False Then
          CalcolaTotali(True)
        End If
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

#End Region

#Region "Gestione DDT: aggiungi / togli"
  Public Overridable Function GetWhereHltmDDT() As String
    Try
      'per zoom dohlTm: prendo tutti i documenti non collegati a fatture (o collegati a questa fattura, se in modifica)
      Return oCldFdin.GetWhereDDT(strDittaCorrente, IsDocumentoEmesso(dttET.Rows(0)!tm_tipork.ToString), strUsaContoFattDoc, _
                            lContoCF, bUsaContoFatt, dttET.Rows(0)!tm_tipork.ToString, NTSCInt(dttET.Rows(0)!tm_anno), _
                            dttET.Rows(0)!tm_serie.ToString, NTSCInt(dttET.Rows(0)!tm_numdoc), _
                            bIgnoraSegueFatturaSuDocRic, dsShared.Tables("ELENCODDT"))

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return ""
    End Try
  End Function

  Public Overridable Function DDTAggiungi(ByRef dttIn As DataTable) As Boolean
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim strCol As String = ""
    Dim bRowAdded As Boolean = False
    Dim nRow As Integer = 0

    Try
      '-------------------------------
      'per ogni ddt carico il record in ELENCODDT ed aggancio il documento
      For Each dtrDDT As DataRow In dttIn.Rows
        nRow += 1
        If Not oCldFdin.GetTestmagDDT(strDittaCorrente, dtrDDT!tm_tipork.ToString, NTSCInt(dtrDDT!tm_anno), _
                                      dtrDDT!tm_serie.ToString, NTSCInt(dtrDDT!tm_numdoc), dttTmp) Then GoTo SkipDDT

        If Not Accconf_CheckBeforecolupdate("BF", NTSCInt(dttTmp.Rows(0)!tm_tipobf)) Then GoTo SkipDDT

        bInImportDDT = True
        If dsShared.Tables("ELENCODDT").Rows.Count = 0 Then
          'Carica il documento selezionato (il primo)
          If Not CaricaTestataBolla(dttTmp.Rows(0)) Then
            GoTo SkipDDT
          End If
          Validacodpaga(NTSCInt(dttET.Rows(0)!tm_codpaga), True)
        Else
          'Verifica i vincoli e le deroghe
          'se chiamato da bevefadi i ddt da collegare sono corretti per forza
          If bVefadi = False Then
            If Not VerificaVincoli(dttTmp.Rows(0), True) Then
              GoTo SkipDDT
            End If
            If Not VerificaDeroghe(dttTmp.Rows(0)) Then
              GoTo SkipDDT
            End If
          End If
        End If    'dsShared.Tables("ELENCODDT").Rows.Count = 0
        bInImportDDT = False

        'If nRow > nNumMaxDDT Then
        '  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128726233559218750, "Attenzione! Con la Fatturazione differita interattiva sono gestibili documenti che riepilogano al massimo |" & nNumMaxDDT & "| DDT/Ricevute fisc.")))
        '  GoTo FINE
        'End If

        '-------------------------------
        'aggiungo il ddt nel datatable
        dsShared.Tables("ELENCODDT").Rows.Add(dsShared.Tables("ELENCODDT").NewRow())
        bRowAdded = True
        For i = 0 To dsShared.Tables("ELENCODDT").Columns.Count - 1
          strCol = dsShared.Tables("ELENCODDT").Columns(i).ColumnName
          If dttTmp.Columns.Contains(strCol) Then
            dsShared.Tables("ELENCODDT").Rows(dsShared.Tables("ELENCODDT").Rows.Count - 1)(strCol) = dttTmp.Rows(0)(strCol)
          End If
        Next

        '-------------------------------
        'confermo tutto
        dsShared.AcceptChanges()
        bRowAdded = False
SkipDDT:
      Next    'For Each dtrDDT As DataRow In dttTmp.Rows
FINE:
      '-------------------------------
      'fine: ricalcolo i totali

      bInImportDDT = False
      CalcolaTotali(True)
      bHasChanges = True  'altrimenti se modifico un DDT in campi che non coinvolgono i totali (come, ad esempio, data comp. ecoomica) la fattura non sembra modificata 

      Return True

    Catch ex As Exception
      If bRowAdded Then
        dsShared.Tables("ELENCODDT").Rows(dsShared.Tables("ELENCODDT").Rows.Count - 1).Delete()
        dsShared.Tables("ELENCODDT").AcceptChanges()
      End If
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      bInImportDDT = False
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function DDTScollega(ByRef dtrDDT As DataRow, ByVal bRicalcolaTotali As Boolean) As Boolean
    'scollego il ddt dalla fattura
    Try
      If dtrDDT Is Nothing Then Return True
      dtrDDT.Delete()
      dsShared.Tables("ELENCODDT").AcceptChanges()
      If bRicalcolaTotali Then CalcolaTotali(True)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function


  Public Overridable Function CaricaTestataBolla(ByRef dtrDDT As DataRow) As Boolean
    '-------------------------
    'CARICA LA TESTATA DELLA 1° BOLLA CHE SARA' LA TESTATA DELLA FATT. DIFF
    Dim evnt As NTSEventArgs = Nothing
    Dim dttTmp As New DataTable
    Dim strTmp As String = ""
    Try
      'Controllo che la data sia anteriore o uguale alla data fattura
      If IsDocumentoEmesso(dttET.Rows(0)!tm_tipork.ToString) And NTSCDate(dtrDDT!tm_datdoc) > NTSCDate(dttET.Rows(0)!tm_datdoc) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128726239924218750, "La data del DDT/Ricevuta fisc. è posteriore alla data della fattura differita. Impossibile collegare questo documento.")))
        Return False
      End If
      If IsDocumentoEmesso(dttET.Rows(0)!tm_tipork.ToString) = False And NTSCDate(dtrDDT!tm_datdoc) > NTSCDate(dttET.Rows(0)!tm_datdoc) Then
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 128726240691875000, "La data del DDT/Ricevuta fisc. è posteriore alla data della fattura differita. Collegare comunque questo documento?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
      End If

      If strUsaContoFattDoc <> "1" Then
        'il test sotto opera correttamente se strUsaContoFattDoc ="1". poi da rivedere/sistemare
        If NTSCInt(dttET.Rows(0)!tm_conto) <> NTSCInt(dtrDDT!tm_conto) Then
          If (strUsaContoFattDoc = "-1" Or strUsaContoFattDoc = "2") And NTSCInt(dtrDDT!tm_contfatt) = NTSCInt(dttET.Rows(0)!tm_conto) Then
            'tutto ok
          Else
            If Not bUsaContoFatt Then 'se è abilitata non controlla la congruenza del conto
              'può verificarsi quando, dopo aver aggiunto un ddt all fattura, entro in modifica e cambio il conto
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129714564945613030, "ATTENZIONE: l'intestatario del DDT è diverso dall'intestatario della fattura. DDT scartato.")))
              Return False
            End If
          End If
        End If
        If strUsaContoFattDoc = "1" Then
          If NTSCInt(dttET.Rows(0)!tm_contfatt) <> NTSCInt(dtrDDT!tm_contfatt) Then
            'può verificarsi quando, dopo aver aggiunto un ddt all fattura, entro in modifica e cambio il conto
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129714564914412967, "ATTENZIONE: il conto fatturazione del DDT è diverso dall'intestatario della fattura. DDT scartato.")))
            Return False
          End If
        End If
      End If    'If strUsaContoFattDoc <> "1" Then

      dttET.Rows(0)!tm_tipobf = dtrDDT!tm_tipobf
      dttET.Rows(0)!tm_codagen = dtrDDT!tm_codagen
      dttET.Rows(0)!tm_codagen2 = dtrDDT!tm_codagen2
      dttET.Rows(0)!tm_controp = dtrDDT!tm_controp
      dttET.Rows(0)!tm_magaz = dtrDDT!tm_magaz
      dttET.Rows(0)!tm_magaz2 = dtrDDT!tm_magaz2
      dttET.Rows(0)!tm_codese = dtrDDT!tm_codese
      dttET.Rows(0)!tm_causale = dtrDDT!tm_causale
      If bAggiornaRiferimSoloSeVuoti Then
        If Not bDaModificaDocumento OrElse NTSCStr(dttET.Rows(0)!tm_riferim).Trim = "" Then dttET.Rows(0)!tm_riferim = dtrDDT!tm_riferim
      Else
        dttET.Rows(0)!tm_riferim = dtrDDT!tm_riferim
      End If
      dttET.Rows(0)!tm_listino = dtrDDT!tm_listino
      dttET.Rows(0)!tm_scont1 = dtrDDT!tm_scont1
      dttET.Rows(0)!tm_scont2 = dtrDDT!tm_scont2
      dttET.Rows(0)!tm_valuta = dtrDDT!tm_valuta
      dttET.Rows(0)!tm_cambio = dtrDDT!tm_cambio
      dttET.Rows(0)!tm_flspinc = dtrDDT!tm_flspinc    'sempre prima di impostare il cod pagamento, altrimenti rischia di non impostare correttamente le spese di incasso
      dttET.Rows(0)!tm_flbolli = dtrDDT!tm_flbolli
      dttET.Rows(0)!tm_codpaga = dtrDDT!tm_codpaga
      dttET.Rows(0)!tm_scopag = dtrDDT!tm_scopag
      dttET.Rows(0)!tm_cup = dtrDDT!tm_cup
      dttET.Rows(0)!tm_cig = dtrDDT!tm_cig
      dttET.Rows(0)!tm_riferimpa = dtrDDT!tm_riferimpa
      oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_codpaga).ToString, strDittaCorrente, "TABPAGA", "N", strTmp, dttTmp)
      If NTSCInt(dttTmp.Rows(0)!tb_decpaga) = 3 Or NTSCInt(dttTmp.Rows(0)!tb_decpaga) = 4 Then
        dttET.Rows(0)!tm_datapag = dtrDDT!tm_datapag
      Else
        dttET.Rows(0)!tm_datapag = CalcolaDataScadenza(NTSCStr(dttET.Rows(0)!tm_tipork), NTSCStr(dttET.Rows(0)!tm_datpar), NTSCDate(dttET.Rows(0)!tm_datdoc).ToShortDateString)
      End If
      dttTmp.Clear()
      If NTSCStr(dttET.Rows(0)!tm_datapag).Trim = "" Then dttET.Rows(0)!tm_datapag = CalcolaDataScadenza(NTSCStr(dttET.Rows(0)!tm_tipork), NTSCStr(dttET.Rows(0)!tm_datpar), NTSCDate(dttET.Rows(0)!tm_datdoc).ToShortDateString)
      If strUsaContoFattDoc = "1" Then
        dttET.Rows(0)!tm_contfatt = dtrDDT!tm_contfatt
      Else
        dttET.Rows(0)!tm_contfatt = 0
      End If
      dttET.Rows(0)!tm_codpaga2 = dtrDDT!tm_codpaga2
      dttET.Rows(0)!tm_datapag2 = dtrDDT!tm_datapag2

      dttET.Rows(0)!tm_commeca = dtrDDT!tm_commeca
      dttET.Rows(0)!tm_subcommeca = dtrDDT!tm_subcommeca

      dttET.Rows(0)!tm_proforma = dtrDDT!tm_proforma
      dttET.Rows(0)!tm_scorpo = dtrDDT!tm_scorpo
      dttET.Rows(0)!tm_annotco = dtrDDT!tm_annotco
      dttET.Rows(0)!tm_codstag = dtrDDT!tm_codstag
      dttET.Rows(0)!tm_caustra = dtrDDT!tm_caustra
      dttET.Rows(0)!tm_aspetto = dtrDDT!tm_aspetto
      dttET.Rows(0)!tm_vettor = dtrDDT!tm_vettor
      dttET.Rows(0)!tm_vettor2 = dtrDDT!tm_vettor2
      dttET.Rows(0)!tm_acuradi = dtrDDT!tm_acuradi
      If bVefadi = False Then
        'direttamente da fdin
        If bIgnoraDestDiv = False Then
          dttET.Rows(0)!tm_coddest = dtrDDT!tm_coddest
          dttET.Rows(0)!tm_coddest2 = dtrDDT!tm_coddest2
        End If
      Else
        'da vefadi: se bIgnoraDestDiv devo riportare la destinaz. solo se in anagra ho impostato la fatturazione per destinazione diversa
        If bIgnoraDestDiv Then
          oCldFdin.ValCodiceDb(NTSCInt(dttET.Rows(0)!tm_conto).ToString, strDittaCorrente, "ANAGRA", "N", strTmp, dttTmp)
          If NTSCStr(dttTmp.Rows(0)!an_fatt) = "D" Then
            dttET.Rows(0)!tm_coddest = dtrDDT!tm_coddest
            dttET.Rows(0)!tm_coddest2 = dtrDDT!tm_coddest2
          End If
          dttTmp.Clear()
        Else
          dttET.Rows(0)!tm_coddest = dtrDDT!tm_coddest
          dttET.Rows(0)!tm_coddest2 = dtrDDT!tm_coddest2
        End If
      End If

      dttET.Rows(0)!tm_porto = dtrDDT!tm_porto
      dttET.Rows(0)!tm_codaspe = dtrDDT!tm_codaspe
      dttET.Rows(0)!tm_codrsta = dtrDDT!tm_codrsta
      dttET.Rows(0)!tm_codport = dtrDDT!tm_codport
      dttET.Rows(0)!tm_codntra = dtrDDT!tm_codntra
      dttET.Rows(0)!tm_codcena = dtrDDT!tm_codcena
      dttET.Rows(0)!tm_abi = dtrDDT!tm_abi
      dttET.Rows(0)!tm_cab = dtrDDT!tm_cab
      dttET.Rows(0)!tm_banc1 = dtrDDT!tm_banc1
      dttET.Rows(0)!tm_banc2 = dtrDDT!tm_banc2
      dttET.Rows(0)!tm_codbanc = dtrDDT!tm_codbanc
      dttET.Rows(0)!tm_autpag = dtrDDT!tm_autpag
      dttET.Rows(0)!tm_codcfam = dtrDDT!tm_codcfam
      dttET.Rows(0)!tm_coddivi = dtrDDT!tm_coddivi
      dttET.Rows(0)!tm_codcli = dtrDDT!tm_codcli

      dttETC.Rows(0)!tm_dianno = dtrDDT!tm_dianno
      dttETC.Rows(0)!tm_dinumero = dtrDDT!tm_dinumero

      '-----------------------------------
      'Mette a posto le note sul documento
      If IsDocumentoEmesso(dttET.Rows(0)!tm_tipork.ToString) Then
        If bRifBolleInNote Then
          dttET.Rows(0)!tm_note = DBNull.Value
        Else
          If strNoteFattDiff = "" Then
            If bNonRiportaNote1DDT = True Then
              If bDaModificaDocumento = False Then dttET.Rows(0)!tm_note = DBNull.Value
            Else
              dttET.Rows(0)!tm_note = dtrDDT!tm_note
            End If
          End If
        End If
      Else
        If bNonRiportaNote1DDT = True Then
          If bDaModificaDocumento = False Then dttET.Rows(0)!tm_note = DBNull.Value
        Else
          dttET.Rows(0)!tm_note = dtrDDT!tm_note
        End If
      End If

      '---------------------------
      'traduco i campi
      If NTSCInt(dtrDDT!tm_tipobf) > 0 Then
        oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_tipobf).ToString, strDittaCorrente, "TABTPBF", "N", strTmp)
        dttET.Rows(0)!xx_tipobf = strTmp
      End If
      If NTSCInt(dtrDDT!tm_codagen) > 0 Then
        oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_codagen).ToString, strDittaCorrente, "TABCAGE", "N", strTmp)
        dttET.Rows(0)!xx_codagen = strTmp
      End If
      If NTSCInt(dtrDDT!tm_codagen2) > 0 Then
        oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_codagen2).ToString, strDittaCorrente, "TABCAGE", "N", strTmp)
        dttET.Rows(0)!xx_codagen2 = strTmp
      End If
      If NTSCInt(dtrDDT!tm_controp) > 0 Then
        oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_controp).ToString, strDittaCorrente, "TABCOVE", "N", strTmp)
        dttET.Rows(0)!xx_controp = strTmp
      End If
      If NTSCInt(dtrDDT!tm_magaz) > 0 Then
        oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_magaz).ToString, strDittaCorrente, "TABMAGA", "N", strTmp)
        dttET.Rows(0)!xx_magaz = strTmp
      End If
      If NTSCInt(dtrDDT!tm_magaz2) > 0 Then
        oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_magaz2).ToString, strDittaCorrente, "TABMAGA", "N", strTmp)
        dttET.Rows(0)!xx_magaz2 = strTmp
      End If
      If NTSCInt(dtrDDT!tm_codese) > 0 Then
        oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_codese).ToString, strDittaCorrente, "TABCIVA", "N", strTmp)
        dttET.Rows(0)!xx_codese = strTmp
      End If
      If NTSCInt(dtrDDT!tm_causale) > 0 Then
        oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_causale).ToString, strDittaCorrente, "TABCAUM", "N", strTmp)
        dttET.Rows(0)!xx_causale = strTmp
      End If
      If NTSCInt(dtrDDT!tm_valuta) > 0 Then
        oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_valuta).ToString, strDittaCorrente, "TABVALU", "N", strTmp)
        dttET.Rows(0)!xx_valuta = strTmp
      End If
      If NTSCInt(dtrDDT!tm_codstag) > 0 Then
        oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_codstag).ToString, strDittaCorrente, "TABSTAG", "N", strTmp)
        dttET.Rows(0)!xx_codstag = strTmp
      End If
      If NTSCInt(dtrDDT!tm_codrsta) > 0 Then
        oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_codrsta).ToString, strDittaCorrente, "TABRSTA", "N", strTmp)
        dttET.Rows(0)!xx_codrsta = strTmp
      End If
      If NTSCInt(dtrDDT!tm_codntra) > 0 Then
        oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_codntra).ToString, strDittaCorrente, "TABNTRA", "N", strTmp)
        dttET.Rows(0)!xx_codntra = strTmp
      End If
      If NTSCStr(dtrDDT!tm_codport).Trim <> "" Then
        oCldFdin.ValCodiceDb(NTSCStr(dtrDDT!tm_codport), strDittaCorrente, "TABMPOR", "S", strTmp)
        dttET.Rows(0)!xx_codport = strTmp
      End If
      If NTSCStr(dtrDDT!tm_codcfam).Trim <> "" Then
        oCldFdin.ValCodiceDb(NTSCStr(dtrDDT!tm_codcfam), strDittaCorrente, "TABCFAM", "S", strTmp)
        dttET.Rows(0)!xx_codcfam = strTmp
      End If
      If NTSCInt(dtrDDT!tm_codpaga2) > 0 Then
        oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_codpaga2).ToString, strDittaCorrente, "TABPAGA", "N", strTmp)
        dttET.Rows(0)!xx_codpaga2 = strTmp
      End If

      '------------------
      'campi tradotti in automatico perchè modificabili
      'If NTSCInt(dtrDDT!tm_caustra) > 0 Then
      '  oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_caustra).ToString, strDittaCorrente, "TABCAUM", "N", strTmp)
      '  dttET.Rows(0)!xx_caustra = strTmp
      'End If
      'If NTSCInt(dtrDDT!tm_vettor) > 0 Then
      '  oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_vettor).ToString, strDittaCorrente, "TABVETT", "N", strTmp)
      '  dttET.Rows(0)!xx_vettor = strTmp
      'End If
      'If NTSCInt(dtrDDT!tm_vettor2) > 0 Then
      '  oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_vettor2).ToString, strDittaCorrente, "TABVETT", "N", strTmp)
      '  dttET.Rows(0)!xx_vettor2 = strTmp
      'End If
      'If NTSCInt(dtrDDT!tm_porto) > 0 Then
      '  oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_porto).ToString, strDittaCorrente, "TABPORT", "N", strTmp)
      '  dttET.Rows(0)!xx_porto = strTmp
      'End If
      'If NTSCInt(dtrDDT!tm_codcena) > 0 Then
      '  oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_codcena).ToString, strDittaCorrente, "TABCENA", "N", strTmp)
      '  dttET.Rows(0)!xx_codcena = strTmp
      'End If
      'If NTSCInt(dtrDDT!tm_codbanc) > 0 Then
      '  oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_codbanc).ToString, strDittaCorrente, "TABBANC", "N", strTmp)
      '  dttET.Rows(0)!xx_codbanc = strTmp
      'End If
      'If NTSCInt(dtrDDT!tm_coddest) > 0 Then
      '  oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_coddest).ToString, strDittaCorrente, "DESTDIV", "N", strTmp, Nothing, dttET.Rows(0)!tm_conto.ToString)
      '  dttET.Rows(0)!xx_coddest = strTmp
      'End If
      'If NTSCInt(dtrDDT!tm_coddest2) > 0 Then
      '  oCldFdin.ValCodiceDb(NTSCInt(dtrDDT!tm_coddest2).ToString, strDittaCorrente, "DESTDIV", "N", strTmp, Nothing, dttET.Rows(0)!tm_conto.ToString)
      '  dttET.Rows(0)!xx_coddest2 = strTmp
      'End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function VerificaVincoli(ByRef dtrDDT As DataRow, ByVal bModif As Boolean) As Boolean
    Dim bVincolo As Boolean = False
    Dim strVincolante As String = ""
    Dim bVincoloValCambi As Boolean = False
    Dim strVincolanteValCambi As String = ""
    Dim evnt As NTSEventArgs = Nothing
    Dim bConfermatoVarValCamb As Boolean = False
    Try
      'Controllo vincolo
      With dttET.Rows(0)
        strVincolante = oApp.Tr(Me, 128726914445625000, "Il DDT/Ricevuta fisc. n° |" & IIf(Trim$(dtrDDT!tm_serie.ToString) = "", dtrDDT!tm_numdoc.ToString, dtrDDT!tm_numdoc.ToString & "/" & dtrDDT!tm_serie.ToString).ToString & "| non può essere aggiunto a questa fattura perchè i seguenti campi differiscono:") & vbCrLf & vbCrLf
        strVincolanteValCambi = oApp.Tr(Me, 128726914578125000, "Il DDT/Ricevuta fisc. |n° " & IIf(Trim$(dtrDDT!tm_serie.ToString) = "", dtrDDT!tm_numdoc.ToString, dtrDDT!tm_numdoc.ToString & "/" & dtrDDT!tm_serie.ToString).ToString & "| non potrebbe essere aggiunto a questa fattura perchè i seguenti campi differiscono:") & vbCrLf & vbCrLf

        If IsDocumentoEmesso(!tm_tipork.ToString) And NTSCDate(dtrDDT!tm_datdoc) > NTSCDate(!tm_datdoc) Then
          bVincolo = True
          strVincolante = strVincolante & oApp.Tr(Me, 128726907491250000, "- Data DDT/Ricevuta fisc. superiore alla data della fattura diff.") & vbCrLf
        End If
        If NTSCInt(dtrDDT!tm_conto) <> NTSCInt(!tm_conto) Then
          If (strUsaContoFattDoc = "-1" Or strUsaContoFattDoc = "2") And NTSCInt(dtrDDT!tm_contfatt) = NTSCInt(!tm_conto) Then
            'tutto ok
          Else
            If Not bUsaContoFatt Then 'se è abilitata non controlla la congruenza del conto
              bVincolo = True
              strVincolante = strVincolante & oApp.Tr(Me, 128726907481562500, "- Codice Conto cliente/fornitore") & vbCrLf
            End If
          End If
        End If
        If strUsaContoFattDoc = "1" Then
          If NTSCInt(dtrDDT!tm_contfatt) <> NTSCInt(!tm_contfatt) Then
            bVincolo = True
            strVincolante = strVincolante & oApp.Tr(Me, 128726907456875000, "- Codice conto fatturazione ") & vbCrLf
          End If
        End If
        If NTSCInt(dtrDDT!tm_codese) <> NTSCInt(!tm_codese) Then
          bVincolo = True
          strVincolante = strVincolante & oApp.Tr(Me, 128726907444375000, "- Codice esenzione") & vbCrLf
        End If
        If Not (bForzaValuteCambiDiv) Then
          If NTSCInt(dtrDDT!tm_valuta) <> NTSCInt(!tm_valuta) Then
            'bVincolo = True
            'strVincolante = strVincolante & "- Codice valuta") & vbCrLf
            bVincoloValCambi = True
            strVincolanteValCambi = strVincolanteValCambi & oApp.Tr(Me, 128726907413593750, "- Codice valuta") & vbCrLf
          End If
          If NTSCDec(dtrDDT!tm_cambio) <> NTSCDec(!tm_cambio) Then
            'bVincolo = True
            'strVincolante = strVincolante & "- Cambio valuta") & vbCrLf
            bVincoloValCambi = True
            strVincolanteValCambi = strVincolanteValCambi & oApp.Tr(Me, 128726907405625000, "- Cambio valuta") & vbCrLf
          End If
        End If
        If NTSCInt(dtrDDT!tm_codagen) <> NTSCInt(!tm_codagen) Then
          bVincolo = True
          strVincolante = strVincolante & oApp.Tr(Me, 128726907377812500, "- Codice agente") & vbCrLf
        End If
        If NTSCInt(dtrDDT!tm_codagen2) <> NTSCInt(!tm_codagen2) Then
          bVincolo = True
          strVincolante = strVincolante & oApp.Tr(Me, 128726907345937500, "- Codice subagente") & vbCrLf
        End If
        If Not bConsentiModifCodPagaSc Then
          'Rimangono dei vincoli come in passato
          If NTSCInt(dtrDDT!tm_codpaga) <> NTSCInt(!tm_codpaga) Then
            bVincolo = True
            strVincolante = strVincolante & oApp.Tr(Me, 128726907309843750, "- Codice pagamento") & vbCrLf
          End If
          If NTSCDec(dtrDDT!tm_scopag) <> NTSCDec(!tm_scopag) Then
            bVincolo = True
            strVincolante = strVincolante & oApp.Tr(Me, 128726907298437500, "- Sconto pagamento") & vbCrLf
          End If
          'Vincolo Gestione Punti Vendita
          If NTSCInt(dtrDDT!tm_codpaga2) <> NTSCInt(!tm_codpaga2) Then
            bVincolo = True
            strVincolante = strVincolante & oApp.Tr(Me, 129567513287500000, "- Codice pagamento 2") & vbCrLf
          End If
        End If
        If NTSCDec(dtrDDT!tm_scont1) <> NTSCDec(!tm_scont1) Then
          bVincolo = True
          strVincolante = strVincolante & oApp.Tr(Me, 128726907287031250, "- Sconto 1 di testata") & vbCrLf
        End If
        If NTSCDec(dtrDDT!tm_scont2) <> NTSCDec(!tm_scont2) Then
          bVincolo = True
          strVincolante = strVincolante & oApp.Tr(Me, 128726907260781250, "- Sconto 2 di testata") & vbCrLf
        End If
        If (dtrDDT!tm_scorpo.ToString = "S" And !tm_scorpo.ToString = "N") Or (dtrDDT!tm_scorpo.ToString = "N" And !tm_scorpo.ToString = "S") Then
          bVincolo = True
          strVincolante = strVincolante & oApp.Tr(Me, 128726907243437500, "- Documento con scorporo") & vbCrLf
        End If
        If (dtrDDT!tm_proforma.ToString = "S" And !tm_proforma.ToString = "N") Or (dtrDDT!tm_proforma.ToString = "N" And !tm_proforma.ToString = "S") Then
          bVincolo = True
          strVincolante = strVincolante & oApp.Tr(Me, 128726907228906250, "- Documento Proforma") & vbCrLf
        End If
        If Not bConsentiModifFlspinc Then
          If (dtrDDT!tm_flspinc.ToString = "S" And !tm_flspinc.ToString = "N") Or (dtrDDT!tm_flspinc.ToString = "N" And !tm_flspinc.ToString = "S") Then
            bVincolo = True
            strVincolante = strVincolante & oApp.Tr(Me, 128726907214843750, "- Opzione Addebita spese incasso") & vbCrLf
          End If
        End If
        If (dtrDDT!tm_flbolli.ToString = "S" And !tm_flbolli.ToString = "N") Or (dtrDDT!tm_flbolli.ToString = "N" And !tm_flbolli.ToString = "S") Then
          bVincolo = True
          strVincolante = strVincolante & oApp.Tr(Me, 128726907203281250, "- Opzione Addebita spese bolli") & vbCrLf
        End If
        
        If dtrDDT!tm_flfatt.ToString = "S" Then
          If dtrDDT!tm_tiporkfat.ToString <> !tm_tipork.ToString Or _
             NTSCInt(dtrDDT!tm_annfat) <> NTSCInt(!tm_anno) Or _
             dtrDDT!tm_alffat.ToString <> !tm_serie.ToString Or _
             NTSCInt(dtrDDT!tm_numfat) <> NTSCInt(!tm_numdoc) Then
            bVincolo = True
            strVincolante = strVincolante & oApp.Tr(Me, 128726907191093750, "- DDT/Ricevuta fisc. già collegato alla Fatt. n° |" & IIf(Trim$(dtrDDT!tm_alffat.ToString) = "", dtrDDT!tm_numfat.ToString, dtrDDT!tm_numfat.ToString & "/" & dtrDDT!tm_alffat.ToString).ToString & "| del |" & NTSCDate(dtrDDT!tm_datfat).ToShortDateString & "| ") & vbCrLf
          End If
        End If

        'Vincolo Dichiarazioni di intento
        If NTSCInt(dtrDDT!tm_dianno) <> NTSCInt(dttETC.Rows(0)!tm_dianno) Or NTSCInt(dtrDDT!tm_dinumero) <> NTSCInt(dttETC.Rows(0)!tm_dinumero) Then
          bVincolo = True
          strVincolante = strVincolante & oApp.Tr(Me, 129567513287500001, "- Anno/Numero Dichiarazione di intento") & vbCrLf
        End If

        If NTSCStr(dtrDDT!tm_cup).Trim <> NTSCStr(!tm_cup).Trim Then
          bVincolo = True
          strVincolante = strVincolante & oApp.Tr(Me, 130473808034313343, "- Codice CUP") & vbCrLf
        End If
        If NTSCStr(dtrDDT!tm_cig).Trim <> NTSCStr(!tm_cig).Trim Then
          bVincolo = True
          strVincolante = strVincolante & oApp.Tr(Me, 130473808148483101, "- Codice CIG") & vbCrLf
        End If
        If NTSCStr(dtrDDT!tm_riferimpa).Trim <> NTSCStr(!tm_riferimpa).Trim Then
          bVincolo = True
          strVincolante = strVincolante & oApp.Tr(Me, 130473808519733903, "- Riferimento ordine PA") & vbCrLf
        End If

        ' controlla vincoli cambi valuta
        If bVincoloValCambi Then
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, strVincolanteValCambi & vbCrLf & oApp.Tr(Me, 128726913667187500, " Forzare la gestione di documenti con cambi/valute diverse ? (Il documento sarà generato come documento in sola moneta principale) "))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
            bConfermatoVarValCamb = True
          Else
            Return False  ' non segnala neanche i rimanenti ...
          End If
        End If
        ' controlla altri vincoli ...

      End With    'With dttET.Rows(0)

      If bVincolo Then
        If Not bModif Then
          ThrowRemoteEvent(New NTSEventArgs("", strVincolante))
          Return False
        Else
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, strVincolante & vbCrLf & _
                                  oApp.Tr(Me, 128726987195312500, "Entrare in modifica del DDT/Ricevuta fisc.?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
            ThrowRemoteEvent(New NTSEventArgs("ApriDocum.:" & _
                                              dtrDDT!tm_tipork.ToString & "§" & _
                                              NTSCInt(dtrDDT!tm_anno).ToString & "§" & _
                                              dtrDDT!tm_serie.ToString & "§" & _
                                              NTSCInt(dtrDDT!tm_numdoc).ToString, ""))
          End If
          Return False
        End If
      End If
      ' se ho confermato una modifica a cambi/valuta, faacio gli opportunie azioni ...
      If bConfermatoVarValCamb Then
        dttET.Rows(0)!tm_valuta = 0
        dttET.Rows(0)!xx_valuta = ""
        bForzaValuteCambiDiv = True
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function VerificaDeroghe(ByRef dtrDDT As DataRow) As Boolean
    Dim evnt As NTSEventArgs = Nothing
    Dim bDeroga As Boolean = False
    Dim strDerogabile As String = ""
    Dim strDocumento As String = ""
    Dim dttTmp As New DataTable

    Try
      bDeroga = False
      With dttET.Rows(0)
        '------------------------------------------------------------------------------------------------------------
        Select Case NTSCStr(dtrDDT!tm_tipork)
          Case "(" : strDocumento = oApp.Tr(Me, 129838806559885816, "Nota di Accredito Differita Ricevuta")
          Case "£" : strDocumento = oApp.Tr(Me, 129838806583910176, "Nota di Accredito Differita Emessa")
          Case "A" : strDocumento = oApp.Tr(Me, 129838806599858054, "Fattura Immediata Emessa")
          Case "B" : strDocumento = oApp.Tr(Me, 129838806618374390, "D.D.T. Emesso")
          Case "C" : strDocumento = oApp.Tr(Me, 129838806633921862, "Corrispettivo Emesso")
          Case "D" : strDocumento = oApp.Tr(Me, 129838806649928336, "Fattura Differita Emessa")
          Case "E" : strDocumento = oApp.Tr(Me, 129838806667233688, "Nota di Addebito Emessa")
          Case "F" : strDocumento = oApp.Tr(Me, 129838806682410052, "Ricevuta Fiscale Emessa")
          Case "I" : strDocumento = oApp.Tr(Me, 129838806697420394, "Riemissione Ricevuta Fiscale")
          Case "J" : strDocumento = oApp.Tr(Me, 129838806712118224, "Nota di Accredito Ricevuta")
          Case "K" : strDocumento = oApp.Tr(Me, 129838806729531002, "Fattura Differita Ricevuta")
          Case "L" : strDocumento = oApp.Tr(Me, 129838806747373484, "Fattura Immediata Ricevuta")
          Case "M" : strDocumento = oApp.Tr(Me, 129838806763145574, "D.D.T. Ricevuto")
          Case "N" : strDocumento = oApp.Tr(Me, 129838806778995792, "Nota di Accredito Emessa")
          Case "P" : strDocumento = oApp.Tr(Me, 129838806795392906, "Fattura/Ricevuta Fiscale Differita")
          Case "S" : strDocumento = oApp.Tr(Me, 129838806812610364, "Fattura/Ricevuta Fiscale Emessa")
          Case "T" : strDocumento = oApp.Tr(Me, 129838806827366790, "Carico da Produzione")
          Case "U" : strDocumento = oApp.Tr(Me, 129838806843119348, "Scarico a Produzione")
          Case "W" : strDocumento = oApp.Tr(Me, 129838806871245428, "Nota di Prelievo")
          Case "Z" : strDocumento = oApp.Tr(Me, 129838806887994118, "Bolla di Movimentazione Interna")
        End Select
        '------------------------------------------------------------------------------------------------------------
        strDerogabile = oApp.Tr(Me, 128727010314218750, "Attenzione!" & vbCrLf & _
          "I seguenti campi del documento di tipo" & vbCrLf & _
          " --> |" & strDocumento & "| N° |" & dtrDDT!tm_numdoc.ToString & IIf(dtrDDT!tm_serie.ToString.Trim <> "", "/" & dtrDDT!tm_serie.ToString, "").ToString.ToString & "|" & vbCrLf & _
          "differiscono da quelli del documento in corso:") & vbCrLf & vbCrLf

        '------------------------------------------------------------------------
        'Controllo deroga
        If NTSCInt(dtrDDT!tm_anno) <> NTSCInt(!tm_anno) Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 128727010295156250, "- Anno DDT/Ricevuta fisc.") & vbCrLf
        End If
        If IsDocumentoEmesso(!tm_tipork.ToString) = False And (NTSCDate(dtrDDT!tm_datdoc) > NTSCDate(!tm_datdoc)) Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 128727010269843750, "- Data DDT/Ricevuta fisc. superiore alla data della fattura diff.") & vbCrLf
        End If
        If (NTSCStr(dttET.Rows(0)!tm_tipork).ToUpper = "K") And _
           (NTSCStr(dtrDDT!tm_tipork).ToUpper = "B") And _
           (bStornaDDTResoForn = True) Then
        Else
          If NTSCInt(dtrDDT!tm_tipobf) <> NTSCInt(!tm_tipobf) Then
            bDeroga = True
            Select Case bNoUpdateTipobf
              Case False : strDerogabile = strDerogabile & oApp.Tr(Me, 128727010255468750, "- (*) Codice Tipo Bolla/Fattura") & vbCrLf
              Case True : strDerogabile = strDerogabile & oApp.Tr(Me, 128727010241093750, "- Codice Tipo Bolla/Fattura") & vbCrLf
            End Select
          End If
          'Decorrenza
          oCldFdin.ValCodiceDb(dtrDDT!tm_codpaga.ToString, strDittaCorrente, "TABPAGA", "N", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            If NTSCInt(dttTmp.Rows(0)!tb_decpaga) = 4 Or NTSCInt(dttTmp.Rows(0)!tb_decpaga) = 3 Then
              If NTSCDate(dtrDDT!tm_datapag) <> NTSCDate(!tm_datapag) Then
                bDeroga = True
                strDerogabile = strDerogabile & oApp.Tr(Me, 128727010228281250, "- (*) Data decorrenza 1° pagamento") & vbCrLf
              End If
            End If
          End If
          dttTmp.Clear()
          dttTmp.Dispose()
          'Abi/cab/banc1 e banc2
          If NTSCInt(dtrDDT!tm_abi) <> NTSCInt(!tm_abi) Or NTSCInt(dtrDDT!tm_cab) <> NTSCInt(!tm_cab) Or NTSCStr(dtrDDT!tm_banc1) <> NTSCStr(!tm_banc1) Or NTSCStr(dtrDDT!tm_banc2) <> NTSCStr(!tm_banc2) Then
            bDeroga = True
            strDerogabile = strDerogabile & oApp.Tr(Me, 128727010204843750, "- (*) Codice ABI e/o CAB e/o descr. banca") & vbCrLf
          End If
          If NTSCInt(dtrDDT!tm_causale) <> NTSCInt(!tm_causale) Then
            bDeroga = True
            strDerogabile = strDerogabile & oApp.Tr(Me, 128727010038750000, "- Codice Causale magazzino") & vbCrLf
          End If
        End If
        'Flag aut. incassi
        If (dtrDDT!tm_autpag.ToString = "S" And !tm_autpag.ToString = "N") Or (dtrDDT!tm_autpag.ToString <> "S" And !tm_autpag.ToString = "S") Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 128727010177031250, "- Flag autorizzazione pagamento") & vbCrLf
        End If
        'INTRA
        If NTSCInt(dtrDDT!tm_codrsta) <> NTSCInt(!tm_codrsta) Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 128727010160468750, "- Codice Regime statistico IntraSTAT") & vbCrLf
        End If
        If NTSCStr(dtrDDT!tm_codport) <> NTSCStr(!tm_codport) Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 128727010133593750, "- Regime trasporto") & vbCrLf
        End If
        If NTSCInt(dtrDDT!tm_codntra) <> NTSCInt(!tm_codntra) Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 128727010120781250, "- Codice Natura transazione IntraSTAT") & vbCrLf
        End If

        If NTSCStr(dtrDDT!tm_porto) <> NTSCStr(!tm_porto) Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 128727010107656250, "- Codice Porto") & vbCrLf
        End If
        If Not bIgnoraDestDiv Then
          If NTSCInt(dtrDDT!tm_coddest) <> NTSCInt(!tm_coddest) Then
            bDeroga = True
            strDerogabile = strDerogabile & oApp.Tr(Me, 128727010095156250, "- Codice destinazione 1") & vbCrLf
          End If
          If NTSCInt(dtrDDT!tm_coddest2) <> NTSCInt(!tm_coddest2) Then
            bDeroga = True
            strDerogabile = strDerogabile & oApp.Tr(Me, 128727010083906250, "- Codice destinazione 2") & vbCrLf
          End If
        End If
        If NTSCInt(dtrDDT!tm_commeca) <> NTSCInt(!tm_commeca) Or NTSCStr(dtrDDT!tm_subcommeca) <> NTSCStr(!tm_subcommeca) Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 128727010051406250, "- Codice Commessa e/o Subcommessa") & vbCrLf
        End If
        If NTSCInt(dtrDDT!tm_coddivi) <> NTSCInt(!tm_coddivi) Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 129280005127031250, "- Codice Divisione") & vbCrLf
        End If
        If NTSCInt(dtrDDT!tm_codcli) <> NTSCInt(!tm_codcli) Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 129280005109511719, "- Codice cliente CA") & vbCrLf
        End If
        If NTSCInt(dtrDDT!tm_magaz) <> NTSCInt(!tm_magaz) Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 128727010025937500, "- Codice 1° magazzino") & vbCrLf
        End If
        If NTSCInt(dtrDDT!tm_magaz2) <> NTSCInt(!tm_magaz2) Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 128727009993593750, "- Codice 2° magazzino") & vbCrLf
        End If
        If NTSCInt(dtrDDT!tm_codcena) <> NTSCInt(!tm_codcena) Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 128727009976093750, "- Codice Centro di C.A.") & vbCrLf
        End If
        If NTSCInt(dtrDDT!tm_codbanc) <> NTSCInt(!tm_codbanc) Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 128727009964218750, "- Codice ns. banca") & vbCrLf
        End If
        If NTSCInt(dtrDDT!tm_codpaga2) <> NTSCInt(!tm_codpaga2) Then
          bDeroga = True
          strDerogabile = strDerogabile & oApp.Tr(Me, 129588180562255859, "- (*) Codice pagamento 2") & vbCrLf
        End If
        If bConsentiModifCodPagaSc Then
          'Se consetita la modifica significa che codpaga e scopag sono derogabili
          If NTSCInt(dtrDDT!tm_codpaga) <> NTSCInt(!tm_codpaga) Then
            bDeroga = True
            strDerogabile = strDerogabile & oApp.Tr(Me, 128727009952343750, "- (*) Codice pagamento") & vbCrLf
          End If
          If NTSCDec(dtrDDT!tm_scopag) <> NTSCDec(!tm_scopag) Then
            bDeroga = True
            strDerogabile = strDerogabile & oApp.Tr(Me, 128727009939375000, "- (*) Sconto pagamento") & vbCrLf
          End If
        End If
      End With    'With dttET.Rows(0)

      If bDeroga Then
        Select Case NTSCStr(dttET.Rows(0)!tm_tipork)
          Case "(" : strDocumento = oApp.Tr(Me, 129838806992695404, "Nota di Accredito Differita Ricevuta")
          Case "£" : strDocumento = oApp.Tr(Me, 129838807007451830, "Nota di Accredito Differita Emessa")
          Case "A" : strDocumento = oApp.Tr(Me, 129838807023223920, "Fattura Immediata Emessa")
          Case "B" : strDocumento = oApp.Tr(Me, 129838807037921750, "D.D.T. Emesso")
          Case "C" : strDocumento = oApp.Tr(Me, 129838807053303200, "Corrispettivo Emesso")
          Case "D" : strDocumento = oApp.Tr(Me, 129838807070667148, "Fattura Differita Emessa")
          Case "E" : strDocumento = oApp.Tr(Me, 129838807086370876, "Nota di Addebito Emessa")
          Case "F" : strDocumento = oApp.Tr(Me, 129838807101410516, "Ricevuta Fiscale Emessa")
          Case "I" : strDocumento = oApp.Tr(Me, 129838807118344760, "Riemissione Ricevuta Fiscale")
          Case "J" : strDocumento = oApp.Tr(Me, 129838807133569954, "Nota di Accredito Ricevuta")
          Case "K" : strDocumento = oApp.Tr(Me, 129838807148541232, "Fattura Differita Ricevuta")
          Case "L" : strDocumento = oApp.Tr(Me, 129838807163698064, "Fattura Immediata Ricevuta")
          Case "M" : strDocumento = oApp.Tr(Me, 129838807180505350, "D.D.T. Ricevuto")
          Case "N" : strDocumento = oApp.Tr(Me, 129838807197459126, "Nota di Accredito Emessa")
          Case "P" : strDocumento = oApp.Tr(Me, 129838807213114024, "Fattura/Ricevuta Fiscale Differita")
          Case "S" : strDocumento = oApp.Tr(Me, 129838807228807986, "Fattura/Ricevuta Fiscale Emessa")
          Case "T" : strDocumento = oApp.Tr(Me, 129838807243740200, "Carico da Produzione")
          Case "U" : strDocumento = oApp.Tr(Me, 129838807258721244, "Scarico a Produzione")
          Case "W" : strDocumento = oApp.Tr(Me, 129838807274229652, "Nota di Prelievo")
          Case "Z" : strDocumento = oApp.Tr(Me, 129838807289181398, "Bolla di Movimentazione Interna")
        End Select
        If Microsoft.VisualBasic.InStr(strDerogabile, "(*)") <> 0 Then
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, strDerogabile & vbCrLf & _
            oApp.Tr(Me, 129838792740712602, "Forzando l'aggiunta del documento selezionato al tipo:" & vbCrLf & _
            " --> |" & strDocumento & "|" & vbCrLf & _
            "i campi contrassegnati con (*) saranno modificati sui documenti agganciati a questa fattura." & vbCrLf & _
            "Proseguire?"))
        Else
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, strDerogabile & vbCrLf & _
            oApp.Tr(Me, 129838802556235988, "Forzare l'aggiunta del documento selezionato al tipo:" & vbCrLf & _
              " --> |" & strDocumento & "|?"))
        End If
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
          Return False
        End If
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
#End Region

  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChanges
    End Get
  End Property

  Public Overridable Function LegNuma(ByVal strTipo As String, ByVal strSerie As String, ByVal nAnno As Integer) As Integer
    Dim strTipoProg As String = ""
    Try
      strTipoProg = strTipo
      Select Case strTipo
        Case "D", "£" : strTipoProg = "A"
        Case "K", "(" : strTipoProg = "L"
        Case "P" : strTipoProg = "S"
      End Select
      Return oCldFdin.LegNuma(strDittaCorrente, strTipoProg, strSerie, nAnno, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggNuma() As Integer
    Dim strDalMessage As String = ""      'messaggio restituito dal DAL per informazioni durante la sua elaborazione
    Dim lProgr As Integer = 0
    Dim i As Integer = 0
    Dim strTipoProg As String = ""
    Try

      strTipoProg = dttET.Rows(0)!tm_tipork.ToString
      Select Case dttET.Rows(0)!tm_tipork.ToString
        Case "D", "£" : strTipoProg = "A"
        Case "K", "(" : strTipoProg = "L"
        Case "P" : strTipoProg = "S"
      End Select

      lProgr = oCldFdin.AggNuma(strDittaCorrente, strTipoProg, dttET.Rows(0)!tm_serie.ToString, _
                                NTSCInt(dttET.Rows(0)!tm_anno), NTSCInt(dttET.Rows(0)!tm_numdoc), _
                                True, True, strDalMessage)
      If strDalMessage <> "" Then
        '-----------------------------------------------
        'giro all'ui il messaggio ricevuto dal DAL
        ThrowRemoteEvent(New NTSEventArgs("", strDalMessage))
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function CambiaNumdoc(ByRef ds As DataSet, ByVal lNewProgr As Integer) As Boolean
    '--------------------------------
    'cambio il numero documento
    Dim i As Integer = 0
    Try

      If lNewProgr <> 0 Then ds.Tables("TESTMAG").Rows(0)!tm_numdoc = lNewProgr
      If lNewProgr <> 0 Then ds.Tables("TESTMAGC").Rows(0)!tm_numdoc = lNewProgr

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function ApriScadenze() As Boolean
    Dim i As Integer = 0
    Dim dttTmp As New DataTable

    Try
      '----------------------
      'Modifica scadenze
      If NTSCInt(dttET.Rows(0)!tm_codpaga) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128669122852656250, "Inserire il codice pagamento prima di modificare le rate delle scadenze.")))
        For i = 1 To 8
          dttET.Rows(0)("tm_tippaga_" & i.ToString) = 0
          dttET.Rows(0)("tm_datsca_" & i.ToString) = DBNull.Value
          dttET.Rows(0)("tm_impsca_" & i.ToString) = 0
          dttET.Rows(0)("tm_impscav_" & i.ToString) = 0
        Next
        Return False
      End If

      '----------------------
      'Controlla che il documento non sia da riepilogare
      oCldFdin.ValCodiceDb(NTSCInt(dttET.Rows(0)!tm_tipobf).ToString, strDittaCorrente, "TABTPBF", "N", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128669125857031250, "Il codice Tipo Bolla/Fattura inserito non è valido.")))
        For i = 1 To 8
          dttET.Rows(0)("tm_tippaga_" & i.ToString) = 0
          dttET.Rows(0)("tm_datsca_" & i.ToString) = DBNull.Value
          dttET.Rows(0)("tm_impsca_" & i.ToString) = 0
          dttET.Rows(0)("tm_impscav_" & i.ToString) = 0
        Next
        Return False
      End If

      '-------------------------
      'eseguo il calcolo standard delle scadenze sull'importo residuo da pagare
      If Not CalcolaScadenzeStandard(True) Then Return False

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function CalcolaScadenzeStandard(ByVal bVisMessageSaldo0 As Boolean) As Boolean
    'IDENTICA A BEVEBOLL.CalcolaScadenzeStandard, solo che il prefisso tabelle è TM_ invece che tm_
    '                                             e per documenti ricevuti testa tipork = K invece che J o L
    Dim dRim As Decimal = 0
    Dim dRimv As Decimal = 0
    Dim dTotimp As Decimal = 0
    Dim dTotimpv As Decimal = 0
    Dim strDataPartenza As String = ""
    Dim nRate As Integer = 0
    Dim strErr As String = ""
    Dim P As CLELBMENU.ParamCalcScad = New CLELBMENU.ParamCalcScad
    Dim nTiprata(60) As Integer ' integra paramcalcscad...
    Dim i As Integer = 0
    Dim dttTmp As New DataTable

    Try
      With dttET.Rows(0)

        dRim = NTSCDec(!tm_totdoc) - NTSCDec(!tm_pagato) - NTSCDec(!tm_pagato2) + NTSCDec(!tm_resto) - NTSCDec(!tm_abbuono) - NTSCDec(!tm_totomag)
        dRimv = NTSCDec(!tm_totdocv) - NTSCDec(!tm_pagatov) - NTSCDec(!tm_abbuonov) - NTSCDec(!tm_totomagv)

        'Si entra solo se Rimanenza/rimanenzav > 0
        If dRim = 0 Then
          For i = 1 To 8
            dttET.Rows(0)("tm_tippaga_" & i.ToString) = 0
            dttET.Rows(0)("tm_datsca_" & i.ToString) = DBNull.Value
            dttET.Rows(0)("tm_impsca_" & i.ToString) = 0
            dttET.Rows(0)("tm_impscav_" & i.ToString) = 0
          Next
          If bVisMessageSaldo0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128669130316250000, "La modifica delle rate di scadenza è consentita solo documenti dove la rimanenza a pagare è maggiore di zero.")))
            Return False
          Else
            Return True
          End If
        End If
        '----------------------
        'La data di partenza è sempre = Data documento (tranne se J, K, L, M, T, ( dove usa tm_datpar, a meno di deroga con opzione di registro)
        'se il 'Tipo scadenza' del pagamento è 'Data diversa'
        'ed esiste la 'Data 1° pagamento', parte da quella
        strDataPartenza = CalcolaDataScadenza(NTSCStr(dttET.Rows(0)!tm_tipork), NTSCStr(dttET.Rows(0)!tm_datpar), NTSCDate(dttET.Rows(0)!tm_datdoc).ToShortDateString)

        oCldFdin.ValCodiceDb(NTSCInt(dttET.Rows(0)!tm_codpaga).ToString, strDittaCorrente, "TABPAGA", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          If (NTSCInt(dttTmp.Rows(0)!tb_decpaga) = 4 Or NTSCInt(dttTmp.Rows(0)!tb_decpaga) = 3) And NTSCStr(dttET.Rows(0)!tm_datapag).Trim <> "" Then
            strDataPartenza = NTSCDate(dttET.Rows(0)!tm_datapag).ToShortDateString
          End If
        End If
        dttTmp.Clear()

        '----------------------
        'determino il totale IVA
        For i = 1 To 8
          dTotimp += NTSCDec(dttET.Rows(0)("tm_imposta_" & i.ToString))
          dTotimpv += NTSCDec(dttET.Rows(0)("tm_impostav_" & i.ToString))
        Next

        '----------------------
        'se documento ricevuto extracee ricalcolo le scadenze senza passare l'iva al tot. documento
        If dttET.Rows(0)!tm_tipork.ToString = "K" Or dttET.Rows(0)!tm_tipork.ToString = "L" Or _
            dttET.Rows(0)!tm_tipork.ToString = "J" Or dttET.Rows(0)!tm_tipork.ToString = "(" Then

          If NTSCInt(!tm_codntra) <> 0 Or NTSCStr(!tm_intra) <> "N" Then
            'Sui doc. ricevuti INTRA toglie l'iva dal calcolo scadenze
            'ricalcolo il totale imponibile
            dRim -= dTotimp
            dRimv -= dTotimpv
          Else
            oCldFdin.ValCodiceDb(dttET.Rows(0)!tm_tipobf.ToString, strDittaCorrente, "TABTPBF", "N", "", dttTmp)
            If NTSCStr(dttTmp.Rows(0)!tb_fattrevch) = "S" Then
              dRim -= dTotimp
              dRimv -= dTotimpv
            ElseIf NTSCStr(dttTmp.Rows(0)!tb_fattrevch) = "M" Then
              'reverse charge misto: devo togliere solo l'iva dai codici di tipo tabciva.tb_revcharge <> 'N'
              dttTmp.Clear()
              For i = 1 To 8
                If NTSCInt(dttET.Rows(0)("tm_codiva_" & i.ToString)) <> 0 Then
                  oCldFdin.ValCodiceDb(NTSCInt(dttET.Rows(0)("tm_codiva_" & i.ToString)).ToString, strDittaCorrente, "TABCIVA", "N", "", dttTmp)
                  If NTSCStr(dttTmp.Rows(0)!tb_revcharge) <> "N" Then
                    dRim -= NTSCDec(dttET.Rows(0)("tm_imposta_" & i.ToString))
                    dRimv -= NTSCDec(dttET.Rows(0)("tm_impostav_" & i.ToString))
                  End If
                End If
              Next
            End If
            dttTmp.Clear()
          End If
        End If


        P.nCodpaga = NTSCInt(dttET.Rows(0)!tm_codpaga)
        P.strDatrif = strDataPartenza
        P.dTotfat = dRim
        P.dTotfatval = dRimv
        P.dIva = dTotimp
        P.dIvaval = dTotimpv
        P.dSpese = NTSCDec(!tm_speacc) + NTSCDec(!tm_speinc) + NTSCDec(!tm_bolli) + NTSCDec(!tm_speimb)
        P.dSpeseval = NTSCDec(!tm_speaccv) + NTSCDec(!tm_speincv) + NTSCDec(!tm_bolliv) + NTSCDec(!tm_speimbv)

        If oCldFdin.CheckCausaleSplitPaymentFromTpbf(strDittaCorrente, NTSCInt(!tm_tipobf)) Then
          'se la causale di CG è di tipo split payment (nuova iva differita con iva che verrà versata direttamente dall'ente pubblico)
          'nel calcolo delle scadenze non devo tener conto dell'IVA
          'NON E' GESTITO IL CASO DI FATTURE EMESSE IN VALUTA AD ENTE PUBBLICO
          P.dTotfat -= P.dIva
          dRim -= P.dIva
          P.dIva = 0
        End If

        nRate = CType(oCleComm, CLELBMENU).CalcolaScad(strDittaCorrente, P.nCodpaga, P.strDatrif, P.dTotfat, P.dTotfatval, P.dIva, _
                            P.dIvaval, P.dSpese, P.dSpeseval, P.strDatsca, P.dImpsca, P.dImpscaval, nTiprata, strErr, _
                            NTSCInt(dttET.Rows(0)!tm_valuta), lContoCF)
        If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
        If nRate < 1 Or nRate > 8 Then
          If bVisMessageSaldo0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128669136551718750, "E' possibile modificare pagamenti con al massimo 8 rate.")))
            For i = 1 To 8
              dttET.Rows(0)("tm_tippaga_" & i.ToString) = 0
              dttET.Rows(0)("tm_datsca_" & i.ToString) = DBNull.Value
              dttET.Rows(0)("tm_impsca_" & i.ToString) = 0
              dttET.Rows(0)("tm_impscav_" & i.ToString) = 0
            Next
            Return False
          End If
        End If

        If bModifCastScad Then
          If (dRim <> ArrDbl(NTSCDec(!tm_impsca_1) + NTSCDec(!tm_impsca_2) + NTSCDec(!tm_impsca_3) + NTSCDec(!tm_impsca_4) + NTSCDec(!tm_impsca_5) + NTSCDec(!tm_impsca_6) + NTSCDec(!tm_impsca_7) + NTSCDec(!tm_impsca_8), oCldFdin.TrovaNdec(0))) Or _
             (dRimv <> ArrDbl(NTSCDec(!tm_impscav_1) + NTSCDec(!tm_impscav_2) + NTSCDec(!tm_impscav_3) + NTSCDec(!tm_impscav_4) + NTSCDec(!tm_impscav_5) + NTSCDec(!tm_impscav_6) + NTSCDec(!tm_impscav_7) + NTSCDec(!tm_impscav_8), oCldFdin.TrovaNdec(NTSCInt(!tm_valuta))) And NTSCInt(!tm_valuta) <> 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128674924592656250, "L'importo precedentemente ripartito sulle rate di scadenza è modificato pertanto le rate verranno ricalcolate.")))
            bModifCastScad = False
            For i = 1 To 8
              dttET.Rows(0)("tm_tippaga_" & i.ToString) = 0
              dttET.Rows(0)("tm_datsca_" & i.ToString) = DBNull.Value
              dttET.Rows(0)("tm_impsca_" & i.ToString) = 0
              dttET.Rows(0)("tm_impscav_" & i.ToString) = 0
            Next
            For i = 1 To nRate
              dttET.Rows(0)("tm_tippaga_" & i.ToString) = nTiprata(i)
              dttET.Rows(0)("tm_datsca_" & i.ToString) = P.strDatsca(i)
              dttET.Rows(0)("tm_impsca_" & i.ToString) = P.dImpsca(i)
              dttET.Rows(0)("tm_impscav_" & i.ToString) = P.dImpscaval(i)
              If i = 8 Then Exit For
            Next
            Return False
          End If
        Else
          'memorizzo su testmag. prima pulisco il vettore
          For i = 1 To 8
            dttET.Rows(0)("tm_tippaga_" & i.ToString) = 0
            dttET.Rows(0)("tm_datsca_" & i.ToString) = DBNull.Value
            dttET.Rows(0)("tm_impsca_" & i.ToString) = 0
            dttET.Rows(0)("tm_impscav_" & i.ToString) = 0
          Next

          For i = 1 To nRate
            dttET.Rows(0)("tm_tippaga_" & i.ToString) = nTiprata(i)
            dttET.Rows(0)("tm_datsca_" & i.ToString) = P.strDatsca(i)
            dttET.Rows(0)("tm_impsca_" & i.ToString) = P.dImpsca(i)
            dttET.Rows(0)("tm_impscav_" & i.ToString) = P.dImpscaval(i)
            If i = 8 Then Exit For
          Next
        End If
      End With    'With dttET.Rows(0)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function CalcolaDataScadenza(ByVal strTipoRk As String, ByVal strDatPar As String, ByVal strDatDoc As String) As String
    Try
      Select Case strTipoRk
        Case "J", "K", "L", "M", "T", "("
          If bCalcolaScadUsaSempreDatdoc Or strDatPar = "" Then
            Return strDatDoc
          Else
            Return NTSCDate(strDatPar).ToShortDateString
          End If
        Case Else
          Return strDatDoc
      End Select
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return ""
    End Try
  End Function

  Public Overridable Function CalcolaStringaPVR() As String
    'calcola la striga per i PVR svizzeri
    'solo se:
    ' - opzione di registro abilitata
    ' - il cliente è gestito a scadenze
    ' - il tipo di pagamento è 'contanti' ed ha una sola rata
    'esempio
    'CalcolaStringaPVR = "0100000589650>090874010001450034999200111+ 010013534>"
    Dim strTmp As String
    Dim i As Integer
    Dim strNumcliPVR As String
    Dim strPrefixFreeStringPVR As String
    Dim strSerie As String
    Dim dttTmp As New DataTable

    Try
      CalcolaStringaPVR = ""
      'solo per fatture immediate emesse o ricevute
      If dttET.Rows(0)!tm_tipork.ToString <> "D" And dttET.Rows(0)!tm_tipork.ToString <> "K" Then Return ""

      If dttEt_conto.Rows(0)!an_scaden.ToString <> "S" Then Return ""

      oCldFdin.ValCodiceDb(NTSCInt(dttET.Rows(0)!tm_codpaga).ToString, strDittaCorrente, "TABPAGA", "N", "", dttTmp)
      If NTSCInt(dttTmp.Rows(0)!tb_tippaga) <> 4 Or NTSCInt(dttTmp.Rows(0)!tb_numrate) <> 1 Then Return ""
      dttTmp.Clear()

      'per i documenti ricevuti occorre inserire la stringa tramite l'apposita voce di menu/file
      If dttET.Rows(0)!tm_tipork.ToString = "K" Then
        Return dttET.Rows(0)!tm_andescr2.ToString & dttET.Rows(0)!tm_anindir.ToString
      End If

      strNumcliPVR = oCldFdin.GetSettingBus("OPZIONI", ".", ".", "NsNumCliPVR", "", " ", "")                    ' NON DOCUMENTARE
      strPrefixFreeStringPVR = oCldFdin.GetSettingBus("OPZIONI", ".", ".", "PrefixFreeStringPVR", "", " ", "")  ' NON DOCUMENTARE
      For i = 1 To (6 - Len(strNumcliPVR))
        strNumcliPVR = "0" & strNumcliPVR
      Next
      For i = 1 To (6 - Len(strPrefixFreeStringPVR))
        strPrefixFreeStringPVR = "0" & strPrefixFreeStringPVR
      Next

      'primi 2 caratteri: 03 = PVR in euro, 01 = PVR in CHF
      'per un fattore di velocità non testo la tabella valuta:
      'se sono in valuta presuppongo che sia l'unica ammessa
      If NTSCInt(dttET.Rows(0)!tm_valuta) = 0 Then
        CalcolaStringaPVR = IIf(oApp.ValutaCorrente = "EUR", "21", "01").ToString
        strNumcliPVR = IIf(oApp.ValutaCorrente = "EUR", "03", "01").ToString & strNumcliPVR
      Else
        CalcolaStringaPVR = IIf(oApp.ValutaCorrente = "EUR", "01", "21").ToString
        strNumcliPVR = IIf(oApp.ValutaCorrente = "EUR", "01", "03").ToString & strNumcliPVR
      End If
      strNumcliPVR = strNumcliPVR & calcolaPVRCheck(strNumcliPVR)

      '10 caratteri successivi: importo residuo da incassare (senza decimali:moltiplico per 100)
      With dttET.Rows(0)
        If NTSCInt(dttET.Rows(0)!tm_valuta) = 0 Then
          strTmp = (NTSCInt((NTSCDec(!tm_totdoc) - NTSCDec(!tm_pagato) - NTSCDec(!tm_pagato2) + NTSCDec(!tm_resto) - NTSCDec(!tm_abbuono) - NTSCDec(!tm_totomag)) * 100)).ToString
        Else
          strTmp = (NTSCInt((NTSCDec(!tm_totdocv) - NTSCDec(!tm_pagatov) - NTSCDec(!tm_abbuonov) - NTSCDec(!tm_totomagv)) * 100)).ToString
        End If
      End With

      For i = 1 To 10 - Len(strTmp)
        strTmp = "0" & strTmp
      Next
      CalcolaStringaPVR = CalcolaStringaPVR & strTmp
      '1 carattere di controllo
      CalcolaStringaPVR = CalcolaStringaPVR & calcolaPVRCheck(CalcolaStringaPVR)
      '1 carattere fisso
      CalcolaStringaPVR = CalcolaStringaPVR & ">"
      '27 caratteri liberi per Business: di cui:
      '6 prefisso variabile da registro - 9 codice cliente - 2 anno - 2 serie - 5 numero documento - 2 numero di rata  (fisso 01)
      CalcolaStringaPVR = CalcolaStringaPVR & strPrefixFreeStringPVR & lContoCF.ToString("000000000") & NTSCDate(dttET.Rows(0)!tm_datdoc).Year.ToString.Substring(2)
      If dttET.Rows(0)!tm_serie.ToString = " " Then
        strSerie = "00"
      Else
        If Asc(dttET.Rows(0)!tm_serie.ToString) <= 90 Then
          strSerie = Chr(Asc(dttET.Rows(0)!tm_serie.ToString) - 65 + 48)
        Else
          strSerie = Chr(Asc(dttET.Rows(0)!tm_serie.ToString) - 97 + 48)
        End If
      End If
      CalcolaStringaPVR = CalcolaStringaPVR & strSerie & NTSCInt(dttET.Rows(0)!tm_numdoc).ToString("00000") & "01"
      '1 carattere di controllo
      CalcolaStringaPVR = CalcolaStringaPVR & calcolaPVRCheck(Mid(CalcolaStringaPVR, 15, 27))
      '2 caratteri fissi
      CalcolaStringaPVR = CalcolaStringaPVR & "+ "

      '9 codice PVR nostro (comprensivo di check digit
      CalcolaStringaPVR = CalcolaStringaPVR & strNumcliPVR

      '1 carattere fisso
      CalcolaStringaPVR = CalcolaStringaPVR & ">"

      Return CalcolaStringaPVR

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return ""
    End Try
  End Function

  Public Overridable Function CalcolaTotali(ByVal bRicalcolaDoc As Boolean) As Boolean
    Dim oTestata As New CLELBMENU.OutTestata
    Try
      Return CalcolaTotali(bRicalcolaDoc, oTestata)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function CalcolaTotali(ByVal bRicalcolaDoc As Boolean, ByRef oTestata As CLELBMENU.OutTestata) As Boolean
    Dim oclPar As CLELBMENU.Parametri = New CLELBMENU.Parametri
    Dim strError As String = ""
    Dim bResult As Boolean = False

    Dim dTotSpese As Decimal = 0
    Dim dTotSpesev As Decimal = 0
    Dim dTotImpon As Decimal = 0
    Dim dTotImponv As Decimal = 0
    Dim dTotIva As Decimal = 0
    Dim dTotIvav As Decimal = 0
    Dim dTotDoc As Decimal = 0
    Dim dTotDocv As Decimal = 0
    Dim dRimanenza As Decimal = 0
    Dim dRimanenzav As Decimal = 0
    Dim dTotQuant As Decimal = 0
    Dim i As Integer = 0

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {bRicalcolaDoc, oTestata})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        oTestata = CType(oIn(1), CLELBMENU.OutTestata)
        Return CBool(oOut)
      End If
      '----------------

      If dsShared.Tables("TESTMAG").Rows.Count = 0 Then Return True
      If bInImportDDT Then Return True

      If bRicalcolaDoc Then
        bInCalcolaTotali = True
        'compilo i parametri che piloteranno il ricalcolo del documento

        With oclPar
          .strNomProg = "BSVEFDIN"
          .bNew = bNew
          .bDocEmesso = IsDocumentoEmesso(dttET.Rows(0)!tm_tipork.ToString)
          .bCalcolaBolli = CBool(IIf(dttET.Rows(0)!tm_flbolli.ToString = "S", True, False))
          .bCalcolaColli = False
          .bCalcolaColliPesiSuDocAperti = False
          .bCalcolaPesoLordo = False
          .bCalcolaPesoNetto = False
          .bCalcPesi = True
          .bNoPesiSuRigheKitFittizie = bNoPesiSuRigheKitFittizie
          .bNonCalcolaProvvig = False
          .strCalcolaSpeseTrasp = "N"
          .bSbloccaIva = CBool(IIf(dttET.Rows(0)!tm_flscdb.ToString = "S", True, False))
          .bSegueFatt = False
          .nGestScostAcconti = nGestScostAcconti
          .dImpoScostAcconti = dImpoScostAcconti
          .nCodese = NTSCInt(dttET.Rows(0)!tm_codese)
          .nPeacIva15 = nPeacIva15
          If NTSCDate(dttET.Rows(0)!tm_datdoc) < NTSCDate(strPeacIvaInc3Fine) Then
            .nPeacIvainc = nPeacIvaInc3
          ElseIf NTSCDate(dttET.Rows(0)!tm_datdoc) < NTSCDate(strPeacIvaInc2Fine) Then
            .nPeacIvainc = nPeacIvaInc2
          Else
            .nPeacIvainc = nPeacIvainc
          End If
          .nPeveIva15 = nPeveIva15
          If NTSCDate(dttET.Rows(0)!tm_datdoc) < NTSCDate(strPeveIvaInc3Fine) Then
            .nPeveIvaInc = nPeveIvaInc3
          ElseIf NTSCDate(dttET.Rows(0)!tm_datdoc) < NTSCDate(strPeveIvaInc2Fine) Then
            .nPeveIvaInc = nPeveIvaInc2
          Else
            .nPeveIvaInc = nPeveIvaInc
          End If
          .bDeterminaBolliSuOperazEsenti = bDeterminaBolliSuOperazEsenti
          .bNonCalcolaProvvig = True
        End With

        '---------------------------
        'ricalcolo il documento: in fdin vb6 calcola1 e calcola2
        bResult = CType(oCleComm, CLELBMENU).CalcolaDocFattureRieplig(strDittaCorrente, oclPar, _
                                dttET.Rows(0), dttETC.Rows(0), dsShared.Tables("ELENCODDT"), _
                                oTestata, strError, _
                                CBool(IIf(IsDocumentoEmesso(dttET.Rows(0)!tm_tipork.ToString), bCalcolaRagg, False)))
        If strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strError))
        If oTestata.nCcontr(20) = -1 Then Return False

        ' adesso visualizza tutto ....
        If bResult Then
          With dttET.Rows(0)
            For i = 1 To 8
              dttET.Rows(0)("tm_codiva_" & i.ToString) = oTestata.nCodiva(i - 1)
              dttET.Rows(0)("tm_imponib_" & i.ToString) = oTestata.dImponib(i - 1)
              dttET.Rows(0)("tm_imposta_" & i.ToString) = oTestata.dImposta(i - 1)
              dttET.Rows(0)("tm_imponibv_" & i.ToString) = oTestata.dImponibv(i - 1)
              dttET.Rows(0)("tm_impostav_" & i.ToString) = oTestata.dImpostav(i - 1)
            Next
            For i = 1 To 20
              dttETC.Rows(0)("tm_ccontr_" & i.ToString) = oTestata.nCcontr(i - 1)
              dttETC.Rows(0)("tm_impcont_" & i.ToString) = oTestata.dImpocont(i - 1)
              dttETC.Rows(0)("tm_impcontv_" & i.ToString) = oTestata.dImpcontv(i - 1)
            Next
            !tm_totcoll = ArrDblEcc(oTestata.dTotcoll, 0)
            !tm_peso = oTestata.dPeso
            !tm_pesonetto = oTestata.dPesonetto

            !tm_impprov = oTestata.dImpprov
            !tm_totprov = oTestata.dTotprov
            !tm_totprov2 = oTestata.dTotprov2

            !tm_speinc = oTestata.dSpeinc
            !tm_speincv = oTestata.dSpeincv
            !tm_speacc = oTestata.dSpeacc
            !tm_speaccv = oTestata.dSpeaccv
            !tm_bolli = oTestata.dBolli
            !tm_bolliv = oTestata.dBolliv

            !tm_speimb = oTestata.dSpeimb
            !tm_speimbv = oTestata.dSpeimbv
            !tm_totlordo = oTestata.dTotlordo
            !tm_totlordov = oTestata.dTotlordov
            !tm_totmerce = oTestata.dTotMerce
            !tm_totmercev = oTestata.dTotmercev
            !tm_totomag = oTestata.dTotomag
            !tm_totomagv = oTestata.dTotomagv
            !tm_totdoc = oTestata.dTotdoc
            !tm_totdocv = oTestata.dTotdocv
            !tm_abbuono = oTestata.dAbbuono
            !tm_abbuonov = oTestata.dAbbuonov
            !tm_pagato = oTestata.dPagato
            !tm_pagato2 = oTestata.dPagato2
            !tm_resto = oTestata.dResto
            !tm_pagatov = oTestata.dPagatov
            !tm_diffiva = oTestata.dDiffIva
            !tm_diffda = oTestata.dDiffDA
            dTotQuant = oTestata.dTotquant
          End With
        End If
        bInCalcolaTotali = False
      End If    'If bRicalcolaDoc Then

      'devo solo aggiornare la UI per i campi unbound
      With dsShared.Tables("TESTMAG").Rows(0)
        dTotSpese = NTSCDec(!tm_speacc) + NTSCDec(!tm_speinc) + NTSCDec(!tm_bolli) + NTSCDec(!tm_speimb)
        dTotSpesev = NTSCDec(!tm_speaccv) + NTSCDec(!tm_speincv) + NTSCDec(!tm_bolliv) + NTSCDec(!tm_speimbv)
        'dTotImpon = NTSCDec(!tm_totmerce) + dTotSpese
        'dTotImponv = NTSCDec(!tm_totmercev) + dTotSpesev
        For i = 1 To 8
          dTotImpon += NTSCDec(dsShared.Tables("TESTMAG").Rows(0)("tm_imponib_" & i.ToString))
          dTotImponv += NTSCDec(dsShared.Tables("TESTMAG").Rows(0)("tm_imponibv_" & i.ToString))
          dTotIva += NTSCDec(dsShared.Tables("TESTMAG").Rows(0)("tm_imposta_" & i.ToString))
          dTotIvav += NTSCDec(dsShared.Tables("TESTMAG").Rows(0)("tm_impostav_" & i.ToString))
        Next
        dTotDoc = NTSCDec(!tm_totdoc) - NTSCDec(!tm_totomag)
        dTotDocv = NTSCDec(!tm_totdocv) - NTSCDec(!tm_totomagv)
        dRimanenza = dTotDoc - NTSCDec(!tm_pagato) - NTSCDec(!tm_pagato2) + NTSCDec(!tm_resto) - NTSCDec(!tm_abbuono)
        dRimanenzav = dTotDocv - NTSCDec(!tm_pagatov) - NTSCDec(!tm_abbuonov)
      End With

      'avviso l'UI di aggiornare i totali dei campi UNBOUND
      ThrowRemoteEvent(New NTSEventArgs("AggTotali.:" & _
                        dTotSpese.ToString & "§" & _
                        dTotSpesev.ToString & "§" & _
                        dTotImpon.ToString & "§" & _
                        dTotImponv.ToString & "§" & _
                        dTotIva.ToString & "§" & _
                        dTotIvav.ToString & "§" & _
                        dTotDoc.ToString & "§" & _
                        dTotDocv.ToString & "§" & _
                        dRimanenza.ToString & "§" & _
                        dRimanenzav.ToString & "§" & _
                        dTotQuant.ToString, ""))
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      bInCalcolaTotali = False
    End Try
  End Function

  Public Overridable Function IsDocRetail(ByVal strTipoDoc As String, ByVal nAnno As Integer, _
                                          ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try

      Return oCldFdin.IsDocRetail(strDittaCorrente, strTipoDoc, nAnno, strSerie, lNumdoc)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function IsDocRetailNew(ByVal strTipoDoc As String, ByVal nAnno As Integer, _
                                             ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Try

      Return oCldFdin.IsDocRetailNew(strDittaCorrente, strTipoDoc, nAnno, strSerie, lNumdoc)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function GetQueryStampaPdf() As String
    Try
      Return oCldFdin.GetQueryStampaPdf(strDittaCorrente, dttET.Rows(0)!tm_tipork.ToString, _
                                        NTSCInt(dttET.Rows(0)!tm_anno), dttET.Rows(0)!tm_serie.ToString, _
                                        NTSCInt(dttET.Rows(0)!tm_numdoc.ToString))

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return ""
    End Try
  End Function

  Public Overridable Function CheckPIvaCFisCliente(ByVal dttConto As DataTable) As Boolean
    Try
      If strControlloPIvaCodFis <> "0" Then
        If NTSCStr(dttET.Rows(0)!tm_tipork) = "A" Or NTSCStr(dttET.Rows(0)!tm_tipork) = "E" Or NTSCStr(dttET.Rows(0)!tm_tipork) = "N" Or _
           NTSCStr(dttET.Rows(0)!tm_tipork) = "S" Or NTSCStr(dttET.Rows(0)!tm_tipork) = "D" Or NTSCStr(dttET.Rows(0)!tm_tipork) = "P" Or _
           NTSCStr(dttET.Rows(0)!tm_tipork) = "£" Then

          If dttConto Is Nothing OrElse dttConto.Rows.Count = 0 Then
            dttConto = New DataTable
            oCldFdin.ValCodiceDb(NTSCStr(dttET.Rows(0)!tm_conto), strDittaCorrente, "ANAGRA", "N", "", dttConto)
          End If

          With dttConto.Rows(0)
            If NTSCStr(!an_tpsogiva) = "E" Then Return True 'Nessun controllo per gli extracee
            If (NTSCStr(!an_tpsogiva) = "I" Or NTSCStr(!an_privato) = "S") And NTSCStr(!an_codfis).Trim = "" Then 'Per gli intraceee o i privati controllo il codice fiscale
              If strControlloPIvaCodFis = "-1" Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130038418613506919, "Attezione! Nell'anagrafica del cliente non è stato indicato il codice fiscale.")))
                Return False
              Else
                ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 130038418666615427, "Attezione! Nell'anagrafica del cliente non è stato indicato il codice fiscale.")))
              End If
            ElseIf NTSCStr(!an_tpsogiva) = "N" And NTSCStr(!an_privato) = "N" And NTSCStr(!an_pariva).Trim = "" Then 'Per le aziende controllo la partita iva
              If strControlloPIvaCodFis = "-1" Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130038422000129494, "Attezione! Nell'anagrafica del cliente non è stata indicata la partita iva.")))
                Return False
              Else
                ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 130038422009970409, "Attezione! Nell'anagrafica del cliente non è stata indicata la partita iva.")))
              End If
            End If
          End With
        End If
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function AlertContabilizzato() As Boolean
    Dim dttAlert As DataTable = Nothing
    Dim strTmp As String = ""
    Try
      Select Case dttET.Rows(0)!tm_tipork.ToString
        Case "D" : strTmp = oApp.Tr(Me, 128740017193745000, "Fattura diff. emessa")
        Case "K" : strTmp = oApp.Tr(Me, 128740017513233000, "Fattura diff. ricevuta")
        Case "P" : strTmp = oApp.Tr(Me, 128740017526337000, "Fatt.Ric.Fisc.Differita")
        Case "£" : strTmp = oApp.Tr(Me, 129242807387802734, "Nota accred. diff. emessa")
        Case "(" : strTmp = oApp.Tr(Me, 129242807406630859, "Nota accred. diff. ricevuta")
      End Select
      dttAlert = CType(oCleComm, CLELBMENU).CreaDynasetAlert
      dttAlert.Rows.Add(dttAlert.NewRow)
      dttAlert.Rows(0)!codditt = strDittaCorrente
      dttAlert.Rows(0)!strMsg = oApp.Tr(Me, 128740015680545000, _
                               "E' stato modificato il documento '|" & strTmp & "|' anno |" & _
                               dttET.Rows(0)!tm_anno.ToString & "| serie '|" & _
                               dttET.Rows(0)!tm_serie.ToString & "|' numero |" & _
                               dttET.Rows(0)!tm_numdoc.ToString & "|. " & _
                               "Tale documento risulta già contabilizzato con la registrazione numero |" & _
                               dttET.Rows(0)!tm_numregef.ToString & "| del |" & _
                              NTSCDate(dttET.Rows(0)!tm_datregef).ToShortDateString & "|." & _
                               "Si consiglia pertanto di ricontabilizzarlo.")
      dttAlert.AcceptChanges()
      CType(oCleComm, CLELBMENU).Verifica_Genera_Alert(2, strDittaCorrente, "BSVEFDIN", 1, 0, dttAlert)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      dttAlert.Clear()
    End Try
  End Function

  Public Overridable Function ConfermaDocConDataAnteriore(ByVal strTipork As String, ByVal nAnno As Integer, _
                                                           ByVal strSerie As String, ByVal strDatdocIn As String) As Boolean
    Try
      'obsoleta
      Return ConfermaDocConDataAnteriore(strTipork, nAnno, strSerie, strDatdocIn, -1)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ConfermaDocConDataAnteriore(ByVal strTipork As String, ByVal nAnno As Integer, _
                                                         ByVal strSerie As String, ByVal strDatdocIn As String, _
                                                         ByVal lNumdoc As Integer) As Boolean
    Dim strTipodoc As String = ""
    Dim strDatdocOut As String = ""
    Dim strTiporkOut As String = ""
    Dim strDatdocOut1 As String = ""
    Dim strTiporkOut1 As String = ""

    Dim evnt As NTSEventArgs = Nothing

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strTipork, nAnno, strSerie, strDatdocIn, lNumdoc})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      If bNew = False Then Return True
      '--------------------------------------------------------------------------------------------------------------
      If oCldFdin.DocConDataAnteriore(strDittaCorrente, strTipork, nAnno, strSerie, strDatdocOut, strTiporkOut, _
                                      lNumdoc, strDatdocOut1, strTiporkOut1) = True Then
        If lNumdoc = -1 Then
          'vecchio sistema
          If NTSCDate(strDatdocOut) > NTSCDate(strDatdocIn) Then
            strTipodoc = TraduciTipork(strTiporkOut)
            evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128993930468454542, "Attenzione!" & vbCrLf & _
              "Esiste |" & strTipodoc & "| posteriore al documento che si vuole inserire" & vbCrLf & _
              "(a parità di anno e serie), con data '|" & NTSCDate(strDatdocIn).ToShortDateString & "|'," & vbCrLf & _
              "rispetto a quella di testata '|" & NTSCDate(strDatdocOut).ToShortDateString & "|'." & vbCrLf & vbCrLf & _
              "Confermare comunque l'inserimento del documento?"))
            ThrowRemoteEvent(evnt)
            If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
          End If
        Else
          'nuovo sistema
          'strDatdocOut = data del documento immediatamente precedente (numero più basso)
          'strDatdocOut1 = data del documento immediatamente successivo (numero più alto)
          If strDatdocOut <> "" Then
            strTipodoc = TraduciTipork(strTiporkOut)
            If NTSCDate(strDatdocOut) > NTSCDate(strDatdocIn) Then
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 130433199356145391, "Attenzione!" & vbCrLf & _
                            "Esiste |" & strTipodoc & "| con numero ANTECEDENTE al documento che si vuole inserire" & vbCrLf & _
                            "(a parità di anno e serie), con data '|" & NTSCDate(strDatdocOut).ToShortDateString & "|'," & vbCrLf & _
                            "successiva rispetto a quella di testata '|" & NTSCDate(strDatdocIn).ToShortDateString & "|'." & vbCrLf & vbCrLf & _
                            "Confermare comunque l'inserimento del documento?"))
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
            End If
          End If    'If strDatdocOut <> "" Then

          If strDatdocOut1 <> "" Then
            strTipodoc = TraduciTipork(strTiporkOut1)
            If NTSCDate(strDatdocOut1) < NTSCDate(strDatdocIn) Then
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 130433199329532513, "Attenzione!" & vbCrLf & _
                            "Esiste |" & strTipodoc & "| con numero SUCCESSIVO al documento che si vuole inserire" & vbCrLf & _
                            "(a parità di anno e serie), con data '|" & NTSCDate(strDatdocOut1).ToShortDateString & "|'," & vbCrLf & _
                            "antecedente rispetto a quella di testata '|" & NTSCDate(strDatdocIn).ToShortDateString & "|'." & vbCrLf & vbCrLf & _
                            "Confermare comunque l'inserimento del documento?"))
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
            End If
          End If    'If strDatdocOut <> "" Then
        End If    'If lNumdoc = -1 Then

      End If    'If oCldFdin.DocConDataAnteriore(strDittaCo
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function TraduciTipork(ByVal strTipork As String) As String
    TraduciTipork = ""
    Try
      Select Case strTipork
        Case "A" : Return oApp.Tr(Me, 130433193530071221, "una 'Fattura immediata emessa'")
        Case "B" : Return oApp.Tr(Me, 130433193544875189, "un 'D.D.T. emesso'")
        Case "C" : Return oApp.Tr(Me, 130433193554873953, "un 'corrispettivo emesso'")
        Case "D" : Return oApp.Tr(Me, 130433193572786642, "una 'Fattura differita emessa'")
        Case "E" : Return oApp.Tr(Me, 130433193583427004, "una 'Nota di addebito emessa'")
        Case "F" : Return oApp.Tr(Me, 130433193594789429, "una 'Ricevuta fiscale emessa'")
        Case "I" : Return oApp.Tr(Me, 130433193605907565, "una 'Riemissione ricevuta fiscale'")
        Case "J" : Return oApp.Tr(Me, 130433193619029163, "una 'Nota di accredito ricevuta'")
        Case "K" : Return oApp.Tr(Me, 130433193628637226, "una 'Fattura differita ricevuta'")
        Case "L" : Return oApp.Tr(Me, 130433193639910077, "una 'Fattura immediata ricevuta'")
        Case "M" : Return oApp.Tr(Me, 130433193651113446, "un 'D.D.T. ricevuto'")
        Case "N" : Return oApp.Tr(Me, 130433193663842197, "una 'Nota di accredito emessa'")
        Case "P" : Return oApp.Tr(Me, 130433193674392647, "una 'Fattura/ricevuta fiscale differita'")
        Case "S" : Return oApp.Tr(Me, 130433193689353525, "una 'Fattura/ricevuta fiscale emessa'")
        Case "T" : Return oApp.Tr(Me, 130433193699593747, "un 'Carico da produzione'")
        Case "U" : Return oApp.Tr(Me, 130433193711758846, "uno 'Scarico a produzione'")
        Case "W" : Return oApp.Tr(Me, 130433193723043308, "una 'Nota di prelievo'")
        Case "Z" : Return oApp.Tr(Me, 130433193733036030, "una 'Bolla di movimentazione interna'")
        Case "£" : Return oApp.Tr(Me, 130433193744486648, "una 'Nota accredito differita emessa'")
        Case "(" : Return oApp.Tr(Me, 130433193756566834, "una 'Nota accredito differita ricevuta'")
      End Select

      Return ""

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function ChiamaPnfa() As Boolean
    Try
      If dttET.Rows.Count = 0 Then Return True

      If IsDocumentoEmesso(NTSCStr(dttET.Rows(0)!tm_tipork)) And "SA".Contains(strCallPnfa) Then Return True
      If IsDocumentoEmesso(NTSCStr(dttET.Rows(0)!tm_tipork)) = False And "SP".Contains(strCallPnfa) Then Return True

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

#Region "FORM BNVECAST"
  Public dsCastShared As DataSet
  Public strCastPrevCelValue As String = ""


  Public Overridable Function CastApri(ByRef dsCast As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      dsCastShared = dsCast

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsCastShared.Tables("IVA").ColumnChanging, AddressOf CastBeforeColUpdateIVA
      AddHandler dsCastShared.Tables("IVA").ColumnChanged, AddressOf CastAfterColUpdateIVA

      AddHandler dsCastShared.Tables("CONTROP").ColumnChanging, AddressOf CastBeforeColUpdateCONTROP
      AddHandler dsCastShared.Tables("CONTROP").ColumnChanged, AddressOf CastAfterColUpdateCONTROP
      AddHandler dsCastShared.Tables("CONTROP").ColumnChanging, AddressOf CastBeforeColUpdateIMPCONT
      AddHandler dsCastShared.Tables("CONTROP").ColumnChanged, AddressOf CastAfterColUpdateIMPCONT

      '--------------------------------------
      'confermo tutto
      dsCastShared.AcceptChanges()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CastControllaCastelletti() As Boolean
    Dim dTotImpIVA As Decimal = 0
    Dim dTotImpCont As Decimal = 0
    Dim dTotImpIVAval As Decimal = 0
    Dim dTotImpContval As Decimal = 0
    Dim i As Integer = 0

    Try
      dTotImpIVA = 0
      dTotImpIVAval = 0
      For i = 0 To 7
        If NTSCInt(dsCastShared.Tables("IVA").Rows(i)!xx_codiva) <> 0 Then
          dTotImpIVA = dTotImpIVA + NTSCDec(dsCastShared.Tables("IVA").Rows(i)!xx_imponib)
          dTotImpIVAval = dTotImpIVAval + NTSCDec(dsCastShared.Tables("IVA").Rows(i)!xx_imponibv)
        Else
          Exit For
        End If
      Next

      dTotImpCont = 0
      dTotImpContval = 0
      For i = 0 To 19
        If NTSCInt(dsCastShared.Tables("CONTROP").Rows(i)!xx_ccontr) <> 0 Then
          dTotImpCont = dTotImpCont + NTSCDec(dsCastShared.Tables("CONTROP").Rows(i)!xx_impcont)
          dTotImpContval = dTotImpContval + NTSCDec(dsCastShared.Tables("CONTROP").Rows(i)!xx_impcontv)
        Else
          Exit For
        End If
      Next

      Return ControllaCastelletti(dTotImpIVA, dTotImpCont, dTotImpIVAval, dTotImpContval)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function


  Public Overridable Function CastRipristinaIva(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsCastShared.Tables("IVA").Select(strFilter)(nRow).RejectChanges()
      Return True
    Catch ex As Exception
    End Try
  End Function
  Public Overridable Function CastSalvaIva(ByVal bDelete As Boolean) As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim bTrov As Boolean = True
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      dtrTmp = dsCastShared.Tables("IVA").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!xx_codiva) <> 0 Then
          If Not oCldFdin.ValCodiceDb(NTSCInt(dtrTmp(i)!xx_codiva).ToString, strDittaCorrente, "TABCIVA", "N") Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128666480656406250, "Codice IVA |" & NTSCInt(dtrTmp(i)!xx_codiva).ToString & "| inesistente")))
            Return False
          End If
        End If
      Next

      'Controllo consecutivita'
      bTrov = True
      dtrTmp = dsCastShared.Tables("IVA").Select("", "xx_idiva")
      For i = 0 To 7
        With dtrTmp(i)
          If NTSCInt(!xx_codiva) = 0 Then
            bTrov = False
            If NTSCDec(!xx_imponib) <> 0 Or NTSCDec(!xx_imponibv) <> 0 Or NTSCDec(!xx_imposta) <> 0 Or NTSCDec(!xx_impostav) <> 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128666483517968750, "Imponibili ed imposte devono essere uguali a 0 se non è impostato il codice IVA")))
              Return False
            End If
          Else
            If bTrov = False Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128666482261875000, "Manca consecutività nel castelletto IVA.")))
              Return False
            End If
          End If
        End With
      Next

      '----------------------------------------
      'chiamo il dal per salvare
      dsCastShared.Tables("IVA").AcceptChanges()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Sub CastBeforeColUpdateIVA(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strCastPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "CastBeforeColUpdateIVA_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub CastAfterColUpdateIVA(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strCastPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strCastPrevCelValue = strCastPrevCelValue.Remove(strCastPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "CastAfterColUpdateIVA_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub CastAfterColUpdateIVA_xx_imponib(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Propongo l'iva
      '--------------------------------------------------------------------------------------------------------------
      If NTSCDec(e.ProposedValue) <> 0 And NTSCInt(e.Row!xx_codiva) <> 0 Then
        If (NTSCDec(e.Row!xx_imposta) = 0) Or (bCastIvaCalcoloImposta = True) Then
          e.Row!xx_imposta = oCldFdin.CalcolaIva(NTSCDec(e.ProposedValue), NTSCInt(e.Row!xx_codiva), oCldFdin.TrovaNdec(0))
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Function CastRipristinaControp(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsCastShared.Tables("CONTROP").Select(strFilter)(nRow).RejectChanges()
      Return True
    Catch ex As Exception
    End Try
  End Function
  Public Overridable Function CastSalvaControp(ByVal bDelete As Boolean) As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim bTrov As Boolean = True
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      dtrTmp = dsCastShared.Tables("CONTROP").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!xx_ccontr) <> 0 Then
          If Not oCldFdin.ValCodiceDb(NTSCInt(dtrTmp(i)!xx_ccontr).ToString, strDittaCorrente, "TABCOVE", "N") Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128666486932343750, "Codice Contropartita |" & NTSCInt(dtrTmp(i)!xx_ccontr).ToString & "| inesistente")))
            Return False
          End If
        End If
      Next

      'Controllo consecutivita'
      bTrov = True
      dtrTmp = dsCastShared.Tables("CONTROP").Select("", "xx_id")
      For i = 0 To 19
        With dtrTmp(i)
          If NTSCInt(!xx_ccontr) = 0 Then
            bTrov = False
            If NTSCDec(!xx_impcont) <> 0 Or NTSCDec(!xx_impcontv) <> 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128666487221875000, "Gli importi devono essere uguali a 0 se non è impostato il codice CONTROPARTITA")))
              Return False
            End If
          Else
            If bTrov = False Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128666487231718750, "Manca consecutività nel castelletto CONTROPARTITE.")))
              Return False
            End If
          End If
        End With
      Next

      '----------------------------------------
      'chiamo il dal per salvare
      dsCastShared.Tables("CONTROP").AcceptChanges()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Sub CastBeforeColUpdateCONTROP(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strCastPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "CastBeforeColUpdateCONTROP_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub CastAfterColUpdateCONTROP(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strCastPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strCastPrevCelValue = strCastPrevCelValue.Remove(strCastPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "CastAfterColUpdateCONTROP_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub CastBeforeColUpdateIMPCONT(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strCastPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "CastBeforeColUpdateIMPCONT_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub CastAfterColUpdateIMPCONT(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strCastPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strCastPrevCelValue = strCastPrevCelValue.Remove(strCastPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "CastAfterColUpdateIMPCONT_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub CastAfterColUpdateIMPCONT_xx_impcont(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim i As Integer = -1
    Dim nIndice As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dtrTmp() As DataRow

    Try
      '--------------------------------------------------------------------------------------------------------------
      If bCastContropVariaCastiva = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      dtrTmp = dsCastShared.Tables("IVA").Select("", "xx_idiva")
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To 7
        If NTSCInt(dtrTmp(i)!xx_codiva) <> 0 Then
          nIndice = i
          Exit For
        End If
      Next
      If nIndice <> -1 Then
        i = 0
        For i = 0 To 7
          If (nIndice <> i) And (NTSCInt(dtrTmp(i)!xx_codiva) <> 0) Then
            nIndice = -1
            Exit For
          End If
        Next
      End If
      If nIndice <> -1 Then
        i = 0
        For i = 0 To 19
          dImponib += NTSCDec(dsCastShared.Tables("CONTROP").Rows(i)!xx_impcont)
        Next
        With dsShared.Tables("TESTMAG").Rows(0)
          dImponib += NTSCDec(!tm_speinc) + NTSCDec(!tm_bolli) + NTSCDec(!tm_totomag)
        End With
        With dsCastShared.Tables("IVA").Rows(nIndice)
          !xx_imponib = dImponib
          If bCastIvaCalcoloImposta = True Then
            !xx_imposta = oCldFdin.CalcolaIva(dImponib, NTSCInt(!xx_codiva), oCldFdin.TrovaNdec(0))
          End If
        End With
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

#End Region

#Region "Gestione ACCCONF"
  Public Overridable Function Accconf_CheckVis() As Boolean
    'in apertura documento, controllo se posso vedere i dati
    Try
      If Not oApp.oGvar.bGestAccconf Then Return True
      If bNew Then Return True

      'tipobf
      If oApp.oGvar.nGestAccconfMagaz = 0 Then
        If NTSCInt(dsShared.Tables("TESTMAG").Rows(0)!tm_tipobf) <> 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, "VIS", strDittaCorrente, _
                                                             NTSCStr(dsShared.Tables("TESTMAG").Rows(0)!tm_tipork), _
                                                             NTSCStr(dsShared.Tables("TESTMAG").Rows(0)!tm_serie), _
                                                             NTSCInt(dsShared.Tables("TESTMAG").Rows(0)!tm_tipobf), 0, 0, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129543506273583985, "Utente non abilitato a visualizzare documenti con tipo B/F |'" & NTSCInt(dsShared.Tables("TESTMAG").Rows(0)!tm_tipobf).ToString & "'|")))
            Return False
          End If
        End If
      End If    'If oApp.oGvar.nGestAccconfMagaz = 0 Then

      nCodtpbfOpen = NTSCInt(dsShared.Tables("TESTMAG").Rows(0)!tm_tipobf)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function Accconf_CheckBeforecolupdate(ByVal strTipo As String, ByVal nCod As Integer) As Boolean
    'strTipo: BF = tipobf, CA = causale, CS = causale scarico, MA = magaz, M2 = magaz2, MS = magaz scarico
    Dim strMsg As String = ""
    Try
      If nCod = 0 Then Return True
      If Not oApp.oGvar.bGestAccconf Then Return True

      If strTipo = "BF" And oApp.oGvar.nGestAccconfMagaz <> 0 Then Return True 'non gestito il blocco sul tipobf
      If (strTipo = "CA" Or strTipo = "CS") And oApp.oGvar.nGestAccconfMagaz <> 1 Then Return True 'non gestito il blocco sulla causale
      If (strTipo = "MA" Or strTipo = "M2" Or strTipo = "MS") And oApp.oGvar.nGestAccconfMagaz <> 2 Then Return True 'non gestito il blocco sulla magazzino

      If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, IIf(bNew, "INS", "MOD").ToString, _
                                                     strDittaCorrente, _
                                                     dttET.Rows(0)!tm_tipork.ToString, _
                                                     dttET.Rows(0)!tm_serie.ToString, _
                                                     NTSCInt(IIf(strTipo = "BF", nCod, 0)), _
                                                     NTSCInt(IIf(strTipo = "MA" Or strTipo = "M2" Or strTipo = "MS", nCod, 0)), _
                                                     NTSCInt(IIf(strTipo = "CA" Or strTipo = "CS", nCod, 0)), 0) Then
        strMsg = oApp.Tr(Me, 129543542396289063, "Utente non abilitato all'utilizzo")
        If bNew Then
          strMsg += oApp.Tr(Me, 129544092200263672, " in nuovi documenti")
        Else
          strMsg += oApp.Tr(Me, 129544092327705078, " in documenti in modifica")
        End If
        Select Case strTipo
          Case "BF" : strMsg += oApp.Tr(Me, 129543542722119141, " del tipo bolla/fattura")
          Case "CA", "CS" : strMsg += oApp.Tr(Me, 129543543818261719, " della causale di magazzino")
          Case "MA", "M2", "MS" : strMsg += oApp.Tr(Me, 129543543806660157, " del magazzino")
        End Select
        strMsg += " '" & nCod.ToString & "'"
        ThrowRemoteEvent(New NTSEventArgs("", strMsg))
        Return False
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function Accconf_CheckSalvaCancella(ByVal strOperaz As String) As Boolean
    'in salvataggio/cancellazione documento, controllo se posso eseguire l'operazione
    'strOperaz: VIS, MOD, CANC
    Dim strMsg As String = ""
    Try
      If Not oApp.oGvar.bGestAccconf Then Return True

      Select Case strOperaz
        Case "INS" : strMsg = oApp.Tr(Me, 129543555604824219, "Utente non abilitato ad inserire documenti con ")
        Case "MOD" : strMsg = oApp.Tr(Me, 129543555591464844, "Utente non abilitato a modificare documenti con ")
        Case "CANC" : strMsg = oApp.Tr(Me, 129543555578984375, "Utente non abilitato a cancellare documenti con ")
      End Select

      'tipobf
      If oApp.oGvar.nGestAccconfMagaz = 0 Then
        If NTSCInt(dsShared.Tables("TESTMAG").Rows(0)!tm_tipobf) <> 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, strOperaz, strDittaCorrente, _
                                                             NTSCStr(dsShared.Tables("TESTMAG").Rows(0)!tm_tipork), _
                                                             NTSCStr(dsShared.Tables("TESTMAG").Rows(0)!tm_serie), _
                                                             NTSCInt(dsShared.Tables("TESTMAG").Rows(0)!tm_tipobf), 0, 0, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129543555544023438, "tipo B/F |'" & NTSCInt(dsShared.Tables("TESTMAG").Rows(0)!tm_tipobf).ToString & "'|")))
            Return False
          End If
          If bNew = False And nCodtpbfOpen <> 0 And nCodtpbfOpen <> NTSCInt(dsShared.Tables("TESTMAG").Rows(0)!tm_tipobf) Then
            If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, strOperaz, strDittaCorrente, _
                                                               NTSCStr(dsShared.Tables("TESTMAG").Rows(0)!tm_tipork), _
                                                               NTSCStr(dsShared.Tables("TESTMAG").Rows(0)!tm_serie), _
                                                               nCodtpbfOpen, 0, 0, 0) Then
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129543555522421875, "tipo B/F di origine |'" & nCodtpbfOpen.ToString & "'|")))
              Return False
            End If
          End If
        End If
      End If    'If oApp.oGvar.nGestAccconfMagaz = 0 Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

#End Region

  Public Overridable Function ConvImpValuta(ByVal dIn As Decimal) As Decimal
    ConvImpValuta = 0
    Try
      Return oCldFdin.ConvImpValuta(strDittaCorrente, True, dIn, _
                                    NTSCInt(dttET.Rows(0)!tm_valuta), NTSCDate(dttET.Rows(0)!tm_datdoc), _
                                    NTSCDec(dttET.Rows(0)!tm_cambio))

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function CalcolaSommaQtaDTTCollegati(ByRef dttDTT As DataTable) As Decimal
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldFdin.CalcolaSommaQtaDTTCollegati(strDittaCorrente, dttDTT)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function CreaPnfa() As Boolean
    Try
      '------------------------
      'inizializzo BEVEPNFA
      If Not oClePnfa Is Nothing Then Return True

      'ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 128764816973482000, "Inizializzazione motore per contabilizzazione fatture ...")))

      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEVEFDIN", "BEVEPNFA", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oClePnfa = CType(oTmp, CLEVEPNFA)
      '------------------------------------------------
      AddHandler oClePnfa.RemoteEvent, AddressOf GestisciEventiEntityPnfa
      If oClePnfa.Init(oApp, oScript, oCleComm, "", False, "", "") = False Then Return False

      'ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", ".".PadRight(100)))

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Sub GestisciEventiEntityPnfa(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      'gli eventuali messaggi dati da BEVEPNFA tramite la ThrowRemoteEvent li passo a chi mi ha chiamato
      'solo se non sono messaggi dove viene chiesta una conferma ...
      Dim e1 As New NTSEventArgs(e.TipoEvento, oApp.Tr(Me, 130416134507244696, "Messaggio da contabilizzazione documenti:") & vbCrLf & e.Message)
      ThrowRemoteEvent(e1)
      e.RetValue = e1.RetValue
      e1 = Nothing

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function RitornaCodicePagamentoPerScaglioni(ByVal lConto As Integer, _
    ByVal lImportoIn As Decimal, ByRef lImportoOut As Decimal, _
    ByVal nCodpagaIn As Integer, ByRef nCodpagaOut As Integer, ByRef strDespagaOut As String, _
    ByRef bInferioreAImportoMinimo As Boolean) As Boolean
    Try
      'obsoleta
      Return RitornaCodicePagamentoPerScaglioni(lConto, lImportoIn, lImportoOut, nCodpagaIn, nCodpagaOut, strDespagaOut, bInferioreAImportoMinimo, 0, False)

    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function RitornaCodicePagamentoPerScaglioni(ByVal lConto As Integer, _
    ByVal lImportoIn As Decimal, ByRef lImportoOut As Decimal, _
    ByVal nCodpagaIn As Integer, ByRef nCodpagaOut As Integer, ByRef strDespagaOut As String, _
    ByRef bInferioreAImportoMinimo As Boolean, ByRef lImportoOut2 As Decimal, _
    ByRef bInferioreAImportoMassimo As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {lConto, lImportoIn, lImportoOut, nCodpagaIn, nCodpagaOut, strDespagaOut, bInferioreAImportoMinimo, _
                                             lImportoOut2, bInferioreAImportoMassimo})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        lImportoOut = NTSCDec(oIn(2))
        nCodpagaOut = NTSCInt(oIn(3))
        strDespagaOut = NTSCStr(oIn(5))
        bInferioreAImportoMinimo = CBool(oIn(6))
        lImportoOut2 = NTSCDec(oIn(7))
        bInferioreAImportoMassimo = CBool(oIn(8))
        Return CBool(oOut)
      End If
      '----------------


      '--------------------------------------------------------------------------------------------------------------
      Return oCldFdin.RitornaCodicePagamentoPerScaglioni(strDittaCorrente, lConto, _
        lImportoIn, lImportoOut, nCodpagaIn, nCodpagaOut, strDespagaOut, bInferioreAImportoMinimo, _
        lImportoOut2, bInferioreAImportoMassimo)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

End Class
