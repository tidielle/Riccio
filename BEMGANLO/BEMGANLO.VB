Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGANLO
  Inherits CLE__BASE

  Public oCldAnlo As CLDMGANLO

  Public bLottoNew As Boolean = False     'se true (letto da anaditac) alo_lotto è calcolato sempre in automatico, se false alo_lotto è uguale a alo_lottox e alo_lottx deve essere numerico di max 9 char
  Public bLottoUnivoco As Boolean = False           'se true al salvataggio del documento controllo che per gli articoli gestiti a lotti il lotto sia univoco (ovvero non esistano altri articoli con lo stesso analotti.alo_lottox oltre a quello della riga che sto validando
  Public lLotto As Integer = 0
  Public strLottox As String = ""
  Public strGenNumLotto As String = ""
  Public strCodart As String = ""
  Public strDescodart As String = ""
  Public lParQual As Integer

  Public bHasChangesVall As Boolean
  Public dsSharedVall As DataSet

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModMG + CLN__STD.bsModSQ
  Private ModuliExt_P As Integer = 0
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

#Region "Inizializzazione"
  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGANLO"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldAnlo = CType(MyBase.ocldBase, CLDMGANLO)
    oCldAnlo.Init(oApp)
    Return True
  End Function
#End Region

#Region "FUNZIONI FORM BNMGANLO"
  Public Overridable Shadows Function Apri(ByVal strCodartIn As String, ByVal lLottoIn As Integer, _
    ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim strNomeTabella As String = "ANALOTTI"

    '------------------------------------------------------------------------------------
    strCodart = strCodartIn
    lLotto = lLottoIn
    '------------------------------------------------------------------------------------
    Try
      If (Not ds Is Nothing) Then ds.Tables.Clear()
      dReturn = oCldAnlo.GetData(strDittaCorrente, strCodart, lLotto, ds)
      If dReturn = False Then Return False
      '----------------------------------------------------------------------------------
      If (ds.Tables(strNomeTabella).Rows.Count = 0) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128715632791745929, "Non esistono dati con queste caratteristiche.")))
        Return False
      End If
      '----------------------------------------------------------------------------------
      '--- Imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      '----------------------------------------------------------------------------------
      ocldBase.SetTableDefaultValueFromDB(strNomeTabella, ds)
      '----------------------------------------------------------------------------------
      SetDefaultValue(ds)
      '----------------------------------------------------------------------------------
      dsShared = ds
      '----------------------------------------------------------------------------------
      '--- Creo gli eventi per la gestione del datatable dentro l'entity
      '----------------------------------------------------------------------------------
      AddHandler dsShared.Tables(strNomeTabella).ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables(strNomeTabella).ColumnChanged, AddressOf AfterColUpdate
      '----------------------------------------------------------------------------------
      bHasChanges = False
      '----------------------------------------------------------------------------------
      GetParametroQual(strCodart)
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function ImpostaNuovaRiga() As Boolean
    Try
      '----------------------------------------------------------------------------------
      dsShared.Tables("ANALOTTI").Rows.Add(dsShared.Tables("ANALOTTI").NewRow)
      With dsShared.Tables("ANALOTTI").Rows(dsShared.Tables("ANALOTTI").Rows.Count - 1)
        !alo_lotto = lLotto
        !alo_lottox = strLottox
      End With
      '----------------------------------------------------------------------------------
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

  Public Overridable Function ImpostaNuovoLotto() As Integer
    Dim lNuovoLotto As Integer = 0

    Try
      '----------------------------------------------------------------------------------
      Select Case strGenNumLotto
        Case "A" : lNuovoLotto = oCldAnlo.ProgressivoLottoPerArticolo(strDittaCorrente, strCodart)
        Case "U" : lNuovoLotto = LegNuma()
      End Select
      '----------------------------------------------------------------------------------
      Return lNuovoLotto
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return lNuovoLotto
    End Try
  End Function

  Public Overridable Function Nuovo(ByVal strCodartIn As String, ByVal lLottoIn As Integer, _
    ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim strNomeTabella As String = "ANALOTTI"
    Dim strTmp As String = ""
    '------------------------------------------------------------------------------------
    strCodart = strCodartIn
    lLotto = lLottoIn
    '------------------------------------------------------------------------------------
    Try
      If bLottoUnivoco Then
        If Not oCldAnlo.LottoxCheckLottoUnivoco(strDittaCorrente, strCodart, _
                                                        strLottox, strTmp) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129526281562842028, "Test lotto univoco: Il lotto |" & strLottox & "| impostato sull'articolo |" & strCodart & "| è già stato utilizzato per l'articolo |" & strTmp & "|")))
          Return False
        End If
      End If
      If (Not ds Is Nothing) Then ds.Tables.Clear()
      dReturn = oCldAnlo.GetData(strDittaCorrente, strCodart, lLotto, ds)
      If dReturn = False Then Return False
      '----------------------------------------------------------------------------------
      '--- Imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      ocldBase.SetTableDefaultValueFromDB(strNomeTabella, ds)
      '----------------------------------------------------------------------------------
      SetDefaultValue(ds)
      '----------------------------------------------------------------------------------
      dsShared = ds
      '----------------------------------------------------------------------------------
      '--- Creo gli eventi per la gestione del datatable dentro l'entity
      '----------------------------------------------------------------------------------
      AddHandler dsShared.Tables(strNomeTabella).ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables(strNomeTabella).ColumnChanged, AddressOf AfterColUpdate
      '----------------------------------------------------------------------------------
      bHasChanges = False
      '----------------------------------------------------------------------------------
      GetParametroQual(strCodart)
      '----------------------------------------------------------------------------------
      Return dReturn
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overrides Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Dim dtrD() As DataRow = Nothing
    Dim nRecDel As Integer = 0
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      Else
        Dim dtrTmp As DataRow() = dsShared.Tables("ANALOTTI").Select(Nothing, Nothing, DataViewRowState.Deleted)
        For i As Integer = 0 To dtrTmp.Length - 1
          If Not CancellaVallotti(NTSCStr(dtrTmp(i)("alo_codart", DataRowVersion.Original)), NTSCInt(dtrTmp(i)("alo_lotto", DataRowVersion.Original))) Then Return False
        Next
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      If bDelete Then
        dtrD = dsShared.Tables("ANALOTTI").Select("", "", DataViewRowState.Deleted)
        If Not oCldAnlo.DeleteAnalotti(strDittaCorrente, _
                                       NTSCStr(dtrD(0)("alo_codart", DataRowVersion.Original)), _
                                       NTSCInt(dtrD(0)("alo_lotto", DataRowVersion.Original)), _
                                       nRecDel) Then
          Return False
        Else
          If nRecDel = 0 Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130063443260747646, "Anagrafica lotto non cancellata. Il lotto risulta essere utilizzato in un altra tabella")))
            Return False
          End If
          bResult = True
        End If
      Else
        If strActLog <> "-1" Then
          bResult = ocldBase.ScriviTabellaSemplice(strDittaCorrente, strNomeTabella, dsShared.Tables(strNomeTabella), "", "", "")
        Else
          bResult = ocldBase.ScriviTabellaSemplice(strDittaCorrente, strNomeTabella, dsShared.Tables(strNomeTabella), _
                    strActLogProg, strActLogNomOggLog, strActLogDesLog)
        End If
      End If

      If bResult Then
        bHasChanges = False
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

  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      '----------------------------------------------------------------------------------
      '--- Gestione di actlog
      '--- Settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      '----------------------------------------------------------------------------------
      strActLog = ocldBase.GetSettingBus("BSMGANLO", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
      strActLogProg = "BSMGANLO"
      strActLogNomOggLog = "ANALOTTI"
      strActLogDesLog = oApp.Tr(Me, 128715640666378802, "Dati relativi a Anagrafica Articolo/Lotto")
      '----------------------------------------------------------------------------------
      '--- Ora imposto i valori di default diversi da quelli impostati nel database
      '----------------------------------------------------------------------------------
      ds.Tables("ANALOTTI").Columns("codditt").DefaultValue = strDittaCorrente
      'ds.Tables("ANALOTTI").Columns("alo_codart").DefaultValue = strCodart
      ds.Tables("ANALOTTI").Columns("alo_dtscad").DefaultValue = NTSCDate(IntSetDate("31/12/2099"))
      ds.Tables("ANALOTTI").Columns("alo_dtcarbus").DefaultValue = NTSCDate(DateTime.Now.ToShortDateString)
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overrides Function TestPreSalva() As Boolean
    '-------------------------------------------------
    'eseguo i controlli necessari prima di fare il salvataggio della riga
    Dim dtrTmp As DataRow() = dsShared.Tables("ANALOTTI").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
    Dim i As Integer = 0
    Dim lNum As Integer = 0
    Dim strMsg As String = ""

    Try
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!alo_lotto) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128715837707894589, "Il campo lotto deve contenere un valore diverso da zero")))
          Return False
        Else
          If (dtrTmp(i).RowState = DataRowState.Added) And (strGenNumLotto = "U") Then
            If NTSCInt(dtrTmp(i)!alo_lotto) = LegNuma() Then
              lNum = oCldAnlo.AggNuma(strDittaCorrente, "LO", "", 0, NTSCInt(dtrTmp(i)!alo_lotto), True, True, strMsg)
              If lNum = 0 Then Return False
            End If
          End If
        End If

        If NTSCInt(dtrTmp(i)!alo_codpqua) > 0 Then CheckVallotti(NTSCStr(dtrTmp(i)!alo_codart), NTSCInt(dtrTmp(i)!alo_lotto))
      Next

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      dtrTmp = dsShared.Tables("ANALOTTI").Select("codditt = " & CStrSQL(dtrTmp(0)!codditt.ToString) & _
      " AND alo_codart = " & CStrSQL(dtrTmp(0)!alo_codart.ToString) & " AND alo_lotto = " & dtrTmp(0)!alo_lotto.ToString)
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222107500000, "Esiste gia una riga con le stesse caratteristiche")))
        Return False
      End If

      If dsShared.Tables("ANALOTTI").Select("codditt = " & CStrSQL(dtrTmp(0)!codditt.ToString) & _
                                            " AND alo_codart = " & CStrSQL(dtrTmp(0)!alo_codart.ToString) & _
                                            " AND alo_lotto = " & dtrTmp(0)!alo_lotto.ToString, "", DataViewRowState.Added).Length > 0 Then
        If oCldAnlo.ValCodiceDb(NTSCStr(dtrTmp(0)!alo_lotto), NTSCStr(dtrTmp(0)!codditt.ToString), "ANALOTTI", "N", , , NTSCStr(dtrTmp(0)!alo_codart)) Then
          If strGenNumLotto = "A" Then
            'correggo il num. di lotto e proseguo
            i = oCldAnlo.ProgressivoLottoPerArticolo(strDittaCorrente, strCodart)
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 130189746639098410, "Codice lotto |" & NTSCInt(dtrTmp(0)!alo_lotto) & "| inserito già esistente. Verrà assegato il nuovo codice '|" & i.ToString & "|'")))
            dtrTmp(0)!alo_lotto = i
            dtrTmp(0)!alo_lottox = i.ToString("000000000")
          Else
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129385408221250000, "Codice lotto |" & NTSCInt(dtrTmp(0)!alo_lotto) & "| inserito già esistente")))
            Return False
          End If
        End If
      End If

      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, strMsg, oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, strMsg, oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function

  Public Overridable Function VerificaCampqua(ByVal lCodpqua As Integer) As Boolean
    Dim dttTmp As New DataTable
    Try
      If Not oCldAnlo.VerificaCampqua(strDittaCorrente, lCodpqua, dttTmp) Then Return False

      If dttTmp.Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129376527265803096, "Non esistono dati nella tabella 'Parametri Qualità Lotto'" & vbCrLf & _
                                                                              "Impossibile continuare.")))
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

  Public Overridable Function CancellaVallotti(ByVal strCodart As String, ByVal lLotto As Integer) As Boolean
    Try
      Return oCldAnlo.CancellaVallotti(strDittaCorrente, strCodart, lLotto)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function CheckVallotti(ByVal strArt As String, ByVal lLotto As Integer) As Boolean
    Dim dttTmp As DataTable = Nothing
    Try
      If Not oCldAnlo.CheckVallotti(strDittaCorrente, strArt, lLotto, dttTmp) Then Return False

      'If dttTmp.Rows.Count = 0 Then
      '  oApp.Ms.gBoxInfo("Se è indicato il Codice Parametro qualità lotto, devono essere inseriti anche i valori.")
      '  Return False
      'End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function GetParametroQual(ByVal strCodart As String) As Boolean
    Dim dttArt As New DataTable
    Try
      lParQual = 0

      oCldAnlo.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttArt)

      If dttArt.Rows.Count > 0 Then
        If NTSCInt(dttArt.Rows(0)!ar_sotgru) > 0 Then
          If oCldAnlo.ValCodiceDb(NTSCStr(dttArt.Rows(0)!ar_sotgru), strDittaCorrente, "TABPQUA", "N") Then
            lParQual = NTSCInt(dttArt.Rows(0)!ar_sotgru)
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




  '--- Eventi di GRLIGIA

  Public Overrides Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
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
      If e.Column.ColumnName <> "alo_codart" Then
        If NTSCStr(e.Row!alo_codart).Trim = "" Then OnAddNew(sender, e)
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

  Public Overridable Sub BeforeColUpdate_alo_lotto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Try
      '----------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128715635970319336, "Attenzione!" & vbCrLf & _
          "Indicare un numero di lotto valido.")))
        e.Row!xx_commeca = ""
      End If
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
  End Sub
  Public Overridable Sub BeforeColUpdate_alo_dtprep(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Try
      '----------------------------------------------------------------------------------
      If NTSCDate(e.ProposedValue) = NTSCDate(IntSetDate("1/1/1000")) Then
        e.ProposedValue = ""
      End If
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
  End Sub
  Public Overridable Sub BeforeColUpdate_alo_dtscad(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False

    Try
      '----------------------------------------------------------------------------------
      If NTSCDate(e.ProposedValue) = NTSCDate(IntSetDate("1/1/1000")) Then
        e.ProposedValue = ""
      End If
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
  End Sub
  Public Overridable Sub BeforeColUpdate_alo_codpqua(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim bOk As Boolean = False
    Dim strDescr As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then e.Row!xx_codpqua = "" : Return
      '----------------------------------------------------------------------------------
      If oCldAnlo.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABPQUA", "N", strDescr) Then
        e.Row!xx_codpqua = strDescr
      Else
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129376516577053096, "Parametro qualità lotto inesistente.")))
        e.ProposedValue = e.Row!alo_codpqua
      End If
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
  End Sub

  Public Overridable Sub OnAddNew(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!alo_codart = strCodart
      e.Row!alo_codpqua = lParQual
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub
#End Region

#Region "FUNZIONI FORM BNMGSELO"
  Public Overridable Function CheckEsistenzaDati(ByVal strCodart As String, ByVal lLotto As Integer) As Boolean
    Try
      'obsoleta
      Return CheckEsistenzaDati(strCodart, lLotto, "")
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function CheckEsistenzaDati(ByVal strCodart As String, ByVal lLotto As Integer, ByVal strLottox As String) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodart, lLotto, strLottox})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      Return oCldAnlo.CheckEsistenzaDati(strDittaCorrente, strCodart, lLotto, strLottox)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Sub DesCodart(ByVal strDittaCorrente As String, _
    ByVal strCodart As String, ByRef strDescr As String, ByRef bEsiste As Boolean)
    Dim dttArtico As DataTable = Nothing

    Try
      If (strCodart.ToString.Trim = "") Or (strCodart.ToString.Trim = ".") Then
        strDescr = ""
      Else
        oCldAnlo.CheckCodart(strDittaCorrente, strCodart, dttArtico)
        If dttArtico.Rows.Count = 0 Then
          bEsiste = False
          strDescr = ""
          dttArtico.Clear()
          Return
        Else
          strDescr = NTSCStr(dttArtico.Rows(0)!ar_descr)
          dttArtico.Clear()
          bEsiste = True
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

  Public Overridable Function LegNuma() As Integer
    Dim lProgr As Integer = 0

    Try
      lProgr = oCldAnlo.LegNuma(strDittaCorrente, "LO", "", 0, True)
      Return lProgr
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function ProgressivoLottoPerArticolo(ByVal strCodart As String) As Integer
    Dim lProgr As Integer = 0

    Try
      lProgr = oCldAnlo.ProgressivoLottoPerArticolo(strDittaCorrente, strCodart)
      Return lProgr
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function


  Public Overridable Function GetNewID(ByRef lLotto As Integer) As Boolean
    'lotto alfanumerico: creo un nuovo ID (ex alo_lotto)
    Try

      Return oCldAnlo.LottoxCreaID(strDittaCorrente, lLotto)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function GetIdFromLottox(ByVal strCodart As String, ByVal strLottox As String, ByRef lLotto As Integer) As Boolean
    'dato il lotto alfanumerico ottengo alo_lotto
    Try

      Return oCldAnlo.LottoxGetID(strDittaCorrente, strCodart, strLottox, lLotto)

    Catch ex As Exception
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

