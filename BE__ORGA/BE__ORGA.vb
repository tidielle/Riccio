Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Drawing.Color

Public Class CLE__ORGA
  Inherits CLE__BASN

#Region "Moduli"
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
#End Region

  Public oCldOrga As CLD__ORGA

  Public strPrevCelValueOrganig As String = ""
  Public dsShared As DataSet
  Public bHasChanges As Boolean = False
  Public lContoCf As Integer = 0
  Public lLead As Integer = 0
  Public lCoddest As Integer = 0
  Public bIsUserCrm As Boolean = False
  Public bModCRM As Boolean = False
  Public bModWss As Boolean = False
  Public bModBFP As Boolean = False
  Public bGesttabcont As Boolean = False
  Public dttOrganigDeleted As DataTable = Nothing     'contiene i record di organig cancelati (solo clienti e solo se c'è CRM o CS)
  Public oSend As CLE__SEND = Nothing
  Public dttWmai As DataTable = Nothing
  Public bRubricaCompleta As Boolean = False

  Public Overrides Function Init(ByRef App As CLE__APP, _
                            ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                            ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                            ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__ORGA"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldOrga = CType(ocldBase, CLD__ORGA)
    oCldOrga.Init(oApp)
    Return True
  End Function
  Public Overridable Function InitEx(ByRef ds As DataSet, ByRef dttOrganigDel As DataTable, ByVal strDitta As String, ByVal ContoCf As Integer, ByVal Lead As Integer) As Boolean
    Try
      strDittaCorrente = strDitta
      dsShared = ds
      dttOrganigDeleted = dttOrganigDel
      lContoCf = ContoCf
      lLead = Lead

      OrgaSetDefaultValue()

      AggiornaDescrizione()
      AggiungiColonneDescrizione()

      AddHandler dsShared.Tables("ORGANIG").ColumnChanging, AddressOf OrgaBeforeColUpdate
      AddHandler dsShared.Tables("ORGANIG").ColumnChanged, AddressOf OrgaAfterColUpdate

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function


  Public Overridable Function Close() As Boolean
    Try
      If Not dsShared Is Nothing AndAlso dsShared.Tables.Contains("ORGANIG") Then
        RemoveHandler dsShared.Tables("ORGANIG").ColumnChanging, AddressOf OrgaBeforeColUpdate
        RemoveHandler dsShared.Tables("ORGANIG").ColumnChanged, AddressOf OrgaAfterColUpdate
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


  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChanges
    End Get
  End Property

  Public Overridable Sub OrgaSetDefaultValue()
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      dsShared.Tables("ORGANIG").Columns("codditt").DefaultValue = strDittaCorrente
      dsShared.Tables("ORGANIG").Columns("og_conto").DefaultValue = lContoCf
      dsShared.Tables("ORGANIG").Columns("og_servdtua").DefaultValue = DBNull.Value
      dsShared.Tables("ORGANIG").Columns("og_codlead").DefaultValue = lLead
      dsShared.Tables("ORGANIG").Columns("og_coddest").DefaultValue = lCoddest
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub OrgaNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsShared.Tables("ORGANIG").Rows.Add(dsShared.Tables("ORGANIG").NewRow)

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

  Public Overridable Function OrgaRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsShared.Tables("ORGANIG").Select(strFilter)(nRow).RejectChanges()
      AggiornaDescrizione()
      bHasChanges = False

      dttPropostaContatti = Nothing 'Azzero eventuali proposte

      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function OrgaTestPreSalva() As Boolean
    Dim oDttgr As New CLEGROUPBY
    Dim dttGr As New DataTable
    Try
      'Il codice ruolo va sempre controllato, sia in nuovo che in modifica
      For Each dtrTmp As DataRow In dsShared.Tables("ORGANIG").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
        If NTSCStr(dtrTmp!og_codruaz).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128387519179780000, "Codice ruolo obbligatorio")))
          Return False
        End If
      Next

      'Il numero progressivo basta controllarlo in fase di inserimento
      For Each dtrTmp As DataRow In dsShared.Tables("ORGANIG").Select(Nothing, Nothing, DataViewRowState.Added)
        If ocldBase.ValCodiceDb(NTSCStr(dtrTmp!og_progr), strDittaCorrente, "ORGANIG", "N", "") = True Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129482986425414092, _
          "Esistono già dati con queste caratteristiche: (riga numero |" & NTSCStr(dtrTmp!og_progr) & "|)." & vbCrLf & _
          "Correggere il N° progressivo organigramma usando 'Numerazioni ditta'.")))
          Return False
        End If

        If bRubricaCompleta Then
          If NTSCInt(dtrTmp!og_conto) = -1 OrElse NTSCInt(dtrTmp!og_codlead) = -1 Then
            Dim oEvent As New NTSEventArgs("ChiediConto\Lead", "")
            ThrowRemoteEvent(oEvent)
            If oEvent.RetValue = ThMsg.RETVALUE_OK Then
              Dim strPart() As String = oEvent.InputValue.Split("|"c)
              dtrTmp!og_conto = strPart(0)
              dtrTmp!og_codlead = strPart(1)
            Else
              Return False
            End If
          End If
        End If
      Next

      'controllo che gli oeratori di Business non siano stati indicati più volte
      'og_coperat
      oDttgr.NTSGroupBy(dsShared.Tables("ORGANIG"), dttGr, "count(og_coperat) as nrec, og_coperat", "og_coperat <> ''", "og_coperat")
      If dttGr.Select("nrec > 1").Length > 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129721458466979398, "ATTENZIONE: nelle righe è stato indicato più volte l'operatore di Business '|" & dttGr.Select("nrec > 1")(0)!og_coperat.ToString & "|'")))
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
  Public Overridable Function OrgaTestUltAgg() As Boolean
    Dim dttTmp As New DataTable
    Try
      'Controlla se la data ultimo aggiornamento dell'anagrafica collegata è corretta
      For Each dtrTmp As DataRow In dsShared.Tables("ORGANIG").Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent Or DataViewRowState.Deleted)
        If NTSCStr(dtrTmp("xx_anultagg", DataRowVersion.Original)) <> "" Then
          oCldOrga.ValCodiceDb(NTSCStr(dtrTmp("og_conto", DataRowVersion.Original)), strDittaCorrente, "ANAGRA", "N", , dttTmp)
          If NTSCDate(dtrTmp("xx_anultagg", DataRowVersion.Original)) <> NTSCDate(dttTmp.Rows(0)!an_ultagg) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130675125382875446, "Attenzione! Il conto |" & NTSCStr(dtrTmp("og_conto", DataRowVersion.Original)) & " " & NTSCStr(dttTmp.Rows(0)!an_descr1) & "| è stato cambiato e non è possibile aggiornare le organizzazioni collegate." & vbCrLf & _
                                                                                  "Le modifiche a |" & NTSCStr(dtrTmp("xx_descrizione", DataRowVersion.Original)) & "| saranno annullate.")))

            dtrTmp.RejectChanges()
            Continue For
          End If
        End If

        If NTSCStr(dtrTmp("xx_leultagg", DataRowVersion.Original)) <> "" Then
          oCldOrga.ValCodiceDb(NTSCStr(dtrTmp("og_codlead", DataRowVersion.Original)), strDittaCorrente, "LEADS", "N", , dttTmp)
          If NTSCDate(dtrTmp("xx_leultagg", DataRowVersion.Original)) <> NTSCDate(dttTmp.Rows(0)!le_ultagg) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130675126896569850, "Attenzione! Il lead |" & NTSCStr(dtrTmp("og_codlead", DataRowVersion.Original)) & " " & NTSCStr(dttTmp.Rows(0)!le_descr1) & "| è stato cambiato e non è possibile aggiornare le organizzazioni collegate." & vbCrLf & _
                                                                                  "Le modifiche a |" & NTSCStr(dtrTmp("xx_descrizione", DataRowVersion.Original)) & "| saranno annullate.")))

            dtrTmp.RejectChanges()
            Continue For
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


  Public Overridable Function OrgaSalva(ByVal bDelete As Boolean) As Boolean
    Try
      If bRubricaCompleta AndAlso bDelete Then
        'Non cancello le organizzazioni se è l'ultima organizzazione presente per quel contatto.
        For Each dtrCurrRow As DataRow In dsShared.Tables("ORGANIG").Select("", "", DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
          If NTSCInt(dtrCurrRow!og_codlead) = 0 Then Continue For
          Dim dttTmp As DataTable = Nothing
          CType(oCleComm, CLELBMENU).RitornaOrganizzazioniCollegateAContatto(NTSCInt(dtrCurrRow("og_contatto", DataRowVersion.Original)), dttTmp)
          'Se c'è collegata a quel contatto solo l'organizzazione attuale all'ora non posso cancellarla, ma la imposto come lead -1
          If dttTmp.Rows.Count = 1 Then
            dtrCurrRow.RejectChanges()
            dtrCurrRow!og_conto = 0
            dtrCurrRow!og_coddest = 0
            dtrCurrRow!og_codlead = -1
            bDelete = False
          End If
        Next
      End If
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not OrgaTestPreSalva() Then Return False
      End If

      'Se sono nella rubrica completa controllo che un altro operatore non abbia variato l'anagrafica associata all'operatore,
      ' in tal caso impedisco la modifica di quell'operatore e lo ripristino, per gli altri record posso continuare con il salvataggio
      If bRubricaCompleta Then OrgaTestUltAgg()

      AggiornaDescrizione()

      If bRubricaCompleta Then
        'Rubrica completa: devo salvare le modifiche sul database
        If Not oCldOrga.OrgaSalva(dsShared) Then Return False

        If Not bDelete Then
          'Crea\Aggiorna i contatti collegati
          For Each dtrCurrRow As DataRow In dsShared.Tables("ORGANIG").Select("", "", DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
            If NTSCInt(dtrCurrRow!og_contatto) = 0 Then
              CType(oCleComm, CLELBMENU).CreaNuovoContattoDaOrganig(strDittaCorrente, NTSCInt(dtrCurrRow!og_progr))
            Else
              CType(oCleComm, CLELBMENU).AggiornaContattoDaOrganig(strDittaCorrente, NTSCInt(dtrCurrRow!og_progr))
            End If
          Next
        End If
      End If

      dsShared.Tables("ORGANIG").AcceptChanges()
      bHasChanges = False

      dttPropostaContatti = Nothing 'Azzero eventuali proposte

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub OrgaOnAddNewRow(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim lNum As Integer = 0
    Dim strErr As String = ""
    Try
      lNum = ocldBase.LegNuma(strDittaCorrente, "OG", " ", 0, True)
      lNum = ocldBase.AggNuma(strDittaCorrente, "OG", " ", 0, lNum, True, True, strErr)
      If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))

      e.Row!og_progr = lNum
      e.Row!og_tipork = "I"
      e.Row!og_links = "N"
      e.Row!og_servdeleted = "N"
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub OrgaBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValueOrganig = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'se non  compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName <> "og_progr" Then
        If e.Row!og_progr.ToString = "0" Then OrgaOnAddNewRow(sender, e)
      End If

      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(oApp.Tr(Me, 128067822802778673, strErr))
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "OrgaBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub OrgaBeforeColUpdate_og_coddest(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim i As Integer = 0
    Dim bOk As Boolean = False
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_coddest = ""
      Else
        Dim strDescr As String = ""
        If oCldOrga.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "DESTDIV", "N", strDescr, , NTSCStr(e.Row!og_conto)) Then
          e.Row!xx_coddest = strDescr
        Else
          e.ProposedValue = e.Row!og_coddest.ToString
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128387541504912000, "Codice destinazione inesistente")))
          Return
        End If
      End If

      'Verifico se esiste un lead con quella combinazione "Cliente\Destinazione" 
      e.Row!og_codlead = oCldOrga.TrovaLeadDaContoDestinazione(strDittaCorrente, NTSCInt(e.Row!og_conto), NTSCInt(e.ProposedValue))

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub OrgaBeforeColUpdate_og_conto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If e.ProposedValue.ToString = "0" Then
        e.Row!xx_conto = ""
      Else
        If ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGRA", "N", strTmp, dttTmp) Then
          e.Row!xx_conto = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128387542871554000, "Codice conto inesistente")))
          e.ProposedValue = e.Row!og_conto
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

  Public Overridable Sub OrgaBeforeColUpdate_og_codruaz(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue).Trim = "" Then
        e.Row!xx_codruaz = ""
      Else
        If ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABRUAZ", "S", strTmp) Then
          e.Row!xx_codruaz = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128387319754510000, "Tipo ruolo inesistente")))
          e.ProposedValue = e.Row!og_codruaz
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

  Public Overridable Sub OrgaBeforeColUpdate_og_prov(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
  Public Overridable Sub OrgaBeforeColUpdate_og_stato(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue).Trim = "" Then
        e.Row!xx_stato = ""
      Else
        If ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTAT", "S", strTmp) Then
          e.Row!xx_stato = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128387545342126000, "Stato estero inesistente")))
          e.ProposedValue = e.Row!og_stato
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

  Public Overridable Sub OrgaBeforeColUpdate_og_codcope(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = "0" Then
        e.Row!xx_codcope = ""
      Else
        If ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCOPE", "N", strTmp) Then
          e.Row!xx_codcope = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128387546139442000, "Codice operatore inesistente")))
          e.ProposedValue = e.Row!og_codcope
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
  Public Overridable Sub OrgaBeforeColUpdate_og_codcage(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = "0" Then
        e.Row!xx_codcage = ""
      Else
        If ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCAGE", "N", strTmp) Then
          e.Row!xx_codcage = strTmp
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130459141293221984, "Codice agente inesistente")))
          e.ProposedValue = e.Row!og_codcage
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
  Public Overridable Sub OrgaBeforeColUpdate_og_codcont(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If bGesttabcont Then
        If NTSCStr(e.ProposedValue) = "0" Then
          e.Row!xx_codcont = ""
        Else
          If ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCONT", "N", strTmp, dttTmp) Then
            e.Row!xx_codcont = strTmp
          Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128387546357218000, "Codice risorsa/contatto inesistente")))
            e.ProposedValue = e.Row!og_codcont
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

  Public Overridable Sub OrgaBeforeColUpdate_og_coperat(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue).Trim = "" Then
        '
      Else
        If ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "OPERAT", "S", strTmp) Then
          '
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128387552010278000, "Codice operatore Bus inesistente")))
          e.ProposedValue = e.Row!og_coperat
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

  Public Overridable Sub OrgaBeforeColUpdate_og_codstco(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Dim strTmp As String = ""
    Try
      If NTSCStr(e.ProposedValue) = "0" Then e.Row!xx_codstco = "" : Return

      If ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABSTCO", "N", strTmp, dttTmp) Then
        If NTSCStr(dttTmp.Rows(0)!tb_usatoper) = "L" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129482986905101592, "Status Commerciale non valido")))
          e.ProposedValue = e.Row!og_codstco
        Else
          e.Row!xx_codstco = strTmp
        End If
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130459141396554240, "Status Commerciale non valido")))
        e.ProposedValue = e.Row!og_codstco
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
  Public Overridable Sub OrgaBeforeColUpdate_og_referente(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If NTSCStr(e.ProposedValue) = "0" Then e.Row!xx_referente = "" : Return

      If ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "CONTATTI", "N", , dttTmp) Then
        e.Row!xx_referente = NTSCStr(dttTmp.Rows(0)!co_descont) & " " & NTSCStr(dttTmp.Rows(0)!co_descont2)
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130467781115426487, "Referente non valido")))
        e.ProposedValue = e.Row!og_referente
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


  Public Overridable Sub OrgaAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strPrevCelValueOrganig.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPrevCelValueOrganig = strPrevCelValueOrganig.Remove(strPrevCelValueOrganig.IndexOf(e.Column.ColumnName.ToUpper & ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "OrgaAfterColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

      'Ignoro l'aggiornamento delle colonne xx_, richiamo il listema di validazione dati solo sui campi interassanti.
      If NTSCInt(e.Row!og_contatto) = 0 AndAlso e.Column.ColumnName.StartsWith("og_") Then
        RicercaContattiSimili(e.Row)
        ThrowRemoteEvent(New NTSEventArgs("AbilitaContatti", ""))
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

  Public Overridable Sub OrgaAfterColUpdate_og_codcont(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Try
      If bGesttabcont Then
        ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCONT", "N", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          With dttTmp.Rows(0)
            e.Row!og_telef = NTSCStr(!tb_telef)
            e.Row!og_fax = NTSCStr(!tb_fax)
            e.Row!og_email = NTSCStr(!tb_email)
            e.Row!og_cell = NTSCStr(!tb_cell)
            e.Row!og_descont = NTSCStr(!tb_descont)
            e.Row!og_descont2 = NTSCStr(!tb_descont2)
            e.Row!og_titolo = NTSCStr(!tb_titolo)
            e.Row!og_indir = NTSCStr(!tb_indir)
            e.Row!og_cap = NTSCStr(!tb_cap)
            e.Row!og_citta = NTSCStr(!tb_citta)
            e.Row!og_prov = NTSCStr(!tb_prov)
            e.Row!og_stato = NTSCStr(!tb_stato)
            e.Row!og_datnasc = IIf(NTSCStr(!tb_datnasc) <> "", NTSCDate(!tb_datnasc), DBNull.Value)
            e.Row!og_sesso = NTSCStr(!tb_sesso)
          End With
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

  Public Overridable Sub OrgaAfterColUpdate_og_telef(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      ThrowRemoteEvent(New NTSEventArgs("AbilitaFunzioni", ""))
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub OrgaAfterColUpdate_og_cell(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      ThrowRemoteEvent(New NTSEventArgs("AbilitaFunzioni", ""))
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub OrgaAfterColUpdate_og_telefpers(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      ThrowRemoteEvent(New NTSEventArgs("AbilitaFunzioni", ""))
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub OrgaAfterColUpdate_og_cellpers(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      ThrowRemoteEvent(New NTSEventArgs("AbilitaFunzioni", ""))
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub OrgaAfterColUpdate_og_email(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      ThrowRemoteEvent(New NTSEventArgs("AbilitaFunzioni", ""))
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub OrgaAfterColUpdate_og_emailpers(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      ThrowRemoteEvent(New NTSEventArgs("AbilitaFunzioni", ""))
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub OrgaAfterColUpdate_og_fbuser(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      ThrowRemoteEvent(New NTSEventArgs("AbilitaFunzioni", ""))
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub OrgaAfterColUpdate_og_twitteruser(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      ThrowRemoteEvent(New NTSEventArgs("AbilitaFunzioni", ""))
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub OrgaAfterColUpdate_og_contatto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCInt(e.ProposedValue) <> 0 Then dttPropostaContatti = Nothing
      ThrowRemoteEvent(New NTSEventArgs("AbilitaFunzioniContatto", ""))
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function AggiornaDatiEmail(ByVal lIdOrga As Integer) As Boolean
    Dim dttDatiEmail As New DataTable
    Try
      ocldBase.ValCodiceDb(lIdOrga.ToString, strDittaCorrente, "ORGANIG", "N", , dttDatiEmail)

      If dttDatiEmail.Rows.Count > 0 Then
        Dim dtrRow() As DataRow = dsShared.Tables("ORGANIG").Select("og_progr = " & lIdOrga)
        If dtrRow.Length > 0 Then
          With dttDatiEmail.Rows(0)
            dtrRow(0)!og_email = !og_email
            dtrRow(0)!og_nomeserv = !og_nomeserv
            dtrRow(0)!og_portaserv = !og_portaserv
            dtrRow(0)!og_nomeservu = !og_nomeservu
            dtrRow(0)!og_portaservu = !og_portaservu
            dtrRow(0)!og_pwdmail = !og_pwdmail
            dtrRow(0)!og_securec = !og_securec
            dtrRow(0)!og_ultaggmail = !og_ultaggmail
            dtrRow(0)!og_securecu = !og_securecu
            dtrRow(0)!og_firmamail = !og_firmamail
            dtrRow(0)!og_exchuser = !og_exchuser
            dtrRow(0)!og_configu = !og_configu
            dtrRow(0)!og_exchuseru = !og_exchuseru
            dtrRow(0)!og_pwdmailu = !og_pwdmailu
            dtrRow(0)!og_authsecure = !og_authsecure
            dtrRow(0)!og_useimap = !og_useimap
          End With
        End If
      End If

      dsShared.Tables("ORGANIG").AcceptChanges()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub AggiornaDescrizione()
    Try
      If Not dsShared.Tables("ORGANIG").Columns.Contains("xx_descrizione") Then dsShared.Tables("ORGANIG").Columns.Add("xx_descrizione", GetType(String))
      For Each dtrRow As DataRow In dsShared.Tables("ORGANIG").Rows
        If dtrRow.RowState = DataRowState.Deleted Then Continue For

        Dim strDescrizione As String = NTSCStr(dtrRow!og_descont) & " " & NTSCStr(dtrRow!og_descont2)
        If strDescrizione.Trim = "" Then
          strDescrizione = NTSCStr(dtrRow!og_email)
          If strDescrizione.Trim = "" Then
            strDescrizione = NTSCStr(dtrRow!og_emailpers)
            If strDescrizione.Trim = "" Then
              strDescrizione = NTSCStr(dtrRow!xx_codruaz)
              If strDescrizione.Trim = "" Then
                strDescrizione = "(* ID. " & NTSCStr(dtrRow!og_progr) & " *)"
              End If
            End If
          End If
        End If
        'Assegno la descrizione solo se è cambiata, così evito di impostare la riga come modificata
        If NTSCStr(dtrRow!xx_descrizione) <> strDescrizione Then dtrRow!xx_descrizione = strDescrizione
      Next

      AggiornaColore()
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub AggiungiColonneDescrizione()
    Dim strTmp As String = ""
    Try
      If Not dsShared.Tables("ORGANIG").Columns.Contains("xx_codstco") Then dsShared.Tables("ORGANIG").Columns.Add("xx_codstco", GetType(String))
      If Not dsShared.Tables("ORGANIG").Columns.Contains("xx_referente") Then dsShared.Tables("ORGANIG").Columns.Add("xx_referente", GetType(String))

      For Each dtrRow As DataRow In dsShared.Tables("ORGANIG").Rows
        If dtrRow.RowState = DataRowState.Deleted Then Continue For

        If NTSCInt(dtrRow!og_codstco) <> 0 Then
          ocldBase.ValCodiceDb(NTSCStr(dtrRow!og_codstco), strDittaCorrente, "TABSTCO", "N", strTmp)
          dtrRow!xx_codstco = strTmp
        End If

        If NTSCInt(dtrRow!og_referente) <> 0 Then
          Dim dttTmp As New DataTable
          ocldBase.ValCodiceDb(NTSCStr(dtrRow!og_referente), strDittaCorrente, "CONTATTI", "N", , dttTmp)
          dtrRow!xx_referente = NTSCStr(dttTmp.Rows(0)!co_descont) & " " & NTSCStr(dttTmp.Rows(0)!co_descont2)
        End If
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
  Public Overridable Sub AggiornaColore()
    Dim strTmp As String = ""
    Dim dtToday As Date = New Date(Now.Year, Now.Month, Now.Day)
    Try
      'Aggiunge la colonna che determina lo sfondo
      If Not dsShared.Tables("ORGANIG").Columns.Contains("backcolor_row") Then dsShared.Tables("ORGANIG").Columns.Add("backcolor_row", GetType(Integer))

      For Each dtrRow As DataRow In dsShared.Tables("ORGANIG").Rows
        If dtrRow.RowState = DataRowState.Deleted Then Continue For

        Dim lColore As Integer = -1

        If bRubricaCompleta Then
          'Colore nel caso di organizzazione interna (conto = 0, lead = 0) o lead (conto = 0, lead <> 0)
          If NTSCInt(dtrRow!og_conto) = 0 Then
            If NTSCInt(dtrRow!og_codlead) = 0 Then
              lColore = PaleGreen.ToArgb
            Else
              lColore = LightCyan.ToArgb
            End If
          End If
        End If

        'Persona con la quale il rapporto di lavoro è terminato
        If NTSCStr(dtrRow!og_old) = "S" OrElse NTSCDate(dtrRow!og_dtfine) < dtToday Then
          lColore = MistyRose.ToArgb
        End If
        'Alla fine assegno il colore alla riga solo se è diverso
        If NTSCInt(dtrRow!backcolor_row) <> lColore Then dtrRow!backcolor_row = lColore
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


  Public Overridable Function GeneraUtenteGuest(ByVal dtrOrga As DataRow) As Boolean
    Try
      'Test per verificare che i dati siano corretti
      With dtrOrga
        If NTSCStr(!og_coperat).Trim <> "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130540564221605326, "Un operatore è già stato assegnato a questa organizzazione. Impossibile continuare.")))
          Return False
        End If

        If NTSCStr(!og_email).Trim = "" OrElse NTSCStr(!og_descont).Trim = "" OrElse NTSCStr(!og_descont2).Trim = "" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130540567166252917, "Per poter generare un nuovo utente è necessario che sia compilati almeno i campi 'Cognome, 'Nome' e 'Indirizzo e-mail Aziendale'")))
          Return False
        End If
      End With

      'Genera il nome operatore da associare all'utente
      Dim strNomeOperatore As String = GeneraNomeUtente(dtrOrga)
      Dim strPassword As String = GeneraPasswordCasuale()

      If Not oCldOrga.CreaOperatore(dtrOrga, strNomeOperatore, strPassword) Then Return False

      dtrOrga!og_coperat = strNomeOperatore

      ThrowRemoteEvent(New NTSEventArgs(ThMsg.MSG_INFO, oApp.Tr(Me, 130540595984952059, "Generato il nome utente |'" & strNomeOperatore & "'| con password |'" & strPassword & "'|")))

      If NTSCInt(dtrOrga!og_codlead) <> 0 Then
        Dim evnt As New NTSEventArgs(ThMsg.MSG_YESNO, oApp.Tr(Me, 130543747269193497, "Agganciare l'utente appena creato alle Relazioni di tutti gli utenti CRM che utilizzano Business for People?"))
        ThrowRemoteEvent(evnt)
        If evnt.RetValue = ThMsg.RETVALUE_YES Then CType(oCleComm, CLELBMENU).CollegaGuestConUtentiCRM(strDittaCorrente, NTSCInt(dtrOrga!og_codlead), strNomeOperatore)
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

  Public Overridable Function GeneraNomeUtente(ByVal dtrRow As DataRow) As String
    Dim strUtente As String = ""
    Dim strUtenteBase As String
    Dim lProgressivo As Integer = 0
    Try
      strUtenteBase = (NTSCStr(dtrRow!og_descont) & "." & NTSCStr(dtrRow!og_descont2)).Replace(" ", "")

      'In caso di nome\cognome uguale viene aggiunto un progressivo
      Do
        strUtente = strUtenteBase

        If lProgressivo > 0 Then strUtente &= lProgressivo
        lProgressivo += 1
      Loop Until Not ocldBase.ValCodiceDb(strUtente, "", "OPERAT", "S")
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return strUtente
  End Function
  Public Overridable Function GeneraPasswordCasuale() As String
    Dim strCaratteriDisponibili As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
    Dim sb As New Text.StringBuilder
    Dim oRandom As New Random
    Try
      For i As Integer = 1 To 6
        Dim lIndice As Integer = oRandom.Next(0, strCaratteriDisponibili.Length)
        sb.Append(strCaratteriDisponibili.Substring(lIndice, 1))
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
    Return sb.ToString()
  End Function

  Public Overridable Function EliminaAttivitàDaOperatoreOrganig(ByVal strOperat As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldOrga.EliminaAttivitàDaOperatoreOrganig(strDittaCorrente, strOperat)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

#Region "Proposta Contatti"
  Public dttPropostaContatti As DataTable = Nothing
  Public Overridable Sub RicercaContattiSimili(ByVal dtrRow As DataRow)
    Try
      If NTSCInt(dtrRow!og_contatto) = 0 Then
        dttPropostaContatti = CType(oCleComm, CLELBMENU).ProponiContattoNuovaOrganizzazione(dtrRow)
      Else
        dttPropostaContatti = Nothing
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

  Public Overridable Function GeneraTestoPropostaContatti() As String
    Dim strTesto As String = ""
    Try
      If dttPropostaContatti Is Nothing Then Return ""

      With dttPropostaContatti.Rows(0)
        strTesto = oApp.Tr(Me, 130465166778161008, "Potrebbe essere") & " "
        'Nome e cognome
        strTesto &= (NTSCStr(!co_descont) & " " & NTSCStr(!co_descont2)).Trim & ", "
        'Se c'è, data di nascita
        If NTSCDate(!co_datnasc) <> New Date(1900, 1, 1) Then
          Select Case NTSCStr(!co_sesso)
            Case "M" : strTesto &= oApp.Tr(Me, 130465170311196879, "nato il")
            Case "F" : strTesto &= oApp.Tr(Me, 130465170582651464, "nata il")
          End Select
          strTesto &= " " & NTSCDate(!co_datnasc).ToShortDateString & ", "
        End If
        'Luogo (città, provincia)
        If NTSCStr(!co_citta).Trim <> "" Then
          strTesto &= oApp.Tr(Me, 130465175597739932, "di |" & NTSCStr(!co_citta) & "|")
          If NTSCStr(!co_prov).Trim <> "" Then strTesto &= " (" & NTSCStr(!co_prov) & ")"
        End If
      End With

      strTesto = strTesto.Trim().Trim(","c) & "?"
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return strTesto
  End Function

  Public Overridable Sub ProssimaPropostaContatto()
    Try
      If dttPropostaContatti Is Nothing Then Return
      dttPropostaContatti.Rows(0).Delete()
      dttPropostaContatti.AcceptChanges()
      If dttPropostaContatti.Rows.Count = 0 Then dttPropostaContatti = Nothing
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ConfermaPropostaContatto(ByVal dtrOrga As DataRow)
    Try
      If dttPropostaContatti Is Nothing Then Return

      CollegaAContatto(dtrOrga, dttPropostaContatti.Rows(0))
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function CollegaAContatto(ByVal dtrOrga As DataRow, ByVal dtrProposta As DataRow) As Boolean
    Try
      With dtrProposta
        dtrOrga!og_contatto = !co_progr

        dtrOrga!og_sesso = !co_sesso 'è sempre valorizzato

        'Tutti gli altri dati solo se sono presenti sul contatto.
        If NTSCStr(!co_descont).Trim <> "" Then dtrOrga!og_descont = !co_descont
        If NTSCStr(!co_descont2).Trim <> "" Then dtrOrga!og_descont2 = !co_descont2
        If NTSCStr(!co_titolo).Trim <> "" Then dtrOrga!og_titolo = !co_titolo
        If NTSCStr(!co_indir).Trim <> "" Then dtrOrga!og_indir = !co_indir
        If NTSCStr(!co_cap).Trim <> "" Then dtrOrga!og_cap = !co_cap
        If NTSCStr(!co_citta).Trim <> "" Then dtrOrga!og_citta = !co_citta
        If NTSCStr(!co_prov).Trim <> "" Then dtrOrga!og_prov = !co_prov
        If NTSCStr(!co_stato).Trim <> "" Then dtrOrga!og_stato = !co_stato
        If NTSCStr(!co_datnasc).Trim <> "" Then dtrOrga!og_datnasc = !co_datnasc
        If NTSCStr(!co_fbuser).Trim <> "" Then dtrOrga!og_fbuser = !co_fbuser
        If NTSCStr(!co_fbpwd).Trim <> "" Then dtrOrga!og_fbpwd = !co_fbpwd
        If NTSCStr(!co_twitteruser).Trim <> "" Then dtrOrga!og_twitteruser = !co_twitteruser
        If NTSCStr(!co_twitterpwd).Trim <> "" Then dtrOrga!og_twitterpwd = !co_twitterpwd
        If NTSCStr(!co_skypeuser).Trim <> "" Then dtrOrga!og_skypeuser = !co_skypeuser
        If NTSCStr(!co_skypepwd).Trim <> "" Then dtrOrga!og_skypepwd = !co_skypepwd
        If NTSCStr(!co_telefpers).Trim <> "" Then dtrOrga!og_telefpers = !co_telefpers
        If NTSCStr(!co_faxpers).Trim <> "" Then dtrOrga!og_faxpers = !co_faxpers
        If NTSCStr(!co_emailpers).Trim <> "" Then dtrOrga!og_emailpers = !co_emailpers
        If NTSCStr(!co_cellpers).Trim <> "" Then dtrOrga!og_cellpers = !co_cellpers
        If NTSCInt(!co_codstco) <> 0 Then dtrOrga!og_codstco = !co_codstco
        If NTSCInt(!co_referente) <> 0 Then dtrOrga!og_referente = !co_referente
        'L'utente di business solo se è un operatore interno
        If NTSCInt(dtrOrga!og_codlead) = 0 AndAlso NTSCInt(dtrOrga!og_conto) = 0 AndAlso NTSCStr(!co_coperat).Trim <> "" Then dtrOrga!og_coperat = !co_coperat
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


  Public Overridable Function VerificaContattoMultiOrganizzazione(ByVal dtrRow As DataRow) As Boolean
    Dim dttOrganizzazioni As DataTable = Nothing
    Try
      If dtrRow Is Nothing Then Return False

      Dim lContatto As Integer = NTSCInt(dtrRow!og_contatto)
      'Controlla se esistono delle organizzazioni collegate al contatto
      If Not CType(oCleComm, CLELBMENU).RitornaOrganizzazioniCollegateAContatto(lContatto, dttOrganizzazioni) Then Return False

      'I casi sono, nessuna ancora collegata
      'Una collegata, che potrebbe essere quella attuale od un'altra
      'Più di una collegata
      Select Case dttOrganizzazioni.Rows.Count
        Case 0 : Return False
        Case 1 : Return NTSCStr(dttOrganizzazioni.Rows(0)!codditt) <> NTSCStr(dtrRow!codditt) OrElse _
                        NTSCInt(dttOrganizzazioni.Rows(0)!og_progr) <> NTSCInt(dtrRow!og_progr)
        Case Else : Return True
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

#Region "Invio E-mail"
  Public Overridable Sub ApriEmail(ByVal strEmail As String)
    Dim strError As String = ""
    Try
      If Not IstanziaSend() Then Return

      oSend.InviaMail(strDittaCorrente, strEmail, "", "", "", Nothing, False, "<html><body></body></html>", strError, "", 0, 0, "BE__ORGA", True)

      If strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strError))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function IstanziaSend() As Boolean
    Try
      If oSend Is Nothing Then
        '-------------------------------
        'invio la mail
        Dim strError As String = ""
        Dim oTmp As Object = Nothing
        If Not CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEXXSOCI", "BE__SEND", oTmp, strError, False, "", "") Then
          If strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strError))
          Return False
        End If
        oSend = CType(oTmp, CLE__SEND)
        AddHandler oSend.RemoteEvent, AddressOf GestisciEventiEntitySend
        oSend.Init(oApp, Nothing, Nothing, "", False, "", "")
        strError = ""
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
  Public Overridable Sub GestisciEventiEntitySend(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      If e.TipoEvento = "APRIWMAI_:" Then dttWmai = oSend.dsWmai.Tables("EMAILS")

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
#End Region


#Region "Gestione Autonoma delle Organizzazioni - RUBRICA"
  Public Overridable Function CaricaOrganizzazioni(ByVal bInterna As Boolean, ByVal bClienti As Boolean, ByVal bFornitori As Boolean, _
                                                   ByVal bLeads As Boolean) As Boolean
    Try
      dsShared = oCldOrga.CaricaOrganizzazioni(strDittaCorrente, bIsUserCrm, bInterna ,bClienti, bFornitori, bLeads)

      bRubricaCompleta = True

      oCldOrga.SetTableDefaultValueFromDB("ORGANIG", dsShared)

      AggiornaDescrizione()
      'AggiungiColonneDescrizione()

      'Se lead e conto sono a -1, in fase di salvataggiodi una nuova organizzazione devo chiedere a chi associare
      lLead = -1
      lContoCf = -1
      OrgaSetDefaultValue()

      dsShared.AcceptChanges()
      bHasChanges = False

      AddHandler dsShared.Tables("ORGANIG").ColumnChanging, AddressOf OrgaBeforeColUpdate
      AddHandler dsShared.Tables("ORGANIG").ColumnChanged, AddressOf OrgaAfterColUpdate
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function LeadCollegatoAConto(ByVal lConto As Integer) As Integer
    Try
      Return oCldOrga.LeadCollegatoAConto(strDittaCorrente, lConto)
    Catch ex As Exception
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
End Class
