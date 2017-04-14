Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica
Imports System.IO

Public Class CLDMGCLAS
  Inherits CLD__BASE

  Public Overridable Function AssociaArticoli(ByVal strDitta As String, ByVal nLivello As Integer, _
    ByVal strCodcla1 As String, ByVal strCodcla2 As String, ByVal strCodcla3 As String, _
    ByVal strCodcla4 As String, ByVal strCodcla5 As String, ByVal dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE artico" & _
        " SET ar_ultagg = " & CDataSQL(Now.ToShortDateString) & "," & _
        " ar_oragg = " & CDblSQL(NTSCDec(Now.Hour * 100) + Now.Minute) & "," & _
        " ar_codcla1 = " & CStrSQL(strCodcla1)
      If nLivello > 1 And strCodcla2.Trim <> "" Then strSQL += ", ar_codcla2 = " & CStrSQL(strCodcla2)
      If nLivello > 2 And strCodcla3.Trim <> "" Then strSQL += ", ar_codcla3 = " & CStrSQL(strCodcla3)
      If nLivello > 3 And strCodcla4.Trim <> "" Then strSQL += ", ar_codcla4 = " & CStrSQL(strCodcla4)
      If nLivello > 4 And strCodcla5.Trim <> "" Then strSQL += ", ar_codcla5 = " & CStrSQL(strCodcla5)
      strSQL += " WHERE codditt = " & CStrSQL(strDitta)
      For i As Integer = 0 To (dttTmp.Rows.Count - 1)
        Execute(strSQL & " AND ar_codart = " & CStrSQL(dttTmp.Rows(i)!codart), _
          CLE__APP.DBTIPO.DBAZI)
      Next
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetData(ByVal strDitta As String, ByVal nLivello As Integer, _
                                      ByVal strCodcla1 As String, ByVal strCodcla2 As String, _
                                      ByVal strCodcla3 As String, ByVal strCodcla4 As String, _
                                      ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM artclas" & nLivello.ToString & _
               " WHERE codditt = " & CStrSQL(strDitta)
      If nLivello > 1 Then strSQL += " AND acl_codcla1 = " & CStrSQL(strCodcla1)
      If nLivello > 2 Then strSQL += " AND acl_codcla2 = " & CStrSQL(strCodcla2)
      If nLivello > 3 Then strSQL += " AND acl_codcla3 = " & CStrSQL(strCodcla3)
      If nLivello > 4 Then strSQL += " AND acl_codcla4 = " & CStrSQL(strCodcla4)
      strSQL += " ORDER BY acl_ordin" & nLivello
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTCLAS" & nLivello.ToString)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataLivello(ByVal strDitta As String, ByVal nLivello As Integer, _
                                ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * " & _
               " FROM artclas" & nLivello.ToString & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " ORDER BY acl_ordin" & nLivello
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTCLAS" & nLivello.ToString)

      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetDataArtAssociati(ByVal strDitta As String, ByVal strCodcla1 As String, _
    ByVal strCodcla2 As String, ByVal strCodcla3 As String, _
    ByVal strCodcla4 As String, ByVal strCodcla5 As String, _
    ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT codditt, ar_codart, ar_descr, ar_desint, 'N' AS xx_seleziona FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codcla1 = " & CStrSQL(strCodcla1) & _
        " AND ar_codcla2 = " & CStrSQL(IIf(strCodcla2.Trim <> "", strCodcla2, " ").ToString) & _
        " AND ar_codcla3 = " & CStrSQL(IIf(strCodcla3.Trim <> "", strCodcla3, " ").ToString) & _
        " AND ar_codcla4 = " & CStrSQL(IIf(strCodcla4.Trim <> "", strCodcla4, " ").ToString) & _
        " AND ar_codcla5 = " & CStrSQL(IIf(strCodcla5.Trim <> "", strCodcla5, " ").ToString)
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function SaveDataArtAssociati(ByVal strDitta As String, ByVal strCodcla1 As String, _
    ByVal strCodcla2 As String, ByVal strCodcla3 As String, _
    ByVal strCodcla4 As String, ByVal strCodcla5 As String, _
    ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE artico" & _
        " SET ar_ultagg = " & CDataSQL(Now.ToShortDateString) & "," & _
        " ar_oragg = " & CDblSQL(NTSCDec(Now.Hour * 100) + Now.Minute) & "," & _
        " ar_codcla1 = " & CStrSQL(strCodcla1) & _
        IIf(strCodcla2.Trim <> "", ", ar_codcla2 = " & CStrSQL(strCodcla2), "").ToString & _
        IIf(strCodcla3.Trim <> "", ", ar_codcla3 = " & CStrSQL(strCodcla3), "").ToString & _
        IIf(strCodcla4.Trim <> "", ", ar_codcla4 = " & CStrSQL(strCodcla4), "").ToString & _
        IIf(strCodcla5.Trim <> "", ", ar_codcla5 = " & CStrSQL(strCodcla5), "").ToString & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function DeleteDataArtAssociati(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE artico" & _
        " SET ar_ultagg = " & CDataSQL(Now.ToShortDateString) & "," & _
        " ar_oragg = " & CDblSQL(NTSCDec(Now.Hour * 100) + Now.Minute) & "," & _
        " ar_codcla1 = ' '," & _
        " ar_codcla2 = ' '," & _
        " ar_codcla3 = ' '," & _
        " ar_codcla4 = ' '," & _
        " ar_codcla5 = ' '" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCodart)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function EliminaAssociazione(ByVal strDitta As String, ByVal strCodcla1 As String, _
    ByVal strCodcla2 As String, ByVal strCodcla3 As String, _
    ByVal strCodcla4 As String, ByVal strCodcla5 As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE artico" & _
        " SET ar_ultagg = " & CDataSQL(Now.ToShortDateString) & "," & _
        " ar_oragg = " & CDblSQL(NTSCDec(Now.Hour * 100) + Now.Minute) & "," & _
        " ar_codcla1 = ' '," & _
        " ar_codcla2 = ' '," & _
        " ar_codcla3 = ' '," & _
        " ar_codcla4 = ' '," & _
        " ar_codcla5 = ' '" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codcla1 = " & CStrSQL(strCodcla1) & _
        IIf(strCodcla2.Trim <> "", " AND ar_codcla2 = " & CStrSQL(strCodcla2), "").ToString & _
        IIf(strCodcla3.Trim <> "", " AND ar_codcla3 = " & CStrSQL(strCodcla3), "").ToString & _
        IIf(strCodcla4.Trim <> "", " AND ar_codcla4 = " & CStrSQL(strCodcla4), "").ToString & _
        IIf(strCodcla5.Trim <> "", " AND ar_codcla5 = " & CStrSQL(strCodcla5), "").ToString
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CancellaLivelliSotto(ByVal nLivello As Integer, ByVal dtrRow As DataRow) As Boolean
    Dim strSQL As String = ""
    Try
      If nLivello = 1 Then GoTo CancellaLivello1

      If nLivello < 6 Then
        strSQL = "DELETE FROM artclas5" & _
          " WHERE codditt = " & CStrSQL(dtrRow("codditt", DataRowVersion.Original)) & _
          " AND acl_codcla1 = " & CStrSQL(dtrRow("acl_codcla1", DataRowVersion.Original))
        Select Case nLivello
          Case 1
          Case 2
          Case 3
            strSQL += " AND acl_codcla2 = " & CStrSQL(dtrRow("acl_codcla2", DataRowVersion.Original)) & _
              " AND acl_codcla3 = " & CStrSQL(dtrRow("acl_codcla", DataRowVersion.Original))
          Case 4
            strSQL += " AND acl_codcla2 = " & CStrSQL(dtrRow("acl_codcla2", DataRowVersion.Original)) & _
              " AND acl_codcla3 = " & CStrSQL(dtrRow("acl_codcla3", DataRowVersion.Original)) & _
              " AND acl_codcla4 = " & CStrSQL(dtrRow("acl_codcla", DataRowVersion.Original))
          Case 5
            strSQL += " AND acl_codcla2 = " & CStrSQL(dtrRow("acl_codcla2", DataRowVersion.Original)) & _
              " AND acl_codcla3 = " & CStrSQL(dtrRow("acl_codcla3", DataRowVersion.Original)) & _
              " AND acl_codcla4 = " & CStrSQL(dtrRow("acl_codcla4", DataRowVersion.Original)) & _
              " AND acl_codcla5 = " & CStrSQL(dtrRow("acl_codcla", DataRowVersion.Original))
        End Select
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      If nLivello < 5 Then
        strSQL = "DELETE FROM artclas4" & _
          " WHERE codditt = " & CStrSQL(dtrRow("codditt", DataRowVersion.Original)) & _
          " AND acl_codcla1 = " & CStrSQL(dtrRow("acl_codcla1", DataRowVersion.Original))
        Select Case nLivello
          Case 1
          Case 2
          Case 3
            strSQL += " AND acl_codcla2 = " & CStrSQL(dtrRow("acl_codcla2", DataRowVersion.Original)) & _
              " AND acl_codcla3 = " & CStrSQL(dtrRow("acl_codcla", DataRowVersion.Original))
          Case 4
            strSQL += " AND acl_codcla2 = " & CStrSQL(dtrRow("acl_codcla2", DataRowVersion.Original)) & _
              " AND acl_codcla3 = " & CStrSQL(dtrRow("acl_codcla3", DataRowVersion.Original)) & _
              " AND acl_codcla4 = " & CStrSQL(dtrRow("acl_codcla", DataRowVersion.Original))
        End Select
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      If nLivello < 4 Then
        strSQL = "DELETE FROM artclas3" & _
          " WHERE codditt = " & CStrSQL(dtrRow("codditt", DataRowVersion.Original)) & _
          " AND acl_codcla1 = " & CStrSQL(dtrRow("acl_codcla1", DataRowVersion.Original))
        Select Case nLivello
          Case 1
          Case 2
            strSQL += " AND acl_codcla2 = " & CStrSQL(dtrRow("acl_codcla", DataRowVersion.Original))
          Case 3
            strSQL += " AND acl_codcla2 = " & CStrSQL(dtrRow("acl_codcla2", DataRowVersion.Original)) & _
              " AND acl_codcla3 = " & CStrSQL(dtrRow("acl_codcla", DataRowVersion.Original))
          Case 4
            strSQL += " AND acl_codcla2 = " & CStrSQL(dtrRow("acl_codcla2", DataRowVersion.Original)) & _
              " AND acl_codcla3 = " & CStrSQL(dtrRow("acl_codcla3", DataRowVersion.Original))
        End Select
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      If nLivello < 3 Then
        strSQL = "DELETE FROM artclas2" & _
          " WHERE codditt = " & CStrSQL(dtrRow("codditt", DataRowVersion.Original)) & _
          " AND acl_codcla1 = " & CStrSQL(dtrRow("acl_codcla1", DataRowVersion.Original))
        Select Case nLivello
          Case 1
          Case 2 : strSQL += " AND acl_codcla2 = " & CStrSQL(dtrRow("acl_codcla", DataRowVersion.Original))
        End Select
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True

CancellaLivello1:
      For i As Integer = 5 To 2 Step -1
        strSQL = "DELETE FROM artclas" & i.ToString & _
          " WHERE codditt = " & CStrSQL(dtrRow("codditt", DataRowVersion.Original)) & _
          " AND acl_codcla1 = " & CStrSQL(dtrRow("acl_codcla", DataRowVersion.Original))
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function RitornaDescrClass(ByVal strDitta As String, ByVal dtrT As DataRow) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      RitornaDescrClass = ""
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dtrT!ar_codcla1).Trim <> "" Then
        strSQL = "SELECT acl_descla1 FROM artclas1" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND acl_codcla1 = " & CStrSQL(NTSCStr(dtrT!ar_codcla1))
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          RitornaDescrClass += IIf(RitornaDescrClass.Trim <> "", " / ", "").ToString & _
            NTSCStr(dttTmp.Rows(0)!acl_descla1)
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dtrT!ar_codcla2).Trim <> "" Then
        strSQL = "SELECT acl_descla2 FROM artclas2" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND acl_codcla1 = " & CStrSQL(NTSCStr(dtrT!ar_codcla1)) & _
          " AND acl_codcla2 = " & CStrSQL(NTSCStr(dtrT!ar_codcla2))
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          RitornaDescrClass += IIf(RitornaDescrClass.Trim <> "", " / ", "").ToString & _
            NTSCStr(dttTmp.Rows(0)!acl_descla2)
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dtrT!ar_codcla3).Trim <> "" Then
        strSQL = "SELECT acl_descla3 FROM artclas3" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND acl_codcla1 = " & CStrSQL(NTSCStr(dtrT!ar_codcla1)) & _
          " AND acl_codcla2 = " & CStrSQL(NTSCStr(dtrT!ar_codcla2)) & _
          " AND acl_codcla3 = " & CStrSQL(NTSCStr(dtrT!ar_codcla3))
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          RitornaDescrClass += IIf(RitornaDescrClass.Trim <> "", " / ", "").ToString & _
            NTSCStr(dttTmp.Rows(0)!acl_descla3)
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dtrT!ar_codcla4).Trim <> "" Then
        strSQL = "SELECT acl_descla4 FROM artclas4" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND acl_codcla1 = " & CStrSQL(NTSCStr(dtrT!ar_codcla1)) & _
          " AND acl_codcla2 = " & CStrSQL(NTSCStr(dtrT!ar_codcla2)) & _
          " AND acl_codcla3 = " & CStrSQL(NTSCStr(dtrT!ar_codcla3)) & _
          " AND acl_codcla4 = " & CStrSQL(NTSCStr(dtrT!ar_codcla4))
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          RitornaDescrClass += IIf(RitornaDescrClass.Trim <> "", " / ", "").ToString & _
            NTSCStr(dttTmp.Rows(0)!acl_descla4)
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCStr(dtrT!ar_codcla5).Trim <> "" Then
        strSQL = "SELECT acl_descla5 FROM artclas5" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND acl_codcla1 = " & CStrSQL(NTSCStr(dtrT!ar_codcla1)) & _
          " AND acl_codcla2 = " & CStrSQL(NTSCStr(dtrT!ar_codcla2)) & _
          " AND acl_codcla3 = " & CStrSQL(NTSCStr(dtrT!ar_codcla3)) & _
          " AND acl_codcla4 = " & CStrSQL(NTSCStr(dtrT!ar_codcla4)) & _
          " AND acl_codcla5 = " & CStrSQL(NTSCStr(dtrT!ar_codcla5))
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          RitornaDescrClass += IIf(RitornaDescrClass.Trim <> "", " / ", "").ToString & _
            NTSCStr(dttTmp.Rows(0)!acl_descla5)
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function EliminaNodo(ByVal strDitta As String, ByVal strTag As String) As Boolean
    Dim i As Integer = 0
    Dim y As Integer = 0
    Dim strSQL As String = ""
    Dim strNodo() As String
    Dim dbConn As DbConnection = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      strNodo = strTag.Split("|"c)
      '--------------------------------------------------------------------------------------------------------------
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE artico SET"
      For i = 1 To 5
        strSQL += " ar_codcla" & i.ToString & " = ' '" & IIf(i < 5, ",", "").ToString
      Next
      strSQL += " WHERE codditt = " & CStrSQL(strDitta)
      For i = (strNodo.Length - 1) To 0 Step -1
        strSQL += " AND ar_codcla" & (i + 1).ToString & " = " & CStrSQL(strNodo(i))
      Next
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '--------------------------------------------------------------------------------------------------------------
      For i = 5 To strNodo.Length Step -1
        strSQL = "DELETE FROM artclas" & i.ToString & _
                 " WHERE codditt = " & CStrSQL(strDitta)
        For y = 0 To (strNodo.Length - 1)
          strSQL += " AND acl_codcla" & (y + 1).ToString & " = " & CStrSQL(strNodo(y))
        Next
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "DELETE FROM artclas" & i.ToString & "lin" & _
                 " WHERE codditt = " & CStrSQL(strDitta)
        For y = 0 To (strNodo.Length - 1)
          strSQL += " AND acx_codcla" & (y + 1).ToString & " = " & CStrSQL(strNodo(y))
        Next
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next
      '--------------------------------------------------------------------------------------------------------------
      ChiudiTrans()
      dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      If IsInTrans Then AnnullaTrans()
      If Not dbConn Is Nothing Then If dbConn.State = ConnectionState.Open Then dbConn.Close()
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Function

#Region "Descrizioni in Lingua"
  Public Overridable Sub GetDataInLingua(ByVal strDitta As String, ByVal nLivello As Integer, _
                                      ByVal strCodcla1 As String, ByVal strCodcla2 As String, _
                                      ByVal strCodcla3 As String, ByVal strCodcla4 As String, _
                                      ByVal strCodcla5 As String, ByVal dsShared As DataSet)
    Dim strSQL As String = ""
    Dim dttDati As DataTable = Nothing
    Dim dsTmp As DataSet = Nothing
    Dim strNomeTabella As String = ""
    Try
      strNomeTabella = "ARTCLAS" & nLivello.ToString & "LIN"

      strSQL = "SELECT " & strNomeTabella & ".*, tb_desling AS xx_codvalu FROM " & strNomeTabella & _
               " INNER JOIN tabling ON " & _
               strNomeTabella & ".acx_codvalu = tabling.tb_codling" & _
               " WHERE codditt = " & CStrSQL(strDitta)
      If nLivello = 1 Then strSQL += " AND acx_codcla1 = " & CStrSQL(strCodcla1)
      If nLivello = 2 Then strSQL += " AND acx_codcla2 = " & CStrSQL(strCodcla2)
      If nLivello = 3 Then strSQL += " AND acx_codcla3 = " & CStrSQL(strCodcla3)
      If nLivello = 4 Then strSQL += " AND acx_codcla4 = " & CStrSQL(strCodcla4)
      If nLivello = 5 Then strSQL += " AND acx_codcla5 = " & CStrSQL(strCodcla5)

      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strNomeTabella, dsTmp)
      SetTableDefaultValueFromDB(strNomeTabella, dsTmp)
      dttDati = dsTmp.Tables(strNomeTabella).Copy
      dttDati.TableName = "ARTCLASLIN"
      dsShared.Tables.Add(dttDati)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Sub

  Public Overridable Sub CancellaDescrizioneInLingua(ByVal strDitta As String, _
                                    ByVal nLivello As Integer, _
                                    ByVal strCodcla1 As String, ByVal strCodcla2 As String, _
                                    ByVal strCodcla3 As String, ByVal strCodcla4 As String, _
                                    ByVal strCodcla5 As String, ByVal nLingua As Integer)
    Dim strSQL As String = ""
    Try
      strSQL = CancellaDescrizioneInLingua_ComponiSQL(strDitta, nLivello, _
               strCodcla1, strCodcla2, strCodcla3, strCodcla4, strCodcla5, nLingua)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Sub

  Public Overridable Function CancellaDescrizioneInLingua_ComponiSQL(ByVal strDitta As String, _
                                  ByVal nLivello As Integer, _
                                  ByVal strCodcla1 As String, ByVal strCodcla2 As String, _
                                  ByVal strCodcla3 As String, ByVal strCodcla4 As String, _
                                  ByVal strCodcla5 As String, ByVal nLingua As Integer) As String
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM artclas" & nLivello.ToString & "lin" & _
               " WHERE codditt = " & CStrSQL(strDitta)
      If nLivello >= 1 Then strSQL += " AND acx_codcla1 = " & CStrSQL(strCodcla1)
      If nLivello >= 2 Then strSQL += " AND acx_codcla2 = " & CStrSQL(strCodcla2)
      If nLivello >= 3 Then strSQL += " AND acx_codcla3 = " & CStrSQL(strCodcla3)
      If nLivello >= 4 Then strSQL += " AND acx_codcla4 = " & CStrSQL(strCodcla4)
      If nLivello = 5 Then strSQL += " AND acx_codcla5 = " & CStrSQL(strCodcla5)
      strSQL += " AND acx_codvalu = " & nLingua

      Return strSQL
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Sub SalvaDescrizioneInLingua(ByVal strDitta As String, _
                                  ByVal nLivello As Integer, ByVal dtrN As DataRow)
    Dim strSQL As String = ""
    Dim nTransRes As Integer = 0
    Dim dbConn As DbConnection = Nothing
    Dim strQueryInsertField As String = ""

    Dim strCodcla1 As String = ""
    Dim strCodcla2 As String = ""
    Dim strCodcla3 As String = ""
    Dim strCodcla4 As String = ""
    Dim strCodcla5 As String = ""
    Dim nLingua As Integer = 0
    Try
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)
      If nLivello >= 1 Then strCodcla1 = NTSCStr(dtrN!acx_codcla1)
      If nLivello >= 2 Then strCodcla2 = NTSCStr(dtrN!acx_codcla2)
      If nLivello >= 3 Then strCodcla3 = NTSCStr(dtrN!acx_codcla3)
      If nLivello >= 4 Then strCodcla4 = NTSCStr(dtrN!acx_codcla4)
      If nLivello = 5 Then strCodcla5 = NTSCStr(dtrN!acx_codcla5)
      nLingua = NTSCInt(dtrN!acx_codvalu)

      strSQL = CancellaDescrizioneInLingua_ComponiSQL(strDitta, nLivello, _
               strCodcla1, strCodcla2, strCodcla3, strCodcla4, strCodcla5, nLingua)
      nTransRes = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If nTransRes = -1 Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Throw (New NTSException(strSQL))
      End If

      strQueryInsertField = GetQueryInsertField(dtrN.Table, "acx_", "")
      strSQL = "INSERT INTO artclas" & nLivello.ToString & "lin " & strQueryInsertField & _
               " VALUES " & GetQueryInsertValue(dtrN.Table, dtrN, "acx_", "")
      nTransRes = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      If nTransRes = -1 Then
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Throw (New NTSException(strSQL))
      End If

      ChiudiTrans()
      dbConn.Close()
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      If IsInTrans Then AnnullaTrans()
      If dbConn IsNot Nothing AndAlso dbConn.State = ConnectionState.Open Then dbConn.Close()
    End Try
  End Sub
#End Region
End Class
