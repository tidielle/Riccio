Imports NTSInformatica.CLN__STD
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports NTSInformatica
Imports System.IO

Public Class CLDVEBOTR
  Inherits CLD__BASE

  Public Overridable Function GetData(ByVal strDitta As String, ByRef dsBotr As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim strTmp As String = ""
    Try
      '-------------------------------------------------------------------------------------
      '--- Apre il recordset su TABBOTR
      '-------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM tabbotr"
      dsBotr = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABBOTR")
      '-------------------------------------------------------------------------------------
      '--- Adesso aggiunge le colonne per le decodifiche
      '-------------------------------------------------------------------------------------
      With dsBotr.Tables("TABBOTR").Columns
        .Add(New DataColumn("xx_codesb_1", GetType(String)))
        .Add(New DataColumn("xx_codesb_2", GetType(String)))
        .Add(New DataColumn("xx_codesb_3", GetType(String)))
        .Add(New DataColumn("xx_codesb_4", GetType(String)))
        .Add(New DataColumn("xx_codesb_5", GetType(String)))
        .Add(New DataColumn("xx_codesb_6", GetType(String)))
        .Add(New DataColumn("xx_codesb_7", GetType(String)))
        .Add(New DataColumn("xx_codesb_8", GetType(String)))
      End With
      If dsBotr.Tables("TABBOTR").Rows.Count > 0 Then
        With dsBotr.Tables("TABBOTR").Rows(0)
          If NTSCInt(!tb_codesb_1) > 0 Then
            If ValCodiceDb(!tb_codesb_1.ToString, strDitta, "TABCIVA", "N", strTmp) Then !xx_codesb_1 = strTmp
          End If
          If NTSCInt(!tb_codesb_2) > 0 Then
            If ValCodiceDb(!tb_codesb_2.ToString, strDitta, "TABCIVA", "N", strTmp) Then !xx_codesb_2 = strTmp
          End If
          If NTSCInt(!tb_codesb_3) > 0 Then
            If ValCodiceDb(!tb_codesb_3.ToString, strDitta, "TABCIVA", "N", strTmp) Then !xx_codesb_3 = strTmp
          End If
          If NTSCInt(!tb_codesb_4) > 0 Then
            If ValCodiceDb(!tb_codesb_4.ToString, strDitta, "TABCIVA", "N", strTmp) Then !xx_codesb_4 = strTmp
          End If
          If NTSCInt(!tb_codesb_5) > 0 Then
            If ValCodiceDb(!tb_codesb_5.ToString, strDitta, "TABCIVA", "N", strTmp) Then !xx_codesb_5 = strTmp
          End If
          If NTSCInt(!tb_codesb_6) > 0 Then
            If ValCodiceDb(!tb_codesb_6.ToString, strDitta, "TABCIVA", "N", strTmp) Then !xx_codesb_6 = strTmp
          End If
          If NTSCInt(!tb_codesb_7) > 0 Then
            If ValCodiceDb(!tb_codesb_7.ToString, strDitta, "TABCIVA", "N", strTmp) Then !xx_codesb_7 = strTmp
          End If
          If NTSCInt(!tb_codesb_8) > 0 Then
            If ValCodiceDb(!tb_codesb_8.ToString, strDitta, "TABCIVA", "N", strTmp) Then !xx_codesb_8 = strTmp
          End If
        End With
      End If
      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function OpzioniPerDitta() As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT rd_dipaz FROM REGDIR" & _
        " WHERE UPPER(rd_liv1) = 'OPZIONI'" & _
        " AND rd_dipaz = 'S'"
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC).Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CheckEsistenzaOpzioni() As Integer
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return CheckEsistenzaOpzioni("")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function CheckEsistenzaOpzioni(ByVal strNomprop As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strNomprop})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCInt(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT COUNT(*) AS Records FROM regprop" & _
        " WHERE rp_nomprop = " & CStrSQL(IIf(strNomprop.Trim <> "", strNomprop, "DeterminaBolliSuOperazEsenti").ToString) & _
        " AND rp_liv2 = '.'" & _
        " AND rp_liv3 = '.'"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      If dttTmp.Rows.Count > 0 Then Return NTSCInt(dttTmp.Rows(0)!Records)
      '--------------------------------------------------------------------------------------------------------------
      Return 0
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function RitornaValProp(ByVal strLiv1 As String, ByVal strLiv2 As String, _
    ByVal strLiv3 As String, ByVal strNomprop As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT vp_valore AS cod, vp_valore + ' - ' + vp_descrval AS val FROM VALPROP" & _
        " WHERE UPPER(vp_liv1) = " & CStrSQL(strLiv1.ToUpper) & _
        " AND UPPER(vp_liv2) = " & CStrSQL(strLiv2.ToUpper) & _
        " AND UPPER(vp_liv3) = " & CStrSQL(strLiv3.ToUpper) & _
        " AND UPPER(vp_nomprop) = " & CStrSQL(strNomprop.ToUpper)
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      '--------------------------------------------------------------------------------------------------------------
      If dttOut.Rows.Count = 0 Then
        dttOut.Clear()
        dttOut.Dispose()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

End Class
