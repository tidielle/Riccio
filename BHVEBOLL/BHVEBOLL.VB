Imports System.Data
Imports System.Data.Common
Imports NTSInformatica.CLN__STD
Imports NTSInformatica.CLD__BASE
Imports System
Public Class CLHVEBOLL
    Inherits CLDVEBOLL

    Public Overridable Sub AggiungiArticolo_import(ByVal strDitta As String, ByVal strArticolo As String, strDescr As String)
        Dim strSQL As String
        Dim dbConn As DbConnection = Nothing
        'dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
        'ApriTrans(dbConn)

        strSQL = "INSERT INTO artico (codditt, ar_codart, ar_descr, ar_unmis, ar_codiva) values (" &
            CStrSQL(strDitta) & "," &
        CStrSQL(strArticolo) & "," &
            CStrSQL(strDescr) & ",'PZ',22)"


        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)


    End Sub
End Class