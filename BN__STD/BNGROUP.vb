Imports NTSInformatica.CLN__STD

Public Class CLEGROUPBY

  Public Overridable Function NTSGroupBy(ByRef dttIn As DataTable, ByRef dttOut As DataTable, _
                                         ByVal strSelect As String, ByVal strWhere As String, _
                                         ByVal strGroupBy As String) As Boolean
    'dato un datatable in ingresso, restituisce un datatable con i dati presenti nella select
    'raggruppati, con la possibilità di fare MIN, MAX, SUM, COUNT, FIRST, LAST
    'non gestisce gli alias!!!
    Dim nRow As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim strGroup() As String = strGroupBy.Split(CChar(","))
    Dim i As Integer = 0
    Dim dttMap As New DataTable

    Try
      dttOut.Clear()
      dttOut = New DataTable

      'nella strSelect non ci deve essere '*'
      If strSelect.IndexOf("*") > -1 Then Throw New Exception("Nella select non deve essere presente il carattere '*'")
      If Not TrovaCampiSelect(dttMap, strSelect) Then Return False
      If dttMap.Rows.Count = 0 Then Throw New Exception("Nella select devono essere indicati dei campi presenti nel datatable")

      If Not AddColumns(dttMap, dttIn, dttOut) Then Return False

      dtrT = dttIn.Select(strWhere, strGroupBy)
      'la order by è uguale alla group by
      For nRow = 0 To dtrT.Length - 1
        If nRow = 0 Then
          dttOut.Rows.Add(dttOut.NewRow)
          AddRowOut(dttMap, dtrT(nRow), dttOut.Rows(dttOut.Rows.Count - 1), True)
        Else
          If strGroup(0) = "" Then
            'la riga è uguale a quella precendete
            AddRowOut(dttMap, dtrT(nRow), dttOut.Rows(dttOut.Rows.Count - 1), False)
          Else
            For i = 0 To strGroup.Length - 1
              'per le stringhe lavoro case-insensitive
              If dttIn.Columns(strGroup(i).Trim).DataType.ToString.IndexOf("String") > -1 And Not dtrT(nRow)(strGroup(i).Trim).Equals(DBNull.Value) Then
                If CLN__STD.NTSCStr(dtrT(nRow)(strGroup(i).Trim)).ToUpper.Equals(CLN__STD.NTSCStr(dtrT(nRow - 1)(strGroup(i).Trim)).ToUpper) = False Then
                  i = strGroup.Length + 10
                End If
              Else
                If dtrT(nRow)(strGroup(i).Trim).Equals(dtrT(nRow - 1)(strGroup(i).Trim)) = False Then
                  i = strGroup.Length + 10
                End If
              End If
            Next
            If i > strGroup.Length + 1 Then
              'la riga attuale è diversa da quella precedente
              dttOut.Rows.Add(dttOut.NewRow)
              AddRowOut(dttMap, dtrT(nRow), dttOut.Rows(dttOut.Rows.Count - 1), True)
            Else
              'la riga è uguale a quella precendete
              AddRowOut(dttMap, dtrT(nRow), dttOut.Rows(dttOut.Rows.Count - 1), False)
            End If
          End If
        End If
      Next

      Return True

    Catch ex As Exception
      Throw ex
    End Try
  End Function

  Private Function TrovaCampiSelect(ByRef dttMap As DataTable, ByVal strSelect As String) As Boolean
    Dim strT() As String
    Dim strOut As String = ""
    Dim i As Integer = 0
    Dim nStart As Integer = 0
    Dim nEnd As Integer = 0

    Try
      If dttMap.Columns.Count = 0 Then
        dttMap.Columns.Add("name", GetType(String))
        dttMap.Columns.Add("alias", GetType(String))
        dttMap.Columns.Add("expr", GetType(String))

        dttMap.Columns("name").DefaultValue = ""
        dttMap.Columns("alias").DefaultValue = ""
        dttMap.Columns("expr").DefaultValue = ""
      End If

      strT = strSelect.Split(CChar(","))
      For i = 0 To strT.Length - 1
        dttMap.Rows.Add(dttMap.NewRow())

        nStart = strT(i).IndexOf(CChar("("))
        If nStart > -1 Then
          nEnd = strT(i).IndexOf(CChar(")"), nStart)
          dttMap.Rows(dttMap.Rows.Count - 1)!expr = strT(i).Substring(0, nStart).Trim
          dttMap.Rows(dttMap.Rows.Count - 1)!name = strT(i).Substring(nStart + 1, nEnd - (nStart + 1)).Trim
        Else
          If strT(i).Trim.ToLower.IndexOf(" as ") > -1 Then
            dttMap.Rows(dttMap.Rows.Count - 1)!name = strT(i).Trim.Substring(0, strT(i).Trim.ToLower.IndexOf(" as "))
          Else
            dttMap.Rows(dttMap.Rows.Count - 1)!name = strT(i).Trim
          End If
        End If
        dttMap.Rows(dttMap.Rows.Count - 1)!alias = dttMap.Rows(dttMap.Rows.Count - 1)!name

        nStart = strT(i).ToLower.ToLower.IndexOf(" as ")
        If nStart > -1 Then dttMap.Rows(dttMap.Rows.Count - 1)!alias = strT(i).Substring(nStart + 4).Trim

      Next
      dttMap.AcceptChanges()

      Return True

    Catch ex As Exception
      Throw ex
    End Try
  End Function

  Private Function AddColumns(ByRef dttMap As DataTable, ByRef dttIn As DataTable, ByRef dttOut As DataTable) As Boolean

    Dim i As Integer = 0
    Dim strColName As String = ""

    Try
      dttOut.Columns.Clear()
      For i = 0 To dttMap.Rows.Count - 1
        Try
          strColName = dttIn.Columns(dttMap.Rows(i)!name.ToString).ColumnName
        Catch ex As Exception
          Throw New Exception("Nel datatable principale non è presente la colonna '" & dttMap.Rows(i)!name.ToString & "'")
        End Try
        If dttMap.Rows(i)!expr.ToString = "count" Then
          dttOut.Columns.Add(dttMap.Rows(i)!alias.ToString, GetType(Integer))
        Else
          dttOut.Columns.Add(dttMap.Rows(i)!alias.ToString, dttIn.Columns(strColName).DataType)
        End If
      Next

      Return True

    Catch ex As Exception
      Throw ex
    End Try
  End Function

  Private Function AddRowOut(ByRef dttMap As DataTable, ByRef dtrIn As DataRow, ByRef dtrOut As DataRow, ByVal bNewRow As Boolean) As Boolean
    Dim i As Integer = 0
    Try
      For i = 0 To dttMap.Rows.Count - 1
        Select Case dttMap.Rows(i)!expr.ToString.ToUpper
          Case "SUM"
            dtrOut(dttMap.Rows(i)!alias.ToString) = NTSCDec(dtrOut(dttMap.Rows(i)!alias.ToString)) + NTSCDec(dtrIn(dttMap.Rows(i)!name.ToString))
          Case "FIRST"
            If dtrOut(dttMap.Rows(i)!alias.ToString).Equals(DBNull.Value) Then
              dtrOut(dttMap.Rows(i)!alias.ToString) = dtrIn(dttMap.Rows(i)!name.ToString)
            End If
          Case "LAST"
            dtrOut(dttMap.Rows(i)!alias.ToString) = dtrIn(dttMap.Rows(i)!name.ToString)
          Case "MIN"
            If dtrOut(dttMap.Rows(i)!alias.ToString).Equals(DBNull.Value) Then
              Select Case dtrOut.Table.Columns(dttMap.Rows(i)!alias.ToString).DataType.FullName
                Case "System.DateTime"
                  dtrOut(dttMap.Rows(i)!alias.ToString) = dtrIn(dttMap.Rows(i)!name.ToString)   'non faccio NTSCDATE, altrimenti in un campo data nullabile con valore null metterei 01/01/1900
                Case "System.String"
                  dtrOut(dttMap.Rows(i)!alias.ToString) = NTSCStr(dtrIn(dttMap.Rows(i)!name.ToString))
                Case Else   'numero
                  dtrOut(dttMap.Rows(i)!alias.ToString) = NTSCDec(dtrIn(dttMap.Rows(i)!name.ToString))
              End Select
            Else
              Select Case dtrOut.Table.Columns(dttMap.Rows(i)!alias.ToString).DataType.FullName
                Case "System.DateTime"
                  If NTSCDate(dtrIn(dttMap.Rows(i)!name.ToString)) < NTSCDate(dtrOut(dttMap.Rows(i)!alias.ToString)) Then
                    dtrOut(dttMap.Rows(i)!alias.ToString) = NTSCDate(dtrIn(dttMap.Rows(i)!name.ToString))
                  End If
                Case "System.String"
                  If NTSCStr(dtrIn(dttMap.Rows(i)!name.ToString)) < NTSCStr(dtrOut(dttMap.Rows(i)!alias.ToString)) Then
                    dtrOut(dttMap.Rows(i)!alias.ToString) = NTSCStr(dtrIn(dttMap.Rows(i)!name.ToString))
                  End If
                Case Else   'numero
                  If NTSCDec(dtrIn(dttMap.Rows(i)!name.ToString)) < NTSCDec(dtrOut(dttMap.Rows(i)!alias.ToString)) Then
                    dtrOut(dttMap.Rows(i)!alias.ToString) = NTSCDec(dtrIn(dttMap.Rows(i)!name.ToString))
                  End If
              End Select
            End If
          Case "MAX"
            Select Case dtrOut.Table.Columns(dttMap.Rows(i)!alias.ToString).DataType.FullName
              Case "System.DateTime"
                If NTSCDate(dtrIn(dttMap.Rows(i)!name.ToString)) > NTSCDate(dtrOut(dttMap.Rows(i)!alias.ToString)) Then
                  dtrOut(dttMap.Rows(i)!alias.ToString) = NTSCDate(dtrIn(dttMap.Rows(i)!name.ToString))
                End If
              Case "System.String"
                If NTSCStr(dtrIn(dttMap.Rows(i)!name.ToString)) > NTSCStr(dtrOut(dttMap.Rows(i)!alias.ToString)) Then
                  dtrOut(dttMap.Rows(i)!alias.ToString) = NTSCStr(dtrIn(dttMap.Rows(i)!name.ToString))
                End If
              Case Else   'numero
                If NTSCDec(dtrIn(dttMap.Rows(i)!name.ToString)) > NTSCDec(dtrOut(dttMap.Rows(i)!alias.ToString)) Then
                  dtrOut(dttMap.Rows(i)!alias.ToString) = NTSCDec(dtrIn(dttMap.Rows(i)!name.ToString))
                End If
            End Select
          Case "COUNT"
            dtrOut(dttMap.Rows(i)!alias.ToString) = NTSCInt(dtrOut(dttMap.Rows(i)!alias.ToString)) + 1
          Case Else
            If bNewRow Then dtrOut(dttMap.Rows(i)!alias.ToString) = dtrIn(dttMap.Rows(i)!name.ToString)
        End Select
      Next

      Return True

    Catch ex As Exception
      Throw ex
    End Try
  End Function
End Class