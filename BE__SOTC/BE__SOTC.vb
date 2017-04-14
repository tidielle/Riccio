Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLE__SOTC
  Inherits CLE__BASE

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



  Public strPDC As String = ""
  Public strStrutturaPDC As String = ""           'viene impostato nella routine 'Apri'
  Public bPDCProfess As Boolean = False           'viene impostato nella routine 'Apri'
  Public bNuovoContoProposto As Boolean = False   'se false il conto non è quello proposto, ma il progressivo è stato modificato manualmente
  Public lNuovoMastro As Integer = 0              'mastro del nuovo conto in fase di inserimento
  Public bAllowOperation As Boolean = False
  Public dttAnaz As New DataTable
  Public nEscomp As Integer                       'esercizione di competenza utilizzato in form ripartizioni di ca

  Public bModCI As Boolean = False
  Public bsModSupCAE As Boolean = False

  Public oCldSotc As CLD__SOTC

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BD__SOTC"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldSotc = CType(MyBase.ocldBase, CLD__SOTC)
    oCldSotc.Init(oApp)
    Return True
  End Function

  Public Overridable Function LeggiDatiDitta(ByVal strDitta As String) As Boolean
    Try
      strDittaCorrente = strDitta

      oCldSotc.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", "", dttAnaz)
      strPDC = dttAnaz.Rows(0)!tb_azcodpcon.ToString
      nEscomp = NTSCInt(dttAnaz.Rows(0)!tb_escomp.ToString)

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


  Public Overrides Function Apri(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Dim dttTmp As New DataTable
    Try
      strDittaCorrente = strDitta

      '--------------------------------------
      'memorizzo se il pdc è di tipo professionista
      bPDCProfess = False
      oCldSotc.ValCodiceDb(strPDC, "", "TABPCON", "S", "", dttTmp)
      bPDCProfess = CBool(IIf(dttTmp.Rows(0)!tb_pcprof.ToString = "S", True, False))
      strStrutturaPDC = NTSCStr(dttTmp.Rows(0)!tb_struttura)
      dttTmp.Clear()

      dReturn = oCldSotc.GetData(strDittaCorrente, ds)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldBase.SetTableDefaultValueFromDB(strNomeTabella, ds)

      SetDefaultValue(ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
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

  Public Overridable Function CaricaColonneUnbound(ByRef dtrIn As DataRow, ByRef bContoMovimentato As Boolean) As Boolean
    Dim dtrS As DataRowState = Nothing
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Try
      dtrS = dtrIn.RowState

      '----------------------
      'descrizione mastro
      If NTSCInt(dtrIn!an_codmast) <> 0 Then
        oCldSotc.ValCodiceDb(NTSCInt(dtrIn!an_codmast).ToString, strDittaCorrente, "TABMAST", "N", strTmp, Nothing, strPDC)
        dtrIn!xx_codmast = strTmp
      Else
        dtrIn!xx_codmast = ""
      End If

      '----------------------
      'descrizione voce irap
      If NTSCInt(dtrIn!an_voceirap) <> 0 Then
        oCldSotc.ValCodiceDb(NTSCInt(dtrIn!an_voceirap).ToString, strDittaCorrente, "TABDIRA", "N", strTmp)
        dtrIn!xx_voceirap = strTmp
      Else
        dtrIn!xx_voceirap = ""
      End If

      '----------------------
      'descrizione valuta
      If NTSCInt(dtrIn!an_valuta) <> 0 Then
        oCldSotc.ValCodiceDb(NTSCInt(dtrIn!an_valuta).ToString, strDittaCorrente, "TABVALU", "N", strTmp)
        dtrIn!xx_valuta = strTmp
      Else
        dtrIn!xx_valuta = ""
      End If

      '----------------------
      'descrizione contropartita ratei/risconti
      If NTSCInt(dtrIn!an_controp) <> 0 Then
        If NTSCInt(dtrIn!an_pcconto) <> 0 Then
          oCldSotc.ValCodiceDb(NTSCInt(dtrIn!an_controp).ToString, strDittaCorrente, "ANAGPC", "N", strTmp, Nothing, strPDC)
        Else
          oCldSotc.ValCodiceDb(NTSCInt(dtrIn!an_controp).ToString, strDittaCorrente, "ANAGRA", "N", strTmp, Nothing, strPDC)
        End If
        dtrIn!xx_controp = strTmp
      Else
        dtrIn!xx_controp = ""
      End If

      '----------------------
      'descrizione contropartita conto funzionamento
      If NTSCInt(dtrIn!an_funzion) <> 0 Then
        If NTSCInt(dtrIn!an_pcconto) <> 0 Then
          oCldSotc.ValCodiceDb(NTSCInt(dtrIn!an_funzion).ToString, strDittaCorrente, "ANAGPC", "N", strTmp, Nothing, IIf(bPDCProfess, "Standard-PR", "Standard-AZ").ToString)
        Else
          oCldSotc.ValCodiceDb(NTSCInt(dtrIn!an_funzion).ToString, strDittaCorrente, "ANAGRA", "N", strTmp, Nothing, IIf(bPDCProfess, "Standard-PR", "Standard-AZ").ToString)
        End If
        dtrIn!xx_funzion = strTmp
      Else
        dtrIn!xx_funzion = ""
      End If


      '----------------------
      'descrizione conto anagpc
      If NTSCInt(dtrIn!an_pcconto) <> 0 Then
        oCldSotc.ValCodiceDb(NTSCInt(dtrIn!an_pcconto).ToString, strDittaCorrente, "ANAGPC", "N", strTmp, Nothing, strPDC)
        dtrIn!xx_pcconto = strTmp
      Else
        dtrIn!xx_pcconto = ""
      End If

      '----------------------
      'descrizione voce finanziaria
      If NTSCStr(dtrIn!an_codvfde).Trim <> "" Then
        oCldSotc.ValCodiceDb(NTSCStr(dtrIn!an_codvfde).ToString, strDittaCorrente, "TABVFDE", "S", strTmp, Nothing, strPDC)
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
      'determino se posso o meno salvare
      bAllowOperation = True
      Select Case strPDC.ToLower
        Case "standard-az", "standard-pr", "standard-cna", "standard-asm", "standard-psm", "standard-xxx"
          If oApp.User.Nome.ToUpper <> "NTS" Or oApp.User.Pwd.ToUpper <> "NTS" Then
            If NTSCInt(dtrIn!an_pcconto) = 0 Then
              'sottoconto ditta: posso modificare tutto (come faceva in vb6)
            Else
              If NTSCInt(Microsoft.VisualBasic.Right(dtrIn!an_conto.ToString, 4)) <= 5000 Then bAllowOperation = False
            End If
          End If
      End Select

      '--------------------------------------
      'se il conto è stato movimentato non posso modificare i flag di gestione partite/scadenze
      bContoMovimentato =  oCldSotc.ContoMovimentato(strDittaCorrente, NTSCInt(dtrIn!an_conto))

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      '-------------------------------------------------
      'gestione di actlog
      'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
      strActLog = oCldBase.GetSettingBus("BS--SOTC", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
      If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"
      strActLogProg = "BS--SOTC"
      strActLogNomOggLog = "ANAGRA"
      strActLogDesLog = oApp.Tr(Me, 128308083951468000, "Anagrafica piano dei conti")

      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("ANAGRA").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("ANAGRA").Columns("an_codpcon").DefaultValue = strPDC
      ds.Tables("ANAGRA").Columns("an_descr1").DefaultValue = "."
      ds.Tables("ANAGRA").Columns("an_dtaper").DefaultValue = DateTime.Now
      ds.Tables("ANAGRA").Columns("an_tipo").DefaultValue = "S"

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Overrides Sub Nuovo()
    Dim dttTmp As New DataTable
    Try
      '----------------------------------------
      'inserisco una nuova riga
      MyBase.Nuovo()

      With dsShared.Tables(strNomeTabella).Rows(dsShared.Tables(strNomeTabella).Rows.Count - 1)
        !an_dtaper = NTSCDate(DateTime.Now.ToShortDateString)
        !an_opnome = oApp.User.Nome

        !an_codmast = lNuovoMastro

        '--------------------------------------
        'propongo i conti CEE e riclassificati da tabmast
        oCldSotc.ValCodiceDb(lNuovoMastro.ToString, "", "TABMAST", "N", "", dttTmp, strPDC)
        !an_kpccee = NTSCStr(dttTmp.Rows(0)!tb_rifceed)
        !an_kpccee2 = NTSCStr(dttTmp.Rows(0)!tb_rifceea)
        !an_rifricd = NTSCStr(dttTmp.Rows(0)!tb_rifricd)
        !an_rifrica = NTSCStr(dttTmp.Rows(0)!tb_rifrica)
        dttTmp.Clear()

      End With

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

  Public Overridable Sub BeforeColUpdate_an_controp(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bOk As Boolean = False
    Dim strTmp As String = ""
    Try

      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_controp = ""
      Else
        If NTSCInt(e.Row!an_pcconto) <> 0 Then
          bOk = oCldSotc.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "ANAGPC", "N", strTmp, Nothing, strPDC)
        Else
          bOk = oCldSotc.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "ANAGRAS", "N", strTmp, Nothing, strPDC)
        End If
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128308826297430000, _
                            "Codice contropartita ratei/risconti non corretto" & vbCrLf & _
                            "(se il conto è valido per piu ditte, anche la contropartita deve esserlo)")))
          Return
        Else
          e.Row!xx_controp = strTmp
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

  Public Overridable Sub BeforeColUpdate_an_funzion(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bOk As Boolean = False
    Dim strTmp As String = ""
    Try

      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_funzion = ""
      Else
        If NTSCInt(e.ProposedValue.ToString.Substring(e.ProposedValue.ToString.Length - 4)) > 5000 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128326981529316000, "Attenzione: il conto selezionato non rientra tra quelli standard (ovvero con progressivo sotto il 5000). ")))
          Return
        End If
        bOk = oCldSotc.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "ANAGPC", "N", strTmp, Nothing, IIf(bPDCProfess, "Standard-PR", "Standard-AZ").ToString)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128308827486090000, "Codice conto funzionamento non corretto" & vbCrLf & _
                                            "(deve essere un conto valido per piu ditte appartenente al PDC 'Standard-PR' o 'Standard-AZ')")))
          Return
        Else
          e.Row!xx_funzion = strTmp
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

  Public Overridable Sub BeforeColUpdate_an_voceirap(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim bOk As Boolean = False
    Dim strTmp As String = ""
    Try

      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_voceirap = ""
      Else
        bOk = oCldSotc.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "TABDIRA", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128771713983620000, "Codice IRAP non corretto")))
          Return
        Else
          e.Row!xx_voceirap = strTmp
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
    Dim bOk As Boolean = False
    Dim strTmp As String = ""
    Try

      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_valuta = ""
      Else
        bOk = oCldSotc.ValCodiceDb(NTSCInt(e.ProposedValue).ToString, strDittaCorrente, "TABVALU", "N", strTmp)
        If bOk = False Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128308825257490000, "Codice Valuta non corretto")))
          Return
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

  Public Overridable Sub BeforeColUpdate_an_partite(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If e.ProposedValue.ToString = "N" And e.Row!an_scaden.ToString = "S" Then
        e.Row!an_scaden = "N"
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128771714030888000, "Se 'Gestione scadenze' è selezionato deve esserlo anche 'Gestione partite'")))
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

  Public Overridable Sub BeforeColUpdate_an_scaden(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If e.ProposedValue.ToString = "S" And e.Row!an_partite.ToString = "N" Then
        e.Row!an_partite = "S"
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128326969320086000, "Se 'Gestione scadenze' è selezionato deve esserlo anche 'Gestione partite'")))
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

      If oCldSotc.ValCodiceDb(NTSCStr(e.ProposedValue), strDittaCorrente, "TABVFDE", "S", , dttTmp) Then
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



  Public Overridable Function TestPreSalvaAnagra(ByRef dtrIn As DataRow) As Boolean
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Try

      If Not bAllowOperation Then
        If dtrIn.RowState = DataRowState.Added Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128771714124020000, "Attenzione! Non è possibile inserire sottoconti con codice inferiore a 5001.")))
          Return False
        Else
          'posso modificare solo il codice valuta ed i campi di tesoreria: è un conto legato ad un pdc standard
          'la riga sotto non serve: l'eventuale modifica fatta alla descrizione viene già annullata uscendo da quel campo
          'dtrIn!an_descr1 = dtrIn("an_descr1", DataRowVersion.Original)

          'LE RIGHE SOTTO NON SERVONO: HO GIA' BLOCCATO I CONTROLLI IN FORM!!!
          'For i = 0 To dsShared.Tables("ANAGRA").Columns.Count - 1
          '  If dsShared.Tables("ANAGRA").Columns(i).ColumnName.ToUpper.PadRight(3).Substring(0, 3) <> "XX_" And _
          '     dsShared.Tables("ANAGRA").Columns(i).ColumnName <> "an_valuta" And _
          '     dsShared.Tables("ANAGRA").Columns(i).ColumnName <> "an_trating" And _
          '     dsShared.Tables("ANAGRA").Columns(i).ColumnName <> "an_codvfde" And _
          '     dsShared.Tables("ANAGRA").Columns(i).ColumnName <> "ts" Then
          '    If dtrIn(dsShared.Tables("ANAGRA").Columns(i).ColumnName).ToString <> dtrIn(dsShared.Tables("ANAGRA").Columns(i).ColumnName, DataRowVersion.Original).ToString Then
          '      ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129736881652908627, "Attenzione! Su sottoconti con codice inferiore a 5001 è possibile modificare solo 'Codice valuta', 'Rating' e 'Voce finanziaria' (voce variata '|" & dsShared.Tables("ANAGRA").Columns(i).ColumnName & "|')")))
          '      Return False
          '    End If
          '  End If
          'Next
        End If
      End If

      '-----------------------------------
      'verifico se il conto è stato modificato da altri
      oCldSotc.ValCodiceDb(NTSCInt(dtrIn!an_conto).ToString, strDittaCorrente, "ANAGRA", "N", "", dttTmp, strPDC)
      If dttTmp.Rows.Count = 0 Then
        If dtrIn.RowState <> DataRowState.Added Then
          dttTmp.Clear()
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128310411141446000, "Il conto è stato cancellato da un altro utente")))
          Return False
        End If
      Else
        If dtrIn.RowState = DataRowState.Added Then
          dttTmp.Clear()
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128771714233376000, "Il conto è appena stato inserito da un altro utente")))
          Return False
        Else
          If NTSCDate(dttTmp.Rows(0)!an_ultagg.ToString) <> NTSCDate(dtrIn!an_ultagg.ToString) Or _
             NTSCDate(dttTmp.Rows(0)!an_opnome.ToString) <> NTSCDate(dtrIn!an_opnome.ToString) Then
            dttTmp.Clear()
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128771714269100000, "Il conto è appena stato modificato da un altro utente")))
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
      Return False
    End Try
  End Function

  Public Overridable Function TestPreCancellaAnagra(ByRef dtrIn As DataRow) As Boolean
    Dim dttTmp As New DataTable
    Dim strErr As String = ""
    Try

      If Not bAllowOperation Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128310404820356000, "Attenzione! Non è possibile cancellare sottoconti con codice inferiore a 5001.")))
        Return False
      End If

      '---------------------------------------------
      If NTSCInt(dtrIn!an_pcconto) = 0 Then
        'sottoconto ditta
        'verifico se il conto è stato movimentato in scaden, prinot, testmag, testord, ...
        oCldSotc.IsAnagDeletable(strDittaCorrente, NTSCInt(dtrIn!an_conto), strErr)
        If strErr <> "" Then
          ThrowRemoteEvent(New NTSEventArgs("", strErr))
          Return False
        End If
      Else
        'è un sottoconto PDC e faccio continuare: dopo chiederà se cancellare il sottoconto PDC e mantere i sottoconti ditta movimentati
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

  Public Overridable Function SalvaAnagra(ByVal bDelete As Boolean, ByRef dtrIn As DataRow) As Boolean
    Dim bResult As Boolean = False
    Dim strSqlWhere As String = ""
    Dim strDesogglog As String = ""
    Dim evnt As NTSEventArgs
    Dim strErr As String = ""

    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete), poi salva

      If bDelete Then

        'se è un sottoconto PDC, prima chiedo se cancellare solo il sottoconto PDC e mantenere gli anagra
        If NTSCInt(dtrIn!an_pcconto) <> 0 Then
          evnt = New NTSEventArgs("MSG_NOYES", oApp.Tr(Me, 128310434748290000, "ATTENZIONE!" & vbCrLf & _
                    "Il conto è utilizzabile da più ditte." & vbCrLf & _
                    "Si desidera procedere comunque eliminando tutti i sottoconti cancellabili e sganciando " & _
                    "quelli eventualmente movimentati nelle varie ditte?"))
          ThrowRemoteEvent(evnt)
          If evnt.RetValue = "NO" Then Return False

          strSqlWhere = CStrSQL(strPDC) & "," & dtrIn!an_pcconto.ToString
          strDesogglog = "Cancellazione dati relativi all'anagrafica Piano dei Conti '" & strPDC & "'"
          oCldSotc.ScriviActLog(strDittaCorrente, "BS--SOTC", "anagpc", "anagpc", strSqlWhere, "A", "D", strDesogglog, False)
          If Not oCldSotc.DeleteDataPDC(dtrIn, 0) Then Return False
          bHasChanges = False
          Return True
        End If    'If NTSCInt(dtrIn!an_pcconto) <> 0 Then

        'CANCELLO (sottoconto non PDC)
        strSqlWhere = CStrSQL(strDittaCorrente) & "," & dtrIn!an_conto.ToString
        strDesogglog = "Cancellazione dati relativi all'anagrafica sottoconto '" & dtrIn!an_conto.ToString & "'"

        'scrivo il log
        oCldSotc.ScriviActLog(strDittaCorrente, "BS--SOTC", "anagra", "anagra", strSqlWhere, "A", "D", strDesogglog, False)

        'test pre-cancellazione già fatti in precedenza: ora devo solo cancellare
        If Not oCldSotc.DeleteData(strDittaCorrente, dtrIn) Then Return False
      Else
        'INSERISCO/AGGIORNO
        If Not TestPreSalvaAnagra(dtrIn) Then Return False

        If NTSCInt(dtrIn!an_pcconto) <> 0 Then
          strSqlWhere = CStrSQL(strPDC) & "," & dtrIn!an_pcconto.ToString
          strDesogglog = "Modifica dati relativi all'anagrafica Piano dei Conti '" & strPDC & "'"

          If dtrIn.RowState <> DataRowState.Added Then
            oCldSotc.ScriviActLog(strDittaCorrente, "BS--SOTC", "anagpc", "anagpc", strSqlWhere, "M", "D", strDesogglog, False)
          End If

          'salvo
          If Not oCldSotc.SaveDataPDC(dtrIn, 0, False, bNuovoContoProposto, strErr) Then
            If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
            Return False
          Else
            If strErr <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strErr))
          End If
        End If 'If NTSCInt(dtrIn!an_pcconto) <> 0 Then

        'sottoconto ditta (devo aggiornarlo anche se ho già aggiornato il pdc, 
        'diversamente perderei le modifiche su valuta e campi tesoreria (presenti su anagra ma non su anagpc)

        strSqlWhere = CStrSQL(strDittaCorrente) & "," & dtrIn!an_conto.ToString
        strDesogglog = "Modifica dati relativi all'anagrafica sottoconto '" & dtrIn!an_conto.ToString & "'"

        'scrivo il log
        If dtrIn.RowState <> DataRowState.Added Then
          oCldSotc.ScriviActLog(strDittaCorrente, "BS--SOTC", "anagra", "anagra", strSqlWhere, "M", "D", strDesogglog, False)
        End If

        'salvo
        If Not oCldSotc.SaveData(dtrIn, bNuovoContoProposto, NTSCStr(dttAnaz.Rows(0)!tb_flriccf)) Then Return False

      End If    'If bDelete Then

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

  Public Overridable Function GetAnagCA(ByRef ds As DataSet) As Boolean
    Try
      Return oCldSotc.GetAnagCA(strDittaCorrente, ds)

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

