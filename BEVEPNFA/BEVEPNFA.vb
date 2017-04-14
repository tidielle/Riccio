Imports System.Data
Imports NTSInformatica.CLN__STD

'ATTENZIONE: chiamato da BNVEPNFA, BNREPNCO, BNPAPNPA

Public Class CLEVEPNFA
  Inherits CLE__BASN

  Private Moduli_P As Integer = bsModMG + bsModVE + bsModCG
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtPAR
  Private ModuliSup_P As Integer = bsModSupGPV
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

  Public oCldPnfa As CLDVEPNFA

  Public bInElaborazione As Boolean = False

  'parametri passati dal chiamante
  Public nEscomp As Integer = 0
  Public strTiporks As String = ""
  Public strDatregForm As String = ""
  Public bDerogaIva As Boolean = False
  Public strDtcomivaForm As String = ""
  Public bAutorizzato As Boolean = False
  Public bRielab As Boolean = False
  Public bIncassi As Boolean = False
  Public bCa As Boolean = False
  Public nAnnoDoc As Integer = 0
  Public strDatdocDa As String = ""
  Public strDatdocA As String = ""
  Public strSeriedocDa As String = ""
  Public strSeriedocA As String = ""
  Public lNumdocDa As Integer = 0
  Public lNumdocA As Integer = 0
  Public nTipobf As Integer = 0
  Public bInt As Boolean = False
  Public bContIncDDT As Boolean = False         'se true, solo per ddt emessi non deve contabilizz il ddt ma solo il relativo incasso
  Public bContCompensDDT As Boolean = False     'se true solo in fatt diff emesse la parte incassata non deve essere rilevata cone incasso, ma come storno di acconti precedentemente registrati (con contab solo incassi ddt em)
  Public bContCompensNoteAcc As Boolean = False 'se true solo in fatt diff emessa nella reg. di incasso deve essere rilevato l'eventuale storno di note di accred precedentemente emesse'
  Public bBnrepnco As Boolean = False             'se TRUE sono stato chiamato da BNREPNCO
  Public bCheckProtocolli As Boolean = False

  'Opzioni di registro
  Public strOpz19 As String = ""
  Public bAlfproRicFromTabnuma As Boolean = False    'se true, per i documenti ricevuti la serie protocollo non viene letta dalla serie fatture/note accred/note addeb indicate in anagrafica ditta->registri IVA ma dalla tabella numerazioni
  Public bGestPVR As Boolean = False
  Public bModuliAcquistati As Boolean = False
  Public bForzaCG As Boolean = False              'se TRUE, anche se non c'è modulo CG esegue le registrazioni in modo standard
  Public strUsaContoFattDoc As String = ""        'se 1 o 2 in prima nota non riporta tm_conto ma tm_contfatt se <> 0, altrimenti tm_conto
  Public bUsaContoFatt As Boolean = False
  Public bUsaNumdocPerNumpar As Boolean = False
  Public bCeas As Boolean = False
  Public bGirocontoIvaIndedRipartito As Boolean = False
  Public bGestDatecomp As Boolean = False
  Public bRidCodClieDebTurnoDest As Boolean = False
  Public bRiferimFattureinCG As Boolean = False
  Public strRiferimInDescr As String = ""       'elenco di tipirk, separati da , (es A, B, C) per i quali tm_riferim deve essere riversato in pn_descr e sc_descr

  Public strDataPlafondProposta As String = "D"

  Public bGestStanziamenti As Boolean = False
  Public strOldgesived As String = "N"
  Public strDtullg As String = ""
  Public strDtulap As String = ""       'inutilizzata
  Public strDtulaca As String = ""
  Public strDtulliqi As String = ""
  Public strDtineser As String = ""
  Public strDtfieser As String = ""
  Public strTipodoc As String = ""
  Public bTestBudget As Boolean = True
  Public bCaDcRaggruppaCorr As Boolean = True
  Public bCollega_MG_DI As Boolean = False
  Public strNaturaOperazDaCodiva As String = " "  'se ' ' dipende dal modulo:se abilitato modulo telematico prende dal cod iva, else da tipobf (come è sempre stato), se 'S' prende sempre dal cod. iva, se 'N' prende sempre da tipobf

  Public lContoIVASplitPayment As Integer = 0
  Public strContoIVASplitPayment As String = ""

  'estremi registrazioni di prima nota da scrivere
  Public strDatreg As String = ""
  Public lNumregFatt As Integer = 0
  Public lNumregOmaggi As Integer = 0
  Public lNumregIncasso As Integer = 0
  Public lNumregStanziamenti As Integer = 0
  Public lNumreg As Integer = 0
  Public lRiga As Integer = 0
  Public lRigaivanew As Integer = 0
  ' nuovi campi
  Public lRigaiva As Integer = 0 ' d'appoggio, sempre =0
  Public strAlfpro As String
  Public strAlfpro2 As String ' sul 2.o registro
  Public lNewnumprot2 As Integer ' sul secondo registro
  Public lNewnumprot As Integer
  Public dTotiva As Decimal
  Public dTotivav As Decimal
  Public dTotivaRC As Decimal       'totale iva di codici con flag 'reverse charge'
  Public dTotivavRC As Decimal      'totale iva di codici con flag 'reverse charge'
  Public dIvaInded As Decimal
  Public dIvaIndedVal As Decimal

  Public lRkfatt As Integer
  Public lRkomag As Integer
  Public lRkincas As Integer
  Public dTdare As Decimal
  Public dTavere As Decimal
  Public lNumRegCorr As Integer   'numero registrazione in fase di creazione
  Public lNumRegTot As Integer    'numero totale di registrazioni da creare

  ' comuni alle varie routine
  Public strDatdoc As String, strAlfdoc As String, strAlfpar As String
  Public nAnnpar As Integer, lNumdoc As Integer, lNumpar As Integer
  Public nCodntra As Integer  ' se <> 0 -> fatt. cee
  Public strFattsosp As String = "N"
  Public strFattXtracee As String = "N"       'se = S è una fattura/nota accred di acquisto extracee
  Public strFattRevCharge As String = "N"       'se = S è una fattura/nota accred di acquisto totalmente reverse charge, M = documento misto con rk parte reverse charge parte no

  Public strAutotr As String
  Public nMemnregiva As Integer
  Public nNregiva As Integer, nCausale As Integer, nCodIva As Integer
  Public lNumprot As Integer
  Public strTiponuma As String, strMemtregiva As String
  Public strTmptregiva, strCautregiva As String, strDAConto As String
  Public strDAControp As String, strDAIva As String, strDAContoinc As String
  Public strDACassainc As String, strDAOmagom As String, strDAContoom As String
  Public lOmagom As Integer, lContoiva As Integer
  Public strCautregiva2 As String, lContoiva2 As Integer, nCaunumregi2 As Integer ' per fatt. iva cee
  Public strGestdtciva As String
  Public strTdociva As String ' Tipo doc. iva (0=nessuno/rel8 1=fattura/storno+ 2=nota accredito/storno- 3=nota addebito 4=corrisp. 5=reso corr. 6=mov.reg.Iva+ 7 Mov.reg.Iva-)
  Public strDescauc As String

  'per ric. fiscali, utili anche nelle rouine chiamate ...
  Public strPrestserv As String ' S/N da tabtpbf
  Public nCausaleTpbf As Integer = 0  'causale di CG memorizzata in TABTPBF (ha prevalenza rispetto a causale di peve/peac se <> 0)
  Public bTotinc As Boolean ' tot incassato
  Public bParzinc As Boolean ' parz. incassato
  Public bNotinc As Boolean ' non incassato
  Public dImpincassato As Decimal, dImpnotincassato As Decimal
  Public dImpincassatov As Decimal, dImpnotincassatov As Decimal
  Public lContoPrimaRiga As Integer
  Public strTipsogivaContoPrimaRiga As String = ""

  'rilevate da tabpeve, tabpeac, tabpecg, tabpepa
  Public nAggControp As Integer = 0      'anagra.an_agcontrop
  Public nCaustom As Integer
  Public nCaustomac As Integer
  Public nCauemft As Integer, nCaunoac As Integer, nCauemfs As Integer, nCaunoaf As Integer
  Public nCauinc As Integer, nCaucorr As Integer
  Public lConribo As Integer, lConriac As Integer, lConriin As Integer
  Public lConcauz As Integer, lConomag As Integer, lConabat As Integer, lConabpa As Integer
  Public lConimba As Integer
  Public nCauemftat As Integer, nCaunoacat As Integer ' autotrasporto
  Public lConclpriv As Integer, lConcrrfc As Integer, lConcrrfp As Integer, lConivrfp As Integer
  Public lConcas As Integer
  Public lConcas2 As Integer
  Public lConcasPeve As Integer
  Public nCaustib As Integer    'causale storno acconti su ddt emessi (utilizzata solo da fatt diff emesse)
  Public nPeveIvainc As Integer = 0
  Public nPeveIva15 As Integer = 0
  Public nPeacIvainc As Integer = 0
  Public nPeacIva15 As Integer = 0

  Public nCaunoacsf As Integer = 0    'causale nota accred storno fattura (parcellazione)
  Public nCausNotul As Integer = 0    'causale emissione notule (parcellazione)

  Public lConRitenut As Integer = 0    'sottoconto ritenuta (parcellazione)
  Public lConAltriprev As Integer = 0  'sottoconto enasarco (parcellazione)

  ' da anaditpa i codici per gestire cassa commercialisti e spese generali
  Public lConspegen As Integer = 0  'sottoconto spese generali
  Public lConcascom As Integer = 0  'sottoconto cassa commercialisti
  Public strCodSpegen As String = ""
  Public strCodCascom As String = ""

  ' da peac
  Public nCaurcft As Integer, nCaurcftcee As Integer, nCaurcna As Integer, nCaupag As Integer
  Public lAconribo As Integer, lAconriac As Integer, lAconriin As Integer, lAconcas As Integer
  Public lAconabat As Integer, lAconabpa As Integer, lAconcauz As Integer, lAconomag As Integer
  Public lAconimba As Integer, nCaurcnacee As Integer, nCaurffs As Integer
  Public nCaurcftXcee As Integer = 0      'ricevuta fattura extracee
  Public nCaurcnaXcee As Integer = 0      'ricevuta nota accred extracee
  Public nCaurcfrc As Integer = 0     'fatt acq reverse charge
  Public nCaurcarc As Integer = 0     'nota accred acq reverse charge
  ' da pecg
  Public lCondaconv As Integer, lCondfconv As Integer ' diff att e passive di conv
  Public lDiffAttCambi As Integer   'sottoconto differenze attive su cambi  (tb_condcat)
  Public lDiffPasCambi As Integer   'sottoconto differenze passive su cambi (tb_condcpa)
  Public nCauststanz As Integer    'causale per storno stanziamenti
  Public lConEff As Integer           'conto EFFETTI ATTIVI
  Public lConRicSosp As Integer = 0   'conto ricavi sospesi (parcellazione)
  Public lConClinot As Integer = 0    'sottoconto clienti notule (parcellazione)
  Public lConTranot As Integer = 0    'sottoconto conto transitorio notule (parcellazione)

  ' da peve per ric. fiscali
  Public nCaurfincc As Integer, nCaurfparc As Integer, nCaurfnonc As Integer, nCaurfriec As Integer
  Public nCaurfincp As Integer, nCaurfparp As Integer, nCaurfnonp As Integer, nCaurfriep As Integer

  ' da tabduri per il tipo/num registro iva princiaple
  Public strTipoprot As String ' "0"=libero-svincolati "1" = coincidenti (vecchio default per le vendite)
  Public strSerfatt As String ' serie protocollo fatture
  Public strSernoac As String ' serie protocollo note accredito
  Public strSernoad As String ' serie protocollo addebito
  Public strTiponume As String ' 0 =nessun controllo 1=una serei unica di protocollo, 2 =2 serie 3=tre serie
  ' da tabduri per il tipo/num registro iva secondario
  Public strTipoprot2 As String ' "0"=libero-svincolati "1" = coincidenti (vecchio default per le vendite)
  Public strSerfatt2 As String ' serie protocollo fatture
  Public strSernoac2 As String ' serie protocollo note accredito
  Public strSernoad2 As String ' serie protocollo addebito
  Public strTiponume2 As String ' 0 =nessun controllo 1=una serei unica di protocollo, 2 =2 serie 3=tre serie

  Public Const nMaxControp As Integer = 150
  Public dttControp As New DataTable    'contiene le contropartite della fattura 

  'datatable temporanei per le registrazioni di prima nota
  Public dttPN As New DataTable  'prinot
  Public dttMO As New DataTable  'moviva
  Public dttSC As New DataTable  'scaden  per i record che devo inserire e/o aggiornare
  Public dttCA As New DataTable  'priana vecchia analitica
  Public dttCA2 As New DataTable 'priana nuova analitica (duplice contabile)

  Public bCa2 As Boolean = False  'nuova analitica? (letto da modulidittaditt)
  Public dttPecx As New DataTable
  Public dttAnagcaPecx As New DataTable 'contiene anagca deic conti di dttpecx

  Public dttPeve As New DataTable
  Public dttPeac As New DataTable
  Public dttPecg As New DataTable

  Public strEstremidoc As String = ""
  Public Const bsVEBOLLmodifCastScad As Integer = 1

  Public bGiroEffettiNoChisCli As Boolean = False

  Public strPrgParent As String = "BNVEPNFA"
  Public bProfes As Boolean = False       'true se ditta professionista
  Public nWriPnTipologia As Integer = 0       'invece di passare nuovi parametri allla wripn per la parcellazione ...
  Public bWripnIncassato As Boolean = False   'invece di passare nuovi parametri allla wripn per la parcellazione ...

  Public dsTabciva As New DataSet             'clone di TABCIVA

  Public lDiffMaxCgCa As Integer = 0
  Public bRegIvaContropRiga1 As Boolean = False       'se TRUE, in doc. iva imposta pn_controp della riga del cli/forn con il primo conto della riga di castelletto controp con pn_riga > 0
  Public bRevChargeNumdocDaPartita As Boolean = False 'se TRUE, nei documenti reverse charge, il numero documento autofattura viene preso dal numero partita (se diverso da 0 e sul registro vendite il numero doc e il n. protocolli siano gestiti svincolati)
  Public bDocIntraNumreg2DaTabnuma As Boolean = False 'se TRUE; per i odcumenti di acquisto intra/rsm il numero del reg. IVA 2 (vendite) non viene preso fisso da tabcauc, ma prevale il num. di registro indicato in tabnuma doc. emesso per la serie = alla serie doc in contabilizzazione

  Public bUsaAggContropAnagra As Boolean = False      'se TRUE, nell contropartite lette da BNVEPEVE e BNMGPEAC viene aggiunto il campo anagra.an_agcontrop e se il sottocont esiste viene preo quello, altrienti quello senza an_agcontrop
  Public dsCove As New DataSet

  Public bDelEffettiNoPres As Boolean = False       'rielaborazione documenti: cancella l'emissione effetti NON PRESENTATI prima di contabilizzare la fattura
  Public bContabAncheConScadSald As Boolean = False 'rielaborazione documenti: contabilizza anche in presenza di scadenze già saldate
  Public bGenEffetti As Boolean = False             'se true dopo aver creato la reg. di CG genero gli effetti (solo per tipork A, B, D)
  Public bAggProvvig As Boolean = False             'se true chiamo BEPRGNPV per estrearre/riestrarre le provvigioni
  Public bFattDiffCompDdtPartita As Boolean = False 'se TRUE, in reg. fatture differite emesse, la compensazione con acconti da DDT deve far perdere la partita degli acconti (OPERA SOLO SU CONTI GESTITI A PARTITE)

  Public bMovCassa As Boolean = False

  'per la gestione del pro-rata direttamente su ogni documento
  Public strProrata As String = "N"        'contiene il valore di tabatti.tb_prorata. Se = 'P' il pro-rata deve venir rilevato direttamente sul documento come % da aggiungere a % indeducib. indicata in tabciva
  Public dPerProrata As Decimal = 0        '% di indeducibilità come da tabatti.tb_aliqesen

  'per generazione effetti
  Public oCleGnef As CLECGGNEF = Nothing
  Public nCausaleEff As Integer = 0
  Public lContoEff As Integer = 0
  Public dImpMinEff As Decimal = 0
  Public strDescaucEff As String = ""

  'per aggiornare le provvigioni
  Public oCleGnpv As CLEPRGNPV = Nothing

  Public bAutotrasportatore As Boolean = False
  Public nCADoc As Integer = 0  'vecchia CA: se nel corpo del documento sono presenti tutti record con dare/avere MG, anche se spuntato di contabilizzare la CA non faccio la contabilizzazione
  '                                          0 = non impostato, 1 = con questa contropartita tutti i record sono dare/avere MG, 2 = record dare/avere CG, 3 = alcuni record dare/avere MG ed altri dare/avere CG

  'estremi IVA diferita nuovo sistema per nota accred a storno fattura
  Public strIEDDatreg As String = ""
  Public lIEDNumreg As Integer = 0
  Public nIEDRigareg As Integer = 0



  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDVEPNFA"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldPnfa = CType(MyBase.ocldBase, CLDVEPNFA)
    oCldPnfa.Init(oApp)


    '---------------------
    ' cerca se bsModCG o bsModExtCGE sono abilitati
    Dim strModuli As String = ""
    Dim strModuliExt As String = ""

    bModuliAcquistati = oCldPnfa.GetModuliAcquistati(strDittaCorrente)
    If oApp.ActKey.MultiKey = "N" Then  'non faccio il controllo sulla chiave di attivazione se ho chiave principale e secondaria, visto che sulla secondaria potrei non avere il modulo CG ma sulla principale si
      If bModuliAcquistati Then
        'controllo se nella chiave di attivazione sono disponibili
        strModuli = GetSettingReg("Business", oApp.Profilo & "\ActKey", "Moduli", "")
        strModuliExt = GetSettingReg("Business", oApp.Profilo & "\ActKey", "ModuliExt", "")
        If Left(strModuli, 1) = "S" Or Mid(strModuliExt, 11, 1) = "S" Then
          'ho la CG o la CG easy
        Else
          'non ho la CG
          bModuliAcquistati = False
        End If
      End If
    End If

    Return True
  End Function

  Public Overridable Function edEscomp_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Dim dttTmp As New DataTable
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldPnfa.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABESCO", "N", strDescr, dttTmp)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128765931591738000, "Esercizio contabile |'" & nCod.ToString & "'| inesistente")))
        Return False
      Else
        strDtineser = NTSCDate(dttTmp.Rows(0)!tb_dtineser).ToShortDateString
        strDtfieser = NTSCDate(dttTmp.Rows(0)!tb_dtfieser).ToShortDateString
      End If

      'test presente in bnpapnpa utile anche per bnvepnfa ...
      If oCldPnfa.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABANAZ", "N", "", dttTmp) Then
        If nCod <> NTSCInt(dttTmp.Rows(0)!tb_escomp) And nCod <> NTSCInt(dttTmp.Rows(0)!tb_escompp) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129648685987617188, "La ditta corrente " & vbCrLf & _
                          "ha come esercizio corrente: |'" & NTSCInt(dttTmp.Rows(0)!tb_escomp).ToString & "'|" & vbCrLf & _
                          "e come esercizio precedente: |'" & NTSCInt(dttTmp.Rows(0)!tb_escompp).ToString & "'|" & vbCrLf & _
                          "mentre l'esercizio di competenza indicato è |'" & nCod.ToString & "'|." & vbCrLf & _
                          "Ripetere l'inserimento.")))
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
  Public Overridable Function edTipobf_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldPnfa.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABTPBF", "N", strDescr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128765931044222000, "Tipo bolla/fattura |'" & nCod.ToString & "'| inesistente")))
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

#Region "Batch"
  Public Overridable Function CaricaControlli() As Boolean
    Dim r1 As System.IO.StreamReader = Nothing
    Dim strT() As String = Nothing
    Dim strDitta As String = ""
    Try
      'modello del file:
      'int=N
      'escomp = 2009
      'datreg=
      'tipork=A;D;N
      'ckDataIva = 0
      'dativa = 31/1/06
      'ckNoAutoriz = -1
      'annodoc = 2006
      'dadata = [#DATA ODIERNA-15]
      'adata = [#DATA ODIERNA-7]
      'daserie=
      'aserie = Z
      'danumero = 0
      'anumero = 999999999
      'tipobf = 0
      'ckRielab = -1
      'ckDelEffettiNoPres = -1
      'ckContabAncheConScadSald = -1
      'ckIncassi = 0
      'ckCa = -1
      'ckContIncDDT = 0
      'ckContCompensDDT = 0
      'ckContCompensNoteAcc = 0
      'ckCheckProtocolli = 0
      'bBnrepnco = 0
      'prgParent=BNVEPNFA
      'ckGenEffetti = -1
      'ckAggProvvig = -1

      r1 = New System.IO.StreamReader(oApp.AvvioProgrammaParametri)
      Do While Not r1.EndOfStream
        strT = r1.ReadLine().Split("="c)
        Select Case strT(0).ToLower
          Case "int" : bInt = CBool(IIf(strT(1) = "S", True, False))
          Case "escomp" : nEscomp = NTSCInt(ResolveCTRL(strT(1)))
          Case "datreg" : strDatregForm = ResolveCTRL(strT(1))
          Case "tipork" : strTiporks = strT(1)
          Case "ckdataiva" : bDerogaIva = CBool(strT(1))
          Case "dativa" : strDtcomivaForm = ResolveCTRL(strT(1))
          Case "cknoautoriz" : bAutorizzato = CBool(strT(1))
          Case "annodoc" : nAnnoDoc = NTSCInt(ResolveCTRL(strT(1)))
          Case "dadata" : strDatdocDa = ResolveCTRL(strT(1))
          Case "adata" : strDatdocA = ResolveCTRL(strT(1))
          Case "daserie" : strSeriedocDa = strT(1)
          Case "aserie" : strSeriedocA = strT(1)
          Case "danumero" : lNumdocDa = NTSCInt(strT(1))
          Case "anumero" : lNumdocA = NTSCInt(strT(1))
          Case "tipobf" : nTipobf = NTSCInt(strT(1))
          Case "ckrielab" : bRielab = CBool(strT(1))
          Case "ckDelEffettiNoPres" : bDelEffettiNoPres = CBool(strT(1))
          Case "ckContabAncheConScadSald" : bContabAncheConScadSald = CBool(strT(1))
          Case "ckincassi" : bIncassi = CBool(strT(1))
          Case "ckca" : bCa = CBool(strT(1))
          Case "ckContIncDDT" : bContIncDDT = CBool(strT(1))
          Case "ckContCompensDDT" : bContCompensDDT = CBool(strT(1))
          Case "ckContCompensNoteAcc" : bContCompensNoteAcc = CBool(strT(1))
          Case "ckCheckProtocolli" : bCheckProtocolli = CBool(strT(1))
          Case "bBnrepnco" : bBnrepnco = CBool(strT(1))
          Case "ckMovCassa" : bMovCassa = CBool(strT(1))
          Case "prgParent" : strPrgParent = strT(1)
          Case "ckGenEffetti" : bGenEffetti = CBool(strT(1))
          Case "ckAggProvvig" : bAggProvvig = CBool(strT(1))
        End Select
      Loop

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
      r1.Close()
    End Try
  End Function
  Public Overridable Function ResolveCTRL(ByVal strIn As String) As String
    Dim dtTemp As DateTime

    Try
      '--------------------------------------------------------------------------------------------------------------
      ResolveCTRL = strIn
      '--------------------------------------------------------------------------------------------------------------
      Select Case strIn
        Case "[#ANNO ATTUALE]"
          ResolveCTRL = DateTime.Now.Year.ToString
        Case "[#ANNO SCORSO]"
          dtTemp = DateAdd("yyyy", -1, DateTime.Now)
          ResolveCTRL = dtTemp.Year.ToString
        Case "[#ANNO CORRENTE]"
          ResolveCTRL = DateTime.Now.Year.ToString
        Case "[#ANNO CORRENTE-1]"
          dtTemp = DateAdd("yyyy", -1, DateTime.Now)
          ResolveCTRL = dtTemp.Year.ToString
        Case "[#ANNO CORRENTE+1]"
          dtTemp = DateAdd("yyyy", 1, DateTime.Now)
          ResolveCTRL = dtTemp.Year.ToString
        Case "[#MESE ATTUALE]"
          ResolveCTRL = DateTime.Now.Month.ToString
        Case "[#MESE SCORSO]"
          dtTemp = DateAdd("m", -1, DateTime.Now)
          ResolveCTRL = dtTemp.Month.ToString
        Case "[#DATA ODIERNA]"
          ResolveCTRL = DateTime.Now.ToShortDateString
        Case "[#DATA ODIERNA+1M]"
          ResolveCTRL = DateAdd("m", 1, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA+2M]"
          ResolveCTRL = DateAdd("m", 2, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA+3M]"
          ResolveCTRL = DateAdd("m", 3, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA+4M]"
          ResolveCTRL = DateAdd("m", 4, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA+5M]"
          ResolveCTRL = DateAdd("m", 5, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA-7]"
          ResolveCTRL = DateAdd("d", -7, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA-15]"
          ResolveCTRL = DateAdd("d", -15, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA-30]"
          ResolveCTRL = DateAdd("m", -1, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA-60]"
          ResolveCTRL = DateAdd("m", -2, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA-90]"
          ResolveCTRL = DateAdd("m", -3, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA-120]"
          ResolveCTRL = DateAdd("m", -4, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA-150]"
          ResolveCTRL = DateAdd("m", -5, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA-180]"
          ResolveCTRL = DateAdd("m", -6, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA-210]"
          ResolveCTRL = DateAdd("m", -7, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA-240]"
          ResolveCTRL = DateAdd("m", -8, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA-270]"
          ResolveCTRL = DateAdd("m", -9, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA-300]"
          ResolveCTRL = DateAdd("m", -10, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA-330]"
          ResolveCTRL = DateAdd("m", -11, DateTime.Now).ToShortDateString
        Case "[#DATA ODIERNA-360]"
          ResolveCTRL = DateAdd("m", -12, DateTime.Now).ToShortDateString
      End Select

    Catch ex As Exception

      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return strIn
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function
#End Region

  Public Overridable Function TestPreElabora() As Boolean
    Dim dttAnaz As New DataTable
    Dim evnt As NTSEventArgs = Nothing
    Dim dttTabduri As New DataTable
    Dim dtTmp As DateTime = Nothing
    Dim l As Integer = 0
    Dim dttTmp As New DataTable
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not LeggiTabpevePeacPecg() Then Return False
      '--------------------------------------------------------------------------------------------------------------
      If strTiporks.Trim = "" AndAlso Not bMovCassa Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128768469374581000, _
                          "Selezionare almeno un tipo documento da elaborare")))
        Return False
      End If
      If nEscomp <= 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128768469390961000, _
                          "Indicare un esercizio di competenza valido")))
        Return False
      End If
      If nAnnoDoc <= 1900 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128768469402973000, _
                          "Anno dei documenti da contabilizzare non corretto." & vbCrLf & "Operazione interrotta.")))
        Return False
      End If

      '-------------------------
      'non possono essere spuntati sia contabilizza anche incassi/pagamenti che 'genera regsitraz. di compensazione con incassi anticipati su ddt'
      If bContCompensDDT Then
        If NTSCInt(Val(oCldPnfa.GetSettingBus("Bsvefdin", "Opzioni", ".", "GestScostAcconti", "0", " ", "0"))) = 2 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130385970092020655, "Elaborazione interrotta: il flag 'Fatt. diff. genera reg. di compens. incassi anticipati su DDT emessi' non è compatibile con l'opzione BSVEFDIN/OPZIONI/GestScostAcconti = 2")))
          Return False
        End If
      End If

      If bContCompensDDT And bIncassi Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129201163075537110, "Elaborazione interrotta: i flags 'Contabilizza anche incassi e pagamenti associati' e " & vbCrLf & "'Fatt. diff. genera reg. di compens. incassi anticipati su DDT emessi' non devono essere selezionati contemporaneamente")))
        Return False
      End If

      If bContIncDDT And bIncassi And bBnrepnco = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129466663542988281, "Elaborazione interrotta: i flags 'Contabilizza anche incassi e pagamenti associati' e  " & vbCrLf & "'DDT emessi->contabilizza solo incassi anticipati' non devono essere selezionati contemporaneamente")))
        Return False
      End If


      If bCa2 And bCa = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129278230963164062, "Elaborazione interrotta: con attivo il modulo della 'Contabilità analitica duplice contabile' il flag 'Genera movimenti di CA' deve essere selezionato")))
        Return False
      End If

      If bCa2 And bGestDatecomp = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129278233848242187, "Elaborazione interrotta: con attivo il modulo della 'Contabilità analitica duplice contabile' l'opzione di registro BSVEPNFA/OPZIONI/GestDatecomp deve essere impostata con valore '-1'")))
        Return False
      End If

      '-------------------------
      If Not oCldPnfa.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", "", dttAnaz) Then Return False
      If Not oCldPnfa.CalcolaDataFineStampaLG(strDittaCorrente, nEscomp, strDtullg, strDtulaca) Then Return False
      strDtulliqi = NTSCDate(dttAnaz.Rows(0)!tb_dtulliq).ToShortDateString
      bProfes = CBool(IIf(dttAnaz.Rows(0)!tb_azprofes.ToString = "S", True, False))

      '-------------------------
      'contorllo l'esercizio di competenza
      If dttAnaz.Rows(0)!tb_azdoppes.ToString <> "S" And NTSCInt(dttAnaz.Rows(0)!tb_escomp) <> nEscomp Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128768469572415000, _
                          "Possono essere eseguite contabilizzazioni solo nell'esercizio 'corrente' (|" & _
                          NTSCInt(dttAnaz.Rows(0)!tb_escomp).ToString & "|) impostato in anagrafica ditta." & vbCrLf & "Operazione interrotta.")))
        Return False
      End If
      If dttAnaz.Rows(0)!tb_azdoppes.ToString = "S" And NTSCInt(dttAnaz.Rows(0)!tb_escomp) <> nEscomp And NTSCInt(dttAnaz.Rows(0)!tb_escompp) <> nEscomp Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128768469960699000, _
                          "Possono essere eseguite contabilizzazioni solo nell'esercizio 'corrente' (|" & _
                          NTSCInt(dttAnaz.Rows(0)!tb_escomp).ToString & "|) o 'precedente' (|" & _
                          NTSCInt(dttAnaz.Rows(0)!tb_escompp).ToString & "|) impostato in anagrafica ditta." & vbCrLf & "Operazione interrotta.")))
        Return False
      End If
      'memorizzo le date di inizio/fine esercizio e controllo che l'esercizio esista ...
      If Not edEscomp_Validated(nEscomp, "") Then Return False

      '-------------------------
      'test data comp. iva
      If bDerogaIva Then
        If strDtcomivaForm.Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128768469971775000, _
                          "E' stato impostato di derogare alla data di compentenza IVA indicata sui documenti, ma non è stata indicata tale data." & vbCrLf & "Operazione interrota.")))
          Return False
        End If

        If NTSCDate(dttAnaz.Rows(0)!tb_dtulliq) >= NTSCDate(strDtcomivaForm) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128768450573157000, _
                          "La data di competenza IVA è anteriore all'ultima liquidazione Iva (|" & NTSCDate(dttAnaz.Rows(0)!tb_dtulliq).ToShortDateString & "|)." & vbCrLf & "Operazione interrotta.")))
          Return False
        End If
      Else
        strDtcomivaForm = ""
      End If

      '-------------------------
      'test libro giornale e registri IVA e liquidazione
      If Not oCldPnfa.CalcolaDataFineStampaRI(strDittaCorrente, nAnnoDoc, dttTabduri) Then Return False
      dtTmp = NTSCDate(IntSetDate("01/01/1900"))
      If dttTabduri.Rows.Count > 0 Then
        If NTSCStr(dttTabduri.Rows(0)!tb_udatreg) <> "" Then
          dtTmp = NTSCDate(dttTabduri.Rows(0)!tb_udatreg)
          If NTSCInt(dttTabduri.Rows(0)!tb_unureg) = 0 Then dtTmp = dtTmp.AddDays(-1)
        End If
      End If
      If strDatregForm <> "" Then
        If NTSCDate(strDtullg) > NTSCDate(strDatregForm) Then
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 128768454645671000, _
                            "La data di registrazione è inferiore alla data di stampa Libro Giornale (|" & strDtullg & "|)." & vbCrLf & _
                            "Procedere ugualmente?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
        End If
        If NTSCDate(strDatregForm) <= dtTmp Then
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 128914314587624570, _
                            "La data di registrazione è inferiore o uguale alla data di stampa definitiva di almeno un registro IVA per l'anno |" & nAnnoDoc & "|." & vbCrLf & _
                            "Procedere ugualmente?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
        End If

        If bCa2 Then
          If NTSCDate(strDtulaca) > NTSCDate(strDatregForm) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129278255809042968, "La data di registrazione è inferiore alla data ultimo aggiornamento CA (|" & strDtulaca & "|)")))
            Return False
          End If
        End If
      End If

      '-------------------------
      'test date inizio/fine elaborazione (semrpe su LG / RI)
      If NTSCDate(strDtullg) > NTSCDate(strDatdocDa) Then
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 128768456153099000, _
                          "La data di inizio elaborazione è inferiore alla data di stampa Libro Giornale (|" & strDtullg & "|)." & vbCrLf & _
                          "Procedere ugualmente?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
      End If
      If NTSCDate(strDatdocDa) <= dtTmp Then
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 128914316046945230, _
                          "La data di inizio elaborazione è inferiore o uguale alla data di stampa definitiva di almeno un registro IVA per l'anno |" & nAnnoDoc & "|." & vbCrLf & _
                          "Procedere ugualmente?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
      End If
      If bCa2 Then
        If NTSCDate(strDtulaca) > NTSCDate(strDatdocDa) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129278256841435546, "La data di inizio elaborazione è inferiore alla data ultimo aggiornamento CA (|" & strDtulaca & "|)")))
          Return False
        End If
      End If

      '-------------------------
      'test serie documenti
      If Not (strSeriedocDa = " " And strSeriedocA.ToLower = "z".PadLeft(SerieMaxLen, "z"c)) Then
        If Not (strSeriedocDa.ToLower = strSeriedocA.ToLower) Then
          LogWrite(oApp.Tr(Me, 128769441789535000, "Per selezione serie documento occorre che le due serie siano uguali."), True)
          Return False
        End If
      End If

      If strPrgParent = "BNPAPNPA" Then
        If strTiporks.IndexOf("2") > -1 Or strTiporks.IndexOf("3") > -1 Or strTiporks.IndexOf("4") > -1 Then
          If bIncassi Then
            LogWrite(oApp.Tr(Me, 129648969863007812, "Non è possibile contabilizzare incassi associati se nell'elaborazione è selezionato 'Avvisi di parcella' e/o 'Note di accredito (parcellazione)'."), True)
            Return False
          End If
        End If
      End If

      oCldPnfa.ValCodiceDb("1", strDittaCorrente, "TABPECG", "N", "", dttTmp)
      If dttTmp.Columns.Contains("tb_contivasplit") Then
        'da net 2014 in poi
        lContoIVASplitPayment = NTSCInt(dttTmp.Rows(0)!tb_contivasplit)
        If lContoIVASplitPayment = 0 Then
          l = NTSCInt(dttTmp.Rows(0)!tb_contrivasplit)
          If l <> 0 Then lContoIVASplitPayment = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, l)
        End If
      Else
        'net 2013
        lContoIVASplitPayment = NTSCInt(oCldPnfa.GetSettingBus("BSCGPECG", "OPZIONI", ".", "ContoIVASplitPayment", "0", " ", "0"))
        If lContoIVASplitPayment = 0 Then
          l = NTSCInt(oCldPnfa.GetSettingBus("BSCGPECG", "OPZIONI", ".", "ContropIVASplitPayment", "0", " ", "0"))
          If l <> 0 Then lContoIVASplitPayment = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, l)
        End If
      End If
      If lContoIVASplitPayment <> 0 Then
        If Not oCldPnfa.ValCodiceDb(lContoIVASplitPayment.ToString, strDittaCorrente, "ANAGRAS", "N", strContoIVASplitPayment) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130655362099559423, "Attenzione: il conto/contropartita IVA split payment impostato in Personalizzazione CG è inesistente")))
          lContoIVASplitPayment = 0
          strContoIVASplitPayment = ""
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se esiste la causale 9999 e questa NON è di tipo 'Normale', avvisa ed esce
      '--------------------------------------------------------------------------------------------------------------
      If oCldPnfa.ValCodiceDb("9999", strDittaCorrente, "TABCAUC", "N", "", dttTmp) = True Then
        If dttTmp.Rows.Count > 0 Then
          If NTSCStr(dttTmp.Rows(0)!tb_tipmov).Trim <> "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130813556820759629, "Attenzione!" & vbCrLf & _
              "La causale '9999', indicata in:" & vbCrLf & _
              " . CAUSALI CONTABILI" & vbCrLf & _
              "deve possedere il 'Tipo movimento' --> 'Normale.'")))
            Return False
          End If
        End If
      End If
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
      dttTmp.Clear()
      dttAnaz.Clear()
      dttTabduri.Clear()
    End Try
  End Function

  Public Overridable Function TestPrecontrolliNotule() As Boolean
    Dim dttTmp As New DataTable
    Try
      If bModuliAcquistati = False Then
        LogWrite(oApp.Tr(Me, 129648726259931641, "Elaborazione interrotta: non è possibile contabilizzare notule se non si possiede il modulo di Contabilità generale"), True)
        Return False
      End If

      If nCausNotul = 0 Then
        LogWrite(oApp.Tr(Me, 129648726501289063, "Elaborazione interrotta: in personalizzazione parcellazione non è stata impostata la causale di emissione notula"), True)
        Return False
      End If

      If lConClinot = 0 Then
        LogWrite(oApp.Tr(Me, 129648726714238281, "Elaborazione interrotta: in personalizzazione contabilità generale non è stata impostata la contropartita'Clienti notule'"), True)
        Return False
      End If

      If lConTranot = 0 Then
        LogWrite(oApp.Tr(Me, 129648726697373047, "Elaborazione interrotta: in personalizzazione contabilità generale non è stata impostata la contropartita'Conto transitorio notule'"), True)
        Return False
      End If

      If Not oCldPnfa.ValCodiceDb(nCausNotul.ToString, strDittaCorrente, "TABCAUC", "N", "", dttTmp) Then
        LogWrite(oApp.Tr(Me, 129648728913671875, "Elaborazione interrotta: Causale contabile per la registrazione di NOTULE |'" & nCausNotul & "'| inesistente"), True)
        Return False
      Else
        If NTSCStr(dttTmp.Rows(0)!tb_tipreg) <> " " Or NTSCStr(dttTmp.Rows(0)!tb_tipreg2) <> " " Then
          LogWrite(oApp.Tr(Me, 129648728880312500, "Elaborazione interrotta: Causale contabile per la registrazione di NOTULE |'" & nCausNotul & "'| non deve gestire i registri IVA"), True)
          Return False
        End If
      End If
      dttTmp.Clear()

      If oCldPnfa.ValCodiceDb(lConClinot.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp) Then
        If NTSCStr(dttTmp.Rows(0)!an_scaden) <> "S" Then
          LogWrite(oApp.Tr(Me, 129648728983671875, "Elaborazione interrotta: la contropartita 'Clienti notule' indicato in personalizzazione contabilità generale è associata ad un sottoconto non gestito a partite/scadenze"), True)
          Return False
        End If
      End If
      dttTmp.Clear()

      If oCldPnfa.ValCodiceDb(lConTranot.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp) Then
        If NTSCStr(dttTmp.Rows(0)!an_scaden) = "S" Then
          LogWrite(oApp.Tr(Me, 129648729490380859, "Elaborazione interrotta: la contropartita 'Conto transitorio notule' indicato in personalizzazione contabilità generale è associata ad un sottoconto gestito a partite/scadenze"), True)
          Return False
        End If
        If NTSCStr(dttTmp.Rows(0)!an_flci) = "S" Then
          LogWrite(oApp.Tr(Me, 129648729766943359, "Elaborazione interrotta: la contropartita 'Conto transitorio notule' indicato in personalizzazione contabilità generale è associata ad un sottoconto gestito in contabilità analitica"), True)
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


  Public Overridable Function LeggiTabpevePeacPecg() As Boolean
    Try
      Return LeggiTabpevePeacPecgConto(0, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function LeggiTabpevePeacPecgConto(ByVal lConto As Integer) As Boolean
    Try
      'obsoleta

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {lConto})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      Return LeggiTabpevePeacPecgConto(lConto, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function LeggiTabpevePeacPecgConto(ByVal lConto As Integer, ByVal strDatdoc As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {lConto, strDatdoc})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      nAggControp = 0
      If lConto <> 0 Then
        oCldPnfa.ValCodiceDb(lConto.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then nAggControp = NTSCInt(dttTmp.Rows(0)!an_agcontrop)
        dttTmp.Clear()
      End If

      If strPrgParent = "BNPAPNPA" Then
        'BNPAPNPA: leggo tabpepa (come struttura simile a tabpeve)
        oCldPnfa.ValCodiceDb(IIf(bProfes, "1", "2").ToString, strDittaCorrente, "TABPEPA", "N", "", dttPeve)
        If dttPeve.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 129648695806181641, "Mancano dati in 'Personalizzazione parcellazione'." & vbCrLf & "Elaborazione/stampa non possibili."), True)
          Return False
        End If
        With dttPeve.Rows(0)
          nCauemft = NTSCInt(!tb_cauemft) : nCaunoac = NTSCInt(!tb_caunoac)
          nCaunoaf = NTSCInt(!tb_caunoaf) : nCauemfs = NTSCInt(!tb_cauemfs)
          nCaucorr = NTSCInt(!tb_caucorr) : nCauinc = NTSCInt(!tb_cauinc)
          nCaunoacsf = NTSCInt(!tb_caunoacsf)   'causale nota accred storno fattura
          nCausNotul = NTSCInt(!tb_cauemnt)     'causale emissione notule
          ' adesso campi per contab. r.fiscali
          lConclpriv = 0 ' per ora  (poi da anaditpa ) .... objStd.gcTabe!tb_conclpriv
          lConRitenut = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, CoveAggControp(NTSCInt(!tb_vlistin))) ' conto ritenuta
          lConAltriprev = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, CoveAggControp(NTSCInt(!tb_vtipobf))) ' conto enasarco
          lConribo = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrribo)))
          lConriin = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrriin)))
          lConcas = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrcas)))
          lConcas2 = lConcas
          lConcasPeve = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrcas)))
          lConabat = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrabat)))
          lConabpa = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrabpa)))
          lConclpriv = NTSCInt(!tb_conclpriv)
          nPeveIva15 = NTSCInt(!tb_iva15)
          nPeveIvainc = NTSCInt(!tb_ivainc)
          If strDatdoc <> "" And NTSCDate(strDatdoc) < NTSCDate(!tb_datfinivainc3) Then
            nPeveIvainc = NTSCInt(!tb_ivainc3)
          ElseIf strDatdoc <> "" And NTSCDate(strDatdoc) < NTSCDate(!tb_datfinivainc2) Then
            nPeveIvainc = NTSCInt(!tb_ivainc2)
          Else
            nPeveIvainc = NTSCInt(!tb_ivainc)
          End If
        End With    'With dttTmp.Rows(0)
        dttPeve.Clear()

        'leggo da anaditpa i codici per gestire cassa commercialisti e spese generali
        oCldPnfa.GetAnaditpa(strDittaCorrente, dttPeve)
        If dttPeve.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 129648713055244141, "Manca Tabella dati 'aggiuntivi parcellazione' in anagrafica ditta!" & vbCrLf & "Elaborazione/stampa non possibili."), True)
          Return False
        End If
        lConspegen = 0
        lConcascom = 0
        strCodSpegen = NTSCStr(dttPeve.Rows(0)!acs_codvparsg).Trim
        strCodCascom = NTSCStr(dttPeve.Rows(0)!acs_codvpar).Trim
        dttPeve.Clear()

        If strCodSpegen <> "" Then
          oCldPnfa.ValCodiceDb(strCodSpegen, strDittaCorrente, "TABVPAR", "S", "", dttPeve)
          If dttPeve.Rows.Count > 0 Then
            lConspegen = NTSCInt(dttPeve.Rows(0)!tb_pcontrop)
            lConspegen = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, lConspegen)
          End If
          dttPeve.Clear()
        End If
        If strCodCascom <> "" Then
          oCldPnfa.ValCodiceDb(strCodCascom, strDittaCorrente, "TABVPAR", "S", "", dttPeve)
          If dttPeve.Rows.Count > 0 Then
            lConcascom = NTSCInt(dttPeve.Rows(0)!tb_pcontrop)
            lConcascom = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, lConcascom)
          End If
          dttPeve.Clear()
        End If
      Else
        'BNVEPNFA + BNREPNCO
        oCldPnfa.ValCodiceDb("1", strDittaCorrente, "TABPEVE", "N", "", dttPeve)
        If dttPeve.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 128769432221993000, "Mancano dati in 'Personalizzazione vendite'." & vbCrLf & "Elaborazione/stampa non possibili."), True)
          Return False
        End If
        With dttPeve.Rows(0)
          nCauemft = NTSCInt(!tb_cauemft) : nCaunoac = NTSCInt(!tb_caunoac)
          nCauemftat = NTSCInt(!tb_cauemftat) : nCaunoacat = NTSCInt(!tb_caunoacat)
          nCaunoaf = NTSCInt(!tb_caunoaf) : nCauemfs = NTSCInt(!tb_cauemfs)
          nCaucorr = NTSCInt(!tb_caucorr) : nCauinc = NTSCInt(!tb_cauinc)
          nCaustom = NTSCInt(!tb_caustom)
          nCaustib = NTSCInt(!tb_caustib)
          lConribo = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrribo)), NTSCInt(!tb_conribo))
          lConriac = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrriac)), NTSCInt(!tb_conriac))
          lConriin = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrriin)), NTSCInt(!tb_conriin))
          lConcas = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrcas)), NTSCInt(!tb_concas))
          lConcas2 = lConcas
          lConcasPeve = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrcas)), NTSCInt(!tb_concas))
          lConabat = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrabat)), NTSCInt(!tb_conabat))
          lConabpa = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrabpa)), NTSCInt(!tb_conabpa))
          lConcauz = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrcauz)), NTSCInt(!tb_concauz))
          lConomag = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contromag)), NTSCInt(!tb_conomag))
          lConimba = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrimba)), NTSCInt(!tb_contimba))
          lConclpriv = NTSCInt(!tb_conclpriv)
          lConcrrfc = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrcrrfc)), NTSCInt(!tb_concrrfc))
          lConcrrfp = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrcrrfp)), NTSCInt(!tb_concrrfp))
          lConivrfp = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_contrivrfp)), NTSCInt(!tb_conivrfp))
          nCaurfincc = NTSCInt(!tb_caurfincc) : nCaurfparc = NTSCInt(!tb_caurfparc)
          nCaurfnonc = NTSCInt(!tb_caurfnonc) : nCaurfriec = NTSCInt(!tb_caurfriec)
          nCaurfincp = NTSCInt(!tb_caurfincp) : nCaurfparp = NTSCInt(!tb_caurfparp)
          nCaurfnonp = NTSCInt(!tb_caurfnonp) : nCaurfriep = NTSCInt(!tb_caurfriep)
          nPeveIva15 = NTSCInt(!tb_iva15)
          nPeveIvainc = NTSCInt(!tb_ivainc)
          If strDatdoc <> "" And NTSCDate(strDatdoc) < NTSCDate(!tb_datfinivainc3) Then
            nPeveIvainc = NTSCInt(!tb_ivainc3)
          ElseIf strDatdoc <> "" And NTSCDate(strDatdoc) < NTSCDate(!tb_datfinivainc2) Then
            nPeveIvainc = NTSCInt(!tb_ivainc2)
          Else
            nPeveIvainc = NTSCInt(!tb_ivainc)
          End If
        End With    'With dttTmp.Rows(0)
      End If    'If strPrgParent = "BNPAPNPA" Then

      '------------------
      If strPrgParent <> "BNPAPNPA" Then
        oCldPnfa.ValCodiceDb("1", strDittaCorrente, "TABPEAC", "N", "", dttPeac)
        If dttPeac.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 128769431898293000, "Mancano dati in 'Personalizzazione acquisti'." & vbCrLf & "Elaborazione/stampa non possibili."), True)
          Return False
        End If

        With dttPeac.Rows(0)
          nCaurcft = NTSCInt(!tb_caurcft)
          nCaurcftcee = NTSCInt(!tb_caurcftcee)
          nCaurcnacee = NTSCInt(!tb_caurcnacee)
          nCaurcna = NTSCInt(!tb_caurcna)
          nCaupag = NTSCInt(!tb_caupag)
          nCaustomac = NTSCInt(!tb_caustomac)
          nCaurcfrc = NTSCInt(!tb_caurcfrc)  'fatt acq reverse charge
          nCaurcarc = NTSCInt(!tb_caurcarc)  'nota accr acq reverse charge
          nCaurcftXcee = NTSCInt(!tb_caurcftextrc)
          nCaurcnaXcee = NTSCInt(!tb_caurcnextrc)
          lAconribo = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_acontrribo)), NTSCInt(!tb_aconribo))
          lAconriac = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_acontrriac)), NTSCInt(!tb_aconriac))
          lAconriin = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_acontrriin)), NTSCInt(!tb_aconriin))
          lAconcas = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_acontrcas)), NTSCInt(!tb_aconcas))
          lAconabat = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_acontrabat)), NTSCInt(!tb_aconabat))
          lAconabpa = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_acontrabpa)), NTSCInt(!tb_aconabpa))
          lAconcauz = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_acontrcauz)), NTSCInt(!tb_aconcauz))
          lAconomag = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_acontromag)), NTSCInt(!tb_aconomag))
          lAconimba = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, CoveAggControp(NTSCInt(!tb_acontrimba)), NTSCInt(!tb_acontimba))
          If dttPeac.Columns.Contains("tb_caurcfs") Then nCaurffs = NTSCInt(!tb_caurcfs) Else nCaurffs = nCaurcft
          nPeacIva15 = NTSCInt(!tb_aiva15)
          nPeacIvainc = NTSCInt(!tb_aivainc)
          If strDatdoc <> "" And NTSCDate(strDatdoc) < NTSCDate(!tb_adatfinivainc3) Then
            nPeacIvainc = NTSCInt(!tb_aivainc3)
          ElseIf strDatdoc <> "" And NTSCDate(strDatdoc) < NTSCDate(!tb_adatfinivainc2) Then
            nPeacIvainc = NTSCInt(!tb_aivainc2)
          Else
            nPeacIvainc = NTSCInt(!tb_aivainc)
          End If
        End With    'With dttTmp.Rows(0)
      End If    'If strPrgParent <> "BNPAPNPA" Then

      '------------------
      oCldPnfa.ValCodiceDb("1", strDittaCorrente, "TABPECG", "N", "", dttPecg)
      If dttPecg.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128769432200933000, "Mancano dati in 'Personalizzazione contabilità'." & vbCrLf & "Elaborazione/stampa non possibili."), True)
        Return False
      End If

      With dttPecg.Rows(0)
        nCauststanz = NTSCInt(!tb_cauststanz)
        lCondaconv = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, NTSCInt(!tb_contrdaconv), NTSCInt(!tb_condaconv))
        lCondfconv = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, NTSCInt(!tb_contrdfconv), NTSCInt(!tb_condfconv))
        lDiffAttCambi = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, NTSCInt(!tb_contrdcat), NTSCInt(!tb_condcat)) 'sottoconto differenze attive su cambi  (tb_condcat)
        lDiffPasCambi = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, NTSCInt(!tb_contrdcpa), NTSCInt(!tb_condcpa)) 'sottoconto differenze passive su cambi (tb_condcpa)
        lConEff = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, NTSCInt(!tb_contreff), NTSCInt(!tb_coneff))         ' conto effetti (per chi manca di modulo CG)
        If lConEff = 0 Then
          LogWrite(oApp.Tr(Me, 128769432492029000, "In 'Personalizzazione contabilità' non è stato impostato il conto/contropartita 'Effetti attivi'." & vbCrLf & "Elaborazione/stampa non possibili."), True)
          Return False
        End If
        lConRicSosp = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, NTSCInt(!tb_contrricsosp), NTSCInt(!tb_conricsosp))  ' conto ricavi sospesi (solo per ditte professioniste)
        lConClinot = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(!tb_contrcln))
        lConTranot = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(!tb_contrctnt))
        If bProfes = False Then lConRicSosp = 0 'se non sono professionista il conto è sempre = 0
        '------------------------------------------------------------------------------------------------------------
        If oCldPnfa.DittaConEmissioneEffettiChiusuraCliente(strDittaCorrente) = False Then
          Dim lContoTEMP As Integer = 0
          lContoTEMP = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, NTSCInt(!tb_conclabpd), NTSCInt(!tb_conclrb))         ' conto effetti (per chi manca di modulo CG)
          If lContoTEMP = 0 Then
            LogWrite(oApp.Tr(Me, 130627892214867385, "In 'Personalizzazione contabilità' non è stato impostato il conto/contropartita 'Clienti C/RB'." & vbCrLf & "Elaborazione/stampa non possibili."), True)
            Return False
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
      End With    'With dttTmp.Rows(0)

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

  Public Overridable Function CoveAggControp(ByVal nCodcove As Integer) As Integer
    CoveAggControp = nCodcove
    Try
      If nAggControp = 0 Then Return nCodcove
      If nCodcove = 0 Then Return nCodcove
      If dsCove.Tables("TABCOVE").Rows.Count = 0 Then Return nCodcove
      If dsCove.Tables("TABCOVE").Select("tb_codcove = " & nAggControp + nCodcove).Length > 0 Then Return nAggControp + nCodcove
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

  Public Overridable Function TipoDocumento(ByVal strTipork As String) As String
    Try
      Select Case strTipork
        Case "A" : Return oApp.Tr(Me, 128769423988141000, "Fattura immediata emessa")
        Case "B" : Return oApp.Tr(Me, 128769424314003000, "D.D.T. emesso")
        Case "C" : Return oApp.Tr(Me, 128769424326327000, "Corrispettivo emesso")
        Case "D" : Return oApp.Tr(Me, 128769424338963000, "Fattura differita emessa")
        Case "E" : Return oApp.Tr(Me, 128769424350819000, "Nota di addebito emessa")
        Case "F" : Return oApp.Tr(Me, 128769424362051000, "Ricevuta fiscale emessa")
        Case "I" : Return oApp.Tr(Me, 128769424370631000, "Riemissione ricevuta fiscale")
        Case "J" : Return oApp.Tr(Me, 128769424379523000, "Nota di accredito ricevuta")
        Case "K" : Return oApp.Tr(Me, 128769424401675000, "Fattura differita ricevuta")
        Case "L" : Return oApp.Tr(Me, 128769424411191000, "Fattura immediata ricevuta")
        Case "M" : Return oApp.Tr(Me, 128769424419927000, "D.D.T. ricevuto")
        Case "N" : Return oApp.Tr(Me, 128769424427727000, "Nota di accredito emessa")
        Case "P" : Return oApp.Tr(Me, 128769424440207000, "Fattura ricevuta fiscale differita")
        Case "S" : Return oApp.Tr(Me, 128769424450971000, "Fattura ricevuta fiscale emessa")
        Case "T" : Return oApp.Tr(Me, 128769424458927000, "Carico da produzione")
        Case "U" : Return oApp.Tr(Me, 128769424466727000, "Scarico a produzione")
        Case "Z" : Return oApp.Tr(Me, 128769424475307000, "Bolla di movimentazione interna")
        Case "£" : Return oApp.Tr(Me, 129243437335136719, "Nota accredito differita emessa")
        Case "(" : Return oApp.Tr(Me, 129243437349052735, "Nota accredito differita ricevuta")
        Case "1" : Return oApp.Tr(Me, 129648742214746094, "Fattura/fattura di acconto (parcellazione)")
        Case "2" : Return oApp.Tr(Me, 129648742225546875, "Nota di accredito/storno fattura (parcellazione)")
        Case "3" : Return oApp.Tr(Me, 129648742235546875, "Avviso di parcella/notula (parcellazione)")
        Case "4" : Return oApp.Tr(Me, 129648742246914063, "Nota di accredito (parcellazione)")
        Case "5" : Return oApp.Tr(Me, 129648742258105469, "Corrispettivo (parcellazione)")
      End Select
      Return strTipork

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return strTipork
    End Try
  End Function

  Public Overridable Function ElaboraSingoloDoc(ByVal strParent As String, ByVal strTipork As String, _
                                                ByVal nAnno As Integer, ByVal strSerie As String, _
                                                ByVal lNumdoc As Integer, ByVal strDatdoc As String, _
                                                ByVal nEscompContab As Integer, ByVal bRielabora As Boolean) As Boolean
    'chiamato dall'entity di BEVEFDIN, BEVEBOLL, per contabilizzare/ricontabilizzare un singolo documento
    Try
      Dim bTipoRkOk As Boolean = False

      '-----------------------------------------
      'aggiungo le opzioni di registro
      Dim strContTipiRk As String = oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "ContabAutomTipirk", "", "", "")
      Dim strContEff As String = oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "ContabAutomEff", "", "", "-1")
      Dim strContPr As String = oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "ContabAutomPr", "", "", "-1")
      Dim strContFatDiffIncPag As String = oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "ContabAutomFatDiffIncPag", "", "", "0")

      'eseguo i controlli
      If strContTipiRk.Trim <> "" Then
        ' se ho specificato un tipo documento nell'opzione di registro, verifico 
        'che il tipo documento sia tra quelli indicati
        If strContTipiRk.ToUpper.Contains(strTipork) Then
          bTipoRkOk = True
        End If
      Else
        'altrimenti li contabilizzo tutti
        bTipoRkOk = True
      End If 'If strContTipiRk <> ""

      '-------------------------------------------
      If strTipork = "B" Or strTipork = "C" Then Return True 'non contabilizzo corrispettivi e DDT di acconto

      If bModuliAcquistati = False Then
        bGenEffetti = False
      Else
        If strContEff = "0" Then 'se ho settato a 0 la proprietà di registro
          bGenEffetti = False
        Else
          bGenEffetti = True
        End If
      End If
      If CBool(ModuliDittaDitt(strDittaCorrente) And bsModPR) = False Then
        bAggProvvig = False
      Else
        If strContPr = "0" Then 'se ho settato a 0 la proprietà di registro
          bAggProvvig = False
        Else
          bAggProvvig = True
        End If
      End If
      If CBool(ModuliDittaDitt(strDittaCorrente) And bsModCI) Then
        bCa = True
      Else
        bCa = False
      End If
      If CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupCAE) Then
        bCa = True
      End If

      bInt = False
      nEscomp = nEscompContab
      strDatregForm = ""
      strTiporks = strTipork
      bDerogaIva = False
      strDtcomivaForm = ""
      bAutorizzato = False
      nAnnoDoc = nAnno
      strDatdocDa = strDatdoc
      strDatdocA = strDatdoc
      strSeriedocDa = strSerie
      strSeriedocA = strSerie
      lNumdocDa = lNumdoc
      lNumdocA = lNumdoc
      nTipobf = 0
      bRielab = bRielabora
      bDelEffettiNoPres = True
      bContabAncheConScadSald = True
      bContIncDDT = False

      'se ho inserito un tipoRk non presente nell'opzione, esco.
      If Not bTipoRkOk Then
        Return False
      End If

      If strTipork = "D" Then
        If strContFatDiffIncPag = "-1" Then
          bIncassi = True
          bContCompensDDT = False
        Else
          bIncassi = False
          bContCompensDDT = True  'sui doc. differiti, se ho degli acconti assumo che siano stati registrati in CG con pnfa come ddt che generano acconti
        End If
      Else
        bIncassi = True
        bContCompensDDT = False 'è incompatibile con 'contabilizza incassi/pagamenti associati'
      End If
      If strTipork = "N" Then
        bContCompensNoteAcc = True  'compenso con eventuali fatture emesse
      Else
        bContCompensNoteAcc = False
      End If
      bCheckProtocolli = False
      bBnrepnco = False
      strPrgParent = "BNVEPNFA"

      Return Elabora()

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function Elabora() As Boolean
    Dim strDesogglog As String = ""
    Dim i As Integer = 0
    Dim strT() As String = Nothing
    Dim dttTmp As New DataTable
    Dim nAnnoIva As Integer = 0
    Dim bIncassiTmp As Boolean = False

    Dim strOpzioneVEBOLL As String = oCldPnfa.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "Prot_su_doc_ric", "N", " ", "N").ToUpper
    Dim strOpzioneFDIN As String = oCldPnfa.GetSettingBus("BSVEFDIN", "OPZIONI", ".", "Prot_su_doc_ric", "N", " ", "N").ToUpper
    Dim strOpzionePNFA As String = oCldPnfa.GetSettingBus(IIf(strPrgParent = "BNPAPNPA", "BSPABOLL", "BSVEBOLL").ToString, _
                                                          "OPZIONI", ".", "Prot_su_doc_ric", "N", " ", "N").ToUpper

    'per BNPAPNPA la registrazione, nel caso in cui tutte le righe sono compilate, sarà:
    '  DARE           AVERE
    '-----------------------
    'cliente                              (da testpar)
    '               ricavo 1              (da movpar)
    '               ricavo 2              (da movpar)
    '               ricavo n              (da movpar)
    '               spese generali        (da testpar)
    '               cassa commercialisti  (da testpar)
    '               spese incasso         (da testpar)
    '               bolli                 (da testpar)
    'ritenuta                             (da testpar)
    '               cliente per ritenuta
    'enasarco                             (da testpar)
    '               cliente per enasarco
    '               riga IVA 1            (da testpar)
    '               riga IVA 2            (da testpar)
    '               riga IVA n            (da testpar)
    '---------------------------------
    'per la notula, la registrazione, nel caso in cui tutte le righe sono compilate, sarà:
    '  DARE           AVERE
    '-----------------------
    'cliente notule                       (gestito a partite/scadenze e in scaden c'è flag sc_salcon = 'S' e in sc_controp c'è cliente)
    '               conto transit. notule  (non gestito a partite/scadenze)
    '---------------------------------
    'nel caso di parcella incassata
    'NON C'E' BISOGNO DI AGGIORNARE SCADEN:
    '   SE LA FATTURA RISULTA ESSERE SALDATA, HO GIA' GENERATO LA SCADENZA CON IL FLAG' SALDATO
    '   NON E' POSSIBILE AVERE INCASSI PARZIALI!!!!!!

    'la registrazione, nel caso in cui tutte le righe sono compilate, sarà:
    '  DARE           AVERE
    '-----------------------
    '               cliente               (da testpar)
    'cassa/banca                          (da testpar)
    '        abbuono                      (da testpar)
    'conto sospeso                        (da movpar)
    '               ricavo 1              (da movpar)
    'conto sospeso                        (da movpar)
    '               ricavo 2              (da movpar)
    'conto sospeso                        (da movpar)
    '               ricavo n              (da movpar)

    Try
      bInElaborazione = True
      '--------------------------------------------------------------------------------------------------------------
      '--- Prima dell'elaborazione, controlla che l'opzione di registro "Prot_su_doc_ric" 
      '--- sia uguale per i programmi:
      '----- --> BSVEBOLL/OPZIONI/Prot_su_doc_ric' 
      '----- --> BSVEFDIN/OPZIONI/Prot_su_doc_ric' 
      '----- --> BSPABOLL/OPZIONI/Prot_su_doc_ric' 
      '--- Se in modalità Batch, lo scrive nel file di LOG e annulla l'elaborazione, 
      '--- altrimenti chiede conferma nel proseguire
      '--------------------------------------------------------------------------------------------------------------
      If (strOpzionePNFA <> strOpzioneVEBOLL) Or (strOpzionePNFA <> strOpzioneFDIN) Then
        If oApp.Batch = True Then
          CLN__STD.WriteMsgBoxToLog(oApp.Tr(Me, 130867839475724201, _
            "Le opzioni di registro" & vbCrLf & _
            " . 'BSVEBOLL/OPZIONI/Prot_su_doc_ric'" & vbCrLf & _
            " . 'BSVEFDIN/OPZIONI/Prot_su_doc_ric'" & vbCrLf & _
            IIf(strPrgParent = "BNPAPNPA", " . 'BSPABOLL/OPZIONI/Prot_su_doc_ric'" & vbCrLf, "").ToString & _
            "sono impostate con valori differenti." & vbCrLf & _
            "Contabilizzazione documenti annullata" & vbCrLf & _
            "per probabili incongruenze in fase di assegnazione numero di protocollo su documenti ricevuti."))
          Return False
        Else
          Dim evnt As New NTSEventArgs(ThMsg.MSG_YESNO, oApp.Tr(Me, 130867837936651562, "Attenzione!" & vbCrLf & _
            "Le opzioni di registro" & vbCrLf & _
            " . 'BSVEBOLL/OPZIONI/Prot_su_doc_ric'" & vbCrLf & _
            " . 'BSVEFDIN/OPZIONI/Prot_su_doc_ric'" & vbCrLf & _
            IIf(strPrgParent = "BNPAPNPA", " . 'BSPABOLL/OPZIONI/Prot_su_doc_ric'" & vbCrLf, "").ToString & _
            "sono impostate con valori differenti." & vbCrLf & _
            "Potrebbero verificarsi incongruenze in fase di assegnazione numero di protocollo su documenti ricevuti." & vbCrLf & _
            "Proseguire?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue <> ThMsg.RETVALUE_YES Then
            ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 130867838768837293, _
              "Contabilizzazione documenti annullata.")))
            Return False
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------


      If oApp.Batch Then
        'carico i controlli dal file
        CLN__STD.WriteMsgBoxToLog(oApp.Tr(Me, 128745779808067000, "BNVEPNFA: Load delle impostazioni per avvio di procedura batch in corso ... (per ulteriori dettagli vedi BNVEPNFA.LOG)"))
        If Not CaricaControlli() Then Return False
      Else
        'i parametri sono stati impostati da BNVEPNFA
      End If

      '---------------------
      'scrivo il log
      Dim strSi As String = oApp.Tr(Me, 130367785313058558, "'Si'")
      Dim strNo As String = oApp.Tr(Me, 130367785688681154, "'No'")
      Dim strData As String = oApp.Tr(Me, 130367786914298310, "(Data di registrazione = Data documento)")
      Dim strIIf1 As String = IIf(bInt, strSi, strNo).ToString
      Dim strIIf2 As String = IIf(strDatregForm <> "", strDatregForm, strData).ToString
      Dim strIIf3 As String = IIf(bDerogaIva, oApp.Tr(Me, 130367788403976276, "'Sì' ('|" & strDtcomivaForm & "|')"), strNo).ToString
      Dim strIIf4 As String = IIf(bAutorizzato, strSi, strNo).ToString
      Dim strIIf5 As String = IIf(bCheckProtocolli, strSi, strNo).ToString
      strDesogglog = vbCrLf & vbCrLf & oApp.Tr(Me, 130367790411150930, _
        " - Registrazione integrativa............................: " & strIIf1 & vbCrLf & _
        " - Esercizio di competenza..............................: " & nEscomp & vbCrLf & _
        " - Data di registrazione................................: " & strIIf2 & vbCrLf & _
        " - Deroga a data competenza Iva.........................: " & strIIf3 & vbCrLf & _
        " - Non generare scadenze autorizzate su fatture ricevute: " & strIIf4 & vbCrLf & _
        " - Controlla protocolli doppi al termine dell'elaboraz..: " & strIIf5 & vbCrLf & _
        " - Selezione documenti da contabilizzare................:" & vbCrLf)
      strT = strTiporks.Split(";"c)
      For i = 0 To strT.Length - 1
        Select Case strT(i)
          'bnvepnfa
          Case "B" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367790750836256, " DDT emessi................................................: SI") & vbCrLf
          Case "A" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367790878804187, " Fatture immediate emesse..................................: SI") & vbCrLf
          Case "D" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367790995678439, " Fatture differite emesse..................................: SI") & vbCrLf
          Case "C" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367791144271238, " Corrispettivi emessi......................................: SI") & vbCrLf
          Case "N" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367791271301675, " Note di accredito emesse..................................: SI") & vbCrLf
          Case "E" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367791395519630, " Note di addebito emesse...................................: SI") & vbCrLf
          Case "F" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367791525518798, " Ricevute fiscali emesse...................................: SI") & vbCrLf
          Case "I" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367791649893002, " Riemissione ricevute fiscali..............................: SI") & vbCrLf
          Case "S" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367791787235873, " Fatture/ricevute fiscali emesse...........................: SI") & vbCrLf
          Case "P" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367791921453764, " Fatture/ricevute fiscali differite........................: SI") & vbCrLf
          Case "L" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367792039890506, " Fatture immediate ricevute................................: SI") & vbCrLf
          Case "K" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367792154577272, " Fatture differite ricevute................................: SI") & vbCrLf
          Case "J" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367792282232705, " Note di accredito ricevute................................: SI") & vbCrLf
          Case "£" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367792411294379, " Note di accredito differite emesse........................: SI") & vbCrLf
          Case "(" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367792550668487, " Note di accredito differite ricevute......................: SI") & vbCrLf
            'bnpapnpa
          Case "1" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367792729573592, " Fattura/fattura di acconto  (parcellazione)...............: SI") & vbCrLf
          Case "2" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367792850979065, " Nota di accredito/storno fattura  (parcellazione).........: SI") & vbCrLf
          Case "3" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367792971603293, " Avviso di parcella/notula  (parcellazione)................: SI") & vbCrLf
          Case "4" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367793100352469, " Nota di accredito  (parcellazione)........................: SI") & vbCrLf
          Case "5" : strDesogglog += "".PadLeft(10) & oApp.Tr(Me, 130367793223789179, " Corrispettivo (parcellazione).............................: SI") & vbCrLf
        End Select
      Next
      Dim strIIf6 As String = IIf(bRielab, strSi, strNo).ToString
      Dim strIIf7 As String = IIf(bIncassi, strSi, strNo).ToString
      Dim strIIf8 As String = IIf(bCa, strSi, strNo).ToString
      Dim strIIf9 As String = IIf(bContIncDDT, strSi, strNo).ToString
      Dim strIIf10 As String = IIf(bContCompensDDT, strSi, strNo).ToString
      Dim strIIf11 As String = IIf(bContCompensNoteAcc, strSi, strNo).ToString
      Dim strIIf12 As String = IIf(bBnrepnco, strSi, strNo).ToString
      strDesogglog += _
        "".PadLeft(10) & oApp.Tr(Me, 130367793372694476, " . Anno documento................................................: '|" & nAnnoDoc.ToString & "|'") & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130367793586755606, " . Data documento dal............................................: '|" & strDatdocDa & "|' al '|" & strDatdocA & "|'") & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130367794857528723, " . Dalla serie/numero documento..................................: '|" & strSeriedocDa & "'|/'|" & lNumdocDa.ToString & "|'") & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130367795271901071, " . Alla serie/numero documento...................................: '|" & strSeriedocA & "|'/'|" & lNumdocA.ToString & "|'") & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130367795529711921, " . Tipo bolla/fattura............................................: '|" & nTipobf.ToString & "|' ") & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130367795723460681, " . Contabilizza anche documenti gia' contabilizzati in precedenza: ") & strIIf6 & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130373713267750119, " . . Cancella emiss. effetti collegati a fattura NON pres. banca.: ") & IIf(bDelEffettiNoPres, strSi, strNo).ToString & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130373713285679908, " . . Contabilizza anche documenti con scadenze saldate...........: ") & IIf(bContabAncheConScadSald, strSi, strNo).ToString & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130367797679385663, " . Contabilizza anche incassi e pagamenti associati..............: ") & strIIf7 & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130367799068283024, " . Genera movimenti di Contabilità Analitica associati...........: ") & strIIf8 & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130367801279675121, " . DDT emessi contabilizza solo gli incassi anticipati...........: ") & strIIf9 & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130367801410299285, " . Fatt. diff. genera reg. di compens. incassi anticip su DDT em.: ") & strIIf10 & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130367801570298261, " . Note accr. emesse genera reg. di compens. con fatture em......: ") & strIIf11 & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130367801850140220, " . Per contabilizzazione documenti Retail........................: ") & strIIf12 & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130398749677460269, " . Genera effetti su scadenze RB di documenti emessi.............: ") & IIf(bGenEffetti, strSi, strNo).ToString & vbCrLf & _
        "".PadLeft(10) & oApp.Tr(Me, 130398749696243804, " . Aggiorna archivio provvigioni.................................: ") & IIf(bAggProvvig, strSi, strNo).ToString & vbCrLf

      '---------------------
      If oApp.Batch Then
        If Not LogStart("BNVEPNFA_BATCH", oApp.Tr(Me, 130367802914352159, "Contabilizzazione documenti di magazzino") & vbCrLf, True) Then Return False
        LogWrite(oApp.Tr(Me, 128768492996465000, "Per dettagli sull'avvio in modalità BATCH consultare il file '|BusNetBatch_" & System.Diagnostics.Process.GetCurrentProcess.Id.ToString & ".log|'"), False)
      Else
        If Not LogStart("BNVEPNFA", oApp.Tr(Me, 130367803161069330, "Contabilizzazione documenti di magazzino") & vbCrLf) Then Return False
      End If
      LogWrite(strDesogglog, False)

      '---------------------
      'legge opzioni di registro
      bRegIvaContropRiga1 = CBool(oCldPnfa.GetSettingBus("BSCGPRIN", "OPZIONI", ".", "RegIvaContropRiga1", "-1", " ", "-1"))
      bRevChargeNumdocDaPartita = CBool(oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "RevChargeNumdocDaPartita", "0", " ", "0"))
      bDocIntraNumreg2DaTabnuma = CBool(oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "DocIntraNumreg2DaTabnuma", "0", " ", "0"))
      bUsaAggContropAnagra = CBool(oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "UsaAggContropAnagra", "0", " ", "0"))
      lDiffMaxCgCa = NTSCInt(oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "DiffMaxCgCa", "10", " ", "10"))
      bFattDiffCompDdtPartita = CBool(oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "FattDiffCompDdtPartita", "0", " ", "0")) 'se TRUE, in reg. fatture differite emesse, la compensazione con acconti da DDT deve far perdere la partita degli acconti (OPERA SOLO SU CONTI GESTITI A PARTITE)
      If strPrgParent = "BNPAPNPA" Then
        strOpz19 = oCldPnfa.GetSettingBus("BSPABOLL", "OPZIONI", ".", "Prot_su_doc_ric", "N", " ", "N") ' S se n prot. attribuito in fase di ins. a mag.
        bForzaCG = CBool(oCldPnfa.GetSettingBus("BSPAPNPA", "OPZIONI", ".", "ForzaCG", "0", " ", "0"))  ' NON DOCUMENTARE
        If bForzaCG Then bModuliAcquistati = True
        bUsaNumdocPerNumpar = CBool(oCldPnfa.GetSettingBus("BSPAPNPA", "OPZIONI", ".", "UsaNumdocPerNumpar", "0", " ", "0")) ' -1 = utilizza sempre tm_numdoc per def. il numero partita da indicare nella reg. contabile e in scadenziario
        bGestDatecomp = CBool(oCldPnfa.GetSettingBus("BSPAPNPA", "OPZIONI", ".", "GestDatecomp", "0", " ", "0"))
        bCeas = False
        bGirocontoIvaIndedRipartito = False
        bGestPVR = False
        bRidCodClieDebTurnoDest = False
        strUsaContoFattDoc = "0"
        bUsaContoFatt = False
      Else
        strOpz19 = oCldPnfa.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "Prot_su_doc_ric", "N", " ", "N") ' S se n prot. attribuito in fase di ins. a mag.
        bForzaCG = CBool(oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "ForzaCG", "0", " ", "0"))  ' NON DOCUMENTARE
        bAlfproRicFromTabnuma = CBool(oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "AlfproRicFromTabnuma", "0", " ", "0"))
        If bForzaCG Then bModuliAcquistati = True
        bUsaNumdocPerNumpar = CBool(oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "UsaNumdocPerNumpar", "0", " ", "0")) ' -1 = utilizza sempre tm_numdoc per def. il numero partita da indicare nella reg. contabile e in scadenziario
        bGestDatecomp = CBool(oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "GestDatecomp", "0", " ", "0"))
        bCeas = CBool(oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "Ceas", "0", " ", "0")) ' -1 = pers Ceas  ' NON DOCUMENTARE
        bGirocontoIvaIndedRipartito = CBool(oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "GirocontoIvaIndedRipartito", "0", " ", "0"))
        bGestPVR = CBool(oCldPnfa.GetSettingBus("OPZIONI", ".", ".", "GestPVR", "0", " ", "0"))
        bRidCodClieDebTurnoDest = CBool(oCldPnfa.GetSettingBus("BSCGDIST", "OPZIONI", ".", "RidCodClieDebTurnoDest", "0", " ", "0"))   ' NON DOCUMENTARE
        strUsaContoFattDoc = oCldPnfa.GetSettingBus("BSVEFADI", "OPZIONI", ".", "UsaContoFattDoc", "0", " ", "0")
        bUsaContoFatt = CBool(Val(oCldPnfa.GetSettingBus("BSVEFADI", "OPZIONI", ".", "UsaContoFatt", "0", " ", "0")))
        Select Case strUsaContoFattDoc
          Case "-1", "0", "1", "2"
          Case Else
            LogWrite(oApp.Tr(Me, 129670343072938315, _
              "L'opzione di registro 'BSVEFADI/OPZIONI/UsaContoFattDoc' indicata ('|" & strUsaContoFattDoc & "|')" & vbCrLf & _
              "    non rientra fra quelli ammessi ('-1', '0', '1', '2')." & vbCrLf & _
              "    Elaborazione/stampa non possibili."), True)
            Return False
        End Select
      End If
      'leggo da BSCGGPRI quale data proporre come data plafond
      strDataPlafondProposta = oCldPnfa.GetSettingBus("BSCGPRIN", "OPZIONI", ".", "DataPlafondProposta", "D", " ", "D")
      'leggo da opzioni generali se l'azienda gestisce i PVR
      '---------------------
      'Personalizzazione DRACMA per ereditare TESTMAG.tm_riferim in:
      'PRINOT.pn_descr e SCADEN.sc_descr
      bRiferimFattureinCG = CBool(oCldPnfa.GetSettingBus("OPZIONI", ".", ".", "RiferimFattureinCG", "0", " ", "0")) 'non documentare. personalizzaz dracma
      strRiferimInDescr = NTSCStr(oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "RiferimFattureinDescr", "", " ", "")).Trim   'elenco di tipirk, separati da , (es "A,B,C") per i quali tm_riferim deve essere riversato in pn_descr e sc_descr (SOLO sulle righe gestite a scadenze)
      If strRiferimInDescr <> "" Then strRiferimInDescr = ("," & strRiferimInDescr & ",").Replace(",,", ",").Replace(" ", "").ToUpper 'normalizzo nel formato ",A,B,C,"
      bTestBudget = CBool(oCldPnfa.GetSettingBus("OPZIONI", ".", ".", "TestBudget", "-1", " ", "-1"))
      strNaturaOperazDaCodiva = oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "NaturaOperazDaCodiva", " ", " ", " ").ToUpper.Trim
      bCaDcRaggruppaCorr = CBool(oCldPnfa.GetSettingBus("BSVEFADI", "OPZIONI", ".", "CaDcRaggruppaCorr", "-1", " ", "-1")) 'se TRUE in contabilizzazione di corrispettivi raggruppati le righe di ca2 dovranno essere raggruppate, e non una per ogni riga di movmag (si perde il collegamento con movmag)
      'bCollega_MG_DI = CBool(oCldPnfa.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "Collega_MG_DI", "0", " ", "0"))
      'Cambiamento da NET 2015, si legge da ANADITAC (dati aggiuntivi contabilità):
      bCollega_MG_DI = oCldPnfa.CollegaMGDI(strDittaCorrente)

      'se ho spuntato di generare la CA da form, verifico se ho il modulo attivo. se non è attivo non genero la CA
      If bCa Then
        If CBool(ModuliDittaDitt(strDittaCorrente) And bsModCI) = False Then bCa = False
      End If

      bCa2 = CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupCAE)  'Contabilità analitica duplice contabile
      If bCa2 Then
        'se ho indicato in TABESCO che non gestisco la CA2 per questo esercizio, non devo gestire la CA2
        oCldPnfa.ValCodiceDb(nEscomp.ToString, strDittaCorrente, "TABESCO", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          If NTSCStr(dttTmp.Rows(0)!tb_gestcadp) = "N" Then bCa2 = False
        End If
        dttTmp.Clear()
      End If

      If bCa2 Then
        'se ho la CA2 devo sempre generare le reg. di CA
        bCa = True  'non vuol dire vecchia CA, ma flag 'Genera reg. CA'!!!!!
        'parcellazione non opera con CA2
        If strPrgParent = "BNPAPNPA" Then
          LogWrite(oApp.Tr(Me, 129648682000595703, "Attenzione! Non è possibile usare il programma di contabilizzazione parcelle se è abilitato il modulo di Contabilità Analitica Estesa. Impossibile proseguire."), CBool(IIf(oApp.Batch, True, False)))
          Return False
        End If
        If Not oCldPnfa.GetTabPecxInnerAnagca(strDittaCorrente, dttPecx, dttAnagcaPecx) Then Return False
        If dttPecx.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 129279140770507813, "Con la 'Contabilità analitica duplice contabile' attiva deve essere compilata la tabella 'Personalizzazione Contabilità analitica ditta'."), CBool(IIf(oApp.Batch, True, False)))
          Return False
        End If
        With dttPecx.Rows(0)
          If NTSCInt(!tb_dcontocoll) = 0 Or NTSCInt(!tb_dcontocolls) = 0 Or NTSCInt(!tb_dcontocolld) = 0 Or _
             NTSCInt(!tb_acontocoll) = 0 Or NTSCInt(!tb_acontocolls) = 0 Or NTSCInt(!tb_acontocolld) = 0 Then
            LogWrite(oApp.Tr(Me, 129279141871923828, "Con la 'Contabilità analitica duplice contabile' attiva la tabella 'Personalizzazione Contabilità analitica ditta' deve essere compilata in tutte le sue parti."), CBool(IIf(oApp.Batch, True, False)))
            Return False
          End If
        End With
      End If

      '---------------------
      'memorizzo se la gestione degli effetti attivi è del tipo 'senza chiusura del cliente'
      oCldPnfa.ValCodiceDb(strDittaCorrente, strDittaCorrente, "ANADITAC", "S", "", dttTmp)
      bGiroEffettiNoChisCli = CBool(IIf(dttTmp.Rows(0)!ac_gestefcc.ToString = "S", False, True))
      dttTmp.Clear()


      '---------------------
      If Not TestPreElabora() Then
        LogWrite(oApp.Tr(Me, 128768487580759000, "L'elaborazione non ha superato i test pre-elaborazione ed è stata interrotta."), CBool(IIf(oApp.Batch, True, False)))
        Return False
      End If

      If strPrgParent = "BNPAPNPA" And bModuliAcquistati = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129648711644375000, _
                          "Elaborazione interrotta: non è possibile contabilizzare documenti se non si possiede il modulo di Contabilità generale")))
        Return False
      End If

      '---------------------
      'Legge ANADITAC per capire se l'esigibilità differita è gestita col vecchio o nuovo sistema
      bGestStanziamenti = False
      oCldPnfa.ValCodiceDb(strDittaCorrente, strDittaCorrente, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        strOldgesived = "S"
      Else
        strOldgesived = NTSCStr(dttTmp.Rows(0)!ac_gesived)
        bGestStanziamenti = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_geststanz) = "S", True, False))
      End If
      dttTmp.Clear()

      If bCa2 And bGestStanziamenti Then
        LogWrite(oApp.Tr(Me, 129278269843417968, "Con la 'Contabilità analitica duplice contabile' attiva la gestione stanziamenti deve essere disabilitata in dati aggiuntivi contabilità (anagrafica ditta)"), True)
        Return False
      End If

      '--------------------
      'If Not LeggiTabpevePeacPecg() Then Return False

      oCldPnfa.LeggiTabellaSemplice(strDittaCorrente, "TABCOVE", dsCove)
      Dim col(1) As System.Data.DataColumn
      dsCove.Tables("TABCOVE").PrimaryKey = col


      '--------------------
      'per velocizzare il tutto memorizzo TABCIVA in memoria
      oCldPnfa.LeggiTabellaSemplice(strDittaCorrente, "TABCIVA", dsTabciva)
      'compatiblità con reverse charge fino net 2012: se è presente la colonna tb_revcharge ma nessun record è diverso da 'n' ignoro la colonna
      If dsTabciva.Tables("TABCIVA").Columns.Contains("tb_revcharge") Then
        If dsTabciva.Tables("TABCIVA").Select("tb_revcharge <> 'N'").Length = 0 Then
          dsTabciva.Tables("TABCIVA").Columns.Remove("tb_revcharge")
        End If
        dsTabciva.Tables("TABCIVA").AcceptChanges()
      End If

      '--------------------
      'preparo i datatable temporanei per la registraz. di prima nota
      If dttPN.Columns.Count = 0 Then
        oCldPnfa.GetTableStructure("PRINOT", False, dttPN)
        oCldPnfa.GetTableStructure("MOVIVA", False, dttMO)
        oCldPnfa.GetTableStructure("SCADEN", False, dttSC)
        oCldPnfa.GetTableStructure("PRIANA", False, dttCA)
        If bCa2 Then oCldPnfa.GetTableStructure("PRIANA2", False, dttCA2)
        oCldPnfa.SetTableDefaultValueFromDB("PRINOT", dttPN.DataSet)
        oCldPnfa.SetTableDefaultValueFromDB("MOVIVA", dttMO.DataSet)
        oCldPnfa.SetTableDefaultValueFromDB("SCADEN", dttSC.DataSet)
        oCldPnfa.SetTableDefaultValueFromDB("PRIANA", dttCA.DataSet)
        If bCa2 Then oCldPnfa.SetTableDefaultValueFromDB("PRIANA2", dttCA2.DataSet)
      End If

      'temporaneo che conterrà l'elenco delle contropartite
      If dttControp.Columns.Count = 0 Then
        dttControp.Columns.Add("lControp", GetType(Integer))
        dttControp.Columns.Add("dImporto", GetType(Decimal))
        dttControp.Columns.Add("dImpVal", GetType(Decimal))
        dttControp.Columns.Add("dIvaInded", GetType(Decimal))
        dttControp.Columns.Add("dIvaIndedVal", GetType(Decimal))
        dttControp.Columns.Add("strDtIniz", GetType(String))
        dttControp.Columns.Add("strDtFin", GetType(String))
        dttControp.Columns.Add("strDtInizOrig", GetType(String))
        dttControp.Columns.Add("strDtFinOrig", GetType(String))
        dttControp.Columns.Add("lConto", GetType(Integer))
        dttControp.Columns.Add("strIvaInded", GetType(String))
      End If

      dttPN.AcceptChanges()
      dttMO.AcceptChanges()
      dttSC.AcceptChanges()
      dttCA.AcceptChanges()
      If bCa2 Then dttCA2.AcceptChanges()
      dttControp.AcceptChanges()

      '--------------------------------------------------------------------------------------------------------------
      bAutotrasportatore = False
      '--------------------------------------------------------------------------------------------------------------
      If bMovCassa AndAlso bRielab Then EliminaRegistrazioniMovimentiCassa()
      '--------------------------------------------------------------------------------------------------------------
      'passo tutti i tipork per lanciare la routine standard 'Esegui' con il parametro giusto
      For i = 0 To strT.Length - 1
        bIncassiTmp = bIncassi
        If strT(i) = "B" And bContIncDDT And bBnrepnco And bIncassi Then bIncassi = False 'se contabilizzo gli incassi anticipati su DDT emessi non devo contabilizzare anche il DDT effettivo
        ElaboraTipo(strT(i))
        bIncassi = bIncassiTmp
      Next
      '--------------------------------------------------------------------------------------------------------------
      'Contabilizza i movimenti di cassa
      If bMovCassa Then ContabilizzaMovimentiDiCassa()
      '--------------------------------------------------------------------------------------------------------------

      '---------------------
      'verifico ed eventualmente segnalo di registro IVA/protocollo/serie protocollo doppi
      'visto he il protocollo 'doppio' potrebbe essere fuori elaborazione, verifico sempre tutto l'anno solare
      If bCheckProtocolli Then
        If bDerogaIva Then
          nAnnoIva = NTSCDate(strDAIva).Year
        ElseIf strDatregForm <> "" Then
          nAnnoIva = NTSCDate(strDatregForm).Year
        Else
          nAnnoIva = NTSCDate(strDatdocDa).Year
        End If
        If bAutotrasportatore = False Then
          ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 129768046339286109, "Controllo protocolli doppi in corso ...")))
          oCldPnfa.GetProtocolloDoppi(strDittaCorrente, nAnnoIva, dttTmp)
          If dttTmp.Rows.Count > 0 Then
            Dim dttDistinct As DataTable = dttTmp.DefaultView.ToTable(True, "mi_nregiva", "mi_tregiva", "tb_desduri")
            Dim strTmp As String = ""
            For i = 0 To dttDistinct.Rows.Count - 1
              Select Case dttDistinct.Rows(i)!mi_tregiva.ToString
                Case "A" : strTmp += "Acquisti"
                Case "V" : strTmp += "Vendite"
                Case "C" : strTmp += "Corrispettivi"
                Case "S" : strTmp += "In sospensione"
                Case "T" : strTmp += "In sospensione Acquisti"
                Case Else : strTmp += ""
              End Select
              strTmp += " - Numero registro: " & dttDistinct.Rows(i)!mi_nregiva.ToString & " (" & dttDistinct.Rows(i)!tb_desduri.ToString & ")" & vbCrLf
            Next
            LogWrite(oApp.Tr(Me, 129768044985483717, "Attenzione: nell'anno solare '|" & nAnnoIva & "|' sono presenti dei protocolli IVA doppi. " & _
            "I registri interessati sono i seguenti:" & vbCrLf & strTmp & " Eseguire la stampa dei registri IVA per ottenere maggiori informazioni"), True)
          End If
        End If
      End If

      '---------------------
      'scrivo actlog
      oCldPnfa.ScriviActLog(strDittaCorrente, "BSVEPNFA", "BSVEPNFA", "", "", "M", "E", strDesogglog, False)

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
      LogStop()
      bInElaborazione = False
    End Try
  End Function
  Public Overridable Function ElaboraTipo(ByVal strTipork As String) As Boolean
    '--------------------
    'elaboro un singolo tipo documento
    Dim dttTm As New DataTable      'contiene i documenti da contabilizzare eventualmente raggruppati per 'corrispettivi o ric fisc incassate totalmente'
    Dim dttTmOrig As New DataTable  'contiene i documenti da contabilizzare letti da DB
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Dim strErr As String = ""
    Dim bOk As Boolean = False
    Dim strAnscaden As String = "S"
    Dim lTmp As Integer = 0
    Dim i As Integer = 0
    Dim e As Integer = 0
    Dim j As Integer = 0
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim nNumaliq As Integer = 8
    Dim bStornaStanziamenti As Boolean = False
    Dim strCausaleStornoStanz As String = ""
    Dim bAggProtP As Boolean = False
    Dim strAlfdocP As String = ""
    Dim lNumdocP As Integer = 0
    Dim bAggProtP2 As Boolean = False
    Dim strAlfdocP2 As String = ""
    Dim lNumdocP2 As Integer = 0
    Dim dttScad As New DataTable
    Dim dTmp As Decimal = 0
    Dim oDttgr As New CLEGROUPBY
    Dim dttGr As New DataTable
    Dim bCorrRagg As Boolean = False   'se TRUE corrispettivi e ricevute fiscali emesse TOTALMENTE incassati vengono raggruppati

    'servono per memorizzare i vecchi estremi di partita qualora nel documento in analisi siano stati cambiati
    'inoltre vengono utilizzati per poter cancellare la fattura in CG nel momento in cui si fa una rielaborazione
    Dim lContoOld As Integer = 0, nAnnoOld As Integer = 0, strSerieOld As String = "", lNumdocOld As Integer = 0
    Dim strDatregftOld As String = "", lNumregftOld As Integer = 0
    Dim strDatreginOld As String = "", lNumreginOld As Integer = 0
    Dim strDatregomOld As String = "", lNumregomOld As Integer = 0
    Dim strTipoNumerazVendPerAcqIntra As String = "A"   'regsitro IVA vendite dove registrare fatture acq intra
    Dim strTmp As String = ""
    Dim strT() As String = Nothing
    Dim strSerieDocEm As String = " "       'seri doc. emessi letta dalla causale di CG (per rsm/intra)

    Dim dttScadRielab As New DataTable      'in caso di rielaborazione, contiene le scadenze collegate alla fattura ed un flag che avvisa se devono essere cancellate, modificate, ecc..

    Dim dtrDataRow() As DataRow = Nothing
    Dim dtDatareg As DateTime
    Dim nNumreg As Integer = 0
    Dim bFatturaFigurativaCancellabile As Boolean = True

    Try
      strTipork = strTipork.ToUpper
      If strTipork.Trim = "" Then Return True 'Non c'era nulla da elaborare

      'descrizione del tipo documento
      strTipodoc = TipoDocumento(strTipork)
      lRkfatt = 0 : lRkomag = 0 : lRkincas = 0 : dTdare = 0 : dTavere = 0 : lNumRegCorr = 0
      lNumreg = 0 : lNumregFatt = 0 : lNumregOmaggi = 0 : lNumregIncasso = 0 : lNumregStanziamenti = 0

      '--------------------
      'eseguo i controlli preliminare per verificare se posso eseguire una operazione di contabilizzazione di notule
      If strTipork = "3" Then
        If Not TestPrecontrolliNotule() Then Return False
      End If

      '--------------------
      'se non ho la CG posso contabilizzare solo documenti emessi, non documenti ricevuti
      If bModuliAcquistati = False Then
        If strTipork = "J" Or strTipork = "K" Or strTipork = "L" Or strTipork = "M" Or strTipork = "T" Or strTipork = "(" Then
          LogWrite(oApp.Tr(Me, 128769428561141000, "Non è possibile contabilizzare i documenti di tipo |'" & strTipodoc & "'| in quanto i programmi di Contabilità non risultano essere tra i moduli acquistati"), True)
          Return False
        End If
      End If

      '--------------------
      If strTipork = "I" Or strTipork = "F" Then
        If bIncassi = False Then
          LogWrite(oApp.Tr(Me, 128769443096971000, "Quando si chiede la contabilizzazione di ricevute fiscali è necessario contabilizzare anche gli incassi."), True)
          Return False
        End If
      End If

      Select Case strTipork
        Case "M", "Z", "T", "U"
          LogWrite(oApp.Tr(Me, 128769442453783000, "In questa versione non e' possibile la contabilizzazione del documento di tipo |'" & strTipodoc & "'|."), True)
          Return False
      End Select

      If bGenEffetti Then
        If bRielab = True And bDelEffettiNoPres = False Then
          LogWrite(oApp.Tr(Me, 130397971390280672, "In rielaborazione documenti già elaborati non è possibile generare gli effetti se non è anche spuntato 'Cancella emiss. effetti collegati a fattura NON pres. banca'"), True)
          Return False
        End If
      End If

      If bBnrepnco And (strTipork = "C" Or strTipork = "F") Then
        bCorrRagg = True
      End If

      If bCorrRagg Then
        If nTipobf <> 0 Or lNumdocDa <> 0 Or lNumdocA <> 999999999 Then
          LogWrite(oApp.Tr(Me, 129592790799941406, "ATTENZIONE: nell'elaborazione di corrispettivi o Ricevute fiscali emesse 'con raggruppamento' i documenti devono essere contabilizzati da numero 0 a numero 999999999 e il tipo bolla/fattura deve essere impostato a 0. Elaborazione interrotta"), True)
          Return False
        End If
        If bIncassi = False Then
          LogWrite(oApp.Tr(Me, 129592793228935547, "ATTENZIONE: nell'elaborazione di corrispettivi o Ricevute fiscali emesse 'con raggruppamento' il flag 'Contabilizza anche incassi associati' deve essere spuntato. Elaborazione interrotta"), True)
          Return False
        End If
      End If

      '--------------------
      'non possono essere contabilizzati i ddt se c'è la ca2 (ma gli incassi sui ddt - gli acconti - si)
      If bCa2 And strTipork = "B" And bContIncDDT = False Then
        LogWrite(oApp.Tr(Me, 129276479925387969, "Con la 'Contabilità analitica duplice contabile' attiva non è possibile contabilizzare i DDT emessi (possono essere contabilizzati solo i relativi incassi come 'acconti')."), True)
        Return False
      End If

      '--------------------
      'ottengo l'elenco dei documenti da contabilizzare
      If strPrgParent = "BNPAPNPA" Then
        'parcelle
        If Not oCldPnfa.GetDocumentiDaContabParcelle(strDittaCorrente, strTipork, strUsaContoFattDoc, nAnnoDoc, _
                                              strDatdocDa, strDatdocA, strSeriedocDa, strSeriedocA, lNumdocDa, _
                                              lNumdocA, bRielab, nTipobf, dttTm) Then Return False
      Else
        'documenti di magazzino
        strTmp = strUsaContoFattDoc
        If strTipork = "B" And bContIncDDT Then
          If strUsaContoFattDoc <> "0" Or bUsaContoFatt Then
            strTmp = "2"
          End If
        End If
        If Not oCldPnfa.GetDocumentiDaContab(strDittaCorrente, strTipork, strTmp, nAnnoDoc, _
                                              strDatdocDa, strDatdocA, strSeriedocDa, strSeriedocA, lNumdocDa, _
                                              lNumdocA, bRielab, nTipobf, dttTm, bBnrepnco) Then Return False


        If strPrgParent = "BNREPNCO" AndAlso strTipork = "C" Then
          Dim dtrRow() As DataRow = dttTm.Select("tm_scostp = 'N' AND tm_retail = 'S'")
          For z As Integer = 0 To dtrRow.Length - 1
            LogWrite(oApp.Tr(Me, 130353885442601428, "Non è stato stampato lo scontrino per il corrispettivo |" & _
                             NTSCInt(dtrRow(z)!tm_anno) & " " & CStrSQL(dtrRow(z)!tm_serie) & " " & NTSCInt(dtrRow(z)!tm_numdoc) & "|."), True)
          Next
        End If
      End If

      'contabilizzazione incassi su ddt emessi: scarto i record che non sono incasati
      If strTipork = "B" And bContIncDDT Then
        For Each dtrT As DataRow In dttTm.Select("tm_pagato = 0 AND tm_pagato2 = 0")
          dtrT.Delete()
        Next
        dttTm.AcceptChanges()
      End If
      '--------------------------------------------------------------------------------------------------------------
      strTmp = ""
      If dttTm.Rows.Count > 0 Then
        If CBool(oCldPnfa.GetSettingBus("BSCGPRIN", "OPZIONI", ".", "SegnalaCreazDocumCliFornBloccati", "0", " ", "0")) = True Then
          oDttgr = New CLEGROUPBY
          dttGr.Clear()
          If oDttgr.NTSGroupBy(dttTm, dttGr, "tm_conto", "", "tm_conto") = True Then
            If dttGr.Rows.Count > 0 Then
              For i = 0 To (dttGr.Rows.Count - 1)
                If oCldPnfa.ValCodiceDb(NTSCStr(dttGr.Rows(i)!tm_conto), strDittaCorrente, "ANAGRA", "N", "", dttTmp1) = True Then
                  With dttTmp1.Rows(0)
                    If (NTSCStr(!an_tipo) <> "S") And (NTSCStr(!an_blocco) = "B") Then
                      strTmp += "    . Cliente: " & NTSCStr(!an_conto) & _
                        IIf(NTSCStr(!an_descr1).Trim <> "", " - ", "").ToString & NTSCStr(!an_descr1).Trim & _
                        " scartato in quanto con 'Blocco Fisso'" & vbCrLf
                      LogWrite(strTmp, True)
                      For Each dtrT As DataRow In dttTm.Select("tm_conto = " & NTSCInt(!an_conto))
                        dtrT.Delete()
                      Next
                      dttTm.AcceptChanges()
                    End If
                  End With
                End If
              Next
            End If
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If dttTm.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128769457274119000, "Non esistono documenti di tipo |'" & strTipodoc & "'| da trattare con queste caratteristiche."), True)
        Return False
      End If

      '--------------------
      'su corrispettivi da RETAIL l'anno del documento non è certo, visto che la contabilizzazione opera per data chisura di cassa,
      'ma l'apertura di cassa potrebbe essere avvenuta il 31/12 e la chiusura alle 4 di mattino del 01/01 dell'anno dopo
      'fiscalmente non sarebbe corretto, ma è prassi comune caricare tutti i corrispettivi in data apertura di cassa, non data chisura
      'già la query per leggere i documenti da contabilizzare ignora l'anno ed usa la data chisura di cassa
      'QUI ricorreggo l'anno dei documenti prendendo quello del primo documento da contabilizzare
      If bBnrepnco And (strTipork = "C" Or strTipork = "F") Then
        nAnnoDoc = NTSCInt(dttTm.Select("", "tm_anno ASC")(0)!tm_anno)
      End If


      '--------------------
      'se documento RETAIL e tipork = 'C' o 'F', devo raggruppare il datatable per tutti i documenti 
      'totalmente incassati in modo da fare una registraz. unica
      'oppure se non ho retail na ho settato l'opzione per raggrupparei i corrispettivi (caso gestito ma non ancora previsto)
      If bCorrRagg Then
        If Not RaggruppaCorrIncassati(strTipork, dttTm, dttTmOrig, bBnrepnco) Then Return False
      End If

      '--------------------
      'solo per retail, la contropartica cassa/banca ha la seguente priorità:
      'prima la controp della tabella specifica REPCPAGA.pp_contrinc 
      'se non impostat oprendo quella di tabpaga (come standard)
      'se manca prendo quella di tabpeve, tabpeac
      'LO FACCIO SEMPRE, altrimenti se abilito la contabilizzaz. automatica dei doc, da retail si comporta in modo diverso se contabilizza da PNFA o da fare la contabilizzaz. in automatico
      'per i documenti non da retail non cambia nulla, visto che tm_codrepc è = ' '
      'If bBnrepnco Then
      For Each dtrT As DataRow In dttTm.Rows
        If strPrgParent <> "BNPAPNPA" AndAlso NTSCStr(dtrT!tm_codrepc).Trim <> "" Then
          If NTSCInt(dtrT!tm_codpaga) <> 0 Then
            i = oCldPnfa.GetContropFromRepcpaga(strDittaCorrente, NTSCStr(dtrT!tm_codrepc), NTSCInt(dtrT!tm_codpaga))
            If i <> 0 Then dtrT!tb_concassp = i
          End If
          If NTSCInt(dtrT!tm_codpaga2) <> 0 Then
            i = oCldPnfa.GetContropFromRepcpaga(strDittaCorrente, NTSCStr(dtrT!tm_codrepc), NTSCInt(dtrT!tm_codpaga2))
            If i <> 0 Then dtrT!tb_concassp2 = i
          End If
        End If
      Next
      dttTm.AcceptChanges()

      'ora devo fare la stessa cosa sul datatable originario, visto che viene utilizzato questo per rilevare 
      'gli effettivi conti di incasso/pagamento nella scriviriga25000
      For Each dtrT As DataRow In dttTmOrig.Rows
        If strPrgParent <> "BNPAPNPA" AndAlso NTSCStr(dtrT!tm_codrepc).Trim <> "" Then
          If NTSCInt(dtrT!tm_codpaga) <> 0 Then
            i = oCldPnfa.GetContropFromRepcpaga(strDittaCorrente, NTSCStr(dtrT!tm_codrepc), NTSCInt(dtrT!tm_codpaga))
            If i <> 0 Then dtrT!tb_concassp = i
          End If
          If NTSCInt(dtrT!tm_codpaga2) <> 0 Then
            i = oCldPnfa.GetContropFromRepcpaga(strDittaCorrente, NTSCStr(dtrT!tm_codrepc), NTSCInt(dtrT!tm_codpaga2))
            If i <> 0 Then dtrT!tb_concassp2 = i
          End If
        End If
      Next
      dttTmOrig.AcceptChanges()
      'End If

      lNumRegTot = dttTm.Rows.Count

      '--------------------
      'inizio il loop sui documenti da generare
      For Each dtrTm As DataRow In dttTm.Rows
        strIEDDatreg = ""
        lIEDNumreg = 0
        nIEDRigareg = 0
        nCADoc = 0
        bAggProtP = False
        bAggProtP2 = False
        lNumregFatt = 0
        lNumregOmaggi = 0
        lNumregIncasso = 0
        lNumregStanziamenti = 0
        lContoOld = 0 : nAnnoOld = 0 : strSerieOld = "" : lNumdocOld = 0
        strDatregftOld = "" : lNumregftOld = 0
        strDatreginOld = "" : lNumreginOld = 0
        strDatregomOld = "" : lNumregomOld = 0
        dTotivaRC = 0 : dTotivavRC = 0

        lNumRegCorr += 1
        dttControp.Clear()
        dttPN.Clear()
        dttMO.Clear()
        dttSC.Clear()
        dttCA.Clear()
        If bCa2 Then dttCA2.Clear()
        dttControp.AcceptChanges()
        dttPN.AcceptChanges()
        dttMO.AcceptChanges()
        dttSC.AcceptChanges()
        dttCA.AcceptChanges()
        If bCa2 Then dttCA2.AcceptChanges()

        strEstremidoc = oApp.Tr(Me, 128770982975606000, "di tipo |'" & strTipodoc & "'| n.|" & _
                            dtrTm!tm_numdoc.ToString & IIf(dtrTm!tm_serie.ToString <> " ", "/" & dtrTm!tm_serie.ToString, "").ToString & "|" & _
                            " del |" & dtrTm!tm_anno.ToString & "|")


        If NTSCStr(dtrTm!tm_flagiva_1).ToUpper = "S" Then
          LogWrite(oApp.Tr(Me, 129302213191757813, "ATTENZIONE: Il documento |" & strEstremidoc & "| NON CONTABILIZZATO perchè deve essere rielaborato (dopo essere stato generato uno o più documenti in esso contenuti sono stati modificati/cancellati)"), True)
          GoTo eseguinext
        End If

        ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 128769973837496000, _
                                          "Registrazione |" & strTipodoc & " " & lNumRegCorr & "| di |" & lNumRegTot & "| in corso (|" & _
                                          dtrTm!tm_anno.ToString & "/" & dtrTm!tm_serie.ToString & "/" & dtrTm!tm_numdoc.ToString & "|) ...")))


        nAggControp = 0
        If bUsaAggContropAnagra Then
          'ho impostato che per le spese di piede leggo le contropartite da peve/peac ma, se in anagra è stato impostato 
          'l'aggiunta contropartita, verifico ed eventualmente prendo il sottoconto con l'aggiunta contropartita.
          'se il conto non esiste prendo quello standard di peve/peac
          LeggiTabpevePeacPecgConto(NTSCInt(dtrTm!tm_conto), NTSCStr(dtrTm!tm_datdoc))
        End If

        If (NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_totomag)) <= (NTSCDec(dtrTm!tm_pagato) + NTSCDec(dtrTm!tm_pagato2) - NTSCDec(dtrTm!tm_resto) + NTSCDec(dtrTm!tm_abbuono)) Then
          bTotinc = True : bParzinc = False : bNotinc = False
        Else
          If NTSCDec(dtrTm!tm_pagato) = 0 And NTSCDec(dtrTm!tm_pagato2) = 0 Then
            bTotinc = False : bParzinc = False : bNotinc = True
          Else
            bTotinc = False : bParzinc = True : bNotinc = False
          End If
        End If
        'determino il sottoconto da utilizzare per i pagamenti solo per le ricevute fiscali ...
        If strTipork = "F" Or strTipork = "I" Or strTipork = "P" Or strTipork = "S" Then
          If NTSCInt(dtrTm!tb_concassp) > 0 Then
            lConcas = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrTm!tb_concassp))
          Else
            lConcas = lConcasPeve  'rimetto in lConcas il valore di default letto da pers vendite
          End If
          If NTSCInt(dtrTm!tb_concassp2) > 0 Then
            lConcas2 = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrTm!tb_concassp2))
          Else
            lConcas2 = lConcasPeve  'rimetto in lConcas2 il valore di default letto da pers vendite
          End If
        End If
        ' per le ric. fiscali (da vedere se va bene considerarae abbuoni/omaggi in ...)
        dImpincassato = NTSCDec(dtrTm!tm_pagato) + NTSCDec(dtrTm!tm_abbuono) + NTSCDec(dtrTm!tm_pagato2) - NTSCDec(dtrTm!tm_resto)
        dImpnotincassato = (NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_totomag)) - (NTSCDec(dtrTm!tm_pagato) + NTSCDec(dtrTm!tm_abbuono) + NTSCDec(dtrTm!tm_pagato2) - NTSCDec(dtrTm!tm_resto))
        dImpincassatov = (NTSCDec(dtrTm!tm_pagatov) + NTSCDec(dtrTm!tm_abbuonov))
        dImpnotincassatov = (NTSCDec(dtrTm!tm_totdocv) - NTSCDec(dtrTm!tm_totomagv)) - (NTSCDec(dtrTm!tm_pagatov) + NTSCDec(dtrTm!tm_abbuonov))
        nCausaleTpbf = NTSCInt(dtrTm!tb_codcauc)
        ' legge tipo bolla fattura per dist. prest. di servizio
        oCldPnfa.ValCodiceDb(NTSCInt(dtrTm!tm_tipobf).ToString, strDittaCorrente, "TABTPBF", "N", "", dttTmp)
        strPrestserv = NTSCStr(dttTmp.Rows(0)!tb_prestserv)
        strAutotr = NTSCStr(dttTmp.Rows(0)!tb_autotr)
        ' controlla se ric. fiscale e segue fattura o fattura ric. fiscale
        If strTipork = "F" And (NTSCStr(dttTmp.Rows(0)!tb_flresocl) = "S" Or NTSCStr(dttTmp.Rows(0)!tb_flresocl) = "F") Then
          LogWrite(oApp.Tr(Me, 129889780776584275, "ATTENZIONE: Il documento |" & strEstremidoc & "| NON CONTABILIZZATO perchè nel tipo bolla/fattura ha impostato 'Segue fattura' o 'Segue fattura ric.fisc'"), True)
          GoTo eseguinext2
        End If
        dttTmp.Clear()

        '--------------------
        'determina n. partita e data documento
        If Not MemPartitaeDatdoc(dtrTm) Then Return False

        '--------------------
        ' controlla e rielaborazione, se la rielab. e' possibile
        If bModuliAcquistati = True Then
          If strTipork <> "3" Then
            lTmp = RitornaContoPrimaRiga(NTSCInt(dtrTm!tm_conto), dtrTm, strAnscaden)
          Else
            'per le notule usa sempre il conto 'clienti notule'
            lTmp = lConClinot
          End If
        Else
          lTmp = lConEff
        End If

        If bRielab = True And dtrTm!tm_flcont.ToString = "S" Then
          strDatregftOld = NTSCDate(dtrTm!tm_datregef).ToShortDateString
          lNumregftOld = NTSCInt(dtrTm!tm_numregef)
          strDatreginOld = NTSCDate(dtrTm!tm_datregin).ToShortDateString
          lNumreginOld = NTSCInt(dtrTm!tm_numrgin)
          strDatregomOld = NTSCDate(dtrTm!tm_datregom).ToShortDateString
          lNumregomOld = NTSCInt(dtrTm!tm_numregom)

          bOk = oCldPnfa.TestrielabAndRielab(strDittaCorrente, dtrTm, lTmp, strAnscaden, nAnnpar, strAlfpar, _
                                             lNumpar, bInt, bGestStanziamenti, lContoOld, nAnnoOld, strSerieOld, _
                                             lNumdocOld, strErr, bContIncDDT, bGiroEffettiNoChisCli, bProfes)


          'se servirà, prelevo le scadenze collegate al documento
          If bOk = False And strAnscaden = "S" And (bDelEffettiNoPres Or bContabAncheConScadSald) And (strErr = "3" Or strErr = "8") Then

            If Not oCldPnfa.GetScadenFattura(strDittaCorrente, lContoOld, nAnnoOld, strSerieOld, lNumdocOld, False, _
                                         IIf(bInt, "S", "N").ToString, -1, dttScadRielab, "", 0, 0) Then
              Return False
            End If

            If strTipork = "B" And bContIncDDT Then
              'la nuova spunta 'Contabilizza anche documenti con scadenze saldate’ non è utilizzabile, 
              'visto che potrebbe già essere stata registrata anche la fattura differita. 
              'In questo caso dopo la registraz. Della fattura viene fatta una nuova registraz. 
              'Tra ddt e fattura per l’incasso, ma se è cambiato l’importo dell’acconto nel DDT verrebbe 
              'generata una regitraz. Sbilanciata in CG!!!!
              LogWrite(oApp.Tr(Me, 130378952003276378, "Flag 'Contabilizza anche documenti con scadenze saldate' ignorato in fase di contabilizzazione SOLO incassi su DDT emessi"), True)
            ElseIf strTipork = "N" And bContCompensNoteAcc Then
              'note accred emesse con compensazione con fatture emesse
              'non riesco a frlo pechè la scadenza risulta essere già saldata
              'ma non serve neanche, visto che in questi casi non esistono scadenze saldate al di fuori della registraz. della nota di accredito
              'in pratica in coda alla reg. di nota accred vengono inserire le righe di 'cliente' a 'cliente' per chiudere la partita della nota con quella della fattura
              LogWrite(oApp.Tr(Me, 130379719662761102, "Flag 'Contabilizza anche documenti con scadenze saldate' ignorato in fase di contabilizzazione Note Accredito emesse CON COMPENSAZIONE CON FATTURE EMESSE"), True)
            Else
              '--------------------------------------------------
              'ci sono dei problemi, verifico se posso continuare la contabilizzazione
              'anche se scadenze saldate
              'solo se non ho cambiato il cliente/fornitore intestatario del documento dall'ultima contabilizzazione
              If lContoOld = lTmp Or lContoOld = 0 And dttScadRielab.Rows.Count > 0 Then
                'sono presenti delle scadenze SALDATE: devo valutare se posso proseguire
                'e successivamente aggiusterò le scadenze oppure no
                If CheckForzaContabConScadenUpd(dtrTm, dttScadRielab, bDelEffettiNoPres, bContabAncheConScadSald, strErr) Then
                  'posso continuare. su dttScadRielab, per ogni scadenza, ho aggiunto delle colonne che mi diranno come modificare i record di scaden
                  strErr = ""
                  bOk = True
                End If
              Else
                strErr = "9"
              End If
            End If

          End If

        Else
          strErr = ""
          If strAnscaden = "S" Or (strPrgParent = "BNPAPNPA" And strTipork <> "3") Then
            bOk = oCldPnfa.TestNewDoc(strDittaCorrente, lTmp, bInt, nAnnpar, strAlfpar, lNumpar)
          Else
            bOk = True
          End If
        End If    'If bRielab = True And dtrTm!tm_flcont.ToString = "S" Then

        If Not bOk Then
          Select Case strErr
            Case "1"
              LogWrite(oApp.Tr(Me, 128770110498460000, "Documento |" & strEstremidoc & _
                                "| non elaborabile/rielaborabile. Registrazione contabile già esistente su registro Iva."), True)
            Case "2"
              LogWrite(oApp.Tr(Me, 128770111739686000, "Documento |" & strEstremidoc & _
                                "| non elaborabile/rielaborabile. Registrazione contabile già esistente su Libro Giornale."), True)
            Case "3"
              LogWrite(oApp.Tr(Me, 129440538790566406, "Documento |" & strEstremidoc & _
                                "| non elaborabile/rielaborabile. Esistono scadenze ATTIVE non saldate di tipo RB o TRATTA con flag 'Autorizzato' spuntato e Numero Distinta = 0: Probabilmente sulla scadenza sono stati emessi degli effetti attivi."), True)
            Case "4"
              LogWrite(oApp.Tr(Me, 129651548014140625, "Documento |" & strEstremidoc & _
                                "| non elaborabile/rielaborabile. Nella gestione professionisti la fattura risulta già essere saldata."), True)
            Case "5"
              LogWrite(oApp.Tr(Me, 129651550884746094, "Documento |" & strEstremidoc & _
                                "| non elaborabile/rielaborabile. Il documento risulta essere già stampato in definitivo sul registro cronologico professionisti"), True)
            Case "6"
              LogWrite(oApp.Tr(Me, 129651551195302734, "Documento |" & strEstremidoc & _
                                "| non elaborabile/rielaborabile. Il documento risulta essere già stampato in definitivo sul registro incassi/pagamenti professionisti"), True)
            Case "7"
              LogWrite(oApp.Tr(Me, 129651551316982422, "Documento |" & strEstremidoc & _
                                "| non elaborabile/rielaborabile. Il documento risulta essere già stampato in definitivo sul registro pagamenti professionisti"), True)
            Case "8"
              LogWrite(oApp.Tr(Me, 130375745609276895, "Documento |" & strEstremidoc & _
                  "| non elaborabile/rielaborabile. Sono già presenti scadenze SALDATE in CG e/o è cambiato l'intestatario del documento dall'ultima contabilizzazione"), True)
            Case "9"
              LogWrite(oApp.Tr(Me, 130375745645117107, "Documento |" & strEstremidoc & _
                  "| non elaborabile/rielaborabile. Sono già presenti scadenze SALDATE in CG non generate dalla precedente contabilizzazione del documento"), True)
            Case "10"
              LogWrite(oApp.Tr(Me, 130375742707012705, "Documento |" & strEstremidoc & _
                                "| non elaborabile/rielaborabile. Sono già presenti scadenze SALDATE in CG di importo superiore al residuo da incassare/pagare del documento (esclusi gli incassi/pagamenti/abbuoni indicati nel piede del documento)"), True)
            Case Else
              LogWrite(oApp.Tr(Me, 128770052335482000, "Documento |" & strEstremidoc & _
                               "| non elaborabile/rielaborabile. Sono già esistenti scadenze e/o saldi di scadenze sulla relativa partita.") & _
                               IIf(strPrgParent <> "BNPAPNPA", "", oApp.Tr(Me, 129648798600263672, ", oppure fattura già incassata nell'archivio professionisti")).ToString, True)
          End Select
          GoTo eseguinext
        End If

        '--------------------
        ' attribuisce n. registrazione
        strDatreg = NTSCDate(dtrTm!tm_datdoc).ToShortDateString
        If strDatregForm <> "" Then strDatreg = strDatregForm
        'DRACMA
        If bRiferimFattureinCG And NTSCStr(dtrTm!tm_datregef) <> "" And (NTSCStr(dtrTm!tm_tipork) = "L" Or NTSCStr(dtrTm!tm_tipork) = "J") Then
          strDatreg = NTSCDate(dtrTm!tm_datregef).ToShortDateString
        End If

        '--------------------
        'verifico la data registrazione con ultima stampa LG, RI, ...
        If Not TestDataRegistrazioneSuLgoRi(strDatreg, dtrTm) Then GoTo eseguinext

        If strTipork = "M" Or strTipork = "Z" Then GoTo EseguiIncasso

        If strTipork = "3" Then GoTo EseguiNotula

EseguiIniz:
        ' esegue contab. anche delle bolle B (RSM) priam reg. emissione fattura/nota accr.
        lNumreg = oCldPnfa.LegAggRegcDitt(strDittaCorrente, strDatreg)
        lNumregFatt = lNumreg
        lRiga = 1 ' n. riga
        lRigaivanew = 0 'n di riga IVA
        dTdare = 0 : dTavere = 0
        'registra riga di prinot (cliente/fornitore in dare/avere)

        '--------------------
        'determino il dare/avere del conto, del conto iva, la causale ecc
        If DeterminaCampiVari(strTipork) = False Then GoTo eseguinext ' se qualcosa va storto nella routine chiamata...

        strFattsosp = "N"
        strFattXtracee = "N"
        strFattRevCharge = "N"
        oCldPnfa.ValCodiceDb(NTSCInt(dtrTm!tm_tipobf).ToString, strDittaCorrente, "TABTPBF", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          strFattsosp = NTSCStr(dttTmp.Rows(0)!tb_fattsosp)
          strFattXtracee = NTSCStr(dttTmp.Rows(0)!tb_fattextrc)
          strFattRevCharge = NTSCStr(dttTmp.Rows(0)!tb_fattrevch)
        End If
        dttTmp.Clear()

        '--------------------
        ' legge le numerazioni per n.o registro e per causale/tipo reg se fatt. sospese
        'per corrispettivi/ric. fisc. emesse totalmente incassati da GPV cerca di prendere il num reg. iva dal puntocassa
        nMemnregiva = 0
        If bBnrepnco And (strTipork = "C" Or strTipork = "F") Then
          nMemnregiva = NTSCInt(dtrTm!tb_numregi)
          strTmptregiva = "C"
        End If
        If Not bCorrRagg Or nMemnregiva = 0 Then
          'caso standard o corr GPV senza indicazione di num .reg iva su punto cassa
          If Not oCldPnfa.GetNumregivaFromTabnuma(strDittaCorrente, strTiponuma, dtrTm!tm_serie.ToString, nAnnoDoc, _
                                                  nMemnregiva, strTmptregiva) Then
            If Not ((bContIncDDT = True And strTipork = "B") Or strFattXtracee = "S") Then
              'se devo contabilizzare solo gli incassi anticipati, faccio proseguire anche se manca la numerazione per i DDT emessi
              LogWrite(oApp.Tr(Me, 128770252238590000, "Manca numerazione |" & strTiponuma & _
                                dtrTm!tm_serie.ToString & nAnnoDoc.ToString & "|." & vbCrLf & _
                                "Per il documento |" & strEstremidoc & "|."), True)
              GoTo eseguinext
            End If
          End If

          If strTipork = "K" Or strTipork = "J" Or strTipork = "L" Or strTipork = "(" Then
            If strOpz19 = "S" Then nMemnregiva = NTSCInt(dtrTm!tm_nregiva)
          End If
        End If
        If nMemnregiva = 0 Then nMemnregiva = 1

        If (strTipork = "D" Or strTipork = "A" Or strTipork = "N" Or strTipork = "E" Or strTipork = "P" Or _
            strTipork = "S" Or strTipork = "£" Or strTipork = "J" Or strTipork = "K" Or strTipork = "L" Or _
            strTipork = "T" Or strTipork = "(") And strFattsosp = "S" Then ' --allora fattura in sost. ####
          If strTipork = "N" Or strTipork = "£" Then
            nCausale = nCaunoaf
            strMemtregiva = "V" ' non esiste  più il registro dlle fatture sospese come prima gestito!!!
          ElseIf strTipork = "K" Or strTipork = "L" Or strTipork = "J" Or strTipork = "T" Or strTipork = "(" Then
            'documento ricevuto
            nCausale = nCaurffs
            strMemtregiva = "A" ' non esiste  più il registro dlle fatture sospese come prima gestito!!!
          Else
            nCausale = nCauemfs
            strMemtregiva = "V" ' non esiste  più il registro dlle fatture sospese come prima gestito!!!
          End If
          ' pertanto vanno sul registro vendite anche le sel la nuemrazione e' per reg. sospeso
        Else

          If strPrgParent = "BNPAPNPA" Then
            'specifico per parcellazione
            If (strTipork = "1" Or strTipork = "2" Or strTipork = "4") And strFattsosp = "S" Then  ' --allora fattura in sost. ####
              If strTipork = "2" Then
                nCausale = nCaunoaf ' nota accred. ad esig. diff
              Else
                If strTipork = "1" Then
                  nCausale = nCauemfs ' fattura ad esig. diff.
                Else
                  ' ???
                End If
              End If
              strMemtregiva = "V" ' non esiste  più il registro dlle fatture sospese come prima gestito!!!
              ' pertanto vanno sul registro vendite anche le sel la nuemrazione e' per reg. sospeso
            Else
              If (strMemtregiva = "V" And (strTmptregiva = "S" Or strTmptregiva = "V")) Then
                'ok va bene
              Else
                If (strTmptregiva <> strMemtregiva) Then
                  LogWrite(oApp.Tr(Me, 129649010805214843, "Tipo registro in numerazione |" & TipoDocumento(strTiponuma) & "| Serie |'" & dtrTm!tm_serie.ToString & "'| anno |" & nAnnoDoc.ToString & "| errato!" & vbCrLf & _
                                  "Non coerente con documento |" & strEstremidoc & "|."), True)
                  GoTo eseguinext
                End If
              End If
            End If
          Else
            'caso normale
            If (strMemtregiva = "V" And (strTmptregiva = "S" Or strTmptregiva = "V")) Then
              'ok va bene
            Else
              If (strMemtregiva = "V" And (strTipork = "S" Or strTipork = "P") And (strTmptregiva = "C")) Then
                strMemtregiva = "C" ' reg. corrispettivi suvfatt/ric. fiscali ' ok, va bene ma cambio tipo registro
              Else
                If (strTmptregiva <> strMemtregiva) And Not ((strTipork = "I" And strPrestserv <> "S") Or (strTipork = "F" And strPrestserv = "S" And bNotinc)) Then
                  If Not (bContIncDDT = True And strTipork = "B") Then
                    LogWrite(oApp.Tr(Me, 128770259893646000, "Tipo registro in numerazione |" & TipoDocumento(strTiponuma) & "| Serie |'" & dtrTm!tm_serie.ToString & "'| anno |" & nAnnoDoc.ToString & "| errato!" & vbCrLf & _
                                 "Non coerente con documento |" & strEstremidoc & "|."), True)
                    GoTo eseguinext
                  End If
                End If
              End If
            End If
          End If    'If strPrgParent = "BNPAPNPA" Then
        End If    'If (strTipork = "D" Or strTipork = "A" Or strTipork = "N" Or strTi


        '--------------------
        'gestione reverse charge
        If strFattRevCharge <> "N" Then
          If (strTipork = "K" Or strTipork = "L") Then   ' --allora fattura acq. reverse charge
            nCausale = nCaurcfrc
          End If
          If (strTipork = "J" Or strTipork = "(") Then   ' --allora nota cred acq. reverse charge
            nCausale = nCaurcarc
          End If
        End If

        '--------------------
        ' gestione fatt. acquisto intracee (cambio causale)
        nCodntra = NTSCInt(dtrTm!tm_codntra)
        If (strTipork = "K" Or strTipork = "L") And nCodntra <> 0 Then   ' --allora fattura acq. cee
          nCausale = nCaurcftcee
        End If
        If (strTipork = "J" Or strTipork = "(") And nCodntra <> 0 Then   ' --allora nota cred acq. cee
          nCausale = nCaurcnacee
        End If

        '--------------------
        ' gestione fatt. acquisto extracee 
        If (strTipork = "K" Or strTipork = "L") And strFattXtracee = "S" Then   ' --allora fattura acq. xtracee
          nCausale = nCaurcftXcee
        End If
        If (strTipork = "J" Or strTipork = "(") And strFattXtracee = "S" Then   ' --allora nota cred acq. xtracee
          nCausale = nCaurcnaXcee
        End If

        '-------------------
        'DA NET 2012: posso prendere la causale da utilizzare in CG da tabtpbf: se impostata ha prevalenza rispetto a quella di peve/peac
        If nCausaleTpbf <> 0 Then nCausale = nCausaleTpbf

        ' legge la causale per vedere se e' coerente con il registro iva/tipo docuneto.
        oCldPnfa.ValCodiceDb(nCausale.ToString, strDittaCorrente, "TABCAUC", "N", "", dttTmp)
        If dttTmp.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 128770262829096000, "Causale contabile |" & nCausale & "| inesistente o non impostata in 'Personalizzazione acquisti' e/o 'Personalizzazione vendite' e/o Tabella tipi bolle/fatture per il tipo |" & dtrTm!tm_tipobf.ToString & "|."), True)
          GoTo eseguinext
        Else

          If strFattsosp = "S" Then
            If NTSCStr(dttTmp.Rows(0)!tb_tipmov) <> "O" And NTSCStr(dttTmp.Rows(0)!tb_tipmov) <> "Q" Then
              'Q = nota accred iva diff storno fattura (simula incasso), O = fattura o nota accred iva differita
              LogWrite(oApp.Tr(Me, 130633007466086315, "ATTENZIONE: Causale contabile |" & nCausale & "| non coerente con tipo bolla/fattura |" & dtrTm!tm_tipobf.ToString & "| per quanto riguarda il flag 'Iva differita'."), True)
              GoTo eseguinext
            End If
          Else
            If NTSCStr(dttTmp.Rows(0)!tb_tipmov) = "O" Or NTSCStr(dttTmp.Rows(0)!tb_tipmov) = "Q" Then
              LogWrite(oApp.Tr(Me, 130633008136465154, "ATTENZIONE: Causale contabile |" & nCausale & "| non coerente con tipo bolla/fattura |" & dtrTm!tm_tipobf.ToString & "| per quanto riguarda il flag 'Iva differita'."), True)
              GoTo eseguinext
            End If
          End If

          'se è una nota di accredito con IVA differita nuova non posso contabilizzarla (forse: sotti i test del caso)
          If strOldgesived = "N" And strFattsosp = "S" And (strTipork = "N" Or strTipork = "J" Or strTipork = "£" Or strTipork = "(") Then
            If NTSCStr(dttTmp.Rows(0)!tb_tipmov) <> "Q" Then
              'miglioria: posso contabilizzarla se non è a 'storno fattura'
            Else
              'se è a 'storno fattura' devo aver indicato la fattura di riferimento
              If Not CheckIvaDiffNostaStornoFt(dtrTm) Then GoTo eseguinext
            End If
          End If

          With dttTmp.Rows(0)
            strCautregiva = !tb_tipreg.ToString
            If strFattXtracee = "S" Then strCautregiva = "A" 'per far andare avanti senza errori
            lContoiva = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, NTSCInt(!tb_ccontriva), NTSCInt(!tb_contiva))
            lContoiva2 = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, NTSCInt(!tb_ccontriva2), NTSCInt(!tb_contiva2)) ' per fatt. acq. cee
            strCautregiva2 = !tb_tipreg2.ToString ' per fatt. acq. cee
            If bDocIntraNumreg2DaTabnuma Then
              'se abilitata l'opzione di reg., do priorità al reg. iva indicato nella numerazione vedite 
              'per la stessa serie documento utilizzata per reg. il doc. ricevuto, al posto di quello sulla causale di CG
              If oCldPnfa.GetNumregivaFromTabnuma(strDittaCorrente, "A", NTSCStr(dtrTm!tm_serie), nAnnoDoc, _
                                                  nCaunumregi2, strCautregiva2) Then
                If nCaunumregi2 = 0 Then nCaunumregi2 = NTSCInt(!tb_numregi2)
              Else
                nCaunumregi2 = NTSCInt(!tb_numregi2)
              End If
            Else
              nCaunumregi2 = NTSCInt(!tb_numregi2) ' per fatt. acq. cee
            End If
            strGestdtciva = !tb_gestdtciva.ToString
            strDescauc = !tb_descauc.ToString
            strTdociva = !tb_tdociva.ToString ' tipo doc iva

            strSerieDocEm = NTSCStr(!tb_seriedocem)

            If bModuliAcquistati = True Then
              oCldPnfa.GetTabduriTabatti(strDittaCorrente, strCautregiva, NTSCInt(!tb_numregi), nAnnoDoc, dttTmp1)
              If dttTmp1.Rows.Count > 0 Then
                If NTSCStr(dttTmp1.Rows(0)!tb_regautotr).ToUpper = "S" Then bAutotrasportatore = True
                If NTSCStr(dttTmp1.Rows(0)!tb_subforn) = "S" Then
                  If nCaunumregi2 = 0 Then  'non eseguo il test su subfornitora/intracee/rsm
                    If !tb_tipmov.ToString <> "C" Then
                      LogWrite(oApp.Tr(Me, 129971821433368753, "Documento |" & strEstremidoc & "|: si sta creando una nuova registrazione con causale NON di tipo 'Iva per cassa' (|" & nCausale & "|) mentre l'attività iva selezionata è 'Iva per cassa'."), True)
                    End If
                  End If
                Else
                  If !tb_tipmov.ToString = "C" Then
                    LogWrite(oApp.Tr(Me, 129971824096156122, "Documento |" & strEstremidoc & "|: si sta creando una nuova registrazione con causale di tipo 'Iva per cassa' (|" & nCausale & "|) mentre l'attività iva selezionata NON è 'Iva per cassa'."), True)
                  End If
                End If
              End If
              dttTmp1.Clear()
            End If
          End With
        End If
        dttTmp.Clear()

        If strCautregiva <> strMemtregiva Then
          If strTipork = "B" And bContIncDDT Then
            'non serve dare il messaggio, tanto il reg. iva non viene utilizzato
          Else
            LogWrite(oApp.Tr(Me, 128770261562618000, "Causale contabile (rilevata dal tipo bolla/fattura o da personalizzaz. acquisti/vendite) |" & nCausale & "| NON COMPATIBILE con tipo registro IVA" & vbCrLf & _
                            "col documento |" & strEstremidoc & "|."), True)
            GoTo eseguinext
          End If
        End If
        ' se ric. fatt. cee, attribuisce anche numero fattura di vendita ....
        ' prende per default  la serie spazio (non abbiamo il dato preciso da
        ' nessuna altra parte per ora!!!)
        'STESSA COSA PER DOCUMENTI REVERSE CHARGE
        If (strTipork = "K" Or strTipork = "L" Or strTipork = "J" Or strTipork = "(") And strFattXtracee <> "S" And (nCodntra <> 0 Or strFattRevCharge <> "N") Then
          ' cerca se la trova la serie associata al registro vendite indicato in tabella causale
          If strSerieDocEm.Trim <> "" Then
            'serie doc. emessi letta dalla causale di CG
            strAlfdoc = strSerieDocEm
          Else
            oCldPnfa.GetSerieFromTabnuma(strDittaCorrente, strCautregiva2, nCaunumregi2, NTSCInt(dtrTm!tm_anno), strAlfdoc)
          End If
          If strAlfdoc = "" Then strAlfdoc = " "
          strErr = ""

          '--------------------
          'verifico se devo registrare le pseudofatture nel come fatt emesse o fatt / ric fisc emesse
          strTipoNumerazVendPerAcqIntra = "A"
          lNumdoc = oCldPnfa.LegNuma(strDittaCorrente, "A", strAlfdoc, NTSCInt(dtrTm!tm_anno), False)
          If lNumdoc = 0 Then
            'non è stata attivata la numerazione per le FATTURE EMESSE: provo con le 'Fatt/ric.fisc. emesse'
            lNumdoc = oCldPnfa.LegNuma(strDittaCorrente, "S", strAlfdoc, NTSCInt(dtrTm!tm_anno), False)
            If lNumdoc > 0 Then strTipoNumerazVendPerAcqIntra = "S"
          End If
          If lNumdoc = 0 Then
            'non è stata attivata nessuna numerazione: creo quella per le fature emesse (caso standard)
            strTipoNumerazVendPerAcqIntra = "A"
            lNumdoc = oCldPnfa.LegNuma(strDittaCorrente, "A", strAlfdoc, NTSCInt(dtrTm!tm_anno), True)
          End If

          If strErr <> "" Then LogWrite(strErr, True)
          ' per ora da per scontatao che la politica su quel registro iva (ncaunumregi2) che è un registro vendite, sia
          ' sempre 'coincidenti' : migliorare facendogli fare le cose che fa per il registro iva principale
          'lNewnumprot2 = lNumdoc
          'strAlfpro2 = " " ' anche questa è una limitzione...
          ' INTERVENTO MIGLIORATIVO : cerca l'alfa protocollo su tabduri del 2.0 registro
          If Not (bContIncDDT = True And strTipork = "B") Then
            oCldPnfa.GetTabduriTabatti(strDittaCorrente, strCautregiva2, nCaunumregi2, nAnnoDoc, dttTmp)
            If dttTmp.Rows.Count = 0 Then
              'se non c'è il modulo della contabilita generale non do il messaggio, tanto nessuno andrà mai a codificare tabduri...
              If bModuliAcquistati = True Then
                LogWrite(oApp.Tr(Me, 128770272733744000, "Dati generali ultimi registri Iva per il registro |" & _
                                          strCautregiva2 & "| n. |" & nCaunumregi2 & "| relativi all'anno |" & _
                                          nAnnoDoc.ToString & "|, ditta |'" & strDittaCorrente & "'| inesistenti." & vbCrLf & _
                                          "per il documento |" & strEstremidoc & "|."), True)
              End If
              strTipoprot2 = "1"
              strSerfatt2 = " "
              strSernoac2 = " "
              strSernoad2 = " "
              strTiponume2 = "0"
              strProrata = "N"
              dPerProrata = 0
            Else
              'se reg. iva di fattura figurativa non faccio contabilizzare. dovrà essere gestita manualmente in movivafig
              If NTSCStr(dttTmp.Rows(0)!tb_ftfig) = "S" Then
                LogWrite(oApp.Tr(Me, 130580319538711852, "Documento |" & strEstremidoc & "| scartato: il documento è collegato ad un registro IVA 2 di tipo 'fattura figurativa'"), True)
                GoTo eseguinext
              End If
              strTipoprot2 = NTSCStr(dttTmp.Rows(0)!tb_tipoprot)
              strSerfatt2 = NTSCStr(dttTmp.Rows(0)!tb_serfatt)
              strSernoac2 = NTSCStr(dttTmp.Rows(0)!tb_sernoac)
              strSernoad2 = NTSCStr(dttTmp.Rows(0)!tb_sernoad)
              strTiponume2 = NTSCStr(dttTmp.Rows(0)!tb_tiponume)
              strProrata = NTSCStr(dttTmp.Rows(0)!tb_prorata)
              dPerProrata = NTSCDec(dttTmp.Rows(0)!tb_aliqesen)
            End If
            dttTmp.Clear()
          End If    'If Not (bContIncDDT = True And strTipork = "B") Then

          If strTipoprot2 = "1" Then ' n. prot e doc. unici (come nel caso vendite, defult)
            strAlfpro2 = strAlfdoc
            lNewnumprot2 = lNumdoc
          Else ' protocolli svincolati, come nel registro acquisti di default
            ' determina la serie (si può migliorare... col test su strTiponume)

            If bRevChargeNumdocDaPartita And NTSCInt(dtrTm!tm_numpar) <> 0 Then
              'devo prendere serie e num. documento autofattura dalla partita del documento
              'solo se su reg. iva vendite impostato che numerazione svincolata da protocollo vendite e num partita <> 0
              strAlfdoc = NTSCStr(dtrTm!tm_alfpar)
              lNumdoc = NTSCInt(dtrTm!tm_numpar)
            End If

            Select Case strTdociva
              Case "1" : strAlfpro2 = strSerfatt2 ' fatture
              Case "2" : strAlfpro2 = strSernoac2
              Case "3" : strAlfpro2 = strSernoad2
              Case Else : strAlfpro2 = strSerfatt2 ' in mancanza
            End Select
            strErr = ""
            lNewnumprot2 = oCldPnfa.LegNuma(strDittaCorrente, "P", strCautregiva2 & strAlfpro2 & nCaunumregi2.ToString("000"), nAnnoDoc, True)
            'lNewnumprot2 = oCldPnfa.AggNuma(strDittaCorrente, "P", strCautregiva2 & strAlfpro2 & nCaunumregi2.ToString("000"), nAnnoDoc, lTmp, True, True, strErr)
            bAggProtP2 = True
            strAlfdocP2 = strCautregiva2 & strAlfpro2 & nCaunumregi2.ToString("000")
            lNumdocP2 = lNewnumprot2
            If strErr <> "" Then LogWrite(strErr, True)
          End If

          'se è una ricontabilizzazione, devo prendere il protocollo vendite dalla registrazione originaria !!!!!!
          'non devo ricalcolarlo ogni volta, altrimenti si creerebbero dei buchi nelle numerazioni vendite!!!

          If bRielab = True And dtrTm!tm_flcont.ToString = "S" Then
            'se è un documento contabilizzato con NET 2012 il numero doc e num protocollo vendite sono su testmag,
            'altrimenti devo leggerlo da CG, come fino a net 2011
            If NTSCInt(dtrTm!tm_numdoc2) <> 0 And NTSCInt(dtrTm!tm_numpro2) <> 0 Then
              bAggProtP2 = False
              If bRevChargeNumdocDaPartita And NTSCInt(dtrTm!tm_numpar) <> 0 And strTipoprot2 = "0" And _
                (strTipork = "K" Or strTipork = "L" Or strTipork = "J" Or strTipork = "(") And strFattXtracee <> "S" And (nCodntra <> 0 Or strFattRevCharge <> "N") Then
                'autofattura su reverse charge con regime svincolato di numeraz. documento da protocollo: non devo mantenere quello vecchio
              Else
                lNumdoc = NTSCInt(dtrTm!tm_numdoc2)
              End If
              lNewnumprot2 = NTSCInt(dtrTm!tm_numpro2)
            Else
              'se ho cancellato la reg. di CG.... non posso fare altro che ricalcolarlo ...
              If oCldPnfa.RielabGetNumpro2FromPrinot(strDittaCorrente, strDatregftOld, lNumregftOld, strCautregiva2, nCaunumregi2, dttTmp) Then
                If dttTmp.Rows.Count > 0 Then
                  bAggProtP2 = False
                  lNumdoc = NTSCInt(dttTmp.Rows(0)!pn_numdoc)
                  lNewnumprot2 = NTSCInt(dttTmp.Rows(0)!pn_numpro)
                End If
              End If
            End If    'If NTSCInt(dtrTm!tm_numdoc2) <> 0 And NTSCInt(dtrTm!tm_numpro2) <> 0 Then
          End If    'If bRielab = True And dtrTm!tm_flcont.ToString = "S" Then
        Else
          ' - no fattur acquisto intracomunitaria...
          lNewnumprot2 = 0
          strAlfpro2 = " "
        End If
        ' -- ora legge tabduri per leggere le regole di attribuziione del n. protocollo
        ' sul registro iva princiaple (quello di acquisto per le fatture intracomunitarie, nel caso di fatture intracom.)
        If Not (bContIncDDT = True And strTipork = "B") Then
          oCldPnfa.GetTabduriTabatti(strDittaCorrente, strMemtregiva, nMemnregiva, nAnnoDoc, dttTmp)
          If dttTmp.Rows.Count = 0 Then
            If bModuliAcquistati = True And strMemtregiva <> " " Then
              LogWrite(oApp.Tr(Me, 128770279887102000, "Dati generali ultimi registri Iva per il registro |" & _
                            strMemtregiva & "| n. |" & nMemnregiva & "| relativi all'anno |" & _
                            nAnnoDoc.ToString & "|, ditta |'" & strDittaCorrente & "'| inesistenti." & vbCrLf & _
                            "per il documento |" & strEstremidoc & "|."), True)
            End If
            If strTipork = "K" Or strTipork = "J" Or strTipork = "L" Or strTipork = "(" Then
              strTipoprot = "0"
            Else
              strTipoprot = "1"
            End If
            strSerfatt = " "
            strSernoac = " "
            strSernoad = " "
            strTiponume = "0"
            strProrata = "N"
            dPerProrata = 0
          Else
            'se reg. iva di fattura figurativa non faccio contabilizzare. dovrà essere gestita manualmente in movivafig
            If NTSCStr(dttTmp.Rows(0)!tb_ftfig) = "S" Then
              If strTipork = "A" Then
                '----------------------------------------------------------------------------------------------------
                '--- Se la Fattura Immediata risulta già contabilizzata controllo, prima, se le registrazione in
                '--- MOVIVAFIG è cancellabile (NON ancora stampata in 'Definitiva')
                '----------------------------------------------------------------------------------------------------
                If oCldPnfa.CreaFatturaFigurativa(strDittaCorrente, strDatreg, strMemtregiva, nMemnregiva, _
                  lContoiva, nCausale, lTmp, dtrTm, bFatturaFigurativaCancellabile) = False Then
                  If bFatturaFigurativaCancellabile = False Then
                    LogWrite(oApp.Tr(Me, 130606926967552178, "Documento |" & strEstremidoc & "| scartato: il documento è collegato ad una Fattura Figurativa già stampata in 'Definitivo'."), True)
                    GoTo eseguinext
                  End If
                Else
                  lRkfatt += 1
                  GoTo eseguinext
                End If
                '----------------------------------------------------------------------------------------------------
              End If
              LogWrite(oApp.Tr(Me, 130580319538711851, "Documento |" & strEstremidoc & "| scartato: il documento è collegato ad un registro IVA di tipo 'fattura figurativa'"), True)
              GoTo eseguinext
            End If
            strTipoprot = NTSCStr(dttTmp.Rows(0)!tb_tipoprot)
            strSerfatt = NTSCStr(dttTmp.Rows(0)!tb_serfatt)
            strSernoac = NTSCStr(dttTmp.Rows(0)!tb_sernoac)
            strSernoad = NTSCStr(dttTmp.Rows(0)!tb_sernoad)
            strTiponume = NTSCStr(dttTmp.Rows(0)!tb_tiponume)
            strProrata = NTSCStr(dttTmp.Rows(0)!tb_prorata)
            dPerProrata = NTSCDec(dttTmp.Rows(0)!tb_aliqesen)
          End If
          dttTmp.Clear()
        Else
          strTipoprot = "1"
          strSerfatt = " "
          strSernoac = " "
          strSernoad = " "
          strTiponume = "0"
        End If    'If Not (bContIncDDT = True And strTipork = "B") Then

        'atribuisce numero protocollo se necessario.. fatture ricevute
        If strTipork = "K" Or strTipork = "J" Or strTipork = "L" Or strTipork = "(" Then
          If strOpz19 = "S" Then
            lNewnumprot = NTSCInt(dtrTm!tm_numprot)
            strAlfpro = NTSCStr(dtrTm!tm_alfpro)
            If strAlfpro = "" Then strAlfpro = " "
            GoTo calcolaiva1
          End If
          'se documento extracee salto comunque l'assegnazione del numero di protocollo acquisti, visto che la registraz. non movimenta l'iva
          If strFattXtracee = "S" Then
            nMemnregiva = 0
            lNewnumprot = 0
            strAlfpro = " "
            GoTo calcolaiva1
          End If
        End If
        ' altri casi per attribuire n. di protocollo e alfa protocllo (in base alle regole)
        If strTipoprot = "1" Then ' n. prot e doc. unici (come nel caso vendite, defult)
          strAlfpro = strAlfdoc : lNewnumprot = lNumdoc
        Else ' protocolli svincolati, come nel registro acquisti di default
          ' determina la serie (si può migliorare... col test su strTiponume)
          If bAlfproRicFromTabnuma And (strTipork = "K" Or strTipork = "J" Or strTipork = "L" Or strTipork = "(") Then
            'prendo la serie protocollo da tabnuma: solo doc ricevuti
            strAlfpro = oCldPnfa.GetAlfproFromTabnuma(strDittaCorrente, "L", NTSCStr(dtrTm!tm_serie), nAnnoDoc)
            If strAlfpro = "" Then
              Select Case strTdociva
                Case "1" : strAlfpro = strSerfatt ' fatture
                Case "2" : strAlfpro = strSernoac
                Case "3" : strAlfpro = strSernoad
                Case Else : strAlfpro = strSerfatt ' in mancanza
              End Select
            End If
          Else
            'caso standard
            Select Case strTdociva
              Case "1" : strAlfpro = strSerfatt ' fatture
              Case "2" : strAlfpro = strSernoac
              Case "3" : strAlfpro = strSernoad
              Case Else : strAlfpro = strSerfatt ' in mancanza
            End Select
          End If

          strErr = ""
          lNewnumprot = oCldPnfa.LegNuma(strDittaCorrente, "P", strMemtregiva & strAlfpro & nMemnregiva.ToString("000"), NTSCDate(strDatreg).Year, True)  'se sto contabilizzando nel 2013 dei documenti del 2013, il protocollo deve essere quello del 2013 (ovvero quello nell'anno della data di contabilizzazione)
          'lNewnumprot = oCldPnfa.AggNuma(strDittaCorrente, "P", strMemtregiva & strAlfpro & nMemnregiva.ToString("000"), nAnnoDoc, lTmp, True, True, strErr)
          bAggProtP = True
          strAlfdocP = strCautregiva & strAlfpro & nMemnregiva.ToString("000")
          lNumdocP = lNewnumprot
        End If

calcolaiva1:
        '--------------------
        ' calcola dTotiva e dTotivav (per acq. cee) e r.f. parz. incass.
        With dtrTm
          dTotiva = ArrDbl(NTSCDec(!tm_imposta_1) + NTSCDec(!tm_imposta_2) + NTSCDec(!tm_imposta_3) + NTSCDec(!tm_imposta_4) + NTSCDec(!tm_imposta_5) + NTSCDec(!tm_imposta_6) + NTSCDec(!tm_imposta_7) + NTSCDec(!tm_imposta_8), oCldPnfa.TrovaNdec(0))
          dTotivav = ArrDbl(NTSCDec(!tm_impostav_1) + NTSCDec(!tm_impostav_2) + NTSCDec(!tm_impostav_3) + NTSCDec(!tm_impostav_4) + NTSCDec(!tm_impostav_5) + NTSCDec(!tm_impostav_6) + NTSCDec(!tm_impostav_7) + NTSCDec(!tm_impostav_8), oCldPnfa.TrovaNdec(NTSCInt(!tm_valuta)))
          If dsTabciva.Tables("TABCIVA").Columns.Contains("tb_revcharge") And strFattRevCharge = "M" And nCodntra = 0 Then
            For i = 1 To 8
              'da net 2013 calcolo quanto è l'importo del'iva per reverse charge da togliere dal tot documento sulla prima riga
              'serve solo su documenti reverse charge MISTI (ovvero con parte non rever se charge)
              If NTSCInt(dtrTm("tm_codiva_" & i.ToString)) <> 0 Then
                GetTabcivaRow(NTSCInt(dtrTm("tm_codiva_" & i.ToString)), dttTmp)
                If NTSCStr(dttTmp.Rows(0)!tb_revcharge) <> "N" Then
                  dTotivaRC += NTSCDec(dtrTm("tm_imposta_" & i.ToString))
                  dTotivavRC += NTSCDec(dtrTm("tm_impostav_" & i.ToString))
                End If
              End If
            Next
          End If
        End With

        ' se l'IVA inded. deve essere ripartita, verifico i cod. iva per sapere se ci sarà il caso e memorizzo l'importo da
        ' distribuire tra le varie contropartite
        'memorizzo in 'dIvaInded' il totale dell'IVA indeducibile
        If bGirocontoIvaIndedRipartito Then
          If Not CalcolaIvaInded(dtrTm, dIvaInded, dIvaIndedVal) Then GoTo eseguinext
        Else
          dIvaInded = 0 : dIvaIndedVal = 0
        End If

        'sulle fatture extracee non si contabilizza l'IVA!!!!
        If strFattXtracee = "S" Then
          dTotiva = 0
          dTotivav = 0
          dIvaInded = 0
          dIvaIndedVal = 0
          'no righe iva, ovvero non devo aggiornare il protocollo acquisti 1 e 2
          lNewnumprot = 0
          lNewnumprot2 = 0
          bAggProtP = False
          bAggProtP2 = False
        End If

calcolacontropartite:
        '--------------------
        'determino il castelletto contropartite

        'se la CA2 è attiva non posso utilizzare lo 'sblocca castelletti', diversamente i dati verrebbero presi sempre ta testmagc invece che da movmag
        If bCa2 Then
          If NTSCStr(dtrTm!tm_flscdb) = "S" Then
            LogWrite(oApp.Tr(Me, 129278911116142578, "Operazione interrotta in fase di determinazione castelletto " & _
                          "contropartite per il documento |" & strEstremidoc & "|: con la 'Contabilità analitica duplice contabile' attiva" & _
                          " la spunta 'Sblocca castelletti' deve essere deselezionata. " & _
                          "Eventuali correzioni/rettifiche dovranno essere eseguite nel corpo del documento"), True)
            GoTo eseguinext
          End If
        End If

        bOk = Calcolacontropartite(dtrTm, dttTmOrig)
        If Not bOk Then
          LogWrite(oApp.Tr(Me, 128770322209202000, "Operazione interrotta in fase di determinazione castelletto " & _
                        "contropartite per il documento |" & strEstremidoc & "|."), True)
          GoTo eseguinext
        End If

        '------------------------------
        'se c'è iva indeducibile e opzione di ripartizione su tutti i costi, la ripartisco ora sui conti dell'array
        If dIvaInded <> 0 Then
          i = 1
          e = 0
          j = 0
          dImporto = 0
          dImpVal = 0
          dImponib = 0
          dImponibval = 0
          While NTSCInt(dttControp.Rows(i)!lControp) <> 0
            If NTSCStr(dttControp.Rows(i)!strIvaInded) = "S" Then
              dImporto = dImporto + NTSCDec(dttControp.Rows(i)!dImporto)
              dImpVal = dImpVal + NTSCDec(dttControp.Rows(i)!dImpVal)
              j = i 'ultima riga che può accettare l'iva inded
              e += 1
            End If
            i += 1
          End While

          If e = 1 Then
            'c'è una sola contropartita: gli assegno tutta l'iva indeducibile
            dttControp.Rows(j)!dIvaInded = dIvaInded
            dttControp.Rows(j)!dIvaIndedVal = dIvaIndedVal
          Else
            For e = 1 To i
              If e = j Then Exit For 'l'ultima la calcolo per differenza
              If NTSCStr(dttControp.Rows(e)!strIvaInded) = "S" Then
                dttControp.Rows(e)!dIvaInded = ArrDbl(dIvaInded * NTSCDec(dttControp.Rows(e)!dImporto) / dImporto, oCldPnfa.TrovaNdec(0))
                If dIvaIndedVal <> 0 Then
                  dttControp.Rows(e)!dIvaIndedVal = ArrDbl(dIvaIndedVal * NTSCDec(dttControp.Rows(e)!dImpVal) / dImpVal, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
                End If
                dImponib = dImponib + NTSCDec(dttControp.Rows(e)!dIvaInded)
                dImponibval = dImponibval + NTSCDec(dttControp.Rows(e)!dIvaIndedVal)
              End If
            Next
            ' l'ultimo lo calcolo per differenza
            dttControp.Rows(j)!dIvaInded = ArrDbl(dIvaInded - dImponib, oCldPnfa.TrovaNdec(0))
            dttControp.Rows(j)!dIvaIndedVal = ArrDbl(dIvaIndedVal - dImponibval, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
          End If
          dttControp.AcceptChanges()
        End If

        'se DDT emesso e spuntato 'contabilizza solo incassi salto direttamente alla reg dell'incasso.
        If strTipork = "B" And bContIncDDT Then
          lContoPrimaRiga = NTSCInt(dtrTm!tm_conto)
          GoTo EseguiIncassoAnticipatoDDtEmessi
        End If


        If NTSCStr(strDatdoc).Trim <> "" AndAlso NTSCDate(strDatreg) < NTSCDate(strDatdoc) Then
          LogWrite(oApp.Tr(Me, 130220045080324629, "ATTENZIONE: Nel documento |" & strEstremidoc & _
                                  "| la data di registrazione è antecedente alla data del documento"), True)
        End If

iniziaregistrazione:
        'FATT EMESSA O RICEVUTA: IL CLIENTE O IL FORNITORE
        'RICEVUTA FISCALE EMESSA A PRIVATO X CESS BENI (O PREST SERV) INCASSATA IN PARTE O TOTALE: CASSA
        'RIC FISC EM A PRIVATO CESS BENI NON INCASSATA: (TABPEVE - CRED DA RIC FISC NON INC CESS BENI)
        'RIC FISC EM A PRIVATO PREST SERV NON INCASSATA: (TABPEVE - CRED DA RIC FISC NON INC PREST SERV)
        'RIC FISC EM NO PRIVATO CESS BENI (O PREST SERV) INCASSATA IN PARTE O TOTALE: IL CLIENTE
        'RIC FISC EM NO PRIVATO CESS BENI NON INCASSATA: (TABPEVE - CRED DA RIC FISC NON INC CESS BENI)
        'RIC FISC EM NO PRIVATO PREST SERV NON INCASSATA: (TABPEVE - CRED DA RIC FISC NON INC PREST SERV)
        'RIEM RIC FISC A PRIVATO CESS BENI: IL CLIENTE PRIVATO PER STORNO TABPEVE - CRED DA RIC FISC NON INC CESS BENI
        'RIEM RIC FISC A PRIVATO PREST SERV: IL CLIENTE PRIVATO PER STORNO TABPEVE - CRED DA RIC FISC NON INC PREST SERV
        'RIEM RIC FISC A NON PRIVATO CESS BENI: IL CLIENTE PER STORNO TABPEVE - CRED DA RIC FISC NON INC CESS BENI
        'RIEM RIC FISC A NON PRIVATO PREST SERV: IL CLIENTE PER STORNO TABPEVE - CRED DA RIC FISC NON INC PREST SERV
        'new 2012:
        'CORRISPETTIVO EMESSO TOTALMENTE INCASSATO RAGGRUPPATO: CONTO CLIENTE PRIVATO DI TABPEVE SE NO RETAIL, ELSE CONTO CLIENTE PRIVATO INDICATO IN TABSTAB
        'RICEVUTA FISCALE EMESSA (a privato o no) sia cess beni che servizi TOTALMENTE INCASSATO RAGGRUPPATO: CONTO CLIENTE PRIVATO DI TABPEVE SE NO RETAIL, ELSE CONTO CLIENTE PRIVATO INDICATO IN TABSTAB
        'PARCELLAZIONE: FATTURA E/O NOTA ACCRED: IL CLIENTE
1000:   If Not ScriviRiga1000(dtrTm, dttTmOrig) Then GoTo eseguinext

        'IVA split payment (nuovo sistema sostitutivo dell'iva differita): solo per documenti emessi. l'iva la verserà l'ente pubblico, non più noi, 
        'per cui occorre fare 2 righe per stornare dal cliente l'importo dell'IVA e assegnarload un conto 'IVA Split payment'
        If oCldPnfa.CheckCausaleSplitPaymentFromTpbf(strDittaCorrente, NTSCInt(dtrTm!tm_tipobf)) Then
          'storno il cliente per iva split payment
1100:     If Not ScriviRiga1100(dtrTm) Then GoTo eseguinext
          'carico il nuovo conto 'iva split payment
1110:     If Not ScriviRiga1110(dtrTm) Then GoTo eseguinext
        End If

        If strTipork = "F" And bParzinc And strPrestserv <> "S" And bModuliAcquistati Then
          ' adesso solo per ric. fiscali, i casi strani
          ' CASO 1: r.f. cessioni, parz. incassate .... : aggiunge riga cassa per la parte incassata
          'RIC FISC EM CESS BENI A CLIENTE PRIVATO E NON, 'NON TOTALMENTE INCASSATA': IMPORTO RESIDUO (CRED RIC FISC NON INC CESS BENI - TABPEVE)
2000:     If Not ScriviRiga2000(dtrTm) Then GoTo eseguinext
        End If

        If strTipork = "F" And bParzinc And strPrestserv = "S" And bModuliAcquistati Then
          ' caso 2 : ric. fisc. prestazione di serv. parz. incassata
          'RIC FISC EM PREST SERV A PRIVATO E NON PARZ INCASSATA: TABPEVE - CRED DA RIC FISC NON INC PREST SERV
3000:     If Not ScriviRiga3000(dtrTm) Then GoTo eseguinext
          ' segue con iva da ric. fisc non inc.
          'RIC FISC EM PREST SERV A PRIVATO E NON PARZ INCASSATA: TABPEVE - IVA DA RIC FISC NON INCASS PREST SERV
4000:     If Not ScriviRiga4000(dtrTm) Then GoTo eseguinext
        End If

        'fine casi strani r.f. se riemissione non registra costi e ricavi, nè diff. euro (per ora) ...
        If strTipork = "I" And strPrestserv <> "S" Then GoTo saltacostiericavi
        If strTipork = "I" And strPrestserv = "S" Then GoTo saltacostiericavi

        '-------------------------
        'Contropartite
        For i = 1 To nMaxControp
          If NTSCInt(dttControp.Rows(i)!lControp) <> 0 Then
            'FATT EM O RICEVUTA: RICAVO (COSTO)
            'RIC FISC TUTTI I CASI: RICAVO
            'PARCELLAZIONE: FATTURA E/O NOTA ACCRED: RICAVO
5000:       If Not ScriviRiga5000(dtrTm, dttControp.Rows(i), dttTmOrig) Then GoTo eseguinext
          Else
            Exit For
          End If
        Next

        If NTSCDec(dtrTm!tm_diffda) <> 0 Then
          'DIFFERENZE DI CONVERSIONE  VALUTA / EURO: tutti i casi
6000:     If Not ScriviRiga6000(dtrTm) Then GoTo eseguinext
        End If

        If strTipork = "I" And strPrestserv <> "S" Then GoTo saltacostiericavi
        If strTipork = "I" And strPrestserv = "S" Then GoTo saltacostiericavi

        If strPrgParent = "BNPAPNPA" Then
          If NTSCDec(dtrTm!tm_ritenut) <> 0 Then
            'PARCELLAZIONE: fattura e/o nota accred: ritenuta (tm_spegen)
6200:       If Not ScriviRiga6200(dtrTm) Then GoTo eseguinext
            'PARCELLAZIONE: fattura e/o nota accred: cliente per storno ritenuta
            If Not ScriviRiga6250(dtrTm) Then GoTo eseguinext
          End If

          If NTSCDec(dtrTm!tm_altriprev) <> 0 Then
            'PARCELLAZIONE: fattura e/o nota accred: enasarco (tm_altriprev)
6300:       If Not ScriviRiga6300(dtrTm) Then GoTo eseguinext
            'PARCELLAZIONE: fattura e/o nota accred: cliente per storno enasarco
            If Not ScriviRiga6350(dtrTm) Then GoTo eseguinext
          End If

          If NTSCDec(dtrTm!tm_spegen) <> 0 Then
            'PARCELLAZIONE: fattura e/o nota accred: le spese generali (tm_spegen)
6500:       If Not ScriviRiga6500(dtrTm) Then GoTo eseguinext
          End If

          If NTSCDec(dtrTm!tm_nonsoggiva) <> 0 Then
            'PARCELLAZIONE: fattura e/o nota accred: cassa commercialisti (tm_nonsoggiva)
6600:       If Not ScriviRiga6600(dtrTm) Then GoTo eseguinext
          End If
        End If    'If strPrgParent = "BNPAPNPA" Then

        If NTSCDec(dtrTm!tm_bolli) <> 0 Then
          'FT EMESSA O RICEVUTA: BOLLI
          'RIC FISCALI TUTTI I CASI: BOLLI
          'PARCELLAZIONE: fattura e/o nota accred: spese incasso
7000:     If Not ScriviRiga7000(dtrTm) Then GoTo eseguinext
        End If

        If NTSCDec(dtrTm!tm_speinc) <> 0 Then
          'FT EM O RIC: SPESE INCASSO
          'RIC FISC TUTTI I CASI: SPESE INCASSO
          'PARCELLAZIONE: fattura e/o nota accred: spese incasso
8000:     If Not ScriviRiga8000(dtrTm) Then GoTo eseguinext
        End If

        If NTSCDec(dtrTm!tm_speacc) <> 0 Then
          'FT EMESSA O RICEVUTA: SPESE TRASP (SPESE ACCESSORIE)
          'RIC FISC TUTTI I CASI: SPESE TRASP
9000:     If Not ScriviRiga9000(dtrTm) Then GoTo eseguinext
        End If

        If NTSCDec(dtrTm!tm_speimb) <> 0 Then
          'FT EMESSA O RICEVUTA: SPESE IMBALLO
          'RIC FISC TUTTI I CASI: SPESE IMBALLO
10000:    If Not ScriviRiga10000(dtrTm) Then GoTo eseguinext
        End If

saltacostiericavi:
        If dtrTm!tm_tipork.ToString = "F" And bNotinc And strPrestserv = "S" Then
          'RIC FISC PREST SERV A PRIVATO E NON 'NON INCASSATE': TABPEVE - IVA DA RIC FISC NON INCASS PREST SERV
11000:    If Not ScriviRiga11000(dtrTm) Then GoTo eseguinext
        End If

        If dtrTm!tm_tipork.ToString = "F" And bNotinc And strPrestserv = "S" Then GoTo saltacastiva

        'se riemissione e cessione di beni , chiude il credito e no castelletto iva
        If dtrTm!tm_tipork.ToString = "I" And strPrestserv <> "S" Then
          'RIMEM RIC FISC A PRIVATO E NON CESS BENI:  STORNO TABPEVE - CRED DA RIC FISC NON INC CESS BENI
12000:    If Not ScriviRiga12000(dtrTm) Then GoTo eseguinext
        End If

        If dtrTm!tm_tipork.ToString = "I" And strPrestserv <> "S" Then GoTo saltacastiva

        'ecc. riemissione r.f. servizi  (storna in iva su rf non inc )
        'e storna credito r.f. non incassate per il totale
        If dtrTm!tm_tipork.ToString = "I" And strPrestserv = "S" Then
          'RIMEM RIC FISC A PRIVATO PREST SERV:  STORNO TABPEVE - IVA DA RIC FISC NON INC PREST SERV
13000:    If Not ScriviRiga13000(dtrTm) Then GoTo eseguinext

          'RIMEM RIC FISC A PRIVATO E NON PREST SERV:  STORNO TABPEVE - CRED DA RIC FISC NON INC PREST SERV
14000:    If Not ScriviRiga14000(dtrTm) Then GoTo eseguinext
        End If

        'se c'è iva indeducibile (ripartita su tutte le righe di costo)
        'aggiungo una riga cumulativa per l'importo dell'indeducibilità
        If dIvaInded <> 0 Then
          'RIC FATT CON IVA INDED E opz di reg bGirocontoIvaIndedRipartito = -1: CONTO IVA PER RIGA DELL'IVA INDED
15000:    If Not ScriviRiga15000(dtrTm) Then GoTo eseguinext
        End If

        'controllo se la fattura è in valuta ed il tipo bolla fattura chiude reg. di acconto:
        'se lo è verifico una eventuale differenza di cambio
        If bModuliAcquistati = True Then
          If NTSCInt(dtrTm!tm_valuta) <> 0 And dtrTm!tb_flacconto.ToString = "E" Then
            If Not CalcolaDiffCambio(lRiga, strDatreg, lNumreg, nCausale, lDiffAttCambi, lDiffPasCambi, _
                                     dtrTm, dttPN.Rows(dttPN.Rows.Count - 1)) Then GoTo eseguinext
          End If
        End If

stornonoteaccred:
        '-------------------------
        'solo su note accred EMESSE, se necessario storno direttamente la fattura EMESSA
        'in tm_annpar, tm_alfpar e tm_numpar della nota accred è indicata la partita della fattura
        'la nota accred deve avere un cod pagamento di una sola rata di tipo 'rim. diretta'
        'e la fattura deve avere scadenze aperte (anche se insolute) di importo maggiore o uguale a quello della nota di accred.
        If strTipork = "N" And bContCompensNoteAcc And NTSCInt(dtrTm!tm_numpar) <> 0 And NTSCInt(dtrTm!tm_annpar) <> 0 Then
          'cerco la fattura 
          If oCldPnfa.GetScadenFattura(strDittaCorrente, lContoPrimaRiga, NTSCInt(dtrTm!tm_annpar), _
                                NTSCStr(dtrTm!tm_alfpar), NTSCInt(dtrTm!tm_numpar), True, _
                                IIf(bInt, "S", "N").ToString, NTSCInt(dtrTm!tm_valuta), dttScad, _
                                NTSCDate(dtrTm!tm_datregef).ToShortDateString, NTSCInt(dtrTm!tm_numregef), 0) Then
            If dttScad.Rows.Count > 0 Then
              'la fattura esiste: verifico se posso compensarla
              'se la scadenza è di una sola rata e la nota accred non deve avere pagato e/o abbuono e/o omaggi ...
              'se le scadenze aperte hanno importo superiore alla nota accred
              For i = 0 To dttScad.Rows.Count - 1
                dTmp += NTSCDec(dttScad.Rows(i)!sc_importo)
              Next
              If NTSCDec(dtrTm!tm_impsca_1) <> 0 And NTSCDec(dtrTm!tm_impsca_2) = 0 And _
                 NTSCDec(dtrTm!tm_pagato) = 0 And NTSCDec(dtrTm!tm_abbuono) = 0 And _
                 NTSCDec(dtrTm!tm_totomag) = 0 And NTSCInt(dtrTm!tm_valuta) = 0 And _
                 NTSCDec(dtrTm!tm_totdoc) <> 0 And dTmp >= NTSCDec(dtrTm!tm_totdoc) And _
                 NTSCDec(dtrTm!tm_pagato2) = 0 Then

                'storno della nota accred in fase di registrazione
15100:          If Not ScriviRiga15100(dtrTm) Then GoTo eseguinext
                'storno della fattura collegata alla nota accred (e aggiorno le relative scadenze...)
15300:          If Not ScriviRiga15300(dtrTm, dttScad) Then GoTo eseguinext
              Else
                'avviso perchè non posso compensare fattura con nota accred
                LogWrite(oApp.Tr(Me, 129203696378410390, "ATTENZIONE: Il documento |" & strEstremidoc & _
                                  "| dovrebbe compensare una fattura emessa (in base a quanto indicato negli estremi partita della nota di accredito). " & _
                                  "Non verrà fatto perchè non sussistono i requisiti:" & vbCrLf & _
                                  "Nota di accredito con scadenza di importo superiore alle scadenze non ancora saldate della fattura" & vbCrLf & _
                                  "Nota di accredito avente scadenza con numero di rate superiore ad 1" & vbCrLf & _
                                  "Nota di accredito con importo pagato e/o abbuono e/o omaggi divesi da 0" & vbCrLf & _
                                  "Nota di accredito con valuta diversa da 0 e/o totale documento uguale a 0" & vbCrLf & _
                                  ""), True)
              End If
            Else
              'avviso che la fattura non è stata trovata in CG
              LogWrite(oApp.Tr(Me, 130633900045339824, "Nota di accredito a storno fattura:" & _
                 " Dalla partita indicata sulla nota di accredito non è stato possibile ottenere la registrazione contabile della fattura, oppure il cliente non gestisce le scadenze, oppure le scadenze risultano essere tutte saldate." & vbCrLf & _
                 "Vedi documento |" & strEstremidoc & "|."), True)
            End If    'If dttScad.Rows.Count > 0 Then
          End If    'If oCldPnfa.GetScadenFattura(strDittaCorrente, lContoPrim
        End If    'If strTipork = "N" And bContCompensNoteAcc And NTSCInt(dtrTm!tm_ ....

        '-------------------------
        'le max 8 cod iva...(stessa dat reg, n.o reg, causale)
righeiva:
        If strFattXtracee = "S" Then GoTo saltacastiva 'una fattura/nota accred extracee non ha IVA, ma solo costi e fornitore

        If bCeas Then nNumaliq = 4 Else nNumaliq = 8
        If NTSCInt(dtrTm!tm_codiva_1) = 0 Then
          'la fattura deve avere OBBLIGATORIAMENTE un codcie IVA, magari con imponibile + imposta = 0!
          LogWrite(oApp.Tr(Me, 128771944790498000, "Operazione interrotta. Il documento |" & strEstremidoc & "| è privo di almeno un codice IVA."), True)
          GoTo eseguinext
        End If
        For i = 1 To nNumaliq
          If NTSCInt(dtrTm("tm_codiva_" & i.ToString)) <> 0 Then
            'FT EMESSA O RICEVUTA: IVA
            'RIC FISC TUTTI I CASI: IVA
            'RIM RIC FISC A PRIVATO E NON PREST SERV: IVA
            'PARCELLAZIONE: FATTURE E/O NOTE ACCRED: IVA
16000:      If Not ScriviRiga16000(dtrTm, i) Then GoTo eseguinext

            'se codice con % di iva indenducibile su acquisti (e iva indeducibile tutta su prima contropartita)
            'inserisco le due righe ...
            GetTabcivaRow(NTSCInt(dtrTm("tm_codiva_" & i.ToString)), dttTmp)
            If Not bGirocontoIvaIndedRipartito And CalcolaIvaIndetr(strCautregiva, NTSCDec(dttTmp.Rows(0)!tb_inded)) <> 0 And strCautregiva = "A" And bModuliAcquistati = True Then

              'FT RICEVUTA CON IVA TOTALM O PARZIALM DEDUC e opz di reg bGirocontoIvaIndedRipartito = 0: PRIMA RIGA DI COSTO SU CUI ASSEGNARE IVA
17000:        If Not ScriviRiga17000(dtrTm, i, False) Then GoTo eseguinext

              'FT RICEVUTA CON IVA TOTALM O PARZIALM INDED e opz di reg bGirocontoIvaIndedRipartito = 0: IVA NON DEDUCIBILE (STORNO DELL'IVA)
18000:        If Not ScriviRiga18000(dtrTm, i, False) Then GoTo eseguinext
            End If

            'adesso fa riga successiva (uguale e contraria ) se acquisti cee
            'STESSA COSA PER DOCUMENTI REVERSE CHARGE
            If (dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "(") And (nCodntra <> 0 Or strFattRevCharge <> "N") Then
              'RIC FATT INTRA: RIGA IVA SU REGISTRO 2
              bOk = True
              If strFattRevCharge = "M" And nCodntra = 0 Then
                'per ogni cod. iva devo vedere se devo applicare il reverse charge o no
                'solo su doc. rever charge misti
                If dttTmp.Columns.Contains("tb_revcharge") Then
                  If NTSCStr(dttTmp.Rows(0)!tb_revcharge) = "N" Then
                    bOk = False
                  End If
                End If
              End If
              If bOk = False Then
                'non devo scrivere il cod. iva anche nelle vendite solo se doc reverse charge misto e codiva non reverse charge
              Else
19000:          If Not ScriviRiga19000(dtrTm, i) Then GoTo eseguinext

                'NO! in acquisto l'eventuale iva inded NON DEVE ESSERE GIRATA DA REG. VENDITE A COSTO, 
                '    altrimenti il costo viene aperto in dare dall'iva inded sel reg. acquisti e stornato dall'iva inded dal reg. vendite
                '                If NTSCDec(dttTmp.Rows(0)!tb_inded) <> 0 Then
                '                  'RIC FATT INTRA CON REG 2 ACQ E IVA PARZ DEDUC e opz di reg bGirocontoIvaIndedRipartito = 0: PRIMA RIGA DI COSTO PER IVA INDED
                '20000:            If Not ScriviRiga17000(dtrTm, i, True) Then GoTo eseguinext

                '                  'RIC FATT INTRA CON REG 2 ACQ E IVA PARZ DEDUC e opz di reg bGirocontoIvaIndedRipartito = 0: IVA INDED
                '21000:            If Not ScriviRiga18000(dtrTm, i, True) Then GoTo eseguinext
                '                End If
              End If    'If bOk Then

            End If
          End If    'If NTSCInt(dtrTm("tm_codiva_" & i.ToString)) <> 0 Then
        Next    'For i = 1 To nNumaliq

saltacastiva:
        '----------------------------
        'aggiorna record di testmag:
        dtrTm!tm_flcont = "S"
        dtrTm!tm_datregef = NTSCDate(strDatreg)
        dtrTm!tm_numregef = lNumreg
        'svuoto, visto che se ricontabilizzo senza flag 'contabilizza incassi/pagamenti associati cancello la reg in CG ma non gli estremi della stessa sus testmag
        dtrTm!tm_datregin = DBNull.Value
        dtrTm!tm_numrgin = 0
        dtrTm!tm_datregom = DBNull.Value
        dtrTm!tm_numregom = 0

        dtrTm!tm_numprot = lNewnumprot
        If strPrgParent <> "BNPAPNPA" Then dtrTm!tm_alfpro = IIf(strAlfpro = "", " ", strAlfpro)
        dtrTm!tm_nregiva = nMemnregiva
        If (dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "J") And NTSCInt(dtrTm!tm_codntra) <> 0 Then
          dtrTm!tm_numdoc2 = lNumdoc
          dtrTm!tm_numpro2 = lNewnumprot2
        Else
          dtrTm!tm_numdoc2 = 0
          dtrTm!tm_numpro2 = 0
        End If
        dtrTm!tm_ultagg = DateTime.Now

        If dTdare <> dTavere Then
          LogWrite(oApp.Tr(Me, 128771978151036000, _
                  "Attenzione! Manca quadratura dare/avere nella registrazione del |" & strDatreg & "| n. |" & _
                  lNumreg & "| generata dal documento |" & strEstremidoc & "|. Ugualmente registrata."), True)
        End If

EseguiOmaggi:
        ' registra storno per omaggi (eventuale)
        If NTSCDec(dtrTm!tm_totomag) = 0 Then GoTo EseguiIncasso

        If NTSCStr(dtrTm!xx_raggr) = "S" Then
          'se registrazione raggruppata di corrispettivo o ricevuta fiscale emessa (totalmente incassati)
          'registro l'incasso nella stessa registrazione del documento IVA
          lRiga += 1
          strDAContoom = "A"
          strDAOmagom = "D"
        Else
          lNumreg = oCldPnfa.LegAggRegcDitt(strDittaCorrente, strDatreg)
          lNumregOmaggi = lNumreg
          dTdare = 0
          dTavere = 0
          lRiga = 1 ' n. riga

          'registra riga di prinot (cliente/fornitore in dare/avere)
          Select Case strTipork
            Case "A", "F", "I", "P", "S" : nCausale = nCaustom : strDAContoom = "A" : strDAOmagom = "D"
            Case "N", "£" : nCausale = nCaustom : strDAContoom = "D" : strDAOmagom = "A"
            Case "D" : nCausale = nCaustom : strDAContoom = "A" : strDAOmagom = "D"
            Case "E" : nCausale = nCaustom : strDAContoom = "A" : strDAOmagom = "D"
            Case "C" : nCausale = nCaustom : strDAContoom = "A" : strDAOmagom = "D"
            Case "L" : nCausale = nCaustomac : strDAContoom = "D" : strDAOmagom = "A"
            Case "K" : nCausale = nCaustomac : strDAContoom = "D" : strDAOmagom = "A"
            Case "J", "(" : nCausale = nCaustomac : strDAContoom = "A" : strDAOmagom = "D"
            Case "B" : nCausale = nCaustom : strDAContoom = "A" : strDAOmagom = "D"
          End Select

          ' legge la causale per vedere se e' coerente con il registro iva/tipo docuneto.
          oCldPnfa.ValCodiceDb(nCausale.ToString, strDittaCorrente, "TABCAUC", "N", "", dttTmp)
          If dttTmp.Rows.Count = 0 Then
            LogWrite(oApp.Tr(Me, 128774657968088000, _
                    "Causale contabile (|" & nCausale & "|) per la registrazione degli omaggi nel documento |" & strEstremidoc & "| inesistente. Verificarla in 'Personalizzazione vendite' e in 'Personalizzazione acquisti')"), True)
            GoTo eseguinext
          End If

          strCautregiva = dttTmp.Rows(0)!tb_tipreg.ToString
          lContoiva = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, NTSCInt(dttTmp.Rows(0)!tb_ccontriva), NTSCInt(dttTmp.Rows(0)!tb_contiva))
          strGestdtciva = dttTmp.Rows(0)!tb_gestdtciva.ToString
          If strCautregiva <> " " Then
            LogWrite(oApp.Tr(Me, 128771983169930000, _
                    "Per il documento |" & strEstremidoc & "| la causale '|" & nCausale & "|' per storno omaggi non puo' essere una causale di tipo Iva."), True)
            GoTo eseguinext
          End If
        End If    'If NTSCStr(dtrTm!xx_raggr) = "S" Then

        'FT EM: CLIENTE PER OMAGGI
        'RIC FISC A CLI NO PRIV CESS BENI (O PREST SERV): CLIENTE PER OMAGGI
22000:  If Not ScriviRiga22000(dtrTm) Then GoTo eseguinext

        'FT EM: OMAGGI
        'RIC FISC EM CLI NO PRIV CESS BENI (O PREST SERV): OMAGGI
23000:  If Not ScriviRiga23000(dtrTm) Then GoTo eseguinext

        dtrTm!tm_datregom = NTSCDate(strDatreg)
        dtrTm!tm_numregom = lNumreg

        If ArrDbl(dTdare, oCldPnfa.TrovaNdec(0)) <> ArrDbl(dTavere, oCldPnfa.TrovaNdec(0)) Then
          LogWrite(oApp.Tr(Me, 128774657751092000, _
                        "Attenzione! Manca quadratura dare/avere nella registrazione del |" & strDatreg & "| n. |" & _
                        lNumreg & "| generata dal documento |" & strEstremidoc & "|. Ugualmente registrata."), True)
        End If

EseguiIncasso:

        If bContCompensDDT And strTipork = "D" And (NTSCDec(dtrTm!tm_pagato) <> 0 Or NTSCDec(dtrTm!tm_pagato2) <> 0) And NTSCDec(dtrTm!tm_valuta) = 0 Then
          'se fattura differita emessa e flag 'genera reg. di compensazione con incassi su ddt emessi e c'è un effettivo pagamento di ddt (rilevato in fattura)
          'NB: evenutali abbuoni (rilevati sui DDT) sono già stati contabilizzati in fase di rilevazione acconti su DDT !!!!
          '    se è spuntato di compensare con acconti su DDT sicuramente non è spuntato (blocco da codice) di contabilizzare incassi/pagamenti

          'cerco eventuali acconti su ddt rilevati in scaden
          If Not oCldPnfa.GetScadenFromDDTsFattDiffEm(strDittaCorrente, "D", NTSCInt(dtrTm!tm_anno), _
                                                      dtrTm!tm_serie.ToString, NTSCInt(dtrTm!tm_numdoc), _
                                                      dttScad, NTSCDate(strDatreginOld).ToShortDateString, _
                                                      lNumreginOld, IIf(bInt, "S", "N").ToString, _
                                                      NTSCInt(dtrTm!tm_valuta), strUsaContoFattDoc, bUsaContoFatt) Then
            GoTo eseguinext
          End If
          'genero la registraz. di compensazione (e memorizzo gli estremi in testmag.tm_datregin e testmag.tm_numrgin
          If dttScad.Rows.Count > 0 Then

            'legge la causale da utilizzare per la contabilizzazione
            oCldPnfa.ValCodiceDb(nCaustib.ToString, strDittaCorrente, "TABCAUC", "N", "", dttTmp)
            If dttTmp.Rows.Count = 0 Then
              LogWrite(oApp.Tr(Me, 129205529275810547, _
                      "Causale contabile (|" & nCaustib & "|) per la registrazione della compensazione acconti su DDT emessi con documento |" & strEstremidoc & "| inesistente. Verificarla in 'Personalizzazione vendite')"), True)
              GoTo eseguinext
            Else
              nCausale = nCaustib
            End If

            'calcolo il totale degli acconti
            dTmp = 0
            'se sul DDT c'era un abbuono, non devo considerarlo, perchè l'ho già contabilizzato con gli acconti su DDT
            For i = 0 To dttScad.Rows.Count - 1
              dTmp += NTSCDec(dttScad.Rows(i)!sc_importo)
              dtrTm!tm_abbuono = NTSCDec(dtrTm!tm_abbuono) - NTSCDec(dttScad.Rows(i)!tm_abbuono)
            Next

            'devo mettere 'saldata' la scadenza della fattura da chiudere con gli acconti.
            'NB: sulle scadenze della fattura la rata incassata con i DDT è sempre la 1, visto che 
            'visto che nella CalcolaScad eseguita nella ScriviRiga1000 la prima scadenza (rata 1) ha importo = testmag.tm_pagato (che è uguale a somma di pagato dei ddt collegati)
            For Each dtrT As DataRow In dttSC.Select("sc_numrata = 1")
              If NTSCDec(dtrT!sc_importo) - NTSCDec(dtrTm!tm_abbuono) <> (dTmp * -1) Then
                'gli importi incassati sui DDT sono superiori all'importo del dovuto nel DDT stesso (es tot. DDT tm_totdoc = 56,04 - tm_pagato = 56,05)
                'cambio l'importo sulla prima rata ed aggiungo una scadenza per l'importo residuo
                LogWrite(oApp.Tr(Me, 129822421128316328, "ATTENZIONE: Per la Fattura n. |" & dtrTm!tm_numdoc.ToString & _
                          "| andrebbe rilevata la compensazione tra partita della fattura e acconti contabilizzati sui DDT, " & _
                          "ma l'importo degli acconti letto dalle scadenze (|" & (dTmp * -1).ToString(oApp.FormatImporti) & _
                          "|) è diverso dal totale incassato indicato sulla fattura (|" & _
                          (NTSCDec(dtrT!sc_importo) - NTSCDec(dtrTm!tm_abbuono)).ToString(oApp.FormatImporti) & _
                          "|). Compensazione tra partite non eseguita!"), True)
                GoTo Fine_Compensa_Fattura_incassi_ddt
              End If

              lNumreg = oCldPnfa.LegAggRegcDitt(strDittaCorrente, strDatreg)
              lNumregIncasso = lNumreg
              dTdare = 0
              dTavere = 0
              lRiga = 1 ' n. riga

              dtrT!sc_flsaldato = "S"
              dtrT!sc_dtsaldato = NTSCDate(strDatreg)
              dtrT!sc_rgsaldato = lNumreg
            Next

            If bFattDiffCompDdtPartita And dttScad.Select("sc_tippaga = -1").Length > 0 Then
              'se conto non gestito a scadenze ma solo a partite ed ho settato di perdere la partita dell'acconto
              'non devo fare nulla: solo al salvataggio dovrò andare a cambiare la partita con quella della fattura
              'nelle registraz. degli acconti

              'non è proprio vero: se ho indicato di perdere la partita dell'acconto, potrei comunque dover inserire delle righe di compensazione
              'er via dell'opzione BSVEFDIN/OPZIONI/GestScostAcconti = 1
              'è il caso, ad esempio, di 2 ddt di 100,03 + 100,02 che confluiscono in una fattura di 200,04.
              'devo comunque inserire il record dell'abbuono e quello della partita dell'acconto
              If NTSCDec(dtrTm!tm_abbuono) <> 0 Then
                If NTSCInt(Val(oCldPnfa.GetSettingBus("Bsvefdin", "Opzioni", ".", "GestScostAcconti", "0", " ", "0"))) = 1 Then
                  'FT DIFF EM: storno cliente intestatario fattura differita per importo pari all'abbuono rilevato solo in fattura
                  If Not ScriviRiga23500(dtrTm, (NTSCDec(dtrTm!tm_abbuono) * -1)) Then GoTo eseguinext
                  'FT DIFF EM: abbuono rilevato solo in fattura (attivo o passivo)
                  If Not ScriviRiga26000(dtrTm) Then GoTo eseguinext
                End If
              End If

              GoTo Fine_Compensa_Fattura_incassi_ddt
            End If

            'FT DIFF EM: storno cliente intestatario fattura differita per importo pari ad acconto, su partita di fattura differita
23500:      If Not ScriviRiga23500(dtrTm, dTmp + (NTSCDec(dtrTm!tm_abbuono) * -1)) Then GoTo eseguinext

            'FT DIFF EM: storno cliente cliente intestatario fattura per acconti su DDT emessi e precedentemente contabilizzati
            oDttgr.NTSGroupBy(dttScad, dttGr, "sc_conto, sc_annpar, sc_alfpar, sc_numpar, sum(sc_importo) as xx_importo, sum(tm_abbuono) as tm_abbuono", "", "sc_conto, sc_annpar, sc_alfpar, sc_numpar")
            For Each dtrT As DataRow In dttGr.Rows
23600:        If Not ScriviRiga23600(dtrTm, NTSCDec(dtrT!xx_importo)) Then GoTo eseguinext
              'devo correggere il numero di partita sulla riga appena inserita, indicando quello della scadenza da saldare
              'il rowstate è sempre 'added'
              With dttPN.Rows(dttPN.Rows.Count - 1)
                !pn_annpar = NTSCInt(dtrT!sc_annpar)
                !pn_alfpar = NTSCStr(dtrT!sc_alfpar)
                !pn_numpar = NTSCInt(dtrT!sc_numpar)
              End With
            Next

            'ora imposto il saldato su tutte le scadenze, poi le memorizzo in dttSC (scadenze da inserire/aggiornare)
            'le sc_tippaga = -1 sono scadenze di clienti gestiti solo a partite!!!!
            For Each dtrT As DataRow In dttScad.Select("sc_tippaga <> -1")
              dtrT!sc_flsaldato = "S"
              dtrT!sc_dtsaldato = NTSCDate(strDatreg)
              dtrT!sc_rgsaldato = lNumreg
              dttSC.ImportRow(dtrT)
              If dttSC.Rows(dttSC.Rows.Count - 1).RowState <> DataRowState.Modified Then
                dttSC.Rows(dttSC.Rows.Count - 1).SetModified()
              End If
            Next

            'rilevo, se serve,l'abbuono (attivo o passivo)
            If NTSCDec(dtrTm!tm_abbuono) <> 0 Then
260001:       If Not ScriviRiga26000(dtrTm) Then GoTo eseguinext
            End If

            ' aggiorna dat e num. reg. incasso
            dtrTm!tm_datregin = NTSCDate(strDatreg)
            dtrTm!tm_numrgin = lNumreg

          End If    'If dttScad.Rows.Count > 0 Then
Fine_Compensa_Fattura_incassi_ddt:
        End If    'If bContCompensDDT And strTipork = "D" And NTSCDec(dtrTm!tm_pagato) <> 0 Then

        ' registra ev. incasso pagamento
        If bIncassi = False Then GoTo EseguiCommit

EseguiIncassoAnticipatoDDtEmessi:
        If strTipork = "B" And bContIncDDT Then
          'se devo contabilizzare solo gli incassi su ddt, il 'pagato' deve essere diverso da 0 ...
          If NTSCDec(dtrTm!tm_pagato) = 0 And NTSCDec(dtrTm!tm_pagato2) = 0 Then GoTo eseguinext
        End If

        If NTSCDec(dtrTm!tm_pagato) = 0 And NTSCDec(dtrTm!tm_pagato2) = 0 And NTSCDec(dtrTm!tm_abbuono) = 0 Then GoTo EseguiCommit

        If strTipork = "2" Or strTipork = "4" Then
          ' per ora non è previsto reg. pag per note di accredito !!!!
          GoTo eseguinext
        End If

        If NTSCStr(dtrTm!xx_raggr) = "S" Then
          'se registrazione raggruppata di corrispettivo o ricevuta fiscale emessa (totalmente incassati)
          'registro l'incasso nella stessa registrazione del documento IVA
          lRiga += 1
          strDAContoinc = "A"
          strDACassainc = "D"
        Else
          lNumreg = oCldPnfa.LegAggRegcDitt(strDittaCorrente, strDatreg)
          lNumregIncasso = lNumreg
          dTdare = 0
          dTavere = 0
          lRiga = 1 ' n. riga

          Select Case strTipork
            Case "A", "F", "P", "S", "I" : nCausale = nCauinc : strDAContoinc = "A" : strDACassainc = "D"
            Case "N", "£" : nCausale = nCaupag : strDAContoinc = "D" : strDACassainc = "A"
            Case "D" : nCausale = nCauinc : strDAContoinc = "A" : strDACassainc = "D"
            Case "E" : nCausale = nCauinc : strDAContoinc = "A" : strDACassainc = "D"
            Case "C" : nCausale = nCauinc : strDAContoinc = "A" : strDACassainc = "D"
            Case "L" : nCausale = nCaupag : strDAContoinc = "D" : strDACassainc = "A"
            Case "K" : nCausale = nCaupag : strDAContoinc = "D" : strDACassainc = "A"
            Case "J", "(" : nCausale = nCauinc : strDAContoinc = "A" : strDACassainc = "D"
              '--bolle
            Case "B" : nCausale = nCauinc : strDAContoinc = "A" : strDACassainc = "D"
              'parcellazione
            Case "1", "5" : nCausale = nCauinc : strDAContoinc = "A" : strDACassainc = "D" : strDAControp = "A"
          End Select

          ' legge la causale per vedere se e' coerente con il registro iva/tipo docuneto.
          oCldPnfa.ValCodiceDb(nCausale.ToString, strDittaCorrente, "TABCAUC", "N", "", dttTmp)
          If dttTmp.Rows.Count = 0 Then
            LogWrite(oApp.Tr(Me, 128771981494888000, _
                    "Causale contabile (|" & nCausale & "|) per la registrazione degli incassi nel documento |" & strEstremidoc & "| inesistente. Verificarla in 'Personalizzazione vendite' e in 'Personalizzazione acquisti')"), True)
            GoTo eseguinext
          End If

          strCautregiva = dttTmp.Rows(0)!tb_tipreg.ToString
          lContoiva = oCldPnfa.TrovaContoDaContrEConto(strDittaCorrente, NTSCInt(dttTmp.Rows(0)!tb_ccontriva), NTSCInt(dttTmp.Rows(0)!tb_contiva))
          strGestdtciva = dttTmp.Rows(0)!tb_gestdtciva.ToString
          If strCautregiva <> " " Then
            LogWrite(oApp.Tr(Me, 128774657868404000, _
                    "Per il documento |" & strEstremidoc & "| la causale '|" & nCausale & "|' per incasso/pagamento non puo' essere una causale di tipo Iva."), True)
            GoTo eseguinext
          End If
        End If    'If NTSCStr(dtrTm!xx_raggr) = "S" Then

        'FT EMESSA O RICEVUTA: CLIENTE (O FORN) PER INCASSATO (O PAGATO) + ABBUONO
        'RIC FISC CESS BENI (O PREST SERV) A PRIVATO INCASSATA TOTALMENTE O IN PARTE: CASSA PER ABBUONO
        'RIC FISC CESS BENI (O PREST SERV) A CLI NO PRIVATO INCASSATA PARZIALE O TOTALE: CLIENTE PER INCASSATO E/O ABBUONO
        'RIC FISC CESS BENI A PRIVATO E NON NON INCASSATA: TABPEVE - CRED DA RIC FISC NON INC CESS BENI PER ABBUONO
        'RIC FISC PREST SERV A PRIVATO E NON NON INCASSATA: TABPEVE - CRED DA RIC FISC NON INC PREST SERV PER ABBUONO
        'RIEMIS RIC FISC A PRIVATO CESS BENI (O PREST SERV): IL CLIENTE PRIVATO PER INCASSATO + ABBUONO
        'RIEMIS RIC FISC A NON PRIVATO CESS BENI (O PREST SERV): IL CLIENTE PER INCASSATO + ABBUONO
        'PARCELLAZIONE: FT EMESSA: CLIENTE PER INCASSATO + ABBUONO
24000:  If Not ScriviRiga24000(dtrTm) Then GoTo eseguinext

        'registra in cassa
        If (NTSCDec(dtrTm!tm_pagato) = 0 And NTSCDec(dtrTm!tm_pagato2) = 0) Or (dtrTm!tm_tipork.ToString = "F" And NTSCInt(dtrTm!tm_conto) <> lContoPrimaRiga) Then GoTo registraabb
        'FT EMESSA O RICEVUTA: CASSA O BANCA PER INCASSATO (O PAGATO)
        'RIC FISC CLI NO PRIV CESS BENI (O PREST SERV) INCASSATA TOTALE O IN PARTE: CASSA O BANCA PER INCASSATO
        'RIEM RIC FISC A PRIVATO E NON CESS BENI (O PREST SERV): CASSA O BANCA PER INCASSATO
        'PARCELLAZIONE: FATTURA E/O NOTA ACCRED: CASSA O BANCA PER INCASSATO
25000:  If Not ScriviRiga25000(dtrTm, dttTmOrig) Then GoTo eseguinext

registraabb:
        'reg. eventuale abbuono ...
        If NTSCDec(dtrTm!tm_abbuono) = 0 Then GoTo EseguiFlinc

        'FT EMESSA O RICEVUTA: ABBUONO
        'RIC FISC TUTTI I CASI: ABBUONO
        'RIEM RIC FISC TUTTI I CASI: ABBUONO
        'PARCELLAZIONE: FATTURA E/O NOTA ACCRED: ABBUONO
26000:  If Not ScriviRiga26000(dtrTm) Then GoTo eseguinext

EseguiFlinc:
        ' aggiorna dat e num. reg. incasso
        dtrTm!tm_datregin = NTSCDate(strDatreg)
        dtrTm!tm_numrgin = lNumreg


        If strPrgParent = "BNPAPNPA" And bProfes Then
EseguiGirocontiSospesi:
          '---------------------------------
          ' se è una contabilità professionisti, eseguo i giroconti da conto sospeso a definitivo
          'se c'è una parte di incassato
          If (NTSCDec(dtrTm!tm_pagato) <> 0 Or NTSCDec(dtrTm!tm_abbuono) <> 0) And lConRicSosp <> 0 Then
            For i = 1 To nMaxControp
              If NTSCInt(dttControp.Rows(i)!lControp) <> 0 Then
                'PARCELLAZIONE: FATTURA E/O NOTA ACCRED: STORNO DEL CONTO SOSPESO x assegnazione a RICAVO EFFETTIVO
28000:          If Not ScriviRiga28000(dtrTm, dttControp.Rows(i), dttTmOrig) Then GoTo eseguinext
              Else
                Exit For
              End If
            Next
          End If    'If NTSCDec(dtrTm!tm_pagato) <> 0 Or NTSCDec(dtrTm!tm_abbuono) <> 0 ..
        End If    'If strPrgParent = "BNPAPNPA" And bProfes Then

        If dTdare <> dTavere Then
          LogWrite(oApp.Tr(Me, 128771998718978000, _
                        "Attenzione! Manca quadratura dare/avere nella registrazione del |" & strDatreg & "| n. |" & _
                        lNumreg & "| generata dal documento |" & strEstremidoc & "|."), True)
        End If

saltaincassi:
        'le righe di EseguiNotula vengono eseguite SOLO se il tipork = "3" = emessa notula
        'e ci si arriva da prima della riga 1000
        GoTo EseguiCommit

EseguiNotula:
        lNumreg = oCldPnfa.LegAggRegcDitt(strDittaCorrente, strDatreg)
        dtrTm!tm_flcont = "S"
        dtrTm!tm_datregef = NTSCDate(strDatreg)
        dtrTm!tm_numregef = lNumreg
        strDAConto = "D" : strDAControp = "A" : strDAIva = "A" : strDAContoinc = "A" : strDACassainc = "D"
        'NOTULA: PRIMA RIGA CLIENTI NOTULE
        'e
        'NOTULA: SECONDA RIGA C/TO TRANSITORIO NOTULE
40000:  If Not ScriviRiga40000(dtrTm) Then GoTo eseguinext

EseguiCommit:
        '----------------------
        AggiornaPrinotScaden(dtrTm)
        '----------------------
        'registro, se necessario, gli stanziamenti
        bStornaStanziamenti = False
        If Not GeneraStornoStanziamenti(dtrTm, bStornaStanziamenti, strCausaleStornoStanz) Then
          GoTo eseguinext
        Else
          'verifico se PRINOT è stato scritto correttamente
          If Not PrinotOk() Then
            GoTo eseguinext
          End If
        End If

        '--------------------
        If Not ElaboraTipo_TrattaScadenzeSaldate(dtrTm, dttSC, dttScadRielab, strDatregftOld, _
                                                lNumregftOld, strDatreginOld, lNumreginOld, _
                                                strDatregomOld, lNumregomOld) Then GoTo eseguinext

        lRkfatt += 1


        If strPrgParent <> "BNPAPNPA" Then
          'Modifica per dichiarazione di intenti
          If CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupDII) And bCollega_MG_DI Then
            dtrDataRow = dttPN.Select("pn_nregiva <> 0")
            If dtrDataRow.Length > 0 Then
              dtDatareg = NTSCDate(dtrDataRow(0)!pn_datreg)
              nNumreg = NTSCInt(dtrDataRow(0)!pn_numreg)
              'Eredito i riferimenti alle dichiarazioni di intento
              For Each dtrRow2 As DataRow In dttPN.Select("pn_datreg = " & CDataSQL(dtDatareg) & " And pn_numreg = " & nNumreg)
                dtrRow2!pn_dianno = NTSCInt(dtrTm!tm_dianno)
                dtrRow2!pn_dinumero = NTSCInt(dtrTm!tm_dinumero)
                dtrRow2.AcceptChanges()
              Next
            End If
          End If
        End If

        If bCa And nCADoc = 3 Then
          'CA vecchia
          'nella registrazione ho sia righe con solo dare/avere MG ed altre con dare/avere CG: non devo far contabillizzare la CA!!
          dttCA.Clear()
          dttCA.AcceptChanges()
          lRkfatt = lRkfatt - 1
          LogWrite(oApp.Tr(Me, 128771998718978001, _
                        "Attenzione! Nel documento |" & strEstremidoc & "| sono presenti righe con causale impostata su DARE/AVERE CG ed altre con DARE/AVERE MG. La registrazione non può essere generata per incongruenze legate alla Contabilità analitica "), True)
          GoTo eseguinext
        End If

        If bCa And nCADoc = 1 Then
          'non devo scrivere la CA collegata alla CG
          dttCA.Clear()
          dttCA.AcceptChanges()
        End If

        '--------------------
        'scrivo la registrazione in transazione ed aggiorno testmag
        If dttPN.Rows.Count > 0 Or dttSC.Rows.Count > 0 Then

          If bFattDiffCompDdtPartita Then
            'riaggiungo le scadenze che contengono gli estremi delle registrazioni 
            'su cui inserire la partita della fattura al posto di quella dell'acconto
            If dttScad.Rows.Count > 0 Then
              For Each dtrT As DataRow In dttScad.Select("sc_tippaga = -1")
                dttSC.ImportRow(dtrT)
              Next
            End If
          End If

          oCldPnfa.CreaRegistrazione(strDittaCorrente, dttPN, dttMO, dttSC, dttCA, strDatreg, dtrTm, _
                                     IIf(bInt, "S", "N").ToString, strDatregftOld, lNumregftOld, _
                                     strDatreginOld, lNumreginOld, strDatregomOld, lNumregomOld, _
                                     bGestStanziamenti, lContoOld, nAnnoOld, strSerieOld, lNumdocOld, _
                                     bStornaStanziamenti, lNumregStanziamenti, nCauststanz, strCausaleStornoStanz, _
                                     strErr, nAnnoDoc, strAlfdoc, lNumdoc, bAggProtP, strAlfdocP, lNumdocP, _
                                     bAggProtP2, strAlfdocP2, lNumdocP2, strTipoNumerazVendPerAcqIntra, _
                                     dttCA2, bCa2, dttTmOrig, bProfes, strFattRevCharge, dttScadRielab, _
                                     bGiroEffettiNoChisCli, lConEff, CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupDII), _
                                     bCollega_MG_DI, dsCove, nAggControp, bRevChargeNumdocDaPartita)
          If strErr <> "" Then LogWrite(strErr, True)
        End If

        '--------------------
        'genero le registrazioni di effetti attivi a cliente
        'solo se nella registrazione ho contabilizzato la fattura
        If bGenEffetti And bModuliAcquistati And _
           (NTSCStr(dtrTm!tm_tipork) = "A" Or NTSCStr(dtrTm!tm_tipork) = "D" Or _
           (NTSCStr(dtrTm!tm_tipork) = "B" And bContIncDDT)) Then
          GeneraEffetti(dtrTm, dttPN, dttSC, strEstremidoc)
        End If

        '--------------------
        'aggiorno l'archivio provvigioni
        If bAggProvvig And (NTSCStr(dtrTm!tm_tipork) = "A" Or NTSCStr(dtrTm!tm_tipork) = "D" Or _
                           (NTSCStr(dtrTm!tm_tipork) = "B" And bContIncDDT) Or _
                            NTSCStr(dtrTm!tm_tipork) = "E" Or NTSCStr(dtrTm!tm_tipork) = "N" Or _
                            NTSCStr(dtrTm!tm_tipork) = "£" Or NTSCStr(dtrTm!tm_tipork) = "C" Or _
                            NTSCStr(dtrTm!tm_tipork) = "F" Or NTSCStr(dtrTm!tm_tipork) = "S" Or _
                            NTSCStr(dtrTm!tm_tipork) = "P") Then
          GeneraProvvigioni(dtrTm, dttPN, dttSC, strEstremidoc)
        End If

eseguinext:
        dttScadRielab.Clear()
        dttControp.Clear()
        dttPN.Clear()
        dttMO.Clear()
        dttSC.Clear()
        dttCA.Clear()
        If bCa2 Then dttCA2.Clear()
        dttScadRielab.AcceptChanges()
        dttControp.AcceptChanges()
        dttPN.AcceptChanges()
        dttMO.AcceptChanges()
        dttSC.AcceptChanges()
        dttCA.AcceptChanges()
        If bCa2 Then dttCA2.AcceptChanges()
        'invalido la liquidazione IVA contenente la fattura generata
        'se non c'è la data di registrazione vuol dire che la fattura non è stata contabilizzata (ad esempio xchè ricontabilizzaz con scadenze già saldate)
        If strDatregForm <> "" Then oCldPnfa.InvalidaLiqIVA(strDittaCorrente, strDatregForm)

eseguinext2:
      Next    'For Each dtrTm As DataRow In dttTm.Rows

      LogWrite(oApp.Tr(Me, 128775455413306000, "Elaborazione completata regolarmente. Trattati/registrati |" & lRkfatt.ToString & _
                  "| documenti di tipo |'" & strTipodoc & "'|, scartati |" & (dttTm.Rows.Count - lRkfatt).ToString & "|"), False)

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
      dttTm.Clear()
      dttTmOrig.Clear()
      dttControp.Clear()
      dttPN.Clear()
      dttMO.Clear()
      dttSC.Clear()
      dttCA.Clear()
      If bCa2 Then dttCA2.Clear()
      dttScad.Clear()
      dttGr.Clear()
      dttScadRielab.Clear()
    End Try
  End Function

  Public Overridable Function RaggruppaCorrIncassati(ByVal strTipork As String, ByRef dttTm As DataTable, _
                                                     ByRef dttTmOrig As DataTable, ByVal bBnrepnco As Boolean) As Boolean
    'documenti RETAIL corrispettivi e Ricevute fiscali:
    'se totalmente incassati faccio una registrazione unica intestando la reg. ad un conto non gestito a partie/scadenze
    'es 'Negozio XXX corr. incassati' e la data registrazione sarà la data di apertura cassa (se retail) oppure la data documento se no retail

    'gli elementi di raggruppamento sono:
    '-giorno di chiusura cassa (movcassa.mc_datapertura)                      (se no retail data documento)
    '-cassa da cui sono stati emessi i corrispettivi (testmagc.tm_codrepc)    (se no retail documento unico, indipendente dalla cassa)
    '-incassato totale 
    '-no valuta
    '-serie documento (per diversi reg. corrispettivi dove far confluire il movimento
    '-natura transazione (per cusale CG per intrastat)
    '-causale cg indicata sul tipobf (per far geneare reg. in cg con cauali diverse, soprattutto se non si ha retail)
    '-se i documenti da raggruppare hanno più di 8 cod. iva o più di 20 contropartite, da errore

    'successivamente anche la routine 'calcolacontrop' va rifatta visto che deve sommare tutte le 
    'contropartite dei documenti raggruppati

    'il conto intestatario del documento raggruppato NON DEVE ESSERE GESTITO A PARTITE/SCADENZE (tanto è totalmente incassato)

    'in questa routine devo creare un nuovo datatable (che verrà utilizzato al posto di quello standard)
    'per determinare il numero di registrazioni da generare.
    'quello standard dovrà essere mantenuto per fare la update su testmag alla fine per memorizzare la reg. in CG
    'e per scrivere la CA / CA2

    '------------------------------------------------------------
    'la registrazione dovrà essere del tipo
    'DARE                           AVERE
    'Corrispettivi negozio XXX
    '                               iva 1
    '                               iva 2
    '                               ricavo 1
    '                               ricavo 2
    '                               Corrispettivi negozio XXX
    'cassa 1
    'cassa 2
    '------------------------------------------------------------

    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim nRec As Integer = 0
    Dim dtrTrag() As DataRow = Nothing
    Dim bOk As Boolean = False

    Try
      'tengo una copia del datatable con i documenti di origine NON RAGGRUPPATI
      'dttTm.Columns.("xx_dtttm") 'puntatore al record di dttTm raggruppato
      dttTmOrig = dttTm.Copy

      dttTm.Clear()

      For Each dtrT As DataRow In dttTmOrig.Rows
        nRec += 1
        dtrT!xx_dtttm = nRec

        If NTSCInt(dtrT!tm_valuta) = 0 And _
           NTSCDec(dtrT!tm_totdoc) - NTSCDec(dtrT!tm_totomag) - NTSCDec(dtrT!tm_abbuono) - NTSCDec(dtrT!tm_pagato) - NTSCDec(dtrT!tm_pagato2) + NTSCDec(dtrT!tm_resto) = 0 Then
          'documento da raggruppare: cerco la riga raggruppata nel nuovo datatable e se c'è sommo gli importi
          If bBnrepnco Then
            dtrTrag = dttTm.Select("xx_raggr = 'S' AND tm_numdoc = 0 " & _
                                   " AND tm_codntra = " & dtrT!tm_codntra.ToString & _
                                   " AND tb_codcauc = " & dtrT!tb_codcauc.ToString & _
                                   " AND tm_codrepc = '" & dtrT!tm_codrepc.ToString & "' " & _
                                   " AND mc_datapertura = " & CDataSQL(NTSCDate(dtrT!mc_datapertura)))
          Else
            dtrTrag = dttTm.Select("xx_raggr = 'S' AND tm_numdoc = 0 " & _
                                   " AND tm_codntra = " & dtrT!tm_codntra.ToString & _
                                   " AND tb_codcauc = " & dtrT!tb_codcauc.ToString & _
                                   " AND tm_serie = '" & dtrT!tm_serie.ToString & "' " & _
                                   " AND tm_datdoc = " & CDataSQL(NTSCDate(dtrT!tm_datdoc)))
          End If

          If dtrTrag.Length = 0 Then
            dttTm.ImportRow(dtrT)

            With dttTm.Rows(dttTm.Rows.Count - 1)
              !tm_numdoc = 0  'marco la riga raggruppata
              'mangengo la serie: diveramente non potrei leggere in tabnuma il num. reg. iva da usare per la contabilizzaz. di quella serie
              'inoltre costringerei a creare la serie ' ' anche se non serve
              'inoltre se imposto come serie la ' ', se sto contabilizzando la serie 'B' per il fatto che la serie è un elemento di rottura non 
              'raggrupperei mai i corrispettivi con serie diversa da ' '
              '!tm_serie = " " 'visto che non indico il num. documento, non ha senso indicare la serie
              !xx_raggr = "S" 'marco la riga raggruppata
              If bBnrepnco Then !tm_datdoc = NTSCDate(dtrT!mc_datapertura)

              'conto corrispettivi non gestito a partite/scadenze del tipo 'Corrispettivi negozio XXXX'
              If bBnrepnco Then
                !tm_conto = NTSCInt(dtrT!tb_contocorrinc) 'conto cliente privato preso da tabstab (in retail li stabilimento/filiale/negozio è sempre memorizzato sui documenti)
                If NTSCInt(!tm_conto) = 0 Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129592493918369140, "ATTENZIONE: errore in fase di raggruppamento corrispettivi/ric. fisc. emesse incassate: Nella tabella degli stabilimenti/filiali/negozi non è stato indicato il conto 'Corrispettivi incassati' per il negozio |" & NTSCInt(!tm_codstab).ToString & "|. Elaborazione interrotta")))
                  Return False
                End If
              Else
                !tm_conto = lConclpriv      'conto cliente privato di tabpeve
                If NTSCInt(!tm_conto) = 0 Then
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129592494998525390, "ATTENZIONE: errore in fase di raggruppamento corrispettivi/ric. fisc. emesse incassate: Nella tabella di 'Personalizzazione vendite' non è stato indicato il 'Conto cliente privato'. Elaborazione interrotta")))
                  Return False
                End If
              End If

            End With    'With dttTm.Rows(dttTm.Rows.Count - 1)

          Else
            With dtrTrag(0)
              dtrT!xx_dtttm = !xx_dtttm 'correggo il puntatore del datatable di origine al record raggruppato
              !tm_totlordo = NTSCDec(!tm_totlordo) + NTSCDec(dtrT!tm_totlordo)
              !tm_totmerce = NTSCDec(!tm_totmerce) + NTSCDec(dtrT!tm_totmerce)
              !tm_bolli = NTSCDec(!tm_bolli) + NTSCDec(dtrT!tm_bolli)
              !tm_speinc = NTSCDec(!tm_speinc) + NTSCDec(dtrT!tm_speinc)
              !tm_speacc = NTSCDec(!tm_speacc) + NTSCDec(dtrT!tm_speacc)
              !tm_speimb = NTSCDec(!tm_speimb) + NTSCDec(dtrT!tm_speimb)
              !tm_totomag = NTSCDec(!tm_totomag) + NTSCDec(dtrT!tm_totomag)
              !tm_abbuono = NTSCDec(!tm_abbuono) + NTSCDec(dtrT!tm_abbuono)
              !tm_totdoc = NTSCDec(!tm_totdoc) + NTSCDec(dtrT!tm_totdoc)
              !tm_pagato = NTSCDec(!tm_pagato) + NTSCDec(dtrT!tm_pagato)
              !tm_pagato2 = NTSCDec(!tm_pagato2) + NTSCDec(dtrT!tm_pagato2)
              !tm_resto = NTSCDec(!tm_resto) + NTSCDec(dtrT!tm_resto)

              '----------------------
              'devo integrare il castelletto contropartite ed il castelletto IVA
              'CASTELLETTO IVA
              For i = 1 To 8
                bOk = False
                If NTSCInt(dtrT("tm_codiva_" & i.ToString)) = 0 Then Exit For 'non ho altri cod. iva: esco dal ciclo
                For j = 1 To 8
                  If NTSCInt(dtrTrag(0)("tm_codiva_" & j.ToString)) = 0 Then
                    If bOk = False Then
                      'non ho ancora trovato lo stesso cod. iva e posso aggiungere codici al record raggruppato: lo faccio
                      dtrTrag(0)("tm_codiva_" & j.ToString) = NTSCInt(dtrT("tm_codiva_" & i.ToString))
                      dtrTrag(0)("tm_imponib_" & j.ToString) = NTSCDec(dtrT("tm_imponib_" & i.ToString))
                      dtrTrag(0)("tm_imposta_" & j.ToString) = NTSCDec(dtrT("tm_imposta_" & i.ToString))
                      bOk = True
                      Exit For
                    End If
                  End If
                  If NTSCInt(dtrT("tm_codiva_" & i.ToString)) = NTSCInt(dtrTrag(0)("tm_codiva_" & j.ToString)) Then
                    'ho trovato lo stesso cod. iva: sommo gli importi
                    bOk = True
                    dtrTrag(0)("tm_imponib_" & j.ToString) = NTSCDec(dtrTrag(0)("tm_imponib_" & j.ToString)) + NTSCDec(dtrT("tm_imponib_" & i.ToString))
                    dtrTrag(0)("tm_imposta_" & j.ToString) = NTSCDec(dtrTrag(0)("tm_imposta_" & j.ToString)) + NTSCDec(dtrT("tm_imposta_" & i.ToString))
                    Exit For
                  End If
                Next
                If bOk = False Then
                  'non ho trovato la corrispondeza sul cod. iva ed ho già usato tutti gli 8 elementi del castelletto IVA:
                  'probabilmente non succederà mai...
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129591951569619141, "ATTENZIONE: errore in fase di raggruppamento corrispettivi/ric. fisc. emesse incassate: nel documento raggruppato dovrebbero essere presenti più di 8 codici IVA. Elaborazione interrotta.")))
                  Return False
                End If
              Next    'For i = 1 To 8

              '----------------------
              'CASTELLETTO CONTROPARTITE
              For i = 1 To 20
                bOk = False
                If NTSCInt(dtrT("tm_ccontr_" & i.ToString)) = 0 Then Exit For 'non ho altri cod. controp.: esco dal ciclo
                For j = 1 To 20
                  If NTSCInt(dtrTrag(0)("tm_ccontr_" & j.ToString)) = 0 Then
                    If bOk = False Then
                      'non ho ancora trovato lo stesso cod. iva e posso aggiungere codici al record raggruppato: lo faccio
                      dtrTrag(0)("tm_ccontr_" & j.ToString) = NTSCInt(dtrT("tm_ccontr_" & i.ToString))
                      dtrTrag(0)("tm_impcont_" & j.ToString) = NTSCDec(dtrT("tm_impcont_" & i.ToString))
                      bOk = True
                      Exit For
                    End If
                  End If
                  If NTSCInt(dtrT("tm_ccontr_" & i.ToString)) = NTSCInt(dtrTrag(0)("tm_ccontr_" & j.ToString)) Then
                    'ho trovato lo stesso cod. iva: sommo gli importi
                    bOk = True
                    dtrTrag(0)("tm_impcont_" & j.ToString) = NTSCDec(dtrTrag(0)("tm_impcont_" & j.ToString)) + NTSCDec(dtrT("tm_impcont_" & i.ToString))
                    Exit For
                  End If
                Next
                If bOk = False Then
                  'non ho trovato la corrispondeza sul cod. iva ed ho già usato tutti gli 8 elementi del castelletto IVA:
                  'probabilmente non succederà mai...
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129591954177656250, "ATTENZIONE: errore in fase di raggruppamento corrispettivi/ric. fisc. emesse incassate: nel documento raggruppato dovrebbero essere presenti più di 20 codici CONTROPARTITE e non è settata l'opzione di registro 'BSVEPNFA/OPZIONI/GestDatecomp= -1'. Elaborazione interrotta.")))
                  Return False
                End If
              Next    'For i = 1 To 20

            End With    'With dttTm.Rows(nRecRagg)
          End If    'If dtrTrag.Length = 0 Then

         
        Else
          'documento da contabilizzare normalmente
          dttTm.ImportRow(dtrT)
        End If    'If NTSCInt(dtrT!tm_valuta) = 0 And _
      Next    'For Each dtrT As DataRow In dttTmOrig.Rows

      dttTm.AcceptChanges()
      dttTmOrig.AcceptChanges()

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

  Public Overridable Function MemPartitaeDatdoc(ByRef dtrTm As DataRow) As Boolean
    Try
      '--------------------
      'determina n. partita e data documento
      Select Case dtrTm!tm_tipork.ToString
        Case "K", "L" ' fatt. fornitori (guarda i ...par)
          If NTSCStr(dtrTm!tm_datpar) = "" Then
            strDatdoc = NTSCDate(dtrTm!tm_datdoc).ToShortDateString
          Else
            strDatdoc = NTSCDate(dtrTm!tm_datpar).ToShortDateString
          End If
          If NTSCInt(dtrTm!tm_numpar) <> 0 And (Not (bUsaNumdocPerNumpar)) Then
            strAlfdoc = NTSCStr(dtrTm!tm_alfpar)
            lNumdoc = NTSCInt(dtrTm!tm_numpar)
            nAnnpar = NTSCInt(dtrTm!tm_annpar)
            strAlfpar = NTSCStr(dtrTm!tm_alfpar)
            lNumpar = NTSCInt(dtrTm!tm_numpar)
          Else
            strAlfdoc = NTSCStr(dtrTm!tm_serie)
            lNumdoc = NTSCInt(dtrTm!tm_numdoc)
            nAnnpar = NTSCInt(dtrTm!tm_anno)
            strAlfpar = NTSCStr(dtrTm!tm_serie)
            lNumpar = NTSCInt(dtrTm!tm_numdoc)
          End If
        Case "J", "(" ' note accr. fornitori (guarda i ...par)
          If NTSCStr(dtrTm!tm_datpar) = "" Then
            strDatdoc = NTSCDate(dtrTm!tm_datdoc).ToShortDateString
          Else
            strDatdoc = NTSCDate(dtrTm!tm_datpar).ToShortDateString
          End If
          If NTSCInt(dtrTm!tm_numpar) <> 0 And (Not (bUsaNumdocPerNumpar)) Then
            strAlfdoc = NTSCStr(dtrTm!tm_alfpar)
            lNumdoc = NTSCInt(dtrTm!tm_numpar)
            nAnnpar = NTSCInt(dtrTm!tm_annpar)
            strAlfpar = NTSCStr(dtrTm!tm_alfpar)
            lNumpar = NTSCInt(dtrTm!tm_numpar)
          Else
            strAlfdoc = NTSCStr(dtrTm!tm_serie)
            lNumdoc = NTSCInt(dtrTm!tm_numdoc)
            nAnnpar = NTSCInt(dtrTm!tm_anno)
            strAlfpar = NTSCStr(dtrTm!tm_serie)
            lNumpar = NTSCInt(dtrTm!tm_numdoc)
          End If
        Case "A", "D", "C", "E", "B", "F", "I", "S", "P" ' fatt.clienti + ric. fiscali, vari tipi
          strDatdoc = NTSCDate(dtrTm!tm_datdoc).ToShortDateString
          strAlfdoc = NTSCStr(dtrTm!tm_serie)
          lNumdoc = NTSCInt(dtrTm!tm_numdoc)
          nAnnpar = NTSCInt(dtrTm!tm_anno)
          strAlfpar = NTSCStr(dtrTm!tm_serie)
          lNumpar = NTSCInt(dtrTm!tm_numdoc)
        Case "N", "£"
          strDatdoc = NTSCDate(dtrTm!tm_datdoc).ToShortDateString
          strAlfdoc = NTSCStr(dtrTm!tm_serie)
          lNumdoc = NTSCInt(dtrTm!tm_numdoc)
          'note di accredito emesse da compensare con fatture emesse: 
          'in questo caso il numero di partita è SEMPRE uguale al numero documento, 
          'diversamente il TestElabeRielab controllerebbe se la partita indicata in tm_numpar (quella della fattura)
          'esiste già in scaden ED OVVIAMENTE C'E'!!!!
          If NTSCInt(dtrTm!tm_numpar) > 0 And bUsaNumdocPerNumpar = False And Not (dtrTm!tm_tipork.ToString = "N" And bContCompensNoteAcc) Then
            nAnnpar = NTSCInt(dtrTm!tm_annpar)
            strAlfpar = NTSCStr(dtrTm!tm_alfpar)
            lNumpar = NTSCInt(dtrTm!tm_numpar)
          Else
            nAnnpar = NTSCInt(dtrTm!tm_anno)
            strAlfpar = NTSCStr(dtrTm!tm_serie)
            lNumpar = NTSCInt(dtrTm!tm_numdoc)
          End If
        Case "1", "5", "3"   ' fatt.clienti, corrispettivi, avviso di parcella/notula
          strDatdoc = NTSCDate(dtrTm!tm_datdoc).ToShortDateString
          strAlfdoc = NTSCStr(dtrTm!tm_serie)
          lNumdoc = NTSCInt(dtrTm!tm_numdoc)
          nAnnpar = NTSCInt(dtrTm!tm_anno)
          strAlfpar = NTSCStr(dtrTm!tm_serie)
          lNumpar = NTSCInt(dtrTm!tm_numdoc)
        Case "2", "4" ' note accredito clienti (guarda i ...par solo per riferimento fattura)
          strDatdoc = NTSCDate(dtrTm!tm_datdoc).ToShortDateString
          strAlfdoc = NTSCStr(dtrTm!tm_serie)
          lNumdoc = NTSCInt(dtrTm!tm_numdoc)
          If NTSCInt(dtrTm!tm_numpar) > 0 And (Not (bUsaNumdocPerNumpar)) Then
            nAnnpar = NTSCInt(dtrTm!tm_annpar)
            strAlfpar = NTSCStr(dtrTm!tm_alfpar)
            lNumpar = NTSCInt(dtrTm!tm_numpar)
          Else
            nAnnpar = NTSCInt(dtrTm!tm_anno)
            strAlfpar = NTSCStr(dtrTm!tm_serie)
            lNumpar = NTSCInt(dtrTm!tm_numdoc)
          End If
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
  Public Overridable Function TestDataRegistrazioneSuLgoRi(ByVal strDatreg As String, ByRef dtrTm As DataRow) As Boolean
    Try
      '----------------------
      'CONTROLLA CHE APPARTENGA ALL'ESERCIZIO SCELTO
      If NTSCDate(strDatreg) < NTSCDate(strDtineser) Or NTSCDate(strDatreg) > NTSCDate(strDtfieser) Then
        LogWrite(oApp.Tr(Me, 128770069378868000, "Attenzione!" & vbCrLf & "Documento |" & strEstremidoc & "| non contabilizzato." & _
                        " La data di registrazione (|" & strDatreg & "|) NON è contenuta nell'esercizio selezionato."), True)
        Return False
      End If
      '----------------------
      If NTSCDate(strDatreg) < NTSCDate(dtrTm!tm_datdoc) Then
        LogWrite(oApp.Tr(Me, 128770069391036000, "Attenzione!" & vbCrLf & "Documento |" & strEstremidoc & "| non contabilizzato." & _
                                "La data di registrazione (|" & strDatreg & "|) è minore della data del documento (|" & _
                                NTSCDate(dtrTm!tm_datdoc).ToShortDateString & "|)"), True)
        Return False
      End If
      '----------------------
      ' test su libro giornale (strDtullg già determinato in TestPreElabora)
      If NTSCDate(strDtullg) > NTSCDate(strDatreg) Then
        LogWrite(oApp.Tr(Me, 128770065278678000, "Attenzione!" & vbCrLf & "Data di registrazione anteriore " & _
                                        " all'ultima stampa del Libro Giornale " & _
                                        "nel documento|" & strEstremidoc & "|. Documento non contabilizzato."), True)
        Return False
      End If

      '----------------------
      If NTSCDate(strDtulaca) > NTSCDate(strDatreg) Then
        LogWrite(oApp.Tr(Me, 129278253569716796, "Attenzione!" & vbCrLf & "Data di registrazione anteriore " & _
                                        " alla data ultimo aggiornamento di CA (memorizzata in anagrafica ditta) " & _
                                        "nel documento|" & strEstremidoc & "|. Documento non contabilizzato."), True)
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
  Public Overridable Function RitornaContoPrimaRiga(ByVal lTmconto As Integer, ByRef dtrTm As DataRow, ByRef strAnscaden As String) As Integer
    ' ric. fiscali (deroghe) su lconto, se = cliente privato
    Dim lConto As Integer = lTmconto ' per i casi normali
    Dim dttTmp As New DataTable
    Try
      strAnscaden = "S"
      strTipsogivaContoPrimaRiga = "N"
      RitornaContoPrimaRiga = lTmconto ' per i casi normali

      If strPrgParent = "BNPAPNPA" Then
        'per le parcelle il comto è sempre l'intestatario del documento
        lConto = lTmconto
        GoTo SALTA
      End If

      ' richiede sia già stato letto peve, settato strPrestserv, e sia esistente dtrTm

      'gestione specifica corrispettivi o ric. fisc. emesse totalmente incassate e raggruppate
      If (NTSCStr(dtrTm!tm_tipork) = "C" Or NTSCStr(dtrTm!tm_tipork) = "F") And _
          NTSCInt(dtrTm!tm_numdoc) = 0 And NTSCStr(dtrTm!xx_raggr) = "S" Then
        strAnscaden = "N"   'FISSO!!!!!
        Return NTSCInt(dtrTm!tm_conto)     'già determinato correttamente nella routine 'RaggruppaCorrIncassati'
      End If

      If NTSCInt(dtrTm!tm_conto) = lConclpriv Then
        If strPrestserv <> "S" Then ' cessioni
          If dtrTm!tm_tipork.ToString = "I" Then
            'setto la cassa sono se non è settata l'opzione per poter emettere ric fisc a cliente privato senza incassarle immediatamente
            If CBool(oCldPnfa.GetSettingBus("Bsveboll", "Opzioni", ".", "RicFiscContoPrivNonInc", "0", " ", "0")) = False Then  ' NON DOCUMENTARE
              If NTSCDec(dtrTm!tm_pagato) = 0 And NTSCDec(dtrTm!tm_pagato2) <> 0 Then
                lConto = lConcas2
              Else
                lConto = lConcas
              End If
            End If
          End If
          If dtrTm!tm_tipork.ToString = "F" Then
            If bTotinc Or bParzinc Then
              If NTSCDec(dtrTm!tm_pagato) = 0 And NTSCDec(dtrTm!tm_pagato2) <> 0 Then
                lConto = lConcas2
              Else
                lConto = lConcas
              End If
            Else
              If bNotinc Then
                lConto = lConcrrfc
              End If
            End If
          End If
        Else ' prestaz. di servizi
          If dtrTm!tm_tipork.ToString = "I" Then
            'setto la cassa sono se non è settata l'opzione per poter emettere ric fisc a cliente privato senza incassarle immediatamente
            If CBool(oCldPnfa.GetSettingBus("Bsveboll", "Opzioni", ".", "RicFiscContoPrivNonInc", "0", " ", "0")) = False Then ' NON DOCUMENTARE
              If NTSCDec(dtrTm!tm_pagato) = 0 And NTSCDec(dtrTm!tm_pagato2) <> 0 Then
                lConto = lConcas2
              Else
                lConto = lConcas
              End If
            End If
          End If
          If dtrTm!tm_tipork.ToString = "F" Then
            If bTotinc Or bParzinc Then
              If NTSCDec(dtrTm!tm_pagato) = 0 And NTSCDec(dtrTm!tm_pagato2) <> 0 Then
                lConto = lConcas2
              Else
                lConto = lConcas
              End If
            Else
              If bNotinc Then
                lConto = lConcrrfp
              End If
            End If
          End If
        End If
      Else ' cod. cliente <> da privato
        If strPrestserv <> "S" Then ' cessioni
          If dtrTm!tm_tipork.ToString = "I" Then
            'lConto = lConcas ' mette il cliente
          End If
          If dtrTm!tm_tipork.ToString = "F" Then
            If bTotinc Or bParzinc Then
              'lConto = lConcas
            Else
              If bNotinc Then
                lConto = lConcrrfc
              End If
            End If
          End If
        Else ' prestaz. di servizi
          If dtrTm!tm_tipork.ToString = "I" Then
            'lConto = lConcas
          End If
          If dtrTm!tm_tipork.ToString = "F" Then
            If bTotinc Or bParzinc Then
              'lConto = lConcas
            Else
              If bNotinc Then
                lConto = lConcrrfp
              End If
            End If
          End If
        End If
      End If

SALTA:
      ' decodifica anagra
      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count > 0 Then
        strAnscaden = dttTmp.Rows(0)!an_scaden.ToString
        strTipsogivaContoPrimaRiga = dttTmp.Rows(0)!an_tpsogiva.ToString
      End If

      Return lConto

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
  Public Overridable Function DeterminaCampiVari(ByVal strTipork As String) As Boolean
    Try
      Select Case strTipork
        Case "A" : nCausale = NTSCInt(IIf(strAutotr = "S", nCauemftat, nCauemft)) : strTiponuma = "A" : strMemtregiva = "V"
          strDAConto = "D" : strDAControp = "A" : strDAIva = "A" : strDAContoinc = "A" : strDACassainc = "D"
        Case "N", "£" : nCausale = NTSCInt(IIf(strAutotr = "S", nCaunoacat, nCaunoac)) : strTiponuma = "A" : strMemtregiva = "V"
          strDAConto = "A" : strDAControp = "D" : strDAIva = "D" : strDAContoinc = "D" : strDACassainc = "A"
        Case "D" : nCausale = NTSCInt(IIf(strAutotr = "S", nCauemftat, nCauemft)) : strTiponuma = "A" : strMemtregiva = "V"
          strDAConto = "D" : strDAControp = "A" : strDAIva = "A" : strDAContoinc = "A" : strDACassainc = "D"
        Case "E" : nCausale = NTSCInt(IIf(strAutotr = "S", nCauemftat, nCauemft)) : strTiponuma = "A" : strMemtregiva = "V"
          strDAConto = "D" : strDAControp = "A" : strDAIva = "A" : strDAContoinc = "A" : strDACassainc = "D"
        Case "C" : nCausale = nCaucorr : strTiponuma = "C" : strMemtregiva = "C"
          strDAConto = "D" : strDAControp = "A" : strDAIva = "A" : strDAContoinc = "A" : strDACassainc = "D"
        Case "L" : nCausale = nCaurcft : strTiponuma = "L" : strMemtregiva = "A"
          strDAConto = "A" : strDAControp = "D" : strDAIva = "D" : strDAContoinc = "D" : strDACassainc = "A"
        Case "K" : nCausale = nCaurcft : strTiponuma = "L" : strMemtregiva = "A"
          strDAConto = "A" : strDAControp = "D" : strDAIva = "D" : strDAContoinc = "D" : strDACassainc = "A"
        Case "J", "(" : nCausale = nCaurcna : strTiponuma = "L" : strMemtregiva = "A"
          strDAConto = "D" : strDAControp = "A" : strDAIva = "A" : strDAContoinc = "A" : strDACassainc = "D"
        Case "B" : nCausale = nCaucorr : strTiponuma = "C" : strMemtregiva = "C"
          strDAConto = "D" : strDAControp = "A" : strDAIva = "A" : strDAContoinc = "A" : strDACassainc = "D"
          ' fatt/ric. fiscale immediata, e diff. (non so se è giusto reg. corrispettiv o reg. vendite, per ora vendite)
          ' lo tratta come fatture emmediate normali, solo nuemratore S anzichè V
        Case "S", "P" : nCausale = NTSCInt(IIf(strAutotr = "S", nCauemftat, nCauemft)) : strTiponuma = "S" : strMemtregiva = "V"
          strDAConto = "D" : strDAControp = "A" : strDAIva = "A" : strDAContoinc = "A" : strDACassainc = "D"
          ' ric. fiscale immediata,  (reg. corrispettivi)
        Case "F" : nCausale = NTSCInt(IIf(strAutotr = "S", nCauemftat, nCauemft)) : strTiponuma = "F" : strMemtregiva = "C"
          strDAConto = "D" : strDAControp = "A" : strDAIva = "A" : strDAContoinc = "A" : strDACassainc = "D"
          If strPrestserv <> "S" Then ' cessione
            If bTotinc Then ' tot incassata
              strMemtregiva = "C" '  iva
              nCausale = nCaurfincc
            Else
              If bParzinc Then 'aprz. incassata
                strMemtregiva = "C" '  iva
                nCausale = nCaurfparc
              Else ' non incassata
                strMemtregiva = "C" '  iva
                nCausale = nCaurfnonc
              End If
            End If
          Else ' prestazione
            If bTotinc Then ' tot incassata
              strMemtregiva = "C" '  iva
              nCausale = nCaurfincp
            Else
              If bParzinc Then 'aprz. incassata
                strMemtregiva = "C" '  iva
                nCausale = nCaurfparp
              Else ' non incassata
                strMemtregiva = " " '  non iva
                nCausale = nCaurfnonp
              End If
            End If
          End If
          ' ric. fiscale riemessa,  (reg. corrispettivi)
        Case "I" : nCausale = NTSCInt(IIf(strAutotr = "S", nCauemftat, nCauemft)) : strTiponuma = "F" : strMemtregiva = "C"
          strDAConto = "D" : strDAControp = "A" : strDAIva = "A" : strDAContoinc = "A" : strDACassainc = "D"
          If strPrestserv = "S" Then
            strMemtregiva = "C" '  iva
            nCausale = nCaurfriep
          Else
            strMemtregiva = " " ' non iva
            nCausale = nCaurfriec
          End If

          '-------------------
          'PARCELLAZIONE
        Case "1" : nCausale = nCauemft : strTiponuma = "P1" : strMemtregiva = "V"
          strDAConto = "D" : strDAControp = "A" : strDAIva = "A" : strDAContoinc = "A" : strDACassainc = "D"
        Case "2" : nCausale = nCaunoacsf : strTiponuma = "P1" : strMemtregiva = "V"
          strDAConto = "A" : strDAControp = "D" : strDAIva = "D" : strDAContoinc = "D" : strDACassainc = "A"
        Case "4" : nCausale = nCaunoac : strTiponuma = "P1" : strMemtregiva = "V"
          strDAConto = "A" : strDAControp = "D" : strDAIva = "D" : strDAContoinc = "D" : strDACassainc = "A"
        Case "5" : nCausale = nCaucorr : strTiponuma = "P3" : strMemtregiva = "C"
          strDAConto = "D" : strDAControp = "A" : strDAIva = "A" : strDAContoinc = "A" : strDACassainc = "D"

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
  Public Overridable Function CalcolaIvaInded(ByRef dtrTm As DataRow, ByRef dIvaInded As Decimal, ByRef dIvaIndedVal As Decimal) As Boolean
    'calcolo l'importo dell'IVA indeducibile
    Dim i As Integer = 0
    Dim dttTmp As New DataTable
    Try
      dIvaInded = 0
      dIvaIndedVal = 0

      For i = 1 To 8
        If NTSCInt(dtrTm("tm_codiva_" & i.ToString)) <> 0 Then
          GetTabcivaRow(NTSCInt(dtrTm("tm_codiva_" & i.ToString)), dttTmp)
          If dttTmp.Rows.Count = 0 Then
            With dtrTm
              LogWrite(oApp.Tr(Me, 128770296863400000, "Attenzione!" & vbCrLf & _
                        "Codice Iva |" & dtrTm("tm_codiva_" & i.ToString).ToString & "| inesistente" & vbCrLf & _
                        "per il documento |" & strEstremidoc & "|."), True)
            End With
            Return False
          Else
            If CalcolaIvaIndetr(strCautregiva, NTSCDec(dttTmp.Rows(0)!tb_inded)) <> 0 Then
              dIvaInded = dIvaInded + ArrDblEcc((NTSCDec(dtrTm("tm_imposta_" & i.ToString)) * CalcolaIvaIndetr(strCautregiva, NTSCDec(dttTmp.Rows(0)!tb_inded)) / 100), oCldPnfa.TrovaNdec(0))
              dIvaIndedVal = dIvaIndedVal + ArrDblEcc((NTSCDec(dtrTm("tm_impostav_" & i.ToString)) * CalcolaIvaIndetr(strCautregiva, NTSCDec(dttTmp.Rows(0)!tb_inded)) / 100), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
            End If
          End If
        End If
      Next
      dIvaInded = ArrDbl(dIvaInded, oCldPnfa.TrovaNdec(0))
      dIvaIndedVal = ArrDbl(dIvaIndedVal, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))

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

  Public Overridable Function Calcolacontropartite(ByRef dtrTm As DataRow) As Boolean
    Try
      'obsoleta
      Return Calcolacontropartite(dtrTm, Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function Calcolacontropartite(ByRef dtrTm As DataRow, ByRef dttTmOrig As DataTable) As Boolean
    Dim i As Integer
    Dim e As Integer
    Dim dImporto As Decimal
    Dim dImpVal As Decimal
    Dim dImpTmc As Decimal            'somma del castelletto contropartite di testmagc
    Dim dImpvalTmc As Decimal         'somma del castelletto contropartite di testmagc
    Dim dttTmp As New DataTable
    Dim dttGr As New DataTable
    Dim oDttgr As New CLEGROUPBY
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dtrTm, dttTmOrig})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrTm = CType(oIn(0), DataRow)
        dttTmOrig = CType(oIn(1), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      dttControp.Clear()
      For i = 0 To 150
        dttControp.Rows.Add(New Object() {0, 0, 0, 0, 0, NTSCDate(dtrTm!tm_datdoc).ToShortDateString, NTSCDate(dtrTm!tm_datdoc).ToShortDateString, NTSCDate(dtrTm!tm_datdoc).ToShortDateString, NTSCDate(dtrTm!tm_datdoc).ToShortDateString, 0, "S"})
      Next
      dttControp.AcceptChanges()

      If (bGestDatecomp And NTSCStr(dtrTm!tm_flscdb) <> "S") Or strPrgParent = "BNPAPNPA" Then
        ' se è attivata l'opzione di considerare le date di inizio/fine validità rileggo tutto da movmag
        'per la parcellazione va letto sempre movpar
        If NTSCStr(dtrTm!xx_raggr) = "S" And Not dttTmOrig Is Nothing Then
          'se il documento che sto realizzando è un corrispettivo o ric fisc emessa totalmente incassata raggruppato
          'devo prendere i movimenti da tutti i documenti che verranno raggruppati
          If Not oCldPnfa.GetDettaglioContropRagg(strDittaCorrente, NTSCInt(dtrTm!xx_dtttm), dttTmOrig, dttTmp) Then Return False
        Else
          'caso standard
          If Not oCldPnfa.GetDettaglioControp(strDittaCorrente, dtrTm!tm_tipork.ToString, NTSCInt(dtrTm!tm_anno), _
                                              dtrTm!tm_serie.ToString, NTSCInt(dtrTm!tm_numdoc), dttTmp, bGestDatecomp) Then Return False
        End If
        i = 0
        dImporto = 0
        dImpVal = 0
        If dttTmp.Rows.Count = 0 Then
          'non dovrebbe mai succedere .....
          LogWrite(oApp.Tr(Me, 128770861152364000, "Operazione interrotta in fase di determinazione castelletto " & _
                    "contropartite per il documento|" & strEstremidoc & "|. Non sono state trovate righe di contropartite"), True)
          Return False
        End If

        If strPrgParent <> "BNPAPNPA" Then
          'miglioria in caso di documenti con scorporo e num. di decimali su prezzi unitari = 2.
          'per ogni contropartita trovata su movmag (potrebbero esserci anche più record) 
          'controllo il totale su testmagc e correggo un importo in modo che la somma di movmag sia uguale al valore di testmagc
          'prima veniva arrotondata sempre l'ultima contropartita, ma era brutto se questa non era spezzettata su più date di competenza economica,
          'soprattuto se l'ultima contropartita era una riga con un cod. iva specifico, dove si vedeva in prima nota una netta differenza tra imponibile e contropartita
          oDttgr.NTSGroupBy(dttTmp, dttGr, "mm_controp, sum(valore) as valore, sum(valorev) as valorev", "", "mm_controp")
          For Each dtrT As DataRow In dttGr.Rows
            For i = 1 To 20
              If NTSCInt(dtrT!mm_controp) = NTSCInt(dtrTm("tm_ccontr_" & i.ToString)) And NTSCInt(dtrT!mm_controp) <> 0 Then
                If NTSCDec(dtrT!valore) <> ArrDbl(NTSCDec(dtrTm("tm_impcont_" & i.ToString)), oCldPnfa.TrovaNdec(0)) Or _
                   NTSCDec(dtrT!valorev) <> ArrDbl(NTSCDec(dtrTm("tm_impcontv_" & i.ToString)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta))) Then
                  dImporto = ArrDbl(NTSCDec(dtrTm("tm_impcont_" & i.ToString)), oCldPnfa.TrovaNdec(0)) - NTSCDec(dtrT!valore)
                  dImpVal = ArrDbl(NTSCDec(dtrTm("tm_impcontv_" & i.ToString)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta))) - NTSCDec(dtrT!valorev)
                  Dim dtrT1 As DataRow = dttTmp.Select("mm_controp = " & dtrT!mm_controp.ToString, "valore DESC")(0)
                  dtrT1!valore = NTSCDec(dtrT1!valore) + dImporto
                  dtrT1!valorev = NTSCDec(dtrT1!valorev) + dImpVal
                End If
                Exit For
              End If
            Next
          Next
          dttGr.Clear()
        End If

        i = 0
        dImporto = 0
        dImpVal = 0

        'i conti non gestiti a date o periodo competenza economica li raggruppo
        For Each dtrT As DataRow In dttTmp.Rows
          'elaboro solo le righe con valore <> 0, diversamente scriverebbe in prinot anche righe che non servono
          '(solo se il totale documento è diverso da 0, diversamente genererebbe una registrazione sbilanciata)...
          If (NTSCDec(dtrT!valore) <> 0 Or NTSCDec(dtrT!valorev) <> 0) Or _
            (NTSCDec(dtrT!mm_controp) <> 0 And ArrDbl(NTSCDec(dtrTm!tm_totdoc) + NTSCDec(dtrTm!tm_totdocv), oCldPnfa.TrovaNdec(0)) = 0) Then
            i = i + 1
            'Controlla se sforiamo il massimo consentito
            If i > nMaxControp Then
              LogWrite(oApp.Tr(Me, 128770872186286000, "Operazione interrotta in fase di determinazione castelletto " & _
                        "contropartite per il documento|" & strEstremidoc & "|. Sono state trovate oltre |" & nMaxControp & "| righe di contropartite/date competenza economica"), True)
              dttTmp.Clear()
              Return False
            End If
            If i > 1 Then
              If NTSCInt(dttControp.Rows(i - 1)!lControp) = NTSCInt(dtrT!mm_controp) And dtrT!an_accperi.ToString <> "S" And dtrT!an_accperi.ToString <> "D" Then
                i = i - 1
                dttControp.Rows(i)!dImporto = ArrDbl(NTSCDec(dttControp.Rows(i)!dImporto) + NTSCDec(dtrT!valore), oCldPnfa.TrovaNdec(0))
                dttControp.Rows(i)!dImpVal = ArrDbl(NTSCDec(dttControp.Rows(i)!dImpVal) + NTSCDec(dtrT!valorev), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
              Else
                dttControp.Rows(i)!lControp = dtrT!mm_controp
                dttControp.Rows(i)!dImporto = ArrDbl(NTSCDec(dtrT!valore), oCldPnfa.TrovaNdec(0))
                dttControp.Rows(i)!dImpVal = ArrDbl(NTSCDec(dtrT!valorev), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
                dttControp.Rows(i)!dIvaInded = 0
                dttControp.Rows(i)!dIvaIndedVal = 0
                dttControp.Rows(i)!strDtIniz = IIf(dtrT!an_accperi.ToString = "S" Or dtrT!an_accperi.ToString = "D", CStr(dtrT!mm_datini), CStr(dtrTm!tm_datdoc))
                dttControp.Rows(i)!strDtFin = IIf(dtrT!an_accperi.ToString = "S" Or dtrT!an_accperi.ToString = "D", CStr(dtrT!mm_datfin), CStr(dtrTm!tm_datdoc))
                dttControp.Rows(i)!strDtInizOrig = CStr(dtrT!mm_datini)
                dttControp.Rows(i)!strDtFinOrig = CStr(dtrT!mm_datfin)
              End If
            Else
              dttControp.Rows(i)!lControp = dtrT!mm_controp
              dttControp.Rows(i)!dImporto = ArrDbl(NTSCDec(dtrT!valore), oCldPnfa.TrovaNdec(0))
              dttControp.Rows(i)!dImpVal = ArrDbl(NTSCDec(dtrT!valorev), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
              dttControp.Rows(i)!dIvaInded = 0
              dttControp.Rows(i)!dIvaIndedVal = 0
              dttControp.Rows(i)!strDtIniz = IIf(dtrT!an_accperi.ToString = "S" Or dtrT!an_accperi.ToString = "D", CStr(dtrT!mm_datini), CStr(dtrTm!tm_datdoc))
              dttControp.Rows(i)!strDtFin = IIf(dtrT!an_accperi.ToString = "S" Or dtrT!an_accperi.ToString = "D", CStr(dtrT!mm_datfin), CStr(dtrTm!tm_datdoc))
              dttControp.Rows(i)!strDtInizOrig = CStr(dtrT!mm_datini)
              dttControp.Rows(i)!strDtFinOrig = CStr(dtrT!mm_datfin)
            End If
            dImporto = ArrDbl(dImporto + NTSCDec(dtrT!valore), oCldPnfa.TrovaNdec(0))
            dImpVal = ArrDbl(dImpVal + NTSCDec(dtrT!valorev), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
          End If    'If ntscdec(dtrT!valore) <> 0 Or ntscdec(dtrT!valorev) <> 0 Then
        Next    'For Each dtrT As DataRow In dttTmp.Rows
        dttTmp.Clear()

        dImpTmc = 0
        dImpvalTmc = 0
        If strPrgParent = "BNPAPNPA" Then
          'parcellazione
          'confronto con totale compensi di piede (compens) + totale rimborsi di piede (nonsogg) + acconti da scalare
          'il totale del corpo è dato dalla somma dei compensi - acconti
          'se la somma contropartite non è uguale, correggo l'ultima
          dImpTmc = ArrDbl(NTSCDec(dtrTm!tm_compens) + NTSCDec(dtrTm!tm_nonsogg) + NTSCDec(dtrTm!tm_totaccscal), oCldPnfa.TrovaNdec(0))
          If dImporto <> dImpTmc Then
            dttControp.Rows(i)!dImporto = ArrDbl(dImpTmc - (dImporto - NTSCDec(dttControp.Rows(i)!dImporto)), oCldPnfa.TrovaNdec(0))
          End If
        Else
          'verifico che la somma delle contropartite ricalcolate sia uguale al castelletto memorizzato in testmagc,
          'se non lo è correggo l'ultima contropartita. non dovrebbe servire più con la modifica fatt acon 
          'oDttgr.NTSGroupBy(dttTmp, dttGr, "mm_controp, sum(valore) as valore, sum(valorev) as valorev", "", "mm_controp")
          'ma per scrupolo ... 
          For e = 1 To 20
            dImpTmc = dImpTmc + ArrDbl(NTSCDec(dtrTm("tm_impcont_" & e.ToString)), oCldPnfa.TrovaNdec(0))
            dImpvalTmc = dImpvalTmc + ArrDbl(NTSCDec(dtrTm("tm_impcontv_" & e.ToString)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
          Next
          If dImporto <> dImpTmc Then
            dttControp.Rows(i)!dImporto = ArrDbl(dImpTmc - (dImporto - NTSCDec(dttControp.Rows(i)!dImporto)), oCldPnfa.TrovaNdec(0))
          End If
          If dImpVal <> dImpvalTmc Then
            dttControp.Rows(i)!dImpVal = ArrDbl(dImpvalTmc - (dImpVal - NTSCDec(dttControp.Rows(i)!dImpVal)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
          End If
        End If    'If strPrgParent = "BNPAPNPA" Then

      Else
        If bGestDatecomp Then
          LogWrite(oApp.Tr(Me, 128770865121094000, "Nel documento |" & strEstremidoc & _
                         "| si sono forzati i totali mediante la funzione 'sblocca castelletti'." & vbCrLf & _
                         "Non sarà possibile, per questo documento, riportare in contabilità le date " & _
                         "di inizio/fine competenza economica"), True)
        End If
        ' altrimenti carico le contropartite da testmagc
        e = 0
        For i = 1 To 20
          'carico solo le contropartite con valore o valore in valuta <> 0 
          '(solo se il totale documento è diverso da 0, diversamente genererebbe una registrazione sbilanciata)
          If (ArrDbl(NTSCDec(dtrTm("tm_impcont_" & i.ToString)), oCldPnfa.TrovaNdec(0)) <> 0 Or _
              ArrDbl(NTSCDec(dtrTm("tm_impcontv_" & i.ToString)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta))) <> 0) Or _
            (NTSCInt(dtrTm("tm_ccontr_" & i.ToString)) <> 0 And ArrDbl(NTSCDec(dtrTm!tm_totdoc) + NTSCDec(dtrTm!tm_totdocv), oCldPnfa.TrovaNdec(0)) = 0) Then
            e = e + 1
            dttControp.Rows(e)!lControp = NTSCInt(dtrTm("tm_ccontr_" & i.ToString))
            dttControp.Rows(e)!dImporto = ArrDbl(NTSCDec(dtrTm("tm_impcont_" & i.ToString)), oCldPnfa.TrovaNdec(0))
            dttControp.Rows(e)!dImpVal = ArrDbl(NTSCDec(dtrTm("tm_impcontv_" & i.ToString)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
            dttControp.Rows(e)!dIvaInded = 0
            dttControp.Rows(e)!dIvaIndedVal = 0
            dttControp.Rows(e)!strDtIniz = NTSCDate(dtrTm!tm_datdoc).ToShortDateString
            dttControp.Rows(e)!strDtFin = NTSCDate(dtrTm!tm_datdoc).ToShortDateString
            dttControp.Rows(e)!strDtInizOrig = NTSCDate(dtrTm!tm_datdoc).ToShortDateString
            dttControp.Rows(e)!strDtFinOrig = NTSCDate(dtrTm!tm_datdoc).ToShortDateString
          End If
        Next
      End If

      'memorizzo sul vettore quali sottoconti gestiscono l'iva indeducibile
      If bGirocontoIvaIndedRipartito Then
        oCldPnfa.AggSottocontiIvaInded(strDittaCorrente, dttControp)
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
      dttControp.AcceptChanges()
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function ParcellazAddControp(ByVal lConto As Integer, ByVal dImporto As Decimal, ByVal dImpVal As Decimal, _
                                                 ByVal strDatIni As String, ByVal strDatFin As String, _
                                                 ByVal strDatIniOrig As String, ByVal strDatFinOrig As String) As Boolean
    'dopo aver scritto prinot devo aggiungere al dettaglio contropartite 
    'le spese generali, le spese di incasso ecc.
    'questo perchè se c'è una parte INCASSATA devo stornare il conto sospeso e riassegnare il conto specifico

    'i conti interessati sono:
    '-rimborso bolli
    '-rimborso spese di incasso
    '-spese generali
    '-cassa commercialisti

    Dim i As Integer = 1
    Try
      If strPrgParent <> "BNPAPNPA" Then Return True

      While NTSCInt(dttControp.Rows(i)!lControp) <> 0
        i += 1
      End While

      dttControp.Rows(i)!lControp = lConto
      dttControp.Rows(i)!dImporto = dImporto
      dttControp.Rows(i)!dImpVal = dImpVal
      dttControp.Rows(i)!dIvaInded = 0
      dttControp.Rows(i)!dIvaIndedVal = 0
      dttControp.Rows(i)!strDtIniz = strDatIni
      dttControp.Rows(i)!strDtFin = strDatFin
      dttControp.Rows(i)!strDtInizOrig = strDatIniOrig
      dttControp.Rows(i)!strDtFinOrig = strDatFinOrig
      dttControp.AcceptChanges()

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


  Public Overridable Function ScriviRiga1000(ByRef dtrTm As DataRow) As Boolean
    Try
      'obsoleta
      Return ScriviRiga1000(dtrTm, Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ScriviRiga1000(ByRef dtrTm As DataRow, ByRef dttTmOrig As DataTable) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable
    Dim bTm_pagato2 As Boolean = False  'caso retail: l'incassato è stato indicato solo su tm_pagato2
    Dim dttPagato2 As New DataTable     'CASO RETAIL: per documenti raggruppati totalmente incassati poteri aver utilizzato per eseguire l'incasso diversi cod. pagamento
    '                                                 in questo datatable sono contenuti, divisi per codpaga, i vari record di pagamento con il relativo importo
    Dim dtrT1() As DataRow = Nothing

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dtrTm, dttTmOrig})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrTm = CType(oIn(0), DataRow)
        dttTmOrig = CType(oIn(1), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      ' inizia la scrittura della riga cliente/fornitore ...
      ' per i casi normali (non ric. fiscali ) ...
      'per intrastat/rsm/reverse charge il totlae documento è senza IVA
      lConto = NTSCInt(dtrTm!tm_conto)
      If (dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "L" Or _
          dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "(") And _
          (nCodntra <> 0 Or strFattRevCharge <> "N") Then
        If dsTabciva.Tables("TABCIVA").Columns.Contains("tb_revcharge") And strFattRevCharge = "M" And nCodntra = 0 Then
          'fattura reverse charge misti da net 2013 (dove nel documento sono presenti cod. iva con flag 'reverse charge' ed altri no)
          dImporto = ArrDbl(NTSCDec(dtrTm!tm_totdoc) - dTotivaRC, oCldPnfa.TrovaNdec(0))
          dImpVal = ArrDbl(NTSCDec(dtrTm!tm_totdocv) - dTotivavRC, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
        Else
          dImporto = ArrDbl(NTSCDec(dtrTm!tm_totdoc) - dTotiva, oCldPnfa.TrovaNdec(0))
          dImpVal = ArrDbl(NTSCDec(dtrTm!tm_totdocv) - dTotivav, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
        End If
      Else
        dImporto = ArrDbl(NTSCDec(dtrTm!tm_totdoc), oCldPnfa.TrovaNdec(0))
        dImpVal = ArrDbl(NTSCDec(dtrTm!tm_totdocv), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      End If
      ' deroghe ric. fiscali ric. fiscali : casi non normali, lconto <> tm_conto se cliente privato
      lConto = RitornaContoPrimaRiga(NTSCInt(dtrTm!tm_conto), dtrTm, strAnscaden)
      ' deroghe ric. fiscali su importi

      'stesso codice sia per cessioni che per prestazioni di servizi
      If dtrTm!tm_tipork.ToString = "F" Then
        If bParzinc Then
          If dImpincassato - NTSCDec(dtrTm!tm_pagato2) + NTSCDec(dtrTm!tm_resto) - NTSCDec(dtrTm!tm_abbuono) <> 0 Then
            'caso standard: l'incassato è stato indicato su tm_pagato ed eventualmente anche su tm_pagato2
            'se la ricevuta fiscale è emessa non ad un privato devo assegnare tutto il'incassato a lconto cliente
            'perchè nella reg. successiva prima storna totalmente il cliente per il tot. incassato!!!
            'If (NTSCDec(dtrTm!tm_pagato2) <> 0 Or NTSCDec(dtrTm!tm_resto) <> 0) And NTSCInt(dtrTm!tm_conto) = lConclpriv Then
            '  dImporto = ArrDbl(dImpincassato - NTSCDec(dtrTm!tm_pagato2) + NTSCDec(dtrTm!tm_resto), oCldPnfa.TrovaNdec(0))
            'Else
            'se ric fisc non incassata l'importo va già bene!!!
            If dImpincassato <> 0 Then dImporto = dImpincassato + NTSCDec(dtrTm!tm_totomag)
            'End If
          Else
            'caso retail: l'incassato è stato indicato solo su tm_pagato2
            dImporto = dImpincassato
            bTm_pagato2 = True
          End If
          dImpVal = ArrDbl(dImpincassatov, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
        Else
          If dImporto <> NTSCDec(dtrTm!tm_pagato2) - NTSCDec(dtrTm!tm_resto) + NTSCDec(dtrTm!tm_abbuono) Then
            'caso standard: l'incassato è stato indicato su tm_pagato ed eventualmente anche su tm_pagato2
            'se la ricevuta fiscale è emessa non ad un privato devo assegnare tutto il'incassato a lconto cliente
            'perchè nella reg. successiva prima storna totalmente il cliente per il tot. incassato!!!
            'If (NTSCDec(dtrTm!tm_pagato2) <> 0 Or NTSCDec(dtrTm!tm_resto) <> 0) And NTSCInt(dtrTm!tm_conto) = lConclpriv Then
            '  dImporto = ArrDbl(dImporto - NTSCDec(dtrTm!tm_pagato2) + NTSCDec(dtrTm!tm_resto), oCldPnfa.TrovaNdec(0))
            'Else
            'se ric fisc non incassata l'importo va già bene!!!
            If dImpincassato <> 0 Then dImporto = dImpincassato + NTSCDec(dtrTm!tm_totomag)
            'End If
          Else
            'caso retail: l'incassato è stato indicato solo su tm_pagato2
            bTm_pagato2 = True
          End If
        End If
      End If

      '--- fine deroghe ric. fiscali per la 1.a riga
      ' memorizza il conto primariga per succ. trattamento
      lContoPrimaRiga = lConto
      strDarave = strDAConto
      lControp = 0
      If bRegIvaContropRiga1 And dttControp.Select("dimporto <> 0 AND lcontrop <> 0").Length > 0 Then
        'se non ho impostato manualmente la contropartita prima riga, la prendo come da opzione di registro
        lControp = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dttControp.Select("dimporto <> 0 AND lcontrop <> 0")(0)!lcontrop))
      End If
      dImponib = 0
      dImponibval = 0
      lNumprot = lNewnumprot ' ### per i fornitori... (ci vuole)
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0
      lRigaiva = 0 ' strAlfpro già settato
      ' chiude la reg. della prima riga
      If bModuliAcquistati = True Then
        oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
        If dttTmp.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 128770838734318000, "(1000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto intestatario del documento (o, per ricevute fiscali, uno dei sengueti conti di " & _
                        "'personalizzazione vendite': conto cassa, crediti da ric. fisc non incassate " & _
                        "per cessione di beni o prestazioni di servizi) inesistente"), True)
          Return False
        Else
          If lConto <> NTSCInt(dtrTm!tm_conto) Then
            If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
              LogWrite(oApp.Tr(Me, 128770936881608000, "(1000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di contropartita |" & lConto & "| gestito  a partite/scadenze"), True)
              Return False
            End If
          End If
        End If
        'FATT EMESSA O RICEVUTA: IL CLIENTE O IL FORNITORE
        'RICEVUTA FISCALE EMESSA A PRIVATO X CESS BENI (O PREST SERV) INCASSATA IN PARTE O TOTALE: CASSA
        'RIC FISC EM A PRIVATO CESS BENI NON INCASSATA: (TABPEVE - CRED DA RIC FISC NON INC CESS BENI)
        'RIC FISC EM A PRIVATO PREST SERV NON INCASSATA: (TABPEVE - CRED DA RIC FISC NON INC PREST SERV)
        'RIC FISC EM NO PRIVATO CESS BENI (O PREST SERV) INCASSATA IN PARTE O TOTALE: IL CLIENTE
        'RIC FISC EM NO PRIVATO CESS BENI NON INCASSATA: (TABPEVE - CRED DA RIC FISC NON INC CESS BENI)
        'RIC FISC EM NO PRIVATO PREST SERV NON INCASSATA: (TABPEVE - CRED DA RIC FISC NON INC PREST SERV)
        'RIEM RIC FISC A PRIVATO CESS BENI: IL CLIENTE PRIVATO PER STORNO TABPEVE - CRED DA RIC FISC NON INC CESS BENI
        'RIEM RIC FISC A PRIVATO PREST SERV: IL CLIENTE PRIVATO PER STORNO TABPEVE - CRED DA RIC FISC NON INC PREST SERV
        'RIEM RIC FISC A NON PRIVATO CESS BENI: IL CLIENTE PER STORNO TABPEVE - CRED DA RIC FISC NON INC CESS BENI
        'RIEM RIC FISC A NON PRIVATO PREST SERV: IL CLIENTE PER STORNO TABPEVE - CRED DA RIC FISC NON INC PREST SERV
        'new 2012:
        'CORRISPETTIVO EMESSO TOTALMENTE INCASSATO RAGGRUPPATO: CONTO CLIENTE PRIVATO DI TABPEVE SE NO RETAIL, ELSE CONTO CLIENTE PRIVATO INDICATO IN TABSTAB
        'RICEVUTA FISCALE EMESSA (a privato o no) sia cess beni che servizi TOTALMENTE INCASSATO RAGGRUPPATO: CONTO CLIENTE PRIVATO DI TABPEVE SE NO RETAIL, ELSE CONTO CLIENTE PRIVATO INDICATO IN TABSTAB
        'PARCELLAZIONE: FATTURA E/O NOTA ACCRED: IL CLIENTE
        nWriPnTipologia = 0
        bWripnIncassato = False
1000:   bOk = Wripn(1000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                  dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
                  strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", _
                  strAlfpro, lRigaiva)
        If Not bOk Then
          LogWrite(oApp.Tr(Me, 128770306738020000, "(1000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
          Return False
        End If
      End If ' se c'è anche modulo CG
      ' --- calcoal scadenze...
      bOk = AggScaden(dtrTm, strDatreg, lNumreg, nCausale, strDatdoc, lNumdoc, strAlfdoc, nAnnpar, strAlfpar, _
                      lNumpar, strDAConto)
      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128770309656838000, "(1000) Operazione interrotta elaborando le scadenze " & _
                        "del documento |" & strEstremidoc & "|."), True)
        Return False
      End If
      If bModuliAcquistati Then 'solo se ho la CG ...
        If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
          If Not WriPriana2(1000, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
            LogWrite(oApp.Tr(Me, 129278280295673828, "(1000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
            Return False
          End If
        End If
      End If

      'LE RIGHE SOTTO FACEVANO GENERARE UNA REGSITRAZIONE NON CORRETTA.
      'risalendo al perchè era stato fatto così, dovrebbe essere perchè con ric. fisc. totalmente incassate
      'la prima riga era sempre la cassa per tm_pagato (quindi andava aggiunto il record per il tm_pagato 2)
      'ma con retail (l'unico programma in grado di impostare tm_pagato2) anche per le ricevute fiscali emesse
      'si passa sempre per il conto 'cassa negozio' per poi stornarlo sempre nella stessa registrazione per 
      'assegnare l'incassato a casa/banca
      '      'solo per ricevute fiscali a privati totalmente incassate: se parte dell'incasso è ANCHE su tm_pagato2 
      '      '(oltre che su tm_pagato) devo aggiungere la riga del secondo pagamento
      '      If dtrTm!tm_tipork.ToString = "F" And bModuliAcquistati = True Then
      '        If bTm_pagato2 = False And NTSCDec(dtrTm!tm_pagato2) <> 0 And NTSCInt(dtrTm!tm_conto) = lConclpriv Then
      '          dImporto = ArrDbl(NTSCDec(dtrTm!tm_pagato2) - NTSCDec(dtrTm!tm_resto), oCldPnfa.TrovaNdec(0))
      '          dImpVal = 0
      '          If NTSCInt(dtrTm!tb_concassp2) > 0 Then
      '            lConto = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrTm!tb_concassp2))
      '          Else
      '            lConto = lConcas2
      '          End If
      '          lRiga += 1
      '1001:     bOk = Wripn(1001, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
      '                    dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
      '                    strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", _
      '                    strAlfpro, lRigaiva)
      '          If Not bOk Then
      '            LogWrite(oApp.Tr(Me, 129587531721054687, "(1001) Operazione interrotta elaborando " & _
      '                          "il documento |" & strEstremidoc & "|."), True)
      '            Return False
      '          End If
      '          If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
      '            If Not WriPriana2(1001, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
      '              LogWrite(oApp.Tr(Me, 129587531911289062, "(1001) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
      '              Return False
      '            End If
      '          End If
      '        End If
      '      End If    'If dtrTm!tm_tipork.ToString = "F" And bModuliAcquistati = True Then

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
      dttPagato2.Clear()
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function ScriviRiga1100(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      If ArrDbl(dTotiva, oCldPnfa.TrovaNdec(0)) = 0 Then Return True 'cod. iva esente (non c'è l'iva)

      'storno il cliente per iva split payment
      'pn_riga fisso = 2
      'pn_csriga fisso = -2
      lRiga = lRiga + 1
      lConto = lContoPrimaRiga
      strDescr = " "
      dImporto = ArrDbl(dTotiva, oCldPnfa.TrovaNdec(0))
      dImpVal = 0
      strDarave = "A"
      If NTSCStr(dtrTm!tm_tipork) = "N" Or NTSCStr(dtrTm!tm_tipork) = "£" Then strDarave = "D" 'nota di accredito emessa
      lControp = lContoIVASplitPayment
      dImponib = 0
      dImponibval = 0
      lNumprot = 0
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0
      lRigaiva = 0 ' strAlfpro già settato

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 130655528086065396, "(1100) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Storno conto cliente intestatario del documento per IVA split payment: conto non impostato in personalizzazione CG"), True)
        Return False
      End If

1100: bOk = Wripn(1100, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                  dttTmp.Rows(0)!an_scaden.ToString, oApp.Tr(Me, 130655547075081086, "IVA SPLIT PAYMENT"), nCausale, dImporto, strDarave, nNregiva, _
                  strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", _
                  " ", lRigaiva)
      If Not bOk Then
        LogWrite(oApp.Tr(Me, 130655528159412370, "(1100) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
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
  Public Overridable Function ScriviRiga1110(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      If ArrDbl(dTotiva, oCldPnfa.TrovaNdec(0)) = 0 Then Return True 'cod. iva esente (non c'è l'iva)

      'carico il nuovo conto 'iva split payment'  dell'iva del documento
      'pn_riga fisso = 3
      'pn_csriga fisso = -2
      lRiga = lRiga + 1
      lConto = lContoIVASplitPayment
      strDescr = " "
      dImporto = ArrDbl(dTotiva, oCldPnfa.TrovaNdec(0))
      dImpVal = 0
      strDarave = "D"
      If NTSCStr(dtrTm!tm_tipork) = "N" Or NTSCStr(dtrTm!tm_tipork) = "£" Then strDarave = "A" 'nota di accredito emessa
      lControp = lContoPrimaRiga
      dImponib = 0
      dImponibval = 0
      lNumprot = 0
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0
      lRigaiva = 0 ' strAlfpro già settato

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 130655529054793286, "(1110) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Giroconto dell'IVA al conto IVA Split payment: conto non impostato in personalizzazione CG"), True)
        Return False
      End If

1110: bOk = Wripn(1110, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                  dttTmp.Rows(0)!an_scaden.ToString, oApp.Tr(Me, 130655536989447346, "IVA SPLIT PAYMENT"), nCausale, dImporto, strDarave, nNregiva, _
                  strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", _
                  " ", lRigaiva)
      If Not bOk Then
        LogWrite(oApp.Tr(Me, 130655529498250093, "(1110) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
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

  Public Overridable Function ScriviRiga2000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      ' CASO 1: r.f. cessioni, parz. incassate .... : aggiunge riga cassa per la parte incassata
      lRiga = lRiga + 1
      lConto = lConcrrfc
      strDescr = " "
      dImporto = ArrDbl(dImpnotincassato, oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(dImpnotincassatov, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      strDarave = "D"
      lControp = NTSCInt(dtrTm!tm_conto)
      dImponib = 0
      dImponibval = 0
      lNumprot = 0
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0
      lRigaiva = 0 ' strAlfpro già settato

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128770844400324000, "(2000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di 'personalizzazione vendite' crediti da ric. fisc non incassate " & _
                        "per cessione di beni inesistente"), True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128771937439236000, "(2000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito  a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      'RIC FISC EM CESS BENI A CLIENTE PRIV E NON, 'NON TOTALMENTE INCASSATA': IMPORTO RESIDUO (CRED RIC FISC NON INC CESS BENI - TABPEVE)
2000: bOk = Wripn(2000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                  dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
                  strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", _
                  " ", lRigaiva)
      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128770313413932000, "(2000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If
      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(2000, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
          LogWrite(oApp.Tr(Me, 129278281187187500, "(2000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
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
  Public Overridable Function ScriviRiga3000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      ' caso 2 : ric. fisc. prestazione di serv. parz. incassata
      lRiga = lRiga + 1
      lConto = lConcrrfp
      strDescr = " "
      dImporto = ArrDbl(dImpnotincassato, oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(dImpnotincassatov, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      strDarave = "D"
      lControp = NTSCInt(dtrTm!tm_conto)
      dImponib = 0
      dImponibval = 0
      lNumprot = 0
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128770845693408000, "(3000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di 'personalizzazione vendite' crediti da ric. fisc non incassate " & _
                        "per prestazioni di servizi inesistente"), True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128770937606228000, "(3000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      'RIC FISC EM PREST SERV A CLIENTE PRIVATO E NON PARZ INCASSATA: TABPEVE - CRED DA RIC FISC NON INC PREST SERV
3000: bOk = Wripn(3000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                  dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
                  strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", " ", lRigaiva)

      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128770314793718000, "(3000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If
      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(3000, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
          LogWrite(oApp.Tr(Me, 129278281014785156, "(3000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
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
  Public Overridable Function ScriviRiga4000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dRapp1 As Decimal = 0
    Dim dttTmp As New DataTable

    Try
      ' segue con iva da ric. fisc non inc.
      lRiga = lRiga + 1
      lConto = lConivrfp
      strDescr = " "
      'dRapp1 = dImpnotincassato / (dImpnotincassato + dImpincassato)
      dTotiva = NTSCDec(dtrTm!tm_imposta_1) + NTSCDec(dtrTm!tm_imposta_2) + NTSCDec(dtrTm!tm_imposta_3) + NTSCDec(dtrTm!tm_imposta_4) + NTSCDec(dtrTm!tm_imposta_5) + NTSCDec(dtrTm!tm_imposta_6) + NTSCDec(dtrTm!tm_imposta_7) + NTSCDec(dtrTm!tm_imposta_8)
      dTotivav = NTSCDec(dtrTm!tm_impostav_1) + NTSCDec(dtrTm!tm_impostav_2) + NTSCDec(dtrTm!tm_impostav_3) + NTSCDec(dtrTm!tm_impostav_4) + NTSCDec(dtrTm!tm_impostav_5) + NTSCDec(dtrTm!tm_impostav_6) + NTSCDec(dtrTm!tm_impostav_7) + NTSCDec(dtrTm!tm_impostav_8)
      'dImporto = ArrDbl(dTotiva * dRapp1, oCldPnfa.TrovaNdec(0))
      'dImpVal = ArrDbl(dTotivav * dRapp1, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))

      'lavoro al contrario: prima determino l'importo incassato, poi per differenza quello non incassato.
      'diversamente potrebbero esserci delle squadrature a causa degli arrotondamenti.
      'per prova fare 'ric fisc per prestaz. servizi' di imponibile 650, iva 108,33 incassata per 325!!!
      dRapp1 = dImpincassato / (dImpnotincassato + dImpincassato)
      dImporto = ArrDbl(dTotiva - ArrDbl((dTotiva * dRapp1), oCldPnfa.TrovaNdec(0)), oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(dTotivav - ArrDbl((dTotivav * dRapp1), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta))), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      strDarave = "A"
      lControp = NTSCInt(dtrTm!tm_conto)
      dImponib = 0
      dImponibval = 0
      lNumprot = 0
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128770975558284000, "(4000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di 'personalizzazione vendite' IVA da ric. fisc non incassate " & _
                        "per prestazioni di servizi inesistente"), True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128770937859728000, "(4000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      'RIC FISC EM PREST SERV A CLIENTE PRIVATO E NON PARZ INCASSATA: TABPEVE - IVA DA RIC FISC NON INCASS PREST SERV
4000: bOk = Wripn(4000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                  dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
                  strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", _
                  strAlfpro, lRigaiva)

      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128770316832482000, "(4000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If
      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(4000, dtrTm, lNumreg, lRiga, "2", 0, Nothing, 0) Then
          LogWrite(oApp.Tr(Me, 129278281502617187, "(4000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
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

  'contropartite
  Public Overridable Function ScriviRiga5000(ByRef dtrTm As DataRow, ByRef dtrControp As DataRow) As Boolean
    Try
      'obsoleta
      Return ScriviRiga5000(dtrTm, dtrControp, Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ScriviRiga5000(ByRef dtrTm As DataRow, ByRef dtrControp As DataRow, _
                                             ByRef dttTmOrig As DataTable) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dtrTm, dtrControp, dttTmOrig})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrTm = CType(oIn(0), DataRow)
        dtrControp = CType(oIn(1), DataRow)
        dttTmOrig = CType(oIn(2), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      If bModuliAcquistati = False Then Return True

      lRiga = lRiga + 1
      If strPrgParent = "BNPAPNPA" Then
        lConto = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrControp!lControp))
      Else
        oCldPnfa.ValCodiceDb(NTSCInt(dtrControp!lControp).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
        If dttTmp.Rows.Count = 0 Then
          'è stata cancellata la contropartita dopo averla usata nel documento...
          LogWrite(oApp.Tr(Me, 128770846127868000, "(5000) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "E' indicata una contropartita contabile (|" & lConto.ToString & _
                          "|) non presente nella tabella delle contropartite ditta."), True)
          Return False
        End If
        lConto = NTSCInt(dttTmp.Rows(0)!tb_concove)
      End If

      dttTmp.Clear()
      strDescr = " "
      dImporto = ArrDbl(NTSCDec(dtrControp!dImporto), oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(NTSCDec(dtrControp!dImpVal), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      strDarave = strDAControp
      If dImporto < 0 And bCa = False And NTSCDec(dtrControp!dIvaInded) = 0 Then ' se non c'è analitica e valore negativo inverte il segno
        If strDarave = "D" Then
          strDarave = "A"
        Else
          strDarave = "D"
        End If
        dImporto = dImporto * -1
        dImpVal = dImpVal * -1
      End If
      lControp = NTSCInt(dtrTm!tm_conto)
      dImponib = 0
      dImponibval = 0
      lNumprot = lNewnumprot ' ### per i fornitori... (ci vuole)
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0
      lRigaiva = 0

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128770970130140000, "(5000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di costo/ricavo associato alla contropartita di 'contropartite ditta' |" & _
                        dtrControp!lControp.ToString & "| inesistente"), True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128770970231072000, "(5000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito  a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      'FATT EM O RICEVUTA: RICAVO (COSTO)
      'RIC FISC TUTTI I CASI: RICAVO
      'PARCELLAZIONE: FATTURA E/O NOTA ACCRED: RICAVO
      nWriPnTipologia = 1
      bWripnIncassato = False
5000: bOk = Wripn(5000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                  dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto + NTSCDec(dtrControp!dIvaInded), _
                  strDarave, nNregiva, strTregiva, nCodIva, dImponib, lNumprot, dImpVal + NTSCDec(dtrControp!dIvaIndedVal), _
                  dImponibval, lControp, NTSCDate(dtrControp!strDtIniz).ToShortDateString, _
                  NTSCDate(dtrControp!strDtFin).ToShortDateString, strAlfpro, lRigaiva)

      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128772004047680000, "(5000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      'gestione della cont. analitica
      If bCa Then
        bOk = WriPriana(dtrTm, lConto, strDarave, dImporto, NTSCInt(dtrControp!lControp), dImpVal, _
                        NTSCDec(dtrControp!dIvaInded), NTSCDec(dtrControp!dIvaIndedVal), _
                        NTSCDate(dtrControp!strDtIniz).ToShortDateString, _
                        NTSCDate(dtrControp!strDtFin).ToShortDateString, False, _
                        dttPN.Rows(dttPN.Rows.Count - 1), dttTmOrig)

        If Not bOk Then
          LogWrite(oApp.Tr(Me, 128770973280248000, "(5000) Operazione interrotta elaborando la contabilità analitica " & _
                          "del documento |" & strEstremidoc & "|."), True)
          Return False
        End If
      End If    'If bCa Then

      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(5000, dtrTm, lNumreg, lRiga, "0", NTSCInt(dtrControp!lControp), dtrControp, _
                          NTSCDec(dtrControp!dIvaInded), dttTmOrig, NTSCDec(dtrControp!dIvaIndedval)) Then
          LogWrite(oApp.Tr(Me, 129278281851572265, "(5000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
          Return False
        End If
      End If

      'se nel castelletto contropartite sono presenti sottoconti di cespiti, avviso che la registrazione di CG dovrà essere riaperta per integrare la parte dei cespiti
      If CBool(ModuliDittaDitt(strDittaCorrente) And bsModCE) Then
        If NTSCStr(dttTmp.Rows(0)!an_tipacq) = "S" Or NTSCStr(dttTmp.Rows(0)!an_tipacq) = "X" Then
          LogWrite(oApp.Tr(Me, 130415243325991672, "Registrazione n. |" & lNumreg.ToString & "| del |" & strDatreg & _
                   "|: nella registrazione è stato acquistato/ceduto un Cespite (|" & NTSCStr(dttTmp.Rows(0)!an_descr1) & _
                   "|): sarà necessario riaprire la registrazione per alimentare l'archivio cespiti. Vedi documento |" & strEstremidoc & "|."), True)
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

  Public Overridable Function ScriviRigaSpeseVarie(ByRef dtrTm As DataRow, ByVal lRigaMsg As Integer, _
                                                  ByVal strDescrErr As String, ByVal strDarave As String, _
                                                  ByVal lConto As Integer, ByVal dImporto As Decimal, _
                                                  ByVal dImpVal As Decimal) As Boolean
    Try
      Return ScriviRigaSpeseVarie(dtrTm, lRigaMsg, strDescrErr, strDarave, lConto, dImporto, dImpVal, -1)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ScriviRigaSpeseVarie(ByRef dtrTm As DataRow, ByVal lRigaMsg As Integer, _
                                                    ByVal strDescrErr As String, ByVal strDarave As String, _
                                                    ByVal lConto As Integer, ByVal dImporto As Decimal, _
                                                    ByVal dImpVal As Decimal, ByVal lControp As Integer) As Boolean
    Dim strDescr As String = " "
    Dim strAnscaden As String = ""
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable
    Dim strCa2Id As String = "1"
    Dim strMsg As String = ""

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dtrTm, lRigaMsg, strDescrErr, strDarave, lConto, dImporto, dImpVal, lControp})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrTm = CType(oIn(0), DataRow)
        Return CBool(oOut)
      End If
      '----------------
      If bModuliAcquistati = False Then Return True

      lRiga = lRiga + 1
      strDescr = " "
      If lControp = -1 Then lControp = NTSCInt(dtrTm!tm_conto)
      dImponib = 0
      dImponibval = 0
      lNumprot = lNewnumprot
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128771756844784000, "(|" & lRigaMsg & "|) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. ") & strDescrErr, True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128771756808124000, "(|" & lRigaMsg & "|) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito  a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      bOk = Wripn(lRigaMsg, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                  dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
                  strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", _
                  strAlfpro, lRigaiva)
      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128771756733868000, "(|" & lRigaMsg & "|) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      'gestione della cont. analitica
      If bCa Then
        Select Case lRigaMsg
          Case 6000 : strMsg = oApp.Tr(Me, 129593337558408203, "DIFFERENZE DI CONVERSIONE VALUTA / EURO")
          Case 6200 : strMsg = oApp.Tr(Me, 129651368387714843, "PARCELLAZIONE - RITENUTA")
          Case 6250 : strMsg = oApp.Tr(Me, 129651368400351562, "PARCELLAZIONE - CLIENTE PER STORNO RITENUTA")
          Case 6300 : strMsg = oApp.Tr(Me, 129651368411708984, "PARCELLAZIONE - ENASARCO")
          Case 6350 : strMsg = oApp.Tr(Me, 129651368423154297, "PARCELLAZIONE - CLIENTE PER STORNO ENASARCO")
          Case 6500 : strMsg = oApp.Tr(Me, 129651319537695312, "PARCELLAZIONE - SPESE GENERALI")
          Case 6600 : strMsg = oApp.Tr(Me, 129651319554970703, "CASSA COMMERCIALISTI")
          Case 7000 : strMsg = oApp.Tr(Me, 129593337801406250, "BOLLI")
          Case 8000 : strMsg = oApp.Tr(Me, 129593337814121093, "SPESE INCASSO")
          Case 9000 : strMsg = oApp.Tr(Me, 129593337830917968, "SPESE TRASPORTO")
          Case 10000 : strMsg = oApp.Tr(Me, 129593337845400390, "SPESE IMBALLO")
          Case Else : strMsg = oApp.Tr(Me, 129593337946835937, "Identificativo riga: |" & lRigaMsg & "|")
        End Select
        bOk = WriPrianaAcc(lConto, strDarave, dImporto, dImpVal, dttPN.Rows(dttPN.Rows.Count - 1), dtrTm)
        If Not bOk Then
          LogWrite(oApp.Tr(Me, 128771756748844000, "(|" & lRigaMsg & "|) Operazione interrotta elaborando la contabilità analitica (|" & strMsg & "|) " & _
                          "del documento |" & strEstremidoc & "|."), True)
          Return False
        End If
      End If    'If bCa Then

      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        Select Case lRigaMsg
          Case 6000 : strCa2Id = "1"    'DIFFERENZE DI CONVERSIONE VALUTA / EURO
          Case 6200 : strCa2Id = "10"   'RITENUTA
          Case 6250 : strCa2Id = "11"   'CLIENTE PER STORNO RITENUTA
          Case 6300 : strCa2Id = "12"   'ENSARCO
          Case 6350 : strCa2Id = "13"   'CLIENTE PER STORNO ENSARCO
          Case 6500 : strCa2Id = "14"   'SPESE GENERALI
          Case 6600 : strCa2Id = "15"   'CASSA COMMERCIALISTI
          Case 7000 : strCa2Id = "8"    'BOLLI
          Case 8000 : strCa2Id = "3"    'SPESE INCASSO
          Case 9000 : strCa2Id = "5"    'SPESE TRASPORTO
          Case 10000 : strCa2Id = "4"   'SPESE IMBALLO
          Case Else : strCa2Id = "1"
        End Select
        If Not WriPriana2(lRigaMsg, dtrTm, lNumreg, lRiga, strCa2Id, 0, Nothing, 0) Then
          LogWrite(oApp.Tr(Me, 129278282792441406, "(|" & lRigaMsg & "|) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
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
  Public Overridable Function ScriviRiga6000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strDarave As String = "D"

    Try
      If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "N" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "£" Then ' se fatt. di acquisto ricevute o note di accr. emesse
        If NTSCDec(dtrTm!tm_diffda) > 0 Then
          lConto = lCondfconv
          strDarave = "D"
          dImporto = ArrDbl(NTSCDec(dtrTm!tm_diffda), oCldPnfa.TrovaNdec(0))
        Else
          lConto = lCondaconv
          strDarave = "A"
          dImporto = ArrDbl(NTSCDec(dtrTm!tm_diffda) * -1, oCldPnfa.TrovaNdec(0))
        End If
      Else
        If NTSCDec(dtrTm!tm_diffda) > 0 Then
          lConto = lCondaconv
          strDarave = "A"
          dImporto = ArrDbl(NTSCDec(dtrTm!tm_diffda), oCldPnfa.TrovaNdec(0))
        Else
          lConto = lCondfconv
          strDarave = "D"
          dImporto = ArrDbl(NTSCDec(dtrTm!tm_diffda) * -1, oCldPnfa.TrovaNdec(0))
        End If
      End If
      dImpVal = 0 ' le diff. di conv non hanno importo in valuta
      strDescr = oApp.Tr(Me, 128771767221436000, _
                              "Conto differenze attive e/o passive di conversione " & _
                              "indicato in 'Personalizzazione contabilità generale' inesistente")
      'DIFFERENZE DI CONVERSIONE VALUTA / EURO
6000: Return ScriviRigaSpeseVarie(dtrTm, 6000, strDescr, strDarave, lConto, dImporto, dImpVal)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function ScriviRiga6200(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strDarave As String
    Dim i As Integer = 0

    Try
      lConto = lConRitenut
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_ritenut), oCldPnfa.TrovaNdec(0))
      dImpVal = 0
      strDarave = strDAConto
      strDescr = oApp.Tr(Me, 129651362092978515, "Conto 'Ritenuta' non indicato 'personalizzazione parcellazione'")
      'PARCELLAZIONE: fattura e/o nota accred: ritenuta (tm_spegen)
      nWriPnTipologia = 0
      bWripnIncassato = False
6200: Return ScriviRigaSpeseVarie(dtrTm, 6200, strDescr, strDarave, lConto, dImporto, dImpVal)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ScriviRiga6250(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strDarave As String

    Try
      lConto = lContoPrimaRiga
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_ritenut), oCldPnfa.TrovaNdec(0))
      dImpVal = 0
      strDarave = strDAControp
      strDescr = ""
      'PARCELLAZIONE: fattura e/o nota accred: cliente per storno ritenuta
      nWriPnTipologia = 0
      bWripnIncassato = False
6250: Return ScriviRigaSpeseVarie(dtrTm, 6250, strDescr, strDarave, lConto, dImporto, dImpVal, lConRitenut)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ScriviRiga6300(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strDarave As String

    Try
      lConto = lConAltriprev
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_altriprev), oCldPnfa.TrovaNdec(0))
      dImpVal = 0
      strDarave = strDAConto
      strDescr = oApp.Tr(Me, 129651366481650390, "Conto 'Enasarco' non indicato 'personalizzazione parcellazione'")
      'PARCELLAZIONE: fattura e/o nota accred: enasarco (tm_altriprev)
      nWriPnTipologia = 0
      bWripnIncassato = False
6300: Return ScriviRigaSpeseVarie(dtrTm, 6300, strDescr, strDarave, lConto, dImporto, dImpVal)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ScriviRiga6350(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strDarave As String

    Try
      lConto = lContoPrimaRiga
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_altriprev), oCldPnfa.TrovaNdec(0))
      dImpVal = 0
      strDarave = strDAControp
      strDescr = ""
      'PARCELLAZIONE: fattura e/o nota accred: cliente per storno enasarco
      nWriPnTipologia = 0
      bWripnIncassato = False
6350: Return ScriviRigaSpeseVarie(dtrTm, 6350, strDescr, strDarave, lConto, dImporto, dImpVal, lConAltriprev)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ScriviRiga6500(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strDarave As String

    Try
      lConto = lConspegen
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_spegen), oCldPnfa.TrovaNdec(0))
      dImpVal = 0
      strDarave = strDAControp
      ' se note di accredito...
      If dImporto < 0 Then
        dImporto = dImporto * -1
        dImpVal = dImpVal * -1
        If strDarave = "D" Then
          strDarave = "A"
        Else
          strDarave = "D"
        End If
      End If
      strDescr = oApp.Tr(Me, 129651317147226562, "Conto 'Spese generali' non indicato in anagrafica ditta -> personalizzazione parcellazione")
      'PARCELLAZIONE: fattura e/o nota accred: le spese generali (tm_spegen)
      nWriPnTipologia = 1
      bWripnIncassato = False
6500: If Not ScriviRigaSpeseVarie(dtrTm, 6500, strDescr, strDarave, lConto, dImporto, dImpVal) Then Return False

      If Not ParcellazAddControp(lConto, dImporto, dImpVal, strDatreg, strDatreg, strDatreg, strDatreg) Then Return False

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
  Public Overridable Function ScriviRiga6600(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strDarave As String

    Try
      lConto = lConcascom
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_nonsoggiva), oCldPnfa.TrovaNdec(0))
      dImpVal = 0
      strDarave = strDAControp
      ' se note di accredito...
      If dImporto < 0 Then
        dImporto = dImporto * -1
        dImpVal = dImpVal * -1
        If strDarave = "D" Then
          strDarave = "A"
        Else
          strDarave = "D"
        End If
      End If
      strDescr = oApp.Tr(Me, 129651319238818359, "Conto 'Cassa commercialisti' non indicato in anagrafica ditta -> personalizzazione parcellazione")
      'PARCELLAZIONE: fattura e/o nota accred: cassa commercialisti (tm_nonsoggiva)
      nWriPnTipologia = 1
      bWripnIncassato = False
6600: If Not ScriviRigaSpeseVarie(dtrTm, 6600, strDescr, strDarave, lConto, dImporto, dImpVal) Then Return False

      If Not ParcellazAddControp(lConto, dImporto, dImpVal, strDatreg, strDatreg, strDatreg, strDatreg) Then Return False

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
  Public Overridable Function ScriviRiga7000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strDarave As String

    Try
      If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "(" Then
        lConto = lAconribo
      Else
        lConto = lConribo
      End If
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_bolli), oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(NTSCDec(dtrTm!tm_bolliv), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      strDarave = strDAControp
      ' bolli in negativo (note di accredito...)
      If dImporto < 0 Then
        dImporto = dImporto * -1
        dImpVal = dImpVal * -1
        If strDarave = "D" Then
          strDarave = "A"
        Else
          strDarave = "D"
        End If
      End If
      strDescr = oApp.Tr(Me, 128771782643350000, _
                              "Conto 'Spese bolli' in 'Personalizzazione acquisti' e/o " & _
                              "'Ricavi da bolli' in 'Personalizzazione vendite' inesistente")
      'FT EMESSA O RICEVUTA: BOLLI
      'RIC FISCALI TUTTI I CASI: BOLLI
      'PARCELLAZIONE: fattura e/o nota accred: bolli
      nWriPnTipologia = 1
      bWripnIncassato = False
7000: If Not ScriviRigaSpeseVarie(dtrTm, 7000, strDescr, strDarave, lConto, dImporto, dImpVal) Then Return False

      If Not ParcellazAddControp(lConto, dImporto, dImpVal, strDatreg, strDatreg, strDatreg, strDatreg) Then Return False

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
  Public Overridable Function ScriviRiga8000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strDarave As String

    Try
      If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "(" Then
        lConto = lAconriin
      Else
        lConto = lConriin
      End If
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_speinc), oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(NTSCDec(dtrTm!tm_speincv), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))

      If dtrTm!tm_scorpo.ToString.ToUpper = "S" AndAlso oApp.oGvar.bSpesePiedeIvateDocScorporo Then
        'Se esiste un codice esenzione in testata lo utilizza per le spese.
        Dim dIva As Decimal = 0
        Dim nCodIva As Integer = nPeveIvainc
        If NTSCStr(dtrTm!tm_tipork) = "L" Or NTSCStr(dtrTm!tm_tipork) = "K" Or NTSCStr(dtrTm!tm_tipork) = "J" Or NTSCStr(dtrTm!tm_tipork) = "(" Then
          'doc ricevuto
          nCodIva = nPeacIvainc
        End If
        If NTSCInt(dtrTm!tm_codese) <> 0 Then nCodIva = NTSCInt(dtrTm!tm_codese)

        'se è settata l'opzione di prendere la contropartita con imponibile più alto nel corpo del doc ...
        'dovrei fare una query su movmag per fare la sum(mm_valore) group by mm_codiva
        'per non rallentare troppo, ciclo su testmag per i vari cod. IVA
        'dagli imponibili dovrei scartare gli importi delle spese di piede (speinc, speacc, speinb)
        'devo prendere solo i cod con aliquota e scartare le esezioni
        If oApp.oGvar.bIvaSpeseIncassoDaImponibMaggiore Then
          nCodIva = oCldPnfa.GetMaxCodivaMovmag(strDittaCorrente, NTSCStr(dtrTm!tm_tipork), NTSCInt(dtrTm!tm_anno), _
                                                NTSCStr(dtrTm!tm_serie), NTSCInt(dtrTm!tm_numdoc))
        End If
        If nCodIva <> 0 Then
          oCldPnfa.Scorporo(dImporto, nCodIva, dImporto, dIva, oCldPnfa.TrovaNdec(0))
        End If
      End If

      strDarave = strDAControp
      strDescr = oApp.Tr(Me, 128771763423460000, _
                              "Conto 'Spese incasso' in 'Personalizzazione acquisti' e/o " & _
                              "'Ricavi spese incasso' in 'Personalizzazione vendite' inesistente")
      'FT EM O RIC: SPESE INCASSO
      'RIC FISC TUTTI I CASI: SPESE INCASSO
      'PARCELLAZIONE: fattura e/o nota accred: spese incasso
      nWriPnTipologia = 1
      bWripnIncassato = False
8000: If Not ScriviRigaSpeseVarie(dtrTm, 8000, strDescr, strDarave, lConto, dImporto, dImpVal) Then Return False

      If Not ParcellazAddControp(lConto, dImporto, dImpVal, strDatreg, strDatreg, strDatreg, strDatreg) Then Return False

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
  Public Overridable Function ScriviRiga9000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strDarave As String

    Try
      If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "(" Then
        lConto = lAconriac
      Else
        lConto = lConriac
      End If

      dImporto = ArrDbl(NTSCDec(dtrTm!tm_speacc), oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(NTSCDec(dtrTm!tm_speaccv), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))

      If dtrTm!tm_scorpo.ToString.ToUpper = "S" AndAlso oApp.oGvar.bSpesePiedeIvateDocScorporo Then
        'Se esiste un codice esenzione in testata lo utilizza per le spese.
        Dim dIva As Decimal = 0
        Dim nCodIva As Integer = nPeveIvainc
        If NTSCStr(dtrTm!tm_tipork) = "L" Or NTSCStr(dtrTm!tm_tipork) = "K" Or NTSCStr(dtrTm!tm_tipork) = "J" Or NTSCStr(dtrTm!tm_tipork) = "(" Then
          'doc ricevuto
          nCodIva = nPeacIvainc
        End If
        If NTSCInt(dtrTm!tm_codese) <> 0 Then nCodIva = NTSCInt(dtrTm!tm_codese)
        If nCodIva <> 0 Then
          oCldPnfa.Scorporo(dImporto, nCodIva, dImporto, dIva, oCldPnfa.TrovaNdec(0))
        End If
      End If

      strDarave = strDAControp
      strDescr = oApp.Tr(Me, 128771761575952000, _
                              "Conto 'Spese accessorie' in 'Personalizzazione acquisti' e/o " & _
                              "'Ricavi spese accessorie' in 'Personalizzazione vendite' inesistente")
      'FT EMESSA O RICEVUTA: SPESE TRASP
      'RIC FISC TUTTI I CASI: SPESE TRASP
9000: Return ScriviRigaSpeseVarie(dtrTm, 9000, strDescr, strDarave, lConto, dImporto, dImpVal)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ScriviRiga10000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strDarave As String

    Try
      If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "(" Then
        lConto = lAconimba
      Else
        lConto = lConimba
      End If
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_speimb), oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(NTSCDec(dtrTm!tm_speimbv), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))

      If dtrTm!tm_scorpo.ToString.ToUpper = "S" AndAlso oApp.oGvar.bSpesePiedeIvateDocScorporo Then
        'Se esiste un codice esenzione in testata lo utilizza per le spese.
        Dim dIva As Decimal = 0
        Dim nCodIva As Integer = nPeveIvainc
        If NTSCStr(dtrTm!tm_tipork) = "L" Or NTSCStr(dtrTm!tm_tipork) = "K" Or NTSCStr(dtrTm!tm_tipork) = "J" Or NTSCStr(dtrTm!tm_tipork) = "(" Then
          'doc ricevuto
          nCodIva = nPeacIvainc
        End If
        If NTSCInt(dtrTm!tm_codese) <> 0 Then nCodIva = NTSCInt(dtrTm!tm_codese)
        If nCodIva <> 0 Then
          oCldPnfa.Scorporo(dImporto, nCodIva, dImporto, dIva, oCldPnfa.TrovaNdec(0))
        End If
      End If

      strDarave = strDAControp
      strDescr = oApp.Tr(Me, 128771760234664000, _
                              "Conto 'Spese imballi' in 'Personalizzazione acquisti' e/o " & _
                              "'Ricavi spese imballi' in 'Personalizzazione vendite' inesistente")
      'FT EMESSA O RICEVUTA: SPESE IMBALLO
      'RIC FISC TUTTI I CASI: SPESE IMBALLO
10000: Return ScriviRigaSpeseVarie(dtrTm, 10000, strDescr, strDarave, lConto, dImporto, dImpVal)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function ScriviRiga11000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      ' ecc. r.f. servizi non incassati (registra in iva ma non su castelletto)
      If bModuliAcquistati = False Then Return True
      lRiga = lRiga + 1
      lConto = lConivrfp
      strDescr = " "
      dTotiva = NTSCDec(dtrTm!tm_imposta_1) + NTSCDec(dtrTm!tm_imposta_2) + NTSCDec(dtrTm!tm_imposta_3) + NTSCDec(dtrTm!tm_imposta_4) + NTSCDec(dtrTm!tm_imposta_5) + NTSCDec(dtrTm!tm_imposta_6) + NTSCDec(dtrTm!tm_imposta_7) + NTSCDec(dtrTm!tm_imposta_8)
      dTotivav = NTSCDec(dtrTm!tm_impostav_1) + NTSCDec(dtrTm!tm_impostav_2) + NTSCDec(dtrTm!tm_impostav_3) + NTSCDec(dtrTm!tm_impostav_4) + NTSCDec(dtrTm!tm_impostav_5) + NTSCDec(dtrTm!tm_impostav_6) + NTSCDec(dtrTm!tm_impostav_7) + NTSCDec(dtrTm!tm_impostav_8)
      dImporto = ArrDbl(dTotiva, oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(dTotivav, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      strDarave = "A"
      lControp = NTSCInt(dtrTm!tm_conto)
      dImponib = 0
      dImponibval = 0
      lNumprot = 0
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0
      lRigaiva = 0

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128771807371500000, "(11000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di 'personalizzazione vendite' IVA da ric. fisc non incassate " & _
                        "per prestazioni di servizi inesistente"), True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128771807352000000, "(11000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      'RIC FISC PREST SERV A PRIVATO E NON 'NON INCASSATE': TABPEVE - IVA DA RIC FISC NON INCASS PREST SERV
11000: bOk = Wripn(11000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
               dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
               strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", " ", lRigaiva)

      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128771807331252000, "(11000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(11000, dtrTm, lNumreg, lRiga, "2", 0, Nothing, 0) Then
          LogWrite(oApp.Tr(Me, 129278283453955078, "(11000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
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
  Public Overridable Function ScriviRiga12000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      ' se riemissione e cessione di beni , chiude il credito e no castelletto iva
      If bModuliAcquistati = False Then Return True

      lRiga = lRiga + 1
      lConto = lConcrrfc
      strDescr = " "
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_totdoc), oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(NTSCDec(dtrTm!tm_totdocv), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      strDarave = "A"
      lControp = NTSCInt(dtrTm!tm_conto)
      dImponib = 0
      dImponibval = 0
      lNumprot = 0
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0
      lRigaiva = 0

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128771811110266000, "(12000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di 'personalizzazione vendite' Credito da ric. fisc non incassate " & _
                        "cessione di beni inesistente"), True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128771811118846000, "(12000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      'RIM RIC FISC A PRIVATO E NON CESS BENI:  STORNO TABPEVE - CRED DA RIC FISC NON INC CESS BENI
12000: bOk = Wripn(12000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
            dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
            strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", " ", lRigaiva)

      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128771811136318000, "(12000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(12000, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
          LogWrite(oApp.Tr(Me, 129278283874101562, "(12000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
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
  Public Overridable Function ScriviRiga13000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      If bModuliAcquistati = False Then Return True

      lRiga = lRiga + 1
      lConto = lConivrfp
      strDescr = " "
      dTotiva = NTSCDec(dtrTm!tm_imposta_1) + NTSCDec(dtrTm!tm_imposta_2) + NTSCDec(dtrTm!tm_imposta_3) + NTSCDec(dtrTm!tm_imposta_4) + NTSCDec(dtrTm!tm_imposta_5) + NTSCDec(dtrTm!tm_imposta_6) + NTSCDec(dtrTm!tm_imposta_7) + NTSCDec(dtrTm!tm_imposta_8)
      dTotivav = NTSCDec(dtrTm!tm_impostav_1) + NTSCDec(dtrTm!tm_impostav_2) + NTSCDec(dtrTm!tm_impostav_3) + NTSCDec(dtrTm!tm_impostav_4) + NTSCDec(dtrTm!tm_impostav_5) + NTSCDec(dtrTm!tm_impostav_6) + NTSCDec(dtrTm!tm_impostav_7) + NTSCDec(dtrTm!tm_impostav_8)
      dImporto = ArrDbl(dTotiva, oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(dTotivav, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      strDarave = "D"
      lControp = NTSCInt(dtrTm!tm_conto)
      dImponib = 0
      dImponibval = 0
      lNumprot = 0
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0
      lRigaiva = 0

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128771816322382000, "(13000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di 'personalizzazione vendite' IVA da ric. fisc non incassate " & _
                        "prestazioni di servizi inesistente"), True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128771816300542000, "(13000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      'RIM RIC FISC A PRIVATO PREST SERV:  STORNO TABPEVE - IVA DA RIC FISC NON INC PREST SERV
13000: bOk = Wripn(13000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
      dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
      strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", " ", lRigaiva)

      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128771816283850000, "(13000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(13000, dtrTm, lNumreg, lRiga, "2", 0, Nothing, 0) Then
          LogWrite(oApp.Tr(Me, 129278284093378906, "(13000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
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
  Public Overridable Function ScriviRiga14000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      If bModuliAcquistati = False Then Return True

      lRiga = lRiga + 1
      lConto = lConcrrfp
      strDescr = " "
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_totdoc), oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(NTSCDec(dtrTm!tm_totdocv), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      strDarave = "A"
      lControp = NTSCInt(dtrTm!tm_conto)
      dImponib = 0
      dImponibval = 0
      lNumprot = 0
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0
      lRigaiva = 0

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128771816225974000, "(14000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di 'personalizzazione vendite' Credito da ric. fisc non incassate " & _
                        "prestazioni di servizi inesistente"), True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128771816247190000, "(14000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      'RIM RIC FISC A PRIVATO E NON PREST SERV:  STORNO TABPEVE - CRED DA RIC FISC NON INC PREST SERV
14000: bOk = Wripn(14000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
        dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
        strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", " ", lRigaiva)

      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128771816206786000, "(14000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(14000, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
          LogWrite(oApp.Tr(Me, 129278284321455078, "(14000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
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
  Public Overridable Function ScriviRiga15000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      'se c'è iva indeducibile (ripartita su tutte le righe di costo)
      'aggiungo una riga cumulativa per l'importo dell'indeducibilità
      If bModuliAcquistati = False Then Return True

      lRiga = lRiga + 1
      lConto = lContoiva
      strDescr = " "
      dImporto = dIvaInded
      dImpVal = dIvaIndedVal
      If strDAControp = "D" Then strDarave = "A" Else strDarave = "D"
      lControp = 0
      dImponib = 0
      dImponibval = 0
      lNumprot = lNewnumprot ' ### per i fornitori... (ci vuole)
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0
      lRigaiva = 0

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128772004165356000, "(15000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto IVA 1 (o conto asssociato alla contropartita IVA 1) " & _
                        "indicato nella causale contabile |" & nCausale.ToString & "| inesistente"), True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128771816147350000, "(15000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      'RIC FATT CON IVA INDED E opz di reg bGirocontoIvaIndedRipartito = -1: CONTO IVA PER RIGA DELL'IVA INDED
15000: bOk = Wripn(15000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
        dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
        strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", strAlfpro, lRigaiva)

      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128771816130034000, "(15000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(15000, dtrTm, lNumreg, lRiga, "2", 0, Nothing, 0) Then
          LogWrite(oApp.Tr(Me, 129278284519375000, "(15000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
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
  Public Overridable Function ScriviRiga15100(ByRef dtrTm As DataRow) As Boolean
    'nota accred EMESSA storno fattura: 
    'la riga del cliente nota accred che storna la prima riga di questa registazione ed azzera le scadenze
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim dImponib As Decimal = 0
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      If bModuliAcquistati = False Then Return True

      If (NTSCInt(dtrTm!tm_conto) <> lContoPrimaRiga) Then
        lConto = lContoPrimaRiga
      Else
        lConto = NTSCInt(dtrTm!tm_conto)
      End If
      lRiga = lRiga + 1
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_totdoc), oCldPnfa.TrovaNdec(0))
      strDescr = " "
      strDarave = "D"
      dImponib = 0
      lNumprot = 0
      nNregiva = 0
      nCodIva = 0
      If (CBool((ModuliDittaDitt(strDittaCorrente) And bsModCG)) Or CBool((ModuliExtDittaDitt(strDittaCorrente) And bsModExtCGE)) Or bForzaCG) And dImporto <> 0 Then
        oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
        If dttTmp.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 129201418934853516, "(15100) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "Conto intestatario del documento inesistente"), True)
          Return False
        Else
          If lConto <> NTSCInt(dtrTm!tm_conto) Then
            If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
              LogWrite(oApp.Tr(Me, 129201418953964844, "(15100) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
              Return False
            End If
          End If
        End If
        'NOTA ACCRED EMESSA: storno il conto intestatario del documento per compensarlo con la fattura
15100:  bOk = Wripn(15100, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
                " ", nCodIva, dImponib, lNumprot, 0, 0, 0, "", "", " ", lRigaiva)

        If Not bOk Then
          LogWrite(oApp.Tr(Me, 129201419572753907, "(15100) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|."), True)
          Return False
        End If

        If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
          If Not WriPriana2(15100, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
            LogWrite(oApp.Tr(Me, 129278284792734375, "(15100) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
            Return False
          End If
        End If

      End If ' se c'è CG

      'storna le scadenze precedentemente calcolate sulla riga 1000
      ''MEGLIO: cancello le scadenze: tanto la nota accred deve avere importo <= scadenze aperte della fattura , 
      ''altrimenti non può essere collegata ... 
      ''se nota accred storna fattura, le scadenze della nota accred non devono esistere
      'dttSC.Clear()
      'dttSC.AcceptChanges()
      For Each dtrT As DataRow In dttSC.Select("sc_datreg = " & CDataSQL(NTSCDate(strDatreg)) & _
                                               " AND sc_numreg = " & lNumreg)
        dtrT!sc_flsaldato = "S"
        dtrT!sc_dtsaldato = NTSCDate(strDatreg)
        dtrT!sc_rgsaldato = lNumreg
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
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function ScriviRiga15300(ByRef dtrTm As DataRow, ByRef dttScadFt As DataTable) As Boolean
    'nota accred EMESSA storno fattura: 
    'la riga del cliente 'FATTURA EMESSA' stornata da questa nota accredito
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim dImponib As Decimal = 0
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim dResiduo As Decimal = 0
    Dim i As Integer = 0

    Try
      If bModuliAcquistati = False Then Return True

      If (NTSCInt(dtrTm!tm_conto) <> lContoPrimaRiga) Then
        lConto = lContoPrimaRiga
      Else
        lConto = NTSCInt(dtrTm!tm_conto)
      End If
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_totdoc), oCldPnfa.TrovaNdec(0))
      lRiga = lRiga + 1
      strDescr = " "
      strDarave = "A"
      dImponib = 0
      lNumprot = 0
      nNregiva = 0
      nCodIva = 0
      If (CBool((ModuliDittaDitt(strDittaCorrente) And bsModCG)) Or CBool((ModuliExtDittaDitt(strDittaCorrente) And bsModExtCGE)) Or bForzaCG) And dImporto <> 0 Then
        oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
        If dttTmp.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 129203688613390859, "(15300) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "Conto intestatario del documento inesistente"), True)
          Return False
        Else
          If lConto <> NTSCInt(dtrTm!tm_conto) Then
            If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
              LogWrite(oApp.Tr(Me, 129203688636672109, "(15300) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
              Return False
            End If
          End If
        End If
        'NOTA ACCRED EMESSA: storno il conto intestatario del documento per compensarlo con la fattura
15300:  bOk = Wripn(15300, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
                " ", nCodIva, dImponib, lNumprot, 0, 0, 0, "", "", " ", lRigaiva)

        If Not bOk Then
          LogWrite(oApp.Tr(Me, 129203695597863515, "(15300) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|."), True)
          Return False
        End If

        If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
          If Not WriPriana2(15300, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
            LogWrite(oApp.Tr(Me, 129278285185683593, "(15300) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
            Return False
          End If
        End If

        'cambio sulla riga appena inserita gli estremi della partita: nella wripn è stata messa la partita della nota accred,
        'ora gli metto la partita della fattura . il rowstate è sempre 'added'
        With dttPN.Rows(dttPN.Rows.Count - 1)
          !pn_annpar = NTSCInt(dtrTm!tm_annpar)
          !pn_alfpar = NTSCStr(dtrTm!tm_alfpar)
          !pn_numpar = NTSCInt(dtrTm!tm_numpar)
        End With

      End If ' se c'è CG

      '---------------------
      'devo mettere 'saldato' sulle scadenze della fattura ...
      'suciramente se sono qui l'importo delle scadenze supera o è uguale a quello della nota di accred.
      'se serve devo fare anche uno 'spezza scadenze'
      'per le righe che devo aggiornare, imposto sul datatable 'modified'
      'se 'spezza scadenze' le nuove righe saranno 'added'
      dtrT = dttScadFt.Select("", "sc_datsca, sc_numrata")
      i = -1
      dResiduo = ArrDbl(NTSCDec(dtrTm!tm_totdoc), oCldPnfa.TrovaNdec(0))
      While dResiduo > 0
        i += 1
        If NTSCDec(dtrT(i)!sc_importo) <= dResiduo Then
          dtrT(i)!sc_flsaldato = "S"
          dtrT(i)!sc_dtsaldato = NTSCDate(strDatreg)
          dtrT(i)!sc_rgsaldato = lNumreg
          dResiduo -= NTSCDec(dtrT(i)!sc_importo)
          dttSC.ImportRow(dtrT(i))
          If dttSC.Rows(dttSC.Rows.Count - 1).RowState <> DataRowState.Modified Then
            dttSC.Rows(dttSC.Rows.Count - 1).SetModified()
          End If
        Else
          'scadenza più grande: devo spezzare
          dttScadFt.Rows.Add(dttScadFt.NewRow)
          For Each col As DataColumn In dttScadFt.Columns
            dttScadFt.Rows(dttScadFt.Rows.Count - 1)(col.ColumnName) = dtrT(i)(col.ColumnName)
          Next
          'sulla nuova scad correggo rata ed importo, sulla vecchia scad correggo solo importo e flag saldato
          'chiudo la rata vecchia, in questo modo se devo ricontabilizzare, ordinando per data scad e num rata riesco a risaldare la stessa rata della rpima contabilizzazione !!!
          With dttScadFt.Rows(dttScadFt.Rows.Count - 1)
            !sc_numrata = 1 + oCldPnfa.GetScadenNumMaxRata(strDittaCorrente, NTSCInt(dtrT(i)!sc_conto), NTSCInt(dtrT(i)!sc_annpar), _
                                                       NTSCStr(dtrT(i)!sc_alfpar), NTSCInt(dtrT(i)!sc_numpar), NTSCStr(dtrT(i)!sc_integr))
            !sc_importo = NTSCDec(dtrT(i)!sc_importo) - dResiduo
            !sc_importoda = NTSCDec(dtrT(i)!sc_importo) - dResiduo
          End With

          dtrT(i)!sc_importo = dResiduo
          dtrT(i)!sc_importoda = dResiduo
          dtrT(i)!sc_flsaldato = "S"
          dtrT(i)!sc_dtsaldato = NTSCDate(strDatreg)
          dtrT(i)!sc_rgsaldato = lNumreg

          dResiduo = 0
          dttSC.ImportRow(dttScadFt.Rows(dttScadFt.Rows.Count - 1))
          If dttSC.Rows(dttSC.Rows.Count - 1).RowState <> DataRowState.Added Then
            dttSC.Rows(dttSC.Rows.Count - 1).SetAdded()
          End If
          dttSC.ImportRow(dtrT(i))
          If dttSC.Rows(dttSC.Rows.Count - 1).RowState <> DataRowState.Modified Then
            dttSC.Rows(dttSC.Rows.Count - 1).SetModified()
          End If
        End If    'If NTSCDec(dtrT(i)!sc_importo) <= dResiduo Then
      End While     'While dResiduo > 0

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

  'iva
  Public Overridable Function ScriviRiga16000(ByRef dtrTm As DataRow, ByVal i As Integer) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable
    Dim dRapp1 As Decimal = 0

    Try
      If bModuliAcquistati = False Then Return True

      lRiga = lRiga + 1
      lConto = lContoiva
      strDescr = " "
      dImporto = ArrDbl(NTSCDec(dtrTm("tm_imposta_" & i.ToString)), oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(NTSCDec(dtrTm("tm_impostav_" & i.ToString)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      dImponib = ArrDbl(NTSCDec(dtrTm("tm_imponib_" & i.ToString)), oCldPnfa.TrovaNdec(0))
      dImponibval = ArrDbl(NTSCDec(dtrTm("tm_imponibv_" & i.ToString)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      ' eccezione ric. fiscali servizi parz. incassate
      If dtrTm!tm_tipork.ToString = "F" And bParzinc And strPrestserv = "S" Then
        dRapp1 = dImpincassato / (dImpnotincassato + dImpincassato) ' rapp. incassato
        dTotiva = NTSCDec(dtrTm!tm_imposta_1) + NTSCDec(dtrTm!tm_imposta_2) + NTSCDec(dtrTm!tm_imposta_3) + NTSCDec(dtrTm!tm_imposta_4) + NTSCDec(dtrTm!tm_imposta_5) + NTSCDec(dtrTm!tm_imposta_6) + NTSCDec(dtrTm!tm_imposta_7) + NTSCDec(dtrTm!tm_imposta_8)
        dTotivav = NTSCDec(dtrTm!tm_impostav_1) + NTSCDec(dtrTm!tm_impostav_2) + NTSCDec(dtrTm!tm_impostav_3) + NTSCDec(dtrTm!tm_impostav_4) + NTSCDec(dtrTm!tm_impostav_5) + NTSCDec(dtrTm!tm_impostav_6) + NTSCDec(dtrTm!tm_impostav_7) + NTSCDec(dtrTm!tm_impostav_8)
        dImporto = ArrDbl(NTSCDec(dtrTm("tm_imposta_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(0))
        dImpVal = ArrDbl(NTSCDec(dtrTm("tm_impostav_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
        dImponib = ArrDbl(NTSCDec(dtrTm("tm_imponib_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(0))
        dImponibval = ArrDbl(NTSCDec(dtrTm("tm_imponibv_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      End If
      ' fine ecc. r.f.
      strDarave = strDAIva
      lControp = NTSCInt(dtrTm!tm_conto)
      lNumprot = lNewnumprot ' ### per i fornitori... (ci vuole)
      nNregiva = nMemnregiva
      strTregiva = strMemtregiva
      nCodIva = NTSCInt(dtrTm("tm_codiva_" & i.ToString))
      lRigaivanew = lRigaivanew + 1

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128771816167474000, "(16000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto IVA 1 (o conto asssociato alla contropartita IVA 1) " & _
                        "indicato nella causale contabile |" & nCausale.ToString & "| inesistente"), True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128771950522896000, "(16000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito  a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      'FT EMESSA O RICEVUTA: IVA
      'RIC FISC TUTTI I CASI: IVA
      'RIM RIC FISC A PRIVATO E NON PREST SERV: IVA
      'PARCELLAZIONE: FATTURE E/O NOTE ACCRED: IVA
      nWriPnTipologia = 0
      bWripnIncassato = False
16000: bOk = Wripn(16000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
             dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, _
             strDarave, nNregiva, strTregiva, nCodIva, dImponib, lNumprot, dImpVal, _
             dImponibval, lControp, "", "", strAlfpro, lRigaivanew)
      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128772004072484000, "(16000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(16000, dtrTm, lNumreg, lRiga, "2", 0, Nothing, 0) Then
          LogWrite(oApp.Tr(Me, 129278285852714843, "(16000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
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
  Public Overridable Function ScriviRiga17000(ByRef dtrTm As DataRow, ByVal i As Integer, ByVal bRegIva2 As Boolean) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Dim dRapp1 As Decimal = 0

    Try
      If bModuliAcquistati = False Then Return True

      'PRIMA RIGA DI IVA INDEDUCIBILE
      lRiga = lRiga + 1
      If bRegIva2 = False Then
        strDarave = strDAIva
        lControp = lContoiva
      Else
        If strDAIva = "D" Then
          strDarave = "A"
        Else
          strDarave = "D"
        End If
        lControp = lContoiva2
      End If
      dImporto = ArrDbl(NTSCDec(dtrTm("tm_imposta_" & i.ToString)), oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(NTSCDec(dtrTm("tm_impostav_" & i.ToString)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      dImponib = ArrDbl(NTSCDec(dtrTm("tm_imponib_" & i.ToString)), oCldPnfa.TrovaNdec(0))
      dImponibval = ArrDbl(NTSCDec(dtrTm("tm_imponibv_" & i.ToString)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      ' eccezione ric. fiscali servizi parz. incassate
      If dtrTm!tm_tipork.ToString = "F" And bParzinc And strPrestserv = "S" Then
        dRapp1 = dImpincassato / (dImpnotincassato + dImpincassato) ' rapp. incassato
        dTotiva = NTSCDec(dtrTm!tm_imposta_1) + NTSCDec(dtrTm!tm_imposta_2) + NTSCDec(dtrTm!tm_imposta_3) + NTSCDec(dtrTm!tm_imposta_4) + NTSCDec(dtrTm!tm_imposta_5) + NTSCDec(dtrTm!tm_imposta_6) + NTSCDec(dtrTm!tm_imposta_7) + NTSCDec(dtrTm!tm_imposta_8)
        dTotivav = NTSCDec(dtrTm!tm_impostav_1) + NTSCDec(dtrTm!tm_impostav_2) + NTSCDec(dtrTm!tm_impostav_3) + NTSCDec(dtrTm!tm_impostav_4) + NTSCDec(dtrTm!tm_impostav_5) + NTSCDec(dtrTm!tm_impostav_6) + NTSCDec(dtrTm!tm_impostav_7) + NTSCDec(dtrTm!tm_impostav_8)
        dImporto = ArrDbl(NTSCDec(dtrTm("tm_imposta_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(0))
        dImpVal = ArrDbl(NTSCDec(dtrTm("tm_impostav_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
        dImponib = ArrDbl(NTSCDec(dtrTm("tm_imponib_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(0))
        dImponibval = ArrDbl(NTSCDec(dtrTm("tm_imponibv_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      End If
      ' fine ecc. r.f.

      lConto = NTSCInt(dttControp.Rows(1)!lControp)
      oCldPnfa.ValCodiceDb(lConto.ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)

      If dttTmp.Rows.Count = 0 Then
        'è stata cancellata la contropartita dopo averla usata nel documento...
        LogWrite(oApp.Tr(Me, 128771955670834000, "(17000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "E' indicata una contropartita contabile (|" & lConto.ToString & _
                        "|) non presente nella tabella delle contropartite ditta."), True)
        Return False
      End If
      lConto = NTSCInt(dttTmp.Rows(0)!tb_concove)
      strDescr = " "
      dImponib = 0
      strTregiva = " "
      nCodIva = 0
      nNregiva = 0
      lNumprot = lNewnumprot
      lRigaiva = 0

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        If bRegIva2 = False Then
          LogWrite(oApp.Tr(Me, 128771954856826000, "(17000) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "Conto IVA 1 (o conto asssociato alla contropartita IVA 1) " & _
                          "indicato nella causale contabile |" & nCausale.ToString & "| inesistente"), True)
        Else
          LogWrite(oApp.Tr(Me, 128771971344622000, "(17000) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "Conto IVA 2 (o conto asssociato alla contropartita IVA 2) " & _
                          "indicato nella causale contabile |" & nCausale.ToString & "| inesistente"), True)
        End If
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128771954837170000, "(17000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      GetTabcivaRow(NTSCInt(dtrTm("tm_codiva_" & i.ToString)), dttTmp1)
      If bRegIva2 = False Then
        '17000
        'FT RICEVUTA CON IVA TOTALM O PARZIALM DEDUC e opz di reg bGirocontoIvaIndedRipartito = 0: PRIMA RIGA DI COSTO SU CUI ASSEGNARE IVA
      Else
        '20000
        'RIC FATT INTRA CON REG 2 ACQ E IVA PARZ DEDUC e opz di reg bGirocontoIvaIndedRipartito = 0: PRIMA RIGA DI COSTO PER IVA INDED
      End If
17000: bOk = Wripn(17000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                  dttTmp.Rows(0)!an_scaden.ToString, strDescr, 9999, _
                  ArrDblEcc((dImporto * CalcolaIvaIndetr(strCautregiva, NTSCDec(dttTmp1.Rows(0)!tb_inded)) / 100), oCldPnfa.TrovaNdec(0)), _
                  strDarave, nNregiva, strTregiva, nCodIva, dImponib, lNumprot, _
                  ArrDblEcc((dImpVal * CalcolaIvaIndetr(strCautregiva, NTSCDec(dttTmp1.Rows(0)!tb_inded)) / 100), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta))), _
                  dImponibval, lControp, "", "", strAlfpro, lRigaiva)
      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128771957191054000, "(17000) Operazione interrotta inserendo la prima riga relativa allo storno IVA indeducibile " & _
                        "del documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(17000, dtrTm, lNumreg, lRiga, "0", NTSCInt(dttControp.Rows(1)!lControp), _
                          dttControp.Rows(1), ArrDblEcc((dImporto * CalcolaIvaIndetr(strCautregiva, NTSCDec(dttTmp1.Rows(0)!tb_inded)) / 100), _
                          oCldPnfa.TrovaNdec(0))) Then
          LogWrite(oApp.Tr(Me, 129278286191035156, "(17000) Operazione interrotta elaborando la Contabilità analitica inserendo la prima riga relativa allo storno IVA indeducibile del documento |" & strEstremidoc & "|."), True)
          Return False
        End If
      End If

      'EVENTUALE RIGA DI CONTABILITA' ANALITICA
      If bCa Then
        bOk = WriPriana(dtrTm, lConto, strDarave, NTSCDec(dttControp.Rows(1)!dImporto), _
                        NTSCInt(dttControp.Rows(1)!lControp), NTSCDec(dttControp.Rows(1)!dImpVal), _
                        ArrDblEcc((dImporto * CalcolaIvaIndetr(strCautregiva, NTSCDec(dttTmp1.Rows(0)!tb_inded)) / 100), oCldPnfa.TrovaNdec(0)), _
                        ArrDblEcc((dImpVal * CalcolaIvaIndetr(strCautregiva, NTSCDec(dttTmp1.Rows(0)!tb_inded)) / 100), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta))), _
                        NTSCDate(dttControp.Rows(1)!strDtIniz).ToShortDateString, _
                        NTSCDate(dttControp.Rows(1)!strDtFin).ToShortDateString, True, _
                        dttPN.Rows(dttPN.Rows.Count - 1), Nothing)
        If Not bOk Then
          LogWrite(oApp.Tr(Me, 128771960330086000, "(17000) Operazione interrotta inserendo i riferimenti di C.A. relativi alla prima riga relativa allo storno IVA indeducibile " & _
                          "del documento |" & strEstremidoc & "|."), True)
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
      dttTmp1.Clear()
    End Try
  End Function
  Public Overridable Function ScriviRiga18000(ByRef dtrTm As DataRow, ByVal i As Integer, ByVal bRegIva2 As Boolean) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable
    Dim dttTmp1 As New DataTable
    Dim dRapp1 As Decimal = 0

    Try
      If bModuliAcquistati = False Then Return True

      lRiga = lRiga + 1
      If bRegIva2 = False Then
        If strDAIva = "D" Then strDarave = "A" Else strDarave = "D"
        lConto = lContoiva
      Else
        strDarave = strDAIva
        lConto = lContoiva2
      End If
      strDescr = " "
      dImponib = 0
      dImporto = ArrDbl(NTSCDec(dtrTm("tm_imposta_" & i.ToString)), oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(NTSCDec(dtrTm("tm_impostav_" & i.ToString)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      dImponib = ArrDbl(NTSCDec(dtrTm("tm_imponib_" & i.ToString)), oCldPnfa.TrovaNdec(0))
      dImponibval = ArrDbl(NTSCDec(dtrTm("tm_imponibv_" & i.ToString)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      ' eccezione ric. fiscali servizi parz. incassate
      If dtrTm!tm_tipork.ToString = "F" And bParzinc And strPrestserv = "S" Then
        dRapp1 = dImpincassato / (dImpnotincassato + dImpincassato) ' rapp. incassato
        dTotiva = NTSCDec(dtrTm!tm_imposta_1) + NTSCDec(dtrTm!tm_imposta_2) + NTSCDec(dtrTm!tm_imposta_3) + NTSCDec(dtrTm!tm_imposta_4) + NTSCDec(dtrTm!tm_imposta_5) + NTSCDec(dtrTm!tm_imposta_6) + NTSCDec(dtrTm!tm_imposta_7) + NTSCDec(dtrTm!tm_imposta_8)
        dTotivav = NTSCDec(dtrTm!tm_impostav_1) + NTSCDec(dtrTm!tm_impostav_2) + NTSCDec(dtrTm!tm_impostav_3) + NTSCDec(dtrTm!tm_impostav_4) + NTSCDec(dtrTm!tm_impostav_5) + NTSCDec(dtrTm!tm_impostav_6) + NTSCDec(dtrTm!tm_impostav_7) + NTSCDec(dtrTm!tm_impostav_8)
        dImporto = ArrDbl(NTSCDec(dtrTm("tm_imposta_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(0))
        dImpVal = ArrDbl(NTSCDec(dtrTm("tm_impostav_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
        dImponib = ArrDbl(NTSCDec(dtrTm("tm_imponib_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(0))
        dImponibval = ArrDbl(NTSCDec(dtrTm("tm_imponibv_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      End If
      ' fine ecc. r.f.
      strTregiva = " "
      nCodIva = 0
      If strPrgParent = "BNPAPNPA" Then
        lControp = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dttControp.Rows(1)!lControp.ToString))
      Else
        oCldPnfa.ValCodiceDb(dttControp.Rows(1)!lControp.ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
        lControp = NTSCInt(dttTmp.Rows(0)!tb_concove)
        dttTmp.Clear()
      End If
      nNregiva = 0
      lNumprot = lNewnumprot
      lRigaiva = 0
      dttTmp.Clear()

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        If bRegIva2 = False Then
          LogWrite(oApp.Tr(Me, 128771966389282000, "(18000) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "Conto IVA 1 (o conto asssociato alla contropartita IVA 1) " & _
                          "indicato nella causale contabile |" & nCausale.ToString & "| inesistente"), True)
        Else
          LogWrite(oApp.Tr(Me, 128771973679474000, "(18000) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "Conto IVA 2 (o conto asssociato alla contropartita IVA 2) " & _
                          "indicato nella causale contabile |" & nCausale.ToString & "| inesistente"), True)
        End If
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128771966454490000, "(18000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito  a partite/scadenze"), True)
            Return False
          End If
        End If
      End If
      GetTabcivaRow(NTSCInt(dtrTm("tm_codiva_" & i.ToString)), dttTmp1)

      If bRegIva2 = False Then
        '18000
        'FT RICEVUTA CON IVA TOTALM O PARZIALM INDED e opz di reg bGirocontoIvaIndedRipartito = 0: IVA NON DEDUCIBILE
      Else
        '21000
        'RIC FATT INTRA CON REG 2 ACQ E IVA PARZ DEDUC e opz di reg bGirocontoIvaIndedRipartito = 0: IVA INDED
      End If
18000: bOk = Wripn(18000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                   dttTmp.Rows(0)!an_scaden.ToString, strDescr, 9999, _
                   ArrDblEcc((dImporto * CalcolaIvaIndetr(strCautregiva, NTSCDec(dttTmp1.Rows(0)!tb_inded)) / 100), oCldPnfa.TrovaNdec(0)), _
                   strDarave, nNregiva, strTregiva, nCodIva, dImponib, lNumprot, _
                   ArrDblEcc((dImpVal * CalcolaIvaIndetr(strCautregiva, NTSCDec(dttTmp1.Rows(0)!tb_inded)) / 100), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta))), _
                   dImponibval, lControp, "", "", strAlfpro, lRigaiva)
      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128772004089020000, "(18000) Operazione interrotta inserendo la seconda riga " & _
                        "relativa allo storno IVA indeducibile nel documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(18000, dtrTm, lNumreg, lRiga, "2", 0, Nothing, _
                          ArrDblEcc((dImporto * CalcolaIvaIndetr(strCautregiva, NTSCDec(dttTmp1.Rows(0)!tb_inded)) / 100), _
                          oCldPnfa.TrovaNdec(0))) Then
          LogWrite(oApp.Tr(Me, 129278286746708984, "(18000) Operazione interrotta elaborando la Contabilità analitica inserendo la seconda riga relativa allo storno IVA indeducibile del documento |" & strEstremidoc & "|."), True)
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
      dttTmp1.Clear()
    End Try
  End Function
  Public Overridable Function ScriviRiga19000(ByRef dtrTm As DataRow, ByVal i As Integer) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable
    Dim dRapp1 As Decimal = 0

    Try
      If bModuliAcquistati = False Then Return True

      lRiga = lRiga + 1
      lConto = lContoiva2
      dImporto = ArrDbl(NTSCDec(dtrTm("tm_imposta_" & i.ToString)), oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(NTSCDec(dtrTm("tm_impostav_" & i.ToString)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      dImponib = ArrDbl(NTSCDec(dtrTm("tm_imponib_" & i.ToString)), oCldPnfa.TrovaNdec(0))
      dImponibval = ArrDbl(NTSCDec(dtrTm("tm_imponibv_" & i.ToString)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      ' eccezione ric. fiscali servizi parz. incassate
      If dtrTm!tm_tipork.ToString = "F" And bParzinc And strPrestserv = "S" Then
        dRapp1 = dImpincassato / (dImpnotincassato + dImpincassato) ' rapp. incassato
        dTotiva = NTSCDec(dtrTm!tm_imposta_1) + NTSCDec(dtrTm!tm_imposta_2) + NTSCDec(dtrTm!tm_imposta_3) + NTSCDec(dtrTm!tm_imposta_4) + NTSCDec(dtrTm!tm_imposta_5) + NTSCDec(dtrTm!tm_imposta_6) + NTSCDec(dtrTm!tm_imposta_7) + NTSCDec(dtrTm!tm_imposta_8)
        dTotivav = NTSCDec(dtrTm!tm_impostav_1) + NTSCDec(dtrTm!tm_impostav_2) + NTSCDec(dtrTm!tm_impostav_3) + NTSCDec(dtrTm!tm_impostav_4) + NTSCDec(dtrTm!tm_impostav_5) + NTSCDec(dtrTm!tm_impostav_6) + NTSCDec(dtrTm!tm_impostav_7) + NTSCDec(dtrTm!tm_impostav_8)
        dImporto = ArrDbl(NTSCDec(dtrTm("tm_imposta_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(0))
        dImpVal = ArrDbl(NTSCDec(dtrTm("tm_impostav_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
        dImponib = ArrDbl(NTSCDec(dtrTm("tm_imponib_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(0))
        dImponibval = ArrDbl(NTSCDec(dtrTm("tm_imponibv_" & i.ToString)) * dRapp1, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      End If
      ' fine ecc. r.f.
      If strDAIva = "D" Then strDarave = "A" Else strDarave = "D"
      lControp = NTSCInt(dtrTm!tm_conto)
      strTregiva = strCautregiva2
      nNregiva = nCaunumregi2
      lNumprot = lNewnumprot2
      lRigaivanew = lRigaivanew + 1

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128771966880838000, "(19000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto IVA 2 (o conto asssociato alla contropartita IVA 2) " & _
                        "indicato nella causale contabile |" & nCausale.ToString & "| inesistente"), True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128771966948854000, "(19000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      'RIC FATT INTRA: RIGA IVA SU REGISTRO 2
19000: bOk = Wripn(19000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
              dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, _
              nNregiva, strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, _
              "", "", strAlfpro2, lRigaivanew)

      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128772004106960000, "(19000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(19000, dtrTm, lNumreg, lRiga, "2", 0, Nothing, 0) Then
          LogWrite(oApp.Tr(Me, 129278287039501953, "(19000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
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
  Public Overridable Function ScriviRiga22000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      If bModuliAcquistati = False Then Return True

      lConto = NTSCInt(dtrTm!tm_conto)
      ' eccezione per ric. fiscali ...
      If lConto <> lContoPrimaRiga Then lConto = lContoPrimaRiga
      strDescr = " "
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_totomag), oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(NTSCDec(dtrTm!tm_totomagv), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      strDarave = strDAContoom
      If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "(" Then
        lControp = lAconomag
      Else
        lControp = lConomag
      End If
      dImponib = 0
      dImponibval = 0
      lNumprot = 0
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128771985784022000, "(22000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto intestatario del documento inesistente"), True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128771985799310000, "(22000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      'FT EM: CLIENTE PER OMAGGI
      'RIC FISC A CLI NO PRIV CESS BENI (O PREST SERV): CLIENTE   PER OMAGGI
22000: bOk = Wripn(22000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
               dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
               strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", " ", lRigaiva)
      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128771986333610000, "(22000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(22000, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
          LogWrite(oApp.Tr(Me, 129278287396220703, "(22000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
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
  Public Overridable Function ScriviRiga23000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      If bModuliAcquistati = False Then Return True

      lRiga = lRiga + 1
      If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "(" Then
        lConto = lAconomag
      Else
        lConto = lConomag
      End If
      strDescr = " "
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_totomag), oCldPnfa.TrovaNdec(0))
      dImpVal = ArrDbl(NTSCDec(dtrTm!tm_totomagv), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      strDarave = strDACassainc
      lControp = NTSCInt(dtrTm!tm_conto)
      dImponib = 0
      dImponibval = 0
      lNumprot = 0
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128771987475062000, "(23000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto 'Omaggi' indicato in 'Personalizzazione vendite' e/o 'Personalizzazione acquisti' inesistente"), True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128771987460866000, "(23000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      'FT EM: OMAGGI
      'RIC FISC EM CLI NO PRIV CESS BENI (O PREST SERV): OMAGGI
23000: bOk = Wripn(23000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
                strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", " ", lRigaiva)

      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128771987967554000, "(23000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(23000, dtrTm, lNumreg, lRiga, "7", 0, Nothing, 0) Then
          LogWrite(oApp.Tr(Me, 129278287544130859, "(23000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
          Return False
        End If
      End If

      If bCa Then
        bOk = WriPrianaAcc(lConto, strDarave, dImporto, dImpVal, dttPN.Rows(dttPN.Rows.Count - 1), dtrTm)
        If Not bOk Then
          LogWrite(oApp.Tr(Me, 128772004217460000, "(23000) Operazione interrotta elaborando la contabilità analitica per il documento |" & strEstremidoc & "|."), True)
          Return False
        End If
      End If ' chkanalitica

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

  Public Overridable Function ScriviRiga23500(ByRef dtrTm As DataRow, ByVal dtotAcconti As Decimal) As Boolean
    'FATTURA DIFF EMESSA storno acconti da DDT: 
    'la riga del cliente fattura che storna la prima riga di questa registazione
    'le scadenze della fattura sono già Ok, visto che sono state calcolate sul residuo (i ddt pagati hanno già diminuito il tot fattura)
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim dImponib As Decimal = 0
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      If bModuliAcquistati = False Then Return True

      If (NTSCInt(dtrTm!tm_conto) <> lContoPrimaRiga) Then
        lConto = lContoPrimaRiga
      Else
        lConto = NTSCInt(dtrTm!tm_conto)
      End If
      lRiga = lRiga + 1
      dImporto = ArrDbl(dtotAcconti * -1, oCldPnfa.TrovaNdec(0))
      strDescr = " "
      strDarave = "A"
      dImponib = 0
      lNumprot = 0
      nNregiva = 0
      nCodIva = 0
      If (CBool((ModuliDittaDitt(strDittaCorrente) And bsModCG)) Or CBool((ModuliExtDittaDitt(strDittaCorrente) And bsModExtCGE)) Or bForzaCG) And dImporto <> 0 Then
        oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
        If dttTmp.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 129205619193183593, "(23500) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "Conto intestatario del documento inesistente"), True)
          Return False
        Else
          If lConto <> NTSCInt(dtrTm!tm_conto) Then
            If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
              LogWrite(oApp.Tr(Me, 129205619214619140, "(23500) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
              Return False
            End If
          End If
        End If
        'FATTURA DIFF EMESSA: storno il conto intestatario del documento per compensarlo con gli acconti da DDT
23500:  bOk = Wripn(23500, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
                " ", nCodIva, dImponib, lNumprot, 0, 0, 0, "", "", " ", lRigaiva)

        If Not bOk Then
          LogWrite(oApp.Tr(Me, 129205619244775390, "(23500) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|."), True)
          Return False
        End If

        If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
          If Not WriPriana2(23500, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
            LogWrite(oApp.Tr(Me, 129278287755087890, "(23500) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
            Return False
          End If
        End If
      End If ' se c'è CG

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
  Public Overridable Function ScriviRiga23600(ByRef dtrTm As DataRow, ByVal dAcconto As Decimal) As Boolean
    'FATTURA DIFF EMESSA storno acconti da DDT: 
    'la riga del cliente fattura che storna l'acconto da DDT emesso
    'deve mettere saldato sulle scadenze del'acconto (tutte, visto che non è possibile avere acconti du ddt che eccedono importo ddt)
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim dImponib As Decimal = 0
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      If bModuliAcquistati = False Then Return True

      If (NTSCInt(dtrTm!tm_conto) <> lContoPrimaRiga) Then
        lConto = lContoPrimaRiga
      Else
        lConto = NTSCInt(dtrTm!tm_conto)
      End If
      lRiga = lRiga + 1
      dImporto = ArrDbl(dAcconto * -1, oCldPnfa.TrovaNdec(0))
      strDescr = " "
      strDarave = "D"
      dImponib = 0
      lNumprot = 0
      nNregiva = 0
      nCodIva = 0
      If (CBool((ModuliDittaDitt(strDittaCorrente) And bsModCG)) Or CBool((ModuliExtDittaDitt(strDittaCorrente) And bsModExtCGE)) Or bForzaCG) And dImporto <> 0 Then
        oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
        If dttTmp.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 129205646837207031, "(23600) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "Conto intestatario del documento inesistente"), True)
          Return False
        Else
          If lConto <> NTSCInt(dtrTm!tm_conto) Then
            If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
              LogWrite(oApp.Tr(Me, 129205646858242187, "(23600) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
              Return False
            End If
          End If
        End If
        'FATTURA DIFF EMESSA: storno il conto intestatario del documento per compensarlo con gli acconti da DDT
23600:  bOk = Wripn(23600, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
               dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
               " ", nCodIva, dImponib, lNumprot, 0, 0, 0, "", "", " ", lRigaiva)

        If Not bOk Then
          LogWrite(oApp.Tr(Me, 129205636926396484, "(23600) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|."), True)
          Return False
        End If

        If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
          If Not WriPriana2(23600, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
            LogWrite(oApp.Tr(Me, 129278287923964843, "(23600) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
            Return False
          End If
        End If
      End If ' se c'è CG

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

  Public Overridable Function ScriviRiga24000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      If bModuliAcquistati = False Then Return True

      If (NTSCInt(dtrTm!tm_conto) <> lContoPrimaRiga) Or (dtrTm!tm_tipork.ToString = "F" And NTSCDec(dtrTm!tm_pagato) = 0 And NTSCDec(dtrTm!tm_pagato2) = 0) Then
        'conto cassa in caso di abbuono su ric fisc emessa a privato per beni i servizi 'incassata'
        lConto = lContoPrimaRiga
        dImporto = ArrDbl(NTSCDec(dtrTm!tm_abbuono), oCldPnfa.TrovaNdec(0))
        dImpVal = ArrDbl(NTSCDec(dtrTm!tm_abbuonov), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      Else
        'pagato riga cliente/fornitore
        'tm_pagato2 e tm_resto sono alimentati solo da retail, dove non è possibile fare documenti in valuta
        lConto = NTSCInt(dtrTm!tm_conto)
        dImporto = ArrDbl(NTSCDec(dtrTm!tm_pagato) + NTSCDec(dtrTm!tm_pagato2) + NTSCDec(dtrTm!tm_abbuono) - NTSCDec(dtrTm!tm_resto), oCldPnfa.TrovaNdec(0))
        dImpVal = ArrDbl(NTSCDec(dtrTm!tm_pagatov) + NTSCDec(dtrTm!tm_abbuonov), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
      End If
      strDescr = " "
      strDarave = strDAContoinc
      If NTSCInt(dtrTm!tb_concassp) > 0 Then
        lControp = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrTm!tb_concassp))
      ElseIf dtrTm.Table.Columns.Contains("tb_concassp2") AndAlso NTSCInt(dtrTm!tb_concassp2) > 0 Then
        lControp = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrTm!tb_concassp2))
      Else
        If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "M" Or dtrTm!tm_tipork.ToString = "(" Then
          lControp = lAconcas
        Else
          lControp = lConcas
          If strPrgParent = "BNPAPNPA" Then
            'parcellazione
            If NTSCInt(dtrTm!tb_codcovg) > 0 Then
              lControp = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrTm!tb_codcovg))
            End If
          End If
        End If
      End If    'If NTSCInt(dtrTm!tb_concassp) > 0 Then

      'se c'è solo l'abbuono e non il pagato, imposto come contropartita di riga l'abbuono
      If ArrDbl(NTSCDec(dtrTm!tm_pagato), oCldPnfa.TrovaNdec(0)) = 0 And ArrDbl(NTSCDec(dtrTm!tm_pagato2), oCldPnfa.TrovaNdec(0)) = 0 Then
        If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "(" Then
          If NTSCDec(dtrTm!tm_abbuono) > 0 Then
            lControp = lAconabat
          Else
            lControp = lAconabpa
          End If
        Else
          If NTSCDec(dtrTm!tm_abbuono) > 0 Then
            lControp = lConabpa
          Else
            lControp = lConabat
          End If
        End If
      End If

      dImponib = 0
      dImponibval = 0
      lNumprot = 0
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0
      If (CBool((ModuliDittaDitt(strDittaCorrente) And bsModCG)) Or CBool((ModuliExtDittaDitt(strDittaCorrente) And bsModExtCGE)) Or bForzaCG) And (dImporto <> 0 Or dImpVal <> 0 Or dImponib <> 0 Or dImpVal <> 0) Then
        oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
        If dttTmp.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 128772052031186000, "(24000) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "Conto intestatario del documento inesistente"), True)
          Return False
        Else
          If lConto <> NTSCInt(dtrTm!tm_conto) Then
            If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
              LogWrite(oApp.Tr(Me, 128772052087190000, "(24000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
              Return False
            End If
          End If
        End If
        'FT EMESSA O RICEVUTA: CLIENTE (O FORN) PER INCASSATO (O PAGATO) + ABBUONO
        'RIC FISC CESS BENI (O PREST SERV) A PRIVATO INCASSATA TOTALMENTE O IN PARTE: CASSA PER ABBUONO
        'RIC FISC CESS BENI (O PREST SERV) A CLI NO PRIVATO INCASSATA PARZIALE O TOTALE: CLIENTE PER INCASSATO E/O ABBUONO
        'RIC FISC CESS BENI A PRIVATO E NON NON INCASSATA: TABPEVE - CRED DA RIC FISC NON INC CESS BENI PER ABBUONO
        'RIC FISC PREST SERV A PRIVATO E NON NON INCASSATA: TABPEVE - CRED DA RIC FISC NON INC PREST SERV PER ABBUONO
        'RIEMIS RIC FISC A PRIVATO CESS BENI (O PREST SERV): IL CLIENTE PRIVATO PER INCASSATO + ABBUONO
        'RIEMIS RIC FISC A NON PRIVATO CESS BENI (O PREST SERV): IL CLIENTE PER INCASSATO + ABBUONO
        'PARCELLAZIONE: FT EMESSA: CLIENTE PER INCASSATO + ABBUONO
        If dImporto <> 0 Or dImpVal <> 0 Then
          nWriPnTipologia = 0
          bWripnIncassato = True
24000:    bOk = Wripn(24000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                  dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
                  strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", " ", lRigaiva)
          If Not bOk Then
            LogWrite(oApp.Tr(Me, 128772053243774000, "(24000) Operazione interrotta elaborando " & _
                            "il documento |" & strEstremidoc & "|."), True)
            Return False
          End If

          If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
            If Not WriPriana2(24000, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
              LogWrite(oApp.Tr(Me, 129278288151406250, "(24000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
              Return False
            End If
          End If
          lRiga = lRiga + 1
        End If

        'se ho utilizzato, per pagare, ANCHE tm_pagato2 ed è una ric. fisc emessa a privato per beni o servizi incassata,
        'devo aggiungere la riga del conto cassa di tm_pagato2
        If NTSCDec(dtrTm!tm_pagato2) <> 0 And NTSCInt(dtrTm!tm_conto) <> lContoPrimaRiga Then

          If dtrTm!tm_tipork.ToString = "F" And bModuliAcquistati = True And (NTSCDec(dtrTm!tm_pagato) <> 0 Or NTSCDec(dtrTm!tm_abbuono) <> 0) Then
            'non devo farlo se ric fisc emessa intestata a cliente privato, perchè tm_pagato2 
            'è già stato rilevato nella reg. della rilevaz. della ricevuta fiscale !!!!

          Else
            If NTSCInt(dtrTm!tb_concassp2) > 0 Then
              lControp = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrTm!tb_concassp2))
            Else
              If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "M" Or dtrTm!tm_tipork.ToString = "(" Then
                lControp = lAconcas
              Else
                lControp = lConcas2
              End If
            End If
            dImporto = ArrDbl(NTSCDec(dtrTm!tm_pagato2), oCldPnfa.TrovaNdec(0))
            dImpVal = 0
            nWriPnTipologia = 0
            bWripnIncassato = True
24001:      bOk = Wripn(24001, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                    dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
                    strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", " ", lRigaiva)
            If Not bOk Then
              LogWrite(oApp.Tr(Me, 129587336442880859, "(24001) Operazione interrotta elaborando " & _
                              "il documento |" & strEstremidoc & "|."), True)
              Return False
            End If

            If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
              If Not WriPriana2(24001, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
                LogWrite(oApp.Tr(Me, 129587336550185546, "(24001) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
                Return False
              End If
            End If
          End If    'If dtrTm!tm_tipork.ToString = "F" And bModuliAcquistati = True Then

        End If    'If NTSCDec(dtrTm!tm_pagato2) <> 0 Then
      End If ' se c'è CG

      ' --- calcola le scadenze (solo se DDT emesso e 'reigstra solo incassi anticipati)
      If NTSCStr(dtrTm!tm_tipork) = "B" And bContIncDDT Then
        bOk = AggScaden(dtrTm, strDatreg, lNumreg, nCausale, strDatdoc, lNumdoc, strAlfdoc, nAnnpar, strAlfpar, lNumpar, "A")
        If Not bOk Then
          LogWrite(oApp.Tr(Me, 129201184824628907, "(24000) Operazione interrotta elaborando le scadenze " & _
                          "del documento |" & strEstremidoc & "|."), True)
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
  Public Overridable Function ScriviRiga25000(ByRef dtrTm As DataRow) As Boolean
    Try
      'obsoleta
      Return ScriviRiga25000(dtrTm, Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ScriviRiga25000(ByRef dtrTm As DataRow, ByRef dttTmOrig As DataTable) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable
    Dim dttPagato As New DataTable     'CASO RETAIL: per documenti raggruppati totalmente incassati poteri aver utilizzato per eseguire l'incasso diversi cod. pagamento
    '                                                in questo datatable sono contenuti, divisi per codpaga, i vari record di pagamento con il relativo importo
    Dim dtrT1() As DataRow = Nothing
    Dim bRestoDetratto As Boolean = False
    Dim lContoCassaResto As Integer = 0

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dtrTm, dttTmOrig})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrTm = CType(oIn(0), DataRow)
        dttTmOrig = CType(oIn(1), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      If bModuliAcquistati = False Then Return True

      dttPagato.Columns.Add("importo", GetType(Decimal))
      dttPagato.Columns.Add("impval", GetType(Decimal))
      dttPagato.Columns.Add("conto", GetType(Integer))

      'determinazione del conto da utilizzare per il resto
      If NTSCDec(dtrTm!tm_resto) <> 0 Then
        'retail: il conto cassa da usare per il resto dovrebbe essere o quello di tabpeve, oppure quello indicato sulla forma di pagamento
        'occhio: potreri avere un resto (contanti) anche con solo tm_pagato <> 0
        If NTSCInt(dtrTm!tb_concassp) > 0 Then
          'prendo il conto cassa in dal cod. pagamento con 'tipo incasso = contanti'. in mancanza prendo tabpeve
          oCldPnfa.ValCodiceDb(dtrTm!tm_codpaga.ToString, strDittaCorrente, "TABPAGA", "N", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            If NTSCStr(dttTmp.Rows(0)!tb_tipincecr) = "C" Then
              lContoCassaResto = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrTm!tb_concassp))
            End If
          End If
        End If
        If dtrTm.Table.Columns.Contains("tb_concassp2") AndAlso NTSCInt(dtrTm!tb_concassp2) > 0 And lContoCassaResto = 0 Then
          oCldPnfa.ValCodiceDb(dtrTm!tm_codpaga2.ToString, strDittaCorrente, "TABPAGA", "N", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then
            If NTSCStr(dttTmp.Rows(0)!tb_tipincecr) = "C" Then
              lContoCassaResto = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrTm!tb_concassp2))
            End If
          End If
        End If
      End If    'If NTSCDec(dtrTm!tm_resto) <> 0 Then

      If lContoCassaResto = 0 Then lContoCassaResto = lConcasPeve 'male che vada come conto cassa prendo quello di tabpeve

      If dttTmOrig Is Nothing OrElse NTSCStr(dtrTm!xx_raggr) = "N" Then
        'documento non raggruppato
        'riga di TM_PAGATO
        dImporto = ArrDbl(NTSCDec(dtrTm!tm_pagato), oCldPnfa.TrovaNdec(0))
        dImpVal = ArrDbl(NTSCDec(dtrTm!tm_pagatov), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))

        If NTSCInt(dtrTm!tb_concassp) > 0 Then
          lConto = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrTm!tb_concassp))
        Else
          If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "M" Or dtrTm!tm_tipork.ToString = "(" Then
            lConto = lAconcas
          Else
            lConto = lConcas
            If strPrgParent = "BNPAPNPA" Then
              'parcellazione
              If NTSCInt(dtrTm!tb_codcovg) > 0 Then
                lConto = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrTm!tb_codcovg))
              End If
            End If
          End If
        End If

        dttPagato.Rows.Add(New Object() {dImporto, dImpVal, lConto})

        'riga di TM_PAGATO2
        'nel caso di incasso tramite tm_pagato2, registro anche la seconda riga di incasso
        If NTSCDec(dtrTm!tm_pagato2) <> 0 Then
          dImporto = ArrDbl(NTSCDec(dtrTm!tm_pagato2), oCldPnfa.TrovaNdec(0))
          dImpVal = 0
          If NTSCInt(dtrTm!tb_concassp2) > 0 Then
            lConto = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrTm!tb_concassp2))
          Else
            If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "M" Or dtrTm!tm_tipork.ToString = "(" Then
              lConto = lAconcas
            Else
              lConto = lConcas2
            End If
          End If
          'il tm_resto (presente solo se presente tm_pagato2 e di importo sempre inferiore a tm_pagato2)
          'è da considerarsi SEMPRE come 'contanti'. se il conto di CG per tm_pagato2 è uguale a quello di tm_resto, faccio una riga sola
          If NTSCDec(dtrTm!tm_resto) <> 0 And lConto = lContoCassaResto Then
            bRestoDetratto = True
            dImporto -= NTSCDec(dtrTm!tm_resto)
          End If
          dttPagato.Rows.Add(New Object() {dImporto, dImpVal, lConto})
        End If    'If NTSCDec(dtrTm!tm_pagato2) <> 0 Then

      Else
        'documento raggruppato: carico tante righe quanti sono i documenti effettivi raggruppando per conto CG di cassa
        'non c'è mai la valuta
        'prima TM_PAGATO
        For Each dtrT As DataRow In dttTmOrig.Select("tm_pagato <> 0 AND xx_dtttm = " & dtrTm!xx_dtttm.ToString)
          dImporto = NTSCDec(dtrT!tm_pagato)
          dImpVal = 0
          If NTSCInt(dtrT!tb_concassp) > 0 Then
            lConto = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrT!tb_concassp))
          Else
            If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "M" Or dtrTm!tm_tipork.ToString = "(" Then
              lConto = lAconcas
            Else
              lConto = lConcas
            End If
          End If
          dtrT1 = dttPagato.Select("conto = " & lConto.ToString)
          If dtrT1.Length > 0 Then
            dtrT1(0)!importo = ArrDbl(NTSCDec(dtrT1(0)!importo) + dImporto, oCldPnfa.TrovaNdec(0))
          Else
            dttPagato.Rows.Add(New Object() {dImporto, dImpVal, lConto})
          End If
        Next
        'poi TM_PAGATO2
        For Each dtrT As DataRow In dttTmOrig.Select("tm_pagato2 <> 0 AND xx_dtttm = " & dtrTm!xx_dtttm.ToString)
          dImporto = NTSCDec(dtrT!tm_pagato2)
          dImpVal = 0
          If NTSCInt(dtrT!tb_concassp2) > 0 Then
            lConto = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrT!tb_concassp2))
          Else
            If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "M" Or dtrTm!tm_tipork.ToString = "(" Then
              lConto = lAconcas
            Else
              lConto = lConcas2
            End If
          End If
          dtrT1 = dttPagato.Select("conto = " & lConto.ToString)
          If dtrT1.Length > 0 Then
            dtrT1(0)!importo = ArrDbl(NTSCDec(dtrT1(0)!importo) + dImporto, oCldPnfa.TrovaNdec(0))
          Else
            dttPagato.Rows.Add(New Object() {dImporto, dImpVal, lConto})
          End If
        Next
      End If    'If dttTmOrig Is Nothing OrElse NTSCStr(dtrTm!xx_raggr) = "N" Then
      dttPagato.AcceptChanges()

      For Each dtrT As DataRow In dttPagato.Select("importo = 0 AND impval = 0")
        'può succedere se tm_pagato = 0 e tm_pagato2 <> 0
        dtrT.Delete()
      Next
      dttPagato.AcceptChanges()

      'correggo la contropartita della riga appena sopra (quella del cliente per la parte incassata)
      'con il conto che effettivamente utilizzerò
      'in questo momento la riga di dttPN è in rowstate = added
      If dttPagato.Rows.Count > 0 Then
        dttPN.Rows(dttPN.Rows.Count - 1)!pn_controp = dttPagato.Rows(0)!conto
      End If

      For Each dtrT As DataRow In dttPagato.Rows
        'registra in cassa
        lRiga = lRiga + 1
        strDescr = " "
        strDarave = strDACassainc
        lControp = NTSCInt(dtrTm!tm_conto)
        dImponib = 0
        dImponibval = 0
        lNumprot = 0
        nNregiva = 0
        strTregiva = " "
        nCodIva = 0
        oCldPnfa.GetContoPerWripn(strDittaCorrente, NTSCInt(dtrT!conto), dttTmp)
        If dttTmp.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 128772055468334000, "(25000) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "Conto/contropartita cassa indicato in 'Personalizzazione vendite' e/o 'Personalizzazione acquisti' e/o sul cod.pagamento inesistente"), True)
          Return False
        Else
          If NTSCInt(dtrT!conto) <> NTSCInt(dtrTm!tm_conto) Then
            If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
              LogWrite(oApp.Tr(Me, 128772055417010000, "(25000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di contropartita |" & NTSCInt(dtrT!conto) & "| gestito a partite/scadenze"), True)
              Return False
            End If
          End If
        End If

        'FT EMESSA O RICEVUTA: CASSA O BANCA PER INCASSATO (O PAGATO)
        'RIC FISC CLI NO PRIV CESS BENI (O PREST SERV) INCASSATA TOTALE O IN PARTE: CASSA O BANCA PER INCASSATO
        'RIEM RIC FISC A PRIVATO E NON CESS BENI (O PREST SERV): CASSA O BANCA PER INCASSATO
        'PARCELLAZIONE: FATTURA E/O NOTA ACCRED: CASSA O BANCA PER INCASSATO
        If NTSCDec(dtrT!importo) <> 0 Or NTSCDec(dtrT!impval) <> 0 Then

          nWriPnTipologia = 0
          bWripnIncassato = True
25000:    bOk = Wripn(25000, dtrTm, strDatreg, lNumreg, lRiga, NTSCInt(dtrT!conto), dttTmp.Rows(0)!an_partite.ToString, _
               dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, NTSCDec(dtrT!importo), strDarave, nNregiva, _
               strTregiva, nCodIva, dImponib, lNumprot, NTSCDec(dtrT!impval), dImponibval, lControp, "", "", " ", lRigaiva)
          If Not bOk Then
            LogWrite(oApp.Tr(Me, 128772055368650000, "(25000) Operazione interrotta elaborando " & _
                            "il documento |" & strEstremidoc & "|."), True)
            Return False
          End If

          If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
            If Not WriPriana2(25000, dtrTm, lNumreg, lRiga, "9", 0, Nothing, 0) Then
              LogWrite(oApp.Tr(Me, 129278288495644531, "(25000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
              Return False
            End If
          End If
        Else
          lRiga = lRiga - 1
        End If    'If dImporto <> 0 Or dImpVal <> 0 Then

      Next    'For Each dtrT As DataRow In dttPagato.Rows

      '--------------------
      'RETAIL: resto in caso di incasso con contante superiore al tot. documento:
      'nel caso di tm_resto <> 0 e conto contabile di resto diverso da conto contabile di cassa2, rilevo una riga in più
      If NTSCDec(dtrTm!tm_resto) <> 0 And bRestoDetratto = False Then
        lRiga = lRiga + 1
        dImporto = ArrDbl(NTSCDec(dtrTm!tm_resto), oCldPnfa.TrovaNdec(0))
        dImpVal = 0
        'prima provo a cercare il primo conto di tipo 'contanti' in repcpaga (RETAIL) su quel reparto
        'se non c'è prendo il conto cassa di tabpeve
        lConto = 0
        If NTSCStr(dtrTm!tm_codrepc).Trim <> "" Then lConto = oCldPnfa.GetContoCassaRepcpaga(strDittaCorrente, NTSCStr(dtrTm!tm_codrepc))
        If lConto <> 0 Then lConto = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, lConto)
        If lConto = 0 Then lConto = lConcasPeve
        strDarave = IIf(strDarave = "D", "A", "D").ToString

        nWriPnTipologia = 0
        bWripnIncassato = True
25002:  bOk = Wripn(dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                    dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
                    strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", " ", lRigaiva)
        If Not bOk Then
          LogWrite(oApp.Tr(Me, 129587373136113281, "(25002) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|."), True)
          Return False
        End If

        If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
          If Not WriPriana2(25000, dtrTm, lNumreg, lRiga, "9", 0, Nothing, 0) Then
            LogWrite(oApp.Tr(Me, 129587373157470703, "(25002) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
            Return False
          End If
        End If
      End If    'If NTSCDec(dtrTm!tm_resto) <> 0 And lConcas2 <> lContoCassaResto Then


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
  Public Overridable Function ScriviRiga26000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      If bModuliAcquistati = False Then Return True

      lRiga = lRiga + 1
      If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "(" Then
        If NTSCDec(dtrTm!tm_abbuono) > 0 Then
          lConto = lAconabat
          dImporto = ArrDbl(NTSCDec(dtrTm!tm_abbuono), oCldPnfa.TrovaNdec(0))
          strDarave = strDACassainc
        Else
          lConto = lAconabpa
          dImporto = ArrDbl(NTSCDec(dtrTm!tm_abbuono) * -1, oCldPnfa.TrovaNdec(0))
          strDarave = strDAContoinc
        End If
      Else
        If NTSCDec(dtrTm!tm_abbuono) > 0 Then
          lConto = lConabpa
          dImporto = ArrDbl(NTSCDec(dtrTm!tm_abbuono), oCldPnfa.TrovaNdec(0))
          dImpVal = ArrDbl(NTSCDec(dtrTm!tm_abbuonov), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
          strDarave = strDACassainc
        Else
          lConto = lConabat
          dImporto = ArrDbl(NTSCDec(dtrTm!tm_abbuono) * -1, oCldPnfa.TrovaNdec(0))
          dImpVal = ArrDbl(NTSCDec(dtrTm!tm_abbuonov) * -1, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
          strDarave = strDAContoinc
        End If
      End If
      strDescr = " "
      lControp = NTSCInt(dtrTm!tm_conto)
      dImponib = 0
      dImponibval = 0
      lNumprot = 0
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128772060044770000, "(26000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto 'Abbuoni attivi' e/o 'Abbuoni passivi' indicato in 'Personalizzazione vendite' e/o 'Personalizzazione acquisti' inesistente"), True)
        Return False
      Else
        If lConto <> NTSCInt(dtrTm!tm_conto) Then
          If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
            LogWrite(oApp.Tr(Me, 128772060059590000, "(26000) Operazione interrotta elaborando " & _
                      "il documento |" & strEstremidoc & "|. " & _
                      "Conto di contropartita |" & lConto & "| gestito a partite/scadenze"), True)
            Return False
          End If
        End If
      End If

      'FT EMESSA O RICEVUTA: ABBUONO
      'RIC FISC TUTTI I CASI: ABBUONO
      'RIEM RIC FISC TUTTI I CASI: ABBUONO
      'PARCELLAZIONE: FATTURA E/O NOTA ACCRED: ABBUONO
      nWriPnTipologia = 0
      bWripnIncassato = True
26000: bOk = Wripn(26000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
             dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausale, dImporto, strDarave, nNregiva, _
             strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, lControp, "", "", " ", lRigaiva)

      If Not bOk Then
        LogWrite(oApp.Tr(Me, 128772060107014000, "(26000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        If Not WriPriana2(26000, dtrTm, lNumreg, lRiga, "6", 0, Nothing, 0) Then
          LogWrite(oApp.Tr(Me, 129278288646123046, "(26000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
          Return False
        End If
      End If

      If bCa Then
        bOk = WriPrianaAcc(lConto, strDarave, dImporto, dImpVal, dttPN.Rows(dttPN.Rows.Count - 1), dtrTm)
        If Not bOk Then
          LogWrite(oApp.Tr(Me, 128772060127606000, "(26000) Operazione interrotta elaborando la contabilità analitica per il documento |" & strEstremidoc & "|."), True)
          Return False
        End If
      End If ' chkanalitica

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

  Public Overridable Function ScriviRiga28000(ByRef dtrTm As DataRow, ByRef dtrControp As DataRow, _
                                              ByRef dttTmOrig As DataTable) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim dImpVal As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim dImponib As Decimal = 0
    Dim dImponibval As Decimal = 0
    Dim strTregiva As String = " "
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable

    Try
      If bModuliAcquistati = False Then Return True

      lRiga = lRiga + 1
      lConto = NTSCInt(dtrControp!lControp)
      If lConto < 1000000 Then
        'mi è stata passata la contropartita
        If strPrgParent = "BNPAPNPA" Then
          lConto = oCldPnfa.TrovaContoDaCodcontr(strDittaCorrente, NTSCInt(dtrControp!lControp))
        Else
          oCldPnfa.ValCodiceDb(NTSCInt(dtrControp!lControp).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
          lConto = NTSCInt(dttTmp.Rows(0)!tb_concove)
          dttTmp.Clear()
        End If
      End If
      strDescr = " "
      strDarave = strDAControp
      If ArrDbl(NTSCDec(dtrTm!tm_pagato) + NTSCDec(dtrTm!tm_abbuono), oCldPnfa.TrovaNdec(0)) = ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut), oCldPnfa.TrovaNdec(0)) Then
        dImporto = ArrDbl(NTSCDec(dtrControp!dImporto), oCldPnfa.TrovaNdec(0))
      Else
        dImporto = ArrDbl(NTSCDec(dtrControp!dImporto) * (NTSCDec(dtrTm!tm_pagato) + NTSCDec(dtrTm!tm_abbuono)) / (NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut)), oCldPnfa.TrovaNdec(0))
      End If
      If dImporto < 0 Then
        dImporto = dImporto * -1
        strDarave = IIf(strDarave = "A", "D", "A").ToString
      End If
      dImpVal = 0
      lControp = NTSCInt(dtrTm!tm_conto)
      dImponib = 0
      dImponibval = 0
      lNumprot = lNewnumprot ' ### per i fornitori... (ci vuole)
      nNregiva = 0
      strTregiva = " "
      nCodIva = 0
      lRigaiva = 0

      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)

      If NTSCStr(dttTmp.Rows(0)!an_sosppr) = "S" Then
        'PARCELLAZIONE: FATTURA E/O NOTA ACCRED: STORNO DEL CONTO SOSPESO
        nWriPnTipologia = 0
        bWripnIncassato = True
28000:  bOk = Wripn(28000, dtrTm, strDatreg, lNumreg, lRiga, lConRicSosp, dttTmp.Rows(0)!an_partite.ToString, _
                    dttTmp.Rows(0)!an_scaden.ToString, strDescr, 9998, dImporto, _
                    IIf(strDarave = "A", "D", "A").ToString, nNregiva, strTregiva, nCodIva, dImponib, _
                    lNumprot, dImpVal, dImponibval, lControp, NTSCDate(dtrControp!strDtIniz).ToShortDateString, _
                    NTSCDate(dtrControp!strDtFin).ToShortDateString, strAlfpro, lRigaiva)
        If Not bOk Then
          LogWrite(oApp.Tr(Me, 129651479274687500, "(28000) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|."), True)
          Return False
        End If

        lRiga = lRiga + 1
        'PARCELLAZIONE: FATTURA E/O NOTA ACCRED: RILEVAZIONE DEL RICAVO
        nWriPnTipologia = 0
        bWripnIncassato = True
28500:  bOk = Wripn(28500, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                    dttTmp.Rows(0)!an_scaden.ToString, strDescr, 9998, dImporto, _
                    strDarave, nNregiva, strTregiva, nCodIva, dImponib, _
                    lNumprot, dImpVal, dImponibval, lControp, NTSCDate(dtrControp!strDtIniz).ToShortDateString, _
                    NTSCDate(dtrControp!strDtFin).ToShortDateString, strAlfpro, lRigaiva)
        If Not bOk Then
          LogWrite(oApp.Tr(Me, 129651481374101563, "(28500) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|."), True)
          Return False
        End If

        'per ora non è gestita la CA!!!
        '''gestione della cont. analitica
        ''If bCa Then
        ''  bOk = WriPriana(dtrTm, lConto, strDarave, dImporto, NTSCInt(dtrControp!lControp), dImpVal, _
        ''                  NTSCDec(dtrControp!dIvaInded), NTSCDec(dtrControp!dIvaIndedVal), _
        ''                  NTSCDate(dtrControp!strDtIniz).ToShortDateString, _
        ''                  NTSCDate(dtrControp!strDtFin).ToShortDateString, False, _
        ''                  dttPN.Rows(dttPN.Rows.Count - 1), dttTmOrig)

        ''  If Not bOk Then
        ''    LogWrite(oApp.Tr(Me, 129651482344501953, "(28500) Operazione interrotta elaborando la contabilità analitica " & _
        ''                    "del documento |" & strEstremidoc & "|."), True)
        ''    Return False
        ''  End If
        ''End If    'If bCa Then

        ''If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
        ''  If Not WriPriana2(28500, dtrTm, lNumreg, lRiga, "0", NTSCInt(dtrControp!lControp), dtrControp, NTSCDec(dtrControp!dIvaInded), dttTmOrig) Then
        ''    LogWrite(oApp.Tr(Me, 129651482477138672, "(28500) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
        ''    Return False
        ''  End If
        ''End If

      End If    'If NTSCStr(dttTmp.Rows(0)!an_sosppr) = "S" Then

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

  Public Overridable Function ScriviRiga40000(ByRef dtrTm As DataRow) As Boolean
    Dim lConto As Integer = 0
    Dim strDescr As String = " "
    Dim dImporto As Decimal = 0
    Dim strAnscaden As String = ""
    Dim strDarave As String = ""
    Dim lControp As Integer = 0
    Dim bOk As Boolean = False
    Dim dttTmp As New DataTable


    Try
      ' inizia la scrittura della riga cliente notule ...
      lRiga = 1
      lConto = lConClinot
      dImporto = ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut) - NTSCDec(dtrTm!tm_altriprev), oCldPnfa.TrovaNdec(0))
      dImporto = ArrDbl(dImporto - (NTSCDec(dtrTm!tm_pagato) + NTSCDec(dtrTm!tm_abbuono)), oCldPnfa.TrovaNdec(0))

      ' memorizza il conto primariga per succ. trattamento
      lContoPrimaRiga = lConto
      strDarave = "D"
      lControp = 0
      lNumprot = lNewnumprot ' ### per i fornitori... (ci vuole)
      strAlfpro = " "

      ' chiude la reg. della prima riga
      If bModuliAcquistati = True Then
        oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
        If dttTmp.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 129648913208027343, "(40000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto 'clienti notule' inesistente"), True)
          Return False
        End If
        'NOTULE: prima riga - clienti notule
        nWriPnTipologia = 0
        bWripnIncassato = False
40000:  bOk = Wripn(40000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                    dttTmp.Rows(0)!an_scaden.ToString, strDescr, nCausNotul, dImporto, strDarave, 0, _
                    " ", 0, 0, 0, 0, 0, NTSCInt(dtrTm!tm_conto), "", "", strAlfpro, 0)
        If Not bOk Then
          LogWrite(oApp.Tr(Me, 129648913187792968, "(40000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
          Return False
        End If
      End If ' se c'è anche modulo CG
      ' --- calcoal scadenze...
      bOk = AggScaden(dtrTm, strDatreg, lNumreg, nCausale, strDatdoc, lNumdoc, strAlfdoc, nAnnpar, strAlfpar, _
                      lNumpar, strDAConto)
      If Not bOk Then
        LogWrite(oApp.Tr(Me, 129648913371464843, "(40000) Operazione interrotta elaborando le scadenze " & _
                        "del documento |" & strEstremidoc & "|."), True)
        Return False
      End If
      'PER ORA LA PARCELLAZIONE NON GESTISCE LA CA!!!!
      'If bModuliAcquistati Then 'solo se ho la CG ...
      '  If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
      '    If Not WriPriana2(1000, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
      '      LogWrite(oApp.Tr(Me, 129648913488183593, "(40000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
      '      Return False
      '    End If
      '  End If
      'End If

      'NOTULE: seconda riga - conto transitorio notule (non gestito a partite/scadenze)
      lRiga = 2
      If bModuliAcquistati = True Then
        nWriPnTipologia = 0
        bWripnIncassato = False
41000:  bOk = Wripn(41000, dtrTm, strDatreg, lNumreg, lRiga, lConTranot, "N", _
                    "N", strDescr, nCausNotul, dImporto, "A", 0, _
                    " ", 0, 0, 0, 0, 0, NTSCInt(dtrTm!tm_conto), "", "", strAlfpro, 0)
        If Not bOk Then
          LogWrite(oApp.Tr(Me, 129648960692226562, "(41000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|."), True)
          Return False
        End If

      End If    'If bModuliAcquistati = True Then

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

  Public Overridable Function Wripn(ByRef dtrTm As DataRow, ByVal strDatreg As String, ByVal lNumreg As Integer, ByVal lRiga As Integer, _
                                  ByVal lConto As Integer, ByVal strAnpartite As String, ByVal strAnscaden As String, _
                                  ByVal strDescr As String, ByVal nCausale As Integer, _
                                  ByVal dImporto As Decimal, ByVal strDarave As String, ByVal nNregiva As Integer, _
                                  ByVal strTregiva As String, ByVal nCodIva As Integer, ByVal dImponib As Decimal, _
                                  ByVal lNumprot As Integer, ByVal dImpVal As Decimal, ByVal dImponibval As Decimal, _
                                  ByVal lControp As Integer, Optional ByVal dDtIniz As String = "", _
                                  Optional ByVal dDtFin As String = "", Optional ByVal strAlfpro As String = " ", _
                                  Optional ByVal lRigaiva As Integer = 0) As Boolean
    Try
      'obsoleta
      Return Wripn(0, dtrTm, strDatreg, lNumreg, lRiga, lConto, strAnpartite, strAnscaden, strDescr, nCausale, _
                   dImporto, strDarave, nNregiva, strTregiva, nCodIva, dImponib, lNumprot, dImpVal, dImponibval, _
                   lControp, dDtIniz, dDtFin, strAlfpro, lRigaiva)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function Wripn(ByVal lIDriga As Integer, ByRef dtrTm As DataRow, ByVal strDatreg As String, ByVal lNumreg As Integer, ByVal lRiga As Integer, _
                                    ByVal lConto As Integer, ByVal strAnpartite As String, ByVal strAnscaden As String, _
                                    ByVal strDescr As String, ByVal nCausale As Integer, _
                                    ByVal dImporto As Decimal, ByVal strDarave As String, ByVal nNregiva As Integer, _
                                    ByVal strTregiva As String, ByVal nCodIva As Integer, ByVal dImponib As Decimal, _
                                    ByVal lNumprot As Integer, ByVal dImpVal As Decimal, ByVal dImponibval As Decimal, _
                                    ByVal lControp As Integer, Optional ByVal dDtIniz As String = "", _
                                    Optional ByVal dDtFin As String = "", Optional ByVal strAlfpro As String = " ", _
                                    Optional ByVal lRigaiva As Integer = 0) As Boolean
    Dim dttTmp As New DataTable
    Dim dttAnagra As New DataTable
    Dim nMese As Integer = 0
    Dim dtComiva As String = ""
    Dim strMentri As String = "*"
    Dim strAn_sosppr As String = "N"
    Try
      'solo per non far dare il messaggio in testprecompila
      'If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, 

      'legge la causale
      oCldPnfa.ValCodiceDb(nCausale.ToString, strDittaCorrente, "TABCAUC", "N", strDescauc)

      '-------------------------------------------------------------------------------------------
      oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttAnagra)
      If bCa2 Or strPrgParent = "BNPAPNPA" Then
        'solo con CA2 (per un fatto di prestazioni, diversamente si comporta come ha sempre fatto per un fatto di prestazioni)
        'se il conto passatomi non è gestito a date di competenza, come pn_datini e pn_datfin prendo la data di registrazione

        'per parcellazione mi serve sapere se il conto usa il costo/ricavo sospeso
        If dttAnagra.Rows.Count > 0 Then
          strAn_sosppr = NTSCStr(dttAnagra.Rows(0)!an_sosppr)
          If NTSCStr(dttAnagra.Rows(0)!an_accperi) = "N" Then
            dDtIniz = strDatreg
            dDtFin = strDatreg
          End If
        End If
      End If    'If bCa2 Then

      '-------------------------------------------------------------------------------------------
      'AUTOTRASPORTATORE
      'se il numero di registro iva è stato impostato, la causale è di tipo autotrasportatore e
      'la data di competenza iva = data documento, verifico se il regsitro è associato ad una
      'attività di tipo autotrasportatore: se così è ricalcolo la data di competenza IVA spostandola alla fine
      'della liquidazione successiva a quella del documento
      If nNregiva <> 0 And strGestdtciva = "R" And NTSCDate(dtrTm!tm_dtcomiva) = NTSCDate(strDatdoc) And bDerogaIva = False Then
        oCldPnfa.GetTabduriTabatti(strDittaCorrente, strTregiva, nNregiva, NTSCDate(dtrTm!tm_dtcomiva).Year, dttTmp)

        If dttTmp.Rows.Count > 0 Then
          If dttTmp.Rows(0)!tb_regautotr.ToString = "N" Then
            dtComiva = NTSCDate(dtrTm!tm_dtcomiva).ToShortDateString
            dttTmp.Clear()
            GoTo Passa
          Else
            bAutotrasportatore = True
            strMentri = dttTmp.Rows(0)!tb_atmentri.ToString
          End If
        End If
        dttTmp.Clear()
        If strMentri <> "*" Then
          Select Case strMentri
            Case "M"
              'liquidazione mensile
              dtComiva = FineMese(NTSCDate(dtrTm!tm_dtcomiva).AddMonths(1).ToShortDateString)
            Case "T"
              'liquidazione trimestrale
              nMese = Month(NTSCDate(dtrTm!tm_dtcomiva))
              If nMese <= 3 Then dtComiva = ("30/06/" & Year(NTSCDate(dtrTm!tm_dtcomiva)))
              If nMese > 3 And nMese <= 6 Then dtComiva = ("30/09/" & Year(NTSCDate(dtrTm!tm_dtcomiva)))
              If nMese > 6 And nMese <= 9 Then dtComiva = ("31/12/" & Year(NTSCDate(dtrTm!tm_dtcomiva)))
              If nMese > 9 Then dtComiva = ("31/03/" & Year(NTSCDate(dtrTm!tm_dtcomiva)) + 1)
            Case "A"
              'liquidazione annuale
              dtComiva = NTSCDate(dtrTm!tm_dtcomiva).ToShortDateString
          End Select
        End If
      Else
        dtComiva = NTSCDate(dtrTm!tm_dtcomiva).ToShortDateString
      End If
Passa:
      'fine ricalcolo data competenza iva
      dttPN.Rows.Add(dttPN.NewRow())
      With dttPN.Rows(dttPN.Rows.Count - 1)
        !codditt = strDittaCorrente
        !pn_datreg = NTSCDate(strDatreg)
        !pn_numreg = lNumreg
        !pn_riga = lRiga

        If lIDriga = 1100 Or lIDriga = 1110 Then
          !pn_csriga = -2   'fisso = righe di giroconto IVA da cliente a IVA split payment!!!
        End If

        If strPrgParent = "BNPAPNPA" Then
          'parcellazione: se la parcella non è incassata devo far passare per il conto sospeso
          If nWriPnTipologia = 0 Or bWripnIncassato Then
            !pn_conto = lConto
            !pn_contodef = 0
          Else
            'ricavi: verifico se devo passare per il conto sospeso
            !pn_conto = IIf(lConRicSosp <> 0 And strAn_sosppr = "S", lConRicSosp, lConto)
            !pn_contodef = IIf(lConRicSosp <> 0 And strAn_sosppr = "S", lConto, 0)
          End If
        Else
          !pn_conto = lConto
          !pn_tmtipork = NTSCStr(dtrTm!tm_tipork)
          !pn_tmanno = NTSCInt(dtrTm!tm_anno)
          !pn_tmserie = NTSCStr(dtrTm!tm_serie)
          !pn_tmnumdoc = NTSCInt(dtrTm!tm_numdoc)
          !pn_tmtipreg = "F"  'fattura
          'specifico se reg. incasso o omaggi solo per documenti non di tipo 'corrispettivo raggruppato'
          If lIDriga >= 22000 And lIDriga <= 23000 Then !pn_tmtipreg = "O" 'omaggio
          If lIDriga >= 23500 And lIDriga <= 26000 Then !pn_tmtipreg = "I" 'incasso/pagamento
        End If

        !pn_causale = nCausale
        !pn_partite = strAnpartite
        !pn_scadenz = strAnscaden
        !pn_darave = strDarave
        If strDarave = "D" Then
          !pn_importo = dImporto
          !pn_dare = dImporto
          !pn_avere = 0
          !pn_impval = dImpVal
          !pn_avereval = 0
          !pn_dareval = dImpVal
          !pn_imponib = dImponib
          !pn_imponibval = dImponibval
          dTdare = ArrDbl(dTdare + dImporto, oCldPnfa.TrovaNdec(0))
        Else
          !pn_importo = dImporto * -1
          !pn_dare = 0
          !pn_avere = dImporto
          !pn_impval = dImpVal * -1
          !pn_avereval = dImpVal
          !pn_dareval = 0
          !pn_imponib = dImponib * -1
          !pn_imponibval = dImponibval * -1
          dTavere = ArrDbl(dTavere + dImporto, oCldPnfa.TrovaNdec(0))
        End If

        ' le sei righe sotto sostituiscono tutto il blocco asteriscato in seguito...
        !pn_datdoc = NTSCDate(strDatdoc)
        !pn_annpar = nAnnpar
        !pn_alfpar = strAlfpar
        !pn_numpar = lNumpar
        !pn_alfdoc = strAlfdoc
        !pn_numdoc = lNumdoc

        !pn_descr = strDescr
        !pn_escomp = nEscomp
        !pn_controp = lControp
        !pn_fllg = " "
        !pn_flri = " "
        !pn_flst = " "
        !pn_tregiva = strTregiva
        !pn_nregiva = nNregiva
        !pn_codiva = nCodIva
        If nCodIva > 0 Then
          GetTabcivaRow(nCodIva, dttTmp)
          If dttTmp.Rows.Count = 0 Then
            LogWrite(oApp.Tr(Me, 128770949628568000, "Attenzione! Codice Iva |" & nCodIva & "| inesistente" & _
                    " nel documento |" & strEstremidoc & "|."), True)
            Return False
          Else
            !pn_aliqiva = NTSCDec(dttTmp.Rows(0)!tb_aliq) '####
            !pn_indetr = CalcolaIvaIndetr(strTregiva, NTSCDec(dttTmp.Rows(0)!tb_inded))
            !pn_tipacq = NTSCStr(dttTmp.Rows(0)!tb_tipacq)
          End If

          !pn_flcee = IIf(dtrTm!tb_prestserv.ToString = "N", "M", dtrTm!tb_prestserv.ToString).ToString

          'se c'è modulo 'telematico operazioni rilevanti IVA' abilitato 
          'prendo dal codice iva SE ho forzato l'opzine = S o se = " " e modulo telematico abilitato
          If strNaturaOperazDaCodiva = "S" Or (strNaturaOperazDaCodiva = "" And CBool((ModuliExtDittaDitt(strDittaCorrente) And bsModExtCON))) Then
            'il campo è riciclato, faccio in controllo un po' più stretto per evitare errori di valori non ammessi
            If dttTmp.Rows(0)!tb_rilanacq.ToString.ToUpper = "S" Or dttTmp.Rows(0)!tb_rilanacq.ToString.ToUpper = "M" Or _
               dttTmp.Rows(0)!tb_rilanacq.ToString.ToUpper = "A" Or dttTmp.Rows(0)!tb_rilanacq.ToString.ToUpper = "B" Or _
               dttTmp.Rows(0)!tb_rilanacq.ToString.ToUpper = "C" Or dttTmp.Rows(0)!tb_rilanacq.ToString.ToUpper = "D" Or _
               dttTmp.Rows(0)!tb_rilanacq.ToString.ToUpper = "E" Then
              !pn_flcee = dttTmp.Rows(0)!tb_rilanacq.ToString.ToUpper
            End If
          End If
          !pn_rsmvidim = "N"
          'se vendita di merci a RSM imposto che deve essere successivamente memorizzata la data vidimazione
          If strTipsogivaContoPrimaRiga = "R" And NTSCStr(!pn_flcee) = "M" And strTregiva <> "A" And strTregiva.Trim <> "" Then
            !pn_rsmvidim = "S"
          End If

          dttTmp.Clear()
        Else
          !pn_aliqiva = 0
          !pn_indetr = 0
          !pn_tipacq = " "
          !pn_flcee = IIf(dtrTm!tb_prestserv.ToString = "N", "M", dtrTm!tb_prestserv.ToString).ToString
        End If
        !pn_numpro = lNumprot

        !pn_codvalu = dtrTm!tm_valuta
        !pn_codccos = 0
        !pn_ultdesc = " "
        If nCodIva <> 0 Then
          If dtrTm!tm_tipork.ToString = "F" Or dtrTm!tm_tipork.ToString = "I" Then
            !pn_contocf = RitornaContoPrimaRiga(NTSCInt(dtrTm!tm_conto), dtrTm, strAnscaden)
            If NTSCInt(!pn_contocf) = 0 Then !pn_contocf = lControp
          Else
            !pn_contocf = lControp
          End If
        Else
          !pn_contocf = 0
        End If
        !pn_cambio = dtrTm!tm_cambio
        !pn_integr = IIf(bInt, "S", "N").ToString
        ' da per scontato che non sia gestito in modo speciale
        !pn_dtvaluta = NTSCDate(strDatreg)
        If dDtIniz <> "" Then
          !pn_datini = NTSCDate(dDtIniz)
        Else
          !pn_datini = NTSCDate(strDatreg)
        End If
        If dDtFin <> "" Then
          !pn_datfin = NTSCDate(dDtFin)
        Else
          !pn_datfin = NTSCDate(strDatreg)
        End If
        'memorizzo in pn_tipmovc un 1 se il sottoconto è un cespite (per la gestione integrata ...)
        !pn_tipmovc = IIf(dttAnagra.Rows(0)!an_tipacq.ToString = "S" Or dttAnagra.Rows(0)!an_tipacq.ToString = "X", 1, 0)
        !pn_spunta = "N"
        !pn_chiusecb = "N"
        !pn_ultagg = NTSCDate(DateTime.Now.ToShortDateString)
        !pn_opnome = oApp.User.Nome
        If bDerogaIva Then
          !pn_dtcomiva = NTSCDate(strDtcomivaForm)
        Else
          !pn_dtcomiva = NTSCDate(IIf(NTSCDate(dtComiva) <> NTSCDate(IntSetDate("01/10/1900")), dtComiva, strDatreg))
        End If
        If NTSCDate(strDtulliqi) >= NTSCDate(!pn_dtcomiva) Then
          LogWrite(oApp.Tr(Me, 128770950361612000, "Data competenza IVA (|" & NTSCDate(!pn_dtcomiva).ToShortDateString & _
                  "|) anteriore all'ultima liquidazione Iva (|" & strDtulliqi & "|)" & _
                  " nel documento |" & strEstremidoc & "|. Documento non contabilizzato."), True)
          Return False
        End If
        If strDataPlafondProposta = "D" Then
          !pn_dtcomplaf = NTSCDate(!pn_datdoc)
        Else
          !pn_dtcomplaf = NTSCDate(strDatreg) ' per ora è così per default : quando avremo il dato in testmag cambieremo !")
        End If
        !pn_alfpro = strAlfpro
        !pn_rigaiva = lRigaiva
        !pn_descauc = strDescauc
        !pn_rivend = IIf(NTSCStr(!pn_tipacq) = "R", "S", "N") ' per ora ..

        !pn_flrsm1 = "O" ' per ora ....
        If strPrgParent = "BNPAPNPA" Then
          !pn_flrsm2 = IIf(nCodIva = 0, "N", "A").ToString
        Else
          !pn_flrsm2 = "N" 'in bsvepnfa VB6 è sempre stato messo 'N' fisso 
        End If

        'specifico per la gestione professionisti
        !pn_movincp = 0
        !pn_movcron = 0
        If bProfes = True And lRiga = 1 Then
          If bWripnIncassato = False Then !pn_movcron = 7 'riferimento sulla riga del cliente per l'emissione della fattura (registro cronol)
          If bWripnIncassato = False And NTSCStr(dtrTm!tm_tipork) = "2" Then !pn_movcron = 8 'nota di accred a storno fattura - riferimento per reg cronologico
          If bWripnIncassato = True Then !pn_movcron = 1 'riferimento sulla riga del cliente sul pagamento (registro cronol) - per adesso è fisso cassa
          If bWripnIncassato = True Then !pn_movincp = 1 'riferimento sulla riga del cliente sul pagamento (registro inc/pag)
        End If

        ' se fattura con iva ad esig diff allora se nuovo sistema riempie opportuanmente anche gli ultimi campi di moviva
        If strOldgesived = "N" And lRiga = 1 And (dtrTm!tm_tipork.ToString = "D" Or dtrTm!tm_tipork.ToString = "A" Or dtrTm!tm_tipork.ToString = "N" Or dtrTm!tm_tipork.ToString = "E" Or dtrTm!tm_tipork.ToString = "P" Or dtrTm!tm_tipork.ToString = "S" _
                                                 Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "T" Or dtrTm!tm_tipork.ToString = "£" Or dtrTm!tm_tipork.ToString = "(") And strFattsosp = "S" Then
          If strIEDDatreg.Trim <> "" Then
            'nota accred IVA differita A STORNO FATTURA
            !pn_ieddatregr = NTSCDate(strIEDDatreg)
            !pn_iednumregr = lIEDNumreg
            !pn_iedrigaregr = nIEDRigareg
          Else
            'emessa fattura IVA differita o emessa nota accred IVA differita NON a storno fattura
            !pn_ieddatregr = NTSCDate(strDatreg)
            !pn_iednumregr = lNumreg
            !pn_iedrigaregr = 1 ' la riga del cliente, sempre")
          End If
        Else ' vecchio sistema (S)
        End If
      End With    'With dttPN.Rows(dttPN.Rows.Count - 1)

      ' adesso aggiorna MOVIVA
      If strTregiva <> " " Then
        dttMO.Rows.Add(dttMO.NewRow())
        With dttMO.Rows(dttMO.Rows.Count - 1)
          !codditt = strDittaCorrente
          !mi_datreg = dttPN.Rows(dttPN.Rows.Count - 1)!pn_datreg
          !mi_numreg = dttPN.Rows(dttPN.Rows.Count - 1)!pn_numreg
          !mi_riga = dttPN.Rows(dttPN.Rows.Count - 1)!pn_riga
          !mi_conto = dttPN.Rows(dttPN.Rows.Count - 1)!pn_conto
          !mi_datmast = dttPN.Rows(dttPN.Rows.Count - 1)!pn_datreg
          !mi_alfmast = " "
          !mi_nummast = dttPN.Rows(dttPN.Rows.Count - 1)!pn_numreg
          !mi_causale = dttPN.Rows(dttPN.Rows.Count - 1)!pn_causale
          !mi_datreg3 = IIf(strGestdtciva = "R", dttPN.Rows(dttPN.Rows.Count - 1)!pn_dtcomiva, dttPN.Rows(dttPN.Rows.Count - 1)!pn_datreg)
          !mi_tregiva = dttPN.Rows(dttPN.Rows.Count - 1)!pn_tregiva
          !mi_nregiva = dttPN.Rows(dttPN.Rows.Count - 1)!pn_nregiva
          '//attenzione all'alfa protocollo ...
          If dttPN.Rows(dttPN.Rows.Count - 1)!pn_tregiva.ToString = "A" Or dttPN.Rows(dttPN.Rows.Count - 1)!pn_tregiva.ToString = "T" Then
            !mi_numpro = dttPN.Rows(dttPN.Rows.Count - 1)!pn_numpro
            !mi_alfdoc3 = " "
          Else
            !mi_numpro = dttPN.Rows(dttPN.Rows.Count - 1)!pn_numpro
            !mi_alfdoc3 = dttPN.Rows(dttPN.Rows.Count - 1)!pn_alfdoc
          End If
          !mi_datdoc = dttPN.Rows(dttPN.Rows.Count - 1)!pn_datdoc
          !mi_alfdoc = dttPN.Rows(dttPN.Rows.Count - 1)!pn_alfdoc
          !mi_numdoc = dttPN.Rows(dttPN.Rows.Count - 1)!pn_numdoc
          !mi_alfpar = dttPN.Rows(dttPN.Rows.Count - 1)!pn_alfpar
          !mi_annpar = dttPN.Rows(dttPN.Rows.Count - 1)!pn_annpar
          !mi_numpar = dttPN.Rows(dttPN.Rows.Count - 1)!pn_numpar
          !mi_escomp = dttPN.Rows(dttPN.Rows.Count - 1)!pn_escomp
          !mi_contocf = dttPN.Rows(dttPN.Rows.Count - 1)!pn_contocf
          !mi_darave = dttPN.Rows(dttPN.Rows.Count - 1)!pn_darave
          !mi_importo = dttPN.Rows(dttPN.Rows.Count - 1)!pn_importo 'importo iva stesso segno di pn_importo
          !mi_codiva = dttPN.Rows(dttPN.Rows.Count - 1)!pn_codiva
          !mi_aliqiva = dttPN.Rows(dttPN.Rows.Count - 1)!pn_aliqiva
          !mi_indetr = dttPN.Rows(dttPN.Rows.Count - 1)!pn_indetr
          !mi_imponib = dttPN.Rows(dttPN.Rows.Count - 1)!pn_imponib ' imponibile stesso segno di pn_importo
          !mi_prodef = "P"
          !mi_controp = 0
          !mi_tipacq = dttPN.Rows(dttPN.Rows.Count - 1)!pn_tipacq
          !mi_codvalu = dttPN.Rows(dttPN.Rows.Count - 1)!pn_codvalu
          !mi_impval = dttPN.Rows(dttPN.Rows.Count - 1)!pn_impval
          !mi_cambio = dttPN.Rows(dttPN.Rows.Count - 1)!pn_cambio
          !mi_imponibval = dttPN.Rows(dttPN.Rows.Count - 1)!pn_imponibval
          !mi_dtorael = ""
          !mi_integr = IIf(bInt, "S", "N").ToString
          !mi_flcee = dttPN.Rows(dttPN.Rows.Count - 1)!pn_flcee

          '--- per ora cosi' , poi da testmag, un giorno
          !mi_dtcomiva = dttPN.Rows(dttPN.Rows.Count - 1)!pn_dtcomiva
          If strDataPlafondProposta = "D" Then
            !mi_dtcomplaf = dttPN.Rows(dttPN.Rows.Count - 1)!pn_datdoc ' così ci va datdoc")
          Else
            !mi_dtcomplaf = NTSCDate(strDatreg) ' così ci va datreg")
          End If
          If strPrgParent = "BNPAPNPA" Then
            !mi_flrsm2 = "A"
          End If
          !mi_ultagg = NTSCDate(DateTime.Now.ToShortDateString)
          !mi_rivend = dttPN.Rows(dttPN.Rows.Count - 1)!pn_rivend
          !mi_alfpro = dttPN.Rows(dttPN.Rows.Count - 1)!pn_alfpro
          !mi_rigareg = 0 ' sempre 0, per le fatture")
          ' se fattura con iva ad esig diff allora se nuovo sistema riempie opportuanmente anche gli ultimi campi di moviva
          If strOldgesived = "N" And (dtrTm!tm_tipork.ToString = "D" Or dtrTm!tm_tipork.ToString = "A" Or dtrTm!tm_tipork.ToString = "N" Or dtrTm!tm_tipork.ToString = "E" Or dtrTm!tm_tipork.ToString = "P" Or dtrTm!tm_tipork.ToString = "S" _
                                      Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "T" Or dtrTm!tm_tipork.ToString = "£" Or dtrTm!tm_tipork.ToString = "(") And strFattsosp = "S" Then
            If strIEDDatreg.Trim <> "" Then
              'nota accred IVA differita A STORNO FATTURA
              !mi_tipoivaed = "N"
              !mi_ieddatregr = NTSCDate(strIEDDatreg)
              !mi_iednumregr = lIEDNumreg
              !mi_iedrigaregr = nIEDRigareg
            Else
              'emessa fattura IVA differita o emessa nota accred IVA differita NON a storno fattura
              !mi_tipoivaed = "F"
              !mi_ieddatregr = !mi_datreg
              !mi_iednumregr = !mi_numreg
              !mi_iedrigaregr = 1 ' la riga del cliente, sempre")
            End If
          Else ' vecchio sistema (S)
            !mi_tipoivaed = " "
            !mi_ieddatregr = DBNull.Value
            !mi_iednumregr = 0
            !mi_iedrigaregr = 0
          End If
        End With    ' With dttMO.Rows(dttMO.Rows.Count - 1)
      End If  ' fine reg. record moviva

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
      dttAnagra.Clear()
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function AggScaden(ByRef dtrTm As DataRow, ByVal strDatreg As String, ByVal lNumreg As Integer, ByVal nCausale As Integer, _
                                        ByVal strDatdoc As String, ByVal lNumdoc As Integer, ByVal strAlfdoc As String, _
                                        ByVal nAnnpar As Integer, ByVal strAlfpar As String, ByVal lNumpar As Integer, _
                                        ByVal strDarave As String) As Boolean
    'se dResiduo <> 0 sono in nota accred a storno fattura:
    'sicuramente omaggi, abbuoni e pagato della nota di accred sono = 0 e la valuta = 0
    Dim lConto1 As Integer
    Dim strAnscaden1 As String = ""
    Dim dttAnag As New DataTable
    Dim dttTmp As New DataTable
    Dim nCodpaga As Integer = 0
    Dim nCodbanc As Integer = 0
    Dim strTippaga As String = ""
    Dim nDecpaga As Integer = 0
    Dim nCodcag As Integer = 0
    Dim lAbi As Integer = 0
    Dim lCab As Integer = 0
    Dim strBanc1 As String = ""
    Dim strBanc2 As String = ""
    Dim strRifriba As String = ""
    Dim strPrefiban As String = ""
    Dim strCin As String = ""
    Dim strIban As String = ""
    Dim strSwift As String = ""
    Dim strTiporid As String = ""
    Dim strDtmandrid As String = ""
    Dim strIdmandrid As String = ""
    Dim strTiposeqrid As String = oCldPnfa.GetSettingBus("OPZIONI", ".", ".", "Tipo_scadenza_RID", "RCUR", " ", "RCUR").ToUpper
    Dim nMaxrata As Integer = 0

    Dim P As CLELBMENU.ParamCalcScad = New CLELBMENU.ParamCalcScad
    Dim nTiprata(60) As Integer ' integra paramcalcscad...
    Dim nRate As Integer = 0
    Dim strErr As String = ""
    Dim i As Integer = 0
    Dim lProt As Integer = 0
    Dim strDescr As String = ""
    Dim nCodstpg As Integer = 0

    Try
      Select Case strTiposeqrid
        Case "FIRST" : strTiposeqrid = "P"
        Case "RCUR" : strTiposeqrid = "R"
        Case "FINAL" : strTiposeqrid = "U"
        Case "OOFF" : strTiposeqrid = " "
        Case Else : strTiposeqrid = "R"
      End Select
      ' trova il conto della prima riga ...
      lConto1 = RitornaContoPrimaRiga(NTSCInt(dtrTm!tm_conto), dtrTm, strAnscaden1)
      ' test se il conto cliente/fornitore est gestito a scadenze ..
      oCldPnfa.ValCodiceDb(lConto1.ToString, strDittaCorrente, "ANAGRA", "N", "", dttAnag)
      If dttAnag.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128771109709410000, "Codice cliente '|" & NTSCInt(dtrTm!tm_conto).ToString & "|' inesistente" & _
                                                 " sul documento |" & strEstremidoc & "|. Generazione effetti non possibile."), True)
        Return False
      End If

      If dttAnag.Rows(0)!an_scaden.ToString <> "S" Then
        'solo in mancanza del modulo di CG: se il cliente non è gestito a partite e scadenze
        'avviso che non sarà possibile generare gli effetti
        If bModuliAcquistati = True Then
        Else
          LogWrite(oApp.Tr(Me, 128771058913034000, "Per il cliente |" & _
                  dttAnag.Rows(0)!an_descr1.ToString & " (" & lConto1 & ")|" & vbCrLf & _
                  "sul documento |" & strEstremidoc & _
                  "| non è possibile generare gli effetti in quanto il conto non è gestito a partite e scadenze."), True)
          lRkfatt = lRkfatt - 1
        End If
        Return True
      End If
      ' legge il pagamento
      nCodpaga = NTSCInt(dtrTm!tm_codpaga)
      nCodbanc = NTSCInt(dttAnag.Rows(0)!an_codbanc)

      oCldPnfa.ValCodiceDb(nCodpaga.ToString, strDittaCorrente, "TABPAGA", "N", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128771054825210000, _
                        "Codice pagamento '|" & nCodpaga.ToString & "|' inesistente" & _
                        " sul documento |" & strEstremidoc & "|. Generazione effetti non possibile."), True)
        Return False
      End If
      strTippaga = dttTmp.Rows(0)!tb_tippaga.ToString
      nDecpaga = NTSCInt(dttTmp.Rows(0)!tb_decpaga)
      nCodstpg = NTSCInt(dttTmp.Rows(0)!tb_codstpg)
      dttTmp.Clear()

      'prelevarli dal documento ....
      If strPrgParent <> "BNPAPNPA" Then nCodcag = NTSCInt(dtrTm!tm_codagen)
      lAbi = NTSCInt(dtrTm!tm_abi)
      lCab = NTSCInt(dtrTm!tm_cab)
      strBanc1 = NTSCStr(dtrTm!tm_banc1)
      strBanc2 = NTSCStr(dtrTm!tm_banc2)

      If bRidCodClieDebTurnoDest = False Then
        strDescr = ""
      Else
        strDescr = oCldPnfa.DescrizioneScadenza(strDittaCorrente, NTSCInt(dtrTm!tm_coddest), NTSCInt(dtrTm!tm_conto))
      End If

      If bGestPVR And NTSCStr(dtrTm!tm_andescr2) <> "" And strTippaga = "4" And _
        (dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "A" Or dtrTm!tm_tipork.ToString = "D") Then
        strRifriba = NTSCStr(dtrTm!tm_andescr2) & NTSCStr(dtrTm!tm_anindir)
        strPrefiban = NTSCStr(dttAnag.Rows(0)!an_prefiban)
        strCin = NTSCStr(dttAnag.Rows(0)!an_cin)
        strIban = NTSCStr(dttAnag.Rows(0)!an_iban)
        strSwift = NTSCStr(dttAnag.Rows(0)!an_swift)
        'strTiposeqrid = " "
      Else
        'prima verifico se la banca sul documento è uguale a quella in anagra
        If lAbi = NTSCInt(dttAnag.Rows(0)("an_abi")) And lCab = NTSCInt(dttAnag.Rows(0)("an_cab")) Then
          strRifriba = NTSCStr(dttAnag.Rows(0)("an_rifriba"))
          strPrefiban = NTSCStr(dttAnag.Rows(0)!an_prefiban)
          strCin = NTSCStr(dttAnag.Rows(0)!an_cin)
          strIban = NTSCStr(dttAnag.Rows(0)!an_iban)
          strSwift = NTSCStr(dttAnag.Rows(0)!an_swift)
          'strTiposeqrid = " "
        Else
          'se non lo è, cerco gli estremi nella tabella CLIBANC
          If oCldPnfa.GetRifribaFromClibanc(strDittaCorrente, NTSCInt(dtrTm!tm_conto), lAbi, lCab, dttTmp) Then
            If dttTmp.Rows.Count > 0 Then
              strRifriba = NTSCStr(dttTmp.Rows(0)!cba_rifriba)
              strPrefiban = NTSCStr(dttTmp.Rows(0)!cba_prefiban)
              strCin = NTSCStr(dttTmp.Rows(0)!cba_cin)
              strIban = NTSCStr(dttTmp.Rows(0)!cba_iban)
              strSwift = NTSCStr(dttTmp.Rows(0)!cba_swift)
              'strTiposeqrid = " "
              dttTmp.Clear()
            End If
          End If
        End If
      End If    'If bGestPVR And NTSCStr(dtrTm!tm_andescr2) <> "" And strTippaga = "4" And _

      strTiporid = NTSCStr(dttAnag.Rows(0)!an_tiporid)
      strDtmandrid = NTSCStr(dttAnag.Rows(0)!an_dtmandrid)
      strIdmandrid = NTSCStr(dttAnag.Rows(0)!an_idmandrid)
      '--------------------------------------------------------------------------------------------------------------
      '--- Se è attiva l'opzione di registro: OPZIONI/RIDSepaTipoInvio (default NON attiva)
      '--- ridetermina il strTiposeqrid
      '--- Se esiste almeno una scadenza con stessi:
      '--- Conto;
      '--- Codice Abi;
      '--- ID-mandato;
      '--- il campo "Tipo Scadenza RID" sarà impostato a "RCUR", altrimenti a "FIRST"
      '--------------------------------------------------------------------------------------------------------------
      Dim bRIDSepaTipoInvio As Boolean = CBool(oCldPnfa.GetSettingBus("OPZIONI", ".", ".", "RIDSepaTipoInvio", "0", " ", "0"))
      If bRIDSepaTipoInvio Then strTiposeqrid = oCldPnfa.ImpostaTiposeqrid(strDittaCorrente, lConto1, lAbi, strIdmandrid)
      '--------------------------------------------------------------------------------------------------------------
      nMaxrata = 0
      ' se c'e ;a cont. dell'incasso allora reg. prima una scadenza di incasso
      If (NTSCDec(dtrTm!tm_pagato) <> 0 Or NTSCDec(dtrTm!tm_pagato2) <> 0 Or NTSCDec(dtrTm!tm_abbuono) <> 0 Or NTSCDec(dtrTm!tm_resto) <> 0) And NTSCStr(dtrTm!tm_tipork) <> "3" Then
        nMaxrata = nMaxrata + 1
        dttSC.Rows.Add(dttSC.NewRow)
        With dttSC.Rows(dttSC.Rows.Count - 1)
          !codditt = strDittaCorrente
          !sc_conto = dtrTm!tm_conto
          !sc_annpar = nAnnpar
          !sc_alfpar = strAlfpar
          !sc_numpar = lNumpar
          !sc_numrata = nMaxrata
          !sc_causale = nCausale
          !sc_datdoc = NTSCDate(strDatdoc)
          !sc_numdoc = lNumdoc
          !sc_alfdoc = strAlfdoc
          !sc_datsca = NTSCDate(strDatreg)
          !sc_importoda = ArrDbl(NTSCDec(dtrTm!tm_pagato) + NTSCDec(dtrTm!tm_pagato2) + NTSCDec(dtrTm!tm_abbuono) - NTSCDec(dtrTm!tm_resto), oCldPnfa.TrovaNdec(0))
          !sc_impvalda = ArrDbl((NTSCDec(dtrTm!tm_pagatov) + NTSCDec(dtrTm!tm_abbuonov)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))  '-- non esiste incasso e abbuono in valuta
          If strDarave = "A" Then
            !sc_impval = ArrDbl((NTSCDec(dtrTm!tm_pagatov) + NTSCDec(dtrTm!tm_abbuonov)) * -1, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
            !sc_importo = ArrDbl((NTSCDec(dtrTm!tm_pagato) + NTSCDec(dtrTm!tm_pagato2) + NTSCDec(dtrTm!tm_abbuono) - NTSCDec(dtrTm!tm_resto)) * -1, oCldPnfa.TrovaNdec(0))
          Else
            !sc_impval = ArrDbl((NTSCDec(dtrTm!tm_pagatov) + NTSCDec(dtrTm!tm_abbuonov)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
            !sc_importo = ArrDbl(NTSCDec(dtrTm!tm_pagato) + NTSCDec(dtrTm!tm_pagato2) + NTSCDec(dtrTm!tm_abbuono) - NTSCDec(dtrTm!tm_resto), oCldPnfa.TrovaNdec(0))
          End If
          !sc_descr = strDescr
          !sc_controp = "0"
          !sc_abi = lAbi
          !sc_cab = lCab
          !sc_codcage = nCodcag
          !sc_numcc = strRifriba
          !sc_cin = strCin
          !sc_prefiban = strPrefiban
          !sc_iban = strIban
          !sc_swift = strSwift
          !sc_tiporid = strTiporid
          If strDtmandrid <> "" Then !sc_dtmandrid = NTSCDate(strDtmandrid)
          !sc_idmandrid = strIdmandrid
          !sc_tiposeqrid = strTiposeqrid
          If bRIDSepaTipoInvio Then strTiposeqrid = "R" 'Se è presente l'opzione di registro per determinare il tipo, allora dopo averlo impostato la prima volta, le successive sarà sempre ricorrente
          If strPrgParent <> "BNPAPNPA" Then
            !sc_fldis = IIf(bAutorizzato = False And (dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "K"), dtrTm!tm_autpag, "N").ToString
          Else
            !sc_fldis = "N"
          End If
          !sc_dtdist = DBNull.Value
          ' se reg. anche incasso la dichiara addirittura saldata....
          If bIncassi Then
            !sc_flsaldato = "S"
            !sc_dtsaldato = NTSCDate(strDatreg) ' gia' saldata con reg. successiva
            ' registrazione successiva o seconda reg. successiva nella data
            If (NTSCDec(dtrTm!tm_totomag) > 0) Then
              !sc_rgsaldato = lNumreg + 2
            Else
              !sc_rgsaldato = lNumreg + 1
            End If
          Else
            !sc_flsaldato = "N"
            !sc_dtsaldato = DBNull.Value
            !sc_rgsaldato = 0
          End If
          !sc_datreg = NTSCDate(strDatreg)
          !sc_numreg = lNumreg
          !sc_opdist = ""
          !sc_codbanc = IIf(NTSCInt(dtrTm!tm_codbanc) <> 0, dtrTm!tm_codbanc, nCodbanc)
          !sc_codpaga = dtrTm!tm_codpaga
          !sc_codvalu = NTSCInt(dtrTm!tm_valuta)
          !sc_cambio = dtrTm!tm_cambio
          If strPrgParent = "BNPAPNPA" Then
            !sc_impfat = NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut)
          Else
            !sc_impfat = dtrTm!tm_totdoc
          End If
          !sc_tippaga = "4" ' contanti per default ....
          !sc_darave = strDarave
          !sc_coddest = 0 '### qui intervenire se del caso..
          !sc_insolu = "N"
          !sc_sollec = 0
          !sc_flsta = "N"
          !sc_salcon = IIf(NTSCStr(dtrTm!tm_tipork) = "3", "S", " ")  'distinguo se è una notula
          !sc_banc1 = strBanc1
          !sc_banc2 = strBanc2
          !sc_bolli = "0"
          !sc_speins = "0"
          !sc_anneff = "0"
          !sc_numeff = "0"
          !sc_anndist = "0"
          !sc_numdist = "0"
          !sc_integr = IIf(bInt, "S", "N").ToString
          !sc_numprot = lNewnumprot ' dtrTm!tm_numprot
          !sc_alfpro = strAlfpro
          If bModuliAcquistati = True Then
          Else
            !sc_controp = dtrTm!tm_conto
            !sc_conto = lConEff
          End If
          !sc_commeca = dtrTm!tm_commeca
          !sc_subcommeca = dtrTm!tm_subcommeca
          !sc_tipcvs = " "

          !sc_codstpg = nCodstpg
          !sc_dtprevip = !sc_datsca
          !sc_datscadorig = !sc_datsca

          !sc_cup = dtrTm!tm_cup
          !sc_cig = dtrTm!tm_cig
          !sc_riferimpa = dtrTm!tm_riferimpa
        End With    'With dttSC.Rows(dttSC.Rows.Count - 1)
      End If    'If (NTSCDec(dtrTm!tm_pagato) <> 0 Or NTSCDec(dtrTm!tm_abbuono) <> 0) Then

      If NTSCStr(dtrTm!tm_tipork) = "B" And bContIncDDT Then
        'se sto contabilizzando un DDT emesso e devo contabilizzare solo la parte incassata (come acconto) esco
        GoTo saltaratefinali
      End If

      ' se c'e omaggi allora reg. prima/seconda una scadenza di incasso
      ' no per le riemissioni, ove non ci possono essere omaggi!!!
      If (NTSCDec(dtrTm!tm_totomag) > 0) And Not (dtrTm!tm_tipork.ToString = "I") Then
        nMaxrata = nMaxrata + 1
        dttSC.Rows.Add(dttSC.NewRow)
        With dttSC.Rows(dttSC.Rows.Count - 1)
          !codditt = strDittaCorrente
          !sc_conto = dtrTm!tm_conto
          !sc_annpar = nAnnpar
          !sc_alfpar = strAlfpar
          !sc_numpar = lNumpar
          !sc_numrata = nMaxrata
          !sc_causale = nCausale
          !sc_datdoc = NTSCDate(strDatdoc)
          !sc_numdoc = lNumdoc
          !sc_alfdoc = strAlfdoc
          !sc_datsca = NTSCDate(strDatreg)
          !sc_importoda = dtrTm!tm_totomag
          !sc_impvalda = ArrDbl((NTSCDec(dtrTm!tm_totomagv)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))   '-- non esiste incasso e abbuono in valuta
          If strDarave = "A" Then
            !sc_impval = ArrDbl((NTSCDec(dtrTm!tm_totomagv)) * -1, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
            !sc_importo = ArrDbl((NTSCDec(dtrTm!tm_totomag)) * -1, oCldPnfa.TrovaNdec(0))
          Else
            !sc_impval = ArrDbl((NTSCDec(dtrTm!tm_totomagv)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
            !sc_importo = dtrTm!tm_totomag
          End If
          !sc_descr = strDescr
          !sc_controp = "0"
          !sc_abi = lAbi
          !sc_cab = lCab
          !sc_codcage = nCodcag
          !sc_numcc = strRifriba
          !sc_cin = strCin
          !sc_prefiban = strPrefiban
          !sc_iban = strIban
          !sc_swift = strSwift
          !sc_tiporid = strTiporid
          If strDtmandrid <> "" Then !sc_dtmandrid = NTSCDate(strDtmandrid)
          !sc_idmandrid = strIdmandrid
          !sc_tiposeqrid = strTiposeqrid
          If bRIDSepaTipoInvio Then strTiposeqrid = "R" 'Se è presente l'opzione di registro per determinare il tipo, allora dopo averlo impostato la prima volta, le successive sarà sempre ricorrente
          !sc_flsaldato = "S"
          If strPrgParent <> "BNPAPNPA" Then
            !sc_fldis = IIf(bAutorizzato = False And (dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "K"), dtrTm!tm_autpag, "N").ToString
          Else
            !sc_fldis = "N"
          End If
          !sc_dtdist = DBNull.Value
          !sc_dtsaldato = NTSCDate(strDatreg) ' gia' saldata con reg. successiva
          ' registrazione successiva
          !sc_rgsaldato = lNumreg + 1
          !sc_datreg = NTSCDate(strDatreg)
          !sc_numreg = lNumreg
          !sc_opdist = ""
          !sc_codbanc = IIf(NTSCInt(dtrTm!tm_codbanc) <> 0, dtrTm!tm_codbanc, nCodbanc)
          !sc_codpaga = 0 'dtrTm!tm_codpaga SUGLI OMAGGIL IL COD PAGAMENTO DEVE ESSERE 0, DIVERSAMENTE NON LAVORANO BENE LE PROVVIGIONI!!!!
          !sc_codvalu = NTSCInt(dtrTm!tm_valuta)
          !sc_cambio = dtrTm!tm_cambio
          If strPrgParent = "BNPAPNPA" Then
            !sc_impfat = NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut)
          Else
            !sc_impfat = dtrTm!tm_totdoc
          End If
          !sc_tippaga = "4" 'strTippaga
          !sc_darave = strDarave
          !sc_coddest = 0 '### qui intervenire se del caso..
          !sc_insolu = "N"
          !sc_sollec = 0
          !sc_flsta = "N"
          !sc_salcon = " "
          !sc_banc1 = strBanc1
          !sc_banc2 = strBanc2
          !sc_bolli = "0"
          !sc_speins = "0"
          !sc_anneff = "0"
          !sc_numeff = "0"
          !sc_anndist = "0"
          !sc_numdist = "0"
          !sc_integr = IIf(bInt, "S", "N").ToString
          !sc_numprot = lNewnumprot 'dtrTm!tm_numprot
          !sc_alfpro = strAlfpro

          If bModuliAcquistati = True Then
          Else
            !sc_controp = dtrTm!tm_conto
            !sc_conto = lConEff
          End If
          !sc_commeca = dtrTm!tm_commeca
          !sc_subcommeca = dtrTm!tm_subcommeca
          !sc_tipcvs = " "

          !sc_codstpg = nCodstpg
          !sc_dtprevip = !sc_datsca
          !sc_datscadorig = !sc_datsca

          !sc_cup = dtrTm!tm_cup
          !sc_cig = dtrTm!tm_cig
          !sc_riferimpa = dtrTm!tm_riferimpa
        End With    'With dttSC.Rows(dttSC.Rows.Count - 1)
      End If    'If (NTSCDec(dtrTm!tm_totomag) > 0) And Not (dtrTm!tm_tipork.ToString = "I") Then

      ' eccezione per rf parzialmete incassate : salta le rate finali
      If dtrTm!tm_tipork.ToString = "F" And bParzinc Then GoTo saltaratefinali

      ' calcola rate residue (o tutte le rate )
      P.nCodpaga = NTSCInt(dtrTm!tm_codpaga)
      If nDecpaga = 3 Or nDecpaga = 4 Then
        If NTSCStr(dtrTm!tm_datapag) = "" Then
          LogWrite(oApp.Tr(Me, 128771088029698000, _
                "Il documento |" & strEstremidoc & "|. possiede un codice pagamento con 'Tipo scadenza' " & _
                "uguale a 'Data bolla' o 'Data diversa' e non è indicata sul documento la 'Data pagamento' di riferimento." & _
                "Si procede al calcolo della scadenza con la data documento."), True)
          P.strDatrif = NTSCDate(dtrTm!tm_datdoc).ToShortDateString
        Else
          P.strDatrif = NTSCDate(dtrTm!tm_datapag).ToShortDateString
        End If
      Else
        If (dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "(") And Not (NTSCStr(dtrTm!tm_datpar) = "") Then
          P.strDatrif = NTSCDate(dtrTm!tm_datpar).ToShortDateString
        Else
          P.strDatrif = NTSCDate(dtrTm!tm_datdoc).ToShortDateString
        End If
      End If

      If strPrgParent = "BNPAPNPA" Then
        'parcellazione / ritenute
        P.dTotfat = ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut) - NTSCDec(dtrTm!tm_altriprev), oCldPnfa.TrovaNdec(0))
        If (NTSCDec(dtrTm!tm_pagato) <> 0 Or NTSCDec(dtrTm!tm_abbuono) <> 0) Then
          P.dTotfat = ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_pagato) - NTSCDec(dtrTm!tm_abbuono) - NTSCDec(dtrTm!tm_ritenut) - NTSCDec(dtrTm!tm_altriprev), oCldPnfa.TrovaNdec(0))
        End If
        P.dSpese = ArrDbl(NTSCDec(dtrTm!tm_bolli) + NTSCDec(dtrTm!tm_speinc), oCldPnfa.TrovaNdec(0))

      Else
        P.dTotfat = ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_totomag), oCldPnfa.TrovaNdec(0))
        If (NTSCDec(dtrTm!tm_pagato) <> 0 Or NTSCDec(dtrTm!tm_pagato2) <> 0 Or NTSCDec(dtrTm!tm_abbuono) <> 0) Then
          P.dTotfat = ArrDbl(NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_totomag) - NTSCDec(dtrTm!tm_pagato) - NTSCDec(dtrTm!tm_pagato2) - NTSCDec(dtrTm!tm_abbuono) + NTSCDec(dtrTm!tm_resto), oCldPnfa.TrovaNdec(0))
        End If

        'p.dTotfatval = dtrTm!tm_totdocv ' migliorare ... passando anche importo in valuta
        P.dTotfatval = ArrDbl(NTSCDec(dtrTm!tm_totdocv) - NTSCDec(dtrTm!tm_totomagv), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
        If (NTSCDec(dtrTm!tm_pagatov) <> 0 Or NTSCDec(dtrTm!tm_abbuonov) <> 0) Then
          P.dTotfatval = ArrDbl(NTSCDec(dtrTm!tm_totdocv) - NTSCDec(dtrTm!tm_totomagv) - NTSCDec(dtrTm!tm_pagatov) - NTSCDec(dtrTm!tm_abbuonov), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
        End If

        ' fatt. acqusiti cee o reverse charge
        If (dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "(") And (nCodntra <> 0 Or strFattRevCharge <> "N") Then
          If dsTabciva.Tables("TABCIVA").Columns.Contains("tb_revcharge") And strFattRevCharge = "M" And nCodntra = 0 Then
            'fattura reverse charge misti da net 2013 (dove nel documento sono presenti cod. iva con flag 'reverse charge' ed altri no)
            P.dTotfatval = P.dTotfatval - dTotivavRC
          Else
            P.dTotfatval = P.dTotfatval - dTotivav
          End If
        End If

        P.dIvaval = ArrDbl(NTSCDec(dtrTm!tm_impostav_1) + NTSCDec(dtrTm!tm_impostav_2) + NTSCDec(dtrTm!tm_impostav_3) + _
                   NTSCDec(dtrTm!tm_impostav_4) + NTSCDec(dtrTm!tm_impostav_5) + NTSCDec(dtrTm!tm_impostav_6) + _
                   NTSCDec(dtrTm!tm_impostav_7) + NTSCDec(dtrTm!tm_impostav_8), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
        P.dSpese = ArrDbl(NTSCDec(dtrTm!tm_bolli) + NTSCDec(dtrTm!tm_speinc) + NTSCDec(dtrTm!tm_speacc) + NTSCDec(dtrTm!tm_speimb), oCldPnfa.TrovaNdec(0))
        P.dSpeseval = NTSCDec(dtrTm!tm_speaccv) + NTSCDec(dtrTm!tm_speimbv) + NTSCDec(dtrTm!tm_speincv) + NTSCDec(dtrTm!tm_bolliv)

      End If    'If strPrgParent = "BNPAPNPA" Then

      ' fatt. acq. cee o reverse charge
      If (dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "(") And (nCodntra <> 0 Or strFattRevCharge <> "N") Then
        If dsTabciva.Tables("TABCIVA").Columns.Contains("tb_revcharge") And strFattRevCharge = "M" And nCodntra = 0 Then
          'fattura reverse charge misti da net 2013 (dove nel documento sono presenti cod. iva con flag 'reverse charge' ed altri no)
          P.dTotfat = P.dTotfat - dTotivaRC
        Else
          P.dTotfat = P.dTotfat - dTotiva
        End If
      End If

      If P.dTotfat = 0 Then Return True ' no more rate da costruire...(doc. tutto pagato o abbuonato)

      P.dIva = ArrDbl(NTSCDec(dtrTm!tm_imposta_1) + NTSCDec(dtrTm!tm_imposta_2) + NTSCDec(dtrTm!tm_imposta_3) + _
                      NTSCDec(dtrTm!tm_imposta_4) + NTSCDec(dtrTm!tm_imposta_5) + NTSCDec(dtrTm!tm_imposta_6) + _
                      NTSCDec(dtrTm!tm_imposta_7) + NTSCDec(dtrTm!tm_imposta_8), oCldPnfa.TrovaNdec(0))
      '-- pers ceas : solo le prime 4 aliquote, visto che dalla 5 alla 8 sono usate come informativa .. vedi bevefadi.vb
      If bCeas Then
        P.dIva = ArrDbl(P.dIva - NTSCDec(dtrTm!tm_imposta_5) - NTSCDec(dtrTm!tm_imposta_6) - NTSCDec(dtrTm!tm_imposta_7) - NTSCDec(dtrTm!tm_imposta_8), oCldPnfa.TrovaNdec(0))
      End If
      '--
      If oCldPnfa.CheckCausaleSplitPaymentFromTpbf(strDittaCorrente, NTSCInt(dtrTm!tm_tipobf)) Then
        'se la causale di CG è di tipo split payment (nuova iva differita con iva che verrà versata direttamente dall'ente pubblico)
        'nel calcolo delle scadenze non devo tener conto dell'IVA
        'NON E' GESTITO IL CASO DI FATTURE EMESSE IN VALUTA AD ENTE PUBBLICO
        P.dTotfat -= P.dIva
        P.dIva = 0
      End If

      ' controlla se forzato o meno ilcastelletto pagaemnti ..
      If CBool(NTSCInt(dtrTm!tm_ccambiati) And bsVEBOLLmodifCastScad) Then
        If NTSCInt(dtrTm!tm_tippaga_1) <> 0 Then
          nRate = 1
          P.strDatsca(1) = NTSCStr(dtrTm!tm_datsca_1)
          P.dImpsca(1) = NTSCDec(dtrTm!tm_impsca_1)
          P.dImpscaval(1) = NTSCDec(dtrTm!tm_impscav_1)
          nTiprata(1) = NTSCInt(dtrTm!tm_tippaga_1)
        Else
          GoTo Prosegui
        End If
        If NTSCInt(dtrTm!tm_tippaga_2) <> 0 Then
          nRate = 2
          P.strDatsca(2) = NTSCStr(dtrTm!tm_datsca_2)
          P.dImpsca(2) = NTSCDec(dtrTm!tm_impsca_2)
          P.dImpscaval(2) = NTSCDec(dtrTm!tm_impscav_2)
          nTiprata(2) = NTSCInt(dtrTm!tm_tippaga_2)
        Else
          GoTo Prosegui
        End If
        If NTSCInt(dtrTm!tm_tippaga_3) <> 0 Then
          nRate = 3
          P.strDatsca(3) = NTSCStr(dtrTm!tm_datsca_3)
          P.dImpsca(3) = NTSCDec(dtrTm!tm_impsca_3)
          P.dImpscaval(3) = NTSCDec(dtrTm!tm_impscav_3)
          nTiprata(3) = NTSCInt(dtrTm!tm_tippaga_3)
        Else
          GoTo Prosegui
        End If
        If NTSCInt(dtrTm!tm_tippaga_4) <> 0 Then
          nRate = 4
          P.strDatsca(4) = NTSCStr(dtrTm!tm_datsca_4)
          P.dImpsca(4) = NTSCDec(dtrTm!tm_impsca_4)
          P.dImpscaval(4) = NTSCDec(dtrTm!tm_impscav_4)
          nTiprata(4) = NTSCInt(dtrTm!tm_tippaga_4)
        Else
          GoTo Prosegui
        End If
        If NTSCInt(dtrTm!tm_tippaga_5) <> 0 Then
          nRate = 5
          P.strDatsca(5) = NTSCStr(dtrTm!tm_datsca_5)
          P.dImpsca(5) = NTSCDec(dtrTm!tm_impsca_5)
          P.dImpscaval(5) = NTSCDec(dtrTm!tm_impscav_5)
          nTiprata(5) = NTSCInt(dtrTm!tm_tippaga_5)
        Else
          GoTo Prosegui
        End If
        If NTSCInt(dtrTm!tm_tippaga_6) <> 0 Then
          nRate = 6
          P.strDatsca(6) = NTSCStr(dtrTm!tm_datsca_6)
          P.dImpsca(6) = NTSCDec(dtrTm!tm_impsca_6)
          P.dImpscaval(6) = NTSCDec(dtrTm!tm_impscav_6)
          nTiprata(6) = NTSCInt(dtrTm!tm_tippaga_6)
        Else
          GoTo Prosegui
        End If
        If NTSCInt(dtrTm!tm_tippaga_7) <> 0 Then
          nRate = 7
          P.strDatsca(7) = NTSCStr(dtrTm!tm_datsca_7)
          P.dImpsca(7) = NTSCDec(dtrTm!tm_impsca_7)
          P.dImpscaval(7) = NTSCDec(dtrTm!tm_impscav_7)
          nTiprata(7) = NTSCInt(dtrTm!tm_tippaga_7)
        Else
          GoTo Prosegui
        End If
        If NTSCInt(dtrTm!tm_tippaga_8) <> 0 Then
          nRate = 8
          P.strDatsca(8) = NTSCStr(dtrTm!tm_datsca_8)
          P.dImpsca(8) = NTSCDec(dtrTm!tm_impsca_8)
          P.dImpscaval(8) = NTSCDec(dtrTm!tm_impscav_8)
          nTiprata(8) = NTSCInt(dtrTm!tm_tippaga_8)
        Else
          GoTo Prosegui
        End If
      Else
        strErr = ""
        nRate = CType(oCleComm, CLELBMENU).CalcolaScad(strDittaCorrente, P.nCodpaga, P.strDatrif, P.dTotfat, P.dTotfatval, P.dIva, _
                    P.dIvaval, P.dSpese, P.dSpeseval, P.strDatsca, P.dImpsca, P.dImpscaval, nTiprata, strErr, NTSCInt(dtrTm!tm_valuta), NTSCInt(dtrTm!tm_conto))
        If strErr <> "" Then LogWrite(strErr, True)
      End If
Prosegui:
      If nRate < 1 Then Return True
      For i = 1 To nRate
        nMaxrata = nMaxrata + 1
        dttSC.Rows.Add(dttSC.NewRow)
        With dttSC.Rows(dttSC.Rows.Count - 1)
          !codditt = strDittaCorrente
          !sc_conto = IIf(NTSCStr(dtrTm!tm_tipork) = "3", lConClinot, dtrTm!tm_conto)
          !sc_annpar = nAnnpar
          !sc_alfpar = strAlfpar
          !sc_numpar = lNumpar
          !sc_numrata = nMaxrata
          !sc_causale = nCausale
          !sc_datdoc = NTSCDate(strDatdoc)
          !sc_numdoc = lNumdoc
          !sc_alfdoc = strAlfdoc
          !sc_datsca = NTSCDate(P.strDatsca(i))
          !sc_importoda = P.dImpsca(i)
          !sc_impvalda = P.dImpscaval(i)
          If strDarave = "A" Then
            !sc_impval = P.dImpscaval(i) * -1
            !sc_importo = P.dImpsca(i) * -1
          Else
            !sc_impval = P.dImpscaval(i)
            !sc_importo = P.dImpsca(i)
          End If
          !sc_descr = strDescr
          !sc_controp = 0
          If strPrgParent = "BNPAPNPA" Then !sc_controp = dtrTm!tm_conto
          !sc_abi = lAbi
          !sc_cab = lCab
          !sc_codcage = nCodcag
          !sc_numcc = strRifriba
          !sc_cin = strCin
          !sc_prefiban = strPrefiban
          !sc_iban = strIban
          !sc_swift = strSwift
          !sc_tiporid = strTiporid
          If strDtmandrid <> "" Then !sc_dtmandrid = NTSCDate(strDtmandrid)
          !sc_idmandrid = strIdmandrid
          !sc_tiposeqrid = strTiposeqrid
          If bRIDSepaTipoInvio Then strTiposeqrid = "R" 'Se è presente l'opzione di registro per determinare il tipo, allora dopo averlo impostato la prima volta, le successive sarà sempre ricorrente
          !sc_flsaldato = "N"
          If strPrgParent <> "BNPAPNPA" Then
            !sc_fldis = IIf(bAutorizzato = False And (dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "K"), dtrTm!tm_autpag, "N").ToString
          Else
            !sc_fldis = "N"
          End If
          !sc_dtdist = DBNull.Value
          !sc_dtsaldato = DBNull.Value
          !sc_rgsaldato = "0"
          !sc_datreg = NTSCDate(strDatreg)
          !sc_numreg = lNumreg
          !sc_opdist = ""
          !sc_codbanc = IIf(NTSCInt(dtrTm!tm_codbanc) <> 0, dtrTm!tm_codbanc, nCodbanc)
          !sc_codpaga = dtrTm!tm_codpaga
          !sc_codvalu = NTSCInt(dtrTm!tm_valuta)
          !sc_cambio = dtrTm!tm_cambio
          If strPrgParent = "BNPAPNPA" Then
            !sc_impfat = NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_ritenut)
          Else
            !sc_impfat = dtrTm!tm_totdoc
          End If
          !sc_tippaga = nTiprata(i) '!!!!!
          !sc_darave = strDarave
          !sc_coddest = 0 '### qui intervenire se del caso..
          !sc_insolu = "N"
          !sc_sollec = 0

          ' setta i campi di griglia
          !sc_flsta = "N"
          !sc_salcon = IIf(NTSCStr(dtrTm!tm_tipork) = "3", "S", " ")  'distinguo se è una notula
          !sc_banc1 = strBanc1
          !sc_banc2 = strBanc2
          !sc_bolli = "0"
          !sc_speins = "0"
          !sc_anneff = "0"
          !sc_numeff = "0"
          !sc_anndist = "0"
          !sc_numdist = "0"
          !sc_numprot = lNewnumprot 'dtrTm!tm_numprot
          !sc_alfpro = strAlfpro
          !sc_integr = IIf(bInt, "S", "N").ToString

          ' se manca la CG allora .. lo registra come già emessi gli effetti
          If bModuliAcquistati = True Then
          Else
            !sc_controp = dtrTm!tm_conto
            !sc_conto = lConEff
            ' attibuisce n. effetto
            strErr = ""
            lProt = oCldPnfa.LegNuma(strDittaCorrente, "EA", strTippaga, nEscomp, True)
            lProt = oCldPnfa.AggNuma(strDittaCorrente, "EA", strTippaga, nEscomp, lProt, True, True, strErr)
            If strErr <> "" Then LogWrite(strErr, True)
            !sc_anneff = nEscomp
            !sc_numeff = lProt
          End If
          !sc_commeca = dtrTm!tm_commeca
          !sc_subcommeca = dtrTm!tm_subcommeca
          !sc_tipcvs = " "

          !sc_codstpg = nCodstpg
          !sc_dtprevip = !sc_datsca
          !sc_datscadorig = !sc_datsca

          !sc_cup = dtrTm!tm_cup
          !sc_cig = dtrTm!tm_cig
          !sc_riferimpa = dtrTm!tm_riferimpa
        End With    'With dttSC.Rows(dttSC.Rows.Count - 1)
      Next    'For i = 1 To nRate
saltaratefinali:

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
      dttAnag.Clear()
    End Try
  End Function

  Public Overridable Function WriPriana(ByRef dtrTm As DataRow, ByVal lContocg As Integer, ByVal strDarave As String, _
                                        ByVal dImporto As Decimal, ByVal nControp As Integer, _
                                        ByVal dImpVal As Decimal, ByVal dIvaInded As Decimal, _
                                        ByVal dIvaIndedVal As Decimal, ByVal dDtIniz As String, _
                                        ByVal dDtFin As String, ByVal bIvaInded As Boolean, _
                                        ByRef dtrPN As DataRow) As Boolean
    Try
      'obsoleta
      Return WriPriana(dtrTm, lContocg, strDarave, dImporto, nControp, dImpVal, dIvaInded, _
                       dIvaIndedVal, dDtIniz, dDtFin, bIvaInded, dtrPN, Nothing)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function WriPriana(ByRef dtrTm As DataRow, ByVal lContocg As Integer, ByVal strDarave As String, _
                                        ByVal dImporto As Decimal, ByVal nControp As Integer, _
                                        ByVal dImpVal As Decimal, ByVal dIvaInded As Decimal, _
                                        ByVal dIvaIndedVal As Decimal, ByVal dDtIniz As String, _
                                        ByVal dDtFin As String, ByVal bIvaInded As Boolean, _
                                        ByRef dtrPN As DataRow, ByRef dttTmOrig As DataTable) As Boolean
    'lContocg =       conto di contabilità generale
    'strDarave =      segno della registrazione
    'nControp =       codice contropartita (da testmagc o riletto dalle righe di movmag)
    'dImporto =       importo totale da ripartire tra le varie righe di ca
    'dImpval =        importo totale in valuta da ripartire tra le varie righe di ca
    'dIvaInded =      totale iva indeducibile da ripartire tra le varie righe di ca
    'dIvaIndedVal =   totale iva indeducibile in valuta da ripartire tra le varie righe di ca
    'dDtIniz =        data inizio competenza
    'dDtFin =         data fine competenza
    'bIvaInded =      true se la routine deve andare creare righe in ca relative alla sola iva indeducibile (causale 9999)
    Dim strAnFlci As String
    Dim dDiff As Decimal
    Dim lContoca As Integer
    Dim dttTmp As New DataTable
    Dim dttTmpca As New DataTable
    Dim dTotCa As Decimal = 0
    Dim dTotCav As Decimal = 0

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dtrTm, lContocg, strDarave, dImporto, nControp, dImpVal, dIvaInded, _
                       dIvaIndedVal, dDtIniz, dDtFin, bIvaInded, dtrPN, dttTmOrig})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrTm = CType(oIn(0), DataRow)
        dtrPN = CType(oIn(11), DataRow)
        Return CBool(oOut)
      End If
      '----------------

      If bCa2 Then Return True 'se c'è la nuova analitica non posso gestire anche la vecchia :)
      If nCADoc = 3 Then Return True 'tanto non contabilizzerò!!!

      oCldPnfa.ValCodiceDb(lContocg.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp)
      If dttTmp.Rows(0)!an_flci.ToString = " " Then Return True

      '-----------------------------------------------------------------------------------------------------
      ' tiro su tutte le rige da movmag a parità di contropartita vendite/acquisti (e di date competenza, se da opzione)
      ' raggruppando per contropartite, centro, linea, commessa, unità di misura, ....
      '-----------------------------------------------------------------------------------------------------
      ' **** QUERY 1 **** : linea <> null e subcommeca <> null (est sempre cosi' !!)
      ' test se fatture differite o fatture immediate
      If Not oCldPnfa.GetMovmagCA(strDittaCorrente, dtrTm!tm_tipork.ToString, NTSCInt(dtrTm!tm_anno), _
                                  dtrTm!tm_serie.ToString, NTSCInt(dtrTm!tm_numdoc), dtrTm!tm_flscdb.ToString, _
                                  bGestDatecomp, dttTmp.Rows(0)!an_accperi.ToString, nControp, dDtIniz, dDtFin, _
                                  dttTmpca, NTSCStr(dtrTm!xx_raggr), NTSCInt(dtrTm!xx_dtttm), dttTmOrig) Then Return False

      If dttTmpca.Rows.Count <= 1 Then
        If dttTmpca.Select("tb_testci = 'E' or tb_testci = 'B'").Length = 0 Then
          'se tutte le righe di magazzino di quella contropartita sono del tipo 'DARE/AVERE MG' (oppure non movimentano la CA) non devo contabilizzare la CA
          If nCADoc = 0 Then
            nCADoc = 1 'memorizzo che per questo documento non devo contabilizzare la CA (se ho già memorizzato delle righe di CA per priana, devo bloccare)
          Else
            nCADoc = 3
          End If
          Return True
        End If
      End If

      'cancello tutti i record che non sono dare/avere CG
      For Each dtrT As DataRow In dttTmpca.Select("NOT (tb_testci = 'E' or tb_testci = 'B')")
        dtrT.Delete()
      Next
      dttTmpca.AcceptChanges()
      ' trovato qualcosa : registra PRIANA
      If dttTmpca.Rows.Count > 0 Then
        ' cerca
        If nCADoc = 1 Then
          nCADoc = 3  'alcune righe di movmag devono andare a finire in CG
        Else
          nCADoc = 2  'alcune righe di movmag devono andare a finire in CG
        End If
        lContoca = NTSCInt(dttTmpca.Rows(0)!mm_contocontr)
        strAnFlci = oCldPnfa.RitornaAcFlciDitt(strDittaCorrente, NTSCInt(dttTmpca.Rows(0)!mm_contocontr))
        ' registra in priana
        If Not TrattaSnaCa(dtrTm, strDarave, strAnFlci, lContoca, lContocg, dImporto, dImpVal, dIvaInded, _
                           dIvaIndedVal, dDtIniz, dDtFin, bIvaInded, dttTmpca, dTotCa, dTotCav, dtrPN) Then
          Return False
        End If
      End If
      dttTmpca.Clear()

      '--- finito varie query : controlli finali
      If dTotCa = 0 And (dImporto + dIvaInded) <> 0 Then
        LogWrite(oApp.Tr(Me, 128771126699720000, "Mancano dati per aggiornamento CA sul conto |" & lContoca.ToString & _
                            "| dal conto CG |" & lContocg & "| in documento |" & strEstremidoc & "|" & vbCrLf & _
                            "- Importo C.A.: |" & dTotCa & "|" & vbCrLf & _
                            "- Importo C.G.: |" & (dImporto + dIvaInded) & "|" & vbCrLf & _
                            "Documento non contabilizzato (probabilmente manca cod. centro e/o linea e/o commessa e/o conto contropartita nelle righe del documento," & vbCrLf & _
                            "oppure nel documento sono presenti righe con causali di magazzino aventi DARE/AVERE CA non impostati tutti nello stesso modo)."), True)
        Return False
      End If
      ' controllo su totale dei moviemti
      If ArrDbl(dTotCa, oCldPnfa.TrovaNdec(0)) <> ArrDbl((dImporto + dIvaInded), oCldPnfa.TrovaNdec(0)) Then ' non so se dImporto est ok
        If Math.Abs(dTotCa - (dImporto + dIvaInded)) > lDiffMaxCgCa Then
          LogWrite(oApp.Tr(Me, 128771144396578000, _
                            "Somma dei valori nei Movimenti di CA sul conto |" & lContoca.ToString & _
                            "| (|" & dTotCa.ToString(oApp.FormatImporti) & "|) diversa da valore in CG su conto |" & lContocg & _
                            "| (|" & (dImporto + dIvaInded).ToString(oApp.FormatImporti) & "|) in documento |" & strEstremidoc & "|" & vbCrLf & _
                            "nel documento potrebbero essere presenti righe con causali di magazzino aventi DARE/AVERE CA non impostati tutti nello stesso modo (CG/MG)."), True)
          Return False
        Else ' tratta la diffrenza buttandola sull'ultima riga ...(importo inf. alle 10 lire
          ' cioè arrotondamenti ...)
          dDiff = ArrDbl((dImporto + dIvaInded) - dTotCa, oCldPnfa.TrovaNdec(0))
          If dttCA.Rows.Count > 0 Then
            With dttCA.Rows(dttCA.Rows.Count - 1)
              If !pa_darave.ToString = "D" Then
                !pa_importo = ArrDbl(NTSCDec(!pa_importo) + dDiff, oCldPnfa.TrovaNdec(0))
                !pa_importoda = ArrDbl(NTSCDec(!pa_importoda) + dDiff, oCldPnfa.TrovaNdec(0))
              Else
                !pa_importoda = ArrDbl(NTSCDec(!pa_importoda) + dDiff, oCldPnfa.TrovaNdec(0))
                !pa_importo = ArrDbl(NTSCDec(!pa_importoda) * -1, oCldPnfa.TrovaNdec(0))
              End If
            End With
          End If
        End If
      End If
      ' controllo su totale dei movimenti in valuta
      If ArrDbl(dTotCav, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta))) <> ArrDbl((dImpVal + dIvaIndedVal), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta))) Then ' non so se dImporto est ok
        If Math.Abs(dTotCav - (dImpVal + dIvaIndedVal)) > lDiffMaxCgCa Then
          LogWrite(oApp.Tr(Me, 128771146465918000, _
                            "Somma dei valori nei Movimenti in valuta di CA sul conto |" & lContoca.ToString & _
                            "| (|" & dTotCav.ToString(oApp.FormatImporti) & "|) diversa da valore in CG su conto |" & lContocg & _
                            "| (|" & (dImpVal + dIvaIndedVal).ToString(oApp.FormatImporti) & "|) in documento |" & strEstremidoc & "|"), True)
          Return False
        Else ' tratta la diffrenza buttandola sull'ultima riga ...(importo inf. alle 10 lire
          ' cioè arrotondamenti ...)
          dDiff = ArrDbl((dImpVal + dIvaIndedVal) - dTotCav, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
          If dttCA.Rows.Count > 0 Then
            With dttCA.Rows(dttCA.Rows.Count - 1)
              If !pa_darave.ToString = "D" Then
                !pa_impval = ArrDbl(NTSCDec(!pa_impval) + dDiff, oCldPnfa.TrovaNdec(0))
                !pa_impvalda = ArrDbl(NTSCDec(!pa_impvalda) + dDiff, oCldPnfa.TrovaNdec(0))
              Else
                !pa_impvalda = ArrDbl(NTSCDec(!pa_impvalda) + dDiff, oCldPnfa.TrovaNdec(0))
                !pa_impval = ArrDbl(NTSCDec(!pa_impvalda) * -1, oCldPnfa.TrovaNdec(0))
              End If
            End With
          End If
        End If
      End If

eseguinext:
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
      dttTmpca.Clear()
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function TrattaSnaCa(ByRef dtrTm As DataRow, ByVal strDarave As String, _
                                          ByVal strAnFlci As String, ByVal lContoca As Integer, _
                                          ByVal lContocg As Integer, ByVal dImporto As Decimal, _
                                          ByVal dImpVal As Decimal, ByVal dIvaInded As Decimal, _
                                          ByVal dIvaIndedVal As Decimal, ByVal dDtIniz As String, _
                                          ByVal dDtFin As String, ByVal bIvaInded As Boolean, _
                                          ByRef dttMMca As DataTable, ByRef dTotCa As Decimal, _
                                          ByRef dTotCav As Decimal, ByRef dtrPN As DataRow) As Boolean
    'lContocg =       conto di contabilità generele
    'strDarave =      segno della registrazione
    'strAnFlci =      tipo di gestione della contabilità analitica (solo a centro, centro e commessa, ....)
    'lContoca =       conto di contabilità analitica letto da movmag
    'dImporto =       importo totale da ripartire tra le varie righe di ca
    'dImpval =        importo totale in valuta da ripartire tra le varie righe di ca
    'dIvaInded =      totale iva indeducibile da ripartire tra le varie righe di ca
    'dIvaIndedVal =   totale iva indeducibile in valuta da ripartire tra le varie righe di ca
    'dDtIniz =        data inizio competenza
    'dDtFin =         data fine competenza
    'bIvaInded =      true se la routine deve andare creare righe in ca relative alla sola iva indeducibile (causale 9999)
    Dim nCodcena As Integer = 0
    Dim strCodcfam As String = " "
    Dim lCommeca As Integer = 0
    Dim strSubCommeca As String = " "
    Dim lProt As Integer = 0
    Dim dImportoCA As Decimal = 0   'importo per ogni riga di CA
    Dim dImpValCA As Decimal = 0    'importo in valuta per ogni riga di CA
    Dim dIvaInd As Decimal = 0      'iva indeducibile per ogni riga di CA
    Dim dIvaIndv As Decimal = 0     'iva indeducibile in vlauta per ogni riga di CA
    Dim strErr As String = ""

    Try

      For Each dtrMMca As DataRow In dttMMca.Rows
        lContoca = NTSCInt(dtrMMca!mm_contocontr)
        strAnFlci = oCldPnfa.RitornaAcFlciDitt(strDittaCorrente, lContoca)

        '---------------------
        ' controlli di compatibilita'
        If Not ((strDarave = "D" And dtrMMca!tb_testci.ToString = "E") Or (strDarave = "A" And dtrMMca!tb_testci.ToString = "B")) Then
          LogWrite(oApp.Tr(Me, 128771699629935000, _
                  "Movimento di C.A. con segno D/A indicato sulla causale di magazzino incompatibile su conto |" & lContoca & vbCrLf & _
                  "| in documento |" & strEstremidoc & "|."), True)
          Return False
        End If

        '---------------------
        'test sui budget
        Select Case strAnFlci
          Case "1" : nCodcena = NTSCInt(dtrMMca!mm_codcena) : strCodcfam = " " : lCommeca = 0 : strSubCommeca = " "
          Case "2" : nCodcena = NTSCInt(dtrMMca!mm_codcena) : strCodcfam = " " : lCommeca = NTSCInt(dtrMMca!mm_commeca) : strSubCommeca = NTSCStr(dtrMMca!subcommeca)
          Case "3" : nCodcena = NTSCInt(dtrMMca!mm_codcena) : strCodcfam = NTSCStr(dtrMMca!codcfam) : lCommeca = 0 : strSubCommeca = " "
          Case "4" : nCodcena = NTSCInt(dtrMMca!mm_codcena) : strCodcfam = NTSCStr(dtrMMca!codcfam) : lCommeca = NTSCInt(dtrMMca!mm_commeca) : strSubCommeca = NTSCStr(dtrMMca!subcommeca)
        End Select
        If Not oCldPnfa.EsisteBudget(strDittaCorrente, "E", lContoca, nCodcena, strCodcfam, lCommeca, strSubCommeca, nEscomp, bTestBudget) Then ' controlla se esiste il budget
          LogWrite(oApp.Tr(Me, 128771701106465000, _
                    "Budget inesistente su centro |" & nCodcena & "|, su conto C.A. |" & lContoca & vbCrLf & _
                    "| in documento |" & strEstremidoc & "|."), True)
          Return False
        End If
        If strAnFlci = "4" Or strAnFlci = "3" Then
          If Not oCldPnfa.EsisteBudget(strDittaCorrente, "L", lContoca, nCodcena, strCodcfam, lCommeca, strSubCommeca, nEscomp, bTestBudget) Then ' controlla se esiste il budget
            LogWrite(oApp.Tr(Me, 128771703101660000, _
                    "Budget inesistente su famiglia/linea |" & strCodcfam & "|, su conto C.A. |" & lContoca & vbCrLf & _
                    "| in documento |" & strEstremidoc & "|."), True)
            Return False
          End If
        End If
        If strAnFlci = "4" Or strAnFlci = "2" Then
          If Not oCldPnfa.EsisteBudget(strDittaCorrente, "C", lContoca, nCodcena, strCodcfam, lCommeca, strSubCommeca, nEscomp, bTestBudget) Then ' controlla se esiste il budget
            LogWrite(oApp.Tr(Me, 128771715917592000, _
                    "Budget inesistente su commessa |" & lCommeca & "|, sottocommessa '|" & strSubCommeca & "|', su conto C.A. |" & lContoca & vbCrLf & _
                    "| in documento |" & strEstremidoc & "|."), True)
            Return False
          End If
        End If

        '---------------------
        ' adesso ok, scrive su priana...
        dttCA.Rows.Add(dttCA.NewRow)
        With dttCA.Rows(dttCA.Rows.Count - 1)
          'trova il numero progressivo di record
          strErr = ""
          lProt = oCldPnfa.LegNuma(strDittaCorrente, "PA", " ", 0, True)
          lProt = oCldPnfa.AggNuma(strDittaCorrente, "PA", " ", 0, lProt, True, True, strErr)
          If strErr <> "" Then LogWrite(strErr, True)
          !codditt = strDittaCorrente
          !pa_progr = lProt
          !pa_escomp = nEscomp
          !pa_conto = lContoca
          !pa_codcena = nCodcena
          !pa_codcfam = strCodcfam
          !pa_commeca = lCommeca
          !pa_subcommeca = strSubCommeca
          If NTSCStr(dtrMMca!ac_unmis) = "" Then
            !pa_unmis = " "
          Else
            !pa_unmis = dtrMMca!ac_unmis
          End If
          Select Case dtrMMca!tb_testci.ToString
            Case Is = "B" : !pa_darave = "A"
            Case Is = "E" : !pa_darave = "D"
            Case Is = " " : !pa_darave = " " ' non dovrebbe mai essere !!
          End Select

          '-- solo se c'è iva indeducibile la ripartisco sulle varie righe di CA
          '   attenzione: in questo modo, a causa della proporzione, potrebbe rimanere una parte
          '   di iva indeducibile non ripartita. Ci pensano i test in wripriana a far tornare il tutto
          If dIvaInded <> 0 Then
            dIvaInd = ArrDbl((dIvaInded * NTSCDec(dtrMMca!valore)) / dImporto, oCldPnfa.TrovaNdec(0))
            If dIvaIndedVal <> 0 Then
              dIvaIndv = ArrDbl((dIvaIndedVal * NTSCDec(dtrMMca!valorev)) / dImpVal, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
            End If
          End If

          If bIvaInded Then
            ' eseguo la registrazione dell'iva indeducibile con causale 9999: devo registrare solo l'iva
            dImportoCA = ArrDbl(dIvaInd, oCldPnfa.TrovaNdec(0))
            dImpValCA = ArrDbl(dIvaIndv, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
          Else
            'caso standard - iva indeducibile ripartiat su tutte le contropartite (no causale 9999)
            dImportoCA = ArrDbl(NTSCDec(dtrMMca!valore) + dIvaInd, oCldPnfa.TrovaNdec(0))
            dImpValCA = ArrDbl(NTSCDec(dtrMMca!valorev) + dIvaIndv, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
          End If

          dTotCa = ArrDbl(dTotCa + NTSCDec(dtrMMca!valore) + dIvaInd, oCldPnfa.TrovaNdec(0))
          dTotCav = ArrDbl(dTotCav + NTSCDec(dtrMMca!valorev) + dIvaIndv, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))

          '!pa_darave = dtrMMca!tb_testci
          If !pa_darave.ToString = "D" Then
            !pa_importo = dImportoCA
            !pa_importoda = dImportoCA
            !pa_impval = dImpValCA
            !pa_impvalda = dImpValCA
            !pa_impolist = 0 ' per ora
            !pa_impolistda = 0 ' per ora
            !pa_quant = dtrMMca!quant
            !pa_quantda = dtrMMca!quant
          Else
            !pa_importo = ArrDbl(dImportoCA * -1, oCldPnfa.TrovaNdec(0))
            !pa_importoda = dImportoCA
            !pa_impval = ArrDbl(dImpValCA * -1, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
            !pa_impvalda = dImpValCA
            !pa_impolist = 0 ' per ora
            !pa_impolistda = 0 ' per ora
            !pa_quant = NTSCDec(dtrMMca!quant) * -1
            !pa_quantda = dtrMMca!quant
          End If
          !pa_controp = dtrTm!tm_conto
          !pa_codvalu = dtrTm!tm_valuta
          !pa_cambio = dtrTm!tm_cambio
          !pa_origine = "C"  ' salta i pa_mm ...
          ' --- !!!! utilizza i dati di prinot per il riempiento di questi campi ...
          !pa_datdoc = dtrPN!pn_datdoc
          !pa_numdoc = dtrPN!pn_numdoc
          !pa_alfdoc = dtrPN!pn_alfdoc
          !pa_datreg = dtrPN!pn_datreg
          !pa_numreg = dtrPN!pn_numreg
          !pa_riga = dtrPN!pn_riga
          !pa_note = DBNull.Value
          !pa_codart = " "
          !pa_desart = DBNull.Value
          !pa_codcope = 0
          !pa_codlavo = 0
          !pa_daore = 0
          !pa_aore = 0
          !pa_lotto = 0
          !pa_mastro = dtrMMca!ac_codmaca
          !pa_datini = NTSCDate(dDtIniz)
          !pa_datfin = NTSCDate(dDtFin)
          !pa_integr = IIf(bInt, "S", "N").ToString
          !pa_contocg = lContocg
          !pa_livrib = 0
          !pa_codcenaorig = 0
        End With    'With dttCA.Rows(dttCA.Rows.Count - 1)
      Next    'For Each dtrMMca As DataRow In dttMMca.Rows
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
  Public Overridable Function WriPrianaAcc(ByVal lContocg As Integer, ByVal strDarave As String, _
                                           ByVal dImporto As Decimal, ByVal dImpVal As Decimal, _
                                           ByRef dtrPN As DataRow, ByRef dtrTm As DataRow) As Boolean
    Dim strCodcfam As String = " "
    Dim strSubCommeca As String = " "
    Dim lCommeca As Integer = 0
    Dim nCodcena As Integer = 0
    Dim lProt As Integer = 0
    Dim lContoca As Integer = 0
    Dim strUnmis As String = ""
    Dim lCodmaca As Integer = 0
    Dim dttTmp As New DataTable
    Dim strErr As String = ""
    Dim strAnFlci As String = " "

    Try
      If bCa2 Then Return True 'se c'è la nuova analitica non posso gestire anche la vecchia :)
      If nCADoc = 1 Or nCADoc = 3 Then Return True 'non ho scritto nessun record di CA del corpo del documento: non scrivo neanche le spese di piede (cor compatibilità con versione precedente che non faceva contabilizzare per nulla)

      oCldPnfa.ValCodiceDb(lContocg.ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp)
      If dttTmp.Rows(0)!an_flci.ToString = " " Then Return True

      '-----------------------
      ' trova il conto ca in analink (1.o conto... per ora)
      ' cerca e setta anceh unmis e cod mastro di ca
      lContoca = oCldPnfa.GetFirstAnalinkFromContoCG(strDittaCorrente, lContocg)
      If lContoca = 0 Then
        LogWrite(oApp.Tr(Me, 128771778798054000, _
                          "Non esistono conti di C.A. collegati al conto C.G. |" & lContocg & vbCrLf & _
                          "| in documento |" & strEstremidoc & "| (Viene preso il primo conto di CA indicato nell'anagrafica del sottoconto -> 'Collegamento con pdc di CA)"), True)
        Return False
      End If

      strAnFlci = oCldPnfa.RitornaAcFlciDitt(strDittaCorrente, lContoca)
      strUnmis = " "
      lCodmaca = NTSCInt(Microsoft.VisualBasic.Fix(lContoca / 10000))

      '-----------------------
      'test sul budget
      Select Case strAnFlci
        Case "1" : nCodcena = NTSCInt(dtrTm!tm_codcena) : strCodcfam = " " : lCommeca = 0 : strSubCommeca = " "
        Case "2" : nCodcena = NTSCInt(dtrTm!tm_codcena) : strCodcfam = " " : lCommeca = NTSCInt(dtrTm!tm_commeca) : strSubCommeca = NTSCStr(dtrTm!tm_subcommeca)
      End Select
      If strAnFlci = "3" Or strAnFlci = "4" Then ' controlla se esiste il budget
        LogWrite(oApp.Tr(Me, 128771782712146000, _
                          "Non ammessa gestione di conto con linee su conto |" & lContoca & vbCrLf & _
                          "| in documento |" & strEstremidoc & "|"), True)
        Return False
      End If
      If nCodcena = 0 Then
        LogWrite(oApp.Tr(Me, 128806601772411000, _
                          "Non è stato indicato il centro di CA per la rilevazione delle spese accessorie nel documento |" & strEstremidoc & "|"), True)
        Return False
      End If

      If Not oCldPnfa.EsisteBudget(strDittaCorrente, "E", lContoca, nCodcena, strCodcfam, lCommeca, strSubCommeca, nEscomp, bTestBudget) Then ' controlla se esiste il budget
        LogWrite(oApp.Tr(Me, 128771782725718000, _
                  "Budget inesistente su centro |" & nCodcena & "| su conto |" & lContoca & vbCrLf & _
                  "| in documento |" & strEstremidoc & "|"), True)
        Return False
      End If
      If strAnFlci = "2" Then
        If Not oCldPnfa.EsisteBudget(strDittaCorrente, "C", lContoca, nCodcena, strCodcfam, lCommeca, strSubCommeca, nEscomp, bTestBudget) Then ' controlla se esiste il budget
          LogWrite(oApp.Tr(Me, 128771782753798000, _
                    "Budget inesistente su commessa |" & lCommeca & "|, sottocommessa '|" & strSubCommeca & "|', su conto |" & lContoca & vbCrLf & _
                    "| in documento |" & strEstremidoc & "|"), True)
          Return False
        End If
      End If

      '-----------------------
      'scrivo
      dttCA.Rows.Add(dttCA.NewRow)
      With dttCA.Rows(dttCA.Rows.Count - 1)
        'trova il numero progressivo di record
        strErr = ""
        lProt = oCldPnfa.LegNuma(strDittaCorrente, "PA", " ", 0, True)
        lProt = oCldPnfa.AggNuma(strDittaCorrente, "PA", " ", 0, lProt, True, True, strErr)
        If strErr <> "" Then LogWrite(strErr, True)
        !codditt = strDittaCorrente
        !pa_progr = lProt
        !pa_escomp = nEscomp
        !pa_conto = lContoca
        !pa_codcena = nCodcena
        !pa_codcfam = strCodcfam
        !pa_commeca = lCommeca
        !pa_subcommeca = strSubCommeca
        If strUnmis = "" Then
          !pa_unmis = " "
        Else
          !pa_unmis = strUnmis
        End If
        !pa_quant = 0
        !pa_quantda = 0
        !pa_darave = strDarave
        If !pa_darave.ToString = "D" Then
          !pa_importo = dImporto
          !pa_importoda = dImporto
          !pa_impval = dImpVal
          !pa_impvalda = dImpVal
          !pa_impolist = 0 ' per ora")
          !pa_impolistda = 0 ' per ora")
        Else
          !pa_importo = dImporto * -1
          !pa_importoda = dImporto
          !pa_impval = dImpVal * -1
          !pa_impvalda = dImpVal
          !pa_impolist = 0 ' per ora")
          !pa_impolistda = 0 ' per ora")
        End If
        !pa_controp = dtrTm!tm_conto
        !pa_codvalu = dtrTm!tm_valuta ' per ora
        !pa_cambio = dtrTm!tm_cambio ' per ora
        !pa_origine = "C"  ' salta i pa_mm ...
        ' --- !!!! utilizza i dati di prinot per il riempiento di questi campi ...
        !pa_datdoc = dtrPN!pn_datdoc
        !pa_numdoc = dtrPN!pn_numdoc
        !pa_alfdoc = dtrPN!pn_alfdoc
        !pa_datreg = dtrPN!pn_datreg
        !pa_numreg = dtrPN!pn_numreg
        !pa_riga = dtrPN!pn_riga
        !pa_note = DBNull.Value
        !pa_codart = " "
        !pa_desart = DBNull.Value
        !pa_codcope = 0
        !pa_codlavo = 0
        !pa_daore = 0
        !pa_aore = 0
        !pa_lotto = 0
        !pa_mastro = lCodmaca 'objStd.gcAnag("an_codmast")
        !pa_datini = NTSCDate(dtrPN!pn_datreg)
        !pa_datfin = NTSCDate(dtrPN!pn_datreg)
        !pa_integr = IIf(bInt, "S", "N").ToString
        !pa_contocg = lContocg
        !pa_livrib = 0
        !pa_codcenaorig = 0
      End With    'With dttCA.Rows(dttCA.Rows.Count - 1)

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
  Public Overridable Function CalcolaDiffCambio(ByRef lRiga As Integer, ByVal strDatreg As String, _
                                                ByVal lNumreg As Integer, _
                                                ByVal nCausale As Integer, ByVal lDiffAttCambi As Integer, _
                                                ByVal lDiffPasCambi As Integer, ByRef dtrTm As DataRow, _
                                                ByRef dtrPn As DataRow) As Boolean
    'cerco eventuali differenze di cambio solo nel caso in cui la fattura è in valuta,
    'e il tipo bolla/fattura chiuse un documento di acconto
    'pressuppone che i documenti riepilogati nella fattura abbiano tutti lo stesso tipo bolla/fattura,
    'o perlomento che su tutti sa impostato allo stesso modo il flag 'chiudi doc. di acconto.

    'cerca all'interno della fattura, in movmag, eventuali righe di acconto;
    'se ce ne sono,
    '   cerca all'interno della fattura di acconto la somma delle contropartite (raggruppa per contropartita,
    '   nel caso in cui ne siano state utilizzate di diverse all'interno dei vari documenti di acconto
    '   una volta eseguita la somma, per ognuna confronta il totale con quello indicato nel documento riepilogativo:
    '   se è diverso rileva la differenza di cambio (il conto utilizzato lo prendo da personalizzaz. CG)

    'lDiffAttCambi sottoconto differenze attive su cambi  (tb_condcat)
    'lDiffPasCambi sottoconto differenze passive su cambi (tb_condcpa)
    Dim i As Integer = 0
    Dim dDiff As Decimal = 0
    Dim dImpOld As Decimal
    Dim dImpNew As Decimal
    Dim lPrecControp As Integer = 0
    Dim lConto As Integer = 0
    Dim bOk As Boolean = False
    Dim dttAcc As New DataTable
    Dim dttTmp As New DataTable

    Try
      '-------------------
      'ottengo i documenti di acconto che sono stati evasi con il corrente documento
      If Not oCldPnfa.GetAccontiPerDifferenzeDiCambio(strDittaCorrente, dtrTm!tm_tipork.ToString, _
                                                      NTSCInt(dtrTm!tm_anno), dtrTm!tm_serie.ToString, _
                                                      NTSCInt(dtrTm!tm_numdoc), dttAcc) Then Return False
      'nel documento non sono stati indicati acconti: esco
      If dttAcc.Rows.Count = 0 Then Return True

      'controllo se in tabpecg sono indicati i conti differenze attive e passive su cambi
      If lDiffPasCambi = 0 Then
        LogWrite(oApp.Tr(Me, 128771937512140000, _
                        "Non è possibile rilevare la differenza su cambio " & _
                        "per il documento |" & strEstremidoc & "| in quanto in 'Personalizzazione contabilità generale' " & _
                        "non è stato indicato il conto differenze passive su cambi."), True)
        Return False
      End If
      If lDiffAttCambi = 0 Then
        LogWrite(oApp.Tr(Me, 128771922230966000, _
                        "Non è possibile rilevare la differenza su cambio " & _
                        "per il documento |" & strEstremidoc & "| in quanto in 'Personalizzazione contabilità generale' " & _
                        "non è stato indicato il conto differenze attive su cambi."), True)
        Return False
      End If

      'per ogni record, cerco l'importo di contropartita di incasso a parità di cod controp:
      For Each dtrAcc As DataRow In dttAcc.Rows
        For i = 1 To nMaxControp
          If NTSCInt(dttControp.Rows(i)!lControp) = NTSCInt(dtrAcc!tm_ccontr_1) Then
            Exit For
          End If
        Next
        If i = nMaxControp Then
          LogWrite(oApp.Tr(Me, 128771922298670000, _
                            "Nel documento |" & strEstremidoc & "| non è stato trovato nessun codice " & _
                            "contropartita uguale a quello utilizzato nell'acconto collegato." & _
                            "Non sarà possibile generare una eventuale registrazione di differenze di cambio."), True)
          Return False
        End If

        If NTSCInt(dttControp.Rows(i)!lControp) <> lPrecControp And lPrecControp <> 0 And dDiff <> 0 Then
          '--------------------------
          'La prima riga
          lRiga = lRiga + 1
          lConto = NTSCInt(IIf(dDiff > 0, lDiffPasCambi, lDiffAttCambi))
          oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
          If dttTmp.Rows.Count = 0 Then
            LogWrite(oApp.Tr(Me, 128771927491236000, "(27000) Operazione interrotta elaborando " & _
                            "il documento |" & strEstremidoc & "|. " & _
                            "Conto di 'Personalizzazione contabilità generale' differenze attive e/o differenze passive su cambi inesistente"), True)
            Return False
          Else
            If lConto <> NTSCInt(dtrTm!tm_conto) Then
              If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
                LogWrite(oApp.Tr(Me, 128771937406632000, "(27000) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "Conto di contropartita |" & lConto & "| gestito  a partite/scadenze"), True)
                Return False
              End If
            End If
          End If

          'DIFFERENZE DI CAMBIO: CONTO DIFFERENZE ATTIVA o DIFFERENZA PASSIVA
27000:    bOk = Wripn(27000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                      dttTmp.Rows(0)!an_scaden.ToString, " ", nCausale, Math.Abs(dDiff), _
                      IIf(dDiff > 0, "D", "A").ToString, 0, " ", 0, 0, 0, 0, 0, 0)
          If Not bOk Then
            LogWrite(oApp.Tr(Me, 128771923875206000, "(27000) Operazione interrotta elaborando il documento |" & strEstremidoc & "|."), True)
            Return False
          End If

          If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
            If Not WriPriana2(27000, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
              LogWrite(oApp.Tr(Me, 129278295362343750, "(27000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
              Return False
            End If
          End If

          '--------------------------
          'la seconda riga
          lRiga = lRiga + 1
          lConto = 0
          oCldPnfa.ValCodiceDb(lPrecControp.ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then lConto = NTSCInt(dttTmp.Rows(0)!tb_concove)
          oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
          If dttTmp.Rows.Count = 0 Then
            LogWrite(oApp.Tr(Me, 128771930125608000, "(28000) Operazione interrotta elaborando " & _
                            "il documento |" & strEstremidoc & "|. " & _
                            "Conto diassociato alla 'contropartita vendite/acquisti ditta' |" & lPrecControp & "| inesistente"), True)
            Return False
          Else
            If lConto <> NTSCInt(dtrTm!tm_conto) Then
              If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
                LogWrite(oApp.Tr(Me, 128771937422544000, "(28000) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "Conto di contropartita |" & lConto & "| gestito  a partite/scadenze"), True)
                Return False
              End If
            End If
          End If

          'DIFFERENZE DI CAMBIO: COSTO / RICAVO
28000:    bOk = Wripn(28000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                      dttTmp.Rows(0)!an_scaden.ToString, " ", nCausale, Math.Abs(dDiff), _
                      IIf(dDiff > 0, "A", "D").ToString, 0, " ", 0, 0, 0, 0, 0, 0)
          If Not bOk Then
            LogWrite(oApp.Tr(Me, 128771923988306000, "(28000) Operazione interrotta elaborando il documento |" & strEstremidoc & "|."), True)
            Return False
          End If

          If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
            If Not WriPriana2(28000, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
              LogWrite(oApp.Tr(Me, 129278295616982421, "(28000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
              Return False
            End If
          End If

          If bCa Then
            bOk = WriPrianaAcc(lConto, IIf(dDiff > 0, "A", "D").ToString, Math.Abs(dDiff), 0, dtrPn, dtrTm)
            If Not bOk Then
              LogWrite(oApp.Tr(Me, 128771931438036000, "(28000) Operazione interrotta elaborando la contabilità analitica per il documento |" & strEstremidoc & "|."), True)
              Return False
            End If
          End If
          '******************************************************************************
          ' con questo sistema ci saranno sicuramente delle differenze di arrotondamento,
          'ma per il momento non si può fare di meglio ...
          '******************************************************************************
          If NTSCDec(dtrAcc!mm_valorev) < 0 Then
            dImpOld = oCldPnfa.ConvImpValuta(strDittaCorrente, False, ArrDbl(Math.Abs(NTSCDec(dtrAcc!mm_valorev)), oCldPnfa.TrovaNdec(0)), NTSCInt(dtrTm!tm_valuta), NTSCDate(dtrTm!tm_datdoc), NTSCDec(dtrAcc!CAMBIOOLD)) * -1
            dImpNew = oCldPnfa.ConvImpValuta(strDittaCorrente, False, ArrDbl(Math.Abs(NTSCDec(dtrAcc!mm_valorev)), oCldPnfa.TrovaNdec(0)), NTSCInt(dtrTm!tm_valuta), NTSCDate(dtrTm!tm_datdoc), NTSCDec(dtrAcc!CAMBIOnew)) * -1
          Else
            dImpOld = oCldPnfa.ConvImpValuta(strDittaCorrente, False, ArrDbl(Math.Abs(NTSCDec(dtrAcc!mm_valorev)), oCldPnfa.TrovaNdec(0)), NTSCInt(dtrTm!tm_valuta), NTSCDate(dtrTm!tm_datdoc), NTSCDec(dtrAcc!CAMBIOOLD))
            dImpNew = oCldPnfa.ConvImpValuta(strDittaCorrente, False, ArrDbl(Math.Abs(NTSCDec(dtrAcc!mm_valorev)), oCldPnfa.TrovaNdec(0)), NTSCInt(dtrTm!tm_valuta), NTSCDate(dtrTm!tm_datdoc), NTSCDec(dtrAcc!CAMBIOnew))
          End If

          dDiff = ArrDbl(dImpNew - dImpOld, oCldPnfa.TrovaNdec(0))
        Else
          If NTSCDec(dtrAcc!mm_valorev) < 0 Then
            dImpOld = oCldPnfa.ConvImpValuta(strDittaCorrente, False, ArrDbl(Math.Abs(NTSCDec(dtrAcc!mm_valorev)), oCldPnfa.TrovaNdec(0)), NTSCInt(dtrTm!tm_valuta), NTSCDate(dtrTm!tm_datdoc), NTSCDec(dtrAcc!CAMBIOOLD)) * -1
            dImpNew = oCldPnfa.ConvImpValuta(strDittaCorrente, False, ArrDbl(Math.Abs(NTSCDec(dtrAcc!mm_valorev)), oCldPnfa.TrovaNdec(0)), NTSCInt(dtrTm!tm_valuta), NTSCDate(dtrTm!tm_datdoc), NTSCDec(dtrAcc!CAMBIOnew)) * -1
          Else
            dImpOld = oCldPnfa.ConvImpValuta(strDittaCorrente, False, ArrDbl(Math.Abs(NTSCDec(dtrAcc!mm_valorev)), oCldPnfa.TrovaNdec(0)), NTSCInt(dtrTm!tm_valuta), NTSCDate(dtrTm!tm_datdoc), NTSCDec(dtrAcc!CAMBIOOLD))
            dImpNew = oCldPnfa.ConvImpValuta(strDittaCorrente, False, ArrDbl(Math.Abs(NTSCDec(dtrAcc!mm_valorev)), oCldPnfa.TrovaNdec(0)), NTSCInt(dtrTm!tm_valuta), NTSCDate(dtrTm!tm_datdoc), NTSCDec(dtrAcc!CAMBIOnew))
          End If
          dDiff = ArrDbl(dDiff + dImpNew - dImpOld, oCldPnfa.TrovaNdec(0))
        End If    'If NTSCInt(dttControp.Rows(i)!lControp) <> lPrecControp And lPrecControp <> 0 And dDiff <> 0 Then

        lPrecControp = NTSCInt(dttControp.Rows(i)!lControp)
      Next    'For Each dtrAcc As DataRow In dttAcc.Rows

      '--------------------------------------------
      'scrivo, se necessario, le due ultime righe
      If dDiff <> 0 Then

        'la prima riga
        lRiga = lRiga + 1
        lConto = NTSCInt(IIf(dDiff > 0, lDiffPasCambi, lDiffAttCambi))
        oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
        If dttTmp.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 128771936050376000, "(29000) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "Conto di 'Personalizzazione contabilità generale' differenze attive e/o differenze passive su cambi inesistente"), True)
          Return False
        Else
          If lConto <> NTSCInt(dtrTm!tm_conto) Then
            If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
              LogWrite(oApp.Tr(Me, 128771936106380000, "(29000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di contropartita |" & lConto & "| gestito  a partite/scadenze"), True)
              Return False
            End If
          End If
        End If

        'DIFFERENZE DI CAMBIO: CONTO DIFFERENZE ATTIVA o DIFFERENZA PASSIVA
29000:  bOk = Wripn(29000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                    dttTmp.Rows(0)!an_scaden.ToString, " ", nCausale, Math.Abs(dDiff), _
                    IIf(dDiff > 0, "D", "A").ToString, 0, " ", 0, 0, 0, 0, 0, 0)
        If Not bOk Then
          LogWrite(oApp.Tr(Me, 128771924163494000, "(29000) Operazione interrotta elaborando il documento |" & strEstremidoc & "|."), True)
          Return False
        End If

        If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
          If Not WriPriana2(29000, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
            LogWrite(oApp.Tr(Me, 129278296058652343, "(29000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
            Return False
          End If
        End If

        'la seconda riga
        lRiga = lRiga + 1
        lConto = 0
        oCldPnfa.ValCodiceDb(lPrecControp.ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then lConto = NTSCInt(dttTmp.Rows(0)!tb_concove)
        oCldPnfa.GetContoPerWripn(strDittaCorrente, lConto, dttTmp)
        If dttTmp.Rows.Count = 0 Then
          LogWrite(oApp.Tr(Me, 128771936761424000, "(30000) Operazione interrotta elaborando " & _
                          "il documento |" & strEstremidoc & "|. " & _
                          "Conto diassociato alla 'contropartita vendite/acquisti ditta' |" & lPrecControp & "| inesistente"), True)
          Return False
        Else
          If lConto <> NTSCInt(dtrTm!tm_conto) Then
            If dttTmp.Rows(0)!an_scaden.ToString = "S" Then
              LogWrite(oApp.Tr(Me, 128771936776712000, "(30000) Operazione interrotta elaborando " & _
                        "il documento |" & strEstremidoc & "|. " & _
                        "Conto di contropartita |" & lConto & "| gestito  a partite/scadenze"), True)
              Return False
            End If
          End If
        End If

        'DIFFERENZE DI CAMBIO: COSTO / RICAVO
30000:  bOk = Wripn(30000, dtrTm, strDatreg, lNumreg, lRiga, lConto, dttTmp.Rows(0)!an_partite.ToString, _
                    dttTmp.Rows(0)!an_scaden.ToString, " ", nCausale, Math.Abs(dDiff), _
                    IIf(dDiff > 0, "A", "D").ToString, 0, " ", 0, 0, 0, 0, 0, 0)
        If Not bOk Then
          LogWrite(oApp.Tr(Me, 128771937580780000, "(30000) Operazione interrotta elaborando il documento |" & strEstremidoc & "|."), True)
          Return False
        End If

        If NTSCStr(dttTmp.Rows(0)!an_flci).Trim <> "" And bCa2 Then
          If Not WriPriana2(30000, dtrTm, lNumreg, lRiga, "1", 0, Nothing, 0) Then
            LogWrite(oApp.Tr(Me, 129278295831220703, "(30000) Operazione interrotta elaborando la Contabilità analitica del documento |" & strEstremidoc & "|."), True)
            Return False
          End If
        End If

        If bCa Then
          bOk = WriPrianaAcc(lConto, IIf(dDiff > 0, "A", "D").ToString, Math.Abs(dDiff), 0, dtrPn, dtrTm)
          If Not bOk Then
            LogWrite(oApp.Tr(Me, 128771924280026000, "(30000) Operazione interrotta elaborando la contabilità analitica per il documento |" & strEstremidoc & "|."), True)
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
    Finally
      dttTmp.Clear()
      dttAcc.Clear()
    End Try
  End Function
  Public Overridable Function PrinotOk() As Boolean
    Dim dttGr As New DataTable
    Dim oDttgr As New CLEGROUPBY
    Dim strTmp As String = ""
    Dim dtrPn() As DataRow = Nothing
    Dim dtrPn1() As DataRow = Nothing
    Dim i As Integer = 0
    Dim e As Integer = 0
    Dim strMessaggio As String = ""
    Dim bOk As Boolean = True
    Try
      If lNumregFatt <> 0 Then strTmp += "pn_numreg = " & lNumregFatt & " OR "
      If lNumregIncasso <> 0 Then strTmp += "pn_numreg = " & lNumregIncasso & " OR "
      If lNumregOmaggi <> 0 Then strTmp += "pn_numreg = " & lNumregOmaggi & " OR "
      If lNumregStanziamenti <> 0 Then strTmp += "pn_numreg = " & lNumregStanziamenti & " OR "
      If strTmp.Length > 0 Then strTmp = strTmp.Substring(0, strTmp.Length - 4)

      If strTmp = "" Then Return True

      oDttgr.NTSGroupBy(dttPN, dttGr, "pn_numreg, sum(pn_importo) as xx_importo", "", "pn_numreg")
      dtrPn1 = dttGr.Select("xx_importo <> 0")
      For e = 0 To dtrPn1.Length - 1
        strMessaggio = oApp.Tr(Me, 128775465712626000, "Il documento |" & strEstremidoc & "| ha generato una registrazione squadrata. Il documento non verrà contabilizzato." & vbCrLf & _
             "La registrazione avrebbe dovuto avere i seguenti dati: " & vbCrLf & _
             "DATA/NUMERO REG     CAUSALE       CONTO              DARE          AVERE   DESCRIZIONE" & vbCrLf & _
             "---------------------------------------------------------------------------------------------------------") & vbCrLf
        dtrPn = dttPN.Select("pn_numreg = " & dtrPn1(e)!pn_numreg.ToString, "pn_riga")
        For i = 0 To dtrPn.Length - 1
          oCldPnfa.ValCodiceDb(dtrPn(i)!pn_conto.ToString, strDittaCorrente, "ANAGRA", "N", strTmp)
          strMessaggio = strMessaggio & strDatreg & dtrPn(i)!pn_numreg.ToString.PadLeft(9) & _
             "     " & dtrPn(i)!pn_causale.ToString.PadLeft(4) & _
             "     " & dtrPn(i)!pn_conto.ToString.PadLeft(9) & _
             NTSCDec(dtrPn(i)!pn_dare).ToString(oApp.FormatImporti).PadLeft(15) & _
             NTSCDec(dtrPn(i)!pn_avere).ToString(oApp.FormatImporti).PadLeft(15) & _
             "   " & strTmp.PadRight(30).Substring(0, 30) & vbCrLf
        Next
        strMessaggio += oApp.Tr(Me, 129869218035666070, "Differenza: ") & NTSCDec(dtrPn1(e)!xx_importo).ToString(oApp.FormatImporti) & vbCrLf
        LogWrite(strMessaggio, True)
        bOk = False
      Next

      Return bOk

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
  Public Overridable Function AggiornaPrinotScaden(ByRef dtrTm As DataRow) As Boolean
    'Aggiorna PRINOT e SCADEN solo se TESTMAG.tm_riferim NON è NULLO
    'Aggiorna solo le righe di PRINOT con pn_scadenz = 'S'
    'Aggiorna le righe di SCADEN (tutte)
    '(prende i primi 40 caratteri perché SCADEN.sc_descr è lungo 40)

    'Se non è attiva l'opzione di registro, esce senza far alcun aggiornamento
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      If NTSCStr(dtrTm!tm_riferim) = "" Then Return True
      If bRiferimFattureinCG = False And strRiferimInDescr.Contains("," & NTSCStr(dtrTm!tm_tipork) & ",") = False Then
        Return True
      End If

      dtrT = dttPN.Select("pn_scadenz = 'S'")
      For i = 0 To dtrT.Length - 1
        dtrT(i)!pn_descr = NTSCStr(dtrTm!tm_riferim)
      Next

      For i = 0 To dttSC.Rows.Count - 1
        dttSC.Rows(i)!sc_descr = Left(NTSCStr(dtrTm!tm_riferim), 40)
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
  Public Overridable Function GeneraStornoStanziamenti(ByRef dtrTm As DataRow, ByRef bDaStornare As Boolean, _
                                                       ByRef strCausaleStornoStanz As String) As Boolean
    'per ogni fattura differita emessa/ricevuta
    'verifico se nei ddt collegati sono stati generati degli stanziamenti
    'se c'è lo stanziamento e non c'è la registrazione di storno
    'genero lo storno e aggiorno testmag
    'la registrazione di storno viene generata in data fattura
    Dim dttDdt As New DataTable
    Dim dttTmp As New DataTable
    Try
      bDaStornare = False

      If Not bGestStanziamenti Then Return True

      If dtrTm!tm_tipork.ToString <> "D" And dtrTm!tm_tipork.ToString <> "K" And _
         dtrTm!tm_tipork.ToString <> "£" And dtrTm!tm_tipork.ToString <> "(" Then Return True

      '-------------------------
      'ottengo i ddt collegati alla fattura con stanziamenti contabilizzati
      If Not oCldPnfa.GetDdtPerStornoAcconti(strDittaCorrente, dtrTm!tm_tipork.ToString, NTSCInt(dtrTm!tm_anno), _
                                        dtrTm!tm_serie.ToString, NTSCInt(dtrTm!tm_numdoc), dttDdt) Then Return False
      If dttDdt.Rows.Count = 0 Then Return True

      '-------------------------
      If nCauststanz = 0 Then
        LogWrite(oApp.Tr(Me, 128774638587124000, "Attenzione!" & _
                  "Non è stato indicata una causale storno stanziamenti valida in 'Personalizzazione Contabilità'." & _
                  "Generazione della registrazione di storno stanziamenti non possibile."), True)
        Return True
      End If

      oCldPnfa.ValCodiceDb(nCauststanz.ToString, strDittaCorrente, "TABCAUC", "N", strCausaleStornoStanz, dttTmp)
      If dttTmp.Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 128774640088156000, "Attenzione!" & _
                  "Non è stato indicata una causale storno stanziamenti valida in 'Personalizzazione Contabilità'." & _
                  "Generazione della registrazione di storno stanziamenti non possibile."), True)
        Return True
      End If
      dttTmp.Clear()

      '-------------------------
      'in fase di creazione registrazioni stornerò anche gli stanziamenti
      bDaStornare = True

      lNumregStanziamenti = oCldPnfa.LegAggRegcDitt(strDittaCorrente, strDatreg)

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
      dttDdt.Clear()
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function CheckIvaDiffNostaStornoFt(ByRef dtrTm As DataRow) As Boolean
    'iva differita nuovo sistema e nota accred storno fattura:
    'controllo se posso effettivamente contabilizzare per stornare la fattura
    Dim ds As New DataSet
    Dim i As Integer = 0
    Dim bOk As Boolean = True
    Try
      If NTSCInt(dtrTm!tm_numpar) = 0 Or NTSCInt(dtrTm!tm_annpar) = 0 Then
        LogWrite(oApp.Tr(Me, 130633016180126426, "Non possono essere contabilizzate 'note di accredito Iva differita a storno fattura'" & _
                         " se sulla nota di accredito non è stata indicata la fattura da stornare (estremi partita)" & vbCrLf & _
                         "Vedi documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      If NTSCDec(dtrTm!tm_impsca_2) <> 0 Or NTSCDec(dtrTm!tm_pagato) <> 0 Or NTSCDec(dtrTm!tm_pagato2) <> 0 Or NTSCDec(dtrTm!tm_abbuono) <> 0 Then
        LogWrite(oApp.Tr(Me, 130633017822455663, "Non possono essere contabilizzate 'note di accredito Iva differita a storno fattura'" & _
                         " aventi numero di scadenze superiori ad una o un importo pagato diverso da 0 o con abbuoni di piede diversi da 0." & vbCrLf & _
                         "Vedi documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      'dalla fattura ottengo gli estremi della registrazione
      If Not oCldPnfa.GetPrinotFromPartita(strDittaCorrente, NTSCInt(dtrTm!tm_conto), NTSCInt(dtrTm!tm_annpar), _
                                           NTSCStr(dtrTm!tm_alfpar), NTSCInt(dtrTm!tm_numpar), ds) Then Return False

      If ds.Tables("PRINOT").Rows.Count = 0 Then
        LogWrite(oApp.Tr(Me, 130633026209589558, "Nota di accredito Iva differita a storno fattura:" & _
                         " Dalla partita indicata sulla nota di accredito non è stato possibile ottenere la registrazione contabile della fattura." & vbCrLf & _
                         "Vedi documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      'se il totale residuo della fattura è inferiore al totale della nota accred non posso continuare
      If NTSCDec(dtrTm!tm_totdoc) > Math.Abs(NTSCDec(ds.Tables("SALDO").Rows(0)!xx_saldo)) Then 'math.abs per docuemnti ricevuti, visto che il saldo di prinot sarebbe negativo
        LogWrite(oApp.Tr(Me, 130633036431058029, "Nota di accredito Iva differita a storno fattura:" & _
                         " Il residuo da incassare/pagare della fattura è inferiore al totale nota di accredito." & vbCrLf & _
                         "Vedi documento |" & strEstremidoc & "|."), True)
        Return False
      End If

      'se ho codici IVA (con aliquota <> 0) diversi da quelli della fattura non posso continuare
      For i = 1 To 8
        If NTSCInt(dtrTm("tm_codiva_" & i.ToString)) <> 0 And NTSCDec(dtrTm("tm_imposta_" & i.ToString)) <> 0 Then
          bOk = False
          For Each dtrT As DataRow In ds.Tables("MOVIVA").Select("mi_aliqiva <> 0")
            If NTSCInt(dtrTm("tm_codiva_" & i.ToString)) = NTSCInt(dtrT!mi_codiva) Then
              bOk = True
              Exit For
            End If
          Next
          If bOk = False Then
            LogWrite(oApp.Tr(Me, 130633041917203916, "Nota di accredito Iva differita a storno fattura:" & _
                                     " Nella nota di accredito è stato utilizzato un cod. IVA (|" & NTSCStr(dtrTm("tm_codiva_" & i.ToString)) & "|) non presente in fattura." & vbCrLf & _
                                     "Vedi documento |" & strEstremidoc & "|."), True)
            Return False
          End If
        End If
      Next

      'estremi da riportare sulla nota accred prnot + moviva per iva differita
      strIEDDatreg = NTSCDate(ds.Tables("PRINOT").Rows(0)!pn_ieddatregr).ToShortDateString
      lIEDNumreg = NTSCInt(ds.Tables("PRINOT").Rows(0)!pn_iednumregr)
      nIEDRigareg = NTSCInt(ds.Tables("PRINOT").Rows(0)!pn_iedrigaregr)

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
      ds.Tables.Clear()
    End Try
  End Function

  Public Overridable Function WriPriana2(ByVal nRigaChiamante As Integer, ByRef dtrTm As DataRow, ByVal lNumreg As Integer, _
                                         ByVal nRigaCG As Integer, ByVal strTipo As String, ByVal nCodcove As Integer, _
                                         ByRef dtrControp As DataRow, ByVal dIvaInded As Decimal) As Boolean
    Try
      'tutti i casi eccetto righe di costo / ricavo
      Return WriPriana2(nRigaChiamante, dtrTm, lNumreg, nRigaCG, strTipo, nCodcove, dtrControp, dIvaInded, Nothing, 0)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function WriPriana2(ByVal nRigaChiamante As Integer, ByRef dtrTm As DataRow, ByVal lNumreg As Integer, _
                                       ByVal nRigaCG As Integer, ByVal strTipo As String, ByVal nCodcove As Integer, _
                                       ByRef dtrControp As DataRow, ByVal dIvaInded As Decimal, ByRef dttTmOrig As DataTable) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nRigaChiamante, dtrTm, lNumreg, nRigaCG, strTipo, nCodcove, dtrControp, dIvaInded, dttTmOrig})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrTm = CType(oIn(1), DataRow)
        dtrControp = CType(oIn(6), DataRow)
        dttTmOrig = CType(oIn(8), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      'obsoleta
      Return WriPriana2(nRigaChiamante, dtrTm, lNumreg, nRigaCG, strTipo, nCodcove, dtrControp, dIvaInded, dttTmOrig, 0)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function WriPriana2(ByVal nRigaChiamante As Integer, ByRef dtrTm As DataRow, ByVal lNumreg As Integer, _
                                         ByVal nRigaCG As Integer, ByVal strTipo As String, ByVal nCodcove As Integer, _
                                         ByRef dtrControp As DataRow, ByVal dIvaInded As Decimal, ByRef dttTmOrig As DataTable, _
                                         ByVal dIvaIndedval As Decimal) As Boolean
    'nuova contabilità analitica duplice contabile:
    'se sono qui è perchè il conto è gestito in contabilità analitica

    '--------------------------------------
    'per dettaglio corpo devo fare select su movmag per ottenere elenco righe raggruppate per tabcove, datini
    'per le spese di piede su priana2 non memorizzo il collegamento con testmag
    'per fatt diff ricevuta con carico da prod, riscrivo solo le lavorazioni (la qta prodotta e le righe di scarico sono quelle già presenti in priana2 da movmag, visto che nella fattura gli unici costi sono quelli della lavorazione)
    'l'IVA e' una partita di giro: non può andare a finire in CA2!!!!!!!!!! (anche perchè se devo cercare in movmag ddt righe per ricalcolare l'iva, tenendo conto degli arrotondamenti non tornerà mai con l'iva raggruppata di cgprin... 
    'per documenti ricevuti con carico da produzione dovrò rilanciare per ogni documento la ScriviPriana2DaTestmag con il parametro 'soloscarichieqtaprd' = true 

    'mi serve: il conto di ca da mm_contocontr o lce_contocontr
    '          centro, linea, commessa presa o da testmag (strTipo = 1) o da movmag/lavcent (strTipo = 0)
    '          riferimenti a movmag e/o lavcent


    '--------------------------------------
    'dtrTm  : riga di testmag (fattura) in analisi
    'nRigaCG: riga di CG a cui deve essere collegata la CA
    'strTipo: 0, costo/ricavo da riga di movmag (su priana2 devo mettere i riferimenti a movmag o lavcent)
    '         1, importi non contemplati sotto di testata/piede 
    '         2, IVA
    '         3, SPESE INCASSO
    '         4, SPESE IMBALLO  
    '         5, SPESE TRASPORTO
    '         6, ABBUONI
    '         7, OMAGGI
    '         8, BOLLI
    '         9, CASSA/BANCA
    '         10, RITENUTA
    '         11, CLIENTE PER STORNO RITENUTA
    '         12, ENSARCO
    '         13, CLIENTE PER STORNO ENSARCO
    '         14, SPESE GENERALI
    '         15, CASSA COMMERCIALISTI

    'dttTmOrig viene passato diverso da Nothing solo per corrispettivi emessi o ricevute fiscali emesse 
    'TOTALMENTE incassate da contabilizzare come RAGGRUPPATE (dtrTm!xx_raggr = 'S')

    Dim dtrCG As DataRow = Nothing      'riga di CG a cui collegare le righe di PRIANA2
    Dim lConto As Integer = 0
    Dim lControp As Integer = 0
    Dim dttMm As New DataTable
    Dim lProgr As Integer = 0
    Dim strErr As String = ""
    Dim dtrContr As DataRow = Nothing   'anagca per la contropartita
    Dim dttCaca As New DataTable
    Dim bDocRicevuto As Boolean = False
    Dim dttTmp As New DataTable
    Dim dImporto As Decimal = 0
    Dim dImpval As Decimal = 0
    Dim dIvaIndedQuota As Decimal = 0
    Dim dIvaIndedQuotaVal As Decimal = 0
    Dim dSomma As Decimal = 0
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nRigaChiamante, dtrTm, lNumreg, nRigaCG, strTipo, nCodcove, dtrControp, dIvaInded, dttTmOrig})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrTm = CType(oIn(1), DataRow)
        dtrControp = CType(oIn(6), DataRow)
        dttTmOrig = CType(oIn(8), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      If strTipo = "2" Then Return True 'l'IVA e' una partita di giro: non può andare a finire in CA

      dtrCG = dttPN.Select("pn_numreg = " & lNumreg.ToString & " AND pn_riga = " & nRigaCG.ToString)(0)
      If NTSCDec(dtrCG!pn_dare) = 0 And NTSCDec(dtrCG!pn_avere) = 0 Then Return True 'per andare a finire in CA da CG deve esserci un valore diverso da 0

      If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "(" Then bDocRicevuto = True

      '--------------------------------
      'determino i dati per alimentare la CA
      If strTipo = "0" Then
        If nCodcove = 0 Then Return True
        If Not oCldPnfa.GetMovmagCA2(strDittaCorrente, dtrTm!tm_tipork.ToString, NTSCInt(dtrTm!tm_anno), _
                                     dtrTm!tm_serie.ToString, NTSCInt(dtrTm!tm_numdoc), _
                                     nCodcove, NTSCDate(dtrControp!strDtInizOrig).ToShortDateString, _
                                     NTSCDate(dtrControp!strDtFinOrig).ToShortDateString, dttMm, _
                                     NTSCStr(dtrTm!xx_raggr), NTSCInt(dtrTm!xx_dtttm), dttTmOrig, _
                                     bCaDcRaggruppaCorr) Then Return False
        '--------------------------------
        'non posso contabilizzare documenti che muovono 2 magazzini
        If dttMm.Rows.Count > 0 Then
          If dttMm.Select("mm_magaz2 <> 0").Length > 0 Then
            'LogWrite(oApp.Tr(Me, 129279754556376953, "Con la 'Contabilità analitica duplice contabile' attiva non è possibile contabilizzare documenti che 'muovono' 2 magazzini, (esempio: reso lavorato da terzista senza cambio di codice articolo)."), True)
            'Return False
            'ora si possono gestire questi tipi di documenti, 
            'MA VIENE PORTATO IN CA DC solo la parte che movimenta il magazzino di CARICO per i documenti ricevuti
            'o magazzino di SCARICO per i documenti emessi
            If dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "(" Then
              'doc ricevuto
              For Each dtrT As DataRow In dttMm.Select("km_carscar = -1")
                dtrT.Delete()
              Next
            Else
              'docemesso
              For Each dtrT As DataRow In dttMm.Select("km_carscar = 1")
                dtrT.Delete()
              Next
            End If
            dttMm.AcceptChanges()
          End If
        End If
        If dttMm.Rows.Count = 0 Then
          'anche se ho indicato un sottoconto di CG che gestisce la CA, in movmag non sono presenti righe che soddisfano i requisiti di CA!!!
          LogWrite(oApp.Tr(Me, 130078185650739569, "ATTENZIONE: Nel documento di magazzino non è stata trovata nessuna riga con contropartita vendite/acquisti |" & nCodcove & "| (che ha collegato un conto di CG che richiede la CA) che soddisfi le richieste della 'Contabilità analitica duplice contabile' (possibli cause: causale di magazzino che non gestisce la CA, stampa riga uguale a 'SCONTO MERCE NC', conto contropartita CA non impostato)."), True)
          Return False
        End If

        '--------------------------------
        'se serve aggiungo la quota di iva inded (solo su acquisti)
        If dIvaInded <> 0 Then
          If bGirocontoIvaIndedRipartito Then
            dImporto = NTSCDec(dtrCG!pn_dare)
            dImpval = NTSCDec(dtrCG!pn_dareval)
            For Each dtrT As DataRow In dttMm.Rows
              dIvaIndedQuota = ArrDbl((dIvaInded / (NTSCDec(dtrCG!pn_dare) - dIvaInded) * NTSCDec(dtrT!mm_valore)), oCldPnfa.TrovaNdec(0))
              If dIvaIndedval <> 0 Then
                dIvaIndedQuotaVal = ArrDbl((dIvaIndedval / (NTSCDec(dtrCG!pn_dareval) - dIvaIndedval) * NTSCDec(dtrT!mm_valorev)), oCldPnfa.TrovaNdec(0))
              Else
                dIvaIndedQuotaVal = 0
              End If
              dtrT!mm_valore = NTSCDec(dtrT!mm_valore) + dIvaIndedQuota
              dtrT!mm_valorev = NTSCDec(dtrT!mm_valorev) + dIvaIndedQuotaVal
              dImporto -= NTSCDec(dtrT!mm_valore)
              dImpval -= NTSCDec(dtrT!mm_valorev)
            Next
            dttMm.Rows(0)!mm_valore = NTSCDec(dttMm.Rows(0)!mm_valore) + dImporto 'se serve cambio l'ultimo importo per farlo tornare con quello della CG
            dttMm.Rows(0)!mm_valorev = NTSCDec(dttMm.Rows(0)!mm_valorev) + dImpval
          Else
            'se sono qui è sicuramente la prima riga di costo a cui assegnare l'iva inded
            For Each dtrT As DataRow In dttMm.Rows
              dImporto += NTSCDec(dtrT!mm_valore)
              dImpval += NTSCDec(dtrT!mm_valorev)
            Next
            For Each dtrT As DataRow In dttMm.Rows
              dtrT!mm_valore = ArrDbl((dIvaInded / dImporto * NTSCDec(dtrT!mm_valore)), oCldPnfa.TrovaNdec(0))
              If (dImpval * NTSCDec(dtrT!mm_valorev)) <> 0 Then
                dtrT!mm_valorev = ArrDbl((dIvaIndedval / dImpval * NTSCDec(dtrT!mm_valorev)), oCldPnfa.TrovaNdec(0))
              Else
                dtrT!mm_valorev = 0
              End If
            Next
            For Each dtrT As DataRow In dttMm.Rows
              dIvaInded -= NTSCDec(dtrT!mm_valore)
              dIvaIndedval -= NTSCDec(dtrT!mm_valorev)
            Next
            dttMm.Rows(0)!mm_valore = NTSCDec(dttMm.Rows(0)!mm_valore) + dIvaInded 'se serve cambio l'ultimo importo per farlo tornare con quello della CG
            dttMm.Rows(0)!mm_valorev = NTSCDec(dttMm.Rows(0)!mm_valorev) + dIvaIndedval
          End If
        End If

      Else
        'preparo il datatable per accettare i nuovi valori
        dttMm = New DataTable
        dttMm.Columns.Add("ac_accperi", GetType(String))
        dttMm.Columns.Add("ac_richcena", GetType(String))
        dttMm.Columns.Add("ac_richcfam", GetType(String))
        dttMm.Columns.Add("ac_richcomm", GetType(String))
        dttMm.Columns.Add("ac_richdivi", GetType(String))
        dttMm.Columns.Add("ac_richstab", GetType(String))
        dttMm.Columns.Add("ac_richarti", GetType(String))
        dttMm.Columns.Add("ac_richcli", GetType(String))
        dttMm.Columns.Add("mm_contocontr", GetType(Integer))
        dttMm.Columns.Add("tb_codcacadd", GetType(Integer))
        dttMm.Columns.Add("mm_codcena", GetType(Integer))
        dttMm.Columns.Add("mm_codcfam", GetType(String))
        dttMm.Columns.Add("mm_commeca", GetType(Integer))
        dttMm.Columns.Add("mm_subcommeca", GetType(String))
        dttMm.Columns.Add("mm_pmtaskid", GetType(Integer))
        dttMm.Columns.Add("mm_coddivi", GetType(Integer))
        dttMm.Columns.Add("tb_codstab", GetType(Integer))
        dttMm.Columns.Add("mm_codart", GetType(String))
        dttMm.Columns.Add("mm_fase", GetType(Integer))
        dttMm.Columns.Add("tm_codcli", GetType(Integer))
        dttMm.Columns.Add("mm_ump", GetType(String))
        dttMm.Columns.Add("mm_quant", GetType(Decimal))
        dttMm.Columns.Add("km_carscar", GetType(Integer))
        dttMm.Columns.Add("mm_valore", GetType(Decimal))
        dttMm.Columns.Add("mm_tipork", GetType(String))
        dttMm.Columns.Add("mm_anno", GetType(Integer))
        dttMm.Columns.Add("mm_serie", GetType(String))
        dttMm.Columns.Add("mm_numdoc", GetType(Integer))
        dttMm.Columns.Add("mm_riga", GetType(Integer))
        dttMm.Columns.Add("lce_rigaa", GetType(Integer))
        dttMm.Columns.Add("mm_valorev", GetType(Decimal))

        Select Case strTipo
          Case "1"    'devo determinare il conto ca dalle controp
            If NTSCInt(dtrCG!pn_conto) = lConclpriv Or NTSCInt(dtrCG!pn_conto) = NTSCInt(dtrTm!tm_conto) Then
              'è un cliente/fornitore: uso il cliente/fornitore generico
              If bDocRicevuto Then
                oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPecx.Rows(0)!tb_contrfornstd)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
              Else
                oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPecx.Rows(0)!tb_contrclistd)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
              End If
              lConto = NTSCInt(dttTmp.Rows(0)!tb_concova)
            Else
              lConto = oCldPnfa.GetContoCaDaContoCGFromTabcove(strDittaCorrente, NTSCInt(dtrCG!pn_conto))
            End If
          Case "3"    'SPESE INCASSO
            If bDocRicevuto Then
              oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontrriin)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
            Else
              oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contrriin)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
            End If
            lConto = NTSCInt(dttTmp.Rows(0)!tb_concova)
          Case "4" 'SPESE IMBALLO 
            If bDocRicevuto Then
              oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontrimba)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
            Else
              oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contrimba)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
            End If
            lConto = NTSCInt(dttTmp.Rows(0)!tb_concova)
          Case "5" 'SPESE TRASPORTO
            If bDocRicevuto Then
              oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontrriac)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
            Else
              oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contrriac)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
            End If
            lConto = NTSCInt(dttTmp.Rows(0)!tb_concova)
          Case "6" 'ABBUONI
            If bDocRicevuto Then
              If NTSCDec(dtrTm!tm_abbuono) > 0 Then
                oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontrabat)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
              Else
                oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontrabpa)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
              End If
            Else
              If NTSCDec(dtrTm!tm_abbuono) > 0 Then
                oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contrabpa)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
              Else
                oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contrabat)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
              End If
            End If
            lConto = NTSCInt(dttTmp.Rows(0)!tb_concova)
          Case "7" 'OMAGGI
            If bDocRicevuto Then
              oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontromag)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
            Else
              oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contromag)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
            End If
            lConto = NTSCInt(dttTmp.Rows(0)!tb_concova)
          Case "8" 'BOLLI
            If bDocRicevuto Then
              oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontrribo)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
            Else
              oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contrribo)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
            End If
            lConto = NTSCInt(dttTmp.Rows(0)!tb_concova)
          Case "9" 'CASSA/BANCA
            'prima guarda su tabpaga
            If NTSCInt(dtrTm!tb_concassp) > 0 Then
              oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dtrTm!tb_concassp)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
            Else
              If bDocRicevuto Then
                oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeac.Rows(0)!tb_acontrcas)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
              Else
                oCldPnfa.ValCodiceDb(CoveAggControp(NTSCInt(dttPeve.Rows(0)!tb_contrcas)).ToString, strDittaCorrente, "TABCOVE", "N", "", dttTmp)
              End If
            End If
            lConto = NTSCInt(dttTmp.Rows(0)!tb_concova)

          Case "10" 'RITENUTA
            'La parcellazione non gestisce la CA
            lConto = 0
          Case "11" 'CLIENTE PER STORNO RITENUTA
            'La parcellazione non gestisce la CA
            lConto = 0
          Case "12" 'ENSARCO
            'La parcellazione non gestisce la CA
            lConto = 0
          Case "13" 'CLIENTE PER STORNO ENSARCO
            'La parcellazione non gestisce la CA
            lConto = 0
          Case "14" 'SPESE GENERALI
            'La parcellazione non gestisce la CA
            lConto = 0
          Case "15" 'CASSA COMMERCIALISTI
            'La parcellazione non gestisce la CA
            lConto = 0
        End Select    'Select Case strTipo

        If lConto = 0 Then
          LogWrite(oApp.Tr(Me, 129593420535917968, "ATTENZIONE: non è stato possibile determinare il conti di CA da associare al conto di CG |" & dtrCG!pn_conto.ToString & "| tramite le contropartite vendite/acquisti. l'archivio della contabilità analitica Duplice contabile non verrà aggiornato correttamente."), True)
          Return True
        End If
        oCldPnfa.ValCodiceDb(lConto.ToString, strDittaCorrente, "ANAGCA", "N", "", dttTmp)

        dttMm.Rows.Add(dttMm.NewRow)
        With dttMm.Rows(0)
          'dati variabili 
          dttMm.Rows(0)!mm_contocontr = dttTmp.Rows(0)!ac_conto
          !ac_accperi = dttTmp.Rows(0)!ac_accperi
          !ac_richcena = dttTmp.Rows(0)!ac_richcena
          !ac_richcfam = dttTmp.Rows(0)!ac_richcfam
          !ac_richcomm = dttTmp.Rows(0)!ac_richcomm
          !ac_richdivi = dttTmp.Rows(0)!ac_richdivi
          !ac_richstab = dttTmp.Rows(0)!ac_richstab
          !ac_richarti = dttTmp.Rows(0)!ac_richarti
          !ac_richcli = dttTmp.Rows(0)!ac_richcli
          'valori fissi
          !tb_codcacadd = dtrTm!tb_codcacadd
          !mm_codcena = dtrTm!tm_codcena
          !mm_codcfam = dtrTm!tm_codcfam
          !mm_commeca = dtrTm!tm_commeca
          !mm_subcommeca = " "
          !mm_pmtaskid = 0
          !mm_coddivi = dtrTm!tm_coddivi
          !tb_codstab = dtrTm!tb_codstab
          !mm_codart = " "
          !mm_fase = 0
          !tm_codcli = dtrTm!tm_codcli  'dal doc di magaz.
          !mm_ump = " "
          !mm_quant = 0
          !km_carscar = 0
          !mm_valore = IIf(NTSCDec(dtrCG!pn_dare) <> 0, NTSCDec(dtrCG!pn_dare), NTSCDec(dtrCG!pn_avere))
          !mm_tipork = DBNull.Value
          !mm_anno = 0
          !mm_serie = DBNull.Value
          !mm_numdoc = 0
          !mm_riga = 0
          !lce_rigaa = 0
          !mm_valorev = IIf(NTSCDec(dtrCG!pn_dareval) <> 0, NTSCDec(dtrCG!pn_dareval), NTSCDec(dtrCG!pn_avereval))
        End With
        dttTmp.Clear()
        dttMm.AcceptChanges()
      End If    'If strTipo = "0" Then

      '--------------------------------
      'è possibile che la somma di mm_valore sia diverso per qualche centesimo con l'importo dare/avere della riga di CG
      'questo è dovuto, ad esempio, al fatto che i prezzi unitari di veboll sono con 2 decimali per cui la somma di movmag.mm_valore
      'è diverso dal totale imponibile (problema che si risolverebbe impostando a 4 decimali i prezzi unitari)
      'per limitare l'errore faccio in modo che la parte di CA2 in euro corrisponda al valore di riga di CG
      'per gli importi in valuta il porblema non si pone
      For Each dtrMm As DataRow In dttMm.Rows
        dSomma += NTSCDec(dtrMm!mm_valore)
      Next
      If NTSCDec(dtrCG!pn_dare) <> 0 Then
        If NTSCDec(dtrCG!pn_dare) <> dSomma Then
          dttMm.Rows(0)!mm_valore = NTSCDec(dttMm.Rows(0)!mm_valore) + (NTSCDec(dtrCG!pn_dare) - dSomma)
        End If
      Else
        If NTSCDec(dtrCG!pn_avere) <> dSomma Then
          dttMm.Rows(0)!mm_valore = NTSCDec(dttMm.Rows(0)!mm_valore) + (NTSCDec(dtrCG!pn_avere) - dSomma)
        End If
      End If

      '--------------------------------
      'scrivo le righe di CA2 collegate alla riga di CG
      For Each dtrT As DataRow In dttMm.Rows
        lProgr = oCldPnfa.LegNuma(strDittaCorrente, "PX", " ", 0, True)
        lProgr = oCldPnfa.AggNuma(strDittaCorrente, "PX", " ", 0, lProgr, True, True, strErr)
        If strErr <> "" Then
          ThrowRemoteEvent(New NTSEventArgs("", strErr))
          Return False
        End If

        'determino il conto contropartita, lo prendo da tabpecx
        If NTSCDec(dtrCG!pn_dare) <> 0 Then
          Select Case NTSCStr(dtrT!ac_accperi).ToUpper
            Case "N" : dtrContr = dttAnagcaPecx.Select("ac_conto = " & dttPecx.Rows(0)!tb_acontocoll.ToString)(0)
            Case "S" : dtrContr = dttAnagcaPecx.Select("ac_conto = " & dttPecx.Rows(0)!tb_acontocolls.ToString)(0)
            Case "D" : dtrContr = dttAnagcaPecx.Select("ac_conto = " & dttPecx.Rows(0)!tb_acontocolld.ToString)(0)
          End Select
        Else
          Select Case NTSCStr(dtrT!ac_accperi).ToUpper
            Case "N" : dtrContr = dttAnagcaPecx.Select("ac_conto = " & dttPecx.Rows(0)!tb_dcontocoll.ToString)(0)
            Case "S" : dtrContr = dttAnagcaPecx.Select("ac_conto = " & dttPecx.Rows(0)!tb_dcontocolls.ToString)(0)
            Case "D" : dtrContr = dttAnagcaPecx.Select("ac_conto = " & dttPecx.Rows(0)!tb_dcontocolld.ToString)(0)
          End Select
        End If

        If NTSCInt(dtrT!tb_codcacadd) = 0 Then
          LogWrite(oApp.Tr(Me, 129593420376406250, "ATTENZIONE: nel documento di magazzino ANNO/SERIE/NUMERO |" & _
                   NTSCInt(dtrTm!tm_anno).ToString & "/" & dtrTm!tm_serie.ToString & "/" & _
                   NTSCInt(dtrTm!tm_numdoc).ToString & _
                   "| è indicata una causale di magazzino che non espone la causale di CA. " & _
                   "L'archivio della contabilità analitica Duplice contabile non verrà aggiornato correttamente."), True)
          Return True
        End If

        If dttCaca.Rows.Count = 0 Then
          oCldPnfa.ValCodiceDb(NTSCInt(dtrT!tb_codcacadd).ToString, strDittaCorrente, "TABCACA", "N", "", dttCaca)
        ElseIf NTSCInt(dttCaca.Rows(0)!tb_codcaca) <> NTSCInt(dtrT!tb_codcacadd) Then
          oCldPnfa.ValCodiceDb(NTSCInt(dtrT!tb_codcacadd).ToString, strDittaCorrente, "TABCACA", "N", "", dttCaca)
        End If

        'compilo la prima riga
        dttCA2.Rows.Add(dttCA2.NewRow)
        With dttCA2.Rows(dttCA2.Rows.Count - 1)
          !codditt = strDittaCorrente
          !pa2_progr = lProgr
          !pa2_rigarg = IIf(NTSCDec(dtrCG!pn_dare) <> 0, 1, 2)  'prima riga sempre in DARE
          !pa2_darave = IIf(NTSCDec(dtrCG!pn_dare) <> 0, "D", "A")
          !pa2_idrigacontrop = IIf(NTSCDec(dtrCG!pn_dare) <> 0, 2, 1)
          !pa2_escomp = nEscomp
          !pa2_conto = dtrT!mm_contocontr
          !pa2_codcaca = dtrT!tb_codcacadd
          If NTSCStr(dtrT!ac_richcena) <> "N" Then !pa2_codcena = dtrT!mm_codcena
          If NTSCStr(dtrT!ac_richcfam) <> "N" Then !pa2_codcfam = dtrT!mm_codcfam
          If NTSCStr(dtrT!ac_richcomm) <> "N" Then
            !pa2_commeca = dtrT!mm_commeca
            !pa2_subcommeca = dtrT!mm_subcommeca
            !pa2_idwbs = dtrT!mm_pmtaskid
          End If
          If NTSCStr(dtrT!ac_richdivi) <> "N" Then !pa2_coddivi = dtrT!mm_coddivi
          If NTSCStr(dtrT!ac_richstab) <> "N" Then !pa2_codstab = dtrT!tb_codstab
          If NTSCStr(dtrT!ac_richarti) <> "N" Then
            !pa2_codart = dtrT!mm_codart
            !pa2_fase = dtrT!mm_fase
          End If
          If (NTSCDec(dtrCG!pn_dare) <> 0 And dttCaca.Rows(0)!tb_dqta.ToString = "S") Or _
             (NTSCDec(dtrCG!pn_dare) = 0 And dttCaca.Rows(0)!tb_aqta.ToString = "S") Then
            !pa2_unmis = dtrT!mm_ump
            !pa2_quant = NTSCDec(dtrT!mm_quant) * NTSCDec(dtrT!km_carscar)
            !pa2_quantda = dtrT!mm_quant
          End If
          !pa2_codvalu = dtrTm!tm_valuta
          !pa2_cambio = dtrTm!tm_cambio
          If NTSCStr(dtrT!ac_richcli) <> "N" Then !pa2_codcli = dtrT!tm_codcli
          !pa2_importo = IIf(NTSCDec(dtrCG!pn_dare) <> 0, NTSCDec(dtrT!mm_valore), NTSCDec(dtrT!mm_valore) * -1)
          !pa2_importoda = NTSCDec(dtrT!mm_valore)
          !pa2_impval = IIf(NTSCDec(dtrCG!pn_dareval) <> 0, NTSCDec(dtrT!mm_valorev), NTSCDec(dtrT!mm_valorev) * -1)
          !pa2_impvalda = NTSCDec(dtrT!mm_valorev)
          !pa2_origine = "C"
          If strTipo = "0" Then 'riporto gli estremi di movmag solo sulle righe del corpo, non per le spese di piede
            !pa2_mmtipork = dtrT!mm_tipork
            !pa2_mmanno = dtrT!mm_anno
            !pa2_mmserie = dtrT!mm_serie
            !pa2_mmnumdoc = dtrT!mm_numdoc
            !pa2_mmriga = dtrT!mm_riga
            !pa2_lcerigaa = NTSCInt(dtrT!lce_rigaa)
          End If
          !pa2_datdoc = dtrCG!pn_datdoc
          !pa2_numdoc = dtrCG!pn_numdoc
          !pa2_alfdoc = dtrCG!pn_alfdoc
          !pa2_datreg = dtrCG!pn_datreg
          !pa2_numreg = dtrCG!pn_numreg
          !pa2_riga = dtrCG!pn_riga
          !pa2_datini = dtrCG!pn_datini
          !pa2_datfin = dtrCG!pn_datfin
          !pa2_integr = dtrCG!pn_integr
          '!pa2_contocg = dtrCG!pn_conto    solo su riga di conto collegamento
          !pa2_ultagg = dtrCG!pn_ultagg
          !pa2_opnome = oApp.User.Nome
        End With    'With dttCA2.Rows(dttCA2.Rows.Count - 1)

        'se quantità e/o valore <> 0 bene, altrimenti non salvo la riga
        If NTSCDec(dttCA2.Rows(dttCA2.Rows.Count - 1)!pa2_quant) = 0 And _
           NTSCDec(dttCA2.Rows(dttCA2.Rows.Count - 1)!pa2_importo) = 0 Then
          dttCA2.Rows(dttCA2.Rows.Count - 1).Delete()
        Else
          'compilo la seconda riga
          dttCA2.Rows.Add(dttCA2.NewRow)
          With dttCA2.Rows(dttCA2.Rows.Count - 1)
            !codditt = strDittaCorrente
            !pa2_progr = lProgr
            !pa2_rigarg = IIf(NTSCDec(dtrCG!pn_dare) <> 0, 2, 1)  'prima riga sempre in DARE
            !pa2_darave = IIf(NTSCDec(dtrCG!pn_dare) <> 0, "A", "D")
            !pa2_idrigacontrop = IIf(NTSCDec(dtrCG!pn_dare) <> 0, 1, 2)
            !pa2_escomp = nEscomp
            !pa2_conto = dtrContr!ac_conto
            !pa2_codcaca = dtrT!tb_codcacadd
            If NTSCStr(dtrContr!ac_richcena) <> "N" Then !pa2_codcena = dtrT!mm_codcena
            If NTSCStr(dtrContr!ac_richcfam) <> "N" Then !pa2_codcfam = dtrT!mm_codcfam
            If NTSCStr(dtrContr!ac_richcomm) <> "N" Then
              !pa2_commeca = dtrT!mm_commeca
              !pa2_subcommeca = dtrT!mm_subcommeca
              !pa2_idwbs = dtrT!mm_pmtaskid
            End If
            If NTSCStr(dtrContr!ac_richdivi) <> "N" Then !pa2_coddivi = dtrT!mm_coddivi
            If NTSCStr(dtrContr!ac_richstab) <> "N" Then !pa2_codstab = dtrT!tb_codstab
            If NTSCStr(dtrContr!ac_richarti) <> "N" Then
              !pa2_codart = dtrT!mm_codart
              !pa2_fase = dtrT!mm_fase
            End If
            If (NTSCDec(dtrCG!pn_dare) <> 0 And dttCaca.Rows(0)!tb_aqta.ToString = "S") Or _
               (NTSCDec(dtrCG!pn_dare) = 0 And dttCaca.Rows(0)!tb_dqta.ToString = "S") Then
              !pa2_unmis = dtrT!mm_ump
              !pa2_quant = NTSCDec(dtrT!mm_quant) * NTSCDec(dtrT!km_carscar) * -1 'inverto di segno rispetto alla riga sopra
              !pa2_quantda = dtrT!mm_quant
            End If
            !pa2_codvalu = dtrTm!tm_valuta
            !pa2_cambio = dtrTm!tm_cambio
            If NTSCStr(dtrT!ac_richcli) <> "N" Then !pa2_codcli = dtrT!tm_codcli
            !pa2_importo = IIf(NTSCDec(dtrCG!pn_dare) <> 0, NTSCDec(dtrT!mm_valore) * -1, NTSCDec(dtrT!mm_valore))
            !pa2_importoda = NTSCDec(dtrT!mm_valore)
            !pa2_impval = IIf(NTSCDec(dtrCG!pn_dareval) <> 0, NTSCDec(dtrT!mm_valorev) * -1, NTSCDec(dtrT!mm_valorev))
            !pa2_impvalda = NTSCDec(dtrT!mm_valorev)
            !pa2_origine = "C"
            '!pa2_mmtipork = dtrT!mm_tipork     solo su riga principale
            '!pa2_mmanno = dtrT!mm_anno
            '!pa2_mmserie = dtrT!mm_serie
            '!pa2_mmnumdoc = dtrT!mm_numdoc
            '!pa2_mmriga = dtrT!mm_riga
            '!pa2_lcerigaa = NTSCInt(dtrT!lce_rigaa)
            !pa2_datdoc = dtrCG!pn_datdoc
            !pa2_numdoc = dtrCG!pn_numdoc
            !pa2_alfdoc = dtrCG!pn_alfdoc
            !pa2_datreg = dtrCG!pn_datreg
            !pa2_numreg = dtrCG!pn_numreg
            !pa2_riga = dtrCG!pn_riga
            !pa2_datini = dtrCG!pn_datini
            !pa2_datfin = dtrCG!pn_datfin
            !pa2_integr = dtrCG!pn_integr
            !pa2_contocg = dtrCG!pn_conto
            !pa2_ultagg = dtrCG!pn_ultagg
            !pa2_opnome = oApp.User.Nome
          End With    'With dttCA2.Rows(dttCA2.Rows.Count - 1)
        End If    'If NTSCDec(dttCA2.Rows(dttCA2.Rows.Count - 1)!pa2_quant) = 0 An ...
        dttCA2.AcceptChanges()
      Next    'For Each dtrT As DataRow In dttMm.Rows

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
      dttCaca.Clear()
      dttTmp.Clear()
    End Try
  End Function


  Public Overridable Function GetTabcivaRow(ByVal nCodiva As Integer, ByRef dttOut As DataTable) As Boolean
    Dim dtrT() As DataRow = Nothing
    Try
      dttOut = New DataTable
      dttOut = dsTabciva.Tables("TABCIVA").Clone
      dtrT = dsTabciva.Tables("TABCIVA").Select("tb_codciva = " & nCodiva.ToString)
      If dtrT.Length > 0 Then
        dttOut.ImportRow(dtrT(0))
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


  Public Overridable Function CheckForzaContabConScadenUpd(ByRef dtrTm As DataRow, ByRef dttScadRielab As DataTable, _
                                                           ByVal bDelEffettiNoPres As Boolean, _
                                                           ByVal bContabAncheConScadSald As Boolean, _
                                                           ByRef strErr As String) As Boolean
    'dtrTm = riga di testmag/testpar in analisi
    'dttScadRielab scadenze collegate alla fattura

    'giro effetti senza chiusura cliente: la scadenza cliente è aperta, ma ho emesso gli effetti: su scaden del cli avro:
    'sc_fldis = 'S' AND sc_numdist = 0 AND sc_flsaldato = 'N' AND sc_darave = 'D' AND (sc_tippaga = 1 OR sc_tippaga = 2)
    Dim dttPN As New DataTable
    Dim dttTmp As New DataTable

    Dim bSaldata As Boolean = False
    Dim dTotScadSaldate As Decimal = 0
    Dim dTotScadNoSald As Decimal = 0
    Dim dTotScadSaldatev As Decimal = 0
    Dim dTotScadNoSaldv As Decimal = 0
    Dim dFtResiduo As Decimal = 0
    Dim dFtResiduov As Decimal = 0
    Try
      dttScadRielab.Columns.Add("xx_tipo", GetType(String))
      dttScadRielab.Columns.Add("xx_operaz", GetType(String))
      dttScadRielab.Columns.Add("xx_mantieni", GetType(String))

      For Each dtrT As DataRow In dttScadRielab.Rows
        dtrT!xx_tipo = ""
        dtrT!xx_operaz = ""
        dtrT!xx_mantieni = "N"
      Next
      dttScadRielab.AcceptChanges()

CANC_EFFETTI_EM_NO_PRES:
      If bDelEffettiNoPres Then
        'se la scadenza è saldata per emissione effetti, memorizzerò che dovrò cancellare la reg. di emissione effetti
        'SOLO se la registrazione è di tipo 'EFFETTI a CLIENTE'
        For Each dtrT As DataRow In dttScadRielab.Select("sc_flsaldato = 'S' OR (sc_fldis = 'S' AND sc_numdist = 0 AND sc_flsaldato = 'N' AND sc_darave = 'D' AND (sc_tippaga = 1 OR sc_tippaga = 2))")

          If bModuliAcquistati Then
            If NTSCStr(dtrT!sc_flsaldato) = "S" Then
              'emissione effetti con chiusura cliente (ma anche possibile solo normale incasso/pagamento)
              If NTSCInt(dtrTm!tm_numrgin) <> 0 And NTSCDate(dtrT!sc_dtsaldato) = NTSCDate(dtrTm!tm_datregin) And NTSCInt(dtrT!sc_rgsaldato) = NTSCInt(dtrTm!tm_numrgin) Then
                'è la reg. dell'incasso contestuale alla fattura: proseguo
                dtrT!xx_tipo = "INC"
                Continue For
              End If
              If NTSCInt(dtrTm!tm_numregom) <> 0 And NTSCDate(dtrT!sc_dtsaldato) = NTSCDate(dtrTm!tm_datregom) And NTSCInt(dtrT!sc_rgsaldato) = NTSCInt(dtrTm!tm_numregom) Then
                'è la reg. degli omaggi contestuale alla fattura: proseguo
                dtrT!xx_tipo = "OMA"
                Continue For
              End If

              dtrT!xx_operaz = "SALD" 'scadenza saldata non cancellabile

              If Not oCldPnfa.GetRegPrinot(strDittaCorrente, NTSCDate(dtrT!sc_dtsaldato).ToShortDateString, NTSCInt(dtrT!sc_rgsaldato), NTSCStr(dtrT!sc_integr), dttPN) Then Return False 'reg. del pagamento
              If dttPN.Rows.Count = 2 Then
                'solo se è la reg. di emissione effetti di tipo DARE AD AVERE secco potrò cancellarla
                If NTSCInt(dttPN.Rows(0)!pn_conto) = NTSCInt(dtrT!sc_conto) And NTSCInt(dttPN.Rows(1)!pn_conto) = lConEff Then
                  dtrT!xx_tipo = "EFF"  'emesso gli effetti
                  'posso cancellare solo se la scadenza non è stata presentata in banca
                  'la partita è la stessa del cliente ma intestata al conto 'effetti attivi' e in sc_controp c'è il conto cliente
                  'cerco la reg. di emissione effetti
                  If Not oCldPnfa.GetScadenFattura(strDittaCorrente, lConEff, NTSCInt(dtrT!sc_annpar), NTSCStr(dtrT!sc_alfpar), _
                                                   NTSCInt(dtrT!sc_numpar), False, NTSCStr(dtrT!sc_integr), -1, dttTmp, "", 0, NTSCInt(dtrT!sc_numrata)) Then
                    Return False
                  End If
                  If dttTmp.Rows.Count > 0 AndAlso NTSCStr(dttTmp.Rows(0)!sc_flsaldato) = "N" AndAlso dttPN.Select("pn_fllg = 'S'").Length = 0 Then
                    'memorizzo che dovrò cancellare la reg. di emissione effetti
                    dtrT!xx_operaz = "DEL;CG;" & NTSCDate(dttTmp.Rows(0)!sc_datreg).ToShortDateString & ";" & NTSCInt(dttTmp.Rows(0)!sc_numreg).ToString
                  End If
                  dttTmp.Clear()
                End If
              End If
            Else
              'scadenza normale non saldata
              If bGiroEffettiNoChisCli Then
                If NTSCStr(dtrT!sc_fldis) = "S" And NTSCInt(dtrT!sc_numdist) = 0 And NTSCStr(dtrT!sc_darave) = "D" And (NTSCInt(dtrT!sc_tippaga) = 1 Or NTSCInt(dtrT!sc_tippaga) = 2) Then
                  dtrT!xx_tipo = "EFF"  'emesso gli effetti
                  'è una emissione effetti senza chiusura cli
                  'cerco la reg. di emissione effetti
                  If Not oCldPnfa.GetScadenFattura(strDittaCorrente, lConEff, NTSCInt(dtrT!sc_annpar), NTSCStr(dtrT!sc_alfpar), _
                                                   NTSCInt(dtrT!sc_numpar), False, NTSCStr(dtrT!sc_integr), -1, dttTmp, "", 0, NTSCInt(dtrT!sc_numrata)) Then
                    Return False
                  End If
                  If Not oCldPnfa.GetRegPrinot(strDittaCorrente, NTSCDate(dttTmp.Rows(0)!sc_datreg).ToShortDateString, NTSCInt(dttTmp.Rows(0)!sc_numreg), NTSCStr(dttTmp.Rows(0)!sc_integr), dttPN) Then Return False 'reg. del pagamento
                  If dttTmp.Rows.Count > 0 AndAlso NTSCStr(dttTmp.Rows(0)!sc_flsaldato) = "N" AndAlso dttPN.Select("pn_fllg = 'S'").Length = 0 Then
                    'memorizzo che dovrò cancellare la reg. di emissione effetti
                    dtrT!xx_operaz = "DEL;CG;" & NTSCDate(dttTmp.Rows(0)!sc_datreg).ToShortDateString & ";" & NTSCInt(dttTmp.Rows(0)!sc_numreg).ToString
                  Else
                    dtrT!xx_operaz = "SALD" 'scadenza saldata non cancellabile
                  End If
                  dttTmp.Clear()
                End If
              End If    'If bGiroEffettiNoChisCli Then
            End If    'If NTSCStr(dtrT!sc_flsaldato) = "S" Then

          Else
            'no modulo CG: non ho record in prinot.
            'sono gestite solo le scadenze attive e vengono sempre contabilizzate con conto = conto effetti
            'se è stata emessa una distinta le scadenze sono sempre saldate, quindi non posso proseguire
            If NTSCStr(dtrT!sc_flsaldato) = "S" Then Return False
          End If    'If bModuliAcquistati Then
        Next

        'se sono rimaste delle scadenze potenzialmente saldate
        'e non ho impostato di continuare la contabilizzazione anche in presenza di scadenze saldate
        'blocco la contabilizzazione
        If bContabAncheConScadSald = False Then
          If dttScadRielab.Select("xx_operaz = 'SALD'").Length > 0 Then Return False 'alcune scadenze saldate NON CANCELLABILI
        End If

      End If    'If bDelEffettiNoPres Then

CALC_SCAD_SALDATE:
      If bContabAncheConScadSald Then
        'memorizzo il residuo da incassare/pagare
        dFtResiduo = (NTSCDec(dtrTm!tm_totdoc) - NTSCDec(dtrTm!tm_totomag) - NTSCDec(dtrTm!tm_abbuono)) - (NTSCDec(dtrTm!tm_pagato) + NTSCDec(dtrTm!tm_pagato2) - NTSCDec(dtrTm!tm_resto))
        dFtResiduov = NTSCDec(dtrTm!tm_totdocv) - NTSCDec(dtrTm!tm_totomagv) - NTSCDec(dtrTm!tm_abbuonov)
        If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "(" Then
          'documento ricevuto
          dFtResiduo *= -1
          dFtResiduov *= -1
        End If

        'per scadenze saldate si intendono anche quelle sc_flsaldato = 'N' ma collegate a BNIDGEID (sc_codincdiff <> 0)
        'devo anche scartare le emissioni effetti senza chiusura cliente
        For Each dtrT As DataRow In dttScadRielab.Rows
          If NTSCInt(dtrTm!tm_numrgin) <> 0 And NTSCDate(dtrT!sc_dtsaldato) = NTSCDate(dtrTm!tm_datregin) And NTSCInt(dtrT!sc_rgsaldato) = NTSCInt(dtrTm!tm_numrgin) Then
            'è la reg. dell'incasso contestuale alla fattura: proseguo
            dtrT!xx_tipo = "INC"
            Continue For
          End If
          If NTSCInt(dtrTm!tm_numregom) <> 0 And NTSCDate(dtrT!sc_dtsaldato) = NTSCDate(dtrTm!tm_datregom) And NTSCInt(dtrT!sc_rgsaldato) = NTSCInt(dtrTm!tm_numregom) Then
            'è la reg. degli omaggi contestuale alla fattura: proseguo
            dtrT!xx_tipo = "OMA"
            Continue For
          End If

          If NTSCStr(dtrT!sc_flsaldato) = "N" Then
            bSaldata = False

            If NTSCInt(dtrT!sc_codincdiff) <> 0 Then
              bSaldata = True
            Else
              If bGiroEffettiNoChisCli Then
                If NTSCStr(dtrT!sc_fldis) = "S" And NTSCInt(dtrT!sc_numdist) = 0 And NTSCStr(dtrT!sc_darave) = "D" And (NTSCInt(dtrT!sc_tippaga) = 1 Or NTSCInt(dtrT!sc_tippaga) = 2) Then
                  'se è collegata ad uan reg di emissione effetti senza chiusura cli ...
                  'non potrei cancellarla, a meno che copo qui sopra non ho impostato che l'emissione effetti dovrà essere cancellata
                  If NTSCStr(dtrT!xx_operaz).StartsWith("DEL;CG") = False Then
                    bSaldata = True
                  End If
                End If
              End If
            End If
          Else
            bSaldata = True

            If NTSCStr(dtrT!xx_operaz).StartsWith("DEL;CG") Then
              bSaldata = False
            End If
          End If    'If NTSCStr(dtrT!sc_flsaldato) = "N" Then

          If bSaldata Then
            dTotScadSaldate += NTSCDec(dtrT!sc_importo)
            dTotScadSaldatev += NTSCDec(dtrT!sc_impval)
            dtrT!xx_operaz = "SALD" 'scadenza saldata non cancellabile
          Else
            dTotScadNoSald += NTSCDec(dtrT!sc_importo)
            dTotScadNoSaldv += NTSCDec(dtrT!sc_impval)

            If NTSCStr(dtrT!xx_operaz) = "" Then
              dtrT!xx_operaz = "DEL;SC;"  'potrebbe essere una scadenza cancellabile in ricontabilizzazione
            Else
              'può essere solo una scadenza saldata per emissione effetti: 
              'verrà già cancellata perchè marcata come dtrT!xx_operaz = "DEL;CG"
            End If
          End If
        Next

        'se l'importo saldato è maggiore del residuo della fattura, non posso contabilizzare
        If dtrTm!tm_tipork.ToString = "K" Or dtrTm!tm_tipork.ToString = "J" Or dtrTm!tm_tipork.ToString = "L" Or dtrTm!tm_tipork.ToString = "(" Then
          'documento ricevuto
          If dTotScadSaldate > 0 Then
            'note di accred
            dFtResiduo *= -1 : dFtResiduov *= -1
            If dTotScadSaldate > dFtResiduo Or dTotScadSaldatev > dFtResiduov Then
              strErr = "10"
              Return False
            End If
          Else
            'normali fatture
            If dTotScadSaldate < dFtResiduo Or dTotScadSaldatev < dFtResiduov Then
              strErr = "10"
              Return False
            End If
          End If
        Else
          If dTotScadSaldate < 0 Then
            'note di accred
            dFtResiduo *= -1 : dFtResiduov *= -1
            If dTotScadSaldate < dFtResiduo Or dTotScadSaldatev < dFtResiduov Then
              strErr = "10"
              Return False
            End If
          Else
            'normali fatture
            If dTotScadSaldate > dFtResiduo Or dTotScadSaldatev > dFtResiduov Then
              strErr = "10"
              Return False
            End If
          End If
        End If
      End If    'If bContabAncheConScadSald Then

      Return True 'posso continuare la contabilizzazione

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      dttPN.Clear()
      dttTmp.Clear()
    End Try
  End Function


  Public Overridable Function ElaboraTipo_TrattaScadenzeSaldate(ByRef dtrTm As DataRow, ByRef dttSc As DataTable, _
                                                                ByRef dttScadRielab As DataTable, ByVal strDatregftOld As String, _
                                                                ByVal lNumregftOld As Integer, ByVal strDatreginOld As String, _
                                                                ByVal lNumreginOld As Integer, ByVal strDatregomOld As String, _
                                                                ByVal lNumregomOld As Integer) As Boolean
    'in rielaborazione, modifico le nuove scadenze per fare in modo che possano convivere con quelle esistenti non cancellabili
    Dim nMaxrata As Integer = 0
    Dim strT() As String = Nothing
    Dim dTotScadOkOld As Decimal = 0    'scadenze vecchie che devo mantenere perchè pagate o con estremi scadenze non variate (per cui posso mantenere)
    Dim dTotScadOkOldv As Decimal = 0  'dTotScadOkOldv in valuta
    Dim dResidNew As Decimal = 0
    Dim dResidNewv As Decimal = 0  'dResidNew in valuta
    Dim dTmp4 As Decimal = 0
    Dim dTmp5 As Decimal = 0  'dTmp4 in valuta
    Dim i As Integer = 0
    Dim n As Integer = 0
    Dim dTotScadNew As Decimal = 0
    Dim dTotScadNewv As Decimal = 0
    Dim strRateOK As String = ","      'nuove rate da mantenere perchè uguali alle vecchie
    Dim dtrT1() As DataRow = Nothing
    Dim bIncPag As Boolean = False    'se true, nelle nuove scadenze la rata 1 è relativa ad incasso e/o abbuono
    Dim nRataIncPagOld As Integer = 0 'data di incasso/pagamento/abbuono della vecchia fattura
    Dim dTotScadIni As Decimal = 0
    Try

      If Not (Not dttScadRielab Is Nothing AndAlso dttScadRielab.Columns.Contains("xx_operaz")) Then Return True
      If dttScadRielab.Rows.Count = 0 Then Return True

      'dttSC.AcceptChanges()NO, altrimenti non salva scaden!!!
      If dttSc.Select("", "", DataViewRowState.CurrentRows).Length = 0 Then Return True 'conto non gestito a partire/scadenze: esco

      If NTSCInt(dttSc.Rows(0)!sc_numrata) = 1 And NTSCDec(dttSc.Rows(0)!sc_importo) = ((NTSCDec(dtrTm!tm_pagato) + NTSCDec(dtrTm!tm_pagato2) - NTSCDec(dtrTm!tm_resto) + NTSCDec(dtrTm!tm_abbuono)) * NTSCInt(IIf(NTSCStr(dttSc.Rows(0)!sc_darave) = "A", -1, 1))) Then
        bIncPag = True
      End If

      '--------------------
      'intanto aggiorno le vecchia escadenze con i dati delle nuove
      For Each dtrT As DataRow In dttScadRielab.Rows
        If ((NTSCInt(dtrT!sc_rgsaldato) = lNumreginOld And NTSCDate(dtrT!sc_rgsaldato) = NTSCDate(strDatreginOld)) Or _
           (NTSCInt(dtrT!sc_rgsaldato) = lNumregomOld And NTSCDate(dtrT!sc_rgsaldato) = NTSCDate(strDatregomOld))) And _
           NTSCInt(dtrT!sc_rgsaldato) <> 0 Then
          nRataIncPagOld = NTSCInt(dtrT!s_numrata)
        End If

        'su quelle vecchie da mantenere fare update di datreg, numreg, se non saldate cambiare anche abi, cab, ecc 
        dtrT!sc_datreg = dttSc.Rows(0)!sc_datreg
        dtrT!sc_numreg = dttSc.Rows(0)!sc_numreg
        dtrT!sc_causale = dttSc.Rows(0)!sc_causale
        dtrT!sc_descr = dttSc.Rows(0)!sc_descr
        dtrT!sc_datdoc = dttSc.Rows(0)!sc_datdoc
        dtrT!sc_alfdoc = dttSc.Rows(0)!sc_alfdoc
        dtrT!sc_numdoc = dttSc.Rows(0)!sc_numdoc
        dtrT!sc_codbanc = dttSc.Rows(0)!sc_codbanc
        dtrT!sc_codcage = dttSc.Rows(0)!sc_codcage
        dtrT!sc_commeca = dttSc.Rows(0)!sc_commeca
        dtrT!sc_subcommeca = dttSc.Rows(0)!sc_subcommeca
        dtrT!sc_coddest = dttSc.Rows(0)!sc_coddest
        dtrT!sc_impfat = dttSc.Rows(0)!sc_impfat
        dtrT!sc_numprot = dttSc.Rows(0)!sc_numprot
        dtrT!sc_alfpro = dttSc.Rows(0)!sc_alfpro
        'mantengo volutamente il vecchio codpaga, tippaga anche su scad. non saldate!!!!!
        If NTSCStr(dtrT!xx_operaz) <> "SALD" Then
          dtrT!sc_abi = dttSc.Rows(0)!sc_abi
          dtrT!sc_cab = dttSc.Rows(0)!sc_cab
          dtrT!sc_banc1 = dttSc.Rows(0)!sc_banc1
          dtrT!sc_banc2 = dttSc.Rows(0)!sc_banc2
          dtrT!sc_numcc = dttSc.Rows(0)!sc_numcc
          dtrT!sc_tipcvs = dttSc.Rows(0)!sc_tipcvs
          dtrT!sc_cin = dttSc.Rows(0)!sc_cin
          dtrT!sc_prefiban = dttSc.Rows(0)!sc_prefiban
          dtrT!sc_iban = dttSc.Rows(0)!sc_iban
          dtrT!sc_swift = dttSc.Rows(0)!sc_swift
        End If
      Next

      '----------------------------
      'se devo cancellare delle emissioni effetti (non presentati in banca), lo scrivo nel file di log
      For Each dtrT As DataRow In dttScadRielab.Select("xx_operaz like 'DEL;%'")
        strT = NTSCStr(dtrT!xx_operaz).Split(";"c)
        If strT(0) = "DEL" And strT(1) = "CG" And NTSCStr(dtrT!xx_tipo) = "EFF" Then
          'non do il messaggio: il file di log sarebbe troppo prolisso
          'LogWrite(oApp.Tr(Me, 130373869084757225, _
          '          "ATTENZIONE: documento |" & strEstremidoc & "|: vecchia scadenza del |" & NTSCDate(dtrT!sc_datsca).ToShortDateString & _
          '          "| di importo |" & NTSCDec(dtrT!sc_importo).ToString(oApp.FormatImporti) & _
          '          "|. A seguito della ricontabilizzazione del documento, è stata cancellata la registrazione " & _
          '          "di EMISSIONE EFFETTI (non presentati in banca)."), True)
        End If
        If strT(0) = "DEL" And strT(1) = "SC" Then
          'scadenza non saldata che devo cancellare: se però le nuove scadenze sono uguali alle vecchie, non farò la delete
          'ma aggiornerò semplicemente sc_datreg e sc_numreg delle vecchie scad
        End If
      Next

      '----------------------------
      'determino quali scadenze vecchie sono da tenere e quali da cancellare
      'xx_operaz = 'SALD' OR xx_mantieni = 'S' = sono da mantenere
      For Each dtrT As DataRow In dttScadRielab.Rows
        dtrT1 = dttSc.Select("sc_numrata = " & CStrSQL(dtrT!sc_numrata.ToString) & _
                            " AND sc_datsca = " & CDataSQL(NTSCDate(dtrT!sc_datsca)) & _
                            " AND sc_codpaga = " & CDblSQL(NTSCDec(dtrT!sc_codpaga)) & _
                            " AND sc_tippaga = " & CDblSQL(NTSCDec(dtrT!sc_tippaga)) & _
                            " AND sc_importo = " & CDblSQL(NTSCDec(dtrT!sc_importo)) & _
                            " AND sc_darave = " & CStrSQL(NTSCStr(dtrT!sc_darave)) & _
                            " AND sc_impval = " & CDblSQL(NTSCDec(dtrT!sc_impval)))
        If dtrT1.Length = 0 Then
          dtrT!xx_mantieni = "N" 'scadenza da cancellare
        Else
          If NTSCStr(dtrT!xx_tipo) = "OMA" Or NTSCStr(dtrT!xx_tipo) = "INC" Then
            'incasso/pagamento contestuale alla fattura: non verrà mai mantenuto perchè verrà ricreato sempre come standard di programma
            dtrT!xx_mantieni = "N" 'scadenza da cancellare
          Else
            If NTSCStr(dtrT!xx_operaz) = "SALD" Then
              dtrT!xx_mantieni = "S"
              strRateOK += dtrT!sc_numrata.ToString & ","
            Else
              'anche se la scadenza è uguale, non essendo saldata non la mantengo
              'diversamente evrei dei problemi in caso di:
              'fattura di 1000 non incassata
              'contabilizzo ed incasso 100 direttamente in CG
              'modifico fattura indicando incassato di 100
              'ricontabilizzo senza contabilizzare incassi: non riesco a ricollegare l'incasso di CG con quello di testmag
              'preferisco ipotizzare che sia un altra quota di incassato e per far lavorare correttamente devo cancellare tutte le scadenze non saldate
              dtrT!xx_mantieni = "N" 'scadenza da cancellare
            End If
          End If
        End If

        'le scadenze di tipo 'emissione effetti' che ho già deciso di cancellare, le ignoro
        If NTSCStr(dtrT!xx_operaz).StartsWith("DEL;CG;") Then
          dtrT!xx_mantieni = "N" 'scadenza da cancellare
          strRateOK = strRateOK.Replace("," & dtrT!sc_numrata.ToString & ",", ",")
        End If
      Next    'For Each dtrT As DataRow In dttScadRielab.Rows
      dttScadRielab.AcceptChanges()

      '----------------------------
      'messaggi di log per l'operatore
      'For Each dtrT As DataRow In dttScadRielab.Select("xx_mantieni = 'N' AND xx_operaz = 'DEL;SC;' AND xx_tipo <> 'INC' AND xx_tipo <> 'OMA'")
      'loggo le scadenze che cancellerò perchè non saldate
      'non serve, basta il messaggio sotto
      'LogWrite(oApp.Tr(Me, 130374465549685418, _
      '                  "ATTENZIONE: documento |" & strEstremidoc & "|: vecchia scadenza del |" & NTSCDate(dtrT!sc_datsca).ToShortDateString & _
      '                  "| di importo |" & NTSCDec(dtrT!sc_importo).ToString(oApp.FormatImporti) & _
      '                  "|. A seguito della ricontabilizzazione del documento, è stata cancellata la scadenza " & _
      '                  "NON SALDATA con rata n. |" & NTSCStr(dtrT!sc_numrata) & "|"), True)
      'Next

      If dttScadRielab.Select("xx_operaz = 'SALD' OR xx_mantieni = 'S'").Length > 0 Then
        LogWrite(oApp.Tr(Me, 130374613370936349, _
                          "ATTENZIONE: rielaborazione documento |" & strEstremidoc & _
                          "|: sono presenti delle scadenze SALDATE che non possono essere cancellate. " & _
                          "Le scadenze verranno ricalcolate ed in contabilità potrebbero non essere uguali " & _
                          "a quelle del documento. Vedi reg. CG del |" & NTSCDate(dttSc.Rows(0)!sc_datreg).ToShortDateString & _
                          "| num. |" & dttSc.Rows(0)!sc_numreg.ToString & "|."), True)
      End If

      '----------------------------
Ricalcolo_nuove_scadenze:
      'devo ricalcolare le scadenze prendendo come totale da dividere l'importo residuo diminuito delle scadenze vecchie che ho deciso di mantenere

      '-- totale delle vecchie scadenze che ho deciso di mantenere 
      '(escuso abbuoni e pagamenti sul vecchio testmag EFFETTIVARENTE REGISTRATI IN CG COME INCASSO/PAGAMENTO)
      dTotScadOkOld = 0 : dTotScadOkOld = 0
      nMaxrata = 0
      For Each dtrT As DataRow In dttScadRielab.Select("xx_operaz = 'SALD' OR xx_mantieni = 'S'")
        If nMaxrata < NTSCInt(dtrT!sc_numrata) Then nMaxrata = NTSCInt(dtrT!sc_numrata) 'memorizzo la rata più alta

        If NTSCStr(dtrT!xx_tipo) = "OMA" Or NTSCStr(dtrT!xx_tipo) = "INC" Then
          'scarto l'incasso/abbuono contestuale vecchio: se prima era pagata ed ora no, non devo considerare questo pagamento
          'nel caso, invece, che l'importo pagato/abbuono venga mantenuto o cambiato in fattura, verrà comunque ricreato
        Else
          dTotScadOkOld += NTSCDec(dtrT!sc_importo) : dTotScadOkOldv += NTSCDec(dtrT!sc_impval)
        End If
      Next

      '-- residuo da pagare nuove scadenze (escuso abbuoni e pagamenti su testmag)
      dTotScadNew = 0 : dTotScadNewv = 0
      For Each dtrT As DataRow In dttSc.Rows
        dTotScadIni += NTSCDec(dtrT!sc_importo)
        If bIncPag And NTSCInt(dtrT!sc_numrata) = 1 Then
          'nei nuovi documenti la rata di incasso/abbuono è sempre la n. 1
          'devo scartare la scadenza di abbuono/incassato contestuale
          strRateOK += "1,"
        ElseIf NTSCInt(dtrT!sc_codpaga) = 0 Then
          'nuova scadenza di omaggio contestuale al doc.
          'devo scartare la scadenza di abbuono/incassato contestuale
          strRateOK += NTSCInt(dtrT!sc_numrata) & ","
        Else
          dTotScadNew += NTSCDec(dtrT!sc_importo) : dTotScadNewv += NTSCDec(dtrT!sc_impval)
        End If
      Next
      '-- nuovo residuo da pagare (se nuove scadenze minori di vecchie: non è possibile, è stato bloccato prima)
      dResidNew = dTotScadNew - dTotScadOkOld
      dResidNewv = dTotScadNewv - dTotScadOkOldv

      dTmp4 = dResidNew
      dTmp5 = dResidNewv

      '-- ora ricalcolo gli importi delle nuove scadenze (se le nuove scadenze non servono, le cancello)
      'devo mantenere sempre la nuova scadenza di omaggi/abbuoni/incassato contestuale
      Dim dtrSca() As DataRow = Nothing

      If strRateOK.Length > 2 Then
        strRateOK = strRateOK.Substring(1, strRateOK.Length - 2)

        'rimuovo le nuove rate che sicuramente non serviranno
        For Each dtrT As DataRow In dttSc.Select("sc_numrata IN (" & strRateOK & ")")
          If NTSCInt(dtrT!sc_numrata) = 1 And bIncPag Then
            'nuova scadenza di incasso e/o abbuono contestuale al doc. mi servirà dopo
          ElseIf NTSCInt(dtrT!sc_codpaga) = 0 Then
            'nuova scadenza di omaggio contestuale al doc. mi servirà dopo
          Else
            dtrT.Delete() 'rimuovo anche la nuova scadenza, tanto verrà mantenuta quella vecchia
          End If
        Next
      End If

      'ora ho solo le scadenze nuove che devo modificare (veramente ho anche la rata 1 di incasso/abbuono contestuale 
      'e la rata di omaggio con sc_codpaga = 0) quelle devo lasciarle stare
      dtrSca = dttSc.Select("", "sc_numrata ASC")
      n = 0
      If bIncPag Then
        n = 1 'è la rata nuova di incasso o abbuono: va mantenuta
        'cerco l'eventuale nuova rata di omaggi: è quella con sc_codpaga = 0
        'può essere la rata 1 o, se c'è incasso/abbuono, la rata 2
        If dtrSca.Length > 1 Then
          If NTSCInt(dtrSca(1)!sc_codpaga) = 0 Then n = 2
        End If
      Else
        'cerco l'eventuale nuova rata di omaggi: è quella con sc_codpaga = 0
        'può essere la rata 1 o, se c'è incasso/abbuono, la rata 2
        If dtrSca.Length > 0 Then
          If NTSCInt(dtrSca(0)!sc_codpaga) = 0 Then n = 1
        End If
      End If

      For i = dtrSca.Length - 1 To n Step -1
        If dResidNew = 0 Then
          'la nuova scadenza è coperta dalla vecchia già saldata: la cancello
          dtrSca(i).Delete()
        Else
          If i > n Then
            'proporzionalizzo
            dtrSca(i)!sc_importo = ArrDbl((dResidNew / dTotScadNew * NTSCDec(dtrSca(i)!sc_importo)), oCldPnfa.TrovaNdec(0))
            dtrSca(i)!sc_importoda = NTSCDec(dtrSca(i)!sc_importo) * NTSCDec(IIf(NTSCStr(dttSc.Rows(0)!sc_darave) = "D", 1, -1))
            If dTotScadNewv <> 0 Then
              dtrSca(i)!sc_impval = ArrDbl((dResidNewv / dTotScadNewv * NTSCDec(dtrSca(i)!sc_impval)), oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
              dtrSca(i)!sc_impvalda = NTSCDec(dtrSca(i)!sc_impval) * NTSCDec(IIf(NTSCStr(dttSc.Rows(0)!sc_darave) = "D", 1, -1))
            Else
              dtrSca(i)!sc_impval = 0
              dtrSca(i)!sc_impvalda = 0
            End If
            dTmp4 -= NTSCDec(dtrSca(i)!sc_importo)
            dTmp5 -= NTSCDec(dtrSca(i)!sc_impval)
          Else
            'vado per differenza
            dtrSca(i)!sc_importo = ArrDbl(dTmp4, oCldPnfa.TrovaNdec(0))
            dtrSca(i)!sc_importoda = dTmp4 * NTSCDec(IIf(NTSCStr(dttSc.Rows(0)!sc_darave) = "D", 1, -1))
            dtrSca(i)!sc_impval = ArrDbl(dTmp5, oCldPnfa.TrovaNdec(NTSCInt(dtrTm!tm_valuta)))
            dtrSca(i)!sc_impvalda = dTmp5 * NTSCDec(IIf(NTSCStr(dttSc.Rows(0)!sc_darave) = "D", 1, -1))
          End If
          'rinumero anche le rate delle nuove scadenze, diversamente poteri avere delle chiavi duplicate causate dai record delle vecchie scad
          nMaxrata += 1
          dtrSca(i)!sc_numrata = nMaxrata
        End If
      Next
      'dttSc.AcceptChanges()NO, altrimenti non salva scaden!!!

      'se le nuove rate hanno numero uguale a quello delle vecchie, riassegno sc_numrata
      For Each dtrT As DataRow In dttSc.Rows
        If dttScadRielab.Select("sc_numrata = " & NTSCInt(dtrT!sc_numrata).ToString & " AND xx_operaz = 'SALD' OR xx_mantieni = 'S'").Length > 0 Then
          nMaxrata += 1
          dtrT!sc_numrata = nMaxrata
        End If
      Next

      'controllo finale: se le nuove scadenze ricalcolate non tornano con le vechie: abortisco
      For Each dtrT As DataRow In dttSc.Rows
        dTotScadIni -= NTSCDec(dtrT!sc_importo)
      Next
      If (dTotScadIni - dTotScadOkOld) <> 0 Then
        LogWrite(oApp.Tr(Me, 130379116521206626, "ATTENZIONE: rielaborazione documento |" & strEstremidoc & _
                          "|: In fase di ricalcolo scadenze il nuovo totale è diverso dal totale partita in CG. Documento non contabilizzato"), True)
        Return False
      End If

      'dttSC.AcceptChanges()NO, altrimenti non salva scaden!!!

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


  Public Overridable Function GeneraEffetti(ByRef dtrTm As DataRow, ByRef dttPN As DataTable, _
                                            ByRef dttSC As DataTable, ByVal strEstremidoc As String) As Boolean
    'genero gli effetti per le scadenze di tipo RB solo per tipork A, D, B
    Dim strErr As String = ""
    Dim strWhereScad As String = ""
    Try
      If dttPN.Rows.Count = 0 Then Return True
      If dttSC.Select("sc_darave = 'D' AND sc_tippaga = 2 AND sc_flsaldato = 'N' AND sc_codincdiff=0").Length = 0 Then Return True

      '---------------------------------
      'istanzio
      If oCleGnef Is Nothing Then
        Dim strErr1 As String = ""
        Dim oTmp As Object = Nothing
        If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNCGGNEF", "BECGGNEF", oTmp, strErr1, False, "", "") = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128244110439910996, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr1 & "|: non verrà eseguita la generazione degli effetti")))
          oCleGnef = Nothing
          Return False
        End If

        oCleGnef = CType(oTmp, CLECGGNEF)
        AddHandler oCleGnef.RemoteEvent, AddressOf GestisciEventiEntityGnef
        If oCleGnef.Init(oApp, oScript, oCleComm, "", False, "", "") = False Then
          oCleGnef = Nothing
          Return False
        End If
        oCleGnef.bModPR = CBool(ModuliDittaDitt(strDittaCorrente) And bsModPR)
        If Not oCleGnef.LeggiDatiDitta(strDittaCorrente, nCausaleEff, lContoEff) Then
          oCleGnef = Nothing
          Return False
        End If

        If nCausaleEff = 0 Or lContoEff = 0 Then
          'non posso fare nulla
          LogWrite(oApp.Tr(Me, 130397994479442180, "Generazione effetti: l'operazione non è stata eseguita perchè in personalizzazione CG non sono stati indicati il conto/controp. effetti e/o la causale di emissione effetti"), True)
          oCleGnef = Nothing
          Return False
        End If

        If oCleGnef.dttAnaz.Rows(0)!tb_azprofes.ToString = "S" And oCleGnef.bParc = False Then
          LogWrite(oApp.Tr(Me, 130398749441706586, "Generazione effetti: Impossibile operare con ditta professionista con giro effetti con chiusura cliente ! Modificare apposita scelta in dati aggiuntivi contabili della ditta!"), True)
          oCleGnef = Nothing
          Return False
        End If

        dImpMinEff = NTSCDec(oCldPnfa.GetSettingBus("BSVEPNFA", "OPZIONI", ".", "GnefImMin", "0", " ", "0"))

        If Not oCleGnef.edCausale_Validated(nCausaleEff, strDescaucEff, strErr) Then
          LogWrite(oApp.Tr(Me, 130397995111635636, "Generazione effetti: ") & strErr, True)
          Return False
        End If
        If Not oCleGnef.edConto_Validated(lContoEff, "", strErr) Then
          LogWrite(oApp.Tr(Me, 130398749364642008, "Generazione effetti: ") & strErr, True)
          Return False
        End If

        oCleGnef.bBevepnfa = True 'a gnef non faccio fare una serie di controlli già fatti qui
        oCleGnef.dsPrinModel = New DataSet
        oCleGnef.dsPrinModel.Tables.Add(dttPN.Clone)
      End If    'If oCleGnef Is Nothing Then

      '---------------------------------
      'lancio l'elaborazione
      If dImpMinEff < 0 Then dImpMinEff = 0
      oCleGnef.nEscompCorr = nEscomp  'preso da BNVEPNFA
      With dttSC.Rows(0)
        strWhereScad = "sc_darave = 'D'§sc_tippaga = 2§sc_flsaldato = 'N'§sc_codincdiff=0" & _
                       "§sc_conto = " & !sc_conto.ToString & _
                       "§sc_annpar = " & !sc_annpar.ToString & _
                       "§sc_alfpar = " & CStrSQL(!sc_alfpar.ToString) & _
                       "§sc_numpar = " & !sc_numpar.ToString
      End With
      If Not oCleGnef.LogWrite("START", False) Then
        oCleGnef = Nothing
        Return False
      End If
      oCleGnef.Elabora(nCausaleEff, lContoEff, NTSCDate(dttPN.Rows(0)!pn_datreg).ToShortDateString, False, _
                       dImpMinEff, strDescaucEff, 0, 9999, strWhereScad, 0, "T", False)
      oCleGnef.LogWrite("STOP", False)
      If oCleGnef.nCountLog > 0 Then
        Dim r1 As New System.IO.StreamReader(oApp.Dir & "\" & "BSCGGNEF.log")
        strErr = r1.ReadToEnd
        r1.Close()
        r1 = Nothing
        LogWrite(oApp.Tr(Me, 130397997625633929, "Generazione effetti per documento num. |" & dtrTm!tm_numdoc.ToString & "|: messaggi dal log elaborativo:") & vbCrLf & strErr, True)
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
  Public Overridable Sub GestisciEventiEntityGnef(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      'giro i messaggi a BNVEPNFA
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


  Public Overridable Function GeneraProvvigioni(ByRef dtrTm As DataRow, ByRef dttPN As DataTable, _
                                                ByRef dttSC As DataTable, ByVal strEstremidoc As String) As Boolean
    Dim dttTmp As New DataTable
    Dim dttProvvig As New DataTable
    Dim strTmp As String = ""
    Dim nAgente1 As Integer = 0
    Dim nAgente2 As Integer = 0
    Dim bOk As Boolean = False
    Dim dImpProv As Decimal = 0
    Dim dProv1 As Decimal = 0
    Dim dProv2 As Decimal = 0
    Dim strErr As String = ""
    Dim strWhere As String = ""

    Try
      '---------------------------------
      'istanzio
      If oCleGnpv Is Nothing Then
        Dim oTmp As Object = Nothing
        If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNPRGNPV", "BEPRGNPV", oTmp, strErr, False, "", "") = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130398749578661699, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|: non verrà eseguito l'aggiornamento dell'archivio provvigioni")))
          oCleGnpv = Nothing
          Return False
        End If

        oCleGnpv = CType(oTmp, CLEPRGNPV)
        AddHandler oCleGnpv.RemoteEvent, AddressOf GestisciEventiEntityGnpv
        If oCleGnpv.Init(oApp, oScript, oCleComm, "", False, "", "") = False Then
          oCleGnpv = Nothing
          Return False
        End If

        oCleGnpv.strUsaContoFattDoc = oCldPnfa.GetSettingBus("BSPRGNPV", "OPZIONI", ".", "UsaContoFattDoc", "0", " ", "0") ' Se impostata, preleva il conto fatturazione del documento al posto di quello indicato in anagrafica
        oCleGnpv.DDTEmessiNoSimulazione = CBool(oCldPnfa.GetSettingBus("BSPRGNPV", "OPZIONI", ".", "DDTEmessiNoSimulazione", "0", " ", "0")) 'se true i ddt emessi sono considerati al pari dei corrispettivi (quindi non vengono considerati nell'elaborazione delle simulaizoni - vedi RSM)
        oCldPnfa.ValCodiceDb(strDittaCorrente, strDittaCorrente, "ANADITAC", "S", "", dttTmp)
        oCleGnpv.bProvvig2 = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_provvig2) = "S", True, False))
        dttTmp.Clear()
      End If    'If oCleGnpv Is Nothing Then

      '---------------------------------
      'controlli preliminari
      If oCleGnpv.DDTEmessiNoSimulazione = False And NTSCStr(dtrTm!tm_tipork) = "B" Then Return True

      '---------------------------------
      'prelevo le provvigioni esistenti 
      If Not oCldPnfa.GetProvvigDoc(strDittaCorrente, NTSCStr(dtrTm!tm_tipork), NTSCInt(dtrTm!tm_anno), _
                                    NTSCStr(dtrTm!tm_serie), NTSCInt(dtrTm!tm_numdoc), dttProvvig) Then Return False
      If dttProvvig.Rows.Count > 0 Then
        'ottengo agente 1 e 2 da provvig
        nAgente1 = NTSCInt(dttProvvig.Rows(0)!pv_codage)
        For Each dtrT As DataRow In dttProvvig.Rows
          If NTSCInt(dtrT!pv_codage) = nAgente1 Then
            dImpProv += NTSCDec(dtrT!pv_impopv)
            dProv1 += NTSCDec(dtrT!pv_provv)
          Else
            dProv2 += NTSCDec(dtrT!pv_provv)
          End If
          If NTSCInt(dtrT!pv_codage) <> nAgente1 Then
            If nAgente2 = 0 Then
              nAgente2 = NTSCInt(dtrT!pv_codage)
            Else
              If NTSCInt(dtrT!pv_codage) <> nAgente2 Then
                'per la provvigione sono stati indicati più di 2 agenti (ovviamente gestiti a mano) non faccio proseguire
                LogWrite(oApp.Tr(Me, 130398074357136013, _
                                 "Aggiornamento archivio provvigioni: per il documento |" & strEstremidoc & "| " & _
                                 "nell'archivio provvigioni sono presenti più di 2 agenti. " & _
                                 "Rielaborazione provvigioni non eseguita."), True)
                Return False
              End If
            End If
          End If
        Next

        bOk = False
        'se sono presenti provvigioni maturate o pagate, se i dati in comune tra la fattura e le provvigioni non sono cambiati, 
        'non faccio nulla, diversamente se ci sono dei pagati avviso che andrà rifatta l'estrazione delle provvigioni 
        'da BNPRGNPV
        If (NTSCInt(dtrTm!tm_codagen) = nAgente1 And NTSCInt(dtrTm!tm_codagen2) = nAgente2) Or _
           (NTSCInt(dtrTm!tm_codagen) = nAgente2 And NTSCInt(dtrTm!tm_codagen2) = nAgente1) And _
           NTSCDate(dttProvvig.Rows(0)!pv_datdoc) = NTSCDate(dtrTm!tm_datdoc) And _
           NTSCInt(dttProvvig.Rows(0)!pv_conto) = NTSCInt(dtrTm!tm_conto) And _
           NTSCDec(dttProvvig.Rows(0)!pv_totfatt) = NTSCDec(dtrTm!tm_totdoc) And _
           NTSCInt(dttProvvig.Rows(0)!pv_codpaga) = NTSCInt(dtrTm!tm_codpaga) And _
           NTSCDec(dttProvvig.Rows(0)!pv_totomag) = NTSCDec(dtrTm!tm_totomag) And _
           Math.Abs(dImpProv - NTSCDec(dtrTm!tm_impprov)) <= 0.02 Then

          'ora controllo se tot. provv è uguale tra testmag e provvig per ogni agente e se le date di scadenza ed importo sono le stesse
          If NTSCInt(dtrTm!tm_codagen) = nAgente1 And nAgente1 <> 0 Then
            If Math.Abs(dProv1 - NTSCDec(dtrTm!tm_totprov)) <= 0.02 Then bOk = True
          ElseIf NTSCInt(dtrTm!tm_codagen2) = nAgente1 And nAgente1 <> 0 Then
            If Math.Abs(dProv1 - NTSCDec(dtrTm!tm_totprov2)) <= 0.02 Then bOk = True
          ElseIf NTSCInt(dtrTm!tm_codagen) = nAgente2 And nAgente2 <> 0 Then
            If Math.Abs(dProv2 - NTSCDec(dtrTm!tm_totprov)) <= 0.02 Then bOk = True
          ElseIf NTSCInt(dtrTm!tm_codagen2) = nAgente2 And nAgente2 <> 0 Then
            If Math.Abs(dProv2 - NTSCDec(dtrTm!tm_totprov2)) <= 0.02 Then bOk = True
          End If
        End If

        'se sono sul vecchio incassato, devo fare attenzione solo ai rk di provvig di tipo RB o TRATTA perchè per il maturato 
        'guarda pv_datscad + gg di attesa esito. visto che tra la priam elaboraz. e la seconda potrei aver cambaito il numero di scadenze
        'lunico modo certo è far rielaborare sempre provvig in caso di incassato vecchio sistema con tippaga 1 0 2
        If bOk Then
          If dttProvvig.Select("pv_origine = 'M' AND pv_tippaga IN (1, 2) AND tb_tippro = 'I'").Length > 0 Then bOk = False
        End If

        'se sono su NUOVO INCASSATO, controllo che le scadenze tra scaden e provvig siano uguali
        If bOk Then
          For Each dtrT As DataRow In dttProvvig.Select("pv_origine = 'T'")
            If dttSC.Select("sc_conto = " & NTSCInt(dtrT!pv_conto) & _
                            " AND sc_annpar = " & NTSCInt(dtrT!pv_annpart) & _
                            " AND sc_alfpar = " & CStrSQL(NTSCStr(dtrT!pv_alfpart)) & _
                            " AND sc_numpar = " & NTSCInt(dtrT!pv_numpart) & _
                            " AND sc_numrata = " & NTSCInt(dtrT!pv_numrat) & _
                            " AND sc_datsca = " & CDataSQL(NTSCDate(dtrT!pv_datscad)) & _
                            " AND sc_importo = " & CDblSQL(NTSCDec(dtrT!pv_impscad))).Length = 0 Then
              'è cambiato l'importo o la data di scadenza: devo ricalcolare
              bOk = False
              Exit For
            End If
          Next
        End If

        If bOk Then
          'le provvigioni non sono cambiate: NON FACCIO NULLA
          Return True
        Else
          If dttProvvig.Select("pv_impvpag > 0").Length > 0 Then
            LogWrite(oApp.Tr(Me, 130398071266990175, _
                             "Aggiornamento archivio provvigioni: per il documento |" & strEstremidoc & "| " & _
                             "alcune provvigioni risultano PAGATE per l'agente/i |" & strTmp & "|. " & _
                             "Rielaborazione provvigioni non eseguita."), True)
            Return False
          End If
          If dttProvvig.Select("pv_impvmat > 0").Length > 0 Then
            strTmp = nAgente1.ToString
            If nAgente2 <> 0 Then strTmp += ", " & nAgente2.ToString
            LogWrite(oApp.Tr(Me, 130398071068725138, _
                     "Aggiornamento archivio provvigioni: per il documento |" & strEstremidoc & "| " & _
                     "alcune provvigioni risultano MATURATE per l'agente/i |" & strTmp & "|. " & _
                     "Al termine dell'elaborazione rilanciare l'elaborazione del maturato."), True)
          End If
        End If
      End If    'If dttProvvig.Rows.Count > 0 Then

      '---------------------------------
      'elaboro/rielaboro
      'se sono qui, sicuramente non ho provvigioni pagate
      'cancello le vecchie righe di provvig (anche se non rielaboro: se provvigioni2 tratto anche documenti con provvig disallineato da scaden
      strWhere = "testmag.tm_tipork = " & CStrSQL(NTSCStr(dtrTm!tm_tipork)) & "§" & _
                 "testmag.tm_anno = " & NTSCStr(dtrTm!tm_anno) & "§" & _
                 "testmag.tm_serie = " & CStrSQL(NTSCStr(dtrTm!tm_serie)) & "§" & _
                 "testmag.tm_numdoc = " & NTSCStr(dtrTm!tm_numdoc)
      'cancello le vecchie provvigioni
      If Not oCleGnpv.AggiornaStato(NTSCDate(IntSetDate("01/01/1900")).ToShortDateString, _
                                    NTSCDate(IntSetDate("31/12/2099")).ToShortDateString, _
                                    0, 9999, _
                                    NTSCStr(dtrTm!tm_tipork) = "A", NTSCStr(dtrTm!tm_tipork) = "D", _
                                    NTSCStr(dtrTm!tm_tipork) = "E", NTSCStr(dtrTm!tm_tipork) = "N", _
                                    NTSCStr(dtrTm!tm_tipork) = "C", NTSCStr(dtrTm!tm_tipork) = "F", _
                                    NTSCStr(dtrTm!tm_tipork) = "S", NTSCStr(dtrTm!tm_tipork) = "P", _
                                    NTSCStr(dtrTm!tm_tipork) = "B", NTSCStr(dtrTm!tm_tipork) = "£", _
                                    True, 0, True, strWhere) Then Return False
      If Not oCleGnpv.LogStart("BNPRGNPV", "Generazione delle Provvigioni") Then Return False
      'inserisco le nuove
      If Not oCleGnpv.Elabora(NTSCDate(IntSetDate("01/01/1900")).ToShortDateString, _
                              NTSCDate(IntSetDate("31/12/2099")).ToShortDateString, _
                              0, 9999, _
                              NTSCStr(dtrTm!tm_tipork) = "A", NTSCStr(dtrTm!tm_tipork) = "D", _
                              NTSCStr(dtrTm!tm_tipork) = "E", NTSCStr(dtrTm!tm_tipork) = "N", _
                              NTSCStr(dtrTm!tm_tipork) = "C", NTSCStr(dtrTm!tm_tipork) = "F", _
                              NTSCStr(dtrTm!tm_tipork) = "S", NTSCStr(dtrTm!tm_tipork) = "P", _
                              NTSCStr(dtrTm!tm_tipork) = "B", NTSCStr(dtrTm!tm_tipork) = "£", _
                              True, True, 0, "", 0, "M", False, strWhere) Then Return False
      oCleGnpv.LogStop()
      If oCleGnpv.LogError Then
        Dim r1 As New System.IO.StreamReader(oCleGnpv.LogFileName)
        strErr = r1.ReadToEnd
        r1.Close()
        r1 = Nothing
        LogWrite(oApp.Tr(Me, 130398670475969989, "Generazione provvigioni per documento num. |" & dtrTm!tm_numdoc.ToString & "|: messaggi dal log elaborativo:") & vbCrLf & strErr, True)
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
      If Not oCleGnpv Is Nothing Then oCleGnpv.LogStop()
      dttTmp.Clear()
      dttProvvig.Clear()
    End Try
  End Function
  Public Overridable Sub GestisciEventiEntityGnpv(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      'giro i messaggi a BNVEPNFA
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

  Public Overridable Function CalcolaIvaIndetr(ByVal strTregiva1 As String, ByVal dIntetr As Decimal) As Decimal
    'determina la percentuale di indetraibilità IVA:
    'se il pro-rata è solo da liquid IVA, ritorna la stessa percentuale avuta in input (ovvero quella presa dal codice IVA
    'se il pro-rata deve essere calcolato in ogni registrazione, aggiunge alla eventuale % in input la % di pro-rata indicata in tabatti
    'il tutto solo se il primo registro IVA è di tipo ACQUISTI
    Try
      If strTregiva1 <> "A" Then
        Return dIntetr
      End If

      If strProrata <> "P" Then
        Return dIntetr
      Else
        Return ArrDbl(dIntetr + (((100 - dIntetr) * dPerProrata) / 100), oCldPnfa.TrovaNdec(0))
      End If

    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

#Region "Contabilizzazione Movimenti di cassa"
  Public Overridable Function EliminaRegistrazioniMovimentiCassa() As Boolean
    Try
      'Scorre le chiusure trovate per prelevare i movimenti compresi tra la chiusura e l'apertura.
      For Each dtrChiusura As DataRow In oCldPnfa.TrovaChiusureDiCassa(strDittaCorrente, strDatdocDa, strDatdocA).Rows
        oCldPnfa.EliminaRegistrazioniMovimentiCassa(strDittaCorrente, oCldPnfa.TrovaMovimentiDiCassa(dtrChiusura, True))
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

  Public Overridable Function ContabilizzaMovimentiDiCassa() As Boolean
    Try
      If Not CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupGPE) Then
        LogWrite(oApp.Tr(Me, 130815101661225901, "Non è possibile contabilizzare i movimenti di cassa in quanto non si dispone del modulo GPV Extended"), True)
        Return False
      End If

      'Scorre le chiusure trovate per prelevare i movimenti compresi tra la chiusura e l'apertura.
      For Each dtrChiusura As DataRow In oCldPnfa.TrovaChiusureDiCassa(strDittaCorrente, strDatdocDa, strDatdocA).Rows
        ThrowRemoteEvent(New NTSEventArgs("AGGIOLABEL", oApp.Tr(Me, 130815071413268337, "Contab. chiusura del |" & NTSCDate(dtrChiusura!mc_datmov).ToShortDateString & _
                                                                                        " " & NTSCDec(dtrChiusura!mc_oramovi).ToString("00.00").Replace(",", ":") & _
                                                                                        "|. Punto cassa |" & NTSCStr(dtrChiusura!mc_codrepc) & "| ...")))
        Dim dttMovCassa As DataTable = oCldPnfa.TrovaMovimentiDiCassa(dtrChiusura, False)
        'Scorre i movimenti di cassa e li registra uno alla volta (o se hanno dei progressivi collegati a gruppi) 
        For Each dtrMovCassa As DataRow In dttMovCassa.Rows
          If dtrMovCassa.RowState = DataRowState.Deleted Then Continue For 'le righe cancellate sono già state elaborate
          Dim dttMov As DataTable = dttMovCassa.Clone 'La struttura dove inserire i movimenti di cassa
          dttMov.Rows.Add(dtrMovCassa.ItemArray)
          'Verifica se ci sono altre righe da aggiungere al datatable
          For Each dtrRow As DataRow In dttMovCassa.Select("mc_progrcoll = " & NTSCInt(dtrMovCassa!mc_progr))
            dttMov.Rows.Add(dtrRow.ItemArray)
            dtrRow.Delete()
          Next
          'Valuta se è un movimento di cassa che genera una nuova scadenza
          dttMov.Columns.Add("xx_newcred", GetType(String))
          If (NTSCStr(dttMov.Rows(0)!mc_causale) = "I" OrElse NTSCStr(dttMov.Rows(0)!mc_causale) = "E") AndAlso _
              NTSCInt(dttMov.Rows(0)!mc_annpar) = 0 Then
            dttMov.Rows(0)!xx_newcred = "S"
          Else
            dttMov.Rows(0)!xx_newcred = "N"
          End If

          RegistraMovimentoCassa(dttMov, NTSCStr(dttMov.Rows(0)!mc_codrepc))
          dtrMovCassa.Delete()
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
  End Function

  Public Overridable Function RegistraMovimentoCassa(ByVal dttMovCassa As DataTable, ByVal strNomePc As String) As Boolean
    Try
      Dim dttRegistrazione As DataTable = PreparaRegistrazioneMovimentoCassa(dttMovCassa, strNomePc)
      If dttRegistrazione Is Nothing Then Return True 'Non c'era nulla da fare!

      Return oCldPnfa.SalvaRegistrazioneMovimentoCassa(dttMovCassa, dttRegistrazione)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function PreparaRegistrazioneMovimentoCassa(ByVal dttMovCassa As DataTable, ByVal strNomePc As String) As DataTable
    Dim dttReto, dttAnaz As New DataTable
    Dim dttPrinot As DataTable = Nothing
    Dim lContoDare, lContoAvere, lContoDareBase, lContoAvereBase, lCausale, lNumreg, nEscomp As Integer
    Dim lAnnPar1, lNumPar1, lNumRata1, lAnnPar2, lNumPar2, lNumRata2 As Integer
    Dim strAlfPar1 As String = " "
    Dim strAlfPar2 As String = " "
    Dim strDescauc As String = ""
    Dim strDescr1 As String = " "
    Dim strDescr2 As String = " "
    Dim dImporto As Decimal
    Try
      oCldPnfa.ValCodiceDb(NTSCStr(dttMovCassa.Rows(0)!mc_codreto), strDittaCorrente, "TABRETO", "N", , dttReto)

      If NTSCStr(dttReto.Rows(0)!tb_contab) = "N" Then Return Nothing

      oCldPnfa.GetTableStructure("PRINOT", False, dttPrinot)
      oCldPnfa.SetTableDefaultValueFromDB("PRINOT", dttPrinot.DataSet)
      dttPrinot.AcceptChanges()

      With dttReto.Rows(0)
        If NTSCStr(dttReto.Rows(0)!tb_tipo) = "T" Then
          lContoDare = DeterminaConto(NTSCInt(dttMovCassa.Rows(1)!mc_conto), NTSCInt(dttMovCassa.Rows(1)!mc_codpaga), NTSCStr(!tb_tpcontoa), _
                                      NTSCInt(!tb_contoa), strNomePc)
        Else
          Dim dtrTmp() As DataRow = dttMovCassa.Select("mc_codpaga <> 0") 'In presenza di abbuoni possono esserci delle righe con codice pagamento 0
          If dtrTmp.Length > 0 Then
            lContoDare = DeterminaConto(NTSCInt(dttMovCassa.Rows(0)!mc_conto), NTSCInt(dtrTmp(0)!mc_codpaga), NTSCStr(!tb_tpcontod), _
                                        NTSCInt(!tb_contod), strNomePc)
          Else
            lContoDare = DeterminaConto(NTSCInt(dttMovCassa.Rows(0)!mc_conto), 0, NTSCStr(!tb_tpcontod), NTSCInt(!tb_contod), strNomePc)
          End If
        End If
        lContoAvere = DeterminaConto(NTSCInt(dttMovCassa.Rows(0)!mc_conto), NTSCInt(dttMovCassa.Rows(0)!mc_codpaga), NTSCStr(!tb_tpcontoa), _
                                     NTSCInt(!tb_contoa), strNomePc)
        lCausale = NTSCInt(!tb_codcauc)
      End With

      lContoDareBase = lContoDare
      lContoAvereBase = lContoAvere

      oCldPnfa.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", , dttAnaz)
      nEscomp = NTSCInt(dttAnaz.Rows(0)!tb_escomp)

      oCldPnfa.ValCodiceDb(NTSCStr(lCausale), strDittaCorrente, "TABCAUC", "N", strDescauc)

      strDescr1 = NTSCStr(dttMovCassa.Rows(0)!mc_note) & " "
      strDescr2 = strDescr1

      Select Case NTSCStr(dttReto.Rows(0)!tb_tipo)
        Case "D", "I" 'Incasso crediti clienti.
          lNumRata1 = 0
          lNumRata2 = 0
          For i As Integer = 0 To dttMovCassa.Rows.Count - 1
            With dttMovCassa.Rows(i) 'Per ogni riga genera una registrazione in prima nota
              lNumreg = oCldPnfa.LegAggRegcDitt(strDittaCorrente, NTSCStr(!mc_datmov))
              dImporto = NTSCDec(!mc_importo)
              'If NTSCStr(dttReto.Rows(0)!tb_tipo) = "D" Then dImporto *= -1

              If NTSCStr(!xx_newcred) = "N" Then
                If NTSCStr(dttReto.Rows(0)!tb_tipo) = "D" Then
                  lAnnPar1 = NTSCInt(!mc_annpar)
                  strAlfPar1 = NTSCStr(!mc_alfpar)
                  lNumPar1 = NTSCInt(!mc_numpar)
                  lNumRata1 = NTSCInt(!mc_numrata)
                  If NTSCStr(!mc_abbuono) = "S" Then
                    lContoAvere = TrovaContoAbbuono(lContoDare, "D")
                  Else
                    lContoAvere = lContoAvereBase
                  End If
                Else
                  lAnnPar2 = NTSCInt(!mc_annpar)
                  strAlfPar2 = NTSCStr(!mc_alfpar)
                  lNumPar2 = NTSCInt(!mc_numpar)
                  lNumRata2 = NTSCInt(!mc_numrata)
                  If NTSCStr(!mc_abbuono) = "S" Then
                    lContoDare = TrovaContoAbbuono(lContoAvere, "I")
                  Else
                    lContoDare = lContoDareBase
                  End If
                End If
              Else
                If NTSCStr(dttReto.Rows(0)!tb_tipo) = "D" Then
                  lAnnPar1 = Now.Year
                  strAlfPar1 = oCldPnfa.SerieNegozio(strDittaCorrente, strNomePc)
                  lNumPar1 = oCldPnfa.TrovaNuovoNumeroPartita(strDittaCorrente, lAnnPar2, strAlfPar2)
                  lNumRata1 += 1
                Else
                  lAnnPar2 = Now.Year
                  strAlfPar2 = oCldPnfa.SerieNegozio(strDittaCorrente, strNomePc)
                  lNumPar2 = oCldPnfa.TrovaNuovoNumeroPartita(strDittaCorrente, lAnnPar2, strAlfPar2)
                  lNumRata2 += 1
                End If
              End If
              'Prepara la tabella della prima nota
              Wripn(dttPrinot, nEscomp, strDescauc, NTSCStr(dttMovCassa.Rows(0)!mc_datmov), lNumreg, 1, lContoDare, strDescr1, lCausale, dImporto, "D", lContoAvere, _
                    lAnnPar1, strAlfPar1, lNumPar1, lNumRata1)
              Wripn(dttPrinot, nEscomp, strDescauc, NTSCStr(dttMovCassa.Rows(0)!mc_datmov), lNumreg, 2, lContoAvere, strDescr1, lCausale, dImporto, "A", lContoDare, _
                    lAnnPar2, strAlfPar2, lNumPar2, lNumRata2)

              dttMovCassa.Rows(i)!mc_datreg = !mc_datmov
              dttMovCassa.Rows(i)!mc_numreg = lNumreg
            End With
          Next
        Case Else
          With dttMovCassa.Rows(0) 'Registrazione in prima nota unica, indifferentemente dalle righe del datatable
            Select Case NTSCStr(dttReto.Rows(0)!tb_tipo)
              Case "C", "A"
                strDescr1 = oApp.Tr(Me, 130312414608885291, "Tess. ") & NTSCStr(!mc_codtes) & " " & strDescr1
                strDescr2 = strDescr1
              Case "F"
                strDescr1 = oApp.Tr(Me, 130312414846545166, "Tess. ") & NTSCStr(!mc_codtes) & " " & strDescr1
                strDescr2 = oApp.Tr(Me, 130312414863922311, "Tess. ") & NTSCStr(dttMovCassa.Rows(1)!mc_codtes) & " " & strDescr1
            End Select

            lNumreg = oCldPnfa.LegAggRegcDitt(strDittaCorrente, NTSCStr(!mc_datmov))
            dImporto = Math.Abs(NTSCDec(!mc_importo))
          End With
          'Prepara la tabella della prima nota
          Wripn(dttPrinot, nEscomp, strDescauc, NTSCStr(dttMovCassa.Rows(0)!mc_datmov), lNumreg, 1, lContoDare, strDescr1, lCausale, dImporto, "D", lContoAvere, _
                lAnnPar1, strAlfPar1, lNumPar1, lNumRata1)
          Wripn(dttPrinot, nEscomp, strDescauc, NTSCStr(dttMovCassa.Rows(0)!mc_datmov), lNumreg, 2, lContoAvere, strDescr2, lCausale, dImporto, "A", lContoDare, _
                lAnnPar2, strAlfPar2, lNumPar2, lNumRata2)

          For i As Integer = 0 To dttMovCassa.Rows.Count - 1
            dttMovCassa.Rows(i)!mc_datreg = dttMovCassa.Rows(0)!mc_datmov
            dttMovCassa.Rows(i)!mc_numreg = lNumreg
          Next
      End Select

      Return dttPrinot
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
  Public Overridable Function Wripn(ByRef dttPrin As DataTable, ByVal nEscomp As Integer, ByVal strDescauc As String, _
                                    ByVal strDatreg As String, ByVal lNumreg As Integer, ByVal lRiga As Integer, _
                                    ByVal lConto As Integer, ByVal strDescr As String, ByVal nCausale As Integer, _
                                    ByVal dImporto As Decimal, ByVal strDarave As String, ByVal lControp As Integer, _
                                    ByVal lAnnPar As Integer, ByVal strAlfPar As String, ByVal lNumPar As Integer, _
                                    ByVal lNumRata As Integer) As Boolean
    Dim dtrMovi As DataRow = Nothing
    Dim dttAnagra As New DataTable
    Dim dttTabciva As New DataTable

    Try
      '----------------------
      'ultimi controlli
      If dImporto = 0 Then Return True 'esco se non devo scrivere la riga (la scrivo solo se è la riga IVA, altrimenti da errore in apertura da bncgprin)

      oCldPnfa.ValCodiceDb(lConto.ToString, strDittaCorrente, "ANAGRA", "N", "", dttAnagra)
      If dttAnagra.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128564791725468750, "Errore : Conto  " & lConto.ToString & " inesistente." & vbCrLf & "Non è stato possibile determinare il conto da utilizzare nella registrazione." & vbCrLf & "Registrazione Impossibile.")))
        Return False
      End If

      '-----------------------
      'aggiungo prinot
      dttPrin.Rows.Add(dttPrin.NewRow)
      With dttPrin.Rows(dttPrin.Rows.Count - 1)
        !codditt = strDittaCorrente
        !pn_datreg = NTSCDate(strDatreg)
        !pn_numreg = lNumreg
        !pn_riga = lRiga
        !pn_conto = lConto
        !pn_causale = nCausale
        !pn_partite = dttAnagra.Rows(0)!an_partite
        !pn_darave = strDarave
        If strDarave = "D" Then
          !pn_importo = ArrDbl(dImporto, oCldPnfa.TrovaNdec(0))
          !pn_dare = ArrDbl(dImporto, oCldPnfa.TrovaNdec(0))
          !pn_avere = 0
          !pn_imponib = 0
        Else
          !pn_importo = ArrDbl(dImporto * -1, oCldPnfa.TrovaNdec(0))
          !pn_dare = 0
          !pn_avere = ArrDbl(dImporto, oCldPnfa.TrovaNdec(0))
          !pn_imponib = 0
        End If
        !pn_datdoc = NTSCDate(strDatreg)
        !pn_annpar = lAnnPar
        !pn_alfpar = strAlfPar
        !pn_numpar = lNumPar
        If strDescr.Length > 255 Then
          !pn_descr = strDescr.Substring(0, 255)
        Else
          !pn_descr = strDescr
        End If
        !pn_scadenz = dttAnagra.Rows(0)!an_scaden
        !pn_escomp = nEscomp
        !pn_controp = lControp
        !pn_fllg = " "
        !pn_flri = " "
        !pn_flst = " "
        !pn_contocf = 0
        !pn_tregiva = " "
        !pn_nregiva = 0
        !pn_codiva = 0
        !pn_aliqiva = 0
        !pn_indetr = 0
        !pn_tipacq = " "
        !pn_numpro = 0
        !pn_imponibval = 0
        !pn_codvalu = 0
        !pn_impval = 0
        !pn_dareval = 0
        !pn_avereval = 0
        !pn_codccos = 0
        !pn_ultdesc = " "
        !pn_cambio = 0
        !pn_integr = "N"
        !pn_dtcomiva = NTSCDate(strDatreg)
        !pn_dtvaluta = NTSCDate(strDatreg)
        !pn_datini = NTSCDate(strDatreg)
        !pn_datfin = NTSCDate(strDatreg)
        !pn_ultagg = NTSCDate(DateTime.Now.ToShortDateString)
        !pn_opnome = oApp.User.Nome
        !pn_spunta = "N"
        !pn_chiusecb = "N"
        !pn_dtcomplaf = NTSCDate(strDatreg)
        !pn_descauc = IIf(nCausale = 9999, oApp.Tr(Me, 128830496452346000, "Giroconto Iva Inded"), strDescauc) ' letto fuori da qui
        !pn_alfpro = " "
        !pn_rigaiva = 0
      End With    'With dttPrin.Rows(dttPrin.Rows.Count - 1)

      Return True
    Catch ex As Exception
      dttPrin.RejectChanges()
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function DeterminaConto(ByVal lConto As Integer, ByVal lCodPaga As Integer, ByVal strTipo As String, _
                                             ByVal lContoFisso As Integer, ByVal strNomePC As String) As Integer
    Try
      Select Case strTipo
        Case "C" : Return lConto
        Case "F" : Return lContoFisso
        Case "P" ' Risale al conto partendo dalla forma di pagamento selezionata
          Dim dttPaga As New DataTable
          Dim lControp As Integer = oCldPnfa.GetContropFromRepcpaga(strDittaCorrente, strNomePC, lCodPaga)
          If lControp = 0 Then
            oCldPnfa.ValCodiceDb(NTSCStr(lCodPaga), strDittaCorrente, "TABPAGA", "N", , dttPaga)
            If dttPaga.Rows.Count = 0 Then Return 0
            lControp = NTSCInt(dttPaga.Rows(0)!tb_concassp)
          End If

          Return ocldBase.TrovaContoDaCodcontr(strDittaCorrente, lControp)
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
    Return 0
  End Function

  Public Overridable Function TrovaContoAbbuono(ByVal lConto As Integer, ByVal strTipo As String) As Integer
    Dim dttTmp As New DataTable
    Dim strTabella As String
    Dim strCampo As String
    Try
      oCldPnfa.ValCodiceDb(NTSCStr(lConto), strDittaCorrente, "ANAGRA", "N", , dttTmp)
      If NTSCStr(dttTmp.Rows(0)!an_tipo) = "C" Then
        strTabella = "tabpeve"
        If strTipo = "I" Then
          strCampo = "tb_conabpa"
        Else
          strCampo = "tb_conabat"
        End If
      Else
        strTabella = "tabpeac"
        If strTipo = "I" Then
          strCampo = "tb_aconabpa"
        Else
          strCampo = "tb_aconabat"
        End If
      End If

      oCldPnfa.ValCodiceDb("1", strDittaCorrente, strTabella, "N", "", dttTmp)

      Return NTSCInt(dttTmp.Rows(0)(strCampo))
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
#End Region
End Class
