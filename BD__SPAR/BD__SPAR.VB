Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica
Imports System.IO

Public Class CLD__SPAR
  Inherits CLD__BASE

  Public Overridable Function GetCodici(ByRef ds As DataSet, ByVal strElencoCodici As String) As Boolean
    Dim strSQL As String = ""
    Dim bOrderByDescr As Boolean = False
    Try

      bOrderByDescr = CBool(GetSettingBus("BS--SPAR", "RECENT", ".", "OrderByDesc", "", "", "0"))

      strSQL = "SELECT Pz_Codform, Pz_Titstam FROM PARSTAG"
      If strElencoCodici <> "" Then
        strSQL = strSQL & " WHERE Pz_Codform IN (" & strElencoCodici & ")"
      End If

      If bOrderByDescr Then
        strSQL = strSQL & " ORDER BY Pz_Titstam"
      Else
        strSQL = strSQL & " ORDER BY Pz_Codform"
      End If


      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "PARSTAG")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function Esegui(ByVal strSQL As String, ByRef nRec As Integer) As Boolean
    Dim dbConn As DbConnection = Nothing
    Try

      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()

      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetParstag(ByRef ds As DataSet, ByVal strPz_Codform As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM PARSTAG" & _
        " WHERE Pz_Codform = " & strPz_Codform

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "PARSTAG")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetHlvlData(ByVal strTabella As String, ByVal strCampo As String, ByVal strHlvlTabella As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DISTINCT " & strCampo & " AS xx_valori FROM " & strHlvlTabella

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TAB")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetGrspData(ByVal strSQL As String, ByRef ds As DataSet, ByRef strErr As String) As Boolean
    Try
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TAB")
      Return True

    Catch ex As Exception
      strErr = ex.Message
      Return False
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function RiempiParstagacc() As Boolean
    'sposto i valori dalla vechcia opzione di registro alla nuova tabella parstagacc
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Try
      strSQL = "SELECT rp_idut, rp_valprop FROM regprop WHERE rp_liv1 = 'BS--SPAR' AND rp_liv2 = 'OPZIONIUT' AND rp_nomprop = 'CanExecute'"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      For Each dtrT As DataRow In dttTmp.Rows
        For Each strT As String In NTSCStr(dtrT!rp_valprop).Split(";"c, ","c)
          If strT.Trim <> "" Then
            strSQL = "INSERT INTO parstagacc (pza_codform, pza_nome, pza_ruolo) " & _
                     " VALUES (" & strT.Trim & ", " & CStrSQL(dtrT!rp_idut) & ", ' ')"
            Try
              Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
            Catch ex As Exception
              'ignoro l'errore: potrebbe essere un codice duplicato
            End Try
          End If
        Next
      Next

      'rimuovo le vecchie impostazioni, ormai obsolete
      If dttTmp.Rows.Count > 0 Then
        strSQL = "DELETE regprop WHERE rp_liv1 = 'BS--SPAR' AND rp_liv2 = 'OPZIONIUT' AND rp_nomprop = 'CanExecute'"
        Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetParstagacc(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT pza_codform FROM parstagacc " & _
               " WHERE pza_nome = " & CStrSQL(oApp.User.Nome) & _
               " OR pza_ruolo = " & CStrSQL(oApp.User.Gruppo)
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

End Class
