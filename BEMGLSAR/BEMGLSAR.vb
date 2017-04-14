Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports System.IO.File

Public Class CLEMGLSAR
  Inherits CLE__BASE

  Public oCldLsar As CLDMGLSAR
  Public bModSupIPL As Boolean

  'Movimentazione
  Public bMovimentaz As Boolean
  Public bEsplComb As Boolean
  Public bEsistMagZero As Boolean 'Giagenza in magazzino > 0
  Public bEsistMinZero As Boolean 'Giacenza in magazzino < 0
  Public bEsplFasi As Boolean
  'Elsa
  Public dsElsaShared As DataSet
  Public bElsaHasChanges As Boolean = False
  Public strCodLSar As String
  Public nCodMagP As Integer
  Public bElsaGrigliaBloccata As Boolean
  Public strWhereFiar As String
  Public bOrdinaCodart As Boolean
  Public bOrdinaDescr As Boolean
  Public bOrdinaCodalt As Boolean
  Public bFlagTrattato As Boolean
  Public bMsgArticoloDuplicato As Boolean
  Public bImpostastato As Boolean
  Public bGesfasi As Boolean
  Public bGeslotti As Boolean
  Public bGesubic As Boolean
  Public bGescomm As Boolean
  Public bGestmatr As Boolean
  Public bLottoNew As Boolean = False     'se true (letto da anaditac) alo_lotto è calcolato sempre in automatico, se false alo_lotto è uguale a alo_lottox e alo_lottx deve essere numerico di max 9 char
  Public bLottoUnivoco As Boolean = False           'se true al salvataggio del documento controllo che per gli articoli gestiti a lotti il lotto sia univoco (ovvero non esistano altri articoli con lo stesso analotti.alo_lottox oltre a quello della riga che sto validando

  Public bInImport As Boolean = False 'se si è in import da terminale logga i msg dei BeforeColUpdate

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
  Public strTermTipoArt As String 'C=codart, B=barcode (default)
  Public nTermCodartPos As Integer '0-255(default 0)
  Public nTermCodartStart As Integer '0-9999(default 1)
  Public nTermCodartStop As Integer '0-9999(default 13)
  Public bTermFase As Boolean 'True (default): nuovo: legge dal file ascii la descrizione o meno
  Public nTermFasePos As Integer '0-255
  Public nTermFaseStart As Integer '0-9999
  Public nTermFaseStop As Integer '0-9999
  Public bTermCom As Boolean 'True (default): nuovo: legge dal file ascii la descrizione o meno
  Public nTermComPos As Integer '0-255
  Public nTermComStart As Integer '0-9999
  Public nTermComStop As Integer '0-9999
  Public bTermLotto As Boolean 'True (default): nuovo: legge dal file ascii le qtà o meno
  Public nTermLottoPos As Integer '0-255(default 0)
  Public nTermLottoStart As Integer '0-9999(default 14)
  Public nTermLottoStop As Integer '0-9999(default 19)
  Public bTermUbic As Boolean 'False(default)
  Public nTermUbicPos As Integer '0-255(default 0)
  Public nTermUbicStart As Integer '0-9999(default 0)
  Public nTermUbicStop As Integer '0-9999(default 0)
  Public bTermMatr As Boolean 'False(default)
  Public nTermMatrPos As Integer '0-255(default 0)
  Public nTermMatrStart As Integer '0-9999(default 0)
  Public nTermMatrStop As Integer '0-9999(default 0)
  Public bTermEsis As Boolean 'False(default)
  Public nTermEsisPos As Integer '0-255(default 0)
  Public nTermEsisStart As Integer '0-9999(default 0)
  Public nTermEsisStop As Integer '0-9999(default 0)
  Public nTermDivEsis As Integer '1-10000(default 1)

  Public bLayoAnnullato As Boolean = False

  Private Moduli_P As Integer = bsModMG + bsModVE + bsModOR
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtORE
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

  Public Overrides Function Apri(ByVal strDitta As String, ByRef ds As System.Data.DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'Tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldLsar.GetData(strDittaCorrente, ds)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'Imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldLsar.SetTableDefaultValueFromDB(strNomeTabella, ds)

      SetDefaultValue(ds)

      dsShared = ds

      '--------------------------------------
      'Creo gli eventi per la gestione del Datatable dentro l'Entity
      AddHandler dsShared.Tables(strNomeTabella).ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables(strNomeTabella).ColumnChanged, AddressOf AfterColUpdate

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

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGLSAR"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldLsar = CType(MyBase.ocldBase, CLDMGLSAR)
    oCldLsar.Init(oApp)
    Return True
  End Function

  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = ocldBase.GetSettingBus("BSMGLSAR", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
      strActLogProg = "BSMGLSAR"
      strActLogNomOggLog = "TABLSAR"
      strActLogDesLog = oApp.Tr(Me, 128521372932600491, "Liste Selezionate Articoli")


      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("TABLSAR").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("TABLSAR").Columns("tb_dtcomp").DefaultValue = NTSCDate(Now)
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly, inizializza il campo "Status" a "Completato"
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then ds.Tables("TABLSAR").Columns("tb_status").DefaultValue = "C"
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overrides Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
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

  Public Overridable Sub BeforeColUpdate_tb_codlsar(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Try

      '-------------------------------------------------
      'verifico se il codice è univoco
      dtrTmp = dsShared.Tables("TABLSAR").Select("tb_codlsar = " & e.ProposedValue.ToString, Nothing, DataViewRowState.CurrentRows)
      If dtrTmp.Length > 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128521372932756748, "Il codice inserito è già stato utilizzato. Inserire un codice non utilizzato")))
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
  Public Overridable Sub BeforeColUpdate_tb_codmagap(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codmagap = ""
      Else
        If Not oCldLsar.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABMAGA", "N", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129030080450236791, "Cod. magazzino inesistente.")))
          Return
        Else
          e.Row!xx_codmagap = strTmp
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

  Public Overrides Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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

  Public Overrides Function TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsShared.Tables("TABLSAR").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!tb_codlsar) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128521372932913005, "Inserire un codice Liste Selezionate Articoli compreso tra 0 e 9999")))
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

  Public Overridable Function DeleteListsar(ByVal strCodLsar As String) As Boolean
    Try
      Return oCldLsar.DeleteListSar(strDittaCorrente, strCodLsar)
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

  Public Overridable Function FileGeneraFile(ByVal dtrTmp As DataRow, ByRef strnomfileinv As String, ByRef nCodlsarTmp As Integer) As Boolean
    Dim strMsg As String
    Dim evt As NTSEventArgs = Nothing
    Dim dsArtico As DataSet = Nothing
    Dim strNomeFile As String
    Dim lw1 As StreamWriter = Nothing
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    Dim dEsist As Decimal
    Dim strRiga As String
    Dim lProgr As Integer
    Dim dQtacon2 As Decimal
    Dim dConver As Decimal
    Dim dQuant As Decimal
    Dim strSeparatoreDecimale As String
    Try
      strSeparatoreDecimale = "."
      '-----------------------------------------------------------------------------------------
      '--- Se non è stato indicato un magazzino valido, esce
      '-----------------------------------------------------------------------------------------
      If NTSCInt(dtrTmp!tb_codmagap) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523926841596871, "Attenzione!" & vbCrLf & _
          "Non è stato indicato un magazzino valido." & vbCrLf & _
          "Generazione file inventario non possibile.")))
        Return False
      End If

      'non posso usare i lotti alfanumerici!!!!
      If bLottoNew Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129519262436296160, "ATTENZIONE: non è possibile utilizzare questa funzione quando è spuntato in anagrafica ditta -> dati aggiuntivi contabilità 'Gestione lotti alfanumerici'")))
        Return False
      End If

      '-----------------------------------------------------------------------------------------
      '--- Controlla lo status di riga
      '-----------------------------------------------------------------------------------------
      If (NTSCStr(dtrTmp!tb_status) = "P") Or (NTSCStr(dtrTmp!tb_status) = "S") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523926793101397, "Attenzione!" & vbCrLf & _
          "Lo 'Status' della lista selezionata corrente dalla quale generare il file inventario deve essere di tipo:" & vbCrLf & _
          " . 'Modificabile'" & vbCrLf & _
          " . 'Completato'" & vbCrLf & _
          "Generazione file inventario non possibile.")))
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se non esistono righe nel dettaglio, esce
      '-----------------------------------------------------------------------------------------
      oCldLsar.GetListsar(strDittaCorrente, NTSCStr(dtrTmp!tb_codlsar), dsTmp)

      If dsTmp.Tables("LISTSAR").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523926618143449, "Attenzione!" & vbCrLf & _
          "Non esistono articoli nel dettaglio della lista selezionata corrente dalla quale generare il file inventario." & vbCrLf & _
          "Generazione file inventario non possibile.")))
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Chiede conferma della generazione del file inventario
      '-----------------------------------------------------------------------------------------
      strMsg = oApp.Tr(Me, 129030083457775287, "Generazione del file inventario dalla lista selezionata corrente:") & vbCrLf & _
        "N° '" & NTSCStr(dtrTmp!tb_codlsar) & "'" & NTSCStr(IIf(Trim(NTSCStr(dtrTmp!tb_deslsar)) <> "", " " & NTSCStr(dtrTmp!tb_deslsar), "")) & ""
      If Trim(NTSCStr(dtrTmp!tb_nomfileinv)) <> "" Then
        strMsg = strMsg & vbCrLf & oApp.Tr(Me, 129030083947625307, "cancellando il file '|" & NTSCStr(dtrTmp!tb_nomfileinv) & "|' precedentemente creato.") & vbCrLf
      Else
        strMsg = strMsg & "." & vbCrLf
      End If
      strMsg = strMsg & oApp.Tr(Me, 129030084160909287, "Confermare?")

      evt = New NTSEventArgs("MSG_YESNO", strMsg)
      ThrowRemoteEvent(evt)
      If Not evt.RetValue = "YES" Then
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      nCodlsarTmp = NTSCInt(dtrTmp!tb_codlsar)
      '-----------------------------------------------------------------------------------------
      '--- Apre un recordset su LISTSAR
      '-----------------------------------------------------------------------------------------
      oCldLsar.GeneraFileGetListSar(strDittaCorrente, NTSCStr(dtrTmp!tb_codlsar), dsArtico)

      If dsArtico.Tables("ARTICO").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523934711847127, "Attenzione!" & vbCrLf & _
          "Non esistono articoli nel dettaglio della lista selezionata con le caratteristiche richieste." & vbCrLf & _
          "Generazione file inventario non possibile.")))
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Determina il nome del file inventario
      '-----------------------------------------------------------------------------------------
      lProgr = oCldLsar.LegNuma(strDittaCorrente, "IN", " ", 0, True)
      lProgr = oCldLsar.AggNuma(strDittaCorrente, "IN", " ", 0, lProgr, True, False, "")
      strnomfileinv = "INV" & Right("000000000" & NTSCStr(lProgr), 9) & ".INV"
      strNomeFile = Mid(oApp.RptDir, 1, (Len(oApp.RptDir) - 3)) & "ASC\" & strnomfileinv
      '-----------------------------------------------------------------------------------------
      '--- Apre il file
      '-----------------------------------------------------------------------------------------
      lw1 = New StreamWriter(strNomeFile)
      '-----------------------------------------------------------------------------------------
      '--- Controllo se per sbaglio il file esiste già
      '--- Se esiste devo trovare il prossimo progressivo valido
      '-----------------------------------------------------------------------------------------
      '--- Struttura file, sezione articoli prelevati da LISTSAR (dettaglio lista selezionata):
      '-----------------------------------------------------------------------------------------
      '--- PRIMA PARTE
      '-----------------------------------------------------------------------------------------
      '--- tipork|ditta|codart|fase|desart|desfase|ap_esist|geslotti|gescomm|gesmatr|
      '--- gesubicaz|gesfasi|ap_magaz|lsa_codlsar|ar_unmis|ar_confez2|ar_qtacon2|ar_unmis2|
      '--- ar_conver|esist_effettiva|codlotto|codmatric|codcommeca|codubicaz|trattato
      '-----------------------------------------------------------------------------------------
      'While Exists(strNomeFile)
      '  lProgr = oCldLsar.LegNuma(strDittaCorrente, "PK", " ", 0, True)
      '  lProgr = oCldLsar.AggNuma(strDittaCorrente, "PK", " ", 0, lProgr, True, False, "")
      '  strNomeFile = Mid(oApp.RptDir, 1, (Len(oApp.RptDir) - 3)) & "ASC\" & "INV" & Right("000000000" & NTSCStr(lProgr), 9) & ".INV"
      'End While

      '-----------------------------------------------------------------------------------------
      For i = 0 To dsArtico.Tables("ARTICO").Rows.Count - 1
        '---------------------------------------------------------------------------------------
        '--- Controlla, se per quell'articolo/magazzino/fase, c'è l'esistenza in ARTPRO
        '---------------------------------------------------------------------------------------
        dEsist = 0
        oCldLsar.GetArtPro(strDittaCorrente, NTSCStr(dsArtico.Tables("ARTICO").Rows(i)!lsa_codart), NTSCStr(dtrTmp!tb_codmagap), NTSCStr(dsArtico.Tables("ARTICO").Rows(i)!lsa_fase), dsTmp)

        If dsTmp.Tables("ARTPRO").Rows.Count > 0 Then dEsist = NTSCDec(dsTmp.Tables("ARTPRO").Rows(0)!ap_esist)
        '---------------------------------------------------------------------------------------

        '---------------------------------------------------------------------------------------
        With dsArtico.Tables("ARTICO").Rows(i)
          dQtacon2 = NTSCDec(!ar_qtacon2)
          dConver = NTSCDec(!ar_conver)
          strRiga = "1|" & strDittaCorrente & "|" & NTSCStr(!lsa_codart) & "|" & NTSCStr(!lsa_fase) & "|" & _
            NTSCStr(!ar_descr) & "|" & NTSCStr(!af_descr) & "|" & _
            NTSCStr(dEsist).Replace(strSeparatoreDecimale, ",") & "|" & _
            NTSCStr(!ar_geslotti) & "|" & NTSCStr(!ar_gescomm) & "|" & _
            NTSCStr(!ar_gestmatr) & "|" & NTSCStr(!ar_gesubic) & "|" & _
            NTSCStr(!ar_gesfasi) & "|" & NTSCStr(dtrTmp!tb_codmagap) & "|" & _
            NTSCStr(dtrTmp!tb_codlsar) & "|" & NTSCStr(!ar_unmis) & "|" & _
            NTSCStr(!ar_confez2) & "|" & _
            NTSCStr(dQtacon2).Replace(strSeparatoreDecimale, ",") & "|" & _
            NTSCStr(!ar_unmis2) & "|" & _
            NTSCStr(dConver).Replace(strSeparatoreDecimale, ",") & "|0|0||0||N"
        End With
        '---------------------------------------------------------------------------------------
        '--- Scrive la riga nel file
        '---------------------------------------------------------------------------------------
        lw1.WriteLine(strRiga)
        '---------------------------------------------------------------------------------------
      Next
      '-----------------------------------------------------------------------------------------
      '--- Struttura file, sezione articoli prelevati da BARCODE:
      '-----------------------------------------------------------------------------------------
      '--- SECONDA PARTE
      '-----------------------------------------------------------------------------------------
      '--- tipork|ditta|barcode|codart|fase|unmis|quantità
      '-----------------------------------------------------------------------------------------
      oCldLsar.GetBarcode(strDittaCorrente, NTSCStr(dtrTmp!tb_codlsar), dsArtico)
      '---------------------------------------------------------------------------------------
      For i = 0 To dsArtico.Tables("ARTICO").Rows.Count - 1
        '-------------------------------------------------------------------------------------
        With dsArtico.Tables("ARTICO").Rows(i)
          dQuant = NTSCDec(!bc_quant)
          strRiga = "2|" & strDittaCorrente & "|" & NTSCStr(!bc_code) & "|" & _
            NTSCStr(!bc_codart) & "|" & NTSCStr(!bc_fase) & "|" & _
            NTSCStr(!bc_unmis) & "|" & _
            NTSCStr(dEsist).Replace(strSeparatoreDecimale, ",")
        End With
        '-------------------------------------------------------------------------------------
        '--- Scrive la riga nel file
        '-------------------------------------------------------------------------------------
        lw1.WriteLine(strRiga)
        '-------------------------------------------------------------------------------------
      Next
      '-----------------------------------------------------------------------------------------
      '--- Chiude il file di testo
      '-----------------------------------------------------------------------------------------
      lw1.Flush()
      lw1.Close()
      '-----------------------------------------------------------------------------------------
      '--- Cancella l'eventuale vecchio file già esistente
      '-----------------------------------------------------------------------------------------
      If Trim(NTSCStr(dtrTmp!tb_nomfileinv)) <> "" Then
        Try
          Kill(Mid(oApp.RptDir, 1, (Len(oApp.RptDir) - 3)) & "ASC\" & NTSCStr(dtrTmp!tb_nomfileinv))
        Catch ex As Exception
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523974195207679, "Cancellazione vecchio file |" & NTSCStr(dtrTmp!tb_nomfileinv) & "| già esistente non riuscita")))
        End Try
      End If
      '-----------------------------------------------------------------------------------------
      '--- Scrive il nome del file appena creato in griglia
      '-----------------------------------------------------------------------------------------
      oCldLsar.UpdateTablsar(strDittaCorrente, strnomfileinv, nCodlsarTmp)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function FileAcquisisciFile(ByVal dtrTmp As DataRow) As Boolean
    Dim strMsg As String = ""
    Dim strNomeFile As String
    Dim evt As NTSEventArgs = Nothing
    Dim f1 As System.IO.StreamReader = Nothing
    Dim strTmp As String
    Dim dRiga As Decimal
    Dim strLineFile() As String
    Try
      If bLottoNew Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129519345688134396, "ATTENZIONE: non è possibile utilizzare questa funzione quando è spuntato in anagrafica ditta -> dati aggiuntivi contabilità 'Gestione lotti alfanumerici'")))
        Return False
      End If

      '-----------------------------------------------------------------------------------------
      '--- Controlla lo status di riga
      '-----------------------------------------------------------------------------------------
      If NTSCStr(dtrTmp!tb_status) <> "C" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523987682839708, "Attenzione!" & vbCrLf & _
          "Lo 'Status' della lista selezionata corrente dalla quale generare il file inventario deve essere di tipo 'Completato'." & vbCrLf & _
          "Acquisizione da file inventario non possibile.")))
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se non c'è un nome di file inventario, avvisa ed esce
      '-----------------------------------------------------------------------------------------
      If Trim(NTSCStr(dtrTmp!tb_nomfileinv)) = "" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523987776384340, "Attenzione!" & vbCrLf & _
          "Per la lista selezionata corrente non esiste un file valido dal quale acquisire righe nel dettaglio." & vbCrLf & _
          "Acquisizione da file inventario non possibile.")))
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Se il un nome di file inventario esiste, ma non esiste fisicamente, avvisa ed esce
      '-----------------------------------------------------------------------------------------
      strNomeFile = Mid(oApp.RptDir, 1, (Len(oApp.RptDir) - 3)) & "ASC\" & NTSCStr(dtrTmp!tb_nomfileinv)
      If Exists(strNomeFile) = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523987883203252, "Attenzione!" & vbCrLf & _
          "Il file inventario '|" & NTSCStr(dtrTmp!tb_nomfileinv) & "|' indicato nella lista selezionata corrente non è esistente." & vbCrLf & _
          "Acquisizione da file inventario non possibile.")))
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Chiede conferma dell'acquisizione dal file inventario
      '-----------------------------------------------------------------------------------------
      evt = New NTSEventArgs("MSG_YESNO", oApp.Tr(Me, 128523991944259734, "Acquisizione dal file inventario '|" & NTSCStr(dtrTmp!tb_nomfileinv) & "|'" & vbCrLf & _
        "eliminando le eventuali righe di dettaglio già esistenti." & vbCrLf & _
        "Confermare?"))
      ThrowRemoteEvent(evt)
      If Not evt.RetValue = "YES" Then
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      '--- Cancella LISTSAR collegata
      '-----------------------------------------------------------------------------------------
      oCldLsar.DeleteListSar(strDittaCorrente, NTSCStr(dtrTmp!tb_codlsar))
      '-----------------------------------------------------------------------------------------
      '--- Apre il file
      '-----------------------------------------------------------------------------------------
      f1 = New System.IO.StreamReader(strNomeFile)
      '-----------------------------------------------------------------------------------------
      dRiga = 1
      '-----------------------------------------------------------------------------------------
      Do While Not f1.EndOfStream
        '---------------------------------------------------------------------------------------
        '--- Legge la riga del file di testo
        '---------------------------------------------------------------------------------------
        strTmp = f1.ReadLine
        '---------------------------------------------------------------------------------------
        '--- Spezza la riga in un vettore di stringhe
        '---------------------------------------------------------------------------------------
        '--- Se la riga NON è di tipo '1' la salta
        '---------------------------------------------------------------------------------------
        strLineFile = strTmp.Split(CType("|", Char))
        If strLineFile(0) <> "1" Then GoTo RigaSuccessiva
        '---------------------------------------------------------------------------------------
        '--- Elementi del vettore:
        '---------------------------------------------------------------------------------------
        '--- 0) tipork, 1) ditta, 2)codart, 3)fase, 4)desart 5)desfase 6)ap_esist
        '--- 7)geslotti 8)gescomm 9)gesmatr 10)gesubicaz 11)gesfasi 12)ap_magaz
        '--- 13)lsa_codlsar 14)ar_unmis 15)ar_confez2 16)ar_qtacon2 17)ar_unmis2
        '--- 18)ar_conver 19)esist_effettiva 20)codlotto 21)codmatric 22)codcommeca
        '--- 23)codubicaz 24)trattato
        '---------------------------------------------------------------------------------------
        oCldLsar.InsertIntoListsar(strDittaCorrente, strLineFile, dRiga, NTSCStr(dtrTmp!tb_codlsar))
        '---------------------------------------------------------------------------------------
        '--- Incrementa il contatore delle righe di LISTSAR
        '---------------------------------------------------------------------------------------
        dRiga = (dRiga + 1)
        '---------------------------------------------------------------------------------------
