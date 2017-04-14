Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.IO
Imports System.Text

Imports System
Public Class CLELBMENU
  Inherits CLE__BASN
  Public oCldComm As CLDLBMENU          'oggetto dal
  Public oMenu As Object = Nothing      'puntatore a cle__menu: ATTENZIONE: NON AGGIUNGERE MAI TRA I REFERENCE DI BELBMENU BE__MENU, altrimenti si crea un riferimento circolare .... oMenu può essere usato solo dagli entity tipo veboll, gsor, ... per arrivare a funzioni di menu. in questi entity va aggiunto il reference a be__menu

#Region "VARIABILI"

  'Parametri per calcolo scadenze
  Public Class ParamCalcScad
    Public nCodpaga As Integer
    Public strDatrif As String
    Public dTotfat As Decimal
    Public dTotfatval As Decimal
    Public dIva As Decimal
    Public dIvaval As Decimal
    Public dSpese As Decimal
    Public dSpeseval As Decimal
    Public strDatsca(60) As String
    Public dImpsca(60) As Decimal
    Public dImpscaval(60) As Decimal
  End Class

  'classe contenente le impostazioni per il calcolo del documento
  Public Class Parametri
    Public bNew As Boolean
    Public bDocEmesso As Boolean
    Public strNomProg As String
    Public bCalcolaBolli As Boolean
    Public bCalcolaColli As Boolean
    Public bCalcolaPesoNetto As Boolean
    Public bCalcolaPesoLordo As Boolean
    Public bNonCalcolaProvvig As Boolean
    Public strCalcolaSpeseTrasp As String
    Public bCalcolaSpeseTraspSoloNuovi As Boolean = False
    Public bUsaVettore1perCalcoloSpeseTrasp As Boolean
    Public bCalcolaColliPesiSuDocAperti As Boolean
    Public nGestScostAcconti As Integer '0=non fa nulla;1=metti in abbuoni; 2=varia acconto.
    Public dImpoScostAcconti As Decimal 'indica l'importo soglia al di sotto del quale fa scattare la funzionalit dell'opzione sopra
    Public bCalcPesi As Boolean
    Public bNoPesiSuRigheKitFittizie As Boolean
    Public bSbloccaIva As Boolean

    Public nPeveIva15 As Integer
    Public nPeacIva15 As Integer
    Public nPeveIvaInc As Integer
    Public nPeacIvainc As Integer
    Public nCodese As Integer

    Public bIncassoTotalePagContanti As Boolean
    Public bSegueFatt As Boolean
    Public bDeterminaBolliSuOperazEsenti As Boolean

    Public nCodivacascom As Integer = 0
    Public nCodivaspegen As Integer = 0
  End Class

  'classe di valori di ritorno della testata del documento dopo aver eseguito il ricalcolo
  Public Class OutTestata
    Public dTotMerce As Decimal = 0
    Public dTotmercev As Decimal = 0
    Public dTotlordo As Decimal = 0
    Public dTotlordov As Decimal = 0
    Public dTotSpese As Decimal = 0
    Public dTotSpesev As Decimal = 0
    Public dTotImpon As Decimal = 0
    Public dTotImponv As Decimal = 0
    Public dTotIva As Decimal = 0
    Public dTotIvav As Decimal = 0
    Public dTotdoc As Decimal = 0
    Public dTotdocv As Decimal = 0
    Public dTotomag As Decimal = 0
    Public dTotomagv As Decimal = 0
    Public dTotdocsom As Decimal = 0
    Public dTotdocsomv As Decimal = 0
    Public dRimanenza As Decimal = 0
    Public dRimanenzav As Decimal = 0
    Public dSpeimb As Decimal = 0
    Public dSpeimbv As Decimal = 0

    Public dSpeacc As Decimal = 0
    Public dSpeaccv As Decimal = 0
    Public dSpeinc As Decimal = 0
    Public dSpeincv As Decimal = 0
    Public dBolli As Decimal = 0
    Public dBolliv As Decimal = 0

    Public nCodiva(8) As Integer
    Public dImponib(8) As Decimal
    Public dImposta(8) As Decimal
    Public dImponibv(8) As Decimal
    Public dImpostav(8) As Decimal
    Public nCcontr(20) As Integer
    Public dImpocont(20) As Decimal
    Public dImpcontv(20) As Decimal
    Public dImpprov As Decimal = 0
    Public dTotprov As Decimal = 0
    Public dTotprov2 As Decimal = 0
    Public dTotcoll As Decimal = 0
    Public dPeso As Decimal = 0
    Public dPesonetto As Decimal = 0
    Public dTotquant As Decimal = 0

    ' campi per diff. da conversione Lira/euro (siamo gi in EUR e facciamo doc. in LIT
    Public dDiffDA As Decimal = 0 ' diff fra tot. doc. e somma controp., spese + iva (come da VEPNFA)
    Public dDiffIva As Decimal = 0 ' diff tra tot doc. e castelletto iva (impon.+imposta)
    Public dDiffCorpoRighe As Decimal = 0 '(diff. tra somma controp. e valori netti di righe)
    Public dTotimpocontr As Decimal = 0 'totale da importi castelletto contropartite
    Public dTotimporighe As Decimal = 0 ' totale da somme righe (dedotti anche sconto pagamento)

    'Castelletto IVA omaggi tipo P (imponibile+iva)
    Public nCodivaOMAGP(8) As Integer
    Public dImponibOMAGP(8) As Decimal
    Public dImpostaOMAGP(8) As Decimal
    Public dImponibvOMAGP(8) As Decimal
    Public dImpostavOMAGP(8) As Decimal
    Public dImponibcivaOMAGP(8) As Decimal
    Public dImponibcivavOMAGP(8) As Decimal

    ' per scorporo (per ora non utilizzati : gli importi vengono gestiti some Davide, in pi le spese accessorie si intendono iva escluse ..)
    Public dImponibciva(8) As Decimal
    Public dImponibcivav(8) As Decimal
    Public dTotmerceciva As Decimal = 0
    Public dTotmercecivav As Decimal = 0
    Public dTotlordociva As Decimal = 0
    Public dTotlordocivav As Decimal = 0

    Public dPagato As Decimal = 0
    Public dPagatov As Decimal = 0
    Public dPagato2 As Decimal = 0
    Public dResto As Decimal = 0
    Public dAbbuono As Decimal = 0
    Public dAbbuonov As Decimal = 0

    Public nIvaSpeseIncasso As Integer = 0

    'campi in più per parcellazione (elencati così come sono nel piede di bnpaboll)
    Public dEdCompens As Decimal = 0      'compenso
    Public dEdTotaccscal As Decimal = 0   'totale acconti già fatturati
    Public dBaseSpegen As Decimal = 0     'importo su cui calcolare le spese generali
    Public dEdSpegen As Decimal = 0       'spese generali
    Public dBaseCascom As Decimal = 0     'importo su cui calcolare la cassa commercialisti
    Public dEdNonsoggiva As Decimal = 0   'cassa commercialisti
    Public dEdNonsogg As Decimal = 0      'spese anticipate
    Public dBaseRit As Decimal = 0        'importo su cui calcolare la ritenuta fiscale
    Public dEdPercomp As Decimal = 0      '% dell'imponibile su cui calcolare la ritenuta
    Public dEdPerrite As Decimal = 0      '% ritenuta fiscale
    Public dEdRitenut As Decimal = 0      'importo ritenuta fiscale
    Public dEdAltriprev As Decimal = 0    'enasarco/altri previdenziali
    Public dEdTotdapagare As Decimal = 0  'totale documento - ritenuta - altri previdenziali 
    Public dEdPerspegen As Decimal = 0    ' % spese generali !!!
    Public dEdCascom As Decimal = 0       ' % cassa commercialisti !!!
  End Class

  'non chiamare direttamente: serve per la collezione di valori per il corpo restituiti dopo il ricalcolo
  Public Class OutCorpo
    Public dValore As Decimal = 0     'per mo_valoremm (o mo_valore!!!)
    Public dValorev As Decimal = 0
  End Class

  'classe di valori di ritorno della testata del documento dopo aver eseguito il ricalcolo
  Public oTestata As OutTestata
  Public oCorpo() As OutCorpo
  Public oPar As Parametri

  'campi di testord/testmag settati nella calcola1
  Public strTipork As String = ""
  Public nValuta As Integer = 0
  Public dCambio As Decimal = 0
  Public bScorp As Boolean
  Public bSpeinc As Boolean
  Public bBolli As Boolean
  Public bFlspegen As Boolean = False
  Public bFlcascom As Boolean = False
  Public dScont1 As Decimal = 0
  Public dScont2 As Decimal = 0
  Public dScoPag As Decimal = 0
  Public dtDatDoc As Date
  Public dtDatapag As Date
  'Public dSpeimb As Decimal = 0
  'Public dSpeimbv As Decimal = 0
  Public nCodese As Integer = 0
  Public lConto As Integer = 0
  Public nCodVett As Integer = 0
  Public strPorto As String = ""
  Public nCodPaga As Integer = 0
  Public nCodntra As Integer = 0

  Public dTotomagLocal As Decimal = 0
  Public dTotomagvLocal As Decimal = 0
  Public bFlspegenCalc As Boolean = False   'se true le spese generali sono già state calcolate. serve perchè vengono aggiunte nella calcola2, ma la calcola2 viene chiamata più volte (es per aggiungere i bolli)

  'Variabili appoggio lavorazioni di fantasmi: usate nella TrattaDbDoc_TrovaFiglieLav
  Public strCodartRoot As String = ""
  Public dPerqtaRoot As Decimal = 0
  Public dQuantRoot As Decimal = 0
  Public bStopFasi As Boolean

  Public bUsaVecchiaCercaPrezzo As Boolean = False
  Public bUsaVecchiaCercaSconti As Boolean = False
  Public bUsaVecchiaCercaProvvigioni As Boolean = False
#End Region

  Public Overrides Function Init(ByRef App As CLE__APP, ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, _
                                ByVal strTabella As String, ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                                ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDLBMENU"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldComm = CType(MyBase.ocldBase, CLDLBMENU)
    oCldComm.Init(oApp)

    VerificaUsoCercaCondCommerciali()

    Return True
  End Function

  Overridable Function Calcola0() As Boolean
    Dim i As Integer
    Try
      dTotomagLocal = 0
      dTotomagvLocal = 0
      bFlspegenCalc = False

      With oTestata
        '-----------------------------------------------------------------------
        'Pulisce i totalizzatori intermedi e finali
        .dTotimpocontr = 0
        .dTotimporighe = 0
        .dDiffDA = 0
        .dDiffIva = 0
        .dDiffCorpoRighe = 0
        'Totalizzatori provvigioni
        .dImpprov = 0
        .dTotprov = 0
        .dTotprov2 = 0
        'Totalizzatori pesi/q.t
        .dTotquant = 0
        .dTotcoll = 0
        .dPeso = 0
        .dPesonetto = 0
        'Totale merce/lordo
        .dTotMerce = 0
        .dTotmercev = 0
        .dTotlordo = 0
        .dTotlordov = 0
        .dTotmerceciva = 0
        .dTotmercecivav = 0
        .dTotlordociva = 0
        .dTotlordocivav = 0
        'Spese/Imponibili/imposte/omaggi/totdoc/pagato/abbuono
        .dTotSpese = 0
        .dTotSpesev = 0
        .dTotImpon = 0
        .dTotImponv = 0
        .dTotIva = 0
        .dTotIvav = 0
        .dTotdoc = 0
        .dTotdocv = 0
        .dTotomag = 0
        .dTotomagv = 0
        .dTotdocsom = 0
        .dTotdocsomv = 0
        .dRimanenza = 0
        .dRimanenzav = 0
        .dPagato = 0
        .dPagatov = 0
        .dPagato2 = 0
        .dResto = 0
        .dAbbuono = 0
        .dAbbuonov = 0
        .dEdCompens = 0
        .dEdNonsogg = 0
        .dEdNonsoggiva = 0
        .dEdTotdapagare = 0
        .dEdTotaccscal = 0
        .dEdSpegen = 0
        .dBaseRit = 0
        .dEdRitenut = 0
        .dEdAltriprev = 0
        .dBaseSpegen = 0
        .dBaseCascom = 0
        .dEdPerspegen = 0
        .dEdCascom = 0
        .dEdPercomp = 0
        .dEdPerrite = 0

        'Azzera castelletto iva/omaggi
        For i = 0 To 8
          .nCodiva(i) = 0
          .dImponib(i) = 0
          .dImposta(i) = 0
          .dImponibv(i) = 0
          .dImpostav(i) = 0
          .dImponibciva(i) = 0
          .dImponibcivav(i) = 0
          'Azzera anche il cast. iva degli omaggi P
          .nCodivaOMAGP(i) = 0
          .dImponibOMAGP(i) = 0
          .dImpostaOMAGP(i) = 0
          .dImponibvOMAGP(i) = 0
          .dImpostavOMAGP(i) = 0
          .dImponibcivaOMAGP(i) = 0
          .dImponibcivavOMAGP(i) = 0
        Next i
        'Azzera castelletto contropartite
        For i = 0 To 20
          .nCcontr(i) = 0
          .dImpocont(i) = 0
          .dImpcontv(i) = 0
        Next i
      End With

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Overridable Function Calcola1(ByVal strDitta As String, ByRef dtrTestata As DataRow, ByRef dttTestataC As DataTable, _
                                ByRef dttCorpo As DataTable) As Boolean
    Dim i As Integer = 0
    Dim J As Integer = 0
    Dim y As Integer = 0
    Dim bCanc As Boolean
    Dim wValore As Decimal = 0        'valore della riga al netto degli sconti di riga ed al loro degli sconti di testata e piede (in valuta, se valuta <>0)
    Dim wValoreciva As Decimal = 0    'come wValore ma riferito a prezzo iva compresa 
    Dim dValoreciva As Decimal = 0    'valore riga al netto di tutti gli sconti con prezzo iva compresa
    Dim dImponibile As Decimal = 0
    Dim dImposta As Decimal = 0
    Dim dImpProvv As Decimal = 0
    Dim dTotProv1 As Decimal = 0
    Dim dTotProv2 As Decimal = 0
    Dim dTotColli As Decimal = 0
    Dim dTotPesolordo As Decimal = 0
    Dim dTotPesoNetto As Decimal = 0
    Dim dPesonettoX As Decimal = 0
    Dim dPesolordoX As Decimal = 0
    Dim dTotomagX As Decimal = 0
    Dim dTotomagvX As Decimal = 0
    Dim dTotquantità As Decimal = 0
    '-------------------------------------
    'campi di movord/movmag utilizzati in calcola1
    Dim dMMquant As Decimal = 0
    Dim dMMprezzo As Decimal = 0
    Dim dMMprezvalc As Decimal = 0
    Dim dMMpreziva As Decimal = 0
    Dim dMMscont1 As Decimal = 0
    Dim dMMscont2 As Decimal = 0
    Dim dMMscont3 As Decimal = 0
    Dim dMMcolli As Decimal = 0
    Dim dMMscont4 As Decimal = 0
    Dim dMMscont5 As Decimal = 0
    Dim dMMscont6 As Decimal = 0
    Dim dMMvprovv As Decimal = 0
    Dim dMMvprovv2 As Decimal = 0
    Dim nMMcodiva As Integer = 0
    Dim nMMcontrop As Integer = 0
    Dim strMMstasino As String = "S"
    Dim strMMcodart As String = ""
    Dim strMMumprz As String = "N"
    Dim dMMperqta As Decimal = 0
    Dim lMMacnum As Integer = 0
    Dim strMMflkit As String = ""
    'Retail
    Dim dMMscontv As Decimal = 0
    Dim dMMscontp As Decimal = 0
    'parcellazione
    Dim dMMvalore As Decimal = 0      'il valore a volte non corrispondea qta * prezzo, ma viene indicato un valore a mano
    Dim strMMtiporiga As String = ""  'per determinare se il valore deve venir calcolato oppure no
    Dim strMMcodvpar As String = ""   'codice articolo per parcellazione (tabvpaf)
    Dim dttTmp As New DataTable
    'Contenitore codici iva\aliquote per evitare di ripetere le query

    Try
      '----------------------------------
      'variabili di testmag.testord
      If oPar.strNomProg <> "BSVEFDIN" And oPar.strNomProg <> "BSVEFADI" Then
        'per la fatturazione differita vengono settate prima in CalcolatotaliFattureriepilog
        bScorp = False
        bSpeinc = False
        bBolli = False
      End If

      Select Case oPar.strNomProg
        Case "BSVEBOLL"
          'questa parte è presa dalla Calcola1 contenuta in bsveboll
          With dtrTestata
            If !et_scorpo.ToString = "S" Then bScorp = True
            If !et_flspinc.ToString = "S" Then bSpeinc = True
            If !et_flboll.ToString = "S" Then bBolli = True
            dScont1 = NTSCDec(!et_scont1)
            dScont2 = NTSCDec(!et_scont2)
            dScoPag = NTSCDec(!et_scopag)
            nValuta = NTSCInt(!et_valuta)
            dCambio = NTSCDec(!et_cambio)

            If oPar.bSbloccaIva Then
              'Castelletti sbloccati
              oTestata.dSpeinc = NTSCDec(!et_speinc)
              oTestata.dSpeincv = NTSCDec(!et_speincv)
              oTestata.dBolli = NTSCDec(!et_bolli)
              oTestata.dBolliv = NTSCDec(!et_bolliv)
              oTestata.dTotomag = NTSCDec(!et_totomag)
              oTestata.dTotomagv = NTSCDec(!et_totomagv)

              'Riporta gli omaggi eventualmente gi indicati
              dTotomagLocal = oTestata.dTotomag
              dTotomagvLocal = oTestata.dTotomagv

              For i = 0 To 7
                oTestata.nCodiva(i) = NTSCInt(dtrTestata("et_codiva_" & (i + 1).ToString))
                oTestata.dImponib(i) = NTSCDec(dtrTestata("et_imponib_" & (i + 1).ToString))
                oTestata.dImposta(i) = NTSCDec(dtrTestata("et_imposta_" & (i + 1).ToString))
                oTestata.dImponibv(i) = NTSCDec(dtrTestata("et_imponibv_" & (i + 1).ToString))
                oTestata.dImpostav(i) = NTSCDec(dtrTestata("et_impostav_" & (i + 1).ToString))
              Next
              If Not dttTestataC Is Nothing Then
                If dttTestataC.Rows.Count > 0 Then
                  For i = 0 To 19
                    'da verificare (testmagc) ... 
                    oTestata.nCcontr(i) = NTSCInt(dttTestataC.Rows(0)("tm_ccontr_" & (i + 1).ToString))
                    oTestata.dImpocont(i) = NTSCDec(dttTestataC.Rows(0)("tm_impcont_" & (i + 1).ToString))
                    oTestata.dImpcontv(i) = NTSCDec(dttTestataC.Rows(0)("tm_impcontv_" & (i + 1).ToString))
                  Next
                End If
              End If
            End If    'If oPar.bSbloccaIva Then

            'In ogni caso ...
            lConto = NTSCInt(!et_conto)
            nCodVett = NTSCInt(!et_vettor)
            strPorto = !et_porto.ToString
            nCodPaga = NTSCInt(!et_codpaga)
            strTipork = !et_tipork.ToString
            dtDatDoc = NTSCDate(!et_datdoc)
            dtDatapag = NTSCDate(!et_datapag)
            nCodntra = NTSCInt(!et_codntra)
            nCodese = NTSCInt(!et_codese)
            oTestata.dPagato = NTSCDec(!et_pagato)
            oTestata.dPagato2 = NTSCDec(!et_pagato2)
            oTestata.dResto = NTSCDec(!et_resto)
            oTestata.dPagatov = NTSCDec(!et_pagatov)
            '---------------------------------------
            If oPar.bIncassoTotalePagContanti = True And oPar.bDocEmesso And oPar.bNew Then
              'Rileva il tipo pagamanto impostato
              oCldComm.ValCodiceDb(NTSCInt(!et_codpaga).ToString, strDitta, "TABPAGA", "N", "", dttTmp)
              If dttTmp.Rows.Count > 0 Then
                If NTSCInt(dttTmp.Rows(0)!tb_tippaga) = 4 Then
                  'Azzera i campi incassato
                  oTestata.dPagato = 0
                  oTestata.dPagatov = 0
                End If
              End If
              dttTmp.Clear()
            End If
            '--------------------------------------
            oTestata.dAbbuono = NTSCDec(!et_abbuono)
            oTestata.dAbbuonov = NTSCDec(!et_abbuonov)
            oTestata.dSpeacc = NTSCDec(!et_speacc)
            oTestata.dSpeaccv = NTSCDec(!et_speaccv)
            oTestata.dSpeimb = NTSCDec(!et_speimb)
            oTestata.dSpeimbv = NTSCDec(!et_speimbv)
            'Le spese incasso non vengono considerate nelle ricevute fiscali da fatturare
            If oPar.bSegueFatt AndAlso (strTipork = "F" OrElse (strTipork = "B" AndAlso CBool(ocldBase.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "NonConsiderareSpeincSuDDTEmessi", "0", " ", "0")))) Then
              oTestata.dSpeinc = 0
              oTestata.dSpeincv = 0
            Else
              oTestata.dSpeinc = NTSCDec(!et_speinc)
              oTestata.dSpeincv = NTSCDec(!et_speincv)
            End If
            'Solo se non è su nuovo documento (colli/pesi)
            If oPar.bNew = False Or oPar.bCalcolaColli = False Then oTestata.dTotcoll = NTSCDec(!et_totcoll)
            If oPar.bNew = False Or oPar.bCalcolaPesoLordo = False Then oTestata.dPeso = NTSCDec(!et_peso)
            If oPar.bNew = False Or oPar.bCalcolaPesoNetto = False Then oTestata.dPesonetto = NTSCDec(!et_pesonetto)

            If oPar.bNew = False And oPar.bCalcolaColliPesiSuDocAperti Then
              If oPar.bCalcolaColli Then oTestata.dTotcoll = 0
              If oPar.bCalcPesi Then
                If oPar.bCalcolaPesoLordo Then oTestata.dPeso = 0
                If oPar.bCalcolaPesoNetto Then oTestata.dPesonetto = 0
              End If
            End If
          End With

        Case "BSORGSOR"
          'questa parte è presa dalla Calcola1 contenuta in bsorgsor
          With dtrTestata
            If !et_scorpo.ToString = "S" Then bScorp = True
            If !et_flspinc.ToString = "S" Then bSpeinc = True
            If !et_flboll.ToString = "S" Then bBolli = True
            strTipork = !et_tipork.ToString
            dtDatDoc = NTSCDate(!et_datdoc)
            dtDatapag = NTSCDate(!et_datapag)
            nValuta = NTSCInt(!et_valuta)
            dCambio = NTSCDec(!et_cambio)
            dScont1 = NTSCDec(!et_scont1)
            dScont2 = NTSCDec(!et_scont2)
            dScoPag = NTSCDec(!et_scopag)
            oTestata.dSpeimb = NTSCDec(!et_speimb)
            oTestata.dSpeimbv = NTSCDec(!et_speimbv)
            nCodese = NTSCInt(!et_codese)
            lConto = NTSCInt(!et_conto)
            nCodVett = NTSCInt(!et_vettor)
            strPorto = !et_porto.ToString
            nCodPaga = NTSCInt(!et_codpaga)
            nCodntra = 0

            oTestata.dSpeacc = NTSCDec(!et_speacc)
            oTestata.dSpeaccv = NTSCDec(!et_speaccv)
            oTestata.dSpeinc = NTSCDec(!et_speinc)
            oTestata.dSpeincv = NTSCDec(!et_speincv)
            oTestata.dPagato = NTSCDec(!et_pagato)
            oTestata.dPagato2 = NTSCDec(!et_pagato2)
            oTestata.dResto = NTSCDec(!et_resto)
            oTestata.dPagatov = NTSCDec(!et_pagatov)
            oTestata.dAbbuono = NTSCDec(!et_abbuono)
            oTestata.dAbbuonov = NTSCDec(!et_abbuonov)

            If oPar.bNew = False Or oPar.bCalcolaColli = False Then oTestata.dTotcoll = NTSCDec(!et_totcoll)
            If oPar.bNew = False Or oPar.bCalcolaPesoLordo = False Then oTestata.dPeso = NTSCDec(!et_peso)
            If oPar.bNew = False Or oPar.bCalcolaPesoNetto = False Then oTestata.dPesonetto = NTSCDec(!et_pesonetto)

            If oPar.bNew = False And oPar.bCalcolaColliPesiSuDocAperti Then
              If oPar.bCalcolaColli Then oTestata.dTotcoll = 0
              If oPar.bCalcolaPesoLordo Then oTestata.dPeso = 0
              If oPar.bCalcolaPesoNetto Then oTestata.dPesonetto = 0
            End If
          End With

        Case "BSCRGSOF"
          'questa parte è presa dalla Calcola1 contenuta in bscrgsof
          With dtrTestata
            If !et_scorpo.ToString = "S" Then bScorp = True
            If !et_flspinc.ToString = "S" Then bSpeinc = True
            If !et_flboll.ToString = "S" Then bBolli = True
            strTipork = !et_tipork.ToString
            dtDatDoc = NTSCDate(!et_datdoc)
            dtDatapag = NTSCDate(!et_datdoc)
            nValuta = NTSCInt(!et_valuta)
            dCambio = NTSCDec(!et_cambio)
            dScont1 = NTSCDec(!et_scont1)
            dScont2 = NTSCDec(!et_scont2)
            dScoPag = NTSCDec(!et_scopag)
            oTestata.dSpeimb = NTSCDec(!et_speimb)
            oTestata.dSpeimbv = NTSCDec(!et_speimbv)
            nCodese = NTSCInt(!et_codese)
            lConto = NTSCInt(!et_conto)
            nCodVett = 0
            strPorto = !et_porto.ToString
            nCodPaga = NTSCInt(!et_codpaga)
            nCodntra = 0

            oTestata.dSpeacc = NTSCDec(!et_speacc)
            oTestata.dSpeaccv = NTSCDec(!et_speaccv)
            oTestata.dSpeinc = NTSCDec(!et_speinc)
            oTestata.dSpeincv = NTSCDec(!et_speincv)
            oTestata.dPagato = 0
            oTestata.dPagato2 = 0
            oTestata.dResto = 0
            oTestata.dPagatov = 0
            oTestata.dAbbuono = NTSCDec(!et_abbuono)
            oTestata.dAbbuonov = NTSCDec(!et_abbuonov)

            If oPar.bNew = False Or oPar.bCalcolaColli = False Then oTestata.dTotcoll = 0
            If oPar.bNew = False Or oPar.bCalcolaPesoLordo = False Then oTestata.dPeso = 0
            If oPar.bNew = False Or oPar.bCalcolaPesoNetto = False Then oTestata.dPesonetto = 0

            If oPar.bNew = False And oPar.bCalcolaColliPesiSuDocAperti Then
              If oPar.bCalcolaColli Then oTestata.dTotcoll = 0
              If oPar.bCalcolaPesoLordo Then oTestata.dPeso = 0
              If oPar.bCalcolaPesoNetto Then oTestata.dPesonetto = 0
            End If
          End With

        Case "BSREGSRE"
          'questa parte è presa dalla Calcola1 contenuta in bsveboll
          With dtrTestata
            If !et_scorpo.ToString = "S" Then bScorp = True
            If !et_flspinc.ToString = "S" Then bSpeinc = True
            If !et_flboll.ToString = "S" Then bBolli = True
            dScont1 = NTSCDec(!et_scont1)
            dScont2 = NTSCDec(!et_scont2)
            dScoPag = NTSCDec(!et_scopag)

            If oPar.bSbloccaIva Then
              'Castelletti sbloccati
              oTestata.dSpeinc = NTSCDec(!et_speinc)
              oTestata.dBolli = NTSCDec(!et_bolli)
              oTestata.dTotomag = NTSCDec(!et_totomag)

              'Riporta gli omaggi eventualmente gi indicati
              dTotomagLocal = oTestata.dTotomag
            End If    'If oPar.bSbloccaIva Then

            'In ogni caso ...
            lConto = NTSCInt(!et_conto)
            nCodVett = NTSCInt(!et_vettor)
            strPorto = !et_porto.ToString
            nCodPaga = NTSCInt(!et_codpaga)
            Try  'Documento retail chiamato dal retail ho et_datcom, mentre documento retail chiamato da veboll\orgsor ha et_datdoc
              dtDatDoc = NTSCDate(!et_datcom)
            Catch ex As Exception
              dtDatDoc = NTSCDate(!et_datdoc)
            End Try
            dtDatapag = NTSCDate(!et_datapag)
            nCodntra = NTSCInt(!et_codntra)
            nCodese = NTSCInt(!et_codese)
            oTestata.dPagato = NTSCDec(!et_pagato)
            oTestata.dPagato2 = NTSCDec(!et_pagato2)
            oTestata.dResto = 0     'OCCHIO: DENTRO RETAIL IL RESTO VIENE CALCOLATO NELLA BEREGSRE!!!!!
            '---------------------------------------
            If oPar.bIncassoTotalePagContanti AndAlso oPar.bNew Then
              'Rileva il tipo pagamanto impostato
              oCldComm.ValCodiceDb(NTSCInt(!et_codpaga).ToString, strDitta, "TABPAGA", "N", "", dttTmp)
              If dttTmp.Rows.Count > 0 Then
                If NTSCInt(dttTmp.Rows(0)!tb_tippaga) = 4 Then
                  'Azzera i campi incassato
                  oTestata.dPagato = 0
                End If
              End If
              dttTmp.Clear()
            End If
            '--------------------------------------
            oTestata.dAbbuono = NTSCDec(!et_abbuono)
            oTestata.dSpeacc = NTSCDec(!et_speacc)
            oTestata.dSpeimb = NTSCDec(!et_speimb)
            'Le spese di incasso vengono considerate solo se sono in tm_codpaga, e non nel codpaga2, quindi se non pago con la prima modalità di pagamento, non devo addebitare le spese di incasso
            If oTestata.dPagato = 0 Then
              oTestata.dSpeinc = 0
            Else
              oTestata.dSpeinc = NTSCDec(!et_speinc)
            End If

            'Solo se non è su nuovo documento (colli/pesi)
            If oPar.bNew = False OrElse oPar.bCalcolaColli = False Then oTestata.dTotcoll = NTSCDec(!et_totcoll)
            If oPar.bNew = False OrElse oPar.bCalcolaPesoLordo = False Then oTestata.dPeso = NTSCDec(!et_peso)
            If oPar.bNew = False OrElse oPar.bCalcolaPesoNetto = False Then oTestata.dPesonetto = NTSCDec(!et_pesonetto)

            If oPar.bNew = False AndAlso oPar.bCalcolaColliPesiSuDocAperti Then
              If oPar.bCalcolaColli Then oTestata.dTotcoll = 0
              If oPar.bCalcPesi Then
                If oPar.bCalcolaPesoLordo Then oTestata.dPeso = 0
                If oPar.bCalcolaPesoNetto Then oTestata.dPesonetto = 0
              End If
            End If

          End With

        Case "BSPABOLL"
          'questa parte è presa dalla Calcola1 contenuta in bsveboll
          With dtrTestata
            bScorp = False
            If !et_flspinc.ToString = "S" Then bSpeinc = True Else bSpeinc = False
            If !et_flboll.ToString = "S" Then bBolli = True Else bBolli = False
            If !et_flspegen.ToString = "S" Then bFlspegen = True Else bFlspegen = False
            If !et_flcascom.ToString = "S" Then bFlcascom = True Else bFlcascom = False
            dScont1 = 0
            dScont2 = 0
            dScoPag = 0
            nValuta = 0
            dCambio = 0

            'In ogni caso ...
            lConto = NTSCInt(!et_conto)
            nCodVett = 0
            strPorto = ""
            nCodPaga = NTSCInt(!et_codpaga)
            strTipork = !et_tipork.ToString
            dtDatDoc = NTSCDate(!et_datdoc)
            dtDatapag = NTSCDate(!et_datapag)
            nCodntra = 0
            nCodese = NTSCInt(!et_codese)
            oTestata.dPagato = NTSCDec(!et_pagato)
            oTestata.dPagato2 = 0
            oTestata.dResto = 0
            oTestata.dPagatov = 0
            oTestata.dSpeinc = NTSCDec(!et_speinc)
            '---------------------------------------
            If oPar.bIncassoTotalePagContanti = True And oPar.bDocEmesso And oPar.bNew Then
              'Rileva il tipo pagamanto impostato
              oCldComm.ValCodiceDb(NTSCInt(!et_codpaga).ToString, strDitta, "TABPAGA", "N", "", dttTmp)
              If dttTmp.Rows.Count > 0 Then
                If NTSCInt(dttTmp.Rows(0)!tb_tippaga) = 4 Then
                  'Azzera i campi incassato
                  oTestata.dPagato = 0
                  oTestata.dPagatov = 0
                End If
              End If
              dttTmp.Clear()
            End If
            '--------------------------------------
            oTestata.dAbbuono = NTSCDec(!et_abbuono)
            oTestata.dAbbuonov = 0
            oTestata.dSpeacc = 0
            oTestata.dSpeaccv = 0
            oTestata.dSpeimb = 0
            oTestata.dSpeimbv = 0
            oTestata.dSpeinc = NTSCDec(!et_speinc)
            oTestata.dSpeincv = 0

            oTestata.dTotcoll = 0
            oTestata.dPeso = 0
            oTestata.dPesonetto = 0
            oTestata.dEdPerspegen = NTSCDec(!et_perspegen)
            oTestata.dEdCascom = NTSCDec(!et_cascom)
            oTestata.dEdAltriprev = NTSCDec(!et_altriprev)
            oTestata.dEdPercomp = NTSCDec(!et_percomp)
            oTestata.dEdPerrite = NTSCDec(!et_perrite)
          End With

      End Select

      For i = 0 To dttCorpo.Rows.Count - 1
        With dttCorpo.Rows(i)
          Select Case oPar.strNomProg
            Case "GENERAL"
              dMMquant = NTSCDec(!QUANT)
              dMMprezzo = NTSCDec(!PREZZO)
              dMMprezvalc = NTSCDec(!PREZVALC)
              dMMpreziva = NTSCDec(!PREZIVA)
              dMMscont1 = NTSCDec(!SCONT1)
              dMMscont2 = NTSCDec(!SCONT2)
              dMMscont3 = NTSCDec(!SCONT3)
              dMMscont4 = NTSCDec(!SCONT4)
              dMMscont5 = NTSCDec(!SCONT5)
              dMMscont6 = NTSCDec(!SCONT6)
              dMMcolli = NTSCDec(!COLLI)
              dMMvprovv = NTSCDec(!VPROVV)
              dMMvprovv2 = NTSCDec(!VPROVV2)
              nMMcodiva = NTSCInt(!CODIVA)
              nMMcontrop = NTSCInt(!CONTROP)
              strMMstasino = !STASINO.ToString
              strMMcodart = !CODART.ToString
              strMMumprz = !UMPRZ.ToString
              dMMperqta = NTSCDec(!PERQTA)
              lMMacnum = NTSCInt(!ACNUM)
            Case "BSVEBOLL"
              dMMquant = NTSCDec(!ec_quant)
              dMMprezzo = NTSCDec(!ec_prezzo)
              dMMprezvalc = NTSCDec(!ec_prezvalc)
              dMMpreziva = NTSCDec(!ec_preziva)
              dMMscont1 = NTSCDec(!ec_scont1)
              dMMscont2 = NTSCDec(!ec_scont2)
              dMMscont3 = NTSCDec(!ec_scont3)
              dMMscont4 = NTSCDec(!ec_scont4)
              dMMscont5 = NTSCDec(!ec_scont5)
              dMMscont6 = NTSCDec(!ec_scont6)
              dMMscontp = NTSCDec(!ec_scontp)
              dMMscontv = NTSCDec(!ec_scontv)
              dMMcolli = NTSCDec(!ec_colli)
              dMMvprovv = NTSCDec(!ec_vprovv)
              dMMvprovv2 = NTSCDec(!ec_vprovv2)
              nMMcodiva = NTSCInt(!ec_codiva)
              nMMcontrop = NTSCInt(!ec_controp)
              strMMstasino = !ec_stasino.ToString
              strMMcodart = !ec_codart.ToString
              strMMumprz = !ec_umprz.ToString
              dMMperqta = NTSCDec(!ec_perqta)
              lMMacnum = NTSCInt(!ec_acnum)
              strMMflkit = ntscstr(!ec_flkit)
              'Case "BSORGNNO"    'qui non passa: USA QUELLO DI BSVEBOLL!!!!!
              '  dMMquant = NTSCDec(!MMquant)
              '  dMMprezzo = NTSCDec(!MMprezzo)
              '  dMMprezvalc = NTSCDec(!MMprezvalc)
              '  dMMpreziva = NTSCDec(!MMpreziva)
              '  dMMscont1 = NTSCDec(!MMscont1)
              '  dMMscont2 = NTSCDec(!MMscont2)
              '  dMMscont3 = NTSCDec(!MMscont3)
              '  dMMscont4 = NTSCDec(!MMscont4)
              '  dMMscont5 = NTSCDec(!MMscont5)
              '  dMMscont6 = NTSCDec(!MMscont6)
              '  dMMscontp = NTSCDec(!MMscontp)
              '  dMMscontv = NTSCDec(!MMscontv)
              '  dMMcolli = NTSCDec(!MMcolli)
              '  dMMvprovv = NTSCDec(!MMvprovv)
              '  dMMvprovv2 = NTSCDec(!MMvprovv2)
              '  nMMcodiva = NTSCInt(!MMcodiva)
              '  nMMcontrop = NTSCInt(!MMcontrop)
              '  strMMstasino = !MMstasino.ToString
              '  strMMcodart = !MMcodart.ToString
              '  strMMumprz = ""
              '  dMMperqta = NTSCDec(!MMperqta)
              '  lMMacnum = 0
            Case "BSVEFDIN"
              dMMquant = NTSCDec(!mm_quant)
              dMMprezzo = NTSCDec(!mm_prezzo)
              dMMprezvalc = NTSCDec(!mm_prezvalc)
              dMMpreziva = NTSCDec(!mm_preziva)
              dMMscont1 = NTSCDec(!mm_scont1)
              dMMscont2 = NTSCDec(!mm_scont2)
              dMMscont3 = NTSCDec(!mm_scont3)
              dMMscont4 = NTSCDec(!mm_scont4)
              dMMscont5 = NTSCDec(!mm_scont5)
              dMMscont6 = NTSCDec(!mm_scont6)
              dMMscontp = NTSCDec(!mm_scontp)
              dMMscontv = NTSCDec(!mm_scontv)
              dMMcolli = NTSCDec(!mm_colli)
              dMMvprovv = NTSCDec(!mm_vprovv)
              dMMvprovv2 = NTSCDec(!mm_vprovv2)
              nMMcodiva = NTSCInt(!mm_codiva)
              nMMcontrop = NTSCInt(!mm_controp)
              strMMstasino = !mm_stasino.ToString
              strMMcodart = !mm_codart.ToString
              strMMumprz = !mm_umprz.ToString
              dMMperqta = NTSCDec(!mm_perqta)
              lMMacnum = NTSCInt(!mm_acnum)
              strMMflkit = ""
              'Case "BSVEGNBF"    'qui non passa: USA QUELLO DI BSVEBOLL!!!!!
              '  dMMquant = NTSCDec(!MMquant)
              '  dMMprezzo = NTSCDec(!MMprezzo)
              '  dMMprezvalc = NTSCDec(!MMprezvalc)
              '  dMMpreziva = NTSCDec(!MMpreziva)
              '  dMMscont1 = NTSCDec(!MMscont1)
              '  dMMscont2 = NTSCDec(!MMscont2)
              '  dMMscont3 = NTSCDec(!MMscont3)
              '  dMMscont4 = NTSCDec(!MMscont4)
              '  dMMscont5 = NTSCDec(!MMscont5)
              '  dMMscont6 = NTSCDec(!MMscont6)
              '  dMMscontp = NTSCDec(!MMscontp)
              '  dMMscontv = NTSCDec(!MMscontv)
              '  dMMcolli = NTSCDec(!MMcolli)
              '  dMMvprovv = NTSCDec(!MMvprovv)
              '  dMMvprovv2 = NTSCDec(!MMvprovv2)
              '  nMMcodiva = NTSCInt(!MMcodiva)
              '  nMMcontrop = NTSCInt(!MMcontrop)
              '  strMMstasino = !MMstasino.ToString
              '  strMMcodart = !MMcodart.ToString
              '  strMMumprz = ""
              '  dMMperqta = NTSCDec(!MMperqta)
              '  lMMacnum = 0
            Case "BSORGSOR"
              dMMquant = NTSCDec(!ec_quant)
              dMMprezzo = NTSCDec(!ec_prezzo)
              dMMprezvalc = NTSCDec(!ec_prezvalc)
              dMMpreziva = NTSCDec(!ec_preziva)
              dMMscont1 = NTSCDec(!ec_scont1)
              dMMscont2 = NTSCDec(!ec_scont2)
              dMMscont3 = NTSCDec(!ec_scont3)
              dMMscont4 = NTSCDec(!ec_scont4)
              dMMscont5 = NTSCDec(!ec_scont5)
              dMMscont6 = NTSCDec(!ec_scont6)
              dMMscontp = NTSCDec(!ec_scontp)
              dMMscontv = NTSCDec(!ec_scontv)
              dMMcolli = NTSCDec(!ec_colli)
              dMMvprovv = NTSCDec(!ec_vprovv)
              dMMvprovv2 = NTSCDec(!ec_vprovv2)
              nMMcodiva = NTSCInt(!ec_codiva)
              nMMcontrop = NTSCInt(!ec_controp)
              strMMstasino = !ec_stasino.ToString
              strMMcodart = !ec_codart.ToString
              strMMumprz = !ec_umprz.ToString
              dMMperqta = NTSCDec(!ec_perqta)
              lMMacnum = 0
              strMMflkit = ntscstr(!ec_flkit)
              'Case "BSORGNNP"    'qui non passa: USA QUELLO DI BSVEBOLL!!!!!
              '  dMMquant = NTSCDec(!mn_mmquant)
              '  dMMprezzo = NTSCDec(!mn_prezzo)
              '  dMMprezvalc = NTSCDec(!mn_prezvalc)
              '  dMMpreziva = NTSCDec(!mn_preziva)
              '  dMMscont1 = NTSCDec(!mn_scont1)
              '  dMMscont2 = NTSCDec(!mn_scont2)
              '  dMMscont3 = NTSCDec(!mn_scont3)
              '  dMMscont4 = NTSCDec(!mn_scont4)
              '  dMMscont5 = NTSCDec(!mn_scont5)
              '  dMMscont6 = NTSCDec(!mn_scont6)
              '  dMMscontp = NTSCDec(!mn_scontp)
              '  dMMscontv = NTSCDec(!mn_scontv)
              '  dMMcolli = NTSCDec(!mn_mmcolli)
              '  dMMvprovv = NTSCDec(!mn_mmvprovv)
              '  dMMvprovv2 = NTSCDec(!mn_mmvprovv2)
              '  nMMcodiva = NTSCInt(!mn_codiva)
              '  nMMcontrop = NTSCInt(!mn_controp)
              '  strMMstasino = !mn_stasino.ToString
              '  strMMcodart = !mn_codart.ToString
              '  strMMumprz = ""
              '  dMMperqta = NTSCDec(!mn_perqta)
              '  lMMacnum = 0
            Case "BSCRGSOF"
              dMMquant = NTSCDec(!ec_quant)
              dMMprezzo = NTSCDec(!ec_prezzo)
              dMMprezvalc = NTSCDec(!ec_prezvalc)
              dMMpreziva = NTSCDec(!ec_preziva)
              dMMscont1 = NTSCDec(!ec_scont1)
              dMMscont2 = NTSCDec(!ec_scont2)
              dMMscont3 = NTSCDec(!ec_scont3)
              dMMscont4 = NTSCDec(!ec_scont4)
              dMMscont5 = NTSCDec(!ec_scont5)
              dMMscont6 = NTSCDec(!ec_scont6)
              dMMcolli = NTSCDec(!ec_colli)
              dMMvprovv = 0
              dMMvprovv2 = 0
              nMMcodiva = NTSCInt(!ec_codiva)
              nMMcontrop = 0
              strMMstasino = !ec_stasino.ToString
              strMMcodart = !ec_codart.ToString
              strMMumprz = !ec_umprz.ToString
              dMMperqta = NTSCDec(!ec_perqta)
              lMMacnum = 0
              strMMflkit = ntscstr(!ec_flkit)
            Case "BSREGSRE"
              dMMquant = NTSCDec(!ec_quant)
              dMMprezzo = NTSCDec(!ec_prezzo)
              dMMprezvalc = 0
              dMMpreziva = NTSCDec(!ec_preziva)
              dMMscont1 = NTSCDec(!ec_scont1)
              dMMscont2 = NTSCDec(!ec_scont2)
              dMMscont3 = NTSCDec(!ec_scont3)
              dMMscont4 = NTSCDec(!ec_scont4)
              dMMscont5 = NTSCDec(!ec_scont5)
              dMMscont6 = NTSCDec(!ec_scont6)
              dMMscontp = NTSCDec(!ec_scontp)
              dMMscontv = NTSCDec(!ec_scontv)
              dMMcolli = NTSCDec(!ec_colli)
              dMMvprovv = NTSCDec(!ec_vprovv)
              dMMvprovv2 = NTSCDec(!ec_vprovv2)
              nMMcodiva = NTSCInt(!ec_codiva)
              nMMcontrop = NTSCInt(!ec_controp)
              strMMstasino = !ec_stasino.ToString
              strMMcodart = !ec_codart.ToString
              strMMumprz = !ec_umprz.ToString
              dMMperqta = NTSCDec(!ec_perqta)
              lMMacnum = 0
              strMMflkit = ntscstr(!ec_flkit)
            Case "BSPABOLL"
              dMMquant = NTSCDec(!ec_quant)
              dMMprezzo = NTSCDec(!ec_prezzo)
              dMMscont1 = NTSCDec(!ec_scont1)
              dMMscont2 = NTSCDec(!ec_scont2)
              dMMscont3 = NTSCDec(!ec_scont3)
              dMMvalore = NTSCDec(!ec_valore)
              strMMtiporiga = NTSCStr(!ec_tiporiga)
              nMMcodiva = NTSCInt(!ec_codiva)
              nMMcontrop = NTSCInt(!ec_controp)
              strMMstasino = !ec_stasino.ToString
              strMMcodvpar = !ec_codvpar.ToString
              lMMacnum = NTSCInt(!ec_acnum)
              strMMflkit = ""
            Case Else
              Throw (New NTSException(oApp.Tr(Me, 127791221379375000, "Funzione CALCDOC.Calcola1: nome programma '|" & oPar.strNomProg & "|' non gestito.")))
              Return False
          End Select

          'Lo sconto a valore deve tenere conto dello scorporo ' QUI NON VA FATTO! Lo scontv tiene già conto dell'iva\non iva
          'If dMMscontv <> 0 And bScorp Then
          '  ocldBase.Scorporo(NTSCDec(!ec_scontv), NTSCInt(!ec_codiva), dMMscontv, 0)
          'End If
          '----------------------------------------------------------------------------------------------------------
          With oTestata
            .dAbbuono = ArrDbl(.dAbbuono, oCldComm.TrovaNdec(0))
            .dAbbuonov = ArrDbl(.dAbbuonov, oCldComm.TrovaNdec(0))
            .dBolli = ArrDbl(.dBolli, oCldComm.TrovaNdec(0))
            .dBolliv = ArrDbl(.dBolliv, oCldComm.TrovaNdec(0))
            For y = 0 To 19
              .dImpocont(y) = ArrDbl(.dImpocont(y), oCldComm.TrovaNdec(0))
              .dImpcontv(y) = ArrDbl(.dImpcontv(y), oCldComm.TrovaNdec(0))
            Next
            For y = 0 To 7
              .dImponib(y) = ArrDbl(.dImponib(y), oCldComm.TrovaNdec(0))
              .dImponibv(y) = ArrDbl(.dImponibv(y), oCldComm.TrovaNdec(0))
              .dImposta(y) = ArrDbl(.dImposta(y), oCldComm.TrovaNdec(0))
              .dImpostav(y) = ArrDbl(.dImpostav(y), oCldComm.TrovaNdec(0))
            Next
            .dPagato = ArrDbl(.dPagato, oCldComm.TrovaNdec(0))
            .dPagatov = ArrDbl(.dPagatov, oCldComm.TrovaNdec(0))
            .dSpeacc = ArrDbl(.dSpeacc, oCldComm.TrovaNdec(0))
            .dSpeaccv = ArrDbl(.dSpeaccv, oCldComm.TrovaNdec(0))
            .dSpeimb = ArrDbl(.dSpeimb, oCldComm.TrovaNdec(0))
            .dSpeimbv = ArrDbl(.dSpeimbv, oCldComm.TrovaNdec(0))
            .dSpeinc = ArrDbl(.dSpeinc, oCldComm.TrovaNdec(0))
            .dSpeincv = ArrDbl(.dSpeincv, oCldComm.TrovaNdec(0))
            .dTotomag = ArrDbl(.dTotomag, oCldComm.TrovaNdec(0))
            .dTotomagv = ArrDbl(.dTotomagv, oCldComm.TrovaNdec(0))
            dTotomagLocal = ArrDbl(dTotomagLocal, oCldComm.TrovaNdec(0))
            dTotomagvLocal = ArrDbl(dTotomagvLocal, oCldComm.TrovaNdec(0))
          End With
          '----------------------------------------------------------------------------------------------------------
          'Allinea Perqta
          If dMMperqta = 0 Then dMMperqta = 1
          '-------------------------------------------------------------------
          'Colli/quantità/Pesi
          dTotColli = dTotColli + dMMcolli
          dTotquantità = dTotquantità + dMMquant
          If oPar.bCalcPesi AndAlso (oPar.bCalcolaPesoLordo OrElse oPar.bCalcolaPesoNetto) Then
            dPesolordoX = 0
            dPesonettoX = 0
            'Se è attiva l'opzione di registro di non calcolare i pesi sulle righe kit allora ingnora i padri analitici e i componenti sintentici
            If Not ((strMMflkit = "A" OrElse strMMflkit = "T") AndAlso oPar.bNoPesiSuRigheKitFittizie) Then
              oCldComm.CercaPesi(strDitta, strMMcodart, dPesolordoX, dPesonettoX)
              dTotPesolordo = dTotPesolordo + (dPesolordoX * dMMquant)
              dTotPesoNetto = dTotPesoNetto + (dPesonettoX * dMMquant)
            End If
          End If
          '-------------------------------------------------------------------
          'Salta le righe se 'Sconto merce NC'
          If strMMstasino = "X" Then GoTo SaltaRiga
          '%%%
          If strMMumprz <> "S" Then
            '-------------------------------------------------------------------
            'Provvigioni
            If dMMvprovv <> 0 OrElse dMMvprovv2 <> 0 Then
              dImpProvv = dImpProvv + ArrDbl(ArrDbl(dMMprezzo * dMMquant / dMMperqta _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont1, 0, dMMscont1))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont2, 0, dMMscont2))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont3, 0, dMMscont3))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont4, 0, dMMscont4))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont5, 0, dMMscont5))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont6, 0, dMMscont6))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScontp, 0, dMMscontp))) / 100 _
                        - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScontv, 0, dMMscontv)), oCldComm.TrovaNdec(0)) _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont1Testa, 0, dScont1))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont2Testa, 0, dScont2))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScontoPagam, 0, dScoPag))) / 100, oCldComm.TrovaNdec(0))
            End If
            dTotProv1 = dTotProv1 + dMMvprovv
            dTotProv2 = dTotProv2 + dMMvprovv2
            '-------------------------------------------------------------------
            'Valori riga (lordo sconti testata/piede: wValore)
            If oPar.strNomProg = "BSPABOLL" Then
              wValore = dMMvalore
            Else
              If nValuta <> 0 Then
                wValore = ArrDbl(dMMprezvalc * dMMquant / dMMperqta * (100 - dMMscont1) / 100 * (100 - dMMscont2) / 100 * (100 - dMMscont3) / 100 * (100 - dMMscont4) / 100 * (100 - dMMscont5) / 100 * (100 - dMMscont6) / 100 * (100 - dMMscontp) / 100 - dMMscontv, oCldComm.TrovaNdec((nValuta)))
              Else
                wValore = ArrDbl(dMMprezzo * dMMquant / dMMperqta * (100 - dMMscont1) / 100 * (100 - dMMscont2) / 100 * (100 - dMMscont3) / 100 * (100 - dMMscont4) / 100 * (100 - dMMscont5) / 100 * (100 - dMMscont6) / 100 * (100 - dMMscontp) / 100 - dMMscontv, oCldComm.TrovaNdec(0))
              End If
              If bScorp Then
                wValoreciva = ArrDbl(dMMpreziva * dMMquant / dMMperqta * (100 - dMMscont1) / 100 * (100 - dMMscont2) / 100 * (100 - dMMscont3) / 100 * (100 - dMMscont4) / 100 * (100 - dMMscont5) / 100 * (100 - dMMscont6) / 100 * (100 - dMMscontp) / 100 - dMMscontv, oCldComm.TrovaNdec(0))
                wValore = ArrDbl(((dMMpreziva * dMMquant / dMMperqta * (100 - dMMscont1) / 100 * (100 - dMMscont2) / 100 * (100 - dMMscont3) / 100 * (100 - dMMscont4) / 100 * (100 - dMMscont5) / 100 * (100 - dMMscont6) / 100 * (100 - dMMscontp) / 100 - dMMscontv) * 100) / (100 + AliquotaIva(nMMcodiva)), oCldComm.TrovaNdec(0))
              End If
            End If    'If oPar.strNomProg = "BSPABOLL" Then
          Else
            '-------------------------------------------------------------------
            'Provvigioni
            If dMMvprovv <> 0 OrElse dMMvprovv2 <> 0 Then
              dImpProvv = dImpProvv + ArrDbl(ArrDbl(dMMprezzo * dMMcolli / dMMperqta _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont1, 0, dMMscont1))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont2, 0, dMMscont2))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont3, 0, dMMscont3))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont4, 0, dMMscont4))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont5, 0, dMMscont5))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont6, 0, dMMscont6))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScontp, 0, dMMscontp))) / 100 _
                        - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScontv, 0, dMMscontv)), oCldComm.TrovaNdec(0)) _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont1Testa, 0, dScont1))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScont2Testa, 0, dScont2))) / 100 _
                        * (100 - NTSCDec(IIf(oApp.oGvar.bCalcImponibProvvIgnoraScontoPagam, 0, dScoPag))) / 100, oCldComm.TrovaNdec(0))

            End If
            dTotProv1 = dTotProv1 + dMMvprovv
            dTotProv2 = dTotProv2 + dMMvprovv2
            '-------------------------------------------------------------------
            'Valori riga (lordo sconti testata/piede: wValore)
            If oPar.strNomProg = "BSPABOLL" Then
              wValore = dMMvalore
            Else
              If nValuta <> 0 Then
                wValore = ArrDbl(dMMprezvalc * dMMcolli / dMMperqta * (100 - dMMscont1) / 100 * (100 - dMMscont2) / 100 * (100 - dMMscont3) / 100 * (100 - dMMscont4) / 100 * (100 - dMMscont5) / 100 * (100 - dMMscont6) / 100 * (100 - dMMscontp) / 100 - dMMscontv, oCldComm.TrovaNdec((nValuta)))
              Else
                wValore = ArrDbl(dMMprezzo * dMMcolli / dMMperqta * (100 - dMMscont1) / 100 * (100 - dMMscont2) / 100 * (100 - dMMscont3) / 100 * (100 - dMMscont4) / 100 * (100 - dMMscont5) / 100 * (100 - dMMscont6) / 100 * (100 - dMMscontp) / 100 - dMMscontv, oCldComm.TrovaNdec(0))
              End If
              If bScorp Then
                wValoreciva = ArrDbl(dMMpreziva * dMMcolli / dMMperqta * (100 - dMMscont1) / 100 * (100 - dMMscont2) / 100 * (100 - dMMscont3) / 100 * (100 - dMMscont4) / 100 * (100 - dMMscont5) / 100 * (100 - dMMscont6) / 100 * (100 - dMMscontp) / 100 - dMMscontv, oCldComm.TrovaNdec(0))
                wValore = ArrDbl(((dMMpreziva * dMMcolli / dMMperqta * (100 - dMMscont1) / 100 * (100 - dMMscont2) / 100 * (100 - dMMscont3) / 100 * (100 - dMMscont4) / 100 * (100 - dMMscont5) / 100 * (100 - dMMscont6) / 100 * (100 - dMMscontp) / 100 - dMMscontv) * 100) / (100 + AliquotaIva(nMMcodiva)), oCldComm.TrovaNdec(0))
              End If
            End If    'If oPar.strNomProg = "BSPABOLL" Then
          End If    'If strMMumprz <> "S" Then
          '%%%
          '-------------------------------------------------------------------
          'Valore riga netto (wValore -> oCorpo(i).dValore togliendo gli sconti di testata/piede)
          If nValuta <> 0 Then
            oTestata.dTotlordov = ArrDbl(oTestata.dTotlordov + wValore, oCldComm.TrovaNdec(nValuta))
            'Non considera gli sconti di testata sulle righe di tariffa CONAI
            'Non considera gli sconti di testata sulle righe di rif. ACCONTO
            If UCase(Left(strMMcodart, 5)) = "CONAI" OrElse lMMacnum > 0 Then
              oCorpo(i).dValorev = wValore
            Else
              oCorpo(i).dValorev = ArrDbl(wValore * (100 - dScont1) / 100 * (100 - dScont2) / 100 * (100 - dScoPag) / 100, oCldComm.TrovaNdec(nValuta))
            End If
            oTestata.dTotmercev = ArrDbl(oTestata.dTotmercev + oCorpo(i).dValorev, oCldComm.TrovaNdec(nValuta))
            oCorpo(i).dValore = oCldComm.ConvImpValuta(strDitta, False, oCorpo(i).dValorev, nValuta, dtDatDoc, dCambio, bCanc)
            oTestata.dTotimporighe = ArrDbl(oTestata.dTotimporighe + oCorpo(i).dValore, oCldComm.TrovaNdec(0)) 'per succ. calcolo differenze
          Else
            oTestata.dTotlordo = ArrDbl(oTestata.dTotlordo + wValore, oCldComm.TrovaNdec(0))
            'Non considera gli sconti di testata sulle righe di tariffa CONAI
            'Non considera gli sconti di testata sulle righe di rif. ACCONTO
            If UCase(Left(strMMcodart, 5)) = "CONAI" OrElse lMMacnum > 0 Then
              oCorpo(i).dValore = wValore
            Else
              If oPar.strNomProg = "BSPABOLL" Then
                'per bspaboll il valore non viene mai ricalcolato: viene inserito a mano o calcolato al salva di riga di bnpaboll
                oCorpo(i).dValore = wValore
              Else
                oCorpo(i).dValore = ArrDbl(wValore * (100 - NTSCDec(dScont1)) / 100 * (100 - NTSCDec(dScont2)) / 100 * (100 - NTSCDec(dScoPag)) / 100, oCldComm.TrovaNdec(0))
              End If
            End If
            oTestata.dTotMerce = ArrDbl(oTestata.dTotMerce + oCorpo(i).dValore, oCldComm.TrovaNdec(0))
            oTestata.dTotimporighe = ArrDbl(oTestata.dTotimporighe + oCorpo(i).dValore, oCldComm.TrovaNdec(0)) 'per succ. calcolo differenze
            If bScorp Then
              oTestata.dTotlordociva = ArrDbl(oTestata.dTotlordociva + wValoreciva, oCldComm.TrovaNdec(0))
              'Non considera gli sconti di testata sulle righe di tariffa CONAI
              'Non considera gli sconti di testata sulle righe di rif. ACCONTO
              If UCase(Left(strMMcodart, 5)) = "CONAI" OrElse lMMacnum > 0 Then
                dValoreciva = wValoreciva
              Else
                dValoreciva = ArrDbl(wValoreciva * (100 - NTSCDec(dScont1)) / 100 * (100 - NTSCDec(dScont2)) / 100 * (100 - NTSCDec(dScoPag)) / 100, oCldComm.TrovaNdec(0))
              End If
              oTestata.dTotmerceciva = ArrDbl(oTestata.dTotmerceciva + dValoreciva, oCldComm.TrovaNdec(0))
            End If
          End If    'If nValuta <> 0 Then

          If oPar.strNomProg = "BSPABOLL" Then
            Select Case strMMtiporiga
              Case "C"
                If lMMacnum <> 0 Then
                  oTestata.dEdTotaccscal = oTestata.dEdTotaccscal + wValore ' storno acconto
                Else
                  oTestata.dEdCompens = oTestata.dEdCompens + wValore ' compenso vero e proprio
                End If
              Case "N" : oTestata.dEdNonsogg = oTestata.dEdNonsogg + wValore
                'Case "K": oTestata.dEdNonsoggiva = oTestata.dEdNonsoggiva + wValore
            End Select

            'PARCELLAZIONE: calcolo le basi imponibili per ritenuta/cassa commercial/spese generali
            oCldComm.ValCodiceDb(strMMcodvpar, strDitta, "TABVPAR", "S", "", dttTmp)
            If dttTmp.Rows.Count > 0 Then
              If NTSCStr(dttTmp.Rows(0)!tb_apprit) = "S" Then
                oTestata.dBaseRit = oTestata.dBaseRit + wValore             'base su cui calcolare ritenuta
              End If
              If NTSCStr(dttTmp.Rows(0)!tb_appcascom) = "S" Then
                oTestata.dBaseCascom = oTestata.dBaseCascom + wValore       'base su cui calcolare cassa commercialisti
              End If
              If NTSCStr(dttTmp.Rows(0)!tb_appspegen) = "S" Then
                oTestata.dBaseSpegen = oTestata.dBaseSpegen + wValore       'base su cui calcolare spese generali
              End If
            End If
            dttTmp.Clear()
          End If    'If oPar.strNomProg = "BSPABOLL" Then

          '-------------------------------------------------------------------
          'Omaggi
          If strMMstasino = "M" OrElse strMMstasino = "O" OrElse strMMstasino = "P" Then
            If nValuta <> 0 Then
              If strMMstasino = "P" Then
                'Caso particolare OMAGGI P
                CastellettoIvaOmaggiP1(nMMcodiva, oCorpo(i).dValorev, dValoreciva)
              Else
                dTotomagvX = ArrDbl(dTotomagvX + oCorpo(i).dValorev, oCldComm.TrovaNdec(nValuta))
              End If
            Else
              If strMMstasino = "P" Then
                'Caso particolare OMAGGI P
                CastellettoIvaOmaggiP1(nMMcodiva, oCorpo(i).dValore, dValoreciva)
              Else
                dTotomagX = ArrDbl(dTotomagX + oCorpo(i).dValore, oCldComm.TrovaNdec(0))
              End If
            End If
          End If    'If strMMstasino = "M" Or strMMs
          If oPar.bSbloccaIva = False Then
            '-----------------------------------------------------------------
            'Castelletto IVA
            If nMMcodiva <> 0 Then
              For J = 0 To 8
                If J = 8 Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791221379531250, "Sono gestibili al massimo 8 diverse aliquote IVA per documento.")))
                  Return False
                End If
                If oTestata.nCodiva(J) = nMMcodiva OrElse oTestata.nCodiva(J) = 0 Then
                  oTestata.nCodiva(J) = nMMcodiva
                  If nValuta <> 0 Then
                    oTestata.dImponibv(J) = ArrDbl(NTSCDec(oTestata.dImponibv(J)) + oCorpo(i).dValorev, oCldComm.TrovaNdec(nValuta))
                  Else
                    oTestata.dImponib(J) = (ArrDbl(NTSCDec(oTestata.dImponib(J)) + oCorpo(i).dValore, oCldComm.TrovaNdec(0)))
                    If bScorp Then
                      oTestata.dImponibciva(J) = (ArrDbl(NTSCDec(oTestata.dImponibciva(J)) + dValoreciva, oCldComm.TrovaNdec(0)))
                    End If
                  End If
                  Exit For
                End If
              Next
              '-----------------------------------------------------------------
              'Castelletto contropartite
              If nMMcontrop <> 0 Then
                'Una riga valida per le contropartite
                For J = 0 To 20
                  If J = 20 Then
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791221379687500, "Sono gestibili al massimo 20 diverse contropartite per documento.")))
                    oTestata.nCcontr(20) = -1
                    Return False
                  End If
                  If oTestata.nCcontr(J) = nMMcontrop OrElse oTestata.nCcontr(J) = 0 Then
                    oTestata.nCcontr(J) = nMMcontrop
                    If nValuta <> 0 Then
                      oTestata.dImpcontv(J) = ArrDbl(oTestata.dImpcontv(J) + oCorpo(i).dValorev, oCldComm.TrovaNdec(nValuta))
                    Else
                      oTestata.dImpocont(J) = ArrDbl(oTestata.dImpocont(J) + oCorpo(i).dValore, oCldComm.TrovaNdec(0))
                    End If
                    Exit For
                  End If
                Next J
              End If
            End If
          End If ' If oPar.bSbloccaIva = False Then
          '-------------------------------------------------------------------
SaltaRiga:
        End With
      Next
      DeterminaIvaSpeseIncasso(dttCorpo, oPar.strNomProg)
      '-----------------------------------------------------------------------
      'Fine del loop sulle righe del documento
      '-----------------------------------------------------------------------
      'Provvigioni
      If oPar.bNonCalcolaProvvig = False Then
        oTestata.dImpprov = (oTestata.dImpprov + dImpProvv)
        oTestata.dTotprov = (oTestata.dTotprov + dTotProv1)
        oTestata.dTotprov2 = (oTestata.dTotprov2 + dTotProv2)
      End If
      '-----------------------------------------------------------------------
      'Totale quantità
      oTestata.dTotquant = (oTestata.dTotquant + dTotquantità)
      '-----------------------------------------------------------------------
      'Calcolo dei campi omaggi (solo se castelletto sbloccato)
      If oPar.bSbloccaIva = False Then
        'Attenzione: non contiene più gli omaggi P (imponibile + iva)
        'viene sommato alla fine in Calcola2
        oTestata.dTotomag = ArrDbl(oTestata.dTotomag + dTotomagX, oCldComm.TrovaNdec(nValuta))
        oTestata.dTotomagv = ArrDbl(oTestata.dTotomagv + dTotomagvX, oCldComm.TrovaNdec(0))
        dTotomagLocal = oTestata.dTotomag
        dTotomagvLocal = oTestata.dTotomagv
      End If
      '-----------------------------------------------------------------------
      'Totale colli e pesi solo per i nuovi documenti
      If oPar.bNew OrElse (oPar.bNew = False AndAlso oPar.bCalcolaColliPesiSuDocAperti) Then
        If oPar.bCalcolaColli Then oTestata.dTotcoll = (oTestata.dTotcoll + dTotColli)
        If oPar.bCalcolaPesoLordo Then oTestata.dPeso = (oTestata.dPeso + dTotPesolordo)
        If oPar.bCalcolaPesoNetto Then oTestata.dPesonetto = (oTestata.dPesonetto + dTotPesoNetto)
      End If
      '-----------------------------------------------------------------------
      'Caso particolare scorporo: ridetermina Totmerce da sommatoria castelletti
      'iva (che in questo momento non contengono spese), se i 3 sconti di testata
      'sono uguali allinea totlordo=totmerce
      If bScorp Then
        oTestata.dTotMerce = 0
        For i = 0 To 8
          If oTestata.nCodiva(i) = 0 Then
            Exit For
          Else
            dImponibile = ScorporaIva(oTestata.dImponibciva(i), oTestata.nCodiva(i), dImposta)
            oTestata.dTotMerce = ArrDbl(oTestata.dTotMerce + dImponibile, oCldComm.TrovaNdec(0))
          End If
        Next i
        If dScont1 = 0 AndAlso dScont2 = 0 AndAlso dScoPag = 0 Then
          oTestata.dTotlordo = oTestata.dTotMerce
        End If
      End If
      '-----------------------------------------------------------------------
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Overridable Function Calcola2(ByVal strDitta As String) As Boolean
    Dim bCanc As Boolean
    Dim nImpSpeacc As Decimal = 0 'spese accessorie
    Dim nIvaSpeacc As Decimal = 0
    Dim nImpSpeaccVal As Decimal = 0
    Dim nIvaSpeaccVal As Decimal = 0
    Dim nImpSpeimb As Decimal = 0  'spese imballi
    Dim nIvaSpeimb As Decimal = 0
    Dim nImpSpeimbVal As Decimal = 0
    Dim nIvaSpeimbVal As Decimal = 0
    Dim nImpSpeinc As Decimal = 0  'spese incasso
    Dim nIvaSpeinc As Decimal = 0
    Dim nImpSpeincVal As Decimal = 0
    Dim nIvaSpeincVal As Decimal = 0
    Dim nImpSpbolli As Decimal = 0  'spese bolli
    Dim nIvaSpbolli As Decimal = 0
    Dim nImpSpbolliVal As Decimal = 0
    Dim nIvaSpbolliVal As Decimal = 0
    Dim nImpSpeincciva As Decimal = 0  'importi ivati delle spese sopra
    Dim nImpSpeimbciva As Decimal = 0
    Dim nImpSpeaccciva As Decimal = 0
    Dim nImpSpbolliciva As Decimal = 0
    Dim nCodiva As Integer = 0  'codici iva spese
    Dim nCodIva15 As Integer = 0
    Dim dDiffscorp As Decimal = 0
    Dim i As Integer = 0
    Dim dMaxDiff As Decimal = 0
    Dim dTotomagP As Decimal = 0
    Dim dTotomagvP As Decimal = 0
    Dim nDecV As Integer = 0
    Dim nSpeTraspRipartisciIva As Integer = 0 'ripartizione delle spese di trasporto su tutti i cod. iva del castelletto iva
    Dim dTmp As Decimal = 0
    Dim dTmp1 As Decimal = 0
    Dim dTmp2 As Decimal = 0
    Dim nCodivaInc As Integer = 0
    Try
      'Riporta i totali omaggi come da Calcola1
      oTestata.dTotomag = dTotomagLocal
      oTestata.dTotomagv = dTotomagvLocal
      nSpeTraspRipartisciIva = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "SpeTraspRipartisciIva", "0", " ", "0"))

      '-----------------------------------------------------------------------
      nDecV = oCldComm.TrovaNdec(nValuta)
      If oPar.bSbloccaIva = False Then
        'Castelletti bloccati (caso normale)
        If nValuta <> 0 Then
          'Dai totali in valuta determina i controvalori in lire/euro
          oTestata.dTotlordo = oCldComm.ConvImpValuta(strDitta, False, oTestata.dTotlordov, nValuta, dtDatDoc, dCambio, bCanc)
          oTestata.dTotMerce = oCldComm.ConvImpValuta(strDitta, False, oTestata.dTotmercev, nValuta, dtDatDoc, dCambio, bCanc)
          oTestata.dTotomag = oCldComm.ConvImpValuta(strDitta, False, oTestata.dTotomagv, nValuta, dtDatDoc, dCambio, bCanc)
        End If
        'Riporta l'imponibile contropartita in / su documenti in valuta
        For i = 0 To 20
          If oTestata.nCcontr(i) = 0 Then
            Exit For
          Else
            If nValuta <> 0 Then
              oTestata.dImpocont(i) = oCldComm.ConvImpValuta(strDitta, False, oTestata.dImpcontv(i), nValuta, dtDatDoc, dCambio, bCanc)
              oTestata.dTotimpocontr = ArrDbl(oTestata.dTotimpocontr + oTestata.dImpocont(i), oCldComm.TrovaNdec(0))
            Else
              oTestata.dTotimpocontr = ArrDbl(oTestata.dTotimpocontr + oTestata.dImpocont(i), oCldComm.TrovaNdec(0))
            End If
          End If
        Next i
      Else
        'Castelletti sbloccati (sui doc. di acquisto)
        oTestata.dTotMerce = 0
        oTestata.dTotmercev = 0
        For i = 0 To 20
          If oTestata.nCcontr(i) <> 0 Then
            oTestata.dTotMerce = ArrDbl(oTestata.dTotMerce + oTestata.dImpocont(i), oCldComm.TrovaNdec(0))
            oTestata.dTotmercev = ArrDbl(oTestata.dTotmercev + oTestata.dImpcontv(i), nDecV)
            ' calcola anche qui il totale delle contropartite
            oTestata.dTotimpocontr = ArrDbl(oTestata.dTotimpocontr + oTestata.dImpocont(i), oCldComm.TrovaNdec(0))
          Else
            Exit For
          End If
        Next i
        'in questo caso assume sempre che dtotimporighe sia ugaule a tot. imp. controp.
        oTestata.dTotimporighe = oTestata.dTotimpocontr
        'LORDO=MERCE
        oTestata.dTotlordo = oTestata.dTotMerce
        oTestata.dTotlordov = oTestata.dTotmercev
      End If    'If oPar.bSbloccaIva = False Then

      '-----------------------------------------------------------------------
      ' calcole spese generali, se del caso... su dbasespegen
      If bFlspegen Then
        If bFlspegenCalc = False Then
          bFlspegenCalc = True
          If oTestata.dBaseSpegen <> 0 Then
            oTestata.dEdSpegen = ArrDbl(oTestata.dBaseSpegen * oTestata.dEdPerspegen / 100, oCldComm.TrovaNdec(0))
            oTestata.dBaseCascom = oTestata.dBaseCascom + oTestata.dEdSpegen ' non so se va sempre bene...
            oTestata.dBaseRit = oTestata.dBaseRit + oTestata.dEdSpegen
          Else
            oTestata.dEdSpegen = 0
          End If
        Else
          'la cassa commercialisti va sempre ricalcolata
          If oTestata.dBaseSpegen <> 0 Then
            oTestata.dBaseCascom = oTestata.dBaseCascom + oTestata.dEdSpegen ' non so se va sempre bene...
          End If
        End If
      End If

      '-----------------------------------------------------------------------
      'Inserisce le spese incasso solo se  attivo il flag
      If bSpeinc = False Then
        oTestata.dSpeinc = 0
        oTestata.dSpeincv = 0
      Else
        oTestata.dBaseCascom = oTestata.dBaseCascom + oTestata.dSpeinc ' non so se va sempre bene...
      End If

      '-----------------------------------------------------------------------
      'cassa commercialisti, su dbasecascom
      If bFlcascom Then
        If oTestata.dBaseCascom <> 0 And oTestata.dEdCascom <> 0 Then
          oTestata.dEdNonsoggiva = ArrDbl(oTestata.dBaseCascom * oTestata.dEdCascom / 100, oCldComm.TrovaNdec(0))
        Else
          oTestata.dEdNonsoggiva = 0
        End If
      End If

      '-----------------------------------------------------------------------------------------
      'Aggiorna il castelletto IVA per spegen non effettuando ripartizioni
      If oTestata.dEdSpegen <> 0 Then
        If oPar.nCodese <> 0 Then oPar.nCodivaspegen = oPar.nCodese
        If oPar.nCodivaspegen = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129691264363947488, "Sono state indicate delle 'Spese generali' di piede documento MA non è stato indicato il codice IVA da utilizzare per rilevarle")))
          Return False
        End If
        For i = 1 To 9
          If i = 9 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129691136989907256, "Sono gestibili al massimo 8 diverse aliquote IVA per documento.")))
            Return False
          End If
          If oTestata.nCodiva(i) = oPar.nCodivaspegen Or oTestata.nCodiva(i) = 0 Then
            oTestata.nCodiva(i) = oPar.nCodivaspegen
            oTestata.dImponib(i) = oTestata.dImponib(i) + oTestata.dEdSpegen
            Exit For
          End If
        Next
      End If

      '-----------------------------------------------------------------------------------------
      'Aggiorna il castelletto IVA per cassa commercialisti non effettuando ripartizioni
      If oTestata.dEdNonsoggiva <> 0 Then
        If oPar.nCodese <> 0 Then oPar.nCodivacascom = oPar.nCodese
        If oPar.nCodivacascom = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129691264836504322, "E' stato indicato un importo in 'Cassa commercialisti' di piede documento MA non è stato indicato il codice IVA da utilizzare per rilevarle")))
          Return False
        End If
        Dim nInizio As Integer = NTSCInt(IIf(oPar.strNomProg = "BSPABOLL", 0, 1))
        Dim nFine As Integer = NTSCInt(IIf(oPar.strNomProg = "BSPABOLL", 8, 9))
        For i = nInizio To nFine
          If i = nFine Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129691137108379477, "Sono gestibili al massimo 8 diverse aliquote IVA per documento.")))
            Return False
          End If
          If oTestata.nCodiva(i) = oPar.nCodivacascom Or oTestata.nCodiva(i) = 0 Then
            oTestata.nCodiva(i) = oPar.nCodivacascom
            oTestata.dImponib(i) = oTestata.dImponib(i) + oTestata.dEdNonsoggiva
            Exit For
          End If
        Next
      End If

      '-----------------------------------------------------------------------
      'Spese accessorie, incasso, bolli
      If oTestata.dSpeacc <> 0 Or oTestata.dSpeinc <> 0 Or oTestata.dBolli <> 0 Or oTestata.dSpeimb <> 0 Then
        nImpSpeacc = 0
        nIvaSpeacc = 0
        nImpSpeinc = 0
        nIvaSpeinc = 0
        nImpSpeimb = 0
        nIvaSpeimb = 0
        nImpSpbolli = 0
        nIvaSpbolli = 0
        nImpSpeaccVal = 0
        nIvaSpeaccVal = 0
        nImpSpeimbVal = 0
        nIvaSpeimbVal = 0
        nImpSpeincVal = 0
        nIvaSpeincVal = 0
        nImpSpbolliVal = 0
        nIvaSpbolliVal = 0
        If oPar.bDocEmesso Then
          nCodiva = oPar.nPeveIvaInc
          nCodIva15 = oPar.nPeveIva15
        Else
          nCodiva = oPar.nPeacIvainc
          nCodIva15 = oPar.nPeacIva15
        End If
        'Se esiste un codice esenzione in testata lo utilizza per le spese
        If nCodese <> 0 Then nCodiva = nCodese

        'Se è attiva l'opzione di registro per le spese di incasso prende l'imponibile maggiore
        If oTestata.nIvaSpeseIncasso = 0 Then
          nCodivaInc = nCodiva
        Else
          nCodivaInc = oTestata.nIvaSpeseIncasso
        End If

        If bScorp AndAlso oApp.oGvar.bSpesePiedeIvateDocScorporo Then
          nImpSpeaccciva = oTestata.dSpeacc
          nImpSpeimbciva = oTestata.dSpeimb
          nImpSpeincciva = oTestata.dSpeinc
          nImpSpbolliciva = oTestata.dBolli
          'Calcola l'imponibile
          Dim dIva As Decimal = 0
          ScorporaIva(oTestata.dSpeacc, nCodiva, dIva)
          nImpSpeacc = oTestata.dSpeacc - dIva
          ScorporaIva(oTestata.dSpeimb, nCodiva, dIva)
          nImpSpeimb = oTestata.dSpeimb - dIva
          ScorporaIva(oTestata.dSpeinc, nCodivaInc, dIva)
          nImpSpeinc = oTestata.dSpeinc - dIva
          nImpSpbolli = oTestata.dBolli 'non c' iva comunque...
        Else
          nImpSpeacc = oTestata.dSpeacc
          nImpSpeimb = oTestata.dSpeimb
          nImpSpeinc = oTestata.dSpeinc
          nImpSpbolli = oTestata.dBolli
          If dCambio <> 0 Then
            nImpSpeaccVal = oTestata.dSpeaccv
            nImpSpeimbVal = oTestata.dSpeimbv
            nImpSpeincVal = oTestata.dSpeincv
            nImpSpbolliVal = oTestata.dBolliv
          End If
          If bScorp Then
            'calcola spese accessorie ivate
            nImpSpeaccciva = ArrDbl(oTestata.dSpeacc + oCldComm.CalcolaIva(oTestata.dSpeacc, nCodiva, oCldComm.TrovaNdec(0)), oCldComm.TrovaNdec(0))
            nImpSpeimbciva = ArrDbl(oTestata.dSpeimb + oCldComm.CalcolaIva(oTestata.dSpeimb, nCodiva, oCldComm.TrovaNdec(0)), oCldComm.TrovaNdec(0))
            nImpSpeincciva = ArrDbl(oTestata.dSpeinc + oCldComm.CalcolaIva(oTestata.dSpeinc, nCodivaInc, oCldComm.TrovaNdec(0)), oCldComm.TrovaNdec(0))
            nImpSpbolliciva = ArrDbl(oTestata.dBolli, oCldComm.TrovaNdec(0)) 'non c' iva comunque...
          End If
        End If
      End If    'If oTestata.dSpeacc <> 0 Or oTestata.dSpeinc <> 0 Or oTestata.dBolli <> 0 Or oTestata.dSpeimb <> 0 Then

      '------------------------------------------------------------------------
      'Gestione bolli
      'calcolati fuori, sono gi inizilizzati
      '------------------------------------------------------------------------
      'Rettifica castelletti per spese

      'se le spese di trasporto le devo ripartire su tutti i cod. iva del castelletto, li tratto separatamente
      If (bScorp = False And nValuta = 0 And nImpSpeacc <> 0) And _
           ((nSpeTraspRipartisciIva = -1) Or (nSpeTraspRipartisciIva = 1 And oPar.bDocEmesso = True) _
                                          Or (nSpeTraspRipartisciIva = 2 And oPar.bDocEmesso = False)) Then
        'ripartisco le spese di trasporto su tutti i codici IVA presenti nel castelletto aventi mm_valore <> 0
        'no doc. in valuta
        'no doc iva compresa (perchè anche le spese di trasp. sarebbero iva compresa e si creerebbe un circolo visioso)
        'sia doc nuovi che in modifica
        'sia doc attivi che passivi
        'si reverse charge
        dTmp = 0
        For i = 0 To 8
          If oTestata.nCodiva(i) <> 0 Then
            dTmp += oTestata.dImponib(i)
          Else
            Exit For
          End If
        Next
        For i = 8 To 0 Step -1
          If oTestata.nCodiva(i) <> 0 And oTestata.dImponib(i) <> 0 Then
            If i = 0 Then
              oTestata.dImponib(i) += nImpSpeacc - dTmp2
              dTmp = 0
            Else
              dTmp1 = ArrDbl((nImpSpeacc / dTmp) * oTestata.dImponib(i), oCldComm.TrovaNdec(0))
              dTmp2 += dTmp1
              oTestata.dImponib(i) += dTmp1
            End If
          End If
        Next
      End If    'If bScorp = False And nValuta = 0 And nImpSpeacc <> 0 And nSpeTraspRipartisciIva = -1 Then

      oTestata.dTotSpese = ArrDbl(nImpSpeacc + nImpSpeinc + nImpSpbolli + nImpSpeimb, oCldComm.TrovaNdec(0))
      oTestata.dTotSpesev = ArrDbl(nImpSpeaccVal + nImpSpeincVal + nImpSpbolliVal + nImpSpeimbVal, nDecV)
      If (bScorp = False And nValuta = 0 And nImpSpeacc <> 0) And _
         ((nSpeTraspRipartisciIva = -1) Or (nSpeTraspRipartisciIva = 1 And oPar.bDocEmesso = True) _
                                          Or (nSpeTraspRipartisciIva = 2 And oPar.bDocEmesso = False)) Then
        dTmp = nImpSpeacc
        nImpSpeacc = 0  'azzero per non tenerne conto nel calcolo del cod. iva spese accessorie 
      End If

      If oPar.bSbloccaIva = False Then
        'Castelletti bloccati
        If nImpSpeacc <> 0 OrElse oTestata.dSpeimb <> 0 Then
          'Aggiorna il castelletto IVA per SPINC e SPTRASP non effettuando ripartizioni
          For i = 0 To 8
            If i = 8 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791221379843750, "Sono gestibili al massimo 8 diverse aliquote IVA per documento.")))
              Return False
            End If
            'Caso standard, tutte le spese hanno lo stesso codice iva
            If oTestata.nCodiva(i) = nCodiva OrElse oTestata.nCodiva(i) = 0 Then
              oTestata.nCodiva(i) = nCodiva
              oTestata.dImponib(i) = ArrDbl(oTestata.dImponib(i) + nImpSpeacc + nImpSpeimb, oCldComm.TrovaNdec(0))
              oTestata.dImponibv(i) = ArrDbl(oTestata.dImponibv(i) + nImpSpeaccVal + nImpSpeimbVal, nDecV)
              oTestata.dImponibciva(i) = ArrDbl(oTestata.dImponibciva(i) + nImpSpeaccciva + nImpSpeimbciva, oCldComm.TrovaNdec(0))
              Exit For
            End If
          Next
        End If
        If oTestata.dSpeinc <> 0 Then
          'Aggiorna il castelletto IVA per SPINC e SPTRASP non effettuando ripartizioni
          For i = 0 To 8
            If i = 8 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130916410439391157, "Sono gestibili al massimo 8 diverse aliquote IVA per documento.")))
              Return False
            End If
            'Caso standard, tutte le spese hanno lo stesso codice iva
            If oTestata.nCodiva(i) = nCodivaInc OrElse oTestata.nCodiva(i) = 0 Then
              oTestata.nCodiva(i) = nCodivaInc
              oTestata.dImponib(i) = ArrDbl(oTestata.dImponib(i) + nImpSpeinc, oCldComm.TrovaNdec(0))
              oTestata.dImponibv(i) = ArrDbl(oTestata.dImponibv(i) + nImpSpeincVal, nDecV)
              oTestata.dImponibciva(i) = ArrDbl(oTestata.dImponibciva(i) + nImpSpeincciva, oCldComm.TrovaNdec(0))
              Exit For
            End If
          Next
        End If
        If (bScorp = False And nValuta = 0 And dTmp <> 0) And _
             ((nSpeTraspRipartisciIva = -1) Or (nSpeTraspRipartisciIva = 1 And oPar.bDocEmesso = True) _
                                            Or (nSpeTraspRipartisciIva = 2 And oPar.bDocEmesso = False)) Then
          nImpSpeacc = dTmp 'lo avevo azzerato per non tenerne conto nel calcolo del cod. iva spese accessorie
        End If
        If oTestata.dBolli <> 0 Then
          'Aggiorna il castelletto IVA per i BOLLI
          For i = 0 To 8
            If i = 8 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791221380000000, "Sono gestibili al massimo 8 diverse aliquote IVA per documento.")))
              Return False
            End If
            If oTestata.nCodiva(i) = nCodIva15 Or oTestata.nCodiva(i) = 0 Then
              oTestata.nCodiva(i) = nCodIva15
              oTestata.dImponib(i) = ArrDbl(oTestata.dImponib(i) + nImpSpbolli, oCldComm.TrovaNdec(0))
              oTestata.dImponibv(i) = ArrDbl(oTestata.dImponibv(i) + nImpSpbolliVal, nDecV)
              oTestata.dImponibciva(i) = ArrDbl(oTestata.dImponibciva(i) + nImpSpbolliciva, oCldComm.TrovaNdec(0))
              Exit For
            End If
          Next i
        End If

        '---------------------------------------------------------------------
        'Adesso calcola Iva su castelletti e converte ev. importi in valuta
        If nValuta <> 0 Then
          For i = 0 To 8
            If oTestata.nCodiva(i) = 0 Then
              Exit For
            Else
              'Deve riempire: Imponibile e imposta in Lire e Imposta in Valuta
              oTestata.dImpostav(i) = ArrDbl(oCldComm.CalcolaIva(oTestata.dImponibv(i), oTestata.nCodiva(i), oCldComm.TrovaNdec((nValuta))), nDecV)
              oTestata.dImponib(i) = ArrDbl(oCldComm.ConvImpValuta(strDitta, False, oTestata.dImponibv(i), (nValuta), dtDatDoc, NTSCDec(dCambio), bCanc), oCldComm.TrovaNdec(0))
              oTestata.dImposta(i) = ArrDbl(oCldComm.ConvImpValuta(strDitta, False, oTestata.dImpostav(i), (nValuta), dtDatDoc, NTSCDec(dCambio), bCanc), oCldComm.TrovaNdec(0))
            End If
          Next i
        Else
          For i = 0 To 8
            If oTestata.nCodiva(i) = 0 Then
              Exit For
            Else
              If bScorp Then
                'Scorpora dai totali
                oTestata.dImponib(i) = ScorporaIva(oTestata.dImponibciva(i), oTestata.nCodiva(i), oTestata.dImposta(i))
                oTestata.dImpostav(i) = 0
                oTestata.dImponibv(i) = 0
              Else
                'Deve riempire: imposta in Lire
                oTestata.dImposta(i) = ArrDbl(oCldComm.CalcolaIva(oTestata.dImponib(i), oTestata.nCodiva(i), oCldComm.TrovaNdec(0)), oCldComm.TrovaNdec(0))
                oTestata.dImpostav(i) = 0
                oTestata.dImponibv(i) = 0
              End If
            End If
          Next
        End If    'If nValuta <> 0 Then

        'Adesso calcola l'Iva sul castelletto OMAGGI P e converte ev. importi in valuta/scorporo
        CastellettoIvaOmaggiP2(strDitta, dTotomagP, dTotomagvP)
        oTestata.dTotomagv = ArrDbl(oTestata.dTotomagv + dTotomagvP, nDecV)
        oTestata.dTotomag = ArrDbl(oTestata.dTotomag + dTotomagP, oCldComm.TrovaNdec(0))
      End If    'If oPar.bSbloccaIva = False Then

      '-----------------------------------------------------------------------
      'Riporta i totali imponibile i imposta
      oTestata.dTotImpon = 0
      oTestata.dTotImponv = 0
      oTestata.dTotIva = 0
      oTestata.dTotIvav = 0
      For i = 0 To 8
        If oTestata.nCodiva(i) = 0 Then Exit For
        oTestata.dTotImpon = ArrDbl(oTestata.dTotImpon + oTestata.dImponib(i), oCldComm.TrovaNdec(0))
        oTestata.dTotImponv = ArrDbl(oTestata.dTotImponv + oTestata.dImponibv(i), nDecV)
        oTestata.dTotIva = ArrDbl(oTestata.dTotIva + oTestata.dImposta(i), oCldComm.TrovaNdec(0))
        oTestata.dTotIvav = ArrDbl(oTestata.dTotIvav + oTestata.dImpostav(i), nDecV)
      Next
      '-----------------------------------------------------------------------
      'Ora se scorporo aggiusta i totali per farli tornare caricando ev. differenze sulla
      'prima controp. costi/ricavi....
      If bScorp Then
        dDiffscorp = ArrDbl(oTestata.dTotImpon - (oTestata.dTotimpocontr + nImpSpeacc + nImpSpeinc + nImpSpbolli + nImpSpeimb), oCldComm.TrovaNdec(0))  ' sempre = se cod. val =0
        If dDiffscorp <> 0 Then
          oTestata.dTotimpocontr = oTestata.dTotimpocontr + dDiffscorp
          oTestata.dImpocont(0) = oTestata.dImpocont(0) + dDiffscorp
        End If
      End If
      '-----------------------------------------------------------------------
      'Totale documento
      'calcola le differenze, se presenti nel caso euro-lira
      If nValuta <> 0 Then
        oTestata.dTotdocv = ArrDbl(oTestata.dTotImponv + oTestata.dTotIvav, nDecV)
        If oPar.bSbloccaIva = True Then
          oTestata.dTotdoc = ArrDbl(oTestata.dTotImpon + oTestata.dTotIva, oCldComm.TrovaNdec(0))
        Else
          oTestata.dTotdoc = ArrDbl(oCldComm.ConvImpValuta(strDitta, False, oTestata.dTotdocv, nValuta, dtDatDoc, dCambio, bCanc), oCldComm.TrovaNdec(0))
        End If
      Else
            oTestata.dTotdoc = ArrDbl(oTestata.dTotImpon + oTestata.dTotIva, oCldComm.TrovaNdec(0))
        oTestata.dTotdocv = ArrDbl(oTestata.dTotImponv + oTestata.dTotIvav, nDecV) ' dovrebbe essere sempre a zero se non in valuta ..)
      End If
      'Riporta le differenze
      oTestata.dDiffIva = ArrDbl(oTestata.dTotdoc - (oTestata.dTotImpon + oTestata.dTotIva), oCldComm.TrovaNdec(0))  ' sempre 0 se cod val =0
      oTestata.dDiffDA = ArrDbl(oTestata.dTotdoc - (oTestata.dTotimpocontr + oTestata.dTotIva + nImpSpeacc + nImpSpeinc + nImpSpbolli + nImpSpeimb), oCldComm.TrovaNdec(0))  ' sempre = se cod. val =0
      oTestata.dDiffCorpoRighe = ArrDbl(oTestata.dTotimpocontr - oTestata.dTotimporighe, oCldComm.TrovaNdec(0)) ' forse il confornto nonnva fatto con totimpocontr..
      '-----------------------------------------------------------------------
      'Omaggi
      If nValuta <> 0 Then
        'Annulla piccole differenze sugli omaggi
        If oTestata.dTotomagv > oTestata.dTotdocv And oTestata.dTotdocv > 0 Then
          oTestata.dTotomagv = oTestata.dTotdocv
          oTestata.dTotomag = oTestata.dTotdoc
        Else
          If Math.Abs(oTestata.dTotomag - oTestata.dTotdoc) <= 0.02 Then
            oTestata.dTotomagv = oTestata.dTotdocv
            oTestata.dTotomag = oTestata.dTotdoc
          End If
        End If
      Else
        If oTestata.dTotomag > oTestata.dTotdoc And oTestata.dTotdoc > 0 Then oTestata.dTotomag = oTestata.dTotdoc
        oTestata.dTotomagv = 0
      End If
      '-----------------------------------------------------------------------
      oTestata.dTotdocsom = ArrDbl(oTestata.dTotdoc - oTestata.dTotomag, oCldComm.TrovaNdec(0))
      oTestata.dTotdocsomv = ArrDbl(oTestata.dTotdocv - oTestata.dTotomagv, nDecV)

      If oPar.strNomProg = "BSPABOLL" Then
        oTestata.dEdTotdapagare = oTestata.dTotdoc   'oTestata.dTotdocsom NELLA PARCELLAZIONE NON CI SONO GLI OMAGGI!!!
        '-----------------------------------------------------------------------
        'PARCELLAZIONE: Calcolo ritenute (su dBaserit)
        oTestata.dEdRitenut = ArrDbl(((oTestata.dBaseRit * oTestata.dEdPercomp) / 100) * oTestata.dEdPerrite / 100, oCldComm.TrovaNdec(0))
        'Netto da pagare, al netto ritenute
        oTestata.dEdTotdapagare = (oTestata.dEdTotdapagare - oTestata.dEdRitenut - oTestata.dEdAltriprev)
        oTestata.dRimanenza = ArrDbl(oTestata.dEdTotdapagare - oTestata.dPagato - oTestata.dAbbuono, oCldComm.TrovaNdec(0))
        oTestata.dRimanenzav = 0
      Else
        oTestata.dRimanenza = ArrDbl(oTestata.dTotdocsom - oTestata.dPagato - oTestata.dPagato2 + oTestata.dResto - oTestata.dAbbuono, oCldComm.TrovaNdec(0))
        oTestata.dRimanenzav = ArrDbl(oTestata.dTotdocsomv - oTestata.dPagatov - oTestata.dAbbuonov, nDecV)
      End If

      '-----------------------------------------------------------------------
      'Scostamento tra acconti in bolla e totale doc. in documenti riepilogati
      If (oPar.strNomProg = "BSVEFADI" Or oPar.strNomProg = "BSVEFDIN") And oPar.nGestScostAcconti > 0 And (oTestata.dPagato > 0 Or oTestata.dPagato2 > 0) Then
        dMaxDiff = oPar.dImpoScostAcconti
        'Se la rimanenza  (in assoluto) minore di 5
        If Math.Abs(oTestata.dRimanenza) < dMaxDiff Then
          If oPar.nGestScostAcconti = 1 Then
            'Lo mette come abbuono
            oTestata.dAbbuono = oTestata.dAbbuono + oTestata.dRimanenza
            oTestata.dAbbuonov = oTestata.dAbbuonov + oTestata.dRimanenzav
            oTestata.dRimanenza = 0
            oTestata.dRimanenzav = 0
          Else
            'Rettifica l'acconto
            oTestata.dPagato = oTestata.dPagato + oTestata.dRimanenza
            oTestata.dPagatov = oTestata.dPagatov + oTestata.dRimanenzav
            oTestata.dRimanenza = 0
            oTestata.dRimanenzav = 0
          End If
        End If
      End If
      '-----------------------------------------------------------------------
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Sub DeterminaIvaSpeseIncasso(ByVal dttCorpo As DataTable, ByVal strProgramma As String)
    Dim strSelect, strGroupBy As String
    Dim oDttgr As New CLEGROUPBY
    Dim dttIva As New DataTable
    Try
      'Se non è attiva l'opzione di registro non eseguo l'elaborazione
      If Not oApp.oGvar.bIvaSpeseIncassoDaImponibMaggiore Then Return
      If dttCorpo.Rows.Count = 0 Then Return 'Non ho righe nel corpo. Ignoro.

      'Preparo i dati per la Group BY
      Select Case strProgramma
        Case "GENERAL"
          strSelect = "CODIVA, SUM(VALORE) AS IMPONBILE"
          strGroupBy = "CODIVA"
        Case "BSVEBOLL", "BSORGSOR", "BSCRGSOF", "BSREGSRE", "BSPABOLL"
          strSelect = "ec_codiva AS CODIVA, SUM(ec_valore) AS IMPONIBILE"
          strGroupBy = "ec_codiva"
        Case "BSVEFDIN"
          strSelect = "mm_codiva AS CODIVA, SUM(mm_valore) AS IMPONIBILE"
          strGroupBy = "mm_codiva"
        Case Else : Return
      End Select

      'Eseguo la groupby indifferentemente dai campi utilizzati per applicarla
      oDttgr = New CLEGROUPBY
      If oDttgr.NTSGroupBy(dttCorpo, dttIva, strSelect, "", strGroupBy) Then
        For Each dtrIva As DataRow In dttIva.Select("", "IMPONIBILE DESC")
          'Se il codice iva non ha una aliquota associata (esempio esenzione) lo ignora e passa al successivo
          If oCldComm.AliquotaIva(NTSCInt(dtrIva!CODIVA)) = 0 Then Continue For

          oTestata.nIvaSpeseIncasso = NTSCInt(dtrIva!CODIVA)
          Exit For
        Next
      End If
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------	
    End Try
    End Sub

  '-------------------------------------------------------------------------
  'Riempie il castelletto degli omaggi di tipo P (imponibile+iva)
  '-------------------------------------------------------------------------
  Sub CastellettoIvaOmaggiP1(ByVal nCodiva As Integer, ByVal zValore As Decimal, ByVal zValoreciva As Decimal)
    Dim i As Integer
    Dim nDecv As Integer = 2
    Try
      If nValuta <> 0 Then nDecv = oCldComm.TrovaNdec(nValuta)

      If nCodiva <> 0 Then
        For i = 0 To 7
          If oTestata.nCodivaOMAGP(i) = nCodiva Or oTestata.nCodivaOMAGP(i) = 0 Then
            oTestata.nCodivaOMAGP(i) = nCodiva
            If nValuta <> 0 Then
              oTestata.dImponibvOMAGP(i) = ArrDbl(oTestata.dImponibvOMAGP(i) + zValore, nDecv)
            Else
              oTestata.dImponibOMAGP(i) = ArrDbl(oTestata.dImponibOMAGP(i) + zValore, oCldComm.TrovaNdec(0))
              If bScorp Then
                oTestata.dImponibcivaOMAGP(i) = ArrDbl(oTestata.dImponibcivaOMAGP(i) + zValoreciva, oCldComm.TrovaNdec(0))
              End If
            End If
            Exit For
          End If
        Next i
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub
  '-------------------------------------------------------------------------
  ' Adesso calcola l'Iva sul castelletto OMAGGI P e converte ev. importi in valuta/scorporo
  ' riportando indietro le somme ivate in lire/euro o valuta
  '-------------------------------------------------------------------------
  Sub CastellettoIvaOmaggiP2(ByVal strDitta As String, ByRef dTotomagP As Decimal, ByRef dTotomagvP As Decimal)
    Dim i As Integer
    Dim bCanc As Boolean
    Dim nDecV As Integer

    Try
      If nValuta <> 0 Then
        nDecV = oCldComm.TrovaNdec(nValuta)
        'Valuta
        For i = 0 To 7
          If oTestata.nCodivaOMAGP(i) = 0 Then
            Exit For
          Else
            'Deve riempire: Imponibile e imposta in Lire e Imposta in Valuta
            oTestata.dImpostavOMAGP(i) = oCldComm.CalcolaIva(oTestata.dImponibvOMAGP(i), oTestata.nCodivaOMAGP(i), oCldComm.TrovaNdec(0))
            oTestata.dImponibOMAGP(i) = oCldComm.ConvImpValuta(strDitta, False, oTestata.dImponibvOMAGP(i), (nValuta), dtDatDoc, dCambio, bCanc)
            oTestata.dImpostaOMAGP(i) = oCldComm.ConvImpValuta(strDitta, False, oTestata.dImpostavOMAGP(i), (nValuta), dtDatDoc, dCambio, bCanc)
          End If
        Next i
      Else
        'In lire/euro
        For i = 0 To 7
          If oTestata.nCodivaOMAGP(i) = 0 Then
            Exit For
          Else
            If bScorp Then
              'scorpora dai totali
              oTestata.dImponibOMAGP(i) = ScorporaIva(oTestata.dImponibcivaOMAGP(i), oTestata.nCodivaOMAGP(i), oTestata.dImpostaOMAGP(i))
              oTestata.dImpostaOMAGP(i) = ArrDbl(oTestata.dImpostaOMAGP(i), oCldComm.TrovaNdec(0))
              oTestata.dImpostav(i) = 0
              oTestata.dImponibv(i) = 0
            Else
              'Deve riempire :imposta in Lire
              oTestata.dImpostaOMAGP(i) = oCldComm.CalcolaIva(oTestata.dImponibOMAGP(i), oTestata.nCodivaOMAGP(i), oCldComm.TrovaNdec(0))
              oTestata.dImpostavOMAGP(i) = 0
              oTestata.dImponibvOMAGP(i) = 0
            End If
          End If
        Next i
      End If
      'Ora riporta indietro gli omaggi sommati
      dTotomagP = 0
      dTotomagvP = 0
      For i = 0 To 8
        If oTestata.nCodivaOMAGP(i) = 0 Then Exit For
        If nValuta <> 0 Then
          dTotomagvP = ArrDbl(dTotomagvP + oTestata.dImponibvOMAGP(i) + oTestata.dImpostavOMAGP(i), nDecV)
        End If
        dTotomagP = ArrDbl(dTotomagP + oTestata.dImponibOMAGP(i) + oTestata.dImpostaOMAGP(i), oCldComm.TrovaNdec(0))
      Next i

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Overridable Function CalcolaDoc(ByVal strDitta As String, ByVal oParams As Parametri, _
                            ByRef dtrTestata As DataRow, ByRef dttTestataC As DataTable, _
                            ByRef dttCorpo As DataTable, ByRef Testata As OutTestata, _
                            ByRef Corpo() As OutCorpo, ByRef strError As String) As Boolean
    Dim bResult As Boolean
    Dim bCanc As Boolean
    Dim i As Integer
    Dim nTippaga As Integer = 0
    Dim dttTmp As New DataTable

    Dim dMemSpeacc As Decimal
    Dim dMemTotimpocontr As Decimal
    Dim dMemBaseCascom As Decimal
    Dim dMemEdImponib(8) As Decimal
    Dim dMemEdImponibv(8) As Decimal
    Dim dMemEdImponibciva(8) As Decimal

    Try
      dttCiva = New DataTable 'Svuoto i codici iva per ricaricarli durante il ricalcolo
      '----------------------
      'inizializzo i valori di uscita
      oTestata = Testata
      oCorpo = Corpo
      If oCorpo.Length <> dttCorpo.Rows.Count Then
        ReDim oCorpo(dttCorpo.Rows.Count)
        For i = 0 To oCorpo.Length - 1
          oCorpo(i) = New OutCorpo
        Next
      End If

      '----------------------
      'setto le variabili per il calcolo
      oPar = oParams

      If Calcola0() Then
        If Calcola1(strDitta, dtrTestata, dttTestataC, dttCorpo) Then

          dMemTotimpocontr = oTestata.dTotimpocontr
          dMemBaseCascom = oTestata.dBaseCascom
          For i = 0 To 8
            dMemEdImponib(i) = oTestata.dImponib(i)
            dMemEdImponibv(i) = oTestata.dImponibv(i)
            dMemEdImponibciva(i) = oTestata.dImponibciva(i)
          Next
          bResult = Calcola2(strDitta)
          bFlspegenCalc = True

          '---------------------------
          'calcolo le spese di trasporto
          '' prima calcola/ricalcola le spese di trasporto, se ce ne sono le condizioni
          dMemSpeacc = oTestata.dSpeacc
          '----------------------------------------------------------------------------------------------------------
          '--- Se impostata l'opzione CalcolaSpeseTraspSoloNuovi, il calcolo delle spese di trasporto viene fatto
          '--- solo sui nuovi documenti
          '----------------------------------------------------------------------------------------------------------
          If oPar.bCalcolaSpeseTraspSoloNuovi AndAlso oParams.bNew = False Then GoTo SaltaCalcoloSpesetrasporto
          If oPar.strCalcolaSpeseTrasp = "S" OrElse (oPar.strCalcolaSpeseTrasp = "E" AndAlso oPar.bDocEmesso) Then
            Dim dPercIva As Decimal = 0
            If oPar.bUsaVettore1perCalcoloSpeseTrasp Then nCodVett = 1
            oCldComm.ValCodiceDb(NTSCStr(oPar.nPeveIvaInc), strDittaCorrente, "TABCIVA", "N", , dttTmp)
            If dttTmp.Rows.Count > 0 Then dPercIva = NTSCDec(dttTmp.Rows(0)!tb_aliq)
            oTestata.dSpeacc = CalcSpeTrasp(strDitta, lConto, oPar.nPeveIvaInc, nCodVett, strPorto, oTestata.dPeso, oTestata.dTotdoc - oTestata.dSpeacc * (100 + dPercIva) / 100, oTestata.dTotMerce)
            If nValuta <> 0 Then
              oTestata.dSpeaccv = oCldComm.ConvImpEur(False, oTestata.dSpeacc, nValuta, dtDatDoc, dCambio, bCanc)
              oTestata.dSpeacc = oCldComm.ConvImpValuta(strDitta, False, oTestata.dSpeaccv, nValuta, dtDatDoc, dCambio, bCanc)
            End If
          End If

          '----------------------------------------------------------------------------------------------------------
SaltaCalcoloSpesetrasporto:
          '----------------------------------------------------------------------------------------------------------
          If oTestata.dSpeacc <> dMemSpeacc Then
            ' se cambiato, allora
            ' rimette a posto
            ' e poi ricalcola tutto
            oTestata.dTotimpocontr = dMemTotimpocontr
            oTestata.dBaseCascom = dMemBaseCascom
            For i = 0 To 8
              oTestata.dImponib(i) = dMemEdImponib(i)
              oTestata.dImponibv(i) = dMemEdImponibv(i)
              oTestata.dImponibciva(i) = dMemEdImponibciva(i)
            Next
            bResult = Calcola2(strDitta)
          End If

          '---------------------------
          'calcolo i bolli
          If bResult Then
            'Calcolo dei bolli
            If oPar.bSbloccaIva = False Then
              If oPar.bCalcolaBolli OrElse oPar.nCodese <> 0 OrElse oPar.bDeterminaBolliSuOperazEsenti Then
                If oParams.strNomProg = "BSREGSRE" Then
                  oTestata.dBolli = CalcolaBolli(strDitta, nCodPaga, NTSCDate(dtrTestata!et_datcom))
                Else
                  oTestata.dBolli = CalcolaBolli(strDitta, nCodPaga, NTSCDate(dtrTestata!et_datdoc))
                End If
                If oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "TipoCalcoloBolli", "X", " ", "X") = "D" And oPar.bCalcolaBolli = False Then
                  'se ho tolto la spunta da calcola bolli, non calcolo neanche quelli su operazioni esenti
                  oTestata.dBolli = 0
                End If
                If oTestata.dBolli <> 0 Then
                  If dtrTestata!et_tipork.ToString = "N" OrElse dtrTestata!et_tipork.ToString = "J" OrElse _
                     dtrTestata!et_tipork.ToString = "2" OrElse dtrTestata!et_tipork.ToString = "4" Then
                    'note di accredito: inverto il segno dei bolli
                    oTestata.dBolli = oTestata.dBolli * -1
                  End If
                  If nValuta <> 0 Then
                    oTestata.dBolliv = oCldComm.ConvImpEur(False, oTestata.dBolli, nValuta, dtDatDoc, dCambio, bCanc)
                    ' ricalcola all'indietro imprto in lire/eur
                    oTestata.dBolli = oCldComm.ConvImpValuta(strDitta, False, oTestata.dBolliv, nValuta, dtDatDoc, dCambio, bCanc)
                    ' vecchio sotto
                    'oTestata.dBolliv = ArrDbl(oTestata.dBolliv / dCambio, oCldComm.TrovaNdec(nValuta))
                  End If
                  ' rimette a posto
                  ' e poi ricalcola tutto
                  oTestata.dTotimpocontr = dMemTotimpocontr
                  oTestata.dBaseCascom = dMemBaseCascom
                  For i = 0 To 8
                    oTestata.dImponib(i) = dMemEdImponib(i)
                    oTestata.dImponibv(i) = dMemEdImponibv(i)
                    oTestata.dImponibciva(i) = dMemEdImponibciva(i)
                  Next
                  bResult = Calcola2(strDitta)
                Else
                  If nValuta <> 0 Then oTestata.dBolliv = 0
                End If    'If oTestata.dBolli <> 0 Then
              End If    'If oPar.bCalcolaColli Or oPar.nCodese <> 0 Then
            End If    'If oPar.bSbloccaIva = False Then

            If oParams.bIncassoTotalePagContanti AndAlso oParams.bDocEmesso AndAlso oParams.bNew Then
              'Rileva il tipo pagamanto impostato
              nTippaga = 0
              oCldComm.ValCodiceDb(NTSCInt(dtrTestata!et_codpaga).ToString, strDitta, "TABPAGA", "N", "", dttTmp)
              If dttTmp.Rows.Count > 0 Then nTippaga = NTSCInt(dttTmp.Rows(0)!tb_tippaga)
              If nTippaga = 4 Then 'contanti
                'Inserisce totale incassato = 0 e pone la rimanenza = a zero
                oTestata.dPagato = oTestata.dRimanenza
                oTestata.dPagatov = oTestata.dRimanenzav
                oTestata.dRimanenza = 0
                oTestata.dRimanenzav = 0
              End If
            End If
          Else
            Return False
          End If    'If bResult Then
        Else
          Return False
        End If    'If Calcola1(dtrTestata, dttCorpo) Then
      Else
        Return False
      End If    'If Calcola0() Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function CalcolaDocFattureRieplig(ByVal strDitta As String, ByVal oParams As Parametri, _
                                                    ByRef dtrTestata As DataRow, ByRef dtrTestataC As DataRow, _
                                                    ByRef dttElencoDDT As DataTable, ByRef Testata As OutTestata, _
                                                    ByRef strError As String, ByRef bCalcolaRagg As Boolean) As Boolean
    'ricalcola il totale della fattura differita
    Dim bResult As Boolean
    Dim bCanc As Boolean
    Dim i As Integer

    Dim dMemTotimpocontr As Decimal
    Dim dMemEdImponib(8) As Decimal
    Dim dMemEdImponibv(8) As Decimal
    Dim dMemEdImponibciva(8) As Decimal

    Dim dttMM As New DataTable      'per ogni ddt contiene le righe del corpo del ddt + alcune informazioni di testata
    Dim dttTmp As New DataTable
    Dim dTmp As Decimal = 0
    Dim bStornaDDTResoForn As Boolean = CBool(oCldComm.GetSettingBusDitt(strDitta, "BSVEFDIN", "OPZIONI", ".", "StornaDDTResoForn", "0", " ", "0"))
    Dim nN As Integer = 1

    Try
      '---------------------------
      'Calcola1, Calcola1_bolla e Calcola2 di bsvefdin

      '----------------------
      'inizializzo i valori di uscita
      oPar = oParams
      oTestata = Testata


      If Calcola0() Then

        '------------------------
        For Each dtrT As DataRow In dttElencoDDT.Rows
          oCldComm.GetDDTPerCalcoladoc(strDitta, dtrT!tm_tipork.ToString, NTSCInt(dtrT!tm_anno), dtrT!tm_serie.ToString, NTSCInt(dtrT!tm_numdoc), dttMM)
          If dttMM.Rows.Count = 0 Then
            strError += oApp.Tr(Me, 128722727616718750, "Attenzione! Esiste una o più bolle senza righe. Impossibile continuare.") & vbCrLf
          Else
            '--------------------------------------------------------------------------------------------------------
            '--- Se :
            '----- 'Fattura Differita Ricevuta'
            '----- evade un 'D.D.T.Emesso' di reso (a Fornitore)
            '----- l'opzione di registro BSVEFDIN\OPZIONI\StornDDTResoForn, attiva
            '--- moltiplica *-1 i Colli e le Quantità di riga
            '--------------------------------------------------------------------------------------------------------
            nN = 1
            If (NTSCStr(dtrTestata!tm_tipork) = "K") And _
               (NTSCStr(dtrT!tm_tipork) = "B") And _
               (bStornaDDTResoForn = True) Then
              nN = -1
              For i = 0 To (dttMM.Rows.Count - 1)
                With dttMM.Rows(i)
                  !mm_colli = NTSCDec(!mm_colli) * -1
                  !mm_quant = NTSCDec(!mm_quant) * -1
                End With
              Next
              dttMM.AcceptChanges()
            End If
            '--------------------------------------------------------------------------------------------------------
            'Calcola1_Bolla di bsvefdin
            With dtrTestata
              bSpeinc = False
              bBolli = False
              bScorp = False
              If !tm_flspinc.ToString = "S" Then bSpeinc = True
              If !tm_flbolli.ToString = "S" Then bBolli = True
              If !tm_scorpo.ToString = "S" Then bScorp = True
              dScont1 = NTSCDec(!tm_scont1)
              dScont2 = NTSCDec(!tm_scont2)
              dScoPag = NTSCDec(!tm_scopag)
              nValuta = NTSCInt(!tm_valuta)
              dCambio = NTSCDec(!tm_cambio)

              'le spese di incasso vengono sepre prese dal documento: sia che abbia sbloccato i castelletti, 
              'sia che non li abbia sbloccati (ovvero sono state calcolate all'impostazione del codice pagamento)
              oTestata.dSpeinc = NTSCDec(!tm_speinc)
              oTestata.dSpeincv = NTSCDec(!tm_speincv)
              If !tm_flscdb.ToString = "S" Then     'sbloccaIva = true 
                'Castelletti sbloccati
                oTestata.dBolli = NTSCDec(!tm_bolli)
                oTestata.dBolliv = NTSCDec(!tm_bolliv)
                'Riporta gli omaggi eventualmente già indicati
                dTotomagLocal = NTSCDec(!tm_totomag)
                dTotomagvLocal = NTSCDec(!tm_totomagv)

                For i = 0 To 7
                  oTestata.nCodiva(i) = NTSCInt(dtrTestata("tm_codiva_" & (i + 1).ToString))
                  oTestata.dImponib(i) = NTSCDec(dtrTestata("tm_imponib_" & (i + 1).ToString))
                  oTestata.dImposta(i) = NTSCDec(dtrTestata("tm_imposta_" & (i + 1).ToString))
                  oTestata.dImponibv(i) = NTSCDec(dtrTestata("tm_imponibv_" & (i + 1).ToString))
                  oTestata.dImpostav(i) = NTSCDec(dtrTestata("tm_impostav_" & (i + 1).ToString))
                Next
                For i = 0 To 19
                  'da verificare (testmagc) ... 
                  oTestata.nCcontr(i) = NTSCInt(dtrTestataC("tm_ccontr_" & (i + 1).ToString))
                  oTestata.dImpocont(i) = NTSCDec(dtrTestataC("tm_impcont_" & (i + 1).ToString))
                  oTestata.dImpcontv(i) = NTSCDec(dtrTestataC("tm_impcontv_" & (i + 1).ToString))
                Next
              Else
                'presi dai ddt
                oTestata.dBolli += NTSCDec(dtrT!tm_bolli)
                oTestata.dBolliv += NTSCDec(dtrT!tm_bolliv)
                'oTestata.dTotomag += NTSCDec(dtrT!tm_totomag)    'ricalcolati nella calcola1
                'oTestata.dTotomagv += NTSCDec(dtrT!tm_totomagv)
              End If    'If !tm_flscdb.ToString = "S" Then     'sbloccaIva = true 
              strTipork = !tm_tipork.ToString
              dtDatDoc = NTSCDate(!tm_datdoc)
              dtDatapag = NTSCDate(!tm_datapag)
              nCodntra = NTSCInt(!tm_codntra)
              nCodese = NTSCInt(!tm_codese)
              lConto = NTSCInt(!tm_conto)
              nCodPaga = NTSCInt(!tm_codpaga)

              'presi sempre dai ddt
              oTestata.dSpeacc += NTSCDec(dtrT!tm_speacc)     'spese trasposto
              oTestata.dSpeaccv += NTSCDec(dtrT!tm_speaccv)
              oTestata.dPagato += NTSCDec(dtrT!tm_pagato)
              oTestata.dPagatov += NTSCDec(dtrT!tm_pagatov)
              oTestata.dPagato2 += NTSCDec(dtrT!tm_pagato2)
              oTestata.dResto += NTSCDec(dtrT!tm_resto)
              oTestata.dAbbuono += NTSCDec(dtrT!tm_abbuono)
              oTestata.dAbbuonov += NTSCDec(dtrT!tm_abbuonov)
              oTestata.dSpeimb += NTSCDec(dtrT!tm_speimb)
              oTestata.dSpeimbv += NTSCDec(dtrT!tm_speimbv)

              oParams.bCalcolaColli = False
              oParams.bCalcolaPesoLordo = False
              oParams.bCalcolaPesoNetto = False
              oTestata.dTotcoll += (NTSCDec(dtrT!tm_totcoll) * nN)
              oTestata.dPeso += NTSCDec(dtrT!tm_peso)
              oTestata.dPesonetto += NTSCDec(dtrT!tm_pesonetto)

              oParams.bNonCalcolaProvvig = True ' non fa ricalcoalre le provigioni a calcdoc.calcola1
              oTestata.dImpprov += NTSCDec(dtrT!tm_impprov)
              oTestata.dTotprov += NTSCDec(dtrT!tm_totprov)
              oTestata.dTotprov2 += NTSCDec(dtrT!tm_totprov2)

            End With    'With dtrTestata

            If bCalcolaRagg = False Then
              ReDim oCorpo(dttMM.Rows.Count)
              For i = 0 To oCorpo.Length - 1
                oCorpo(i) = New OutCorpo
              Next
              Calcola1(strDitta, dtrTestata, dtrTestataC.Table, dttMM)
            End If
            If strError <> "" Then Throw New NTSException(strError)

          End If    'If dttMM.Rows.Count = 0 Then
        Next    'For Each dtrT As DataRow In dttElencoDDT.Rows

        '----------------------
        'se devo eseguire il calcolo raggruppato e non sommando le singole righe dei DDT 
        '(per fare un calcolo uguale a quello della stampa bsvefdra)
        'ottengo l'elenco del corpo di dutti i ddt collegati la fattura raggruppati, poi eseguo la calcola1
        If bCalcolaRagg Then
          oCldComm.GetDDTPerCalcoladocRagg(strDitta, dttElencoDDT, dttMM)
          ReDim oCorpo(dttMM.Rows.Count)
          For i = 0 To oCorpo.Length - 1
            oCorpo(i) = New OutCorpo
          Next
          Calcola1(strDitta, dtrTestata, dtrTestataC.Table, dttMM)
        End If

        '------------------------
        'ora calcola le spese di incasso
        'If bSbloccaIva = vbUnchecked Then
        If NTSCStr(dtrTestata!tm_flscdb) <> "S" Then
          'Non calcola le spese incasso se il doc. è aperto e il cod. pagamento corrente
          'è uguale a quello presente al momento dell'apertura, questo per evitare che
          'se riapro il doc. e le spese incasso in TABPAGA sono variate me le cambia
          'anche se non voglio.
          If oParams.bNew = False And NTSCInt(dtrTestata!tm_codpaga) = NTSCInt(dtrTestata("tm_codpaga", DataRowVersion.Original)) Then
            'tengo le spese del documento ...
            GoTo fine1
          End If

          'prendo le spese da tabpaga
          oCldComm.ValCodiceDb(NTSCInt(dtrTestata!tm_codpaga).ToString, strDitta, "TABPAGA", "N", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            If dtrTestata!tm_flspinc.ToString = "S" Then
              dtrTestata!tm_speinc = NTSCDec(dttTmp.Rows(0)!tb_speinca)
            Else
              dtrTestata!tm_speinc = 0
            End If
          End If
          dttTmp.Clear()

          'E le passa nelle variabili opportune
          If NTSCInt(dtrTestata!tm_valuta) <> 0 Then
            dtrTestata!tm_speincv = oCldComm.ConvImpEur(False, NTSCDec(dtrTestata!tm_speinc), NTSCInt(dtrTestata!tm_valuta), NTSCDate(dtrTestata!tm_datdoc), NTSCDec(dtrTestata!tm_cambio), bCanc)                       '(non andrebbe bene... ma ...)
            dtrTestata!tm_speinc = oCldComm.ConvImpValuta(strDitta, False, NTSCDec(dtrTestata!tm_speincv), NTSCInt(dtrTestata!tm_valuta), NTSCDate(dtrTestata!tm_datdoc), NTSCDec(dtrTestata!tm_cambio), bCanc)  '(non andrebbe bene... ma ...)
          End If
        End If    ' If NTSCStr(dtrTestata!tm_flscdb) <> "S" Then
fine1:

        '----------------------------
        'calcola2 di bsvefdin
        dMemTotimpocontr = oTestata.dTotimpocontr
        For i = 0 To 8
          dMemEdImponib(i) = oTestata.dImponib(i)
          dMemEdImponibv(i) = oTestata.dImponibv(i)
          dMemEdImponibciva(i) = oTestata.dImponibciva(i)
        Next
        bResult = Calcola2(strDitta)
        If bResult Then
          If oPar.bSbloccaIva = False Then
            If oPar.bCalcolaBolli Or oPar.nCodese <> 0 Or oPar.bDeterminaBolliSuOperazEsenti Then
              dTmp = oTestata.dBolli
              oTestata.dBolli = CalcolaBolli(strDitta, NTSCInt(dtrTestata!tm_codpaga), NTSCDate(dtrTestata!tm_datdoc))
              If oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "TipoCalcoloBolli", "X", " ", "X") = "D" And oPar.bCalcolaBolli = False Then
                'se ho tolto la spunta da calcola bolli, non calcolo neanche quelli su operazioni esenti
                oTestata.dBolli = 0
              End If
              If dtrTestata!tm_tipork.ToString = "£" Then
                'note di accredito: inverto il segno dei bolli
                oTestata.dBolli = oTestata.dBolli * -1
              End If
              If oTestata.dBolli <> dTmp Then 'se l'importo dei bolli è cambiato, ricalcolo i totali. devo farlo anche se da bolli <> 0 diventati bolli = 0 (ad esempio ddt che evade fattura di acconto con bolli. in fattura diff. imponbile = 0 e non devono essere applicati i bolli!!)
                If NTSCInt(dtrTestata!tm_valuta) <> 0 Then
                  oTestata.dBolliv = oCldComm.ConvImpEur(False, oTestata.dBolli, NTSCInt(dtrTestata!tm_valuta), NTSCDate(dtrTestata!tm_datdoc), NTSCDec(dtrTestata!tm_cambio), bCanc)
                  oTestata.dBolli = oCldComm.ConvImpValuta(strDitta, False, oTestata.dBolliv, NTSCInt(dtrTestata!tm_valuta), NTSCDate(dtrTestata!tm_datdoc), NTSCDec(dtrTestata!tm_cambio), bCanc)
                End If
                ' rimette a posto
                ' e poi ricalcola tutto
                oTestata.dTotimpocontr = dMemTotimpocontr
                For i = 0 To 8
                  oTestata.dImponib(i) = dMemEdImponib(i)
                  oTestata.dImponibv(i) = dMemEdImponibv(i)
                  oTestata.dImponibciva(i) = dMemEdImponibciva(i)
                Next
                bResult = Calcola2(strDitta)
              End If    'If oTestata.dBolli <> 0 Then
            End If    'If oPar.bCalcolaColli Or oPar.nCodese <> 0 Then
          End If    'If oPar.bSbloccaIva = False Then
        End If    'If Calcola2(strDitta) Then

      End If    'If Calcola0() Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttMM.Clear()
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function CalcolaBolli(ByVal strDitta As String, ByVal nCodpaga As Integer) As Decimal
    Try
      'obsoleta
      Return CalcolaBolli(strDitta, nCodpaga, DateTime.Now)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CalcolaBolli(ByVal strDitta As String, ByVal nCodpaga As Integer, ByVal dtDatDoc As DateTime) As Decimal
    ' restituisce nalla valuta principale l'importo dei bolli su fatt. esenti + bolli su tratte
    Dim snaTmp As DataTable = New DataTable
    Dim snaT1 As DataTable = New DataTable
    Dim snaT2 As DataTable = New DataTable
    Dim dBolli As Decimal
    Dim fCanc As Boolean
    Dim dBollitmp As Decimal
    Dim dComodo As Decimal
    Dim strErr As String = ""
    ' per calcolo bolli su tratte
    Dim P As ParamCalcScad = New ParamCalcScad
    Dim nTiprata(60) As Integer ' integra paramcalcscad...
    Dim nRate As Integer
    Dim i As Integer
    Dim dOperazEsenti As Decimal

    Dim strTipoCalcoloBolli As String = oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "TipoCalcoloBolli", "X", " ", "X")
    'strTipoCalcoloBolli T = BOLLI solo su tratte, E = bolli solo su operazioni esenti (es 2 euro se esportatore abituale articolo 8/C):, 
    '                    X = bolli sia su tratte che su operazioni esenti, N = non calcolare mai i bolli, 
    '                    D = se tolto la spunta in veboll/gsor non calcolo mai i bolli, else li calcolo sia su tratte che su operazioni esenti  
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, nCodpaga, dtDatDoc})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCDec(oOut)
      End If
      '----------------

      If strTipoCalcoloBolli = "N" Then
        dBolli = 0
        Return dBolli
      End If

      oCldComm.ValCodiceDb("1", strDitta, "TABBOTR", "N", "", snaTmp)
      If snaTmp.Rows.Count = 0 Then
        Throw New NTSException("Impossibile calcolare i Bolli : manca tabella relativa !")
        Return 0
      End If

      'per i bolli su op. esenti, prima del 26/06/2013 era di 1,81, poi è passato a 2,00
      'nella tabella bolli deve eserci 2,00: nel caso e se la data del doc è ante 26/06/2013 cambio al volo in 1,81
      If dtDatDoc < NTSCDate(IntSetDate("26/06/2013")) Then
        If NTSCDec(snaTmp.Rows(0)!tb_impboes) = 2 Then
          snaTmp.Rows(0)!tb_impboes = 1.81
          snaTmp.AcceptChanges()
        End If
      Else
        'per ora i campi non sono esposti nella form della gestione bolli, ma gengono gestiti correttamente
        'se la data del documento è successiva alla data impostata come paletto
        'l'importo dei bolli esenti è quello di tb_impboes1 e non tb_impboes
        If NTSCDate(snaTmp.Rows(0)!tb_datfinimpboes) <> NTSCDate(IntSetDate("01/01/1900")) And NTSCDec(snaTmp.Rows(0)!tb_impboes1) <> 0 Then
          If dtDatDoc >= NTSCDate(snaTmp.Rows(0)!tb_datfinimpboes) Then
            snaTmp.Rows(0)!tb_impboes = NTSCDec(snaTmp.Rows(0)!tb_impboes1)
          End If
        End If
      End If

      '-----------------------------
      If strTipoCalcoloBolli <> "T" Then
        'Calcolo sel bollo su documenti esenti
        If oPar.bDeterminaBolliSuOperazEsenti = True Then
          'Nuovo modo (determinazione dell'ammontare delle operazioni esenti sul castelletto IVA)
          dOperazEsenti = 0
          For i = 0 To 8
            If oTestata.nCodiva(i) = NTSCInt(snaTmp.Rows(0)!tb_codesb_1) Or _
               oTestata.nCodiva(i) = NTSCInt(snaTmp.Rows(0)!tb_codesb_2) Or _
               oTestata.nCodiva(i) = NTSCInt(snaTmp.Rows(0)!tb_codesb_3) Or _
               oTestata.nCodiva(i) = NTSCInt(snaTmp.Rows(0)!tb_codesb_4) Or _
               oTestata.nCodiva(i) = NTSCInt(snaTmp.Rows(0)!tb_codesb_5) Or _
               oTestata.nCodiva(i) = NTSCInt(snaTmp.Rows(0)!tb_codesb_6) Or _
               oTestata.nCodiva(i) = NTSCInt(snaTmp.Rows(0)!tb_codesb_7) Or _
               oTestata.nCodiva(i) = NTSCInt(snaTmp.Rows(0)!tb_codesb_8) Then
              dOperazEsenti = ArrDbl(dOperazEsenti + oTestata.dImponib(i), oCldComm.TrovaNdec(0))
            End If
          Next
          If dOperazEsenti > NTSCDec(snaTmp.Rows(0)!tb_impftes) Then
            dBolli = NTSCDec(snaTmp.Rows(0)!tb_impboes)
          End If
        Else
          'Vecchio modo (in funzione del cod. di esenzione di testata)
          If nCodese <> 0 Then
            If nCodese = NTSCInt(snaTmp.Rows(0)!tb_codesb_1) Or _
                nCodese = NTSCInt(snaTmp.Rows(0)!tb_codesb_2) Or _
                nCodese = NTSCInt(snaTmp.Rows(0)!tb_codesb_3) Or _
                nCodese = NTSCInt(snaTmp.Rows(0)!tb_codesb_4) Or _
                nCodese = NTSCInt(snaTmp.Rows(0)!tb_codesb_5) Or _
                nCodese = NTSCInt(snaTmp.Rows(0)!tb_codesb_6) Or _
                nCodese = NTSCInt(snaTmp.Rows(0)!tb_codesb_7) Or _
                nCodese = NTSCInt(snaTmp.Rows(0)!tb_codesb_8) Then
              If oTestata.dTotdoc > NTSCDec(snaTmp.Rows(0)!tb_impftes) Then
                dBolli = NTSCDec(snaTmp.Rows(0)!tb_impboes)
              End If
            End If
          End If
        End If
      End If    'If strTipoCalcoloBolli <> "T" Then

      '-----------------------------
      'Calcolo del bollo su tratte
      If bBolli Then
        If strTipoCalcoloBolli <> "E" Then

          oCldComm.ValCodiceDb(nCodpaga.ToString, strDitta, "PAGA", "N", "", snaT1)
          If snaT1.Rows.Count = 0 Then
            'DAVIDE
            'Non ha trovato il codice pagamento
            'probabilmente  a zero.
            'MsgBox "Impossibile calcolare i Bolli : manca tabella relativa !", vbExclamation, "Business"
            snaT1.Clear()
          Else
            If NTSCInt(snaT1.Rows(0)!tb_tippaga) = 1 Then ' solo per tratte
              ' qui calcoalre i boli sulle sigole rate !!!!!
              ' calcola rate residue (o tutte le rate )
              P.nCodpaga = nCodpaga
              'objStd.LegTabeRkAllDitt gstrExt,  "TABPAGA", nCodpaga, fCanc
              If Not fCanc Then
                If NTSCInt(snaT1.Rows(0)!tb_decpaga) = 3 Or NTSCInt(snaT1.Rows(0)!tb_decpaga) = 4 Then
                  P.strDatrif = dtDatapag.ToShortDateString
                Else
                  P.strDatrif = dtDatDoc.ToShortDateString
                End If
              Else
                P.strDatrif = dtDatDoc.ToShortDateString
              End If

              P.dTotfat = ArrDbl(oTestata.dTotdoc - oTestata.dTotomag, oCldComm.TrovaNdec(0))
              If (oTestata.dPagato <> 0 Or oTestata.dPagato2 <> 0 Or oTestata.dAbbuono <> 0) Then
                P.dTotfat = ArrDbl(oTestata.dTotdoc - oTestata.dTotomag - oTestata.dPagato - oTestata.dPagato + oTestata.dResto - oTestata.dAbbuono, oCldComm.TrovaNdec(0))
              End If
              ' fatt. acq. cee
              If (strTipork = "K" Or strTipork = "L" Or strTipork = "J" Or strTipork = "(") And nCodntra <> 0 Then
                P.dTotfat = P.dTotfat - oTestata.dTotIva
              End If
              If P.dTotfat = 0 Then GoTo aggscadenend ' no more rate da costruire...(doc. tutto pagato o abbuonato)

              P.dTotfatval = oTestata.dTotdocv ' migliorare ... passando anche importo in valuta
              ' fatt. acqusiti cee
              If (strTipork = "K" Or strTipork = "L" Or strTipork = "J" Or strTipork = "(") And nCodntra <> 0 Then
                P.dTotfatval = P.dTotfatval - oTestata.dTotIvav
              End If
              P.dIva = ArrDbl(oTestata.dTotIva, oCldComm.TrovaNdec(0))
              P.dIvaval = ArrDbl(oTestata.dTotIvav, oCldComm.TrovaNdec(nValuta))
              P.dSpese = ArrDbl(oTestata.dTotSpese, oCldComm.TrovaNdec(0))
              P.dSpeseval = ArrDbl(oTestata.dTotSpesev, oCldComm.TrovaNdec(nValuta))
              nRate = CalcolaScad(strDitta, P.nCodpaga, P.strDatrif, P.dTotfat, P.dTotfatval, P.dIva, _
                                  P.dIvaval, P.dSpese, P.dSpeseval, P.strDatsca, P.dImpsca, P.dImpscaval, nTiprata, strErr, 0)
              If strErr <> "" Then Throw New NTSException(strErr)

              If nRate < 1 Then GoTo aggscadenend
              For i = 1 To nRate
                If nTiprata(i) = 1 Then
                  If nCodese = 0 Then
                    dBollitmp = P.dImpsca(i) * NTSCDec(snaTmp.Rows(0)!tb_botrita) / 100
                  Else
                    oCldComm.ValCodiceDb(nCodese.ToString, strDitta, "TABCIVA", "N", "", snaT2)
                    If NTSCInt(snaT2.Rows(0)!tb_tipiva) = 2 Then
                      dBollitmp = P.dImpsca(i) * NTSCDec(snaTmp.Rows(0)!tb_botrest) / 100
                    Else
                      dBollitmp = P.dImpsca(i) * NTSCDec(snaTmp.Rows(0)!tb_botrita) / 100
                    End If
                    snaT2.Clear()
                  End If
                  ' adesso applica arrot. e minimo bollo
                  If NTSCDec(snaTmp.Rows(0)!tb_arrot) = 0 Then
                    dComodo = 0
                  Else
                    dComodo = NTSCInt((dBollitmp + NTSCDec(snaTmp.Rows(0)!tb_arrot) - 0.01) / NTSCDec(snaTmp.Rows(0)!tb_arrot))
                  End If
                  dComodo = dComodo * NTSCDec(snaTmp.Rows(0)!tb_arrot)
                  If dComodo < NTSCDec(snaTmp.Rows(0)!tb_minimo) Then
                    dComodo = NTSCDec(snaTmp.Rows(0)!tb_minimo)
                  End If
                  dBolli = dBolli + ArrDbl(dComodo, oCldComm.TrovaNdec(0))
                End If
              Next
aggscadenend:
            End If
            snaT1.Clear()
          End If    'If snaT1.Rows.Count = 0 Then
        End If    'If strTipoCalcoloBolli <> "E" Then
      End If    'If bBolli Then
      snaTmp.Clear()
      Return dBolli

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      snaTmp.Clear()
      snaT1.Clear()
      snaT2.Clear()
    End Try
  End Function


  Public Overridable Function CalcolaScad(ByVal strDitta As String, ByVal nCodpaga As Integer, ByVal strDatrif As String, _
                              ByVal dTotfat As Decimal, ByVal dTotfatval As Decimal, ByVal dIva As Decimal, _
                              ByVal dIvaval As Decimal, ByVal dSpese As Decimal, ByVal dSpeseval As Decimal, _
                              ByRef strDatsca() As String, ByRef dImpsca() As Decimal, ByRef dImpscaval() As Decimal, _
                              ByRef nTiporata() As Integer, ByRef strErr As String, Optional ByVal nCodvalu As Integer = 99, _
                              Optional ByVal lConto As Integer = 0) As Integer
    ' calcola scadenze
    Dim nDec As Integer

    Dim bInvertiFMmonorata As Boolean

    Dim nNumrate As Integer, nGioiniz As Integer
    Dim nGioperi As Integer, nPrirata As Integer
    Dim nDecpaga As Integer, nGiofiss As Integer
    Dim nMesees1 As Integer, nMesees2 As Integer
    Dim nMesi As Integer, nGiorni As Integer
    Dim nMesi2 As Integer, nGiorni2 As Integer
    Dim bErafinemese As Boolean
    Dim nMesiper As Integer, nGiorniper As Integer
    Dim strShiftm As String, nGioiniz2 As Integer
    Dim strDatprec As String, nRate As Integer
    Dim dGiaass As Decimal, dGiaassval As Decimal
    Dim dGiaass1 As Decimal, dGiass1val As Decimal
    Dim i As Integer, n As Integer, nTippaga As Integer
    '-- calcolo condiz. particolari
    Dim strFlcondp As String
    Dim nTipp(6) As Integer, dQuota(6) As Decimal, nGiorn(6) As Integer, strBase(6) As String
    Dim strMemdatprec As String
    Dim strMemdatprec2 As String

    Dim nDecval As Integer
    Dim nCodvaluta As Integer
    Dim snaAnag As DataTable = New DataTable
    Dim nGiof As Integer, nMesf As Integer

    Dim dttTmp As DataTable = New DataTable
    Dim dValresiduo As Decimal = 0
    Dim dValresiduoval As Decimal = 0
    Dim strGgcalend As String = ""     '= A o = B è una forma di pagam. da calcolare secondo art. 62/2012 (merci deperibili), ovvero entro 30 GG (A) / 60 (B) gg da fine mese della data di ricevimento fattura (ovvero data diversa)
    Dim strDtMax As String = ""         'data massima entro la quale devono essere inserite tutte le scadenze
    Dim bFineMese As Boolean = False

    Try
      strErr = ""

      ' determina cod. valuta
      nCodvaluta = nCodvalu
      If nCodvaluta = 0 Then nCodvaluta = 99

      ' determina i n. decimali della valuta di default
      nDec = 2

      ' adesso per la seconda valuta ...
      Select Case nCodvaluta
        Case 99 : nDecval = 2
        Case 100 : nDecval = 0 ' lira come codice valuta
        Case Else : nDecval = 2
      End Select
      ' ok . ho determinato il sistema degli arrotondamenti (decimali su valuta)

      dGiaass = 0
      dGiaassval = 0
      For i = 1 To 60
        strDatsca(i) = ""
        dImpsca(i) = 0
        dImpscaval(i) = 0
        nTiporata(i) = 0
      Next i
      oCldComm.ValCodiceDb(nCodpaga.ToString, strDitta, "TABPAGA", "N", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        dttTmp.Clear()
        Return 0
      End If
      nTippaga = NTSCInt(dttTmp.Rows(0)!tb_tippaga)
      nNumrate = NTSCInt(dttTmp.Rows(0)!tb_numrate)
      nGioiniz = NTSCInt(dttTmp.Rows(0)!tb_gioiniz)
      nGioperi = NTSCInt(dttTmp.Rows(0)!tb_gioperi)
      nPrirata = NTSCInt(dttTmp.Rows(0)!tb_prirata)
      nDecpaga = NTSCInt(dttTmp.Rows(0)!tb_decpaga)
      nGiofiss = NTSCInt(dttTmp.Rows(0)!tb_giofiss)
      nMesees1 = NTSCInt(dttTmp.Rows(0)!tb_mesees1)
      nMesees2 = NTSCInt(dttTmp.Rows(0)!tb_mesees2)
      ' nuovi campi
      strShiftm = "N"
      nGioiniz2 = 0
      strShiftm = dttTmp.Rows(0)!tb_shiftm.ToString
      nGioiniz2 = NTSCInt(dttTmp.Rows(0)!tb_gioiniz2)
      If nGioiniz2 = 0 Then nGioiniz2 = nGioiniz + nGioperi

      '--
      ' condiz. particolari ...
      strFlcondp = dttTmp.Rows(0)!tb_flcondp.ToString

      If strFlcondp = "S" Then
        nTipp(1) = NTSCInt(dttTmp.Rows(0)!tb_tipp_1)
        nTipp(2) = NTSCInt(dttTmp.Rows(0)!tb_tipp_2)
        nTipp(3) = NTSCInt(dttTmp.Rows(0)!tb_tipp_3)
        nTipp(4) = NTSCInt(dttTmp.Rows(0)!tb_tipp_4)
        nTipp(5) = NTSCInt(dttTmp.Rows(0)!tb_tipp_5)
        nTipp(6) = NTSCInt(dttTmp.Rows(0)!tb_tipp_6)
        dQuota(1) = NTSCDec(dttTmp.Rows(0)!tb_quota_1)
        dQuota(2) = NTSCDec(dttTmp.Rows(0)!tb_quota_2)
        dQuota(3) = NTSCDec(dttTmp.Rows(0)!tb_quota_3)
        dQuota(4) = NTSCDec(dttTmp.Rows(0)!tb_quota_4)
        dQuota(5) = NTSCDec(dttTmp.Rows(0)!tb_quota_5)
        dQuota(6) = NTSCDec(dttTmp.Rows(0)!tb_quota_6)
        nGiorn(1) = NTSCInt(dttTmp.Rows(0)!tb_giorni_1)
        nGiorn(2) = NTSCInt(dttTmp.Rows(0)!tb_giorni_2)
        nGiorn(3) = NTSCInt(dttTmp.Rows(0)!tb_giorni_3)
        nGiorn(4) = NTSCInt(dttTmp.Rows(0)!tb_giorni_4)
        nGiorn(5) = NTSCInt(dttTmp.Rows(0)!tb_giorni_5)
        nGiorn(6) = NTSCInt(dttTmp.Rows(0)!tb_giorni_6)
        strBase(1) = dttTmp.Rows(0)!tb_base_1.ToString
        strBase(2) = dttTmp.Rows(0)!tb_base_2.ToString
        strBase(3) = dttTmp.Rows(0)!tb_base_3.ToString
        strBase(4) = dttTmp.Rows(0)!tb_base_4.ToString
        strBase(5) = dttTmp.Rows(0)!tb_base_5.ToString
        strBase(6) = dttTmp.Rows(0)!tb_base_6.ToString
      End If

      ' determina se c'e il cod. cliente deroga ai mesi esclusi e giorno fisso
      ' (ha priorit quello indicato sull'anagr. del cliente
      If lConto <> 0 Then
        oCldComm.ValCodiceDb(lConto.ToString, strDitta, "ANAGRACF", "N", "", snaAnag)
        If snaAnag.Rows.Count > 0 Then

          If NTSCInt(snaAnag.Rows(0)!an_mesees1) <> 0 Then
            nMesees1 = NTSCInt(snaAnag.Rows(0)!an_mesees1)
            nMesees2 = NTSCInt(snaAnag.Rows(0)!an_mesees2)
          End If
          If NTSCInt(snaAnag.Rows(0)!an_giofiss) <> 0 Then
            nGiofiss = NTSCInt(snaAnag.Rows(0)!an_giofiss)
          End If
        End If
        snaAnag.Clear()
      End If    'If lConto <> 0 Then

      '--
      nRate = nNumrate
      strDatprec = strDatrif
      ' setta default
      bInvertiFMmonorata = False
      '-- eccezione : se fine mese, 45 gg, monorata, se opazione di registro \\OPZIONI\InvertiFMmonorata = -1
      ' applica prima i giorni e poi il fine mese ... (solo per n. giorni non multiplo di 30 !!!
      If nRate = 1 And nDecpaga = 2 And ((nGioiniz Mod 30) <> 0) And strFlcondp = "N" Then
        bInvertiFMmonorata = CBool(oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "InvertiFMmonorata", "0", " ", "0"))
        If bInvertiFMmonorata Then
          GoTo prosegui2
        End If
      End If
      '---
      If nDecpaga = 2 Then   ' scadenza fine mese modific datprec
        strDatprec = FineMese(strDatprec)
      End If
prosegui2:
      If nDecpaga = 6 Then
        If dttTmp.Rows(0)!tb_datafdec.ToString = "" Then
          Throw New NTSException("Attenzione: manca data fissa decorrenza su pagamento " & nCodpaga)
        Else
          strDatprec = dttTmp.Rows(0)!tb_datafdec.ToString
        End If
      End If

      strGgcalend = NTSCStr(dttTmp.Rows(0)!tb_ggcalend).Trim
      If strGgcalend = "A" Then
        strDtMax = DateAdd("d", 30, FineMese(strDatprec)).ToShortDateString
      End If
      If strGgcalend = "B" Then
        strDtMax = DateAdd("d", 60, FineMese(strDatprec)).ToShortDateString
      End If

      dttTmp.Clear()

      ' -- spezzo i giorni fissi in modo logico (30 gg = 1 mese)
      nMesf = NTSCInt(Fix(nGiofiss / 30))
      nGiof = nGiofiss - nMesf * 30

      ' adesso calcolo diverso se condizioni particolari ....
      If strFlcondp = "N" Then
        nMesi = NTSCInt(Fix(nGioiniz / 30))
        nGiorni = nGioiniz - nMesi * 30
        nMesi2 = NTSCInt(Fix(nGioiniz2 / 30)) ' dec. seconda rata
        nGiorni2 = nGioiniz2 - nMesi2 * 30 ' dec. seconda rata
        nMesiper = NTSCInt(Fix(nGioperi / 30))
        nGiorniper = nGioperi - nMesiper * 30
        ' calcola il n. di rate totali (ecc. 1.a rata)
        For i = 1 To nRate
          nTiporata(i) = nTippaga ' unico tipo di pagamento ...
          If i = 1 Then
            strDatsca(i) = DateAdd("m", nMesi, strDatprec).ToShortDateString
            If EstFineMese(strDatprec) Then strDatsca(i) = FineMese(strDatsca(i))
            strDatsca(i) = DateAdd("d", nGiorni, strDatsca(i)).ToShortDateString
            If nDecpaga = 5 Then   ' scadenza fine mese solo dalla 2.a rata in poi
              strDatprec = FineMese(strDatprec)
            End If
            '--- tratta l'eccezione...
            If bInvertiFMmonorata Then   ' porta dopo a fine mese...
              strDatsca(i) = FineMese(strDatsca(i))
            End If
          Else
            If i = 2 Then 'And nNumrate > 2 Then ' eccezione : pi di due rate
              strDatsca(i) = DateAdd("m", nMesi2, strDatprec).ToShortDateString
              If EstFineMese(strDatprec) Then strDatsca(i) = FineMese(strDatsca(i))
              strDatsca(i) = DateAdd("d", nGiorni2, strDatsca(i)).ToShortDateString
              'If nDecpaga = 5 Then   ' scadenza fine mese solo dalla 2.a rata in poi
              '  strDatprec = cercapsp.FineMese(strDatprec)
              'End If
            Else ' seconda rata di max 2 oppure dalla 3.a in poi ...
              strDatsca(i) = DateAdd("m", nMesi2 + (nMesiper * (i - 2)), strDatprec).ToShortDateString
              If EstFineMese(strDatprec) Then strDatsca(i) = FineMese(strDatsca(i))
              ' qui ridefinire in mesi/giorni l'espessione sotto da aumentare...
              strDatsca(i) = DateAdd("d", nGiorni2 + (nGiorniper * (i - 2)), strDatsca(i)).ToShortDateString
            End If
          End If
          ' se eccezione mesi allora (lavorare su var. di comodo non strDatsca!!)
          If DatePart("m", strDatsca(i)) = nMesees1 Or DatePart("m", strDatsca(i)) = nMesees2 Then
            strMemdatprec2 = strDatsca(i) ' salva la data prima ..
            strDatsca(i) = DateAdd("m", nMesf, FineMese(strDatsca(i))).ToShortDateString '!!!!!! variato !!!! 9/3/98
            strDatsca(i) = DateAdd("d", nGiof, (strDatsca(i))).ToShortDateString ' modificato

            If strShiftm = "S" Then
              'se shifta, allore, sposta anche strdatprec di un mese
              If EstFineMese(strDatprec) Then bErafinemese = True Else bErafinemese = False
              strDatprec = DateAdd("m", 1, strDatprec).ToShortDateString
              If bErafinemese Then
                strDatprec = FineMese(strDatprec)
              End If
              If EstFineMese(strMemdatprec2) Then ' se era fine mese, allora
                'se  indicato un fine mese 30/60/...
                If nGiof = 0 And nMesf > 0 Then strDatsca(i) = FineMese(strDatsca(i))
              End If
            Else
              'nessuno shift, porta comunque a fine mese
              If EstFineMese(strMemdatprec2) Then
                'se  indicato un fine mese 30/60/...
                If nGiof = 0 And nMesf > 0 Then strDatsca(i) = FineMese(strDatsca(i))
              End If
            End If
          End If
          ' gestire le eccezioni sugli importi ....
          ' 1 = 1.a rata di sola iva
          ' 2 = 1.a rata di iva e spese
          ' 3 = iva su prima rata
          ' 4 = iva e spese su prima rata
          If i = nRate Then
            dImpsca(i) = dTotfat - dGiaass
            dImpscaval(i) = dTotfatval - dGiaassval
          Else
            If i = 1 Then    'est la prima rata
              Select Case nPrirata
                Case 0
                  dImpsca(i) = ArrDbl(dTotfat / nRate, nDec)
                  dImpscaval(i) = ArrDbl(dTotfatval / nRate, nDecval)

                Case 1
                  dImpsca(i) = dIva
                  dImpscaval(i) = dIvaval

                Case 2
                  dImpsca(i) = dIva + dSpese
                  dImpscaval(i) = dIvaval + dSpeseval

                Case 3
                  dImpsca(i) = ArrDbl((dTotfat - dIva) / nRate, nDec) + dIva
                  dImpscaval(i) = ArrDbl((dTotfatval - dIvaval) / nRate, nDecval) + dIvaval

                Case 4
                  dImpsca(i) = ArrDbl((dTotfat - dIva - dSpese) / nRate, nDec) + dIva + dSpese
                  dImpscaval(i) = ArrDbl((dTotfatval - dIvaval - dSpeseval) / nRate, nDecval) + dIvaval + dSpeseval

              End Select
              dGiaass = dGiaass + dImpsca(i)
              dGiaassval = dGiaassval + dImpscaval(i)
              dGiaass1 = dGiaass
              dGiass1val = dGiaassval
            Else
              dImpsca(i) = ArrDbl((dTotfat - dGiaass1) / (nRate - 1), nDec)
              dImpscaval(i) = ArrDbl((dTotfatval - dGiass1val) / (nRate - 1), nDecval)
              dGiaass = dGiaass + dImpsca(i)
              dGiaassval = dGiaassval + dImpscaval(i)
            End If
          End If
        Next
      Else ' in caso condiz particolari = "S"
        dValresiduo = dTotfat
        dValresiduoval = dTotfatval
        For i = 1 To nRate
          nTiporata(i) = nTipp(i)
          Select Case strBase(i) ' calcola importi
            Case "T" 'tot doc.
              dImpsca(i) = ArrDbl(dTotfat * dQuota(i) / 100, nDec)
              dImpscaval(i) = ArrDbl(dTotfatval * dQuota(i) / 100, nDecval)
            Case "I" 'iva.
              dImpsca(i) = ArrDbl(dIva * dQuota(i) / 100, nDec)
              dImpscaval(i) = ArrDbl(dIvaval * dQuota(i) / 100, nDecval)
            Case "S" 'spese + iva .
              dImpsca(i) = ArrDbl((dIva + dSpese) * dQuota(i) / 100, nDec)
              dImpscaval(i) = ArrDbl((dIvaval + dSpeseval) * dQuota(i) / 100, nDecval)
            Case "M" 'merce.
              dImpsca(i) = ArrDbl((dTotfat - dIva - dSpese) * dQuota(i) / 100, nDec)
              dImpscaval(i) = ArrDbl((dTotfatval - dIvaval - dSpeseval) * dQuota(i) / 100, nDecval)
            Case "N" 'merce + spese.
              dImpsca(i) = ArrDbl((dTotfat - dIva) * dQuota(i) / 100, nDec)
              dImpscaval(i) = ArrDbl((dTotfatval - dIvaval) * dQuota(i) / 100, nDecval)
            Case "R" 'valore residuo
              dImpsca(i) = ArrDbl((dValresiduo) * dQuota(i) / 100, nDec)
              dImpscaval(i) = ArrDbl((dValresiduoval) * dQuota(i) / 100, nDecval)
          End Select
          dGiaass = dGiaass + dImpsca(i)
          dGiaassval = dGiaassval + dImpscaval(i)
          If strBase(i) <> "R" Then ' se non è residuo, calcola il valore residuo
            dValresiduo = dValresiduo - dImpsca(i)
            dValresiduoval = dValresiduoval - dImpscaval(i)
          End If
        Next i
        If Math.Abs(ArrDbl(dTotfat - dGiaass, nDec)) > 0.02 Then
          strErr = oApp.Tr(Me, 128073602372994978, "Calcolo scadenze: Attenzione! La differenza di inputazione alle rate (|" & ArrDbl(dTotfat - dGiaass, nDec) & "|) è superiore a 0,02 euro: potrebbe essere male inserita la struttura del pagamento nella tabella dei pagamenti (Codice |" & nCodpaga & "|); controllare le rate e le scadenze calcolate.")
        End If
        ' se ci sono differenze carica tutto su ultima rata
        If dGiaass <> dTotfat Then
          dImpsca(nRate) = dImpsca(nRate) + (dTotfat - dGiaass)
        End If
        If dGiaassval <> dTotfatval Then
          dImpscaval(nRate) = dImpscaval(nRate) + (dTotfatval - dGiaassval)
        End If
        ' adesso calcola le date di scadenza
        strMemdatprec = strDatprec ' salva data precedente
        For i = 1 To nRate
          nMesiper = NTSCInt(Fix(nGiorn(i) / 30))
          nGiorniper = nGiorn(i) - nMesiper * 30
          strDatsca(i) = DateAdd("m", nMesiper, strDatprec).ToShortDateString
          If EstFineMese(strDatprec) Then strDatsca(i) = FineMese(strDatsca(i))
          ' qui ridefinire in mesi/giorni l'espessione sotto da aumentare...
          strDatsca(i) = DateAdd("d", nGiorniper, strDatsca(i)).ToShortDateString
          ' se eccezione mesi allora (lavorare su var. di comodo non strDatsca!!)
          If DatePart("m", strDatsca(i)) = nMesees1 Or DatePart("m", strDatsca(i)) = nMesees2 Then
            strMemdatprec2 = strDatsca(i) ' salva la data prima ..
            strDatsca(i) = DateAdd("m", nMesf, FineMese(strDatsca(i))).ToShortDateString
            strDatsca(i) = DateAdd("d", nGiof, (strDatsca(i))).ToShortDateString ' modificato
            If strShiftm = "S" Then '30/12/2002
              'se shifta, allore, sposta anche strdatprec di un mese
              If EstFineMese(strDatprec) Then bErafinemese = True Else bErafinemese = False
              strDatprec = DateAdd("m", 1, strDatprec).ToShortDateString
              If bErafinemese Then
                strDatprec = FineMese(strDatprec)
              End If
              If EstFineMese(strMemdatprec2) Then ' se era fine mese, allora
                'se  indicato un fine mese 30/60/...
                If nGiof = 0 And nMesf > 0 Then strDatsca(i) = FineMese(strDatsca(i))
              End If
            Else
              'nessuno shift, porta comunque a fine mese
              If EstFineMese(strMemdatprec2) Then
                'se  indicato un fine mese 30/60/...
                If nGiof = 0 And nMesf > 0 Then strDatsca(i) = FineMese(strDatsca(i))
              End If
            End If
          End If
          If i = 1 Then
            If nDecpaga = 5 Then   ' scadenza fine mese solo dalla 2.a rata in poi
              strDatprec = FineMese(strDatprec)
            End If
          End If
        Next i
      End If

      ' qui verificare se ci sono rate con importo zero (ecc. 1 o 2)e toglire da nrate ed eliminarle
RESTART:
      If nRate > 1 Then
        For i = 1 To nRate
          If dImpsca(i) = 0 And dImpscaval(i) = 0 Then
            For n = i To nRate - 1
              strDatsca(n) = strDatsca(n + 1)
              dImpsca(n) = dImpsca(n + 1)
              dImpscaval(n) = dImpscaval(n + 1)
              nTiporata(n) = nTiporata(n + 1)
            Next
            strDatsca(nRate) = ""
            dImpsca(nRate) = 0
            dImpscaval(nRate) = 0
            nTiporata(nRate) = 0
            nRate -= 1
            GoTo RESTART
          End If
        Next
      End If

      'se ho impostato la data massima, tutte le scadenze non devono superare tale data
      'per ora non raggruppo. al limite dovrei farlo per scadenze con la stessa tipologia di pagamento
      If strDtMax <> "" Then
        For i = 1 To nRate
          If NTSCDate(strDatsca(i)) > NTSCDate(strDtMax) Then strDatsca(i) = strDtMax
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
      For i = 1 To 60
        If strDatsca(i).Trim = "" Then Exit For
        bFineMese = False
        If strDatsca(i) = FineMese(strDatsca(i)) Then bFineMese = True
        If (nMesees1 <> 0) And NTSCDate(strDatsca(i)).Month = nMesees1 Then
          strDatsca(i) = NTSCStr(DateAdd(DateInterval.Month, 1, NTSCDate(strDatsca(i))))
          If bFineMese = True Then strDatsca(i) = FineMese(strDatsca(i))
        End If
        If (nMesees2 <> 0) And NTSCDate(strDatsca(i)).Month = nMesees2 Then
          strDatsca(i) = NTSCStr(DateAdd(DateInterval.Month, 1, NTSCDate(strDatsca(i))))
          If bFineMese = True Then strDatsca(i) = FineMese(strDatsca(i))
        End If
      Next i
      '--------------------------------------------------------------------------------------------------------------
      Return nRate

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function CalcSpeTrasp(ByVal strDitta As String, ByVal lConto As Integer, ByVal nCodivaspe As Integer, ByVal nCodvett As Integer, _
                              ByVal strCodport As String, ByVal dPesolordo As Decimal, ByVal dTotdoc As Decimal, _
                              ByVal dTotmerce As Decimal) As Decimal

    Dim dValore As Decimal
    Dim dContrassegno As Decimal
    Dim snaTmp As DataTable

    Try
      dValore = 0
      dContrassegno = 0
      CalcSpeTrasp = 0
      '--------------------------------------------------------------------------
      '--- Se non c' il codice vettore e la modalit di trasporto esce
      If nCodvett = 0 Or strCodport.Trim = "" Then Return 0

      '--------------------------------------------------------------------------
      '--- Se non c' il record corrispondente in SPETRASP esce
      snaTmp = oCldComm.LeggiSpeTrasp(strDitta, nCodvett, strCodport)
      If snaTmp.Rows.Count = 0 Then
        snaTmp.Clear()
        Return 0
      End If
      '--------------------------------------------------------------------------
      '--- Se nella prima riga delle Modalit di addebito non ci sono valori esce
      If NTSCDec(snaTmp.Rows(0)!st_base1) = 0 Then
        snaTmp.Clear()
        Return 0
      End If
      '--------------------------------------------------------------------------
      '--- Spese di Trasporto
      '--------------------------------------------------------------------------
      '--- Addebito x Valore/Valori
      If (snaTmp.Rows(0)!st_addclie.ToString = "V") And (snaTmp.Rows(0)!st_percval.ToString = "V") Then
        dValore = ValoreValori(snaTmp.Rows(0), dTotmerce)
      End If
      '--- Addebito x Valore/Percentuali
      If (snaTmp.Rows(0)!st_addclie.ToString = "V") And (snaTmp.Rows(0)!st_percval.ToString = "P") Then
        dValore = ValorePercentuali(snaTmp.Rows(0), dTotmerce)
      End If
      '--- Addebito x Peso/Valori
      If (snaTmp.Rows(0)!st_addclie.ToString = "P") And (snaTmp.Rows(0)!st_percval.ToString = "V") Then
        dValore = PesoValori(snaTmp.Rows(0), dPesolordo)
      End If
      '--- Addebito x Peso/Percentuali
      If (snaTmp.Rows(0)!st_addclie.ToString = "P") And (snaTmp.Rows(0)!st_percval.ToString = "P") Then
        dValore = PesoPercentuali(snaTmp.Rows(0), dPesolordo, dTotmerce)
      End If
      '--------------------------------------------------------------------------
      '--- Spese di Contrassegno
      '--------------------------------------------------------------------------
      '--- Addebito x Valore/Valori
      If snaTmp.Rows(0)!st_percvalcs.ToString = "V" Then
        dContrassegno = Valori(snaTmp.Rows(0), dTotdoc)
      End If
      '--- Addebito x Valore/Percentuali
      If snaTmp.Rows(0)!st_percvalcs.ToString = "P" Then
        dContrassegno = Percentuali(snaTmp.Rows(0), dTotdoc)
      End If
      '--------------------------------------------------------------------------
      snaTmp.Clear()

      Return ArrDbl((dValore + dContrassegno), oApp.NDecImporti)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function


  Public Overridable Function ValoreValori(ByVal snaTmp As DataRow, ByVal dTotmerce As Decimal) As Decimal
    Dim dTmp As Decimal

    Try
      dTmp = NTSCDec(snaTmp!st_fisso)
      If (NTSCDec(snaTmp!st_base1) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base1)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val1))
        GoTo ValoreValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base2) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base2)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val2))
        GoTo ValoreValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base3) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base3)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val3))
        GoTo ValoreValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base4) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base4)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val4))
        GoTo ValoreValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base5) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base5)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val5))
        GoTo ValoreValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base6) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base6)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val6))
        GoTo ValoreValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base7) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base7)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val7))
        GoTo ValoreValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base8) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base8)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val8))
        GoTo ValoreValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base8) > 0) And (dTotmerce > NTSCDec(snaTmp!st_base8)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val9))
        GoTo ValoreValoriEnd
      End If
ValoreValoriEnd:
      If dTmp < NTSCDec(snaTmp!st_minimo) Then dTmp = NTSCDec(snaTmp!st_minimo)
      ValoreValori = dTmp

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function Valori(ByVal snaTmp As DataRow, ByVal dTotdoc As Decimal) As Decimal
    Dim dTmp As Decimal
    Try
      dTmp = NTSCDec(snaTmp!st_fissocs)
      If (NTSCDec(snaTmp!st_basecs1) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs1)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_valcs1))
        GoTo ValoriEnd
      End If
      If (NTSCDec(snaTmp!st_basecs2) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs2)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_valcs2))
        GoTo ValoriEnd
      End If
      If (NTSCDec(snaTmp!st_basecs3) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs3)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_valcs3))
        GoTo ValoriEnd
      End If
      If (NTSCDec(snaTmp!st_basecs4) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs4)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_valcs4))
        GoTo ValoriEnd
      End If
      If (NTSCDec(snaTmp!st_basecs5) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs5)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_valcs5))
        GoTo ValoriEnd
      End If
      If (NTSCDec(snaTmp!st_basecs6) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs6)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_valcs6))
        GoTo ValoriEnd
      End If
      If (NTSCDec(snaTmp!st_basecs7) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs7)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_valcs7))
        GoTo ValoriEnd
      End If
      If (NTSCDec(snaTmp!st_basecs8) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs8)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_valcs8))
        GoTo ValoriEnd
      End If
      If (NTSCDec(snaTmp!st_basecs8) > 0) And (dTotdoc > NTSCDec(snaTmp!st_basecs8)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_valcs9))
        GoTo ValoriEnd
      End If
ValoriEnd:
      If dTmp < NTSCDec(snaTmp!st_minimocs) Then dTmp = NTSCDec(snaTmp!st_minimocs)
      Return dTmp
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function PesoValori(ByVal snaTmp As DataRow, ByVal dPesolordo As Decimal) As Decimal
    Dim dTmp As Decimal
    Try
      dTmp = NTSCDec(snaTmp!st_fisso)
      If (NTSCDec(snaTmp!st_base1) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base1)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val1))
        GoTo PesoValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base2) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base2)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val2))
        GoTo PesoValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base3) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base3)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val3))
        GoTo PesoValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base4) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base4)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val4))
        GoTo PesoValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base5) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base5)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val5))
        GoTo PesoValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base6) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base6)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val6))
        GoTo PesoValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base7) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base7)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val7))
        GoTo PesoValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base8) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base8)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val8))
        GoTo PesoValoriEnd
      End If
      If (NTSCDec(snaTmp!st_base8) > 0) And (dPesolordo > NTSCDec(snaTmp!st_base8)) Then
        dTmp = (dTmp + NTSCDec(snaTmp!st_val9))
        GoTo PesoValoriEnd
      End If
PesoValoriEnd:
      If dTmp < NTSCDec(snaTmp!st_minimo) Then dTmp = NTSCDec(snaTmp!st_minimo)

      Return dTmp
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function Percentuali(ByVal snaTmp As DataRow, ByVal dTotdoc As Decimal) As Decimal
    Dim dTmp As Decimal
    Try

      dTmp = NTSCDec(snaTmp!st_fissocs)
      If (NTSCDec(snaTmp!st_basecs1) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs1)) Then
        dTmp = dTmp + (dTotdoc * (NTSCDec(snaTmp!st_valcs1) / 100))
        GoTo PercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_basecs2) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs2)) Then
        dTmp = dTmp + (dTotdoc * (NTSCDec(snaTmp!st_valcs2) / 100))
        GoTo PercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_basecs3) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs3)) Then
        dTmp = dTmp + (dTotdoc * (NTSCDec(snaTmp!st_valcs3) / 100))
        GoTo PercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_basecs4) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs4)) Then
        dTmp = dTmp + (dTotdoc * (NTSCDec(snaTmp!st_valcs4) / 100))
        GoTo PercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_basecs5) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs5)) Then
        dTmp = dTmp + (dTotdoc * (NTSCDec(snaTmp!st_valcs5) / 100))
        GoTo PercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_basecs6) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs6)) Then
        dTmp = dTmp + (dTotdoc * (NTSCDec(snaTmp!st_valcs6) / 100))
        GoTo PercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_basecs7) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs7)) Then
        dTmp = dTmp + (dTotdoc * (NTSCDec(snaTmp!st_valcs7) / 100))
        GoTo PercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_basecs8) > 0) And (dTotdoc <= NTSCDec(snaTmp!st_basecs8)) Then
        dTmp = dTmp + (dTotdoc * (NTSCDec(snaTmp!st_valcs8) / 100))
        GoTo PercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_basecs8) > 0) And (dTotdoc > NTSCDec(snaTmp!st_basecs8)) Then
        dTmp = dTmp + (dTotdoc * (NTSCDec(snaTmp!st_valcs9) / 100))
        GoTo PercentualiEnd
      End If
PercentualiEnd:
      If dTmp < NTSCDec(snaTmp!st_minimocs) Then dTmp = NTSCDec(snaTmp!st_minimocs)
      Return dTmp

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ValorePercentuali(ByVal snaTmp As DataRow, ByVal dTotmerce As Decimal) As Decimal
    Dim dTmp As Decimal
    Try
      dTmp = NTSCDec(snaTmp!st_fisso)
      If (NTSCDec(snaTmp!st_base1) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base1)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val1) / 100)
        GoTo ValorePercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base2) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base2)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val2) / 100)
        GoTo ValorePercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base3) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base3)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val3) / 100)
        GoTo ValorePercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base4) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base4)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val4) / 100)
        GoTo ValorePercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base5) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base5)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val5) / 100)
        GoTo ValorePercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base6) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base6)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val6) / 100)
        GoTo ValorePercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base7) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base7)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val7) / 100)
        GoTo ValorePercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base8) > 0) And (dTotmerce <= NTSCDec(snaTmp!st_base8)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val8) / 100)
        GoTo ValorePercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base8) > 0) And (dTotmerce > NTSCDec(snaTmp!st_base8)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val9) / 100)
        GoTo ValorePercentualiEnd
      End If
ValorePercentualiEnd:
      If dTmp < NTSCDec(snaTmp!st_minimo) Then dTmp = NTSCDec(snaTmp!st_minimo)
      Return dTmp
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function PesoPercentuali(ByVal snaTmp As DataRow, ByVal dPesolordo As Decimal, ByVal dTotmerce As Decimal) As Decimal
    Dim dTmp As Decimal
    Try

      dTmp = NTSCDec(snaTmp!st_fisso)
      If (NTSCDec(snaTmp!st_base1) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base1)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val1) / 100)
        GoTo PesoPercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base2) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base2)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val2) / 100)
        GoTo PesoPercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base3) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base3)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val3) / 100)
        GoTo PesoPercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base4) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base4)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val4) / 100)
        GoTo PesoPercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base5) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base5)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val5) / 100)
        GoTo PesoPercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base6) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base6)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val6) / 100)
        GoTo PesoPercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base7) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base7)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val7) / 100)
        GoTo PesoPercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base8) > 0) And (dPesolordo <= NTSCDec(snaTmp!st_base8)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val8) / 100)
        GoTo PesoPercentualiEnd
      End If
      If (NTSCDec(snaTmp!st_base8) > 0) And (dPesolordo > NTSCDec(snaTmp!st_base8)) Then
        dTmp = dTmp + (dTotmerce * NTSCDec(snaTmp!st_val9) / 100)
        GoTo PesoPercentualiEnd
      End If
PesoPercentualiEnd:
      If dTmp < NTSCDec(snaTmp!st_minimo) Then dTmp = NTSCDec(snaTmp!st_minimo)
      Return dTmp

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function CalcolaDataValuta(ByVal strDitta As String, ByRef nCodbanc As Integer, _
                                                ByVal lContobanca As Integer, ByVal nCodcauc As Integer, _
                                                ByVal strDatreg As String, ByRef bNontrovato As Boolean) As String
    Dim dttTmp As New DataTable
    Dim nGgvalu As Integer = 0
    Dim strAvaind As String = ""
    Dim strValfiss As String = ""
    Dim dtDataout As DateTime

    Dim lNumber As Integer = 0
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim k As Integer = 0
    Dim dtTemp As DateTime
    Try

      bNontrovato = True
      If nCodbanc = 0 And lContobanca = 0 Then Return strDatreg

      If nCodbanc = 0 And lContobanca > 0 Then
        If Not oCldComm.GetBancaFromConto(strDitta, lContobanca, nCodbanc) Then
          Return strDatreg
        End If
      End If

      ' adesso cerca condban
      If Not oCldComm.GetCondban(strDitta, nCodbanc, nCodcauc, dttTmp) Then
        Return strDatreg
      End If
      If dttTmp.Rows.Count = 0 Then
        dttTmp.Clear()
        Return strDatreg
      End If
      strAvaind = dttTmp.Rows(0)!cb_avaind.ToString
      strValfiss = dttTmp.Rows(0)!cb_valfiss.ToString
      nGgvalu = NTSCInt(dttTmp.Rows(0)!cb_ggvalu)
      dttTmp.Clear()

      If strAvaind = "-" Then nGgvalu = nGgvalu * -1
      ' adesso calcola la data
      If strValfiss = "S" Then
        dtDataout = DateAdd("d", nGgvalu, NTSCDate(strDatreg))
        Return dtDataout.ToShortDateString
      End If

      ' non è data fissa, pertanto scarta sabati e domeniche o solo domeniche ..
      If strValfiss = "N" Then ' non lavorativi
        lNumber = nGgvalu
        j = Math.Sign(lNumber)
        dtTemp = NTSCDate(strDatreg)
        For i = 1 To Math.Abs(lNumber)
          dtTemp = DateAdd("d", j, dtTemp)
          k = Weekday(dtTemp)
          If k = vbSunday Or k = vbSaturday Then
            dtTemp = DateAdd("d", j, dtTemp)
            k = Weekday(dtTemp)
            If k = vbSunday Or k = vbSaturday Then
              dtTemp = DateAdd("d", j, dtTemp)
            End If
          End If
        Next i
        Return dtTemp.ToShortDateString
      End If

      If strValfiss = "D" Then ' festivi
        lNumber = nGgvalu
        j = Math.Sign(lNumber)
        dtTemp = NTSCDate(strDatreg)
        For i = 1 To Math.Abs(lNumber)
          dtTemp = DateAdd("d", j, dtTemp)
          k = Weekday(dtTemp)
          If k = vbSunday Then
            dtTemp = DateAdd("d", j, dtTemp)
            k = Weekday(dtTemp)
            If k = vbSunday Then
              dtTemp = DateAdd("d", j, dtTemp)
            End If
          End If
        Next i
        Return dtTemp.ToShortDateString
      End If

      Return strDatreg

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return strDatreg
    End Try
  End Function

  Public Overridable Function ConvImpEur(ByVal bPrzun As Boolean, ByVal dImportoin As Decimal, ByVal nCodVal As Integer, ByVal dtDataValidita As Date, ByRef dCambiout As Decimal, Optional ByVal bCancel As Boolean = False) As Decimal
    Try
      Return oCldComm.ConvImpEur(False, dImportoin, nCodVal, dtDataValidita, dCambiout, bCancel)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return 0
    End Try
  End Function

#Region "Cancellaz registraz. contabile"
  Public Overridable Function TestPreCancellaRegistrazione(ByVal strDitta As String, ByVal strDatreg As String, _
                                                           ByVal lNumreg As Integer, ByVal strPnint As String, _
                                                           ByVal bTestAncheLGRI As Boolean, ByRef strError As String) As Boolean
    Dim dttScad As New DataTable
    Try
      Return TestPreCancellaRegistrazioneEx(strDitta, strDatreg, lNumreg, strPnint, bTestAncheLGRI, strError, dttScad)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttScad.Clear()
    End Try
  End Function
  Public Overridable Function TestPreCancellaRegistrazioneEx(ByVal strDitta As String, ByVal strDatreg As String, _
                                                             ByVal lNumreg As Integer, ByVal strPnint As String, _
                                                             ByVal bTestAncheLGRI As Boolean, ByRef strError As String, _
                                                             ByRef dttScad As DataTable) As Boolean
    Try
      '---------------------------
      'controllo se posso cancellare la registrazione: non posso farlo se è collegata a 
      'ritenute/professionisti/cespiti/project management/beni usati/...
      strError = ""

      If bTestAncheLGRI Then
        If oCldComm.TestPreCancellaRegInLG(strDitta, strDatreg, lNumreg, strPnint) Then
          strError = oApp.Tr(Me, 128256014361644000, "La registrazione di Prima Nota del |" & NTSCDate(strDatreg).ToShortDateString & "| numero |" & CStr(lNumreg) & "|." & vbCrLf & _
              "E' stata stampata sul libro giornale. Eliminazione registrazione non effettuata.")
          Return False
        End If
        If oCldComm.TestPreCancellaRegInRI(strDitta, strDatreg, lNumreg, strPnint) Then
          strError = oApp.Tr(Me, 128256014378804000, "La registrazione di Prima Nota del |" & NTSCDate(strDatreg).ToShortDateString & "| numero |" & CStr(lNumreg) & "|." & vbCrLf & _
              "E' stata stampata sul registro IVA. Eliminazione registrazione non effettuata.")
          Return False
        End If
      End If

      If oCldComm.TestPreCancellaRegInRitenut(strDitta, strDatreg, lNumreg) Then
        strError = ScriviLog("RITENUT", strDatreg, lNumreg)
        Return False
      End If

      If oCldComm.TestPreCancellaRegInPagcomp(strDitta, strDatreg, lNumreg) Then
        strError = ScriviLog("PAGCOMP", strDatreg, lNumreg)
        Return False
      End If

      If oCldComm.TestPreCancellaRegInMovcesp(strDitta, strDatreg, lNumreg) Then
        strError = ScriviLog("MOVCESP", strDatreg, lNumreg)
        Return False
      End If

      If oCldComm.TestPreCancellaRegInPrfatt(strDitta, strDatreg, lNumreg) Then
        strError = ScriviLog("PRFATT", strDatreg, lNumreg)
        Return False
      End If

      If oCldComm.TestPreCancellaRegInPrincpag(strDitta, strDatreg, lNumreg) Then
        strError = ScriviLog("PRINCPAG", strDatreg, lNumreg)
        Return False
      End If

      If oCldComm.TestPreCancellaRegInMovusat(strDitta, strDatreg, lNumreg) Then
        strError = ScriviLog("MOVUSAT", strDatreg, lNumreg)
        Return False
      End If

      If oCldComm.TestPreCancellaRegInTasks(strDitta, strDatreg, lNumreg) Then
        strError = ScriviLog("PRIANA", strDatreg, lNumreg)
        Return False
      End If

      If oCldComm.TestPreCancellaRegInPriana2(strDitta, strDatreg, lNumreg) Then
        strError = ScriviLog("PRIANA2", strDatreg, lNumreg)
        Return False
      End If

      If oCldComm.TestPreCancellaRegInAbpd(strDitta, strDatreg, lNumreg) Then
        strError = ScriviLog("ABPD", strDatreg, lNumreg)
        Return False
      End If

      If oCldComm.TestPreCancellaRegInScaden(strDitta, strDatreg, lNumreg, dttScad) Then
        strError = ScriviLog("SCAD", strDatreg, lNumreg)
        Return False
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ScriviLog(ByVal strStringa As String, ByVal strDatreg As String, ByVal lNumreg As Integer) As String
    Dim strTmp As String = ""
    Dim strMsg As String = ""
    Try

      Select Case strStringa
        Case "RITENUT" : strTmp = oApp.Tr(Me, 128177379117119071, "Compensi professionisti")
        Case "PAGCOMP" : strTmp = oApp.Tr(Me, 128177379419468626, "Pagamenti compensi professionisti")
        Case "PRFATT" : strTmp = oApp.Tr(Me, 128177379403687073, "Fatture professionisti")
        Case "PRINCPAG" : strTmp = oApp.Tr(Me, 128177379382436665, "Incassi/pagamenti professionisti")
        Case "MOVCESP" : strTmp = oApp.Tr(Me, 128177379361967522, "Movimentazioni cespiti")
        Case "MOVUSAT" : strTmp = oApp.Tr(Me, 128177379344467186, "Movimenti beni usati")
        Case "PRIANA" : strTmp = oApp.Tr(Me, 128177379320091718, "Movimenti di contabilità analitica agganciati tasks del Project Management")
        Case "PRIANA2" : strTmp = oApp.Tr(Me, 129235901966093750, "Movimenti di contabilità analitica con data registrazione non superiore alla data ultimo aggiornamento CA indicata in anagrafica ditta")
        Case "ABPD" : strTmp = oApp.Tr(Me, 129628022402050781, "Incassi/pagamenti differiti  non ancora scaduti")
        Case "SCAD" : strTmp = oApp.Tr(Me, 130415144141303745, "Scadenze saldate in data uguale o successiva a quella della registrazione")
      End Select

      strMsg = oApp.Tr(Me, 128177379766194033, "Esistono dati in '|" & strTmp & "|'" & vbCrLf & _
    "relativi alla registrazione di Prima Nota del |" & NTSCDate(strDatreg).ToShortDateString & "| numero |" & CStr(lNumreg) & "|." & vbCrLf & _
    "Eliminazione registrazione non effettuata.")

      Return strMsg
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    End Try

  End Function

  Public Overridable Function CancellaRegistrazione(ByVal strDitta As String, ByVal strDatreg As String, _
                                                    ByVal lNumreg As Integer, ByVal strPnInt As String) As Boolean
    Dim bModDII As Boolean = False
    Try
      If CBool(ModuliSupDittaDitt(strDitta) And bsModSupDII) Then
        bModDII = True
      End If
      Return oCldComm.CancellaRegistrazione(strDitta, strDatreg, lNumreg, strPnInt, bModDII)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "Generazione files ABI/RID/MAV/BONIFICI per bncgauto, bncgdist, ..."
  Public Overridable Function GenAbiBonifRiba(ByVal strDitta As String, ByVal strOperat As String, ByVal strDtdist As String, _
                                              ByVal strDtvalbon As String, ByRef strFile As String, _
                                              ByVal strTipoScadenze As String, ByVal strTippaga As String, _
                                              ByVal nDistAnno As Integer, ByVal lDistNum As Integer, _
                                              ByVal strPnint As String, ByVal bPrefissaFileAbiConDitta As Boolean, _
                                              ByVal bOldPrefixRiba As Boolean, ByVal bOldPrefixBonif As Boolean, _
                                              ByVal bRaggruppaBonificiForn As Boolean, ByVal bStampaRiferimentiFatture As Boolean, _
                                              ByVal bDataScadInDataValBenef As Boolean, _
                                              ByVal strPathFileAbi As String, ByRef strErr As String) As Boolean
    '-------------------------------
    'generazione file ABI
    Dim dttAnaz As New DataTable
    Dim dttTabbanc As New DataTable
    Dim dttScaden As New DataTable
    Dim bRes As Boolean = False
    Try

      If Not IsDate(strDtdist) Then
        strErr += vbCrLf & oApp.Tr(Me, 128238680140819626, "Data distinta obbligatoria per la generazione dei file ABI")
        Return False
      End If

      oCldComm.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttAnaz)


      '------------------------------------
      'l'opzione di raggruppamento vale solo per i bonifici a fornitori
      'non per le riba emesse
      If strTippaga = "2" And strTipoScadenze = "A" Then
        bRaggruppaBonificiForn = False
      End If

      '------------------------------------
      'ottengo i dati
      If bRaggruppaBonificiForn Then
        ' qui invece raggruppa per fornitore, scadenza, banca
        If Not oCldComm.GetDataScadenDistinta(strDitta, strTippaga, nDistAnno, lDistNum, strPnint, True, dttScaden) Then
          Return False
        End If
      Else
        If Not oCldComm.GetDataScadenDistinta(strDitta, strTippaga, nDistAnno, lDistNum, strPnint, False, dttScaden) Then
          Return False
        End If
      End If
      If dttScaden.Rows.Count = 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 128939606128900759, "Nessuna scadenza autorizzata")
        Return False
      Else
        oCldComm.ValCodiceDb(dttScaden.Rows(0)!sc_codbanc.ToString, strDitta, "TABBANC", "N", "", dttTabbanc)
      End If

      If strTippaga = "2" And strTipoScadenze = "A" Then
        'Genera RIBA-ABI
        bRes = GenAbiRiba(strDitta, strOperat, strDtdist, strFile, strTipoScadenze, strTippaga, nDistAnno, lDistNum, strPnint, bPrefissaFileAbiConDitta, bOldPrefixRiba, bRaggruppaBonificiForn, dttAnaz, dttTabbanc, dttScaden, bStampaRiferimentiFatture, strPathFileAbi, strErr)
      Else
        If (strTippaga = "3" Or strTippaga = "5") And strTipoScadenze = "P" Then
          'Genera BONIFICI-ABI
          bRes = GenAbiBonif(strDitta, strOperat, strDtdist, strDtvalbon, strFile, strTipoScadenze, strTippaga, nDistAnno, lDistNum, strPnint, bPrefissaFileAbiConDitta, bOldPrefixBonif, bRaggruppaBonificiForn, dttAnaz, dttTabbanc, dttScaden, strPathFileAbi, bDataScadInDataValBenef, strErr)
        Else
          strErr += vbCrLf & oApp.Tr(Me, 128238681550372169, "Generazione file ABI non prevista per scadenze diverse da (PASSIVE con tipo pagamento 'Rim.Diretta' o 'Accr.Bancario')")
          Return False
        End If
      End If

      Return bRes
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttScaden.Clear()
      dttAnaz.Clear()
      dttTabbanc.Clear()
    End Try
  End Function

  Public Overridable Function GenAbiRiba(ByVal strDitta As String, ByVal strOperat As String, ByVal strDtdist As String, ByRef strNomeFile As String, _
                                          ByVal strTipoScadenze As String, ByVal strTippaga As String, _
                                          ByVal nDistAnno As Integer, ByVal lDistNum As Integer, _
                                          ByVal strPnint As String, ByVal bPrefissaFileAbiConDitta As Boolean, _
                                          ByVal bOldPrefixRiba As Boolean, ByVal bRaggruppaBonificiForn As Boolean, _
                                          ByRef dttAnaz As DataTable, ByRef dttTabbanc As DataTable, ByRef dttScaden As DataTable, _
                                          ByVal bStampaRiferimentiFatture As Boolean, ByVal strPathFileAbi As String, _
                                          ByRef strErr As String) As Boolean
    '-------------------------------
    'generazione file ABI RIBA
    Dim dttControp As New DataTable
    Dim dttDestdiv As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strNomeFile83 As String = ""
    Dim strIdValuta As String = ""
    Dim strPICFCreditore As String = ""
    Dim evt As NTSEventArgs = Nothing
    Dim w1 As StreamWriter = Nothing
    Dim strPR As String = ""
    Dim strNomeSup As String = ""
    Dim lProgrScad As Integer = 0     'Contatore N° RIBA generata
    Dim lRigheTot As Integer = 0      'Contatore righe sritte
    Dim dTotScad As Decimal = 0       'Progressivo valore scadenze
    Dim dImporto As Decimal = 0

    Dim strCINDebitore As String = ""
    Dim strCCDebitore As String = ""
    Dim strDescr1 As String = ""
    Dim strDescr2 As String = ""
    Dim strIndir As String = ""
    Dim strCap As String = ""
    Dim strCitta As String = ""
    Dim strProv As String = ""
    Dim strStato As String = ""
    Dim strPICF As String = ""
    Dim strExt As String = ".asc"
    Dim strRiapriPartiteRaggPartita As String = ""

    Dim bFileCBInumdoc9Char As Boolean = CBool(oCldComm.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "FileCBInumdoc9Char", "0", " ", "0"))
    Dim nNumChar As Integer = 5
    Dim strRifEstConto As String = "RIF.ESTR.CONTO N. "
    Dim strRifNSFatt As String = "RIF. NS. FATT. N. "
    Dim bFileRibaCodFisElsePiva As Boolean = CBool(oCldComm.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "FileRibaCodFisElsePiva", "0", " ", "0"))
    Dim bAccodaCIGCUPPinDescr As Boolean = False  'see true nella descrzione accoda i primi 40 caratteri di sc_descr, ovvero l'eventuale riferimento CIG e/o CUP indicato manualmente sulla scadenza (riferimenti pe rchi lavora con gli enti pubblici)
    Dim strTmp As String = ""
    Dim strCsbf As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Opzione di registro, globale, OPZIONI/FileCBInumdoc9Char (-1/0 <-- default)
      '--- Se attiva, il campo relativo al numero documento viene formattato con 9 caratteri anziché con 5 (standard)
      '--- e le descrizioni di riga vengono diminuite di 4 caratteri
      '--------------------------------------------------------------------------------------------------------------
      If bFileCBInumdoc9Char = True Then
        nNumChar = 9
        strRifEstConto = "RF.ES.CONTO N "
        strRifNSFatt = "RIF.NS.FAT.N. "
      End If
      '--------------------------------------------------------------------------------------------------------------
      strRiapriPartiteRaggPartita = oCldComm.GetSettingBusDitt(strDitta, "BSCGINSO", "OPZIONI", ".", "RiapriPartiteRaggPartita", "X", " ", "X")
      '--------------------------------------------------------------------------------------------------------------
      'nome del file
      strExt = oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "FileABICBIEstensioneFile", ".asc", " ", ".asc")
      bAccodaCIGCUPPinDescr = CBool(oCldComm.GetSettingBusDitt(strDitta, "BSCGDIST", "OPZIONI", ".", "AccodaCIGCUPPinDescr", "0", " ", "0"))
      If bPrefissaFileAbiConDitta Then
        strNomeFile83 = strDitta & "_Abi" & Format(lDistNum, "00000") & strExt
      Else
        strNomeFile83 = "Abi" & Format(lDistNum, "00000") & strExt
      End If
      strNomeFile = strPathFileAbi & strNomeFile83

      '----------------------
      'valuta
      If oApp.ValutaCorrente = "EUR" Then
        Select Case nValuta
          Case 0 : strIdValuta = "E"
          Case 100 : strIdValuta = "I"
          Case Else
            strErr += vbCrLf & oApp.Tr(Me, 128939614857617569, _
                "Codice valuta non valido per generazione file Abi." & vbCrLf & _
                "Codici valuta consentiti: '0' o '100'.")
            Return False
        End Select
      Else
        strErr += vbCrLf & oApp.Tr(Me, 128939614921994629, _
                "Codice valuta non valido per generazione file Abi." & vbCrLf & _
                "Codici valuta consentiti: '0' o '100'.")
        Return False
      End If

      '----------------------
      'P.IVA ditta
      strPICFCreditore = ""
      If NTSCStr(dttAnaz.Rows(0)!tb_azpiva) <> "" Then
        strPICFCreditore = NTSCStr(dttAnaz.Rows(0)!tb_azpiva)
      Else
        strPICFCreditore = NTSCStr(dttAnaz.Rows(0)!tb_azcodf)
      End If

      '----------------------
      'Controlla se già esiste il file
      If System.IO.File.Exists(strNomeFile) Then
        'evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128238690194733441, "Esiste già un file chiamato |" & UCase$(strNomeFile) & "|. Sovrascriverlo?"))
        'ThrowRemoteEvent(evt)
        'If evt.RetValue = "NO" Then Return False
        System.IO.File.Delete(strNomeFile)
      End If

      w1 = New StreamWriter(strNomeFile, False)
      '----------------------
      'Scrive la testata
      If bOldPrefixRiba Then
        strPR = " RB"
      Else
        strPR = " IB"
      End If
      strNomeSup = Left("Dist. " & Format(lDistNum, "000000000") & "-" & nDistAnno.ToString, 20)
      w1.WriteLine(strPR & Left(NTSCStr(dttTabbanc.Rows(0)!tb_rifriba) & "     ", 5) & _
                   Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_abi), 5) & NTSCDate(strDtdist).ToString("ddMMyy") & _
                   Left(strNomeSup & Space(20), 20) & "".PadLeft(6) & "".PadLeft(68) & strIdValuta & "".PadLeft(6))
      lRigheTot = 1
      lProgrScad = 1
      dTotScad = 0

      '----------------------
      'scrivo il corpo
      dtrT = dttScaden.Select()
      For i = 0 To dtrT.Length - 1
        With dtrT(i)

          If NTSCInt(!sc_abi) = 0 Or NTSCInt(!sc_cab) = 0 Then
            strErr += vbCrLf & oApp.Tr(Me, 128939605564976464, _
    "Attenzione! Codice ABI e/o CAB mancante per la scadenza al |" & NTSCDate(!sc_datsca).ToShortDateString & _
    "| di conto/controp. |" & !sc_conto.ToString & "/" & !sc_controp.ToString & "|.")
          End If
          '------------------------------------------------------------------------------
          'Rileva C/C e CIN del beneficiario
          strCINDebitore = " "
          strCCDebitore = UCase(NTSCStr(!sc_numcc))
          'Controlla che in sc_numcc sia indicato: 000000000000#A
          '(ossia 12 caratteri del c/c, + #, + carattere CIN)
          If InStr(strCCDebitore, "#") > 0 Then
            'rileva il carattere CIN
            If InStr(strCCDebitore, "#") < Len(strCCDebitore) Then
              strCINDebitore = Mid(strCCDebitore, InStr(strCCDebitore, "#") + 1, 1)
            End If
            'rileva il c/c
            If InStr(strCCDebitore, "#") > 1 Then
              strCCDebitore = Left(strCCDebitore, InStr(strCCDebitore, "#") - 1)
            Else
              strCCDebitore = ""
            End If
          End If

          Select Case nValuta
            Case 0 : dImporto = (NTSCDec(!sc_importoda) * 100)
            Case 100 : dImporto = NTSCDec(!sc_impvalda)
          End Select
          ' decod. conto per tirarae su il cod. fisc. o partita iva
          If (!sc_tippaga.ToString = "1" Or !sc_tippaga.ToString = "2") And !sc_darave.ToString = "D" And NTSCInt(!sc_controp) <> 0 Then
            'caso di riba clienti con emissione effetti già eseguita
            oCldComm.ValCodiceDb(!sc_controp.ToString, strDitta, "ANAGRA", "N", "", dttControp)
          Else
            'riba fornitori o riba clienti senza emissione effetti 
            '(ovvero giro manuale di emissione effetti senza chisura cliente: emetto gli effetti,
            'in bus non risulta nulla di questa emissione, all'incasso si fa la reg. a mano 'banca a cliente')
            oCldComm.ValCodiceDb(!sc_conto.ToString, strDitta, "ANAGRA", "N", "", dttControp)
          End If
          If dttControp.Rows.Count > 0 Then
            strDescr1 = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_descr1))
            strDescr2 = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_descr2))
            strIndir = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_indir))
            strCap = NTSCStr(dttControp.Rows(0)!an_cap)
            strCitta = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_citta))
            strProv = NTSCStr(dttControp.Rows(0)!an_prov)
            strStato = NTSCStr(dttControp.Rows(0)!an_stato)
            If bFileRibaCodFisElsePiva = False Then
              'caso standard: privilegia la PIVA
              If NTSCStr(dttControp.Rows(0)!an_pariva) <> "" Then
                strPICF = NTSCStr(dttControp.Rows(0)!an_pariva)
              Else
                strPICF = NTSCStr(dttControp.Rows(0)!an_codfis)
              End If
            Else
              'privilegio il COD FISC
              If NTSCStr(dttControp.Rows(0)!an_codfis) <> "" Then
                strPICF = NTSCStr(dttControp.Rows(0)!an_codfis)
              Else
                strPICF = NTSCStr(dttControp.Rows(0)!an_pariva)
              End If
            End If
            If NTSCInt(!sc_coddest) > 0 Then
              If (!sc_tippaga.ToString = "1" Or !sc_tippaga.ToString = "2") And !sc_darave.ToString = "D" Then
                oCldComm.ValCodiceDb(!sc_coddest.ToString, strDitta, "DESTDIV", "N", "", dttDestdiv, !sc_controp.ToString)
              Else
                oCldComm.ValCodiceDb(!sc_coddest.ToString, strDitta, "DESTDIV", "N", "", dttDestdiv, !sc_conto.ToString)
              End If
              If dttDestdiv.Rows.Count > 0 Then
                strDescr1 = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_nomdest))
                strDescr2 = " "
                strIndir = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_inddest))
                strCap = NTSCStr(dttDestdiv.Rows(0)!dd_capdest)
                strCitta = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_locdest))
                strProv = NTSCStr(dttDestdiv.Rows(0)!dd_prodest)
                strStato = " "
              End If
            End If
          Else
            strDescr1 = ""
            strDescr2 = ""
            strIndir = ""
            strCap = ""
            strCitta = ""
            strProv = ""
            strStato = ""
            strPICF = ""
          End If

          If NTSCInt(!sc_abi) = 0 Then
            strErr += vbCrLf & oApp.Tr(Me, 128238715135798820, "Attenzione: non è stato indicato il codice ABI nella scadenza relativa al conto/contropartita |" & !sc_conto.ToString & "/" & !sc_controp.ToString & "|, partita numero |" & !sc_numpar.ToString & "|, serie |'" & !sc_alfpar.ToString & "'|, anno |" & !sc_annpar.ToString & "|. Il file generato non sarà corretto.")
          End If
          If Trim(strPICF) = "" Then
            strErr += vbCrLf & oApp.Tr(Me, 128939604907767934, "Attenzione: non è stato indicato il codice fiscale/partita IVA sul conto/contropartita |" & !sc_conto.ToString & "/" & !sc_controp.ToString & "|, partita numero |" & !sc_numpar.ToString & "|, serie |'" & !sc_alfpar.ToString & "'|, anno |" & !sc_annpar.ToString & "|. Il file generato non sarà corretto.")
          End If
          If Trim(strPICFCreditore) = "" Then
            strErr += vbCrLf & oApp.Tr(Me, 128939604974332564, "Attenzione: non è stato indicato il codice fiscale/partita IVA in anagrafica ditta, relativa alla ditta corrente |" & strDitta & "| . Il file generato non sarà corretto.")
          End If
          '----------------------------------------------------------------------------------------------------------
          strCsbf = Right("".PadLeft(12, "0"c) & dttTabbanc.Rows(0)!tb_csbf.ToString, 12)
          If dttTabbanc.Rows(0)!tb_csbf.ToString.Trim = "" AndAlso _
            CBool(oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "FormattaCsbfFileRibaNull", "0", " ", "0")) = True Then
            strCsbf = "".PadLeft(12)
          End If
          '----------------------------------------------------------------------------------------------------------
          w1.WriteLine(" 14" & Format(lProgrScad, "0000000") & "".PadLeft(12) & NTSCDate(!sc_datsca).ToString("ddMMyy") & "30000" & Format(dImporto, "0000000000000") & "-" & Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_abi), 5) & Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_cab), 5) & strCsbf & Right(Format(!sc_abi, "00000"), 5) & Right(Format(!sc_cab, "00000"), 5) & Right$("".PadLeft(12, CChar("0")) & strCCDebitore, 12) & Left$(dttTabbanc.Rows(0)!tb_rifriba.ToString & "     ", 5) & "4" & Format(!sc_controp, "0000000000000000") & "".PadLeft(6) & strIdValuta)
          w1.WriteLine(" 20" & Format(lProgrScad, "0000000") & Left$(ConvStrFileAbi(dttAnaz.Rows(0)!tb_azrags1.ToString) & "".PadLeft(24), 24) & Left(ConvStrFileAbi(dttAnaz.Rows(0)!tb_azrags2.ToString) & "".PadLeft(24), 24) & Left(ConvStrFileAbi(dttAnaz.Rows(0)!tb_azindir.ToString) & "".PadLeft(24), 24) & Left(ConvStrFileAbi(dttAnaz.Rows(0)!tb_azcitta.ToString) & "".PadLeft(24), 24) & "".PadLeft(14))
          w1.WriteLine(" 30" & Format(lProgrScad, "0000000") & Left$(NTSCStr(strDescr1) & "".PadLeft(30), 30) & Left(NTSCStr(strDescr2) & "".PadLeft(30), 30) & Left$(NTSCStr(strPICF) & "".PadLeft(16), 16) & "".PadLeft(34))
          If Not IsNumeric(strCap) Then
            strErr += vbCrLf & oApp.Tr(Me, 129307660379297291, "Attenzione: il CAP relativo al Conto/controp. '|" & !sc_conto.ToString() & "/" & !sc_controp.ToString & "|' è sbagliato (non è un numero reale). Pertanto sarà generato il file con un CAP a zero.")
            strCap = "00000"
          Else
            strCap = strCap.PadLeft(5, "0"c).Substring(0, 5)
          End If
          w1.WriteLine(" 40" & Format(lProgrScad, "0000000") & Left$(NTSCStr(strIndir) & "".PadLeft(30), 30) & Format(Val(NTSCStr(strCap)), "00000") & Left$(NTSCStr(strCitta) & "".PadLeft(23), 23) & Left$(NTSCStr(strProv) & "".PadLeft(2), 2) & Left$(ConvStrFileAbi(NTSCStr(!sc_banc1)) & "".PadLeft(20), 20) & Left$(ConvStrFileAbi(NTSCStr(!sc_banc2)) & "".PadLeft(20), 20) & "".PadLeft(10))
          If (IIf(Trim(NTSCStr(!sc_alfdoc)) = "", " ", Left(!sc_alfdoc.ToString, 1))).ToString.ToUpper = strRiapriPartiteRaggPartita.ToUpper Then
            If bStampaRiferimentiFatture And bAccodaCIGCUPPinDescr = False Then
              w1.WriteLine(" 50" & Format(lProgrScad, "0000000") & Left(NTSCStr(!sc_descr) & Space(80), 80) & Space(10) & Left(NTSCStr(strPICFCreditore) & Space(16), 16) & Space(4))
            Else
              If NTSCStr(!sc_cup).Trim <> "" Or NTSCStr(!sc_cig).Trim <> "" Then
                strTmp = " " & oApp.Tr(Me, 130474020661944044, "CUP:") & NTSCStr(!sc_cup).Trim & _
                         " " & oApp.Tr(Me, 130474020682296458, "CIG:") & NTSCStr(!sc_cig).Trim
                strTmp = strTmp.PadRight(42).Substring(0, 42)
              ElseIf bAccodaCIGCUPPinDescr Then
                strTmp = " " & NTSCStr(!sc_descr).PadRight(41).Substring(0, 41)
              Else
                strTmp = "".PadLeft(42)
              End If
              strTmp = " 50" & Format(lProgrScad, "0000000") & strRifEstConto & Right$("".PadLeft(nNumChar) & CStr(!sc_numdoc), nNumChar) & "/X" & " DEL " & NTSCDate(!sc_datdoc).ToString("dd/MM/yy") & _
                       strTmp & _
                       "".PadLeft(10) & Left$(NTSCStr(strPICFCreditore) & "".PadLeft(16), 16) & "".PadLeft(4)
              w1.WriteLine(strTmp)
            End If
          Else
            If NTSCStr(!sc_cup).Trim <> "" Or NTSCStr(!sc_cig).Trim <> "" Then
              strTmp = " " & oApp.Tr(Me, 130474020748645217, "CUP:") & NTSCStr(!sc_cup).Trim & _
                       " " & oApp.Tr(Me, 130474020764109574, "CIG:") & NTSCStr(!sc_cig).Trim
              strTmp = strTmp.PadRight(42).Substring(0, 42)
            ElseIf bAccodaCIGCUPPinDescr Then
              strTmp = " " & NTSCStr(!sc_descr).PadRight(41).Substring(0, 41)
            Else
              strTmp = "".PadLeft(42)
            End If
            strTmp = " 50" & Format(lProgrScad, "0000000") & strRifNSFatt & Right$("".PadLeft(nNumChar) & CStr(!sc_numdoc), nNumChar) & IIf(Trim(NTSCStr(!sc_alfdoc)) = "", "  ", "/" & Left(NTSCStr(!sc_alfdoc), 1)).ToString & " DEL " & NTSCDate(!sc_datdoc).ToString("dd/MM/yy") & _
                     strTmp & _
                     "".PadLeft(10) & Left$(NTSCStr(strPICFCreditore) & "".PadLeft(16), 16) & "".PadLeft(4)
            w1.WriteLine(strTmp)
          End If
          w1.WriteLine(" 51" & Format(lProgrScad, "0000000") & Format(!sc_numeff, "0000000000") & Left(ConvStrFileAbi(dttAnaz.Rows(0)!tb_azrags1.ToString) & "".PadLeft(20), 20) & "".PadLeft(15) & "".PadLeft(16, CChar("0")) & "".PadLeft(49))
          w1.WriteLine(" 70" & Format(lProgrScad, "0000000") & "".PadLeft(110))
          dTotScad = dTotScad + dImporto
          lRigheTot = lRigheTot + 7
          lProgrScad = lProgrScad + 1

        End With
      Next

      '----------------------
      'Scrive il piede
      w1.WriteLine(" EF" & Left(NTSCStr(dttTabbanc.Rows(0)!tb_rifriba) & "     ", 5) & _
                   Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_abi), 5) & _
                   NTSCDate(strDtdist).ToString("ddMMyy") & Left(strNomeSup & Space(20), 20) & "".PadLeft(6) & _
                   Format(lProgrScad - 1, "0000000") & Format(dTotScad, "000000000000000") & "".PadLeft(15, CChar("0")) & _
                   Format(lRigheTot + 1, "0000000") & "".PadLeft(24) & strIdValuta & "".PadLeft(6))
      w1.Flush()
      w1.Close()

      '----------------------
      'se devo copio il file anche nella dir alternativa
      strExt = oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "FileABICBICopiaIn", "", " ", "").Trim
      If strExt <> "" Then
        Try
          File.Copy(strNomeFile, (strExt & "\" & strNomeFile83).Replace("\\", "\"), True)
          strNomeFile += vbCrLf & "e copiato in " & (strExt & "\" & strNomeFile83).Replace("\\", "\")
        Catch ex As Exception
          'ignoro l'errore
        End Try
      End If

      Return True
    Catch ex As Exception
      If Not w1 Is Nothing Then w1.Close()
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttScaden.Clear()
      dttAnaz.Clear()
      dttTabbanc.Clear()
    End Try
  End Function

  Public Overridable Function GenAbiBonif(ByVal strDitta As String, ByVal strOperat As String, ByVal strDtdist As String, _
                                          ByVal strDtvalbon As String, ByRef strNomeFile As String, _
                                          ByVal strTipoScadenze As String, ByVal strTippaga As String, _
                                          ByVal nDistAnno As Integer, ByVal lDistNum As Integer, _
                                          ByVal strPnint As String, ByVal bPrefissaFileAbiConDitta As Boolean, _
                                          ByVal bOldPrefixBonif As Boolean, ByVal bRaggruppaBonificiForn As Boolean, _
                                          ByRef dttAnaz As DataTable, ByRef dttTabbanc As DataTable, ByRef dttScaden As DataTable, _
                                          ByVal strPathFileAbi As String, ByVal bDataScadInDataValBenef As Boolean, _
                                          ByRef strErr As String) As Boolean
    '-------------------------------
    'generazione file bonifici italia
    Dim dttControp As New DataTable
    Dim dttDestdiv As New DataTable
    Dim dttTmp As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim nCausale As Integer = 0
    Dim i As Integer = 0
    Dim strNomeFile83 As String = ""
    Dim strIdValuta As String = ""
    Dim strPICFOrdin As String = ""
    Dim evt As NTSEventArgs = Nothing
    Dim w1 As StreamWriter = Nothing
    Dim strPR As String = ""
    Dim strNomeSup As String = ""
    Dim lProgrScad As Integer = 0     'Contatore N° RIBA generata
    Dim lRigheTot As Integer = 0      'Contatore righe sritte
    Dim dTotScad As Decimal = 0       'Progressivo valore scadenze
    Dim dImporto As Decimal = 0

    Dim strDescr1 As String = ""
    Dim strDescr2 As String = ""
    Dim strIndir As String = ""
    Dim strCap As String = ""
    Dim strCitta As String = ""
    Dim strProv As String = ""
    Dim strStato As String = ""
    Dim strPICF As String = ""

    'Ordinante
    Dim strIBANOrdinante As String
    Dim lAbi As Integer 'Ordinante
    Dim lCab As Integer 'Ordinante
    Dim strCC As String 'Ordinante
    Dim strRif As String 'Codice SIA
    'Beneficiario
    Dim strIBANBeneficiario As String
    Dim strCINBeneficiario As String
    Dim strCCBeneficiario As String
    Dim strExt As String = ".asc"
    Dim str1 As String = ""
    Dim str2 As String = ""
    Dim bAccodaCIGCUPPinDescr As Boolean = False
    Dim strTmp As String = ""

    Dim bFileCBInumdoc9Char As Boolean = CBool(oCldComm.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "FileCBInumdoc9Char", "0", " ", "0"))
    Dim nNumChar As Integer = 5
    Dim strRifVSFatt As String = "RIF. VS. FATT. N. "
    Dim evnt As NTSEventArgs = Nothing

    Try
      'chiedo se gestire bonifici con tracciato SEPA o ante SEPA
      evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 130220835937588402, "Generazione BONIFICO secondo il tracciato SEPA (SI) o con il vecchio tracciato (NO)?"))
      ThrowRemoteEvent(evnt)
      If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
        Return GenAbiBonifSepa(strDitta, strOperat, strDtdist, strDtvalbon, strNomeFile, strTipoScadenze, strTippaga, _
                               nDistAnno, lDistNum, strPnint, bPrefissaFileAbiConDitta, bRaggruppaBonificiForn, _
                               dttAnaz, dttTabbanc, dttScaden, strPathFileAbi, False, strErr)
      End If

      '--------------------------------------------------------------------------------------------------------------
      '--- Opzione di registro, globale, OPZIONI/FileCBInumdoc9Char (-1/0 <-- default)
      '--- Se attiva, il campo relativo al numero documento viene formattato con 9 caratteri anziché con 5 (standard)
      '--- e le descrizioni di riga vengono diminuite di 4 caratteri
      '--------------------------------------------------------------------------------------------------------------
      If bFileCBInumdoc9Char = True Then
        nNumChar = 9
        strRifVSFatt = "RIF.VS.FAT.N. "
      End If
      '--------------------------------------------------------------------------------------------------------------
      bAccodaCIGCUPPinDescr = CBool(ocldBase.GetSettingBus("BSCGDIST", "OPZIONI", ".", "AccodaCIGCUPPinDescr", "0", " ", "0"))
      '----------------------
      'nome del file
      strExt = oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "FileABICBIEstensioneFile", ".asc", " ", ".asc")
      If bPrefissaFileAbiConDitta Then
        strNomeFile83 = strDitta & "_Bon" & Format(lDistNum, "00000") & strExt
      Else
        strNomeFile83 = "Bon" & Format(lDistNum, "00000") & strExt
      End If
      strNomeFile = strPathFileAbi & strNomeFile83

      '----------------------
      'valuta
      If oApp.ValutaCorrente = "EUR" Then
        Select Case nValuta
          Case 0 : strIdValuta = "E"
          Case 100 : strIdValuta = "I"
          Case Else
            strErr += vbCrLf & oApp.Tr(Me, 130408994163370221, _
                "Codice valuta non valido per generazione file Bonifici." & vbCrLf & _
                "Codici valuta consentiti: '0' o '100'.")
            Return False
        End Select
      Else
        strErr += vbCrLf & oApp.Tr(Me, 130408994181925904, _
                "Codice valuta non valido per generazione file Bonifici." & vbCrLf & _
                "Codici valuta consentiti: '0' o '100'.")
        Return False
      End If

      lAbi = NTSCInt(dttTabbanc.Rows(0)!tb_abi)
      lCab = NTSCInt(dttTabbanc.Rows(0)!tb_cab)
      strRif = Trim(NTSCStr(dttTabbanc.Rows(0)!tb_rifriba)) 'SIA
      strCC = Trim(NTSCStr(dttTabbanc.Rows(0)!tb_csbf)) ' id. ns conto presso la banca
      'L'IBAN ordinante lo compone dai 4 campi: prefisso IBAN + CIN + ABI +CAB + NUM c/c
      If NTSCStr(dttTabbanc.Rows(0)!tb_prefiban) = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 128939607639574099, _
        "Non è indicato il 'prefisso IBAN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If Len(NTSCStr(dttTabbanc.Rows(0)!tb_prefiban)) <> 4 Then
        strErr += vbCrLf & oApp.Tr(Me, 128939607826767589, _
          "Il 'prefisso IBAN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "| deve essere di 4 caratteri")
        Return False
      End If
      If Left(NTSCStr(dttTabbanc.Rows(0)!tb_prefiban), 2) <> "IT" And Left(NTSCStr(dttTabbanc.Rows(0)!tb_prefiban), 2) <> "SM" Then
        strErr += vbCrLf & oApp.Tr(Me, 128939608200060784, _
          "Il 'prefisso IBAN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "| deve iniziare con i caratteri IT o SM.")
        Return False
      End If
      If NTSCStr(dttTabbanc.Rows(0)!tb_cin) = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 128939608329596179, _
          "Non è indicato il 'CIN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If lAbi = 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 128939608841956324, _
          "Non è indicato il cod. 'ABI banca' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If lCab = 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 128939608974304309, _
          "Non è indicato il cod. 'CAB filiale' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If strCC = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 128939609273532634, _
          "Non è indicato il 'N° C/C' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      strIBANOrdinante = NTSCStr(dttTabbanc.Rows(0)!tb_prefiban) & _
                         NTSCStr(dttTabbanc.Rows(0)!tb_cin) & _
                         Microsoft.VisualBasic.Right("00000" & lAbi, 5) & _
                         Microsoft.VisualBasic.Right("00000" & lCab, 5) & _
                         Microsoft.VisualBasic.Right("000000000000" & strCC, 12)

      '----------------------
      'P.IVA ditta
      strPICFOrdin = ""
      If NTSCStr(dttAnaz.Rows(0)!tb_azpiva) <> "" Then
        strPICFOrdin = NTSCStr(dttAnaz.Rows(0)!tb_azpiva)
      Else
        strPICFOrdin = NTSCStr(dttAnaz.Rows(0)!tb_azcodf)
      End If

      '----------------------
      'Controlla se già esiste il file
      If System.IO.File.Exists(strNomeFile) Then
        'evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128238690194733441, "Esiste già un file chiamato |" & UCase(strNomeFile) & "|. Sovrascriverlo?"))
        'ThrowRemoteEvent(evt)
        'If evt.RetValue = "NO" Then Return False
        System.IO.File.Delete(strNomeFile)
      End If

      w1 = New StreamWriter(strNomeFile, False)
      '----------------------
      'Scrive la testata
      If bOldPrefixBonif Then
        strPR = " HR"       'vecchio prefisso
      Else
        strPR = " PC"       'nuovo prefisso
      End If
      strNomeSup = Left("Dist. " & Format(lDistNum, "000000000") & "-" & nDistAnno.ToString, 20)

      w1.WriteLine(strPR & Left(strRif & "     ", 5) & _
                   Right("00000" & NTSCStr(lAbi), 5) & NTSCDate(strDtdist).ToString("ddMMyy") & _
                   Left(strNomeSup & Space(20), 20) & "".PadLeft(74) & strIdValuta & "".PadLeft(6))
      lRigheTot = 1
      lProgrScad = 1
      dTotScad = 0

      '----------------------
      'scrivo il corpo
      dtrT = dttScaden.Select()
      For i = 0 To dtrT.Length - 1
        With dtrT(i)
          '------------------------------------------------------------------------------
          'Rileva l'IBAN del beneficiario
          If NTSCInt(!sc_abi) = 0 Or NTSCInt(!sc_cab) = 0 Then
            strErr += vbCrLf & oApp.Tr(Me, 128939605464504499, _
    "Attenzione! Codice ABI e/o CAB mancante per la scadenza al |" & NTSCDate(!sc_datsca).ToShortDateString & _
    "| di conto/controp. |" & !sc_conto.ToString & "/" & !sc_controp.ToString & "|.")
          End If
          '------------------------------------------------------------------------------
          'Rileva C/C e CIN del beneficiario
          strCINBeneficiario = Trim(NTSCStr(!sc_cin))
          strCCBeneficiario = UCase(NTSCStr(!sc_numcc)).Trim

          If InStr(strCCBeneficiario, "#") > 0 Then 'per compatibilità
            If InStr(strCCBeneficiario, "#") < Len(strCCBeneficiario) Then
              'Se il CIN presente nel campo sc_cin non è valido lo prende da sc_numcc (per compatibilità all'indietro)
              If (Not strCINBeneficiario Like "[A-Z]") Or strCCBeneficiario = "" Then
                strCINBeneficiario = Mid(strCCBeneficiario, InStr(strCCBeneficiario, "#") + 1, 1)
              End If
            End If
            'toglie dal C/c in cin e il prefiban
            If InStr(strCCBeneficiario, "#") > 1 Then
              strCCBeneficiario = Left(strCCBeneficiario, InStr(strCCBeneficiario, "#") - 1)
            Else
              strCCBeneficiario = ""
            End If
          End If
          'Controlla correttezza c/c e CIN
          If strCCBeneficiario = "" Or Len(strCCBeneficiario) > 12 Then
            strErr += vbCrLf & oApp.Tr(Me, 128383214084408000, _
                "Attenzione! numero conto corrente non corretto |'" & NTSCStr(!sc_numcc) & _
                "'| per la scadenza al |" & NTSCStr(!sc_datsca) & "| di conto/controp. |" & _
                !sc_conto.ToString & "/" & !sc_controp.ToString & "|." & vbCrLf & vbCrLf & _
                "Non rispetta la convenzione adottata: max 12 caratteri per il num. conto corrente.")
          End If
          If (Not strCINBeneficiario Like "[A-Z]") Then
            strErr += vbCrLf & oApp.Tr(Me, 128383215354092000, _
                "Attenzione! il carattere di controllo CIN deve essere un carattere alfabetico A-Z per la " & _
                "scadenza al |" & NTSCStr(!sc_datsca) & "| di conto/controp. |" & _
                !sc_conto.ToString & "/" & !sc_controp.ToString & "|.")
          End If
          'Rileva il prefisso IBAN
          strIBANBeneficiario = Trim(NTSCStr(!sc_prefiban)).ToUpper
          If strIBANBeneficiario = "" Then
            strErr += vbCrLf & oApp.Tr(Me, 128383216118180000, _
                "Attenzione! Non è indicato il 'prefisso IBAN' per la scadenza al |" & NTSCStr(!sc_datsca) & _
                "| di conto/controp. |" & !sc_conto.ToString & "/" & !sc_controp.ToString & "|.")
          End If
          If Len(strIBANBeneficiario) <> 4 Then
            strErr += vbCrLf & oApp.Tr(Me, 128383216324724000, _
                "Attenzione! Il 'prefisso IBAN', per la scadenza al |" & NTSCStr(!sc_datsca) & _
                "| di conto/controp. |" & !sc_conto.ToString & "/" & !sc_controp.ToString & "| deve essere di 4 caratteri")
          End If
          If Left(strIBANBeneficiario, 2) <> "IT" And Left(strIBANBeneficiario, 2) <> "SM" Then
            strErr += vbCrLf & oApp.Tr(Me, 128383216438136000, _
                "Attenzione! Il 'prefisso IBAN', per la scadenza al |" & NTSCStr(!sc_datsca) & _
                "| di conto/controp. |" & !sc_conto.ToString & "/" & !sc_controp.ToString & "|, " & _
                "deve iniziare con i caratteri IT o SM.")
          End If
          strIBANBeneficiario = strIBANBeneficiario & strCINBeneficiario & Right("00000" & NTSCInt(!sc_abi), 5) & _
                                Right("00000" & NTSCInt(!sc_cab), 5) & Right("000000000000" & strCCBeneficiario, 12)

          Select Case nValuta
            Case 0 : dImporto = (NTSCDec(!sc_importoda) * 100)
            Case 100 : dImporto = NTSCDec(!sc_impvalda)
          End Select
          ' decod. conto per tirarae su il cod. fisc. o partita iva
          If (!sc_tippaga.ToString = "1" Or !sc_tippaga.ToString = "2") And !sc_darave.ToString = "D" Then
            oCldComm.ValCodiceDb(!sc_controp.ToString, strDitta, "ANAGRA", "N", "", dttControp)
          Else
            oCldComm.ValCodiceDb(!sc_conto.ToString, strDitta, "ANAGRA", "N", "", dttControp)
          End If
          If dttControp.Rows.Count > 0 Then
            strDescr1 = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_descr1))
            strDescr2 = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_descr2))
            strIndir = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_indir))
            strCap = NTSCStr(dttControp.Rows(0)!an_cap)
            strCitta = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_citta))
            strProv = NTSCStr(dttControp.Rows(0)!an_prov)
            strStato = NTSCStr(dttControp.Rows(0)!an_stato)
            If NTSCStr(dttControp.Rows(0)!an_pariva) <> "" Then
              strPICF = NTSCStr(dttControp.Rows(0)!an_pariva)
            Else
              strPICF = NTSCStr(dttControp.Rows(0)!an_codfis)
            End If
            If NTSCInt(!sc_coddest) > 0 Then
              If (!sc_tippaga.ToString = "1" Or !sc_tippaga.ToString = "2") And !sc_darave.ToString = "D" Then
                oCldComm.ValCodiceDb(!sc_coddest.ToString, strDitta, "DESTDIV", "N", "", dttDestdiv, !sc_controp.ToString)
              Else
                oCldComm.ValCodiceDb(!sc_coddest.ToString, strDitta, "DESTDIV", "N", "", dttDestdiv, !sc_conto.ToString)
              End If
              If dttDestdiv.Rows.Count > 0 Then
                strDescr1 = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_nomdest))
                strDescr2 = " "
                strIndir = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_inddest))
                strCap = NTSCStr(dttDestdiv.Rows(0)!dd_capdest)
                strCitta = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_locdest))
                strProv = NTSCStr(dttDestdiv.Rows(0)!dd_prodest)
                strStato = " "
              End If
            End If
          Else
            strDescr1 = ""
            strDescr2 = ""
            strIndir = ""
            strCap = ""
            strCitta = ""
            strProv = ""
            strStato = ""
            strPICF = ""
          End If
          '---------------------------------------------------------------------------------------------------------------
          str1 = IIf(strDtvalbon = "", NTSCDate(!sc_datsca).ToString("ddMMyy"), NTSCDate(strDtvalbon).ToString("ddMMyy")).ToString
          str2 = "000000"
          If bDataScadInDataValBenef = True Then
            str2 = str1
            str1 = "000000"
          End If
          '---------------------------------------------------------------------------------------------------------------
          w1.WriteLine(" 10" & Format(lProgrScad, "0000000") & "      " & str1 & str2 & "48000" & Format(dImporto, "0000000000000") & "+" & Right("00000" & NTSCStr(lAbi), 5) & Right("00000" & NTSCStr(lCab), 5) & Right("".PadLeft(12, CChar("0")) & strCC, 12) & Right(Format(!sc_abi, "00000"), 5) & Right(Format(!sc_cab, "00000"), 5) & Right("".PadLeft(12, CChar("0")) & strCCBeneficiario, 12) & Left(strRif & "     ", 5) & "5" & Format(!sc_conto, "0000000000000000") & "      " & strIdValuta)
          w1.WriteLine(" 16" & Format(lProgrScad, "0000000") & Left(strIBANOrdinante & "".PadLeft(27), 27) & "".PadLeft(7) & "".PadLeft(76))
          w1.WriteLine(" 17" & Format(lProgrScad, "0000000") & Left(strIBANBeneficiario & "".PadLeft(27), 27) & "".PadLeft(7) & "".PadLeft(76))
          w1.WriteLine(" 20" & Format(lProgrScad, "0000000") & Left(ConvStrFileAbi(dttAnaz.Rows(0)!tb_azrags1.ToString) & " " & ConvStrFileAbi(dttAnaz.Rows(0)!tb_azrags2.ToString) & Space(30), 30) & Left(ConvStrFileAbi(dttAnaz.Rows(0)!tb_azindir.ToString) & Space(30), 30) & Left(ConvStrFileAbi(dttAnaz.Rows(0)!tb_azcitta.ToString) & Space(30), 30) & Left(strPICFOrdin & Space(16), 16) & Space(4))
          w1.WriteLine(" 30" & Format(lProgrScad, "0000000") & Left(NTSCStr(strDescr1) & Space(30), 30) & Left(NTSCStr(strDescr2) & Space(30), 30) & Space(30) & Left(NTSCStr(strPICF) & Space(16), 16) & Space(4))
          If Not IsNumeric(strCap) Then
            strErr += vbCrLf & oApp.Tr(Me, 129307661833396095, "Attenzione: il CAP relativo al Conto '|" & !sc_conto.ToString() & "|' è sbagliato (non è un numero reale). Pertanto sarà generato il file con un CAP a zero.")
            strCap = "00000"
          Else
            strCap = strCap.PadLeft(5, "0"c).Substring(0, 5)
          End If
          w1.WriteLine(" 40" & Format(lProgrScad, "0000000") & Left(NTSCStr(strIndir) & "".PadLeft(30), 30) & Format(Val(NTSCStr(strCap)), "00000") & Left(NTSCStr(strCitta) & "".PadLeft(23), 23) & Left(NTSCStr(strProv) & "".PadLeft(2), 2) & Left(ConvStrFileAbi(NTSCStr(!sc_banc1)) & "".PadLeft(20), 20) & Left(ConvStrFileAbi(NTSCStr(!sc_banc2)) & "".PadLeft(20), 20) & "".PadLeft(10))
          nCausale = NTSCInt(!sc_causale)
          If nCausale <> 0 Then
            oCldComm.ValCodiceDb(!sc_causale.ToString, strDitta, "TABCAUC", "N", "", dttTmp)
            nCausale = NTSCInt(dttTmp.Rows(0)!tb_numregi2)
          End If
          If bRaggruppaBonificiForn Then
            If NTSCInt(!nrec) > 1 Then
              w1.WriteLine(" 50" & Format(lProgrScad, "0000000") & strRifVSFatt & Right("".PadLeft(nNumChar) & CStr(IIf(nCausale = 0, !sc_numdoc, !sc_numpar)), nNumChar) & IIf(Trim(NTSCStr(IIf(nCausale = 0, !sc_alfdoc, !sc_alfpar))) = "", "  ", "/" & Left(IIf(nCausale = 0, !sc_alfdoc, !sc_alfpar).ToString, 1)).ToString & " DEL " & NTSCDate(!sc_datdoc).ToString("dd/MM/yy") & " e altre" & "".PadLeft(39) & "".PadLeft(25))
            Else
              w1.WriteLine(" 50" & Format(lProgrScad, "0000000") & strRifVSFatt & Right("".PadLeft(nNumChar) & CStr(IIf(nCausale = 0, !sc_numdoc, !sc_numpar)), nNumChar) & IIf(Trim(NTSCStr(IIf(nCausale = 0, !sc_alfdoc, !sc_alfpar))) = "", "  ", "/" & Left(IIf(nCausale = 0, !sc_alfdoc, !sc_alfpar).ToString, 1)).ToString & " DEL " & NTSCDate(!sc_datdoc).ToString("dd/MM/yy") & "".PadLeft(47) & "".PadLeft(25))
            End If
          Else
            'w1.WriteLine(" 50" & Format(lProgrScad, "0000000") & strRifVSFatt & Right("".PadLeft(nNumChar) & CStr(IIf(nCausale = 0, !sc_numdoc, !sc_numpar)), nNumChar) & IIf(Trim(NTSCStr(IIf(nCausale = 0, !sc_alfdoc, !sc_alfpar))) = "", "  ", "/" & Left(IIf(nCausale = 0, !sc_alfdoc, !sc_alfpar).ToString, 1)).ToString & " DEL " & NTSCDate(!sc_datdoc).ToString("dd/MM/yy") & "".PadLeft(47) & "".PadLeft(25))
            strTmp = "".PadLeft(47) & "".PadLeft(25)

            If NTSCStr(!sc_cup).Trim <> "" Or NTSCStr(!sc_cig).Trim <> "" Then
              strTmp = " " & oApp.Tr(Me, 130474020821595303, "CUP:") & NTSCStr(!sc_cup).Trim & _
                       " " & oApp.Tr(Me, 130474020835394863, "CIG:") & NTSCStr(!sc_cig).Trim
              strTmp = strTmp.PadRight(42).Substring(0, 42) & "".PadLeft(5) & "".PadLeft(25)
            End If
            If bAccodaCIGCUPPinDescr = True And NTSCStr(!sc_cig).Trim = "" And NTSCStr(!sc_cup).Trim = "" Then
              strTmp = "".PadLeft(1) & NTSCStr(!sc_descr).PadRight(41, " "c).Substring(0, 41) & "".PadLeft(5) & "".PadLeft(25)
            End If
            w1.WriteLine(" 50" & Format(lProgrScad, "0000000") & strRifVSFatt & Right("".PadLeft(nNumChar) & CStr(IIf(nCausale = 0, !sc_numdoc, !sc_numpar)), nNumChar) & IIf(Trim(NTSCStr(IIf(nCausale = 0, !sc_alfdoc, !sc_alfpar))) = "", "  ", "/" & Left(IIf(nCausale = 0, !sc_alfdoc, !sc_alfpar).ToString, 1)).ToString & " DEL " & NTSCDate(!sc_datdoc).ToString("dd/MM/yy") & strTmp)
          End If
          w1.WriteLine(" 70" & Format(lProgrScad, "0000000") & "".PadLeft(100) & strCINBeneficiario & "".PadLeft(9))
          dTotScad = dTotScad + dImporto
          lRigheTot = lRigheTot + 8
          lProgrScad = lProgrScad + 1

        End With
      Next
      If bRaggruppaBonificiForn Then dttScaden.Clear()

      '----------------------
      'Scrive il piede
      w1.WriteLine(" EF" & Left(strRif & "     ", 5) & _
                   Right("00000" & NTSCStr(lAbi), 5) & _
                   NTSCDate(strDtdist).ToString("ddMMyy") & Left(strNomeSup & Space(20), 20) & "".PadLeft(6) & _
                   Format(lProgrScad - 1, "0000000") & "".PadLeft(15, CChar("0")) & Format(dTotScad, "000000000000000") & _
                   Format(lRigheTot + 1, "0000000") & "".PadLeft(24) & strIdValuta & "".PadLeft(6))
      w1.Flush()
      w1.Close()

      '----------------------
      'se devo copio il file anche nella dir alternativa
      strExt = oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "FileABICBICopiaIn", "", " ", "").Trim
      If strExt <> "" Then
        Try
          File.Copy(strNomeFile, (strExt & "\" & strNomeFile83).Replace("\\", "\"), True)
          strNomeFile += vbCrLf & "e copiato in " & (strExt & "\" & strNomeFile83).Replace("\\", "\")
        Catch ex As Exception
          'ignoro l'errore
        End Try
      End If

      Return True
    Catch ex As Exception
      If Not w1 Is Nothing Then w1.Close()
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GenAbiBonifSepa(ByVal strDitta As String, ByVal strOperat As String, ByVal strDtdist As String, _
                                          ByVal strDtvalbon As String, ByRef strNomeFileOut As String, _
                                          ByVal strTipoScadenze As String, ByVal strTippaga As String, _
                                          ByVal nDistAnno As Integer, ByVal lDistNum As Integer, _
                                          ByVal strPnint As String, ByVal bPrefissaFileAbiConDitta As Boolean, _
                                          ByVal bRaggruppaBonificiForn As Boolean, _
                                          ByRef dttAnaz As DataTable, ByRef dttTabbanc As DataTable, ByRef dttScaden As DataTable, _
                                          ByVal strPathFileAbi As String, ByVal bBonifEst As Boolean, ByRef strErr As String) As Boolean
    'GENERAZIONE BONIFICI con tracciato SEPA (sia bonifici italia che esteri)

    Dim strNomeFile As String = ""
    Dim bAccodaCIGCUPPinDescr As Boolean = False  'solo per bonifici italia
    Dim bFileCBInumdoc9Char As Boolean = CBool(oCldComm.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "FileCBInumdoc9Char", "0", " ", "0"))
    Dim bSepaIndirSplit As Boolean = CBool(oCldComm.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "SepaIndirSplit", "0", " ", "0"))  'se TRUE l'indirizzo del cliente non sarà esposto in un unico TAG, ma nei vari sottotag VIA, CIVICO, CAP, CITTA'
    Dim nNumChar As Integer = 5
    Dim strRifVSFatt As String = "RIF. VS. FATT. N. "
    Dim strRiferim As String = ""

    Dim strExt As String = ""
    Dim strNomeFile83 As String = ""
    Dim dtrT() As DataRow = Nothing 'record di scaden da portare su file

    Dim nRow As Integer = 0
    Dim sbFile As New System.Text.StringBuilder   'contenuto del file
    Dim w1 As StreamWriter = Nothing
    Dim strTmp As String = ""

    'dati debitore
    Dim strPICFOrdin As String = "" 'partita IVA o Cod fisc ditta
    Dim strIBANOrdinante As String = ""

    'dati beneficiario
    Dim dttControp As New DataTable
    Dim dttDestdiv As New DataTable
    Dim strDescr1 As String = ""
    Dim strDescr2 As String = ""
    Dim strIndir As String = ""
    Dim strCap As String = ""
    Dim strCitta As String = ""
    Dim strProv As String = ""
    Dim strStato As String = ""
    Dim strIBANBeneficiario As String = ""
    Dim strCINBeneficiario As String = ""
    Dim strCCBeneficiario As String = ""

    Dim strIDFile As String = ""      'ID univoco del flusso (max 35 caratteri)
    Dim strDtScad As String = ""      'data scadenza
    Dim lProgrScad As Integer = 0     'Contatore N° RID nel file
    Dim lRigheTot As Integer = 0      'Contatore righe scritte
    Dim dTotScad As Decimal = 0       'Progressivo valore scadenze
    Dim dttFiles As New DataTable     'un record per ogni file da generare (uno per ogni data scadenza!!!!)
    Dim oDttgr As New CLEGROUPBY

    Dim dttTmp As New DataTable
    Try
      If bBonifEst Then
        dttScaden = New DataTable
        dttTabbanc = New DataTable
        dttAnaz = New DataTable
      End If

      If bFileCBInumdoc9Char = True Then
        nNumChar = 9
        strRifVSFatt = "RIF.VS.FAT.N. "
      End If

      If Not IsDate(strDtdist) Then
        strErr += vbCrLf & oApp.Tr(Me, 130220861969773973, "Data distinta obbligatoria per la generazione dei file BONIFICO SEPA")
        Return False
      End If

      'If bBonifEst Then
      '  If Not (strTippaga = "5" And strTipoScadenze = "P") Then
      '    strErr += vbCrLf & oApp.Tr(Me, 130220861986849326, "Generazione file BONIFICO ESTERO prevista solo su fornitori e per scadenze di tipo 'Accredito bancario'.")
      '    Return False
      '  End If
      'End If

      '----------------------
      'valuta
      If oApp.ValutaCorrente = "EUR" Then
        Select Case nValuta
          Case 0
          Case Else
            strErr += vbCrLf & oApp.Tr(Me, 128939605192933309, "I bonifici secondo il tracciato SEPA possono essere realizzati solo con valuta EURO")
            Return False
        End Select
      Else
        strErr += vbCrLf & oApp.Tr(Me, 130220863073083746, "I bonifici secondo il tracciato SEPA possono essere realizzati solo con valuta EURO")
        Return False
      End If

      oCldComm.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttAnaz)

      '------------------------------------
      'ottengo i dati (solo per bonifici esteri, visto che per quelli italia sono già stati passati come parametro)
      If bBonifEst Then
        If Not oCldComm.GetDataScadenDistinta(strDitta, strTippaga, nDistAnno, lDistNum, strPnint, bRaggruppaBonificiForn, dttScaden) Then
          Return False
        End If
        If dttScaden.Rows.Count = 0 Then
          strErr += vbCrLf & oApp.Tr(Me, 128939606065929994, "Nessuna scadenza autorizzata")
          Return False
        End If
        oCldComm.ValCodiceDb(dttScaden.Rows(0)!sc_codbanc.ToString, strDitta, "TABBANC", "N", "", dttTabbanc)
      Else
        bAccodaCIGCUPPinDescr = CBool(ocldBase.GetSettingBus("BSCGDIST", "OPZIONI", ".", "AccodaCIGCUPPinDescr", "0", " ", "0"))
      End If

      '----------------------
      'ottengo l'elenco dei files da generare (uno per ogni data scadenza)
      If strDtvalbon <> "" Then
        'se ho indicato la data valuta faccio adedebitare tutto a quella data
        dttFiles.Columns.Add("sc_datsca", GetType(DateTime))
        dttFiles.Rows.Add(New Object() {NTSCDate(strDtvalbon)})
      Else
        strDtvalbon = ""
        If Not oDttgr.NTSGroupBy(dttScaden, dttFiles, "sc_datsca", "", "sc_datsca") Then
          Return False
        End If
      End If

      '----------------------
      'controlli preliminari
      If NTSCStr(dttTabbanc.Rows(0)!tb_codcuc).Trim = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 130220875860154569, "Nella tabella BANCHE della banca di presentazione BONIFICO non è stato indicato il codice CUC")
        Return False
      End If

      'Non è più obbligatorio da marzo 2016
      'If Trim(NTSCStr(dttTabbanc.Rows(0)!tb_swift)) = "" Then
      '  strErr += vbCrLf & oApp.Tr(Me, 130221475725968819, _
      '    "Non è indicato il 'BIC/SWIFT' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
      '  Return False
      'End If

      '----------------------
      'L'IBAN ordinante lo compone dai 4 campi: prefisso IBAN + CIN + ABI +CAB + NUM c/c
      If NTSCStr(dttTabbanc.Rows(0)!tb_prefiban) = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 130221475883276220, _
        "Non è indicato il 'prefisso IBAN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If Len(NTSCStr(dttTabbanc.Rows(0)!tb_prefiban)) <> 4 Then
        strErr += vbCrLf & oApp.Tr(Me, 130221475857604896, _
          "Il 'prefisso IBAN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "| deve essere di 4 caratteri")
        Return False
      End If
      If Left(NTSCStr(dttTabbanc.Rows(0)!tb_prefiban), 2) <> "IT" And Left(NTSCStr(dttTabbanc.Rows(0)!tb_prefiban), 2) <> "SM" Then
        strErr += vbCrLf & oApp.Tr(Me, 130221475832398898, _
          "Il 'prefisso IBAN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "| deve iniziare con i caratteri IT o SM.")
        Return False
      End If
      If NTSCStr(dttTabbanc.Rows(0)!tb_cin) = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 130221475801273691, _
          "Non è indicato il 'CIN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If NTSCInt(dttTabbanc.Rows(0)!tb_abi) = 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 130221475781829731, _
          "Non è indicato il cod. 'ABI banca' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If NTSCInt(dttTabbanc.Rows(0)!tb_cab) = 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 130221475764388903, _
          "Non è indicato il cod. 'CAB filiale' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If Trim(NTSCStr(dttTabbanc.Rows(0)!tb_csbf)) = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 130221475747748734, _
          "Non è indicato il 'N° C/C' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      strIBANOrdinante = NTSCStr(dttTabbanc.Rows(0)!tb_prefiban) & _
                         NTSCStr(dttTabbanc.Rows(0)!tb_cin) & _
                         Microsoft.VisualBasic.Right("00000" & NTSCInt(dttTabbanc.Rows(0)!tb_abi), 5) & _
                         Microsoft.VisualBasic.Right("00000" & NTSCInt(dttTabbanc.Rows(0)!tb_cab), 5) & _
                         Microsoft.VisualBasic.Right("000000000000" & Trim(NTSCStr(dttTabbanc.Rows(0)!tb_csbf)), 12)

      '----------------------
      'P.IVA ditta
      strPICFOrdin = ""
      If NTSCStr(dttAnaz.Rows(0)!tb_azpiva) <> "" Then
        strPICFOrdin = NTSCStr(dttAnaz.Rows(0)!tb_azpiva)
      Else
        strPICFOrdin = NTSCStr(dttAnaz.Rows(0)!tb_azcodf)
      End If

INIZIO:
      For Each dtrFile As DataRow In dttFiles.Rows
        'rispeto ai bonifici vecchi, in quelli sepa la è possibile indicare una sola data di disposizione 
        '(la data da cui far uscire i soldi dalla banca non è specificabile per le varie disposizioni, ma una unica globale di distinta)
        'la data valuta non esiste
        'per poter generare disposizione con date diverse l'unica soluzione è generare più files
        'nel nome del file verrà indicata anche la data di disposizione/pagamento
        sbFile = New System.Text.StringBuilder   'contenuto del file
        strTmp = ""

        strIDFile = ""      'ID univoco del flusso (max 35 caratteri)
        strDtScad = ""      'data scadenza
        lProgrScad = 0     'Contatore N° RID nel file
        lRigheTot = 0      'Contatore righe scritte
        dTotScad = 0       'Progressivo valore scadenze

        If strDtvalbon <> "" Then
          'se ho indicato la data valuta faccio adedebitare tutto a quella data
          dtrT = dttScaden.Select("")
        Else
          dtrT = dttScaden.Select("sc_datsca = " & CDataSQL(NTSCDate(dtrFile!sc_datsca)))
        End If

        '----------------------
        'determino il nome del flusso: max 35 caratteri. successivamente dovrò aggiungerne altri, per cui, per scrupolo, ne uso max 20
        'il nome del flusso deve essere univoco per ogni disposizione a parità di giorno
        While strIDFile = strDitta.PadRight(10).Substring(0, 8).Trim & DateTime.Now.ToString("yyMMddHHmmss")
          System.Threading.Thread.Sleep(100)
        End While
        strIDFile = strDitta.PadRight(10).Substring(0, 8).Trim & DateTime.Now.ToString("yyMMddHHmmss")

        '----------------------
        'inizio a scrivere il file
        sbFile.Append("<?xml version=""1.0"" encoding=""UTF-8""?>" & vbCrLf)
        sbFile.Append("<CBIBdyPaymentRequest xmlns=""urn:CBI:xsd:CBIBdyPaymentRequest.00.04.00"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:PMRQ=""urn:CBI:xsd:CBIPaymentRequest.00.04.00"">" & vbCrLf)
        sbFile.Append("<CBIEnvelPaymentRequest>" & vbCrLf)
        sbFile.Append("<CBIPaymentRequest>" & vbCrLf)

        '----------------------
        'SEZIONE GRUPPO
        sbFile.Append("<PMRQ:GrpHdr xmlns:PMRQ=""urn:CBI:xsd:CBIPaymentRequest.00.04.00"">" & vbCrLf)
        sbFile.Append(" <PMRQ:MsgId>" & strIDFile & "</PMRQ:MsgId>" & vbCrLf)  'identificativo univoco del flusso
        sbFile.Append(" <PMRQ:CreDtTm>" & DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss").Replace(".", ":") & "</PMRQ:CreDtTm>" & vbCrLf)  'data/ora creazione tipo 2013-04-13T14:40:06
        sbFile.Append(" <PMRQ:NbOfTxs>" & "°NUM_DISP°" & "</PMRQ:NbOfTxs>" & vbCrLf)  'numero delle disposizioni contenute nel file
        sbFile.Append(" <PMRQ:CtrlSum>" & "°TOT_DISP°" & "</PMRQ:CtrlSum>" & vbCrLf)  'somma degli importi di tutte le disposizioni
        sbFile.Append(" <PMRQ:InitgPty>" & vbCrLf)
        sbFile.Append("  <PMRQ:Nm>" & (ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azrags1)) & " " & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azrags2))).PadRight(70).Substring(0, 70).Trim & "</PMRQ:Nm>" & vbCrLf) 'nome del mittente (max 70 char)
        sbFile.Append("  <PMRQ:Id>" & vbCrLf)
        sbFile.Append("   <PMRQ:OrgId>" & vbCrLf)
        sbFile.Append("    <PMRQ:Othr>" & vbCrLf)
        sbFile.Append("     <PMRQ:Id>" & NTSCStr(dttTabbanc.Rows(0)!tb_codcuc).Trim & "</PMRQ:Id>" & vbCrLf)  'CUC banca mittente varchar(35)
        sbFile.Append("     <PMRQ:Issr>CBI</PMRQ:Issr>" & vbCrLf)  'valore fisso 'CBI'
        sbFile.Append("    </PMRQ:Othr>" & vbCrLf)
        sbFile.Append("   </PMRQ:OrgId>" & vbCrLf)
        sbFile.Append("  </PMRQ:Id>" & vbCrLf)
        sbFile.Append(" </PMRQ:InitgPty>" & vbCrLf)
        sbFile.Append("</PMRQ:GrpHdr>" & vbCrLf)
        sbFile.Append("<PMRQ:PmtInf xmlns:PMRQ=""urn:CBI:xsd:CBIPaymentRequest.00.04.00"">" & vbCrLf)
        sbFile.Append(" <PMRQ:PmtInfId>" & strIDFile & "</PMRQ:PmtInfId>" & vbCrLf)   'IDENTIFICATIVO UNIVOCO DEL FLUSSO DI ACCREDITO (VARCHAR(35)) PUO' ESSERE ANCHE UGUALE A <MsgId> CON IN CODA UN '-1'
        sbFile.Append(" <PMRQ:PmtMtd>TRA</PMRQ:PmtMtd>" & vbCrLf)       'SIGNIFICA bonifico sepa con esito all'ordinante (in alternativa TRF ovvero SENZA ESITO all'ordinante)
        sbFile.Append(" <PMRQ:BtchBookg>true</PMRQ:BtchBookg>" & vbCrLf)    'INCASSO CUMULATIVO (VALORE 'TRUE' O 'FALSE')
        sbFile.Append(" <PMRQ:PmtTpInf>" & vbCrLf)
        sbFile.Append("  <PMRQ:SvcLvl>" & vbCrLf)
        sbFile.Append("   <PMRQ:Cd>SEPA</PMRQ:Cd>" & vbCrLf) 'VALORE FISSO 'SEPA'
        sbFile.Append("  </PMRQ:SvcLvl>	" & vbCrLf)
        sbFile.Append(" </PMRQ:PmtTpInf>" & vbCrLf)
        If strDtvalbon <> "" Then
          sbFile.Append(" <PMRQ:ReqdExctnDt>" & NTSCDate(strDtvalbon).ToString("yyyy-MM-dd") & "</PMRQ:ReqdExctnDt>" & vbCrLf)  'DATA SCADENZA RICHIESTA DAL MITTENTE: 2013-07-23
        Else
          sbFile.Append(" <PMRQ:ReqdExctnDt>" & NTSCDate(dtrFile!sc_datsca).ToString("yyyy-MM-dd") & "</PMRQ:ReqdExctnDt>" & vbCrLf)  'DATA SCADENZA RICHIESTA DAL MITTENTE: 2013-07-23
        End If
        sbFile.Append(" <PMRQ:Dbtr>" & vbCrLf)
        sbFile.Append("  <PMRQ:Nm>" & (ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azrags1)) & " " & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azrags2))).PadRight(70).Substring(0, 70).Trim & "</PMRQ:Nm>" & vbCrLf)    'DEBITORE TITOLARE DEL C/C (VARCHAR(70)): DITTA
        sbFile.Append("  <PMRQ:PstlAdr>" & vbCrLf)

        If bSepaIndirSplit Then
          sbFile.Append("   <PMRQ:AdrTp>ADDR</PMRQ:AdrTp>" & vbCrLf)
          sbFile.Append("   <PMRQ:StrtNm>" & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azindir)).Trim & "</PMRQ:StrtNm>" & vbCrLf)
          'sbFile.Append("   <PMRQ:BldgNb>" & Numero civico... & "</PMRQ:BldgNb>" & vbCrLf)
          sbFile.Append("   <PMRQ:PstCd>" & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azcap)).Trim & "</PMRQ:PstCd>" & vbCrLf)
          sbFile.Append("   <PMRQ:TwnNm>" & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azcitta)).Trim & "</PMRQ:TwnNm>" & vbCrLf)
          sbFile.Append("   <PMRQ:CtrySubDvsn>" & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azprov)).Trim & "</PMRQ:CtrySubDvsn>" & vbCrLf)
          If NTSCStr(dttAnaz.Rows(0)!tb_azstato) = "IT" Or NTSCStr(dttAnaz.Rows(0)!tb_azstato).Trim = "I" Or NTSCStr(dttAnaz.Rows(0)!tb_azstato).Trim = "" Then
            sbFile.Append("   <PMRQ:Ctry>IT</PMRQ:Ctry>" & vbCrLf)    'STATO DEBITORE
          ElseIf NTSCStr(dttAnaz.Rows(0)!tb_azstato) = "RSM" Then
            sbFile.Append("   <PMRQ:Ctry>SM</PMRQ:Ctry>" & vbCrLf)    'STATO DEBITORE
          Else
            sbFile.Append("   <PMRQ:Ctry>" & NTSCStr(dttAnaz.Rows(0)!tb_azstato) & "</RLM:PMRQ>" & vbCrLf)    'STATO DEBITORE
          End If
        Else
          If NTSCStr(dttAnaz.Rows(0)!tb_azstato) = "IT" Or NTSCStr(dttAnaz.Rows(0)!tb_azstato).Trim = "I" Or NTSCStr(dttAnaz.Rows(0)!tb_azstato).Trim = "" Then
            sbFile.Append("   <PMRQ:Ctry>IT</PMRQ:Ctry>" & vbCrLf)    'STATO DEBITORE
          ElseIf NTSCStr(dttAnaz.Rows(0)!tb_azstato) = "RSM" Then
            sbFile.Append("   <PMRQ:Ctry>SM</PMRQ:Ctry>" & vbCrLf)    'STATO DEBITORE
          Else
            sbFile.Append("   <PMRQ:Ctry>" & NTSCStr(dttAnaz.Rows(0)!tb_azstato) & "</RLM:PMRQ>" & vbCrLf)    'STATO DEBITORE
          End If
          sbFile.Append("   <PMRQ:AdrLine>" & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azindir)).Trim & "</PMRQ:AdrLine>" & vbCrLf) '1^ RIGA: INDIRIZZO DEBITORE (VARCHAR(70)): VIA DANTE, 1
          sbFile.Append("   <PMRQ:AdrLine>" & (NTSCStr(dttAnaz.Rows(0)!tb_azcap) & " " & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azcitta)) & " " & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azprov))).PadRight(70).Substring(0, 70).Trim & "</PMRQ:AdrLine>" & vbCrLf) '2^ RIGA: CAP CITTA' DEBITORE (VARCHAR(70)): 47838 MONTEFIORE CONCA
        End If

        sbFile.Append("  </PMRQ:PstlAdr>" & vbCrLf)
        sbFile.Append(" </PMRQ:Dbtr>" & vbCrLf)
        sbFile.Append(" <PMRQ:DbtrAcct>" & vbCrLf)
        sbFile.Append("  <PMRQ:Id>" & vbCrLf)
        sbFile.Append("   <PMRQ:IBAN>" & strIBANOrdinante.Trim & "</PMRQ:IBAN>" & vbCrLf)    'IBAN DEBITORE
        sbFile.Append("  </PMRQ:Id>" & vbCrLf)
        sbFile.Append(" </PMRQ:DbtrAcct>" & vbCrLf)
        sbFile.Append(" <PMRQ:DbtrAgt>" & vbCrLf)
        sbFile.Append("  <PMRQ:FinInstnId>" & vbCrLf)
        sbFile.Append("   <PMRQ:BIC>" & NTSCStr(dttTabbanc.Rows(0)!tb_swift).Trim & "</PMRQ:BIC>" & vbCrLf)  'CODICE BIC DELLA BANCA DEL DEBITORE
        sbFile.Append("   <PMRQ:ClrSysMmbId>" & vbCrLf)
        sbFile.Append("    <PMRQ:MmbId>" & Microsoft.VisualBasic.Right("00000" & NTSCInt(dttTabbanc.Rows(0)!tb_abi), 5) & "</PMRQ:MmbId>" & vbCrLf)    'ABI BANCA DEBITORE
        sbFile.Append("   </PMRQ:ClrSysMmbId>" & vbCrLf)
        sbFile.Append("  </PMRQ:FinInstnId>" & vbCrLf)
        sbFile.Append(" </PMRQ:DbtrAgt>" & vbCrLf)
        sbFile.Append(" <PMRQ:ChrgBr>SLEV</PMRQ:ChrgBr>" & vbCrLf)

        '----------------------
        'SEZIONE INFORMAZIONI SULLE DISPOSIZIONI DI PAGAMENTO
        'CHE DOVRANNO ESSERE RIPETUTE PER TUTTI I RECORD DI SCADEN CONTENUTI TRA I TAG <CdtTrfTxInf> e </CdtTrfTxInf>

        'luppo su tutti i record di scaden
        For nRow = 0 To dtrT.Length - 1
          '------------------------------
          'ottengo l'IBAN beneficiario
          If bBonifEst Then
            strIBANBeneficiario = NTSCStr(dtrT(nRow)!sc_iban)
            If Trim(strIBANBeneficiario) = "" Then
              strErr += vbCrLf & oApp.Tr(Me, 130408994300646531, _
                    "Attenzione: codice IBAN beneficiario (ossia il campo 'IBAN estero') non indicato per la scadenza: |" & dtrT(nRow)!sc_conto.ToString & "| - |" & _
                    dtrT(nRow)!sc_annpar.ToString & "| - |" & dtrT(nRow)!sc_alfpar.ToString & "| - |" & dtrT(nRow)!sc_numpar.ToString & "| - |" & dtrT(nRow)!sc_numrata.ToString & _
                    "|.Si procedere comunque.")
            End If
          Else
            '----------------------------
            'Rileva l'IBAN del beneficiario
            If NTSCInt(dtrT(nRow)!sc_abi) = 0 Or NTSCInt(dtrT(nRow)!sc_cab) = 0 Then
              strErr += vbCrLf & oApp.Tr(Me, 130408994318085807, _
                        "Attenzione! Codice ABI e/o CAB mancante per la scadenza al |" & NTSCDate(dtrT(nRow)!sc_datsca).ToShortDateString & _
                        "| di conto/controp. |" & dtrT(nRow)!sc_conto.ToString & "/" & dtrT(nRow)!sc_controp.ToString & "|.")
            End If
            '----------------------------
            'Rileva C/C e CIN del beneficiario
            strCINBeneficiario = Trim(NTSCStr(dtrT(nRow)!sc_cin))
            strCCBeneficiario = UCase(NTSCStr(dtrT(nRow)!sc_numcc)).Trim

            If InStr(strCCBeneficiario, "#") > 0 Then 'per compatibilità
              If InStr(strCCBeneficiario, "#") < Len(strCCBeneficiario) Then
                'Se il CIN presente nel campo sc_cin non è valido lo prende da sc_numcc (per compatibilità all'indietro)
                If (Not strCINBeneficiario Like "[A-Z]") Or strCCBeneficiario = "" Then
                  strCINBeneficiario = Mid(strCCBeneficiario, InStr(strCCBeneficiario, "#") + 1, 1)
                End If
              End If
              'toglie dal C/c in cin e il prefiban
              If InStr(strCCBeneficiario, "#") > 1 Then
                strCCBeneficiario = Left(strCCBeneficiario, InStr(strCCBeneficiario, "#") - 1)
              Else
                strCCBeneficiario = ""
              End If
            End If
            'Controlla correttezza c/c e CIN
            If strCCBeneficiario = "" Or Len(strCCBeneficiario) > 12 Then
              strErr += vbCrLf & oApp.Tr(Me, 130408994492637929, _
                        "Attenzione! numero conto corrente non corretto |'" & NTSCStr(dtrT(nRow)!sc_numcc) & _
                        "'| per la scadenza al |" & NTSCStr(dtrT(nRow)!sc_datsca) & "| di conto/controp. |" & _
                        dtrT(nRow)!sc_conto.ToString & "/" & dtrT(nRow)!sc_controp.ToString & "|." & vbCrLf & vbCrLf & _
                        "Non rispetta la convenzione adottata: max 12 caratteri per il num. conto corrente.")
            End If
            If (Not strCINBeneficiario Like "[A-Z]") Then
              strErr += vbCrLf & oApp.Tr(Me, 130408994472960167, _
                        "Attenzione! il carattere di controllo CIN deve essere un carattere alfabetico A-Z per la " & _
                        "scadenza al |" & NTSCStr(dtrT(nRow)!sc_datsca) & "| di conto/controp. |" & _
                        dtrT(nRow)!sc_conto.ToString & "/" & dtrT(nRow)!sc_controp.ToString & "|.")
            End If
            'Rileva il prefisso IBAN
            strIBANBeneficiario = Trim(NTSCStr(dtrT(nRow)!sc_prefiban)).ToUpper
            If strIBANBeneficiario = "" Then
              strErr += vbCrLf & oApp.Tr(Me, 130408994455992716, _
                        "Attenzione! Non è indicato il 'prefisso IBAN' per la scadenza al |" & NTSCStr(dtrT(nRow)!sc_datsca) & _
                        "| di conto/controp. |" & dtrT(nRow)!sc_conto.ToString & "/" & dtrT(nRow)!sc_controp.ToString & "|.")
            End If
            If Len(strIBANBeneficiario) <> 4 Then
              strErr += vbCrLf & oApp.Tr(Me, 130408994438876294, _
                        "Attenzione! Il 'prefisso IBAN', per la scadenza al |" & NTSCStr(dtrT(nRow)!sc_datsca) & _
                        "| di conto/controp. |" & dtrT(nRow)!sc_conto.ToString & "/" & dtrT(nRow)!sc_controp.ToString & "| deve essere di 4 caratteri")
            End If
            If Left(strIBANBeneficiario, 2) <> "IT" And Left(strIBANBeneficiario, 2) <> "SM" Then
              strErr += vbCrLf & oApp.Tr(Me, 130408994423513035, _
                        "Attenzione! Il 'prefisso IBAN', per la scadenza al |" & NTSCStr(dtrT(nRow)!sc_datsca) & _
                        "| di conto/controp. |" & dtrT(nRow)!sc_conto.ToString & "/" & dtrT(nRow)!sc_controp.ToString & "|, " & _
                        "deve iniziare con i caratteri IT o SM.")
            End If
            strIBANBeneficiario = strIBANBeneficiario & strCINBeneficiario & Right("00000" & NTSCInt(dtrT(nRow)!sc_abi), 5) & _
                                  Right("00000" & NTSCInt(dtrT(nRow)!sc_cab), 5) & Right("000000000000" & strCCBeneficiario, 12)
          End If

          '----------------------------
          'ottengo l'indirizzo del destinatario
          If (dtrT(nRow)!sc_tippaga.ToString = "1" Or dtrT(nRow)!sc_tippaga.ToString = "2") And dtrT(nRow)!sc_darave.ToString = "D" Then
            oCldComm.ValCodiceDb(dtrT(nRow)!sc_controp.ToString, strDitta, "ANAGRA", "N", "", dttControp)
          Else
            oCldComm.ValCodiceDb(dtrT(nRow)!sc_conto.ToString, strDitta, "ANAGRA", "N", "", dttControp)
          End If
          If dttControp.Rows.Count > 0 Then
            strDescr1 = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_descr1))
            strDescr2 = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_descr2))
            strIndir = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_indir))
            strCap = NTSCStr(dttControp.Rows(0)!an_cap)
            strCitta = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_citta))
            strProv = NTSCStr(dttControp.Rows(0)!an_prov)
            strStato = NTSCStr(dttControp.Rows(0)!an_stato)
            If strStato.ToUpper = "I" Then strStato = "IT"
            If strStato <> "IT" And strStato.Trim <> "" Then
              oCldComm.ValCodiceDb(strStato, strDittaCorrente, "TABSTAT", "S", "", dttTmp)
              If dttTmp.Rows.Count > 0 Then
                If NTSCStr(dttTmp.Rows(0)!tb_siglacee).ToUpper.Trim <> "" Then
                  strStato = NTSCStr(dttTmp.Rows(0)!tb_siglacee).ToUpper
                End If
              End If
              dttTmp.Clear()
            End If
            If strStato = "RSM" Then strStato = "SM"
            If NTSCInt(dtrT(nRow)!sc_coddest) > 0 Then
              If (dtrT(nRow)!sc_tippaga.ToString = "1" Or dtrT(nRow)!sc_tippaga.ToString = "2") And dtrT(nRow)!sc_darave.ToString = "D" Then
                oCldComm.ValCodiceDb(dtrT(nRow)!sc_coddest.ToString, strDitta, "DESTDIV", "N", "", dttDestdiv, dtrT(nRow)!sc_controp.ToString)
              Else
                oCldComm.ValCodiceDb(dtrT(nRow)!sc_coddest.ToString, strDitta, "DESTDIV", "N", "", dttDestdiv, dtrT(nRow)!sc_conto.ToString)
              End If
              If dttDestdiv.Rows.Count > 0 Then
                strDescr1 = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_nomdest))
                strDescr2 = " "
                strIndir = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_inddest))
                strCap = NTSCStr(dttDestdiv.Rows(0)!dd_capdest)
                strCitta = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_locdest))
                strProv = NTSCStr(dttDestdiv.Rows(0)!dd_prodest)
                strStato = NTSCStr(dttDestdiv.Rows(0)!dd_stato)
                If strStato.ToUpper = "I" Then strStato = "IT"
                If strStato <> "IT" And strStato.Trim <> "" Then
                  oCldComm.ValCodiceDb(strStato, strDittaCorrente, "TABSTAT", "S", "", dttTmp)
                  If dttTmp.Rows.Count > 0 Then
                    If NTSCStr(dttTmp.Rows(0)!tb_siglacee).ToUpper.Trim <> "" Then
                      strStato = NTSCStr(dttTmp.Rows(0)!tb_siglacee).ToUpper
                    End If
                  End If
                  dttTmp.Clear()
                End If
                If strStato = "RSM" Then strStato = "SM"
              End If
            End If
          Else
            strDescr1 = ""
            strDescr2 = ""
            strIndir = ""
            strCap = ""
            strCitta = ""
            strProv = ""
            strStato = ""
          End If

          If NTSCStr(dtrT(nRow)!sc_swift).Trim = "" And strStato.Trim <> "" And strStato.Trim <> "IT" Then
            If dtrT(nRow)!sc_darave.ToString = "A" Then 'se è un fornitore
              Dim evnt As New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 130221491459576767, "Attenzione: il fornitore " & dtrT(nRow)!sc_conto.ToString & " è estero." & vbCrLf & "Vuoi che il file Xml abbia il codice stato italiano 'IT'?"))
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
                strStato = "IT"
              End If
            Else
              strErr += vbCrLf & oApp.Tr(Me, 130221491459576766, _
                      "Attenzione: codice 'BIC/SWIFT' beneficiario non indicato per la scadenza: |" & dtrT(nRow)!sc_conto.ToString & "| - |" & _
                      dtrT(nRow)!sc_annpar.ToString & "| - |" & dtrT(nRow)!sc_alfpar.ToString & "| - |" & dtrT(nRow)!sc_numpar.ToString & "| - |" & dtrT(nRow)!sc_numrata.ToString & _
                      "|. Si procederà comunque.")
            End If
          End If
          If strStato.Trim = "" Then strStato = "IT"

          '----------------------------
          'riferimenti
          strRiferim = strRifVSFatt
          If NTSCInt(dtrT(nRow)!sc_causale) = 0 Then
            strRiferim += NTSCInt(dtrT(nRow)!sc_numdoc).ToString
            If NTSCStr(dtrT(nRow)!sc_alfdoc).Trim <> "" Then strRiferim += "/" & NTSCStr(dtrT(nRow)!sc_alfdoc)
          Else
            strRiferim += NTSCInt(dtrT(nRow)!sc_numpar).ToString
            If NTSCStr(dtrT(nRow)!sc_alfpar).Trim <> "" Then strRiferim += "/" & NTSCStr(dtrT(nRow)!sc_alfpar)
          End If
          strRiferim += " DEL " & NTSCDate(dtrT(nRow)!sc_datdoc).ToString("dd/MM/yy")

          If bRaggruppaBonificiForn Then
            If NTSCInt(dtrT(nRow)!nrec) > 1 Then

              dttTmp.Clear()
              If Not oCldComm.GetAbiBonifSepaRiferim(strDitta, CBool(IIf(strDtvalbon <> "", True, False)), nDistAnno, lDistNum, strPnint, dtrT(nRow), dttTmp) Then Return False
              strRiferim = strRifVSFatt
              For Each dtrT1 As DataRow In dttTmp.Rows
                If NTSCInt(dtrT(nRow)!sc_causale) = 0 Then
                  strRiferim += NTSCInt(dtrT1!sc_numdoc).ToString
                  If NTSCStr(dtrT1!sc_alfdoc).Trim <> "" Then strRiferim += "/" & NTSCStr(dtrT1!sc_alfdoc)
                Else
                  strRiferim += NTSCInt(dtrT1!sc_numpar).ToString
                  If NTSCStr(dtrT1!sc_alfpar).Trim <> "" Then strRiferim += "/" & NTSCStr(dtrT1!sc_alfpar)
                End If
                strRiferim += " DEL " & NTSCDate(dtrT1!sc_datdoc).ToString("dd/MM/yy") & ", "
              Next
              dttTmp.Clear()
              strRiferim = strRiferim.Substring(0, strRiferim.Length - 2)
              If strRiferim.Length > 140 Then
                'max 140 caratteri
                strRiferim = strRiferim.Substring(0, 130) & oApp.Tr(Me, 130984716195425980, " e altre")
              End If
            End If
          Else
            If NTSCStr(dtrT(nRow)!sc_cup).Trim <> "" Then strRiferim += " " & oApp.Tr(Me, 130474020611072098, "CUP:") & NTSCStr(dtrT(nRow)!sc_cup).Trim
            If NTSCStr(dtrT(nRow)!sc_cig).Trim <> "" Then strRiferim += " " & oApp.Tr(Me, 130474020626382893, "CIG:") & NTSCStr(dtrT(nRow)!sc_cig).Trim
            If bAccodaCIGCUPPinDescr = True And NTSCStr(dtrT(nRow)!sc_cig).Trim = "" And NTSCStr(dtrT(nRow)!sc_cup).Trim = "" Then
              strRiferim += " " & NTSCStr(dtrT(nRow)!sc_descr)
            End If
          End If
          strRiferim = ConvStrFileAbi(strRiferim.Trim)
          strRiferim = strRiferim.PadRight(140).Substring(0, 140).Trim

          '----------------------------
          'inizio a comporre la sezione 
          lProgrScad += 1
          lRigheTot += 1
          dTotScad += NTSCDec(dtrT(nRow)!sc_importoda)
          sbFile.Append(" <PMRQ:CdtTrfTxInf>" & vbCrLf)
          sbFile.Append("  <PMRQ:PmtId>" & vbCrLf)
          sbFile.Append("   <PMRQ:InstrId>" & strIDFile & "-" & lProgrScad.ToString & "</PMRQ:InstrId>" & vbCrLf) '>IDENTIFICATIVO UNIVOCO DEL FLUSSO DI ADDEBITO (VARCHAR(35))
          sbFile.Append("   <PMRQ:EndToEndId>" & strIDFile & "-" & lProgrScad.ToString & "</PMRQ:EndToEndId>" & vbCrLf) '>IDENTIFICATIVO UNIVOCO DEL FLUSSO DI ADDEBITO (VARCHAR(35))
          sbFile.Append("  </PMRQ:PmtId>" & vbCrLf)
          If Left(strIBANBeneficiario, 2) = "IT" Then
            sbFile.Append("  <PMRQ:PmtTpInf>" & vbCrLf)
            sbFile.Append("   <PMRQ:CtgyPurp>" & vbCrLf)
            sbFile.Append("    <PMRQ:Cd>SUPP</PMRQ:Cd>" & vbCrLf) 'CASH: con questo (sistema generico - il file non passa)
            sbFile.Append("   </PMRQ:CtgyPurp>" & vbCrLf)
            sbFile.Append("  </PMRQ:PmtTpInf>" & vbCrLf)
          End If
          sbFile.Append("  <PMRQ:Amt>" & vbCrLf)
          sbFile.Append("   <PMRQ:InstdAmt Ccy=""EUR"">" & NTSCDec(dtrT(nRow)!sc_importoda).ToString("0.00").Replace(",", ".") & "</PMRQ:InstdAmt>" & vbCrLf) 'IMPORTO SCADENZA
          sbFile.Append("  </PMRQ:Amt>" & vbCrLf)
          If NTSCStr(dtrT(nRow)!sc_swift).Trim <> "" And Left(strIBANBeneficiario, 2) <> "IT" Then
            'obbligatorio solo per fornitori esteri
            sbFile.Append("  <PMRQ:CdtrAgt>" & vbCrLf)
            sbFile.Append("   <PMRQ:FinInstnId>" & vbCrLf)
            sbFile.Append("    <PMRQ:BIC>" & NTSCStr(dtrT(nRow)!sc_swift).Trim & "</PMRQ:BIC>" & vbCrLf) 'BIC BANCA CREDITORE
            sbFile.Append("   </PMRQ:FinInstnId>" & vbCrLf)
            sbFile.Append("  </PMRQ:CdtrAgt>" & vbCrLf)
          End If
          sbFile.Append("  <PMRQ:Cdtr>" & vbCrLf)
          sbFile.Append("   <PMRQ:Nm>" & (strDescr1 & " " & strDescr2).PadRight(70).Substring(0, 70).Trim & "</PMRQ:Nm>" & vbCrLf) 'DESCRIZIONE DEBITORE VARCHAR (70)
          sbFile.Append("   <PMRQ:PstlAdr>" & vbCrLf)

          If bSepaIndirSplit Then
            sbFile.Append("    <PMRQ:AdrTp>ADDR</PMRQ:AdrTp>" & vbCrLf)
            sbFile.Append("    <PMRQ:StrtNm>" & strIndir.Trim & "</PMRQ:StrtNm>" & vbCrLf)
            'sbFile.Append("    <PMRQ:BldgNb>" & Numero civico... & "</PMRQ:BldgNb>" & vbCrLf)
            sbFile.Append("    <PMRQ:PstCd>" & strCap.Trim & "</PMRQ:PstCd>" & vbCrLf)
            sbFile.Append("    <PMRQ:TwnNm>" & strCitta.Trim & "</PMRQ:TwnNm>" & vbCrLf)
            sbFile.Append("    <PMRQ:CtrySubDvsn>" & strProv.Trim & "</PMRQ:CtrySubDvsn>" & vbCrLf)
            sbFile.Append("    <PMRQ:Ctry>" & strStato & "</PMRQ:Ctry>" & vbCrLf) 'STATO CREDITORE: IT
          Else
            If Left(strIBANBeneficiario, 2).ToUpper <> "IT" Then
              sbFile.Append("    <PMRQ:TwnNm>" & (strCitta).PadRight(70).Substring(0, 70).Trim & "</PMRQ:TwnNm>" & vbCrLf)
            End If
            sbFile.Append("    <PMRQ:Ctry>" & strStato & "</PMRQ:Ctry>" & vbCrLf) 'STATO CREDITORE: IT
            If Left(strIBANBeneficiario, 2).ToUpper = "IT" Then
              sbFile.Append("    <PMRQ:AdrLine>" & strIndir.Trim & "</PMRQ:AdrLine>" & vbCrLf) '1^ RIGA: INDIRIZZO CREDITORE (VARCHAR(70)): VIA DANTE, 1
              sbFile.Append("    <PMRQ:AdrLine>" & (strCap & " " & strCitta & " " & strProv).PadRight(70).Substring(0, 70).Trim & "</PMRQ:AdrLine>" & vbCrLf) '2^ RIGA: CAP CITTA' CREDITORE (VARCHAR(70)): 47838 MONTEFIORE CONCA
            End If
          End If

          sbFile.Append("   </PMRQ:PstlAdr>" & vbCrLf)
          sbFile.Append("  </PMRQ:Cdtr>" & vbCrLf)
          sbFile.Append("  <PMRQ:CdtrAcct>" & vbCrLf)
          sbFile.Append("   <PMRQ:Id>" & vbCrLf)
          sbFile.Append("    <PMRQ:IBAN>" & strIBANBeneficiario.Trim & "</PMRQ:IBAN>" & vbCrLf)  'IBAN CREDITORE
          sbFile.Append("   </PMRQ:Id>" & vbCrLf)
          sbFile.Append("  </PMRQ:CdtrAcct>" & vbCrLf)
          If bBonifEst Then
            sbFile.Append("  <PMRQ:RgltryRptg>" & vbCrLf)
            sbFile.Append("   <PMRQ:DbtCdtRptgInd>DEBT</PMRQ:DbtCdtRptgInd>" & vbCrLf)
            'non passo la causale valutaria: per il momento non sembra obbligatoria
            'sbFile.Append("   <PMRQ:Dtls>" & vbCrLf)
            'sbFile.Append("    <PMRQ:Cd>CVA</PMRQ:Cd>" & vbCrLf)
            'sbFile.Append("   </PMRQ:Dtls>" & vbCrLf)
            sbFile.Append("  </PMRQ:RgltryRptg>" & vbCrLf)
          End If
          sbFile.Append("  <PMRQ:RmtInf>" & vbCrLf)
          sbFile.Append("   <PMRQ:Ustrd>" & strRiferim & "</PMRQ:Ustrd>" & vbCrLf)
          sbFile.Append("  </PMRQ:RmtInf>" & vbCrLf)
          sbFile.Append(" </PMRQ:CdtTrfTxInf>" & vbCrLf)
        Next    'For nRow = 0 To dtrT.Length - 1

        sbFile.Append("</PMRQ:PmtInf>" & vbCrLf)
        sbFile.Append("</CBIPaymentRequest>" & vbCrLf)
        sbFile.Append("</CBIEnvelPaymentRequest>" & vbCrLf)
        sbFile.Append("</CBIBdyPaymentRequest>" & vbCrLf)


        '----------------------
        'sostituisco nel file i marcatori con il num. tot delle disposizioni e gli importi
        strTmp = sbFile.ToString
        strTmp = strTmp.Replace("°NUM_DISP°", lRigheTot.ToString)
        strTmp = strTmp.Replace("°TOT_DISP°", dTotScad.ToString("0.00").Replace(",", "."))

        '----------------------
        'nome del file
        If strDtvalbon <> "" Then
          strExt = "_" & NTSCDate(strDtvalbon).ToString("ddMMyy") & ".xml"
        Else
          strExt = "_" & NTSCDate(dtrFile!sc_datsca).ToString("ddMMyy") & ".xml"
        End If
        If bBonifEst Then
          If bPrefissaFileAbiConDitta Then
            strNomeFile83 = strDitta & "_Bone" & Format(lDistNum, "0000") & strExt
          Else
            strNomeFile83 = "Bone" & Format(lDistNum, "0000") & strExt
          End If
        Else
          If bPrefissaFileAbiConDitta Then
            strNomeFile83 = strDitta & "_Bon" & Format(lDistNum, "00000") & strExt
          Else
            strNomeFile83 = "Bon" & Format(lDistNum, "00000") & strExt
          End If
        End If
        strNomeFile = strPathFileAbi & strNomeFile83

        '----------------------
        'salvo il file
        If System.IO.File.Exists(strNomeFile) Then System.IO.File.Delete(strNomeFile)
        w1 = New StreamWriter(strNomeFile, False)
        w1.Write(strTmp)
        w1.Flush()
        w1.Close()
        w1 = Nothing

        '----------------------
        'se devo copio il file anche nella dir alternativa
        strExt = oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "FileABICBICopiaIn", "", " ", "").Trim
        If strExt <> "" Then
          Try
            File.Copy(strNomeFile, (strExt & "\" & strNomeFile83).Replace("\\", "\"), True)
            strNomeFile += vbCrLf & "e copiato in " & (strExt & "\" & strNomeFile83).Replace("\\", "\")
          Catch ex As Exception
            'ignoro l'errore
          End Try
        End If
        strNomeFileOut += vbCrLf & strNomeFile & vbCrLf

      Next    'For Each dtrFile As DataRow In dttFiles.Rows

      Return True
    Catch ex As Exception
      If Not w1 Is Nothing Then w1.Close()
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GenRid(ByVal strDitta As String, ByVal strOperat As String, ByVal strDtdist As String, ByRef strNomeFile As String, _
                                          ByVal strTipoScadenze As String, ByVal strTippaga As String, _
                                          ByVal nDistAnno As Integer, ByVal lDistNum As Integer, _
                                          ByVal strPnint As String, ByVal bPrefissaFileAbiConDitta As Boolean, _
                                          ByVal bGestRid As Boolean, ByVal bRidCodCLieDeb As Boolean, _
                                          ByVal bRidCodClieDebTurnoDest As Boolean, ByVal bStampaRiferimentiFatture As Boolean, _
                                          ByVal strPathFileAbi As String, ByRef strErr As String) As Boolean
    '-------------------------------
    'generazione file ABI RIBA
    Dim dttAnaz As New DataTable
    Dim dttTabbanc As New DataTable
    Dim dttScaden As New DataTable
    Dim dttControp As New DataTable
    Dim dttDestdiv As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strNomeFile83 As String = ""
    Dim strIdValuta As String = ""
    Dim evt As NTSEventArgs = Nothing
    Dim w1 As StreamWriter = Nothing
    Dim strPR As String = ""
    Dim strNomeSup As String = ""
    Dim lProgrScad As Integer = 0     'Contatore N° RIBA generata
    Dim lRigheTot As Integer = 0      'Contatore righe sritte
    Dim dTotScad As Decimal = 0       'Progressivo valore scadenze
    Dim dImporto As Decimal = 0

    Dim strCINDebitore As String = ""
    Dim strCCDebitore As String = ""
    Dim strDescr1 As String = ""
    Dim strDescr2 As String = ""
    Dim strIndir As String = ""
    Dim strCap As String = ""
    Dim strCitta As String = ""
    Dim strProv As String = ""
    Dim strStato As String = ""
    Dim strPICF As String = ""
    Dim strContatto As String = ""
    Dim strExt As String = ".asc"

    Dim bVeloce As Boolean = False
    Dim strTmp As String = ""
    Dim strRiapriPartiteRaggPartita As String = ""

    Dim bFileCBInumdoc9Char As Boolean = CBool(oCldComm.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "FileCBInumdoc9Char", "0", " ", "0"))
    Dim nNumChar As Integer = 5
    Dim nNumChar1 As Integer = 6
    Dim strRifEstConto As String = "RIF.ESTR.CONTO N. "
    Dim strRifNSFatt As String = "RIF. NS. FATT. N."

    Dim bRIDRec10CodCliDebFillWithZeroLeft As Boolean = False
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Opzione di registro, globale, OPZIONI/FileCBInumdoc9Char (-1/0 <-- default)
      '--- Se attiva, il campo relativo al numero documento viene formattato con 9 caratteri anziché con 5 (standard)
      '--- e le descrizioni di riga vengono diminuite di 3-4 caratteri
      '--------------------------------------------------------------------------------------------------------------
      If bFileCBInumdoc9Char = True Then
        nNumChar = 9
        nNumChar1 = 9
        strRifEstConto = "RF.ES.CONTO N "
        strRifNSFatt = "RIF.NS.FATT.N."
      End If
      '--------------------------------------------------------------------------------------------------------------
      strRiapriPartiteRaggPartita = oCldComm.GetSettingBusDitt(strDitta, "BSCGINSO", "OPZIONI", ".", "RiapriPartiteRaggPartita", "X", " ", "X")
      '--------------------------------------------------------------------------------------------------------------
      bRIDRec10CodCliDebFillWithZeroLeft = CBool(oCldComm.GetSettingBus("BSCGINSO", "OPZIONI", ".", "RIDRec10CodCliDebFillWithZeroLeft", "0", " ", "0"))
      '--------------------------------------------------------------------------------------------------------------
      If Not IsDate(strDtdist) Then
        strErr += vbCrLf & oApp.Tr(Me, 130408994623181631, "Data distinta obbligatoria per la generazione dei file RID")
        Return False
      End If

      If Not (strTippaga = "1" And strTipoScadenze = "A" And bGestRid = True) Then
        strErr += vbCrLf & oApp.Tr(Me, 130408994600469049, "Generazione file RID prevista solo per scadenze ATTIVE con tipo pagamento TRATTA e con apposita opzione di registro di Business abilitata")
        Return False
      End If

      oCldComm.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttAnaz)

      '------------------------------------
      'ottengo i dati
      If Not oCldComm.GetDataScadenDistinta(strDitta, strTippaga, nDistAnno, lDistNum, strPnint, False, dttScaden) Then
        Return False
      End If
      dtrT = dttScaden.Select()
      If dtrT.Length = 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 128939606187808894, "Nessuna scadenza autorizzata")
        Return False
      End If
      If dtrT.Length > 0 Then
        oCldComm.ValCodiceDb(dtrT(0)!sc_codbanc.ToString, strDitta, "TABBANC", "N", "", dttTabbanc)
      End If

      '----------------------
      'nome del file
      strExt = oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "FileABICBIEstensioneFile", ".asc", " ", ".asc")
      If bPrefissaFileAbiConDitta Then
        strNomeFile83 = strDitta & "_Rid" & Format(lDistNum, "00000") & strExt
      Else
        strNomeFile83 = "Rid" & Format(lDistNum, "00000") & strExt
      End If
      strNomeFile = strPathFileAbi & strNomeFile83

      bVeloce = CBool(oCldComm.GetSettingBusDitt(strDitta, "BSCGDIST", "OPZIONI", ".", "RidVeloce", "0", " ", "0"))

      '----------------------
      'valuta
      If oApp.ValutaCorrente = "EUR" Then
        Select Case nValuta
          Case 0 : strIdValuta = "E"
          Case 100 : strIdValuta = "I"
          Case Else
            strErr += vbCrLf & oApp.Tr(Me, 130408994826378632, _
                "Codice valuta non valido per generazione file RID." & vbCrLf & _
                "Codici valuta consentiti: '0' o '100'.")
            Return False
        End Select
      Else
        strErr += vbCrLf & oApp.Tr(Me, 130408994803982976, _
                "Codice valuta non valido per generazione file RID." & vbCrLf & _
                "Codici valuta consentiti: '0' o '100'.")
        Return False
      End If

      '----------------------
      'Controlla se già esiste il file
      If System.IO.File.Exists(strNomeFile) Then
        'evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128238690194733441, "Esiste già un file chiamato |" & UCase(strNomeFile) & "|. Sovrascriverlo?"))
        'ThrowRemoteEvent(evt)
        'If evt.RetValue = "NO" Then Return False
        System.IO.File.Delete(strNomeFile)
      End If

      w1 = New StreamWriter(strNomeFile, False)
      '----------------------
      'Scrive la testata
      strPR = " IR"
      strNomeSup = Left("Dist. " & Format(lDistNum, "000000000") & "-" & nDistAnno.ToString, 20)
      strTmp = strPR & Left(NTSCStr(dttTabbanc.Rows(0)!tb_rifriba) & "     ", 5) & _
                       Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_abi), 5) & NTSCDate(strDtdist).ToString("ddMMyy") & _
                       Left(strNomeSup & Space(20), 20) & "".PadLeft(6) & "".PadLeft(68) & strIdValuta & "".PadLeft(6)

      'Se è in modalità veloce metto nel 113° carattere "V" (a indice 0 è il 112°)
      If bVeloce Then
        strTmp = strTmp.Substring(0, 112) & "V" & strTmp.Substring(113)
      End If

      w1.WriteLine(strTmp)


      lRigheTot = 1
      lProgrScad = 1
      dTotScad = 0

      '----------------------
      'scrivo il corpo
      For i = 0 To dtrT.Length - 1
        With dtrT(i)

          If NTSCInt(!sc_abi) = 0 Or NTSCInt(!sc_cab) = 0 Then
            strErr += vbCrLf & oApp.Tr(Me, 128939605798265179, _
    "Attenzione! Codice ABI e/o CAB mancante per la scadenza al |" & NTSCDate(!sc_datsca).ToShortDateString & _
    "| di conto/controp. |" & !sc_conto.ToString & "/" & !sc_controp.ToString & "|.")
          End If
          '------------------------------------------------------------------------------
          'Rileva C/C e CIN del beneficiario
          strCINDebitore = " "
          strCCDebitore = UCase(NTSCStr(!sc_numcc))
          'Controlla che in sc_numcc sia indicato: 000000000000#A
          '(ossia 12 caratteri del c/c, + #, + carattere CIN)
          If InStr(strCCDebitore, "#") > 0 Then
            'rileva il carattere CIN
            If InStr(strCCDebitore, "#") < Len(strCCDebitore) Then
              strCINDebitore = Mid(strCCDebitore, InStr(strCCDebitore, "#") + 1, 1)
            End If
            'rileva il c/c
            If InStr(strCCDebitore, "#") > 1 Then
              strCCDebitore = Left(strCCDebitore, InStr(strCCDebitore, "#") - 1)
            Else
              strCCDebitore = ""
            End If
          End If

          Select Case nValuta
            Case 0 : dImporto = (NTSCDec(!sc_importoda) * 100)
            Case 100 : dImporto = NTSCDec(!sc_impvalda)
          End Select
          ' decod. conto per tirare su il cod. fisc. o partita iva
          If (!sc_tippaga.ToString = "1" Or !sc_tippaga.ToString = "2") And !sc_darave.ToString = "D" Then
            oCldComm.ValCodiceDb(!sc_controp.ToString, strDitta, "ANAGRA", "N", "", dttControp)
          Else
            oCldComm.ValCodiceDb(!sc_conto.ToString, strDitta, "ANAGRA", "N", "", dttControp)
          End If
          If dttControp.Rows.Count > 0 Then
            strDescr1 = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_descr1))
            strDescr2 = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_descr2))
            strIndir = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_indir))
            strCap = NTSCStr(dttControp.Rows(0)!an_cap)
            strCitta = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_citta))
            strProv = NTSCStr(dttControp.Rows(0)!an_prov)
            strStato = NTSCStr(dttControp.Rows(0)!an_stato)
            strContatto = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_contatt))
            If NTSCStr(dttControp.Rows(0)!an_pariva) <> "" Then
              strPICF = NTSCStr(dttControp.Rows(0)!an_pariva)
            Else
              strPICF = NTSCStr(dttControp.Rows(0)!an_codfis)
            End If
            If NTSCInt(!sc_coddest) > 0 Then
              If (!sc_tippaga.ToString = "1" Or !sc_tippaga.ToString = "2") And !sc_darave.ToString = "D" Then
                oCldComm.ValCodiceDb(!sc_coddest.ToString, strDitta, "DESTDIV", "N", "", dttDestdiv, !sc_controp.ToString)
              Else
                oCldComm.ValCodiceDb(!sc_coddest.ToString, strDitta, "DESTDIV", "N", "", dttDestdiv, !sc_conto.ToString)
              End If
              If dttDestdiv.Rows.Count > 0 Then
                strDescr1 = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_nomdest))
                strDescr2 = " "
                strIndir = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_inddest))
                strCap = NTSCStr(dttDestdiv.Rows(0)!dd_capdest)
                strCitta = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_locdest))
                strProv = NTSCStr(dttDestdiv.Rows(0)!dd_prodest)
                If ocldBase.GetSettingBus("BSCGDIST", "OPZIONI", ".", "RidCFPIdaDestDiv", "0", ".", "0") = "-1" Then
                  If NTSCStr(dttDestdiv.Rows(0)!dd_pariva) <> "" Then
                    strPICF = NTSCStr(dttDestdiv.Rows(0)!dd_pariva)
                  ElseIf NTSCStr(dttDestdiv.Rows(0)!dd_codfis) <> "" Then
                    strPICF = NTSCStr(dttDestdiv.Rows(0)!dd_codfis)
                  End If
                End If
                strStato = " "
              End If
            End If
          Else
            strDescr1 = ""
            strDescr2 = ""
            strIndir = ""
            strCap = ""
            strCitta = ""
            strProv = ""
            strStato = ""
            strPICF = ""
            strContatto = ""
          End If

          If bRidCodClieDebTurnoDest Then
            strTmp = " 10" & Format(lProgrScad, "0000000") & NTSCDate(!sc_datsca).ToString("ddMMyy") & "      " & NTSCDate(!sc_datsca).ToString("ddMMyy") & "50000" & Format(dImporto, "0000000000000") & "-" & Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_abi), 5) & Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_cab), 5) & Left(dttTabbanc.Rows(0)!tb_csbf.ToString & "".PadLeft(12), 12) & Right("00000" & NTSCStr(!sc_abi), 5) & Right("00000" & NTSCStr(!sc_cab), 5) & Left(!sc_numcc.ToString & "".PadLeft(12, CChar("0")), 12) & Left(dttTabbanc.Rows(0)!tb_rifriba.ToString & "     ", 5) & "4" & Left(NTSCStr(!sc_descr) & "".PadLeft(16, CChar("0")), 16) & "".PadLeft(6) & strIdValuta
          Else
            If bRidCodCLieDeb Then
              strTmp = " 10" & Format(lProgrScad, "0000000") & NTSCDate(!sc_datsca).ToString("ddMMyy") & "      " & NTSCDate(!sc_datsca).ToString("ddMMyy") & "50000" & Format(dImporto, "0000000000000") & "-" & Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_abi), 5) & Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_cab), 5) & Left(dttTabbanc.Rows(0)!tb_csbf.ToString & "".PadLeft(12), 12) & Right("00000" & NTSCStr(!sc_abi), 5) & Right("00000" & NTSCStr(!sc_cab), 5) & Left(!sc_numcc.ToString & "".PadLeft(12, CChar("0")), 12) & Left(dttTabbanc.Rows(0)!tb_rifriba.ToString & "     ", 5) & "4" & Left(strContatto & "".PadLeft(16, CChar(" ")), 16) & "".PadLeft(6) & strIdValuta
            Else
              If bRIDRec10CodCliDebFillWithZeroLeft Then
                strTmp = " 10" & Format(lProgrScad, "0000000") & NTSCDate(!sc_datsca).ToString("ddMMyy") & "      " & NTSCDate(!sc_datsca).ToString("ddMMyy") & "50000" & Format(dImporto, "0000000000000") & "-" & Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_abi), 5) & Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_cab), 5) & Right("".PadLeft(12, CChar("0")) & dttTabbanc.Rows(0)!tb_csbf.ToString, 12) & Right("00000" & NTSCStr(!sc_abi), 5) & Right("00000" & NTSCStr(!sc_cab), 5) & Right("".PadLeft(16, CChar("0")) & strCCDebitore, 12) & Left(dttTabbanc.Rows(0)!tb_rifriba.ToString & "     ", 5) & "4" & Right(NTSCStr(!sc_controp).PadLeft(16, CChar("0")), 16) & "".PadLeft(6) & strIdValuta
              Else
                strTmp = " 10" & Format(lProgrScad, "0000000") & NTSCDate(!sc_datsca).ToString("ddMMyy") & "      " & NTSCDate(!sc_datsca).ToString("ddMMyy") & "50000" & Format(dImporto, "0000000000000") & "-" & Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_abi), 5) & Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_cab), 5) & Right("".PadLeft(12, CChar("0")) & dttTabbanc.Rows(0)!tb_csbf.ToString, 12) & Right("00000" & NTSCStr(!sc_abi), 5) & Right("00000" & NTSCStr(!sc_cab), 5) & Right("".PadLeft(16, CChar("0")) & strCCDebitore, 12) & Left(dttTabbanc.Rows(0)!tb_rifriba.ToString & "     ", 5) & "4" & Left(NTSCStr(!sc_controp) & "".PadLeft(16), 16) & "".PadLeft(6) & strIdValuta
              End If
            End If
          End If
          'Se è in modalità veloce metto nel 119° carattere "V" (a indice 0 è il 118°)
          If bVeloce Then
            strTmp = strTmp.Substring(0, 118) & "V" & strTmp.Substring(119)
          End If

          w1.WriteLine(strTmp)
          w1.WriteLine(" 20" & Format(lProgrScad, "0000000") & Left(ConvStrFileAbi(dttAnaz.Rows(0)!tb_azrags1.ToString) & "".PadLeft(30), 30) & Left(ConvStrFileAbi(dttAnaz.Rows(0)!tb_azindir.ToString) & "".PadLeft(30), 30) & Left(ConvStrFileAbi(dttAnaz.Rows(0)!tb_azcitta.ToString) & "".PadLeft(30), 30) & "".PadLeft(20))
          w1.WriteLine(" 30" & Format(lProgrScad, "0000000") & Left(NTSCStr(strDescr1) & "".PadLeft(30), 30) & Left(NTSCStr(strDescr2) & "".PadLeft(30), 30) & "".PadLeft(30) & Left(NTSCStr(strPICF) & "".PadLeft(16), 16) & "".PadLeft(4))
          If Not IsNumeric(strCap) Then
            strErr += vbCrLf & oApp.Tr(Me, 129973853643801964, "Attenzione: il CAP relativo al Conto/controp. '|" & !sc_conto.ToString() & "/" & !sc_controp.ToString & "|' è sbagliato (non è un numero reale). Pertanto sarà generato il file con un CAP a zero.")
            strCap = "00000"
          Else
            strCap = strCap.PadLeft(5, "0"c).Substring(0, 5)
          End If
          w1.WriteLine(" 40" & Format(lProgrScad, "0000000") & Left(NTSCStr(strIndir) & "".PadLeft(30), 30) & Format(Val(NTSCStr(strCap)), "00000") & Left(NTSCStr(strCitta) & "".PadLeft(23), 23) & Left(NTSCStr(strProv) & "".PadLeft(2), 2) & "".PadLeft(50))
          If (IIf(Trim(NTSCStr(!sc_alfdoc)) = "", " ", Left(!sc_alfdoc.ToString, 1))).ToString.ToUpper = strRiapriPartiteRaggPartita.ToUpper Then
            If bStampaRiferimentiFatture Then
              w1.WriteLine(" 50" & Format(lProgrScad, "0000000") & Left(NTSCStr(!sc_descr) & Space(40), 40) & Space(40) & Space(30))
            Else
              w1.WriteLine(" 50" & Format(lProgrScad, "0000000") & strRifEstConto & Right("".PadLeft(nNumChar) & CStr(!sc_numdoc), nNumChar) & "/" & strRiapriPartiteRaggPartita & " DEL " & NTSCDate(!sc_datdoc).ToString("dd/MM/yy") & "".PadLeft(52) & "".PadLeft(20))
            End If
          Else
            'se sono stato chiamato da bscgdist devo scrivere anche il cliente (in sc_conto c'è il conto effetti)
            If NTSCInt(!sc_controp) <> 0 Then
              'dist
              w1.WriteLine(" 50" & Format(lProgrScad, "0000000") & strRifNSFatt & Right("".PadLeft(nNumChar1) & CStr(!sc_numdoc), nNumChar1) & IIf(Trim(NTSCStr(!sc_alfdoc)) = "", "  ", "/" & Left(!sc_alfdoc.ToString, 1)).ToString & " DEL " & NTSCDate(!sc_datdoc).ToString("dd/MM/yy") & " C:" & Right("".PadLeft(9, "0"c) & NTSCStr(!sc_controp), 9) & "".PadLeft(40) & "".PadLeft(20))
            Else
              'auto
              w1.WriteLine(" 50" & Format(lProgrScad, "0000000") & strRifNSFatt & Right("".PadLeft(nNumChar1) & CStr(!sc_numdoc), nNumChar1) & IIf(Trim(NTSCStr(!sc_alfdoc)) = "", "  ", "/" & Left(!sc_alfdoc.ToString, 1)).ToString & " DEL " & NTSCDate(!sc_datdoc).ToString("dd/MM/yy") & "".PadLeft(52) & "".PadLeft(20))
            End If

          End If
          w1.WriteLine(" 70" & Format(lProgrScad, "0000000") & "".PadLeft(15) & "".PadLeft(69) & "".PadLeft(3, CChar(" ")) & "".PadLeft(23))
          dTotScad = dTotScad + dImporto
          lRigheTot = lRigheTot + 6
          lProgrScad = lProgrScad + 1

        End With
      Next

      '----------------------
      'Scrive il piede
      strTmp = " EF" & Left(NTSCStr(dttTabbanc.Rows(0)!tb_rifriba) & "     ", 5) & _
                     Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_abi), 5) & _
                     NTSCDate(strDtdist).ToString("ddMMyy") & Left(strNomeSup & Space(20), 20) & "".PadLeft(6) & _
                     Format(lProgrScad - 1, "0000000") & Format(dTotScad, "000000000000000") & "".PadLeft(15, CChar("0")) & _
                     Format(lRigheTot + 1, "0000000") & "".PadLeft(24) & strIdValuta & "".PadLeft(6)

      'Se è in modalità veloce metto nel 113° carattere "V" (a indice 0 è il 112°)
      If bVeloce Then
        strTmp = strTmp.Substring(0, 112) & "V" & strTmp.Substring(113)
      End If

      w1.WriteLine(strTmp)
      w1.Flush()
      w1.Close()

      '----------------------
      'se devo copio il file anche nella dir alternativa
      strExt = oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "FileABICBICopiaIn", "", " ", "").Trim
      If strExt <> "" Then
        Try
          File.Copy(strNomeFile, (strExt & "\" & strNomeFile83).Replace("\\", "\"), True)
          strNomeFile += vbCrLf & "e copiato in " & (strExt & "\" & strNomeFile83).Replace("\\", "\")
        Catch ex As Exception
          'ignoro l'errore
        End Try
      End If

      Return True
    Catch ex As Exception
      If Not w1 Is Nothing Then w1.Close()
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttScaden.Clear()
      dttAnaz.Clear()
      dttTabbanc.Clear()
    End Try
  End Function
  Public Overridable Function GenRidSepa(ByVal strDitta As String, ByVal strOperat As String, ByVal strDtdist As String, ByRef strNomeFile As String, _
                                          ByVal strTipoScadenze As String, ByVal strTippaga As String, _
                                          ByVal nDistAnno As Integer, ByVal lDistNum As Integer, _
                                          ByVal strPnint As String, ByVal bPrefissaFileAbiConDitta As Boolean, _
                                          ByVal bGestRid As Boolean, ByVal bRidCodCLieDeb As Boolean, _
                                          ByVal bRidCodClieDebTurnoDest As Boolean, ByVal bStampaRiferimentiFatture As Boolean, _
                                          ByVal strPathFileAbi As String, ByRef strErr As String) As Boolean
    '-------------------------------
    'generazione file RID su traccaito SEPA 
    Dim strIDFile As String = ""      'ID univoco del flusso (max 35 caratteri)
    Dim sbFile As New System.Text.StringBuilder   'contenuto del file
    Dim dttAnaz As New DataTable
    Dim dttTabbanc As New DataTable
    Dim dttScaden As New DataTable
    Dim dttControp As New DataTable
    Dim dttDestdiv As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strNomeFile83 As String = ""
    Dim evt As NTSEventArgs = Nothing
    Dim w1 As StreamWriter = Nothing

    Dim strNomeSup As String = ""

    Dim strExt As String = ".asc"

    Dim strTmp As String = ""
    Dim strRiapriPartiteRaggPartita As String = ""

    Dim strRifEstConto As String = "RIF.ESTR.CONTO N. "
    Dim strRifNSFatt As String = "RIF. NS. FATT. N."
    Dim strRiferim As String = ""

    Dim strDescr1 As String = ""
    Dim strDescr2 As String = ""
    Dim strIndir As String = ""
    Dim strCap As String = ""
    Dim strCitta As String = ""
    Dim strProv As String = ""
    Dim strStato As String = ""
    Dim strIBANDebitore As String = ""
    Dim strCINDebitore As String = ""
    Dim strCCDebitore As String = ""

    Dim strIBANOrdinante As String = ""

    Dim strTipoRid As String = ""
    Dim strTipoSeqRid As String = ""
    Dim strTipoRidOld As String = ""    'dtrT(i)!sc_tiporid record precedente
    Dim strTipoSeqRidOld As String = "" 'dtrT(i)!sc_tiposqerid record precedente
    Dim dtScadOld As Date = DateTime.Now     'dtrT(i)!sc_datsca record precedente
    Dim nPmtInf As Integer = 0

    Dim lProgrScad As Integer = 0     'Contatore N° RID nel file
    Dim lRigheTot As Integer = 0      'Contatore righe scritte
    Dim dTotScad As Decimal = 0       'Progressivo valore scadenze

    Dim dttTmp As New DataTable
    Dim strCausale As String = "CASH" 'CAUSALE DELLA TRANSAZIONE CASH=Transaction is a general cash management instruction. ACCT=Transaction moves funds between 2 accounts of same account holder at the same bank.
    Dim bSepaIndirSplit As Boolean = CBool(oCldComm.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "SepaIndirSplit", "0", " ", "0"))  'se TRUE l'indirizzo del cliente non sarà esposto in un unico TAG, ma nei vari sottotag VIA, CIVICO, CAP, CITTA'

    Try
      strRiapriPartiteRaggPartita = oCldComm.GetSettingBusDitt(strDitta, "BSCGINSO", "OPZIONI", ".", "RiapriPartiteRaggPartita", "X", " ", "X")
      strCausale = oCldComm.GetSettingBusDitt(strDitta, "BSCGDIST", "OPZIONI", ".", "CausaleRidSepa", "CASH", " ", "CASH")

      If Not IsDate(strDtdist) Then
        strErr += vbCrLf & oApp.Tr(Me, 128939607121901284, "Data distinta obbligatoria per la generazione dei file RID")
        Return False
      End If

      If Not (strTippaga = "1" And strTipoScadenze = "A" And bGestRid = True) Then
        strErr += vbCrLf & oApp.Tr(Me, 128238810542828534, "Generazione file RID prevista solo per scadenze ATTIVE con tipo pagamento TRATTA e con apposita opzione di registro di Business abilitata")
        Return False
      End If

      oCldComm.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttAnaz)

      '------------------------------------
      'ottengo i dati
      If Not oCldComm.GetDataScadenDistinta(strDitta, strTippaga, nDistAnno, lDistNum, strPnint, False, dttScaden) Then
        Return False
      End If

      dtrT = dttScaden.Select("", "sc_tiporid, sc_tiposeqrid")  'scarto le scadenze 'NO RID'
      If dtrT.Length = 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 130408994918212138, "Nessuna scadenza autorizzata")
        Return False
      End If
      If dtrT.Length > 0 Then
        oCldComm.ValCodiceDb(dtrT(0)!sc_codbanc.ToString, strDitta, "TABBANC", "N", "", dttTabbanc)
      End If

      '----------------------
      'controlli preliminari
      If NTSCStr(dttTabbanc.Rows(0)!tb_codcuc).Trim = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 130198340093715577, "Nella tabella BANCHE della banca di presentazione RID non è stato indicato il codice CUC")
        Return False
      End If

      If dttScaden.Select("sc_tiporid = ' '").Length > 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 130198371471376786, "Tra le scadenze selezionate sono presenti alcune con 'Tipo RID' = 'NO RID'")
        Return False
      End If

      '----------------------
      'L'IBAN ordinante lo compone dai 4 campi: prefisso IBAN + CIN + ABI +CAB + NUM c/c
      If NTSCStr(dttTabbanc.Rows(0)!tb_prefiban) = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 130198383208670678, _
        "Non è indicato il 'prefisso IBAN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If Len(NTSCStr(dttTabbanc.Rows(0)!tb_prefiban)) <> 4 Then
        strErr += vbCrLf & oApp.Tr(Me, 130198383227848814, _
          "Il 'prefisso IBAN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "| deve essere di 4 caratteri")
        Return False
      End If
      If Left(NTSCStr(dttTabbanc.Rows(0)!tb_prefiban), 2) <> "IT" And Left(NTSCStr(dttTabbanc.Rows(0)!tb_prefiban), 2) <> "SM" Then
        strErr += vbCrLf & oApp.Tr(Me, 130198383246083750, _
          "Il 'prefisso IBAN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "| deve iniziare con i caratteri IT o SM.")
        Return False
      End If
      If NTSCStr(dttTabbanc.Rows(0)!tb_cin) = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 130198383262717880, _
          "Non è indicato il 'CIN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If NTSCInt(dttTabbanc.Rows(0)!tb_abi) = 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 130198383288839233, _
          "Non è indicato il cod. 'ABI banca' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If NTSCInt(dttTabbanc.Rows(0)!tb_cab) = 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 130198383336649527, _
          "Non è indicato il cod. 'CAB filiale' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If Trim(NTSCStr(dttTabbanc.Rows(0)!tb_csbf)) = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 130198383358600957, _
          "Non è indicato il 'N° C/C' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      strIBANOrdinante = NTSCStr(dttTabbanc.Rows(0)!tb_prefiban) & _
                         NTSCStr(dttTabbanc.Rows(0)!tb_cin) & _
                         Microsoft.VisualBasic.Right("00000" & NTSCInt(dttTabbanc.Rows(0)!tb_abi), 5) & _
                         Microsoft.VisualBasic.Right("00000" & NTSCInt(dttTabbanc.Rows(0)!tb_cab), 5) & _
                         Microsoft.VisualBasic.Right("000000000000" & Trim(NTSCStr(dttTabbanc.Rows(0)!tb_csbf)), 12)

      ''Non è più obbligatorio da marzo 2016
      'If Trim(NTSCStr(dttTabbanc.Rows(0)!tb_swift)) = "" Then
      '  strErr += vbCrLf & oApp.Tr(Me, 130198384735138976, _
      '    "Non è indicato il 'BIC/SWIFT' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
      '  Return False
      'End If

      If Trim(NTSCStr(dttTabbanc.Rows(0)!tb_rididcred)) = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 130198385533053750, _
          "Non è indicato il 'Identificativo Creditore' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If

      '----------------------
      'nome del file
      strExt = ".xml"
      If bPrefissaFileAbiConDitta Then
        strNomeFile83 = strDitta & "_Rid" & Format(lDistNum, "00000") & strExt
      Else
        strNomeFile83 = "Rid" & Format(lDistNum, "00000") & strExt
      End If
      strNomeFile = strPathFileAbi & strNomeFile83

      '----------------------
      'valuta
      If oApp.ValutaCorrente = "EUR" Then
        Select Case nValuta
          Case 0 'tutto ok
          Case Else
            strErr += vbCrLf & oApp.Tr(Me, 128939606793297019, _
                "Codice valuta non valido per generazione file RID." & vbCrLf & _
                "Codici valuta consentiti: '0'.")
            Return False
        End Select
      Else
        strErr += vbCrLf & oApp.Tr(Me, 128939606829391924, _
                "Codice valuta non valido per generazione file RID." & vbCrLf & _
                "Codici valuta consentiti: '0' o '100'.")
        Return False
      End If

      '----------------------
      'Controlla se già esiste il file
      If System.IO.File.Exists(strNomeFile) Then
        'evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128238690194733441, "Esiste già un file chiamato |" & UCase(strNomeFile) & "|. Sovrascriverlo?"))
        'ThrowRemoteEvent(evt)
        'If evt.RetValue = "NO" Then Return False
        System.IO.File.Delete(strNomeFile)
      End If

      '----------------------
      'determino il nome del flusso: max 35 caratteri. successivamente dovrò aggiungerne altri, per cui, per scrupolo, ne uso max 20
      strIDFile = strDitta.PadRight(10).Substring(0, 8).Trim & DateTime.Now.ToString("yyMMddHHmmss")

      '----------------------
      'inizio a scrivere il file
      sbFile.Append("<?xml version=""1.0"" encoding=""UTF-8""?>" & vbCrLf)
      sbFile.Append("<CBIBdySDDReq xsi:schemaLocation=""urn:CBI:xsd:CBIBdySDDReq.00.01.00 CBIBdySDDReq.00.01.00.xsd "" xmlns=""urn:CBI:xsd:CBIBdySDDReq.00.01.00"" xmlns:BODY=""urn:CBI:xsd:CBIBdySDDReq.00.01.00"" xmlns:RLM=""urn:CBI:xsd:CBISDDReqLogMsg.00.01.00"" xmlns:SGNT=""urn:CBI:xsd:CBISgnInf.001.04"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">" & vbCrLf)
      sbFile.Append("<BODY:PhyMsgInf>" & vbCrLf)
      'sbFile.Append("<BODY:PhyMsgTpCd>INC-SDDC-01</BODY:PhyMsgTpCd>" & vbCrLf)
      sbFile.Append("<BODY:PhyMsgTpCd>INC-SDD" & NTSCStr(dtrT(0)!sc_tiporid).ToUpper & "-01</BODY:PhyMsgTpCd>" & vbCrLf)
      sbFile.Append("<BODY:NbOfLogMsg>1</BODY:NbOfLogMsg>" & vbCrLf)
      sbFile.Append("</BODY:PhyMsgInf>" & vbCrLf)
      sbFile.Append("<BODY:CBIEnvelSDDReqLogMsg>" & vbCrLf)
      sbFile.Append("<BODY:CBISDDReqLogMsg>" & vbCrLf)

      '----------------------
      'SEZIONE GRUPPO
      sbFile.Append("<RLM:GrpHdr>" & vbCrLf)
      sbFile.Append(" <RLM:MsgId>" & strIDFile & "</RLM:MsgId>" & vbCrLf)  'identificativo univoco del flusso
      sbFile.Append(" <RLM:CreDtTm>" & DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss").Replace(".", ":") & "</RLM:CreDtTm>" & vbCrLf)  'data/ora creazione tipo 2013-04-13T14:40:06
      sbFile.Append(" <RLM:NbOfTxs>" & "°NUM_DISP°" & "</RLM:NbOfTxs>" & vbCrLf)  'numero delle disposizioni contenute nel file
      sbFile.Append(" <RLM:CtrlSum>" & "°TOT_DISP°" & "</RLM:CtrlSum>" & vbCrLf)  'somma degli importi di tutte le disposizioni
      sbFile.Append(" <RLM:InitgPty>" & vbCrLf)
      sbFile.Append("  <RLM:Nm>" & (ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azrags1)) & " " & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azrags2))).PadRight(70).Substring(0, 70).Trim & "</RLM:Nm>" & vbCrLf) 'nome del mittente (max 70 char)
      sbFile.Append("  <RLM:Id>" & vbCrLf)
      sbFile.Append("   <RLM:OrgId>" & vbCrLf)
      sbFile.Append("    <RLM:Othr>" & vbCrLf)
      sbFile.Append("     <RLM:Id>" & NTSCStr(dttTabbanc.Rows(0)!tb_codcuc).Trim & "</RLM:Id>" & vbCrLf)  'CUC banca mittente varchar(35)
      sbFile.Append("     <RLM:Issr>CBI</RLM:Issr>" & vbCrLf)  'valore fisso 'CBI'
      sbFile.Append("    </RLM:Othr>" & vbCrLf)
      sbFile.Append("   </RLM:OrgId>" & vbCrLf)
      sbFile.Append("  </RLM:Id>" & vbCrLf)
      sbFile.Append(" </RLM:InitgPty>" & vbCrLf)
      sbFile.Append("</RLM:GrpHdr>" & vbCrLf)

      'luppo su tutti i record di scaden, ordinando per sc_tiporid e sc_tiposeqrid (che servono per creare le varie <PmtInf>)
      For i = 0 To dtrT.Length - 1

        '----------------------
        'SEZIONE INFORMAZIONI SULLE DISPOSIZIONI
        'questa sezione deve essere ripetuta più volte se devo presentare insieme rid CORE e B2B e/o se ho sequenze di incasso diverse FRST/RCUR/FNAL
START_INIZIO_PmtInf:
        If NTSCStr(dtrT(i)!sc_tiporid) = strTipoRidOld And _
           NTSCStr(dtrT(i)!sc_tiposeqrid) = strTipoSeqRidOld And _
           NTSCDate(dtrT(i)!sc_datsca) = dtScadOld Then
          'non devo riscrivere la sezione <PmtInf>
          GoTo START_INIZIO_DrctDbtTxInf
        Else
          'chiudo il precedene tag 'PmtInf'
          If nPmtInf > 0 Then sbFile.Append("</RLM:PmtInf>" & vbCrLf)
        End If

        'la sezione è cambiata: devo sostituire i marcatori con i totali di sezione
        strTmp = sbFile.ToString
        sbFile = New System.Text.StringBuilder
        sbFile.Append(strTmp)

        strTipoRidOld = NTSCStr(dtrT(i)!sc_tiporid)
        strTipoSeqRidOld = NTSCStr(dtrT(i)!sc_tiposeqrid)
        dtScadOld = NTSCDate(dtrT(i)!sc_datsca)

        Select Case NTSCStr(dtrT(i)!sc_tiporid)
          Case " " 'no RID: caso non possibile
          Case "C" : strTipoRid = "CORE"
          Case "B" : strTipoRid = "B2B"
        End Select
        Select Case NTSCStr(dtrT(i)!sc_tiposeqrid)
          Case " " : strTipoSeqRid = "OOFF"
          Case "P" : strTipoSeqRid = "FRST"
          Case "R" : strTipoSeqRid = "RCUR"
          Case "U" : strTipoSeqRid = "FNAL"
        End Select

        nPmtInf += 1
        sbFile.Append("<RLM:PmtInf>" & vbCrLf)
        sbFile.Append(" <RLM:PmtInfId>" & strIDFile & "-" & nPmtInf.ToString & "</RLM:PmtInfId>" & vbCrLf)   'IDENTIFICATIVO UNIVOCO DEL FLUSSO DI ACCREDITO (VARCHAR(35)) PUO' ESSERE ANCHE UGUALE A <MsgId> CON IN CODA UN '-1'
        sbFile.Append(" <RLM:PmtMtd>DD</RLM:PmtMtd>" & vbCrLf)       'valore fisso 'DD'
        sbFile.Append(" <RLM:PmtTpInf>" & vbCrLf)
        sbFile.Append("  <RLM:SvcLvl>" & vbCrLf)
        sbFile.Append("   <RLM:Cd>SEPA</RLM:Cd>" & vbCrLf) 'VALORE FISSO 'SEPA'
        sbFile.Append("  </RLM:SvcLvl>" & vbCrLf)
        sbFile.Append("  <RLM:LclInstrm>" & vbCrLf)
        sbFile.Append("   <RLM:Cd>" & strTipoRid & "</RLM:Cd>" & vbCrLf)    'VALORE FISSO 'CORE' (senza apici) oppure 'COR1' oppure 'B2B'
        sbFile.Append("  </RLM:LclInstrm>" & vbCrLf)
        sbFile.Append("  <RLM:SeqTp>" & strTipoSeqRid & "</RLM:SeqTp>" & vbCrLf)   'Tipo sequenza/frequenza dell'incasso. Assume solo i seguenti valori: - FRST (prima di una serie di disposizioni) - RCUR  (l'autorizzazione viene utilizzata per una serie di incassi a scadenze regolari) - FNAL  (ultima di una serie di disposizioni)  - OOFF (singola non ripetuta)
        sbFile.Append("  <RLM:CtgyPurp>" & vbCrLf)
        sbFile.Append("   <RLM:Cd>OTHR</RLM:Cd>" & vbCrLf)
        sbFile.Append("  </RLM:CtgyPurp>" & vbCrLf)
        sbFile.Append(" </RLM:PmtTpInf>" & vbCrLf)
        sbFile.Append(" <RLM:ReqdColltnDt>" & NTSCDate(dtrT(i)!sc_datsca).ToString("yyyy-MM-dd") & "</RLM:ReqdColltnDt>" & vbCrLf)  'DATA SCADENZA RICHIESTA DAL MITTENTE: 2013-07-23
        sbFile.Append(" <RLM:Cdtr>" & vbCrLf)
        sbFile.Append("  <RLM:Nm>" & (ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azrags1)) & " " & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azrags2))).PadRight(70).Substring(0, 70).Trim & "</RLM:Nm>" & vbCrLf)    'CREDITORE TITOLARE DEL C/C/BENEFICIARIO (VARCHAR(70)): DITTA
        sbFile.Append("  <RLM:PstlAdr>" & vbCrLf)

        If bSepaIndirSplit Then
          sbFile.Append("   <RLM:AdrTp>ADDR</RLM:AdrTp>" & vbCrLf)
          sbFile.Append("   <RLM:StrtNm>" & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azindir)).Trim & "</RLM:StrtNm>" & vbCrLf)
          'sbFile.Append("   <RLM:BldgNb>" & Numero civico... & "</RLM:BldgNb>" & vbCrLf)
          sbFile.Append("   <RLM:PstCd>" & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azcap)).Trim & "</RLM:PstCd>" & vbCrLf)
          sbFile.Append("   <RLM:TwnNm>" & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azcitta)).Trim & "</RLM:TwnNm>" & vbCrLf)
          sbFile.Append("   <RLM:CtrySubDvsn>" & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azprov)).Trim & "</RLM:CtrySubDvsn>" & vbCrLf)
          If NTSCStr(dttAnaz.Rows(0)!tb_azstato) = "IT" Or NTSCStr(dttAnaz.Rows(0)!tb_azstato).Trim = "I" Or NTSCStr(dttAnaz.Rows(0)!tb_azstato).Trim = "" Then
            sbFile.Append("   <RLM:Ctry>IT</RLM:Ctry>" & vbCrLf)    'STATO CREDITORE
          ElseIf NTSCStr(dttAnaz.Rows(0)!tb_azstato) = "RSM" Then
            sbFile.Append("   <RLM:Ctry>SM</RLM:Ctry>" & vbCrLf)    'STATO CREDITORE
          Else
            sbFile.Append("   <RLM:Ctry>" & NTSCStr(dttAnaz.Rows(0)!tb_azstato) & "</RLM:Ctry>" & vbCrLf)    'STATO CREDITORE
          End If
        Else
          If NTSCStr(dttAnaz.Rows(0)!tb_azstato) = "IT" Or NTSCStr(dttAnaz.Rows(0)!tb_azstato).Trim = "I" Or NTSCStr(dttAnaz.Rows(0)!tb_azstato).Trim = "" Then
            sbFile.Append("   <RLM:Ctry>IT</RLM:Ctry>" & vbCrLf)    'STATO CREDITORE
          ElseIf NTSCStr(dttAnaz.Rows(0)!tb_azstato) = "RSM" Then
            sbFile.Append("   <RLM:Ctry>SM</RLM:Ctry>" & vbCrLf)    'STATO CREDITORE
          Else
            sbFile.Append("   <RLM:Ctry>" & NTSCStr(dttAnaz.Rows(0)!tb_azstato) & "</RLM:Ctry>" & vbCrLf)    'STATO CREDITORE
          End If
          sbFile.Append("   <RLM:AdrLine>" & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azindir)).Trim & "</RLM:AdrLine>" & vbCrLf) '1^ RIGA: INDIRIZZO CREDITORE (VARCHAR(70)): VIA DANTE, 1
          sbFile.Append("   <RLM:AdrLine>" & (NTSCStr(dttAnaz.Rows(0)!tb_azcap) & " " & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azcitta)) & " " & ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azprov))).PadRight(70).Substring(0, 70).Trim & "</RLM:AdrLine>" & vbCrLf) '2^ RIGA: CAP CITTA' CREDITORE (VARCHAR(70)): 47838 MONTEFIORE CONCA
        End If

        sbFile.Append("  </RLM:PstlAdr>" & vbCrLf)
        sbFile.Append(" </RLM:Cdtr>" & vbCrLf)
        sbFile.Append(" <RLM:CdtrAcct>" & vbCrLf)
        sbFile.Append("  <RLM:Id>" & vbCrLf)
        sbFile.Append("   <RLM:IBAN>" & strIBANOrdinante.Trim & "</RLM:IBAN>" & vbCrLf)    'IBAN CREDITORE
        sbFile.Append("  </RLM:Id>" & vbCrLf)
        sbFile.Append(" </RLM:CdtrAcct>" & vbCrLf)
        sbFile.Append(" <RLM:CdtrAgt>" & vbCrLf)
        sbFile.Append("  <RLM:FinInstnId>" & vbCrLf)
        sbFile.Append("   <RLM:ClrSysMmbId>" & vbCrLf)
        sbFile.Append("     <RLM:MmbId>" & NTSCInt(dttTabbanc.Rows(0)!tb_abi).ToString("00000") & "</RLM:MmbId>" & vbCrLf)  'CODICE ABI DELLA BANCA DEL BENEFICIARIO
        sbFile.Append("   </RLM:ClrSysMmbId>" & vbCrLf)
        sbFile.Append("  </RLM:FinInstnId>" & vbCrLf)
        sbFile.Append(" </RLM:CdtrAgt>" & vbCrLf)
        sbFile.Append(" <RLM:ChrgBr>SLEV</RLM:ChrgBr>" & vbCrLf) 'FISSO 'SLEV'
        sbFile.Append(" <RLM:CdtrSchmeId>" & vbCrLf)
        sbFile.Append("  <RLM:Id>" & vbCrLf)
        sbFile.Append("   <RLM:PrvtId>" & vbCrLf)
        sbFile.Append("    <RLM:Othr>" & vbCrLf)
        sbFile.Append("     <RLM:Id>" & NTSCStr(dttTabbanc.Rows(0)!tb_rididcred).Trim & "</RLM:Id>" & vbCrLf)   'identificativo creditore (tabbanc.tb_rididcred)
        sbFile.Append("     <RLM:SchmeNm>" & vbCrLf)
        sbFile.Append("      <RLM:Prtry>SEPA</RLM:Prtry>" & vbCrLf)    'VALORE FISSO 'SEPA'
        sbFile.Append("     </RLM:SchmeNm>" & vbCrLf)
        sbFile.Append("    </RLM:Othr>" & vbCrLf)
        sbFile.Append("   </RLM:PrvtId>" & vbCrLf)
        sbFile.Append("  </RLM:Id>" & vbCrLf)
        sbFile.Append(" </RLM:CdtrSchmeId>" & vbCrLf)

        '----------------------
        'SEZIONE INFORMAZIONI SULLE DISPOSIZIONI DI INCASSO
        'CHE DOVRANNO ESSERE RIPETUTE PER TUTTI I RECORD DI SCADEN CONTENUTI TRA I TAG <DrctDbtTxInf> e </DrctDbtTxInf>

START_INIZIO_DrctDbtTxInf:
        If (dtrT(i)!sc_tippaga.ToString = "1" Or dtrT(i)!sc_tippaga.ToString = "2") And dtrT(i)!sc_darave.ToString = "D" Then
          oCldComm.ValCodiceDb(dtrT(i)!sc_controp.ToString, strDitta, "ANAGRA", "N", "", dttControp)
        Else
          oCldComm.ValCodiceDb(dtrT(i)!sc_conto.ToString, strDitta, "ANAGRA", "N", "", dttControp)
        End If
        If dttControp.Rows.Count > 0 Then
          strDescr1 = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_descr1))
          strDescr2 = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_descr2))
          strIndir = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_indir))
          strCap = NTSCStr(dttControp.Rows(0)!an_cap)
          strCitta = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_citta))
          strProv = NTSCStr(dttControp.Rows(0)!an_prov)
          strStato = NTSCStr(dttControp.Rows(0)!an_stato)
          If NTSCInt(dtrT(i)!sc_coddest) > 0 Then
            If (dtrT(i)!sc_tippaga.ToString = "1" Or dtrT(i)!sc_tippaga.ToString = "2") And dtrT(i)!sc_darave.ToString = "D" Then
              oCldComm.ValCodiceDb(dtrT(i)!sc_coddest.ToString, strDitta, "DESTDIV", "N", "", dttDestdiv, dtrT(i)!sc_controp.ToString)
            Else
              oCldComm.ValCodiceDb(dtrT(i)!sc_coddest.ToString, strDitta, "DESTDIV", "N", "", dttDestdiv, dtrT(i)!sc_conto.ToString)
            End If
            If dttDestdiv.Rows.Count > 0 Then
              strDescr1 = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_nomdest))
              strDescr2 = " "
              strIndir = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_inddest))
              strCap = NTSCStr(dttDestdiv.Rows(0)!dd_capdest)
              strCitta = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_locdest))
              strProv = NTSCStr(dttDestdiv.Rows(0)!dd_prodest)
              strStato = NTSCStr(dttDestdiv.Rows(0)!dd_stato)
            End If
          End If
        Else
          strDescr1 = ""
          strDescr2 = ""
          strIndir = ""
          strCap = ""
          strCitta = ""
          strProv = ""
          strStato = ""
        End If

        If NTSCStr(dtrT(i)!sc_idmandrid).Trim = "" Then
          strErr += vbCrLf & oApp.Tr(Me, 130198401314537523, "Attenzione: il Mandato RID sul Conto/controp. '|" & dtrT(i)!sc_conto.ToString() & "/" & dtrT(i)!sc_controp.ToString & "|' non è stato impostato. Pertanto il file generato non sarà corretto.")
        End If
        If NTSCDate(dtrT(i)!sc_dtmandrid) > DateTime.Now Then
          strErr += vbCrLf & oApp.Tr(Me, 130198401960205895, "Attenzione: la data inizio Mandato RID sul Conto/controp. '|" & dtrT(i)!sc_conto.ToString() & "/" & dtrT(i)!sc_controp.ToString & "|' è successiva alla data odierna.")
        End If
        Select Case strStato.Trim.ToUpper
          Case "", "I" : strStato = "IT"
        End Select
        If strStato <> "IT" Then
          oCldComm.ValCodiceDb(strStato, strDittaCorrente, "TABSTAT", "S", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            If NTSCStr(dttTmp.Rows(0)!tb_siglacee).ToUpper.Trim <> "" Then
              strStato = NTSCStr(dttTmp.Rows(0)!tb_siglacee).ToUpper
            End If
          End If
          dttTmp.Clear()
        End If
        If strStato = "RSM" Then strStato = "SM"
        If strStato.Trim = "" Then strStato = "IT"

        '------------------------------
        'ottengo l'IBAN debitore
        If strStato.ToUpper <> "IT" And strStato.ToUpper <> "SM" Then
          strIBANDebitore = NTSCStr(dtrT(i)!sc_iban)
          If Trim(strIBANDebitore) = "" Then
            strErr += vbCrLf & oApp.Tr(Me, 130221483605934265, _
                  "Attenzione: codice IBAN debitore (ossia il campo 'IBAN estero') non indicato per la scadenza: |" & dtrT(i)!sc_conto.ToString & "| - |" & _
                  dtrT(i)!sc_annpar.ToString & "| - |" & dtrT(i)!sc_alfpar.ToString & "| - |" & dtrT(i)!sc_numpar.ToString & "| - |" & dtrT(i)!sc_numrata.ToString & _
                  "|.Si procedere comunque.")
          End If

          If NTSCStr(dtrT(i)!sc_swift).Trim = "" And strStato.Trim <> "" And strStato.Trim <> "IT" Then
            strErr += vbCrLf & oApp.Tr(Me, 130378794174829212, _
                    "Attenzione: codice 'BIC/SWIFT' debitore estero non indicato per la scadenza: |" & dtrT(i)!sc_conto.ToString & "| - |" & _
                    dtrT(i)!sc_annpar.ToString & "| - |" & dtrT(i)!sc_alfpar.ToString & "| - |" & dtrT(i)!sc_numpar.ToString & "| - |" & dtrT(i)!sc_numrata.ToString & _
                    "|.Si procedere comunque.")
          End If
        Else
          '----------------------------
          'Rileva l'IBAN del debitore ITALIA E SAN MARINO
          If NTSCInt(dtrT(i)!sc_abi) = 0 And NTSCInt(dtrT(i)!sc_cab) = 0 And NTSCStr(dtrT(i)!sc_iban).Trim <> "" Then
            'il cliente italiano/rsm usa una banca estera: prendo l'iban estero
            strIBANDebitore = NTSCStr(dtrT(i)!sc_iban)
          Else
            'costriusco l'iban con i dati italia
            If NTSCInt(dtrT(i)!sc_abi) = 0 Or NTSCInt(dtrT(i)!sc_cab) = 0 Then
              If NTSCStr(dtrT(i)!sc_iban).Trim <> "" Then
                strErr += vbCrLf & oApp.Tr(Me, 130662454384884621, _
                                        "Attenzione! Codice ABI e/o CAB mancante MA indicato il codice IBAM estero per la scadenza al |" & NTSCDate(dtrT(i)!sc_datsca).ToShortDateString & _
                                        "| di conto/controp. |" & dtrT(i)!sc_conto.ToString & "/" & dtrT(i)!sc_controp.ToString & "|. Impostare correttamente ABI e CAB, oppure azzerarli per usare l'IBAN estero.")
              Else
                strErr += vbCrLf & oApp.Tr(Me, 130221486246273818, _
                                        "Attenzione! Codice ABI e/o CAB mancante per la scadenza al |" & NTSCDate(dtrT(i)!sc_datsca).ToShortDateString & _
                                        "| di conto/controp. |" & dtrT(i)!sc_conto.ToString & "/" & dtrT(i)!sc_controp.ToString & "|.")
              End If
            End If
            '----------------------------
            'Rileva C/C e CIN del debitore
            strCINDebitore = Trim(NTSCStr(dtrT(i)!sc_cin))
            strCCDebitore = UCase(NTSCStr(dtrT(i)!sc_numcc)).Trim

            If InStr(strCCDebitore, "#") > 0 Then 'per compatibilità
              If InStr(strCCDebitore, "#") < Len(strCCDebitore) Then
                'Se il CIN presente nel campo sc_cin non è valido lo prende da sc_numcc (per compatibilità all'indietro)
                If (Not strCINDebitore Like "[A-Z]") Or strCCDebitore = "" Then
                  strCINDebitore = Mid(strCCDebitore, InStr(strCCDebitore, "#") + 1, 1)
                End If
              End If
              'toglie dal C/c in cin e il prefiban
              If InStr(strCCDebitore, "#") > 1 Then
                strCCDebitore = Left(strCCDebitore, InStr(strCCDebitore, "#") - 1)
              Else
                strCCDebitore = ""
              End If
            End If
            'Controlla correttezza c/c e CIN
            If strCCDebitore = "" Or Len(strCCDebitore) > 12 Then
              strErr += vbCrLf & oApp.Tr(Me, 130221486319768546, _
                        "Attenzione! numero conto corrente non corretto |'" & NTSCStr(dtrT(i)!sc_numcc) & _
                        "'| per la scadenza al |" & NTSCStr(dtrT(i)!sc_datsca) & "| di conto/controp. |" & _
                        dtrT(i)!sc_conto.ToString & "/" & dtrT(i)!sc_controp.ToString & "|." & vbCrLf & vbCrLf & _
                        "Non rispetta la convenzione adottata: max 12 caratteri per il num. conto corrente.")
            End If
            If (Not strCINDebitore Like "[A-Z]") Then
              strErr += vbCrLf & oApp.Tr(Me, 130221486385035415, _
                        "Attenzione! il carattere di controllo CIN deve essere un carattere alfabetico A-Z per la " & _
                        "scadenza al |" & NTSCStr(dtrT(i)!sc_datsca) & "| di conto/controp. |" & _
                        dtrT(i)!sc_conto.ToString & "/" & dtrT(i)!sc_controp.ToString & "|.")
            End If
            'Rileva il prefisso IBAN
            strIBANDebitore = Trim(NTSCStr(dtrT(i)!sc_prefiban)).ToUpper
            If strIBANDebitore = "" Then
              strErr += vbCrLf & oApp.Tr(Me, 130221486468750523, _
                        "Attenzione! Non è indicato il 'prefisso IBAN' per la scadenza al |" & NTSCStr(dtrT(i)!sc_datsca) & _
                        "| di conto/controp. |" & dtrT(i)!sc_conto.ToString & "/" & dtrT(i)!sc_controp.ToString & "|.")
            End If
            If Len(strIBANDebitore) <> 4 Then
              strErr += vbCrLf & oApp.Tr(Me, 130221486484669295, _
                        "Attenzione! Il 'prefisso IBAN', per la scadenza al |" & NTSCStr(dtrT(i)!sc_datsca) & _
                        "| di conto/controp. |" & dtrT(i)!sc_conto.ToString & "/" & dtrT(i)!sc_controp.ToString & "| deve essere di 4 caratteri")
            End If
            If Left(strIBANDebitore, 2) <> "IT" And Left(strIBANDebitore, 2) <> "SM" Then
              strErr += vbCrLf & oApp.Tr(Me, 130221486573931767, _
                        "Attenzione! Il 'prefisso IBAN', per la scadenza al |" & NTSCStr(dtrT(i)!sc_datsca) & _
                        "| di conto/controp. |" & dtrT(i)!sc_conto.ToString & "/" & dtrT(i)!sc_controp.ToString & "|, " & _
                        "deve iniziare con i caratteri IT o SM.")
            End If
            strIBANDebitore = strIBANDebitore & strCINDebitore & Right("00000" & NTSCInt(dtrT(i)!sc_abi), 5) & _
                                  Right("00000" & NTSCInt(dtrT(i)!sc_cab), 5) & Right("000000000000" & strCCDebitore, 12)
          End If    'If NTSCInt(dtrT(i)!sc_abi) = 0 And NTSCInt(dtrT(i)!sc_cab) = 0 And NTSCStr(dtrT(i)!sc_iban).Trim <> "" Then

        End If    'If strStato.ToUpper <> "IT" And strStato.ToUpper <> "SM" Then

        '-----------------------------
        'determino i riferimenti
        If (IIf(Trim(NTSCStr(dtrT(i)!sc_alfdoc)) = "", " ", Left(dtrT(i)!sc_alfdoc.ToString, 1))).ToString.ToUpper = strRiapriPartiteRaggPartita.ToUpper Then
          If bStampaRiferimentiFatture Then
            strRiferim = NTSCStr(dtrT(i)!sc_descr)
          Else
            strRiferim = strRifEstConto & CStr(dtrT(i)!sc_numdoc) & "/" & strRiapriPartiteRaggPartita & " DEL " & NTSCDate(dtrT(i)!sc_datdoc).ToString("dd/MM/yy")
          End If
        Else
          'se sono stato chiamato da bscgdist devo scrivere anche il cliente (in sc_conto c'è il conto effetti)
          If NTSCInt(dtrT(i)!sc_controp) <> 0 Then
            'dist
            strRiferim = strRifNSFatt & CStr(dtrT(i)!sc_numdoc) & IIf(Trim(NTSCStr(dtrT(i)!sc_alfdoc)) = "", "  ", "/" & Left(dtrT(i)!sc_alfdoc.ToString, 1)).ToString & " DEL " & NTSCDate(dtrT(i)!sc_datdoc).ToString("dd/MM/yy") & " C:" & NTSCStr(dtrT(i)!sc_controp)
          Else
            'auto
            strRiferim = strRifNSFatt & CStr(dtrT(i)!sc_numdoc) & IIf(Trim(NTSCStr(dtrT(i)!sc_alfdoc)) = "", "  ", "/" & Left(dtrT(i)!sc_alfdoc.ToString, 1)).ToString & " DEL " & NTSCDate(dtrT(i)!sc_datdoc).ToString("dd/MM/yy")
          End If
        End If

        If NTSCStr(dtrT(i)!sc_cup).Trim <> "" Then strRiferim += " " & oApp.Tr(Me, 130540426108281220, "CUP:") & NTSCStr(dtrT(i)!sc_cup).Trim
        If NTSCStr(dtrT(i)!sc_cig).Trim <> "" Then strRiferim += " " & oApp.Tr(Me, 130540426286883070, "CIG:") & NTSCStr(dtrT(i)!sc_cig).Trim

        strRiferim = ConvStrFileAbi(strRiferim)
        strRiferim = strRiferim.PadRight(140).Substring(0, 140).Trim

        lProgrScad += 1
        lRigheTot += 1
        dTotScad += NTSCDec(dtrT(i)!sc_importo)
        sbFile.Append(" <RLM:DrctDbtTxInf>" & vbCrLf)
        sbFile.Append("  <RLM:PmtId>" & vbCrLf)
        sbFile.Append("   <RLM:InstrId>" & strIDFile & "-" & lProgrScad.ToString & "</RLM:InstrId>" & vbCrLf) '>IDENTIFICATIVO UNIVOCO DEL FLUSSO DI ADDEBITO (VARCHAR(35))
        sbFile.Append("   <RLM:EndToEndId>" & strIDFile & "-" & nPmtInf.ToString & "-" & lProgrScad.ToString & "</RLM:EndToEndId>" & vbCrLf) '>IDENTIFICATIVO UNIVOCO DEL FLUSSO DI ACCREDITO (VARCHAR(35))
        sbFile.Append("  </RLM:PmtId>" & vbCrLf)
        sbFile.Append("  <RLM:InstdAmt Ccy=""EUR"">" & NTSCDec(dtrT(i)!sc_importo).ToString("0.00").Replace(",", ".") & "</RLM:InstdAmt>" & vbCrLf) 'IMPORTO SCADENZA
        sbFile.Append("  <RLM:DrctDbtTx>" & vbCrLf)
        sbFile.Append("   <RLM:MndtRltdInf>" & vbCrLf)
        sbFile.Append("    <RLM:MndtId>" & NTSCStr(dtrT(i)!sc_idmandrid).Trim & "</RLM:MndtId>" & vbCrLf) 'Identificativo assegnato al mandato sottoscritto dal debitore. VARCHAR(35)
        sbFile.Append("    <RLM:DtOfSgntr>" & NTSCDate(dtrT(i)!sc_dtmandrid).ToString("yyyy-MM-dd") & "</RLM:DtOfSgntr>" & vbCrLf) 'DATA SCADENZA MANDATO: 2012-11-11
        sbFile.Append("    <RLM:AmdmntInd>false</RLM:AmdmntInd>" & vbCrLf) 'FISSO 'false'
        sbFile.Append("   </RLM:MndtRltdInf>" & vbCrLf)
        sbFile.Append("  </RLM:DrctDbtTx>" & vbCrLf)
        If strIBANDebitore.ToUpper.StartsWith("IT") = False And strIBANDebitore.ToUpper.StartsWith("SM") = False Then
          'obbligatorio solo iban che non iniziano per IT o SM
          sbFile.Append("  <RLM:DbtrAgt>" & vbCrLf)
          sbFile.Append("   <RLM:FinInstnId>" & vbCrLf)
          sbFile.Append("    <RLM:BIC>" & NTSCStr(dtrT(i)!sc_swift).Trim & "</RLM:BIC>" & vbCrLf) 'BIC BANCA DEBITORE
          sbFile.Append("   </RLM:FinInstnId>" & vbCrLf)
          sbFile.Append("  </RLM:DbtrAgt>" & vbCrLf)
        End If
        sbFile.Append("  <RLM:Dbtr>" & vbCrLf)
        sbFile.Append("   <RLM:Nm>" & (strDescr1 & " " & strDescr2).PadRight(70).Substring(0, 70).Trim & "</RLM:Nm>" & vbCrLf) 'DESCRIZIONE DEBITORE VARCHAR (70)
        sbFile.Append("   <RLM:PstlAdr>" & vbCrLf)

        If bSepaIndirSplit Then
          sbFile.Append("    <RLM:AdrTp>ADDR</RLM:AdrTp>" & vbCrLf)
          sbFile.Append("    <RLM:StrtNm>" & strIndir.Trim & "</RLM:StrtNm>" & vbCrLf)
          'sbFile.Append("    <RLM:BldgNb>" & Numero civico... & "</RLM:BldgNb>" & vbCrLf)
          sbFile.Append("    <RLM:PstCd>" & strCap.Trim & "</RLM:PstCd>" & vbCrLf)
          sbFile.Append("    <RLM:TwnNm>" & strCitta.Trim & "</RLM:TwnNm>" & vbCrLf)
          sbFile.Append("    <RLM:CtrySubDvsn>" & strProv.Trim & "</RLM:CtrySubDvsn>" & vbCrLf)
          sbFile.Append("    <RLM:Ctry>" & strStato & "</RLM:Ctry>" & vbCrLf) 'STATO DEBITORE: IT
        Else
          sbFile.Append("    <RLM:Ctry>" & strStato & "</RLM:Ctry>" & vbCrLf) 'STATO DEBITORE: IT
          sbFile.Append("    <RLM:AdrLine>" & strIndir.Trim & "</RLM:AdrLine>" & vbCrLf) '1^ RIGA: INDIRIZZO CREDITORE (VARCHAR(70)): VIA DANTE, 1
          sbFile.Append("    <RLM:AdrLine>" & (strCap & " " & strCitta & " " & strProv).PadRight(70).Substring(0, 70).Trim & "</RLM:AdrLine>" & vbCrLf) '2^ RIGA: CAP CITTA' CREDITORE (VARCHAR(70)): 47838 MONTEFIORE CONCA
        End If

        sbFile.Append("   </RLM:PstlAdr>" & vbCrLf)
        sbFile.Append("  </RLM:Dbtr>" & vbCrLf)
        sbFile.Append("  <RLM:DbtrAcct>" & vbCrLf)
        sbFile.Append("   <RLM:Id>" & vbCrLf)
        sbFile.Append("    <RLM:IBAN>" & strIBANDebitore.Trim & "</RLM:IBAN>" & vbCrLf)  'IBAN DEBITORE
        sbFile.Append("   </RLM:Id>" & vbCrLf)
        sbFile.Append("  </RLM:DbtrAcct>" & vbCrLf)
        sbFile.Append("  <RLM:Purp>" & vbCrLf)
        sbFile.Append("    <RLM:Cd>" & strCausale & "</RLM:Cd>" & vbCrLf) 'CAUSALE DELLA TRANSAZIONE CASH=Transaction is a general cash management instruction. ACCT=Transaction moves funds between 2 accounts of same account holder at the same bank.
        sbFile.Append("  </RLM:Purp>" & vbCrLf)
        sbFile.Append("  <RLM:RmtInf>" & vbCrLf)
        sbFile.Append("   <RLM:Ustrd>" & strRiferim & "</RLM:Ustrd>" & vbCrLf)
        sbFile.Append("  </RLM:RmtInf>" & vbCrLf)
        sbFile.Append(" </RLM:DrctDbtTxInf>" & vbCrLf)
      Next    'For i = 0 To dtrT.Length - 1

      'chiudo l'ultimo tag 'PmtInf'
      sbFile.Append("</RLM:PmtInf>" & vbCrLf)

      sbFile.Append("</BODY:CBISDDReqLogMsg>" & vbCrLf)
      sbFile.Append("</BODY:CBIEnvelSDDReqLogMsg>" & vbCrLf)
      sbFile.Append("</CBIBdySDDReq>" & vbCrLf)

      '----------------------
      'sostituisco nel file i marcatori con il num. tot delle disposizioni e gli importi
      strTmp = sbFile.ToString
      strTmp = strTmp.Replace("°NUM_DISP°", lRigheTot.ToString)
      strTmp = strTmp.Replace("°TOT_DISP°", dTotScad.ToString("0.00").Replace(",", "."))

      '----------------------
      'salvo il file
      w1 = New StreamWriter(strNomeFile, False)
      w1.Write(strTmp)
      w1.Flush()
      w1.Close()

      '----------------------
      'se devo copio il file anche nella dir alternativa
      strExt = oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "FileABICBICopiaIn", "", " ", "").Trim
      If strExt <> "" Then
        Try
          File.Copy(strNomeFile, (strExt & "\" & strNomeFile83).Replace("\\", "\"), True)
          strNomeFile += vbCrLf & "e copiato in " & (strExt & "\" & strNomeFile83).Replace("\\", "\")
        Catch ex As Exception
          'ignoro l'errore
        End Try
      End If

      Return True
    Catch ex As Exception
      If Not w1 Is Nothing Then w1.Close()
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttScaden.Clear()
      dttAnaz.Clear()
      dttTabbanc.Clear()
    End Try
  End Function
  Public Overridable Function GenMav(ByVal strDitta As String, ByVal strOperat As String, ByVal strDtdist As String, _
                                     ByVal strDtvalbon As String, ByRef strNomeFile As String, _
                                     ByVal strTipoScadenze As String, ByVal strTippaga As String, _
                                     ByVal nDistAnno As Integer, ByVal lDistNum As Integer, _
                                     ByVal strPnint As String, ByVal bPrefissaFileAbiConDitta As Boolean, _
                                     ByVal bGestMav As Boolean, ByVal bGestMavExt As Boolean, _
                                     ByVal strPathFileAbi As String, ByRef strErr As String) As Boolean
    '-------------------------------
    'generazione file ABI RIBA
    Dim dttAnaz As New DataTable
    Dim dttTabbanc As New DataTable
    Dim dttScaden As New DataTable
    Dim dttControp As New DataTable
    Dim dttDestdiv As New DataTable
    Dim dttMM As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim strNomeFile83 As String = ""
    Dim strIdValuta As String = ""
    Dim evt As NTSEventArgs = Nothing
    Dim w1 As StreamWriter = Nothing
    Dim strPR As String = ""
    Dim strNomeSup As String = ""
    Dim lProgrScad As Integer = 0     'Contatore N° RIBA generata
    Dim lRigheTot As Integer = 0      'Contatore righe sritte
    Dim dTotScad As Decimal = 0       'Progressivo valore scadenze
    Dim dImporto As Decimal = 0
    Dim strRiga As String = ""

    Dim strDescr1 As String = ""
    Dim strDescr2 As String = ""
    Dim strIndir As String = ""
    Dim strCap As String = ""
    Dim strCitta As String = ""
    Dim strProv As String = ""
    Dim strStato As String = ""
    Dim strPICF As String = ""
    Dim strExt As String = ".asc"
    Dim strRiapriPartiteRaggPartita As String = ""

    Dim bFileCBInumdoc9Char As Boolean = CBool(oCldComm.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "FileCBInumdoc9Char", "0", " ", "0"))
    Dim nNumChar As Integer = 6
    Dim strRifEstConto As String = "RF.ES.CONTO N. "
    Dim strRifNSFatt As String = "RIF.NS.FATT.N. "
    Dim bCaratteriCodaRecordsIM_EF As Boolean = CBool(oCldComm.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "CaratteriCodaRecordsIM_EF", "0", " ", "0"))

    Try
      '--------------------------------------------------------------------------------------------------------------
      strRiapriPartiteRaggPartita = oCldComm.GetSettingBusDitt(strDitta, "BSCGINSO", "OPZIONI", ".", "RiapriPartiteRaggPartita", "X", " ", "X")
      '--------------------------------------------------------------------------------------------------------------
      If Not IsDate(strDtdist) Then
        strErr += vbCrLf & oApp.Tr(Me, 128939606989240789, "Data distinta obbligatoria per la generazione dei file RID")
        Return False
      End If

      If Not (strTippaga = "4" And strTipoScadenze = "A" And bGestMav = True) Then
        strErr += vbCrLf & oApp.Tr(Me, 128238821578811172, "Generazione file M.AV. prevista solo su clienti, per scadenze di tipo 'Contanti' e con apposita opzione di registro di Business abilitata.")
        Return False
      End If

      oCldComm.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttAnaz)

      '------------------------------------
      'ottengo i dati
      If Not oCldComm.GetDataScadenDistinta(strDitta, strTippaga, nDistAnno, lDistNum, strPnint, False, dttScaden) Then
        Return False
      End If
      dtrT = dttScaden.Select()
      If dtrT.Length = 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 128939605979364724, "Nessuna scadenza autorizzata")
        Return False
      End If
      If dtrT.Length > 0 Then
        oCldComm.ValCodiceDb(dtrT(0)!sc_codbanc.ToString, strDitta, "TABBANC", "N", "", dttTabbanc)
      End If

      '----------------------
      'valuta
      If oApp.ValutaCorrente <> "EUR" Or nValuta <> 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 128238828598076790, "E' possibile emettere M.AV. solo su aziende con valuta di conto = EURO e su scadenze in valuta 0")
        Return False
      End If

      '----------------------
      'nome del file
      strExt = oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "FileABICBIEstensioneFile", ".asc", " ", ".asc")
      If bPrefissaFileAbiConDitta Then
        strNomeFile83 = strDitta & "_Mav" & Format(lDistNum, "00000") & strExt
      Else
        strNomeFile83 = "Mav" & Format(lDistNum, "00000") & strExt
      End If
      strNomeFile = strPathFileAbi & strNomeFile83

      '----------------------
      'Controlla se già esiste il file
      If System.IO.File.Exists(strNomeFile) Then
        'evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128238690194733441, "Esiste già un file chiamato |" & UCase(strNomeFile) & "|. Sovrascriverlo?"))
        'ThrowRemoteEvent(evt)
        'If evt.RetValue = "NO" Then Return False
        System.IO.File.Delete(strNomeFile)
      End If

      w1 = New StreamWriter(strNomeFile, False)
      '----------------------
      'Scrive la testata
      strPR = " IM"
      strNomeSup = Left(nDistAnno.ToString & lDistNum.ToString & Space(20), 20)
      w1.WriteLine(strPR & Left(NTSCStr(dttTabbanc.Rows(0)!tb_rifriba) & "     ", 5) & _
                Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_abi), 5) & _
                NTSCDate(strDtdist).ToString("ddMMyy") & strNomeSup & Space(74) & "E " & _
                IIf(bCaratteriCodaRecordsIM_EF = True, "".PadLeft(5), "".PadLeft(5, "0"c)).ToString)
      lRigheTot = 1
      lProgrScad = 1
      dTotScad = 0

      '----------------------
      'scrivo il corpo
      For i = 0 To dtrT.Length - 1
        With dtrT(i)

          dImporto = (NTSCDec(!sc_importoda) * 100)

          ' decod. conto per tirare su il cod. fisc. o partita iva
          If (!sc_tippaga.ToString = "1" Or !sc_tippaga.ToString = "2") And !sc_darave.ToString = "D" Then
            oCldComm.ValCodiceDb(!sc_controp.ToString, strDitta, "ANAGRA", "N", "", dttControp)
          Else
            oCldComm.ValCodiceDb(!sc_conto.ToString, strDitta, "ANAGRA", "N", "", dttControp)
          End If
          If dttControp.Rows.Count > 0 Then
            strDescr1 = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_descr1))
            strDescr2 = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_descr2))
            strIndir = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_indir))
            strCap = NTSCStr(dttControp.Rows(0)!an_cap)
            strCitta = ConvStrFileAbi(NTSCStr(dttControp.Rows(0)!an_citta))
            strProv = NTSCStr(dttControp.Rows(0)!an_prov)
            strStato = NTSCStr(dttControp.Rows(0)!an_stato)
            If NTSCStr(dttControp.Rows(0)!an_pariva) <> "" Then
              strPICF = NTSCStr(dttControp.Rows(0)!an_pariva)
            Else
              strPICF = NTSCStr(dttControp.Rows(0)!an_codfis)
            End If
            If NTSCInt(!sc_coddest) > 0 Then
              If (!sc_tippaga.ToString = "1" Or !sc_tippaga.ToString = "2") And !sc_darave.ToString = "D" Then
                oCldComm.ValCodiceDb(!sc_coddest.ToString, strDitta, "DESTDIV", "N", "", dttDestdiv, !sc_controp.ToString)
              Else
                oCldComm.ValCodiceDb(!sc_coddest.ToString, strDitta, "DESTDIV", "N", "", dttDestdiv, !sc_conto.ToString)
              End If
              If dttDestdiv.Rows.Count > 0 Then
                strDescr1 = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_nomdest))
                strDescr2 = " "
                strIndir = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_inddest))
                strCap = NTSCStr(dttDestdiv.Rows(0)!dd_capdest)
                strCitta = ConvStrFileAbi(NTSCStr(dttDestdiv.Rows(0)!dd_locdest))
                strProv = NTSCStr(dttDestdiv.Rows(0)!dd_prodest)
                strStato = " "
              End If
            End If
          Else
            strDescr1 = ""
            strDescr2 = ""
            strIndir = ""
            strCap = ""
            strCitta = ""
            strProv = ""
            strStato = ""
            strPICF = ""
          End If

          'riga 14
          dImporto = NTSCDec(!sc_importoda) * 100
          strRiga = " 14" & Format(lProgrScad, "0000000") & Space(12) & NTSCDate(!sc_datsca).ToString("ddMMyy") & "07000"
          strRiga = strRiga & Format(dImporto, "0000000000000") & "-" & Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_abi), 5)
          strRiga = strRiga & Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_cab), 5) & Left(dttTabbanc.Rows(0)!tb_csbf.ToString & "".PadLeft(12), 12)
          strRiga = strRiga & Space(22) & Left(dttTabbanc.Rows(0)!tb_rifriba.ToString & "     ", 5) & "4" & Left(!sc_conto.ToString & Space(16), 16)
          strRiga = strRiga & Space(6) & "E"
          lRigheTot = lRigheTot + 1
          w1.WriteLine(strRiga)

          'riga 20
          strRiga = " 20" & Format(lProgrScad, "0000000") & Left(ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azrags1)) & "".PadLeft(24), 24)
          strRiga = strRiga & Left(ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azrags2)) & "".PadLeft(24), 24) & Left(ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azindir)) & "".PadLeft(24), 24)
          strRiga = strRiga & Left(ConvStrFileAbi(NTSCStr(dttAnaz.Rows(0)!tb_azcitta)) & "".PadLeft(24), 24) & "".PadLeft(14)
          lRigheTot = lRigheTot + 1
          w1.WriteLine(strRiga)

          'riga 30
          If bGestMavExt = False Then
            strRiga = " 30" & Format(lProgrScad, "0000000") & Left(NTSCStr(strDescr1) & "".PadLeft(30), 30)
            strRiga = strRiga & Left(NTSCStr(strDescr2) & "".PadLeft(30), 30)
            strRiga = strRiga & Left(NTSCStr(strPICF) & "".PadLeft(16), 16)
            strRiga = strRiga & "".PadLeft(34)
          Else
            strRiga = " 30" & Format(lProgrScad, "0000000") & Left(NTSCStr(strDescr2) & "".PadLeft(30), 30)
            strRiga = strRiga & "".PadLeft(80)
          End If
          lRigheTot = lRigheTot + 1
          w1.WriteLine(strRiga)

          'riga 40
          strRiga = " 40" & Format(lProgrScad, "0000000") & Left(NTSCStr(strIndir) & "".PadLeft(30), 30)
          strCap = strCap.PadLeft(5, "0"c).Substring(0, 5)
          strRiga = strRiga & Format(Val(NTSCStr(strCap)), "00000") & Left(NTSCStr(strCitta) & "".PadLeft(23), 23)
          strRiga = strRiga & Left(NTSCStr(strProv) & "".PadLeft(2), 2) & Space(50)
          lRigheTot = lRigheTot + 1
          w1.WriteLine(strRiga)

          'riga 51
          strRiga = " 51" & Format(lProgrScad, "0000000") & "0000000000" & Space(54)
          strRiga = strRiga & "000000000000" & Space(34)
          lRigheTot = lRigheTot + 1
          w1.WriteLine(strRiga)

          'riga 59
          strRiga = " 59" & Format(lProgrScad, "0000000")
          If bGestMavExt = False Then
            If (IIf(Trim(NTSCStr(!sc_alfdoc)) = "", " ", Left(!sc_alfdoc.ToString, 1))).ToString.ToUpper = strRiapriPartiteRaggPartita.ToUpper Then
              strRiga = strRiga & strRifEstConto & Right("".PadLeft(nNumChar) & CStr(!sc_numdoc), nNumChar) & "/" & strRiapriPartiteRaggPartita & " DEL " & NTSCDate(!sc_datdoc).ToString("dd/MM/yy") & Space(16)
            Else
              strRiga = strRiga & strRifNSFatt & Right("".PadLeft(nNumChar) & CStr(!sc_numdoc), nNumChar) & IIf(Trim(NTSCStr(!sc_alfdoc)) = "", "  ", "/" & Left(!sc_alfdoc.ToString, 1)).ToString & " DEL " & NTSCDate(!sc_datdoc).ToString("dd/MM/yy") & Space(16)
            End If
          Else
            strRiga = strRiga & Left(strDescr1 & Space(55), 55)
          End If
          strRiga = strRiga & Right(Format(!sc_conto, "000000000"), 9) & !sc_annpar.ToString & !sc_alfpar.ToString & Right(Format(!sc_numpar, "000000"), 6) & Right("00" & !sc_numrata.ToString, 2) & Space(33)
          lRigheTot = lRigheTot + 1
          w1.WriteLine(strRiga)

          'ancora riga 59
          If bGestMavExt = False Then
            If Trim(NTSCStr(!sc_descr)) <> "" Then
              strRiga = " 59" & Format(lProgrScad, "0000000")
              strRiga = strRiga & Left(ConvStrFileAbi(NTSCStr(!sc_descr)) & "".PadLeft(55), 55) & Space(55)
              lRigheTot = lRigheTot + 1
              w1.WriteLine(strRiga)
            End If
          Else
            strRiga = " 59" & Format(lProgrScad, "0000000")
            strRiga = strRiga & Left("RIF.N° " & CStr(!sc_numdoc) & Space(110), 110)
            lRigheTot = lRigheTot + 1
            w1.WriteLine(strRiga)
            If oCldComm.GenMavGetMovmag(strDitta, NTSCDate(!sc_datdoc).Year, !sc_alfdoc.ToString, NTSCInt(!sc_numdoc), dttMM) Then
              For l = 0 To dttMM.Rows.Count - 1
                strRiga = " 59" & Format(lProgrScad, "0000000")
                strRiga = strRiga & Left(Left(dttMM.Rows(l)!mm_descr.ToString & Space(40), 40) & " " & _
                          Left(dttMM.Rows(l)!mm_ump.ToString & Space(3), 3) & " " & _
                          dttMM.Rows(l)!mm_quant.ToString & " " & _
                          NTSCInt(dttMM.Rows(l)!mm_valore).ToString(oApp.FormatImporti) & Space(110), 110)
                lRigheTot = lRigheTot + 1
                w1.WriteLine(strRiga)
              Next
              dttMM.Clear()
            End If

          End If

          'riga 70
          w1.WriteLine(" 70" & Format(lProgrScad, "0000000") & Space(110))
          dTotScad = dTotScad + dImporto
          lRigheTot = lRigheTot + 1
          lProgrScad = lProgrScad + 1
        End With
      Next

      '----------------------
      'Scrive il piede
      w1.WriteLine(" EF" & Left(NTSCStr(dttTabbanc.Rows(0)!tb_rifriba) & "     ", 5) & _
                   Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_abi), 5) & _
                   NTSCDate(strDtdist).ToString("ddMMyy") & Left(strNomeSup & Space(20), 20) & "".PadLeft(6) & _
                   Format(lProgrScad - 1, "0000000") & Format(dTotScad, "000000000000000") & "".PadLeft(15, CChar("0")) & _
                   Format(lRigheTot + 1, "0000000") & "".PadLeft(24) & "E" & _
                   IIf(bCaratteriCodaRecordsIM_EF = True, "".PadLeft(6), "".PadLeft(6, "0"c)).ToString)
      w1.Flush()
      w1.Close()

      '----------------------
      'se devo copio il file anche nella dir alternativa
      strExt = oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "FileABICBICopiaIn", "", " ", "").Trim
      If strExt <> "" Then
        Try
          File.Copy(strNomeFile, (strExt & "\" & strNomeFile83).Replace("\\", "\"), True)
          strNomeFile += vbCrLf & "e copiato in " & (strExt & "\" & strNomeFile83).Replace("\\", "\")
        Catch ex As Exception
          'ignoro l'errore
        End Try
      End If

      Return True
    Catch ex As Exception
      If Not w1 Is Nothing Then w1.Close()
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttScaden.Clear()
      dttAnaz.Clear()
      dttTabbanc.Clear()
    End Try
  End Function

  Public Overridable Function GenBonifEst(ByVal strDitta As String, ByVal strOperat As String, _
    ByVal strDtdist As String, ByVal strDtvalbon As String, ByRef strNomeFile As String, _
    ByVal strTipoScadenze As String, ByVal strTippaga As String, ByVal nDistAnno As Integer, _
    ByVal lDistNum As Integer, ByVal strPnint As String, ByVal bPrefissaFileAbiConDitta As Boolean, _
    ByVal strPathFileAbi As String, ByRef strErr As String) As Boolean

    Try
      '--------------------------------------------------------------------------------------------------------------
      Return GenBonifEst(strDitta, strOperat, strDtdist, strDtvalbon, strNomeFile, strTipoScadenze, _
        strTippaga, nDistAnno, lDistNum, strPnint, bPrefissaFileAbiConDitta, strPathFileAbi, strErr, False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GenBonifEst(ByVal strDitta As String, ByVal strOperat As String, _
                                          ByVal strDtdist As String, ByVal strDtvalbon As String, _
                                          ByRef strNomeFile As String, ByVal strTipoScadenze As String, _
                                          ByVal strTippaga As String, ByVal nDistAnno As Integer, _
                                          ByVal lDistNum As Integer, ByVal strPnint As String, _
                                          ByVal bPrefissaFileAbiConDitta As Boolean, _
                                          ByVal strPathFileAbi As String, ByRef strErr As String, _
                                          ByVal bRaggruppaBonificiForn As Boolean) As Boolean
    '-------------------------------
    'generazione file BONIFICO ESTERO
    Dim evt As NTSEventArgs = Nothing
    Dim dttAnaz As New DataTable
    Dim dttTabbanc As New DataTable
    Dim dttScaden As New DataTable
    Dim dttControp As New DataTable
    Dim dttDestdiv As New DataTable
    Dim dttTmp As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strNomeFile83 As String = ""
    Dim strIdValuta As String = ""
    Dim w1 As StreamWriter = Nothing
    Dim strPR As String = ""
    Dim strNomeSup As String = ""
    Dim lProgrScad As Integer = 0     'Contatore N° RIBA generata
    Dim lRigheTot As Integer = 0      'Contatore righe sritte
    Dim dTotScad As Decimal = 0       'Progressivo valore scadenze
    Dim dImporto As Decimal = 0

    Dim strCINDebitore As String = ""
    Dim strCCDebitore As String = ""
    Dim strDescr1 As String = ""
    Dim strDescr2 As String = ""
    Dim strIndir As String = ""
    Dim strCap As String = ""
    Dim strCitta As String = ""
    Dim strProv As String = ""
    Dim strStato As String = ""
    Dim strPICF As String = ""
    Dim strContatto As String = ""
    Dim strBICOrdinante As String = ""
    Dim strIBANOrdinante As String = ""
    Dim strRiga As String = ""

    Dim strStatoBancaDest As String = ""
    Dim strBICBeneficiario As String = ""
    Dim strIBANBeneficiario As String = ""
    Dim nCod770 As Integer = 0
    Dim strExt As String = ".asc"
    Dim nValuta As Integer = 0
    Dim evnt As NTSEventArgs = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strOperat, strDtdist, strDtvalbon, strNomeFile, _
        strTipoScadenze, strTippaga, nDistAnno, lDistNum, strPnint, bPrefissaFileAbiConDitta, _
        strPathFileAbi, strErr, bRaggruppaBonificiForn})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strNomeFile = NTSCStr(oIn(4))
        strErr = NTSCStr(oIn(12))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      'chiedo se gestire bonifici con tracciato SEPA o ante SEPA
      evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 130220847384328049, "Generazione BONIFICO ESTERO secondo il tracciato SEPA (SI) o con il vecchio tracciato (NO)?"))
      ThrowRemoteEvent(evnt)
      If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
        Return GenAbiBonifSepa(strDitta, strOperat, strDtdist, strDtvalbon, strNomeFile, strTipoScadenze, _
                               strTippaga, nDistAnno, lDistNum, strPnint, bPrefissaFileAbiConDitta, _
                               bRaggruppaBonificiForn, Nothing, Nothing, Nothing, strPathFileAbi, True, strErr)
      End If


      If Not IsDate(strDtdist) Then
        strErr += vbCrLf & oApp.Tr(Me, 128939607057524224, "Data distinta obbligatoria per la generazione dei file BONIFICO ESTERO")
        Return False
      End If

      'If Not (strTippaga = "5" And strTipoScadenze = "P") Then
      '  strErr += vbCrLf & oApp.Tr(Me, 128238821014876050, "Generazione file BONIFICO ESTERO prevista solo su fornitori e per scadenze di tipo 'Accredito bancario'.")
      '  Return False
      'End If

      oCldComm.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttAnaz)

      '------------------------------------
      'ottengo i dati
      If Not oCldComm.GetDataScadenDistinta(strDitta, strTippaga, nDistAnno, lDistNum, strPnint, False, dttScaden) Then
        Return False
      End If
      dtrT = dttScaden.Select()
      If dtrT.Length = 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 130408994717584601, "Nessuna scadenza autorizzata")
        Return False
      End If
      If dtrT.Length > 0 Then
        oCldComm.ValCodiceDb(dtrT(0)!sc_codbanc.ToString, strDitta, "TABBANC", "N", "", dttTabbanc)
      End If

      '----------------------
      'nome del file
      strExt = oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "FileABICBIEstensioneFile", ".asc", " ", ".asc")
      If bPrefissaFileAbiConDitta Then
        strNomeFile83 = strDitta & "_Bone" & Format(lDistNum, "0000") & strExt
      Else
        strNomeFile83 = "Bone" & Format(lDistNum, "0000") & strExt
      End If
      strNomeFile = strPathFileAbi & strNomeFile83

      '----------------------
      'valuta
      nValuta = NTSCInt(dtrT(0)!sc_codvalu)
      If nValuta = 0 Then
        strIdValuta = oApp.ValutaCorrente
      Else
        oCldComm.ValCodiceDb(nValuta.ToString, strDitta, "TABVALU", "N", strIdValuta)
      End If

      '----------------------
      'Rileva BIC dell'ordinante
      strBICOrdinante = oCldComm.GenAbiBIC(NTSCInt(dttTabbanc.Rows(0)!tb_abi), NTSCInt(dttTabbanc.Rows(0)!tb_cab))
      'L'IBAN ordinante lo compone dai 4 campi: prefisso IBAN + CIN + ABI +CAB + NUM c/c
      If NTSCStr(dttTabbanc.Rows(0)!tb_prefiban) = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 128939607567696799, _
        "Non è indicato il 'prefisso IBAN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If Len(NTSCStr(dttTabbanc.Rows(0)!tb_prefiban)) <> 4 Then
        strErr += vbCrLf & oApp.Tr(Me, 128939607892082179, _
          "Il 'prefisso IBAN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "| deve essere di 4 caratteri")
        Return False
      End If
      If Left(NTSCStr(dttTabbanc.Rows(0)!tb_prefiban), 2) <> "IT" And Left(NTSCStr(dttTabbanc.Rows(0)!tb_prefiban), 2) <> "SM" Then
        strErr += vbCrLf & oApp.Tr(Me, 128939608048962199, _
          "Il 'prefisso IBAN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "| deve iniziare con i caratteri IT o SM.")
        Return False
      End If
      If NTSCStr(dttTabbanc.Rows(0)!tb_cin) = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 128939608387879294, _
          "Non è indicato il 'CIN' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If NTSCInt(dttTabbanc.Rows(0)!tb_abi) = 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 128939608664606899, _
          "Non è indicato il cod. 'ABI banca' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If NTSCInt(dttTabbanc.Rows(0)!tb_cab) = 0 Then
        strErr += vbCrLf & oApp.Tr(Me, 128939609044931569, _
          "Non è indicato il cod. 'CAB filiale' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      If Trim(NTSCStr(dttTabbanc.Rows(0)!tb_csbf)) = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 128939609204780434, _
          "Non è indicato il 'N° C/C' nel cod. ns. banca |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|")
        Return False
      End If
      strIBANOrdinante = NTSCStr(dttTabbanc.Rows(0)!tb_prefiban) & _
                         NTSCStr(dttTabbanc.Rows(0)!tb_cin) & _
                         Microsoft.VisualBasic.Right("00000" & NTSCInt(dttTabbanc.Rows(0)!tb_abi), 5) & _
                         Microsoft.VisualBasic.Right("00000" & NTSCInt(dttTabbanc.Rows(0)!tb_cab), 5) & _
                         Microsoft.VisualBasic.Right("000000000000" & Trim(NTSCStr(dttTabbanc.Rows(0)!tb_csbf)), 12)

      'Controlla se IBAN e BIC sono presenti
      If strIBANOrdinante = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 128238883469621872, _
                  "Attenzione: codice IBAN (italia) non indicato per la banca di codice |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "|.")
        Return False
      End If
      If strIBANOrdinante.Length <> 27 Then
        strErr += vbCrLf & oApp.Tr(Me, 128383223203536000, _
                  "Attenzione: codice IBAN (italia) '|" & strIBANOrdinante & "|' per la banca di codice |" & dttTabbanc.Rows(0)!tb_codbanc.ToString & "| non è di 27 caratteri.")
        Return False
      End If
      If strBICOrdinante = "" Then
        strErr += vbCrLf & oApp.Tr(Me, 128383223191836000, _
                  "Attenzione: codice internazionale SWIFT (tabella CAB) o BIC (tabella ABI) non indicato per la banca con ABI |" & dttTabbanc.Rows(0)!tb_abi.ToString & "| e CAB |" & dttTabbanc.Rows(0)!tb_cab.ToString & "|; integrarlo in 'Gestione ABI/CAB'. Proceduto ugualmente")
        'Return False
      End If

      '----------------------
      'Controlla se già esiste il file
      If System.IO.File.Exists(strNomeFile) Then
        'evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128238690194733441, "Esiste già un file chiamato |" & UCase(strNomeFile) & "|. Sovrascriverlo?"))
        'ThrowRemoteEvent(evt)
        'If evt.RetValue = "NO" Then Return False
        System.IO.File.Delete(strNomeFile)
      End If

      w1 = New StreamWriter(strNomeFile, False)
      '----------------------
      'Scrive la testata
      strPR = " PE"
      strNomeSup = Left(nDistAnno.ToString & lDistNum.ToString & Space(20), 20)
      w1.WriteLine(strPR & Left(NTSCStr(dttTabbanc.Rows(0)!tb_rifriba) & "     ", 5) & _
                   Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_abi), 5) & NTSCDate(strDtdist).ToString("ddMMyy") & _
                   Left(strNomeSup & Space(20), 20) & Space(6) & Space(70) & "".PadLeft(5))
      lRigheTot = 1
      lProgrScad = 1
      dTotScad = 0

      '----------------------
      'scrivo il corpo
      For i = 0 To dtrT.Length - 1
        With dtrT(i)
          '------------------------------------------------------------------------------
          'Rileva BIC del beneficiario
          'IN se la filiale si trova all'estero, DO se la filiale si trova in italia (anche se la banca è estera)
          If NTSCInt(!sc_abi) = 0 Or NTSCInt(!sc_cab) = 0 Then
            strErr += vbCrLf & oApp.Tr(Me, 128238887427877740, _
                "Attenzione: impossibile determinare lo stato e il codice SWIFT/BIC della banca per la scadenza: |" & !sc_conto.ToString & "| - |" & _
                !sc_annpar.ToString & "| - |" & !sc_alfpar.ToString & "| - |" & !sc_numpar.ToString & "| - |" & !sc_numrata.ToString & _
                "| a causa della mancanza della codifica di ABI o CAB. Verrà considerata una filiale nel territorio italiano.")
            strStatoBancaDest = "DO"
            strBICBeneficiario = ""
          Else
            oCldComm.ValCodiceDb(!sc_cab.ToString, strDitta, "CAB", "N", "", dttTmp, !sc_abi.ToString)
            If dttTmp.Rows.Count = 0 Then
              strErr += vbCrLf & oApp.Tr(Me, 128238889871814480, _
                  "Attenzione: impossibile determinare lo stato della banca per la scadenza: |" & !sc_conto.ToString & "| - |" & _
                  !sc_annpar.ToString & "| - |" & !sc_alfpar.ToString & "| - |" & !sc_numpar.ToString & "| - |" & !sc_numrata.ToString & _
                  "| a causa della mancanza della codifica di ABI o CAB. Verrà considerata una filiale nel territorio italiano.")
              strStatoBancaDest = "DO"
            Else
              strBICBeneficiario = NTSCStr(dttTmp.Rows(0)!AbcSwift)
              If Trim(NTSCStr(dttTmp.Rows(0)!abcstato)) = "I" Or Trim(NTSCStr(dttTmp.Rows(0)!abcstato)) = "" Then
                strStatoBancaDest = "DO"
              Else
                strStatoBancaDest = "IN"
              End If
              dttTmp.Clear()
              If Trim(strBICBeneficiario) = "" Then
                oCldComm.ValCodiceDb(!sc_abi.ToString, strDitta, "ABI", "N", "", dttTmp)
                If dttTmp.Rows.Count > 0 Then strBICBeneficiario = NTSCStr(dttTmp.Rows(0)!AbiCodalf)
                dttTmp.Clear()
              End If
              If strBICBeneficiario = "" Then
                strErr += vbCrLf & oApp.Tr(Me, 128238892588497372, _
                    "Attenzione: codice internazionale SWIFT (tabella CAB) o BIC (tabella ABI) non rilevato per la banca per la scadenza: |" & !sc_conto.ToString & "| - |" & _
                    !sc_annpar.ToString & "| - |" & !sc_alfpar.ToString & "| - |" & !sc_numpar.ToString & "| - |" & !sc_numrata.ToString & _
                    "|.Si procedere comunque.")
              End If
            End If
          End If    'If NTSCInt(!sc_abi) = 0 Or NTSCInt(!sc_cab) = 0 Then

          '------------------------------------------------------------------------------
          'controlla il NUM. C/C del beneficiario che rappresenta il codice IBAN
          strIBANBeneficiario = NTSCStr(!sc_iban)
          If Trim(strIBANBeneficiario) = "" Then
            strErr += vbCrLf & oApp.Tr(Me, 128238892570064356, _
                  "Attenzione: codice IBAN beneficiario (ossia il campo 'IBAN estero') non indicato per la scadenza: |" & !sc_conto.ToString & "| - |" & _
                  !sc_annpar.ToString & "| - |" & !sc_alfpar.ToString & "| - |" & !sc_numpar.ToString & "| - |" & !sc_numrata.ToString & _
                  "|.Si procedere comunque.")
          End If

          'riga H1
          strRiga = " H1" & Format(lProgrScad, "0000000") & "PAYORDD  93AUN" & "450"
          strRiga = strRiga & Right(Format(!sc_conto, "000000000"), 9) & !sc_annpar.ToString & !sc_alfpar.ToString & Right(Format(!sc_numpar, "000000"), 6) & Right(Format(!sc_numrata, "00"), 2) & Space(12)
          strRiga = strRiga & " 9 " & Space(14) & Space(14) & Space(14)
          strRiga = strRiga & strStatoBancaDest & " "
          strRiga = strRiga & Space(3) & Space(3) & Space(5)
          lRigheTot = lRigheTot + 1
          w1.WriteLine(strRiga)

          'riga P1
          strRiga = " P1" & Format(lProgrScad, "0000000") & "9  "
          If nValuta = 0 Then
            strRiga = strRiga & Format(!sc_importoda, "000000000000000.00")
            dTotScad = dTotScad + NTSCDec(!sc_importoda)
          Else
            strRiga = strRiga & Format(!sc_impvalda, "000000000000000.00")
            dTotScad = dTotScad + NTSCDec(!sc_impvalda)
          End If
          strRiga = strRiga & Left(strIdValuta & "   ", 3)
          strRiga = strRiga & Space(18) & Space(18) & Space(11) & Space(38) & " "
          lRigheTot = lRigheTot + 1
          w1.WriteLine(strRiga)

          'riga PH
          'Banche e C/C interessati espressi in forma codificata
          strRiga = " PH" & Format(lProgrScad, "0000000")
          strRiga = strRiga & Left(strIBANOrdinante & Space(34), 34)
          strRiga = strRiga & Left(strBICOrdinante & Space(11), 11)
          strRiga = strRiga & Space(5)
          strRiga = strRiga & Left(strIBANBeneficiario & Space(34), 34)
          strRiga = strRiga & Left(strBICBeneficiario & Space(11), 11)
          strRiga = strRiga & Space(15)
          lRigheTot = lRigheTot + 1
          w1.WriteLine(strRiga)

          'riga P5
          'informazioni sul beneficiario
          strRiga = " P5" & Format(lProgrScad, "0000000") & "BE "
          oCldComm.ValCodiceDb(!sc_conto.ToString, strDitta, "ANAGRA", "N", "", dttControp)
          strRiga = strRiga & Left(NTSCStr(dttControp.Rows(0)!an_descr1) & NTSCStr(dttControp.Rows(0)!an_descr2) & Space(35), 35)
          strRiga = strRiga & Left(NTSCStr(dttControp.Rows(0)!an_indir) & Space(35), 35)
          strRiga = strRiga & Left(NTSCStr(dttControp.Rows(0)!an_citta) & Space(32), 32)
          strRiga = strRiga & Space(5)
          lRigheTot = lRigheTot + 1
          w1.WriteLine(strRiga)

          'riga P8
          strRiga = " P8" & Format(lProgrScad, "0000000") & "380" & Left(!sc_numpar.ToString & NTSCStr(!sc_alfpar) & Space(17), 17)
          strRiga = strRiga & "171" & Format(NTSCDate(!sc_datdoc), "yyyyMMdd") & "102" & Space(16) & Space(34) & Space(26)
          lRigheTot = lRigheTot + 1
          w1.WriteLine(strRiga)

          'riga P9
          strRiga = " P9" & Format(lProgrScad, "0000000") & "ZZ RA "
          '-------------------------------------------------------------------------------------
          If (NTSCStr(!sc_tipcvs) = " ") Or (NTSCStr(!sc_tipcvs) = "") Then
            If CDbl(!sc_importoda) > 12500 Then
              strRiga = strRiga & "EMI"
            Else
              strRiga = strRiga & "INF"
            End If
          Else
            strRiga = strRiga & NTSCStr(!sc_tipcvs)
          End If
          '-------------------------------------------------------------------------------------
          strRiga = strRiga & "".PadLeft(8, CChar("0")) & "".PadLeft(8, CChar("0")) & "".PadLeft(7, CChar("0")) & Space(12) & Space(5)
          '-------------------------------------------------------------------------------------
          If NTSCStr(!sc_cauval) = "" Then
            strErr += vbCrLf & oApp.Tr(Me, 128939609404318069, _
                "Attenzione!" & vbCrLf & _
                "Il bonifico estero generato per:" & vbCrLf & _
                "  Numero conto..: |'" & NTSCStr(!sc_conto) & "'|" & vbCrLf & _
                "  Anno partita..: |'" & NTSCStr(!sc_annpar) & "'|" & vbCrLf & _
                "  Serie partita.: |'" & NTSCStr(!sc_alfpar) & "'|" & vbCrLf & _
                "  Numero partita: |'" & NTSCStr(!sc_numpar) & "'|" & vbCrLf & _
                "  Numero rata...: |'" & NTSCStr(!sc_numrata) & "'|" & vbCrLf & _
                "Non possiede una causale valutaria valida (scaden.sc_cauval).")
          End If
          '-------------------------------------------------------------------------------------
          strRiga = strRiga & Left(NTSCStr(!sc_cauval) & Space(5), 5) & "".PadLeft(15, CChar("0")) & "," & "00" & "  " & "".PadLeft(15, CChar("0")) & "," & "00"
          '-------------------------------------------------------------------------------------
          nCod770 = oCldComm.GenAnagraCod770BonifEst(strDitta, NTSCInt(!sc_conto))
          If nCod770 = 0 Then
            strErr += vbCrLf & oApp.Tr(Me, 128939609467445089, _
                "Attenzione!" & vbCrLf & _
                "Il bonifico estero generato per:" & vbCrLf & _
                "  Numero conto..: |'" & NTSCStr(!sc_conto) & "'|" & vbCrLf & _
                "  Anno partita..: |'" & NTSCStr(!sc_annpar) & "'|" & vbCrLf & _
                "  Serie partita.: |'" & NTSCStr(!sc_alfpar) & "'|" & vbCrLf & _
                "  Numero partita: |'" & NTSCStr(!sc_numpar) & "'|" & vbCrLf & _
                "  Numero rata...: |'" & NTSCStr(!sc_numrata) & "'|" & vbCrLf & _
                "Non possiede un codice paese del Fornirore beneficiario valido (tabstat.tb_cod770).")
          End If
          strRiga = strRiga & Right("000" & CStr(nCod770), 3) & Space(15)
          lRigheTot = lRigheTot + 1
          w1.WriteLine(strRiga)
          lProgrScad = lProgrScad + 1 'incremento il numero di bonifici scritti
        End With
      Next

      '----------------------
      'Scrive il piede
      w1.WriteLine(" EF" & Left(NTSCStr(dttTabbanc.Rows(0)!tb_rifriba) & "     ", 5) & _
                   Right("00000" & NTSCStr(dttTabbanc.Rows(0)!tb_abi), 5) & _
                   NTSCDate(strDtdist).ToString("ddMMyy") & Left(strNomeSup & Space(20), 20) & "".PadLeft(6) & _
                   Format(lProgrScad - 1, "0000000") & Space(12) & Format(dTotScad, "000000000000000.00") & _
                   Format(lRigheTot + 1, "0000000") & Space(25) & "".PadLeft(6))
      w1.Flush()
      w1.Close()

      '----------------------
      'se devo copio il file anche nella dir alternativa
      strExt = oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "FileABICBICopiaIn", "", " ", "").Trim
      If strExt <> "" Then
        Try
          File.Copy(strNomeFile, (strExt & "\" & strNomeFile83).Replace("\\", "\"), True)
          strNomeFile += vbCrLf & "e copiato in " & (strExt & "\" & strNomeFile83).Replace("\\", "\")
        Catch ex As Exception
          'ignoro l'errore
        End Try
      End If

      Return True
    Catch ex As Exception
      If Not w1 Is Nothing Then w1.Close()
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttScaden.Clear()
      dttAnaz.Clear()
      dttTabbanc.Clear()
    End Try
  End Function

  Public Overridable Function ConvStrFileAbi(ByVal strIn As String) As String
    Dim i As Integer = 0
    Dim strOut As String
    strOut = ""
    For i = 1 To Len(strIn)
      Select Case Mid(strIn, i, 1)
        Case "à", "á"
          strOut = strOut & "a'"
        Case "è", "é"
          strOut = strOut & "e'"
        Case "ì", "í"
          strOut = strOut & "i'"
        Case "ò", "ó"
          strOut = strOut & "o'"
        Case "ù", "ú"
          strOut = strOut & "u'"
        Case "a" To "z", "A" To "Z", "0" To "9", "/", "-", "?", ":", "(", ")", ".", ",", "'", " "
          strOut = strOut & Mid(strIn, i, 1)
        Case """", "$", "%", "&", "*", "+", ";", "<", "=", ">", "_"
          strOut = strOut & " "
        Case Else
          'Carattere non consentito
          strOut = strOut & " "
      End Select
    Next
    Return strOut
  End Function
#End Region

  Public Overridable Function SalvaListino(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, ByVal strUnmis As String, _
                                           ByVal strtipoListino As String, ByVal nListino As Integer, ByVal lConto As Integer, _
                                           ByVal strDatIni As String, ByVal nCodlavo As Integer, ByVal nCodvalu As Integer, _
                                           ByVal nCodtpro As Integer, ByVal dPrezzo As Decimal) As Boolean
    Try
      'obsoleta
      Return SalvaListino(strDitta, strCodart, nFase, strUnmis, strtipoListino, nListino, lConto, strDatIni, _
                          nCodlavo, nCodvalu, nCodtpro, dPrezzo, 0)

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
  Public Overridable Function SalvaListino(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, ByVal strUnmis As String, _
                                           ByVal strtipoListino As String, ByVal nListino As Integer, ByVal lConto As Integer, _
                                           ByVal strDatIni As String, ByVal nCodlavo As Integer, ByVal nCodvalu As Integer, _
                                           ByVal nCodtpro As Integer, ByVal dPrezzo As Decimal, ByVal lCoddest As Integer) As Boolean
    '------------------------------------------
    'dato un listino, provvede ad aggiornare la tabella 'listini' aggiustando 
    'se necessario i record precedenti e successivi a livello di date inizio/fine validità
    'chiamato, ad esempio, da BSMGSTRL
    Dim dttTmp As New DataTable
    Dim strCodartNew As String = ""
    Dim dPerqta As Decimal = 1
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nFase, strUnmis, strtipoListino, nListino, lConto, strDatIni, _
                          nCodlavo, nCodvalu, nCodtpro, dPrezzo, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------


      '----------------------------
      'determino il cod articolo reale (vedi articoli a varianti)
      oCldComm.ValCodiceDb(strCodart, strDitta, "ARTICO", "S", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then Return False

      dPerqta = NTSCDec(dttTmp.Rows(0)!ar_perqta)
      If NTSCStr(dttTmp.Rows(0)!ar_codroot) = "" Or NTSCStr(dttTmp.Rows(0)!ar_prevar) = "S" Then
        strCodartNew = strCodart
      ElseIf NTSCStr(dttTmp.Rows(0)!ar_prevar) = "1" Then
        strCodartNew = NTSCStr(dttTmp.Rows(0)!ar_codroot) & NTSCStr(dttTmp.Rows(0)!ar_codvar1)
      Else
        strCodartNew = NTSCStr(dttTmp.Rows(0)!ar_codroot)
      End If
      dttTmp.Clear()


      Return oCldComm.SalvaListino(strDitta, strCodartNew, nFase, strUnmis, strtipoListino, nListino, lConto, _
                                    strDatIni, nCodlavo, nCodvalu, nCodtpro, dPerqta, dPrezzo, lCoddest)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaPrezzo(ByVal strDitta As String, ByVal strCodart As String, ByVal nCodlavo As Integer, _
                           ByVal lConto As Integer, ByVal nListino As Integer, ByVal strUnmisin As String, _
                           ByVal nFase As Integer, ByVal strTipoval As String, ByVal bConspromo As Boolean, _
                           ByVal nCodpromoin As Integer, ByVal lLottoin As Integer, ByVal dtDatdoc As DateTime, _
                           ByVal nCodvalu As Integer, ByVal dQuant As Decimal, _
                           ByRef dPrezzo As Decimal, ByRef dPrelist As Decimal, ByRef nCodpromo As Integer, _
                           ByRef strPrzNet As String, _
                           Optional ByRef nPerqta As Integer = 1, _
                           Optional ByRef dDaQuant As Decimal = 0, Optional ByRef dAquant As Decimal = 9999999999, _
                           Optional ByRef dPerqta As Decimal = 1, Optional ByRef strUnmisout As String = "", _
                           Optional ByRef strTipovalout As String = "", Optional ByRef strCodCas As String = " ", _
                           Optional ByRef strErr As String = Nothing, Optional ByRef dtDatAggOut As Date = Nothing) As Boolean
    Try
      Return CercaPrezzo(strDitta, strCodart, nCodlavo, _
                         lConto, nListino, strUnmisin, _
                         nFase, strTipoval, bConspromo, _
                         nCodpromoin, lLottoin, dtDatdoc, _
                         nCodvalu, dQuant, _
                         dPrezzo, dPrelist, nCodpromo, _
                         0, strPrzNet, "", nPerqta, dDaQuant, dAquant, _
                         dPerqta, strUnmisout, _
                         strTipovalout, strCodCas, _
                         strErr, dtDatAggOut, )
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaPrezzo(ByVal strDitta As String, ByVal strCodart As String, ByVal nCodlavo As Integer, _
                            ByVal lConto As Integer, ByVal nListino As Integer, ByVal strUnmisin As String, _
                            ByVal nFase As Integer, ByVal strTipoval As String, ByVal bConspromo As Boolean, _
                            ByVal nCodpromoin As Integer, ByVal lLottoin As Integer, ByVal dtDatdoc As DateTime, _
                            ByVal nCodvalu As Integer, ByVal dQuant As Decimal, _
                            ByRef dPrezzo As Decimal, ByRef dPrelist As Decimal, ByRef nCodpromo As Integer, _
                            ByRef lProgr As Integer, ByRef strPrzNet As String, ByRef strTipoOut As String, _
                            Optional ByRef nPerqta As Integer = 1, _
                            Optional ByRef dDaQuant As Decimal = 0, Optional ByRef dAquant As Decimal = 9999999999, _
                            Optional ByRef dPerqta As Decimal = 1, Optional ByRef strUnmisout As String = "", _
                            Optional ByRef strTipovalout As String = "", Optional ByRef strCodCas As String = " ", _
                            Optional ByRef strErr As String = Nothing, Optional ByRef dtDatAggOut As Date = Nothing, _
                            Optional ByRef dtAData As Date = Nothing) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nCodlavo, lConto, nListino, strUnmisin, _
                                             nFase, strTipoval, bConspromo, nCodpromoin, lLottoin, dtDatdoc, _
                                             nCodvalu, dQuant, dPrezzo, dPrelist, nCodpromo, _
                                             lProgr, strPrzNet, strTipoOut, nPerqta, dDaQuant, dAquant, _
                                             dPerqta, strUnmisout, strTipovalout, strCodCas, _
                                             strErr, dtDatAggOut, dtAData})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dPrezzo = NTSCDec(oIn(14))
        dPrelist = NTSCDec(oIn(15))
        nCodpromo = NTSCInt(oIn(16))
        lProgr = NTSCInt(oIn(17))
        strPrzNet = NTSCStr(oIn(18))
        strTipoOut = NTSCStr(oIn(19))
        nPerqta = NTSCInt(oIn(20))
        dDaQuant = NTSCDec(oIn(21))
        dAquant = NTSCDec(oIn(22))
        dPerqta = NTSCDec(oIn(23))
        strUnmisout = NTSCStr(oIn(24))
        strTipovalout = NTSCStr(oIn(25))
        strCodCas = NTSCStr(oIn(26))
        strErr = NTSCStr(oIn(27))
        dtDatAggOut = CDate(oIn(28))
        dtAData = CDate(oIn(29))
        Return CBool(oOut)
      End If
      '----------------

      Return oCldComm.CercaPrezzo(strDitta, strCodart, nCodlavo, _
                                     lConto, nListino, strUnmisin, _
                                     nFase, strTipoval, bConspromo, _
                                     nCodpromoin, lLottoin, dtDatdoc, _
                                     nCodvalu, dQuant, _
                                     dPrezzo, dPrelist, nCodpromo, _
                                     strPrzNet, strTipoOut, nPerqta, dDaQuant, dAquant, _
                                     dPerqta, strUnmisout, _
                                     strTipovalout, strCodCas, _
                                     strErr, dtDatAggOut, dtAData, lProgr)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaPrezzo(ByVal strDitta As String, ByVal strCodart As String, ByVal nCodlavo As Integer, _
                          ByVal lConto As Integer, ByVal nListino As Integer, ByVal strUnmisin As String, _
                          ByVal nFase As Integer, ByVal strTipoval As String, ByVal bConspromo As Boolean, _
                          ByVal nCodpromoin As Integer, ByVal lLottoin As Integer, ByVal dtDatdoc As DateTime, _
                          ByVal nCodvalu As Integer, ByVal dQuant As Decimal, _
                          ByRef dPrezzo As Decimal, ByRef dPrelist As Decimal, ByRef nCodpromo As Integer, _
                          ByRef lProgr As Integer, ByRef strPrzNet As String, ByRef strTipoOut As String, _
                          ByRef nPerqta As Integer, ByRef dDaQuant As Decimal, ByRef dAquant As Decimal, _
                          ByRef dPerqta As Decimal, ByRef strUnmisout As String, _
                          ByRef strTipovalout As String, ByRef strCodCas As String, _
                          ByRef strErr As String, ByRef dtDatAggOut As Date, _
                          ByRef dtAData As Date, ByVal bNoPrelist As Boolean) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, nCodlavo, lConto, nListino, strUnmisin, _
                                             nFase, strTipoval, bConspromo, nCodpromoin, lLottoin, dtDatdoc, _
                                             nCodvalu, dQuant, dPrezzo, dPrelist, nCodpromo, _
                                             lProgr, strPrzNet, strTipoOut, nPerqta, dDaQuant, dAquant, _
                                             dPerqta, strUnmisout, strTipovalout, strCodCas, _
                                             strErr, dtDatAggOut, dtAData, bNoPrelist})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dPrezzo = NTSCDec(oIn(14))
        dPrelist = NTSCDec(oIn(15))
        nCodpromo = NTSCInt(oIn(16))
        lProgr = NTSCInt(oIn(17))
        strPrzNet = NTSCStr(oIn(18))
        strTipoOut = NTSCStr(oIn(19))
        nPerqta = NTSCInt(oIn(20))
        dDaQuant = NTSCDec(oIn(21))
        dAquant = NTSCDec(oIn(22))
        dPerqta = NTSCDec(oIn(23))
        strUnmisout = NTSCStr(oIn(24))
        strTipovalout = NTSCStr(oIn(25))
        strCodCas = NTSCStr(oIn(26))
        strErr = NTSCStr(oIn(27))
        dtDatAggOut = CDate(oIn(28))
        dtAData = CDate(oIn(29))
        Return CBool(oOut)
      End If
      '----------------
      Return oCldComm.CercaPrezzo(strDitta, strCodart, nCodlavo, lConto, nListino, strUnmisin, _
                                 nFase, strTipoval, bConspromo, nCodpromoin, lLottoin, dtDatdoc, _
                                 nCodvalu, dQuant, dPrezzo, dPrelist, nCodpromo, strPrzNet, strTipoOut, _
                                 nPerqta, dDaQuant, dAquant, dPerqta, strUnmisout, strTipovalout, strCodCas, _
                                 strErr, dtDatAggOut, dtAData, lProgr, bNoPrelist)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaPrezzoMultiArti(ByVal strDitta As String, ByVal nCodlavo As Integer, _
                                                   ByVal lConto As Integer, ByVal nListino As Integer, ByVal strUnmisin As String, _
                                                   ByVal strTipoval As String, ByVal bConspromo As Boolean, _
                                                   ByVal nCodpromoin As Integer, ByVal lLottoin As Integer, ByVal dtDatdoc As DateTime, _
                                                   ByVal nCodvalu As Integer, ByVal dQuant As Decimal, ByVal bNoPrelist As Boolean, _
                                                   ByVal lListaSel As Integer, ByVal strWhereArtico As String, ByRef dttListini As DataTable) As Boolean
    Try
      'Usata solo dalla stampa listini, per velocizzare le operazioni elaborando tutti gli articoli in una volta, invece che un singolo articolo
      Return oCldComm.CercaPrezzo(strDitta, "", nCodlavo, lConto, nListino, strUnmisin, _
                                 0, strTipoval, bConspromo, nCodpromoin, lLottoin, dtDatdoc, _
                                 nCodvalu, dQuant, 0, 0, 0, "N", " ", 0, 0, 0, 0, "", "", " ", "", _
                                 New Date(1900, 1, 1), New Date(1900, 1, 1), 0, bNoPrelist, lListaSel, strWhereArtico, dttListini)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaPrezzoVCL(ByVal strDitta As String, ByVal strCodart As String, ByVal lConto As Integer, _
                                            ByVal nListino As Integer, ByVal strTipoval As String, ByVal dtDatdoc As DateTime, _
                                            ByVal nCodvalu As Integer, ByVal dQuant As Decimal, _
                                            ByRef dPrezzo As Decimal, ByRef strTipovalout As String, _
                                            ByRef dDaQuant As Decimal, ByRef dAquant As Decimal) As Boolean
    Try
      Return oCldComm.CercaPrezzoVCL(strDitta, strCodart, lConto, nListino, strTipoval, dtDatdoc, _
                                     nCodvalu, dQuant, dPrezzo, strTipovalout, dDaQuant, dAquant)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaPrezzoFifo(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, _
                                ByVal nCodmaga As Integer, ByVal dtData As String, ByVal dQta As Decimal, _
                                ByVal nConsEsPrenImp As Integer, ByRef dValoretot As Decimal, ByRef strErr As String) As Decimal
    Try
      Return oCldComm.CercaPrezzoFifo(strDitta, strCodart, nFase, nCodmaga, dtData, dQta, nConsEsPrenImp, dValoretot, strErr)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaSconti(ByVal strDitta As String, ByVal strCodart As String, ByVal lConto As Integer, _
                              ByVal nClscar As Integer, ByVal nClscan As Integer, ByVal strTipoval As String, _
                              ByVal bConspromo As Boolean, ByVal nCodpromoin As Integer, ByVal dtDatdoc As Date, _
                              ByVal dQuant As Decimal, ByRef dSc1 As Decimal, ByRef dSc2 As Decimal, ByRef dSc3 As Decimal, _
                              ByRef dSc4 As Decimal, ByRef dSc5 As Decimal, ByRef dSc6 As Decimal, _
                              ByRef nPromo As Integer, ByVal strPrzNet As String, Optional ByRef nPerqta As Integer = 1, _
                              Optional ByVal dDaQuant As Decimal = 0, Optional ByVal dAquant As Decimal = 9999999999, _
                              Optional ByRef strUnmisout As String = "", Optional ByRef strTipovalout As String = " ") As Boolean
    Try
      Return CercaSconti(strDitta, strCodart, lConto, nClscar, nClscan, strTipoval, _
                               bConspromo, nCodpromoin, dtDatdoc, dQuant, dSc1, dSc2, dSc3, _
                               dSc4, dSc5, dSc6, nPromo, strPrzNet, Nothing, Nothing, nPerqta, dDaQuant, dAquant, _
                               strUnmisout, strTipovalout)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaSconti(ByVal strDitta As String, ByVal strCodart As String, ByVal lConto As Integer, _
                              ByVal nClscar As Integer, ByVal nClscan As Integer, ByVal strTipoval As String, _
                              ByVal bConspromo As Boolean, ByVal nCodpromoin As Integer, ByVal dtDatdoc As Date, _
                              ByVal dQuant As Decimal, ByRef dSc1 As Decimal, ByRef dSc2 As Decimal, ByRef dSc3 As Decimal, _
                              ByRef dSc4 As Decimal, ByRef dSc5 As Decimal, ByRef dSc6 As Decimal, _
                              ByRef nPromo As Integer, ByVal strPrzNet As String, ByRef dtDaData As Date, ByRef dtAData As Date, _
                              Optional ByRef nPerqta As Integer = 1, _
                              Optional ByRef dDaQuant As Decimal = 0, Optional ByRef dAquant As Decimal = 9999999999, _
                              Optional ByRef strUnmisout As String = "", Optional ByRef strTipovalout As String = " ") As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, lConto, _
                                             nClscar, nClscan, strTipoval, _
                                             bConspromo, nCodpromoin, dtDatdoc, _
                                             dQuant, dSc1, dSc2, dSc3, _
                                             dSc4, dSc5, dSc6, _
                                             nPromo, strPrzNet, dtDaData, dtAData, _
                                             nPerqta, dDaQuant, dAquant, _
                                             strUnmisout, strTipovalout})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dSc1 = NTSCDec(oIn(10))
        dSc2 = NTSCDec(oIn(11))
        dSc3 = NTSCDec(oIn(12))
        dSc4 = NTSCDec(oIn(13))
        dSc5 = NTSCDec(oIn(14))
        dSc6 = NTSCDec(oIn(15))
        nPromo = NTSCInt(oIn(16))
        dtDaData = CDate(oIn(18))
        dtAData = CDate(oIn(19))
        nPerqta = NTSCInt(oIn(20))
        dDaQuant = NTSCDec(oIn(21))
        dAquant = NTSCDec(oIn(22))
        strUnmisout = NTSCStr(oIn(23))
        strTipovalout = NTSCStr(oIn(24))
        Return CBool(oOut)
      End If
      '----------------

      Return oCldComm.CercaSconti(strDitta, strCodart, lConto, nClscar, nClscan, strTipoval, _
                               bConspromo, nCodpromoin, dtDatdoc, dQuant, dSc1, dSc2, dSc3, _
                               dSc4, dSc5, dSc6, nPromo, strPrzNet, nPerqta, dDaQuant, dAquant, _
                               strUnmisout, strTipovalout, dtDaData, dtAData, 999999999)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaSconti(ByVal strDitta As String, ByVal strCodart As String, ByVal lConto As Integer, _
                              ByVal nClscar As Integer, ByVal nClscan As Integer, ByVal strTipoval As String, _
                              ByVal bConspromo As Boolean, ByVal nCodpromoin As Integer, ByVal dtDatdoc As Date, _
                              ByVal dQuant As Decimal, ByRef dSc1 As Decimal, ByRef dSc2 As Decimal, ByRef dSc3 As Decimal, _
                              ByRef dSc4 As Decimal, ByRef dSc5 As Decimal, ByRef dSc6 As Decimal, _
                              ByRef nPromo As Integer, ByVal strPrzNet As String, ByRef dtDaData As Date, ByRef dtAData As Date, _
                              ByRef nPerqta As Integer, ByRef dDaQuant As Decimal, ByRef dAquant As Decimal, _
                              ByRef strUnmisout As String, ByRef strTipovalout As String, ByVal nListino As Integer) As Boolean

    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, lConto, _
                                             nClscar, nClscan, strTipoval, _
                                             bConspromo, nCodpromoin, dtDatdoc, _
                                             dQuant, dSc1, dSc2, dSc3, _
                                             dSc4, dSc5, dSc6, _
                                             nPromo, strPrzNet, dtDaData, dtAData, _
                                             nPerqta, dDaQuant, dAquant, _
                                             strUnmisout, strTipovalout, nListino})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dSc1 = NTSCDec(oIn(10))
        dSc2 = NTSCDec(oIn(11))
        dSc3 = NTSCDec(oIn(12))
        dSc4 = NTSCDec(oIn(13))
        dSc5 = NTSCDec(oIn(14))
        dSc6 = NTSCDec(oIn(15))
        nPromo = NTSCInt(oIn(16))
        dtDaData = CDate(oIn(18))
        dtAData = CDate(oIn(19))
        nPerqta = NTSCInt(oIn(20))
        dDaQuant = NTSCDec(oIn(21))
        dAquant = NTSCDec(oIn(22))
        strUnmisout = NTSCStr(oIn(23))
        strTipovalout = NTSCStr(oIn(24))
        Return CBool(oOut)
      End If
      '----------------
      Return oCldComm.CercaSconti(strDitta, strCodart, lConto, nClscar, nClscan, strTipoval, _
                                  bConspromo, nCodpromoin, dtDatdoc, dQuant, dSc1, dSc2, dSc3, _
                                  dSc4, dSc5, dSc6, nPromo, strPrzNet, nPerqta, dDaQuant, dAquant, _
                                  strUnmisout, strTipovalout, dtDaData, dtAData, nListino)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub VerificaUsoCercaCondCommerciali()
    'Viene valutato quale sistema di CercaPrezzi\CercaSconti utilizzare
    Dim methodsBE() As System.Reflection.MethodInfo = Nothing
    Dim methodsBD() As System.Reflection.MethodInfo = Nothing
    Try
      Select Case oCldComm.GetSettingBus("OPZIONI", ".", ".", "UsaVecchiaCercaPrezzo", "0", ".", "0")
        Case "-1" : bUsaVecchiaCercaPrezzo = True 'Voglio SEMPRE usare la vecchia cerca prezzo
        Case "1" : bUsaVecchiaCercaPrezzo = False 'Voglio SEMPRE usare la nuova cerca prezzo
        Case Else 'non ho specificato, valuto quale usare
          If Not Me.GetType.ToString.StartsWith("NTSInformatica.CLE") Then 'Se inizia con CLE non è personalizzata
            'ottengo i metodi della classe ereditata (sono presenti solo quelli ereditati e specifici della classe figlia)
            methodsBE = Me.GetType.GetMethods(System.Reflection.BindingFlags.Public Or _
                                              System.Reflection.BindingFlags.Instance Or _
                                              System.Reflection.BindingFlags.DeclaredOnly)

            For Each method As System.Reflection.MethodInfo In methodsBE
              If method.Name.ToLower = "cercaprezzo" Then
                bUsaVecchiaCercaPrezzo = True
                Exit Select ' Non serve che controlli anche il BD, è già personalizzato
              End If
            Next
          End If

          If Not oCldComm.GetType.ToString.StartsWith("NTSInformatica.CLD") Then 'Se inizia con CLD non è personalizzata
            'ottengo i metodi della classe ereditata (sono presenti solo quelli ereditati e specifici della classe figlia)
            methodsBD = oCldComm.GetType.GetMethods(System.Reflection.BindingFlags.Public Or _
                                                    System.Reflection.BindingFlags.Instance Or _
                                                    System.Reflection.BindingFlags.DeclaredOnly)

            For Each method As System.Reflection.MethodInfo In methodsBD
              Dim strMethod As String = method.Name.ToLower
              If strMethod = "cercaprezzo" OrElse strMethod = "preparadatatablelistini" OrElse strMethod = "preparadatatableartprox" OrElse _
                 strMethod = "preparadatatableprezzofifo" OrElse strMethod = "preparadatatablecostomediopond" OrElse _
                 strMethod = "cercaprezzo_prelist" OrElse strMethod = "cercaprezzo_listino" OrElse strMethod = "cercaprezzo_costi" OrElse _
                 strMethod = "cercaprezzo_valuta" Then
                bUsaVecchiaCercaPrezzo = True
                Exit For
              End If
            Next
          End If
      End Select

      Select Case oCldComm.GetSettingBus("OPZIONI", ".", ".", "UsaVecchiaCercaSconti", "0", ".", "0")
        Case "-1" : bUsaVecchiaCercaSconti = True 'Voglio SEMPRE usare la vecchia cerca sconti
        Case "1" : bUsaVecchiaCercaSconti = False 'Voglio SEMPRE usare la nuova cerca sconti
        Case Else 'non ho specificato, valuto quale usare
          If Not Me.GetType.ToString.StartsWith("NTSInformatica.CLE") Then 'Se inizia con CLE non è personalizzata
            'ottengo i metodi della classe ereditata (sono presenti solo quelli ereditati e specifici della classe figlia)
            If methodsBE Is Nothing Then methodsBE = Me.GetType.GetMethods(System.Reflection.BindingFlags.Public Or _
                                                                           System.Reflection.BindingFlags.Instance Or _
                                                                           System.Reflection.BindingFlags.DeclaredOnly)

            For Each method As System.Reflection.MethodInfo In methodsBE
              If method.Name.ToLower = "cercasconti" Then
                bUsaVecchiaCercaSconti = True
                Exit Select ' Non serve che controlli anche il BD, è già personalizzato
              End If
            Next
          End If

          If Not oCldComm.GetType.ToString.StartsWith("NTSInformatica.CLD") Then 'Se inizia con CLD non è personalizzata
            'ottengo i metodi della classe ereditata (sono presenti solo quelli ereditati e specifici della classe figlia)
            If methodsBD Is Nothing Then methodsBD = oCldComm.GetType.GetMethods(System.Reflection.BindingFlags.Public Or _
                                                                                 System.Reflection.BindingFlags.Instance Or _
                                                                                 System.Reflection.BindingFlags.DeclaredOnly)

            For Each method As System.Reflection.MethodInfo In methodsBD
              Dim strMethod As String = method.Name.ToLower
              If strMethod = "cercasconti" Then
                bUsaVecchiaCercaSconti = True
                Exit For
              End If
            Next
          End If
      End Select

      Select Case oCldComm.GetSettingBus("OPZIONI", ".", ".", "UsaVecchiaCercaProvvigioni", "0", ".", "0")
        Case "-1" : bUsaVecchiaCercaProvvigioni = True 'Voglio SEMPRE usare la vecchia cerca sconti
        Case "1" : bUsaVecchiaCercaProvvigioni = False 'Voglio SEMPRE usare la nuova cerca sconti
        Case Else 'non ho specificato, valuto quale usare
          If Not Me.GetType.ToString.StartsWith("NTSInformatica.CLE") Then 'Se inizia con CLE non è personalizzata
            'ottengo i metodi della classe ereditata (sono presenti solo quelli ereditati e specifici della classe figlia)
            If methodsBE Is Nothing Then methodsBE = Me.GetType.GetMethods(System.Reflection.BindingFlags.Public Or _
                                                                           System.Reflection.BindingFlags.Instance Or _
                                                                           System.Reflection.BindingFlags.DeclaredOnly)

            For Each method As System.Reflection.MethodInfo In methodsBE
              If method.Name.ToLower = "cercaprovv" Then
                bUsaVecchiaCercaProvvigioni = True
                Exit Select ' Non serve che controlli anche il BD, è già personalizzato
              End If
            Next
          End If

          If Not oCldComm.GetType.ToString.StartsWith("NTSInformatica.CLD") Then 'Se inizia con CLD non è personalizzata
            'ottengo i metodi della classe ereditata (sono presenti solo quelli ereditati e specifici della classe figlia)
            If methodsBD Is Nothing Then methodsBD = oCldComm.GetType.GetMethods(System.Reflection.BindingFlags.Public Or _
                                                                                 System.Reflection.BindingFlags.Instance Or _
                                                                                 System.Reflection.BindingFlags.DeclaredOnly)

            For Each method As System.Reflection.MethodInfo In methodsBD
              Dim strMethod As String = method.Name.ToLower
              If strMethod = "cercaprovv" Then
                bUsaVecchiaCercaProvvigioni = True
                Exit For
              End If
            Next
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
  Public Overridable Function CercaCondCommerciali(ByVal oCondCommerciali As NTSCondCommerciali) As Boolean
    'le chiamate alla cerca prezzo\cerca sconti vecchie sono tutte su un riga per evitare di riempire il codice con solo il loro passaggio dei parametri
    Try
      With oCondCommerciali
        If .Input.dQuant = 0 Then .Input.dQuant = .Input.dColli
        If .Input.strUnmis.Trim = "" Then .Input.strUnmis = .Input.strUmp

        If .bCalcolaPrezzo Then 'Devo calcolare il prezzo
          If bUsaVecchiaCercaPrezzo Then
            If .Input.strWhereArtico.Trim <> "" Then 'Chiamata da BNMGSTLI
              CercaPrezzoMultiArti(.Input.strDitta, .Input.nCodlavo, .Input.lConto, .Input.nListino, .Input.strUnmis, .Input.strTipoval, .Input.bConspromo, .Input.nCodpromo, .Input.lLotto, .Input.dtDatdoc, .Input.nCodvalu, .Input.dQuant, .Input.bNoPrelist, .Input.lListaSel, .Input.strWhereArtico, .OutputPrezzo.dttListini)
            Else
              'Voglio che sia chiamata la vecchia CercaPrezzo
              .OutputPrezzo.strCodCas = .Input.strCodCas ' è un parametro passato in input ma che ritorna anche un output
              If .Input.bConsideraPrezziUnmis Then 'Se deve considerare i prezzi per unità di misura, allora deve fare la "tripla" ricerca in base all'articolo
                If .Input.bPrezziPerUnmis Then
                  CercaPrezzo(.Input.strDitta, .Input.strCodart, .Input.nCodlavo, .Input.lConto, .Input.nListino, .Input.strUnmis, .Input.nFase, .Input.strTipoval, .Input.bConspromo, .Input.nCodpromo, .Input.lLotto, .Input.dtDatdoc, .Input.nCodvalu, .Input.dColli, .OutputPrezzo.dPrezzo, .OutputPrezzo.dPrelist, .OutputPrezzo.nCodpromo, .OutputPrezzo.lProgr, .OutputPrezzo.strPrzNet, .OutputPrezzo.strTipo, .OutputPrezzo.nPerqta, .OutputPrezzo.dDaQuant, .OutputPrezzo.dAquant, .OutputPrezzo.dPerqta, .OutputPrezzo.strUnmis, .OutputPrezzo.strTipoval, .OutputPrezzo.strCodCas, .OutputPrezzo.strErr, .OutputPrezzo.dtDatAgg, .OutputPrezzo.dtAData, .Input.bNoPrelist)
                  If .OutputPrezzo.dPrezzo = 0 Then
                    CercaPrezzo(.Input.strDitta, .Input.strCodart, .Input.nCodlavo, .Input.lConto, .Input.nListino, .Input.strUmp, .Input.nFase, .Input.strTipoval, .Input.bConspromo, .Input.nCodpromo, .Input.lLotto, .Input.dtDatdoc, .Input.nCodvalu, .Input.dQuant, .OutputPrezzo.dPrezzo, .OutputPrezzo.dPrelist, .OutputPrezzo.nCodpromo, .OutputPrezzo.lProgr, .OutputPrezzo.strPrzNet, .OutputPrezzo.strTipo, .OutputPrezzo.nPerqta, .OutputPrezzo.dDaQuant, .OutputPrezzo.dAquant, .OutputPrezzo.dPerqta, .OutputPrezzo.strUnmis, .OutputPrezzo.strTipoval, .OutputPrezzo.strCodCas, .OutputPrezzo.strErr, .OutputPrezzo.dtDatAgg, .OutputPrezzo.dtAData, .Input.bNoPrelist)
                    .OutputPrezzo.bDoppiaRicerca = True
                  End If
                Else
                  CercaPrezzo(.Input.strDitta, .Input.strCodart, .Input.nCodlavo, .Input.lConto, .Input.nListino, NTSCStr(IIf(.Input.bGestionePrezzi, .Input.strUmp, "")), .Input.nFase, .Input.strTipoval, .Input.bConspromo, .Input.nCodpromo, .Input.lLotto, .Input.dtDatdoc, .Input.nCodvalu, .Input.dQuant, .OutputPrezzo.dPrezzo, .OutputPrezzo.dPrelist, .OutputPrezzo.nCodpromo, .OutputPrezzo.lProgr, .OutputPrezzo.strPrzNet, .OutputPrezzo.strTipo, .OutputPrezzo.nPerqta, .OutputPrezzo.dDaQuant, .OutputPrezzo.dAquant, .OutputPrezzo.dPerqta, .OutputPrezzo.strUnmis, .OutputPrezzo.strTipoval, .OutputPrezzo.strCodCas, .OutputPrezzo.strErr, .OutputPrezzo.dtDatAgg, .OutputPrezzo.dtAData, .Input.bNoPrelist)
                End If
              Else
                CercaPrezzo(.Input.strDitta, .Input.strCodart, .Input.nCodlavo, .Input.lConto, .Input.nListino, .Input.strUnmis, .Input.nFase, .Input.strTipoval, .Input.bConspromo, .Input.nCodpromo, .Input.lLotto, .Input.dtDatdoc, .Input.nCodvalu, .Input.dQuant, .OutputPrezzo.dPrezzo, .OutputPrezzo.dPrelist, .OutputPrezzo.nCodpromo, .OutputPrezzo.lProgr, .OutputPrezzo.strPrzNet, .OutputPrezzo.strTipo, .OutputPrezzo.nPerqta, .OutputPrezzo.dDaQuant, .OutputPrezzo.dAquant, .OutputPrezzo.dPerqta, .OutputPrezzo.strUnmis, .OutputPrezzo.strTipoval, .OutputPrezzo.strCodCas, .OutputPrezzo.strErr, .OutputPrezzo.dtDatAgg, .OutputPrezzo.dtAData, .Input.bNoPrelist)
              End If
            End If
          Else
            'Chiama la nuova Cerca Prezzo
            If Not oCldComm.CercaCondCommerciali_CercaPrezzo(oCondCommerciali) Then Return False
          End If

            'Devo convertire il prezzo e il prezzo di listino in base all'unità di misura
            If .OutputPrezzo.bDoppiaRicerca Then
              If .OutputPrezzo.dPrelist <> 0 Then ConvertiPrezzoperUM(.Input.strDitta, .Input.strCodart, _
                                                                      .Input.strUnmis, .OutputPrezzo.dPrelist, 0) 'prelist  sempre in valuta 0!!!
              If .OutputPrezzo.dPrezzo <> 0 Then ConvertiPrezzoperUM(.Input.strDitta, .Input.strCodart, _
                                                                     .Input.strUnmis, .OutputPrezzo.dPrezzo, .Input.nCodvalu)
            End If

            'Dopo il calcolo del prezzo, il prezzo netto di output diventa il prezzo netto di input per la cercasconti\cercaprovvigioni
            .Input.strPrzNet = .OutputPrezzo.strPrzNet
            If .Input.dDivisorePrezzo = 0 Then .Input.dDivisorePrezzo = 1
            .Input.dPrezzo = .OutputPrezzo.dPrezzo / .Input.dDivisorePrezzo
          End If

          If .bCalcolaSconti Then 'Devo calcolare il prezzo
            If bUsaVecchiaCercaSconti Then
              'Voglio che sia chiamata la vecchia CercaSconti
              CercaSconti(.Input.strDitta, .Input.strCodart, .Input.lConto, .Input.nClscar, .Input.nClscan, .Input.strTipoval, .Input.bConspromo, .Input.nCodpromo, .Input.dtDatdoc, .Input.dQuant, .OutputSconti.dSconto1, .OutputSconti.dSconto2, .OutputSconti.dSconto3, .OutputSconti.dSconto4, .OutputSconti.dSconto5, .OutputSconti.dSconto6, .OutputSconti.nPromo, .Input.strPrzNet, .OutputSconti.dtDaData, .OutputSconti.dtAData, .OutputSconti.nPerqta, .OutputSconti.dDaQuant, .OutputSconti.dAquant, .OutputSconti.strUnmis, .OutputSconti.strTipoval, .Input.nListino)
            Else
              'Chiama la nuova Cerca Sconti
              If Not oCldComm.CercaCondCommerciali_CercaSconti(oCondCommerciali) Then Return False
            End If

            'Dopo il calcolo degli sconti, questi vanno riaggiornati nel metodo di input
            .Input.dSconto1 = .OutputSconti.dSconto1
            .Input.dSconto2 = .OutputSconti.dSconto2
            .Input.dSconto3 = .OutputSconti.dSconto3
            .Input.dSconto4 = .OutputSconti.dSconto4
            .Input.dSconto5 = .OutputSconti.dSconto5
            .Input.dSconto6 = .OutputSconti.dSconto6
          End If

          If .bCalcolaProvvigioni Then 'Devo calcolare le provvigioni
            If bUsaVecchiaCercaProvvigioni Then
              'Voglio che sia chiamata la vecchia CercaProvv
              If .Input.nCodage1 <> 0 Then CercaProvv(.Input.strDitta, .Input.strCodart, .Input.lConto, .Input.nClprar, .Input.nClpran, .Input.strTipoval, .Input.bConspromo, .Input.nCodpromo, .Input.dtDatdoc, .Input.nCodage1, .Input.dSconto1, .Input.dSconto2, .Input.dSconto3, .Input.dSconto4, .Input.dSconto5, .Input.dSconto6, .Input.dScontT1, .Input.dScontT2, .Input.dPrezzo, .Input.strUnmis, .Input.dQuant, .OutputProvvAgente1.dProvv, .OutputProvvAgente1.strError, .OutputProvvAgente1.nPromo, .OutputProvvAgente1.dVprovv, .OutputProvvAgente1.strTipoval)
              If .Input.nCodage2 <> 0 Then CercaProvv(.Input.strDitta, .Input.strCodart, .Input.lConto, .Input.nClprar, .Input.nClpran, .Input.strTipoval, .Input.bConspromo, .Input.nCodpromo, .Input.dtDatdoc, .Input.nCodage2, .Input.dSconto1, .Input.dSconto2, .Input.dSconto3, .Input.dSconto4, .Input.dSconto5, .Input.dSconto6, .Input.dScontT1, .Input.dScontT2, .Input.dPrezzo, .Input.strUnmis, .Input.dQuant, .OutputProvvAgente2.dProvv, .OutputProvvAgente2.strError, .OutputProvvAgente2.nPromo, .OutputProvvAgente2.dVprovv, .OutputProvvAgente2.strTipoval)
            Else
              'Chiama la nuova Cerca Provvigioni
              If Not oCldComm.CercaCondCommerciali_CercaProvvigioni(oCondCommerciali) Then Return False
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

  Public Overridable Function CercaProvv(ByVal strDitta As String, ByVal strCodart As String, ByVal lConto As Integer, _
                            ByVal nClprar As Integer, ByVal nClpran As Integer, ByVal strTipoval As String, _
                            ByVal bConspromo As Boolean, ByVal nCodpromoin As Integer, ByVal dtDatdoc As Date, _
                            ByVal nCodage As Integer, ByVal dSc1 As Decimal, ByVal dSc2 As Decimal, _
                            ByVal dSc3 As Decimal, ByVal dSc4 As Decimal, ByVal dSc5 As Decimal, ByVal dSc6 As Decimal, _
                            ByVal dSt1 As Decimal, ByVal dSt2 As Decimal, ByVal dPrezzo As Decimal, _
                            ByVal strUnmisin As String, ByVal dQuant As Decimal, ByRef dProvv As Decimal, _
                            Optional ByRef strError As String = "", Optional ByRef nPromo As Integer = 0, _
                            Optional ByRef dVprovv As Decimal = 0, Optional ByRef strTipovalout As String = "") As Boolean
    Try
      Return oCldComm.CercaProvv(strDitta, strCodart, lConto, nClprar, nClpran, strTipoval, _
                             bConspromo, nCodpromoin, dtDatdoc, nCodage, dSc1, dSc2, _
                             dSc3, dSc4, dSc5, dSc6, dSt1, dSt2, dPrezzo, strUnmisin, dQuant, dProvv, _
                             strError, nPromo, dVprovv, strTipovalout)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function IsTerzista(ByVal strDitta As String, ByVal nMaga As Integer) As Boolean
    Try
      Return oCldComm.IsTerzista(strDitta, nMaga)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiArtpro(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, _
                              ByVal nMagaz As Integer, ByVal strDispMultiMag As String, ByRef dttArtPro As DataTable) As Boolean
    Try
      Return oCldComm.LeggiArtpro(strDitta, strCodart, nFase, nMagaz, strDispMultiMag, dttArtPro)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaEsistDaXpro(ByVal strDitta As String, ByVal strCodart As String, ByVal nCodmaga As Integer, _
                                 Optional ByVal lCommeca As Integer = 0, Optional ByVal lLotto As Integer = 0, _
                                 Optional ByVal nFase As Integer = 0, Optional ByVal strUbicaz As String = " ", _
                                 Optional ByRef dOrdin As Decimal = 0, Optional ByRef dImpeg As Decimal = 0, _
                                 Optional ByRef dPrenot As Decimal = 0) As Decimal
    Try
      Return oCldComm.TrovaEsistDaXpro(strDitta, strCodart, nCodmaga, lCommeca, lLotto, nFase, strUbicaz, dOrdin, dImpeg, dPrenot)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TrovaEsistDaXproTC(ByVal strDitta As String, ByVal strCodart As String, ByVal nCodmaga As Integer, ByVal strTaglia As String, _
                                 Optional ByVal lCommeca As Integer = 0, Optional ByVal lLotto As Integer = 0, _
                                 Optional ByVal nFase As Integer = 0, Optional ByVal strUbicaz As String = " ", _
                                 Optional ByRef dOrdin As Decimal = 0, Optional ByRef dImpeg As Decimal = 0, _
                                 Optional ByRef dPrenot As Decimal = 0) As Decimal
    Try
      Return oCldComm.TrovaEsistDaXproTC(strDitta, strCodart, nCodmaga, strTaglia, lCommeca, lLotto, nFase, strUbicaz, dOrdin, dImpeg, dPrenot)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiCodpagaCltipb(ByVal strDitta As String, ByVal lConto As Integer, ByVal nTpbf As Integer) As Integer
    Try
      Return oCldComm.LeggiCodpagaCltipb(strDitta, lConto, nTpbf)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaArticoUnMis(ByVal strDitta As String, ByVal strCodart As String) As String
    Try
      Return oCldComm.TrovaArticoUnMis(strDitta, strCodart)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub ConvertiPrezzoperUM(ByVal strDitta As String, ByVal strCodart As String, ByVal strUnMisin As String, ByRef dPrezzo As Decimal, ByVal nCodValu As Integer)
    Try
      oCldComm.ConvertiPrezzoperUM(strDitta, strCodart, strUnMisin, dPrezzo, nCodValu)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function GestioneUMPrezzo(ByVal bGestisciPrezzi As Boolean, _
                                ByVal strNomeprog As String, _
                                ByVal strTipork As String, _
                                ByVal bTerzista As Boolean, _
                                ByVal strCodart As String, _
                                ByVal strUnmis As String, _
                                ByVal strDitta As String) As Boolean
    Try
      Return oCldComm.GestioneUMPrezzo(bGestisciPrezzi, _
                                strNomeprog, _
                                strTipork, _
                                bTerzista, _
                                strCodart, _
                                strUnmis, _
                                strDitta)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ConvQuantUM(ByVal strDitta As String, _
                            ByVal strCodart As String, _
                            ByVal strUM As String, _
                            ByVal dQtaUM As Decimal, _
                            ByVal dMis1 As Decimal, _
                            ByVal dMis2 As Decimal, _
                            ByVal dMis3 As Decimal, _
                            ByVal strUM2 As String, _
                            ByRef dQtaUM2 As Decimal, _
                            ByRef strError As String, _
                            Optional ByVal nRound As Integer = -1, _
                            Optional ByVal strUnmis As String = ".", _
                            Optional ByVal strConfez2 As String = ".", _
                            Optional ByVal dQtacon2 As Decimal = -1, _
                            Optional ByVal strUnmis2 As String = ".", _
                            Optional ByVal dConver As Decimal = -1, _
                            Optional ByVal strFormula As String = ".", _
                            Optional ByVal strUnmis4 As String = ".") As Boolean
    Try
      Return oCldComm.ConvQuantUM(strDitta, _
                           strCodart, _
                           strUM, _
                           dQtaUM, _
                           dMis1, _
                           dMis2, _
                           dMis3, _
                           strUM2, _
                           dQtaUM2, _
                           strError, _
                           nRound, _
                           strUnmis, _
                           strConfez2, _
                           dQtacon2, _
                           strUnmis2, _
                           dConver, _
                           strFormula, _
                           strUnmis4)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ConvQuantUMP(ByVal strDitta As String, _
                            ByVal strCodart As String, _
                            ByVal strUM As String, _
                            ByVal dQtaUM As Decimal, _
                            ByVal dMis1 As Decimal, _
                            ByVal dMis2 As Decimal, _
                            ByVal dMis3 As Decimal, _
                            ByRef dQtaUMP As Decimal, _
                            ByRef bDaMisure As Boolean, _
                            ByRef strError As String, _
                            Optional ByVal nRound As Integer = -1, _
                            Optional ByVal strUnmis As String = ".", _
                            Optional ByVal strConfez2 As String = ".", _
                            Optional ByVal dQtacon2 As Decimal = -1, _
                            Optional ByVal strUnmis2 As String = ".", _
                            Optional ByVal dConver As Decimal = -1, _
                            Optional ByVal strFormula As String = ".", _
                            Optional ByVal strUnmis4 As String = ".") As Boolean
    Try
      Return oCldComm.ConvQuantUMP(strDitta, _
                            strCodart, _
                            strUM, _
                            dQtaUM, _
                            dMis1, _
                            dMis2, _
                            dMis3, _
                            dQtaUMP, _
                            bDaMisure, _
                            strError, _
                            nRound, _
                            strUnmis, _
                            strConfez2, _
                            dQtacon2, _
                            strUnmis2, _
                            dConver, _
                            strFormula, _
                            strUnmis4)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      If ex.Message.IndexOf("°°°") > -1 Then
        'messaggio gestito di formula non corretta
        Dim i As Integer = ex.Message.IndexOf("°°°") + 3
        Dim l As Integer = ex.Message.IndexOf("°°°", i)
        ThrowRemoteEvent(New NTSEventArgs("", ex.Message.Substring(i, l - i)))
        dQtaUMP = 0
        Return True
      Else
        Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ConvQuantUMIntra2(ByVal strDitta As String, ByVal strCodart As String, _
                                              ByVal strUM As String, ByVal dQtaUM As Decimal, _
                                              ByVal dMis1 As Decimal, ByVal dMis2 As Decimal, _
                                              ByVal dMis3 As Decimal, ByRef dQtaUM2 As Decimal, _
                                              Optional ByVal nRound As Integer = 3) As Boolean
    Try
      Return oCldComm.ConvQuantUMIntra2(strDitta, strCodart, strUM, dQtaUM, dMis1, dMis2, dMis3, dQtaUM2, nRound)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub LeggiDisponibilita(ByVal strDitta As String, ByVal strCodart As String, ByVal nMagaz As Integer, _
                              ByVal nFase As Integer, ByVal lCommeca As Integer, ByRef dDisponibilita As Decimal, _
                              ByRef dDisponibilitaNetta As Decimal, ByRef dUltCost As Decimal, _
                              ByVal bRilevaDisponibPerCommessa As Boolean, ByVal strDispMultiMag As String, _
                              ByVal strTestScominCome As String, Optional ByVal strGescomm As String = "?")
    Try
      LeggiDisponibilita(strDitta, strCodart, nMagaz, nFase, lCommeca, dDisponibilita, dDisponibilitaNetta, dUltCost, _
                         bRilevaDisponibPerCommessa, strDispMultiMag, strTestScominCome, 0, 0, 0, strGescomm)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub LeggiDisponibilita(ByVal strDitta As String, ByVal strCodart As String, ByVal nMagaz As Integer, _
                            ByVal nFase As Integer, ByVal lCommeca As Integer, ByRef dDisponibilita As Decimal, _
                            ByRef dDisponibilitaNetta As Decimal, ByRef dUltCost As Decimal, _
                            ByVal bRilevaDisponibPerCommessa As Boolean, ByVal strDispMultiMag As String, _
                            ByVal strTestScominCome As String, ByRef dOrdinato As Decimal, _
                            ByRef dImpegnato As Decimal, ByRef dPrenotato As Decimal, _
                            Optional ByVal strGescomm As String = "?")
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nMagaz, nFase, lCommeca, dDisponibilita, dDisponibilitaNetta, dUltCost, _
                         bRilevaDisponibPerCommessa, strDispMultiMag, strTestScominCome, dOrdinato, dImpegnato, dPrenotato, strGescomm})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dDisponibilita = NTSCDec(oIn(5))
        dDisponibilitaNetta = NTSCDec(oIn(6))
        dUltCost = NTSCDec(oIn(7))
        dOrdinato = NTSCDec(oIn(11))
        dImpegnato = NTSCDec(oIn(12))
        dPrenotato = NTSCDec(oIn(13))
        Return
      End If
      '----------------

      oCldComm.LeggiDisponibilita(strDitta, strCodart, nMagaz, _
                               nFase, lCommeca, dDisponibilita, _
                               dDisponibilitaNetta, dUltCost, _
                               dOrdinato, dImpegnato, dPrenotato, _
                               bRilevaDisponibPerCommessa, strDispMultiMag, _
                               strTestScominCome, strGescomm)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function TrovaUbicazionePerMagazzino(ByVal strDitta As String, ByVal strCodart As String, ByVal nMagaz As Integer) As String
    Try
      TrovaUbicazionePerMagazzino = ""
      Return oCldComm.TrovaUbicazionePerMagazzino(strDitta, strCodart, nMagaz)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AgenteGestitoSCPR(ByVal strDitta As String, ByVal nCodage As Integer) As Boolean
    Try
      Return oCldComm.AgenteGestitoSCPR(strDitta, nCodage)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaCodartDescrLingua(ByVal strDitta As String, ByVal strCodart As String, ByVal nLingua As Integer, _
                                         ByRef strDescr As String, ByRef strDesint As String, ByRef strNote As String) As Boolean
    Try
      Return oCldComm.TrovaCodartDescrLingua(strDitta, strCodart, nLingua, strDescr, strDesint, strNote)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaCodarfoDaCodart(ByVal strCodArt As String, ByVal lContoCli As Integer, _
                                     ByVal strDitta As String, ByVal bCodarfoDaBarcode As Boolean, _
                                     ByVal strBarcode As String) As String
    Try
      TrovaCodarfoDaCodart = ""
      Return oCldComm.TrovaCodarfoDaCodart(strCodArt, lContoCli, strDitta, bCodarfoDaBarcode, strBarcode)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TrovaCodartDaBarcode(ByVal strChild As String, ByVal strBarcode As String, ByVal strDitta As String, _
                                       ByRef strUnmis As String, ByRef dQuant As Decimal, _
                                       ByRef dColli As Decimal, ByRef nFaseBC As Integer, ByRef strError As String) As String
    Try
      TrovaCodartDaBarcode = ""
      Return oCldComm.TrovaCodartDaBarcode(strChild, strBarcode, strDitta, strUnmis, dQuant, dColli, nFaseBC, strError)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TrovaCodartDaBarcode(ByVal strChild As String, ByVal strBarcode As String, ByVal strDitta As String, _
                                     ByRef strUnmis As String, ByRef dQuant As Decimal, _
                                     ByRef dColli As Decimal, ByRef nFaseBC As Integer, ByRef strError As String, _
                                     ByRef dsBarcode As DataSet) As String
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strChild, strBarcode, strDitta, strUnmis, dQuant, dColli, nFaseBC, strError, dsBarcode})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strUnmis = NTSCStr(oIn(3))
        dQuant = NTSCDec(oIn(4))
        dColli = NTSCDec(oIn(5))
        nFaseBC = NTSCInt(oIn(6))
        strError = NTSCStr(oIn(7))
        dsBarcode = CType(oIn(8), DataSet)
        Return CStr(oOut)
      End If
      '----------------


      TrovaCodartDaBarcode = ""
      Return oCldComm.TrovaCodartDaBarcode(strChild, strBarcode, strDitta, strUnmis, dQuant, dColli, nFaseBC, strError, dsBarcode)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TrovaCodartDaCodarfo(ByVal strCodarfo As String, ByVal lContoCli As Integer, _
                                     ByVal strDitta As String, ByRef strError As String) As String
    Try
      TrovaCodartDaCodarfo = ""
      Return oCldComm.TrovaCodartDaCodarfo(strCodarfo, lContoCli, strDitta, strError)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LegOrgaDitt(ByVal strDitta As String, ByVal strValuein As String, _
                                   ByVal bObbl As Boolean, ByRef strOut As String, ByRef strError As String) As Boolean
    '------------------------------------------
    'Effettua la lettura di un codice contatto-risorsa dalla tabella 
    'organig e tabcont per ottenere la decodifica opportuna
    'Unificato il funzionamento con la LegOrgaGrDitt
    '------------------------------------------
    Dim dsOrga As DataSet = Nothing
    Try
      LegOrgaDitt = False
      '------------------------------------------------------------------
      If NTSCInt(strValuein) = 0 And bObbl = False Then
        strOut = ""
        Return False
      End If
      '------------------------------------------------------------------
      If Val(strValuein) = 0 Then
        strError = "Risorsa-Contatto obbligatorio."
        strOut = ""
        Return False
      End If
      '------------------------------------------------------------------
      strValuein = NTSCStr(NTSCInt(strValuein))

      oCldComm.LegOrgaDitt(strDitta, strValuein, bObbl, strOut, strError, dsOrga)

      If dsOrga.Tables("ORGANIG").Rows.Count = 0 Then
        strError = "Risorsa-Contatto inesistente."
        strOut = ""
      Else
        If NTSCStr(dsOrga.Tables("ORGANIG").Rows(0)!tb_descont) = "" And NTSCStr(dsOrga.Tables("ORGANIG").Rows(0)!og_descont) = "" Then
          strError = "Dati insufficenti per effettuare la decodifica."
          strOut = ""
        Else
          If NTSCInt(dsOrga.Tables("ORGANIG").Rows(0)!og_codcont) = 0 Then
            strOut = NTSCStr(dsOrga.Tables("ORGANIG").Rows(0)!og_descont) & " " & NTSCStr(dsOrga.Tables("ORGANIG").Rows(0)!og_descont2)
          Else
            strOut = NTSCStr(dsOrga.Tables("ORGANIG").Rows(0)!tb_descont) & " " & NTSCStr(dsOrga.Tables("ORGANIG").Rows(0)!tb_descont2)
          End If
          Return True
        End If
      End If
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ScriviAllole(ByVal strDitta As String, ByVal strDLLChiamante As String, ByVal strTipo As String, _
                       ByVal strTipork As String, ByVal strTipodoc As String, _
                       ByVal nAnnoDoc As Integer, ByVal strSerieDoc As String, _
                       ByVal lNumdoc As Integer, ByVal lRigadoc As Integer, _
                       ByVal lCodice As Integer, ByVal lControp As Integer, _
                       ByVal strStrcod As String, ByVal lCommeca As Integer, _
                       ByVal lLotto As Integer, ByVal strMatric As String, _
                       ByVal strDescr As String, ByVal bSovrascrivi As Boolean, _
                       ByVal strPercorsoFile As String, Optional ByVal lCodlead As Integer = 0, _
                       Optional ByVal lCodoppo As Integer = 0, Optional ByVal lCodchia As Integer = 0, _
                       Optional ByVal lNumcontr As Integer = 0) As Boolean
    Dim bEseguiFunzione As Boolean
    Dim bEsisteAllole As Boolean
    Dim lProgresl As Integer
    Dim strPathRegistro As String
    Dim strCartella As String
    Dim strNomedoc As String
    Dim strTmp As String
    Dim dsTmp As DataSet = Nothing
    Try

      '--------------------------
      '--- Se l'opzione di registro legata al programma chiamante è falsa
      '--- esce dalla funzione senza elaborare/inserire niente
      '--------------------------
      bEseguiFunzione = CBool(oCldComm.GetSettingBusDitt(strDitta, strDLLChiamante, "OPZIONI", ".", "GeneraAllole", "0", " ", "0"))
      If bEseguiFunzione = False Then Return False
      '--------------------------

      '--------------------------
      '--- Legge il percorso di default sulla cartella OPZIONI di BS--AOLE
      '--------------------------
      strPathRegistro = oCldComm.GetSettingBusDitt(strDitta, "BS--AOLE", "OPZIONI", ".", "Cartella_per_documenti", oApp.OfficeDir, " ", oApp.OfficeDir)
      '--------------------------
      '--- Il percorso completo del file passato da programma, se inizia con il percorso
      '--- dell'opzione di registro 'Cartella_per_documenti' allora viene tolto.
      '--- Se resta solo il nome del file, viene messo nel campo ao_nomedoc,
      '--- altrimenti viene spezzato e il nome del file viene messo in ao_nomedoc
      '--- e la parte residua in ao_cartella.
      '--- Se invece il percorso completo del file passato da programma NON inizia
      '--- con il percorso dell'opzione di registro 'Cartella_per_documenti'
      '--- allora viene messo tutto in ao_nomedoc
      '--------------------------
      strCartella = ""
      strNomedoc = ""
      '--------------------------
      If LCase(Mid(strPercorsoFile, 1, Len(strPathRegistro))) = LCase(strPathRegistro) Then
        strTmp = Mid(strPercorsoFile, (Len(strPathRegistro) + 1))
        If InStr(1, strTmp, "\") <> 0 Then
          strNomedoc = StrReverse(Mid(StrReverse(strTmp), 1, InStr(1, StrReverse(strTmp), "\") - 1))
          strCartella = StrReverse(Mid(StrReverse(strTmp), InStr(1, StrReverse(strTmp), "\") + 1))
        Else
          strNomedoc = strTmp
        End If
      Else
        strNomedoc = strPercorsoFile
      End If
      '--------------------------
      '--- Se il flag di ingresso per la sovrascrittura è settato a False
      '--- accoda il record ad ALLOLE senza controllare che ne esista già uno con
      '--- le stesse caratterisctiche.
      '--- Altrimenti controlla che esista o meno e fa la INSERT INTO o l'UPDATE
      '--- a seconda dei casi
      '--------------------------
      If bSovrascrivi = False Then
        oCldComm.SalvaAllole(strDitta, strTipo, lCodice, strStrcod, _
                                           strTipodoc, nAnnoDoc, strSerieDoc, lNumdoc, _
                                           lRigadoc, lCommeca, lLotto, strMatric, _
                                           strDescr, strCartella, strNomedoc, strTipork, _
                                           lControp, lCodlead, lCodoppo, lCodchia, _
                                           lNumcontr)
      Else
        oCldComm.GetAllole(strDitta, strTipo, strTipork, strNomedoc, _
                                         strStrcod, lCodice, lCommeca, lLotto, _
                                         strTipodoc, nAnnoDoc, strSerieDoc, lNumdoc, _
                                         strMatric, lRigadoc, lCodlead, dsTmp)

        If dsTmp.Tables("ALLOLE").Rows.Count = 0 Then
          bEsisteAllole = False
        Else
          bEsisteAllole = True
          lProgresl = NTSCInt(dsTmp.Tables("ALLOLE").Rows(0)!ao_progress)
        End If

        oCldComm.SovrascriviAllole(strDitta, bEsisteAllole, strTipo, lCodice, _
                                   strStrcod, strTipodoc, nAnnoDoc, strSerieDoc, _
                                   lNumdoc, lRigadoc, lCommeca, lLotto, _
                                   strMatric, strDescr, strCartella, strNomedoc, _
                                   strTipork, lControp, lCodlead, lCodoppo, _
                                   lCodchia, lNumcontr, lProgresl)
      End If
      '--------------------------
      Return True
      '--------------------------

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CreaPicking(ByVal strCaller As String, ByVal strDitta As String, ByVal strTipork As String, _
                                      ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                      ByRef lIdpick As Integer, Optional ByVal bAggiungiFile As Boolean = False, _
                                      Optional ByVal bNonAggiungiBarcode As Boolean = False) As Boolean
    '------------------------
    'genera un file di picking a partire dalla nota di prelievo selezionata.
    'non genera il file se è già stato preso in carico da un terminalino
    'se viene passato lIdpick <> 0 vuol dire che il documento è una nota di prelievo in modfiica, per cui prima va cancellato il vecchio file
    'bAggiungiFile True aggiunge nel file di testo senza cancellarlo
    'bNonAggiungiBarcode True non aggiunge i barcode
    '------------ AGGIUNTE NUOVO PICKING ------------------
    ' La gestione è rimasta uguale, ma prima di salvare i dati o cmq di lavorare sui file .pik fa un controllo se la versione è quella nuova
    ' o quella vecchia, e decide se generare il file o scrivere sulla tabella
    Dim lRigaa, i As Integer
    Dim strRiga As String
    Dim strDescr1 As String
    Dim strDescr2 As String
    Dim strIndir As String
    Dim strCap As String
    Dim strCitta As String
    Dim strProv As String
    Dim strMatric As String
    Dim strLottoMatr As String
    Dim dttTestprb As New DataTable
    Dim dttMovprb As New DataTable
    Dim dttTmp As New DataTable
    Dim strNomeFile As String = ""
    Dim lCodNota As Integer
    Dim strErr As String = ""
    Dim w1 As System.IO.StreamWriter = Nothing
    'Aggiunte per il nuovo picking
    Dim bVecchioPick As Boolean = False
    Dim dttTestPic, dttMovPic, dttMaga As New DataTable
    Dim bGroupage As Boolean
    Dim lRigaMovPic As Integer = 1
    Dim dttLotto As New DataTable
    Dim bFirst As Boolean = False
    Try
      If strTipork <> "W" Then Return True

      ' Se sono attivati entrambi i moduli (o nessuno dei 2) faccio decidere alla chiave di registro chi ha priorità
      If CBool(ModuliSupDittaDitt(strDitta) And bsModSupRME) AndAlso CBool(ModuliExtDittaDitt(strDitta) And bsModExtPIK) Then
        If oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "UsaNuovoPicking", "N", "", "N") = "N" Then bVecchioPick = True
      ElseIf CBool(ModuliSupDittaDitt(strDitta) And bsModSupRME) Then
        bVecchioPick = False
      ElseIf CBool(ModuliExtDittaDitt(strDitta) And bsModExtPIK) Then
        bVecchioPick = True
      Else
        If oCldComm.GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "UsaNuovoPicking", "N", "", "N") = "N" Then bVecchioPick = True
      End If


      If bVecchioPick Then If GetAscServDir() = "" Then Return True

      If lIdpick = 0 Then
        lIdpick = oCldComm.LegNuma(strDitta, "PK", " ", 0, True)
        lIdpick = oCldComm.AggNuma(strDitta, "PK", " ", 0, lIdpick, True, False, strErr)
        If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))

        If bVecchioPick Then
          strNomeFile = GetPikDir() & "PIK" & lIdpick.ToString("000000000") & ".PIK"
          'Controllo se per sbaglio il file esiste già, se esiste devo trovare un picking corretto
          While System.IO.File.Exists(strNomeFile)
            lIdpick = oCldComm.LegNuma(strDitta, "PK", " ", 0, True)
            lIdpick = oCldComm.AggNuma(strDitta, "PK", " ", 0, lIdpick, True, False, strErr)
            If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
            strNomeFile = GetPikDir() & "PIK" & lIdpick.ToString("000000000") & ".PIK"
          End While
        Else
          If Not oCldComm.CreaPickingControllaEsiste(strDitta, lIdpick, dttTmp) Then Return False
          'Controllo se per sbaglio il record esiste già, se esiste devo trovare un picking corretto
          While dttTmp.Rows.Count <> 0
            lIdpick = oCldComm.LegNuma(strDitta, "PK", " ", 0, True)
            lIdpick = oCldComm.AggNuma(strDitta, "PK", " ", 0, lIdpick, True, False, strErr)

            If Not oCldComm.CreaPickingControllaEsiste(strDitta, lIdpick, dttTmp) Then Return False
          End While
          bGroupage = False
          dttTmp.Clear()
        End If
      Else
        If bVecchioPick Then
          strNomeFile = GetPikDir() & "PIK" & lIdpick.ToString("000000000") & ".PIK"
          If bAggiungiFile = False Then System.IO.File.Delete(strNomeFile) 'se sono in aggiunta non cancello il file
        Else
          bGroupage = True
          If Not bAggiungiFile Then
            If Not oCldComm.CreaPickingCancellaVecchio(strDitta, lIdpick) Then Return False
            bGroupage = False
          End If
        End If
      End If

      lCodNota = 1

      '------------------------
      'Apre il file/Datatable
      '------------------------p
      If bVecchioPick Then
        w1 = New System.IO.StreamWriter(strNomeFile, bAggiungiFile)
      Else
        oCldComm.GetTableStructure("TESTPIC", False, dttTestPic)
        oCldComm.GetTableStructure("MOVPIC", False, dttMovPic)
        oCldComm.SetTableDefaultValueFromDB("TESTPIC", dttTestPic.DataSet)
        oCldComm.SetTableDefaultValueFromDB("MOVPIC", dttMovPic.DataSet)
      End If

      '------------------------
      strDescr1 = ""
      strDescr2 = ""
      strIndir = ""
      strCap = ""
      strCitta = ""
      strProv = ""
      '------------------------
      'Apre uno recordset sul record di TESTPRB appena creato
      If Not oCldComm.CreaPickingLeggiNota(strDitta, strTipork, nAnno, strSerie, lNumdoc, dttTestprb) Then Return False
      If dttTestprb.Rows.Count = 0 Then Return False

      '------------------------
      'Compone la riga del file relativa alla testata
      With dttTestprb.Rows(0)
        If Not oCldComm.ValCodiceDb(NTSCStr(!tm_magaz), strDitta, "TABMAGA", "S", , dttMaga) Then Return False

        If NTSCInt(!tm_coddest) <> 0 Then
          oCldComm.ValCodiceDb(NTSCInt(!tm_coddest).ToString, strDitta, "DESTDIV", "N", "", dttTmp, !tm_conto.ToString)
          If dttTmp.Rows.Count = 0 Then
            dttTmp.Clear()
            oCldComm.ValCodiceDb(NTSCInt(!tm_conto).ToString, strDitta, "ANAGRA", "N", "", dttTmp)
            strDescr1 = NTSCStr(dttTmp.Rows(0)!an_descr1)
            strDescr2 = NTSCStr(dttTmp.Rows(0)!an_descr2)
            strIndir = NTSCStr(dttTmp.Rows(0)!an_indir)
            strCap = NTSCStr(dttTmp.Rows(0)!an_cap)
            strCitta = NTSCStr(dttTmp.Rows(0)!an_citta)
            strProv = NTSCStr(dttTmp.Rows(0)!an_prov)
          Else
            strDescr1 = NTSCStr(dttTmp.Rows(0)!dd_nomdest)
            strDescr2 = NTSCStr(dttTmp.Rows(0)!dd_nomdest2)
            strIndir = NTSCStr(dttTmp.Rows(0)!dd_inddest)
            strCap = NTSCStr(dttTmp.Rows(0)!dd_capdest)
            strCitta = NTSCStr(dttTmp.Rows(0)!dd_locdest)
            strProv = NTSCStr(dttTmp.Rows(0)!dd_prodest)
          End If
          dttTmp.Clear()
        Else
          oCldComm.ValCodiceDb(NTSCInt(!tm_conto).ToString, strDitta, "ANAGRA", "N", "", dttTmp)
          strDescr1 = NTSCStr(dttTmp.Rows(0)!an_descr1)
          strDescr2 = NTSCStr(dttTmp.Rows(0)!an_descr2)
          strIndir = NTSCStr(dttTmp.Rows(0)!an_indir)
          strCap = NTSCStr(dttTmp.Rows(0)!an_cap)
          strCitta = NTSCStr(dttTmp.Rows(0)!an_citta)
          strProv = NTSCStr(dttTmp.Rows(0)!an_prov)
        End If
        dttTmp.Clear()

        If bVecchioPick Then
          strRiga = "1|" & strDitta & "|" & CStr(lIdpick) & "|" & CStr(!tm_conto) & "|" & _
                    CStr(!tm_coddest) & "|" & strDescr1 & "|" & strDescr2 & "|" & strIndir & "|" & _
                    strCap & "|" & strCitta & "|" & strProv & "|" & strTipork & "|" & CStr(nAnno) & "|" & _
                    strSerie & "|" & CStr(lNumdoc) & "|" & NTSCDate(!tm_datdoc).ToString("ddMMyy") & "|" & _
                    CStr(!tm_conto2) & "|" & NTSCStr(!tm_riferim) & "|" & CStr(!tm_vettor) & "|" & _
                    NTSCStr(!tm_aspetto) & "|" & CStr(!tm_totcoll) & "|" & CStr(!tm_peso) & "|" & _
                    CStr(!tm_pesonetto) & "|" & Left(Replace(NTSCStr(!tm_note), vbCrLf, " "), 255) & "|N|-1|" & oApp.User.Nome & "|" & _
                    NTSCStr(!tb_desvett)

          '------------------------
          'Scrive la riga di testata nel file
          w1.WriteLine(strRiga)
        Else
          'Prepara i dati da salvare sul database
          dttTestPic.Rows.Add()
          dttTestPic.Rows(0)!codditt = strDitta
          dttTestPic.Rows(0)!tp_idpick = lIdpick
          dttTestPic.Rows(0)!tp_rigapick = 0 'Viene aggiornato con il valore corretto in fase di salvataggio
          If bGroupage Then
            ' Se il picking diventa un groupage aggiorna anche gli altri testpic con lo stesso idpick
            dttTestPic.Rows(0)!tp_tiporkp = "G"
            oCldComm.CraePickingGroupage(strDitta, lIdpick)
          Else
            dttTestPic.Rows(0)!tp_tiporkp = "P"
          End If
          dttTestPic.Rows(0)!tp_conto = !tm_conto
          dttTestPic.Rows(0)!tp_mxanno = nAnno
          dttTestPic.Rows(0)!tp_mxserie = strSerie
          dttTestPic.Rows(0)!tp_mxnumdoc = lNumdoc
          dttTestPic.Rows(0)!tp_mxtipork = strTipork
          dttTestPic.Rows(0)!tp_datdoc = !tm_datdoc
          dttTestPic.Rows(0)!tp_riferim = !tm_riferim
          dttTestPic.Rows(0)!tp_totcoll = !tm_totcoll
          dttTestPic.Rows(0)!tp_aspetto = !tm_aspetto
          dttTestPic.Rows(0)!tp_note = !tm_note
          dttTestPic.Rows(0)!tp_peso = !tm_peso
          dttTestPic.Rows(0)!tp_pesonetto = !tm_pesonetto
          dttTestPic.Rows(0)!tp_ultagg = Now
          dttTestPic.Rows(0)!tp_opnome = oApp.User.Nome
          dttTestPic.Rows(0)!tp_totvolume = 0
          dttTestPic.Rows(0)!tp_vistato = "N"
          dttTestPic.Rows(0)!tp_flchiusa = "N"
          dttTestPic.Rows(0)!tp_annpar = 0
          dttTestPic.Rows(0)!tp_numpar = 0
          dttTestPic.Rows(0)!tp_alfpar = " "
        End If
      End With    'With dttTestprb

      '------------------------
      'Chiude il recordset su TESTPRB
      dttTestprb.Clear()

      '------------------------
      'Apre un ulteriore recordset sulle righe sulle quali ciclare e inserire nel file
      If Not oCldComm.CreaPickingLeggiRigheNota(strDitta, strTipork, nAnno, strSerie, lNumdoc, dttMovprb) Then Return False
      '-------------------------
      'Gestione taglie e colore, spezza ulteriormente le righe, attiva solo sul nuovo picking
      If Not bVecchioPick Then 'Scorrendolo al contrario le righe aggiunte dopo non danno fastidio
        For z As Integer = dttMovprb.Rows.Count - 1 To 0 Step -1
          With dttMovprb.Rows(z)
            If NTSCInt(!ar_codtagl) > 0 Then
              oCldComm.ValCodiceDb(NTSCStr(!ar_codtagl), strDitta, "TABTAGL", "N", , dttTmp)
              bFirst = True
              For i = 1 To 24
                If NTSCDec(dttMovprb.Rows(z)("mm_quant" & i.ToString("00"))) > 0 Then
                  If bFirst Then
                    !mm_quant = dttMovprb.Rows(z)("mm_quant" & i.ToString("00"))
                    !mm_tctaglia = dttTmp.Rows(0)("tb_dest" & i.ToString("00"))
                    !mm_tcindtagl = !ar_codtagl
                    bFirst = False
                  Else
                    dttMovprb.Rows.Add()
                    dttMovprb.Rows(dttMovprb.Rows.Count - 1).ItemArray = .ItemArray
                    dttMovprb.Rows(dttMovprb.Rows.Count - 1)!mm_quant = dttMovprb.Rows(z)("mm_quant" & i.ToString("00"))
                    dttMovprb.Rows(dttMovprb.Rows.Count - 1)!mm_tctaglia = dttTmp.Rows(0)("tb_dest" & i.ToString("00"))
                    dttMovprb.Rows(dttMovprb.Rows.Count - 1)!mm_tcindtagl = !ar_codtagl
                  End If
                End If
              Next
            End If
          End With
        Next
      End If
      For Each dtrMovprb As DataRow In dttMovprb.Rows
        '---------------------------
        'Determina la matricola prelevando l'eventuale primo record in BARCODE relativo
        'all'articolo di riga
        strMatric = ""
        If Trim(NTSCStr(dtrMovprb!mm_codart)) <> "" Then
          'Prima cerco il barcode relativo all'unità di misura dei colli
          If Not oCldComm.CreaPickingGetFirstBarcode(strDitta, dtrMovprb!mm_codart.ToString, _
                                                    dtrMovprb!mm_unmis.ToString, dttTmp) Then Return False
          If dttTmp.Rows.Count > 0 Then
            strMatric = NTSCStr(dttTmp.Rows(0)!bc_code)
          Else
            dttTmp.Clear()
            'Cerco il barcode relativo all'unità di misura principale
            If Not oCldComm.CreaPickingGetFirstBarcode(strDitta, dtrMovprb!mm_codart.ToString, _
                                          dtrMovprb!mm_ump.ToString, dttTmp) Then Return False
            If dttTmp.Rows.Count > 0 Then
              strMatric = NTSCStr(dttTmp.Rows(0)!bc_code)
            Else
              dttTmp.Clear()
              'Cerco il barcode più recente
              If Not oCldComm.CreaPickingGetFirstBarcode(strDitta, dtrMovprb!mm_codart.ToString, _
                                          "", dttTmp) Then Return False
              If dttTmp.Rows.Count > 0 Then strMatric = NTSCStr(dttTmp.Rows(0)!bc_code)
            End If
          End If
          dttTmp.Clear()
        End If

        If Not oCldComm.CreaPickingGetMatricole(strDitta, dtrMovprb!mm_tipork.ToString, NTSCInt(dtrMovprb!mm_anno), _
                                                        dtrMovprb!mm_serie.ToString, NTSCInt(dtrMovprb!mm_numdoc), _
                                                        NTSCInt(dtrMovprb!mm_riga), dttTmp) Then Return False

        If dttTmp.Rows.Count = 0 Then
          '----------------------
          dttTmp.Clear()
          strLottoMatr = "N"
          If NTSCInt(dtrMovprb!mm_lotto) <> 0 Or (NTSCStr(dtrMovprb!mm_matric) <> "") Then strLottoMatr = "S"
          '----------------------
          With dtrMovprb
            If bVecchioPick Then
              strRiga = "2|" & strDitta & "|" & strTipork & "|" & CStr(nAnno) & "|" & strSerie & "|" & _
                        CStr(lNumdoc) & "|" & CStr(!mm_riga) & "|0|" & NTSCStr(!mm_codart) & "|" & _
                        IIf(NTSCDec(!mm_colli) <> 0, NTSCStr(!mm_unmis), NTSCStr(!mm_ump)).ToString & "|" & _
                        NTSCStr(!mm_descr) & "|" & _
                        IIf(NTSCDec(!mm_colli) <> 0, CStr(!mm_colli), CStr(!mm_quant)).ToString & "|" & _
                        CStr(!mm_quant) & "|" & strMatric & "|" & NTSCStr(!mm_stasino) & "|0|0|" & _
                        CStr(!mm_lotto) & "|" & NTSCStr(!mm_desint) & "|" & NTSCStr(!mm_ump) & "|" & _
                        NTSCStr(!mm_note) & "|" & CStr(!mm_fase) & "|" & _
                        NTSCStr(IIf(!ar_gesubic.ToString = "S", !mm_ubicaz, !ar_ubicaz)) & "|" & _
                        NTSCStr(!ar_gestmatr) & "|" & NTSCStr(!ar_geslotti) & "|" & _
                        CStr(lCodNota) & "||" & NTSCStr(!ar_unmis2) & "|" & CStr(!ar_conver) & "|" & _
                        NTSCStr(!ar_confez2) & "|" & CStr(!ar_qtacon2) & "||" & strLottoMatr & "|" & _
                        CStr(!ar_gesubic) & "|" & CStr(!mm_commeca)

              '-----------------------
              'Scrive la riga di testata nel file
              w1.WriteLine(strRiga)
            Else
              dttMovPic.Rows.Add()
              Dim dtrPic As DataRow = dttMovPic.Rows(dttMovPic.Rows.Count - 1)
              dtrPic!codditt = strDitta
              dtrPic!mp_idpick = lIdpick
              dtrPic!mp_rigapick = 0 ' Viene aggiornata in fase di salvataggio
              dtrPic!mp_riga = lRigaMovPic
              dtrPic!mp_codart = !mm_codart
              dtrPic!mp_unmis = !mm_unmis
              dtrPic!mp_colli = IIf(NTSCDec(!mm_colli) <> 0, !mm_colli, !mm_quant)
              dtrPic!mp_quant = !mm_quant
              dtrPic!mp_lotto = !mm_lotto

              ocldBase.ValCodiceDb(NTSCStr(!mm_lotto), strDitta, "ANALOTTI", "N", , dttLotto, NTSCStr(!mm_codart))
              If dttLotto.Rows.Count > 0 Then
                dtrPic!mp_codalflot = dttLotto.Rows(0)!alo_lottox
              Else
                dtrPic!mp_codalflot = " "
              End If

              dtrPic!mp_commeca = !mm_commeca
              dtrPic!mp_ump = !mm_ump
              dtrPic!mp_note = !mm_note
              dtrPic!mp_fase = !mm_fase
              dtrPic!mp_ubicaz = " "
              dtrPic!mp_ultagg = Now
              dtrPic!mp_tctaglia = !mm_tctaglia
              dtrPic!mp_tcindtagl = !mm_tcindtagl
              dtrPic!mp_mxanno = nAnno
              dtrPic!mp_mxnumdoc = lNumdoc
              dtrPic!mp_mxserie = strSerie
              dtrPic!mp_mxtipork = strTipork
              dtrPic!mp_mxriga = !mm_riga
              dtrPic!mp_subriga = 0
              dtrPic!mp_idpall = 0
              dtrPic!mp_rigapall = 0
              dtrPic!mp_numpack = " "
              dtrPic!mp_barcode = " "
              dtrPic!mp_barcoder = " "
              dtrPic!mp_flevas = "C"
              dtrPic!mp_rigapack = 0
              dtrPic!mp_flcompl = "N"
              dtrPic!mp_colliriscr = 0
              dtrPic!mp_quantriscr = 0


              lRigaMovPic += 1
            End If
          End With
        Else
          strLottoMatr = "S"
          lRigaa = 0
          For Each dtrTmp As DataRow In dttTmp.Rows
            With dtrTmp
              If bVecchioPick Then
                '---------------------
                strRiga = "2|" & strDitta & "|" & strTipork & "|" & CStr(nAnno) & "|" & _
                          strSerie & "|" & CStr(lNumdoc) & "|" & CStr(dtrMovprb!mm_riga) & "|" & CStr(lRigaa) & "|" & _
                          NTSCStr(dtrMovprb!mm_codart) & "|" & _
                          NTSCStr(dtrMovprb!mm_ump) & "|" & _
                          NTSCStr(dtrMovprb!mm_descr) & "|" & _
                          CStr(!mma_quant) & "|" & _
                          CStr(!mma_quant) & "|" & strMatric & "|" & NTSCStr(dtrMovprb!mm_stasino) & _
                          "|0|0|" & CStr(dtrMovprb!mm_lotto) & "|" & NTSCStr(dtrMovprb!mm_desint) & "|" & _
                          NTSCStr(dtrMovprb!mm_ump) & "|" & NTSCStr(dtrMovprb!mm_note) & "|" & CStr(dtrMovprb!mm_fase) & "|" & _
                          NTSCStr(IIf(dtrMovprb!ar_gesubic.ToString = "S", dtrMovprb!mm_ubicaz, dtrMovprb!ar_ubicaz)) & "|" & _
                          NTSCStr(dtrMovprb!ar_gestmatr) & "|" & _
                          NTSCStr(dtrMovprb!ar_geslotti) & "|" & CStr(lCodNota) & "||" & _
                          NTSCStr(dtrMovprb!ar_unmis2) & "|" & CStr(dtrMovprb!ar_conver) & "|" & _
                          NTSCStr(dtrMovprb!ar_confez2) & "|" & CStr(dtrMovprb!ar_qtacon2) & "|" & _
                          NTSCStr(!mma_matric) & "|" & strLottoMatr & "|" & _
                          CStr(dtrMovprb!ar_gesubic) & "|" & CStr(dtrMovprb!mm_commeca)
                '--------------------------
                'Scrive la riga di testata nel file
                w1.WriteLine(strRiga)
              Else
                dttMovPic.Rows.Add()
                Dim dtrPic As DataRow = dttMovPic.Rows(dttMovPic.Rows.Count - 1)
                dtrPic!codditt = strDitta
                dtrPic!mp_idpick = lIdpick
                dtrPic!mp_rigapick = 0 ' Viene aggiornata in fase di salvataggio
                dtrPic!mp_riga = lRigaa
                dtrPic!mp_codart = dtrMovprb!mm_codart
                dtrPic!mp_unmis = dtrMovprb!mm_unmis
                dtrPic!mp_colli = !mma_quant
                dtrPic!mp_quant = !mma_quant
                dtrPic!mp_matric = !mma_matric
                dtrPic!mp_lotto = dtrMovprb!mm_lotto

                ocldBase.ValCodiceDb(NTSCStr(dtrMovprb!mm_lotto), strDitta, "ANALOTTI", "N", , dttLotto, NTSCStr(dtrMovprb!mm_codart))
                If dttLotto.Rows.Count > 0 Then
                  dtrPic!mp_codalflot = dttLotto.Rows(0)!alo_lottox
                End If

                dtrPic!mp_commeca = dtrMovprb!mm_commeca
                dtrPic!mp_ump = dtrMovprb!mm_ump
                dtrPic!mp_note = dtrMovprb!mm_note
                dtrPic!mp_fase = dtrMovprb!mm_fase
                dtrPic!mp_ubicaz = " "
                dtrPic!mp_ultagg = Now
                dtrPic!mp_tctaglia = dtrMovprb!mm_tctaglia
                dtrPic!mp_tcindtagl = dtrMovprb!mm_tcindtagl
                dtrPic!mp_mxanno = nAnno
                dtrPic!mp_mxnumdoc = lNumdoc
                dtrPic!mp_mxserie = strSerie
                dtrPic!mp_mxtipork = strTipork
                dtrPic!mp_mxriga = dtrMovprb!mm_riga
                dtrPic!mp_subriga = 0
                dtrPic!mp_idpall = 0
                dtrPic!mp_rigapall = 0
                dtrPic!mp_numpack = " "
                dtrPic!mp_barcode = " "
                dtrPic!mp_barcoder = " "
                dtrPic!mp_flevas = "C"
                dtrPic!mp_rigapack = 0
                dtrPic!mp_flcompl = "N"
                dtrPic!mp_colliriscr = 0
                dtrPic!mp_quantriscr = 0

                lRigaMovPic += 1
              End If

              lRigaa = (lRigaa + 1)
            End With    'With dtrTmp
          Next    'For Each dtrTmp As DataRow In dttTmp.Rows
          dttTmp.Clear()
        End If    'If dttTmp.Rows.Count = 0 Then
      Next    'For Each dtrMovprb As DataRow In dttMovprb.Rows

      '------------------------
      'Chiude il recordset su MOVPRB
      dttMovprb.Clear()

      '------------------------
      'Aggiorna il campo di TESTPRB.tm_idpick
      If Not oCldComm.CreaPickingAggIdpickNota(strDitta, strTipork, nAnno, strSerie, lNumdoc, lIdpick) Then Return False

      '------------------------
      'Chiude il file di testo
      If bVecchioPick Then
        w1.Close()

        'default aggiunge i barcode
        If bNonAggiungiBarcode = False Then
          CreaPickingGetAllBarcode(strCaller, strDitta, lIdpick)
        End If
      Else
        If Not oCldComm.CreaPickingSalvaDati(strDitta, lIdpick, oApp.User.Nome, dttTestPic, dttMovPic) Then Return False
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
      dttTestprb.Clear()
      dttMovprb.Clear()
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function CreaPickingGetAllBarcode(ByVal strCaller As String, ByVal strDitta As String, _
                                                       ByVal lIdpick As Integer) As Boolean
    Dim dttTmp As New DataTable
    Dim strRiga As String = ""
    Dim w1 As System.IO.StreamWriter = Nothing
    Dim strNomeFile As String = ""
    Try
      strNomeFile = GetPikDir() & "PIK" & lIdpick.ToString("000000000") & ".PIK"

      '------------------------
      'Apre il file
      '------------------------p
      w1 = New System.IO.StreamWriter(strNomeFile, True)

      '------------------------
      'aggiungo i codici a barre
      If Not oCldComm.CreaPickingGetAllBarcode(strDitta, lIdpick, dttTmp) Then Return False
      For Each dtrTmp As DataRow In dttTmp.Rows
        strRiga = "3|" & strDitta & "|" & NTSCStr(dtrTmp!bc_code) & "|" & _
                  NTSCStr(dtrTmp!bc_codart) & "|" & CStr(dtrTmp!bc_fase) & "|" & _
                  NTSCStr(dtrTmp!bc_unmis) & "|" & _
                  Replace(NTSCDec(dtrTmp!bc_quant).ToString, ".", ",")
        w1.WriteLine(strRiga)
      Next
      dttTmp.Clear()

      '------------------------
      'Chiude il file di testo
      w1.Close()

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


  Public Overridable Function GetAscServDir() As String
    Dim strDir As String = ""
    Dim nPunto As Integer

    Try
      ' procedura che setta la dir di default per le immagini gif e jpg
      ' la dir di default è contenuta in strImagesDir (public nel modulo)

      If InStr(1, oApp.RptDir, "\") = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128655267064531250, "Non è stato possibile determinare la directory ASC sul server (nel reg. di windows la RptDir non ha nel percorso una '\')")))
        Return ""
      End If

      nPunto = 0
      While InStr(nPunto + 1, oApp.RptDir, "\") > 0
        nPunto = InStr(nPunto + 1, oApp.RptDir, "\")
      End While

      strDir = Left(oApp.RptDir, nPunto) & "Asc\"

      If System.IO.Directory.Exists(strDir) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128655267828125000, "Directory ASC sul server (|" & strDir & "|) non corretta")))
        Return ""
      End If

      Return strDir

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
  Public Overridable Function GetPikDir() As String
    Dim strDir As String = ""
    Try
      Return ocldBase.GetSettingBus("OPZIONI", ".", ".", "PikDir", GetAscServDir(), ".", GetAscServDir)
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


  Public Overridable Function GeneraNumCommeca(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, _
                                               ByVal strSerie As String, ByVal lNumord As Integer, _
                                               ByVal lRiga As Integer, ByVal nCodtpbf As Integer, _
                                               ByVal lConto As Integer, ByVal strCodart As String, _
                                               ByVal strDescr As String, ByVal strDesint As String, _
                                               ByVal strNote As String, ByVal strDatord As String, _
                                               ByRef strRetSubcommeca As String, ByVal strTipoDocOrd As String, _
                                               ByRef strDescrOut As String, ByRef strError As String) As Integer
    '-------------------------
    'Legge dal registro le due variabili di opzione
    'tipo generazione  (A=anno, S=serie, N=numero, R=riga)
    '1 = AASSNNNNN
    '2,0  = AASSNNNRR
    ' ,-1 = AASNNNNRR
    ' , 1 = AANNNNRRR
    '3 = AASSNNNNN + subcomm. RR
    '4 = numeraz. progressiva commesse (NNNNNNNNN)
    '5 = cod. sottogr. merc. (GG) + numeraz. progressiva commesse (NNNNNNN)
    Dim lComme As Integer
    Dim strSubcomme As String
    Dim nRigacom As Integer
    Dim strTipogen As String
    Dim strGenbudget As String
    Dim nSerie As Integer
    Dim nAnnoaa As Integer
    Dim nSecolo As Integer
    Dim strDescrArt As String
    Dim strDescr1 As String
    Dim strDescr2 As String
    Dim strDescr3 As String
    Dim dttTmp As New DataTable
    Dim strTotDescr As String = ""
    Dim strPadre As String
    Dim strTipoGen2New As String = ""
    Dim lNum As Integer
    Dim strGruppo As String
    Dim strNumero As String
    Dim strErr As String = ""
    Dim dsTmp As New DataSet

    Try
      strError = ""
      GeneraNumCommeca = 0

      strDescrArt = strDescr
      If strDescrArt = "" Then strDescrArt = " "
      '-----------------------
      Select Case strTipoDocOrd
        Case "O"
          strPadre = "BSORGSOR"
          strDescr = "Ordine n° " & lNumord & " del " & strDatord & " " & strDescr
        Case "L"
          strPadre = "BSDBLPRO"
          strDescr = "Lista prod.progr. " & lNumord * 100 + lRiga & " del " & strDatord & " " & strDescr
        Case "C3"
          strPadre = "BSCSGSER"
          strDescr = "Chiamata n. " & lNumord & " del " & strDatord
        Case Else
          strPadre = "BSVEBOLL"
          strDescr = "Documento n° " & lNumord & " del " & strDatord & " " & strDescr
      End Select

      '----------------------
      'Inizializza a vuoto e zero le variabili di ritorno, in caso di errore...
      strRetSubcommeca = " "
      strDescr1 = " "
      strDescr2 = " "
      strDescr3 = " "
      strSubcomme = " "

      '----------------------
      Select Case strTipoDocOrd
        Case "L"
          strTipogen = "2"
        Case "C", "C3"
          strTipogen = "4"
        Case Else
          strTipogen = oCldComm.GetSettingBusDitt(strDitta, strPadre, "OPZIONI", ".", "TipoGenNumCommeca", "1", " ", "1")
      End Select
      '----------------------
      strGenbudget = oCldComm.GetSettingBusDitt(strDitta, strPadre, "OPZIONI", ".", "GenBudgetGenNumCommeca", "-1", " ", "-1")
      If strTipogen = "2" Then
        strTipoGen2New = oCldComm.GetSettingBusDitt(strDitta, strPadre, "OPZIONI", ".", "TipoGenNumCommeca2New", "0", " ", "0")
      End If

      '--------------------------
      'Determina nrigacom
      If (lRiga > 99) And (strTipogen > "1") And (strTipogen < "4") Then
        If Not (strTipogen = "2" And strTipoGen2New = "1") Then
          strError = oApp.Tr(Me, 128552353215440000, "Non consentita numero riga superiore a 99 con questo tipo di gen. commessa!")
          Return 0
        Else
          If (lRiga > 999) Then
            strError = oApp.Tr(Me, 128552353215440001, "Non consentita numero riga superiore a 999 con questo tipo di gen. commessa!")
            Return 0
          End If
        End If
      End If
        '--------------------------
        If (strTipogen <> "2") Or (strTipogen = "2" And (strTipoGen2New <> "1")) Then
          nRigacom = NTSCInt(Microsoft.VisualBasic.Int(lRiga / 100))
          nRigacom = lRiga - nRigacom * 100
        Else
          nRigacom = NTSCInt(Microsoft.VisualBasic.Int(lRiga / 1000))
          nRigacom = lRiga - nRigacom * 1000
        End If
        '--------------------------
        'Determina 2 numeri per l'anno
        nSecolo = NTSCInt(Microsoft.VisualBasic.Int(nAnno / 100))
        nAnnoaa = nAnno - nSecolo * 100
        '--------------------------
        'Determina 2 numeri per la serie (o un numero)
        If (strTipogen <> "2") Or (strTipogen = "2" And (strTipoGen2New = "0")) Then
          '-----------------------
          'Due numeri per la serie
          If strSerie = " " Then
            nSerie = 0
          Else
            nSerie = NTSCInt(IIf(IsNumeric(strSerie), Asc(strSerie) - 48, Asc(strSerie) - 64))
          End If
        Else
          If strTipoGen2New = "-1" Then ' 1 solo numero per la serie
            If strSerie = " " Then
              nSerie = 0
            Else
              If Asc(strSerie) < 74 Then   ' controllare
                nSerie = NTSCInt(IIf(IsNumeric(strSerie), Asc(strSerie) - 48, Asc(strSerie) - 64))
              Else
                strError = oApp.Tr(Me, 128552352843224000, "Non consentita serie superiore a I con questo tipo di gen. commessa!")
                Return 0
              End If
            End If
          Else ' nessun numero per la serie
            'ok
          End If
        End If
        '--------------------------
        'Ora testa il numero documento
        If strTipoDocOrd <> "L" Then
          If lNumord > 999 And strTipogen = "2" And strTipoGen2New = "0" Then
            strError = oApp.Tr(Me, 128552352154016000, "Non consentito numero ordine superiore a 999 con questo tipo di gen. commessa !!!")
            Return 0
          End If
          If lNumord > 9999 And strTipogen = "2" And strTipoGen2New <> "0" Then
            strError = oApp.Tr(Me, 128552352456968000, "Non consentito numero ordine superiore a 9999 con questo tipo di gen. commessa !!!")
            Return 0
          End If
          If lNumord > 99999 And (strTipogen = "1" Or strTipogen = "3") Then
            strError = oApp.Tr(Me, 128552352602828000, "Non consentito numero ordine superiore a 99999 con questo tipo di gen. commessa !!!")
            Return 0
          End If
        End If
        '--------------------------
        'Ora compone il numero
        Select Case strTipogen
          Case "1"
            lComme = nAnnoaa * 10000000 + nSerie * 100000 + lNumord
            strTotDescr = strDescr & " " & strDesint & " " & strNote
          Case "2"
            If strTipoGen2New = "-1" Then lComme = nAnnoaa * 10000000 + nSerie * 1000000 + lNumord * 100 + nRigacom
            If strTipoGen2New = "0" Then lComme = nAnnoaa * 10000000 + nSerie * 100000 + lNumord * 100 + nRigacom
            If strTipoGen2New = "1" Then lComme = nAnnoaa * 10000000 + lNumord * 1000 + nRigacom
            strTotDescr = strDescr
            If strTipoDocOrd <> "L" Then
              strTotDescr = strTotDescr & " Riga:" & lRiga
            End If
            strTotDescr = strTotDescr & " " & strDesint & " " & strNote
          Case "3"
            lComme = nAnnoaa * 10000000 + nSerie * 100000 + lNumord
            strTotDescr = strDescr & " Riga:" & lRiga & " " & strDesint & " " & strNote
            strSubcomme = lRiga.ToString.PadLeft(2, "0"c).Substring(0, 2)
          Case "4" ' numero progr. di commessa
            lNum = oCldComm.LegNuma(strDitta, "CO", "", 0, True)
            lNum = oCldComm.AggNuma(strDitta, "CO", "", 0, lNum, True, False, strErr)
            If strErr <> "" Then
              strError = strErr
              Return 0
            End If
            lComme = lNum
            strTotDescr = strDescr
            If (strTipoDocOrd <> "L") And (strTipoDocOrd <> "C3") Then
              strTotDescr = strTotDescr & " Riga:" & lRiga
            End If
            strTotDescr = strTotDescr & " " & strDesint & " " & strNote
          Case "5" ' sottogruppo merc. + numero progr. commessa
            oCldComm.ValCodiceDb(strCodart, strDitta, "ARTICO", "S", "", dttTmp)
            If dttTmp.Rows.Count = 0 Then
              strError = oApp.Tr(Me, 128552411932234500, "GeneraNumCommeca: articolo |" & strCodart & "| inesistente")
              Return 0
            Else
              strGruppo = dttTmp.Rows(0)!ar_gruppo.ToString.PadLeft(2, "0"c).Substring(0, 2)
              dttTmp.Clear()
            End If
            lNum = oCldComm.LegNuma(strDitta, "CO", "", 0, True)
            lNum = oCldComm.AggNuma(strDitta, "CO", "", 0, lNum, True, False, strErr)
            If strErr <> "" Then
              strError = strErr
              Return 0
            End If
            If lNum > 9999999 Then
              strError = oApp.Tr(Me, 128552371205076000, "Non consentito numero commessa superiore a 9999999 con questo tipo di generazione commessa!")
              Return 0
            End If
            strNumero = lNum.ToString.PadLeft(7, "0"c).Substring(0, 7)
            lComme = NTSCInt(strGruppo & strNumero)
            strTotDescr = strDescr
            If (strTipoDocOrd <> "L") And (strTipoDocOrd <> "C3") Then
              strTotDescr = strTotDescr & " Riga:" & lRiga
            End If
            strTotDescr = strTotDescr & " " & strDesint & " " & strNote
        End Select
        '--------------------------
        If Len(strTotDescr) > 40 Then
          strDescr1 = Left(strTotDescr, 40)
        Else
          strDescr1 = Left(strTotDescr, Len(strTotDescr))
        End If
        '--------------------------
        If Len(strTotDescr) > 40 Then
          strDescr2 = Mid(strTotDescr, 41, 40)
          If Len(strTotDescr) > 80 Then
            strDescr3 = Mid(strTotDescr, 81, Len(strTotDescr) - 80)
          End If
        End If

        '--------------------------
        'Adesso crea il record in COMMESS
        oCldComm.ValCodiceDb(lComme.ToString, strDitta, "COMMESS", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          dttTmp.Clear()
          'Avverte solo se il tipo generazione NON è di tipo 3 o 1
          'dove nel n° di commessa non c'è la riga: verrebbe fuori questo messaggio
          'ad ogni riga di IC che si va ad inserire
          If strTipogen <> "3" And strTipogen <> "1" Then
            strError = oApp.Tr(Me, 128552414998292500, "La commessa n°|" & lComme.ToString & "| è già presente in archivio.")
          End If
          strDescrOut = strDescr1
          strRetSubcommeca = " "
        Else
          dsTmp.Tables.Add(dttTmp)
          dttTmp.TableName = "COMMESS"
          oCldComm.SetTableDefaultValueFromDB("COMMESS", dsTmp)
          dttTmp.Rows.Add(dttTmp.NewRow)
          With dttTmp.Rows(0)
            !codditt = strDitta
            !co_comme = lComme
            !co_conto = lConto
            !co_descr1 = strDescr1
            !co_descr2 = strDescr2
            !co_dtaper = NTSCDate(strDatord)
            !co_dtagg = NTSCDate(DateTime.Now.ToShortDateString)
            !co_dtchiu = NTSCDate(IntSetDate("31/12/2099"))
            !co_dtscad = NTSCDate(IntSetDate("31/12/2099"))
            !co_note = strDescr3
            If strTipork = "R" Or strTipork = "V" Then
              !co_tipork = strTipork
              !co_anno = nAnno
              !co_serie = strSerie
              !co_numord = lNumord
              !co_riga = NTSCInt(IIf(strTipogen = "3" Or strTipogen = "1", 0, lRiga))
            End If
            !co_listmat = 1
          End With
          If Not oCldComm.ScriviTabellaSemplice(strDitta, "COMMESS", dttTmp, "", "", "") Then Return 0
        End If    'If dttTmp.Rows.Count > 0 Then

        '--------------------------
        If strTipogen = "3" And lRiga > 0 Then
          oCldComm.ValCodiceDb(strSubcomme, strDitta, "SUBCOMM", "S", "", dttTmp, lComme.ToString)
          If dttTmp.Rows.Count > 0 Then
            dttTmp.Clear()
            strError = oApp.Tr(Me, lComme, "La sottocommessa '|" & strSubcomme & "|' è già presente in archivio.")
            strDescrOut = strDescr1
            strRetSubcommeca = strSubcomme
            Return lComme
          Else
            dsTmp.Tables.Clear()
            dsTmp.Tables.Add(dttTmp)
            dttTmp.TableName = "SUBCOMM"
            oCldComm.SetTableDefaultValueFromDB("SUBCOMM", dsTmp)
            dttTmp.Rows.Add(dttTmp.NewRow)
            With dttTmp.Rows(0)
              !codditt = strDitta
              !sco_commeca = lComme
              !sco_subcommeca = strSubcomme
              !sco_descr = strCodart
              !sco_note = strDescrArt
              If strTipork = "R" Then
                !sco_tipork = strTipork
                !sco_anno = nAnno
                !sco_serie = strSerie
                !sco_numord = lNumord
                !sco_riga = lRiga
              End If
            End With
            If Not oCldComm.ScriviTabellaSemplice(strDitta, "SUBCOMM", dttTmp, "", "", "") Then Return 0
          End If
          strRetSubcommeca = strSubcomme
        End If    'If strTipogen = "3" And lRiga > 0 Then

        '--------------------------
        'Adesso crea il budget ricopiandolo dallo schema budget
        If strGenbudget = "-1" Then oCldComm.GeneraNumcommeca_CreaBudget(strDitta, lComme)

        '--------------------------
        'Finito, ritorna i valori trovati
        strDescrOut = strDescr1
        Return lComme

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DateAddEx(ByVal strDitta As String, ByVal Interval As String, _
                                        ByVal Number As Decimal, ByVal Data As Date, _
                                        Optional ByVal nCodCentro As Integer = -1, _
                                        Optional ByVal nWeekDay As Integer = 0) As Date
    Dim lNumber As Integer
    Dim dtTemp As Date

    Try
      'Esce con Null se la data non è corretta
      If Not IsDate(Data) Then Return Nothing
      'Se l'intervallo non è 'd' si comporta come la DateDdd
      If Interval <> "d" Then Return DateAdd(Interval, Number, Data)
      'Converte il Double il Long
      lNumber = NTSCInt(Number)
      'Se il numero e 0 ritorna la data tal quale
      If lNumber = 0 Then Return Data
      'Chiamata alla funzione di controllo nel calendario di Business
      If Interval = "d" AndAlso nCodCentro <> -1 Then
        If oCldComm.CaricaCalCentro(strDitta, nCodCentro, lNumber, Data, dtTemp) Then
          DateAddEx = dtTemp
          Exit Function
        End If
      End If

      dtTemp = Data
      'Aggiunge (o diminuisce) i giorni escludendo Sabati e Domeniche
      If lNumber < 0 Then
        For i As Integer = 1 To Math.Abs(lNumber)
          'Devo tornare indietro
          Select Case dtTemp.DayOfWeek
            Case DayOfWeek.Monday : dtTemp = dtTemp.AddDays(-3) 'Se il giorno di partenza è lunedì salta domenica e sabato
            Case DayOfWeek.Sunday : dtTemp = dtTemp.AddDays(-2) 'Se il giorno di partenza è domenica salta il sabato
            Case Else : dtTemp = dtTemp.AddDays(-1) ' Per tutti gli altri giorni di partenza passa al giorno precedente
          End Select
        Next
      Else
        'Devo andare avanti
        For i As Integer = 1 To Math.Abs(lNumber)
          Select Case dtTemp.DayOfWeek
            Case DayOfWeek.Friday : dtTemp = dtTemp.AddDays(3) 'Se il giorno di partenza è venerdì salta sabato e domenica
            Case DayOfWeek.Saturday : dtTemp = dtTemp.AddDays(2) 'Se il giorno di partenza è sabato salta la domenica
            Case Else : dtTemp = dtTemp.AddDays(1) ' Per tutti gli altri giorni di partenza passa al giorno successivo
          End Select
        Next
      End If

      Return dtTemp
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return Data
    End Try
  End Function

#Region "CATALOGO FORNITORI"
  Public Overridable Function TestPreImportaArticoDaArtest(ByVal strDitta As String, ByRef strError As String) As Boolean
    Dim nLungRoot As Integer
    Dim nLungExt As Integer
    Dim nCodArtDaCat As Integer
    Dim nCodArtDaCatNListPubb As Integer
    Dim nCodArtDaCatNListIngr As Integer
    Dim bGestFaseDaCatalogo As Boolean
    Dim nLavorazioneDaCatalogo As Integer
    Dim dttTmp As New DataTable
    Dim strTmp As String = ""
    Try
      nCodArtDaCat = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoTipoGen", "0", " ", "0"))
      nCodArtDaCatNListPubb = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoNListPubb", "11", " ", "11"))
      nCodArtDaCatNListIngr = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoNListIngr", "12", " ", "12"))
      bGestFaseDaCatalogo = CBool(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "GestFaseDaCatalogo", "0", " ", "0"))
      nLavorazioneDaCatalogo = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "LavorazioneDaCatalogo", "200", " ", "200"))

      'controllo coerenza chiave registro con opzioni registro
      Select Case nCodArtDaCat
        Case 2

          nLungExt = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "GeneraCodiceLungExt", "0", " ", "0"))
          If Not nLungExt > 0 Then
            strError = "Valore nel Registro di Business: BSMGARTI-OPZIONI-GeneraCodiceLungExt" & vbCrLf & "non valido." & vbCrLf & _
            "Generazione articolo non possibile!"
            'esci acquisizione
            Return False
          End If

        Case 3

          nLungRoot = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "GeneraCodiceLungRoot", "0", " ", "0"))
          If Not nLungRoot > 0 Then
            strError = "Valore nel Registro di Business: BSMGARTI-OPZIONI-GeneraCodiceLungRoot" & vbCrLf & "non valido." & vbCrLf & _
            "Generazione articolo non possibile!"
            'esci acquisizione
            Return False
          End If
          nLungExt = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "GeneraCodiceLungExt", "0", " ", "0"))
          If Not nLungExt > 0 Then
            strError = "Valore nel Registro di Business: BSMGARTI-OPZIONI-GeneraCodiceLungExt" & vbCrLf & "non valido." & vbCrLf & _
              "Generazione articolo non possibile!"
            'esci acquisizione
            Return False
          End If
          If nLungRoot + nLungExt > CLN__STD.CodartMaxLen Then
            strError = "Somma dei valori nel Registro di Business:" & vbCrLf & _
            "BSMGARTI-OPZIONI-GeneraCodiceLungRoot e BSMGARTI-OPZIONI-GeneraCodiceLungExt" & vbCrLf & _
            "superiore a |" & CLN__STD.CodartMaxLen.ToString & "|. Generazione articolo non possibile!"
            'esci acquisizione
            Return False
          End If

      End Select

      If Not nCodArtDaCatNListPubb > 0 Then
        strError = "Valore nel Registro di Business:" & vbCrLf & _
        "BSMGARTI-OPZIONI-CodartDaCatalogoNListPubb" & vbCrLf & _
        "inferiore di 1. Generazione articolo non possibile!"
        'esci acquisizione
        Return False
      End If
      If Not nCodArtDaCatNListIngr > 0 Then
        strError = "Valore nel Registro di Business:" & vbCrLf & _
        "BSMGARTI-OPZIONI-CodartDaCatalogoNListIngr" & vbCrLf & _
        "inferiore di 1. Generazione articolo non possibile!"
        'esci acquisizione
        Return False
      End If

      'controlla la presenza e descrizione di dei codici 1-2-3 di tabling

      oCldComm.ValCodiceDb("1", strDitta, "TABLING", "N", strTmp, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        strError = "Codice lingua 1 mancante in tabella lingue." & vbCrLf & _
        "Generazione articolo non possibile!"
        'esci acquisizione
        Return False
      Else
        If Len(strTmp) = 0 Then
          strError = "Descrizione codice lingua 1 mancante in tabella lingue." & vbCrLf & _
          "Generazione articolo non possibile!"
          'esci acquisizione
          Return False
        End If
      End If

      oCldComm.ValCodiceDb("2", strDitta, "TABLING", "N", strTmp, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        strError = "Codice lingua 2 mancante in tabella lingue." & vbCrLf & _
        "Generazione articolo non possibile!"
        'esci acquisizione
        Return False
      Else
        If Len(strTmp) = 0 Then
          strError = "Descrizione codice lingua 2 mancante in tabella lingue." & vbCrLf & _
          "Generazione articolo non possibile!"
          'esci acquisizione
          Return False
        End If
      End If

      oCldComm.ValCodiceDb("3", strDitta, "TABLING", "N", strTmp, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        strError = "Codice lingua 3 mancante in tabella lingue." & vbCrLf & _
        "Generazione articolo non possibile!"
        'esci acquisizione
        Return False
      Else
        If Len(strTmp) = 0 Then
          strError = "Descrizione codice lingua 3 mancante in tabella lingue." & vbCrLf & _
          "Generazione articolo non possibile!"
          'esci acquisizione
          Return False
        End If
      End If

      If bGestFaseDaCatalogo Then
        oCldComm.ValCodiceDb(NTSCStr(nLavorazioneDaCatalogo), strDitta, "TABLAVO", "N", strTmp, dttTmp)
        If dttTmp.Rows.Count = 0 Then
          strError = "Codice lavorazione |" & nLavorazioneDaCatalogo & "| impostata nella opzione LavorazioneDaCatalogo non presente nella tabella delle lavorazioni." & vbCrLf & _
          "Generazione articolo non possibile!"
          'esci acquisizione
          Return False
        End If
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CreaArticoDaArtest(ByVal strDitta As String, ByVal strAe_codartf As String, _
                                                 ByVal strAe_forn As String, ByVal strAe_codmarc As String, _
                                                 ByRef strArticoCodArt As String, ByRef strError As String) As Boolean
    Dim nControa As Integer
    Dim nControp As Integer
    Dim nContros As Integer
    Dim nLungRoot As Integer
    Dim nLungExt As Integer
    Dim nCodArtDaCat As Integer
    Dim nCodArtDaCatNListPubb As Integer
    Dim nCodArtDaCatNListIngr As Integer
    Dim bGestFaseDaCatalogo As Boolean
    Dim nFaseDaCatalogo As Integer
    Dim nLavorazioneDaCatalogo As Integer
    Dim strDesFaseDaCatalogo As String
    Try

      nControa = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "Controa", "0", " ", "0"))
      nControp = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "Controp", "0", " ", "0"))
      nContros = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "Contros", "0", " ", "0"))
      nLungRoot = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "GeneraCodiceLungRoot", "0", " ", "0"))
      nLungExt = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "GeneraCodiceLungExt", "0", " ", "0"))
      nCodArtDaCat = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoTipoGen", "0", " ", "0"))
      nCodArtDaCatNListPubb = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoNListPubb", "11", " ", "11"))
      nCodArtDaCatNListIngr = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoNListIngr", "12", " ", "12"))
      bGestFaseDaCatalogo = CBool(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "GestFaseDaCatalogo", "0", " ", "0"))
      nFaseDaCatalogo = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "FaseDaCatalogo", "200", " ", "200"))
      nLavorazioneDaCatalogo = NTSCInt(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "LavorazioneDaCatalogo", "200", " ", "200"))
      strDesFaseDaCatalogo = NTSCStr(oCldComm.GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "DesFaseDaCatalogo", "FINITO", " ", "FINITO"))

      Return oCldComm.CreaArticoDaArtest(strDitta, strAe_codartf, _
                                         strAe_forn, strAe_codmarc, _
                                         strArticoCodArt, strError, _
                                         nControa, nControp, _
                                         nContros, nLungRoot, _
                                         nLungExt, nCodArtDaCat, _
                                         nCodArtDaCatNListPubb, nCodArtDaCatNListIngr, _
                                         bGestFaseDaCatalogo, nFaseDaCatalogo, _
                                         nLavorazioneDaCatalogo, strDesFaseDaCatalogo)

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region


  Public Overridable Function ScriviPrezzo(ByVal strDitta As String, ByVal strCodartListino As String, _
                                           ByVal nFase As Integer, ByVal nCodlavo As Integer, ByVal strTipo As String, _
                                           ByVal nListino As Integer, ByVal lConto As Integer, ByVal nCodvalu As Integer, _
                                           ByVal strUnmis As String, ByVal nCodtpro As Integer, ByVal dDaQuant As Decimal, _
                                           ByVal dtDatAgg As Date, ByVal dPrezzo As Decimal, ByVal dtDatscadPromo As Date, _
                                           ByVal dPerqta As Decimal, ByVal dAquant As Decimal, ByVal strNote As String, _
                                           ByVal strNetto As String, ByVal bUpdateexistingrk As Boolean) As Integer
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return ScriviPrezzo(strDitta, strCodartListino, nFase, nCodlavo, strTipo, nListino, lConto, nCodvalu, _
                          strUnmis, nCodtpro, dDaQuant, dtDatAgg, dPrezzo, dtDatscadPromo, dPerqta, dAquant, strNote, _
                          strNetto, bUpdateexistingrk, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    End Try
  End Function
  Public Overridable Function ScriviPrezzo(ByVal strDitta As String, ByVal strCodartListino As String, _
                                         ByVal nFase As Integer, ByVal nCodlavo As Integer, ByVal strTipo As String, _
                                         ByVal nListino As Integer, ByVal lConto As Integer, ByVal nCodvalu As Integer, _
                                         ByVal strUnmis As String, ByVal nCodtpro As Integer, ByVal dDaQuant As Decimal, _
                                         ByVal dtDatAgg As Date, ByVal dPrezzo As Decimal, ByVal dtDatscadPromo As Date, _
                                         ByVal dPerqta As Decimal, ByVal dAquant As Decimal, ByVal strNote As String, _
                                         ByVal strNetto As String, ByVal bUpdateexistingrk As Boolean, _
                                         ByVal strDataScadenzaListino As String) As Integer
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodartListino, nFase, nCodlavo, strTipo, nListino, _
                                             lConto, nCodvalu, strUnmis, nCodtpro, dDaQuant, dtDatAgg, dPrezzo, _
                                             dtDatscadPromo, dPerqta, dAquant, strNote, strNetto, bUpdateexistingrk, _
                                             strDataScadenzaListino})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCInt(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      Return ScriviPrezzo(strDitta, strCodartListino, nFase, nCodlavo, strTipo, nListino, lConto, nCodvalu, _
                          strUnmis, nCodtpro, dDaQuant, dtDatAgg, dPrezzo, dtDatscadPromo, dPerqta, dAquant, strNote, _
                          strNetto, bUpdateexistingrk, strDataScadenzaListino, 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    End Try
  End Function
  Public Overridable Function ScriviPrezzo(ByVal strDitta As String, ByVal strCodartListino As String, _
                                           ByVal nFase As Integer, ByVal nCodlavo As Integer, ByVal strTipo As String, _
                                           ByVal nListino As Integer, ByVal lConto As Integer, ByVal nCodvalu As Integer, _
                                           ByVal strUnmis As String, ByVal nCodtpro As Integer, ByVal dDaQuant As Decimal, _
                                           ByVal dtDatAgg As Date, ByVal dPrezzo As Decimal, ByVal dtDatscadPromo As Date, _
                                           ByVal dPerqta As Decimal, ByVal dAquant As Decimal, ByVal strNote As String, _
                                           ByVal strNetto As String, ByVal bUpdateexistingrk As Boolean, _
                                           ByVal strDataScadenzaListino As String, ByVal lCoddest As Integer) As Integer
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodartListino, nFase, nCodlavo, strTipo, nListino, _
                                             lConto, nCodvalu, strUnmis, nCodtpro, dDaQuant, dtDatAgg, dPrezzo, _
                                             dtDatscadPromo, dPerqta, dAquant, strNote, strNetto, bUpdateexistingrk, _
                                             strDataScadenzaListino, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCInt(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      Return oCldComm.ScriviPrezzo(strDitta, strCodartListino, nFase, nCodlavo, strTipo, nListino, lConto, nCodvalu, _
                                   strUnmis, nCodtpro, dDaQuant, dtDatAgg, dPrezzo, dtDatscadPromo, dPerqta, dAquant, strNote, _
                                   strNetto, bUpdateexistingrk, strDataScadenzaListino, lCoddest)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    End Try
  End Function
  Public Overridable Function ScriviSconto(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, _
                                        ByVal lConto As Integer, ByVal nClscan As Integer, ByVal nClscar As Integer, _
                                        ByVal nCodtpro As Integer, ByVal dDaQuant As Decimal, ByVal dAquant As Decimal, _
                                        ByVal dtDatAgg As Date, ByVal dtDatscadPromo As Date, ByVal strUnmis As String, _
                                        ByVal dScont1 As Decimal, ByVal dScont2 As Decimal, ByVal dScont3 As Decimal, _
                                        ByVal dScont4 As Decimal, ByVal dScont5 As Decimal, ByVal dScont6 As Decimal, _
                                        ByVal bUpdateexistingrk As Boolean) As Boolean
    '----------------------------------
    'Si occupa di inserire un nuovo record se non esiste la stessa combinazione
    'altrimenti se esiste ma con data validità anteriore fa scadere il record precedente e ne scrive uno nuovo
    'in caso contrario (data aggiornamento identica) sovrascrive
    Try
      'obosleta
      Return ScriviSconto(strDitta, strCodart, nFase, lConto, nClscan, nClscar, nCodtpro, dDaQuant, dAquant, _
                           dtDatAgg, dtDatscadPromo, strUnmis, dScont1, dScont2, dScont3, dScont4, dScont5, dScont6, _
                           bUpdateexistingrk, 0)

    Catch ex As Exception
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    End Try
  End Function
  Public Overridable Function ScriviSconto(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, _
                                        ByVal lConto As Integer, ByVal nClscan As Integer, ByVal nClscar As Integer, _
                                        ByVal nCodtpro As Integer, ByVal dDaQuant As Decimal, ByVal dAquant As Decimal, _
                                        ByVal dtDatAgg As Date, ByVal dtDatscadPromo As Date, ByVal strUnmis As String, _
                                        ByVal dScont1 As Decimal, ByVal dScont2 As Decimal, ByVal dScont3 As Decimal, _
                                        ByVal dScont4 As Decimal, ByVal dScont5 As Decimal, ByVal dScont6 As Decimal, _
                                        ByVal bUpdateexistingrk As Boolean, ByVal lCoddest As Integer) As Boolean
    '----------------------------------
    'Si occupa di inserire un nuovo record se non esiste la stessa combinazione
    'altrimenti se esiste ma con data validità anteriore fa scadere il record precedente e ne scrive uno nuovo
    'in caso contrario (data aggiornamento identica) sovrascrive
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nFase, lConto, nClscan, nClscar, nCodtpro, dDaQuant, dAquant, _
                                             dtDatAgg, dtDatscadPromo, strUnmis, dScont1, dScont2, dScont3, dScont4, dScont5, dScont6, _
                                             bUpdateexistingrk, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      Return oCldComm.ScriviSconto(strDitta, strCodart, nFase, lConto, nClscan, nClscar, nCodtpro, dDaQuant, dAquant, _
                                 dtDatAgg, dtDatscadPromo, strUnmis, dScont1, dScont2, dScont3, dScont4, dScont5, dScont6, _
                                 bUpdateexistingrk, lCoddest)

    Catch ex As Exception
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    End Try
  End Function
  Public Overridable Function ScriviProvvig(ByVal strDitta As String, ByVal strCodart As String, ByVal lCodcage As Integer, _
                                      ByVal lConto As Integer, ByVal nClpran As Integer, ByVal nClprar As Integer, _
                                      ByVal nCodtpro As Integer, ByVal dtDatAgg As Date, ByVal dtDatscadPromo As Date, _
                                      ByVal dProvv As Decimal, ByVal dVprovv As Decimal, ByVal strUnmis As String, _
                                      ByVal bUpdateexistingrk As Boolean) As Boolean
    Try
      'obosleta
      Return ScriviProvvig(strDitta, strCodart, lCodcage, lConto, nClpran, nClprar, _
                          nCodtpro, dtDatAgg, dtDatscadPromo, dProvv, dVprovv, strUnmis, _
                          bUpdateexistingrk, 0)
    Catch ex As Exception
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    End Try
  End Function
  Public Overridable Function ScriviProvvig(ByVal strDitta As String, ByVal strCodart As String, ByVal lCodcage As Integer, _
                                    ByVal lConto As Integer, ByVal nClpran As Integer, ByVal nClprar As Integer, _
                                    ByVal nCodtpro As Integer, ByVal dtDatAgg As Date, ByVal dtDatscadPromo As Date, _
                                    ByVal dProvv As Decimal, ByVal dVprovv As Decimal, ByVal strUnmis As String, _
                                    ByVal bUpdateexistingrk As Boolean, ByVal lCoddest As Integer) As Boolean
    '----------------------------------
    'Si occupa di inserire un nuovo record se non esiste la stessa combinazione
    'altrimenti se esiste ma con data validità anteriore fa scadere il record precedente e ne scrive uno nuovo
    'in caso contrario (data aggiornamento identica) sovrascrive
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, lCodcage, lConto, nClpran, nClprar, _
                                              nCodtpro, dtDatAgg, dtDatscadPromo, dProvv, dVprovv, strUnmis, _
                                              bUpdateexistingrk, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      Return oCldComm.ScriviProvvig(strDitta, strCodart, lCodcage, lConto, nClpran, nClprar, _
                                    nCodtpro, dtDatAgg, dtDatscadPromo, dProvv, dVprovv, strUnmis, _
                                    bUpdateexistingrk, lCoddest)
    Catch ex As Exception
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    End Try
  End Function

  Public Overridable Function LeggiCampiPerHlvl(ByVal strTabella As String, ByRef dttOut As DataTable) As Boolean
    Try
      Return oCldComm.LeggiCampiPerHlvl(strTabella, dttOut)

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
  Public Overridable Function TrovaNomeZoomHlvl(ByVal strCampo As String) As String
    'dato un nome di campo, restituisce lo zoom utilizzabile (solo per griglie tipo HLVL)
    Try
      If strCampo.ToUpper.IndexOf("CODART") > -1 Then Return "ZOOMARTICO"
      If strCampo.ToUpper.IndexOf("CONTO") > -1 Then Return "ZOOMANAGRAF"
      If strCampo.ToUpper.IndexOf("AGEN") > -1 Then Return "ZOOMTABCAGE"
      Select Case strCampo.ToUpper
        Case "ANAGRA.AN_CONTO", "BUDGET.BU_CONTO", "CODARFO.CAF_CONTO", "COMMESS.CO_CONTO", "DESTDIV.DD_CONTO", _
             "KEYMAG.KM_CONTO", "KEYORD.KO_CONTO", "KEYPRB.KM_CONTO", "LISTINI.LC_CONTO", "LISTSEL.LSE_CONTO", _
             "MOVIVA.MI_CONTO", "ORDLIST.OL_CONTO", "PERPROV.PER_CONTO", "PRIANA.PA_CONTO", "PRINOT.PN_CONTO", _
             "PROBUDGET.PB_CONTO", "PROVVIG.PV_CONTO", "SCADEN.SC_CONTO", "SCONTI.SO_CONTO", "TESTMAG.TM_CONTO", _
             "TESTORD.TD_CONTO", "TESTPRB.TM_CONTO", "ALLOLE.AO_CODICE", "MOVIVA.MI_CONTOCF", "PRINOT.PN_CONTOCF", _
             "TESTMAG.TM_CONTO2", "TESTPRB.TM_CONTO2", "LEADS.LE_CONTO", "LEADS.LE_CODPARTN", _
             "OPPORTUN.OP_CODPARTN", "OPPORTUN.OP_CONSEG", "TESTOFF.TD_CONTO", "TESTOFF.TD_CONTFATT"
          Return "ZOOMANAGRAC"
        Case "ARTICO.AR_FORN", "ARTICO.AR_FORN2", "TABCAGE.TB_CODFORN", "TABMAGA.TB_MAGCONTO"
          Return "ZOOMANAGRAF"
        Case "SCADEN.SC_CONTROP"
          Return "ZOOMANAGRAS"
        Case "ARTICO.AR_CODMARC"
          Return "ZOOMTABMARC"
        Case "ARTICO.AR_CODART", "ARTDEF.AD_CODART", "ARTDEFX.ADX_CODART", "ARTLIF.AL_CODART", _
             "ARTPRO.AP_CODART", "ARTPROX.APX_CODART", "ARTVAL.AX_CODART", "BARCODE.BC_CODART", _
             "BUDGETD.BUD_CODART", "CODARFO.CAF_CODART", "KEYMAG.KM_CODART", "KEYORD.KO_CODART", _
             "KEYPRB.KM_CODART", "LISTINI.LC_CODART", "MOVMAG.MM_CODART", "MOVORD.MO_CODART", _
             "MOVPRB.MM_CODART", "ORDLIST.OL_CODART", "PERPROV.PER_CODART", "PRIANA.PA_CODART", _
             "PROBUDGETD.PBD_CODART", "SCONTI.SO_CODART", "ZZAGAD.ZZ_CODART", "ZZXAGAD.ZZX_CODART", _
             "MOVOFF.MO_CODART"
          Return "ZOOMARTICO"
        Case "TABBANC.TB_CONBANC", _
             "TABCAUC.TB_CONTIVA", "TABCAUC.TB_CD_1", "TABCAUC.TB_CD_2", "TABCAUC.TB_CD_3", "TABCAUC.TB_CD_4", _
             "TABCAUC.TB_CA_1", "TABCAUC.TB_CA_2", "TABCAUC.TB_CA_3", "TABCAUC.TB_CA_4", _
             "TABPEAC.TB_ACONCAS", "TABPEAC.TB_ACONABAT", "TABPEAC.TB_ACONABPA", "TABPEAC.TB_ACONOMAG", _
             "TABPEAC.TB_ACONRIBO", "TABPEAC.TB_ACONRIIN", "TABPEAC.TB_ACONRIAC", "TABPEAC.TB_ACONCAUZ", _
             "TABPECG.TB_CONEFF", "TABPECG.TB_CONABAT", "TABPECG.TB_CONABPA", "TABPECG.TB_CONDCAT", _
             "TABPECG.TB_CONDCPA", "TABPEVE.TB_CONCAS", "TABPEVE.TB_CONABAT", "TABPEVE.TB_CONABPA", _
             "TABPEVE.TB_CONOMAG", "TABPEVE.TB_CONRIBO", "TABPEVE.TB_CONRIIN", "TABPEVE.TB_CONRIAC", _
             "TABPEVE.TB_CONCAUZ", "ANAGRA.AN_CODESE", "ARTICO.AR_CODIVA", "MOVIVA.MI_CODIVA", "MOVMAG.MM_CODIVA", _
             "MOVORD.MO_CODIVA", "MOVPRB.MM_CODIVA", "ORDLIST.OL_CODIVA", "PRINOT.PN_CODIVA", _
             "TABENAS.TB_CODIVFT", "TABPRIV.TB_PCODIVA", _
             "TESTMAG.TM_CODESE", "TESTMAG.TM_CODIVA_1", "TESTMAG.TM_CODIVA_2", "TESTMAG.TM_CODIVA_3", _
             "TESTMAG.TM_CODIVA_4", "TESTMAG.TM_CODIVA_5", "TESTMAG.TM_CODIVA_6", "TESTMAG.TM_CODIVA_7", _
             "TESTMAG.TM_CODIVA_8", "TESTORD.TD_CODESE", _
             "TESTPRB.TM_CODESE", "TESTPRB.TM_CODIVA_1", "TESTPRB.TM_CODIVA_2", "TESTPRB.TM_CODIVA_3", _
             "TESTPRB.TM_CODIVA_4", "TESTPRB.TM_CODIVA_5", "TESTPRB.TM_CODIVA_6", "TESTPRB.TM_CODIVA_7", _
             "TESTPRB.TM_CODIVA_8", "TESTOFF.TD_CODESE"
          Return "ZOOMTABCIVA"
        Case "ANAGRA.AN_CODPAG", "PROVVIG.PV_CODPAGA", "SCADEN.SC_CODPAGA", "TESTMAG.TM_CODPAGA", _
           "TESTORD.TD_CODPAGA", "TESTPRB.TM_CODPAGA"
          Return "ZOOMTABPAGA"
        Case "ANAGRA.AN_CODMAST", "PRIANA.PA_MASTRO", _
             "TABANAZ.TB_MASCLI_1", "TABANAZ.TB_MASCLI_2", "TABANAZ.TB_MASCLI_3", "TABANAZ.TB_MASCLI_4", _
             "TABANAZ.TB_MASCLI_5", "TABANAZ.TB_MASCLI_6", "TABANAZ.TB_MASCLI_7", "TABANAZ.TB_MASCLI_8", _
             "TABANAZ.TB_MASCLI_9", "TABANAZ.TB_MASCLI_10", "TABANAZ.TB_MASFOR_1", "TABANAZ.TB_MASFOR_2", _
             "TABANAZ.TB_MASFOR_3", "TABANAZ.TB_MASFOR_4", "TABANAZ.TB_MASFOR_5", "TABANAZ.TB_MASFOR_6", _
             "TABANAZ.TB_MASFOR_7", "TABANAZ.TB_MASFOR_8", "TABANAZ.TB_MASFOR_9", "TABANAZ.TB_MASFOR_10"
          Return "ZOOMTABMAST"
        Case "ANAGRA.AN_CODPAG", "PROVVIG.PV_CODPAGA", "SCADEN.SC_CODPAGA", "TESTMAG.TM_CODPAGA", _
             "TESTORD.TD_CODPAGA", "TESTPRB.TM_CODPAGA", "TESTOFF.TD_CODPAGA"
          Return "ZOOMTABPAGA"
        Case "ANAGRA.AN_PORTO", "TESTMAG.TM_PORTO", "TESTORD.TD_PORTO", "TESTPRB.TM_PORTO", "TESTOFF.TD_PORTO"
          Return "ZOOMTABPORT"
        Case "ANAGRA.AN_VALUTA", "ARTVAL.AX_CODVALU", "CAMBI.WXCODVALU", "LISTINI.LC_CODVALU", _
             "MOVIVA.MI_CODVALU", "ORDLIST.OL_CODVALU", "PRIANA.PA_CODVALU", "PRINOT.PNCODVALU", _
             "SCADEN.SC_CODVALU", "TESTMAG.TM_VALUTA", "TESTORD.TD_VALUTA", "TESTPRB.TM_VALUTA", _
             "TESTOFF.TD_VALUTA"
          Return "ZOOMTABVALU"
        Case "ANAGRA.AN_VETT", "TESTMAG.TM_VETTOR", "TESTORD.TD_VETTOR", "TESTPRB.TM_VETTOR"
          Return "ZOOMTABVETT"
        Case "ANAGRA.AN_ZONA", "DESTDIV.DD_CODZONA", "LEADS.LE_ZONA"
          Return "ZOOMTABZONE"
        Case "KEYMAG.KM_MAGAZ", "KEYORD.KO_MAGAZ", "KEYPRB.KM_MAGAZ", "ARTPRO.AP_MAGAZ", _
             "MOVMAG.MM_MAGAZ", "MOVMAG.MM_MAGAZ2", "MOVMAG.MM_MAGDISIMP", "MOVMAG.MM_MAGDISIMP2", _
             "MOVORD.MO_MAGAZ", "MOVORD.MO_MAGAZ2", _
             "MOVPRB.MM_MAGAZ", "MOVPRB.MM_MAGAZ2", "MOVPRB.MM_MAGDISIMP", "MOVPRB.MM_MAGDISIMP2", _
             "ORDLIST.OL_MAGAZ", "ORDLIST.OL_MAGAZ2", "TABTPBF.TB_TMAGAZZ", "TABTPBF.TB_TMAGAZZ2", _
             "TESTMAG.TM_MAGAZ", "TESTMAG.TM_MAGAZ2", "TESTORD.TD_MAGAZ", "TESTORD.TD_MAGAZ2", _
             "TESTPRB.TM_MAGAZ", "ARTDEF.AD_MAGAZ", "TESTPRB.TM_MAGAZ2"
          Return "ZOOMTABMAGA"
        Case "ARTICO.AR_CODPDON"
          Return "ZOOMTABPDON"
        Case "MOVIVA.MI_CONTROP", "PRINOT.PN_CONTROP", "ARTICO.AR_CONTROA", "ARTICO.AR_CONTROP", _
             "MOVMAG.MM_CONTROP", "MOVORD.MO_CONTROP", _
             "MOVPRB.MM_CONTROP", "ORDLIST.OL_CONTROP", "TABCOPE.TB_OCODCONTR", "TABTPBF.TB_TCONTRO", _
             "TESTMAG.TM_CONTROP", "TESTORD.TD_CONTROP", "TESTPRB.TM_CONTROP"
          Return "ZOOMTABCOVE"
        Case "ARTICO.AR_FAMPROD", "BUDGET.BU_CODCFAM", "BUDGETD.BUD_CODCFAM", "BUDGETDMEN.BUDM_CODCFAM", _
             "BUDGETMEN.BUM_CODCFAM", "MOVMAG.MM_CODCFAM", "MOVORD.MO_CODCFAM", "MOVPRB.MM_CODCFAM", _
             "ORDLIST.OL_CODCFAM", "PRIANA.PA_CODCFAM"
          Return "ZOOMTABCFAM"
        Case "ARTICO.AR_GRUPPO", "TABSGME.TB_CODGRUPM"
          Return "ZOOMTABGMER"
        Case "ARTICO.AR_SOTGRU"
          Return "ZOOMTABSGME"
        Case "BUDGET.BU_CODCENA", "BUDGETD.BUD_CODCENA", "BUDGETDMEN.BUDM_CODCENA", "BUDGETMEN.BUM_CODCENA", _
             "MOVMAG.MM_CODCENA", "MOVORD.MO_CODCENA", "MOVPRB.MM_CODCENA", "ORDLIST.OL_CODCENA", _
             "PRIANA.PA_CODCENA", "TABCOPE.TB_OCODCENA", "TABTPBF.TB_VCODCEN", "TESTMAG.TM_CODCENA", _
             "TESTORD.TD_CODCENA", "TESTPRB.TM_CODCENA"
          Return "ZOOMTABCENA"
        Case "PRIANA.PA_CODLAVO", "PROBUDGETD.PBD_CODLAVO", "TABCOPE.TB_OCODLAVO", "BUDGETD.BUD_CODLAVO"
          Return "ZOOMTABLAVO"
        Case "CONDBAN.CB_CAUSALE", "PRINOT.PN_CAUSALE", "SCADEN.SC_CAUSALE"
          Return "ZOOMTABCAUC"
        Case "CONDBAN.CB_CODBANC", "SCADEN.SC_CODBANC", "TASSI.TX_CODBANC"
          Return "ZOOMTABBANC"
        Case "LISTINI.LC_CODTPRO", "PERPROV.PER_CODTPRO"
          Return "ZOOMTABTPRO"
        Case "MOVMAG.MM_CAUSALE", "MOVMAG.MM_CAUSALE2", "MOVPRB.MM_CAUSALE", "MOVPRB.MM_CAUSALE2", _
             "TABTPBF.TB_TCAUMAG", "TABTPBF.TB_TCAUTRA", "TESTMAG.TMCAUSALE", "TESTMAG.CAUSTRA", _
             "TESTORD.TD_CAMBIO", "TESTPRB.TM_CAUSALE", "TESTPRB.TM_CAUSTRA"
          Return "ZOOMTABCAUM"
        Case "PRIANA.PA_CODCOPE"
          Return "ZOOMTABCOPE"
        Case "TABCAGE.TB_CODTBEN"
          Return "ZOOMTABENAS"
        Case "TABMAST.TB_CLASMAST"
          Return "ZOOMTABCLAS"
        Case "TESTMAG.TM_CODASPE", "TESTORD.TD_CODASPE", "TESTPRB.TM_CODASPE"
          Return "ZOOMTABASPE"
        Case "TESTMAG.TM_CODNTRA", "TESTPRB.TM_CODNTRA"
          Return "ZOOMTABNTRA"
        Case "TESTMAG.TM_CODPORT", "TESTPRB.TM_CODPORT"
          Return "ZOOMTABMPOR"
        Case "TESTMAG.TM_CODRSTA", "TESTPRB.TM_CODRSTA"
          Return "ZOOMTABRSTA"
        Case "TESTMAG.TM_TIPOBF", "TESTORD.TD_TIPOBF", "TESTPRB.TM_TIPOBF", "TESTOFF.TD_TIPOBF"
          Return "ZOOMTABTPBF"
        Case "TESTOFF.TD_CODLEAD", "LEADS.LE_CODLEAD", "OPPORTUN.OP_CODLEAD", "CRACT.CA_CODLEAD"
          Return "ZOOMLEADS"
        Case "LEADS.LE_CLASCON"
          Return "ZOOMTABCSCL"
        Case "LEADS.LE_CATEG"
          Return "ZOOMTABCATE"
        Case "LEADS.LE_CODCPTR", "OPPORTUN.OP_CODCPTR"
          Return "ZOOMTABCPTR"
        Case "LEADS.LE_CODLING"
          Return "ZOOMTABLING"
        Case "LEADS.LE_CODPERV", "OPPORTUN.OP_CODPERV"
          Return "ZOOMTABPERV"
        Case "LEADS.LE_CODCANA"
          Return "ZOOMTABCANA"
        Case "LEADS.LE_STATO"
          Return "ZOOMTABSTAT"
        Case "OPPORTUN.OP_CODCAMP"
          Return "ZOOMTABCAMP"
        Case "OPPORTUN.OP_CODMSOS"
          Return "ZOOMTABMSOS"
        Case "OPPORTUN.OP_CODTOPP"
          Return "ZOOMTABTOPP"
        Case "CRACT.CA_CODTACO"
          Return "ZOOMTABTACO"
        Case "TESTOFF.TD_CODCFAM"
          Return "ZOOMTABCFAM"
        Case Else
          Return "ZOOMHLVL"
          'oParam.strTipo = "TESTORD"
          'oParam.strDescr = strCampo
          ''oparam.strOut = ..... valore di ritorno
          'return "ZOOMTABPORT"
          'If NTSZOOM.strIn <> strIn And NTSZOOM.strIn <> "" Then strIn = NTSZOOM.strIn


          'frmHlvl.Init(oMenu, oParamHlvl, DittaCorrente)
          'frmHlvl.InitEntity(oCleConf)
          'frmHlvl.ShowDialog()

          'If NTSZOOM.strIn <> strIn And NTSZOOM.strIn <> "" Then strIn = oParam.strOut
      End Select

      Return ""

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrattaDbDoc_TrovaFiglieLav(ByRef dttFigliOut As DataTable, ByRef dttLavOut As DataTable, _
                                         ByVal strTipoArtConf As String, ByVal bUsaFiltroCommessa As Boolean, ByVal bRiportaFasiFantasmi As Boolean, _
                                         ByVal bUsaFiltroPadre As Boolean, ByVal strDitta As String, ByRef strError As String, _
                                         ByVal strCodart As String, ByVal nFase As Integer, ByVal lRigaPadre As Integer, _
                                         ByVal dtDatval As Date, ByVal lCommeca As Integer, _
                                         ByVal strSubc As String, ByVal lCodClie As Integer, ByVal dtDatcons As Date, _
                                         ByVal dQuant As Decimal, ByVal dColli As Decimal, ByVal lCodCena As Integer, _
                                         ByVal strFamProd As String, ByVal nMagClav As Integer, ByRef lRigaFigli As Integer, _
                                         ByRef dCostoLav As Decimal, ByVal strUmprz As String, ByVal strUnmis As String, _
                                         ByVal strUmp As String, ByVal nLiv As Integer, ByRef lCount As Integer, ByRef lRow As Integer, _
                                         ByRef lRowFase As Integer, ByRef lFaseSucc As Integer, ByVal bFantasma As Boolean, _
                                         ByVal strDescrTagliaRoot As String, ByVal strDescrTagliaPadr As String, _
                                         ByVal nIndiceTagliaRoot As Integer, ByVal nIndiceTagliaPadr As Integer, _
                                         ByVal strColoreRoot As String, ByVal strColorePadr As String) As Boolean

    Try
      Return TrattaDbDoc_TrovaFiglieLav(dttFigliOut, dttLavOut, strTipoArtConf, bUsaFiltroCommessa, bRiportaFasiFantasmi, _
                                        bUsaFiltroPadre, strDitta, strError, strCodart, nFase, lRigaPadre, _
                                        dtDatval, lCommeca, strSubc, lCodClie, dtDatcons, _
                                        dQuant, dColli, lCodCena, strFamProd, nMagClav, lRigaFigli, _
                                        dCostoLav, strUmprz, strUnmis, strUmp, nLiv, lCount, lRow, _
                                        lRowFase, lFaseSucc, bFantasma, strDescrTagliaRoot, strDescrTagliaPadr, _
                                        nIndiceTagliaRoot, nIndiceTagliaPadr, strColoreRoot, strColorePadr, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrattaDbDoc_TrovaFiglieLav(ByRef dttFigliOut As DataTable, ByRef dttLavOut As DataTable, _
                                         ByVal strTipoArtConf As String, ByVal bUsaFiltroCommessa As Boolean, ByVal bRiportaFasiFantasmi As Boolean, _
                                         ByVal bUsaFiltroPadre As Boolean, ByVal strDitta As String, ByRef strError As String, _
                                         ByVal strCodart As String, ByVal nFase As Integer, ByVal lRigaPadre As Integer, _
                                         ByVal dtDatval As Date, ByVal lCommeca As Integer, _
                                         ByVal strSubc As String, ByVal lCodClie As Integer, ByVal dtDatcons As Date, _
                                         ByVal dQuant As Decimal, ByVal dColli As Decimal, ByVal lCodCena As Integer, _
                                         ByVal strFamProd As String, ByVal nMagClav As Integer, ByRef lRigaFigli As Integer, _
                                         ByRef dCostoLav As Decimal, ByVal strUmprz As String, ByVal strUnmis As String, _
                                         ByVal strUmp As String, ByVal nLiv As Integer, ByRef lCount As Integer, ByRef lRow As Integer, _
                                         ByRef lRowFase As Integer, ByRef lFaseSucc As Integer, ByVal bFantasma As Boolean, _
                                         ByVal strDescrTagliaRoot As String, ByVal strDescrTagliaPadr As String, _
                                         ByVal nIndiceTagliaRoot As Integer, ByVal nIndiceTagliaPadr As Integer, _
                                         ByVal strColoreRoot As String, ByVal strColorePadr As String, _
                                         ByVal strVarianteTagliaPadr As String) As Boolean
    'in vb6 DOCTrattaDbFasi, DOCTrattaDbFasiEx DOCTrattaDbFasiEx2, DOCTrattaDbFasiMod di leprod e
    'TrattaDbDoc contenuta in bustrdb.bas

    ' ROUTINE RICORSIVA CHE LEGGENDO LA DISTINTA BASE A FASI E NON (se fase = 0 articolo non a fasi)
    ' GENERA LE RIGHE DI IMPEGNO DI PROD./SCARICO DI PROD.
    ' SCARTANDO EVENTUALI FIGLI FANTASMA
    '
    ' strCodart: codice articolo padre
    ' nFase: fase articolo                      (SE FASE = 0 ARTICOLO NON A FASI)
    ' lRigaPadre: numero riga padre
    ' dtDatval: data validità distinta
    ' nListino: listino costi materiali scarichi
    ' lCommeca: eventuale cod. commessa riga padre
    ' strSubc: evemtuale sottocommessa riga padre
    ' lCodClie: codclie riga padre
    ' dtDatcons: data consegna riga padre
    ' dQuant, dColli: quantità/colli riga padre
    ' lCodCena: codice cetro padre
    ' strFamProd: codice famiglia padre
    ' nMagClav: eventuale magazzino di c/lavoro (oppure zero)
    ' lRigaFigli: ultima riga usata x creare i figli nel transitorio
    '             (passata ByRef x riflettere incrementi all'indietro)
    ' dCostoLav: prezzo lavorazione terzista determinata (di ritorno)
    '%%%
    ' strUmprz: prezzi riferiti ad u.d.m. <> dalla u.m.p. nella riga padre
    ' strUnmis: u.d.m. riga padre (serve solo se strUMprz="S")
    ' strUMP: u.d.m. principale riga padre (serve solo se strUMprz="S")
    '%%%
    ' nliv: opzionale, livello di ricorsione (default zero)
    ' lCount: opzionale, numero di figli trovati (default zero)
    ' lRow: opzionale, numero di righe processate (defaultzero)
    ' lRowFase: opzionale numero di fasi processate (default zero)
    ' lFaseSucc: opzione, riferimento fase successiva (default zer)
    ' bFantasma: opzionale, x indicare se il padre era un fantasma
    ' TCP
    ' strDescrTagliaRoot -> Descr. Taglia art. di partenza
    ' strDescrTagliaPadr -> Descr. Taglia art. padre
    ' nIndiceTagliaRoot -> Indice Taglia art. di partenza
    ' nIndiceTagliaPadr -> Indice Taglia art. padre
    ' strColoreRoot -> Colore art. di partenza
    ' strColorePadr -> Colore art. padre

    Const nMaxLiv As Integer = 20     'Numero massimo livello per controllo ricorsione
    Dim dttArti As New DataTable
    Dim dNewQuant As Decimal
    Dim dNewColli As Decimal
    Dim dQuoz As Decimal
    Dim lQuozint As Integer
    Dim dtNewDatcons As Date
    Dim strGesvar As String
    Dim lRRFencePadre As Integer
    Dim dFCorrltPadre As Decimal

    Dim bRRFdaFasi As Boolean = False
    Dim nRrfenceFase As Integer = 0
    Dim dFcorrltFase As Decimal = 0
    Dim nTmpRrfence As Integer = 0
    Dim dTmpFcorrlt As Decimal = 0

    Dim dPerQta As Decimal
    Dim bMsgNoRow As Boolean
    'TCP
    Dim strCodFigli As String
    Dim strAppoDescrTagliaPadr As String
    Dim nAppoIndiceTagliaPadr As Integer
    Dim strAppoColorePadr As String
    Dim strAppoVarianteTagliaPadr As String = ""

    Dim dttMovdis As New DataTable
    Dim dttCicli As New DataTable
    Dim strErr As String = ""

    Dim bIgnoraFantasma As Boolean = False

    Try

      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dttFigliOut, dttLavOut, strTipoArtConf, bUsaFiltroCommessa, bRiportaFasiFantasmi, _
                                             bUsaFiltroPadre, strDitta, strError, strCodart, nFase, lRigaPadre, _
                                             dtDatval, lCommeca, strSubc, lCodClie, dtDatcons, _
                                             dQuant, dColli, lCodCena, strFamProd, nMagClav, lRigaFigli, _
                                             dCostoLav, strUmprz, strUnmis, strUmp, nLiv, lCount, lRow, _
                                             lRowFase, lFaseSucc, bFantasma, strDescrTagliaRoot, strDescrTagliaPadr, _
                                             nIndiceTagliaRoot, nIndiceTagliaPadr, strColoreRoot, strColorePadr, strVarianteTagliaPadr})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttFigliOut = CType(oIn(0), DataTable)
        dttLavOut = CType(oIn(1), DataTable)
        strError = NTSCStr(oIn(7))
        lRigaFigli = NTSCInt(oIn(21))
        dCostoLav = NTSCDec(oIn(22))
        lCount = NTSCInt(oIn(27))
        lRow = NTSCInt(oIn(28))
        lRowFase = NTSCInt(oIn(29))
        lFaseSucc = NTSCInt(oIn(30))
        Return CBool(oOut)
      End If
      '----------------

      If nFase > 0 Then
        If CBool((ModuliExtDittaDitt(strDitta) And CLN__STD.bsModExtLEX)) Or CBool((ModuliExtDittaDitt(strDitta) And CLN__STD.bsModExtTCP)) Then
          'posso proseguire
        Else
          strError += vbCrLf & oApp.Tr(Me, 128600634686562500, "Impossibile trattare articoli gestiti a fasi: modulo LOGISTICA ESTESA non presente in chiave di attivazione.")
          Return False
        End If
      End If

      'Controllo ricorsione
      If nLiv > nMaxLiv Then
        strError += vbCrLf & oApp.Tr(Me, 128599852767500000, "Superati |" & nMaxLiv & "| livelli di distinta! Probabile riferimento circolare. Controllare la distinta per il codice articolo padre |'" & strCodart.ToUpper & "'|.")
        Return False
      End If

      'Variabile che controlla se visualizzare o meno il messagggio di mancanza figli
      If lCount = -1 Then
        bMsgNoRow = True
        lCount = 0
      Else
        bMsgNoRow = False
      End If

      strGesvar = "N"
      lRRFencePadre = 0
      dPerQta = 1
      oCldComm.ValCodiceDb(strCodart, strDitta, "ARTICO", "S", "", dttArti)
      If dttArti.Rows.Count > 0 Then
        strGesvar = NTSCStr(dttArti.Rows(0)!ar_gesvar)
        lRRFencePadre = NTSCInt(dttArti.Rows(0)!ar_rrfence)
        dFCorrltPadre = NTSCDec(dttArti.Rows(0)!ar_fcorrlt)
        dPerQta = NTSCDec(dttArti.Rows(0)!ar_perqta)
      End If
      dttArti.Clear()
      If dPerQta = 0 Then dPerQta = 1

      If nFase > 0 Then
        'Rileva RRFence & FCorrlt relativi alla fase corrente del padre
        bRRFdaFasi = False
        oCldComm.ValCodiceDb(nFase.ToString, strDitta, "ARTFASI", "N", "", dttArti, strCodart)
        If dttArti.Rows.Count > 0 Then
          nRrfenceFase = NTSCInt(dttArti.Rows(0)!af_rrfence)
          lRRFencePadre = nRrfenceFase
          dFcorrltFase = NTSCDec(dttArti.Rows(0)!af_fcorrlt)
          dFCorrltPadre = dFcorrltFase
          bRRFdaFasi = True
        End If
        dttArti.Clear()
      End If

      'A primo livello si salva alcune variabili locali e prepara i datatable
      If nLiv = 0 Then
        strCodartRoot = strCodart
        dPerqtaRoot = dPerQta
        dQuantRoot = dQuant
        bStopFasi = False

        dttFigliOut.Clear()
        dttFigliOut.Columns.Add("md_codfigli", GetType(String))
        dttFigliOut.Columns.Add("md_fasefigli", GetType(Integer))
        dttFigliOut.Columns.Add("md_unmis", GetType(String))
        dttFigliOut.Columns.Add("md_ump", GetType(String))
        dttFigliOut.Columns.Add("md_misura1", GetType(Decimal))
        dttFigliOut.Columns.Add("md_misura2", GetType(Decimal))
        dttFigliOut.Columns.Add("md_misura3", GetType(Decimal))
        dttFigliOut.Columns.Add("md_codmaga", GetType(Integer))
        dttFigliOut.Columns.Add("md_proven", GetType(String))
        dttFigliOut.Columns.Add("md_note", GetType(String))
        dttFigliOut.Columns.Add("md_quant", GetType(Decimal))
        dttFigliOut.Columns.Add("md_colli", GetType(Decimal))
        dttFigliOut.Columns.Add("md_datcons", GetType(DateTime))
        dttFigliOut.Columns.Add("md_listino", GetType(Integer))
        dttFigliOut.Columns.Add("md_rigah", GetType(Integer))
        dttFigliOut.Columns.Add("md_codclie", GetType(Integer))
        dttFigliOut.Columns.Add("md_commeca", GetType(Integer))
        dttFigliOut.Columns.Add("md_subc", GetType(String))
        dttFigliOut.Columns.Add("md_codcena", GetType(Integer))
        dttFigliOut.Columns.Add("md_famprod", GetType(String))
        dttFigliOut.Columns.Add("md_rigafigli", GetType(Integer))
        dttFigliOut.Columns.Add("md_rowcorr", GetType(Integer))
        dttFigliOut.Columns.Add("md_magclav", GetType(Integer))
        dttFigliOut.Columns.Add("md_descrtagliaroot", GetType(String))
        dttFigliOut.Columns.Add("md_indicetagliaroot", GetType(Integer))
        dttFigliOut.Columns.Add("md_descrtagliapadr", GetType(String))
        dttFigliOut.Columns.Add("md_indicetagliapadr", GetType(Integer))
        dttFigliOut.Columns.Add("md_interr", GetType(String))
        dttFigliOut.AcceptChanges()
      End If

      '------------------------
      If nFase > 0 Then
        If Not oCldComm.TrattaDbDocFasi_TrovaFiglieLav(strDitta, strCodart, nFase, strGesvar, dtDatval, strCodartRoot, _
                                                   strTipoArtConf, bUsaFiltroPadre, bUsaFiltroCommessa, lCodClie, _
                                                   lCommeca, strDescrTagliaPadr, nIndiceTagliaPadr, dttMovdis, dttCicli) Then Return False

      Else
        If Not oCldComm.TrattaDbDoc_TrovaFiglieLav(strDitta, strCodart, strGesvar, dtDatval, strCodartRoot, _
                                                  strTipoArtConf, bUsaFiltroPadre, bUsaFiltroCommessa, lCodClie, _
                                                  lCommeca, strDescrTagliaPadr, nIndiceTagliaPadr, dttMovdis, dttCicli) Then Return False
      End If

      '------------------------
      For Each dtrMD As DataRow In dttMovdis.Rows
        With dtrMD
          'Dim s As String = dtrMD!md_codfigli.ToString
          lRow = lRow + 1
          '---------------------------
          'Esclude gli articoli 'da non considerare in MRP'
          If !ar_consmrp.ToString <> "S" Then
            GoTo SaltaFiglio
          End If
          '---------------------------
          'Calcola la quantità impegata dal figlio
          If NTSCDec(!md_perpz) = 1 Then
            dNewQuant = (dQuant * (NTSCDec(!md_quantump) + NTSCDec(!md_sfrido)))
          Else
            dQuoz = dQuant / NTSCDec(!md_perpz)
            lQuozint = NTSCInt(Fix(dQuoz))
            If lQuozint < dQuoz Then lQuozint = lQuozint + 1
            dNewQuant = lQuozint * NTSCDec(!md_quantump)
          End If
          dNewQuant = ArrDbl(dNewQuant, 3)
          'da UMP cerca i colli
          If Not ConvQuantUM(strDitta, !md_codfigli.ToString, !md_ump.ToString, dNewQuant, NTSCDec(!md_misura1), NTSCDec(!md_misura2), NTSCDec(!md_misura3), !md_unmis.ToString, dNewColli, strErr, 3) Then
            If strErr <> "" Then strError += vbCrLf & strErr
            GoTo SaltaFiglio
          End If
          '-------------------------
          'Calcola la nuova data consegna del figlio
          If nFase > 0 Then
            nTmpRrfence = NTSCInt(IIf(bRRFdaFasi, nRrfenceFase, NTSCDec(!RRFence)))
            dTmpFcorrlt = NTSCDec(IIf(bRRFdaFasi, dFcorrltFase, NTSCDec(!Fcorrlt)))
            dtNewDatcons = DateAddEx(strDitta, "d", ((nTmpRrfence + Int(dTmpFcorrlt * dQuant)) * -1) + NTSCDec(!md_ritardo), dtDatcons, 0)
          Else
            dtNewDatcons = DateAddEx(strDitta, "d", ((NTSCDec(!RRFence) + Int(NTSCDec(!Fcorrlt) * dQuant)) * -1) + NTSCDec(!md_ritardo), dtDatcons, 0)
          End If
          '-------------------------
          'Corregge eventualmente la sotto-commessa
          If strSubc = "" Then strSubc = " "
          If lCommeca = 0 Then strSubc = " "

          bIgnoraFantasma = False
          'If (!md_interr.ToString = "F" Or !FiglioFantasma.ToString = "S") And !ar_gesfasi.ToString = "S" Then
          '  bIgnoraFantasma = True
          '  strError += vbCrLf & oApp.Tr(Me, 128600645955156250, "L'articolo figlio |'" & !md_codfigli.ToString & "'| (incluso nella distinta base di |'" & strCodart & "'|) risulta essere un articolo con distinta base fantasma (o legame fantasma). Essendo un articolo gestito a fasi viene ignorata l'indicazione fantasma e trattato come livello di distinta normale.")
          'End If

          'TCP
          strAppoDescrTagliaPadr = strDescrTagliaPadr
          nAppoIndiceTagliaPadr = nIndiceTagliaPadr
          strAppoColorePadr = strColorePadr
          strAppoVarianteTagliaPadr = strVarianteTagliaPadr

          'Controlla i fantasmi
          If (!md_interr.ToString = "F" Or !FiglioFantasma.ToString = "S") And Not bIgnoraFantasma Then
            'TCP
            If strDescrTagliaRoot <> "" And strDescrTagliaRoot <> " " Then
              'Esplosione articolo a Taglie e colori

              'Passaggio della TAGLIA e del COLORE al livello sottostante
              If NTSCInt(!TaglieColoriFiglio) = 0 Then

                'Eventuale 2a variante (abbinamento di taglia)
                'Scala taglie del figlio = 0
                Select Case !md_tctipo.ToString
                  Case "C", "G"
                    'Lo trasformo in base all'abbinamento taglie
                    strAppoVarianteTagliaPadr = oCldComm.TrattaDb_GetVarianteTagliafiglio(strDitta, NTSCStr(!md_coddb), NTSCStr(!md_riga), nAppoIndiceTagliaPadr)
                    If strAppoVarianteTagliaPadr = "" Then
                      'Azzero il colore per non farlo procedere
                      strAppoColorePadr = ""
                    End If
                  Case Else
                    strAppoVarianteTagliaPadr = ""
                End Select

                strAppoDescrTagliaPadr = ""
                nAppoIndiceTagliaPadr = 0
                If !TipoOpzFiglio.ToString <> "G" Then
                  strAppoColorePadr = ""
                  strAppoVarianteTagliaPadr = ""
                End If

              Else

                'Scala taglie del figlio <> 0

                strAppoVarianteTagliaPadr = ""

                Select Case NTSCStr(!md_tctipo)
                  Case "C", "G"
                    'Lo trasformo in base all'abbinamento taglie
                    oCldComm.TrattaDb_GetTagliafiglio(strDitta, NTSCStr(!md_coddb), NTSCStr(!md_riga), nAppoIndiceTagliaPadr, strAppoDescrTagliaPadr, nAppoIndiceTagliaPadr)
                    If nAppoIndiceTagliaPadr = 0 Then
                      'Azzero il colore per non farlo procedere
                      strAppoColorePadr = ""
                    End If
                End Select
              End If

              If strAppoColorePadr <> "" Then
                Select Case NTSCStr(!md_tctipo)
                  Case "D", "E", "G"
                    'Lo trasformo in base all'abbinamento colore
                    strAppoColorePadr = oCldComm.TrattaDb_GetVarianteColorefiglio(strDitta, !md_coddb.ToString, NTSCStr(!md_riga), strColorePadr)
                End Select
              End If
            End If
            'END TCP

            'Ricorsione
            If Not TrattaDbDoc_TrovaFiglieLav(dttFigliOut, dttLavOut, strTipoArtConf, bUsaFiltroCommessa, bRiportaFasiFantasmi, _
                                bUsaFiltroPadre, strDitta, strError, NTSCStr(!md_codfigli), NTSCInt(!md_fasefigli), lRigaPadre, dtDatval, lCommeca, _
                                strSubc, lCodClie, dtNewDatcons, dNewQuant, dNewColli, lCodCena, _
                                strFamProd, nMagClav, lRigaFigli, dCostoLav, "", "", _
                                "", nLiv + 1, lCount, lRow, lRowFase, lFaseSucc, True, strDescrTagliaRoot, _
                                strAppoDescrTagliaPadr, nIndiceTagliaRoot, nAppoIndiceTagliaPadr, strColoreRoot, _
                                strAppoColorePadr, strAppoVarianteTagliaPadr) Then
              Return False
            End If
          Else

            'TCP
            'strCodfiglio potrebbe dover essere determinato...
            If strDescrTagliaRoot <> "" And strDescrTagliaRoot <> " " Then
              'Esplosione articolo a Taglie e colori

              'Passaggio della TAGLIA e del COLORE al livello sottostante
              If NTSCInt(!TaglieColoriFiglio) = 0 Then

                'Eventuale 2a variante (abbinamento di taglia)
                'Scala taglie del figlio = 0
                Select Case !md_tctipo.ToString
                  Case "C", "G"
                    'Lo trasformo in base all'abbinamento taglie
                    strAppoVarianteTagliaPadr = oCldComm.TrattaDb_GetVarianteTagliafiglio(strDitta, NTSCStr(!md_coddb), NTSCStr(!md_riga), nAppoIndiceTagliaPadr)
                    If strAppoVarianteTagliaPadr = "" Then
                      'Azzero il colore per non farlo procedere
                      strAppoColorePadr = ""
                    End If
                  Case Else
                    strAppoVarianteTagliaPadr = ""
                End Select

                strAppoDescrTagliaPadr = ""
                nAppoIndiceTagliaPadr = 0
                If !TipoOpzFiglio.ToString <> "G" Then
                  strAppoColorePadr = ""
                  strAppoVarianteTagliaPadr = ""
                End If

              Else

                'Scala taglie del figlio <> 0

                strAppoVarianteTagliaPadr = ""

                Select Case NTSCStr(!md_tctipo)
                  Case "C", "G"
                    'Lo trasformo in base all'abbinamento taglie
                    oCldComm.TrattaDb_GetTagliafiglio(strDitta, NTSCStr(!md_coddb), NTSCStr(!md_riga), nAppoIndiceTagliaPadr, strAppoDescrTagliaPadr, nAppoIndiceTagliaPadr)
                    If nAppoIndiceTagliaPadr = 0 Then
                      'Azzero il colore per non farlo procedere
                      strAppoColorePadr = ""
                    End If
                End Select
              End If

              If !TipoOpzFiglio.ToString = "G" Then
                'Articolo ROOT a VARIANTI
                If strAppoColorePadr <> "" Then
                  'Passaggio del COLORE
                  Select Case NTSCStr(!md_tctipo)
                    Case "D", "E", "G"
                      'Lo trasformo in base all'abbinamento
                      strAppoColorePadr = oCldComm.TrattaDb_GetVarianteColorefiglio(strDitta, !md_coddb.ToString, NTSCStr(!md_riga), strColorePadr)
                  End Select
                  strCodFigli = NTSCStr(!md_codfigli) & strAppoColorePadr
                  If strAppoColorePadr <> "" And strAppoVarianteTagliaPadr <> "" Then
                    strCodFigli = strCodFigli & strAppoVarianteTagliaPadr
                  End If

                  'Ora tratta il figlio
                  oCldComm.ValCodiceDb(strCodFigli, strDitta, "ARTICO", "S", "", dttArti)
                  If dttArti.Rows.Count > 0 Then
                    dttFigliOut.Rows.Add(dttFigliOut.NewRow())
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_codfigli = strCodFigli
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_fasefigli = !md_fasefigli
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_unmis = !md_unmis
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_ump = !md_ump
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_misura1 = !md_misura1
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_misura2 = !md_misura2
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_misura3 = !md_misura3
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_codmaga = !md_codmaga
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_proven = !md_proven
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_note = !md_note
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_quant = dNewQuant
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_colli = dNewColli
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_datcons = dtNewDatcons
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_rigah = lRigaPadre
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_codclie = lCodClie
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_commeca = lCommeca
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_subc = strSubc
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_codcena = lCodCena
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_famprod = strFamProd
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_rigafigli = lRigaFigli
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_rowcorr = lRow
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_magclav = nMagClav
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_descrtagliaroot = strDescrTagliaRoot
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_indicetagliaroot = nIndiceTagliaRoot
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_descrtagliapadr = strAppoDescrTagliaPadr
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_indicetagliapadr = nAppoIndiceTagliaPadr
                    dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_interr = !md_interr
                    dttFigliOut.AcceptChanges()

                    'If fmForm.ScriviRigaDiba(snaMovdis2, dNewQuant, dNewColli, dtNewDatcons, nListino, lRigaPadre, lCodClie, lCommeca, strSubc, lCodCena, strFamProd, lRigaFigli, lRow, nMagClav, strDescrTagliaRoot, nIndiceTagliaRoot, strAppoDescrTagliaPadr, nAppoIndiceTagliaPadr) Then
                    lCount = lCount + 1
                    'End If
                  Else
                    'Salta il figlio e messaggio
                    strError += vbCrLf & oApp.Tr(Me, 128599976241406250, "Articolo |'" & strCodFigli & "'| inesistente. Sarà scartato." & _
                                                                         "Verificare la distinta dell'articolo padre |'" & UCase$(strCodart) & "'|.")
                    GoTo SaltaFiglio
                  End If    'If dttArti.Rows.Count > 0 Then
                  dttArti.Clear()
                Else
                  'Salta il figlio e no messaggio (manca abbinamento taglia potrebbe essere voluto)
                  'strError += vbCrLf & oApp.Tr(Me, 128599976639531250, "Articolo |'" & !md_codfigli.ToString & "'| fittizio. Sarà scartato." & _
                  '                                   "Verificare la distinta dell'articolo padre |'" & UCase$(strCodart) & "'|.")
                  GoTo SaltaFiglio
                End If
              Else
                dttFigliOut.Rows.Add(dttFigliOut.NewRow())
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_codfigli = !md_codfigli
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_fasefigli = !md_fasefigli
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_unmis = !md_unmis
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_ump = !md_ump
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_misura1 = !md_misura1
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_misura2 = !md_misura2
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_misura3 = !md_misura3
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_codmaga = !md_codmaga
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_proven = !md_proven
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_note = !md_note
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_quant = dNewQuant
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_colli = dNewColli
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_datcons = dtNewDatcons
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_rigah = lRigaPadre
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_codclie = lCodClie
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_commeca = lCommeca
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_subc = strSubc
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_codcena = lCodCena
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_famprod = strFamProd
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_rigafigli = lRigaFigli
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_rowcorr = lRow
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_magclav = nMagClav
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_descrtagliaroot = strDescrTagliaRoot
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_indicetagliaroot = nIndiceTagliaRoot
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_descrtagliapadr = strAppoDescrTagliaPadr
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_indicetagliapadr = nAppoIndiceTagliaPadr
                dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_interr = !md_interr
                dttFigliOut.AcceptChanges()
                'If fmForm.ScriviRigaDiba(snaMovdis, dNewQuant, dNewColli, dtNewDatcons, nListino, lRigaPadre, lCodClie, lCommeca, strSubc, lCodCena, strFamProd, lRigaFigli, lRow, nMagClav, strDescrTagliaRoot, nIndiceTagliaRoot, strAppoDescrTagliaPadr, nAppoIndiceTagliaPadr) Then
                lCount = lCount + 1
                'End If
              End If
            Else
              dttFigliOut.Rows.Add(dttFigliOut.NewRow())
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_codfigli = !md_codfigli
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_fasefigli = !md_fasefigli
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_unmis = !md_unmis
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_ump = !md_ump
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_misura1 = !md_misura1
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_misura2 = !md_misura2
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_misura3 = !md_misura3
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_codmaga = !md_codmaga
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_proven = !md_proven
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_note = !md_note
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_quant = dNewQuant
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_colli = dNewColli
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_datcons = dtNewDatcons
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_rigah = lRigaPadre
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_codclie = lCodClie
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_commeca = lCommeca
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_subc = strSubc
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_codcena = lCodCena
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_famprod = strFamProd
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_rigafigli = lRigaFigli
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_rowcorr = lRow
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_magclav = nMagClav
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_descrtagliaroot = strDescrTagliaRoot
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_indicetagliaroot = nIndiceTagliaRoot
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_descrtagliapadr = strAppoDescrTagliaPadr
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_indicetagliapadr = nAppoIndiceTagliaPadr
              dttFigliOut.Rows(dttFigliOut.Rows.Count - 1)!md_interr = !md_interr
              dttFigliOut.AcceptChanges()
              'If fmForm.ScriviRigaDiba(snaMovdis, dNewQuant, dNewColli, dtNewDatcons, nListino, lRigaPadre, lCodClie, lCommeca, strSubc, lCodCena, strFamProd, lRigaFigli, lRow, nMagClav, strDescrTagliaRoot, nIndiceTagliaRoot, strAppoDescrTagliaPadr, nAppoIndiceTagliaPadr) Then
              lCount = lCount + 1
              'End If
            End If
          End If

SaltaFiglio:
        End With    'With dtrMD
      Next    'For Each dtrMD As DataRow In dttMovdis.Rows

      'Segnala se il padre non ha figli
      If nLiv = 0 Then
        If lCount = 0 Then
          If Not bMsgNoRow Then
            strError += vbCrLf & oApp.Tr(Me, 128599993230156250, "Per l'articolo |" & strCodart & "| non sono stati trovati materiali nella distinta base.")
          End If
        End If
      End If

      '------------------------
      'Analisi lavorazioni
      'Se è un fantasma e non viene attivata l'opzione non riporta le fasi
      If bFantasma And (Not bRiportaFasiFantasmi) Then
        GoTo SkipFasi
      End If
      If bStopFasi Then
        GoTo SkipFasi
      End If

      If dttLavOut.Columns.Contains("dd_codartpadre") = False Then
        dttLavOut.Clear()
        dttLavOut = dttCicli.Clone
        dttLavOut.Columns.Add("dd_codartpadre", GetType(String))
        dttLavOut.Columns.Add("dd_rrfencepadre", GetType(Integer))
        dttLavOut.Columns.Add("dd_fcorrltpadre", GetType(Decimal))
        dttLavOut.Columns.Add("dd_perqtapadre", GetType(Decimal))
        dttLavOut.Columns.Add("dd_quant", GetType(Decimal))
        dttLavOut.Columns.Add("dd_datcons", GetType(DateTime))
        dttLavOut.Columns.Add("dd_rigah", GetType(Integer))
        dttLavOut.Columns.Add("dd_numlav", GetType(Integer))
        dttLavOut.Columns.Add("dd_sufase", GetType(Integer))
        dttLavOut.Columns.Add("dd_costolav", GetType(Decimal))
        dttLavOut.Columns.Add("dd_quantfant", GetType(Decimal))
        dttLavOut.Columns.Add("dd_umprz", GetType(String))
        dttLavOut.Columns.Add("dd_unmis", GetType(String))
        dttLavOut.Columns.Add("dd_ump", GetType(String))
        dttLavOut.Columns.Add("dd_colli", GetType(Decimal))
      End If

      For Each dtrDD As DataRow In dttCicli.Rows
        lRowFase = lRowFase + 1
        dttLavOut.ImportRow(dtrDD)
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_codartpadre = IIf(bFantasma, strCodartRoot, strCodart).ToString
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_rrfencepadre = lRRFencePadre
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_fcorrltpadre = dFCorrltPadre
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_perqtapadre = NTSCDec(IIf(bFantasma, dPerqtaRoot, dPerQta))
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_quant = NTSCDec(IIf(bFantasma, dQuantRoot, dQuant))
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_datcons = dtDatcons
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_rigah = lRigaPadre
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_numlav = lRowFase
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_sufase = lFaseSucc
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_costolav = dCostoLav
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_quantfant = dQuant
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_umprz = strUmprz
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_unmis = strUnmis
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_ump = strUmp
        dttLavOut.Rows(dttLavOut.Rows.Count - 1)!dd_colli = dColli
        dttLavOut.AcceptChanges()
      Next    'For Each dtrDD As DataRow In dttCicli.Rows

SkipFasi:
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttArti.Clear()
      dttCicli.Clear()
      dttMovdis.Clear()
    End Try
  End Function

#Region "Esplosione distinta base per documenti"
  Public Overridable Function TrattaDb_TrovaFigli(ByVal strDitta As String, ByRef dttMovdis As DataTable, _
                                                  ByVal strTipoArtConf As String, ByVal strCodart As String, _
                                                  ByVal strCodRoot As String, ByVal strGesvarPadr As String, _
                                                  ByVal bNoFiltroDataVal As Boolean, ByVal bUsaFiltroPadre As Boolean, _
                                                  ByVal strTipoRoot As String, ByVal dtDatValConv As Date, _
                                                  ByVal nSortTipoFiglio As Integer, ByVal bEscludiFornitoDaTerz As Boolean, _
                                                  ByVal lCodclie As Integer, ByVal lLotto As Integer, _
                                                  ByVal bUsaFiltroCommessa As Boolean, ByVal lCommeca As Integer, _
                                                  ByVal strDescrTagliaPadr As String, ByVal nIndiceTagliaPadr As Integer, _
                                                  ByVal bGestFase As Boolean, ByVal nFase As Integer, _
                                                  Optional ByVal bDbDoc As Boolean = False) As Boolean
    Return oCldComm.TrattaDb_TrovaFigli(strDitta, dttMovdis, strTipoArtConf, strCodart, _
                               strCodRoot, strGesvarPadr, bNoFiltroDataVal, bUsaFiltroPadre, _
                               strTipoRoot, dtDatValConv, nSortTipoFiglio, bEscludiFornitoDaTerz, _
                               lCodclie, lLotto, bUsaFiltroCommessa, lCommeca, strDescrTagliaPadr, _
                               nIndiceTagliaPadr, bGestFase, nFase, bDbDoc)
  End Function
  Public Overridable Function TrattaDb_TrovaLav(ByVal strDitta As String, _
                                                ByRef dttCicli As DataTable, ByVal nTipoOp As Integer, _
                                                ByVal strTipoArtConf As String, ByVal strCodart As String, _
                                                ByVal strCodRoot As String, ByVal strGesvarPadr As String, _
                                                ByVal bNoFiltroDataVal As Boolean, ByVal bUsaFiltroPadre As Boolean, _
                                                ByVal strTipoRoot As String, ByVal dtDatValConv As Date, _
                                                ByVal lCodclie As Integer, _
                                                ByVal bUsaFiltroCommessa As Boolean, ByVal lCommeca As Integer, _
                                                ByVal bGestFase As Boolean, ByVal nFase As Integer) As Boolean
    Return oCldComm.TrattaDb_TrovaLav(strDitta, dttCicli, nTipoOp, strTipoArtConf, strCodart, _
                                      strCodRoot, strGesvarPadr, bNoFiltroDataVal, bUsaFiltroPadre, _
                                      strTipoRoot, dtDatValConv, lCodclie, bUsaFiltroCommessa, _
                                      lCommeca, bGestFase, nFase)
  End Function
  Public Overridable Function TrattaDb_GetVarianteColorefiglio(ByVal strDitta As String, ByVal strCodDb As String, _
                                                               ByVal strRiga As String, ByVal strColorePadr As String) As String
    Return oCldComm.TrattaDb_GetVarianteColorefiglio(strDitta, strCodDb, strRiga, strColorePadr)
  End Function
  Public Overridable Function TrattaDb_GetVarianteTagliafiglio(ByVal strDitta As String, ByVal strCodDb As String, _
                                                               ByVal strRiga As String, ByVal nIndiceTagliaPadr As Integer) As String
    Return oCldComm.TrattaDb_GetVarianteTagliafiglio(strDitta, strCodDb, strRiga, nIndiceTagliaPadr)
  End Function
  Public Overridable Sub TrattaDb_GetTagliafiglio(ByVal strDitta As String, ByVal strCodDb As String, _
                                                  ByVal strRiga As String, ByVal nIndiceTagliaPadr As Integer, _
                                                  ByRef strTagliaFigl As String, ByRef nIndTagliaFigl As Integer)
    oCldComm.TrattaDb_GetTagliafiglio(strDitta, strCodDb, strRiga, nIndiceTagliaPadr, strTagliaFigl, nIndTagliaFigl)
    Return
  End Sub

#End Region

  Public Overridable Function RiempiTTStMatr(ByVal strDitta As String, ByVal strSTMTTStMatr As String, _
                                              ByVal lSTMIITTStMatr As Integer, _
                                              ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                              ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                              ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                              ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                              ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                              ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                              ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                              ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                              ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                              ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                              ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                              ByVal strQuery As String) As Boolean
    Try
      Return oCldComm.RiempiTTStMatr(strDitta, strSTMTTStMatr, lSTMIITTStMatr, _
                                      strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                                      nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                                      nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                                      strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                                      nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                                      nSTMQualeMatr, strError, strQuery)

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
  Public Overridable Function RiempiTTStMatr(ByVal strDitta As String, ByVal strSTMTTStMatr As String, _
                                              ByVal lSTMIITTStMatr As Integer, _
                                              ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                              ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                              ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                              ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                              ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                              ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                              ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                              ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                              ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                              ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                              ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                              ByVal strQuery As String, _
                                              ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                              ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                              ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldComm.RiempiTTStMatr(strDitta, strSTMTTStMatr, lSTMIITTStMatr, _
                                      strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                                      nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                                      nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                                      strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                                      nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                                      nSTMQualeMatr, strError, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                      strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5)

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
  Public Overridable Function RiempiTTStMatr(ByVal strDitta As String, ByVal strSTMTTStMatr As String, _
                                              ByVal lSTMIITTStMatr As Integer, _
                                              ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                              ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                              ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                              ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                              ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                              ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                              ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                              ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                              ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                              ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                              ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                              ByVal strQuery As String, _
                                              ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                              ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                              ByVal strClassificazioneLivello5 As String, _
                                              ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                              ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldComm.RiempiTTStMatr(strDitta, strSTMTTStMatr, lSTMIITTStMatr, _
                                      strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                                      nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                                      nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                                      strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                                      nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                                      nSTMQualeMatr, strError, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                      strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                                      strCodart, nCodlsar, strConto, nCodlsel)

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
  Public Overridable Function RiempiTTStMats(ByVal strDitta As String, ByVal strSTMTTStMats As String, _
                                              ByVal lSTMIITTStMats As Integer, _
                                              ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                              ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                              ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                              ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                              ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                              ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                              ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                              ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                              ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                              ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                              ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                              ByVal strQuery As String) As Boolean
    Try
      Return oCldComm.RiempiTTStMats(strDitta, strSTMTTStMats, lSTMIITTStMats, _
                                      strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                                      nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                                      nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                                      strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                                      nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                                      nSTMQualeMatr, strError, strQuery)

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
  Public Overridable Function RiempiTTStMats(ByVal strDitta As String, ByVal strSTMTTStMats As String, _
                                              ByVal lSTMIITTStMats As Integer, _
                                              ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                              ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                              ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                              ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                              ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                              ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                              ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                              ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                              ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                              ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                              ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                              ByVal strQuery As String, _
                                              ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                              ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                              ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldComm.RiempiTTStMats(strDitta, strSTMTTStMats, lSTMIITTStMats, _
                                      strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                                      nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                                      nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                                      strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                                      nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                                      nSTMQualeMatr, strError, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                      strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5)

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
  Public Overridable Function RiempiTTStMats(ByVal strDitta As String, ByVal strSTMTTStMats As String, _
                                              ByVal lSTMIITTStMats As Integer, _
                                              ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                              ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                              ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                              ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                              ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                              ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                              ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                              ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                              ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                              ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                              ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                              ByVal strQuery As String, _
                                              ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                              ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                              ByVal strClassificazioneLivello5 As String, _
                                              ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                              ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Try
      'solo per non far dare il messaggio in TestPrecompila: le chiamate sono corrette così
      'CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
      Return oCldComm.RiempiTTStMats(strDitta, strSTMTTStMats, lSTMIITTStMats, _
                                      strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                                      nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                                      nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                                      strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                                      nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                                      nSTMQualeMatr, strError, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                                      strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                                      strCodart, nCodlsar, strConto, nCodlsel)

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

  Public Overridable Function AggiornaPrezzi(ByVal strDitta As String, ByVal nCodpdon As Integer, _
                                             ByVal strCodartIn As String, ByVal nFaseIn As Integer, _
                                             ByVal nValutaIn As Integer, ByVal strUnmisIn As String, _
                                             ByVal dPerqta As Decimal, ByVal dDaQuant As Decimal, _
                                             ByVal dAQuant As Decimal, ByVal dtData As Date, _
                                             ByVal nCodlavIn As Integer, ByVal strNetto As String, _
                                             ByVal dPrezzoNew() As Decimal, ByVal bAggiornaAnche1 As Boolean) As Boolean
    Dim i As Integer = 0
    Dim bAggListinoConPercZero As Boolean
    Dim strTipoArro As String = ""
    Dim dttTmp As New DataTable
    Dim nListinoPartenza As Integer
    Dim dTmp As Decimal
    Try
      'scrivo i listini da 1 a 8 leggendo le impostazioni del codice relazione listini in tabpdon
      'equivalente della funzione eseguiex di bsmgagli
      If (nCodpdon = 0) Then Return False

      strTipoArro = UCase(NTSCStr(oCldComm.GetSettingBusDitt(strDitta, "BSMGELAR", "OPZIONI", ".", "TipoArrotondamento", "E", " ", "E")))
      bAggListinoConPercZero = CBool(NTSCStr(oCldComm.GetSettingBusDitt(strDitta, "BSMGELAR", "OPZIONI", ".", "AggListinoConPercZero", "0", " ", "0")))

      oCldComm.ValCodiceDb(NTSCStr(nCodpdon), strDitta, "TABPDON", "N", "", dttTmp)

      If bAggiornaAnche1 Then nListinoPartenza = 1 Else nListinoPartenza = 2
      '----------------------------------------------------------------------------------
      For i = nListinoPartenza To 8
        '--------------------------------------------------------------------------------
        If (bAggListinoConPercZero = False) And (NTSCDec(dttTmp.Rows(0)("tb_rica_" & i)) = 0) Then Continue For
        '--------------------------------------------------------------------------------
        If dPrezzoNew(i) <> 0 Then
          'calcolo il nuovo prezzo
          If Not NTSCDec(dttTmp.Rows(0)("tb_rica_" & i)) = 0 Then
            dPrezzoNew(i) = ArrDbl((dPrezzoNew(i) + (dPrezzoNew(i) * NTSCDec(dttTmp.Rows(0)("tb_rica_" & i)) / 100)), oCldComm.TrovaNdecSuPrzUn(nValutaIn))
            If Not NTSCDec(dttTmp.Rows(0)("tb_arro_" & i)) = 0 Then
              If Not ((dPrezzoNew(i) / NTSCDec(dttTmp.Rows(0)("tb_arro_" & i))) = Fix(dPrezzoNew(i) / NTSCDec(dttTmp.Rows(0)("tb_arro_" & i)))) Then
                dTmp = Fix(dPrezzoNew(i) / NTSCDec(dttTmp.Rows(0)("tb_arro_" & i)))
                If strTipoArro = "E" Then 'eccesso
                  dTmp = dTmp + 1
                Else 'matematico
                  dTmp = ArrDbl(dTmp, 0)
                End If
                dPrezzoNew(i) = ArrDbl((dTmp * NTSCDec(dttTmp.Rows(0)("tb_arro_" & i))), oCldComm.TrovaNdecSuPrzUn(nValutaIn))
              End If
            End If
          End If
          '------------------------------------------------------------------------------
          ScriviPrezzo(strDitta, strCodartIn, nFaseIn, _
              nCodlavIn, " ", i, 0, nValutaIn, strUnmisIn, 0, dDaQuant, dtData, dPrezzoNew(i), _
            Nothing, dPerqta, dAQuant, "", _
            strNetto, True)
          '------------------------------------------------------------------------------
        End If
        '--------------------------------------------------------------------------------
      Next

      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '----------------------------------------------------------------------------------
    End Try
  End Function

#Region "Alerts"

  Public Overridable Function CreaDynasetAlert() As DataTable
    Dim dttTmp As New DataTable
    Try
      dttTmp.TableName = "ALERTS"
      dttTmp.Columns.Add("codditt").DefaultValue = ""
      dttTmp.Columns.Add("strMsg").DefaultValue = ""
      dttTmp.Columns.Add("strCodLead").DefaultValue = ""    'Solo x alert di tipo Attività CRM
      dttTmp.Columns.Add("strConto").DefaultValue = ""      'Solo x alert di tipo e-mail
      dttTmp.Columns.Add("strParam1").DefaultValue = ""
      dttTmp.Columns.Add("strParam2").DefaultValue = ""
      dttTmp.Columns.Add("strParam3").DefaultValue = ""
      dttTmp.Columns.Add("strParam4").DefaultValue = ""
      dttTmp.Columns.Add("strParam5").DefaultValue = ""
      dttTmp.Columns.Add("strParam6").DefaultValue = ""
      dttTmp.Columns.Add("strParam7").DefaultValue = ""
      dttTmp.Columns.Add("strParam8").DefaultValue = ""
      dttTmp.Columns.Add("strParam9").DefaultValue = ""
      dttTmp.Columns.Add("strParam10").DefaultValue = ""
      dttTmp.Columns.Add("strParam11").DefaultValue = ""
      dttTmp.Columns.Add("strParam12").DefaultValue = ""
      dttTmp.Columns.Add("strParam13").DefaultValue = ""
      dttTmp.Columns.Add("strParam14").DefaultValue = ""
      dttTmp.Columns.Add("strParam15").DefaultValue = ""
      dttTmp.Columns.Add("strParam16").DefaultValue = ""
      dttTmp.Columns.Add("strParam17").DefaultValue = ""
      dttTmp.Columns.Add("strParam18").DefaultValue = ""
      dttTmp.Columns.Add("strParam19").DefaultValue = ""
      dttTmp.Columns.Add("strParam20").DefaultValue = ""

      dttTmp.Columns("codditt").DefaultValue = ""
      dttTmp.Columns("strMsg").DefaultValue = ""
      dttTmp.Columns("strCodLead").DefaultValue = ""
      dttTmp.Columns("strConto").DefaultValue = ""
      dttTmp.Columns("strParam1").DefaultValue = ""
      dttTmp.Columns("strParam2").DefaultValue = ""
      dttTmp.Columns("strParam3").DefaultValue = ""
      dttTmp.Columns("strParam4").DefaultValue = ""
      dttTmp.Columns("strParam5").DefaultValue = ""
      dttTmp.Columns("strParam6").DefaultValue = ""
      dttTmp.Columns("strParam7").DefaultValue = ""
      dttTmp.Columns("strParam8").DefaultValue = ""
      dttTmp.Columns("strParam9").DefaultValue = ""
      dttTmp.Columns("strParam10").DefaultValue = ""
      dttTmp.Columns("strParam11").DefaultValue = ""
      dttTmp.Columns("strParam12").DefaultValue = ""
      dttTmp.Columns("strParam13").DefaultValue = ""
      dttTmp.Columns("strParam14").DefaultValue = ""
      dttTmp.Columns("strParam15").DefaultValue = ""
      dttTmp.Columns("strParam16").DefaultValue = ""
      dttTmp.Columns("strParam17").DefaultValue = ""
      dttTmp.Columns("strParam18").DefaultValue = ""
      dttTmp.Columns("strParam19").DefaultValue = ""
      dttTmp.Columns("strParam20").DefaultValue = ""
      dttTmp.AcceptChanges()
      Return dttTmp

    Catch ex As Exception
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    End Try
  End Function

  Public Overridable Function Verifica_Genera_Alert(ByVal nTipoOperazione As Integer, ByVal strCodditt As String, _
                                                    ByVal strProgramma As String, ByVal lIdEvento As Integer, _
                                                    ByVal lIdAlert As Integer, ByVal dttMsgOutParam As DataTable) As Boolean
    'tolto reference a BE__SIAL per poter gestire ANDROID
    Dim oCleSial As Object = Nothing
    Dim bSetHandle As Boolean = False
    Dim method As System.Reflection.MethodInfo = Nothing
    Dim eventInfo As System.Reflection.EventInfo = Nothing
    Dim memberInfo As System.Reflection.MemberInfo = Nothing
    Dim handler As [Delegate] = Nothing
    Try
      '-------------------------
      'creo SIAL per verificare e se necessario generare l'alert 
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BELBMENU", "BE__SIAL", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 128939609581198729, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleSial = oTmp

      'AddHandler oCleSial.RemoteEvent, AddressOf GestisciEventiEntitySial
      'aggancio l'evento tramite reflection
      method = Me.GetType.GetMethod("GestisciEventiEntitySial")
      eventInfo = oCleSial.GetType.GetEvent("RemoteEvent")
      handler = [Delegate].CreateDelegate(eventInfo.EventHandlerType, Me, method)
      eventInfo.AddEventHandler(oCleSial, handler)

      bSetHandle = True

      'If oCleSial.Init(oApp, oScript, Me, "", False, "", "") = False Then Return False
      Dim fun As System.Reflection.MethodInfo = oCleSial.GetType.GetMethod("Init")
      If Not CBool(fun.Invoke(oCleSial, New Object() {oApp, oScript, Me, "", False, "", ""})) Then Return False

      'oCleSial.oMenu = Me.oMenu
      memberInfo = oCleSial.GetType.GetMember("oMenu")(0)
      CType(memberInfo, System.Reflection.FieldInfo).SetValue(oCleSial, oMenu, Nothing, Nothing, Nothing)

      'Return oCleSial.Verifica_Genera_Alert(nTipoOperazione, strCodditt, strProgramma, lIdEvento, lIdAlert, dttMsgOutParam)
      fun = oCleSial.GetType.GetMethod("Verifica_Genera_Alert")
      Return CBool(fun.Invoke(oCleSial, New Object() {nTipoOperazione, strCodditt, strProgramma, lIdEvento, lIdAlert, dttMsgOutParam}))

      Return True

    Catch ex As Exception
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    Finally
      If bSetHandle Then
        'RemoveHandler oCleSial.RemoteEvent, AddressOf GestisciEventiEntitySial
        eventInfo.RemoveEventHandler(oCleSial, handler)
      End If
      If Not oCleSial Is Nothing Then oCleSial = Nothing
    End Try
  End Function


  Public Overridable Function VisualizzaPopup(ByVal strNomePc As String, ByVal strCodditt As String, ByVal lIdAlert As Integer, ByVal strDataOra As String, ByVal strMitt As String, ByVal strDesAlert As String, ByVal strTesto As String) As Boolean
    'tolto reference a BE__SIAL per poter gestire ANDROID
    Dim oCleSial As Object = Nothing
    Dim bSetHandle As Boolean = False
    Dim method As System.Reflection.MethodInfo = Nothing
    Dim eventInfo As System.Reflection.EventInfo = Nothing
    Dim memberInfo As System.Reflection.MemberInfo = Nothing
    Dim handler As [Delegate] = Nothing
    Try
      '-------------------------
      'creo SIAL per verificare e se necessario generare l'alert 
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BELBMENU", "BE__SIAL", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 128939609643700729, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleSial = oTmp

      'AddHandler oCleSial.RemoteEvent, AddressOf GestisciEventiEntitySial
      'aggancio l'evento tramite reflection
      method = Me.GetType.GetMethod("GestisciEventiEntitySial")
      eventInfo = oCleSial.GetType.GetEvent("RemoteEvent")
      handler = [Delegate].CreateDelegate(eventInfo.EventHandlerType, Me, method)
      eventInfo.AddEventHandler(oCleSial, handler)

      bSetHandle = True

      'If oCleSial.Init(oApp, oScript, Me, "", False, "", "") = False Then Return False
      Dim fun As System.Reflection.MethodInfo = oCleSial.GetType.GetMethod("Init")
      If Not CBool(fun.Invoke(oCleSial, New Object() {oApp, oScript, Me, "", False, "", ""})) Then Return False

      'oCleSial.oMenu = Me.oMenu
      memberInfo = oCleSial.GetType.GetMember("oMenu")(0)
      CType(memberInfo, System.Reflection.FieldInfo).SetValue(oCleSial, oMenu, Nothing, Nothing, Nothing)

      'Return oCleSial.VisualizzaPopup(strNomePc, strCodditt, lIdAlert, strDataOra, strMitt, strDesAlert, strTesto)
      fun = oCleSial.GetType.GetMethod("VisualizzaPopup")
      Return CBool(fun.Invoke(oCleSial, New Object() {strNomePc, strCodditt, lIdAlert, strDataOra, strMitt, strDesAlert, strTesto}))

    Catch ex As Exception
      Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    Finally
      If bSetHandle Then
        'RemoveHandler oCleSial.RemoteEvent, AddressOf GestisciEventiEntitySial
        eventInfo.RemoveEventHandler(oCleSial, handler)
      End If
      oCleSial = Nothing
    End Try
  End Function


  Public Overridable Sub GestisciEventiEntitySial(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      'utilizzato dall'entity CLE__SIAL per passare messaggi: li giro direttamente all'UI
      'ATTENZIONE: in base al tipo di messaggio posso ho ritornare una throw new ntsexception, oppure 
      'nel caso serva, pur sporcando il modello, posso fare una runchild per visualizzare un child contenente anche una sola msgbox
      '(è una porta aperta per il futuro, per ora potrebbe non servire ...)

      If oApp.TipoProgramma = CLE__APP.TIPOPROGR.Winfowm Then
        'se sono qui vuol dire che ho una form a disposizione: posso visualizzare il messaggio
        If GestErrorCallThrow() Then
          Throw New NTSException(e.Message)
        Else
          ThrowRemoteEvent(e)
        End If
      Else
        Throw New NTSException(e.Message)
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

#End Region

#Region "Datawarehouse"
  Public Overridable Function CheckTabscen(ByRef dsTabScen As DataSet) As Boolean
    Try
      oCldComm.LeggiTabellaSemplice(strDittaCorrente, "TABSCEN", dsTabScen)
      If dsTabScen.Tables("TABSCEN").Rows.Count = 0 Then
        CreaTabscen()
        oCldComm.LeggiTabellaSemplice(strDittaCorrente, "TABSCEN", dsTabScen)
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
  Public Overridable Function CreaTabscen() As Boolean
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Dim nScenario As Integer
    Try
      oCldComm.GetScenari(dsTmp)
      For i = 0 To dsTmp.Tables("TABDIME").Rows.Count - 1
        nScenario = NTSCInt(dsTmp.Tables("TABDIME").Rows(i)!scenario)
        oCldComm.InsertTabscen(NTSCStr(nScenario), DescScenariStandard(nScenario))
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
  Public Overridable Function DescScenariStandard(ByVal nScenario As Integer) As String
    Try
      Select Case nScenario
        Case 1 : Return "Ciclo attivo/passivo"
        Case 2 : Return "CRM Leads"
        Case 3 : Return "CRM Opportunità"
        Case 4 : Return "CRM Attività"
        Case 5 : Return "CRM Offerte"
        Case 7 : Return "Contabilità Analitica"
        Case 8 : Return "Contab. generale"
        Case 9 : Return "Customer Service"
        Case 10 : Return "CRM Offerte - Righe"
        Case 11 : Return "Scadenziario"
        Case Else : Return ""
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

  Public Overridable Function LeggiStatistica(ByRef dttPar As DataTable, ByRef dsDwarehe As DataSet) As Boolean
    Dim dsAggiorna As DataSet = Nothing
    Try
      'legge i parametri esenziali
      If Not PopoladttParEsenziali(dsAggiorna, dttPar) Then Return False

      'legge le tabelle della statistica
      If Not CaricaDatatableDatawarehouse(dsAggiorna, dttPar) Then Return False

      'legge i parametri statistica
      If Not PopoladttParDefault(dsAggiorna, dttPar) Then Return False

      Return oCldComm.CreaQuery(dsAggiorna, dsDwarehe, dttPar)

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
  Public Overridable Function CreaQuery(ByVal dsAggiorna As DataSet, ByRef dsDwarehe As DataSet, _
                                        ByRef dttPar As DataTable) As Boolean
    Try

      Return oCldComm.CreaQuery(dsAggiorna, dsDwarehe, dttPar)

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
  Public Overridable Function Analisi(ByVal nTipoScenario As Integer) As String
    Try
      Return oCldComm.Analisi(nTipoScenario)

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
  Public Overridable Function ConvertiInData(ByRef dttPar As DataTable, ByVal strData As String, ByRef strErrore As String) As String
    Try
      Return oCldComm.ConvertiInData(dttPar, strData, strErrore)

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
  Public Overridable Function PopoladttParEsenziali(ByRef dsAggiorna As DataSet, ByRef dttPar As DataTable) As Boolean
    Try
      'default leggo le info da tabdwim oppure da opzioni di registro
      If Not oCldComm.ControllaPresenzadttPar(dttPar, "strDitta") Then
        Return False
      End If
      If Not oCldComm.ControllaPresenzadttPar(dttPar, "lCoddwim") Then
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
  Public Overridable Function PopoladttParDefault(ByRef dsAggiorna As DataSet, ByRef dttPar As DataTable) As Boolean
    Try
      'default leggo le info da tabdwim oppure da opzioni di registro
      If Not oCldComm.ControllaPresenzadttPar(dttPar, "nTipoScenario") Then
        If Not dsAggiorna.Tables("TABDWIM") Is Nothing Then
          oCldComm.CreaRigadttPar(dttPar, "nTipoScenario", NTSCStr(dsAggiorna.Tables("TABDWIM").Rows(0)!tb_dwscenario), "V")
        Else
          Return False
        End If
      End If
      If Not oCldComm.ControllaPresenzadttPar(dttPar, "bCellDblClick") Then
        oCldComm.CreaRigadttPar(dttPar, "bCellDblClick", NTSCStr(IIf(False, "-1", "0")), "V")
      End If

      If oCldComm.LeggidttPar(dttPar, "bCellDblClick") = "0" Then
        If Not oCldComm.ControllaPresenzadttPar(dttPar, "strAlimSolofsel") Then
          If Not dsAggiorna.Tables("TABDWIM") Is Nothing Then
            oCldComm.CreaRigadttPar(dttPar, "strAlimSolofsel", NTSCStr(dsAggiorna.Tables("TABDWIM").Rows(0)!tb_solofsel), "V")
          Else
            Return False
          End If
        End If
        oCldComm.CreaRigadttPar(dttPar, "bGestAnaextC", NTSCStr(IIf(False, "-1", "0")), "V") 'default false dovrebbe essere passato
      Else
        oCldComm.CreaRigadttPar(dttPar, "strAlimSolofsel", "N", "V") 'sempre fisso a N per doppioclick
        oCldComm.CreaRigadttPar(dttPar, "bGestAnaextC", "0", "V") 'non seleziono i campi di anaext
      End If

      If Not oCldComm.ControllaPresenzadttPar(dttPar, "strSQLH") Then
        oCldComm.CreaRigadttPar(dttPar, "strSQLH", "", "R")
      End If
      If Not oCldComm.ControllaPresenzadttPar(dttPar, "strDitta") Then
        oCldComm.CreaRigadttPar(dttPar, "strDitta", strDittaCorrente, "V")
      End If
      If Not oCldComm.ControllaPresenzadttPar(dttPar, "lCoddwim") Then
        If Not dsAggiorna.Tables("TABDWIM") Is Nothing Then
          oCldComm.CreaRigadttPar(dttPar, "lCoddwim", NTSCStr(dsAggiorna.Tables("TABDWIM").Rows(0)!tb_coddwim), "V")
        Else
          Return False
        End If
      End If
      If Not oCldComm.ControllaPresenzadttPar(dttPar, "strAlimSelwherem") Then
        If Not dsAggiorna.Tables("TABDWIM") Is Nothing Then
          oCldComm.CreaRigadttPar(dttPar, "strAlimSelwherem", NTSCStr(dsAggiorna.Tables("TABDWIM").Rows(0)!tb_selwherem), "V")
        Else
          Return False
        End If
      End If
      If Not oCldComm.ControllaPresenzadttPar(dttPar, "strAlimMultid") Then
        If Not dsAggiorna.Tables("TABDWIM") Is Nothing Then
          oCldComm.CreaRigadttPar(dttPar, "strAlimMultid", NTSCStr(dsAggiorna.Tables("TABDWIM").Rows(0)!tb_multid), "V")
        Else
          Return False
        End If
      End If
      If Not oCldComm.ControllaPresenzadttPar(dttPar, "bGestJoinMov") Then
        oCldComm.CreaRigadttPar(dttPar, "bGestJoinMov", NTSCStr(IIf(CBool(oCldComm.GetSettingBus("BSDWQVDA", "OPZIONI", ".", "GestJoinMov", "0", " ", "0")), "-1", "0")), "V")
      End If
      If Not oCldComm.ControllaPresenzadttPar(dttPar, "bLikeNonOttimistico") Then
        oCldComm.CreaRigadttPar(dttPar, "bLikeNonOttimistico", NTSCStr(IIf(CBool(oCldComm.GetSettingBus("BSDWQVDA", "OPZIONI", ".", "LikeNonOttimistico", "0", " ", "0")), "-1", "0")), "V")
      End If
      If Not oCldComm.ControllaPresenzadttPar(dttPar, "strSQLV") Then
        oCldComm.CreaRigadttPar(dttPar, "strSQLV", oCldComm.LeggidttPar(dttPar, "strSQLH"), "R")
      End If
      dttPar.AcceptChanges()

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
  Public Overridable Function CaricaDatatableDatawarehouse(ByRef dsAggiorna As DataSet, ByRef dttPar As DataTable) As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      'leggo quelli essenziali per la creazione della query del datawarehouse
      dsAggiorna = Nothing
      dsAggiorna = New DataSet

      If Not GetTabdwim(oCldComm.LeggidttPar(dttPar, "strDitta"), NTSCInt(oCldComm.LeggidttPar(dttPar, "lCoddwim")), 0, False, _
                          dsTmp) Then Return False
      dsAggiorna.Tables.Add(dsTmp.Tables("TABDWIM").Copy)

      If Not GetDwselect(oCldComm.LeggidttPar(dttPar, "strDitta"), NTSCInt(oCldComm.LeggidttPar(dttPar, "lCoddwim")), dsTmp) Then Return False
      dsAggiorna.Tables.Add(dsTmp.Tables("DWSELECT").Copy)

      If Not CaricaFiltri(oCldComm.LeggidttPar(dttPar, "strDitta"), NTSCInt(oCldComm.LeggidttPar(dttPar, "lCoddwim")), dsTmp) Then Return False
      dsAggiorna.Tables.Add(dsTmp.Tables("DWFILT").Copy)

      If Not CaricaCalc(oCldComm.LeggidttPar(dttPar, "strDitta"), NTSCInt(oCldComm.LeggidttPar(dttPar, "lCoddwim")), dsTmp) Then Return False
      dsAggiorna.Tables.Add(dsTmp.Tables("DWCAMCAL").Copy)

      If dsAggiorna.Tables("TABDWIM").Rows.Count = 1 Then
        GetTabdime(NTSCInt(dsAggiorna.Tables("TABDWIM").Rows(0)!tb_dwscenario), dsTmp)
      Else
        Return False
      End If
      dsAggiorna.Tables.Add(dsTmp.Tables("TABDIME").Copy)

      CaricaUpdateTabdime(dsAggiorna.Tables("TABDIME"), dsAggiorna.Tables("DWSELECT"))

      'per ora per alleggerire il caricamento cruscotti non carico la tabella delle visibilità e relativo aggiornamento
      'If Not GetDwselvis(oCldComm.LeggidttPar(dttPar, "strDitta"), NTSCInt(oCldComm.LeggidttPar(dttPar, "lCoddwim")), dsTmp) Then Return False
      'dsAggiorna.Tables.Add(dsTmp.Tables("DWSELVIS").Copy)
      'AggiornaTabdimeVisDet(dsAggiorna.Tables("TABDIME"), dsAggiorna.Tables("DWSELVIS"))

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
  Public Overridable Function CaricaUpdateTabdime(ByRef dttTabdime As DataTable, ByRef dttDwselect As DataTable) As Boolean
    Dim i As Integer
    Dim dtrTmp() As DataRow
    Try
      For i = 0 To dttDwselect.Rows.Count - 1
        With dttDwselect.Rows(i)
          dtrTmp = dttTabdime.Select("tb_coddimd = " & NTSCStr(!ds_coddimd))
          If dtrTmp.Length > 0 Then
            dtrTmp(0)!xx_order = NTSCStr(!ds_order) 'CONTROLLLARE CANC vb6 net
            dtrTmp(0)!xx_selez = "S"
            dtrTmp(0)!xx_valcombo = NTSCStr(!ds_valcombo)
            dtrTmp(0)!tb_gruppo = NTSCStr(!ds_gruppo)
          End If
        End With
      Next
      dttTabdime.AcceptChanges()

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
  Public Overridable Function AggiornaTabdimeVisDet(ByRef dttTabdime As DataTable, ByRef dttDwselvis As DataTable) As Boolean
    Dim i As Integer
    Dim dtrTmp() As DataRow
    Try
      If dttDwselvis.Rows.Count = 0 Then
        For Each dtrDimeRow As DataRow In dttTabdime.Rows
          dtrDimeRow!xx_visdet = dtrDimeRow!tb_visdim
        Next
      Else
        For i = 0 To dttDwselvis.Rows.Count - 1
          With dttDwselvis.Rows(i)
            dtrTmp = dttTabdime.Select("tb_coddimd = " & NTSCStr(!dsv_coddimd))
            If dtrTmp.Length > 0 Then
              dtrTmp(0)!xx_visdet = NTSCStr(!dsv_visdet)
            End If
          End With
        Next
      End If
      dttTabdime.AcceptChanges()

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

  Public Overridable Function GetTabdime(ByVal nTipoScenario As Integer, ByRef ds As DataSet) As Boolean
    Try
      oCldComm.GetTabdime(nTipoScenario, ds)

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
  Public Overridable Function GetTabdwim(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                       ByVal nTipoScenario As Integer, ByVal bSoloCod As Boolean, _
                                       ByRef ds As DataSet) As Boolean
    Try
      oCldComm.GetTabdwim(strDitta, lCoddwim, nTipoScenario, bSoloCod, _
                          ds)

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
  Public Overridable Function GetDwselect(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                        ByRef ds As DataSet) As Boolean
    Try
      oCldComm.GetDwselect(strDitta, lCoddwim, ds)

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
  Public Overridable Function CaricaFiltri(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                      ByRef ds As DataSet) As Boolean
    Try
      oCldComm.CaricaFiltri(strDitta, lCoddwim, ds)

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
  Public Overridable Function CaricaCalc(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                    ByRef ds As DataSet) As Boolean
    Try
      oCldComm.CaricaCalc(strDitta, lCoddwim, ds)

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
  Public Overridable Function GetDwselvis(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                        ByRef ds As DataSet) As Boolean
    Try
      oCldComm.GetDwselvis(strDitta, lCoddwim, ds)

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
  Public Overridable Function GetDwcrus(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                        ByVal bNonApplicaFiltro As Boolean, ByRef ds As DataSet) As Boolean
    Try
      oCldComm.GetDwcrus(strDitta, lCoddwim, bNonApplicaFiltro, ds)

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
  Public Overridable Function GetDwlayoute(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                      ByRef ds As DataSet) As Boolean
    Try
      oCldComm.GetDwlayoute(strDitta, lCoddwim, ds)

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
  Public Overridable Function GetTabaext(ByVal strDitta As String, ByVal strTipo As String, _
                                         ByRef ds As DataSet) As Boolean
    Try
      oCldComm.GetTabaext(strDitta, strTipo, ds)

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

  Public Overridable Function GetDwimsto(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                         ByVal strDataoraStoricoCarica As String, ByRef ds As DataSet) As Boolean
    Try
      oCldComm.GetDwimsto(strDitta, lCoddwim, strDataoraStoricoCarica, ds)

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
  Public Overridable Function GetDwselsto(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                          ByVal strDataoraStoricoCarica As String, ByRef ds As DataSet) As Boolean
    Try
      oCldComm.GetDwselsto(strDitta, lCoddwim, strDataoraStoricoCarica, ds)

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
  Public Overridable Function CaricaDwfilsto(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                             ByVal strDataoraStoricoCarica As String, ByRef ds As DataSet) As Boolean
    Try
      oCldComm.CaricaDwfilsto(strDitta, lCoddwim, strDataoraStoricoCarica, ds)

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
  Public Overridable Function CaricaDwcamsto(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                             ByVal strDataoraStoricoCarica As String, ByRef ds As DataSet) As Boolean
    Try
      oCldComm.CaricaDwCamsto(strDitta, lCoddwim, strDataoraStoricoCarica, ds)

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
  Public Overridable Function GetDwsevsto(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                          ByVal strDataoraStoricoCarica As String, ByRef ds As DataSet) As Boolean
    Try
      oCldComm.GetDwsevsto(strDitta, lCoddwim, strDataoraStoricoCarica, ds)

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
  Public Overridable Function GetDwlaysto(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                          ByVal strDataoraStoricoCarica As String, ByRef ds As DataSet) As Boolean
    Try
      oCldComm.GetDwlaysto(strDitta, lCoddwim, strDataoraStoricoCarica, ds)

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


  Public Overridable Function AggiornaListini(ByVal strDitta As String, ByVal strCodart As String, _
    ByVal nCodvalu As Integer, ByVal strDatavalidita As String, ByVal nListino As Integer, _
    ByVal bQuant As Boolean, ByVal dQuant As Decimal, ByVal strDtinvigore As String, ByVal nCodpdon As Integer, _
    ByVal nCodlavo As Integer, ByVal nFase As Integer, _
    Optional ByVal strUnmis As String = "", _
    Optional ByVal strNetto As String = "", _
    Optional ByVal bSilent As Boolean = True) As Boolean
    Dim bAggListinoConPercZero As Boolean = False
    Dim bEsisteListino As Boolean = False
    Dim i As Integer = 2
    Dim j As Integer = 0
    Dim dPrezzo As Decimal = 0
    Dim dPerqta As Decimal = 0
    Dim dNuovoprezzo As Decimal = 0
    Dim dTmp As Decimal = 0
    Dim strTipoArro As String = ""
    Dim strUnitaMisura As String = ""
    Dim strPrezzoNetto As String = "N"
    Dim strNote As String = ""
    Dim strDataScadenza As String = ""
    Dim dttTmp As New DataTable
    Dim dttTabpdon As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strTipoArro = oCldComm.GetSettingBusDitt(strDitta, "BSMGELAR", "OPZIONI", ".", "TipoArrotondamento", "E", " ", "E")
      '--------------------------------------------------------------------------------------------------------------
      bAggListinoConPercZero = CBool(oCldComm.GetSettingBus("BSMGELAR", "OPZIONI", ".", "AggListinoConPercZero", "0", " ", "0"))
      '--------------------------------------------------------------------------------------------------------------
      '--- Prende l'Unità di Misura dell'articolo (se non è passata quella del listino)
      '--------------------------------------------------------------------------------------------------------------
      If strUnmis.Trim = "" Then
        oCldComm.ValCodiceDb(strCodart, strDitta, "ARTICO", "S", "", dttTmp)
        strUnitaMisura = NTSCStr(dttTmp.Rows(0)!ar_unmis)
        dttTmp.Clear()
        dttTmp.Dispose()
      Else
        strUnitaMisura = strUnmis
      End If
      '--------------------------------------------------------------------------------------------------------------
      If strNetto.Trim <> "" Then strPrezzoNetto = strNetto
      '--------------------------------------------------------------------------------------------------------------
      '--- Cerca il nuovo prezzo inserito in listini
      '--------------------------------------------------------------------------------------------------------------
      If oCldComm.SelezionaNuovoPrezzo(strDitta, strCodart, nCodvalu, strDtinvigore, nListino, nCodlavo, strUnitaMisura, nFase, dttTmp) = False Then
        If bSilent = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129231639595958580, "Non trovato listino di partenza.")))
          Return False
        End If
      Else
        dPrezzo = NTSCDec(dttTmp.Rows(0)!lc_prezzo)
        dPerqta = NTSCDec(dttTmp.Rows(0)!lc_perqta)
        strNote = NTSCStr(dttTmp.Rows(0)!lc_note)
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      '--- Cerca il record in TABPDON relativo all'articolo selezionato
      '--------------------------------------------------------------------------------------------------------------
      oCldComm.ValCodiceDb(nCodpdon.ToString, strDitta, "TABPDON", "N", "", dttTabpdon)
      '--------------------------------------------------------------------------------------------------------------
      For i = 2 To 8
        '------------------------------------------------------------------------------------------------------------
        '--- Se l'opzione di registro non è attiva e la percentuale di ricarico è = 0
        '--- salta l'aggiornamento del listino
        '------------------------------------------------------------------------------------------------------------
        If (bAggListinoConPercZero = False) And (NTSCDec(dttTabpdon.Rows(0)("tb_rica_" & i)) = 0) Then GoTo SaltaRecord
        '------------------------------------------------------------------------------------------------------------
        bEsisteListino = oCldComm.SelectLISTINIQ1(strDitta, strCodart, nCodvalu, strDatavalidita, i, nCodlavo, _
          strUnitaMisura, nFase, dttTmp)
        '------------------------------------------------------------------------------------------------------------
        If bEsisteListino = True Then
          For j = 0 To (dttTmp.Rows.Count - 1)
            If CDate(dttTmp.Rows(j)!lc_datagg) < CDate(DateAdd("d", -1, strDatavalidita)) Then
              oCldComm.UpdateLISTINIQ1(strDitta, strDatavalidita, NTSCInt(dttTmp.Rows(j)!lc_progr))
            End If
          Next
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Se la percentuale di ricarico è diversa da 0
        '------------------------------------------------------------------------------------------------------------
        If NTSCDec(dttTabpdon.Rows(0)("tb_rica_" & i)) <> 0 Then
          dNuovoprezzo = ArrDbl((dPrezzo + (dPrezzo * NTSCDec(dttTabpdon.Rows(0)("tb_rica_" & i)) / 100)), oCldComm.TrovaNdecSuPrzUn(nCodvalu))
          If NTSCDec(dttTabpdon.Rows(0)("tb_arro_" & i)) <> 0 Then
            If Not ((dNuovoprezzo / NTSCDec(dttTabpdon.Rows(0)("tb_arro_" & i))) = Fix(dNuovoprezzo / NTSCDec(dttTabpdon.Rows(0)("tb_arro_" & i)))) Then
              dTmp = Fix(dNuovoprezzo / NTSCDec(dttTabpdon.Rows(0)("tb_arro_" & i)))
              If strTipoArro = "E" Then 'eccesso
                dTmp += 1
              Else 'matematico
                dTmp = ArrDbl(dTmp, 0)
              End If
              dNuovoprezzo = ArrDbl((dTmp * NTSCDec(dttTabpdon.Rows(0)("tb_arro_" & i))), oCldComm.TrovaNdecSuPrzUn(nCodvalu))
            End If
          End If
          '----------------------------------------------------------------------------------------------------------
          '--- Aggiunge un nuovo record con la data di validità aggiornata
          '----------------------------------------------------------------------------------------------------------
          '--- Prima di inserire il listino controlla se c'è un listino posteriore
          '--- se esiste mette lc_datscad = al giorno prima del listino posteriore già esistente
          '--- altrimenti mette lc_datscad = 31/12/2099
          '----------------------------------------------------------------------------------------------------------
          strDataScadenza = oCldComm.SelectLISTINIQ2(strDitta, strCodart, nCodlavo, 0, nCodvalu, 0, i, _
            strDatavalidita, 0, strUnitaMisura, nFase, 0)
          '----------------------------------------------------------------------------------------------------------
          If oCldComm.ListinoGiaPresente(strDitta, strCodart, nCodlavo, 0, i, nCodvalu, 0, strDatavalidita, 0, nFase, 0) = False Then
            oCldComm.InsertLISTINI(strDitta, strCodart, nCodlavo, 0, nCodvalu, 0, i, strDatavalidita, " ", _
              dNuovoprezzo, strDataScadenza, 0, 9999999999, dPerqta, strUnitaMisura, strNote, strPrezzoNetto, nFase, 0)
          End If
          '----------------------------------------------------------------------------------------------------------
        Else
          '----------------------------------------------------------------------------------------------------------
          If bEsisteListino = False Then GoTo SaltaRecord
          '----------------------------------------------------------------------------------------------------------
          dNuovoprezzo = dPrezzo
          '----------------------------------------------------------------------------------------------------------
          strDataScadenza = oCldComm.SelectLISTINIQ2(strDitta, strCodart, nCodlavo, 0, nCodvalu, 0, i, _
            strDatavalidita, 0, strUnitaMisura, nFase, 0)
          '----------------------------------------------------------------------------------------------------------
          If oCldComm.ListinoGiaPresente(strDitta, strCodart, nCodlavo, 0, i, nCodvalu, 0, strDatavalidita, 0, nFase, 0) = False Then
            oCldComm.InsertLISTINI(strDitta, strCodart, nCodlavo, 0, nCodvalu, 0, i, strDatavalidita, " ", _
              dNuovoprezzo, strDataScadenza, 0, 9999999999, dPerqta, strUnitaMisura, strNote, strPrezzoNetto, nFase, 0)
          End If
          '----------------------------------------------------------------------------------------------------------
        End If
        '------------------------------------------------------------------------------------------------------------
SaltaRecord:
        '------------------------------------------------------------------------------------------------------------
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttTmp.Clear() : dttTmp.Dispose()
      dttTabpdon.Clear() : dttTabpdon.Dispose()
    End Try
  End Function

  Public Overridable Function RitornaListinoDaAnagraDestdiv(ByVal strDitta As String, ByVal lConto As Integer, ByVal lDestdiv As Integer) As Integer
    Dim lListino As Integer = -9999
    Dim dttTmp As New DataTable
    Try
      'Se è stata passata la destinazione, verifico se è associato un listino.
      If lDestdiv <> 0 Then
        ocldBase.ValCodiceDb(lDestdiv.ToString, strDitta, "DESTDIV", "N", "", dttTmp, lConto.ToString)
        If dttTmp.Rows.Count > 0 Then lListino = NTSCInt(dttTmp.Rows(0)!dd_listino)
      End If

      'Non era associato un listino alla destinazione, ritorno il listino dell'anagrafica
      If lListino = -9999 Then
        ocldBase.ValCodiceDb(lConto.ToString, strDitta, "ANAGRA", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          lListino = NTSCInt(dttTmp.Rows(0)!an_listino)
        Else
          lListino = 0 'Non dovrebbe mai capitare, ma in caso il conto non sia valido ritorna sempre il listino 0
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
    Return lListino
  End Function


#Region "Scrittura Priana2 in memoria su datatable identico a tabella"
  Public Overridable Function WriPriana2CG(ByVal strDitta As String, ByVal strDescrMsg As String, ByVal nEscomp As Integer, ByVal strDatregCG As String, _
                                       ByVal lNumregCG As Integer, ByVal nRigaCG As Integer, ByRef dttCa2 As DataTable, _
                                       ByVal dImporto As Decimal, ByVal nCodcaca As Integer, ByVal lContoCG As Integer, _
                                       ByVal strDarave As String, ByVal nContropCa As Integer, _
                                       ByVal nCodcena As Integer, ByVal strLinea As String, ByVal lCommeca As Integer, _
                                       ByVal nCoddivi As Integer, ByVal lCodstab As Integer, ByVal lCodcli As Integer, _
                                       ByVal strCodart As String, ByVal nFaseart As Integer, ByVal strDatini As String, ByVal strDatfin As String, ByVal strIntegr As String, _
                                       ByVal dQta As Decimal, ByRef strErr As String, Optional ByVal lContoCa As Integer = 0, _
                                       Optional ByVal lContropCa As Integer = 0, Optional ByVal strOrigine As String = "C", _
                                       Optional ByVal bChiusureAnnuali As Boolean = False) As Boolean
    Try
      Return WriPriana2CG(strDitta, strDescrMsg, nEscomp, strDatregCG, lNumregCG, nRigaCG, dttCa2, _
                          dImporto, nCodcaca, lContoCG, strDarave, nContropCa, nCodcena, strLinea, _
                          lCommeca, nCoddivi, lCodstab, lCodcli, strCodart, nFaseart, strDatini, _
                          strDatfin, strIntegr, dQta, strErr, lContoCa, lContropCa, strOrigine, _
                          bChiusureAnnuali, " ", 0)

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
  Public Overridable Function WriPriana2CG(ByVal strDitta As String, ByVal strDescrMsg As String, ByVal nEscomp As Integer, ByVal strDatregCG As String, _
                                         ByVal lNumregCG As Integer, ByVal nRigaCG As Integer, ByRef dttCa2 As DataTable, _
                                         ByVal dImporto As Decimal, ByVal nCodcaca As Integer, ByVal lContoCG As Integer, _
                                         ByVal strDarave As String, ByVal nContropCa As Integer, _
                                         ByVal nCodcena As Integer, ByVal strLinea As String, ByVal lCommeca As Integer, _
                                         ByVal nCoddivi As Integer, ByVal lCodstab As Integer, ByVal lCodcli As Integer, _
                                         ByVal strCodart As String, ByVal nFaseart As Integer, ByVal strDatini As String, ByVal strDatfin As String, ByVal strIntegr As String, _
                                         ByVal dQta As Decimal, ByRef strErr As String, ByVal lContoCa As Integer, _
                                         ByVal lContropCa As Integer, ByVal strOrigine As String, _
                                         ByVal bChiusureAnnuali As Boolean, ByVal strSubcommeca As String, _
                                         ByVal lIdwbs As Integer) As Boolean
    'scrivo priana2 
    'utilizzato da bepepnrt, bececalc, bececont, becgcaes, ...
    Dim dttTmp As New DataTable
    Dim dttAnagca As New DataTable
    Dim lProgr As Integer = 0
    Dim dttPecx As New DataTable
    Dim strAccperi As String = ""
    Dim strAccperiCG As String = ""
    Dim lContoCaMain As Integer = 0
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strDescrMsg, nEscomp, strDatregCG, lNumregCG, nRigaCG, dttCa2, _
                                          dImporto, nCodcaca, lContoCG, strDarave, nContropCa, nCodcena, strLinea, lCommeca, _
                                          nCoddivi, lCodstab, lCodcli, strCodart, nFaseart, strDatini, strDatfin, strIntegr, _
                                          dQta, strErr, lContoCa, lContropCa, strOrigine, bChiusureAnnuali, strSubcommeca, _
                                          lIdwbs})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttCa2 = CType(oIn(6), DataTable)
        strErr = NTSCStr(oIn(24))
        Return CBool(oOut)
      End If
      '----------------

      strErr = ""
      If dImporto = 0 Then Return True

      If strOrigine <> "C" Then
        'non deve essere collegata ad una reg. di CG
        lNumregCG = 0
        nRigaCG = 0
        lContoCG = 0
      Else
        '-------------------------------------------------
        'se il conto CG non è gestito a CA esco
        oCldComm.ValCodiceDb(lContoCG.ToString, strDitta, "ANAGRA", "N", "", dttTmp)
        If dttTmp.Rows.Count = 0 Then
          strErr = oApp.Tr(Me, 129291852932539062, "ATTENZIONE: errore in fase di scrittura 'Contabilità analitica duplice contabile' sulla riga del |" & strDescrMsg & "|:" & vbCrLf & "la registrazione di analitica è collegata alla contabilitàgenerale, ma non è stato indicato il conto di CG a cui deve essere collegata")
          Return False
        End If
        strAccperiCG = NTSCStr(dttTmp.Rows(0)!an_accperi)
        If NTSCStr(dttTmp.Rows(0)!an_flci).Trim = "" Then Return True
        dttTmp.Clear()
      End If

      oCldComm.ValCodiceDb("1", strDitta, "TABPECX", "N", "", dttPecx)

      '-------------------------------------------------
      'TEST PRE/SCRITTURA
      If lContoCa = 0 Then
        oCldComm.ValCodiceDb(nContropCa.ToString, strDitta, "TABCOVE", "N", "", dttTmp)
        If dttTmp.Rows.Count = 0 Then
          strErr = oApp.Tr(Me, 129290342191337891, "ATTENZIONE: errore in fase di scrittura 'Contabilità analitica duplice contabile' sulla riga del |" & strDescrMsg & "|:" & vbCrLf & "Contropartita |" & nContropCa.ToString & "| non presente nelle 'Contropartite Ditta'")
          Return False
        End If
        lContoCa = NTSCInt(dttTmp.Rows(0)!tb_concova)
        dttTmp.Clear()
      End If

      If lContoCa = 0 Then
        strErr = oApp.Tr(Me, 129290375998017578, "ATTENZIONE: errore in fase di scrittura 'Contabilità analitica duplice contabile' sulla riga del |" & strDescrMsg & "|:" & vbCrLf & "Conto di CA non impostato, oppure non indicato nella contropartita ditta |" & nContropCa.ToString & "|")
        Return False
      End If

      oCldComm.ValCodiceDb(lContoCa.ToString, strDitta, "ANAGCA", "N", "", dttAnagca)
      If dttAnagca.Rows.Count = 0 Then
        strErr = oApp.Tr(Me, 129291163717656250, "ATTENZIONE: errore in fase di scrittura 'Contabilità analitica duplice contabile' sulla riga del |" & strDescrMsg & "|:" & vbCrLf & "Conto di CA |" & lContoCa.ToString & "| non trovato nell'anagrafica sottoconti CA")
        Return False
      End If
      strAccperi = dttAnagca.Rows(0)!ac_accperi.ToString

      If strAccperiCG <> strAccperi And lContoCG <> 0 Then
        strErr = oApp.Tr(Me, 129291852447421875, "ATTENZIONE: errore in fase di scrittura 'Contabilità analitica duplice contabile' sulla riga del |" & strDescrMsg & "|:" & vbCrLf & "Conto di CA |" & lContoCa.ToString & "| ha una gestione date di competenza diverso rispetto a quelli di CG (|" & lContoCG.ToString & "|)")
        Return False
      End If

      lContoCaMain = lContoCa
      If Not WriPriana2_TestContoCa(dttAnagca, strDescrMsg, nCodcena, strLinea, lCommeca, nCoddivi, _
                                    lCodstab, lCodcli, strCodart, nFaseart, strDatini, strDatfin, _
                                    bChiusureAnnuali, strErr) Then Return False

      '-------------------------------------------------
      'progressivo di numerazione
      lProgr = oCldComm.LegNuma(strDitta, "PX", " ", 0, True)
      lProgr = oCldComm.AggNuma(strDitta, "PX", " ", 0, lProgr, True, True, strErr)
      If strErr <> "" Then Return False

PRIMARIGA:
      '-------------------------------------------------
      '1^ riga: il conto di CA
      dttCa2.Rows.Add(dttCa2.NewRow)
      With dttCa2.Rows(dttCa2.Rows.Count - 1)
        !codditt = strDitta
        !pa2_progr = lProgr
        !pa2_rigarg = IIf(strDarave = "D", 1, 2)  'se in DARE riga 1, altrimenti 2
        !pa2_darave = strDarave
        !pa2_idrigacontrop = IIf(strDarave = "D", 2, 1)
        !pa2_escomp = nEscomp
        !pa2_conto = lContoCa
        !pa2_codcaca = nCodcaca
        If NTSCStr(dttAnagca.Rows(0)!ac_richcena) <> "N" Then !pa2_codcena = nCodcena
        If NTSCStr(dttAnagca.Rows(0)!ac_richcfam) <> "N" Then !pa2_codcfam = strLinea
        If NTSCStr(dttAnagca.Rows(0)!ac_richcomm) <> "N" Then
          !pa2_commeca = lCommeca
          !pa2_subcommeca = strSubcommeca
          !pa2_idwbs = lIdwbs
        End If
        If NTSCStr(dttAnagca.Rows(0)!ac_richdivi) <> "N" Then !pa2_coddivi = nCoddivi
        If NTSCStr(dttAnagca.Rows(0)!ac_richstab) <> "N" Then !pa2_codstab = lCodstab
        If NTSCStr(dttAnagca.Rows(0)!ac_richcli) <> "N" Then !pa2_codcli = lCodcli
        If NTSCStr(dttAnagca.Rows(0)!ac_richarti) <> "N" Then
          !pa2_codart = strCodart
          !pa2_fase = nFaseart
        End If
        If NTSCStr(!pa2_codart) = "" Then !pa2_codart = " "
        !pa2_quant = IIf(strDarave = "D", dQta, dQta * -1)
        !pa2_quantda = dQta
        !pa2_importo = IIf(strDarave = "D", dImporto, dImporto * -1)
        !pa2_importoda = dImporto
        !pa2_origine = strOrigine
        !pa2_datreg = strDatregCG
        !pa2_numreg = lNumregCG
        !pa2_riga = nRigaCG
        !pa2_datini = strDatini
        !pa2_datfin = strDatfin
        !pa2_integr = strIntegr
        '!pa2_contocg = dtrCG!pn_conto    solo su riga di conto collegamento
        !pa2_ultagg = DateTime.Now
        !pa2_opnome = oApp.User.Nome
      End With    'With dttCa2.Rows(dttCa2.Rows.Count - 1)

SECONDARIGA:
      '2^ riga: il conto di collegamento / contropartita
      If strOrigine = "C" Then
        'determino il conto di collegamento da utilizzare (registrazione collegata a CG)
        If strDarave = "D" Then
          Select Case NTSCStr(dttAnagca.Rows(0)!ac_accperi)
            Case "N" : lContoCa = NTSCInt(dttPecx.Rows(0)!tb_acontocoll)
            Case "D" : lContoCa = NTSCInt(dttPecx.Rows(0)!tb_acontocolld)
            Case "S" : lContoCa = NTSCInt(dttPecx.Rows(0)!tb_acontocolls)
          End Select
        Else
          Select Case NTSCStr(dttAnagca.Rows(0)!ac_accperi)
            Case "N" : lContoCa = NTSCInt(dttPecx.Rows(0)!tb_dcontocoll)
            Case "D" : lContoCa = NTSCInt(dttPecx.Rows(0)!tb_dcontocolld)
            Case "S" : lContoCa = NTSCInt(dttPecx.Rows(0)!tb_dcontocolls)
          End Select
        End If
      Else
        lContoCa = lContropCa
      End If

      dttAnagca.Clear()
      If lContoCa = 0 Then
        strErr = oApp.Tr(Me, 129290316032451172, "ATTENZIONE: errore in fase di scrittura 'Contabilità analitica duplice contabile' sulla riga del |" & strDescrMsg & "|:" & vbCrLf & "Conto di collegamento CA non indicato in 'Personalizzazione contabilità analitica ditta'")
        Return False
      End If

      oCldComm.ValCodiceDb(lContoCa.ToString, strDitta, "ANAGCA", "N", "", dttAnagca)
      If dttAnagca.Rows.Count = 0 Then
        strErr = oApp.Tr(Me, 129291796585664062, "ATTENZIONE: errore in fase di scrittura 'Contabilità analitica duplice contabile' sulla riga del |" & strDescrMsg & "|:" & vbCrLf & "Conto di contropartita CA |" & lContoCa.ToString & "| non trovato nell'anagrafica sottoconti CA")
        Return False
      End If
      If strAccperi <> dttAnagca.Rows(0)!ac_accperi.ToString Then
        strErr = oApp.Tr(Me, 129291796965498047, "ATTENZIONE: errore in fase di scrittura 'Contabilità analitica duplice contabile' sulla riga del |" & strDescrMsg & "|:" & vbCrLf & "Conto di contropartita CA |" & lContoCa.ToString & "| con gestione date di competenza diversa da quella impostata sul conto di CA principale (|" & lContoCaMain.ToString & "|)")
        Return False
      End If
      If Not WriPriana2_TestContoCa(dttAnagca, strDescrMsg, nCodcena, strLinea, lCommeca, nCoddivi, _
                                    lCodstab, lCodcli, strCodart, nFaseart, strDatini, strDatfin, _
                                    bChiusureAnnuali, strErr) Then Return False

      dttCa2.Rows.Add(dttCa2.NewRow)
      With dttCa2.Rows(dttCa2.Rows.Count - 1)
        !codditt = strDitta
        !pa2_progr = lProgr
        !pa2_rigarg = IIf(strDarave = "D", 2, 1)  'inverso di riga 1
        !pa2_darave = IIf(strDarave = "D", "A", "D").ToString
        !pa2_idrigacontrop = IIf(strDarave = "D", 1, 2)
        !pa2_escomp = nEscomp
        !pa2_conto = lContoCa
        !pa2_codcaca = nCodcaca
        If NTSCStr(dttAnagca.Rows(0)!ac_richcena) <> "N" Then !pa2_codcena = nCodcena
        If NTSCStr(dttAnagca.Rows(0)!ac_richcfam) <> "N" Then !pa2_codcfam = strLinea
        If NTSCStr(dttAnagca.Rows(0)!ac_richcomm) <> "N" Then
          !pa2_commeca = lCommeca
          !pa2_subcommeca = strSubcommeca
          !pa2_idwbs = lIdwbs
        End If
        If NTSCStr(dttAnagca.Rows(0)!ac_richdivi) <> "N" Then !pa2_coddivi = nCoddivi
        If NTSCStr(dttAnagca.Rows(0)!ac_richstab) <> "N" Then !pa2_codstab = lCodstab
        If NTSCStr(dttAnagca.Rows(0)!ac_richcli) <> "N" Then !pa2_codcli = lCodcli
        If NTSCStr(dttAnagca.Rows(0)!ac_richarti) <> "N" Then
          !pa2_codart = strCodart
          !pa2_fase = nFaseart
        End If
        If NTSCStr(!pa2_codart) = "" Then !pa2_codart = " "
        !pa2_quant = IIf(strDarave = "D", dQta * -1, dQta)
        !pa2_quantda = dQta
        !pa2_importo = IIf(strDarave = "D", dImporto * -1, dImporto)
        !pa2_importoda = dImporto
        !pa2_origine = strOrigine
        !pa2_datreg = strDatregCG
        !pa2_numreg = lNumregCG
        !pa2_riga = nRigaCG
        !pa2_datini = strDatini
        !pa2_datfin = strDatfin
        !pa2_integr = strIntegr
        !pa2_contocg = IIf(strOrigine = "C", lContoCG, 0)
        !pa2_ultagg = DateTime.Now
        !pa2_opnome = oApp.User.Nome
      End With    'With dttCa2.Rows(dttCa2.Rows.Count - 1)

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
      dttAnagca.Clear()
      dttPecx.Clear()
    End Try
  End Function
  Public Overridable Function WriPriana2_TestContoCa(ByRef dttAnagca As DataTable, ByVal strDescrMsg As String, _
                                                     ByVal nCodcena As Integer, ByVal strLinea As String, _
                                                     ByVal lCommeca As Integer, ByVal nCoddivi As Integer, _
                                                     ByVal lCodstab As Integer, ByVal lCodcli As Integer, _
                                                     ByVal strCodart As String, ByVal nFaseArt As Integer, _
                                                     ByVal strDatini As String, ByVal strDatfin As String, _
                                                     ByVal bChiusureAnnuali As Boolean, ByRef strErr As String) As Boolean
    Dim strMsgBase As String = ""
    Try
      strMsgBase = oApp.Tr(Me, 129290939164570312, _
                           "ATTENZIONE: errore in fase di scrittura 'Contabilità analitica duplice contabile' " & _
                           "sulla riga del |" & strDescrMsg & "|:" & vbCrLf & "Il conto di CA |" & dttAnagca.Rows(0)!ac_conto.ToString & "| ")
      With dttAnagca.Rows(0)
        If NTSCStr(!ac_richcena) = "S" And nCodcena = 0 Then
          strErr = strMsgBase & oApp.Tr(Me, 129290323895390625, "è gestito a centro ma non è indicata tale informazione")
          Return False
        End If
        If NTSCStr(!ac_richcomm) = "S" And lCommeca = 0 Then
          strErr = strMsgBase & oApp.Tr(Me, 129290324781777344, "è gestito a commessa non è indicata tale informazione")
          Return False
        End If
        If NTSCStr(!ac_richcfam) = "S" And strLinea.Trim = "" Then
          strErr = strMsgBase & oApp.Tr(Me, 129290325076015625, "è gestito a linea/famiglia ma non è indicata tale informazione")
          Return False
        End If
        If NTSCStr(!ac_richdivi) = "S" And nCoddivi = 0 Then
          strErr = strMsgBase & oApp.Tr(Me, 129290325373613281, "è gestito a divisione ma non è indicata tale informazione")
          Return False
        End If
        If NTSCStr(!ac_richstab) = "S" And lCodstab = 0 Then
          strErr = strMsgBase & oApp.Tr(Me, 129290325572890625, "è gestito a stabilimento/filiale/negozio ma non è indicata tale informazione")
          Return False
        End If
        If NTSCStr(!ac_richcli) = "S" And lCodcli = 0 Then
          strErr = strMsgBase & oApp.Tr(Me, 129290325981201172, "è gestito a cliente CA ma non è indicata tale informazione")
          Return False
        End If
        If NTSCStr(!ac_richarti) = "S" And strCodart.Trim = "" Then
          strErr = strMsgBase & oApp.Tr(Me, 129290326760556641, "è gestito ad articolo ma non è indicata tale informazione")
          Return False
        End If
        If NTSCStr(!ac_accperi) = "S" And strDatini = strDatfin And bChiusureAnnuali = False Then
          strErr = strMsgBase & oApp.Tr(Me, 129290333330156250, "è gestito a 'periodo competenza economica' ma le date di inizio e fine competenza non sono diverse")
          Return False
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
#End Region

  Public Overridable Function CreaAnalottiDaLottox(ByVal strDitta As String, ByVal strCodart As String, ByVal strDatdoc As String, _
                                                   ByVal strLottox As String, ByVal strStatoProv As String, ByRef lId As Integer, ByRef strErr As String) As Boolean
    Try
      Return CreaAnalottiDaLottox(strDitta, strCodart, strDatdoc, strLottox, strStatoProv, lId, strErr, strDatdoc, "")
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
  Public Overridable Function CreaAnalottiDaLottox(ByVal strDitta As String, ByVal strCodart As String, ByVal strDatdoc As String, _
                                                  ByVal strLottox As String, ByVal strStatoProv As String, ByRef lId As Integer, _
                                                  ByRef strErr As String, ByVal strDatProd As String, ByVal strDatScad As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, strDatdoc, strLottox, strStatoProv, lId, strErr, strDatProd, strDatScad})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        lId = NTSCInt(oIn(5))
        strErr = NTSCStr(oIn(6))
        Return CBool(oOut)
      End If
      '----------------


      lId = 0 'ovvero analotti.alo_lotto
      strErr = ""
      If strLottox.Trim = "" Then Return True

      '----------------------
      'verifico se esiste già
      oCldComm.ValCodiceDb(strLottox, strDitta, "ANALOTTIX", "S", "", dttTmp, strCodart)
      If dttTmp.Rows.Count > 0 Then
        'strErr = oApp.Tr(Me, 129513953697258385, "Anagrafica lotti già esistente per articolo |'" & strCodart & "'|, lotto |'" & strLottox & "'|")
        lId = NTSCInt(dttTmp.Rows(0)!alo_lotto)
        Return True
      End If
      dttTmp.Clear()

      Return oCldComm.CreaAnalottiDaLottox(strDitta, strCodart, strDatdoc, strLottox, strStatoProv, lId, strErr, strDatProd, strDatScad)

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
  Public Overridable Function CreaAnalottiDaDoc(ByVal strDitta As String, ByRef dtrT As DataRow, ByRef dtrC As DataRow, _
                                                ByRef strLottoxOut As String, ByRef strErr As String) As Boolean
    'routine unica per creazione lotto in automatico
    'se sono qui vuol dire che l'articolo è gestito a lotti, non ho indicato il cod lotto ed è attiva la nuova gestione a lotti alfanumerici
    Dim strDatscad As String = ""
    Dim dttTmp As New DataTable
    Dim nCodlotx As Integer = 0
    Dim nGGscad As Integer = 0
    Dim lId As Integer = 0
    Dim strStatoProv As String = ""
    Dim strTmp As String = ""

    strErr = ""
    strLottoxOut = ""
    Try
      'test per no fare nulla
      If NTSCInt(dtrC!ec_magaz2) <> 0 Then Return True

      Select Case NTSCStr(dtrC!ec_tipork)
        Case "O", "$", "H", "M", "T", "L"
          'ok
        Case Else
          Return True
      End Select

      'devo creare l'anagrafica

      '----------------------
      'cerco di calcolare la data di scadenza e verifico se in artico è indicato di creare il lotto in automatico
      strDatscad = NTSCDate(dtrT!et_datdoc).ToShortDateString

      oCldComm.ValCodiceDb(NTSCStr(dtrC!ec_codart), strDitta, "ARTICO", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then nCodlotx = NTSCInt(dttTmp.Rows(0)!ar_codtlox)
      dttTmp.Clear()

      'non devo calcolare il lotto in automatico
      If nCodlotx = 0 Then Return True

      'data di scadenza
      oCldComm.ValCodiceDb(nCodlotx.ToString, strDitta, "TABLOTX", "N", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then Return True

      strDatscad = NTSCDate(strDatscad).AddDays(NTSCInt(dttTmp.Rows(0)!tb_ggscad)).ToShortDateString

      oCldComm.LottoxCreaID(strDitta, lId)

      'determino il codice da assegnare al lotto
      CreaAnalottiDaDoc_CompilaSezione(strDitta, NTSCStr(dttTmp.Rows(0)!tb_tipocod1), dtrT, dtrC, _
                                      NTSCStr(dttTmp.Rows(0)!tb_testofisso), strLottoxOut, _
                                      NTSCInt(dttTmp.Rows(0)!tb_lencod1), strTmp)
      strLottoxOut = strTmp
      CreaAnalottiDaDoc_CompilaSezione(strDitta, NTSCStr(dttTmp.Rows(0)!tb_tipocod2), dtrT, dtrC, _
                                      NTSCStr(dttTmp.Rows(0)!tb_testofisso), strLottoxOut, _
                                      NTSCInt(dttTmp.Rows(0)!tb_lencod2), strTmp)
      strLottoxOut += strTmp
      CreaAnalottiDaDoc_CompilaSezione(strDitta, NTSCStr(dttTmp.Rows(0)!tb_tipocod3), dtrT, dtrC, _
                                      NTSCStr(dttTmp.Rows(0)!tb_testofisso), strLottoxOut, _
                                      NTSCInt(dttTmp.Rows(0)!tb_lencod3), strTmp)
      strLottoxOut += strTmp
      CreaAnalottiDaDoc_CompilaSezione(strDitta, NTSCStr(dttTmp.Rows(0)!tb_tipocod4), dtrT, dtrC, _
                                      NTSCStr(dttTmp.Rows(0)!tb_testofisso), strLottoxOut, _
                                      NTSCInt(dttTmp.Rows(0)!tb_lencod4), strTmp)
      strLottoxOut += strTmp

      If strLottoxOut.Trim = "" Then Return True

      oCldComm.ValCodiceDb(NTSCInt(dtrT!et_conto).ToString, strDitta, "ANAGRA", "N", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then strStatoProv = NTSCStr(dttTmp.Rows(0)!an_stato).Trim
      dttTmp.Clear()

      'creo l'anagrafica
      Return oCldComm.CreaAnalottiDaLottoxDoc(strDitta, NTSCStr(dtrC!ec_codart), NTSCDate(dtrT!et_datdoc).ToShortDateString, _
                                              strLottoxOut, strDatscad, nCodlotx, lId, strStatoProv)

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
  Public Overridable Function CreaAnalottiDaDoc_CompilaSezione(ByVal strDitta As String, ByVal strTipo As String, _
                                                               ByRef dtrT As DataRow, ByRef dtrC As DataRow, _
                                                               ByVal strCodFisso As String, ByVal strPrefissoTmp As String, _
                                                               ByVal strSezLen As Integer, ByRef strOut As String) As Boolean
    'casi gestiti
    '" ", "Non gestito"})
    '"D", "Data docum. (AAAAMMGG)"})
    '"M", "Anno e mese docum (AAAAMM)"})
    '"Y", "Giorno da inizio anno"})
    '"F", "Cod. cli/forn. docum"})
    '"N", "Nazione cli/forn. docum"})
    '"C", "Cod. articolo"})
    '"X", "Cod. fisso"})
    '"G", "Gruppo merc. articolo"})
    '"S", "Gruppo/sottogr. merc. art."})
    '"I", "Famiglia articolo"})
    '"P", "Progressivo numerico"})
    Dim dttTmp As New DataTable
    strOut = ""
    Try
      Select Case strTipo
        Case " " : strOut = ""
        Case "D" : strOut = NTSCDate(dtrT!et_datdoc).ToString("yyyyMMdd")
        Case "M" : strOut = NTSCDate(dtrT!et_datdoc).ToString("yyyyMM")
        Case "Y" : strOut = (DateDiff(DateInterval.Day, NTSCDate(IntSetDate("01/01/" & NTSCDate(dtrT!et_datdoc).Year.ToString)), NTSCDate(dtrT!et_datdoc)) + 1).ToString("000")
        Case "F" : strOut = NTSCInt(dtrT!et_conto).ToString("000000000")
        Case "N"
          oCldComm.ValCodiceDb(NTSCInt(dtrT!et_conto).ToString, strDitta, "ANAGRA", "N", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then strOut = NTSCStr(dttTmp.Rows(0)!an_stato).PadRight(strSezLen).Substring(0, strSezLen)
        Case "C" : strOut = NTSCStr(dtrC!ec_codart).PadRight(strSezLen).Substring(0, strSezLen)
        Case "X" : strOut = strCodFisso.PadRight(strSezLen).Substring(0, strSezLen)
        Case "G"
          oCldComm.ValCodiceDb(NTSCStr(dtrC!ec_codart), strDitta, "ARTICO", "S", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then strOut = NTSCInt(dttTmp.Rows(0)!ar_gruppo).ToString("00")
        Case "S"
          oCldComm.ValCodiceDb(NTSCStr(dtrC!ec_codart), strDitta, "ARTICO", "S", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then strOut = NTSCInt(dttTmp.Rows(0)!ar_sotgru).ToString("0000")
        Case "I"
          oCldComm.ValCodiceDb(NTSCStr(dtrC!ec_codart), strDitta, "ARTICO", "S", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then strOut = NTSCStr(dttTmp.Rows(0)!ar_famprod).PadRight(strSezLen).Substring(0, strSezLen)
        Case "P"
          strOut = oCldComm.CreaAnalottiDaLottoxDoc_GetProgr(strDitta, strPrefissoTmp).ToString("00000")
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
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function ScaricaLottox_CercaLotti(ByVal strDitta As String, ByVal strCodart As String, _
                                                       ByVal lFase As Integer, ByVal lConto As Integer, _
                                                       ByVal strTipscarlotx As String, _
                                                       ByVal strDatdoc As String, ByVal nCodmaga As Integer, _
                                                       ByVal dQta As Decimal, ByVal strTipork As String, _
                                                       ByVal nTipobf As Integer, ByVal nCodcaum As Integer, _
                                                       ByRef dttMMcorpo As DataTable, _
                                                       ByRef dttOut As DataTable) As Boolean
    'dato un articolo, cerca i lotti da scaricare

    'per determinare i lotti da scaricare
    'faccio una query su lotcpro inner join analotti sul magazzino da scaricare
    'a seconda che sia fifo/lifo/data scadenza/ ordinerò i record da scaricare per data doc carico/data scadenza/ecc...


    'occhio: possono esserci problemi in caso di diversi utenti che scaricano lo stesso articolo/lotto in contemporanea,
    'visto che le righe del corpo di un documento in fase di creazione non sono visibili in atri documenti
    'verrà controllato al salvataggio del documento se lotti negativi con l'opzione di registro BSVEBOLL/OPZIONI/CheckLottiInScarico=-1)
    Dim dtrT() As DataRow = Nothing
    Try
      If Not oCldComm.ScaricaLottox_CercaLotti(strDitta, strCodart, lFase, strTipscarlotx, _
                                               strDatdoc, nCodmaga, dttOut) Then Return True

      'scarto i lotti con data scadenza ante data documento o data preparazione successiva al documento
      For Each dtrT1 As DataRow In dttOut.Select("alo_dtscad < " & CDataSQL(strDatdoc) & " OR alo_dtcarbus > " & CDataSQL(strDatdoc))
        dtrT1.Delete()
      Next
      dttOut.AcceptChanges()

      'se lo stesso articolo è indicato più volte all'interno dello stesso documento (es evado diverse righe d'ordine)
      'devo decurtare dal datatable ottenuto sopra quanto già consumato nelle righe precedenti
      If Not dttMMcorpo Is Nothing Then
        For Each dtrMM As DataRow In dttMMcorpo.Select("ec_codart = " & CStrSQL(strCodart) & _
                                                         " AND ec_fase = " & lFase & _
                                                         " AND ec_lotto <> 0 AND ec_quant <> 0")
          dtrT = dttOut.Select("alo_lotto = " & NTSCInt(dtrMM!ec_lotto).ToString)
          If dtrT.Length > 0 Then
            dtrT(0)!lp_esist = NTSCDec(dtrT(0)!lp_esist) - NTSCDec(dtrMM!ec_quant)
            If NTSCDec(dtrT(0)!lp_esist) <= 0 Then dtrT(0).Delete()
            dttOut.AcceptChanges()
          End If
        Next
      End If    'If Not dttMMcorpo Is Nothing Then

      'se ho scelto di scaricare un unico lotto, la quantità in giacenza deve essere >= alla quantità da scaricare
      If strTipscarlotx = "U" Then
        For Each dtrT1 As DataRow In dttOut.Select("lp_esist < " & CDblSQL(dQta))
          dtrT1.Delete()
        Next
        dttOut.AcceptChanges()
        If dttOut.Rows.Count = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130440194927424898, "Attenzione!" & vbCrLf & _
            "Non è stato attribuito nessun numero di lotto.")))
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


  Public Overridable Sub ConfigurazioneTabelleEssenziali()
    Try
      InizializzaContatti()            ' Creazione contatti da organig

      TrasferisciConfigurazionePosta() ' Trasferimento configurazione di posta sulla tabella e-mail conf

      GeneraLeadFittizio()             ' Genera il lead -1

      AttivaModuliFriendly()           ' Attiva i moduli che inizialmente non erano configurati
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


#Region "Gestione ACCCONF"
  Public Overridable Function AccconfGetBlocchi(ByVal strDitta As String, ByVal strTipork As String, _
                                                ByRef dttOut As DataTable) As Boolean
    'cerca in accconf i vincoli impostati per l'operatore su visib/modif/cancellaz.
    'tipork: 0 = tutti tranne la CG, 1 = tutta la CG, ! = offerte, mo_tipork = ordini/prop. d'ordine, mm_tipork per doc. di magazzino e note prel.

    Try
      dttOut = New DataTable
      If oApp.oGvar.bGestAccconf = False Then Return True
      Return oCldComm.AccconfGetBlocchi(strDitta, strTipork, dttOut)

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

  Public Overridable Function AccconfCheck(ByRef dttAccconf As DataTable, ByVal strTipoCheck As String, _
                                           ByVal strDitta As String, _
                                           ByVal strTipork As String, ByVal strSerie As String, _
                                           ByVal nTipobf As Integer, ByVal nMagaz As Integer, _
                                           ByVal nMmcausale As Integer, ByVal nCgcausale As Integer) As Boolean
    'controllo in accconf se posso eseguire l'operazione indicata in nTipoCheck
    'strTipoCheck = INS: inserimento, VIS = visualizzazione, MOD = modifica, CANC = cancellazione
    'se manca il record, sicuramente non posso eseguire l'operazione
    Dim strFiltro As String = ""
    Try
      If oApp.oGvar.bGestAccconf = False Then Return True

      If dttAccconf.Rows.Count = 0 Then Return False
      Select Case strTipoCheck
        Case "INS" : strFiltro = "ac_ins = 'S'"
        Case "VIS" : strFiltro = "ac_vis = 'S'"
        Case "MOD" : strFiltro = "ac_vis = 'S' AND ac_mod = 'S'"
        Case "CANC" : strFiltro = "ac_vis = 'S' AND ac_canc = 'S'"
      End Select
      If strTipork = "1" Then
        strFiltro += " AND ac_tipork = " & CStrSQL(strTipork)
      Else
        strFiltro += " AND (ac_tipork = " & CStrSQL(strTipork) & " OR ac_tipork = '0')"
      End If

      If strSerie <> "*" Then strFiltro += " AND (ac_serie = '*' OR ac_serie = " & CStrSQL(strSerie) & ")"
      If nTipobf <> 0 Then strFiltro += " AND (ac_tipobf = 0 OR ac_tipobf = " & nTipobf & ")"
      If nMmcausale <> 0 Then strFiltro += " AND (ac_mmcausale = 0 OR ac_mmcausale = " & nMmcausale & ")"
      If nMagaz <> 0 Then strFiltro += " AND (ac_magaz = 0 OR ac_magaz = " & nMagaz & ")"
      If nCgcausale <> 0 Then strFiltro += " AND (ac_cgcausale = 0 OR ac_cgcausale = " & nCgcausale & ")"

      'se è stato configurato sia il ruolo che l'operatore, prendo l'operatore 
      'se operatore non indicato c'è al suo posto '*', che come codice ASCII è 42
      If dttAccconf.Select(strFiltro, "ac_opnome DESC, ac_ruolo DESC").Length = 0 Then Return False

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

#Region "GPV Disconnesso"
  'Per evitare di dover fare tutte le query su reoffline per vedere se il negozio è disconnesso o no.
  Public strTipoDisconnesso As String = "" 'S=Server, C=Negozio disconnesso, N=Non attiva
  Public lNegozioDisconnesso As Integer = 0

  Public Overridable Function GPVDiscCheckSerieAbilitata(ByVal strDitta As String, ByVal strSerie As String) As Boolean
    Try
      'Controlla il tipo di installazione
      If strTipoDisconnesso = "" Then
        Dim dsTmp As DataSet = Nothing
        oCldComm.LeggiTabellaSemplice(strDitta, "REOFFLINE", dsTmp)
        If dsTmp.Tables("REOFFLINE").Rows.Count = 0 Then
          strTipoDisconnesso = "N"
        Else
          strTipoDisconnesso = NTSCStr(dsTmp.Tables("REOFFLINE").Rows(0)!ro_tipoinst)
          lNegozioDisconnesso = NTSCInt(dsTmp.Tables("REOFFLINE").Rows(0)!ro_codstab)
        End If
      End If

      'Valuta se può usare una determinata serie
      Select Case strTipoDisconnesso
        Case "N" : Return True ' non è attiva la modalità disconnessa, posso usare tutte le serie.
        Case "S" : Return oCldComm.GPVSerieAmmessaServer(strDitta, strSerie) 'sono il server, posso usare tutte le serie tranne quelle dei negozi disconnessi.
        Case "C" : Return oCldComm.GPVSerieAmmessaNegozio(strDitta, strSerie, lNegozioDisconnesso) 'sono un negozio disconnesso, posso usare solo le serie che mi sono state assegante.
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

  Public Overridable Function GPVDiscCheckSerieAbilitataSulNegozio(ByVal strDitta As String, ByVal strSerie As String, ByVal lNegozio As Integer) As Boolean
    Dim strTipo As String = ""
    Try
      'Controlla il tipo di installazione
      Dim dsTmp As DataSet = Nothing
      oCldComm.LeggiTabellaSemplice(strDitta, "REOFFLINE", dsTmp)
      If dsTmp.Tables("REOFFLINE").Rows.Count = 0 Then
        strTipo = "N"
      Else
        strTipo = NTSCStr(dsTmp.Tables("REOFFLINE").Rows(0)!ro_tipoinst)
      End If

      'Valuta se può usare una determinata serie
      If strTipo = "N" Then Return True ' non è attiva la modalità disconnessa, posso usare tutte le serie.

      'Verifica se negozio disconnesso
      If Not oCldComm.GPVCheckNegozioDisconnesso(strDitta, lNegozio, strTipo) Then Return False

      Select Case strTipo
        Case "S" : Return oCldComm.GPVSerieAmmessaServer(strDitta, strSerie) 'sono il server, posso usare tutte le serie tranne quelle dei negozi disconnessi.
        Case "C" : Return oCldComm.GPVSerieAmmessaNegozio(strDitta, strSerie, lNegozio) 'sono un negozio disconnesso, posso usare solo le serie che mi sono state assegante.
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
#End Region

#Region "Gestione Barcode EAN128"
  Public strMarcatoreEan128 As String = Nothing
  Public Overridable Function TrascodificaBarcodeEan128(ByVal strDitta As String, ByVal strChiamante As String, ByVal strBarcodeEAN128 As String, _
                                                        ByVal strCodiceBarcodeEAN128 As String, ByRef dttOut As DataTable) As Boolean
    'in strEAN128Barcode c'è il codice a barre effettivamente inserito (visto che nella e.proposedvalue non ci sono caratteri 'particolari')
    'dove ogni carattere SPARATO è separato da §
    Dim nPosStart As Integer = 0        'byte inizio marcatore
    Dim nPosEnd As Integer = 0          'byte fine marcatore
    Dim nPosEndDato As Integer = 0      'byte fine dato a cui il marcatore si riferisce
    Dim nCharX As Integer = 0           'numero di caratteri di delimitatore di campi a lunghezza variabile trattati
    Try
      dttOut = New DataTable
      dttOut.Columns.Add("tipo", GetType(String))
      dttOut.Columns.Add("valore", GetType(String))
      dttOut.AcceptChanges()
      '------------------------------
      If strCodiceBarcodeEAN128 = "" Then Return False
      '------------------------------
      If Not TrascodificaBarcodeEan128_IdentificaBarcode(strDitta, strChiamante, strBarcodeEAN128, dttOut) Then Return False
      '------------------------------
      'Marcatore ean128 personalizzato
      If strMarcatoreEan128 Is Nothing Then strMarcatoreEan128 = oCldComm.GetSettingBus("OPZIONI", ".", ".", "FineMarcatoreEan128", "", ".", "")
      '------------------------------
      'tolgo i caratteri in più (CONTROL, ALT, SHIFT, RETURN FINALE, RIGHT FINALE)
      strCodiceBarcodeEAN128 = strCodiceBarcodeEAN128.Replace("ControlKey§", "").Replace("ShiftKey§", "").Replace("AltKey§", "").Replace("Return§", "").Replace("Right§", "").Replace("Left§", "").Replace("Up§", "").Replace("Down§", "")
      Dim strEAN128Parts() As String = strCodiceBarcodeEAN128.Split(New Char() {"§"c}, StringSplitOptions.RemoveEmptyEntries)

      '------------------------------
      'inizio a valutare la stringa passata in input
      nPosStart = 0
      While nPosStart < strBarcodeEAN128.Length - 1
        TrascodificaBarcodeEan128_IdentificaTipoMarcatore(strBarcodeEAN128, strEAN128Parts, nPosStart, nPosEnd, nPosEndDato, nCharX)

        If nPosEndDato > strBarcodeEAN128.Length Then Exit While
        If nPosEndDato - nPosEnd < 0 OrElse nPosEnd - nPosStart < 0 Then Exit While

        TrascodificaBarcodeEan128_AssociaDato(strBarcodeEAN128.Substring(nPosStart, nPosEnd - nPosStart).Trim, _
                                              strBarcodeEAN128.Substring(nPosEnd, nPosEndDato - nPosEnd).Trim, dttOut)

        nPosStart = nPosEndDato
        'In questo momento la posizione corrente dovrebbe essere un numero, se non lo è passo al carattere successivo
        While nPosStart < strBarcodeEAN128.Length AndAlso Not IsNumeric(strBarcodeEAN128(nPosStart))
          nPosStart += 1
        End While
      End While

      If Not TrascodificaBarcodeEan128_ControlliFinaliDati(dttOut) Then Return False

      dttOut.AcceptChanges()

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

  Public Overridable Function TrascodificaBarcodeEan128_IdentificaBarcode(ByVal strDitta As String, ByVal strChiamante As String, ByVal strBarcodeEAN128 As String, ByVal dttOut As DataTable) As Boolean
    Dim dsBarcode As DataSet = Nothing
    Dim dQuant, dColli As Decimal
    Dim strUnMis As String = ""
    Try
      'cerco di parire se è un EAN 128 ed ottengo il barcode
      If strBarcodeEAN128.Length >= 16 Then
        Dim strCodice As String = strBarcodeEAN128.Substring(0, 2)
        If strCodice = "01" OrElse strCodice = "02" Then
          Dim strBarcode As String = strBarcodeEAN128.Substring(2, 14)
          oCldComm.TrovaCodartDaBarcode(strChiamante, strBarcode, strDitta, strUnMis, dQuant, dColli, 0, "", dsBarcode)
          If dsBarcode.Tables("BARCODE").Rows.Count = 0 Then
            'Caso particolare, i barcode che iniziano con 0, 00 non sono GTIN 14, li cerco nuovamente senza gli 0.
            ' Con 9 iniziale è GTIN 14 a quantità variabile
            ' I caratteri _ sono aggiunti perchè così nella query la ricerca è più ampia.
            If strBarcode.StartsWith("00") Then
              strBarcode = "%" & strBarcode.Substring(2)
            ElseIf strBarcode.StartsWith("0") OrElse strBarcode.StartsWith("9") Then
              strBarcode = "%" & strBarcode.Substring(1)
            Else
              Return False ' Non è un GTIN 12/13 o un GTIN 14 variabile
            End If
            strBarcode = strBarcode.Remove(strBarcode.Length - 1) & "%"

            oCldComm.TrovaBarcodeEAN14(strDitta, strBarcode, dsBarcode)
            If dsBarcode.Tables("BARCODE").Rows.Count > 0 Then
              'Essendo dei casi particolari le quantità sono fisse a 1, saranno poi indicate dentro i tag del barcode le quantità vere:
              'Vedi tag 37 e 310x
              strUnMis = NTSCStr(dsBarcode.Tables("BARCODE").Rows(0)!bc_unmis)
              dColli = 1
              dQuant = 1
            End If
          ElseIf NTSCStr(dsBarcode.Tables("BARCODE").Rows(0)!bc_tipo) = "U" Then
            'Barcode dicihiarato come GTIN\EAN 14. La quantità è libera, e uso quella indicata.
            dColli = 1
            dQuant = NTSCDec(dsBarcode.Tables("BARCODE").Rows(0)!bc_quant)
          End If
        End If
      End If

      If dsBarcode Is Nothing OrElse dsBarcode.Tables("BARCODE").Rows.Count = 0 Then Return False 'non è stato codificato in business

      'è un EAN 128 (o potrebbe esserlo): continuo nella ricerca dei barcatori di campo
      dttOut.Rows.Add(New Object() {"Articolo", dsBarcode.Tables("BARCODE").Rows(0)!bc_codart})
      dttOut.Rows.Add(New Object() {"Fase", dsBarcode.Tables("BARCODE").Rows(0)!bc_fase})
      dttOut.Rows.Add(New Object() {"UnitaMisura", strUnMis})
      dttOut.Rows.Add(New Object() {"Colli", dColli})
      dttOut.Rows.Add(New Object() {"Quantita", dQuant})
      dttOut.Rows.Add(New Object() {"Taglia", dsBarcode.Tables("BARCODE").Rows(0)!bc_tagl})

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

  Public Overridable Sub TrascodificaBarcodeEan128_IdentificaTipoMarcatore(ByVal strBarcodeEAN128 As String, ByVal strEAN128Parts() As String, _
                                                                           ByVal nPosStart As Integer, ByRef nPosEnd As Integer, ByRef nPosEndDato As Integer, _
                                                                           ByRef nCharX As Integer)
    Try
      Select Case strBarcodeEAN128.Substring(nPosStart, 2)
        Case "00"
          nPosEnd = nPosStart + 2
          nPosEndDato = nPosEnd + 18
        Case "01", "02"
          nPosEnd = nPosStart + 2
          nPosEndDato = nPosEnd + 14
        Case "10", "21", "22", "23", "24", "25", "37", "40", "42", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99"
          'lunghezza variabile
          nPosEnd = nPosStart + 2
          nPosEndDato = TrascodificaBarcodeEan128_TrovaFineMarcatore(strEAN128Parts, nPosEnd + nCharX) - nCharX
          If nPosEndDato > -1 Then
            nCharX += 1                 'memorizzo il fatto che ho trovato un marcatore di fine campo. mi servirà per mantenere allineato il cursore sulla stringa con l'array di byte
          Else
            nPosEndDato = strBarcodeEAN128.Length  'non è stato trovato il marcatore di fine campo: vuol dire che arriva fino alla fine del barcode
          End If
        Case "11", "13", "15", "17"
          nPosEnd = nPosStart + 2
          nPosEndDato = nPosEnd + 6
        Case "20"
          nPosEnd = nPosStart + 2
          nPosEndDato = nPosEnd + 2
        Case "31", "32", "33", "34", "35", "36"
          nPosEnd = nPosStart + 4
          nPosEndDato = nPosEnd + 6
        Case "41"
          nPosEnd = nPosStart + 3
          nPosEndDato = nPosEnd + 13
        Case "80"
          nPosEnd = nPosStart + 4
          Select Case strBarcodeEAN128.Substring(nPosStart, 4)
            Case "8001", "8003"
              nPosEndDato = nPosEnd + 14
            Case "8002", "8004"
              nPosEndDato = TrascodificaBarcodeEan128_TrovaFineMarcatore(strEAN128Parts, nPosEnd + nCharX) - nCharX
              If nPosEndDato > -1 Then
                nCharX += 1                 'memorizzo il fatto che ho trovato un marcatore di fine campo. mi servirà per mantenere allineato il cursore sulla stringa con l'array di byte
              Else
                nPosEndDato = strBarcodeEAN128.Length  'non è stato trovato il marcatore di fine campo: vuol dire che arriva fino alla fine del barcode
              End If
            Case "8005"
              nPosEndDato = nPosEnd + 6
          End Select
        Case "81"
          nPosEnd = nPosStart + 4
          Select Case strBarcodeEAN128.Substring(nPosStart, 4)
            Case "8100" : nPosEndDato = nPosEnd + 6
            Case "8101" : nPosEndDato = nPosEnd + 10
            Case "8102" : nPosEndDato = nPosEnd + 2
          End Select
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
  Public Overridable Function TrascodificaBarcodeEan128_TrovaFineMarcatore(ByVal strEAN128() As String, ByVal nStart As Integer) As Integer
    Try
      For i As Integer = nStart To strEAN128.Length - 1
        Dim strCurrent As String = strEAN128(i).ToUpper
        If strCurrent = "APPS" OrElse strCurrent = "MENU" OrElse strCurrent = "F8" OrElse strCurrent = strMarcatoreEan128 Then Return i
      Next
      Return -1
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

  Public Overridable Sub TrascodificaBarcodeEan128_AssociaDato(ByVal strCode As String, ByVal strDato As String, ByVal dttDati As DataTable)
    Try
      'tratto solo i codici che mi interessano
      Select Case strCode
        Case "10" : dttDati.Rows.Add(New Object() {"Lotto", strDato})
        Case "11" : dttDati.Rows.Add(New Object() {"DataProduzione", TrascodificaBarcodeEan128_ConvertiData(strDato, False)})
        Case "15" : dttDati.Rows.Add(New Object() {"DataConsumoEntroIl", TrascodificaBarcodeEan128_ConvertiData(strDato, True)})
        Case "17" : dttDati.Rows.Add(New Object() {"DataScadenza", TrascodificaBarcodeEan128_ConvertiData(strDato, True)})
        Case "21" : dttDati.Rows.Add(New Object() {"Matricola", strDato})
        Case "37" : dttDati.Rows.Add(New Object() {"Moltiplicatore", strDato})
        Case Else
          If strCode.StartsWith("310") Then 'Il tipo quantità c'è per forza.
            dttDati.Select("tipo = 'Quantita'")(0)!valore = NTSCDec(NTSCDec(strDato) / 10 ^ NTSCInt(strCode.Substring(3, 1)))
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
  Public Overridable Function TrascodificaBarcodeEan128_ControlliFinaliDati(ByVal dttDati As DataTable) As Boolean
    Try
      Return TrascodificaBarcodeEan128_ControlliFinaliDati(dttDati, strDittaCorrente)
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
  Public Overridable Function TrascodificaBarcodeEan128_ControlliFinaliDati(ByVal dttDati As DataTable, ByVal strDitta As String) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dttDati, strDitta})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------
      'Se alla fine c'è il codice 15 ma non il 17, allora copio nel 17 il valore del 15
      Dim dtrEntroIl() As DataRow = dttDati.Select("tipo = 'DataConsumoEntroIl'")
      If dtrEntroIl.Length > 0 Then
        If dttDati.Select("tipo = 'DataScadenza'").Length = 0 Then dttDati.Rows.Add(New Object() {"DataScadenza", dtrEntroIl(0)!valore})
      End If

      'Se esiste il moltiplicatore delle quantità\colli, allora moltiplico quantità e colli trovati per quel valore.
      Dim dtrMoltiplicatore() As DataRow = dttDati.Select("tipo = 'Moltiplicatore'")
      If dtrMoltiplicatore.Length > 0 Then
        Dim dtrQuant As DataRow = dttDati.Select("tipo = 'Quantita'")(0)
        dtrQuant!valore = NTSCDec(dtrQuant!valore) * NTSCDec(dtrMoltiplicatore(0)!valore)

        Dim dtrColli As DataRow = dttDati.Select("tipo = 'Colli'")(0)
        dtrColli!valore = NTSCDec(dtrColli!valore) * NTSCDec(dtrMoltiplicatore(0)!valore)
      End If


      Dim dtrLotto() As DataRow = dttDati.Select("tipo = 'Lotto'")
      If dtrLotto.Length > 0 Then
        '----------------------
        'determino il codice numerico del lotto
        Dim dttTmp As New DataTable
        oCldComm.ValCodiceDb(strDitta, strDitta, "ANADITAC", "S", "", dttTmp)
        If dttTmp.Rows.Count > 0 AndAlso NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "N" Then
          dtrLotto(0)!valore = NTSCStr(dtrLotto(0)!valore).TrimStart("0"c) 'gli 0 davanti non servono per idendificare un lotto numerico, potrebbero essere troppi

          If Not IsNumeric(dtrLotto(0)!valore) OrElse NTSCInt(dtrLotto(0)!valore) < 0 OrElse NTSCStr(dtrLotto(0)!valore).Length > 9 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130615713195008418, "Il lotto |'" & NTSCStr(dtrLotto(0)!valore) & "' non è valido per la gestione numerica dei lotti")))
            dtrLotto(0).Delete()
          Else
            dtrLotto(0)!valore = NTSCStr(dtrLotto(0)!valore).PadLeft(9, "0"c) ' aggiungo gli 0 davanti per far divenare il lotto come da standard di business
          End If
        End If
      End If

      dttDati.AcceptChanges()

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

  Public Overridable Function TrascodificaBarcodeEan128_ConvertiData(ByVal strDate As String, ByVal bFineMeseSeGiorno0 As Boolean) As Date
    Try
      Dim lAnno As Integer = NTSCInt(strDate.Substring(0, 2)) + 2000
      Dim lMese As Integer = NTSCInt(strDate.Substring(2, 2))
      Dim lGiorno As Integer = NTSCInt(strDate.Substring(4, 2))

      'Se il giorno non è indicato valuto come gestirlo
      If lGiorno = 0 Then
        If bFineMeseSeGiorno0 Then
          Return New Date(lAnno, lMese, 1).AddMonths(1).AddDays(-1)
        Else
          Return New Date(lAnno, lMese, 1)
        End If
      Else
        Return New Date(lAnno, lMese, lGiorno)
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
#End Region

#Region "Gestione Contatti\Organig"
  Public Overridable Sub InizializzaContatti()
    Dim dttOrganig As DataTable = Nothing
    Try
      'Verifica se ci sono delle organizzazioni ma non i contatti, allora vuol dire che vanno creati i contatti
      If oCldComm.VerificaPresenzaContatti() Then Return
      If Not oCldComm.VerificaPresenzaOrganizzazioni() Then Return

      'Sono in una situazione nuova, devo creare i contatti. Prima cosa svuoto ogni riferimento da organig dei contatti
      ' per evitare che la procedura crea collegamenti errati a causa di una situazione sporca (ec: TRUNCATE della tabella contatti a db avviato)
      If Not oCldComm.AzzeraContattiSuOrganizzazioni() Then Return

      'Carica tutte le organizzazioni da aggiornare (lavora su TUTTE le ditte)
      If Not oCldComm.CaricaOrganizzazioni(dttOrganig) Then Return

      'Scorre tutte le organizzazioni e per ognuna valuta se creare un nuovo contatto o collegarlo a uno esistente
      For Each dtrOrg As DataRow In dttOrganig.Rows
        CollegaOrganizzazioneAContatto(dtrOrg)
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

  Public Overridable Function CollegaOrganizzazioneAContatto(ByVal dtrOrganig As DataRow) As Boolean
    Dim dttContatti As DataTable = Nothing
    Dim dtrSelected As DataRow = Nothing
    Dim lMaxPoint As Integer = 0
    Try
      'Valuta se creare un nuovo contatto o se utilizzarne uno esistente.
      '1) se il nome e cognome non è stato indicato passa subito a creare un nuovo contatto senza nome e cognome
      If NTSCStr(dtrOrganig!og_descont).Trim = "" AndAlso NTSCStr(dtrOrganig!og_descont2).Trim = "" Then
        oCldComm.CreaNuovoContatto(dtrOrganig)
        Return True
      End If

      '2) Valuta se ci sono dei contatti compatibili a parità di Cognome\Nome o Nome\Cognome
      If Not oCldComm.CercaContattiPerNomeCognomeEsatto(NTSCStr(dtrOrganig!og_descont), NTSCStr(dtrOrganig!og_descont2), dttContatti) Then Return False

      '3) Scansiona la lista dei contatti per assegnargli un punteggio e valutare la probabilità che siano la stessa persona.
      For Each dtrContatto As DataRow In dttContatti.Rows
        Dim lCoerenze As Integer = 0
        Dim lConflitti As Integer = 0
        ControllaPunteggioCollegamento(dtrOrganig, dtrContatto, lCoerenze, lConflitti)

        'Se non ci sono stati conflitti, ma solo coerenze, allora assegna la riga all'attuale coerenza trovata
        If lConflitti = 0 AndAlso lCoerenze > lMaxPoint AndAlso lCoerenze > 2 Then dtrSelected = dtrContatto : lMaxPoint = lCoerenze
      Next

      If dtrSelected Is Nothing Then
        oCldComm.CreaNuovoContatto(dtrOrganig) 'Non è stata trovata alcuna associazione, creo un nuovo contatto
      Else
        'Associazione trovata, collego l'organizzazione al contatto e riverso i dati mancanti tra le entità.
        oCldComm.CollegaOrganizzazioneAContatto(dtrOrganig, dtrSelected)
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
  Public Overridable Sub ControllaPunteggioCollegamento(ByVal dtrOrganig As DataRow, ByVal dtrContatto As DataRow, ByRef lCoerenze As Integer, _
                                                        ByRef lConflitti As Integer)
    Try
      'Se un parametro è uguale associa un certo quantitativo di punti alle coerenze
      'Se da una parte è valorizzato e dell'altra no, non assegna punti
      'Se è diverso assegna punti ai conflitti

      'Il nome e cognome sono controllati a monte, quindi non generano conflitti, ma solo coerenze (con effetto minimo).
      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_descont), NTSCStr(dtrContatto!co_descont), 2, lCoerenze, 0)
      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_descont2), NTSCStr(dtrContatto!co_descont2), 2, lCoerenze, 0)
      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_descont), NTSCStr(dtrContatto!co_descont2), 1, lCoerenze, 0)
      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_descont2), NTSCStr(dtrContatto!co_descont), 1, lCoerenze, 0)

      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_titolo), NTSCStr(dtrContatto!co_titolo), 1, lCoerenze, lConflitti)
      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_indir), NTSCStr(dtrContatto!co_indir), 7, lCoerenze, lConflitti)
      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_cap), NTSCStr(dtrContatto!co_cap), 2, lCoerenze, lConflitti)
      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_citta), NTSCStr(dtrContatto!co_citta), 2, lCoerenze, lConflitti)
      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_prov), NTSCStr(dtrContatto!co_prov), 2, lCoerenze, lConflitti)
      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_stato), NTSCStr(dtrContatto!co_stato), 1, lCoerenze, lConflitti)
      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_datnasc), NTSCStr(dtrContatto!co_datnasc), 7, lCoerenze, lConflitti)

      ControllaPunteggioCollegamento_Numero(NTSCDec(dtrOrganig!og_codstco), NTSCDec(dtrContatto!co_codstco), 1, lCoerenze, lConflitti)
      ControllaPunteggioCollegamento_Numero(NTSCDec(dtrOrganig!og_referente), NTSCDec(dtrContatto!co_referente), 4, lCoerenze, lConflitti)

      'ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_sesso), NTSCStr(dtrContatto!co_sesso), 1, lCoerenze, lConflitti) Ignoro, non rilevante
      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_coperat), NTSCStr(dtrContatto!co_coperat), 10, lCoerenze, lConflitti)
      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_fbuser), NTSCStr(dtrContatto!co_fbuser), 10, lCoerenze, lConflitti)
      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_twitteruser), NTSCStr(dtrContatto!co_twitteruser), 10, lCoerenze, lConflitti)
      ControllaPunteggioCollegamento_Stringa(NTSCStr(dtrOrganig!og_skypeuser), NTSCStr(dtrContatto!co_skypeuser), 10, lCoerenze, lConflitti)
      ControllaPunteggioCollegamento_Stringa(SemplificaNumeroTelefono(NTSCStr(dtrOrganig!og_telefpers)), SemplificaNumeroTelefono(NTSCStr(dtrContatto!co_telefpers)), 8, lCoerenze, lConflitti)
      ControllaPunteggioCollegamento_Stringa(SemplificaNumeroTelefono(NTSCStr(dtrOrganig!og_faxpers)), SemplificaNumeroTelefono(NTSCStr(dtrContatto!co_faxpers)), 8, lCoerenze, lConflitti)
      ControllaPunteggioCollegamento_Stringa(SemplificaNumeroTelefono(NTSCStr(dtrOrganig!og_emailpers)), SemplificaNumeroTelefono(NTSCStr(dtrContatto!co_emailpers)), 8, lCoerenze, lConflitti)
      ControllaPunteggioCollegamento_Stringa(SemplificaNumeroTelefono(NTSCStr(dtrOrganig!og_cellpers)), SemplificaNumeroTelefono(NTSCStr(dtrContatto!co_cellpers)), 8, lCoerenze, lConflitti)
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
  Public Overridable Sub ControllaPunteggioCollegamento_Stringa(ByVal strValOrganig As String, ByVal strValContatto As String, _
                                                                ByVal lPunti As Integer, ByRef lCoerenze As Integer, ByRef lConflitti As Integer)
    Try
      'Verifica il punteggio per i campi di tipo stringa
      strValOrganig = strValOrganig.Trim.ToUpper
      strValContatto = strValContatto.Trim.ToUpper

      If strValOrganig = "" OrElse strValContatto = "" Then Return 'campo non valorizzato, ritorno senza assegnare punti

      If strValOrganig = strValContatto Then
        lCoerenze += lPunti 'Dato coerente, assegna i punti alle coerenze
      Else
        lConflitti += lPunti 'Conflitto sui dati, assegna i punti ai conflitti
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
  Public Overridable Sub ControllaPunteggioCollegamento_Numero(ByVal dValOrganig As Decimal, ByVal dValContatto As Decimal, _
                                                               ByVal lPunti As Integer, ByRef lCoerenze As Integer, ByRef lConflitti As Integer)
    Try
      'Verifica il punteggio per i campi di tipo stringa
      If dValOrganig = 0 OrElse dValContatto = 0 Then Return 'campo non valorizzato, ritorno senza assegnare punti

      If dValOrganig = dValContatto Then
        lCoerenze += lPunti 'Dato coerente, assegna i punti alle coerenze
      Else
        lConflitti += lPunti 'Conflitto sui dati, assegna i punti ai conflitti
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
  Public Overridable Function SemplificaNumeroTelefono(ByVal strNumero As String) As String
    Try
      If strNumero = "" Then Return strNumero 'compo non impostato, lo ritorno così com'è.
      'Semplifica il numero di telefono nel caso siamo presenti caratteri speciali al suo interno, come spazi, -, /, +39
      'Se alla fine non rimangono solo numeri allora non è semplificabile (alcuni scrivono altre informazioni nei numeri di telefono)
      ' e il programma restituisce quanto è stato passato inizialmente
      Dim strNumeroNew As String = strNumero.Replace(" ", "").Replace("-", "").Replace("/", "").Replace("\", "").Replace(".", "") _
                                            .Replace("(", "").Replace(")", "")
      'Rimuove la parte iniziale di +39, 0039 e 039
      If strNumeroNew.StartsWith("+39") Then strNumeroNew = strNumeroNew.Substring(3)
      If strNumeroNew.StartsWith("039") Then strNumeroNew = strNumeroNew.Substring(3)
      If strNumeroNew.StartsWith("0039") Then strNumeroNew = strNumeroNew.Substring(4)

      strNumeroNew = strNumeroNew.Replace("+", "")

      If strNumeroNew.Length < 7 Then Return strNumero ' Troppo corto, restituisco il numero così come è stato passato

      'Controlla che i caratteri rimasti siano tutti numeri
      For Each cCharacter As Char In strNumeroNew.ToCharArray()
        If Not IsNumeric(cCharacter) Then Return strNumero ' dopo la semplificazione non è un numero di telefono, restituisco il numero così come è stato passato
      Next

      strNumero = strNumeroNew
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return strNumero
  End Function

  Public Overridable Function ProponiContattoNuovaOrganizzazione(ByVal dtrOrganig As DataRow) As DataTable
    Dim dttContatti As DataTable = Nothing
    Dim dtrSelected As DataRow = Nothing
    Dim lMaxPoint As Integer = 0
    Try
      'Valuta se creare un nuovo contatto o se utilizzarne uno esistente.
      '1) Carica tutti i contatti a parità simile a parità di nome e cognome
      If Not oCldComm.CercaContattiPerNomeCognome(NTSCStr(dtrOrganig!og_descont), NTSCStr(dtrOrganig!og_descont2), dttContatti) Then Return Nothing

      'Se non ha trovato dati, esce. non si possono fare proposte
      If dttContatti.Rows.Count = 0 Then Return Nothing

      dttContatti.Columns.Add("xx_conflitti", GetType(Integer))
      dttContatti.Columns.Add("xx_coerenze", GetType(Integer))
      dttContatti.Columns.Add("xx_punteggio", GetType(Integer))

      '2) Scansiona la lista dei contatti per assegnargli un punteggio e valutare la probabilità che siano la stessa persona.
      For Each dtrContatto As DataRow In dttContatti.Rows
        Dim lCoerenze As Integer = 0
        Dim lConflitti As Integer = 0
        ControllaPunteggioCollegamento(dtrOrganig, dtrContatto, lCoerenze, lConflitti)

        If lCoerenze - lConflitti <= 0 Then
          dtrContatto.Delete()
        Else
          dtrContatto!xx_conflitti = lConflitti
          dtrContatto!xx_coerenze = lCoerenze
          dtrContatto!xx_punteggio = lCoerenze - lConflitti
        End If
      Next

      dttContatti.AcceptChanges()

      'Se non ha trovato dati, esce. non si possono fare proposte
      If dttContatti.Rows.Count = 0 Then Return Nothing

      'Rioridina per la colonna punteggio, dal punteggio più alto al più basso
      Dim dttContattiOrdered As DataTable = dttContatti.Clone
      Dim dtrContatti() As DataRow = dttContatti.Select("", "xx_punteggio DESC")
      For Each dtrContatto As DataRow In dtrContatti
        dttContattiOrdered.Rows.Add(dtrContatto.ItemArray)
      Next

      Return dttContattiOrdered
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

  Public Overridable Function RitornaOrganizzazioniCollegateAContatto(ByVal lContatto As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      'Ritorna tutte le organizzazioni collegate ad un contatto
      Return oCldComm.RitornaOrganizzazioniCollegateAContatto(lContatto, dttOut)
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

  Public Overridable Function CreaNuovoContattoDaOrganig(ByVal strDitta As String, ByVal lOrganig As Integer) As Boolean
    Dim dttTmp As DataTable = Nothing
    Try
      oCldComm.RitornaOrganizzazioneDaProgressivo(strDitta, lOrganig, dttTmp)

      If dttTmp.Rows.Count = 0 Then Return False

      oCldComm.CreaNuovoContatto(dttTmp.Rows(0))

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
  Public Overridable Function AggiornaContattoDaOrganig(ByVal strDitta As String, ByVal lOrganig As Integer) As Boolean
    Try
      Return oCldComm.AggiornaContattoDaOrganizzazione(strDitta, lOrganig)
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
  Public Overridable Function AggiornaContattiDaDatatable(ByVal strDittaCorrente As String, ByVal dttOrganig As DataTable) As Boolean
    Try
      'Aggiorna i contatti in base ai dati passati nel datatable
      For Each dtrOrga As DataRow In dttOrganig.Rows
        If NTSCInt(dtrOrga!og_contatto) = 0 Then
          dtrOrga!og_contatto = oCldComm.CreaNuovoContatto(dtrOrga)
        Else
          oCldComm.AggiornaContattoDaOrganizzazione(strDittaCorrente, NTSCInt(dtrOrga!og_progr))
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
#End Region

#Region "Trasferimento Configurazione Business E-mail da Organig a EmailConf"
  Public Overridable Function TrasferisciConfigurazionePosta() As Boolean
    Try
      '0) Verifica che non sia già stata fatta questa operazione
      If oCldComm.VerificaDatiTrasferiti() Then Return True

      '1) Cerca le configurazioni da trasferire
      Dim dttConfigurazioni As DataTable = oCldComm.RitornaConfigurazioniPostaOrganig()
      If dttConfigurazioni Is Nothing OrElse dttConfigurazioni.Rows.Count = 0 Then Return False

      '2) Scorre le configurazioni trovate
      For Each dtrConfig As DataRow In dttConfigurazioni.Rows
        If NTSCStr(dtrConfig!og_coperat).Trim = "" Then Continue For 'Ignoro le righe senza il nome utente, non saprei quali opzioni di registro guardare
        '2a) cerca i dati mancanti indicati nelle opzioni di registro
        If Not CompilaDatiDaOpzioniDiRegistro(dtrConfig) Then Return False

        '2b) Salva i dati sulla nuova tabella
        If Not oCldComm.AggiungiConfigurazioneEmailDaOrganig(dtrConfig) Then Return False
      Next

      '3) Svuota i campi di configurazione presenti su organig
      If Not oCldComm.SvuotaConfigurazionePostaSuOrganig() Then Return False

      '4) Cancella le opzioni di registro che non servono più
      If Not oCldComm.CancellaOpzioniRegistroConfigurazionePosta() Then Return False

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

  Public Overridable Function CompilaDatiDaOpzioniDiRegistro(ByVal dtrConfig As DataRow) As Boolean
    Dim strCurretUser As String = oApp.User.Nome
    Try
      oApp.User.Nome = NTSCStr(dtrConfig!og_coperat)
      If oCldComm.GetSettingBusDitt(NTSCStr(dtrConfig!codditt), "BSEMGMAI", "OPZIONIUT", ".", "AUTHSECURE", "0", ".", "0") = "0" Then
        dtrConfig!og_authsecure = "N"
      Else
        dtrConfig!og_authsecure = "S"
      End If
      dtrConfig!og_configu = oCldComm.GetSettingBusDitt(NTSCStr(dtrConfig!codditt), "BSXXWMAI", "OPZIONIUT", ".", "AUTH", "A", ".", "A")
      dtrConfig!og_exchuseru = oCldComm.GetSettingBusDitt(NTSCStr(dtrConfig!codditt), "BSXXWMAI", "OPZIONIUT", ".", "LOG", "", ".", "")
      dtrConfig!og_pwdmailu = oCldComm.GetSettingBusDitt(NTSCStr(dtrConfig!codditt), "BSXXWMAI", "OPZIONIUT", ".", "PWD", "", ".", "")
      dtrConfig.AcceptChanges()

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
      oApp.User.Nome = strCurretUser
    End Try
  End Function
#End Region

#Region "Creazione automatica attività per diario"
  Public Overridable Function CreazioneAutomaticaAttivita_NuovaOpportunita(ByVal strDitta As String, ByVal dtrOpportunita As DataRow) As Boolean
    Try
      'Cerca il tipo di attività di default per le nuove opportunità
      Dim lTaco As Integer = oCldComm.RitornaTipoAttivitaDefault(strDitta, "U")
      If lTaco = 0 Then Return False 'Se non è definito non creo la nuova opportunità

      'Crea il datatable di cract da creare
      Dim dttCract As DataTable = CreazioneAutomaticaAttivita_NuovaOpportunita_PreparaDati(strDitta, dtrOpportunita, lTaco)

      'Salva la nuova attività
      Return oCldComm.SalvaNuovaAttivita(dttCract)
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
  Public Overridable Function CreazioneAutomaticaAttivita_NuovaOpportunita_PreparaDati(ByVal strDitta As String, ByVal dtrOpportunita As DataRow, ByVal lTaco As Integer) As DataTable
    Dim dttCract As New DataTable
    Try
      oCldComm.GetTableStructure("CRACT", False, dttCract)
      oCldComm.SetTableDefaultValueFromDB("CRACT", dttCract.DataSet)
      dttCract.Rows.Add()

      With dttCract.Rows(0)
        !codditt = strDitta
        '!ca_codcrac = Viene inserito durante il salvataggio dell'attività in transazione.
        !ca_codlead = dtrOpportunita!op_codlead
        !ca_codoppo = dtrOpportunita!op_codoppo
        !ca_oggetto = dtrOpportunita!op_oggetto 'Fino a 255 caratteri
        !ca_codtaco = lTaco
        !ca_status = "E"
        !ca_note = " "
        !ca_ultagg = Now
        !ca_opnome = oApp.User.Nome
        !ca_dataes = Now.ToShortDateString
        !ca_oraes = ConvOra60Ora100(NTSCDec(Now.Hour & "," & Now.Minute), oApp.NDecSuTempiDiba)
        !ca_opnomeinc = oApp.User.Nome
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
    Return dttCract
  End Function

  Public Overridable Function CreazioneAutomaticaAttivita_CambioOrganizzazione(ByVal strDitta As String, ByVal dttOrganizzazioneNew As DataTable, _
                                                                               ByVal dttOrganizzazioneOld As DataTable) As Boolean
    Try
      'Cerca il tipo di attività di default per le nuove opportunità
      Dim lTaco As Integer = oCldComm.RitornaTipoAttivitaDefault(strDitta, "C")
      If lTaco = 0 Then Return False 'Se non è definito non creo la nuova opportunità
      'Duplico i datatable per evitare di sovrascrivere quelli che mi sono stati passati
      dttOrganizzazioneOld = dttOrganizzazioneOld.Copy
      dttOrganizzazioneNew = dttOrganizzazioneNew.Copy
      dttOrganizzazioneOld.AcceptChanges()
      dttOrganizzazioneNew.AcceptChanges()
      'Per le organizzazioni deve lavorare sempre con tutte le righe visto che nei vari programmi cancella sempre tutto e reinserisce.
      'il confronto viene fatto tra le righe vecchie e quelle nuove
      For Each dtrOrganizzazioneNew As DataRow In dttOrganizzazioneNew.Rows
        'Cerco la riga vecchia
        Dim dtrOrganizzazioneOld As DataRow
        Dim dtrTmp() As DataRow = dttOrganizzazioneOld.Select("og_progr = " & NTSCInt(dtrOrganizzazioneNew!og_progr))
        If dtrTmp.Length > 0 Then
          dtrOrganizzazioneOld = dtrTmp(0) 'Riga modificata
        Else
          dtrOrganizzazioneOld = Nothing 'Nuova Riga
        End If

        'Valuta se la riga dell'organizzazione ha iniziato a lavorare o a smesso di lavorare per una certa organizzazione
        Dim strOggetto As String = CreazioneAutomaticaAttivita_CambioOrganizzazione_ValutaOrganizzazioneCambiata(strDitta, dtrOrganizzazioneNew, dtrOrganizzazioneOld)
        If dtrOrganizzazioneOld IsNot Nothing Then
          dtrOrganizzazioneOld.Delete()
          dttOrganizzazioneOld.AcceptChanges()
        End If
        If strOggetto = "" Then Continue For

        'Crea il datatable di cract da creare
        Dim dttCract As DataTable = CreazioneAutomaticaAttivita_CambioOrganizzazione_PreparaDati(strDitta, dtrOrganizzazioneNew, lTaco, strOggetto)

        'Salva la nuova attività
        oCldComm.SalvaNuovaAttivita(dttCract)
      Next

      'Le righe rimaste in dttOrganizzazioneOld sono quelle cancellate
      For Each dtrOrganizzazioneOld As DataRow In dttOrganizzazioneOld.Rows
        If NTSCInt(dtrOrganizzazioneOld!og_progr) = 0 Then Continue For
        'Valuta se la riga dell'organizzazione ha iniziato a lavorare o a smesso di lavorare per una certa organizzazione
        Dim strOggetto As String = CreazioneAutomaticaAttivita_CambioOrganizzazione_ValutaOrganizzazioneCambiata(strDitta, Nothing, dtrOrganizzazioneOld)
        If strOggetto = "" Then Continue For

        'Crea il datatable di cract da creare
        Dim dttCract As DataTable = CreazioneAutomaticaAttivita_CambioOrganizzazione_PreparaDati(strDitta, dtrOrganizzazioneOld, lTaco, strOggetto)

        'Salva la nuova attività
        oCldComm.SalvaNuovaAttivita(dttCract)
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
  Public Overridable Function CreazioneAutomaticaAttivita_CambioOrganizzazione_ValutaOrganizzazioneCambiata(ByVal strDitta As String, _
                                                              ByVal dtrOrganizzazioneNew As DataRow, ByVal dtrOrganizzazioneOld As DataRow) As String
    Dim lLead As Integer = 0
    Dim strDescr As String = ""
    Try
      'Se non è una organizzazione di un lead non registra l'attività
      If dtrOrganizzazioneOld Is Nothing Then  'Nuova Riga
        lLead = NTSCInt(dtrOrganizzazioneNew!og_codlead)
        If lLead <= 0 Then Return ""
        oCldComm.ValCodiceDb(lLead.ToString, strDitta, "LEADS", "N", strDescr)
        Return oApp.Tr(Me, 130481599296992328, "|" & NTSCStr(dtrOrganizzazioneNew!og_descont) & " " & NTSCStr(dtrOrganizzazioneNew!og_descont2) & "| ha iniziato a lavorare per |" & strDescr & "|")
      ElseIf dtrOrganizzazioneNew Is Nothing Then 'Riga Cancellata
        lLead = NTSCInt(dtrOrganizzazioneOld!og_codlead)
        If lLead <= 0 Then Return ""
        oCldComm.ValCodiceDb(lLead.ToString, strDitta, "LEADS", "N", strDescr)
        Return oApp.Tr(Me, 130481599383923047, "|" & NTSCStr(dtrOrganizzazioneOld!og_descont) & " " & NTSCStr(dtrOrganizzazioneOld!og_descont2) & "| ha lasciato |" & strDescr & "|")
      Else 'Riga modificata
        If NTSCStr(dtrOrganizzazioneNew!og_old) <> NTSCStr(dtrOrganizzazioneOld!og_old) Then
          'Cambiato il flag di obsoleto
          lLead = NTSCInt(dtrOrganizzazioneNew!og_codlead)
          If lLead <= 0 Then Return ""
          oCldComm.ValCodiceDb(lLead.ToString, strDitta, "LEADS", "N", strDescr)
          If NTSCStr(dtrOrganizzazioneNew!og_old) = "S" Then
            'Riga resa obsoleta
            Return oApp.Tr(Me, 130481754920335807, "|" & NTSCStr(dtrOrganizzazioneNew!og_descont) & " " & NTSCStr(dtrOrganizzazioneNew!og_descont2) & "| ha lasciato |" & strDescr & "|")
          Else
            'Riga tornata attiva
            Return oApp.Tr(Me, 130481602815369884, "|" & NTSCStr(dtrOrganizzazioneNew!og_descont) & " " & NTSCStr(dtrOrganizzazioneNew!og_descont2) & "| ha iniziato a lavorare per |" & strDescr & "|")
          End If
        ElseIf NTSCDate(dtrOrganizzazioneNew!og_dtfine) <> NTSCDate(dtrOrganizzazioneOld!og_dtfine) AndAlso NTSCDate(dtrOrganizzazioneNew!og_dtfine) < New Date(2099, 12, 31) Then
          'Cambiata la data di fine rapport
          lLead = NTSCInt(dtrOrganizzazioneNew!og_codlead)
          If lLead <= 0 Then Return ""
          oCldComm.ValCodiceDb(lLead.ToString, strDitta, "LEADS", "N", strDescr)
          If NTSCDate(dtrOrganizzazioneNew!og_dtfine) > Now Then
            Return oApp.Tr(Me, 130481793410247488, "|" & NTSCStr(dtrOrganizzazioneNew!og_descont) & " " & NTSCStr(dtrOrganizzazioneNew!og_descont2) & "| lascerà |" & strDescr & "| il | " & NTSCDate(dtrOrganizzazioneNew!og_dtfine) & "|")
          Else
            Return oApp.Tr(Me, 130481793429912093, "|" & NTSCStr(dtrOrganizzazioneNew!og_descont) & " " & NTSCStr(dtrOrganizzazioneNew!og_descont2) & "| ha lasciato |" & strDescr & "| il | " & NTSCDate(dtrOrganizzazioneNew!og_dtfine) & "|")
          End If
        ElseIf NTSCInt(dtrOrganizzazioneNew!og_codlead) = -1 AndAlso NTSCInt(dtrOrganizzazioneOld!og_codlead) <> -1 Then
          'Scollegamento dell'organizzazione dal lead
          lLead = NTSCInt(dtrOrganizzazioneOld!og_codlead)
          If lLead <= 0 Then Return ""
          oCldComm.ValCodiceDb(lLead.ToString, strDitta, "LEADS", "N", strDescr)
          Return oApp.Tr(Me, 130481755019755034, "|" & NTSCStr(dtrOrganizzazioneNew!og_descont) & " " & NTSCStr(dtrOrganizzazioneNew!og_descont2) & "| ha lasciato |" & strDescr & "|")
        ElseIf NTSCInt(dtrOrganizzazioneNew!og_codlead) <> -1 AndAlso NTSCInt(dtrOrganizzazioneOld!og_codlead) = -1 Then
          'Collegamento dell'organizzazione al lead
          lLead = NTSCInt(dtrOrganizzazioneNew!og_codlead)
          If lLead <= 0 Then Return ""
          oCldComm.ValCodiceDb(lLead.ToString, strDitta, "LEADS", "N", strDescr)
          Return oApp.Tr(Me, 130481602786581069, "|" & NTSCStr(dtrOrganizzazioneNew!og_descont) & " " & NTSCStr(dtrOrganizzazioneNew!og_descont2) & "| ha iniziato a lavorare per |" & strDescr & "|")
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
    Return ""
  End Function
  Public Overridable Function CreazioneAutomaticaAttivita_CambioOrganizzazione_PreparaDati(ByVal strDitta As String, ByVal dtrOrganizzazione As DataRow, _
                                                                                           ByVal lTaco As Integer, ByVal strOggetto As String) As DataTable
    Dim dttCract As New DataTable
    Try
      oCldComm.GetTableStructure("CRACT", False, dttCract)
      oCldComm.SetTableDefaultValueFromDB("CRACT", dttCract.DataSet)
      dttCract.Rows.Add()

      With dttCract.Rows(0)
        !codditt = strDitta
        '!ca_codcrac = Viene inserito durante il salvataggio dell'attività in transazione.
        !ca_codlead = dtrOrganizzazione!og_codlead
        !ca_organig = dtrOrganizzazione!og_progr
        !ca_oggetto = strOggetto 'Fino a 255 caratteri
        !ca_codtaco = lTaco
        !ca_status = "E"
        !ca_note = " "
        !ca_ultagg = Now
        !ca_opnome = oApp.User.Nome
        !ca_dataes = Now.ToShortDateString
        !ca_oraes = ConvOra60Ora100(NTSCDec(Now.Hour & "," & Now.Minute), oApp.NDecSuTempiDiba)
        !ca_opnomeinc = oApp.User.Nome
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
    Return dttCract
  End Function

  Public Overridable Function CreazioneAutomaticaAttivita_CambioStatusCommercialeLead(ByVal strDitta As String, ByVal dtrLead As DataRow) As Boolean
    Try
      'Valuta se la riga dell'organizzazione ha iniziato a lavorare o a smesso di lavorare per una certa organizzazione
      Dim strOggetto As String = CreazioneAutomaticaAttivita_CambioStatusCommercialeLead_ValutaStatusCambiato(strDitta, dtrLead)
      If strOggetto = "" Then Return False

      'Cerca il tipo di attività di default per le nuove opportunità
      Dim lTaco As Integer = oCldComm.RitornaTipoAttivitaDefault(strDitta, "S")
      If lTaco = 0 Then Return False 'Se non è definito non creo la nuova opportunità

      'Crea il datatable di cract da creare
      Dim dttCract As DataTable = CreazioneAutomaticaAttivita_CambioStatusCommercialeLead_PreparaDati(strDitta, dtrLead, lTaco, strOggetto)

      'Salva la nuova attività
      Return oCldComm.SalvaNuovaAttivita(dttCract)
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
  Public Overridable Function CreazioneAutomaticaAttivita_CambioStatusCommercialeLead_ValutaStatusCambiato(ByVal strDitta As String, ByVal dtrLead As DataRow) As String
    Dim strDescr As String = ""
    Try
      'Altrimenti sono cambiati i dati sui lead
      If dtrLead.RowState = DataRowState.Modified Then
        If NTSCInt(dtrLead!le_codstco) <> NTSCInt(dtrLead("le_codstco", DataRowVersion.Original)) Then
          'Status commerciale cambiato. Creo l'attività
          Dim lStatus As Integer = NTSCInt(dtrLead!le_codstco)
          If lStatus = 0 Then
            Return oApp.Tr(Me, 130481755087742050, "Lo status è stato rimosso")
          Else
            oCldComm.ValCodiceDb(lStatus.ToString, strDitta, "TABSTCO", "N", strDescr)
            Return oApp.Tr(Me, 130481755145472107, "Lo status è diventato '|" & strDescr & "|'")
          End If
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
    Return ""
  End Function
  Public Overridable Function CreazioneAutomaticaAttivita_CambioStatusCommercialeLead_PreparaDati(ByVal strDitta As String, ByVal dtrLead As DataRow, _
                                                                                      ByVal lTaco As Integer, ByVal strOggetto As String) As DataTable
    Dim dttCract As New DataTable
    Try
      oCldComm.GetTableStructure("CRACT", False, dttCract)
      oCldComm.SetTableDefaultValueFromDB("CRACT", dttCract.DataSet)
      dttCract.Rows.Add()

      With dttCract.Rows(0)
        !codditt = strDitta
        '!ca_codcrac = Viene inserito durante il salvataggio dell'attività in transazione.
        !ca_codlead = dtrLead!le_codlead
        !ca_organig = 0
        !ca_oggetto = strOggetto 'Fino a 255 caratteri
        !ca_codtaco = lTaco
        !ca_status = "E"
        !ca_note = " "
        !ca_ultagg = Now
        !ca_opnome = oApp.User.Nome
        !ca_dataes = Now.ToShortDateString
        !ca_oraes = ConvOra60Ora100(NTSCDec(Now.Hour & "," & Now.Minute), oApp.NDecSuTempiDiba)
        !ca_opnomeinc = oApp.User.Nome
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
    Return dttCract
  End Function

  Public Overridable Function CreazioneAutomaticaAttivita_CambioStatusCommercialeOrganizzazione(ByVal strDitta As String, ByVal dttOrganizzazioneNew As DataTable, _
                                                                                                ByVal dttOrganizzazioneOld As DataTable) As Boolean
    Try
      'Cerca il tipo di attività di default per le nuove opportunità
      Dim lTaco As Integer = oCldComm.RitornaTipoAttivitaDefault(strDitta, "S")
      If lTaco = 0 Then Return False 'Se non è definito non creo la nuova opportunità
      'Duplico i datatable per evitare di sovrascrivere quelli che mi sono stati passati
      dttOrganizzazioneOld = dttOrganizzazioneOld.Copy
      dttOrganizzazioneNew = dttOrganizzazioneNew.Copy
      dttOrganizzazioneOld.AcceptChanges()
      dttOrganizzazioneNew.AcceptChanges()
      'Per le organizzazioni deve lavorare sempre con tutte le righe visto che nei vari programmi cancella sempre tutto e reinserisce.
      'il confronto viene fatto tra le righe vecchie e quelle nuove
      For Each dtrOrganizzazioneNew As DataRow In dttOrganizzazioneNew.Rows
        'Cerco la riga vecchia
        Dim dtrOrganizzazioneOld As DataRow
        Dim dtrTmp() As DataRow = dttOrganizzazioneOld.Select("og_progr = " & NTSCInt(dtrOrganizzazioneNew!og_progr))
        If dtrTmp.Length = 0 Then Continue For 'Le righe nuove o cancellate non interessato
        dtrOrganizzazioneOld = dtrTmp(0) 'Riga modificata
        
        'Valuta se la riga dell'organizzazione ha iniziato a lavorare o a smesso di lavorare per una certa organizzazione
        Dim strOggetto As String = CreazioneAutomaticaAttivita_CambioStatusCommercialeOrganizzazione_ValutaStatusCambiato(strDitta, dtrOrganizzazioneNew, dtrOrganizzazioneOld)
        If strOggetto = "" Then Continue For

        'Crea il datatable di cract da creare
        Dim dttCract As DataTable = CreazioneAutomaticaAttivita_CambioStatusCommercialeOrganizzazione_PreparaDati(strDitta, dtrOrganizzazioneNew, lTaco, strOggetto)

        'Salva la nuova attività
        oCldComm.SalvaNuovaAttivita(dttCract)
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
  Public Overridable Function CreazioneAutomaticaAttivita_CambioStatusCommercialeOrganizzazione_ValutaStatusCambiato(ByVal strDitta As String, _
                                                                 ByVal dtrOrganizzazioneNew As DataRow, ByVal dtrOrganizzazioneOld As DataRow) As String
    Dim strDescr As String = ""
    Try
      'Altrimenti sono cambiati i dati sui lead
      If dtrOrganizzazioneNew IsNot Nothing AndAlso dtrOrganizzazioneOld IsNot Nothing Then
        If NTSCInt(dtrOrganizzazioneNew!og_codstco) <> NTSCInt(dtrOrganizzazioneOld!og_codstco) Then
          'Status commerciale cambiato. Creo l'attività
          Dim lStatus As Integer = NTSCInt(dtrOrganizzazioneNew!og_codstco)
          If lStatus = 0 Then
            Return oApp.Tr(Me, 130481615953828580, "Lo status di |" & NTSCStr(dtrOrganizzazioneNew!og_descont) & " " & NTSCStr(dtrOrganizzazioneNew!og_descont2) & "| è stato rimosso")
          Else
            oCldComm.ValCodiceDb(lStatus.ToString, strDitta, "TABSTCO", "N", strDescr)
            Return oApp.Tr(Me, 130481615934467654, "Lo status di |" & NTSCStr(dtrOrganizzazioneNew!og_descont) & " " & NTSCStr(dtrOrganizzazioneNew!og_descont2) & "| è diventato '|" & strDescr & "|'")
          End If
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
    Return ""
  End Function
  Public Overridable Function CreazioneAutomaticaAttivita_CambioStatusCommercialeOrganizzazione_PreparaDati(ByVal strDitta As String, ByVal dtrOrganizzazione As DataRow, _
                                                                                      ByVal lTaco As Integer, ByVal strOggetto As String) As DataTable
    Dim dttCract As New DataTable
    Try
      oCldComm.GetTableStructure("CRACT", False, dttCract)
      oCldComm.SetTableDefaultValueFromDB("CRACT", dttCract.DataSet)
      dttCract.Rows.Add()

      With dttCract.Rows(0)
        !codditt = strDitta
        '!ca_codcrac = Viene inserito durante il salvataggio dell'attività in transazione.
        !ca_codlead = dtrOrganizzazione!og_codlead
        !ca_organig = dtrOrganizzazione!og_progr
        !ca_oggetto = strOggetto 'Fino a 255 caratteri
        !ca_codtaco = lTaco
        !ca_status = "E"
        !ca_note = " "
        !ca_ultagg = Now
        !ca_opnome = oApp.User.Nome
        !ca_dataes = Now.ToShortDateString
        !ca_oraes = ConvOra60Ora100(NTSCDec(Now.Hour & "," & Now.Minute), oApp.NDecSuTempiDiba)
        !ca_opnomeinc = oApp.User.Nome
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
    Return dttCract
  End Function
#End Region

#Region "Generazione Lead Fittizio"
  Public Overridable Sub GeneraLeadFittizio()
    Try
      oCldComm.GeneraLeadFittizio()
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

#Region "Collegamento Guest e utenti CRM"
  Public Overridable Function CollegaGuestConUtentiCRM(ByVal strDitta As String, ByVal lLead As Integer, ByVal strGuest As String) As Boolean
    Try
      'Trova gli utenti CRM collegati al lead al quale è associato l'utente
      Dim dttAccLead As DataTable = oCldComm.TrovaAccessiLeadDaLead(strDitta, lLead)

      'Per ogni utente CRM trovato controlla se esiste già la relazione e se non esiste la crea
      For Each dtrAccLead As DataRow In dttAccLead.Rows
        'Solo se ho gli accessi posso essere agganciato al lead tramite B4P
        If NTSCStr(dtrAccLead!opcr_crmvis) = "N" Then Continue For

        oCldComm.CreaRelazione(NTSCStr(dtrAccLead!opcr_opnome), strGuest)
      Next

      ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 130543788255903814, "Operazione completata.")))

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

  Public Overridable Function CollegaUtenteCRMConLeadGuest(ByVal strUtenteCRM As String) As Boolean
    Try
      'Trova i lead con utente guest collegato all'organizzazione che sono collegati all'utente CRM
      Dim dttAccLead As DataTable = oCldComm.TrovaOperatoriGuestDaAccessiLead(strUtenteCRM)

      'Per ogni utente trovato controlla se esiste già la relazione e se non esiste la crea
      For Each dtrAccLead As DataRow In dttAccLead.Rows
        'Solo se ho gli accessi posso essere agganciato al lead tramite B4P
        If NTSCStr(dtrAccLead!opcr_crmvis) = "N" Then Continue For

        oCldComm.CreaRelazione(NTSCStr(dtrAccLead!og_coperat), strUtenteCRM)
      Next

      ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 130543799191793489, "Operazione completata.")))

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

#Region "Promozioni"
  Public Overridable Function CompatibilitaPromozioni(ByVal strDitta As String, ByVal lPromo As Integer, ByVal lTipoTessera As Integer, _
                                                      Optional ByVal dtrPromo As DataRow = Nothing) As Boolean
    Dim dttPromo, dttTmp As New DataTable
    Dim dtrRow() As DataRow
    Dim dtIniMax, dtFinMin As Date
    Dim dOrIniMax, dOrFinMin As Decimal
    Dim strMsg As String = ""
    Dim strDescr As String = ""
    Dim bOk As Boolean
    Dim i, j As Integer
    Dim dtrTmp As DataRow
    'Controlla se l'associazione promozione\tipo tessera è compatibile con quelle già presenti nello stesso tipo tessera
    Try
      'verifico che i dati passati siano corretti
      If strDitta.Trim = "" Or lPromo = 0 Or lTipoTessera = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129394731358279184, "Impossibile verificare la compatibilità delle promozioni sul tipo tessera")))
        Return True
      End If

      'Carica tutte le promozioni di quel tipo tessera, poi i controlli li facciamo in memoria
      If Not oCldComm.CaricaPromoDaTipoTessera(strDitta, lTipoTessera, 0, 0, False, Nothing, 0, dttPromo) Then Return False

      'STEP 1: Controlla se sulla tessera che ci siano delle promozioni dello stesso tipo
      If dtrPromo Is Nothing Then
        If Not oCldComm.ValCodiceDb(lPromo.ToString, strDitta, "TABREPR", "N", , dttTmp) Then Return False
        dtrTmp = dttTmp.Rows(0)
      Else
        dtrTmp = dtrPromo
      End If

      'Se sono promozioni di accomulo punti vanno sempre bene, anche se riguardano gli stessi articoli\periodi.
      If NTSCStr(dtrTmp!tb_tipopr) = "Q" OrElse NTSCStr(dtrTmp!tb_tipopr) = "V" Then Return True

      dtrRow = dttPromo.Select("tb_tipopr = " & CStrSQL(dtrTmp!tb_tipopr))
      If dtrRow.Length = 0 Then Return True

      'STEP 2: Controlla se le promozioni dello stesso tipo si accavallano come date\ora\giorni e reparto
      For i = 0 To dtrRow.Length - 1
        'La promozione non deve considerare se stessa
        If NTSCInt(dtrRow(i)!tb_codrepr) = NTSCInt(dtrTmp!tb_codrepr) Then Continue For

        If NTSCInt(dtrRow(i)!tb_codrere) <> NTSCInt(dtrTmp!tb_codrere) AndAlso NTSCInt(dtrRow(i)!tb_codrere) <> 0 AndAlso NTSCInt(dtrTmp!tb_codrere) <> 0 Then Continue For
        'Codice reparto uguale, passo al controllo delle date.
        'La logica è la seguente: confronta la data più alta di inizio con quella più bassa di fine, se sono in ordine cronologico si sormontano
        'ES1: A----B C----D la data massima di inizio è C, la minima di fine è B, B < C non è in ordine cronologico, va bene.
        'ES2: A----B
        '        C----D sono sempre B e C, ma a questo giro C < B, quindi sono in ordine cronologico, si sormontano.
        If NTSCDate(dtrRow(i)!tb_datini) > NTSCDate(dtrTmp!tb_datini) Then
          dtIniMax = NTSCDate(dtrRow(i)!tb_datini)
        Else
          dtIniMax = NTSCDate(dtrTmp!tb_datini)
        End If

        If NTSCDate(dtrRow(i)!tb_datfin) < NTSCDate(dtrTmp!tb_datfin) Then
          dtFinMin = NTSCDate(dtrRow(i)!tb_datfin)
        Else
          dtFinMin = NTSCDate(dtrTmp!tb_datfin)
        End If

        If dtIniMax > dtFinMin Then Continue For 'Le date non si sormontano

        'Le ore si sormontano, controllo gli orari
        If NTSCDec(dtrRow(i)!tb_orini) > NTSCDec(dtrTmp!tb_orini) Then
          dOrIniMax = NTSCDec(dtrRow(i)!tb_orini)
        Else
          dOrIniMax = NTSCDec(dtrTmp!tb_orini)
        End If

        If NTSCDec(dtrRow(i)!tb_orfin) < NTSCDec(dtrTmp!tb_orfin) Then
          dOrFinMin = NTSCDec(dtrRow(i)!tb_orfin)
        Else
          dOrFinMin = NTSCDec(dtrTmp!tb_orfin)
        End If

        If dOrIniMax > dOrFinMin Then Continue For 'Le ore non si sormontano

        'Bisogna controllare che le periodo in cui si sormontano le date\ore, i giorni della settimana siano abilitati negli stessi periodi
        'Ne basta 1 che sia attivato nello stesso periodo per poter dare il problema dell'accavallamento
        bOk = True
        For j = 0 To 6 '(7 giorni - 1)
          Select Case dtIniMax.AddDays(j).DayOfWeek
            Case DayOfWeek.Monday
              If NTSCStr(dtrRow(i)!tb_fllun) = "S" And NTSCStr(dtrTmp!tb_fllun) = "S" Then
                bOk = False
                Exit For
              End If
            Case DayOfWeek.Tuesday
              If NTSCStr(dtrRow(i)!tb_flmar) = "S" And NTSCStr(dtrTmp!tb_flmar) = "S" Then
                bOk = False
                Exit For
              End If
            Case DayOfWeek.Wednesday
              If NTSCStr(dtrRow(i)!tb_flmer) = "S" And NTSCStr(dtrTmp!tb_flmer) = "S" Then
                bOk = False
                Exit For
              End If
            Case DayOfWeek.Thursday
              If NTSCStr(dtrRow(i)!tb_flgio) = "S" And NTSCStr(dtrTmp!tb_flgio) = "S" Then
                bOk = False
                Exit For
              End If
            Case DayOfWeek.Friday
              If NTSCStr(dtrRow(i)!tb_flven) = "S" And NTSCStr(dtrTmp!tb_flven) = "S" Then
                bOk = False
                Exit For
              End If
            Case DayOfWeek.Saturday
              If NTSCStr(dtrRow(i)!tb_flsab) = "S" And NTSCStr(dtrTmp!tb_flsab) = "S" Then
                bOk = False
                Exit For
              End If
            Case DayOfWeek.Sunday
              If NTSCStr(dtrRow(i)!tb_fldom) = "S" And NTSCStr(dtrTmp!tb_fldom) = "S" Then
                bOk = False
                Exit For
              End If
          End Select

          If dtIniMax.AddDays(j) = dtFinMin Then Exit For
        Next

        If bOk Then Continue For 'i giorni nella settimana non si sormontano nello stesso periodo

        'STEP 3: Finale, al momento le 2 promozioni sono simili, rimane da controllare se vengono applicate agli stessi articoli
        'Se uno dei 2 ha la spunta su tutti gli articoli, per forza si sormontano
        If NTSCStr(dtrRow(i)!tb_fltutti) = "N" And NTSCStr(dtrTmp!tb_fltutti) = "N" Then
          If oCldComm.ControllaPromoArticoli(strDitta, dtrRow(i), dtrTmp) Then Continue For
        End If

        'Se sono arrivato fino a qui, devo dare il messaggio di errore, altrimenti sono entrato in un continue for.
        'Il messaggio non deve essere bloccante per le promozioni che assegnano dei punti (concesso, ma per sicurezza avvisa)
        strMsg &= "    " & NTSCInt(dtrRow(i)!tb_codrepr) & " - " & NTSCStr(dtrRow(i)!tb_desrepr) & vbCrLf
      Next

      'Nessun messaggio: è andato tutto bene e la promozione è compatibile con quelle presenti
      If strMsg = "" Then Return True

      oCldComm.ValCodiceDb(lTipoTessera.ToString, strDitta, "TABRETT", "N", strDescr)

      If NTSCStr(dtrTmp!tb_tipopr) = "V" Or NTSCStr(dtrTmp!tb_tipopr) = "Q" Then
        ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 129394881890397285, "La promozione '|" & NTSCStr(dtrTmp!tb_desrepr) & "|', " & _
                                                                                          "applicata al tipo tessera '|" & strDescr & _
                                                                                          "| potrebbe non essere compatibile con le promozioni:") & vbCrLf & strMsg))
      Else
        ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 129394764530648597, "La promozione '|" & NTSCStr(dtrTmp!tb_desrepr) & "|', " & _
                                                                                          "applicata al tipo tessera '|" & strDescr & _
                                                                                          "|' non è compatibile con le promozioni:") & vbCrLf & strMsg))
      End If

      'Non è compatibile
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
  Public strTipiPromozioneCumulabili As String
  Public Overridable Function CompatibilitaPromozioniPerArticolo(ByVal strDitta As String, ByVal dtrPromo1 As DataRow, ByVal dtrPromo2 As DataRow, ByVal strCodart As String) As Boolean
    'Controlla se 2 promozioni sono compatibili. Il controllo va fatto in fase di lettura articolo, quindi deve essere veloce!!!!
    Try
      If NTSCStr(dtrPromo1!tb_codrepr) = NTSCStr(dtrPromo2!tb_codrepr) Then Return True 'è la stessa promozione

      'Le promozioni scelte come cumulabili non sono mai incompatibili.
      If strTipiPromozioneCumulabili = "" Then
        strTipiPromozioneCumulabili = oCldComm.GetSettingBus("BSREPROM", "OPZIONI", ".", "TipiPromozioneCumulabili", " ", ".", " ")
        strTipiPromozioneCumulabili = strTipiPromozioneCumulabili.Replace("M", "").Replace("N", "").Replace("L", "") 'Le promozioni NxM e applica listino non sono mai comulabili
        strTipiPromozioneCumulabili &= "DEOXYZ" 'Le promozioni differite sono sempre comulabili 
      End If

      If strTipiPromozioneCumulabili.Contains(NTSCStr(dtrPromo1!tb_tipopr)) Then Return True

      'STEP 1: Controlla se sulla tessera che ci siano delle promozioni dello stesso tipo
      If NTSCStr(dtrPromo1!tb_tipopr) <> NTSCStr(dtrPromo2!tb_tipopr) Then Return True 'Sono 2 promozioni diverse, non servono altri controlli

      'STEP 2: Controlla se entrambe hanno quell'articolo
      If NTSCStr(dtrPromo1!tb_fltutti) = "N" And NTSCStr(dtrPromo2!tb_fltutti) = "N" Then
        If oCldComm.ControllaPromoSingoloArticolo(strDitta, dtrPromo1, dtrPromo2, strCodart) Then Return True
      End If


      'Non è compatibile
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

  Public Overridable Function CaricaContrattiCliente(ByVal strDitta As String, ByVal lConto As Integer, ByVal lCoddest As Integer, ByVal dtDatPromo As Date, ByVal dOraPromo As Decimal) As DataTable
    Try
      Return oCldComm.CaricaContrattiCliente(strDitta, lConto, lCoddest, dtDatPromo, dOraPromo)
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
  Public Overridable Function CaricaContrattiAgente(ByVal strDitta As String, ByVal lAgente As Integer, ByVal dtDatPromo As Date, ByVal dOraPromo As Decimal) As DataTable
    Try
      Return oCldComm.CaricaContrattiAgente(strDitta, lAgente, dtDatPromo, dOraPromo)
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
  Public Overridable Function CaricaPromozioni(ByVal strDitta As String, ByVal strProgChiamante As String, ByVal lConto As Integer, ByVal lCoddest As Integer, _
                                               ByVal dtDataPromo As Date, ByVal dOraPromo As Decimal, ByVal lTessere() As Integer, _
                                               ByVal lCodrere As Integer, ByVal lCodstab As Integer, ByVal lCausalePromoPiede As Integer, _
                                               ByRef dttPromo As DataTable, ByRef dsPromoOmaggi As DataSet) As Boolean
    Dim dttPromoClie As DataTable = Nothing
    Dim dttTmp As DataTable = Nothing
    Try
      dttPromo = Nothing 'Svuota la lista delle promozioni

      'A partire dalla tessera che è stata associata al conto carica le promozioni da applicare nel documento.
      'Se non viene passata la lista delle tessere\contratti allora carica il tipo no card.
      If lTessere Is Nothing OrElse lTessere.Length = 0 Then
        If Not oCldComm.CaricaTipoTesseraDaTessera(strDitta, 0, dttTmp) Then Return False

        'Carica tutte le promozioni di quel tipo tessera
        If dttTmp.Rows.Count > 0 Then
          If Not oCldComm.CaricaPromoDaTipoTessera(strDitta, NTSCInt(dttTmp.Rows(0)!tb_codrett), lCodrere, lCodstab, _
                                                   True, dtDataPromo, dOraPromo, dttPromo) Then Return False
        End If
      Else
        For Each lTessera As Integer In lTessere
          If Not oCldComm.CaricaTipoTesseraDaTessera(strDitta, lTessera, dttTmp) Then Return False

          'Carica tutte le promozioni di quel tipo tessera
          If dttTmp.Rows.Count > 0 Then
            If Not oCldComm.CaricaPromoDaTipoTessera(strDitta, NTSCInt(dttTmp.Rows(0)!tb_codrett), lCodrere, lCodstab, _
                                                     True, dtDataPromo, dOraPromo, dttTmp) Then Return False

            If dttTmp.Rows.Count > 0 Then
              'Indico su ogni promozione qual'è la tessera che ha generato la promozione
              For Each dtrRow As DataRow In dttTmp.Rows
                dtrRow!xx_contratto = lTessera
              Next

              If dttPromo Is Nothing Then
                'Al primo giro copio la struttura
                dttPromo = dttTmp.Copy
              Else
                'Riverso i dati trovati nel datatable con le promozioni
                For Each dtrTmp As DataRow In dttTmp.Rows
                  'se la promozione esiste già non la riversa nuovamente.
                  If dttPromo.Select("tb_codrepr = " & NTSCInt(dtrTmp!tb_codrepr)).Length = 0 Then dttPromo.Rows.Add(dtrTmp.ItemArray)
                Next
              End If
            End If
          End If
        Next
      End If
      'Non ho trovato promozioni finora, creo la struttura vuota
      If dttPromo Is Nothing Then
        'Così carica il datatable vuoto, ma con tutti i campi che servono
        oCldComm.CaricaPromoDaTipoTessera(strDitta, -1, -1, -1, True, New Date(1900, 1, 1), 0, dttPromo)
      End If


      'Ora le promozioni attive per il conto specifico (promozioni attivate da premi ottenuti con i punti)
      If strProgChiamante = "BEREGSRE" Then
        If Not oCldComm.CaricaPromoSpecificheCliente(strDitta, lConto, lCodrere, dtDataPromo, dOraPromo, dttPromoClie) Then Return False

        'travaso i dati tra i 2 datatable (non si può usare ItemArray o i vari ImportRow, ecc... visto che i 2 datatable sono diversi)
        For z As Integer = 0 To dttPromoClie.Rows.Count - 1
          'Controlla che la promozione non sia già presente, in caso segnala il problema.
          If dttPromo.Select("tb_codrepr = " & NTSCInt(dttPromoClie.Rows(z)!tb_codrepr)).Length > 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129478413142318409, "La promozione |" & NTSCInt(dttPromoClie.Rows(z)!tb_codrepr) & _
                             "| - '|" & NTSCStr(dttPromoClie.Rows(z)!tb_desrepr) & "|' derivata da un premio non sarà valutata, in quanto è già presente" & _
                             " tra le promozione attive")))
            Continue For
          End If

          'Controlla al volo che la promozione sia compatibile con quelle presenti.
          If lTessere.Length > 0 Then
            If Not CompatibilitaPromozioni(strDitta, NTSCInt(dttPromoClie.Rows(z)!tb_codrepr), lTessere(0), dttPromoClie.Rows(z)) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129478419132030578, "La promozione |" & NTSCInt(dttPromoClie.Rows(z)!tb_codrepr) & _
                               "| - |" & NTSCStr(dttPromoClie.Rows(z)!tb_desrepr) & "| derivata da un premio non sarà valutata, in quanto non è compatibile con le altre promozioni presenti sulla tessera")))
              Continue For
            End If
          End If

          dttPromo.Rows.Add()
          For Each oColumn As DataColumn In dttPromo.Columns
            dttPromo.Rows(dttPromo.Rows.Count - 1)(oColumn.ColumnName) = dttPromoClie.Rows(z)(oColumn.ColumnName)
          Next
        Next
      End If

      'Ogni volta che ricarico le promozioni, azzero il conteggio di quante volte sono state usate per articolo Omaggio
      dsPromoOmaggi = New DataSet
      'In caso deve anche riempire le colonne di quante volte è stata utilizzata una certa promozione
      For z As Integer = dttPromo.Rows.Count - 1 To 0 Step -1 ' Ci sono delle delete di righe, darebbe problemi se si andasse in ordine crescente
        'Controllo prima per cliente, la query filtra sul conto (che è indicizzato) e dovrebbe essere più veloce
        If NTSCInt(dttPromo.Rows(z)!tb_nprclie) > 0 Then
          'Non usare dttEt.rows(0)!et_conto, questo metodo viene chiamato dalla beforeColUpdate del conto e quindi prenderebbe quello vecchio
          oCldComm.TrovaNumeroUsiPromo(dttPromo.Rows(z), dsPromoOmaggi, lConto)

          'Per gli omaggi non viene usato xx_nprclie, ma il dataset dsPromoOmaggi
          If NTSCStr(dttPromo.Rows(z)!tb_tipopr) <> "F" And NTSCStr(dttPromo.Rows(z)!tb_tipopr) <> "A" Then
            'Controllo subito se la promozione va annullata oppure no, così evito in caso di fare la query successiva
            If NTSCInt(dttPromo.Rows(z)!tb_nprclie) <= NTSCInt(dttPromo.Rows(z)!xx_nprclie) Then
              dttPromo.Rows(z).Delete()
              Continue For
            End If
          Else
            If Not CaricaPromozioni_ControlloMassimoPerOmaggio(strDitta, dttPromo.Rows(z), "C", dsPromoOmaggi) Then Continue For
          End If
        End If
        'Controllo per documento
        If NTSCInt(dttPromo.Rows(z)!tb_npromo) > 0 Then
          oCldComm.TrovaNumeroUsiPromo(dttPromo.Rows(z), dsPromoOmaggi, 0)

          'Per gli omaggi non viene usato xx_nprclie, ma il dataset dsPromoOmaggi
          If NTSCStr(dttPromo.Rows(z)!tb_tipopr) <> "F" And NTSCStr(dttPromo.Rows(z)!tb_tipopr) <> "A" Then
            'Controllo subito se la promozione va annullata oppure no, così evito in caso di fare la query successiva
            If NTSCInt(dttPromo.Rows(z)!tb_npromo) <= NTSCInt(dttPromo.Rows(z)!xx_npromo) Then
              dttPromo.Rows(z).Delete()
              Continue For
            End If
          Else
            If Not CaricaPromozioni_ControlloMassimoPerOmaggio(strDitta, dttPromo.Rows(z), "P", dsPromoOmaggi) Then Continue For
          End If
        End If
      Next

      dttPromo.AcceptChanges()

      If lCausalePromoPiede > 0 Then
        'Il datatable ha le colonne (non è completamente vuoto)
        If dttPromo.Columns.Count > 2 Then
          Dim dtrRow() As DataRow = dttPromo.Select("tb_tipopr IN ('T','U')")

          'Se ci sono delle promozioni di sconto di piede, verifica che sia configurata la causale nella promozione retail
          If dtrRow.Length > 0 Then
            If lCausalePromoPiede = 0 Then
              Dim strMsg As String = ""

              strMsg = oApp.Tr(Me, 129751617669956070, "Le promozioni: " & vbCrLf)

              For Each dtrTmp As DataRow In dtrRow
                strMsg &= NTSCStr(dtrTmp!tb_desrepr) & vbCrLf
              Next

              strMsg &= oApp.Tr(Me, 129751618312871325, "Non saranno applicate in quanto non è stata indicata la causale di magazzino per le promozioni di piede nella Personalizzazione GPV")

              ThrowRemoteEvent(New NTSEventArgs("", strMsg))
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
  Public Overridable Function CaricaPromozioniAgente(ByVal strDitta As String, ByVal strProgChiamante As String, ByVal lNAgente As Integer, ByVal lAgente As Integer, _
                                                     ByVal dtDataPromo As Date, ByVal dOraPromo As Decimal, ByVal lTessere() As Integer, _
                                                     ByVal lCodrere As Integer, ByVal lCodstab As Integer, ByRef dttPromo As DataTable) As Boolean
    Dim dttTmp As DataTable = Nothing
    Try
      'A partire dalla tessera che è stata associata al conto carica le promozioni da applicare nel documento.
      'Se non viene passata la lista delle tessere\contratti allora carica il tipo no card.
      If lTessere IsNot Nothing AndAlso lTessere.Length > 0 Then
        For Each lTessera As Integer In lTessere
          If Not oCldComm.CaricaTipoTesseraDaTessera(strDitta, lTessera, dttTmp) Then Return False

          'Carica tutte le promozioni di quel tipo tessera
          If dttTmp.Rows.Count > 0 Then
            If Not oCldComm.CaricaPromoDaTipoTessera(strDitta, NTSCInt(dttTmp.Rows(0)!tb_codrett), lCodrere, lCodstab, _
                                                     True, dtDataPromo, dOraPromo, dttTmp) Then Return False

            If dttTmp.Rows.Count > 0 Then
              'Indico su ogni promozione qual'è la tessera che ha generato la promozione
              For Each dtrRow As DataRow In dttTmp.Rows
                dtrRow!xx_contratto = lTessera
              Next

              If dttPromo Is Nothing Then
                'Al primo giro copio la struttura
                dttPromo = dttTmp.Copy
              Else
                'Riverso i dati trovati nel datatable con le promozioni
                For Each dtrTmp As DataRow In dttTmp.Rows
                  'se la promozione esiste già non la riversa nuovamente.
                  If dttPromo.Select("tb_codrepr = " & NTSCInt(dtrTmp!tb_codrepr) & " AND xx_agente = " & lAgente & " AND xx_nagente = " & lNAgente).Length = 0 Then
                    dttPromo.Rows.Add(dtrTmp.ItemArray)
                    dttPromo.Rows(dttPromo.Rows.Count - 1)!xx_agente = lAgente
                    dttPromo.Rows(dttPromo.Rows.Count - 1)!xx_nagente = lNAgente 'Se è l'agente 1 o l'agente 2.
                  End If
                Next
              End If
            End If
          End If
        Next
      End If

      dttPromo.AcceptChanges()

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
  Public Overridable Function CaricaPromozioni_ControlloMassimoPerOmaggio(ByVal strDitta As String, ByVal dtrPromo As DataRow, ByVal strTipo As String, ByVal dsPromoOmaggi As DataSet) As Boolean
    Dim dttTmp As New DataTable
    Dim x, i As Integer
    Try
      If NTSCStr(dtrPromo!tb_artomg).Trim = "" Then
        If Not oCldComm.CaricaDatiOmaggioDaLista(strDitta, NTSCInt(dtrPromo!tb_lsaromg), dttTmp) Then Return False

        'Da tutte le righe della lista selezionata controllo quali sono state movimentate (distribuite con gli omaggi)
        For x = 0 To dttTmp.Rows.Count - 1
          Dim bTrovato As Boolean = False
          With dsPromoOmaggi.Tables(strTipo & NTSCInt(dtrPromo!tb_codrepr))
            'Per ogni articolo della lista selezionata aggiorno con il residuo della disponibilità dell'articolo per quella promozione
            For i = 0 To .Rows.Count - 1
              If NTSCStr(dttTmp.Rows(x)!ar_codart) = NTSCStr(.Rows(i)!mm_codart) Then
                bTrovato = True
                .Rows(i)!QUANT = NTSCDec(dttTmp.Rows(x)!xx_quant) * NTSCDec(IIf(strTipo = "C", dtrPromo!tb_nprclie, dtrPromo!tb_npromo)) - NTSCDec(.Rows(i)!QUANT)
                If NTSCDec(.Rows(i)!QUANT) < 0 Then .Rows(i)!QUANT = 0
              End If
            Next
            'Non ho trovato nessun movimento (inserisco la riga con il massimo della disponibilità)
            If Not bTrovato Then
              .Rows.Add()
              .Rows(.Rows.Count - 1)!mm_codart = NTSCStr(dttTmp.Rows(x)!ar_codart)
              .Rows(.Rows.Count - 1)!QUANT = NTSCDec(dttTmp.Rows(x)!xx_quant) * NTSCDec(IIf(strTipo = "C", dtrPromo!tb_nprclie, dtrPromo!tb_npromo))
            End If
          End With
        Next
      Else
        With dsPromoOmaggi.Tables(strTipo & NTSCInt(dtrPromo!tb_codrepr))
          'Se è a 0 vuol dire che la riga non è stata ancora movimentanta, quindi crea la nuova riga con la quantità massima disponibile
          If .Rows.Count = 0 Then
            .Rows.Add()
            .Rows(0)!mm_codart = dtrPromo!tb_artomg
            .Rows(0)!QUANT = NTSCDec(dtrPromo!tb_artqta) * NTSCDec(IIf(strTipo = "C", dtrPromo!tb_nprclie, dtrPromo!tb_npromo))
          Else
            .Rows(0)!QUANT = NTSCDec(dtrPromo!tb_artqta) * NTSCDec(IIf(strTipo = "C", dtrPromo!tb_nprclie, dtrPromo!tb_npromo)) - NTSCDec(.Rows(0)!QUANT)

            'Se la quantità è 0 allora ho sforato il limite massimo di distribuzione della promozione
            'Quindi la posso eliminare e non condiderare più
            If NTSCDec(.Rows(0)!QUANT) <= 0 Then
              dtrPromo.Delete()
              Return False
            End If
          End If
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


  Public Overridable Function AnnullaPromozioni(ByVal oPromozioni As NTSPromozioni) As Boolean
    Try
      'Non ci sono promozioni da ricalcolare
      If oPromozioni.dttPromo Is Nothing Then Return True

      If Not oPromozioni.dttEc.Columns.Contains("xxo_exomaggiomxn") Then oPromozioni.dttEc.Columns.Add("xxo_exomaggiomxn", GetType(String))

      Dim strFiltroEvasione As String = CalcolaPromozioni_FiltroEvasione_ConsideraDaOfferte(oPromozioni)

      For Each dtrEc As DataRow In oPromozioni.dttEc.Select("(ec_tiporiga = 'S' " & _
                                      NTSCStr(IIf(oPromozioni.Parametri.bDaGPV, "", " OR ec_tiporiga = 'O'")) & ")" & strFiltroEvasione)
        With dtrEc
          'Annullo le righe di Sconto di Piede a % e Sconto di piede a valore
          If oPromozioni.Parametri.bDaGPV Then ThrowRemoteEvent(New NTSEventArgs("bNoSalvaRiga", "-1"))
          .Delete()
          If oPromozioni.Parametri.bDaGPV Then ThrowRemoteEvent(New NTSEventArgs("bNoSalvaRiga", "0"))
        End With
      Next
      oPromozioni.dttEc.AcceptChanges()

      Dim strSelect As String = "ec_tiporiga <> 'O'"
      If oPromozioni.Parametri.bDaGPV Then
        strSelect &= " AND xxo_gift = 'N'"
        oPromozioni.dttEt.Rows(0)!xx_totpunti = 0
      End If

      Dim bContieneColonnaEvasioneOff As Boolean = oPromozioni.dttEc.Columns.Contains("ec_oqanno")

      'Annulla tutte le promozioni per ricalcolarle
      For Each dtrRow As DataRow In oPromozioni.dttEc.Select(strSelect & strFiltroEvasione)
        With dtrRow
          'è una riga d'ordine che evade una offerta, devo annullare solo le promozioni per agente!
          If bContieneColonnaEvasioneOff AndAlso NTSCInt(dtrRow!ec_oqanno) <> 0 Then
            'Se una qualsiasi promozione applicata va a ricalcolare le provvigioni allora devo ricalcolarle! 
            ' (le ricalcola sempre per entrambi gli agenti)
            If CalcolaPromozioni_PromozioneImmediataPerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr1out)) OrElse _
               CalcolaPromozioni_PromozioneImmediataPerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr2out)) OrElse _
               CalcolaPromozioni_PromozioneImmediataPerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr3out)) OrElse _
               CalcolaPromozioni_PromozioneImmediataPerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr4out)) OrElse _
               CalcolaPromozioni_PromozioneImmediataPerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr5out)) OrElse _
               CalcolaPromozioni_PromozioneImmediataPerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr6out)) Then
              'Cerco il progressivo della riga corrente nel datatable
              For k As Integer = 0 To oPromozioni.dttEc.Rows.Count - 1
                If oPromozioni.dttEc.Rows(k) Is dtrRow Then
                  ThrowRemoteEvent(New NTSEventArgs("SettaProvvigioni", k.ToString))
                  Exit For
                End If
              Next
            End If

            'Se è una promozione per agente allora va annullata!
            If CalcolaPromozioni_PromozionePerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr1)) Then !ec_codrepr1 = 0
            If CalcolaPromozioni_PromozionePerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr2)) Then !ec_codrepr2 = 0
            If CalcolaPromozioni_PromozionePerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr3)) Then !ec_codrepr3 = 0
            If CalcolaPromozioni_PromozionePerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr4)) Then !ec_codrepr4 = 0
            If CalcolaPromozioni_PromozionePerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr5)) Then !ec_codrepr5 = 0
            If CalcolaPromozioni_PromozionePerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr6)) Then !ec_codrepr6 = 0
            If CalcolaPromozioni_PromozionePerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr1out)) Then !ec_codrepr1out = 0
            If CalcolaPromozioni_PromozionePerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr2out)) Then !ec_codrepr2out = 0
            If CalcolaPromozioni_PromozionePerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr3out)) Then !ec_codrepr3out = 0
            If CalcolaPromozioni_PromozionePerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr4out)) Then !ec_codrepr4out = 0
            If CalcolaPromozioni_PromozionePerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr5out)) Then !ec_codrepr5out = 0
            If CalcolaPromozioni_PromozionePerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr6out)) Then !ec_codrepr6out = 0

            !ec_prov1percdiff = 0
            !ec_prov2percdiff = 0
            If oPromozioni.dttEc.Columns.Contains("ec_prov1valdiff") Then
              'Colonne presenti solo in gestione documenti
              !ec_prov1valdiff = 0
              !ec_prov2valdiff = 0
            End If
          Else
            'è una riga generata da una promozione (altrimenti le righe con stampa riga diverso da S non possono avere promozioni)
            'Guarda solo la prima promozione, perchè le altre se ci sono, vuol dire che c'è anche la prima.
            If NTSCStr(!ec_tiporiga) = "M" Then
              If NTSCStr(!ec_stasino) = "S" Then 'Caso MxN con omaggio a pagamento, ricalcolo il prezzo e gli sconti della riga omaggio
                'Cerco il progressivo della riga corrente nel datatable
                For k As Integer = 0 To oPromozioni.dttEc.Rows.Count - 1
                  If oPromozioni.dttEc.Rows(k) Is dtrRow Then
                    ThrowRemoteEvent(New NTSEventArgs("SettaPrezzoSconti", k.ToString))
                    Exit For
                  End If
                Next
              End If
              !xxo_exomaggiomxn = "S"
            Else
              !xxo_exomaggiomxn = "N"
            End If

            If NTSCStr(!ec_tiporiga) <> "N" Then !ec_stasino = "S"

            'Se una qualsiasi promozione applicata va a ricalcolare le provvigioni allora devo ricalcolarle! 
            ' (le ricalcola sempre per entrambi gli agenti)
            If CalcolaPromozioni_PromozioneImmediataPerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr1out)) OrElse _
               CalcolaPromozioni_PromozioneImmediataPerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr2out)) OrElse _
               CalcolaPromozioni_PromozioneImmediataPerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr3out)) OrElse _
               CalcolaPromozioni_PromozioneImmediataPerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr4out)) OrElse _
               CalcolaPromozioni_PromozioneImmediataPerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr5out)) OrElse _
               CalcolaPromozioni_PromozioneImmediataPerAgente(oPromozioni.Parametri.strDitta, NTSCInt(!ec_codrepr6out)) Then
              'Cerco il progressivo della riga corrente nel datatable
              For k As Integer = 0 To oPromozioni.dttEc.Rows.Count - 1
                If oPromozioni.dttEc.Rows(k) Is dtrRow Then
                  ThrowRemoteEvent(New NTSEventArgs("SettaProvvigioni", k.ToString))
                  Exit For
                End If
              Next
            End If

            'Azzero le promozioni di riga
            !ec_codrepr1 = 0
            !ec_codrepr2 = 0
            !ec_codrepr3 = 0
            !ec_codrepr4 = 0
            !ec_codrepr5 = 0
            !ec_codrepr6 = 0
            !ec_codrepr1out = 0
            !ec_codrepr2out = 0
            !ec_codrepr3out = 0
            !ec_codrepr4out = 0
            !ec_codrepr5out = 0
            !ec_codrepr6out = 0
            If NTSCStr(!ec_tiporiga) = "M" AndAlso oPromozioni.Parametri.lCodivaOmaggi = NTSCInt(!ec_codiva) Then
              'Setta il codice IVA se non esiste quello di esenzione in testata
              If NTSCInt(oPromozioni.dttEt.Rows(0)!et_codese) = 0 Then
                'Se è un buono determina il codice iva originario
                Dim dttArti As New DataTable
                oCldComm.ValCodiceDb(NTSCStr(!ec_codart), NTSCStr(!codditt), "ARTICO", "S", , dttArti)
                !ec_codiva = dttArti.Rows(0)!ar_codiva
              Else
                !ec_codiva = oPromozioni.dttEt.Rows(0)!et_codese
              End If
          End If
          If NTSCStr(!ec_tiporiga) <> "N" Then !ec_tiporiga = "N"
          If NTSCInt(!xxo_listpromo) <> 0 Then !xxo_listpromo = 0
          If NTSCDec(!ec_scontp) <> 0 Then !ec_scontp = 0
          !ec_scpercdiff = 0
          If oPromozioni.dttEc.Columns.Contains("ec_scvaldiff") Then !ec_scvaldiff = 0 'Colonna presente solo in gestione documenti
          If Not oPromozioni.Parametri.bDaOfferta Then
            !ec_prov1percdiff = 0
            !ec_prov2percdiff = 0
            If oPromozioni.dttEc.Columns.Contains("ec_prov1valdiff") Then
              'Colonne presenti solo in gestione documenti
              !ec_prov1valdiff = 0
              !ec_prov2valdiff = 0
            End If
          End If
          End If
        End With
      Next

      CalcolaPromozioni_ColoreGrigliaPromozioni(oPromozioni)

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


  Public Overridable Function CalcolaPromozioni(ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim strSelect As String = ""
    Dim lPuntiTot As Integer = 0
    Dim dScontoTotalePromo(6) As Decimal
    Try
      'Si passa tutte le promozioni applicabili che ha trovato
      For Each dtrPromo As DataRow In oPromozioni.dttPromo.Rows
        With dtrPromo
          '"F" - maggio raggiunto valore "A" - Omaggio acquistando certi articoli (questi 2 vanno calcolati alla fine)
          If NTSCStr(!tb_tipopr) = "F" OrElse NTSCStr(!tb_tipopr) = "A" Then Continue For

          'Controlla che sia stato raggiunto il valore minimo per assegnare la promozione o che non sia una promozione differita
          If NTSCInt(!tb_valmin) > 0 AndAlso NTSCStr(!tb_tipopr) <> "D" AndAlso NTSCStr(!tb_tipopr) <> "E" AndAlso NTSCStr(!tb_tipopr) <> "O" _
                                     AndAlso NTSCStr(!tb_tipopr) <> "X" AndAlso NTSCStr(!tb_tipopr) <> "Y" AndAlso NTSCStr(!tb_tipopr) <> "Z" Then
            ThrowRemoteEvent(New NTSEventArgs("CalcolaTotali", ""))

            If NTSCDec(!tb_valmin) > CalcolaPromozioni_PromoTotaleDoc(oPromozioni) Then Continue For
          End If

          CalcolaPromozioni_FiltroPromozione(dtrPromo, strSelect, oPromozioni)

          'Seleziona le righe che potrebbero essere considerate valide per la promozione (esclude a priori l'articolo D).
          Dim dtrRows() As DataRow = oPromozioni.dttEc.Select(strSelect)

          'Compatibilità promozioni, esclude le righe non compatibili con la promozione attuale
          Dim lPromo As Integer = 0
          For Each dtrEc As DataRow In dtrRows
            'Se stessa non la considera
            For y As Integer = 1 To 6
              lPromo = NTSCInt(dtrEc("ec_codrepr" & y))
              If lPromo = 0 Then Continue For 'Continua alla prossima promozione
              Dim dtrTmp As DataRow = oPromozioni.dttPromo.Select("tb_codrepr = " & lPromo)(0)

              If Not CompatibilitaPromozioniPerArticolo(oPromozioni.Parametri.strDitta, dtrPromo, dtrTmp, NTSCStr(dtrEc!ec_codart)) Then
                strSelect &= " AND ec_riga <> " & NTSCInt(dtrEc!ec_riga)
              End If
            Next
          Next

          'Applico il nuovo filtro, escludendo le righe con promozioni incompatibili
          dtrRows = oPromozioni.dttEc.Select(strSelect)

          If dtrRows.Length = 0 Then Continue For

          Select Case NTSCStr(!tb_tipopr)
            Case "V" ' Punti x valore
              CalcolaPromozioni_PuntiPerValore(dtrPromo, dtrRows, lPuntiTot, oPromozioni)
            Case "Q" ' Punti x qta
              CalcolaPromozioni_PuntiPerQta(dtrPromo, dtrRows, lPuntiTot, oPromozioni)
            Case "T" ' Sconto piede %
              CalcolaPromozioni_ScontoPiedePerc(dtrPromo, dtrRows, oPromozioni)
            Case "U" ' Sconto piede a valore
              CalcolaPromozioni_ScontoPiedeValore(dtrPromo, dtrRows, oPromozioni)
            Case "M" ' MxN stesso articolo
              CalcolaPromozioni_MxNstessoArticolo(dtrPromo, dtrRows, oPromozioni)
            Case "N" ' MxN misto
              CalcolaPromozioni_MxNmisto(dtrPromo, dtrRows, oPromozioni)
            Case "P" ' Sconto riga %
              CalcolaPromozioni_ScontoRigaPerc(dtrPromo, dtrRows, dScontoTotalePromo, oPromozioni)
            Case "L" ' Applica listino
              CalcolaPromozioni_ApplicaListino(dtrPromo, dtrRows, oPromozioni)
            Case "D"  ' Sconto % Differito 
              CalcolaPromozioni_ScontoPercDiff(dtrPromo, dtrRows, oPromozioni)
            Case "E"  ' Sconto Valore Differito 
              CalcolaPromozioni_ScontoValoreDiff(dtrPromo, dtrRows, oPromozioni)
            Case "O"  ' Articolo Omaggio Differito 
              CalcolaPromozioni_OmaggioDiff(dtrPromo, dtrRows, oPromozioni)
            Case "X" ' Provvigione % Differito 
              CalcolaPromozioni_ProvvigPercDiff(dtrPromo, dtrRows, oPromozioni)
            Case "Y" ' Provvigione Valore Differito 
              CalcolaPromozioni_ProvvigValoreDiff(dtrPromo, dtrRows, oPromozioni)
            Case "Z" ' Premio Omaggio Differito 
              CalcolaPromozioni_PremioDiff(dtrPromo, dtrRows, oPromozioni)
            Case "W" ' Provvigione Agente
              CalcolaPromozioni_ProvvigioneAgentePerc(dtrPromo, dtrRows, oPromozioni)
            Case Else 'Promozione Personalizzata
              CalcolaPromozioni_ApplicaPromozione_Pers(dtrPromo, dtrRows, oPromozioni)
          End Select

          If lPuntiTot < 0 Then lPuntiTot = 0
          oPromozioni.dttEt.Rows(0)!xx_totpunti = lPuntiTot
        End With
      Next

      CalcolaPromozioni_ColoreGrigliaPromozioni(oPromozioni)

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
  Public Overridable Function CalcolaPromozioni_PromoTotaleDoc(ByVal oPromozioni As NTSPromozioni) As Decimal
    Dim dTotal As Decimal
    Dim dSpese As Decimal
    Try
      'Il totale del documento (per le promozioni) è dato da quanto deve pagare il cliente + acconti + gift card (senza le varie spese di trasporto, incasso, ecc...)
      With oPromozioni
        dTotal = NTSCDec(oPromozioni.dttEt.Rows(0)!et_totdoc) - NTSCDec(oPromozioni.dttEt.Rows(0)!et_abbuono) - NTSCDec(oPromozioni.dttEt.Rows(0)!et_totomag)

        dSpese = NTSCDec(oPromozioni.dttEt.Rows(0)!et_speacc) + NTSCDec(oPromozioni.dttEt.Rows(0)!et_speimb) + NTSCDec(oPromozioni.dttEt.Rows(0)!et_speinc) + NTSCDec(oPromozioni.dttEt.Rows(0)!et_bolli)
        If dSpese <> 0 Then
          If NTSCInt(oPromozioni.dttEt.Rows(0)!et_codese) > 0 Then
            dTotal -= AggiungiIva(dSpese, NTSCInt(oPromozioni.dttEt.Rows(0)!et_codese))
          Else
            dTotal -= AggiungiIva(dSpese, .Parametri.nPeacIvainc)
          End If
        End If

        For Each dtrEc As DataRow In .dttEc.Rows
          With dtrEc
            If (oPromozioni.Parametri.bDaGPV AndAlso NTSCStr(!xxo_gift) <> "N") OrElse _
                (.Table.Columns.Contains("ec_acnum") AndAlso NTSCInt(!ec_acnum) > 0) Then
              If oPromozioni.Parametri.bDaGPV AndAlso NTSCDec(!xxo_valoreiva) > 0 Then
                dTotal += Math.Abs(NTSCDec(!xxo_valoreiva))
              Else
                dTotal += AggiungiIva(Math.Abs(NTSCDec(!ec_valore)), NTSCInt(!ec_codiva))
              End If
            End If
          End With
        Next
      End With

      Return dTotal
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return 0
    End Try
  End Function
  Public Overridable Function CalcolaPromozioni_FiltroPromozione(ByVal dtrRow As DataRow, ByRef strSelect As String, ByVal oPromozioni As NTSPromozioni) As Boolean
    Try
      With dtrRow
        'Compone la select per filtrare solo le righe degli articoli rilevanti
        strSelect = ""
        'Se è uguale a S deve prendere tutte le promozioni
        If NTSCStr(!tb_fltutti) = "N" Then
          If NTSCStr(!tb_codart).Trim <> "" Then
            strSelect = " ec_codart = " & CStrSQL(!tb_codart)
          ElseIf NTSCInt(!tb_codlsar) <> 0 Then
            'Seleziona da database tutti gli articoli associati alla lista selezionata e li separa con , per metterli in una IN (...)
            Dim dttArticoli As New DataTable
            If Not oCldComm.ArticoliInListaSelezionata(oPromozioni.Parametri.strDitta, NTSCInt(!tb_codlsar), dttArticoli) Then Return False

            Dim strArticoli As New StringBuilder
            For Each dtrArt As DataRow In dttArticoli.Rows
              strArticoli.Append(CStrSQL(dtrArt!lsa_codart) & ", ")
            Next
            If strArticoli.Length > 0 Then
              strArticoli = strArticoli.Remove(strArticoli.Length - 2, 2)
            Else
              strArticoli.Append("' '") 'Per evitare errore di IN vuota. anche se in effetti non ha molto senso creare una promozione con lista selezionata vuota.
            End If

            strSelect = " ec_codart IN (" & strArticoli.ToString() & ")"
          Else
            If NTSCInt(!tb_gruppo) <> 0 Then strSelect &= " AND xxo_gruppo = " & NTSCInt(!tb_gruppo)
            If NTSCInt(!tb_sotgru) <> 0 Then strSelect &= " AND xxo_sotgru = " & NTSCInt(!tb_sotgru)
            If NTSCStr(!tb_codcfam).Trim <> "" Then strSelect &= " AND ec_codcfam = " & CStrSQL(!tb_codcfam)

            strSelect = strSelect.Substring(5)
          End If
        End If

        If strSelect.Length > 0 Then strSelect &= " AND "

        'Gli articoli D vengono sempre esclusi dalle promozioni e le righe che con stampa riga diverso da SI e le gift card
        strSelect &= " ec_codart <> 'D' AND ec_stasino = 'S'"
        If oPromozioni.Parametri.bDaGPV Then strSelect &= " AND xxo_gift = 'N' "
        'Ad esempio in gestione ordini e gestione offerte non è presente la causale per determinare il reso di riga
        If oPromozioni.dttEc.Columns.Contains("xxo_reso") Then
          If Not oPromozioni.Parametri.strPromoNoStornoResi.ToUpper.Contains(NTSCStr(!tb_tipopr)) Then
            strSelect &= " AND ISNULL(xxo_reso, ' ') NOT IN ('R','D') "
          End If
        End If

        'Infine esclude le righe che evadono offerte, ordini o note di prelievo
        Select Case NTSCStr(dtrRow!tb_tipopr)
          Case "W", "X", "Y", "Z" 'Promozione per agente, vanno ricalcolate negli ordini perchè le offerte non gestiscono le provvigioni
            strSelect &= CalcolaPromozioni_FiltroEvasione_ConsideraDaOfferte(oPromozioni)
          Case "M", "N" 'MxN normale e misto
            'Gli articoli KIT non devono essere trattati nell'MxN perchè darebbero problemi.
            strSelect &= " AND ec_flkit = ' '"
            strSelect &= CalcolaPromozioni_FiltroEvasione(oPromozioni)
          Case Else 'Promozioni per cliente
            strSelect &= CalcolaPromozioni_FiltroEvasione(oPromozioni)
        End Select
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

  Public Overridable Function CalcolaPromozioni_PuntiPerValore(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByRef lPuntiTot As Integer, _
                                                               ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim x As Integer
    Dim nMinMax, nMaxDoc, nMaxTot, nMaxClie As Integer
    Dim lMaxPunti As Integer
    Try
      With dtrPromo
        'Controlliamo le singole righe
        Dim dValoreTot As Decimal = 0
        For x = 0 To dtrRow.Length - 1
          ' If Not CalcolaPromozioni_CheckMaxPerDocu(dCountDoc, dttPromo.Rows(lRigaPromo), dtrRow, x) Then Exit For
          If NTSCStr(dtrRow(x)!ec_stasino) <> "S" Then Continue For 'Le righe con Stampa Riga = Sconto Merce NC non vanno contate

          If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrRow(x)) Then Continue For

          If oPromozioni.Parametri.bDaGPV AndAlso NTSCDec(dtrRow(x)!xxo_valoreiva) > 0 Then
            dValoreTot += NTSCDec(dtrRow(x)!xxo_valoreiva)
          Else
            dValoreTot += AggiungiIva(NTSCDec(dtrRow(x)!ec_valore), NTSCInt(dtrRow(x)!ec_codiva))
          End If
        Next

        lPuntiTot += NTSCInt(Math.Floor(dValoreTot / NTSCDec(!tb_valric))) * NTSCInt(!tb_ptiacc)

        nMaxDoc = NTSCInt(!tb_nprdoc)
        nMaxTot = NTSCInt(!tb_npromo) - NTSCInt(!xx_npromo)
        nMaxClie = NTSCInt(!tb_nprclie) - NTSCInt(!xx_nprclie)

        'Identifica qual'è il limite massimo di promozioni da applicare
        If nMaxDoc > 0 Then nMinMax = nMaxDoc
        If nMinMax = 0 Or nMaxTot > 0 And nMaxTot < nMinMax Then nMinMax = nMaxTot
        If nMinMax = 0 Or nMaxClie > 0 And nMaxClie < nMinMax Then nMinMax = nMaxClie

        If nMinMax > 0 Then
          For x = 0 To dtrRow.Length - 1
            ' If Not CalcolaPromozioni_CheckMaxPerDocu(dCountDoc, dttPromo.Rows(lRigaPromo), dtrRow, x) Then Exit For
            If NTSCStr(dtrRow(x)!ec_stasino) <> "S" Then Continue For 'Le righe con Stampa Riga = Sconto Merce NC non vanno contate

            If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrRow(x)) Then Continue For
          Next

          lMaxPunti = nMinMax * NTSCInt(!tb_ptiacc)
          If lMaxPunti < lPuntiTot Then lPuntiTot = lMaxPunti
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
  Public Overridable Function CalcolaPromozioni_PuntiPerQta(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByRef lPuntiTot As Integer, _
                                                            ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim x As Integer
    Dim dCountDoc As Decimal = 0
    Try
      With dtrPromo
        'Controlliamo le singole righe
        Dim dQtaTot As Decimal = 0
        For x = 0 To dtrRow.Length - 1
          If NTSCStr(dtrRow(x)!ec_stasino) <> "S" Then Continue For 'Le righe con Stampa Riga = Sconto Merce NC non vanno contate

          If Not CalcolaPromozioni_CheckMaxPerDocu(dCountDoc, dtrPromo, dtrRow, x, oPromozioni) Then Exit For

          dQtaTot += NTSCDec(dtrRow(x)!ec_quant)
        Next

        Dim lPuntiDoc As Integer = NTSCInt(Math.Floor(dQtaTot / NTSCDec(!tb_qtaric))) * NTSCInt(!tb_ptiacc)
        If lPuntiDoc > 0 Then
          lPuntiTot += lPuntiDoc

          For x = 0 To dtrRow.Length - 1
            If NTSCStr(dtrRow(x)!ec_stasino) <> "S" Then Continue For 'Le righe con Stampa Riga = Sconto Merce NC non vanno contate

            If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrRow(x)) Then Continue For
          Next
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
  Public Overridable Function CalcolaPromozioni_ScontoPiedePerc(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim bSalta As Boolean = False
    Dim dValore As Decimal = 0
    Dim x As Integer
    Try
      'Genera una riga con il valore dello sconto
      'Se sono qui, vuol dire che è già verificato che devo assegnare lo sconto,
      'quindi aggiorno le righe con il codice della promozione e creo la riga di sconto
      With dtrPromo
        'Ricalcolo i totali per avere il valore del documento corretto e complessivo di tutto.
        ThrowRemoteEvent(New NTSEventArgs("CalcolaTotali", ""))

        If oPromozioni.Parametri.lCausaleScontiPiede = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130631153242649380, "La promozione di piede non sarà calcolata in quanto non è stata configurata l'opzione di registro BSREGSRE\OPZIONI\CausaleScontiPiede")))
          Return True
        End If
        If NTSCDec(oPromozioni.dttEt.Rows(0)!et_totdoc) > 0 Then
          For x = 0 To dtrRow.Length - 1
            'Prima controlla se può applicare la promozione su tutte le righe, altrimenti la annulla in partenza.
            If NTSCDec(dtrRow(x)!ec_codrepr6) <> 0 AndAlso NTSCDec(dtrRow(x)!ec_codrepr5) <> 0 AndAlso NTSCDec(dtrRow(x)!ec_codrepr4) <> 0 AndAlso _
               NTSCDec(dtrRow(x)!ec_codrepr3) <> 0 AndAlso NTSCDec(dtrRow(x)!ec_codrepr2) <> 0 AndAlso NTSCDec(dtrRow(x)!ec_codrepr1) <> 0 Then
              bSalta = True
              Exit For
            End If
          Next
          If Not bSalta Then
            Dim dValoreSconto As Decimal = CalcolaPromozioni_PromoTotaleDoc(oPromozioni) * (NTSCDec(!tb_sconto) / 100)
            If dValoreSconto = 0 Then Return True 'Se lo sconto è 0, allora evito di creare la riga

            For x = 0 To dtrRow.Length - 1
              If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrRow(x)) Then Continue For
            Next
            'ThrowRemoteEvent(New NTSEventArgs("bNonValidare", "-1"))
            oPromozioni.dttEc.Rows.InsertAt(oPromozioni.dttEc.NewRow, 0)
            'ThrowRemoteEvent(New NTSEventArgs("bNonValidare", "0"))
            Dim dtrEc As DataRow = oPromozioni.dttEc.Rows(0)
            dtrEc!ec_codart = "D"
            dtrEc!ec_descr = !tb_desrepr
            If dtrEc.Table.Columns.Contains("ec_causale") Then dtrEc!ec_causale = oPromozioni.Parametri.lCausaleScontiPiede
            dtrEc!ec_quant = 1
            'dtrEc!ec_codrepr1 = NTSCInt(!tb_codrepr)
            If Not oPromozioni.Parametri.bdaOfferta Then
              dtrEc!ec_codrepc = oPromozioni.Parametri.strCodRepc
              dtrEc!ec_opnome = oApp.User.Nome ' strOperatore: problemi di gestione con le varie opzioni in pers retail per cambiare commesso al volo
            End If
            dtrEc!ec_codiva = oPromozioni.dttEc.Select("MAX(ec_valore) = ec_valore")(0)!ec_codiva 'Prende sempre quella con il valore più alto, come codice iva
            If NTSCStr(oPromozioni.dttEt.Rows(0)!et_scorpo) = "N" Then
              dtrEc!ec_prezzo = -dValoreSconto / ((100 - NTSCDec(oPromozioni.dttEt.Rows(0)!et_scont1)) / 100 * _
                                                  (100 - NTSCDec(oPromozioni.dttEt.Rows(0)!et_scont2)) / 100 * _
                                                  (100 - NTSCDec(oPromozioni.dttEt.Rows(0)!et_scopag)) / 100)
            Else
              dtrEc!ec_preziva = -dValoreSconto / ((100 - NTSCDec(oPromozioni.dttEt.Rows(0)!et_scont1)) / 100 * _
                                                   (100 - NTSCDec(oPromozioni.dttEt.Rows(0)!et_scont2)) / 100 * _
                                                   (100 - NTSCDec(oPromozioni.dttEt.Rows(0)!et_scopag)) / 100)
            End If
            dtrEc!ec_flprzmod = "N"
            dtrEc!ec_scont1 = 0
            dtrEc!ec_scont2 = 0
            dtrEc!ec_scont3 = 0
            dtrEc!ec_scont4 = 0
            dtrEc!ec_scont5 = 0
            dtrEc!ec_scont6 = 0
            dtrEc!ec_flsc1mod = "N"
            dtrEc!ec_flsc2mod = "N"
            dtrEc!ec_flsc3mod = "N"
            dtrEc!ec_flsc4mod = "N"
            dtrEc!ec_flsc5mod = "N"
            dtrEc!ec_flsc6mod = "N"
            dtrEc!ec_tiporiga = "S"
            CalcolaPromozioni_AggiornaPromozioneDiRigaOut(NTSCInt(!tb_codrepr), dtrEc)
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
  Public Overridable Function CalcolaPromozioni_ScontoPiedeValore(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim x As Integer
    Dim bSalta As Boolean = False
    Try
      'Genera una riga per indicare che si è raggiunto il valore dello sconto ed è stato attribuito
      'Se sono qui, vuol dire che è già verificato che devo assegnare lo sconto,
      'quindi aggiorno le righe con il codice della promozione e creo la riga di sconto

      'Ricalcolo i totali per avere il valore del documento corretto e complessivo di tutto.
      ThrowRemoteEvent(New NTSEventArgs("CalcolaTotali", ""))

      With dtrPromo
        If oPromozioni.Parametri.lCausaleScontiPiede = 0 Then
          Return True
        ElseIf NTSCDec(oPromozioni.dttEt.Rows(0)!et_totdoc) > 0 Then
          For x = 0 To dtrRow.Length - 1
            'Prima controlla se può applicare la promozione su tutte le righe, altrimenti la annulla in partenza.
            If NTSCDec(dtrRow(x)!ec_codrepr6) <> 0 AndAlso NTSCDec(dtrRow(x)!ec_codrepr5) <> 0 AndAlso NTSCDec(dtrRow(x)!ec_codrepr4) <> 0 AndAlso _
               NTSCDec(dtrRow(x)!ec_codrepr3) <> 0 AndAlso NTSCDec(dtrRow(x)!ec_codrepr2) <> 0 AndAlso NTSCDec(dtrRow(x)!ec_codrepr1) <> 0 Then
              bSalta = True
              Exit For
            End If
          Next
          If Not bSalta Then
            For x = 0 To dtrRow.Length - 1
              If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrRow(x)) Then Continue For
            Next
            'ThrowRemoteEvent(New NTSEventArgs("bNonValidare", "-1"))
            oPromozioni.dttEc.Rows.InsertAt(oPromozioni.dttEc.NewRow, 0)
            'ThrowRemoteEvent(New NTSEventArgs("bNonValidare", "0"))
            Dim dtrEc As DataRow = oPromozioni.dttEc.Rows(0)
            dtrEc!ec_codart = "D"
            dtrEc!ec_descr = !tb_desrepr
            If dtrEc.Table.Columns.Contains("ec_causale") Then dtrEc!ec_causale = oPromozioni.Parametri.lCausaleScontiPiede
            dtrEc!ec_quant = 1
            'dtrEc!ec_codrepr1 = NTSCInt(!tb_codrepr)
            If Not oPromozioni.Parametri.bDaOfferta Then
              dtrEc!ec_codrepc = oPromozioni.Parametri.strCodRepc
              dtrEc!ec_opnome = oApp.User.Nome ' strOperatore: problemi di gestione con le varie opzioni in pers retail per cambiare commesso al volo
            End If
            dtrEc!ec_codiva = oPromozioni.dttEc.Select("MAX(ec_valore) = ec_valore")(0)!ec_codiva 'Prende sempre quella con il valore più alto, come codice iva
            If NTSCStr(oPromozioni.dttEt.Rows(0)!et_scorpo) = "N" Then
              dtrEc!ec_prezzo = -ScorporaIva(NTSCDec(!tb_scontv), NTSCInt(dtrEc!ec_codiva), 0)
            Else
              dtrEc!ec_preziva = -NTSCDec(!tb_scontv)
            End If
            dtrEc!ec_flprzmod = "N"
            dtrEc!ec_scont1 = 0
            dtrEc!ec_scont2 = 0
            dtrEc!ec_scont3 = 0
            dtrEc!ec_scont4 = 0
            dtrEc!ec_scont5 = 0
            dtrEc!ec_scont6 = 0
            dtrEc!ec_flsc1mod = "N"
            dtrEc!ec_flsc2mod = "N"
            dtrEc!ec_flsc3mod = "N"
            dtrEc!ec_flsc4mod = "N"
            dtrEc!ec_flsc5mod = "N"
            dtrEc!ec_flsc6mod = "N"
            dtrEc!ec_tiporiga = "S"
            CalcolaPromozioni_AggiornaPromozioneDiRigaOut(NTSCInt(!tb_codrepr), dtrEc)
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
  Public Overridable Function CalcolaPromozioni_MxNstessoArticolo(ByVal dtrPromo As DataRow, ByRef dtrRows() As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim strCurrentCodart As String = ""
    Dim arListCodart As New ArrayList
    Try
      With dtrPromo
        Dim dQtaOmaggio As Decimal = NTSCDec(!tb_quantm) - NTSCDec(!tb_quantn)

        'Scorre tutte le righe, per trattare un articolo alla volta.
        For Each dtrArticolo As DataRow In dtrRows
          If CalcolaPromozioni_MxNBloccoMovQtaLotto(NTSCStr(dtrArticolo!ec_codart), oPromozioni) Then Continue For

          If arListCodart.Contains(NTSCStr(dtrArticolo!ec_codart)) Then Continue For 'Se è già stato trattato lo ignoro
          strCurrentCodart = NTSCStr(dtrArticolo!ec_codart)
          arListCodart.Add(NTSCStr(dtrArticolo!ec_codart))

          Dim dQtaTot As Decimal = 0
          'scorro gli articoli dello stesso tipo del corrente per trovare la quantità massima di omaggio possibile per quell'articolo
          For Each dtrRow As DataRow In dtrRows
            If strCurrentCodart <> NTSCStr(dtrRow!ec_codart) Then Continue For
            dQtaTot += NTSCDec(dtrRow!ec_quant)
          Next

          'Quante volte bisogna applicare la promozione
          Dim dQtaTotOmaggio As Decimal = Math.Floor(dQtaTot * dQtaOmaggio / NTSCDec(!tb_quantm) / dQtaOmaggio) * dQtaOmaggio
          'Quante volte è possibile applicare la promozione
          CalcolaPromozioni_CheckMaxPerDocuMxN(dQtaTotOmaggio, dtrPromo, oPromozioni)

          If dQtaTotOmaggio >= 1 Then
            'Aggiornare le righe delle promozioni e annulla eventuli sconti se necessario
            For Each dtrRow As DataRow In dtrRows
              If strCurrentCodart <> NTSCStr(dtrRow!ec_codart) Then Continue For
              CalcolaPromozioni_AnnullaScontiRiga(dtrPromo, dtrRow)
              If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrRow) Then Continue For
            Next

            'Applica la promozione
            CalcolaPromozioni_MxNstessoArticoloApplica(dQtaTotOmaggio, dtrRows, strCurrentCodart, dtrPromo, oPromozioni)
          End If
        Next
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
  Public Overridable Sub CalcolaPromozioni_MxNstessoArticoloApplica(ByVal dDaOmaggiare As Decimal, ByVal dtrRow() As DataRow, ByVal strCurrentCodart As String, _
                                                                    ByVal dtrPromo As DataRow, ByVal oPromozioni As NTSPromozioni)
    Dim dtrSuperioreMinima As DataRow = Nothing
    Dim dtrInferioreMassima As DataRow = Nothing
    Try
      'Prima scorre gli ex omaggi. Questo per evitare di vedere la griglia che si modifica di continuo. così credo un po' di stabilità
      For Each dtrExOmaggi As DataRow In dtrRow
        If NTSCStr(dtrExOmaggi!ec_codart) <> strCurrentCodart Then Continue For 'Se è un articolo diverso lo ignora
        ' Ignora eventuali righe già omaggiate o che non siano EX omaggi
        If NTSCStr(dtrExOmaggi!xxo_exomaggiomxn) = "N" OrElse NTSCStr(dtrExOmaggi!ec_tiporiga) = "M" Then Continue For
        If dDaOmaggiare = 0 Then Return 'Elaborazione finita, quantità completamente assegnata

        If NTSCDec(dtrExOmaggi!ec_quant) <= dDaOmaggiare Then
          'Assegno la quantità dell'ex omaggio come omaggio attuale
          CalcolaPromozioni_MxN_DeterminaPrezzoOmaggio(dtrExOmaggi, dtrPromo, oPromozioni)
          If oPromozioni.Parametri.lCodivaOmaggi <> 0 Then dtrExOmaggi!ec_codiva = oPromozioni.Parametri.lCodivaOmaggi
          dtrExOmaggi!ec_tiporiga = "M"
          CalcolaPromozioni_AggiornaPromozioneDiRigaOut(NTSCInt(dtrPromo!tb_codrepr), dtrExOmaggi)
          dDaOmaggiare -= NTSCDec(dtrExOmaggi!ec_quant)
        Else
          'Altrimenti spezzo la riga e ne creo una nuova con la quantità omaggiata
          oPromozioni.dttEc.Rows.Add()
          Dim dtrEc As DataRow = oPromozioni.dttEc.Rows(oPromozioni.dttEc.Rows.Count - 1)
          dtrEc.ItemArray = dtrExOmaggi.ItemArray
          Dim dQuant As Decimal = NTSCDec(dtrExOmaggi!ec_quant)
          If Not oPromozioni.Parametri.bDaGPV Then dtrExOmaggi!ec_colli = ArrDbl(NTSCDec(dtrExOmaggi!ec_colli) / NTSCDec(dtrExOmaggi!ec_quant) * (NTSCDec(dtrExOmaggi!ec_quant) - dDaOmaggiare), oApp.NDecQta)
          dtrExOmaggi!ec_quant = dQuant - dDaOmaggiare
          Dim dtrT() As DataRow = oPromozioni.dttEc.Select("MAX(ec_riga) = ec_riga")
          dtrEc!ec_riga = NTSCInt(dtrT(0)!ec_riga) + oPromozioni.Parametri.nIncremContatoreRiga
          If Not oPromozioni.Parametri.bDaGPV Then dtrEc!ec_colli = ArrDbl(NTSCDec(dtrEc!ec_colli) / NTSCDec(dtrEc!ec_quant) * dDaOmaggiare, oApp.NDecQta)
          dtrEc!ec_quant = dDaOmaggiare
          CalcolaPromozioni_MxN_DeterminaPrezzoOmaggio(dtrEc, dtrPromo, oPromozioni)
          If Not oPromozioni.Parametri.bDaOfferta Then
            dtrEc!ec_codrepc = oPromozioni.Parametri.strCodRepc
            dtrEc!ec_opnome = oApp.User.Nome ' strOperatore: problemi di gestione con le varie opzioni in pers retail per cambiare commesso al volo
          End If
          If oPromozioni.Parametri.lCodivaOmaggi <> 0 Then dtrEc!ec_codiva = oPromozioni.Parametri.lCodivaOmaggi
          dtrEc!ec_tiporiga = "M"
          CalcolaPromozioni_MxNSpezza_AggiornaTcoMatricole(oPromozioni, dtrExOmaggi, dtrEc)
          CalcolaPromozioni_AggiornaPromozioneDiRigaOut(NTSCInt(dtrPromo!tb_codrepr), dtrEc)
          CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(dtrPromo!tb_codrepr), dtrExOmaggi)
          CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(dtrPromo!tb_codrepr), dtrEc)
          Return
        End If
      Next

      'Se è sufficiente scalo la quantità da omaggiare a partire dalle ultima riga
      For z As Integer = dtrRow.Length - 1 To 0 Step -1
        If dDaOmaggiare = 0 Then Return 'Elaborazione finita, quantità completamente assegnata

        If NTSCStr(dtrRow(z)!ec_codart) <> strCurrentCodart Then Continue For 'Se è un articolo diverso lo ignora
        If NTSCStr(dtrRow(z)!ec_tiporiga) = "M" Then Continue For ' Ignora eventuali righe già omaggiate
        'Caso semplice, trovo subito la quantità che mi serve. Assegno tutto quanto.
        If NTSCDec(dtrRow(z)!ec_quant) = dDaOmaggiare Then
          CalcolaPromozioni_MxN_DeterminaPrezzoOmaggio(dtrRow(z), dtrPromo, oPromozioni)
          If oPromozioni.Parametri.lCodivaOmaggi <> 0 Then dtrRow(z)!ec_codiva = oPromozioni.Parametri.lCodivaOmaggi
          dtrRow(z)!ec_tiporiga = "M"
          CalcolaPromozioni_AggiornaPromozioneDiRigaOut(NTSCInt(dtrPromo!tb_codrepr), dtrRow(z))
          Return
        Else
          'Determino quali righe prendere in considerazione
          If dDaOmaggiare > NTSCDec(dtrRow(z)!ec_quant) AndAlso _
             (dtrInferioreMassima Is Nothing OrElse NTSCDec(dtrInferioreMassima!ec_quant) < NTSCDec(dtrRow(z)!ec_quant)) Then dtrInferioreMassima = dtrRow(z)
          If dDaOmaggiare < NTSCDec(dtrRow(z)!ec_quant) AndAlso _
             (dtrSuperioreMinima Is Nothing OrElse NTSCDec(dtrSuperioreMinima!ec_quant) > NTSCDec(dtrRow(z)!ec_quant)) Then dtrSuperioreMinima = dtrRow(z)
        End If
      Next

      If dtrInferioreMassima IsNot Nothing Then
        'Assegno la quantità minima che ho trovato e rieseguo l'elaborazione.
        CalcolaPromozioni_MxN_DeterminaPrezzoOmaggio(dtrInferioreMassima, dtrPromo, oPromozioni)
        If oPromozioni.Parametri.lCodivaOmaggi <> 0 Then dtrInferioreMassima!ec_codiva = oPromozioni.Parametri.lCodivaOmaggi
        dtrInferioreMassima!ec_tiporiga = "M"
        CalcolaPromozioni_AggiornaPromozioneDiRigaOut(NTSCInt(dtrPromo!tb_codrepr), dtrInferioreMassima)
        dDaOmaggiare -= NTSCDec(dtrInferioreMassima!ec_quant)
        CalcolaPromozioni_MxNstessoArticoloApplica(dDaOmaggiare, dtrRow, strCurrentCodart, dtrPromo, oPromozioni)
        Return
      ElseIf dtrSuperioreMinima IsNot Nothing Then
        'Altrimenti spezzo la riga e ne creo una nuova con la quantità omaggiata
        oPromozioni.dttEc.Rows.Add()
        Dim dtrEc As DataRow = oPromozioni.dttEc.Rows(oPromozioni.dttEc.Rows.Count - 1)
        dtrEc.ItemArray = dtrSuperioreMinima.ItemArray
        Dim dQuant As Decimal = NTSCDec(dtrSuperioreMinima!ec_quant)
        If Not oPromozioni.Parametri.bDaGPV Then dtrSuperioreMinima!ec_colli = ArrDbl(NTSCDec(dtrSuperioreMinima!ec_colli) / NTSCDec(dtrSuperioreMinima!ec_quant) * (NTSCDec(dtrSuperioreMinima!ec_quant) - dDaOmaggiare), oApp.NDecQta)
        dtrSuperioreMinima!ec_quant = dQuant - dDaOmaggiare
        Dim dtrT() As DataRow = oPromozioni.dttEc.Select("MAX(ec_riga) = ec_riga")
        dtrEc!ec_riga = NTSCInt(dtrT(0)!ec_riga) + oPromozioni.Parametri.nIncremContatoreRiga
        If Not oPromozioni.Parametri.bDaGPV Then dtrEc!ec_colli = ArrDbl(NTSCDec(dtrEc!ec_colli) / NTSCDec(dtrEc!ec_quant) * dDaOmaggiare, oApp.NDecQta)
        dtrEc!ec_quant = dDaOmaggiare
        CalcolaPromozioni_MxN_DeterminaPrezzoOmaggio(dtrEc, dtrPromo, oPromozioni)
        If Not oPromozioni.Parametri.bDaOfferta Then
          dtrEc!ec_codrepc = oPromozioni.Parametri.strCodRepc
          dtrEc!ec_opnome = oApp.User.Nome ' strOperatore: problemi di gestione con le varie opzioni in pers retail per cambiare commesso al volo
        End If
        If oPromozioni.Parametri.lCodivaOmaggi <> 0 Then dtrEc!ec_codiva = oPromozioni.Parametri.lCodivaOmaggi
        dtrEc!ec_tiporiga = "M"
        CalcolaPromozioni_MxNSpezza_AggiornaTcoMatricole(oPromozioni, dtrSuperioreMinima, dtrEc)
        CalcolaPromozioni_AggiornaPromozioneDiRigaOut(NTSCInt(dtrPromo!tb_codrepr), dtrEc)
        CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(dtrPromo!tb_codrepr), dtrSuperioreMinima)
        CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(dtrPromo!tb_codrepr), dtrEc)
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
  Public Overridable Function CalcolaPromozioni_MxNmisto(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim x, j As Integer
    Dim dtrSel() As DataRow
    Dim dQtaTot, dQtaOmaggio, dQtaTotOmaggio, dTmp As Decimal
    Dim strArticoli As String = ""
    Dim bApplicata As Boolean = False
    Dim dCountDoc As Decimal = 0
    Try
      With dtrPromo
        For x = 0 To dtrRow.Length - 1
          If CalcolaPromozioni_MxNBloccoMovQtaLotto(NTSCStr(dtrRow(x)!ec_codart), oPromozioni) Then Continue For

          strArticoli &= CStrSQL(dtrRow(x)!ec_codart) & ", "
        Next
        If strArticoli.Length = 0 Then Return True 'Non ci sono articoli da trattare, esce dalla promozione
        strArticoli = strArticoli.Remove(strArticoli.Length - 2)

        dtrSel = oPromozioni.dttEc.Select("ec_codart IN (" & strArticoli & ") AND ec_stasino = 'S'", "ec_prezzo DESC, ec_riga")

        For Each dtrTmp As DataRow In dtrSel
          CalcolaPromozioni_AnnullaScontiRiga(dtrPromo, dtrTmp)

          dQtaTot += NTSCDec(dtrTmp!ec_quant)
        Next

        dQtaOmaggio = NTSCDec(!tb_quantm) - NTSCDec(!tb_quantn)
        dQtaTotOmaggio = Math.Floor(dQtaTot * dQtaOmaggio / NTSCDec(!tb_quantm) / dQtaOmaggio) * dQtaOmaggio

        'Al contrario così parto a scalare dalle ultime righe
        dTmp = dQtaTotOmaggio
        For j = dtrSel.Length - 1 To 0 Step -1
          If dTmp = 0 Then Exit For

          If Not CalcolaPromozioni_CheckMaxPerDocu(dCountDoc, dtrPromo, dtrSel, j, oPromozioni) Then Exit For

          bApplicata = True

          'Se è sufficiente scalo la quantità da omaggiare dall'ultima riga (che è quella di valore minore, e poi salgo)
          If NTSCDec(dtrSel(j)!ec_quant) <= dTmp Then
            CalcolaPromozioni_MxN_DeterminaPrezzoOmaggio(dtrSel(j), dtrPromo, oPromozioni)
            If oPromozioni.Parametri.lCodivaOmaggi <> 0 Then dtrSel(j)!ec_codiva = oPromozioni.Parametri.lCodivaOmaggi
            dtrSel(j)!ec_tiporiga = "M"
            CalcolaPromozioni_AggiornaPromozioneDiRigaOut(NTSCInt(dtrPromo!tb_codrepr), dtrSel(j))
            dTmp -= NTSCDec(dtrSel(j)!ec_quant)
          Else
            'Altrimenti spezzo la riga e ne creo una nuova con la quantità omaggiata
            oPromozioni.dttEc.Rows.Add()
            Dim dtrEc As DataRow = oPromozioni.dttEc.Rows(oPromozioni.dttEc.Rows.Count - 1)
            dtrEc.ItemArray = dtrSel(j).ItemArray
            Dim dQuant As Decimal = NTSCDec(dtrSel(j)!ec_quant)
            If Not oPromozioni.Parametri.bDaGPV Then dtrSel(j)!ec_colli = ArrDbl(NTSCDec(dtrSel(j)!ec_colli) / NTSCDec(dtrSel(j)!ec_quant) * (NTSCDec(dtrSel(j)!ec_quant) - dTmp), oApp.NDecQta)
            dtrSel(j)!ec_quant = dQuant - dTmp
            Dim dtrT() As DataRow = oPromozioni.dttEc.Select("MAX(ec_riga) = ec_riga")
            dtrEc!ec_riga = NTSCInt(dtrT(0)!ec_riga) + oPromozioni.Parametri.nIncremContatoreRiga
            If Not oPromozioni.Parametri.bDaGPV Then dtrEc!ec_colli = ArrDbl(NTSCDec(dtrEc!ec_colli) / NTSCDec(dtrEc!ec_quant) * dTmp, oApp.NDecQta)
            dtrEc!ec_quant = dTmp
            If Not oPromozioni.Parametri.bDaOfferta Then
              dtrEc!ec_codrepc = oPromozioni.Parametri.strCodRepc
              dtrEc!ec_opnome = oApp.User.Nome ' strOperatore: problemi di gestione con le varie opzioni in pers retail per cambiare commesso al volo
            End If
            CalcolaPromozioni_MxN_DeterminaPrezzoOmaggio(dtrEc, dtrPromo, oPromozioni)
            If oPromozioni.Parametri.lCodivaOmaggi <> 0 Then dtrEc!ec_codiva = oPromozioni.Parametri.lCodivaOmaggi
            dtrEc!ec_tiporiga = "M"
            CalcolaPromozioni_MxNSpezza_AggiornaTcoMatricole(oPromozioni, dtrSel(j), dtrEc)
            CalcolaPromozioni_AggiornaPromozioneDiRigaOut(NTSCInt(dtrPromo!tb_codrepr), dtrEc)
            CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrEc)
            dTmp = 0
          End If
        Next

        If bApplicata Then
          For j = 0 To dtrSel.Length - 1
            If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrSel(j)) Then Continue For
          Next
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
  Public Overridable Function CalcolaPromozioni_MxNBloccoMovQtaLotto(ByVal strCodart As String, ByVal oPromozioni As NTSPromozioni) As Boolean
    Try
      'Verifico se è applicabile la movimentazione per lotto
      If oPromozioni.Parametri.bMovimQtaLotti Then
        Dim dttArt As New DataTable
        oCldComm.ValCodiceDb(strCodart, oPromozioni.Parametri.strDitta, "ARTICO", "S", , dttArt)
        If dttArt.Rows.Count > 0 Then
          If NTSCStr(dttArt.Rows(0)!ar_polriord) = "F" OrElse NTSCDec(dttArt.Rows(0)!ar_minord) > 0 Then Return True
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
  Public Overridable Sub CalcolaPromozioni_MxN_DeterminaPrezzoOmaggio(ByVal dtrEc As DataRow, ByVal dtrPromo As DataRow, ByVal oPromozioni As NTSPromozioni)
    Try
      'Se non devo far pagare nulla al cliente, indico il flag di stampa riga corretto.
      'Altrimenti potrei dover far pagare un prezzo simbolico o applicare uno sconto in promozione
      If NTSCDec(dtrPromo!tb_valomg) = 0 AndAlso NTSCDec(dtrPromo!tb_sconto) = 0 Then
        dtrEc!ec_stasino = oPromozioni.Parametri.strStampaRigaOmaggi
      ElseIf NTSCDec(dtrPromo!tb_valomg) <> 0 Then
        If NTSCStr(oPromozioni.dttEt.Rows(0)!et_scorpo) = "S" Then
          dtrEc!ec_preziva = dtrPromo!tb_valomg
        Else
          dtrEc!ec_prezzo = dtrPromo!tb_valomg
        End If
        dtrEc!ec_scont1 = 0
        dtrEc!ec_scont2 = 0
        dtrEc!ec_scont3 = 0
        dtrEc!ec_scont4 = 0
        dtrEc!ec_scont5 = 0
        dtrEc!ec_scont6 = 0
      ElseIf NTSCDec(dtrPromo!tb_sconto) <> 0 Then
        dtrEc!ec_scontp = NTSCDec(dtrPromo!tb_sconto)
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

  Public Overridable Function CalcolaPromozioni_ScontoRigaPerc(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByRef dScontoTotalePromo() As Decimal, ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim x, k As Integer
    Dim dCountDoc As Decimal = 0
    Try
      With dtrPromo
        'Controlliamo le singole righe
        For x = 0 To dtrRow.Length - 1
          'Cumula gli sconti di piede
          For k = 0 To dScontoTotalePromo.Length - 1
            Dim dSconto As Decimal = 0
            If k = 0 Then
              dSconto = NTSCDec(!tb_sconto)
            Else
              dSconto = NTSCDec(dtrPromo("tb_scont" & k))
            End If

            dScontoTotalePromo(k) = dSconto

            If CalcolaPromozioni_ScontoRigaPerc_VerificaAltraPromoApplicata(dtrPromo.Table, dtrRow(x)) Then
              dScontoTotalePromo(k) = 100 - dScontoTotalePromo(k)
              dScontoTotalePromo(k) *= (100 - dSconto) / 100
              dScontoTotalePromo(k) = 100 - dScontoTotalePromo(k)
            End If
          Next

          'Se non supera lo sconto già applicato, non applica la promozione
          If NTSCStr(!tb_flscontsup) = "S" Then
            Dim dTotScont As Decimal = 100
            dTotScont -= NTSCDec(dtrRow(x)!ec_scont1)
            dTotScont *= (100 - NTSCDec(dtrRow(x)!ec_scont2)) / 100
            dTotScont *= (100 - NTSCDec(dtrRow(x)!ec_scont3)) / 100
            dTotScont *= (100 - NTSCDec(dtrRow(x)!ec_scont4)) / 100
            dTotScont *= (100 - NTSCDec(dtrRow(x)!ec_scont5)) / 100
            dTotScont *= (100 - NTSCDec(dtrRow(x)!ec_scont6)) / 100

            dTotScont = 100 - dTotScont

            Dim dTotCorrente As Decimal = 100
            dTotCorrente -= dScontoTotalePromo(0)
            For z As Integer = 1 To dScontoTotalePromo.Length - 1
              dTotCorrente *= (100 - dScontoTotalePromo(z)) / 100
            Next
            dTotCorrente = 100 - dTotCorrente

            'Valuta se lo sconto applicato è migliore di quello derivato dalla promozione.
            If dTotCorrente <= dTotScont Then Continue For
          End If

          If Not CalcolaPromozioni_CheckMaxPerDocu(dCountDoc, dtrPromo, dtrRow, x, oPromozioni) Then Exit For

          If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrRow(x)) Then Continue For
          CalcolaPromozioni_AggiornaPromozioneDiRigaOut(NTSCInt(dtrPromo!tb_codrepr), dtrRow(x))

          'Applica il listino, aggiungendo il listino calcolato in una variabile temporanea
          dtrRow(x)!ec_scontp = dScontoTotalePromo(0)
          CalcolaPromozioni_AnnullaScontiRiga(dtrPromo, dtrRow(x))
          'Dopo aver azzerato gli sconti precedenti (se necessario) sostituisco eventuali sconti aggiuntivi percentuali
          For z As Integer = 1 To 6
            If dScontoTotalePromo(z) <> 0 Then
              dtrRow(x)("ec_scont" & z) = dScontoTotalePromo(z)
              dtrRow(x)("ec_flsc" & z & "mod") = "N"
            End If
          Next

          For z As Integer = 0 To oPromozioni.dttEc.Rows.Count - 1
            If oPromozioni.dttEc.Rows(z) Is dtrRow(x) Then
              ThrowRemoteEvent(New NTSEventArgs("SettaValoriRiga", z.ToString))
              Exit For
            End If
          Next
        Next
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
  Public Overridable Function CalcolaPromozioni_ScontoRigaPerc_VerificaAltraPromoApplicata(ByVal dttPromo As DataTable, ByVal dtrEc As DataRow) As Boolean
    Try
      For z As Integer = 1 To 6
        Dim lPromo As Integer = NTSCInt(dtrEc("ec_codrepr" & z))
        If lPromo = 0 Then Exit For

        Dim dtrPromo() As DataRow = dttPromo.Select("tb_codrepr = " & lPromo)
        If dtrPromo.Length > 0 AndAlso NTSCStr(dtrPromo(0)!tb_tipopr) = "P" Then Return True
      Next

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
  Public Overridable Function CalcolaPromozioni_ApplicaListino(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim x As Integer
    Dim dCountDoc As Decimal = 0
    Try
      'Controlliamo le singole righe
      With dtrPromo
        For x = 0 To dtrRow.Length - 1
          If Not CalcolaPromozioni_CheckMaxPerDocu(dCountDoc, dtrPromo, dtrRow, x, oPromozioni) Then Exit For

          If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrRow(x)) Then Continue For

          'Applica il listino, aggiungendo il listino calcolato in una variabile temporanea
          dtrRow(x)!xxo_listpromo = NTSCInt(!tb_codlist)
          CalcolaPromozioni_AggiornaPromozioneDiRigaOut(NTSCInt(dtrPromo!tb_codrepr), dtrRow(x))

          CalcolaPromozioni_AnnullaScontiRiga(dtrPromo, dtrRow(x))

          For z As Integer = 0 To oPromozioni.dttEc.Rows.Count - 1
            If oPromozioni.dttEc.Rows(z) Is dtrRow(x) Then
              ThrowRemoteEvent(New NTSEventArgs("SettaValoriRiga", z.ToString))
              Exit For
            End If
          Next
        Next
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
  Public Overridable Function CalcolaPromozioni_ApplicaPromozione_Pers(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Try
      Return True 'Per gestire promozioni personalizzate
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

  Public Overridable Function CalcolaPromozioni_ScontoPercDiff(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Try
      With dtrPromo
        'Controlliamo le singole righe
        For Each dtrEc As DataRow In dtrRow
          'Basta indicare che la promozione è stata applicata sulla riga.
          If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrEc) Then Continue For

          dtrEc!ec_scpercdiff = ArrDbl(NTSCDec(dtrEc!ec_valore) * NTSCDec(!tb_sconto) / 100, oApp.NDecImporti)
        Next
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
  Public Overridable Function CalcolaPromozioni_ScontoValoreDiff(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Try
      With dtrPromo
        'Controlliamo le singole righe
        For Each dtrEc As DataRow In dtrRow
          'Basta indicare che la promozione è stata applicata sulla riga.
          If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrEc) Then Continue For
        Next
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
  Public Overridable Function CalcolaPromozioni_OmaggioDiff(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Try
      With dtrPromo
        'Controlliamo le singole righe
        For Each dtrEc As DataRow In dtrRow
          'Basta indicare che la promozione è stata applicata sulla riga.
          If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrEc) Then Continue For
        Next
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

  Public Overridable Function CalcolaPromozioni_ProvvigPercDiff(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Try
      With dtrPromo
        'Controlliamo le singole righe
        For Each dtrEc As DataRow In dtrRow
          'Basta indicare che la promozione è stata applicata sulla riga.
          If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrEc) Then Continue For

          dtrEc("ec_prov" & NTSCInt(!xx_nagente) & "percdiff") = ArrDbl(NTSCDec(dtrEc!ec_valore) * NTSCDec(!tb_sconto) / 100, oApp.NDecImporti)
        Next
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
  Public Overridable Function CalcolaPromozioni_ProvvigValoreDiff(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Try
      With dtrPromo
        'Controlliamo le singole righe
        For Each dtrEc As DataRow In dtrRow
          'Basta indicare che la promozione è stata applicata sulla riga.
          If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrEc) Then Continue For
        Next
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
  Public Overridable Function CalcolaPromozioni_PremioDiff(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Try
      With dtrPromo
        'Controlliamo le singole righe
        For Each dtrEc As DataRow In dtrRow
          'Basta indicare che la promozione è stata applicata sulla riga.
          If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrEc) Then Continue For
        Next
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

  Public Overridable Function CalcolaPromozioni_ProvvigioneAgentePerc(ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim x As Integer
    Dim dCountDoc As Decimal = 0
    Dim strSuffix As String = ""
    Try
      With dtrPromo
        If NTSCInt(!xx_nagente) = 2 Then strSuffix = "2"
        'Controlliamo le singole righe
        For x = 0 To dtrRow.Length - 1
          'Se non supera lo sconto già applicato, non applica la promozione
          If NTSCStr(!tb_flscontsup) = "S" Then
            'Valuta se lo sconto applicato è migliore di quello derivato dalla promozione.
            If NTSCDec(dtrRow(x)("ec_provv" & strSuffix)) <= NTSCDec(dtrPromo!tb_sconto) Then Continue For
          End If

          If Not CalcolaPromozioni_CheckMaxPerDocu(dCountDoc, dtrPromo, dtrRow, x, oPromozioni) Then Exit For

          If Not CalcolaPromozioni_AggiornaPromozioneDiRiga(NTSCInt(!tb_codrepr), dtrRow(x)) Then Continue For
          CalcolaPromozioni_AggiornaPromozioneDiRigaOut(NTSCInt(dtrPromo!tb_codrepr), dtrRow(x))

          'Dopo aver azzerato gli sconti precedenti (se necessario) sostituisco eventuali sconti aggiuntivi percentuali
          If NTSCDec(dtrPromo!tb_sconto) <> 0 Then
            dtrRow(x)("ec_provv" & strSuffix) = NTSCDec(dtrPromo!tb_sconto)
            dtrRow(x)("ec_flprov" & NTSCStr(IIf(strSuffix.Length = 0, "1", strSuffix)) & "mod") = "N"
          End If
        Next
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

  Public Overridable Function CalcolaPromozioni_FiltroEvasione(ByVal oPromozioni As NTSPromozioni) As String
    Dim strSelect As String = ""
    Try
      If Not oPromozioni.Parametri.bDaGPV Then
        If oPromozioni.dttEc.Columns.Contains("ec_oaanno") Then strSelect &= " AND ec_oaanno = 0 "
        If oPromozioni.dttEc.Columns.Contains("ec_oranno") Then strSelect &= " AND ec_oranno = 0 "
        If oPromozioni.dttEc.Columns.Contains("ec_npanno") Then strSelect &= " AND ec_npanno = 0 "
        If oPromozioni.dttEc.Columns.Contains("ec_flevas") Then strSelect &= " AND ec_flevas = 'C' "
        If oPromozioni.dttEc.Columns.Contains("ec_nprquaeva") Then strSelect &= " AND ec_nprquaeva = 0 "
        If oPromozioni.dttEc.Columns.Contains("ec_quaeva") Then strSelect &= " AND ec_quaeva = 0 "
        If oPromozioni.dttEc.Columns.Contains("ec_oqanno") Then strSelect &= " AND ec_oqanno = 0 "
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
    Return strSelect
  End Function
  Public Overridable Function CalcolaPromozioni_FiltroEvasione_ConsideraDaOfferte(ByVal oPromozioni As NTSPromozioni) As String
    Dim strSelect As String = ""
    Try
      'Considera come da trattare anche le righe 
      If Not oPromozioni.Parametri.bDaGPV Then
        If oPromozioni.dttEc.Columns.Contains("ec_oaanno") Then strSelect &= " AND ec_oaanno = 0 "
        If oPromozioni.dttEc.Columns.Contains("ec_oranno") Then strSelect &= " AND ec_oranno = 0 "
        If oPromozioni.dttEc.Columns.Contains("ec_npanno") Then strSelect &= " AND ec_npanno = 0 "
        If oPromozioni.dttEc.Columns.Contains("ec_flevas") Then strSelect &= " AND ec_flevas = 'C' "
        If oPromozioni.dttEc.Columns.Contains("ec_nprquaeva") Then strSelect &= " AND ec_nprquaeva = 0 "
        If oPromozioni.dttEc.Columns.Contains("ec_quaeva") Then strSelect &= " AND ec_quaeva = 0 "
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
    Return strSelect
  End Function


  Public Overridable Function CalcolaPromozioni_AggiornaPromozioneDiRiga(ByVal lCodrepr As Integer, ByRef dtrRow As DataRow) As Boolean
    Try
      For i As Integer = 1 To 6
        Dim strCol As String = "ec_codrepr" & i
        If NTSCInt(dtrRow(strCol)) = 0 OrElse NTSCInt(dtrRow(strCol)) = lCodrepr Then
          dtrRow(strCol) = lCodrepr
          Return True
        End If
      Next

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
  Public Overridable Function CalcolaPromozioni_AggiornaPromozioneDiRigaOut(ByVal lCodrepr As Integer, ByRef dtrRow As DataRow) As Boolean
    Try
      For i As Integer = 1 To 6
        Dim strCol As String = "ec_codrepr" & i & "out"
        If NTSCInt(dtrRow(strCol)) = 0 OrElse NTSCInt(dtrRow(strCol)) = lCodrepr Then
          dtrRow(strCol) = lCodrepr
          Return True
        End If
      Next
      
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

  Public Overridable Function CalcolaPromozioni_CheckMaxPerDocu(ByRef dCountDoc As Decimal, ByVal dtrPromo As DataRow, ByRef dtrRow() As DataRow, _
                                                                ByRef i As Integer, ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim bSalta As Boolean = False
    Dim nMinMax, nMaxDoc, nMaxTot, nMaxClie As Integer
    Dim nMoltiplicatore As Decimal = 1
    Try
      nMaxDoc = NTSCInt(dtrPromo!tb_nprdoc)
      nMaxTot = NTSCInt(dtrPromo!tb_npromo) - NTSCInt(dtrPromo!xx_npromo)
      nMaxClie = NTSCInt(dtrPromo!tb_nprclie) - NTSCInt(dtrPromo!xx_nprclie)

      'Identifica qual'è il limite massimo di promozioni da applicare
      If nMaxDoc > 0 Then nMinMax = nMaxDoc
      If nMinMax = 0 Or nMaxTot > 0 And nMaxTot < nMinMax Then nMinMax = nMaxTot
      If nMinMax = 0 Or nMaxClie > 0 And nMaxClie < nMinMax Then nMinMax = nMaxClie

      If nMinMax = 0 Then Return True

      If NTSCStr(dtrPromo!tb_tipopr) = "M" OrElse NTSCStr(dtrPromo!tb_tipopr) = "N" Then nMoltiplicatore = NTSCDec(dtrPromo!tb_quantn)

      If dCountDoc >= nMinMax Then Return False

      'Il residuo lo inserisco in una nuova riga (questo è sicuramente l'ultima volta che devo entrare qui dentro
      If NTSCDec(dtrRow(i)!ec_quant) + dCountDoc * nMoltiplicatore > nMinMax * nMoltiplicatore Then
        'Controllo riga successiva, per evitare di spezzare troppe volte un articolo
        If i < dtrRow.Length - 1 Then
          If nMinMax - dCountDoc = NTSCDec(dtrRow(i + 1)!ec_quant) Then
            i += 1
            bSalta = True
            dCountDoc += NTSCDec(dtrRow(i)!ec_quant)
          End If
        End If

        If Not bSalta Then
          Dim dVariazione As Decimal = (nMinMax - dCountDoc) * nMoltiplicatore
          oPromozioni.dttEc.Rows.Add()
          Dim dtrEc As DataRow = oPromozioni.dttEc.Rows(oPromozioni.dttEc.Rows.Count - 1)
          dtrEc.ItemArray = dtrRow(i).ItemArray
          Dim dtrT() As DataRow = oPromozioni.dttEc.Select("MAX(ec_riga) = ec_riga")
          dtrEc!ec_riga = NTSCInt(dtrT(0)!ec_riga) + oPromozioni.Parametri.nIncremContatoreRiga
          dtrEc!ec_quant = dVariazione
          If Not oPromozioni.Parametri.bDaOfferta Then
            dtrEc!ec_codrepc = oPromozioni.Parametri.strCodRepc
            dtrEc!ec_opnome = oApp.User.Nome ' strOperatore: problemi di gestione con le varie opzioni in pers retail per cambiare commesso al volo
          End If
          Select Case NTSCStr(dtrPromo!tb_tipopr)
            Case "M", "N" : dtrEc!ec_stasino = oPromozioni.Parametri.strStampaRigaOmaggi
            Case Else : dtrEc!ec_stasino = "S"
          End Select
          If oPromozioni.Parametri.lCodivaOmaggi <> 0 Then dtrEc!ec_codiva = oPromozioni.Parametri.lCodivaOmaggi
          dtrRow(i)!ec_quant = NTSCDec(dtrRow(i)!ec_quant) - dVariazione
          dtrRow(i) = dtrEc
          dCountDoc = nMinMax
        End If
      Else
        dCountDoc += NTSCDec(dtrRow(i)!ec_quant) / nMoltiplicatore
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
  Public Overridable Function CalcolaPromozioni_CheckMaxPerDocuMxN(ByRef dQtaDaOmaggiare As Decimal, ByVal dtrPromo As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim nMinMax, nMaxDoc, nMaxTot, nMaxClie As Integer
    Try
      nMaxDoc = NTSCInt(dtrPromo!tb_nprdoc)
      nMaxTot = NTSCInt(dtrPromo!tb_npromo) - NTSCInt(dtrPromo!xx_npromo)
      nMaxClie = NTSCInt(dtrPromo!tb_nprclie) - NTSCInt(dtrPromo!xx_nprclie)

      'Identifica qual'è il limite massimo di promozioni da applicare
      If nMaxDoc > 0 Then nMinMax = nMaxDoc
      If nMinMax = 0 OrElse (nMaxTot > 0 AndAlso nMaxTot < nMinMax) Then nMinMax = nMaxTot
      If nMinMax = 0 OrElse (nMaxClie > 0 AndAlso nMaxClie < nMinMax) Then nMinMax = nMaxClie

      If nMinMax = 0 Then Return True

      nMinMax *= (NTSCInt(dtrPromo!tb_quantm) - NTSCInt(dtrPromo!tb_quantn)) 'Moltiplico per la quantità da omaggiare

      If dQtaDaOmaggiare >= nMinMax Then dQtaDaOmaggiare = nMinMax

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

  Public Overridable Function CalcolaPromozioni_AnnullaScontiRiga(ByVal dtrPromo As DataRow, ByRef dtrEc As DataRow) As Boolean
    Try
      If NTSCStr(dtrPromo!tb_flnosconti) = "S" Then
        dtrEc!ec_scont1 = 0
        dtrEc!ec_scont2 = 0
        dtrEc!ec_scont3 = 0
        dtrEc!ec_scont4 = 0
        dtrEc!ec_scont5 = 0
        dtrEc!ec_scont6 = 0
        dtrEc!ec_flsc1mod = "N"
        dtrEc!ec_flsc2mod = "N"
        dtrEc!ec_flsc3mod = "N"
        dtrEc!ec_flsc4mod = "N"
        dtrEc!ec_flsc5mod = "N"
        dtrEc!ec_flsc6mod = "N"
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

  Public Overridable Function CalcolaPromozioni_MxNSpezza_AggiornaTcoMatricole(ByVal oPromozioni As NTSPromozioni, ByVal dtrOld As DataRow, ByVal dtrNew As DataRow) As Boolean
    Try
      If oPromozioni.dttEcTc IsNot Nothing Then CalcolaPromozioni_MxNSpezza_AggiornaDettaglioTco(oPromozioni, dtrOld, dtrNew)
      If oPromozioni.dttMovMatr IsNot Nothing Then CalcolaPromozioni_MxNSpezza_AggiornaDettaglioMatricole(oPromozioni, dtrOld, dtrNew)

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
  Public Overridable Function CalcolaPromozioni_MxNSpezza_AggiornaDettaglioTco(ByVal oPromozioni As NTSPromozioni, ByVal dtrOld As DataRow, ByVal dtrNew As DataRow) As Boolean
    Dim dtrTcOld, dtrTcNew As DataRow
    Dim dQtaOld As Decimal = 0
    Try
      dQtaOld = NTSCDec(dtrOld!ec_quant)

      Dim dtrTmp() As DataRow = oPromozioni.dttEcTc.Select("ec_riga = " & NTSCInt(dtrOld!ec_riga))
      If dtrTmp.Length = 0 Then Return True 'Non è gestito a Taglie e Colori
      dtrTcOld = dtrTmp(0)

      'Creo il dettaglio taglie per la nuova riga (per adesso lo clono dalla riga vecchio, poi le quantità le aggiorno con l'elaborazione successiva
      oPromozioni.dttEcTc.Rows.Add(dtrTcOld.ItemArray)
      dtrTcNew = oPromozioni.dttEcTc.Rows(oPromozioni.dttEcTc.Rows.Count - 1)
      dtrTcNew!ec_riga = dtrNew!ec_riga

      'Scorro tutte le taglie presenti della vecchia riga e deciso come spezzare le taglie sulla riga
      For i As Integer = 1 To 24
        Dim strColonna As String = "ec_quant" & i.ToString("00")
        If NTSCDec(dtrTcOld(strColonna)) = 0 Then Continue For

        If dQtaOld >= NTSCDec(dtrTcOld(strColonna)) Then
          'Quantità sufficiente sulla vecchia taglia
          dQtaOld -= NTSCDec(dtrTcOld(strColonna))
          dtrTcNew(strColonna) = 0
        ElseIf dQtaOld > 0 Then
          'Quantità non sufficiente, ma c'è ancora un resido, quindi la riga va spezzata
          dtrTcNew(strColonna) = NTSCDec(dtrTcOld(strColonna)) - dQtaOld
          dtrTcOld(strColonna) = NTSCDec(dtrTcOld(strColonna)) - NTSCDec(dtrTcNew(strColonna))
          dQtaOld = 0
        Else
          'Quantità non sufficiente e nessun residuo. Sposto tutto sulla nuova riga
          dtrTcNew(strColonna) = dtrTcOld(strColonna)
          dtrTcOld(strColonna) = 0
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
  Public Overridable Function CalcolaPromozioni_MxNSpezza_AggiornaDettaglioMatricole(ByVal oPromozioni As NTSPromozioni, ByVal dtrOld As DataRow, ByVal dtrNew As DataRow) As Boolean
    Dim dQtaOld As Decimal = 0
    Try
      dQtaOld = NTSCDec(dtrOld!ec_quant)

      'Scorro tutte le righe di matricole che ho trovato (se l'articolo non è gestito a matricole semplicemente non entra mai nel for)
      For Each dtrMatr As DataRow In oPromozioni.dttMovMatr.Select("mma_riga = " & NTSCInt(dtrOld!ec_riga))
        If dQtaOld >= NTSCDec(dtrMatr!mma_quant) Then
          'Quantità sufficiente sulla riga vecchia, la matricola non va spostata sulla riga nuova
          dQtaOld -= NTSCDec(dtrMatr!mma_quant)
        ElseIf dQtaOld > 0 Then
          'Quantità non sufficiente, ma c'è ancora un resido, la matricola va spezzata tra le 2 righe
          Dim dQtaMatr As Decimal = NTSCDec(dtrMatr!mma_quant)
          dtrMatr!mma_quant = dQtaOld
          oPromozioni.dttMovMatr.Rows.Add(dtrMatr.ItemArray)
          With oPromozioni.dttMovMatr.Rows(oPromozioni.dttMovMatr.Rows.Count - 1)
            dtrMatr!mma_riga = dtrNew!ec_riga
            dtrMatr!mma_rigaa = 1
            dtrMatr!mma_quant = dQtaMatr - dQtaOld
          End With

          dQtaOld = 0
        Else
          'Quantità non sufficiente e senza residuo, da spostare tutta la quantità sulla nuova riga
          dtrMatr!mma_riga = dtrNew!ec_riga
          dtrMatr!mma_rigaa = NTSCInt(oPromozioni.dttMovMatr.Compute("MAX(mma_rigaa)", "mma_riga = " & NTSCInt(dtrNew!ec_riga))) + oPromozioni.Parametri.nIncremContatoreRiga
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


  Public Overridable Function CalcolaPromozioni_PromozionePerAgente(ByVal strDittta As String, ByVal lPromo As Integer) As Boolean
    Try
      If lPromo = 0 Then Return False

      'Verifica se è una promozione per agente
      Dim dttPromo As New DataTable
      oCldComm.ValCodiceDb(lPromo.ToString, strDittta, "TABREPR", "N", , dttPromo)
      If dttPromo.Rows.Count = 0 Then Return False 'non dovrebbe capitare, ma se succede che la promozione non è codificata fa finta che sia per utente
      Select Case NTSCStr(dttPromo.Rows(0)!tb_tipopr)
        Case "W", "X", "Y", "Z" : Return True
      End Select

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
  Public Overridable Function CalcolaPromozioni_PromozioneImmediataPerAgente(ByVal strDitta As String, ByVal lPromo As Integer) As Boolean
    Try
      If lPromo = 0 Then Return False

      'Verifica se è una promozione immediata (non differita) per agente
      Dim dttPromo As New DataTable
      oCldComm.ValCodiceDb(lPromo.ToString, strDitta, "TABREPR", "N", , dttPromo)
      If dttPromo.Rows.Count = 0 Then Return False 'non dovrebbe capitare, ma se succede che la promozione non è codificata fa finta che sia per utente
      Select Case NTSCStr(dttPromo.Rows(0)!tb_tipopr)
        Case "W" : Return True
      End Select

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



  Public Overridable Sub CalcolaPromozioni_ColoreGrigliaPromozioni(ByVal oPromozioni As NTSPromozioni)
    Try
      'Scorre le promozioni e valuta se sono state applicate o meno, e assegna un colore alla riga
      For Each dtrPromo As DataRow In oPromozioni.dttPromo.Rows
        Dim lPromo As Integer = NTSCInt(dtrPromo!tb_codrepr)
        If oPromozioni.dttEc.Select("ec_codrepr1 = " & lPromo & " OR ec_codrepr2 = " & lPromo & _
                                " OR ec_codrepr3 = " & lPromo & " OR ec_codrepr4 = " & lPromo & _
                                " OR ec_codrepr5 = " & lPromo & " OR ec_codrepr6 = " & lPromo & _
                                " OR ec_codrepr1out = " & lPromo & " OR ec_codrepr2out = " & lPromo & _
                                " OR ec_codrepr3out = " & lPromo & " OR ec_codrepr4out = " & lPromo & _
                                " OR ec_codrepr5out = " & lPromo & " OR ec_codrepr6out = " & lPromo).Length = 0 Then
          dtrPromo!backcolor_row = -1
        Else
          dtrPromo!backcolor_row = Color.LightGreen.ToArgb
        End If
      Next
      oPromozioni.dttPromo.AcceptChanges()
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




  Public Overridable Function CalcolaOmaggiDaPromozioni(ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim dtrRow() As DataRow
    Dim strSelect As String = ""
    Dim j, x As Integer
    Dim lNumMaxRilasci As Integer
    Try
      If oPromozioni.dttOmaggi IsNot Nothing Then oPromozioni.dttOmaggi.Clear()
      'Si passa tutte le promozioni applicabili che ha trovato
      For Each dtrPromo As DataRow In oPromozioni.dttPromo.Rows
        With dtrPromo
          '"F" - maggio raggiunto valore "A" - Omaggio acquistando certi articoli (questi 2 vanno calcolati alla fine)
          If NTSCStr(!tb_tipopr) <> "F" And NTSCStr(!tb_tipopr) <> "A" Then Continue For

          'Seleziona le righe che potrebbero essere considerate valide per la promozione.
          CalcolaPromozioni_FiltroPromozione(dtrPromo, strSelect, oPromozioni)
          If strSelect.Trim <> "" Then strSelect &= " AND "

          strSelect &= " ec_tiporiga = 'N'"

          dtrRow = oPromozioni.dttEc.Select(strSelect)

          If dtrRow.Length = 0 Then Continue For

          Dim dValore As Decimal = 0

          Select Case NTSCStr(!tb_tipopr)
            Case "F" ' Omaggio raggiunto un certo valore
              dValore = 0
              For x = 0 To dtrRow.Length - 1
                If oPromozioni.Parametri.bDaGPV AndAlso NTSCDec(dtrRow(x)!xxo_valoreiva) > 0 Then
                  dValore += NTSCDec(dtrRow(x)!xxo_valoreiva)
                Else
                  dValore += AggiungiIva(NTSCDec(dtrRow(x)!ec_valore), NTSCInt(dtrRow(x)!ec_codiva))
                End If
              Next

              'Preparo la tabella per proporre le possibili promozioni da inserire
              If dValore >= NTSCDec(!tb_valmin) Then
                'Moltiplica le quantità dell'articolo omaggio per il numero di volte che si raggiunge il valore.
                'In caso sia impostato un massimo per documento -> prende il minore tra i 2 calcoli.
                If NTSCDec(!tb_valmin) = 0 Then
                  lNumMaxRilasci = 1 'Se il valore è minimo di acquisto è 0, considera di rilasciare sempre e solo 1 omaggio.
                Else
                  lNumMaxRilasci = NTSCInt(Math.Floor(dValore / NTSCDec(!tb_valmin)))
                End If
                If lNumMaxRilasci > NTSCInt(!tb_nprdoc) And NTSCInt(!tb_nprdoc) <> 0 Then lNumMaxRilasci = NTSCInt(!tb_nprdoc)

                If Not CalcolaOmaggiDaPromozioni_PreparaOmaggiDaProporre(dtrPromo, lNumMaxRilasci, oPromozioni) Then Continue For
              End If
            Case "A" ' Omaggio acquistato un set di articoli
              If NTSCDec(!tb_valmin) > 0 AndAlso NTSCDec(!tb_valmin) > CalcolaPromozioni_PromoTotaleDoc(oPromozioni) Then Continue For
              'è stato verificato che gli articoli sono presenti, ma se è una lista selezionata bisogna anche verificare che ci siano come quantità.
              'Negli altri casi invece basta che ci sia un articolo compatibile e lo prende da usare per la promozione
              Select Case NTSCInt(!tb_codlsar)
                Case 0
                  'Altrimenti per ogni articolo trovato propone l'omaggio
                  For x = 0 To dtrRow.Length - 1
                    dValore += NTSCDec(dtrRow(x)!ec_quant)
                  Next

                  lNumMaxRilasci = NTSCInt(Math.Floor(dValore))
                  If lNumMaxRilasci > NTSCInt(!tb_nprdoc) And NTSCInt(!tb_nprdoc) <> 0 Then lNumMaxRilasci = NTSCInt(!tb_nprdoc)

                  If Not CalcolaOmaggiDaPromozioni_PreparaOmaggiDaProporre(dtrPromo, lNumMaxRilasci, oPromozioni) Then Continue For
                Case Else
                  'Se è una lista selezionata devo controllare anche le quantità degli articoli da prelevare
                  'Prendo il rapporto minimo tra i vari articoli presenti e quelli della lista
                  Dim dttArtiLista As New DataTable
                  If Not oCldComm.ArticoliInListaSelezionata(oPromozioni.Parametri.strDitta, NTSCInt(!tb_codlsar), dttArtiLista) Then Return False

                  'Per ogni articolo trovato controllo che sia presente e in che rapporto è disponibile con la quantità presente nel documento
                  lNumMaxRilasci = 999999
                  For Each dtrArtiLista As DataRow In dttArtiLista.Rows
                    If NTSCDec(dtrArtiLista!lsa_esist) = 0 Then Continue For
                    dValore = 0

                    dtrRow = oPromozioni.dttEc.Select("ec_codart = " & CStrSQL(dtrArtiLista!lsa_codart) & " AND ec_tiporiga = 'N'")

                    For j = 0 To dtrRow.Length - 1
                      dValore += NTSCDec(dtrRow(j)!ec_quant)
                    Next

                    If lNumMaxRilasci > NTSCInt(Math.Floor(dValore / NTSCDec(dtrArtiLista!lsa_esist))) Then
                      lNumMaxRilasci = NTSCInt(Math.Floor(dValore / NTSCDec(dtrArtiLista!lsa_esist)))
                    End If
                  Next

                  'Se non è riuscito a determinare il numero di rilasci, allora propone sempre 1
                  If lNumMaxRilasci = 999999 Then lNumMaxRilasci = 1

                  'A questo punto ho il valore minimo di quanti omaggi devo lasciare
                  If lNumMaxRilasci > NTSCInt(!tb_nprdoc) And NTSCInt(!tb_nprdoc) > 0 Then lNumMaxRilasci = NTSCInt(!tb_nprdoc)

                  If Not CalcolaOmaggiDaPromozioni_PreparaOmaggiDaProporre(dtrPromo, lNumMaxRilasci, oPromozioni) Then Continue For
              End Select
          End Select
        End With
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
  Public Overridable Function CalcolaOmaggiDaPromozioni_PreparaOmaggiDaProporre(ByVal dtrPromo As DataRow, ByVal lNumMaxRilasci As Integer, _
                                                                                ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim dttTmp As New DataTable
    Dim dtrRow() As DataRow
    Dim dDistribuiti As Decimal
    Try
      With dtrPromo
        If NTSCStr(!tb_artomg).Trim <> "" Then
          'Carica i dati e rimpie il datatable che dovrà mostrare i dati nella griglia degli omaggi
          oCldComm.CaricaDatiOmaggioDaArticolo(oPromozioni.Parametri.strDitta, NTSCStr(!tb_artomg), False, dttTmp)

          If dttTmp.Rows.Count = 0 Then Return False

          'Deve escludere le quantità già importate per lo stesso articolo
          dtrRow = oPromozioni.dttEc.Select("ec_codart = " & CStrSQL(!tb_artomg) & " AND ec_tiporiga = 'O' AND ec_codrepr1out = " & NTSCInt(!tb_codrepr))
          If dtrRow.Length > 0 Then dDistribuiti = NTSCDec(dtrRow(0)!ec_quant)

          If oPromozioni.dttOmaggi.Rows.Count = 0 Then
            If NTSCDec(!tb_artqta) * lNumMaxRilasci <> dDistribuiti Then
              CalcolaOmaggiDaPromozioni_DuplicaDatatable(dttTmp, oPromozioni.dttOmaggi)
              Dim dtrOmaggio As DataRow = oPromozioni.dttOmaggi.Rows(0)
              dtrOmaggio!xx_quant = NTSCDec(!tb_artqta) * lNumMaxRilasci - dDistribuiti
              dtrOmaggio!xx_maxquant = dtrOmaggio!xx_quant
              dtrOmaggio!xx_codrepr = NTSCInt(!tb_codrepr)
              dtrOmaggio!xx_desrepr = NTSCStr(!tb_desrepr)
              dtrOmaggio!xx_tipo = !tb_tipopr

              CalcolaOmaggiDaPromozioni_AggiornaMassimoOmaggi(dtrPromo, dtrOmaggio, oPromozioni)
            End If
          Else
            If NTSCDec(!tb_artqta) * lNumMaxRilasci <> dDistribuiti Then
              oPromozioni.dttOmaggi.Rows.Add()
              Dim dtrOmaggio As DataRow = oPromozioni.dttOmaggi.Rows(oPromozioni.dttOmaggi.Rows.Count - 1)
              dtrOmaggio.ItemArray = dttTmp.Rows(0).ItemArray
              dtrOmaggio!xx_quant = NTSCDec(!tb_artqta) * lNumMaxRilasci - dDistribuiti
              dtrOmaggio!xx_maxquant = dtrOmaggio!xx_quant
              dtrOmaggio!xx_codrepr = NTSCInt(!tb_codrepr)
              dtrOmaggio!xx_desrepr = NTSCStr(!tb_desrepr)
              dtrOmaggio!xx_tipo = !tb_tipopr

              CalcolaOmaggiDaPromozioni_AggiornaMassimoOmaggi(dtrPromo, dtrOmaggio, oPromozioni)
            End If
          End If
        ElseIf NTSCInt(!tb_lsaromg) <> 0 Then
          oCldComm.CaricaDatiOmaggioDaLista(oPromozioni.Parametri.strDitta, NTSCInt(!tb_lsaromg), dttTmp)

          If dttTmp.Rows.Count = 0 Then Return False

          If oPromozioni.dttOmaggi.Rows.Count = 0 Then
            CalcolaOmaggiDaPromozioni_DuplicaDatatable(dttTmp, oPromozioni.dttOmaggi)

            For Each dtrOmaggi As DataRow In oPromozioni.dttOmaggi.Rows
              'Deve escludere le quantità già importate per lo stesso articolo
              dtrRow = oPromozioni.dttEc.Select("ec_codart = " & CStrSQL(dtrOmaggi!ar_codart) & " AND ec_tiporiga = 'O' AND ec_codrepr1out = " & NTSCInt(!tb_codrepr))
              If dtrRow.Length > 0 Then dDistribuiti += NTSCDec(dtrRow(0)!ec_quant)
            Next

            For Each dtrOmaggi As DataRow In oPromozioni.dttOmaggi.Rows
              If NTSCDec(dtrOmaggi!xx_quant) * lNumMaxRilasci <> dDistribuiti Then
                dtrOmaggi!xx_quant = NTSCDec(dtrOmaggi!xx_quant) * lNumMaxRilasci - dDistribuiti
                dtrOmaggi!xx_maxquant = dtrOmaggi!xx_quant
                dtrOmaggi!xx_codrepr = NTSCInt(!tb_codrepr)
                dtrOmaggi!xx_desrepr = NTSCStr(!tb_desrepr)
                dtrOmaggi!xx_tipo = !tb_tipopr

                CalcolaOmaggiDaPromozioni_AggiornaMassimoOmaggi(dtrPromo, dtrOmaggi, oPromozioni)
              Else
                dtrOmaggi.Delete()
              End If
            Next
          Else
            For Each dtrTmp As DataRow In dttTmp.Rows
              'Deve escludere le quantità già importate per lo stesso articolo
              dtrRow = oPromozioni.dttEc.Select("ec_codart = " & CStrSQL(dtrTmp!ar_codart) & " AND ec_tiporiga = 'O' AND ec_codrepr1out = " & NTSCInt(!tb_codrepr))
              If dtrRow.Length > 0 Then dDistribuiti += NTSCDec(dtrRow(0)!ec_quant)
            Next

            For Each dtrTmp As DataRow In dttTmp.Rows
              If NTSCDec(dtrTmp!xx_quant) * lNumMaxRilasci <> dDistribuiti Then
                oPromozioni.dttOmaggi.Rows.Add()
                Dim dtrOmaggio As DataRow = oPromozioni.dttOmaggi.Rows(oPromozioni.dttOmaggi.Rows.Count - 1)
                dtrOmaggio.ItemArray = dtrTmp.ItemArray
                dtrOmaggio!xx_quant = NTSCDec(dtrOmaggio!xx_quant) * lNumMaxRilasci - dDistribuiti
                dtrOmaggio!xx_maxquant = dtrOmaggio!xx_quant
                dtrOmaggio!xx_codrepr = NTSCInt(!tb_codrepr)
                dtrOmaggio!xx_desrepr = NTSCStr(!tb_desrepr)
                dtrOmaggio!xx_tipo = !tb_tipopr

                CalcolaOmaggiDaPromozioni_AggiornaMassimoOmaggi(dtrPromo, dtrOmaggio, oPromozioni)
              End If
            Next
          End If
        End If
      End With
      oPromozioni.dttOmaggi.AcceptChanges()

      'Le righe a 0 vanno eliminate
      For x As Integer = oPromozioni.dttOmaggi.Rows.Count - 1 To 0 Step -1
        With oPromozioni.dttOmaggi.Rows(x)
          If NTSCDec(!xx_quant) <= 0 Then
            .Delete()
          Else
            Select Case oPromozioni.Parametri.strOmaggiDesel
              Case "A" : If oPromozioni.dttOmaggi.Select("xx_tipo = " & CStrSQL(!xx_tipo) & " AND xx_sel = 'S'").Length = 0 Then !xx_sel = "S"
              Case "N" : !xx_sel = "N"
              Case "T" : !xx_sel = "S"
            End Select
          End If
        End With
      Next
      oPromozioni.dttOmaggi.AcceptChanges()

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
  Public Overridable Function CalcolaOmaggiDaPromozioni_AggiornaMassimoOmaggi(ByVal dtrPromo As DataRow, ByVal dtrOmaggio As DataRow, ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim dtrRow() As DataRow
    Try
      With dtrPromo
        'Controllo di non aver superato il massimo per cliente
        If oPromozioni.dsPromoOmaggi.Tables.Contains("C" & NTSCInt(!tb_codrepr)) Then
          If NTSCDec(dtrOmaggio!xx_quant) > NTSCDec(oPromozioni.dsPromoOmaggi.Tables("C" & NTSCInt(!tb_codrepr)).Rows(0)!QUANT) Then
            dtrRow = oPromozioni.dsPromoOmaggi.Tables("C" & NTSCInt(!tb_codrepr)).Select("mm_codart = " & CStrSQL(dtrOmaggio!ar_codart))
            If dtrRow.Length > 0 Then
              If NTSCDec(dtrOmaggio!xx_quant) > NTSCDec(dtrRow(0)!QUANT) Then dtrOmaggio!xx_quant = dtrRow(0)!QUANT
            End If
          End If
        End If
        'Controllo di non aver superato il massimo per documento
        If oPromozioni.dsPromoOmaggi.Tables.Contains("P" & NTSCInt(!tb_codrepr)) Then
          If NTSCDec(dtrOmaggio!xx_quant) > NTSCDec(oPromozioni.dsPromoOmaggi.Tables("P" & NTSCInt(!tb_codrepr)).Rows(0)!QUANT) Then
            dtrRow = oPromozioni.dsPromoOmaggi.Tables("P" & NTSCInt(!tb_codrepr)).Select("mm_codart = " & CStrSQL(dtrOmaggio!ar_codart))
            If dtrRow.Length > 0 Then
              If NTSCDec(dtrOmaggio!xx_quant) > NTSCDec(dtrRow(0)!QUANT) Then dtrOmaggio!xx_quant = dtrRow(0)!QUANT
            End If
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
  Public Overridable Sub CalcolaOmaggiDaPromozioni_DuplicaDatatable(ByVal dttFrom As DataTable, ByVal dttTo As DataTable)
    Try
      'Per duplicare un datatable senza perdere il riferimento all'oggetto originale
      If dttTo.Columns.Count > 0 Then
        dttTo.Clear()
        dttTo.Columns.Clear()
      End If

      For Each oColumn As DataColumn In dttFrom.Columns
        dttTo.Columns.Add(oColumn.ColumnName)
      Next

      For Each dtrRow As DataRow In dttFrom.Rows
        dttTo.Rows.Add(dtrRow.ItemArray)
      Next

      dttTo.AcceptChanges()
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

  Public Overridable Function AggiungiArticoliOmaggio(ByVal oPromozioni As NTSPromozioni) As Boolean
    Dim lStart As Integer = 0
    Dim dtrRow(), dtrRowEc() As DataRow
    Try
      'Le prime righe le riserva agli sconti di piede a valore o %, poi mette gli articoli omaggio
      With oPromozioni
        For i As Integer = 0 To .dttEc.Rows.Count - 1
          If NTSCStr(.dttEc.Rows(i)!ec_codart) = "D" AndAlso (NTSCStr(.dttEc.Rows(i)!ec_tiporiga) = "S" OrElse _
                                                              NTSCStr(.dttEc.Rows(i)!ec_tiporiga) = "O") Then Continue For
          lStart = i
          Exit For
        Next

        'Sono gli omaggi che sono stati selezionati dal commesso, e che quindi devono essere dati al cliente
        dtrRow = .dttOmaggi.Select("xx_sel = 'S' AND xx_quant > 0")
        For i As Integer = 0 To dtrRow.Length - 1
          'Se ci sono già delle righe relative allo stesso articolo omaggio, incremento quelle
          dtrRowEc = .dttEc.Select("ec_codart = " & CStrSQL(dtrRow(i)!ar_codart) & " AND ec_tiporiga = 'O' AND ec_codrepr1out = " & NTSCInt(dtrRow(i)!xx_codrepr) & CalcolaPromozioni_FiltroEvasione(oPromozioni)) 'Gli articoli omaggio non sono presenti nelle promozioni per agente, quindi va bene il filtro completo.
          If dtrRowEc.Length = 0 Then
            .dttEc.Rows.InsertAt(.dttEc.NewRow, lStart)
            Dim dtrT() As DataRow = .dttEc.Select("MAX(ec_riga) = ec_riga")
            .dttEc.Rows(lStart)!codditt = "."
            .dttEc.Rows(lStart)!codditt = oPromozioni.Parametri.strDitta
            .dttEc.Rows(lStart)!ec_riga = NTSCInt(dtrT(0)!ec_riga) + .Parametri.nIncremContatoreRiga
            .dttEc.Rows(lStart)!ec_codart = dtrRow(i)!ar_codart
            .dttEc.Rows(lStart)!ec_quant = dtrRow(i)!xx_quant
            .dttEc.Rows(lStart)!ec_stasino = .Parametri.strStampaRigaOmaggi
            If .Parametri.lCodivaOmaggi <> 0 Then .dttEc.Rows(lStart)!ec_codiva = .Parametri.lCodivaOmaggi
            If Not oPromozioni.Parametri.bdaOfferta Then
              .dttEc.Rows(lStart)!ec_codrepc = .Parametri.strCodRepc
              .dttEc.Rows(lStart)!ec_opnome = oApp.User.Nome ' strOperatore: problemi di gestione con le varie opzioni in pers retail per cambiare commesso al volo
            End If
            .dttEc.Rows(lStart)!ec_tiporiga = "O"
            CalcolaPromozioni_AggiornaPromozioneDiRigaOut(NTSCInt(dtrRow(i)!xx_codrepr), .dttEc.Rows(lStart))

            lStart += 1
          Else
            dtrRowEc(0)!ec_quant = NTSCDec(dtrRowEc(0)!ec_quant) + NTSCDec(dtrRow(i)!xx_quant)
          End If

          If dtrRow.Length > 0 Then ThrowRemoteEvent(New NTSEventArgs("CalcolaTotali", ""))
        Next
      End With

      CalcolaPromozioni_ColoreGrigliaPromozioni(oPromozioni)

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


  Dim dttCiva As New DataTable
  Public Overridable Function AliquotaIva(ByVal lCodIva As Integer) As Decimal
    Dim dtrTmp As DataRow
    Dim dttTmp As New DataTable
    Try
      If lCodIva = 0 Then Return 0
      'Non ha codici caricati, carica il primo che gli viene passato
      If dttCiva.Rows.Count = 0 Then
        oCldComm.ValCodiceDb(lCodIva.ToString, "", "TABCIVA", "N", , dttTmp)
        dttCiva = dttTmp
        dttCiva.PrimaryKey = New DataColumn() {dttCiva.Columns("tb_codciva")}
      End If

      'Controlla solo nella tabella temporanea e non sul db
      dtrTmp = dttCiva.Rows.Find(lCodIva)

      'Se non è stato ancora caricato, legge il nuovo codice
      If dtrTmp Is Nothing Then
        oCldComm.ValCodiceDb(lCodIva.ToString, "", "TABCIVA", "N", , dttTmp)
        dttCiva.Rows.Add(dttTmp.Rows(0).ItemArray)
        dtrTmp = dttCiva.Rows.Find(lCodIva)
      End If

      If Not dtrTmp Is Nothing Then Return NTSCDec(dtrTmp!tb_aliq)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return 0
  End Function
  Public Overridable Function AggiungiIva(ByVal dPrezzo As Decimal, ByVal lCodIva As Integer) As Decimal
    Try
      Return dPrezzo + ArrDbl((dPrezzo * AliquotaIva(lCodIva) / 100), oApp.NDecImporti)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return 0
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ScorporaIva(ByVal dPrezzo As Decimal, ByVal lCodIva As Integer, ByRef dIva As Decimal) As Decimal
    Dim dImponib, dAliq As Decimal
    Dim nDec As Integer = oCldComm.TrovaNdec(0)
    Try
      dAliq = AliquotaIva(lCodIva)

      If dAliq = 0 Then
        dImponib = dPrezzo
        dIva = 0
      Else
        dPrezzo = ArrDbl(dPrezzo, nDec)
        dImponib = ArrDbl((dPrezzo * 100) / (100 + dAliq), nDec)
        dIva = dPrezzo - dImponib
      End If

      Return dImponib
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return 0
    End Try
  End Function
#End Region

#Region "Attivazione moduli Friendly"

  Public Overridable Sub AttivaModuliFriendly()
    Try
      If Not FRIENDLY Then Return

      oCldComm.AttivaModuliFriendly()
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
End Class
