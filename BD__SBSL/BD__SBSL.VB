Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLD__SBSL
  Inherits CLD__BASE


  Public Overridable Function Apri(ByVal strDitta As String, ByVal strDtStart As String, _
                                   ByVal strDtEnd As String, ByVal strDeviceName As String, _
                                   ByVal strUser As String, ByVal strTipoEventi As String, _
                                   ByRef dsOut As DataSet) As Boolean
    'se chiedo un peridoo da data a data, prendo anche un po' di giorni prima, visto he potrei essermi connesso
    'il giorno precedente e non essermi mai disconnesso
    'xx_durata sarà ore, minuti
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 2000 sbslog.*, sb_ultagg as xx_data, 0.0 as xx_durata, '' as xx_evento, " & _
               " 'N' as xx_disconn FROM sbslog " & _
               " Where sb_ultagg >= " & CDataSQL(NTSCDate(strDtStart).AddDays(-7)) & _
               " AND sb_ultagg <= " & CDataSQL(NTSCDate(strDtEnd).AddDays(1)) & _
               " AND sb_child <> 'NTSUIMSGBOX'" & _
               " AND sb_child <> 'FRM__MENU_X'" & _
               " AND sb_child <> 'FRMMENU'"
      Select Case strTipoEventi
        Case "A" : strSQL += "AND sb_evento NOT IN ('A', 'X')"
        Case "P" : strSQL += "AND sb_evento IN ('A', 'X')"
      End Select
      If strDeviceName.Trim <> "" Then strSQL += " AND sb_devicename = " & CStrSQL(strDeviceName)
      If strUser.Trim <> "" Then strSQL += " AND sb_user = " & CStrSQL(strUser)

      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "SBSLOG")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function Apri_CountRk(ByVal strDitta As String, ByVal strDtStart As String, _
                                   ByVal strDtEnd As String, ByVal strDeviceName As String, _
                                   ByVal strUser As String, ByVal strTipoEventi As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT count(*) as NREC FROM sbslog " & _
               " Where sb_ultagg >= " & CDataSQL(NTSCDate(strDtStart).AddDays(-7)) & _
               " AND sb_ultagg <= " & CDataSQL(NTSCDate(strDtEnd).AddDays(1)) & _
               " AND sb_child <> 'NTSUIMSGBOX'" & _
               " AND sb_child <> 'FRM__MENU_X'" & _
               " AND sb_child <> 'FRMMENU'"
      Select Case strTipoEventi
        Case "A" : strSQL += "AND sb_evento NOT IN ('A', 'X')"
        Case "P" : strSQL += "AND sb_evento IN ('A', 'X')"
      End Select
      If strDeviceName.Trim <> "" Then strSQL += " AND sb_devicename = " & CStrSQL(strDeviceName)
      If strUser.Trim <> "" Then strSQL += " AND sb_user = " & CStrSQL(strUser)

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      If dttTmp.Rows.Count > 0 Then
        Return NTSCInt(dttTmp.Rows(0)!NREC)
      End If

      Return 0

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function


  Public Overridable Function Cancella(ByVal strDitta As String, ByVal strDtStart As String, _
                                       ByVal strDtEnd As String, ByVal strDeviceName As String, _
                                       ByVal strUser As String, ByVal strTipoEventi As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM sbslog " & _
               " Where sb_ultagg >= " & CDataOraSQL(strDtStart & " 00:00:00") & _
               " AND sb_ultagg <= " & CDataOraSQL(strDtEnd & " 23:59:59")
      Select Case strTipoEventi
        Case "A" : strSQL += "AND sb_evento NOT IN ('A', 'X')"
        Case "P" : strSQL += "AND sb_evento IN ('A', 'X')"
      End Select
      If strDeviceName.Trim <> "" Then strSQL += " AND sb_devicename = " & CStrSQL(strDeviceName)
      If strUser.Trim <> "" Then strSQL += " AND sb_user = " & CStrSQL(strUser)

      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

End Class
