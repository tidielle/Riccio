Imports System.Data
Imports System.Data.Common
Imports NTSInformatica.CLN__STD
Public Class CLHORGSOR
    Inherits CLDORGSOR
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

    'Public Overrides Function ZoomSeor(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String,
    '                       ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String,
    '                       ByVal strDataA As String, ByVal strDatConsDa As String, ByVal strDatConsA As String,
    '                       ByVal strEvaso As String, ByVal strRiferim As String, ByVal strDitta As String,
    '                       ByVal strBlocco As String, ByVal strSospeso As String, ByVal bModuloCRM As Boolean,
    '                       ByVal bIsCRMUser As Boolean, ByVal nCodcageAccdito As Integer,
    '                       ByVal strWhereFiltriEstesi As String, ByVal strWhereFiltriEstesiRighe As String) As DataSet
    '    Dim dsTmp As DataSet
    '    Dim dsPrz As DataTable
    '    Dim i As Integer = 0
    '    Dim J As Integer = 0
    '    Dim td_numord As Integer
    '    dsTmp = MyBase.ZoomSeor(strTipork, nAnno, strSerie,
    '                        lContoDa, lContoA, strDataDa,
    '                        strDataA, strDatConsDa, strDatConsA,
    '                        strEvaso, strRiferim, strDitta,
    '                        strBlocco, strSospeso, bModuloCRM,
    '                        bIsCRMUser, nCodcageAccdito,
    '                        strWhereFiltriEstesi, strWhereFiltriEstesiRighe)

    '    dsTmp.Tables("seor").Columns.Add("xx_sc", GetType(Decimal))
    '    dsTmp.Tables("seor").Columns.Add("xx_nr", GetType(Decimal))
    '    dsTmp.Tables("seor").Columns.Add("xx_ft", GetType(Decimal))


    '    For i = 0 To dsTmp.Tables(0).Rows.Count - 1
    '        td_numord = NTSCInt(dsTmp.Tables(0).Rows(i)("td_numord").ToString)
    '        Dim td_anno As Integer = NTSCInt(dsTmp.Tables(0).Rows(i)("td_anno").ToString)
    '        Dim td_tipork As String = NTSCStr(dsTmp.Tables(0).Rows(i)("td_tipork").ToString)
    '        Dim td_serie As String = NTSCStr(dsTmp.Tables(0).Rows(i)("td_serie").ToString)
    '        dsPrz = prezzoivato(strDitta, td_tipork, td_anno, td_serie, td_numord)

    '        For J = 0 To dsPrz.Rows.Count - 1
    '            If NTSCInt(dsPrz.Rows(J)("mo_magaz")) = 2 Then
    '                dsTmp.Tables(0).Rows(i)("xx_nr") = NTSCDec(dsPrz.Rows(J)("totivato").ToString)
    '            ElseIf NTSCInt(dsPrz.Rows(j)("mo_magaz")) = 1 And dsPrz.Rows(J)("mo_controp").ToString = "1020" Then
    '                dsTmp.Tables(0).Rows(i)("xx_sc") = NTSCDec(dsPrz.Rows(J)("totivato").ToString)
    '            ElseIf NTSCInt(dsPrz.Rows(j)("mo_magaz")) = 1 And dsPrz.Rows(J)("mo_controp").ToString = "1007" Then
    '                dsTmp.Tables(0).Rows(i)("xx_ft") = NTSCDec(dsPrz.Rows(J)("totivato").ToString)
    '            End If
    '        Next

    '    Next
    '    Return dsTmp
    'End Function
    'Public Overridable Function prezzoivato(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As DataTable
    '    Dim strsql As String
    '    Dim dsPrz As DataTable
    '    strsql = "SELECT movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord," &
    '              "movord.mo_magaz, SUM(movord.mo_valore * (1 + tabciva.tb_aliq / 100)) AS totivato,  movord.mo_controp" &
    '              "  From movord INNER Join tabciva On movord.mo_codiva = tabciva.tb_codciva" &
    '               " WHERE codditt = " & CStrSQL(strDitta) &
    '               " AND movord.mo_tipork = 'R'" &
    '               " AND movord.mo_anno = " & nAnno &
    '               " AND movord.mo_serie = " & CStrSQL(strSerie) &
    '               " AND movord.mo_numord = " & lNumdoc.ToString &
    '               " Group By movord.mo_tipork, movord.mo_anno, movord.mo_serie, " &
    '               " movord.mo_numord, movord.mo_magaz, movord.mo_controp"
    '    dsPrz = OpenRecordset(strsql, CLE__APP.DBTIPO.DBAZI)
    '    Return dsPrz
    'End Function
End Class
