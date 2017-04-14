Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica
Imports System.IO

Public Class CLD__HLSR
  Inherits CLD__BASE

  Public Overridable Function GetData(ByVal strDitta As String, ByVal strDescr As String, ByVal strDtIni As String, _
    ByRef dsHlsr As DataSet) As Boolean
    Try
      '-------------------------------------------------------------------------------------------------------------
      Return GetData(strDitta, strDescr, strDtIni, "", 0, 0, "", dsHlsr)
      '-------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetData(ByVal strDitta As String, ByVal strDescr As String, ByVal strDtIni As String, _
    ByVal strTipo As String, ByVal lContoCF As Integer, ByVal lNumreg As Integer, ByVal strDatreg As String, _
    ByRef dsHlsr As DataSet) As Boolean
    Dim strSQL As String = ""

    Try
      '-------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strDescr, strDtIni, strTipo, lContoCF, lNumreg, strDatreg, _
        dsHlsr})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsHlsr = CType(oIn(7), DataSet)
        Return CBool(oOut)
      End If
      '-------------------------------------------------------------------------------------------------------------
      Select Case strTipo
        Case "I" '--- ZOOM SU CODICI IVA
          strSQL = "SELECT mi_codiva AS pn_conto, tb_desciva AS xx_descr1" & _
            " FROM moviva INNER JOIN tabciva ON moviva.mi_codiva = tabciva.tb_codciva" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            IIf(strDtIni.Trim <> "", " AND mi_datreg >= " & CDataSQL(strDtIni), "").ToString & _
            IIf(strDescr.Trim <> "", " AND tb_desciva LIKE " & CStrSQL(strDescr.Replace("*", "%")), "").ToString & _
            " AND CONVERT(VARCHAR(10), mi_datreg, 105) + CAST(mi_numreg AS VARCHAR(10))" & _
            " IN (SELECT CONVERT(VARCHAR(10), mi_datreg, 105) + CAST(mi_numreg AS VARCHAR(10))" & _
            " FROM moviva" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND mi_contocf = " & lContoCF & ")"
        Case Else
          strSQL = "SELECT pn_conto, an_descr1 as xx_descr1" & _
            " FROM prinot INNER JOIN anagra ON prinot.codditt = anagra.codditt AND prinot.pn_conto = anagra.an_conto" & _
            " WHERE an_tipo = 'S'" & _
            " AND anagra.codditt = " & CStrSQL(strDitta) & _
            IIf(strDtIni.Trim <> "", " AND pn_datreg >= " & CDataSQL(strDtIni), "").ToString & _
            IIf(strDescr.Trim <> "", " AND an_descr1 LIKE " & CStrSQL(strDescr.Replace("*", "%")), "").ToString
      End Select
      If strTipo = "A" Then
        '-----------------------------------------------------------------------------------------------------------
        '--- Sottoconti utilizzati dal Conto di testata
        '-----------------------------------------------------------------------------------------------------------
        strSQL += " AND CONVERT(VARCHAR(10), pn_datreg, 105) + CAST(pn_numreg AS VARCHAR(10))" & _
          " IN (SELECT CONVERT(VARCHAR(10), pn_datreg, 105) + CAST(pn_numreg AS VARCHAR(10))" & _
          " FROM prinot" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND pn_conto = " & lContoCF & " )"
      End If
      Select Case strTipo
        Case "I"
          strSQL += " GROUP BY mi_codiva, tb_desciva" & _
            " ORDER BY mi_codiva, tb_desciva"
        Case Else
          strSQL += " GROUP BY pn_conto, an_descr1" & _
            " ORDER BY pn_conto, an_descr1"
      End Select
      dsHlsr = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "PRINOT")
      '-------------------------------------------------------------------------------------------------------------
      '--- Devo togliere i record con descrizione che inzia con il prefisso indicato nella opzione di registro
      '-------------------------------------------------------------------------------------------------------------
      If oApp.oGvar.strZoomScartaDescrPrefix <> "" Then
        For Each dtrT As DataRow In dsHlsr.Tables("PRINOT").Rows
          If NTSCStr(dtrT!xx_descr1).PadRight(oApp.oGvar.strZoomScartaDescrPrefix.Length).Substring(0, oApp.oGvar.strZoomScartaDescrPrefix.Length).ToUpper = oApp.oGvar.strZoomScartaDescrPrefix Then
            dtrT.Delete()
          End If
        Next
        dsHlsr.AcceptChanges()
      End If
      '-------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

End Class
