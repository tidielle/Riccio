Imports System.Data
Imports NTSInformatica.CLN__STD

Partial Public Class CLE__ANAZ
  Inherits CLE__BASN

#Region "Moduli"
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
  Private ModuliSup_P As Integer = CLN__STD.bsModSupCAE
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

#Region "Variabili"
  Public oCldAnaz As CLD__ANAZ
  Public dttInsg As New DataTable

  Public lDestdomf As Integer = -1              'Valore pre-impostato per Indirizzo Domicilio fiscale per provvedimenti amministrativi
  Public lDestsedel As Integer = 0              'Valore pre-impostato per Indirizzo Residenza\Domicilio fiscale\Sede legale in Italia
  Public lDestresan As Integer = 0              'Valore pre-impostato per Indirizzo Residenza\sede legale estera
  Public lDestcorr As Integer = 0               'Valore pre-impostato per Indirizzo Luogo di esercizio attività all'estero

  Public bScriviActlog As Boolean = False
  Public bGesttabcont As Boolean = False

  Public bHasChanges As Boolean
  Public dsShared As DataSet
  Public bAnagen As Boolean = False
  Public bNew As Boolean = False
  Public bNuovoDaAnagen As Boolean = False

  Public lCodDestNew As Integer = 0             'usato da BN__DESG
  Public dttAnazulEx As DataTable               'clone di dsShared.Tables("ANAZUL") per altre destinazioni diverse
  Public dttTabattiEx As DataTable              'clone di dsShared.Tables("TABATTI") usato nella form modale
  Public dttTabduriEx As DataTable              'clone di dsShared.Tables("TABURI") usato nella form modale
  Public dttAccditmEx As DataTable              'clone di dsShared.Tables("ACCDITM") usato nella form modale
  Public dttAccDittEx As DataTable              'clone di dsShared.Tables("ACCDITT") usato nella form modale
  Public nAnnoTabattiAperto As Integer = 0      'usato da BN__AIVA
  Public nAnnoTabduriAperto As Integer = 0      'usato da BN__DURI
  Public strUserAperto As String = ""           'usato da BN__DITM e BN__DITT
  Public nModuloAperto As Integer = 0           'usato da BN__DITT

  Public strEscoPrevCelValue As String = ""
  Public strDicePrevCelValue As String = ""
  Public strDitsPrevCelValue As String = ""
  Public strDapaPrevCelValue As String = ""
  Public strDitaPrevCelValue As String = ""
  Public strDisdPrevCelValue As String = ""
  Public strDismPrevCelValue As String = ""
  Public strAnivPrevCelValue As String = ""
  Public strAivaPrevCelValue As String = ""
  Public strDuriPrevCelValue As String = ""
  Public strDitoPrevCelValue As String = ""
  Public strDitmPrevCelValue As String = ""
  Public strDittPrevCelValue As String = ""

  Public bEscoHasChanges As Boolean
  Public bDiceHasChanges As Boolean
  Public bAmodHasChanges As Boolean
  Public bDitsHasChanges As Boolean
  Public bDapaHasChanges As Boolean
  Public bDitaHasChanges As Boolean
  Public bDisdHasChanges As Boolean
  Public bDismHasChanges As Boolean
  Public bAnivHasChanges As Boolean
  Public bAivaHasChanges As Boolean
  Public bDuriHasChanges As Boolean
  Public bDitoHasChanges As Boolean
  Public bDitmHasChanges As Boolean
  Public bDittHasChanges As Boolean
  Private bNuovaAnalitica As Boolean = False

  Public bsModExtCRM As Boolean = False
  Public bCreaCAREVIS As Boolean = False

  Public Property IsNuovaAnalitica() As Boolean
    Get
      Return bNuovaAnalitica
    End Get
    Set(ByVal value As Boolean)
      bNuovaAnalitica = value
    End Set
  End Property
