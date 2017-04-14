Imports System.Data
Imports NTSInformatica.CLN__STD


Imports System
Public Class CLE__CLIE
  Inherits CLE__BASN

  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
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

  Public oCldClie As CLD__CLIE

  Public strPrevCelValueTipb As String = ""
  Public strPrevCelValueBanc As String = ""
  Public strPrevCelValueStpg As String = ""

  Public bHasChanges As Boolean
  Public dsShared As DataSet
  Public dttDestdivEx As DataTable                'clone di dsShared.Tables("DESTDIV") per altre destinazioni diverse
  Public dttDestdivDeleted As New DataTable       'clone di dsShared.Tables("DESTDIV") contenente i destdiv iniziali (fileapri successivamente cancellati). serve per cancellare i leads collegati in fase di salvataggio
  Public dttOrganigDeleted As New DataTable       'contiene i record di organig cancelati (solo clienti e solo se c'è CRM o CS)

  Public lCodDestNew As Integer = 0               'usato da BN__DESG
  Public bNew As Boolean = False

  Public bServer As Boolean = True                'USATO dal CRM: se false non posso inserire clienti da lead

  'GetSettingBus
  Public bNonProporreSiglaRic As Boolean = False
  Public strGeneraIdPswClienti As String = "0"
  Public strAnagenDtIniz As String = "0"
  Public strGestAnaext As String = ""
  Public strBloccaavvertifido As String = ""
  Public strBloccainsolu As String = ""
  Public strBloccaRDScadute As String = ""
  Public bRiapriSuSalva As Boolean = False
  Public bScriviActlog As Boolean = False
  Public nCodpagaInAddNew As Integer = 0
  Public nListinoInAddNew As Integer = 1
  Public strPasswbl As String = ""
  Public bGestAlert As Boolean = False
  Public bGesttabcont As Boolean = False
  Public strTestSalvaCfPiva As String = "N"
  Public strPrevCelValueTiprk As String = ""

  Public strMemblocco As String = ""
  Public dMemfido As Decimal = 0

  Public strTipoConto As String = "C"
  Public lDestdomf As Integer = -1              'Valore pre-impostato per Indirizzo Domicilio fiscale per provvedimenti amministrativi
  Public lDestsedel As Integer = 0              'Valore pre-impostato per Indirizzo Residenza\Domicilio fiscale\Sede legale in Italia
  Public lDestresan As Integer = 0              'Valore pre-impostato per Indirizzo Residenza\sede legale estera
  Public lDestcorr As Integer = 0               'Valore pre-impostato per Indirizzo Luogo di esercizio attività all'estero
  Public dttAnaz As New DataTable
  Public nCurRow As Integer = 0                 'Riga in dsShared.Tables("ANAGRA") in visualizzazione in UI (ovvero il cli/forn che si stà trattando)

  'per accessi CRM e CS e anagrafiche generali
  Public bAnagen As Boolean = False
  Public bModuloAS As Boolean = False
  Public bModuloCRM As Boolean = False
  Public bIsCRMUser As Boolean = False
  Public bAmm As Boolean = False
  Public strAccvis As String = ""
  Public strAccmod As String = ""
  Public strRegvis As String = ""
  Public strRegmod As String = ""
  Public lCodorgaOperat As Integer = 0
  Public nCodcageoperat As Integer = 0
  Public bNuovoContoProposto As Boolean = False   'se false il conto non è quello proposto, ma il progressivo è stato modificato manualmente
  Public bNuovoDaAnagen As Boolean = False
  Public strUserCrm As String = ""
  Public strDefaultUserCrm As String = ""
  Public strVoceFinClie As String = ""
  Public strVoceFinForn As String = ""

  Public lLead As Integer = 0                     'lead collegato al cliente/fornitore in visualizzazione
  Public lAgenteFileApri As Integer = 0           'è il cod agente letto in apertura record: servirà in salva in caso di CRM
  Public bRiscriviDestdiv As Boolean = False      'per ogni cliente/fornitore, se entro nelle destinazioni diverse al salvataggio devo riscrivere destdiv: se non entro nelle destinazioni non lo faccio per ottimizzare le prestazioni
  Public bRiscriviClibanc As Boolean = False      'vedi sopra
  Public bRiscriviClistpg As Boolean = False      'vedi sopra
  Public bRiscriviClitipb As Boolean = False      'vedi sopra
  Public bRiscriviOrganig As Boolean = False      'vedi sopra
  Public bRiscriviCodarfo As Boolean = False      'vedi sopra
  Public lContoProgrMoltip As Integer = 10000     'numero che aggiunto al mastro permette di determinare il codice cliente in  'nuovo'
  Public bCampiCAEAttivi As Boolean = False

  Public lIITtdestdiv As Integer = 0              '--- Instid per TTDESTDIV di appoggio per stampa da modale Altri Indirizzi

  Public strOrderBy As String = ""

  Public lCoddestDaGestioneZoom As Integer = 0
  Public dttOrganigOld As DataTable = Nothing

  Public bSelCodiceNoApri As Boolean = False

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__CLIE"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldClie = CType(MyBase.ocldBase, CLD__CLIE)
    oCldClie.Init(oApp)
    Return True
  End Function

  Public Overridable Function LeggiDatiDitta(ByVal strDitta As String, ByVal bGestAnaext As Boolean) As Boolean
    Dim dttTmp As New DataTable
    Try
      oCldClie.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttAnaz)
      strDittaCorrente = strDitta

      oCldClie.ValCodiceDb(dttAnaz.Rows(0)!tb_azcodpcon.ToString, "", "TABPCON", "S", "", dttTmp)
      Select Case dttTmp.Rows(0)!tb_struttura.ToString
        Case "A" : lContoProgrMoltip = 100000
        Case "B" : lContoProgrMoltip = 1000000
        Case "C" : lContoProgrMoltip = 1000000
        Case "D" : lContoProgrMoltip = 100000
        Case "S" : lContoProgrMoltip = 10000
      End Select
      dttTmp.Clear()

      If bGestAnaext Then
        'determino quali tipi di estensioni anagrafiche sono gestiti
        strGestAnaext = oCldClie.GetAnaextGestiti(strDittaCorrente)
      Else
        strGestAnaext = ""
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

  Public Overridable Sub InizializzaModuli()
    Try
      bModuloAS = False
      bModuloCRM = False
      bModuloAS = CBool(ModuliDittaDitt(strDittaCorrente) And CLN__STD.bsModAS)
      bModuloCRM = CBool(ModuliExtDittaDitt(strDittaCorrente) And CLN__STD.bsModExtCRM) OrElse _
                   CBool(ModuliSupDittaDitt(strDittaCorrente) And CLN__STD.bsModSupWCR)
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


  Public Overridable Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("ANAGRA").Columns("an_tipo").DefaultValue = strTipoConto
      ds.Tables("ANAGRA").Columns("an_conto").DefaultValue = 0
      ds.Tables("ANAGRA").Columns("an_descr1").DefaultValue = " "
      ds.Tables("ANAGRA").Columns("an_usaem").DefaultValue = "S"
      ds.Tables("ANAGRA").Columns("an_sesso").DefaultValue = "M"
      ds.Tables("ANAGRA").Columns("an_persfg").DefaultValue = "F"
      ds.Tables("ANAGRA").Columns("an_profes").DefaultValue = "N"
      ds.Tables("ANAGRA").Columns("an_condom").DefaultValue = "N"
      ds.Tables("ANAGRA").Columns("an_tpsogiva").DefaultValue = "N"
      ds.Tables("ANAGRA").Columns("an_soggresi").DefaultValue = "S"
      ds.Tables("ANAGRA").Columns("an_omocodice").DefaultValue = "N"
      ds.Tables("ANAGRA").Columns("an_dtaper").DefaultValue = Now.ToShortDateString
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

  Public Overridable Function ProfessMovim() As Boolean
    Dim bOut As Boolean = False
    Try
      bOut = oCldClie.ContoMovimPrinot(strDittaCorrente, NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto))
      Return bOut

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function Apri(ByVal strDitta As String, ByVal bNuovo As Boolean, _
                                   ByVal lContoCF As Integer, ByVal strApriWhere As String, _
                                   ByRef dsClie As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim strActLog As String = ""
    Dim dttTmp As New DataTable
    Try
      '--------------------------------------
      'predispongo l'ambiente
      If lDestdomf = -1 Then
        oCldClie.ValCodiceDb("1", strDittaCorrente, "TABINSG", "N", "", dttTmp)
        lDestdomf = NTSCInt(dttTmp.Rows(0)!tb_destdomf)
        lDestsedel = NTSCInt(dttTmp.Rows(0)!tb_destsedel)
        lDestresan = NTSCInt(dttTmp.Rows(0)!tb_destresan)
        lDestcorr = NTSCInt(dttTmp.Rows(0)!tb_destcorr)
        dttTmp.Clear()

        '-------------------------------------------------
        'gestione di actlog
        'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
        strActLog = oCldClie.GetSettingBus("BS--CLIE", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
        If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1" Else strActLog = "0"
        bScriviActlog = CBool(strActLog)
      End If

      If bModuloCRM And bIsCRMUser And strTipoConto = "F" Then
        If bAmm = False Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128366732603916000, "L'utente |'" & oApp.User.Nome & "'| NON è abilitato alla visualizzazione dei dati relativi a fornitori.")))
          Return False
        End If
      End If

      strUserCrm = strDefaultUserCrm
      If bModuloCRM Then
        If bIsCRMUser Then
          strUserCrm = oApp.User.Nome
        End If
      End If

      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldClie.GetData(strDittaCorrente, lContoCF, strApriWhere, bModuloCRM, strAccvis, _
                                 lCodorgaOperat, strRegvis, bIsCRMUser, strTipoConto, dsClie, strOrderBy)
      If dReturn = False Then Return False

      If dsClie.Tables("ANAGRA").Rows.Count = 0 And bNuovo = False And lContoCF <> -1 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128272215204106000, _
                      "Non è stata trovata nessuna anagrafica con gli estremi indicati") & _
                      IIf(bModuloCRM, oApp.Tr(Me, 128366730927696000, vbCrLf & "Oppure Cliente non visibile a causa di blocco da modulo CRM"), "").ToString))
        Return False
      End If

      '--------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldClie.SetTableDefaultValueFromDB("ANAGRA", dsClie)

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      SetDefaultValue(dsClie)
      dsShared = dsClie

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("ANAGRA").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("ANAGRA").ColumnChanged, AddressOf AfterColUpdate

      '--------------------------------------
      'confermo tutto
      dsShared.Tables("ANAGRA").AcceptChanges()
      bHasChanges = False
      nCurRow = 0

      '--------------------------------------
      'se è un nuovo record inserisco la riga nuova nel datatable
      If bNuovo Then
        If dsShared.Tables("ANAGRA").Rows.Count > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128272214650930000, "Codice anagrafica già esistente")))
          Return False
        End If

        dsShared.Tables("ANAGRA").Rows.Add(dsShared.Tables("ANAGRA").NewRow)
        bHasChanges = True
        dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto = lContoCF
      End If

      bNew = bNuovo

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function NuovoAnagra(ByVal lConto As Integer, ByVal lMastro As Integer, _
                                          ByVal lAnagen As Integer, ByVal lLead As Integer, _
                                          ByVal strMastro As String, ByRef dtrA As DataRow) As Boolean
    'in creazione nuovo cliente/fornitore inserisce i valori obbligatori
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Try
      bNuovoDaAnagen = False

      dtrA!codditt = strDittaCorrente
      dtrA!an_codpcon = dttAnaz.Rows(0)!tb_azcodpcon.ToString
      dtrA!an_tipo = strTipoConto
      dtrA!an_conto = lConto
      dtrA!an_codmast = lMastro
      dtrA!xx_codmast = strMastro
      dtrA!an_codanag = lAnagen
      dtrA!an_opnome = oApp.User.Nome
      dtrA!an_status = "A"
      dtrA!an_dtaper = NTSCDate(DateTime.Now.ToShortDateString)
      If strTipoConto = "C" Then
        dtrA!an_partite = IIf(dttAnaz.Rows(0)!tb_ventil.ToString = "C" Or dttAnaz.Rows(0)!tb_ventil.ToString = "S", "S", "N").ToString
        dtrA!an_scaden = IIf(dttAnaz.Rows(0)!tb_azgestscad.ToString = "C" Or dttAnaz.Rows(0)!tb_azgestscad.ToString = "S", "S", "N").ToString
        dtrA!an_listino = nListinoInAddNew
        dtrA!an_codpag = nCodpagaInAddNew
        dtrA!an_codvfde = strVoceFinClie
      Else  
        dtrA!an_partite = IIf(dttAnaz.Rows(0)!tb_ventil.ToString = "F" Or dttAnaz.Rows(0)!tb_ventil.ToString = "S", "S", "N").ToString
        dtrA!an_scaden = IIf(dttAnaz.Rows(0)!tb_azgestscad.ToString = "F" Or dttAnaz.Rows(0)!tb_azgestscad.ToString = "S", "S", "N").ToString
        dtrA!an_listino = 0
        dtrA!an_codpag = 0
        dtrA!an_codvfde = strVoceFinForn
      End If
      oCldClie.ValCodiceDb(lMastro.ToString, strDittaCorrente, "TABMAST", "N", "", dttTmp, dttAnaz.Rows(0)!tb_azcodpcon.ToString)
      dtrA!an_kpccee = NTSCStr(dttTmp.Rows(0)!tb_rifceed)
      dtrA!an_kpccee2 = NTSCStr(dttTmp.Rows(0)!tb_rifceea)
      dtrA!an_rifricd = NTSCStr(dttTmp.Rows(0)!tb_rifricd)
      dtrA!an_rifrica = NTSCStr(dttTmp.Rows(0)!tb_rifrica)
      dttTmp.Clear()

      '--------------------------------
      'se nuovo da lead
      If lLead <> 0 And strTipoConto = "C" And lAnagen = 0 Then
        oCldClie.ValCodiceDb(lLead.ToString, strDittaCorrente, "LEADS", "N", "", dttTmp)
        With dttTmp.Rows(0)
          dtrA!an_descr1 = NTSCStr(!le_descr1)
          dtrA!an_descr2 = NTSCStr(!le_descr2)
          dtrA!an_indir = NTSCStr(!le_indir)
          dtrA!an_citta = NTSCStr(!le_citta)
          dtrA!an_codfis = NTSCStr(!le_codfis)
          dtrA!an_cap = NTSCStr(!le_cap)
          dtrA!an_prov = NTSCStr(!le_prov)
          dtrA!an_pariva = NTSCStr(!le_pariva)
          dtrA!an_telef = NTSCStr(!le_telef)
          dtrA!an_faxtlx = NTSCStr(!le_faxtlx)
          dtrA!an_contatt = NTSCStr(!le_contatt)
          dtrA!an_zona = NTSCInt(!le_zona)
          dtrA!an_categ = NTSCInt(!le_categ)
          dtrA!an_agente = NTSCInt(!le_agente)
          dtrA!an_banc1 = NTSCStr(!le_banc1)
          dtrA!an_banc2 = NTSCStr(!le_banc2)
          dtrA!an_abi = NTSCInt(!le_abi)
          dtrA!an_cab = NTSCInt(!le_cab)
          dtrA!an_listino = NTSCInt(IIf(NTSCInt(!le_listino) = 0, nListinoInAddNew, NTSCInt(!le_listino)))
          dtrA!an_clascon = NTSCInt(!le_clascon)
          dtrA!an_note = NTSCStr(!le_note)
          dtrA!an_stato = NTSCStr(!le_stato)
          dtrA!an_agente2 = NTSCInt(!le_agente2)
          dtrA!an_note2 = NTSCStr(!le_note2)
          dtrA!an_email = NTSCStr(!le_email)
          dtrA!an_website = NTSCStr(!le_website)
          dtrA!an_webuid = NTSCStr(!le_webuid)
          dtrA!an_webpwd = NTSCStr(!le_webpwd)
          dtrA!an_usaem = NTSCStr(!le_usaem)
          dtrA!an_codling = NTSCInt(!le_codling)
          dtrA!an_privacy = NTSCStr(!le_privacy)
          dtrA!an_codcana = NTSCInt(!le_codcana)
          dtrA!an_siglaric = NTSCStr(!le_siglaric)
          dtrA!an_cell = NTSCStr(!le_cell)
          dtrA!an_titolo = NTSCStr(!le_titolo)
          dtrA!an_latitud = NTSCStr(!le_latitud)
          dtrA!an_longitud = NTSCStr(!le_longitud)
        End With
        dttTmp.Clear()
      End If    'If lLead <> 0 Then

      '--------------------------------
      'se nuovo da anagen
      If lAnagen <> 0 Then
        bNuovoDaAnagen = True
        oCldClie.ValCodiceDb(lAnagen.ToString, strDittaCorrente, "ANAGEN", "N", "", dttTmp)
        With dttTmp.Rows(0)
          dtrA!an_descr1 = NTSCStr(!ag_descr1)
          dtrA!an_descr2 = NTSCStr(!ag_descr2)
          dtrA!an_codcomu = NTSCStr(!ag_codcomu) 'Spostato prima di cap\città\provincia perchè altrimenti li sovrascriverebbe.
          dtrA!an_indir = NTSCStr(!ag_indir)
          dtrA!an_cap = NTSCStr(!ag_cap)
          dtrA!an_citta = NTSCStr(!ag_citta)
          dtrA!an_prov = NTSCStr(!ag_prov)
          dtrA!an_stato = NTSCStr(!ag_stato)
          dtrA!an_persfg = NTSCStr(!ag_persfg)
          dtrA!an_codfis = NTSCStr(!ag_codfis)
          dtrA!an_pariva = NTSCStr(!ag_pariva)
          dtrA!an_telef = NTSCStr(!ag_telef)
          dtrA!an_faxtlx = NTSCStr(!ag_faxtlx)
          dtrA!an_valuta = NTSCInt(!ag_valuta)
          dtrA!an_codling = NTSCInt(!ag_codling)
          dtrA!an_destin = NTSCInt(!ag_destin)
          dtrA!an_destpag = NTSCInt(!ag_destpag)
          dtrA!an_note = NTSCStr(!ag_note)
          dtrA!an_note2 = NTSCStr(!ag_note2)
          dtrA!an_email = NTSCStr(!ag_email)
          dtrA!an_website = NTSCStr(!ag_website)
          dtrA!an_usaem = NTSCStr(!ag_usaem)
          dtrA!an_webuid = NTSCStr(!ag_webuid)
          dtrA!an_webpwd = NTSCStr(!ag_webpwd)
          dtrA!an_datnasc = NTSCDate(!ag_datnasc)
          dtrA!an_citnasc = NTSCStr(!ag_citnasc)
          dtrA!an_pronasc = NTSCStr(!ag_pronasc)
          dtrA!an_stanasc = NTSCStr(!ag_stanasc)
          dtrA!an_codfisest = NTSCStr(!ag_codfisest)
          dtrA!an_sesso = NTSCStr(!ag_sesso)
          dtrA!an_cell = NTSCStr(!ag_cell)
          dtrA!an_titolo = NTSCStr(!ag_titolo)
          dtrA!an_profes = NTSCStr(!ag_profes)
          dtrA!an_condom = NTSCStr(!ag_condom)
          dtrA!an_tpsogiva = NTSCStr(!ag_tpsogiva)
          If Not bNonProporreSiglaRic Then
            dtrA!an_siglaric = NTSCStr(!ag_siglaric)
          Else
            dtrA!an_siglaric = ""
          End If
          dtrA!an_cognome = NTSCStr(!ag_cognome)
          dtrA!an_nome = NTSCStr(!ag_nome)
          dtrA!an_codcomn = NTSCStr(!ag_codcomn)
          dtrA!an_nazion1 = NTSCStr(!ag_nazion1)
          dtrA!an_nazion2 = NTSCStr(!ag_nazion2)
          dtrA!an_statofed = NTSCStr(!ag_statofed)
          dtrA!an_soggresi = NTSCStr(!ag_soggresi)
          dtrA!an_omocodice = NTSCStr(!ag_omocodice)
          dtrA!an_estcodiso = NTSCStr(!ag_estcodiso)
          dtrA!an_estpariva = NTSCStr(!ag_estpariva)
          dtrA!an_codrtac = NTSCInt(!ag_codrtac)
          dtrA!an_destcorr = NTSCInt(!ag_destcorr)
          dtrA!an_destdomf = NTSCInt(!ag_destdomf)
          dtrA!an_destresan = NTSCInt(!ag_destresan)
          dtrA!an_destsedel = NTSCInt(!ag_destsedel)
          dtrA!an_latitud = NTSCStr(!ag_latitud)
          dtrA!an_longitud = NTSCStr(!ag_longitud)
        End With
        dttTmp.Clear()
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


  Public Overridable Function CaricaDestdiv(ByRef dtrIn As DataRow) As Boolean
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Try
      '---------------------------------------
      'ottengo destdiv
      If Not oCldClie.GetDataDestdiv(strDittaCorrente, NTSCInt(dtrIn!an_conto), dsShared, bModuloCRM, _
        bIsCRMUser, strTipoConto) Then Return False
      oCldClie.SetTableDefaultValueFromDB("DESTDIV", dsShared)

      bRiscriviDestdiv = False
      'in nuovo salvo sempre le eventuali destdiv
      If bNew Then
        bRiscriviDestdiv = True

        '---------------------------------
        'se nuovo da ANGAGEN devo tirare su anche le destinazioni diverse di destgen
        If NTSCInt(dtrIn!an_codanag) <> 0 Then
          If Not oCldClie.GetDestgen(NTSCInt(dtrIn!an_codanag), dttTmp) Then Return False
          For i = 0 To dttTmp.Rows.Count - 1
            dsShared.Tables("DESTDIV").Rows.Add()
            With dsShared.Tables("DESTDIV").Rows(dsShared.Tables("DESTDIV").Rows.Count - 1)
              !codditt = strDittaCorrente
              !dd_conto = dtrIn!an_conto
              !dd_coddest = NTSCInt(dttTmp.Rows(i)!dd_coddest)
              !dd_nomdest = NTSCStr(dttTmp.Rows(i)!dd_nomdest)
              !dd_nomdest2 = NTSCStr(dttTmp.Rows(i)!dd_nomdest2)
              !dd_inddest = NTSCStr(dttTmp.Rows(i)!dd_inddest)
              !dd_capdest = NTSCStr(dttTmp.Rows(i)!dd_capdest)
              !dd_locdest = NTSCStr(dttTmp.Rows(i)!dd_locdest)
              !dd_prodest = NTSCStr(dttTmp.Rows(i)!dd_prodest)
              !dd_turno = NTSCStr(dttTmp.Rows(i)!dd_turno)
              !dd_telef = NTSCStr(dttTmp.Rows(i)!dd_telef)
              !dd_codfis = NTSCStr(dttTmp.Rows(i)!dd_codfis)
              !dd_pariva = NTSCStr(dttTmp.Rows(i)!dd_pariva)
              !dd_faxtlx = NTSCStr(dttTmp.Rows(i)!dd_faxtlx)
              !dd_email = NTSCStr(dttTmp.Rows(i)!dd_email)
              !dd_usaem = NTSCStr(dttTmp.Rows(i)!dd_usaem)
              !dd_stato = NTSCStr(dttTmp.Rows(i)!dd_stato)
              !dd_codcomu = NTSCStr(dttTmp.Rows(i)!dd_codcomu)
              !dd_codfisest = NTSCStr(dttTmp.Rows(i)!dd_codfisest)
              !dd_statofed = NTSCStr(dttTmp.Rows(i)!dd_statofed)
              !xx_codcomu = NTSCStr(dttTmp.Rows(i)!xx_codcomu)
              !xx_stato = NTSCStr(dttTmp.Rows(i)!xx_stato)
              !dd_latitud = NTSCStr(dttTmp.Rows(i)!dd_latitud)
              !dd_longitud = NTSCStr(dttTmp.Rows(i)!dd_longitud)
            End With
          Next
          dsShared.Tables("DESTDIV").AcceptChanges()
          dttTmp.Clear()
        End If    'If lAnagen <> 0 Then
      End If    'If NTSCInt(dtrIn!an_codanag) <> 0 Then

      dttDestdivDeleted.Clear()
      dttDestdivDeleted = dsShared.Tables("DESTDIV").Clone()
      dttDestdivDeleted.AcceptChanges()

      For Each dtrRow As DataRow In dsShared.Tables("DESTDIV").Rows
        If NTSCInt(dtrRow!dd_listino) = -9999 Then dtrRow!xx_listino = oApp.Tr(Me, 130408346225153134, "Usa il listino dell'anagrafica clienti")
      Next
      dsShared.Tables("DESTDIV").AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function CaricaAnaext(ByRef dtrIn As DataRow, ByVal nTipo As Integer) As Boolean
    'nTipo: 0 = tutti
    '       1 = anaext di anagra
    '       2 = anaext di destdiv
    Dim dtrT1 As DataRow = Nothing
    Dim dsTmp As New DataSet
    Try
      '---------------------------------------
      'carico ANAEXT del conto cliente/fornitore e di destdiv
      If nTipo = 0 Or nTipo = 1 Then
        If strGestAnaext.IndexOf(strTipoConto) > -1 Then
          'anaext di anagra
          If dsShared.Tables.Contains("ANAEXT") Then dsShared.Tables.Remove("ANAEXT")
          If Not oCldClie.GetDataAnaext(strDittaCorrente, NTSCInt(dtrIn!an_conto), 0, 0, strTipoConto, dsShared) Then Return False

          If dsShared.Tables("ANAEXT").Rows.Count = 0 And lLead <> 0 And strGestAnaext.IndexOf("L") > -1 Then
            'non ho trovato anaext: può essere un 'nuovo da lead: cerco di caricare anaext da anaext del lead
            oCldClie.GetDataAnaext(strDittaCorrente, 0, 0, lLead, "L", dsTmp)
            If dsTmp.Tables("ANAEXT").Rows.Count > 0 Then
              dsTmp.Tables("ANAEXT").Rows(0)!ax_codlead = 0
              dsTmp.Tables("ANAEXT").Rows(0)!ax_tipork = strTipoConto
              dsTmp.Tables("ANAEXT").Rows(0)!ax_conto = NTSCInt(dtrIn!an_conto)
              dsTmp.Tables("ANAEXT").Rows(0)!ax_coddest = 0
              dsShared.Tables("ANAEXT").ImportRow(dsTmp.Tables("ANAEXT").Rows(0))
              dsTmp.Clear()
              dsShared.Tables("ANAEXT").AcceptChanges()
            End If
          End If

          'non ho ancora trovato anaext collegato ad anagra: lo creo vuoto (è il caso, ad esempio, di nuovo da anagen)
          If dsShared.Tables("ANAEXT").Rows.Count = 0 Then
            dsShared.Tables("ANAEXT").Rows.Add(dsShared.Tables("ANAEXT").NewRow)
            dsShared.Tables("ANAEXT").Rows(0)!codditt = strDittaCorrente
            dsShared.Tables("ANAEXT").Rows(0)!ax_conto = NTSCInt(dtrIn!an_conto)
            dsShared.Tables("ANAEXT").Rows(0)!ax_coddest = 0
            dsShared.Tables("ANAEXT").Rows(0)!ax_codlead = 0
            dsShared.Tables("ANAEXT").Rows(0)!ax_tipork = strTipoConto
            dsShared.Tables("ANAEXT").Rows(0)!ax_codart = " "
            dsShared.Tables("ANAEXT").Rows(0)!ax_matric = " "
            dsShared.Tables("ANAEXT").AcceptChanges()
          End If
        End If    'If strGestAnaext.IndexOf(strTipoConto) > -1 Then
      End If    'If nTipo = 0 Or nTipo = 1 Then

      '--------------------------------
      'anaext di destdiv
      If nTipo = 0 Or nTipo = 2 Then
        If strGestAnaext.IndexOf(IIf(strTipoConto = "C", "D", "E").ToString) > -1 Then
          If dsShared.Tables.Contains("ANAEXTDD") Then dsShared.Tables.Remove("ANAEXTDD")
          If Not oCldClie.GetDataAnaext(strDittaCorrente, NTSCInt(dtrIn!an_conto), -1, 0, IIf(strTipoConto = "C", "D", "E").ToString, dsShared) Then Return False

          'per ogni destdiv verifico se c'è un anaext: se non c'è lo creo (è il caso, ad esempio, di nuovo da anagen)
          If dsShared.Tables("ANAEXTDD").Rows.Count <> dsShared.Tables("DESTDIV").Rows.Count Then
            For Each dtrT As DataRow In dsShared.Tables("DESTDIV").Rows
              If dsShared.Tables("ANAEXTDD").Select("ax_coddest = " & dtrT!dd_coddest.ToString).Length = 0 Then
                dsShared.Tables("ANAEXTDD").Rows.Add(dsShared.Tables("ANAEXTDD").NewRow)
                With dsShared.Tables("ANAEXTDD").Rows(dsShared.Tables("ANAEXTDD").Rows.Count - 1)
                  !codditt = strDittaCorrente
                  !ax_conto = NTSCInt(dtrIn!an_conto)
                  !ax_coddest = NTSCInt(dtrT!dd_coddest.ToString)
                  !ax_codlead = 0
                  !ax_tipork = IIf(strTipoConto = "C", "D", "E").ToString
                  !ax_codart = " "
                  !ax_matric = " "
                End With
              End If
            Next
            dsShared.Tables("ANAEXTDD").AcceptChanges()
          End If
        End If    'If strGestAnaext.IndexOf(IIf(strTipoConto = "C", "D", "E").ToString) > -1 Then
      End If    'If nTipo = 0 Or nTipo = 2 Then
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function CaricaTabelleCollegate(ByRef dtrIn As DataRow) As Boolean
    Try
      '---------------------------------------
      'ottengo clitipb
      If Not oCldClie.GetDataTabelleCollegate(strDittaCorrente, NTSCInt(dtrIn!an_conto), bGesttabcont, dsShared) Then Return False
      If bNew And lLead <> 0 Then
        'solo per 'nuovo da lead' organig deve essere quello del lead
        If Not oCldClie.GetOrganigFromLead(strDittaCorrente, lLead, bGesttabcont, dsShared) Then Return False
        For Each dtrT As DataRow In dsShared.Tables("ORGANIG").Rows
          dtrT!og_conto = NTSCInt(dtrIn!an_conto)
          dtrT!xx_conto = NTSCStr(dtrIn!an_descr1)
          dtrT!og_tipork = "I"
        Next
        dsShared.Tables("ORGANIG").AcceptChanges()
      End If

      oCldClie.SetTableDefaultValueFromDB("CLITIPB", dsShared)
      oCldClie.SetTableDefaultValueFromDB("CLIBANC", dsShared)
      oCldClie.SetTableDefaultValueFromDB("CLISTPG", dsShared)
      oCldClie.SetTableDefaultValueFromDB("ORGANIG", dsShared)
      oCldClie.SetTableDefaultValueFromDB("ANACONA", dsShared)

      dsShared.Tables("CLITIPB").AcceptChanges()
      dsShared.Tables("CLIBANC").AcceptChanges()
      dsShared.Tables("CLISTPG").AcceptChanges()
      dsShared.Tables("ORGANIG").AcceptChanges()
      dsShared.Tables("ANACONA").AcceptChanges()

      dttOrganigOld = dsShared.Tables("ORGANIG").Copy
      dttOrganigOld.AcceptChanges()
      '----------------------------------------
      'clone di organig per le righe cancellate (solo clienti e solo se c'è CRM o CS
      dttOrganigDeleted.Clear()
      dttOrganigDeleted = dsShared.Tables("ORGANIG").Clone()
      dttOrganigDeleted.AcceptChanges()

      bRiscriviClibanc = False
      bRiscriviClistpg = False
      bRiscriviClitipb = False
      bRiscriviOrganig = False
      bRiscriviCodarfo = False

      'in nuovo salvo sempre tutto
      If bNew Then
        bRiscriviClibanc = True
        bRiscriviClistpg = True
        bRiscriviClitipb = True
        bRiscriviOrganig = True
        bRiscriviCodarfo = True
      End If

      '----------------------------------------
      'imposto i valodi di default
      oCldClie.SetTableDefaultValueFromDB("CLITIPB", dsShared)
      oCldClie.SetTableDefaultValueFromDB("CLIBANC", dsShared)
      oCldClie.SetTableDefaultValueFromDB("CLISTPG", dsShared)
      oCldClie.SetTableDefaultValueFromDB("ORGANIG", dsShared)
      oCldClie.SetTableDefaultValueFromDB("ANACONA", dsShared)

      TipbSetDefaultValue()
      TiprkSetDefaultValue()
      BancSetDefaultValue()
      StpgSetDefaultValue()
      AncaSetDefaultValue()

      AddHandler dsShared.Tables("CLITIPB").ColumnChanging, AddressOf TipbBeforeColUpdate
      AddHandler dsShared.Tables("CLITIPB").ColumnChanged, AddressOf TipbAfterColUpdate

      AddHandler dsShared.Tables("TRKTPBF").ColumnChanging, AddressOf TiprkBeforeColUpdate
      AddHandler dsShared.Tables("TRKTPBF").ColumnChanged, AddressOf TiprkAfterColUpdate

      AddHandler dsShared.Tables("CLIBANC").ColumnChanging, AddressOf BancBeforeColUpdate
      AddHandler dsShared.Tables("CLIBANC").ColumnChanged, AddressOf BancAfterColUpdate

      AddHandler dsShared.Tables("CLISTPG").ColumnChanging, AddressOf StpgBeforeColUpdate
      AddHandler dsShared.Tables("CLISTPG").ColumnChanged, AddressOf StpgAfterColUpdate

      AddHandler dsShared.Tables("ANACONA").ColumnChanging, AddressOf AncaBeforeColUpdate
      AddHandler dsShared.Tables("ANACONA").ColumnChanged, AddressOf AncaAfterColUpdate

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function



  Public Overridable Function CaricaColonneUnbound(ByRef dtrIn As DataRow, ByRef bContoMovimentato As Boolean) As Boolean
    '------------------------------------
    'viene eseguita ad ogni cambio di record
    Dim dtrS As DataRowState = Nothing
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      dtrS = dtrIn.RowState

      '---------------------------------------
      'carico le destinazioni diverse
      If Not CaricaDestdiv(dtrIn) Then Return False

      '---------------------------------------
      'carico ANAEXT del conto cliente/fornitore e di destdiv
      If Not CaricaAnaext(dtrIn, 0) Then Return False

      '---------------------------------------
      'carico CLIBANC, CLITPIB, ORGANIG, ...
      If Not CaricaTabelleCollegate(dtrIn) Then Return False

      '---------------------------------------
      If NTSCInt(dtrIn!an_codling) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_codling).ToString, strDittaCorrente, "TABLING", "N", strTmp)
        dtrIn!xx_codling = strTmp
      Else
        dtrIn!xx_codling = ""
      End If

      If NTSCInt(dtrIn!an_valuta) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_valuta).ToString, strDittaCorrente, "TABVALU", "N", strTmp)
        dtrIn!xx_valuta = strTmp
      Else
        dtrIn!xx_valuta = ""
      End If

      If NTSCInt(dtrIn!an_codrtac) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_codrtac).ToString, strDittaCorrente, "TABRTAC", "N", strTmp)
        dtrIn!xx_codrtac = strTmp
      Else
        dtrIn!xx_codrtac = ""
      End If

      If NTSCInt(dtrIn!an_agente) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_agente).ToString, strDittaCorrente, "TABCAGE", "N", strTmp)
        dtrIn!xx_agente = strTmp
      Else
        dtrIn!xx_agente = ""
      End If

      If NTSCInt(dtrIn!an_agente2) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_agente2).ToString, strDittaCorrente, "TABCAGE", "N", strTmp)
        dtrIn!xx_agente2 = strTmp
      Else
        dtrIn!xx_agente2 = ""
      End If

      If NTSCInt(dtrIn!an_categ) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_categ).ToString, strDittaCorrente, "TABCATE", "N", strTmp)
        dtrIn!xx_categ = strTmp
      Else
        dtrIn!xx_categ = ""
      End If

      If NTSCInt(dtrIn!an_zona) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_zona).ToString, strDittaCorrente, "TABZONE", "N", strTmp)
        dtrIn!xx_zona = strTmp
      Else
        dtrIn!xx_zona = ""
      End If

      If NTSCInt(dtrIn!an_codcana) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_codcana).ToString, strDittaCorrente, "TABCANA", "N", strTmp)
        dtrIn!xx_codcana = strTmp
      Else
        dtrIn!xx_codcana = ""
      End If

      If NTSCInt(dtrIn!an_codbanc) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_codbanc).ToString, strDittaCorrente, "TABBANC", "N", strTmp)
        dtrIn!xx_codbanc = strTmp
      Else
        dtrIn!xx_codbanc = ""
      End If

      If NTSCInt(dtrIn!an_listino) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_listino).ToString, strDittaCorrente, "TABLIST", "N", strTmp)
        dtrIn!xx_listino = strTmp
      Else
        dtrIn!xx_listino = ""
      End If

      If NTSCInt(dtrIn!an_clascon) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_clascon).ToString, strDittaCorrente, "TABCSCL", "N", strTmp)
        dtrIn!xx_clascon = strTmp
      Else
        dtrIn!xx_clascon = ""
      End If

      If NTSCInt(dtrIn!an_claprov) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_claprov).ToString, strDittaCorrente, "TABCPCL", "N", strTmp)
        dtrIn!xx_claprov = strTmp
      Else
        dtrIn!xx_claprov = ""
      End If

      If NTSCInt(dtrIn!an_codpag) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_codpag).ToString, strDittaCorrente, "TABPAGA", "N", strTmp)
        dtrIn!xx_codpag = strTmp
      Else
        dtrIn!xx_codpag = ""
      End If
      If NTSCInt(dtrIn!an_codpaga2) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_codpaga2).ToString, strDittaCorrente, "TABPAGA", "N", strTmp)
        dtrIn!xx_codpaga2 = strTmp
      Else
        dtrIn!xx_codpaga2 = ""
      End If
      If NTSCInt(dtrIn!an_codpaga3) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_codpaga3).ToString, strDittaCorrente, "TABPAGA", "N", strTmp)
        dtrIn!xx_codpaga3 = strTmp
      Else
        dtrIn!xx_codpaga3 = ""
      End If

      If NTSCInt(dtrIn!an_codpagadet) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_codpagadet).ToString, strDittaCorrente, "TABPAGA", "N", strTmp)
        dtrIn!xx_codpagadet = strTmp
      Else
        dtrIn!xx_codpagadet = ""
      End If
      If NTSCInt(dtrIn!an_codpagadet2) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_codpagadet2).ToString, strDittaCorrente, "TABPAGA", "N", strTmp)
        dtrIn!xx_codpagadet2 = strTmp
      Else
        dtrIn!xx_codpagadet2 = ""
      End If
      If NTSCInt(dtrIn!an_codpagadet3) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_codpagadet3).ToString, strDittaCorrente, "TABPAGA", "N", strTmp)
        dtrIn!xx_codpagadet3 = strTmp
      Else
        dtrIn!xx_codpagadet3 = ""
      End If

      If NTSCStr(dtrIn!an_porto) <> "" Then
        oCldClie.ValCodiceDb(NTSCStr(dtrIn!an_porto).ToString, strDittaCorrente, "TABPORT", "S", strTmp)
        dtrIn!xx_porto = strTmp
      Else
        dtrIn!xx_porto = ""
      End If

      If NTSCInt(dtrIn!an_codtpbf) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_codtpbf).ToString, strDittaCorrente, "TABTPBF", "N", strTmp)
        dtrIn!xx_codtpbf = strTmp
      Else
        dtrIn!xx_codtpbf = ""
      End If

      If NTSCInt(dtrIn!an_vett) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_vett).ToString, strDittaCorrente, "TABVETT", "N", strTmp)
        dtrIn!xx_vett = strTmp
      Else
        dtrIn!xx_vett = ""
      End If

      If NTSCInt(dtrIn!an_vett2) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_vett2).ToString, strDittaCorrente, "TABVETT", "N", strTmp)
        dtrIn!xx_vett2 = strTmp
      Else
        dtrIn!xx_vett2 = ""
      End If

      If NTSCInt(dtrIn!an_codese) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_codese).ToString, strDittaCorrente, "TABCIVA", "N", strTmp)
        dtrIn!xx_codese = strTmp
      Else
        dtrIn!xx_codese = ""
      End If

      If NTSCInt(dtrIn!an_codntra) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_codntra).ToString, strDittaCorrente, "TABNTRA", "N", strTmp)
        dtrIn!xx_codntra = strTmp
      Else
        dtrIn!xx_codntra = ""
      End If

      '----------------------
      'descrizione contropartita abituale
      If NTSCInt(dtrIn!an_controp) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_controp).ToString, strDittaCorrente, "ANAGRA", "N", strTmp, Nothing, dtrIn!an_codpcon.ToString)
        dtrIn!xx_controp = strTmp
      Else
        dtrIn!xx_controp = ""
      End If

      '----------------------
      'descrizione conto fatturazione
      If NTSCInt(dtrIn!an_contfatt) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_contfatt).ToString, strDittaCorrente, "ANAGRA", "N", strTmp, Nothing, dtrIn!an_contfatt.ToString)
        dtrIn!xx_contfatt = strTmp
      Else
        dtrIn!xx_contfatt = ""
      End If

      '----------------------
      'descrizione destinazione merce
      If NTSCInt(dtrIn!an_destin) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_destin).ToString, strDittaCorrente, "DESTDIV", "N", strTmp, Nothing, dtrIn!an_conto.ToString)
        dtrIn!xx_destin = strTmp
      Else
        dtrIn!xx_destin = ""
      End If

      '----------------------
      'descrizione destinazione pagamenti
      If NTSCInt(dtrIn!an_destpag) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_destpag).ToString, strDittaCorrente, "DESTDIV", "N", strTmp, Nothing, dtrIn!an_conto.ToString)
        dtrIn!xx_destpag = strTmp
      Else
        dtrIn!xx_destpag = ""
      End If

      '----------------------
      'tipologia entità (CAE)
      If NTSCInt(dtrIn!an_codtcdc) <> 0 Then
        oCldClie.ValCodiceDb(NTSCInt(dtrIn!an_codtcdc).ToString, strDittaCorrente, "TABTCDC", "N", strTmp)
        dtrIn!xx_codtcdc = strTmp
      Else
        dtrIn!xx_codtcdc = ""
      End If

      '----------------------
      'Aggregazione budget (CAE)
      If NTSCstr(dtrIn!an_coddica) <> "" Then
        oCldClie.ValCodiceDb(NTSCStr(dtrIn!an_coddica).ToString, strDittaCorrente, "TABDICA", "S", strTmp)
        dtrIn!xx_coddica = strTmp
      Else
        dtrIn!xx_coddica = ""
      End If

      '----------------------
      'Valori aggregazione budget (CAE)
      If NTSCstr(dtrIn!an_coddicv) <> "" Then
        oCldClie.ValCodiceDb(NTSCStr(dtrIn!an_coddicv).ToString, strDittaCorrente, "TABDICV", "S", strTmp, Nothing, dtrIn!an_coddica.ToString)
        dtrIn!xx_coddicv = strTmp
      Else
        dtrIn!xx_coddicv = ""
      End If

      '----------------------
      'Voce finanziaria (Tesoreria)
      If NTSCStr(dtrIn!an_codvfde) <> "" Then
        oCldClie.ValCodiceDb(NTSCStr(dtrIn!an_codvfde).ToString, strDittaCorrente, "TABVFDE", "S", strTmp)
        dtrIn!xx_codvfde = strTmp
      Else
        dtrIn!xx_codvfde = ""
      End If

      '----------------------
      'rimetto a posto il datarowstate della riga
      Select Case dtrS
        Case DataRowState.Added : If dtrIn.RowState <> DataRowState.Added Then dtrIn.SetAdded()
        Case DataRowState.Modified : If dtrIn.RowState <> DataRowState.Modified Then dtrIn.SetModified()
        Case DataRowState.Unchanged
          dtrIn.AcceptChanges()
          bHasChanges = False
      End Select

      '--------------------------------------
      'se il conto è stato movimentato non posso modificare i flag di gestione partite/scadenze
      bContoMovimentato = oCldClie.ContoMovimPrinot(strDittaCorrente, NTSCInt(dtrIn!an_conto))

      strMemblocco = NTSCStr(dtrIn!an_blocco)
      dMemfido = NTSCDec(dtrIn!an_fido)

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function Ripristina() As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      bHasChanges = False
      dsShared.RejectChanges()

      bNew = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function TestPreSalva(ByRef bCreaAnagen As Boolean, ByRef strDtIniAnagen As String, ByRef bMantieniLeadsDestdivDeleted As Boolean) As Boolean
    Dim dtrTmp As DataRow
    Dim i As Integer = 0
    Dim evnt As NTSEventArgs
    Dim lContoTmp As Integer = 0
    Dim strErr As String = ""
    Dim bHasRecordsAfter As Boolean = False
    Dim strTmp As String = ""
    Dim lProgr As Integer = 0
    Dim strUserID As String = ""
    Dim strPsw As String = ""
    Dim dttTmp As New DataTable
    Dim nAnno As Integer = 0
    Dim nNumero As Integer = 0
    Try
      bCreaAnagen = False

      dtrTmp = dsShared.Tables("ANAGRA").Rows(nCurRow)
      If NTSCStr(dtrTmp!an_descr1).Trim = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074225056000, "Il campo 'Ragione sociale (1a parte)' è obbligatorio")))
        Return False
      End If

      If dtrTmp!an_partite.ToString = "N" And dtrTmp!an_scaden.ToString = "S" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074294320000, "Se 'gestione scadenze' è selezionato deve esserlo anche 'gestione partite'")))
        Return False
      End If

      If dtrTmp!an_persfg.ToString = "F" And dtrTmp!an_sesso.ToString = "S" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381624795842000, "Se tipo soggetto è 'Persona fisica' il sesso deve essere 'Maschio' o 'Femmina'")))
        Return False
      End If

      If (NTSCDec(dtrTmp!an_codpagscagl1) <> 0) And _
         (NTSCDec(dtrTmp!an_codpagscagl1) >= NTSCDec(dtrTmp!an_codpagscagl2)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130565529085687557, "Attenzione!" & vbCrLf & _
          "Se indicato un 'Limite minimo importo', il 'Limite grandi importi' deve essere maggiore.")))
        Return False
      End If

      If NTSCStr(dtrTmp!an_dtaper).Trim = "" Then dtrTmp!an_dtaper = Now.ToShortDateString

      '--------------------------------
      'test su cliente bloccato
      If bNew = False Then
        If (strMemblocco <> "N") And (NTSCStr(dtrTmp!an_blocco) <> strMemblocco) Then
          evnt = New NTSEventArgs(CLN__STD.ThMsg.INPUTPWD, oApp.Tr(Me, 128738074360464000, "Inserire Password per cambiare il blocco cliente:"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue.ToUpper <> strPasswbl.ToUpper Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074428636000, "Password non corretta : variazione impossibile, risistemare campo blocco come in origine")))
            Return False
          Else
            GoTo SaltaRichiestaPassword
          End If
        End If
        If Not (strBloccaavvertifido = "S" Or strBloccainsolu = "S" Or strBloccaRDScadute = "S") Then
          GoTo SaltaRichiestaPassword
        End If
        If NTSCStr(dtrTmp!an_blocco) <> strMemblocco Or dMemfido <> NTSCDec(dtrTmp!an_fido) Then
          evnt = New NTSEventArgs(CLN__STD.ThMsg.INPUTPWD, oApp.Tr(Me, 128377920157340000, "Inserire Password per cambiare il blocco cliente:"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue.ToUpper <> strPasswbl.ToUpper Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128377921554996000, "Password non corretta : variazione impossibile, risistemare campo blocco come in origine")))
            Return False
          End If
        End If