#Region "BN__NUOA"

  Public Overridable Function FRM__NUOA_edMastro_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef lConto As Integer) As Boolean
    Dim bOut As Boolean = False
    Dim dttTmp As New DataTable
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldSotc.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABMAST", "N", strDescr, dttTmp, strPDC)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127965883462031250, "Codice mastro |'" & nCod.ToString & "'| inesistente sul PDC selezionato")))
        Return False
      Else
        If NTSCStr(dttTmp.Rows(0)!tb_tipomast) <> "S" Then
          dttTmp.Clear()
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128310307173392000, "Codice mastro |'" & nCod.ToString & "'| non di tipo 'Sottoconti'")))
          Return False
        End If
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

  Public Overridable Function FRM__NUOA_CheckConto(ByVal nMastro As Integer, ByVal lProgr As Integer, _
                                                 ByRef lContoOut As Integer) As Boolean
    Try
      'obsoleta
      Return FRM__NUOA_CheckConto(nMastro, lProgr, lContoOut, False)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function FRM__NUOA_CheckConto(ByVal nMastro As Integer, ByVal lProgr As Integer, _
                                                   ByRef lContoOut As Integer, ByVal bPdc As Boolean) As Boolean
    Dim dMoltiplicatore As Integer = 0
    Dim strTmp As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nMastro, lProgr, lContoOut, bPdc})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        lContoOut = NTSCInt(oIn(2))
        Return CBool(oOut)
      End If
      '----------------

      '-----------------------------
      'controlli per verificare se il nuovo conto è valido
      Select Case strStrutturaPDC
        Case "A" : dMoltiplicatore = 100000
        Case "B" : dMoltiplicatore = 1000000
        Case "C" : dMoltiplicatore = 1000000
        Case "D" : dMoltiplicatore = 100000
        Case "S" : dMoltiplicatore = 10000
      End Select

      lContoOut = (nMastro * dMoltiplicatore) + lProgr

      '------------------------------
      'Se il piano dei conti è 'Standard-AZ' o 'Standard-PR', 'Standard-CNA', 'Standard-ASM', 'Standard-PSM', 'Standard-XXX'
      'il nome operatore o la password sono diversi da 'NTS'
      'e le ultime 4 cifre del conto sono inferiori o uguali a 5000
      'NON PERMETTE L'INSERIMENTO DEL CONTO
      Select Case LCase(strPDC)
        Case "standard-az", "standard-pr", "standard-cna", "standard-asm", "standard-psm", "standard-xxx"
          If oApp.User.Nome.ToUpper <> "NTS" Or oApp.User.Pwd.ToUpper <> "NTS" Then
            If NTSCInt(Microsoft.VisualBasic.Right(lProgr.ToString, 4)) <= 5000 Then
              lContoOut = 0
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128310327621662000, _
                              "Attenzione! Inserimento conto non possibile." & vbCrLf & _
                              "E' consentita la creazione solo di quei sottoconti che sono superiori a 5000.")))
              Return False
            End If
          End If
      End Select

      '-----------------------------
      'Controlla che non si sia un conto duplicato
      If oCldSotc.ValCodiceDb(lContoOut.ToString, strDittaCorrente, "ANAGRA", "N", strTmp, Nothing, strPDC) Then
        lContoOut = 0
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128310330278966000, "Conto già esistente (|" & strTmp & "|)")))
        Return False
      End If

      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla che il nuovo numero conto indicato non esista già in ANAGRA
      '--- di tipo sottoconto ('S'), per il Piano dei Conti corrente.
      '--------------------------------------------------------------------------------------------------------------
      If bPdc Then
        If Not oCldSotc.ValidaNuovoContoAnagra(strPDC, lContoOut) Then
          lContoOut = 0
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128310325889438000, "Codice conto non valido." & vbCrLf & _
            "Il codice indicato esiste già presente nei sottoconti ditta" & vbCrLf & _
            "relativi al Piano dei conti corrente |'" & strPDC & "'|.")))
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

  Public Overridable Function RitornaProgressivo(ByVal nCodmast As Integer, ByVal bGenerico As Boolean) As Integer
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldSotc.RitornaProgressivo(strDittaCorrente, nCodmast, bGenerico, strPDC)
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

