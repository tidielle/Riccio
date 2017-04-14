#Region "Import"

Imports System.Data
Imports NTSInformatica.CLN__STD

#End Region

Public Class CLDMGPROM
  Inherits CLD__BASE

  Public Overridable Function CancellaAllCodtproInArtprom(ByVal strDitta As String, ByVal nCodTpro As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM artprom" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND apr_codtpro = " & NTSCInt(nCodTpro)
      Me.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'Non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ValidaCodtproInArtprom(ByVal strDitta As String, ByVal nCodTpro As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 apr_codtpro FROM artprom" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND apr_codtpro = " & nCodTpro

      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABTMP").Tables("TABTMP").Rows.Count > 0 Then
        Return True
      End If
      'return false 'default
    Catch ex As Exception
      '--------------------------------------------------------------
      'Non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ValidaCodartInArtprom(ByVal strDitta As String, ByVal nCodTpro As Integer, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 apr_codtpro FROM artprom" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND apr_codtpro = " & nCodTpro & _
               " AND apr_codart = " & CStrSQL(strCodart)

      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABTMP").Tables("TABTMP").Rows.Count > 0 Then
        Return True
      End If
      'return false 'default
    Catch ex As Exception
      '--------------------------------------------------------------
      'Non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ScriviTabellaTabtpro(ByVal strDitta As String, ByVal strNomeTabella As String, ByRef dttTable As System.Data.DataTable) As Boolean
    Dim dtrChange() As DataRow = Nothing
    Dim bInDelete As Boolean = False
    Dim i As Integer
    Try

      '-----------------------------
      'prima le delete
      bInDelete = True
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Deleted)
      For i = 0 To dtrChange.Length - 1

        CancellaAllCodtproInArtprom(strDitta, NTSCInt(dtrChange(i).Item("tb_codtpro", DataRowVersion.Original)))

      Next
      bInDelete = False

      Return MyBase.ScriviTabellaSemplice(strDitta, strNomeTabella, dttTable, "", "", "")
    Catch ex As Exception
      Try
        '--------------------------------------------------------------
        'Se in fase di cancellaz di una riga si verifica un errore
        'ripristino la riga corrente
        '--------------------------------------------------------------
        If bInDelete Then dtrChange(i).RejectChanges()
      Catch
      End Try
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function ScriviTabellaArtprom(ByVal strDittaCorrente As String, ByVal strNomeTabella As String, ByRef dttTable As System.Data.DataTable) As Boolean

    Dim dtrChange() As DataRow = Nothing
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer

    Dim strPrefissoCampo As String = "apr_"
    Dim bInDelete As Boolean = False

    Try

      '-----------------------------
      'prima le delete
      bInDelete = True
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Deleted)
      For i = 0 To dtrChange.Length - 1

        strSQL = "DELETE FROM " + strNomeTabella + " WHERE codditt = '" & NTSCStr(strDittaCorrente) & "'" & _
               " AND apr_codart = " & CStrSQL(dtrChange(i).Item("apr_codart", DataRowVersion.Original)) & _
               " AND apr_codtpro = " & CStrSQL(dtrChange(i).Item("apr_codtpro", DataRowVersion.Original))


        Me.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        dtrChange(i).AcceptChanges()
      Next
      bInDelete = False

      '-----------------------------
      'adesso le insert
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Added)
      strSQL = "INSERT INTO " + strNomeTabella + " " & GetQueryInsertField(dttTable, strPrefissoCampo)
      For i = 0 To dtrChange.Length - 1
        strSQLVal = GetQueryInsertValue(dttTable, dtrChange(i), strPrefissoCampo)
        Me.Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI)
        dtrChange(i).AcceptChanges()
      Next

      '-----------------------------
      'infine le update
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrChange.Length - 1

        strSQL = "UPDATE " + strNomeTabella + " SET " & GetQueryUpdate(dttTable, dtrChange(i), strPrefissoCampo)
        strSQL += " WHERE " + strNomeTabella + ".codditt = " & CStrSQL(strDittaCorrente) & _
                  " AND " & strNomeTabella + ".apr_codart = " & CStrSQL(dtrChange(i).Item("apr_codart")) & _
                  " AND " & strNomeTabella + ".apr_codtpro = " & CStrSQL(dtrChange(i).Item("apr_codtpro"))
        Me.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        dtrChange(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      Try
        '--------------------------------------------------------------
        'Se in fase di cancellaz di una riga si verifica un errore
        'ripristino la riga corrente
        '--------------------------------------------------------------
        If bInDelete Then dtrChange(i).RejectChanges()
      Catch
      End Try
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function LeggiTabellaArtprom(ByVal strDitta As String, ByVal nCodProm As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artprom.*, artico.ar_descr as xx_desart, tabdpro.tb_desdpro as xx_desdpro " & _
               " FROM artprom " & _
               "  INNER JOIN artico ON artprom.apr_codart = artico.ar_codart AND artico.codditt = artprom.codditt " & _
               "  LEFT JOIN tabdpro ON artprom.apr_coddpro = tabdpro.tb_coddpro AND artprom.codditt = tabdpro.codditt " & _
               " WHERE artprom.codditt = " & CStrSQL(strDitta) & _
               "   AND artprom.apr_codtpro = " & nCodProm & _
               " ORDER BY artprom.apr_codart"

      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "artprom", ds)

      With ds.Tables("artprom").Columns
        .Add(New DataColumn("xx_checked", GetType(String)))
      End With
      If ds.Tables("artprom").Rows.Count > 0 Then
        For i As Integer = 0 To ds.Tables("artprom").Rows.Count - 1
          ds.Tables("artprom").Rows(i)!xx_checked = "N"
        Next
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'Non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggiungiArticoliDettaglioPromozione(ByVal strDittaCorrente As String, ByVal strArticoliSelezionati As String, ByVal nCodProm As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO artprom (codditt, apr_codart, apr_codtpro, apr_coddpro," & _
                                         " apr_note, apr_ultagg, apr_opnome)" & _
      " SELECT " & CStrSQL(strDittaCorrente) & ", ar_codart, " & _
              nCodProm & ", 0, NULL, " & _
              CDataOraSQL(Date.Now) & ", " & _
              CStrSQL(oApp.User.Nome) & _
      " FROM artico" & _
      " WHERE codditt = " & CStrSQL(strDittaCorrente) & _
      " AND " & Replace(strArticoliSelezionati, "§", " AND ") & _
      " AND ar_codart NOT IN (SELECT apr_codart FROM artprom" & _
                                              " WHERE codditt = " & CStrSQL(strDittaCorrente) & _
                                              " AND apr_codtpro = " & nCodProm & ")"
      Me.Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      If Not ds.Tables("artprom") Is Nothing Then ds.Tables.Remove("artprom")

      Return LeggiTabellaArtprom(strDittaCorrente, nCodProm, ds)

    Catch ex As Exception
      '--------------------------------------------------------------
      'Non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
End Class