SaltaRichiestaPassword:
      End If    'If bNew = False Then

      If oApp.ValutaCorrente.ToUpper = "EUR" Then
        If NTSCStr(dtrTmp!an_pariva) <> "" And NTSCStr(dtrTmp!an_tpsogiva) = "N" Then
          If oApp.CheckCfpi(2, NTSCStr(dtrTmp!an_pariva)) = False Then
            evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128272479176868000, "Partita IVA non corretta. Confermi ugualmente ? "))
            ThrowRemoteEvent(evnt)
            If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
          End If
        End If
        'If NTSDate(dtrTmp!an_codfis) <> "" And NTSCStr(dtrTmp!an_tpsogiva) = "N" And NTSDate(dtrTmp!an_omocodice).ToUpper = "N" Then
        If NTSCStr(dtrTmp!an_codfis) <> "" And NTSCStr(dtrTmp!an_tpsogiva) = "N" And NTSCStr(dtrTmp!an_omocodice).ToUpper = "N" Then
          If oApp.CheckCfpi(3, NTSCStr(dtrTmp!an_codfis).ToUpper) = False Then
            evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128272480218168000, "Codice fiscale non corretto. Confermi ugualmente ? "))
            ThrowRemoteEvent(evnt)
            If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
          End If
        End If
      End If

      If Not bNew Then
        If NTSCStr(dtrTmp!an_ultagg).Trim <> "" Then
          If NTSCStr(dtrTmp!an_ultagg) <> NTSCStr(oCldClie.GetUltaggAnagra(strDittaCorrente, NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto))) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128273077580698000, _
                  "L'anagrafica che si sta cercando di aggiornare è stata nel frattempo modificata." & vbCrLf & _
                  "Aggiornamento non possibile.")))
            Return False
          End If
        End If
      Else
        lProgr = NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto)
        Dim lProgrOld As Integer = 0
