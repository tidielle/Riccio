Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica
Imports System.IO

Public Class CLD__VLOG
  Inherits CLD__BASE

  Public Overridable Function GetDataApri(ByVal strSvloSQLQuery As String, ByVal nOrdinamento As Integer, ByVal nSvloOrigine As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      strSQL = strSvloSQLQuery
      '-----------------------------------------------------------------------------------------
      '--- Dedtetrmina l'ordinamento a seconda della voce di menu relativa selezionata
      '-----------------------------------------------------------------------------------------
      If nOrdinamento = 0 Then strSQL = strSQL & " ORDER BY al_data DESC"
      If nOrdinamento = 1 Then
        If nSvloOrigine = 0 Then
          strSQL = strSQL & " ORDER BY Substring(al_desogglog, 0, 255)"
        Else
          strSQL = strSQL & " ORDER BY Substring(al_desogglog, 0, 255)"
        End If
      End If
      If nOrdinamento = 2 Then strSQL = strSQL & " ORDER BY al_opnome"
      '-----------------------------------------------------------------------------------------
      Select Case nSvloOrigine
        Case 0
          ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ACTLOG")
        Case 1
          ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "ACTLOG")
      End Select

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function FileCancella(ByVal strSvloCodditt As String, ByVal strCodici As String, ByVal bSvloSelCodditt As Boolean, ByVal nSvloOrigine As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT al_progr FROM actlog" & _
        " WHERE al_progr IN (" & strCodici
      strSQL = Mid(strSQL, 1, (strSQL.Length - 2)) & ")"
      If bSvloSelCodditt = True Then strSQL += " AND codditt = " & CStrSQL(strSvloCodditt)
      Select Case nSvloOrigine
        Case 0 : dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        Case 1 : dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      End Select
      If dttTmp.Rows.Count > 0 Then
        For i As Integer = 0 To (dttTmp.Rows.Count - 1)
          strSQL = "DELETE FROM actlog" & _
            " WHERE al_progrpadr = " & NTSCInt(dttTmp.Rows(i)!al_progr) & _
            IIf(bSvloSelCodditt = True, " AND codditt = " & CStrSQL(strSvloCodditt), "").ToString
          Select Case nSvloOrigine
            Case 0 : Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            Case 1 : Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
          End Select
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM actlog" & _
        " WHERE al_progr IN (" & strCodici
      strSQL = Mid(strSQL, 1, (strSQL.Length - 2)) & ")"
      If bSvloSelCodditt = True Then strSQL += " AND codditt = " & CStrSQL(strSvloCodditt)
      Select Case nSvloOrigine
        Case 0 : Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        Case 1 : Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function StampaSuFile(ByVal strDittaCorrente As String, ByVal strAl_progr As String, ByVal nSvloOrigine As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM actlog" & _
        " WHERE codditt = " & CStrSQL(strDittaCorrente) & _
        " AND (al_progr = " & NTSCInt(strAl_progr) & " OR al_progrpadr = " & NTSCInt(strAl_progr) & ")" & _
        " ORDER BY al_progr"

      Select Case nSvloOrigine
        Case 0
          ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ACTLOG")
        Case 1
          ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "ACTLOG")
      End Select

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function InserisciAttivitaMancanti() As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT cb_nomtab FROM structlog" & _
        " WHERE LOWER(cb_desogglog) = 'aggiornamento struttura dababase'" & _
        " AND UPPER(cb_nomogglog) = 'BUSADMINTOOLS'" & _
        " AND cb_tabprim = 'S'" & _
        " AND cb_tipoogglog = 'E'"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      If dttTmp.Rows.Count = 0 Then
        strSQL = "INSERT INTO structlog (cb_desogglog, cb_nomogglog, cb_nomtab, cb_tabprim, cb_tipoogglog," & _
          " cb_descaselect, cb_sqlkey, cb_sqlselect)" & _
          " VALUES ('Aggiornamento struttura Dababase', 'BUSADMINTOOLS', ' ', 'S', 'E', NULL, NULL, NULL)"
        Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

#Region "funzioni specifiche per BN__VLOG.BN__SVLO"

  Public Overridable Function GetStructLog(ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT cb_desogglog FROM STRUCTLOG" & _
        " WHERE cb_tabprim = 'S'" & _
        " ORDER BY cb_desogglog"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "STRUCTLOG")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function CheckSelectionStructLog(ByVal strDesogglog As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT cb_nomogglog FROM STRUCTLOG" & _
        " WHERE cb_desogglog = " & CStrSQL(strDesogglog)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "STRUCTLOG")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CheckSelectionActLog(ByVal stredOpnome As String, ByVal strNomogglog As String, ByVal stredCodditt As String, _
  ByVal bckSelDesogglog As Boolean, ByVal bckSelCodditt As Boolean, ByVal stredDesogglog As String, ByVal bopTipoop1 As Boolean, ByVal bopTipoop2 As Boolean, _
  ByVal bopTipoogglog0 As Boolean, ByVal bopTipoogglog1 As Boolean, ByVal stredDatini As String, ByVal stredDatfin As String, ByVal bConn As Boolean, _
  ByVal bopOrigine0 As Boolean, ByRef ds As DataSet) As Boolean

    Dim strSQL As String = ""
    Dim strDesogglog As String
    Dim strDatfinTmp As String
    Try
      strSQL = "SELECT TOP 1 al_progr FROM actlog" & _
        " WHERE al_tabprim = 'S'"
      If Trim(stredOpnome) <> "" Then
        strSQL = strSQL & " AND al_opnome = " & CStrSQL(stredOpnome)
      End If
      If bckSelDesogglog = True Then
        If strNomogglog.IndexOf("*") > -1 Then
          strSQL = strSQL & " AND al_nomogglog like " & CStrSQL(strNomogglog.Replace("*", "%"))
        Else
          strSQL = strSQL & " AND al_nomogglog = " & CStrSQL(strNomogglog)
        End If
      End If
      If bckSelCodditt = True Then
        If stredCodditt <> "" Then
          strSQL = strSQL & " AND codditt = " & CStrSQL(stredCodditt)
        End If
      End If
      If Trim(stredDesogglog) <> "" Then
        strDesogglog = Trim(stredDesogglog)
        If strDesogglog.Substring(0, 1) = "*" Then strDesogglog = strDesogglog.Substring(1)
        If strDesogglog.Substring(strDesogglog.Length - 1, 1) = "*" Then
          strDesogglog = strDesogglog.Substring(0, strDesogglog.Length - 1)
        End If
        If strDesogglog.Substring(0, 1) <> "%" Then strDesogglog = "%" & strDesogglog
        If strDesogglog.Substring(Len(strDesogglog) - 1, 1) <> "%" Then strDesogglog = strDesogglog & "%"
        strSQL = strSQL & " AND al_desogglog LIKE " & CStrSQL(strDesogglog)
      End If
      If bopTipoop1 = True Then strSQL = strSQL & " AND al_tipoop = 'M'"
      If bopTipoop2 = True Then strSQL = strSQL & " AND al_tipoop = 'A'"
      If bopTipoogglog0 = True Then strSQL = strSQL & " AND al_tipoogglog = 'D'"
      If bopTipoogglog1 = True Then strSQL = strSQL & " AND al_tipoogglog = 'E'"
      If (NTSCDate(stredDatini) <> NTSCDate("01/01/1900")) Or (NTSCDate(stredDatfin) <> NTSCDate("31/12/2099")) Then
        If NTSCDate(stredDatfin) <> NTSCDate("31/12/2099") Then
          strDatfinTmp = (NTSCDate(stredDatfin).AddDays(1)).ToShortDateString
        Else
          strDatfinTmp = stredDatfin
        End If
        strSQL = strSQL & " AND al_data BETWEEN " & CDataSQL(stredDatini) & " AND " & CDataSQL(strDatfinTmp)
      End If
      If bopOrigine0 = True Then
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ACTLOG")
      Else
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "ACTLOG")
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function CheckCoddittSelect(ByVal stredCodditt As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_azrags1 FROM tabanaz" & _
        " WHERE codditt = " & CStrSQL(stredCodditt)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function CheckOpnomeSelect(ByVal stredOpnome As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT OpNome FROM OPERAT" & _
        " WHERE OpNome = " & CStrSQL(stredOpnome)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "OPERAT")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function CostruisciWhereStructLog(ByVal strDesogglog As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT cb_nomogglog FROM STRUCTLOG" & _
        " WHERE cb_desogglog = " & CStrSQL(strDesogglog)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "STRUCTLOG")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

#End Region

End Class