RigaSuccessiva:
      Loop
      '-----------------------------------------------------------------------------------------
      '--- Chiude il file di testo
      '-----------------------------------------------------------------------------------------
      f1.Close()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckInPromozioni(ByVal lCodlsar As Integer) As Boolean
    Dim dttTmp As New DataTable
    Dim strMsg As String = ""
    Try
      Return True

      'If Not oCldLsar.CheckInPromozioni(strDittaCorrente, lCodlsar, dttTmp) Then Return False

      ''Se non può cancellarla ritorna false
      'If dttTmp.Rows.Count > 0 Then
      '  strMsg = oApp.Tr(Me, 129525087615387213, "Non è possibile modificare o cancellare la lista selenazionata in quanto è in uso nelle seguenti promozioni:")

      '  For z As Integer = 0 To dttTmp.Rows.Count - 1
      '    strMsg &= vbCrLf & NTSCStr(dttTmp.Rows(z)!tb_codrepr) & " - " & NTSCStr(dttTmp.Rows(z)!tb_desrepr)
      '  Next

      '  ThrowRemoteEvent(New NTSEventArgs("", strMsg))
      '  Return False
      'End If

      'Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function Duplica(ByVal strCodlistsel As String, ByVal strNewCodlistsel As String) As Boolean
    Try
      Return oCldLsar.Duplica(strDittaCorrente, strCodlistsel, strNewCodlistsel)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function GetImpostazioniFile(ByRef dttOut As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldLsar.GetImpostazioniFile(dttOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function ImpostazioneFileGiaEsistente(ByVal strNome As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldLsar.ImpostazioneFileGiaEsistente(strNome)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function DeleteImpostazioniFile(ByVal strNome As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldLsar.DeleteImpostazioniFile(strNome)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

#Region "funzioni specifiche per BNNGLSAR.BNMGELSA.VB"
  Public Overridable Sub ElsaOnAddNew(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dReturn As Boolean = False
    Dim nNcampo As Integer
    Dim dsMaxListsar As DataSet = Nothing
    Try
      If e.Row!lsa_riga Is Nothing Then Exit Sub
      dReturn = oCldLsar.GetMaxListsar(strDittaCorrente, NTSCStr(e.Row!lsa_codlsar), dsMaxListsar)

      If dsMaxListsar.Tables("LISTSAR").Rows.Count > 0 Then
        nNcampo = NTSCInt(dsMaxListsar.Tables("LISTSAR").Rows(0)!Riga)
      Else
        nNcampo = 0
      End If

      e.Row!lsa_riga = NTSCStr(nNcampo + 1)

      e.Row!lsa_trattato = NTSCStr(IIf(bImpostastato = True, "S", "N"))

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Function ElsaApri(ByVal strDitta As String, ByRef dsElsa As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim nOrdin As Integer = -1 'ovvero ordinato per come è stata scritta la lista selezionata
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      If bOrdinaCodart Then nOrdin = 1
      If bOrdinaDescr Then nOrdin = 2
      If bOrdinaCodalt Then nOrdin = 3

      dReturn = oCldLsar.ElsaGetData(strDitta, strCodLSar, bOrdinaCodart, dsElsa, nOrdin)
      If dReturn = False Then Return False

      oCldLsar.SetTableDefaultValueFromDB("LISTSAR", dsElsa)

      dsElsaShared = dsElsa

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsElsaShared.Tables("LISTSAR").ColumnChanging, AddressOf ElsaBeforeColUpdate
      AddHandler dsElsaShared.Tables("LISTSAR").ColumnChanged, AddressOf ElsaAfterColUpdate

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      dsElsaShared.Tables("LISTSAR").Columns("codditt").DefaultValue = strDittaCorrente
      dsElsaShared.Tables("LISTSAR").Columns("lsa_codlsar").DefaultValue = strCodLSar
      dsElsaShared.Tables("LISTSAR").AcceptChanges()

      bElsaHasChanges = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ElsaSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not ElsaTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldLsar.ScriviTabellaSemplice(strDittaCorrente, "LISTSAR", dsElsaShared.Tables("LISTSAR"), "", "", "")

      If bResult Then
        bElsaHasChanges = False
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
  Public ReadOnly Property ElsaRecordIsChanged() As Boolean
    Get
      Return bElsaHasChanges
    End Get
  End Property
  Public Overridable Function ElsaTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Dim dttTmp As New DataTable
    Dim strMsg As String = ""
    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsElsaShared.Tables("LISTSAR").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      If Not CheckInPromozioni(NTSCInt(dtrCurrRow(i)!lsa_codlsar)) Then Return False

      For i = 0 To dtrCurrRow.Length - 1
        If Trim(NTSCStr(dtrCurrRow(i)!lsa_codart)) = "" Then
          If Not bInImport Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128522220458276255, "Codice articolo obbligatorio")))
          Else
            LogWrite(oApp.Tr(Me, 130356401776517078, "Codice articolo obbligatorio"), True)
          End If
          Return False
        End If

        If Not oCldLsar.ValCodiceDb(dtrCurrRow(i)!lsa_codart.ToString, strDittaCorrente, "ARTICO", "S", strTmp, dttTmp) Then
          If Not bInImport Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129030080038044015, "Codice articolo non corretto")))
          Else
            LogWrite(oApp.Tr(Me, 130356402024326230, "Codice articolo non corretto"), True)
          End If
          Return False
        Else
          If NTSCStr(dttTmp.Rows(0)!ar_gesfasi) = "S" And NTSCInt(dtrCurrRow(i)!lsa_fase) = 0 Then
            If Not bInImport Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128523179645830947, "L'articolo è gestito per fasi e pertanto deve essere indicato un codice fase valido.")))
            Else
              LogWrite(oApp.Tr(Me, 130356402519550280, "L'articolo è gestito per fasi e pertanto deve essere indicato un codice fase valido."), True)
            End If
            Return False
          End If
        End If

        If NTSCInt(dtrCurrRow(i)!lsa_commeca) <> 0 Then
          If Not oCldLsar.ValCodiceDb(dtrCurrRow(i)!lsa_codart.ToString, strDittaCorrente, "ARTICO", "S", strTmp, dttTmp) Then
            If Not bInImport Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129030079979762019, "Codice articolo non corretto")))
            Else
              LogWrite(oApp.Tr(Me, 130356402794578725, "Codice articolo non corretto"), True)
            End If
            Return False
          Else
            If NTSCStr(dttTmp.Rows(0)!ar_gescomm) <> "S" Then
              If Not bInImport Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128522223549960524, "Attenzione!" & vbCrLf & _
                  "L'articolo indicato non è gestito a commesse." & vbCrLf & _
                  "Selezione numero di commessa non possibile.")))
              Else
                LogWrite(oApp.Tr(Me, 130356403070076235, "Attenzione!" & vbCrLf & _
                  "L'articolo indicato non è gestito a commesse." & vbCrLf & _
                  "Selezione numero di commessa non possibile."), True)
              End If
              Return False
            End If
          End If
        End If

        '-------------------------------
        'test lotto univoco
        If bLottoUnivoco Then
          If Not oCldLsar.LottoxCheckLottoUnivoco(strDittaCorrente, NTSCStr(dtrCurrRow(i)!lsa_codart), _
                                                  NTSCStr(dtrCurrRow(i)!xx_lottox), strTmp) Then
            If Not bInImport Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129526281562842028, "Test lotto univoco: Il lotto |" & NTSCStr(dtrCurrRow(i)!xx_lottox) & "| impostato sull'articolo |" & NTSCStr(dtrCurrRow(i)!lsa_codart) & "| è già stato utilizzato per l'articolo |" & strTmp & "|")))
            Else
              LogWrite(oApp.Tr(Me, 130356403449679239, "Test lotto univoco: Il lotto |" & NTSCStr(dtrCurrRow(i)!xx_lottox) & "| impostato sull'articolo |" & NTSCStr(dtrCurrRow(i)!lsa_codart) & "| è già stato utilizzato per l'articolo |" & strTmp & "|"), True)
            End If
            Return False
          End If
        End If

        If Not ArticoloGiaEsistente(dtrCurrRow(i)) Then
          Return False
        End If
      Next

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      dtrTmp = dsElsaShared.Tables("LISTSAR").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
      " AND lsa_codlsar = " & dtrCurrRow(0)!lsa_codlsar.ToString & " AND lsa_riga = " & dtrCurrRow(0)!lsa_riga.ToString)
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        If Not bInImport Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222107500000, "Esiste gia una riga con le stesse caratteristiche")))
        Else
          LogWrite(oApp.Tr(Me, 130356403798078626, "Esiste gia una riga con le stesse caratteristiche"), True)
        End If
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
      Return False
    End Try
  End Function
  Public Overridable Sub ElsaNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsElsaShared.Tables("LISTSAR").Rows.Add(dsElsaShared.Tables("LISTSAR").NewRow)
      bElsaHasChanges = True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Function ElsaRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsElsaShared.Tables("LISTSAR").Select(strFilter)(nRow).RejectChanges()
      bElsaHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub ElsaBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '-------------------------------------------------------------
      'se non  compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName <> "lsa_riga" Then
        If e.Row!lsa_riga.ToString = "0" Then ElsaOnAddNew(sender, e)
      End If

      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "ElsaBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub ElsaAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bElsaHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "ElsaAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub ElsaBeforeColUpdate_lsa_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If NTSCStr(e.ProposedValue) = "" Then
        e.Row!xx_descr = ""
        e.Row!xx_desint = ""
        e.Row!xx_unmis = ""
      Else
        If Not oCldLsar.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", strTmp, dttTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          If Not bInImport Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129030080239765347, "Cod. articolo inesistente.")))
          Else
            LogWrite(oApp.Tr(Me, 130356381483278106, "Cod. articolo inesistente."), True)
          End If
          Return
        Else
          e.ProposedValue = e.ProposedValue.ToString.ToUpper
          If Microsoft.VisualBasic.Right(e.ProposedValue.ToString, 1) = " " Then
            e.ProposedValue = Microsoft.VisualBasic.RTrim(e.ProposedValue.ToString)
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
  Public Overridable Sub ElsaBeforeColUpdate_lsa_fase(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If Not oCldLsar.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTFASI", "N", strTmp, Nothing, NTSCStr(e.Row!lsa_codart)) Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        If Not bInImport Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129030080322110151, "Cod. fase inesistente.")))
        Else
          LogWrite(oApp.Tr(Me, 130356381963746856, "Cod. fase inesistente."), True)
        End If
        Return
      Else
        e.Row!xx_fase = strTmp
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
  Public Overridable Sub ElsaBeforeColUpdate_lsa_commeca(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Dim strMsg As String = ""
    Try
      If Trim(NTSCStr(e.Row!lsa_codart)) = "" Then
        If Not bInImport Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128522242092132290, "Indicare l'articolo prima di inserire la commessa.")))
        Else
          LogWrite(oApp.Tr(Me, 130356382298903106, "Indicare l'articolo prima di inserire la commessa."), True)
        End If
        e.ProposedValue = e.Row(e.Column.ColumnName)
        Return
      End If

      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_commeca = ""
      Else
        If Not oCldLsar.ValCodiceDb(e.Row!lsa_codart.ToString, strDittaCorrente, "ARTICO", "S", strTmp, dttTmp) Then
          If Not bInImport Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128522244226263797, "Codice articolo non corretto")))
          Else
            LogWrite(oApp.Tr(Me, 130356382811246856, "Codice articolo non corretto"), True)
          End If
          Return
        Else
          If NTSCStr(dttTmp.Rows(0)!ar_gescomm) <> "S" Then
            e.ProposedValue = e.Row(e.Column.ColumnName)
            If Not bInImport Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128522244444487403, "Attenzione!" & vbCrLf & _
                "L'articolo indicato non è gestito a commesse." & vbCrLf & _
                "Selezione numero di commessa non possibile.")))
            Else
              LogWrite(oApp.Tr(Me, 130356383087496856, "Attenzione!" & vbCrLf & _
                "L'articolo indicato non è gestito a commesse." & vbCrLf & _
                "Selezione numero di commessa non possibile."), True)
            End If
            Return
          End If
        End If

        If Not oCldLsar.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "COMMESS", "N", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          If Not bInImport Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129030080364923199, "Cod. commessa inesistente.")))
          Else
            LogWrite(oApp.Tr(Me, 130356383400153106, "Cod. commessa inesistente."), True)
          End If
          Return
        Else
          e.Row!xx_commeca = strTmp
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
  Public Overridable Sub ElsaBeforeColUpdate_lsa_ubicaz(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.ProposedValue = e.ProposedValue.ToString.ToUpper
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ElsaBeforeColUpdate_xx_lottox(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If NTSCStr(e.Row!lsa_codart).Trim = "" Then
        'prima di indicare il lotto devo indicare l'articolo 
        e.ProposedValue = ""
        If Not bInImport Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513889258401769, "Prima di indicare il lotto inserire il cod. articolo")))
        Else
          LogWrite(oApp.Tr(Me, 130356383899684356, "Prima di indicare il lotto inserire il cod. articolo"), True)
        End If
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
            If Not bInImport Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513977264885199, "Il codice lotto deve essere un numero compreso tra 0 e 999999999")))
            Else
              LogWrite(oApp.Tr(Me, 130356384382965606, "Il codice lotto deve essere un numero compreso tra 0 e 999999999"), True)
            End If
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

  Public Overridable Sub ElsaAfterColUpdate_lsa_codart(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      If Not oCldLsar.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ARTICO", "S", strTmp, dttTmp) Then
        Return
      Else
        e.Row!xx_descr = dttTmp.Rows(0)!ar_descr
        e.Row!xx_desint = dttTmp.Rows(0)!ar_desint
        e.Row!xx_unmis = dttTmp.Rows(0)!ar_unmis
        e.Row!xx_lottox = ""
        If NTSCStr(dttTmp.Rows(0)!ar_gesfasi) = "N" Then
          e.Row!lsa_fase = 0
          e.Row!xx_fase = ""
        Else
          e.Row!lsa_fase = NTSCInt(dttTmp.Rows(0)!ar_ultfase)
          If NTSCInt(dttTmp.Rows(0)!ar_ultfase) <> 0 Then
            If oCldLsar.ValCodiceDb(NTSCStr(e.Row!lsa_fase), strDittaCorrente, "ARTFASI", "N", strTmp, Nothing, e.ProposedValue.ToString) Then
              e.Row!xx_fase = strTmp
            End If
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
  Public Overridable Sub ElsaAfterColUpdate_xx_lottox(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dttTmp As New DataTable
    Dim lIdLotto As Integer = 0
    Dim strErr As String = ""

    Try
      'dato il lotto alfanumerico devo tradure in ID lotto numerico
      If NTSCStr(e.ProposedValue) = "" Then
        e.Row!lsa_lotto = 0
        Return
      End If

      'dato il lotto alfanumerico ottengo l'ID numerico
      oCldLsar.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANALOTTIX", "S", "", dttTmp, NTSCStr(e.Row!lsa_codart))
      If dttTmp.Rows.Count = 0 Then
        'creo l'anagrafica lotti
        If Not CType(oCleComm, CLELBMENU).CreaAnalottiDaLottox(strDittaCorrente, NTSCStr(e.Row!lsa_codart), _
                                          DateTime.Now.ToShortDateString, _
                                          NTSCStr(e.ProposedValue), "", lIdLotto, strErr) Then
          If Not bInImport Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129513215192910290, "Codice lotto inesistente") & vbCrLf & strErr))
          Else
            LogWrite(oApp.Tr(Me, 130356385682968629, "Codice lotto inesistente") & vbCrLf & strErr, True)
          End If
          e.Row!xx_lottox = ""
          Return
        Else
          If strErr <> "" Then
            If Not bInImport Then
              ThrowRemoteEvent(New NTSEventArgs("", strErr))
            Else
              LogWrite(strErr, True)
            End If
          End If
        End If
      Else
        lIdLotto = NTSCInt(dttTmp.Rows(0)!alo_lotto)
      End If

      If NTSCInt(e.Row!lsa_lotto) <> lIdLotto Then e.Row!lsa_lotto = lIdLotto

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

  Public Overridable Sub Seleziona(ByVal btlbImpostaStato As Boolean)
    Dim dReturn As Boolean = False
    Dim dMaxRiga As Decimal
    Dim dsMaxListsar As DataSet = Nothing
    Dim nOrdin As Integer = 1
    Try
      '-----------------------------------------------------------------------------------------
      '--- Calcola la riga massima di LISTSAR
      '-----------------------------------------------------------------------------------------
      dReturn = oCldLsar.GetMaxListsar(strDittaCorrente, NTSCStr(strCodLSar), dsMaxListsar)

      If dsMaxListsar.Tables("LISTSAR").Rows.Count > 0 Then
        dMaxRiga = NTSCInt(dsMaxListsar.Tables("LISTSAR").Rows(0)!Riga)
      Else
        dMaxRiga = 0
      End If

      If bOrdinaCodart Then nOrdin = 1
      If bOrdinaDescr Then nOrdin = 2
      If bOrdinaCodalt Then nOrdin = 3

      oCldLsar.Seleziona(strDittaCorrente, strCodLSar, dMaxRiga, strWhereFiar, btlbImpostaStato, nOrdin, _
      bMovimentaz, bEsplComb, bEsistMagZero, bEsistMinZero, bEsplFasi, nCodMagP)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function RecordBlocca() As Boolean
    Try
      If oCldLsar.RecordBlocca(strDittaCorrente, strCodLSar) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128522926654788949, "Blocco eseguito.")))
        Return True
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

  Public Overridable Function RecordSblocca() As Boolean
    Try
      If oCldLsar.RecordSblocca(strDittaCorrente, strCodLSar) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129030080408673759, "Sblocco eseguito.")))
        Return True
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

  Public Overridable Function GetQueryStampaWord(ByVal strCodLSar As String) As String
    Try
      Return oCldLsar.GetQueryStampaWord(strDittaCorrente, strCodLSar)
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

  Public Overridable Function ArticoloGiaEsistente(ByVal dtrCurrRow As DataRow) As Boolean
    Dim dttTmp As New DataTable
    Dim dsTmp As DataSet = Nothing
    Dim strMessaggio As String = ""
    Dim evt As NTSEventArgs = Nothing
    Try
      If bInImport Then Return True 'Ignoro questa verifica durante l'importazione da file terminale

      If Not oCldLsar.ValCodiceDb(dtrCurrRow!lsa_codart.ToString, strDittaCorrente, "ARTICO", "S", , dttTmp) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129030079922261283, "Codice articolo non corretto")))
        Return False
      Else
        bGesfasi = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ar_gesfasi) = "N", False, True))
        bGeslotti = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ar_geslotti) = "N", False, True))
        bGesubic = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ar_gesubic) = "N", False, True))
        bGescomm = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ar_gescomm) = "N", False, True))
        bGestmatr = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ar_gestmatr) = "N", False, True))
      End If

      oCldLsar.ControllaFasLotUbiComMat(strDittaCorrente, bGesfasi, bGeslotti, bGesubic, _
                                        bGescomm, bGestmatr, NTSCStr(dtrCurrRow!lsa_codart), strCodLSar, _
                                        NTSCStr(dtrCurrRow!lsa_fase), NTSCStr(dtrCurrRow!lsa_riga), bMsgArticoloDuplicato, NTSCStr(dtrCurrRow!lsa_lotto), _
                                        NTSCStr(dtrCurrRow!lsa_ubicaz), NTSCStr(dtrCurrRow!lsa_commeca), NTSCStr(dtrCurrRow!lsa_matric), dsTmp)

      If dsTmp.Tables("LISTSAR").Rows.Count > 0 Then
        strMessaggio = oApp.Tr(Me, 129030087828331956, "Attenzione!" & vbCrLf & _
          "Articolo")
        If bGesfasi = True Then strMessaggio = strMessaggio & oApp.Tr(Me, 129030087989905830, "/Fase")
        If bMsgArticoloDuplicato = True Then
          If bGeslotti = True Then strMessaggio = strMessaggio & oApp.Tr(Me, 129030088345087083, "/Lotto")
          If bGesubic = True Then strMessaggio = strMessaggio & oApp.Tr(Me, 129030088374620412, "/Ubicazione")
          If bGescomm = True Then strMessaggio = strMessaggio & oApp.Tr(Me, 129030088402591131, "/Commessa")
          If bGestmatr = True Then strMessaggio = strMessaggio & oApp.Tr(Me, 129030088427436630, "/Matricola")
        End If
        strMessaggio = strMessaggio & vbCrLf
        If (bGesfasi = False) And (bGeslotti = False) And (bGesubic = False) And _
           (bGescomm = False) And (bGestmatr = False) Then
          strMessaggio = oApp.Tr(Me, 128693136738612188, "Attenzione!" & vbCrLf & _
            "Articolo già esistente." & vbCrLf)
        Else
          strMessaggio = strMessaggio & oApp.Tr(Me, 129030087231729843, "Attenzione!" & vbCrLf & _
            "Articoli già esistenti.") & vbCrLf
        End If
        strMessaggio = strMessaggio & oApp.Tr(Me, 129030087399551772, "Confermare ugualmente?")
      End If
      '----------------------------------------------------------------------------------------
      If strMessaggio <> "" Then
        If Not bInImport Then
          evt = New NTSEventArgs(ThMsg.MSG_YESNO, strMessaggio)
          ThrowRemoteEvent(evt)
          If Not evt.RetValue = "YES" Then
            Return False
          End If
        Else
          LogWrite(strMessaggio, True)
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

  Public Overridable Function ImpostaTrattatoElsa(ByVal strTrattato As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldLsar.ImpostaTrattatoElsa(strDittaCorrente, strCodLSar, strTrattato)
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

#Region "funzioni specifiche per BNNGLSAR.BNMGTERM.VB"
  Public Overridable Function TerminaleLeggiCheck(ByVal strCaller As String) As Boolean
    Try
      'File terminale
      strTermPrgOwner = strCaller
      bTermExecute = CBool(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermExecute", "0", " ", "0")))
      strTermCommand = ""
      bTermSilent = False
      If bTermExecute Then
        strTermCommand = oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermCommand", "", " ", "")
        bTermSilent = CBool(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermSilent", "0", " ", "0")))
      End If
      strTermFileName = oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermFileName", "", " ", "")
      bTermDeleteAfter = CBool(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDeleteAfter", "-1", " ", "-1")))
      bTermIgnoreFirstRow = CBool(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermIgnoreFirstRow", "0", " ", "0")))
      bTermShowLogErr = CBool(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermShowLogErr", "-1", " ", "-1")))
      strTermFileType = UCase$(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermFileType", "F", " ", "F"))
      If strTermFileType <> "F" And strTermFileType <> "S" And strTermFileType <> "C" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129247189375625000, "Valore della proprietà '|" & strTermPrgOwner & "|\|" & strTermKey & "|\TermFileType' (|" & strTermFileType & "|) non corretto, verrà utilizzato 'F'.")))
        strTermFileType = "F"
      End If
      strTermFieldSep = ""
      If strTermFileType = "S" Then
        strTermFieldSep = Microsoft.VisualBasic.Left(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermFieldSep", ";", " ", ";"), 1)
      End If
      strTermDecimalSep = Microsoft.VisualBasic.Left(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDecimalSep", ",", " ", ","), 1)
      If strTermDecimalSep <> "," And strTermDecimalSep <> "." Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129247189556718750, "Valore delle proprietà '|" & strTermPrgOwner & "|\|" & strTermKey & "|\TermDecimalSep' (|" & strTermDecimalSep & "|) non corretto, verrà utilizzato ','.")))
        strTermDecimalSep = ","
      End If
      strTermTipoArt = UCase$(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermTipoArt", "B", " ", "B"))
      If strTermTipoArt <> "C" And strTermTipoArt <> "B" And strTermTipoArt <> "X" Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571261447812500, "Valore della proprietà '|" & strTermPrgOwner & "|\|" & strTermKey & "|\TermTipoArt' (|" & strTermTipoArt & "|) non corretto, verrà utilizzato 'B'.")))
        strTermTipoArt = "B"
      End If
      bTermUbic = CBool(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermUbic", "0", " ", "0")))
      bTermLotto = CBool(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermLotto", "-1", " ", "-1")))
      bTermFase = CBool(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermFase", "-1", " ", "-1")))
      bTermCom = CBool(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermCom", "-1", " ", "-1")))
      bTermMatr = CBool(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermMatr", "0", " ", "0")))
      bTermEsis = CBool(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermEsis", "0", " ", "0")))
      nTermCodartPos = 0
      nTermFasePos = 0
      nTermComPos = 0
      nTermLottoPos = 0
      nTermUbicPos = 0
      nTermMatrPos = 0
      nTermEsisPos = 0
      nTermCodartStart = 0
      nTermCodartStop = 0
      nTermFaseStart = 0
      nTermFaseStop = 0
      nTermComStart = 0
      nTermComStop = 0
      nTermLottoStart = 0
      nTermLottoStop = 0
      nTermUbicStart = 0
      nTermUbicStop = 0
      nTermMatrStart = 0
      nTermMatrStop = 0
      nTermEsisStart = 0
      nTermEsisStop = 0
      nTermDivEsis = 1
      If Not (strTermFileType = "F") Then
        nTermCodartPos = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermCodartPos", "1", " ", "1")))
        If bTermFase Then
          nTermFasePos = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermFasePos", "2", " ", "2")))
        End If
        If bTermCom Then
          nTermComPos = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermComPos", "2", " ", "2")))
        End If
        If bTermLotto Then
          nTermLottoPos = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermLottoPos", "3", " ", "3")))
        End If
        If bTermUbic Then
          nTermUbicPos = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermUbicPos", "4", " ", "4")))
        End If
        If bTermMatr Then
          nTermMatrPos = CInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermMatrPos", "5", " ", "5")))
        End If
        If bTermEsis Then
          nTermEsisPos = CInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermEsisPos", "6", " ", "6")))
        End If
        nTermCodartStart = 0
        nTermCodartStop = 0
        nTermFaseStart = 0
        nTermFaseStop = 0
        nTermComStart = 0
        nTermComStop = 0
        nTermLottoStart = 0
        nTermLottoStop = 0
        nTermUbicStart = 0
        nTermUbicStop = 0
        nTermMatrStart = 0
        nTermEsisStart = 0
        nTermMatrStop = 0
        nTermEsisStop = 0
      Else
        nTermCodartPos = 0
        nTermFasePos = 0
        nTermComPos = 0
        nTermLottoPos = 0
        nTermUbicPos = 0
        nTermMatrPos = 0
        nTermEsisPos = 0
        nTermCodartStart = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermCodartStart", "1", " ", "1")))
        nTermCodartStop = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermCodartStop", "13", " ", "13"))) '13 char
        If bTermFase Then
          nTermFaseStart = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermFaseStart", "14", " ", "14")))
          nTermFaseStop = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermFaseStop", "54", " ", "54"))) '40 char
        End If
        If bTermCom Then
          nTermComStart = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermComStart", "55", " ", "55")))
          nTermComStop = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermComStop", "95", " ", "95"))) '40 char
        End If
        If bTermLotto Then
          nTermLottoStart = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermLottoStart", "96", " ", "96")))
          nTermLottoStop = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermLottoStop", "102", " ", "102"))) '6 char
        End If
        If bTermUbic Then
          nTermUbicStart = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermUbicStart", "103", " ", "103")))
          nTermUbicStop = NTSCInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermUbicStop", "113", " ", "113"))) '10 char
        End If
        If bTermMatr Then
          nTermMatrStart = CInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermMatrStart", "114", " ", "114")))
          nTermMatrStop = CInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermMatrStop", "124", " ", "124"))) '10 char
        End If
        If bTermEsis Then
          nTermEsisStart = CInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermEsisStart", "125", " ", "125")))
          nTermEsisStop = CInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermEsisStop", "135", " ", "135"))) '10 char
        End If
      End If
      If bTermEsis Then
        nTermDivEsis = CInt(Val(oCldLsar.GetSettingBusDitt(strDittaCorrente, strTermPrgOwner, strTermKey, ".", "TermDivEsis", "1", " ", "1")))
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
  Public Overridable Function TerminaleLeggiImportaFile(Optional ByVal bSelOrdini As Boolean = False) As Boolean
    Dim dttTmp As New DataTable
    Dim strFile As String = ""
    Dim strLine() As String = Nothing
    Dim lRow As Integer = 0
    Dim lRowScart As Integer = 0
    Dim lRowOK As Integer = 0
    Dim strArt As String = ""
    Dim nFase As Integer = 0
    Dim nCom As Integer = 0
    Dim nLotto As Integer = 0
    Dim strLottoX As String = ""
    Dim strUbic As String = ""
    Dim strMatr As String = ""
    Dim dEsis As Decimal = 0
    Dim strOrder As String = ""

    Try
      bInImport = True
      '--------------------------------------------------------------------------------------------------------------
      '--- Obbliga che sia presente l'articolo D
      '--------------------------------------------------------------------------------------------------------------
      oCldLsar.ValCodiceDb("D", strDittaCorrente, "ARTICO", "S", "", dttTmp)
      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571314396718750, "E' obbligatorio che sia codificato l'articolo 'D' (descrittivo) in anagrafica articoli.")))
        dttTmp.Clear()
        Return False
      End If
      dttTmp.Clear()
      '--------------------------------------------------------------------------------------------------------------
      strOrder = oCldLsar.GetSettingBusDitt(strDittaCorrente, "BSVEHLBC", "OPZIONI", ".", "OrdineTrattamentoRigheOrdine", "DO", " ", "DO")  ' NON DOCUMENTARE
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
        nFase = 0
        nCom = 0
        nLotto = 0
        dEsis = 0
        'inserisco righe in bsmglsar
        If TerminaleLeggiSplitRow(lRow, strLine(lRow), strArt, nFase, nCom, nLotto, strUbic, strMatr, dEsis, strLottoX) Then
          If TerminaleLeggiMakeRow(strArt, nFase, nCom, nLotto, strUbic, strMatr, dEsis, strLottoX) Then lRowOK += 1
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
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571326541718750, "|" & lRowOK & "| righe importate correttamente, su |" & lRow & "| totali.")))
        End If
      Else
        If lRowOK = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571326802656250, "Nessuna riga importata.")))
        Else
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128571326938750000, "|" & lRowOK & "| righe importate.")))
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
    Finally
      bInImport = False
    End Try
  End Function
  Public Overridable Function TerminaleLeggiSplitRow(ByVal lRow As Integer, ByVal strLine As String, _
    ByRef strArt As String, ByRef nFase As Integer, ByRef nCom As Integer, ByRef nLotto As Integer, _
    ByRef strUbic As String, ByRef strMatr As String, ByRef dEsis As Decimal) As Boolean

    Try
      'obsoleta
      Return TerminaleLeggiSplitRow(lRow, strLine, strArt, nFase, nCom, nLotto, strUbic, strMatr, dEsis, ".")

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function TerminaleLeggiSplitRow(ByVal lRow As Integer, ByVal strLine As String, _
    ByRef strArt As String, ByRef nFase As Integer, ByRef nCom As Integer, ByRef nLotto As Integer, _
    ByRef strUbic As String, ByRef strMatr As String, ByRef dEsis As Decimal, ByRef strLottoX As String) As Boolean
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim Y As Integer = 0
    Dim bTrov As Boolean = False
    Dim bLottoX As Boolean = False   'se true la ditta gestisce i lotti alfanumerici
    Dim dttTmp As New DataTable

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {lRow, strLine, strArt, nFase, nCom, nLotto, strUbic, strMatr, dEsis, strLottoX})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strArt = CType(oIn(2), String)
        nFase = NTSCInt(oIn(3))
        nCom = NTSCInt(oIn(4))
        nLotto = NTSCInt(oIn(5))
        strUbic = CType(oIn(6), String)
        strMatr = CType(oIn(7), String)
        dEsis = NTSCDec(oIn(8))
        strLottoX = CType(oIn(9), String)
        Return CBool(oOut)
      End If
      '----------------

      If strLottoX = "." Then
        'vecchia chamata: non gestisce i lotti alfanumerici
        bLottoX = False
      Else
        'nuova chiamata
        strLottoX = ""
        oCldLsar.ValCodiceDb(strDittaCorrente, strDittaCorrente, "ANADITAC", "S", "", dttTmp)
        If dttTmp.Rows.Count > 0 AndAlso NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S" Then bLottoX = True
        dttTmp.Clear()
      End If

      Select Case strTermFileType
        Case "F"
          '--------------------------
          'Caso FIXED
          strArt = Mid(strLine, nTermCodartStart, nTermCodartStop - nTermCodartStart + 1)
          strArt = RTrim(strArt)
          nFase = 0
          If bTermFase Then
            If IsNumeric(Mid(strLine, nTermFaseStart, nTermFaseStop - nTermFaseStart + 1)) Then
              nFase = NTSCInt(Mid(strLine, nTermFaseStart, nTermFaseStop - nTermFaseStart + 1))
            Else
              LogWrite(oApp.Tr(Me, 128571334703125000, "Il campo fase non risulta essere numerico: '|" & Mid(strLine, nTermFaseStart, nTermFaseStop - nTermFaseStart + 1) & "|' nella linea |" & lRow & "| e verrà considerata fase a zero: '|" & strLine & "|'."), True)
              nFase = 0
            End If
          End If
          nCom = 0
          If bTermCom Then
            If IsNumeric(Mid(strLine, nTermComStart, nTermComStop - nTermComStart + 1)) Then
              nCom = NTSCInt(Mid(strLine, nTermComStart, nTermComStop - nTermComStart + 1))
            Else
              LogWrite(oApp.Tr(Me, 130356310569423156, "Il campo commessa non risulta essere numerico: '|" & Mid(strLine, nTermComStart, nTermComStop - nTermComStart + 1) & "|' nella linea |" & lRow & "| e verrà considerata commessa a zero: '|" & strLine & "|'."), True)
              nCom = 0
            End If
          End If
          strLottoX = ""
          nLotto = 0
          If bTermLotto Then
            If bLottoX Then
              strLottoX = Mid(strLine, nTermLottoStart, nTermLottoStop - nTermLottoStart + 1).Trim
            Else
              If IsNumeric(Mid(strLine, nTermLottoStart, nTermLottoStop - nTermLottoStart + 1)) Then
                nLotto = NTSCInt(Mid(strLine, nTermLottoStart, nTermLottoStop - nTermLottoStart + 1))
              Else
                LogWrite(oApp.Tr(Me, 130356310597392085, "Il campo fase non risulta essere numerico: '|" & Mid(strLine, nTermLottoStart, nTermLottoStop - nTermLottoStart + 1) & "|' nella linea |" & lRow & "| e verrà considerata fase a zero: '|" & strLine & "|'."), True)
                nLotto = 0
              End If
            End If
          End If
          strUbic = ""
          If bTermUbic Then
            strUbic = Mid(strLine, nTermUbicStart, nTermUbicStop - nTermUbicStart + 1)
            strUbic = RTrim(strUbic)
          End If
          strMatr = ""
          If bTermMatr Then
            strMatr = Mid(strLine, nTermMatrStart, nTermMatrStop - nTermMatrStart + 1)
            strMatr = RTrim(strMatr)
          End If
          dEsis = 0
          If bTermEsis Then
            If strTermDecimalSep = "," Then
              If IsNumeric(Mid(strLine, nTermEsisStart, nTermEsisStop - nTermEsisStart + 1)) Then
                dEsis = NTSCInt(Mid(strLine, nTermEsisStart, nTermEsisStop - nTermEsisStart + 1))
              Else
                LogWrite(oApp.Tr(Me, 130356310643642381, "Il campo esistenza non risulta essere numerico: '|" & Mid(strLine, nTermEsisStart, nTermEsisStop - nTermEsisStart + 1) & "|' nella linea |" & lRow & "| e verrà considerata esistenza a zero: '|" & strLine & "|'."), True)
                dEsis = 0
              End If
            Else
              'punto come separatore
              dEsis = NTSCDec(Mid(strLine, nTermEsisStart, nTermEsisStop - nTermEsisStart + 1).Replace(".", ","))
            End If
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
            LogWrite(oApp.Tr(Me, 128571335681875000, "Codice articolo non trovato nella posizione |" & nTermCodartPos & "|; la riga verrà scartata. |'" & strLine & "'|"), True)
            Return False
          End If
          strArt = RTrim(strArt)
          '2)fase
          nFase = 0
          If bTermFase Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermFasePos Then
                'trovato
                bTrov = True
                If IsNumeric(Mid(strLine, Y + 1, i - (Y + 1))) Then
                  nFase = NTSCInt(Mid(strLine, Y + 1, i - (Y + 1)))
                Else
                  LogWrite(oApp.Tr(Me, 130356310950050592, "Il campo fase non risulta essere numerico: '|" & Mid(strLine, Y + 1, i - (Y + 1)) & "|' nella linea |" & lRow & "| e verrà considerata fase a zero: '|" & strLine & "|'."), True)
                  nFase = 0
                End If
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '---------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermFasePos Then
                  'trovato
                  bTrov = True
                  If IsNumeric(Mid(strLine, Y + 1)) Then
                    nFase = NTSCInt(Mid(strLine, Y + 1))
                  Else
                    LogWrite(oApp.Tr(Me, 130356311078020161, "Il campo fase non risulta essere numerico: '|" & Mid(strLine, Y + 1) & "|' nella linea |" & lRow & "| e verrà considerata fase a zero: '|" & strLine & "|'."), True)
                    nFase = 0
                  End If
                  Exit Do
                End If
              End If
              '-----------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 130356311289740266, "Fase non trovato nella posizione |" & nTermFasePos & "|; verà utilizata fase a zero. |'" & strLine & "'|"), True)
            End If
          End If
          '3)commessa
          nCom = 0
          If bTermCom Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermComPos Then
                'trovato
                bTrov = True
                If IsNumeric(Mid(strLine, Y + 1, i - (Y + 1))) Then
                  nCom = NTSCInt(Mid(strLine, Y + 1, i - (Y + 1)))
                Else
                  LogWrite(oApp.Tr(Me, 130356310827393557, "Il campo commessa non risulta essere numerico: '|" & Mid(strLine, Y + 1, i - (Y + 1)) & "|' nella linea |" & lRow & "| e verrà considerata commessa a zero: '|" & strLine & "|'."), True)
                  nCom = 0
                End If
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '---------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermComPos Then
                  'trovato
                  bTrov = True
                  If IsNumeric(Mid(strLine, Y + 1)) Then
                    nCom = NTSCInt(Mid(strLine, Y + 1))
                  Else
                    LogWrite(oApp.Tr(Me, 130356311115364150, "Il campo commessa non risulta essere numerico: '|" & Mid(strLine, Y + 1) & "'| nella linea |" & lRow & "| e verrà considerata commessa a zero: '|" & strLine & "|'."), True)
                    nCom = 0
                  End If
                  Exit Do
                End If
              End If
              '-----------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 128571335354062500, "Commessa non trovato nella posizione |" & nTermFasePos & "|; verà utilizata commessa a zero. '|" & strLine & "|'"), True)
            End If
          End If
          '4)lotto
          strLottoX = ""
          nLotto = 0
          If bTermLotto Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermLottoPos Then
                'trovato
                bTrov = True
                If bLottoX Then
                  strLottoX = Mid(strLine, Y + 1, i - (Y + 1)).Trim
                Else
                  If IsNumeric(Mid(strLine, Y + 1, i - (Y + 1))) Then
                    nLotto = NTSCInt(Mid(strLine, Y + 1, i - (Y + 1)))
                  Else
                    LogWrite(oApp.Tr(Me, 130356310867550064, "Il campo lotto non risulta essere numerico: '|" & Mid(strLine, Y + 1, i - (Y + 1)) & "|' nella linea |" & lRow & "| e verrà considerata lotto a zero: '|" & strLine & "|'."), True)
                    nLotto = 0
                  End If
                End If
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '---------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermLottoPos Then
                  'trovato
                  bTrov = True
                  If bLottoX Then
                    strLottoX = Mid(strLine, Y + 1).Trim
                  Else
                    If IsNumeric(Mid(strLine, Y + 1)) Then
                      nLotto = NTSCInt(Mid(strLine, Y + 1))
                    Else
                      LogWrite(oApp.Tr(Me, 130356311154114398, "Il campo lotto non risulta essere numerico: '|" & Mid(strLine, Y + 1) & "|' nella linea |" & lRow & "| e verrà considerata lotto a zero: '|" & strLine & "|'."), True)
                      nLotto = 0
                    End If
                  End If
                  Exit Do
                End If
              End If
              '-----------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 130356311381459603, "Lotto non trovato nella posizione |" & nTermLottoPos & "|; verà utilizata lotto a zero. '|" & strLine & "|'"), True)
            End If
          End If
          '5)ubicazione
          If bTermUbic = True Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermUbicPos Then
                'trovato
                bTrov = True
                strUbic = Mid(strLine, Y + 1, i - (Y + 1))
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '---------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermUbicPos Then
                  'trovato
                  bTrov = True
                  strUbic = Mid(strLine, Y + 1)
                  Exit Do
                End If
              End If
              '---------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 130356311501460371, "Ubicazione articolo non trovata nella posizione |" & nTermUbicPos & "|; la riga verrà scartata. |'" & strLine & "'|"), True)
              Return False
            End If
            strUbic = RTrim(strUbic)
          End If    'If bTermUbic = True Then
          '6)matricola
          If bTermMatr = True Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermMatrPos Then
                'trovato
                bTrov = True
                strMatr = Mid(strLine, Y + 1, i - (Y + 1))
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '---------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermMatrPos Then
                  'trovato
                  bTrov = True
                  strMatr = Mid(strLine, Y + 1)
                  Exit Do
                End If
              End If
              '---------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 130356311538335607, "Matricola articolo non trovata nella posizione |" & nTermMatrPos & "|; la riga verrà scartata. |'" & strLine & "'|"), True)
              Return False
            End If
            strMatr = RTrim(strMatr)
          End If    'If bTermMatr = True Then
          '7)esistenza
          dEsis = 0
          If bTermEsis Then
            j = 0 : Y = 0
            bTrov = False
            i = InStr(Y + 1, strLine, strTermFieldSep)
            Do While i <> 0
              j = j + 1
              If j = nTermEsisPos Then
                'trovato
                bTrov = True
                If strTermDecimalSep = "," Then
                  If IsNumeric(Mid(strLine, Y + 1, i - (Y + 1))) Then
                    dEsis = NTSCInt(Mid(strLine, Y + 1, i - (Y + 1)))
                  Else
                    LogWrite(oApp.Tr(Me, 130356310908175324, "Il campo esistenza non risulta essere numerico: '|" & Mid(strLine, Y + 1, i - (Y + 1)) & "|' nella linea |" & lRow & "| e verrà considerata esistenza a zero: '|" & strLine & "|'."), True)
                    dEsis = 0
                  End If
                Else
                  'punto come separatore
                  dEsis = NTSCDec(Mid(strLine, Y + 1, i - (Y + 1)).Replace(".", ","))
                End If
                Exit Do
              End If
              Y = i
              i = InStr(Y + 1, strLine, strTermFieldSep)
              '---------------------
              'Ultimo campo
              If i = 0 Then
                j = j + 1
                If j = nTermEsisPos Then
                  'trovato
                  bTrov = True
                  If strTermDecimalSep = "," Then
                    If IsNumeric(Mid(strLine, Y + 1)) Then
                      dEsis = NTSCInt(Mid(strLine, Y + 1))
                    Else
                      LogWrite(oApp.Tr(Me, 128571335407812500, "Il campo esistenza non risulta essere numerico: '|" & Mid(strLine, Y + 1) & "|' nella linea |" & lRow & "| e verrà considerata esistenza a zero: '|" & strLine & "|'."), True)
                      dEsis = 0
                    End If
                  Else
                    'punto come separatore
                    dEsis = NTSCDec(Mid(strLine, Y + 1).Replace(".", ","))
                  End If
                  Exit Do
                End If
              End If
              '-----------------------
            Loop
            If Not bTrov Then
              LogWrite(oApp.Tr(Me, 130356311333646797, "Esistenza non trovato nella posizione |" & nTermEsisPos & "|; verà utilizata esistenza a zero. |'" & strLine & "'|"), True)
            End If
          End If
      End Select
      'Ora applica i divisori
      If bTermEsis Then
        If nTermDivEsis > 1 And nTermDivEsis <= 10000 Then
          dEsis = ArrDbl(dEsis / nTermDivEsis, 3)
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
  Public Overridable Function TerminaleLeggiMakeRow(ByVal strArt As String, ByRef nFase As Integer, ByRef nCom As Integer, ByRef nLotto As Integer, _
  ByRef strUbic As String, ByRef strMatr As String, ByRef dEsis As Decimal) As Boolean
    Try
      'obsoleta
      Return TerminaleLeggiMakeRow(strArt, nFase, nCom, nLotto, strUbic, strMatr, dEsis, "")

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function TerminaleLeggiMakeRow(ByVal strArt As String, ByRef nFase As Integer, ByRef nCom As Integer, ByRef nLotto As Integer, _
    ByRef strUbic As String, ByRef strMatr As String, ByRef dEsis As Decimal, ByRef strLottoX As String) As Boolean
    Dim strTmp As String = ""
    Dim dttTmp, dttTagl As New DataTable
    Dim bAddRow As Boolean = False
    Dim strArt1 As String = ""
    Dim nNcampo As Integer
    Dim dsMaxListsar As DataSet = Nothing
    Dim dtrTmp() As DataRow
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strArt, nFase, nCom, nLotto, strUbic, strMatr, dEsis, strLottoX})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        nFase = NTSCInt(oIn(1))
        nCom = NTSCInt(oIn(2))
        nLotto = NTSCInt(oIn(3))
        strUbic = CType(oIn(4), String)
        strMatr = CType(oIn(5), String)
        dEsis = NTSCDec(oIn(6))
        strLottoX = CType(oIn(7), String)
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      '--- Verifico se l'articolo esiste, in base alla sua tipologia
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTermTipoArt
        Case "C", "X"
          'c = articolo
          'x = articolo o barcode
          oCldLsar.ValCodiceDb(strArt, strDittaCorrente, "ARTICO", "S", "", dttTmp)
          If dttTmp.Rows.Count = 0 And strTermTipoArt = "X" Then
            'lo cerco come barcode
            oCldLsar.ValCodiceDb(strArt, strDittaCorrente, "BARCODE", "S", "", dttTmp)
            If dttTmp.Rows.Count > 0 Then
              strArt = NTSCStr(dttTmp.Rows(0)!bc_codart)
            End If
          End If
          If dttTmp.Rows.Count = 0 Then
            strArt = "D"
          End If
        Case "B"
          oCldLsar.ValCodiceDb(strArt, strDittaCorrente, "BARCODE", "S", "", dttTmp)
          If dttTmp.Rows.Count = 0 Then
            strArt = "D"
          Else
            strArt = NTSCStr(dttTmp.Rows(0)!bc_codart)
          End If
      End Select
      dttTmp.Clear()
      '--------------------------------------------------------------------------------------------------------------
      '--- Se articolo/fase/ubicazione/commessa duplicati salta
      '--------------------------------------------------------------------------------------------------------------
      dtrTmp = dsElsaShared.Tables("LISTSAR").Select("lsa_codart = " & CStrSQL(strArt) & _
                                                     " AND lsa_fase = " & nFase & _
                                                     " AND lsa_ubicaz = " & CStrSQL(strUbic) & _
                                                     " AND lsa_commeca = " & nCom & _
                                                     " AND lsa_matric = " & CStrSQL(strMatr))
      If dtrTmp.Length > 0 Then
        LogWrite(oApp.Tr(Me, 130356387780325802, "Codice articolo '|" & strArt & "|'" & vbCrLf & _
                                                 "    fase '|" & nFase & "|'" & vbCrLf & _
                                                 "    ubicazione '|" & strUbic & "|'" & vbCrLf & _
                                                 "    commessa '|" & nCom & "|'" & vbCrLf & _
                                                 "    matricola '|" & strMatr & "|'" & vbCrLf & _
                                                 "    già presenti nella lista selezionata."), True)
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If strUbic = "" Then
        'LogWrite(oApp.Tr(Me, 130359127703170911, "L'ubicazione non può essere NULL. Assegnato valore di default ' '."), True)
        strUbic = " "
      End If

      'scrive il record
      oCldLsar.GetMaxListsar(strDittaCorrente, strCodLSar, dsMaxListsar)
      If dsMaxListsar.Tables("LISTSAR").Rows.Count > 0 Then
        nNcampo = NTSCInt(dsMaxListsar.Tables("LISTSAR").Rows(0)!Riga)
      Else
        nNcampo = 0
      End If

      dsElsaShared.Tables("LISTSAR").Rows.Add(dsElsaShared.Tables("LISTSAR").NewRow)
      With dsElsaShared.Tables("LISTSAR").Rows(dsElsaShared.Tables("LISTSAR").Rows.Count - 1)
        !codditt = "."
        !codditt = strDittaCorrente
        !lsa_riga = NTSCStr(nNcampo + 1)
        !lsa_trattato = NTSCStr(IIf(bImpostastato = True, "S", "N"))
        !lsa_codlsar = strCodLSar

        !lsa_codart = strArt
        If bTermFase = True Then !lsa_fase = nFase
        If bTermCom = True Then !lsa_commeca = nCom
        If bTermLotto = True Then
          If strLottoX <> "" Then
            !xx_lottox = strLottoX
          Else
            !xx_lottox = nLotto
          End If
        End If
        If bTermUbic = True Then !lsa_ubicaz = strUbic
        If bTermMatr = True Then !lsa_matric = strMatr
        If bTermEsis = True Then !lsa_esist = dEsis
      End With
      '--------------------------------------------------------------------------------------------------------------
      ElsaSalva(False)
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
#End Region

End Class
