Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGCALS
  Inherits CLD__BASE

  Public Overridable Function ElaboraListini(ByVal strDitta As String, ByVal dtrTmp As DataRow) As Boolean
    Dim strSQL As String = ""
    Dim dtDataScad As New Date(2099, 12, 31)
    Try
      '---------------------------------------------------------------------------------------
      '--- Con un ciclo aggiorna i listini che hanno la data di scadenza pari al giorno
      '--- prima del record da cancellare, mettendola al '31/12/2099' o al giorno prima dell'inizio del listino successivo
      '---------------------------------------------------------------------------------------
      With dtrTmp
        Dim strWhere As String = " WHERE codditt = " & CStrSQL(strDitta) & _
                                 "   AND lc_codart = " & CStrSQL(!lc_codart) & _
                                 "   AND lc_codlavo = " & NTSCStr(!lc_codlavo) & _
                                 "   AND lc_conto = " & NTSCStr(!lc_conto) & _
                                 "   AND lc_coddest = " & NTSCStr(!lc_coddest) & _
                                 "   AND lc_codvalu = " & NTSCStr(!lc_codvalu) & _
                                 "   AND lc_codtpro = " & NTSCStr(!lc_codtpro) & _
                                 "   AND lc_listino = " & NTSCStr(!lc_listino) & _
                                 "   AND lc_fase = " & NTSCStr(!lc_fase)
        'Verifica se c'è un listino nelle stesse condizioni 
        ' per portare la data scadenza del listino precedente al giorno precedente l'inizio del listino successivo a quello da cancellare
        strSQL = "SELECT MIN(lc_datagg) AS dtagg FROM listini" & _
                 strWhere & _
                 "   AND lc_datagg > " & CDataSQL(NTSCDate(!lc_datscad))

        Dim dttTmp As DataTable = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          If dttTmp.Rows(0)!dtagg.Equals(DBNull.Value) = False Then
            dtDataScad = NTSCDate(dttTmp.Rows(0)!dtagg).AddDays(-1) 'Toglie un giorno dalla data aggiornamento per calcolare la data scadenza
          End If
        End If

        'Aggiorna il listino precedente con la nuova data scadenza che non si accavalla con eventuali listini successivi
        strSQL = "UPDATE listini" & _
                 "   SET lc_datscad = " & CDataSQL(dtDataScad) & _
                 strWhere & _
                 "   AND lc_datscad = " & CDataSQL(NTSCDate(!lc_datagg).AddDays(-1))
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End With
      '-------------------------------------------------------------------------------------
      '--- Se il listino da aggiornare è collegato ad un conto, aggiorna il TimeStamp
      '--- di ANAGRA per ragioni di concorrenza, se eventualmente BS--CLIE fosse aperto
      '-------------------------------------------------------------------------------------
      If NTSCInt(dtrTmp!lc_conto) <> 0 Then UpdateTimeStampConto(strDitta, NTSCInt(dtrTmp!lc_conto))
      '-------------------------------------------------------------------------------------

      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ElaboraListiniErrori(ByVal strDitta As String, ByVal strQuery As String, _
                                                 ByVal bopTipolist0 As Boolean, ByVal stredDtinval As String, _
                                                 ByVal stredCodvalu As String, ByVal stredCodlavo As String, _
                                                 ByVal bopTipolist1 As Boolean, ByVal stredConto As String, _
                                                 ByVal bedCodtpro As Boolean, ByVal stredCodtpro As String, _
                                                 ByVal bopUltimaFase As Boolean, ByVal bopSoloFase As Boolean, _
                                                 ByVal stredFase As String, ByVal stredListino As String, _
                                                 ByVal bopTipolist2 As Boolean, ByRef lRecords As Integer) As Boolean
    Try
      'obsoleta
      Return ElaboraListiniErrori(strDitta, strQuery, bopTipolist0, stredDtinval, stredCodvalu, stredCodlavo, _
                                  bopTipolist1, stredConto, bedCodtpro, stredCodtpro, bopUltimaFase, bopSoloFase, _
                                  stredFase, stredListino, bopTipolist2, lRecords, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ElaboraListiniErrori(ByVal strDitta As String, ByVal strQuery As String, _
                                                   ByVal bopTipolist0 As Boolean, ByVal stredDtinval As String, _
                                                   ByVal stredCodvalu As String, ByVal stredCodlavo As String, _
                                                   ByVal bopTipolist1 As Boolean, ByVal stredConto As String, _
                                                   ByVal bedCodtpro As Boolean, ByVal stredCodtpro As String, _
                                                   ByVal bopUltimaFase As Boolean, ByVal bopSoloFase As Boolean, _
                                                   ByVal stredFase As String, ByVal stredListino As String, _
                                                   ByVal bopTipolist2 As Boolean, ByRef lRecords As Integer, _
                                                   ByVal lCoddest As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strSQLTmp As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strQuery, bopTipolist0, stredDtinval, stredCodvalu, stredCodlavo, _
                                  bopTipolist1, stredConto, bedCodtpro, stredCodtpro, bopUltimaFase, bopSoloFase, _
                                  stredFase, stredListino, bopTipolist2, lRecords, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        lRecords = NTSCInt(oIn(15))        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      'aggiungo la where dei campi con OR e AND rimappati
      TraduciWhere(strQuery, strSQLTmp)
      '---------------------------------------------------------------------------------------
      '--- Se il tipo listino NON è 'Generico', aggiorna massivamente, il TimeStamp di ANAGRA
      '--- che ha riferimenti nei LISTINI selezionati per l'aggiornamento
      '--- (per ragioni di concorrenza, se eventualmente BS--CLIE fosse aperto)
      '---------------------------------------------------------------------------------------
      If bopTipolist0 = False Then
        strSQL = "UPDATE anagra" & _
          " SET an_ultagg = GETDATE()" & _
          " FROM listini, artico" & _
          " WHERE anagra.codditt = listini.codditt" & _
          " AND anagra.an_conto = listini.lc_conto" & _
          " AND artico.codditt = listini.codditt" & _
          " AND artico.ar_codart = listini.lc_codart" & _
          " AND anagra.codditt = " & CStrSQL(strDitta)
        strSQL &= " AND lc_datagg = " & CDataSQL(stredDtinval) & _
          " AND lc_codvalu = " & stredCodvalu & _
          " AND lc_codlavo = " & stredCodlavo & _
          " AND lc_conto <> 0 AND lc_listino = 0" & _
          " AND lc_tipo = " & CStrSQL(IIf(bopTipolist1 = True, "C", "F")) 
        If NTSCInt(stredConto) <> 0 Then
          strSQL &= " AND lc_conto = " & stredConto & _
                    " AND lc_coddest = " & lCoddest.ToString
        End If
        If bedCodtpro = True Then strSQL &= " AND lc_codtpro = " & stredCodtpro
        If bopUltimaFase = True Then strSQL &= " AND ar_ultfase = lc_fase"
        If bopSoloFase = True Then strSQL &= " AND lc_fase = " & stredFase

        strSQL &= strSQLTmp

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '---------------------------------------------------------------------------------------
      strSQL = "DELETE listini FROM artico" & _
        " WHERE listini.codditt = artico.codditt" & _
        " AND listini.lc_codart = artico.ar_codart" & _
        " AND listini.codditt = " & CStrSQL(strDitta) & _
        strSQLTmp
      If bopTipolist0 Then
        strSQL &= " AND lc_tipo = ' ' AND lc_conto = 0"
        If Not NTSCInt(stredListino) = 0 Then strSQL &= " AND lc_listino = " & stredListino
      End If
      If Not bopTipolist0 Then
        If bopTipolist1 Then strSQL &= " AND lc_tipo = 'C'"
        If bopTipolist2 Then strSQL &= " AND lc_tipo = 'F'"
        strSQL &= " AND lc_conto <> 0 AND lc_listino = 0"
        If Not NTSCInt(stredConto) = 0 Then
          strSQL &= " AND lc_conto = " & stredConto & _
                    " AND lc_coddest = " & lCoddest.ToString
        End If
      End If
      strSQL &= " AND lc_datagg = " & CDataSQL(stredDtinval) & _
        " AND lc_codvalu = " & stredCodvalu & _
        " AND lc_codlavo = " & stredCodlavo
      If bedCodtpro Then strSQL &= " AND lc_codtpro = " & stredCodtpro
      If bopUltimaFase = True Then strSQL &= " AND ar_ultfase = lc_fase"
      If bopSoloFase = True Then strSQL &= " AND lc_fase = " & stredFase

      lRecords = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ElaboraListiniVecchi(ByVal strDitta As String, ByVal strQuery As String, _
                                                   ByVal bopTipolist0 As Boolean, ByVal stredCodvalu As String, _
                                                   ByVal stredCodlavo As String, ByVal stredDtfival As String, _
                                                   ByVal bopTipolist1 As Boolean, ByVal stredConto As String, _
                                                   ByVal bedCodtpro As Boolean, ByVal stredCodtpro As String, _
                                                   ByVal bopUltimaFase As Boolean, ByVal bopSoloFase As Boolean, _
                                                   ByVal stredFase As String, ByVal stredListino As String, _
                                                   ByVal bopTipolist2 As Boolean, ByRef lRecords As Integer, _
                                                   ByVal lCoddest As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strSQLTmp As String = ""

    Try
      'aggiungo la where dei campi con OR e AND rimappati
      TraduciWhere(strQuery, strSQLTmp)
      '---------------------------------------------------------------------------------------
      '--- Se il tipo listino NON è 'Generico', aggiorna massivamente, il TimeStamp di ANAGRA
      '--- che ha riferimenti nei LISTINI selezionati per l'aggiornamento
      '--- (per ragioni di concorrenza, se eventualmente BS--CLIE fosse aperto)
      '---------------------------------------------------------------------------------------
      If bopTipolist0 = False Then
        strSQL = "UPDATE anagra" & _
          " SET an_ultagg = GETDATE()" & _
          " FROM listini, artico" & _
          " WHERE anagra.codditt = listini.codditt" & _
          " AND anagra.an_conto = listini.lc_conto" & _
          " AND artico.codditt = listini.codditt" & _
          " AND artico.ar_codart = listini.lc_codart" & _
          " AND anagra.codditt = " & CStrSQL(strDitta)
        strSQL &= " AND lc_codvalu = " & stredCodvalu & _
          " AND lc_codlavo = " & stredCodlavo & _
          " AND lc_conto <> 0 AND lc_listino = 0" & _
          " AND lc_datscad <= " & CDataSQL(stredDtfival) & _
          " AND lc_tipo = " & CStrSQL(NTSCStr(IIf(bopTipolist1 = True, "C", "F")))
        If NTSCInt(stredConto) <> 0 Then
          strSQL &= " AND lc_conto = " & stredConto & _
                    " AND lc_coddest = " & lCoddest.ToString
        End If
        If bedCodtpro = True Then strSQL &= " AND lc_codtpro = " & stredCodtpro
        If bopUltimaFase = True Then strSQL &= " AND ar_ultfase = lc_fase"
        If bopSoloFase = True Then strSQL &= " AND lc_fase = " & stredFase

        strSQL &= strSQLTmp

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '---------------------------------------------------------------------------------------
      strSQL = "DELETE listini FROM artico" & _
        " WHERE listini.codditt = artico.codditt" & _
        " AND listini.lc_codart = artico.ar_codart" & _
        " and listini.codditt = " & CStrSQL(strDitta)
      If bopTipolist0 Then
        strSQL &= " AND lc_tipo = ' ' AND lc_conto = 0"
        If Not NTSCInt(stredListino) = 0 Then strSQL &= " AND lc_listino = " & stredListino
      End If
      If Not bopTipolist0 Then
        If bopTipolist1 Then strSQL &= " AND lc_tipo = 'C'"
        If bopTipolist2 Then strSQL &= " AND lc_tipo = 'F'"
        strSQL &= " AND lc_conto <> 0 AND lc_listino = 0"
        If NTSCInt(stredConto) <> 0 Then
          strSQL &= " AND lc_conto = " & stredConto & _
                    " AND lc_coddest = " & lCoddest.ToString
        End If
      End If
      strSQL &= " AND lc_codvalu = " & stredCodvalu & _
        " AND lc_codlavo = " & stredCodlavo & _
        " AND lc_datscad <= " & CDataSQL(stredDtfival)
      If bedCodtpro Then strSQL &= " AND lc_codtpro = " & stredCodtpro
      If bopUltimaFase = True Then strSQL &= " AND ar_ultfase = lc_fase"
      If bopSoloFase = True Then strSQL &= " AND lc_fase = " & stredFase

      strSQL &= strSQLTmp

      lRecords = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetListini(ByVal strDitta As String, ByVal strQuery As String, _
                                         ByVal bopTipolist0 As Boolean, ByVal stredListino As String, _
                                         ByVal bopTipolist1 As Boolean, ByVal bopTipolist2 As Boolean, _
                                         ByVal stredConto As String, ByVal stredCodvalu As String, _
                                         ByVal stredCodlavo As String, ByVal stredDtinval As String, _
                                         ByVal bedCodtpro As Boolean, ByVal stredCodtpro As String, _
                                         ByVal bopUltimaFase As Boolean, ByVal bopSoloFase As Boolean, _
                                         ByVal stredFase As String, ByRef ds As DataSet) As Boolean
    Try
      'obsoleta
      Return GetListini(strDitta, strQuery, bopTipolist0, stredListino, bopTipolist1, bopTipolist2, stredConto, stredCodvalu, _
                        stredCodlavo, stredDtinval, bedCodtpro, stredCodtpro, bopUltimaFase, bopSoloFase, stredFase, ds, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetListini(ByVal strDitta As String, ByVal strQuery As String, _
                                         ByVal bopTipolist0 As Boolean, ByVal stredListino As String, _
                                         ByVal bopTipolist1 As Boolean, ByVal bopTipolist2 As Boolean, _
                                         ByVal stredConto As String, ByVal stredCodvalu As String, _
                                         ByVal stredCodlavo As String, ByVal stredDtinval As String, _
                                         ByVal bedCodtpro As Boolean, ByVal stredCodtpro As String, _
                                         ByVal bopUltimaFase As Boolean, ByVal bopSoloFase As Boolean, _
                                         ByVal stredFase As String, ByRef ds As DataSet, _
                                         ByVal lCoddest As Integer) As Boolean
    Dim strSQL As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strQuery, bopTipolist0, stredListino, bopTipolist1, bopTipolist2, stredConto, stredCodvalu, _
                        stredCodlavo, stredDtinval, bedCodtpro, stredCodtpro, bopUltimaFase, bopSoloFase, stredFase, ds, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(14), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "SELECT listini.lc_codart, listini.lc_codlavo, listini.lc_conto," & _
        " listini.lc_codvalu, listini.lc_codtpro, listini.lc_listino, listini.lc_datagg," & _
        " listini.lc_fase, listini.lc_datscad, listini.lc_coddest " & _
        " FROM listini INNER JOIN artico ON (listini.codditt = artico.codditt) AND (listini.lc_codart = artico.ar_codart)" & _
        " WHERE listini.codditt = " & CStrSQL(strDitta)
      If bopTipolist0 Then
        strSQL &= " AND lc_tipo = ' ' AND lc_conto = 0"
        If Not NTSCInt(stredListino) = 0 Then strSQL &= " AND lc_listino = " & stredListino
      End If
      If Not bopTipolist0 Then
        If bopTipolist1 Then strSQL &= " AND lc_tipo = 'C'"
        If bopTipolist2 Then strSQL &= " AND lc_tipo = 'F'"
        strSQL &= " AND lc_conto <> 0 AND lc_listino = 0"
        If NTSCInt(stredConto) <> 0 Then
          strSQL &= " AND lc_conto = " & stredConto & _
                    " AND lc_coddest = " & lCoddest.ToString
        End If
      End If
      strSQL &= " AND lc_codvalu = " & stredCodvalu & _
        " AND lc_codlavo = " & stredCodlavo & _
        " AND lc_datagg = " & CDataSQL(stredDtinval)
      If bedCodtpro Then strSQL &= " AND lc_codtpro = " & stredCodtpro
      If bopUltimaFase = True Then strSQL &= " AND ar_ultfase = lc_fase"
      If bopSoloFase = True Then strSQL &= " AND lc_fase = " & stredFase

      'aggiungo la where dei campi con OR e AND rimappati
      TraduciWhere(strQuery, strSQL)


      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetListiniVecchi(ByVal strDitta As String, ByVal strQuery As String, _
                                             ByVal bopTipolist0 As Boolean, ByVal stredCodvalu As String, _
                                             ByVal stredCodlavo As String, ByVal stredDtfival As String, _
                                             ByVal bopTipolist1 As Boolean, ByVal stredConto As String, _
                                             ByVal bedCodtpro As Boolean, ByVal stredCodtpro As String, _
                                             ByVal bopUltimaFase As Boolean, ByVal bopSoloFase As Boolean, _
                                             ByVal stredFase As String, ByVal stredListino As String, _
                                             ByVal bopTipolist2 As Boolean, ByRef dsListini As DataSet) As Boolean
    Try
      'obsoleta
      Return GetListiniVecchi(strDitta, strQuery, bopTipolist0, stredCodvalu, stredCodlavo, stredDtfival, _
                              bopTipolist1, stredConto, bedCodtpro, stredCodtpro, bopUltimaFase, bopSoloFase, _
                              stredFase, stredListino, bopTipolist2, dsListini, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetListiniVecchi(ByVal strDitta As String, ByVal strQuery As String, _
                                               ByVal bopTipolist0 As Boolean, ByVal stredCodvalu As String, _
                                               ByVal stredCodlavo As String, ByVal stredDtfival As String, _
                                               ByVal bopTipolist1 As Boolean, ByVal stredConto As String, _
                                               ByVal bedCodtpro As Boolean, ByVal stredCodtpro As String, _
                                               ByVal bopUltimaFase As Boolean, ByVal bopSoloFase As Boolean, _
                                               ByVal stredFase As String, ByVal stredListino As String, _
                                               ByVal bopTipolist2 As Boolean, ByRef dsListini As DataSet, _
                                               ByVal lCoddest As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strSQLTmp As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strQuery, bopTipolist0, stredCodvalu, stredCodlavo, stredDtfival, _
                              bopTipolist1, stredConto, bedCodtpro, stredCodtpro, bopUltimaFase, bopSoloFase, _
                              stredFase, stredListino, bopTipolist2, dsListini, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsListini = CType(oIn(14), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      'aggiungo la where dei campi con OR e AND rimappati
      TraduciWhere(strQuery, strSQLTmp)
      '---------------------------------------------------------------------------------------
      '--- Se il tipo listino NON è 'Generico', aggiorna massivamente, il TimeStamp di ANAGRA
      '--- che ha riferimenti nei LISTINI selezionati per l'aggiornamento
      '--- (per ragioni di concorrenza, se eventualmente BS--CLIE fosse aperto)
      '---------------------------------------------------------------------------------------
      strSQL = "SELECT listini.lc_codart, listini.lc_codlavo, listini.lc_conto," & _
        " listini.lc_codvalu, listini.lc_codtpro, listini.lc_listino, listini.lc_datagg," & _
        " listini.lc_fase, listini.lc_coddest" & _
        " FROM listini INNER JOIN artico ON (listini.codditt = artico.codditt) AND (listini.lc_codart = artico.ar_codart)" & _
        " WHERE listini.codditt = artico.codditt" & _
        " AND listini.lc_codart = artico.ar_codart" & _
        " and listini.codditt = " & CStrSQL(strDitta)
      If bopTipolist0 Then
        strSQL &= " AND lc_tipo = ' ' AND lc_conto = 0"
        If Not NTSCInt(stredListino) = 0 Then strSQL &= " AND lc_listino = " & stredListino
      End If
      If Not bopTipolist0 Then
        If bopTipolist1 Then strSQL &= " AND lc_tipo = 'C'"
        If bopTipolist2 Then strSQL &= " AND lc_tipo = 'F'"
        strSQL &= " AND lc_conto <> 0 AND lc_listino = 0"
        If NTSCInt(stredConto) <> 0 Then
          strSQL &= " AND lc_conto = " & stredConto & _
                    " AND lc_coddest = " & lCoddest.ToString
        End If
      End If
      strSQL &= " AND lc_codvalu = " & stredCodvalu & _
        " AND lc_codlavo = " & stredCodlavo & _
        " AND lc_datscad <= " & CDataSQL(stredDtfival)
      If bedCodtpro Then strSQL &= " AND lc_codtpro = " & stredCodtpro
      If bopUltimaFase = True Then strSQL &= " AND ar_ultfase = lc_fase"
      If bopSoloFase = True Then strSQL &= " AND lc_fase = " & stredFase

      strSQL &= strSQLTmp

      dsListini = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function UpdateTimeStampConto(ByVal strDitta As String, ByVal lConto As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      '--- Aggiorna il timestamp di anagra (an_ultagg) con la data di sistema
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE anagra" & _
        " SET an_ultagg = GETDATE()" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND an_conto = " & lConto
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function



  Public Overridable Function VerificaListiniSuccessivi(ByVal dtrListino As DataRow, ByVal dtNewDtScad As Date) As Boolean
    Dim strSQL As String = ""
    Try
      With dtrListino
        'Verifica se c'è un listino nelle stesse condizioni 
        ' per portare la data scadenza del listino precedente al giorno precedente l'inizio del listino successivo a quello da cancellare
        strSQL = "SELECT lc_datagg FROM listini" & _
                 " WHERE codditt = " & CStrSQL(!codditt) & _
                 "   AND lc_codart = " & CStrSQL(!lc_codart) & _
                 "   AND lc_codlavo = " & NTSCStr(!lc_codlavo) & _
                 "   AND lc_conto = " & NTSCStr(!lc_conto) & _
                 "   AND lc_coddest = " & NTSCStr(!lc_coddest) & _
                 "   AND lc_codvalu = " & NTSCStr(!lc_codvalu) & _
                 "   AND lc_codtpro = " & NTSCStr(!lc_codtpro) & _
                 "   AND lc_listino = " & NTSCStr(!lc_listino) & _
                 "   AND lc_fase = " & NTSCStr(!lc_fase) & _
                 "   AND lc_datagg > " & CDataSQL(NTSCDate(!lc_datscad)) & _
                 "   AND lc_datagg <= " & CDataSQL(dtNewDtScad) 
      End With

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function PortaAScadenzaListini(ByVal strDitta As String, ByVal dtrTmp As DataRow, ByVal dtScad As Date) As Boolean
    Dim strSQL As String = ""
    Try
      With dtrTmp
        strSQL = "UPDATE listini" & _
                 "   SET lc_datscad = " & CDataSQL(dtScad) & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 "   AND lc_progr = " & CStrSQL(!lc_progr)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End With
      '-------------------------------------------------------------------------------------
      '--- Se il listino da aggiornare è collegato ad un conto, aggiorna il TimeStamp
      '--- di ANAGRA per ragioni di concorrenza, se eventualmente BS--CLIE fosse aperto
      '-------------------------------------------------------------------------------------
      If NTSCInt(dtrTmp!lc_conto) <> 0 Then UpdateTimeStampConto(strDitta, NTSCInt(dtrTmp!lc_conto))
      '-------------------------------------------------------------------------------------

      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetListiniAgg(ByVal strDitta As String, ByVal strQuery As String, _
                                          ByVal bopTipolist0 As Boolean, ByVal stredListino As String, _
                                          ByVal bopTipolist1 As Boolean, ByVal bopTipolist2 As Boolean, _
                                          ByVal stredConto As String, ByVal stredCodvalu As String, _
                                          ByVal stredCodlavo As String, ByVal stredDtinval As String, _
                                          ByVal bedCodtpro As Boolean, ByVal stredCodtpro As String, _
                                          ByVal bopUltimaFase As Boolean, ByVal bopSoloFase As Boolean, _
                                          ByVal stredFase As String, ByRef ds As DataSet) As Boolean
    Try
      'obsoleta
      Return GetListiniAgg(strDitta, strQuery, bopTipolist0, stredListino, bopTipolist1, bopTipolist2, _
                           stredConto, stredCodvalu, stredCodlavo, stredDtinval, bedCodtpro, stredCodtpro, _
                           bopUltimaFase, bopSoloFase, stredFase, ds, 0)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetListiniAgg(ByVal strDitta As String, ByVal strQuery As String, _
                                            ByVal bopTipolist0 As Boolean, ByVal stredListino As String, _
                                            ByVal bopTipolist1 As Boolean, ByVal bopTipolist2 As Boolean, _
                                            ByVal stredConto As String, ByVal stredCodvalu As String, _
                                            ByVal stredCodlavo As String, ByVal stredDtinval As String, _
                                            ByVal bedCodtpro As Boolean, ByVal stredCodtpro As String, _
                                            ByVal bopUltimaFase As Boolean, ByVal bopSoloFase As Boolean, _
                                            ByVal stredFase As String, ByRef ds As DataSet, _
                                            ByVal lCoddest As Integer) As Boolean
    Dim strSQL As String = ""

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strQuery, bopTipolist0, stredListino, bopTipolist1, bopTipolist2, _
                           stredConto, stredCodvalu, stredCodlavo, stredDtinval, bedCodtpro, stredCodtpro, _
                           bopUltimaFase, bopSoloFase, stredFase, ds, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(14), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "SELECT listini.* " & _
               " FROM listini " & _
               "   INNER JOIN artico ON listini.codditt = artico.codditt AND listini.lc_codart = artico.ar_codart " & _
               " WHERE listini.codditt = " & CStrSQL(strDitta)
      If bopTipolist0 Then
        strSQL &= " AND lc_tipo = ' ' AND lc_conto = 0"
        If Not NTSCInt(stredListino) = 0 Then strSQL &= " AND lc_listino = " & stredListino
      End If
      If Not bopTipolist0 Then
        If bopTipolist1 Then strSQL &= " AND lc_tipo = 'C'"
        If bopTipolist2 Then strSQL &= " AND lc_tipo = 'F'"
        strSQL &= " AND lc_conto <> 0 AND lc_listino = 0"
        If NTSCInt(stredConto) <> 0 Then
          strSQL &= " AND lc_conto = " & stredConto & _
                    " AND lc_coddest = " & lCoddest.ToString
        End If
      End If
      strSQL &= " AND lc_codvalu = " & stredCodvalu & _
                " AND lc_codlavo = " & stredCodlavo & _
                " AND " & CDataSQL(Now) & " BETWEEN lc_datagg AND lc_datscad "
      If bedCodtpro Then strSQL &= " AND lc_codtpro = " & stredCodtpro
      If bopUltimaFase = True Then strSQL &= " AND ar_ultfase = lc_fase"
      If bopSoloFase = True Then strSQL &= " AND lc_fase = " & stredFase

      'aggiungo la where dei campi con OR e AND rimappati
      TraduciWhere(strQuery, strSQL)


      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
End Class
