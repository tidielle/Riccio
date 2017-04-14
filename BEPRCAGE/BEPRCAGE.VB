Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEPRCAGE
  Inherits CLE__BASE

  Public oCldCage As CLDPRCAGE
  
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

  Public Overrides Function Init(ByRef App As CLE__APP, _
                                    ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                                    ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                                    ByVal strRemotePort As String) As Boolean
    SetDbVersionNeeded(21, 13)
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDPRCAGE"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldCage = CType(MyBase.ocldBase, CLDPRCAGE)
    oCldCage.Init(oApp)

    Return True
  End Function

  Public Overrides Sub Nuovo()
    Try
      MyBase.Nuovo()

      dsShared.Tables("TABCAGE").Rows(dsShared.Tables("TABCAGE").Rows.Count - 1)!tb_datper = IntSetDate("01/01/1900")
      dsShared.Tables("TABCAGE").Rows(dsShared.Tables("TABCAGE").Rows.Count - 1)!tb_datpro = IntSetDate("01/01/1900")
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overrides Function Apri(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verrà usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      If Not ocldBase.LeggiTabellaSemplice(strDittaCorrente, "TABCAGE", ds) Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      ocldBase.SetTableDefaultValueFromDB("TABCAGE", ds)

      SetDefaultValue(ds)

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("TABCAGE").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("TABCAGE").ColumnChanged, AddressOf AfterColUpdate

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
  Public Overrides Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not TestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog <> "-1" Then
        bResult = ocldBase.ScriviTabellaSemplice(strDittaCorrente, "TABCAGE", dsShared.Tables("TABCAGE"), "", "", "")
      Else
        bResult = ocldBase.ScriviTabellaSemplice(strDittaCorrente, "TABCAGE", dsShared.Tables("TABCAGE"), _
                                                 strActLogProg, strActLogNomOggLog, strActLogDesLog)
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

  Public Overridable Function CancellaProv(ByVal lCodCage As Integer) As Boolean
    Try
      Return oCldCage.CancellaProv(strDittaCorrente, lCodCage)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overrides Function TestPreSalva() As Boolean
    '-------------------------------------------------
    'eseguo i controlli necessari prima di fare il salvataggio della riga
    Dim dtrCurrRow As DataRow() = dsShared.Tables("TABCAGE").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
    Dim i As Integer = 0
    Try
      For i = 0 To dtrCurrRow.Length - 1
        If NTSCInt(dtrCurrRow(i)!tb_codcage) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128726824769054157, "Il campo codice agente deve contenere un valore compreso tra 1 e 9999")))
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
    End Try
  End Function

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


    Catch ex As Exception
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
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(oApp.Tr(Me, 128067822802778673, strErr))
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

  Public Overridable Sub BeforeColUpdate_tb_codcage(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim dtrTmp() As DataRow = Nothing
    Dim strErrore As String = ""
    Try
      If dsShared.Tables("TABCAGE").Rows.Count > 1 Then
        dtrTmp = dsShared.Tables("TABCAGE").Select("tb_codcage = " & e.ProposedValue.ToString())
        If dtrTmp.Length > 0 Then
          e.ProposedValue = e.Row(e.Column.ColumnName)
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128171551470156250, "Cod. Agente già esistente: inserire un nuovo Codice Agente.")))
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

  Public Overridable Function ValCodiceDB(ByVal strCodice As String, ByVal strTipo As String, ByVal strTabella As String, ByRef strDesc As String) As Boolean
    Try
      If Not ocldBase.ValCodiceDb(strCodice, strDittaCorrente, strTabella, strTipo, strDesc) Then Return False

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

  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      '--------------------------------------------------------------------------------------------------------------
      ds.Tables("TABCAGE").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("TABCAGE").Columns("tb_tiptab").DefaultValue = "A"
      ds.Tables("TABCAGE").Columns("tb_tippro").DefaultValue = "F"
      ds.Tables("TABCAGE").Columns("tb_datper").DefaultValue = IntSetDate("01/01/1900")
      ds.Tables("TABCAGE").Columns("tb_datpro").DefaultValue = IntSetDate("01/01/1900")
      ds.Tables("TABCAGE").Columns("tb_datiniz").DefaultValue = IntSetDate("01/01/1900")
      ds.Tables("TABCAGE").Columns("tb_datfin").DefaultValue = IntSetDate("31/12/2099")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Sub

End Class
