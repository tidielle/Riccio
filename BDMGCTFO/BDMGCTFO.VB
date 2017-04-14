#Region "Imports"
Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO
#End Region

Public Class CLDMGCTFO
  Inherits CLD__BASE

  Public Overridable Function CaricaLabelTabling(ByVal strDitta As String, _
                                                 ByVal strNomeTabella As String, _
                                                 ByRef strLing() As String) As Boolean

    Dim strSQL As String = ""
    Dim dttTmp As DataTable
    Try

      strSQL = " SELECT tb_desling, tb_codling FROM " & strNomeTabella & " WHERE tb_codling = 1 or tb_codling = 2 or tb_codling = 3"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        For i As Integer = 0 To dttTmp.Rows.Count - 1
          strLing(NTSCInt(dttTmp.Rows(i)!tb_codling) - 1) = NTSCStr(dttTmp.Rows(i)!tb_desling)
        Next
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetTimeStampArtest(ByVal strDitta As String, ByVal strCodartf As String, _
    ByVal lForn As Integer, ByVal nCodmarc As Integer) As DateTime
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      GetTimeStampArtest = NTSCDate(IntSetDate("01/01/1900"))
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT ae_ultagg FROM artest" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ae_codartf = " & CStrSQL(strCodartf) & _
        " AND ae_forn = " & lForn & _
        " AND ae_codmarc = " & nCodmarc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then GetTimeStampArtest = NTSCDate(dttTmp.Rows(0)!ae_ultagg)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function LeggiLayout(ByVal strDitta As String, _
                                          ByVal strNomeTabella As String, _
                                          ByRef dsOut As System.Data.DataSet) As Boolean
    Dim strSQL As String = ""
    Try

      If dsOut.Tables(strNomeTabella) Is Nothing Then
        '------------------------------------------------
        'query base 
        strSQL = "SELECT TOP 1 ae_codartf, ae_forn, ae_codmarc FROM artest"
        dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strNomeTabella)
        dsOut.Tables(strNomeTabella).Clear()
      End If

      If dsOut.Tables(strNomeTabella & "Dettaglio") Is Nothing Then
        '------------------------------------------------
        'query base 
        strSQL = "SELECT TOP 1 artest.*, an_descr1, tb_desmarc, tb_desciva," & _
                 " tb_desgmer, tb_dessgme,tb_descsar, tb_descfam" & _
                 " FROM ((((((artest LEFT JOIN anagra" & _
                 " ON (artest.ae_forn = anagra.an_conto)" & _
                 " AND (artest.codditt = anagra.codditt)) LEFT JOIN tabmarc" & _
                 " ON (artest.ae_codmarc = tabmarc.tb_codmarc)" & _
                 " AND (artest.codditt = tabmarc.codditt)) LEFT JOIN tabgmer" & _
                 " ON (artest.ae_gruppo = tabgmer.tb_codgmer)" & _
                 " AND (artest.codditt = tabgmer.codditt)) LEFT JOIN tabsgme" & _
                 " ON (artest.ae_sotgru = tabsgme.tb_codsgme)" & _
                 " AND (artest.codditt = tabsgme.codditt)) LEFT JOIN tabcsar" & _
                 " ON (artest.ae_clascon = tabcsar.tb_codcsar)" & _
                 " AND (artest.codditt = tabcsar.codditt)) LEFT JOIN tabcfam" & _
                 " ON (artest.ae_famprod = tabcfam.tb_codcfam)" & _
                 " AND (artest.codditt = tabcfam.codditt)) LEFT JOIN tabciva" & _
                 " ON (artest.ae_codiva = tabciva.tb_codciva)"
        OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strNomeTabella & "Dettaglio", dsOut)
      End If
      dsOut.Tables(strNomeTabella & "Dettaglio").Clear()

      dsOut.AcceptChanges()

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function LeggiDettaglio(ByVal strDitta As String, _
                                             ByVal strNomeTabella As String, _
                                             ByRef dsOut As DataSet, _
                                             ByVal nPosition As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      '------------------------------------------------
      'query base 
      strSQL = "SELECT artest.*, an_descr1, tb_desmarc, tb_desciva," & _
               " tb_desgmer, tb_dessgme,tb_descsar, tb_descfam" & _
               " FROM ((((((artest LEFT JOIN anagra" & _
               " ON (artest.ae_forn = anagra.an_conto)" & _
               " AND (artest.codditt = anagra.codditt)) LEFT JOIN tabmarc" & _
               " ON (artest.ae_codmarc = tabmarc.tb_codmarc)" & _
               " AND (artest.codditt = tabmarc.codditt)) LEFT JOIN tabgmer" & _
               " ON (artest.ae_gruppo = tabgmer.tb_codgmer)" & _
               " AND (artest.codditt = tabgmer.codditt)) LEFT JOIN tabsgme" & _
               " ON (artest.ae_sotgru = tabsgme.tb_codsgme)" & _
               " AND (artest.codditt = tabsgme.codditt)) LEFT JOIN tabcsar" & _
               " ON (artest.ae_clascon = tabcsar.tb_codcsar)" & _
               " AND (artest.codditt = tabcsar.codditt)) LEFT JOIN tabcfam" & _
               " ON (artest.ae_famprod = tabcfam.tb_codcfam)" & _
               " AND (artest.codditt = tabcfam.codditt)) LEFT JOIN tabciva" & _
               " ON (artest.ae_codiva = tabciva.tb_codciva)" & _
               " WHERE artest.codditt = " & CStrSQL(strDitta) & _
               " AND artest.ae_forn = " & NTSCInt(dsOut.Tables(strNomeTabella).Rows(nPosition)!ae_forn) & _
               " AND artest.ae_codmarc = " & NTSCInt(dsOut.Tables(strNomeTabella).Rows(nPosition)!ae_codmarc) & _
               " AND artest.ae_codartf = " & CStrSQL(dsOut.Tables(strNomeTabella).Rows(nPosition)!ae_codartf)

      If Not dsOut.Tables(strNomeTabella & "Dettaglio") Is Nothing Then
        dsOut.Tables.Remove(strNomeTabella & "Dettaglio")
        dsOut.AcceptChanges()
      End If

      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strNomeTabella & "Dettaglio", dsOut)

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function VerificaArticolo(ByVal strDitta As String, _
                                               ByVal strNomeTabella As String, _
                                               ByRef dttTmp As DataTable, _
                                               ByVal strForn As String, _
                                               ByVal strCodartf As String, _
                                               ByVal strCodmarc As String) As Boolean
    Dim strSQL As String = ""
    Try

      strSQL = "SELECT ae_codartf, ae_forn, ae_codmarc FROM " & strNomeTabella & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND ae_forn = " & NTSCInt(strForn) & _
               " AND ae_codmarc = " & NTSCInt(strCodmarc) & _
               " AND ae_codartf = " & CStrSQL(strCodartf)

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strNomeTabella).Tables(strNomeTabella).Copy

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overrides Function ScriviTabellaSemplice(ByVal strDitta As String, ByVal strNomeTabella As String, ByRef dttTable As DataTable, _
                                      ByVal strActLogProg As String, ByVal strActLogNomOggLog As String, _
                                      ByVal strActLogDesLog As String) As Boolean
    Dim dtrChange() As DataRow = Nothing
    Dim strSQL As String = ""
    Dim strSQL1 As String = ""
    Dim strSQLVal As String = ""

    Dim i As Integer
    Dim e As Integer

    Dim strCampoChiave() As String
    Dim strTipoCampoChiave() As String
    Dim strPrefissoCampo As String
    Dim bInDelete As Boolean = False

    Try

      '-------------------------
      'dati relativi alla tabella 

      strTipoCampoChiave = "S;N;N".Split(";"c)
      strCampoChiave = "ae_codartf;ae_forn;ae_codmarc".Split(";"c)
      strPrefissoCampo = "ae_"


      '-----------------------------
      'prima le delete
      bInDelete = True
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Deleted)
      For i = 0 To dtrChange.Length - 1

        strSQL = "DELETE FROM " & strNomeTabella & _
                 " WHERE " & strNomeTabella & ".codditt = " & CStrSQL(strDitta)
        For e = 0 To strCampoChiave.Length - 1
          strSQL &= " AND " & strCampoChiave(e) & " = "
          Select Case strTipoCampoChiave(e)
            Case "N" : strSQL &= CDblSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString())
            Case "S" : strSQL &= CStrSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString())
          End Select

        Next
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        dtrChange(i).AcceptChanges()
      Next
      bInDelete = False

      '-----------------------------
      'adesso le insert
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Added)
      strSQL = "INSERT INTO " & strNomeTabella & " " & GetQueryInsertField(dttTable, strPrefissoCampo)
      For i = 0 To dtrChange.Length - 1
        strSQLVal = GetQueryInsertValue(dttTable, dtrChange(i), strPrefissoCampo)
        Me.Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI)
        dtrChange(i).AcceptChanges()
        strSQL1 = "UPDATE " & strNomeTabella & _
          " SET ae_ultagg = " & CDataOraSQL(NTSCDate(DateTime.Now.ToString)) & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ae_codartf = " & CStrSQL(dtrChange(i)!ae_codartf) & _
          " AND ae_forn = " & NTSCInt(dtrChange(i)!ae_forn) & _
          " AND ae_codmarc = " & NTSCInt(dtrChange(i)!ae_codmarc)
        Execute(strSQL1, CLE__APP.DBTIPO.DBAZI)
        dtrChange(i).AcceptChanges()
      Next

      '-----------------------------
      'infine le update
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrChange.Length - 1
        strSQL = "UPDATE " & strNomeTabella & " SET " & GetQueryUpdate(dttTable, dtrChange(i), strPrefissoCampo) & _
                 " WHERE " & strNomeTabella & ".codditt = " & CStrSQL(strDitta)
        For e = 0 To strCampoChiave.Length - 1
          strSQL &= " AND " & strCampoChiave(e) & " = "
          Select Case strTipoCampoChiave(e)
            Case "N" : strSQL &= CDblSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString())
            Case "S" : strSQL &= CStrSQL(dtrChange(i)(strCampoChiave(e), DataRowVersion.Original).ToString())
          End Select
        Next
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
        If bInDelete Then dtrChange(i).RejectChanges()
      Catch
      End Try
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function RitornaArtest(ByVal strDitta As String, ByVal strCodartf As String, _
    ByVal lForn As Integer, ByVal nCodmarc As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM artest" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ae_codartf = " & CStrSQL(strCodartf) & _
        " AND ae_forn = " & lForn & _
        " AND ae_codmarc = " & nCodmarc
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttOut.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

End Class
