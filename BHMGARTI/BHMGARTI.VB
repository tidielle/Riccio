Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLHMGARTI
    Inherits CLDMGARTI
    Public Overrides Function GetDataApri(ByVal strDitta As String, ByVal strQuery As String, ByVal strCodArt As String, ByRef ds As System.Data.DataSet) As Boolean
        If MyBase.GetDataApri(strDitta, strQuery, strCodArt, ds) Then
            Dim strSQL As String = ""
            Try
                'strSQL = "SELECT barcode.*, artfasi.af_descr as xx_fase" &
                '  " FROM barcode" &
                '  " LEFT JOIN artfasi ON barcode.codditt = artfasi.codditt AND barcode.bc_codart = artfasi.af_codart AND barcode.bc_fase = artfasi.af_fase" &
                '  " WHERE barcode.codditt = " & CStrSQL(strDitta) &
                '  " AND bc_codart = " & CStrSQL(strCodArt) &
                '  " ORDER BY barcode.codditt, bc_datins"
                strSQL = "SELECT *" &
                  " FROM TABHHAR" &
                  " WHERE TABHHAR.codditt = " & CStrSQL(strDitta) &
                  " AND tb_codart = " & CStrSQL(strCodArt)

                OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABHHAR", ds)

            Catch ex As Exception

            End Try
            Return True
        End If

    End Function


End Class