#Region "FUNZIONI FORM BNSQVALO"
  Public Overridable Function ImportaDati(ByVal strValoCodart As String, ByVal lValolotto As Integer, ByVal nValocodpqua As Integer) As Boolean
    Try
      Return oCldAnlo.ImportaDati(strDittaCorrente, strValoCodart, lValolotto, nValocodpqua)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ApriVall(ByVal strValoCodart As String, ByVal lValoLotto As Integer, ByRef ds As DataSet) As Boolean
    Try
      If Not oCldAnlo.ApriVall(strDittaCorrente, strValoCodart, lValoLotto, ds) Then Return False

      For z As Integer = 0 To ds.Tables("VALLOTTI").Rows.Count - 1
        With ds.Tables("VALLOTTI").Rows(z)
          Select Case NTSCInt(!alv_tipcamp)
            Case 7 : !xx_valore = !alv_valdouble
            Case 8 : !xx_valore = NTSCStr(NTSCDate(!alv_valdata)).Split(" "c)(0) ' Prende la parte prima dell'orario
            Case 10 : !xx_valore = !alv_valtext
          End Select
        End With
      Next

      AddHandler ds.Tables("VALLOTTI").ColumnChanging, AddressOf BeforeColUpdateVall
      AddHandler ds.Tables("VALLOTTI").ColumnChanged, AddressOf AfterColUpdateVall

      dsSharedVall = ds

      dsSharedVall.AcceptChanges()

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function CaricaCombo(ByVal nValoCodpqua As Integer, ByVal nCampo As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      Return oCldAnlo.CaricaCombo(strDittaCorrente, nValoCodpqua, nCampo, dttOut)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function SalvaVall() As Boolean
    Try
      For z As Integer = 0 To dsSharedVall.Tables("VALLOTTI").Rows.Count - 1
        With dsSharedVall.Tables("VALLOTTI").Rows(z)
          Select Case NTSCInt(!alv_tipcamp)
            Case 7 : !alv_valdouble = !xx_valore
            Case 8 : !alv_valdata = !xx_valore
            Case 10 : !alv_valtext = !xx_valore
          End Select
        End With
      Next

      If Not oCldAnlo.SalvaVall(dsSharedVall) Then Return False

      dsSharedVall.AcceptChanges()
      bHasChangesVall = False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub AfterColUpdateVall(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChangesVall = True

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdateVall_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub BeforeColUpdateVall(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
      Dim strFunction As String = "BeforeColUpdateVall_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub BeforeColUpdateVall_xx_valore(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      Select Case NTSCInt(e.Row!alv_tipcamp)
        Case 7 ' Al momento non gestito
        Case 8 : e.ProposedValue = NTSCStr(e.ProposedValue).Split(" "c)(0) ' Esclude l'orario e tiene la data
        Case 10
          If NTSCStr(e.ProposedValue) = "" Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129379161332013651, "Specificare un messaggio")))
            e.ProposedValue = e.Row!xx_valore
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
  Public Overridable Sub AfterColUpdateVall_xx_combo(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      Dim strTmp As String = NTSCStr(e.Row!xx_combo)
      If strTmp.Contains(".") Then strTmp = strTmp.Substring(0, strTmp.IndexOf("."))
      e.Row!alv_valcombo = strTmp
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function RipristinaVall(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    Try
      dsSharedVall.Tables("VALLOTTI").Select(strFilter)(nRow).RejectChanges()

      Return True
    Catch ex As Exception
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

  Public Overridable Function DeleteAnalotti() As Boolean
    'cancello le anagrafiche dei lotti non presenti in 
    'lotcdef, movmag, movprb, movord, ordlist, listsar, listprod
    Try
      Return oCldAnlo.DeleteAnalotti(strDittaCorrente, "", 0, 0)

    Catch ex As Exception
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