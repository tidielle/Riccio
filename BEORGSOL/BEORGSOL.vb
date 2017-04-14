Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEORGSOL
  Inherits CLEMGDOCU

  'testord serve per fare in modo che ordlist venga gestito tipo testord/movord
  'quando cambio un campo di ordlist che dovrebbe essere i ntestord lo memorizzo anche in TESTA, così le routine di BEMGDOCU funzionano regolarmente
  'ad ogni cambio di riga di ordlist memorizzo in testa i dati di ordlist su cui entro

  Private Moduli_P As Integer = bsModOR
  Private ModuliExt_P As Integer = bsModExtORE
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

  Public oCldGsol As CLDORGSOL
  Public strTipork As String = ""
  Public bSaltaValidazione As Boolean = False         'se true il conto non deve venir validato ... impostato da zoom fornitori
  Public nFirstMagaz As Integer = 0
  Public bRiscriviImpegni As Boolean = False          'se true salvando la riga devono essere risalvati anche gli impegni e lavorazioni relative
  Public bCambioFornitorePrezzi As Boolean = False
  Public nCodStabilimento As Integer = 0
  Public bArtFasiGetFornFromCicli As Boolean = False

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDORGSOL"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldGsol = CType(MyBase.ocldBase, CLDORGSOL)
    oCldGsol.Init(oApp)

    BORDINI = True            'avviso BEMGDOCU che non sono documenti di magazzino
    BGESTORDLIST = True       'avviso BEMGDOCU che le tabelle devono gestire ordlist, non testord/movord (dal tipork non si rileva la differenza ...)

    Return True
  End Function

  Public Overridable Function IstanziaNTSCondCommerciali() As NTSCondCommerciali
    Try
      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEORGSOL", "BN__STD.NTSCondCommerciali", oTmp, strErr, False, "", "") = False Then
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

  Public Overridable Function LeggiRegistro() As Boolean
    Try
      gbUsaFiltroCommessa = CBool(oCldGsol.GetSettingBus("Bsorgsol", "Opzioni", ".", "UsaFiltroCommessa", "-1", " ", "-1")) 'Legge la distinta base ignora il filtro commessa (default -1=applica filtro)
      gbUsaFiltroPadre = CBool(oCldGsol.GetSettingBus("Bsorgsol", "Opzioni", ".", "UsaFiltroPadre", "-1", " ", "-1")) 'Legge la distinta base ignora il filtro art. padre (default -1=applica filtro)
      gstrTipoArtConf = (oCldGsol.GetSettingBus("Bscpcopr", "Opzioni", ".", "TipoArtConf", "0", " ", "0"))
      gbRiportaFasiFantasmi = CBool(Val(oCldGsol.GetSettingBus("Bsorgsol", "Opzioni", ".", "RiportaFasiFantasmi", "0", " ", "0"))) 'se impostata a -1 espodendo la distinta riporta le fasi anche dei fantasmi
      strTipoListinoMat = oCldGsol.GetSettingBus("Bsveboll", "Opzioni", ".", "Tipo_list_costi_mat_carichi", " ", " ", " ") 'blank,1,2,3
      strTipValSemPf = oCldGsol.GetSettingBus("Bsveboll", "Opzioni", ".", "Tipo_valorizz_sem_pf", " ", " ", " ")
      bIgnoraMagDistintaBase = CBool(Val(oCldGsol.GetSettingBus("Bsorgsol", "Opzioni", ".", "IgnoraMagDistintaBase", "0", " ", "0"))) '-1 o 0 (abilitato lo stesso comportamento di BSORGSOR/BSVEBOLL)
      bEsplodiD = Not (CBool(oCldGsol.GetSettingBus("Bsorgsol", "Opzioni", ".", "No_Esplosione_Articolo_D", "0", " ", "0")))
      '-----------------------------------------------------------------------------------------
      '--- Test per vedere se si vuole attivare la gestione dei prezzi riferiti ad una u.d.m. diversa dalla principale
      '-----------------------------------------------------------------------------------------
      bGestionePrezzi = CBool(Val(oCldGsol.GetSettingBus("OPZIONI", ".", ".", "AbilitaPrezzoUM", "0", " ", "0")))
      '-----------------------------------------------------------------------------------------
      bNoTempiSuTerzisti = CBool(Val(oCldGsol.GetSettingBus("Bsorgsol", "OPZIONI", ".", "NoTempiSuTerzisti", "0", " ", "0"))) 'opzione di registro che se abilitata quando scrive la lavorazione terzista non inserisce il tempo di esecuzione (giorni x 8 ore) ma inserisce zero.
      nCodStabilimento = NTSCInt(oCldGsol.GetSettingBus("BSDBEMRP", "OPZIONIUT", ".", "CodStabilimentoPianificazione", "0", " ", "0"))
      bArtFasiGetFornFromCicli = CBool(Val(oCldGsol.GetSettingBus("Bsorgsol", "OPZIONI", ".", "ArtFasiGetFornFromCicli", "0", " ", "0"))) 'se = TRUE, per aticoli a fasi la cui fase non è = a ultima fase di artico cerca di leggere il fornitoreed il magazzino di produzione dalla DB

      bGestioneAbbinamentiTaglie = CBool(oCldGsol.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "Gestione_Abbinamenti_Taglie", "0", " ", "0"))

      nListinoCalcoloRicaricoMargine = NTSCInt(oCldGsol.GetSettingBusDitt(strDittaCorrente, "Bsorgsol", "OPZIONI", ".", "ListinoCalcoloRicaricoMargine", "0", ".", "0"))
      If (nListinoCalcoloRicaricoMargine < -2) Or (nListinoCalcoloRicaricoMargine > 9999) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130804005158503185, "Attenzione!" & vbCrLf & _
          "L'opzione di registro:" & vbCrLf & _
          " . 'BSORGSOL/OPZIONI/ListinoCalcoloRicaricoMargine'" & vbCrLf & _
          "è stata impostata con un valore NON valido (|" & nListinoCalcoloRicaricoMargine.ToString & "|)." & vbCrLf & _
          "Valore consentito compreso fra '-2' e '9999'." & vbCrLf & _
          "L'opzione sarà considerata a '0'.")))
        nListinoCalcoloRicaricoMargine = 0
      End If
      bMargineValoreUnitario = CBool(oCldGsol.GetSettingBus("BSORGSOL", "OPZIONI", ".", "MargineValoreUnitario", "0", ".", "0"))

      bForzaLetturaPrezziQta = CBool(oCldGsol.GetSettingBus("BSORGSOL", "OPZIONI", ".", "ForzaLetturaPrezziQta", "0", " ", "0")) '-1=rilegge sempre  prezzi alla variazione della qta
      bForzaLetturaScontiQta = CBool(oCldGsol.GetSettingBus("BSORGSOL", "OPZIONI", ".", "ForzaLetturaScontiQta", "0", " ", "0")) '-1=rilegge sempre gli sconti alla variazione della qta

      If bForzaLetturaPrezziQta Then nPrperqta = CLN__STD.bsPrSetsiqta
      If bForzaLetturaScontiQta Then nScperqta = CLN__STD.bsPrSetsiqta

      'Legge il primo magazzino di merce propria da proporre come predefinito
      nFirstMagaz = oCldGsol.GetFirstMagazMercePropria(strDittaCorrente)
      If nFirstMagaz = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128619656345312500, "Non rilevato alcun magazzino di tipo 'Merce propria' codificato. Impossibile proseguire.")))
        Return False
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
    End Try
  End Function

  Public Overridable Function ApriOrdlist(ByVal strDitta As String, _
                                      ByVal strSQLArtico As String, ByVal lConto As Integer, _
                                      ByVal lCommeca As Integer, ByVal nMagaz As Integer, _
                                      ByVal strDatordini As String, ByVal strDatordfin As String, _
                                      ByVal strDatconsini As String, ByVal strDatconsfin As String, _
                                      ByVal bGenerato As Boolean, ByVal bConfermato As Boolean, _
                                      ByVal bCongelato As Boolean, ByVal bEmRDA As Boolean, _
                                      ByVal bAppRDA As Boolean, ByVal bEmRDO As Boolean, _
                                      ByVal bEmOrdine As Boolean, ByVal lProgr As Integer, _
                                      ByRef ds As DataSet) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strSQLArtico, lConto, lCommeca, nMagaz, _
                                             strDatordini, strDatordfin, strDatconsini, strDatconsfin, _
                                             bGenerato, bConfermato, bCongelato, bEmRDA, bAppRDA, bEmRDO, _
                                             bEmOrdine, lProgr, ds})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(17), DataSet)        'alla funzione è passato ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      Return ApriOrdlist(strDitta, strSQLArtico, lConto, lCommeca, nMagaz, _
                          strDatordini, strDatordfin, strDatconsini, strDatconsfin, _
                          bGenerato, bConfermato, bCongelato, bEmRDA, bAppRDA, bEmRDO, _
                          bEmOrdine, lProgr, ds, 0)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ApriOrdlist(ByVal strDitta As String, _
                                    ByVal strSQLArtico As String, ByVal lConto As Integer, _
                                    ByVal lCommeca As Integer, ByVal nMagaz As Integer, _
                                    ByVal strDatordini As String, ByVal strDatordfin As String, _
                                    ByVal strDatconsini As String, ByVal strDatconsfin As String, _
                                    ByVal bGenerato As Boolean, ByVal bConfermato As Boolean, _
                                    ByVal bCongelato As Boolean, ByVal bEmRDA As Boolean, _
                                    ByVal bAppRDA As Boolean, ByVal bEmRDO As Boolean, _
                                    ByVal bEmOrdine As Boolean, ByVal lProgr As Integer, _
                                    ByRef ds As DataSet, ByVal lOrdinamento As Integer) As Boolean
    Dim dReturn As Boolean = False
    Dim strTmp As String = ""
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldGsol.GetOrdlist(strDitta, strTipork, strSQLArtico, lConto, _
                                    lCommeca, nMagaz, strDatordini, strDatordfin, _
                                    strDatconsini, strDatconsfin, bGenerato, bConfermato, _
                                    bCongelato, bEmRDA, bAppRDA, bEmRDO, bEmOrdine, lProgr, nCodStabilimento, ds, _
                                    lOrdinamento)

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      SetDefaultValueOrdlist(ds)
      SetDefaultValueOrdlistImp(ds)
      If bModTCO Then
        SetDefaultValueOrdlistTC(ds)
        SetDefaultValueOrdlistImpTC(ds)
      End If
      SetDefaultValueZzattivit(ds)
      SetDefaultValueZzassris(ds)

      '--------------------------------------
      'generalizzo i datatable
      'per standardizzare i nomi delle colonne ...
      StandardizzaDatatable(ds, False)

      '--------------------------------------
      'se non c'è il modulo PM ed ho compilato il task-id cerco di decodificarlo da COMWBS
      For Each dtrT As DataRow In ds.Tables("CORPO").Select("ec_pmtaskid <> 0 and ec_commeca <> 0")
        oCldDocu.ValCodiceDb(dtrT!ec_pmtaskid.ToString, strDittaCorrente, "COMWBS", "N", strTmp, Nothing, dtrT!ec_commeca.ToString, dtrT!ec_subcommeca.ToString)
        dtrT!xxo_pmtaskid = strTmp
        dtrT.AcceptChanges()
      Next

      bDocEmesso = False
      bNew = False

      If dReturn = False Then Return False

      If Not dsShared Is Nothing Then
        RemoveHandler dsShared.Tables("CORPO").ColumnChanging, AddressOf BeforeColUpdate_CORPO
        RemoveHandler dsShared.Tables("CORPO").ColumnChanged, AddressOf AfterColUpdate_CORPO
        RemoveHandler dsShared.Tables("CORPOIMP").ColumnChanging, AddressOf BeforeColUpdate_CORPOIMP
        RemoveHandler dsShared.Tables("CORPOIMP").ColumnChanged, AddressOf AfterColUpdate_CORPOIMP
        RemoveHandler dsShared.Tables("ASSRIS").ColumnChanging, AddressOf BeforeColUpdate_ASSRIS
        RemoveHandler dsShared.Tables("ASSRIS").ColumnChanged, AddressOf AfterColUpdate_ASSRIS
        RemoveHandler dsShared.Tables("ATTIVIT").ColumnChanging, AddressOf BeforeColUpdate_ATTIVIT
        RemoveHandler dsShared.Tables("ATTIVIT").ColumnChanged, AddressOf AfterColUpdate_ATTIVIT
        If bModTCO Then
          RemoveHandler dsShared.Tables("CORPOTC").ColumnChanging, AddressOf BeforeColUpdate_CORPOTC
          RemoveHandler dsShared.Tables("CORPOTC").ColumnChanged, AddressOf AfterColUpdate_CORPOTC
          RemoveHandler dsShared.Tables("CORPOIMPTC").ColumnChanging, AddressOf BeforeColUpdate_CORPOIMPTC
          RemoveHandler dsShared.Tables("CORPOIMPTC").ColumnChanged, AddressOf AfterColUpdate_CORPOIMPTC
        End If
      End If

      dsShared = ds
      dttET = dsShared.Tables("TESTA")
      dttEC = dsShared.Tables("CORPO")
      dttECIMP = dsShared.Tables("CORPOIMP")
      dttECTC = dsShared.Tables("CORPOTC")
      dttECIMPTC = dsShared.Tables("CORPOIMPTC")
      dttASSRIS = dsShared.Tables("ASSRIS")
      dttATTIVIT = dsShared.Tables("ATTIVIT")

      AddHandler dsShared.Tables("CORPO").ColumnChanging, AddressOf BeforeColUpdate_CORPO
      AddHandler dsShared.Tables("CORPO").ColumnChanged, AddressOf AfterColUpdate_CORPO
      AddHandler dsShared.Tables("CORPOIMP").ColumnChanging, AddressOf BeforeColUpdate_CORPOIMP
      AddHandler dsShared.Tables("CORPOIMP").ColumnChanged, AddressOf AfterColUpdate_CORPOIMP
      AddHandler dsShared.Tables("ASSRIS").ColumnChanging, AddressOf BeforeColUpdate_ASSRIS
      AddHandler dsShared.Tables("ASSRIS").ColumnChanged, AddressOf AfterColUpdate_ASSRIS
      AddHandler dsShared.Tables("ATTIVIT").ColumnChanging, AddressOf BeforeColUpdate_ATTIVIT
      AddHandler dsShared.Tables("ATTIVIT").ColumnChanged, AddressOf AfterColUpdate_ATTIVIT
      If bModTCO Then
        AddHandler dsShared.Tables("CORPOTC").ColumnChanging, AddressOf BeforeColUpdate_CORPOTC
        AddHandler dsShared.Tables("CORPOTC").ColumnChanged, AddressOf AfterColUpdate_CORPOTC
        AddHandler dsShared.Tables("CORPOIMPTC").ColumnChanging, AddressOf BeforeColUpdate_CORPOIMPTC
        AddHandler dsShared.Tables("CORPOIMPTC").ColumnChanged, AddressOf AfterColUpdate_CORPOIMPTC
      End If

      AggiungiColonneUnbound(dsShared)

      ds.AcceptChanges()        'confermo tutto

      If ds.Tables("CORPO").Rows.Count > 0 Then SettaTesta(ds.Tables("CORPO").Rows(0))

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function StandardizzaDatatable(ByRef ds As DataSet, ByVal bSoloImpegni As Boolean) As Boolean
    '-------------------------------------
    'rendo il datatable esattamente come se lo aspetta BEMGDOCU
    Try
      If Not bSoloImpegni Then
        If Not CambiaPrefissoCampiDatatable(ds.Tables("TESTORD"), "TESTORD", "TESTA", "td_", "et_") Then Return False
        If Not CambiaPrefissoCampiDatatable(ds.Tables("ORDLIST"), "ORDLIST", "CORPO", "ol_", "ec_") Then Return False
      End If

      If Not CambiaPrefissoCampiDatatable(ds.Tables("ORDLISTIMP"), "ORDLISTIMP", "CORPOIMP", "ol_", "ec_") Then Return False

      If Not bSoloImpegni Then If Not CambiaPrefissoCampiDatatable(ds.Tables("ORDLISTTC"), "ORDLISTTC", "CORPOTC", "ol_", "ec_") Then Return False
      If Not CambiaPrefissoCampiDatatable(ds.Tables("ORDLISTIMPTC"), "ORDLISTIMPTC", "CORPOIMPTC", "ol_", "ec_") Then Return False

      If Not bSoloImpegni Then
        ds.Tables("TESTA").Columns("et_datord").ColumnName = "et_datdoc"
        ds.Tables("TESTA").Columns("et_numord").ColumnName = "et_numdoc"
        ds.Tables("CORPO").Columns("ec_progr").ColumnName = "ec_riga"
        ds.Tables("CORPO").Columns("ec_olprogr").ColumnName = "ec_rigaor"
      End If

      ds.Tables("ATTIVIT").Columns("at_progr").ColumnName = "at_riga"
      ds.Tables("ASSRIS").Columns("as_progr").ColumnName = "as_riga"
      ds.Tables("CORPOIMP").Columns("ec_progr").ColumnName = "ec_riga"
      ds.Tables("CORPOIMP").Columns("ec_olprogr").ColumnName = "ec_rigaor"
      ds.Tables("CORPOIMPTC").Columns("ec_progr").ColumnName = "ec_riga"

      If Not bSoloImpegni Then
        ds.Tables("CORPOTC").Columns("ec_progr").ColumnName = "ec_riga"
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

  Public Overridable Function ApriOrdListImpegni(ByVal lProgrH As Integer) As Boolean
    '-------------------------
    'dato una riga H ricarico gli impegni/lavorazioni collegate. 
    'viene chiamata all'apertura della proposta d'ordine e ad ogni cambio di riga
    Dim ds As New DataSet
    Dim i As Integer = 0
    Try
      If Not oCldGsol.GetOrdListImpegni(strDittaCorrente, lProgrH, ds) Then Return False
      StandardizzaDatatable(ds, True)

      'importo gli impegni
      For i = 0 To ds.Tables("CORPOIMP").Rows.Count - 1
        dsShared.Tables("CORPOIMP").ImportRow(ds.Tables("CORPOIMP").Rows(i))
      Next
      dsShared.Tables("CORPOIMP").AcceptChanges()

      For i = 0 To ds.Tables("CORPOIMPTC").Rows.Count - 1
        dsShared.Tables("CORPOIMPTC").ImportRow(ds.Tables("CORPOIMPTC").Rows(i))
      Next
      dsShared.Tables("CORPOIMPTC").AcceptChanges()

      'importo attivit
      For i = 0 To ds.Tables("ATTIVIT").Rows.Count - 1
        dsShared.Tables("ATTIVIT").ImportRow(ds.Tables("ATTIVIT").Rows(i))
      Next
      dsShared.Tables("ATTIVIT").AcceptChanges()
      'importo assris
      For i = 0 To ds.Tables("ASSRIS").Rows.Count - 1
        dsShared.Tables("ASSRIS").ImportRow(ds.Tables("ASSRIS").Rows(i))
      Next
      dsShared.Tables("ASSRIS").AcceptChanges()

      bRiscriviImpegni = False

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function SvuotaTemporaneiImpegni() As Boolean
    Try
      'svuoto i temporanei dei datatable per gli impegni: al cambio di riga e in stato 0
      'altrimenti potrei trovarmi su una nuova riga con ancora i temporanei della riga precedente
      If Not dttECIMP Is Nothing Then
        dttECIMP.Clear()
        dttECIMPTC.Clear()
        dttATTIVIT.Clear()
        dttASSRIS.Clear()
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