#End Region

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__ANAZ"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldAnaz = CType(MyBase.ocldBase, CLD__ANAZ)
    oCldAnaz.Init(oApp)

    bsModExtCRM = CBool(Me.ModuliExt() And CLN__STD.bsModExtCRM)

    Return True
  End Function

  Public Overridable Function Apri(ByVal strDitta As String, ByVal bNuovo As Boolean, ByRef dsAnaz As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim strActLog As String = ""
    Try
      strDittaCorrente = strDitta
      bNuovoDaAnagen = False
      oCldAnaz.IsToSyncAnagcaTabs = False


      ''--------------------------------------
      'predispongo l'ambiente
      If lDestdomf = -1 Then
        oCldAnaz.ValCodiceDb("1", strDittaCorrente, "TABINSG", "N", "", dttInsg)
        lDestdomf = NTSCInt(dttInsg.Rows(0)!tb_destdomf)
        lDestsedel = NTSCInt(dttInsg.Rows(0)!tb_destsedel)
        lDestresan = NTSCInt(dttInsg.Rows(0)!tb_destresan)
        lDestcorr = NTSCInt(dttInsg.Rows(0)!tb_destcorr)

        '-------------------------------------------------
        'gestione di actlog
        'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
        strActLog = oCldAnaz.GetSettingBus("BS--ANAZ", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
        If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1" Else strActLog = "0"
        bScriviActlog = CBool(strActLog)
      End If

      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      dReturn = oCldAnaz.GetData(strDittaCorrente, bGesttabcont, dsAnaz)
      If dReturn = False Then Return False

      If dsAnaz.Tables("TABANAZ").Rows.Count = 0 And bNuovo = False And strDitta <> "." Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865199642872838, "Non è stata trovata nessuna anagrafica con gli estremi indicati")))
        Return False
      End If

      '--------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      If strDittaCorrente <> "." Then
        oCldAnaz.SetTableDefaultValueFromDB("TABANAZ", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("ORGANIG", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("TABESCO", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("ANAZIVA", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("TABATTI", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("TABDURI", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("ANAZMOD", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("ACCDITO", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("ACCDITM", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("ACCDITT", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("ANADITAC", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("ANADITACS", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("ANADITPA", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("ANADITACE", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("ANADITASD", dsAnaz)
        oCldAnaz.SetTableDefaultValueFromDB("ANADITASM", dsAnaz)
      End If

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      SetDefaultValue(dsAnaz)
      dsShared = dsAnaz

      EscoSetDefaultValue()
      DiceSetDefaultValue()
      DisdSetDefaultValue()
      DismSetDefaultValue()
      AnivSetDefaultValue()
      DitoSetDefaultValue()

      If bNuovo Then
        '------------------------------------
        'carico anazmod con i moduli abilitati
        If Not CreaAnazMod() Then Return False
      End If

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("TABANAZ").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("TABANAZ").ColumnChanged, AddressOf AfterColUpdate

      AddHandler dsShared.Tables("TABESCO").ColumnChanging, AddressOf EscoBeforeColUpdate
      AddHandler dsShared.Tables("TABESCO").ColumnChanged, AddressOf EscoAfterColUpdate

      AddHandler dsShared.Tables("ANADITACE").ColumnChanging, AddressOf DiceBeforeColUpdate
      AddHandler dsShared.Tables("ANADITACE").ColumnChanged, AddressOf DiceAfterColUpdate

      AddHandler dsShared.Tables("ANAZMOD").ColumnChanging, AddressOf AmodBeforeColUpdate
      AddHandler dsShared.Tables("ANAZMOD").ColumnChanged, AddressOf AmodAfterColUpdate

      AddHandler dsShared.Tables("ANADITACS").ColumnChanging, AddressOf DitsBeforeColUpdate
      AddHandler dsShared.Tables("ANADITACS").ColumnChanged, AddressOf DitsAfterColUpdate

      AddHandler dsShared.Tables("ANADITPA").ColumnChanging, AddressOf DapaBeforeColUpdate
      AddHandler dsShared.Tables("ANADITPA").ColumnChanged, AddressOf DapaAfterColUpdate

      AddHandler dsShared.Tables("ANADITAC").ColumnChanging, AddressOf DitaBeforeColUpdate
      AddHandler dsShared.Tables("ANADITAC").ColumnChanged, AddressOf DitaAfterColUpdate

      AddHandler dsShared.Tables("ANADITASD").ColumnChanging, AddressOf DisdBeforeColUpdate
      AddHandler dsShared.Tables("ANADITASD").ColumnChanged, AddressOf DisdAfterColUpdate

      AddHandler dsShared.Tables("ANADITASM").ColumnChanging, AddressOf DismBeforeColUpdate
      AddHandler dsShared.Tables("ANADITASM").ColumnChanged, AddressOf DismAfterColUpdate

      AddHandler dsShared.Tables("ANAZIVA").ColumnChanging, AddressOf AnivBeforeColUpdate
      AddHandler dsShared.Tables("ANAZIVA").ColumnChanged, AddressOf AnivAfterColUpdate

      AddHandler dsShared.Tables("ACCDITO").ColumnChanging, AddressOf DitoBeforeColUpdate
      AddHandler dsShared.Tables("ACCDITO").ColumnChanged, AddressOf DitoAfterColUpdate
      dsShared.Tables("TABANAZ").Columns.Add("xx_azcodpcca", GetType(String))
      dsShared.Tables("TABESCO").Columns.Add("xx_codescg", GetType(String))

      '--------------------------------------
      'confermo tutto
      dsShared.AcceptChanges()
      bHasChanges = False
      bEscoHasChanges = False
      bDiceHasChanges = False
      bAmodHasChanges = False
      bDitsHasChanges = False
      bDapaHasChanges = False
      bDitaHasChanges = False
      bDisdHasChanges = False
      bDismHasChanges = False
      bAnivHasChanges = False
      bAivaHasChanges = False
      bDuriHasChanges = False
      bDitoHasChanges = False
      bDitmHasChanges = False
      bDittHasChanges = False

      nAnnoTabattiAperto = 0    
      nAnnoTabduriAperto = 0
      strUserAperto = ""
      nModuloAperto = 0

      '--------------------------------------
      'se è un nuovo record inserisco la riga nuova nel datatable
      If bNuovo Then
        If dsShared.Tables("TABANAZ").Rows.Count > 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865199674122838, "Codice anagrafica già esistente")))
          Return False
        End If

        dsShared.Tables("TABANAZ").Rows.Add(dsShared.Tables("TABANAZ").NewRow)
        bHasChanges = True
        dsShared.Tables("TABANAZ").Rows(0)!codditt = strDitta
      End If

      If bNuovaAnalitica AndAlso dsShared.Tables("TABANAZ").Rows.Count > 0 Then
        NuovaCARiempiColonneUnbound()
      End If
      '------------------------------------
      'creo i DATI AGGIUNTIVI CONTABILITA' se non presenti
      If dsShared.Tables("ANADITAC").Rows.Count = 0 And strDittaCorrente <> "." Then
        dsShared.Tables("ANADITAC").Rows.Add(dsShared.Tables("ANADITAC").NewRow)
        dsShared.Tables("ANADITAC").Rows(0)!codditt = strDittaCorrente
        dsShared.Tables("ANADITAC").Rows(0)!ac_dtinipre = NTSCDate(IntSetDate("01/01/1900"))
        dsShared.Tables("ANADITAC").Rows(0)!ac_dtfinpre = NTSCDate(IntSetDate("31/12/2099"))
        dsShared.Tables("ANADITAC").Rows(0)!ac_flintbol = "S"
        dsShared.Tables("ANADITAC").Rows(0)!ac_flgiobol = "S"
        dsShared.Tables("ANADITAC").Rows(0)!ac_flrifboi = "N"
        dsShared.Tables("ANADITAC").Rows(0)!ac_tpdesagg = 8
        dsShared.Tables("ANADITAC").Rows(0)!ac_cespint = "N"
        dsShared.Tables("ANADITAC").Rows(0)!ac_percint = "S"
        dsShared.Tables("ANADITAC").Rows(0)!ac_gesived = "N"
        dsShared.Tables("ANADITAC").Rows(0)!ac_gestefcc = IIf(dsShared.Tables("TABANAZ").Rows(0)!tb_azprofes.ToString = "S", "N", "S")
        dsShared.Tables("ANADITAC").Rows(0)!ac_geststanz = "N"
        dsShared.Tables("ANADITAC").Rows(0)!ac_provvig2 = "S"
        dsShared.Tables("ANADITAC").Rows(0)!ac_lotti2 = "S"
      End If

      '------------------------------------
      'creo i DATI AGGIUNTIVI CESPITI se non presenti
      If dsShared.Tables("ANADITACS").Rows.Count = 0 And strDittaCorrente <> "." Then
        dsShared.Tables("ANADITACS").Rows.Add(dsShared.Tables("ANADITACS").NewRow)
        dsShared.Tables("ANADITACS").Rows(0)!codditt = strDittaCorrente
        dsShared.Tables("ANADITACS").Rows(0)!acs_cespint = "S"
        dsShared.Tables("ANADITACS").Rows(0)!acs_gruspedi = " "
        dsShared.Tables("ANADITACS").Rows(0)!acs_codpuman = "5001  01"
        dsShared.Tables("ANADITACS").Rows(0)!acs_percman1 = 5
        dsShared.Tables("ANADITACS").Rows(0)!acs_percman2 = 0
        dsShared.Tables("ANADITACS").Rows(0)!acs_tipoamcont = "C"
      End If

      '------------------------------------
      'creo i DATI AGGIUNTIVI PARCELLAZIONE se non presenti
      If dsShared.Tables("ANADITPA").Rows.Count = 0 And strDittaCorrente <> "." Then
        dsShared.Tables("ANADITPA").Rows.Add(dsShared.Tables("ANADITPA").NewRow)
        dsShared.Tables("ANADITPA").Rows(0)!codditt = strDittaCorrente
      End If

      '--------------------------------------
      'compilo la descrizione dei moduli
      CompletaAnazMod()
      CompletaAccditm()
      CompletaAccditt()

      '--------------------------------------
      'Controllo di coerenza per opzione/gestabcont
      If oCldAnaz.CheckCoerenzaGestTabcont(strDittaCorrente, bGesttabcont) Then
        If bGesttabcont = True Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865199700685338, "Attenzione: L'impostazione della scelta 'Gestione tabella Contatti'" & vbCrLf & "(BUSINESS/OPZIONI/GestTabcont)" & vbCrLf & "non è in linea con il contenuto della tabella 'Organizzazione'" & vbCrLf & "(esistono alucuni records della tabella 'Organizzazione' che non hanno il codice contatto impostato)")))
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865199728341588, "Attenzione: L'impostazione della scelta 'Gestione tabella Contatti'" & vbCrLf & "(BUSINESS/OPZIONI/GestTabcont)" & vbCrLf & "non è in linea con il contenuto della tabella 'Organizzazione'" & vbCrLf & "(esistono alucuni records della tabella 'Organizzazione' che hanno il codice contatto impostato)")))
        End If
      End If

      bNew = bNuovo

      CorreggiAnazMod()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CaricaColonneUnbound(ByRef dtrIn As DataRow) As Boolean
    '------------------------------------
    'viene eseguita ad ogni cambio di record
    Dim dtrS As DataRowState = Nothing
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      dtrS = dtrIn.RowState

      '---------------------------------------
      'carico le destinazioni diverse e le altre tabelle collegate
      If Not CaricaAnazul(dtrIn) Then Return False

      '---------------------------------------
      If NTSCInt(dtrIn!tb_azcodabi) <> 0 Then
        oCldAnaz.ValCodiceDb(NTSCInt(dtrIn!tb_azcodabi).ToString, strDittaCorrente, "ABI", "N", strTmp)
        dtrIn!xx_azcodabi = strTmp
      Else
        dtrIn!xx_azcodabi = ""
      End If

      If NTSCInt(dtrIn!tb_azcodcab) <> 0 Then
        oCldAnaz.ValCodiceDb(NTSCInt(dtrIn!tb_azcodcab).ToString, strDittaCorrente, "CAB", "N", strTmp, Nothing, NTSCInt(dtrIn!tb_azcodabi).ToString)
        dtrIn!xx_azcodcab = strTmp
      Else
        dtrIn!xx_azcodcab = ""
      End If

      If NTSCInt(dtrIn!tb_natura) <> 0 Then
        oCldAnaz.ValCodiceDb(NTSCInt(dtrIn!tb_natura).ToString, strDittaCorrente, "TABNGIU", "N", strTmp)
        dtrIn!xx_natura = strTmp
      Else
        dtrIn!xx_natura = ""
      End If

      If NTSCInt(dtrIn!tb_azcodrtac) <> 0 Then
        oCldAnaz.ValCodiceDb(NTSCInt(dtrIn!tb_azcodrtac).ToString, strDittaCorrente, "TABRTAC", "N", strTmp)
        dtrIn!xx_azcodrtac = strTmp
      Else
        dtrIn!xx_azcodrtac = ""
      End If

      If NTSCInt(dtrIn!tb_azcodstud) <> 0 Then
        oCldAnaz.ValCodiceDb(NTSCInt(dtrIn!tb_azcodstud).ToString, strDittaCorrente, "TABSTUD", "N", strTmp)
        dtrIn!xx_azcodstud = strTmp
      Else
        dtrIn!xx_azcodstud = ""
      End If

      If NTSCInt(dtrIn!tb_azcodgrua) <> 0 Then
        oCldAnaz.ValCodiceDb(NTSCInt(dtrIn!tb_azcodgrua).ToString, strDittaCorrente, "TABGRUA", "N", strTmp)
        dtrIn!xx_azcodgrua = strTmp
      Else
        dtrIn!xx_azcodgrua = ""
      End If

      If NTSCInt(dtrIn!tb_mascli_1) <> 0 Then
        oCldAnaz.ValCodiceDb(NTSCInt(dtrIn!tb_mascli_1).ToString, strDittaCorrente, "TABMAST", "N", strTmp, Nothing, dtrIn!tb_azcodpcon.ToString)
        dtrIn!xx_mascli_1 = strTmp
      Else
        dtrIn!xx_mascli_1 = ""
      End If

      If NTSCInt(dtrIn!tb_masfor_1) <> 0 Then
        oCldAnaz.ValCodiceDb(NTSCInt(dtrIn!tb_masfor_1).ToString, strDittaCorrente, "TABMAST", "N", strTmp, Nothing, dtrIn!tb_azcodpcon.ToString)
        dtrIn!xx_masfor_1 = strTmp
      Else
        dtrIn!xx_masfor_1 = ""
      End If

      If NTSCStr(dtrIn!tb_azcoddpr) <> "" Then
        oCldAnaz.ValCodiceDb(NTSCStr(dtrIn!tb_azcoddpr), NTSCStr(dtrIn!tb_azcoddpr), "TABANAZ", "S", strTmp)
        dtrIn!xx_azcoddpr = strTmp
      Else
        dtrIn!xx_azcoddpr = ""
      End If

      If NTSCStr(dtrIn!tb_azcoddpr) <> "" Then
        oCldAnaz.ValCodiceDb(NTSCStr(dtrIn!tb_azcoddpr), NTSCStr(dtrIn!tb_azcoddpr), "TABANAZ", "S", strTmp)
        dtrIn!xx_azcoddpr = strTmp
      Else
        dtrIn!xx_azcoddpr = ""
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

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function CaricaAnazul(ByRef dtrIn As DataRow) As Boolean
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Try
      '---------------------------------------
      'ottengo ANAZUL
      If Not oCldAnaz.GetDataAnazul(strDittaCorrente, dsShared) Then Return False
      oCldAnaz.SetTableDefaultValueFromDB("ANAZUL", dsShared)

      'in nuovo salvo sempre le eventuali ANAZUL
      If bNew Then
        '---------------------------------
        'se nuovo da ANGAGEN devo tirare su anche le destinazioni diverse di destgen
        If NTSCInt(dtrIn!tb_azcodanag) <> 0 Then
          If Not oCldAnaz.GetDestgen(NTSCInt(dtrIn!tb_azcodanag), dttTmp) Then Return False
          For i = 0 To dttTmp.Rows.Count - 1
            dsShared.Tables("ANAZUL").Rows.Add()
            With dsShared.Tables("ANAZUL").Rows(dsShared.Tables("ANAZUL").Rows.Count - 1)
              !codditt = strDittaCorrente
              !ul_coddest = NTSCInt(dttTmp.Rows(i)!dd_coddest)
              !ul_nomdest = NTSCStr(dttTmp.Rows(i)!dd_nomdest)
              !ul_nomdest2 = NTSCStr(dttTmp.Rows(i)!dd_nomdest2)
              !ul_inddest = NTSCStr(dttTmp.Rows(i)!dd_inddest)
              !ul_capdest = NTSCStr(dttTmp.Rows(i)!dd_capdest)
              !ul_locdest = NTSCStr(dttTmp.Rows(i)!dd_locdest)
              !ul_prodest = NTSCStr(dttTmp.Rows(i)!dd_prodest)
              !ul_turno = NTSCStr(dttTmp.Rows(i)!dd_turno)
              !ul_telef = NTSCStr(dttTmp.Rows(i)!dd_telef)
              !ul_codfis = NTSCStr(dttTmp.Rows(i)!dd_codfis)
              !ul_pariva = NTSCStr(dttTmp.Rows(i)!dd_pariva)
              !ul_faxtlx = NTSCStr(dttTmp.Rows(i)!dd_faxtlx)
              !ul_email = NTSCStr(dttTmp.Rows(i)!dd_email)
              !ul_usaem = NTSCStr(dttTmp.Rows(i)!dd_usaem)
              !ul_stato = NTSCStr(dttTmp.Rows(i)!dd_stato)
              !ul_codcomu = NTSCStr(dttTmp.Rows(i)!dd_codcomu)
              !ul_codfisest = NTSCStr(dttTmp.Rows(i)!dd_codfisest)
              !ul_statofed = NTSCStr(dttTmp.Rows(i)!dd_statofed)
              !xx_codcomu = NTSCStr(dttTmp.Rows(i)!xx_codcomu)
              !xx_stato = NTSCStr(dttTmp.Rows(i)!xx_stato)
            End With
          Next
          dsShared.Tables("ANAZUL").AcceptChanges()
          dttTmp.Clear()
        End If    'If lAnagen <> 0 Then
      End If    'If NTSCInt(dtrIn!an_codanag) <> 0 Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function


  Public Overridable Function CreaAnazMod() As Boolean
    Dim i As Integer = 0
    Try
      'Riempie ANAZMOD prelevando i dati da TABINSG quando creo una nuova ditta
      For i = 1 To 60
        dsShared.Tables("ANAZMOD").Rows.Add(dsShared.Tables("ANAZMOD").NewRow())
        With dsShared.Tables("ANAZMOD").Rows(dsShared.Tables("ANAZMOD").Rows.Count - 1)
          !codditt = strDittaCorrente
          !am_modulo = i
          !am_abilit = "S"
          !am_aliasditta = DBNull.Value
          !am_pwd = DBNull.Value
          !xx_desmodulo = DBNull.Value
          !xx_abinsg = "S"
        End With
      Next
      For i = 101 To 160
        dsShared.Tables("ANAZMOD").Rows.Add(dsShared.Tables("ANAZMOD").NewRow())
        With dsShared.Tables("ANAZMOD").Rows(dsShared.Tables("ANAZMOD").Rows.Count - 1)
          !codditt = strDittaCorrente
          !am_modulo = i
          !am_abilit = "N"
          !am_aliasditta = DBNull.Value
          !am_pwd = DBNull.Value
          !xx_desmodulo = DBNull.Value
          !xx_abinsg = "N"
        End With
      Next
      For i = 201 To 260
        dsShared.Tables("ANAZMOD").Rows.Add(dsShared.Tables("ANAZMOD").NewRow())
        With dsShared.Tables("ANAZMOD").Rows(dsShared.Tables("ANAZMOD").Rows.Count - 1)
          !codditt = strDittaCorrente
          !am_modulo = i
          !am_abilit = "N"
          !am_aliasditta = DBNull.Value
          !am_pwd = DBNull.Value
          !xx_desmodulo = DBNull.Value
          !xx_abinsg = "N"
        End With
      Next

      For i = 0 To dsShared.Tables("ANAZMOD").Rows.Count - 1
        Select Case i + 1
          Case 1 To 15
            dsShared.Tables("ANAZMOD").Rows(i)!am_abilit = dttInsg.Rows(0)("tb_mod_" & NTSCStr(i + 1))
            dsShared.Tables("ANAZMOD").Rows(i)!xx_abinsg = dttInsg.Rows(0)("tb_mod_" & NTSCStr(i + 1))
          Case 16 To 30
            dsShared.Tables("ANAZMOD").Rows(i)!am_abilit = dttInsg.Rows(0)("tb_mod2_" & NTSCStr(i + 1 - 15))
            dsShared.Tables("ANAZMOD").Rows(i)!xx_abinsg = dttInsg.Rows(0)("tb_mod2_" & NTSCStr(i + 1 - 15))
          Case 31 To 45
            dsShared.Tables("ANAZMOD").Rows(i)!am_abilit = dttInsg.Rows(0)("tb_mod3_" & NTSCStr(i + 1 - 30))
            dsShared.Tables("ANAZMOD").Rows(i)!xx_abinsg = dttInsg.Rows(0)("tb_mod3_" & NTSCStr(i + 1 - 30))
          Case 46 To 60
            dsShared.Tables("ANAZMOD").Rows(i)!am_abilit = dttInsg.Rows(0)("tb_mod4_" & NTSCStr(i + 1 - 45))
            dsShared.Tables("ANAZMOD").Rows(i)!xx_abinsg = dttInsg.Rows(0)("tb_mod4_" & NTSCStr(i + 1 - 45))
          Case 61 To 90
            dsShared.Tables("ANAZMOD").Rows(i)!am_abilit = dttInsg.Rows(0)("tb_modsup_" & NTSCStr(i + 1 - 60))
            dsShared.Tables("ANAZMOD").Rows(i)!xx_abinsg = dttInsg.Rows(0)("tb_modsup_" & NTSCStr(i + 1 - 60))
          Case 91 To 120
            dsShared.Tables("ANAZMOD").Rows(i)!am_abilit = dttInsg.Rows(0)("tb_modsupext_" & NTSCStr(i + 1 - 90))
            dsShared.Tables("ANAZMOD").Rows(i)!xx_abinsg = dttInsg.Rows(0)("tb_modsupext_" & NTSCStr(i + 1 - 90))
          Case 121 To 150
            dsShared.Tables("ANAZMOD").Rows(i)!am_abilit = dttInsg.Rows(0)("tb_modptn_" & NTSCStr(i + 1 - 120))
            dsShared.Tables("ANAZMOD").Rows(i)!xx_abinsg = dttInsg.Rows(0)("tb_modptn_" & NTSCStr(i + 1 - 120))
          Case 151 To 180
            dsShared.Tables("ANAZMOD").Rows(i)!am_abilit = dttInsg.Rows(0)("tb_modptnext_" & NTSCStr(i + 1 - 150))
            dsShared.Tables("ANAZMOD").Rows(i)!xx_abinsg = dttInsg.Rows(0)("tb_modptnext_" & NTSCStr(i + 1 - 150))
        End Select
      Next
      dsShared.Tables("ANAZMOD").AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function CompletaAnazMod() As Boolean
    'aggiungo ad anazmod la descrizione dei moduli e forzo il modulo 40 = "N"
    Dim i As Integer = 0
    Try
      If strDittaCorrente = "." Then Return True

      'per compatibilità con conversioni bus 8...
      'Controlla se anazmod è di 60 record aggiorna il numero di record
      If dsShared.Tables("ANAZMOD").Rows.Count = 60 Then
        For i = 101 To 160
          dsShared.Tables("ANAZMOD").Rows.Add(dsShared.Tables("ANAZMOD").NewRow())
          With dsShared.Tables("ANAZMOD").Rows(dsShared.Tables("ANAZMOD").Rows.Count - 1)
            !codditt = strDittaCorrente
            !am_modulo = i
            !am_abilit = "N"
            !am_aliasditta = DBNull.Value
            !am_pwd = DBNull.Value
            !xx_desmodulo = DBNull.Value
            !xx_abinsg = "N"
          End With
        Next
        For i = 201 To 260
          dsShared.Tables("ANAZMOD").Rows.Add(dsShared.Tables("ANAZMOD").NewRow())
          With dsShared.Tables("ANAZMOD").Rows(dsShared.Tables("ANAZMOD").Rows.Count - 1)
            !codditt = strDittaCorrente
            !am_modulo = i
            !am_abilit = "N"
            !am_aliasditta = DBNull.Value
            !am_pwd = DBNull.Value
            !xx_desmodulo = DBNull.Value
            !xx_abinsg = "N"
          End With
        Next
      End If

      'descrizione dei moduli
      For Each dtrT As DataRow In dsShared.Tables("ANAZMOD").Rows
        dtrT!xx_desmodulo = DESMODULO(NTSCInt(dtrT!am_modulo))
        If NTSCInt(dtrT!am_modulo) = 40 Then dtrT!xx_abinsg = "N"
      Next

      dsShared.Tables("ANAZMOD").AcceptChanges()
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function DESMODULO(ByVal nModulo As Integer) As String
    Try
      DESMODULO = ".................................................."
      Select Case nModulo
        Case 1 : DESMODULO = "Contabilità Generale e semplificata"
        Case 2 : DESMODULO = "Ordini"
        Case 3 : DESMODULO = "Vendite"
        Case 4 : DESMODULO = "Provvigioni"
        Case 5 : DESMODULO = "Statistiche"
        Case 6 : DESMODULO = "Magazzino"
        Case 7 : DESMODULO = "Distinta Base"
        Case 8 : DESMODULO = "Contabilità Analitica"
        Case 9 : DESMODULO = "Cespiti"
        Case 10 : DESMODULO = "Verticale 1"
        Case 11 : DESMODULO = "Source Extender"
        Case 12 : DESMODULO = "Conai"
        Case 13 : DESMODULO = "Customer Service"
        Case 14 : DESMODULO = "Produzione"
        Case 15 : DESMODULO = "Import/Export dati"
        Case 16 : DESMODULO = "Richiesta d'acquisto"
        Case 17 : DESMODULO = "Tentata vendita"
        Case 18 : DESMODULO = "Project Management"
        Case 19 : DESMODULO = "Intrastat"
        Case 20 : DESMODULO = "Generaz. PDF da documenti"
        Case 21 : DESMODULO = "Ritenute d'acconto"
        Case 24 : DESMODULO = "Tesoreria"
        Case 26 : DESMODULO = "RSM"
        Case 27 : DESMODULO = "Configuratore di prodotto"
        Case 28 : DESMODULO = "Sistema Qualità"
        Case 31 : DESMODULO = "Configuratore su distinta neutra"
        Case 32 : DESMODULO = "Retail"
        Case 33 : DESMODULO = "Taglie e colori"
        Case 34 : DESMODULO = "Bilancio UE e nota integrativa"
        Case 35 : DESMODULO = "Black List"
        Case 36 : DESMODULO = "CRM"
        Case 37 : DESMODULO = "Picking"
        Case 38 : DESMODULO = "Parcellazione"
        Case 39 : DESMODULO = "Telematico Operazioni Rilevanti Iva"
        Case 41 : DESMODULO = "Contabilità generale EASY"
        Case 42 : DESMODULO = "Ordini EASY"
        Case 43 : DESMODULO = "Magazzino EASY"
        Case 44 : DESMODULO = "Contabilità professionisti"
        Case 45 : DESMODULO = "F24 BUSINESS"
        Case 46 : DESMODULO = "Logistica Extended"
        Case 47 : DESMODULO = "Verticale 2"
        Case 48 : DESMODULO = "International"
        Case 49 : DESMODULO = "Non Solo Shop"
        Case 50 : DESMODULO = "Commesse light"
          'Case 51: DESMODULO = "Controllo di gestione"
          'Case 52: DESMODULO = "770"
          'Case 53: DESMODULO = "Assistenza tecnica"
          'Case 54: DESMODULO = "740 + ICI + IRAP"
          'Case 55: DESMODULO = "750 + ICI + IRAP"
          'Case 56: DESMODULO = "760 + ICI + IRAP"
        Case 51 : DESMODULO = "Elenchi Iva Cli-for"
        Case 52 : DESMODULO = "Produzione Taglie colori"
        Case 55 : DESMODULO = "BUSNET Frame"
        Case 56 : DESMODULO = "Moduli Supplementari NTS"
        Case 57 : DESMODULO = "Data-Warehouse"
        Case 60 : DESMODULO = "Moduli Supplementari Partner"
        Case 101 : DESMODULO = "Inventari su palmare"
        Case 102 : DESMODULO = "Controllo ric. merce su palmare"
        Case 103 : DESMODULO = "Logistica su palmari"
          'Case 104 : DESMODULO = "Web-CRM"
          'Case 105 : DESMODULO = "Web Soft Service"
        Case 106 : DESMODULO = "E-mail integrata"
        Case 107 : DESMODULO = "Datawarehouse Extended"
        Case 108 : DESMODULO = "Contabilità Analitica DC"
        Case 109 : DESMODULO = "Budget e controllo di gestione"
        Case 110 : DESMODULO = "Desktop consolle"
        Case 111 : DESMODULO = "Tesoreria e flussi finanziari"
        Case 112 : DESMODULO = "Tesoreria Extended"
        Case 113 : DESMODULO = "Gestione punti vendita"
        Case 114 : DESMODULO = "Gestione punti vendita extended"
        Case 115 : DESMODULO = "Gestione punti vendita recovery"
        Case 116 : DESMODULO = "Gestione incassi/pagamenti differiti"
        Case 118 : DESMODULO = "Collegamento a NetPro Classic"
        Case 119 : DESMODULO = "Bus4People Extented"
        Case 120 : DESMODULO = "Analisi fattibilità impegni"
        Case 121 : DESMODULO = "Pec-mail"
        Case 122 : DESMODULO = "Dichiarazione di intenti"
        Case 123 : DESMODULO = "Collegamento e-Commerce"
        Case 124 : DESMODULO = "Business For People Guest Extended"
        Case 125 : DESMODULO = "Customer Service Map View"
        Case 126 : DESMODULO = "Contratti e Condizioni Commerciali"
        Case 128 : DESMODULO = "Crystal Reports 64bit server"
        Case 129 : DESMODULO = "Smart Business Framework"
        Case 131 : DESMODULO = "Gest. Mercati Ortofrutta"
        Case 135 : DESMODULO = "Analisi di bilancio"
        Case 201 To 230 : DESMODULO = RicercaNomePartner(nModulo)
        Case 231 To 260 : DESMODULO = RicercaNomePartner(nModulo)
      End Select
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
      Return ".................................................."
    End Try
  End Function
  Public Overridable Function RicercaNomePartner(ByVal nModulo As Integer) As String
    Dim strNomePartner As String = ""
    Dim strRootOpzReg As String = ""
    Dim strOpzReg As String = ""
    Try
      RicercaNomePartner = ".................................................."

      If nModulo >= 201 And nModulo <= 230 Then
        strRootOpzReg = "NomeModuloPtn"
        strOpzReg = strRootOpzReg & Format(nModulo - 200, "00")
        strNomePartner = NTSCStr(oCldAnaz.GetSettingBus("OPZIONI", ".", ".", strOpzReg, "..................................................", " ", ".................................................."))
      ElseIf nModulo >= 231 And nModulo <= 260 Then
        strRootOpzReg = "NomeModuloPtnExt"
        strOpzReg = strRootOpzReg & Format(nModulo - 230, "00")
        strNomePartner = NTSCStr(oCldAnaz.GetSettingBus("OPZIONI", ".", ".", strOpzReg, "..................................................", " ", ".................................................."))
      End If

      Return strNomePartner

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return ".................................................."
  End Function

  Public Overridable Function CompletaAccditm() As Boolean
    'aggiungo ad accditm la descrizione dei moduli
    Dim i As Integer = 0
    Try
      If strDittaCorrente = "." Then Return True

      For Each dtrT As DataRow In dsShared.Tables("ACCDITM").Rows
        dtrT!xx_modulo = DESMODULO(NTSCInt(dtrT!opdi_modulo))
      Next

      dsShared.Tables("ACCDITM").AcceptChanges()
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function CompletaAccditt() As Boolean
    'aggiungo ad accditm la descrizione dei moduli
    Dim nModulo As Integer = 0
    Try
      If strDittaCorrente = "." Then Return True

      For Each dtrT As DataRow In dsShared.Tables("ACCDITT").Rows
        nModulo = 0
        dtrT!xx_nomprog = oCldAnaz.GetDescrProgramma(dtrT!opdi_nomprog.ToString, nModulo)
        dtrT!xx_modulo = nModulo
      Next

      dsShared.Tables("ACCDITT").AcceptChanges()
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function NuovaDitta(ByVal strDitta As String, ByVal lAnagen As Integer, ByRef dtrA As DataRow) As Boolean
    'in creazione nuova ditta inserisce i valori obbligatori
    Dim dttTmp As New DataTable
    Try
      NuovaDitta = False
      bNuovoDaAnagen = False

      strDittaCorrente = strDitta
      dtrA!codditt = strDittaCorrente
      dtrA!tb_azcodanag = lAnagen
      dtrA!tb_azopnome = oApp.User.Nome
      dtrA!tb_ventil = IIf(bAnagen, "S", "N").ToString
      dtrA!tb_azgestscad = IIf(bAnagen, "S", "N").ToString

      '--------------------------------
      'se nuovo da anagen
      If lAnagen <> 0 Then
        bNuovoDaAnagen = True
        oCldAnaz.ValCodiceDb(lAnagen.ToString, strDittaCorrente, "ANAGEN", "N", "", dttTmp)
        With dttTmp.Rows(0)
          dtrA!tb_azrags1 = NTSCStr(!ag_descr1)
          dtrA!tb_azrags2 = NTSCStr(!ag_descr2)
          dtrA!tb_azindir = NTSCStr(!ag_indir)
          dtrA!tb_azcap = NTSCStr(!ag_cap)
          dtrA!tb_azcitta = NTSCStr(!ag_citta)
          dtrA!tb_azprov = NTSCStr(!ag_prov)
          dtrA!tb_azstato = NTSCStr(!ag_stato)
          dtrA!tb_azcodf = NTSCStr(!ag_codfis)
          dtrA!tb_azpiva = NTSCStr(!ag_pariva)
          dtrA!tb_aztelef = NTSCStr(!ag_telef)
          dtrA!tb_azfaxtlx = NTSCStr(!ag_faxtlx)
          dtrA!tb_azemail = NTSCStr(!ag_email)
          dtrA!tb_azwebsite = NTSCStr(!ag_website)
          dtrA!tb_azusaem = NTSCStr(!ag_usaem)
          dtrA!tb_azwebuid = NTSCStr(!ag_webuid)
          dtrA!tb_azwebpwd = NTSCStr(!ag_webpwd)
          dtrA!tb_azpersfg = NTSCStr(!ag_persfg)
          dtrA!tb_latitud = NTSCStr(!ag_latitud)
          dtrA!tb_longitud = NTSCStr(!ag_longitud)
          If !ag_datnasc.Equals(DBNull.Value) = False Then dtrA!tb_datnasc = NTSCDate(!ag_datnasc)
          dtrA!tb_locnasc = NTSCStr(!ag_citnasc)
          dtrA!tb_pronasc = NTSCStr(!ag_pronasc)
          dtrA!tb_azstanasc = NTSCStr(!ag_stanasc)
          dtrA!tb_azcodfisest = NTSCStr(!ag_codfisest)
          dtrA!tb_sesso = NTSCStr(!ag_sesso)
          dtrA!tb_azcell = NTSCStr(!ag_cell)
          dtrA!tb_aztitolo = NTSCStr(!ag_titolo)
          dtrA!tb_azprofes = NTSCStr(!ag_profes)
          dsShared.Tables("ANADITAC").Rows(0)!ac_gestefcc = IIf(dsShared.Tables("TABANAZ").Rows(0)!tb_azprofes.ToString = "S", "N", "S")
          dtrA!tb_azcondom = NTSCStr(!ag_condom)
          dtrA!tb_azcodcomu = NTSCStr(!ag_codcomu)
          dtrA!tb_azsiglaric = NTSCStr(!ag_siglaric)
          dtrA!tb_azcognome = NTSCStr(!ag_cognome)
          dtrA!tb_aznome = NTSCStr(!ag_nome)
          dtrA!tb_azcodcomn = NTSCStr(!ag_codcomn)
          dtrA!tb_aznazion1 = NTSCStr(!ag_nazion1)
          dtrA!tb_aznazion2 = NTSCStr(!ag_nazion2)
          dtrA!tb_azstatofed = NTSCStr(!ag_statofed)
          dtrA!tb_azsoggresi = NTSCStr(!ag_soggresi)
          dtrA!tb_azomocodice = NTSCStr(!ag_omocodice)
          dtrA!tb_azestcodiso = NTSCStr(!ag_estcodiso)
          dtrA!tb_azestpariva = NTSCStr(!ag_estpariva)
          dtrA!tb_azcodrtac = NTSCInt(!ag_codrtac)
          dtrA!tb_uldestcorr = NTSCInt(!ag_destcorr)
          dtrA!tb_uldestdomf = NTSCInt(!ag_destdomf)
          dtrA!tb_uldestresan = NTSCInt(!ag_destresan)
          dtrA!tb_uldestsedel = NTSCInt(!ag_destsedel)
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

  Public Overridable Function CheckDitteAnagen(ByVal lCodanag As Integer) As Boolean
    'ritorna true se esistono ditte collegate all'anagrafica generale passatami in input
    Try
      Return oCldAnaz.CheckDitteAnagen(lCodanag)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try

  End Function

  Public Overridable Function IsDbNoDitte() As Boolean
    'ritorna false se in tabanaz non sono presenti ditte
    Dim bOk As Boolean = False
    Dim strTmp As String = ""
    Try
      bOk = oCldAnaz.IsDbMultiDitta(strTmp)
      If bOk = False And strTmp = "" Then Return True
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


  Public Overridable Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database

      ds.Tables("TABANAZ").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("TABANAZ").Columns("tb_azrags1").DefaultValue = "..."
      ds.Tables("TABANAZ").Columns("tb_azcodpcon").DefaultValue = ""
      ds.Tables("TABANAZ").Columns("tb_mesech").DefaultValue = 12

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

  Public Overridable Function NuovaCARiempiColonneUnbound() As Boolean
    Dim bEsito As Boolean = True
    Dim i As Integer = 0
    Dim dtrTmpAnaz As DataRow
    Dim dtrTmpEsci As DataRow
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      dtrTmpAnaz = dsShared.Tables("TABANAZ").Rows(0)
      If Not NTSCStr(dtrTmpAnaz!tb_azcodpcca).Trim.Length = 0 Then
        ocldBase.ValCodiceDb(dtrTmpAnaz!tb_azcodpcca.ToString, strDittaCorrente, "TABPCCA", "S", strTmp)
        dtrTmpAnaz!xx_azcodpcca = strTmp
      Else
        oCldAnaz.IsToSyncAnagcaTabs = True
      End If

      For i = 0 To dsShared.Tables("TABESCO").Rows.Count - 1
        dtrTmpEsci = dsShared.Tables("TABESCO").Rows(i)
        If Not NTSCInt(dtrTmpEsci!tb_codescg) = 0 Then
          ocldBase.ValCodiceDb(dtrTmpEsci!tb_codescg.ToString, strDittaCorrente, "TABESCG", "N", strTmp)
          dtrTmpEsci!xx_codescg = strTmp
        End If
      Next
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      bEsito = False
    End Try
    Return bEsito
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



  Public Overridable Function TestPreSalva() As Boolean
    Dim dtrTmp As DataRow
    Dim i As Integer = 0
    Dim evnt As NTSEventArgs
    Dim strErr As String = ""
    Dim bHasRecordsAfter As Boolean = False
    Dim strTmp As String = ""
    Dim dtrT() As DataRow = Nothing

    Dim dttTmp As New DataTable
    Try
      If bNuovaAnalitica AndAlso Not CStr(dsShared.Tables("ANADITAC").Rows(0)!ac_gprincomp).Equals("S") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129357713959013295, _
        "Attenzione! Con il modulo di CA DC attivo il flag 'Ripartisci competenza costi/ricavi su mese'" & vbCrLf & _
        "contenuto in 'Dati aggiuntivi contabilità' deve essere selezionato.")))
        Return False
      End If

      dtrTmp = dsShared.Tables("TABANAZ").Rows(0)

      dtrTmp!tb_codanaz = 1 ' deve essere sempre uguale a 1

      If NTSCStr(dtrTmp!tb_azrags1).Trim = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865199752091588, "Il campo 'Ragione sociale (1a parte)' è obbligatorio")))
        Return False
      End If

      If NTSCStr(dtrTmp!tb_azcodpcon).Trim = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865199775685338, "Codice Piano dei conti obbligatorio.")))
        Return False
      End If

      If dtrTmp!tb_azpersfg.ToString = "F" And dtrTmp!tb_sesso.ToString = "S" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865199801935338, "Se tipo soggetto è 'Persona fisica' il sesso deve essere 'Maschio' o 'Femmina'")))
        Return False
      End If

      If oApp.ValutaCorrente.ToUpper = "EUR" Then
        If NTSCStr(dtrTmp!tb_azpiva) <> "" Then
          If oApp.CheckCfpi(2, NTSCStr(dtrTmp!tb_azpiva)) = False Then
            evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128865199834122838, "Partita IVA non corretta. Confermi ugualmente ? "))
            ThrowRemoteEvent(evnt)
            If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
          End If
        End If
        If NTSCStr(dtrTmp!tb_azcodf) <> "" And NTSCStr(dtrTmp!tb_azomocodice).ToUpper = "N" Then
          If oApp.CheckCfpi(3, NTSCStr(dtrTmp!tb_azcodf).ToUpper) = False Then
            evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128865199858497838, "Codice fiscale non corretto. Confermi ugualmente ? "))
            ThrowRemoteEvent(evnt)
            If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_NO Then Return False
          End If
        End If
      End If

      '------------------------
      'potrei aver cancello l'esercizio contabile dopo averlo impostato 
      If NTSCInt(dtrTmp!tb_escomp) <> 0 Then
        dtrT = dsShared.Tables("TABESCO").Select("tb_codesco = " & NTSCInt(dtrTmp!tb_escomp).ToString)
        If dtrT.Length = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865199881779088, "Esercizio contabile corrente inesistente")))
          Return False
        End If
      End If
      If NTSCInt(dtrTmp!tb_escompp) <> 0 Then
        dtrT = dsShared.Tables("TABESCO").Select("tb_codesco = " & NTSCInt(dtrTmp!tb_escompp).ToString)
        If dtrT.Length = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865199904591588, "Esercizio contabile precedente inesistente")))
          Return False
        Else
        End If
      End If
      If NTSCInt(dtrTmp!tb_escomp) <> 0 And NTSCInt(dtrTmp!tb_escompp) <> 0 And NTSCInt(dtrTmp!tb_escomp) <> NTSCInt(dtrTmp!tb_escompp) + 1 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865199927716588, "L'esercizio contabile precedente, se impostato, deve essere minore di una unità rispetto all'esercizio contabile corrente")))
        Return False
      End If
      If dtrTmp!tb_azdoppes.ToString <> "N" And NTSCInt(dtrTmp!tb_escompp) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865199951310338, "Attenzione! Se in doppio esercizio o effettuata chiusura, occorre indicre anche esercizio precedente")))
        Return False
      End If

      '------------------------
      Select Case dtrTmp!tb_azgestscad.ToString
        Case "N"
        Case "S"
          If dtrTmp!tb_ventil.ToString <> "S" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200032872838, _
                                "La gestione dello scadenziario può essere 'Sia clienti che fornitori'" & vbCrLf & _
                                "solo se la gestione delle partite è:" & vbCrLf & _
                                ". 'Sia clienti che fornitori'")))
            Return False
          End If
        Case "C"
          If (dtrTmp!tb_ventil.ToString <> "S") And (dtrTmp!tb_ventil.ToString <> "C") Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200058654088, _
                                "La gestione dello scadenziario può essere 'Solo clienti'" & vbCrLf & _
                                "solo se la gestione delle partite è:" & vbCrLf & _
                                ". 'Sia clienti che fornitori'" & vbCrLf & _
                                ". 'Solo clienti'")))
            Return False
          End If
        Case "F"
          If (dtrTmp!tb_ventil.ToString <> "S") And (dtrTmp!tb_ventil.ToString <> "F") Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200081622838, _
                                "La gestione dello scadenziario può essere 'Solo fornitori'" & vbCrLf & _
                                "solo se la gestione delle partite è:" & vbCrLf & _
                                ". 'Sia clienti che fornitori'" & vbCrLf & _
                                ". 'Solo fornitori'")))
            Return False
          End If
      End Select    'Select Case dtrTmp!tb_azgestscad.ToString

      '------------------------
      'Controlla se i dati nello storico delle variazioni è coerente con i dati in maschera
      dtrT = dsShared.Tables("ANADITVA").Select("tb_tipovar = 'N' AND " & _
                                                " tb_datini <= " & CDataSQL(dtrTmp!tb_dtulvng.ToString) & " AND " & _
                                                " tb_datfin >= " & CDataSQL(dtrTmp!tb_dtulvng.ToString))
      If dtrT.Length > 0 Then
        If NTSCInt(dtrTmp!tb_natura) <> NTSCInt(dtrT(0)!tb_vnatura) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200161779088, _
                                                "Il valore della natura giuridica è diverso da quello presente nello storico relativo." & vbCrLf & _
                                                "Variare lo storico prima di aggiornare i dati.")))
          Return False
        End If
      End If

      dtrT = dsShared.Tables("ANADITVA").Select("tb_tipovar = 'A' AND " & _
                                          " tb_datini <= " & CDataSQL(dtrTmp!tb_dtulvat.ToString) & " AND " & _
                                          " tb_datfin >= " & CDataSQL(dtrTmp!tb_dtulvat.ToString))
      If dtrT.Length > 0 Then
        If (NTSCInt(dtrTmp!tb_codattx) <> NTSCInt(dtrT(0)!tb_vcodattx)) Or (NTSCStr(dtrTmp!tb_descatt) <> NTSCStr(dtrT(0)!tb_vdescatt)) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200217091588, _
                                                "Il valore codice attività ISTAT prevalente e/o della descrizione attività sono diversi da quelli presenti nello storico relativo." & vbCrLf & _
                                                "Variare lo storico prima di aggiornare i dati.")))
          Return False
        End If
      End If

      '------------------------
      'se c'è il modulo contabilità verifico che siano stati impostati i dati necessari
      If dttInsg.Rows(0)!tb_mod_1.ToString = "S" Then
        If dsShared.Tables("ANADITAC").Rows.Count = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200244747838, "Inserire i 'Dati aggiuntivi contabilità' prima di salvare l'Anagrafica ditta.")))
          Return False
        End If
        If dsShared.Tables("TABESCO").Rows.Count = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200271779088, "Inserire i 'Dati aggiuntivi ESERCIZI CONTABILI' prima di salvare l'Anagrafica ditta.")))
          Return False
        End If
        If dsShared.Tables("ANAZIVA").Rows.Count = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200296779088, "Inserire i 'Dati aggiuntivi IVA' prima di salvare l'Anagrafica ditta.")))
          Return False
        End If
      End If    'If dttInsg.Rows(0)!tb_mod_1.ToString = "S" Then

      '------------------------
      'Se esiste il modulo dei Cespiti allora devono esistere im dati nella tabella
      'relativa ai "Dati aggiuntivi cespiti" (TTANADITACS)
      If dttInsg.Rows(0)!tb_mod_9.ToString = "S" Then
        If dsShared.Tables("ANADITACS").Rows.Count = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200333810338, "Inserire i 'Dati aggiuntivi cespiti' prima di salvare l'Anagrafica ditta.")))
          Return False
        End If
      End If

      If Not bNew Then
        If NTSCDate(dsShared.Tables("TABANAZ").Rows(0)!tb_azultagg).ToString <> oCldAnaz.GetUltaggTabanaz(strDittaCorrente) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200364747838, _
                " L'anagrafica che si sta cercando di aggiornare è stata nel frattempo modificata." & vbCrLf & _
                "Aggiornamento non possibile.")))
          Return False
        End If
      Else
        If oCldAnaz.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200387716588, "Il codice ditta è già stato utilizzato da un altro utente o sessione. Salvataggio non eseguito")))
          Return False
        End If
      End If

      dsShared.Tables("TABANAZ").Rows(0)!tb_azopnome = oApp.User.Nome
      dsShared.Tables("TABANAZ").Rows(0)!tb_azultagg = DateTime.Now

      If bNuovaAnalitica Then
        If NTSCStr(dtrTmp!tb_azcodpcca).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129157987153879345, "Codice Piano dei conti di CA obbligatorio.")))
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
  Public Overridable Function TestPreCancella() As Boolean
    Dim dttTmp As New DataTable
    Try
      If NTSCDate(dsShared.Tables("TABANAZ").Rows(0)!tb_azultagg).ToString <> oCldAnaz.GetUltaggTabanaz(strDittaCorrente) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200413029088, _
              "L'anagrafica che si sta cercando di aggiornare è stata nel frattempo modificata." & vbCrLf & _
              "Aggiornamento non possibile.")))
        Return False
      End If

      '--------------------
      'Prima di cancellare verifico se la ditta è quella predefinita: se lo è non posso procedere
      oCldAnaz.ValCodiceDb(oApp.Db.Nome, strDittaCorrente, "AZIENDE", "S", "", dttTmp)
      If dttTmp.Rows(0)!azExt.ToString.ToUpper = strDittaCorrente.ToUpper Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200435372838, _
                        "La ditta che si sta cercando di eliminare è quella predefinita per il database in uso in 'Gestione aziende/database'. " & vbCrLf & _
                        "Cancellazione non possibile.")))
        Return False
      End If
      '--------------------
      'Posso cancellare la ditta solo se non è la sola in tabanaz
      If oCldAnaz.IsDbMultiDitta("") = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200459279088, _
                            "La ditta che si sta cercando di eliminare è l'unica presente nel database in uso. " & vbCrLf & _
                            "Cancellazione non possibile.")))
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
  Public Overridable Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim dtUltaggTmp As Date = NTSCDate(dsShared.Tables("TABANAZ").Rows(0)!tb_azultagg)
    Try
      '----------------------------------------
      If bDelete Then
        If Not TestPreCancella() Then Return False

        If Not oCldAnaz.Cancella(strDittaCorrente, bScriviActlog, bsModExtCRM) Then Return False
      Else
        If Not TestPreSalva() Then Return False

        If Not oCldAnaz.Salva(dsShared, bNew, bAnagen, bScriviActlog, bCreaCAREVIS, bsModExtCRM) Then Return False
        'reimposto ultagg: potrebbe averlo modificato la routine che aggiorna anagen
        dsShared.Tables("TABANAZ").Rows(0)!tb_azultagg = NTSCDate(oCldAnaz.GetUltaggTabanaz(strDittaCorrente))
        dsShared.AcceptChanges()

        CType(oCleComm, CLELBMENU).AggiornaContattiDaDatatable(strDittaCorrente, dsShared.Tables("ORGANIG"))
      End If
      '----------------------------------------

      bNew = False
      bHasChanges = False

      Return True
    Catch ex As Exception
      dsShared.Tables("TABANAZ").Rows(0)!tb_azultagg = dtUltaggTmp   'altrimenti a risalvataggio avvisa che un altro utente ha cambiato l'anagrafica ditta ...
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function


  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChanges
    End Get
  End Property

  Public Overridable Function VariazioneDate(ByVal strTipovar As String, ByVal strDataNew As String) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      '--------------------
      '"N" = Data variazione natura giuridica          tabanaz.tb_dtulvng
      '"A" = Data ultima variazione attività           tabanaz.tb_dtulvat
      '"C" = Data ultima variazione capitale sociale   tabanaz.tb_dtulvcs

      If strTipovar = "N" And NTSCDate(strDataNew) < NTSCDate(dsShared.Tables("TABANAZ").Rows(0)!tb_dtulvng.ToString) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200481935338, "La data indicata è inferiore a quella da variare")))
        Return False
      End If

      If strTipovar = "A" And NTSCDate(strDataNew) < NTSCDate(dsShared.Tables("TABANAZ").Rows(0)!tb_dtulvat.ToString) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200504591588, "La data indicata è inferiore a quella da variare")))
        Return False
      End If

      '-------------------
      'Se esiste già il record in TTANADITVA fa semlplicemente l'Update
      bHasChanges = True
      dtrT = dsShared.Tables("ANADITVA").Select("tb_tipovar = " & CStrSQL(strTipovar) & " AND tb_datini = " & CDataSQL(strDataNew))
      If dtrT.Length > 0 Then
        Select Case strTipovar
          Case "N"
            dtrT(0)!tb_vnatura = dsShared.Tables("TABANAZ").Rows(0)!tb_natura
          Case "A"
            dtrT(0)!tb_vcodattx = dsShared.Tables("TABANAZ").Rows(0)!tb_codattx
            dtrT(0)!tb_vdescatt = dsShared.Tables("TABANAZ").Rows(0)!tb_descatt
        End Select
      Else
        'aggiorno il vecchio record
        dtrT = dsShared.Tables("ANADITVA").Select("tb_tipovar = " & CStrSQL(strTipovar) & _
                                                  " AND tb_datini <= " & CDataSQL(strDataNew) & _
                                                  " AND tb_datfin >= " & CDataSQL(strDataNew))
        For i = 0 To dtrT.Length - 1
          dtrT(i)!tb_datfin = NTSCDate(DateAdd("d", -1, NTSCDate(strDataNew)))
        Next

        'inserisco quello nuovo
        dsShared.Tables("ANADITVA").Rows.Add(dsShared.Tables("ANADITVA").NewRow)
        With dsShared.Tables("ANADITVA").Rows(dsShared.Tables("ANADITVA").Rows.Count - 1)
          !codditt = strDittaCorrente
          !tb_datini = NTSCDate(strDataNew)
          !tb_datfin = NTSCDate(IntSetDate("31/12/2099"))
          !tb_tipovar = strTipovar
          !tb_vcapdelco = 0
          !tb_vcapsotco = 0
          !tb_vcapverco = 0
          !tb_vcapvalaz = 0
          !tb_vcapnumaz = 0
          !tb_vcodattx = IIf(strTipovar = "N", " ", dsShared.Tables("TABANAZ").Rows(0)!tb_codattx)
          !tb_vdescatt = IIf(strTipovar = "N", DBNull.Value, dsShared.Tables("TABANAZ").Rows(0)!tb_descatt)
          !tb_vnatura = IIf(strTipovar = "N", dsShared.Tables("TABANAZ").Rows(0)!tb_natura, 0)
        End With

      End If    'If dtrT.Length > 0 Then

      Select Case strTipovar
        Case "N"
          dsShared.Tables("TABANAZ").Rows(0)!tb_dtulvng = strDataNew
        Case "A"
          dsShared.Tables("TABANAZ").Rows(0)!tb_dtulvat = strDataNew
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

  Public Overridable Function AggRiclassif() As Boolean
    Try

      Return oCldAnaz.AggRiclassif(strDittaCorrente, dsShared.Tables("TABANAZ").Rows(0)!tb_azcodpcon.ToString)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function GetRelease(ByVal strDitta As String, ByRef bIs15 As Boolean) As Boolean
    bIs15 = True
  End Function


  Public Overridable Function CorreggiAnazMod() As Boolean
    Dim dttInsg As New datatable
    Dim i As Integer = 0
    Dim dtrRow() As DataRow
    Dim strMod1, strMod2, strModExt, strModSup, strModPtn, strModPtnExt As String
    Try
      ' Corregge la colonna "Abilitati in Inizializzazioni globali" ...
      If Not oCldAnaz.LeggiInsg(dttInsg) Then Return False

      With dsShared.Tables("ANAZMOD")
        'Moduli 1
        For i = 1 To 15
          dtrRow = .Select("am_modulo = " & i)
          If dtrRow.Length <> 0 Then dtrRow(0)!xx_abinsg = dttInsg.Rows(0)("tb_mod_" & i)
        Next
        For i = 1 To 15
          dtrRow = .Select("am_modulo = " & i + 15)
          If dtrRow.Length <> 0 Then dtrRow(0)!xx_abinsg = dttInsg.Rows(0)("tb_mod2_" & i)
        Next
        'Moduli 2
        For i = 1 To 15
          dtrRow = .Select("am_modulo = " & i + 30)
          If dtrRow.Length <> 0 Then dtrRow(0)!xx_abinsg = dttInsg.Rows(0)("tb_mod3_" & i)
        Next
        For i = 1 To 15
          dtrRow = .Select("am_modulo = " & i + 45)
          If dtrRow.Length <> 0 Then dtrRow(0)!xx_abinsg = dttInsg.Rows(0)("tb_mod4_" & i)
        Next
        'Moduli Sup
        For i = 1 To 30
          dtrRow = .Select("am_modulo = " & i + 100)
          If dtrRow.Length <> 0 Then dtrRow(0)!xx_abinsg = dttInsg.Rows(0)("tb_modsup_" & i)
        Next
        'Moduli Ext
        For i = 1 To 30
          dtrRow = .Select("am_modulo = " & i + 130)
          If dtrRow.Length <> 0 Then dtrRow(0)!xx_abinsg = dttInsg.Rows(0)("tb_modsupext_" & i)
        Next
        'Moduli Ptn
        For i = 1 To 30
          dtrRow = .Select("am_modulo = " & i + 200)
          If dtrRow.Length <> 0 Then dtrRow(0)!xx_abinsg = dttInsg.Rows(0)("tb_modptn_" & i)
        Next
        'Moduli PtnExt
        For i = 1 To 30
          dtrRow = .Select("am_modulo = " & i + 230)
          If dtrRow.Length <> 0 Then dtrRow(0)!xx_abinsg = dttInsg.Rows(0)("tb_modptnext_" & i)
        Next

        ' ... e aggiunge la colonna con i moduli presenti in chiave dentro la griglia dei "Servizi abilitati"
        strMod1 = GetSettingReg("Business", oApp.Profilo & "\ActKey", "Moduli", "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSS").ToUpper()
        strMod2 = GetSettingReg("Business", oApp.Profilo & "\ActKey", "ModuliExt", "NNNNNNNNNNNNNNNNNNNNNNNNNNNNNN").ToUpper()
        strModExt = GetSettingReg("Business", oApp.Profilo & "\ActKey", "ModuliSup", "NNNNNNNNNNNNNNNNNNNNNNNNNNNNNN").ToUpper()
        strModSup = GetSettingReg("Business", oApp.Profilo & "\ActKey", "ModuliSupExt", "NNNNNNNNNNNNNNNNNNNNNNNNNNNNNN").ToUpper()
        strModPtn = GetSettingReg("Business", oApp.Profilo & "\ActKey", "ModuliPtn", "NNNNNNNNNNNNNNNNNNNNNNNNNNNNNN").ToUpper()
        strModPtnExt = GetSettingReg("Business", oApp.Profilo & "\ActKey", "ModuliPtnExt", "NNNNNNNNNNNNNNNNNNNNNNNNNNNNNN").ToUpper()

        'Moduli 1
        For i = 1 To 30
          dtrRow = .Select("am_modulo = " & i)
          If dtrRow.Length <> 0 Then dtrRow(0)!xx_abchiave = strMod1.Substring(i - 1, 1)
        Next
        'Moduli 2
        For i = 1 To 30
          dtrRow = .Select("am_modulo = " & i + 30)
          If dtrRow.Length <> 0 Then dtrRow(0)!xx_abchiave = strMod2.Substring(i - 1, 1)
        Next
        'Moduli ext
        For i = 1 To 30
          dtrRow = .Select("am_modulo = " & i + 100)
          If dtrRow.Length <> 0 Then dtrRow(0)!xx_abchiave = strModExt.Substring(i - 1, 1)
        Next
        'Moduli Sup
        For i = 1 To 30
          dtrRow = .Select("am_modulo = " & i + 130)
          If dtrRow.Length <> 0 Then dtrRow(0)!xx_abchiave = strModSup.Substring(i - 1, 1)
        Next
        'Moduli Ptn
        For i = 1 To 30
          dtrRow = .Select("am_modulo = " & i + 200)
          If dtrRow.Length <> 0 Then dtrRow(0)!xx_abchiave = strModPtn.Substring(i - 1, 1)
        Next
        'Moduli ptnext
        For i = 1 To 30
          dtrRow = .Select("am_modulo = " & i + 230)
          If dtrRow.Length <> 0 Then dtrRow(0)!xx_abchiave = strModPtnExt.Substring(i - 1, 1)
        Next
      End With

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '-------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function CheckInitGlobali() As Boolean
    Try
      Return oCldAnaz.CheckInitGlobali()

    Catch ex As Exception
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

  Public Overridable Sub BeforeColUpdate_tb_azcodpcon(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString.Trim = "" Then
        e.Row!xx_azcodpcon = ""
        e.Row!tb_mascli_1 = 0
        e.Row!tb_masfor_1 = 0
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABPCON", "S", strTmp) Then
          e.ProposedValue = e.Row!tb_azcodpcon.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200551154088, "Codice PDC inesistente")))
        Else
          e.Row!xx_azcodpcon = strTmp
          e.Row!tb_mascli_1 = 0
          e.Row!tb_masfor_1 = 0
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

  Public Overridable Sub BeforeColUpdate_tb_mascli_1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(e.ProposedValue.ToString) = 0 Then
        e.Row!xx_mascli_1 = ""
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABMAST", "N", strTmp, dttTmp, NTSCStr(e.Row!tb_azcodpcon)) Then
          e.ProposedValue = e.Row!tb_mascli_1.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200573029088, "Codice mastro clienti inesistente")))
        Else
          If dttTmp.Rows(0)!tb_tipomast.ToString <> "C" Then
            e.ProposedValue = e.Row!tb_mascli_1.ToString
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200598029088, "Codice mastro non appartenente alla tipologia clienti")))
          Else
            e.Row!xx_mascli_1 = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_masfor_1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(e.ProposedValue.ToString) = 0 Then
        e.Row!xx_masfor_1 = ""
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABMAST", "N", strTmp, dttTmp, NTSCStr(e.Row!tb_azcodpcon)) Then
          e.ProposedValue = e.Row!tb_masfor_1.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200621466588, "Codice mastro fornitori inesistente")))
        Else
          If dttTmp.Rows(0)!tb_tipomast.ToString <> "F" Then
            e.ProposedValue = e.Row!tb_mascli_1.ToString
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200642872838, "Codice mastro non appartenente alla tipologia fornitori")))
          Else
            e.Row!xx_masfor_1 = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_azcodrtac(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_azcodrtac = ""
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABRTAC", "N", strTmp) Then
          e.ProposedValue = e.Row!tb_azcodrtac.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200663654088, "Codice assog. ritenuta acconto inesistente")))
        Else
          e.Row!xx_azcodrtac = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_natura(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_natura = ""
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABNGIU", "N", strTmp) Then
          e.ProposedValue = e.Row!tb_natura.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200685372838, "Codice natura giuridica inesistente")))
        Else
          e.Row!xx_natura = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_azcodstud(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_azcodstud = ""
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTUD", "N", strTmp) Then
          e.ProposedValue = e.Row!tb_azcodstud.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200706935338, "Codice studio inesistente")))
        Else
          e.Row!xx_azcodstud = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_azcodgrua(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_azcodgrua = ""
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABGRUA", "N", strTmp) Then
          e.ProposedValue = e.Row!tb_azcodgrua.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200727872838, "Codice gruppo di aziende inesistente")))
        Else
          e.Row!xx_azcodgrua = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_codattx(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString.Trim = "" Then
        'e.Row!tb_descatt = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABATEC", "S", strTmp) Then
          'e.ProposedValue = e.Row!tb_codattx.ToString
          'ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128643275718437500, "Codice attività ISTAT prevalente inesistente")))
        Else
          If strTmp.Length > 50 Then strTmp = strTmp.Substring(0, 50)
          e.Row!tb_descatt = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_azcoddpr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_azcoddpr = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABANAZ", "S", strTmp) Then
          e.ProposedValue = e.Row!tb_azcoddpr.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200762404088, "Codice ditta 'principale' inesistente")))
        Else
          e.Row!xx_azcoddpr = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_escomp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dtrT() As DataRow = Nothing
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_escomp = ""
      Else
        dtrT = dsShared.Tables("TABESCO").Select("tb_codesco = " & e.ProposedValue.ToString)
        If dtrT.Length = 0 Then
          e.ProposedValue = e.Row!tb_escomp.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200782247838, "Esercizio contabile corrente inesistente")))
        Else
          e.Row!xx_escomp = dtrT(0)!tb_desesco.ToString
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

  Public Overridable Sub BeforeColUpdate_tb_escompp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dtrT() As DataRow = Nothing
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_escompp = ""
      Else
        dtrT = dsShared.Tables("TABESCO").Select("tb_codesco = " & e.ProposedValue.ToString)
        If dtrT.Length = 0 Then
          e.ProposedValue = e.Row!tb_escompp.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200801154088, "Esercizio contabile precedente inesistente")))
        Else
          e.Row!xx_escompp = dtrT(0)!tb_desesco.ToString
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

  Public Overridable Sub BeforeColUpdate_tb_azrags1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If e.ProposedValue.ToString.Trim <> "" And bNew = True Then
        If e.ProposedValue.ToString.Length > 20 Then
          e.Row!tb_azsiglaric = e.ProposedValue.ToString.Substring(0, 20)
        Else
          e.Row!tb_azsiglaric = e.ProposedValue.ToString
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

  Public Overridable Sub BeforeColUpdate_tb_azcodf(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bOk As Boolean = False
    Dim evnt As NTSEventArgs
    Try
      If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
      End If

      If e.ProposedValue.ToString <> "" Then
        If oApp.ValutaCorrente.ToUpper = "EUR" Then
          If oApp.CheckCfpi(3, e.ProposedValue.ToString) = False Then
            If NTSCStr(e.Row!tb_azomocodice) = "N" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200824747838, "Codice fiscale non corretto")))
              bOk = True
            End If
          End If
        End If

        If oCldAnaz.DittaPresenteCfPi(strDittaCorrente, "", e.ProposedValue.ToString) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200886466588, "Attenzione! Codice fiscale già esistente in altra anagrafica.")))
        End If

        If bOk = False Then
          'tre lettere
          If (Not IsNumeric(Mid(e.ProposedValue.ToString, 1, 1))) And (Not IsNumeric(Mid(e.ProposedValue.ToString, 2, 1))) And (Not IsNumeric(Mid(e.ProposedValue.ToString, 3, 1))) Then
            If NTSCStr(e.Row!tb_azpersfg) = "G" Then
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128865200930529088, "Attenzione : il codice fiscale impostato appartiene ad una persona fisica ed il campo 'tipo soggetto' non è 'persona fisica'. Vuoi aggiornare il campo 'tipo soggetto' ?"))
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then e.Row!tb_azpersfg = "F"
            End If
            ' 11 cifre
          ElseIf (IsNumeric(e.ProposedValue.ToString) And Len(e.ProposedValue.ToString) = 11) Then
            If NTSCStr(e.Row!tb_azpersfg) = "F" Then
              evnt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128865200952404088, "Attenzione : il codice fiscale impostato appartiene ad una persona giuridica ed il campo 'tipo soggetto' non è 'persona giuridica'. Vuoi aggiornare il campo 'tipo soggetto' ?"))
              ThrowRemoteEvent(evnt)
              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then e.Row!tb_azpersfg = "G"
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

  Public Overridable Sub BeforeColUpdate_tb_azpiva(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
        e.ProposedValue = e.ProposedValue.ToString.ToUpper
      End If

      If e.ProposedValue.ToString <> "" Then
        If oApp.ValutaCorrente.ToUpper = "EUR" Then
          If oApp.CheckCfpi(2, e.ProposedValue.ToString) = False Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200974122838, "Partita IVA non corretta")))
          End If
        End If

        If oCldAnaz.DittaPresenteCfPi(strDittaCorrente, e.ProposedValue.ToString, "") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865200995997838, "Attenzione! Partita IVA già esistente in altra anagrafica.")))
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

  Public Overridable Sub BeforeColUpdate_tb_azprov(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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

  Public Overridable Sub BeforeColUpdate_tb_azpronasc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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

  Public Overridable Sub BeforeColUpdate_tb_azcodcomu(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_azcodcomu = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "COMUNI", "S", strTmp, dttTmp) Then
          e.ProposedValue = e.Row!tb_azcodcomu.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201017404088, "Codice comune inesistente")))
        Else
          e.Row!xx_azcodcomu = strTmp
          e.Row!tb_azcap = NTSCStr(dttTmp.Rows(0)!co_cap)
          e.Row!tb_azcitta = NTSCStr(dttTmp.Rows(0)!co_denom)
          e.Row!tb_azprov = NTSCStr(dttTmp.Rows(0)!co_prov)
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

  Public Overridable Sub BeforeColUpdate_tb_azcodcomn(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_azcodcomn = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "COMUNI", "S", strTmp, dttTmp) Then
          e.ProposedValue = e.Row!tb_azcodcomn.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201038185338, "Codice comune di nascita inesistente")))
        Else
          e.Row!xx_azcodcomn = strTmp
          e.Row!tb_locnasc = NTSCStr(dttTmp.Rows(0)!co_denom)
          e.Row!tb_pronasc = NTSCStr(dttTmp.Rows(0)!co_prov)
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

  Public Overridable Sub BeforeColUpdate_tb_azstato(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = " " Then e.ProposedValue = ""
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_azstato = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.Row!xx_azstato = strTmp
        Else
          e.Row!xx_azstato = ""
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

  Public Overridable Sub BeforeColUpdate_tb_azstanasc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = " " Then e.ProposedValue = ""
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_azstanasc = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.Row!xx_azstanasc = strTmp
        Else
          e.Row!xx_azstanasc = ""
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

  Public Overridable Sub BeforeColUpdate_tb_aznazion1(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = " " Then e.ProposedValue = ""
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_aznazion1 = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.Row!xx_aznazion1 = strTmp
        Else
          e.Row!xx_aznazion1 = ""
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

  Public Overridable Sub BeforeColUpdate_tb_aznazion2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = " " Then e.ProposedValue = ""
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_aznazion2 = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.Row!xx_aznazion2 = strTmp
        Else
          e.Row!xx_aznazion2 = ""
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

  Public Overridable Sub BeforeColUpdate_tb_azcodabi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) <> 0 Then
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ABI", "N", strTmp) Then
          e.ProposedValue = 0
          e.Row!xx_azcodabi = ""
          e.Row!tb_azcab = 0
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201060372838, "Codice abi non corretto")))
        Else
          e.Row!xx_azcodabi = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_azcodcab(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) <> 0 Then
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "CAB", "N", strTmp, Nothing, e.Row!tb_azcodabi.ToString) Then
          e.ProposedValue = 0
          e.Row!xx_azcodcab = ""
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201081779088, "Codice cab non corretto")))
        Else
          e.Row!xx_azcodcab = strTmp
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

  Public Overridable Sub BeforeColUpdate_tb_azcodpcca(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErrore As String = ""
    Dim strCodiceDaSalvare As String = ""
    Dim strDescr As String = ""
    Try
      strCodiceDaSalvare = e.ProposedValue.ToString.Trim.ToUpper
      If strCodiceDaSalvare.Length <> 0 Then
        '-------------------------------------------------
        'verifico se il codice esiste in tabdica e si applica alle linee
        If ocldBase.ValCodiceDb(strCodiceDaSalvare, strDittaCorrente, "TABPCCA", "S", strDescr) = True Then
          e.ProposedValue = strCodiceDaSalvare
          e.Row!xx_azcodpcca = strDescr
        Else
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129157939281398027, "Piano dei conti non esiste. Inserire un codice salvato nella tabella di PDC.")))
        End If
      Else
        e.ProposedValue = " "
        e.Row!xx_azcodpcca = ""
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

  Public Overridable Sub BeforeColUpdate_tb_codtcdc(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) <> 0 Then
        If oCldAnaz.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABTCDC", "N", strTmp) Then
          e.Row!xx_codtcdc = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129294514872601416, "Tipologia entità inesistente.")))
          e.ProposedValue = e.Row!tb_codtcdc
        End If
      Else
        e.Row!xx_codtcdc = ""
      End If


    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub BeforeColUpdate_tb_azcodcaf(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) <> 0 Then
        If oCldAnaz.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABBANC", "N", strTmp) Then
          e.Row!xx_azcodcaf = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129791442438493604, "codice Nostra banca inesistente.")))
          e.ProposedValue = e.Row!tb_azcodcaf
        End If
      Else
        e.Row!xx_azcodcaf = ""
      End If

    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------------------------------------------------------
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

  Public Overridable Sub AfterColUpdate_tb_azprofes(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      dsShared.Tables("ANADITAC").Rows(0)!ac_gestefcc = IIf(dsShared.Tables("TABANAZ").Rows(0)!tb_azprofes.ToString = "S", "N", "S")

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

#Region "FUNZIONI PER FRM__NUOV"
  Public Overridable Function TestAnagen(ByVal lCodAnag As Integer, ByRef strDesanag As String) As Boolean
    '-------------------------------------
    'controllo se il mastro passatomi esiste: se si ritorno anche la descrizione
    Dim dttTmp As New DataTable
    Try
      strDesanag = ""

      oCldAnaz.ValCodiceDb(lCodAnag.ToString, "", "ANAGEN", "N", strDesanag, dttTmp)
      If dttTmp.Rows.Count = 0 Then Return False

      Return True

    Catch ex As Exception
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

#Region "FUNZIONI PER BN__DESG"

  Public Overridable Function DesgSetDataTable(ByVal strDitta As String, ByRef dttAnazul As DataTable) As Boolean
    Try
      dttAnazulEx = dttAnazul

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dttAnazulEx.ColumnChanging, AddressOf DesgBeforeColUpdate
      AddHandler dttAnazulEx.ColumnChanged, AddressOf DesgAfterColUpdate

      Return True

    Catch ex As Exception
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

      dttAnazulEx.Columns("codditt").DefaultValue = strDittaCorrente
      dttAnazulEx.Columns("ul_nomdest").DefaultValue = " "
      dttAnazulEx.Columns("ul_usaem").DefaultValue = "S"

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
      dttAnazulEx.Rows.Add(dttAnazulEx.NewRow)
      If lCodDestNew <> 0 Then
        dttAnazulEx.Rows(dttAnazulEx.Rows.Count - 1)!ul_coddest = lCodDestNew
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
      dttAnazulEx.Select(strFilter)(nRow).RejectChanges()
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function DesgTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dttAnazulEx.Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!ul_coddest) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128275056739278000, "Il codice destinazione è obbligatorio")))
          Return False
        End If

        If NTSCStr(dtrTmp(i)!ul_nomdest).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128272475736600000, "Il campo 'Ragione sociale (1a parte)' è obbligatorio")))
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

      dttAnazulEx.AcceptChanges()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
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

  Public Overridable Sub DesgBeforeColUpdate_ul_coddest(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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

      If dttAnazulEx.Rows.Count > 1 Then
        dtrTmp = dttAnazulEx.Select("ul_coddest = " & e.ProposedValue.ToString())
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

  Public Overridable Sub DesgBeforeColUpdate_ul_codcomu(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_codcomu = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "COMUNI", "S", strTmp, dttTmp) Then
          e.ProposedValue = e.Row!ul_codcomu.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128273258125802000, "Codice comune inesistente")))
        Else
          e.Row!xx_codcomu = strTmp
          e.Row!ul_capdest = NTSCStr(dttTmp.Rows(0)!co_cap)
          e.Row!ul_locdest = NTSCStr(dttTmp.Rows(0)!co_denom)
          e.Row!ul_prodest = NTSCStr(dttTmp.Rows(0)!co_prov)
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

  Public Overridable Sub DesgBeforeColUpdate_ul_stato(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = " " Then e.ProposedValue = ""
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_stato = ""
      Else
        If e.ProposedValue.ToString <> e.ProposedValue.ToString.ToUpper Then
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
        End If
        If oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
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
#End Region

#Region "FUNZIONI PER FRM__ESCI"
  Public Overridable Sub EscoSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dsShared.Tables("TABESCO").Columns("codditt").DefaultValue = strDittaCorrente
      dsShared.Tables("TABESCO").Columns("tb_desesco").DefaultValue = "."
      dsShared.Tables("TABESCO").Columns("tb_dtineser").DefaultValue = NTSCDate(IntSetDate("01/01/1900"))
      dsShared.Tables("TABESCO").Columns("tb_dtfieser").DefaultValue = NTSCDate(IntSetDate("31/12/2099"))

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

  Public Overridable Sub EscoNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("TABESCO").Rows.Add(dsShared.Tables("TABESCO").NewRow)
      bHasChanges = True
      bEscoHasChanges = True
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

  Public Overridable Function EscoRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("TABESCO").Select(strFilter)(nRow).RejectChanges()
      bEscoHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function EscoTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("TABESCO").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!tb_codesco) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201546154088, "Inserire un codice diverso da 0 nell' esercizio contabile")))
          Return False
        End If
        If bNuovaAnalitica Then
          Dim evnt As NTSEventArgs = New NTSEventArgs(CLN__STD.ThMsg.MSG_NOYES, _
              oApp.Tr(Me, 129161390550823748, "La ditta appartiene ad un gruppo aziendale." & vbCrLf & _
              "Vuoi collegare l'esercizio ditta all'esercizio aziendale?"))
          If NTSCInt(dtrTmp(i)!tb_codescg) = 0 Then
            If NTSCInt(dsShared.Tables("TABANAZ").Rows(0)!tb_azcodgrua) <> 0 Then
              ThrowRemoteEvent(evnt)

              If evnt.RetValue = CLN__STD.ThMsg.RETVALUE_YES Then
                Return False
              End If

            End If
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

  Public Overridable Function EscoSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not EscoTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      dsShared.Tables("TABESCO").AcceptChanges()
      bEscoHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property EscoRecordIsChanged() As Boolean
    Get
      Return bEscoHasChanges
    End Get
  End Property

  Public Overridable Sub EscoBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strEscoPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "EscoBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub EscoBeforeColUpdate_tb_codesco(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try

      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = dsShared.Tables("TABESCO").Select("tb_codesco = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201734904088, "Il codice inserito è già stato utilizzato. Inserire un codice non utilizzato")))
        Return
      End If

      If NTSCInt(e.ProposedValue) > 1900 And NTSCInt(e.ProposedValue) < 2100 Then
        e.Row!tb_desesco = "Anno " & NTSCInt(e.ProposedValue).ToString
        If NTSCDate(e.Row!tb_dtineser) = NTSCDate(IntSetDate("01/01/1900")) Then
          e.Row!tb_dtineser = NTSCDate(IntSetDate("01/01/" & NTSCInt(e.ProposedValue).ToString))
        End If
        If NTSCDate(e.Row!tb_dtfieser) = NTSCDate(IntSetDate("31/12/2099")) Then
          e.Row!tb_dtfieser = NTSCDate(IntSetDate("31/12/" & NTSCInt(e.ProposedValue).ToString))
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

  Public Overridable Sub EscoBeforeColUpdate_tb_numestr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_numestr = ""
        Return
      End If

      dtrTmp = e.Row.Table.Select("tb_codesco = " & e.ProposedValue.ToString)
      If dtrTmp.Length = 0 Then
        e.ProposedValue = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128644906681406250, "Esercizio contabile inesistente")))
      Else
        e.Row!xx_numestr = dtrTmp(0)!tb_desesco
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

  Public Overridable Sub EscoBeforeColUpdate_tb_forfctre(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_forfctre = ""
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABDFOR", "N", strTmp) Then
          e.ProposedValue = e.Row!tb_forfctre.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128644909958125000, "Codice forfait inesistente")))
        Else
          e.Row!xx_forfctre = strTmp
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

  Public Overridable Sub EscoBeforeColUpdate_tb_cdtsitso(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_cdtsitso = ""
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSISO", "N", strTmp) Then
          e.ProposedValue = e.Row!tb_cdtsitso.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128644910671875000, "Codice situazione società inesistente")))
        Else
          e.Row!xx_cdtsitso = strTmp
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

  Public Overridable Sub EscoBeforeColUpdate_tb_codescg(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErrore As String = ""
    Dim intCodiceDaSalvare As Integer = 0
    Dim dttTmp As New DataTable
    Try
      intCodiceDaSalvare = NTSCInt(e.ProposedValue)
      If intCodiceDaSalvare <> 0 Then
        '-------------------------------------------------
        'verifico se il codice esiste in tabescg e si applica alle linee
        If ocldBase.ValCodiceDb(e.ProposedValue.ToString.Trim, strDittaCorrente, "TABESCG", "N", "", dttTmp) = True _
       AndAlso NTSCInt(dttTmp.Rows(0)!tb_codgrua) = NTSCInt(dsShared.Tables("TABANAZ").Rows(0)!tb_azcodgrua) Then
          e.ProposedValue = intCodiceDaSalvare
          e.Row!xx_codescg = dttTmp.Rows(0)!tb_desescg
          e.Row!tb_codgrua = dttTmp.Rows(0)!tb_codgrua
        Else
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129161583662758825, "Codice esercizio a livello di gruppo aziende non è valido.")))
        End If
      Else
        e.ProposedValue = 0
        e.Row!xx_codescg = " "
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

  Public Overridable Sub EscoAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strEscoPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strEscoPrevCelValue = strEscoPrevCelValue.Remove(strEscoPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      bEscoHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "EscoAfterColUpdate_" & e.Column.ColumnName.ToLower
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

#Region "FUNZIONI PER FRMCFDICE"
  Public Overridable Sub DiceSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dsShared.Tables("ANADITACE").Columns("codditt").DefaultValue = strDittaCorrente

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

  Public Overridable Sub DiceNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("ANADITACE").Rows.Add(dsShared.Tables("ANADITACE").NewRow)
      bHasChanges = True
      bDiceHasChanges = True
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

  Public Overridable Function DiceRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("ANADITACE").Select(strFilter)(nRow).RejectChanges()
      bDiceHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function DiceTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("ANADITACE").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!ae_numesco) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201567091588, "Inserire un codice diverso da 0 nell' esercizio contabile")))
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

  Public Overridable Function DiceSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not DiceTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      dsShared.Tables("ANADITACE").AcceptChanges()
      bDiceHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property DiceRecordIsChanged() As Boolean
    Get
      Return bDiceHasChanges
    End Get
  End Property

  Public Overridable Sub DiceBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strDicePrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DiceBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub DiceBeforeColUpdate_ae_numesco(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try
      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = dsShared.Tables("TABESCO").Select("tb_codesco = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length = 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128644982064687500, "Esercizio contabile inesistente")))
        Return
      End If

      dtrTmp = dsShared.Tables("ANADITACE").Select("ae_numesco = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128644982049843750, "Il codice inserito è già stato utilizzato. Inserire un codice non utilizzato")))
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

  Public Overridable Sub DiceAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strDicePrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strDicePrevCelValue = strDicePrevCelValue.Remove(strDicePrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      bDiceHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DiceAfterColUpdate_" & e.Column.ColumnName.ToLower
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

#Region "FUNZIONI PER FRM__AMOD"
  Public Overridable Function AmodRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("ANAZMOD").Select(strFilter)(nRow).RejectChanges()
      bAmodHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function AmodTestPreSalva() As Boolean
    'Dim dtrTmp() As DataRow
    'Dim i As Integer = 0
    Try
      'dtrTmp = dsShared.Tables("ANAZMOD").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)

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

  Public Overridable Function AmodSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not AmodTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      dsShared.Tables("ANAZMOD").AcceptChanges()
      bAmodHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property AmodRecordIsChanged() As Boolean
    Get
      Return bAmodHasChanges
    End Get
  End Property

  Public Overridable Sub AmodBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strDicePrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AmodBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub AmodBeforeColUpdate_am_abilit(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If e.ProposedValue.ToString = "S" And e.Row!xx_abinsg.ToString = "N" Then
        e.ProposedValue = "N"
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647293564531250, "Non è possibile abilitare moduli non abilitati in 'inizializzazioni comuni/globali'")))
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

  Public Overridable Sub AmodAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strDicePrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strDicePrevCelValue = strDicePrevCelValue.Remove(strDicePrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      bAmodHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AmodAfterColUpdate_" & e.Column.ColumnName.ToLower
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

#Region "FUNZIONI PER FRM__DITS"
  Public Overridable Function CespitiMovimentati() As Boolean
    Try

      Return oCldAnaz.CespitiMovimentati(strDittaCorrente)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function DitsRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("ANADITACS").Select(strFilter)(nRow).RejectChanges()
      bDitsHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function DitsTestPreSalva() As Boolean
    'Dim dtrTmp() As DataRow
    'Dim i As Integer = 0
    Try
      'dtrTmp = dsShared.Tables("ANADITACS").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      'For i = 0 To dtrTmp.Length - 1
      '  If NTSCInt(dtrTmp(i)!tb_codDits) = 0 Then
      '    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201636154088, "Inserire un codice diverso da 0 nell' esercizio contabile")))
      '    Return False
      '  End If
      'Next

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

  Public Overridable Function DitsSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not DitsTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      dsShared.Tables("ANADITACS").AcceptChanges()
      bDitsHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property DitsRecordIsChanged() As Boolean
    Get
      Return bDitsHasChanges
    End Get
  End Property

  Public Overridable Sub DitsBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strDitsPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DitsBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub DitsBeforeColUpdate_acs_gruspedi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = "" Then e.ProposedValue = " "
      If NTSCStr(e.ProposedValue).Trim = "" Then
        e.Row!xx_gruspedi = ""
        Return
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSPCE", "S", strTmp) Then
          e.ProposedValue = e.Row!acs_gruspedi.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201806154088, "Codice Gruppo/specie cespiti inesistente")))
        Else
          e.Row!xx_gruspedi = strTmp
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

  Public Overridable Sub DitsBeforeColUpdate_acs_codpuman(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = "" Then e.ProposedValue = " "
      If NTSCStr(e.ProposedValue).Trim = "" Then
        e.Row!xx_codpuman = ""
        Return
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABPUCE", "S", strTmp) Then
          e.ProposedValue = e.Row!acs_codpuman.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201822872838, "Codice Punto manutenzioni e riparazioni inesistente")))
        Else
          e.Row!xx_codpuman = strTmp
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

  Public Overridable Sub DitsAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strDitsPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strDitsPrevCelValue = strDitsPrevCelValue.Remove(strDitsPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      bDitsHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DitsAfterColUpdate_" & e.Column.ColumnName.ToLower
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

#Region "FUNZIONI PER FRM__DAPA"

  Public Overridable Function DapaRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("ANADITPA").Select(strFilter)(nRow).RejectChanges()
      bDapaHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function DapaTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("ANADITPA").Select(Nothing, Nothing)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!acs_cascom) = 0 And dtrTmp(i)!acs_appcomm.ToString = "S" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647347330625000, "Se selezionato 'Applica cassa commercialisti' deve essere indicata anche la percentuale relativa.")))
          Return False
        End If

        If NTSCInt(dtrTmp(i)!acs_spegen) = 0 And dtrTmp(i)!acs_appspegen.ToString = "S" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647347530468750, "Se selezionato 'Applica spese generali' deve essere indicata anche la percentuale relativa.")))
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

  Public Overridable Function DapaSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not DapaTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      dsShared.Tables("ANADITPA").AcceptChanges()
      bDapaHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property DapaRecordIsChanged() As Boolean
    Get
      Return bDapaHasChanges
    End Get
  End Property

  Public Overridable Sub DapaBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strDapaPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DapaBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub DapaBeforeColUpdate_acs_codvpar(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = " " Then e.ProposedValue = " "
      If NTSCStr(e.ProposedValue).Trim = "" Then
        e.Row!xx_codvpar = ""
        Return
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABVPAR", "S", strTmp) Then
          e.ProposedValue = e.Row!acs_codvpar.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647350869531250, "Voce parcellazione cassa commercialisti inesistente")))
        Else
          e.Row!xx_codvpar = strTmp
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

  Public Overridable Sub DapaBeforeColUpdate_acs_codvparsg(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = " " Then e.ProposedValue = " "
      If NTSCStr(e.ProposedValue).Trim = "" Then
        e.Row!xx_codvparsg = ""
        Return
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABVPAR", "S", strTmp) Then
          e.ProposedValue = e.Row!acs_codvparsg.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201894747838, "Voce parcellazione spese generali inesistente")))
        Else
          e.Row!xx_codvparsg = strTmp
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

  Public Overridable Sub DapaBeforeColUpdate_acs_codvparsa(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = " " Then e.ProposedValue = " "
      If NTSCStr(e.ProposedValue).Trim = "" Then
        e.Row!xx_codvparsa = ""
        Return
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABVPAR", "S", strTmp) Then
          e.ProposedValue = e.Row!acs_codvparsa.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201918185338, "Voce parcellazione storno acconti inesistente")))
        Else
          e.Row!xx_codvparsa = strTmp
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

  Public Overridable Sub DapaBeforeColUpdate_acs_codtpbf(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codtpbf = ""
        Return
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABTPBF", "N", strTmp) Then
          e.ProposedValue = e.Row!acs_codtpbf.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201935685338, "Tipo bolla/fattura inesistente")))
        Else
          e.Row!xx_codtpbf = strTmp
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

  Public Overridable Sub DapaBeforeColUpdate_acs_codrtacp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codrtacp = ""
        Return
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABRTAC", "N", strTmp) Then
          e.ProposedValue = e.Row!acs_codrtacp.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201954747838, "Codice assoggettamento ritenuta d'acconto per clienti privati inesistente")))
        Else
          e.Row!xx_codrtacp = strTmp
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

  Public Overridable Sub DapaAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strDapaPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strDapaPrevCelValue = strDapaPrevCelValue.Remove(strDapaPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      bDapaHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DapaAfterColUpdate_" & e.Column.ColumnName.ToLower
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

#Region "FUNZIONI PER FRM__DITA"

  Public Overridable Function RitenuteMovimentate() As Boolean
    Try

      Return oCldAnaz.RitenuteMovimentate(strDittaCorrente)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function ProvvigioniNonPagate() As Boolean
    Try

      Return oCldAnaz.ProvvigioniNonPagate(strDittaCorrente)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function IvaMovimentata() As Boolean
    Try

      Return oCldAnaz.IvaMovimentata(strDittaCorrente)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function


  Public Overridable Function DitaRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("ANADITPA").Select(strFilter)(nRow).RejectChanges()
      bDitaHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function DitaTestPreSalva() As Boolean
    Try


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

  Public Overridable Function DitaSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not DitaTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      dsShared.Tables("ANADITPA").AcceptChanges()
      bDitaHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property DitaRecordIsChanged() As Boolean
    Get
      Return bDitaHasChanges
    End Get
  End Property

  Public Overridable Sub DitaBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strDitaPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DitaBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub DitaBeforeColUpdate_ac_cdtivapri(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_cdtivapri = ""
        Return
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCIVA", "N", strTmp) Then
          e.ProposedValue = e.Row!ac_cdtivapri.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647464662343750, "Codice IVA inesistente")))
        Else
          e.Row!xx_cdtivapri = strTmp
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
  Public Overridable Sub DitaBeforeColUpdate_ac_lotti2(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = "N" And NTSCStr(e.Row!ac_lotti2) = "S" Then
        e.ProposedValue = "S"
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513091397594095, "Una volta impostato di voler gestire i lotti Alfanumerici non è più possibile tornare alla gestione 'Lotti numerici'")))
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

  Public Overridable Sub DitaAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strDitaPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strDitaPrevCelValue = strDitaPrevCelValue.Remove(strDitaPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      bDitaHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DitaAfterColUpdate_" & e.Column.ColumnName.ToLower
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

#Region "FUNZIONI PER FRMCFDISD"
  Public Overridable Sub DisdSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dsShared.Tables("ANADITASD").Columns("codditt").DefaultValue = strDittaCorrente

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

  Public Overridable Sub DisdNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("ANADITASD").Rows.Add(dsShared.Tables("ANADITASD").NewRow)
      bHasChanges = True
      bDisdHasChanges = True
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

  Public Overridable Function DisdRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("ANADITASD").Select(strFilter)(nRow).RejectChanges()
      bDisdHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function DisdTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("ANADITASD").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!asd_numesco) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647529516406250, "Inserire un codice diverso da 0 nell' esercizio contabile")))
          Return False
        End If

        If NTSCInt(dtrTmp(i)!asd_numprogr) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647529504062500, "Inserire un codice diverso da 0 nel progressivo")))
          Return False
        End If

        'If NTSCInt(dtrTmp(i)!asd_progalm) = 0 Then
        '  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647529740156250, "Inserire un codice diverso da 0 nel progressivo rigo allegato 'M'")))
        '  Return False
        'End If
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

  Public Overridable Function DisdSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not DisdTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      dsShared.Tables("ANADITASD").AcceptChanges()
      bDisdHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property DisdRecordIsChanged() As Boolean
    Get
      Return bDisdHasChanges
    End Get
  End Property

  Public Overridable Sub DisdBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strDisdPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DisdBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub DisdBeforeColUpdate_asd_numesco(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try
      dtrTmp = dsShared.Tables("ANADITACE").Select("ae_numesco = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length = 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647538136406250, "Esercizio contabile inesistente in 'Studi di settore - dati aggiuntivi esercizi'")))
        Return
      End If

      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = dsShared.Tables("ANADITASD").Select("asd_numesco = " & e.ProposedValue.ToString & " AND asd_numprogr = " & e.Row!asd_numprogr.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647538154843750, "Esiste già una riga con Esercizio contabile e progressivo uguali a quello che si sta cercando di salvare")))
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

  Public Overridable Sub DisdBeforeColUpdate_asd_numprogr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try
      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = dsShared.Tables("ANADITASD").Select("asd_numesco = " & e.Row!asd_numesco.ToString & " AND asd_numprogr = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865201717091588, "Esiste già una riga con Esercizio contabile e progressivo uguali a quello che si sta cercando di salvare")))
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

  Public Overridable Sub DisdBeforeColUpdate_asd_coddest(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_coddest = ""
        Return
      End If

      dtrTmp = dsShared.Tables("ANAZUL").Select("ul_coddest = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length = 0 Then
        e.ProposedValue = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647525983437500, "Destinazione diversa inesistente")))
      Else
        e.Row!xx_coddest = dtrTmp(0)!ul_nomdest.ToString
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

  Public Overridable Sub DisdBeforeColUpdate_asd_ascodatt(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = "" Then e.ProposedValue = " "
      If NTSCStr(e.ProposedValue).Trim = "" Then
        e.Row!xx_ascodatt = ""
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABATEC", "S", strTmp) Then
          e.ProposedValue = e.Row!asd_ascodatt.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647528848437500, "Codice ISTAT inesistente")))
        Else
          e.Row!xx_ascodatt = strTmp
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

  Public Overridable Sub DisdAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strDisdPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strDisdPrevCelValue = strDisdPrevCelValue.Remove(strDisdPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      bDiceHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DisdAfterColUpdate_" & e.Column.ColumnName.ToLower
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

#Region "FUNZIONI PER FRMCFDISM"
  Public Overridable Sub DismSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dsShared.Tables("ANADITASD").Columns("codditt").DefaultValue = strDittaCorrente

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

  Public Overridable Sub DismNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("ANADITASM").Rows.Add(dsShared.Tables("ANADITASM").NewRow)
      bHasChanges = True
      bDismHasChanges = True
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

  Public Overridable Function DismRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("ANADITASD").Select(strFilter)(nRow).RejectChanges()
      bDismHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function DismTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("ANADITASM").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!asm_numesco) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647554165000000, "Inserire un codice diverso da 0 nell' esercizio contabile")))
          Return False
        End If

        If NTSCInt(dtrTmp(i)!asm_numprog) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647554178437500, "Inserire un codice diverso da 0 nel progressivo")))
          Return False
        End If

        'If NTSCInt(dtrTmp(i)!asd_progalm) = 0 Then
        '  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647529740156250, "Inserire un codice diverso da 0 nel progressivo rigo allegato 'M'")))
        '  Return False
        'End If
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

  Public Overridable Function DismSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not DismTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      dsShared.Tables("ANADITASM").AcceptChanges()
      bDismHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property DismRecordIsChanged() As Boolean
    Get
      Return bDismHasChanges
    End Get
  End Property

  Public Overridable Sub DismBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strDismPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DismBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub DismBeforeColUpdate_asm_numesco(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try
      dtrTmp = dsShared.Tables("ANADITACE").Select("ae_numesco = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length = 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647555815156250, "Esercizio contabile inesistente in 'Studi di settore - dati aggiuntivi esercizi'")))
        Return
      End If

      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = dsShared.Tables("ANADITASM").Select("asm_numesco = " & e.ProposedValue.ToString & " AND asm_numprog = " & e.Row!asm_numprog.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647555800625000, "Esiste già una riga con Esercizio contabile e progressivo uguali a quello che si sta cercando di salvare")))
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

  Public Overridable Sub DismBeforeColUpdate_asm_numprog(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try
      dtrTmp = dsShared.Tables("ANADITASD").Select("asd_progalm = " & e.ProposedValue.ToString & " AND asd_numesco = " & e.Row!asm_numesco.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length = 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647555776718750, "Progressivo inesistente in 'Studi di settore - dati aggiuntivi esercizi' colonna 'Numero progr.rigo all.M' per l'anno |" & e.Row!asm_numesco.ToString & "|")))
        Return
      End If

      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = dsShared.Tables("ANADITASM").Select("asm_numesco = " & e.Row!asm_numesco.ToString & " AND asm_numprog = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647555764843750, "Esiste già una riga con Esercizio contabile e progressivo uguali a quello che si sta cercando di salvare")))
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

  Public Overridable Sub DismBeforeColUpdate_asm_coddest(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_coddest = ""
        Return
      End If

      dtrTmp = dsShared.Tables("ANAZUL").Select("ul_coddest = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length = 0 Then
        e.ProposedValue = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647555969531250, "Destinazione diversa inesistente")))
      Else
        e.Row!xx_coddest = dtrTmp(0)!ul_nomdest.ToString
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

  Public Overridable Sub DismBeforeColUpdate_asm_stucodat(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = "" Then e.ProposedValue = " "
      If NTSCStr(e.ProposedValue).Trim = "" Then
        e.Row!xx_stucodat = ""
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABATEC", "S", strTmp) Then
          e.ProposedValue = e.Row!asm_stucodat.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865202214435338, "Codice ISTAT inesistente")))
        Else
          e.Row!xx_stucodat = strTmp
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

  Public Overridable Sub DismAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strDismPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strDismPrevCelValue = strDismPrevCelValue.Remove(strDismPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      bDiceHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DismAfterColUpdate_" & e.Column.ColumnName.ToLower
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

#Region "FUNZIONI PER FRM__ANIV"
  Public Overridable Sub AnivSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dsShared.Tables("ANAZIVA").Columns("codditt").DefaultValue = strDittaCorrente
      dsShared.Tables("ANAZIVA").Columns("ai_utilcred").DefaultValue = "I"

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

  Public Overridable Sub AnivNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("ANAZIVA").Rows.Add(dsShared.Tables("ANAZIVA").NewRow)
      bHasChanges = True
      bAnivHasChanges = True
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

  Public Overridable Function AnivRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("ANAZIVA").Select(strFilter)(nRow).RejectChanges()
      bAnivHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function AnivTestPreSalva() As Boolean
    'Dim dtrTmp() As DataRow
    'Dim i As Integer = 0
    Try
      'dtrTmp = dsShared.Tables("ANAZIVA").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      'For i = 0 To dtrTmp.Length - 1
      '  If NTSCInt(dtrTmp(i)!tb_codAniv) = 0 Then
      '    ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128647600075312500, "Inserire un codice diverso da 0 nell' esercizio contabile")))
      '    Return False
      '  End If
      'Next

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

  Public Overridable Function AnivSalva(ByVal bDelete As Boolean, ByVal nAnnoDelete As Integer) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not AnivTestPreSalva() Then Return False
      Else
        'devo cancellare anche tabduri e tabatti
        dtrT = dsShared.Tables("TABDURI").Select("tb_anno = " & nAnnoDelete.ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i).Delete()
        Next

        dtrT = dsShared.Tables("TABATTI").Select("tb_anno = " & nAnnoDelete.ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i).Delete()
        Next

        dsShared.Tables("TABDURI").AcceptChanges()
        dsShared.Tables("TABATTI").AcceptChanges()
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      dsShared.Tables("ANAZIVA").AcceptChanges()
      bAnivHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property AnivRecordIsChanged() As Boolean
    Get
      Return bAnivHasChanges
    End Get
  End Property

  Public Overridable Sub AnivBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strAnivPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AnivBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub AnivBeforeColUpdate_ai_codivapr(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codivapr = ""
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCIVA", "N", strTmp) Then
          e.ProposedValue = e.Row!ai_codivapr.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128648379487187500, "Codice IVA inesistente")))
        Else
          e.Row!xx_codivapr = strTmp
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

  Public Overridable Sub AnivBeforeColUpdate_ai_codditcg(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = " " Then e.ProposedValue = ""
      If NTSCStr(e.ProposedValue).Trim = "" Then
        e.Row!xx_codditcg = ""
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABANAZ", "S", strTmp) Then
          e.ProposedValue = e.Row!ai_codditcg.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865202229435338, "Codice Ditta capogruppo inesistente")))
        Else
          e.Row!xx_codditcg = strTmp
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

  Public Overridable Sub AnivAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strAnivPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strAnivPrevCelValue = strAnivPrevCelValue.Remove(strAnivPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      bAnivHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AnivAfterColUpdate_" & e.Column.ColumnName.ToLower
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

#Region "FUNZIONI PER BN__AIVA"
  Public Overridable Function AivaSetDataTable(ByVal strDitta As String, ByRef dttTabatti As DataTable) As Boolean
    Try
      dttTabattiEx = dttTabatti
      AivaSetDefaultValue()

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dttTabattiEx.ColumnChanging, AddressOf AivaBeforeColUpdate
      AddHandler dttTabattiEx.ColumnChanged, AddressOf AivaAfterColUpdate

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub AivaSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database

      dttTabattiEx.Columns("codditt").DefaultValue = strDittaCorrente
      dttTabattiEx.Columns("tb_dtinat").DefaultValue = NTSCDate(IntSetDate("01/01/1900"))
      dttTabattiEx.Columns("tb_dtfiat").DefaultValue = NTSCDate(IntSetDate("31/12/2099"))

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

  Public Overridable Sub AivaNuovo()
    Try
      AivaSetDefaultValue()

      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dttTabattiEx.Rows.Add(dttTabattiEx.NewRow)

      bHasChanges = True
      bAivaHasChanges = True
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

  Public Overridable Function AivaRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dttTabattiEx.Select(strFilter)(nRow).RejectChanges()
      bAivaHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function AivaTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dttTabattiEx.Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        dtrTmp(i)!tb_anno = nAnnoTabattiAperto

        If NTSCInt(dtrTmp(i)!tb_codatti) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128648464344218750, "Il codice attività è obbligatorio")))
          Return False
        End If

        If NTSCStr(dtrTmp(i)!tb_desatti).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128648464362031250, "Il campo 'Descrizione attività' è obbligatorio")))
          Return False
        End If

        If NTSCStr(dtrTmp(i)!tb_subforn) = "S" Then
          If NTSCStr(dtrTmp(i)!tb_atmentri) = "A" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129956427509835957, "Con selezionato 'IVA di cassa' non è possibile gestire come periodicità Iva 'annuale'")))
            Return False
          End If
          If NTSCStr(dtrTmp(i)!tb_beniusat) <> "N" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129956429923022109, "Con selezionato 'IVA di cassa' non è possibile impostare una attività di tipo 'Margine beni usati'")))
            Return False
          End If
          If NTSCStr(dtrTmp(i)!tb_74ter) <> "N" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129956430431808533, "Con selezionato 'IVA di cassa' non è possibile impostare una attività di tipo '74Ter'")))
            Return False
          End If
          If NTSCStr(dtrTmp(i)!tb_agric) <> "N" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129989984465783003, "Con selezionato 'IVA di cassa' non è possibile impostare una attività di tipo 'Agricoltura' o 'Agriturismo'.")))
            Return False
          End If
        End If    'If NTSCStr(dtrTmp(i)!tb_subforn) = "S" Then

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

  Public Overridable Function AivaSalva(ByVal bDelete As Boolean, ByVal nCodattiDeleted As Integer) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not AivaTestPreSalva() Then Return False

      Else
        'devo cancellare anche tabduri e tabatti
        dtrT = dsShared.Tables("TABDURI").Select("tb_anno = " & nAnnoTabattiAperto.ToString & " AND tb_ucodatti = " & nCodattiDeleted.ToString)
        For i = 0 To dtrT.Length - 1
          dtrT(i).Delete()
        Next
        dsShared.Tables("TABDURI").AcceptChanges()
      End If

      dttTabattiEx.AcceptChanges()
      bAivaHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property AivaRecordIsChanged() As Boolean
    Get
      Return bAivaHasChanges
    End Get
  End Property

  Public Overridable Sub AivaBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strAivaPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AivaBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub AivaBeforeColUpdate_tb_codatti(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow

    Try
      If dttTabattiEx.Rows.Count > 1 Then
        dtrTmp = dttTabattiEx.Select("tb_codatti = " & e.ProposedValue.ToString())
        If dtrTmp.Length > 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128648465008437500, "Codice attività già esistente: inserire un nuovo codice")))
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

  Public Overridable Sub AivaBeforeColUpdate_tb_codusatdef(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codusatdef = ""
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABUSAT", "N", strTmp, dttTmp) Then
          e.ProposedValue = e.Row!tb_codusatdef.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128648466505156250, "Codice bene usato inesistente")))
        Else
          e.Row!xx_codusatdef = strTmp
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

  Public Overridable Sub AivaBeforeColUpdate_tb_acodista(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If e.ProposedValue.ToString = " " Then e.ProposedValue = ""
      If e.ProposedValue.ToString = "" Then
        e.Row!xx_acodista = ""
      Else
        If oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABATEC", "S", strTmp) Then
          e.Row!xx_acodista = strTmp
        Else
          e.ProposedValue = e.Row!tb_acodista.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128649211608906250, "Codice ISTAT inesistente")))
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

  Public Overridable Sub AivaAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strAivaPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strAivaPrevCelValue = strAivaPrevCelValue.Remove(strAivaPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      bAivaHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AivaAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Function AivaBeforeCancella(ByVal nCodattiDeleted As Integer) As Boolean
    Dim dsTmp As DataSet = Nothing
    Try
      oCldAnaz.GetMoviva(strDittaCorrente, nAnnoTabattiAperto, _
                         nCodattiDeleted, dsTmp)

      If dsTmp.Tables("TABATTI").Rows.Count > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129833623052846185, "Attenzione attivitÃ  iva non cancellabile codice giÃ  utilizzato")))
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

