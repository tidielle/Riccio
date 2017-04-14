Imports NTSInformatica
Imports NTSInformatica.CLN__STD

'Classe di supporto per il passaggio delle informazioni relative alla ricerca delle condizioni commerciali.
Public Class NTSCondCommerciali
  Public Input As DatiInput
  Public OutputPrezzo As DatiOutputPrezzo
  Public OutputSconti As DatiOutputSconti
  Public OutputProvvAgente1 As DatiOutputProvvigioni
  Public OutputProvvAgente2 As DatiOutputProvvigioni

  Public bCalcolaPrezzo As Boolean
  Public bCalcolaSconti As Boolean
  Public bCalcolaProvvigioni As Boolean

  Public Sub New()
    SvuotaDati()
  End Sub

  'La classe con i dati in ingresso, validi per prezzi e sconti
  Public Structure DatiInput
    Public strDitta As String                'Ditta corrente
    Public strTipoRk As String               'Tipo documento
    Public bRetail As Boolean                'Documento fatto da GPV

    Public bConsideraPrezziUnmis As Boolean  'Se deve valutare o meno se considerare le opzioni di bGestionePrezzi e bPrezziPerUnmis
    Public bGestionePrezzi As Boolean        'Opzione di registro OPZIONI\AbilitaPrezzoUM, non posso richiderla da questa classe, deve per forza essere passata 
    Public bPrezziPerUnmis As Boolean        'Se i prezzi devono essere per unità di misura o no

    Public dtDatdoc As DateTime              'Data documento
    Public nListino As Integer               'Listino 
    Public nCodvalu As Integer               'Codice valuta 
    Public bNoPrelist As Boolean             'Non calcolare il prezzo di listino

    Public strCodart As String               'Codice articolo
    Public strUmp As String                  'Unità di misura principale
    Public strUnmis As String                'Unità di misura
    Public dColli As Decimal                 'Colli
    Public dQuant As Decimal                 'Quantità
    Public nFase As Integer                  'Fase
    Public lLotto As Integer                 'Lotto

    Public lConto As Integer                 'Cliente\Fornitore
    Public lDestdiv As Integer               'Destinazione Diversa

    Public nCodlavo As Integer               'Codice Lavorazione
    Public strTipoval As String              'Tipologia di listino da considerare
    Public strCodCas As String               'Codice casella (CP2)
    Public bConspromo As Boolean             'Considera Promozioni
    Public nCodpromo As Integer              'Codice promozione

    Public nClscar As Integer                'Classe sconto articolo
    Public nClscan As Integer                'Classe sconti cliente

    Public strPrzNet As String               'Flag prezzo netto

    Public dDivisorePrezzo As Decimal        'Divisore del prezzo in uscita (usato da CS per canoni annuali, semetrali, mensili, ...)

    Public strWhereArtico As String          'Filtro per ricerche multiple -> valori ritornati in dttListino
    Public lListaSel As Integer              'Per ricerche multiple        -> valori ritornati in dttListino

    'Provvigioni:
    Public nCodage1 As Integer               'Agente 1
    Public nCodage2 As Integer               'Agente 2

    Public nClprar As Integer                'Classe articolo agente 
    Public nClpran As Integer                'Classe cliente agente

    Public dSconto1 As Decimal               'Sconto 1 
    Public dSconto2 As Decimal               'Sconto 2 
    Public dSconto3 As Decimal               'Sconto 3 
    Public dSconto4 As Decimal               'Sconto 4
    Public dSconto5 As Decimal               'Sconto 5
    Public dSconto6 As Decimal               'Sconto 6
    Public dScontT1 As Decimal               'Sconto Testata 1
    Public dScontT2 As Decimal               'Sconto Testata 2
    Public dPrezzo As Decimal                'Prezzo di riga
  End Structure

  'La classe con i dati in uscita per i prezzi 
  Public Structure DatiOutputPrezzo
    Public lProgr As Integer          'Progressivo del listino trovato
    Public dPrezzo As Decimal         'Prezzo trovato
    Public dPrelist As Decimal        'Prezzo di listino associato
    Public nCodpromo As Integer       'Codice promozione

    Public strPrzNet As String        'Listino a prezzo netto
    Public strTipo As String          'Tipo di calcolo per trovare il listino

    Public strUnmis As String         'Unità di misura indicata sul listino
    Public dDaQuant As Decimal        'Validità listino dalla quantità
    Public dAquant As Decimal         'Validità listino fino alla quantità
    Public dPerqta As Decimal         'Rapporto quantità prezzo 
    Public nPerqta As Integer         'Prezzo per quantità si\no

    Public strTipoval As String       'Tipo di listino trovato 
    Public strCodCas As String        'Codice casella (CP2)
    Public strErr As String           'Messaggio di errore (se presente)"

    Public dtDatAgg As Date           'Da data validità listino
    Public dtAData As Date            'A data validità listino 

    Public dttListini As DataTable    'Ritornato nel caso di ricerche multiple

    Public bDoppiaRicerca As Boolean  'A true se c'è stata la necessità di effettuare una doppia ricerca (vedi input)
    Public strDescrCalcolo As String  'come è stato trovato il prezzo.
  End Structure
  'La classe con i dati in uscita per gli sconti
  Public Structure DatiOutputSconti
    Public dSconto1 As Decimal      'Sconto 1
    Public dSconto2 As Decimal      'Sconto 2
    Public dSconto3 As Decimal      'Sconto 3
    Public dSconto4 As Decimal      'Sconto 4
    Public dSconto5 As Decimal      'Sconto 5
    Public dSconto6 As Decimal      'Sconto 6

    Public nPromo As Integer        'Codice promozione
    Public dtDaData As Date         'Sconto valido dalla data
    Public dtAData As Date          'Sconto valido dalla data

    Public nPerqta As Integer       'Sconto per quantità si\no
    Public dDaQuant As Decimal      'Sconto valido dalla quantità
    Public dAquant As Decimal       'Sconto valido fino alla quantità

    Public strUnmis As String       'Unità di misura dello sconto
    Public strTipoval As String     'Tipo di sconto trovato

    Public strDescrCalcoloScont1 As String 'come è stato calcolato lo sconto.
    Public strDescrCalcoloScont2 As String 'come è stato calcolato lo sconto.
    Public strDescrCalcoloScont3 As String 'come è stato calcolato lo sconto.
    Public strDescrCalcoloScont4 As String 'come è stato calcolato lo sconto.
    Public strDescrCalcoloScont5 As String 'come è stato calcolato lo sconto.
    Public strDescrCalcoloScont6 As String 'come è stato calcolato lo sconto.
  End Structure
  'La classe con i dati in uscita pre le provvigioni
  Public Structure DatiOutputProvvigioni
    Public dProvv As Decimal      'Provvigione
    Public dVprovv As Decimal     'Valore provvigione

    Public nPromo As Integer      'Codice promozione

    Public strTipoval As String   'Tipo calcolo provvigione
    Public strError As String     'Messaggio di errore

    Public strDescrCalcolo As String 'come è stato calcolata la provvigione.
  End Structure


  'Pulisce tutti i dati contenuti, utile per iniziare una nuova elaborazione
  Public Overridable Sub SvuotaDati()
    Input = New DatiInput
    OutputPrezzo = New DatiOutputPrezzo
    OutputSconti = New DatiOutputSconti
    OutputProvvAgente1 = New DatiOutputProvvigioni
    OutputProvvAgente2 = New DatiOutputProvvigioni

    bCalcolaPrezzo = False
    bCalcolaSconti = False
    bCalcolaProvvigioni = False

    With Input
      .strDitta = " "
      .strTipoRk = " "
      .bRetail = False

      .bConsideraPrezziUnmis = False
      .bGestionePrezzi = False
      .bPrezziPerUnmis = False

      .dtDatdoc = New Date(1900, 1, 1)
      .nListino = 0
      .nCodvalu = 0
      .bNoPrelist = False

      .strCodart = " "
      .strUmp = " "
      .strUnmis = " "
      .dColli = 0
      .dQuant = 0
      .nFase = 0
      .lLotto = 0

      .lConto = 0
      .lDestdiv = 0

      .nCodlavo = 0
      .strTipoval = " "
      .strCodCas = " "
      .bConspromo = False
      .nCodpromo = 0

      .nClscar = 0
      .nClscan = 0

      .strPrzNet = " "

      .dDivisorePrezzo = 1

      .strWhereArtico = ""
      .lListaSel = 0

      .nCodage1 = 0
      .nCodage2 = 0

      .nClprar = 0
      .nClpran = 0

      .dSconto1 = 0
      .dSconto2 = 0
      .dSconto3 = 0
      .dSconto4 = 0
      .dSconto5 = 0
      .dSconto6 = 0
      .dScontT1 = 0
      .dScontT2 = 0
      .dPrezzo = 0
    End With

    SvuotaOutput()
  End Sub
  'Pulisce solo i dati di output, utile per iniziare una nuova elaborazione con gli stessi dati di input di quella precedente.
  Sub SvuotaOutput()
    With OutputPrezzo
      .lProgr = 0
      .dPrezzo = 0
      .dPrelist = 0
      .nCodpromo = 0

      .strPrzNet = "N"
      .strTipo = " "

      .strUnmis = " "
      .dDaQuant = 0
      .dAquant = 9999999999
      .dPerqta = 0
      .nPerqta = 0

      .strTipoval = " "
      .strCodCas = " "
      .strErr = ""

      .dtDatAgg = New Date(1900, 1, 1)
      .dtAData = New Date(1900, 1, 1)

      .dttListini = Nothing

      .bDoppiaRicerca = False
      .strDescrCalcolo = ""
    End With

    With OutputSconti
      .dSconto1 = 0
      .dSconto2 = 0
      .dSconto3 = 0
      .dSconto4 = 0
      .dSconto5 = 0
      .dSconto6 = 0

      .nPromo = 0
      .dtDaData = New Date(1900, 1, 1)
      .dtAData = New Date(1900, 1, 1)

      .nPerqta = 0
      .dDaQuant = 0
      .dAquant = 9999999999

      .strUnmis = " "
      .strTipoval = " "

      .strDescrCalcoloScont1 = ""
      .strDescrCalcoloScont2 = ""
      .strDescrCalcoloScont3 = ""
      .strDescrCalcoloScont4 = ""
      .strDescrCalcoloScont5 = ""
      .strDescrCalcoloScont6 = ""
    End With

    With OutputProvvAgente1
      .dProvv = 0
      .dVprovv = 0
      .nPromo = 0

      .strTipoval = " "
      .strError = ""
      .strDescrCalcolo = ""
    End With

    With OutputProvvAgente2
      .dProvv = 0
      .dVprovv = 0
      .nPromo = 0

      .strTipoval = " "
      .strError = ""
      .strDescrCalcolo = ""
    End With
  End Sub
