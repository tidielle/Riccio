Imports System.Data
Imports NTSInformatica.CLN__STD

'31/03/08: E' la base di partenza per tutti i tipi di documenti di magazzino
Imports System
Public Class CLEMGDOCU
  Inherits CLE__BASN

  Public oCldDocu As CLDMGDOCU

  Public BORDINI As Boolean = True              'se true l'entity sta gestendo un ordine, diversamente un documento di magazzino
  Public BGESTORDLIST As Boolean = False        'se TRUE l'entity deve gestire ordlist
  Public bInitializing As Boolean = False

  Public strOldArt As String ' L'articolo precedente
  Public bCambioArticoloScarico As Boolean = False

  'Per determinare se usare la vecchia SettaPrezzo e SettaSconti
  Public bUsaVecchiaSettaPrezzo As Boolean = False
  Public bUsaVecchiaSettaSconti As Boolean = False
  Public bUsaVecchiaSettaProvvigioni As Boolean = False

  Public bUsaVecchiaSettaPrezzoPerDitta As Boolean = False
  Public bUsaVecchiaSettaScontiPerDitta As Boolean = False
  Public bUsaVecchiaSettaProvvigioniPerDitta As Boolean = False

  Public nListinoCalcoloRicaricoMargine As Integer = 0
  Public bMargineValoreUnitario As Boolean = False
  Public bCaricamentoDocumentoOrdine As Boolean = False

#Region "variabili"
  Public strProgName As String = ""     'nome del programma in analisi (BNORGSOR, BNVEBOLL, ...)
  Public bServer As Boolean = True

  Public dttPecx As New DataTable
  Public dttPeve As New DataTable
  Public dttPeac As New DataTable

  Public dsShared As DataSet          'dataset contemente le tabelle testord e movord
  Public dttET As DataTable           'puntatore a dsShared tabella testord/testmag
  Public dttETC As DataTable          'puntatore a dsShared tabella testmagc
  Public dttEC As DataTable           'puntatore a dsShared tabella movord/movmag
  Public dttECTC As DataTable         'puntatore a dsShared tabella movord/movmag TCO
  Public dttECIMP As DataTable        'puntatore a dsShared tabella movord impegni/movmag scarichi collegati
  Public dttECIMPTC As DataTable      'puntatore a dsShared tabella movord impegni/movmag scarichi collegati TCO
  Public dttASSRIS As DataTable       'puntatore a dsShared tabella assris
  Public dttATTIVIT As DataTable      'puntatore a dsShared tabella attivit
  Public dttLAVCENT As DataTable      'puntatore a dsShared tabella lavcent
  Public dttPROESEC As DataTable      'puntatore a dsShared tabella proesec
  Public dttMOVMATR As DataTable      'puntatore a dsShared tabella movmatr
  Public dttMOVCLA As DataTable       'puntatore a dsShared tabella movcla
  Public dttMOVCONA As DataTable      'puntatore a dsShared tabella movcona
  Public dttSCHETRASP As DataTable    'puntatore a dsShared tabella schetrasp

  Public bInUnload As Boolean = False
  Public bHasChangesET As Boolean = False
  Public bDocEmesso As Boolean
  Public lAgControp As Integer
  Public nClscan As Integer
  Public nClpran As Integer
  Public nTabling As Integer

  Public bNew As Boolean
  Public bTerzista As Boolean = False
  Public lContoCF As Integer       'contiene lo stesso valore di td_conto. viene utilizzato nella validazione di td_conto per impostare la destinazione diversa da anagra
  Public lContoNewDoc As Integer = 0          'contiene il conto da proporre nei nuovi documenti
  Public lCrighe As Integer
  Public lCrigheYT As Integer
  Public bProgrCambiato As Boolean = False    'se true in nuovo documento ho forzato il num doc prima di salvare

  Public nCurRow As Integer        'riga di dttEC su cui sono: al campo di riga devo fare i test Pre-Salva-Riga

  Public strMsgCambioDatiTestata As String = ""

  Public dtUlap As DateTime     'data ultimo aaggiornam magazzino da tabanaz
  Public dttAnaz As New DataTable 'datatable aperto sulla dittacorrente
  Public dtInizEser As DateTime 'data di inizio esercizio corrente
  Public dtDataNewDoc As DateTime

  Public bModPM As Boolean
  Public bConai As Boolean
  Public bModCP2 As Boolean
  Public bModuloCRM As Boolean
  Public bModuloAS As Boolean
  Public bModRET As Boolean
  Public bModGPV As Boolean 'Retail 2
  Public bModuloRA As Boolean
  Public bModLEX As Boolean
  Public bModRSM As Boolean
  Public bModCCC As Boolean

  Public bArtConai As Boolean 'presenza di articoli che iniziano per CONAI con modulo attivo

  'per CRM
  Public bIsCRMUser As Boolean = False
  Public bAmm As Boolean = False
  Public strAccvis As String = ""
  Public strAccmod As String = ""
  Public strRegvis As String = ""
  Public strRegmod As String = ""
  Public bAccvis As Boolean = True
  Public bAccmod As Boolean = True

  Public lLead As Integer = 0                     'usato solo negli ordini

  'gestione taglie e colori
  Public bModTCO As Boolean = False
  Public bAttivaRecentTCO As Boolean = False
  Public nAnnoTCO As Integer = 0
  Public nCodstagTCO As Integer = 0

  'le variabili sotto servono per far calcolare o meno il peso o i colli: non li calcolo se li forzo a mano nel textbox relativo
  Public bCalcolaColli As Boolean = True
  Public bCalcolaPesoLordo As Boolean = True
  Public bCalcolaPesoNetto As Boolean = True
  Public bSegueFatt As Boolean = False          'Spese incasso escluse sui doc da riepilogare

  'Costanti e variabili per flag campi modifcati
  Public bModifCastScad As Boolean = False

  'Variabili per personalizzazioni TABPEVE
  Public nPeveIva15 As Integer
  Public nPeveIvaInc As Integer       'codice IVA per spese di trasporto valido fino alla data strPeveIvaIncFine
  Public strPeveIvaInc3Fine As String 'data di fine validità nPeveIvaInc3 (dopo questa data si usa nPeveIvaInc2)
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
  Public strPeacIvaInc3Fine As String 'data di fine validità nPeacIvaInc3 (dopo questa data si usa nPeacIvaInc2)
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

  Public nGLOBPeveTipobf As Integer
  Public nGLOBPeacTipobf As Integer

  'variabili di riga per gestione prezzi e sconti per qta
  Public nPrperqta As Integer = CLN__STD.bsPrSetnoqta
  Public nScperqta As Integer = CLN__STD.bsPrSetnoqta

  'di utilità spicciola ...
  Public bInValidazCodart As Boolean = False   'se TRUE sono dentro alla validazione di mo_codart
  Public bInValidazFaseCodart As Boolean = False   'se TRUE sono dentro alla validazione di mo_fase
  Public bInValidazQuant As Boolean = False    'se TRUE sono dentro alla validazione di mo_quant
  Public bInOnAddNewRow As Boolean = False     'se TRUE sono nella onaddnewrow del corpo 
  Public bInOnAddNewRowImp As Boolean = False  'se TRUE sono nella onaddnewrow degli impegni/scarichi del corpo 
  Public bInOnAddNewRowLavcent As Boolean = False  'se TRUE sono nella onaddnewrow di lavcent 
  Public bNonValidare As Boolean = False       'per evitare che vengano eseguite la BeforeUpdate e la AfterUpdate quando non serve
  Public bInImportRigheOff As Boolean = False  'TRUE durante l'inserimento di righe di CORPO da offerta
  Public bInImportRigheNota As Boolean = False 'TRUE durante l'inserimento di righe di CORPO da nota prelievo
  Public bInImportRigheDiba As Boolean = False 'TRUE durante l'inserimento di righe di CORPO da nota prelievo
  Public bInImportRigheOrd As Boolean = False  'TRUE durante l'inserimento di righe di CORPO da ordine aperto
  Public nInEsplodiDbRicorsiva As Integer = 0  '1 durante l'inserimento l'esplosione ricorsiva della distinta base in un carico da prod
  Public bInImportKit As Boolean = False       'TRUE in fase di inserimenti righe figlie di KIT 
  Public bInImportConai As Boolean = False     'TRUE in fase di inserimenti righe figlie di CONAI
  Public bInInsertArticoDaZoom As Boolean = False       'TRUE in fase di inserimento articoli da zoom multiselezione
  Public bInDuplicadoc As Boolean = False      'TRUE in fase di duplicazione documento
  Public bInEsplodiDiba As Boolean = False     'TRUE solo da SCRIVIRIGADIBA quando per un OP/CP terzista devo ricalcolare il prezzo del padre
  Public strInDuplicadocTiporkOrig As String = ""
  Public bCambioContoGsol As Boolean = False    'TRUE solo da BNORGSOL quando in righe in modifica bambio il fornitore: devo ricalcolare prezzi e sconti
  Public bInIncollaRiga As Boolean = False
  Public bInCreaDocDaGnor As Boolean = False    'TRUE se beorgsor è chiamato da BEORGNOR
  Public bInImportDaPicking As Boolean = False  'TRUE se chiamato da BEPKPICK
  Public bSaltaAfterInsert As Boolean = False   'TRUE non fa eseguire la afterinsert del corpo: settato da BNORGNOR
  Public bDocNonModificabile As Boolean = False           'non posso ne salvare ne cancellare il documento aperto
  Public bNonModificabileperPicking As Boolean = False    'non posso ne salvare ne cancellare il documento aperto
  Public bInCalcoloTempi60DaTempi100 As Boolean = False   'lavorazioni: quando riverso i dati da colonne di db a colonne xx_ non devo riclacolare le colonne db da xx_, altrimenti si crea un loop 
  Public bInNuovoDocSilent As Boolean = False   'TRUE in nuovo doc durante la generazione di documenti o ordini dai programmi elaborativi per non far visualizzare messaggi di tipo YESNO durante il salvataggio del doc/ord
  Public bInSalvaOrdine As Boolean = False
  Public bInApriDocSilent As Boolean = False    'TRUE in apri doc durante la generazione di documenti o ordini dai programmi elaborativi per non far visualizzare messaggi di tipo YESNO durante il salvataggio del doc/ord
  Public bCalcolaScadUsaSempreDatdoc As Boolean = False
  Public bDocDaRetail As Boolean = False ' TRUE se gestione documenti viene chiamato dal Retail 2
  Public bAggDatConsFigliSuQtaPadre As Boolean = False
  Public bDocDaFattChiamate As Boolean = False ' TRUE se gestione documenti viene chiamato dalla fatturazione chiamate
  Public bInvalidazEcValore As Boolean = False  'TRUE se sono nella aftercolupdate di ec_valore
  Public strControlloPIvaCodFis As String = ""
  Public bRicalcolaValoriRiga As Boolean = False 'TRUE durante elborazione "Ricalcola Prezzi\Sconti\Provvigioni"
  Public bIgnoraBloccoQtaTaglieInvf As Boolean = False
  Public bConsentiScontiSuOmaggi As Boolean = False 'Se true consente di indicare gli sconti sugli omaggi

  Public bEndEditET As Boolean = True          'SERVE SOLO PER VELOCIZZARE L'USCITA DAL CAMPO COD CONTO    (altrimenti è lentissimo... - l'operatività ed i controlli eseguiti sono gli stessi sia che la variabile sia settata su true che su false)
  Public bEndEditEC As Boolean = True          'SERVE SOLO PER VELOCIZZARE L'USCITA DAL CAMPO COD ARTICOLO (altrimenti è lentissimo... - l'operatività ed i controlli eseguiti sono gli stessi sia che la variabile sia settata su true che su false)
  Public bEndEditETimp As Boolean = True       'stessa cosa per impegni collegati
  Public bEndEditECimp As Boolean = True       'stessa cosa per impegni collegati 
  Public bEraBloccato As Boolean = False       'per blocco clienti 

  Public strTipovalOut As String = ""

  Public lModuliDittaDitt As Integer = 0
  Public lModuliExtDittaDitt As Integer = 0
  Public lModuliSupDittaDitt As Integer = 0
  Public lModuliSupExtDittaDitt As Integer = 0
  Public lModuliPtnDittaDitt As Integer = 0
  Public lModuliPtnExtDittaDitt As Integer = 0

  'variabili per BeforeColUpdate
  Public dttEt_conto As New DataTable           'contiene sempre anagra del conto intestatario del documento
  Public dttArti As New DataTable               'contiene ARTICO dell'ultimo articolo analizzato. serve per evitare inutili query su artico (se l'articolo non è cambiato, evito la query e riciclo dttArti)

  Public bCancellaScarichi As Boolean = False   'al cambio di riga se ho cambiato articolo o fase o ... cancello gli scarichi collegati
  Public bGestionePrezzi As Boolean = False     'opzione di registro per determinare se sono gestiti i prezzi per unità di misura diversa dalla principale
  Public bControllaCommessaChiusa As Boolean = False 'se true in validazione codice commessa controlla se in comess la commessa è chiusa
  Public bOrdOpenCambioTpbfChiediCambioMagaz As Boolean = False 'se TRUE, in modifica documento se si cambia il TIPOBF con tutte le righe del corpo INEVASE cambia magazzini, controp testata ecc come se il documento fosse nuovo
  Public nTipoVettore0Coddest As Integer = 0    'tipo di impostazione vettore da destdiv: 0: se la destinaz. viene impostaata in automatico con l'inserimento del conto usa vettore conto (vecchio comportamento sbagliato - mantenuto per compatibilità), 1: vettore = 0 su destdiv vuol dire usa vettore di anagra, -1: importa vettore da destdiv anche se codice vettore = 0 (ha sempre priorità su quello di anagra)

  Public bCambioQuantita As Boolean = False     'settate nella RecordBeforeInsertUpdate per poi essere utilizzate nella AfterUpdate per far chiedere: visto che è cambiata la quantotà cambiarla nache negli scarichi collegati?
  Public bCambioSconti As Boolean = False
  Public dRapportoQta As Decimal = 0            'come sopra
  Public bCambioFlevas As Boolean = False       'come sopra
  Public lDiffDatcons As Integer = 0            'come sopra
  Public bCambioCommessa As Boolean = False

  Public bTableCodarfoEmpty As Boolean = False  'se true, in validazione articolo se non viene trovato in artico viene cercato in codarfo
  Public bTableBarcodeEmpty As Boolean = False  'se true, in validazione articolo se non viene trovato in artico viene cercato in barcode

  Public bNuovoDaImportExport As Boolean = False

  Public dtrHT As DataRow = Nothing             'normalmente = nothing: se validato è la riga di carico da prod (H o T) a cui si riferiscono le righe di carico (U o Y)
  Public dsImpe As New DataSet                  'dataset per eventi di gestione griglia impegni/scarichi collegati
  Public dsTtat As New DataSet                  'dataset per eventi di gestione griglia lavorazioni collegate (attivit)
  Public dsTtas As New DataSet                  'dataset per eventi di gestione griglia lavorazioni collegate (assris)
  Public dsLavc As New DataSet                  'dataset per eventi di gestione griglia lavorazioni collegate (lavcent)
  Public nListinoScarichi As Integer = 0        'listino da utilizzare per gli scarichi collegati

  Public nFasi As Integer = 0                   'per TCO, utilizzato nell'espolosizone della distinta base per scrittura scarichi/impegni collegati
  Public dsTabmaga As New DataSet               'tabella tabmaga: la carico in memoria in form_load per velocizzare poi la 'ValorizzaProduzione'
  Public dsTabciva As New DataSet               'tabella tabciva: la carico in memoria in form_load per velocizzare ...
  Public dsTabcove As New DataSet               'tabella tabcove: la carico in memoria in form_load per velocizzare ...
  Public dsTabcena As New DataSet               'tabella tabcena: la carico in memoria in form_load per velocizzare ...
  Public bAccontiVerificati As Boolean = False
  Public dRRfenceArtPadre As Decimal = 0        'utilizzato in esplosione DB per non fare una query su artico per ogni riga di impegno/scarico
  Public dFcorrltArtPadre As Decimal = 0        'utilizzato in esplosione DB per non fare una query su artico per ogni riga di impegno/scarico

  Public lTmNumIT As Integer = 0                'Personalizzazione TM
  Public bDocRetail As Boolean = False          'se TRUE il documento aperto è stato generato da RETAIL: non si può salvare/cancellare

  Public strProgChiamante As String             'utilizzato soltanto se richiamato da BNORGNNP
  Public bIgnoraDisponibilita As Boolean = False

  Public dtControllaConcorrenzaOggettiDataLock As DateTime = DateTime.Now
  Public bDocumentLockNoSave As Boolean = False     'se true il documento non potrà essere salvato/cancellato perchè in apertura un altro utente aveva il doc in modifica

  'dbsece
  Public dsSeceShared As DataSet
  Public lSeceIITTTabcent As Integer
  Public bSeceAnnullato As Boolean
  Public nSeceCodcent As Integer

  'bdloma
  Public dsLomaShared As DataSet
  Public bLomaHasChanges As Boolean
  Public lIITTartico As Integer
  Public bModificaMateriali As Boolean

  'mghllo
  Public dsHlloShared As DataSet
  Public nHlloMagaz As Integer
  Public nHlloFase As Integer
  Public lHlloLotto As Integer
  Public strHlloUbicaz As String
  Public dHlloQuant As Decimal
  Public bHlloAnnullato As Boolean
  Public nTipoGestArticolo As Integer
  Public strHlloCodart As String
  Public strHlloDesart As String
  Public strHlloDesmaga As String
  Public nHlloTipoGestArt As Integer

  'mghla2
  Public strCodart As String
  Public dsHla2Shared As DataSet

  'mgsctr
  Public dsSctrShared As DataSet
  Public dsSctrTestata As DataSet
  Public bSctrHasChanges As Boolean = False
  Public bSctrIsLoad As Boolean = False
  Public dSctrColli As Decimal
  Public dSctrQuat As Decimal
  Public dSctrPeso As Decimal

  ' Logistica di magazzino su palmare
  Public bPropUbicazNonUsareSoloArtico As Boolean
  Public bModSupRme As Boolean

  Public strPrevCelValueImp As String = ""
  Public strPrevCelValueAtt As String = ""
  Public strPrevCelValueAss As String = ""
  Public strPrevCelValueLav As String = ""

  'Project Management
  Public lEseddaes As Integer = 0     '--- Identifica il record di PROESEB associato alla riga di movord
  Public lIItttasks As Integer = 0
  Public lIIttproesebappo As Integer = 0
  Public strOut As String
  Public strDaraveout As String
  Public strDaravedef As String
  Public strTipoTask As String
  Public strGestcost As String ' da togliere?
  Public strPrevGrup As String ' da togliere?
  '--- Variabili di form per singoli task (tutti i costi e le qta in formato DA)
  Public dFWorkres As Decimal
  'Public dFWorkrim as Decimal
  Public dFQtares As Decimal
  Public dFCostores As Decimal
  Public dFQtarim As Decimal
  Public dFCostorim As Decimal
  Public dFQtadisda As Decimal
  Public dFValdisda As Decimal
  Public dFQtadis As Decimal
  Public dFValdis As Decimal
  Public strFSalcon As String
  Public strFDarave As String
  Public strFPrevgrup As String
  Public strFGestcost As String
  Public dBasecost As Decimal
  Public dBasecostun As Decimal
  Public dBasework As Decimal
  Public dBaseqta As Decimal
  Public dQtadispass As Decimal
  Public dValdispass As Decimal
  Public bEranewrk As Boolean
  Public bPMControllaBaseCostMG As Boolean
  Public bPMSoloTaskRilasciatiMG As Boolean
  Public dQuant1 As Decimal
  Public dQuant2 As Decimal
  Public bInSalvarigaPM As Boolean = False
  Public bInSistemaCampiRIMDISDaCampiIMPQTAPM As Boolean = False
  'fine PM
  Public bNoCalcolaTotali As Boolean = False 'Blocca la CalcolaTotali, viene usato dal Retail, visto che usa una sua CalcolaTotali diversa da quelle di gestione ordini\gestione documenti

  Public bNonStampareFigliKitSintetici As Boolean = False
  Public bSetStasinoSuRigheKit As Boolean = False

  Public bLottoNew As Boolean = False   'se true la gestione lotti è alfanumerica (da net 2012), diversamente è alfanum ma accetta solo lotti numerici di max 9 char (formattati tipo '000000001')
  Public bCallPnfa As Boolean = False   'se true, al salva in new/update di documenti gestiti da bnvepnfa (eccetto corrispettivi), dopo il salvataggio lancio pnfa per la contabilizzazione
  Public strCallPnfa As String = "N"    'se bCallPnfa = true, "S" = documenti sia attivi che passivi, A = solo ciclo attivo, P = solo ciclo passivo

  'gestione di accconf (limitazioni funzionali su tipobf/causale di magaz/magaz di testata dipendneti da tipork, serie, opreatore,gruppo operat
  Public dttAccconf As New DataTable            'datatable contenente i blocchi su vis/mod/canc per l'utente/gruppo a cui appartiene l'utente (per CG blocco solo su causale contabile)
  Public nCodCaumOpen As Integer = 0            'su registrazione in modifica, contiene tm_codcaum presente all'apertura della registrazione
  Public nCodCaumimpOpen As Integer = 0
  Public nCodtpbfOpen As Integer = 0
  Public nCodmagaOpen As Integer = 0
  Public nCodmaga2Open As Integer = 0
  Public nCodmagimpOpen As Integer = 0

  Public bRilevaCodPagaCambioContoDocModif As Boolean = False
  Public dColliRowEnter As Decimal = 0      'colli quando sono entrato nella riga
  Public dQtaRowEnter As Decimal = 0        'qta quando sono entrato nella riga

  Public bLetturaBCinArtRilevaDati As Boolean

  Public bLayoAnnullato As Boolean = False

  Public oCondCommerciali As NTSCondCommerciali

  'mgcacl
  Public dsCaclShared As DataSet

  'Promozioni
  Public dttOmaggi As New DataTable
  Public dttPromozioni As DataTable
  Public dsPromozioniOmaggi As DataSet
  Public bInCalcoloPromozioni As Boolean
  Public bNoCambioDataContratto As Boolean

  Public bSalvaNuovoDocApplicaPromo As Boolean
#End Region

#Region "Opzioni di Registro"
  Public bDisabilitaCheckAnnoData As Boolean = False
  Public bChiediDestInTestata As Boolean = False
  Public strVisNoteConto As String = "N"
  Public strVisNoteArti As String = "N"
  Public bRiportaNote1CF As Boolean = False
  Public bRiportaNote2CF As Boolean = False
  Public nRiportaNoteDaAnaext As Integer = 0
  Public bDisabilitaDecodificaBarcode As Boolean = False
  Public bProponiArtSostitutivo As Boolean = False
  Public nProponiArtSostitutivo As Integer = 0
  Public nIncremContatoreRiga As Integer = 1
  Public bUsaMagStockArticolo As Boolean = False
  Public bBD_CodArfo As Boolean = False
  Public bRilevaDisponibPerCommessa As Boolean = False
  Public strDispMultiMag As String = "N"
  Public bForzaLetturaPrezziQta As Boolean = False
  Public bForzaLetturaScontiQta As Boolean = False
  Public strVisMemList1 As String = "N"
  Public nVisMemNumList As Integer = 1
  Public bProponiQtaDaAss As Boolean = False
  Public bMovimQtaLotti As Boolean = False
  Public nTrattaSoloCliFornDellAgente As Integer = 0
  Public nTestCostoCaricoListino1 As Integer = 0
  Public bTestCostoCaricoAncheSL As Boolean = False
  Public strTestEsistQuando As String = " "
  Public bIgnoraMagTestata1Ordine As Boolean = False
  Public nSpeseDaRipartire As Integer = 0
  Public bDisabilitaObblContoLavoro As Boolean = False
  Public nMagAccettazione As Integer = 0
  Public bRiproponiDataDoc As Boolean = False
  Public bRiportaCostiLavOPT As Boolean = False
  Public bTmp501 As Boolean = False
  Public bConsMultiEvasRigheOrdNote As Boolean = False
  Public bIncassoTotalePagContanti As Boolean = False
  Public bFiltroMagazRigheOrdini As Boolean = False
  Public nFiltroMagazRigheOrdiniEscludiTipobf As Integer = 0
  Public bStornaProvvAcconti As Boolean = False
  Public bNoEvasioneOrdSospesi As Boolean = False
  Public bConfermaRigaBarcode As Boolean = False
  Public bCodartDaBarcode As Boolean = False  'NON DOCUMENTARE: se true, quando entro nella colonna ec_codart o ec_matric e la cella è vuota, faccio scatenare un FRECCIA IN GIU' per far apparire il cursore nella cella, altrimenti ci sono stati casi che se la cella non ha il cursore, i primi 2 caratteri si invertono
  Public bRiapriDocSuSalva As Integer = 0
  Public bDopoStpChiamaStpRegCassa As Boolean = False
  Public bNonRiportareLottiDaCLav As Boolean = False
  Public bMantieniCausaliRigheNota As Boolean = False
  Public bMantieniCausaliTestata1Nota As Boolean = False
  Public bCarichiProdDaAvmg As Boolean = False
  Public bRifClavManuale As Boolean = False
  Public bRideterminaProvvsuRigheOrd As Boolean = False
  Public bSpeseIncSoloSuPrimaFatt As Boolean = False
  Public bNoAddebitoSpeincSuNoteCredito As Boolean = False
  Public bDisabilitaObblContoCASuScarichiSL As Boolean = False
  Public bAggMovClaFasiPerCommessa As Boolean = False
  Public bAttribRifCLavAttSuScarProdArtFasi As Boolean = False
  Public bAggMovClaFasiFiltroCliente As Boolean = False
  Public bRendiBloccantiAlertCLavoro As Boolean = False
  Public bDisabilitaCheckDateAnteriori As Boolean = False
  Public bNonProporreEvasioneIPSuNotePrel As Boolean = False
  Public bNonMostrareImpegniPrenotASaldo As Boolean = False
  Public bNonEreditareDateCompDaOrd As Boolean = False
  Public bProponiFornSuZ As Boolean = False
  Public bPassaCommessaTestataHLMO As Boolean = False
  Public bIgnoraTipobfTestata1Ordine As Boolean = False
  Public nIgnoraTipobfTestata1Ordine As Integer = 0
  Public bSegnalaEvasioneOrdBloccati As Boolean = False
  Public bConsentiCreazDocumCliFornBloccoFisso As Boolean = False
  Public bConfermacompilazioneSchedaTrasp As Boolean = False
  Public bSegnalaCreazDocumCliFornBloccati As Boolean = False
  Public bBloccaAgente1 As Boolean = False
  Public nRicalcPrezziOrd As Integer = 0
  Public nRicalcPrezziNota As Integer = 0
  Public nRicalcScontiOrd As Integer = 0
  Public nRicalcScontiNota As Integer = 0
  Public nRicalcProvvOrd As Integer = 0
  Public nRicalcProvvNota As Integer = 0
  Public bChiediContoOrdini As Boolean = False
  Public bDisabilitaCheckLotti As Boolean = False
  Public bDisabilitaCheckUbicaz As Boolean = False
  Public bDisabilitaCheckFasi As Boolean = False
  Public bDisabilitaCheckMatricole As Boolean = False
  Public bProteggiDocContab As Boolean = False
  Public bProteggiDocFatt As Boolean = False
  Public nCO2_ArrQuant As Integer = 3
  Public bTMclav As Boolean = False
  Public bDisableCheckRegCassa As Boolean = False
  Public bRiportaCambio1Ord As Boolean = False
  Public bRiportaCambio1Nota As Boolean = False
  Public bNoMsgCommessaDiversa As Boolean = False
  Public bChiamaCollSilent As Boolean = False
  Public bPrelistIvato As Boolean = False
  Public bCheckLottiInScarico As Boolean = False
  Public strCheckLottiInScaricoCome As String = "E"
  Public nCheckUbicazInScarico As Integer = 0
  Public strCheckUbicazInScaricoCome As String = "E"
  Public bAddScontiPerPeso As Boolean = False
  Public bContoNuoviDoc As Boolean = False
  Public nContoNuoviDocQuale As Integer = 0
  Public bModifTempiLavSuQtaScart As Boolean = False
  Public nConaiListinoContributo As Integer = 1
  Public bConaiDisableCheckCodart As Boolean = False
  Public bNoMsgCongruenzaConto As Boolean = False
  Public bEvadendoRiportaSempreDatiTrasp As Integer = 0
  Public bGestPVR As Boolean = False
  Public bCollega_MG_DI As Boolean = False
  Public bSelDichAutomaticamente As Boolean = False
  Public dPercControlloImportoDich As Decimal = 0
  Public bAbilOrdiniBarcode As Boolean = True
  Public bNoMsgCongruenzaMagimp As Boolean = False
  Public nGestioneMatricSuNoteDiPrel As Integer = 0
  Public bNoModifQuantSuColli As Boolean = False
  Public lNoModifQuantSuColli As Integer = 0
  Public bBarcodeConQta As Boolean = False
  Public bBarcodeDerogaQta As Boolean = False
  Public nArrotonda5Cent As Integer = 0
  Public bTestCostoCaricoInferiore As Boolean = False
  Public dTestCostoCaricoPercDiff As Decimal = 0
  Public nTestCostoCaricoListino2 As Integer = 0
  Public bTestCostoCaricoFinale As Boolean = False
  Public bTestCostoCaricoRiga As Boolean = False
  Public bAggScarichiMateriali As Boolean = True
  Public bApriAnagraficaPrivata As Boolean = False
  Public bEliminaMessaggioCambioDatiTest As Boolean = False
  Public bMantieniMagRigheOrdine As Boolean = False
  Public bSegnalaDiffMagaz As Boolean = False
  Public strRipartiSpeaccSuValstat As String = "N"
  Public bUsaKeyMag As Boolean = False
  Public strAutoNumProt As String = "N"
  Public bModifColliSuQuant As Boolean = False
  Public bGenNumCommecaAutR As Boolean = False
  Public bGenNumCommecaAutR_AllaFine As Boolean = True
  Public bDisabilitaMsgPrezzoZero As Boolean = False
  Public bDisabilitaMsgQuantZero As Boolean = False
  Public strTestScomin As String = "N"
  Public strContrFidoInsolinInsDoc As String = "N"
  Public strCalcPesi As String = "N"
  Public strTestScominCome As String = "A"
  Public strTestEsistPWD As String = ""
  Public strTestEsist As String = "N"
  Public strTipoTestEsist As String = "0"
  Public strTestEsistArtTc As String = "C"
  Public strTestSottoCosto As String = "N"
  Public strTestSottoCostoQuando As String = "A"
  Public strTestSottoCostoTipo As String = "U"
  Public strTestSottoCostoZero As String = "S"
  Public bTestSottoCostoModificaRigheEsist As Boolean
  Public bTest_esistenza_duplimpprod As Boolean = False
  Public bTestEsistScortaMultiMag As Boolean = False
  Public bTestEsistPerComm As Boolean = False
  Public strTestEsistCome As String = "E"
  Public bCalcolaColliPesiSuDocAperti As Boolean = False
  Public bUsaVettore1perCalcoloSpeseTrasp As Boolean = False
  Public strCalcolaSpeseTrasp As String = "N"
  Public bCalcolaSpeseTraspSoloNuovi As Boolean = False
  Public strTestSottoPWD As String = ""
  Public bPMControllaBaseCostOR As Boolean = False
  Public bPMSoloTaskRilasciatiOR As Boolean = False
  Public strTipoGenNumCommeca As String = "1"
  Public bDuplicaAllole As Boolean = False
  Public strContrFidoInsolinInsOrd As String = "N"
  Public bSegnalaCreazOrdiniCliFornBloccati As Boolean = False
  Public strVisNoteContoApertura As String = " "
  Public strTipValSemPf As String = " "
  Public strTipoListinoMat As String = " "
  Public bReprintDoc As Boolean = False
  Public strTestSottoCostoModifica As String = "N"
  Public bTestSottoCostoBolMovInt As Boolean = False
  Public bTestSottoCostoOscuraCosto As Boolean = False
  Public dTestSottoCostoRicaricoMinimo As Decimal = 0
  Public strNoteNuoviDocumenti As String = ""
  Public gstrTipoArtConf As String = "0"
  Public bIgnoraMagDistintaBase As Boolean = False
  Public bEreditaMagImpCli As Boolean = False
  Public bLeggiOpzioniDoc As Boolean = False
  Public bRiportaCommDaTestataDupl As Boolean = False
  Public gbUsaFiltroCommessa As Boolean = True
  Public gbUsaFiltroPadre As Boolean = True
  Public bNoDatValDistinta As Boolean = False
  Public nEsplosioneDbRicorsiva As Integer = 0
  Public fRicalcPrez As Boolean = False
  Public fRicalcScon As Boolean = False
  Public fRicalcProv As Boolean = False
  Public fSelArticDiv As Boolean = False
  Public fSelContoDiv As Boolean = False
  Public gbRiportaFasiFantasmi As Boolean = False
  Public strFormatQtaEsistCorpo As String = "0.000"
  Public lRilevaDisponibCodCommessa As Integer = 9999
  Public bDisabilitaCheckCommesse As Boolean = False
  Public bDuplicaR2HSoloNonPrenot As Boolean = False
  Public bDuplicaR2HEreditaRifImpCli As Boolean = False
  Public bDuplicaR2HRagSocSuRiferim As Boolean = False
  Public bDuplicaR2HEscludiArtNoDiba As Boolean = False
  Public bDuplicaR2HSoloArtGesComm As Boolean = False
  Public bTestCoerenzaCentriLavMagaz As Boolean = False
  Public bNoCheckFidoSuPrev As Boolean = False
  Public nSetStatoRigaOrdlist As Integer = 0
  Public bDescrAggKitSuNote As Boolean = False
  Public bNoColliSuFigliKit As Boolean = False
  Public bAbilitaPromozioni As Boolean = False
  Public nContropDefaultOfferte As Integer = 0
  Public bNonRiportareDatiTestOA As Boolean = False
  Public bUsaCondPagContoFatt As Boolean = True
  Public nUsaCondPagContoFatt As Integer = -1
  Public bPrioritaCodeseArticolo As Boolean = False
  Public bDeterminaBolliSuOperazEsenti As Boolean = False
  Public bSalvaCondizioniFinali As Boolean = False
  Public nSalvaCondizioniFinaliClientiDove As Integer = 0
  Public nSalvaCondizioniFinaliFornitoriDove As Integer = 0
  Public bNonCalcolaColli As Boolean = False
  Public strSoloConfermate As String = "0"
  Public bNoTempiSuTerzisti As Boolean = False
  Public bNomeDocWordNumero As Boolean = False
  Public strElencoMagazzini As String = ""
  Public bConsentiCreazOrdiniCliFornBloccoFisso As Boolean = False
  Public bRilevaBloccoDaAnagra As Boolean = False
  Public bNoAttribRifCLav As Boolean = False
  Public bDisabilitaControlliSubcomm As Boolean = False
  Public bGestAlert As Boolean = False
  Public strDescrLinguaRifAcc As String = ""
  Public bEsplodiD As Boolean = True
  Public bGestAnaext As Boolean = False
  Public bNoPesiSuRigheKitFittizie As Boolean = False
  '--- Flags che indicano se riportare o meno incassato e/o abbuono, in importazione ordini/note
  Public bRiportaIncassato1Ord As Boolean = False
  Public bRiportaAbbuono1Ord As Boolean = False
  Public bRiportaIncassato1Nota As Boolean = False
  Public bRiportaAbbuono1Nota As Boolean = False
  Public bNonRiportareAbiCabBancaInEvasione As Boolean = False
  Public bRiportaPesoColliNota As Boolean = False
  Public bSalvaAncheSeNonModificato As Boolean = False
  Public bApriAnalottiDopoNew As Boolean = False    '(solo con gestione lotti alfanumerici abilitata): se true, dopo che è stata creata in automatico una anagrafica lotti, quest'ultima verrà aperta (no in modalità batch e/o in acquisizione massiva righe di documento)
  Public bAbilitaGenerazLottoSuOrdini As Boolean = False   '(solo con gestione lotti alfanumerici abilitata): se true la generazione lotti automatica è abilitata anche negli ordini oltre che nei documenti di magazzino
  Public nControllaConcorrenzaOggetti As Integer = 0
  Public bLottoUnivoco As Boolean = False           'se true al salvataggio del documento controllo che per gli articoli gestiti a lotti il lotto sia univoco (ovvero non esistano altri articoli con lo stesso analotti.alo_lottox oltre a quello della riga che sto validando
  Public bDatIvaComeDatdoc As Boolean = False       'se TRUE la data di competenza IVA verrà impostata sempre uguale alla data documento (al cambio della data documento non farà domande)
  Public nInibisciTipiArticoliDiversi As Integer = 0  'se -1 al salva di ordini/documenti di vendita non fa salvare se sono presenti articoli con ar_tipo diversi (escludendo gli ar_tipo = '' o = null e articoli descrittivi) Il tutto serve per poter differenziare, sulla base del DL n° 1 del 24/1/2012 (convertito con la legge n° 27 del 24/3/2012, e in vigore dal 24/10/2012), la vendita di PRODOTTI DETERIORABILI ENTRO O OLTRE 60ggi da parte dei produttori.
  Public nInibisciTipiArticoliDiversiTrannePagContanti As Integer = 0
  Public strNomeCampoArtico As String = ""
  Public strDataInibisciArticoliDiversi As String = ""
  Public bAggiornaQtaFigliKitDaPadre As Boolean = False 'se TRUE al cambio della qta di un padre din un KIT cambio proporzionalmente la qta dei figli
  Public bArticoliDiversiChiediConferma As Boolean = False 'se attiva, al salvataggio di un documento, nel caso di blocco su articoli di tipo diverso, chiede conferma dell'operazione anziché inibirne il salvataggio
  Public bUsaAggContropAnagra As Boolean = False      'se TRUE, nell contropartite lette da BNVEPEVE e BNMGPEAC viene aggiunto il campo anagra.an_agcontrop e se il sottocont esiste viene preo quello, altrienti quello senza an_agcontrop
  Public nRiportaDescrNoteDaCaricoCLAV As Integer = 0
  Public bNoModificaUbicCambioMaga As Boolean = False

  Public bInBNNPIENP As Boolean = False   'TRUE (alzato da bInBNNPIENP) in fase generazione documento da BNNPIENP
  Public strNetProDB As String = "" 'stringa di connessione allo schedulatore Net@Pro: se presente al salva/cancella gli ordini devono essere aggiornati anche sullo schedulatore
  Public bNetProOrdLock As Boolean = False  'se TRUE il documento non è modificabile/cancellabile perchè in evasione su Net@Pro

  Public bMsgPrezzoZeroFinale As Boolean = False '(-1/0 <-- default) Se attiva, in presenza dell’opzione “DisabilitaMsgPrezzoZero” settata a 0, avvisa anche a fine documento con un unico messaggio la presenza di eventuali righe con prezzo a 0.
  Public nDatiniCompMese As Integer = 0         'se -1, nel campo 'data inizio competenza eeconomica' viene proposto il primo del mese, altrimenti datdoc: serve per chi gestisce i conti a data comp. economica ma vuole fare sempre analisi mensili, senza che pnfa generi molte righe di prinot/priana2
  Public bFlagRielabFattDiffSuVariazioneDDT As Boolean = False

  Public bGestioneAbbinamentiTaglie As Boolean = False

  Public lRiportaMisura1DaArtico As Integer = 0
  Public lRiportaMisura2DaArtico As Integer = 0
  Public lRiportaMisura3DaArtico As Integer = 0

  Public bGnnpCheckFidoInsolu_NoBlocco As Boolean = False
  Public bGnbfCheckFidoInsolu_NoBlocco As Boolean = False

  Public bSpezzaDocArt62Deterior As Boolean

  Public bCalcolaPrezzoScontoProvvAncheSeRigaEvade As Boolean = False
  Public nGiorniMargineScadenzaLotto As Integer = 9999
  Public bValoreStatisticoNoScontoPagamento As Boolean = False

  'Promozioni
  Public strStampaRigaOmaggi As String = ""
  Public lCodivaOmaggi As Integer = 0
  Public strPromoNoStornoResi As String = ""
  Public strOmaggiDesel As String = ""
  Public lCausaleScontiPiede As Integer = 0
  Public strTipiRkNoPromozioni As String = ""

  'Variabili opzioni da registro GLOBALI lette all'inizio per gestione della cartella \Bsorgsor\OpzioniDoc\
  Public strGLOBContrFidoInsolinInsOrd As String
  Public strGLOBVisMemList1 As String
  Public strGLOBVisNoteConto As String
  Public strGLOBVisNoteArticolo As String
  Public strGLOBVisNoteContoApertura As String
  Public strGLOBTestScomin As String
  Public strGLOBTestScominCome As String
  Public strGLOBTestEsist As String
  Public strGLOBTipoTestEsist As String
  Public strGLOBTestSottoCosto As String
  Public strGLOBTestSottoCostoQuando As String
  Public strGLOBTestSottoCostoTipo As String
  Public strGLOBTestSottoCostoZero As String
  Public dGLOBTestSottoCostoRicaricoMinimo As Decimal
  Public strGLOBTestSottoCostoModifica As String
  Public strGLOBCalcolaSpeseTrasp As String
  Public bGLOBCalcolaSpeseTraspSoloNuovi As Boolean = False
  Public bGLOBUsaVettore1perCalcoloSpeseTrasp As Boolean
  Public strGLOBNoteNuoviDocumenti As String
  Public bGLOBRiportaNote1CF As Boolean
  Public bGLOBRiportaNote2CF As Boolean
  Public nGLOBRiportaNoteDaAnaext As Integer = 0
  Public fGLOBSelContoDiv As Boolean
  Public bGLOBChiediDestInTestata As Boolean
  Public bGLOBRilevaDisponibPerCommessa As Boolean
  Public strGLOBTestEsistCome As String
  Public bGLOBTestEsistPerComm As Boolean
  Public bGLOBDisabilitaMsgPrezzoZero As Boolean
  Public bGLOBDisabilitaMsgQuantZero As Boolean
  Public bGLOBUsaMagStockArticolo As Boolean
  Public bGLOBMovimQtaLotti As Boolean
  Public nGLOBVisMemNumList As Integer
  Public bGLOBModifColliSuQuant As Boolean
  Public bGLOBSalvaCondizioniFinali As Boolean
  Public nGLOBSalvaCondizioniFinaliClientiDove As Integer = 0
  Public nGLOBSalvaCondizioniFinaliFornitoriDove As Integer = 0
  Public strGLOBCalcPesi As String
  Public strGLOBContrFidoInsolinInsDoc As String
  Public strGLOBTestEsistQuando As String
  Public strGLOBTestEsistArtTc As String
  Public bGLOBUsaKeyMag As Boolean
  Public bGLOBMantieniMagRigheOrdine As Boolean
  Public bGLOBEliminaMessaggioCambioDatiTest As Boolean
  Public bGLOBIgnoraMagTestata1Ordine As Boolean
  Public bGLOBRiproponiDataDoc As Boolean
  Public nGLOBRicalcPrezziOrd As Integer
  Public nGLOBRicalcPrezziNota As Integer
  Public nGLOBRicalcScontiOrd As Integer
  Public nGLOBRicalcScontiNota As Integer
  Public nGLOBRicalcProvvOrd As Integer
  Public nGLOBRicalcProvvNota As Integer
  Public bGLOBChiediContoOrdini As Boolean
  Public bGLOBIncassoTotalePagContanti As Boolean
  Public bGLOBFiltroMagazRigheOrdini As Boolean
  Public nGLOBFiltroMagazRigheOrdiniEs As Integer
  Public bGLOBRiapriDocSuSalva As Integer
  Public bGLOBDopoStpChiamaStpRegCassa As Boolean
  Public bGLOBEvadendoRiportaSempreDatiTrasp As Integer
  Public bGLOBDisabilitaCheckDateAnteriori As Boolean
  Public bGLOBContoNuoviDoc As Boolean
  Public nGLOBContoNuoviDocQuale As Integer
  Public nGLOBFiltroMagazRigheOrdiniEscludiTipobf As Integer
  Public bGLOBConsentiCreazDocumCliFornBloccoFisso As Boolean = False
  Public bGLOBConfermacompilazioneSchedaTrasp As Boolean = False
  Public bGLOBApriAnalottiDopoNew As Boolean = False
  Public bGLOBAbilitaGenerazLottoSuOrdini As Boolean = False
  Public nGLOBInibisciTipiArticoliDiversi As Integer = 0
  Public strGLOBDataInibisciArticoliDiversi As String = ""
  Public bGLOBArticoliDiversiChiediConferma As Boolean = False
  Public bGLOBMsgPrezzoZeroFinale As Boolean
  Public bGLOBSpezzaDocArt62Deterior As Boolean
  Public strQuandoAgContropManca As String = "0"
#End Region

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGDOCU"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldDocu = CType(MyBase.ocldBase, CLDMGDOCU)
    oCldDocu.Init(oApp)
    strFormatQtaEsistCorpo = oApp.FormatQta

    VerificaUsoSettaCondCommerciali()

    Return True
  End Function


  Public Overridable Sub VerificaUsoSettaCondCommerciali()
    Dim strTEMP As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      LeggiSettaggioCondizioniCommerciali
      '--------------------------------------------------------------------------------------------------------------
      If Not Me.GetType.ToString.StartsWith("NTSInformatica.CLE") Then 'Se inizia con CLE non è personalizzata
        'ottengo i metodi della classe ereditata (sono presenti solo quelli ereditati e specifici della classe figlia)
        For Each method As System.Reflection.MethodInfo In Me.GetType.GetMethods(System.Reflection.BindingFlags.Public Or _
                                                                                 System.Reflection.BindingFlags.Instance Or _
                                                                                 System.Reflection.BindingFlags.DeclaredOnly)
          Select Case method.Name.ToLower
            Case "settaprezzo" : bUsaVecchiaSettaPrezzo = True
            Case "settasconti" : bUsaVecchiaSettaSconti = True
            Case "settaprovvigioni" : bUsaVecchiaSettaProvvigioni = True
          End Select
        Next
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function InitExt() As Boolean
    Dim dttTmp As New DataTable
    Try
      '--------------------------------------------------------------------------------------------------------------
      LeggiSettaggioCondizioniCommerciali()
      '--------------------------------------------------------------------------------------------------------------
      lModuliDittaDitt = ModuliDittaDitt(strDittaCorrente)
      lModuliExtDittaDitt = ModuliExtDittaDitt(strDittaCorrente)
      lModuliSupDittaDitt = ModuliSupDittaDitt(strDittaCorrente)
      lModuliSupExtDittaDitt = ModuliSupExtDittaDitt(strDittaCorrente)
      lModuliPtnDittaDitt = ModuliPtnDittaDitt(strDittaCorrente)
      lModuliPtnExtDittaDitt = ModuliPtnExtDittaDitt(strDittaCorrente)

      strMsgCambioDatiTestata = oApp.Tr(Me, 127792257204531250, "Il cambio della data, del Cliente o Fornitore, della destinazione diversa 1, degli agenti e del " & _
                                            "listino, con righe inserite nel corpo del documento, può comportare " & _
                                            "incongruità nel prezzo di listino, negli sconti e nelle provvigioni applicate.")

      '---------------------------------
      bModTCO = False
      If CBool((lModuliExtDittaDitt And CLN__STD.bsModExtTCO)) Then bModTCO = True

      bConai = False
      If CBool((lModuliDittaDitt And bsModCN)) Then bConai = True

      bModPM = False
      If CBool((lModuliDittaDitt And CLN__STD.bsModPM)) Then bModPM = True

      bModCP2 = False
      If CBool((lModuliExtDittaDitt And CLN__STD.bsModExtCP2)) Then bModCP2 = True

      bModuloCRM = False
      If CBool((lModuliExtDittaDitt And CLN__STD.bsModExtCRM)) Then bModuloCRM = True

      bModuloAS = False
      If CBool((lModuliDittaDitt And CLN__STD.bsModAS)) Then bModuloAS = True

      bModRET = False
      If CBool((lModuliExtDittaDitt And CLN__STD.bsModExtRET)) Then bModRET = True

      bModGPV = False
      If CBool((lModuliSupDittaDitt And CLN__STD.bsModSupGPV)) Then bModGPV = True

      bModuloRA = False
      If CBool((lModuliDittaDitt And CLN__STD.bsModRA)) Then bModuloRA = True

      bModLEX = False
      If CBool((lModuliExtDittaDitt And CLN__STD.bsModExtLEX)) OrElse CBool((lModuliExtDittaDitt And CLN__STD.bsModExtTCP)) Then bModLEX = True

      bModCCC = False
      If CBool((lModuliSupDittaDitt And CLN__STD.bsModSupCCC)) Then
        bModCCC = True
        'Verifica se in presenza del modulo CCC ci possono essere dei vincoli causati dall'utilizzo di una vecchia cerca prezzo\cerca sconti
        If bUsaVecchiaSettaPrezzo OrElse bUsaVecchiaSettaPrezzoPerDitta Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130790025310351021, _
                "Attezione! Il modulo Contatti e Condizioni Commerciali è attivo, " & _
                "ma a causa di una personalizzazione sulla ricerca del prezzo o dell'opzione di registro 'OPZIONI\UsaVecchiaCercaPrezzo' " & _
                "alcune promozioni potrebbero non essere applicate correttamente.")))
        End If
        If bUsaVecchiaSettaSconti OrElse bUsaVecchiaSettaScontiPerDitta Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130790027251953808, _
                "Attezione! Il modulo Contatti e Condizioni Commerciali è attivo, " & _
                "ma a causa di una personalizzazione sulla ricerca degli sconti o dell'opzione di registro 'OPZIONI\UsaVecchiaCercaSconti' " & _
                "alcune promozioni potrebbero non essere applicate correttamente.")))
        End If
      End If


      '---------------------------------
      'leggo tabpeve e tabpeac
      If Not LeggePevePeac() Then Return False

      '---------------------------------
      dttAnaz.Clear()
      oCldDocu.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", "", dttAnaz)
      If dttAnaz.Rows.Count = 0 Then Return False
      dtUlap = NTSCDate(dttAnaz.Rows(0)!tb_dtulap)

      '---------------------------------
      'determina la data di inizio esercizio
      dtInizEser = oCldDocu.GetDataInizioEsercizioCorrente(strDittaCorrente)
      If dtInizEser = NTSCDate(IntSetDate("01/01/1900")) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128654490143125000, "Attenzione! Manca o non è valido nella Anagrafica Ditta il codice esercizio contabile corrente. Impossibile proseguire.")))
        Return False
      End If
      dtInizEser = DateAdd("d", -1, dtInizEser)

      'Data nuovo documento
      dtDataNewDoc = NTSCDate(DateTime.Now.ToShortDateString)

      '---------------------------------
      'Test per vedere se si vuole attivare la gestione dei prezzi riferiti ad una u.d.m. diversa dalla principale
      nTipoVettore0Coddest = NTSCInt(oCldDocu.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "TipoVettore0Coddest", "0", " ", "0"))
      bGestionePrezzi = CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "AbilitaPrezzoUM", "0", " ", "0"))
      bOrdOpenCambioTpbfChiediCambioMagaz = CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "OrdOpenCambioTpbfChiediCambioMagaz", "0", " ", "0"))
      bControllaCommessaChiusa = CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "ControllaCommessaChiusa", "0", " ", "0"))
      bPropUbicazNonUsareSoloArtico = CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSVEBOLL", "OPZIONI", ".", "PropUbicazNonUsareSoloArtico", "0", " ", "0"))
      nUsaCondPagContoFatt = NTSCInt(oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSVEBOLL", "Opzioni", ".", "UsaCondPagContoFatt", "-1", " ", "-1")) ' se abilitata a -1 in fase di selezione ordini/impegni aperti non riporta i dati di testata dell'ord. aperto originaria (opera come in Business8)
      If nUsaCondPagContoFatt = -1 Then
        bUsaCondPagContoFatt = True
      Else
        bUsaCondPagContoFatt = False
      End If
      nControllaConcorrenzaOggetti = NTSCInt(oCldDocu.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "ControllaConcorrenzaOggetti", "0", " ", "0"))
      bLottoUnivoco = CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "LottoUnivoco", "0", " ", "0"))
      bUsaAggContropAnagra = CBool(oCldDocu.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "UsaAggContropAnagra ", "0", " ", "0"))

      If strProgName <> "" Then   'strProgName= "" = bsorgsol
        nEsplosioneDbRicorsiva = NTSCInt(oCldDocu.GetSettingBusDitt(strDittaCorrente, "BS" & strProgName.ToUpper.Substring(2), "Opzioni", ".", "EsplosioneDbRicorsiva", "0", " ", "0"))
      End If
      strNetProDB = oCldDocu.GetSettingBus("Opzioni", ".", ".", "MESDB_" & strDittaCorrente, "", " ", "").Trim
      If CBool((lModuliSupDittaDitt And bsModSupNTP)) = False Then strNetProDB = ""

      If BGESTORDLIST = False Then
        's etrue in modifica dellaqta del kit padre aggiorno la qta del kit figlio
        bAggiornaQtaFigliKitDaPadre = CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, IIf(BORDINI, "BSORGSOR", "BSVEBOLL").ToString, "Opzioni", ".", "AggiornaQtaFigliKitDaPadre", "0", " ", "0"))
      End If

      If bModPM Then
        lIItttasks = oCldDocu.GetTblInstId("TTTASKS", False)
        lIIttproesebappo = oCldDocu.GetTblInstId("TTPROESEC", False)
        If BORDINI = False Then
          bPMControllaBaseCostMG = CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "PMControllaBaseCostMG", "0", " ", "0"))
          bPMSoloTaskRilasciatiMG = CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "PMSoloTaskRilasciatiMG", "0", " ", "0"))
        Else
          bPMControllaBaseCostOR = CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "PMControllaBaseCostOR", "0", " ", "0"))
          bPMSoloTaskRilasciatiOR = CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "PMSoloTaskRilasciatiOR", "0", " ", "0"))
        End If
      End If

      If Not CaricaTabelleCache() Then Return False

      '---------------------------------
      'se c'è il modulo della CA2 i campi di tabpecx obbligatori devono essere inseriti
      If CBool(lModuliSupDittaDitt And bsModSupCAE) Then
        If dttPecx.Rows.Count = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129270449930839843, "Attenzione! Con il modulo della contabilità analitica 'duplice contabile' attivato deve essere correttamente compilata la tabella di 'personalizzazione contabilità analitica ditta'. Impossibile proseguire.")))
          Return False
        End If
      End If

      bNonStampareFigliKitSintetici = False
      If strProgName.Trim <> "" Then
        bNonStampareFigliKitSintetici = CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "BS" & strProgName.ToUpper.Substring(2), "OPZIONI", ".", "NonStampareFigliKitSintetici", "0", " ", "0"))
      End If

      '---------------------------------
      'determino se devo gestire i lotti 'alfanumerici puri' o in compatibilità con versioni precedenti a net 2012 (solo numerici)
      bLottoNew = False
      bCallPnfa = False
      strCallPnfa = "N"
      oCldDocu.ValCodiceDb(strDittaCorrente, strDittaCorrente, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
        strCallPnfa = NTSCStr(dttTmp.Rows(0)!ac_contabft)
        bCallPnfa = CBool(IIf(strCallPnfa = "N", False, True))
      End If
      dttTmp.Clear()

      If BGESTORDLIST = False Then
        If bModuloCRM Then
          bIsCRMUser = CType(CType(oCleComm, CLELBMENU).oMenu, CLE__MENU).IsCrmUser(strDittaCorrente, bAmm, strAccvis, strAccmod, strRegvis, strRegmod)
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

  Public Overridable Function IstanziaNTSPromozioni() As NTSPromozioni
    Try
      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEVEBOLL", "BN__STD.NTSPromozioni", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 127791222114531250, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return Nothing
      End If
      Return CType(oTmp, NTSPromozioni)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return Nothing
  End Function

  Public Overridable Sub Belbmenu_GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      Select Case e.TipoEvento
        Case "CalcolaTotali" : CalcolaTotali()
        Case "bNoSalvaRiga" ' bNoSalvaRiga = (e.Message = "-1")
        Case "SettaValoriRiga" : SettaValoriRiga(dttEC.Rows(NTSCInt(e.Message)))
        Case "SettaPrezzoSconti"
          Dim dttTmp As New DataTable
          Dim dtrEc As DataRow = dttEC.Rows(NTSCInt(e.Message))
          oCldDocu.ValCodiceDb(NTSCStr(dtrEc!ec_codart), strDittaCorrente, "ARTICO", "S", , dttTmp)
          If dttTmp.Rows.Count > 0 Then
            bInValidazCodart = True 'Per forzare il ricalcolo del prezzo
            SettaCondCommerciali(True, True, False, dtrEc, NTSCInt(dttTmp.Rows(0)!ar_clascon), nClscan, False)
            bInValidazCodart = False
            SettaValoriRiga(dtrEc)
          End If
        Case "SettaProvvigioni"
          SettaCondCommerciali(False, False, True, dttEC.Rows(NTSCInt(e.Message)), 0, 0, True)
        Case Else : ThrowRemoteEvent(e)
      End Select
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

  Public Overridable Sub RemoveHandlerEntity()
    Try
      RemoveHandler CType(oCleComm, CLELBMENU).RemoteEvent, AddressOf Belbmenu_GestisciEventiEntity
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


  '-----------------------
  Public Overridable Function CaricaTabelleCache() As Boolean
    Try
      '---------------------------------
      'memorizzo tutto tabmaga, tabciva e tabcove per velocizzare poi le query in insert righe articoli
      Return oCldDocu.CaricaTabelleCache(strDittaCorrente, dsTabmaga, dsTabciva, dsTabcove, dsTabcena, dttPecx)
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
  Public Overridable Function GetTabmagaRow(ByVal nMagaz As Integer) As DataRow
    GetTabmagaRow = Nothing
    Dim dtrT() As DataRow = Nothing
    Dim dttTmp As New DataTable
    Try
      dtrT = dsTabmaga.Tables("TABMAGA").Select("tb_codmaga = " & nMagaz.ToString)
      If dtrT.Length > 0 Then
        Return dtrT(0)
      Else
        oCldDocu.ValCodiceDb(nMagaz.ToString, strDittaCorrente, "TABMAGA", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          CaricaTabelleCache()
          Return dttTmp.Rows(0)
        End If
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
  Public Overridable Function GetTabcivaRow(ByVal nCodCiva As Integer) As DataRow
    GetTabcivaRow = Nothing
    Dim dtrT() As DataRow = Nothing
    Dim dttTmp As New DataTable
    Try
      dtrT = dsTabciva.Tables("TABCIVA").Select("tb_codciva = " & nCodCiva.ToString)
      If dtrT.Length > 0 Then
        Return dtrT(0)
      Else
        oCldDocu.ValCodiceDb(nCodCiva.ToString, strDittaCorrente, "TABCIVA", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          CaricaTabelleCache()
          Return dttTmp.Rows(0)
        End If
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
  Public Overridable Function GetTabcoveRow(ByVal nCodCove As Integer) As DataRow
    GetTabcoveRow = Nothing
    Dim dtrT() As DataRow = Nothing
    Dim dttTmp As New DataTable
    Try
      dtrT = dsTabcove.Tables("TABCOVE").Select("tb_codcove = " & nCodCove.ToString)
      If dtrT.Length > 0 Then
        Return dtrT(0)
      Else
        oCldDocu.ValCodiceDb(nCodCove.ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          CaricaTabelleCache()
          Return dttTmp.Rows(0)
        End If
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
  Public Overridable Function GetTabcenaRow(ByVal nCodCena As Integer) As DataRow
    GetTabcenaRow = Nothing
    Dim dtrT() As DataRow = Nothing
    Dim dttTmp As New DataTable
    Try
      dtrT = dsTabcena.Tables("TABCENA").Select("tb_codcena = " & nCodCena.ToString)
      If dtrT.Length > 0 Then
        Return dtrT(0)
      Else
        oCldDocu.ValCodiceDb(nCodCena.ToString, strDittaCorrente, "TABCENA", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          CaricaTabelleCache()
          Return dttTmp.Rows(0)
        End If
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
  '-----------------------

  Public Overridable Function LeggePevePeac() As Boolean
    Dim dttTmp As New DataTable
    Try

      '---------------------------------
      'leggo tabpeve
      If Not oCldDocu.GetPevePeac(dttPeve, dttPeac) Then Return False

      If dttPeve.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101093750, "Tabella 'Personalizzazione Vendite' non compilata. Impostarla prima di proseguire")))
        Return False
      Else
        nPeveIva15 = NTSCInt(dttPeve.Rows(0)!tb_iva15)
        nPeveIvaInc = NTSCInt(dttPeve.Rows(0)!tb_ivainc)
        nPeveIvaInc2 = NTSCInt(dttPeve.Rows(0)!tb_ivainc2)
        nPeveIvaInc3 = NTSCInt(dttPeve.Rows(0)!tb_ivainc3)
        strPeveIvaInc3Fine = NTSCDate(dttPeve.Rows(0)!tb_datfinivainc3).ToShortDateString
        strPeveIvaInc2Fine = NTSCDate(dttPeve.Rows(0)!tb_datfinivainc2).ToShortDateString
        nPeveTipobf = NTSCInt(dttPeve.Rows(0)!tb_vtipobf)
        lPeveContro = NTSCInt(dttPeve.Rows(0)!tb_vcontro)
        nPeveMagazz = NTSCInt(dttPeve.Rows(0)!tb_vmagazz)
        nPeveCaumag = NTSCInt(dttPeve.Rows(0)!tb_vcaumag)
        nPeveListin = NTSCInt(dttPeve.Rows(0)!tb_vlistin)
        strPeveScorpo = dttPeve.Rows(0)!tb_vscorpo.ToString
        nPeveCautra = NTSCInt(dttPeve.Rows(0)!tb_vcautra)
        strPeveAcurad = dttPeve.Rows(0)!tb_vacurad.ToString
        strPeveDatora = dttPeve.Rows(0)!tb_vdatora.ToString
        lConclpriv = NTSCInt(dttPeve.Rows(0)!tb_conclpriv)
      End If

      '---------------------------------
      'leggo tabpeac
      If dttPeac.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101250000, "Tabella 'Personalizzazione Acquisti' non compilata. Impostarla prima di proseguire")))
        Return False
      Else
        nPeacIva15 = NTSCInt(dttPeac.Rows(0)!tb_aiva15)
        nPeacIvainc = NTSCInt(dttPeac.Rows(0)!tb_aivainc)
        nPeacIvaInc2 = NTSCInt(dttPeac.Rows(0)!tb_aivainc2)
        nPeacIvaInc3 = NTSCInt(dttPeac.Rows(0)!tb_aivainc3)
        strPeacIvaInc3Fine = NTSCDate(dttPeac.Rows(0)!tb_adatfinivainc3).ToShortDateString
        strPeacIvaInc2Fine = NTSCDate(dttPeac.Rows(0)!tb_adatfinivainc2).ToShortDateString
        nPeacTipobf = NTSCInt(dttPeac.Rows(0)!tb_atipobf)
        lPeacContro = NTSCInt(dttPeac.Rows(0)!tb_acontro)
        nPeacMagazz = NTSCInt(dttPeac.Rows(0)!tb_amagazz)
        nPeacCaumag = NTSCInt(dttPeac.Rows(0)!tb_acaumag)
        nPeacListin = NTSCInt(dttPeac.Rows(0)!tb_alistin)
        nPeacListinCStd = NTSCInt(dttPeac.Rows(0)!tb_listcstd)
        nPeacScapro = NTSCInt(dttPeac.Rows(0)!tb_causcapro)
        strPeacScorpo = dttPeac.Rows(0)!tb_ascorpo.ToString
      End If

      nGLOBPeveTipobf = nPeveTipobf
      nGLOBPeacTipobf = nPeacTipobf

      oCldDocu.ValCodiceDb("1", strDittaCorrente, "TABBOTR", "N", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129407035511728516, "Tabella 'Bolli' non compilata. Impostarla prima di proseguire")))
        Return False
      Else
        'se ho indicato dei codici di esenzione che faranno applicare dei bolli, in peve e peac il codice di 
        'escluso art 15 è obbligatorio (diversamente si verificheranno errori vaghi in pnfa o blocco di loop 
        'infinito se si entra in 'visualizza castelletti di bnveboll'
        If NTSCInt(dttTmp.Rows(0)!tb_codesb_1) <> 0 Or NTSCInt(dttTmp.Rows(0)!tb_codesb_2) <> 0 Or _
           NTSCInt(dttTmp.Rows(0)!tb_codesb_3) <> 0 Or NTSCInt(dttTmp.Rows(0)!tb_codesb_4) <> 0 Then
          If nPeacIva15 = 0 Or nPeveIva15 = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129737704676638777, "Attenzione: con indicati dei codici di esenzione nella 'Tabella bolli', in personalizzaz. acquisti e personalizzaz. vendite il campo 'Cod. IVA escluso art. 15' deve essere compilato")))
          End If
        End If
      End If

      If CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupCAE) Then
        If NTSCInt(dttPeac.Rows(0)!tb_acontrriac) = 0 Or NTSCInt(dttPeac.Rows(0)!tb_acontrriin) = 0 Or _
           NTSCInt(dttPeac.Rows(0)!tb_acontrimba) = 0 Or NTSCInt(dttPeac.Rows(0)!tb_acontrribo) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129437084538447265, "Con il modulo 'Contabilità analitica duplice contabile' " & _
                                                "attivato è obbigatorio aver compilato in personalizzazione " & _
                                                "acquisti le seguenti CONTROPARTITE:" & vbCrLf & _
                                                "- SPESE DI TRASPORTO" & vbCrLf & _
                                                "- SPESE INCASSO" & vbCrLf & _
                                                "- SPESE IMBALLO" & vbCrLf & _
                                                "- BOLLI")))
          Return False
        End If
        If NTSCInt(dttPeve.Rows(0)!tb_contrriac) = 0 Or NTSCInt(dttPeve.Rows(0)!tb_contrriin) = 0 Or _
           NTSCInt(dttPeve.Rows(0)!tb_contrimba) = 0 Or NTSCInt(dttPeve.Rows(0)!tb_contrribo) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129437086847890625, "Con il modulo 'Contabilità analitica duplice contabile' " & _
                                                "attivato è obbigatorio aver compilato in personalizzazione " & _
                                                "vendite le seguenti CONTROPARTITE:" & vbCrLf & _
                                                "- SPESE DI TRASPORTO" & vbCrLf & _
                                                "- SPESE INCASSO" & vbCrLf & _
                                                "- SPESE IMBALLO" & vbCrLf & _
                                                "- BOLLI")))
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
      Return False
    End Try
  End Function

  Public Overridable Function TabEmpty(ByVal strDitta As String, ByRef bAgenti As Boolean, ByRef bNoteprel As Boolean) As Boolean
    Try
      '---------------------------------
      'ritorna false se la tabella agenti o note di prelievo è compilata

      Return oCldDocu.TabEmpty(strDitta, bAgenti, bNoteprel)

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


  Public Overridable Function ResetVar() As Boolean
    Try
      'non resettare le variabili qui sotto commentate: in apertura documento verrebbero riresettate:
      'vengono settate correttamente in fase di inserimento/modifica nuovi doc e riletti in apertura doc esistenti
      'nClscan = 0
      'nClpran = 0
      'nTabling = 0
      'lContoCF = 0
      bDocumentLockNoSave = False
      bCalcolaColli = True
      bCalcolaPesoLordo = True
      bCalcolaPesoNetto = True
      bSegueFatt = False
      bModifCastScad = False
      bNetProOrdLock = False
      bInBNNPIENP = False

      bNew = False
      bAccvis = True
      bAccmod = True
      lLead = 0
      lCrighe = 0
      lCrigheYT = 0
      bTerzista = False
      bEraBloccato = False    'per blocco clienti
      bProgrCambiato = False
      bCancellaScarichi = False
      lDiffDatcons = 0
      dRapportoQta = 0
      bCambioFlevas = False
      bCambioQuantita = False
      bCambioSconti = False
      bCambioCommessa = False
      bNuovoDaImportExport = False
      dtrHT = Nothing
      nListinoScarichi = 0
      dsImpe.Clear()
      bInImportRigheOff = False
      bInImportRigheOrd = False
      bInImportRigheNota = False
      bInImportRigheDiba = False
      bInImportKit = False
      bInImportConai = False
      bInInsertArticoDaZoom = False
      bInDuplicadoc = False
      strInDuplicadocTiporkOrig = ""
      bInIncollaRiga = False
      bSaltaAfterInsert = False
      bInCreaDocDaGnor = False

      bDocNonModificabile = False
      bNonModificabileperPicking = False
      bAccontiVerificati = False
      bInCalcoloTempi60DaTempi100 = False
      bDocRetail = False

      If bModPM Then
        oCldDocu.ResetTblInstId("TTPROESEC", False, lIIttproesebappo)
        oCldDocu.ResetTblInstId("TTTASKS", False, lIItttasks)
      End If

      nCodCaumOpen = 0
      nCodCaumimpOpen = 0
      nCodtpbfOpen = 0
      nCodmagaOpen = 0
      nCodmaga2Open = 0
      nCodmagimpOpen = 0

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


  Public Overridable Function GetMemDttArti(ByVal strCodart As String) As DataTable
    'serve per evitare di fare query inutili su artico: una volta che l'articolo è letto lo memorizzo in locale per usi futuri
    Try
      If dttArti Is Nothing Then
        oCldDocu.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttArti)
        Return dttArti
      ElseIf dttArti.Rows.Count = 0 Then
        oCldDocu.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttArti)
        Return dttArti
      Else
        If dttArti.Rows(0)!ar_codart.ToString.ToUpper <> strCodart.ToUpper And strCodart.Trim <> "" Then
          dttArti.Clear()
          oCldDocu.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttArti)
          Return dttArti
        Else
          Return dttArti
        End If
      End If

      Return Nothing

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return Nothing
    End Try
  End Function


  Public Overridable Function CheckExpireDate(ByVal strDatdoc As String) As Boolean
    Dim lRow As Integer = 0
    Dim strDataScadenze As String = NTSCDate(IIf(CLN__STD.FRIENDLY = True, _
                                                 oApp.ActKey.DataScad, _
                                                 DateAdd(DateInterval.Month, 1, NTSCDate(oApp.ActKey.DataScad))).ToString).ToShortDateString

    Try
      If bNew = False Then Return False

      If NTSCDate(strDatdoc) > NTSCDate(strDataScadenze) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526534944286000, _
          "La data del documento, che si cerca di salvare, (|" & NTSCDate(strDatdoc).ToShortDateString & "|) " & _
          "è successiva " & _
          IIf(CLN__STD.FRIENDLY = True, "alla", "di oltre un mese la").ToString & _
          " data di scadenza della chiave di attivazione del programma" & _
          " (|" & NTSCDate(oApp.ActKey.DataScad).ToShortDateString & "|)." & vbCrLf & vbCrLf & _
          "Impossibile proseguire.")))
        Return True
      End If

      If oApp.ActKey.Demo = "S" Then
        lRow = oCldDocu.GetNrecTot(strDittaCorrente, BORDINI)
        If lRow > 500 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526534928686000, _
                          "Questa è una versione dimostrativa del prodotto pertanto non consente di inserire oltre i 500 movimenti d'ordine / movimenti di magazzino : attualmente sono presenti |" & lRow & "| movimenti." & vbCrLf & vbCrLf & "Impossibile proseguire.")))
          Return True
        End If
      End If

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

  Public Overridable Function LegNuma(ByVal strTipo As String, ByVal strSerie As String, ByVal nAnno As Integer) As Integer
    Dim strTipoProg As String = ""
    Try
      strTipoProg = strTipo
      Select Case strTipo
        Case "A", "D", "E", "N", "£" : strTipoProg = "A"
        Case "B" : strTipoProg = "B"
        Case "C" : strTipoProg = "C"
        Case "M" : strTipoProg = "M"
        Case "Z" : strTipoProg = "Z"
        Case "L", "K", "J", "(" : strTipoProg = "L"
        Case "W" : strTipoProg = "W"
        Case "T" : strTipoProg = "T"
        Case "U" : strTipoProg = "U"
        Case "F", "I" : strTipoProg = "F"
        Case "S" : strTipoProg = "S"
        Case "V" : strTipoProg = "VV"
      End Select
      Return oCldDocu.LegNuma(strDittaCorrente, strTipoProg, strSerie, nAnno, False)
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

      strTipoProg = dttET.Rows(0)!et_tipork.ToString
      Select Case dttET.Rows(0)!et_tipork.ToString
        Case "A", "D", "E", "N", "£" : strTipoProg = "A"
        Case "B" : strTipoProg = "B"
        Case "C" : strTipoProg = "C"
        Case "M" : strTipoProg = "M"
        Case "Z" : strTipoProg = "Z"
        Case "L", "K", "J", "(" : strTipoProg = "L"
        Case "W" : strTipoProg = "W"
        Case "T" : strTipoProg = "T"
        Case "U" : strTipoProg = "U"
        Case "F", "I" : strTipoProg = "F"
        Case "S" : strTipoProg = "S"
        Case "V" : strTipoProg = "VV"
      End Select

      lProgr = oCldDocu.AggNuma(strDittaCorrente, strTipoProg, dttET.Rows(0)!et_serie.ToString, _
                                NTSCInt(dttET.Rows(0)!et_anno), NTSCInt(dttET.Rows(0)!et_numdoc), _
                                True, True, strDalMessage)
      If strDalMessage <> "" Then
        '-----------------------------------------------
        'il progressivo  stato utilizzato da un altro: correggo testord e movord 
        If lProgr <> NTSCInt(dttET.Rows(0)!et_numdoc) Then
          dttET.Rows(0)!et_numdoc = lProgr
          For i = 0 To dttEC.Rows.Count - 1
            dttEC.Rows(i)!ec_numdoc = lProgr
          Next
        End If
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

  Public Overridable Sub Scorporo(ByVal dIvaInclusa As Decimal, ByVal nCodiceIva As Integer, ByRef dImponibile As Decimal, ByRef dImposta As Decimal, Optional ByVal nDec As Integer = -1)
    Try
      oCldDocu.Scorporo(dIvaInclusa, nCodiceIva, dImponibile, dImposta, nDec)
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


  Public Overridable Function ZoomSedoc(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                           ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String, _
                           ByVal strDataA As String, ByVal nTipobfDa As Integer, ByVal nTipobfA As Integer, _
                           ByVal strRiferim As String, ByVal lNumpar As Integer, ByVal strDitta As String) As DataSet
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strTipork, nAnno, strSerie, _
                                             lContoDa, lContoA, strDataDa, _
                                             strDataA, nTipobfDa, nTipobfA, _
                                             strRiferim, lNumpar, strDitta})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CType(oOut, DataSet)
      End If
      '----------------

      Return ZoomSedoc(strTipork, nAnno, strSerie, lContoDa, lContoA, strDataDa, strDataA, nTipobfDa, _
                       nTipobfA, strRiferim, lNumpar, strDitta, "", "")

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return Nothing
    End Try
  End Function
  Public Overridable Function ZoomSedoc(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                         ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String, _
                         ByVal strDataA As String, ByVal nTipobfDa As Integer, ByVal nTipobfA As Integer, _
                         ByVal strRiferim As String, ByVal lNumpar As Integer, ByVal strDitta As String, _
                         ByVal strWhereFiltriEstesi As String, ByVal strWhereFiltriEstesiMovmag As String) As DataSet
    Try
      Return oCldDocu.ZoomSedoc(strTipork, nAnno, strSerie, lContoDa, lContoA, strDataDa, strDataA, nTipobfDa, _
                                nTipobfA, strRiferim, lNumpar, strDitta, strWhereFiltriEstesi, strWhereFiltriEstesiMovmag)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return Nothing
    End Try
  End Function

  '-------------------------------------

  Public Overridable Function NuovoDocumento(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                             ByVal nAnno As Integer, ByVal strSerie As String, _
                                             ByVal lNumdoc As Integer) As Boolean
    Dim i As Integer = 0
    Try
      ResetVar()

      bNew = True

      CType(oCleComm, CLELBMENU).AccconfGetBlocchi(strDittaCorrente, strTipoDoc, dttAccconf)

      '----------------------------------------
      'svuoto movord
      'non spostare queste righe prima di aggiungere la nuova riga, 
      'diversamente vengono scatenati tutti gli eventi di datatable movord con righe dentro!!!
      dttEC.Clear()
      'dttET.Clear()                            'non posso farlo: diversamente scateno tutti gli eventi di validazione di testord che daranno il messaggio di campo che non accetta valore null (visto che il datatable è vuoto ...)
      dsShared.Tables("CORPO").Clear() : dsShared.Tables("CORPO").AcceptChanges()
      dsShared.Tables("CORPOIMP").Clear() : dsShared.Tables("CORPOIMP").AcceptChanges()
      If bModTCO Then
        dsShared.Tables("CORPOTC").Clear() : dsShared.Tables("CORPOTC").AcceptChanges()
        dsShared.Tables("CORPOIMPTC").Clear() : dsShared.Tables("CORPOIMPTC").AcceptChanges()
      End If
      If bModPM Then
        dsShared.Tables("PROESEC").Clear() : dsShared.Tables("PROESEC").AcceptChanges()
      End If
      If BORDINI = False Then
        dsShared.Tables("TESTAC").Clear() : dsShared.Tables("TESTAC").AcceptChanges()
        dsShared.Tables("MOVMATR").Clear() : dsShared.Tables("MOVMATR").AcceptChanges()
        dsShared.Tables("MOVCONA").Clear() : dsShared.Tables("MOVCONA").AcceptChanges()
        dsShared.Tables("MOVCLA").Clear() : dsShared.Tables("MOVCLA").AcceptChanges()
        dsShared.Tables("LAVCENT").Clear() : dsShared.Tables("LAVCENT").AcceptChanges()
      Else
        dsShared.Tables("ATTIVIT").Clear() : dsShared.Tables("ATTIVIT").AcceptChanges()
        dsShared.Tables("ASSRIS").Clear() : dsShared.Tables("ASSRIS").AcceptChanges()
      End If
      dsShared.Tables("SCHETRASP").Clear() : dsShared.Tables("SCHETRASP").AcceptChanges()

      If BORDINI = False Then
        '----------------------------------------
        'creo una nuova riga di testmagc/testprbc
        dttETC.Rows.Add(dsShared.Tables("TESTAC").NewRow)
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
      End If

      '----------------------------------------
      'creo una nuova riga di testmag
      dttET.Rows.Add(dsShared.Tables("TESTA").NewRow)
      dttET.Rows(dttET.Rows.Count - 1)!codditt = strDittaCorrente
      dttET.Rows(dttET.Rows.Count - 1)!et_tipork = strTipoDoc
      dttET.Rows(dttET.Rows.Count - 1)!et_anno = nAnno
      dttET.Rows(dttET.Rows.Count - 1)!et_serie = strSerie
      dttET.Rows(dttET.Rows.Count - 1)!et_numdoc = lNumdoc
      If bRiproponiDataDoc = True And bDocEmesso = True Then
        dttET.Rows(dttET.Rows.Count - 1)!et_datdoc = dtDataNewDoc
      Else
        dttET.Rows(dttET.Rows.Count - 1)!et_datdoc = NTSCDate(DateTime.Now.ToShortDateString)
      End If
      If BORDINI Then dttET.Rows(dttET.Rows.Count - 1)!et_datcons = NTSCDate(DateTime.Now.ToShortDateString)
      dttET.Rows(dttET.Rows.Count - 1)!et_ultagg = Now
      If bModTCO And bAttivaRecentTCO Then
        dttET.Rows(dttET.Rows.Count - 1)!et_annotco = nAnnoTCO
        dttET.Rows(dttET.Rows.Count - 1)!et_codstag = nCodstagTCO
      End If

      '----------------------------------------
      'nel datatable c'è ancora il vecchio documento in memoria: lo elimino 
      '(visto chce non l'ho potuto fare nella Salva o nella Ripristina perchè diversamente nella tabella
      'a cui sono collegati i controlli non ci sarebbe stato alcun record e tutti i controlli avrebbero dato il messaggio
      'di valore impostato non corretto)
      dsShared.Tables("TESTA").AcceptChanges()
      If dttET.Rows.Count > 1 Then
        For i = 0 To dttET.Rows.Count - 2
          dttET.Rows(0).Delete()
        Next
        dsShared.Tables("TESTA").AcceptChanges()
      End If

      '----------------------------------------
      If bDocEmesso Then
        If BORDINI = False Then dttET.Rows(0)!et_causale = nPeveCaumag
        If BORDINI = False Then dttET.Rows(0)!et_listino = nPeveListin
        dttET.Rows(0)!et_acuradi = strPeveAcurad
        If oApp.oGvar.bGestAccconf = True And oApp.oGvar.nGestAccconfMagaz = 0 Then
          'non posso proporre il tipobf, visto che l'utente non potrebbe essere abilitato ad utilizzarlo
          dttET.Rows(0)!et_tipobf = 0
        Else
          dttET.Rows(0)!et_tipobf = oCldDocu.ProponiTipoBFdaTRKTPBF(strDittaCorrente, _
                                                                    NTSCStr(dttET.Rows(0)!et_tipork), _
                                                                    NTSCStr(dttET.Rows(0)!et_serie))
          If NTSCInt(dttET.Rows(0)!et_tipobf) = 0 Then
            Select Case NTSCStr(dttET.Rows(0)!et_tipork)
              Case "A" : dttET.Rows(0)!et_tipobf = dttPeve.Rows(0)!tb_tipobfrka
              Case "B" : dttET.Rows(0)!et_tipobf = dttPeve.Rows(0)!tb_tipobfrkb
              Case "C" : dttET.Rows(0)!et_tipobf = dttPeve.Rows(0)!tb_tipobfrkc
              Case "E" : dttET.Rows(0)!et_tipobf = dttPeve.Rows(0)!tb_tipobfrke
              Case "F" : dttET.Rows(0)!et_tipobf = dttPeve.Rows(0)!tb_tipobfrkf
              Case "I" : dttET.Rows(0)!et_tipobf = dttPeve.Rows(0)!tb_tipobfrki
              Case "N" : dttET.Rows(0)!et_tipobf = dttPeve.Rows(0)!tb_tipobfrkn
              Case "S" : dttET.Rows(0)!et_tipobf = dttPeve.Rows(0)!tb_tipobfrks
              Case "R", "V" : dttET.Rows(0)!et_tipobf = dttPeve.Rows(0)!tb_tipobfrkr
              Case "X" : dttET.Rows(0)!et_tipobf = dttPeve.Rows(0)!tb_tipobfrkx
              Case "Q" : dttET.Rows(0)!et_tipobf = dttPeve.Rows(0)!tb_tipobfrkq
              Case "#" : dttET.Rows(0)!et_tipobf = dttPeve.Rows(0)!tb_tipobfrko
            End Select
            If NTSCInt(dttET.Rows(0)!et_tipobf) = 0 Then dttET.Rows(0)!et_tipobf = nPeveTipobf
          End If
        End If
        'i campi sotto vengono caricati dalla routine beforecollupdateTestord
        'dttET.Rows(dttET.Rows.Count - 1)!et_controp = lPeveContro
        'dttET.Rows(dttET.Rows.Count - 1)!et_magaz = nPeveMagazz
      Else
        If BORDINI = False Then dttET.Rows(0)!et_causale = nPeacCaumag
        If BORDINI = False Then dttET.Rows(0)!et_listino = nPeacListin
        If strTipoDoc = "T" Then dttET.Rows(0)!et_causcap = nPeacScapro
        If oApp.oGvar.bGestAccconf = True And oApp.oGvar.nGestAccconfMagaz = 0 Then
          'non posso proporre il tipobf, visto che l'utente non potrebbe essere abilitato ad utilizzarlo
          dttET.Rows(0)!et_tipobf = 0
        Else
          dttET.Rows(0)!et_tipobf = oCldDocu.ProponiTipoBFdaTRKTPBF(strDittaCorrente, _
                                                                    NTSCStr(dttET.Rows(0)!et_tipork), _
                                                                    NTSCStr(dttET.Rows(0)!et_serie))
          If NTSCInt(dttET.Rows(0)!et_tipobf) = 0 Then
            Select Case NTSCStr(dttET.Rows(0)!et_tipork)
              Case "J" : dttET.Rows(0)!et_tipobf = dttPeac.Rows(0)!tb_tipobfrkj
              Case "L" : dttET.Rows(0)!et_tipobf = dttPeac.Rows(0)!tb_tipobfrkl
              Case "M" : dttET.Rows(0)!et_tipobf = dttPeac.Rows(0)!tb_tipobfrkm
              Case "T" : dttET.Rows(0)!et_tipobf = dttPeac.Rows(0)!tb_tipobfrkt
              Case "O", "$" : dttET.Rows(0)!et_tipobf = dttPeac.Rows(0)!tb_tipobfrko
              Case "H" : dttET.Rows(0)!et_tipobf = dttPeac.Rows(0)!tb_tipobfrkh
            End Select
            If NTSCInt(dttET.Rows(0)!et_tipobf) = 0 Then dttET.Rows(0)!et_tipobf = nPeacTipobf
          End If
        End If
        dttET.Rows(0)!et_acuradi = " "
      End If    'If bDocEmesso Then

      If bRiproponiDataDoc Then
        dttET.Rows(0)!et_datdoc = dtDataNewDoc
        dttET.Rows(0)!et_datapag = dtDataNewDoc
      End If

      If Not dttET.Columns.Contains("xx_totquant") Then dttET.Columns.Add("xx_totquant")

      If nInibisciTipiArticoliDiversi = 1 Then
        If Not dttEC.Columns.Contains("xxo_art62") Then dttEC.Columns.Add("xxo_art62")
        If Not dttECIMP.Columns.Contains("xxo_art62") Then dttECIMP.Columns.Add("xxo_art62")
      End If

      dsShared.Tables("TESTA").AcceptChanges()


      '----------------------------------------
      'ricollego i puntatori
      dttET = dsShared.Tables("TESTA")
      dttEC = dsShared.Tables("CORPO")
      dttECTC = dsShared.Tables("CORPOTC")
      dttECIMP = dsShared.Tables("CORPOIMP")
      dttECIMPTC = dsShared.Tables("CORPOIMPTC")
      dttASSRIS = dsShared.Tables("ASSRIS")
      dttATTIVIT = dsShared.Tables("ATTIVIT")
      dttLAVCENT = dsShared.Tables("LAVCENT")
      dttPROESEC = dsShared.Tables("PROESEC")
      dttSCHETRASP = dsShared.Tables("SCHETRASP")

      If BORDINI = False Then
        dttETC = dsShared.Tables("TESTAC")
        dttMOVMATR = dsShared.Tables("MOVMATR")
        dttMOVCLA = dsShared.Tables("MOVCLA")
        dttMOVCONA = dsShared.Tables("MOVCONA")
      End If

      If Not dttEC.Columns.Contains("xx_sel") Then dttEC.Columns.Add("xx_sel") : dttEC.Columns("xx_sel").DefaultValue = "N"

      AggiungiColonneUnbound(dsShared)


      bHasChangesET = True

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

  Public Overridable Function ApriDocumento(ByRef ds As DataSet) As Boolean
    Dim dtrTmp() As DataRow = Nothing
    Dim strTmp As String = ""
    Try
      ResetVar()

      If ds.Tables("TESTA").Rows.Count > 0 Then
        lLead = oCldDocu.CercaLeadDaConto(strDittaCorrente, NTSCStr(ds.Tables("TESTA").Rows(0)!xx_tipo), _
                                          NTSCInt(ds.Tables("TESTA").Rows(0)!et_conto), 0, lModuliDittaDitt, _
                                          lModuliExtDittaDitt, lModuliSupDittaDitt)
      End If

      '--------------------------------------
      'se non c'è il modulo PM ed ho compilato il task-id cerco di decodificarlo da COMWBS
      For Each dtrT As DataRow In ds.Tables("CORPO").Select("ec_pmtaskid <> 0 and ec_commeca <> 0")
        oCldDocu.ValCodiceDb(dtrT!ec_pmtaskid.ToString, strDittaCorrente, "COMWBS", "N", strTmp, Nothing, dtrT!ec_commeca.ToString, dtrT!ec_subcommeca.ToString)
        dtrT!xxo_pmtaskid = strTmp
        dtrT.AcceptChanges()
      Next

      '--------------------------------------
      If Not dsShared Is Nothing Then
        RemoveHandler dsShared.Tables("TESTA").ColumnChanging, AddressOf BeforeColUpdate_TESTA
        RemoveHandler dsShared.Tables("TESTA").ColumnChanged, AddressOf AfterColUpdate_TESTA
        If dsShared.Tables.Contains("TESTAC") Then
          RemoveHandler dsShared.Tables("TESTAC").ColumnChanging, AddressOf BeforeColUpdate_TESTAC
          RemoveHandler dsShared.Tables("TESTAC").ColumnChanged, AddressOf AfterColUpdate_TESTAC
        End If
        RemoveHandler dsShared.Tables("CORPO").ColumnChanging, AddressOf BeforeColUpdate_CORPO
        RemoveHandler dsShared.Tables("CORPO").ColumnChanged, AddressOf AfterColUpdate_CORPO
        RemoveHandler dsShared.Tables("CORPOIMP").ColumnChanging, AddressOf BeforeColUpdate_CORPOIMP
        RemoveHandler dsShared.Tables("CORPOIMP").ColumnChanged, AddressOf AfterColUpdate_CORPOIMP
        If dsShared.Tables.Contains("ASSRIS") Then
          RemoveHandler dsShared.Tables("ASSRIS").ColumnChanging, AddressOf BeforeColUpdate_ASSRIS
          RemoveHandler dsShared.Tables("ASSRIS").ColumnChanged, AddressOf AfterColUpdate_ASSRIS
        End If
        If dsShared.Tables.Contains("ATTIVIT") Then
          RemoveHandler dsShared.Tables("ATTIVIT").ColumnChanging, AddressOf BeforeColUpdate_ATTIVIT
          RemoveHandler dsShared.Tables("ATTIVIT").ColumnChanged, AddressOf AfterColUpdate_ATTIVIT
        End If
        If dsShared.Tables.Contains("LAVCENT") Then
          RemoveHandler dsShared.Tables("LAVCENT").ColumnChanging, AddressOf BeforeColUpdate_LAVCENT
          RemoveHandler dsShared.Tables("LAVCENT").ColumnChanged, AddressOf AfterColUpdate_LAVCENT
        End If
        If bModTCO Then
          RemoveHandler dsShared.Tables("CORPOTC").ColumnChanging, AddressOf BeforeColUpdate_CORPOTC
          RemoveHandler dsShared.Tables("CORPOTC").ColumnChanged, AddressOf AfterColUpdate_CORPOTC
          RemoveHandler dsShared.Tables("CORPOIMPTC").ColumnChanging, AddressOf BeforeColUpdate_CORPOIMPTC
          RemoveHandler dsShared.Tables("CORPOIMPTC").ColumnChanged, AddressOf AfterColUpdate_CORPOIMPTC
        End If
        If bModPM Then
          RemoveHandler dsShared.Tables("PROESEC").ColumnChanging, AddressOf BeforeColUpdate_PROESEC
          RemoveHandler dsShared.Tables("PROESEC").ColumnChanged, AddressOf AfterColUpdate_PROESEC
        End If
      End If

      '--------------------------------------
      'memorizzo i puntatori locali
      dsShared = ds
      dttET = dsShared.Tables("TESTA")
      dttEC = dsShared.Tables("CORPO")
      dttECTC = dsShared.Tables("CORPOTC")
      dttECIMP = dsShared.Tables("CORPOIMP")
      dttECIMPTC = dsShared.Tables("CORPOIMPTC")
      dttATTIVIT = dsShared.Tables("ATTIVIT")
      dttLAVCENT = dsShared.Tables("LAVCENT")
      dttASSRIS = dsShared.Tables("ASSRIS")
      dttPROESEC = dsShared.Tables("PROESE")
      dttSCHETRASP = dsShared.Tables("SCHETRASP")
      If BORDINI = False Then
        dttETC = dsShared.Tables("TESTAC")
        dttMOVMATR = dsShared.Tables("MOVMATR")
        dttMOVCLA = dsShared.Tables("MOVCLA")
        dttMOVCONA = dsShared.Tables("MOVCONA")
      End If

      '---------------------------------------
      'associo i colori alle righe KIT FIGLI
      If BGESTORDLIST = False Then
        For Each dtrT As DataRow In ds.Tables("CORPO").Rows
          ColoraCelle(dtrT)
        Next
      End If


      If Not dttET.Columns.Contains("xx_totquant") Then dttET.Columns.Add("xx_totquant")
      If Not dttEC.Columns.Contains("xx_sel") Then
        dttEC.Columns.Add("xx_sel")
        dttEC.Columns("xx_sel").DefaultValue = "N"

        For z As Integer = 0 To dttEC.Rows.Count - 1
          dttEC.Rows(z)!xx_sel = "N"
        Next
      End If

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("TESTA").ColumnChanging, AddressOf BeforeColUpdate_TESTA
      AddHandler dsShared.Tables("TESTA").ColumnChanged, AddressOf AfterColUpdate_TESTA
      If dsShared.Tables.Contains("TESTAC") Then
        AddHandler dsShared.Tables("TESTAC").ColumnChanging, AddressOf BeforeColUpdate_TESTAC
        AddHandler dsShared.Tables("TESTAC").ColumnChanged, AddressOf AfterColUpdate_TESTAC
      End If
      AddHandler dsShared.Tables("CORPO").ColumnChanging, AddressOf BeforeColUpdate_CORPO
      AddHandler dsShared.Tables("CORPO").ColumnChanged, AddressOf AfterColUpdate_CORPO
      AddHandler dsShared.Tables("CORPOIMP").ColumnChanging, AddressOf BeforeColUpdate_CORPOIMP
      AddHandler dsShared.Tables("CORPOIMP").ColumnChanged, AddressOf AfterColUpdate_CORPOIMP
      If dsShared.Tables.Contains("ASSRIS") Then
        AddHandler dsShared.Tables("ASSRIS").ColumnChanging, AddressOf BeforeColUpdate_ASSRIS
        AddHandler dsShared.Tables("ASSRIS").ColumnChanged, AddressOf AfterColUpdate_ASSRIS
      End If
      If dsShared.Tables.Contains("ATTIVIT") Then
        AddHandler dsShared.Tables("ATTIVIT").ColumnChanging, AddressOf BeforeColUpdate_ATTIVIT
        AddHandler dsShared.Tables("ATTIVIT").ColumnChanged, AddressOf AfterColUpdate_ATTIVIT
      End If
      If dsShared.Tables.Contains("LAVCENT") Then
        AddHandler dsShared.Tables("LAVCENT").ColumnChanging, AddressOf BeforeColUpdate_LAVCENT
        AddHandler dsShared.Tables("LAVCENT").ColumnChanged, AddressOf AfterColUpdate_LAVCENT
      End If
      If bModTCO Then
        AddHandler dsShared.Tables("CORPOTC").ColumnChanging, AddressOf BeforeColUpdate_CORPOTC
        AddHandler dsShared.Tables("CORPOTC").ColumnChanged, AddressOf AfterColUpdate_CORPOTC
        AddHandler dsShared.Tables("CORPOIMPTC").ColumnChanging, AddressOf BeforeColUpdate_CORPOIMPTC
        AddHandler dsShared.Tables("CORPOIMPTC").ColumnChanged, AddressOf AfterColUpdate_CORPOIMPTC
      End If
      If bModPM Then
        AddHandler dsShared.Tables("PROESEC").ColumnChanging, AddressOf BeforeColUpdate_PROESEC
        AddHandler dsShared.Tables("PROESEC").ColumnChanged, AddressOf AfterColUpdate_PROESEC
      End If

      bHasChangesET = False

      dtrTmp = ds.Tables("CORPO").Select(Nothing, "ec_riga DESC")
      If dtrTmp.Length > 0 Then
        lCrighe = NTSCInt(dtrTmp(0)!ec_riga)
        bTerzista = CType(oCleComm, CLELBMENU).IsTerzista(strDittaCorrente, NTSCInt(ds.Tables("TESTA").Rows(0)!et_magimp.ToString))

        If dttET.Rows(0)!et_tipork.ToString = "H" Or dttET.Rows(0)!et_tipork.ToString = "T" Then
          dtrTmp = ds.Tables("CORPOIMP").Select(Nothing, "ec_riga DESC")
          If dtrTmp.Length > 0 Then lCrigheYT = NTSCInt(dtrTmp(0)!ec_riga)

          If dttET.Rows.Count > 0 Then
            'lo rifaccio per prendere il listino di testata se strTipoListinoMat = " "
            Select Case strTipoListinoMat
              Case " "
                'Listino del carico
                nListinoScarichi = NTSCInt(dttET.Rows(0)!et_listino)
              Case "1"
                'Listino costo standard
                nListinoScarichi = nPeacListinCStd
              Case "2"
                'Ultimo costo di acquisto
                nListinoScarichi = 0
              Case "3"
                'Costo emdio non implementato
                'MsgBox "Valorizzazione materiali scaricati a costo medio non impelentata. Verra utilizzato il listino indicato nella testata del carico di produzione.", vbExclamation, bsTtlMsg
                nListinoScarichi = -2 'Val(edListino.Text)
              Case "4"
                'Ultimo costo comprensivo di oneri accessori
                nListinoScarichi = -1
            End Select
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

#Region "Gestione ACCCONF"
  Public Overridable Function Accconf_CheckVis() As Boolean
    'in apertura documento, controllo se posso vedere i dati
    Try
      If Not oApp.oGvar.bGestAccconf Then Return True
      If bDocDaRetail Then Return True 'GPV non gestisce questi controlli.
      If bNew Then Return True

      'tipobf
      If oApp.oGvar.nGestAccconfMagaz = 0 Then
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_tipobf) <> 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, "VIS", strDittaCorrente, _
                                                             NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                             NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                             NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_tipobf), 0, 0, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129543506273583985, "Utente non abilitato a visualizzare documenti con tipo B/F |'" & NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_tipobf).ToString & "'|")))
            Return False
          End If
        End If
      End If    'If oApp.oGvar.nGestAccconfMagaz = 0 Then

      'magazzino
      If oApp.oGvar.nGestAccconfMagaz = 2 Then
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz) <> 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, "VIS", strDittaCorrente, _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                       0, NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz), 0, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129543534522392578, "Utente non abilitato a visualizzare documenti con magazzino di testata |'" & NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz).ToString & "'|")))
            Return False
          End If
        End If

        'magazzino 2
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz2) <> 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, "VIS", strDittaCorrente, _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                       0, NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz2), 0, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129543534758789063, "Utente non abilitato a visualizzare documenti con magazzino 2 di testata |'" & NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz2).ToString & "'|")))
            Return False
          End If
        End If

        'magaz impegni
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magimp) <> 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, "VIS", strDittaCorrente, _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                       0, NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magimp), 0, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129543534916708985, "Utente non abilitato a visualizzare documenti con magazzino impegni di testata |'" & NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magimp).ToString & "'|")))
            Return False
          End If
        End If
      End If    'If oApp.oGvar.nGestAccconfMagaz = 2 Then

      If BORDINI = False And oApp.oGvar.nGestAccconfMagaz = 1 Then
        'causale
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causale) <> 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, "VIS", strDittaCorrente, _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                       0, 0, NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causale), 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129543535891650391, "Utente non abilitato a visualizzare documenti con causale di testata |'" & NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causale).ToString & "'|")))
            Return False
          End If
        End If

        'causale scarico
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causcap) <> 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, "VIS", strDittaCorrente, _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                       0, 0, NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causcap), 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129543536132763672, "Utente non abilitato a visualizzare documenti con causale di scarico di testata |'" & NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causcap).ToString & "'|")))
            Return False
          End If
        End If
      End If    'If BORDINI = False And oApp.oGvar.nGestAccconfMagaz = 1 Then

      If BORDINI = False Then
        nCodCaumOpen = NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causale)
        nCodCaumimpOpen = NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causcap)
      End If
      nCodtpbfOpen = NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_tipobf)
      nCodmagaOpen = NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz)
      nCodmaga2Open = NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz2)
      nCodmagimpOpen = NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magimp)

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
                                                     dttET.Rows(0)!et_tipork.ToString, _
                                                     dttET.Rows(0)!et_serie.ToString, _
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
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_tipobf) <> 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, strOperaz, strDittaCorrente, _
                                                             NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                             NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                             NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_tipobf), 0, 0, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129543555544023438, "tipo B/F |'" & NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_tipobf).ToString & "'|")))
            Return False
          End If
          If bNew = False And nCodtpbfOpen <> 0 And nCodtpbfOpen <> NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_tipobf) Then
            If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, strOperaz, strDittaCorrente, _
                                                               NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                               NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                               nCodtpbfOpen, 0, 0, 0) Then
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129543555522421875, "tipo B/F di origine |'" & nCodtpbfOpen.ToString & "'|")))
              Return False
            End If
          End If
        End If
      End If    'If oApp.oGvar.nGestAccconfMagaz = 0 Then

      'magazzino
      If oApp.oGvar.nGestAccconfMagaz = 2 Then
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz) <> 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, strOperaz, strDittaCorrente, _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                       0, NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz), 0, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129543555500986328, "magazzino di testata |'" & NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz).ToString & "'|")))
            Return False
          End If
          If bNew = False And nCodmagaOpen <> 0 And nCodmagaOpen <> NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz) Then
            If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, strOperaz, strDittaCorrente, _
                                                               NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                               NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                               0, nCodmagaOpen, 0, 0) Then
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129543555481943360, "magazzino di testata di origine |'" & nCodmagaOpen.ToString & "'|")))
              Return False
            End If
          End If
        End If

        'magazzino 2
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz2) <> 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, strOperaz, strDittaCorrente, _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                       0, NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz2), 0, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129543555462988282, "magazzino 2 di testata |'" & NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz2).ToString & "'|")))
            Return False
          End If
          If bNew = False And nCodmaga2Open <> 0 And nCodmaga2Open <> NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magaz2) Then
            If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, strOperaz, strDittaCorrente, _
                                                               NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                               NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                               0, nCodmaga2Open, 0, 0) Then
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129543555443222657, "magazzino 2 di testata di origine |'" & nCodmaga2Open.ToString & "'|")))
              Return False
            End If
          End If
        End If

        'magazzino impegni
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magimp) <> 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, strOperaz, strDittaCorrente, _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                       0, NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magimp), 0, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129543555420429688, "magazzino di scarico di testata |'" & NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magimp).ToString & "'|")))
            Return False
          End If
          If bNew = False And nCodmagimpOpen <> 0 And nCodmagimpOpen <> NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magimp) Then
            If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, strOperaz, strDittaCorrente, _
                                                               NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                               NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                               0, nCodmagimpOpen, 0, 0) Then
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129543555401464844, "magazzino di scarico di testata di origine |'" & nCodmagimpOpen.ToString & "'|")))
              Return False
            End If
          End If
        End If
      End If    'If oApp.oGvar.nGestAccconfMagaz = 2 Then

      If BORDINI = False And oApp.oGvar.nGestAccconfMagaz = 1 Then
        'causale
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causale) <> 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, strOperaz, strDittaCorrente, _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                       0, 0, NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causale), 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129543555373544922, "causale di testata |'" & NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causale).ToString & "'|")))
            Return False
          End If
          If bNew = False And nCodCaumOpen <> 0 And nCodCaumOpen <> NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causale) Then
            If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, strOperaz, strDittaCorrente, _
                                                               NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                               NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                               0, 0, nCodCaumOpen, 0) Then
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129543555357226563, "causale di testata di origine |'" & nCodCaumOpen.ToString & "'|")))
              Return False
            End If
          End If
        End If

        'causale scarico
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causcap) <> 0 Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, strOperaz, strDittaCorrente, _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                       NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                       0, 0, NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causcap), 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129543555321142578, "causale di scarico di testata |'" & NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causcap).ToString & "'|")))
            Return False
          End If
        End If
        If bNew = False And nCodCaumimpOpen <> 0 And nCodCaumimpOpen <> NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_causcap) Then
          If Not CType(oCleComm, CLELBMENU).AccconfCheck(dttAccconf, strOperaz, strDittaCorrente, _
                                                             NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_tipork), _
                                                             NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_serie), _
                                                             0, 0, nCodCaumimpOpen, 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129543555306103516, "causale di scarico di testata di origine |'" & nCodCaumimpOpen.ToString & "'|")))
            Return False
          End If
        End If
      End If    'If BORDINI = False And oApp.oGvar.nGestAccconfMagaz = 1 Then

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

  Public Overridable Sub FileApriCaricaPM()
    'casrica su MMTRANS i dati prelevati da proesec (rem/rim/ecc)
    Try
      bNonValidare = True
      Dim strDesta As String = ""
      Dim strDaraveout As String = ""
      Dim strTipoTask As String = ""
      Dim strGestcost As String = ""
      Dim dFQtares As Decimal = 0
      Dim dFWorkres As Decimal = 0
      Dim dFCostores As Decimal = 0
      Dim dBasecost As Decimal = 0
      Dim dBasecostun As Decimal = 0
      Dim dBasework As Decimal = 0
      Dim dBaseqta As Decimal = 0
      Dim strPrevGrup As String = ""
      Dim strRilasciato As String = ""

      ' ora PM , carica ttproesec
      For Each dtrT As DataRow In dttEC.Select("ec_commeca <> 0 AND ec_pmtaskid <> 0")
        oCldDocu.Caricattproesebappo(strDittaCorrente, lIIttproesebappo, NTSCInt(dtrT!ec_commeca), _
                                     NTSCInt(dtrT!ec_pmtaskid), Nothing)
      Next

      ' ora campi unnbound di ttprianax

      ' adesso aggironna i campi PM cosiddetti unbound (pmres, pmrim, ecc.)
      ' sui rk che hanno un taskid
      For Each dtrT As DataRow In dttEC.Rows
        dFQtarim = 0
        'dFDesqtarim = 0
        dFCostorim = 0
        dFQtares = 0
        dFCostores = 0
        dFQtadisda = 0
        dFValdisda = 0

        If NTSCInt(dtrT!ec_pmtaskid) <> 0 Then
          If BORDINI Then
            lEseddaes = oCldDocu.TrovaEsecIdDaMovord(strDittaCorrente, "TTPROESEC", lIIttproesebappo, _
                                  NTSCInt(dtrT!ec_commeca), NTSCStr(dttET.Rows(0)!et_tipork), _
                                  NTSCInt(dttET.Rows(0)!et_anno), NTSCStr(dttET.Rows(0)!et_serie), _
                                  NTSCInt(dttET.Rows(0)!et_numdoc), NTSCInt(dtrT!ec_riga))
          Else
            lEseddaes = oCldDocu.TrovaEsecIdDaMovmag(strDittaCorrente, "TTPROESEC", lIIttproesebappo, _
                                  NTSCInt(dtrT!ec_commeca), NTSCStr(dttET.Rows(0)!et_tipork), _
                                  NTSCInt(dttET.Rows(0)!et_anno), NTSCStr(dttET.Rows(0)!et_serie), _
                                  NTSCInt(dttET.Rows(0)!et_numdoc), NTSCInt(dtrT!ec_riga))
          End If

          If Not (oCldDocu.LegTasksCompleteDao(strDittaCorrente, "TTPROESEC", lIIttproesebappo, lIItttasks, _
                                NTSCInt(dtrT!ec_commeca), NTSCInt(dtrT!ec_pmtaskid), lEseddaes, _
                                IntSetDate("31/12/2099"), False, strDesta, strDaraveout, strTipoTask, _
                                strGestcost, dFQtares, dFWorkres, dFCostores, dBasecost, dBasecostun, _
                                dBasework, dBaseqta, strPrevGrup, BORDINI, strRilasciato)) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129442445411083984, "Sul Task è stato rilevato un problema in LegTaskCompleteDao in caricamento dati task-PM ! ")))
          Else
            ' ok, adesso ricostruisce gli altri campi:
            ' ricostruisce all'indietro QTADISDA/VALDISDA
            '                           QTARIM/VALRIM
            Select Case NTSCStr(dttET.Rows(0)!et_tipork)
              Case "A", "C"
                dFQtadisda = NTSCDec(dtrT!ec_pmQTADIS) * -1
                dFValdisda = NTSCDec(dtrT!ec_pmVALDIS) * -1
              Case "B"
                If strTipoTask = "6" Or strTipoTask = "7" Then
                  'Spediz. a cliente/cantiere
                  'Spediz. a fornitore
                  dFQtadisda = NTSCDec(dtrT!ec_pmQTADIS)
                  dFValdisda = NTSCDec(dtrT!ec_pmVALDIS)
                Else
                  'Fatturazione
                  'Appr. esterno di tipo Rett. costo (Es. Reso a fornitore)
                  'Acq. servizio di tipo Rett. costo (Es. Reso a fornitore)
                  'Appr. interno di tipo Rett. costo (Es. Reso a fornitore)
                  'altro ????
                  dFQtadisda = NTSCDec(dtrT!ec_pmQTADIS) * -1
                  dFValdisda = NTSCDec(dtrT!ec_pmVALDIS) * -1
                End If
              Case "W"
                If strTipoTask = "8" Then
                  'Fatturazione
                  dFQtadisda = NTSCDec(dtrT!ec_pmQTADIS) * -1
                  dFValdisda = NTSCDec(dtrT!ec_pmVALDIS) * -1
                Else
                  'Spediz. a cliente/cantiere
                  'Spediz. a fornitore (o altro ?)
                  dFQtadisda = NTSCDec(dtrT!ec_pmQTADIS)
                  dFValdisda = NTSCDec(dtrT!ec_pmVALDIS)
                End If
              Case "R", "#"
                'impegno cliente, 
                dFQtadisda = NTSCDec(dtrT!ec_pmQTADIS) * -1
                dFValdisda = NTSCDec(dtrT!ec_pmVALDIS) * -1
              Case Else
                'Tutti gli altri
                dFQtadisda = NTSCDec(dtrT!ec_pmQTADIS)
                dFValdisda = NTSCDec(dtrT!ec_pmVALDIS)
            End Select
            dFQtarim = dFQtares - dFQtadisda
            dFCostorim = dFCostores - dFValdisda
          End If

          ' DARAVE già settato ... NO : lo setto ora
          If NTSCStr(dttET.Rows(0)!et_tipork) = "B" Or NTSCStr(dttET.Rows(0)!et_tipork) = "A" Then
            dtrT!xxo_darave = "A"
          Else
            dtrT!xxo_darave = "D"
          End If

          Select Case NTSCStr(dttET.Rows(0)!et_tipork)
            Case "R", "#"
              dtrT!xxo_darave = "A"
            Case "H", "O", "X"
              dtrT!xxo_darave = "D"
            Case "A", "C"
              dtrT!xxo_darave = "A"
            Case "B"
              If strTipoTask = "6" Or strTipoTask = "7" Then
                'Spediz. a cliente/cantiere
                'Spediz. a fornitore
                dtrT!xxo_darave = "D"
              Else
                'Fatturazione
                'Appr. esterno di tipo Rett. costo (Es. Reso a fornitore)
                'Acq. servizio di tipo Rett. costo (Es. Reso a fornitore)
                'Appr. interno di tipo Rett. costo (Es. Reso a fornitore)
                'altro ????
                dtrT!xxo_darave = "A"
              End If
            Case "W"
              If strTipoTask = "8" Then
                'Fatturazione
                dtrT!xxo_darave = "A"
              Else
                'Spediz. a cliente/cantiere
                'Spediz. a fornitore (o altro ?)
                dtrT!xxo_darave = "D"
              End If
            Case Else
              'Tutti gli altri
              dtrT!xxo_darave = "D"
          End Select

          dtrT!xxo_pmtaskid = strDesta ' descrizione task

          dtrT!xxo_pmqtares = dFQtares
          dtrT!xxo_pmqtarim = dFQtarim

          dtrT!xxo_pmvalres = dFCostores

          dtrT!xxo_pmvalrim = dFCostorim
          dtrT!xxo_pmqtadisda = dFQtadisda
          dtrT!xxo_pmvaldisda = dFValdisda
          dtrT!xxo_prevgrup = strPrevGrup
          dtrT!xxo_gestcost = strGestcost
          dtrT.AcceptChanges()
        End If

      Next    'For Each dtrT As DataRow In dttEC.Rows

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      bNonValidare = False
    End Try
  End Sub

  Public Overridable Function RipristinaDocumento() As Boolean
    Try
      'non decommentare la riga sotto: in fase di ripristino di un nuovo documento i controlli di testata 
      'rimarrebbero senza nessun record e darebbero una sfilza di errori!!
      'dttET.Rows(nRow).RejectChanges()
      bHasChangesET = False

      If bNew Then
        oCldDocu.DeleteDocAllole(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                 dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), BORDINI, Nothing)
      End If

      ResetVar()

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
      bInNuovoDocSilent = False
      bInApriDocSilent = False
    End Try
  End Function

  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChangesET
    End Get
  End Property

  '-------------------------------------

  Public Overridable Function TestPreSalvaSTD() As Boolean
    '----------------------------
    'test pre salvataggio documento standard
    'NB: molti di questi controlli sono già stati fatti in okTestata di bsorgsor e beveboll 
    '    e quella routine viene richiamata prima di questa funzione!!!!!
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1 As DataRow = Nothing
    Dim i As Integer = 0
    Dim dQta As Decimal = 0
    Dim dttTmp As New DataTable
    Dim dttGr As New DataTable
    Dim oDttgr As New CLEGROUPBY
    Dim strTmp As String = ""
    Try
      If bInUnload Then Return False

      If dttET.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222099843750, "Impossibile salvare: non è presente nessun documento da trattare")))
        Return False
      End If
      If NTSCInt(dttET.Rows(0)!et_conto) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222100156250, "Il codice cliente/fornitore è obbligatorio: inserirlo prima di salvare")))
        Return False
      End If
      If NTSCInt(dttET.Rows(0)!et_tipobf) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222100312500, "Il tipo bolla/fattura è obbligatorio: inserirlo prima di salvare")))
        Return False
      End If
      If NTSCInt(dttET.Rows(0)!et_magaz) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222100468750, "Il codice magazzino 1 è obbligatorio: inserirlo prima di salvare")))
        Return False
      End If
      If bNew And NTSCInt(dttET.Rows(0)!et_codese) <> 0 And NTSCStr(dttET.Rows(0)!et_scorpo) = "S" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523713058482000, "Un documento in esenzione non può essere di tipo IVA inclusa (scorporo)")))
        Return False
      End If
      If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 And NTSCDec(dttET.Rows(0)!et_cambio) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523168912287500, "Il cambio deve essere diverso da 0 quando è indicata la valuta")))
        Return False
      End If
      If dttEC.Select("ec_codart = ' '").Length > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128547908506380000, "Impossibile salvare: nel corpo del documento sono presenti righe senza codice articolo")))
        Return False
      End If
      If dttEC.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222100937500, "Impossibile salvare: non è presente nessuna riga nel corpo del documento")))
        Return False
      End If
      If bNew And (dttET.Rows(0)!et_tipork.ToString = "T" Or dttET.Rows(0)!et_tipork.ToString = "H") And NTSCStr(dttET.Rows(0)!et_scorpo) = "S" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523170610659500, "Impossibile emettere documenti di 'Produzione' di tipo iva compresa (scorporo)")))
        Return False
      End If
      If bNew And (dttET.Rows(0)!et_tipork.ToString = "T" Or dttET.Rows(0)!et_tipork.ToString = "H") And bTerzista = False And NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523173669033500, "Impossibile emettere documenti di 'Produzione' INTERNI in valuta")))
        Return False
      End If

      '----------------------------------------
      'controllo articoli gestiti a fasi
      If Not bDisabilitaCheckFasi Then
        dtrT = dttEC.Select("(xxo_gesfasi = 'S' AND ec_fase = 0) OR (xxo_gesfasi = 'N' AND ec_fase <> 0)")
        If dtrT.Length > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526549402576000, "L'articolo |" & UCase(NTSCStr(dtrT(0)!ec_codart)) & "| nella riga |" & dtrT(0)!ec_riga.ToString & "| o è gestito a fasi e non è indicata nessuna fase valida, oppure non è gestito a fasi ed è stato inserito un codice fase diverso da 0")))
          Return False
        End If

        If (dttET.Rows(0)!et_tipork.ToString = "H" Or dttET.Rows(0)!et_tipork.ToString = "T") Then
          dtrT = dttECIMP.Select("(xxo_gesfasi = 'S' AND ec_fase = 0) OR (xxo_gesfasi = 'N' AND ec_fase <> 0)")
          If dtrT.Length > 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526550055748000, "L'articolo |" & UCase(NTSCStr(dtrT(0)!ec_codart)) & "| nella riga |" & dtrT(0)!ec_riga.ToString & "| degli IMPEGNI/SCARICHI DI PRODUZIONE o è gestito a fasi e non è indicata nessuna fase valida, oppure non è gestito a fasi ed è stato inserito un codice fase diverso da 0")))
            Return False
          End If
        End If
      End If    'If Not bDisabilitaCheckFasi Then

      If bModTCO Then
        If Not bInNuovoDocSilent Then
          If NTSCInt(dttET.Rows(0)!et_annotco) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128528309999314000, "Indicare nel piede del documento, un anno valido (per 'Taglie e colori').")))
            Return False
          End If
          If NTSCInt(dttET.Rows(0)!et_codstag) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128528310080746000, "Indicare nel piede del documento, un codice stagione valido (per 'Taglie e colori').")))
            Return False
          End If
        End If
        'la quantità di riga deve sempre essere uguale alla somma delle quantità per taglia
        For Each dtrT1 In dttEC.Select("xxo_codtagl <> 0")
          dtrT = dttECTC.Select("ec_riga = " & dtrT1!ec_riga.ToString)
          If dtrT.Length = 0 And NTSCDec(dtrT1!ec_quant) <> 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129646991275742188, "Attenzione: sull'articolo |" & dtrT1!ec_codart.ToString & "|, riga |" & dtrT1!ec_riga.ToString & "|, non è stato compilato il 'dettaglio taglie'.")))
            If Not bIgnoraBloccoQtaTaglieInvf Then Return False
          End If
          dQta = 0
          For i = 1 To 24
            dQta += NTSCDec(dtrT(0)("ec_quant" & i.ToString("00")))
          Next
          If dQta <> NTSCDec(dtrT1!ec_quant) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129646992470517578, "Attenzione: sull'articolo |" & dtrT1!ec_codart.ToString & "|, riga |" & dtrT1!ec_riga.ToString & "|, la quantità non corrisponde a quella del 'dettaglio taglie'. Entrare nel dettaglio taglie e confermare le quantità inserite")))
            If Not bIgnoraBloccoQtaTaglieInvf Then Return False
          End If
        Next
      End If

      If Not bDisabilitaCheckAnnoData And bInCreaDocDaGnor = False Then
        If NTSCInt(dttET.Rows(0)!et_anno) <> NTSCDate(dttET.Rows(0)!et_datdoc).Year Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128782409552895000, "La data del documento non è compresa nell'anno del documento.")))
          Return False
        End If
      End If

      '----------------------------------------
      If Not Accconf_CheckSalvaCancella(IIf(bNew, "INS", "MOD").ToString) Then Return False

      '----------------------------------------
      'Controllo KIT
      If BGESTORDLIST = False Then
        If Not TestPreSalvaSTD_CheckKit() Then Return False
      End If

      '----------------------------------------
      'test lotto univoco
      If bLottoUnivoco And bDocEmesso = False Then
        If Not TestPreSalvaSTD_LottoUnivoco() Then Return False
      End If

      '----------------------------------------
      'ricontrollo che cod iva e controp siano stati inseriti
      'il test non è stato fatto se inserito articoli da multiselezione da zoom artico
      If dttEC.Select("ec_codiva = 0 AND ec_prezzo <> 0 and ec_quant <> 0").Length > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129733489402378481, "Nel documento sono presenti righe con valore diverso da 0 ma senza codice IVA")))
      End If
      If dttEC.Select("ec_controp = 0 AND ec_prezzo <> 0 and ec_quant <> 0").Length > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129733489834543243, "Nel documento sono presenti righe con valore diverso da 0 ma senza codice CONTROPARTITA")))
      End If

      '----------------------------------------
      'Per Net 2013: se ho utilizzato dei codici IVA con tipo 'Reverse charge' <> 'N' 
      'in tabtpbf deve essereci una causale contabile di tipo 'Reverse charge'
      'non eseguo il test sulle righe degli scarichi: tanto si parla sempre e solo di acquisti nella normativa
      If bDocEmesso = False Then
        oDttgr.NTSGroupBy(dttEC, dttGr, "ec_codiva", "ec_codiva <> 0", "ec_codiva")
        For Each dtrT2 As DataRow In dttGr.Rows
          strTmp += dtrT2!ec_codiva.ToString & ", "
        Next
        If strTmp.Length > 0 Then
          strTmp = strTmp.Substring(0, strTmp.Length - 2)
          If dsTabciva.Tables("TABCIVA").Select("tb_codciva IN (" & strTmp & ") AND tb_revcharge <> 'N'").Length > 0 Then
            oCldDocu.ValCodiceDb(NTSCInt(dttET.Rows(0)!et_tipobf).ToString, strDittaCorrente, "TABTPBF", "N", "", dttTmp)
            If NTSCStr(dttTmp.Rows(0)!tb_fattrevch) = "N" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129774910479635784, "ATTENZIONE: se nel corpo del documento sono presenti dei codici IVA di tipo 'Reverse charge' " & vbCrLf & _
              "nel Tipo bolla/fattura deve essere opportunamente configurato 'Reverse charge acquisti'")))
              Return False
            End If
          End If
        End If
      End If

      '----------------------------------------
      'controlli CA: se ho importato le righe da ordine non ho fatto i controlli al salva di riga
      'se la causale ha testci <> " ": li rifaccio
      If BORDINI = False Then
        If (CBool(lModuliSupDittaDitt And bsModSupCAE) = False) And (CBool(lModuliDittaDitt And bsModCI) = True) Then
          For Each dtrTca As DataRow In dttEC.Select("ec_codart <> 'D' and xxo_testci <> ' ' and ec_contocontr <> 0")
            If Not CorpoTestPreSalva_CheckCA(dtrTca) Then Return False
          Next
        End If

        If nGiorniMargineScadenzaLotto <> 9999 AndAlso bDocEmesso Then
          For Each dtrEc As DataRow In dttEC.Select("ec_lotto <> 0")
            oCldDocu.ValCodiceDb(NTSCStr(dtrEc!ec_lotto), strDittaCorrente, "ANALOTTI", "S", "", dttTmp, NTSCStr(dtrEc!ec_codart))

            If NTSCDate(dttET.Rows(0)!et_datdoc).AddDays(nGiorniMargineScadenzaLotto) >= NTSCDate(dttTmp.Rows(0)!alo_dtscad) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131092606811897160, "Sulla riga " & NTSCInt(dtrEc!ec_riga) & " non è possibile usare il lotto '|" & NTSCStr(dttTmp.Rows(0)!alo_lottox) & "|'." & vbCrLf & _
                                                "La data scadenza |" & NTSCDate(dttTmp.Rows(0)!alo_dtscad).ToShortDateString & "| risulta essere inferiore o uguale alla data documento più |" & nGiorniMargineScadenzaLotto & "| giorni (|" & NTSCDate(dttET.Rows(0)!et_datdoc).AddDays(nGiorniMargineScadenzaLotto) & "|)")))
              Return False
            End If
          Next
        End If
      End If    'If BORDINI = False Then

      '----------------------------------------
      If CheckExpireDate(NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString) Then Return False

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

  Public Overridable Function TestPreSalvaSTD_CheckKit() As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim strMsg As String = ""
    Try

      '----------------------------------
      'Controlla che ci siano i riferimenti alle righe kit padre quando servono
      dtrT = dttEC.Select("(((ec_flkit = ' ' OR ec_flkit = 'A' OR ec_flkit = 'S') AND ec_ktriga <> 0) OR ((ec_flkit = 'B' OR ec_flkit = 'T') AND ec_ktriga = 0))")
      If dtrT.Length > 0 Then
        Select Case dtrT(0)!ec_flkit.ToString
          Case "A", "S"
            strMsg = oApp.Tr(Me, 128527187758332000, "L'articolo kit |'" & NTSCStr(dtrT(0)!ec_codart) & "'| nella riga |" & dtrT(0)!ec_riga.ToString & "| deve possedere il 'Riferimento riga kit' uguale a zero.")
          Case "T", "B"
            strMsg = oApp.Tr(Me, 128527187823072000, "L'articolo componente kit |'" & NTSCStr(dtrT(0)!ec_codart) & "'| nella riga |" & dtrT(0)!ec_riga.ToString & "| deve possedere il 'Riferimento riga kit' maggiore di zero.")
          Case Else
            strMsg = oApp.Tr(Me, 128527187809500000, "L'articolo |'" & NTSCStr(dtrT(0)!ec_codart) & "'| nella riga |" & dtrT(0)!ec_riga.ToString & "| deve possedere il 'Riferimento riga kit' uguale a zero.")
        End Select
        ThrowRemoteEvent(New NTSEventArgs("", strMsg))
        Return False
      End If

      '----------------------------------
      'Controlla che le righe kit padre esistano
      dtrT = dttEC.Select("ec_ktriga <> 0")
      For Each dtrTmp As DataRow In dtrT
        dtrT1 = dttEC.Select("(ec_flkit = 'A' OR ec_flkit = 'S') AND ec_riga = " & NTSCInt(dtrTmp!ec_ktriga).ToString)
        If dtrT1.Length = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128527191434670000, "L'articolo componente kit |'" & NTSCStr(dtrTmp!ec_codart) & "'| nella riga |" & dtrTmp!ec_riga.ToString & "| fa riferimento alla riga padre |" & dtrTmp!ec_ktriga.ToString & "| inesistente o non di tipo kit.")))
          Return False
        End If
      Next

      '----------------------------------
      'Controlla la congruenza tra il tipo di righe kit padri e figlie (padre sintetico figli sintetico / padre analitico figli analitici)
      dtrT = dttEC.Select("ec_flkit = 'A' AND ec_ktriga = 0")    'prendo tutti i padri 'analitici'
      For Each dtrTmp As DataRow In dtrT
        dtrT1 = dttEC.Select("ec_flkit = 'T' AND ec_riga = " & NTSCInt(dtrTmp!ec_ktriga).ToString) 'cerco i figli 'sintetici'
        If dtrT1.Length > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128527200632744000, "L'articolo kit |'" & NTSCStr(dtrTmp!ec_codart) & "'| nella riga |" & NTSCInt(dtrTmp!ec_riga).ToString & "| è di tipo 'Kit analitico', mentre l'articolo componente |'" & NTSCStr(dtrT1(0)!ec_codart) & "'| indicato nella riga |" & NTSCInt(dtrT1(0)!ec_riga).ToString & "| è di tipo 'Compon. sintetico'.")))
          Return False
        End If
      Next

      dtrT = dttEC.Select("ec_flkit = 'S' AND ec_ktriga = 0")    'prendo tutti i padri 'sintetici'
      For Each dtrTmp As DataRow In dtrT
        dtrT1 = dttEC.Select("ec_flkit = 'B' AND ec_riga = " & NTSCInt(dtrTmp!ec_ktriga).ToString) 'cerco i figli 'analitici'
        If dtrT1.Length > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128527201939712000, "L'articolo kit |'" & NTSCStr(dtrTmp!ec_codart) & "'| nella riga |" & NTSCInt(dtrTmp!ec_riga).ToString & "| è di tipo 'Kit sintetico', mentre l'articolo componente |'" & NTSCStr(dtrT1(0)!ec_codart) & "'| indicato nella riga |" & NTSCInt(dtrT1(0)!ec_riga).ToString & "| è di tipo 'Compon. analitico'.")))
          Return False
        End If
      Next

      '----------------------------------
      'Controlla che le righe kit padre analitico o figlio sintetico non contengano prezi, sconti, provv.
      dtrT = dttEC.Select("(ec_flkit = 'A' OR ec_flkit = 'T') AND ec_prezzo <> 0")
      If dtrT.Length > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128527202871968000, "L'articolo |'" & NTSCStr(dtrT(0)!ec_codart) & "'| nella riga |" & NTSCInt(dtrT(0)!ec_riga).ToString & "| deve possedere prezzo uguale a zero essendo un 'Kit analitico' oppure un 'Componente kit sintetico'.")))
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
  Public Overridable Function TestPreSalvaSTD_LottoUnivoco() As Boolean
    'se abilitata l'opzione, controllo che per ogni articolo gestito a lotti 
    'in analotti non sia presente un analotti.alo_lottox con articolo diverso da quello in analisi
    Dim dttGr As New DataTable
    Dim oDttgr As New CLEGROUPBY
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Dim dtrT() As DataRow = Nothing
    Try
      oDttgr.NTSGroupBy(dttEC, dttGr, "ec_codart, xxo_lottox", "xxo_geslotti = 'S' and ec_lotto <> 0", "ec_codart, xxo_lottox")
      For i = 0 To dttGr.Rows.Count - 1
        If Not oCldDocu.LottoxCheckLottoUnivoco(strDittaCorrente, NTSCStr(dttGr.Rows(i)!ec_codart), _
                                                NTSCStr(dttGr.Rows(i)!xxo_lottox), strTmp) Then
          dtrT = dttEC.Select("ec_codart = " & CStrSQL(NTSCStr(dttGr.Rows(i)!ec_codart)) & " AND xxo_lottox = " & CStrSQL(NTSCStr(dttGr.Rows(i)!xxo_lottox)))
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129526281562842028, "Test lotto univoco: Il lotto |" & NTSCStr(dttGr.Rows(i)!xxo_lottox) & "| impostato sull'articolo |" & NTSCStr(dttGr.Rows(i)!ec_codart) & "| (riga |" & dtrT(0)!ec_riga.ToString & "|) è già stato utilizzato per l'articolo |" & strTmp & "|")))
          Return False
        End If
      Next

      If dttET.Rows(0)!et_tipork.ToString = "T" Or dttET.Rows(0)!et_tipork.ToString = "H" Then
        oDttgr.NTSGroupBy(dttECIMP, dttGr, "ec_codart, xxo_lottox", "xxo_geslotti = 'S' and ec_lotto <> 0", "ec_codart, xxo_lottox")
        For i = 0 To dttGr.Rows.Count - 1
          If Not oCldDocu.LottoxCheckLottoUnivoco(strDittaCorrente, NTSCStr(dttGr.Rows(i)!ec_codart), _
                                                  NTSCStr(dttGr.Rows(i)!xxo_lottox), strTmp) Then
            dtrT = dttECIMP.Select("ec_codart = " & CStrSQL(NTSCStr(dttGr.Rows(i)!ec_codart)) & " AND xxo_lottox = " & CStrSQL(NTSCStr(dttGr.Rows(i)!xxo_lottox)))
            If BORDINI Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129527898411757812, "Test lotto univoco: Il lotto |" & NTSCStr(dttGr.Rows(i)!xxo_lottox) & "| impostato negli scarichi collegati sull'articolo |" & NTSCStr(dttGr.Rows(i)!ec_codart) & "| (riga |" & dtrT(0)!ec_riga.ToString & "| collegata a riga di carico |" & dtrT(0)!ec_rigaor.ToString & "|) è già stato utilizzato per l'articolo |" & strTmp & "|")))
            Else
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129526283173255428, "Test lotto univoco: Il lotto |" & NTSCStr(dttGr.Rows(i)!xxo_lottox) & "| impostato negli scarichi collegati sull'articolo |" & NTSCStr(dttGr.Rows(i)!ec_codart) & "| (riga |" & dtrT(0)!ec_riga.ToString & "| collegata a riga di carico |" & dtrT(0)!ec_prriga.ToString & "|) è già stato utilizzato per l'articolo |" & strTmp & "|")))
            End If
            Return False
          End If
        Next
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
      dttGr.Clear()
    End Try
  End Function

  Public Overridable Function TestPreSalvaSTD_CheckEsistFinale() As Boolean
    Dim dttGr As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim dEsist As Decimal = 0
    Dim dPrenot As Decimal = 0
    Dim dImpeg As Decimal = 0
    Dim dOrdin As Decimal = 0
    Dim dQuant As Decimal = 0
    Dim strMsg As String = ""
    Dim evnt As NTSEventArgs = Nothing
    Dim nMagaz As Integer = 0
    Dim bMagaz2 As Boolean
    Dim strFiltroRigheNP As String = ""
    Dim strTestoMsgTC As String = ""
    Try
      '-------------------------
      'TEST ESISTENZA FINALE
      'Non lo esegue sui doc aperti
      If bNew = False Or bDocDaRetail Then Return True

      'Controllo test abilitato
      If strTestEsist.ToUpper <> "A" And strTestEsist.ToUpper <> "B" And strTestEsist.ToUpper <> "P" Then Return True
      If strTestEsistQuando.ToUpper <> "F" And strTestEsistQuando.ToUpper <> "S" Then Return True

      If strTestEsist <> " " And strTestEsist <> "N" And strTestEsistCome = "D" And BORDINI = False Then
        'in evasione di una riga collegata ad una nota di prelievo, se il test esistenza deve essere fatto su esistenza netta (esistenza - prenotato)
        'non faccio il controllo
        strFiltroRigheNP = " AND ec_npnum = 0 "
      End If

      '-------------------------
      'Loppa sulle righe
      'raggruppo per articolo
      If strTestEsistArtTc = "T" Then 'Raggruppamento per articoli TC, raggruppati per taglia
        CheckEsistenza_RaggruppaArticoliTC(strFiltroRigheNP, dttGr)
      Else 'raggruppamento classico per articoli NON TC o a taglia e colore raggruppati per colore.
        CheckEsistenza_RaggruppaArticoli(strFiltroRigheNP, dttGr)
      End If

      '-------------------------
      'per ogni articolo eseguo la query su artpro (solo articoli con ar_stainv = 'S')
      For Each dtrTmp As DataRow In dttGr.Rows
        bMagaz2 = False
        '-------------------------
        'solo per veboll Controlla se è una causale di scarico
        nMagaz = NTSCInt(dtrTmp!ec_magaz)

        If dtrTmp.Table.Columns.Contains("xxo_esist2") AndAlso NTSCInt(dtrTmp!xxo_esist2) < 0 Then
          nMagaz = NTSCInt(dtrTmp!ec_magaz2)
          bMagaz2 = True
        End If

        '-------------------------
        '-------------------------
        If strTestEsistArtTc = "T" AndAlso NTSCInt(dtrTmp!xxo_codtagl) <> 0 Then
          'Test esistenza per taglia sugli articoli TC con opzione settata
          If Not oCldDocu.ChekEsistenzaFinaleTC(strDittaCorrente, dtrTmp!ec_codart.ToString, NTSCInt(dtrTmp!ec_fase), nMagaz, dttTmp) Then Return False
        Else
          'Test esistenza per colore per gli articoli TC senza opzione settata (e per tutti gli altri articoli)
          If Not oCldDocu.ChekEsistenzaFinale(strDittaCorrente, dtrTmp!ec_codart.ToString, NTSCInt(dtrTmp!ec_fase), nMagaz, dttTmp) Then Return False
        End If
        If dttTmp.Rows.Count > 0 Then
          If dttTmp.Rows(0)!ar_stainv.ToString = "S" Then
            If strTestEsistArtTc = "T" AndAlso NTSCInt(dtrTmp!xxo_codtagl) <> 0 Then
              For i = 1 To 24
                dEsist = NTSCDec(dttTmp.Rows(0)("apt_esist" & i.ToString("00")))

                Select Case strTestEsistCome
                  Case "D"
                    dEsist = dEsist - NTSCDec(dttTmp.Rows(0)("apt_prenot" & i.ToString("00")))
                  Case "I"
                    dEsist = dEsist - NTSCDec(dttTmp.Rows(0)("apt_impeg" & i.ToString("00"))) + _
                                      NTSCDec(dttTmp.Rows(0)("apt_ordin" & i.ToString("00")))
                End Select

                dtrTmp("ec_quant" & i.ToString("00")) = NTSCDec(dtrTmp("ec_quant" & i.ToString("00"))) * NTSCInt(dtrTmp("xxo_esist" & NTSCStr(IIf(bMagaz2, "2", ""))))

                If dEsist + NTSCDec(dtrTmp("ec_quant" & i.ToString("00"))) < 0 Then strTestoMsgTC &= oApp.Tr(Me, 129990061244403890, "'|" & GetDescrTaglia(NTSCInt(dtrTmp!xxo_codtagl), i) & "|' esist. |" & dEsist & "|")
              Next
            Else
              'Per ogni riga (che rappresenta articolo/magazzino) determina il consumo
              dEsist = NTSCDec(dttTmp.Rows(0)!ap_esist)
              dPrenot = NTSCDec(dttTmp.Rows(0)!ap_prenot)
              dImpeg = NTSCDec(dttTmp.Rows(0)!ap_impeg)
              dOrdin = NTSCDec(dttTmp.Rows(0)!ap_ordin)

              'Se serve rileva la disponibilità per articolo/commessa
              If dttTmp.Rows(0)!ar_gescomm.ToString = "S" And NTSCInt(dtrTmp!ec_commeca) > 0 And bTestEsistPerComm Then
                dEsist = CType(oCleComm, CLELBMENU).TrovaEsistDaXpro(strDittaCorrente, dtrTmp!ec_codart.ToString, _
                                      nMagaz, NTSCInt(dtrTmp!ec_commeca), 0, NTSCInt(dtrTmp!ec_fase), _
                                      "", dOrdin, dImpeg, dPrenot)
              End If

              'Test su Esistenza oppura Disponibilità netta?
              Select Case strTestEsistCome
                Case "D" : dEsist = dEsist - dPrenot
                Case "I" : dEsist = dEsist - dImpeg + dOrdin
              End Select
            End If

            dQuant = 0

            '----------------------
            'nel datatable potrei avere lo stesso articolo/fase/magazzino/commessa su più righe: il testa va fatto raggruppato!!!
            dtrT = dttGr.Select("ec_codart = " & CStrSQL(dtrTmp!ec_codart) & _
                                " AND ec_fase = " & dtrTmp!ec_fase.ToString & _
                                " AND ec_magaz" & NTSCStr(IIf(bMagaz2, "2", "")) & " = " & nMagaz.ToString & _
                                IIf(dttTmp.Rows(0)!ar_gescomm.ToString = "S" And NTSCInt(dtrTmp!ec_commeca) > 0 And bTestEsistPerComm, " AND ec_commeca = " & dtrTmp!ec_commeca.ToString, "").ToString)
            If BORDINI Then
              For i = 0 To dtrT.Length - 1
                dQuant += -NTSCDec(dtrT(i)!Quant)
              Next
            Else
              For i = 0 To dtrT.Length - 1
                dQuant += NTSCDec(dtrT(i)!Quant) * NTSCInt(dtrT(i)("xxo_esist" & NTSCStr(IIf(bMagaz2, "2", ""))))
              Next
            End If

            '----------------------
            'ho l'esistenza e la quantità totale da consumare nel documento:
            If strTestoMsgTC.Trim <> "" OrElse (ArrDbl(dEsist, 3) + ArrDbl(dQuant, 3)) < 0 Then
              'cerco la riga nel documento, prima nella griglia principale, se non trovo cerco negli scarichi collegati
              dtrT = dttEC.Select("ec_codart = " & CStrSQL(dtrTmp!ec_codart) & _
                                  " AND ec_fase = " & dtrTmp!ec_fase.ToString & _
                                  " AND (ec_magaz = " & nMagaz.ToString & _
                                  " OR ec_magaz2 = " & nMagaz.ToString & ") " & _
                                IIf(dttTmp.Rows(0)!ar_gescomm.ToString = "S" And NTSCInt(dtrTmp!ec_commeca) > 0 And bTestEsistPerComm, " AND ec_commeca = " & dtrTmp!ec_commeca.ToString, "").ToString, "ec_riga")
              If dtrT.Length = 0 Then
                dtrT = dttECIMP.Select("ec_codart = " & CStrSQL(dtrTmp!ec_codart) & _
                                    " AND ec_fase = " & dtrTmp!ec_fase.ToString & _
                                    " AND ec_magaz = " & dtrTmp!ec_magaz.ToString & _
                                    IIf(dttTmp.Rows(0)!ar_gescomm.ToString = "S" And NTSCInt(dtrTmp!ec_commeca) > 0 And bTestEsistPerComm, " AND ec_commeca = " & dtrTmp!ec_commeca.ToString, "").ToString, "ec_riga")
              End If

              'do il messaggio
              Dim str1 As String = oApp.Tr(Me, 128669099165000000, " (Commessa |" & dtrTmp!ec_commeca.ToString & "|)")
              Dim str2 As String = oApp.Tr(Me, 128669100162031250, " del documento")
              Dim str3 As String = ""
              If Not BORDINI Then str3 = oApp.Tr(Me, 128669101694687500, " dello Scarico collegata a riga |" & dtrT(0)!ec_prriga.ToString & "|")
              strMsg = oApp.Tr(Me, 128669096610781250, "Attenzione!" & vbCrLf & _
                "Disponibilità articolo '|" & dtrT(0)!ec_codart.ToString & "|', su riga |" & dtrT(0)!ec_riga.ToString & "|") & _
                IIf(dttTmp.Rows(0)!ar_gescomm.ToString = "S" And NTSCInt(dtrTmp!ec_commeca) > 0 And bTestEsistPerComm, _
                str1, "").ToString

              If BORDINI Then
                strMsg &= str2 & ", " & oApp.Tr(Me, 129990174439070306, "sta diventando negativa")
              Else
                strMsg &= IIf(NTSCInt(dtrT(0)!ec_prriga) = 0, str2, str3).ToString & ", " & oApp.Tr(Me, 129990174543556733, "sta diventando negativa")
              End If

              If strTestoMsgTC.Trim = "" Then
                strMsg &= oApp.Tr(Me, 129990066834399838, vbCrLf & "(qtà occorrente: |" & (ArrDbl(dQuant, 3) * -1).ToString(oApp.FormatQta) & _
                          "|, qtà esistente: |" & ArrDbl(dEsist, 3).ToString(oApp.FormatQta) & "|).")
              Else
                strMsg &= vbCrLf & strTestoMsgTC
              End If

              Select Case strTestEsist
                Case "A" 'Avviso
                  evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNOCANC, strMsg & vbCrLf & vbCrLf & oApp.Tr(Me, 128669105652031250, "Continuare il controllo?"))
                  ThrowRemoteEvent(evnt)
                  Select Case evnt.RetValue
                    Case CLN__STD.ThMsg.RETVALUE_NO
                      Return True
                    Case CLN__STD.ThMsg.RETVALUE_CANCEL
                      Return False
                  End Select
                Case "P" 'Sblocco con password
                  Dim strPwd As String = oCldDocu.GetSettingBusDitt(strDittaCorrente, "Bsveboll", "Opzioni", ".", "EsistPWD", "NTS", " ", "NTS")
                  If bLeggiOpzioniDoc Then strPwd = oCldDocu.GetSettingBusDitt(strDittaCorrente, "Bsveboll", "OpzioniDoc", ".", "EsistPWD", "NTS", NTSCStr(dttET.Rows(0)!et_tipork), strPwd)
Riprova:
                  evnt = New NTSEventArgs(CLN__STD.ThMsg.INPUTPWD, strMsg & oApp.Tr(Me, 128669104125000000, vbCrLf & "Inserire Password per continuare:"))
                  ThrowRemoteEvent(evnt)
                  If evnt.RetValue <> "" Then
                    'Controlla PWD
                    If UCase(evnt.RetValue) <> UCase(strPwd) Then
                      GoTo Riprova
                    Else
                      'Esce da tutti i controlla dopo l'OK della pwd
                      '.Close
                      'Set snaTest = Nothing
                      'CheckEsistFinale = True
                      'GoTo RoutineEnd
                    End If
                  Else
                    Return False
                  End If
                Case "B" 'Blocco
                  ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 128669102834531250, vbCrLf & " Impossibile continuare.")))
                  Return False
              End Select    'Select Case strTestEsist
              strTestoMsgTC = ""
            End If    'If (ArrDbl(dEsist, 3) + ArrDbl(dQuant, 3)) < 0 Then

          End If    'If dttTmp.Rows(0)!ar_stainv.ToString = "S" Then
        End If    'If dttTmp.Rows.Count > 0 Then
      Next    'For Each dtrTmp As DataRow In dttGr.Rows

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
      dttGr.Clear()
    End Try
  End Function


  Public Overridable Function CheckEsistenza_RaggruppaArticoliTC(ByVal strFiltroRigheNP As String, ByRef dttGr As DataTable) As Boolean
    Dim dtrTagl() As DataRow = Nothing
    Dim dtrRow() As DataRow = Nothing
    Dim dttCopy As New DataTable
    Dim i, k, y As Integer
    Try
      'Non è possibile usare la NTSGroupBy perchè si sta lavorando su 2 datatable diverse.
      'Simulo le operazioni che fa normalmente, lavorando sulle 2 tabelle.

      'Filtra le sole righe che dovranno essere testate
      dttCopy = dttEC.Copy 'Copia le righe perchè così si potranno rimuovere dal datatable copia per evitare problemi
      dtrRow = dttCopy.Select("ec_codart <> 'M' AND ec_codart <> 'D' AND ec_flkit <> 'S' AND ec_flkit <> 'A'" & NTSCStr(IIf(Not BORDINI, " AND xxo_esist < 0", "")) & strFiltroRigheNP)

      dttGr = New DataTable
      dttGr.Columns.Add("ec_codart", GetType(String))
      dttGr.Columns.Add("ec_magaz", GetType(Integer))
      dttGr.Columns.Add("ec_magaz2", GetType(Integer))
      dttGr.Columns.Add("ec_fase", GetType(Integer))
      dttGr.Columns.Add("ec_commeca", GetType(Integer))
      dttGr.Columns.Add("xxo_codtagl", GetType(Integer))
      dttGr.Columns.Add("Quant", GetType(Decimal))
      dttGr.Columns.Add("xxo_esist", GetType(Decimal))
      dttGr.Columns.Add("xxo_esist2", GetType(Decimal))
      For i = 1 To 24
        dttGr.Columns.Add("ec_quant" & i.ToString("00"), GetType(Decimal))
      Next

      'Inizia a raggruppare
      For i = 0 To dtrRow.Length - 1
        'Le righe già elaborate vanno escluse
        If dtrRow(i).RowState = DataRowState.Deleted Then Continue For
        'Aggiungo la riga
        dttGr.Rows.Add()
        With dttGr.Rows(dttGr.Rows.Count - 1)
          !ec_codart = dtrRow(i)!ec_codart
          !ec_magaz = dtrRow(i)!ec_magaz
          !ec_magaz2 = dtrRow(i)!ec_magaz2
          !ec_fase = dtrRow(i)!ec_fase
          !ec_commeca = dtrRow(i)!ec_commeca
          !Quant = dtrRow(i)!ec_quant
          !xxo_codtagl = dtrRow(i)!xxo_codtagl
          If BORDINI Then
            !xxo_esist = -1
            !xxo_esist2 = 0
          Else
            !xxo_esist = dtrRow(i)!xxo_esist
            !xxo_esist2 = dtrRow(i)!xxo_esist2
          End If

          'Controlla se per la riga selezionata c'è un dettaglio TCO, in caso lo riversa nel datatable raggruppato
          dtrTagl = dttECTC.Select("ec_riga = " & NTSCInt(dtrRow(i)!ec_riga))
          If dtrTagl.Length > 0 Then
            For k = 1 To 24
              dttGr.Rows(dttGr.Rows.Count - 1)("ec_quant" & k.ToString("00")) = dtrTagl(0)("ec_quant" & k.ToString("00"))
            Next
          End If

          For k = i + 1 To dtrRow.Length - 1
            'Controllo se esistono altre righe con le stesse caratteristiche
            If NTSCStr(dtrRow(i)!ec_codart) = NTSCStr(dtrRow(k)!ec_codart) And NTSCInt(dtrRow(i)!ec_magaz) = NTSCInt(dtrRow(k)!ec_magaz) And _
               NTSCInt(dtrRow(i)!ec_magaz2) = NTSCInt(dtrRow(k)!ec_magaz2) And NTSCInt(dtrRow(i)!ec_commeca) = NTSCInt(dtrRow(k)!ec_commeca) And _
               NTSCInt(dtrRow(i)!ec_fase) = NTSCInt(dtrRow(k)!ec_fase) And (Not BORDINI AndAlso NTSCDec(dtrRow(i)!xxo_esist) = NTSCDec(dtrRow(k)!xxo_esist)) And _
               (Not BORDINI AndAlso NTSCDec(dtrRow(i)!xxo_esist2) = NTSCDec(dtrRow(k)!xxo_esist2)) Then

              !ec_quant = dtrRow(k)!ec_quant

              'Controlla se per la riga selezionata c'è un dettaglio TCO, in caso lo riversa nel datatable raggruppato
              dtrTagl = dttECTC.Select("ec_riga = " & NTSCInt(dtrRow(k)!ec_riga))
              If dtrTagl.Length > 0 Then
                For y = 1 To 24
                  dttGr.Rows(dttGr.Rows.Count - 1)("ec_quant" & y.ToString("00")) = dtrTagl(0)("ec_quant" & y.ToString("00"))
                Next
              End If

              dtrRow(k).Delete()
            End If
          Next
        End With
      Next

      dttGr.AcceptChanges()

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
  Public Overridable Function CheckEsistenza_RaggruppaArticoli(ByVal strFiltroRigheNP As String, ByRef dttGr As DataTable) As Boolean
    Dim oDttgr As New CLEGROUPBY
    Dim dttTmp As New DataTable
    Dim strQuery As String = ""
    Dim i As Integer = 0
    Try
      'prendo lgi articoli dal magaz 1
      If Not BORDINI Then strQuery = ", xxo_esist, xxo_esist2"

      If Not oDttgr.NTSGroupBy(dttEC, dttGr, "min(ec_tipork) as ec_tipork, ec_codart, ec_magaz, ec_magaz2, ec_commeca, ec_fase, " & _
                                             " Sum(ec_quant) as Quant" & strQuery, _
                                             "ec_codart <> 'M' AND ec_codart <> 'D' AND ec_flkit <> 'S' AND ec_flkit <> 'A' " & NTSCStr(IIf(BORDINI, "", "AND xxo_esist < 0")) & strFiltroRigheNP, _
                                             "ec_codart, ec_magaz, ec_magaz2, ec_commeca, ec_fase" & strQuery) Then Return False

      'prendo gli articoli dal magaz 2
      If Not oDttgr.NTSGroupBy(dttEC, dttTmp, "min(ec_tipork) as ec_tipork, ec_codart, ec_magaz2, ec_commeca, " & _
                                             " ec_fase, Sum(ec_quant) as Quant" & strQuery, _
                                             "ec_codart <> 'M' AND ec_codart <> 'D' AND ec_flkit <> 'S' AND ec_flkit <> 'A' and ec_magaz2 > 0 " & NTSCStr(IIf(BORDINI, "", "AND xxo_esist2 < 0")) & strFiltroRigheNP, _
                                             "ec_codart, ec_magaz2, ec_commeca, ec_fase" & strQuery) Then Return False
      For i = 0 To dttTmp.Rows.Count - 1
        dttGr.ImportRow(dttTmp.Rows(i))
      Next

      'prendo gli articoli dal magaz impegni
      If dttET.Rows(0)!et_tipork.ToString = "T" Then
        dttTmp.Clear()
        If Not oDttgr.NTSGroupBy(dttECIMP, dttTmp, "min(ec_tipork) as ec_tipork, ec_codart, ec_magaz, ec_commeca, " & _
                                               " ec_fase, Sum(ec_quant) as Quant" & strQuery, _
                                               "ec_codart <> 'M' AND ec_codart <> 'D' AND ec_flkit <> 'S' AND ec_flkit <> 'A' " & NTSCStr(IIf(BORDINI, "", "AND xxo_esist < 0")), _
                                               "ec_codart, ec_magaz, ec_commeca, ec_fase" & strQuery) Then Return False
        For i = 0 To dttTmp.Rows.Count - 1
          dttGr.ImportRow(dttTmp.Rows(i))
        Next
      End If
      dttGr.AcceptChanges()

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
  '-------------------------------------

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
      bOk = oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, strTabella, strTipoCod, strTmp, dttTmp)
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
      bOk = oCldDocu.ValCodiceDb(strCodpaga, strDittaCorrente, "PAGA", "N", "", dttTmp)
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

  Public Overridable Sub CambioScontiTest()
    '------------------------------
    'viene chiamata quando si cambiano gli sconti di testata o di piede 
    'per ricalcolare il valore delle righe nel corpo del documento
    Dim i As Integer = 0
    Try
      If dttEC.Rows.Count = 0 Then Return

      While i < dttEC.Rows.Count
        If bModPM Then
          'legge i valori preesistenti
          Dim dttclone As DataTable
          dttclone = dttEC.Clone()
          dttclone.ImportRow(dttEC.Rows(i))
          dttclone.Rows(0).RejectChanges()
          If BORDINI Then
            If Not oCldDocu.AggProesebDaMovordDao(strDittaCorrente, 1, "MOTRANS", 0, "TTPROESEC", lIIttproesebappo, _
                                          dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                          dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                          NTSCInt(dttEC.Rows(i)!ec_riga), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                          Nothing, dttclone) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129446546154648437, "Attenzione: non è stato possibile aggiornare la commessa (proeseb) sugli archivi !")))
            End If
          Else
            If Not oCldDocu.AggProesebDaMovmagDao(strDittaCorrente, 1, "MMTRANS", 0, "TTPROESEC", lIIttproesebappo, _
                                          dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                          dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                          NTSCInt(dttEC.Rows(i)!ec_riga), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                          Nothing, dttclone) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129445644849931640, "Attenzione: non è stato possibile aggiornare la commessa (proeseb) sugli archivi !")))
            End If
          End If
          'agg. altri record di mmtrans, riferiti allo stesso task-id, se presenti
          dQtadispass = 0 : dValdispass = 0
          AggResAltriRkCorpo(1, 1, dttEC, dttclone.Rows(0), dQtadispass, dValdispass)
        End If

        SettaValoriRiga(dttEC.Rows(i))

        If bModPM Then
          If BORDINI Then
            If Not oCldDocu.AggProesebDaMovordDao(strDittaCorrente, 0, "MOTRANS", 0, "TTPROESEC", lIIttproesebappo, _
                    dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                    dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                    NTSCInt(dttEC.Rows(i)!ec_riga), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                    Nothing, dttEC) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129446546510400390, "Attenzione: non è stato possibile aggiornare la commessa (proeseb) sugli archivi !")))
            End If
          Else
            If Not oCldDocu.AggProesebDaMovmagDao(strDittaCorrente, 0, "MMTRANS", 0, "TTPROESEC", lIIttproesebappo, _
                                dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                NTSCInt(dttEC.Rows(i)!ec_riga), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                Nothing, dttEC) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129445644868330078, "Attenzione: non è stato possibile aggiornare la commessa (proeseb) sugli archivi !")))
            End If
          End If

          ' scrive per stornare i vecchi valori
          Dim dttclone As DataTable
          dttclone = dttEC.Clone()
          dttclone.ImportRow(dttEC.Rows(i))
          dttclone.Rows(0).RejectChanges()
          AggResAltriRkCorpo(1, 2, dttEC, dttclone.Rows(0), dQtadispass, dValdispass)
          ' scrive completamente i nuovi valori
          AggResAltriRkCorpo(0, 0, dttEC, dttEC.Rows(i), dQtadispass, dValdispass)
        End If

        dttEC.Rows(i).EndEdit()
        i += 1
      End While

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

  Public Overridable Sub CambioDatiTestataInGriglia(ByVal nCodese As Integer, ByVal nMagaz As Integer, ByVal nMagaz2 As Integer, _
                                      ByVal nMagimp As Integer, ByVal nControp As Integer, ByVal lCommeca As Integer, _
                                      ByVal strConfermato As String, ByVal strDatCons As String, ByVal strSubcommeca As String)

    Try
      CambioDatiTestataInGriglia(nCodese, nMagaz, nMagaz2, nMagimp, nControp, lCommeca, strConfermato, strDatCons, strSubcommeca, "")
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
  Public Overridable Sub CambioDatiTestataInGriglia(ByVal nCodese As Integer, ByVal nMagaz As Integer, ByVal nMagaz2 As Integer, _
                                        ByVal nMagimp As Integer, ByVal nControp As Integer, ByVal lCommeca As Integer, _
                                        ByVal strConfermato As String, ByVal strDatCons As String, ByVal strSubcommeca As String, _
                                        ByVal strRilasciato As String)
    '--------------------------------------------------
    'cambio i dati di griglia al variare dei dati di testata (chiamato dall UI)
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim lDiffGG As Integer = 0
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nCodese, nMagaz, nMagaz2, nMagimp, nControp, lCommeca, strConfermato, strDatCons, strSubcommeca, strRilasciato})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return
      End If
      '----------------

      If dttEC.Rows.Count = 0 Then Return

      '-------------------------------
      'righe del documento movmag/movord/movprb/...
      If nMagimp = 0 Then
        For i = 0 To dttEC.Rows.Count - 1
          If nCodese <> 0 Then dttEC.Rows(i)!ec_codiva = nCodese
          If nMagaz <> 0 Then dttEC.Rows(i)!ec_magaz = nMagaz
          If nMagaz2 <> 0 Then dttEC.Rows(i)!ec_magaz2 = nMagaz2
          If nControp <> 0 Then dttEC.Rows(i)!ec_controp = nControp
          If lCommeca <> 0 Then dttEC.Rows(i)!ec_commeca = lCommeca
          If strConfermato <> "" Then
            'se non imposto bNonValidare = true va nella afteruodate di ec_confermato dove chiede ancora se aggioranre anche gli impegni/scarichi collegati !!!!
            bNonValidare = True
            bHasChangesET = True
            dttEC.Rows(i)!ec_confermato = strConfermato
            bNonValidare = False
            dttEC.Rows(i).AcceptChanges()
          End If
          If strRilasciato <> "" Then dttEC.Rows(i)!ec_rilasciato = strRilasciato
          If strDatCons <> "" Then
            'calcola la differenza di giorni tra la vecchia data e la nuova data
            lDiffGG = NTSCInt(DateDiff("d", NTSCDate(dttEC.Rows(i)!ec_datcons), NTSCDate(strDatCons)))
            dttEC.Rows(i)!ec_datcons = strDatCons
            If NTSCStr(dttET.Rows(0)!et_tipork) = "H" Then
              dtrT = dttECIMP.Select("ec_rigaor = " & NTSCInt(dttEC.Rows(i)!ec_riga).ToString)
              For l = 0 To dtrT.Length - 1
                'aggiorna impegni collegati e lavorazioni
                dtrT(l)!ec_datcons = DateAdd("d", NTSCDec(lDiffGG), NTSCDate(dtrT(l)!ec_datcons))
              Next
              dtrT = dttATTIVIT.Select("at_riga = " & NTSCInt(dttEC.Rows(i)!ec_riga).ToString)
              For l = 0 To dtrT.Length - 1
                'aggiorna lavorazioni
                dtrT(l)!at_dtminima = DateAdd("d", NTSCDec(lDiffGG), dtrT(l)!at_dtminima)
                dtrT(l)!at_datcons = DateAdd("d", NTSCDec(lDiffGG), dtrT(l)!at_datcons)
              Next
            End If
            dttECIMP.AcceptChanges()
            dttATTIVIT.AcceptChanges()
          End If
          If strSubcommeca <> "" Then dttEC.Rows(i)!ec_subcommeca = strSubcommeca
        Next
        dttEC.AcceptChanges()
      End If    'If nMagimp = 0 Then

      '-------------------------------
      'devo aggiornare anche gli impegni/scarichi collegati
      If NTSCStr(dttET.Rows(0)!et_tipork) = "H" Or NTSCStr(dttET.Rows(0)!et_tipork) = "T" Then
        If nMagimp <> 0 Then
          For i = 0 To dttECIMP.Rows.Count - 1
            dttECIMP.Rows(i)!ec_magaz = nMagimp
          Next
        End If
        If lCommeca <> 0 Then
          For i = 0 To dttECIMP.Rows.Count - 1
            dttECIMP.Rows(i)!ec_commeca = lCommeca
          Next
        End If
        If strSubcommeca <> "" Then
          For i = 0 To dttECIMP.Rows.Count - 1
            dttECIMP.Rows(i)!ec_subcommeca = strSubcommeca
          Next
        End If
        If strConfermato <> "" Then
          For i = 0 To dttECIMP.Rows.Count - 1
            dttECIMP.Rows(i)!ec_confermato = strConfermato
          Next
        End If
        If strRilasciato <> "" Then
          For i = 0 To dttECIMP.Rows.Count - 1
            dttECIMP.Rows(i)!ec_rilasciato = strRilasciato
          Next
        End If
        dttECIMP.AcceptChanges()
      End If    'If NTSCStr(dttET.Rows(0)!et_tipork) = "H" Or NTSCStr(dttET.Rows(0)!et_tipork) = "T" Then

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

  Public Overridable Sub AggiornaCambio(ByVal nCodval As Integer, ByVal dtDataValidita As String, ByVal dCambio As Decimal, ByVal bSovrascrivi As Boolean)
    Try

      oCldDocu.AggiornaCambio(nCodval, dtDataValidita, dCambio, bSovrascrivi)

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

  Public Overridable Function CercaLeadDaConto(ByVal lConto As Integer, ByVal lCoddest1 As Integer) As Integer
    Try

      Return oCldDocu.CercaLeadDaConto(strDittaCorrente, dttET.Rows(0)!et_conto.ToString, lConto, lCoddest1, _
                                       lModuliDittaDitt, lModuliExtDittaDitt, lModuliSupDittaDitt)

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

  Public Overridable Function DescrBlocco(ByVal strTipo As String) As String
    Select Case strTipo
      Case "N" : DescrBlocco = oApp.Tr(Me, 128661971238906250, "NESSUN BLOCCO")
      Case "I" : DescrBlocco = oApp.Tr(Me, 128661971377656250, "INSOLUTI")
      Case "F" : DescrBlocco = oApp.Tr(Me, 128661971391562500, "FUORI FIDO")
      Case "R" : DescrBlocco = oApp.Tr(Me, 128661971402968750, "R.D. SCADUTE")
      Case "B" : DescrBlocco = oApp.Tr(Me, 128661971415000000, "FISSO")
      Case Else : DescrBlocco = oApp.Tr(Me, 128661971425000000, "BLOCCO '|" & strTipo & "|' SCONOSCIUTO")
    End Select
  End Function

  Public Overridable Function MagazSecondario(ByRef dtrT As DataRow, ByRef f2Mag As Boolean) As Boolean
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(dtrT!et_causale) <> 0 And dtrT!et_tipork.ToString <> "T" Then
        If oCldDocu.ValCodiceDb(NTSCInt(dtrT!et_causale).ToString, strDittaCorrente, "TABCAUM", "N", "", dttTmp) Then
          If NTSCInt(dttTmp.Rows(0)!tb_causec) > 0 Then
            f2Mag = True
          Else
            f2Mag = False
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

  Public Overridable Function DescrTipork(ByVal strTipork As String) As String
    Select Case strTipork
      Case "A" : DescrTipork = "'Fattura imm. emessa'"
      Case "B" : DescrTipork = "'D.D.T. emesso'"
      Case "C" : DescrTipork = "'Corrispettivo'"
      Case "D" : DescrTipork = "'Fattura diff. emessa'"
      Case "E" : DescrTipork = "'Nota di addebito emessa'"
      Case "F" : DescrTipork = "'Ricevuta fisc. emessa'"
      Case "I" : DescrTipork = "'Riemissione ric. fiscale'"
      Case "J" : DescrTipork = "'Nota di accredito ricevuta'"
      Case "K" : DescrTipork = "'Fattura diff. ricevuta'"
      Case "L" : DescrTipork = "'Fattura imm. ricevuta'"
      Case "M" : DescrTipork = "'D.D.T. ricevuto'"
      Case "N" : DescrTipork = "'Nota di accredito emessa'"
      Case "P" : DescrTipork = "'Fattura ric. fisc. differita'"
      Case "S" : DescrTipork = "'Fattura ric. fisc. emessa'"
      Case "T" : DescrTipork = "'Carico di produzione'"
      Case "U" : DescrTipork = "'Scarico di produzione'"
      Case "Z" : DescrTipork = "'Bolla di mov. interna'"
      Case "W" : DescrTipork = "'Nota di prelievo'"
      Case "£" : DescrTipork = "'Nota accred. diff. emessa'"
      Case "(" : DescrTipork = "'Nota accred. diff. ricevuta'"
      Case Else : DescrTipork = "'" & strTipork & "'"
    End Select
  End Function

  Public Overridable Function DescrTiporkOr(ByVal strTipork As String) As String
    Select Case strTipork
      Case "#" : Return oApp.Tr(Me, 128668063228593750, "'Impegno di commessa'")
      Case "$" : Return oApp.Tr(Me, 128668063393437500, "'Ordine a fornitore aperto'")
      Case "Y" : Return oApp.Tr(Me, 128668063410781250, "'Impegno di produzione'")
      Case "H" : Return oApp.Tr(Me, 128668063422656250, "'Ordine di produzione'")
      Case "O" : Return oApp.Tr(Me, 128668063437187500, "'Ordine a fornitore'")
      Case "R" : Return oApp.Tr(Me, 128668063447500000, "'Impegno cliente'")
      Case "V" : Return oApp.Tr(Me, 128668063456875000, "'Impegno cliente aperto'")
      Case "X" : Return oApp.Tr(Me, 128668063467031250, "'Impegno di trasferimento'")
      Case "Q" : Return oApp.Tr(Me, 128668063477500000, "'Preventivo'")
      Case Else : Return "'" & strTipork & "'"
    End Select
  End Function

  '-------------------------------------

  Public Overridable Function CheckFidoInsol(ByVal bDopo As Boolean, ByVal lConto As Integer, ByRef dttEt As DataTable, ByRef bBloccaCli As Boolean) As Boolean
    '-------------------------
    'utilizzabile sia per ORDINI CHE PER DOCUMENTI DI MAGAZZINO
    Dim oDcst As New CLE__DCST
    Dim bBloc As Boolean = False
    Dim oCleDcst As CLECGDCST = Nothing
    Dim nEscomp As Integer
    Try
      bBloc = Not bDopo

      oCldDocu.EscompFromDate(strDittaCorrente, NTSCDate(dttEt.Rows(0)!et_datdoc), nEscomp, Nothing)

      With oDcst
        .lConto = lConto
        .nEscomp = nEscomp
        .strData = NTSCDate(dttEt.Rows(0)!et_datdoc).ToShortDateString
        .bControllaFido = True
        .bControllaInsoluti = True
        .bStatPagamenti = True
        If BORDINI Then
          .dttMotrans = dttEC
        Else
          .dttMmtrans = dttEC
        End If
        .dImpOdierno = NTSCDec(dttEt.Rows(0)!et_totdoc)
        .bOpendoc = Not bNew
        .strTipork = dttEt.Rows(0)!et_tipork.ToString
        .nAnno = NTSCInt(dttEt.Rows(0)!et_anno)
        .strSerie = dttEt.Rows(0)!et_serie.ToString
        .lNumdoc = NTSCInt(dttEt.Rows(0)!et_numdoc)
        .bBlocca = bBloc
        .bVisForm = False
        If bInCreaDocDaGnor Then .bVismess = False Else .bVismess = True
      End With

      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEMGDOCU", "BECGDCST", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 130597607173640172, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleDcst = CType(oTmp, CLECGDCST)
      '------------------------------------------------
      AddHandler oCleDcst.RemoteEvent, AddressOf GestisciEventiEntityDcst
      If oCleDcst.Init(oApp, oScript, oCleComm, "", False, "", "") = False Then Return True
      oCleDcst.oDcst = oDcst
      If Not oCleDcst.Esegui(strDittaCorrente) Then Return False
      bBloccaCli = oDcst.bClienteBloccatoOut      'se serve avviso di bloccare il cliente 

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
      If Not oCleDcst Is Nothing Then RemoveHandler oCleDcst.RemoteEvent, AddressOf GestisciEventiEntityDcst
    End Try
  End Function
  Public Overridable Sub GestisciEventiEntityDcst(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      'giro il messaggio al BN 'può essere solo un messaggio di avvertimento di fuori fido, insoluti, ...
      ThrowRemoteEvent(e)
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

  '-------------------------------------

  Public Overridable Function ValorizzaProduzione(ByVal dtrEC As DataRow) As Boolean
    '------------------------------------
    ' VALORIZZA L'INTERO ORDINE DI PROD (Se dtrEc = nothing) O UNA SINGOLA RIGA
    '
    ' - negli ordini di produzione INTERNI mo_prezzo viene determinato
    '   dal valore costi di lavorazione (as_valore) diviso quantità ordinata
    ' - negli ordini di produzione ESTERNA (terzisti) è il costo lavorazione
    '   (as_valore) che viene determinato da mm_prezzo x quantità
    ' - as_valore/as_valorev è sempre riferito sempre alla quantità ordinata
    '   (come mo_valoremm) e non alla quantità residua
    '
    ' Setta mo_valoremm (e mo_prezzo solo sugli ordini di prod. INTERNI)
    ' Setta mo_valore (che è sempre dato da quantord-quaeva x prezzo)
    '
    ' - ignora le provvigioni e gli sconti sia su ordini che impegni

    'IN BEORGSOR ec_valore è il valore resido da evadere
    '            e negli ordini da prod contiene quantità * prezzo ancora da evadere (sia ordini interni che esterni)
    '            ec_valoremm è il valore riga originario totale
    '            e negli ordini da prod contiene costo materiali + (quantità * prezzo - costo lavorazione) (sia ordini interni che esterni)
    'IN BEVEBOLL ec_valore è il valore riga totale
    '            e nei carichi da prod contiene costo materiali + (quantità * prezzo - costo lavorazione) (sia ordini interni che esterni)

    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim dScontV As Decimal
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1 As DataRow = Nothing
    Dim dtrRows() As DataRow = Nothing
    Dim strColValore As String = ""
    Dim strTmp As String = ""
    Try
      If dtrEC Is Nothing Then Return True
      If dtrEC!ec_tipork.ToString <> "H" And dtrEC!ec_tipork.ToString <> "T" Then Return True

      If BGESTORDLIST Or BORDINI = False Then
        strColValore = "ec_valore"
      Else
        strColValore = "ec_valoremm"
      End If

      bNonValidare = True

      '----------------------------
      'determino le righe di dttEC da rivalorizzare (tutte o solo una)
      If dtrEC Is Nothing Then
        dtrRows = dttEC.Select()
      Else
        dtrRows = dttEC.Select("ec_riga = " & dtrEC!ec_riga.ToString)
      End If

      'Azzera mo_valoremm / mm_valore. per veboll anche valorev
      For i = 0 To dtrRows.Length - 1
        dtrRows(i)(strColValore) = 0
        If BORDINI And bTerzista Then
          'se sono ordini di prod terzista il valore è già ok (come per prezzo di acquisto)
        Else
          'azzero: in ordini di prod interni la valuta è sempre = 0
          dtrRows(i)!ec_valorev = 0
        End If
      Next

      If NTSCDec(dtrEC!ec_scontv) <> 0 And NTSCStr(dttET.Rows(0)!et_scorpo) = "S" Then
        oCldDocu.Scorporo(NTSCDec(dtrEC!ec_scontv), NTSCInt(dtrEC!ec_codiva), dScontV, 0)
      Else
        dScontV = NTSCDec(dtrEC!ec_scontv)
      End If

      If bTerzista Then
        'calcola as_valore / lce_valore da mo_prezzo, as_valorev / lce_valorev da mo_prezvalc
        'Non tengo conto negli ordini di produzione INTERNI degli sconti di riga.
        For i = 0 To dtrRows.Length - 1
          If BORDINI Then
            dtrT = dttASSRIS.Select("as_riga = " & dtrRows(i)!ec_riga.ToString)
            For l = 0 To dtrT.Length - 1
              dtrT(l)!as_valore = ArrDbl(ArrDbl(NTSCDec(dtrRows(i)!ec_prezzo) * _
                                         NTSCDec(IIf(dtrRows(i)!ec_umprz.ToString <> "S", dtrRows(i)!ec_quant, dtrRows(i)!ec_colli)) / _
                                         NTSCDec(dtrRows(i)!ec_perqta) * _
                                         (100 - NTSCDec(dtrEC!ec_scont1)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont2)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont3)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont4)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont5)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont6)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scontp)) / 100 - dScontV, oCldDocu.TrovaNdec(0)) * _
                                         (100 - NTSCDec(dttET.Rows(0)!et_scont1)) / 100 * _
                                         (100 - NTSCDec(dttET.Rows(0)!et_scont2)) / 100 * _
                                         (100 - NTSCDec(dttET.Rows(0)!et_scopag)) / 100, _
                                         oCldDocu.TrovaNdec(0))
              If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
                dtrT(l)!as_valorev = ArrDbl(ArrDbl(NTSCDec(dtrRows(i)!ec_prezvalc) * _
                                         NTSCDec(IIf(dtrRows(i)!ec_umprz.ToString <> "S", dtrRows(i)!ec_quant, dtrRows(i)!ec_colli)) / _
                                         NTSCDec(dtrRows(i)!ec_perqta) * _
                                         (100 - NTSCDec(dtrEC!ec_scont1)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont2)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont3)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont4)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont5)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont6)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scontp)) / 100 - dScontV, oCldDocu.TrovaNdec(0)) * _
                                         (100 - NTSCDec(dttET.Rows(0)!et_scont1)) / 100 * _
                                         (100 - NTSCDec(dttET.Rows(0)!et_scont2)) / 100 * _
                                         (100 - NTSCDec(dttET.Rows(0)!et_scopag)) / 100, _
                                         oCldDocu.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta)))
              Else
                dtrT(l)!as_valorev = 0
              End If
            Next
          Else
            dtrT = dttLAVCENT.Select("lce_riga = " & dtrRows(i)!ec_riga.ToString)
            For l = 0 To dtrT.Length - 1
              dtrT(l)!lce_valore = ArrDbl(ArrDbl(NTSCDec(dtrRows(i)!ec_prezzo) * _
                                          NTSCDec(IIf(dtrRows(i)!ec_umprz.ToString <> "S", dtrRows(i)!ec_quant, dtrRows(i)!ec_colli)) / _
                                          NTSCDec(dtrRows(i)!ec_perqta) * _
                                         (100 - NTSCDec(dtrEC!ec_scont1)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont2)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont3)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont4)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont5)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont6)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scontp)) / 100 - _
                                         dScontV, oCldDocu.TrovaNdec(0)) * _
                                         (100 - NTSCDec(dttET.Rows(0)!et_scont1)) / 100 * _
                                         (100 - NTSCDec(dttET.Rows(0)!et_scont2)) / 100 * _
                                         (100 - NTSCDec(dttET.Rows(0)!et_scopag)) / 100, _
                                         oCldDocu.TrovaNdec(0))
              If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
                dtrT(l)!lce_valorev = ArrDbl(ArrDbl(NTSCDec(dtrRows(i)!ec_prezvalc) * _
                                         NTSCDec(IIf(dtrRows(i)!ec_umprz.ToString <> "S", dtrRows(i)!ec_quant, dtrRows(i)!ec_colli)) / _
                                         NTSCDec(dtrRows(i)!ec_perqta) * _
                                         (100 - NTSCDec(dtrEC!ec_scont1)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont2)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont3)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont4)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont5)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scont6)) / 100 * _
                                         (100 - NTSCDec(dtrEC!ec_scontp)) / 100 - _
                                         dScontV, oCldDocu.TrovaNdec(0)) * _
                                         (100 - NTSCDec(dttET.Rows(0)!et_scont1)) / 100 * _
                                         (100 - NTSCDec(dttET.Rows(0)!et_scont2)) / 100 * _
                                         (100 - NTSCDec(dttET.Rows(0)!et_scopag)) / 100, _
                                         oCldDocu.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta)))
              Else
                dtrT(l)!lce_valorev = 0
              End If
            Next
          End If
        Next    'For i = 0 To dtrRows.Length - 1
      End If    'If bTerzista Then

      Select Case strTipValSemPf
        'Attenzione: nei terzisti obbligatoria una sola riga di lavorazioni
        Case "1"
          'mo_valoremm non include il costo delle lavorazioni
        Case "2"
          'mo_valoremm include solo il costo delle lavorazioni dei terzisti
          If bTerzista Then
            If BORDINI Then
              If BGESTORDLIST = False Then
                For i = 0 To dtrRows.Length - 1
                  dtrT = dttASSRIS.Select("as_riga = " & dtrRows(i)!ec_riga.ToString)
                  For l = 0 To dtrT.Length - 1
                    dtrRows(i)!ec_valoremm = ArrDbl(NTSCDec(dtrRows(i)!ec_valoremm) + NTSCDec(dtrT(l)!as_valore), oCldDocu.TrovaNdec(0))
                  Next
                Next
              End If
            Else
              For i = 0 To dtrRows.Length - 1
                dtrT = dttLAVCENT.Select("lce_riga = " & dtrRows(i)!ec_riga.ToString)
                For l = 0 To dtrT.Length - 1
                  dtrRows(i)!ec_valore = ArrDbl(NTSCDec(dtrRows(i)!ec_valore) + NTSCDec(dtrT(l)!lce_valore), oCldDocu.TrovaNdec(0))
                  If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
                    dtrRows(i)!ec_valorev = ArrDbl(NTSCDec(dtrRows(i)!ec_valorev) + NTSCDec(dtrT(l)!lce_valorev), oCldDocu.TrovaNdec(0))
                  Else
                    dtrRows(i)!ec_valorev = 0
                  End If
                Next
              Next
            End If    'If BORDINI Then
          End If    'If bTerzista Then

        Case " ", "4"
          'Caso normale
          If BORDINI Then
            If BGESTORDLIST = False Then
              For i = 0 To dtrRows.Length - 1
                dtrT = dttASSRIS.Select("as_riga = " & dtrRows(i)!ec_riga.ToString)
                For l = 0 To dtrT.Length - 1
                  dtrRows(i)!ec_valoremm = ArrDbl(NTSCDec(dtrRows(i)!ec_valoremm) + NTSCDec(dtrT(l)!as_valore) + NTSCDec(dtrT(l)!as_valmo), oCldDocu.TrovaNdec(0))
                Next
              Next
            Else
              For i = 0 To dtrRows.Length - 1
                dtrT = dttASSRIS.Select("as_riga = " & dtrRows(i)!ec_riga.ToString)
                For l = 0 To dtrT.Length - 1
                  dtrRows(i)!ec_valore = ArrDbl(NTSCDec(dtrRows(i)!ec_valore) + NTSCDec(dtrT(l)!as_valore) + NTSCDec(dtrT(l)!as_valmo), oCldDocu.TrovaNdec(0))
                Next
              Next
            End If
          Else
            For i = 0 To dtrRows.Length - 1
              dtrT = dttLAVCENT.Select("lce_riga = " & dtrRows(i)!ec_riga.ToString)
              For l = 0 To dtrT.Length - 1
                dtrRows(i)!ec_valore = ArrDbl(NTSCDec(dtrRows(i)!ec_valore) + NTSCDec(dtrT(l)!lce_valore) + NTSCDec(dtrT(l)!lce_valmo), oCldDocu.TrovaNdec(0))
                If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
                  dtrRows(i)!ec_valorev = ArrDbl(NTSCDec(dtrRows(i)!ec_valorev) + NTSCDec(dtrT(l)!lce_valorev), oCldDocu.TrovaNdec(0))
                Else
                  dtrRows(i)!ec_valorev = 0
                End If
              Next
            Next
          End If    'If BORDINI Then

        Case Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128552514051040952, "Tipo di valorizzazione '|" & strTipValSemPf & "|' non gestito.")))
      End Select

      If Not bTerzista Then
        'Aggiusta il prezzo (comprende solo il costo delle lavorazioni)
        'Non tengo mai conto negli ordini di produzione INTERNI degli sconti di riga.
        'MEMORIZZO NEL PREZZO DELLA RIGA PRODOTTA IL COSTO TOTALE DELLA LAVORAZIONE SE LAVORAZ. INTERNA
        For i = 0 To dtrRows.Length - 1
          If NTSCDec(dtrRows(i)!ec_quant) <> 0 Then
            dtrRows(i)!ec_prezzo = ArrDbl(NTSCDec(dtrRows(i)(strColValore)) / _
                                          NTSCDec(dtrRows(i)!ec_quant) * _
                                          NTSCDec(dtrRows(i)!ec_perqta), oCldDocu.TrovaNdecSuPrzUn(0))
          Else
            dtrRows(i)!ec_prezzo = 0
          End If
          dtrRows(i)!ec_prezvalc = 0
        Next
      End If

      'Materiali (esclude quelli su magazzino merce altrui purchè non a fasi)
      'Se l'opzione \opzioni\Tipo_valorizz_sem_pf ha il nuovo valore 4
      'consiudera anche i materiali sui magazzini di tipo MERCE ALTRUI *
      For i = 0 To dtrRows.Length - 1
        If BORDINI Then
          dtrT = dttECIMP.Select("ec_rigaor = " & dtrRows(i)!ec_riga.ToString)
        Else
          dtrT = dttECIMP.Select("ec_prriga = " & dtrRows(i)!ec_riga.ToString)
        End If    'If BORDINI Then
        For l = 0 To dtrT.Length - 1
          If strTipValSemPf <> "4" Then
            dtrT1 = GetTabmagaRow(NTSCInt(dtrT(l)!ec_magaz))
            If Not dtrT1 Is Nothing Then
              If (dtrT1!tb_flclavo.ToString <> "C" And dtrT1!tb_flclavo.ToString <> "X") Or _
                  ((dtrT1!tb_flclavo.ToString = "C" Or dtrT1!tb_flclavo.ToString = "X") And NTSCInt(dtrT(l)!ec_fase) <> 0) Then
                dtrRows(i)(strColValore) = NTSCDec(dtrRows(i)(strColValore)) + NTSCDec(dtrT(l)(strColValore))
                dtrRows(i)!ec_valorev = NTSCDec(dtrRows(i)!ec_valorev) + NTSCDec(dtrT(l)!ec_valorev)
              End If
            End If
          Else
            dtrRows(i)(strColValore) = NTSCDec(dtrRows(i)(strColValore)) + NTSCDec(dtrT(l)(strColValore))
            dtrRows(i)!ec_valorev = NTSCDec(dtrRows(i)!ec_valorev) + NTSCDec(dtrT(l)!ec_valorev)
          End If
        Next    'For l = 0 To dtrT.Length - 1
      Next  'For i = 0 To dtrRows.Length - 1

      'Non effettua alcuna operazione sulle provvigioni
      'nei carichi di produzioni non sono gestite.
      CalcolaValoreLavorazioni(NTSCStr(dttET.Rows(0)!et_tipork))

      bNonValidare = False

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
      bNonValidare = False
    End Try
  End Function
  Public Overridable Sub CalcolaValoreLavorazioni(ByVal strTipoRk As String)
    Try
      If strTipoRk <> "T" AndAlso strTipoRk <> "H" Then Return

      'Calcola il valore effettivo delle lavorazioni.
      If BGESTORDLIST = False Then
        For Each dtrRiga As DataRow In dttEC.Rows
          Dim dValoreLav As Decimal = 0
          If BORDINI Then
            For Each dtrAtt As DataRow In dttATTIVIT.Select("at_riga = " & dtrRiga!ec_riga.ToString)
              For Each dtrAss As DataRow In dttASSRIS.Select("as_riga = " & NTSCInt(dtrRiga!ec_riga) & " AND as_fase = " & NTSCInt(dtrAtt!at_fase))
                dValoreLav += ArrDbl(NTSCDec(dtrAtt!at_qtapr) * NTSCDec(dtrAss!as_valore), oCldDocu.TrovaNdec(0))
              Next
            Next
          Else
            For Each dtrLav As DataRow In dttLAVCENT.Select("lce_riga = " & dtrRiga!ec_riga.ToString)
              dValoreLav += ArrDbl(NTSCDec(dtrLav!lce_qtaes) * NTSCDec(dtrLav!lce_valore), oCldDocu.TrovaNdec(0))
            Next
          End If    'If BORDINI Then
          dtrRiga!xxo_valorelav = dValoreLav
        Next
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
  End Sub

  Public Overridable Function CalcolaTotali() As Boolean
    Dim oTestata As CLELBMENU.OutTestata = Nothing
    Try
      Return CalcolaTotali(oTestata)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      oTestata = Nothing
    End Try
  End Function
  Public Overridable Function CalcolaTotali(ByRef oTestata As CLELBMENU.OutTestata) As Boolean
    Dim strError As String = ""
    Dim oclPar As CLELBMENU.Parametri = New CLELBMENU.Parametri
    'Dim oTestata As CLELBMENU.OutTestata = New CLELBMENU.OutTestata
    Dim oCorpo() As CLELBMENU.OutCorpo
    Dim i As Integer = 0
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {oTestata})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        oTestata = CType(oIn(0), CLELBMENU.OutTestata)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      If bNoCalcolaTotali Then Return True

      oTestata = New CLELBMENU.OutTestata
      '---------------------------------------
      'ricalcolo i totali del documento
      If dttET Is Nothing Then Return True
      If dttET.Rows.Count = 0 Then Return True

      '---------------------------------------
      'confermo tutte le modifiche
      dttEC.AcceptChanges()       'NON TOGLIERLO ALTRIMENTI DA ERRORE IN QUESTO CASO: nuovo ic, ins cli, ins mp1, qta 10, prz 10, cambo sconto di testa 1 e sconto di testata 2: poi da errore

      If dttEC.Rows.Count > 0 Then
        If (dttET.Rows(0)!et_tipork.ToString = "T" Or dttET.Rows(0)!et_tipork.ToString = "H") And bTerzista = False Then
          If NTSCDec(dttET.Rows(0)!et_scont1) <> 0 Or _
             NTSCDec(dttET.Rows(0)!et_scont2) <> 0 Or _
             NTSCDec(dttET.Rows(0)!et_scopag) <> 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129609868169667969, "ATTENZIONE: nei documenti di produzione INTERNI gli sconti di testata e sconto pagamento devono essere uguali a 0.")))
            Return False
          End If
        End If
      End If

      ReDim oCorpo(dttEC.Rows.Count)

      '---------------------------------------
      'compilo i parametri che piloteranno il ricalcolo del documento
      With oclPar
        .strNomProg = IIf(BORDINI, "BSORGSOR", "BSVEBOLL").ToString
        .bNew = bNew
        .bDocEmesso = bDocEmesso
        .bCalcolaBolli = CBool(IIf(dttET.Rows(0)!et_flboll.ToString = "S", True, False))
        .bCalcolaColli = CBool(IIf(bNonCalcolaColli, False, bCalcolaColli))
        .bCalcolaColliPesiSuDocAperti = bCalcolaColliPesiSuDocAperti
        .bCalcolaPesoLordo = bCalcolaPesoLordo
        .bCalcolaPesoNetto = bCalcolaPesoNetto
        .bCalcPesi = CBool(IIf(strCalcPesi = "S", True, False))
        .bNoPesiSuRigheKitFittizie = bNoPesiSuRigheKitFittizie
        .bNonCalcolaProvvig = False
        If BORDINI Then
          .bSbloccaIva = False
        Else
          .bSbloccaIva = CBool(IIf(dttET.Rows(0)!et_flscdb.ToString = "S", True, False))
          .bSegueFatt = bSegueFatt
        End If
        .strCalcolaSpeseTrasp = strCalcolaSpeseTrasp
        .bCalcolaSpeseTraspSoloNuovi = bCalcolaSpeseTraspSoloNuovi
        .bUsaVettore1perCalcoloSpeseTrasp = bUsaVettore1perCalcoloSpeseTrasp
        .nGestScostAcconti = 0
        .dImpoScostAcconti = 0
        .nCodese = NTSCInt(dttET.Rows(0)!et_codese)
        .nPeacIva15 = nPeacIva15
        If NTSCDate(dttET.Rows(0)!et_datdoc) < NTSCDate(strPeacIvaInc3Fine) Then
          .nPeacIvainc = nPeacIvaInc3
        ElseIf NTSCDate(dttET.Rows(0)!et_datdoc) < NTSCDate(strPeacIvaInc2Fine) Then
          .nPeacIvainc = nPeacIvaInc2
        Else
          .nPeacIvainc = nPeacIvainc
        End If
        .nPeveIva15 = nPeveIva15
        If NTSCDate(dttET.Rows(0)!et_datdoc) < NTSCDate(strPeveIvaInc3Fine) Then
          .nPeveIvaInc = nPeveIvaInc3
        ElseIf NTSCDate(dttET.Rows(0)!et_datdoc) < NTSCDate(strPeveIvaInc2Fine) Then
          .nPeveIvaInc = nPeveIvaInc2
        Else
          .nPeveIvaInc = nPeveIvaInc
        End If
        .bDeterminaBolliSuOperazEsenti = bDeterminaBolliSuOperazEsenti
        .bIncassoTotalePagContanti = bIncassoTotalePagContanti
      End With

      bNonValidare = True

      '----------------------------
      'Eseguo la CType(oCleComm, CLELBMENU).CalcolaDoc e aggiorno testord
      If CalcolaTotaliFase2(dttET, dttETC, oclPar, oTestata, oCorpo) Then
        If bDocRetail Then
          'Non era possibile metterlo dentro la calcola totali, visto che ha un ulteriore comportamento diverso tra il retail e gestione documenti\ordini
          oTestata.dRimanenza += NTSCDec(dttET.Rows(0)!et_resto) - NTSCDec(dttET.Rows(0)!et_pagato2)
        End If
        'avviso l'UI di aggiornare i totali dei campi UNBOUND
        ThrowRemoteEvent(New NTSEventArgs("AggTotali.:" & _
                          oTestata.dTotSpese.ToString & "§" & _
                          oTestata.dTotSpesev.ToString & "§" & _
                          oTestata.dTotImpon.ToString & "§" & _
                          oTestata.dTotImponv.ToString & "§" & _
                          oTestata.dTotIva.ToString & "§" & _
                          oTestata.dTotIvav.ToString & "§" & _
                          (oTestata.dTotdoc - oTestata.dTotomag).ToString & "§" & _
                          (oTestata.dTotdocv - oTestata.dTotomagv).ToString & "§" & _
                          oTestata.dRimanenza.ToString & "§" & _
                          oTestata.dRimanenzav.ToString, ""))

        '--------------------
        'rispetto alla routine standard devo riversare i totali contropartite in tstmagc/testprbc
        If BGESTORDLIST = False Then
          For i = 1 To 8
            If NTSCDec(dttET.Rows(0)("et_codiva_" & i.ToString)) <> NTSCDec(oTestata.nCodiva(i - 1)) Then dttET.Rows(0)("et_codiva_" & i.ToString) = oTestata.nCodiva(i - 1)
            If NTSCDec(dttET.Rows(0)("et_imponib_" & i.ToString)) <> NTSCDec(oTestata.dImponib(i - 1)) Then dttET.Rows(0)("et_imponib_" & i.ToString) = oTestata.dImponib(i - 1)
            If NTSCDec(dttET.Rows(0)("et_imposta_" & i.ToString)) <> NTSCDec(oTestata.dImposta(i - 1)) Then dttET.Rows(0)("et_imposta_" & i.ToString) = oTestata.dImposta(i - 1)
            If NTSCDec(dttET.Rows(0)("et_imponibv_" & i.ToString)) <> NTSCDec(oTestata.dImponibv(i - 1)) Then dttET.Rows(0)("et_imponibv_" & i.ToString) = oTestata.dImponibv(i - 1)
            If NTSCDec(dttET.Rows(0)("et_impostav_" & i.ToString)) <> NTSCDec(oTestata.dImpostav(i - 1)) Then dttET.Rows(0)("et_impostav_" & i.ToString) = oTestata.dImpostav(i - 1)
          Next
        End If
        If BORDINI = False Then
          For i = 1 To 20
            If NTSCDec(dttETC.Rows(0)("tm_ccontr_" & i.ToString)) <> NTSCDec(oTestata.nCcontr(i - 1)) Then dttETC.Rows(0)("tm_ccontr_" & i.ToString) = oTestata.nCcontr(i - 1)
            If NTSCDec(dttETC.Rows(0)("tm_impcont_" & i.ToString)) <> NTSCDec(oTestata.dImpocont(i - 1)) Then dttETC.Rows(0)("tm_impcont_" & i.ToString) = oTestata.dImpocont(i - 1)
            If NTSCDec(dttETC.Rows(0)("tm_impcontv_" & i.ToString)) <> NTSCDec(oTestata.dImpcontv(i - 1)) Then dttETC.Rows(0)("tm_impcontv_" & i.ToString) = oTestata.dImpcontv(i - 1)
          Next
        End If

        bNonValidare = False
      Else
        Return False
      End If

      Return True
    Catch ex As Exception
      bNonValidare = False
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    Finally
      oclPar = Nothing
      oCorpo = Nothing
      bNonValidare = False
    End Try

  End Function

  Public Overridable Function CalcolaTotaliFase2(ByRef dttET As DataTable, ByRef dttETC As DataTable, _
                                                 ByRef oclPar As CLELBMENU.Parametri, _
                                                 ByRef oTestata As CLELBMENU.OutTestata, _
                                                 ByRef oCorpo() As CLELBMENU.OutCorpo) As Boolean
    Dim strError As String = ""
    Dim bResult As Boolean = False

    Try
      '---------------------------------------
      'eseguo il ricalcolo

      bResult = CType(oCleComm, CLELBMENU).CalcolaDoc(strDittaCorrente, oclPar, dttET.Rows(0), _
                                                      dttETC, dttEC, oTestata, oCorpo, strError)
      If strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strError))
      If oTestata.nCcontr(20) = -1 Then Return False

      If bResult Then
        With dttET.Rows(0)
          If ArrDblEcc(oTestata.dTotcoll, 0) > 999999999 Then
            'darebbe errore perchè sul DB il campo non è un float ma un int!!!! 
            !et_totcoll = 0
          Else
            !et_totcoll = ArrDblEcc(oTestata.dTotcoll, 0)
          End If
          !et_peso = oTestata.dPeso
          !et_pesonetto = oTestata.dPesonetto

          !et_impprov = oTestata.dImpprov
          !et_totprov = oTestata.dTotprov
          !et_totprov2 = oTestata.dTotprov2

          !et_speinc = oTestata.dSpeinc
          !et_speincv = oTestata.dSpeincv
          !et_speacc = oTestata.dSpeacc
          !et_speaccv = oTestata.dSpeaccv
          !et_bolli = oTestata.dBolli
          !et_bolliv = oTestata.dBolliv

          !et_totlordo = oTestata.dTotlordo
          !et_totlordov = oTestata.dTotlordov
          !et_totmerce = oTestata.dTotMerce
          !et_totmercev = oTestata.dTotmercev
          !et_totomag = oTestata.dTotomag
          !et_totomagv = oTestata.dTotomagv
          !et_totdoc = oTestata.dTotdoc
          !et_totdocv = oTestata.dTotdocv
          !et_abbuono = oTestata.dAbbuono
          !et_abbuonov = oTestata.dAbbuonov
          !et_pagatov = oTestata.dPagatov
          !et_pagato = oTestata.dPagato
          !et_pagato2 = oTestata.dPagato2
          !et_resto = oTestata.dResto

          !xx_totquant = oTestata.dTotquant
          If BORDINI = False Then
            !et_diffiva = oTestata.dDiffIva
            !et_diffda = oTestata.dDiffDA
          End If
        End With
      End If

      Return bResult
    Catch ex As Exception
      bNonValidare = False
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  '-------------------------------------

  Public Overridable Sub RunQueryBeforePrint(ByVal strChild As String)
    Try
      oCldDocu.RunQueryBeforePrint(strDittaCorrente, strChild)
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

  '-------------------------------------
  Public Overridable Function GetWhereHltd(ByVal lConto As Integer) As String
    Dim bRetailExt As Boolean = False
    Try
      'per zoom dohlTd: zoom importa testate ordini
      If CBool(lModuliSupDittaDitt And bsModSupGPE) Then bRetailExt = True

      Return oCldDocu.GetWhereHltd(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, _
                                   bNonProporreEvasioneIPSuNotePrel, bNonMostrareImpegniPrenotASaldo, _
                                   bDocEmesso, bNoEvasioneOrdSospesi, NTSCInt(dttET.Rows(0)!et_valuta), lConto, _
                                   dttET.Rows(0)!et_scorpo.ToString, bRetailExt)

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
  Public Overridable Function GetWhereHlmo(ByVal lConto As Integer) As String
    Dim bRetailExt As Boolean = False
    Try
      'per zoom dohlMo: zoom importa righe ordini
      If CBool(lModuliSupDittaDitt And bsModSupGPE) Then bRetailExt = True

      Return oCldDocu.GetWhereHlmo(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, dttET.Rows(0)!et_scorpo.ToString, _
                                   lConto, NTSCInt(dttET.Rows(0)!et_valuta), _
                                   bNonProporreEvasioneIPSuNotePrel, bDocEmesso, _
                                   bFiltroMagazRigheOrdini, nFiltroMagazRigheOrdiniEscludiTipobf, _
                                   bNoEvasioneOrdSospesi, NTSCInt(dttET.Rows(0)!et_magaz), _
                                   NTSCInt(dttET.Rows(0)!et_magaz2), _
                                   NTSCInt(dttET.Rows(0)!et_tipobf), bRetailExt)

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
  Public Overridable Function GetWhereHltm(ByVal strTipork As String, ByVal nValuta As Integer, ByVal lConto As Integer, _
                                         ByVal strScorpo As String) As String
    Try
      'per zoom dohlTm: zoom importa note di prelievo
      Return oCldDocu.GetWhereHltm(strDittaCorrente, strTipork, nValuta, lConto, strScorpo)

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

  '-------------------------------------

  Public Overridable Function AlertSottoCosto() As Boolean
    Dim dttAlert As DataTable = Nothing
    Dim strParteMsg As String = ""
    Dim nList As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim dOuPrezzo As Decimal = 0
    Dim dPrezzoNet As Decimal = 0
    Dim dScontvUnit As Decimal = 0
    Try
      If dttET.Rows(0)!et_tipork.ToString <> "R" And _
         dttET.Rows(0)!et_tipork.ToString <> "#" And _
         dttET.Rows(0)!et_tipork.ToString <> "Q" And _
         dttET.Rows(0)!et_tipork.ToString <> "A" And _
         dttET.Rows(0)!et_tipork.ToString <> "B" And _
         dttET.Rows(0)!et_tipork.ToString <> "C" And _
         dttET.Rows(0)!et_tipork.ToString <> "E" And _
         dttET.Rows(0)!et_tipork.ToString <> "W" And _
         dttET.Rows(0)!et_tipork.ToString <> "F" And _
         dttET.Rows(0)!et_tipork.ToString <> "S" Then Return True

      dttAlert = CType(oCleComm, CLELBMENU).CreaDynasetAlert
      'Inizio controllo
      nList = 0
      strParteMsg = oApp.Tr(Me, 128740026518553000, "all'ultimo costo di acquisto")
      Select Case strTestSottoCostoTipo
        Case "U" 'Ultimo costo
          nList = 0
          strParteMsg = oApp.Tr(Me, 128740026508413000, "all'ultimo costo di acquisto")
        Case "M" 'Costo medio
          nList = -2
          strParteMsg = oApp.Tr(Me, 128740026497337000, "al costo medio")
        Case "S" 'Costo standard
          nList = nPeacListinCStd
          strParteMsg = oApp.Tr(Me, 128740026483765000, "al listino costo standard |" & nPeacListinCStd & "| indicato in 'Personalizzazione acquisti'")
        Case Else
          If IsNumeric(strTestSottoCostoTipo) Then
            If CLng(strTestSottoCostoTipo) >= 1 And CLng(strTestSottoCostoTipo) <= 9999 Then
              nList = CInt(strTestSottoCostoTipo)
              strParteMsg = oApp.Tr(Me, 128740026684693000, "al listino |" & nList & "|")
            End If
          End If
      End Select

      dtrT = dttEC.Select("ec_flkit <> 'A' AND ec_flkit <> 'T' " & IIf(strTestSottoCostoZero = "N", "AND ec_prezzo <> 0", "").ToString, "ec_riga")
      For i = 0 To dtrT.Length - 1
        With dtrT(i)
          dOuPrezzo = 0
          dScontvUnit = 0
          If NTSCDec(!ec_quant) <> 0 Then
            dScontvUnit = NTSCDec(!ec_scontv) / NTSCDec(!ec_quant) * NTSCDec(!ec_perqta)
          End If
          'Calcola il prezzo netto
          dPrezzoNet = ArrDbl(NTSCDec(!ec_prezzo) * (100 - NTSCDec(!ec_scont1)) / 100 * (100 - NTSCDec(!ec_scont2)) / 100 * (100 - NTSCDec(!ec_scont3)) / 100 * (100 - NTSCDec(!ec_scont4)) / 100 * (100 - NTSCDec(!ec_scont5)) / 100 * (100 - NTSCDec(!ec_scont6)) / 100 * (100 - NTSCDec(!ec_scontp)) / 100 - dScontvUnit * (100 - NTSCDec(dttET.Rows(0)!et_scont1)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scont2)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scopag)) / 100, oCldDocu.TrovaNdecSuPrzUn(0))

          SettaCondCommerciali_IstanziaNTSCondCommerciali()
          oCondCommerciali.bCalcolaPrezzo = True
          With oCondCommerciali.Input
            .strDitta = strDittaCorrente
            .strCodart = NTSCStr(dtrT(i)!ec_codart)
            .nListino = nList
            .nFase = NTSCInt(dtrT(i)!ec_fase)
            .strTipoval = "P"
            .bConspromo = True
            .nCodpromo = NTSCInt(dtrT(i)!ec_codtpro)
            .dtDatdoc = NTSCDate(dttET.Rows(0)!et_datdoc)
            .nCodvalu = NTSCInt(dttET.Rows(0)!et_valuta)
            .dQuant = NTSCDec(dtrT(i)!ec_quant)
          End With
          '----------
          CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
          '----------
          dOuPrezzo = oCondCommerciali.OutputPrezzo.dPrezzo
          'Se presente una % di ricarico alza il prezzo
          If dTestSottoCostoRicaricoMinimo <> 0 Then
            dOuPrezzo = ArrDbl(dOuPrezzo + ((dOuPrezzo * dTestSottoCostoRicaricoMinimo) / 100), oCldDocu.TrovaNdecSuPrzUn(0))
          End If
          'Esegue il controllo
          If dPrezzoNet < dOuPrezzo Then
            'Non riempio il recordset nel vb-script tramite la opportuna funzione...
            'lo faccio qui perchè molto più facile
            dttAlert.Rows.Add(dttAlert.NewRow)
            dttAlert.Rows(dttAlert.Rows.Count - 1)!codditt = strDittaCorrente
            dttAlert.Rows(dttAlert.Rows.Count - 1)!strMsg = oApp.Tr(Me, 128740033662513000, _
                      "Il prezzo di vendita, al netto degli sconti, dell'articolo '|" & !ec_codart.ToString & _
                      "|' (|" & dPrezzoNet.ToString(oApp.FormatPrzUn) & "|), " & _
                      "associato alla riga |" & !ec_riga.ToString & "| del documento avente tipo '|" & _
                      !ec_tipork.ToString & "|' anno |" & !ec_anno.ToString & "| serie '|" & _
                      !ec_serie.ToString & "|' numero |" & !ec_numdoc.ToString & "|, " & _
                      "è inferiore |" & strParteMsg & "| (|" & dOuPrezzo.ToString(oApp.FormatPrzUn) & _
                      "| ricaricato del |" & dTestSottoCostoRicaricoMinimo.ToString(oApp.FormatSconti) & "|%).")
          End If
        End With
      Next    'For i = 0 To dtrT.Length - 1

      dttAlert.AcceptChanges()
      If BORDINI Then
        CType(oCleComm, CLELBMENU).Verifica_Genera_Alert(2, strDittaCorrente, "BSORGSOR", 1, 0, dttAlert)
      Else
        CType(oCleComm, CLELBMENU).Verifica_Genera_Alert(2, strDittaCorrente, "BSVEBOLL", 1, 0, dttAlert)
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
      If Not dttAlert Is Nothing Then dttAlert.Clear()
    End Try
  End Function

#Region "BeforeColUpdate_TESTA"
  Public Overridable Sub BeforeColUpdate_TESTA(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return
    Dim strErr As String = ""
    Try
      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
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
      Dim strFunction As String = "BeforeColUpdate_TESTA_" & e.Column.ColumnName.ToLower
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

  Public Overridable Function BeforeColUpdate_TESTA_et_datdoc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strThrowEvent As String = ""      'contiene tutti gli eventi che devono essere eseguiti dall'ui
    Dim evnt As NTSEventArgs = Nothing
    Try
      If Not bDisabilitaCheckAnnoData And bInCreaDocDaGnor = False Then
        If NTSCInt(e.Row!et_anno) <> NTSCDate(e.ProposedValue).Year Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222102500000, "La data del documento non è inclusa nell'anno indicato")))
          Return True
        End If
      End If

      strThrowEvent = strThrowEvent & "CambioDtPg:S" & "|"
      If dttEC.Rows.Count > 0 And bInDuplicadoc = False And Not bEliminaMessaggioCambioDatiTest Then
        ThrowRemoteEvent(New NTSEventArgs("", strMsgCambioDatiTestata))
      End If

      'avviso l'UI delle operazioni grafiche che devono essere eseguite a causa della validazione del conto
      If strThrowEvent <> "" Then ThrowRemoteEvent(New NTSEventArgs(strThrowEvent.Substring(0, strThrowEvent.Length - 1), ""))

      If NTSCDate(dttET.Rows(0)!et_datapag) = NTSCDate(dttET.Rows(0)!et_datpar) Or NTSCDate(dttET.Rows(0)!et_datapag) = NTSCDate(dttET.Rows(0)!et_datdoc) Then
        dttET.Rows(0)!et_datapag = CalcolaDataScadenza(NTSCStr(dttET.Rows(0)!et_tipork), NTSCStr(dttET.Rows(0)!et_datpar), NTSCDate(e.ProposedValue).ToShortDateString)
      End If

      If CBool(lModuliSupDittaDitt And bsModSupCAE) And dttEC.Rows.Count > 0 Then
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 129774843980112228, "Modificare la data di inizio competenza economica sulle righe del corpo del documento aventi data inizio uguale alla vecchia data documento?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
          If nDatiniCompMese = -1 Then
            For Each dtrT As DataRow In dttEC.Select("ec_datini = " & CDataSQL(NTSCDate(e.Row!et_datdoc).AddDays((NTSCDate(e.Row!et_datdoc).Day - 1) * -1)))
              If NTSCDate(dtrT!ec_datfin) = NTSCDate(dtrT!ec_datini) Then
                dtrT!ec_datfin = NTSCDate(e.ProposedValue).AddDays((NTSCDate(e.ProposedValue).Day - 1) * -1)  'propongo il primo del mese 
              End If
              dtrT!ec_datini = NTSCDate(e.ProposedValue).AddDays((NTSCDate(e.ProposedValue).Day - 1) * -1) 'propongo il primo del mese 
              dtrT.AcceptChanges()
            Next
          Else
            For Each dtrT As DataRow In dttEC.Select("ec_datini = " & CDataSQL(NTSCDate(e.Row!et_datdoc)))
              If NTSCDate(dtrT!ec_datfin) = NTSCDate(dtrT!ec_datini) Then dtrT!ec_datfin = e.ProposedValue
              dtrT!ec_datini = e.ProposedValue
              dtrT.AcceptChanges()
            Next
          End If
        End If
      End If

      If bModCCC Then
        If bOrdini Then
          e.Row!et_datpromo = e.ProposedValue
        Else
          dttETC.Rows(0)!tm_datpromo = e.ProposedValue
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

  Public Overridable Function BeforeColUpdate_TESTA_et_conto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim bOk As Boolean = False
    Dim strThrowEvent As String = ""
    Dim strNoteOut As String = ""
    Dim dttTmp As New DataTable
    Try
      bOk = BeforeColUpdate_TESTA_et_contoSTD(sender, e)
      If bOk = False Then Return False

      '----------------------------
      If bNew Then
        If Not dttEt_conto Is Nothing Then
          If dttEt_conto.Rows.Count > 0 Then

            If NTSCInt(e.Row!et_coddest) = 0 And bChiediDestInTestata And bInCreaDocDaGnor = False Then
              'chiedera se aprire lo zoom destinazioni diverse (solo se ci sono destinazioni)
              If oCldDocu.IsContoWithDestdiv(strDittaCorrente, NTSCInt(dttEt_conto.Rows(0)!an_conto)) Then
                strThrowEvent = strThrowEvent & "ChiediDest" & "|"
              End If

            End If

            If strVisNoteConto = "S" And bInCreaDocDaGnor = False Then
              '--------------------------------------------
              'visualizzare le note del cliente in uscita dal codice conto
              If dttEt_conto.Rows(0)!an_note2.ToString <> "" Then
                strThrowEvent = strThrowEvent & "VNoteConto:" & _
                  dttEt_conto.Rows(0)!an_note2.ToString.Replace("|"c, "") & "|"
              End If
              'la parte sotto non serve: verrà scatenata al cambio della destinazione diversa
              'If NTSCint(e.Row!et_coddest) <> 0 Then
              '  If dttTmp1.Rows(0)!dd_note.ToString <> "" Then strThrowEvent = strThrowEvent & "VNoteContD:" & dttTmp1.Rows(0)!dd_note.ToString & "|"
              'End If
            End If

            If bRiportaNote1CF Then e.Row!et_note = dttEt_conto.Rows(0)!an_note.ToString
            If bRiportaNote2CF Then e.Row!et_note = dttEt_conto.Rows(0)!an_note2.ToString

            '--------------------------------------------------------------------------------------------------------
            If (BORDINI = True) And (BGESTORDLIST = False) And (bGestAnaext = True) Then
              If (bRiportaNote1CF = False And bRiportaNote2CF = False And nRiportaNoteDaAnaext > 0) Then
                If oCldDocu.RitornaNoteEstensioni(strDittaCorrente, NTSCInt(dttEt_conto.Rows(0)!an_conto), 0, 0, _
                   dttEt_conto.Rows(0)!an_tipo.ToString, nRiportaNoteDaAnaext, strNoteOut) Then
                  e.Row!et_note = strNoteOut
                End If
              End If
            End If
            '--------------------------------------------------------------------------------------------------------

            'test se cliente bloccato
            If ((BORDINI And strContrFidoInsolinInsOrd = "S") Or (BORDINI = False And strContrFidoInsolinInsDoc = "S")) And _
                bNew And bDocEmesso And NTSCInt(e.ProposedValue) <> 0 Then
              If Not (NTSCStr(dttET.Rows(0)!et_tipork) = "Q" And bNoCheckFidoSuPrev) Then
                CheckFidoInsol(False, NTSCInt(e.ProposedValue), dttET, bOk)
                If bOk Then
                  If Not bInCreaDocDaGnor Then
                    If strProgChiamante = "BEECIMPO" Then
                      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130657924106637309, _
                                                        "L'ordine verrà importato anche se il cliente risulta bloccato a causa del superamento del Fido.")))
                    ElseIf (strProgChiamante = "BNORGNNP" AndAlso bGnnpCheckFidoInsolu_NoBlocco) OrElse _
                           (strProgChiamante = "BNVEGNBF" AndAlso bGnbfCheckFidoInsolu_NoBlocco) Then
                      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130736688458132925, _
                                                        "L'ordine verrà elaborato anche se il cliente risulta bloccato a causa del superamento del Fido.")))
                    Else
                      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128521107436726000, _
                                        "Blocco cliente : Inserimento possibile ma conferma dell' impegno/documento impossibile, se " & _
                                        " non verrà sbloccato nel frattempo !! ")))
                      bEraBloccato = True
                    End If
                  End If
                End If
              End If
            End If    ' If strContrFidoInsolinInsOrd = "S" And bNew And bDocEmesso Then

            'Riporta lo stato dell'eventuale blocco
            If bRilevaBloccoDaAnagra Then
              oCldDocu.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp)
              dttET.Rows(0)!et_blocco = dttTmp.Rows(0)!an_blocco.ToString
              dttTmp.Clear()
            End If

          End If    'If dttEt_conto.rows.count > 0 Then
        End If    'If Not dttEt_conto Is Nothing Then
      End If    ' If bNew Then

      '-----------------------------------------------
      'avviso l'UI delle operazioni grafiche che devono essere eseguite a causa della validazione del conto
      If strThrowEvent <> "" Then ThrowRemoteEvent(New NTSEventArgs(strThrowEvent.Substring(0, strThrowEvent.Length - 1), ""))

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

  Public Overridable Function BeforeColUpdate_TESTA_et_contoSTD(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp1 As DataTable = New DataTable()
    Dim bOk As Boolean = False
    Dim evnt As NTSEventArgs

    Try
      'valido il conto, indicando che prevalentemente dovrebbe essere un fornitore
      bOk = ValCodice(e, "ANAGRA", "xx_conto", oApp.Tr(Me, 127792254722500000, "Codice cliente/fornitore |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"), "N", dttEt_conto)
      If bOk = False Then Return False

      If Not dttEt_conto Is Nothing Then

        If dttEt_conto.Rows.Count > 0 Then
          '--------------------------------
          'no sottoconti
          If dttEt_conto.Rows(0)!an_tipo.ToString = "S" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101562500, "Il codice del Cliente o Fornitore |" & NTSCInt(e.ProposedValue).ToString & "| non è valido in quanto appartiene ai sottoconti.")))
            e.ProposedValue = e.Row!et_conto
            Return False
          End If

          '--------------------------------
          'Controllo Cliente privato
          If lConclpriv = NTSCInt(e.ProposedValue) And lConclpriv <> 0 Then
            If BORDINI Then
              'è possibile solo aprire ordini emessi a cliente privato (per poter modificare ordini importati da NON SOLO SHOP)
              If bNew Then
                e.ProposedValue = e.Row!et_conto
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128661964843750000, "Impossibile emettere ordini a clienti generici privati.")))
                Return False
              End If
            Else
              If e.Row!et_tipork.ToString <> "C" And _
                 e.Row!et_tipork.ToString <> "F" And _
                 e.Row!et_tipork.ToString <> "I" And _
                 e.Row!et_tipork.ToString <> "W" Then
                e.ProposedValue = e.Row!et_conto
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128661966561718750, "Attenzione!" & vbCrLf & _
                  "Il codice del conto privato generico '|" & lConclpriv & "|'" & vbCrLf & _
                  "è utilizzabile solo su documenti di tipo:" & vbCrLf & _
                  " . 'Corrispettivo'" & vbCrLf & _
                  " . 'Ricevuta Fiscale Emessa'" & vbCrLf & _
                  " . 'Riemissione Ricevuta Fiscale'" & vbCrLf & _
                  " . 'Nota di Prelievo'")))
                Return False
              End If
            End If    'If BORDINI Then
          End If    'If lConclpriv = NTSCInt(e.ProposedValue) Then

          '--------------------------------
          'test CRM
          If bModuloCRM And bIsCRMUser Then
            lLead = oCldDocu.CercaLeadDaConto(strDittaCorrente, dttEt_conto.Rows(0)!an_tipo.ToString, _
                                              NTSCInt(dttEt_conto.Rows(0)!an_conto), 0, lModuliDittaDitt, _
                                              lModuliExtDittaDitt, lModuliSupDittaDitt)
          End If

          'non do il messaggio per imp di trasferim, diversamente da beorgnnp darebbe sempre il messaggio
          If bNoMsgCongruenzaConto = False Then
            If dttET.Rows(0)!et_tipork.ToString <> "X" Then
              If bDocEmesso And dttEt_conto.Rows(0)!an_tipo.ToString <> "C" And bInNuovoDocSilent = False Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101718750, "ATTENZIONE: si sta emettendo un documento emesso ad un fornitore")))
              ElseIf bDocEmesso = False And dttEt_conto.Rows(0)!an_tipo.ToString <> "F" And bInNuovoDocSilent = False Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101875000, "ATTENZIONE: si sta emettendo un documento ricevuto ad un cliente")))
              End If
            End If
          End If

          '--------------------------------
          'memorizzo il conto cliente per la successiva decodifica della destinazione diversa
          e.Row!xx_tipo = dttEt_conto.Rows(0)!an_tipo.ToString
          If bModuloCRM And bIsCRMUser And NTSCInt(e.ProposedValue) > 0 Then
            If Not CheckCrm(False) Then
              e.ProposedValue = 0
              Return False
            End If
          End If

          lContoCF = NTSCInt(e.ProposedValue)
          e.Row!xx_conto = dttEt_conto.Rows(0)!an_descr1.ToString & " " & _
                          dttEt_conto.Rows(0)!an_descr2.ToString & vbCrLf & _
                          dttEt_conto.Rows(0)!an_indir.ToString & vbCrLf & _
                          dttEt_conto.Rows(0)!an_cap.ToString & " " & _
                          dttEt_conto.Rows(0)!an_citta.ToString & " (" & _
                          dttEt_conto.Rows(0)!an_prov.ToString & ")   (" & _
                          dttEt_conto.Rows(0)!an_stato.ToString & ")"

          '--------------------------------
          'cliente bloccato
          If BORDINI And dttEt_conto.Rows(0)!an_blocco.ToString = "B" Then
            If bConsentiCreazOrdiniCliFornBloccoFisso Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128588725539531250, "Attenzione! Cliente con blocco fisso indicato in anagrafica. Si procede comunque.")))
            Else
              e.ProposedValue = 0
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222102031250, "Attenzione! Cliente con blocco fisso: l'inserimento del documento è consentito solo se il cliente verrà sbloccato nell'anagrafica.")))
              e.Row!xx_conto = ""
              Return True
            End If
          End If
          If BORDINI = False And dttEt_conto.Rows(0)!an_blocco.ToString = "B" Then
            If bConsentiCreazDocumCliFornBloccoFisso Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129434491925351562, "Attenzione! Cliente con blocco fisso indicato in anagrafica. Si procede comunque.")))
            Else
              e.ProposedValue = 0
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129434491939755859, "Attenzione! Cliente con blocco fisso: l'inserimento del documento è consentito solo se il cliente verrà sbloccato nell'anagrafica.")))
              e.Row!xx_conto = ""
              Return True
            End If
          End If

          If Not CheckPIvaCFisCliente(dttEt_conto) Then
            e.ProposedValue = e.Row!et_conto
            Return False
          End If

          '--------------------------------
          'Test su altri blocchi
          If bSegnalaCreazDocumCliFornBloccati Then
            If dttEt_conto.Rows(0)!an_blocco.ToString <> "B" And dttEt_conto.Rows(0)!an_blocco.ToString <> "N" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128661969717812500, _
              "Attenzione! Cliente con blocco |" & _
              DescrBlocco(dttEt_conto.Rows(0)!an_blocco.ToString) & "| indicato in anagrafica. Si procede comunque.")))
            End If
          End If

          'Se impostato il filtro sul cod. agente lo applico
          If nTrattaSoloCliFornDellAgente > 0 Then
            If NTSCInt(dttEt_conto.Rows(0)!an_agente) <> nTrattaSoloCliFornDellAgente Then
              e.ProposedValue = 0
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128661959523125000, "Il codice del Cliente o Fornitore non è ammesso per l'agente |" & nTrattaSoloCliFornDellAgente & "|.")))
              e.Row!xx_conto = ""
              Return True
            End If
          End If

          lAgControp = NTSCInt(dttEt_conto.Rows(0)!an_agcontrop)

          If dttEC.Rows.Count > 0 And bInDuplicadoc = False And Not bEliminaMessaggioCambioDatiTest Then ThrowRemoteEvent(New NTSEventArgs("", strMsgCambioDatiTestata))

          bEndEditET = False

          '--------------------------------
          'azzero comunque la destinaz diversa
          e.Row!et_coddest = 0
          e.Row!et_coddest2 = 0

          '--------------------------------
          'solo sui nuovi documenti
          If bNew Then
            nClscan = NTSCInt(dttEt_conto.Rows(0)!an_clascon)
            nClpran = NTSCInt(dttEt_conto.Rows(0)!an_claprov)
            nTabling = NTSCInt(dttEt_conto.Rows(0)!an_codling)

            e.Row!et_listino = CType(oCleComm, CLELBMENU).RitornaListinoDaAnagraDestdiv(strDittaCorrente, NTSCInt(e.ProposedValue), NTSCInt(e.Row!et_coddest))
            If NTSCInt(dttEt_conto.Rows(0)!an_listino) = 0 Then
              '---------------------------
              'memorizzo il listino da utilizzare per gli scarichi: lo rifaccio perchè se 
              'se il forntiore usa il listino 0 sui nuovi dicumenti non viene scatenata la BeforecolUpdate_et_listino
              'e non verrebbe settato correttamente 'nListinoScarichi'
              Select Case strTipoListinoMat
                Case " "
                  'Listino del carico:    lo farò tra poche righe, se c'è il record di testata
                  nListinoScarichi = NTSCInt(dttET.Rows(0)!et_listino)
                Case "1"
                  'Listino costo standard
                  nListinoScarichi = nPeacListinCStd
                Case "2"
                  'Ultimo costo di acquisto
                  nListinoScarichi = 0
                Case "3"
                  'Costo emdio non implementato
                  'MsgBox "Valorizzazione materiali scaricati a costo medio non impelentata. Verra utilizzato il listino indicato nella testata del carico di produzione.", vbExclamation, bsTtlMsg
                  nListinoScarichi = -2 'Val(edListino.Text)
                Case "4"
                  'Ultimo costo comprensivo di oneri accessori
                  nListinoScarichi = -1
              End Select
            End If

            bInitializing = True
            e.Row!et_valuta = dttEt_conto.Rows(0)!an_valuta
            bInitializing = False

            If bNoAddebitoSpeincSuNoteCredito And e.Row!et_tipork.ToString = "N" Then
              e.Row!et_flspinc = "N"
            Else
              e.Row!et_flspinc = dttEt_conto.Rows(0)!an_spinc
            End If

            'e.Row!et_flspinc = dttEt_conto.Rows(0)!an_spinc
            e.Row!et_flboll = dttEt_conto.Rows(0)!an_bolli
            e.Row!et_codese = dttEt_conto.Rows(0)!an_codese
            e.Row!et_porto = dttEt_conto.Rows(0)!an_porto

            e.Row!et_abi = dttEt_conto.Rows(0)!an_abi
            e.Row!et_cab = dttEt_conto.Rows(0)!an_cab
            e.Row!et_banc1 = dttEt_conto.Rows(0)!an_banc1
            e.Row!et_banc2 = dttEt_conto.Rows(0)!an_banc2
            e.Row!et_codbanc = dttEt_conto.Rows(0)!an_codbanc

            'Segnala un messaggio se la dich. d'intenti  scaduta
            If NTSCInt(dttEt_conto.Rows(0)!an_codese) <> 0 And _
                dttEt_conto.Rows(0)!an_numdic.ToString <> "" And dttEt_conto.Rows(0)!an_scaddic.ToString <> "" Then

              dttTmp1.Clear()
              oCldDocu.ValCodiceDb(dttEt_conto.Rows(0)!an_codese.ToString, strDittaCorrente, "CIVA", "N", "", dttTmp1)
              If dttTmp1.Rows.Count > 0 Then
                If NTSCDec(dttTmp1.Rows(0)!tb_aliq) = 0 And _
                  NTSCDate(dttEt_conto.Rows(0)!an_scaddic) < NTSCDate(dttET.Rows(0)!et_datdoc) Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222102343750, "Attenzione! Sul cliente è ancora presente un codice " & _
                                "di esenzione (|" & dttEt_conto.Rows(0)!an_codese.ToString & "|), mentre la data " & _
                                "scadenza della dichiarazione intenti (|" & dttEt_conto.Rows(0)!an_scaddic.ToString & _
                                "|) è antecedente alla data del nuovo documento che si stà creando.")))
                End If
              End If    'If dttTmp1.Rows.Count > 0 Then
              dttTmp1.Clear()
            End If    'If NTSCint(dttEt_conto.Rows(0)!an_codese) <> 0 And _

            If NTSCInt(dttEt_conto.Rows(0)!an_codtpbf) <> 0 Then e.Row!et_tipobf = NTSCInt(dttEt_conto.Rows(0)!an_codtpbf)

            e.Row!et_codpaga = 0
            e.Row!et_codpaga = CType(oCleComm, CLELBMENU).LeggiCodpagaCltipb(strDittaCorrente, NTSCInt(e.ProposedValue), NTSCInt(e.Row!et_tipobf))
            If NTSCInt(e.Row!et_codpaga) = 0 Then e.Row!et_codpaga = dttEt_conto.Rows(0)!an_codpag.ToString

            e.Row!et_codagen = dttEt_conto.Rows(0)!an_agente
            e.Row!et_codagen2 = dttEt_conto.Rows(0)!an_agente2

            e.Row!et_vettor = dttEt_conto.Rows(0)!an_vett
            e.Row!et_vettor2 = dttEt_conto.Rows(0)!an_vett2

            If NTSCStr(dttEt_conto.Rows(0)!an_acuradi) <> "X" Then e.Row!et_acuradi = NTSCStr(dttEt_conto.Rows(0)!an_acuradi)

            'dopo aver impostato l'agente, altrimenti non viene preso l'agente che c'è nella destinaz diversa ...
            e.Row!et_coddest = NTSCInt(dttEt_conto.Rows(0)!an_destin)

            If nTipoVettore0Coddest = 0 Then
              'era un bug, mantenuto per chi usa business da anni:
              'anche se è stato impostato un vettore su destiv, all'inserimento del conto veniva preso sempre il vettore di anagra
              'poi se si cambiava la destinazione veniva preso il vettore da destdiv, anche se con cod = 0!!!
              e.Row!et_vettor = dttEt_conto.Rows(0)!an_vett
              e.Row!et_vettor2 = dttEt_conto.Rows(0)!an_vett2
            End If
          Else
            '--------------------------------------------------------------------------------------------------------
            If bRilevaCodPagaCambioContoDocModif = True Then
              e.Row!et_codpaga = 0
              e.Row!et_codpaga = CType(oCleComm, CLELBMENU).LeggiCodpagaCltipb(strDittaCorrente, NTSCInt(e.ProposedValue), NTSCInt(e.Row!et_tipobf))
              If NTSCInt(e.Row!et_codpaga) = 0 Then e.Row!et_codpaga = dttEt_conto.Rows(0)!an_codpag.ToString
            End If
            '--------------------------------------------------------------------------------------------------------
            If bInDuplicadoc = True And bInCreaDocDaGnor = True Then
              e.Row!et_abi = dttEt_conto.Rows(0)!an_abi
              e.Row!et_cab = dttEt_conto.Rows(0)!an_cab
              e.Row!et_banc1 = dttEt_conto.Rows(0)!an_banc1
              e.Row!et_banc2 = dttEt_conto.Rows(0)!an_banc2
              e.Row!et_codbanc = dttEt_conto.Rows(0)!an_codbanc
            Else
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128661998924218750, "Inserire sul documento corrente l'ABI/CAB del cliente/fornitore selezionato?"))
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
                e.Row!et_abi = dttEt_conto.Rows(0)!an_abi
                e.Row!et_cab = dttEt_conto.Rows(0)!an_cab
                e.Row!et_banc1 = dttEt_conto.Rows(0)!an_banc1
                e.Row!et_banc2 = dttEt_conto.Rows(0)!an_banc2
                e.Row!et_codbanc = dttEt_conto.Rows(0)!an_codbanc
              End If
            End If
          End If    'If bNew Then

          e.Row!et_contfatt = NTSCInt(dttEt_conto.Rows(0)!an_contfatt)
          bEndEditET = True
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
    Finally
      bEndEditET = True
      bInitializing = False
    End Try
  End Function

  Public Overridable Function BeforeColUpdate_TESTA_et_coddest(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Dim dttTmp As DataTable = New DataTable

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_coddest = ""
        'faccio dare il messaggio solo se cambio la destiv: se la destdiv viene cambiata xchè cambio il cli non do il messaggio
        If bEndEditEC And dttEC.Rows.Count > 0 And bInDuplicadoc = False And Not bEliminaMessaggioCambioDatiTest Then ThrowRemoteEvent(New NTSEventArgs("", strMsgCambioDatiTestata))

        e.Row!et_porto = dttEt_conto.Rows(0)!an_porto
        If NTSCStr(dttEt_conto.Rows(0)!an_acuradi) <> "X" Then e.Row!et_acuradi = NTSCStr(dttEt_conto.Rows(0)!an_acuradi)
        If nTipoVettore0Coddest = 0 Then
          'vecchio sistema, concettualmente sbagliato
        Else
          e.Row!et_vettor = dttEt_conto.Rows(0)!an_vett
          e.Row!et_vettor2 = dttEt_conto.Rows(0)!an_vett2
        End If
      Else
        bOk = oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "DESTDIV", "N", strTmp, dttTmp, lContoCF.ToString)
        If dttTmp.Rows.Count = 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222103750000, "Codice destinazione diversa inesistente (oppure codice cliente/fornitore non impostato)")))
          Return False
        Else
          '----------------------------------------------------------------------------------------------------------
          With dttTmp.Rows(0)
            strTmp = NTSCStr(!dd_nomdest).Trim & _
              IIf(NTSCStr(!dd_inddest).Trim <> "", " - " & NTSCStr(!dd_inddest).Trim, "").ToString & _
              IIf(NTSCStr(!dd_locdest).Trim <> "", " - " & NTSCStr(!dd_locdest).Trim, "").ToString
          End With
          '----------------------------------------------------------------------------------------------------------
          e.Row!xx_coddest = strTmp
          'faccio dare il messaggio solo se cambio la destiv: se la destdiv viene cambiata xchè cambio il cli non do il messaggio
          If bEndEditEC And dttEC.Rows.Count > 0 And bInDuplicadoc = False And Not bEliminaMessaggioCambioDatiTest Then ThrowRemoteEvent(New NTSEventArgs("", strMsgCambioDatiTestata))

          '----------------------------------------------
          'propongo agenti e vettori della destinazione diversa
          If bNew Or (bNew = False And bBloccaAgente1 = False) Then
            'L'agente 1 non lo cambia sui doc aperti se abilitata opz. BloccaAgente1
            If NTSCInt(dttTmp.Rows(0)!dd_agente) <> 0 And NTSCInt(e.Row!et_codagen) <> NTSCInt(dttTmp.Rows(0)!dd_agente) Then e.Row!et_codagen = dttTmp.Rows(0)!dd_agente
          End If
          If NTSCInt(dttTmp.Rows(0)!dd_agente2) <> 0 And NTSCInt(e.Row!et_codagen2) <> NTSCInt(dttTmp.Rows(0)!dd_agente2) Then e.Row!et_codagen2 = dttTmp.Rows(0)!dd_agente2

          If NTSCStr(dttTmp.Rows(0)!dd_acuradi) <> "X" Then
            e.Row!et_acuradi = NTSCStr(dttTmp.Rows(0)!dd_acuradi)
          Else
            If NTSCStr(dttEt_conto.Rows(0)!an_acuradi) <> "X" Then e.Row!et_acuradi = NTSCStr(dttEt_conto.Rows(0)!an_acuradi)
          End If

          If nTipoVettore0Coddest = -1 Then
            'prendo su il vettore da destdivv anche se vettore = 0
            e.Row!et_vettor = dttTmp.Rows(0)!dd_vett
            e.Row!et_vettor2 = dttTmp.Rows(0)!dd_vett2
          ElseIf nTipoVettore0Coddest = 1 Then
            'prendo su dadestdiv solo se vettore <> 0, else quello di angra
            If NTSCInt(dttTmp.Rows(0)!dd_vett) <> 0 Then
              e.Row!et_vettor = dttTmp.Rows(0)!dd_vett
            Else
              e.Row!et_vettor = dttEt_conto.Rows(0)!an_vett
            End If
            If NTSCInt(dttTmp.Rows(0)!dd_vett2) <> 0 Then
              e.Row!et_vettor2 = dttTmp.Rows(0)!dd_vett2
            Else
              e.Row!et_vettor2 = dttEt_conto.Rows(0)!an_vett2
            End If

            If NTSCStr(dttTmp.Rows(0)!dd_porto) <> "" Then
              e.Row!et_porto = dttTmp.Rows(0)!dd_porto
            Else
              e.Row!et_porto = dttEt_conto.Rows(0)!an_porto
            End If

          Else
            'vecchio sistema
            If NTSCInt(dttTmp.Rows(0)!dd_vett) <> 0 And NTSCInt(e.Row!et_vettor) <> NTSCInt(dttTmp.Rows(0)!dd_vett) Then e.Row!et_vettor = dttTmp.Rows(0)!dd_vett
            If NTSCInt(dttTmp.Rows(0)!dd_vett2) <> 0 And NTSCInt(e.Row!et_vettor2) <> NTSCInt(dttTmp.Rows(0)!dd_vett2) Then e.Row!et_vettor2 = dttTmp.Rows(0)!dd_vett2
          End If
        End If

        '--------------------------
        'visualizzo le eventuali note del conto
        If NTSCInt(e.ProposedValue) <> 0 And lContoCF <> 0 And strVisNoteConto = "S" And bNew And bInCreaDocDaGnor = False Then
          If oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "DESTDIV", "N", "", dttTmp, lContoCF.ToString) Then
            If dttTmp.Rows.Count > 0 Then
              If NTSCStr(dttTmp.Rows(0)!dd_note).Trim <> "" Then
                ThrowRemoteEvent(New NTSEventArgs("VNoteContD:" & dttTmp.Rows(0)!dd_note.ToString, ""))
              End If
            End If
          End If
        End If    'If NTSCInt(e.ProposedValue) <> 0 And lContoCF <> 0 And strVisNoteConto = "S" And bNew And bInCreaDocDaGnor = False Then
      End If

      '--------------------------------
      'solo sui nuovi documenti
      If bNew Then
        e.Row!et_listino = CType(oCleComm, CLELBMENU).RitornaListinoDaAnagraDestdiv(strDittaCorrente, NTSCInt(dttEt_conto.Rows(0)!an_conto), NTSCInt(e.ProposedValue))
        If NTSCInt(dttEt_conto.Rows(0)!an_listino) = 0 Then
          '---------------------------
          'memorizzo il listino da utilizzare per gli scarichi: lo rifaccio perchè se 
          'se il forntiore usa il listino 0 sui nuovi dicumenti non viene scatenata la BeforecolUpdate_et_listino
          'e non verrebbe settato correttamente 'nListinoScarichi'
          Select Case strTipoListinoMat
            Case " "
              'Listino del carico:    lo farò tra poche righe, se c'è il record di testata
              nListinoScarichi = NTSCInt(dttET.Rows(0)!et_listino)
            Case "1"
              'Listino costo standard
              nListinoScarichi = nPeacListinCStd
            Case "2"
              'Ultimo costo di acquisto
              nListinoScarichi = 0
            Case "3"
              'Costo emdio non implementato
              'MsgBox "Valorizzazione materiali scaricati a costo medio non impelentata. Verra utilizzato il listino indicato nella testata del carico di produzione.", vbExclamation, bsTtlMsg
              nListinoScarichi = -2 'Val(edListino.Text)
            Case "4"
              'Ultimo costo comprensivo di oneri accessori
              nListinoScarichi = -1
          End Select
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

  Public Overridable Function BeforeColUpdate_TESTA_et_coddest2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim bOk As Boolean = False
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_coddest2 = ""
      Else
        bOk = oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "DESTDIV", "N", strTmp, dttTmp, lContoCF.ToString)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222104062500, "Codice destinazione diversa 2 inesistente (oppure codice cliente/fornitore non impostato)")))
          Return False
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

  Public Overridable Function BeforeColUpdate_TESTA_et_tipobf(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As DataTable = New DataTable()
    Dim nNewCodpaga As Integer
    Dim f2Mag As Boolean = False
    Dim bForza As Boolean = False


    Try
      ValCodice(e, "TPBF", "xx_tipobf", oApp.Tr(Me, 127792254788750000, "Tipo bolla/fattura |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"), "N", dttTmp)

      If dttEC.Rows.Count > 0 And bInDuplicadoc = False And Not bEliminaMessaggioCambioDatiTest Then
        ThrowRemoteEvent(New NTSEventArgs("", strMsgCambioDatiTestata))
      End If

      '--------------------------------
      If Not Accconf_CheckBeforecolupdate("BF", NTSCInt(e.ProposedValue)) Then
        e.ProposedValue = 0
        Return False
      End If

      If bNew = False And bInDuplicadoc = False And BORDINI And BGESTORDLIST = False Then
        'se sono in modifica MA tutte le righe non sono state evase, posso ancora cambiare magazzini e causali come se il documento fosse nuovo
        If dttEC.Select("ec_quaeva <> 0").Length = 0 Then
          If bOrdOpenCambioTpbfChiediCambioMagaz Then
            bForza = True
          End If
        End If
      End If

      '-----------------------------------
      'entro qui sotto solo se il tipo bolla/fattura esiste
      If bNew Or bInDuplicadoc Or bForza Then     'non devo cambiare se ho aperto il documento ed ho cambiato il tipobf ..
        If Not dttTmp Is Nothing Then
          If dttTmp.Rows.Count > 0 Then

            e.Row!et_codcena = dttTmp.Rows(0)!tb_vcodcen

            If BGESTORDLIST = False Then e.Row!et_coddivi = dttTmp.Rows(0)!tb_coddivi

            If e.Row!et_tipork.ToString = "H" Or e.Row!et_tipork.ToString = "T" Then e.Row!et_magimp = dttTmp.Rows(0)!tb_tmagimp

            If dttTmp.Rows(0)!tb_new506.ToString.ToUpper = "S" Then
              '----------------------------------------------------
              'Deroga alle impostazioni originarie di PEVE o PEAC
              e.Row!et_controp = dttTmp.Rows(0)!tb_tcontro
              e.Row!et_magaz = dttTmp.Rows(0)!tb_tmagazz
              e.Row!et_caustra = dttTmp.Rows(0)!tb_tcautra
              If BORDINI = False Then
                e.Row!et_causale = dttTmp.Rows(0)!tb_tcaumag
                MagazSecondario(e.Row, f2Mag)
              End If
              If dttEC.Rows.Count = 0 Or bInDuplicadoc Then e.Row!et_scorpo = dttTmp.Rows(0)!tb_tscorpo 'cambio il flag 'scorporo solo se non ho righe nel corpo (o se sto duplicando un doc)
              If (f2Mag And e.Row!et_tipork.ToString <> "T") Or (BORDINI And e.Row!et_tipork.ToString = "X") Then
                e.Row!et_magaz2 = dttTmp.Rows(0)!tb_tmagazz2
              Else
                e.Row!et_magaz2 = 0
              End If
              If BORDINI = False Then
                If e.Row!et_tipork.ToString = "T" Then
                  e.Row!et_causcap = NTSCInt(dttTmp.Rows(0)!tb_tcauscap)
                Else
                  e.Row!et_causcap = 0
                  e.Row!et_magimp = 0
                End If
              End If
            Else
              '----------------------------------------------------
              'Riporta le impostazioni originarie di PEVE o PEAC
              e.Row!et_controp = IIf(bDocEmesso, lPeveContro, lPeacContro)
              e.Row!et_magaz = IIf(bDocEmesso, nPeveMagazz, nPeacMagazz)
              If BORDINI = False Then
                e.Row!et_causale = IIf(bDocEmesso, nPeveCaumag, nPeacCaumag)
                MagazSecondario(e.Row, f2Mag)
              End If
              If dttEC.Rows.Count = 0 Or bInDuplicadoc Then e.Row!et_scorpo = strPeveScorpo 'cambio il flag 'scorporo solo se non ho righe nel corpo
              e.Row!et_caustra = IIf(bDocEmesso, nPeveCautra, 0)
              If BORDINI = False Then
                If e.Row!et_tipork.ToString = "T" Then
                  e.Row!et_causcap = nPeacScapro
                Else
                  e.Row!et_causcap = 0
                  e.Row!et_magimp = 0
                End If
              End If
            End If    'If dttTmp.Rows(0)!tb_new506.ToString = "S" Then

            'Mette il flag su proforma
            If BORDINI = False Then
              e.Row!et_proforma = dttTmp.Rows(0)!tb_tprofor.ToString
              If e.Row!et_tipork.ToString = "B" Then
                e.Row!xx_przbol = dttTmp.Rows(0)!tb_przbol.ToString
              Else
                e.Row!xx_przbol = "N"
              End If
            End If

            '----------------------------------------------------
            'Legge Clitpbf
            If lContoCF <> 0 Then
              nNewCodpaga = CType(oCleComm, CLELBMENU).LeggiCodpagaCltipb(strDittaCorrente, NTSCInt(lContoCF), NTSCInt(e.ProposedValue))
              If nNewCodpaga <> 0 Then e.Row!et_codpaga = nNewCodpaga
            End If
          End If    'If dttTmp.Rows.Count > 0 Then
        End If    'If Not dttTmp Is Nothing Then
      End If    'If bNew Then

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

  Public Overridable Function BeforeColUpdate_TESTA_et_codpaga(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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
          e.Row!et_datapag = CalcolaDataScadenza(NTSCStr(e.Row!et_tipork), NTSCStr(e.Row!et_datpar), NTSCDate(e.Row!et_datdoc).ToShortDateString)
          strThrowEvent = strThrowEvent & "edDtPrimoP:N" & "|"
        End If

        'Ricerca lo sconto pagamento
        e.Row!et_scopag = NTSCDec(dttTmp.Rows(0)!tb_scopaga)
        If e.Row!et_flspinc.ToString = "S" Then
          e.Row!et_speinc = NTSCDec(dttTmp.Rows(0)!tb_speinca)
          If NTSCInt(e.Row!et_valuta) <> 0 Then
            e.Row!et_speincv = oCldDocu.ConvImpEur(False, NTSCDec(dttTmp.Rows(0)!tb_speinca), _
                                                    NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), _
                                                    NTSCDec(dttET.Rows(0)!et_cambio))
          End If
        Else
          e.Row!et_speinc = 0
          e.Row!et_speincv = 0
        End If
      Else
        strThrowEvent = strThrowEvent & "edDtPrimoP:N" & "|"
        e.Row!et_scopag = 0
        e.Row!et_speinc = 0
        e.Row!et_speincv = 0
      End If

      '-----------------------------------------------
      'avviso l'UI delle operazioni grafiche che devono essere eseguite a causa della validazione del conto
      If strThrowEvent <> "" Then ThrowRemoteEvent(New NTSEventArgs(strThrowEvent.Substring(0, strThrowEvent.Length - 1), ""))

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

  Public Overridable Function BeforeColUpdate_TESTA_et_codagen(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ValCodice(e, "CAGE", "xx_codagen", oApp.Tr(Me, 127792254860937500, "Codice agente |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"))
      If dttEC.Rows.Count > 0 Then
        If NTSCInt(e.ProposedValue) = 0 Then
          For Each dtrEc As DataRow In dttEC.Rows
            dtrEc!ec_provv = 0
          Next
        Else
          If bInDuplicadoc = False And Not bEliminaMessaggioCambioDatiTest Then ThrowRemoteEvent(New NTSEventArgs("", strMsgCambioDatiTestata))
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

  Public Overridable Function BeforeColUpdate_TESTA_et_codagen2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ValCodice(e, "CAGE", "xx_codagen2", oApp.Tr(Me, 127792255233593750, "Codice agente 2 |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"))
      If dttEC.Rows.Count > 0 Then
        If NTSCInt(e.ProposedValue) = 0 Then
          For Each dtrEc As DataRow In dttEC.Rows
            dtrEc!ec_provv2 = 0
          Next
        Else
          If bInDuplicadoc = False And Not bEliminaMessaggioCambioDatiTest Then ThrowRemoteEvent(New NTSEventArgs("", strMsgCambioDatiTestata))
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

  Public Overridable Function BeforeColUpdate_TESTA_et_listino(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      'If NTSCInt(e.ProposedValue) < 0 And NTSCInt(e.ProposedValue) <> -2 Then
      ' e.ProposedValue = 1
      ' ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128521417299944000, "Listino '-1' non valido")))
      'Else
      If dttEC.Rows.Count > 0 And bInDuplicadoc = False And Not bEliminaMessaggioCambioDatiTest Then ThrowRemoteEvent(New NTSEventArgs("", strMsgCambioDatiTestata))
      'End If

      Select Case strTipoListinoMat
        Case " "
          'Listino del carico
          nListinoScarichi = NTSCInt(e.ProposedValue)
        Case "1"
          'Listino costo standard
          nListinoScarichi = nPeacListinCStd
        Case "2"
          'Ultimo costo di acquisto
          nListinoScarichi = 0
        Case "3"
          'Costo emdio non implementato
          'MsgBox "Valorizzazione materiali scaricati a costo medio non impelentata. Verra utilizzato il listino indicato nella testata del carico di produzione.", vbExclamation, bsTtlMsg
          nListinoScarichi = -2 'Val(edListino.Text)
        Case "4"
          'Ultimo costo comprensivo di oneri accessori
          nListinoScarichi = -1
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

  Public Overridable Function BeforeColUpdate_TESTA_et_scont1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      '---------------------------------------
      'ordini di prod. interni: no sconti di testata e sconto pagamento (neanche sconti di riga, ma quelli vnegono azzerati al cambio di riga)
      'spostato nella TestPreSalva con messaggio
      'If (dttET.Rows(0)!et_tipork.ToString = "T" Or dttET.Rows(0)!et_tipork.ToString = "H") And bTerzista = False Then
      '  If NTSCDec(e.ProposedValue) <> 0 Then e.ProposedValue = 0
      'End If

      CambioScontiTest()
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

  Public Overridable Function BeforeColUpdate_TESTA_et_scont2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      '---------------------------------------
      'ordini di prod. interni: no sconti di testata e sconto pagamento (neanche sconti di riga, ma quelli vnegono azzerati al cambio di riga)
      'spostato nella TestPreSalva con messaggio
      'If (dttET.Rows(0)!et_tipork.ToString = "T" Or dttET.Rows(0)!et_tipork.ToString = "H") And bTerzista = False Then
      '  If NTSCDec(e.ProposedValue) <> 0 Then e.ProposedValue = 0
      'End If

      CambioScontiTest()
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

  Public Overridable Function BeforeColUpdate_TESTA_et_scopag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      '---------------------------------------
      'ordini di prod. interni: no sconti di testata e sconto pagamento (neanche sconti di riga, ma quelli vnegono azzerati al cambio di riga)
      'spostato nella TestPreSalva con messaggio
      'If (dttET.Rows(0)!et_tipork.ToString = "T" Or dttET.Rows(0)!et_tipork.ToString = "H") And bTerzista = False Then
      '  If NTSCDec(e.ProposedValue) <> 0 Then e.ProposedValue = 0
      'End If

      CambioScontiTest()
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

  Public Overridable Function BeforeColUpdate_TESTA_et_codese(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim i As Integer = 0
    Try

      If NTSCInt(e.ProposedValue) = 0 And dttEC.Rows.Count > 0 Then
        'rimetto il cod iva standard
        Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128740035682583000, "Modificare il Codice IVA su tutte le righe di questo documento?"))
        If bInDuplicadoc Then
          msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES
        Else
          ThrowRemoteEvent(msg)
        End If
        If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
          For i = 0 To dttEC.Rows.Count - 1
            If GetMemDttArti(NTSCStr(dttEC.Rows(i)!ec_codart)).Rows.Count > 0 Then
              dttEC.Rows(i)!ec_codiva = NTSCInt(dttArti.Rows(0)!ar_codiva)
            Else
              dttEC.Rows(i)!ec_codiva = 0
            End If
          Next
        End If
      End If

      If ValCodice(e, "CIVA", "xx_codese", oApp.Tr(Me, 127792255204062500, "Codice IVA |" & NTSCInt(e.ProposedValue).ToString & "| non corretto")) Then

        If NTSCInt(e.ProposedValue) > 0 And dttEC.Rows.Count > 0 And dttET.Rows(0)!et_tipork.ToString <> "U" Then
          Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 127791955669687500, "Modificare il Codice IVA su tutte le righe di questo documento?"))
          If bInDuplicadoc Then
            msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES
          Else
            ThrowRemoteEvent(msg)
          End If
          If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
            CambioDatiTestataInGriglia(NTSCInt(e.ProposedValue), 0, 0, 0, 0, 0, "", "", "")
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

  Public Overridable Function BeforeColUpdate_TESTA_et_controp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try

      If NTSCInt(e.ProposedValue) = 0 And dttEC.Rows.Count > 0 Then
        'rimetto il cod controp standard
        Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128740035754257000, "Modificare il Codice contropartita vendite/acquisti su tutte le righe di questo documento?"))
        If bInDuplicadoc Then
          msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES
        Else
          ThrowRemoteEvent(msg)
        End If
        If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
          For i = 0 To dttEC.Rows.Count - 1
            If GetMemDttArti(NTSCStr(dttEC.Rows(i)!ec_codart)).Rows.Count > 0 Then
              dttEC.Rows(i)!ec_controp = NTSCInt(IIf(bDocEmesso, dttArti.Rows(0)!ar_controp, dttArti.Rows(0)!ar_controa)) + lAgControp
            Else
              dttEC.Rows(i)!ec_controp = 0
            End If
          Next
        End If
      End If

      If ValCodice(e, "COVE", "xx_controp", oApp.Tr(Me, 128517054671070000, "Codice CONTROPARTITA |" & NTSCInt(e.ProposedValue).ToString & "| non corretto")) Then
        If NTSCInt(e.ProposedValue) > 0 And dttEC.Rows.Count > 0 Then
          Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128740035773445000, "Modificare il Codice contropartita vendite/acquisti su tutte le righe di questo documento?"))
          ThrowRemoteEvent(msg)
          If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
            CambioDatiTestataInGriglia(0, 0, 0, 0, NTSCInt(e.ProposedValue), 0, "", "", "")
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

  Public Overridable Function BeforeColUpdate_TESTA_et_magaz(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If ValCodice(e, "MAGA", "xx_magaz", oApp.Tr(Me, 127792254916250000, "Codice magazzino 1 |" & NTSCInt(e.ProposedValue).ToString & "| non corretto")) Then

        '--------------------------------
        If Not Accconf_CheckBeforecolupdate("MA", NTSCInt(e.ProposedValue)) Then
          e.ProposedValue = 0
          Return False
        End If

        If NTSCInt(e.ProposedValue) > 0 And dttEC.Rows.Count > 0 Then

          If NTSCInt(e.ProposedValue) > 0 And dttEC.Rows.Count > 0 Then
            Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 127791956140312500, "Modificare il Codice Magazzino 1 su tutte le righe di questo documento?"))
            If bInDuplicadoc Then
              msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES
            Else
              ThrowRemoteEvent(msg)
            End If
            If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              CambioDatiTestataInGriglia(0, NTSCInt(e.ProposedValue), 0, 0, 0, 0, "", "", "")
            End If
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

  Public Overridable Function BeforeColUpdate_TESTA_et_magaz2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If ValCodice(e, "MAGA", "xx_magaz2", oApp.Tr(Me, 127792254958125000, "Codice magazzino 2 |" & NTSCInt(e.ProposedValue).ToString & "| non corretto")) Then

        '--------------------------------
        If Not Accconf_CheckBeforecolupdate("M2", NTSCInt(e.ProposedValue)) Then
          e.ProposedValue = 0
          Return False
        End If

        If NTSCInt(e.ProposedValue) > 0 And dttEC.Rows.Count > 0 Then

          If NTSCInt(e.ProposedValue) > 0 And dttEC.Rows.Count > 0 Then
            Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 127791956424687500, "Modificare il Codice Magazzino 2 su tutte le righe di questo documento?"))
            If bInDuplicadoc Then
              msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES
            Else
              ThrowRemoteEvent(msg)
            End If
            If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              CambioDatiTestataInGriglia(0, 0, NTSCInt(e.ProposedValue), 0, 0, 0, "", "", "")
            End If
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

  Public Overridable Function BeforeColUpdate_TESTA_et_magimp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As New DataTable
    Dim dttTabcent As New DataTable
    Dim nNewCodcent As Integer = 0
    Dim nNewCcodcontr As Integer = 0
    Dim nNewCcodcena As Integer = 0
    Dim lNewConcova As Integer = 0
    Dim dtrAtt As DataRow = Nothing       'attività non ancora completate 
    Dim dtrAss() As DataRow = Nothing     'centro di lavoro (può essere al massimo 1) collegato all'attività non completata
    Dim dtrEc() As DataRow = Nothing      'riga H collegata all'attività non completata 
    Dim nAtt As Integer = 0
    Dim lLastRow As Integer = -1
    Dim lCountFasi As Integer = 0
    Dim bModif As Boolean = False
    Dim dtrLavoraz() As DataRow = Nothing

    Dim dPrezvalc As Decimal = 0
    Dim dPrezzo As Decimal = 0
    Dim dValorevRes As Decimal = 0
    Dim dValoreRes As Decimal = 0
    Dim dValore As Decimal = 0
    Dim dValorev As Decimal = 0

    Try
      bTerzista = False
      If ValCodice(e, "MAGA", "xx_magimp", oApp.Tr(Me, 127792255410468750, "Codice magazzino impegni |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"), "N", dttTmp) Then

        '--------------------------------
        If Not Accconf_CheckBeforecolupdate("MS", NTSCInt(e.ProposedValue)) Then
          e.ProposedValue = 0
          Return False
        End If

        bTerzista = CType(oCleComm, CLELBMENU).IsTerzista(strDittaCorrente, NTSCInt(e.ProposedValue))

        If NTSCInt(e.ProposedValue) > 0 And dttEC.Rows.Count > 0 Then

          If NTSCInt(e.ProposedValue) > 0 And dttEC.Rows.Count > 0 And (NTSCStr(dttET.Rows(0)!et_tipork) = "H" Or NTSCStr(dttET.Rows(0)!et_tipork) = "T") Then
            Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 127791956468125000, "Modificare il codice Magazzino impegni su tutte le righe di questo documento?"))
            If bInDuplicadoc Then
              msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES
            Else
              ThrowRemoteEvent(msg)
            End If
            If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              CambioDatiTestataInGriglia(0, 0, 0, NTSCInt(e.ProposedValue), 0, 0, "", "", "")
            End If
          End If

          '----------------------
          'Se il documento è di tipo terzista cambia il centro solo se non abilitato il PM
          If bTerzista And bModPM = False And (NTSCStr(dttET.Rows(0)!et_tipork) = "H" Or NTSCStr(dttET.Rows(0)!et_tipork) = "T") Then
            Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128526520410708000, "Modificare il centro di lavoro indicato nelle lavorazioni di questo documento di produzione ESTERNO, e rilevare nuovamente i costi di lavorazione dai listini?"))
            If bInDuplicadoc Then
              msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES
            Else
              ThrowRemoteEvent(msg)
            End If
            If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then

              'il conto associato al magazzino deve essere uguale al conto intestatario del documento
              If NTSCInt(dttTmp.Rows(0)!tb_magconto) <> NTSCInt(dttET.Rows(0)!et_conto) Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526521920164000, "Il conto indicato in testata documento deve essere uguale al conto associato al Magazzino impegni; impossibile rettificare le righe di lavorazione.")))
                Return False
              End If

              'Si assicura che non ci siano righe di lavorazioni triferite a LAVCENT G
              If BORDINI = False Then
                If dttLAVCENT.Select("lce_gnumdoc > 0").Length > 0 Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128663079803750000, "Impossibile modificare i centri di lavoro in quanto su una o più fasi di lavoro esistono riferimenti ad AVANZAMENTI DI LAVORAZIONE.")))
                  Return False
                End If
              End If

              'Determina il centro di lavoro esterno
              If Not oCldDocu.GetTabcentTabcoveFromMagaz(strDittaCorrente, NTSCInt(e.ProposedValue), dttTabcent) Then Return False
              If dttTabcent.Rows.Count = 0 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526525077292000, "Nessun centro di lavoro esterno associato al magazzino impegni |" & NTSCInt(e.ProposedValue).ToString & "|; impossibile rettificare le righe di lavorazione.")))
                Return False
              End If

              nNewCodcent = NTSCInt(dttTabcent.Rows(0)!tb_codcent)
              nNewCcodcontr = NTSCInt(dttTabcent.Rows(0)!tb_ccodcontr)
              nNewCcodcena = NTSCInt(dttTabcent.Rows(0)!tb_ccodcena)
              lNewConcova = NTSCInt(dttTabcent.Rows(0)!tb_concova)
              dttTabcent.Clear()

              '-------------------------
              'ora devo cambiare il centro di lavoro sulle rihe di assris /lavcent collegate 
              'alle attività ancora non totalmente evase
              lLastRow = -1
              lCountFasi = 0
              bModif = False

              '-------------------------
              'ORDINI DI PRODUZIONE
              'ottengo l'elenco delle lavorazioni non ancora evase totalmente
              If dttET.Rows(0)!et_tipork.ToString = "H" Then

                For Each dtrAtt In dttATTIVIT.Select("at_flevas <> 'S'", "at_riga, at_fase")
                  dtrAss = dttASSRIS.Select("as_riga = " & dtrAtt!at_riga.ToString & " AND as_fase = " & dtrAtt!at_fase.ToString)
                  If dtrAss.Length > 0 Then
                    'se c'è il centro di lavoro ...
                    dtrEc = dttEC.Select("ec_riga = " & dtrAtt!at_riga.ToString & " AND ec_flevas <> 'S'")

                    'Per ogni riga caricata
                    If lLastRow = NTSCInt(dtrAtt!at_riga) Then
                      lCountFasi = lCountFasi + 1
                    Else
                      lCountFasi = 1
                    End If
                    lLastRow = NTSCInt(dtrAtt!at_riga)

                    'Si assicura sia una sola riga (per il terzista non posso avere più di una attività), 
                    'nel caso cancella oltre la prima fase
                    If lCountFasi > 1 Then
                      bModif = True
                      dtrAtt.Delete()
                      dtrAss(0).Delete()
                      GoTo SaltaFase
                    End If

                    'Se il centro della riga è già corretto non fa nulla
                    If nNewCodcent = NTSCInt(dtrAss(0)!as_codcent) Then
                      GoTo SaltaFase
                    End If

                    'Rileva il costo della lavorazione
                    bModif = True
                    dPrezvalc = 0

                    SettaCondCommerciali_IstanziaNTSCondCommerciali()
                    oCondCommerciali.bCalcolaPrezzo = True
                    With oCondCommerciali.Input
                      .strDitta = strDittaCorrente
                      .strCodart = NTSCStr(dtrEc(0)!ec_codart)
                      .nCodlavo = NTSCInt(dtrAtt!at_codlavo)
                      .lConto = NTSCInt(dttET.Rows(0)!et_conto)
                      .lDestdiv = NTSCInt(dttET.Rows(0)!et_coddest)
                      .nListino = nPeacListinCStd
                      .strUnmis = NTSCStr(dtrEc(0)!ec_unmis)
                      .strUmp = NTSCStr(dtrEc(0)!ec_ump)
                      .nFase = NTSCInt(dtrEc(0)!ec_fase)
                      .strTipoval = "P"
                      .bConspromo = True
                      .dtDatdoc = NTSCDate(dttET.Rows(0)!et_datdoc)
                      .nCodvalu = NTSCInt(dttET.Rows(0)!et_valuta)
                      .dColli = NTSCDec(dtrEc(0)!ec_colli)
                      .dQuant = NTSCDec(dtrEc(0)!ec_quant)
                      .bConsideraPrezziUnmis = True
                      .bPrezziPerUnmis = (NTSCStr(dtrEc(0)!ec_umprz) = "S")
                    End With
                    '--- Calcolo prezzo
                    CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
                    '--- Valore di ritorno
                    dPrezzo = oCondCommerciali.OutputPrezzo.dPrezzo

                    'Conversione valore finale
                    Dim dQtaTmp As Decimal = 0
                    If dtrEc(0)!ec_umprz.ToString = "S" Then
                      dQtaTmp = NTSCDec(dtrEc(0)!ec_colli)
                    Else
                      dQtaTmp = NTSCDec(dtrEc(0)!ec_quant)
                    End If
                    If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
                      dPrezvalc = dPrezzo
                      dValorev = ArrDbl(dPrezvalc * dQtaTmp / NTSCInt(dtrEc(0)!ec_perqta), oCldDocu.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta)))
                      'trasformo il prezzo da valuta a lire/euro
                      dPrezzo = oCldDocu.ConvImpValuta(strDittaCorrente, True, dPrezvalc, NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
                      dValore = ArrDbl(dPrezzo * dQtaTmp / NTSCInt(dtrEc(0)!ec_perqta), oCldDocu.TrovaNdec(0))
                    Else
                      dValore = ArrDbl(dPrezzo * dQtaTmp / NTSCInt(dtrEc(0)!ec_perqta), oCldDocu.TrovaNdec(0))
                      dValorev = 0
                    End If

                    'Cambia il codcent/flag terzista/controp/contocontr/codcena/costolav/valori
                    dtrAtt!at_terzista = "S"
                    dtrAss(0)!as_codcent = nNewCodcent
                    dtrAss(0)!as_controp = nNewCcodcontr
                    dtrAss(0)!as_contocontr = lNewConcova
                    dtrAss(0)!as_codcena = nNewCcodcena
                    dtrAss(0)!as_valmo = 0
                    dtrAss(0)!as_pagaora = 0
                    dtrAss(0)!as_cmacora = 0
                    dtrAss(0)!as_pagaoratt = 0
                    dtrAss(0)!as_cmacoratt = 0
                    dtrAss(0)!as_valore = dValore
                    dtrAss(0)!as_valorev = dValorev

                    dtrAtt.AcceptChanges()
                    dtrAss(0).AcceptChanges()

                    'Scrive anche in MOTRANS i costi della lavorazione
                    'Occhio alle righe di MOTRANS parzialmente evase
                    dValorevRes = 0
                    If dtrEc(0)!ec_umprz.ToString <> "S" Then
                      dValoreRes = ArrDbl(dPrezzo * (NTSCDec(dtrEc(0)!ec_quant) - NTSCDec(dtrEc(0)!ec_quaeva)) / NTSCDec(dtrEc(0)!ec_perqta), oCldDocu.TrovaNdec(0))
                      If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
                        dValorevRes = ArrDbl(dPrezvalc * (NTSCDec(dtrEc(0)!ec_quant) - NTSCDec(dtrEc(0)!ec_quaeva)) / NTSCDec(dtrEc(0)!ec_perqta), oCldDocu.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta)))
                      End If
                    Else
                      dValoreRes = ArrDbl(dPrezzo * (NTSCDec(dtrEc(0)!ec_colli) - NTSCDec(dtrEc(0)!ec_coleva)) / NTSCDec(dtrEc(0)!ec_perqta), oCldDocu.TrovaNdec(0))
                      If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
                        dValorevRes = ArrDbl(dPrezvalc * (NTSCDec(dtrEc(0)!ec_colli) - NTSCDec(dtrEc(0)!ec_coleva)) / NTSCDec(dtrEc(0)!ec_perqta), oCldDocu.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta)))
                      End If
                    End If

                    dtrEc(0)!ec_prezvalc = dPrezvalc
                    dtrEc(0)!ec_prezzo = dPrezzo
                    dtrEc(0)!ec_valorev = dValorevRes
                    dtrEc(0)!ec_valore = dValoreRes
                    dtrEc(0)!ec_valoremm = dValore

                    dtrEc(0).AcceptChanges()
SaltaFase:
                  End If    'If dtrAss.Length > 0 Then
                Next    'For Each dtrAtt In dttATTIVIT.Select("at_flevas <> 'S'", "at_riga, at_fase")
                dttATTIVIT.AcceptChanges()
                dttASSRIS.AcceptChanges()
              End If    'If dttET.Rows(0)!et_tipork.ToString = "H" Then

              '----------------
              'CARICHI DA PRODUZIONE
              If dttET.Rows(0)!et_tipork.ToString = "T" Then
                lLastRow = -1
                lCountFasi = 0
                For Each dtrAtt In dttLAVCENT.Select("", "lce_riga, lce_rigaa")
                  'se c'è il centro di lavoro ...
                  dtrEc = dttEC.Select("ec_riga = " & dtrAtt!lce_riga.ToString)

                  'Per ogni riga caricata
                  If lLastRow = NTSCInt(dtrAtt!lce_riga) Then
                    lCountFasi = lCountFasi + 1
                  Else
                    lCountFasi = 1
                  End If
                  lLastRow = NTSCInt(dtrAtt!lce_riga)

                  'Si assicura sia una sola riga (per il terzista non posso avere più di una attività), 
                  'nel caso cancella oltre la prima fase
                  If lCountFasi > 1 Then
                    bModif = True
                    dtrAtt.Delete()
                    GoTo SaltaFase1
                  End If

                  'Se il centro della riga è già corretto non fa nulla
                  If nNewCodcent = NTSCInt(dtrAtt!lce_codcent) Then
                    GoTo SaltaFase1
                  End If

                  'Rileva il costo della lavorazione
                  bModif = True
                  dPrezvalc = 0


                  SettaCondCommerciali_IstanziaNTSCondCommerciali()
                  oCondCommerciali.bCalcolaPrezzo = True
                  With oCondCommerciali.Input
                    .strDitta = strDittaCorrente
                    .strCodart = NTSCStr(dtrEc(0)!ec_codart)
                    .nCodlavo = NTSCInt(dtrAtt!lce_codlavo)
                    .lConto = NTSCInt(dttET.Rows(0)!et_conto)
                    .lDestdiv = NTSCInt(dttET.Rows(0)!et_coddest)
                    .nListino = nPeacListinCStd
                    .strUnmis = NTSCStr(dtrEc(0)!ec_unmis)
                    .strUmp = NTSCStr(dtrEc(0)!ec_ump)
                    .nFase = NTSCInt(dtrEc(0)!ec_fase)
                    .strTipoval = "P"
                    .bConspromo = True
                    .dtDatdoc = NTSCDate(dttET.Rows(0)!et_datdoc)
                    .nCodvalu = NTSCInt(dttET.Rows(0)!et_valuta)
                    .dColli = NTSCDec(dtrEc(0)!ec_colli)
                    .dQuant = NTSCDec(dtrEc(0)!ec_quant)
                    .bConsideraPrezziUnmis = True
                    .bPrezziPerUnmis = (NTSCStr(dtrEc(0)!ec_umprz) = "S")
                  End With
                  '--- Calcolo prezzo
                  CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
                  '--- Valore di ritorno
                  dPrezzo = oCondCommerciali.OutputPrezzo.dPrezzo

                  'Conversione valore finale
                  Dim dQtaTmp As Decimal = 0
                  If dtrEc(0)!ec_umprz.ToString = "S" Then
                    dQtaTmp = NTSCDec(dtrEc(0)!ec_colli)
                  Else
                    dQtaTmp = NTSCDec(dtrEc(0)!ec_quant)
                  End If
                  If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
                    dPrezvalc = dPrezzo
                    dValorev = ArrDbl(dPrezvalc * dQtaTmp / NTSCInt(dtrEc(0)!ec_perqta), oCldDocu.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta)))
                    'trasformo il prezzo da valuta a lire/euro
                    dPrezzo = oCldDocu.ConvImpValuta(strDittaCorrente, True, dPrezvalc, NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
                    dValore = ArrDbl(dPrezzo * dQtaTmp / NTSCInt(dtrEc(0)!ec_perqta), oCldDocu.TrovaNdec(0))
                  Else
                    dValore = ArrDbl(dPrezzo * dQtaTmp / NTSCInt(dtrEc(0)!ec_perqta), oCldDocu.TrovaNdec(0))
                    dValorev = 0
                  End If

                  'Cambia il codcent/flag terzista/controp/contocontr/codcena/costolav/valori
                  dtrAtt!lce_codcent = nNewCodcent
                  dtrAtt!lce_controp = nNewCcodcontr
                  dtrAtt!lce_contocontr = lNewConcova
                  dtrAtt!lce_codcena = nNewCcodcena
                  dtrAtt!lce_valmo = 0
                  dtrAtt!lce_pagaora = 0
                  dtrAtt!lce_cmacora = 0
                  dtrAtt!lce_pagaoratt = 0
                  dtrAtt!lce_cmacoratt = 0
                  dtrAtt.AcceptChanges()

                  'Cambia il prezzo/prezvalc su movimento di carico T
                  dtrEc(0)!ec_prezvalc = dPrezvalc
                  dtrEc(0)!ec_prezzo = dPrezzo
                  dtrEc(0).AcceptChanges()
SaltaFase1:
                Next    'For Each dtrAtt In dttLAVCENT.Select("", "lce_riga, lce_rigaa")
                dttLAVCENT.AcceptChanges()
              End If    'If dttET.Rows(0)!et_tipork.ToString = "T" Then

              '----------------
              'Riapre il corpo, e rivalorizza se serve
              If bModif Then ValorizzaProduzione(Nothing)
            End If    'If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
          End If    'If bTerzista And bModPM = False And NTSCStr(dttET.Rows(0)!et_tipork) = "H" Then

        End If    'If NTSCInt(e.ProposedValue) > 0 And dttEC.Rows.Count > 0 Then

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

  Public Overridable Function BeforeColUpdate_TESTA_et_valuta(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ValCodice(e, "VALU", "xx_valuta", oApp.Tr(Me, 127792254993750000, "Codice valuta |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"))

      '--------------------------------------------
      'con scorporo non è possibile emettere documenti in valuta
      If e.Row!et_scorpo.ToString = "S" And bInitializing = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222103281250, "Impossibile utilizzare la valuta quando è attivo lo scorporo")))
        e.ProposedValue = 0
        e.Row!xx_valuta = ""
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

  Public Overridable Function BeforeColUpdate_TESTA_et_confermato(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If dttEC.Rows.Count > 0 Then

        Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 127791956557187500, "Modificare il campo 'Confermato' su tutte le righe di questo documento (e relativi impegni/scarichi a produzione)?"))
        If bInDuplicadoc Then
          msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES
        Else
          ThrowRemoteEvent(msg)
        End If
        If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
          CambioDatiTestataInGriglia(0, 0, 0, 0, 0, 0, e.ProposedValue.ToString, "", "")
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

  Public Overridable Function BeforeColUpdate_TESTA_et_rilasciato(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If dttEC.Rows.Count > 0 Then

        Dim msg As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 129126230282080078, "Modificare il campo 'Rilasciato' su tutte le righe di questo documento (e relativi impegni/scarichi a produzione)?"))
        If bInDuplicadoc Then
          msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES
        Else
          ThrowRemoteEvent(msg)
        End If
        If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
          CambioDatiTestataInGriglia(0, 0, 0, 0, 0, 0, "", "", "", e.ProposedValue.ToString)
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
  Public Overridable Function BeforeColUpdate_TESTA_et_scorpo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If e.ProposedValue.ToString = "S" Then
        If e.Row!et_valuta.ToString <> "0" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222103437500, "Impossibile utilizzare la valuta quando è attivo lo scorporo")))
          e.Row!et_scorpo = "N"
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

  Public Overridable Function BeforeColUpdate_TESTA_et_vettor(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As New DataTable
    Try
      ValCodice(e, "VETT", "xx_vettor", oApp.Tr(Me, 127792255031718750, "Codice vettore |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"))
      If NTSCInt(e.ProposedValue) <> 0 Then
        'se ho impostato sulle anagrafiche un 'acuradi' specifico', lascio quello dell'anagrafica, anche se non è 'V' = vettore: altrimenti l'utente si vede dei dati cambiati senza saperlo
        If NTSCInt(e.Row!et_coddest) = 0 Then
          If NTSCStr(dttEt_conto.Rows(0)!an_acuradi) = "X" Then
            e.Row!et_acuradi = "V"
          End If
        Else
          oCldDocu.ValCodiceDb(NTSCInt(e.Row!et_coddest).ToString, strDittaCorrente, "DESTDIV", "N", "", dttTmp, lContoCF.ToString)
          If dttTmp.Rows.Count > 0 AndAlso NTSCStr(dttTmp.Rows(0)!dd_acuradi) = "X" And NTSCStr(dttEt_conto.Rows(0)!an_acuradi) = "X" Then
            e.Row!et_acuradi = "V"
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

  Public Overridable Function BeforeColUpdate_TESTA_et_vettor2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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

  Public Overridable Function BeforeColUpdate_TESTA_et_caustra(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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

  Public Overridable Function BeforeColUpdate_TESTA_et_porto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCStr(e.ProposedValue) <> " " Then e.ProposedValue = NTSCStr(e.ProposedValue).Replace(" ", "")

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

  Public Overridable Function BeforeColUpdate_TESTA_et_codaspe(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If e.ProposedValue.ToString <> "" Then ValCodice(e, "ASPE", "et_aspetto", oApp.Tr(Me, 127792255281250000, "Codice aspetto esteriore dei beni |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"))
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

  Public Overridable Function BeforeColUpdate_TESTA_et_annpar(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(e.ProposedValue) <> 0 Then
        If NTSCInt(e.ProposedValue) < 1900 Or NTSCInt(e.ProposedValue) > 2099 Then
          e.ProposedValue = e.Row!et_annpar
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128661419693750000, "L'anno partita deve essere o 0 o un numero compreso tra 1900 e 2099")))
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

  Public Overridable Function BeforeColUpdate_TESTA_et_abi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!et_banc1 = ""
        e.Row!et_cab = "0"
        e.Row!et_banc2 = ""
      Else
        bOk = oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ABI", "N", strTmp)
        If bOk = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222104531250, "Codice ABI |" & NTSCInt(e.ProposedValue).ToString & "| non corretto")))
          e.ProposedValue = e.Row(e.Column.ColumnName)
          Return False
        Else
          e.Row!et_banc1 = strTmp
          e.Row!et_cab = "0"
          e.Row!et_banc2 = ""
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

  Public Overridable Function BeforeColUpdate_TESTA_et_cab(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!et_banc2 = ""
      Else
        bOk = oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "CAB", "N", strTmp, Nothing, e.Row!et_abi.ToString)
        If bOk = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222104687500, "Codice CAB |" & NTSCInt(e.ProposedValue).ToString & "| non corretto")))
          e.ProposedValue = e.Row(e.Column.ColumnName)
          Return False
        Else
          e.Row!et_banc2 = strTmp
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

  Public Overridable Function BeforeColUpdate_TESTA_et_codcena(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim event0 As NTSEventArgs
    Try
      If Not ValCodice(e, "CENA", "xx_codcena", oApp.Tr(Me, 127792255357343750, "Codice centro |" & NTSCInt(e.ProposedValue).ToString & "| non corretto")) Then Return False

      ' Controllo che sia cambiato veramente rispetto a  prima
      If NTSCInt(e.ProposedValue) <> NTSCInt(e.Row!et_codcena) And dttEC.Rows.Count <> 0 Then
        event0 = New NTSEventArgs(ThMsg.MSG_YESNO, oApp.Tr(Me, 129270320769952968, "Il Centro nella testata è cambiato." & vbCrLf & "Applicare la modifica anche alle righe del corpo?"))
        ThrowRemoteEvent(event0)
        If event0.RetValue = ThMsg.RETVALUE_YES Then
          For Each dtrT As DataRow In dttEC.Rows
            dtrT!ec_codcena = NTSCInt(e.ProposedValue)
            dtrT.AcceptChanges()
          Next
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

  Public Overridable Function BeforeColUpdate_TESTA_et_codcfam(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ValCodice(e, "CFAM", "xx_codcfam", oApp.Tr(Me, 127792255559687500, "Codice famiglia '|" & e.ProposedValue.ToString & "|' non corretto"), "S")
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

  Public Overridable Function BeforeColUpdate_TESTA_et_codstag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ValCodice(e, "STAG", "xx_codstag", oApp.Tr(Me, 128517056871770000, "Codice stagione (Taglie e colori) |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"))
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

  Public Overridable Function BeforeColUpdate_TESTA_et_annotco(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If (NTSCInt(e.ProposedValue.ToString) < 1900 Or NTSCInt(e.ProposedValue.ToString) > 2099) And bModTCO And NTSCInt(e.ProposedValue.ToString) <> 0 Then
        e.ProposedValue = DateTime.Now.Year
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128517059668408000, "Indicare un ANNO taglie e colori compreso tra 1900 e 2099")))
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

  Public Overridable Function BeforeColUpdate_TESTA_et_proforma(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Try
      strTmp = NTSCStr(oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSVEBOLL", "Opzioni", ".", "FattureProformaSerie", "§", " ", "§"))
      If strTmp <> "§" Then
        If e.ProposedValue.ToString = "S" And e.Row!et_serie.ToString.ToUpper <> strTmp.ToUpper Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130002072019694773, "Possono essere emesse fatture proforma solo con serie '|" & strTmp & "|'. Il flag 'Proforma' verrà deselezionato.")))
          e.ProposedValue = "N"
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

  Public Overridable Function BeforeColUpdate_TESTA_et_codbanc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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

  Public Overridable Function BeforeColUpdate_TESTA_et_contfatt(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As New DataTable
    Dim evnt As NTSEventArgs
    Try
      ValCodice(e, "ANAGRACF", "xx_contfatt", oApp.Tr(Me, 127792254686093750, "Codice cliente/fornitore per conto fatturazione |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"), "N", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        If bInImportRigheNota = False And bInImportRigheOrd = False Then
          If dttTmp.Rows(0)!an_tipo.ToString = "F" And bDocEmesso Then
            ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 128517065436358000, "Attenzione: si sta indicando un FORNITORE nel conto fatturazione in un documento EMESSO")))
          End If
          If dttTmp.Rows(0)!an_tipo.ToString = "C" And bDocEmesso = False Then
            ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 128517066059422000, "Attenzione: si sta indicando un CLIENTE nel conto fatturazione in un documento RICEVUTO")))
          End If
          If bUsaCondPagContoFatt And Not bInNuovoDocSilent Then
            evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128661412794375000, "Inserire sul documento corrente l'ABI/CAB/Condizioni pagamento, codice esenzione del cliente/fornitore di fatturazione?"))
            ThrowRemoteEvent(evnt)
            If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              e.Row!et_abi = dttTmp.Rows(0)!an_abi
              e.Row!et_cab = dttTmp.Rows(0)!an_cab
              e.Row!et_banc1 = dttTmp.Rows(0)!an_banc1
              e.Row!et_banc2 = dttTmp.Rows(0)!an_banc2
              e.Row!et_codbanc = dttTmp.Rows(0)!an_codbanc
              e.Row!et_codpaga = dttTmp.Rows(0)!an_codpag
              e.Row!et_codese = dttTmp.Rows(0)!an_codese
            End If ' If bUsaCondPagContoFatt Then
          End If

          If nUsaCondPagContoFatt = 1 And Not bInNuovoDocSilent Then
            evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128661412794375001, "Inserire sul documento corrente l'ABI/CAB del cliente/fornitore di fatturazione?"))
            ThrowRemoteEvent(evnt)
            If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              e.Row!et_abi = dttTmp.Rows(0)!an_abi
              e.Row!et_cab = dttTmp.Rows(0)!an_cab
              e.Row!et_banc1 = dttTmp.Rows(0)!an_banc1
              e.Row!et_banc2 = dttTmp.Rows(0)!an_banc2
            End If ' If bUsaCondPagContoFatt Then
          End If

        End If ' If bInImportRigheNota = False And bInImportRigheOrd = False Then
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

  Public Overridable Function BeforeColUpdate_TESTA_et_commeca(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim bCheckCommecaSuRighe As Boolean = False
    Dim dttTmp As DataTable = New DataTable
    Dim dttTmp1 As DataTable = New DataTable
    Dim msg As NTSEventArgs = Nothing
    Try
      If ValCodice(e, "COMMESS", "xx_commeca", oApp.Tr(Me, 128740035836505000, "Codice commessa |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"), "N", dttTmp) Then
        If (bControllaCommessaChiusa = True) And (NTSCInt(e.ProposedValue) <> 0) Then
          If Year(NTSCDate(dttTmp.Rows(0)!co_dtchiu)) <> 2099 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222095156250, "Attenzione!" & vbCrLf & "La commessa indicata è già stata chiusa.")))
          End If
        End If

        '---------------------------------------------
        'Al cambio della commessa, se era già stata indicata una sottocommessa e questa
        'non esiste, azzera il campo
        If bDisabilitaControlliSubcomm = False Then
          If NTSCInt(e.ProposedValue) <> 0 Then
            If NTSCStr(e.Row!et_subcommeca).Trim <> "" Then
              oCldDocu.ValCodiceDb(NTSCStr(e.Row!et_subcommeca), strDittaCorrente, "SUBCOMM", "S", "", dttTmp1, e.ProposedValue.ToString)
              If dttTmp1.Rows.Count = 0 Then
                e.Row!et_subcommeca = " "
              End If
              dttTmp1.Clear()
            End If
          Else
            If NTSCStr(e.Row!et_subcommeca) <> " " Then e.Row!et_subcommeca = " "
          End If
        End If

        If NTSCInt(e.ProposedValue) > 0 And dttEC.Rows.Count > 0 Then
          If bModPM = False Then
            bCheckCommecaSuRighe = True
          Else
            If (dttEC.Select("ec_pmtaskid > 0").Length = 0) And (dttECIMP.Select("ec_pmtaskid > 0").Length = 0) Then
              bCheckCommecaSuRighe = True
            End If
          End If
          If bCheckCommecaSuRighe = True Then
            If NTSCStr(dttET.Rows(0)!et_tipork) = "H" Or NTSCStr(dttET.Rows(0)!et_tipork) = "T" Then
              msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128522909695032000, "Modificare il Codice commessa su tutte le righe di questo documento ed i relativi impegni/scarichi collegati?"))
            Else
              msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 127791956529062500, "Modificare il Codice commessa su tutte le righe di questo documento?"))
            End If
            ThrowRemoteEvent(msg)
            If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
              CambioDatiTestataInGriglia(0, 0, 0, 0, 0, NTSCInt(e.ProposedValue), "", "", "")
            End If
          End If    'If bModPM = False Then
        End If
      Else
        e.Row!et_subcommeca = " "
      End If    'If ValCodice(e, "COMMESS", "xx_commeca", oApp.Tr(Me, 128740035855537000, "Codice commessa non corretto"), "N", dttTmp) Then

      If NTSCInt(e.ProposedValue) > 0 And dttTmp.Rows.Count > 0 And bControllaCommessaChiusa And bInCreaDocDaGnor = False Then
        If ValCodice(e, "COMMESS", "xx_commeca", oApp.Tr(Me, 128740035878313000, "Codice commessa |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"), "N", dttTmp) Then
          If Year(NTSCDate(dttTmp.Rows(0)!co_dtchiu)) <> 2099 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222104375000, "Attenzione!" & vbCrLf & "La commessa indicata è già stata chiusa.")))
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
    Finally
      dttTmp.Clear()
      dttTmp1.Clear()
    End Try
  End Function

  Public Overridable Function BeforeColUpdate_TESTA_et_subcommeca(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim msg As NTSEventArgs = Nothing
    Dim dttTmp As New DataTable
    Try
      If NTSCStr(e.ProposedValue) = "" Then e.ProposedValue = " "

      If bDisabilitaControlliSubcomm = False Then
        If NTSCStr(e.ProposedValue) <> " " Then
          oCldDocu.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "SUBCOMM", "S", "", dttTmp, e.Row!et_commeca.ToString)
          If dttTmp.Rows.Count = 0 Then
            e.ProposedValue = " "
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128547937874934000, "Codice sottocommessa inesistente")))
          End If
        End If
      End If    'If bDisabilitaControlliSubcomm = False Then

      If bModPM = False And dttEC.Rows.Count > 0 Then
        If NTSCStr(dttET.Rows(0)!et_tipork) = "H" Or NTSCStr(dttET.Rows(0)!et_tipork) = "T" Then
          msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128522903430848000, "Modificare la subcommessa su tutte le righe di questo documento ed i relativi impegni/scarichi collegati?"))
        Else
          msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128522903446760000, "Modificare la subcommessa su tutte le righe di questo documento?"))
        End If

        ThrowRemoteEvent(msg)
        If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
          CambioDatiTestataInGriglia(0, 0, 0, 0, 0, 0, "", "", NTSCStr(e.ProposedValue))
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

  Public Overridable Function BeforeColUpdate_TESTA_et_datpar(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCDate(dttET.Rows(0)!et_datapag) = NTSCDate(dttET.Rows(0)!et_datpar) Or NTSCDate(dttET.Rows(0)!et_datapag) = NTSCDate(dttET.Rows(0)!et_datdoc) Then
        dttET.Rows(0)!et_datapag = CalcolaDataScadenza(NTSCStr(dttET.Rows(0)!et_tipork), NTSCStr(e.ProposedValue), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString)
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

  Public Overridable Function BeforeColUpdate_TESTA_et_coddivi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim event0 As NTSEventArgs
    Try
      ValCodice(e, "DIVI", "xx_coddivi", oApp.Tr(Me, 129270278684816250, "Codice divisione C/A |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"))

      ' Controllo che sia cambiato veramente rispetto a  prima
      If NTSCInt(e.ProposedValue) <> NTSCInt(e.Row!et_coddivi) And dttEC.Rows.Count <> 0 Then
        event0 = New NTSEventArgs(ThMsg.MSG_YESNO, oApp.Tr(Me, 129270320391808437, "La divisione di CA nella testata è cambiato." & vbCrLf & "Applicare la modifica anche alle righe del corpo?"))
        ThrowRemoteEvent(event0)
        If event0.RetValue = ThMsg.RETVALUE_YES Then
          For Each dtrT As DataRow In dttEC.Rows
            dtrT!ec_coddivi = NTSCInt(e.ProposedValue)
            dtrT.AcceptChanges()
          Next
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

  Public Overridable Function BeforeColUpdate_TESTA_et_codcli(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codcli = ""
        Return True
      End If
      If Not oCldDocu.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "ANAGRACF", "N", strTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129228204070185546, "Codice cliente C/A '|" & e.ProposedValue.ToString & "|' non corretto")))
        e.ProposedValue = e.Row!et_codcli.ToString
        Return False
      End If
      e.Row!xx_codcli = strTmp

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

#Region "AfterColUpdate_TESTA"
  Public Overridable Sub AfterColUpdate_TESTA(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return

    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChangesET = True
      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      If bEndEditET = True Then
        e.Row.EndEdit()
        e.Row.EndEdit()
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_TESTA_" & e.Column.ColumnName.ToLower
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

  Public Overridable Function AfterColUpdate_TESTA_et_conto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      'Al cambio di cliente azzero sempre l'organizzazione
      If BORDINI Then
        e.Row!et_organig = 0
      Else
        e.Row!et_conto2 = e.Row!et_conto
      End If

      CaricaContrattiCliente()
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function AfterColUpdate_TESTA_et_coddest(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      CaricaContrattiCliente()
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function AfterColUpdate_TESTA_et_codpaga(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        If NTSCStr(e.Row!et_datdoc).Trim <> "" Then
          e.Row!et_datapag = CalcolaDataScadenza(NTSCStr(e.Row!et_tipork), NTSCStr(e.Row!et_datpar), NTSCDate(e.Row!et_datdoc).ToShortDateString)
        End If
      Else
        If GetTipoPag(NTSCInt(e.ProposedValue).ToString) <> 4 Then
          If NTSCStr(e.Row!et_datdoc).Trim <> "" Then
            e.Row!et_datapag = CalcolaDataScadenza(NTSCStr(e.Row!et_tipork), NTSCStr(e.Row!et_datpar), NTSCDate(e.Row!et_datdoc).ToShortDateString)
          End If
        End If
      End If

      CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_scont1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      'ricalcolo i valori di riga
      For Each dtrT As DataRow In dttEC.Rows
        If (dttET.Rows(0)!et_tipork.ToString = "T" Or dttET.Rows(0)!et_tipork.ToString = "H") Then ValorizzaProduzione(dtrT)
        SettaValoriRiga(dtrT)
      Next
      CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_scont2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      'ricalcolo i valori di riga
      For Each dtrT As DataRow In dttEC.Rows
        If (dttET.Rows(0)!et_tipork.ToString = "T" Or dttET.Rows(0)!et_tipork.ToString = "H") Then ValorizzaProduzione(dtrT)
        SettaValoriRiga(dtrT)
      Next
      CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_scopag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      'ricalcolo i valori di riga
      For Each dtrT As DataRow In dttEC.Rows
        If (dttET.Rows(0)!et_tipork.ToString = "T" Or dttET.Rows(0)!et_tipork.ToString = "H") Then ValorizzaProduzione(dtrT)
        SettaValoriRiga(dtrT)
      Next
      CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_speinc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_bolli(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_totomag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_speacc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_speaccv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
        dttET.Rows(0)!et_speacc = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttET.Rows(0)!et_speaccv), _
                                                        NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), _
                                                        NTSCDec(dttET.Rows(0)!et_cambio))
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

  Public Overridable Function AfterColUpdate_TESTA_et_valuta(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try

      If NTSCInt(e.ProposedValue) <> 0 Then
        'cerco il cambio alla data del documento
        If bNew Then e.Row!et_cambio = oCldDocu.CercaCambioDiOggi(NTSCInt(e.ProposedValue), e.Row!et_datdoc.ToString)

        'ricalcolo gli importi in valuta da quelli in euro
        If NTSCDec(dttET.Rows(0)!et_speinc) <> 0 Then
          dttET.Rows(0)!et_speincv = oCldDocu.ConvImpEur(False, NTSCDec(dttET.Rows(0)!et_speinc), NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
        End If
        If NTSCDec(dttET.Rows(0)!et_speimb) > 0 Then
          dttET.Rows(0)!et_speimbv = oCldDocu.ConvImpEur(False, NTSCDec(dttET.Rows(0)!et_speimb), NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
        End If
        If NTSCDec(dttET.Rows(0)!et_bolli) > 0 Then
          dttET.Rows(0)!et_bolliv = oCldDocu.ConvImpEur(False, NTSCDec(dttET.Rows(0)!et_bolli), NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
        End If
        If NTSCDec(dttET.Rows(0)!et_speacc) > 0 Then
          dttET.Rows(0)!et_speaccv = oCldDocu.ConvImpEur(False, NTSCDec(dttET.Rows(0)!et_speacc), NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
        End If
      Else
        If NTSCDec(e.Row!et_cambio) <> 0 Then e.Row!et_cambio = 0
        If NTSCDec(e.Row!et_speincv) <> 0 Then e.Row!et_speincv = 0
        If NTSCDec(e.Row!et_speimbv) <> 0 Then e.Row!et_speimbv = 0
        If NTSCDec(e.Row!et_bolliv) <> 0 Then e.Row!et_bolliv = 0
        If NTSCDec(e.Row!et_speaccv) <> 0 Then e.Row!et_speaccv = 0
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

  Public Overridable Function AfterColUpdate_TESTA_et_cambio(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim i As Integer = 0
    Try
      '---------------------------------------------
      For i = 0 To dttEC.Rows.Count - 1
        'devo aggiornare il valore delle righe di movord al variare del cambio
        Dim dTmp As Decimal = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttEC.Rows(i)!ec_prezvalc), NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))


        If bModPM Then
          Dim dttclone As DataTable
          dttclone = dttEC.Clone()
          dttclone.ImportRow(dttEC.Rows(i))
          dttclone.Rows(0).RejectChanges()
          If BORDINI Then
            If Not oCldDocu.AggProesebDaMovordDao(strDittaCorrente, 1, "MOTRANS", 0, "TTPROESEC", lIIttproesebappo, _
                                          dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                          dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                          NTSCInt(dttEC.Rows(i)!ec_riga), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                          Nothing, dttclone) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129446548670068359, "Attenzione: non è stato possibile aggiornare la commessa (proeseb) sugli archivi !")))
            End If
          Else
            If Not oCldDocu.AggProesebDaMovmagDao(strDittaCorrente, 1, "MMTRANS", 0, "TTPROESEC", lIIttproesebappo, _
                                          dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                          dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                          NTSCInt(dttEC.Rows(i)!ec_riga), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                          Nothing, dttclone) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129445644891933593, "Attenzione: non è stato possibile aggiornare la commessa (proeseb) sugli archivi !")))
            End If
          End If
          'agg. altri record di mmtrans, riferiti allo stesso task-id, se presenti
          dQtadispass = 0 : dValdispass = 0
          'legge i valori preesistenti
          AggResAltriRkCorpo(1, 1, dttEC, dttclone.Rows(0), dQtadispass, dValdispass)
        End If

        dttEC.Rows(i)!ec_prezzo = dTmp

        If bModPM Then
          If BORDINI Then
            If Not oCldDocu.AggProesebDaMovordDao(strDittaCorrente, 0, "MOTRANS", 0, "TTPROESEC", lIIttproesebappo, _
                                dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                NTSCInt(dttEC.Rows(i)!ec_riga), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                Nothing, dttEC) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129446545502099609, "Attenzione: non è stato possibile aggiornare la commessa (proeseb) sugli archivi !")))
            End If
          Else
            If Not oCldDocu.AggProesebDaMovmagDao(strDittaCorrente, 0, "MMTRANS", 0, "TTPROESEC", lIIttproesebappo, _
                                dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), _
                                NTSCInt(dttEC.Rows(i)!ec_riga), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString, _
                                Nothing, dttEC) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129445644830498046, "Attenzione: non è stato possibile aggiornare la commessa (proeseb) sugli archivi !")))
            End If
          End If
          ' scrive per stornare i vecchi valori
          Dim dttclone As DataTable
          dttclone = dttEC.Clone()
          dttclone.ImportRow(dttEC.Rows(i))
          dttclone.Rows(0).RejectChanges()
          AggResAltriRkCorpo(1, 2, dttEC, dttclone.Rows(0), dQtadispass, dValdispass)
          ' scrive completamente i nuovi valori
          AggResAltriRkCorpo(0, 0, dttEC, dttEC.Rows(i), dQtadispass, dValdispass)
        End If

        dttEC.AcceptChanges()
      Next

      '---------------------------------------------
      If dttET.Rows(0)!et_tipork.ToString = "H" Or dttET.Rows(0)!et_tipork.ToString = "T" Then
        For i = 0 To dttECIMP.Rows.Count - 1
          'devo aggiornare il valore delle righe di movord/movmag al variare del cambio
          Dim dTmp As Decimal = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttECIMP.Rows(i)!ec_prezvalc), NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
          dttECIMP.Rows(i)!ec_prezzo = dTmp
        Next
      End If

      '---------------------------------------------
      'rivalorizzo gli impegni e le lavorazioni, visto che è cambiato il cambio ...
      ValorizzaProduzione(Nothing)

      '---------------------------------------------
      'devo aggiornare lbSpeinc, edSpeimb, lbBolli, edSpeacc
      If NTSCDec(e.Row!et_cambio) <> 0 Then
        If NTSCDec(dttET.Rows(0)!et_speincv) <> 0 Then
          dttET.Rows(0)!et_speinc = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttET.Rows(0)!et_speincv), NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
        End If
        If NTSCDec(dttET.Rows(0)!et_speimbv) > 0 Then
          dttET.Rows(0)!et_speimb = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttET.Rows(0)!et_speimbv), NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
        End If
        If NTSCDec(dttET.Rows(0)!et_bolliv) > 0 Then
          dttET.Rows(0)!et_bolli = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttET.Rows(0)!et_bolliv), NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
        End If
        If NTSCDec(dttET.Rows(0)!et_speaccv) > 0 Then
          dttET.Rows(0)!et_speacc = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttET.Rows(0)!et_speaccv), NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCDec(dttET.Rows(0)!et_cambio))
        End If
      Else
        If NTSCDec(e.Row!et_cambio) <> 0 Then e.Row!et_cambio = 0
        If NTSCDec(e.Row!et_speincv) <> 0 Then e.Row!et_speincv = 0
        If NTSCDec(e.Row!et_speimbv) <> 0 Then e.Row!et_speimbv = 0
        If NTSCDec(e.Row!et_bolliv) <> 0 Then e.Row!et_bolliv = 0
        If NTSCDec(e.Row!et_speaccv) <> 0 Then e.Row!et_speaccv = 0
      End If

      CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_codese(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_flspinc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As New DataTable
    Try
      If e.Row!et_flspinc.ToString = "S" And NTSCInt(e.Row!et_codpaga) <> 0 Then
        oCldDocu.ValCodiceDb(NTSCInt(e.Row!et_codpaga).ToString, strDittaCorrente, "TABPAGA", "N", "", dttTmp)
        e.Row!et_speinc = NTSCDec(dttTmp.Rows(0)!tb_speinca)
        If NTSCInt(e.Row!et_valuta) <> 0 Then
          e.Row!et_speincv = oCldDocu.ConvImpEur(False, NTSCDec(dttTmp.Rows(0)!tb_speinca), _
                                                  NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), _
                                                  NTSCDec(dttET.Rows(0)!et_cambio))
        End If
      Else
        e.Row!et_speinc = 0
        e.Row!et_speincv = 0
      End If

      'CalcolaTotali()   'non serve: viene eseguita in automatico al cambio degli importi delle spese!!!
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

  Public Overridable Function AfterColUpdate_TESTA_et_flboll(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      'CalcolaTotali()   'non serve: viene eseguita in automatico al cambio degli importi delle spese!!!
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

  Public Overridable Function AfterColUpdate_TESTA_et_pagato(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_abbuono(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_speincv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
        dttET.Rows(0)!et_speinc = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttET.Rows(0)!et_speincv), _
                                                        NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), _
                                                        NTSCDec(dttET.Rows(0)!et_cambio))
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

  Public Overridable Function AfterColUpdate_TESTA_et_bolliv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
        dttET.Rows(0)!et_bolli = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttET.Rows(0)!et_bolliv), _
                                                        NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), _
                                                        NTSCDec(dttET.Rows(0)!et_cambio))
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

  Public Overridable Function AfterColUpdate_TESTA_et_totomagv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
        dttET.Rows(0)!et_totomag = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttET.Rows(0)!et_totomagv), _
                                                        NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), _
                                                        NTSCDec(dttET.Rows(0)!et_cambio))
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

  Public Overridable Function AfterColUpdate_TESTA_et_pagatov(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
        dttET.Rows(0)!et_pagato = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttET.Rows(0)!et_pagatov), _
                                                        NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), _
                                                        NTSCDec(dttET.Rows(0)!et_cambio))
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

  Public Overridable Function AfterColUpdate_TESTA_et_abbuonov(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
        dttET.Rows(0)!et_abbuono = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttET.Rows(0)!et_abbuonov), _
                                                        NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), _
                                                        NTSCDec(dttET.Rows(0)!et_cambio))
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

  Public Overridable Function AfterColUpdate_TESTA_et_speimb(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_speimbv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
        dttET.Rows(0)!et_speimb = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(dttET.Rows(0)!et_speimbv), _
                                                        NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), _
                                                        NTSCDec(dttET.Rows(0)!et_cambio))
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

  Public Overridable Function AfterColUpdate_TESTA_et_vettor(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not bCalcolaSpeseTraspSoloNuovi OrElse bNew Then
        If strCalcolaSpeseTrasp = "S" OrElse (strCalcolaSpeseTrasp = "E" AndAlso bDocEmesso) Then
          CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_porto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not bCalcolaSpeseTraspSoloNuovi OrElse bNew Then
        If strCalcolaSpeseTrasp = "S" OrElse (strCalcolaSpeseTrasp = "E" AndAlso bDocEmesso) Then
          CalcolaTotali()
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

  Public Overridable Function AfterColUpdate_TESTA_et_peso(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not bCalcolaSpeseTraspSoloNuovi OrElse bNew Then
        If strCalcolaSpeseTrasp = "S" OrElse (strCalcolaSpeseTrasp = "E" AndAlso bDocEmesso) Then
          CalcolaTotali()
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


  Public Overridable Sub AfterColUpdate_TESTA_et_abi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs("AggiornaColoreAbiCab", ""))
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub AfterColUpdate_TESTA_et_cab(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs("AggiornaColoreAbiCab", ""))
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub AfterColUpdate_TESTA_et_codagen(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      CaricaContrattiCliente() 'Ricarca tutti i contratti, sia agente che cliente
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub AfterColUpdate_TESTA_et_codagen2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      CaricaContrattiCliente() 'Ricarca tutti i contratti, sia agente che cliente
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

  Public Overridable Sub BeforeColUpdate_TESTAC(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return
    Dim strErr As String = ""
    Try
      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
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
      Dim strFunction As String = "BeforeColUpdate_TESTAC_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub AfterColUpdate_TESTAC(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return

    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChangesET = True
      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      If bEndEditET = True Then
        e.Row.EndEdit()
        e.Row.EndEdit()
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_TESTAC_" & e.Column.ColumnName.ToLower
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

  '-------------------------------------

#Region "Precedenti movimenti e precedenti ordini"
  Public Overridable Function GetPrecedentiMovimenti(ByVal strDitta As String, ByRef dsOut As DataSet, _
                                                    ByVal strCodart As String, ByVal nFase As Integer, _
                                                    ByVal nMagaz As Integer, ByVal lConto As Integer) As Boolean
    Try
      Return GetPrecedentiMovimenti(strDitta, dsOut, strCodart, nFase, nMagaz, lConto, False, False, "", 0, "", True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function GetPrecedentiMovimenti(ByVal strDitta As String, ByRef dsOut As DataSet, _
                                                    ByVal strCodart As String, ByVal nFase As Integer, _
                                                    ByVal nMagaz As Integer, ByVal lConto As Integer, _
                                                    ByVal bModuloCRM As Boolean, ByVal bIsCRMUser As Boolean, _
                                                    ByVal strAccvis As String, ByVal lCodorgaOperat As Integer, _
                                                    ByVal strRegvis As String, ByVal bAmm As Boolean) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dsOut, strCodart, nFase, nMagaz, lConto, bModuloCRM, bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsOut = CType(oIn(1), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      Return oCldDocu.GetPrecedentiMovimenti(strDitta, dsOut, strCodart, nFase, nMagaz, lConto, bModuloCRM, _
                                             bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function GetPrecedentiOrdini(ByVal strDitta As String, ByRef dsOut As DataSet, _
                                                ByVal strCodart As String, ByVal nFase As Integer, _
                                                ByVal lConto As Integer) As Boolean
    Try
      Return oCldDocu.GetPrecedentiOrdini(strDitta, dsOut, strCodart, nFase, lConto)
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

#Region "Import da terminale, da lista selezionata, Esportazione\Importazione riga"
  Public strTermPrgOwner As String = ""
  Public strTermKey As String = "Terminale"
  'Shell da eseguire
  Public bTermExecute As Boolean 'False(default)
  Public strTermCommand As String 'vuoto(default)
  Public bTermSilent As Boolean 'False(default)
  'File da acquisire
  Public strTermFileName As String 'sempre obbligatorio
  Public bTermDeleteAfter As Boolean 'True(default)
  Public bTermIgnoreFirstRow As Boolean 'False(default)
  Public bTermShowLogErr As Boolean 'False(default)
  Public strTermFileType As String 'F=fixed(default), C=comma delimited, S=carattere separatore
  Public strTermFieldSep As String 'vuoto(default)
  Public strTermDecimalSep As String '(virgola)=default, (punto)
  'Tracciato file
  Public strTermTipoArt As String 'C=codart, B=barcode (default), A=codarfo
  Public nTermCodartPos As Integer '0-255(default 0)
  Public nTermCodartStart As Integer '0-9999(default 1)
  Public nTermCodartStop As Integer '0-9999(default 13)
  Public bTermDescr As Boolean 'True (default): nuovo: legge dal file ascii la descrizione o meno
  Public nTermDescrPos As Integer '0-255
  Public nTermDescrStart As Integer '0-9999
  Public nTermDescrStop As Integer '0-9999
  Public bTermDesint As Boolean 'True (default): nuovo: legge dal file ascii la descrizione o meno
  Public nTermDesintPos As Integer '0-255
  Public nTermDesintStart As Integer '0-9999
  Public nTermDesintStop As Integer '0-9999
  Public bTermQuant As Boolean 'True (default): nuovo: legge dal file ascii le qtà o meno
  Public nTermQuantPos As Integer '0-255(default 0)
  Public nTermQuantStart As Integer '0-9999(default 14)
  Public nTermQuantStop As Integer '0-9999(default 19)
  Public bTermPrezzi As Boolean 'False(default)
  Public nTermPrezziPos As Integer '0-255(default 0)
  Public nTermPrezziStart As Integer '0-9999(default 0)
  Public nTermPrezziStop As Integer '0-9999(default 0)
  Public nTermDivQuant As Integer '1-10000(default 1)
  Public nTermDivPrezzi As Integer '1-10000(default 1)
  Public bTermScont1 As Boolean 'False(default)
  Public nTermScont1Pos As Integer '0-255(default 0)
  Public nTermScont1Start As Integer '0-9999(default 0)
  Public nTermScont1Stop As Integer '0-9999(default 0)
  Public nTermDivScont1 As Integer '1-10000(default 1)
  Public bTermScont2 As Boolean 'False(default)
  Public nTermScont2Pos As Integer '0-255(default 0)
  Public nTermScont2Start As Integer '0-9999(default 0)
  Public nTermScont2Stop As Integer '0-9999(default 0)
  Public nTermDivScont2 As Integer '1-10000(default 1)
  Public bTermScont3 As Boolean 'False(default)
  Public nTermScont3Pos As Integer '0-255(default 0)
  Public nTermScont3Start As Integer '0-9999(default 0)
  Public nTermScont3Stop As Integer '0-9999(default 0)
  Public nTermDivScont3 As Integer '1-10000(default 1)
  Public bTermScont4 As Boolean 'False(default)
  Public nTermScont4Pos As Integer '0-255(default 0)
  Public nTermScont4Start As Integer '0-9999(default 0)
  Public nTermScont4Stop As Integer '0-9999(default 0)
  Public nTermDivScont4 As Integer '1-10000(default 1)
  Public bTermScont5 As Boolean 'False(default)
  Public nTermScont5Pos As Integer '0-255(default 0)
  Public nTermScont5Start As Integer '0-9999(default 0)
  Public nTermScont5Stop As Integer '0-9999(default 0)
  Public nTermDivScont5 As Integer '1-10000(default 1)
  Public bTermScont6 As Boolean 'False(default)
  Public nTermScont6Pos As Integer '0-255(default 0)
  Public nTermScont6Start As Integer '0-9999(default 0)
  Public nTermScont6Stop As Integer '0-9999(default 0)
  Public nTermDivScont6 As Integer '1-10000(default 1)
  Public bTermDatcons As Boolean 'True (default): nuovo: legge dal file ascii la descrizione o meno
  Public nTermDatconsPos As Integer '0-255
  Public nTermDatconsStart As Integer '0-9999
  Public nTermDatconsStop As Integer '0-9999
  Public bTermBarcodeConfez As Boolean 'False (default), se True mette la quantità come colli quando il tipo di lettura è barcode
  Public bTermProponiUmSecDaArticolo As Boolean 'se abilitata utilizza i campi 'UM da proporre' nell'articolo
  '--- Export
  Public bTermExecuteE As Boolean
  Public strTermCommandE As String
  Public bTermSilentE As Boolean
  Public strTermFileNameE As String
  Public bGeneraTracE As Boolean
  Public bTermShowLogErrE As Boolean
  Public strTermFileTypeE As String
  Public strTermFieldSepE As String
  Public bMatricE As Boolean
  Public lMatricE As Integer
  Public lQtaMatrE As Integer
  Public bTagliaE As Boolean
  Public lTagliaE As Integer
  Public lQtaTagliaE As Integer
  Public bTipoRigaE As Boolean
  Public dttExport As New DataTable
  Public bRimuoviVbcrlf As Boolean
  '--- Import
  Public bTermExecuteI As Boolean
  Public strTermCommandI As String
  Public bTermSilentI As Boolean
  Public strTermFileNameI As String
  Public bTermDeleteAfterI As Boolean
  Public bTermIgnoreFirstRowI As Boolean
  Public bTermRicalPrezI As Boolean
  Public bTermRicalScontI As Boolean
  Public bTermRicalProvvI As Boolean
  Public bTermShowLogErrI As Boolean
  Public strTermFileTypeI As String
  Public strTermFieldSepI As String
  Public strTermDecimalSepI As String
  Public bMatricI As Boolean
  Public lMatricI As Integer
  Public lQtaMatrI As Integer
  Public bTagliaI As Boolean
  Public lTagliaI As Integer
  Public lQtaTagliaI As Integer
  Public bTipoRigaI As Boolean
  Public dttImport As New DataTable


  Public Overridable Function TerminaleLeggiCheck(ByVal strCaller As String) As Boolean
    Dim strImport(), strExport(), strRighe(), strCelle() As String
    Try
      'File terminale
      strTermPrgOwner = strCaller
      bTermExecute = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermExecute", "0", " ", "0")))
      strTermCommand = ""
      bTermSilent = False
      If bTermExecute Then
        strTermCommand = oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermCommand", "", " ", "")
        bTermSilent = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermSilent", "0", " ", "0")))
      End If
      strTermFileName = oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermFileName", "", " ", "")
      bTermDeleteAfter = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDeleteAfter", "-1", " ", "-1")))
      bTermIgnoreFirstRow = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermIgnoreFirstRow", "0", " ", "0")))
      bTermShowLogErr = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermShowLogErr", "-1", " ", "-1")))
      strTermFileType = UCase$(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermFileType", "F", " ", "F"))
      If strTermFileType <> "F" And strTermFileType <> "S" And strTermFileType <> "C" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129247189375625000, "Valore della proprietà '|" & strTermPrgOwner & "|\|" & strTermKey & "|\TermFileType' (|" & strTermFileType & "|) non corretto, verrà utilizzato 'F'.")))
        strTermFileType = "F"
      End If
      strTermFieldSep = ""
      If strTermFileType = "S" Then
        strTermFieldSep = Microsoft.VisualBasic.Left(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermFieldSep", ";", " ", ";"), 1)
      End If
      strTermDecimalSep = Microsoft.VisualBasic.Left(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDecimalSep", ",", " ", ","), 1)
      If strTermDecimalSep <> "," And strTermDecimalSep <> "." Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129247189556718750, "Valore delle proprietà '|" & strTermPrgOwner & "|\|" & strTermKey & "|\TermDecimalSep' (|" & strTermDecimalSep & "|) non corretto, verrà utilizzato ','.")))
        strTermDecimalSep = ","
      End If
      strTermTipoArt = UCase$(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermTipoArt", "B", " ", "B"))
      If strTermTipoArt <> "C" And strTermTipoArt <> "B" And strTermTipoArt <> "A" And strTermTipoArt <> "X" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571261447812500, "Valore della proprietà '|" & strTermPrgOwner & "|\|" & strTermKey & "|\TermTipoArt' (|" & strTermTipoArt & "|) non corretto, verrà utilizzato 'B'.")))
        strTermTipoArt = "B"
      End If
      bTermPrezzi = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermPrezzi", "0", " ", "0")))
      bTermQuant = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermQuant", "-1", " ", "-1")))
      bTermDescr = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDescr", "-1", " ", "-1")))
      bTermDesint = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDesint", "-1", " ", "-1")))
      bTermScont1 = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont1", "0", " ", "0")))
      bTermScont2 = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont2", "0", " ", "0")))
      bTermScont3 = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont3", "0", " ", "0")))
      bTermScont4 = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont4", "0", " ", "0")))
      bTermScont5 = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont5", "0", " ", "0")))
      bTermScont6 = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont6", "0", " ", "0")))
      bTermDatcons = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDatcons", "0", " ", "0")))
      nTermCodartPos = 0
      nTermDescrPos = 0
      nTermDesintPos = 0
      nTermQuantPos = 0
      nTermPrezziPos = 0
      nTermScont1Pos = 0
      nTermScont2Pos = 0
      nTermScont3Pos = 0
      nTermScont4Pos = 0
      nTermScont5Pos = 0
      nTermScont6Pos = 0
      nTermDatconsPos = 0
      nTermCodartStart = 0
      nTermCodartStop = 0
      nTermDescrStart = 0
      nTermDescrStop = 0
      nTermDesintStart = 0
      nTermDesintStop = 0
      nTermQuantStart = 0
      nTermQuantStop = 0
      nTermPrezziStart = 0
      nTermPrezziStop = 0
      nTermDivPrezzi = 1
      nTermDivQuant = 1
      nTermScont1Start = 0
      nTermScont1Stop = 0
      nTermDivScont1 = 1
      nTermScont2Start = 0
      nTermScont2Stop = 0
      nTermDivScont2 = 1
      nTermScont3Start = 0
      nTermScont3Stop = 0
      nTermDivScont3 = 1
      nTermScont4Start = 0
      nTermScont4Stop = 0
      nTermDivScont4 = 1
      nTermScont5Start = 0
      nTermScont5Stop = 0
      nTermDivScont5 = 1
      nTermScont6Start = 0
      nTermScont6Stop = 0
      nTermDivScont6 = 1
      nTermDatconsStart = 0
      nTermDatconsStop = 0
      If Not (strTermFileType = "F") Then
        nTermCodartPos = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermCodartPos", "1", " ", "1")))
        If bTermDescr Then
          nTermDescrPos = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDescrPos", "2", " ", "2")))
        End If
        If bTermDesint Then
          nTermDesintPos = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDesintPos", "2", " ", "2")))
        End If
        If bTermQuant Then
          nTermQuantPos = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermQuantPos", "3", " ", "3")))
        End If
        If bTermPrezzi Then
          nTermPrezziPos = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermPrezziPos", "4", " ", "4")))
        End If
        If bTermScont1 Then
          nTermScont1Pos = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont1Pos", "5", " ", "5")))
        End If
        If bTermScont2 Then
          nTermScont2Pos = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont2Pos", "6", " ", "6")))
        End If
        If bTermScont3 Then
          nTermScont3Pos = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont3Pos", "7", " ", "7")))
        End If
        If bTermScont4 Then
          nTermScont4Pos = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont4Pos", "8", " ", "8")))
        End If
        If bTermScont5 Then
          nTermScont5Pos = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont5Pos", "9", " ", "9")))
        End If
        If bTermScont6 Then
          nTermScont6Pos = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont6Pos", "10", " ", "10")))
        End If
        If bTermDatcons Then
          nTermDatconsPos = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDatconsPos", "2", " ", "2")))
        End If
        nTermCodartStart = 0
        nTermCodartStop = 0
        nTermDescrStart = 0
        nTermDescrStop = 0
        nTermDesintStart = 0
        nTermDesintStop = 0
        nTermQuantStart = 0
        nTermQuantStop = 0
        nTermPrezziStart = 0
        nTermPrezziStop = 0
        nTermScont1Start = 0
        nTermScont2Start = 0
        nTermScont3Start = 0
        nTermScont4Start = 0
        nTermScont5Start = 0
        nTermScont6Start = 0
        nTermScont1Stop = 0
        nTermScont2Stop = 0
        nTermScont3Stop = 0
        nTermScont4Stop = 0
        nTermScont5Stop = 0
        nTermScont6Stop = 0
        nTermDatconsStart = 0
        nTermDatconsStop = 0
      Else
        nTermCodartPos = 0
        nTermDescrPos = 0
        nTermDesintPos = 0
        nTermQuantPos = 0
        nTermPrezziPos = 0
        nTermScont1Pos = 0
        nTermScont2Pos = 0
        nTermScont3Pos = 0
        nTermScont4Pos = 0
        nTermScont5Pos = 0
        nTermScont6Pos = 0
        nTermDatconsPos = 0
        nTermCodartStart = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermCodartStart", "1", " ", "1")))
        nTermCodartStop = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermCodartStop", "13", " ", "13"))) '13 char
        If bTermDescr Then
          nTermDescrStart = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDescrStart", "14", " ", "14")))
          nTermDescrStop = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDescrStop", "54", " ", "54"))) '40 char
        End If
        If bTermDesint Then
          nTermDesintStart = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDesintStart", "55", " ", "55")))
          nTermDesintStop = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDesintStop", "95", " ", "95"))) '40 char
        End If
        If bTermQuant Then
          nTermQuantStart = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermQuantStart", "96", " ", "96")))
          nTermQuantStop = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermQuantStop", "102", " ", "102"))) '6 char
        End If
        If bTermPrezzi Then
          nTermPrezziStart = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermPrezziStart", "103", " ", "103")))
          nTermPrezziStop = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermPrezziStop", "113", " ", "113"))) '10 char
        End If
        If bTermScont1 Then
          nTermScont1Start = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont1Start", "114", " ", "114")))
          nTermScont1Stop = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont1Stop", "124", " ", "124"))) '10 char
        End If
        If bTermScont2 Then
          nTermScont2Start = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont2Start", "125", " ", "125")))
          nTermScont2Stop = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont2Stop", "135", " ", "135"))) '10 char
        End If
        If bTermScont3 Then
          nTermScont3Start = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont3Start", "136", " ", "136")))
          nTermScont3Stop = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont3Stop", "146", " ", "146"))) '10 char
        End If
        If bTermScont4 Then
          nTermScont4Start = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont4Start", "147", " ", "147")))
          nTermScont4Stop = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont4Stop", "157", " ", "157"))) '10 char
        End If
        If bTermScont5 Then
          nTermScont5Start = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont5Start", "158", " ", "158")))
          nTermScont5Stop = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont5Stop", "168", " ", "168"))) '10 char
        End If
        If bTermScont6 Then
          nTermScont6Start = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont6Start", "169", " ", "169")))
          nTermScont6Stop = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermScont6Stop", "179", " ", "179"))) '10 char
        End If
        If bTermDatcons Then
          nTermDatconsStart = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDatconsStart", "180", " ", "180")))
          nTermDatconsStop = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDatconsStop", "190", " ", "190"))) '10 char
        End If
      End If
      If bTermQuant Then
        nTermDivQuant = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDivQuant", "1", " ", "1")))
      End If
      If bTermPrezzi Then
        nTermDivPrezzi = NTSCInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDivPrezzi", "1", " ", "1")))
      End If
      If bTermScont1 Then
        nTermDivScont1 = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDivScont1", "1", " ", "1")))
      End If
      If bTermScont2 Then
        nTermDivScont2 = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDivScont2", "1", " ", "1")))
      End If
      If bTermScont3 Then
        nTermDivScont3 = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDivScont3", "1", " ", "1")))
      End If
      If bTermScont4 Then
        nTermDivScont4 = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDivScont4", "1", " ", "1")))
      End If
      If bTermScont5 Then
        nTermDivScont5 = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDivScont5", "1", " ", "1")))
      End If
      If bTermScont6 Then
        nTermDivScont6 = CInt(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDivScont6", "1", " ", "1")))
      End If
      bTermBarcodeConfez = False
      If strTermTipoArt = "B" Then
        bTermBarcodeConfez = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermBarcodeConfez", "0", " ", "0")))
      End If
      bTermProponiUmSecDaArticolo = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermProponiUmSecDaArticolo", "0", " ", "0")))

      'Export 
      strExport = oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "Export01", "0", " ", "0").Split("|"c)
      If strExport.Length > 15 Then
        bTermExecuteE = CBool(strExport(0))
        strTermCommandE = ""
        bTermSilentE = False
        If bTermExecuteE Then
          strTermCommandE = strExport(1)
          bTermSilentE = CBool(strExport(2))
        End If
        strTermFileNameE = strExport(3)
        bGeneraTracE = CBool(strExport(4))
        bTermShowLogErrE = CBool(strExport(5))
        strTermFileTypeE = UCase$(strExport(6))
        If strTermFileTypeE <> "F" And strTermFileTypeE <> "S" And strTermFileTypeE <> "C" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129247189413906250, "Valore della proprietà '|" & strTermPrgOwner & "|\Terminale\TermFileTypeE' (|" & strTermFileType & "|) non corretto, verrà utilizzato 'F'.")))
          strTermFileTypeE = "F"
        End If
        strTermFieldSepE = ""
        If strTermFileTypeE = "S" Then
          strTermFieldSepE = Microsoft.VisualBasic.Left(strExport(7), 1)
        End If
        bMatricE = CBool(strExport(8))
        lMatricE = NTSCInt(strExport(9))
        lQtaMatrE = NTSCInt(strExport(10))
        bTagliaE = CBool(strExport(11))
        lTagliaE = NTSCInt(strExport(12))
        lQtaTagliaE = NTSCInt(strExport(13))
        bTipoRigaE = CBool(strExport(14))
        If strExport.Length > 16 Then
          bRimuoviVbcrlf = CBool(strExport(15))
          strRighe = strExport(16).Split("$"c)
        Else
          bRimuoviVbcrlf = True
          strRighe = strExport(15).Split("$"c)
        End If
        'Righe Export
        'Separo le righe

        dttExport = New DataTable
        dttExport.Columns.Add("xx_nomee", GetType(String))
        dttExport.Columns.Add("xx_pose", GetType(Integer))
        dttExport.Columns.Add("xx_lune", GetType(Integer))
        dttExport.Columns.Add("xx_dive", GetType(Integer))
        For z As Integer = 0 To strRighe.Length - 1
          ' per ogni riga separo le celle
          strCelle = strRighe(z).Split("-"c)
          dttExport.Rows.Add()
          dttExport.Rows(z)!xx_nomee = strCelle(0)
          dttExport.Rows(z)!xx_pose = strCelle(1)
          dttExport.Rows(z)!xx_lune = strCelle(2)
          dttExport.Rows(z)!xx_dive = strCelle(3)
        Next
      Else
        dttExport = New DataTable
        dttExport.Columns.Add("xx_nomee", GetType(String))
        dttExport.Columns.Add("xx_pose", GetType(Integer))
        dttExport.Columns.Add("xx_lune", GetType(Integer))
        dttExport.Columns.Add("xx_dive", GetType(Integer))
        strTermCommandE = ""
        strTermFileTypeE = "F"
        strTermFieldSepE = ""
      End If

      dttExport.Columns("xx_nomee").DefaultValue = "art"
      dttExport.Columns("xx_pose").DefaultValue = "0"
      dttExport.Columns("xx_lune").DefaultValue = "1"
      dttExport.Columns("xx_dive").DefaultValue = "1"

      'Import
      strImport = oCldDocu.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "Import01", "0", " ", "0").Split("|"c)
      If strImport.Length > 20 Then
        bTermExecuteI = CBool(strImport(0))
        strTermCommandI = ""
        bTermSilentI = False
        If bTermExecuteI Then
          strTermCommandI = strImport(1)
          bTermSilentI = CBool(strImport(2))
        End If
        strTermFileNameI = strImport(3)
        bTermDeleteAfterI = CBool(strImport(4))
        bTermIgnoreFirstRowI = CBool(strImport(5))
        bTermRicalPrezI = CBool(strImport(6))
        bTermRicalScontI = CBool(strImport(7))
        bTermRicalProvvI = CBool(strImport(8))
        bTermShowLogErrI = CBool(strImport(9))
        strTermFileTypeI = UCase$(strImport(10))
        If strTermFileTypeI <> "F" And strTermFileTypeI <> "S" And strTermFileTypeI <> "C" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129247189436250000, "Valore della proprietà '|" & strTermPrgOwner & "|\Terminale\TermFileTypeI' (|" & strTermFileType & "|) non corretto, verrà utilizzato 'F'.")))
          strTermFileTypeI = "F"
        End If
        strTermFieldSepI = ""
        If strTermFileTypeI = "S" Then
          strTermFieldSepI = Microsoft.VisualBasic.Left(strImport(11), 1)
        End If
        strTermDecimalSepI = Microsoft.VisualBasic.Left(strImport(12), 1)
        If strTermDecimalSepI <> "," And strTermDecimalSepI <> "." Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129247189534375000, "Valore delle proprietà '|" & strTermPrgOwner & "|\Terminale\TermDecimalSepI' (|" & strTermDecimalSep & "|) non corretto, verrà utilizzato ','.")))
          strTermDecimalSepI = ","
        End If
        bMatricI = CBool(strImport(13))
        lMatricI = NTSCInt(strImport(14))
        lQtaMatrI = NTSCInt(strImport(15))
        bTagliaI = CBool(strImport(16))
        lTagliaI = NTSCInt(strImport(17))
        lQtaTagliaI = NTSCInt(strImport(18))
        bTipoRigaI = CBool(strImport(19))
        'Righe Import
        'Separo le righe
        strRighe = strImport(20).Split("$"c)
        dttImport = New DataTable
        dttImport.Columns.Add("xx_nomei", GetType(String))
        dttImport.Columns.Add("xx_posi", GetType(Integer))
        dttImport.Columns.Add("xx_luni", GetType(Integer))
        dttImport.Columns.Add("xx_divi", GetType(Integer))
        For z As Integer = 0 To strRighe.Length - 1
          ' per ogni riga separo le celle
          strCelle = strRighe(z).Split("-"c)
          dttImport.Rows.Add()
          dttImport.Rows(z)!xx_nomei = strCelle(0)
          dttImport.Rows(z)!xx_posi = strCelle(1)
          dttImport.Rows(z)!xx_luni = strCelle(2)
          dttImport.Rows(z)!xx_divi = strCelle(3)
        Next
      Else
        dttImport = New DataTable
        dttImport.Columns.Add("xx_nomei", GetType(String))
        dttImport.Columns.Add("xx_posi", GetType(Integer))
        dttImport.Columns.Add("xx_luni", GetType(Integer))
        dttImport.Columns.Add("xx_divi", GetType(Integer))
        strTermCommandI = ""
        strTermFileTypeI = "F"
        strTermFieldSepI = ""
        strTermDecimalSepI = ","
      End If

      dttImport.Columns("xx_nomei").DefaultValue = "art"
      dttImport.Columns("xx_posi").DefaultValue = "0"
      dttImport.Columns("xx_luni").DefaultValue = "1"
      dttImport.Columns("xx_divi").DefaultValue = "1"

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
  Public Overridable Function TerminaleLeggiImportaFile(Optional ByVal bSelOrdini As Boolean = False) As Boolean
    Dim dttTmp As New DataTable
    Dim strFile As String = ""
    Dim strLine() As String = Nothing
    Dim lRow As Integer = 0
    Dim lRowScart As Integer = 0
    Dim lRowOK As Integer = 0
    Dim strArt As String = ""
    Dim strDescr As String = ""
    Dim strDesint As String = ""
    Dim dQuant As Decimal = 0
    Dim dPrezzo As Decimal = 0
    Dim dScont1 As Decimal = 0
    Dim dScont2 As Decimal = 0
    Dim dScont3 As Decimal = 0
    Dim dScont4 As Decimal = 0
    Dim dScont5 As Decimal = 0
    Dim dScont6 As Decimal = 0
    Dim strDatcons As String = ""
    Dim strOrder As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Obbliga che sia presente l'articolo D
      '--------------------------------------------------------------------------------------------------------------
      oCldDocu.ValCodiceDb("D", strDittaCorrente, "ARTICO", "S", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571314396718750, "E' obbligatorio che sia codificato l'articolo 'D' (descrittivo) in anagrafica articoli.")))
        dttTmp.Clear()
        Return False
      End If
      dttTmp.Clear()
      '--------------------------------------------------------------------------------------------------------------
      strOrder = oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSVEHLBC", "OPZIONI", ".", "OrdineTrattamentoRigheOrdine", "DO", " ", "DO")  ' NON DOCUMENTARE
      '--------------------------------------------------------------------------------------------------------------
      If Not System.IO.File.Exists(strTermFileName) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571314914843750, "File da importare '|" & strTermFileName & "|' non trovato. Impossibile continuare.")))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Apre il file ASCII
      '--------------------------------------------------------------------------------------------------------------
      LogStart("BSMGDOCU", "Importazione file da terminale")
      Dim r1 As New System.IO.StreamReader(strTermFileName, System.Text.Encoding.Default)
      strFile = r1.ReadToEnd
      r1.Close()
      strLine = strFile.Replace(Chr(10), "").Split(Chr(13))
      For lRow = 0 To strLine.Length - 1
        If lRow = 0 And bTermIgnoreFirstRow Then
          lRowOK += 1
          GoTo NEXTROW
        End If
        If strLine(lRow).Trim = "" Then
          lRowScart += 1
          GoTo NEXTROW
        End If
        dQuant = 0
        dPrezzo = 0
        dScont1 = 0
        dScont2 = 0
        dScont3 = 0
        dScont4 = 0
        dScont5 = 0
        dScont6 = 0
        If bSelOrdini = False Then
          'inserisco righe in bsveboll/bsorgsor
          If TerminaleLeggiSplitRow(lRow, strLine(lRow), strArt, dQuant, dPrezzo, NTSCInt(dttET.Rows(0)!et_valuta), strDescr, dScont1, dScont2, dScont3, dScont4, dScont5, dScont6, strDesint, strDatcons) Then
            If TerminaleLeggiMakeRow(strArt, strDescr, dQuant, dPrezzo, dScont1, dScont2, dScont3, dScont4, dScont5, dScont6, strDesint, strDatcons) Then lRowOK += 1
          End If
        Else
          'seleziono righe in bnmgdocu.frmvehlbc
          If TerminaleLeggiSplitRow(lRow, strLine(lRow), strArt, dQuant, dPrezzo, nHlbcValuta, strDescr, dScont1, dScont2, dScont3, dScont4, dScont5, dScont6, strDesint, strDatcons) Then
            If TerminaleProponiEvasioneOrdini(strArt, dQuant, strOrder) Then lRowOK += 1
          End If
        End If
NEXTROW:
      Next    'For i = 0 To strLine.Length - 1
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiude il log
      '--------------------------------------------------------------------------------------------------------------
      LogStop()
      '--------------------------------------------------------------------------------------------------------------
      '--- Se richiesto cancella il file
      '--------------------------------------------------------------------------------------------------------------
      If bTermDeleteAfter Then
        System.IO.File.Delete(strTermFileName)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If bTermIgnoreFirstRow Then
        lRow -= 1
        lRowOK -= 1
      End If
      '--------------------------------------------------------------------------------------------------------------
      lRow -= lRowScart
      '--------------------------------------------------------------------------------------------------------------
      '--- Segnala se ci sono stati dei problemi
      '--------------------------------------------------------------------------------------------------------------
      If lRow <> lRowOK Then
        If lRowOK = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571326373281250, "Nessuna riga importata, su |" & lRow & "| totali.")))
        Else
          ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 128571326541718750, "|" & lRowOK & "| righe importate correttamente, su |" & lRow & "| totali.")))
        End If
      Else
        If lRowOK = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571326802656250, "Nessuna riga importata.")))
        Else
          ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 128571326938750000, "|" & lRowOK & "| righe importate.")))
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      LogStop()
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function TerminaleLeggiSplitRow(ByVal lRow As Integer, ByVal strLine As String, _
    ByRef strArt As String, ByRef dQuant As Decimal, ByRef dPrezzo As Decimal, ByVal nValuta As Integer, _
    ByRef strDescr As String, ByRef dScont1 As Decimal, ByRef dScont2 As Decimal, ByRef dScont3 As Decimal, _
    ByRef dScont4 As Decimal, ByRef dScont5 As Decimal, ByRef dScont6 As Decimal, ByRef strDesint As String, _
    ByRef strDatcons As String) As Boolean
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim Y As Integer = 0
    Dim bTrov As Boolean = False
    Try
      Select Case strTermFileType
        Case "F"
          '--------------------------
          'Caso FIXED
          strArt = Mid(strLine, nTermCodartStart, nTermCodartStop - nTermCodartStart + 1)
          strArt = RTrim(strArt)
          strDescr = ""
          If bTermDescr Then
            strDescr = Mid(strLine, nTermDescrStart, nTermDescrStop - nTermDescrStart + 1)
            strDescr = RTrim(strDescr)
          End If
          strDesint = ""
          If bTermDesint Then
            strDesint = Mid(strLine, nTermDesintStart, nTermDesintStop - nTermDesintStart + 1)
            strDesint = RTrim(strDesint)
          End If
          dQuant = 0
          If bTermQuant Then
            If strTermDecimalSep = "," Then
              If IsNumeric(Mid(strLine, nTermQuantStart, nTermQuantStop - nTermQuantStart + 1)) Then
                dQuant = NTSCDec(Mid(strLine, nTermQuantStart, nTermQuantStop - nTermQuantStart + 1))
              Else
                LogWrite(oApp.Tr(Me, 128571334703125000, "Il campo quatità non risulta essere numerico: '|" & _
                Mid(strLine, nTermQuantStart, nTermQuantStop - nTermQuantStart + 1) & "|' nella linea |" & lRow & _
                "| e verrà considerata quantità a zero: '|" & strLine & "|'."), True)
                dQuant = 0
              End If
            Else
              'punto come separatore
              dQuant = NTSCDec(Mid(strLine, nTermQuantStart, nTermQuantStop - nTermQuantStart + 1).Replace(".", ","))
            End If
          End If
          dPrezzo = 0
          If bTermPrezzi Then
            If strTermDecimalSep = "," Then
              If IsNumeric(Mid(strLine, nTermPrezziStart, nTermPrezziStop - nTermPrezziStart + 1)) Then
                dPrezzo = NTSCDec(Mid(strLine, nTermPrezziStart, nTermPrezziStop - nTermPrezziStart + 1))
              Else
                LogWrite(oApp.Tr(Me, 128571335748125000, "Il campo prezzo non risulta essere numerico: '|" & _
                Mid(strLine, nTermPrezziStart, nTermPrezziStop - nTermPrezziStart + 1) & _
                "|' nella linea |" & lRow & "| e verrà considerato prezzo a zero: '|" & strLine & "|'."), True)
                dPrezzo = 0
              End If
            Else
              'punto come separatore
              dPrezzo = NTSCDec(Mid(strLine, nTermPrezziStart, nTermPrezziStop - nTermPrezziStart + 1).Replace(".", ","))
            End If
          End If
          dScont1 = 0
          If bTermScont1 Then
            If strTermDecimalSep = "," Then
              If IsNumeric(Mid(strLine, nTermScont1Start, nTermScont1Stop - nTermScont1Start + 1)) Then
                dScont1 = NTSCDec(Mid(strLine, nTermScont1Start, nTermScont1Stop - nTermScont1Start + 1))
              Else
                LogWrite(oApp.Tr(Me, 128775244255926000, "Il campo sconto 1 non risulta essere numerico: '|" & _
                Mid(strLine, nTermScont1Start, nTermScont1Stop - nTermScont1Start + 1) & _
                "|' nella linea |" & lRow & "| e verrà considerato sconto 1 a zero: '|" & strLine & "|'."), True)
                dScont1 = 0
              End If
            Else
              'punto come separatore
              dScont1 = NTSCDec(Mid(strLine, nTermScont1Start, nTermScont1Stop - nTermScont1Start + 1).Replace(".", ","))
            End If
          End If
          dScont2 = 0
          If bTermScont2 Then
            If strTermDecimalSep = "," Then
              If IsNumeric(Mid(strLine, nTermScont2Start, nTermScont2Stop - nTermScont2Start + 1)) Then
                dScont2 = NTSCDec(Mid(strLine, nTermScont2Start, nTermScont2Stop - nTermScont2Start + 1))
              Else
                LogWrite(oApp.Tr(Me, 128775244230966000, _
                "Il campo sconto 2 non risulta essere numerico: '|" _
                & Mid(strLine, nTermScont2Start, nTermScont2Stop - nTermScont2Start + 1) & _
                "|' nella linea |" & lRow & "| e verrà considerato sconto 2 a zero: '|" & strLine & "|'."), True)
                dScont2 = 0
              End If
            Else
              'punto come separatore
              dScont2 = NTSCDec(Mid(strLine, nTermScont2Start, nTermScont2Stop - nTermScont2Start + 1).Replace(".", ","))
            End If
          End If
          dScont3 = 0
          If bTermScont3 Then
            If strTermDecimalSep = "," Then
              If IsNumeric(Mid(strLine, nTermScont3Start, nTermScont3Stop - nTermScont3Start + 1)) Then
                dScont3 = NTSCDec(Mid(strLine, nTermScont3Start, nTermScont3Stop - nTermScont3Start + 1))
              Else
                LogWrite(oApp.Tr(Me, 128775244212402000, "Il campo sconto 3 non risulta essere numerico: '|" & _
                Mid(strLine, nTermScont3Start, nTermScont3Stop - nTermScont3Start + 1) & _
                "|' nella linea |" & lRow & "| e verrà considerato sconto 3 a zero: '|" & strLine & "|'."), True)
                dScont3 = 0
              End If
            Else
              'punto come separatore
              dScont3 = NTSCDec(Mid(strLine, nTermScont3Start, nTermScont3Stop - nTermScont3Start + 1).Replace(".", ","))
            End If
          End If
          dScont4 = 0
          If bTermScont4 Then
            If strTermDecimalSep = "," Then
              If IsNumeric(Mid(strLine, nTermScont4Start, nTermScont4Stop - nTermScont4Start + 1)) Then
                dScont4 = NTSCDec(Mid(strLine, nTermScont4Start, nTermScont4Stop - nTermScont4Start + 1))
              Else
                LogWrite(oApp.Tr(Me, 128775244185102000, "Il campo sconto 4 non risulta essere numerico: '|" & _
                Mid(strLine, nTermScont4Start, nTermScont4Stop - nTermScont4Start + 1) & _
                "|' nella linea |" & lRow & "| e verrà considerato sconto 4 a zero: '|" & strLine & "|'."), True)
                dScont4 = 0
              End If
            Else
              'punto come separatore
              dScont4 = NTSCDec(Mid(strLine, nTermScont4Start, nTermScont4Stop - nTermScont4Start + 1).Replace(".", ","))
            End If
          End If
          dScont5 = 0
          If bTermScont5 Then
            If strTermDecimalSep = "," Then
              If IsNumeric(Mid(strLine, nTermScont5Start, nTermScont5Stop - nTermScont5Start + 1)) Then
                dScont5 = NTSCDec(Mid(strLine, nTermScont5Start, nTermScont5Stop - nTermScont5Start + 1))
              Else
                LogWrite(oApp.Tr(Me, 128775244163106000, "Il campo sconto 5 non risulta essere numerico: '|" & _
                Mid(strLine, nTermScont5Start, nTermScont5Stop - nTermScont5Start + 1) & _
                "|' nella linea |" & lRow & "| e verrà considerato sconto 5 a zero: '|" & strLine & "|'."), True)
                dScont5 = 0
              End If
            Else
              'punto come separatore
              dScont5 = NTSCDec(Mid(strLine, nTermScont5Start, nTermScont5Stop - nTermScont5Start + 1).Replace(".", ","))
            End If
          End If
          dScont6 = 0
          If bTermScont6 Then
            If strTermDecimalSep = "," Then
              If IsNumeric(Mid(strLine, nTermScont6Start, nTermScont6Stop - nTermScont6Start + 1)) Then
                dScont6 = NTSCDec(Mid(strLine, nTermScont6Start, nTermScont6Stop - nTermScont6Start + 1))
              Else
                LogWrite(oApp.Tr(Me, 128775244146882000, "Il campo sconto 6 non risulta essere numerico: '|" & _
                Mid(strLine, nTermScont6Start, nTermScont6Stop - nTermScont6Start + 1) & _
                "|' nella linea |" & lRow & "| e verrà considerato sconto 6 a zero: '|" & strLine & "|'."), True)
                dScont6 = 0
              End If
            Else
              'punto come separatore
              dScont6 = NTSCDec(Mid(strLine, nTermScont6Start, nTermScont6Stop - nTermScont6Start + 1).Replace(".", ","))
            End If
          End If
          strDatcons = ""
          If bTermDatcons Then
            strDatcons = Mid(strLine, nTermDatconsStart, nTermDatconsStop - nTermDatconsStart + 1)
            strDatcons = RTrim(strDatcons)
          End If
        Case "S"
          '-----------------------
          'Caso SEPARATORE
          '1)articolo
          j = 0 : Y = 0
          bTrov = False
          strLine &= strTermFieldSep 'Serve nel caso la riga abbia solo il codice articolo\barcode senza altri campi e senza separatori.
          i = InStr(Y + 1, strLine, strTermFieldSep)
          Do While i <> 0
            j = j + 1
            If j = nTermCodartPos Then
              'trovato
              bTrov = True
              strArt = Mid(strLine, Y + 1, i - (Y + 1))
              Exit Do
            End If
            Y = i
            i = InStr(Y + 1, strLine, strTermFieldSep)
            '---------------------
            'Ultimo campo
            If i = 0 Then
              j = j + 1
              If j = nTermCodartPos Then
                'trovato
                bTrov = True
                strArt = Mid(strLine, Y + 1)
                Exit Do
              End If
            End If
            '---------------------
          Loop
          If Not bTrov Then
            LogWrite(oApp.Tr(Me, 128571335681875000, "Codice articolo non trovato nella posizione |" & _
            nTermCodartPos & "|; la riga verrà scartata. '|" & strLine & "|'"), True)
            Return False
          End If
          strArt = RTrim(strArt)
          '2)descrizione
          If bTermDescr = True Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermDescrPos Then
                'trovato
                bTrov = True
                strDescr = Mid(strLine, Y + 1, i - (Y + 1))
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '---------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermDescrPos Then
                  'trovato
                  bTrov = True
                  strDescr = Mid(strLine, Y + 1)
                  Exit Do
                End If
              End If
              '---------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 128571335598750000, "Descrizione articolo non trovata nella posizione |" & nTermDescrPos & _
              "|; la riga verrà scartata. '|" & strLine & "|'"), True)
              Return False
            End If
            strDescr = RTrim(strDescr)
          End If    'If bTermDescr = True Then
          '3)descrizione interna
          If bTermDesint = True Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermDesintPos Then
                'trovato
                bTrov = True
                strDesint = Mid(strLine, Y + 1, i - (Y + 1))
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '---------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermDesintPos Then
                  'trovato
                  bTrov = True
                  strDesint = Mid(strLine, Y + 1)
                  Exit Do
                End If
              End If
              '---------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 129038082151254762, "Descrizione interna articolo non trovata nella posizione |" & nTermDescrPos & _
              "|; la riga verrà scartata. '|" & strLine & "|'"), True)
              Return False
            End If
            strDesint = RTrim(strDesint)
          End If    'If bTermDescr = True Then
          '4)quantità
          dQuant = 0
          If bTermQuant Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermQuantPos Then
                'trovato
                bTrov = True
                If strTermDecimalSep = "," Then
                  If IsNumeric(Mid(strLine, Y + 1, i - (Y + 1))) Then
                    dQuant = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)))
                  Else
                    LogWrite(oApp.Tr(Me, 128571335525156250, "Il campo quantità non risulta essere numerico: '|" & _
                    Mid(strLine, Y + 1, i - (Y + 1)) & "|' nella linea |" & lRow & _
                    "| e verrà considerata quantità a zero: '|" & strLine & "|'."), True)
                    dQuant = 0
                  End If
                Else
                  'punto come separatore
                  dQuant = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)).Replace(".", ","))
                End If
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '---------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermQuantPos Then
                  'trovato
                  bTrov = True
                  If strTermDecimalSep = "," Then
                    If IsNumeric(Mid(strLine, Y + 1)) Then
                      dQuant = NTSCDec(Mid(strLine, Y + 1))
                    Else
                      LogWrite(oApp.Tr(Me, 128571335407812500, _
                      "Il campo quantità non risulta essere numerico: '|" & Mid(strLine, Y + 1) & "|' nella linea |" & _
                      lRow & "| e verrà considerata quantità a zero: '|" & strLine & "|'."), True)
                      dQuant = 0
                    End If
                  Else
                    'punto come separatore
                    dQuant = NTSCDec(Mid(strLine, Y + 1).Replace(".", ","))
                  End If
                  Exit Do
                End If
              End If
              '-----------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 128571335354062500, "Quantità non trovato nella posizione |" & nTermQuantPos & _
              "|; verà utilizata quantità a zero. '|" & strLine & "|'"), True)
            End If
          End If
          '5)prezzi
          dPrezzo = 0
          If bTermPrezzi Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermPrezziPos Then
                'trovato
                bTrov = True
                If strTermDecimalSep = "," Then
                  If IsNumeric(Mid(strLine, Y + 1, i - (Y + 1))) Then
                    dPrezzo = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)))
                  Else
                    LogWrite(oApp.Tr(Me, 128571335300625000, "Il campo prezzo non risulta essere numerico: '|" & _
                    Mid(strLine, Y + 1, i - (Y + 1)) & "|' nella linea |" & lRow & _
                    "| e verrà considerato prezzo a zero: '|" & strLine & "|'."), True)
                    dPrezzo = 0
                  End If
                Else
                  'punto come separatore
                  dPrezzo = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)).Replace(".", ","))
                End If
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '------------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermPrezziPos Then
                  'trovato
                  bTrov = True
                  If strTermDecimalSep = "," Then
                    If IsNumeric(Mid(strLine, Y + 1)) Then
                      dPrezzo = NTSCDec(Mid(strLine, Y + 1))
                    Else
                      LogWrite(oApp.Tr(Me, 128571335221718750, "Il campo prezzo non risulta essere numerico: '|" & _
                      Mid(strLine, Y + 1) & "|' nella linea |" & lRow & "| e verrà considerato prezzo a zero: '|" & _
                      strLine & "|'."), True)
                      dPrezzo = 0
                    End If
                  Else
                    'punto come separatore
                    dPrezzo = NTSCDec(Mid(strLine, Y + 1).Replace(".", ","))
                  End If
                  Exit Do
                End If
              End If
              '-------------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 128775252211804000, "Prezzo non trovato nella posizione |" & nTermPrezziPos & _
              "|; verrà utilizzato prezzo a zero. '|" & strLine & "|'"), True)
            End If
          End If
          '6)sconto 1
          dScont1 = 0
          If bTermScont1 Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermScont1Pos Then
                'trovato
                bTrov = True
                If strTermDecimalSep = "," Then
                  If IsNumeric(Mid(strLine, Y + 1, i - (Y + 1))) Then
                    dScont1 = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)))
                  Else
                    LogWrite(oApp.Tr(Me, 128775252223972000, "Il campo sconto 1 non risulta essere numerico: '|" & _
                    Mid(strLine, Y + 1, i - (Y + 1)) & "|' nella linea |" & lRow & _
                    "| e verrà considerato sconto 1 a zero: '|" & strLine & "|'."), True)
                    dScont1 = 0
                  End If
                Else
                  'punto come separatore
                  dScont1 = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)).Replace(".", ","))
                End If
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '-----------------------------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermScont1Pos Then
                  'trovato
                  bTrov = True
                  If strTermDecimalSep = "," Then
                    If IsNumeric(Mid(strLine, Y + 1)) Then
                      dScont1 = NTSCDec(Mid(strLine, Y + 1))
                    Else
                      LogWrite(oApp.Tr(Me, 128775252235048000, "Il campo sconto 1 non risulta essere numerico: '|" & _
                      Mid(strLine, Y + 1) & "|' nella linea |" & lRow & "| e verrà considerato sconto 1 a zero: '|" & _
                      strLine & "|'."), True)
                      dScont1 = 0
                    End If
                  Else
                    'punto come separatore
                    dScont1 = NTSCDec(Mid(strLine, Y + 1).Replace(".", ","))
                  End If
                  Exit Do
                End If
              End If
              '-----------------------------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 128775252246592000, "Sconto 1 non trovato nella posizione |" & nTermScont1Pos & _
              "|; verrà utilizzato sconto 1 a zero. '|" & strLine & "|'"), True)
            End If
          End If
          '7)sconto 2
          dScont2 = 0
          If bTermScont2 Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermScont2Pos Then
                'trovato
                bTrov = True
                If strTermDecimalSep = "," Then
                  If IsNumeric(Mid(strLine, Y + 1, i - (Y + 1))) Then
                    dScont2 = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)))
                  Else
                    LogWrite(oApp.Tr(Me, 128775252257668000, "Il campo sconto 2 non risulta essere numerico: '|" & _
                    Mid(strLine, Y + 1, i - (Y + 1)) & "|' nella linea |" & lRow & _
                    "| e verrà considerato sconto 2 a zero: '|" & strLine & "|'."), True)
                    dScont2 = 0
                  End If
                Else
                  'punto come separatore
                  dScont2 = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)).Replace(".", ","))
                End If
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '-----------------------------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermScont2Pos Then
                  'trovato
                  bTrov = True
                  If strTermDecimalSep = "," Then
                    If IsNumeric(Mid(strLine, Y + 1)) Then
                      dScont2 = NTSCDec(Mid(strLine, Y + 1))
                    Else
                      LogWrite(oApp.Tr(Me, 128775252268900000, "Il campo sconto 2 non risulta essere numerico: '|" & _
                      Mid(strLine, Y + 1) & "|' nella linea |" & lRow & "| e verrà considerato sconto 2 a zero: '|" & _
                      strLine & "|'."), True)
                      dScont2 = 0
                    End If
                  Else
                    'punto come separatore
                    dScont2 = NTSCDec(Mid(strLine, Y + 1).Replace(".", ","))
                  End If
                  Exit Do
                End If
              End If
              '-----------------------------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 128775252280600000, "Sconto 2 non trovato nella posizione |" & nTermScont2Pos & _
              "|; verrà utilizzato sconto 2 a zero. '|" & strLine & "|'"), True)
            End If
          End If
          '8)sconto 3
          dScont3 = 0
          If bTermScont3 Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermScont3Pos Then
                'trovato
                bTrov = True
                If strTermDecimalSep = "," Then
                  If IsNumeric(Mid(strLine, Y + 1, i - (Y + 1))) Then
                    dScont3 = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)))
                  Else
                    LogWrite(oApp.Tr(Me, 128775252291832000, "Il campo sconto 3 non risulta essere numerico: '|" & _
                    Mid(strLine, Y + 1, i - (Y + 1)) & "|' nella linea |" & lRow & _
                    "| e verrà considerato sconto 3 a zero: '|" & strLine & "|'."), True)
                    dScont3 = 0
                  End If
                Else
                  'punto come separatore
                  dScont3 = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)).Replace(".", ","))
                End If
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '-----------------------------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermScont3Pos Then
                  'trovato
                  bTrov = True
                  If strTermDecimalSep = "," Then
                    If IsNumeric(Mid(strLine, Y + 1)) Then
                      dScont3 = NTSCDec(Mid(strLine, Y + 1))
                    Else
                      LogWrite(oApp.Tr(Me, 128775252302440000, "Il campo sconto 3 non risulta essere numerico: '|" & _
                      Mid(strLine, Y + 1) & "|' nella linea |" & lRow & "| e verrà considerato sconto 3 a zero: '|" & _
                      strLine & "|'."), True)
                      dScont3 = 0
                    End If
                  Else
                    'punto come separatore
                    dScont3 = NTSCDec(Mid(strLine, Y + 1).Replace(".", ","))
                  End If
                  Exit Do
                End If
              End If
              '-----------------------------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 128775252313984000, "Sconto 3 non trovato nella posizione |" & nTermScont3Pos & _
              "|; verrà utilizzato sconto 3 a zero. '|" & strLine & "|'"), True)
            End If
          End If
          '9)sconto 4
          dScont4 = 0
          If bTermScont4 Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermScont4Pos Then
                'trovato
                bTrov = True
                If strTermDecimalSep = "," Then
                  If IsNumeric(Mid(strLine, Y + 1, i - (Y + 1))) Then
                    dScont4 = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)))
                  Else
                    LogWrite(oApp.Tr(Me, 128775252325996000, "Il campo sconto 4 non risulta essere numerico: '|" & _
                    Mid(strLine, Y + 1, i - (Y + 1)) & "|' nella linea |" & lRow & _
                    "| e verrà considerato sconto 4 a zero: '|" & strLine & "|'."), True)
                    dScont4 = 0
                  End If
                Else
                  'punto come separatore
                  dScont4 = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)).Replace(".", ","))
                End If
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '-----------------------------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermScont4Pos Then
                  'trovato
                  bTrov = True
                  If strTermDecimalSep = "," Then
                    If IsNumeric(Mid(strLine, Y + 1)) Then
                      dScont4 = NTSCDec(Mid(strLine, Y + 1))
                    Else
                      LogWrite(oApp.Tr(Me, 128775252336916000, "Il campo sconto 4 non risulta essere numerico: '|" & _
                      Mid(strLine, Y + 1) & "|' nella linea |" & lRow & _
                      "| e verrà considerato sconto 4 a zero: '|" & strLine & "|'."), True)
                      dScont4 = 0
                    End If
                  Else
                    'punto come separatore
                    dScont4 = NTSCDec(Mid(strLine, Y + 1).Replace(".", ","))
                  End If
                  Exit Do
                End If
              End If
              '-----------------------------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 128775252348148000, "Sconto 4 non trovato nella posizione |" & nTermScont4Pos & _
              "|; verrà utilizzato sconto 4 a zero. '|" & strLine & "|'"), True)
            End If
          End If
          '10)sconto 5
          dScont5 = 0
          If bTermScont5 Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermScont5Pos Then
                'trovato
                bTrov = True
                If strTermDecimalSep = "," Then
                  If IsNumeric(Mid(strLine, Y + 1, i - (Y + 1))) Then
                    dScont5 = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)))
                  Else
                    LogWrite(oApp.Tr(Me, 128775252359380000, "Il campo sconto 5 non risulta essere numerico: '|" & _
                    Mid(strLine, Y + 1, i - (Y + 1)) & "|' nella linea |" & lRow & _
                    "| e verrà considerato sconto 5 a zero: '|" & strLine & "|'."), True)
                    dScont5 = 0
                  End If
                Else
                  'punto come separatore
                  dScont5 = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)).Replace(".", ","))
                End If
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '-----------------------------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermScont5Pos Then
                  'trovato
                  bTrov = True
                  If strTermDecimalSep = "," Then
                    If IsNumeric(Mid(strLine, Y + 1)) Then
                      dScont5 = NTSCDec(Mid(strLine, Y + 1))
                    Else
                      LogWrite(oApp.Tr(Me, 128775252370300000, "Il campo sconto 5 non risulta essere numerico: '|" & _
                      Mid(strLine, Y + 1) & "|' nella linea |" & lRow & "| e verrà considerato sconto 5 a zero: '|" & _
                      strLine & "|'."), True)
                      dScont5 = 0
                    End If
                  Else
                    'punto come separatore
                    dScont5 = NTSCDec(Mid(strLine, Y + 1).Replace(".", ","))
                  End If
                  Exit Do
                End If
              End If
              '-----------------------------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 128775252382000000, "Sconto 5 non trovato nella posizione |" & _
              nTermScont5Pos & "|; verrà utilizzato sconto 5 a zero. '|" & strLine & "|'"), True)
            End If
          End If
          '11)sconto 6
          dScont6 = 0
          If bTermScont6 Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermScont6Pos Then
                'trovato
                bTrov = True
                If strTermDecimalSep = "," Then
                  If IsNumeric(Mid(strLine, Y + 1, i - (Y + 1))) Then
                    dScont6 = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)))
                  Else
                    LogWrite(oApp.Tr(Me, 128775252393700000, "Il campo sconto 6 non risulta essere numerico: '|" & _
                    Mid(strLine, Y + 1, i - (Y + 1)) & "|' nella linea |" & lRow & _
                    "| e verrà considerato sconto 6 a zero: '|" & strLine & "|'."), True)
                    dScont6 = 0
                  End If
                Else
                  'punto come separatore
                  dScont6 = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)).Replace(".", ","))
                End If
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '-----------------------------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermScont6Pos Then
                  'trovato
                  bTrov = True
                  If strTermDecimalSep = "," Then
                    If IsNumeric(Mid(strLine, Y + 1)) Then
                      dScont6 = NTSCDec(Mid(strLine, Y + 1))
                    Else
                      LogWrite(oApp.Tr(Me, 128775252404776000, "Il campo sconto 6 non risulta essere numerico: '|" & _
                      Mid(strLine, Y + 1) & "|' nella linea |" & lRow & "| e verrà considerato sconto 6 a zero: '|" & _
                      strLine & "|'."), True)
                      dScont6 = 0
                    End If
                  Else
                    'punto come separatore
                    dScont6 = NTSCDec(Mid(strLine, Y + 1).Replace(".", ","))
                  End If
                  Exit Do
                End If
              End If
              '-----------------------------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 128775252196048000, "Sconto 6 non trovato nella posizione |" & nTermScont6Pos & _
              "|; verrà utilizzato sconto 6 a zero. '|" & strLine & "|'"), True)
            End If
          End If
          '12)descrizione interna
          If bTermDatcons = True Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermDatconsPos Then
                'trovato
                bTrov = True
                strDatcons = Mid(strLine, Y + 1, i - (Y + 1))
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '---------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermDatconsPos Then
                  'trovato
                  bTrov = True
                  strDatcons = Mid(strLine, Y + 1)
                  Exit Do
                End If
              End If
              '---------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 129038082990317262, "Data consegna articolo non trovata nella posizione |" & _
              nTermDescrPos & "|; la riga verrà scartata. '|" & strLine & "|'"), True)
              Return False
            End If
            strDatcons = RTrim(strDatcons)
          End If    'If bTermDescr = True Then
      End Select
      'Ora applica i divisori
      If bTermQuant Then
        If nTermDivQuant > 1 And nTermDivQuant <= 10000 Then
          dQuant = ArrDbl(dQuant / nTermDivQuant, 3)
        End If
      End If
      If bTermPrezzi Then
        If nTermDivPrezzi > 1 And nTermDivPrezzi <= 10000 Then
          dPrezzo = ArrDbl(dPrezzo / nTermDivPrezzi, oCldDocu.TrovaNdecSuPrzUn(nValuta))
        End If
      End If
      If bTermScont1 Then
        If nTermDivScont1 > 1 And nTermDivScont1 <= 10000 Then
          dScont1 = ArrDbl(dScont1 / nTermDivScont1, 2)
        End If
      End If
      If bTermScont2 Then
        If nTermDivScont2 > 1 And nTermDivScont2 <= 10000 Then
          dScont2 = ArrDbl(dScont2 / nTermDivScont2, 2)
        End If
      End If
      If bTermScont3 Then
        If nTermDivScont3 > 1 And nTermDivScont3 <= 10000 Then
          dScont3 = ArrDbl(dScont3 / nTermDivScont3, 2)
        End If
      End If
      If bTermScont4 Then
        If nTermDivScont4 > 1 And nTermDivScont4 <= 10000 Then
          dScont4 = ArrDbl(dScont4 / nTermDivScont4, 2)
        End If
      End If
      If bTermScont5 Then
        If nTermDivScont5 > 1 And nTermDivScont5 <= 10000 Then
          dScont5 = ArrDbl(dScont5 / nTermDivScont5, 2)
        End If
      End If
      If bTermScont6 Then
        If nTermDivScont6 > 1 And nTermDivScont6 <= 10000 Then
          dScont6 = ArrDbl(dScont6 / nTermDivScont6, 2)
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
  Public Overridable Function TerminaleLeggiMakeRow(ByVal strArt As String, ByVal strDescr As String, _
    ByVal dQuant As Decimal, ByVal dPrezzo As Decimal, ByRef dScont1In As Decimal, ByRef dScont2In As Decimal, _
    ByRef dScont3In As Decimal, ByRef dScont4In As Decimal, ByRef dScont5In As Decimal, ByRef dScont6In As Decimal, _
    ByVal strDesint As String, ByVal strDatcons As String) As Boolean
    Dim strTmp As String = ""
    Dim dttTmp, dttTagl As New DataTable
    Dim bAddRow As Boolean = False
    Dim strUnmisBarcode As String = ""
    Dim strBarcode As String = ""
    Dim dBCQuant As Decimal = 0
    Dim nFase As Integer = 0
    Dim strArt1 As String = ""
    Dim strTaglia As String = ""
    Dim bDisabilitaMsgPrezzoZeroTmp As Boolean = bDisabilitaMsgPrezzoZero
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Verifico se l'articolo esiste, in base alla sua tipologia
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTermTipoArt
        Case "C", "X"
          'c = articolo
          'x = articolo o barcode o codarfo
          oCldDocu.ValCodiceDb(strArt, strDittaCorrente, "ARTICO", "S", "", dttTmp)
          If dttTmp.Rows.Count = 0 And strTermTipoArt = "X" Then
            'lo cerco come barcode
            oCldDocu.ValCodiceDb(strArt, strDittaCorrente, "BARCODE", "S", "", dttTmp)
            If dttTmp.Rows.Count > 0 Then
              strBarcode = NTSCStr(dttTmp.Rows(0)!bc_code)
              strUnmisBarcode = NTSCStr(dttTmp.Rows(0)!bc_unmis)
              dBCQuant = NTSCDec(dttTmp.Rows(0)!bc_quant)
              strArt = NTSCStr(dttTmp.Rows(0)!bc_codart)
              nFase = NTSCInt(dttTmp.Rows(0)!bc_fase)
              strTaglia = NTSCStr(dttTmp.Rows(0)!bc_tagl)
            End If
          End If
          If dttTmp.Rows.Count = 0 And strTermTipoArt = "X" Then
            'lo cerco come codarfo
            strArt1 = CType(oCleComm, CLELBMENU).TrovaCodartDaCodarfo(strArt, NTSCInt(dttET.Rows(0)!et_conto), strDittaCorrente, "")
            If strArt1 <> "" Then
              strArt = strArt1
              oCldDocu.ValCodiceDb(strArt, strDittaCorrente, "ARTICO", "S", "", dttTmp)
            End If
          End If
          If dttTmp.Rows.Count = 0 Then
            strDescr = Microsoft.VisualBasic.Left("Articolo " & strArt & " non trovato.", 40)
            strArt = "D"
          End If
        Case "B"
          oCldDocu.ValCodiceDb(strArt, strDittaCorrente, "BARCODE", "S", "", dttTmp)
          If dttTmp.Rows.Count = 0 Then
            strDescr = Microsoft.VisualBasic.Left("Barcode " & strArt & " non trovato.", 40)
            strArt = "D"
          Else
            strBarcode = NTSCStr(dttTmp.Rows(0)!bc_code)
            strUnmisBarcode = NTSCStr(dttTmp.Rows(0)!bc_unmis)
            dBCQuant = NTSCDec(dttTmp.Rows(0)!bc_quant)
            strArt = NTSCStr(dttTmp.Rows(0)!bc_codart)
            nFase = NTSCInt(dttTmp.Rows(0)!bc_fase)
            strTaglia = NTSCStr(dttTmp.Rows(0)!bc_tagl)
          End If
        Case "A"
          strTmp = CType(oCleComm, CLELBMENU).TrovaCodartDaCodarfo(strArt, NTSCInt(dttET.Rows(0)!et_conto), strDittaCorrente, "")
          If strTmp = "" Then
            strDescr = Microsoft.VisualBasic.Left("Art. C/F " & strArt & " non trovato.", 40)
            strArt = "D"
          Else
            strArt = strTmp
          End If
      End Select
      dttTmp.Clear()
      '--------------------------------------------------------------------------------------------------------------
      '--- Ora aggiunge la riga: non c'è bisogno di scriverla in BEORGSOR, 
      '--- visto che già da qui vengono eseguite anche le routine ereditate ...
      '--- (es BeforeColUpdate_CORPO_ec_codart di beorgsor)
      '--------------------------------------------------------------------------------------------------------------
      dttEC.Rows.Add(dttEC.NewRow)
      With dttEC.Rows(dttEC.Rows.Count - 1)
        '------------------------------------------------------------------------------------------------------------  
        '--- Forzo la MovordOnAddNewRow
        '------------------------------------------------------------------------------------------------------------
        !codditt = "."
        !codditt = strDittaCorrente
        If strBarcode <> "" Then !ec_matric = strBarcode
        !ec_codart = strArt
        If strTermTipoArt = "B" And nFase <> 0 Then !ec_fase = nFase
        If strDescr <> "" Then !ec_descr = strDescr.PadRight(40).Substring(0, 40).Trim
        If (bTermDesint = True) And (strDesint.Trim <> "") Then
          !ec_desint = strDesint.PadRight(40).Substring(0, 40).Trim
        End If
        If bTermQuant Then
          If bTermBarcodeConfez And strTermTipoArt = "B" And strUnmisBarcode <> "" Then
            '--------------------------------------------------------------------------------------------------------
            '--- Caso con opzione
            '--------------------------------------------------------------------------------------------------------
            !ec_unmis = strUnmisBarcode
          Else
            '--------------------------------------------------------------------------------------------------------
            '--- Caso standard
            '--------------------------------------------------------------------------------------------------------
            If Not (bTermProponiUmSecDaArticolo And strTermTipoArt <> "B") Then
              !ec_unmis = NTSCStr(!ec_ump)
            End If
          End If
          !ec_colli = ArrDbl(dQuant, 3)

          If strTermTipoArt = "B" Then !ec_quant = NTSCDec(!ec_colli) * dBCQuant

          If NTSCInt(!xxo_codtagl) <> 0 Then
            oCldDocu.ValCodiceDb(NTSCStr(!xxo_codtagl), strDittaCorrente, "TABTAGL", "N", , dttTagl)

            For z As Integer = 1 To 24
              If NTSCStr(dttTagl.Rows(0)("tb_dest" & z.ToString.PadLeft(2, "0"c))) = strTaglia Then
                !ec_quant = ArrDbl(dQuant, 3)
                dsShared.Tables("CORPOTC").Rows(dsShared.Tables("CORPOTC").Rows.Count - 1)("ec_quant" & z.ToString.PadLeft(2, "0"c)) = ArrDbl(dQuant, 3)
              End If
            Next
          End If
        Else
          !ec_quant = 1
          If NTSCInt(!xxo_codtagl) <> 0 Then
            oCldDocu.ValCodiceDb(NTSCStr(!xxo_codtagl), strDittaCorrente, "TABTAGL", "N", , dttTagl)

            For z As Integer = 1 To 24
              If NTSCStr(dttTagl.Rows(0)("tb_dest" & z.ToString.PadLeft(2, "0"c))) = strTaglia Then
                !ec_quant = 1
                dsShared.Tables("CORPOTC").Rows(dsShared.Tables("CORPOTC").Rows.Count - 1)("ec_quant" & z.ToString.PadLeft(2, "0"c)) = 1
              End If
            Next
          End If
        End If
        If Not (bTermProponiUmSecDaArticolo And strTermTipoArt <> "B") Then
          '----------------------------------------------------------------------------------------------------------
          '--- Caso standard
          '----------------------------------------------------------------------------------------------------------
          '   !ec_unmis = NTSCStr(!ec_ump)
          !ec_colli = ArrDbl(dQuant, 3)
        End If
        '------------------------------------------------------------------------------------------------------------
        If bTermPrezzi Then
          If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
            !ec_prezvalc = dPrezzo
          ElseIf NTSCStr(dttET.Rows(0)!et_scorpo) = "S" Then
            !ec_preziva = dPrezzo
          Else
            !ec_prezzo = dPrezzo
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Se almeno uno dei 6 sconti è indicato nel file ascii, setta tutti e 6 gli sconti
        '--- dal file ascii, altrimenti funziona come prima
        '------------------------------------------------------------------------------------------------------------
        If (bTermScont1 = True) Or (bTermScont2 = True) Or (bTermScont3 = True) Or _
           (bTermScont4 = True) Or (bTermScont5 = True) Or (bTermScont6 = True) Then
          !ec_scont1 = dScont1In
          !ec_scont2 = dScont2In
          !ec_scont3 = dScont3In
          !ec_scont4 = dScont4In
          !ec_scont5 = dScont5In
          !ec_scont6 = dScont6In
        End If
        '------------------------------------------------------------------------------------------------------------
        If (bTermDatcons = True) And (BORDINI = True) Then !ec_datcons = strDatcons
        '------------------------------------------------------------------------------------------------------------
        If Not CorpoCheckRiga(3, dttEC.Rows(dttEC.Rows.Count - 1)) Then
          .Delete()
          Return False
        End If
      End With
      '--------------------------------------------------------------------------------------------------------------
      bDisabilitaMsgPrezzoZero = True
      RecordSalva(dttEC.Rows.Count - 1, False, Nothing)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If bAddRow Then dttEC.Rows(dttEC.Rows.Count - 1).Delete()
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      bDisabilitaMsgPrezzoZero = bDisabilitaMsgPrezzoZeroTmp
      dttEC.AcceptChanges()
    End Try
  End Function
  Public Overridable Function TerminaleProponiEvasioneOrdini(ByVal strArt As String, ByVal dQuant As Decimal, ByVal strOrder As String) As Boolean
    'può essere chiamato solo da bnmgdocu.frmvehlbc
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Dim dttArti As New DataTable
    Dim bAddRow As Boolean = False
    Dim strUnmisBarcode As String = ""
    Dim dBCQuant As Decimal = 0
    Dim dColli As Decimal = 0
    Dim strUnmis As String = ""
    Dim strUMP As String = ""
    Dim strErr As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim bRigaNuova As Boolean = True
    Dim i As Integer = 0
    Dim dtrTrow As DataRow = Nothing
    Dim dQuadaeva As Decimal = 0
    Dim strUMDaPr As String = ""
    Dim strErrore As String = ""
    Try
      '------------------------------
      'verifico se l'articolo esiste, in base alla sua tipologia
      Select Case strTermTipoArt
        Case "C"
          oCldDocu.ValCodiceDb(strArt, strDittaCorrente, "ARTICO", "S", "", dttTmp)
          If dttTmp.Rows.Count = 0 Then
            LogWrite(oApp.Tr(Me, 128712914823437500, "Articolo |" & strArt & "| inesistente. Verrà scartato."), True)
            Return False
          End If
        Case "B"
          oCldDocu.ValCodiceDb(strArt, strDittaCorrente, "BARCODE", "S", "", dttTmp)
          If dttTmp.Rows.Count = 0 Then
            LogWrite(oApp.Tr(Me, 128712915357187500, "Barcode |" & strArt & "| non trovato. Verrà scartato."), True)
            Return False
          Else
            strUnmisBarcode = NTSCStr(dttTmp.Rows(0)!bc_unmis)
            dBCQuant = NTSCDec(dttTmp.Rows(0)!bc_quant)
            strArt = NTSCStr(dttTmp.Rows(0)!bc_codart)
          End If
        Case "A"
          strTmp = CType(oCleComm, CLELBMENU).TrovaCodartDaCodarfo(strArt, lHlbcConto, strDittaCorrente, "")
          If strTmp = "" Then
            LogWrite(oApp.Tr(Me, 128712916202500000, "Articolo cliente/fornitore |" & strArt & "| (conto |" & lHlbcConto & "|) non associato ad alcun articolo. Verrà scartato."), True)
            Return False
          Else
            strArt = strTmp
          End If
      End Select
      dttTmp.Clear()
      oCldDocu.ValCodiceDb(strArt, strDittaCorrente, "ARTICO", "S", "", dttArti)

      '-----------------------------
      'determino le quantità
      If bTermQuant Then
        If bTermBarcodeConfez And strTermTipoArt = "B" And strUnmisBarcode <> "" Then
          'caso con opzione
          dColli = ArrDbl(dQuant, 3)
          strUnmis = strUnmisBarcode
          strUMP = NTSCStr(dttArti.Rows(0)!ar_unmis)
          If Not CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, strArt, strUnmis, dColli, 1, 1, 1, dQuant, False, strErr, 3) Then
            If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
            'usa l'UMP, in caso di errore di conversione
            dQuant = dColli
            strUnmis = strUMP
          End If
        Else
          If bBarcodeDerogaQta And strTermTipoArt = "B" Then
            'il fattore di conversione è derogato sulla tabella barcode
            dColli = ArrDbl(dQuant, 3)
            strUnmis = strUnmisBarcode
            strUMP = NTSCStr(dttArti.Rows(0)!ar_unmis)
            'ATTENZIONE! deroga alla ConvQuantUMP, uso il fattore di moltiplicazione in BARCODE
            dQuant = ArrDbl(dColli * dBCQuant, 3)
          Else
            If bTermProponiUmSecDaArticolo And strTermTipoArt <> "B" Then
              'Legge UMDaPr
              Select Case dttET.Rows(0)!et_tipork.ToString
                'Se un documento di vendita
                Case "A", "B", "C", "D", "N", "W", "E", "F", "S", "I", "R", "Q", "X", "V", "£"
                  strUMDaPr = UCase(dttArti.Rows(0)!ar_umdapr.ToString)
                Case Else
                  strUMDaPr = UCase(dttArti.Rows(0)!ar_umdapra.ToString)
              End Select
              dColli = ArrDbl(dQuant, 3)
              strUMP = NTSCStr(dttArti.Rows(0)!ar_unmis)
              strUnmis = strUMP 'per default
              'Unità di misura da proporre
              Select Case strUMDaPr
                Case "S" 'secondaria
                  If NTSCStr(dttArti.Rows(0)!ar_unmis2).Trim <> "" Then strUnmis = dttArti.Rows(0)!ar_unmis2.ToString
                Case "C" 'confezione
                  If NTSCStr(dttArti.Rows(0)!ar_confez2).Trim <> "" Then strUnmis = dttArti.Rows(0)!ar_confez2.ToString
                Case "Q" 'formula
                  If NTSCStr(dttArti.Rows(0)!ar_um4).Trim <> "" Then strUnmis = dttArti.Rows(0)!ar_um4.ToString
              End Select
              If strUMP = strUnmis Then
                dQuant = dColli
              Else
                CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, strCodart, strUnmis, dColli, 1, 1, 1, dQuant, False, strErr, 3)
              End If
              '------------------------------------------------------------------------------------------
            Else
              'caso standard
              dQuant = ArrDbl(dQuant, 3)
              strUMP = NTSCStr(dttArti.Rows(0)!ar_unmis)
              dColli = dQuant
              strUnmis = strUMP
            End If
          End If
        End If
      Else
        dQuant = 1
      End If

      '-----------------------------
      'Ora posso cercare la riga in dsHlbcShared.tables("SELDOC")
      'A questo punto abbiamo strCodart a dQuant valorizzati e siamo pronti per assegnare gli
      'ordini
      'Acquisizione quantità
Ripeti1:
      Dim strOrderBy As String = ""
      Select Case strOrder
        Case "DC"  '--> data consegna
          strOrderBy = "xx_prefer DESC, mo_datcons ASC, xx_flevasdaeva DESC, mo_riga ASC"
        Case Else 'DO --> data ordine
          strOrderBy = "xx_prefer DESC, xx_datord ASC, xx_flevasdaeva DESC, mo_riga ASC"
      End Select

      dtrT = dsHlbcShared.Tables("SELDOC").Select("mo_codart = " & CStrSQL(strArt) & _
                                                  " AND xx_codpaga = " & nHlbcCodpaga & _
                                                  " AND xx_coddest = " & nHlbcCoddest, strOrderBy)
      If dtrT.Length = 0 Then dtrT = dsHlbcShared.Tables("SELDOC").Select("mo_codart = " & CStrSQL(strArt), strOrderBy)

      bRigaNuova = True
      i = 0
      If dtrT.Length > 0 Then
        'Identificazione riga da aggiornare
        Do While Not i = dtrT.Length
          If dtrT(i)!xx_flevasdaeva.ToString.ToUpper = "C" Or NTSCDate(dtrT(i)!xx_datord) = New Date(2099, 12, 31) Then
            dtrTrow = dtrT(i)
            dQuadaeva = NTSCDec(dtrTrow!xx_quadaeva)
            bRigaNuova = False
            Exit Do
          End If
          i += 1
        Loop    'Do While Not i = dtrT.Length

        'Controllo per articolo movimentabile
        If Not bRigaNuova Then
          If NTSCDate(dtrT(i)!xx_datord) <> New Date(2099, 12, 31) Then
            If NTSCInt(dtrTrow!xx_codpaga) <> nHlbcCodpaga Or NTSCInt(dtrTrow!xx_coddest) <> nHlbcCoddest Then
              LogWrite(oApp.Tr(Me, 129304108505573887, "Articolo: '|" & strArt & "|' non evadibile! Controlla condizione di pagamento e destinazione difformi dal documento corrente. Verrà scartato."), True)
              'TerminaleProponiEvasioneOrdini = True 'comunque procede
              Return False
            End If
          End If
          If (dQuadaeva + dQuant < NTSCDec(dtrTrow!xx_qtares)) Or (NTSCDate(dtrT(i)!xx_datord) = New Date(2099, 12, 31)) Then
            'Aggiornamento riga
            dtrTrow!xx_quadaeva = dQuadaeva + dQuant
            dtrTrow.AcceptChanges()
          Else
            'Aggiornamento riga
            dtrTrow!xx_quadaeva = NTSCDec(dtrTrow!xx_qtares)
            dtrTrow!xx_flevasdaeva = "S"
            dtrTrow.AcceptChanges()

            If ArrDbl(dQuant - (NTSCDec(dtrTrow!xx_qtares) - dQuadaeva), 3) > 0 Then
              'davide 05/05/2003: non fa una nuova riga ma va a vedere se ci sono
              'altre righe d'ordine da scalare
              'Riga nuova
              'RigaNuova snaTmp!bc_codart, ArrDbl(dQtaBarre - (dQtares - dQuadaeva), 3)
              dQuant = ArrDbl(dQuant - (NTSCDec(dtrTrow!xx_qtares) - dQuadaeva), 3)
              GoTo Ripeti1
            End If
          End If
        Else
          'Riga nuova
          HlbcRigaNuova(strArt, 0, NTSCStr(dttArti.Rows(0)!ar_descr), dQuant)
        End If ' brigaNUova
      Else
        'Riga nuova
        HlbcRigaNuova(strArt, 0, NTSCStr(dttArti.Rows(0)!ar_descr), dQuant)
      End If    'If dtrT.Length > 0 Then

      Return True

    Catch ex As Exception
      If bAddRow Then dttEC.Rows(dttEC.Rows.Count - 1).Delete()
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      dttTmp.Clear()
      dttArti.Clear()
      dsHlbcShared.Tables("SELDOC").AcceptChanges()
    End Try
  End Function

  Public Overridable Function EsportaRighe(ByVal strExport() As String, ByVal dtrRow() As DataRow, ByVal strChiamante As String) As Boolean
    Dim strRighe(), strCell(), strSep As String
    Dim sbResult As New Text.StringBuilder
    Dim swOut As IO.StreamWriter
    Dim bNoVbCrlf As Boolean
    Dim bFixed As Boolean
    Dim i As Integer
    Try
      strRighe = strExport(strExport.Length - 1).Split("$"c)
      bFixed = CBool(IIf(Microsoft.VisualBasic.Left(strExport(6), 1) = "S", False, True))
      strSep = strExport(7)

      If strExport.Length > 16 Then
        bNoVbCrlf = CBool(strExport(15))
      Else
        bNoVbCrlf = True
      End If

      LogStart("BSMGDOCU", "Esportazione righe")

      ' Deve scrivere il tracciato
      If CBool(strExport(4)) Then
        sbResult.Append("BUSTRACCIATO: " & strExport(6) & "|" & strExport(7) & "|" & strExport(8) & "|" & strExport(9) & "|" & strExport(10) & _
                        "|" & strExport(11) & "|" & strExport(12) & "|" & strExport(13) & "|" & strExport(14) & "|" & strExport(15) & "|")
        For i = 0 To strRighe.Length - 1
          sbResult.Append(strRighe(i) & "$")
        Next
        sbResult.Remove(sbResult.Length - 1, 1)
        sbResult.Append(vbCrLf)
      End If

      For z As Integer = 0 To dtrRow.Length - 1
        'Se si è scelto di esportare la matricola o la taglia aggiunge davanti il tipo rk (o se si è scelto di farzarlo a mano)
        If CBool(strExport(8)) Or CBool(strExport(11)) Or CBool(strExport(14)) Then
          sbResult.Append("1")
          If Not bFixed Then sbResult.Append(strSep)
        End If
        For i = 0 To strRighe.Length - 1 ' Vengono salvate già in ordine di posizione, quindi basta applicarle nell'ordine passato
          strCell = strRighe(i).Split("-"c)
          With dtrRow(z)
            Select Case strCell(0)
              Case "art" 'Articolo
                ElaboraEsportazione(NTSCStr(!ec_codart), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "des" 'Descrizione
                Dim strDescr As String = NTSCStr(!ec_descr)
                If bNoVbCrlf Then
                  strDescr = strDescr.Replace(vbCrLf, " ")
                  strDescr = strDescr.Replace(Chr(10), " ")
                  strDescr = strDescr.Replace(Chr(13), " ")
                End If
                ElaboraEsportazione(strDescr, sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "dei" 'Desc. interna
                Dim strDescr As String = NTSCStr(!ec_desint)
                If bNoVbCrlf Then
                  strDescr = strDescr.Replace(vbCrLf, " ")
                  strDescr = strDescr.Replace(Chr(10), " ")
                  strDescr = strDescr.Replace(Chr(13), " ")
                End If
                ElaboraEsportazione(strDescr, sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "unm" 'Unità di misura
                ElaboraEsportazione(NTSCStr(!ec_unmis), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "col" 'Colli
                ElaboraEsportazione(NTSCStr(!ec_colli), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "qta" 'Quantità
                ElaboraEsportazione(NTSCStr(!ec_quant), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "prz" 'Prezzo
                ElaboraEsportazione(NTSCStr(!ec_prezzo), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "sc1" 'Sconto 1
                ElaboraEsportazione(NTSCStr(!ec_scont1), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "sc2" 'Sconto 2
                ElaboraEsportazione(NTSCStr(!ec_scont2), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "sc3" 'Sconto 3
                ElaboraEsportazione(NTSCStr(!ec_scont3), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "sc4" 'Sconto 4
                ElaboraEsportazione(NTSCStr(!ec_scont4), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "sc5" 'Sconto 5
                ElaboraEsportazione(NTSCStr(!ec_scont5), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "sc6" 'Sconto 6
                ElaboraEsportazione(NTSCStr(!ec_scont6), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "pr1" 'Perc. provvigioni 1
                ElaboraEsportazione(NTSCStr(!ec_vprovv), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "pr2" 'Perc. provvigioni 2
                ElaboraEsportazione(NTSCStr(!ec_vprovv2), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "ma1" 'Magazzino 1
                ElaboraEsportazione(NTSCStr(!ec_magaz), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "ma2" 'Magazzino 2
                ElaboraEsportazione(NTSCStr(!ec_magaz2), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "not" 'Note
                Dim strDescr As String = NTSCStr(!ec_note)
                If bNoVbCrlf Then
                  strDescr = strDescr.Replace(vbCrLf, " ")
                  strDescr = strDescr.Replace(Chr(10), " ")
                  strDescr = strDescr.Replace(Chr(13), " ")
                End If
                ElaboraEsportazione(strDescr, sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "cau" 'Causale
                ElaboraEsportazione(NTSCStr(!ec_causale), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "cnp" 'Contropartita
                ElaboraEsportazione(NTSCStr(!ec_controp), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "iva" 'Codice iva
                ElaboraEsportazione(NTSCStr(!ec_codiva), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "str" 'Stampa riga
                ElaboraEsportazione(NTSCStr(!ec_stasino), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "com" 'Commessa
                ElaboraEsportazione(NTSCStr(!ec_commeca), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "lot" 'Lotto
                ElaboraEsportazione(NTSCStr(!xxo_lottox), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "cda" 'Centro di CA
                ElaboraEsportazione(NTSCStr(!ec_codcena), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "cca" 'Conto contropartita CA
                ElaboraEsportazione(NTSCStr(!ec_contocontr), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "mi1" 'Misura 1
                ElaboraEsportazione(NTSCStr(!ec_misura1), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "mi2" 'Misura 2
                ElaboraEsportazione(NTSCStr(!ec_misura2), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "mi3" 'Misura 3
                ElaboraEsportazione(NTSCStr(!ec_misura3), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "fas" 'Fase
                ElaboraEsportazione(NTSCStr(!ec_fase), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "ubi" 'Ubicazione
                ElaboraEsportazione(NTSCStr(!ec_ubicaz), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "prn" 'Prezzo netto
                ElaboraEsportazione(NTSCStr(!ec_flprznet), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "pro" 'Promozione
                ElaboraEsportazione(NTSCStr(!ec_codtpro), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "dic" 'Data inizio competenza
                ElaboraEsportazione(NTSCStr(!ec_datini), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "dif" 'Data fine competenza
                ElaboraEsportazione(NTSCStr(!ec_datfin), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "dtc" 'Data consegna
                If strChiamante = "BSORGSOR" Then ElaboraEsportazione(NTSCStr(!ec_datcons), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "con" 'Confermato
                If strChiamante = "BSORGSOR" Then ElaboraEsportazione(NTSCStr(!ec_confermato), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
              Case "ril" 'Rilasciato
                If strChiamante = "BSORGSOR" Then ElaboraEsportazione(NTSCStr(!ec_rilasciato), sbResult, strSep, bFixed, NTSCInt(strCell(2)))
            End Select
          End With
        Next
        If Not bFixed Then sbResult.Remove(sbResult.Length - 1, 1)
        sbResult.Append(vbCrLf)

        'Esporta la matricola
        If CBool(strExport(8)) And strChiamante <> "BSORGSOR" Then EsportaMatricola(dtrRow(z), bFixed, NTSCInt(strExport(9)), NTSCInt(strExport(10)), strSep, sbResult)

        'Esporta la taglia
        If CBool(strExport(11)) Then EsportaTaglia(dtrRow(z), bFixed, NTSCInt(strExport(12)), NTSCInt(strExport(13)), strSep, sbResult)
      Next

      Try
        swOut = New IO.StreamWriter(strExport(3), False, System.Text.Encoding.UTF8)
        swOut.Write(sbResult.ToString())
        swOut.Flush()
        swOut.Close()
      Catch ex As Exception
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129249470406387072, "Si è verificato un errore durante la creazione del file: |" & strExport(3) & "|" & vbCrLf & _
                                                                              "Verificare che il percorso e il nome del file siano corretti")))
        Return False
      End Try

      'Lancia la shell
      If CBool(strExport(0)) Then
        If strExport(1).Trim <> "" Then
          Try
            Dim proc As New System.Diagnostics.Process
            If CBool(strExport(2)) Then proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            proc.StartInfo.FileName = strExport(1)
            proc.StartInfo.UseShellExecute = True
            proc.Start()
            proc.WaitForExit()
          Catch ex As Exception
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129249470376074184, "Errore durante l'esecuzione della Shell '|" & CBool(strExport(1)) & "|'. Errore: ") & vbCrLf & ex.Message))
            Return False
          End Try
        End If
      End If    'If oCleBoll.bTermExecute Then

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
      LogStop()
    End Try
  End Function
  Public Overridable Function ElaboraEsportazione(ByVal strCampo As String, ByRef sbResult As Text.StringBuilder, ByVal strSep As String, _
                                                  ByVal bFixed As Boolean, ByVal lLength As Integer) As Boolean
    Try
      If bFixed Then
        sbResult.Append(strCampo.PadRight(lLength, " "c).Substring(0, lLength))
      Else
        sbResult.Append(strCampo & strSep)
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
  Public Overridable Function EsportaMatricola(ByVal dtrSel As DataRow, ByVal bFixed As Boolean, ByVal lMatric As Integer, ByVal lQta As Integer, _
                                               ByVal strSep As String, ByRef sbResult As Text.StringBuilder) As Boolean
    Dim dtrRow() As DataRow
    Try
      dtrRow = dttMOVMATR.Select("mma_riga = " & NTSCInt(dtrSel!ec_riga), "mma_rigaa")

      For z As Integer = 0 To dtrRow.Length - 1
        sbResult.Append("2")
        If bFixed Then
          sbResult.Append(NTSCStr(dtrRow(z)!mma_matric).PadRight(lMatric).Substring(0, lMatric))
          sbResult.Append(NTSCStr(dtrRow(z)!mma_quant).PadRight(lQta).Substring(0, lQta))
        Else
          sbResult.Append(strSep)
          sbResult.Append(NTSCStr(dtrRow(z)!mma_matric))
          sbResult.Append(strSep)
          sbResult.Append(NTSCStr(dtrRow(z)!mma_quant))
        End If
        sbResult.Append(vbCrLf)
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
  Public Overridable Function EsportaTaglia(ByVal dtrSel As DataRow, ByVal bFixed As Boolean, ByVal lTaglia As Integer, ByVal lQta As Integer, _
                                            ByVal strSep As String, ByRef sbResult As Text.StringBuilder) As Boolean
    Dim dtrRow() As DataRow
    Dim dttTagl As New DataTable
    Try
      dtrRow = dttECTC.Select("ec_riga = " & NTSCInt(dtrSel!ec_riga))

      If dtrRow.Length > 0 Then
        oCldDocu.ValCodiceDb(NTSCStr(dtrSel!xxo_codtagl), strDittaCorrente, "TABTAGL", "N", "", dttTagl)
        sbResult.Append("3")
        For z As Integer = 1 To 24
          If NTSCStr(dttTagl.Rows(0)("tb_dest" & z.ToString("00"))).Trim <> "" Then
            If bFixed Then
              sbResult.Append(NTSCStr(dttTagl.Rows(0)("tb_dest" & z.ToString("00"))).PadRight(lTaglia).Substring(0, lTaglia))
              sbResult.Append(NTSCStr(dtrRow(0)("ec_quant" & z.ToString("00"))).PadRight(lQta).Substring(0, lQta))
            Else
              sbResult.Append(strSep)
              sbResult.Append(NTSCStr(dttTagl.Rows(0)("tb_dest" & z.ToString("00"))))
              sbResult.Append(strSep)
              sbResult.Append(NTSCStr(dtrRow(0)("ec_quant" & z.ToString("00"))))
            End If
          End If
        Next
        sbResult.Append(vbCrLf)
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

  Public Overridable Function ImportaRighe(ByVal strImport() As String, ByVal strChiamante As String) As Boolean
    Dim strRighe(), strCell(), strSep, strSepDec, strFile, strOut As String
    Dim srIn As IO.StreamReader
    Dim bFixed, bTracciato As Boolean
    Dim i, lPos As Integer
    Dim dttTmp As New DataTable
    Dim bMatric, bTaglia, bTipoRiga As Boolean
    Dim lMatric, lQtaMatric, lTaglia, lQtaTaglia As Integer
    Try
      LogStart("BSMGDOCU", "Importazione righe")

      'Lancia la shell
      If CBool(strImport(0)) Then
        'Prima di tutto cancella il file se esiste
        Try
          System.IO.File.Delete(strImport(3))
        Catch ex As Exception
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571303239218750, "Impossibile cancellare il file '|" & strImport(3) & "|' prima della acquisizione. Errore: ") & vbCrLf & ex.Message))
          Return False
        End Try

        If strImport(1).Trim <> "" Then
          Try
            Dim proc As New System.Diagnostics.Process
            If CBool(strImport(2)) Then proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            proc.StartInfo.FileName = strImport(1)
            proc.StartInfo.UseShellExecute = True
            proc.Start()
            proc.WaitForExit()
          Catch ex As Exception
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129249491468377746, "Errore durante l'esecuzione della Shell '|" & CBool(strImport(1)) & "|'. Errore: ") & vbCrLf & ex.Message))
            Return False
          End Try
        End If
      End If    'If oCleBoll.bTermExecute Then

      '-- Controllo se il file di import esiste ----------------------------------------------------------------------
      If Not System.IO.File.Exists(strImport(3)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129248817897117886, "File da importare '|" & strImport(3) & "|' non trovato. Impossibile continuare.")))
        Return False
      End If

      srIn = New IO.StreamReader(strImport(3), System.Text.Encoding.Default)
      strFile = srIn.ReadLine

      'Configurazione presa dal file
      bFixed = CBool(IIf(strImport(10) = "F", True, False))
      strSep = Microsoft.VisualBasic.Left(strImport(11), 1)
      strRighe = strImport(strImport.Length - 1).Split("$"c)
      strSepDec = Microsoft.VisualBasic.Left(strImport(12), 1)
      bMatric = CBool(strImport(13))
      lMatric = NTSCInt(strImport(14))
      lQtaMatric = NTSCInt(strImport(15))
      bTaglia = CBool(strImport(16))
      lTaglia = NTSCInt(strImport(17))
      lQtaTaglia = NTSCInt(strImport(18))
      bTipoRiga = CBool(strImport(19))

      'Verifico se c'è il tracciato e in caso do precedenza all'impostazione del tracciato rispetto a quella configurata
      If strFile.Length > "BUSTRACCIATO: ".Length Then
        If strFile.Substring(0, "BUSTRACCIATO: ".Length) = "BUSTRACCIATO: " Then
          bTracciato = True
          strCell = strFile.Substring("BUSTRACCIATO: ".Length).Split("|"c)
          bFixed = CBool(IIf(strCell(0) = "F", True, False))
          strSep = strCell(1)
          strSepDec = ","
          strRighe = strCell(strCell.Length - 1).Split("$"c)
          bMatric = CBool(strCell(2))
          lMatric = NTSCInt(strCell(3))
          lQtaMatric = NTSCInt(strCell(4))
          bTaglia = CBool(strCell(5))
          lTaglia = NTSCInt(strCell(6))
          lQtaTaglia = NTSCInt(strCell(7))
          bTipoRiga = CBool(strCell(8))
        End If
      End If

      If Not bTracciato And strImport(strImport.Length - 1) = "NOCFG" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129489867302812500, "Importazione non configurata.")))
        Return False
      End If

      'Se ho trovato il tracciato o ho scelto di saltare la prima riga, leggo subito la seconda.
      If bTracciato Or CBool(strImport(5)) Then
        strFile = srIn.ReadLine
      End If

      'Compongo il campo con un separatore per poter usare la stessa procedura nei 2 sistemi di importazione
      If bFixed Then
        'Determina il separatore da usare per non dare fastidio alla riga:
        Dim strSepTmp As String = SeparatoreConsigliato(strFile)
        If bTipoRiga Then
          Select Case strFile(0)
            Case "1"c
              'C'è il primo carattere con il tipo riga
              lPos = 1
              strOut = "1" & strSepTmp
              For i = 0 To strRighe.Length - 1
                strOut &= strFile.Substring(lPos, NTSCInt(strRighe(i).Split("-"c)(2))) & strSepTmp
                lPos += NTSCInt(strRighe(i).Split("-"c)(2))
              Next
              strOut = strOut.Remove(strOut.Length - 1, 1)
            Case "2"c
              strOut = "2" & strSepTmp & strFile.Substring(1, lMatric) & strSepTmp & strFile.Substring(lMatric + 1, lQtaMatric)
            Case "3"c
              lPos = 1
              strOut = "3" & strSepTmp
              While lPos < strFile.Length - 1
                strOut &= strFile.Substring(lPos, lTaglia) & strSepTmp & strFile.Substring(lTaglia + 1, lQtaTaglia)
                lPos += lTaglia + lQtaTaglia
              End While
            Case Else
              strOut = ""
          End Select
        Else
          'Non c'è il primo carattere con il tipo riga
          lPos = 0
          strOut = ""
          For i = 0 To strRighe.Length - 1
            'Prende la dimensione minore tra il fine riga e la lunghezza della stringa impostata nel tag (così non da errore di "Index e length devono fare riferimento a una posizione nella stringa.")
            Dim lSize As Integer = NTSCInt(strRighe(i).Split("-"c)(2))
            If lSize + lPos < strFile.Length Then
              strOut &= strFile.Substring(lPos, lSize) & strSepTmp
            Else
              strOut &= strFile.Substring(lPos) & strSepTmp
            End If
            lPos += lSize
          Next
          strOut = strOut.Remove(strOut.Length - 1, 1)
        End If

        If Not ElaboraImportazione(strOut, strSepTmp, strSepDec, strRighe, bTipoRiga, CBool(strImport(6)), CBool(strImport(7)), CBool(strImport(8)), strChiamante) Then Return False
      Else
        If Not ElaboraImportazione(strFile, strSep, strSepDec, strRighe, bTipoRiga, CBool(strImport(6)), CBool(strImport(7)), CBool(strImport(8)), strChiamante) Then Return False
      End If

      While Not srIn.EndOfStream
        strFile = srIn.ReadLine()

        If bFixed Then
          Dim strSepTmp As String = SeparatoreConsigliato(strFile)
          If bTipoRiga Then
            Select Case strFile(0)
              Case "1"c
                'C'è il primo carattere con il tipo riga
                lPos = 1
                strOut = "1" & strSepTmp
                For i = 0 To strRighe.Length - 1
                  strOut &= strFile.Substring(lPos, NTSCInt(strRighe(i).Split("-"c)(2))) & strSepTmp
                  lPos += NTSCInt(strRighe(i).Split("-"c)(2))
                Next
                strOut = strOut.Remove(strOut.Length - 1, 1)
              Case "2"c
                strOut = "2" & strSepTmp & strFile.Substring(1, lMatric) & strSepTmp & strFile.Substring(lMatric + 1, lQtaMatric)
              Case "3"c
                lPos = 1
                strOut = "3" & strSepTmp
                While lPos < strFile.Length - 1
                  strOut &= strFile.Substring(lPos, lTaglia) & strSepTmp & strFile.Substring(lPos + lTaglia, lQtaTaglia) & strSepTmp
                  lPos += lTaglia + lQtaTaglia
                End While
                strOut = strOut.Remove(strOut.Length - 1, 1)
              Case Else
                strOut = ""
            End Select
          Else
            'Non c'è il primo carattere con il tipo riga
            lPos = 0
            strOut = ""
            For i = 0 To strRighe.Length - 1
              'Prende la dimensione minore tra il fine riga e la lunghezza della stringa impostata nel tag (così non da errore di "Index e length devono fare riferimento a una posizione nella stringa.")
              Dim lSize As Integer = NTSCInt(strRighe(i).Split("-"c)(2))
              If lSize + lPos < strFile.Length Then
                strOut &= strFile.Substring(lPos, lSize) & strSepTmp
              Else
                strOut &= strFile.Substring(lPos) & strSepTmp
              End If
              lPos += lSize
            Next
            strOut = strOut.Remove(strOut.Length - 1, 1)
          End If

          If Not ElaboraImportazione(strOut, strSepTmp, strSepDec, strRighe, bTipoRiga, CBool(strImport(6)), CBool(strImport(7)), CBool(strImport(8)), strChiamante) Then Return False
        Else
          If Not ElaboraImportazione(strFile, strSep, strSepDec, strRighe, bTipoRiga, CBool(strImport(6)), CBool(strImport(7)), CBool(strImport(8)), strChiamante) Then Return False
        End If
      End While

      srIn.Close()

      '--------------------------------------------------------------------------------------------------------------
      '--- Se richiesto cancella il file
      '--------------------------------------------------------------------------------------------------------------
      If CBool(strImport(4)) Then
        IO.File.Delete(strImport(3))
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
      LogStop()
    End Try
  End Function
  Public Overridable Function ElaboraImportazione(ByVal strRiga As String, ByVal strSep As String, ByVal strDec As String, ByVal strTracciato() As String, _
                                                  ByVal bTipoRiga As Boolean, ByVal bRicPrz As Boolean, ByVal bRicScn As Boolean, ByVal bRicPrv As Boolean, _
                                                  ByVal strChiamante As String) As Boolean
    Dim strPart(), strTmp, strArt, strBCUnmis As String
    Dim strDescr As String = ""
    Dim dtrRow() As DataRow
    Dim dBCQuant As Decimal
    Dim dttTmp, dttTagl As New DataTable
    Dim lStart As Integer
    Dim i As Integer
    Dim nFase As Integer = 0
    Dim strTaglia As String = ""
    Dim bDisabilitaMsgPrezzoZeroTmp As Boolean = bDisabilitaMsgPrezzoZero
    Try
      strArt = ""
      dBCQuant = 0
      strBCUnmis = ""

      strPart = strRiga.Split(CType(strSep, Char))

      'Se c'è il tipo riga devo prendere la cella successiva
      If bTipoRiga Then
        lStart = 1
        If strPart(0) = "2" Then 'è una riga con matricola 
          If strChiamante = "BSORGSOR" Then Return True
          If dttEC.Rows.Count = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129856882071375592, "Errore durante l'importazione. Il file non è corretto.")))
            Return False
          End If
          dttMOVMATR.Rows.Add(dttMOVMATR.NewRow)
          With dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)
            !codditt = strDittaCorrente
            'Considero che appartengano all'ultima riga importata
            !mma_tipork = dttEC.Rows(dttEC.Rows.Count - 1)!ec_tipork
            !mma_anno = dttEC.Rows(dttEC.Rows.Count - 1)!ec_anno
            !mma_serie = dttEC.Rows(dttEC.Rows.Count - 1)!ec_serie
            !mma_numdoc = dttEC.Rows(dttEC.Rows.Count - 1)!ec_numdoc
            !mma_riga = dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga
            !mma_rigaa = NTSCInt(dttMOVMATR.Compute("MAX(mma_rigaa)", "mma_riga = " & NTSCInt(!mma_riga))) + 1
            !mma_matric = strPart(1)
            !mma_quant = strPart(2)
            Try
              !mma_quant = NTSCDec(strPart(2).Trim().Replace(strDec, ","))
            Catch ex As Exception
              LogWrite(oApp.Tr(Me, 129252972890937500, "Valore '|" & strPart(2).Trim & "| non valido per le quantità, verrà inserito 1."), True)
              !mma_quant = 1
            End Try
          End With

          Return True
        ElseIf strPart(0) = "3" Then 'è una riga di taglia
          'Considero che appartengano all'ultima riga importata
          oCldDocu.ValCodiceDb(NTSCStr(dttEC.Rows(dttEC.Rows.Count - 1)!xxo_codtagl), strDittaCorrente, "TABTAGL", "N", "", dttTagl)
          'Aggiungo subito la nuova riga (controllando che non ci sia già una esistente)
          dtrRow = dttECTC.Select("ec_riga = " & NTSCInt(dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga))
          If dtrRow.Length = 0 Then
            dttECTC.Rows.Add(dttECTC.NewRow)
            With dttECTC.Rows(dttECTC.Rows.Count - 1)
              !codditt = strDittaCorrente
              !ec_tipork = dttEC.Rows(dttEC.Rows.Count - 1)!ec_tipork
              !ec_anno = dttEC.Rows(dttEC.Rows.Count - 1)!ec_anno
              !ec_serie = dttEC.Rows(dttEC.Rows.Count - 1)!ec_serie
              !ec_numdoc = dttEC.Rows(dttEC.Rows.Count - 1)!ec_numdoc
              !ec_riga = dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga
            End With
            dtrRow = dttECTC.Select("ec_riga = " & NTSCInt(dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga))
          End If

          For z As Integer = 1 To strPart.Length - 1 Step 2
            For i = 1 To 24
              'Se trovo la taglia la assegno 
              If NTSCStr(dttTagl.Rows(0)("tb_dest" & i.ToString("00"))) = strPart(z).Trim Then
                dtrRow(0)("ec_quant" & i.ToString("00")) = NTSCDec(strPart(z + 1).Trim().Replace(strDec, ","))
              End If
            Next
          Next

          Return True
        End If
      Else
        lStart = 0
      End If

      If (strPart.Length <> strTracciato.Length And Not bTipoRiga) Or (strPart.Length - 1 <> strTracciato.Length And bTipoRiga) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129248667436327086, "Il tracciato non corrisponde con il formato della riga da importare. Elaborazione annullata.")))
        Return False
      End If

      'Scorre i vettori 2 volte. La prima per cercare il codice articolo (o equivalente, se lo trova genera la riga ed effettua una serie di controlli)
      For i = 0 To strTracciato.Length - 1
        strArt = ""
        dBCQuant = 0
        strBCUnmis = ""

        Select Case strTracciato(i).Substring(0, 3)
          Case "art" ' Articolo
            oCldDocu.ValCodiceDb(strPart(i + lStart).Trim, strDittaCorrente, "ARTICO", "S", "", dttTmp)
            If dttTmp.Rows.Count = 0 Then
              LogWrite(oApp.Tr(Me, 129248817998684936, "Articolo |" & strPart(i + lStart).Trim & "| non trovato. Verrà importato come articolo 'D'."), True)
              strDescr = oApp.Tr(Me, 129597071865468750, "Articolo |" & strPart(i + lStart).Trim & "| non trovato")
              strArt = "D"
            Else
              strArt = strPart(i + lStart).Trim
            End If
            Exit For
          Case "acf" ' Art. C/F
            strTmp = CType(oCleComm, CLELBMENU).TrovaCodartDaCodarfo(strPart(i + lStart).Trim, NTSCInt(dttET.Rows(0)!et_conto), strDittaCorrente, "")
            If strTmp = "" Then
              LogWrite(oApp.Tr(Me, 129248818010091697, "Articolo cliente/fornitore |" & strPart(i + lStart).Trim & "| (conto |" & lHlbcConto & "|) non associato ad alcun articolo. Verrà scartato."), True)
              Return False
            Else
              strArt = strTmp
              Exit For
            End If
          Case "bar" ' Barcode
            oCldDocu.ValCodiceDb(strPart(i + lStart).Trim, strDittaCorrente, "BARCODE", "S", "", dttTmp)
            If dttTmp.Rows.Count = 0 Then
              LogWrite(oApp.Tr(Me, 129248818031030135, "Barcode |" & strPart(i + lStart).Trim & "| non trovato. Verrà scartato."), True)
              Return False
            Else
              strBCUnmis = NTSCStr(dttTmp.Rows(0)!bc_unmis)
              dBCQuant = NTSCDec(dttTmp.Rows(0)!bc_quant)
              strArt = NTSCStr(dttTmp.Rows(0)!bc_codart)
              nFase = NTSCInt(dttTmp.Rows(0)!bc_fase)
              strTaglia = NTSCStr(dttTmp.Rows(0)!bc_tagl)
              Exit For
            End If
          Case "all" ' Art. o Barcode o Art. C/F
            oCldDocu.ValCodiceDb(strPart(i + lStart).Trim, strDittaCorrente, "ARTICO", "S", "", dttTmp)
            If dttTmp.Rows.Count <> 0 Then
              strArt = strPart(i + lStart).Trim
              Exit For
            Else
              strTmp = CType(oCleComm, CLELBMENU).TrovaCodartDaCodarfo(strPart(i + lStart).Trim, lHlbcConto, strDittaCorrente, "")
              If strTmp <> "" Then
                strArt = strTmp
                Exit For
              Else
                oCldDocu.ValCodiceDb(strPart(i + lStart).Trim, strDittaCorrente, "BARCODE", "S", "", dttTmp)
                If dttTmp.Rows.Count <> 0 Then
                  strBCUnmis = NTSCStr(dttTmp.Rows(0)!bc_unmis)
                  dBCQuant = NTSCDec(dttTmp.Rows(0)!bc_quant)
                  strArt = NTSCStr(dttTmp.Rows(0)!bc_codart)
                  nFase = NTSCInt(dttTmp.Rows(0)!bc_fase)
                  strTaglia = NTSCStr(dttTmp.Rows(0)!bc_tagl)
                  Exit For
                Else
                  LogWrite(oApp.Tr(Me, 129882210269846975, "Articolo |" & strPart(i + lStart).Trim & "| non trovato. Verrà importato come articolo 'D'."), True)
                  strDescr = oApp.Tr(Me, 129882210290879023, "Articolo |" & strPart(i + lStart).Trim & "| non trovato")
                  strArt = "D"
                  Exit For
                End If
              End If
            End If
        End Select
      Next

      'Creo la nuova riga con i dati trovati
      dttEC.Rows.Add(dttEC.NewRow)
      With dttEC.Rows(dttEC.Rows.Count - 1)
        '------------------------------------------------------------------------------------------------------------  
        '--- Forzo la MovordOnAddNewRow
        '------------------------------------------------------------------------------------------------------------
        !codditt = "."
        !codditt = strDittaCorrente
        !ec_codart = strArt
        If strDescr <> "" Then !ec_descr = strDescr
        !ec_quant = 1 ' di base lascio 1, poi in caso modifico dopo colli o quantità
        If nFase <> 0 Then !ec_fase = nFase

        'Sovrascrivo i dati caricati di default con quelli passati.
        For i = 0 To strTracciato.Length - 1
          Select Case strTracciato(i).Substring(0, 3) ' i primi 3 caratteri indicano il tipo di campo
            Case "   " ' Non deve fare nulla, quella riga non interessa a business.
            Case "des" 'Descrizione
              If strDescr = "" Then !ec_descr = strPart(i + lStart).Trim()
            Case "dei" 'Desc. interna
              !ec_desint = strPart(i + lStart).Trim()
            Case "unm" 'Unità di misura
              !ec_unmis = strPart(i + lStart).Trim()
            Case "col" 'Colli
              Try
                If strBCUnmis <> "" Then !ec_unmis = strBCUnmis 'può essere solo l'UM indicata nella tabella barcode!
                !ec_colli = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249473075325890, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per i colli, verrà inserito 1."), True)
                !ec_colli = 1
              End Try
            Case "qta" 'Quantità
              Try
                !ec_quant = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249474664711062, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per le quantità, verrà inserito 1."), True)
                !ec_quant = 1
              End Try
            Case "prz" 'Prezzo
              If Not bRicPrz Then
                Try
                  !ec_prezzo = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                Catch ex As Exception
                  LogWrite(oApp.Tr(Me, 129249474994088170, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per il prezzo, verrà lasciato quello attuale."), True)
                End Try
              End If
            Case "sc1" 'Sconto 1
              If Not bRicScn Then
                Try
                  !ec_scont1 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                Catch ex As Exception
                  LogWrite(oApp.Tr(Me, 129249476158780996, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per lo sconto 1, verrà lasciato quello attuale."), True)
                End Try
              End If
            Case "sc2" 'Sconto 2
              If Not bRicScn Then
                Try
                  !ec_scont2 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                Catch ex As Exception
                  LogWrite(oApp.Tr(Me, 129249480825655996, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per lo sconto 2, verrà lasciato quello attuale."), True)
                End Try
              End If
            Case "sc3" 'Sconto 3
              If Not bRicScn Then
                Try
                  !ec_scont3 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                Catch ex As Exception
                  LogWrite(oApp.Tr(Me, 129249480806593496, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per lo sconto 3, verrà lasciato quello attuale."), True)
                End Try
              End If
            Case "sc4" 'Sconto 4
              If Not bRicScn Then
                Try
                  !ec_scont4 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                Catch ex As Exception
                  LogWrite(oApp.Tr(Me, 129249480791280996, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per lo sconto 4, verrà lasciato quello attuale."), True)
                End Try
              End If
            Case "sc5" 'Sconto 5
              If Not bRicScn Then
                Try
                  !ec_scont5 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                Catch ex As Exception
                  LogWrite(oApp.Tr(Me, 129249480775187246, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per lo sconto 5, verrà lasciato quello attuale."), True)
                End Try
              End If
            Case "sc6" 'Sconto 6
              If Not bRicScn Then
                Try
                  !ec_scont6 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                Catch ex As Exception
                  LogWrite(oApp.Tr(Me, 129249480762999746, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per lo sconto 6, verrà lasciato quello attuale."), True)
                End Try
              End If
            Case "pr1" 'Perc. provvigioni 1
              If Not bRicPrv Then
                Try
                  !ec_provv = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                Catch ex As Exception
                  LogWrite(oApp.Tr(Me, 129249490225549446, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la provvigiorne 1, verrà lasciato quello attuale."), True)
                End Try
              End If
            Case "pr2" 'Perc. provvigioni 2
              If Not bRicPrv Then
                Try
                  !ec_provv2 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                Catch ex As Exception
                  LogWrite(oApp.Tr(Me, 129249490212891171, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la provvigione 2, verrà lasciato quello attuale."), True)
                End Try
              End If
            Case "ma1" 'Magazzino 1
              Try
                !ec_magaz = NTSCInt(strPart(i + lStart).Trim())
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249490202733296, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per il magazzino 1, verrà lasciato quello attuale."), True)
              End Try
            Case "ma2" 'Magazzino 2
              Try
                !ec_magaz2 = NTSCInt(strPart(i + lStart).Trim())
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249490193356796, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per il magazzino 2, verrà lasciato quello attuale."), True)
              End Try
            Case "not" 'Note
              !ec_note = strPart(i + lStart).Trim()
            Case "cau" 'Causale
              Try
                !ec_causale = NTSCInt(strPart(i + lStart).Trim())
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249490182730096, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la causale, verrà lasciato quello attuale."), True)
              End Try
            Case "cnp" 'Contropartita
              Try
                !ec_controp = NTSCInt(strPart(i + lStart).Trim())
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249490172103396, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la contropartita, verrà lasciato quello attuale."), True)
              End Try
            Case "iva" 'Codice iva
              Try
                !ec_codiva = NTSCInt(strPart(i + lStart).Trim())
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249491557602206, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per il codice iva, verrà lasciato quello attuale."), True)
              End Try
            Case "str" 'Stampa riga
              !ec_stasino = strPart(i + lStart).Trim()
            Case "com" 'Commessa
              Try
                !ec_commeca = NTSCInt(strPart(i + lStart).Trim())
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249491567759106, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la commessa, verrà lasciato quello attuale."), True)
              End Try
            Case "lot" 'Lotto
              Try
                !xxo_lottox = NTSCStr(strPart(i + lStart).Trim())
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249491579009826, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per il lotto, verrà lasciato quello attuale."), True)
              End Try
            Case "cda" 'Centro di CA
              Try
                !ec_codcena = NTSCInt(strPart(i + lStart).Trim())
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249491589791766, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per il centro, verrà lasciato quello attuale."), True)
              End Try
            Case "cca" 'Conto contropartita CA
              Try
                !ec_contocontr = NTSCInt(strPart(i + lStart).Trim())
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249480688624746, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la contropartita di CA, verrà lasciato quello attuale."), True)
              End Try
            Case "mi1" 'Misura 1
              Try
                !ec_misura1 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249489386508971, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la misura 1, verrà lasciato quello attuale."), True)
              End Try
            Case "mi2" 'Misura 2
              Try
                !ec_misura2 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249489377445021, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la misura 2, verrà lasciato quello attuale."), True)
              End Try
            Case "mi3" 'Misura 3
              Try
                !ec_misura3 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249489366193221, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la misura 3, verrà lasciato quello attuale."), True)
              End Try
            Case "fas" 'Fase
              Try
                !ec_fase = NTSCInt(strPart(i + lStart).Trim())
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249489351190821, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la fase, verrà lasciato quello attuale."), True)
              End Try
            Case "ubi" 'Ubicazione
              !ec_ubicaz = strPart(i + lStart).Trim()
            Case "prn" 'Prezzo netto
              !ec_flprznet = strPart(i + lStart).Trim()
            Case "pro" 'Promozione
              !ec_codtpro = strPart(i + lStart).Trim()
            Case "dic" 'Data inizio competenza
              Try
                !ec_datini = NTSCDate(strPart(i + lStart).Trim())
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249489320404646, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la data inizio competenza, verrà lasciato quello attuale."), True)
              End Try
            Case "dif" 'Data fine competenza
              Try
                !ec_datfin = NTSCDate(strPart(i + lStart).Trim())
              Catch ex As Exception
                LogWrite(oApp.Tr(Me, 129249491679797526, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la data fine competenza, verrà lasciato quello attuale."), True)
              End Try
            Case "dtc" 'Data consegna
              If strChiamante = "BSORGSOR" Then
                Try
                  !ec_datcons = NTSCDate(strPart(i + lStart).Trim())
                Catch ex As Exception
                  LogWrite(oApp.Tr(Me, 129249489613732821, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la data consegna, verrà lasciato quello attuale."), True)
                End Try
              End If
            Case "con" 'Confermato
              If strChiamante = "BSORGSOR" Then !ec_confermato = strPart(i + lStart).Trim()
            Case "ril" 'Rilasciato
              If strChiamante = "BSORGSOR" Then !ec_rilasciato = strPart(i + lStart).Trim()
          End Select
        Next

        'Se non ho indicato il tipo riga, allora prendo la taglia dal barcode
        If Not bTipoRiga AndAlso strTaglia <> "" Then
          'Considero che appartengano all'ultima riga importata
          oCldDocu.ValCodiceDb(NTSCStr(dttEC.Rows(dttEC.Rows.Count - 1)!xxo_codtagl), strDittaCorrente, "TABTAGL", "N", "", dttTagl)
          'Aggiungo subito la nuova riga (controllando che non ci sia già una esistente)
          dtrRow = dttECTC.Select("ec_riga = " & NTSCInt(dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga))
          If dtrRow.Length = 0 Then
            dttECTC.Rows.Add(dttECTC.NewRow)
            With dttECTC.Rows(dttECTC.Rows.Count - 1)
              !codditt = strDittaCorrente
              !ec_tipork = dttEC.Rows(dttEC.Rows.Count - 1)!ec_tipork
              !ec_anno = dttEC.Rows(dttEC.Rows.Count - 1)!ec_anno
              !ec_serie = dttEC.Rows(dttEC.Rows.Count - 1)!ec_serie
              !ec_numdoc = dttEC.Rows(dttEC.Rows.Count - 1)!ec_numdoc
              !ec_riga = dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga
            End With
            dtrRow = dttECTC.Select("ec_riga = " & NTSCInt(dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga))
          End If

          For i = 1 To 24
            'Se trovo la taglia la assegno 
            If NTSCStr(dttTagl.Rows(0)("tb_dest" & i.ToString("00"))) = strTaglia Then
              dtrRow(0)("ec_quant" & i.ToString("00")) = dttEC.Rows(dttEC.Rows.Count - 1)!ec_quant
              Exit For
            End If
          Next
        End If
      End With

      '--------------------------------------------------------------------------------------------------------------
      bDisabilitaMsgPrezzoZero = True
      RecordSalva(dttEC.Rows.Count - 1, False, Nothing)
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
    Finally
      bDisabilitaMsgPrezzoZero = bDisabilitaMsgPrezzoZeroTmp
    End Try
  End Function

  Public Overridable Function SeparatoreConsigliato(ByVal strRiga As String) As String
    Dim strSepTmp As String = "+"
    Try
      If Not strRiga.Contains("§") Then
        strSepTmp = "§"
      ElseIf Not strRiga.Contains("|") Then
        strSepTmp = "|"
      ElseIf Not strRiga.Contains("#") Then
        strSepTmp = "#"
      ElseIf Not strRiga.Contains("-") Then
        strSepTmp = "-"
      ElseIf Not strRiga.Contains("_") Then
        strSepTmp = "_"
      ElseIf Not strRiga.Contains("@") Then
        strSepTmp = "@"
      ElseIf Not strRiga.Contains("$") Then
        strSepTmp = "$"
      ElseIf Not strRiga.Contains("&") Then
        strSepTmp = "&"
      ElseIf Not strRiga.Contains("£") Then
        strSepTmp = "£"
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
    Return strSepTmp
  End Function


  Public Overridable Sub SeparatePathAndFileName(ByVal FullPath As String, ByRef Path As String, ByRef FileName As String)
    Try
      Dim nSepPos As Integer = 0
      Dim nSepPos2 As Integer = 0
      Dim fUsingDriveSep As Boolean

      nSepPos = InStrRev(FullPath, "\")
      nSepPos2 = InStrRev(FullPath, "/")
      If nSepPos2 > nSepPos Then
        nSepPos = nSepPos2
      End If
      nSepPos2 = InStrRev(FullPath, ":")
      If nSepPos2 > nSepPos Then
        nSepPos = nSepPos2
        fUsingDriveSep = True
      End If

      If nSepPos = 0 Then
        'Separator was not found.
        Path = CurDir$()
        FileName = FullPath
      Else
        If fUsingDriveSep Then
          Path = Left$(FullPath, nSepPos)
        Else
          Path = Left$(FullPath, nSepPos - 1)
        End If
        FileName = Mid$(FullPath, nSepPos + 1)
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
  End Sub


  Public Overridable Function ImportaDaLista(ByVal lLista As Integer, ByVal bCancella As Boolean) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return ImportaDaLista(lLista, bCancella, False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function ImportaDaLista(ByVal lLista As Integer, ByVal bCancella As Boolean, ByVal bScarichi As Boolean) As Boolean
    Dim dttLista, dttTagl As New DataTable
    Dim dTotQta As Decimal
    Dim dtrRow(), dtrRowLista() As DataRow
    Dim i, k As Integer

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {lLista, bCancella, bScarichi})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      '--- Leggo le righe della lista selezionata
      '--------------------------------------------------------------------------------------------------------------
      If Not oCldDocu.SelezionaRigheLista(strDittaCorrente, lLista, dttLista) Then Return False
      '--------------------------------------------------------------------------------------------------------------
      If dttLista.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129303030435377450, "La lista selezionata non contiene articoli.")))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiungo tutte le righe trovate.
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrLista As DataRow In dttLista.Rows
        '------------------------------------------------------------------------------------------------------------
        If dtrLista.RowState = DataRowState.Deleted Then Continue For
        '------------------------------------------------------------------------------------------------------------
        With dtrLista
          '----------------------------------------------------------------------------------------------------------
          '--- Preparo le righe del corpo
          '----------------------------------------------------------------------------------------------------------
          AggiungiRigaCorpo(bScarichi, NTSCStr(!lsa_codart), NTSCInt(!lsa_fase), 0)
          '----------------------------------------------------------------------------------------------------------
          Dim strCodArt As String
          If bScarichi = False Then
            strCodArt = NTSCStr(dttEC.Rows(dttEC.Rows.Count - 1)!ec_codart)
          Else
            strCodArt = NTSCStr(dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_codart)
          End If

          If strCodArt.Trim = "" Then
            If bScarichi = False Then
              dttEC.Rows(dttEC.Rows.Count - 1).RejectChanges()
            Else
              dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1).RejectChanges()
            End If
          Else
            If bScarichi = False Then
              dttEC.Rows(dttEC.Rows.Count - 1)!ec_commeca = !lsa_commeca
              dttEC.Rows(dttEC.Rows.Count - 1)!ec_lotto = !lsa_lotto
              dttEC.Rows(dttEC.Rows.Count - 1)!ec_ubicaz = !lsa_ubicaz
            Else
              dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_commeca = !lsa_commeca
              dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_lotto = !lsa_lotto
              dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_ubicaz = !lsa_ubicaz
              dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_magaz = NTSCInt(dttET.Rows(0)!et_magimp)
              If BORDINI = False Then
                dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_causale = NTSCInt(dttET.Rows(0)!et_causcap)
              End If
            End If
            '----------------------------------------------------------------------------------------------------------
            '--- Se c'è aggiunto la matricola (negli ordini non c'è la gestione matricole)
            '----------------------------------------------------------------------------------------------------------
            If NTSCStr(!lsa_matric).Trim <> "" And Not BORDINI Then
              dttMOVMATR.Rows.Add(dttMOVMATR.NewRow())
              dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!codditt = strDittaCorrente
              '--------------------------------------------------------------------------------------------------------
              '--- Considero che appartengano all'ultima riga importata
              '--------------------------------------------------------------------------------------------------------
              If bScarichi = False Then
                dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!mma_tipork = dttEC.Rows(dttEC.Rows.Count - 1)!ec_tipork
                dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!mma_anno = dttEC.Rows(dttEC.Rows.Count - 1)!ec_anno
                dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!mma_serie = dttEC.Rows(dttEC.Rows.Count - 1)!ec_serie
                dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!mma_numdoc = dttEC.Rows(dttEC.Rows.Count - 1)!ec_numdoc
                dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!mma_riga = dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga
                dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!mma_rigaa = NTSCInt(dttMOVMATR.Compute("MAX(mma_rigaa)", "mma_riga = " & NTSCInt(dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga))) + 1
              Else
                dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!mma_tipork = dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_tipork
                dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!mma_anno = dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_anno
                dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!mma_serie = dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_serie
                dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!mma_numdoc = dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_numdoc
                dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!mma_riga = dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_riga
                dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!mma_rigaa = NTSCInt(dttMOVMATR.Compute("MAX(mma_rigaa)", "mma_riga = " & NTSCInt(dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_riga))) + 1
              End If
              dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!mma_matric = !lsa_matric
              dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)!mma_quant = !lsa_esist
            End If
            '----------------------------------------------------------------------------------------------------------
            '--- Se c'è aggiungo la taglia
            '----------------------------------------------------------------------------------------------------------
            If NTSCInt(!lsa_tcindtagl) = 0 Then
              If bScarichi = False Then
                dttEC.Rows(dttEC.Rows.Count - 1)!ec_quant = !lsa_esist
              Else
                dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1)!ec_quant = !lsa_esist
              End If
            Else
              If bScarichi = False Then
                dTotQta = 0
                '------------------------------------------------------------------------------------------------------
                '--- Controlla se ci sono altre righe con lo stesso articolo ma taglie diverse
                '------------------------------------------------------------------------------------------------------
                dtrRowLista = dttLista.Select("lsa_codart = " & CStrSQL(!lsa_codart) & _
                                              " AND lsa_fase = " & NTSCInt(!lsa_fase) & _
                                              " AND lsa_commeca = " & NTSCInt(!lsa_commeca) & _
                                              " AND lsa_lotto = " & NTSCInt(!lsa_lotto) & _
                                              " AND lsa_ubicaz = " & CStrSQL(!lsa_ubicaz) & _
                                              " AND lsa_tcindtagl <> 0") 'lsa_tcindtagl per gestire il caso in cui non è stata messa la taglia nella lista su alcuni articoli
                '------------------------------------------------------------------------------------------------------
                For k = 0 To dtrRowLista.Length - 1
                  oCldDocu.ValCodiceDb(NTSCStr(dtrRowLista(k)!lsa_tcindtagl), strDittaCorrente, "TABTAGL", "N", "", dttTagl)
                  If k = 0 Then
                    dttECTC.Rows(dttECTC.Rows.Count - 1)!codditt = strDittaCorrente
                    dttECTC.Rows(dttECTC.Rows.Count - 1)!ec_tipork = dttEC.Rows(dttEC.Rows.Count - 1)!ec_tipork
                    dttECTC.Rows(dttECTC.Rows.Count - 1)!ec_anno = dttEC.Rows(dttEC.Rows.Count - 1)!ec_anno
                    dttECTC.Rows(dttECTC.Rows.Count - 1)!ec_serie = dttEC.Rows(dttEC.Rows.Count - 1)!ec_serie
                    dttECTC.Rows(dttECTC.Rows.Count - 1)!ec_numdoc = dttEC.Rows(dttEC.Rows.Count - 1)!ec_numdoc
                    dttECTC.Rows(dttECTC.Rows.Count - 1)!ec_riga = dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga
                  End If
                  If NTSCStr(dtrRowLista(k)!lsa_tctaglia).Trim <> "" Then
                    dtrRow = dttECTC.Select("ec_riga = " & NTSCInt(dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga))
                    '----------------------------------------------------------------------------------------------------
                    For i = 1 To 24
                      '--------------------------------------------------------------------------------------------------
                      '--- Se trovo la taglia la assegno 
                      '--------------------------------------------------------------------------------------------------
                      If NTSCStr(dttTagl.Rows(0)("tb_dest" & i.ToString("00"))) = NTSCStr(dtrRowLista(k)!lsa_tctaglia) Then
                        dtrRow(0)("ec_quant" & i.ToString("00")) = NTSCDec(dtrRow(0)("ec_quant" & i.ToString("00"))) + NTSCDec(dtrRowLista(k)!lsa_esist)
                        dTotQta += NTSCDec(dtrRowLista(k)!lsa_esist)
                      End If
                    Next
                    '----------------------------------------------------------------------------------------------------
                    dtrRowLista(k).Delete()
                    '----------------------------------------------------------------------------------------------------
                  Else
                    'Così crea la riga ma non permette di salvarla perchè il dettaglio taglie è errato. (visto che la lista di partenza era errata)
                    dTotQta += NTSCDec(dtrRowLista(k)!lsa_esist)
                  End If
                Next
                dttEC.Rows(dttEC.Rows.Count - 1)!ec_quant = dTotQta
              End If
            End If
          End If
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      If bCancella = True Then CancellaLista(lLista)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function CaricaListe(ByVal lMaga As Integer, ByVal strDescr As String, ByVal strOperat As String, ByRef dttOut As DataTable) As Boolean
    Try
      If Not oCldDocu.CaricaListe(strDittaCorrente, lMaga, strDescr, strOperat, dttOut) Then Return False

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

  Public Overridable Function ControllaRigheBarcode(ByVal lLista As Integer, ByVal bCancella As Boolean) As Boolean
    Dim dttLista As New DataTable
    Dim strOrder As String
    Try
      'Leggo le righe della lista selezionata
      If Not oCldDocu.SelezionaRigheLista(strDittaCorrente, lLista, dttLista) Then Return False

      If dttLista.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129303030435377451, "La lista selezionata non contiene articoli.")))
        Return False
      End If

      '--------------------------------------------------------------------------------------------------------------
      strOrder = oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSVEHLBC", "OPZIONI", ".", "OrdineTrattamentoRigheOrdine", "DO", " ", "DO")  ' NON DOCUMENTARE

      For z As Integer = 0 To dttLista.Rows.Count - 1
        ProponiEvasioneOrdiniDaLista(NTSCStr(dttLista.Rows(z)!lsa_codart), NTSCDec(dttLista.Rows(z)!lsa_esist), strOrder)
      Next

      If bCancella Then
        CancellaLista(lLista)
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
  Public Overridable Function ProponiEvasioneOrdiniDaLista(ByVal strArt As String, ByRef dQuant As Decimal, ByVal strOrder As String) As Boolean
    Dim dttLista As New DataTable
    Dim dQuadaeva As Decimal
    Dim dtrT(), dtrTrow As DataRow
    Dim bRigaNuova As Boolean = False
    Dim i As Integer = 0
    Dim strDescr As String = ""
    Try
      dtrTrow = Nothing

      Select Case strOrder
        Case "DC"  '--> data consegna
          dtrT = dsHlbcShared.Tables("SELDOC").Select("mo_codart = " & CStrSQL(strArt), "xx_prefer DESC, mo_datcons ASC, xx_flevasdaeva DESC, mo_riga ASC")
        Case Else 'DO --> data ordine
          dtrT = dsHlbcShared.Tables("SELDOC").Select("mo_codart = " & CStrSQL(strArt), "xx_prefer DESC, xx_datord ASC, xx_flevasdaeva DESC, mo_riga ASC")
      End Select

      bRigaNuova = True
      i = 0
      If dtrT.Length > 0 Then
        'Identificazione riga da aggiornare
        Do While Not i = dtrT.Length
          If dtrT(i)!xx_flevasdaeva.ToString.ToUpper = "C" Or NTSCDate(dtrT(i)!xx_datord) = NTSCDate(IntSetDate("31/12/2099")) Then
            dtrTrow = dtrT(i)
            dQuadaeva = NTSCDec(dtrTrow!xx_quadaeva)
            bRigaNuova = False
            Exit Do
          End If
          i += 1
        Loop    'Do While Not i = dtrT.Length

        'Controllo per articolo movimentabile
        If Not bRigaNuova Then
          If NTSCDate(dtrT(i)!xx_datord) <> NTSCDate(IntSetDate("31/12/2099")) Then
            If NTSCInt(dtrTrow!xx_codpaga) <> nHlbcCodpaga Or NTSCInt(dtrTrow!xx_coddest) <> nHlbcCoddest Then
              LogWrite(oApp.Tr(Me, 129304108547209537, "Articolo: '|" & strArt & "|' non evadibile! Controlla condizione di pagamento e destinazione difformi dal documento corrente. Verrà scartato."), True)
              'TerminaleProponiEvasioneOrdini = True 'comunque procede
              Return False
            End If
          End If
          If (dQuadaeva + dQuant < NTSCDec(dtrTrow!xx_qtares)) Or (NTSCDate(dtrT(i)!xx_datord) = NTSCDate(IntSetDate("31/12/2099"))) Then
            'Aggiornamento riga
            dtrTrow!xx_quadaeva = dQuadaeva + dQuant
            dtrTrow.AcceptChanges()
          Else
            'Aggiornamento riga
            dtrTrow!xx_quadaeva = NTSCDec(dtrTrow!xx_qtares)
            dtrTrow!xx_flevasdaeva = "S"
            dtrTrow.AcceptChanges()

            If ArrDbl(dQuant - (NTSCDec(dtrTrow!xx_qtares) - dQuadaeva), 3) > 0 Then
              'davide 05/05/2003: non fa una nuova riga ma va a vedere se ci sono
              'altre righe d'ordine da scalare
              'Riga nuova
              'RigaNuova snaTmp!bc_codart, ArrDbl(dQtaBarre - (dQtares - dQuadaeva), 3)
              dQuant = ArrDbl(dQuant - (NTSCDec(dtrTrow!xx_qtares) - dQuadaeva), 3)
              ProponiEvasioneOrdiniDaLista(strArt, dQuant, strOrder)
            End If
          End If
        Else
          oCldDocu.ValCodiceDb(strArt, strDittaCorrente, "ARTICO", "S", strDescr)
          'Riga nuova
          HlbcRigaNuova(strArt, 0, strDescr, dQuant)
        End If ' brigaNUova
      Else
        oCldDocu.ValCodiceDb(strArt, strDittaCorrente, "ARTICO", "S", strDescr)
        'Riga nuova
        HlbcRigaNuova(strArt, 0, strDescr, dQuant)
      End If    'If dtrT.Length > 0 Then

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

  Public Overridable Function CancellaLista(ByVal lLista As Integer) As Boolean
    Dim dttTmp As New DataTable
    Dim strMsg As String
    Try
      'Prima valuta se può cancellarla o se è in uso da una promozione
      If Not oCldDocu.CheckInPromozione(strDittaCorrente, lLista, dttTmp) Then Return False

      If dttTmp.Rows.Count > 0 Then
        strMsg = oApp.Tr(Me, 129525087615387213, "Non è possibile modificare o cancellare la lista selenazionata in quanto è in uso nelle seguenti promozioni:")

        For z As Integer = 0 To dttTmp.Rows.Count - 1
          strMsg &= vbCrLf & NTSCStr(dttTmp.Rows(z)!tb_codrepr) & " - " & NTSCStr(dttTmp.Rows(z)!tb_desrepr)
        Next

        ThrowRemoteEvent(New NTSEventArgs("", strMsg))
        Return False
      End If


      If Not oCldDocu.CancellaLista(strDittaCorrente, lLista) Then Return False

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

  Public Overridable Function FileRegcassa() As Boolean
    Dim strParent As String = "BSVEBOLL"
    Dim lProfilo As Integer = 0
    Dim strTabellaIN As String = ""
    Dim strTabellaOU As String = ""
    Dim strPath As String = ""
    Dim bManca As Boolean = False
    Dim lTimeOut As Integer = 0
    Dim bIEGeneraFileSarema As Boolean = False
    Dim strIENomeFileSarema As String = ""
    Dim bIEScontiAValore As Boolean = False
    Dim bIEEsponiValoreNetto As Boolean = False
    Dim bIEEsponiTipoPag As Boolean = False
    Dim strCommandShell As String = ""
    Dim strWHERE As String = ""

    Dim oCleImex As CLEIEIMEX = Nothing

    Try
      If BORDINI Then strParent = "BSORGSOR"

      'Legge le informazioni sul registro
      lProfilo = NTSCInt(oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "Opzioni", ".", "IEProfilo", "0", " ", ""))
      strTabellaIN = oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "Opzioni", ".", "IETabellaIN", "", " ", "")
      strTabellaOU = oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "Opzioni", ".", "IETabellaOU", "", " ", "")
      strPath = oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "Opzioni", ".", "IEDirFile", oApp.AscDir, " ", oApp.AscDir)
      lTimeOut = NTSCInt(oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "Opzioni", ".", "IEScriptTimeOut", "-2", ".", "-2"))
      strCommandShell = oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "Opzioni", ".", "ComandoRegCassa", "", " ", "")
      bIEGeneraFileSarema = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "Opzioni", ".", "IEGeneraFileSarema", "0", " ", "0")))
      strIENomeFileSarema = oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "Opzioni", ".", "IENomeFileSarema", "SR_DATI.001", " ", "SR_DATI.001")
      bIEScontiAValore = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "Opzioni", ".", "IEScontiAValore", "0", " ", "0")))
      bIEEsponiValoreNetto = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "Opzioni", ".", "IEEsponiValoreNetto", "0", " ", "0"))) 'se abilitata espone nella riga dello scontrino il valore riga (netto sconti) senza menzionale la quantità. Ha effetto solo se \Bsveboll\Opzioni\IEGeneraFileSarema ha valore -1 (modalità di generazione del file con lo scontrino diretta).
      bIEEsponiTipoPag = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, "Bsveboll", "Opzioni", ".", "IEEsponiTipoPag", "0", " ", "0"))) 'se abilitata espone nel piede del documento il tipo di pagamento Assegno o Carta di credito (quando scelti) al posto di contanti in modo fisso.
      If bLeggiOpzioniDoc Then
        lProfilo = NTSCInt(oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "OpzioniDoc", ".", "IEProfilo", "0", dttET.Rows(0)!et_tipork.ToString, lProfilo.ToString))
        strTabellaIN = oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "OpzioniDoc", ".", "IETabellaIN", "", dttET.Rows(0)!et_tipork.ToString, strTabellaIN)
        strTabellaOU = oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "OpzioniDoc", ".", "IETabellaOU", "", dttET.Rows(0)!et_tipork.ToString, strTabellaOU)
        strPath = oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "OpzioniDoc", ".", "IEDirFile", oApp.AscDir, dttET.Rows(0)!et_tipork.ToString, strPath)
        lTimeOut = NTSCInt(oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "OpzioniDoc", ".", "IEScriptTimeOut", "-2", dttET.Rows(0)!et_tipork.ToString, lTimeOut.ToString))
        strCommandShell = oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "OpzioniDoc", ".", "ComandoRegCassa", "", dttET.Rows(0)!et_tipork.ToString, strCommandShell)
        bIEGeneraFileSarema = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "OpzioniDoc", ".", "IEGeneraFileSarema", "0", dttET.Rows(0)!et_tipork.ToString, IIf(bIEGeneraFileSarema, "-1", "0").ToString)))
        strIENomeFileSarema = oCldDocu.GetSettingBusDitt(strDittaCorrente, strParent, "OpzioniDoc", ".", "IENomeFileSarema", "SR_DATI.001", dttET.Rows(0)!et_tipork.ToString, strIENomeFileSarema)
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly, l'opzione:
      '--- "Genera file SAREMA senza profilo I/E"
      '--- è resa sempre attiva
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then bIEGeneraFileSarema = True
      '--------------------------------------------------------------------------------------------------------------
      If (lProfilo = 0 Or Trim$(strTabellaIN) = "" Or Trim$(strTabellaOU) = "") And bIEGeneraFileSarema = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128693039537968750, "Mancano informazioni nella chiave del registro \BSVEBOLL\Opzioni\ (o \BSVEBOLL\opzioniDoc\ per l'esecuzione del profilo Import/Export (proprietà 'IEProfilo', 'IETabellaIN', 'IETabellaOU').")))
        Return False
      End If

      If bIEGeneraFileSarema Then
        'Genera il file SAREMA LANGUAGE direttamente
        If Not GeneraFileSarema(strPath, strIENomeFileSarema, dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                                   dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), False, _
                                                   bIEScontiAValore, bIEEsponiValoreNetto, bIEEsponiTipoPag) Then Return False
      Else
        'strPath = gstrDir & "\ASC"
        strWHERE = oCldDocu.GetWhereStampaRegCassa(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), _
                                                   dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), BORDINI)
        'vecchia chiamata che gestiva solo i profili fatti in VB6
        'If Not CType(CType(oCleComm, CLELBMENU).oMenu, CLE__MENU).RunIeImex(strDittaCorrente, lProfilo, strTabellaIN, strTabellaOU, strPath, strWHERE, lTimeOut) Then Return False
        '---------------------------------------------
        'inizializzo oCleImex
        Dim strErr As String = ""
        Dim oTmp As Object = Nothing
        If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEVEBOLL", "BEIEIMEX", oTmp, strErr, False, "", "") = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128722560569218750, "ERRORE in fase di creazione Entity 'BEIEIMEX'. Errore:") & vbCrLf & strErr))
        End If
        oCleImex = CType(oTmp, CLEIEIMEX)
        oCleImex.Init(oApp, CType(oCleComm, CLELBMENU).oScriptE, CType(oCleComm, CLELBMENU), "", False, "", "")
        oCleImex.strDittaCorrente = strDittaCorrente
        AddHandler oCleImex.RemoteEvent, AddressOf BeIeImex_GestisciEventiEntity

        If Not oCleImex.Esegui(lProfilo, strTabellaIN, strTabellaOU, strPath, strWHERE, 0, "", 0, "", strWHERE) Then Return False
      End If

      'Lancia la shell
      If Trim$(strCommandShell) <> "" Then
        Try
          System.Diagnostics.Process.Start(strCommandShell)
        Catch ex As Exception
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128693052021718750, "Errore durante l'esecuzione della Shell |" & strCommandShell & "|")))
        End Try
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
      If Not oCleImex Is Nothing Then RemoveHandler oCleImex.RemoteEvent, AddressOf BeIeImex_GestisciEventiEntity
    End Try
  End Function
  Public Overridable Sub BeIeImex_GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      If e.TipoEvento <> "GRIAGG" Then
        'giro il messaggio alla form 
        If e.TipoEvento = "IMEX_ERROR" Then
          ThrowRemoteEvent(New NTSEventArgs("", e.Message))
        End If

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
  End Sub

  Public Overridable Function GeneraFileSarema(ByVal strPath As String, ByVal strFile As String, ByVal strTmTipork As String, _
                                              ByVal nTmAnno As Integer, ByVal strTmSerie As String, ByVal lTmNumdoc As Integer, _
                                              ByVal bEsponiResto As Boolean, ByVal bScontiAValore As Boolean, _
                                              ByVal bEsponiValoreNetto As Boolean, ByVal bEsponiTipoPag As Boolean) As Boolean
    Dim strOutput As String, strLine As String, dQuant As Decimal
    Dim dAbbuo As Decimal, dTdoc As Decimal
    Dim strTipoRiga As String
    Dim bScontoSingolo As Boolean, dScont As Decimal
    Dim dValoreLordo As Decimal, dValoreNetto As Decimal, dValoreSconto As Decimal
    Dim strDescr As String, strIENomeFileSemaforoSarema As String
    Dim w1 As System.IO.StreamWriter = Nothing
    Dim dttTmp As New DataTable
    Dim strTipoPagSL As String = ""
    Dim nMaxCharDesArt As Integer = 0
    Dim bEsponiQtaEPrezzoComeDescrizione As Boolean = False
    Dim dScontV As Decimal

    Try

      bEsponiQtaEPrezzoComeDescrizione = CBool(oCldDocu.GetSettingBus("Bsveboll", "Opzioni", ".", "IEEsponiQtaEPrezzoComeDescrizione", "0", " ", "0")) '(opera solo se abilitata opzione IEEsponiValoreNetto) se abilitata espone nello scontrino, prima della riga con il valore, una descrizione che include la quantità, il prezzo e l'eventuale moltiplicatore. Utile quando abbiamo prezzi con > di 2 decimali ed in presenz a di moltiplicatore

      strIENomeFileSemaforoSarema = oCldDocu.GetSettingBusDitt(strDittaCorrente, "Bsveboll", "Opzioni", ".", "IENomeFileSemaforoSarema", "SR_START.001", " ", "SR_START.001")

      nMaxCharDesArt = NTSCInt(oCldDocu.GetSettingBusDitt(strDittaCorrente, "Bsveboll", "Opzioni", ".", "IEMaxCaratteriDescrArt", "10", " ", "10"))  'definisce il numero di caratteri massimo esposti nel file SR_DATi dello scontrino fiscale, per defeualt 10. Prima di modificare l'opzione verificare se il WPOS1 e/o il registratore di cassa in uso supporta in numero di caratteri che si vuole impostare. Valori ammessi da 10 a 40.
      If (nMaxCharDesArt < 10) Or (nMaxCharDesArt > 40) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128992266589776781, "L'opzione di registro \Bsveboll\Opzioni\IEMaxCaratteriDescrArt può assumere valori compresi tra 10 e 40. Impossibile continuare.")))
        Return False
      End If

      If dttEC.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128693111072187500, "Nessuna riga rilevata per la generazione del file per il registratore di cassa.")))
        Return False
      End If

      '-----------------------------------------------------------------------------------------
      'Esce se totale documento e abbuono sono <= 0
      If NTSCDec(dttET.Rows(0)!et_totdoc) <= 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128693111898125000, "Attenzione! corrispettivo con totale documento negativo o pari a zero. Stampa non possibile.")))
        Return False
      End If
      If NTSCDec(dttET.Rows(0)!et_abbuono) < 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128693112343906250, "Attenzione! corrispettivo con abbuono negativo. Stampa non possibile.")))
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      dAbbuo = NTSCDec(dttET.Rows(0)!et_abbuono)
      dTdoc = NTSCDec(dttET.Rows(0)!et_totdoc)
      '-----------------------------------------------------------------------------------------
      'Cancella il file se già esiste
      AddDirSep(strPath)
      If strPath.EndsWith("\") = False Then strPath += "\"
      strOutput = strPath & strFile

      If System.IO.File.Exists(strOutput) Then
        System.IO.File.Delete(strOutput)
      Else
        'ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130202776627907472, "ATTENZIONE!" & vbCrLf & _
        '  "Percorso/nome file:" & vbCrLf & _
        '  " . |" & strOutput & "|" & vbCrLf & _
        '  "NON validi." & vbCrLf & _
        '  "Stampa su registratore di cassa non possibile.")))
        'Return False
      End If

      '-----------------------------------------------------------------------------------------
      'Crea il file
      w1 = New System.IO.StreamWriter(strOutput, True)

      '-----------------------------------------------------------------------------------------
      'Loppa sulle righe
      'dPrezivaPrec = 0

      '-----------------------------------------------------------------------------------------
      For Each dtrT As DataRow In dttEC.Select("", "ec_riga")
        '-------------------------------------------------------------------------------------
        'Scarta la riga in alcuni casi
        If ((NTSCDec(dtrT!ec_preziva) * 100) = 0) Or (NTSCDec(dtrT!ec_quant) = 0) Then GoTo SaltaRiga
        '-------------------------------------------------------------------------------------
        'Salta se prezzi negativi
        If (NTSCDec(dtrT!ec_preziva) * 100) < 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128693117972187500, "ATTENZIONE! Riga |" & dtrT!ec_riga.ToString & "|, articolo |" & UCase(dtrT!ec_descr.ToString) & "| con prezzo negativo. Riga scartata dalla stampa dello scontrino.")))
          GoTo SaltaRiga
        End If
        '-------------------------------------------------------------------------------------
        'Salta con elevate qtà
        dQuant = Math.Abs(ArrDbl(NTSCDec(dtrT!ec_quant), 3))
        If dQuant > 9999 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128693119386250000, "Attenzione! impossibile stampare su registratore di cassa quantità superiori a 9999 (quantità indicata nella riga |" & dtrT!ec_riga.ToString & "|: |" & NTSCDec(dtrT!ec_quant).ToString(oApp.FormatQta) & "|). Questa riga verrà scartata dalla stampa.")))
          GoTo SaltaRiga
        End If
        '-------------------------------------------------------------------------------------
        'Salta con moltiplicatori <> 1 (solo se non è abilitata l'opzione bEsponiValoreNetto)
        If Not bEsponiValoreNetto Then
          If NTSCInt(dtrT!ec_perqta) <> 1 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128693119764062500, "Attenzione! impossibile stampare su registratore di cassa prezzi con moltiplicatore diverso da 1 (moltiplicatore indicato nella riga |" & dtrT!ec_riga.ToString & "| : |" & dtrT!ec_perqta.ToString & "|). Questa riga verrà scartata dalla stampa.")))
            GoTo SaltaRiga
          End If
          If ArrDbl(NTSCDec(dtrT!ec_preziva), 2) <> NTSCDec(dtrT!ec_preziva) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128693119753125000, "Attenzione! impossibile stampare su registratore di cassa prezzi con un numero di decimali superiore a 2 (prezzo indicato nella riga |" & dtrT!ec_riga.ToString & "| : |" & NTSCDec(dtrT!ec_preziva).ToString(oApp.FormatPrzUn) & "|). Questa riga verrà scartata dalla stampa.")))
            GoTo SaltaRiga
          End If
        End If
        '-------------------------------------------------------------------------------------
        'Scarta le righe con sconti negativi
        '-------------------------------------------------------------------------------------
        If Not bEsponiValoreNetto And (NTSCDec(dttET.Rows(0)!et_scont1) < 0 Or NTSCDec(dttET.Rows(0)!et_scont2) < 0 Or NTSCDec(dttET.Rows(0)!et_scopag) < 0 Or NTSCDec(dtrT!ec_scont1) < 0 Or NTSCDec(dtrT!ec_scont2) < 0 Or NTSCDec(dtrT!ec_scont3) < 0 Or NTSCDec(dtrT!ec_scont4) < 0 Or NTSCDec(dtrT!ec_scont5) < 0 Or NTSCDec(dtrT!ec_scont6) < 0) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128693121291093750, _
          "ATTENZIONE! Riga |" & dtrT!ec_riga.ToString & "|, articolo |" & UCase(dtrT!ec_descr.ToString) & _
          "| con uno o più sconti negativi (maggiorazioni). Riga scartata dalla stampa dello scontrino.")))
          GoTo SaltaRiga
        End If
        '-------------------------------------------------------------------------------------
        '--- Salta con elevate qtà
        '-------------------------------------------------------------------------------------
        If (NTSCDec(dtrT!ec_preziva) * 100) > 999999999 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128693121642812500, _
          "Attenzione! impossibile stampare su registratore di cassa prezzi superiori a 9999999.99 (prezzo indicato nella riga |" & _
          dtrT!ec_riga.ToString & "| : |" & ((NTSCDec(dtrT!ec_preziva) * 100) / 100).ToString(oApp.FormatPrzUn) & "|). Questa riga verrà scartata dalla stampa.")))
          GoTo SaltaRiga
        End If
        '-------------------------------------------------------------------------------------
        '--- Salta le righe di SUBTOTALE
        '-------------------------------------------------------------------------------------
        strTipoRiga = "" 'ntscstr(!mms_tiporiga)
        If strTipoRiga = "" Then strTipoRiga = " " 'riga normale di articolo
        If UCase(strTipoRiga) = "Z" Then
          GoTo SaltaRiga
        End If
        '-------------------------------------------------------------------------------------
        'Scrive RESO se la qtà è negativa
        'Per adesso l'abbuono viene trattato come articolo normale

        'If (!ec_quant < 0) And (!mms_tiporiga = " ") Then
        '  w1.WriteLine("RESO;"
        'End If
        'Select Case !mms_tiporiga
        Select Case UCase(strTipoRiga)
          'Case "S"
          '  dPercSconto = ArrDbl(((!ec_preziva / dPrezivaPrec) * 100), 2)
          '  strScInt = Left(CStr(dPercSconto), 2)
          '  strScDec = Mid(CStr(dPercSconto), (InStr(1, CStr(dPercSconto), ",") + 1))
          '  w1.WriteLine("DISC,% " & strScInt & "." & strScDec
          Case "A", "S"
            w1.WriteLine("COUP,V" & ArrDbl((NTSCDec(dtrT!ec_preziva) * 100), 0) & ";")
          Case Else
            '-------------------------------------------------------------------------------------
            'Compone la riga
            strLine = "PLUD,CODE[" 'filler1
            strLine = strLine & Left(NTSCStr(dtrT!ec_matric) & "             ", 13) 'barcode
            strLine = strLine & "],n" 'filler2
            'Prima verifica se è impostato il reparto sui Codici IVA
            Dim strReparto As String
            Dim dtrIva As DataRow = GetTabcivaRow(NTSCInt(dtrT!ec_codiva))
            If NTSCInt(dtrIva!tb_reparto) <> 0 Then
              strReparto = NTSCInt(dtrIva!tb_reparto).ToString("00")
            Else
              oCldDocu.ValCodiceDb(dtrT!ec_codart.ToString, strDittaCorrente, "ARTICO", "S", "", dttTmp)
              strReparto = NTSCInt(IIf(NTSCInt(dttTmp.Rows(0)!ar_reparto) = 0, 1, NTSCInt(dttTmp.Rows(0)!ar_reparto))).ToString("00") 'reparto
            End If
            strLine &= strReparto

            dttTmp.Clear()
            If bEsponiValoreNetto Then
              'Espone solo il valore netto senza qtà e sconti
              dValoreNetto = ArrDbl(ArrDbl(NTSCDec(dtrT!ec_preziva) * NTSCDec(dtrT!ec_quant) / NTSCInt(dtrT!ec_perqta) * (100 - NTSCDec(dtrT!ec_scont1)) / 100 * (100 - NTSCDec(dtrT!ec_scont2)) / 100 * (100 - NTSCDec(dtrT!ec_scont3)) / 100 * (100 - NTSCDec(dtrT!ec_scont4)) / 100 * (100 - NTSCDec(dtrT!ec_scont5)) / 100 * (100 - NTSCDec(dtrT!ec_scont6)) / 100, 2) * (100 - NTSCDec(dttET.Rows(0)!et_scont1)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scont2)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scopag)) / 100, 2)
              strLine = strLine & ",P" 'filler3

              Dim strValore As String = (dValoreNetto * 100).ToString
              If InStr(strValore, System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.CurrencyDecimalSeparator) <> 0 Then
                strValore = Mid(strValore, 1, InStr(strValore, System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.CurrencyDecimalSeparator) - 1)
              End If
              strLine = strLine & strValore 'valore riga netto

              If bEsponiQtaEPrezzoComeDescrizione Then
                'Riga descrittiva prima dell'articolo
                w1.WriteLine("PRNT,: " & NTSCDec(dtrT!ec_quant).ToString.Replace(",", ".") & " X " & dtrT!ec_preziva.ToString.Replace(",", ".") & ";")
                If ArrDbl(NTSCDec(dtrT!ec_perqta), 3) <> 1 Then
                  w1.WriteLine("PRNT,: Moltipl. prezzo/q.ta " & NTSCDec(dtrT!ec_perqta).ToString.Replace(",", ".") & ";")
                End If
              End If

            Else
              'Caso normale: espone sia quantità, che valore e poi gli sconti
              strLine = strLine & ",P" 'filler3
              strLine = strLine & ArrDbl((NTSCDec(dtrT!ec_preziva) * 100), 0) 'prezzo
              strLine = strLine & ",q" 'filler4
              strLine = strLine & CDblSQL(dQuant)  'quantità (punto al posto della virgola)
            End If
            strLine = strLine & ",:" 'filler5
            'Sostituisce caratteri ' , " : che possono dare problemi con alcuni rec di cassa
            strDescr = NTSCStr(dtrT!ec_descr)
            strDescr = Replace(strDescr, "'", " ")
            strDescr = Replace(strDescr, ",", " ")
            strDescr = Replace(strDescr, """", " ")
            strDescr = Replace(strDescr, ":", " ")
            strLine = strLine & Left(strDescr & "".PadLeft(nMaxCharDesArt), nMaxCharDesArt) 'descrizione
            If (NTSCDec(dtrT!ec_quant) < 0) Then
              strLine = strLine & ",R" 'riga di reso
            End If
            strLine = strLine & ";" 'filler6
            w1.WriteLine(strLine)
            '-------------------------------------------------------------------------------------
            'Scrive lo sconto
            'If (dttEt.rows(0)!et_scont1 + dttEt.rows(0)!et_scont2 + dttEt.rows(0)!et_scopag + !ec_scont1 + !ec_scont2 + !ec_scont3 + !ec_scont4 + !ec_scont5 + !ec_scont6) > 0 Then
            '  lScInt = CLng(Int(100 - ((100 - !ec_scont1) / 100 * (100 - !ec_scont2) / 100 * (100 - !ec_scont3) / 100 * (100 - !ec_scont4) / 100 * (100 - !ec_scont5) / 100 * (100 - !ec_scont6) / 100 * (100 - dttEt.rows(0)!et_scont1) / 100 * (100 - dttEt.rows(0)!et_scont2) / 100 * (100 - dttEt.rows(0)!et_scopag) / 100) * 100))
            '  strScInt = Left(CStr(lScInt), 2)
            '  lScDec = CLng((100 - ((100 - !ec_scont1) / 100 * (100 - !ec_scont2) / 100 * (100 - !ec_scont3) / 100 * (100 - !ec_scont4) / 100 * (100 - !ec_scont5) / 100 * (100 - !ec_scont6) / 100 * (100 - dttEt.rows(0)!et_scont1) / 100 * (100 - dttEt.rows(0)!et_scont2) / 100 * (100 - dttEt.rows(0)!et_scopag) / 100) * 100 - Int(100 - ((100 - !ec_scont1) / 100 * (100 - !ec_scont2) / 100 * (100 - !ec_scont3) / 100 * (100 - !ec_scont4) / 100 * (100 - !ec_scont5) / 100 * (100 - !ec_scont6) / 100 * (100 - dttEt.rows(0)!et_scont1) / 100 * (100 - dttEt.rows(0)!et_scont2) / 100 * (100 - dttEt.rows(0)!et_scopag) / 100) * 100)) * 10)
            '  strScDec = Left(CStr(lScDec), 2)
            '  w1.WriteLine("DISC,% " & strScInt & "." & strScDec
            'End If

            'Non fa nulla se non ci sono sconti
            If NTSCDec(dttET.Rows(0)!et_scont1) = 0 And NTSCDec(dttET.Rows(0)!et_scont2) = 0 And NTSCDec(dttET.Rows(0)!et_scopag) = 0 And NTSCDec(dtrT!ec_scont1) = 0 And NTSCDec(dtrT!ec_scont2) = 0 And NTSCDec(dtrT!ec_scont3) = 0 And NTSCDec(dtrT!ec_scont4) = 0 And NTSCDec(dtrT!ec_scont5) = 0 And NTSCDec(dtrT!ec_scont6) = 0 Then
              GoTo SaltaRiga
            End If

            'Se abilitata l'opzione di registro bEsponiValoreNetto gli sconti sono già stati trattati prima
            If bEsponiValoreNetto Then
              GoTo SaltaRiga
            End If

            'Determina se c'è uno o più sconti
            bScontoSingolo = True
            dScont = 0
            If NTSCDec(dttET.Rows(0)!et_scont1) > 0 Then
              dScont = NTSCDec(dttET.Rows(0)!et_scont1)
            End If
            If NTSCDec(dttET.Rows(0)!et_scont2) > 0 Then
              If dScont > 0 Then bScontoSingolo = False
              If bScontoSingolo Then dScont = NTSCDec(dttET.Rows(0)!et_scont2)
            End If
            If NTSCDec(dttET.Rows(0)!et_scopag) > 0 Then
              If dScont > 0 Then bScontoSingolo = False
              If bScontoSingolo Then dScont = NTSCDec(dttET.Rows(0)!et_scopag)
            End If
            If NTSCDec(dtrT!ec_scont1) > 0 Then
              If dScont > 0 Then bScontoSingolo = False
              If bScontoSingolo Then dScont = NTSCDec(dtrT!ec_scont1)
            End If
            If NTSCDec(dtrT!ec_scont2) > 0 Then
              If dScont > 0 Then bScontoSingolo = False
              If bScontoSingolo Then dScont = NTSCDec(dtrT!ec_scont2)
            End If
            If NTSCDec(dtrT!ec_scont3) > 0 Then
              If dScont > 0 Then bScontoSingolo = False
              If bScontoSingolo Then dScont = NTSCDec(dtrT!ec_scont3)
            End If
            If NTSCDec(dtrT!ec_scont4) > 0 Then
              If dScont > 0 Then bScontoSingolo = False
              If bScontoSingolo Then dScont = NTSCDec(dtrT!ec_scont4)
            End If
            If NTSCDec(dtrT!ec_scont5) > 0 Then
              If dScont > 0 Then bScontoSingolo = False
              If bScontoSingolo Then dScont = NTSCDec(dtrT!ec_scont5)
            End If
            If NTSCDec(dtrT!ec_scont6) > 0 Then
              If dScont > 0 Then bScontoSingolo = False
              If bScontoSingolo Then dScont = NTSCDec(dtrT!ec_scont6)
            End If

            'Se abilitata opzione di registro \bsveboll\opzioni\IEScontiAValore nello scontrino
            'espone lo sconto non in % ma a valore per evitare differenze sul totale scontrino
            'dovuti a calcoli diversi dello sconto nel reg. di cassa (anche se 1 solo sconto presente)
            If bScontiAValore Then bScontoSingolo = False

            'Se c'è un solo sconto lo applica con una DISC,%
            If bScontoSingolo And dScont > 0 Then
              dScont = ArrDbl(dScont, 2) 'Comunque deve essere già a 2
              w1.WriteLine("DISC,%" & CDblSQL(dScont) & ";")
            End If

            'Se ci sono più sconti li trasforma a valore e li applica con una COUP,V
            'Questo evita inevitabili problemi di differenze dovute ad arrotondamenti
            If Not bScontoSingolo Then
              If NTSCDec(dtrT!ec_scontv) <> 0 And NTSCStr(dttET.Rows(0)!et_scorpo) = "S" Then
                oCldDocu.Scorporo(NTSCDec(dtrT!ec_scontv), NTSCInt(dtrT!ec_codiva), dScontV, 0)
              Else
                dScontV = NTSCDec(dtrT!ec_scontv)
              End If

              dValoreNetto = ArrDbl(ArrDbl(NTSCDec(dtrT!ec_preziva) * NTSCDec(dtrT!ec_quant) * (100 - NTSCDec(dtrT!ec_scont1)) / 100 * (100 - NTSCDec(dtrT!ec_scont2)) / 100 * (100 - NTSCDec(dtrT!ec_scont3)) / 100 * (100 - NTSCDec(dtrT!ec_scont4)) / 100 * (100 - NTSCDec(dtrT!ec_scont5)) / 100 * (100 - NTSCDec(dtrT!ec_scont6)) / 100 * (100 - NTSCDec(dtrT!ec_scontp)) / 100 - dScontV, 2) * (100 - NTSCDec(dttET.Rows(0)!et_scont1)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scont2)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scopag)) / 100, 2)
              dValoreLordo = ArrDbl(NTSCDec(dtrT!ec_preziva) * NTSCDec(dtrT!ec_quant), 2)
              dValoreSconto = ArrDbl(dValoreLordo - dValoreNetto, 2)
              If dValoreSconto > 0 Then
                w1.WriteLine("COUP,V" & dValoreSconto * 100 & ";")
              End If
            End If
        End Select
        '-------------------------------------------------------------------------------------
SaltaRiga:
        '-------------------------------------------------------------------------------------
        'Memorizza il prezzo iva (per gli sconti) precedente
        'dPrezivaPrec = !ec_preziva
      Next    'For Each dtrT As DataRow In dttEC.Select("", "ec_riga")

      '---------------------------------------------------------------------------------------
      '--- Scrive l'abbuono
      '---------------------------------------------------------------------------------------
      w1.WriteLine("CLER,C001;")
      If dAbbuo > 0 Then
        w1.WriteLine("COST,V" & CStr(dAbbuo * 100) & ";")
      End If
      '---------------------------------------------------------------------------------------
      '--- Scrive il tipo pagamento, specifico per BSVEBOLL
      '---------------------------------------------------------------------------------------
      If bEsponiTipoPag Then
        strTipoPagSL = "CASH"
        oCldDocu.ValCodiceDb(dttET.Rows(0)!et_codpaga.ToString, strDittaCorrente, "TABPAGA", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          Select Case NTSCStr(dttTmp.Rows(0)!tb_tipincecr).ToUpper
            Case "A" : strTipoPagSL = "CHEQ" 'assegno
            Case "V" : strTipoPagSL = "CARD" 'carta di credito
            Case "B" : strTipoPagSL = "TEND,N4" 'bancomat
            Case "F" : strTipoPagSL = "TEND,N5" 'finanziamento
            Case "Z" : strTipoPagSL = "TEND,N6" 'altro
            Case Else : strTipoPagSL = "CASH" 'Negli altri casi lascia CASH (contanti, non definito)
          End Select
        End If
        dttTmp.Clear()
        w1.WriteLine(strTipoPagSL & ";")
      Else
        '---------------------------------------------------------------------------------------
        '--- Caso normale: espone sempre CASH;
        '---------------------------------------------------------------------------------------
        w1.WriteLine("CASH;")
      End If
      '-----------------------------------------------------------------------------------------
      'Chiude il file
      w1.Flush()
      w1.Close()

      '-----------------------------------------------------------------------------------------
      'Copia il file marker
      'Crea nella cartella dove viene creato il file dati
      'il file SR_START.001 (001=COM1) con dentro la password fornita da RTS
      'lo crea per copia di un file SR_STARX.001 che DEVE già esistere
      Try
        System.IO.File.Copy(strPath & "SR_STARX.001", strPath & strIENomeFileSemaforoSarema)
      Catch ex As Exception
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128693137477500000, "Errore durante la rimonima del file |" & UCase(strPath & "SR_STARX.001") & "| in |" & UCase(strPath & strIENomeFileSemaforoSarema) & "|. ") & vbCrLf & vbCrLf & ex.Message))
      End Try

      Return True

    Catch ex As Exception
      If Not w1 Is Nothing Then w1.Close()
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function CheckPIvaCFisCliente(ByVal dttConto As DataTable) As Boolean
    Try
      If strControlloPIvaCodFis <> "0" Then
        If NTSCStr(dttET.Rows(0)!et_tipork) = "A" Or NTSCStr(dttET.Rows(0)!et_tipork) = "E" Or NTSCStr(dttET.Rows(0)!et_tipork) = "N" Or _
           NTSCStr(dttET.Rows(0)!et_tipork) = "S" Or NTSCStr(dttET.Rows(0)!et_tipork) = "D" Or NTSCStr(dttET.Rows(0)!et_tipork) = "P" Or _
           NTSCStr(dttET.Rows(0)!et_tipork) = "£" Then

          If dttConto Is Nothing OrElse dttConto.Rows.Count = 0 Then
            dttConto = New DataTable
            oCldDocu.ValCodiceDb(NTSCStr(dttET.Rows(0)!et_conto), strDittaCorrente, "ANAGRA", "N", "", dttConto)
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

  Public Overridable Function ZoomListini(ByVal strDitta As String, ByRef dsOut As DataSet, _
                                       ByVal strCodart As String, ByVal nFase As Integer, _
                                       ByVal lConto As Integer, ByVal bSoloEuro As Boolean, _
                                       ByVal nValuta As Integer, _
                                       ByVal bListinoCorrente As Boolean, ByVal strDtValid As String, _
                                       ByVal lFornitore1 As Integer, ByVal lFornitore2 As Integer) As Boolean
    Try
      Return oCldDocu.ZoomListini(strDitta, dsOut, strCodart, nFase, lConto, bSoloEuro, _
                                  nValuta, bListinoCorrente, strDtValid, _
                                  lFornitore1, lFornitore2)
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
  Public Overridable Function ZoomListini_edCodvalu_Validated(ByVal nCodvalu As Integer, ByRef strDesvaluta As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCodvalu = 0 Then
        strDesvaluta = ""
        Return True
      End If

      bOut = oCldDocu.ValCodiceDb(nCodvalu.ToString, strDittaCorrente, "TABVALU", "N", strDesvaluta)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127965883462031250, "Codice valuta |'" & nCodvalu.ToString & "'| inesistente")))
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
  Public Overridable Function ZoomListini_edCodart_Validated(ByVal strCod As String, ByRef strDescr As String, _
                                        ByRef nFase As Integer, ByRef lForn1 As Integer, ByRef lForn2 As Integer) As Boolean
    Dim bOut As Boolean = False
    Dim dttTmp As New DataTable
    Try
      If strCod = "" Then
        strDescr = ""
        Return True
      End If

      bOut = oCldDocu.ValCodiceDb(strCod, strDittaCorrente, "ARTICO", "S", strDescr, dttTmp)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128570517775000000, "Codice articolo |'" & strCod & "'| inesistente")))
        Return False
      Else
        nFase = NTSCInt(dttTmp.Rows(0)!ar_ultfase)
        lForn1 = NTSCInt(dttTmp.Rows(0)!ar_forn)
        lForn2 = NTSCInt(dttTmp.Rows(0)!ar_forn2)
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
  Public Overridable Function TrovaPromozioniPerArticoloCliente(ByVal strDitta As String, ByVal strCodart As String, ByVal lConto As Integer, _
                                                                ByVal bValidoGiorno As Boolean, ByVal dtDataVal As Date, ByRef dttPromo As DataTable) As Boolean
    Try
      Return oCldDocu.TrovaPromozioniPerArticoloCliente(strDitta, strCodart, lConto, bValidoGiorno, dtDataVal, dttPromo)
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

  '-------------------------------------
  Public Overridable Sub BeforeColUpdate_CORPOTC(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return
    Dim strErr As String = ""
    Try
      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
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
      Dim strFunction As String = "BeforeColUpdate_CORPOTC_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub AfterColUpdate_CORPOTC(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return

    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChangesET = True
      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_CORPOTC_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub BeforeColUpdate_CORPOIMPTC(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return
    Dim strErr As String = ""
    Try
      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
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
      Dim strFunction As String = "BeforeColUpdate_CORPOIMPTC_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub AfterColUpdate_CORPOIMPTC(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return

    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If


      bHasChangesET = True
      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()


      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_CORPOIMPTC_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub BeforeColUpdate_PROESEC(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return
    Dim strErr As String = ""
    Try
      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
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
      Dim strFunction As String = "BeforeColUpdate_PROESEC_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub AfterColUpdate_PROESEC(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return

    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChangesET = True
      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_PROESEC_" & e.Column.ColumnName.ToLower
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

  Public Overridable Function AggiungiRigaCorpo(ByVal bScarichi As Boolean, ByVal strCodart As String, _
                                                ByVal nFase As Integer, ByVal nRiga As Integer, _
                                                Optional ByVal nCausale As Integer = 0, Optional ByVal nMagazzino As Integer = 0) As Boolean
    'bScarichi True Se riga di corpo scarichi False riga di corpo
    'nRiga se 0 la calcola in automatico
    Try
      If bScarichi = False Then
        dttEC.Rows.Add(dttEC.NewRow)
        With dttEC.Rows(dttEC.Rows.Count - 1)
          !codditt = "."
          !codditt = strDittaCorrente
          If nRiga <> 0 Then
            !ec_riga = nRiga
          End If
          If nMagazzino <> 0 Then
            !ec_magaz = nMagazzino
          Else
            !ec_magaz = dttET.Rows(0)!et_magaz
          End If

          If BORDINI = False Then
            If nCausale <> 0 Then
              !ec_causale = nCausale
            Else
              !ec_causale = dttET.Rows(0)!et_causale
            End If
          End If
          !ec_codart = strCodart
          !ec_fase = nFase
        End With
      Else
        dsImpe.Tables("CORPOIMP").Rows.Add(dsImpe.Tables("CORPOIMP").NewRow)
        With dsImpe.Tables("CORPOIMP").Rows(dsImpe.Tables("CORPOIMP").Rows.Count - 1)
          !codditt = "."
          !codditt = strDittaCorrente
          If nRiga <> 0 Then
            !ec_riga = nRiga
          End If
          If nMagazzino <> 0 Then
            !ec_magaz = nMagazzino
          Else
            !ec_magaz = dttET.Rows(0)!et_magaz
          End If

          If BORDINI = False Then
            If nCausale <> 0 Then
              !ec_causale = nCausale
            Else
              !ec_causale = dttET.Rows(0)!et_causale
            End If
          End If
          !ec_codart = strCodart
          !ec_fase = nFase
        End With
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

  Public Overridable Function CheckAperturaSchetrasp(ByVal bInSalvataggio As Boolean, ByRef bAnnullaSalvataggio As Boolean) As Boolean
    Dim evt As NTSEventArgs = Nothing
    Dim dttTmp As New DataTable
    Try
      'se da elaborzione esce
      If bInNuovoDocSilent Or bInApriDocSilent Then Return False

      'no i carichi/scarichi, note di prelievo
      If NTSCStr(dttET.Rows(0)!et_tipork) = "T" Or NTSCStr(dttET.Rows(0)!et_tipork) = "U" _
        Or NTSCStr(dttET.Rows(0)!et_tipork) = "H" Or NTSCStr(dttET.Rows(0)!et_tipork) = "Y" _
        Or NTSCStr(dttET.Rows(0)!et_tipork) = "W" Then
        If bInSalvataggio = False Then ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129042252351215542, "Compilazione della scheda di trasporto non ammessa per questo tipo di documento")))
        Return False
      End If

      'se al salvataggio è gia compilata la scheda di trasporto 
      'controlla la scheda se no errori esce
      If bInSalvataggio = True And dttSCHETRASP.Rows.Count > 0 Then
        If bConai Then
          bArtConai = PresenzaArtConai(dttEC)
        Else
          bArtConai = False
        End If
        If CheckCampiSchetrasp(dttSCHETRASP.Rows(0), dttET.Rows(0)) Then
          Return False
        Else
          bAnnullaSalvataggio = True
          Return False
        End If
      End If

      'test opzione registro
      If (bInSalvataggio = True And bConfermacompilazioneSchedaTrasp = False) Then Return False

      If bInSalvataggio = True Then
        evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128998069823056143, "Aprire la scheda di trasporto?"))
        ThrowRemoteEvent(evt)
        If Not evt.RetValue = "YES" Then
          Return False
        End If
      End If

      'check aver inserito il cliente
      If NTSCInt(dttET.Rows(0)!et_conto) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129042193273865040, "Per utilizzare la scheda di trasporto" & vbCrLf & _
          "valorizzare il conto del documento")))
        Return False
      End If

      'check almeno uno dei due vettori inseriti
      If NTSCInt(dttET.Rows(0)!et_vettor) = 0 And NTSCInt(dttET.Rows(0)!et_vettor2) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128989610363503535, "Per utilizzare la scheda di trasporto" & vbCrLf & _
          "valorizzare il vettore 1 o il vettore 2 del documento")))
        Return False
      End If

      'controllo che il vettore 1 o il vettore 2 abbiano il codice fornitore valorizzato
      If NTSCInt(dttET.Rows(0)!et_vettor) <> 0 Then
        If oCldDocu.ValCodiceDb(NTSCStr(dttET.Rows(0)!et_vettor), strDittaCorrente, "TABVETT", "N", , dttTmp) Then
          If NTSCInt(dttTmp.Rows(0)!tb_codforn) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129026002167415285, "Per utilizzare la scheda di trasporto" & vbCrLf & _
              "valorizzare il codice fornitore della tabella vettori/spedizionieri del vettore 1 indicato nel documento")))
            Return False
          End If
        End If
      End If

      If NTSCInt(dttET.Rows(0)!et_vettor2) <> 0 Then
        If oCldDocu.ValCodiceDb(NTSCStr(dttET.Rows(0)!et_vettor2), strDittaCorrente, "TABVETT", "N", , dttTmp) Then
          If NTSCInt(dttTmp.Rows(0)!tb_codforn) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129026008742837478, "Per utilizzare la scheda di trasporto" & vbCrLf & _
              "valorizzare il codice fornitore della tabella vettori/spedizionieri del vettore 2 indicato nel documento")))
            Return False
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

  Public Overridable Function GetDescrTaglia(ByVal lCodtagl As Integer, ByVal lNumtagl As Integer) As String
    Dim dttTmp As New DataTable
    Try
      oCldDocu.ValCodiceDb(lCodtagl.ToString, strDittaCorrente, "TABTAGL", "N", , dttTmp)

      If dttTmp.Rows.Count = 0 Then Return ""

      Return NTSCStr(dttTmp.Rows(0)("tb_dest" & lNumtagl.ToString("00")))
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

  Public Overridable Function DocumentLockAdd(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                               ByVal lNumdoc As Integer, ByVal lVers As Integer, ByVal bReset As Boolean) As Boolean
    'memorizzo in ttkeys il fatto che il documento è aperto dall'operatore corrente
    'in questo modo se un altro operatore apre lo stesso doc può venir avvisato/bloccato
    Try
      'se non devo fare nulla, esco
      If nControllaConcorrenzaOggetti <> 1 And nControllaConcorrenzaOggetti <> -1 Then Return True

      If bReset Then dtControllaConcorrenzaOggettiDataLock = DateTime.Now

      If lNumdoc = 0 Then Return True

      If Not oCldDocu.DocumentLockAdd(strDittaCorrente, dtControllaConcorrenzaOggettiDataLock, _
                                      strTipork, nAnno, strSerie, lNumdoc, lVers) Then Return False

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
  Public Overridable Function DocumentLockRemove() As Boolean
    'cancello da ttkeys il fatto che il documento è aperto dall'operatore corrente
    Try
      'se non devo fare nulla, esco
      If nControllaConcorrenzaOggetti <> 1 And nControllaConcorrenzaOggetti <> -1 Then Return True
      If Not oCldDocu.DocumentLockRemove(strDittaCorrente, dtControllaConcorrenzaOggettiDataLock) Then Return False
      DocumentLockRemove = False

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
  Public Overridable Function DocumentLockCheck(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                                               ByVal lNumdoc As Integer, ByVal lVers As Integer, ByRef strMsgOut As String) As Boolean
    'verifico se il documento passato in input è loccato da un operatore
    Try
      bDocumentLockNoSave = False
      If nControllaConcorrenzaOggetti <> 1 And nControllaConcorrenzaOggetti <> -1 Then Return True
      Return oCldDocu.DocumentLockCheck(strDittaCorrente, dtControllaConcorrenzaOggettiDataLock, strTipork, nAnno, strSerie, lNumdoc, lVers, strMsgOut)

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

  Public Overridable Function VisMsg() As Boolean
    Try
      If bInImportRigheOff Then Return False
      If bInImportRigheOrd Then Return False
      If bInInsertArticoDaZoom Then Return False
      If bInCreaDocDaGnor Then Return False
      If bInImportKit Then Return False
      If bInImportConai Then Return False
      If bInDuplicadoc Then Return False
      If bInIncollaRiga Then Return False
      If bInImportRigheNota Then Return False
      If bInImportRigheDiba Then Return False
      If bInNuovoDocSilent Then Return False
      If bInApriDocSilent Then Return False

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

  Public Overridable Function CheckCrm(ByRef bRipristina As Boolean) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      bRipristina = False
      bAccmod = True
      '--------------------------------------------------------------------------------------------------------------
      If bModuloCRM Then
        If bIsCRMUser Then
          If NTSCStr(dttET.Rows(0)!xx_tipo) = "C" Then
            bAccvis = CType(CType(oCleComm, CLELBMENU).oMenu, CLE__MENU).CercaAccessiCrmDaLead(strDittaCorrente, lLead, bAccmod)
            If bAccvis And BORDINI Then
              If bNew = False Then
                bAccvis = CType(CType(oCleComm, CLELBMENU).oMenu, CLE__MENU).CercaAccessiCrmDaTestord(strDittaCorrente, NTSCStr(dttET.Rows(0)!et_tipork), _
                                                                            NTSCInt(dttET.Rows(0)!et_anno), NTSCStr(dttET.Rows(0)!et_serie), _
                                                                            NTSCInt(dttET.Rows(0)!et_numdoc), bAccmod)
              End If
            End If
            If bAccvis = False Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129738658668770593, "Attenzione!" & vbCrLf & _
                "L'utente |'" & oApp.User.Nome & "'| NON è abilitato alla visualizzazione dei dati relativi al Cliente di questo documento.")))
              bRipristina = True
              Return False
            End If
          Else
            If bAmm = False Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129738658750736631, "Attenzione!" & vbCrLf & _
                "L'utente |'" & oApp.User.Nome & "'| NON è abilitato alla visualizzazione dei dati relativi ai fornitori.")))
              bRipristina = True
              Return False
            End If
          End If
        End If    ' If oCleBoll.bIsCRMUser Then
      End If    'If oCleBoll.bModuloCRM Then
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function


  Public Overridable Function SettaDatiCreazione() As Boolean
    Try
      If dttET.Columns.Contains("et_datcreaz") Then dttET.Rows(0)!et_datcreaz = Now.ToShortDateString
      If dttET.Columns.Contains("et_orcreaz") Then dttET.Rows(0)!et_orcreaz = NTSCDec(Now.Hour & "," & Now.Minute.ToString("00"))
      If dttET.Columns.Contains("et_opcreaz") Then dttET.Rows(0)!et_opcreaz = dttET.Rows(0)!et_opnome

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


  Public Overridable Function RicalcolaCondizioniDocumento(ByVal dtrCurrRow As DataRow) As Boolean
    Dim dtrRighe() As DataRow = Nothing
    Dim strProgr As String
    Dim bEliminaMessaggioCambioDatiTestOld As Boolean = bEliminaMessaggioCambioDatiTest
    Try
      bEliminaMessaggioCambioDatiTest = True
      bRicalcolaValoriRiga = True
      'Rileggo le opzioni di registro che ho settato come recent nel programma di configurazione
      If BORDINI Then
        strProgr = "BSORGSOR"
      Else
        strProgr = "BSVEBOLL"
      End If

      'Aggiorna i dati di testata
      If oCldDocu.GetSettingBus(strProgr, "RECENT", ".", "RicalcRileggiDaAnangra", "S", ".", "S") = "S" AndAlso NTSCInt(dttET.Rows(0)!et_conto) > 0 Then
        Dim dttClie As New DataTable
        If oCldDocu.ValCodiceDb(NTSCStr(dttET.Rows(0)!et_conto), strDittaCorrente, "ANAGRA", "N", "", dttClie) Then

          dttET.Rows(0)!et_codagen = dttClie.Rows(0)!an_agente
          dttET.Rows(0)!et_codagen2 = dttClie.Rows(0)!an_agente2

          dttET.Rows(0)!et_codpaga = CType(oCleComm, CLELBMENU).LeggiCodpagaCltipb(strDittaCorrente, NTSCInt(dttET.Rows(0)!et_conto), NTSCInt(dttET.Rows(0)!et_tipobf))
          If NTSCInt(dttET.Rows(0)!et_codpaga) = 0 Then dttET.Rows(0)!et_codpaga = dttClie.Rows(0)!an_codpag.ToString

          dttET.Rows(0)!et_abi = dttClie.Rows(0)!an_abi
          dttET.Rows(0)!et_cab = dttClie.Rows(0)!an_cab
          dttET.Rows(0)!et_banc1 = dttClie.Rows(0)!an_banc1
          dttET.Rows(0)!et_banc2 = dttClie.Rows(0)!an_banc2
          dttET.Rows(0)!et_codbanc = dttClie.Rows(0)!an_codbanc
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130409078765922094, "Conto non valido, i dati di testata non verrano riletti.")))
        End If
      End If

      'Preparo un array di DataRow con solo le righe da trattare
      Select Case oCldDocu.GetSettingBus(strProgr, "RECENT", ".", "RicalcRigheElab", "T", ".", "T")
        Case "C"
          If Not dtrCurrRow Is Nothing Then
            ReDim dtrRighe(0)
            dtrRighe(0) = dtrCurrRow
          End If
        Case "S"
          dtrRighe = dttEC.Select("xx_sel = 'S'")
        Case Else
          dtrRighe = dttEC.Select()
      End Select

      If dtrRighe.Length = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130409085846635801, "Non sono state trovate righe da trattare.")))
        Return False
      End If

      'Se devo aggioranre le condizioni del documento (prezzi, sconti, provvigioni)
      Dim bCalcolaPrezzi As Boolean = oCldDocu.GetSettingBus(strProgr, "RECENT", ".", "RicalcPrezzi", "S", ".", "S") = "S"
      Dim bCalcolaSconti As Boolean = oCldDocu.GetSettingBus(strProgr, "RECENT", ".", "RicalcSconti", "S", ".", "S") = "S"
      Dim bCalcolaProvv As Boolean = oCldDocu.GetSettingBus(strProgr, "RECENT", ".", "RicalcProvvigioni", "S", ".", "S") = "S"
      bCalcolaPrezzoScontoProvvAncheSeRigaEvade = oCldDocu.GetSettingBus(strProgr, "RECENT", ".", "AncheRigheEvase", "S", ".", "S") = "S"
      For Each dtrCurr As DataRow In dtrRighe
        Dim dttArticolo As New DataTable
        oCldDocu.ValCodiceDb(NTSCStr(dtrCurr!ec_codart), strDittaCorrente, "ARTICO", "S", "", dttArticolo)
        If dttArticolo.rows.count = 0 Then Continue For
        If dttECIMP.Rows.Count > 0 Then
          'Ricalcolo anche gli impegni collegati
          Dim dtrRow() As DataRow
          If BORDINI Then
            dtrRow = dttECIMP.Select("ec_rigaor = " & NTSCInt(dtrCurr!ec_riga))
          Else
            dtrRow = dttECIMP.Select("ec_prriga = " & NTSCInt(dtrCurr!ec_riga))
          End If
          For Each dtrCurrImp As DataRow In dtrRow
            SettaCondCommerciali(bCalcolaPrezzi, False, False, dtrCurrImp, 0, 0, False)
          Next
        End If

        'Se l'agente 1 o l'agente 2 non sono indicato devo azzerare i campi relativi
        SettaCondCommerciali(bCalcolaPrezzi, bCalcolaSconti, bCalcolaProvv, dtrCurr, NTSCInt(dttArticolo.Rows(0)!ar_clascon), nClscan, False)
        SettaValoriRiga(dtrCurr)
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
    Finally
      bCalcolaPrezzoScontoProvvAncheSeRigaEvade = False
      bRicalcolaValoriRiga = False
      bEliminaMessaggioCambioDatiTest = bEliminaMessaggioCambioDatiTestOld
    End Try
  End Function

  Public Overridable Function CalcolaScadenzeStandard(ByVal bVisMessageSaldo0 As Boolean) As Boolean
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
      If bDocDaRetail Then Return True ' La calcola scadenze viene fatta dal retail e non deve essere rifatta da gestione documenti

      With dttET.Rows(0)
        dRim = NTSCDec(!et_totdoc) - NTSCDec(!et_pagato) - NTSCDec(!et_abbuono) - NTSCDec(!et_pagato2) + NTSCDec(!et_resto) - NTSCDec(!et_totomag)
        dRimv = NTSCDec(!et_totdocv) - NTSCDec(!et_pagatov) - NTSCDec(!et_abbuonov) - NTSCDec(!et_totomagv)

        'Si entra solo se Rimanenza/rimanenzav > 0
        If dRim = 0 Then
          For i = 1 To 8
            dttET.Rows(0)("et_tippaga_" & i.ToString) = 0
            dttET.Rows(0)("et_datsca_" & i.ToString) = DBNull.Value
            dttET.Rows(0)("et_impsca_" & i.ToString) = 0
            dttET.Rows(0)("et_impscav_" & i.ToString) = 0
          Next
          If bVisMessageSaldo0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128669130316250000, "La modifica delle rate di scadenza è consentita solo documenti dove la rimanenza a pagare è maggiore di zero.")))
            Return False
          Else
            Return True
          End If
        End If
        '----------------------

        If BORDINI Then
          'la data pertenza è la data di consegna generale di testata ordine
          strDataPartenza = NTSCDate(dttET.Rows(0)!et_datcons).ToShortDateString
        Else
          'La data di partenza è sempre = Data documento (tranne se J, K, L, M, T dove usa tm_datpar, a meno di deroga con opzione di registro)
          'se il 'Tipo scadenza' del pagamento è 'Data diversa'
          'ed esiste la 'Data 1° pagamento', parte da quella
          strDataPartenza = CalcolaDataScadenza(NTSCStr(dttET.Rows(0)!et_tipork), NTSCStr(dttET.Rows(0)!et_datpar), NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString)
        End If

        oCldDocu.ValCodiceDb(NTSCInt(dttET.Rows(0)!et_codpaga).ToString, strDittaCorrente, "TABPAGA", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          If (NTSCInt(dttTmp.Rows(0)!tb_decpaga) = 4) And NTSCStr(dttET.Rows(0)!et_datapag).Trim <> "" Then strDataPartenza = NTSCDate(dttET.Rows(0)!et_datapag).ToShortDateString
        End If
        dttTmp.Clear()

        '----------------------
        'determino il totale IVA
        For i = 1 To 8
          dTotimp += NTSCDec(dttET.Rows(0)("et_imposta_" & i.ToString))
          dTotimpv += NTSCDec(dttET.Rows(0)("et_impostav_" & i.ToString))
        Next

        '----------------------
        'se documento ricevuto extracee ricalcolo le scadenze senza passare l'iva al tot. documento
        If dttET.Rows(0)!et_tipork.ToString = "K" Or dttET.Rows(0)!et_tipork.ToString = "L" Or _
            dttET.Rows(0)!et_tipork.ToString = "J" Or dttET.Rows(0)!et_tipork.ToString = "(" Then

          If NTSCInt(!et_codntra) <> 0 Or NTSCStr(!et_intra) <> "N" Then
            'Sui doc. ricevuti INTRA toglie l'iva dal calcolo scadenze
            'ricalcolo il totale imponibile
            dRim -= dTotimp
            dRimv -= dTotimpv
          Else
            oCldDocu.ValCodiceDb(dttET.Rows(0)!et_tipobf.ToString, strDittaCorrente, "TABTPBF", "N", "", dttTmp)
            If NTSCStr(dttTmp.Rows(0)!tb_fattrevch) = "S" Then
              dRim -= dTotimp
              dRimv -= dTotimpv
            ElseIf NTSCStr(dttTmp.Rows(0)!tb_fattrevch) = "M" Then
              'reverse charge misto: devo togliere solo l'iva dai codici di tipo tabciva.tb_revcharge <> 'N'
              dttTmp.Clear()
              For i = 1 To 8
                If NTSCInt(dttET.Rows(0)("et_codiva_" & i.ToString)) <> 0 Then
                  oCldDocu.ValCodiceDb(NTSCInt(dttET.Rows(0)("et_codiva_" & i.ToString)).ToString, strDittaCorrente, "TABCIVA", "N", "", dttTmp)
                  If NTSCStr(dttTmp.Rows(0)!tb_revcharge) <> "N" Then
                    dRim -= NTSCDec(dttET.Rows(0)("et_imposta_" & i.ToString))
                    dRimv -= NTSCDec(dttET.Rows(0)("et_impostav_" & i.ToString))
                  End If
                End If
              Next
            End If
            dttTmp.Clear()
          End If
        End If

        P.nCodpaga = NTSCInt(dttET.Rows(0)!et_codpaga)
        P.strDatrif = strDataPartenza
        P.dTotfat = dRim
        P.dTotfatval = dRimv
        P.dIva = dTotimp
        P.dIvaval = dTotimpv
        P.dSpese = NTSCDec(!et_speacc) + NTSCDec(!et_speinc) + NTSCDec(!et_bolli) + NTSCDec(!et_speimb)
        P.dSpeseval = NTSCDec(!et_speaccv) + NTSCDec(!et_speincv) + NTSCDec(!et_bolliv) + NTSCDec(!et_speimbv)

        If oCldDocu.CheckCausaleSplitPaymentFromTpbf(strDittaCorrente, NTSCInt(!et_tipobf)) Then
          'se la causale di CG è di tipo split payment (nuova iva differita con iva che verrà versata direttamente dall'ente pubblico)
          'nel calcolo delle scadenze non devo tener conto dell'IVA
          'NON E' GESTITO IL CASO DI FATTURE EMESSE IN VALUTA AD ENTE PUBBLICO
          P.dTotfat -= P.dIva
          dRim -= P.dIva
          P.dIva = 0
        End If

        nRate = CType(oCleComm, CLELBMENU).CalcolaScad(strDittaCorrente, P.nCodpaga, P.strDatrif, P.dTotfat, P.dTotfatval, P.dIva, _
                            P.dIvaval, P.dSpese, P.dSpeseval, P.strDatsca, P.dImpsca, P.dImpscaval, nTiprata, strErr, _
                            NTSCInt(dttET.Rows(0)!et_valuta), lContoCF)
        If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
        If nRate < 1 Or nRate > 8 Then
          If bVisMessageSaldo0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128669136551718750, "E' possibile modificare pagamenti con al massimo 8 rate.")))
            For i = 1 To 8
              dttET.Rows(0)("et_tippaga_" & i.ToString) = 0
              dttET.Rows(0)("et_datsca_" & i.ToString) = DBNull.Value
              dttET.Rows(0)("et_impsca_" & i.ToString) = 0
              dttET.Rows(0)("et_impscav_" & i.ToString) = 0
            Next
            Return False
          End If
        End If

        If bModifCastScad Then
          If (dRim <> ArrDbl(NTSCDec(!et_impsca_1) + NTSCDec(!et_impsca_2) + NTSCDec(!et_impsca_3) + NTSCDec(!et_impsca_4) + NTSCDec(!et_impsca_5) + NTSCDec(!et_impsca_6) + NTSCDec(!et_impsca_7) + NTSCDec(!et_impsca_8), oCldDocu.TrovaNdec(0))) Or _
             (dRimv <> ArrDbl(NTSCDec(!et_impscav_1) + NTSCDec(!et_impscav_2) + NTSCDec(!et_impscav_3) + NTSCDec(!et_impscav_4) + NTSCDec(!et_impscav_5) + NTSCDec(!et_impscav_6) + NTSCDec(!et_impscav_7) + NTSCDec(!et_impscav_8), oCldDocu.TrovaNdec(NTSCInt(!et_valuta))) And NTSCInt(!et_valuta) <> 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128674924592656250, "L'importo precedentemente ripartito sulle rate di scadenza è modificato pertanto le rate verranno ricalcolate.")))
            bModifCastScad = False
            For i = 1 To 8
              dttET.Rows(0)("et_tippaga_" & i.ToString) = 0
              dttET.Rows(0)("et_datsca_" & i.ToString) = DBNull.Value
              dttET.Rows(0)("et_impsca_" & i.ToString) = 0
              dttET.Rows(0)("et_impscav_" & i.ToString) = 0
            Next
            For i = 1 To nRate
              dttET.Rows(0)("et_tippaga_" & i.ToString) = nTiprata(i)
              dttET.Rows(0)("et_datsca_" & i.ToString) = P.strDatsca(i)
              dttET.Rows(0)("et_impsca_" & i.ToString) = P.dImpsca(i)
              dttET.Rows(0)("et_impscav_" & i.ToString) = P.dImpscaval(i)
              If i = 8 Then Exit For
            Next
            Return False
          End If
        Else
          'memorizzo su testmag. prima pulisco il vettore
          For i = 1 To 8
            dttET.Rows(0)("et_tippaga_" & i.ToString) = 0
            dttET.Rows(0)("et_datsca_" & i.ToString) = DBNull.Value
            dttET.Rows(0)("et_impsca_" & i.ToString) = 0
            dttET.Rows(0)("et_impscav_" & i.ToString) = 0
          Next
          For i = 1 To nRate
            dttET.Rows(0)("et_tippaga_" & i.ToString) = nTiprata(i)
            dttET.Rows(0)("et_datsca_" & i.ToString) = P.strDatsca(i)
            dttET.Rows(0)("et_impsca_" & i.ToString) = P.dImpsca(i)
            dttET.Rows(0)("et_impscav_" & i.ToString) = P.dImpscaval(i)
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

  Public Overridable Function SpezzaRigheDaZoomLotti(ByVal dttLotti As datatable, ByVal dtrEc As datarow) As Boolean
    Dim strSQL As String = ""
    Try
      Dim bGestioneLotti As Boolean = (dtrEc!xxo_geslotti.ToString = "S")
      Dim bGestioneUbicazioni As Boolean = (GetMemDttArti(NTSCStr(dtrEc!ec_codart)).Rows(0)!ar_gesubic.ToString = "S")

      'La prima riga la assegna alla riga corrente
      If bGestioneLotti Then dtrEc!ec_lotto = dttLotti.Rows(0)!alo_lotto
      If bGestioneUbicazioni Then dtrEc!ec_ubicaz = dttLotti.Rows(0)!xx_ubicaz
      dtrEc!ec_quant = dttLotti.Rows(0)!xx_assegnata

      'Le righe successive le assegna moltiplicando la prima riga
      For i As Integer = 1 To dttLotti.Rows.Count - 1
        dttEC.Rows.Add(dtrEc.ItemArray)
        With dttEC.Rows(dttEC.Rows.Count - 1)
          !ec_riga = NTSCInt(dttEC.Compute("MAX(ec_riga)", "")) + nIncremContatoreRiga
          If bGestioneLotti Then !ec_lotto = dttLotti.Rows(i)!alo_lotto
          If bGestioneUbicazioni Then dtrEc!ec_ubicaz = dttLotti.Rows(i)!xx_ubicaz
          !ec_quant = dttLotti.Rows(i)!xx_assegnata
        End With
      Next

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function VerificaCodicePagamentoContanti(ByVal lCodpaga As Integer) As Boolean
    Try
      If lCodpaga = 0 Then Return False

      Dim dttPaga As New DataTable
      oCldDocu.ValCodiceDb(lCodpaga.ToString, strDittaCorrente, "TABPAGA", "N", , dttPaga)
      If dttPaga.Rows.Count = 0 Then Return False 'La forma di pagamento non esiste

      Return (NTSCInt(dttPaga.Rows(0)!tb_tippaga) = 4)
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

  Public Overridable Sub AggiungiColonneUnbound(ByRef ds As DataSet)
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiunge le colonne xxo_percricarico, xxo_percmargine e xxo_marginevalore, se non esistono
      '--------------------------------------------------------------------------------------------------------------
      With ds.Tables("CORPO").Columns
        If .Contains("xxo_prznet") = False Then .Add("xxo_prznet", GetType(Decimal))
        If .Contains("xxo_percricarico") = False Then .Add("xxo_percricarico", GetType(Decimal))
        If .Contains("xxo_percmargine") = False Then .Add("xxo_percmargine", GetType(Decimal))
        If .Contains("xxo_marginevalore") = False Then .Add("xxo_marginevalore", GetType(Decimal))
        If .Contains("xxo_valorelav") = False Then .Add("xxo_valorelav", GetType(Decimal))
      End With
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrRow As DataRow In ds.Tables("CORPO").Rows
        With dtrRow
          '----------------------------------------------------------------------------------------------------------
          '--- Inizializza le colonne
          '----------------------------------------------------------------------------------------------------------
          !xxo_percricarico = 0
          !xxo_percmargine = 0
          !xxo_marginevalore = 0
          '----------------------------------------------------------------------------------------------------------
          '--- Calcola % Ricarico, % Margine e Margine Valore
          '----------------------------------------------------------------------------------------------------------
          If Not BGESTORDLIST Then CalcolaMargineRicarico(dtrRow, NTSCDate(ds.Tables("TESTA").Rows(0)!et_datdoc))
          '----------------------------------------------------------------------------------------------------------
          CalcolaPrezzoNetto(dtrRow)
        End With
      Next

      If ds.Tables("TESTA").Rows.Count > 0 Then CalcolaValoreLavorazioni(NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork))
      '--------------------------------------------------------------------------------------------------------------
      ds.Tables("CORPO").AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
  Public Overridable Sub CalcolaMargineRicarico(ByVal dtrRow As DataRow, ByVal dtDataDoc As Date)
    Try
      '----------------------------------------------------------------------------------------------------------
      '--- Calcola % Ricarico, % Margine e Margine Valore
      '----------------------------------------------------------------------------------------------------------
      With dtrRow
        If NTSCDec(!ec_quant) = 0 Then
          !xxo_percricarico = 0
          !xxo_percmargine = 0
          !xxo_marginevalore = 0
        Else
          Dim dPrezzo As Decimal = oCldDocu.RitornaPrezzoPerCalcoloRic_Marg(strDittaCorrente, NTSCStr(!ec_codart), 0, 0, nListinoCalcoloRicaricoMargine, _
                                                                            0, 0, dtDataDoc.ToShortDateString, NTSCDec(!ec_quant), NTSCInt(!ec_fase), _
                                                                            NTSCStr(!ec_ump))

          Dim dPrzNetto As Decimal = NTSCDec(!ec_valore) / NTSCDec(!ec_quant)
          If dPrezzo <> 0 Then !xxo_percricarico = (dPrzNetto - dPrezzo) / dPrezzo * 100
          If dPrzNetto <> 0 Then !xxo_percmargine = (dPrzNetto - dPrezzo) / dPrzNetto * 100
          If bMargineValoreUnitario Then
            !xxo_marginevalore = dPrzNetto - dPrezzo
          Else
            !xxo_marginevalore = NTSCDec(!ec_valore) - dPrezzo * NTSCDec(!ec_quant)
          End If
        End If
      End With
      '----------------------------------------------------------------------------------------------------------
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


#Region "FORM FRMVEADRG"
  Public bAdrgHasChanges As Boolean = False
  Public dsAdrgShared As DataSet
  Public strAdrgPrevCelValue As String = ""

  Public Overridable Function AdrgApri(ByRef dsAdrg As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'creo il datatable
      dsAdrg.Tables.Clear()
      dsAdrg.Tables.Add("ADRG")
      dsAdrg.Tables("ADRG").Columns.Add("ak_riga", GetType(Integer))
      dsAdrg.Tables("ADRG").Columns.Add("ak_codart", GetType(String))
      dsAdrg.Tables("ADRG").Columns.Add("xx_codart", GetType(String))
      dsAdrg.Tables("ADRG").Columns.Add("ak_unmis", GetType(String))
      dsAdrg.Tables("ADRG").Columns.Add("ak_colli", GetType(Decimal))
      dsAdrg.Tables("ADRG").Columns.Add("ak_ump", GetType(String))
      dsAdrg.Tables("ADRG").Columns.Add("ak_quant", GetType(Decimal))
      dsAdrg.Tables("ADRG").Columns("ak_riga").AutoIncrement = True

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      AdrgSetDefaultValue(dsAdrg)
      dsAdrgShared = dsAdrg

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsAdrgShared.Tables("ADRG").ColumnChanging, AddressOf AdrgBeforeColUpdate
      AddHandler dsAdrgShared.Tables("ADRG").ColumnChanged, AddressOf AdrgAfterColUpdate

      '--------------------------------------
      'confermo tutto
      dsAdrgShared.Tables("ADRG").AcceptChanges()
      bAdrgHasChanges = False

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

  Public Overridable Sub AdrgSetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("ADRG").Columns("ak_codart").DefaultValue = ""
      ds.Tables("ADRG").Columns("xx_codart").DefaultValue = ""
      ds.Tables("ADRG").Columns("ak_unmis").DefaultValue = ""
      ds.Tables("ADRG").Columns("ak_colli").DefaultValue = 0
      ds.Tables("ADRG").Columns("ak_ump").DefaultValue = ""
      ds.Tables("ADRG").Columns("ak_quant").DefaultValue = 0

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

  Public Overridable Sub AdrgNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsAdrgShared.Tables("ADRG").Rows.Add(dsAdrgShared.Tables("ADRG").NewRow)
      bAdrgHasChanges = True
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

  Public Overridable Function AdrgRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsAdrgShared.Tables("ADRG").Select(strFilter)(nRow).RejectChanges()
      bAdrgHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function AdrgSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not AdrgTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      dsAdrgShared.Tables("ADRG").AcceptChanges()

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

  Public ReadOnly Property AdrgRecordIsChanged() As Boolean
    Get
      Return bAdrgHasChanges
    End Get
  End Property

  Public Overridable Sub AdrgBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strAdrgPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AdrgBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub AdrgBeforeColUpdate_ak_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Dim dttTmp As New DataTable
    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.Row!xx_codart = ""
        e.Row!ak_colli = 0
        e.Row!ak_quant = 0
        Return
      End If

      If NTSCStr(e.ProposedValue) <> NTSCStr(e.ProposedValue).ToUpper Then
        e.ProposedValue = NTSCStr(e.ProposedValue).ToUpper
      End If

      If Not oCldDocu.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
        e.ProposedValue = ""
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128705228205625000, "Codice articolo non corretto")))
        Return
      End If

      If dsAdrgShared.Tables("ADRG").Select("ak_codart = " & CStrSQL(e.ProposedValue.ToString), Nothing, DataViewRowState.CurrentRows).Length > 0 Then
        e.ProposedValue = ""
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127930179092201112, "Il codice articolo inserito è già stato utilizzato. Inserire un codice non utilizzato")))
        Return
      End If

      If NTSCInt(dttTmp.Rows(0)!ar_codtagl) <> 0 Then
        e.ProposedValue = ""
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128705232272031250, "Non è possibile indicare un articolo gestito a taglie/colori")))
        Return
      End If

      If NTSCStr(dttTmp.Rows(0)!ar_tipokit) <> " " Then
        e.ProposedValue = ""
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128705232784687500, "Non è possibile indicare un articolo di tipo KIT")))
        Return
      End If

      e.Row!xx_codart = dttTmp.Rows(0)!ar_descr
      e.Row!ak_ump = dttTmp.Rows(0)!ar_unmis
      e.Row!ak_colli = 0
      e.Row!ak_quant = 0

      Select Case dttTmp.Rows(0)!ar_umdapr.ToString
        Case "P"
          e.Row!ak_unmis = dttTmp.Rows(0)!ar_unmis
        Case "S"
          If (NTSCStr(dttTmp.Rows(0)!ar_unmis2)).Trim <> "" Then
            e.Row!ak_unmis = dttTmp.Rows(0)!ar_unmis2
          Else
            e.Row!ak_unmis = dttTmp.Rows(0)!ar_unmis
          End If
        Case "C"
          If (NTSCStr(dttTmp.Rows(0)!ar_confez2)).Trim <> "" Then
            e.Row!ak_unmis = dttTmp.Rows(0)!ar_confez2
          Else
            e.Row!ak_unmis = dttTmp.Rows(0)!ar_unmis
          End If
        Case "Q"
          If (NTSCStr(dttTmp.Rows(0)!ar_um4)).Trim <> "" Then
            e.Row!ak_unmis = dttTmp.Rows(0)!ar_um4
          Else
            e.Row!ak_unmis = dttTmp.Rows(0)!ar_unmis
          End If
      End Select

      dttTmp.Clear()


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
  End Sub


  Public Overridable Sub AdrgAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strAdrgPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strAdrgPrevCelValue = strAdrgPrevCelValue.Remove(strAdrgPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bAdrgHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AdrgAfterColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub AdrgAfterColUpdate_ak_colli(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dQuant As Decimal = 0
    Dim strErrore As String = ""

    Try
      If NTSCStr(e.Row!ak_codart) = "" Then Return

      If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, e.Row!ak_codart.ToString, e.Row!ak_unmis.ToString, _
                                NTSCDec(e.Row!ak_colli), 0, 0, 0, dQuant, False, strErrore, oApp.NDecQta) Then
        e.Row!ak_quant = dQuant
      Else
        e.Row!ak_quant = 0
      End If
      If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))

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


  Public Overridable Function AdrgTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsAdrgShared.Tables("ADRG").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCStr(dtrTmp(i)!ak_codart).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128705224919375000, "Il codice articolo è obbligatorio. Inserirlo prima di salvare la riga")))
          Return False
        End If
        If NTSCDec(dtrTmp(i)!ak_quant) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128705225449062500, "Inserire una quatità diversa da 0 prima di salvare la riga")))
          Return False
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
      Return False
    End Try
  End Function

#End Region

#Region "FORM FRMVEHLBC"
  Public bHlbcHasChanges As Boolean = False
  Public dsHlbcShared As DataSet
  Public strHlbcPrevCelValue As String = ""
  Public nHlbcTipoOperaz As Integer = 0
  Public bHlbcBarcodeEvadiPerDataCons As Boolean = False
  Public bHlbcSelezionaOrdiniPreferenziali As Boolean = False
  Public nHlbcCodpaga As Integer = 0                  'codice pagamento passato dal chiamante
  Public nHlbcCoddest As Integer = 0                  'destinazione diversa passata dal chiamante
  Public lHlbcConto As Integer = 0
  Public strHlbcTipork As String = ""
  Public nHlbcValuta As Integer = 0
  Public strHlbcScorpo As String = ""

  Public Overridable Function HlbcGetData(ByVal strQuery As String, ByRef dsHlbc As DataSet) As Boolean
    Try
      bHlbcBarcodeEvadiPerDataCons = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, "Bsvebolx", "Opzioni", ".", "BarcodeEvadiPerDataCons", "0", " ", "0"))) 'se abilitata a -1 l'evasione delle righe d'ordine con barcode avviane per data consegna al posto di data ordine
      bHlbcSelezionaOrdiniPreferenziali = CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSVEHLBC", "OPZIONI", ".", "SelezionaOrdiniPreferenziali", "0", " ", "0"))  ' NON DOCUMENTARE

      oCldDocu.HlbcGetData(strDittaCorrente, strQuery, dsHlbc)

      dsHlbcShared = dsHlbc

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsHlbcShared.Tables("SELDOC").ColumnChanging, AddressOf HlbcBeforeColUpdate
      AddHandler dsHlbcShared.Tables("SELDOC").ColumnChanged, AddressOf HlbcAfterColUpdate

      '--------------------------------------
      'confermo tutto
      dsHlbcShared.Tables("SELDOC").AcceptChanges()
      bHlbcHasChanges = False

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

  Public Overridable Function HlbcRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsHlbcShared.Tables("SELDOC").Select(strFilter)(nRow).RejectChanges()
      bHlbcHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public ReadOnly Property HlbcRecordIsChanged() As Boolean
    Get
      Return bHlbcHasChanges
    End Get
  End Property

  Public Overridable Sub HlbcBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strHlbcPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "HlbcBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub HlbcAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strHlbcPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strHlbcPrevCelValue = strHlbcPrevCelValue.Remove(strHlbcPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHlbcHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "HlbcAfterColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub HlbcAfterColUpdate_xx_quadaeva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCDec(e.ProposedValue) < NTSCDec(e.Row!xx_qtares) Then
        e.Row!xx_flevasdaeva = "C"
      Else
        e.Row!xx_flevasdaeva = "S"
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
  End Sub

  Public Overridable Function HlbcSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not HlbcTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      dsHlbcShared.Tables("SELDOC").AcceptChanges()
      bHlbcHasChanges = False

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
  Public Overridable Function HlbcTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim dDaEvadere As Decimal = 0
    Try
      dtrTmp = dsHlbcShared.Tables("SELDOC").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        dDaEvadere = NTSCDec(dtrTmp(i)!mo_quant) - NTSCDec(dtrTmp(i)!mo_quaeva)
        If dDaEvadere < 0 Then dDaEvadere = 0
        If NTSCDec(dtrTmp(i)!xx_quadaeva) > dDaEvadere And dtrTmp(i)!xx_flevasdaeva.ToString <> "C" Then
          'ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128710611979687500, "E' obbligatorio evadere a saldo la riga quando la quantità da evadere supera o è uguale a quella evadibile.")))
          'Return False
          dtrTmp(i)!xx_flevasdaeva = "S"
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
      Return False
    End Try
  End Function


  Public Overridable Function HlbcProcessBarcode(ByVal strBarcode As String, ByRef dtrTrow As DataRow) As Boolean
    Dim dttTmp As New DataTable
    Dim dttArt As New DataTable
    Dim strUnmisBarre As String = ""
    Dim dQtaBarre As Decimal = 0
    Dim strErr As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim bRigaNuova As Boolean = False
    Dim i As Integer = 0
    Dim dQuadaeva As Decimal = 0

    Try
      If strBarcode.Trim = "" Then Return True

      oCldDocu.ValCodiceDb(strBarcode, strDittaCorrente, "BARCODE", "S", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128712513094062500, "Nessun articolo associato al codice a barre: '|" & strBarcode & "|'")))
        Return False
      End If
      oCldDocu.ValCodiceDb(dttTmp.Rows(0)!bc_codart.ToString, strDittaCorrente, "ARTICO", "S", "", dttArt)
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128712514952343750, "Articolo '|" & dttTmp.Rows(0)!bc_codart.ToString & "|' associato al codice a barre: '|" & strBarcode & "|' inesistente")))
        Return False
      End If

      If NTSCInt(dttArt.Rows(0)!ar_codtagl) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128712579542031250, "Funzionalità non abilitata per articoli gestiti a Taglie e Colori")))
        Return False
      End If

      '------------------------------
      'Rileva la quantità (IN UMP) da trattare
      If bBarcodeConQta Then
        'Deve fare come BSVEBOLL, con opzione BarcodeConQta abilitata
        dQtaBarre = NTSCDec(dttTmp.Rows(0)!bc_quant)
      Else
        If bBarcodeDerogaQta Then
          dQtaBarre = NTSCDec(dttTmp.Rows(0)!bc_quant)
        Else
          'Deve fare come BSVEBOLL standard
          strUnmisBarre = NTSCStr(dttTmp.Rows(0)!bc_unmis)
          If strUnmisBarre <> NTSCStr(dttArt.Rows(0)!ar_unmis) And strUnmisBarre <> NTSCStr(dttArt.Rows(0)!ar_unmis2) And _
              strUnmisBarre <> NTSCStr(dttArt.Rows(0)!ar_confez2) And strUnmisBarre <> NTSCStr(dttArt.Rows(0)!ar_um4) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128712517279375000, "L'unità di misura associata al codice a barre '|" & strBarcode & "|' non è una di quelle consentite per l'articolo. Verrà utilizzata l'Unità di misura principale.")))
            strUnmisBarre = NTSCStr(dttArt.Rows(0)!ar_unmis)
          End If
          If strUnmisBarre = NTSCStr(dttArt.Rows(0)!ar_unmis) Then
            dQtaBarre = 1
          Else
            CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, NTSCStr(dttTmp.Rows(0)!bc_codart), strUnmisBarre, 1, 1, 1, 1, dQtaBarre, False, strErr, 3)
            If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
          End If
        End If
      End If

      '------------------------------
      If nHlbcTipoOperaz = 1 Then     'acquisizione 
        'Acquisizione quantità
Ripeti1:
        Dim strOrderBy As String
        If bHlbcBarcodeEvadiPerDataCons Then
          strOrderBy = "mo_datcons asc, xx_flevasdaeva desc, mo_riga ASC"
        Else
          strOrderBy = "xx_datord asc, xx_flevasdaeva desc, mo_riga ASC"
        End If

        dtrT = dsHlbcShared.Tables("SELDOC").Select("mo_codart = " & CStrSQL(NTSCStr(dttTmp.Rows(0)!bc_codart)) & " AND xx_codpaga = " & nHlbcCodpaga & " AND xx_coddest = " & nHlbcCoddest, strOrderBy)
        If dtrT.Length = 0 Then dtrT = dsHlbcShared.Tables("SELDOC").Select("mo_codart = " & CStrSQL(NTSCStr(dttTmp.Rows(0)!bc_codart)), strOrderBy)
        bRigaNuova = True
        i = 0
        dtrTrow = Nothing
        If dtrT.Length > 0 Then
          'Identificazione riga da aggiornare
          Do While Not i = dtrT.Length
            If dtrT(i)!xx_flevasdaeva.ToString.ToUpper = "C" Or NTSCDate(dtrT(i)!xx_datord) = New Date(2099, 12, 31) Then
              dtrTrow = dtrT(i)
              dQuadaeva = NTSCDec(dtrTrow!xx_quadaeva)
              bRigaNuova = False
              GoTo Continua
            End If
            i += 1
          Loop    'Do While Not i = dtrT.Length
Continua:
          'Controllo per articolo movimentabile
          If Not bRigaNuova Then
            If NTSCDate(dtrTrow!xx_datord) <> New Date(2099, 12, 31) Then
              If NTSCInt(dtrTrow!xx_codpaga) <> nHlbcCodpaga Or NTSCInt(dtrTrow!xx_coddest) <> nHlbcCoddest Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128712528669531250, "Articolo: '|" & NTSCStr(dttTmp.Rows(0)!bc_codart) & "|' non evadibile! Controlla condizione di pagamento e destinazione.")))
                Return False
              End If
            End If
            If (dQuadaeva + dQtaBarre < NTSCDec(dtrTrow!xx_qtares)) Or NTSCDate(dtrTrow!xx_datord) = New Date(2099, 12, 31) Then
              'Aggiornamento riga
              dtrTrow!xx_quadaeva = dQuadaeva + dQtaBarre
              dtrTrow.AcceptChanges()
            Else
              'Aggiornamento riga
              dtrTrow!xx_quadaeva = NTSCDec(dtrTrow!xx_qtares)
              dtrTrow!xx_flevasdaeva = "S"
              dtrTrow.AcceptChanges()

              If ArrDbl(dQtaBarre - (NTSCDec(dtrTrow!xx_qtares) - dQuadaeva), 3) > 0 Then
                'davide 05/05/2003: non fa una nuova riga ma va a vedere se ci sono
                'altre righe d'ordine da scalare
                'Riga nuova
                'RigaNuova snaTmp!bc_codart, ArrDbl(dQtaBarre - (dQtares - dQuadaeva), 3)
                dQtaBarre = ArrDbl(dQtaBarre - (NTSCDec(dtrTrow!xx_qtares) - dQuadaeva), 3)
                GoTo Ripeti1
              End If
            End If
          Else
            'Riga nuova
            HlbcRigaNuova(NTSCStr(dttTmp.Rows(0)!bc_codart), NTSCInt(dttTmp.Rows(0)!bc_fase), NTSCStr(dttArt.Rows(0)!ar_descr), dQtaBarre)
            dtrTrow = dsHlbcShared.Tables("SELDOC").Rows(dsHlbcShared.Tables("SELDOC").Rows.Count - 1)
          End If ' brigaNUova
        Else
          'Riga nuova
          HlbcRigaNuova(NTSCStr(dttTmp.Rows(0)!bc_codart), NTSCInt(dttTmp.Rows(0)!bc_fase), NTSCStr(dttArt.Rows(0)!ar_descr), dQtaBarre)
          dtrTrow = dsHlbcShared.Tables("SELDOC").Rows(dsHlbcShared.Tables("SELDOC").Rows.Count - 1)
        End If    'If dtrT.Length > 0 Then
      End If    'If nHlbctipoOperaz = 1 Then


      '------------------------------
      If nHlbcTipoOperaz = 2 Then     'storno
        'Cancellazione quantità
Ripeti:
        dtrT = dsHlbcShared.Tables("SELDOC").Select("mo_codart = " & CStrSQL(NTSCStr(dttTmp.Rows(0)!bc_codart)), IIf(bHlbcBarcodeEvadiPerDataCons, "mo_datcons", "xx_datord").ToString & " desc, xx_flevasdaeva asc, mo_riga ASC")
        bRigaNuova = True
        i = 0
        dtrTrow = Nothing
        If dtrT.Length > 0 Then
          'Identificazione riga da aggiornare
          dQuadaeva = 0
          Do While Not i = dtrT.Length
            If NTSCDec(dtrT(i)!xx_quadaeva) > 0 Then
              dtrTrow = dtrT(i)
              dQuadaeva = NTSCDec(dtrT(i)!xx_quadaeva)
              GoTo Continua1
            End If
            i += 1
          Loop    'Do While Not i = dtrT.Length
Continua1:
          If dQuadaeva = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740035959659000, _
                            "Non c'è niente da eliminare per l'articolo: |" & NTSCStr(dttTmp.Rows(0)!bc_codart) & "|" & vbCrLf & _
                            "Quantità ancora da eliminare: |" & dQtaBarre.ToString(oApp.FormatQta) & "|")))
            Return True
          End If
          'Controllo per articolo movimentabile
          If NTSCDate(dtrTrow!xx_datord) <> NTSCDate(IntSetDate("31/12/2099")) Then
            If NTSCInt(dtrTrow!xx_codpaga) <> nHlbcCodpaga Or NTSCInt(dtrTrow!xx_coddest) <> nHlbcCoddest Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128712554459062500, "Articolo: '|" & NTSCStr(dttTmp.Rows(0)!bc_codart) & "|' non eliminabile! Controlla condizione di pagamento e destinazione.")))
              Return False
            End If
          End If
          If dQtaBarre >= dQuadaeva Then
            If NTSCDate(dtrTrow!xx_datord) <> NTSCDate(IntSetDate("31/12/2099")) Then
              'Aggiornamento riga
              dtrTrow!xx_quadaeva = 0
              dtrTrow!xx_flevasdaeva = "C"
              dtrTrow.AcceptChanges()
            Else
              'Eliminazione riga
              dtrTrow.Delete()
              dsHlbcShared.Tables("SELDOC").AcceptChanges()
              dtrTrow = Nothing
            End If
            'Ulteriore quantità da eliminare
            dQtaBarre = ArrDbl(dQtaBarre - dQuadaeva, 3)
            If dQtaBarre > 0 Then
              GoTo Ripeti
            End If
          Else
            'Aggiornamento riga
            dtrTrow!xx_quadaeva = NTSCDec(dtrTrow!xx_quadaeva) - dQtaBarre
            dtrTrow!xx_flevasdaeva = "C"
            dtrTrow.AcceptChanges()
          End If
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128740035976195000, _
                          "Non c'è niente da eliminare per l'articolo: |" & NTSCStr(dttTmp.Rows(0)!bc_codart) & "|" & vbCrLf & _
                          "Quantità ancora da eliminare: |" & dQtaBarre.ToString(oApp.FormatQta) & "|")))
          Return True
        End If    'If dtrT.Length > 0 Then
      End If    'If nHlbctipoOperaz = 2 Then

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
      dttArt.Clear()
    End Try
  End Function
  Public Overridable Function HlbcRigaNuova(ByVal strCodart As String, ByVal nFase As Integer, ByVal strDescr As String, ByVal dQtaBarre As Decimal) As Boolean
    Dim nRiga As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      dtrT = dsHlbcShared.Tables("SELDOC").Select("", "mo_riga DESC")
      If dtrT.Length > 0 Then nRiga = NTSCInt(dtrT(0)!mo_riga)
      nRiga += 1
      dsHlbcShared.Tables("SELDOC").Rows.Add(dsHlbcShared.Tables("SELDOC").NewRow)
      With dsHlbcShared.Tables("SELDOC").Rows(dsHlbcShared.Tables("SELDOC").Rows.Count - 1)
        !mo_codart = strCodart
        !mo_fase = nFase
        !mo_quant = dQtaBarre
        !mo_descr = strDescr
        !xx_quadaeva = dQtaBarre
        !xx_flevasdaeva = "C"
        !xx_datord = NTSCDate(IntSetDate("31/12/2099"))
        !mo_datcons = NTSCDate(IntSetDate("31/12/2099"))
        !xx_codpaga = nHlbcCodpaga
        !xx_coddest = nHlbcCoddest
        !mo_tipork = " "
        !mo_anno = 0
        !mo_serie = " "
        !mo_numord = 0
        !mo_riga = nRiga
        !mo_quaeva = 0
        !xx_qtares = 0
      End With    'With dsHlbcShared.Tables("SELDOC").Rows(dsHlbcShared.Tables("SELDOC").Rows.Count - 1)
      dsHlbcShared.Tables("SELDOC").Rows(dsHlbcShared.Tables("SELDOC").Rows.Count - 1).AcceptChanges()

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

  Public Overridable Function HlbcGetWhereHltd() As String
    Dim bRetailExt As Boolean = False
    Try
      'per zoom dohlTd: zoom importa testate ordini
      bNonProporreEvasioneIPSuNotePrel = CBool(Val(oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSVEBOLL", "OPZIONI", ".", "NonProporreEvasioneIPSuNotePrel", "0", " ", "0"))) 'Se abilitata, nella selezione ordini (su note prel.) non propone gli Impegni di prod.

      If CBool(lModuliSupDittaDitt And bsModSupGPE) Then bRetailExt = True

      Return oCldDocu.GetWhereHltd(strDittaCorrente, strHlbcTipork, bNonProporreEvasioneIPSuNotePrel, False, _
                                   bDocEmesso, False, nHlbcValuta, lHlbcConto, strHlbcScorpo, bRetailExt)

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

#End Region

#Region "Logistica di magazzino su palmare"
  Public Overridable Function TrascodificaUbicazione(ByVal lMaga As Integer, ByRef strUbicaz As String, ByVal bUsaUbicConFigli As Boolean) As Boolean
    Try
      Return TrascodificaUbicazione(lMaga, strUbicaz, bUsaUbicConFigli, False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function TrascodificaUbicazione(ByVal lMaga As Integer, ByRef strUbicaz As String, ByVal bUsaUbicConFigli As Boolean, ByVal bCheckBloccato As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {lMaga, strUbicaz, bUsaUbicConFigli, bCheckBloccato})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strUbicaz = NTSCStr(oIn(1))
        Return CBool(oOut)
      End If
      '----------------

      Return TrascodificaUbicazione(lMaga, strUbicaz, bUsaUbicConFigli, bCheckBloccato, False)
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

  Public Overridable Function TrascodificaUbicazione(ByVal lMaga As Integer, ByRef strUbicaz As String, ByVal bUsaUbicConFigli As Boolean, _
                                                     ByVal bCheckBloccato As Boolean, ByVal bUbicBloccSoloAvviso As Boolean) As Boolean
    ' Verifica che l'ubicazione passata sia valida
    Dim dttUbicaz, dttMaga As New DataTable
    Dim strL1, strL2, strL3, strL4, strL5 As String
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {lMaga, strUbicaz, bUsaUbicConFigli, bCheckBloccato, bUbicBloccSoloAvviso})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strUbicaz = NTSCStr(oIn(1))
        Return CBool(oOut)
      End If
      '----------------

      '12 - vengono inseriti quando l'articolo non è gestito ad ubicazioni e non ci sono altre ubicazioni assegnabili
      If strUbicaz = "".PadLeft(12, "-"c) Then Return True
      ' Verifico se il magazzino è gestito a ubicazioni
      If Not oCldDocu.ValCodiceDb(NTSCStr(lMaga), strDittaCorrente, "TABMAGA", "N", , dttMaga) Then Return False
      If dttMaga.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130685496654788233, "Il magazzino |" & lMaga & "| non esiste.")))
        Return False
      End If
      If NTSCStr(dttMaga.Rows(0)!tb_gestubi) = "N" Then Return True ' se non è gestito a ubicazioni, ogni ubicazione è valida

      ' Verifico se potrebbe essere un BC di una ubicazione, se non è un BC allora provo a vedere se è direttamente una ubicazione
      If IsNumeric(strUbicaz) Then
        If oCldDocu.TrascoUbicaz(strDittaCorrente, strUbicaz, lMaga, dttUbicaz) Then
          If dttUbicaz.Rows.Count <> 0 Then
            strUbicaz = NTSCStr(dttUbicaz.Rows(0)!au_ubicaz)
          End If
        End If
      End If

      ' verifico se è una ubicazione valida
      If Not oCldDocu.ValUbicaz(strDittaCorrente, strUbicaz.PadRight(12, "-"c), lMaga, dttUbicaz) Then Return False

      If dttUbicaz.Rows.Count = 0 Then Return False

      If bCheckBloccato Then
        If NTSCStr(dttUbicaz.Rows(0)!au_bloccata) = "S" Then
          If bUbicBloccSoloAvviso Then
            ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 129882808839089669, "Attenzione! Ubicazione Bloccata." & vbCrLf & _
                                                                                              "Si procede ugualmente")))
          Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129642021208708996, "Non è possibile usare Ubicazioni Bloccate!")))
            Return False
          End If
        End If
      End If

      strUbicaz = strUbicaz.ToUpper.PadRight(12, "-"c)

      ' spezzo l'ubicazione
      strL1 = strUbicaz.Substring(0, 2)
      strL2 = strUbicaz.Substring(2, 2)
      strL3 = strUbicaz.Substring(4, 3)
      strL4 = strUbicaz.Substring(7, 3)
      strL5 = strUbicaz.Substring(10, 2)

      ' Se ho scelto di non usare le ubicazione che hanno dei figli
      If Not bUsaUbicConFigli Then
        ' controllo se sopra quel livello ci sono ubicazioni disponibili
        If Not oCldDocu.FigliUbicazione(strDittaCorrente, strL1, strL2, strL3, strL4, strL5, lMaga, dttUbicaz) Then Return False

        If dttUbicaz.Rows.Count <> 1 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128885057150426075, "Ubicazione non utilizzabile." & vbCrLf & "L'ubicazione non è di ultimo livello.")))
          Return False
        End If

        If bCheckBloccato Then
          ' verifico se è una ubicazione valida
          If Not oCldDocu.ValUbicaz(strDittaCorrente, strL1.PadRight(12, "-"c), lMaga, dttUbicaz) Then Return False
          If dttUbicaz.Rows.Count = 0 Then Return True ' è giusto true, se dopo la prima ubicazione non ne trovo altre va bene che dia l'ubicazione per buona!

          If NTSCStr(dttUbicaz.Rows(0)!au_bloccata) = "S" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129642023372104958, "Ubicazione Bloccata!")))
            Return False
          End If

          ' verifico se è una ubicazione valida
          If Not oCldDocu.ValUbicaz(strDittaCorrente, (strL1 & strL2).PadRight(12, "-"c), lMaga, dttUbicaz) Then Return False
          If dttUbicaz.Rows.Count = 0 Then Return True ' è giusto true, se dopo la prima ubicazione non ne trovo altre va bene che dia l'ubicazione per buona!

          If NTSCStr(dttUbicaz.Rows(0)!au_bloccata) = "S" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129642023403979346, "Ubicazione Bloccata!")))
            Return False
          End If

          ' verifico se è una ubicazione valida
          If Not oCldDocu.ValUbicaz(strDittaCorrente, (strL1 & strL2 & strL3).PadRight(12, "-"c), lMaga, dttUbicaz) Then Return False
          If dttUbicaz.Rows.Count = 0 Then Return True ' è giusto true, se dopo la prima ubicazione non ne trovo altre va bene che dia l'ubicazione per buona!

          If NTSCStr(dttUbicaz.Rows(0)!au_bloccata) = "S" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129642023412104190, "Ubicazione Bloccata!")))
            Return False
          End If

          ' verifico se è una ubicazione valida
          If Not oCldDocu.ValUbicaz(strDittaCorrente, (strL1 & strL2 & strL3 & strL4).PadRight(12, "-"c), lMaga, dttUbicaz) Then Return False
          If dttUbicaz.Rows.Count = 0 Then Return True ' è giusto true, se dopo la prima ubicazione non ne trovo altre va bene che dia l'ubicazione per buona!

          If NTSCStr(dttUbicaz.Rows(0)!au_bloccata) = "S" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129642023431478818, "Ubicazione Bloccata!")))
            Return False
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

  '---- Funzioni per la proposta dei campi ----
  Public Overridable Function PropostaUbicazione(ByVal strProgramma As String, ByVal strTipoRk As String, ByVal strStatus As String, ByVal strArtico As String, _
                                                 ByVal lFase As Integer, ByVal lMaga As Integer, ByVal strUbicOrd As String, ByRef strUbic As String) As Boolean
    Dim dttArtMaga, dttMaga As New DataTable
    Try

      GetMemDttArti(strArtico)

      If NTSCStr(dttArti.Rows(0)!ar_gesubic) = "S" Then
        If strUbicOrd.Trim <> "" Then
          strUbic = strUbicOrd.PadRight(12, "-"c)
        Else
          If Not bPropUbicazNonUsareSoloArtico Then
            strUbic = NTSCStr(dttArti.Rows(0)!ar_ubicaz).Trim.PadRight(12, "-"c)
          Else
            'Uso artmaga
            If oCldDocu.ValCodiceDb(NTSCStr(lMaga), strDittaCorrente, "ARTMAGA", "N", , dttArtMaga, strArtico, NTSCStr(lFase)) Then
              strUbic = NTSCStr(dttArtMaga.Rows(0)!am_ubicaz).Trim.PadRight(12, "-"c)
            End If

            'Se ancora non c'è provo con artico
            If strUbic.Trim = "" Then strUbic = NTSCStr(dttArti.Rows(0)!ar_ubicaz).Trim.PadRight(12, "-"c)

            'Se ancora non c'è provo con tabmaga
            If Not oCldDocu.ValCodiceDb(NTSCStr(lMaga), strDittaCorrente, "TABMAGA", "S", , dttMaga) Then Return False
            If strUbic.Trim.Trim("-"c) = "" Then strUbic = NTSCStr(dttMaga.Rows(0)!tb_dubicaz).Trim.PadRight(12, "-"c)

          End If
        End If
      Else
        strUbic = ""
      End If

      If strUbic = "".PadRight(12, "-"c) Then strUbic = ""

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
  Public Overridable Function PropostaMatricola(ByVal strProgramma As String, ByVal strTipoRk As String, ByVal strStatus As String, ByVal strArtico As String, _
                                                ByVal lFase As Integer, ByVal lMaga As Integer, ByVal strMatriOrd As String, ByRef strMatri As String) As Boolean
    Try
      ' Predisposta ma non gestita.
      ' Al momento è necessario che l'utente inserisca a mano la matricola.

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
  Public Overridable Function PropostaLotto(ByVal strProgramma As String, ByVal strTipoRk As String, ByVal strStatus As String, ByVal strArtico As String, _
                                          ByVal lFase As Integer, ByVal lMaga As Integer, ByVal lLottoOrd As Integer, ByRef lLotto As Integer) As Boolean
    Try
      'obsoleta
      Return PropostaLotto(strProgramma, strTipoRk, strStatus, strArtico, lFase, lMaga, lLottoOrd, lLotto, "")
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
  Public Overridable Function PropostaLotto(ByVal strProgramma As String, ByVal strTipoRk As String, ByVal strStatus As String, ByVal strArtico As String, _
                                            ByVal lFase As Integer, ByVal lMaga As Integer, ByVal lLottoOrd As Integer, ByRef lLotto As Integer, ByRef strLottox As String) As Boolean
    Dim dttMaga As New DataTable
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strProgramma, strTipoRk, strStatus, strArtico, lFase, lMaga, lLottoOrd, lLotto, strLottox})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        lLotto = NTSCInt(oIn(7))
        strLottox = NTSCStr(oIn(8))
        Return CBool(oOut)
      End If
      '----------------

      ' If lLotto = 0 Then Return True

      GetMemDttArti(strArtico)

      If NTSCStr(dttArti.Rows(0)!ar_geslotti) = "S" Then
        If lLottoOrd <> 0 Then
          lLotto = lLottoOrd
          strLottox = ""
        Else
          If Not oCldDocu.ValCodiceDb(NTSCStr(lMaga), strDittaCorrente, "TABMAGA", "N", , dttMaga) Then Return False
          lLotto = 0
          strLottox = NTSCStr(dttMaga.Rows(0)!tb_dlottox)
        End If
      Else
        lLotto = 0
        strLottox = ""
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
  '--------------------------------------------

#End Region

#Region "funzioni specifiche per BNDBSECE.VB"
  Public Overridable Function SeceApri(ByVal strDitta As String, ByRef dsSece As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldDocu.GetDataSece(strDitta, lSeceIITTTabcent, dsSece)
      If dReturn = False Then Return False

      oCldDocu.SetTableDefaultValueFromDB("TTTABCENT", dsSece)

      dsSeceShared = dsSece

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

#Region "funzioni specifiche per BNDBLOMA.VB"
  Public Overridable Function LomaApri(ByVal strDitta As String, ByRef dsLoma As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldDocu.GetDataLoma(strDitta, lIITTartico, dsLoma)
      If dReturn = False Then Return False

      oCldDocu.SetTableDefaultValueFromDB("TTARTICO", dsLoma)

      dsLomaShared = dsLoma

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsLomaShared.Tables("TTARTICO").ColumnChanging, AddressOf LomaBeforeColUpdate
      AddHandler dsLomaShared.Tables("TTARTICO").ColumnChanged, AddressOf LomaAfterColUpdate

      bLomaHasChanges = False

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
  Public Overridable Function LomaSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not LomaTestPreSalva() Then Return False
      End If

      'chiamo il dal per salvare
      bResult = oCldDocu.ScriviTabellaSemplice(strDittaCorrente, "TTARTICO", dsLomaShared.Tables("TTARTICO"), "", "", "")

      If bResult Then
        bLomaHasChanges = False
      End If

      Return bResult
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
  Public ReadOnly Property LomaRecordIsChanged() As Boolean
    Get
      Return bLomaHasChanges
    End Get
  End Property
  Public Overridable Function LomaTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim i As Integer
    Dim strTmp As String = ""
    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsLomaShared.Tables("TTARTICO").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        '-------------------------------
        'test lotto univoco
        If bLottoUnivoco And NTSCStr(dtrCurrRow(i)!xx_lottox) <> "" Then
          If Not oCldDocu.LottoxCheckLottoUnivoco(strDittaCorrente, NTSCStr(dtrCurrRow(i)!ar_codart), _
                        NTSCStr(dtrCurrRow(i)!xx_lottox), strTmp) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129526858643223250, "Test lotto univoco: Il lotto |" & NTSCStr(dtrCurrRow(i)!xx_lottox) & "| impostato sull'articolo |" & NTSCStr(dtrCurrRow(i)!ar_codart) & "| è già stato utilizzato per l'articolo |" & strTmp & "|")))
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
      Return False
    End Try
  End Function
  Public Overridable Function LomaRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsLomaShared.Tables("TTARTICO").Select(strFilter)(nRow).RejectChanges()
      bLomaHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub LomaBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      'quando dovrò riversare le modifiche in movmag processerò solo le righe con RIGACAMBIATA <> 0
      If e.Column.ColumnName <> "ar_scosic" Then e.Row!ar_scosic = -1

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "LomaBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub LomaBeforeColUpdate_ar_maxlotto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    'ar_maxlotto ovvero lotto numerico di form per versamento di bndbavmg e bndbavex
    Try
      If NTSCStr(e.Row!ar_codart).Trim = "" Then
        'prima di indicare il lotto devo indicare l'articolo 
        e.ProposedValue = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513888485432635, "Prima di indicare il lotto inserire il cod. articolo")))
        Return
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
  End Sub
  Public Overridable Sub LomaBeforeColUpdate_xx_lottox(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    'ar_maxlotto ovvero lotto numerico di form per versamento di bndbavmg e bndbavex
    Try
      If NTSCStr(e.Row!ar_codart).Trim = "" Then
        'prima di indicare il lotto devo indicare l'articolo 
        e.ProposedValue = ""
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513889258401769, "Prima di indicare il lotto inserire il cod. articolo")))
        Return
      End If

      If NTSCStr(e.ProposedValue).Trim = "" And NTSCStr(e.ProposedValue) <> NTSCStr(e.ProposedValue).Trim Then
        e.ProposedValue = ""
        Return
      End If

      If bLottoNew = False And NTSCStr(e.ProposedValue) <> "" Then
        'vecchia gestione lotti: il lotto è solo numerico di max 9 char: lo formatto
        If NTSCStr(e.ProposedValue) <> "" Then
          If Not IsNumeric(e.ProposedValue) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513977264885199, "Il codice lotto deve essere un numero compreso tra 0 e 999999999")))
            e.ProposedValue = ""
            Return
          End If
        End If

        e.ProposedValue = NTSCInt(e.ProposedValue).ToString("000000000")

        If NTSCInt(e.ProposedValue) = 0 Then e.ProposedValue = ""
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
  End Sub

  Public Overridable Sub LomaAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bLomaHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "LomaAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub LomaAfterColUpdate_ar_maxlotto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    'sono stato chiamato da bnmgalo e mi è stato passato il lotto numerico: compilo la colonna con il lotto alfanumerico
    'ar_maxlotto ovvero lotto numerico di form per versamento di bndbavmg e bndbavex
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(e.ProposedValue.ToString) = 0 Then
        e.Row!xx_lottox = ""
        Return
      End If

      oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANALOTTI", "S", "", dttTmp, NTSCStr(e.Row!ar_codart))
      If dttTmp.Rows.Count = 0 Then
        'non posso creare l'anagrafica lotti, visto che non saprei che alo_lottox memorizzare !!!!
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513219111285131, "Codice lotto inesistente")))
        e.Row!ar_maxlotto = 0
        Return
      End If

      If NTSCStr(e.Row!xx_lottox) <> NTSCStr(dttTmp.Rows(0)!alo_lottox) Then e.Row!xx_lottox = NTSCStr(dttTmp.Rows(0)!alo_lottox)

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
  End Sub
  Public Overridable Sub LomaAfterColUpdate_xx_lottox(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    'ar_maxlotto ovvero lotto numerico di form per versamento di bndbavmg e bndbavex
    Dim dttTmp As New DataTable
    Dim lIdLotto As Integer = 0
    Dim strErr As String = ""

    Try

      'dato il lotto alfanumerico devo tradure in ID lotto numerico
      If NTSCStr(e.ProposedValue) = "" Then
        e.Row!ar_maxlotto = 0
        Return
      End If

      'dato il lotto alfanumerico ottengo l'ID numerico
      oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANALOTTIX", "S", "", dttTmp, NTSCStr(e.Row!ar_codart))
      If dttTmp.Rows.Count = 0 Then
        'creo l'anagrafica lotti
        If Not CType(oCleComm, CLELBMENU).CreaAnalottiDaLottox(strDittaCorrente, NTSCStr(e.Row!ar_codart), _
                                          DateTime.Now.ToShortDateString, _
                                          NTSCStr(e.ProposedValue), "", lIdLotto, strErr) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513215192910290, "Codice lotto inesistente") & vbCrLf & strErr))
          e.Row!xx_lottox = ""
          Return
        Else
          If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
        End If
      Else
        lIdLotto = NTSCInt(dttTmp.Rows(0)!alo_lotto)
      End If

      If NTSCInt(e.Row!ar_maxlotto) <> lIdLotto Then e.Row!ar_maxlotto = lIdLotto

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
  End Sub

  Public Overridable Function LomaUpdateTTArtico(ByVal strRIGAU As String) As Boolean
    Try
      oCldDocu.LomaUpdateTTArtico(strDittaCorrente, lIITTartico, lHlloLotto, strRIGAU)

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
  Public Overridable Function Tabmaga_Validated(ByVal strCod As String, ByRef strTmp As String) As Boolean
    Try
      If Not strCod = "" Then
        oCldDocu.ValCodiceDb(strCod, strDittaCorrente, "TABMAGA", "N", strTmp)
      Else
        strTmp = ""
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

  Public Overridable Sub LomaBeforeColUpdate_ar_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Dim strLastUmis As String
    Dim strTmp As String = ""
    Try
      strLastUmis = ""
      If oCldDocu.ValCodiceDb(NTSCStr(e.Row!ar_codart), strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
        strLastUmis = NTSCStr(dttTmp.Rows(0)!ar_unmis)
      End If

      If Trim(e.ProposedValue.ToString) = "" Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128877122442605651, "Codice articolo obbligatorio.")))
        Return
      End If

      If Not oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128877124701398281, "Codice articolo inesistente.")))
        Return
      Else
        If UCase(NTSCStr(dttTmp.Rows(0)!ar_unmis)) <> UCase(strLastUmis) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128877125615404643, "L'articolo non ha la stessa unità di misura dell'articolo originario")))
          Return
        End If
      End If

      e.Row!ar_descr = NTSCStr(dttTmp.Rows(0)!ar_descr)
      e.Row!ar_inesaur = NTSCStr(dttTmp.Rows(0)!ar_geslotti)
      e.Row!ar_ripriord = NTSCStr(dttTmp.Rows(0)!ar_gestmatr)
      e.Row!ar_maxlotto = 0
      e.Row!ar_desint = ""
      e.Row!ar_codvar3 = NTSCStr(dttTmp.Rows(0)!ar_ultfase)
      'valido la fase
      If NTSCInt(e.Row!ar_codvar3) = 0 Then
        e.Row!ar_formula = ""
      Else
        If Not oCldDocu.ValCodiceDb(NTSCStr(e.Row!ar_codvar3), strDittaCorrente, "ARTFASI", "N", strTmp, , e.ProposedValue.ToString) Then
          e.Row!ar_formula = ""
        Else
          e.Row!ar_formula = strTmp
        End If
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
  End Sub
  Public Overridable Sub LomaBeforeColUpdate_ar_codvar3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Dim strGesfasi As String = ""
    Try
      If Trim(e.Row!ar_codart.ToString) = "" Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128877132054064971, "Codice articolo inesistente.")))
        Return
      End If

      '--- Prende ar_gesfasi da ARTICO
      If oCldDocu.ValCodiceDb(NTSCStr(e.Row!ar_codart), strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
        strGesfasi = NTSCStr(dttTmp.Rows(0)!ar_gesfasi)
      End If

      '--- Se l'articolo è gestito a fasi ne controlla l'esistenza in ARTFASI
      '--- altrimenti deve essere obbligatoriamente a 0
      Select Case strGesfasi
        Case "S"
          If NTSCInt(e.ProposedValue.ToString) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128877134789621871, "L'articolo è gestito per fasi e pertanto deve essere indicato un codice fase valido.")))
            Return
          Else
            If Not oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTFASI", "N", strTmp, , NTSCStr(e.Row!ar_codart)) Then
              e.ProposedValue = e.Row(e.Column.ColumnName)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128877135568064353, "Fase inesistente per l'articolo indicato.")))
              Return
            Else
              e.Row!ar_formula = strTmp
            End If
          End If
        Case "N"
          If NTSCInt(e.ProposedValue.ToString) <> 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128877136900934831, "L'articolo non è gestito per fasi e pertanto non può essere indicato un codice fase.")))
            Return
          End If
      End Select

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
#End Region

#Region "funzioni specifiche per BNDBHLLO.VB"
  Public Overridable Function HlloApri(ByVal strDitta As String, ByRef dsHllo As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim i As Integer
    Dim dttTmp As New DataTable
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldDocu.GetDataHllo(strDitta, strHlloCodart, nHlloMagaz, nHlloFase, dsHllo)
      If dReturn = False Then Return False

      oCldDocu.SetTableDefaultValueFromDB("LOTCPRO", dsHllo)

      For i = 0 To dsHllo.Tables("LOTCPRO").Rows.Count - 1
        With dsHllo.Tables("LOTCPRO").Rows(i)
          If nTipoGestArticolo = 2 Then Exit For
          If NTSCInt(!xx_lotto) = 0 Then Exit For

          If oCldDocu.ValCodiceDb(strHlloCodart, strDittaCorrente, "ANALOTTI", "N", , dttTmp, NTSCStr(!xx_lotto)) Then
            If dttTmp.Rows.Count = 0 Then
              !xx_note = ""
            Else
              !xx_note = NTSCStr(dttTmp.Rows(0)!alo_note)
            End If
          Else
            !xx_note = ""
          End If
        End With
      Next

      dsHlloShared = dsHllo

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
  Public Overridable Function Artfasi_Validated(ByVal nCod As Integer, ByVal strCodart As String, _
                                                ByRef strDescr As String) As Boolean
    Dim strTmp As String = ""
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldDocu.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "ARTFASI", "N", strTmp, Nothing, strCodart) Then
          Return False
        Else
          strDescr = strTmp
        End If
      Else
        strDescr = ""
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
  Public Overridable Function Artico_Validated(ByVal strCod As String, ByRef dttTmp As DataTable) As Boolean
    Try
      If Not strCod = "" Then
        If Not oCldDocu.ValCodiceDb(strCod, strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
          Return False
        End If
      Else
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
  Public Overridable Function Hllo_ProponiLottox(ByVal strCodart As String, ByVal nFase As Integer, ByVal nMagaz As Integer, _
                                                 ByVal dQtaTot As Decimal, ByVal strDatDoc As String, ByRef dttLotti As DataTable) As Boolean
    Dim dttTmp As New DataTable
    Try
      If Not oCldDocu.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttTmp) Then Return False
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130699376496443832, "Articolo '|" & strCodart & "|' non trovato nella sua anagrafica")))
        Return False
      End If

      If NTSCStr(dttTmp.Rows(0)!ar_tipscarlotx) = "M" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130699376802706617, "Articolo '|" & strCodart & "|' con scarico lotti manuale. Operazione interrotta.")))
        Return False
      End If

      If Not CType(oCleComm, CLELBMENU).ScaricaLottox_CercaLotti(strDittaCorrente, strCodart, _
                                       nFase, 0, NTSCStr(dttTmp.Rows(0)!ar_tipscarlotx), _
                                       NTSCDate(strDatDoc).ToShortDateString, _
                                       nMagaz, dQtaTot, "", 0, 0, Nothing, dttLotti) Then Return False

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
#End Region

#Region "funzioni specifiche per BNMGHLA2.VB"
  Public Overridable Function Hla2Apri(ByVal strDitta As String, ByVal stredCod As String, _
                                       ByVal stredDescr As String, ByRef dsHla2 As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldDocu.GetDataHla2(strDitta, stredCod, stredDescr, dsHla2)
      If dReturn = False Then Return False

      oCldDocu.SetTableDefaultValueFromDB("ARTICO", dsHla2)

      dsHla2Shared = dsHla2

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

#Region "funzioni specifiche per BNMGSCTR.VB"
  Public Overridable Function SctrApri(ByVal strDitta As String, ByRef ds As DataSet, ByVal ds2 As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      dsSctrTestata = ds2

      SctrSetDefaultValue(ds)

      RiempicolonneUnbound(ds)

      dsSctrShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsSctrShared.Tables("SCHETRASP").ColumnChanging, AddressOf BeforeColUpdate_SCHETRASP
      AddHandler dsSctrShared.Tables("SCHETRASP").ColumnChanged, AddressOf AfterColUpdate_SCHETRASP

      bSctrHasChanges = False

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
  Public Overridable Function SctrSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Dim i As Integer = 0
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not SctrTestPreSalva() Then Return False
      End If

      If bDelete Then
        dsSctrShared.Tables("SCHETRASP").Clear()
      End If

      dsSctrShared.Tables("SCHETRASP").AcceptChanges()
      bSctrHasChanges = False

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
  Public Overridable Sub SctrNuovo()
    Try
      If bSctrIsLoad Then SctrSetDefaultValue(dsSctrShared)

      '----------------------------------------
      'inserisco una nuova riga
      dsSctrShared.Tables("SCHETRASP").Rows.Add(dsSctrShared.Tables("SCHETRASP").NewRow)
      bSctrHasChanges = True

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
  Public Overridable Function SctrRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsSctrShared.Tables("SCHETRASP").Select(strFilter)(nRow).RejectChanges()
      bSctrHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function
  Public ReadOnly Property SctrRecordIsChanged() As Boolean
    Get
      Return bSctrHasChanges
    End Get
  End Property

  Public Overridable Sub SctrSetDefaultValue(ByRef ds As DataSet)
    Dim dttTmp As New DataTable
    Dim dttTmp2 As New DataTable
    Dim strTmp As String = ""
    Dim dsTmp As DataSet = Nothing
    Dim strDatscheda As String = ""
    Dim strTipork As String = ""
    Dim strSerie As String = ""
    Dim strScheTraspPropostaCaricatore As String = ""
    Dim dQuant As Decimal
    Dim dPeso As Decimal
    Try
      If BORDINI Then
        strScheTraspPropostaCaricatore = oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "ScheTraspPropostaCaricatore", " ", " ", " ")
      Else
        strScheTraspPropostaCaricatore = oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSVEBOLL", "OPZIONI", ".", "ScheTraspPropostaCaricatore", " ", " ", " ")
      End If

      '-------------------------------------------------
      'Imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("SCHETRASP").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("SCHETRASP").Columns("et_tipork").DefaultValue = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_tipork)
      ds.Tables("SCHETRASP").Columns("et_anno").DefaultValue = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_anno)
      ds.Tables("SCHETRASP").Columns("et_serie").DefaultValue = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_serie)
      ds.Tables("SCHETRASP").Columns("et_numdoc").DefaultValue = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_numdoc)

      If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor) <> 0 Then
        ds.Tables("SCHETRASP").Columns("et_vetttipo").DefaultValue = "1"
        ds.Tables("SCHETRASP").Columns("et_vettcod").DefaultValue = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor)
        If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor), strDittaCorrente, "TABVETT", "N", , dttTmp2) Then
          ds.Tables("SCHETRASP").Columns("et_vettalbo").DefaultValue = NTSCStr(dttTmp2.Rows(0)!tb_iscralb)
        End If
      ElseIf NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor2) <> 0 Then
        ds.Tables("SCHETRASP").Columns("et_vetttipo").DefaultValue = "2"
        ds.Tables("SCHETRASP").Columns("et_vettcod").DefaultValue = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor2)
        If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor2), strDittaCorrente, "TABVETT", "N", , dttTmp2) Then
          ds.Tables("SCHETRASP").Columns("et_vettalbo").DefaultValue = NTSCStr(dttTmp2.Rows(0)!tb_iscralb)
        End If
      End If

      strTmp = ""
      If oCldDocu.ValCodiceDb(NTSCStr(ds.Tables("SCHETRASP").Columns("et_vettcod").DefaultValue), strDittaCorrente, "TABVETT", "N", strTmp) Then
        ds.Tables("SCHETRASP").Columns("xx_vettcod").DefaultValue = strTmp
      End If

      If oCldDocu.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", , dttTmp) Then
        ds.Tables("SCHETRASP").Columns("et_commrs1").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azrags1)
        ds.Tables("SCHETRASP").Columns("et_commrs2").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azrags2)
        ds.Tables("SCHETRASP").Columns("et_commind").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azindir)
        ds.Tables("SCHETRASP").Columns("et_commcap").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azcap)
        ds.Tables("SCHETRASP").Columns("et_commcitt").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azcitta)
        ds.Tables("SCHETRASP").Columns("et_commprov").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azprov)
        ds.Tables("SCHETRASP").Columns("et_commstat").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azstato)
        strTmp = ""
        If oCldDocu.ValCodiceDb(NTSCStr(dttTmp.Rows(0)!tb_azstato), strDittaCorrente, "TABSTAT", "S", strTmp) Then
          ds.Tables("SCHETRASP").Columns("xx_commstat").DefaultValue = strTmp
        End If
        ds.Tables("SCHETRASP").Columns("et_commcf").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azcodf)
        ds.Tables("SCHETRASP").Columns("et_commpi").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azpiva)
        ds.Tables("SCHETRASP").Columns("et_commtel").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_aztelef)
        ds.Tables("SCHETRASP").Columns("et_commmail").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azemail)

        ds.Tables("SCHETRASP").Columns("et_proprs1").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azrags1)
        ds.Tables("SCHETRASP").Columns("et_proprs2").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azrags2)
        ds.Tables("SCHETRASP").Columns("et_propind").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azindir)
        ds.Tables("SCHETRASP").Columns("et_propcap").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azcap)
        ds.Tables("SCHETRASP").Columns("et_propcitt").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azcitta)
        ds.Tables("SCHETRASP").Columns("et_propprov").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azprov)
        ds.Tables("SCHETRASP").Columns("et_propstat").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azstato)
        strTmp = ""
        If oCldDocu.ValCodiceDb(NTSCStr(dttTmp.Rows(0)!tb_azstato), strDittaCorrente, "TABSTAT", "S", strTmp) Then
          ds.Tables("SCHETRASP").Columns("xx_propstat").DefaultValue = strTmp
        End If
        ds.Tables("SCHETRASP").Columns("et_propcf").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azcodf)
        ds.Tables("SCHETRASP").Columns("et_proppi").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azpiva)
        ds.Tables("SCHETRASP").Columns("et_proptel").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_aztelef)
        ds.Tables("SCHETRASP").Columns("et_propmail").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azemail)

        ds.Tables("SCHETRASP").Columns("et_lcind").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azindir)
        ds.Tables("SCHETRASP").Columns("et_lccap").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azcap)
        ds.Tables("SCHETRASP").Columns("et_lccitt").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azcitta)
        ds.Tables("SCHETRASP").Columns("et_lcprov").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azprov)
        ds.Tables("SCHETRASP").Columns("et_lcstat").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azstato)
        strTmp = ""
        If oCldDocu.ValCodiceDb(NTSCStr(dttTmp.Rows(0)!tb_azstato), strDittaCorrente, "TABSTAT", "S", strTmp) Then
          ds.Tables("SCHETRASP").Columns("xx_lcstat").DefaultValue = strTmp
        End If
      End If

      'propongo il caricatore dal opzione se settata
      If Trim(strScheTraspPropostaCaricatore) <> "" And ds.Tables("SCHETRASP").Rows.Count = 0 Then
        Select Case strScheTraspPropostaCaricatore
          Case "V1"
            ds.Tables("SCHETRASP").Columns("et_caritipo").DefaultValue = "1"
          Case "V2"
            ds.Tables("SCHETRASP").Columns("et_caritipo").DefaultValue = "2"
          Case "D"
            ds.Tables("SCHETRASP").Columns("et_caritipo").DefaultValue = "D"
          Case "C"
            ds.Tables("SCHETRASP").Columns("et_caritipo").DefaultValue = "C"
          Case "D1"
            ds.Tables("SCHETRASP").Columns("et_caritipo").DefaultValue = "X"
          Case "D2"
            ds.Tables("SCHETRASP").Columns("et_caritipo").DefaultValue = "Y"
          Case "A"
            ds.Tables("SCHETRASP").Columns("et_caritipo").DefaultValue = "M"
        End Select

        'controllo la coerenza campi e propongo gli altri campi del caricatore
        If SctrProponiValidaCaricatore(Nothing, ds.Tables("SCHETRASP")) Then
          GoTo Salta2
        Else
          'se mi da un incoerenza sui campi obbligatori uso la proposizione standard senza opzione 
          ds.Tables("SCHETRASP").Columns("et_caritipo").DefaultValue = "D"
          ds.Tables("SCHETRASP").Columns("et_caricod").DefaultValue = "0"
          ds.Tables("SCHETRASP").Columns("et_caridest").DefaultValue = "0"
        End If
      End If

      '1 or 2 vett
      If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor) = 0 Then
        GoTo Salta
      End If
      If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor), strDittaCorrente, "TABVETT", "N", , dttTmp2) Then
        If NTSCInt(dttTmp2.Rows(0)!tb_codforn) = 0 Then
          GoTo Salta
        End If
        If oCldDocu.ValCodiceDb(NTSCStr(dttTmp2.Rows(0)!tb_codforn), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
          ds.Tables("SCHETRASP").Columns("et_carirs1").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_descr1)
          ds.Tables("SCHETRASP").Columns("et_carirs2").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_descr2)
          ds.Tables("SCHETRASP").Columns("et_cariind").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_indir)
          ds.Tables("SCHETRASP").Columns("et_caricap").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_cap)
          ds.Tables("SCHETRASP").Columns("et_caricitt").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_citta)
          ds.Tables("SCHETRASP").Columns("et_cariprov").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_prov)
          ds.Tables("SCHETRASP").Columns("et_caristat").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_stato)
          strTmp = ""
          If oCldDocu.ValCodiceDb(NTSCStr(dttTmp.Rows(0)!an_stato), strDittaCorrente, "TABSTAT", "S", strTmp) Then
            ds.Tables("SCHETRASP").Columns("xx_caristat").DefaultValue = strTmp
          End If
          ds.Tables("SCHETRASP").Columns("et_caricf").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_codfis)
          ds.Tables("SCHETRASP").Columns("et_caripi").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_pariva)
          ds.Tables("SCHETRASP").Columns("et_caritel").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_telef)
          ds.Tables("SCHETRASP").Columns("et_carimail").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_email)
          ds.Tables("SCHETRASP").Columns("et_caritipo").DefaultValue = "1"
          ds.Tables("SCHETRASP").Columns("et_caricod").DefaultValue = NTSCInt(dttTmp2.Rows(0)!tb_codforn)
          ds.Tables("SCHETRASP").Columns("et_caridest").DefaultValue = "0"
          GoTo Salta2
        End If
      End If
Salta:
      If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor2) = 0 Then
        GoTo Salta2
      End If
      If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor2), strDittaCorrente, "TABVETT", "N", , dttTmp2) Then
        If NTSCInt(dttTmp2.Rows(0)!tb_codforn) = 0 Then
          GoTo Salta2
        End If
        If oCldDocu.ValCodiceDb(NTSCStr(dttTmp2.Rows(0)!tb_codforn), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
          ds.Tables("SCHETRASP").Columns("et_carirs1").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_descr1)
          ds.Tables("SCHETRASP").Columns("et_carirs2").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_descr2)
          ds.Tables("SCHETRASP").Columns("et_cariind").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_indir)
          ds.Tables("SCHETRASP").Columns("et_caricap").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_cap)
          ds.Tables("SCHETRASP").Columns("et_caricitt").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_citta)
          ds.Tables("SCHETRASP").Columns("et_cariprov").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_prov)
          ds.Tables("SCHETRASP").Columns("et_caristat").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_stato)
          strTmp = ""
          If oCldDocu.ValCodiceDb(NTSCStr(dttTmp.Rows(0)!an_stato), strDittaCorrente, "TABSTAT", "S", strTmp) Then
            ds.Tables("SCHETRASP").Columns("xx_caristat").DefaultValue = strTmp
          End If
          ds.Tables("SCHETRASP").Columns("et_caricf").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_codfis)
          ds.Tables("SCHETRASP").Columns("et_caripi").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_pariva)
          ds.Tables("SCHETRASP").Columns("et_caritel").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_telef)
          ds.Tables("SCHETRASP").Columns("et_carimail").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_email)
          ds.Tables("SCHETRASP").Columns("et_caritipo").DefaultValue = "2"
          ds.Tables("SCHETRASP").Columns("et_caricod").DefaultValue = NTSCInt(dttTmp2.Rows(0)!tb_codforn)
          ds.Tables("SCHETRASP").Columns("et_caridest").DefaultValue = "0"
        End If
      End If
Salta2:
      'cliente
      ds.Tables("SCHETRASP").Columns("et_lstipo").DefaultValue = "C"
      If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
        GoTo Salta3
      End If
      If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
        ds.Tables("SCHETRASP").Columns("et_lsind").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_indir)
        ds.Tables("SCHETRASP").Columns("et_lscap").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_cap)
        ds.Tables("SCHETRASP").Columns("et_lscitt").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_citta)
        ds.Tables("SCHETRASP").Columns("et_lsprov").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_prov)
        ds.Tables("SCHETRASP").Columns("et_lsstat").DefaultValue = NTSCStr(dttTmp.Rows(0)!an_stato)
        strTmp = ""
        If oCldDocu.ValCodiceDb(NTSCStr(dttTmp.Rows(0)!an_stato), strDittaCorrente, "TABSTAT", "S", strTmp) Then
          ds.Tables("SCHETRASP").Columns("xx_lsstat").DefaultValue = strTmp
        End If
        ds.Tables("SCHETRASP").Columns("et_lstipo").DefaultValue = "C"
        ds.Tables("SCHETRASP").Columns("et_lsconto").DefaultValue = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
        ds.Tables("SCHETRASP").Columns("et_lsdest").DefaultValue = "0"
      End If
Salta3:
      ds.Tables("SCHETRASP").Columns("et_compcitt").DefaultValue = NTSCStr(oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSVEBOLL", "RECENT", ".", "RecentCittàSchetrasp", " ", " ", " "))

      oCldDocu.GetOperat(strDittaCorrente, oApp.User.Nome, dsTmp)
      If dsTmp.Tables("OPERAT").Rows.Count = 0 Then
        ds.Tables("SCHETRASP").Columns("et_compilat").DefaultValue = oApp.User.Nome
      Else
        ds.Tables("SCHETRASP").Columns("et_compilat").DefaultValue = NTSCStr(dsTmp.Tables("OPERAT").Rows(0)!xx_operat)
      End If

      If (BORDINI And CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "ScheTraspUsaTotColli", "0", " ", "0"))) _
        Or (BORDINI = False And CBool(oCldDocu.GetSettingBusDitt(strDittaCorrente, "BSVEBOLL", "OPZIONI", ".", "ScheTraspUsaTotColli", "0", " ", "0"))) Then
        If bArtConai Then
          dQuant = dSctrColli
        Else
          dQuant = NTSCDec(dsSctrTestata.Tables("TESTA").Rows(0)!et_totcoll)
        End If
      Else
        If bArtConai Then
          dQuant = dSctrQuat
        Else
          dQuant = NTSCDec(dsSctrTestata.Tables("TESTA").Rows(0)!xx_totquant)
        End If
      End If
      ds.Tables("SCHETRASP").Columns("et_quant").DefaultValue = dQuant

      If bArtConai Then
        dPeso = dSctrPeso
      Else
        dPeso = NTSCDec(dsSctrTestata.Tables("TESTA").Rows(0)!et_peso)
      End If
      ds.Tables("SCHETRASP").Columns("et_peso").DefaultValue = dPeso

      If BORDINI = False Then
        strDatscheda = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_dtiniz)
      Else
        strDatscheda = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_datcons)
      End If

      Select Case NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_tipork)
        Case "A" : strTipork = "Fattura Imm. emessa"
        Case "B" : strTipork = "DDT emesso"
        Case "C" : strTipork = "Corrispettivo emesso"
        Case "E" : strTipork = "Nota di Addeb. emessa"
        Case "F" : strTipork = "Ric.Fiscale Emessa"
        Case "I" : strTipork = "Riemissione Ric.Fiscali"
        Case "J" : strTipork = "Nota Accr. ricevuta"
        Case "L" : strTipork = "Fattura Imm. ricevuta"
        Case "M" : strTipork = "DDT ricevuto"
        Case "N" : strTipork = "Nota Accr. emessa"
        Case "S" : strTipork = "Fatt.Ric.Fisc. Emessa"
        Case "T" : strTipork = "Carico da produzione"
        Case "W" : strTipork = "Nota di prelievo"
        Case "Z" : strTipork = "Bolla di mov. interna"
        Case "R" : strTipork = "Impegno cliente"
        Case "O" : strTipork = "Ordine fornitore"
        Case "H" : strTipork = "Ordine di produzione"
        Case "X" : strTipork = "Impegno Trasferimento"
        Case "Q" : strTipork = "Preventivo"
        Case "#" : strTipork = "Impegno di commessa"
        Case "V" : strTipork = "Impegno cliente aperto"
        Case "$" : strTipork = "Ordine fornitore aperto"
        Case Else : strTipork = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_tipork)
      End Select

      strSerie = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_serie)
      ds.Tables("SCHETRASP").Columns("xx_riferim").DefaultValue = strTipork & " n° " & NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_numdoc) & NTSCStr(IIf(strSerie = "" Or strSerie = " ", "", "/" & strSerie)) & " del " & NTSCDate(dsSctrTestata.Tables("TESTA").Rows(0)!et_datdoc).ToShortDateString

      ds.Tables("SCHETRASP").Columns("xx_datcomp").DefaultValue = strDatscheda

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

  Public Overridable Sub BeforeColUpdate_SCHETRASP(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return
    Dim strErr As String = ""
    Try
      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
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
      Dim strFunction As String = "BeforeColUpdate_SCHETRASP_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub AfterColUpdate_SCHETRASP(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    If bInUnload Then Return
    If bNonValidare Then Return

    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bSctrHasChanges = True
      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_SCHETRASP_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub BeforeColUpdate_SCHETRASP_et_vetttipo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp2 As New DataTable
    Try
      If e.ProposedValue.ToString = "1" Then
        If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129026027449583376, "Attenzione! Non è possibile selezionare il Vettore 1 in quanto non è valorizzato nel documento")))
          e.ProposedValue = "2"
          e.Row!et_vettcod = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor2)
        Else
          e.Row!et_vettcod = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor)
        End If
      End If
      If e.ProposedValue.ToString = "2" Then
        e.Row!et_vettcod = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor2)
        If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor2) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128992909283529137, "Attenzione! Non è possibile selezionare il Vettore 2 in quanto non è valorizzato nel documento")))
          e.ProposedValue = "1"
          e.Row!et_vettcod = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor)
        Else
          e.Row!et_vettcod = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor2)
        End If
      End If
      If Not (e.ProposedValue.ToString = "1" Or e.ProposedValue.ToString = "2") Then
        e.Row!et_vettcod = "0"
      End If

      If e.ProposedValue.ToString = "1" Then
        If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor), strDittaCorrente, "TABVETT", "N", , dttTmp2) Then
          e.Row!et_vettalbo = NTSCStr(dttTmp2.Rows(0)!tb_iscralb)
        End If
      ElseIf e.ProposedValue.ToString = "2" Then
        If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor2), strDittaCorrente, "TABVETT", "N", , dttTmp2) Then
          e.Row!et_vettalbo = NTSCStr(dttTmp2.Rows(0)!tb_iscralb)
        End If
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
  End Sub
  Public Overridable Sub BeforeColUpdate_SCHETRASP_et_vettcod(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      ValCodice(e, "VETT", "xx_vettcod", oApp.Tr(Me, 128989635359429617, "Codice vettore |" & NTSCInt(e.ProposedValue).ToString & "| non corretto"))
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
  Public Overridable Sub BeforeColUpdate_SCHETRASP_et_commtipo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      Select Case e.ProposedValue.ToString
        Case "D" 'ditta
          e.Row!et_commcod = "0"
          e.Row!et_commdest = "0"
          If oCldDocu.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", , dttTmp) Then
            e.Row!et_commrs1 = NTSCStr(dttTmp.Rows(0)!tb_azrags1)
            e.Row!et_commrs2 = NTSCStr(dttTmp.Rows(0)!tb_azrags2)
            e.Row!et_commind = NTSCStr(dttTmp.Rows(0)!tb_azindir)
            e.Row!et_commcap = NTSCStr(dttTmp.Rows(0)!tb_azcap)
            e.Row!et_commcitt = NTSCStr(dttTmp.Rows(0)!tb_azcitta)
            e.Row!et_commprov = NTSCStr(dttTmp.Rows(0)!tb_azprov)
            e.Row!et_commstat = NTSCStr(dttTmp.Rows(0)!tb_azstato)
            e.Row!et_commcf = NTSCStr(dttTmp.Rows(0)!tb_azcodf)
            e.Row!et_commpi = NTSCStr(dttTmp.Rows(0)!tb_azpiva)
            e.Row!et_commtel = NTSCStr(dttTmp.Rows(0)!tb_aztelef)
            e.Row!et_commmail = NTSCStr(dttTmp.Rows(0)!tb_azemail)
            Return
          End If
        Case "C" 'cliente
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028377342617440, "Attenzione! Non è possibile, come committente, scegliere 'Cliente/forn.'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
            e.Row!et_commcod = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
            e.Row!et_commdest = "0"
            e.Row!et_commrs1 = NTSCStr(dttTmp.Rows(0)!an_descr1)
            e.Row!et_commrs2 = NTSCStr(dttTmp.Rows(0)!an_descr2)
            e.Row!et_commind = NTSCStr(dttTmp.Rows(0)!an_indir)
            e.Row!et_commcap = NTSCStr(dttTmp.Rows(0)!an_cap)
            e.Row!et_commcitt = NTSCStr(dttTmp.Rows(0)!an_citta)
            e.Row!et_commprov = NTSCStr(dttTmp.Rows(0)!an_prov)
            e.Row!et_commstat = NTSCStr(dttTmp.Rows(0)!an_stato)
            e.Row!et_commcf = NTSCStr(dttTmp.Rows(0)!an_codfis)
            e.Row!et_commpi = NTSCStr(dttTmp.Rows(0)!an_pariva)
            e.Row!et_commtel = NTSCStr(dttTmp.Rows(0)!an_telef)
            e.Row!et_commmail = NTSCStr(dttTmp.Rows(0)!an_email)
            Return
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "X" 'cliente dest 1
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028322281045567, "Attenzione! Non è possibile, come committente, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return
          End If
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028322402591729, "Attenzione! Non è possibile, come committente, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Destinaz.")))
            Return
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest), strDittaCorrente, "DESTDIV", "N", , dttTmp, NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)) Then
            e.Row!et_commcod = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
            e.Row!et_commdest = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest)
            e.Row!et_commrs1 = NTSCStr(dttTmp.Rows(0)!dd_nomdest)
            e.Row!et_commrs2 = NTSCStr(dttTmp.Rows(0)!dd_nomdest2)
            e.Row!et_commind = NTSCStr(dttTmp.Rows(0)!dd_inddest)
            e.Row!et_commcap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
            e.Row!et_commcitt = NTSCStr(dttTmp.Rows(0)!dd_locdest)
            e.Row!et_commprov = NTSCStr(dttTmp.Rows(0)!dd_prodest)
            e.Row!et_commstat = NTSCStr(dttTmp.Rows(0)!dd_stato)
            e.Row!et_commcf = NTSCStr(dttTmp.Rows(0)!dd_codfis)
            e.Row!et_commpi = NTSCStr(dttTmp.Rows(0)!dd_pariva)
            e.Row!et_commtel = NTSCStr(dttTmp.Rows(0)!dd_telef)
            e.Row!et_commmail = NTSCStr(dttTmp.Rows(0)!dd_email)
            Return
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "Y" 'cliente dest 2
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028323022841713, "Attenzione! Non è possibile, come committente, scegliere 'Cliente/forn. (dest. 2)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return
          End If
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028323168864207, "Attenzione! Non è possibile, come committente, scegliere 'Cliente/forn. (dest. 2)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Destin. diversa 2")))
            Return
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2), strDittaCorrente, "DESTDIV", "N", , dttTmp, NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)) Then
            e.Row!et_commcod = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
            e.Row!et_commdest = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2)
            e.Row!et_commrs1 = NTSCStr(dttTmp.Rows(0)!dd_nomdest)
            e.Row!et_commrs2 = NTSCStr(dttTmp.Rows(0)!dd_nomdest2)
            e.Row!et_commind = NTSCStr(dttTmp.Rows(0)!dd_inddest)
            e.Row!et_commcap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
            e.Row!et_commcitt = NTSCStr(dttTmp.Rows(0)!dd_locdest)
            e.Row!et_commprov = NTSCStr(dttTmp.Rows(0)!dd_prodest)
            e.Row!et_commstat = NTSCStr(dttTmp.Rows(0)!dd_stato)
            e.Row!et_commcf = NTSCStr(dttTmp.Rows(0)!dd_codfis)
            e.Row!et_commpi = NTSCStr(dttTmp.Rows(0)!dd_pariva)
            e.Row!et_commtel = NTSCStr(dttTmp.Rows(0)!dd_telef)
            e.Row!et_commmail = NTSCStr(dttTmp.Rows(0)!dd_email)
            Return
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "F" 'conto fatturazione
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_contfatt) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028377549144246, "Attenzione! Non è possibile, come committente, scegliere 'Conto fatturazione'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Conto fatturaz.")))
            Return
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_contfatt), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
            e.Row!et_commcod = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_contfatt)
            e.Row!et_commdest = "0"
            e.Row!et_commrs1 = NTSCStr(dttTmp.Rows(0)!an_descr1)
            e.Row!et_commrs2 = NTSCStr(dttTmp.Rows(0)!an_descr2)
            e.Row!et_commind = NTSCStr(dttTmp.Rows(0)!an_indir)
            e.Row!et_commcap = NTSCStr(dttTmp.Rows(0)!an_cap)
            e.Row!et_commcitt = NTSCStr(dttTmp.Rows(0)!an_citta)
            e.Row!et_commprov = NTSCStr(dttTmp.Rows(0)!an_prov)
            e.Row!et_commstat = NTSCStr(dttTmp.Rows(0)!an_stato)
            e.Row!et_commcf = NTSCStr(dttTmp.Rows(0)!an_codfis)
            e.Row!et_commpi = NTSCStr(dttTmp.Rows(0)!an_pariva)
            e.Row!et_commtel = NTSCStr(dttTmp.Rows(0)!an_telef)
            e.Row!et_commmail = NTSCStr(dttTmp.Rows(0)!an_email)
            Return
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "M" 'manuale
          e.Row!et_commcod = "0"
          e.Row!et_commdest = "0"
          e.Row!et_commrs1 = ""
          e.Row!et_commrs2 = ""
          e.Row!et_commind = ""
          e.Row!et_commcap = ""
          e.Row!et_commcitt = ""
          e.Row!et_commprov = ""
          e.Row!et_commstat = ""
          e.Row!et_commcf = ""
          e.Row!et_commpi = ""
          e.Row!et_commtel = ""
          e.Row!et_commmail = ""
          Return
      End Select

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
  Public Overridable Sub BeforeColUpdate_SCHETRASP_et_caritipo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If Not SctrProponiValidaCaricatore(e, Nothing) Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        Return
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
  End Sub
  Public Overridable Sub BeforeColUpdate_SCHETRASP_et_proptipo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      Select Case e.ProposedValue.ToString
        Case "D" 'ditta
          e.Row!et_propcod = "0"
          e.Row!et_propdest = "0"
          If oCldDocu.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", , dttTmp) Then
            e.Row!et_proprs1 = NTSCStr(dttTmp.Rows(0)!tb_azrags1)
            e.Row!et_proprs2 = NTSCStr(dttTmp.Rows(0)!tb_azrags2)
            e.Row!et_propind = NTSCStr(dttTmp.Rows(0)!tb_azindir)
            e.Row!et_propcap = NTSCStr(dttTmp.Rows(0)!tb_azcap)
            e.Row!et_propcitt = NTSCStr(dttTmp.Rows(0)!tb_azcitta)
            e.Row!et_propprov = NTSCStr(dttTmp.Rows(0)!tb_azprov)
            e.Row!et_propstat = NTSCStr(dttTmp.Rows(0)!tb_azstato)
            e.Row!et_propcf = NTSCStr(dttTmp.Rows(0)!tb_azcodf)
            e.Row!et_proppi = NTSCStr(dttTmp.Rows(0)!tb_azpiva)
            e.Row!et_proptel = NTSCStr(dttTmp.Rows(0)!tb_aztelef)
            e.Row!et_propmail = NTSCStr(dttTmp.Rows(0)!tb_azemail)
            Return
          End If
        Case "C" 'cliente
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028331778779527, "Attenzione! Non è possibile, come proprietario, scegliere 'Cliente/forn.'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
            e.Row!et_propcod = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
            e.Row!et_propdest = "0"
            e.Row!et_proprs1 = NTSCStr(dttTmp.Rows(0)!an_descr1)
            e.Row!et_proprs2 = NTSCStr(dttTmp.Rows(0)!an_descr2)
            e.Row!et_propind = NTSCStr(dttTmp.Rows(0)!an_indir)
            e.Row!et_propcap = NTSCStr(dttTmp.Rows(0)!an_cap)
            e.Row!et_propcitt = NTSCStr(dttTmp.Rows(0)!an_citta)
            e.Row!et_propprov = NTSCStr(dttTmp.Rows(0)!an_prov)
            e.Row!et_propstat = NTSCStr(dttTmp.Rows(0)!an_stato)
            e.Row!et_propcf = NTSCStr(dttTmp.Rows(0)!an_codfis)
            e.Row!et_proppi = NTSCStr(dttTmp.Rows(0)!an_pariva)
            e.Row!et_proptel = NTSCStr(dttTmp.Rows(0)!an_telef)
            e.Row!et_propmail = NTSCStr(dttTmp.Rows(0)!an_email)
            Return
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "X" 'cliente dest 1
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028332160400635, "Attenzione! Non è possibile, come proprietario, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return
          End If
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028332181341351, "Attenzione! Non è possibile, come proprietario, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Destinaz.")))
            Return
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest), strDittaCorrente, "DESTDIV", "N", , dttTmp, NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)) Then
            e.Row!et_propcod = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
            e.Row!et_propdest = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest)
            e.Row!et_proprs1 = NTSCStr(dttTmp.Rows(0)!dd_nomdest)
            e.Row!et_proprs2 = NTSCStr(dttTmp.Rows(0)!dd_nomdest2)
            e.Row!et_propind = NTSCStr(dttTmp.Rows(0)!dd_inddest)
            e.Row!et_propcap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
            e.Row!et_propcitt = NTSCStr(dttTmp.Rows(0)!dd_locdest)
            e.Row!et_propprov = NTSCStr(dttTmp.Rows(0)!dd_prodest)
            e.Row!et_propstat = NTSCStr(dttTmp.Rows(0)!dd_stato)
            e.Row!et_propcf = NTSCStr(dttTmp.Rows(0)!dd_codfis)
            e.Row!et_proppi = NTSCStr(dttTmp.Rows(0)!dd_pariva)
            e.Row!et_proptel = NTSCStr(dttTmp.Rows(0)!dd_telef)
            e.Row!et_propmail = NTSCStr(dttTmp.Rows(0)!dd_email)
            Return
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "Y" 'cliente dest 2
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028332372308179, "Attenzione! Non è possibile, come proprietario, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return
          End If
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028332391529881, "Attenzione! Non è possibile, come proprietario, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Destin. diversa 2")))
            Return
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2), strDittaCorrente, "DESTDIV", "N", , dttTmp, NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)) Then
            e.Row!et_propcod = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
            e.Row!et_propdest = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2)
            e.Row!et_proprs1 = NTSCStr(dttTmp.Rows(0)!dd_nomdest)
            e.Row!et_proprs2 = NTSCStr(dttTmp.Rows(0)!dd_nomdest2)
            e.Row!et_propind = NTSCStr(dttTmp.Rows(0)!dd_inddest)
            e.Row!et_propcap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
            e.Row!et_propcitt = NTSCStr(dttTmp.Rows(0)!dd_locdest)
            e.Row!et_propprov = NTSCStr(dttTmp.Rows(0)!dd_prodest)
            e.Row!et_propstat = NTSCStr(dttTmp.Rows(0)!dd_stato)
            e.Row!et_propcf = NTSCStr(dttTmp.Rows(0)!dd_codfis)
            e.Row!et_proppi = NTSCStr(dttTmp.Rows(0)!dd_pariva)
            e.Row!et_proptel = NTSCStr(dttTmp.Rows(0)!dd_telef)
            e.Row!et_propmail = NTSCStr(dttTmp.Rows(0)!dd_email)
            Return
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "F" 'conto fatturazione
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_contfatt) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028333751738777, "Attenzione! Non è possibile, come proprietario, scegliere 'Conto fatturazione'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Conto fatturaz.")))
            Return
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_contfatt), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
            e.Row!et_propcod = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_contfatt)
            e.Row!et_propdest = "0"
            e.Row!et_proprs1 = NTSCStr(dttTmp.Rows(0)!an_descr1)
            e.Row!et_proprs2 = NTSCStr(dttTmp.Rows(0)!an_descr2)
            e.Row!et_propind = NTSCStr(dttTmp.Rows(0)!an_indir)
            e.Row!et_propcap = NTSCStr(dttTmp.Rows(0)!an_cap)
            e.Row!et_propcitt = NTSCStr(dttTmp.Rows(0)!an_citta)
            e.Row!et_propprov = NTSCStr(dttTmp.Rows(0)!an_prov)
            e.Row!et_propstat = NTSCStr(dttTmp.Rows(0)!an_stato)
            e.Row!et_propcf = NTSCStr(dttTmp.Rows(0)!an_codfis)
            e.Row!et_proppi = NTSCStr(dttTmp.Rows(0)!an_pariva)
            e.Row!et_proptel = NTSCStr(dttTmp.Rows(0)!an_telef)
            e.Row!et_propmail = NTSCStr(dttTmp.Rows(0)!an_email)
            Return
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "M" 'manuale
          e.Row!et_propcod = "0"
          e.Row!et_propdest = "0"
          e.Row!et_proprs1 = ""
          e.Row!et_proprs2 = ""
          e.Row!et_propind = ""
          e.Row!et_propcap = ""
          e.Row!et_propcitt = ""
          e.Row!et_propprov = ""
          e.Row!et_propstat = ""
          e.Row!et_propcf = ""
          e.Row!et_proppi = ""
          e.Row!et_proptel = ""
          e.Row!et_propmail = ""
          Return
        Case "N" '(Non definito)
          e.Row!et_propcod = "0"
          e.Row!et_propdest = "0"
          e.Row!et_proprs1 = " "
          e.Row!et_proprs2 = ""
          e.Row!et_propind = " "
          e.Row!et_propcap = ""
          e.Row!et_propcitt = " "
          e.Row!et_propprov = ""
          e.Row!et_propstat = ""
          e.Row!et_propcf = ""
          e.Row!et_proppi = " "
          e.Row!et_proptel = " "
          e.Row!et_propmail = ""
          Return
      End Select

Azzera:
      e.Row!et_propcod = "0"
      e.Row!et_propdest = "0"
      e.Row!et_proprs1 = ""
      e.Row!et_proprs2 = ""
      e.Row!et_propind = ""
      e.Row!et_propcap = ""
      e.Row!et_propcitt = ""
      e.Row!et_propprov = ""
      e.Row!et_propstat = ""
      e.Row!et_propcf = ""
      e.Row!et_proppi = ""
      e.Row!et_proptel = ""
      e.Row!et_propmail = ""

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
  Public Overridable Sub BeforeColUpdate_SCHETRASP_et_lctipo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Dim dttTmp2 As New DataTable
    Try
      Select Case e.ProposedValue.ToString
        Case "D" 'ditta
          e.Row!et_lcconto = "0"
          e.Row!et_lcdest = "0"
          If oCldDocu.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", , dttTmp) Then
            e.Row!et_lcind = NTSCStr(dttTmp.Rows(0)!tb_azindir)
            e.Row!et_lccap = NTSCStr(dttTmp.Rows(0)!tb_azcap)
            e.Row!et_lccitt = NTSCStr(dttTmp.Rows(0)!tb_azcitta)
            e.Row!et_lcprov = NTSCStr(dttTmp.Rows(0)!tb_azprov)
            e.Row!et_lcstat = NTSCStr(dttTmp.Rows(0)!tb_azstato)
            Return
          End If
        Case "W" 'mag 1 dest
          e.Row!et_lcconto = "0"
          e.Row!et_lcdest = "0"
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_magaz) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028336437455557, "Attenzione! Non è possibile, come luogo carico, scegliere 'Ditta corrente (mag. 1 dest.)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Magazzino 1")))
            Return
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_magaz), strDittaCorrente, "TABMAGA", "N", , dttTmp2) Then
            If NTSCInt(dttTmp2.Rows(0)!tb_magconto) = 0 Then
              e.ProposedValue = e.Row(e.Column.ColumnName)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028342181680083, "Attenzione! Non è possibile, come luogo carico, scegliere 'Ditta corrente (mag. 1 dest.)'" & vbCrLf & _
                "quando non è valorizzato il Conto C/F della tabella magazzini relativo al Magazzino 1 indicato nel documento")))
              Return
            End If
            If NTSCInt(dttTmp2.Rows(0)!tb_coddest) = 0 Then
              e.ProposedValue = e.Row(e.Column.ColumnName)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028378043121372, "Attenzione! Non è possibile, come luogo carico, scegliere 'Ditta corrente (mag. 1 dest.)'" & vbCrLf & _
                "quando non è valorizzata la Destinazione della tabella magazzini relativo al Magazzino 1 indicato nel documento")))
              Return
            End If
            If oCldDocu.ValCodiceDb(NTSCStr(dttTmp2.Rows(0)!tb_coddest), strDittaCorrente, "DESTDIV", "N", , dttTmp, NTSCStr(dttTmp2.Rows(0)!tb_magconto)) Then
              e.Row!et_lcconto = NTSCInt(dttTmp2.Rows(0)!tb_magconto)
              e.Row!et_lcdest = NTSCInt(dttTmp2.Rows(0)!tb_coddest)
              e.Row!et_lcind = NTSCStr(dttTmp.Rows(0)!dd_inddest)
              e.Row!et_lccap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
              e.Row!et_lccitt = NTSCStr(dttTmp.Rows(0)!dd_locdest)
              e.Row!et_lcprov = NTSCStr(dttTmp.Rows(0)!dd_prodest)
              e.Row!et_lcstat = NTSCStr(dttTmp.Rows(0)!dd_stato)
              Return
            Else
              e.ProposedValue = e.Row(e.Column.ColumnName)
              Return
            End If
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "Z" 'mag 2 dest
          e.Row!et_lcconto = "0"
          e.Row!et_lcdest = "0"
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_magaz2) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028377833313883, "Attenzione! Non è possibile, come luogo carico, scegliere 'Ditta corrente (mag. 2 dest.)'" & vbCrLf & _
               "quando sul documento non è valorizzato il campo Magazzino 2")))
            Return
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_magaz2), strDittaCorrente, "TABMAGA", "N", , dttTmp2) Then
            If NTSCInt(dttTmp2.Rows(0)!tb_magconto) = 0 Then
              e.ProposedValue = e.Row(e.Column.ColumnName)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028377940170415, "Attenzione! Non è possibile, come luogo carico, scegliere 'Ditta corrente (mag. 2 dest.)'" & vbCrLf & _
                "quando non è valorizzato il Conto C/F della tabella magazzini relativo al Magazzino 2 indicato nel documento")))
              Return
            End If
            If NTSCInt(dttTmp2.Rows(0)!tb_coddest) = 0 Then
              e.ProposedValue = e.Row(e.Column.ColumnName)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028342220435291, "Attenzione! Non è possibile, come luogo carico, scegliere 'Ditta corrente (mag. 2 dest.)'" & vbCrLf & _
                "quando non è valorizzata la Destinazione della tabella magazzini relativo al Magazzino 2 indicato nel documento")))
              Return
            End If
            If oCldDocu.ValCodiceDb(NTSCStr(dttTmp2.Rows(0)!tb_coddest), strDittaCorrente, "DESTDIV", "N", , dttTmp, NTSCStr(dttTmp2.Rows(0)!tb_magconto)) Then
              e.Row!et_lcconto = NTSCInt(dttTmp2.Rows(0)!tb_magconto)
              e.Row!et_lcdest = NTSCInt(dttTmp2.Rows(0)!tb_coddest)
              e.Row!et_lcind = NTSCStr(dttTmp.Rows(0)!dd_inddest)
              e.Row!et_lccap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
              e.Row!et_lccitt = NTSCStr(dttTmp.Rows(0)!dd_locdest)
              e.Row!et_lcprov = NTSCStr(dttTmp.Rows(0)!dd_prodest)
              e.Row!et_lcstat = NTSCStr(dttTmp.Rows(0)!dd_stato)
              Return
            Else
              e.ProposedValue = e.Row(e.Column.ColumnName)
              Return
            End If
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "C" 'cliente
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028377639597363, "Attenzione! Non è possibile, come luogo carico, scegliere 'Cliente/forn.'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return
          End If
          e.Row!et_lcconto = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
          e.Row!et_lcdest = "0"
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
            e.Row!et_lcind = NTSCStr(dttTmp.Rows(0)!an_indir)
            e.Row!et_lccap = NTSCStr(dttTmp.Rows(0)!an_cap)
            e.Row!et_lccitt = NTSCStr(dttTmp.Rows(0)!an_citta)
            e.Row!et_lcprov = NTSCStr(dttTmp.Rows(0)!an_prov)
            e.Row!et_lcstat = NTSCStr(dttTmp.Rows(0)!an_stato)
            Return
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "X" 'cliente dest 1
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028343756761796, "Attenzione! Non è possibile, come luogo carico, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return
          End If
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028343774249588, "Attenzione! Non è possibile, come luogo carico, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Destinaz.")))
            Return
          End If
          e.Row!et_lcconto = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
          e.Row!et_lcdest = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest)
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest), strDittaCorrente, "DESTDIV", "N", , dttTmp, NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)) Then
            e.Row!et_lcind = NTSCStr(dttTmp.Rows(0)!dd_inddest)
            e.Row!et_lccap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
            e.Row!et_lccitt = NTSCStr(dttTmp.Rows(0)!dd_locdest)
            e.Row!et_lcprov = NTSCStr(dttTmp.Rows(0)!dd_prodest)
            e.Row!et_lcstat = NTSCStr(dttTmp.Rows(0)!dd_stato)
            Return
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "Y" 'cliente dest 2
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028343800168994, "Attenzione! Non è possibile, come luogo carico, scegliere 'Cliente/forn. (dest. 2)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return
          End If
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028343816563799, "Attenzione! Non è possibile, come luogo carico, scegliere 'Cliente/forn. (dest. 2)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Destin. diversa 2")))
            Return
          End If
          e.Row!et_lcconto = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
          e.Row!et_lcdest = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2)
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2), strDittaCorrente, "DESTDIV", "N", , dttTmp, NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)) Then
            e.Row!et_lcind = NTSCStr(dttTmp.Rows(0)!dd_inddest)
            e.Row!et_lccap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
            e.Row!et_lccitt = NTSCStr(dttTmp.Rows(0)!dd_locdest)
            e.Row!et_lcprov = NTSCStr(dttTmp.Rows(0)!dd_prodest)
            e.Row!et_lcstat = NTSCStr(dttTmp.Rows(0)!dd_stato)
            Return
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "M" 'manuale
          e.Row!et_lcconto = "0"
          e.Row!et_lcdest = "0"
          e.Row!et_lcind = ""
          e.Row!et_lccap = ""
          e.Row!et_lccitt = ""
          e.Row!et_lcprov = ""
          e.Row!et_lcstat = ""
          Return
      End Select

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
  Public Overridable Sub BeforeColUpdate_SCHETRASP_et_lstipo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Dim dttTmp2 As New DataTable
    Try
      Select Case e.ProposedValue.ToString
        Case "D" 'ditta
          e.Row!et_lsconto = "0"
          e.Row!et_lsdest = "0"
          If oCldDocu.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", , dttTmp) Then
            e.Row!et_lsind = NTSCStr(dttTmp.Rows(0)!tb_azindir)
            e.Row!et_lscap = NTSCStr(dttTmp.Rows(0)!tb_azcap)
            e.Row!et_lscitt = NTSCStr(dttTmp.Rows(0)!tb_azcitta)
            e.Row!et_lsprov = NTSCStr(dttTmp.Rows(0)!tb_azprov)
            e.Row!et_lsstat = NTSCStr(dttTmp.Rows(0)!tb_azstato)
            Return
          End If
        Case "W" 'mag 1 dest
          e.Row!et_lsconto = "0"
          e.Row!et_lsdest = "0"
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_magaz) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028347520098015, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Ditta corrente (mag. 1 dest.)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Magazzino 1")))
            Return
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_magaz), strDittaCorrente, "TABMAGA", "N", , dttTmp2) Then
            If NTSCInt(dttTmp2.Rows(0)!tb_magconto) = 0 Then
              e.ProposedValue = e.Row(e.Column.ColumnName)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028347496044293, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Ditta corrente (mag. 1 dest.)'" & vbCrLf & _
                "quando non è valorizzato il Conto C/F della tabella magazzini relativo al Magazzino 1 indicato nel documento")))
              Return
            End If
            If NTSCInt(dttTmp2.Rows(0)!tb_coddest) = 0 Then
              e.ProposedValue = e.Row(e.Column.ColumnName)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028347477144940, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Ditta corrente (mag. 1 dest.)'" & vbCrLf & _
                "quando non è valorizzata la Destinazione della tabella magazzini relativo al Magazzino 1 indicato nel documento")))
              Return
            End If
            If oCldDocu.ValCodiceDb(NTSCStr(dttTmp2.Rows(0)!tb_coddest), strDittaCorrente, "DESTDIV", "N", , dttTmp, NTSCStr(dttTmp2.Rows(0)!tb_magconto)) Then
              e.Row!et_lsconto = NTSCInt(dttTmp2.Rows(0)!tb_magconto)
              e.Row!et_lsdest = NTSCInt(dttTmp2.Rows(0)!tb_coddest)
              e.Row!et_lsind = NTSCStr(dttTmp.Rows(0)!dd_inddest)
              e.Row!et_lscap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
              e.Row!et_lscitt = NTSCStr(dttTmp.Rows(0)!dd_locdest)
              e.Row!et_lsprov = NTSCStr(dttTmp.Rows(0)!dd_prodest)
              e.Row!et_lsstat = NTSCStr(dttTmp.Rows(0)!dd_stato)
              Return
            Else
              e.ProposedValue = e.Row(e.Column.ColumnName)
              Return
            End If
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "Z" 'mag 2 dest
          e.Row!et_lsconto = "0"
          e.Row!et_lsdest = "0"
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_magaz2) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028347453091218, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Ditta corrente (mag. 2 dest.)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Magazzino 2")))
            Return
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_magaz2), strDittaCorrente, "TABMAGA", "N", , dttTmp2) Then
            If NTSCInt(dttTmp2.Rows(0)!tb_magconto) = 0 Then
              e.ProposedValue = e.Row(e.Column.ColumnName)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028347423726934, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Ditta corrente (mag. 2 dest.)'" & vbCrLf & _
                "quando non è valorizzato il Conto C/F della tabella magazzini relativo al Magazzino 2 indicato nel documento")))
              Return
            End If
            If NTSCInt(dttTmp2.Rows(0)!tb_coddest) = 0 Then
              e.ProposedValue = e.Row(e.Column.ColumnName)
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028347403578037, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Ditta corrente (mag. 2 dest.)'" & vbCrLf & _
                "quando non è valorizzata la Destinazione della tabella magazzini relativo al Magazzino 2 indicato nel documento")))
              Return
            End If
            If oCldDocu.ValCodiceDb(NTSCStr(dttTmp2.Rows(0)!tb_coddest), strDittaCorrente, "DESTDIV", "N", , dttTmp, NTSCStr(dttTmp2.Rows(0)!tb_magconto)) Then
              e.Row!et_lsconto = NTSCInt(dttTmp2.Rows(0)!tb_magconto)
              e.Row!et_lsdest = NTSCInt(dttTmp2.Rows(0)!tb_coddest)
              e.Row!et_lsind = NTSCStr(dttTmp.Rows(0)!dd_inddest)
              e.Row!et_lscap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
              e.Row!et_lscitt = NTSCStr(dttTmp.Rows(0)!dd_locdest)
              e.Row!et_lsprov = NTSCStr(dttTmp.Rows(0)!dd_prodest)
              e.Row!et_lsstat = NTSCStr(dttTmp.Rows(0)!dd_stato)
              Return
            Else
              e.ProposedValue = e.Row(e.Column.ColumnName)
              Return
            End If
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "C" 'cliente
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028347372027051, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Cliente/forn.'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return
          End If
          e.Row!et_lsconto = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
          e.Row!et_lsdest = "0"
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
            e.Row!et_lsind = NTSCStr(dttTmp.Rows(0)!an_indir)
            e.Row!et_lscap = NTSCStr(dttTmp.Rows(0)!an_cap)
            e.Row!et_lscitt = NTSCStr(dttTmp.Rows(0)!an_citta)
            e.Row!et_lsprov = NTSCStr(dttTmp.Rows(0)!an_prov)
            e.Row!et_lsstat = NTSCStr(dttTmp.Rows(0)!an_stato)
            Return
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "X" 'cliente dest 1
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028348661087880, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return
          End If
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028348684829216, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Destinaz.")))
            Return
          End If
          e.Row!et_lsconto = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
          e.Row!et_lsdest = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest)
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest), strDittaCorrente, "DESTDIV", "N", , dttTmp, NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)) Then
            e.Row!et_lsind = NTSCStr(dttTmp.Rows(0)!dd_inddest)
            e.Row!et_lscap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
            e.Row!et_lscitt = NTSCStr(dttTmp.Rows(0)!dd_locdest)
            e.Row!et_lsprov = NTSCStr(dttTmp.Rows(0)!dd_prodest)
            e.Row!et_lsstat = NTSCStr(dttTmp.Rows(0)!dd_stato)
            Return
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "Y" 'cliente dest 2
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028349228561909, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Cliente/forn. (dest. 2)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return
          End If
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2) = 0 Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028378124982224, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Cliente/forn. (dest. 2)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Destin. diversa 2")))
            Return
          End If
          e.Row!et_lsconto = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
          e.Row!et_lsdest = NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2)
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2), strDittaCorrente, "DESTDIV", "N", , dttTmp, NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)) Then
            e.Row!et_lsind = NTSCStr(dttTmp.Rows(0)!dd_inddest)
            e.Row!et_lscap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
            e.Row!et_lscitt = NTSCStr(dttTmp.Rows(0)!dd_locdest)
            e.Row!et_lsprov = NTSCStr(dttTmp.Rows(0)!dd_prodest)
            e.Row!et_lsstat = NTSCStr(dttTmp.Rows(0)!dd_stato)
            Return
          Else
            e.ProposedValue = e.Row(e.Column.ColumnName)
            Return
          End If
        Case "M" 'manuale
          e.Row!et_lsconto = "0"
          e.Row!et_lsdest = "0"
          e.Row!et_lsind = ""
          e.Row!et_lscap = ""
          e.Row!et_lscitt = ""
          e.Row!et_lsprov = ""
          e.Row!et_lsstat = ""
          Return
      End Select

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

  Public Overridable Sub BeforeColUpdate_SCHETRASP_et_commstat(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) <> "" Then
        If Not oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993998152797116, "Cod. stato inesistente.")))
          Return
        Else
          e.Row!xx_commstat = strTmp
        End If
      Else
        e.Row!xx_commstat = ""
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
  End Sub
  Public Overridable Sub BeforeColUpdate_SCHETRASP_et_caristat(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) <> "" Then
        If Not oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993997999827387, "Cod. stato inesistente.")))
          Return
        Else
          e.Row!xx_caristat = strTmp
        End If
      Else
        e.Row!xx_caristat = ""
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
  End Sub
  Public Overridable Sub BeforeColUpdate_SCHETRASP_et_propstat(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) <> "" Then
        If Not oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993998040296396, "Cod. stato inesistente.")))
          Return
        Else
          e.Row!xx_propstat = strTmp
        End If
      Else
        e.Row!xx_propstat = ""
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
  End Sub
  Public Overridable Sub BeforeColUpdate_SCHETRASP_et_lcstat(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) <> "" Then
        If Not oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993998074359114, "Cod. stato inesistente.")))
          Return
        Else
          e.Row!xx_lcstat = strTmp
        End If
      Else
        e.Row!xx_lcstat = ""
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
  End Sub
  Public Overridable Sub BeforeColUpdate_SCHETRASP_et_lsstat(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) <> "" Then
        If Not oCldDocu.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993998110296844, "Cod. stato inesistente.")))
          Return
        Else
          e.Row!xx_lsstat = strTmp
        End If
      Else
        e.Row!xx_lsstat = ""
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
  End Sub

  Public Overridable Function SctrTestPreSalva() As Boolean
    'Eseguo i controlli necessari prima di fare il salvataggio della riga
    Dim dtrTmp As DataRow() = dsSctrShared.Tables("SCHETRASP").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
    Dim i As Integer = 0
    Try
      For i = 0 To dtrTmp.Length - 1
        If Not CheckCampiSchetrasp(dtrTmp(i), dsSctrTestata.Tables("TESTA").Rows(0)) Then Return False
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
      Return False
    End Try
  End Function

  Public Overridable Function RiempicolonneUnbound(ByRef ds As DataSet) As Boolean
    Dim i As Integer = 0
    Dim strDatscheda As String = ""
    Dim strTipork As String = ""
    Dim strSerie As String = ""
    Try
      If BORDINI = False Then
        strDatscheda = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_dtiniz)
      Else
        strDatscheda = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_datcons)
      End If

      Select Case NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_tipork)
        Case "A" : strTipork = "Fattura Imm. emessa"
        Case "B" : strTipork = "DDT emesso"
        Case "C" : strTipork = "Corrispettivo emesso"
        Case "E" : strTipork = "Nota di Addeb. emessa"
        Case "F" : strTipork = "Ric.Fiscale Emessa"
        Case "I" : strTipork = "Riemissione Ric.Fiscali"
        Case "J" : strTipork = "Nota Accr. ricevuta"
        Case "L" : strTipork = "Fattura Imm. ricevuta"
        Case "M" : strTipork = "DDT ricevuto"
        Case "N" : strTipork = "Nota Accr. emessa"
        Case "S" : strTipork = "Fatt.Ric.Fisc. Emessa"
        Case "T" : strTipork = "Carico da produzione"
        Case "W" : strTipork = "Nota di prelievo"
        Case "Z" : strTipork = "Bolla di mov. interna"
        Case "R" : strTipork = "Impegno cliente"
        Case "O" : strTipork = "Ordine fornitore"
        Case "H" : strTipork = "Ordine di produzione"
        Case "X" : strTipork = "Impegno Trasferimento"
        Case "Q" : strTipork = "Preventivo"
        Case "#" : strTipork = "Impegno di commessa"
        Case "V" : strTipork = "Impegno cliente aperto"
        Case "$" : strTipork = "Ordine fornitore aperto"
        Case Else : strTipork = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_tipork)
      End Select

      For i = 0 To ds.Tables("SCHETRASP").Rows.Count - 1
        strSerie = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_serie)
        ds.Tables("SCHETRASP").Rows(i)!xx_riferim = strTipork & " n° " & NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_numdoc) & NTSCStr(IIf(strSerie = "" Or strSerie = " ", "", "/" & strSerie)) & " del " & NTSCDate(dsSctrTestata.Tables("TESTA").Rows(0)!et_datdoc).ToShortDateString
        ds.Tables("SCHETRASP").Rows(i)!xx_datcomp = strDatscheda
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
      Return False
    End Try
  End Function

  Public Overridable Function CheckCampiSchetrasp(ByVal dtrTmp As DataRow, ByVal dtrTmpTesta As DataRow) As Boolean
    Try
      'test coerenza con documento
      'vettore
      If NTSCStr(dtrTmp!et_vetttipo) = "1" And NTSCInt(dtrTmpTesta!et_vettor) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028582681937910, "Attenzione! Nella scheda di trasporto il 'Vettore 1'" & vbCrLf & _
        "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_vetttipo) = "2" And NTSCInt(dtrTmpTesta!et_vettor2) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028582696783940, "Attenzione! Nella scheda di trasporto il 'Vettore 2'" & vbCrLf & _
        "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_vetttipo) = "1" And NTSCInt(dtrTmpTesta!et_vettor) <> 0 And NTSCInt(dtrTmpTesta!et_vettor) <> NTSCInt(dtrTmp!et_vettcod) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129042407275449457, "Attenzione! Nella scheda di trasporto il 'Vettore 1'" & vbCrLf & _
        "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_vetttipo) = "2" And NTSCInt(dtrTmpTesta!et_vettor2) <> 0 And NTSCInt(dtrTmpTesta!et_vettor2) <> NTSCInt(dtrTmp!et_vettcod) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129042407294028808, "Attenzione! Nella scheda di trasporto il 'Vettore 2'" & vbCrLf & _
        "non è coerente con il campo indicato nel documento")))
        Return False
      End If

      'committente
      If NTSCStr(dtrTmp!et_commtipo) = "C" And (NTSCInt(dtrTmp!et_commcod) <> 0 And NTSCInt(dtrTmp!et_commdest) = 0) _
        And NTSCInt(dtrTmp!et_commcod) <> NTSCInt(dtrTmpTesta!et_conto) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028540504548232, "Attenzione! Nella scheda di trasporto il 'Cliente/forn.' del committente" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_commtipo) = "X" And (NTSCInt(dtrTmp!et_commcod) <> 0 And NTSCInt(dtrTmp!et_commdest) <> 0) _
        And (NTSCInt(dtrTmp!et_commcod) <> NTSCInt(dtrTmpTesta!et_conto) _
        Or NTSCInt(dtrTmp!et_commdest) <> NTSCInt(dtrTmpTesta!et_coddest)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028540520798232, "Attenzione! Nella scheda di trasporto il 'Cliente/forn. (dest. 1)' del committente" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_commtipo) = "Y" And (NTSCInt(dtrTmp!et_commcod) <> 0 And NTSCInt(dtrTmp!et_commdest) <> 0) _
        And (NTSCInt(dtrTmp!et_commcod) <> NTSCInt(dtrTmpTesta!et_conto) _
        Or NTSCInt(dtrTmp!et_commdest) <> NTSCInt(dtrTmpTesta!et_coddest2)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028540537360732, "Attenzione! Nella scheda di trasporto il 'Cliente/forn. (dest. 2)' del committente" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_commtipo) = "F" And (NTSCInt(dtrTmp!et_commcod) <> 0 And NTSCInt(dtrTmp!et_commdest) = 0) _
        And NTSCInt(dtrTmp!et_commcod) <> NTSCInt(dtrTmpTesta!et_contfatt) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028540555954482, "Attenzione! Nella scheda di trasporto il 'Conto fatturazione' del committente" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      'caricatore
      If NTSCStr(dtrTmp!et_caritipo) = "C" And (NTSCInt(dtrTmp!et_caricod) <> 0 And NTSCInt(dtrTmp!et_caridest) = 0) _
        And NTSCInt(dtrTmp!et_caricod) <> NTSCInt(dtrTmpTesta!et_conto) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028545869079482, "Attenzione! Nella scheda di trasporto il 'Cliente/forn.' del caricatore" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_caritipo) = "X" And (NTSCInt(dtrTmp!et_caricod) <> 0 And NTSCInt(dtrTmp!et_caridest) <> 0) _
        And (NTSCInt(dtrTmp!et_caricod) <> NTSCInt(dtrTmpTesta!et_conto) _
        Or NTSCInt(dtrTmp!et_caridest) <> NTSCInt(dtrTmpTesta!et_coddest)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028545885954482, "Attenzione! Nella scheda di trasporto il 'Cliente/forn. (dest. 1)' del caricatore" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_caritipo) = "Y" And (NTSCInt(dtrTmp!et_caricod) <> 0 And NTSCInt(dtrTmp!et_caridest) <> 0) _
        And (NTSCInt(dtrTmp!et_caricod) <> NTSCInt(dtrTmpTesta!et_conto) _
        Or NTSCInt(dtrTmp!et_caridest) <> NTSCInt(dtrTmpTesta!et_coddest2)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028545902985732, "Attenzione! Nella scheda di trasporto il 'Cliente/forn. (dest. 2)' del caricatore" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      'non testa la modifica del conto della tabella vettori/spedizionieri gia bloccato in apertura
      If NTSCStr(dtrTmp!et_caritipo) = "1" And (NTSCInt(dtrTmp!et_caricod) <> 0 And NTSCInt(dtrTmp!et_caridest) = 0) _
        And NTSCInt(dtrTmpTesta!et_vettor) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028545925485732, "Attenzione! Nella scheda di trasporto il 'Vettore 1' del caricatore" & vbCrLf & _
         "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_caritipo) = "2" And (NTSCInt(dtrTmp!et_caricod) <> 0 And NTSCInt(dtrTmp!et_caridest) = 0) _
        And NTSCInt(dtrTmpTesta!et_vettor2) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028545942829482, "Attenzione! Nella scheda di trasporto il 'Vettore 2' del caricatore" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      'proprietario
      If NTSCStr(dtrTmp!et_proptipo) = "C" And (NTSCInt(dtrTmp!et_propcod) <> 0 And NTSCInt(dtrTmp!et_propdest) = 0) _
        And NTSCInt(dtrTmp!et_propcod) <> NTSCInt(dtrTmpTesta!et_conto) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028554569079482, "Attenzione! Nella scheda di trasporto il 'Cliente/forn.' del proprietario" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_proptipo) = "X" And (NTSCInt(dtrTmp!et_propcod) <> 0 And NTSCInt(dtrTmp!et_propdest) <> 0) _
        And (NTSCInt(dtrTmp!et_propcod) <> NTSCInt(dtrTmpTesta!et_conto) _
        Or NTSCInt(dtrTmp!et_propdest) <> NTSCInt(dtrTmpTesta!et_coddest)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028554588610732, "Attenzione! Nella scheda di trasporto il 'Cliente/forn. (dest. 1)' del proprietario" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_proptipo) = "Y" And (NTSCInt(dtrTmp!et_propcod) <> 0 And NTSCInt(dtrTmp!et_propdest) <> 0) _
        And (NTSCInt(dtrTmp!et_propcod) <> NTSCInt(dtrTmpTesta!et_conto) _
        Or NTSCInt(dtrTmp!et_propdest) <> NTSCInt(dtrTmpTesta!et_coddest2)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028554603298232, "Attenzione! Nella scheda di trasporto il 'Cliente/forn. (dest. 2)' del proprietario" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_proptipo) = "F" And (NTSCInt(dtrTmp!et_propcod) <> 0 And NTSCInt(dtrTmp!et_propdest) = 0) _
        And NTSCInt(dtrTmp!et_propcod) <> NTSCInt(dtrTmpTesta!et_contfatt) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028554618298232, "Attenzione! Nella scheda di trasporto il 'Conto fatturazione' del proprietario" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      'luogo carico
      'non testa il cambiamento nella tabella magazzini del Conto C/F e destinazione
      If NTSCStr(dtrTmp!et_lctipo) = "W" And (NTSCInt(dtrTmp!et_lcconto) <> 0 And NTSCInt(dtrTmp!et_lcdest) <> 0) _
        And NTSCInt(dtrTmpTesta!et_magaz) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028558423610732, "Attenzione! Nella scheda di trasporto la 'Ditta corrente (mag. 1 dest.)' del luogo carico" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lctipo) = "Z" And (NTSCInt(dtrTmp!et_lcconto) <> 0 And NTSCInt(dtrTmp!et_lcdest) <> 0) _
        And NTSCInt(dtrTmpTesta!et_magaz2) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028558443454482, "Attenzione! Nella scheda di trasporto la 'Ditta corrente (mag. 2 dest.)' del luogo carico" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lctipo) = "C" And (NTSCInt(dtrTmp!et_lcconto) <> 0 And NTSCInt(dtrTmp!et_lcdest) = 0) _
        And NTSCInt(dtrTmp!et_lcconto) <> NTSCInt(dtrTmpTesta!et_conto) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028558460329482, "Attenzione! Nella scheda di trasporto il 'Cliente/forn.' del luogo carico" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lctipo) = "X" And (NTSCInt(dtrTmp!et_lcconto) <> 0 And NTSCInt(dtrTmp!et_lcdest) <> 0) _
        And (NTSCInt(dtrTmp!et_lcconto) <> NTSCInt(dtrTmpTesta!et_conto) _
        Or NTSCInt(dtrTmp!et_lcdest) <> NTSCInt(dtrTmpTesta!et_coddest)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028558482673232, "Attenzione! Nella scheda di trasporto il 'Cliente/forn. (dest. 1)' del luogo carico" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lctipo) = "Y" And (NTSCInt(dtrTmp!et_lcconto) <> 0 And NTSCInt(dtrTmp!et_lcdest) <> 0) _
        And (NTSCInt(dtrTmp!et_lcconto) <> NTSCInt(dtrTmpTesta!et_conto) _
        Or NTSCInt(dtrTmp!et_lcdest) <> NTSCInt(dtrTmpTesta!et_coddest2)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028558500329482, "Attenzione! Nella scheda di trasporto il 'Cliente/forn. (dest. 2)' del luogo carico" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      'luogo scarico
      'non testa il cambiamento nella tabella magazzini del Conto C/F e destinazione
      If NTSCStr(dtrTmp!et_lstipo) = "W" And (NTSCInt(dtrTmp!et_lsconto) <> 0 And NTSCInt(dtrTmp!et_lsdest) <> 0) _
        And NTSCInt(dtrTmpTesta!et_magaz) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028566828298232, "Attenzione! Nella scheda di trasporto la 'Ditta corrente (mag. 1 dest.)' del luogo scarico" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lstipo) = "Z" And (NTSCInt(dtrTmp!et_lsconto) <> 0 And NTSCInt(dtrTmp!et_lsdest) <> 0) _
        And NTSCInt(dtrTmpTesta!et_magaz2) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028566845329482, "Attenzione! Nella scheda di trasporto la 'Ditta corrente (mag. 2 dest.)' del luogo scarico" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lstipo) = "C" And (NTSCInt(dtrTmp!et_lsconto) <> 0 And NTSCInt(dtrTmp!et_lsdest) = 0) _
        And NTSCInt(dtrTmp!et_lsconto) <> NTSCInt(dtrTmpTesta!et_conto) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028566865485732, "Attenzione! Nella scheda di trasporto il 'Cliente/forn.' del luogo scarico" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lstipo) = "X" And (NTSCInt(dtrTmp!et_lsconto) <> 0 And NTSCInt(dtrTmp!et_lsdest) <> 0) _
        And (NTSCInt(dtrTmp!et_lsconto) <> NTSCInt(dtrTmpTesta!et_conto) _
        Or NTSCInt(dtrTmp!et_lsdest) <> NTSCInt(dtrTmpTesta!et_coddest)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028566879704482, "Attenzione! Nella scheda di trasporto il 'Cliente/forn. (dest. 1)' del luogo scarico" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lstipo) = "Y" And (NTSCInt(dtrTmp!et_lsconto) <> 0 And NTSCInt(dtrTmp!et_lsdest) <> 0) _
        And (NTSCInt(dtrTmp!et_lsconto) <> NTSCInt(dtrTmpTesta!et_conto) _
        Or NTSCInt(dtrTmp!et_lsdest) <> NTSCInt(dtrTmpTesta!et_coddest2)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028566896579482, "Attenzione! Nella scheda di trasporto il 'Cliente/forn. (dest. 2)' del luogo scarico" & vbCrLf & _
          "non è coerente con il campo indicato nel documento")))
        Return False
      End If

      'vettore1/2 non coerente con il caricatore
      If NTSCStr(dtrTmp!et_vetttipo) = "1" And NTSCStr(dtrTmp!et_caritipo) = "2" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028575166194944, "Attenzione! Non è possibile, come caricatore, scegliere 'Vettore 2'" & vbCrLf & _
          "quando sulla scheda di trasporto il vettore indicato è il vettore 1")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_vetttipo) = "2" And NTSCStr(dtrTmp!et_caritipo) = "1" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028575181666466, "Attenzione! Non è possibile, come caricatore, scegliere 'Vettore 1'" & vbCrLf & _
          "quando sulla scheda di trasporto il vettore indicato è il vettore 2")))
        Return False
      End If

      'quantità e peso
      If bArtConai = False Then
        If NTSCDec(dtrTmp!et_quant) <> NTSCDec(dtrTmpTesta!xx_totquant) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028570609753657, "Attenzione! Nella scheda di trasporto il 'Quantità'" & vbCrLf & _
            "non è coerente con il campo indicato nel documento")))
          'Return False 'non bloccante
        End If
        If NTSCDec(dtrTmp!et_peso) <> NTSCDec(dtrTmpTesta!et_peso) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028570624915630, "Attenzione! Nella scheda di trasporto il 'Peso'" & vbCrLf & _
            "non è coerente con il campo indicato nel documento")))
          'Return False 'non bloccante
        End If
      End If

      'test validità
      'committente
      If NTSCStr(dtrTmp!et_commtipo) = "D" And Not (NTSCInt(dtrTmp!et_commcod) = 0 And NTSCInt(dtrTmp!et_commdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028364248630472, "Attenzione! Non è possibile, come committente, scegliere 'Ditta corrente'" & vbCrLf & _
          "quando sulla scheda di trasporto Conto e destinazione sono valorizzati")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_commtipo) = "C" And Not (NTSCInt(dtrTmp!et_commcod) <> 0 And NTSCInt(dtrTmp!et_commdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028355125890268, "Attenzione! Non è possibile, come committente, scegliere 'Cliente/forn.'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_commtipo) = "X" And Not (NTSCInt(dtrTmp!et_commcod) <> 0 And NTSCInt(dtrTmp!et_commdest) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028355107300088, "Attenzione! Non è possibile, come committente, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_commtipo) = "Y" And Not (NTSCInt(dtrTmp!et_commcod) <> 0 And NTSCInt(dtrTmp!et_commdest) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028378210123759, "Attenzione! Non è possibile, come committente, scegliere 'Cliente/forn. (dest. 2)'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_commtipo) = "F" And Not (NTSCInt(dtrTmp!et_commcod) <> 0 And NTSCInt(dtrTmp!et_commdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028355860280488, "Attenzione! Non è possibile, come committente, scegliere 'Conto fatturazione'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_commtipo) = "M" And Not (NTSCInt(dtrTmp!et_commcod) = 0 And NTSCInt(dtrTmp!et_commdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028364865086430, "Attenzione! Non è possibile, come committente, scegliere '(Altro manuale)'" & vbCrLf & _
          "quando sulla scheda di trasporto Conto e destinazione sono valorizzati")))
        Return False
      End If
      'caricatore
      If NTSCStr(dtrTmp!et_caritipo) = "D" And Not (NTSCInt(dtrTmp!et_caricod) = 0 And NTSCInt(dtrTmp!et_caridest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028364956476885, "Attenzione! Non è possibile, come caricatore, scegliere 'Ditta corrente'" & vbCrLf & _
          "quando sulla scheda di trasporto Conto e destinazione sono valorizzati")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_caritipo) = "C" And Not (NTSCInt(dtrTmp!et_caricod) <> 0 And NTSCInt(dtrTmp!et_caridest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028357178321768, "Attenzione! Non è possibile, come caricatore, scegliere 'Cliente/forn.'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_caritipo) = "X" And Not (NTSCInt(dtrTmp!et_caricod) <> 0 And NTSCInt(dtrTmp!et_caridest) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028357611212930, "Attenzione! Non è possibile, come caricatore, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_caritipo) = "Y" And Not (NTSCInt(dtrTmp!et_caricod) <> 0 And NTSCInt(dtrTmp!et_caridest) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028358569166234, "Attenzione! Non è possibile, come caricatore, scegliere 'Cliente/forn. (dest. 2)'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_caritipo) = "1" And Not (NTSCInt(dtrTmp!et_caricod) <> 0 And NTSCInt(dtrTmp!et_caridest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028358526673850, "Attenzione! Non è possibile, come caricatore, scegliere 'Vettore 1'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_caritipo) = "2" And Not (NTSCInt(dtrTmp!et_caricod) <> 0 And NTSCInt(dtrTmp!et_caridest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028358546670266, "Attenzione! Non è possibile, come caricatore, scegliere 'Vettore 2'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_caritipo) = "M" And Not (NTSCInt(dtrTmp!et_caricod) = 0 And NTSCInt(dtrTmp!et_caridest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028365193310953, "Attenzione! Non è possibile, come caricatore, scegliere '(Altro manuale)'" & vbCrLf & _
          "quando sulla scheda di trasporto Conto e destinazione sono valorizzati")))
        Return False
      End If
      'proprietario
      If NTSCStr(dtrTmp!et_proptipo) = "D" And Not (NTSCInt(dtrTmp!et_propcod) = 0 And NTSCInt(dtrTmp!et_propdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028378376657477, "Attenzione! Non è possibile, come proprietario, scegliere 'Ditta corrente'" & vbCrLf & _
          "quando sulla scheda di trasporto Conto e destinazione sono valorizzati")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_proptipo) = "C" And Not (NTSCInt(dtrTmp!et_propcod) <> 0 And NTSCInt(dtrTmp!et_propdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028360142009330, "Attenzione! Non è possibile, come proprietario, scegliere 'Cliente/forn.'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_proptipo) = "X" And Not (NTSCInt(dtrTmp!et_propcod) <> 0 And NTSCInt(dtrTmp!et_propdest) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028361758282142, "Attenzione! Non è possibile, come proprietario, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_proptipo) = "Y" And Not (NTSCInt(dtrTmp!et_propcod) <> 0 And NTSCInt(dtrTmp!et_propdest) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028361743128608, "Attenzione! Non è possibile, come proprietario, scegliere 'Cliente/forn. (dest. 2)'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_proptipo) = "F" And Not (NTSCInt(dtrTmp!et_propcod) <> 0 And NTSCInt(dtrTmp!et_propdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028362064789706, "Attenzione! Non è possibile, come proprietario, scegliere 'Conto fatturazione'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_proptipo) = "M" And Not (NTSCInt(dtrTmp!et_propcod) = 0 And NTSCInt(dtrTmp!et_propdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028378313543385, "Attenzione! Non è possibile, come proprietario, scegliere '(Altro manuale)'" & vbCrLf & _
          "quando sulla scheda di trasporto Conto e destinazione sono valorizzati")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_proptipo) = "N" And Not (NTSCInt(dtrTmp!et_propcod) = 0 And NTSCInt(dtrTmp!et_propdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028365708846853, "Attenzione! Non è possibile, come proprietario, scegliere '(Non definito)'" & vbCrLf & _
          "quando sulla scheda di trasporto Conto e destinazione sono valorizzati")))
        Return False
      End If
      'luogo carico
      If NTSCStr(dtrTmp!et_lctipo) = "D" And Not (NTSCInt(dtrTmp!et_lcconto) = 0 And NTSCInt(dtrTmp!et_lcdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028370069655675, "Attenzione! Non è possibile, come luogo carico, scegliere 'Ditta corrente'" & vbCrLf & _
          "quando sulla scheda di trasporto Conto e destinazione sono valorizzati")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lctipo) = "W" And Not (NTSCInt(dtrTmp!et_lcconto) <> 0 And NTSCInt(dtrTmp!et_lcdest) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028370088714881, "Attenzione! Non è possibile, come luogo carico, scegliere 'Ditta corrente (mag. 1 dest.)'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lctipo) = "Z" And Not (NTSCInt(dtrTmp!et_lcconto) <> 0 And NTSCInt(dtrTmp!et_lcdest) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028370106680526, "Attenzione! Non è possibile, come luogo carico, scegliere 'Ditta corrente (mag. 2 dest.)'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lctipo) = "C" And Not (NTSCInt(dtrTmp!et_lcconto) <> 0 And NTSCInt(dtrTmp!et_lcdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028378469922608, "Attenzione! Non è possibile, come luogo carico, scegliere 'Cliente/forn.'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lctipo) = "X" And Not (NTSCInt(dtrTmp!et_lcconto) <> 0 And NTSCInt(dtrTmp!et_lcdest) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028371317564999, "Attenzione! Non è possibile, come luogo carico, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lctipo) = "Y" And Not (NTSCInt(dtrTmp!et_lcconto) <> 0 And NTSCInt(dtrTmp!et_lcdest) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028371337405320, "Attenzione! Non è possibile, come luogo carico, scegliere 'Cliente/forn. (dest. 2)'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lctipo) = "M" And Not (NTSCInt(dtrTmp!et_lcconto) = 0 And NTSCInt(dtrTmp!et_lcdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028371355214742, "Attenzione! Non è possibile, come luogo carico, scegliere '(Altro manuale)'" & vbCrLf & _
          "quando sulla scheda di trasporto Conto e destinazione sono valorizzati")))
        Return False
      End If
      'luogo scarico
      If NTSCStr(dtrTmp!et_lstipo) = "D" And Not (NTSCInt(dtrTmp!et_lsconto) = 0 And NTSCInt(dtrTmp!et_lsdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028373968356863, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Ditta corrente'" & vbCrLf & _
          "quando sulla scheda di trasporto Conto e destinazione sono valorizzati")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lstipo) = "W" And Not (NTSCInt(dtrTmp!et_lsconto) <> 0 And NTSCInt(dtrTmp!et_lsdest) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028373983979163, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Ditta corrente (mag. 1 dest.)'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lstipo) = "Z" And Not (NTSCInt(dtrTmp!et_lsconto) <> 0 And NTSCInt(dtrTmp!et_lsdest) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028374008193728, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Ditta corrente (mag. 2 dest.)'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lstipo) = "C" And Not (NTSCInt(dtrTmp!et_lsconto) <> 0 And NTSCInt(dtrTmp!et_lsdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028374021628906, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Cliente/forn.'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lstipo) = "X" And Not (NTSCInt(dtrTmp!et_lsconto) <> 0 And NTSCInt(dtrTmp!et_lsdest) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028374036001422, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lstipo) = "Y" And Not (NTSCInt(dtrTmp!et_lsconto) <> 0 And NTSCInt(dtrTmp!et_lsdest) <> 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028374061934440, "Attenzione! Non è possibile, come luogo scarico, scegliere 'Cliente/forn. (dest. 2)'" & vbCrLf & _
          "quando sul documento non è valorizzato tale campo")))
        Return False
      End If
      If NTSCStr(dtrTmp!et_lstipo) = "M" And Not (NTSCInt(dtrTmp!et_lsconto) = 0 And NTSCInt(dtrTmp!et_lsdest) = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129028374079431416, "Attenzione! Non è possibile, come luogo scarico, scegliere '(Altro manuale)'" & vbCrLf & _
          "quando sulla scheda di trasporto Conto e destinazione sono valorizzati")))
        Return False
      End If

      'test obbligatorietà
      If Trim(NTSCStr(dtrTmp!et_commrs1)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993842947691107, "Attenzione! Ragione sociale 1 obbligatoria nel committente.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_commind)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993845443422828, "Attenzione! Indirizzo obbligatorio nel committente.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_commcitt)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993845464479723, "Attenzione! Città obbligatoria nel committente.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_commpi)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993845476957883, "Attenzione! Partita iva obbligatoria nel committente.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_commtel)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993845492711560, "Attenzione! Telefono obbligatorio nel committente.")))
        Return False
      End If

      If Trim(NTSCStr(dtrTmp!et_carirs1)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993845510648915, "Attenzione! Ragione sociale 1 obbligatoria nel caricatore.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_cariind)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993845539660637, "Attenzione! Indirizzo obbligatorio nel caricatore.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_caricitt)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993845556974084, "Attenzione! Città obbligatoria nel caricatore.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_caripi)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993845574599485, "Attenzione! Partita iva obbligatoria nel caricatore.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_caritel)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993845595968334, "Attenzione! Telefono obbligatorio nel caricatore.")))
        Return False
      End If

      If Trim(NTSCStr(dtrTmp!et_proprs1)) = "" And Not (NTSCStr(dtrTmp!et_proptipo) = "N") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993846484881257, "Attenzione! Ragione sociale 1 obbligatoria nel proprietario.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_propind)) = "" And Not (NTSCStr(dtrTmp!et_proptipo) = "N") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993846500011026, "Attenzione! Indirizzo obbligatorio nel proprietario.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_propcitt)) = "" And Not (NTSCStr(dtrTmp!et_proptipo) = "N") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993846516232634, "Attenzione! Città obbligatoria nel proprietario.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_proppi)) = "" And Not (NTSCStr(dtrTmp!et_proptipo) = "N") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993846531674357, "Attenzione! Partita iva obbligatoria nel proprietario.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_proptel)) = "" And Not (NTSCStr(dtrTmp!et_proptipo) = "N") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993846548051942, "Attenzione! Telefono obbligatorio nel proprietario.")))
        Return False
      End If

      If Trim(NTSCStr(dtrTmp!et_evendich)) = "" And (NTSCStr(dtrTmp!et_proptipo) = "N") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993941155778971, "Attenzione! Eventuali dichiarazioni obbligatorie nel proprietario.")))
        Return False
      End If

      If Trim(NTSCStr(dtrTmp!et_tipmerce)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993941176404367, "Attenzione! Tipologia merce obbligatoria.")))
        Return False
      End If

      If Trim(NTSCStr(dtrTmp!et_lcind)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993942799091772, "Attenzione! Indirizzo obbligatorio nel luogo carico.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_lccitt)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993942816279602, "Attenzione! Città obbligatoria nel luogo carico.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_lsind)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993942830811131, "Attenzione! Indirizzo obbligatorio nel luogo scarico.")))
        Return False
      End If
      If Trim(NTSCStr(dtrTmp!et_lscitt)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993942850655262, "Attenzione! Città obbligatoria nel luogo scarico.")))
        Return False
      End If

      If Trim(NTSCStr(dtrTmp!et_compcitt)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993941193435944, "Attenzione! Città di compilazione obbligatoria.")))
        Return False
      End If

      If Trim(NTSCStr(dtrTmp!et_compilat)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128993941210311268, "Attenzione! Nome cognome compilatore obbligatoria.")))
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

  Public Overridable Function SctrProponiValidaCaricatore(ByVal e As DataColumnChangeEventArgs, ByRef dttSchetrasp As DataTable) As Boolean
    'solo uno dei 2 parametri deve essere valorizzato e, dttSchetrasp
    'se tutti e due Nothing non fa nulla
    'se tutti e due valorizzati valida
    Dim dttTmp As New DataTable
    Dim dttTmp2 As New DataTable
    Dim strCaricatore As String = ""
    Dim bProponi As Boolean 'bProponi True quando propone in nuovo, False quando valida.
    Dim strCaricod As String = ""
    Dim strCaridest As String = ""
    Dim strCarirs1 As String = ""
    Dim strCarirs2 As String = ""
    Dim strCariind As String = ""
    Dim strCaricap As String = ""
    Dim strCaricitt As String = ""
    Dim strCariprov As String = ""
    Dim strCaristat As String = ""
    Dim strCaricf As String = ""
    Dim strCaripi As String = ""
    Dim strCaritel As String = ""
    Dim strCarimail As String = ""
    Dim strMsgInProposta As String = ""
    Try
      If e Is Nothing And dttSchetrasp Is Nothing Then 'se tutti e due i parametri non fa nulla
        Return False
      End If
      strMsgInProposta = oApp.Tr(Me, 130129977933406882, "Attenzione! C'è un incoerenza tra il valore del OPZIONE ScheTraspPropostaCaricatore" & vbCrLf & _
         "e i dati indicati nel documento. Opzione disattivata." & vbCrLf)
      If e Is Nothing Then
        bProponi = True
      End If

      If bProponi Then
        strCaricatore = NTSCStr(dttSchetrasp.Columns("et_caritipo").DefaultValue)
      Else
        strCaricatore = e.ProposedValue.ToString
      End If

      Select Case strCaricatore
        Case "D" 'ditta
          strCaricod = "0"
          strCaridest = "0"
          If oCldDocu.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", , dttTmp) Then
            strCarirs1 = NTSCStr(dttTmp.Rows(0)!tb_azrags1)
            strCarirs2 = NTSCStr(dttTmp.Rows(0)!tb_azrags2)
            strCariind = NTSCStr(dttTmp.Rows(0)!tb_azindir)
            strCaricap = NTSCStr(dttTmp.Rows(0)!tb_azcap)
            strCaricitt = NTSCStr(dttTmp.Rows(0)!tb_azcitta)
            strCariprov = NTSCStr(dttTmp.Rows(0)!tb_azprov)
            strCaristat = NTSCStr(dttTmp.Rows(0)!tb_azstato)
            strCaricf = NTSCStr(dttTmp.Rows(0)!tb_azcodf)
            strCaripi = NTSCStr(dttTmp.Rows(0)!tb_azpiva)
            strCaritel = NTSCStr(dttTmp.Rows(0)!tb_aztelef)
            strCarimail = NTSCStr(dttTmp.Rows(0)!tb_azemail)
          Else
            Return False
          End If
        Case "C" 'cliente
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", NTSCStr(IIf(bProponi, strMsgInProposta, "")) & _
              oApp.Tr(Me, 129028325933959609, "Attenzione! Non è possibile, come caricatore, scegliere 'Cliente/forn.'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return False
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
            strCaricod = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
            strCaridest = "0"
            strCarirs1 = NTSCStr(dttTmp.Rows(0)!an_descr1)
            strCarirs2 = NTSCStr(dttTmp.Rows(0)!an_descr2)
            strCariind = NTSCStr(dttTmp.Rows(0)!an_indir)
            strCaricap = NTSCStr(dttTmp.Rows(0)!an_cap)
            strCaricitt = NTSCStr(dttTmp.Rows(0)!an_citta)
            strCariprov = NTSCStr(dttTmp.Rows(0)!an_prov)
            strCaristat = NTSCStr(dttTmp.Rows(0)!an_stato)
            strCaricf = NTSCStr(dttTmp.Rows(0)!an_codfis)
            strCaripi = NTSCStr(dttTmp.Rows(0)!an_pariva)
            strCaritel = NTSCStr(dttTmp.Rows(0)!an_telef)
            strCarimail = NTSCStr(dttTmp.Rows(0)!an_email)
          Else
            Return False
          End If
        Case "X" 'cliente dest 1
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", NTSCStr(IIf(bProponi, strMsgInProposta, "")) & _
            oApp.Tr(Me, 129028326166684353, "Attenzione! Non è possibile, come caricatore, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return False
          End If
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", NTSCStr(IIf(bProponi, strMsgInProposta, "")) & _
            oApp.Tr(Me, 129028326185439873, "Attenzione! Non è possibile, come caricatore, scegliere 'Cliente/forn. (dest. 1)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Destinaz.")))
            Return False
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest), strDittaCorrente, "DESTDIV", "N", , dttTmp, NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)) Then
            strCaricod = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
            strCaridest = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest)
            strCarirs1 = NTSCStr(dttTmp.Rows(0)!dd_nomdest)
            strCarirs2 = NTSCStr(dttTmp.Rows(0)!dd_nomdest2)
            strCariind = NTSCStr(dttTmp.Rows(0)!dd_inddest)
            strCaricap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
            strCaricitt = NTSCStr(dttTmp.Rows(0)!dd_locdest)
            strCariprov = NTSCStr(dttTmp.Rows(0)!dd_prodest)
            strCaristat = NTSCStr(dttTmp.Rows(0)!dd_stato)
            strCaricf = NTSCStr(dttTmp.Rows(0)!dd_codfis)
            strCaripi = NTSCStr(dttTmp.Rows(0)!dd_pariva)
            strCaritel = NTSCStr(dttTmp.Rows(0)!dd_telef)
            strCarimail = NTSCStr(dttTmp.Rows(0)!dd_email)
          Else
            Return False
          End If
        Case "Y" 'cliente dest 2
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", NTSCStr(IIf(bProponi, strMsgInProposta, "")) & _
            oApp.Tr(Me, 129028326767329881, "Attenzione! Non è possibile, come caricatore, scegliere 'Cliente/forn. (dest. 2)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Cli./Forn.")))
            Return False
          End If
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", NTSCStr(IIf(bProponi, strMsgInProposta, "")) & _
              oApp.Tr(Me, 129028326790305393, "Attenzione! Non è possibile, come caricatore, scegliere 'Cliente/forn. (dest. 2)'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Destin. diversa 2")))
            Return False
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2), strDittaCorrente, "DESTDIV", "N", , dttTmp, NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)) Then
            strCaricod = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_conto)
            strCaridest = NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_coddest2)
            strCarirs1 = NTSCStr(dttTmp.Rows(0)!dd_nomdest)
            strCarirs2 = NTSCStr(dttTmp.Rows(0)!dd_nomdest2)
            strCariind = NTSCStr(dttTmp.Rows(0)!dd_inddest)
            strCaricap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
            strCaricitt = NTSCStr(dttTmp.Rows(0)!dd_locdest)
            strCariprov = NTSCStr(dttTmp.Rows(0)!dd_prodest)
            strCaristat = NTSCStr(dttTmp.Rows(0)!dd_stato)
            strCaricf = NTSCStr(dttTmp.Rows(0)!dd_codfis)
            strCaripi = NTSCStr(dttTmp.Rows(0)!dd_pariva)
            strCaritel = NTSCStr(dttTmp.Rows(0)!dd_telef)
            strCarimail = NTSCStr(dttTmp.Rows(0)!dd_email)
          Else
            Return False
          End If
        Case "1" 'vettore 1
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", NTSCStr(IIf(bProponi, strMsgInProposta, "")) & _
            oApp.Tr(Me, 129028328950432457, "Attenzione! Non è possibile, come caricatore, scegliere 'Vettore 1'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Vettore")))
            Return False
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor), strDittaCorrente, "TABVETT", "N", , dttTmp2) Then
            If NTSCInt(dttTmp2.Rows(0)!tb_codforn) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", NTSCStr(IIf(bProponi, strMsgInProposta, "")) & _
                oApp.Tr(Me, 129028377738486522, "Attenzione! Non è possibile, come caricatore, scegliere 'Vettore 1'" & vbCrLf & _
                "quando non è valorizzato il codice fornitore della tabella vettori/spedizionieri del Vettore indicato nel documento")))
              Return False
            End If
            If oCldDocu.ValCodiceDb(NTSCStr(dttTmp2.Rows(0)!tb_codforn), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
              strCaricod = NTSCStr(dttTmp2.Rows(0)!tb_codforn)
              strCaridest = "0"
              strCarirs1 = NTSCStr(dttTmp.Rows(0)!an_descr1)
              strCarirs2 = NTSCStr(dttTmp.Rows(0)!an_descr2)
              strCariind = NTSCStr(dttTmp.Rows(0)!an_indir)
              strCaricap = NTSCStr(dttTmp.Rows(0)!an_cap)
              strCaricitt = NTSCStr(dttTmp.Rows(0)!an_citta)
              strCariprov = NTSCStr(dttTmp.Rows(0)!an_prov)
              strCaristat = NTSCStr(dttTmp.Rows(0)!an_stato)
              strCaricf = NTSCStr(dttTmp.Rows(0)!an_codfis)
              strCaripi = NTSCStr(dttTmp.Rows(0)!an_pariva)
              strCaritel = NTSCStr(dttTmp.Rows(0)!an_telef)
              strCarimail = NTSCStr(dttTmp.Rows(0)!an_email)
            Else
              Return False
            End If
          Else
            Return False
          End If
        Case "2" 'vettore 2
          If NTSCInt(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor2) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", NTSCStr(IIf(bProponi, strMsgInProposta, "")) & _
              oApp.Tr(Me, 129028329429274377, "Attenzione! Non è possibile, come caricatore, scegliere 'Vettore 2'" & vbCrLf & _
              "quando sul documento non è valorizzato il campo Vettore 2")))
            Return False
          End If
          If oCldDocu.ValCodiceDb(NTSCStr(dsSctrTestata.Tables("TESTA").Rows(0)!et_vettor2), strDittaCorrente, "TABVETT", "N", , dttTmp2) Then
            If NTSCInt(dttTmp2.Rows(0)!tb_codforn) = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", NTSCStr(IIf(bProponi, strMsgInProposta, "")) & _
                oApp.Tr(Me, 129028331284630537, "Attenzione! Non è possibile, come caricatore, scegliere 'Vettore 2'" & vbCrLf & _
                "quando non è valorizzato il codice fornitore della tabella vettori/spedizionieri del Vettore 2 indicato nel documento")))
              Return False
            End If
            If oCldDocu.ValCodiceDb(NTSCStr(dttTmp2.Rows(0)!tb_codforn), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
              strCaricod = NTSCStr(dttTmp2.Rows(0)!tb_codforn)
              strCaridest = "0"
              strCarirs1 = NTSCStr(dttTmp.Rows(0)!an_descr1)
              strCarirs2 = NTSCStr(dttTmp.Rows(0)!an_descr2)
              strCariind = NTSCStr(dttTmp.Rows(0)!an_indir)
              strCaricap = NTSCStr(dttTmp.Rows(0)!an_cap)
              strCaricitt = NTSCStr(dttTmp.Rows(0)!an_citta)
              strCariprov = NTSCStr(dttTmp.Rows(0)!an_prov)
              strCaristat = NTSCStr(dttTmp.Rows(0)!an_stato)
              strCaricf = NTSCStr(dttTmp.Rows(0)!an_codfis)
              strCaripi = NTSCStr(dttTmp.Rows(0)!an_pariva)
              strCaritel = NTSCStr(dttTmp.Rows(0)!an_telef)
              strCarimail = NTSCStr(dttTmp.Rows(0)!an_email)
            Else
              Return False
            End If
          Else
            Return False
          End If
        Case "M" 'manuale
          strCaricod = "0"
          strCaridest = "0"
          strCarirs1 = ""
          strCarirs2 = ""
          strCariind = ""
          strCaricap = ""
          strCaricitt = ""
          strCariprov = ""
          strCaristat = ""
          strCaricf = ""
          strCaripi = ""
          strCaritel = ""
          strCarimail = ""
      End Select

      'il test obbligatorietà (Trim) viene fatto al salvataggio sui campi obbligatori da db
      If bProponi Then
        dttSchetrasp.Columns("et_caricod").DefaultValue = strCaricod
        dttSchetrasp.Columns("et_caridest").DefaultValue = strCaridest
        dttSchetrasp.Columns("et_carirs1").DefaultValue = strCarirs1
        dttSchetrasp.Columns("et_carirs2").DefaultValue = strCarirs2
        dttSchetrasp.Columns("et_cariind").DefaultValue = strCariind
        dttSchetrasp.Columns("et_caricap").DefaultValue = strCaricap
        dttSchetrasp.Columns("et_caricitt").DefaultValue = strCaricitt
        dttSchetrasp.Columns("et_cariprov").DefaultValue = strCariprov
        dttSchetrasp.Columns("et_caristat").DefaultValue = strCaristat
        dttSchetrasp.Columns("et_caricf").DefaultValue = strCaricf
        dttSchetrasp.Columns("et_caripi").DefaultValue = strCaripi
        dttSchetrasp.Columns("et_caritel").DefaultValue = strCaritel
        dttSchetrasp.Columns("et_carimail").DefaultValue = strCarimail
      Else
        e.Row!et_caricod = strCaricod
        e.Row!et_caridest = strCaridest
        e.Row!et_carirs1 = strCarirs1
        e.Row!et_carirs2 = strCarirs2
        e.Row!et_cariind = strCariind
        e.Row!et_caricap = strCaricap
        e.Row!et_caricitt = strCaricitt
        e.Row!et_cariprov = strCariprov
        e.Row!et_caristat = strCaristat
        e.Row!et_caricf = strCaricf
        e.Row!et_caripi = strCaripi
        e.Row!et_caritel = strCaritel
        e.Row!et_carimail = strCarimail
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

#Region "funzioni specifiche per BNMGTERM.VB"
  Public Overridable Function GetImpostazioniFile(ByVal strLiv1 As String, ByRef dttOut As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldDocu.GetImpostazioniFile(strLiv1, dttOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function ImpostazioneFileGiaEsistente(ByVal strLiv1 As String, ByVal strNome As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldDocu.ImpostazioneFileGiaEsistente(strLiv1, strNome)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function DeleteImpostazioniFile(ByVal strLiv1 As String, ByVal strNome As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldDocu.DeleteImpostazioniFile(strLiv1, strNome)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
#End Region

#Region "funzioni specifiche per BNDBCACL.VB"
  Public Overridable Function CaclApri(ByVal strDitta As String, ByVal strCodartf As String, _
    ByRef dsOut As DataSet) As Boolean
    Try
      '-------------------------------------------------------------------------------------------------------------
      strDittaCorrente = strDitta
      '-------------------------------------------------------------------------------------------------------------
      If oCldDocu.GetDataCacl(strDitta, strCodartf, dsOut) = False Then Return False
      '-------------------------------------------------------------------------------------------------------------
      dsCaclShared = dsOut
      '-------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
#End Region

  Public Overridable Function ConvImpValuta(ByVal dIn As Decimal) As Decimal
    ConvImpValuta = 0
    Try
      Return oCldDocu.ConvImpValuta(strDittaCorrente, True, dIn, _
                                    NTSCInt(dttET.Rows(0)!et_valuta), NTSCDate(dttET.Rows(0)!et_datdoc), _
                                    NTSCDec(dttET.Rows(0)!et_cambio))

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

  Public Overridable Function PresenzaArtConai(ByVal dttTmp As DataTable) As Boolean
    Dim i As Integer
    Dim bArtC As Boolean
    Try
      If dttTmp Is Nothing Then Return False

      For i = 0 To dttTmp.Rows.Count - 1
        If Microsoft.VisualBasic.Left(NTSCStr(dttTmp.Rows(i)!ec_codart), 5).ToUpper = "CONAI" Then
          bArtC = True
          Exit For
        End If
      Next

      Return bArtC

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
  Public Overridable Sub ImpostaFormatQtaEsistCorpo(ByVal strNewFormat As String)
    Try
      If strFormatQtaEsistCorpo <> strNewFormat Then
        strFormatQtaEsistCorpo = strNewFormat
        ThrowRemoteEvent(New NTSEventArgs("FormatQtaEsistCorpo", ""))
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
  End Sub

  Public Overridable Function CoveAggControp(ByVal nCodcove As Integer) As Integer
    CoveAggControp = nCodcove
    Try
      If bUsaAggContropAnagra = False Then Return nCodcove
      If lAgControp = 0 Then Return nCodcove
      If nCodcove = 0 Then Return nCodcove
      If dsTabcove.Tables("TABCOVE").Rows.Count = 0 Then Return nCodcove
      If dsTabcove.Tables("TABCOVE").Select("tb_codcove = " & lAgControp + nCodcove).Length > 0 Then Return lAgControp + nCodcove
      Return nCodcove

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

#Region "Promozioni"
  Public Overridable Sub CaricaContrattiCliente()
    Dim dttContratti As DataTable
    Dim dtDatPromo As Date
    Dim dOraPromo As Decimal
    Try
      If Not bModCCC Then Return
      'Su alcuni tipi di documento non deve calcolare le promozioni
      If strTipiRkNoPromozioni.IndexOf(NTSCStr(dttET.Rows(0)!et_tipork), StringComparison.CurrentCultureIgnoreCase) > -1 Then Return

      'Annulla eventuali promozioni applicate
      If dttEC.Rows.Count > 0 Then AnnullaPromozioni()

      'Come prima operazione annullo i contratti precedenti
      If BORDINI Then
        dttET.Rows(0)!et_codtes = 0
        dttET.Rows(0)!et_codtes2 = 0
        dttET.Rows(0)!et_codtes3 = 0
        dttET.Rows(0)!et_codtes4 = 0
        dttET.Rows(0)!et_codtes5 = 0
        dttET.Rows(0)!et_codtes6 = 0
      Else
        dttETC.Rows(0)!tm_codtes = 0
        dttETC.Rows(0)!tm_codtes2 = 0
        dttETC.Rows(0)!tm_codtes3 = 0
        dttETC.Rows(0)!tm_codtes4 = 0
        dttETC.Rows(0)!tm_codtes5 = 0
        dttETC.Rows(0)!tm_codtes6 = 0
      End If

      DataOraPromozioni(dtDatPromo, dOraPromo)

      'Non ho ancora indicato il cliente. Inutile procedere
      If NTSCInt(dttET.Rows(0)!et_conto) = 0 Then Return

      With dttET.Rows(0)
        'Richiedo la lista dei contratti associati al cliente\destinazione
        dttContratti = CType(oCleComm, CLELBMENU).CaricaContrattiCliente(strDittaCorrente, NTSCInt(!et_conto), NTSCInt(!et_coddest), dtDatPromo, dOraPromo)
      End With

      For z As Integer = 0 To dttContratti.Rows.Count - 1
        'Il primo codice contratto è senza il numero 1, i successivi hanno il numero
        If BORDINI Then
          dttET.Rows(0)(("et_codtes" & (z + 1)).Trim("1"c)) = dttContratti.Rows(z)!ret_codtes
        Else
          dttETC.Rows(0)(("tm_codtes" & (z + 1)).Trim("1"c)) = dttContratti.Rows(z)!ret_codtes
        End If
      Next

      'Aggiungo i contratti associati all'agente 1 e 2
      AggiungiContrattiAgente(1, NTSCInt(dttET.Rows(0)!et_codagen))
      'Considera l'agente 2 anche se è uguale all'agente 1
      AggiungiContrattiAgente(2, NTSCInt(dttET.Rows(0)!et_codagen2))

      CaricaPromozioni()
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
  Public Overridable Sub AggiungiContrattiAgente(ByVal nPosAge As Integer, ByVal lAgente As Integer)
    Dim dtDatPromo As Date
    Dim dOraPromo As Decimal
    Dim strDescr As String = ""
    Try
      'L'agente non è stato impostato, non devo fare nulla
      If lAgente = 0 Then Return

      'Determino data di validità delle promozioni
      DataOraPromozioni(dtDatPromo, dOraPromo)

      oCldDocu.ValCodiceDb(lAgente.ToString, strDittaCorrente, "tabcage", "N", strDescr)

      'Determino i contratti e li ciclo per assegnarli al primo posto vuoto tra i contratti
      For Each dtrContratto As DataRow In CType(oCleComm, CLELBMENU).CaricaContrattiAgente(strDittaCorrente, lAgente, dtDatPromo, dOraPromo).Rows
        For z As Integer = 1 To 6
          'Il primo codice contratto è senza il numero 1, i successivi hanno il numero
          Dim strIndice As String = NTSCStr(z).Trim("1"c)
          If BORDINI Then
            If NTSCInt(dttET.Rows(0)("et_codtes" & strIndice)) <> 0 Then Continue For
            dttET.Rows(0)("et_codtes" & strIndice) = dtrContratto!ret_codtes
            Exit For
          Else
            If NTSCInt(dttETC.Rows(0)("tm_codtes" & strIndice)) <> 0 Then Continue For
            dttETC.Rows(0)("tm_codtes" & strIndice) = dtrContratto!ret_codtes
            Exit For
          End If
        Next
      Next
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


  Public Overridable Sub CancellaContratto(ByVal strPosContratto As String)
    Dim lContratto As Integer = 0
    Dim strDesContratto As String = ""
    Try
      If BORDINI Then
        lContratto = NTSCInt(dttET.Rows(0)("et_codtes" & strPosContratto))
        strDesContratto = NTSCStr(dttET.Rows(0)("xx_codtes" & strPosContratto))
      Else
        lContratto = NTSCInt(dttETC.Rows(0)("tm_codtes" & strPosContratto))
        strDesContratto = NTSCStr(dttETC.Rows(0)("xx_codtes" & strPosContratto))
      End If

      If lContratto = 0 Then Return

      Dim evnt As New NTSEventArgs(ThMsg.MSG_YESNO, oApp.Tr(Me, 130597419720365610, "Escludere il contratto |" & lContratto & " - '" & strDesContratto & "'| da questo documento?" & vbCrLf & _
                                                                                    "N.B. Escludendo un contratto verranno annullate tutte le promozioni applicate sul documento."))
      ThrowRemoteEvent(evnt)
      If evnt.RetValue <> ThMsg.RETVALUE_YES Then Return

      If BORDINI Then
        dttET.Rows(0)("et_codtes" & strPosContratto) = 0
      Else
        dttETC.Rows(0)("tm_codtes" & strPosContratto) = 0
      End If

      AnnullaPromozioni()

      CaricaPromozioni()
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


  Public Overridable Function ValidaContratto(ByVal dtrRow As DataRow, ByVal lContratto As Integer, ByVal strCampoValore As String, _
                                              ByVal strCampoDescr As String) As Integer
    Dim strDescr As String = ""
    Try
      If lContratto = 0 Then dtrRow(strCampoDescr) = "" : Return 0

      Dim dttRefites As New DataTable
      If oCldDocu.ValCodiceDb(lContratto.ToString, strDittaCorrente, "REFITES", "N", strDescr, dttRefites) Then
        If NTSCInt(dttRefites.Rows(0)!ret_codcage) > 0 Then
          strDescr &= " (" & NTSCInt(dttRefites.Rows(0)!ret_codcage) & " - "
          If NTSCInt(dttRefites.Rows(0)!ret_codcage) = NTSCInt(dttET.Rows(0)!et_codagen) Then
            strDescr &= NTSCStr(dttET.Rows(0)!xx_codagen)
          Else
            strDescr &= NTSCStr(dttET.Rows(0)!xx_codagen2)
          End If
          strDescr &= ")"
        End If
        dtrRow(strCampoDescr) = strDescr
        'Se manca la colonna relativa per capire chi è l'agente la aggiunge
        If Not dtrRow.Table.Columns.Contains(strCampoDescr & "agente") Then dtrRow.Table.Columns.Add(strCampoDescr & "agente", GetType(Integer))
        dtrRow(strCampoDescr & "agente") = dttRefites.Rows(0)!ret_codcage
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130596757733862463, "Contratto " & lContratto & " non valido")))
        lContratto = NTSCInt(dtrRow(strCampoValore))
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
    Return lContratto
  End Function

  Public Overridable Sub CaricaPromozioni()
    Dim arContrattiConto, arContrattiAgente1, arContrattiAgente2 As New ArrayList
    Dim lContrattiConto() As Integer = Nothing
    Dim lContrattiAgente1() As Integer = Nothing
    Dim lContrattiAgente2() As Integer = Nothing
    Dim lStabilimento As Integer = 0
    Dim dtDatPromo As Date
    Dim dOraPromo As Decimal
    Try
      If Not bModCCC Then Return
      If dttET.Rows.Count = 0 Then Return

      'Determina i contratti associati all'utente
      For z As Integer = 1 To 6
        Dim lContratto As Integer
        Dim lAgenteAssociatoContratto As Integer = 0
        Dim strSuffisso As String = z.ToString.Trim("1"c)
        If BORDINI Then
          lContratto = NTSCInt(dttET.Rows(0)("et_codtes" & strSuffisso))
        Else
          lContratto = NTSCInt(dttETC.Rows(0)("tm_codtes" & strSuffisso))
        End If
        If lContratto = 0 Then Continue For
        Dim strColonnaAgente As String = "xx_codtes" & strSuffisso & "agente"
        If BORDINI Then
          If dttET.columns.contains(strColonnaAgente) Then lAgenteAssociatoContratto = NTSCInt(dttET.Rows(0)(strColonnaAgente))
        Else
          If dttETC.columns.contains(strColonnaAgente) Then lAgenteAssociatoContratto = NTSCInt(dttETC.Rows(0)(strColonnaAgente))
        End If

        Select Case lAgenteAssociatoContratto
          Case 0 : arContrattiConto.Add(lContratto) 'Agente 0 = Cliente
          Case NTSCInt(dttET.Rows(0)!et_codagen)
            'Agente = Agente 1, caso particolare, Agente 1 e Agente 2 sono uguali
            If arContrattiAgente1.Contains(lContratto) Then
              'L'Agente 1 è uguale all'agente 2 e sull'Agente 1 ho già indicato il contratto
              If NTSCInt(dttET.Rows(0)!et_codagen) = NTSCInt(dttET.Rows(0)!et_codagen2) Then arContrattiAgente2.Add(lContratto)
            Else
              'Non era ancora stato aggiunto, lo applico sull'agente 1
              arContrattiAgente1.Add(lContratto)
            End If
          Case NTSCInt(dttET.Rows(0)!et_codagen2) : arContrattiAgente2.Add(lContratto) 'Agente = Agente 2
        End Select
      Next

      'Riversa i contratti trovati con una struttura compatibile a quella richiesta per le promozioni
      lContrattiConto = ArrayListToIntegerArray(arContrattiConto)
      lContrattiAgente1 = ArrayListToIntegerArray(arContrattiAgente1)
      lContrattiAgente2 = ArrayListToIntegerArray(arContrattiAgente2)

      'Non ho ancora indicato il cliente. Inutile avviare la ricerca delle promoziona
      If dttET.Rows.Count = 0 OrElse NTSCInt(dttET.Rows(0)!et_conto) = 0 Then Return

      'Quando carico le promozioni verifico se sono valide per stabilimento associato al magazzino del negozio
      If NTSCInt(dttET.Rows(0)!et_magaz) <> 0 Then
        Dim dttMaga As New DataTable
        oCldDocu.ValCodiceDb(NTSCStr(dttET.Rows(0)!et_magaz), strDittaCorrente, "TABMAGA", "N", , dttMaga)
        lStabilimento = NTSCInt(dttMaga.Rows(0)!tb_codstab)
      End If

      DataOraPromozioni(dtDatPromo, dOraPromo)

      'Carica le promozioni da applicare al documento per conto
      CType(oCleComm, CLELBMENU).CaricaPromozioni(strDittaCorrente, NTSCStr(IIf(BORDINI, "BEORGSOR", "BEVEBOLL")), NTSCInt(dttET.Rows(0)!et_conto), NTSCInt(dttET.Rows(0)!et_coddest), _
                                                  dtDatPromo, dOraPromo, lContrattiConto, 0, lStabilimento, lCausaleScontiPiede, dttPromozioni, dsPromozioniOmaggi)
      'Carica le promozioni da applicare al documento per agente 1
      CType(oCleComm, CLELBMENU).CaricaPromozioniAgente(strDittaCorrente, NTSCStr(IIf(BORDINI, "BEORGSOR", "BEVEBOLL")), 1, NTSCInt(dttET.Rows(0)!et_codagen), _
                                                        dtDatPromo, dOraPromo, lContrattiAgente1, 0, lStabilimento, dttPromozioni)
      'Carica le promozioni da applicare al documento per agente 2
      CType(oCleComm, CLELBMENU).CaricaPromozioniAgente(strDittaCorrente, NTSCStr(IIf(BORDINI, "BEORGSOR", "BEVEBOLL")), 2, NTSCInt(dttET.Rows(0)!et_codagen2), _
                                                        dtDatPromo, dOraPromo, lContrattiAgente2, 0, lStabilimento, dttPromozioni)

      dttPromozioni.AcceptChanges()

      ThrowRemoteEvent(New NTSEventArgs("CaricaGrigliaPromozioni", ""))
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
  Public Overridable Sub ColoraGrigliaPromozioni()
    Try
      CType(oCleComm, CLELBMENU).CalcolaPromozioni_ColoreGrigliaPromozioni(PreparaParametriPromozioni())
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


  Public Overridable Function AnnullaPromozioni() As Boolean
    Try
      If Not bModCCC Then Return True

      'Prepara i parametri
      Dim oPromozioni As NTSPromozioni = PreparaParametriPromozioni()
      'Esegue la richiesta
      AddHandler CType(oCleComm, CLELBMENU).RemoteEvent, AddressOf Belbmenu_GestisciEventiEntity
      CType(oCleComm, CLELBMENU).AnnullaPromozioni(oPromozioni)

      'Ricolora le celle, per aggiornare visivamente le righe
      For Each dtrRow As DataRow In dttEC.Rows
        ColoraCelle(dtrRow)
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
    Finally
      If Not oCleComm Is Nothing Then RemoveHandler CType(oCleComm, CLELBMENU).RemoteEvent, AddressOf Belbmenu_GestisciEventiEntity
    End Try
  End Function
  Public Overridable Function ApplicaPromozioni() As Boolean
    Try
      If Not bModCCC Then Return True

      If dttEC.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130597366296990266, "Per calcolare le promozioni è necessario inserire almeno una riga nel corpo.")))
        Return False
      End If

      'Verifica che tutte le righe siano corrette
      For nRow As Integer = 0 To dttEC.Rows.Count - 1
        If Not CorpoTestPreSalva(dttEC, nRow) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130633043021704860, "Non è possibile calcolare le promozione finchè non verranno sistemati i problemi presenti sulle righe del corpo.")))
          Return False
        End If
      Next

      'Prepara i parametri
      Dim oPromozioni As NTSPromozioni = PreparaParametriPromozioni()

      'Prima di fare un nuovo calcolo annulla sempre le precedenti promozioni
      AddHandler CType(oCleComm, CLELBMENU).RemoteEvent, AddressOf Belbmenu_GestisciEventiEntity
      CType(oCleComm, CLELBMENU).AnnullaPromozioni(oPromozioni)

      'Esegue la richiesta
      bInCalcoloPromozioni = True
      CType(oCleComm, CLELBMENU).CalcolaPromozioni(oPromozioni)          ' Calcola le promozioni, esclusi gli omaggi da scegliere se consigarli o meno

      CType(oCleComm, CLELBMENU).CalcolaOmaggiDaPromozioni(oPromozioni)  ' Calcola Gli omaggi da scegliere se vanno consegnati o meno

      'Se ci sono delle promozioni da mostrare facciamo vedere la maschera di omaggi per sceglierli
      If dttOmaggi.Rows.Count > 0 Then ThrowRemoteEvent(New NTSEventArgs("MostraPropostaOmaggi", ""))

      'Ricolora le celle, per mostrare le righe in promozione
      For Each dtrRow As DataRow In dttEC.Rows
        ColoraCelle(dtrRow)
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
    Finally
      If Not oCleComm Is Nothing Then RemoveHandler CType(oCleComm, CLELBMENU).RemoteEvent, AddressOf Belbmenu_GestisciEventiEntity
      bInCalcoloPromozioni = False
    End Try
  End Function
  Public Overridable Function PreparaParametriPromozioni() As NTSPromozioni
    Dim oPromozioni As NTSPromozioni = Nothing
    Try
      oPromozioni = IstanziaNTSPromozioni()
      With oPromozioni
        .dttPromo = dttPromozioni
        .dsPromoOmaggi = dsPromozioniOmaggi
        .dttEc = dttEC
        .dttEt = dttET
        .dttEcTc = dttECTC
        .dttMovMatr = dttMOVMATR
        .dttOmaggi = dttOmaggi

        With .Parametri
          .strDitta = strDittaCorrente
          .strCodRepc = " " 'Gestione documenti non ha il punto cassa
          .strPromoNoStornoResi = strPromoNoStornoResi
          .nPeacIvainc = nPeacIvainc

          .strStampaRigaOmaggi = strStampaRigaOmaggi
          .lCodivaOmaggi = lCodivaOmaggi
          .strOmaggiDesel = strOmaggiDesel

          .lCausaleScontiPiede = lCausaleScontiPiede
          .bMovimQtaLotti = bMovimQtaLotti
          .nIncremContatoreRiga = nIncremContatoreRiga
        End With
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
    Return oPromozioni
  End Function


  Public Overridable Sub RicalcolaScontiSeAnnullaPromozione(ByVal dtrEc As DataRow, ByVal lPromoAnnullata As Integer)
    Try
      'Se sto annullando una promozione di sconto di riga che annullava gli sconti precedenti o che sovrascriveva alcuni sconti di riga,
      ' Allora ripristino gli sconti tramite la cerca sconti
      If bDocDaRetail OrElse bModCCC = False Then Return 'Le promozioni non vanno ricalcolate se il programma è chiamato da GPV.
      Dim dtrPromo() As DataRow = dttPromozioni.Select("tb_codrepr = " & lPromoAnnullata)
      If dtrPromo.Length = 0 Then Return

      With dtrPromo(0)
        'se avevo azzerato gli sconti allora passo direttamente al ricalcolo
        If NTSCStr(!tb_flnosconti) = "N" Then
          If NTSCStr(!tb_tipopr) <> "P" Then Return

          If NTSCDec(!tb_scont1) + NTSCDec(!tb_scont2) + NTSCDec(!tb_scont3) + NTSCDec(!tb_scont4) + NTSCDec(!tb_scont5) + NTSCDec(!tb_scont6) = 0 AndAlso _
             NTSCStr(!tb_flnosconti) = "N" Then Return
        End If
      End With

      If GetMemDttArti(dtrEc!ec_codart.ToString).Rows.Count = 0 Then Return

      'Pulisce gli sconti
      bInValidazCodart = True 'Per forzare il ricalcolo del prezzo
      SettaCondCommerciali(False, True, False, dtrEc, NTSCInt(dttArti.Rows(0)!ar_clascon), nClscan, False)
      bInValidazCodart = False
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

  Public Overridable Sub AggiungiArticoliOmaggio()
    Try
      CType(oCleComm, CLELBMENU).AggiungiArticoliOmaggio(PreparaParametriPromozioni())
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


  Public Overridable Sub DataOraPromozioni(ByRef dtDataPromo As Date, ByRef dOraPromo As Decimal)
    Try
      If BORDINI Then
        dtDataPromo = NTSCDate(dttET.Rows(0)!et_datpromo)
        dOraPromo = NTSCDec(dttET.Rows(0)!et_orpromo)
      Else
        dtDataPromo = NTSCDate(dttETC.Rows(0)!tm_datpromo)
        dOraPromo = NTSCDec(dttETC.Rows(0)!tm_orpromo)
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
  End Sub

  Public Overridable Function ArrayListToIntegerArray(ByVal arArray As ArrayList) As Integer()
    Dim lInteger() As Integer = Nothing
    Try
      If arArray.Count > 0 Then
        ReDim lInteger(arArray.Count - 1)
        For z As Integer = 0 To arArray.Count - 1
          lInteger(z) = NTSCInt(arArray(z))
        Next
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
    Return lInteger
  End Function
#End Region

  Public Overridable Sub LeggiSettaggioCondizioniCommerciali()
    Dim strTEMP As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      bUsaVecchiaSettaPrezzoPerDitta = CType(oCleComm, CLELBMENU).bUsaVecchiaCercaPrezzo
      strTEMP = oCldDocu.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "UsaVecchiaCercaPrezzo", "0", ".", "0")
      If strTEMP = "-1" Then
        bUsaVecchiaSettaPrezzoPerDitta = True
      Else
        If strTEMP = "1" Then bUsaVecchiaSettaPrezzoPerDitta = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      bUsaVecchiaSettaScontiPerDitta = CType(oCleComm, CLELBMENU).bUsaVecchiaCercaSconti
      strTEMP = oCldDocu.GetSettingBus("OPZIONI", ".", ".", "UsaVecchiaCercaSconti", "0", ".", "0")
      If strTEMP = "-1" Then
        bUsaVecchiaSettaScontiPerDitta = True
      Else
        If strTEMP = "1" Then bUsaVecchiaSettaScontiPerDitta = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      bUsaVecchiaSettaProvvigioniPerDitta = CType(oCleComm, CLELBMENU).bUsaVecchiaCercaProvvigioni
      strTEMP = oCldDocu.GetSettingBus("OPZIONI", ".", ".", "UsaVecchiaCercaProvvigioni", "0", ".", "0")
      If strTEMP = "-1" Then
        bUsaVecchiaSettaProvvigioniPerDitta = True
      Else
        If strTEMP = "1" Then bUsaVecchiaSettaProvvigioniPerDitta = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class