RIPROVA:
        strTmp = ""
        oCldClie.ValCodiceDb(lProgr.ToString, strDittaCorrente, "ANAGRA", "N", strTmp)
        If strTmp <> "" Then
          Select Case strTipoConto
            Case "C" : lProgr = oCldClie.LegNuma(strDittaCorrente, "CC", "", NTSCInt(dtrTmp!an_codmast), True)
            Case "F" : lProgr = oCldClie.LegNuma(strDittaCorrente, "FF", "", NTSCInt(dtrTmp!an_codmast), True)
          End Select
          lProgr = (NTSCInt(dtrTmp!an_codmast) * lContoProgrMoltip) + lProgr
          If lProgr = lProgrOld Then
            'Se mi propone sempre lo stesso progressivo è inutile continuare l'elaborazione
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131012346272137889, "Il codice cliente utilizzato è già in uso e le numerazioni clienti\fornitori non sono correttamente configurate.")))
            Return False
          End If
          lProgrOld = lProgr
          GoTo RIPROVA
          Return False
        End If

        If lProgr <> NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto) Then
          Select Case strTipoConto
            Case "C"
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128378013520172000, "Il codice Cliente è già stato utilizzato da un altro utente o sessione. Verrà utilizzato il progressivo |" & lProgr.ToString & "|. Salvataggio non eseguito")))
            Case "F"
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130199013512060201, "Il codice Fornitore è già stato utilizzato da un altro utente o sessione. Verrà utilizzato il progressivo |" & lProgr.ToString & "|. Salvataggio non eseguito")))
          End Select

          dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto = lProgr
          For i = 0 To dsShared.Tables("DESTDIV").Rows.Count - 1
            dsShared.Tables("DESTDIV").Rows(i)!dd_conto = lProgr
          Next
          For i = 0 To dsShared.Tables("CLITIPB").Rows.Count - 1
            dsShared.Tables("CLITIPB").Rows(i)!ctp_conto = lProgr
          Next
          For i = 0 To dsShared.Tables("CLIBANC").Rows.Count - 1
            dsShared.Tables("CLIBANC").Rows(i)!cba_conto = lProgr
          Next
          For i = 0 To dsShared.Tables("CLISTPG").Rows.Count - 1
            dsShared.Tables("CLISTPG").Rows(i)!cts_conto = lProgr
          Next
          For i = 0 To dsShared.Tables("ORGANIG").Rows.Count - 1
            dsShared.Tables("ORGANIG").Rows(i)!og_conto = lProgr
          Next

          If strGestAnaext.IndexOf(strTipoConto) > -1 Then dsShared.Tables("ANAEXT").Rows(0)!ax_conto = lProgr
          If strGestAnaext.IndexOf(IIf(strTipoConto = "C", "D", "E").ToString) > -1 Then
            For i = 0 To dsShared.Tables("ANAEXTDD").Rows.Count - 1
              dsShared.Tables("ANAEXTDD").Rows(i)!ax_conto = lProgr
            Next
          End If
          Return False
        End If
      End If
      Dim lPos As Integer
      Dim strNumero As String
      Dim strAnno As String
      'Modulo Dichiarazioni di intento:
      If CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupDII) Then
        'Eseguo la validazione solo il campo è compilato
        Select Case strTipoConto
          Case "C"
            If dsShared.Tables("ANAGRA").Rows(nCurRow)!an_numdicp.ToString.Trim <> "" Then
              lPos = InStr(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_numdicp.ToString, "/")
              If lPos > 0 Then
                nAnno = 0
                strNumero = Left(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_numdicp.ToString, lPos - 1)
                If IsNumeric(strNumero) Then
                  nNumero = NTSCInt(strNumero)
                  strAnno = Mid(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_numdicp.ToString, lPos + 1)
                  If IsNumeric(strAnno) Then
                    nAnno = NTSCInt(strAnno) + 2000
                  End If
                End If
              End If

              If nAnno = 0 Or nNumero = 0 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130766873117349537, "ATTENZIONE" & vbCrLf & _
                                "Il numero protocollo dichiarazione d'intento non è nel formato 'numero/anno' come richiesto dal modulo in oggetto." & vbCrLf & _
                                "Impossibile salvare. Per proseguire correggere il numero o svuotare il campo.")))
                Return False
              End If
              If Not oCldClie.TestDichint(strDittaCorrente, strTipoConto, nAnno, nNumero) Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129636574972998148, "ATTENZIONE" & vbCrLf & _
                                "Dichiarazione di intento inesistente." & vbCrLf & _
                                "Impossibile salvare.")))
                Return False
              End If
            End If
          Case "F"
            If dsShared.Tables("ANAGRA").Rows(nCurRow)!an_numdic.ToString.Trim <> "" Then
              lPos = InStr(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_numdic.ToString, "/")
              If lPos > 0 Then
                nAnno = 0
                strNumero = Left(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_numdic.ToString, lPos - 1)
                If IsNumeric(strNumero) Then
                  nNumero = NTSCInt(strNumero)
                  strAnno = Mid(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_numdic.ToString, lPos + 1)
                  If IsNumeric(strAnno) Then
                    nAnno = NTSCInt(strAnno) + 2000
                  End If
                End If
              End If
              If nAnno = 0 Or nNumero = 0 Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130766876906946835, "ATTENZIONE" & vbCrLf & _
                                "Il numero dichiarazione d'intento non è nel formato 'numero/anno' come richiesto dal modulo in oggetto." & vbCrLf & _
                                "Impossibile salvare. Per proseguire correggere il numero o svuotare il campo.")))
                Return False
              End If
              If Not oCldClie.TestDichint(strDittaCorrente, strTipoConto, nAnno, nNumero) Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129636574972998147, "ATTENZIONE" & vbCrLf & _
                                "Dichiarazione di intento inesistente." & vbCrLf & _
                                "Impossibile salvare.")))
                Return False
              End If
            End If
        End Select
      End If

      '--------------------------------
      'se richiesto creo userid e pwd per web
      If bNew And strGeneraIdPswClienti = "-1" And strTipoConto = "C" And _
         NTSCStr(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_webpwd) = "" And _
         NTSCStr(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_webuid) = "" Then