#Region "FUNZIONI PER BN__DURI"
  Public Overridable Function DuriSetDataTable(ByVal strDitta As String, ByRef dttTabduri As DataTable) As Boolean
    Try
      dttTabduriEx = dttTabduri
      DuriSetDefaultValue()

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dttTabduriEx.ColumnChanging, AddressOf DuriBeforeColUpdate
      AddHandler dttTabduriEx.ColumnChanged, AddressOf DuriAfterColUpdate

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub DuriSetDefaultValue()
    Dim nCodatti As Integer = 0
    Dim strCodatti As String = ""
    Dim dtrTmp() As DataRow

    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dttTabduriEx.Columns("codditt").DefaultValue = strDittaCorrente
      dttTabduriEx.Columns("tb_udatreg").DefaultValue = NTSCDate(IntSetDate("01/01/1900"))
      dttTabduriEx.Columns("tb_tipoprot").DefaultValue = "0"
      '--------------------------------------------------------------------------------------------------------------
      dtrTmp = dsShared.Tables("TABATTI").Select("tb_anno = " & nAnnoTabduriAperto)
      If dtrTmp.Length = 1 Then
        nCodatti = NTSCInt(dtrTmp(0)!tb_codatti)
        strCodatti = NTSCStr(dtrTmp(0)!tb_desatti)
      End If
      '--------------------------------------------------------------------------------------------------------------
      dttTabduriEx.Columns("tb_ucodatti").DefaultValue = nCodatti
      dttTabduriEx.Columns("xx_ucodatti").DefaultValue = strCodatti

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

  Public Overridable Sub DuriNuovo()
    Try
      DuriSetDefaultValue()

      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dttTabduriEx.Rows.Add(dttTabduriEx.NewRow)

      bHasChanges = True
      bDuriHasChanges = True
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

  Public Overridable Function DuriRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dttTabduriEx.Select(strFilter)(nRow).RejectChanges()
      bDuriHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function DuriTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow

    Try
      '--------------------------------------------------------------------------------------------------------------
      dtrTmp = dttTabduriEx.Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i As Integer = 0 To dtrTmp.Length - 1
        '------------------------------------------------------------------------------------------------------------
        dtrTmp(i)!tb_anno = nAnnoTabduriAperto
        '------------------------------------------------------------------------------------------------------------
        If NTSCInt(dtrTmp(i)!tb_unumreg) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128649250716406250, _
            "Il numero registro IVA è obbligatorio")))
          Return False
        End If
        '------------------------------------------------------------------------------------------------------------
        If NTSCInt(dtrTmp(i)!tb_ucodatti) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128649214433125000, _
            "Il codice attività è obbligatorio")))
          Return False
        End If
        '------------------------------------------------------------------------------------------------------------
        If dtrTmp(i).RowState = DataRowState.Modified And _
           NTSCStr(dtrTmp(i)!tb_utipreg) = "V" Then
          If NTSCStr(dtrTmp(i)("tb_tipoprot", DataRowVersion.Original)) <> NTSCStr(dtrTmp(i)!tb_tipoprot) Then
            If oCldAnaz.TipoProtCambiato(strDittaCorrente, dtrTmp(i)) = True Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 131049393236131189, "Attenzione!" & vbCrLf & _
                "Non è possibile modificare:" & vbCrLf & _
                " . Relazione serie/numero documento - serie/numero protocollo" & vbCrLf & _
                "se già movimentato.")))
              Return False
            End If
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
        '--- Test per fattura figurativa
        '------------------------------------------------------------------------------------------------------------
        If NTSCStr(dtrTmp(i)!tb_ftfig) = "S" Then
          If NTSCStr(dtrTmp(i)!tb_utipreg) <> "V" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130580274137788060, "E' possibile spuntare 'Fattura figurativa' solo con registro di tipo 'Vendite'")))
            Return False
          End If
          If NTSCStr(dtrTmp(i)!tb_tipoprot) <> "1" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130580274137788061, "E' possibile spuntare 'Fattura figurativa' solo con 'Rel.doc./prot.vend.' uguale a 'Coincidenti'")))
            Return False
          End If
        End If
        '------------------------------------------------------------------------------------------------------------
      Next i
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

  Public Overridable Function DuriSalva(ByVal bDelete As Boolean) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not DuriTestPreSalva() Then Return False
      End If

      dttTabduriEx.AcceptChanges()
      bDuriHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property DuriRecordIsChanged() As Boolean
    Get
      Return bDuriHasChanges
    End Get
  End Property

  Public Overridable Sub DuriBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strDuriPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DuriBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub DuriBeforeColUpdate_tb_utipreg(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow

    Try
      If dttTabduriEx.Rows.Count > 1 Then
        dtrTmp = dttTabduriEx.Select("tb_unumreg = " & e.Row!tb_unumreg.ToString & " AND tb_utipreg = " & CStrSQL(e.ProposedValue))
        If dtrTmp.Length > 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865202348497838, "Codice/tipo registro IVA già esistente")))
          Return
        End If

        If e.ProposedValue.ToString = "V" Or e.ProposedValue.ToString = "C" Or e.ProposedValue.ToString = "S" Then
          e.Row!tb_tipoprot = "1"
        Else
          e.Row!tb_tipoprot = "0"
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

  Public Overridable Sub DuriBeforeColUpdate_tb_unumreg(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow

    Try
      If dttTabduriEx.Rows.Count > 1 Then
        dtrTmp = dttTabduriEx.Select("tb_unumreg = " & e.ProposedValue.ToString() & " AND tb_utipreg = " & CStrSQL(e.Row!tb_utipreg.ToString))
        If dtrTmp.Length > 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865202363810338, "Codice/tipo registro IVA già esistente")))
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

  Public Overridable Sub DuriBeforeColUpdate_tb_ucodatti(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_ucodatti = ""
        Return
      End If

      dtrTmp = dsShared.Tables("TABATTI").Select("tb_codatti = " & e.ProposedValue.ToString & " AND tb_anno = " & nAnnoTabduriAperto, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length = 0 Then
        e.ProposedValue = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128649248649062500, "Attività IVA inesistente per l'anno in analisi")))
      Else
        e.Row!xx_ucodatti = dtrTmp(0)!tb_desatti.ToString
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

  Public Overridable Sub DuriBeforeColUpdate_tb_ucoddest(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_ucoddest = ""
        Return
      End If

      dtrTmp = dsShared.Tables("ANAZUL").Select("ul_coddest = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length = 0 Then
        e.ProposedValue = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128649248185468750, "Destinazione diversa inesistente")))
      Else
        e.Row!xx_ucoddest = dtrTmp(0)!ul_nomdest.ToString
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

  Public Overridable Sub DuriBeforeColUpdate_tb_tipoprot(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) = 1 Then e.Row!tb_tiponume = "0"

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

  Public Overridable Sub DuriBeforeColUpdate_tb_tiponume(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) <> 0 And NTSCInt(e.Row!tb_tipoprot) = 1 Then
        e.ProposedValue = "0"
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128649257734062500, "'Controllo serie protocollo' è abilitabile solo se 'Relaz. doc./prot.' è impostato su 'svincolati'")))
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

  Public Overridable Function DuriBeforeColUpdate_tb_serfatt(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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
  Public Overridable Function DuriBeforeColUpdate_tb_sernoac(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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
  Public Overridable Function DuriBeforeColUpdate_tb_sernoad(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
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

  Public Overridable Sub DuriAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strDuriPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strDuriPrevCelValue = strDuriPrevCelValue.Remove(strDuriPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      bDuriHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DuriAfterColUpdate_" & e.Column.ColumnName.ToLower
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

#Region "FUNZIONI PER BN__DITO"
  Public Overridable Sub DitoSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dsShared.Tables("ACCDITO").Columns("codditt").DefaultValue = strDittaCorrente

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

  Public Overridable Function DitoCaricaUsers() As Boolean
    Dim dsTmp As New DataSet
    Dim i As Integer = 0
    Try
      If Not oCldAnaz.LeggiTabellaSemplice(strDittaCorrente, "OPERAT", dsTmp) Then Return False
      For i = 0 To dsTmp.Tables("OPERAT").Rows.Count - 1
        If dsShared.Tables("ACCDITO").Select("opdi_opnome = " & CStrSQL(dsTmp.Tables("OPERAT").Rows(i)!OpNome.ToString)).Length = 0 Then
          dsShared.Tables("ACCDITO").Rows.Add(dsShared.Tables("ACCDITO").NewRow)
          dsShared.Tables("ACCDITO").Rows(dsShared.Tables("ACCDITO").Rows.Count - 1)!opdi_opnome = dsTmp.Tables("OPERAT").Rows(i)!OpNome.ToString
        End If
      Next

      dsShared.Tables("ACCDITO").AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub DitoNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("ACCDITO").Rows.Add(dsShared.Tables("ACCDITO").NewRow)
      bHasChanges = True
      bDitoHasChanges = True
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

  Public Overridable Function DitoRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("ACCDITO").Select(strFilter)(nRow).RejectChanges()
      bDitoHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function DitoTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("ACCDITO").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCStr(dtrTmp(i)!opdi_opnome).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128649291060625000, "Inserire un codice utente")))
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

  Public Overridable Function DitoSalva(ByVal bDelete As Boolean) As Boolean
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not DitoTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      dsShared.Tables("ACCDITO").AcceptChanges()
      bDitoHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property DitoRecordIsChanged() As Boolean
    Get
      Return bDitoHasChanges
    End Get
  End Property

  Public Overridable Sub DitoBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strDitoPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DitoBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub DitoBeforeColUpdate_opdi_opnome(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try
      If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "OPERAT", "S") Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128649308761093750, "Operatore inesisntente")))
        Return
      End If

      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = dsShared.Tables("ACCDITO").Select("opdi_opnome = " & CStrSQL(e.ProposedValue.ToString), Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128649292676875000, "Esiste già un utente uguale a quello che si sta cercando di salvare")))
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

  Public Overridable Sub DitoBeforeColUpdate_opdi_codcage(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codcage = ""
        Return
      Else
        If Not oCldAnaz.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCAGE", "N", strTmp) Then
          e.ProposedValue = e.Row!opdi_codcage.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128865235688148248, "Codice Agente inesistente")))
        Else
          e.Row!xx_codcage = strTmp
        End If
      End If
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub DitoAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strDitoPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strDitoPrevCelValue = strDitoPrevCelValue.Remove(strDitoPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      bDiceHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DitoAfterColUpdate_" & e.Column.ColumnName.ToLower
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

#Region "FUNZIONI PER BN__DITM"
  Public Overridable Function DitmSetDataTable(ByVal strDitta As String, ByRef dttAccditm As DataTable) As Boolean
    Try
      dttAccditmEx = dttAccditm
      DitmSetDefaultValue()

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dttAccditmEx.ColumnChanging, AddressOf DitmBeforeColUpdate
      AddHandler dttAccditmEx.ColumnChanged, AddressOf DitmAfterColUpdate

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub DitmSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dttAccditmEx.Columns("codditt").DefaultValue = strDittaCorrente
      dttAccditmEx.Columns("opdi_opnome").DefaultValue = strUserAperto
      dttAccditmEx.Columns("opdi_abilit").DefaultValue = "S"

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

  Public Overridable Function DitmCaricaModuli() As Boolean
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      dtrT = dsShared.Tables("ANAZMOD").Select("xx_abinsg = 'S'")
      For i = 0 To dtrT.Length - 1
        If dttAccditmEx.Select("opdi_modulo = " & dtrT(i)!am_modulo.ToString).Length = 0 Then
          dttAccditmEx.Rows.Add(dttAccditmEx.NewRow)
          dttAccditmEx.Rows(dttAccditmEx.Rows.Count - 1)!opdi_opnome = strUserAperto
          dttAccditmEx.Rows(dttAccditmEx.Rows.Count - 1)!opdi_modulo = dtrT(i)!am_modulo
        End If
      Next

      dttAccditmEx.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub DitmNuovo()
    Try
      DitmSetDefaultValue()

      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dttAccditmEx.Rows.Add(dttAccditmEx.NewRow)

      bHasChanges = True
      bDitmHasChanges = True
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

  Public Overridable Function DitmRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dttAccditmEx.Select(strFilter)(nRow).RejectChanges()
      bDitmHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function DitmTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dttAccditmEx.Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        dtrTmp(i)!opdi_opnome = strUserAperto

        If NTSCInt(dtrTmp(i)!opdi_modulo) < 1 Or NTSCInt(dtrTmp(i)!opdi_modulo) > 260 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128649325708906250, "Il codice modulo deve essere un numero compreso tra 1 e 260")))
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

  Public Overridable Function DitmSalva(ByVal bDelete As Boolean) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not DitmTestPreSalva() Then Return False
      End If

      dttAccditmEx.AcceptChanges()
      bDitmHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property DitmRecordIsChanged() As Boolean
    Get
      Return bDitmHasChanges
    End Get
  End Property

  Public Overridable Sub DitmBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strDitmPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DitmBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub DitmBeforeColUpdate_opdi_modulo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow = Nothing
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_modulo = ""
        Return
      End If

      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = dttAccditmEx.Select("opdi_modulo = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128649856051093750, "Esiste già un modulo uguale a quello che si sta cercando di salvare")))
        Return
      End If

      e.Row!xx_modulo = DESMODULO(NTSCInt(e.ProposedValue))

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

  Public Overridable Sub DitmAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strDitmPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strDitmPrevCelValue = strDitmPrevCelValue.Remove(strDitmPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      bDitmHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DitmAfterColUpdate_" & e.Column.ColumnName.ToLower
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

#Region "FUNZIONI PER BN__DITT"
  Public Overridable Function DittSetDataTable(ByVal strDitta As String, ByRef dttAccDitt As DataTable) As Boolean
    Try
      dttAccDittEx = dttAccDitt
      DittSetDefaultValue()

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dttAccDittEx.ColumnChanging, AddressOf DittBeforeColUpdate
      AddHandler dttAccDittEx.ColumnChanged, AddressOf DittAfterColUpdate

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub DittSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dttAccDittEx.Columns("codditt").DefaultValue = strDittaCorrente
      dttAccDittEx.Columns("opdi_opnome").DefaultValue = strUserAperto
      dttAccDittEx.Columns("xx_modulo").DefaultValue = nModuloAperto
      dttAccDittEx.Columns("opdi_abilit").DefaultValue = "S"

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

  Public Overridable Function DittCaricaProgrammi() As Boolean
    Dim i As Integer = 0
    Dim dttTmp As New DataTable
    Try
      If Not oCldAnaz.GetProgrammiFromModulo(nModuloAperto, dttTmp) Then Return False

      For i = 0 To dttTmp.Rows.Count - 1
        If dttAccDittEx.Select("opdi_nomprog = " & CStrSQL(dttTmp.Rows(i)!mnmd_nomprog)).Length = 0 Then
          dttAccDittEx.Rows.Add(dttAccDittEx.NewRow)
          dttAccDittEx.Rows(dttAccDittEx.Rows.Count - 1)!opdi_opnome = strUserAperto
          dttAccDittEx.Rows(dttAccDittEx.Rows.Count - 1)!xx_modulo = nModuloAperto
          dttAccDittEx.Rows(dttAccDittEx.Rows.Count - 1)!opdi_nomprog = dttTmp.Rows(i)!mnmd_nomprog.ToString

          'se lo stesso programma/voce di menu è condivisa con altri moduli, devo settare il falg
          'come era nell'altro modulo, visto che l'eseguibilità del programma non è per modulo, 
          'ma per l'operatore o è eseguibile oppure no, indipendentemente dal modulo
          For Each dtrT1 As DataRow In dsShared.Tables("ACCDITT").Select("codditt = " & CStrSQL(strDittaCorrente) & _
                                                                     " AND opdi_opnome = " & CStrSQL(dttAccDittEx.Rows(dttAccDittEx.Rows.Count - 1)!opdi_opnome) & _
                                                                     " AND opdi_nomprog = " & CStrSQL(dttAccDittEx.Rows(dttAccDittEx.Rows.Count - 1)!opdi_nomprog))
            dttAccDittEx.Rows(dttAccDittEx.Rows.Count - 1)!opdi_abilit = dtrT1!opdi_abilit
            dtrT1.Delete()
          Next

        End If
      Next

      dttAccDittEx.AcceptChanges()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub DittNuovo()
    Try
      DittSetDefaultValue()

      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dttAccDittEx.Rows.Add(dttAccDittEx.NewRow)

      bHasChanges = True
      bDittHasChanges = True
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

  Public Overridable Function DittRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dttAccDittEx.Select(strFilter)(nRow).RejectChanges()
      bDittHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function DittTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dttAccDittEx.Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        dtrTmp(i)!opdi_opnome = strUserAperto
        dtrTmp(i)!xx_modulo = nModuloAperto

        If NTSCStr(dtrTmp(i)!opdi_nomprog).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128649891345468750, "Il codice programma deve essere impostato")))
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

  Public Overridable Function DittSalva(ByVal bDelete As Boolean) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not DittTestPreSalva() Then Return False
      End If

      dttAccDittEx.AcceptChanges()
      bDittHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public ReadOnly Property DittRecordIsChanged() As Boolean
    Get
      Return bDittHasChanges
    End Get
  End Property

  Public Overridable Sub DittBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strDittPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DittBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub DittBeforeColUpdate_opdi_nomprog(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow = Nothing
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.Row!xx_nomprog = ""
        Return
      End If

      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = dttAccDittEx.Select("opdi_nomprog = " & CStrSQL(e.ProposedValue.ToString), Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128649891240781250, "Esiste già un programma uguale a quello che si sta cercando di salvare")))
        Return
      End If

      strTmp = oCldAnaz.GetDescrProgramma(e.ProposedValue.ToString, nModuloAperto)
      If strTmp = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128649891161406250, "Programma |" & e.ProposedValue.ToString & "| inesistente o non appartenente al modulo in analisi")))
        e.ProposedValue = e.Row(e.Column.ColumnName)
        Return
      Else
        e.Row!xx_nomprog = strTmp
        e.Row!xx_modulo = nModuloAperto
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

  Public Overridable Sub DittAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strDittPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strDittPrevCelValue = strDittPrevCelValue.Remove(strDittPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True
      bDittHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "DittAfterColUpdate_" & e.Column.ColumnName.ToLower
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
