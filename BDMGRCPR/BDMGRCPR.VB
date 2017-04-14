Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDMGRCPR
  Inherits CLD__BASE

  Public Overridable Sub RitornaAltriDati(ByVal strDitta As String, _
    ByVal strCodart As String, ByVal nValuta As Integer, ByVal dtData As Date, _
    ByVal dQuant As Decimal, ByVal strUnmis As String, ByVal nFase As Integer, _
    ByRef dUltcos As Decimal, ByRef dDaQuant As Decimal, ByRef dAQuant As Decimal)
    Dim strSQL As String = ""
    Dim strCodartList As String = ""
    Dim dttTmp As New DataTable

    Try
      '----------------------------------------------------------------------------------
      dUltcos = 0
      strSQL = "SELECT apx_ultcos FROM artprox" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND apx_codart = " & CStrSQL(strCodart) & _
               "   AND apx_fase = " & nFase
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dUltcos = NTSCDec(dttTmp.Rows(0)!apx_ultcos.ToString)
      End If
      dttTmp.Clear()
      '----------------------------------------------------------------------------------
      dDaQuant = 0
      dAQuant = 9999999999
      strSQL = "SELECT TOP 1 lc_daquant, lc_aquant FROM busvw_listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCodart) & _
        " AND lc_tipo = ' '" & _
        " AND lc_conto = 0" & _
        " AND lc_listino = 1" & _
        " AND lc_codvalu = " & nValuta & _
        " AND lc_codlavo = 0" & _
        " AND lc_codtpro = 0" & _
        " AND lc_datagg <= " & CDataSQL(dtData) & _
        " AND lc_datscad >= " & CDataSQL(dtData) & _
        " AND lc_daquant <= " & CDblSQL(Math.Abs(dQuant)) & _
        " AND lc_aquant >= " & CDblSQL(Math.Abs(dQuant)) & _
        " AND lc_unmis = " & CStrSQL(strUnmis) & _
        " AND lc_fase = " & nFase & _
        " ORDER BY lc_conto , lc_listino, lc_datagg DESC, lc_daquant, lc_fase"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        If Not (NTSCDec(dttTmp.Rows(0)!lc_daquant.ToString) = 0 And _
                NTSCDec(dttTmp.Rows(0)!lc_aquant.ToString) = 9999999999) Then
          dDaQuant = NTSCDec(dttTmp.Rows(0)!lc_daquant.ToString)
          dAQuant = NTSCDec(dttTmp.Rows(0)!lc_aquant.ToString)
        End If
      End If
      dttTmp.Clear()
      '----------------------------------------------------------------------------------
      Return
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub RitornaDatiListino(ByVal strDitta As String, _
    ByVal strCodart As String, ByVal nListino As Integer, ByVal nValuta As Integer, _
    ByVal dtData As Date, ByVal dQuant As Decimal, ByVal strUnmis As String, _
    ByVal nFase As Integer, _
    ByRef dPrezzo As Decimal, ByRef dtDatagg As Date)
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '-------------------------------------------------------------------------------------
      dPrezzo = 0
      dtDatagg = NTSCDate(IntSetDate("01/01/1900"))
      '-------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 lc_prezzo, lc_datagg FROM busvw_listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCodart) & _
        " AND lc_tipo = ' '" & _
        " AND lc_conto = 0" & _
        " AND lc_listino = " & nListino & _
        " AND lc_codvalu = " & nValuta & _
        " AND lc_codlavo = 0" & _
        " AND lc_codtpro = 0" & _
        " AND lc_datagg <= " & CDataSQL(dtData) & _
        " AND lc_datscad >= " & CDataSQL(dtData) & _
        " AND lc_daquant <= " & CDblSQL(Math.Abs(dQuant)) & _
        " AND lc_aquant >= " & CDblSQL(Math.Abs(dQuant)) & _
        " AND lc_unmis = " & CStrSQL(strUnmis) & _
        " AND lc_fase = " & nFase & _
        " ORDER BY lc_conto , lc_listino, lc_datagg DESC, lc_daquant, lc_fase"
      '-------------------------------------------------------------------------------------
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-------------------------------------------------------------------------------------
      If dttTmp.Rows.Count > 0 Then
        dPrezzo = NTSCDec(dttTmp.Rows(0)!lc_prezzo.ToString)
        dtDatagg = NTSCDate(dttTmp.Rows(0)!lc_datagg.ToString)
      End If
      '-------------------------------------------------------------------------------------
      dttTmp.Clear()
      '-------------------------------------------------------------------------------------
      Return
      '-------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '-------------------------------------------------------------------------------------
    End Try
  End Sub

End Class