RICALCOLA_USERID:
        strUserID = UCase(Left(NTSCStr(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_descr1), 3)) & NTSCStr(NTSCInt((9999 * Rnd())))
        If Not oCldClie.CheckWebUserID(strDittaCorrente, NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto), strUserID) Then GoTo RICALCOLA_USERID

RICALCOLA_USERPWD:
        strPsw = NTSCStr(NTSCInt((9999999 * Rnd())))
        If Not oCldClie.CheckWebUserPwd(strDittaCorrente, NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto), strPsw) Then GoTo RICALCOLA_USERPWD

        dsShared.Tables("ANAGRA").Rows(nCurRow)!an_webuid = strUserID
        dsShared.Tables("ANAGRA").Rows(nCurRow)!an_webpwd = strPsw
      End If

      '--------------------------------
      'verifico se creare ANAGEN
      If bNew And bAnagen And NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_codanag) = 0 Then
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128376061286590000, "Creare l'anagrafica e le eventuali destinazioni anche nell'Anagrafica Generale?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then bCreaAnagen = False Else bCreaAnagen = True

        If bCreaAnagen Then
          If NTSCStr(dttAnaz.Rows(0)!tb_flriccf) <> "S" Then
            'codice anagrafica = codice cliente: verifico se esiste già
            oCldClie.ValCodiceDb(NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto).ToString, strDittaCorrente, "ANAGEN", "N", strTmp)
            If strTmp <> "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128376063050950000, "Anagrafica generale |" & NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto).ToString & "| già esistente. La creazione non sarà possibile.")))
              bCreaAnagen = False
            Else
              dsShared.Tables("ANAGRA").Rows(nCurRow)!an_codanag = NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto)
            End If
          Else
            i = oCldClie.LegNumg("AG", " ", 0, True)
            oCldClie.ValCodiceDb(i.ToString, strDittaCorrente, "ANAGEN", "N", "", dttTmp)
            If dttTmp.Rows.Count > 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129991908275875926, "Attenzione: il progressivo da attribuire alla nuova anagrafica generale letto dalla tabella numerazioni risulta essere già usato. Correggere la tabella numerazioni")))
              Return False
            End If
            dsShared.Tables("ANAGRA").Rows(nCurRow)!an_codanag = i
          End If

          Select Case strAnagenDtIniz
            Case "0"
START:
              evnt = New NTSEventArgs(CLN__STD.ThMsg.INPUTBOXDATA, oApp.Tr(Me, 128376087166562000, "Inserire la data di inizio validità in Anagrafica Generale"))
              evnt.InputValue = DateTime.Now.ToShortDateString  'valore da passare all'inputboxnew
              ThrowRemoteEvent(evnt)

              If evnt.RetValue = "" Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128376089774198000, "Non è stata indicata nessuna data: verrà impostata la data odierna")))
                strDtIniAnagen = DateTime.Now.ToShortDateString
              Else
                If IsDate(evnt.RetValue) Then
                  strDtIniAnagen = NTSCDate(evnt.RetValue).ToShortDateString
                  If NTSCDate(strDtIniAnagen) < NTSCDate(IntSetDate("01/01/1900")) Or NTSCDate(strDtIniAnagen) > NTSCDate(IntSetDate("31/12/2099")) Then
                    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128376090336110000, "La data inserita deve essere compresa tra 01/01/1900 e 31/12/2099: ripetere")))
                    GoTo START
                  End If
                Else
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128376090448742000, "La data inserita non è corretta: ripetere")))
                  GoTo START
                End If
              End If
            Case "1" : strDtIniAnagen = DateTime.Now.ToShortDateString
            Case "2" : strDtIniAnagen = NTSCDate(IntSetDate("01/01/1900")).ToShortDateString
          End Select
        End If
      End If    'If bNew And bAnagen And NTSCInt(dsSha

      bMantieniLeadsDestdivDeleted = False
      If bModuloCRM Or bModuloAS Then
        If dttDestdivDeleted.Select("dd_codlead <> 0").Length > 0 Then
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128377122757069000, "Sono state cancellate alcune destinazioni diverse collegate a LEADS: Cancellare anche i leads (YES) o mantenerli (NO)?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then bMantieniLeadsDestdivDeleted = True
        End If
      End If

      dsShared.Tables("ANAGRA").Rows(nCurRow)!an_opnome = oApp.User.Nome
      dsShared.Tables("ANAGRA").Rows(nCurRow)!an_ultagg = DateTime.Now

      If strGestAnaext.IndexOf(strTipoConto) > -1 Then
        dsShared.Tables("ANAEXT").Rows(0)!ax_opnome = oApp.User.Nome
        dsShared.Tables("ANAEXT").Rows(0)!ax_ultagg = DateTime.Now
      End If

      If strGestAnaext.IndexOf(IIf(strTipoConto = "C", "D", "E").ToString) > -1 Then
        For i = 0 To dsShared.Tables("ANAEXTDD").Rows.Count - 1
          dsShared.Tables("ANAEXTDD").Rows(i)!ax_opnome = oApp.User.Nome
          dsShared.Tables("ANAEXTDD").Rows(i)!ax_ultagg = DateTime.Now
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
      Return False
    End Try
  End Function
  Public Overridable Function TestSalvaCfPiva() As Boolean
    Dim dtrTmp As DataRow
    Dim evnt As NTSEventArgs

    Try

      If strTestSalvaCfPiva = "C" Or strTestSalvaCfPiva = "A" Then
        dtrTmp = dsShared.Tables("ANAGRA").Rows(nCurRow)
        If NTSCStr(dtrTmp!an_codfis) <> "" Then
          If oCldClie.ContoPresenteCfPi(strDittaCorrente, NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto), "", dtrTmp!an_codfis.ToString, strTipoConto) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738079995354000, "Attenzione! Codice fiscale giÃ  esistente in altra anagrafica.")))
          End If
        End If
        If NTSCStr(dtrTmp!an_pariva) <> "" Then
          If oCldClie.ContoPresenteCfPi(strDittaCorrente, NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto), dtrTmp!an_pariva.ToString, "", strTipoConto) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074949998000, "Attenzione! Partita IVA già  esistente in altra anagrafica.")))
          End If
        End If

        If strTestSalvaCfPiva = "C" Then
          If oApp.ValutaCorrente.ToUpper = "EUR" Then
            If NTSCStr(dtrTmp!an_pariva) <> "" And NTSCStr(dtrTmp!an_tpsogiva) = "N" Then
              If oApp.CheckCfpi(2, NTSCStr(dtrTmp!an_pariva)) = False Then
                evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128272474566868000, "Partita IVA non corretta. Confermi ugualmente ? "))
                ThrowRemoteEvent(evnt)
                If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
              End If
            End If
            If NTSCStr(dtrTmp!an_codfis) <> "" And NTSCStr(dtrTmp!an_tpsogiva) = "N" And NTSCStr(dtrTmp!an_omocodice).ToUpper = "N" Then
              If oApp.CheckCfpi(3, NTSCStr(dtrTmp!an_codfis).ToUpper) = False Then
                evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128272480625168000, "Codice fiscale non corretto. Confermi ugualmente ? "))
                ThrowRemoteEvent(evnt)
                If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
              End If
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
      Return False
    End Try
  End Function
  Public Overridable Function TestPreCancella(ByVal lConto As Integer, ByRef bSganciaOrganic As Boolean, ByVal dtUltagg As DateTime) As Boolean
    Dim strMsg As String = ""
    Dim evnt As NTSEventArgs
    Try
      If dtUltagg.ToShortDateString <> NTSCDate(oCldClie.GetUltaggAnagra(strDittaCorrente, NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto))).ToShortDateString Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128380290470060000, _
              "L'anagrafica che si sta cercando di cancellare è stata nel frattempo modificata." & vbCrLf & _
              "Aggiornamento non possibile.")))
        Return False
      End If

      If Not oCldClie.IsAnagDeletable(strDittaCorrente, lConto, strMsg) Then
        ThrowRemoteEvent(New NTSEventArgs("", strMsg))
        Return False
      End If

      If bModuloCRM OrElse bModuloAS And strTipoConto.ToUpper = "C" Then
        strMsg = oApp.Tr(Me, 128380285844728000, "Cancellare anche gli eventuali LEADS/CONTATTI relativi al Cliente e destinazioni associate?" & vbCrLf & _
                              " . se si sceglie 'Sì' saranno eliminati i LEADS/CONTATTI relativi al Cliente e alle sue destinazioni" & vbCrLf & _
                              " . se si sceglie 'No' saranno sganciati i LEADS/CONTATTI relativi al Cliente a alle sue destinazioni mantenendoli attivi.")
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, strMsg)
        ThrowRemoteEvent(evnt)
        If evnt.RetValue <> CLN__STD.ThMsg.RETVALUE_NO Then bSganciaOrganic = True
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

  Public Overridable Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bCreaAnagen As Boolean = False
    Dim strDtIniAnagen As String = "01/01/1900"
    Dim bMantieniLeadsDestdivDeleted As Boolean = False
    Dim bSganciaOrganic As Boolean = False
    Dim strUserCrmTmp As String = strUserCrm
    Dim bScollegaOperatoriAssociati As Boolean = False
    Dim bScollegaOperatoriAssociatiDest As Boolean = False
    Dim bDestinazioniConAgentiCambiate As Boolean = False
    Dim nAgente1Prima As Integer = 0
    Dim nAgente2Prima As Integer = 0
    Dim strOperatore1 As String = ""
    Dim strOperatore2 As String = ""
    Dim dttTmp As New DataTable
    Dim dtrTmp() As DataRow

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se si è salvato un "Cliente" esistente, cambiando uno o entrambi gli agenti, con il modulo CRM attivo,
      '--- chiede di scollegare l'eventuale agente dagli accessi per lead (tabella ACCLEAD),
      '--- non più esistente nell'Anagrafica del Cliente
      '--------------------------------------------------------------------------------------------------------------
      With dsShared.Tables("ANAGRA").Rows(nCurRow)
        If bModuloCRM AndAlso Not bNew AndAlso strTipoConto = "C" Then
          '----------------------------------------------------------------------------------------------------------
          oCldClie.ValCodiceDb(NTSCStr(!an_conto), strDittaCorrente, "ANAGRA", "N", "", dttTmp)
          nAgente1Prima = NTSCInt(dttTmp.Rows(0)!an_agente)
          nAgente2Prima = NTSCInt(dttTmp.Rows(0)!an_agente2)
          dttTmp.Clear()
          dttTmp.Dispose()
          '----------------------------------------------------------------------------------------------------------
          If nAgente1Prima <> 0 OrElse nAgente2Prima <> 0 Then
            '----------------------------------------------------------------------------------------------------------
            If (nAgente1Prima <> 0) AndAlso (nAgente1Prima <> NTSCInt(!an_agente)) AndAlso (nAgente1Prima <> NTSCInt(!an_agente2)) Then
              bScollegaOperatoriAssociati = True
              If Not oCldClie.RitornaOperatoriLead(strDittaCorrente, NTSCInt(!an_conto), nAgente1Prima, strOperatore1) Then
                GoTo SaltaControloAgenti
              End If
            End If
            If (nAgente2Prima <> 0) AndAlso (nAgente2Prima <> NTSCInt(!an_agente)) AndAlso (nAgente2Prima <> NTSCInt(!an_agente2)) Then
              bScollegaOperatoriAssociati = True
              If Not oCldClie.RitornaOperatoriLead(strDittaCorrente, NTSCInt(!an_conto), nAgente2Prima, strOperatore2) Then
                GoTo SaltaControloAgenti
              End If
            End If
          End If
          '----------------------------------------------------------------------------------------------------------
        End If
      End With
      '--------------------------------------------------------------------------------------------------------------