End Class


'Classe di supporto per il passaggio delle informazioni relative al calcolo delle promozioni
Public Class NTSPromozioni
  Public dttEt As DataTable       ' Testata del documento
  Public dttEc As DataTable       ' Corpo del documento
  Public dttEcTc As DataTable     ' Dettaglio Taglie e Colori
  Public dttMovMatr As DataTable  ' Dettaglio Matricole
  Public dttPromo As DataTable    ' Lista delle promozioni applicabili
  Public dttOmaggi As DataTable   ' Lista degli omaggio derivati da promozioni (no MxN)
  Public dsPromoOmaggi As DataSet ' Promozioni Omaggi
  Public Parametri As DatiParametri

  Public Sub New()
    SvuotaDati()
  End Sub

  'Parametri da passare al calcolo delle promozioni
  Public Structure DatiParametri
    Public strDitta As String               ' Ditta Corrente
    Public bDaGPV As Boolean                ' Chiamata eseguita da GPV 
    Public bDaOfferta As Boolean            ' Chiamata eseguita da Gestione Offerte
    Public strCodRepc As String             ' Nome computer (da salvare per GPV)
    Public strPromoNoStornoResi As String   ' Opz. registro 'TipiPromozioniSuiResi'
    Public nPeacIvainc As Integer           ' Codice Iva perso da Peac

    Public strStampaRigaOmaggi As String    ' Stampa riga da indicare sulle eventuali righe di omaggio
    Public lCodivaOmaggi As Integer         ' Codice Iva da indicare sulle eventuali righe di omaggio
    Public lCausaleScontiPiede As Integer   ' Causale da utilizzare per gli sconti di piede
    Public strOmaggiDesel As String         ' Indica cosa deve essere selezionato nella griglia degli articoli omaggio

    Public bMovimQtaLotti As Boolean        ' Se gli articoli devono essere movimentati a multipli di un certo lotto. Implica il blocco degli articoli MxN
    Public nIncremContatoreRiga As Integer  ' Di quanto incrementare il numeratore di riga sulle righe aggiunte da promozione 
  End Structure

  'Pulisce tutti i dati contenuti, utile per iniziare una nuova elaborazione
  Public Overridable Sub SvuotaDati()
    dttEc = Nothing
    dttEt = Nothing
    dttEcTc = Nothing
    dttMovMatr = Nothing
    dttPromo = Nothing
    dttOmaggi = Nothing
    dsPromoOmaggi = Nothing

    With Parametri
      .strDitta = ""
      .bDaGPV = False
      .bDaOfferta = False
      .strCodRepc = " "
      .strPromoNoStornoResi = ""
      .nPeacIvainc = 0

      .strStampaRigaOmaggi = "M"
      .lCodivaOmaggi = 0
      .lCausaleScontiPiede = 0
      .strOmaggiDesel = "T"

      .bMovimQtaLotti = False
      .nIncremContatoreRiga = 1 'Default 1, con zero non incrementerebbe il contatore di riga!
    End With
  End Sub
End Class

