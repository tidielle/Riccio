Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGGBAR
  Inherits CLD__BASE

  Public Overridable Function GetArticoli(ByVal strDitta As String, ByVal strWhere As String, ByVal bConBc As Boolean, ByVal bUmp As Boolean, _
                                          ByVal bUmSec As Boolean, ByVal bUmConf As Boolean, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strBarcode, strQuery As String
    Try
      If bConBc Then
        strBarcode = ""
      Else
        'Gli articoli TCO vengono sempre considerati, vengono filtrati dopo per taglia
        strBarcode = " AND ((SELECT COUNT(codditt) FROM barcode WHERE barcode.codditt = artico.codditt AND ar_codart = bc_codart AND REPLACE_UNMIS = bc_unmis) = 0 OR ar_codtagl <> 0) "
      End If

      strQuery = "SELECT ar_codart, REPLACE_UNMIS AS xx_unmis, ar_ultfase, ar_codtagl FROM artico " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND REPLACE_UNMIS IS NOT null " & _
                 " AND NOT ((ar_codtagl > 0 OR ar_gesvar = 'S') AND ISNULL(ar_codroot, '') = '') " & _
                 strBarcode

      TraduciWhere(strWhere, strQuery)

      If bUmp Then
        strSQL &= strQuery.Replace("REPLACE_UNMIS", "ar_unmis")
      End If
      If bUmSec Then
        If strSQL.Trim <> "" Then strSQL &= " UNION ALL "
        strSQL &= strQuery.Replace("REPLACE_UNMIS", "ar_unmis2")
      End If
      If bUmConf Then
        If strSQL.Trim <> "" Then strSQL &= " UNION ALL "
        strSQL &= strQuery.Replace("REPLACE_UNMIS", "ar_confez2")
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckBCTaglie(ByVal strDitta As String, ByVal strArt As String, ByVal strUm As String, ByVal strTagl As String, _
                                            ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM barcode " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND bc_codart = " & CStrSQL(strArt) & _
               " AND bc_unmis = " & CStrSQL(strUm) & _
               " AND bc_tagl = " & CStrSQL(strTagl)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function InsertBarcode(ByVal strDitta As String, ByVal strBarcCodart As String, _
                                            ByVal strDummy As String, ByVal strBarcUnmis As String, _
                                            ByVal lOrins As Integer, ByVal strQuale As String, _
                                            ByVal strCampo As String, ByVal lFase As Integer) As Boolean
    Dim dQuant As Decimal = 1
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If (strBarcCodart <> "D") And (strBarcCodart <> "M") Then
        If ValCodiceDb(strBarcCodart, strDitta, "ARTICO", "S", "", dttTmp) = True Then
          If strBarcUnmis <> NTSCStr(dttTmp.Rows(0)!ar_unmis) Then
            If strBarcUnmis = NTSCStr(dttTmp.Rows(0)!ar_confez2) Then
              dQuant = NTSCDec(dttTmp.Rows(0)!ar_qtacon2)
            Else
              dQuant = NTSCDec(dttTmp.Rows(0)!ar_conver)
            End If
          End If
        End If
        dttTmp.Clear()
        dttTmp.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO BARCODE (codditt, bc_codart, bc_datins, bc_code, bc_unmis, bc_quant, bc_orins," & _
        " bc_datagg, bc_oragg, bc_tipo, bc_tagl, bc_fase)" & _
        " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strBarcCodart) & ", " & CDataSQL(Now) & ", " & _
        CStrSQL(strDummy) & ", " & CStrSQL(strBarcUnmis) & ", " & CDblSQL(dQuant) & ", " & lOrins & ", " & _
        CDataOraSQL(Now) & ", " & lOrins & ", " & CStrSQL(strQuale) & ", " & CStrSQL(strCampo) & ", " & lFase & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
End Class