SaltaControloAgenti:
      '--------------------------------------------------------------------------------------------------------------
      '--- Stessa cosa sulle destinazioni
      '--------------------------------------------------------------------------------------------------------------
      If (bModuloCRM = True) And (bNew = False) And (strTipoConto = "C") Then
        If oCldClie.DestinazioniPreModifica(strDittaCorrente, NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto), dttTmp) = True Then
          For i As Integer = 0 To (dttTmp.Rows.Count - 1)
            dtrTmp = dsShared.Tables("DESTDIV").Select("dd_coddest = " & NTSCInt(dttTmp.Rows(i)!dd_coddest), "")
            If dtrTmp.Length = 0 Then
              bDestinazioniConAgentiCambiate = True
              Exit For
            Else
              If (NTSCInt(dttTmp.Rows(i)!dd_agente) <> 0) And (NTSCInt(dttTmp.Rows(i)!dd_agente) <> NTSCInt(dtrTmp(0)!dd_agente)) And (NTSCInt(dttTmp.Rows(i)!dd_agente) <> NTSCInt(dtrTmp(0)!dd_agente2)) Then
                bDestinazioniConAgentiCambiate = True
                Exit For
              End If
              If (NTSCInt(dttTmp.Rows(i)!dd_agente2) <> 0) And (NTSCInt(dttTmp.Rows(i)!dd_agente2) <> NTSCInt(dttTmp.Rows(i)!dd_agente)) And (NTSCInt(dttTmp.Rows(i)!dd_agente2) <> NTSCInt(dtrTmp(0)!dd_agente2)) Then
                bDestinazioniConAgentiCambiate = True
                Exit For
              End If
            End If
          Next
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (bScollegaOperatoriAssociati = True) Or (bDestinazioniConAgentiCambiate = True) Then
        bScollegaOperatoriAssociati = False
        bScollegaOperatoriAssociatiDest = False
        Dim evt As NTSEventArgs = Nothing
        evt = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, oApp.Tr(Me, 130444609739036130, "Attenzione!" & vbCrLf & _
          "Gli agenti relativi al Cliente e/o alle destinazioni, sono stati modificati." & vbCrLf & _
          "Eliminare, dagli 'Operatori associati al Lead/Destinazioni', quelli non più presenti nell'Anagrafica/Altri Indirizzi?"))
        ThrowRemoteEvent(evt)
        If evt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
          bScollegaOperatoriAssociati = True
          bScollegaOperatoriAssociatiDest = True
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If bDelete Then
        If Not TestPreCancella(NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto), _
                               bSganciaOrganic, _
                               NTSCDate(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_ultagg)) Then Return False

        If Not oCldClie.Cancella(strDittaCorrente, NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto), _
                                 strTipoConto, bModuloCRM, bModuloAS, bSganciaOrganic, strGestAnaext, bScriviActlog, _
                                 NTSCStr(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_descr1)) Then Return False
      Else
        If Not TestPreSalva(bCreaAnagen, strDtIniAnagen, bMantieniLeadsDestdivDeleted) Then Return False
        If bGestAlert Then  'ALERT Modifica manuale dello status di 'blocco conto' di un cliente
          AlertCambioStatusBlocco()
        End If

        If bModuloCRM AndAlso Not bIsCRMUser AndAlso strTipoConto = "C" AndAlso bNew Then
          'non sono un utente CRM ma ho il modulo attivo: 
          'se posso collego il lead all'agente, diversamente all'utente standard (default 'ADMIN')
          If NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_agente) <> 0 Then
            oCldClie.GetBusUserFromAccditoAgente(strDittaCorrente, NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_agente), strUserCrmTmp)
            If strUserCrmTmp = "" Then strUserCrmTmp = strUserCrm
          End If
        End If

        If Not oCldClie.Salva(dsShared, NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto), bNew, _
                              bNuovoContoProposto, NTSCStr(dttAnaz.Rows(0)!tb_flriccf), strDtIniAnagen, _
                              strGestAnaext, bRiscriviDestdiv, lLead, lAgenteFileApri, bAnagen, bModuloCRM, bModuloAS, _
                              dttDestdivDeleted, bMantieniLeadsDestdivDeleted, bNuovoDaAnagen, bRiscriviClitipb, _
                              bRiscriviClibanc, bRiscriviOrganig, dttOrganigDeleted, bScriviActlog, bRiscriviCodarfo, _
                              strUserCrmTmp, bRiscriviClistpg, bIsCRMUser, _
                              bScollegaOperatoriAssociati, strOperatore1, strOperatore2, False) Then Return False

        If bRiscriviOrganig Then
          If lLead <> 0 Then 'Solo per le organizzazioni che sono collegate anche ad un lead
            CType(oCleComm, CLELBMENU).CreazioneAutomaticaAttivita_CambioStatusCommercialeOrganizzazione(strDittaCorrente, dsShared.Tables("ORGANIG"), dttOrganigOld)
            CType(oCleComm, CLELBMENU).CreazioneAutomaticaAttivita_CambioOrganizzazione(strDittaCorrente, dsShared.Tables("ORGANIG"), dttOrganigOld)
          End If
          CType(oCleComm, CLELBMENU).AggiornaContattiDaDatatable(strDittaCorrente, dsShared.Tables("ORGANIG"))
        End If
      End If

      dttDestdivDeleted.Clear()
      dttDestdivDeleted.AcceptChanges()

      dttOrganigDeleted.Clear()
      dttOrganigDeleted.AcceptChanges()

      bNew = False
      bHasChanges = False

      dttOrganigOld = dsShared.Tables("ORGANIG").Copy
      dttOrganigOld.AcceptChanges()

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


  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChanges
    End Get
  End Property


  Public Overridable Function CercaLeadDaConto(ByVal lConto As Integer, ByVal lCoddest1 As Integer, _
                                               ByVal lModuliDittaDitt As Integer, _
                                               ByVal lModuliExtDittaDitt As Integer, _
                                               ByVal lModuliSupDittaDitt As Integer) As Integer
    Try

      Return oCldClie.CercaLeadDaConto(strDittaCorrente, strTipoConto, lConto, lCoddest1, lModuliDittaDitt, lModuliExtDittaDitt, lModuliSupDittaDitt)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function DuplicaConto(ByVal lContoNew As Integer, ByVal lMastroNew As Integer, _
                                           ByVal bAnagen As Boolean, ByVal bDestdiv As Boolean, _
                                           ByVal bListini As Boolean, ByVal bSconti As Boolean, _
                                           ByVal bProvv As Boolean, ByVal bCodarfo As Boolean, _
                                           ByVal strTipoContoOld As String) As Boolean
    Try
      'obsoleta
      Return DuplicaConto(lContoNew, lMastroNew, bAnagen, bDestdiv, bListini, bSconti, _
                          bProvv, bCodarfo, strTipoContoOld, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function DuplicaConto(ByVal lContoNew As Integer, ByVal lMastroNew As Integer, _
                                           ByVal bAnagen As Boolean, ByVal bDestdiv As Boolean, _
                                           ByVal bListini As Boolean, ByVal bSconti As Boolean, _
                                           ByVal bProvv As Boolean, ByVal bCodarfo As Boolean, _
                                           ByVal strTipoContoOld As String, ByVal bOrganig As Boolean) As Boolean
    Dim i As Integer = 0
    Dim lContoOld As Integer = 0
    Dim evnt As NTSEventArgs
    Dim lNum As Integer = 0
    Dim strErr As String = ""
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {lContoNew, lMastroNew, bAnagen, bDestdiv, bListini, bSconti, _
                                             bProvv, bCodarfo, strTipoContoOld, bOrganig})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      lContoOld = NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto)

      nCurRow = 0
      lLead = 0

      If Not bAnagen Then
        bNuovoDaAnagen = False
        dsShared.Tables("ANAGRA").Rows(nCurRow)!an_codanag = 0
      Else
        bNuovoDaAnagen = True
      End If

      '---------------------
      'correggo destdiv
      If Not bDestdiv Then
        dsShared.Tables("DESTDIV").Clear()
        If dsShared.Tables.Contains("ANAEXTDD") Then dsShared.Tables("ANAEXTDD").Clear()
        dsShared.Tables("ANAGRA").Rows(nCurRow)!an_destcorr = 0
        dsShared.Tables("ANAGRA").Rows(nCurRow)!an_destdomf = 0
        dsShared.Tables("ANAGRA").Rows(nCurRow)!an_destresan = 0
        dsShared.Tables("ANAGRA").Rows(nCurRow)!an_destsedel = 0
        dsShared.Tables("ANAGRA").Rows(nCurRow)!an_destin = 0
        dsShared.Tables("ANAGRA").Rows(nCurRow)!an_destpag = 0
        '------------------------------------------------------------------------------------------------------------
        For i = (dsShared.Tables("ORGANIG").Rows.Count - 1) To 0 Step -1
          If NTSCInt(dsShared.Tables("ORGANIG").Rows(i)!og_coddest) <> 0 Then
            dsShared.Tables("ORGANIG").Rows(i).Delete()
          End If
        Next
        dsShared.Tables("ORGANIG").AcceptChanges()
        '------------------------------------------------------------------------------------------------------------
      Else
        For i = 0 To dsShared.Tables("DESTDIV").Rows.Count - 1
          dsShared.Tables("DESTDIV").Rows(i)!dd_conto = lContoNew
          dsShared.Tables("DESTDIV").Rows(i)!dd_codlead = 0
        Next
      End If

      '---------------------
      'correggo anagra
      dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto = lContoNew
      dsShared.Tables("ANAGRA").Rows(nCurRow)!an_tipo = strTipoConto
      dsShared.Tables("ANAGRA").Rows(nCurRow)!an_codmast = lMastroNew
      dsShared.Tables("ANAGRA").Rows(nCurRow)!an_webpwd = ""
      dsShared.Tables("ANAGRA").Rows(nCurRow)!an_webuid = ""

      '---------------------
      'anaext: se non lo gestivo sul tipoconto iniziale ma ora lo gestisco devo inizializzarlo
      '        sia per anagra che per destdiv
      If strGestAnaext.IndexOf(strTipoContoOld) = -1 And strGestAnaext.IndexOf(strTipoConto) > -1 Then
        CaricaAnaext(dsShared.Tables("ANAGRA").Rows(nCurRow), 1)
      End If
      If strGestAnaext.IndexOf(IIf(strTipoContoOld = "C", "D", "E").ToString) = -1 And _
         strGestAnaext.IndexOf(IIf(strTipoConto = "C", "D", "E").ToString) > -1 Then
        CaricaAnaext(dsShared.Tables("ANAGRA").Rows(nCurRow), 2)
      End If

      '---------------------
      'se per il nuovo tipo non gestisco anaext 
      If strGestAnaext.IndexOf(strTipoConto) = -1 Then
        If dsShared.Tables.Contains("ANAEXT") Then dsShared.Tables("ANAEXT").Clear()
      End If
      If strGestAnaext.IndexOf(IIf(strTipoConto = "C", "D", "E").ToString) = -1 Then
        If dsShared.Tables.Contains("ANAEXTDD") Then dsShared.Tables("ANAEXTDD").Clear()
      End If

      '---------------------
      'correggo anaext e anaext di destdiv
      If strGestAnaext.IndexOf(strTipoConto) > -1 Then
        If dsShared.Tables.Contains("ANAEXT") Then
          dsShared.Tables("ANAEXT").Rows(0)!ax_conto = lContoNew
          dsShared.Tables("ANAEXT").Rows(0)!ax_tipork = strTipoConto
        End If
      End If
      If strGestAnaext.IndexOf(IIf(strTipoConto = "C", "D", "E").ToString) > -1 Then
        If dsShared.Tables.Contains("ANAEXT") Then
          For i = 0 To dsShared.Tables("ANAEXTDD").Rows.Count - 1
            dsShared.Tables("ANAEXTDD").Rows(i)!ax_conto = lContoNew
            dsShared.Tables("ANAEXTDD").Rows(i)!ax_tipork = IIf(strTipoConto = "C", "D", "E").ToString
          Next
        End If
      End If

      '---------------------
      'correggo clitipb
      dsShared.Tables("CLITIPB").Clear()
      'For i = 0 To dsShared.Tables("CLITIPB").Rows.Count - 1
      '  dsShared.Tables("CLITIPB").Rows(i)!ctp_conto = lContoNew
      'Next

      '---------------------
      'correggo clibanc
      dsShared.Tables("CLIBANC").Clear()
      'For i = 0 To dsShared.Tables("CLIBANC").Rows.Count - 1
      '  dsShared.Tables("CLIBANC").Rows(i)!cba_conto = lContoNew
      'Next

      '---------------------
      'correggo trktpbf
      For i = 0 To dsShared.Tables("TRKTPBF").Rows.Count - 1
        dsShared.Tables("TRKTPBF").Rows(i)!tkt_conto = lContoNew
      Next

      '---------------------
      'correggo clistpg
      dsShared.Tables("CLISTPG").Clear()

      dsShared.Tables("ANACONA").Clear()

      '---------------------
      'correggo organig
      '--------------------------------------------------------------------------------------------------------------
      '--- Se si sta duplicando l'Organizzazione del conto
      '--- e l'opzione di registro "BS--CLIE/OPZIONI/SpostaOrganizzazioneInDuplica" è attiva,
      '--- NON determina/salva un nuovo progressivo, ma appoggia il progressivo di origine
      '--- nel campo non più utilizzato ORGANIG.og_nomeserv
      '--------------------------------------------------------------------------------------------------------------
      If bOrganig Then
        Dim bDuplicaORGANIG As Boolean = _
          CBool(oCldClie.GetSettingBus("BS--CLIE", "OPZIONI", ".", "SpostaOrganizzazioneInDuplica", "0", " ", "0"))
        If strTipoConto.ToUpper <> "C" Then bDuplicaORGANIG = False
        For i = 0 To dsShared.Tables("ORGANIG").Rows.Count - 1
          If bDuplicaORGANIG = False Then
            lNum = ocldBase.LegNuma(strDittaCorrente, "OG", " ", 0, True)
            lNum = ocldBase.AggNuma(strDittaCorrente, "OG", " ", 0, lNum, True, True, strErr)
          End If
          If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
          If bDuplicaORGANIG = False Then
            dsShared.Tables("ORGANIG").Rows(i)!og_progr = lNum
          Else
            dsShared.Tables("ORGANIG").Rows(i)!og_nomeserv = NTSCStr(dsShared.Tables("ORGANIG").Rows(i)!og_progr)
          End If
          dsShared.Tables("ORGANIG").Rows(i)!og_conto = lContoNew
        Next
        '------------------------------------------------------------------------------------------------------------
      Else
        dsShared.Tables("ORGANIG").Clear()
      End If

      '---------------------
      'correggo codarfo
      If bCodarfo Then
        For i = 0 To dsShared.Tables("CODARFO").Rows.Count - 1
          dsShared.Tables("CODARFO").Rows(i)!caf_conto = lContoNew
        Next
      Else
        dsShared.Tables("CODARFO").Clear()
      End If

      evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128473835841698662, "Salvare il cliente/fornitore appena creato?"))
      ThrowRemoteEvent(evnt)
      If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False

      Salva(False)
      dsShared.AcceptChanges()

      '---------------------
      'duplico listini / sconti / provvigioni 
      If bListini Or bSconti Or bProvv Then
        'ora duplico listini/sconti/provv
        oCldClie.DuplicaListiniScontiProvv(strDittaCorrente, lContoOld, lContoNew, bListini, bSconti, bProvv, NTSCStr(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_tipo))
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

  Public Overridable Function GetWhereHlmo(ByVal lConto As Integer) As String
    Try

      Return oCldClie.GetWhereHlmo(strDittaCorrente, lConto)

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

  Public Overridable Function AlertCambioStatusBlocco() As Boolean
    Dim dttAlert As DataTable = Nothing
    Dim strOld As String = ""
    Dim strNew As String = ""
    Try
      If bNew Then Return True

      If NTSCStr(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_blocco) <> NTSCStr(dsShared.Tables("ANAGRA").Rows(nCurRow)("an_blocco", DataRowVersion.Original)) Then

        Select Case NTSCStr(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_blocco)
          Case "F" : strNew = "Fuori fido"
          Case "I" : strNew = "Insoluti"
          Case "N" : strNew = "(Nessuno)"
          Case "B" : strNew = "Blocco fisso"
          Case "R" : strNew = "RD Scadute"
        End Select

        Select Case NTSCStr(dsShared.Tables("ANAGRA").Rows(nCurRow)("an_blocco", DataRowVersion.Original))
          Case "F" : strOld = "Fuori fido"
          Case "I" : strOld = "Insoluti"
          Case "N" : strOld = "(Nessuno)"
          Case "B" : strOld = "Blocco fisso"
          Case "R" : strOld = "RD Scadute"
        End Select

        dttAlert = CType(oCleComm, CLELBMENU).CreaDynasetAlert
        dttAlert.Rows.Add(dttAlert.NewRow)
        dttAlert.Rows(0)!codditt = strDittaCorrente
        dttAlert.Rows(0)!strMsg = oApp.Tr(Me, 128735766416222000, _
                                 "Cliente |" & NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto).ToString & _
                                 "| - '|" & NTSCStr(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_descr1) & _
                                 "|', è stato cambiato manualmente lo status di 'Blocco conto'." & vbCrLf & _
                                 "Lo status di 'Blocco conto' è passato da '|" & strOld & "|' a '|" & strNew & "|'.")
        dttAlert.AcceptChanges()
        CType(oCleComm, CLELBMENU).Verifica_Genera_Alert(2, strDittaCorrente, "BS--CLIE", 1, 0, dttAlert)

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

  Public Overridable Function LeggiDichint(ByVal nAnno As Integer, ByVal nNumero As Integer, ByRef dttDich As DataTable) As Boolean
    Try
      Return oCldClie.LeggiDichint(strDittaCorrente, strTipoConto, nAnno, nNumero, dttDich)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub CopiaOrganizzazioneSuAnagra(ByVal dtrAnagra As DataRow, ByVal dtrOrga As DataRow)
    Try
      With dtrOrga
        dtrAnagra!an_descr1 = (NTSCStr(!og_descont) & " " & NTSCStr(!og_descont2)).Trim
        dtrAnagra!an_titolo = !og_titolo
        dtrAnagra!an_sesso = !og_sesso
        dtrAnagra!an_datnasc = !og_datnasc
        dtrAnagra!an_persf = "S"
        dtrAnagra!an_datini = !og_dtiniz
        dtrAnagra!an_datfin = !og_dtfine
        dtrAnagra!an_indir = !og_indir
        dtrAnagra!an_cap = !og_cap
        dtrAnagra!an_citta = !og_citta
        dtrAnagra!an_prov = !og_prov
        dtrAnagra!an_stato = !og_stato
        dtrAnagra!an_telef = !og_telef
        dtrAnagra!an_faxtlx = !og_fax
        dtrAnagra!an_email = !og_email
        dtrAnagra!an_cell = !og_cell
        dtrAnagra!an_usaem = !og_usaem
      End With
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Function VerificaPresenzaLeads() As Boolean
    Try
      'Non devono essere presenti sia i lead ma devono esserci le anagrafiche
      Return oCldClie.VerificaPresenzaLeads(strDittaCorrente) OrElse Not oCldClie.VerificaPresenzaClienti(strDittaCorrente)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function ArticoliDeteriorabili() As Boolean
    Try
      Return oCldClie.ArticoliDeteriorabili(strDittaCorrente)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function


  Public Overridable Function CheckInitGlobali() As Boolean
    Try
      Return oCldClie.CheckInitGlobali()
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function