#Region "FRMCIALIN"
  Public bAlinHasChanges As Boolean
  Public dsAlinShared As DataSet
  Public lContoAlin As Integer = 0

  Public Overridable Function AlinApri(ByVal strDitta As String, ByRef dsAlin As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldSotc.GetAnalink(strDittaCorrente, lContoAlin, dsAlin)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldSotc.SetTableDefaultValueFromDB("ANALINK", dsAlin)

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      AlinSetDefaultValue(dsAlin)
      dsAlinShared = dsAlin

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsAlinShared.Tables("ANALINK").ColumnChanging, AddressOf AlinBeforeColUpdate
      AddHandler dsAlinShared.Tables("ANALINK").ColumnChanged, AddressOf AlinAfterColUpdate

      '--------------------------------------
      'confermo tutto
      dsAlinShared.Tables("ANALINK").AcceptChanges()
      bAlinHasChanges = False

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

  Public Overridable Sub AlinSetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("ANALINK").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("ANALINK").Columns("anl_anconto").DefaultValue = lContoAlin
      ds.Tables("ANALINK").Columns("anl_acconto").DefaultValue = 0
      ds.Tables("ANALINK").Columns("xxx_acconto").DefaultValue = ""

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub AlinNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsAlinShared.Tables("ANALINK").Rows.Add(dsAlinShared.Tables("ANALINK").NewRow)
      bAlinHasChanges = True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Overridable Function AlinRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsAlinShared.Tables("ANALINK").Select(strFilter)(nRow).RejectChanges()
      bAlinHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function AlinSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not AlinTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldSotc.ScriviTabellaSemplice(strDittaCorrente, "ANALINK", dsAlinShared.Tables("ANALINK"), "", "", "")
      If bResult Then
        bAlinHasChanges = False
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

  Public ReadOnly Property AlinRecordIsChanged() As Boolean
    Get
      Return bAlinHasChanges
    End Get
  End Property


  Public Overridable Sub AlinBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper + ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AlinBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub AlinBeforeColUpdate_anl_acconto(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""

    Try
      If dsAlinShared.Tables("ANALINK").Rows.Count > 1 Then
        dtrTmp = dsAlinShared.Tables("ANALINK").Select("anl_acconto = " & e.ProposedValue.ToString())
        If dtrTmp.Length > 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128327821547070000, "Codice conto CA già esistente: inserire un nuovo codice")))
          Return
        End If
      End If

      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xxx_acconto = ""
      Else
        If Not oCldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGCA", "N", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128327821533080000, "Codice conto CA non corretto")))
        Else
          e.Row!xxx_acconto = strTmp
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


  Public Overridable Sub AlinAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bAlinHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AlinAfterColUpdate_" & e.Column.ColumnName.ToLower
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


  Public Overridable Function AlinTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Try
      dtrTmp = dsAlinShared.Tables("ANALINK").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrTmp.Length - 1
        If NTSCInt(dtrTmp(i)!anl_acconto) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222114062500, "Il campo codice conto CA deve contenere un valore diverso da 0")))
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