#Region "SetDefaultValue"
  Public Overridable Sub SetDefaultValueOrdlist(ByRef ds As DataSet)
    Try
      oCldGsol.SetTableDefaultValueFromDB("TESTORD", "TESTORD", ds)

      '--------------------------------------------------------------
      'imposto i valori di default di movord: con la riga che seguen prendo prima i valori dal database
      oCldGsol.SetTableDefaultValueFromDB("ORDLIST", "ORDLIST", ds)
      'ora imposto i valori di default diversi da quelli impostati nel database
      With ds.Tables("ORDLIST")
        .Columns("codditt").DefaultValue = strDittaCorrente
        .Columns("ol_datini").DefaultValue = DateTime.Now.ToShortDateString
        .Columns("ol_datfin").DefaultValue = DateTime.Now.ToShortDateString
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
  Public Overridable Sub SetDefaultValueOrdlistImp(ByRef ds As DataSet)
    Try
      '--------------------------------------------------------------
      'imposto i valori di default di movord: con la riga che seguen prendo prima i valori dal database
      oCldGsol.SetTableDefaultValueFromDB("ORDLIST", "ORDLISTIMP", ds)
      'ora imposto i valori di default diversi da quelli impostati nel database
      With ds.Tables("ORDLISTIMP")
        .Columns("codditt").DefaultValue = strDittaCorrente
        .Columns("ol_tipork").DefaultValue = "Y"
        .Columns("ol_datini").DefaultValue = DateTime.Now.ToShortDateString
        .Columns("ol_datfin").DefaultValue = DateTime.Now.ToShortDateString
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
  Public Overridable Sub SetDefaultValueOrdlistTC(ByRef ds As DataSet)
    Try

      '--------------------------------------------------------------
      'imposto i valori di default di movord: con la riga che seguen prendo prima i valori dal database
      oCldGsol.SetTableDefaultValueFromDB("ORDLISTTC", ds)
      'ora imposto i valori di default diversi da quelli impostati nel database
      With ds.Tables("ORDLISTTC")
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
  Public Overridable Sub SetDefaultValueOrdlistImpTC(ByRef ds As DataSet)
    Try

      '--------------------------------------------------------------
      'imposto i valori di default di movord: con la riga che seguen prendo prima i valori dal database
      oCldGsol.SetTableDefaultValueFromDB("ORDLISTTC", "ORDLISTIMPTC", ds)
      'ora imposto i valori di default diversi da quelli impostati nel database
      With ds.Tables("ORDLISTIMPTC")
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
  Public Overridable Sub SetDefaultValueZzassris(ByRef ds As DataSet)
    Try

      '--------------------------------------------------------------
      'imposto i valori di default di movord: con la riga che seguen prendo prima i valori dal database
      oCldGsol.SetTableDefaultValueFromDB("ZZASSRIS", "ASSRIS", ds)
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
  Public Overridable Sub SetDefaultValueZzattivit(ByRef ds As DataSet)
    Try

      '--------------------------------------------------------------
      'imposto i valori di default di movord: con la riga che seguen prendo prima i valori dal database
      oCldGsol.SetTableDefaultValueFromDB("ZZATTIVIT", "ATTIVIT", ds)
      'ora imposto i valori di default diversi da quelli impostati nel database
      With ds.Tables("ATTIVIT")
        .Columns("codditt").DefaultValue = strDittaCorrente
        .Columns("at_sutipork").DefaultValue = "H"
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
#End Region

  Public Overridable Function RecordCancella(ByVal lProgr As Integer) As Boolean
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      bNonValidare = True      'per non far scatenare la recordSalva al cambio di riga

      If lProgr = -1 Then
        dtrT = dttEC.Select()
      Else
        dtrT = dttEC.Select("ec_riga = " & lProgr.ToString)
      End If

      For i = 0 To dtrT.Length - 1
        If Not oCldGsol.SalvaOrdlist(strDittaCorrente, dsShared, NTSCInt(dtrT(i)!ec_riga), strTipork, "D", bModTCO, False, bGestioneAbbinamentiTaglie) Then
          Return False
        Else
          dtrT(i).Delete()
          dttEC.AcceptChanges()
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
    Finally
      bNonValidare = False
      dsShared.AcceptChanges()
    End Try
  End Function

  Public Overrides Function RecordSalva(ByVal nRow As Integer, ByVal bDelete As Boolean, ByRef dtrDeleted As DataRow) As Boolean
    Dim nStato As Integer       '1 = nuova riga, 2 = riga modificata
    Dim dtrT() As DataRow = Nothing
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Try
      If bNonValidare Then Return False
      If bInUnload Then Return False
      If nRow < 0 Then Return True
      If nRow >= dttEC.Rows.Count Then Return False
      If dttEC.Rows(nRow).RowState <> DataRowState.Unchanged Then bHasChangesET = True
      If nRow >= dttEC.Rows.Count Then Return True 'passo di qui anche se sono sull'ultima riga (quella di addnew) 

      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If bDelete = False Then
        If nRow >= 0 Then

          If dttEC.Rows(nRow).RowState = DataRowState.Added Then
            nStato = 1
            If Not CorpoTestPreSalva(dttEC, nRow) Then Return False
            If Not CorpoBeforeInsert(nRow) Then Return False
          End If

          If dttEC.Rows(nRow).RowState = DataRowState.Modified Then
            nStato = 2
            If Not CorpoTestPreSalva(dttEC, nRow) Then Return False
            If Not CorpoBeforeUpdate(nRow) Then Return False
          End If

          '-------------------------------
          'test lotto univoco
          If bLottoUnivoco And NTSCStr(dttEC.Rows(nRow)!xxo_geslotti) = "S" And NTSCStr(dttEC.Rows(nRow)!xxo_lottox) <> "" Then
            If Not oCldGsol.LottoxCheckLottoUnivoco(strDittaCorrente, NTSCStr(dttEC.Rows(nRow)!ec_codart), _
                                        NTSCStr(dttEC.Rows(nRow)!xxo_lottox), strTmp) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129526281562842028, "Test lotto univoco: Il lotto '|" & NTSCStr(dttEC.Rows(nRow)!xxo_lottox) & "|' impostato sull'articolo |" & NTSCStr(dttEC.Rows(nRow)!ec_codart) & "| è già stato utilizzato per l'articolo |" & strTmp & "|")))
              Return False
            End If
          End If

          '-------------------------------
          'salvo nel DB
          'attenzione: viene chiamata in automatico alla fine della 'ScriviRigheDaDiba' 
          If Not oCldGsol.SalvaOrdlist(strDittaCorrente, dsShared, NTSCInt(dttEC.Rows(nRow)!ec_riga), _
                                       strTipork, IIf(dttEC.Rows(nRow).RowState = DataRowState.Added, "N", "U").ToString, _
                                       bModTCO, bRiscriviImpegni, bGestioneAbbinamentiTaglie) Then Return False
          dsShared.AcceptChanges()
          bRiscriviImpegni = False

          If nStato = 1 Then
            'esplodo la db, poi devo salvare gli impegni
            'nella CorpoAfterInsert viene chiamata la ScriviRigheDaDiba dove alla fine viene ricalcolato il valore della riga H e lanciata 
            'la RecordSalva che con 'bRiscriviImpegni = True' salverà tutti gli impegni/lavoraz collegate'
            If strTipork = "H" Then bRiscriviImpegni = True
            CorpoAfterInsert(nRow)
            If strTipork = "H" Then SettaValoriRiga(dttEC.Rows(nRow)) 'per far ricalcolare il valore di riga (serve per le lavoraz. terziste altrimenti in mo_valore c'è anche il costo del materiale)
          End If

          If nStato = 2 Then
            CorpoAfterUpdate(nRow)
            'se ordine di prod devo risalvare, visto che nella CorpoAfterUpdate modifico impegni/lavoraz. collegati
            If dttEC.Rows(nRow)!ec_tipork.ToString = "H" Then
              If Not oCldGsol.SalvaOrdlist(strDittaCorrente, dsShared, NTSCInt(dttEC.Rows(nRow)!ec_riga), _
                             strTipork, IIf(dttEC.Rows(nRow).RowState = DataRowState.Added, "N", "U").ToString, _
                             bModTCO, True, bGestioneAbbinamentiTaglie) Then Return False
              dsShared.AcceptChanges()
              bRiscriviImpegni = False
            End If
          End If

          oCldGsol.AggiornaLLC(dttEC.Rows(nRow), dttTmp)

          If dttTmp.Rows.Count <> 0 Then dttEC.Rows(nRow)!xxo_livmindb = dttTmp.Rows(0)!xx_livmindb
        End If
      Else
        '-------------------------------
        'cancello nel DB
        bNonValidare = True
        If Not oCldGsol.SalvaOrdlist(strDittaCorrente, dsShared, NTSCInt(dttEC.Rows(nRow)!ec_riga), _
                                     strTipork, "D", bModTCO, False, bGestioneAbbinamentiTaglie) Then
          bNonValidare = False
          Return False
        End If
        If CorpoAfterDelete(dtrDeleted) = False Then Return False

        '-------------------------------
        'confermo le modifiche apportate alla griglia
        dsShared.AcceptChanges()
        bRiscriviImpegni = False
      End If    'If Not bDelete Then

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
    End Try
  End Function
  Public Overrides Function CorpoImpRecordSalva(ByVal nRow As Integer, ByVal bDelete As Boolean, ByRef dtrDeleted As DataRow) As Boolean
    Dim strTmp As String = ""
    Dim dttCorpo As DataTable
    Try
      If Not MyBase.CorpoImpRecordSalva(nRow, bDelete, dtrDeleted) Then Return False

      If Not bDelete Then
        '-------------------------------
        'test lotto univoco
        If bLottoUnivoco And nRow <> -1 Then
          If dsImpe.Tables.Count = 0 Then
            dttCorpo = dsShared.Tables("CORPOIMP")
          Else
            dttCorpo = dsImpe.Tables("CORPOIMP")
          End If
          If NTSCStr(dttCorpo.Rows(nRow)!xxo_geslotti) = "S" And NTSCStr(dttCorpo.Rows(nRow)!xxo_lottox) <> "" Then
            If Not oCldGsol.LottoxCheckLottoUnivoco(strDittaCorrente, NTSCStr(dttCorpo.Rows(nRow)!ec_codart), _
                                                    NTSCStr(dttCorpo.Rows(nRow)!xxo_lottox), strTmp) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129526821645190340, "Test lotto univoco: Il lotto '|" & NTSCStr(dttCorpo.Rows(nRow)!xxo_lottox) & "|' impostato sull'articolo |" & NTSCStr(dttCorpo.Rows(nRow)!ec_codart) & "| è già stato utilizzato per l'articolo |" & strTmp & "|")))
              Return False
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


  Public Overridable Function SettaTesta(ByRef dtrRow As DataRow) As Boolean
    'al cambio di riga o di uno dei campi sotto riportati aggiorno testa con i dati di ordlist corrente
    Try
      If dttET.Rows.Count = 0 Then
        dttET.Rows.Add(dttET.NewRow())
      End If

      dttET.Rows(0)!et_tipork = strTipork
      If Not dtrRow Is Nothing Then
        With dttET.Rows(0)
          !et_tipork = strTipork
          !et_conto = dtrRow!ec_conto
          !et_datdoc = dtrRow!ec_datord
          !et_valuta = dtrRow!ec_codvalu
          !et_cambio = dtrRow!ec_cambio
          !et_magaz = dtrRow!ec_magaz
          !et_magaz2 = dtrRow!ec_magaz2
          !et_magimp = dtrRow!ec_magimp
          !et_scorpo = "N"
        End With
        bTerzista = CType(oCleComm, CLELBMENU).IsTerzista(strDittaCorrente, NTSCInt(dtrRow!ec_magimp))
      End If
      nClscan = 0
      nTabling = 0

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub SettaPrezzoGsol(ByRef dtrEC As DataRow)
    Dim dttTmp As New DataTable
    Try
      nClscan = 0
      If NTSCInt(dttET.Rows(0)!et_conto) <> 0 Then
        oCldGsol.ValCodiceDb(NTSCInt(dttET.Rows(0)!et_conto).ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp)
        dttET.Rows(0)!et_listino = NTSCInt(dttTmp.Rows(0)!an_listino)
        dttTmp.Clear()
      End If
      bCambioContoGsol = True
      SettaPrezzo(dtrEC, NTSCStr(dtrEC!ec_codart), strVisMemList1, nVisMemNumList)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      bCambioContoGsol = False
    End Try
  End Sub
  Public Overridable Sub SettaScontiGsol(ByRef dtrEC As DataRow)
    Dim dttTmp As New DataTable
    Try
      nClscan = 0
      If NTSCInt(dttET.Rows(0)!et_conto) <> 0 Then
        oCldGsol.ValCodiceDb(NTSCInt(dttET.Rows(0)!et_conto).ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp)
        nClscan = NTSCInt(dttTmp.Rows(0)!an_clascon)
        dttTmp.Clear()
      End If
      GetMemDttArti(NTSCStr(dtrEC!ec_codart))
      bCambioContoGsol = True
      SettaSconti(dtrEC, NTSCInt(dttArti.Rows(0)!ar_clascon), nClscan)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      bCambioContoGsol = False
    End Try
  End Sub

  Public Overrides Sub CorpoOnAddNewRow(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strTipork = "H" And NTSCStr(e.Row!ec_tipork) <> "Y" Then SvuotaTemporaneiImpegni()
      MyBase.CorpoOnAddNewRow(sender, e)

      e.Row!ec_datord = NTSCDate(DateTime.Now.ToShortDateString)
      e.Row!ec_datcons = NTSCDate(DateTime.Now.ToShortDateString)
      e.Row!ec_magaz = nFirstMagaz
      If e.Row!ec_tipork.ToString = "H" Then
        'se sto inserendo una riga di scarico devo proporre il magaz di scarico della riga di ordine di prod, altrimenti il primo magazzino
        If bInOnAddNewRowImp Then
          e.Row!ec_magimp = NTSCInt(dttET.Rows(0)!et_magimp)
        Else
          e.Row!ec_magimp = nFirstMagaz
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

  Public Overrides Function CorpoBeforeUpdate(ByVal nRow As Integer) As Boolean
    Dim dtrEC As DataRow
    Dim dtrT() As DataRow = Nothing
    Try
      dtrEC = dttEC.Rows(nRow)
      If Not MyBase.CorpoBeforeUpdate(nRow) Then Return False

      '---------------------------------------------------------------------------------------
      'Calcolo del rapporto cambio quantità/flevas/datcons
      If dttEC.Rows(nRow)!ec_tipork.ToString = "H" And dtrEC.RowState <> DataRowState.Added Then
        'data consegna
        lDiffDatcons = NTSCInt(DateDiff("d", NTSCDate(dtrEC("ec_datcons", DataRowVersion.Original)), NTSCDate(dtrEC!ec_datcons)))
      End If    'If dttet.rows(0)!et_tipork.ToString = "H" Then

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overrides Function CorpoAfterUpdate(ByVal nRow As Integer) As Boolean
    Dim dtrEC As DataRow
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0

    Try
      dtrEC = dttEC.Rows(nRow)

      If dttEC.Rows(nRow)!ec_tipork.ToString = "H" Then
        '---------------------------------
        'al cambio della data di consegna sulla riga di produzione chiedo se cambiarla sulle righe degli impegni/lavorazioni
        If lDiffDatcons <> 0 Then
          AggImpegniDatcons(lDiffDatcons, dtrEC)
        End If
        lDiffDatcons = 0

        '---------------------------------
        'lo stato degli impegni deve essere uguale allo stato dell'ordine
        dtrT = dttECIMP.Select("ec_stato <> " & CStrSQL(dttEC.Rows(nRow)!ec_stato.ToString))
        If dtrT.Length <> 0 Then
          For i = 0 To dtrT.Length - 1
            dtrT(i)!ec_stato = dttEC.Rows(nRow)!ec_stato.ToString
          Next
        End If
      End If

      If Not MyBase.CorpoAfterUpdate(nRow) Then Return False

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
      bCancellaScarichi = False
      lDiffDatcons = 0
      dRapportoQta = 0
      bCambioFlevas = False
      bCambioQuantita = False
      bCambioCommessa = False
    End Try
  End Function

#Region "BeforeColUpdate"
  Public Overrides Function BeforeColUpdate_CORPO_ec_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Try
      If NTSCStr(e.Row!ec_tipork) = "Y" Then
        e.Row!ec_conto = NTSCInt(dttET.Rows(0)!et_conto)
      Else
        oCldGsol.ValCodiceDb(NTSCStr(e.ProposedValue).ToString, strDittaCorrente, "ARTICO", "S", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          e.Row!ec_conto = NTSCInt(dttTmp.Rows(0)!ar_forn)
        End If
      End If

      'NB: SETTO IL CONTO CLIENTE PRIMA DELLA AfterColUpdate_CORPO_ec_codart, così le routine cercaprezzo/sconti/proposta magaz/descr. articolo in lingua sono già ok
      If Not MyBase.BeforeColUpdate_CORPO_ec_codart(sender, e) Then Return False

      If bModTCO And CStrSQL(e.Row!ec_codart).Trim <> "" And CStrSQL(e.ProposedValue).Trim <> "" And _
         (e.Row.RowState <> DataRowState.Added Or NTSCDec(e.Row!ec_quant) <> 0) Then
        e.ProposedValue = e.Row!ec_codart
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128810685484864000, "Con il modulo 'Taglie e Colori' attivato una volta inserito un articolo non è più possibile modificarlo. eventualmente cancellare e reinserire la riga")))
        Return False
      End If

      'Se si tratta di ordine di prod. riporta il magazzino impegni:
      'il mag. di produzione dell'articolo o il primo magazzino associato al fornitore abituale
      'se mancati manutiene il primo magazzino di merce propria (quello predefinito)
      If e.Row!ec_tipork.ToString = "H" Then
        If NTSCInt(dttTmp.Rows(0)!ar_magprod) > 0 Then
          e.Row!ec_magimp = NTSCInt(dttTmp.Rows(0)!ar_magprod)
        Else
          If NTSCInt(dttTmp.Rows(0)!ar_forn) > 0 Then
            i = oCldGsol.GetTabgamaFromConto(strDittaCorrente, NTSCInt(dttTmp.Rows(0)!ar_forn))
            If i <> 0 Then e.Row!ec_magimp = i
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
    End Try
  End Function

  Public Overrides Function BeforeColUpdate_CORPO_ec_fase(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim nMagProd As Integer = 0
    Dim lFornProd As Integer = 0
    Try
      'NB: SETTO IL CONTO CLIENTE PRIMA DELLA AfterColUpdate_CORPO_ec_fase, così le routine cercaprezzo/sconti/proposta magaz/descr. articolo in lingua sono già ok
      If Not MyBase.BeforeColUpdate_CORPO_ec_fase(sender, e) Then Return False

      If NTSCInt(e.ProposedValue) = 0 Then Return True

      'Se si tratta di ordine di prod. riporta il magazzino impegni:
      'il mag. di produzione dell'articolo o il primo magazzino associato al fornitore abituale
      'se mancati manutiene il primo magazzino di merce propria (quello predefinito)
      If e.Row!ec_tipork.ToString = "H" Then
        oCldGsol.ValCodiceDb(NTSCStr(e.Row!ec_codart), strDittaCorrente, "ARTICO", "S", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          nMagProd = NTSCInt(dttTmp.Rows(0)!ar_magprod)
          lFornProd = NTSCInt(dttTmp.Rows(0)!ar_forn)
          If lFornProd <> 0 Then
            i = oCldGsol.GetTabgamaFromConto(strDittaCorrente, NTSCInt(dttTmp.Rows(0)!ar_forn))
            If i <> 0 Then nMagProd = i
          End If
        End If

        'per aticoli a fasi la cui fase non è = a ultima fase di artico cerca di leggere il fornitore ed il magazzino di produzione dalla DB
        If bArtFasiGetFornFromCicli Then
          If NTSCInt(e.ProposedValue) <> NTSCInt(dttTmp.Rows(0)!ar_ultfase) Then
            'se la fase è minore di quella finale, cerco il fornitore abituale ed il magaz. produzoine dalla tabella 'cicli'
            If Not oCldGsol.GetFornEMagazFromCicli(strDittaCorrente, NTSCStr(dttTmp.Rows(0)!ar_coddb), NTSCInt(e.ProposedValue), NTSCDate(e.Row!ec_datord).ToShortDateString, l, i) Then Return False
            If l <> 0 Then
              lFornProd = l
              nMagProd = i
            End If
          End If
        End If

        If lFornProd <> 0 Then e.Row!ec_conto = lFornProd
        If nMagProd <> 0 Then e.Row!ec_magimp = nMagProd
      End If    'If e.Row!ec_tipork.ToString = "H" Then

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

  Public Overridable Function BeforeColUpdate_CORPO_ec_conto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim dttTmp As New DataTable
    Dim bOk As Boolean = True
    Dim evnt As NTSEventArgs = Nothing
    Dim i As Integer = 0
    Try
      If bSaltaValidazione Then Return True

      If NTSCInt(e.ProposedValue) = 0 Then e.Row!xxo_conto = "" : Return True
      If Not ValCodice(e, "ANAGRAFC", "xxo_conto", oApp.Tr(Me, 128619068781250000, "Codice cliente/fornitore non corretto"), "N", dttTmp) Then
        Return False
      End If

      'il conto non era stato ancora
      If NTSCInt(e.Row!ec_conto) = 0 Then
        e.Row!ec_codvalu = dttTmp.Rows(0)!an_valuta
      End If
      dttET.Rows(0)!et_conto = e.ProposedValue
      dttET.Rows(0)!et_listino = NTSCInt(dttTmp.Rows(0)!an_listino)

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

      nTabling = NTSCInt(dttTmp.Rows(0)!an_codling)

      If NTSCStr(e.Row!ec_codart).Trim <> "" And bCambioFornitorePrezzi = False Then
        If e.Row.RowState <> DataRowState.Added And e.Row!ec_tipork.ToString <> "Y" Then
          bOk = True
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128619862971406250, _
                                  "Il cambio del Fornitore può comportare il cambio del prezzo e/o degli sconti." & vbCrLf & _
                                  "Utilizzare prezzo e sconti relativi al fornitore indicato?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = "NO" Then bOk = False
        Else
          bOk = True
        End If

        If bOk And e.Row!ec_tipork.ToString <> "Y" Then
          SettaPrezzoGsol(e.Row)
          SettaScontiGsol(e.Row)

          'cambio anche sugli impegni collegati
          For i = 0 To dttECIMP.Rows.Count - 1
            If NTSCInt(dttECIMP.Rows(i)!ec_conto) <> NTSCInt(e.ProposedValue) Then
              dttECIMP.Rows(i)!ec_conto = NTSCInt(e.ProposedValue)
            End If
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
    Finally
      dttTmp.Clear()
      bSaltaValidazione = False
    End Try
  End Function

  Public Overrides Function BeforeColUpdate_CORPO_ec_codclie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ValCodice(e, "ANAGRAC", "xxo_codclie", oApp.Tr(Me, 128810685538216000, "Codice cliente non corretto"), "N")
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function BeforeColUpdate_CORPO_ec_codvalu(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      ValCodice(e, "TABVALU", "xxo_codvalu", oApp.Tr(Me, 128619068803281250, "Codice valuta non corretto"), "N")
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function BeforeColUpdate_CORPO_ec_magimp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim evnt As NTSEventArgs = Nothing
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

    Dim dPrezvalc As Decimal = 0
    Dim dPrezzo As Decimal = 0
    Dim dValorevRes As Decimal = 0
    Dim dValoreRes As Decimal = 0
    Dim dValore As Decimal = 0
    Dim dValorev As Decimal = 0


    Try
      ValCodice(e, "TABMAGA", "xxo_magimp", oApp.Tr(Me, 128619819356718750, "Codice magazzino impegni non corretto"), "N", dttTmp)
      If e.Row!ec_tipork.ToString = "H" Then
        'il flag 'terzista' si riferisce sempre al magazzino impegni della riga 'H'
        bTerzista = CType(oCleComm, CLELBMENU).IsTerzista(strDittaCorrente, NTSCInt(e.ProposedValue))
        If CType(oCleComm, CLELBMENU).GestioneUMPrezzo(bGestionePrezzi, "BSORGSOR", _
                     dttET.Rows(0)!et_tipork.ToString, _
                     bTerzista, e.Row!ec_codart.ToString, e.Row!ec_unmis.ToString, _
                     strDittaCorrente) And _
                     Not ((dttET.Rows(0)!et_tipork.ToString = "H" Or _
                     dttET.Rows(0)!et_tipork.ToString = "T") And _
                     e.Row.Table.TableName.ToUpper = "CORPOIMP") Then
          e.Row!ec_umprz = "S"
        Else
          e.Row!ec_umprz = "N"
        End If
      End If
      If e.Row!ec_tipork.ToString = "H" And e.Row!ec_codart.ToString.Trim <> "" And dttECIMP.Rows.Count > 0 And NTSCInt(e.ProposedValue) <> 0 Then
        evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128625721572656250, _
                                  "Modificare il magazzino degli impegni su tutte le righe di questo ordine di produzione?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
          '----------------------------
          'modifico il magazzino sigli impegni
          For Each dtrT As DataRow In dttECIMP.Rows
            dtrT!ec_magaz = NTSCInt(e.ProposedValue)
          Next
          dttECIMP.AcceptChanges()
          bRiscriviImpegni = True
        End If

        If bTerzista Then
          evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128526520410708000, "Modificare il centro di lavoro indicato nelle lavorazioni di questo documento di produzione ESTERNO?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then

            If NTSCInt(dttTmp.Rows(0)!tb_magconto) <> NTSCInt(e.Row!ec_conto) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526521920164000, "Il conto fornitore deve essere uguale al conto associato al Magazzino impegni; impossibile rettificare le righe di lavorazione.")))
              Return False
            End If

            'Determina il centro di lavoro esterno
            If Not oCldGsol.GetTabcentTabcoveFromMagaz(strDittaCorrente, NTSCInt(e.ProposedValue), dttTabcent) Then Return False
            If dttTabcent.Rows.Count = 0 Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128526525077292000, "Nessun centro di lavoro esterno associato al magazzino impegni |" & NTSCInt(e.ProposedValue).ToString & "|; impossibile rettificare le righe di lavorazione.")))
              Return False
            End If

            bRiscriviImpegni = True

            nNewCodcent = NTSCInt(dttTabcent.Rows(0)!tb_codcent)
            nNewCcodcontr = NTSCInt(dttTabcent.Rows(0)!tb_ccodcontr)
            nNewCcodcena = NTSCInt(dttTabcent.Rows(0)!tb_ccodcena)
            lNewConcova = NTSCInt(dttTabcent.Rows(0)!tb_concova)
            dttTabcent.Clear()

            '-------------------------
            'ora devo cambiare il centro di lavoro sulle rihe di assris collegate 
            'alle attività ancora non totalmente evase
            lLastRow = -1
            lCountFasi = 0
            bModif = False

            '-------------------------
            'ottengo l'elenco delle lavorazioni non ancora evase totalmente
            For Each dtrAtt In dttATTIVIT.Select("at_flevas <> 'S'", "at_riga, at_fase")
              dtrAss = dttASSRIS.Select("as_riga = " & dtrAtt!at_riga.ToString & " AND as_fase = " & dtrAtt!at_fase.ToString)
              If dtrAss.Length > 0 Then
                'se c'è il centro di lavoro ...
                dtrEc = dttEC.Select("ec_riga = " & dtrAtt!at_riga.ToString)

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

                Dim oCondCommerciali As NTSCondCommerciali = IstanziaNTSCondCommerciali()
                oCondCommerciali.bCalcolaPrezzo = True
                With oCondCommerciali.Input
                  .strDitta = strDittaCorrente
                  .strCodart = NTSCStr(dtrEc(0)!ec_codart)
                  .nCodlavo = NTSCInt(dtrAtt!at_codlavo)
                  .lConto = NTSCInt(dttET.Rows(0)!et_conto)
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
                  .bPrezziPerUnmis = (dtrEc(0)!ec_umprz.ToString = "S")
                End With
                '--------------------
                CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
                '--------------------
                dPrezzo = oCondCommerciali.OutputPrezzo.dPrezzo
                'Calcoli finali
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
                  dValoreRes = ArrDbl(dPrezzo * (NTSCDec(dtrEc(0)!ec_quant)) / NTSCDec(dtrEc(0)!ec_perqta), oCldDocu.TrovaNdec(0))
                  If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
                    dValorevRes = ArrDbl(dPrezvalc * (NTSCDec(dtrEc(0)!ec_quant)) / NTSCDec(dtrEc(0)!ec_perqta), oCldDocu.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta)))
                  End If
                Else
                  dValoreRes = ArrDbl(dPrezzo * (NTSCDec(dtrEc(0)!ec_colli)) / NTSCDec(dtrEc(0)!ec_perqta), oCldDocu.TrovaNdec(0))
                  If NTSCInt(dttET.Rows(0)!et_valuta) <> 0 Then
                    dValorevRes = ArrDbl(dPrezvalc * (NTSCDec(dtrEc(0)!ec_colli)) / NTSCDec(dtrEc(0)!ec_perqta), oCldDocu.TrovaNdec(NTSCInt(dttET.Rows(0)!et_valuta)))
                  End If
                End If

                dtrEc(0)!ec_prezvalc = dPrezvalc
                dtrEc(0)!ec_prezzo = dPrezzo
                dtrEc(0)!ec_valorev = dValorevRes
                dtrEc(0)!ec_valore = dValoreRes

                dtrEc(0).AcceptChanges()
