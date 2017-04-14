Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO

Public Class CLFMGARTI
  Inherits CLEMGARTI

  Public bHasChangesBarcode As Boolean = False

    Public Overrides Function Apri(ByVal strDitta As String, ByRef ds As System.Data.DataSet) As Boolean
        Try
            If MyBase.Apri(strDitta, ds) Then

                oCldArti.SetTableDefaultValueFromDB("TABHHAR", ds)
                SetDefaultValue(ds)

                dsShared = ds
                '--------------------------------------
                'creo gli eventi per la gestione del datatable dentro l'entity
                AddHandler dsShared.Tables("TABHHAR").ColumnChanging, AddressOf BeforeColUpdate_TABHHAR_tb_artcamp
                AddHandler dsShared.Tables("TABHHAR").TableNewRow, AddressOf BeforeInsert_TABHHAR_tb_artcamp
                AddHandler dsShared.Tables("TABHHAR").ColumnChanged, AddressOf AfterColUpdate

                bHasChangesBarcode = False
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

    '  Public Overrides Sub Nuovo(ByRef ds As System.Data.DataSet)
    '      Try
    '          MyBase.Nuovo(ds)

    '          oCldArti.SetTableDefaultValueFromDB("TABHHAR", ds)
    '          SetDefaultValue(ds)
    '          '--------------------------------------
    '          'creo gli eventi per la gestione del datatable dentro l'entity
    '          AddHandler dsShared.Tables("TABHHAR").ColumnChanging, AddressOf BeforeColUpdate
    '          AddHandler dsShared.Tables("TABHHAR").ColumnChanged, AddressOf AfterColUpdate

    '          bHasChangesBarcode = False

    '      Catch ex As Exception
    '          '--------------------------------------------------------------
    '          If GestErrorCallThrow() Then
    '              Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    '          Else
    '              ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
    '          End If
    '          '--------------------------------------------------------------
    '      End Try

    '  End Sub

    'Public Overridable Function RipristinaBarcode(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    '    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    '    Try
    '        dsShared.Tables("TABHHAR").Select(strFilter)(nRow).RejectChanges()
    '        bHasChangesBarcode = False

    '        Return True
    '    Catch ex As Exception
    '    End Try
    'End Function

    'Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    '    Try
    '        'ora imposto i valori di default diversi da quelli impostati nel database
    '        ds.Tables("TABHHAR").Columns("codditt").DefaultValue = strDittaCorrente
    '        ds.Tables("TABHHAR").Columns("tb_codart").DefaultValue = 


    '    Catch ex As Exception
    '        '--------------------------------------------------------------
    '        If GestErrorCallThrow() Then
    '            Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    '        Else
    '            ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
    '        End If
    '        '--------------------------------------------------------------
    '    End Try
    'End Sub

    Public Overridable Sub BeforeColUpdate_TABHHAR_tb_artcamp(sender As Object, e As DataColumnChangeEventArgs)
        Try
            Dim nveicolo As Integer = 0
            nveicolo = NTSCInt(e.ProposedValue)
            If oCldArti.ValCodiceDb(nveicolo.ToString, strDittaCorrente, "ARTEST", "N") = False Then
                ThrowRemoteEvent(New NTSEventArgs("", "Veicolo inesistente"))
                e.ProposedValue = e.Row(e.Column.ColumnName)
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
    Public Overridable Sub BeforeInsert_TABHHAR_tb_artcamp(sender As Object, e As DataTableNewRowEventArgs)
        Try
            'Dim nveicolo As Integer = 0
            'nveicolo = NTSCInt(e.ProposedValue)
            'If oCldArti.ValCodiceDb(nveicolo.ToString, strDittaCorrente, "ARTEST", "N") = False Then
            '    ThrowRemoteEvent(New NTSEventArgs("", "Veicolo inesistente"))
            '    e.ProposedValue = e.Row(e.Column.ColumnName)
            '    Return
            'End If
            MsgBox("qqq")
        Catch ex As Exception
            '--------------------------------------------------------------
            If GestErrorCallThrow() Then
                Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
            Else
                ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
            End If
            '--------------------------------------------------------------    
        End Try


    End Sub

    'Public Overrides Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    '    Dim strErr As String = ""
    '    Try
    '        'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
    '        'solo se il dato e uguale a quello precedentemente contenuto nella cella
    '        If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
    '            strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
    '            Return
    '        End If
    '        '-------------------------------------------------------------
    '        'controllo che in una cella short non venga inserito un numero troppo grande
    '        If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
    '        '-------------------------------------------------------------
    '        'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
    '        Dim strFunction As String = "BeforeColUpdate_" & e.Column.ColumnName.ToLower
    '        Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
    '        If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

    '    Catch ex As Exception
    '        '--------------------------------------------------------------
    '        If GestErrorCallThrow() Then
    '            Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    '        Else
    '            ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
    '        End If
    '        '--------------------------------------------------------------
    '    End Try
    'End Sub

    'Public Overrides Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    '    Try
    '        'non valido la colonna se il dato non è cambiato
    '        If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
    '            strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
    '            Return
    '        End If

    '        bHasChangesBarcode = True

    '        'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
    '        'vengano fatte le routine di validazione del caso

    '        ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

    '        e.Row.EndEdit()
    '        e.Row.EndEdit()

    '        '-------------------------------------------------------------
    '        'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
    '        Dim strFunction As String = "AfterColUpdate_" & e.Column.ColumnName.ToLower
    '        Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
    '        If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})
    '    Catch ex As Exception
    '        '--------------------------------------------------------------
    '        If GestErrorCallThrow() Then
    '            Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    '        Else
    '            ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
    '        End If
    '        '--------------------------------------------------------------
    '    End Try
    'End Sub

End Class
