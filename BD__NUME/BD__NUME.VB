Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica


Public Class CLD__NUME
  Inherits CLD__BASE

  Public Overridable Function LeggiTabella(ByVal strDitta As String, ByVal strFiltroTipo As String, ByRef ds As DataSet, ByVal strCodpcon As String) As Boolean
    Dim strSQL As String = ""

    Try
      Select Case strFiltroTipo
        Case "CC", "FF", "SS"
          strSQL = "SELECT tabnuma.*, tabnuma.tb_numcodl AS xx_mastro, tabmast.tb_desmast AS xx_numcodl FROM tabnuma"
          strSQL += " LEFT JOIN tabmast ON tabnuma.tb_numcodl = tabmast.tb_codmast AND tabmast.tb_codpcon = " & CStrSQL(strCodpcon)
          strSQL += " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = '" & strFiltroTipo & "'"
          strSQL += " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"

        Case "SA"
          strSQL = "SELECT tabnuma.*, tabnuma.tb_numcodl AS xx_mastro, tabmaca.tb_desmaca AS xx_numcodl FROM tabnuma"
          strSQL += " LEFT JOIN tabmaca ON tabnuma.tb_numcodl = tabmaca.tb_codmaca AND tabnuma.codditt = tabmaca.codditt"
          strSQL += " WHERE tabnuma.codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = '" & strFiltroTipo & "'"
          strSQL += " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"

        Case "CQ"
          strSQL = "SELECT tabnuma.*, tabnuma.tb_numserie AS xx_codart, artico.ar_descr AS xx_numcodl FROM tabnuma"
          strSQL += " LEFT JOIN artico ON tabnuma.codditt = artico.codditt AND tabnuma.tb_numserie = artico.ar_codart"
          strSQL += " WHERE tabnuma.codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = '" & strFiltroTipo & "'"
          strSQL += " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"

        Case "CP"
          strSQL = "SELECT tabnuma.*, tabnuma.tb_numserie AS xx_codfam, tabcfam.tb_descfam AS xx_numcodl FROM tabnuma"
          strSQL += " LEFT JOIN tabcfam ON tabnuma.codditt = tabcfam.codditt AND tabnuma.tb_numserie = tabcfam.tb_codcfam"
          strSQL += " WHERE tabnuma.codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = '" & strFiltroTipo & "'"
          strSQL += " AND tb_numserie <> ' '"
          strSQL += " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"

        Case "P"
          strSQL = "SELECT tabnuma.*, LEFT(tb_numserie,1) AS xx_tipo, RIGHT('000' + tb_numserie, 3) AS xx_num,"
          strSQL += " SUBSTRING(tb_numserie, 2, LEN(tb_numserie) - 4) AS xx_numserie2  FROM tabnuma"
          strSQL += " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = '" & strFiltroTipo & "'"
          strSQL += " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"

        Case "R", "O", "Q", "A", "L", "B", "M", "C", "Z", "Y", "H", "W", "X", "T", "U", "G", "F", "S", "V", "CL", "NC", "$", "VV", "#", "P1", "P2", "P3", "!", "PD"
          strSQL = "SELECT tabnuma.* FROM tabnuma"
          strSQL += " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = '" & strFiltroTipo & "'"
          Select Case strFiltroTipo
            Case "$", "A", "B", "C", "F", "G", "H", "L", "M", "O", "Q", "R", "S", "T", "U", "VV", "W", "Y", "Y", "Z", "#"
              strSQL += " ORDER BY tb_numtipo, tb_numcodl DESC, tb_numserie DESC"
            Case "P1", "P2", "P3", "!", "PD"
              strSQL += " ORDER BY codditt, tb_numtipo, tb_numserie, tb_numcodl"
            Case Else
              strSQL += " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"
          End Select

        Case "SM"
          strSQL = "SELECT tabnuma.*, tabnuma.tb_numserie AS xx_numserie1 FROM tabnuma"
          strSQL += " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = '" & strFiltroTipo & "'"
          strSQL += " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"

        Case "DA", "EA"
          strSQL = "SELECT tabnuma.*, tabnuma.tb_numserie AS xx_numserie FROM tabnuma"
          strSQL += " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = '" & strFiltroTipo & "'"
          strSQL += " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"

        Case "DI"
          strSQL = "SELECT tabnuma.*, tabnuma.tb_numserie AS xx_numserie3 FROM tabnuma"
          strSQL += " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = '" & strFiltroTipo & "'"
          strSQL += " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"

        Case "RO"
          strSQL = "SELECT tabnuma.* FROM tabnuma"
          strSQL += " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_numtipo IN ('RO', 'RH', 'SO', 'SH')"
          strSQL += " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"

        Case "AA"
          strSQL = "SELECT tabnuma.*, tabnuma.tb_numserie AS xx_codroot FROM tabnuma"
          strSQL += " WHERE tabnuma.codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = '" & strFiltroTipo & "'"
          strSQL += " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"

          'varie e varie BS--NUMX NTS
        Case "PA", "OP", "OL", "OO", "CO", "LO", "BC", "BG", "RT", "CE", "IC", "EL", "MF", "FL", "MT", "CP2", "DT", "OG", "CQ2", "SP", "AP", "TP", "MA", "IM", "F4", "V1", "PK", "C8", "C7", "C9", "C6", "C3", "C4", "C5", "C2", "C1", "GP", "PR", "UC", "ID"
          strSQL = "SELECT tabnuma.* FROM tabnuma"
          strSQL += " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = '" & RimappaNomiUguali(strFiltroTipo) & "'"
          strSQL += " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"

          'NTS
        Case "AD", "AF"
          strSQL = "SELECT tabnuma.* FROM tabnuma"
          strSQL += " WHERE codditt = " & CStrSQL(strDitta) & " AND tb_numtipo = '" & strFiltroTipo & "'"
          strSQL += " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"

        Case Else
          Throw (New NTSException(oApp.Tr("", 128302564510781250, "ERRORE filtro non riconosciuto: |" & strFiltroTipo & "|")))

      End Select

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABNUMA")
      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function FileElaboraGetTabNuma(ByVal strDitta As String, ByVal strAnnoIni As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_numtipo, tb_numserie, tb_numcodl FROM tabnuma" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo IN ('A', 'B', 'C', 'F', 'G', 'H', 'L', 'M', 'O', 'P' , 'Q', 'R', 'S', 'T'," & _
                             " 'U', 'V', 'W', 'X', 'Y', 'Z', 'RH', 'RO', 'SH', 'SO', 'VV', '$', 'EA', 'DA', 'SM', '#', 'P1', 'P2', 'P3', '!', 'PD', 'DI')" & _
        " AND tb_numcodl = " & strAnnoIni & _
        " GROUP BY tb_numtipo, tb_numserie, tb_numcodl" & _
        " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABNUMA")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function FileElaboraSetTabNuma(ByVal strDitta As String, ByVal strAnnoIni As String, ByVal strAnnoFin As String, ByVal strtb_numtipo As String, ByVal strtb_numserie As String) As Boolean
    Dim strSQL As String = ""
    Dim ds As DataSet
    Try
      strSQL = " SELECT tb_numtipo" & _
        " FROM tabnuma" & _
        " WHERE tb_numtipo = " & CStrSQL(strtb_numtipo) & _
        " AND tb_numserie = " & CStrSQL(strtb_numserie) & _
        " AND tb_numcodl = " & strAnnoFin & _
        " AND codditt = " & CStrSQL(strDitta)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABNUMA")

      If ds.Tables("TABNUMA").Rows.Count = 0 Then

        strSQL = "INSERT INTO tabnuma (codditt, tb_numtipo, tb_numserie, tb_numcodl, tb_numprog," & _
          " tb_numprog2, tb_numdata, tb_numdata2, tb_numflag, tb_numflag2, tb_numval, tb_numval2, tb_alfpro)" & _
          " SELECT " & CStrSQL(strDitta) & ", tb_numtipo, tb_numserie, " & strAnnoFin & ", 0, tb_numprog2," & _
          " tb_numdata, tb_numdata2, tb_numflag, tb_numflag2, tb_numval, tb_numval2, tb_alfpro" & _
          " FROM tabnuma" & _
          " WHERE tb_numtipo = " & CStrSQL(strtb_numtipo) & _
          " AND tb_numserie = " & CStrSQL(strtb_numserie) & _
          " AND tb_numcodl = " & strAnnoIni & _
        " AND codditt = " & CStrSQL(strDitta)

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function EstMastroCFSDitt(ByVal strDitta As String, ByVal strTipo As String, ByVal lMastro As Integer) As Boolean
    Dim ds As DataSet
    Dim strCPDC As String
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_azcodpcon FROM tabanaz where codditt = " & CStrSQL(strDitta)
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")
      If ds.Tables("TABANAZ").Rows.Count > 0 Then
        strCPDC = NTSCStr(ds.Tables("TABANAZ").Rows(0)!tb_azcodpcon)
      Else
        strCPDC = strDitta ' per sicurezza
      End If

      strSQL = "SELECT tb_tipomast FROM tabmast WHERE tb_codpcon = " & CStrSQL(strCPDC) & " and tb_codmast = " & lMastro
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABMAST")
      If ds.Tables("TABMAST").Rows.Count > 0 Then
        If strTipo = NTSCStr(ds.Tables("TABMAST").Rows(0)!tb_tipomast) Then
          EstMastroCFSDitt = True
        Else
          EstMastroCFSDitt = False
        End If
      Else
        EstMastroCFSDitt = False ' non l'ha trovato in verità...
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetMastro(ByVal strCodpcon As String, ByVal nCodmast As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_desmast FROM tabmast" & _
          " WHERE tb_codpcon = " & CStrSQL(strCodpcon) & _
          " AND tb_codmast = " & nCodmast

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABMAST")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckTable(ByVal strDitta As String, ByVal strTipo As String, ByVal strSerie As String, ByVal nAnno As Integer, ByVal dvrs As DataRowState) As Boolean
    Dim strSQL As String = ""
    Dim ds As DataSet
    CheckTable = True
    Try
      strSQL = "SELECT tb_numtipo FROM tabnuma" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo = '" & RimappaNomiUguali(strTipo) & "'" & _
        " AND tb_numserie = '" & strSerie & "'" & _
        " AND tb_numcodl = " & nAnno & _
        " ORDER BY tb_numtipo, tb_numserie, tb_numcodl"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABNUMA")
      If dvrs = DataViewRowState.Added Then
        If Not ds.Tables("TABNUMA").Rows.Count = 0 Then CheckTable = False
      Else
        If Not ds.Tables("TABNUMA").Rows.Count <= 1 Then CheckTable = False
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function RimappaNomiUguali(ByVal strNome As String) As String
    Dim strSQL As String = ""
    RimappaNomiUguali = ""
    Try
      Select Case strNome
        Case "CQ2"
          Return "CQ"
        Case "CP2"
          Return "CP"
        Case Else
          Return strNome
      End Select
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ScriviTabella(ByVal strDitta As String, ByRef dttTable As DataTable) As Boolean
    Dim dtrChange() As DataRow = Nothing
    Dim strSQL As String = ""
    Dim strSQLVal As String = ""
    Dim i As Integer

    Try

      '-----------------------------
      'prima le delete
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Deleted)
      For i = 0 To dtrChange.Length - 1

        strSQL = "DELETE FROM tabnuma WHERE "
        strSQL = strSQL + " codditt = " & CStrSQL(strDitta) & " AND " & _
        " tb_numtipo = " & CStrSQL(dtrChange(i)("tb_numtipo", DataRowVersion.Original).ToString()) & " AND " & _
        " tb_numserie = " & CStrSQL(dtrChange(i)("tb_numserie", DataRowVersion.Original).ToString()) & " AND " & _
        " tb_numcodl = " & dtrChange(i)("tb_numcodl", DataRowVersion.Original).ToString()
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        dtrChange(i).AcceptChanges()
      Next

      '-----------------------------
      'adesso le insert
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Added)
      strSQL = "INSERT INTO tabnuma (codditt, tb_numtipo, tb_numserie, tb_numcodl," & _
        " tb_numprog, tb_numprog2, tb_numdata, tb_numdata2, tb_numflag, tb_numflag2, " & _
        " tb_numval, tb_numval2, tb_alfpro, tb_note)"
      For i = 0 To dtrChange.Length - 1
        strSQLVal = "(" & CStrSQL(strDitta) & ", " & _
                    CStrSQL(dtrChange(i)("tb_numtipo").ToString()) & ", " & _
                    CStrSQL(dtrChange(i)("tb_numserie").ToString()) & ", " & _
                    dtrChange(i)("tb_numcodl").ToString() & ", " & _
                    dtrChange(i)("tb_numprog").ToString() & ", " & _
                    dtrChange(i)("tb_numprog2").ToString() & ", " & _
                    CDataSQL(NTSCDate((dtrChange(i)("tb_numdata").ToString()))) & ", " & _
                    CDataSQL(NTSCDate((dtrChange(i)("tb_numdata2").ToString()))) & ", " & _
                    CStrSQL(dtrChange(i)("tb_numflag").ToString()) & ", " & _
                    CStrSQL(dtrChange(i)("tb_numflag2").ToString()) & ", " & _
                    CDblSQL(dtrChange(i)("tb_numval").ToString()) & ", " & _
                    CDblSQL(dtrChange(i)("tb_numval2").ToString()) & ", " & _
                    CStrSQL(dtrChange(i)("tb_alfpro").ToString()) & ", " & _
                    CStrSQL(dtrChange(i)("tb_note")) & ")"
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI)
        dtrChange(i).AcceptChanges()
      Next

      '-----------------------------
      'infine le update
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrChange.Length - 1
        strSQL = "UPDATE tabnuma SET " & _
                    " codditt = " & CStrSQL(strDitta) & ", " & _
                    " tb_numtipo = " & CStrSQL(dtrChange(i)("tb_numtipo").ToString()) & ", " & _
                    " tb_numserie = " & CStrSQL(dtrChange(i)("tb_numserie").ToString()) & ", " & _
                    " tb_numcodl = " & dtrChange(i)("tb_numcodl").ToString() & ", " & _
                    " tb_numprog = " & dtrChange(i)("tb_numprog").ToString() & ", " & _
                    " tb_numprog2 = " & dtrChange(i)("tb_numprog2").ToString() & ", " & _
                    " tb_numdata = " & CDataSQL(NTSCDate((dtrChange(i)("tb_numdata").ToString()))) & ", " & _
                    " tb_numdata2 = " & CDataSQL(NTSCDate((dtrChange(i)("tb_numdata2").ToString()))) & ", " & _
                    " tb_numflag = " & CStrSQL(dtrChange(i)("tb_numflag").ToString()) & ", " & _
                    " tb_numflag2 = " & CStrSQL(dtrChange(i)("tb_numflag2").ToString()) & ", " & _
                    " tb_numval = " & CDblSQL(dtrChange(i)("tb_numval").ToString()) & ", " & _
                    " tb_numval2 = " & CDblSQL(dtrChange(i)("tb_numval2").ToString()) & ", " & _
                    " tb_alfpro = " & CStrSQL(dtrChange(i)("tb_alfpro").ToString()) & ", " & _
                    " tb_note = " & CStrSQL(dtrChange(i)("tb_note"))
        strSQL += " WHERE "
        strSQL = strSQL + " codditt = " & CStrSQL(strDitta) & " AND " & _
        " tb_numtipo = " & CStrSQL(dtrChange(i)("tb_numtipo", DataRowVersion.Original).ToString()) & " AND " & _
        " tb_numserie = " & CStrSQL(dtrChange(i)("tb_numserie", DataRowVersion.Original).ToString()) & " AND " & _
        " tb_numcodl = " & dtrChange(i)("tb_numcodl", DataRowVersion.Original).ToString()

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