#End Region

#Region "FRMCIANAC"
  Public bAnacHasChanges As Boolean
  Public dsAnacShared As DataSet
  Public lContoAnac As Integer = 0

  Public Overridable Function AnacApri(ByVal strDitta As String, ByRef dsAnac As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta
      dReturn = oCldSotc.GetAnacent(strDittaCorrente, lContoAnac, dsAnac)
      If dReturn = False Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      oCldSotc.SetTableDefaultValueFromDB("ANACENT", dsAnac)

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      AnacSetDefaultValue(dsAnac)
      dsAnacShared = dsAnac

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsAnacShared.Tables("ANACENT").ColumnChanging, AddressOf AnacBeforeColUpdate
      AddHandler dsAnacShared.Tables("ANACENT").ColumnChanged, AddressOf AnacAfterColUpdate

      '--------------------------------------
      'confermo tutto
      dsAnacShared.Tables("ANACENT").AcceptChanges()
      bAnacHasChanges = False

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

  Public Overridable Sub AnacSetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("ANACENT").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("ANACENT").Columns("anc_conto").DefaultValue = lContoAnac

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub AnacNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga (da non usarsi con tabella collegata alla griglia)
      dsAnacShared.Tables("ANACENT").Rows.Add(dsAnacShared.Tables("ANACENT").NewRow)
      bAnacHasChanges = True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Overridable Function AnacRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsAnacShared.Tables("ANACENT").Select(strFilter)(nRow).RejectChanges()
      bAnacHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function AnacSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not AnacTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldSotc.ScriviTabellaSemplice(strDittaCorrente, "ANACENT", dsAnacShared.Tables("ANACENT"), "", "", "")
      If bResult Then
        bAnacHasChanges = False
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

  Public ReadOnly Property AnacRecordIsChanged() As Boolean
    Get
      Return bAnacHasChanges
    End Get
  End Property


  Public Overridable Sub AnacBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper + ";"
        Return
      End If
      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AnacBeforeColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub AnacBeforeColUpdate_anc_contoca(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_contoca = ""
      Else
        If Not ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "ANAGCA", "N", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129737005936205708, "Codice conto CA non corretto")))
        Else
          e.Row!xx_contoca = strTmp
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

  Public Overridable Sub AnacBeforeColUpdate_anc_codcena(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codcena = ""
      Else
        If Not ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCENA", "N", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128583638771843706, "Codice conto CA non corretto")))
        Else
          e.Row!xx_codcena = strTmp
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

  Public Overridable Sub AnacBeforeColUpdate_anc_codcfam(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      If Trim(NTSCStr(e.ProposedValue)) = "" Then
        e.Row!xx_codcfam = ""
      Else
        If Not ocldBase.ValCodiceDb(e.ProposedValue.ToString, strDittaCorrente, "TABCFAM", "S", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128583644269291602, "Codice famiglia non corretto")))
        Else
          e.Row!xx_codcfam = strTmp
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


  Public Overridable Sub AnacAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bAnacHasChanges = True

      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AnacAfterColUpdate_" & e.Column.ColumnName.ToLower
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


  Public Overridable Function AnacTestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim dtrCurrRow() As DataRow
    Dim dttTmp As New DataTable
    Dim strFlci As String = ""
    Dim bTestBudget As Boolean = True
    Try
      dtrCurrRow = dsAnacShared.Tables("ANACENT").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrCurrRow.Length - 1
        If NTSCInt(dtrCurrRow(i)!anc_contoca) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128771714405358000, "Il campo codice conto CA deve contenere un valore diverso da 0")))
          Return False
        End If
        If NTSCInt(dtrCurrRow(i)!anc_codcena) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128583627303778737, "Il campo codice centro deve contenere un valore diverso da 0")))
          Return False
        End If

        bTestBudget = CBool(oCldSotc.GetSettingBus("OPZIONI", ".", ".", "TestBudget", "-1", " ", "-1"))
        If Not oCldSotc.EsisteBudget(strDittaCorrente, "E", NTSCInt(dtrCurrRow(i)!anc_contoca), NTSCInt(dtrCurrRow(i)!anc_codcena), " ", 0, " ", nEscomp, bTestBudget) Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128583640806518031, "Manca budget per questo centro.")))
          Return False
        End If

        If ocldBase.ValCodiceDb(NTSCStr(dtrCurrRow(i)!anc_contoca), strDittaCorrente, "ANAGCA", "N", , dttTmp) Then
          strFlci = NTSCStr(dttTmp.Rows(0)!ac_flci)

          If (strFlci = "3") Or (strFlci = "4") Then
            If Trim(NTSCStr(dtrCurrRow(i)!anc_codcfam)) = "" Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128583651211709748, "Indicare la linea.")))
              Return False
            End If
          Else
            If Trim(NTSCStr(dtrCurrRow(i)!anc_codcfam)) <> "" Then
              Select Case strFlci
                Case "1" : ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128583651450090786, "Conto gestito per Centro, inserimento Linea non permesso.")))
                Case "2" : ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128583651564126276, "Conto gestito per Centro e Commessa, inserimento Linea non permesso.")))
              End Select
              Return False
            End If
          End If
        End If
      Next

      '-------------------------------------------------
      'devo verificase se  presente un'altra riga con la stessa chiave ...
      'tenendo in considerazione chce la riga che devo salvare  sempre una, potrei trovarmi nella situazione di 
      '- nuovo record
      '- record modificato
      'in entrambi i casi le righe trovate sono sempre 2, quella precedente non modificata e quella nuova o in modifica
      dtrTmp = dsAnacShared.Tables("ANACENT").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
      " AND anc_conto = " & dtrCurrRow(0)!anc_conto.ToString & " AND anc_contoca = " & dtrCurrRow(0)!anc_contoca.ToString & _
      " AND anc_codcena = " & dtrCurrRow(0)!anc_codcena.ToString & " AND anc_codcfam = " & CStrSQL(dtrCurrRow(0)!anc_codcfam.ToString))
      If dtrTmp.Length > 1 Then
        ' una nuova riga uguale ad un'altra precedentemente inserita
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129737005765089404, "Esiste gia una riga con le stesse caratteristiche")))
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

