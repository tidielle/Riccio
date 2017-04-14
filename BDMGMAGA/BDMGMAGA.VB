Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica
Imports System.IO

Public Class CLDMGMAGA
  Inherits CLD__BASE

  Public Overridable Function CheckEsistenzaARTDEF(ByVal strDitta As String, ByVal nCodmaga As Integer) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 ad_magaz FROM artdef" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ad_magaz = " & nCodmaga & _
        " ORDER BY codditt, ad_codart, ad_magaz, ad_fase"
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GetData(ByVal strDitta As String, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tabmaga.*, an_descr1 as xx_magconto, tabmaga1.tb_desmaga as xx_magass, dd_nomdest as xx_coddest, tb_desstab as xx_desstab " & _
                       "FROM (((tabmaga LEFT JOIN tabmaga as tabmaga1 ON tabmaga.codditt = tabmaga1.codditt AND tabmaga.tb_magass = tabmaga1.tb_codmaga) " & _
                       "LEFT JOIN anagra on tabmaga.codditt = anagra.codditt AND tabmaga.tb_magconto = anagra.an_conto) " & _
                       "LEFT JOIN destdiv ON anagra.codditt = destdiv.codditt AND anagra.an_conto = destdiv.dd_conto AND tabmaga.tb_coddest = destdiv.dd_coddest) " & _
                       "LEFT JOIN tabstab ON tabmaga.codditt = tabstab.codditt AND tabmaga.tb_codstab = tabstab.tb_codstab " & _
                       "WHERE tabmaga.codditt = " & CStrSQL(strDitta) & " " & _
                       "ORDER BY tabmaga.tb_codmaga "
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABMAGA")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ValidaUbicazione(ByVal strDitta As String, ByVal lMaga As Integer, ByVal strUbic As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM anaubic " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND au_magaz = " & lMaga & _
               " AND (au_ubicaz = " & CStrSQL(strUbic.PadRight(12, "-"c)) & _
               "      OR au_barcode = " & CStrSQL(strUbic) & ")"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
End Class
