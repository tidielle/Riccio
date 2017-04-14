Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica


Public Class CLD__NUMG
  Inherits CLD__BASE


  Public Overridable Function LeggiTabella(ByVal strDitta As String, ByVal strFiltroTipo As String, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""

    Try
      Select Case strFiltroTipo
        Case "SS"
          strSQL = "SELECT tabnumg.*, tabpcon.tb_despcon AS xx_numserieg, tabmast.tb_desmast AS xx_numcodlg  FROM tabnumg" & _
         " LEFT JOIN tabpcon ON tabnumg.tb_numserieg = tabpcon.tb_codpcon" & _
         " LEFT JOIN tabmast ON tabnumg.tb_numcodlg = tabmast.tb_codmast AND tabpcon.tb_codpcon = tabmast.tb_codpcon"
          strSQL += " WHERE tb_numtipog = '" & strFiltroTipo & "'"
          strSQL += " ORDER BY tb_numtipog, tb_numserieg, tb_numcodlg"
        Case "SX"
          strSQL = "SELECT tabnumg.*, tabpcca.tb_despcca AS xx_numserieg, tabmac2.tb_desmac2 AS xx_numcodlg  FROM tabnumg" & _
         " LEFT JOIN tabpcca ON tabnumg.tb_numserieg = tabpcca.tb_codpcca " & _
         " LEFT JOIN tabmac2 ON tabnumg.tb_numcodlg = tabmac2.tb_codmac2 AND tabpcca.tb_codpcca = tabmac2.tb_codpcca"
          strSQL += " WHERE tb_numtipog = '" & strFiltroTipo & "'"
          strSQL += " ORDER BY tb_numtipog, tb_numserieg, tb_numcodlg"
        Case "AG"
          strSQL = "SELECT tabnumg.* FROM tabnumg"
          strSQL += " WHERE tb_numtipog = '" & strFiltroTipo & "' AND tb_numserieg = ' ' AND tb_numcodlg = 0"
          strSQL += " ORDER BY tb_numtipog, tb_numserieg, tb_numcodlg"
        Case Else
          Throw (New NTSException(oApp.Tr("", 128302564510781250, "ERRORE filtro non riconosciuto: |" & strFiltroTipo & "|")))

      End Select

      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABNUMG")
      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Sub CreaPrimoAG()
    Dim strSQL As String = ""

    Try
      strSQL = "INSERT INTO TABNUMG (tb_numtipog, tb_numserieg, tb_numcodlg, tb_numprogg)" & _
               " VALUES ('AG', ' ', 0, 0)"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function ScriviTabella(ByVal strDitta As String, ByRef dttTable As DataTable, _
                                        ByVal strActLogProg As String, ByVal strActLogNomOggLog As String, _
                                        ByVal strActLogDesLog As String) As Boolean
    Dim dtrChange() As DataRow = Nothing
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer

    Dim strActLogSqlWhere As String = ""

    Try

      '-----------------------------
      'prima le delete
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Deleted)
      For i = 0 To dtrChange.Length - 1

        '---------
        'gestione di actlog
        If strActLogProg <> "" Then
          strActLogSqlWhere = CStrSQL(dtrChange(i)("tb_numtipog", DataRowVersion.Original).ToString())
          strActLogSqlWhere += "," & CStrSQL(dtrChange(i)("tb_numserieg", DataRowVersion.Original).ToString())
          strActLogSqlWhere += "," & CDblSQL(dtrChange(i)("tb_numcodlg", DataRowVersion.Original).ToString())
          ScriviActLog(strDitta, strActLogProg, strActLogNomOggLog, "tabnumg", strActLogSqlWhere, "A", "D", _
                      "DELETE: " & strActLogDesLog & " (" & strActLogSqlWhere.Substring(strActLogSqlWhere.IndexOf(",") + 1).ToString & ")", False)
        End If
        '---------

        strSQL = "DELETE FROM tabnumg WHERE "
        strSQL = strSQL + " tb_numtipog = " & CStrSQL(dtrChange(i)("tb_numtipog", DataRowVersion.Original).ToString()) & " AND " & _
        " tb_numserieg = " & CStrSQL(dtrChange(i)("tb_numserieg", DataRowVersion.Original).ToString()) & " AND " & _
        " tb_numcodlg = " & CDblSQL(dtrChange(i)("tb_numcodlg", DataRowVersion.Original).ToString())
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        dtrChange(i).AcceptChanges()
      Next

      '-----------------------------
      'adesso le insert
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Added)
      strSQL = "INSERT INTO tabnumg (tb_numtipog, tb_numserieg, tb_numcodlg, tb_numprogg, tb_note)"
      For i = 0 To dtrChange.Length - 1
        strSQLVal = "(" & CStrSQL(dtrChange(i)("tb_numtipog").ToString()) & ", " & _
                    CStrSQL(dtrChange(i)("tb_numserieg").ToString()) & ", " & _
                    CDblSQL(dtrChange(i)("tb_numcodlg").ToString()) & ", " & _
                    CDblSQL(dtrChange(i)("tb_numprogg").ToString()) & ", " & _
                    CStrSQL(dtrChange(i)("tb_note")) & ")"
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI)
        dtrChange(i).AcceptChanges()
      Next

      '-----------------------------
      'infine le update
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrChange.Length - 1

        '---------
        'gestione di actlog
        If strActLogProg <> "" Then
          strActLogSqlWhere = CStrSQL(dtrChange(i)("tb_numtipog", DataRowVersion.Original).ToString())
          strActLogSqlWhere += "," & CStrSQL(dtrChange(i)("tb_numserieg", DataRowVersion.Original).ToString())
          strActLogSqlWhere += "," & CDblSQL(dtrChange(i)("tb_numcodlg", DataRowVersion.Original).ToString())
          ScriviActLog(strDitta, strActLogProg, strActLogNomOggLog, "tabnumg", strActLogSqlWhere, "M", "D", _
                      "UPDATE: " & strActLogDesLog & " (" & strActLogSqlWhere.Substring(strActLogSqlWhere.IndexOf(",") + 1).ToString & ")", False)
        End If
        '---------

        strSQL = "UPDATE tabnumg SET " & _
                 " tb_numtipog = " & CStrSQL(dtrChange(i)("tb_numtipog").ToString()) & ", " & _
                 " tb_numserieg = " & CStrSQL(dtrChange(i)("tb_numserieg").ToString()) & ", " & _
                 " tb_numcodlg = " & CDblSQL(dtrChange(i)("tb_numcodlg").ToString()) & ", " & _
                 " tb_numprogg = " & CDblSQL(dtrChange(i)("tb_numprogg").ToString()) & ", " & _
                 " tb_note = " & CStrSQL(dtrChange(i)("tb_note"))
        strSQL += " WHERE "
        strSQL += " tb_numtipog = " & CStrSQL(dtrChange(i)("tb_numtipog", DataRowVersion.Original).ToString()) & " AND " & _
        " tb_numserieg = " & CStrSQL(dtrChange(i)("tb_numserieg", DataRowVersion.Original).ToString()) & " AND " & _
        " tb_numcodlg = " & CDblSQL(dtrChange(i)("tb_numcodlg", DataRowVersion.Original).ToString())

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        dtrChange(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Try
        'se in fase di cancellaz di una riga si verifica un errore ripristino la riga corrente
        dtrChange(i).RejectChanges()
      Catch
      End Try
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

End Class