#End Region

#Region "BNCIANA2"
  Public bAna2HasChanges As Boolean
  Public dsAna2Shared As DataSet
  Public strCodpcon As String = ""
  Public lConto As Integer = 0

  Public Overridable Sub Anacent2AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '--------------------------------------------------------------------------------------------------------------
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper & ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      bAna2HasChanges = True
      '--------------------------------------------------------------------------------------------------------------
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))
      e.Row.EndEdit()
      e.Row.EndEdit()
      '--------------------------------------------------------------------------------------------------------------
      Dim strFunction As String = "Anacent2AfterColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub Anacent2BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper + ";"
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '--------------------------------------------------------------------------------------------------------------
      Dim strFunction As String = "Anacent2BeforeColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public Overridable Sub Anacent2BeforeColUpdate_codditt(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(e.ProposedValue).Length = 0 Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129187553011584485, "Attenzione!" & vbCrLf & _
          "Indicare un codice ditta valido.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not ocldBase.ValCodiceDb(e.ProposedValue.ToString, e.ProposedValue.ToString, "TABANAZ", "S", "", dttTmp) Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129187554646271985, "Codice ditta non corretto")))
      Else
        e.Row!anc2_codpcca = NTSCStr(dttTmp.Rows(0)!tb_azcodpcca)
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
      dttTmp.Dispose()
    End Try
  End Sub
  Public Overridable Sub Anacent2BeforeColUpdate_anc2_contoca(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.Row!codditt.ToString.Trim = "" Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129187488169488710, "Attenzione!" & vbCrLf & _
          "Indicare il codice ditta prima inserire il conto di CA.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_contoca = ""
      Else
        If Not oCldSotc.GetAnagca2(e.Row!anc2_codpcca.ToString, NTSCInt(e.ProposedValue), dttTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128771714331258000, "Codice conto CA non corretto")))
        Else
          e.Row!xx_contoca = NTSCStr(dttTmp.Rows(0)!ac_descr1)
        End If
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
      dttTmp.Dispose()
    End Try
  End Sub
  Public Overridable Sub Anacent2BeforeColUpdate_anc2_codcena(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.Row!codditt.ToString.Trim = "" Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129187488915056225, "Attenzione!" & vbCrLf & _
          "Indicare il codice ditta prima inserire il centro.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codcena = ""
      Else
        If Not ocldBase.ValCodiceDb(e.ProposedValue.ToString, e.Row!codditt.ToString, "TABCENA", "N", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129187491035125610, "Codice centro non corretto")))
        Else
          e.Row!xx_codcena = strTmp
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
  Public Overridable Sub Anacent2BeforeColUpdate_anc2_codcfam(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(e.ProposedValue) = "" Then
        e.ProposedValue = " "
        e.Row!xx_codcfam = ""
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(e.ProposedValue).Trim = "" Then
        e.Row!xx_codcfam = ""
      Else
        If Not ocldBase.ValCodiceDb(e.ProposedValue.ToString, e.Row!codditt.ToString, "TABCFAM", "S", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129187491992417855, "Codice famiglia non corretto")))
        Else
          e.Row!xx_codcfam = strTmp
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
  Public Overridable Sub Anacent2BeforeColUpdate_anc2_coddivi(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.Row!codditt.ToString.Trim = "" Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129187492286666600, "Attenzione!" & vbCrLf & _
          "Indicare il codice ditta prima inserire la divisione.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_coddivi = ""
      Else
        If Not ocldBase.ValCodiceDb(e.ProposedValue.ToString, e.Row!codditt.ToString, "TABDIVI", "N", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129187492541689340, "Codice divisione non corretto")))
        Else
          e.Row!xx_coddivi = strTmp
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
  Public Overridable Sub Anacent2BeforeColUpdate_anc2_codstab(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If e.Row!codditt.ToString.Trim = "" Then
        e.ProposedValue = e.Row(e.Column.ColumnName)
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129187492821915545, "Attenzione!" & vbCrLf & _
          "Indicare il codice ditta prima inserire lo stabilimento/filiale/negozio.")))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(e.ProposedValue) = 0 Then
        e.Row!xx_codstab = ""
      Else
        If Not ocldBase.ValCodiceDb(e.ProposedValue.ToString, e.Row!codditt.ToString, "TABSTAB", "N", strTmp) Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129187493143672295, "Codice stabilimento/filiale/negozio non corretto")))
        Else
          e.Row!xx_codstab = strTmp
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

  Public Overridable Sub Anacent2Nuovo()
    Try
      '--------------------------------------------------------------------------------------------------------------
      dsAna2Shared.Tables("ANACENT2").Rows.Add(dsAna2Shared.Tables("ANACENT2").NewRow)
      bAna2HasChanges = True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

  Public ReadOnly Property Anacent2RecordIsChanged() As Boolean
    Get
      Return bAna2HasChanges
    End Get
  End Property

  Public Overridable Function Anacent2Ripristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      dsAna2Shared.Tables("ANACENT2").Select(strFilter)(nRow).RejectChanges()
      bAna2HasChanges = False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Function Anacent2Salva(ByVal strDitta As String, ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      If bDelete = False Then
        If Not Anacent2TestPreSalva() Then Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      bResult = oCldSotc.ScriviTabellaSemplice(strDitta, "ANACENT2", dsAna2Shared.Tables("ANACENT2"), "", "", "")
      If bResult = True Then
        bAna2HasChanges = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return bResult
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Sub Anacent2SetDefaultValue(ByRef ds As DataSet)
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      ds.Tables("ANACENT2").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("ANACENT2").Columns("anc2_codpcon").DefaultValue = strCodpcon
      ds.Tables("ANACENT2").Columns("anc2_conto").DefaultValue = lConto
      oCldSotc.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then
        ds.Tables("ANACENT2").Columns("anc2_codpcca").DefaultValue = NTSCStr(dttTmp.Rows(0)!tb_azcodpcca)
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
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Function Anacent2TestPreSalva() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer = 0
    Dim dtrCurrRow() As DataRow
    Dim dttTmp As New DataTable
    Dim oDttgr As New CLEGROUPBY

    Try
      '--------------------------------------------------------------------------------------------------------------
      dtrCurrRow = dsAna2Shared.Tables("ANACENT2").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To (dtrCurrRow.Length - 1)
        If NTSCInt(dtrCurrRow(i)!anc2_contoca) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129737005834289527, "Il campo codice conto CA deve contenere un valore diverso da 0")))
          Return False
        End If
        If oCldSotc.GetAnagca2(NTSCStr(dtrCurrRow(i)!anc2_codpcca), NTSCInt(dtrCurrRow(i)!anc2_conto), dttTmp) Then
          If (NTSCStr(dttTmp.Rows(0)!ac_richcena) = "S") And (NTSCInt(dtrCurrRow(i)!anc2_codcena) = 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129187505793975905, "Il campo codice centro deve contenere un valore diverso da 0")))
            Return False
          End If
          If (NTSCStr(dttTmp.Rows(0)!ac_richcfam) = "S") And (NTSCStr(dtrCurrRow(i)!anc2_codcfam).Trim = "") Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129187506652387760, "Il campo codice linea/prodorro deve contenere un valore valido")))
            Return False
          End If
          If (NTSCStr(dttTmp.Rows(0)!ac_richdivi) = "S") And (NTSCInt(dtrCurrRow(i)!anc2_coddivi) = 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129187507039013405, "Il campo codice divisione deve contenere un valore diverso da 0")))
            Return False
          End If
          If (NTSCStr(dttTmp.Rows(0)!ac_richstab) = "S") And (NTSCInt(dtrCurrRow(i)!anc2_codstab) = 0) Then
            ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129187507250494010, "Il campo codice stabilimento deve contenere un valore diverso da 0")))
            Return False
          End If
        End If
      Next

      oDttgr.NTSGroupBy(dsAna2Shared.Tables("ANACENT2"), dttTmp, "codditt, Sum(anc2_perc) AS SOMMA", "", "codditt")
      For i = 0 To dttTmp.Rows.Count - 1
        If NTSCDec(dttTmp.Rows(i)!SOMMA) > 100 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129439661506624430, "Per la ditta '|" & NTSCStr(dttTmp.Rows(i)!codditt) & "|' la somma delle percentuali è superiore a 100 (|" & NTSCDec(dttTmp.Rows(i)!SOMMA) & "|)")))
          Return False
        End If
      Next

      '--------------------------------------------------------------------------------------------------------------
      dtrTmp = dsAna2Shared.Tables("ANACENT2").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
        " AND anc2_codpcon = " & CStrSQL(dtrCurrRow(0)!anc2_codpcon.ToString) & _
        " AND anc2_conto = " & dtrCurrRow(0)!anc2_conto.ToString & _
        " AND anc2_codpcca = " & CStrSQL(dtrCurrRow(0)!anc2_codpcca.ToString) & _
        " AND anc2_contoca = " & dtrCurrRow(0)!anc2_contoca.ToString & _
        " AND anc2_codcena = " & dtrCurrRow(0)!anc2_codcena.ToString & _
        " AND anc2_codcfam = " & CStrSQL(dtrCurrRow(0)!anc2_codcfam.ToString) & _
        " AND anc2_coddivi = " & dtrCurrRow(0)!anc2_coddivi.ToString & _
        " AND anc2_codstab = " & dtrCurrRow(0)!anc2_codstab.ToString)
      If dtrTmp.Length > 1 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222107500000, "Esiste gia una riga con le stesse caratteristiche")))
        Return False
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
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function DuplicaDitta(ByVal dtrIn As DataRow) As Boolean
    Try
      Return oCldSotc.DuplicaDitta(dtrIn)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function GetAnacent2(ByRef dsAna2 As DataSet) As Boolean
    Dim dReturn As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      dReturn = oCldSotc.GetAnacent2(strCodpcon, lConto, dsAna2)
      If dReturn = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      oCldSotc.SetTableDefaultValueFromDB("ANACENT2", dsAna2)
      '--------------------------------------------------------------------------------------------------------------
      Anacent2SetDefaultValue(dsAna2)
      dsAna2Shared = dsAna2
      '--------------------------------------------------------------------------------------------------------------
      AddHandler dsAna2Shared.Tables("ANACENT2").ColumnChanging, AddressOf Anacent2BeforeColUpdate
      AddHandler dsAna2Shared.Tables("ANACENT2").ColumnChanged, AddressOf Anacent2AfterColUpdate
      '--------------------------------------------------------------------------------------------------------------
      dsAna2Shared.Tables("ANACENT2").AcceptChanges()
      bAna2HasChanges = False
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

  Public Overridable Function IsMultiDitta() As Boolean
    Try
      Return oCldSotc.IsMultiDitta()
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
