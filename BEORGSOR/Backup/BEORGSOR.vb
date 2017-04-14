Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Globalization


Imports System
Public Class CLEORGSOR
    Inherits CLEMGDOCU
#Region "VARIABILI"

    Public oCldGsor As CLDORGSOR 'oggetto dal
    Public strLastTiporkRegistroDoc As String = "."
    Public dtTimeStampDocY As DateTime
    Public nOrdrig As Integer = 0 'ordinamento righe corpo

    Public strBlocco As String = "*", strSospeso As String = "*"

    'variabili per 'nuovo da preventivo'
    Public bNuovoDaPrev As Boolean = False
    Public strTipoPrev As String = ""
    Public nAnnoPrev As Integer = 0
    Public strSeriePrev As String = ""
    Public lNumPrev As Integer = 0
    Public lLettoRegistro As Boolean = False

    Public bRipristinaDocumento As Boolean = False
    Public bNonEreditaNoteDaPreventivoInIC As Boolean = False 'se True - non eredita le note in IC creato da Preventivo con la funzione 'Nuovo documento da precedente'), default 0 - false, quindi di default eredita le note
    Public bNonEreditaNoteDaICInOF As Boolean = False 'se True - non eredita le note in Ordine a Fornitore creato da Impegno Cliente con la funzione 'Nuovo documento da precedente'), default 0 - false, quindi di default eredita le note

    Public bSoloSerieInTRKTPBF As Boolean = False 'se true, fa un controllo che il tipo bolla/fattura esista per tipodoc/serie

  Public bRiportaAbbuono1Offerta As Boolean = False
#End Region

#Region "Moduli"
    Private Moduli_P As Integer = CLN__STD.bsModOR
    Private ModuliExt_P As Integer = CLN__STD.bsModExtORE
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
#End Region

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    MyBase.strProgName = "BNORGSOR"
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDORGSOR"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldGsor = CType(MyBase.ocldBase, CLDORGSOR)
    oCldGsor.Init(oApp)
    BORDINI = True       'bemgdocu gestire un ordine

    VerificaUsoSettaCondCommerciali()

    Return True
  End Function

  Public Overrides Function InitExt() As Boolean
    Dim dRet As Boolean
    Try
      dRet = MyBase.InitExt()

      bPrelistIvato = True        'per compatibilità con VB6, dove l'opzione non viene settata. in questo modo la cercaprezzo per gsor funziona come in VB6

      Return dRet

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

  '------------------------

  Public Overridable Sub LeggiRegistro()
    Dim strTemp() As String
    Try
      '---------------------------------
      'leggo le opzioni di registro globali
      bGestAnaext = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BS--CLIE", "OPZIONI", ".", "GestAnaExt", "0", " ", "0"))

      strCalcPesi = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "Calc_pesi_in_doc", "S", " ", "S")
      strContrFidoInsolinInsOrd = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "Contr_fido_insol_ordine", "N", " ", "N")
      strVisMemList1 = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "Vis_mem_list1_ord", "N", " ", "N")
      nVisMemNumList = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "VisMemNumList", "1", " ", "1")) 'indica il numero di listino da memorizzare (default listino 1)
      strVisNoteConto = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "Vis_note_conto_in_ord", "N", " ", "N")
      strVisNoteArti = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "Vis_note_articolo_in_ordini", "0", " ", "0")
      strVisNoteContoApertura = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "Vis_note_conto_in_apertura", " ", " ", " ")
      strTipValSemPf = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsveboll", "Opzioni", ".", "Tipo_valorizz_sem_pf", " ", " ", " ")
      strTipoListinoMat = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsveboll", "Opzioni", ".", "Tipo_list_costi_mat_carichi", " ", " ", " ") 'blank,1,2,3
      bReprintDoc = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "ConfermaRistampa", "0", " ", "0"))
      nIncremContatoreRiga = CInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "IncremContatoreRiga", "1", " ", "1"))
      strTestScomin = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "Test_scorta_min", "N", " ", "N")
      strTestScominCome = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "Test_scorta_min_come", "A", " ", "A") 'A=esist-prenot(default), B=esist-impeg
      strTestEsist = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "Test_esistenza", "N", " ", "N")
      strTestEsistQuando = UCase$(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "Test_esistenza_quando", " ", " ", " "))
      strTestEsistArtTc = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "TestEsistArticoliTC", "C", " ", "C") ' C = Colore, T = Taglia
      strTipoTestEsist = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "Tipo_test_esistenza", "O", " ", "O")
      strTestSottoCosto = UCase$(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "TestSottoCosto", "N", " ", "N")) 'N,A,B,P
      strTestSottoCostoModifica = UCase$(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "TestSottoCostoModifica", "N", " ", "N")) 'N,A,B,P
      strTestSottoCostoQuando = UCase$(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "TestSottoCostoQuando", "A", " ", "A")) 'A=per riga,B=finale,C=riga+finale
      strTestSottoCostoTipo = UCase$(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "TestSottoCostoTipo", "U", " ", "U")) 'U,M,S (oppure n° listino)
      strTestSottoCostoZero = UCase$(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "TestSottoCostoZero", "S", " ", "S")) 'S,N,M
      dTestSottoCostoRicaricoMinimo = NTSCDec(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "TestSottoCostoRicaricoMinimo", "0", " ", "0")) 'percentuale (indicare "12,50" per avere un controllo su un ricarico del 12.5%) per il test sottiocosto
      bTestSottoCostoOscuraCosto = CBool(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "TestSottoCostoOscuraCosto", "0", " ", "0"))) 'se impostati i messaggi sui test sottocosto abilitando questa opzione non appare all'operatore la cifra del costo oggetto di confronto. Compaiono aterischi.
      'Implementazione calcolo spese trasp (4.0b) S=sempre N=mai (def), 'E' solo doc. emessi
      strCalcolaSpeseTrasp = oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "CalcolaSpeseTrasp", "N", " ", "N")
      bCalcolaSpeseTraspSoloNuovi = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "CalcolaSpeseTraspSoloNuovi", "0", " ", "0"))
      If oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "UsaVettore1perCalcoloSpeseTrasp", "N", " ", "N") = "S" Then
        bUsaVettore1perCalcoloSpeseTrasp = True
      Else
        bUsaVettore1perCalcoloSpeseTrasp = False
      End If
      lNoModifQuantSuColli = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "NoModifQuantSuColli", "0", " ", "0"))
      bNoModifQuantSuColli = CBool(lNoModifQuantSuColli) 'se attiva (-1) quando si modificano manualmente i colli (o UM) non modifica la quantità SE GIA' DIVERSA DA ZERO (1 = non si attiva anche se la quantità = 0)

      strNoteNuoviDocumenti = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "NoteNuoviDocumenti", "", " ", "")
      bRiportaNote1CF = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "RiportaNote1CF", "0", " ", "0"))
      bRiportaNote2CF = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "RiportaNote2CF", "0", " ", "0"))
      nRiportaNoteDaAnaext = 0
      If bGestAnaext = True Then
        nRiportaNoteDaAnaext = CInt(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "RiportaNoteDaAnaext", "0", " ", "0"))) 'imposta nel campo note dell'ordine campi descritivi eventualmente presenti nelle estensioni anagrafiche. Assume valori numerico compresi tra 0 e 15 (0=nessuno, 1=dalla descr. breve 1 ax_descr1, 2=dalla descr. breve 2 ax_descr2, ..., 10=dalla descr. breve 10 ax_descr10, 11=dalla descr. estesa 1 ax_desext1, 12=dalla descr. estesa 2 ax_desext2, 13=dalla descr. estesa 3 ax_desext3, 14=dal memo 1 ax_memo1, 15=dal memo 2 ax_memo2
      End If
      gstrTipoArtConf = UCase$(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bscpcopr", "Opzioni", ".", "TipoArtConf", "0", " ", "0"))
      bIgnoraMagDistintaBase = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "IgnoraMagDistintaBase", "0", " ", "0")) '-1 o 0
      bGenNumCommecaAutR = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "GenNumCommecaAutR", "0", " ", "0"))
      bGenNumCommecaAutR_AllaFine = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "GenNumCommecaAutR_AllaFine", "0", " ", "0"))
      bEreditaMagImpCli = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "EreditaMagImpCli", "0", " ", "0")) '-1 duplicando ic->op eredita il mag. IC come magazzino 1 e mag. impegni (in deroga al tpbf)
      bCalcolaColliPesiSuDocAperti = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "CalcolaColliPesiSuDocAperti", "0", " ", "0")) '-1=i colli/pesi vengono ricalcolati sui nuovi documenti
      bLeggiOpzioniDoc = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "LeggiOpzioniDoc", "0", " ", "0"))
      bRiportaCommDaTestataDupl = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "RiportaCommDaTestataDupl", "0", " ", "0"))
      bForzaLetturaScontiQta = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "ForzaLetturaScontiQta", "0", " ", "0")) '-1=rilegge sempre gli sconti alla variazione della qta
      bForzaLetturaPrezziQta = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "ForzaLetturaPrezziQta", "0", " ", "0")) '-1=rilegge sempre  prezzi alla variazione della qta
      bDisabilitaMsgPrezzoZero = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "DisabilitaMsgPrezzoZero", "0", " ", "0")) '-1=disabilita il messaggio di conferma prezzo zero
      bForzaLetturaPrezziQta = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "ForzaLetturaPrezziQta", "0", " ", "0")) '-1=rilegge sempre  prezzi alla variazione della qta
      bDisabilitaMsgPrezzoZero = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "DisabilitaMsgPrezzoZero", "0", " ", "0")) '-1=disabilita il messaggio di conferma prezzo zero
      bDisabilitaMsgQuantZero = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "DisabilitaMsgQuantZero", "0", " ", "0")) '-1=disabilita il messaggio di conferma quantità zero
      gbUsaFiltroCommessa = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "UsaFiltroCommessa", "-1", " ", "-1")) 'Legge la distinta base ignora il filtro commessa (default -1=applica filtro)
      gbUsaFiltroPadre = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "UsaFiltroPadre", "-1", " ", "-1")) 'Legge la distinta base ignora il filtro art. padre (default -1=applica filtro)
      bNoDatValDistinta = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "NoDatValDistinta", "0", " ", "0")) 'se impostata a -1 non chiede conferma della data validità distinta, usa sempre data documento
      bDisabilitaCheckAnnoData = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "DisabilitaCheckAnnoData", "0", " ", "0")) 'se impostata a -1 non fa il controllo se la data ordine è inclusa nell'anno
      fRicalcPrez = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "RicalcolaPrezziDaOA", "0", " ", "0")) 'se attiva evadendo O.A. ricalcola i prezzi
      fRicalcScon = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "RicalcolaScontiDaOA", "0", " ", "0")) 'se attiva evadendo O.A. ricalcola gli sconti
      fRicalcProv = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "RicalcolaProvvigioniDaOA", "0", " ", "0")) 'se attiva evadendo O.A. ricalcola le provvigioni
      fSelArticDiv = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "SelezionaOAArticoloDiv", "0", " ", "0")) 'se attiva evadendo O.A. consente di cambiare codice articolo
      fSelContoDiv = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "SelezionaOAContoDiv", "0", " ", "0")) 'se attiva evadendo O.A. consente di selezionare O.A. intestati ad altri conti
      gbRiportaFasiFantasmi = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "RiportaFasiFantasmi", "0", " ", "0")) 'se impostata a -1 espodendo la distinta riporta le fasi anche dei fantasmi
      bChiediDestInTestata = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "ChiediDestInTestata", "0", " ", "0")) 'se impostata a  -1 se il cliente NON ha destinazioni div. indicate, chiede se aprire lo zoom destinazioni in testata (dopo aver digitato il conto)
      ImpostaFormatQtaEsistCorpo(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "FormatQtaEsistCorpo", oApp.FormatQta, " ", oApp.FormatQta))  'formattazione VB sulle qta esist/impeg/ordin. sul corpo
      bRilevaDisponibPerCommessa = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "RilevaDisponibPerCommessa", "0", " ", "0")) 'se attiva (-1) mostra nel corpo le disponbilità per commessa (sugni articoli gestiti a ordini/impegni/commessa
      lRilevaDisponibCodCommessa = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "RilevaDisponibCodCommessa", "9999", " ", "9999")) 'indicare il n. di comemssa (default=9999) su cui visualizzare con un messaggio la disponibilità per articolo/magazzino/commessa
      strTestEsistCome = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "TestEsistCome", "E", " ", "E")  'E(defaut)=su esistenza, D=disponibilità netta
      bTestEsistPerComm = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "TestEsistPerComm", "0", " ", "0")) 'se attiva (-1) determina esistenze/disponibilità x articoo/magazzino/commessa sugli articoli gestiti a commessa
      nProponiArtSostitutivo = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "ProponiArtSostitutivo", "0", " ", "0")) 'se attiva (-1) alla digitazione del codice articolo, se possiede un articolo sostitutivo lo segnala dando la possibilità di usarlo.
      bDisabilitaCheckCommesse = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "Opzioni", ".", "DisabilitaCheckCommesse", "0", " ", "0")) '-1=disabilita il controllo finale sulla obblig. del numero commessa
      bBD_CodArfo = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "Opzioni", ".", "Bd_CodArfo", "0", " ", "0")) '-1=legge il barcode da caf_desnote
      strDispMultiMag = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "DispMultiMag", "N", " ", "N")
      bTestEsistScortaMultiMag = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "TestEsistScortaMultiMag", "0", " ", "0"))
      bDuplicaR2HSoloNonPrenot = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "DuplicaR2HSoloNonPrenot", "0", " ", "0"))
      bDuplicaR2HEreditaRifImpCli = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "DuplicaR2HEreditaRifImpCli", "0", " ", "0"))
      bDuplicaR2HRagSocSuRiferim = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "DuplicaR2HRagSocSuRiferim", "0", " ", "0"))
      bDuplicaR2HEscludiArtNoDiba = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "DuplicaR2HEscludiArtNoDiba", "0", " ", "0"))
      bDuplicaR2HSoloArtGesComm = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "DuplicaR2HSoloArtGesComm", "0", " ", "0"))
      bTest_esistenza_duplimpprod = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "Test_esistenza_duplimpprod", "0", " ", "0"))
      bTestCoerenzaCentriLavMagaz = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "Opzioni", ".", "TestCoerenzaCentriLavMagaz", "0", " ", "0")) 'se -1, prima di salvare un ORDINE DI PRODUZIONE se il magazzino impegni è diverso da 0 controlla che i centri di lavoro nelle lavorazioni collegate abbiano magazzino associato = magazzino impegni
      bBarcodeConQta = CBool(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "Opzioni", ".", "BarcodeConQta", "0", " ", "0"))) 'se attiva (-1) rileva la quantità indicata nei barcode
      bBarcodeDerogaQta = CBool(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "Opzioni", ".", "BarcodeDerogaQta", "0", " ", "0"))) 'se attiva (-1) rileva la quantità indicata nei barcode pero' in deroga a BarcodeConQta (sono alternative)
      bRiapriDocSuSalva = NTSCInt(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "Opzioni", ".", "RiapriDocSuSalva", "0", " ", "0"))) 'se abilitata a -1 al salvataggio del documento lo riapre (lo stesso) posizionandosi in testata, se impsotata ad 1 utilizza l'impostazione della voce di menu 'Riapri doc dopo salva'
      bLetturaBCinArtRilevaDati = CBool(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "Opzioni", ".", "LetturaBCinArtRilevaDati", "0", " ", "0")))
      strQuandoAgContropManca = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Opzioni", ".", ".", "QuandoAgcontropManca", "0", " ", "0")
      bDisabilitaDecodificaBarcode = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "DisabilitaDecodificaBarcode", "0", " ", "0")) 'se attiva (-1) non effettua la decodifica della colonna barcode
      bCodartDaBarcode = CBool(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "CodartDaBarcode", "0", " ", "0"))) 'NON DOCUMENTARE

      '-----------------------------------------------------------------------------------------
      '--- Federico: opzione di registro che fa saltare il controllo sul fido del cliente
      '--- (se il tipo documento è 'Preventivo')
      '-----------------------------------------------------------------------------------------
      bNoCheckFidoSuPrev = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "NoCheckFidoSuPrev", "0", " ", "0"))
      '-----------------------------------------------------------------------------------------
      '--- Opzione di registro per il settaggio dell'eventuale riga di ORDLIST collegata
      '--- e il messaggio di richiesta
      '--- 0 = appare la MsgBox per la richiesta
      '--- 1 = non appare la MsgBox e riapre automaticamente la riga di ORDLIST (ol_stato = 'S')
      '--- 2 = non appare la MsgBox e non riapre la riga di ORDLIST (ol_stato rimane = 'T')
      '-----------------------------------------------------------------------------------------
      nSetStatoRigaOrdlist = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "SetStatoRigaOrdlist", "0", " ", "0"))
      '-----------------------------------------------------------------------------------------
      strTemp = strDispMultiMag.Split(";"c)
      strElencoMagazzini = ""
      For i As Integer = 0 To strTemp.Length - 1
        If IsNumeric(strTemp(i)) Then
          strElencoMagazzini = strElencoMagazzini & strTemp(i) & ", "
        End If
      Next
      If strElencoMagazzini <> "" Then strElencoMagazzini = Mid(strElencoMagazzini, 1, Len(strElencoMagazzini) - 2)
      bNomeDocWordNumero = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "NomeDocWordNumero", "0", " ", "0"))
      bUsaMagStockArticolo = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "UsaMagStockArticolo", "0", " ", "0")) 'digitando righe sul corpo, se l'opzione è abilitata a -1, effettua il movimento sul magazzino di stock invece del magazzino di testata.
      bDescrAggKitSuNote = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "DescrAggKitSuNote", "0", " ", "0")) 'se abilitata a -1 quando vengono letti i componenti dei kit viene riportata la 'Descrizione aggiuntiva' indicata sul componente KIT nel campo 'Note' della riga del corpo (attenzione: se abilitata l'opzione il campi note eventuamente ereditato dal'articolo viene sovrascitto)
      bProponiQtaDaAss = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "ProponiQtaDaAss", "0", " ", "0")) 'se abilitata a -1 propone per default la quantità residua da assegnare (per la generazioend ella nota di relievo) rimane solo il flag 'Assegnazioen a saldo' che deve essere messo manualmente, solo sugli ImpegniCliente
      bMovimQtaLotti = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "MovimQtaLotti", "0", " ", "0")) 'se abilitata a -1 obbliga che l'articolo venga movimentato a multipli di lotto ( se l'articolo ha una politica di riordino "su fabbisogno con lotto" ' NON DOCUMENTARE
      bNonCalcolaColli = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "NonCalcolaColli", "0", " ", "0")) 'se abilitata non effettua i calcoli automatici sul campo totale colli (sia sui documenti aperti che quelli nuovi) il campo colli è sempre un dato da imputare manualmente
      '-----------------------------------------------------------------------------------------
      bAttivaRecentTCO = False
      If bModTCO Then
        bAttivaRecentTCO = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "AttivaRecentTCO", "0", " ", "0")) 'se abilitato, attiva il RECENT per i campi relativi a Taglie e colori
      End If
      '-----------------------------------------------------------------------------------------
      bModifColliSuQuant = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "ModifColliSuQuant", "0", " ", "0")) 'se abilitata a -1 (anche in \opzionidoc) alla modifica del campo QUANTITA vengono modificati all'indietro i COLLI (col fattore di conversione tra UM)
      bNoTempiSuTerzisti = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OPZIONI", ".", "NoTempiSuTerzisti", "0", " ", "0")) 'opzione di registro che se abilitata quando scrive la lavorazione terzista non inserisce il tempo di esecuzione (giorni x 8 ore) ma inserisce zero.
      bNoColliSuFigliKit = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "NoColliSuFigliKit", "0", " ", "0")) 'se abilitata a -1 sui figli dei kit inserire zero nel campo colli (al posto dei colli per conversione della UMP indicata nei colli)

      bAggDatConsFigliSuQtaPadre = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "AggDatConsFigliSuQtaPadre", "0", " ", "0")) ' Se abilitata a -1 al cambio della quantità di una riga di ordine di produzione ricalcola la data di consegna (NON TIENE CONTI DI MD_RITARDO)
      bNoPesiSuRigheKitFittizie = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "NoPesiSuRigheKitFittizie", "0", " ", "0"))

      'CRM
      strSoloConfermate = oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "Solo_Offerte_Confermate", "0", " ", "0")
      nContropDefaultOfferte = CInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "ContropDefaultOfferte", "0", " ", "0")) 'indica la contropartita da utilizzarsi nel caso non sia specificata nell'anagrafica articolo o nella testata del documento
      'PROMOZIONI
      bAbilitaPromozioni = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "Abilita_Promozioni", "0", " ", "0"))
      'ALERT
      bGestAlert = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "Abilita_Alert", "0", " ", "0"))
      '
      'bUsaKeyOrdWord = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente,"Bsorgsor", "Opzioni", ".", "UsaKeyordWord", "0", " ", "0"))
      bNonRiportareDatiTestOA = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "NonRiportareDatiTestOA", "0", " ", "0")) ' se abilitata a -1 in fase di selezione ordini/impegni aperti non riporta i dati di testata dell'ord. aperto originaria (opera come in Business8)
      nUsaCondPagContoFatt = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "UsaCondPagContoFatt", "-1", " ", "-1")) ' se abilitata a -1 in fase di selezione ordini/impegni aperti non riporta i dati di testata dell'ord. aperto originaria (opera come in Business8)
      If nUsaCondPagContoFatt = -1 Then
        bUsaCondPagContoFatt = True
      Else
        bUsaCondPagContoFatt = False
      End If
      'bNewCallSP = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente,"OPZIONI", ".", ".", "NewCallSP", "-1", " ", "-1")) 'se attiva (-1) chiama le varie Stored Procedures passando direttamente i parametri con una stringa precostruita
      bDeterminaBolliSuOperazEsenti = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Opzioni", ".", ".", "DeterminaBolliSuOperazEsenti", "0", " ", "0")) 'Se attiva il bollo non viene determinato solo se in testata vi è il codice di esenzione, ma se la somma delle operazioni esenti del documenti (righe e spese di piede) supera la soglia minima in TABBOTR  ' NON DOCUMENTARE
      '-----------------------------------------------------------------------------------------
      '--- Su righe nuove:
      '--- se attiva, se codice Esenzione di testata è diversa da zero, se tipo Iva articolo
      '--- è = 2 (Op. Esenti/Non imp.), propone ARTICO.ar_codiva
      '-----------------------------------------------------------------------------------------
      bPrioritaCodeseArticolo = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "PrioritaCodeseArticolo", "0", " ", "0"))
      '-----------------------------------------------------------------------------------------
      bSalvaCondizioniFinali = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "SalvaCondizioniFinali", "0", " ", "0"))
      '-----------------------------------------------------------------------------------------
      bDuplicaAllole = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "DuplicaAllole", "0", " ", "0")) 'DA NON DOCUMENTARE
      bRilevaBloccoDaAnagra = CBool(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "RilevaBloccoDaAnagra", "0", " ", "0"))) ' se abilitata alla indicazione del cliente rileva lo stato di blocco dalla anagrafica alla testata dell'ordine
      bConsentiCreazOrdiniCliFornBloccoFisso = CBool(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "ConsentiCreazOrdiniCliFornBloccoFisso", "0", " ", "0"))) ' se abilitata consente di intestare un ordini anche a cli/forn con blocco fisso.
      bConfermacompilazioneSchedaTrasp = CBool(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "ConfermacompilazioneSchedaTrasp", "0", " ", "0"))) ' se abilitata quando salva/stampa chiede di compilare la scheda di trasporto

      strTipoGenNumCommeca = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "TipoGenNumCommeca", "1", " ", "1")
      bDisabilitaControlliSubcomm = CBool(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "DisabilitaControlliSubcomm", "0", " ", "0"))) ''--- Se abilitata, NON controlla la validità della sottocommessa di riga
      bEsplodiD = Not (CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "Opzioni", ".", "No_Esplosione_Articolo_D", "0", " ", "0")))

      bSegnalaCreazOrdiniCliFornBloccati = CBool(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "SegnalaCreazOrdiniCliFornBloccati", "0", " ", "0"))) ' se abilitata avvisa (senza bloccare) se il cli/forn è in uno stato di blocco per insoluti/fuori fido/rd scadute. Non ha effetto se l'opzione di registro \bsorgsor\opzioni(doc)\Contr_fido_insol_doc
      If strContrFidoInsolinInsOrd = "S" Then bSegnalaCreazOrdiniCliFornBloccati = False

      If bForzaLetturaPrezziQta Then nPrperqta = CLN__STD.bsPrSetsiqta
      If bForzaLetturaScontiQta Then nScperqta = CLN__STD.bsPrSetsiqta

      bSalvaAncheSeNonModificato = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "Opzioni", ".", "SalvaAncheSeNonModificato", "0", " ", "0"))

      bAbilitaGenerazLottoSuOrdini = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "Opzioni", ".", "AbilitaGenerazLottoSuOrdini", "0", " ", "0"))
      bApriAnalottiDopoNew = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "Opzioni", ".", "ApriAnalottiDopoNew", "0", " ", "0"))

      bNonEreditaNoteDaPreventivoInIC = CBool(Val(oCldGsor.GetSettingBus("BSORGSOR", "OPZIONI", ".", "NonEreditaNoteDaPreventivoInIC", "0", " ", "0")))
      bNonEreditaNoteDaICInOF = CBool(Val(oCldGsor.GetSettingBus("BSORGSOR", "OPZIONI", ".", "NonEreditaNoteDaImpegnoInOrdine", "0", " ", "0")))
      nInibisciTipiArticoliDiversi = NTSCInt(oCldGsor.GetSettingBus("BSORGSOR", "OPZIONI", ".", "InibisciTipiArticoliDiversi", "0", " ", "0"))
      nInibisciTipiArticoliDiversiTrannePagContanti = NTSCInt(oCldGsor.GetSettingBus("BSORGSOR", "OPZIONI", ".", "InibisciTipiArticoliDiversiTrannePagContanti", "0", " ", "0"))
      strNomeCampoArtico = NTSCStr(oCldGsor.GetSettingBus("BSORGSOR", "OPZIONI", ".", "NomeCampoArtico", "ar_tipo", " ", "ar_tipo"))
      strDataInibisciArticoliDiversi = NTSCStr(oCldGsor.GetSettingBus("BSORGSOR", "OPZIONI", ".", "DataInibisciArticoliDiversi", "", " ", ""))

      bRilevaCodPagaCambioContoDocModif = CBool(oCldDocu.GetSettingBus("OPZIONI", ".", ".", "RilevaCodPagaCambioContoDocModif", "0", " ", "0"))

      bGestioneAbbinamentiTaglie = CBool(oCldGsor.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "Gestione_Abbinamenti_Taglie", "0", " ", "0"))

      lRiportaMisura1DaArtico = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "RiportaMisura1DaArticolo", "0", ".", "0")) 'Compila la Misura 1 con il valore indicato nell'anagrafica articoli
      lRiportaMisura2DaArtico = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "RiportaMisura2DaArticolo", "0", ".", "0")) 'Compila la Misura 2 con il valore indicato nell'anagrafica articoli
      lRiportaMisura3DaArtico = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "RiportaMisura3DaArticolo", "0", ".", "0")) 'Compila la Misura 3 con il valore indicato nell'anagrafica articoli

      bSoloSerieInTRKTPBF = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "SoloSerieInTRKTPBF", "0", ".", "0"))

      bRiportaAbbuono1Offerta = CBool(oCldGsor.GetSettingBus("BSORGSOR", "OPZIONI", ".", "RiportaAbbuono1Offerta", "0", " ", "0"))

      bConsentiScontiSuOmaggi = CBool(oCldGsor.GetSettingBus("OPZIONI", ".", ".", "ConsentiScontiSuOmaggi", "0", " ", "0"))

      'Per promozioni, legge le stesse di BEREGSRE
      strStampaRigaOmaggi = oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSREGSRE", "OPZIONI", ".", "StampaRigaOmaggi", "M", " ", "M")
      lCodivaOmaggi = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSREGSRE", "OPZIONI", ".", "CodiceIvaOmaggi", "0", " ", "0"))
      strPromoNoStornoResi = oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSREGSRE", "OPZIONI", ".", "TipiPromozioniSuiResi", "", ".", "")
      strOmaggiDesel = oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSREGSRE", "OPZIONI", ".", "OmaggiProponiDeselezionati", "A", ".", "A") 'A = Automatico, N = Nessuno, T = Tutti 
      lCausaleScontiPiede = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSREGSRE", "OPZIONI", ".", "CausaleScontiPiede", "0", " ", "0"))
      strTipiRkNoPromozioni = oCldGsor.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "TipiRkNoPromozioni", "TUZHXY", ".", "TUZHXY")

      bSalvaNuovoDocApplicaPromo = CBool(Val(oCldGsor.GetSettingBus("BSORGSOR", "Opzioni", ".", "SalvaNuovoDocApplicaPromo", "0", " ", "0")))
      '--------------------------------------------------------------------------------------------------------------
      nListinoCalcoloRicaricoMargine = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "ListinoCalcoloRicaricoMargine", "0", ".", "0"))
      If (nListinoCalcoloRicaricoMargine < -2) Or (nListinoCalcoloRicaricoMargine > 9999) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130804005158503185, "Attenzione!" & vbCrLf & _
          "L'opzione di registro:" & vbCrLf & _
          " . 'BSORGSOR/OPZIONI/ListinoCalcoloRicaricoMargine'" & vbCrLf & _
          "è stata impostata con un valore NON valido (|" & nListinoCalcoloRicaricoMargine.ToString & "|)." & vbCrLf & _
          "Valore consentito compreso fra '-2' e '9999'." & vbCrLf & _
          "L'opzione sarà considerata a '0'.")))
        nListinoCalcoloRicaricoMargine = 0
      End If
      bMargineValoreUnitario = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "MargineValoreUnitario", "0", ".", "0"))

      nDatiniCompMese = NTSCInt(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsveboll", "OPZIONI", ".", "DatiniCompMese", "0", " ", "0"))) 'se -1, nel campo 'data inizio competenza eeconomica' viene proposto il primo del mese, altrimenti datdoc: serve per chi gestisce i conti a data comp. economica ma vuole fare sempre analisi mensili, senza che pnfa generi molte righe di prinot/priana2
      nGiorniMargineScadenzaLotto = NTSCInt(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "GiorniMargineControlloScadenzaLotto", "9999", " ", "9999")))

      '--------------------------------------------------------------------------------------------------------------
      '----------------------
      'Imposta tutte le variabili originarie (per opzioni documento)
      strGLOBCalcPesi = strCalcPesi
      strGLOBContrFidoInsolinInsOrd = strContrFidoInsolinInsOrd
      strGLOBVisMemList1 = strVisMemList1
      nGLOBVisMemNumList = nVisMemNumList
      strGLOBVisNoteConto = strVisNoteConto
      strGLOBVisNoteArticolo = strVisNoteArti
      strGLOBVisNoteContoApertura = strVisNoteContoApertura
      strGLOBTestScomin = strTestScomin
      strGLOBTestScominCome = strTestScominCome
      strGLOBTestEsist = strTestEsist
      strGLOBTestEsistQuando = strTestEsistQuando
      strGLOBTestEsistArtTc = strTestEsistArtTc
      strGLOBTipoTestEsist = strTipoTestEsist
      strGLOBTestSottoCosto = strTestSottoCosto
      strGLOBTestSottoCostoModifica = strTestSottoCostoModifica
      strGLOBTestSottoCostoQuando = strTestSottoCostoQuando
      strGLOBTestSottoCostoTipo = strTestSottoCostoTipo
      strGLOBTestSottoCostoZero = strTestSottoCostoZero
      dGLOBTestSottoCostoRicaricoMinimo = dTestSottoCostoRicaricoMinimo
      strGLOBCalcolaSpeseTrasp = strCalcolaSpeseTrasp
      bGLOBCalcolaSpeseTraspSoloNuovi = bCalcolaSpeseTraspSoloNuovi
      bGLOBUsaVettore1perCalcoloSpeseTrasp = bUsaVettore1perCalcoloSpeseTrasp
      strGLOBNoteNuoviDocumenti = strNoteNuoviDocumenti
      bGLOBRiportaNote1CF = bRiportaNote1CF
      bGLOBRiportaNote2CF = bRiportaNote2CF
      nGLOBRiportaNoteDaAnaext = nRiportaNoteDaAnaext
      fGLOBSelContoDiv = fSelContoDiv
      bGLOBChiediDestInTestata = bChiediDestInTestata
      bGLOBRilevaDisponibPerCommessa = bRilevaDisponibPerCommessa
      strGLOBTestEsistCome = strTestEsistCome
      bGLOBTestEsistPerComm = bTestEsistPerComm
      bGLOBUsaMagStockArticolo = bUsaMagStockArticolo
      bGLOBMovimQtaLotti = bMovimQtaLotti
      bGLOBModifColliSuQuant = bModifColliSuQuant
      bGLOBSalvaCondizioniFinali = bSalvaCondizioniFinali
      bGLOBConfermacompilazioneSchedaTrasp = bConfermacompilazioneSchedaTrasp
      bGLOBRiapriDocSuSalva = bRiapriDocSuSalva
      bGLOBDisabilitaMsgPrezzoZero = bDisabilitaMsgPrezzoZero
      bGLOBDisabilitaMsgQuantZero = bDisabilitaMsgQuantZero

      bGLOBApriAnalottiDopoNew = bApriAnalottiDopoNew
      bGLOBAbilitaGenerazLottoSuOrdini = bAbilitaGenerazLottoSuOrdini
      nGLOBInibisciTipiArticoliDiversi = nInibisciTipiArticoliDiversi
      strGLOBDataInibisciArticoliDiversi = strDataInibisciArticoliDiversi

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

  Public Overridable Function LeggiRegistroDoc(ByVal strTipodoc As String) As Boolean
    Try
      If Not lLettoRegistro Then
        '---------------------------------
        'leggo le opzioni di registro globali
        LeggiRegistro()
        lLettoRegistro = True
      End If

      CType(oCleComm, CLELBMENU).AccconfGetBlocchi(strDittaCorrente, strTipodoc, dttAccconf)

      '-------------------------
      'solo se attiva l'opzione di registro e se posso evito di rileggere il registro
      If Not bLeggiOpzioniDoc Then Return True
      If strTipodoc = strLastTiporkRegistroDoc Then Return True
      strLastTiporkRegistroDoc = strTipodoc

      '-------------------------
      strContrFidoInsolinInsOrd = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "Contr_fido_insol_ordine", "N", strTipodoc, strGLOBContrFidoInsolinInsOrd)
      If strContrFidoInsolinInsOrd = "S" Then bSegnalaCreazOrdiniCliFornBloccati = False
      strCalcPesi = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "Calc_pesi_in_doc", "N", strTipodoc, strGLOBCalcPesi)
      strVisMemList1 = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "Vis_mem_list1_ord", "N", strTipodoc, strGLOBVisMemList1)
      nVisMemNumList = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "VisMemNumList", "1", strTipodoc, nGLOBVisMemNumList.ToString))
      strVisNoteConto = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "Vis_note_conto_in_ord", "N", strTipodoc, strGLOBVisNoteConto)
      strVisNoteArti = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "Vis_note_articolo_in_ordini", "0", strTipodoc, strGLOBVisNoteArticolo)
      strVisNoteContoApertura = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "Vis_note_conto_in_apertura", " ", strTipodoc, strGLOBVisNoteContoApertura)
      strTestScomin = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "Test_scorta_min", "N", strTipodoc, strGLOBTestScomin)
      strTestScominCome = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "Test_scorta_min_come", "A", strTipodoc, strGLOBTestScominCome)
      strTestEsist = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "Test_esistenza", "N", strTipodoc, strGLOBTestEsist)
      strTestEsistQuando = UCase$(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "Test_esistenza_quando", " ", strTipodoc, strGLOBTestEsistQuando))
      strTestEsistArtTc = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "TestEsistArticoliTC", "C", strTipodoc, strGLOBTestEsistArtTc) ' C = Colore, T = Taglia
      strTipoTestEsist = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "Tipo_test_esistenza", "O", strTipodoc, strGLOBTipoTestEsist)
      strTestSottoCosto = UCase$(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "TestSottoCosto", "N", strTipodoc, strGLOBTestSottoCosto))
      strTestSottoCostoModifica = UCase$(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "TestSottoCostoModifica", "N", strTipodoc, strGLOBTestSottoCostoModifica))
      strTestSottoCostoQuando = UCase$(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "TestSottoCostoQuando", "A", strTipodoc, strGLOBTestSottoCostoQuando))
      strTestSottoCostoTipo = UCase$(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "TestSottoCostoTipo", "U", strTipodoc, strGLOBTestSottoCostoTipo))
      strTestSottoCostoZero = UCase$(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "TestSottoCostoZero", "S", strTipodoc, strGLOBTestSottoCostoZero))
      dTestSottoCostoRicaricoMinimo = NTSCDec(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "TestSottoCostoRicaricoMinimo", "0", strTipodoc, dGLOBTestSottoCostoRicaricoMinimo.ToString))
      strCalcolaSpeseTrasp = oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONIDOC", ".", "CalcolaSpeseTrasp", "N", strTipodoc, strGLOBCalcolaSpeseTrasp)
      bCalcolaSpeseTraspSoloNuovi = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONIDOC", ".", "CalcolaSpeseTraspSoloNuovi", "0", strTipodoc, IIf(bGLOBCalcolaSpeseTraspSoloNuovi, "-1", "0").ToString))
      bUsaVettore1perCalcoloSpeseTrasp = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONIDOC", ".", "UsaVettore1perCalcoloSpeseTrasp", "N", strTipodoc, IIf(bGLOBUsaVettore1perCalcoloSpeseTrasp, "-1", "0").ToString))
      strNoteNuoviDocumenti = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "NoteNuoviDocumenti", "", strTipodoc, strGLOBNoteNuoviDocumenti)
      bRiportaNote1CF = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "RiportaNote1CF", "0", strTipodoc, IIf(bGLOBRiportaNote1CF, "-1", "0").ToString))
      bRiportaNote2CF = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "RiportaNote2CF", "0", strTipodoc, IIf(bGLOBRiportaNote2CF, "-1", "0").ToString))
      If bGestAnaext = True Then
        nRiportaNoteDaAnaext = CInt(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "RiportaNoteDaAnaext", "0", strTipodoc, "0")))
      End If
      fSelContoDiv = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OpzioniDoc", ".", "SelezionaOAContoDiv", "0", strTipodoc, IIf(fGLOBSelContoDiv, "-1", "0").ToString))
      bChiediDestInTestata = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "ChiediDestInTestata", "0", strTipodoc, IIf(bGLOBChiediDestInTestata, "-1", "0").ToString))
      bRilevaDisponibPerCommessa = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "RilevaDisponibPerCommessa", "0", strTipodoc, IIf(bGLOBRilevaDisponibPerCommessa, "-1", "0").ToString))
      bDisabilitaMsgPrezzoZero = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "DisabilitaMsgPrezzoZero", "0", strTipodoc, IIf(bGLOBDisabilitaMsgPrezzoZero, "-1", "0").ToString))
      bDisabilitaMsgQuantZero = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "DisabilitaMsgQuantZero", "0", strTipodoc, IIf(bGLOBDisabilitaMsgQuantZero, "-1", "0").ToString))
      strTestEsistCome = oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "TestEsistCome", "E", strTipodoc, strGLOBTestEsistCome)
      bTestEsistPerComm = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "TestEsistPerComm", "0", strTipodoc, IIf(bGLOBTestEsistPerComm, "-1", "0").ToString))
      bUsaMagStockArticolo = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "UsaMagStockArticolo", "0", strTipodoc, IIf(bGLOBUsaMagStockArticolo, "-1", "0").ToString))
      bMovimQtaLotti = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "MovimQtaLotti", "0", strTipodoc, IIf(bGLOBMovimQtaLotti, "-1", "0").ToString))  ' NON DOCUMENTARE
      bModifColliSuQuant = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "OpzioniDoc", ".", "ModifColliSuQuant", "0", strTipodoc, IIf(bGLOBModifColliSuQuant, "-1", "0").ToString))
      bSalvaCondizioniFinali = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OpzioniDoc", ".", "SalvaCondizioniFinali", "0", strTipodoc, IIf(bGLOBSalvaCondizioniFinali, "-1", "0").ToString))
      bConfermacompilazioneSchedaTrasp = CBool(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONIDOC", ".", "ConfermacompilazioneSchedaTrasp", "0", strTipodoc, IIf(bGLOBConfermacompilazioneSchedaTrasp, "-1", "0").ToString)))
      bRiapriDocSuSalva = NTSCInt(Val(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OpzioniDoc", ".", "RiapriDocSuSalva", "0", strTipodoc, bGLOBRiapriDocSuSalva.ToString)))

      If (CBool(bRiportaNote1CF) = True Or CBool(bRiportaNote2CF) = True) And nRiportaNoteDaAnaext > 0 Then nRiportaNoteDaAnaext = 0

      bAbilitaGenerazLottoSuOrdini = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONIDOC", ".", "AbilitaGenerazLottoSuOrdini", "0", strTipodoc, IIf(bGLOBAbilitaGenerazLottoSuOrdini, "-1", "0").ToString))
      bApriAnalottiDopoNew = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OpzioniDoc", ".", "ApriAnalottiDopoNew", "0", strTipodoc, IIf(bGLOBApriAnalottiDopoNew, "-1", "0").ToString))
      nInibisciTipiArticoliDiversi = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONIDOC", ".", "InibisciTipiArticoliDiversi", "0", strTipodoc, nGLOBInibisciTipiArticoliDiversi.ToString))
      strDataInibisciArticoliDiversi = NTSCStr(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONIDOC", ".", "DataInibisciArticoliDiversi", "", strTipodoc, strGLOBDataInibisciArticoliDiversi))

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

  Public Overrides Function ResetVar() As Boolean
    Try
      '--------------------------
      'azzero le variabili utilizzate d a'nuovo da preventivo'
      bNuovoDaPrev = False
      strTipoPrev = ""
      nAnnoPrev = 0
      strSeriePrev = ""
      lNumPrev = 0

      Return MyBase.ResetVar()

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


  Public Overridable Function OkTestata() As Boolean
    Try
      '----------------------------
      'inseriti diversi test che in vb6 erano in okTestata
      If NTSCInt(dttET.Rows(0)!et_conto) = 0 Or NTSCInt(dttET.Rows(0)!et_magaz) = 0 Or NTSCInt(dttET.Rows(0)!et_tipobf) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222117187500, "Impostare prima il conto cliente/fornitore, il tipo Bolla/fattura ed il codice magazzino")))
        Return False
      ElseIf bNew And NTSCInt(dttET.Rows(0)!et_codese) <> 0 And dttET.Rows(0)!et_scorpo.ToString = "S" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523167576927500, "Un documento in esenzione non può essere di tipo IVA inclusa (scorporo). Il flag 'scorporo' verrà deselezionato.")))
        dttET.Rows(0)!et_scorpo = "N"
      ElseIf NTSCInt(dttET.Rows(0)!et_valuta) <> 0 And NTSCDec(dttET.Rows(0)!et_cambio) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523168912287500, "Il cambio deve essere diverso da 0 quando è indicata la valuta")))
        Return False
      ElseIf dttET.Rows(0)!et_tipork.ToString = "X" And NTSCInt(dttET.Rows(0)!et_magaz2) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523169502591500, "Codice magazzino 2 DI TESTATA obbligatorio per gli Impegni di trasferimento")))
        Return False
      ElseIf dttET.Rows(0)!et_tipork.ToString = "H" And NTSCInt(dttET.Rows(0)!et_magimp) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523170290859500, "Codice magazzino impegni DI TESTATA obbligatorio per gli Ordini di produzione")))
        Return False
      ElseIf bNew And dttET.Rows(0)!et_tipork.ToString = "H" And dttET.Rows(0)!et_scorpo.ToString = "S" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523170610659500, "Impossibile emettere documenti di tipo 'Ordine di produzione' di tipo iva compresa (scorporo). Il flag 'scorporo' verrà deselezionato.")))
        dttET.Rows(0)!et_scorpo = "N"
      ElseIf bNew And dttET.Rows(0)!et_tipork.ToString = "H" And bTerzista = False And NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523173669033500, "Impossibile emettere documenti di tipo 'Ordine di produzione' INTERNI in valuta")))
        Return False
      ElseIf bNew And NTSCInt(dttET.Rows(0)!et_conto) = lConclpriv Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128763308167116000, "Impossibile emettere ordini a clienti generici privati")))
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

#Region "NUOVO/APRI/DUPLICA"
  Public Overridable Function NuovoOrdine(ByVal strDitta As String, ByVal strTipoDoc As String, _
                           ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    Dim i As Integer = 0
    Try
      If Not CType(oCleComm, CLELBMENU).GPVDiscCheckSerieAbilitata(strDitta, strSerie) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129957197706875667, "La serie '|" & strSerie & "|' non può essere usata in base ai vincoli di Gestione Punti Vendita in Modalità Disconnessa.")))
        Return False
      End If

      '----------------------------------------
      'verifico se esiste già un ordine con le stesse caratteristiche
      If oCldGsor.EsisteOrdine(strDitta, strTipoDoc, nAnno, strSerie, lNumdoc) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128763308224680000, "Esiste già un documento con le stesse caratteristiche di quello che si desidera creare.")))
        Return False
      End If

      bDocEmesso = False
      Select Case strTipoDoc
        Case "R", "#", "Q", "Y", "X", "V" : bDocEmesso = True
      End Select

      LeggiRegistroDoc(strTipoDoc)

      If Not CheckSoloSerieInTRKTPBF(strDitta, strTipoDoc, _
                               strSerie) Then
        Return False
      End If

      If bModTCO And bAttivaRecentTCO Then
        nAnnoTCO = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "RECENT", ".", "AnnoTCO", "0", " ", "0"))
        nCodstagTCO = NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "RECENT", ".", "CodstagTCO", "0", " ", "0"))
      End If

      '----------------------------------------
      MyBase.NuovoDocumento(strDitta, strTipoDoc, nAnno, strSerie, lNumdoc)

      If Not dttET.Columns.Contains("xx_organig") Then dttET.Columns.Add("xx_organig", GetType(String))
      '----------------------------------------
      'lo chiamo solo per settare dtControllaConcorrenzaOggettiDataLock
      DocumentLockAdd("", 0, "", 0, 0, True)

      If NTSCInt(dttET.Rows(0)!et_numdoc) <> LegNuma(dttET.Rows(0)!et_tipork.ToString, dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_anno)) Then
        bProgrCambiato = True
      Else
        bProgrCambiato = False
      End If

      If strNoteNuoviDocumenti.Trim <> "" Then
        dttET.Rows(0)!et_note = strNoteNuoviDocumenti
      End If

      If bModCCC Then dttET.Rows(0)!et_orpromo = (Now.Hour * 100 + Now.Minute) / 100
      '--------------------------------------------------------------------------------------------------------------
      AggiungiColonneUnbound(dsShared)
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
  Public Overridable Function NuovoDocDaImportExport(ByVal strDitta As String, ByVal strTipoDoc As String, _
                           ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                           ByVal bRicalcolaValoriRiga As Boolean) As Boolean
    '---------------------------------
    'questa funzione serve nel seguente caso:
    'tramite import/export ho scritto per i cavoli miei TESTORD e MOVORD, 
    'ora voglio ricalcolare i valori di riga, ricalcolare i totli documento 
    'e risalvare per aggiornare keyord, lotcpro, ecc.

    'ATTENZIONE:DURANTE IL SALVATAGGIO VIENE ESEGUITA LA ROUTINE TESTPRESALVA CHE MANDA DEI MESSAGGI TRAMITE THROWREMOTEEVENT
    'SE CHI CHIAMA E' UNA UI TUTTO OK, SE INVECE E' UN ENTITY O I MESSAGGI NON VERRANNO VISUALIZZATI, OPPURE (MEGLIO)
    'CON LA RIGA 'AddHandler oCleGsor.RemoteEvent, AddressOf GestisciEventiEntity' L'ENTITY CHE ISTANZIA beorgsor
    'RICEVERA' I MESSAGGI CHE DOVRANNO ESSERE TRADOTTI!!!!!!


    'per poter utilizzare questo comando occorre inserire le seguenti righe di codice da una UI, mentre se da un entity vedi BEORGNOR.InitExt

    '******************* CREAZIONE DELL'ENTYTY *******************
    'Dim strErr As String = ""
    'Dim oTmp As Object = Nothing
    'If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORGSOR", "BEORGSOR", oTmp, strErr, False, "", "") = False Then
    '  oApp.MgBoxErr(oApp.Tr(Me, 127791222114531250, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
    '  Return False
    'End If
    'oCleGsor = CType(oTmp, CLEORGSOR)
    ''------------------------------------------------
    'bRemoting = Menu.Remoting("BNORGSOR", strRemoteServer, strRemotePort)
    'AddHandler oCleGsor.RemoteEvent, AddressOf GestisciEventiEntity
    'If oCleGsor.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    'If Not oCleGsor.InitExt() Then Return False
    'If oCleGsor.bModuloCRM Then
    '  oCleGsor.bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, oCleGsor.bAmm, oCleGsor.strAccvis, oCleGsor.strAccmod, oCleGsor.strRegvis, oCleGsor.strRegmod)
    'End If


    '******************* CARICO IL DOCUMENTO *******************
    'If Not oCleGsor.NuovoDocDaImportExport(strDitta, strTipoDoc, nAnno, strSerie, lNumdoc, True) Then Return False

    '******************* SALVO IL DOCUMENTO ********************
    'If Not oCleGsor.SalvaOrdine("N") Then Return False         'attenzione, il parametro "N" è fisso!!!!

    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim lNumTmp As Integer = 0
    Try

      ResetVar()

      '---------------------------
      'leggo il documento (prima legge le opzioni di registro)
      If Not ApriOrdine(strDitta, False, strTipoDoc, nAnno, strSerie, lNumdoc, ds) Then Return False
      If ds.Tables("TESTA").Rows.Count = 0 Then Return False

      If Not OkTestata() Then Return False

      bNew = True
      bHasChangesET = True
      bNuovoDaImportExport = True

      '---------------------------
      'verifico se devo aggiornare tabnuma al salvataggio
      bProgrCambiato = False
      lNumTmp = LegNuma(IIf(strTipoDoc = "V", "VV", strTipoDoc).ToString, strSerie, nAnno)
      If lNumTmp <> NTSCInt(ds.Tables("TESTA").Rows(0)!et_numdoc) Then bProgrCambiato = True

      '---------------------------
      'se necessario ricalcolo il valore delle righe
      If bRicalcolaValoriRiga Then
        If dttET.Rows(0)!et_tipork.ToString = "H" Then
          'ricalcolo i valori di riga degli impegni collegati e delle lavorazioni
          For i = 0 To dttECIMP.Rows.Count - 1
            SettaValoriRiga(dttECIMP.Rows(i))
          Next
        End If
        For i = 0 To dttEC.Rows.Count - 1
          If dttET.Rows(0)!et_tipork.ToString = "H" Then ValorizzaProduzione(dttEC.Rows(i))
          SettaValoriRiga(dttEC.Rows(i))
        Next
      End If
      dsShared.AcceptChanges()    'forse per ogni riga andrebbe fatta la RecordSalva, sia per il corpo che per gli impegni/lavoraz/assris...

      'attenzione: al salvataggio devo prima cancellare le righe di testord/movord vecchie, poi reinserirle
      'altrimenti non saprei come fare per aggiornare i totali!!!!!

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

  Public Overridable Function ApriOrdine(ByVal strDitta As String, ByVal bNew As Boolean, ByVal strTipoDoc As String, _
                           ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                           ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim bOk As Boolean = False
    Dim strTmp As String = ""
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim bRetail As Boolean = False
    Dim strMsg As String = ""
    Try
      '--------------------------------------------------------------------------------------------------------------
      bCaricamentoDocumentoOrdine = True
      '--------------------------------------------------------------------------------------------------------------
      If lNumdoc <> 0 Then LeggiRegistroDoc(strTipoDoc)

      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      '--------------------------------------
      'se è un documento fatto con retail esco
      If Not bDocDaRetail Then
        If (bModRET Or bModGPV) And lNumdoc <> 0 Then
          If oCldGsor.IsDocRetail(strDitta, strTipoDoc, nAnno, strSerie, lNumdoc) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523795520642000, "Attenzione!" & vbCrLf & _
                            "L'ordine/impegno selezionato è stato creato con il programma" & vbCrLf & _
                            "'Gestione unificata documenti' o 'Gestione Punto Vendita'." & vbCrLf & _
                            "Non sarà possibile salvare e/o cancellare il documento." & vbCrLf & _
                            "Alcune informazione specifiche del retail non saranno visualizzate")))
            bRetail = True
          End If
        End If
      End If

      '--------------------------------
      'solo all'avvio del programma (ovvero numdoc = 0) memorizzo se in barcode o codarfo c'è qualche cosa
      If lNumdoc = 0 Then
        oCldDocu.CheckBarcodeCodarfo(strDittaCorrente, bTableBarcodeEmpty, bTableCodarfoEmpty)
      End If

      dReturn = oCldGsor.ApriOrdine(strDitta, bNew, strTipoDoc, nAnno, strSerie, lNumdoc, ds, bModTCO, bModPM, nOrdrig)

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      SetDefaultValueTestord(ds)
      SetDefaultValueMovord(ds, False)
      SetDefaultValueMovord(ds, True)
      SetDefaultValueAssris(ds)
      SetDefaultValueAttivit(ds)
      If bModTCO Then
        SetDefaultValueMovordTC(ds)
        SetDefaultValueMovordImpTC(ds)
      End If
      If bModPM Then SetDefaultValueProesec(ds)
      oCldGsor.SetTableDefaultValueFromDB("SCHETRASP", ds)

      '--------------------------------------
      'generalizzo i datatable
      If Not CambiaPrefissoCampiDatatable(ds.Tables("TESTORD"), "TESTORD", "TESTA", "td_", "et_") Then Return False
      If Not CambiaPrefissoCampiDatatable(ds.Tables("MOVORD"), "MOVORD", "CORPO", "mo_", "ec_") Then Return False
      If Not CambiaPrefissoCampiDatatable(ds.Tables("MOVORDIMP"), "MOVORDIMP", "CORPOIMP", "mo_", "ec_") Then Return False
      If bModTCO Then
        If Not CambiaPrefissoCampiDatatable(ds.Tables("MOVORDTC"), "MOVORDTC", "CORPOTC", "mo_", "ec_") Then Return False
        If Not CambiaPrefissoCampiDatatable(ds.Tables("MOVORDIMPTC"), "MOVORDIMPTC", "CORPOIMPTC", "mo_", "ec_") Then Return False
      End If
      If Not CambiaPrefissoCampiDatatable(ds.Tables("SCHETRASP"), "SCHETRASP", "SCHETRASP", "sct_", "et_") Then Return False

      '--------------------------------------
      'per standardizzare i nomi delle colonne ...
      ds.Tables("TESTA").Columns("et_datord").ColumnName = "et_datdoc"
      ds.Tables("TESTA").Columns("et_numord").ColumnName = "et_numdoc"
      ds.Tables("CORPO").Columns("ec_numord").ColumnName = "ec_numdoc"
      ds.Tables("CORPOIMP").Columns("ec_numord").ColumnName = "ec_numdoc"
      If bModTCO Then
        ds.Tables("CORPOTC").Columns("ec_numord").ColumnName = "ec_numdoc"
        ds.Tables("CORPOIMPTC").Columns("ec_numord").ColumnName = "ec_numdoc"
      End If

      If nInibisciTipiArticoliDiversi = 1 Then
        Dim dttTmp As New DataTable
        If Not ds.Tables("CORPO").Columns.Contains("xxo_art62") Then ds.Tables("CORPO").Columns.Add("xxo_art62")
        If Not ds.Tables("CORPOIMP").Columns.Contains("xxo_art62") Then ds.Tables("CORPOIMP").Columns.Add("xxo_art62")

        'Scorre le righe del documento per aggiungere il tipo articolo
        For i = 0 To ds.Tables("CORPO").Rows.Count - 1
          oCldGsor.ValCodiceDb(NTSCStr(ds.Tables("CORPO").Rows(i)!ec_codart), strDittaCorrente, "ARTICO", "S", , dttTmp)
          If dttTmp.Rows.Count > 0 Then ds.Tables("CORPO").Rows(i)!xxo_art62 = dttTmp.Rows(0)(strNomeCampoArtico)
        Next
        For i = 0 To ds.Tables("CORPOIMP").Rows.Count - 1
          oCldGsor.ValCodiceDb(NTSCStr(ds.Tables("CORPOIMP").Rows(i)!ec_codart), strDittaCorrente, "ARTICO", "S", , dttTmp)
          If dttTmp.Rows.Count > 0 Then ds.Tables("CORPOIMP").Rows(i)!xxo_art62 = dttTmp.Rows(0)(strNomeCampoArtico)
        Next
      End If

      '--------------------------------------
      'compilo la descrizione del cli/forn
      If ds.Tables("TESTA").Rows.Count > 0 Then
        lContoCF = NTSCInt(ds.Tables("TESTA").Rows(0)!et_conto)
        bOk = oCldGsor.ValCodiceDb(ds.Tables("TESTA").Rows(0)!et_conto.ToString, strDittaCorrente, "ANAGRACF", "N", strTmp, dttEt_conto)
        If bOk Then
          If dttEt_conto.Rows.Count > 0 Then
            ds.Tables("TESTA").Rows(0)!xx_tipo = dttEt_conto.Rows(0)!an_tipo.ToString
            ds.Tables("TESTA").Rows(0)!xx_conto = dttEt_conto.Rows(0)!an_descr1.ToString & " " & _
                                dttEt_conto.Rows(0)!an_descr2.ToString & vbCrLf & _
                                dttEt_conto.Rows(0)!an_indir.ToString & vbCrLf & _
                                dttEt_conto.Rows(0)!an_cap.ToString & " " & _
                                dttEt_conto.Rows(0)!an_citta.ToString & " (" & _
                                dttEt_conto.Rows(0)!an_prov.ToString & ")   (" & _
                                dttEt_conto.Rows(0)!an_stato.ToString & ")"
            lAgControp = NTSCInt(dttEt_conto.Rows(0)!an_agcontrop)
            nClscan = NTSCInt(dttEt_conto.Rows(0)!an_clascon)
            nClpran = NTSCInt(dttEt_conto.Rows(0)!an_claprov)
            nTabling = NTSCInt(dttEt_conto.Rows(0)!an_codling)
          End If
        End If
      End If    'If ds.Tables("TESTA").Rows.Count > 0 Then

      Select Case strTipoDoc
        Case "R", "Q", "Y", "X", "V", "#" : bDocEmesso = True
        Case Else : bDocEmesso = False
      End Select

      bNew = False

      dtrTmp = ds.Tables("CORPO").Select(Nothing, "ec_riga DESC")

      If dReturn = False Then Return False

      If ApriDocumento(ds) = False Then Return False
      bDocRetail = bRetail


      '------------------------------
      If dttET.Rows.Count > 0 Then
        If dttET.Rows(0)!et_tipork.ToString = "H" Then
          dtTimeStampDocY = oCldGsor.GetTimeStampTestord(strDittaCorrente, "Y", _
                                   NTSCInt(dttET.Rows(0)!et_anno), NTSCStr(dttET.Rows(0)!et_serie), _
                                   NTSCInt(dttET.Rows(0)!et_numdoc))
        End If
      End If

      '------------------------------
      'se è la lettura iniziale in form_load non faccio altro ed esco
      If lNumdoc = 0 Then
        ds.AcceptChanges()
        Return True
      End If

      '------------------------------
      If dttET.Rows.Count > 0 Then      'solo se ho trovato un documento ...

        If Not Accconf_CheckVis() Then Return False

        'per compatibilità con VB6
        If NTSCStr(dttET.Rows(0)!et_datapag) = "" Then dttET.Rows(0)!et_datapag = dttET.Rows(0)!et_datdoc

        If dttET.Rows(0)!et_tipork.ToString = "O" Or dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#" Then
          'Elimina eventuali multiple evasioni a saldo della stessa riga
          If Not bNew Then
            If Not ApriOrdineAggUnicaEvasioneSaldo(strTipoDoc, nAnno, strSerie, lNumdoc) Then Return False
          End If
        End If

        '------------------------
        'Su opzione abilitata propone già la quantità assegnata
        If bProponiQtaDaAss And (dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#") Then
          dtrTmp = dttEC.Select("ec_flevas <> 'S' AND ec_flevapre <> 'S' AND (ec_quant - ec_quaeva - ec_quapre) > 0")
          For i = 0 To dtrTmp.Length - 1
            dttEC.Rows(i)!xxo_qtadaass = Math.Round((NTSCDec(dttEC.Rows(i)!ec_quant) - NTSCDec(dttEC.Rows(i)!ec_quaeva) - NTSCDec(dttEC.Rows(i)!ec_quapre)), 3)
          Next
        End If

        '------------------------
        'carico il project management (solo in modifica documento)
        If bModPM Then FileApriCaricaPM()

        '------------------------
        'se serve verifico se qualche altro utente ha il documento aperto
        DocumentLockCheck(dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), 0, strMsg)
        If strMsg.Trim <> "" Then
          If nControllaConcorrenzaOggetti = -1 Then
            'avviso
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & vbCrLf & _
                              oApp.Tr(Me, 129176064247451172, "Potrebbe non essere possibile salvare il documento in apertura se " & _
                              "il primo operatore apporterà delle modifiche." & vbCrLf & _
                              "Diversamente se modifichiamo il documento sarà il primo operatore a perdere il lavoro svolto.")))
          End If
          If nControllaConcorrenzaOggetti = 1 Then
            'blocco
            bDocumentLockNoSave = True
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & vbCrLf & _
                              oApp.Tr(Me, 129176039613408203, "Non sarà possibile salvare il documento in apertura.")))
          End If
        End If
        '------------------------
        'se serve memorizzo che l'utente ha il doc aperto (solo se non sono bloccato. se sono bloccato non ha senso, visto che alla fine non potrò salvare ...
        If bDocumentLockNoSave = True Then
          'non aggiungo il lock. tanto non potrò salvare il documento ...
        Else
          DocumentLockAdd(dttET.Rows(0)!et_tipork.ToString, NTSCInt(dttET.Rows(0)!et_anno), dttET.Rows(0)!et_serie.ToString, NTSCInt(dttET.Rows(0)!et_numdoc), 0, True)
        End If
      End If    ' If dttET.Rows.Count > 0 Then          'solo se ho trovato un documento ...
      '--------------------------------------------------------------------------------------------------------------
      '--- In modifica di un ORDINE/IMPEGNO, prima dell'apertura, effettua ulteriori controlli (prima erano nel BN...)
      '--------------------------------------------------------------------------------------------------------------
      TestaBlocchi(bNew)
      '--------------------------------------------------------------------------------------------------------------

      '------------------------------
      'se c'è il collegamento con NETPRO controllo che il documento non sia in evasione ed eventualmente avviso
      NetProCheckOrdModifCanc(True)

      For Each dtrRow As DataRow In dsShared.Tables("CORPO").Rows
        ColoraCelle(dtrRow)
      Next

      If bNew = False AndAlso bModCCC AndAlso bDocDaRetail = False Then
        If ds.Tables("TESTA").Rows.Count > 0 Then
          'Per ogni contratto presente ricarica le descrizioni e altre informazioni
          If NTSCInt(dttET.Rows(0)!et_codtes) <> 0 Then ValidaContratto(dttET.Rows(0), NTSCInt(dttET.Rows(0)!et_codtes), "et_codtes", "xx_codtes")
          If NTSCInt(dttET.Rows(0)!et_codtes2) <> 0 Then ValidaContratto(dttET.Rows(0), NTSCInt(dttET.Rows(0)!et_codtes2), "et_codtes2", "xx_codtes2")
          If NTSCInt(dttET.Rows(0)!et_codtes3) <> 0 Then ValidaContratto(dttET.Rows(0), NTSCInt(dttET.Rows(0)!et_codtes3), "et_codtes3", "xx_codtes3")
          If NTSCInt(dttET.Rows(0)!et_codtes4) <> 0 Then ValidaContratto(dttET.Rows(0), NTSCInt(dttET.Rows(0)!et_codtes4), "et_codtes4", "xx_codtes4")
          If NTSCInt(dttET.Rows(0)!et_codtes5) <> 0 Then ValidaContratto(dttET.Rows(0), NTSCInt(dttET.Rows(0)!et_codtes5), "et_codtes5", "xx_codtes5")
          If NTSCInt(dttET.Rows(0)!et_codtes6) <> 0 Then ValidaContratto(dttET.Rows(0), NTSCInt(dttET.Rows(0)!et_codtes6), "et_codtes6", "xx_codtes6")
          CaricaPromozioni()
          ColoraGrigliaPromozioni()
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      AggiungiColonneUnbound(ds)
      '--------------------------------------------------------------------------------------------------------------
      bHasChangesET = False 'imposto che il documento non è cambiato

      ds.AcceptChanges()        'confermo tutto
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
      bCaricamentoDocumentoOrdine = False
    End Try
  End Function
  Public Overridable Function ApriOrdineSvuotaEvasoPrenotatoPmOfa(ByRef ds As DataSet, ByVal strTipoDoc As String) As Boolean
    'Svuota i flag evasione e prenotazione (eventualmente anche su impegni collegati)
    'svuota i dati del P.M.
    'svuota i riferimenti a IMP e ORD aperti
    Dim i As Integer = 0
    Try
      For Each dtrT As DataRow In ds.Tables("CORPO").Rows
        dtrT!ec_colpre = 0
        dtrT!ec_coleva = 0
        dtrT!ec_quapre = 0
        dtrT!ec_quaeva = 0
        dtrT!ec_flevas = "C"
        dtrT!ec_flevapre = "C"
        dtrT!ec_pmtaskid = 0
        dtrT!ec_pmsalcon = "C"
        dtrT!ec_pmqtadis = 0
        dtrT!ec_pmvaldis = 0
        dtrT!xxo_pmqtadisda = 0
        dtrT!xxo_pmvaldisda = 0
        dtrT!xxo_pmqtares = 0
        dtrT!xxo_pmvalres = 0
        dtrT!xxo_pmqtarim = 0
        dtrT!xxo_pmvalrim = 0
        dtrT!xxo_destask = " "
        dtrT!xxo_darave = " "
        dtrT!xxo_prevgrup = "N"
        dtrT!xxo_gestcost = "1"
        dtrT!ec_oatipo = DBNull.Value
        dtrT!ec_oaanno = 0
        dtrT!ec_oaserie = DBNull.Value
        dtrT!ec_oanum = 0
        dtrT!ec_oariga = 0
        dtrT!ec_oasalcon = "C"
        dtrT!ec_oaqtadis = 0
        dtrT!ec_oacoldis = 0
        dtrT!ec_oavaldis = 0
        dtrT!ec_valore = dtrT!ec_valoremm
        dtrT!ec_oqtipo = DBNull.Value
        dtrT!ec_oqanno = 0
        dtrT!ec_oqserie = DBNull.Value
        dtrT!ec_oqnum = 0
        dtrT!ec_oqriga = 0
        dtrT!ec_oqsalcon = "C"
      Next
      If strTipoDoc = "H" Then
        For Each dtrT As DataRow In ds.Tables("CORPOIMP").Rows
          dtrT!ec_colpre = 0
          dtrT!ec_coleva = 0
          dtrT!ec_quapre = 0
          dtrT!ec_quaeva = 0
          dtrT!ec_flevas = "C"
          dtrT!ec_flevapre = "C"
          dtrT!ec_pmtaskid = 0
          dtrT!ec_pmsalcon = "C"
          dtrT!ec_pmqtadis = 0
          dtrT!ec_pmvaldis = 0
          dtrT!xxo_pmqtadisda = 0
          dtrT!xxo_pmvaldisda = 0
          dtrT!xxo_pmqtares = 0
          dtrT!xxo_pmvalres = 0
          dtrT!xxo_pmqtarim = 0
          dtrT!xxo_pmvalrim = 0
          dtrT!xxo_destask = " "
          dtrT!xxo_darave = " "
          dtrT!xxo_prevgrup = "N"
          dtrT!xxo_gestcost = "1"
          dtrT!ec_valore = dtrT!ec_valoremm
        Next
        For Each dtrT As DataRow In ds.Tables("ATTIVIT").Rows
          dtrT!at_tempattees = 0
          dtrT!at_tempesees = 0
          dtrT!at_flevas = "C"
          dtrT!at_qtaes = 0
        Next
      End If

      If bModTCO Then
        For Each dtrT As DataRow In ds.Tables("CORPOTC").Rows
          For i = 1 To 24
            dtrT("ec_quaeva" & i.ToString.PadLeft(2, "0"c)) = 0
            dtrT("ec_quapre" & i.ToString.PadLeft(2, "0"c)) = 0
          Next
        Next
        If strTipoDoc = "H" Then
          For Each dtrT As DataRow In ds.Tables("CORPOIMPTC").Rows
            For i = 1 To 24
              dtrT("ec_quaeva" & i.ToString.PadLeft(2, "0"c)) = 0
              dtrT("ec_quapre" & i.ToString.PadLeft(2, "0"c)) = 0
            Next
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
  Public Overridable Function ApriOrdineAggUnicaEvasioneSaldo(ByVal strTipoDoc As String, ByVal nAnno As Integer, _
                                                              ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean
    '----------------------------------
    'Elimina eventuali multiple evasioni a saldo della stessa riga
    Dim dttTmp As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim dttOaf() As DataRow = Nothing
    Dim i As Integer = 0
    Dim l As Integer = 0

    Try
      '----------------------------------
      'elenco delle righe del documento collegate ad IMP / ORD aperti
      If dttEC.Select("ec_oanum <> 0").Length = 0 Then Return True

      '----------------------------------
      'ottengo l'elenco degli oa_salcon diversi da mo_flevas di Imp cli aperti e li correggo come su imp cli aperti
      If Not oCldGsor.ApriOrdineGetOfaSalcon(strDittaCorrente, strTipoDoc, nAnno, strSerie, lNumdoc, dttTmp) Then Return False

      For i = 0 To dttTmp.Rows.Count - 1
        dtrT = dttEC.Select("ec_riga = " & NTSCInt(dttTmp.Rows(i)!mo_riga).ToString)
        If dtrT.Length > 0 Then dtrT(0)!ec_oasalcon = NTSCStr(dttTmp.Rows(i)!mo_flevas)
      Next

      '----------------------------------
      'Elimina le evasione a saldo doppie sulla stessa riga di ordine
      dttOaf = dttEC.Select("ec_oanum <> 0 AND ec_oasalcon = 'S'")
      For i = 0 To dttOaf.Length - 1
        dtrT = dttEC.Select("ec_oatipo = " & CStrSQL(dttOaf(i)!ec_oatipo) & _
                            " AND ec_oaanno = " & dttOaf(i)!ec_oaanno.ToString & _
                            " AND ec_oaserie = " & CStrSQL(dttOaf(i)!ec_oaserie) & _
                            " AND ec_oanum = " & dttOaf(i)!ec_oanum.ToString & _
                            " AND ec_oariga =" & dttOaf(i)!ec_oariga.ToString, "ec_riga")
        For l = 1 To dtrT.Length - 1
          dtrT(l)!ec_oasalcon = "C"
        Next
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

  Public Overridable Sub SetDefaultValueTestord(ByRef ds As DataSet)
    Try

      '--------------------------------------------------------------
      'imposto i valori di default di movord: con la riga che seguen prendo prima i valori dal database
      oCldGsor.SetTableDefaultValueFromDB("TESTORD", ds)
      'ora imposto i valori di default diversi da quelli impostati nel database
      With ds.Tables("TESTORD")
        .Columns("codditt").DefaultValue = strDittaCorrente
        .Columns("td_tipork").DefaultValue = "R"
        .Columns("td_serie").DefaultValue = " "
        .Columns("td_datord").DefaultValue = DateTime.Now.ToString
        .Columns("td_riferim").DefaultValue = DBNull.Value
        .Columns("td_datcons").DefaultValue = DateTime.Now.ToString
        .Columns("td_datapag").DefaultValue = DateTime.Now.ToString
        .Columns("td_scorpo").DefaultValue = "N"
        .Columns("td_acuradi").DefaultValue = " "
        .Columns("td_note").DefaultValue = ""
        .Columns("td_flevas").DefaultValue = "N"
        .Columns("td_flstam").DefaultValue = "N"
        .Columns("td_porto").DefaultValue = ""
        .Columns("td_aspetto").DefaultValue = ""
        .Columns("td_alfpar").DefaultValue = " "
        .Columns("td_datpar").DefaultValue = DBNull.Value
        .Columns("td_flspinc").DefaultValue = "N"
        .Columns("td_flboll").DefaultValue = "N"
        .Columns("td_subcommeca").DefaultValue = " "
        .Columns("td_blocco").DefaultValue = "N"
        .Columns("td_confermato").DefaultValue = "N"
        .Columns("td_rilasciato").DefaultValue = "N"
        .Columns("td_aperto").DefaultValue = "N"
        .Columns("td_banc1").DefaultValue = ""
        .Columns("td_banc2").DefaultValue = ""
        .Columns("td_sospeso").DefaultValue = "N"
        .Columns("td_soloasa").DefaultValue = "N"
        .Columns("td_ultagg").DefaultValue = IntSetDate("01/01/1900")
        .Columns("td_opnome").DefaultValue = oApp.User.Nome
        .Columns("td_codstag").DefaultValue = 0
        .Columns("td_codcfam").DefaultValue = " "
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
  Public Overridable Sub SetDefaultValueMovord(ByRef ds As DataSet, ByVal bMovordImp As Boolean)
    Try
      '--------------------------------------------------------------
      'imposto i valori di default di movord: con la riga che seguen prendo prima i valori dal database
      oCldGsor.SetTableDefaultValueFromDB("MOVORD", IIf(bMovordImp = False, "MOVORD", "MOVORDIMP").ToString(), ds)
      'ora imposto i valori di default diversi da quelli impostati nel database
      With ds.Tables(IIf(bMovordImp = False, "MOVORD", "MOVORDIMP").ToString())
        .Columns("codditt").DefaultValue = strDittaCorrente
        .Columns("mo_tipork").DefaultValue = "R"
        .Columns("mo_serie").DefaultValue = " "
        .Columns("mo_codart").DefaultValue = " "
        .Columns("mo_datcons").DefaultValue = IntSetDate("01/01/1900")
        .Columns("mo_unmis").DefaultValue = ""
        .Columns("mo_descr").DefaultValue = ""
        .Columns("mo_flevas").DefaultValue = "C"
        .Columns("mo_flevapre").DefaultValue = "C"
        .Columns("mo_note").DefaultValue = ""
        .Columns("mo_stasino").DefaultValue = "S"
        .Columns("mo_tiporkor").DefaultValue = ""
        .Columns("mo_serieor").DefaultValue = ""
        .Columns("mo_codcfam").DefaultValue = " "
        .Columns("mo_subcommeca").DefaultValue = " "
        .Columns("mo_desint").DefaultValue = ""
        .Columns("mo_ump").DefaultValue = " "
        .Columns("mo_confermato").DefaultValue = "N"
        .Columns("mo_lotto").DefaultValue = 0
        .Columns("mo_rilasciato").DefaultValue = "N"
        .Columns("mo_aperto").DefaultValue = "N"
        .Columns("mo_ricimp").DefaultValue = "N"
        .Columns("mo_datconsor").DefaultValue = IntSetDate("01/01/1900")
        .Columns("mo_ultagg").DefaultValue = IntSetDate("01/01/1900")
        .Columns("mo_perqta").DefaultValue = 1
        .Columns("mo_flkit").DefaultValue = " "
        .Columns("mo_oatipo").DefaultValue = ""
        .Columns("mo_oaserie").DefaultValue = ""
        .Columns("mo_oasalcon").DefaultValue = "C"
        .Columns("mo_flelab").DefaultValue = "N"
        .Columns("mo_flcom").DefaultValue = "N"
        .Columns("mo_flprznet").DefaultValue = "N"
        .Columns("mo_flforf").DefaultValue = "N"
        .Columns("mo_matric").DefaultValue = " "
        .Columns("mo_umprz").DefaultValue = " "
        .Columns("mo_pmsalcon").DefaultValue = "C"
        .Columns("mo_oqtipo").DefaultValue = ""
        .Columns("mo_oqserie").DefaultValue = ""
        .Columns("mo_oqsalcon").DefaultValue = "C"
        .Columns("mo_ubicaz").DefaultValue = " "
        .Columns("mo_codpf").DefaultValue = ""
        .Columns("mo_dtvaldb").DefaultValue = IntSetDate("01/01/1900")
        .Columns("mo_autoriz").DefaultValue = "N"
        .Columns("xxo_flevasass").DefaultValue = "C"
        .Columns("xxo_qtadaass").DefaultValue = 0
        .Columns("mo_datini").DefaultValue = DateTime.Now.ToShortDateString
        .Columns("mo_datfin").DefaultValue = DateTime.Now.ToShortDateString

        .Columns("xxo_pmqtadisda").DefaultValue = 0
        .Columns("xxo_pmvaldisda").DefaultValue = 0
        .Columns("xxo_pmqtares").DefaultValue = 0
        .Columns("xxo_pmvalres").DefaultValue = 0
        .Columns("xxo_pmqtarim").DefaultValue = 0
        .Columns("xxo_pmvalrim").DefaultValue = 0
        .Columns("xxo_pmtaskid").DefaultValue = ""
        .Columns("xxo_darave").DefaultValue = " "
        .Columns("xxo_prevgrup").DefaultValue = ""
        .Columns("xxo_gestcost").DefaultValue = ""
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
  Public Overridable Sub SetDefaultValueAssris(ByRef ds As DataSet)
    Try

      '--------------------------------------------------------------
      'imposto i valori di default di movord: con la riga che seguen prendo prima i valori dal database
      oCldGsor.SetTableDefaultValueFromDB("ASSRIS", ds)
      'ora imposto i valori di default diversi da quelli impostati nel database
      With ds.Tables("ASSRIS")
        .Columns("codditt").DefaultValue = strDittaCorrente
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
  Public Overridable Sub SetDefaultValueAttivit(ByRef ds As DataSet)
    Try

      '--------------------------------------------------------------
      'imposto i valori di default di movord: con la riga che seguen prendo prima i valori dal database
      oCldGsor.SetTableDefaultValueFromDB("ATTIVIT", ds)
      'ora imposto i valori di default diversi da quelli impostati nel database
      With ds.Tables("ATTIVIT")
        .Columns("codditt").DefaultValue = strDittaCorrente
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
  Public Overridable Sub SetDefaultValueMovordTC(ByRef ds As DataSet)
    Try

      '--------------------------------------------------------------
      'imposto i valori di default di movord: con la riga che seguen prendo prima i valori dal database
      oCldGsor.SetTableDefaultValueFromDB("MOVORDTC", ds)
      'ora imposto i valori di default diversi da quelli impostati nel database
      With ds.Tables("MOVORDTC")
        .Columns("codditt").DefaultValue = strDittaCorrente
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
  Public Overridable Sub SetDefaultValueMovordImpTC(ByRef ds As DataSet)
    Try

      '--------------------------------------------------------------
      'imposto i valori di default di movord: con la riga che seguen prendo prima i valori dal database
      oCldGsor.SetTableDefaultValueFromDB("MOVORDTC", "MOVORDIMPTC", ds)
      'ora imposto i valori di default diversi da quelli impostati nel database
      With ds.Tables("MOVORDIMPTC")
        .Columns("codditt").DefaultValue = strDittaCorrente
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
  Public Overridable Sub SetDefaultValueProesec(ByRef ds As DataSet)
    Try

      '--------------------------------------------------------------
      'imposto i valori di default di movord: con la riga che seguen prendo prima i valori dal database
      oCldGsor.SetTableDefaultValueFromDB("PROESEC", ds)
      'ora imposto i valori di default diversi da quelli impostati nel database
      With ds.Tables("PROESEC")
        .Columns("codditt").DefaultValue = strDittaCorrente
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

  Public Overridable Function DuplicaDoc(ByVal strNewTipork As String, ByVal nNewAnno As Integer, ByVal strNewSerie As String, _
                                         ByVal lNewNumord As Integer, ByVal lNewConto As Integer, ByVal nNewTipobf As Integer) As Boolean
    'dal documento aperto, ne crea uno duplicato e setta lo stato di quest'ultimo su nuovo
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim lNumTmp As Integer = 0
    Dim dttTmp, dttAnagra As New DataTable
    Dim bRicalcolaPrezziSconti As Boolean = False
    Dim strTiporkOrig As String = dttET.Rows(0)!et_tipork.ToString
    Dim nAnnoOrig As Integer = NTSCInt(dttET.Rows(0)!et_anno)
    Dim strSerieOrig As String = dttET.Rows(0)!et_serie.ToString
    Dim lNumOrig As Integer = NTSCInt(dttET.Rows(0)!et_numdoc)
    Dim dtrTmp() As DataRow = Nothing
    Dim dtrTmp1() As DataRow = Nothing
    Dim bLinguaDiv As Boolean = False
    Dim strDescr As String = ""
    Dim strOriginalTipork As String = ""
    Try

      CType(oCleComm, CLELBMENU).AccconfGetBlocchi(strDittaCorrente, strNewTipork, dttAccconf)

      strOriginalTipork = NTSCStr(dttET.Rows(0)!et_tipork)
      '-------------------------------
      'test pre-duplica
      If dttET.Rows(0)!et_tipork.ToString = strNewTipork Then
        'posso continuare
      ElseIf dttET.Rows(0)!et_tipork.ToString = "Q" And (strNewTipork = "R" Or strNewTipork = "O" Or strNewTipork = "#") Then
        'Da 'preventivo' solo 'Impegno cliente', 'Ordine fornitore', 'Impegno di commessa'
      ElseIf (dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#") And (strNewTipork = "O" Or strNewTipork = "H") Then
        'DA 'Impegno cliente' o 'Impegno di commessa' solo 'Ordine fornitore', 'Ordine di produzione'
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128573002731562500, _
                      "ATTENZIONE: sono consentite solo le seguenti duplicazioni: " & vbCrLf & _
                      "Nuovo documento dello stesso tipo del documento di origine" & vbCrLf & _
                      "Da 'preventivo' solo 'Impegno cliente', 'Ordine fornitore', 'Impegno di commessa'" & vbCrLf & _
                      "DA 'Impegno cliente' o 'Impegno di commessa' solo 'Ordine fornitore', 'Ordine di produzione'")))
        Return False
      End If

      'devo ricalcolare sempre prezzi/sconti/provv
      'se da tipork Q sto creando un O, oppure se da un tipork R o # sto creando un O o un H
      If (dttET.Rows(0)!et_tipork.ToString = "Q" And strNewTipork = "O") Or _
           ((dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#") And (strNewTipork = "O" Or strNewTipork = "H")) Then
        bRicalcolaPrezziSconti = True
      End If

      '-------------------------------
      If (dttET.Rows(0)!et_tipork.ToString = strNewTipork) Or _
         (dttET.Rows(0)!et_tipork.ToString = "Q" And strNewTipork <> "O") Or _
         ((dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#") And strNewTipork <> "O") Or _
         ((dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#") And strNewTipork <> "H") Then
        If lNewConto = 0 Then lNewConto = NTSCInt(dttET.Rows(0)!et_conto)
        If nNewTipobf = 0 Then nNewTipobf = NTSCInt(dttET.Rows(0)!et_tipobf)
      End If

      If lNewConto = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128602612713906250, "Indicare un conto cliente/fornitore valido")))
        Return False
      Else
        oCldGsor.ValCodiceDb(lNewConto.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp)
        If dttTmp.Rows.Count = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128605156510156250, "Codice conto |" & lNewConto.ToString & "| inesistente")))
          Return False
        Else
          If dttTmp.Rows(0)!an_tipo.ToString = "S" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128602613721562500, "Il conto non può far parte dei sottoconti")))
            Return False
          End If
        End If
        dttTmp.Clear()
      End If

      If nNewTipobf = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128602614980781250, "Il tipo Bolla/Fattura per il nuovo documento deve essere diverso da 0")))
        Return False
      Else
        'se da IC a OP il nuovo tipobf deve avere il magazzino impegni impostato
        If strNewTipork = "H" AndAlso dttET.Rows(0)!et_tipork.ToString <> "H" Then
          If Not oCldGsor.ValCodiceDb(nNewTipobf.ToString, strDittaCorrente, "TABTPBF", "N", "", dttTmp) Then Return False
          If NTSCInt(dttTmp.Rows(0)!tb_tmagimp) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130748776195522794, "Il tipo Bolla/Fattura per il nuovo documento ha il 'Magazzino impegni' non impostato.")))
            Return False
          End If
          dttTmp.Clear()
        End If
      End If

      If lNewNumord <= 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128602615352968750, "Il numero per il nuovo documento deve essere maggiore di 0")))
        Return False
      End If

      If dttET.Rows(0)!et_tipork.ToString = "Q" And strNewTipork <> "Q" And dttET.Rows(0)!et_flevas.ToString = "S" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128605129006718750, "Preventivo già evaso; impossibile utilizzarlo per creare un ordine o un impegno.")))
        Return False
      End If

      '-------------------------------
      'verifico se il nuovo documento da creare esiste già
      If oCldGsor.EsisteOrdine(strDittaCorrente, strNewTipork, nNewAnno, strNewSerie, lNewNumord) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128602616075000000, "Documento da creare già esistente")))
        Return False
      End If

      '-------------------------------
      'imposto le varibili del caso: verranno utilizzate anche nel salvataggio per memorizzare sul preventivo, 
      'nel campo note, gli estremi del nuovo documento generato
      If dttET.Rows(0)!et_tipork.ToString = "Q" And strNewTipork <> "Q" Then
        bNuovoDaPrev = True
        strTipoPrev = "Q"
        nAnnoPrev = NTSCInt(dttET.Rows(0)!et_anno)
        strSeriePrev = NTSCStr(dttET.Rows(0)!et_serie)
        lNumPrev = NTSCInt(dttET.Rows(0)!et_numdoc)
      End If

      '-------------------------------
      'DUPLICO
      bNew = True
      bHasChangesET = True
      bInDuplicadoc = True
      strInDuplicadocTiporkOrig = strTiporkOrig

      'rimuovo il blocco impostato in apertura documento
      DocumentLockRemove()

      If (strTiporkOrig = "R" And strNewTipork = "H") Or _
         (strTiporkOrig = "R" And strNewTipork = "O") Or _
         (strTiporkOrig = "Q" And strNewTipork = "O") Then
        'da IC a OP
        'da IC a OF
        'da Prev a OF
      Else
        If CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "UsaDataOdiernaSuDuplica", "0", " ", "0")) Then
          dsShared.Tables("TESTA").Rows(0)!et_datdoc = NTSCDate(DateTime.Now.ToShortDateString)
        End If
      End If

      'ora la parte in comune: cambio gli estremi del documento
      If dttET.Rows(0)!et_tipork.ToString = strNewTipork Then
        'se il nuovo documento è dello stesso tipo del precedente ...

        'cambio gli estremi del documento e devo svuotare il flag di evasione e prenotazione 
        '(come se il documento no fosse mai stato evaso)
        CambiaNumdoc(dsShared, lNewNumord, strNewSerie, nNewAnno, False)
        dttET.Rows(0)!et_flevas = "N"
        ApriOrdineSvuotaEvasoPrenotatoPmOfa(dsShared, strNewTipork)
        dsShared.AcceptChanges()

      Else
        'nuovo documento di tipo diverso da quello di origine

        '--------------------------
        'al caso rimuovo la DB
        If dsShared.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" And strNewTipork <> "H" Then
          dsShared.Tables("CORPOIMP").Clear()
          If bModTCO Then dsShared.Tables("CORPOIMPTC").Clear()
          dsShared.Tables("ATTIVIT").Clear()
          dsShared.Tables("ASSRIS").Clear()
        End If
        dsShared.AcceptChanges()

        '--------------------------
        'Se il documento di origine è Impegno cliente (R),
        'quello di destinazione è Ordine di produzione (H)
        'e l'impostazione di registro è True (-1)
        'eredita anno/serie/numero partita
        'eredita la ragione sociale 1 e 2 sui riferimenti
        If (dsShared.Tables("TESTA").Rows(0)!et_tipork.ToString = "R" Or dsShared.Tables("TESTA").Rows(0)!et_tipork.ToString = "#") And strNewTipork = "H" Then
          If bDuplicaR2HEreditaRifImpCli = True Then
            dsShared.Tables("TESTA").Rows(0)!et_annpar = dsShared.Tables("TESTA").Rows(0)!et_anno
            dsShared.Tables("TESTA").Rows(0)!et_alfpar = dsShared.Tables("TESTA").Rows(0)!et_serie
            dsShared.Tables("TESTA").Rows(0)!et_numpar = dsShared.Tables("TESTA").Rows(0)!et_numdoc
          Else
            dsShared.Tables("TESTA").Rows(0)!et_annpar = 0
            dsShared.Tables("TESTA").Rows(0)!et_alfpar = " "
            dsShared.Tables("TESTA").Rows(0)!et_numpar = 0
          End If
          If bDuplicaR2HRagSocSuRiferim = True Then
            oCldGsor.ValCodiceDb(NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_conto).ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp)
            dsShared.Tables("TESTA").Rows(0)!et_riferim = Left(Trim(NTSCStr(dttTmp.Rows(0)!an_descr1) & " " & NTSCStr(dttTmp.Rows(0)!an_descr2)), 50)
            dttTmp.Clear()
          End If
        End If

        '--------------------------
        'cambio gli estremi del documento
        LeggiRegistroDoc(strNewTipork)
        dsShared.Tables("TESTA").Rows(0)!et_tipork = strNewTipork
        CambiaNumdoc(dsShared, lNewNumord, strNewSerie, nNewAnno, False)
        Select Case strNewTipork
          Case "R", "Q", "Y", "X", "V", "#" : bDocEmesso = True
          Case Else : bDocEmesso = False
        End Select

        '--------------------------
        'cambio cli/forn e tipobf
        'per il fatto che è un nuovo doc (bNew) al cambio del conto vengono ricalcolati agenti, forma pagam, banca, ecc.
        oCldGsor.ValCodiceDb(NTSCStr(lNewConto), strDittaCorrente, "ANAGRA", "N", , dttAnagra)
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_conto) <> lNewConto Then
          ' Se è cambiato il conto e ha la lingua diversa imposto che deve ricaricare le descrizioni degli articoli
          oCldGsor.ValCodiceDb(NTSCStr(dsShared.Tables("TESTA").Rows(0)!et_conto), strDittaCorrente, "ANAGRA", "N", , dttTmp)
          If dttAnagra.Rows.Count <> 0 And dttTmp.Rows.Count <> 0 Then
            If NTSCInt(dttAnagra.Rows(0)!an_codling) <> NTSCInt(dttTmp.Rows(0)!an_codling) Then bLinguaDiv = True
          End If

          dsShared.Tables("TESTA").Rows(0)!et_conto = lNewConto
          bRicalcolaPrezziSconti = True
        End If

        ''--- Se abilitata, in creazione di un ordine da precedente, preleva la data di sistema anziché quella dell'ordine di origine Then

        '--------------------------
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_tipobf) <> nNewTipobf Then
          dsShared.Tables("TESTA").Rows(0)!et_tipobf = nNewTipobf
          'devo cambiare anche i magazzini di riga : già fatto in autom in validaz tipobf
        End If

        dsShared.Tables("TESTA").Rows(0)!et_confermato = "N"
        dsShared.Tables("TESTA").Rows(0)!et_rilasciato = "N"
        dsShared.Tables("TESTA").Rows(0)!et_aperto = "N"

        '--------------------------
        'forzo il ricalcolo di contropartia e codice IVA di riga
        If Not (strNewTipork = "R" And strTiporkOrig = "Q") Then
          'da preventivo ad impegno devo mantenere codice contropartita e codice iva dell'impegno
          BeforeColUpdate_TESTA_et_controp(dsShared.Tables("TESTA"), New System.Data.DataColumnChangeEventArgs(dsShared.Tables("TESTA").Rows(0), dsShared.Tables("TESTA").Columns("et_controp"), dsShared.Tables("TESTA").Rows(0)!et_controp))
          BeforeColUpdate_TESTA_et_codese(dsShared.Tables("TESTA"), New System.Data.DataColumnChangeEventArgs(dsShared.Tables("TESTA").Rows(0), dsShared.Tables("TESTA").Columns("et_codese"), dsShared.Tables("TESTA").Rows(0)!et_codese))
        End If
        'per la valorizzazione dei semilavorati negli ordini di prod rivalido il listino
        BeforeColUpdate_TESTA_et_listino(dsShared.Tables("TESTA"), New System.Data.DataColumnChangeEventArgs(dsShared.Tables("TESTA").Rows(0), dsShared.Tables("TESTA").Columns("et_listino"), dsShared.Tables("TESTA").Rows(0)!et_listino))
        If NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magimp) <> 0 And strNewTipork <> "H" Then dsShared.Tables("TESTA").Rows(0)!et_magimp = 0
        bTerzista = False
        bTerzista = CType(oCleComm, CLELBMENU).IsTerzista(strDittaCorrente, NTSCInt(dsShared.Tables("TESTA").Rows(0)!et_magimp))
        For Each dtrT As DataRow In dsShared.Tables("CORPO").Rows
          dtrT!ec_confermato = "N"
          dtrT!ec_rilasciato = "N"
          dtrT!ec_aperto = "N"
          If bRiportaCommDaTestataDupl Then
            dtrT!ec_commeca = dttET.Rows(0)!et_commeca
            dtrT!ec_subcommeca = dttET.Rows(0)!et_subcommeca
          End If
          '----------------------------------------------------------------------------------------------------------
          '--- In caso di duplicazione da un conto ad un altro, ridetermina il Codice Articolo Cliente/Fornitore
          '--- (CODARFO), anmche se lo ricalcola alla fine (questo perchè rimaneva sporco)
          '----------------------------------------------------------------------------------------------------------
          If (NTSCStr(dttET.Rows(0)!et_tipork) <> "Y") And (NTSCStr(dttET.Rows(0)!et_tipork) <> "U") Then
            dtrT!xxo_codarfo = CType(oCleComm, CLELBMENU).TrovaCodarfoDaCodart(dtrT!ec_codart.ToString, _
            NTSCInt(dttET.Rows(0)!et_conto), strDittaCorrente, bBD_CodArfo, dtrT!ec_matric.ToString)
          End If
          '----------------------------------------------------------------------------------------------------------
        Next

        '--------------------------
        'tolgo dal documento le righe da non ereditare
        'Se il documento di origine è Impegno cliente (R),
        'quello di destinazione è Ordine di produzione (H)
        'e l'impostazione di registro è True (-1)
        'preleva solo le righe che non risultano prenotate a saldo (mo_flevapre <> "S")
        If (strTiporkOrig = "R" Or strTiporkOrig = "#") And strNewTipork = "H" And bDuplicaR2HSoloNonPrenot = True Then
          dtrTmp = dsShared.Tables("CORPO").Select("ec_flevapre = 'S'")
          If dtrTmp.Length > 0 Then
            For i = 0 To dtrTmp.Length - 1
              If bModTCO Then
                dtrTmp1 = dsShared.Tables("CORPOTC").Select("ec_riga = " & NTSCInt(dtrTmp(i)!ec_riga).ToString)
                For l = 0 To dtrTmp1.Length - 1
                  dtrTmp1(l).Delete()
                Next
              End If
              dtrTmp(i).Delete()
            Next
          End If
        End If
        dsShared.Tables("CORPO").AcceptChanges()
        If (strTiporkOrig = "R" Or strTiporkOrig = "#") And strNewTipork = "H" And bDuplicaR2HEscludiArtNoDiba = True Then
          dtrTmp = dsShared.Tables("CORPO").Select("xxo_coddb IS null")
          If dtrTmp.Length > 0 Then
            For i = 0 To dtrTmp.Length - 1
              If bModTCO Then
                dtrTmp1 = dsShared.Tables("CORPOTC").Select("ec_riga = " & NTSCInt(dtrTmp(i)!ec_riga).ToString)
                For l = 0 To dtrTmp1.Length - 1
                  dtrTmp1(l).Delete()
                Next
              End If
              dtrTmp(i).Delete()
            Next
          End If
        End If
        dsShared.Tables("CORPO").AcceptChanges()
        If (strTiporkOrig = "R" Or strTiporkOrig = "#") And strNewTipork = "H" And bDuplicaR2HSoloArtGesComm = True Then
          dtrTmp = dsShared.Tables("CORPO").Select("xxo_gescomm = 'N'")
          If dtrTmp.Length > 0 Then
            For i = 0 To dtrTmp.Length - 1
              If bModTCO Then
                dtrTmp1 = dsShared.Tables("CORPOTC").Select("ec_riga = " & NTSCInt(dtrTmp(i)!ec_riga).ToString)
                For l = 0 To dtrTmp1.Length - 1
                  dtrTmp1(l).Delete()
                Next
              End If
              dtrTmp(i).Delete()
            Next
          End If
        End If
        dsShared.Tables("CORPO").AcceptChanges()
        If dsShared.Tables.Contains("CORPOTC") Then dsShared.Tables("CORPOTC").AcceptChanges()
        ' se è cambiata la lingua reimposto la descrizione 
        'o si è passati da preventivo/imp cli a ordine fornitore
        If bLinguaDiv = True Then
          If ((strTiporkOrig = "R" Or strTiporkOrig = "#") And (strNewTipork = "H")) Or _
             ((strTiporkOrig = "R" Or strTiporkOrig = "#") And (strNewTipork = "O")) Or _
             ((strTiporkOrig = "Q") And (strNewTipork = "O")) Then
            With dsShared.Tables("CORPO")
              For i = 0 To .Rows.Count - 1
                If NTSCStr(.Rows(i)!ec_codart) <> "D" And NTSCStr(.Rows(i)!ec_codart) <> "M" Then
                  If oCldGsor.ValCodiceDb(NTSCStr(.Rows(i)!ec_codart), strDittaCorrente, "ARTICO", "S", strDescr, dttTmp) Then
                    .Rows(i)!ec_descr = strDescr
                    .Rows(i)!ec_desint = NTSCStr(dttTmp.Rows(0)!ar_desint)
                    If (NTSCDec(dttTmp.Rows(0)!ar_pesoca) = 0) And (NTSCStr(dttTmp.Rows(0)!ar_note).Trim <> "") Then
                      .Rows(i)!ec_note = NTSCStr(dttTmp.Rows(0)!ar_note)
                    End If
                  End If
                End If
                If NTSCInt(dttAnagra.Rows(0)!an_codling) <> 0 Then
                  If oCldGsor.ValCodiceDb(NTSCStr(.Rows(i)!ec_codart), strDittaCorrente, "ARTVAL", "S", strDescr, dttTmp, NTSCStr(dttAnagra.Rows(0)!an_codling)) Then
                    .Rows(i)!ec_descr = strDescr
                    .Rows(i)!ec_desint = NTSCStr(dttTmp.Rows(0)!ax_desint)
                  End If
                End If
                dttTmp.Clear()
                dttTmp.Dispose()
              Next
            End With
          End If
        End If    'If bLinguaDiv = True Then
        dsShared.Tables("CORPO").AcceptChanges()
        If bModTCO Then dsShared.Tables("CORPOTC").AcceptChanges()

        If dsShared.Tables("CORPO").Rows.Count = 0 Then
          If bDuplicaR2HSoloNonPrenot = False And bDuplicaR2HEscludiArtNoDiba = False And bDuplicaR2HSoloArtGesComm = False Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128605996136406250, "Attenzione! Non esistono righe da inserire nel documento di destinazione")))
            Return False
          Else
            If bDuplicaR2HSoloNonPrenot = True Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128605996677343750, "Attenzione! Non esistono righe non prenotate a saldo da inserire nel documento di destinazione")))
              Return False
            End If
            If bDuplicaR2HEscludiArtNoDiba = True Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128605997513125000, "Attenzione! Non esistono righe con articoli con Distinta Base da inserire nel documento di destinazione")))
              Return False
            End If
            If bDuplicaR2HSoloArtGesComm = True Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128605997809687500, "Attenzione! Non esistono righe con articoli gestiti a Commessa da inserire nel documento di destinazione")))
              Return False
            End If
          End If
          Return False
        End If

        '--------------------------
        'azzero prenotato, evaso, riferimenti vari a OFA, PM, ...
        dttET.Rows(0)!et_flevas = "N"
        ApriOrdineSvuotaEvasoPrenotatoPmOfa(dsShared, strNewTipork)
        dsShared.AcceptChanges()

        '--------------------------
        'ricalcolo prezzi/sconti/provvigioni di riga
        For i = 0 To dsShared.Tables("CORPO").Rows.Count - 1
          If bRicalcolaPrezziSconti Then
            dsShared.Tables("CORPO").Rows(i)!ec_provv = 0
            dsShared.Tables("CORPO").Rows(i)!ec_provv2 = 0
            dsShared.Tables("CORPO").AcceptChanges()
          End If
          GetMemDttArti(dsShared.Tables("CORPO").Rows(i)!ec_codart.ToString)
          SettaCondCommerciali(bRicalcolaPrezziSconti, bRicalcolaPrezziSconti, bRicalcolaPrezziSconti, dsShared.Tables("CORPO").Rows(i), NTSCInt(dttArti.Rows(0)!ar_clascon), nClscan, False)

          '  dsShared.Tables("CORPO").Rows(i)!ec_codcfam = dttArti.Rows(0)!ar_famprod
          If NTSCInt(dttArti.Rows(0)!ar_numecr) <> 0 Then
            dsShared.Tables("CORPO").Rows(i)!ec_codcena = dttArti.Rows(0)!ar_numecr
          End If

          'Determina il lotto
          CalcolaLottox(dsShared.Tables("CORPO").Rows(i))
        Next

        '--------------------------
        'se scelgo come documento di destinazione ordine di prod. devo esplodere la distinta base !!!
        If strNewTipork = "H" Then
          For i = 0 To dsShared.Tables("CORPO").Rows.Count - 1
            With dsShared.Tables("CORPO").Rows(i)
              ScriviRigheDaDiba(dsShared.Tables("CORPO").Rows(i), NTSCStr(!ec_codart), NTSCInt(!ec_fase), NTSCDec(!ec_quant), _
                     NTSCInt(dttET.Rows(0)!et_conto), NTSCInt(!ec_commeca), NTSCStr(!ec_subcommeca), _
                     NTSCInt(!ec_codcena), NTSCDate(!ec_datcons), NTSCStr(!ec_codcfam), NTSCInt(!ec_perqta), _
                     NTSCStr(!ec_umprz), NTSCStr(!ec_unmis), NTSCStr(!ec_ump), NTSCDec(!ec_colli), False)
              ValorizzaProduzione(dsShared.Tables("CORPO").Rows(i))
            End With
          Next
        End If

      End If    'If dttET.Rows(0)!et_tipork.ToString = strNewTipork Then

      If NTSCStr(dttET.Rows(0)!et_retail) = "S" Then
        ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 129956236048163626, "Attenzione! Si sta duplicando un ordine creato da Gestione Punti Vendita." & vbCrLf & _
                                                                                          "Verranno eliminati dal documento eventuali promozioni, il secondo codice pagamento (con anche il resto) e le informazioni sul negozio che ha creato il documento.")))
        dttET.Rows(0)!et_retail = "N"
        dttET.Rows(0)!et_codrepc = " "
        dttET.Rows(0)!et_datpromo = DBNull.Value
        dttET.Rows(0)!et_orpromo = 0
        dttET.Rows(0)!et_codtes = 0
        dttET.Rows(0)!et_pagato2v = 0
        dttET.Rows(0)!et_pagato2cambio = 0
        dttET.Rows(0)!et_pagato2valu = 0
        dttET.Rows(0)!et_codstab = 0
        dttET.Rows(0)!et_resto = 0
        dttET.Rows(0)!et_codpaga2 = 0
        dttET.Rows(0)!et_pagato2 = 0
        dttET.Rows(0)!et_datapag2 = DBNull.Value

        For i = 0 To dttEC.Rows.Count - 1
          With dttEC.Rows(i)
            !ec_scontp = 0
            !ec_scontv = 0
            !ec_codrepr1 = 0
            !ec_codrepr2 = 0
            !ec_codrepr3 = 0
            !ec_codrepr4 = 0
            !ec_codrepr5 = 0
            !ec_codrepr6 = 0
            !ec_codrepc = " "
          End With
        Next
      End If

      '---------------------------
      dsShared.Tables("TESTA").Rows(0)!et_flevas = "N"
      dsShared.Tables("TESTA").Rows(0)!et_flstam = "N"
      dsShared.Tables("TESTA").Rows(0)!et_cup = ""
      dsShared.Tables("TESTA").Rows(0)!et_cig = ""
      dsShared.Tables("TESTA").Rows(0)!et_riferimpa = ""

      '---------------------------
      'verifico se devo aggiornare tabnuma al salvataggio
      bProgrCambiato = False
      If lNewNumord <> LegNuma(IIf(strNewTipork = "V", "VV", strNewTipork).ToString, strNewSerie, nNewAnno) Then bProgrCambiato = True

      '---------------------------
      'se necessario ricalcolo il valore delle righe
      If dttET.Rows(0)!et_tipork.ToString = "H" Then
        'ricalcolo i valori di riga degli impegni collegati e delle lavorazioni
        For i = 0 To dttECIMP.Rows.Count - 1
          SettaValoriRiga(dttECIMP.Rows(i))
        Next
      End If
      For i = 0 To dttEC.Rows.Count - 1
        If dttET.Rows(0)!et_tipork.ToString = "H" Then ValorizzaProduzione(dttEC.Rows(i))
        SettaValoriRiga(dttEC.Rows(i))
      Next
      bDocRetail = False

      '---------------------------
      'Se impostata l'opzione di registro relativa, duplica anche i records di ALLOLE di tipo file (se esistenti)
      If bDuplicaAllole = True Then
        If Not oCldGsor.DuplicaDocAllole(strDittaCorrente, strTiporkOrig, nAnnoOrig, strSerieOrig, lNumOrig, strNewTipork, nNewAnno, strNewSerie, lNewNumord) Then
          oCldGsor.DeleteDocAllole(strDittaCorrente, strNewTipork, nNewAnno, strNewSerie, lNewNumord, True, Nothing)
          Return False
        End If
      End If    'If bDuplicaAllole = True Then

      'rimuovo eventuali riferimenti agli ordini di NetPro
      bNetProOrdLock = False
      For i = 0 To dsShared.Tables("CORPO").Rows.Count - 1
        dsShared.Tables("CORPO").Rows(i)!ec_netpid = ""
      Next
      If strNewTipork = "H" Then
        For i = 0 To dsShared.Tables("CORPOIMP").Rows.Count - 1
          dsShared.Tables("CORPOIMP").Rows(i)!ec_netpid = ""
        Next
        For i = 0 To dsShared.Tables("ATTIVIT").Rows.Count - 1
          dsShared.Tables("ATTIVIT").Rows(i)!at_netpid = ""
        Next
      End If

      bInDuplicadoc = False
      strInDuplicadocTiporkOrig = ""

      'Se il nuovo documento è un Impegno cliente e il documento originale è un Preventivo, con l'opzione di registro 
      'NonEreditaNoteDaPreventivoInIC = -1 le note del documento originale non saranno ereditate sul nuovo IC
      If strOriginalTipork = "Q" AndAlso strNewTipork = "R" AndAlso bNonEreditaNoteDaPreventivoInIC Then
        dsShared.Tables("TESTA").Rows(0)!et_note = ""
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se il nuovo documento è un Ordine Fornitore e il documento originale è un Impegno Cliente,
      '--- con l'opzione di registro NonEreditaNoteDaICInOF = -1
      '--- le note del documento originale non saranno ereditate sul nuovo OF
      '--------------------------------------------------------------------------------------------------------------
      If strOriginalTipork = "R" AndAlso strNewTipork = "O" AndAlso bNonEreditaNoteDaICInOF Then
        dsShared.Tables("TESTA").Rows(0)!et_note = ""
      End If
      '--------------------------------------------------------------------------------------------------------------

      'su IC, se serve, ricreo le commesse
      If (strNewTipork = "R" Or strNewTipork = "#") And bGenNumCommecaAutR Then
        If bGenNumCommecaAutR_AllaFine Then
          For i = 0 To dsShared.Tables("CORPO").Rows.Count - 1
            If NTSCStr(dsShared.Tables("CORPO").Rows(i)!xxo_gescomm) <> "N" Then dsShared.Tables("CORPO").Rows(i)!ec_commeca = 0
          Next
        End If
      End If

      dsShared.AcceptChanges()    'forse per ogni riga andrebbe fatta la RecordSalva, sia per il corpo che per gli impegni/lavoraz/assris...

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
      bInDuplicadoc = False
      strInDuplicadocTiporkOrig = ""
    End Try
  End Function
#End Region

#Region "Before\AfterColUpdate"
  Public Overridable Function BeforeColUpdate_TESTA_et_datcons(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim msg As NTSEventArgs
    Try
      If dttEC.Rows.Count > 0 Then

        If dttET.Rows(0)!et_tipork.ToString <> "H" Then
          msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 127791956594531250, "Modificare la data di consegna su tutte le righe di questo documento?"))
        Else
          msg = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128588727908125000, "Modificare la data di consegna su tutte le righe di questo ordine di produzione (comprese le righe degli impegni di produzione/lavorazioni collegate)?"))
        End If
        ThrowRemoteEvent(msg)
        If msg.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
          CambioDatiTestataInGriglia(0, 0, 0, 0, 0, 0, "", e.ProposedValue.ToString, "")
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
  Public Overridable Function BeforeColUpdate_TESTA_et_alfpar(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(NTSCStr(e.ProposedValue), False)
      If strTmp <> NTSCStr(e.ProposedValue) Then e.ProposedValue = strTmp

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

  Public Overridable Function BeforeColUpdate_TESTA_et_organig(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttOrganig As New DataTable
    Try
      If NTSCInt(e.ProposedValue) = 0 Then e.Row!xx_organig = "" : Return True

      oCldGsor.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "ORGANIG", "S", "", dttOrganig)
      If dttOrganig.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130474639785441882, "La persona non esiste")))
        e.ProposedValue = e.Row!et_organig
        Return False
      Else
        If NTSCInt(dttOrganig.Rows(0)!og_conto) <> NTSCInt(e.Row!et_conto) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130474640355904363, "Questa persona lavora per un'altra azienda.")))
          e.ProposedValue = e.Row!et_organig
          Return False
        End If
        e.Row!xx_organig = (NTSCStr(dttOrganig.Rows(0)!og_descont) & " " & NTSCStr(dttOrganig.Rows(0)!og_descont2)).Trim
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

  Public Overridable Function BeforeColUpdate_TESTA_et_codtes(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      e.ProposedValue = ValidaContratto(e.Row, NTSCInt(e.ProposedValue), "et_codtes", "xx_codtes")
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
  Public Overridable Function BeforeColUpdate_TESTA_et_codtes2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      e.ProposedValue = ValidaContratto(e.Row, NTSCInt(e.ProposedValue), "et_codtes2", "xx_codtes2")
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
  Public Overridable Function BeforeColUpdate_TESTA_et_codtes3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      e.ProposedValue = ValidaContratto(e.Row, NTSCInt(e.ProposedValue), "et_codtes3", "xx_codtes3")
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
  Public Overridable Function BeforeColUpdate_TESTA_et_codtes4(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      e.ProposedValue = ValidaContratto(e.Row, NTSCInt(e.ProposedValue), "et_codtes4", "xx_codtes4")
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
  Public Overridable Function BeforeColUpdate_TESTA_et_codtes5(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      e.ProposedValue = ValidaContratto(e.Row, NTSCInt(e.ProposedValue), "et_codtes5", "xx_codtes5")
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
  Public Overridable Function BeforeColUpdate_TESTA_et_codtes6(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      e.ProposedValue = ValidaContratto(e.Row, NTSCInt(e.ProposedValue), "et_codtes6", "xx_codtes6")
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

  Public Overridable Function BeforeColUpdate_TESTA_et_datpromo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not bModCCC Then Return True
      If dttEC.Rows.Count = 0 Then Return True

      Dim evnt As New NTSEventArgs(ThMsg.MSG_YESNO, oApp.Tr(Me, 130597599410480073, "Il cambio della data promozione comporterà il caricamento dei contratti validi a quella data" & _
                                                                                    " e l'annullamento delle promozioni precedentemente applicate." & vbCrLf & _
                                                                                    "Procedere con il cambio di data?"))
      ThrowRemoteEvent(evnt)
      If evnt.RetValue = ThMsg.RETVALUE_YES Then Return True

      e.ProposedValue = e.Row!et_datpromo
      bNoCambioDataContratto = True

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function BeforeColUpdate_TESTA_et_orpromo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not bModCCC Then Return True
      If dttEC.Rows.Count = 0 Then Return True

      Dim evnt As New NTSEventArgs(ThMsg.MSG_YESNO, oApp.Tr(Me, 130597607273744390, "Il cambio dell'ora promozione comporterà il caricamento delle promozioni valide in quella fascia oraria" & _
                                                                                    " e quindi l'annullamento delle promozioni precedentemente applicate." & vbCrLf & _
                                                                                    "Procedere con il cambio di orario?"))
      ThrowRemoteEvent(evnt)
      If evnt.RetValue = ThMsg.RETVALUE_YES Then Return True

      e.ProposedValue = e.Row!et_orpromo
      bNoCambioDataContratto = True

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function AfterColUpdate_TESTA_et_datpromo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not bNoCambioDataContratto Then CaricaContrattiCliente()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      bNoCambioDataContratto = False
    End Try
  End Function
  Public Overridable Function AfterColUpdate_TESTA_et_orpromo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If Not bNoCambioDataContratto Then CaricaContrattiCliente()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      bNoCambioDataContratto = False
    End Try
  End Function
#End Region

#Region "SALVA/CANCELLA e TestPreSalva"
  Public Overrides Function RipristinaDocumento() As Boolean
    Dim bCollaudi As Boolean = False
    Try
      MyBase.RipristinaDocumento()

      'rimuovo il blocco impostato in apertura programma
      DocumentLockRemove()

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

  Public Overridable Function SalvaOrdine(ByVal strState As String) As Boolean
    'strState: N = new, D = delete, U = update
    'strOrdlistCollegati = elenco degli ordlist separati da ',' che sono serviti per generare il documento (da BNORGNOR): al salva devo aggiornare anche ordlist/movrdo
    Dim bResult As Boolean = False
    Dim strErrore As String = ""
    Dim bSetStatoOrdlist As Boolean = False     'se impostata a true cancellando l'ordine non verrà riaperta l'eventuale RDA collegata
    Dim strTipork As String = ""
    Dim bModRA As Boolean = bModuloRA
    Dim dtUltaggTmp As Date = NTSCDate(dttET.Rows(0)!et_ultagg)
    Dim strOpnomeTmp As String = NTSCStr(dttET.Rows(0)!et_opnome)
    Dim bAnnullaSalvataggio As Boolean = False
    Dim i As Integer = 0

    Try
      bInSalvaOrdine = True
      strTipork = dttET.Rows(0)!et_tipork.ToString

      '----------------------------------------
      'controllo se il documento aperto in modifica/cancellaz è stato variato nel frattempo da un altro utente
      If Not bNew Then
        If bDocRetail Then
          'non posso risalvare/cancellare documenti fatti con retail
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128832250775746000, "Documenti generati con il modulo RETAIL non possono essere salvati e/o cancellati con questo programma")))
          Return False
        End If

        If bDocumentLockNoSave Then
          'in apertura documento un altro utente aveva lo stesso doc aperto (o se sono in veboll aveva un ddt aperto 
          'contenente l'ordine che ho cercato di aprire ...) ed è impostato di bloccare la modifica: non faccio salvare
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129176049337099610, "Il documento non può essere modificato/cancellato perchè (come avvisato in apertura documento) un altro operatore lo aveva già in consultazione/modifica. E' possibile solo ripristinare")))
          Return False
        End If

        If Not CheckTimeStamp(strState) Then Return False
      End If

      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If strState = "D" Then
        If Not TestPreCancellaTestord(bSetStatoOrdlist) Then Return False
      Else
        If Not OkTestata() Then Return False

        If Not TestPreSalvaTestord() Then Return False

        '---------------------------------------
        'ricalcolo i totali del documento
        If CalcolaTotali() = False Then Return False

        '---------------------------------------
        'ATTENZIONE! Va lasciato DOPO la CalcolaTotali, altrimenti non funziona!!
        If Not TestPreSalvaTestord_CheckFido() Then Return False

        If Not AggDisimpegno() Then Return False
      End If

      'scheda di trasporto
      If Not strState = "D" Then
        If CheckAperturaSchetrasp(True, bAnnullaSalvataggio) Then
          ThrowRemoteEvent(New NTSEventArgs("SCHEDATRAS:", ""))
        Else
          If bAnnullaSalvataggio Then Return False
        End If
      End If

      'il modulo RDA/RDO vale solo per ordini fornitori e ordini di prod a terzista!!
      If bModRA Then
        If strTipork <> "O" And strTipork <> "H" Then bModRA = False
        If strTipork = "H" And bTerzista = False Then bModRA = False
      End If

      For i = 1 To 8
        dttET.Rows(0)("et_tippaga_" & i.ToString) = 0
        dttET.Rows(0)("et_datsca_" & i.ToString) = DBNull.Value
        dttET.Rows(0)("et_impsca_" & i.ToString) = 0
        dttET.Rows(0)("et_impscav_" & i.ToString) = 0
      Next
      'calcolo le scadenze sul residuo da pagare
      If NTSCInt(dttET.Rows(0)!et_codpaga) <> 0 Then
        If Not CalcolaScadenzeStandard(False) Then Return False
      End If

      dttET.Rows(0)!et_ultagg = DateTime.Now
      dttET.Rows(0)!et_opnome = oApp.User.Nome
      For Each dtrT As DataRow In dttEC.Rows
        dtrT!ec_ultagg = dttET.Rows(0)!et_ultagg
      Next
      dttEC.AcceptChanges()
      If Not dttECIMP Is Nothing Then
        For Each dtrT As DataRow In dttECIMP.Rows
          dtrT!ec_ultagg = dttET.Rows(0)!et_ultagg
        Next
        dttECIMP.AcceptChanges()
      End If
      'Setta dati creazione documento
      If strState = "N" Then SettaDatiCreazione()

      'Salva il documento
      bResult = oCldGsor.SalvaOrdine(dsShared, strState, bSetStatoOrdlist, bModPM, bModRA, bModTCO, bProgrCambiato, _
                                     bNuovoDaPrev, strTipoPrev, nAnnoPrev, strSeriePrev, lNumPrev, bNuovoDaImportExport, _
                                     strErrore, strNetProDB)
      If bResult Then
        bHasChangesET = False
      Else
        dttET.Rows(0)!et_ultagg = dtUltaggTmp    'altrimenti a risalvataggio avvisa che un altro utente ha cambiato l'anagrafica ditta ...
        dttET.Rows(0)!et_opnome = strOpnomeTmp
        For Each dtrT As DataRow In dttEC.Rows
          dtrT!ec_ultagg = dtUltaggTmp
        Next
        dttEC.AcceptChanges()
        If Not dttECIMP Is Nothing Then
          For Each dtrT As DataRow In dttECIMP.Rows
            dtrT!ec_ultagg = dtUltaggTmp
          Next
          dttECIMP.AcceptChanges()
        End If
      End If
      'comunico gli errori all'UI
      If strErrore <> "" Then
        If strErrore.Substring(0, 3) = "*N*" Then
          'cambio il numero documento in tutti i posti interessati
          strErrore = strErrore.Substring(3)
          CambiaNumdoc(dsShared, NTSCInt(dttET.Rows(0)!et_numdoc))
          ThrowRemoteEvent(New NTSEventArgs("", strErrore))
        Else
          If strErrore.Substring(0, 12) = "display_info" Then
            strErrore = strErrore.Substring(12)
            ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, strErrore))
          Else
            ThrowRemoteEvent(New NTSEventArgs("", strErrore))
          End If
        End If
      End If

      If bResult Then
        'rimuovo il blocco impostato in apertura documento
        DocumentLockRemove()
      End If

      If bResult And bGestAlert And strState <> "D" Then
        AlertSottoCosto()
      End If

      If bResult Then
        SalvaOrdine_Pers(strState)
      End If

      Return bResult
    Catch ex As Exception
      dttET.Rows(0)!et_ultagg = dtUltaggTmp    'altrimenti a risalvataggio avvisa che un altro utente ha cambiato l'anagrafica ditta ...
      dttET.Rows(0)!et_opnome = strOpnomeTmp
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    Finally
      bInSalvaOrdine = False
      bInNuovoDocSilent = False
      bInApriDocSilent = False
    End Try
  End Function

  Public Overridable Function SalvaOrdine_Pers(ByVal strState As String) As Boolean
    Try
      Return True

    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function TestPreSalvaTestord() As Boolean
    '-------------------------------------------------
    'eseguo i controlli necessari prima di fare il salvataggio della riga
    Dim i As Integer = 0
    Dim bOk As Boolean = False
    Dim dtrT() As DataRow = Nothing
    Dim strTestSottoCostoLOC As String = ""
    Dim evnt As NTSEventArgs = Nothing
    Dim dtLastdoc As DateTime = NTSCDate(IntSetDate("01/01/1900"))
    Dim dttTmp As New DataTable
    Dim oDttgr As CLEGROUPBY = Nothing
    Dim dttGr As New DataTable
    Dim bGestDateComp As Boolean = False
    Dim strLastTipo As String = ""
    Dim strMsg As String = ""

    Try
      If bInUnload Then Return False

      'eseguo i test pre salvataggio standard
      If Not TestPreSalvaSTD() Then Return False

      If Not bDisabilitaCheckAnnoData And bInCreaDocDaGnor = False Then
        If NTSCInt(dttET.Rows(0)!et_anno) <> NTSCDate(dttET.Rows(0)!et_datdoc).Year Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222100000000, "La data del documento non è inclusa nell'anno indicato")))
          Return False
        End If
      End If
      If NTSCInt(dttET.Rows(0)!et_magaz2) = 0 And dttET.Rows(0)!et_tipork.ToString = "X" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222100625000, "Il codice magazzino 2 è obbligatorio per i documenti di trasferimento: inserirlo prima di salvare")))
        Return False
      End If
      If NTSCInt(dttET.Rows(0)!et_magaz) = NTSCInt(dttET.Rows(0)!et_magaz2) And dttET.Rows(0)!et_tipork.ToString = "X" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526530225436000, "Il codice magazzino deve essere diverso dal magazzino 2 per i documenti di trasferimento: modificarli prima di salvare")))
        Return False
      End If
      If NTSCInt(dttET.Rows(0)!et_magimp) = 0 And dttET.Rows(0)!et_tipork.ToString = "H" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222100781250, "Il codice magazzino impegni è obbligatorio per i documenti di produzione: inserirlo prima di salvare")))
        Return False
      End If
      If bNew And NTSCInt(dttET.Rows(0)!et_conto) = lConclpriv Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523173687597500, "Impossibile emettere ordini a clienti generici privati")))
        Return False
      End If
      If dttET.Rows(0)!et_tipork.ToString = "Y" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526536835630000, "Impossibile salvare un Impegno di produzione")))
        Return False
      End If

      If NTSCDate(dttET.Rows(0)!et_datcons) < NTSCDate(dttET.Rows(0)!et_datdoc) Then
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 128526609851280000, "La data di consegna indicata in testata è inferiore alla data dell'ordine/impegno." & vbCrLf & "Proseguire ugualmente?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = "NO" Then Return False
      End If

      '----------------------------------------
      'aggiorno il flag 'evaso' di testata
      If NTSCStr(dttET.Rows(0)!et_tipork) <> "Q" Then
        If dttEC.Select("ec_flevas = 'C'").Length = 0 Then
          dttET.Rows(0)!et_flevas = "S"
        Else
          dttET.Rows(0)!et_flevas = "N"
        End If
      End If

      If bSegnalaCreazOrdiniCliFornBloccati Then
        oCldGsor.ValCodiceDb(dttET.Rows(0)!et_conto.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp)
        If (NTSCStr(dttTmp.Rows(0)!an_blocco) <> "B") And (NTSCStr(dttTmp.Rows(0)!an_blocco) <> "N") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128969001048609200, _
            "Attenzione! Cliente con blocco |" & DescrBlocco(NTSCStr(dttTmp.Rows(0)!an_blocco)) & "| indicato in anagrafica. Si procede comunque alla creazione dell'ordine/impegno.")))
        End If
        dttTmp.Clear()
      End If

      If dttET.Rows(0)!et_tipork.ToString <> "Q" And dttET.Rows(0)!et_tipork.ToString <> "$" And dttET.Rows(0)!et_tipork.ToString <> "V" Then
        If ((dttET.Rows(0)!et_tipork.ToString = "R") Or (dttET.Rows(0)!et_tipork.ToString = "#")) And _
           (bGenNumCommecaAutR = True) And (bGenNumCommecaAutR_AllaFine = True) Then
        Else
          '----------------------------------------
          'controllo articoli gestiti a commessa
          If Not bDisabilitaCheckCommesse Then
            dtrT = dttEC.Select("xxo_gescomm <> 'N' AND ec_commeca = 0")
            If dtrT.Length > 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128763308308140000, "L'articolo |" & UCase(NTSCStr(dtrT(0)!ec_codart)) & "| nella riga |" & dtrT(0)!ec_riga.ToString & "| è gestito a commessa e non è indicato nessun numero di commessa")))
              Return False
            End If
            If dttET.Rows(0)!et_tipork.ToString = "H" Then
              dtrT = dttECIMP.Select("xxo_gescomm <> 'N' AND ec_commeca = 0")
              If dtrT.Length > 0 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128763308320776000, "L'articolo |" & UCase(NTSCStr(dtrT(0)!ec_codart)) & "| nella riga |" & dtrT(0)!ec_riga.ToString & "| degli IMPEGNI DI PRODUZIONE è gestito a commessa e non è indicato nessun numero di commessa")))
                Return False
              End If
            End If
          End If
        End If
      End If    'If dttET.Rows(0)!et_tipork.ToString <> "Q" And dttET.Rows(0)!et_tipork.ToString <> "$" And dttET.Rows(0)!et_tipork.ToString <> "V" Then

      '----------------------------------------
      'Controllo sotto costo finale
      bOk = True
      If ((strTestSottoCosto = "A" Or strTestSottoCosto = "P" Or strTestSottoCosto = "B") And bNew) Or _
          ((strTestSottoCostoModifica = "A" Or strTestSottoCostoModifica = "P" Or strTestSottoCostoModifica = "B") And bNew = False) Then
        'da fare
        'controllo fatto riga per riga
        If strTestSottoCostoQuando <> "B" And strTestSottoCostoQuando <> "C" Then
          'da non fare
          bOk = False
        Else
          'Esegue il test solo sui documenti R (impegni cliente)
          If dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "Q" Or dttET.Rows(0)!et_tipork.ToString = "#" Then
            'da fare
          Else
            'da non fare
            bOk = False
          End If
        End If
      Else
        'da non fare
        bOk = False
      End If

      If bOk Then
        If bNew Then strTestSottoCostoLOC = strTestSottoCosto Else strTestSottoCostoLOC = strTestSottoCostoModifica
        If strTestSottoPWD = "" Then strTestSottoPWD = UCase(oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "TestSottoCostoPWD", "NTS", " ", "NTS"))
        'Controllo test abilitato sulla riga
        If strTestSottoCostoQuando = "B" Or strTestSottoCostoQuando = "C" Then
          dtrT = dttEC.Select("ec_flkit <> 'A' AND ec_flkit <> 'T' ")
          For Each dtrTmp As DataRow In dtrT
            If TestSottoCostoSTD(dtrTmp!ec_codart.ToString, NTSCInt(dtrTmp!ec_fase), NTSCDec(dtrTmp!ec_quant), _
                              NTSCDec(dtrTmp!ec_prezzo), NTSCDate(dttET.Rows(0)!et_datdoc), NTSCInt(dttET.Rows(0)!et_valuta), _
                              NTSCDec(dtrTmp!ec_scont1), NTSCDec(dtrTmp!ec_scont2), NTSCDec(dtrTmp!ec_scont3), _
                              NTSCDec(dtrTmp!ec_scont4), NTSCDec(dtrTmp!ec_scont5), NTSCDec(dtrTmp!ec_scont6), _
                              NTSCDec(dttET.Rows(0)!et_scont1), NTSCDec(dttET.Rows(0)!et_scont2), _
                              NTSCDec(dttET.Rows(0)!et_scopag), dtrTmp!ec_flkit.ToString, strTestSottoPWD, _
                              strTestSottoCostoLOC, strTestSottoCostoTipo, strTestSottoCostoZero, _
                              strTestSottoCostoQuando, False, dTestSottoCostoRicaricoMinimo, _
                              bTestSottoCostoOscuraCosto, NTSCInt(dtrTmp!ec_riga), NTSCInt(dtrTmp!ec_codtpro), _
                              dtrTmp!ec_flprznet.ToString) = False Then
              Return False
            End If

          Next
        End If
      End If    'If bOk Then

      '----------------------------------------
      'Controllo evasione
      If Not TestPreSalvaTestord_CheckEvasione() Then Return False

      '----------------------------------------
      'Controllo esistenza finale
      If Not TestPreSalvaSTD_CheckEsistFinale() Then Return False

      '----------------------------------------
      'Controllo assegnazioni di riga (Solo su impegni cliente)
      If NTSCStr(dttET.Rows(0)!et_tipork) = "R" Or NTSCStr(dttET.Rows(0)!et_tipork) = "#" Then
        'Solo se attiva opzione di registro di proposta assegnazione
        'Se non è attiva l'opzione infatti sono sufficenti i controlli di riga (ds_Validate)
        If bProponiQtaDaAss Then
          dtrT = dttEC.Select("xxo_qtadaass <> 0")
          For Each dtrTmp As DataRow In dtrT
            If Not CheckAssegnazione(dtrTmp) Then Return False
          Next
        End If
      End If

      '----------------------------
      'verifico le date di competenza sulle contropartite
      '------------------------------------
      'Controlla che, se il conto (CG e/o CA) agganciato alla contropartita di riga
      'ha una gestione 'periodo di comp. economica', 'data inizio competenza economica' sia
      'inferiore a 'data fine competenza economica'
      oCldGsor.ValCodiceDb(strDittaCorrente, strDittaCorrente, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        If NTSCStr(dttTmp.Rows(0)!ac_flrifboi) = "S" Then

          oDttgr = New CLEGROUPBY
          dttGr.Clear()
          If Not oDttgr.NTSGroupBy(dttEC, dttGr, "ec_controp, ec_codart", "ec_datini >= ec_datfin", "ec_controp") Then
            Return False
          End If
          For i = 0 To dttGr.Rows.Count - 1
            If NTSCStr(dttGr.Rows(i)!ec_codart) <> "D" And NTSCInt(dttGr.Rows(i)!ec_controp) <> 0 Then
              If Not oCldGsor.SalvaDocumentoTestDataComp(strDittaCorrente, NTSCInt(dttGr.Rows(i)!ec_controp), CBool(lModuliDittaDitt And bsModCI), bGestDateComp) Then Return False
              If bGestDateComp Then
                dtrT = dttEC.Select("ec_codart <> 'D' AND ec_datini >= ec_datfin AND ec_controp = " & NTSCInt(dttGr.Rows(i)!ec_controp).ToString)
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128667211955781250, _
                                 "La riga |" & dtrT(0)!ec_riga.ToString & "| (articolo |" & dtrT(0)!ec_codart.ToString & _
                                 "|) " & vbCrLf & "possiede una contropartita il cui codice conto (di CG e/o CA) richiede obbligatoriamente " & _
                                 "l'indicazione di un periodo competenza economica valido " & vbCrLf & "('Data inizio comp. economica' " & _
                                 "deve essere inferiore alla 'data fine comp. economica')")))
                Return False
              End If
            End If
          Next    'For i = 0 To dtrT.Length - 1
        End If    'If NTSCStr(dttTmp.Rows(0)!ac_flrifboi) = "S" Then
      End If    'If dttTmp.Rows.Count > 0 Then

      '----------------------------------------
      'Se si è in inserimento di un nuovo ordine/impegno e questo possiede la data anteriore
      'all'eventuale ultimo ordine/impegno inserito a parità di tipo/anno/serie chiede conferma prima di proseguire
      If bNew And bDisabilitaCheckDateAnteriori = False And bNuovoDaImportExport = False Then
        dtLastdoc = oCldGsor.GetDatdocLastOrd(strDittaCorrente, NTSCStr(dttET.Rows(0)!et_tipork), NTSCInt(dttET.Rows(0)!et_anno), NTSCStr(dttET.Rows(0)!et_serie))
        If dtLastdoc > NTSCDate(dttET.Rows(0)!et_datdoc) Then
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 128534209815262000, "Attenzione!" & vbCrLf & _
            "Esiste un documento precedente a quello che si vuole inserire" & vbCrLf & _
            "(a parità di tipoligia, anno e serie), con data |'" & dtLastdoc.ToShortDateString & "'|," & vbCrLf & _
            "posteriore a quella di testata |'" & NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString & "'|." & vbCrLf & vbCrLf & _
            "Confermare comunque l'inserimento dell'ordine/impegno?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = "NO" Then Return False
        End If
      End If


      If NTSCStr(dttET.Rows(0)!et_tipork) = "H" Then
        '----------------------------------------
        'Controllo attivit
        If Not TestPreSalvaTestord_CheckAttivit() Then Return False

        '----------------------------------------
        'in vb6 CheckSequenzaQtaLavorate
        'Controlla la congruenza delle quantità fra le fasi di tutte le lavorazioni.
        For Each dtrTmp As DataRow In dttEC.Rows
          dtrT = dttATTIVIT.Select("at_riga = " & NTSCInt(dtrTmp!ec_riga).ToString & " AND at_qtapr <> " & CDblSQL(NTSCDec(dtrTmp!ec_quant)))
          If dtrT.Length > 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128528262055850000, "La quantità indicata nella fase |" & NTSCInt(dtrT(0)!at_fase).ToString & "|, associata alla riga |" & NTSCInt(dtrT(0)!at_riga).ToString & "| dell'ORDINE DI LAVORAZIONE, non è uguale a quella ordinata.")))
            Return False
          End If
        Next

        '----------------------------------------
        'in modifica non posso cancellare record già avviati in avlavp
        If Not oCldGsor.CheckOrdInAvlavp(strDittaCorrente, NTSCStr(dttET.Rows(0)!et_tipork), _
                                NTSCInt(dttET.Rows(0)!et_anno), NTSCStr(dttET.Rows(0)!et_serie), _
                                NTSCInt(dttET.Rows(0)!et_numdoc), dttTmp) Then Return False
        If dttTmp.Rows.Count > 0 Then
          For Each dtrT1 As DataRow In dttTmp.Rows
            Dim dtrT2() As DataRow = dttEC.Select("ec_riga = " & NTSCStr(dtrT1!lce_orriga))
            If dtrT2.Length = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130778189141065745, "ATTENZIONE: una o più lavorazioni contenute nell'ordine sono già state avviate/completate, per cui non possono essere cancellate. Vedi riga |" & NTSCStr(dtrT1!lce_orriga) & "|")))
              Return False
            End If
            If NTSCStr(dtrT2(0)!ec_codart).ToUpper = NTSCStr(dtrT1!lce_codart).ToUpper Then
              'tutto ok
            Else
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130778189157236572, "ATTENZIONE: una o più lavorazioni contenute nell'ordine sono già state avviate/completate, per cui l'articolo (o la fase) non può essere cambiato. Vedi riga |" & NTSCStr(dtrT1!lce_orriga) & "|")))
              Return False
            End If
          Next
        End If

        If bModTCO Then
          'controlla che le sommatorie dei records di MOTRAIMPTC coincidano 
          'con le righe degli impegni collegati che hanno l'articolo gestito per taglie e colori
          If Not TestPreSalvaTestord_CheckMotraimptc() Then Return False
        End If
      End If

      '--------------------------------------------------------------------------------------------------------------
      'solo documenti emessi
      'non faccio salvare se sono presenti articoli con artico.ar_tipo diversi (escludendo articoli descrittivi e ar_tipo = ' ' e ar_tipo = null)
      'Il tutto serve per poter differenziare, sulla base del DL n° 1 del 24/1/2012 (convertito con la legge n° 27 del 24/3/2012, e in vigore dal 24/10/2012), la vendita di PRODOTTI DETERIORABILI ENTRO O OLTRE 60ggi da parte dei produttori.. Per i primi il pagamento deve avvenire entro 30gg per i restanti entro 60gg pena interessi di mora maggiorati.
      '--------------------------------------------------------------------------------------------------------------
      If (strDataInibisciArticoliDiversi.Trim <> "") And (IsDate(strDataInibisciArticoliDiversi)) Then
        If NTSCDate(dttET.Rows(0)!et_datdoc) >= NTSCDate(strDataInibisciArticoliDiversi) And NTSCInt(dttET.Rows(0)!et_codpaga) <> 0 Then
          i = 0
          If nInibisciTipiArticoliDiversiTrannePagContanti = -1 Then
            oCldGsor.ValCodiceDb(NTSCStr(dttET.Rows(0)!et_codpaga), strDittaCorrente, "TABPAGA", "N", "", dttTmp)
            i = NTSCInt(dttTmp.Rows(0)!tb_tippaga)
            dttTmp.Clear()
            dttTmp.Dispose()
          End If
          If (nInibisciTipiArticoliDiversi = -1 Or nInibisciTipiArticoliDiversi = 1) And (i <> 4) Then
            If NTSCStr(dttET.Rows(0)!et_tipork) = "R" Or NTSCStr(dttET.Rows(0)!et_tipork) = "Q" Or NTSCStr(dttET.Rows(0)!et_tipork) = "V" Then
              For Each dtrT1 As DataRow In dttEC.Rows
                If Not oCldGsor.ValCodiceDb(NTSCStr(dtrT1!ec_codart), strDittaCorrente, "ARTICO", "S", "", dttTmp) Then Return False
                If dttTmp.Rows.Count > 0 Then
                  If NTSCStr(dttTmp.Rows(0)(strNomeCampoArtico)).Trim <> "" And NTSCStr(dttTmp.Rows(0)!ar_stainv) = "S" Then
                    If strLastTipo <> "" And strLastTipo <> NTSCStr(dttTmp.Rows(0)(strNomeCampoArtico)).ToUpper Then
                      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129938211138583866, "Attenzione: nel documento sono presenti articoli non descrittivi di tipologia diversa tra loro. Impossibile salvare")))
                      Return False
                    Else
                      strLastTipo = NTSCStr(dttTmp.Rows(0)(strNomeCampoArtico)).ToUpper
                    End If
                  End If
                End If
              Next
            End If
          End If    'If nInibisciTipiArticoliDiversi = -1 Then
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------

      '----------------------------------------
      'Controlla se una riga di MOTRANS evade a saldo solo una volta l'ordine
      If Not TestPreSalvaTestord_CheckEvasioneSaldo() Then Return False

      '----------------------------------------
      'controllo presenza di tutti i dati per gestire la Contabilità analitica duplice contabile
      If Not TestPreSalvaTestord_CheckCa2() Then Return False


      '------------------------------
      'se c'è il collegamento con NETPRO controllo che il documento non sia in evasione ed eventualmente avviso
      If Not NetProCheckOrdModifCanc(False) Then Return False


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
  Public Overridable Function TestPreSalvaTestord_CheckEvasione() As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Try
      '----------------------------------------
      'Controllo evasione
      'Controlla se una riga di MOTRANS non sia a saldo 'C' quando mo_quant <= mo_quaeva
      dtrT = dttEC.Select("ec_flevas = 'C' AND ec_quant <= ec_quaeva AND ec_quant <> 0")
      If dtrT.Length > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128763308332788000, "La riga |" & dtrT(0)!ec_riga.ToString & "| non può essere evasa parzialmente (in conto), quando la quantità ordinata è inferiore o uguale alla quantità evasa.")))
        Return False
      End If

      If dttET.Rows(0)!et_tipork.ToString = "H" Then
        'Controlla se una riga di MOTRANS non sia a saldo 'C' quando mo_quant <= mo_quaeva
        dtrT = dttECIMP.Select("ec_flevas = 'C' AND ec_quant <= ec_quaeva AND ec_quant <> 0")
        If dtrT.Length > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128763308346048000, "La riga |" & dtrT(0)!ec_riga.ToString & "| dell'IMPEGNO DI PRODUZIONE (associata alla riga |" & dtrT(0)!ec_rigaor.ToString & "| dell'ORDINE DI PRODUZIONE) non può essere evasa parzialmente (in conto), quando la quantità ordinata è inferiore o uguale alla quantità evasa.")))
          Return False
        End If

        '----------------------------------------
        'Controlla che non ci siano padri evasi e figli non evasi
        dtrT = dttEC.Select("ec_flevas = 'S'")
        For Each dtrTmp As DataRow In dtrT
          dtrT1 = dttECIMP.Select("ec_flevas = 'C' AND ec_rigaor = " & NTSCInt(dtrTmp!ec_riga).ToString)
          If dtrT1.Length > 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526625076100000, "La riga |" & dtrT1(0)!ec_riga.ToString & "| dell'IMPEGNO DI PRODUZIONE (associata alla riga |" & dtrT1(0)!ec_rigaor.ToString & "| dell'ORDINE DI PRODUZIONE) deve essere evasa a saldo in quanto la riga dell'articolo padre risulta già evasa a saldo.")))
            Return False
          End If
        Next

        '----------------------------------------
        'Controlla se una riga di impegno non sia prenotata a saldo 'C' quando mo_quant <= mo_quaeva
        dtrT = dttECIMP.Select("ec_flevapre = 'C' AND ec_quant <= (ec_quaeva + ec_quapre) AND ec_quant <> 0 AND ec_flevas = 'C'")
        If dtrT.Length > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128763308361024000, "La riga |" & dtrT(0)!ec_riga.ToString & "| dell'IMPEGNO DI PRODUZIONE (associata alla riga |" & dtrT(0)!ec_rigaor.ToString & "| dell'ORDINE DI PRODUZIONE) non può essere prenotata parzialmente (in conto), quando la quantità ordinata è inferiore o uguale alla somma tra la quantità evasa e quella prenotata.")))
          Return False
        End If

        '----------------------------------------
        'Controlla se una riga di ordine di prod non sia prenotata a saldo 'C' quando mo_quant <= mo_quaeva
        dtrT = dttEC.Select("ec_flevapre = 'C' AND ec_quant <= (ec_quaeva + ec_quapre) AND ec_quant <> 0 AND ec_flevas = 'C'")
        If dtrT.Length > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526628159128000, "La riga |" & dtrT(0)!ec_riga.ToString & "| non può essere prenotata parzialmente (in conto), quando la quantità ordinata è inferiore o uguale alla somma tra la quantità evasa e quella prenotata.")))
          Return False
        End If

      End If    'If dttET.Rows(0)!et_tipork.ToString = "H" Then


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
  Public Overridable Function TestPreSalvaTestord_CheckAttivit() As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim oDttgr As CLEGROUPBY = Nothing
    Dim dttGr As New DataTable
    Try
      '-----------------------------
      'Cod. centro obbligatorio sempre:
      'per ogni riga di attivit verifico se c'è il centro in assris
      For Each dtrTmp As DataRow In dttATTIVIT.Rows
        dtrT1 = dttASSRIS.Select("as_riga = " & NTSCInt(dtrTmp!at_riga).ToString & _
                                 " AND as_fase = " & NTSCInt(dtrTmp!at_fase).ToString)
        If dtrT1.Length > 0 Then
          If NTSCInt(dtrT1(0)!as_codcent) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128527492182932000, "La fase n° |" & NTSCInt(dtrTmp!at_fase).ToString & "|, delle attività associate alla riga n° |" & NTSCInt(dtrTmp!at_riga).ToString & "|, dell'ORDINE DI PRODUZIONE, possiede il Codice centro pari a 0.")))
            Return False
          End If
        End If
      Next

      '-----------------------------
      'Congruenza terzisti: 
      If bTerzista Then

        'solo una lavorazione per riga di carico
        oDttgr = New CLEGROUPBY
        dttGr.Clear()
        If Not oDttgr.NTSGroupBy(dttATTIVIT, dttGr, "First(at_riga) AS Riga, Count(at_riga) AS NumDuplicati", "", "at_riga") Then
          Return False
        End If
        dtrT = dttGr.Select("NumDuplicati > 1")
        If dtrT.Length > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128527495252856000, "Associata alla riga |" & dtrT(0)!RIGA.ToString & "| dell'ORDINE DI PRODUZIONE esistono |" & dtrT(0)!NumDuplicati.ToString & "| righe di attività. Per le lavorazioni a terzisti è obbligatorio dichiarare una sola lavorazione per riga d'ordine.")))
          Return False
        End If
        dttGr.Clear()

        'Il campo tb_magterz del centro terzista deve essere uguale a MAGIMP
        For Each dtrTmp As DataRow In dttASSRIS.Rows
          oCldGsor.ValCodiceDb(NTSCInt(dtrTmp!as_codcent).ToString, strDittaCorrente, "TABCENT", "N", "", dttGr)
          If dttGr.Rows.Count > 0 Then
            If NTSCInt(dttGr.Rows(0)!tb_magterz) <> NTSCInt(dttET.Rows(0)!et_magimp) Then
              Dim dtrRow() As datarow = dttEc.select("ec_riga = " & ntscint(dtrTmp!as_riga))
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128527498272860000, "Per la fase |" & NTSCInt(dtrTmp!as_fase).ToString & "| , delle attività associata alla riga |" & NTSCInt(dtrTmp!as_riga).ToString & "| (articolo |" & ntscstr(dtrRow(0)!ec_codart) & "|) dell'ORDINE DI PRODUZIONE, il centro |" & NTSCInt(dtrTmp!as_codcent).ToString & "| non appartiene al terzista che ha eseguito la lavorazione.")))
              Return False
            End If

            '----------------------------------------
            'in vb6 bsorgsor.CheckCoerenzaCentriLavMagaz
            'test eseguito solo se opzione di registro BSORGSOR/OPZIONI/CheckCoerenzaCentriLavMagaz = -1
            'solo sugli ordini di produzione (e solo se il magazzino impegni <> 0)
            'controlla che il magazzino associato ai centri di lavoro usati per le lavorazioni sia uguale al magazzino impegni
            If bTestCoerenzaCentriLavMagaz And NTSCInt(dttET.Rows(0)!et_magimp) <> 0 Then
              If NTSCInt(dttGr.Rows(0)!tb_magterz) <> NTSCInt(dttET.Rows(0)!et_magimp) Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128528260047818000, "ATTENZIONE: per l'articolo da produrre sulla riga |" & NTSCInt(dtrTmp!as_riga).ToString & "| sono presenti righe di lavorazioni collegate a centri di lavoro (|" & NTSCInt(dtrTmp!as_codcent).ToString & "|)" & vbCrLf & "il cui magazzino associato è diverso dal 'magazzino impegni' indicato in testata documento (|" & NTSCInt(dttET.Rows(0)!et_magimp).ToString & "|)")))
                Return False
              End If
            End If
          End If
        Next

      Else

        'Non deve essere indicato un centro terzista
        'tb_flclavo <> ' ' AND tb_flclavo <> 'C'
        For Each dtrTmp As DataRow In dttASSRIS.Rows
          If Not oCldGsor.GetTebmagaTerzFormTabcent(strDittaCorrente, NTSCInt(dtrTmp!as_codcent), dttGr) Then Return False
          If dttGr.Rows.Count > 0 Then
            If NTSCStr(dttGr.Rows(0)!tb_flclavo) <> " " And NTSCStr(dttGr.Rows(0)!tb_flclavo) <> "C" Then
              Dim dtrRow() As datarow = dttEc.select("ec_riga = " & ntscint(dtrTmp!as_riga))
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128527500106640000, "Per la fase |" & NTSCInt(dtrTmp!as_fase).ToString & "|, delle attività associata alla riga |" & NTSCInt(dtrTmp!as_riga).ToString & "| (articolo |" & ntscstr(dtrRow(0)!ec_codart) & "|) dell'ORDINE DI PRODUZIONE, il centro |" & NTSCInt(dtrTmp!as_codcent).ToString & "| appartiene a un magazzino terzista anche se la lavorazione è interna.")))
              Return False
            End If
          End If
        Next

      End If    'If bTerzista Then

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
  Public Overridable Function TestPreSalvaTestord_CheckMotraimptc() As Boolean
    '------------------------
    'controlla che le sommatorie dei records di MOTRAIMPTC coincidano 
    'con le righe degli impegni collegati che hanno l'articolo gestito per taglie e colori
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim i As Integer = 0
    Dim bRigheAZero As Boolean = False
    Dim evnt As NTSEventArgs = Nothing

    Try
      dtrT = dttECIMP.Select("xxo_codtagl <> 0")
      For i = 0 To dtrT.Length - 1
        If NTSCDec(dtrT(i)!ec_quant) = 0 Then bRigheAZero = True

        dtrT1 = dttECIMPTC.Select("ec_riga = " & NTSCInt(dtrT(i)!ec_riga).ToString)
        If dtrT1.Length > 0 Then
          If NTSCDec(dtrT(i)!ec_quant) <> NTSCDec(dtrT1(0)!ec_quant01) + NTSCDec(dtrT1(0)!ec_quant02) + NTSCDec(dtrT1(0)!ec_quant03) + _
                                          NTSCDec(dtrT1(0)!ec_quant04) + NTSCDec(dtrT1(0)!ec_quant05) + NTSCDec(dtrT1(0)!ec_quant06) + _
                                          NTSCDec(dtrT1(0)!ec_quant07) + NTSCDec(dtrT1(0)!ec_quant08) + NTSCDec(dtrT1(0)!ec_quant09) + _
                                          NTSCDec(dtrT1(0)!ec_quant10) + NTSCDec(dtrT1(0)!ec_quant11) + NTSCDec(dtrT1(0)!ec_quant12) + _
                                          NTSCDec(dtrT1(0)!ec_quant13) + NTSCDec(dtrT1(0)!ec_quant14) + NTSCDec(dtrT1(0)!ec_quant15) + _
                                          NTSCDec(dtrT1(0)!ec_quant16) + NTSCDec(dtrT1(0)!ec_quant17) + NTSCDec(dtrT1(0)!ec_quant18) + _
                                          NTSCDec(dtrT1(0)!ec_quant19) + NTSCDec(dtrT1(0)!ec_quant20) + NTSCDec(dtrT1(0)!ec_quant21) + _
                                          NTSCDec(dtrT1(0)!ec_quant22) + NTSCDec(dtrT1(0)!ec_quant23) + NTSCDec(dtrT1(0)!ec_quant24) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128528299762180000, _
                            "L'articolo |'" & dtrT(i)!ec_codart.ToString & "'|, riga |'" & dtrT(i)!ec_riga.ToString & "'|" & _
                            " negli 'Impegni collegati', possiede una quantità di riga diversa" & _
                            " dalla sommatoria presente nel Dettaglio quantità - Taglie e colori-." & vbCrLf & _
                            "Intervenire su tali valori prima di salvare l'ordine di produzione.")))
            Return False
          End If
        End If
      Next

      '------------------------
      'Se esistono righe con articoli per taglie e colori con quantità a zero
      'negli Impegni collegati, chiede conferma
      If bRigheAZero Then
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 128528302186490000, _
                                "Esistono righe con quantità pari a zero negli Impegni collegati." & vbCrLf & _
                                "Proseguire ugualmente con il salvataggio dell'ordine di produzione?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = "NO" Then Return False
      End If

      '------------------------
      'per gli articoli prodotti di tipo TCO controllo se sono presenti impegni collegati dove non è stata 
      'specificata la taglia
      dtrT = dttEC.Select("xxo_codtagl <> 0")
      For i = 0 To dtrT.Length - 1
        If dttECIMP.Select("ec_rigaor = " & NTSCInt(dtrT(i)!ec_riga).ToString & " AND ec_tctaglia = ' '").Length <> 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128528307575666000, _
                              "Attenzione!" & vbCrLf & _
                              "Negli 'Impegni collegati' relativi all'articolo |'" & NTSCStr(dtrT(i)!ec_codart) & _
                              "'| (riga |'" & NTSCInt(dtrT(i)!ec_riga).ToString & "'|)" & vbCrLf & _
                              "esistono righe nelle quali non è stata indicata una taglia valida.")))
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
    End Try
  End Function
  Public Overridable Function TestPreSalvaTestord_CheckEvasioneSaldo() As Boolean
    '----------------------------------------
    'Controlla se una riga di MOTRANS evade a saldo solo una volta l'ordine
    Dim oDttgr As CLEGROUPBY = Nothing
    Dim dttGr As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim dttMo As New DataTable
    Dim dQta As Decimal = 0
    Dim QuaEvasa As Decimal = 0
    Dim QuaDisEvasa As Decimal = 0
    Dim ColDisEvasa As Decimal = 0
    Dim ValDisEvasa As Decimal = 0

    Try
      If dttET.Rows(0)!et_tipork.ToString <> "R" And _
         dttET.Rows(0)!et_tipork.ToString <> "#" And _
         dttET.Rows(0)!et_tipork.ToString <> "O" Then Return True

      oDttgr = New CLEGROUPBY
      dttGr.Clear()
      If Not oDttgr.NTSGroupBy(dttEC, dttGr, "Min(ec_oatipo) AS Tipo, Min(ec_oaanno), Min(ec_oaserie) AS Serie, " & _
                                            " Min(ec_oanum) AS Num, Min(ec_oariga) AS Riga, Min(ec_oasalcon), " & _
                                            " Count(ec_oatipo) AS NumDuplicati, Count(ec_oasalcon) as NumSalcon", _
                                            "ec_oasalcon='S'", _
                                            "ec_oatipo, ec_oaanno, ec_oaserie, ec_oanum, ec_oariga, ec_oasalcon") Then
        Return False
      End If
      dtrT = dttGr.Select("NumDuplicati > 1 AND NumSalcon > 1")
      If dtrT.Length > 0 Then
        Select Case dtrT(i)!tipo.ToString
          Case "$"
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128763308416640000, "La riga |" & dtrT(i)!RIGA.ToString & "| dell' ORDINE FORNITORE APERTO n° |" & dtrT(i)!num.ToString & "| è stata evasa a saldo |" & dtrT(i)!NumDuplicati.ToString & "| volte in questo ordine. E' possibile evadere solo una volta una riga d'ordine a saldo.")))
          Case "V"
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128528316105466000, "La riga |" & dtrT(i)!RIGA.ToString & "| dell' IMPEGNO CLIENTE APERTO n° |" & dtrT(i)!num.ToString & "| è stata evasa a saldo |" & dtrT(i)!NumDuplicati.ToString & "| volte in questo ordine. E' possibile evadere solo una volta una riga d'ordine a saldo.")))
        End Select
        Return False
      End If

      '----------------------------------------
      'Controlla che una riga d'ordine sia evasa a saldo quando mo_quant supera mo_quant
      'dell'ordine aperto a cui fa riferimento
      oDttgr = New CLEGROUPBY
      dttGr.Clear()
      If Not oDttgr.NTSGroupBy(dttEC, dttGr, "Min(ec_oatipo) as Tipo, Min(ec_oaanno) as Anno, Min(ec_oaserie) as Serie, " & _
                                             " Min(ec_oanum) as Num, Min(ec_oariga) as Riga, Sum(ec_quant) as QuaEva", _
                                             "ec_oanum > 0", _
                                             "ec_oatipo, ec_oaanno, ec_oaserie, ec_oanum, ec_oariga") Then
        Return False
      End If

      For i = 0 To dttGr.Rows.Count - 1
        'leggo dall'ordine aperto quant - quaeva e lo metto in QuaDiff
        If Not oCldGsor.GetQuaevaOFA(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, _
                                     NTSCInt(dttET.Rows(0)!et_anno), dttET.Rows(0)!et_serie.ToString, _
                                     NTSCInt(IIf(bNew, 0, dttET.Rows(0)!et_numdoc)), _
                                     dttGr.Rows(i)!tipo.ToString, NTSCInt(dttGr.Rows(i)!anno), _
                                     dttGr.Rows(i)!serie.ToString, NTSCInt(dttGr.Rows(i)!num), _
                                     NTSCInt(dttGr.Rows(i)!riga), QuaEvasa, QuaDisEvasa, _
                                     ColDisEvasa, ValDisEvasa, dttMo) Then Return False
        If dttMo.Rows.Count > 0 Then
          'QUANTO ANCORA DA EVADERE DA MOVORD  - QUANTO E' STATO EVASO IN QUESTO DOC
          dQta = ArrDbl(NTSCDec(dttMo.Rows(0)!QuaDiff) - NTSCDec(dttGr.Rows(i)!QUAEVA), 3)

          'nei documenti aperti devo aggiungere quanto era già evaso con il documento corrente
          If bNew = False Then dQta = ArrDbl(dQta + QuaEvasa, 3)

          'ora dQta contiene quanto deve essere ancora evaso
          If dQta <= 0 Then
            'La quantità evasa globale supera o è uguale a quella ordinata sull'ordine di origine

            If dttEC.Select("ec_oatipo = " & CStrSQL(dttGr.Rows(i)!Tipo.ToString) & _
                            " AND ec_oaanno = " & NTSCInt(dttGr.Rows(i)!anno).ToString & _
                            " AND ec_oaserie = " & CStrSQL(dttGr.Rows(i)!serie) & _
                            " AND ec_oanum = " & NTSCInt(dttGr.Rows(i)!num).ToString & _
                            " AND ec_oariga = " & NTSCInt(dttGr.Rows(i)!riga).ToString & _
                            " AND ec_oasalcon = 'S'").Length = 0 Then
              Select Case dttGr.Rows(i)!Tipo.ToString
                Case "$"
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128528992167694000, "E' obbligatorio evadere a saldo la riga |" & NTSCInt(dttGr.Rows(i)!riga).ToString & "| dell' ORDINE FORNITORE APERTO n° |" & NTSCInt(dttGr.Rows(i)!num).ToString & "| in quanto la quantità evasa è uguale o supera la quantità originariamente indicata.")))
                Case "V"
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128528992182202000, "E' obbligatorio evadere a saldo la riga |" & NTSCInt(dttGr.Rows(i)!riga).ToString & "| dell' IMPEGNO CLIENTE APERTO n° |" & NTSCInt(dttGr.Rows(i)!num).ToString & "| in quanto la quantità evasa è uguale o supera la quantità originariamente indicata.")))
              End Select
              Return False
            End If    'If dttEC.Select("ec_oatipo = " & NTSCStr(dttGr.Rows(0)!Tipo.ToString) & _
          End If    'If dQta <= 0 Then
        End If    'If dttMo.Rows.Count > 0 Then
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
      dttMo.Clear()
      dttGr.Clear()
    End Try
  End Function
  Public Overridable Function TestPreSalvaTestord_CheckCa2() As Boolean
    '----------------------------
    'faccio gli stessi test di beveboll TestPreSalvaTestmag_CheckCa2

    Dim dttAnagca As New DataTable
    Dim dttAnagca2 As New DataTable
    Dim strMsg As String = ""
    Dim nItem As Integer = 0
    Dim nCodcaca As Integer = 0
    Dim lConto As Integer = 0
    Dim lConto2 As Integer = 0

    Dim dtrMm() As DataRow = Nothing  'elenco righe da testare
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim nCodcove As Integer = 0
    Dim lContocg As Integer = 0
    Dim bOk As Boolean = False
    Dim nCodStab As Integer = 0
    Dim strTmp As String = ""

    Dim nCausale As Integer = 0
    Dim nCausale2 As Integer = 0
    Dim nCausaleImp As Integer = 0
    Dim strFlCi As String = " "
    Dim strFlCi2 As String = " "
    Dim strFlCiImp As String = " "
    Dim nXxcodcacad As Integer = 0        'tabcaca per materiali
    Dim nXxcodcacad2 As Integer = 0
    Dim nXxcodcacadImp As Integer = 0
    Dim nCodcacalv As Integer = 0         'tabcaca per lavorazioni
    Dim nEsist As Integer = 0
    Dim dttTmp As New DataTable

    Try
      '----------------------------
      'se non è attivo il modulo della CA2 esco
      If CBool(lModuliSupDittaDitt And bsModSupCAE) = False Then Return True
      If Not oCldGsor.CheckEsercizioInCa2(strDittaCorrente, NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString) Then Return True

      Select Case NTSCStr(dttET.Rows(0)!et_tipork).ToUpper
        Case "R", "O", "H", "X", "#"
          'unici tipi gestiti dalla CA2 (# per impegno ricambi da customer service, che andrà a finire in tipork Z)
        Case Else
          Return True
      End Select

      'visto che in movord non ci sono le causali di magazzino, il test CI lo prendo dalle causali del tipobf
      oCldGsor.ValCodiceDb(dttET.Rows(0)!et_tipobf.ToString, strDittaCorrente, "TABTPBF", "N", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        nCausale = NTSCInt(dttTmp.Rows(0)!tb_tcaumag)
        nCausaleImp = NTSCInt(dttTmp.Rows(0)!tb_tcauscap)
      End If
      dttTmp.Clear()
      If nCausale > 0 Then
        oCldGsor.ValCodiceDb(nCausale.ToString, strDittaCorrente, "TABCAUM", "N", "", dttTmp)
        nEsist = NTSCInt(dttTmp.Rows(0)!tb_esist)
        strFlCi = NTSCStr(dttTmp.Rows(0)!tb_testci)
        nCausale2 = NTSCInt(dttTmp.Rows(0)!tb_causec)
        nXxcodcacad = NTSCInt(dttTmp.Rows(0)!tb_codcacadd)
        nCodcacalv = NTSCInt(dttTmp.Rows(0)!tb_codcacalv)
      End If
      dttTmp.Clear()
      If nCausale2 > 0 Then
        oCldGsor.ValCodiceDb(nCausale2.ToString, strDittaCorrente, "TABCAUM", "N", "", dttTmp)
        strFlCi2 = NTSCStr(dttTmp.Rows(0)!tb_testci)
        nXxcodcacad2 = NTSCInt(dttTmp.Rows(0)!tb_codcacadd)
        dttTmp.Clear()
      End If
      If nCausaleImp > 0 Then
        oCldGsor.ValCodiceDb(nCausaleImp.ToString, strDittaCorrente, "TABCAUM", "N", "", dttTmp)
        strFlCiImp = NTSCStr(dttTmp.Rows(0)!tb_testci)
        nXxcodcacadImp = NTSCInt(dttTmp.Rows(0)!tb_codcacadd)
        dttTmp.Clear()
      End If

      '----------------------------
      'CONTROLLO DATI DEL CORPO

      'ESCLUDO LE RIGHE CHE NON MOVIMENTANO L'ANALITICA (NON DOVRANNO ANDARE IN CA)
      If dttET.Rows(0)!et_tipork.ToString = "H" Then
        'scarichi collegati 
        If strFlCiImp <> " " Then
          dtrMm = dttECIMP.Select("ec_quant <> 0 OR ec_valore <> 0", "ec_riga")
        Else
          dtrMm = dttEC.Select("ec_riga = -123")
        End If
      Else
        If strFlCi <> " " Or strFlCi2 <> " " Then
          dtrMm = dttEC.Select("ec_quant <> 0 OR ec_valore <> 0", "ec_riga")
        Else
          dtrMm = dttEC.Select("ec_riga = -123")
        End If
      End If
      For i = 0 To dtrMm.Length - 1
        For nItem = 1 To NTSCInt(IIf(nCausale2 <> 0, 2, 1))
          'sevo ripetere il giro 2 volte per le causali doppie
          If nItem = 1 Then
            'caso standard
            If dttET.Rows(0)!et_tipork.ToString = "H" Then
              If strFlCiImp.Trim = "" Then GoTo SALTATEST_CORPO
            Else
              If strFlCi.Trim = "" Then GoTo SALTATEST_CORPO
            End If
          Else
            'caso secondo magazzinor pe causali doppie
            If strFlCi2.Trim = "" Then GoTo SALTATEST_CORPO
          End If    'If nItem = 1 Then
          nCodcove = NTSCInt(dtrMm(i)!ec_controp)

          lConto = 0
          lConto2 = 0

          'verifico se la riga ha un conto di CG collegato a tabcove con contocg gestito a ca: se contocg = 0 è come se fosse gestito in CA
          dtrT = dsTabcove.Tables("TABCOVE").Select("tb_codcove = " & nCodcove.ToString)
          If NTSCInt(dtrT(0)!tb_concove) <> 0 And NTSCStr(dtrT(0)!xx_flci).Trim = "" Then GoTo SALTATEST_CORPO 'il conto CG non gestisce la CA
          If NTSCInt(dtrT(0)!tb_concove) = 0 And NTSCInt(dtrT(0)!tb_concova) = 0 Then GoTo SALTATEST_CORPO 'se non c'è ne conto cg ne conto ca salto
          lContocg = NTSCInt(dtrT(0)!tb_concove)

          '-------------------------
          'se sono qui vuol dire che la riga deve andare a finire in CA

          '-------------------------
          'determino la causale di CA
          If nItem = 1 Then
            If dttET.Rows(0)!et_tipork.ToString = "H" Then
              nCodcaca = nXxcodcacadImp
            Else
              nCodcaca = nXxcodcacad
            End If
            lConto = NTSCInt(dtrMm(i)!ec_contocontr)
          Else
            nCodcaca = nXxcodcacad2
            lConto = NTSCInt(dtrT(0)!tb_concova2)   'movimento con causali doppie: questo conto è quello da usarsi sulla riga di carico
          End If
          If nCodcaca = 0 Then
            If dttET.Rows(0)!et_tipork.ToString = "H" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129277347409199219, "ATTENZIONE: la riga di scarico |" & dtrMm(i)!ec_riga.ToString & "| (collegata alla riga di carico |" & dtrMm(i)!ec_rigaor.ToString & "|) contiene dei costi/ricavi da rilevare in CA ma nella tabelle delle causali di magazzino non è indicata la causale di Contabilità analitica da utilizzare")))
            Else
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129277115869277344, "ATTENZIONE: la riga |" & dtrMm(i)!ec_riga.ToString & "| contiene dei costi/ricavi da rilevare in CA ma nella tabelle delle causali di magazzino non è indicata la causale di Contabilità analitica da utilizzare")))
            End If
            Return False
          End If

          '-------------------------
          'decodifica del conto principale (movmag.mm_contocontr)
          If lConto = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129277351772011719, "ATTENZIONE!" & _
              "Il documento contiene dei costi/ricavi da rilevare in CA ma, nella tabella delle contropartite vendite, per il codice |" & NTSCInt(dtrMm(i)!ec_controp).ToString & "| non è indicata la contropartita di CA da utilizzare." & vbCrLf & _
              "Aggiornare la tabella delle contropartite e ricaricare il programma di Gestione Ordini/Impegni.")))
            Return False
          End If
          If dttAnagca.Rows.Count = 0 Then
            oCldGsor.ValCodiceDb(lConto.ToString, strDittaCorrente, "ANAGCA", "N", "", dttAnagca)
          Else
            If NTSCInt(dttAnagca.Rows(0)!ac_conto) <> lConto Then
              oCldGsor.ValCodiceDb(lConto.ToString, strDittaCorrente, "ANAGCA", "N", "", dttAnagca)
            End If
          End If
          If dttAnagca.Rows.Count = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129301651510830078, "ATTENZIONE: il documento contiene dei costi/ricavi da rilevare in CA sul conto |" & lConto.ToString & "| che non risulta essere presente nel piano dei conti di CA. (Contropartita |" & NTSCInt(dtrMm(i)!ec_controp).ToString & "|)")))
            Return False
          End If


          '-------------------------
          'cerco il conto contropartita da utilizzare per la seconda riga di ca (quella per la quadratura). 
          'lo prendo da tabcove.tb_concovag
          lConto2 = NTSCInt(dtrT(0)!tb_concovag)
          If lConto2 = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129277425994121094, "ATTENZIONE!" & vbCrLf & _
              "Il documento contiene dei costi/ricavi da rilevare in CA ma, nella tabella delle contropartite vendite, per il codice |" & NTSCInt(dtrMm(i)!ec_controp).ToString & "| non è indicata la contropartita di 'giro' da utilizzare." & vbCrLf & _
              "Aggiornare la tabella delle contropartite e ricaricare il programma di Gestione Ordini/Impegni.")))
            Return False
          End If
          If dttAnagca2.Rows.Count = 0 Then
            oCldGsor.ValCodiceDb(lConto2.ToString, strDittaCorrente, "ANAGCA", "N", "", dttAnagca2)
          Else
            If NTSCInt(dttAnagca2.Rows(0)!ac_conto) <> lConto2 Then
              oCldGsor.ValCodiceDb(lConto2.ToString, strDittaCorrente, "ANAGCA", "N", "", dttAnagca2)
            End If
          End If
          If dttAnagca2.Rows.Count = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129301652227705078, "ATTENZIONE: il documento contiene dei costi/ricavi da rilevare in CA sul conto |" & lConto2.ToString & "| che non risulta essere presente nel piano dei conti di CA. (Contropartita |" & NTSCInt(dtrMm(i)!ec_controp).ToString & "|)")))
            Return False
          End If

          '-------------------------
          'messaggio di base
          If dttET.Rows(0)!et_tipork.ToString <> "H" Then
            strMsg = oApp.Tr(Me, 129270529761875000, "ATTENZIONE. Sulla riga |" & dtrMm(i)!ec_riga.ToString & "| è richiesta dalla 'contabilità analitica duplice contabile' (causale di CA |" & nCodcaca.ToString & "|, conto |" & lConto.ToString & "|, contropartita |" & lConto2.ToString & "|) l'indicazione ")
          Else
            strMsg = oApp.Tr(Me, 129270518107939453, "ATTENZIONE. Sulla riga di scarico |" & dtrMm(i)!ec_riga.ToString & "| collegata alla riga di carico |" & dtrMm(i)!ec_rigaor.ToString & "| è richiesta dalla 'contabilità analitica duplice contabile'  (causale di CA |" & nCodcaca.ToString & "|, conto |" & lConto.ToString & "|, contropartita |" & lConto2.ToString & "|) l'indicazione")
          End If


          '-------------------------
          'INIZIO I TEST
          'gestione date competenza
          If NTSCInt(dtrT(0)!tb_concove) <> 0 Then
            If NTSCStr(dtrT(0)!xx_accperi).ToUpper <> dttAnagca.Rows(0)!ac_accperi.ToString.ToUpper Or _
              NTSCStr(dtrT(0)!xx_accperi).ToUpper <> dttAnagca2.Rows(0)!ac_accperi.ToString.ToUpper Then
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129272889980166015, " del 'RICHIEDI DATE' non è lo stesso sui conti coinvolti (conto CG |" & lContocg.ToString & "|).")))
              Return False
            End If
          End If
          'se carico da prod devo prendere le date dal T, non dall'U
          If dtrMm(i)!ec_tipork.ToString = "Y" Then
            Select Case dttAnagca.Rows(0)!ac_accperi.ToString.ToUpper
              Case "S"  'periodo di competenza
                If NTSCDate(dttEC.Select("ec_riga = " & dtrMm(i)!ec_rigaor.ToString)(0)!ec_datini).ToShortDateString = NTSCDate(dttEC.Select("ec_riga = " & dtrMm(i)!ec_rigaor.ToString)(0)!ec_datfin).ToShortDateString Then
                  ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129272905755615234, " della data inizio competenza DEVE essere DIVERSA dalla data di fine competenza nella riga di carico (i conti di CA sono gestiti a PERIODO di competenza economica).")))
                  Return False
                End If
              Case "D"  'data competenza
                If nDatiniCompMese = -1 Then
                  If NTSCDate(dttEC.Select("ec_riga = " & dtrMm(i)!ec_rigaor.ToString)(0)!ec_datini).ToShortDateString < NTSCDate(dttET.Rows(0)!et_datdoc).AddDays((NTSCDate(dttET.Rows(0)!et_datdoc).Day - 1) * -1).ToShortDateString Then
                    ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 130945660389472754, " la data inizio competenza SULLA RIGA DI CARICO: tale data è INFERIORE al primo del mese della data del documento (i conti di CA sono gestiti a DATA di competenza economica): proseguo ugualmente.")))
                  End If
                Else
                  If NTSCDate(dttEC.Select("ec_riga = " & dtrMm(i)!ec_rigaor.ToString)(0)!ec_datini).ToShortDateString < NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString Then
                    ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129509529847792968, " la data inizio competenza SULLA RIGA DI CARICO: tale data è INFERIORE alla data del documento (i conti di CA sono gestiti a DATA di competenza economica): proseguo ugualmente.")))
                  End If
                End If
              Case "N"  'date non gestite
                If NTSCDate(dttEC.Select("ec_riga = " & dtrMm(i)!ec_rigaor.ToString)(0)!ec_datini).ToShortDateString <> NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString Then
                  'ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129509529873066406, " della data inizio competenza DEVE essere uguale alla data del documento nella riga di carico (i conti di CA non gestiscono le date di competenza economica).")))
                  'Return False
                  'non do il messaggio e correggo al volo la data: se importo righe d'ordine o di nota di prel 
                  'è normale che la data di inizio sia uguale alla data dell'ordine/della nota che è sicuramente diversa dalla data di questo documento
                  dttEC.Select("ec_riga = " & dtrMm(i)!ec_rigaor.ToString)(0)!ec_datini = dttET.Rows(0)!et_datdoc
                  dttEC.Select("ec_riga = " & dtrMm(i)!ec_rigaor.ToString)(0).AcceptChanges()
                End If
            End Select
          Else
            Select Case dttAnagca.Rows(0)!ac_accperi.ToString.ToUpper
              Case "S"  'periodo di competenza
                If NTSCDate(dtrMm(i)!ec_datini).ToShortDateString = NTSCDate(dtrMm(i)!ec_datfin).ToShortDateString Then
                  ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129272892637089843, " della data inizio competenza DEVE essere DIVERSA dalla data di fine competenza (i conti di CA sono gestiti a PERIODO di competenza economica).")))
                  Return False
                End If
              Case "D"  'data competenza
                If nDatiniCompMese = -1 Then
                  If NTSCDate(dtrMm(i)!ec_datini).ToShortDateString < NTSCDate(dttET.Rows(0)!et_datdoc).AddDays((NTSCDate(dttET.Rows(0)!et_datdoc).Day - 1) * -1).ToShortDateString Then
                    ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 130945661665871418, " la data inizio competenza: tale data è INFERIORE al primo del mese della data del documento (i conti di CA sono gestiti a DATA di competenza economica): proseguo ugualmente.")))
                  End If
                Else
                  If NTSCDate(dtrMm(i)!ec_datini).ToShortDateString < NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString Then
                    ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129509518087333984, " la data inizio competenza: tale data è INFERIORE alla data del documento (i conti di CA sono gestiti a DATA di competenza economica): proseguo ugualmente.")))
                  End If
                End If
              Case "N"  'date non gestite
                If NTSCDate(dtrMm(i)!ec_datini).ToShortDateString <> NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString Then
                  'ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129272892183652343, " della data inizio competenza DEVE essere uguale alla data del documento (i conti di CA non gestiscono le date di competenza economica).")))
                  'Return False
                  'non do il messaggio e correggo al volo la data: se importo righe d'ordine o di nota di prel 
                  'è normale che la data di inizio sia uguale alla data dell'ordine/della nota che è sicuramente diversa dalla data di questo documento
                  dtrMm(i)!ec_datini = dttET.Rows(0)!et_datdoc
                  dtrMm(i).AcceptChanges()
                End If
            End Select
          End If

          'gestione centro
          If (dttAnagca.Rows(0)!ac_richcena.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richcena.ToString.ToUpper = "S") And NTSCInt(dtrMm(i)!ec_codcena) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129270519476562500, " del centro di costo/ricavo.")))
            Return False
          End If
          'gestione commessa
          If (dttAnagca.Rows(0)!ac_richcomm.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richcomm.ToString.ToUpper = "S") And NTSCInt(dtrMm(i)!ec_commeca) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129270519490800781, " della commessa.")))
            Return False
          End If
          'gestione linea/famiglia
          If (dttAnagca.Rows(0)!ac_richcfam.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richcfam.ToString.ToUpper = "S") And NTSCStr(dtrMm(i)!ec_codcfam).Trim = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129270519350644531, " della linea/famiglia.")))
            Return False
          End If
          'gestione divisione
          If (dttAnagca.Rows(0)!ac_richdivi.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richdivi.ToString.ToUpper = "S") And NTSCInt(dtrMm(i)!ec_coddivi) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129270519764716797, " della divisione.")))
            Return False
          End If
          'gestione cliente
          If (dttAnagca.Rows(0)!ac_richcli.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richcli.ToString.ToUpper = "S") And NTSCInt(dttET.Rows(0)!et_codcli) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129270520103916015, "ATTENZIONE. la gestione della 'contabilità analitica duplice contabile' richiede l'indicazione del cliente di CA in testata documento (il dato è richiesto dalla contropartita di CA indicata sulla riga |" & dtrMm(i)!ec_riga.ToString & "|.")))
            Return False
          End If
          'gestione stabilimento: prendo lo stabilimento dal magazzino 
          bOk = False
          If dttAnagca.Rows(0)!ac_richstab.ToString.ToUpper = "S" Or dttAnagca.Rows(0)!ac_richstab.ToString.ToUpper = "S" Then
            'il magaz 1 deve sempre essere presente ...
            nCodStab = 0
            If nItem = 1 Then
              nCodStab = NTSCInt(dsTabmaga.Tables("TABMAGA").Select("tb_codmaga = " & NTSCInt(dtrMm(i)!ec_magaz).ToString)(0)!tb_codstab)
            Else
              nCodStab = NTSCInt(dsTabmaga.Tables("TABMAGA").Select("tb_codmaga = " & NTSCInt(dtrMm(i)!ec_magaz2).ToString)(0)!tb_codstab)
            End If
            If nCodStab <> 0 Then bOk = True
            'non posso imporre lo stabilimento sempre sul magaz 1, visto che il documento potrebbe servire per 
            'spostare la merce dal magaz accettazione al magazzino di ca e nel magaz di accettazione non aver 
            'indicato lo stabilimento
            If bOk = False Then
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129271328571367187, " dello stabilimento/filiale/negozio ma nella tabella dei magazzini, per i magazzini indicati nella riga in analisi, non è riportato tale dato.")))
              Return False
            End If
          End If
          'gestione articolo: non possibile: non posso salvare documenti senza articolo !!!!

          'se ho indicato un centro ed anche una linea/comm/stab/divi/arti/.... ed il dato è richiesto o facoltativo (diversamente non viene salvato) il centro deve essere principale!
          If NTSCInt(dtrMm(i)!ec_codcena) <> 0 And (dttAnagca.Rows(0)!ac_richcena.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richcena.ToString.ToUpper <> "N") Then
            If (NTSCInt(dtrMm(i)!ec_commeca) <> 0 And (dttAnagca.Rows(0)!ac_richcomm.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richcomm.ToString.ToUpper <> "N")) Or _
               (NTSCStr(dtrMm(i)!ec_codcfam).Trim <> "" And (dttAnagca.Rows(0)!ac_richcfam.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richcfam.ToString.ToUpper <> "N")) Or _
               (NTSCInt(dtrMm(i)!ec_coddivi) <> 0 And (dttAnagca.Rows(0)!ac_richdivi.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richdivi.ToString.ToUpper <> "N")) Or _
               (NTSCInt(dttET.Rows(0)!et_codcli) <> 0 And (dttAnagca.Rows(0)!ac_richcli.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richcli.ToString.ToUpper <> "N")) Or _
               (nCodStab <> 0 And (dttAnagca.Rows(0)!ac_richstab.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richstab.ToString.ToUpper <> "N")) Or _
               ((dttAnagca.Rows(0)!ac_richarti.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richarti.ToString.ToUpper <> "N")) Then
              strTmp = oCldGsor.GetTipcenaCa2(strDittaCorrente, NTSCInt(dtrMm(i)!ec_codcena))
              If strTmp <> "P" And strTmp <> "*" Then
                Dim strTmp11 As String = oApp.Tr(Me, 129351511824082031, _
                        " di scarico collegata alla riga di carico |" & dtrMm(i)!ec_rigaor.ToString & "| ")
                Dim strTmp12 As String = oApp.Tr(Me, 130385101088266703, "se si indica Commessa o Linea/famiglia o " & _
                        "Divisione o Stabilimento/filiale/negozio o Cliente o Articolo " & _
                        "(e tale dato risulta obbligatorio o facontativo nell'anagrafica del sottoconto di CA|" & _
                        dttAnagca.Rows(0)!ac_conto.ToString & "| / |" & dttAnagca2.Rows(0)!ac_conto.ToString & "|) " & _
                        "l'eventuale centro indicato deve essere di tipo 'Principale'")
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129351509737929687, _
                        "ATTENZIONE: Riga |" & dtrMm(i)!ec_riga.ToString & "|") & _
                        IIf(dttET.Rows(0)!et_tipork.ToString = "H", strTmp11, "").ToString & _
                        ": " & vbCrLf & strTmp12))
                Return False
              End If
            End If
          End If

SALTATEST_CORPO:
        Next    'For nItem = 1 To NTSCInt(IIf(NTSCInt(dtrMm(i)!ec_controp2) <> 0, 2, 1))
      Next    'For i = 0 To dtrTmp.Length - 1

      '----------------------------
      'CONTROLLO LAVORAZIONI E QTA PRODOTTA
      If Not TestPreSalvaTestord_CheckCa2_Lavorazioni(nCausale, nEsist, strFlCi, nCodcacalv) Then Return False

      '----------------------------
      'CONTROLLO SPESE DI PIEDE
      If Not TestPreSalvaTestord_CheckCa2_SpesePiede() Then Return False

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
      dttAnagca.Clear()
      dttAnagca.Clear()
    End Try
  End Function
  Public Overridable Function TestPreSalvaTestord_CheckCa2_Lavorazioni(ByVal nCausale As Integer, ByVal nEsist As Integer, ByVal strFlci As String, ByVal nCodcacalv As Integer) As Boolean
    Dim nCodcove As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim dtrMMPadre() As DataRow = Nothing   'riga di movmag tipo T a cui si riferiscono le righe di lavorazione
    Dim lContocg As Integer = 0
    Dim nCodcaca As Integer = 0

    Dim lConto As Integer = 0
    Dim lConto2 As Integer = 0
    Dim dttAnagca As New DataTable
    Dim dttAnagca2 As New DataTable

    Dim strMsg As String = ""

    Dim dtrMm() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Dim dtrAtt() As DataRow = Nothing

    Try

      If dttET.Rows(0)!et_tipork.ToString <> "H" Then Return True

      '----------------------------
      'CONTROLLO LAVORAZIONI SU CARICHI DA PRODUZIONE

      'ESCLUDO LE RIGHE CHE NON MOVIMENTANO QTA o VALORE
      For Each dtrL As DataRow In dttASSRIS.Select("as_valore <> 0 OR as_valmo <> 0", "as_riga")

        dtrMMPadre = dttEC.Select("ec_riga = " & dtrL!as_riga.ToString)

        If bTerzista Then
          nCodcove = NTSCInt(dtrMMPadre(0)!ec_controp)
        Else
          nCodcove = NTSCInt(dtrL!as_controp)
        End If

        'verifico se la riga ha un conto di CG collegato a tabcove con contocg gestito a ca: se contocg = 0 è come se fosse gestito in CA
        dtrT = dsTabcove.Tables("TABCOVE").Select("tb_codcove = " & nCodcove.ToString)
        If NTSCInt(dtrT(0)!tb_concove) <> 0 And NTSCStr(dtrT(0)!xx_flci).Trim = "" Then GoTo SALTATEST_LAVORAZ 'il conto CG non gestisce la CA
        If NTSCInt(dtrT(0)!tb_concove) = 0 And NTSCInt(dtrT(0)!tb_concova) = 0 Then GoTo SALTATEST_LAVORAZ 'se non c'è ne conto cg ne conto ca salto
        lContocg = NTSCInt(dtrT(0)!tb_concove)

        '-------------------------
        'determino la causale di CA
        nCodcaca = nCodcacalv
        If nCodcaca = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129277368661601563, "ATTENZIONE: il documento contiene delle lavorazioni da rilevare in CA ma nella tabella delle causali di magazzino, per il codice |" & nCausale.ToString & "| non è indicata la causale di CA da utilizzare per le lavorazioni")))
          Return False
        End If

        '-------------------------
        'decodifica del conto principale (movmag.mm_contocontr se prod da terzista, altrimenti lavcent.lce_contocontr)
        If bTerzista Then
          lConto = NTSCInt(dtrMMPadre(0)!ec_contocontr)
        Else
          lConto = NTSCInt(dtrL!as_contocontr)
        End If

        If lConto = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129277364429902344, "ATTENZIONE: il documento contiene delle lavorazioni da rilevare in CA ma nella tabella delle contropartite vendite, per il codice |" & nCodcove.ToString & "| non è indicata la contropartita di CA da utilizzare")))
          Return False
        End If
        If dttAnagca.Rows.Count = 0 Then
          oCldGsor.ValCodiceDb(lConto.ToString, strDittaCorrente, "ANAGCA", "N", "", dttAnagca)
        Else
          If NTSCInt(dttAnagca.Rows(0)!ac_conto) <> lConto Then
            oCldGsor.ValCodiceDb(lConto.ToString, strDittaCorrente, "ANAGCA", "N", "", dttAnagca)
          End If
        End If

        '-------------------------
        'cerco il conto contropartita da utilizzare per la seconda riga di ca (quella per la quadratura). 
        'lo prendo da tabcove.tb_concovag
        lConto2 = NTSCInt(dsTabcove.Tables("TABCOVE").Select("tb_codcove = " & nCodcove.ToString)(0)!tb_concovag)
        If lConto2 = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129277426017880859, "ATTENZIONE" & vbCrLf & _
            "Il documento contiene dei costi/ricavi da rilevare in CA ma, nella tabella delle contropartite vendite, per il codice |" & nCodcove.ToString & "| non è indicata la contropartita di 'giro' da utilizzare." & vbCrLf & _
            "Aggiornare la tabella delle contropartite e ricaricare il programma di Gestione Ordini/Impegni.")))
          Return False
        End If
        If dttAnagca2.Rows.Count = 0 Then
          oCldGsor.ValCodiceDb(lConto2.ToString, strDittaCorrente, "ANAGCA", "N", "", dttAnagca2)
        Else
          If NTSCInt(dttAnagca2.Rows(0)!ac_conto) <> lConto2 Then
            oCldGsor.ValCodiceDb(lConto2.ToString, strDittaCorrente, "ANAGCA", "N", "", dttAnagca2)
          End If
        End If

        '-------------------------
        'messaggio di base
        strMsg = oApp.Tr(Me, 129271270978779297, "ATTENZIONE. Sulla fase di lavorazione |" & dtrL!as_fase.ToString & "| collegata alla riga di carico |" & dtrL!as_riga.ToString & "| per lavorazioni interne (oppure sulla riga della quantità prodotta per lavorazioni esterne) è richiesto dalla 'contabilità analitica duplice contabile' (causale |" & nCodcaca.ToString & "|, conto |" & lConto.ToString & "|, contropartita |" & lConto2.ToString & "|) l'indicazione")

        '-------------------------
        'INIZIO I TEST
        'gestione date competenza
        If NTSCInt(dtrT(0)!tb_concove) <> 0 Then
          If NTSCStr(dtrT(0)!xx_accperi).ToUpper <> dttAnagca.Rows(0)!ac_accperi.ToString.ToUpper Or _
             NTSCStr(dtrT(0)!xx_accperi).ToUpper <> dttAnagca2.Rows(0)!ac_accperi.ToString.ToUpper Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129272899189072265, " del 'RICHIEDI DATE' non è lo stesso sui conti coinvolti (conto CG |" & lContocg.ToString & "|).")))
            Return False
          End If
        End If
        Select Case dttAnagca.Rows(0)!ac_accperi.ToString.ToUpper
          Case "S"  'periodo di competenza
            If NTSCDate(dtrMMPadre(0)!ec_datini).ToShortDateString = NTSCDate(dtrMMPadre(0)!ec_datfin).ToShortDateString Then
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129272899369873047, " della data inizio competenza DEVE essere DIVERSA dalla data di fine competenza sulla riga di carico (i conti di CA sono gestiti a PERIODO di competenza economica).")))
              Return False
            End If
          Case "D"  'data competenza
          Case "N"  'date non gestite
            If NTSCDate(dtrMMPadre(0)!ec_datini).ToShortDateString <> NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString Then
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129272899354755859, " della data inizio competenza DEVE essere uguale alla data del documento sulla riga di carico (i conti di CA non gestiscono le date di competenza economica).")))
              Return False
            End If
        End Select


        'gestione centro
        If (dttAnagca.Rows(0)!ac_richcena.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richcena.ToString.ToUpper = "S") And NTSCInt(dtrMMPadre(0)!ec_codcena) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129272899334677734, " del centro di costo/ricavo.")))
          Return False
        End If
        'gestione commessa
        If (dttAnagca.Rows(0)!ac_richcomm.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richcomm.ToString.ToUpper = "S") And NTSCInt(dtrMMPadre(0)!ec_commeca) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129271271537568359, " della commessa.")))
          Return False
        End If
        'gestione linea/famiglia
        If (dttAnagca.Rows(0)!ac_richcfam.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richcfam.ToString.ToUpper = "S") And NTSCStr(dtrMMPadre(0)!ec_codcfam).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129271271521406250, " della linea/famiglia.")))
          Return False
        End If
        'gestione divisione: la prendo dalla riga 'T'
        If (dttAnagca.Rows(0)!ac_richdivi.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richdivi.ToString.ToUpper = "S") Then
          If NTSCInt(dtrMMPadre(0)!ec_coddivi) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129271280809033203, " della divisione (da indicare nella riga di carico prodotto).")))
            Return False
          End If
        End If
        'gestione cliente
        If (dttAnagca.Rows(0)!ac_richcli.ToString.ToUpper = "S" Or dttAnagca.Rows(0)!ac_richcli.ToString.ToUpper = "S") And NTSCInt(dttET.Rows(0)!et_codcli) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129271271822451172, "ATTENZIONE. la gestione della 'contabilità analitica duplice contabile' richiede l'indicazione del cliente di CA in testata documento (il dato è richiesto dalla contropartita di CA indicata sulla riga |" & dtrL!lce_riga.ToString & "|.")))
          Return False
        End If
        'gestione stabilimento: prendo lo stabilimento dal magazzino di carico
        If dttAnagca.Rows(0)!ac_richstab.ToString.ToUpper = "S" Or dttAnagca.Rows(0)!ac_richstab.ToString.ToUpper = "S" Then
          'il magaz 1 deve sempre essere presente ...
          If Not dsTabmaga.Tables("TABMAGA").Select("tb_codmaga = " & NTSCInt(dtrMMPadre(0)!ec_magaz).ToString)(0)!tb_codstab.ToString <> "0" Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129271328593291015, " dello stabilimento/filiale/negozio ma nella tabella dei magazzini, per i magazzini indicati nella riga in analisi, non è riportato tale dato.")))
            Return False
          End If
        End If
        'gestione articolo: non possibile: non posso salvare documenti senza articolo !!!!

        'se ho indicato un centro ed anche una linea/comm/stab/divi/arti/.... ed il dato è richiesto o facoltativo (diversamente non viene salvato) il centro deve essere principale!
        If NTSCInt(dtrMMPadre(0)!ec_codcena) <> 0 And (dttAnagca.Rows(0)!ac_richcena.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richcena.ToString.ToUpper <> "N") Then
          If (NTSCInt(dtrMMPadre(0)!ec_commeca) <> 0 And (dttAnagca.Rows(0)!ac_richcomm.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richcomm.ToString.ToUpper <> "N")) Or _
             (NTSCStr(dtrMMPadre(0)!ec_codcfam).Trim <> "" And (dttAnagca.Rows(0)!ac_richcfam.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richcfam.ToString.ToUpper <> "N")) Or _
             (NTSCInt(dtrMMPadre(0)!ec_coddivi) <> 0 And (dttAnagca.Rows(0)!ac_richdivi.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richdivi.ToString.ToUpper <> "N")) Or _
             (NTSCInt(dttET.Rows(0)!et_codcli) <> 0 And (dttAnagca.Rows(0)!ac_richcli.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richcli.ToString.ToUpper <> "N")) Or _
             (NTSCInt(dsTabmaga.Tables("TABMAGA").Select("tb_codmaga = " & NTSCInt(dtrMMPadre(0)!ec_magaz).ToString)(0)!tb_codstab) <> 0 And (dttAnagca.Rows(0)!ac_richstab.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richstab.ToString.ToUpper <> "N")) Or _
             ((dttAnagca.Rows(0)!ac_richarti.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richarti.ToString.ToUpper <> "N")) Then
            strTmp = oCldGsor.GetTipcenaCa2(strDittaCorrente, NTSCInt(dtrMMPadre(0)!ec_codcena))
            If strTmp <> "P" And strTmp <> "*" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129350903763222657, _
                      "ATTENZIONE: Riga la fase |" & dtrL!as_fase.ToString & "| collegata alla riga di carico |" & _
                      dtrL!as_riga.ToString & "| per lavorazioni interne (oppure sulla riga della quantità " & _
                      "prodotta per lavorazioni esterne):" & vbCrLf & "se si indica Commessa o Linea/famiglia o " & _
                      "Divisione o Stabilimento/filiale/negozio o Cliente o Articolo " & _
                      "(e tale dato risulta obbligatorio o facontativo nell'anagrafica del sottoconto di CA|" & _
                      dttAnagca.Rows(0)!ac_conto.ToString & "| / |" & dttAnagca2.Rows(0)!ac_conto.ToString & "|) " & _
                      "l'eventuale centro indicato deve essere di tipo 'Principale'")))
              Return False
            End If
          End If
        End If

SALTATEST_LAVORAZ:
      Next    'For Each dtrL As DataRow In dttLAVCENT.Select("", "lce_riga, lce_rigaa")



      '-------------------------------------
      'CONTROLLO QUANTITA' PRODOTTA SU CARICHI DA PRODUZIONE (prendo i dati dalla riga 'T')

      'determino la causale di CA
      dttAnagca.Clear()
      dttAnagca2.Clear()
      If NTSCInt(dttPecx.Rows(0)!tb_cauqtapro) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129271310695673828, "ATTENZIONE: il documento di produzione dovrà registrare la 'Contabilità analitica duplice contabile' ma in 'personalizzazione CA per ditta' non è indicata la causale da utilizzare per rilevare la quantità prodotta.")))
        Return False
      End If
      oCldGsor.ValCodiceDb(NTSCInt(dttPecx.Rows(0)!tb_cauqtapro).ToString, strDittaCorrente, "TABCACA", "N", "", dttAnagca)
      If NTSCStr(dttAnagca.Rows(0)!tb_dval).ToUpper <> "N" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129271311904072265, "ATTENZIONE: il documento di produzione dovrà registrare la 'Contabilità analitica duplice contabile' e in 'personalizzazione CA per ditta' deve essere indicata una causale da utilizzare per rilevare la quantità prodotta che non movimenta il VALORE.")))
        Return False
      End If
      If NTSCStr(dttAnagca.Rows(0)!tb_dqtapr).ToUpper <> "S" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129272852645185547, "ATTENZIONE: il documento di produzione dovrà registrare la 'Contabilità analitica duplice contabile' e in 'personalizzazione CA per ditta' deve essere indicata una causale da utilizzare per rilevare la quantità prodotta che ha spuntato 'Richiesta quantità prodotto'.")))
        Return False
      End If
      dttAnagca.Clear()

      '-------------------------
      'LOOP sulle righe T
      dtrMm = dttEC.Select("ec_quant <> 0", "ec_riga")
      For i = 0 To dtrMm.Length - 1
        If nEsist = 0 Then GoTo SALTATEST_QTAPROD
        nCodcove = NTSCInt(dtrMm(i)!ec_controp)
        lConto = 0    'NON SERVE: è l'unica regsitrazione in CA ad avere una sola riga !!!!!!!!!!

        'verifico se la riga ha un conto di CG collegato a tabcove con contocg gestito a ca: se contocg = 0 è come se fosse gestito in CA
        dtrT = dsTabcove.Tables("TABCOVE").Select("tb_codcove = " & nCodcove.ToString)
        If strFlci.Trim = "" And NTSCInt(dtrT(0)!tb_concova2) = 0 Then GoTo SALTATEST_QTAPROD 'il conto CG non gestisce la CA
        lContocg = NTSCInt(dtrT(0)!tb_concove)

        '-------------------------
        'cerco il conto contropartita da utilizzare
        'lo prendo da tabcove.tb_concovag
        lConto2 = NTSCInt(dtrT(0)!tb_concova2)
        If lConto2 = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129271302611542968, "ATTENZIONE: il carico da produzione deve rilevare in CA la quantità prodotta ma nella tabella delle contropartite vendite, per il codice |" & nCodcove.ToString & "| non è indicata la contropartita '2' da utilizzare")))
          Return False
        End If
        If dttAnagca2.Rows.Count = 0 Then
          oCldGsor.ValCodiceDb(lConto2.ToString, strDittaCorrente, "ANAGCA", "N", "", dttAnagca2)
        Else
          If NTSCInt(dttAnagca2.Rows(0)!ac_conto) <> lConto2 Then
            oCldGsor.ValCodiceDb(lConto2.ToString, strDittaCorrente, "ANAGCA", "N", "", dttAnagca2)
          End If
        End If

        '---------------------
        'messaggio di base
        strMsg = oApp.Tr(Me, 129271343103164062, "ATTENZIONE. Sulla riga |" & dtrMm(i)!ec_riga.ToString & "|, per la rilevazione della quantità prodotta sul conto |" & lConto2.ToString & "|, è richiesto dalla 'contabilità analitica duplice contabile' l'indicazione")

        '---------------------
        'INIZIO I TEST

        'gestione date competenza
        Select Case dttAnagca2.Rows(0)!ac_accperi.ToString.ToUpper
          Case "S"  'periodo di competenza
            If NTSCDate(dtrMm(i)!ec_datini).ToShortDateString = NTSCDate(dtrMm(i)!ec_datfin).ToShortDateString Then
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129272909602275390, " che la data inizio competenza sia DIVERSA dalla data di fine competenza sulla riga di carico (il conto di CA è gestito a PERIODO di competenza economica).")))
              Return False
            End If
          Case "D"  'data competenza
          Case "N"  'date non gestite
            If NTSCDate(dtrMm(i)!ec_datini).ToShortDateString <> NTSCDate(dttET.Rows(0)!et_datdoc).ToShortDateString Then
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129272909585556640, " che la data inizio competenza sia UGUALE alla data del documento sulla riga di carico (il conto di CA non è gestito a data di competenza economica).")))
              Return False
            End If
        End Select

        'gestione centro
        If dttAnagca2.Rows(0)!ac_richcena.ToString.ToUpper = "S" And NTSCInt(dtrMm(i)!ec_codcena) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129271341656464843, " del centro di costo/ricavo.")))
          Return False
        End If
        'gestione commessa
        If dttAnagca2.Rows(0)!ac_richcomm.ToString.ToUpper = "S" And NTSCInt(dtrMm(i)!ec_commeca) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129271341640703125, " della commessa.")))
          Return False
        End If
        'gestione linea/famiglia
        If dttAnagca2.Rows(0)!ac_richcfam.ToString.ToUpper = "S" And NTSCStr(dtrMm(i)!ec_codcfam).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129271341624296875, " della linea/famiglia.")))
          Return False
        End If
        'gestione divisione: la prendo dalla riga 'T'
        If dttAnagca2.Rows(0)!ac_richdivi.ToString.ToUpper = "S" And NTSCInt(dtrMm(i)!ec_coddivi) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129271341685263672, " della divisione.")))
          Return False
        End If
        'gestione cliente
        If dttAnagca2.Rows(0)!ac_richcli.ToString.ToUpper = "S" And NTSCInt(dttET.Rows(0)!et_codcli) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129271341712861328, "ATTENZIONE. la gestione della 'contabilità analitica duplice contabile' richiede l'indicazione del cliente di CA in testata documento (il dato è richiesto dalla contropartita di CA indicata sulla riga |" & dtrMm(i)!ec_riga.ToString & "|).")))
          Return False
        End If
        'gestione stabilimento: prendo lo stabilimento dal magazzino di carico
        If dttAnagca2.Rows(0)!ac_richstab.ToString.ToUpper = "S" Then
          'il magaz 1 deve sempre essere presente ...
          If Not dsTabmaga.Tables("TABMAGA").Select("tb_codmaga = " & NTSCInt(dtrMm(i)!ec_magaz).ToString)(0)!tb_codstab.ToString <> "0" Then
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & oApp.Tr(Me, 129271341737255859, " dello stabilimento/filiale/negozio ma nella tabella dei magazzini, per i magazzini indicati nella riga in analisi, non è riportato tale dato.")))
            Return False
          End If
        End If
        'gestione articolo: non possibile: non posso salvare documenti senza articolo !!!!

        'se ho indicato un centro ed anche una linea/comm/stab/divi/arti/.... ed il dato è richiesto o facoltativo (diversamente non viene salvato) il centro deve essere principale!
        If NTSCInt(dtrMm(i)!ec_codcena) <> 0 And dttAnagca2.Rows(0)!ac_richcena.ToString.ToUpper <> "N" Then
          If (NTSCInt(dtrMm(i)!ec_commeca) <> 0 And dttAnagca2.Rows(0)!ac_richcomm.ToString.ToUpper <> "N") Or _
             (NTSCStr(dtrMm(i)!ec_codcfam).Trim <> "" And dttAnagca2.Rows(0)!ac_richcfam.ToString.ToUpper <> "N") Or _
             (NTSCInt(dtrMm(i)!ec_coddivi) <> 0 And dttAnagca2.Rows(0)!ac_richdivi.ToString.ToUpper <> "N") Or _
             (NTSCInt(dttET.Rows(0)!et_codcli) <> 0 And dttAnagca2.Rows(0)!ac_richcli.ToString.ToUpper <> "N") Or _
             (NTSCInt(dsTabmaga.Tables("TABMAGA").Select("tb_codmaga = " & NTSCInt(dtrMm(i)!ec_magaz).ToString)(0)!tb_codstab) <> 0 And dttAnagca2.Rows(0)!ac_richstab.ToString.ToUpper <> "N") Or _
             (dttAnagca2.Rows(0)!ac_richarti.ToString.ToUpper <> "N") Then
            strTmp = oCldGsor.GetTipcenaCa2(strDittaCorrente, NTSCInt(dtrMm(i)!ec_codcena))
            If strTmp <> "P" And strTmp <> "*" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129351479827119140, _
                      "ATTENZIONE (riga |" & dtrMm(i)!ec_riga.ToString & "| relativa a quantità prodotta): se si indica Commessa o Linea/famiglia o " & _
                      "Divisione o Stabilimento/filiale/negozio o Cliente o Articolo " & _
                      "(e tale dato risulta obbligatorio o facontativo nell'anagrafica del sottoconto di CA|" & _
                      dttAnagca2.Rows(0)!ac_conto.ToString & "|) " & _
                      "l'eventuale centro indicato deve essere di tipo 'Principale'")))
              Return False
            End If
          End If
        End If

SALTATEST_QTAPROD:
      Next    'For i = 0 To dtrMm.Length - 1

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
      dttAnagca.Clear()
      dttAnagca2.Clear()
    End Try
  End Function
  Public Overridable Function TestPreSalvaTestord_CheckCa2_SpesePiede() As Boolean
    Dim dttPeve As New DataTable
    Dim dttPeac As New DataTable
    Dim dttTmp As New DataTable

    Dim dttAnagca As New DataTable
    Dim dttAnagca2 As New DataTable

    Dim nItem As Integer = 0

    Dim strMsg As String = ""
    Dim dImp As Decimal = 0
    Dim lContocg As Integer = 0

    Dim nCodcaca As Integer = 0
    Dim nCodcove As Integer = 0
    Dim lConto As Integer = 0
    Dim lConto2 As Integer = 0

    Dim dtrT() As DataRow = Nothing
    Dim strTmp As String = ""

    Try

      '----------------------------
      'CONTROLLO SPESE DI PIEDE
      oCldGsor.ValCodiceDb("1", strDittaCorrente, "TABPEVE", "N", "", dttPeve)
      oCldGsor.ValCodiceDb("1", strDittaCorrente, "TABPEAC", "N", "", dttPeac)

      'per le spese di piede verifico se centro/linea/commessa/.... sono presenti in base a quanto richiesto dai conti di CA
      nCodcaca = -1
      For nItem = 0 To 8
        Select Case nItem
          Case 0
            strMsg = oApp.Tr(Me, 129270495053027343, "sono indicate delle spese di trasporto")
            dImp = NTSCDec(dttET.Rows(0)!et_speacc)
            If bDocEmesso Then
              nCodcove = CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contrriac))
            Else
              nCodcove = CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontrriac))
            End If
          Case 1
            strMsg = oApp.Tr(Me, 129270495075419922, "sono indicate delle spese di incasso")
            dImp = NTSCDec(dttET.Rows(0)!et_speinc)
            If bDocEmesso Then
              nCodcove = CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contrriin))
            Else
              nCodcove = CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontrriin))
            End If
          Case 2
            strMsg = oApp.Tr(Me, 129270469335175781, "sono indicate delle spese di imballo")
            dImp = NTSCDec(dttET.Rows(0)!et_speimb)
            If bDocEmesso Then
              nCodcove = CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contrimba))
            Else
              nCodcove = CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontrimba))
            End If
          Case 3
            strMsg = oApp.Tr(Me, 129276530325173125, "sono presenti degli omaggi")
            dImp = NTSCDec(dttET.Rows(0)!et_totomag)
            If bDocEmesso Then
              nCodcove = CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contromag))
            Else
              nCodcove = CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontromag))
            End If
          Case 4
            strMsg = oApp.Tr(Me, 129277439002314453, "sono presenti degli abbuoni")
            dImp = NTSCDec(dttET.Rows(0)!et_abbuono)
            If dImp > 0 Then
              If bDocEmesso Then
                nCodcove = CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contrabat))
              Else
                nCodcove = CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontrabpa))
              End If
            Else
              If bDocEmesso Then
                nCodcove = CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contrabpa))
              Else
                nCodcove = CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontrabat))
              End If
            End If
          Case 5
            strMsg = oApp.Tr(Me, 129276530294372344, "è indicato un incasso/pagamento")
            dImp = NTSCDec(dttET.Rows(0)!et_pagato)
            oCldGsor.ValCodiceDb(NTSCInt(dttET.Rows(0)!et_codpaga).ToString, strDittaCorrente, "TABPAGA", "N", "", dttTmp)
            If dttTmp.Rows.Count > 0 Then
              nCodcove = CoveAggControp(NTSCInt(dttTmp.Rows(0)!tb_concassp))
            End If
            If nCodcove = 0 Then
              If bDocEmesso Then
                nCodcove = CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contrcas))
              Else
                nCodcove = CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontrcas))
              End If
            End If
          Case 6
            strMsg = oApp.Tr(Me, 129276531971471953, "è indicato un conto cliente/fornitore generico")
            dImp = NTSCDec(dttET.Rows(0)!et_totdoc)
            If dttET.Rows(0)!xx_tipo.ToString <> "F" Then
              nCodcove = CoveAggControp(NTSCInt(dttPecx.Rows(0)!tb_contrclistd))
            Else
              nCodcove = CoveAggControp(NTSCInt(dttPecx.Rows(0)!tb_contrfornstd))
            End If
            If nCodcove = 0 Then GoTo SALTATEST 'non intendo gestire l'analitica per clienti/fornitori
          Case 7
            strMsg = oApp.Tr(Me, 129277439230312500, "sono presenti dei bolli")
            dImp = NTSCDec(dttET.Rows(0)!et_bolli)
            If bDocEmesso Then
              nCodcove = CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contrribo))
            Else
              nCodcove = CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontrribo))
            End If
            If nCodcove = 0 Then GoTo SALTATEST 'non intendo gestire l'analitica per clienti/fornitori
        End Select

        If dImp = 0 Then GoTo SALTATEST

        If nCodcove = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129277385105439453, "Nel documento |" & strMsg & "| ma in personalizzazione acquisti e/o vendite non è indicata la contropartita ditta da utilizzare (con la CA attivata per questa spesa non è possibile utilizzare direttamente il sottoconto)")))
          Return False
        End If


        dtrT = dsTabcove.Tables("TABCOVE").Select("tb_codcove = " & nCodcove.ToString)
        If dtrT.Length = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129278035547041015, "Nel documento |" & strMsg & "| ma in personalizzazione acquisti e/o vendite è indicata una contropartita (|" & nCodcove.ToString & "|) non presente nella tabella delle 'Contropartite ditta'")))
          Return False
        End If
        If NTSCStr(dtrT(0)!xx_flci).Trim = "" Then GoTo SALTATEST 'il conto di cg non è gestito a ca
        If NTSCInt(dtrT(0)!tb_concove) = 0 And NTSCInt(dtrT(0)!tb_concova) = 0 Then GoTo SALTATEST 'se non c'è ne conto cg ne conto ca salto
        lContocg = NTSCInt(dtrT(0)!tb_concove)

        'se sono arrivato qui c'è qualche cosa da scrivere in ca ...

        '------------------------------
        'verifico la causale di CA (la prendo dal tipobf)
        If nCodcaca = -1 Then
          oCldGsor.ValCodiceDb(NTSCInt(dttET.Rows(0)!et_tipobf).ToString, strDittaCorrente, "TABTPBF", "N", "", dttTmp)
          nCodcaca = NTSCInt(dttTmp.Rows(0)!tb_codcacadd)
          dttTmp.Clear()
          If nCodcaca = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129276581815524688, "ATTENZIONE: il documento contiene delle spese di piede da rilevare in CA ma nell'anagrafica del tipo bolla/fattura non è stata indicata la causale di CA da utilizzare")))
            Return False
          End If
        End If

        '------------------------------
        'cerco conto e contropartita
        lConto = NTSCInt(dtrT(0)!tb_concova)
        If lConto = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129277401522333984, "ATTENZIONE. Nel documento |" & strMsg & "| da rilevare in contabilità analitica ma in 'Contropartite ditta', per il codice |" & nCodcove.ToString & "|, non è stato indicato il conto CA da utilizzare")))
          Return False
        End If
        oCldGsor.ValCodiceDb(lConto.ToString, strDittaCorrente, "ANAGCA", "N", "", dttAnagca)

        Select Case nItem
          'per le spese che non devono essere rilevate in priana2 direttamente da movmag il conto di giro non serve
          Case 3, 4, 5, 6
            lConto2 = lConto
            dttAnagca2 = dttAnagca
          Case Else
            lConto2 = NTSCInt(dtrT(0)!tb_concovag)
            If lConto2 = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129277401589609375, "ATTENZIONE. Nel documento |" & strMsg & "| da rilevare in contabilità analitica ma in 'Contropartite ditta', per il codice |" & nCodcove.ToString & "|, non è stato indicato il conto di 'giro' da utilizzare")))
              Return False
            End If
            oCldGsor.ValCodiceDb(lConto2.ToString, strDittaCorrente, "ANAGCA", "N", "", dttAnagca2)
        End Select

        'gestione date competenza
        If dttAnagca.Rows(0)!ac_accperi.ToString.ToUpper <> dttAnagca2.Rows(0)!ac_accperi.ToString.ToUpper Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129273902500107421, "ATTENZIONE. Nel documento |" & strMsg & "| da rilevare in contabilità analitica (conto |" & lConto & "|, contropartita |" & lConto2.ToString & "|) ma sui conti il campo 'Richiedi date' non hanno la stessa impostazione, oppure sono settati a 'Periodo competenza economica'")))
          Return False
        End If
        'gestione centro
        If (dttAnagca.Rows(0)!ac_richcena.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richcena.ToString.ToUpper = "S") And NTSCInt(dttET.Rows(0)!et_codcena) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129270466593515625, "ATTENZIONE. Nel documento |" & strMsg & "| da rilevare in contabilità analitica (conto |" & lConto & "|, contropartita |" & lConto2.ToString & "|) è richiesta l'indicazione del centro di costo/ricavo, ma nella testata del documento non è stato indicato nessun centro di CA.")))
          Return False
        End If
        'gestione commessa
        If (dttAnagca.Rows(0)!ac_richcomm.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richcomm.ToString.ToUpper = "S") And NTSCInt(dttET.Rows(0)!et_commeca) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129270467749023437, "ATTENZIONE. Nel documento |" & strMsg & "| da rilevare in contabilità analitica (conto |" & lConto & "|, contropartita |" & lConto2.ToString & "|) è richiesta l'indicazione della commessa, ma nella testata del documento non è stata indicata nessuna commessa di CA.")))
          Return False
        End If
        'gestione linea/famiglia
        If (dttAnagca.Rows(0)!ac_richcfam.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richcfam.ToString.ToUpper = "S") And NTSCStr(dttET.Rows(0)!et_codcfam).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129270478624580078, "ATTENZIONE. Nel documento |" & strMsg & "| da rilevare in contabilità analitica (conto |" & lConto & "|, contropartita |" & lConto2.ToString & "|) è richiesta l'indicazione della linea/famiglia, ma nella testata del documento non è stata indicata nessuna linea/famiglia di CA.")))
          Return False
        End If
        'gestione divisione
        If (dttAnagca.Rows(0)!ac_richdivi.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richdivi.ToString.ToUpper = "S") And NTSCInt(dttET.Rows(0)!et_coddivi) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129270478593066406, "ATTENZIONE. Nel documento |" & strMsg & "| da rilevare in contabilità analitica (conto |" & lConto & "|, contropartita |" & lConto2.ToString & "|) è richiesta l'indicazione della divisione, ma nella testata del documento non è stata indicata nessuna divisione di CA.")))
          Return False
        End If
        'gestione cliente
        If (dttAnagca.Rows(0)!ac_richcli.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richcli.ToString.ToUpper = "S") And NTSCInt(dttET.Rows(0)!et_codcli) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129270478578027343, "ATTENZIONE. Nel documento |" & strMsg & "| da rilevare in contabilità analitica (conto |" & lConto & "|, contropartita |" & lConto2.ToString & "|) è richiesta l'indicazione del cliente, ma nella testata del documento non è stato indicato nessun cliente di CA.")))
          Return False
        End If
        'gestione stabilimento: prendo lo stabilimento dal magazzino 1 (SEMPRE)
        If dttAnagca.Rows(0)!ac_richstab.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richstab.ToString.ToUpper = "S" Then
          If NTSCInt(dttET.Rows(0)!et_magaz) = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129270495146621093, "ATTENZIONE. Nel documento |" & strMsg & "| da rilevare in contabilità analitica (conto |" & lConto & "|, contropartita |" & lConto2.ToString & "|) è richiesta l'indicazione dello stabilimento/filiale/negozio, ma nella testata del documento non è stato indicato il magazzino principale / magazzino di carico da cui prelevare tale informazione.")))
            Return False
          Else
            If dsTabmaga.Tables("TABMAGA").Select("tb_codmaga = " & NTSCInt(dttET.Rows(0)!et_magaz).ToString)(0)!tb_codstab.ToString = "0" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129270477078359375, "ATTENZIONE. Nel documento |" & strMsg & "| da rilevare in contabilità analitica (conto |" & lConto & "|, contropartita |" & lConto2.ToString & "|) è richiesta l'indicazione dello stabilimento/filiale/negozio, ma nella testata del documento, sul magazzino principale (|" & dttET.Rows(0)!et_magaz.ToString & "|) non è stato indicato nessuno stabilimento/filiale/negozio di CA.")))
              Return False
            End If
          End If
        End If
        'gestione articolo: non possibile
        If dttAnagca.Rows(0)!ac_richarti.ToString.ToUpper = "S" Or dttAnagca2.Rows(0)!ac_richarti.ToString.ToUpper = "S" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129270478559462890, "ATTENZIONE. Nel documento |" & strMsg & "| da rilevare in contabilità analitica (conto |" & lConto & "|, contropartita |" & lConto2.ToString & "|) è richiesta l'indicazione dell'articolo, ma negli importi di piede non è possibile utilizzare conti che richiedono tale informazione.")))
          Return False
        End If

        'se ho indicato un centro ed anche una linea/comm/stab/divi/arti/.... ed il dato è richiesto o facoltativo (diversamente non viene salvato) il centro deve essere principale!
        If NTSCInt(dttET.Rows(0)!et_codcena) <> 0 And (dttAnagca.Rows(0)!ac_richcena.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richcena.ToString.ToUpper <> "N") Then
          If (NTSCInt(dttET.Rows(0)!et_commeca) <> 0 And (dttAnagca.Rows(0)!ac_richcomm.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richcomm.ToString.ToUpper <> "N")) Or _
             (NTSCStr(dttET.Rows(0)!et_codcfam).Trim <> "" And (dttAnagca.Rows(0)!ac_richcfam.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richcfam.ToString.ToUpper <> "N")) Or _
             (NTSCInt(dttET.Rows(0)!et_coddivi) <> 0 And (dttAnagca.Rows(0)!ac_richdivi.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richdivi.ToString.ToUpper <> "N")) Or _
             (NTSCInt(dsTabmaga.Tables("TABMAGA").Select("tb_codmaga = " & NTSCInt(dttET.Rows(0)!et_magaz).ToString)(0)!tb_codstab) <> 0 And (dttAnagca.Rows(0)!ac_richstab.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richstab.ToString.ToUpper <> "N")) Or _
             (NTSCInt(dttET.Rows(0)!et_codcli) <> 0 And (dttAnagca.Rows(0)!ac_richcli.ToString.ToUpper <> "N" Or dttAnagca2.Rows(0)!ac_richcli.ToString.ToUpper <> "N")) Then
            strTmp = oCldGsor.GetTipcenaCa2(strDittaCorrente, NTSCInt(dttET.Rows(0)!et_codcena))
            If strTmp <> "P" And strTmp <> "*" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129351482687187500, "ATTENZIONE. Nel documento |" & strMsg & _
                          "| da rilevare in contabilità analitica: se si indica Commessa o Linea/famiglia o " & _
                          "Divisione o Stabilimento/filiale/negozio o Cliente o Articolo " & _
                          "(e tale dato risulta obbligatorio o facontativo nell'anagrafica del sottoconto di CA |" & _
                          dttAnagca.Rows(0)!ac_conto.ToString & "| / |" & dttAnagca2.Rows(0)!ac_conto.ToString & "|) " & _
                          "l'eventuale centro indicato deve essere di tipo 'Principale'")))
              Return False
            End If
          End If
        End If
SALTATEST:
      Next    'For nItem = 0 To 6

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
      dttPeve.Clear()
      dttPeac.Clear()
      dttAnagca.Clear()
      dttAnagca2.Clear()
    End Try
  End Function
  Public Overridable Function TestPreSalvaTestord_CheckFido() As Boolean
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable
    Try
      If strProgChiamante = "BEECIMPO" Then Return True 'Posso salvare sempre se sto importando dal modulo ecommerce
      '----------------------------------------
      'controllo se il documento è bloccato
      If strContrFidoInsolinInsOrd = "S" And bNew And bDocEmesso Then
        If Not (NTSCStr(dttET.Rows(0)!et_tipork) = "Q" And bNoCheckFidoSuPrev) Then
          If bEraBloccato Then
            CheckFidoInsol(True, NTSCInt(dttET.Rows(0)!et_conto), dttET, bOk)
          Else
            CheckFidoInsol(False, NTSCInt(dttET.Rows(0)!et_conto), dttET, bOk)
          End If

          If bOk Then
            'Ora se richiesto rileva il tipo di blocco presente in questo momento (dopo eventualmente la
            'routine fido/insoluti che blocca il soggetto
            If bNew Then
              If bRilevaBloccoDaAnagra Then
                oCldGsor.ValCodiceDb(dttET.Rows(0)!et_conto.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp)
                dttET.Rows(0)!et_blocco = NTSCStr(dttTmp.Rows(0)!an_blocco)
                dttTmp.Clear()
              End If
            End If
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128521109933568000, _
                              "Blocco cliente : conferma dell' impegno impossibile se non verrà sbloccato in anagrafica!")))
            bEraBloccato = True
            Return False
          End If
        End If
      End If    ' If strContrFidoInsolinInsOrd = "S" And bNew And bDocEmesso Then

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
  Public Overridable Function TestPreSalvaTestord_GenAutCommeca() As Boolean
    Dim bTipoGen2New As Boolean = False
    Dim strTipogen As String = ""
    Dim dtrT() As DataRow = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(dttET.Rows(0)!et_tipork) <> "R") And (NTSCStr(dttET.Rows(0)!et_tipork) <> "#") Then Return True
      '--------------------------------------------------------------------------------------------------------------
      If (bGenNumCommecaAutR = False) Or (bGenNumCommecaAutR_AllaFine = False) Then Return True
      '--------------------------------------------------------------------------------------------------------------
      strTipogen = oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "TipoGenNumCommeca", "1", " ", "1")
      '--------------------------------------------------------------------------------------------------------------
      If strTipogen = "2" Then
        bTipoGen2New = CBool(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BSORGSOR", "OPZIONI", ".", "TipoGenNumCommeca2New", "0", " ", "0"))
      End If
      If (strTipogen <> "2") Or ((strTipogen = "2") And (bTipoGen2New = False)) Then
      Else
        If Asc(NTSCStr(dttET.Rows(0)!et_serie)) >= 74 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129866578161312070, "Attenzione!" & _
            "Non è consentita una serie superiore a 'I' con questo tipo di generazione commessa." & vbCrLf & _
            "Aggiornamento ordine/impegno non possibile.")))
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (strTipogen > "1") And (strTipogen < "4") Then
        dtrT = dttEC.Select("ec_riga > 99 AND ec_commeca = 0")
        If dtrT.Length > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129866573907564748, "Attenzione!" & _
            "Esistono righe con numero superiore a 99 e numero di commessa a zero." & vbCrLf & _
            "Con questo tipo di generazione commessa non è possibile la generazione automatica della commessa." & vbCrLf & _
            "Aggiornamento ordine/impegno non possibile.")))
          Return False
        End If
      End If
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

  Public Overridable Function TestPreCancellaTestord(ByRef bSetStatoOrdlist As Boolean) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim evnt As NTSEventArgs = Nothing
    Dim dttTmp As New DataTable
    Dim strMsg As String = ""
    Try
      '--------------------------
      'se sono presenti righe evase non faccio cancellare
      If dttEC.Select("ec_quaeva <> 0 OR ec_quapre <> 0").Length > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526288586602000, "Impossibile eliminare il documento in quanto possiede una o più righe con quantità evasa o prenotata diversa da 0")))
        Return False
      End If

      '--------------------------
      'test oridni di produzione
      If NTSCStr(dttET.Rows(0)!et_tipork) = "H" Then
        'non devono essere evase righe di impegni
        If dttECIMP.Select("ec_quaeva <> 0 OR ec_quapre <> 0").Length > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526289739598000, "Impossibile eliminare l'ordine di produzione in quanto possiede una o più righe di impegni di produzione con quantità evasa o prenotata diversa da 0")))
          Return False
        End If

        'Controlla che non ci siano 'avanzamenti di lav.' associati
        If dttATTIVIT.Select("at_qtaes <> 0 OR at_tempattees <> 0 OR at_tempesees <> 0").Length > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526290788038000, "Impossibile eliminare l'ordine di produzione in quanto possiede una o più attività parzialmente o totalmente evasa (in avanzamenti/carichi di prod.)")))
          Return False
        End If

      End If    'If NTSCStr(dttET.Rows(0)!et_tipork) = "H" Then

      '--------------------------
      If Not Accconf_CheckSalvaCancella("CANC") Then Return False

      '--------------------------
      'Se esiste il modulo RDA ed esistono righe in ORDLIST chiede se aggiornare ol_stato
      'e poi sgancia sempre i campi di ORDLIST eventualmente collegati a TESTORD/MOVORD
      If bModuloRA Then
        If oCldGsor.CheckRDAcollegate(strDittaCorrente, NTSCStr(dttET.Rows(0)!et_tipork), _
                                      NTSCInt(dttET.Rows(0)!et_anno), NTSCStr(dttET.Rows(0)!et_serie), _
                                      NTSCInt(dttET.Rows(0)!et_numdoc)) Then
          Select Case nSetStatoRigaOrdlist
            Case 0
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128526304772070000, _
                          "Esiste una Richiesta d'offerta collegata all'ordine che si intende cancellare." & vbCrLf & _
                          "Mantenere chiusa la RDO collegata?" & vbCrLf & vbCrLf & _
                          "Se si preme 'Sì' la Richiesta d'offerta sarà mantenuta chiusa." & vbCrLf & _
                          "Se si preme 'No' la Richiesta d'offerta sarà riaperta."))
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = "NO" Then bSetStatoOrdlist = True
            Case 1
              bSetStatoOrdlist = True
            Case 2
              bSetStatoOrdlist = False
          End Select
        End If
      End If    'If bModuloRA Then

      '------------------------------
      'se c'è il collegamento con NETPRO controllo che il documento non sia in evasione ed eventualmente avviso
      If Not NetProCheckOrdModifCanc(False) Then Return False

      If Not oCldGsor.CheckOrdInAvlavp(strDittaCorrente, NTSCStr(dttET.Rows(0)!et_tipork), _
                                      NTSCInt(dttET.Rows(0)!et_anno), NTSCStr(dttET.Rows(0)!et_serie), _
                                      NTSCInt(dttET.Rows(0)!et_numdoc), dttTmp) Then Return False
      If dttTmp.Rows.Count > 0 Then
        strMsg = oApp.Tr(Me, 130771929449759502, "ATTENZIONE: una o più lavorazioni contenute nell'ordine sono già state avviate/completate." & vbCrLf & _
                "Prima cancellare tutte le lavorazioni collegate all'ordine dall'avanzamento lavorazioni." & vbCrLf & _
                "Progressivi di lavorazione: " & vbCrLf)
        For Each dtrT1 As DataRow In dttTmp.Rows
          strMsg += dtrT1!lce_progr.ToString & ", "
        Next
        strMsg = strMsg.Substring(0, strMsg.Length - 2)
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
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function AggDisimpegno() As Boolean
    Dim dtrTmp() As DataRow = Nothing
    Dim i As Integer = 0
    Dim dttMo As New DataTable
    Dim dQta As Decimal = 0
    Dim dCol As Decimal = 0
    Dim dVal As Decimal = 0
    Dim QuaEvasa As Decimal = 0
    Dim QuaDisEvasa As Decimal = 0
    Dim ColDisEvasa As Decimal = 0
    Dim ValDisEvasa As Decimal = 0
    Dim strFlevas As String = ""

    Dim dttGr As New DataTable
    Dim oDttgr As New CLEGROUPBY
    Try
      '----------------------------
      'Tratta il disimpegnato in conto sugli ORDINI
      dtrTmp = dttEC.Select("ec_oasalcon = 'C' AND ec_oanum > 0 AND ec_oaqtadis > 0")
      For i = 0 To dtrTmp.Length - 1
        With dtrTmp(i)
          If oCldGsor.TrovaNdec(0) = 0 Then
            !ec_oavaldis = Fix(NTSCDec(!ec_oavaldis) / NTSCDec(!ec_oaqtadis)) * NTSCDec(!ec_quant)
          Else
            !ec_oavaldis = Fix(NTSCDec(!ec_oavaldis) / NTSCDec(!ec_oaqtadis) * 100 * NTSCDec(!ec_quant)) / 100
          End If
          !ec_oaqtadis = NTSCDec(!ec_quant)
          !ec_oacoldis = NTSCDec(!ec_colli)
        End With
      Next

      '----------------------------
      'ordini/impegni aperti: devo aggiornare il disimpegno 
      'prendo qta,colli e valore da ordine aperto, aggiungo quanto evaso in precedenza da me se documento aperto 
      dtrTmp = dttEC.Select("ec_oasalcon = 'S' AND ec_oanum > 0", "ec_riga")
      For i = 0 To dtrTmp.Length - 1
        With dtrTmp(i)
          If Not oCldGsor.GetQuaevaOFA(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, _
                             NTSCInt(dttET.Rows(0)!et_anno), dttET.Rows(0)!et_serie.ToString, _
                             NTSCInt(IIf(bNew, 0, dttET.Rows(0)!et_numdoc)), _
                             !ec_oatipo.ToString, NTSCInt(!ec_oaanno), _
                             !ec_oaserie.ToString, NTSCInt(!ec_oanum), _
                             NTSCInt(!ec_oariga), QuaEvasa, QuaDisEvasa, _
                             ColDisEvasa, ValDisEvasa, dttMo) Then Return False

          If dttMo.Rows.Count > 0 Then
            strFlevas = NTSCStr(dttMo.Rows(0)!mo_flevas).ToUpper
            dQta = NTSCDec(dttMo.Rows(0)!QuaDiff)
            dCol = NTSCDec(dttMo.Rows(0)!ColDiff)
            dVal = NTSCDec(dttMo.Rows(0)!mo_valore)
            If dQta < 0 Or strFlevas = "S" Then dQta = 0
            If dCol < 0 Or strFlevas = "S" Then dCol = 0
            If dVal < 0 Or strFlevas = "S" Then dVal = 0
            'Considera eventualmente il doc. aperto consolidato
            If bNew = False Then
              dQta = dQta + QuaDisEvasa
              dCol = dCol + ColDisEvasa
              dVal = dVal + ValDisEvasa
            End If
            'Considera il doc. sul temporaneo
            dttGr.Clear()
            If Not oDttgr.NTSGroupBy(dttEC, dttGr, "Sum(ec_oaqtadis) AS QuaEvasa, Sum(ec_oacoldis) AS ColEvasa, Sum(ec_oavaldis) AS ValEvasa", _
                                    "ec_oatipo = " & CStrSQL(dtrTmp(i)!ec_oatipo) & " AND ec_oaanno = " & dtrTmp(i)!ec_oaanno.ToString & _
                                    " AND ec_oaserie = " & CStrSQL(dtrTmp(i)!ec_oaserie) & " AND ec_oanum = " & dtrTmp(i)!ec_oanum.ToString & _
                                    " AND ec_oariga = " & dtrTmp(i)!ec_oariga.ToString & " AND ec_oasalcon = 'C'", _
                                    "") Then
              Return False
            End If
            If dttGr.Rows.Count > 0 Then
              dQta = dQta - NTSCDec(dttGr.Rows(0)!QuaEvasa)
              dCol = dCol - NTSCDec(dttGr.Rows(0)!ColEvasa)
              dVal = dVal - NTSCDec(dttGr.Rows(0)!ValEvasa)
            End If

            'aggiorno la riga in analisi
            dtrTmp(i)!ec_oaqtadis = dQta
            dtrTmp(i)!ec_oacoldis = dCol
            dtrTmp(i)!ec_oavaldis = dVal

          End If    'If dttMo.Rows.Count > 0 Then
        End With    'With dtrTmp(i)
      Next    'For i = 0 To dtrTmp.Length - 1

      If bModTCO Then
        If dttECTC.Columns.Contains("ec_qtadis01") Then
          If Not AggDisimpegnoTc(dttET.Rows(0)!et_tipork.ToString, dttEC, dttECTC) Then Return False
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
      dttMo.Clear()
      dttGr.Clear()
    End Try
  End Function

  Public Overridable Function AggDisimpegnoTc(ByVal strTipork As String, ByRef dttIn As DataTable, ByRef dttInTC As DataTable) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim dtrT2() As DataRow = Nothing
    Dim dtrTtc() As DataRow = Nothing
    Dim l As Integer = 0
    Dim i As Integer = 0
    Dim n As Integer = 0
    Dim dttMo As New DataTable
    Dim dQta(25) As Decimal
    Dim QuaEvasa(25) As Decimal
    Dim strFlevas As String = ""

    Try

      '--------------------------------
      'Tratta il disimpegnato in conto sugli ORDINI
      'su tco la quantità disimpegnata è semre uguale alla quantità (non posso evadere quantità superiori al residuo ordinato)
      dtrT = dttIn.Select("xxo_codtagl <> 0 AND ec_oasalcon = 'C' AND ec_oanum > 0 AND ec_oaqtadis > 0")
      For l = 0 To dtrT.Length - 1
        With dtrT(l)
          dtrTtc = dttInTC.Select("ec_riga = " & !ec_riga.ToString)
          If dtrTtc.Length > 0 Then
            For i = 1 To 24
              dtrTtc(0)("ec_qtadis" & i.ToString("00")) = NTSCDec(dtrTtc(0)("ec_quant" & i.ToString("00")))
            Next
          End If
        End With    'With dtrT(i)
      Next    'For l = 0 To dtrT.Length - 1

      '--------------------------------
      'Tratta il disimpegnato a saldo sugli ORDINI
      dtrT = dttIn.Select("xxo_codtagl <> 0 AND ec_oasalcon = 'S' AND ec_oanum > 0", "ec_riga")
      For i = 0 To dtrT.Length - 1
        With dtrT(i)
          If Not oCldGsor.GetQuaevaOFAtc(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, _
                             NTSCInt(dttET.Rows(0)!et_anno), dttET.Rows(0)!et_serie.ToString, _
                             NTSCInt(IIf(bNew, 0, dttET.Rows(0)!et_numdoc)), _
                             !ec_oatipo.ToString, NTSCInt(!ec_oaanno), _
                             !ec_oaserie.ToString, NTSCInt(!ec_oanum), _
                             NTSCInt(!ec_oariga), QuaEvasa, dttMo) Then Return False
          If dttMo.Rows.Count > 0 Then
            strFlevas = NTSCStr(dttMo.Rows(0)!FLEVAS).ToUpper
            For l = 1 To 24
              dQta(l) = NTSCDec(dttMo.Rows(0)("QuaDiff" & l.ToString("00")))
              If (dQta(l) < 0) Or (strFlevas = "S") Then dQta(l) = 0
            Next
            'Se vi erano valori da evadere negativi non lo stornava correttamente
            'in MOVORD/ARTPRO
            'Considera eventualmente il doc. aperto consolidato
            If bNew = False Then
              For l = 1 To 24
                dQta(l) = dQta(l) + QuaEvasa(l)
              Next
            End If

            'Considera il doc. sul temporaneo
            dtrT2 = dttIn.Select("ec_oatipo = " & CStrSQL(dtrT(i)!ec_oatipo) & " AND ec_oaanno = " & dtrT(i)!ec_oaanno.ToString & _
                                 " AND ec_oaserie = " & CStrSQL(dtrT(i)!ec_oaserie) & " AND ec_oanum = " & dtrT(i)!ec_oanum.ToString & _
                                 " AND ec_oariga = " & dtrT(i)!ec_oariga.ToString & " AND ec_oasalcon = 'C'")
            For n = 0 To dtrT2.Length - 1
              dtrTtc = dttInTC.Select("ec_riga = " & dtrT2(n)!ec_riga.ToString)
              For l = 1 To 24
                dQta(l) = dQta(l) - NTSCDec(dtrTtc(0)("ec_qtadis" & l.ToString("00")))
              Next
            Next

            'aggiorno la riga in analisi
            dtrTtc = dttInTC.Select("ec_riga = " & dtrT(i)!ec_riga.ToString)
            If dtrTtc.Length > 0 Then
              For l = 1 To 24
                dtrTtc(0)("ec_qtadis" & l.ToString("00")) = dQta(l)
              Next
            End If
          End If    'If dttMo.Rows.Count > 0 Then
        End With    'With dtrT(i)
      Next    'For i = 0 To dtrT.Length - 1

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
      dttMo.Clear()
    End Try
  End Function

  Public Overridable Function OrdStornaImportExport(ByVal strDitta As String, ByVal strTipoRk As String, _
                                                    ByVal nAnno As Integer, ByVal strSerie As String, _
                                                    ByVal lNumord As Integer, ByVal bMsg As Boolean) As Boolean
    'utlizzato dall'import/export per cancellare keyord, artpro, artproc, lotcpro
    'correggere movord per ordini forn aperti come se si stesse cancellando il documento
    Dim strMsg As String = ""
    Dim strDestipork As String = ""
    Dim bPM, bModTCO, bDocEmesso As Boolean
    Dim dttTmp As New DataTable
    Try
      '-----------------------------------------------------------------------------------------
      '--- Se non riesce ad aprire il file di LOG esce dall'elaborazione
      '-----------------------------------------------------------------------------------------
      If Not LogStart("OrdStorna") Then Return False
      '-----------------------------------------------------------------------------------------
      '--- LOG di descrizione parametri
      '-----------------------------------------------------------------------------------------
      LogWrite("--> " & Str(Now()) & oApp.Tr(Me, 129381688591250000, " chiamata metodo OrdStorna: operazione di storno ordini/impegni"), False)
      Dim strTmp13 As String = oApp.Tr(Me, 129381689270781250, "vero")
      Dim strTmp14 As String = oApp.Tr(Me, 129381689346562500, "falso")
      strMsg = oApp.Tr(Me, 129381688714843750, "Il ricalcolo è stato lanciato con i seguenti parametri:" & vbCrLf & _
               "     . Tipo record: '|" & strTipoRk & "|'" & vbCrLf & _
               "     . anno: |" & nAnno & vbCrLf & _
               "|     . serie: '|" & strSerie & "|'" & vbCrLf & _
               "     . numero: |" & lNumord & vbCrLf & _
               "|     . flag messaggi: ") & NTSCStr(IIf(bMsg, strTmp13, strTmp14))
      LogWrite(strMsg, False)
      '-----------------------------------------------------------------------------------------
      '--- Scrive nel file di LOG gli estremi del documento da ricalcolare
      '-----------------------------------------------------------------------------------------
      Select Case strTipoRk
        Case "$" : strDestipork = oApp.Tr(Me, 129381689556875000, "Ordine fornitore aperto")
        Case "H" : strDestipork = oApp.Tr(Me, 129381689782343750, "Ordine di produzione")
        Case "O" : strDestipork = oApp.Tr(Me, 129381689795468750, "Ordine fornitore")
        Case "Q" : strDestipork = oApp.Tr(Me, 129381689803593750, "Preventivo")
        Case "R", "#" : strDestipork = oApp.Tr(Me, 129381689810000000, "Impegno cliente")
        Case "V" : strDestipork = oApp.Tr(Me, 129381689818593750, "Impegno cliente aperto")
        Case "X" : strDestipork = oApp.Tr(Me, 129381689825625000, "Impegno di trasferimento")
        Case "Y" : strDestipork = oApp.Tr(Me, 129381689838281250, "Impegno di produzione")
      End Select
      '-----------------------------------------------------------------------------------------
      strMsg = ""
      '-----------------------------------------------------------------------------------------
      '--- Se il documento è di tipo 'Ordine di produzione' (strTipork = 'H')
      '--- o 'Impegno di produzione' (strTipork = 'Y')
      '--- lo scrive nel file di LOG (ed eventualmente dà il messaggio) e salta l'elaborazione
      '-----------------------------------------------------------------------------------------
      If (strTipoRk = "H") Or (strTipoRk = "Y") Then
        strMsg = oApp.Tr(Me, 129381689974062500, "L'ordine/impegno è di tipo '|" & strDestipork & "|' quindi non calcolabile." & vbCrLf & _
                         "    Elaborazione non effettuata.")
        LogWrite(strMsg, True)
        If bMsg Then ThrowRemoteEvent(New NTSEventArgs("", strMsg))
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Modulo Project Management
      '-----------------------------------------------------------------------------------------
      If CBool(ModuliDittaDitt(strDitta) And bsModPM) Then
        bPM = True
      Else
        bPM = False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Modulo TCO
      '-----------------------------------------------------------------------------------------
      If CBool(ModuliExtDittaDitt(strDitta) And bsModExtTCO) Then
        bModTCO = True
      Else
        bModTCO = False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se esiste almeno una riga in MOVORD mo_pmtaskid diversa da zero avvisa ed esce
      '--- (se esiste il modulo del Project Management)
      '-----------------------------------------------------------------------------------------
      If bPM Then
        If Not oCldGsor.CheckPM(strDitta, strTipoRk, nAnno, strSerie, lNumord, dttTmp) Then Return False
        If NTSCInt(dttTmp.Rows(0)!Records) <> 0 Then
          strMsg = oApp.Tr(Me, 129381692950156250, "Questo ordine/impegno di tipo '|" & strDestipork & "|'" & vbCrLf & _
                           "    movimenta attività del Project Management" & vbCrLf & _
                           "    pertanto non è possibile effettuare un ricalcolo.")
          LogWrite(strMsg, True)
          If bMsg Then ThrowRemoteEvent(New NTSEventArgs("", strMsg))
          Return False
        End If
      End If
      '-----------------------------------------------------------------------------------------
      '--- Determina dal tipo se è un ordine/impegno emesso o ricevuto
      '-----------------------------------------------------------------------------------------
      Select Case strTipoRk
        Case "R", "#", "Q", "Y", "X", "V" : bDocEmesso = True
        Case Else : bDocEmesso = False
      End Select
      '-----------------------------------------------------------------------------------------
      '--- Apre uno Snapshot su TESTORD (controlla anche che esista...)
      '-----------------------------------------------------------------------------------------
      If Not ApriOrdine(strDitta, False, strTipoRk, nAnno, strSerie, lNumord, dsShared) Then Return False

      If dttET.Rows.Count = 0 Then
        strMsg = oApp.Tr(Me, 129381695129687500, "Ordine/impegno di tipo '|" & strDestipork & "|'" & vbCrLf & _
                 "    anno: |" & nAnno & "|, numero: |" & NTSCStr(IIf(strSerie <> " ", strSerie & "/", "")) & lNumord & "| inesistente." & vbCrLf & _
                 "    Elaborazione non effettuata.")
        LogWrite(strMsg, True)
        If bMsg Then ThrowRemoteEvent(New NTSEventArgs("", strMsg))
        Return False
      End If

      If dttEC.Rows.Count = 0 Then
        '---------------------------------------------------------------------------------------
        '--- Se MOVORD è vuoto dà messaggio ed esce (non dovrebbe mai accadere...)
        '---------------------------------------------------------------------------------------
        strMsg = oApp.Tr(Me, 129381695715781250, "Questo ordine/impegno di tipo '|" & strDestipork & "|'" & vbCrLf & _
                         "    non possiede righe nel corpo." & vbCrLf & _
                         "    Elaborazione non effettuata.")
        LogWrite(strMsg, True)
        If bMsg Then ThrowRemoteEvent(New NTSEventArgs("", strMsg))
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Inizia l'elaborazione
      '-----------------------------------------------------------------------------------------
      If Not oCldGsor.OrdStornaImportExport(strDittaCorrente, bModTCO, strTipoRk, nAnno, strSerie, lNumord, strMsg) Then
        If bMsg Then ThrowRemoteEvent(New NTSEventArgs("", strMsg))
        LogWrite(strMsg, False)
        Return False
      Else
        strMsg = oApp.Tr(Me, 129381702250937500, "Storno ordine/impegno di tipo '|" & strDestipork & "|'" & vbCrLf & _
               "    anno: |" & nAnno & "|, numero: |" & NTSCStr(IIf(strSerie <> " ", strSerie & "/", "")) & lNumord & vbCrLf & _
               "|    effettuato regolarmente.")
        If bMsg = True Then ThrowRemoteEvent(New NTSEventArgs("", strMsg))
        LogWrite(strMsg, False)
      End If
      '-----------------------------------------------------------------------------------------

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
      '-----------------------------------------------------------------------------------------
      '--- Se si sono verificati degli errori chiede se visualizzare il file di LOG
      '-----------------------------------------------------------------------------------------
      If nCountLog > 0 Then
        '-----------------------------------------------------------------------------------------
        '--- Chiude il file di LOG
        '-----------------------------------------------------------------------------------------
        If bMsg Then
          LogStop()
          strMsg = oApp.Tr(Me, 129381095913691033, "Esistono dei messaggi nel file di log dello storno documenti." & vbCrLf & _
                                                   "Visualizzare il file?")

          Dim evnt As New NTSEventArgs(ThMsg.MSG_YESNO, strMsg)
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = ThMsg.RETVALUE_YES Then
            Shell("notepad.exe """ & Me.GetType.Name & "_" & oApp.User.Nome & ".log""", vbNormalFocus)
          End If
        End If
      End If
      '-----------------------------------------------------------------------------------------
    End Try
  End Function
#End Region

  Public Overridable Function CheckTimeStamp(ByVal strState As String) As Boolean
    Try
      'se è un documento aperto controllo se è stato modificato
      If NTSCDate(dttET.Rows(0)!et_ultagg) <> oCldGsor.GetTimeStampTestord(strDittaCorrente, NTSCStr(dttET.Rows(0)!et_tipork), _
                                                              NTSCInt(dttET.Rows(0)!et_anno), NTSCStr(dttET.Rows(0)!et_serie), _
                                                              NTSCInt(dttET.Rows(0)!et_numdoc)) Then
        If strState = "D" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523808025230000, "L'ordine che si cerca di eliminare è stato modificato da un altro utente o sessione. E' possibile solo ripristinare.")))
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523810613114000, "L'ordine che si cerca di salvare è stato modificato da un altro utente o sessione. E' possibile solo ripristinare.")))
        End If
        Return False
      End If

      If NTSCStr(dttET.Rows(0)!et_tipork) = "H" Then
        If dtTimeStampDocY <> oCldGsor.GetTimeStampTestord(strDittaCorrente, "Y", _
                                         NTSCInt(dttET.Rows(0)!et_anno), NTSCStr(dttET.Rows(0)!et_serie), _
                                         NTSCInt(dttET.Rows(0)!et_numdoc)) Then
          If strState = "D" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523811414174000, "L'ordine (IMPEGNO DI PRODUZIONE) che si cerca di eliminare è stato modificato da un altro utente o sessione. E' possibile solo ripristinare.")))
          Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523811401070000, "L'ordine (IMPEGNO DI PRODUZIONE) che si cerca di salvare è stato modificato da un altro utente o sessione. E' possibile solo ripristinare.")))
          End If
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

  Public Overridable Function ScriviMotransPerCreaNota(ByVal lIIMotrans As Integer) As Boolean
    Try
      '---------------------------------
      'riempio motrans solo per scrivere la nota di prelievo: una volta che bsorgnno verrà riscritto questa routine non servirà più
      Return oCldGsor.ScriviMotransPerCreaNota(strDittaCorrente, lIIMotrans, dttEC)

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

  '------------------------

  Public Overridable Function GetWhereHlof(ByVal lContoLead As Integer) As String
    Try
      Return GetWhereHlof(lContoLead, 0)

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
  Public Overridable Function GetWhereHlof(ByVal lContoLead As Integer, ByVal nValuta As Integer) As String
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {lContoLead, nValuta})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCStr(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      GetWhereHlof = ""
      '--------------------------------------------------------------------------------------------------------------
      Return oCldGsor.GetWhereHlof(strDittaCorrente, lContoLead, nValuta)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return ""
    End Try
  End Function
  Public Overridable Function ImportaOfferte(ByRef dttOfferte As DataTable) As Boolean
    Dim evnt As NTSEventArgs = Nothing
    Dim dttTmp As New DataTable
    Dim dttMf As New DataTable
    Dim strMess As String = ""
    Dim strHead As String = ""
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim strMsg As String = ""
    Dim nEcPrec As Integer = 0

    Try
      '---------------------------------------------------------------------------
      'verifico se alcune note sono in modifica tra quelli da importare
      For Each dtrT2 As DataRow In dttOfferte.Rows
        DocumentLockCheck(dtrT2!td_tipork.ToString, NTSCInt(dtrT2!td_anno), dtrT2!td_serie.ToString, NTSCInt(dtrT2!td_numord), NTSCInt(dtrT2!td_vers), strMsg)
        If strMsg.Trim <> "" Then
          If nControllaConcorrenzaOggetti = -1 Then
            'avviso
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & vbCrLf & _
                              oApp.Tr(Me, 129179716077109375, "Potrebbe non essere possibile salvare il documento in apertura se " & _
                              "il primo operatore apporterà delle modifiche." & vbCrLf & _
                              "Diversamente se modifichiamo il documento sarà il primo operatore a perdere il lavoro svolto.")))
          End If
          If nControllaConcorrenzaOggetti = 1 Then
            'blocco
            bDocumentLockNoSave = True
            ThrowRemoteEvent(New NTSEventArgs("", strMsg & vbCrLf & _
                              oApp.Tr(Me, 129179716096162109, "Importazione offerte annullata.")))
            Return False
          End If
        End If
      Next

      nEcPrec = 0
      If dttEC.Rows.Count > 0 Then nEcPrec = NTSCInt(dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga)

      '----------------------
      'Per ogni record di TTSELDOC inserische le righe in MOTRANS
      For Each dtrTD As DataRow In dttOfferte.Rows
        '--------------------
        'Controlla la congruenza delle informazioni di testata e piede
        If dttEC.Rows.Count = 0 Then
          'Riporta i campi di testata e piede dalla 1a offerta
          With dtrTD
            '--------------------
            'Controlli iniziali (anche sul primo ordine)
            strHead = oApp.Tr(Me, 128763308479820000, "Attenzione! I seguenti campi dell'OFFERTA serie '|" & _
                                              !td_serie.ToString & "|' numero |" & !td_numord.ToString & _
                                              "| versione |" & !td_vers.ToString & "|, differiscono dal documento " & _
                                              "corrente:" & vbCrLf & vbCrLf)
            strMess = ""
            If NTSCInt(!td_codese) <> NTSCInt(dttET.Rows(0)!et_codese) Then
              strMess = strMess & oApp.Tr(Me, 128581071120781250, "- Codice esenzione |" & NTSCInt(!td_codese).ToString & "|" & vbCrLf)
            End If
            If strMess <> "" Then
              strMess = strHead & strMess & vbCrLf & oApp.Tr(Me, 128581071598593750, "Forzare l'inserimento dell'OFFERTA?")
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, strMess)
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
                strMess = ""
                GoTo Salta
              End If
            End If
            strMess = ""
            '---------------------
            dttET.Rows(0)!et_tipobf = !td_tipobf
            dttET.Rows(0)!et_codchia = !td_codchia
            dttET.Rows(0)!et_codattp = !td_codattp
            dttET.Rows(0)!et_scorpo = !td_scorpo
            dttET.Rows(0)!et_flspinc = !td_flspinc

            If NTSCInt(!td_codpaga) > 0 And NTSCInt(!td_conto) = NTSCInt(dttET.Rows(0)!et_conto) Then
              dttET.Rows(0)!et_codpaga = !td_codpaga
              '----------------------
              'Se il pagamento è a 'data diversa' riporta la datapag dell'ordine
              dttET.Rows(0)!et_datapag = NTSCDate(dttET.Rows(0)!et_datdoc)
            End If

            If bNew Then dttET.Rows(0)!et_codagen = !td_codagen

            dttET.Rows(0)!et_listino = !td_listino
            'Data di consegna riga per riga
            If NTSCInt(!td_consggconf) <> 0 Then
              dttET.Rows(0)!et_datcons = DateAdd("d", NTSCInt(!td_consggconf), NTSCDate(dttET.Rows(0)!et_datdoc))
            Else
              If NTSCStr(!td_datcons) <> "" Then
                dttET.Rows(0)!et_datcons = NTSCDate(!td_datcons)
              Else
                dttET.Rows(0)!et_datcons = NTSCDate(dttET.Rows(0)!et_datcons)
              End If
            End If
            dttET.Rows(0)!et_scont1 = !td_scont1
            dttET.Rows(0)!et_scont2 = !td_scont2
            dttET.Rows(0)!et_scopag = !td_scopag

            If NTSCInt(!td_conto) = NTSCInt(dttET.Rows(0)!et_conto) Then
              dttET.Rows(0)!et_codese = !td_codese
              dttET.Rows(0)!et_porto = NTSCStr(!td_porto)
              dttET.Rows(0)!et_coddest = !td_coddest
              dttET.Rows(0)!et_coddest2 = !td_coddest2
              dttET.Rows(0)!et_codagen2 = !td_codagen2
              dttET.Rows(0)!et_abi = !td_abi
              dttET.Rows(0)!et_cab = !td_cab
              dttET.Rows(0)!et_banc1 = NTSCStr(!td_banc1)
              dttET.Rows(0)!et_banc2 = NTSCStr(!td_banc2)
            End If

            dttET.Rows(0)!et_contfatt = !td_contfatt

            dttET.Rows(0)!et_speacc = !td_speacc
            dttET.Rows(0)!et_speaccv = !td_speaccv
            dttET.Rows(0)!et_speimb = !td_speimb
            dttET.Rows(0)!et_speimbv = !td_speimbv
            dttET.Rows(0)!et_cambio = !td_cambio
            dttET.Rows(0)!et_organig = !td_organig

            'li riporta solo se sono vuoti
            If NTSCStr(dttET.Rows(0)!et_riferim).Trim = "" Then dttET.Rows(0)!et_riferim = NTSCStr(!td_riferim)
            If NTSCStr(dttET.Rows(0)!et_note).Trim = "" Then dttET.Rows(0)!et_note = NTSCStr(!td_note)


            '-------------------
            'Se l'Offerta è collegata ad una Chiamata,
            'preleva commessa, centro CA e linea/famiglia da proporre in testata e sulle righe
            If NTSCInt(!td_codchia) <> 0 Then
              If oCldGsor.GetNnchiam(strDittaCorrente, NTSCInt(!td_codchia), dttTmp) Then
                If dttTmp.Rows.Count > 0 Then
                  dttET.Rows(0)!et_commeca = dttTmp.Rows(0)!op_commeca
                  dttET.Rows(0)!et_codcena = dttTmp.Rows(0)!op_codcena
                  dttET.Rows(0)!et_codcfam = dttTmp.Rows(0)!op_codcfam
                  dttET.Rows(0)!et_cup = dttTmp.Rows(0)!op_cup
                  dttET.Rows(0)!et_cig = dttTmp.Rows(0)!op_cig
                  dttET.Rows(0)!et_riferimpa = dttTmp.Rows(0)!op_riferimpa
                End If
              End If
            End If
            '--------------------------------------------------------------------------------------------------------
            '--- Se il codice famiglia di testata è nullo e quello dell'offerta no, lo eredita
            '--------------------------------------------------------------------------------------------------------
            If (NTSCStr(dttET.Rows(0)!et_codcfam).Trim = "") And (NTSCStr(!td_codcfam).Trim <> "") Then
              dttET.Rows(0)!et_codcfam = NTSCStr(!td_codcfam)
            End If
            '--------------------------------------------------------------------------------------------------------
            'Imposta la variabile bterzista per i carichi di prod. importati da ordini
            'questo assicura che il documento sia di tipo terzista quando il tipo b/f
            'prima di chiamare l'ordine non è correttamente impostato come magazzino impegni
            bTerzista = False
          End With    'With dtrTD
        Else
          '---------------------
          'Segnala le differenze
          With dtrTD
            strHead = oApp.Tr(Me, 128581069784687500, "Attenzione! I seguenti campi dell'OFFERTA serie '|" & _
                                              !td_serie.ToString & "|' numero |" & !td_numord.ToString & _
                                              "| versione |" & !td_vers.ToString & "|, differiscono dal documento " & _
                                              "corrente:" & vbCrLf & vbCrLf)
            strMess = ""
            If NTSCInt(!td_scont1) <> NTSCInt(dttET.Rows(0)!et_scont1) Then
              strMess = strMess & oApp.Tr(Me, 128763308649548000, "- Sconto 1 di testata |" & NTSCInt(!td_scont1).ToString(oApp.FormatSconti) & "%|" & vbCrLf)
            End If
            If NTSCInt(!td_scont2) <> NTSCInt(dttET.Rows(0)!et_scont2) Then
              strMess = strMess & oApp.Tr(Me, 128763308705708000, "- Sconto 2 di testata |" & NTSCInt(!td_scont2).ToString(oApp.FormatSconti) & "%|" & vbCrLf)
            End If
            If NTSCInt(!td_codpaga) <> NTSCInt(dttET.Rows(0)!et_codpaga) Then
              'Se il codice pagamento è 0 lo assegna
              If NTSCInt(dttET.Rows(0)!et_codpaga) = 0 And NTSCInt(!td_codpaga) <> 0 Then
                dttET.Rows(0)!et_codpaga = NTSCInt(!td_codpaga)
              Else
                If NTSCInt(!td_codpaga) <> 0 Then
                  strMess = strMess & oApp.Tr(Me, 128763308770760000, "- Codice pagamento |" & NTSCInt(!td_codpaga).ToString() & "|" & vbCrLf)
                End If
              End If
            End If
            If NTSCInt(!td_tipobf) <> NTSCInt(dttET.Rows(0)!et_tipobf) Then
              strMess = strMess & oApp.Tr(Me, 128763308821772000, "- Codice tipo Bolla/Fattura |" & NTSCInt(!td_tipobf).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_scopag) <> NTSCInt(dttET.Rows(0)!et_scopag) Then
              strMess = strMess & oApp.Tr(Me, 128763308880272000, "- Sconto pagamento |" & NTSCInt(!td_scopag).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_codagen) <> NTSCInt(dttET.Rows(0)!et_codagen) Then
              strMess = strMess & oApp.Tr(Me, 128581682617187500, "- Codice agente |" & NTSCInt(!td_codagen).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_codagen2) <> NTSCInt(dttET.Rows(0)!et_codagen2) Then
              strMess = strMess & oApp.Tr(Me, 128763308990252000, "- Codice subagente |" & NTSCInt(!td_codagen2).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_coddest) <> NTSCInt(dttET.Rows(0)!et_coddest) Then
              strMess = strMess & oApp.Tr(Me, 128763309046412000, "- Codice 1° destinazione |" & NTSCInt(!td_coddest).ToString() & "|" & vbCrLf)
            End If

            If NTSCInt(!td_coddest2) <> NTSCInt(dttET.Rows(0)!et_coddest2) Then
              strMess = strMess & oApp.Tr(Me, 128763309067940000, "- Codice 2° destinazione |" & NTSCInt(!td_coddest2).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_codese) <> NTSCInt(dttET.Rows(0)!et_codese) Then
              strMess = strMess & oApp.Tr(Me, 128763309172454000, "- Codice esenzione |" & NTSCInt(!td_codese).ToString() & "|" & vbCrLf)
            End If
            If strMess <> "" Then
              strMess = strHead & strMess & vbCrLf & oApp.Tr(Me, 128763308592608000, "Forzare l'inserimento dell'OFFERTA mantenendo i dati di testata presenti nell'ordine?")
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, strMess)
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
                strMess = ""
                GoTo Salta
              End If
            End If
            strMess = ""
          End With    'With dtrTD
        End If    'If dttEC.Rows.Count = 0 Then

        '---------------------------
        'Riporta le righe d'offerta
        If Not oCldGsor.GetMovoff(strDittaCorrente, dtrTD!td_tipork.ToString, NTSCInt(dtrTD!td_anno), _
                                  dtrTD!td_serie.ToString, NTSCInt(dtrTD!td_numord), NTSCInt(dtrTD!td_vers), _
                                  strSoloConfermate, dttMf) Then GoTo salta
        If dttMf.Rows.Count = 0 Then GoTo salta

        For Each dtrMf As DataRow In dttMf.Rows
          ScriviRigaOff(dtrMf)
        Next
        dttMf.Clear()
Salta:
      Next    'For Each dtrTD As DataRow In dttOfferte.Rows

      '------------------
      'prendo le righe figlio di kit e, per ognuna, riassegno la riga del kit padre
      dtrT = dttEC.Select("ec_flkit = 'T' OR ec_flkit = 'B' AND ec_oqnum > 0")  'riga figlio
      For i = 0 To dtrT.Length - 1
        'cerco la riga padre
        dtrT1 = dttEC.Select("(ec_flkit = 'A' OR ec_flkit = 'S') " & _
                             " AND ec_ktriga = " & dtrT(i)!ec_ktriga.ToString & _
                             " AND ec_oqtipo = " & CStrSQL(dtrT(i)!ec_oqtipo.ToString) & _
                             " AND ec_oqanno = " & dtrT(i)!ec_oqanno.ToString & _
                             " AND ec_oqserie = " & CStrSQL(dtrT(i)!ec_oqserie.ToString) & _
                             " AND ec_oqnum = " & dtrT(i)!ec_oqnum.ToString)
        If dtrT1.Length > 0 Then dtrT(i)!ec_ktriga = dtrT1(0)!ec_riga
      Next

      '------------------
      'sui kit padre azzero il numero di riga
      dtrT = dttEC.Select("ec_flkit = 'S' OR ec_flkit = 'A'")
      For i = 0 To dtrT.Length - 1
        dtrT(i)!ec_ktriga = 0
      Next

      If dttEC.Select("ec_riga > " & nEcPrec & " AND xxo_codtagl > 0").Length > 0 Then
        'sulle righe aggiunte, se c'erano articoli tco avviso che ho inserito tutto sulla prima taglia
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128581866131406251, "Attenzione: tra le righe importate sono presenti articoli gestiti a taglie e colori. Per questi articoli la quantità è stata inserita tutta sulla prima taglia!")))
      End If

      '-----------------------------------------------------------------------
      'se serve aggiungo i lock su ttkeys 
      If nControllaConcorrenzaOggetti <> 0 Then
        For Each dtrT2 As DataRow In dttOfferte.Rows
          DocumentLockAdd(dtrT2!td_tipork.ToString, NTSCInt(dtrT2!td_anno), dtrT2!td_serie.ToString, NTSCInt(dtrT2!td_numord), NTSCInt(dtrT2!td_vers), False)
        Next
      End If    'If nControllaConcorrenzaOggetti <> 0 Then


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
      dttET.AcceptChanges()
      dttEC.AcceptChanges()
      dttTmp.Clear()
      dttMf.Clear()
    End Try
  End Function
  Public Overridable Function ScriviRigaOff(ByRef dtrMf As DataRow) As Boolean
    Dim nControp As Integer = 0
    Dim dtrT() As DataRow = Nothing

    Try
      '-----------------------
      'Prima di scrivere la riga controlla che tale riga d'offerta non sia già stata prelevata
      If dttEC.Select("ec_oqtipo = " & CStrSQL(dtrMf!mo_tipork) & _
               " AND ec_oqanno = " & dtrMf!mo_anno.ToString & _
               " AND ec_oqserie = " & CStrSQL(dtrMf!mo_serie) & _
               " AND ec_oqnum = " & dtrMf!mo_numord.ToString & _
               " AND ec_oqvers = " & dtrMf!mo_vers.ToString & _
               " AND ec_oqriga = " & dtrMf!mo_riga.ToString).Length > 0 Then
        Return True
      End If

      bInImportRigheOff = True

      dttEC.Rows.Add(dttEC.NewRow)
      With dttEC.Rows(dttEC.Rows.Count - 1)
        'forzo la MovordOnAddNewRow
        !codditt = "."
        !codditt = strDittaCorrente
        !ec_codart = dtrMf!mo_codart.ToString
        !ec_fase = NTSCInt(dtrMf!mo_fase)
        !ec_descr = dtrMf!mo_descr
        !ec_desint = dtrMf!mo_desint
        !ec_codiva = dtrMf!mo_codiva
        !ec_note = dtrMf!mo_note
        !ec_stasino = dtrMf!mo_stasino
        !ec_flkit = dtrMf!mo_flkit
        !ec_ktriga = NTSCInt(IIf(dtrMf!mo_flkit.ToString = "A" Or dtrMf!mo_flkit.ToString = "S", dtrMf!mo_riga, dtrMf!mo_ktriga))
        !ec_flforf = dtrMf!mo_flforf
        !ec_oqtipo = dtrMf!mo_tipork
        !ec_oqanno = dtrMf!mo_anno
        !ec_oqserie = dtrMf!mo_serie
        !ec_oqnum = dtrMf!mo_numord
        !ec_oqvers = dtrMf!mo_vers
        !ec_oqriga = dtrMf!mo_riga
        !ec_oqsalcon = "S"
        !ec_codtpro = dtrMf!mo_codtpro

        'Se non indicata prende quella della riga dell'offerta
        If NTSCStr(!ec_codcfam) = "" Then !ec_codcfam = NTSCStr(dtrMf!mo_codcfam)

        '------------------
        'scarico sempre tutta l'offerta
        !ec_misura1 = NTSCDec(dtrMf!mo_misura1)
        !ec_misura2 = NTSCDec(dtrMf!mo_misura2)
        !ec_misura3 = NTSCDec(dtrMf!mo_misura3)
        !ec_unmis = NTSCStr(dtrMf!mo_unmis)
        !ec_colli = NTSCDec(dtrMf!mo_colli)
        !ec_quant = NTSCDec(dtrMf!mo_quant)

        !ec_unmis = dtrMf!mo_unmis
        !ec_flprznet = dtrMf!mo_flprznet
        !ec_umprz = dtrMf!mo_umprz
        !ec_prezivav = NTSCDec(dtrMf!mo_prezivav)
        !ec_preziva = NTSCDec(dtrMf!mo_preziva)
        !ec_prezvalc = NTSCDec(dtrMf!mo_prezvalc)
        !ec_prezzo = NTSCDec(dtrMf!mo_prezzo)
        !ec_prelist = NTSCDec(dtrMf!mo_prelist)
        !ec_scont1 = NTSCDec(dtrMf!mo_scont1)
        !ec_scont2 = NTSCDec(dtrMf!mo_scont2)
        !ec_scont3 = NTSCDec(dtrMf!mo_scont3)
        !ec_scont4 = NTSCDec(dtrMf!mo_scont4)
        !ec_scont5 = NTSCDec(dtrMf!mo_scont5)
        !ec_scont6 = NTSCDec(dtrMf!mo_scont6)

        !ec_scontp = NTSCDec(dtrMf!mo_scontp)
        !ec_scontv = 0 'FISSO 0

        !ec_codrepr1 = dtrMf!mo_codrepr1
        !ec_codrepr2 = dtrMf!mo_codrepr2
        !ec_codrepr3 = dtrMf!mo_codrepr3
        !ec_codrepr4 = dtrMf!mo_codrepr4
        !ec_codrepr5 = dtrMf!mo_codrepr5
        !ec_codrepr6 = dtrMf!mo_codrepr6
        !ec_codrepr1out = dtrMf!mo_codrepr1out
        !ec_codrepr2out = dtrMf!mo_codrepr2out
        !ec_codrepr3out = dtrMf!mo_codrepr3out
        !ec_codrepr4out = dtrMf!mo_codrepr4out
        !ec_codrepr5out = dtrMf!mo_codrepr5out
        !ec_codrepr6out = dtrMf!mo_codrepr6out

        !ec_tiporiga = dtrMf!mo_tiporiga
        !ec_flprzmod = dtrMf!mo_flprzmod

        !ec_flsc1mod = dtrMf!mo_flsc1mod
        !ec_flsc2mod = dtrMf!mo_flsc2mod
        !ec_flsc3mod = dtrMf!mo_flsc3mod
        !ec_flsc4mod = dtrMf!mo_flsc4mod
        !ec_flsc5mod = dtrMf!mo_flsc5mod
        !ec_flsc6mod = dtrMf!mo_flsc6mod

        !ec_flprov1mod = dtrMf!mo_flprov1mod
        !ec_flprov2mod = dtrMf!mo_flprov2mod
        !ec_scpercdiff = dtrMf!mo_scpercdiff
        !ec_prov1percdiff = dtrMf!mo_prov1percdiff
        !ec_prov2percdiff = dtrMf!mo_prov2percdiff

        '------------------
        'Data di consegna riga per riga
        If NTSCInt(dtrMf!mo_consggconf) > 0 Then
          !ec_datcons = DateAdd("d", NTSCInt(dtrMf!mo_consggconf), NTSCDate(dttET.Rows(0)!et_datdoc))
        Else
          If NTSCStr(dtrMf!mo_datcons).Trim <> "" Then
            !ec_datcons = NTSCDate(dtrMf!mo_datcons)
          Else
            !ec_datcons = NTSCDate(dttET.Rows(0)!et_datdoc)
          End If
        End If
        !ec_datconsor = !ec_datcons

        '------------------
        'La contropartita nn può essere 0, pertanto controllo il valore presente nell'opzione di registro
        'ContropDefaultOfferte, se nn impostata prendo la prima contropartita presente nella tabella
        If NTSCInt(!ec_controp) = 0 Then
          If nContropDefaultOfferte <> 0 Then
            !ec_controp = nContropDefaultOfferte
          End If
        End If    'If NTSCInt(!ec_controp) = 0 Then
        If NTSCInt(!ec_controp) = 0 Then
          nControp = oCldGsor.GetFirstTabcove(strDittaCorrente)
          !ec_controp = nControp
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128581866131406250, "L'articolo '|" & !ec_codart.ToString & "|' non ha il cod. contropartita vendite, pertanto verrà inserito con il valore di default |" & nControp & "|. Controllare l'esattezza del valore.")))
        End If

        'se l'articolo è TCO, mette tutta la qta sulla prima taglia
        If NTSCInt(!xxo_codtagl) <> 0 Then
          dtrT = dttECTC.Select("ec_riga = " & NTSCInt(!ec_riga))
          If dtrT.Length > 0 Then dtrT(0)!ec_quant01 = NTSCDec(dtrMf!mo_quant)
        End If
      End With    'dttEC.Rows(dttEC.Rows.Count - 1)

      If Not ScriviRigaOff_Pers(dttEC.Rows(dttEC.Rows.Count - 1), dtrMf) Then
        dttEC.Rows(dttEC.Rows.Count - 1).Delete()
        Return False
      End If

      'la SettaProvvigioniDaOfferte non serve: anche in VB6 viene chiamata nella ScriviRigaOff, ma alla fine, 
      'nella ImportaOfferte viene rilanciata la SettaProvvigioni standard!!!

      If Not RecordSalva(dttEC.Rows.Count - 1, False, Nothing) Then
        dttEC.Rows(dttEC.Rows.Count - 1).Delete()
        Return False
      End If

      Return True

    Catch ex As Exception
      dttEC.Rows(dttEC.Rows.Count - 1).Delete()
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      bInImportRigheOff = False
    End Try
  End Function
  Public Overridable Function ScriviRigaOff_Pers(ByRef dtrNew As DataRow, ByRef dtrOld As DataRow) As Boolean
    Try
      'a disposizione per rive per caricare campi personalizzati di offerta in fase di import righe da offerta
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

  Public Overridable Function GetWhereHlmoOfa(ByVal bVisPrecOrd As Boolean) As String
    Try
      GetWhereHlmoOfa = ""
      Return oCldGsor.GetWhereHlmoOfa(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, _
                                  CBool(IIf(dttET.Rows(0)!et_scorpo.ToString = "S", True, False)), _
                                  fSelContoDiv, NTSCInt(dttET.Rows(0)!et_conto), _
                                  NTSCInt(dttET.Rows(0)!et_valuta), bVisPrecOrd)

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

  Public Overridable Function ImportaOrdini(ByRef dttOfferte As DataTable) As Boolean
    Dim strOrdinRighe As String = "mo_riga"
    Try
      Select Case NTSCInt(oCldGsor.GetSettingBusDitt(strDittaCorrente, "BNORGSOR", "RECENT", ".", "RecordOrdinaModo", "0", " ", "0"))
        Case 1 : strOrdinRighe = "mo_codart"
        Case 2 : strOrdinRighe = "mo_descr"
        Case 3 : strOrdinRighe = "mo_commeca"
      End Select

      Return ImportaOrdini(dttOfferte, strOrdinRighe)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function ImportaOrdini(ByRef dttRighe As DataTable, ByVal strSort As String) As Boolean
    Dim evnt As NTSEventArgs = Nothing
    Dim strMess As String = ""
    Dim strHead As String = ""
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim dtrT1() As DataRow = Nothing
    Dim strTipodoc As String = ""
    Dim dttTd As New DataTable

    Dim oDttgr As New CLEGROUPBY
    Dim dttGr As New DataTable
    Dim strMsg As String = ""
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dttRighe, strSort})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If

      '---------------------------------------------------------------------------
      'verifico se alcuni ordini sono in modifica tra quelli da importare
      If nControllaConcorrenzaOggetti <> 0 Then
        If Not oDttgr.NTSGroupBy(dttRighe, dttGr, "mo_tipork, mo_anno, mo_serie, mo_numord", "", "mo_tipork, mo_anno, mo_serie, mo_numord") Then Return False
        For Each dtrT2 As DataRow In dttGr.Rows
          DocumentLockCheck(dtrT2!mo_tipork.ToString, NTSCInt(dtrT2!mo_anno), dtrT2!mo_serie.ToString, NTSCInt(dtrT2!mo_numord), 0, strMsg)
          If strMsg.Trim <> "" Then
            If nControllaConcorrenzaOggetti = -1 Then
              'avviso
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & vbCrLf & _
                                oApp.Tr(Me, 129179712453710938, "Potrebbe non essere possibile salvare il documento in apertura se " & _
                                "il primo operatore apporterà delle modifiche." & vbCrLf & _
                                "Diversamente se modifichiamo il documento sarà il primo operatore a perdere il lavoro svolto.")))
            End If
            If nControllaConcorrenzaOggetti = 1 Then
              'blocco
              bDocumentLockNoSave = True
              ThrowRemoteEvent(New NTSEventArgs("", strMsg & vbCrLf & _
                                oApp.Tr(Me, 129179712495869141, "Importazione ordini aperti annullata.")))
              Return False
            End If
          End If
        Next
      End If    'If nControllaConcorrenzaOggetti <> 0 Then
      For Each dtrMo As DataRow In dttRighe.Select("", strSort)
        Select Case dtrMo!mo_tipork.ToString
          Case "R" : strTipodoc = oApp.Tr(Me, 128582693400312500, "IMPEGNO CLIENTE")
          Case "#" : strTipodoc = oApp.Tr(Me, 128582693510312500, "IMPEGNO DI COMMESSA")
          Case "O" : strTipodoc = oApp.Tr(Me, 128582693522187500, "ORDINE FORNITORE")
          Case "V" : strTipodoc = oApp.Tr(Me, 128582693533281250, "IMPEGNO CLIENTE APERTO")
          Case "$" : strTipodoc = oApp.Tr(Me, 128582693544062500, "ORDINE CLIENTE APERTO")
        End Select

        If bNonRiportareDatiTestOA Then
          '---------------------------
          'Riporta le righe d'ordine
          ScriviRigaOrd(dtrMo, 0, 0, 0)
          Continue For
        End If

        '--------------------
        If Not oCldGsor.GetTestord(strDittaCorrente, dtrMo!mo_tipork.ToString, NTSCInt(dtrMo!mo_anno), _
                                   dtrMo!mo_serie.ToString, NTSCInt(dtrMo!mo_numord), dttTd) Then Continue For
        If dttTd.Rows.Count = 0 Then Continue For

        '--------------------
        'Controlla la congruenza delle informazioni di testata e piede
        If dttEC.Rows.Count = 0 Then
          'Riporta i campi di testata e piede del primo ordine
          With dttTd.Rows(0)
            '--------------------
            'Controlli iniziali (anche sul primo ordine)
            strHead = oApp.Tr(Me, 128582694785625000, "Attenzione! I seguenti campi dell'|" & strTipodoc & "| serie '|" & _
                                              !td_serie.ToString & "|' numero |" & !td_numord.ToString & _
                                              "|, differiscono dal documento " & _
                                              "corrente:" & vbCrLf & vbCrLf)
            strMess = ""
            If NTSCInt(!td_codese) <> NTSCInt(dttET.Rows(0)!et_codese) Then
              strMess = strMess & oApp.Tr(Me, 128763308540036000, "- Codice esenzione |" & NTSCInt(!td_codese).ToString & "|" & vbCrLf)
            End If
            If strMess <> "" Then
              strMess = strHead & strMess & vbCrLf & oApp.Tr(Me, 128582694846718750, "Forzare l'inserimento dell'|" & strTipodoc & "|?")
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, strMess)
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
                strMess = ""
                Continue For
              End If
            End If
            strMess = ""
            '---------------------
            dttET.Rows(0)!et_tipobf = !td_tipobf
            dttET.Rows(0)!et_flspinc = !td_flspinc

            If NTSCInt(!td_codpaga) > 0 And NTSCInt(!td_conto) = NTSCInt(dttET.Rows(0)!et_conto) Then
              dttET.Rows(0)!et_codpaga = !td_codpaga
              '----------------------
              'Se il pagamento è a 'data diversa' riporta la datapag dell'ordine
              dttET.Rows(0)!et_datapag = NTSCDate(dttET.Rows(0)!et_datdoc)
            End If

            If bNew Then dttET.Rows(0)!et_codagen = !td_codagen

            dttET.Rows(0)!et_listino = !td_listino
            dttET.Rows(0)!et_scont1 = !td_scont1
            dttET.Rows(0)!et_scont2 = !td_scont2
            dttET.Rows(0)!et_scopag = !td_scopag

            If NTSCInt(!td_conto) = NTSCInt(dttET.Rows(0)!et_conto) Then
              dttET.Rows(0)!et_codese = !td_codese
              dttET.Rows(0)!et_porto = NTSCStr(!td_porto)
              dttET.Rows(0)!et_coddest = !td_coddest
              dttET.Rows(0)!et_coddest2 = !td_coddest2
              dttET.Rows(0)!et_codagen2 = !td_codagen2
              dttET.Rows(0)!et_vettor = !td_vettor
              dttET.Rows(0)!et_vettor2 = !td_vettor2
              dttET.Rows(0)!et_abi = !td_abi
              dttET.Rows(0)!et_cab = !td_cab
              dttET.Rows(0)!et_banc1 = NTSCStr(!td_banc1)
              dttET.Rows(0)!et_banc2 = NTSCStr(!td_banc2)
            End If

            'dttET.Rows(0)!et_contfatt = !td_contfatt
            dttET.Rows(0)!et_magaz = !td_magaz
            dttET.Rows(0)!et_magaz2 = !td_magaz2
            dttET.Rows(0)!et_magimp = !td_magimp

            dttET.Rows(0)!et_speacc = !td_speacc
            dttET.Rows(0)!et_speaccv = !td_speaccv
            dttET.Rows(0)!et_speimb = !td_speimb
            dttET.Rows(0)!et_speimbv = !td_speimbv
            dttET.Rows(0)!et_cambio = !td_cambio

            'li riporta solo se sono vuoti
            If NTSCStr(dttET.Rows(0)!et_riferim).Trim = "" Then dttET.Rows(0)!et_riferim = NTSCStr(!td_riferim)
            If NTSCStr(dttET.Rows(0)!et_note).Trim = "" Then dttET.Rows(0)!et_note = NTSCStr(!td_note)

            dttET.Rows(0)!et_commeca = !td_commeca
            dttET.Rows(0)!et_subcommeca = !td_subcommeca
            dttET.Rows(0)!et_codaspe = !td_codaspe
            dttET.Rows(0)!et_aspetto = !td_aspetto
            dttET.Rows(0)!et_caustra = !td_caustra
            dttET.Rows(0)!et_codcena = !td_codcena

            If NTSCStr(dtrMo!mo_tipork) = "V" OrElse NTSCStr(dtrMo!mo_tipork) = "$" Then
              dttET.Rows(0)!et_cig = !td_cig
              dttET.Rows(0)!et_cup = !td_cup
              dttET.Rows(0)!et_riferimpa = !td_riferimpa
            End If

            'Imposta la variabile bterzista per i carichi di prod. importati da ordini
            'questo assicura che il documento sia di tipo terzista quando il tipo b/f
            'prima di chiamare l'ordine non è correttamente impostato come magazzino impegni
            bTerzista = False
          End With    'With dtrMo
        Else
          '---------------------
          'Segnala le differenze
          With dttTd.Rows(0)
            strHead = oApp.Tr(Me, 128582694885468750, "Attenzione! I seguenti campi dell'|" & strTipodoc & "| serie '|" & _
                                              !td_serie.ToString & "|' numero |" & !td_numord.ToString & _
                                              "|, differiscono dal documento " & _
                                              "corrente:" & vbCrLf & vbCrLf)
            strMess = ""
            If NTSCInt(!td_scont1) <> NTSCInt(dttET.Rows(0)!et_scont1) Then
              strMess = strMess & oApp.Tr(Me, 128581679715156250, "- Sconto 1 di testata |" & NTSCInt(!td_scont1).ToString(oApp.FormatSconti) & "%|" & vbCrLf)
            End If
            If NTSCInt(!td_scont2) <> NTSCInt(dttET.Rows(0)!et_scont2) Then
              strMess = strMess & oApp.Tr(Me, 128581679729687500, "- Sconto 2 di testata |" & NTSCInt(!td_scont2).ToString(oApp.FormatSconti) & "%|" & vbCrLf)
            End If
            If NTSCInt(!td_codpaga) <> NTSCInt(dttET.Rows(0)!et_codpaga) Then
              'Se il codice pagamento è 0 lo assegna
              If NTSCInt(dttET.Rows(0)!et_codpaga) = 0 And NTSCInt(!td_codpaga) <> 0 Then
                dttET.Rows(0)!et_codpaga = NTSCInt(!td_codpaga)
              Else
                If NTSCInt(!td_codpaga) <> 0 Then
                  strMess = strMess & oApp.Tr(Me, 128581681255000000, "- Codice pagamento |" & NTSCInt(!td_codpaga).ToString() & "|" & vbCrLf)
                End If
              End If
            End If
            If NTSCInt(!td_tipobf) <> NTSCInt(dttET.Rows(0)!et_tipobf) Then
              strMess = strMess & oApp.Tr(Me, 128581681603750000, "- Codice tipo Bolla/Fattura |" & NTSCInt(!td_tipobf).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_scopag) <> NTSCInt(dttET.Rows(0)!et_scopag) Then
              strMess = strMess & oApp.Tr(Me, 128581682388593750, "- Sconto pagamento |" & NTSCInt(!td_scopag).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_codagen) <> NTSCInt(dttET.Rows(0)!et_codagen) Then
              strMess = strMess & oApp.Tr(Me, 128763308937212000, "- Codice agente |" & NTSCInt(!td_codagen).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_codagen2) <> NTSCInt(dttET.Rows(0)!et_codagen2) Then
              strMess = strMess & oApp.Tr(Me, 128581682814843750, "- Codice subagente |" & NTSCInt(!td_codagen2).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_magaz) <> NTSCInt(dttET.Rows(0)!et_magaz) Then
              strMess = strMess & oApp.Tr(Me, 128582703552656250, "- Codice 1° magazzino |" & NTSCInt(!td_magaz).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_magaz2) <> NTSCInt(dttET.Rows(0)!et_magaz2) Then
              strMess = strMess & oApp.Tr(Me, 128582703662656250, "- Codice 2° magazzino |" & NTSCInt(!td_magaz2).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_magimp) <> NTSCInt(dttET.Rows(0)!et_magimp) Then
              strMess = strMess & oApp.Tr(Me, 128582703863281250, "- Codice magazzino impegni |" & NTSCInt(!td_magimp).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_commeca) <> NTSCInt(dttET.Rows(0)!et_commeca) Then
              strMess = strMess & oApp.Tr(Me, 128763309221282000, "- Codice commessa |" & NTSCInt(!td_commeca).ToString() & "|" & vbCrLf)
            End If
            If NTSCStr(!td_subcommeca).ToUpper <> NTSCStr(dttET.Rows(0)!et_subcommeca).ToUpper Then
              strMess = strMess & oApp.Tr(Me, 128582704052968750, "- Codice sub-commessa |" & NTSCStr(!td_subcommeca).ToUpper & "|" & vbCrLf)
            End If
            If NTSCInt(!td_coddest) <> NTSCInt(dttET.Rows(0)!et_coddest) Then
              strMess = strMess & oApp.Tr(Me, 128581684888906250, "- Codice 1° destinazione |" & NTSCInt(!td_coddest).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_coddest2) <> NTSCInt(dttET.Rows(0)!et_coddest2) Then
              strMess = strMess & oApp.Tr(Me, 128581685136562500, "- Codice 2° destinazione |" & NTSCInt(!td_coddest2).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_codcena) <> NTSCInt(dttET.Rows(0)!et_codcena) Then
              strMess = strMess & oApp.Tr(Me, 128582705068281250, "- Codice centro C.A. |" & NTSCInt(!td_codcena).ToString() & "|" & vbCrLf)
            End If
            If NTSCInt(!td_codese) <> NTSCInt(dttET.Rows(0)!et_codese) Then
              strMess = strMess & oApp.Tr(Me, 128581685275781250, "- Codice esenzione |" & NTSCInt(!td_codese).ToString() & "|" & vbCrLf)
            End If
            If strMess <> "" Then
              strMess = strHead & strMess & vbCrLf & oApp.Tr(Me, 128582694924218750, "Forzare l'inserimento dell'|" & strTipodoc & "| mantenendo i dati di testata presenti nell'ordine?")
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, strMess)
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
                strMess = ""
                Continue For
              End If
            End If
            strMess = ""
          End With    'With dtrMo
        End If    'If dttEC.Rows.Count = 0 Then

        '---------------------------
        'Riporta le righe d'ordine
        ScriviRigaOrd(dtrMo, NTSCDec(dttTd.Rows(0)!td_scont1), NTSCDec(dttTd.Rows(0)!td_scont2), NTSCDec(dttTd.Rows(0)!td_scopag))
      Next    'For Each dtrMo As DataRow In dttOfferte.Rows


      '--------------------------------
      'se serve aggiungo i lock su ttkeys 
      If nControllaConcorrenzaOggetti <> 0 Then
        For Each dtrT2 As DataRow In dttGr.Rows
          DocumentLockAdd(dtrT2!mo_tipork.ToString, NTSCInt(dtrT2!mo_anno), dtrT2!mo_serie.ToString, NTSCInt(dtrT2!mo_numord), 0, False)
        Next
      End If    'If nControllaConcorrenzaOggetti <> 0 Then


      '------------------
      'prendo le righe figlio di kit e, per ognuna, riassegno la riga del kit padre
      dtrT = dttEC.Select("ec_flkit = 'T' OR ec_flkit = 'B' AND ec_oanum > 0")      'riga figlio
      For i = 0 To dtrT.Length - 1
        'cerco la riga padre
        dtrT1 = dttEC.Select("(ec_flkit = 'A' OR ec_flkit = 'S') " & _
                             " AND ec_ktriga = " & dtrT(i)!ec_ktriga.ToString & _
                             " AND ec_oatipo = " & CStrSQL(dtrT(i)!ec_oatipo.ToString) & _
                             " AND ec_oaanno = " & dtrT(i)!ec_oaanno.ToString & _
                             " AND ec_oaserie = " & CStrSQL(dtrT(i)!ec_oaserie.ToString) & _
                             " AND ec_oanum = " & dtrT(i)!ec_oanum.ToString)
        If dtrT1.Length > 0 Then dtrT(i)!ec_ktriga = dtrT1(0)!ec_riga
      Next

      '------------------
      'sui kit padre azzero il numero di riga
      dtrT = dttEC.Select("ec_flkit = 'S' OR ec_flkit = 'A'")
      For i = 0 To dtrT.Length - 1
        dtrT(i)!ec_ktriga = 0
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
      dttET.AcceptChanges()
      dttEC.AcceptChanges()
      dttTd.Clear()
      dttGr.Clear()
    End Try
  End Function

  Public Overridable Function ScriviRigaOrd(ByRef dtrMo As DataRow, ByVal dSc1 As Decimal, ByVal dSc2 As Decimal, ByVal dScpag As Decimal) As Boolean
    Dim bDelNewRow As Boolean = False
    Dim bDelNewRowTc As Boolean = False
    Dim nControp As Integer = 0
    Dim bTuttoIlResiduo As Boolean = False
    Dim dQuant As Decimal = 0
    Dim dColli As Decimal = 0
    Dim strFlevas As String = ""
    Dim strErr As String = ""
    Dim dMmvalore As Decimal = 0
    Dim dMmValorev As Decimal = 0
    Dim dMmValdisimp As Decimal = 0
    Dim dVProvv As Decimal = 0
    Dim dVProvv2 As Decimal = 0
    Dim i As Integer = 0
    Try
      With dtrMo
        '----------------------------
        'OCCHIO ALLE PROVVIGIONI: le provvigioni vengono calcolate in automatico all'inserimento dell'aticolo
        'se non è settata l'opzione fRicalcProv devo mantenere le provvigioni dell'ordine aperto, 
        'per cui se l'opzione E' SETTATA non forzo le provvigioni dall'ordine aperto, mentre se NON E' SETTATA le forzo
        'dopo aver indicato l'articolo

        'STESSA COSA PER PREZZI E SCONTI

        '------------------------
        'Preselezione quantità
        If ArrDbl(NTSCDec(!mo_quant) - NTSCDec(!mo_quaeva), 3) <> ArrDbl(NTSCDec(!xx_quadaeva), 3) Then
          bTuttoIlResiduo = False
        Else
          bTuttoIlResiduo = True
        End If

        'Esce se ha già soddisfatto la necessità
        If NTSCDec(!xx_quadaeva) <= 0 And (Not bTuttoIlResiduo) Then Return True
        'Esce se la riga è prenotata a saldo
        If NTSCStr(!mo_flevapre) = "S" Then Return True

        'Quantità/colli/flevas
        If bTuttoIlResiduo Then
          dQuant = NTSCDec(!mo_quant) - NTSCDec(!mo_quaeva)
          dColli = NTSCDec(!mo_colli) - NTSCDec(!mo_coleva)
          strFlevas = "S"
        Else
          'Può essere superiore o inferiore al residuo
          dQuant = NTSCDec(dtrMo!xx_quadaeva)
          If Not CType(oCleComm, CLELBMENU).ConvQuantUM(strDittaCorrente, !mo_codart.ToString, !mo_ump.ToString, _
                                            dQuant, NTSCDec(!mo_misura1), NTSCDec(!mo_misura2), NTSCDec(!mo_misura3), _
                                            !mo_unmis.ToString, dColli, strErr, 3) Then
            If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
            Return False
          End If
          If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
          strFlevas = !xx_flevasdaeva.ToString
        End If

        'Se nel documento originale non era stati impostati i colli allora li lascio a 0 anche in questo
        If NTSCDec(!mo_colli) = 0 Then dColli = 0

        '-----------------
        If !mo_umprz.ToString <> "S" Then
          dMmvalore = ArrDbl(ArrDbl(NTSCDec(!mo_prezzo) * dQuant / NTSCDec(!mo_perqta) * (100 - NTSCDec(!mo_scont1)) / 100 * (100 - NTSCDec(!mo_scont2)) / 100 * (100 - NTSCDec(!mo_scont3)) / 100 * (100 - NTSCDec(!mo_scont4)) / 100 * (100 - NTSCDec(!mo_scont5)) / 100 * (100 - NTSCDec(!mo_scont6)) / 100 * (100 - NTSCDec(!mo_scontp)) / 100 - NTSCDec(!mo_scontv), oCldGsor.TrovaNdec(0)) * (100 - NTSCDec(dttET.Rows(0)!et_scont1)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scont2)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scopag)) / 100, oCldGsor.TrovaNdec(0))
          dMmValorev = 0
          If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then dMmValorev = ArrDbl(ArrDbl(NTSCDec(!mo_prezvalc) * dQuant / NTSCDec(!mo_perqta) * (100 - NTSCDec(!mo_scont1)) / 100 * (100 - NTSCDec(!mo_scont2)) / 100 * (100 - NTSCDec(!mo_scont3)) / 100 * (100 - NTSCDec(!mo_scont4)) / 100 * (100 - NTSCDec(!mo_scont5)) / 100 * (100 - NTSCDec(!mo_scont6)) / 100 * (100 - NTSCDec(!mo_scontp)) / 100 - NTSCDec(!mo_scontv), oCldGsor.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta))) * (100 - NTSCDec(dttET.Rows(0)!et_scont1)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scont2)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scopag)) / 100, oCldGsor.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta)))
          dMmValdisimp = ArrDbl(ArrDbl(NTSCDec(!mo_prezzo) * dQuant / NTSCDec(!mo_perqta) * (100 - NTSCDec(!mo_scont1)) / 100 * (100 - NTSCDec(!mo_scont2)) / 100 * (100 - NTSCDec(!mo_scont3)) / 100 * (100 - NTSCDec(!mo_scont4)) / 100 * (100 - NTSCDec(!mo_scont5)) / 100 * (100 - NTSCDec(!mo_scont6)) / 100 * (100 - NTSCDec(!mo_scontp)) / 100 - NTSCDec(!mo_scontv), oCldGsor.TrovaNdec(0)) * (100 - dSc1) / 100 * (100 - dSc2) / 100 * (100 - dScpag) / 100, oCldGsor.TrovaNdec(0))
        Else
          dMmvalore = ArrDbl(ArrDbl(NTSCDec(!mo_prezzo) * dColli / NTSCDec(!mo_perqta) * (100 - NTSCDec(!mo_scont1)) / 100 * (100 - NTSCDec(!mo_scont2)) / 100 * (100 - NTSCDec(!mo_scont3)) / 100 * (100 - NTSCDec(!mo_scont4)) / 100 * (100 - NTSCDec(!mo_scont5)) / 100 * (100 - NTSCDec(!mo_scont6)) / 100 * (100 - NTSCDec(!mo_scontp)) / 100 - NTSCDec(!mo_scontv), oCldGsor.TrovaNdec(0)) * (100 - NTSCDec(dttET.Rows(0)!et_scont1)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scont2)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scopag)) / 100, oCldGsor.TrovaNdec(0))
          dMmValorev = 0
          If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then dMmValorev = ArrDbl(ArrDbl(NTSCDec(!mo_prezvalc) * dColli / NTSCDec(!mo_perqta) * (100 - NTSCDec(!mo_scont1)) / 100 * (100 - NTSCDec(!mo_scont2)) / 100 * (100 - NTSCDec(!mo_scont3)) / 100 * (100 - NTSCDec(!mo_scont4)) / 100 * (100 - NTSCDec(!mo_scont5)) / 100 * (100 - NTSCDec(!mo_scont6)) / 100 * (100 - NTSCDec(!mo_scontp)) / 100 - NTSCDec(!mo_scontv), oCldGsor.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta))) * (100 - NTSCDec(dttET.Rows(0)!et_scont1)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scont2)) / 100 * (100 - NTSCDec(dttET.Rows(0)!et_scopag)) / 100, oCldGsor.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta)))
          dMmValdisimp = ArrDbl(ArrDbl(NTSCDec(!mo_prezzo) * dColli / NTSCDec(!mo_perqta) * (100 - NTSCDec(!mo_scont1)) / 100 * (100 - NTSCDec(!mo_scont2)) / 100 * (100 - NTSCDec(!mo_scont3)) / 100 * (100 - NTSCDec(!mo_scont4)) / 100 * (100 - NTSCDec(!mo_scont5)) / 100 * (100 - NTSCDec(!mo_scont6)) / 100 * (100 - NTSCDec(!mo_scontp)) / 100 - NTSCDec(!mo_scontv), oCldGsor.TrovaNdec(0)) * (100 - dSc1) / 100 * (100 - dSc2) / 100 * (100 - dScpag) / 100, oCldGsor.TrovaNdec(0))
        End If
        dVProvv = ArrDbl(dMmvalore * NTSCDec(!mo_provv) / 100 * 100 / (100 - NTSCDec(dttET.Rows(0)!et_scopag)), oCldGsor.TrovaNdec(0))
        dVProvv2 = ArrDbl(dMmvalore * NTSCDec(!mo_provv2) / 100 * 100 / (100 - NTSCDec(dttET.Rows(0)!et_scopag)), oCldGsor.TrovaNdec(0))
      End With    'With dtrMo

      bInImportRigheOrd = True

      dttEC.Rows.Add(dttEC.NewRow)
      bDelNewRow = True
      With dttEC.Rows(dttEC.Rows.Count - 1)
        'forzo la MovordOnAddNewRow
        !codditt = "."
        !codditt = strDittaCorrente
        !ec_codart = dtrMo!mo_codart.ToString
        !ec_fase = NTSCInt(dtrMo!mo_fase)
        !ec_descr = dtrMo!mo_descr
        !ec_desint = dtrMo!mo_desint
        !ec_controp = dtrMo!mo_controp
        !ec_contocontr = dtrMo!mo_contocontr
        !ec_codiva = dtrMo!mo_codiva
        !ec_note = dtrMo!mo_note
        !ec_stasino = dtrMo!mo_stasino
        !ec_flkit = dtrMo!mo_flkit
        !ec_ktriga = NTSCInt(IIf(dtrMo!mo_flkit.ToString = "A" Or dtrMo!mo_flkit.ToString = "S", dtrMo!mo_riga, dtrMo!mo_ktriga))
        !ec_codtpro = dtrMo!mo_codtpro
        !ec_commen = dtrMo!mo_commen
        !ec_codcfam = NTSCStr(dtrMo!mo_codcfam)
        !ec_commeca = dtrMo!mo_commeca
        !ec_subcommeca = dtrMo!mo_subcommeca
        !ec_codcena = dtrMo!mo_codcena
        !ec_confermato = dtrMo!mo_confermato
        !ec_lotto = dtrMo!mo_lotto
        !ec_rilasciato = dtrMo!mo_rilasciato
        !ec_aperto = dtrMo!mo_aperto
        !ec_datconsor = dtrMo!mo_datconsor
        !ec_codclie = dtrMo!mo_codclie
        !ec_flforf = dtrMo!mo_flforf
        !ec_matric = dtrMo!mo_matric
        !ec_verdb = dtrMo!mo_verdb
        !ec_qtap = dtrMo!mo_qtap
        !ec_qtapeva = dtrMo!mo_qtapeva
        !ec_qtappre = dtrMo!mo_qtappre
        !ec_przp = dtrMo!mo_przp
        !ec_przpval = dtrMo!mo_przpval
        !ec_przpiva = dtrMo!mo_przpiva
        !ec_oaqtapdis = dtrMo!mo_oaqtapdis
        !ec_codlavo = dtrMo!mo_codlavo
        !ec_ubicaz = dtrMo!mo_ubicaz
        !ec_datini = dtrMo!mo_datini
        !ec_datfin = dtrMo!mo_datfin

        '------------------
        'riferimenti ad ordine aperto
        !ec_oatipo = dtrMo!mo_tipork
        !ec_oaanno = dtrMo!mo_anno
        !ec_oaserie = dtrMo!mo_serie
        !ec_oanum = dtrMo!mo_numord
        !ec_oariga = dtrMo!mo_riga
        !ec_oaqtadis = dQuant
        !ec_oacoldis = dColli
        !ec_oavaldis = dMmValdisimp
        !ec_oasalcon = strFlevas

        '------------------
        !ec_misura1 = NTSCDec(dtrMo!mo_misura1)
        !ec_misura2 = NTSCDec(dtrMo!mo_misura2)
        !ec_misura3 = NTSCDec(dtrMo!mo_misura3)
        !ec_unmis = NTSCStr(dtrMo!mo_unmis)
        !ec_colli = dColli
        !ec_quant = dQuant
        !ec_flevas = "C"
        !ec_colpre = dtrMo!mo_colpre
        !ec_quapre = dtrMo!mo_quapre
        !ec_flevapre = dtrMo!mo_flevapre

        'se non dovevo ricalcolare i prezzi rimetto quello dell'ordine aperto (li ha ricalcolati all'inserimento dell'articolo)
        !ec_unmis = dtrMo!mo_unmis
        If fRicalcPrez = False Then
          !ec_flprznet = dtrMo!mo_flprznet
          !ec_umprz = dtrMo!mo_umprz
          !ec_prezivav = NTSCDec(dtrMo!mo_prezivav)
          !ec_preziva = NTSCDec(dtrMo!mo_preziva)
          !ec_prezvalc = NTSCDec(dtrMo!mo_prezvalc)
          !ec_prezzo = NTSCDec(dtrMo!mo_prezzo)
          !ec_prelist = NTSCDec(dtrMo!mo_prelist)
          !ec_valore = dMmvalore
          !ec_valorev = dMmValorev
          !ec_valoremm = dMmvalore
          !ec_umprz = dtrMo!mo_umprz
        End If

        'se non dovevo ricalcolare gli sconti rimetto quelli dell'ordine aperto (li ha ricalcolati all'inserimento dell'articolo)
        If fRicalcScon = False Then
          !ec_scont1 = NTSCDec(dtrMo!mo_scont1)
          !ec_scont2 = NTSCDec(dtrMo!mo_scont2)
          !ec_scont3 = NTSCDec(dtrMo!mo_scont3)
          !ec_scont4 = NTSCDec(dtrMo!mo_scont4)
          !ec_scont5 = NTSCDec(dtrMo!mo_scont5)
          !ec_scont6 = NTSCDec(dtrMo!mo_scont6)
          !ec_scontp = NTSCDec(dtrMo!mo_scontp)
          !ec_scontv = 0 ' Fisso 0
        End If

        'se non dovevo ricalcolare le provvigioni rimetto quelli dell'ordine aperto (li ha ricalcolati all'inserimento dell'articolo)
        If fRicalcProv = False Then
          !ec_provv = dtrMo!mo_provv
          !ec_provv2 = dtrMo!mo_provv2
          !ec_vprovv = dVProvv
          !ec_vprovv2 = dVProvv2
          !ec_ricimp = dtrMo!mo_ricimp
        End If

        !ec_codrepr1 = dtrMo!mo_codrepr1
        !ec_codrepr2 = dtrMo!mo_codrepr2
        !ec_codrepr3 = dtrMo!mo_codrepr3
        !ec_codrepr4 = dtrMo!mo_codrepr4
        !ec_codrepr5 = dtrMo!mo_codrepr5
        !ec_codrepr6 = dtrMo!mo_codrepr6
        !ec_codrepr1out = dtrMo!mo_codrepr1out
        !ec_codrepr2out = dtrMo!mo_codrepr2out
        !ec_codrepr3out = dtrMo!mo_codrepr3out
        !ec_codrepr4out = dtrMo!mo_codrepr4out
        !ec_codrepr5out = dtrMo!mo_codrepr5out
        !ec_codrepr6out = dtrMo!mo_codrepr6out

        !ec_tiporiga = dtrMo!mo_tiporiga
        !ec_flprzmod = dtrMo!mo_flprzmod

        !ec_flsc1mod = dtrMo!mo_flsc1mod
        !ec_flsc2mod = dtrMo!mo_flsc2mod
        !ec_flsc3mod = dtrMo!mo_flsc3mod
        !ec_flsc4mod = dtrMo!mo_flsc4mod
        !ec_flsc5mod = dtrMo!mo_flsc5mod
        !ec_flsc6mod = dtrMo!mo_flsc6mod

        !ec_flprov1mod = dtrMo!mo_flprov1mod
        !ec_flprov2mod = dtrMo!mo_flprov2mod
        !ec_scpercdiff = dtrMo!mo_scpercdiff
        !ec_prov1percdiff = dtrMo!mo_prov1percdiff
        !ec_prov2percdiff = dtrMo!mo_prov2percdiff
      End With

      'Se articolo TCO completo il dettaglio TCO
      If bModTCO Then
        If Not (dtrMo!mo_quant01.Equals(DBNull.Value)) Then
          bDelNewRowTc = True
          With dttECTC.Rows(dttECTC.Rows.Count - 1)
            For i = 1 To 24
              dttECTC.Rows(dttECTC.Rows.Count - 1)("ec_quant" & i.ToString("00")) = ArrDbl(NTSCDec(dtrMo("xx_quadaeva" & i.ToString("00"))), 3)
              dttECTC.Rows(dttECTC.Rows.Count - 1)("ec_qtadis" & i.ToString("00")) = ArrDbl(NTSCDec(dtrMo("xx_quadaeva" & i.ToString("00"))), 3)
            Next
          End With
        End If
      End If
      'END TCO

      If Not ScriviRigaOrd_Pers(dttEC.Rows(dttEC.Rows.Count - 1), dtrMo) Then
        dttEC.Rows(dttEC.Rows.Count - 1).Delete()
        Return False
      End If

      If Not RecordSalva(dttEC.Rows.Count - 1, False, Nothing) Then
        dttEC.Rows(dttEC.Rows.Count - 1).Delete()
        Return False
      End If

      Return True

    Catch ex As Exception
      If bDelNewRow Then dttEC.Rows(dttEC.Rows.Count - 1).Delete()
      If bDelNewRowTc Then dttECTC.Rows(dttECTC.Rows.Count - 1).Delete()
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      bInImportRigheOrd = False
    End Try
  End Function
  Public Overridable Function ScriviRigaOrd_Pers(ByRef dtrNew As DataRow, ByRef dtrOld As DataRow) As Boolean
    Try
      'a disposizione per rive per caricare campi personalizzati di ofa/ica in fase di import righe ordine aperto
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

  '------------------------

  Public Overridable Function CambiaNumdoc(ByRef ds As DataSet, ByVal lNewProgr As Integer, Optional ByVal strNewSerie As String = "", Optional ByVal nNewAnno As Integer = 0, Optional ByVal bTestNumdoc As Boolean = False) As Boolean
    '--------------------------------
    'cambio il numero documento
    Dim i As Integer = 0

    Try
      'se non serve non faccio nulla
      If lNewProgr = 0 And strNewSerie = "" And nNewAnno = 0 Then Return True

      '----------------------------------------
      'verifico se esiste già un ordine con le stesse caratteristiche
      If bTestNumdoc Then
        If oCldGsor.EsisteOrdine(strDittaCorrente, ds.Tables("TESTA").Rows(0)!et_tipork.ToString, nNewAnno, strNewSerie, lNewProgr) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222099375000, "Esiste già un documento con le stesse caratteristiche di quello che si desidera creare.")))
          Return False
        End If
      End If

      If lNewProgr <> 0 Then ds.Tables("TESTA").Rows(0)!et_numdoc = lNewProgr
      If strNewSerie <> "" Then ds.Tables("TESTA").Rows(i)!et_serie = strNewSerie
      If nNewAnno <> 0 Then ds.Tables("TESTA").Rows(i)!et_anno = nNewAnno

      For i = 0 To ds.Tables("CORPO").Rows.Count - 1
        If lNewProgr <> 0 Then ds.Tables("CORPO").Rows(i)!ec_numdoc = lNewProgr
        If strNewSerie <> "" Then ds.Tables("CORPO").Rows(i)!ec_serie = strNewSerie
        If nNewAnno <> 0 Then ds.Tables("CORPO").Rows(i)!ec_anno = nNewAnno
        If NTSCStr(ds.Tables("CORPO").Rows(i)!ec_tipork) <> NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork) Then
          ds.Tables("CORPO").Rows(i)!ec_tipork = NTSCStr(ds.Tables("TESTA").Rows(0)!et_tipork)
        End If
      Next

      If ds.Tables("TESTA").Rows(0)!et_tipork.ToString = "H" Then
        For i = 0 To ds.Tables("CORPOIMP").Rows.Count - 1
          If lNewProgr <> 0 Then ds.Tables("CORPOIMP").Rows(i)!ec_numdoc = lNewProgr
          If strNewSerie <> "" Then ds.Tables("CORPOIMP").Rows(i)!ec_serie = strNewSerie
          If nNewAnno <> 0 Then ds.Tables("CORPOIMP").Rows(i)!ec_anno = nNewAnno
          If NTSCInt(ds.Tables("CORPOIMP").Rows(i)!ec_numordor) <> 0 Then
            If lNewProgr <> 0 Then ds.Tables("CORPOIMP").Rows(i)!ec_numordor = lNewProgr
            If strNewSerie <> "" Then ds.Tables("CORPOIMP").Rows(i)!ec_serieor = strNewSerie
            If nNewAnno <> 0 Then ds.Tables("CORPOIMP").Rows(i)!ec_annoor = nNewAnno
          End If
        Next

        For i = 0 To ds.Tables("ATTIVIT").Rows.Count - 1
          If lNewProgr <> 0 Then ds.Tables("ATTIVIT").Rows(i)!at_numord = lNewProgr
          If strNewSerie <> "" Then ds.Tables("ATTIVIT").Rows(i)!at_serie = strNewSerie
          If nNewAnno <> 0 Then ds.Tables("ATTIVIT").Rows(i)!at_anno = nNewAnno
          If NTSCInt(ds.Tables("ATTIVIT").Rows(i)!at_sunumord) <> 0 Then
            If lNewProgr <> 0 Then ds.Tables("ATTIVIT").Rows(i)!at_sunumord = lNewProgr
            If strNewSerie <> "" Then ds.Tables("ATTIVIT").Rows(i)!at_suserie = strNewSerie
            If nNewAnno <> 0 Then ds.Tables("ATTIVIT").Rows(i)!at_suanno = nNewAnno
          End If
        Next

        For i = 0 To ds.Tables("ASSRIS").Rows.Count - 1
          If lNewProgr <> 0 Then ds.Tables("ASSRIS").Rows(i)!as_numord = lNewProgr
          If strNewSerie <> "" Then ds.Tables("ASSRIS").Rows(i)!as_serie = strNewSerie
          If nNewAnno <> 0 Then ds.Tables("ASSRIS").Rows(i)!as_anno = nNewAnno
        Next

      End If

      If bModTCO Then
        For i = 0 To ds.Tables("CORPOTC").Rows.Count - 1
          If lNewProgr <> 0 Then ds.Tables("CORPOTC").Rows(i)!ec_numdoc = lNewProgr
          If strNewSerie <> "" Then ds.Tables("CORPOTC").Rows(i)!ec_serie = strNewSerie
          If nNewAnno <> 0 Then ds.Tables("CORPOTC").Rows(i)!ec_anno = nNewAnno
          If NTSCStr(ds.Tables("CORPOTC").Rows(i)!ec_tipork) <> NTSCStr(ds.Tables("CORPO").Rows(0)!ec_tipork) Then
            ds.Tables("CORPOTC").Rows(i)!ec_tipork = NTSCStr(ds.Tables("CORPO").Rows(0)!ec_tipork)
          End If
        Next
        For i = 0 To ds.Tables("CORPOIMPTC").Rows.Count - 1
          If lNewProgr <> 0 Then ds.Tables("CORPOIMPTC").Rows(i)!ec_numdoc = lNewProgr
          If strNewSerie <> "" Then ds.Tables("CORPOIMPTC").Rows(i)!ec_serie = strNewSerie
          If nNewAnno <> 0 Then ds.Tables("CORPOIMPTC").Rows(i)!ec_anno = nNewAnno
          If NTSCStr(ds.Tables("CORPOIMPTC").Rows(i)!ec_tipork) <> NTSCStr(ds.Tables("CORPOIMP").Rows(0)!ec_tipork) Then
            ds.Tables("CORPOIMPTC").Rows(i)!ec_tipork = NTSCStr(ds.Tables("CORPOIMP").Rows(0)!ec_tipork)
          End If
        Next
      End If

      For i = 0 To ds.Tables("SCHETRASP").Rows.Count - 1
        If lNewProgr <> 0 Then ds.Tables("SCHETRASP").Rows(i)!et_numdoc = lNewProgr
        If strNewSerie <> "" Then ds.Tables("SCHETRASP").Rows(i)!et_serie = strNewSerie
        If nNewAnno <> 0 Then ds.Tables("SCHETRASP").Rows(i)!et_anno = nNewAnno
      Next

      '-----------------------------
      'Segnala un messaggio di avvertimento in caso di attribuzione automatica delle commesse su numeratore
      If (dttET.Rows(0)!et_tipork.ToString = "R" Or dttET.Rows(0)!et_tipork.ToString = "#") And bGenNumCommecaAutR = True And dttEC.Rows.Count > 0 Then
        'Legge se l'opzione di registro NON è sul numeratore progressivo
        If (oCldGsor.GetSettingBusDitt(strDittaCorrente, "Bsorgsor", "Opzioni", ".", "TipoGenNumCommeca", "1", " ", "1") <> "4") And _
           (bGenNumCommecaAutR_AllaFine = False) Then
          ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 128534309595734216, "Attenzione! Nel documento sono state automaticamente generate delle commesse il cui numero è composto, in parte, dal numero dell'ordine, e tale numero ordine è stato modificato. E' necessario procedere manualmente alla rettifica dei numeri di commessa attribuiti sulle righe per gli articoli gestiti a commessa.")))
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

  Public Overridable Function GetDettaglioEvasione(ByVal lRiga As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      With dttET.Rows(0)
        Return oCldGsor.GetDettaglioEvasione(strDittaCorrente, NTSCStr(!et_tipork), NTSCInt(!et_anno), _
                                             NTSCStr(!et_serie), NTSCInt(!et_numdoc), lRiga, dttOut)
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
  End Function

  Public Overridable Function GetMotransDaGestNuov(ByVal lId As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      Return oCldGsor.GetMotransDaGestNuov(strDittaCorrente, lId, dttOut)
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


  '------------------------

  Public Overridable Function GetQueryStampaWord() As String
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    Dim ocldConf As CLDORCONF
    Try
      GetQueryStampaWord = ""

      'la prima parte della query (SELECT ... FROM ) la prendo da bdorconf, la where la costruisco in locale
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, Me.GetType.Assembly.GetName.Name, "BDORCONF", oTmp, strErr, False, "", "") = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128480830865991500, "ERRORE in fase di istanziazione Dll:") & vbCrLf & strErr))
        Return ""
      End If
      ocldConf = CType(oTmp, CLDORCONF)
      '------------------------------------------------
      ocldConf.Init(oApp)

      GetQueryStampaWord = ocldConf.GsorGetQuerySelectFromStampaWord
      GetQueryStampaWord += oCldGsor.GetQueryStampaWord(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, _
                                              NTSCInt(dttET.Rows(0)!et_anno), dttET.Rows(0)!et_serie.ToString, _
                                              NTSCInt(dttET.Rows(0)!et_numdoc.ToString))
      GetQueryStampaWord += oCldGsor.GetQueryOrderStampaWord(nOrdrig)


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

  Public Overridable Function GetQueryStampaPdf() As String
    Try
      Return oCldGsor.GetQueryStampaPdf(strDittaCorrente, dttET.Rows(0)!et_tipork.ToString, _
                                        NTSCInt(dttET.Rows(0)!et_anno), dttET.Rows(0)!et_serie.ToString, _
                                        NTSCInt(dttET.Rows(0)!et_numdoc.ToString))

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

  Public Overridable Sub AggiornaFlagStampato()
    Try
      oCldGsor.AggiornaFlagStampato(strDittaCorrente, NTSCStr(dttET.Rows(0)!et_tipork), _
                                    NTSCInt(dttET.Rows(0)!et_anno.ToString), NTSCStr(dttET.Rows(0)!et_serie), _
                                    NTSCInt(dttET.Rows(0)!et_numdoc.ToString))
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

  Public Overridable Function TestaBlocchi(ByVal bNew As Boolean) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      bRipristinaDocumento = False
      '--------------------------------------------------------------------------------------------------------------
      If bNew = True Then Return True
      '--------------------------------------------------------------------------------------------------------------
      If dttET.Rows.Count = 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      '--- CRM
      '--------------------------------------------------------------------------------------------------------------
      If CheckCrm(bRipristinaDocumento) = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function


  Public Overridable Function NetProCheckOrdModifCanc(ByVal bOpen As Boolean) As Boolean
    'per ogni riga del documento di produzione interno (che viene avanzato da NetPro) 
    'verifico se su netpro l'ordine è in evsione: se lo è non faccio ne salvare ne cancellare
    Try
      If strNetProDB = "" Then Return True
      If dttET.Rows.Count = 0 Then Return True

      If dttET.Rows(0)!et_tipork.ToString = "H" Then
        If bTerzista Then Return True
      Else
        Return True
      End If

      If bOpen Then
        If dttEC.Select("ec_netpid <> '' AND (ec_netpstatus = 'EMI' OR ec_netpstatus = 'EXE')").Length > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129932143269873695, "Ordine non modificabile/cancellabile: su Net@Pro questo documento è in corso di evasione. Per poterlo modificare riportare su Net@Pro lo status su 'REL'")))
          bNetProOrdLock = True
          Return False
        End If
      Else
        If bNetProOrdLock Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129932150324547199, "Ordine non modificabile/cancellabile: su Net@Pro questo documento è in corso di evasione. Per poterlo modificare riportare su Net@Pro lo status su 'REL'")))
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

  '------------------------

  Public Overridable Function CheckSoloSerieInTRKTPBF(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                                      ByVal strSerie As String) As Boolean
    Try
      If bSoloSerieInTRKTPBF Then
        If Not oCldGsor.CheckSoloSerieInTRKTPBF(strDitta, strTipoDoc, strSerie) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130512911001454327, "Con attiva l'opzione SoloSerieInTRKTPBF non è presente un tipo bolla/fattura per il doc |'" & DescrTiporkOr(strTipoDoc) & "'| serie '|" & strSerie & "|'." & vbCrLf & "Creazione documento annullata.")))
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


#Region "FORM BEORDEMO"
  Public bDemoHasChanges As Boolean = False
  Public dsDemoShared As DataSet
  Public strDemoPrevCelValue As String = ""

  Public Overridable Function DemoApri(ByRef dsDemo As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      DemoSetDefaultValue(dsDemo)
      dsDemoShared = dsDemo

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsDemoShared.Tables("DEMO").ColumnChanging, AddressOf DemoBeforeColUpdate
      AddHandler dsDemoShared.Tables("DEMO").ColumnChanged, AddressOf DemoAfterColUpdate

      '--------------------------------------
      'confermo tutto
      dsDemoShared.Tables("DEMO").AcceptChanges()
      bDemoHasChanges = False

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

  Public Overridable Sub DemoSetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      'ds.Tables("TABDEMO").Columns("ec_colli").DefaultValue = 
      'ds.Tables("TABDEMO").Columns("ec_quant").DefaultValue = 
      'ds.Tables("TABDEMO").Columns("ec_datcons").DefaultValue = 

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

  Public Overridable Sub DemoNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsDemoShared.Tables("DEMO").Rows.Add(dsDemoShared.Tables("DEMO").NewRow)
      bDemoHasChanges = True
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

  Public Overridable Function DemoRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsDemoShared.Tables("DEMO").Select(strFilter)(nRow).RejectChanges()
      bDemoHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function DemoSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not DemoTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      dsDemoShared.Tables("DEMO").AcceptChanges()

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

  Public ReadOnly Property DemoRecordIsChanged() As Boolean
    Get
      Return bDemoHasChanges
    End Get
  End Property

  Public Overridable Sub DemoBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strDemoPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DemoBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub DemoAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strDemoPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strDemoPrevCelValue = strDemoPrevCelValue.Remove(strDemoPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bDemoHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DemoAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Function DemoAfterColUpdate_ec_colli(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dQuant As Decimal = 0
    Dim strErrore As String = ""

    Try
      If e.Row!ec_codart.ToString = "D" Or e.Row!ec_codart.ToString = "M" Then
        e.Row!ec_quant = e.Row!ec_colli
      Else
        If CType(oCleComm, CLELBMENU).ConvQuantUMP(strDittaCorrente, e.Row!ec_codart.ToString, e.Row!ec_unmis.ToString, _
                        NTSCDec(e.Row!ec_colli), _
                        NTSCDec(e.Row!ec_misura1), NTSCDec(e.Row!ec_misura2), _
                        NTSCDec(e.Row!ec_misura3), dQuant, False, strErrore, oApp.NDecQta) Then
          e.Row!ec_quant = dQuant
        Else
          e.Row!ec_quant = 0
        End If
        If strErrore <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErrore))
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

  Public Overridable Function DemoTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsDemoShared.Tables("DEMO").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1

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

#Region "FORM BSORSEOR"
  Public Overridable Function Bnorseor_edConto_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Dim dttTmp As New DataTable
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldGsor.ValCodiceDb(nCod.ToString, strDittaCorrente, "ANAGRA", "N", strDescr, dttTmp)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128602583906093750, "Codice conto |'" & nCod.ToString & "'| inesistente")))
        Return False
      Else
        If dttTmp.Rows(0)!an_tipo.ToString = "S" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128602583976250000, "Il codice conto non può essere un sottoconto")))
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
  Public Overridable Function Bnorseor_edTipoBf_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldGsor.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABTPBF", "N", strDescr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128602584562343750, "Codice tipo bolla/fattura |'" & nCod.ToString & "'| inesistente")))
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
#End Region

#Region "FORM BSORSEDO"
  Public nSedoCodageAccdito As Integer = 0
  Public Overridable Function ZoomSeor(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                         ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String, _
                         ByVal strDataA As String, ByVal strDatConsDa As String, ByVal strDatConsA As String, _
                         ByVal strEvaso As String, ByVal strRiferim As String, ByVal strDitta As String) As DataSet
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strTipork, nAnno, strSerie, _
                                             lContoDa, lContoA, strDataDa, _
                                             strDataA, strDatConsDa, strDatConsA, _
                                             strEvaso, strRiferim, strDitta})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CType(oOut, DataSet)
      End If
      '----------------

      Return ZoomSeor(strTipork, nAnno, strSerie, _
               lContoDa, lContoA, strDataDa, _
               strDataA, strDatConsDa, strDatConsA, _
               strEvaso, strRiferim, strDitta, _
               "", "")


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
  Public Overridable Function ZoomSeor(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                       ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String, _
                       ByVal strDataA As String, ByVal strDatConsDa As String, ByVal strDatConsA As String, _
                       ByVal strEvaso As String, ByVal strRiferim As String, ByVal strDitta As String, _
                       ByVal strWhereFiltriEstesi As String, ByVal strWhereFiltriEstesiRighe As String) As DataSet
    Try
      Return oCldGsor.ZoomSeor(strTipork, nAnno, strSerie, lContoDa, lContoA, strDataDa, strDataA, _
                               strDatConsDa, strDatConsA, strEvaso, strRiferim, strDitta, strBlocco, _
                               strSospeso, bModuloCRM, bIsCRMUser, nSedoCodageAccdito, _
                               strWhereFiltriEstesi, strWhereFiltriEstesiRighe)

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
  Public Overridable Function RitornaCodcageAccdito() As Integer
    Try
      Return oCldGsor.RitornaCodcageAccdito(strDittaCorrente)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
#End Region

#Region "FORM BNORDTSC"
  Public Overridable Function ApriScadenze() As Boolean
    Dim i As Integer = 0
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(dttET.Rows(0)!et_codpaga) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130904195471978111, "Attenzione!" & vbCrLf & _
          "Inserire il codice pagamento prima di visualizzare le rate delle scadenze.")))
        For i = 1 To 8
          dttET.Rows(0)("et_tippaga_" & i.ToString) = 0
          dttET.Rows(0)("et_datsca_" & i.ToString) = DBNull.Value
          dttET.Rows(0)("et_impsca_" & i.ToString) = 0
          dttET.Rows(0)("et_impscav_" & i.ToString) = 0
        Next i
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      oCldGsor.ValCodiceDb(NTSCInt(dttET.Rows(0)!et_tipobf).ToString, strDittaCorrente, "TABTPBF", "N", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128669125857031250, "Attenzione!" & vbCrLf & _
          "Il codice Tipo Bolla/Fattura inserito non è valido.")))
        For i = 1 To 8
          dttET.Rows(0)("et_tippaga_" & i.ToString) = 0
          dttET.Rows(0)("et_datsca_" & i.ToString) = DBNull.Value
          dttET.Rows(0)("et_impsca_" & i.ToString) = 0
          dttET.Rows(0)("et_impscav_" & i.ToString) = 0
        Next i
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If CalcolaScadenzeStandard(False) = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
#End Region

#Region "FORM BNORCAST"
  Public dsCastShared As DataSet
  Public Overridable Function CastApri(ByRef dsCast As DataSet) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      dsCastShared = dsCast
      '--------------------------------------------------------------------------------------------------------------
      dsCastShared.AcceptChanges()
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
#End Region

End Class