SaltaFase:
              End If    'If dtrAss.Length > 0 Then
            Next    'For Each dtrAtt In dttATTIVIT.Select("at_flevas <> 'S'", "at_riga, at_fase")

            '----------------
            'Riapre il corpo, e rivalorizza se serve

            If bModif Then ValorizzaProduzione(e.Row)

          End If    'If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then

        End If    'If bTerzista Then

      End If    'If e.Row!ec_tipork.ToString = "H" And e.Row!ec_codart.ToString.Trim <> "" And dttECIMP.Rows.Count > 0 Then


      Return True
    Catch ex As Exception
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

#Region "AfterColUpdate"
  Public Overrides Sub AfterColUpdate_CORPO(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim nTabLingTmp As Integer = 0
    Try
      MyBase.AfterColUpdate_CORPO(sender, e)

      'aggiorno testa con i possibili valori di corpo modificati (conto, datord, valuta, cambio, ...)
      Select Case e.Column.ColumnName.ToUpper
        Case "EC_DATORD", "EC_CODVALU", "EC_CAMBIO", "EC_MAGAZ", "EC_MAGAZ2", "EC_MAGIMP"
          SettaTesta(e.Row)
        Case "EC_CONTO"
          'solo in inserimento nuova riga: dopo aver digitato il cod articolo, se il fornitore abituale lavora
          'in lingua deve proporre la descr. in lingua, ma in uscita dalla SettTesta il cod lingua viene azzerato...
          'lo persisto con una variabile temporanea
          nTabLingTmp = nTabling
          SettaTesta(e.Row)
          nTabling = nTabLingTmp
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

  Public Overridable Function AfterColUpdate_CORPO_ec_codvalu(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Try
      If NTSCInt(e.ProposedValue) <> 0 Then
        'NON POSSO FARLO QUI... potrei aver impostato magazzino terzista, poi indico la valuta poi imposto magazzino interno ....
        'ESEGUO IL TEST SOLO IN FASE DI SALVATAGGIO RIGA
        'If strTipork = "H" Then
        '  bTerzista = CType(oCleComm, CLELBMENU).IsTerzista(strDittaCorrente, NTSCInt(e.Row!ec_magimp))
        '  If bTerzista Then
        '    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128625759685156250, "Non è possibile emettere proposte d'ordine di produzione in valuta")))
        '    e.ProposedValue = 0
        '  End If
        'End If
        'cerco il cambio alla data del documento
        e.Row!ec_cambio = oCldGsol.CercaCambioDiOggi(NTSCInt(e.ProposedValue), e.Row!ec_datord.ToString)
      Else
        If NTSCDec(e.Row!ec_cambio) <> 0 Then
          e.Row!ec_cambio = 0
          e.Row!ec_prezvalc = 0
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

  Public Overridable Function AfterColUpdate_CORPO_ec_cambio(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    Dim i As Integer = 0
    Dim dTmp As Decimal = 0
    Try
      '---------------------------------------------
      'devo aggiornare il valore delle righe di movord al variare del cambio
      dTmp = oCldGsol.ConvImpValuta(strDittaCorrente, True, NTSCDec(e.Row!ec_prezvalc), NTSCInt(e.Row!ec_codvalu), NTSCDate(e.Row!ec_datord), NTSCDec(e.Row!ec_cambio))
      e.Row!ec_prezzo = dTmp

      '---------------------------------------------
      If dttET.Rows(0)!et_tipork.ToString = "H" Then
        For Each dtrT As DataRow In dttECIMP.Select("ec_rigaor = " & NTSCInt(e.Row!ec_riga))
          'devo aggiornare il valore delle righe di movord/movmag al variare del cambio
          dTmp = oCldDocu.ConvImpValuta(strDittaCorrente, True, NTSCDec(dtrT!ec_prezvalc), NTSCInt(e.Row!ec_codvalu), NTSCDate(e.Row!ec_datord), NTSCDec(e.Row!ec_cambio))
          dtrT!ec_prezzo = dTmp
        Next
      End If

      '---------------------------------------------
      'rivalorizzo gli impegni e le lavorazioni, visto che è cambiato il cambio ...
      ValorizzaProduzione(e.Row)

      Return True
    Catch ex As Exception
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

  Public Overrides Function CorpoTestPreSalva(ByRef dttCorpo As DataTable, ByVal nRow As Integer) As Boolean
    Dim i As Integer = 0

    'usata sia da corpo che da corpoimp
    Try
      If dttCorpo.Rows(nRow)!ec_tipork.ToString = "H" Then
        If NTSCInt(dttCorpo.Rows(nRow)!ec_magimp) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128619664818750000, "Il codice magazzino impegni è un campo obbligatorio nelle proposte d'ordine di produzione: indicarlo prima di salvare la riga")))
          Return False
        End If

        'Assicura che un ORD.PROD. INTERNO non sia in valuta
        If NTSCInt(dttCorpo.Rows(nRow)!ec_codvalu) > 0 Then
          bTerzista = CType(oCleComm, CLELBMENU).IsTerzista(strDittaCorrente, NTSCInt(dttCorpo.Rows(nRow)!ec_magimp))
          If bTerzista = False Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128619685850312500, "Impossibile creare proposte d'ordine di produzione INTERNE in valuta.")))
            Return False
          End If
        End If
      End If    'If dttET.Rows(0)!et_tipork.ToString = "H" Then

      For i = 0 To dttCorpo.Rows.Count - 1
        If Microsoft.VisualBasic.Right(NTSCStr(dttCorpo.Rows(i)!ec_codart), 1) = " " Then
          dttCorpo.Rows(i)!ec_codart = Microsoft.VisualBasic.RTrim(dttCorpo.Rows(i)!ec_codart.ToString)
        End If
      Next

      If Not MyBase.CorpoTestPreSalva(dttCorpo, nRow) Then Return False

      If NTSCInt(dttCorpo.Rows(nRow)!ec_codvalu) <> 0 And NTSCDec(dttCorpo.Rows(nRow)!ec_cambio) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128619699244062500, "Se impostata una valuta diversa da 0 il cambio deve essere diverso da 0")))
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

  Public Overridable Function RiempiTmpTable(ByVal lIIAssris As Integer, ByVal lIIAttivit As Integer, _
                                            ByVal lIIOrlist As Integer, ByVal lIIOrdl As Integer, _
                                            ByVal lIIOrlisttc As Integer, ByVal strProgr As String, _
                                            ByVal bSoloImpegni As Boolean, ByVal bSoloAttivita As Boolean) As Boolean
    Try
      Return oCldGsol.RiempiTmpTable(strDittaCorrente, strTipork, lIIAssris, lIIAttivit, lIIOrlist, _
                                     lIIOrdl, lIIOrlisttc, strProgr, bModTCO, bSoloImpegni, bSoloAttivita)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

End Class