#Region "Before/After update"
  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
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

  Public Overridable Sub BeforeColUpdate_an_codling(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codling = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If Not oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABLING", "N", strTmp) Then
          e.ProposedValue = e.Row!an_codling.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074630664000, "Codice lingua inesistente")))
        Else
          e.Row!xx_codling = strTmp
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

  Public Overridable Sub BeforeColUpdate_an_valuta(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_valuta = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If Not oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABVALU", "N", strTmp) Then
          e.ProposedValue = e.Row!an_valuta.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074664360000, "Codice valuta inesistente")))
        Else
          e.Row!xx_valuta = strTmp
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

  Public Overridable Sub BeforeColUpdate_an_codrtac(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codrtac = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If Not oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABRTAC", "N", strTmp) Then
          e.ProposedValue = e.Row!an_codrtac.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074679492000, "Codice assog. ritenuta acconto")))
        Else
          e.Row!xx_codrtac = strTmp
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

  Public Overridable Sub BeforeColUpdate_an_descr1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If e.ProposedValue.ToString.Trim <> "" And bNonProporreSiglaRic = False And bNew = True Then
        If e.ProposedValue.ToString.Length > 20 Then
          e.Row!an_siglaric = e.ProposedValue.ToString.Substring(0, 20)
        Else
          e.Row!an_siglaric = e.ProposedValue.ToString
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

  Public Overridable Sub BeforeColUpdate_an_codfis(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bOk As Boolean = False
    Dim evnt As NTSEventArgs
    Try
      If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
      End If

      If e.ProposedValue.ToString <> "" Then
        If oApp.ValutaCorrente.ToUpper = "EUR" And NTSCStr(e.Row!an_tpsogiva) = "N" Then
          If oApp.CheckCfpi(3, e.ProposedValue.ToString) = False Then
            If NTSCStr(e.Row!an_omocodice) = "N" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074901626000, "Codice fiscale non corretto")))
              bOk = True
            End If
          End If
        End If

        If oCldClie.ContoPresenteCfPi(strDittaCorrente, NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto), "", e.ProposedValue.ToString, strTipoConto) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074915354000, "Attenzione! Codice fiscale già esistente in altra anagrafica.")))
        End If

        If bOk = False AndAlso NTSCStr(e.Row!an_tpsogiva) <> "I" AndAlso NTSCStr(e.Row!an_tpsogiva) <> "E" AndAlso NTSCStr(e.Row!an_tpsogiva) <> "R" Then
          'tre lettere
          If (Not IsNumeric(Mid(e.ProposedValue.ToString, 1, 1))) And (Not IsNumeric(Mid(e.ProposedValue.ToString, 2, 1))) And (Not IsNumeric(Mid(e.ProposedValue.ToString, 3, 1))) Then
            If NTSCStr(e.Row!an_persfg) = "G" Then
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128296617726528000, "Attenzione : il codice fiscale impostato appartiene ad una persona fisica ed il campo 'tipo soggetto' non è 'persona fisica'. Vuoi aggiornare il campo 'tipo soggetto' ?"))
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then e.Row!an_persfg = "F"
            End If
            ' 11 cifre
          ElseIf (IsNumeric(e.ProposedValue.ToString) And Len(e.ProposedValue.ToString) = 11) Then
            If NTSCStr(e.Row!an_persfg) = "F" Then
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128296618205136000, "Attenzione : il codice fiscale impostato appartiene ad una persona giuridica ed il campo 'tipo soggetto' non è 'persona giuridica'. Vuoi aggiornare il campo 'tipo soggetto' ?"))
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then e.Row!an_persfg = "G"
            End If
          End If    ' If (Not IsNumeric(Mid(e.ProposedValue.ToSt
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

  Public Overridable Sub BeforeColUpdate_an_pariva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
      End If

      If e.ProposedValue.ToString <> "" Then
        If oApp.ValutaCorrente.ToUpper = "EUR" And NTSCStr(e.Row!an_tpsogiva) = "N" Then
          If oApp.CheckCfpi(2, e.ProposedValue.ToString) = False Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074930642000, "Partita IVA non corretta")))
          End If
        End If

        If oCldClie.ContoPresenteCfPi(strDittaCorrente, NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto), e.ProposedValue.ToString, "", strTipoConto) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074948738000, "Attenzione! Partita IVA già esistente in altra anagrafica.")))
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

  Public Overridable Sub BeforeColUpdate_an_prov(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
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

  Public Overridable Sub BeforeColUpdate_an_pronasc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
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

  Public Overridable Sub BeforeColUpdate_an_codcomu(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_codcomu = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If Not oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "COMUNI", "S", strTmp, dttTmp) Then
          e.ProposedValue = e.Row!an_codcomu.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074694000000, "Codice comune inesistente")))
        Else
          e.Row!xx_codcomu = strTmp
          e.Row!an_cap = NTSCStr(dttTmp.Rows(0)!co_cap)
          e.Row!an_citta = NTSCStr(dttTmp.Rows(0)!co_denom)
          e.Row!an_prov = NTSCStr(dttTmp.Rows(0)!co_prov)
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

  Public Overridable Sub BeforeColUpdate_an_codcomn(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_codcomn = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If Not oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "COMUNI", "S", strTmp, dttTmp) Then
          e.ProposedValue = e.Row!an_codcomn.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074711628000, "Codice comune di nascita inesistente")))
        Else
          e.Row!xx_codcomn = strTmp
          e.Row!an_citnasc = NTSCStr(dttTmp.Rows(0)!co_denom)
          e.Row!an_pronasc = NTSCStr(dttTmp.Rows(0)!co_prov)
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

  Public Overridable Sub BeforeColUpdate_an_stato(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = " " Then e.ProposedValue = ""
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_stato = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.Row!xx_stato = strTmp
        Else
          e.Row!xx_stato = ""
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

  Public Overridable Sub BeforeColUpdate_an_stanasc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = " " Then e.ProposedValue = ""
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_stanasc = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.Row!xx_stanasc = strTmp
        Else
          e.Row!xx_stanasc = ""
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

  Public Overridable Sub BeforeColUpdate_an_nazion1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = " " Then e.ProposedValue = ""
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_nazion1 = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.Row!xx_nazion1 = strTmp
        Else
          e.Row!xx_nazion1 = ""
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

  Public Overridable Sub BeforeColUpdate_an_nazion2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = " " Then e.ProposedValue = ""
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_nazion2 = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.Row!xx_nazion2 = strTmp
        Else
          e.Row!xx_nazion2 = ""
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

  Public Overridable Sub BeforeColUpdate_an_destin(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim i As Integer = 0
    Dim bOk As Boolean = False
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_destin = ""
      Else
        'nel caso in cui l'anagrafica C/F viene creata dalla anagrafica generale
        'i codici delle destinazioni non sono ancora agganciati al nuovo codice anagrafica C/F
        'quindi la validazione della destinazione diversa deve essere evitata
        If dsShared.Tables("DESTDIV") IsNot Nothing Then
          If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
            e.ProposedValue = e.ProposedValue.ToString.ToUpper
          End If
          For i = 0 To dsShared.Tables("DESTDIV").Rows.Count - 1
            If NTSCInt(e.ProposedValue) = NTSCInt(dsShared.Tables("DESTDIV").Rows(i)!dd_coddest) Then
              bOk = True
              Exit For
            End If
          Next
          If bOk = False Then
            e.ProposedValue = e.Row!an_destin.ToString
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074725668000, "Codice destinazione merce inesistente")))
          Else
            e.Row!xx_destin = dsShared.Tables("DESTDIV").Rows(i)!dd_nomdest.ToString
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
  End Sub

  Public Overridable Sub BeforeColUpdate_an_destpag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim i As Integer = 0
    Dim bOk As Boolean = False
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_destpag = ""
      Else
        'nel caso in cui l'anagrafica C/F viene creata dalla anagrafica generale
        'i codici delle destinazioni non sono ancora agganciati al nuovo codice anagrafica C/F
        'quindi la validazione della destinazione diversa deve essere evitata
        If dsShared.Tables("DESTDIV") IsNot Nothing Then
          If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
            e.ProposedValue = e.ProposedValue.ToString.ToUpper
          End If
          For i = 0 To dsShared.Tables("DESTDIV").Rows.Count - 1
            If NTSCInt(e.ProposedValue) = NTSCInt(dsShared.Tables("DESTDIV").Rows(i)!dd_coddest) Then
              bOk = True
              Exit For
            End If
          Next
          If bOk = False Then
            e.ProposedValue = e.Row!an_destpag.ToString
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074740488000, "Codice destinazione pagamenti inesistente")))
          Else
            e.Row!xx_destpag = dsShared.Tables("DESTDIV").Rows(i)!dd_nomdest.ToString
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
  End Sub

  Public Overridable Sub BeforeColUpdate_an_abi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!an_banc1 = ""
        e.Row!an_cab = 0
        e.Row!an_banc2 = ""
        e.Row!an_swift = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ABI", "N", strTmp) = False Then
        e.ProposedValue = 0
        e.Row!an_banc1 = ""
        e.Row!an_cab = 0
        e.Row!an_banc2 = ""
        e.Row!an_swift = ""
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128033192474713393, "Codice abi non corretto.")))
      Else
        e.Row!an_banc1 = strTmp.Trim
        e.Row!an_cab = 0
        e.Row!an_banc2 = ""
        e.Row!an_swift = ""
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

  Public Overridable Sub BeforeColUpdate_an_cab(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!an_banc2 = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "CAB", "N", strTmp, dttTmp, e.Row!an_abi.ToString) = False Then
        e.ProposedValue = 0
        e.Row!an_banc2 = ""
        e.Row!an_swift = ""
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128033198691658675, "Codice cab non corretto")))
      Else
        e.Row!an_banc2 = strTmp.Trim
        e.Row!an_swift = NTSCStr(dttTmp.Rows(0)!abcswift)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_an_agente(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_agente = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCAGE", "N", strTmp) Then
          e.Row!xx_agente = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738075188580000, "Codice agente inesistente")))
          e.ProposedValue = e.Row!an_agente
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

  Public Overridable Sub BeforeColUpdate_an_agente2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_agente2 = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCAGE", "N", strTmp) Then
          e.Row!xx_agente2 = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128376963266010000, "Codice agente2 inesistente")))
          e.ProposedValue = e.Row!an_agente2
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

  Public Overridable Sub BeforeColUpdate_an_categ(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_categ = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCATE", "N", strTmp) Then
          e.Row!xx_categ = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381593451978000, "Codice categoria inesistente")))
          e.ProposedValue = e.Row!an_categ
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

  Public Overridable Sub BeforeColUpdate_an_claprov(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_claprov = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCPCL", "N", strTmp) Then
          e.Row!xx_claprov = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381597639814000, "Codice classe provvigioni inesistente")))
          e.ProposedValue = e.Row!an_claprov
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

  Public Overridable Sub BeforeColUpdate_an_clascon(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_clascon = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCSCL", "N", strTmp) Then
          e.Row!xx_clascon = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381598144162000, "Codice classe sconto inesistente")))
          e.ProposedValue = e.Row!an_clascon
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

  Public Overridable Sub BeforeColUpdate_an_codbanc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codbanc = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABBANC", "N", strTmp) Then
          e.Row!xx_codbanc = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381598864258000, "Codice nostra banca inesistente")))
          e.ProposedValue = e.Row!an_codbanc
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

  Public Overridable Sub BeforeColUpdate_an_codcana(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codcana = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCANA", "N", strTmp) Then
          e.Row!xx_codcana = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381599354410000, "Codice canale inesistente")))
          e.ProposedValue = e.Row!an_codcana
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

  Public Overridable Sub BeforeColUpdate_an_codese(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codese = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCIVA", "N", strTmp, dttTmp) Then

          If NTSCInt(dttTmp.Rows(0)!tb_tipiva.ToString) = 1 Then
            Dim evt As NTSEventArgs = Nothing
            evt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128033198729023457, "Attenzione ! Codice IVA non di esenzione. confermi ugualmente?"))
            ThrowRemoteEvent(evt)
            If evt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then
              e.ProposedValue = e.Row!an_codese
            Else
              e.Row!xx_codese = strTmp
            End If
          Else
            e.Row!xx_codese = strTmp
          End If
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381603613858000, "Codice IVA inesistente")))
          e.ProposedValue = e.Row!an_codese
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
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_an_codtpbf(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codtpbf = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strTmp) Then
          e.Row!xx_codtpbf = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381607827822000, "Tipo Bolla/Fattura inesistente")))
          e.ProposedValue = e.Row!an_codtpbf
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

  Public Overridable Sub BeforeColUpdate_an_codntra(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codntra = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABNTRA", "N", strTmp) Then
          e.Row!xx_codntra = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381606514778000, "Codice natura transazione inesistente")))
          e.ProposedValue = e.Row!an_codntra
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

  Public Overridable Sub BeforeColUpdate_an_codpag(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codpag = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABPAGA", "N", strTmp) Then
          e.Row!xx_codpag = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738075329400000, "Codice pagamento inesistente")))
          e.ProposedValue = e.Row!an_codpag
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

  Public Overridable Sub BeforeColUpdate_an_codpaga2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codpaga2 = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABPAGA", "N", strTmp) Then
          e.Row!xx_codpaga2 = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130952465556964782, "Codice pagamento 2 inesistente")))
          e.ProposedValue = e.Row!an_codpaga2
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

  Public Overridable Sub BeforeColUpdate_an_codpaga3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codpaga3 = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABPAGA", "N", strTmp) Then
          e.Row!xx_codpaga3 = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130952465599797900, "Codice pagamento 2 inesistente")))
          e.ProposedValue = e.Row!an_codpaga3
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


  Public Overridable Sub BeforeColUpdate_an_codpagadet(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codpagadet = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABPAGA", "N", strTmp) Then
          e.Row!xx_codpagadet = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130565523423119376, "Codice pagamento per articoli deteriorabili inesistente")))
          e.ProposedValue = e.Row!an_codpagadet
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
  Public Overridable Sub BeforeColUpdate_an_codpagadet2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codpagadet2 = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABPAGA", "N", strTmp) Then
          e.Row!xx_codpagadet2 = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130952465682713831, "Codice pagamento per articoli deteriorabili inesistente")))
          e.ProposedValue = e.Row!an_codpagadet2
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
  Public Overridable Sub BeforeColUpdate_an_codpagadet3(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codpagadet3 = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABPAGA", "N", strTmp) Then
          e.Row!xx_codpagadet3 = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130565523595913483, "Codice pagamento per articoli deteriorabili inesistente")))
          e.ProposedValue = e.Row!an_codpagadet3
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

  Public Overridable Sub BeforeColUpdate_an_contfatt(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_contfatt = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strTmp, dttTmp) Then
          If dttTmp.Rows(0)!an_tipo.ToString = "S" Then
            e.ProposedValue = e.Row!an_contfatt
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381614304678000, "Il conto fatturazione deve far parte dei Clienti o dei Fornitori.")))
          Else
            e.Row!xx_contfatt = strTmp
          End If
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381612052050000, "Codice conto fatturazione inesistente")))
          e.ProposedValue = e.Row!an_contfatt
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
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_an_dtaper(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCStr(e.ProposedValue).Trim = "" Then
        e.ProposedValue = Now.ToShortDateString
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

  Public Overridable Sub BeforeColUpdate_an_controp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_controp = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strTmp, dttTmp) Then
          If dttTmp.Rows(0)!an_tipo.ToString <> "S" Then
            e.ProposedValue = e.Row!an_controp
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381614899350000, "Il conto contropartita abituale deve far parte dei sottoconti.")))
          Else
            e.Row!xx_controp = strTmp
          End If
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381614875638000, "Codice conto contropartita abituale inesistente")))
          e.ProposedValue = e.Row!an_controp
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
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_an_listino(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_listino = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABLIST", "N", strTmp) Then
          e.Row!xx_listino = strTmp
        Else
          'ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381626009522000, "Codice listino inesistente")))
          e.Row!xx_listino = ""
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

  Public Overridable Sub BeforeColUpdate_an_porto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_porto = ""
      Else
        If e.ProposedValue.ToString.ToUpper <> e.ProposedValue.ToString Then e.ProposedValue = e.ProposedValue.ToString.ToUpper
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABPORT", "S", strTmp) Then
          e.Row!xx_porto = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381636606624000, "Codice porto inesistente")))
          e.ProposedValue = e.Row!an_porto
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

  Public Overridable Sub BeforeColUpdate_an_vett(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_vett = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABVETT", "S", strTmp) Then
          e.Row!xx_vett = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738075471188000, "Codice vettore inesistente")))
          e.ProposedValue = NTSCStr(e.Row!an_vett)
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

  Public Overridable Sub BeforeColUpdate_an_vett2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_vett2 = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABVETT", "S", strTmp) Then
          e.Row!xx_vett2 = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128376964204662000, "Codice vettore 2 inesistente")))
          e.ProposedValue = NTSCStr(e.Row!an_vett2)
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

  Public Overridable Sub BeforeColUpdate_an_zona(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_zona = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABZONE", "S", strTmp) Then
          e.Row!xx_zona = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128376962359494000, "Codice zona inesistente")))
          e.ProposedValue = NTSCStr(e.Row!an_zona)
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

  Public Overridable Sub BeforeColUpdate_an_persfg(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      Select Case NTSCStr(e.ProposedValue)
        Case "F"
          If NTSCStr(e.Row!an_sesso) = "S" Then e.Row!an_sesso = "M"
        Case "G"
          e.Row!an_cognome = ""
          e.Row!an_nome = ""
          e.Row!an_sesso = "S"
      End Select
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_an_codtcdc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New datatable
    Try
      If ntscint(e.proposedvalue) = 0 Then e.row!xx_codtcdc = "" : Return

      If ocldclie.valcodicedb(ntscstr(e.Proposedvalue), strDittaCorrente, "TABTCDC", "N", , dttTmp) Then
        If ntscstr(dttTmp.rows(0)!tb_tipork) = "K" Then
          e.row!xx_codtcdc = dttTmp.rows(0)!tb_destcdc
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701344910501, "La Tipologia entità deve essere di tipo 'Cliente'")))
          e.proposedvalue = e.row!an_codtcdc
        End If
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701332254575, "Tipologia entità inesistente")))
        e.proposedvalue = e.row!an_codtcdc
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

  Public Overridable Sub BeforeColUpdate_an_coddica(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New datatable
    Try
      If NTSCStr(e.ProposedValue).Trim = "" Then e.ProposedValue = " " : e.Row!xx_coddica = "" : e.Row!an_coddicv = "" : Return

      e.ProposedValue = NTSCStr(e.ProposedValue).ToUpper

      If ocldclie.valcodicedb(ntscstr(e.Proposedvalue), strDittaCorrente, "TABDICA", "S", , dttTmp) Then
        If ntscstr(dttTmp.rows(0)!tb_applicaa) = "K" Then
          If ntscint(dttTmp.rows(0)!tb_liv) <> 1 Then
            e.row!xx_coddica = dttTmp.rows(0)!tb_desdica
            e.row!an_coddicv = ""
          Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701312880071, "Il codice di aggregazione budget non deve essere di primo livello")))
            e.proposedvalue = e.row!an_coddica
          End If
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701302567835, "Il codice di aggregazione budget deve essere per 'Cliente'")))
          e.proposedvalue = e.row!an_coddica
        End If
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701289911909, "Aggregazione budget inesistente")))
        e.proposedvalue = e.row!an_coddica
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

  Public Overridable Sub BeforeColUpdate_an_coddicv(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New datatable
    Try
      If ntscstr(e.proposedvalue).trim = "" Then e.row!xx_coddicv = "" : e.proposedvalue = " " : Return

      If ntscstr(e.row!an_coddica).trim = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701049449315, "Prima inserire un codice di aggregazione budget.")))
        e.proposedvalue = " "
        Return
      End If

      e.ProposedValue = NTSCStr(e.ProposedValue).ToUpper

      If ocldclie.valcodicedb(ntscstr(e.Proposedvalue), strDittaCorrente, "TABDICV", "S", , dttTmp, ntscstr(e.row!an_coddica)) Then
        e.row!xx_coddicv = dttTmp.rows(0)!tb_desdicv
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129319701272568603, "Valore aggregazione budget inesistente")))
        e.proposedvalue = e.row!an_coddicv
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

  Public Overridable Sub BeforeColUpdate_an_codvfde(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If NTSCStr(e.ProposedValue).Trim = "" Then e.Row!xx_codvfde = "" : Return

      e.ProposedValue = NTSCStr(e.ProposedValue).ToUpper

      If oCldClie.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABVFDE", "S", , dttTmp) Then
        e.Row!xx_codvfde = dttTmp.Rows(0)!tb_desvfde
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129526002770372416, "Voce finanziaria inesistente")))
        e.ProposedValue = e.Row!an_codvfde
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

  Public Overridable Sub BeforeColUpdate_an_status(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      If (bModuloCRM = False) Or (bIsCRMUser = False) Or (strTipoConto.ToUpper <> "C") Then Return
      '--------------------------------------------------------------------------------------------------------------
      If e.ProposedValue.ToString = e.Row!an_status.ToString.ToUpper Then Return
      '--------------------------------------------------------------------------------------------------------------
      If dsShared.Tables("DESTDIV").Rows.Count = 0 Then Return
      '--------------------------------------------------------------------------------------------------------------
      Dim evt As NTSEventArgs = Nothing
      evt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 129965961371445947, "Attenzione!" & vbCrLf & _
        "E' stato modificato lo status del Cliente." & vbCrLf & _
        "Allineare lo status anche sulle eventuali destinazioni diverse relative?"))
      ThrowRemoteEvent(evt)
      If evt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dsShared.Tables("DESTDIV").Rows.Count - 1)
        With dsShared.Tables("DESTDIV").Rows(i)
          !xx_lestatus = e.ProposedValue.ToString
        End With
      Next
      dsShared.Tables("DESTDIV").AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

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

  Public Overridable Sub AfterColUpdate_an_abi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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

  Public Overridable Sub AfterColUpdate_an_cab(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
#End Region

#Region "FUNZIONI PER BN__DESG"

  Public Overridable Function DesgSetDataTable(ByVal strDitta As String, ByRef dttDestdiv As DataTable) As Boolean
    Try
      dttDestdivEx = dttDestdiv

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dttDestdivEx.ColumnChanging, AddressOf DesgBeforeColUpdate
      AddHandler dttDestdivEx.ColumnChanged, AddressOf DesgAfterColUpdate

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Sub DesgSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database

      dttDestdivEx.Columns("codditt").DefaultValue = strDittaCorrente
      dttDestdivEx.Columns("dd_conto").DefaultValue = NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto)
      dttDestdivEx.Columns("dd_nomdest").DefaultValue = " "
      dttDestdivEx.Columns("dd_usaem").DefaultValue = "S"
      dttDestdivEx.Columns("xx_lestatus").DefaultValue = NTSCStr(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_status)
      dttDestdivEx.Columns("dd_listino").DefaultValue = "-9999"
      dttDestdivEx.Columns("xx_listino").DefaultValue = oApp.Tr(Me, 130408341386950817, "Usa il listino indicato nell'anagrafica clienti")

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

  Public Overridable Sub DesgNuovo()
    Try
      DesgSetDefaultValue()

      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dttDestdivEx.Rows.Add(dttDestdivEx.NewRow)
      If lCodDestNew <> 0 Then
        dttDestdivEx.Rows(dttDestdivEx.Rows.Count - 1)!dd_coddest = lCodDestNew
      End If

      bHasChanges = True
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

  Public Overridable Function DesgRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dttDestdivEx.Select(strFilter)(nRow).RejectChanges()
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function DesgTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dttDestdivEx.Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!dd_coddest) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128275056739278000, "Il codice destinazione è obbligatorio")))
          Return False
        End If

        If NTSCStr(dtrTmp(i)!dd_nomdest).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128272475736600000, "Il campo 'Ragione sociale (1a parte)' è obbligatorio")))
          Return False
        End If

        dtrTmp(i)!xx_modificato = "S"
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

  Public Overridable Function DesgSalva(ByVal bDelete As Boolean) As Boolean
    Dim dtrTmp() As DataRow = Nothing
    Dim dtrTmp1() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not DesgTestPreSalva() Then Return False
      End If

      If bDelete Then
        If strGestAnaext.IndexOf(IIf(strTipoConto = "C", "D", "E").ToString) > -1 Then
          'devo cancellare da anaext le destinazioni cancellate
          For i = 0 To dttDestdivDeleted.Rows.Count - 1
            dtrTmp1 = dsShared.Tables("ANAEXTDD").Select("ax_coddest = " & dttDestdivDeleted.Rows(i)!dd_coddest.ToString)
            If dtrTmp1.Length > 0 Then dtrTmp1(0).Delete()
          Next
          dsShared.Tables("ANAEXTDD").AcceptChanges()
        End If

      Else
        If strGestAnaext.IndexOf(IIf(strTipoConto = "C", "D", "E").ToString) > -1 Then
          dtrTmp1 = dttDestdivEx.Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
          dtrTmp = dsShared.Tables("ANAEXTDD").Select(" ax_coddest = " & NTSCInt(dtrTmp1(0)!dd_coddest.ToString()))
          If dtrTmp.Length > 0 Then
            'devo aggiornare: non devo fare nulla
          Else
            'è una nuova destinazione: devo integrare anaext
            dsShared.Tables("ANAEXTDD").Rows.Add(dsShared.Tables("ANAEXTDD").NewRow)
            With dsShared.Tables("ANAEXTDD").Rows(dsShared.Tables("ANAEXTDD").Rows.Count - 1)
              !codditt = strDittaCorrente
              !ax_conto = NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto)
              !ax_coddest = NTSCInt(dtrTmp1(0)!dd_coddest.ToString())
              !ax_codlead = 0
              !ax_tipork = IIf(strTipoConto = "C", "D", "E").ToString
              !ax_codart = " "
              !ax_matric = " "
            End With
          End If
          dsShared.Tables("ANAEXTDD").AcceptChanges()
        End If
      End If

      dttDestdivEx.AcceptChanges()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DesgTestPreCancella(ByVal lConto As Integer, ByVal lCoddest As Integer) As Boolean
    Dim strMsg As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCldClie.GetSettingBus("BS--CLIE", "OPZIONI", ".", "DestdivDeleteNoCheck", "0", " ", "0") <> "-1" Then  'non documentare. in vb6 lo faceva fare, ma in NET vogliamo evitare cose del genere ...
        If Not oCldClie.IsDestdivDeletable(strDittaCorrente, lConto, lCoddest, strMsg) Then
          ThrowRemoteEvent(New NTSEventArgs("", strMsg))
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
      Return False
    End Try
  End Function

  Public Overridable Sub DesgBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DesgBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub DesgBeforeColUpdate_dd_coddest(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Dim strErrore As String = ""

    Try
      If lCodDestNew = 0 Then
        If NTSCInt(e.ProposedValue) <> 0 And _
          (NTSCInt(e.ProposedValue) = lDestdomf Or NTSCInt(e.ProposedValue) = lDestsedel Or _
           NTSCInt(e.ProposedValue) = lDestresan Or NTSCInt(e.ProposedValue) = lDestcorr) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128275088911212000, "Codice destinazione riservato: indicare un nuovo codice")))
          Return
        End If
      End If

      If dttDestdivEx.Rows.Count > 1 Then
        dtrTmp = dttDestdivEx.Select("dd_coddest = " & e.ProposedValue.ToString())
        If dtrTmp.Length > 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128275060700586000, "Codice destinazione già esistente: inserire un nuovo codice")))
          Return
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

  Public Overridable Sub DesgBeforeColUpdate_dd_codfis(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
      End If

      If e.ProposedValue.ToString <> "" Then
        If oApp.ValutaCorrente.ToUpper = "EUR" Then
          If oApp.CheckCfpi(3, e.ProposedValue.ToString) = False Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074963870000, "Codice fiscale non corretto")))
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
  End Sub

  Public Overridable Sub DesgBeforeColUpdate_dd_pariva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
      End If

      If e.ProposedValue.ToString <> "" Then
        If oApp.ValutaCorrente.ToUpper = "EUR" Then
          If oApp.CheckCfpi(2, e.ProposedValue.ToString) = False Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074993354000, "Partita IVA non corretta")))
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
  End Sub

  Public Overridable Sub DesgBeforeColUpdate_dd_codcomu(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_codcomu = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If Not oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "COMUNI", "S", strTmp, dttTmp) Then
          e.ProposedValue = e.Row!dd_codcomu.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738074887898000, "Codice comune inesistente")))
        Else
          e.Row!xx_codcomu = strTmp
          e.Row!dd_capdest = NTSCStr(dttTmp.Rows(0)!co_cap)
          e.Row!dd_locdest = NTSCStr(dttTmp.Rows(0)!co_denom)
          e.Row!dd_prodest = NTSCStr(dttTmp.Rows(0)!co_prov)
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

  Public Overridable Sub DesgBeforeColUpdate_dd_stato(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = " " Then e.ProposedValue = ""
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_stato = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.Row!xx_stato = strTmp
        Else
          e.Row!xx_stato = ""
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

  Public Overridable Sub DesgBeforeColUpdate_dd_vett(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_vett = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABVETT", "S", strTmp) Then
          e.Row!xx_vett = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128376964428834000, "Codice vettore inesistente")))
          e.ProposedValue = NTSCStr(e.Row!dd_vett)
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

  Public Overridable Sub DesgBeforeColUpdate_dd_vett2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_vett2 = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABVETT", "S", strTmp) Then
          e.Row!xx_vett2 = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738075547698000, "Codice vettore 2 inesistente")))
          e.ProposedValue = NTSCStr(e.Row!dd_vett2)
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

  Public Overridable Sub DesgBeforeColUpdate_dd_codzona(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codzona = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABZONE", "S", strTmp) Then
          e.Row!xx_codzona = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738075604794000, "Codice zona inesistente")))
          e.ProposedValue = NTSCStr(e.Row!dd_codzona)
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

  Public Overridable Sub DesgBeforeColUpdate_dd_agente(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_agente = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCAGE", "S", strTmp) Then
          e.Row!xx_agente = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128376963888762000, "Codice agente inesistente")))
          e.ProposedValue = e.Row!dd_agente
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

  Public Overridable Sub DesgBeforeColUpdate_dd_agente2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_agente2 = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCAGE", "S", strTmp) Then
          e.Row!xx_agente2 = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738075262250000, "Codice agente2 inesistente")))
          e.ProposedValue = e.Row!dd_agente2
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

  Public Overridable Sub DesgBeforeColUpdate_dd_listino(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      Select Case e.ProposedValue.ToString
        Case "0"
          e.Row!xx_listino = ""
        Case "-9999"
          e.Row!xx_listino = oApp.Tr(Me, 130408330067122829, "Usa il listino dell'anagrafica clienti")
        Case Else
          If NTSCInt(e.ProposedValue) < -9 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130408331825408906, "Listino non valido")))
            e.ProposedValue = e.Row!dd_listino
            Return
          End If
          If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABLIST", "N", strTmp) Then
            e.Row!xx_listino = strTmp
          Else
            'ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381626009522000, "Codice listino inesistente")))
            e.Row!xx_listino = ""
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

  Public Overridable Sub DesgBeforeColUpdate_dd_porto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_porto = ""
      Else
        If e.ProposedValue.ToString.ToUpper <> e.ProposedValue.ToString Then e.ProposedValue = e.ProposedValue.ToString.ToUpper
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABPORT", "S", strTmp) Then
          e.Row!xx_porto = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130695054181924332, "Codice porto inesistente")))
          e.ProposedValue = e.Row!dd_porto
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

  Public Overridable Sub DesgAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DesgAfterColUpdate_" & e.Column.ColumnName.ToLower
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



  Public Overridable Function DesgAgganciaLead(ByVal lCodlead As Integer, ByRef dtrD As DataRow) As Boolean
    '---------------------------------
    'dato un codice lead verifico se posso e nel caso importo i dati anagrafici dal lead
    Dim i As Integer = 0
    Dim dttTmp As New DataTable
    Dim dsTmp As New DataSet
    Dim dtrT() As DataRow = Nothing
    Try

      If lCodlead = 0 Then Return True

      For i = 0 To dsShared.Tables("DESTDIV").Rows.Count - 1
        If NTSCInt(dsShared.Tables("DESTDIV").Rows(i)!dd_codlead) = lCodlead Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738075692466000, _
                          "Il codice lead selezionato risulta essere già collegato alla destinazione " & _
                          "diversa numero |" & NTSCInt(dsShared.Tables("DESTDIV").Rows(i)!dd_coddest).ToString & _
                          "|. Operazione annullata")))
          Return False
        End If
      Next

      For i = 0 To dttDestdivEx.Rows.Count - 1
        If NTSCInt(dttDestdivEx.Rows(i)!dd_codlead) = lCodlead Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738075741294000, _
                          "Il codice lead selezionato risulta essere già collegato alla destinazione " & _
                          "diversa numero |" & NTSCInt(dttDestdivEx.Rows(i)!dd_coddest).ToString & _
                          "|. Operazione annullata")))
          Return False
        End If
      Next

      oCldClie.ValCodiceDb(lCodlead.ToString, strDittaCorrente, "LEADS", "N", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128377018812671000, "Codice LEAD inesistente. Operazione annullata")))
        Return False
      End If

      dtrD!dd_codlead = lCodlead
      With dttTmp.Rows(0)
        dtrD!dd_nomdest = NTSCStr(!le_descr1)
        dtrD!dd_inddest = NTSCStr(!le_indir)
        dtrD!dd_capdest = NTSCStr(!le_cap)
        dtrD!dd_locdest = NTSCStr(!le_citta)
        dtrD!dd_prodest = NTSCStr(!le_prov)
        dtrD!dd_telef = Left(Trim(NTSCStr(!le_telef)), 15)
        dtrD!dd_codzona = NTSCInt(!le_zona)
        dtrD!dd_codfis = NTSCStr(!le_codfis)
        dtrD!dd_pariva = NTSCStr(!le_pariva)
        dtrD!dd_faxtlx = NTSCStr(!le_faxtlx)
        dtrD!dd_agente = NTSCInt(!le_agente)
        dtrD!dd_agente2 = NTSCInt(!le_agente2)
        dtrD!dd_email = NTSCStr(!le_email)
        dtrD!dd_usaem = NTSCStr(!le_usaem)
        dtrD!dd_nomdest2 = NTSCStr(!le_descr2)
        dtrD!dd_stato = NTSCStr(!le_stato)
        dtrD!dd_note = NTSCStr(!le_note2)
      End With
      dttTmp.Clear()

      '----------------------------
      'eredito anaext di destdiv (solo se gestisco sia lead che destdiv ad anaext)
      If strGestAnaext.IndexOf(IIf(strTipoConto = "C", "D", "E").ToString) > -1 And strGestAnaext.IndexOf("L") > -1 Then
        oCldClie.GetDataAnaext(strDittaCorrente, 0, 0, lCodlead, "L", dsTmp)
        If dsTmp.Tables("ANAEXT").Rows.Count > 0 Then
          'rimuovo il vecchio anaext
          dtrT = dsShared.Tables("ANAEXTDD").Select("ax_coddest = " & dtrD!dd_coddest.ToString)
          dtrT(0).Delete()
          'inserisco in anaext la riga del lead
          dsTmp.Tables("ANAEXT").Rows(0)!ax_codlead = 0
          dsTmp.Tables("ANAEXT").Rows(0)!ax_tipork = IIf(strTipoConto = "C", "D", "E").ToString
          dsTmp.Tables("ANAEXT").Rows(0)!ax_conto = NTSCInt(dtrD!dd_conto)
          dsTmp.Tables("ANAEXT").Rows(0)!ax_coddest = NTSCInt(dtrD!dd_coddest)
          dsShared.Tables("ANAEXTDD").ImportRow(dsTmp.Tables("ANAEXT").Rows(0))
          dsTmp.Clear()
          dsShared.Tables("ANAEXT").AcceptChanges()
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

  Public Overridable Function RiempiTTDESTDIVPerStampa(ByRef dttTmp As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldClie.RiempiTTDESTDIVPerStampa(strDittaCorrente, lIITtdestdiv, dttTmp)
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

#Region "FUNZIONI PER FRM__NUOV"
  Public Overridable Function TestAnagen(ByVal lCodAnag As Integer, ByRef strDesanag As String, ByRef lProgr As Integer) As Boolean
    '-------------------------------------
    'controllo se il mastro passatomi esiste: se si ritorno anche la descrizione
    Dim dttTmp As New DataTable
    Try
      strDesanag = ""
      lProgr = -1

      oCldClie.ValCodiceDb(lCodAnag.ToString, "", "ANAGEN", "N", strDesanag, dttTmp)
      If dttTmp.Rows.Count = 0 Then Return False

      If dttAnaz.Rows(0)!tb_flriccf.ToString <> "S" Then
        If lCodAnag.ToString.Length > lContoProgrMoltip.ToString.Length - 1 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128377731956292000, _
                        "Il codice Anagrafica Generale selezionato ha un numero di cifre superiore" & vbCrLf & _
                        "a quello consentito per il Piano dei Conti corrente." & vbCrLf & _
                        "Pertanto sarà preso solo il numero di cifre utile.")))
          lCodAnag = NTSCInt(Right(lCodAnag.ToString, lContoProgrMoltip.ToString.Length))
        Else
          lProgr = lCodAnag
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

  Public Overridable Function TestLead(ByVal lCodlead As Integer, ByRef strDeslead As String) As Boolean
    '-------------------------------------
    'controllo se il mastro passatomi esiste: se si ritorno anche la descrizione
    Dim dttTmp As New DataTable
    Try
      strDeslead = ""
      oCldClie.ValCodiceDb(lCodlead.ToString, strDittaCorrente, "LEADS", "N", strDeslead, dttTmp)
      If dttTmp.Rows.Count = 0 Then Return False

      If NTSCInt(dttTmp.Rows(0)!le_conto) <> 0 Then   'solo lead non ancora collegati a clienti
        dttTmp.Clear()
        Return False
      End If
      dttTmp.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestMastro(ByVal lCodMast As Integer, ByRef strDesmast As String, _
                                         ByVal strCodPcon As String, ByVal strTipoConto As String, _
                                         ByRef lProgr As Integer) As Boolean
    '-------------------------------------
    'controllo se il mastro passatomi esiste: se si ritorno anche la descrizione
    Dim dttTmp As New DataTable
    Try
      strDesmast = ""
      lProgr = -1
      oCldClie.ValCodiceDb(lCodMast.ToString, "", "TABMAST", "N", strDesmast, dttTmp, strCodPcon)
      If dttTmp.Rows.Count = 0 Then Return False
      If dttTmp.Rows(0)!tb_tipomast.ToString <> strTipoConto Then Return False

      If dttAnaz.Rows(0)!tb_flriccf.ToString = "S" Then
        Select Case strTipoConto
          Case "C" : lProgr = oCldClie.LegNuma(strDittaCorrente, "CC", "", lCodMast, True)
          Case "F" : lProgr = oCldClie.LegNuma(strDittaCorrente, "FF", "", lCodMast, True)
        End Select
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

#Region "Funzioni per CLITIPB"
  Public Overridable Sub TipbSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dsShared.Tables("CLITIPB").Columns("codditt").DefaultValue = strDittaCorrente
      dsShared.Tables("CLITIPB").Columns("ctp_conto").DefaultValue = NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto)

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


  Public Overridable Sub TipbNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("CLITIPB").Rows.Add(dsShared.Tables("CLITIPB").NewRow)

      bHasChanges = True
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

  Public Overridable Function TipbRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("CLITIPB").Select(strFilter)(nRow).RejectChanges()
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function TipbTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("CLITIPB").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!ctp_codtpbf) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738075813124000, "Il campo tipo bolla/fattura deve contenere un valore compreso tra 1 e 9999")))
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

  Public Overridable Function TipbSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TipbTestPreSalva() Then Return False
      End If

      dsShared.Tables("CLITIPB").AcceptChanges()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Sub TipbBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValueTipb = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "TipbBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub TipbBeforeColUpdate_ctp_codtpbf(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dtrTmp() As DataRow
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codtpbf = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strTmp) Then
          If dsShared.Tables("CLITIPB").Rows.Count > 1 Then
            dtrTmp = dsShared.Tables("CLITIPB").Select("ctp_codtpbf = " & e.ProposedValue.ToString())
            If dtrTmp.Length > 0 Then
              e.ProposedValue = e.Row!ctp_codtpbf
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128387321428390000, "Tipo bolla/fattura già esistente: inserire un nuovo codice")))
              Return
            End If
          End If
          e.Row!xx_codtpbf = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128387319754510000, "Tipo bolla/fattura inesistente")))
          e.ProposedValue = e.Row!ctp_codtpbf
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

  Public Overridable Sub TipbBeforeColUpdate_ctp_codpaga(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codpaga = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABPAGA", "N", strTmp) Then
          e.Row!xx_codpaga = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738075396394000, "Codice pagamento inesistente")))
          e.ProposedValue = e.Row!ctp_codpaga
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


  Public Overridable Sub TipbAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strPrevCelValueTipb.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPrevCelValueTipb = strPrevCelValueTipb.Remove(strPrevCelValueTipb.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "TipbAfterColUpdate_" & e.Column.ColumnName.ToLower
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
#End Region

#Region "Funzioni per TIRK"
  Public Overridable Sub TiprkSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dsShared.Tables("TRKTPBF").Columns("codditt").DefaultValue = strDittaCorrente
      dsShared.Tables("TRKTPBF").Columns("tkt_conto").DefaultValue = NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto)
      dsShared.Tables("TRKTPBF").Columns("tkt_listino").DefaultValue = 0

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


  Public Overridable Sub TiprkNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("TRKTPBF").Rows.Add(dsShared.Tables("TRKTPBF").NewRow)

      bHasChanges = True
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

  Public Overridable Function TiprkRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("TRKTPBF").Select(strFilter)(nRow).RejectChanges()
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function TiprkTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("TRKTPBF").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!tkt_tipobf) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129738664989435735, "Il campo tipo bolla/fattura deve contenere un valore compreso tra 1 e 9999")))
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

  Public Overridable Function TiprkSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TiprkTestPreSalva() Then Return False
      End If

      dsShared.Tables("TRKTPBF").AcceptChanges()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Sub TiprkBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValueTiprk = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "TiprkBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub TiprkBeforeColUpdate_tkt_tipork(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dtrTmp() As DataRow
    Try
      dtrTmp = dsShared.Tables("TRKTPBF").Select("tkt_tipork = " & CStrSQL(e.ProposedValue))

      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row!tkt_tipork
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129738665422642114, "Tipo documento già esistente: inserire un nuovo codice")))
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

  Public Overridable Sub TiprkBeforeColUpdate_tkt_tipobf(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codtpbf = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strTmp) Then
          e.Row!xx_codtpbf = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129738733527940893, "Tipo bolla/fattura inesistente")))
          e.ProposedValue = e.Row!tkt_tipobf
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

  Public Overridable Sub TiprkAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strPrevCelValueTiprk.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPrevCelValueTiprk = strPrevCelValueTiprk.Remove(strPrevCelValueTiprk.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "TiprkAfterColUpdate_" & e.Column.ColumnName.ToLower
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
#End Region

#Region "Funzioni per CLIBANC"
  Public Overridable Sub BancSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dsShared.Tables("CLIBANC").Columns("codditt").DefaultValue = strDittaCorrente
      dsShared.Tables("CLIBANC").Columns("cba_conto").DefaultValue = NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto)
      dsShared.Tables("CLIBANC").Columns("cba_ultagg").DefaultValue = NTSCDate(DateTime.Now.ToShortDateString)

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


  Public Overridable Sub BancNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("CLIBANC").Rows.Add(dsShared.Tables("CLIBANC").NewRow)

      bHasChanges = True
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

  Public Overridable Function BancRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("CLIBANC").Select(strFilter)(nRow).RejectChanges()
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function BancTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim oDttgr As New CLEGROUPBY
    Dim dttGr As New DataTable

    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("CLIBANC").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!cba_abi) = 0 Or NTSCInt(dtrTmp(i)!cba_cab) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128387320580842000, "I campi ABI e CAB deve contenere un valore diverso da 0")))
          Return False
        End If
      Next

      'test su univocità record
      oDttgr.NTSGroupBy(dsShared.Tables("CLIBANC"), dttGr, "count(cba_abi) as NREC", "", "cba_abi, cba_cab, cba_rifriba")
      For i = 0 To dttGr.Rows.Count - 1
        If NTSCInt(dttGr.Rows(i)!NREC) > 1 Then
          dttGr.Clear()
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128387343871454000, "Nella griglia sono presenti più volte righe con stesso codice ABI, CAB e C/C")))
          Return False
        End If
      Next
      dttGr.Clear()

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

  Public Overridable Function BancSalva(ByVal bDelete As Boolean) As Boolean
    Dim i As Integer = 0
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not BancTestPreSalva() Then Return False
      End If

      dsShared.Tables("CLIBANC").AcceptChanges()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Sub BancBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValueBanc = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BancBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub BancBeforeColUpdate_cba_abi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) <> 0 Then
        If Not oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ABI", "N", strTmp) Then
          e.ProposedValue = 0
          e.Row!cba_cab = 0
          e.Row!cba_swift = 0
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738075070020000, "Codice abi non corretto")))
        Else
          e.Row!xx_abi = strTmp
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

  Public Overridable Sub BancBeforeColUpdate_cba_cab(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(e.ProposedValue) <> 0 Then
        If Not oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "CAB", "N", strTmp, dttTmp, e.Row!cba_abi.ToString) Then
          e.ProposedValue = 0
          e.Row!cba_swift = 0
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738075130236000, "Codice cab non corretto")))
        Else
          e.Row!xx_cab = strTmp
          e.Row!cba_swift = NTSCStr(dttTmp.Rows(0)!abcswift)
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
    Finally
      dttTmp.Clear()
    End Try
  End Sub

  Public Overridable Sub BancBeforeColUpdate_cba_codvalu(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_codvalu = ""
      Else
        If oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABVALU", "N", strTmp) Then
          e.Row!xx_codvalu = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128381606849242000, "Codice valuta inesistente")))
          e.ProposedValue = e.Row!cba_codvalu
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



  Public Overridable Sub BancAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strPrevCelValueBanc.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPrevCelValueBanc = strPrevCelValueBanc.Remove(strPrevCelValueBanc.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "BancAfterColUpdate_" & e.Column.ColumnName.ToLower
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
#End Region

#Region "Funzioni per CLISTPG"
  Public Overridable Sub StpgSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dsShared.Tables("CLISTPG").Columns("codditt").DefaultValue = strDittaCorrente
      dsShared.Tables("CLISTPG").Columns("cts_conto").DefaultValue = NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto)
      dsShared.Tables("CLISTPG").Columns("cts_ggritmed").DefaultValue = 0

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


  Public Overridable Sub StpgNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("CLISTPG").Rows.Add(dsShared.Tables("CLISTPG").NewRow)

      bHasChanges = True
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

  Public Overridable Function StpgRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("CLISTPG").Select(strFilter)(nRow).RejectChanges()
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function StpgTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim oDttgr As New CLEGROUPBY
    Dim dttGr As New DataTable

    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("CLISTPG").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!cts_codstpg) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128387320580842001, "Il campo codice sottotipo pagamento deve contenere un valore diverso da 0")))
          Return False
        End If
      Next

      'test su univocità record
      oDttgr.NTSGroupBy(dsShared.Tables("CLISTPG"), dttGr, "count(cts_codstpg) as NREC", "", "cts_codstpg")
      For i = 0 To dttGr.Rows.Count - 1
        If NTSCInt(dttGr.Rows(i)!NREC) > 1 Then
          dttGr.Clear()
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128387343871454001, "Nella griglia sono presenti più volte righe con stesso codice sottotipo pagamento")))
          Return False
        End If
      Next
      dttGr.Clear()

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

  Public Overridable Function StpgSalva(ByVal bDelete As Boolean) As Boolean
    Dim i As Integer = 0
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not StpgTestPreSalva() Then Return False
      End If

      dsShared.Tables("CLISTPG").AcceptChanges()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Sub StpgBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValueStpg = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "StpgBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub StpgBeforeColUpdate_cts_codstpg(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) <> 0 Then
        If Not oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTPG", "N", strTmp) Then
          e.ProposedValue = e.Row!cts_codstpg
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128738075070020001, "Codice sottotipo pagamento non corretto")))
        Else
          e.Row!xx_desstpg = strTmp
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

  Public Overridable Sub StpgAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strPrevCelValueStpg.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPrevCelValueStpg = strPrevCelValueStpg.Remove(strPrevCelValueStpg.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "StpgAfterColUpdate_" & e.Column.ColumnName.ToLower
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
#End Region

#Region "Funzioni per FRM_SIMU"
  Public Overridable Function PrimoMagazzino() As Integer
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldClie.PrimoMagazzino(strDittaCorrente)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function ProgressiviMagazzino(ByVal strCodart As String, ByVal nMagaz As Integer, _
    ByVal nFase As Integer, ByRef dttTmp As DataTable) As Boolean

    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldClie.ProgressiviMagazzino(strDittaCorrente, strCodart, nMagaz, nFase, dttTmp)
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

  Public Overridable Function SimulaVendita(ByVal lConto As Integer, ByVal strCodart As String, _
                                          ByVal nListino As Integer, ByVal dQuant As Decimal, ByVal nAnClascon As Integer, _
                                          ByRef dPrezzo As Decimal, _
                                          ByRef dSconto1 As Decimal, ByRef dSconto2 As Decimal, ByRef dSconto3 As Decimal, _
                                          ByRef dSconto4 As Decimal, ByRef dSconto5 As Decimal, ByRef dSconto6 As Decimal, _
                                          ByRef strTipovalListOut As String, ByRef strTipovalScontOut As String) As Boolean
    Try
      'obsoleta
      Return SimulaVendita(lConto, strCodart, nListino, dQuant, nAnClascon, dPrezzo, _
                           dSconto1, dSconto2, dSconto3, dSconto4, dSconto5, dSconto6, _
                           strTipovalListOut, strTipovalScontOut, 0)


    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function SimulaVendita(ByVal lConto As Integer, ByVal strCodart As String, _
                                            ByVal nListino As Integer, ByVal dQuant As Decimal, ByVal nAnClascon As Integer, _
                                            ByRef dPrezzo As Decimal, _
                                            ByRef dSconto1 As Decimal, ByRef dSconto2 As Decimal, ByRef dSconto3 As Decimal, _
                                            ByRef dSconto4 As Decimal, ByRef dSconto5 As Decimal, ByRef dSconto6 As Decimal, _
                                            ByRef strTipovalListOut As String, ByRef strTipovalScontOut As String, _
                                            ByVal lCoddest As Integer) As Boolean
    Dim bGestionePrezzi As Boolean = CBool(oCldClie.GetSettingBusDitt(strDittaCorrente, "OPZIONI", ".", ".", "AbilitaPrezzoUM", "0", " ", "0"))
    Dim bModLEX As Boolean = False
    Dim lModuliExtDittaDitt As Integer = ModuliExtDittaDitt(strDittaCorrente)
    Dim nFase As Integer = 0
    Dim nPromo As Integer = 0
    Dim nPrperqta As Integer = 0
    Dim nArClascon As Integer = 0
    Dim dPrelist As Decimal = 0
    Dim dDaQuantOut As Decimal = 0
    Dim dAQuantOut As Decimal = 0
    Dim dPerqtaOut As Decimal = 0
    Dim strUmprz As String = "S"
    Dim strPrzNet As String = ""
    Dim strUnmisOut As String = ""
    Dim dttTmp As New DataTable
    Dim oCondCommerciali As NTSCondCommerciali
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {lConto, strCodart, nListino, dQuant, nAnClascon, dPrezzo, _
                           dSconto1, dSconto2, dSconto3, dSconto4, dSconto5, dSconto6, _
                           strTipovalListOut, strTipovalScontOut, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dPrezzo = NTSCDec(oIn(5))
        dSconto1 = NTSCDec(oIn(6))
        dSconto2 = NTSCDec(oIn(7))
        dSconto3 = NTSCDec(oIn(8))
        dSconto4 = NTSCDec(oIn(9))
        dSconto5 = NTSCDec(oIn(10))
        dSconto6 = NTSCDec(oIn(11))
        strTipovalListOut = NTSCStr(oIn(12))
        strTipovalScontOut = NTSCStr(oIn(13))
        Return CBool(oOut)
      End If
      '----------------


      '--------------------------------------------------------------------------------------------------------------
      If oCldClie.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttTmp) = False Then Return False
      nArClascon = NTSCInt(dttTmp.Rows(0)!ar_clascon)
      '--------------------------------------------------------------------------------------------------------------
      If CType(oCleComm, CLELBMENU).GestioneUMPrezzo(bGestionePrezzi, "BSVEBOLL", _
        IIf(strTipoConto = "C", "B", "M").ToString, False, strCodart, NTSCStr(dttTmp.Rows(0)!ar_unmis), _
        strDittaCorrente) = True Then strUmprz = "S" Else strUmprz = "N"
      '--------------------------------------------------------------------------------------------------------------
      If CBool((lModuliExtDittaDitt And CLN__STD.bsModExtLEX)) Or _
         CBool((lModuliExtDittaDitt And CLN__STD.bsModExtTCP)) Then bModLEX = True
      '--------------------------------------------------------------------------------------------------------------
      If (dttTmp.Rows(0)!ar_gesfasi.ToString = "N") Or (bModLEX = False) Then
        nFase = 0
      Else
        nFase = NTSCInt(dttTmp.Rows(0)!ar_ultfase)
      End If
      '--------------------------------------------------------------------------------------------------------------
      oCondCommerciali = IstanziaNTSCondCommerciali()
      '--------------------------------------------------------------------------------------------------------------
      oCondCommerciali.bCalcolaPrezzo = True
      oCondCommerciali.bCalcolaSconti = True
      With oCondCommerciali.Input
        .strDitta = strDittaCorrente
        .strCodart = strCodart
        .lConto = lConto
        .lDestdiv = lCoddest
        .nListino = nListino
        .bGestionePrezzi = bGestionePrezzi
        .strUnmis = ""
        .strUmp = NTSCStr(dttTmp.Rows(0)!ar_unmis)
        .nFase = nFase
        .strTipoval = "P"
        .bConspromo = True
        .dtDatdoc = Now
        .dColli = 0
        .dQuant = dQuant
        .bConsideraPrezziUnmis = True
        .bPrezziPerUnmis = (strUmprz = "S")

        .nClscar = nArClascon
        .nClscan = nAnClascon
      End With

      CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)

      With oCondCommerciali.OutputPrezzo
        dPrezzo = .dPrezzo
        dPrelist = .dPrelist
        nPromo = .nCodpromo
        strPrzNet = .strPrzNet
        nPrperqta = .nPerqta
        dDaQuantOut = .dDaQuant
        dAQuantOut = .dAquant
        dPerqtaOut = .dPerqta
        strUnmisOut = .strUnmis
        strTipovalListOut = .strTipoval
      End With

      With oCondCommerciali.OutputSconti
        dSconto1 = .dSconto1
        dSconto2 = .dSconto2
        dSconto3 = .dSconto3
        dSconto4 = .dSconto4
        dSconto5 = .dSconto5
        dSconto6 = .dSconto6
        strTipovalScontOut = .strTipoval
      End With

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

  Public Overridable Function IstanziaNTSCondCommerciali() As NTSCondCommerciali
    Try
      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BE__CLIE", "BN__STD.NTSCondCommerciali", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 127791222114531250, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return Nothing
      End If
      Return CType(oTmp, NTSCondCommerciali)
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
#End Region

#Region "Funzioni per FRM__ANCA"
  Public Overridable Sub AncaSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dsShared.Tables("ANACONA").Columns("codditt").DefaultValue = strDittaCorrente
      dsShared.Tables("ANACONA").Columns("acn_conto").DefaultValue = NTSCInt(dsShared.Tables("ANAGRA").Rows(nCurRow)!an_conto)

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


  Public Overridable Sub AncaNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("ANACONA").Rows.Add(dsShared.Tables("ANACONA").NewRow)

      bHasChanges = True
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

  Public Overridable Function AncaRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("ANACONA").Select(strFilter)(nRow).RejectChanges()
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function AncaTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("ANACONA").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!acn_sotgru) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130695191410953105, "Il sottogruppo merceologico è un campo obbligatorio. Inserirlo prima di salvare")))
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

  Public Overridable Function AncaSalva(ByVal bDelete As Boolean) As Boolean
    Dim i As Integer = 0
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not AncaTestPreSalva() Then Return False
      End If

      dsShared.Tables("ANACONA").AcceptChanges()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Sub AncaBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValueBanc = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AncaBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub AncaBeforeColUpdate_acn_sotgru(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_sotgru = ""
      Else
        If Not oCldClie.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSGME", "N", strTmp) Then
          e.ProposedValue = 0
          e.Row!xx_sotgru = ""
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130695206995845148, "Sottogruppo merceologico non corretto")))
        Else
          e.Row!xx_sotgru = strTmp
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

  Public Overridable Sub AncaAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strPrevCelValueTipb.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPrevCelValueTipb = strPrevCelValueTipb.Remove(strPrevCelValueTipb.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AncaAfterColUpdate_" & e.Column.ColumnName.ToLower
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
#End Region
End Class
